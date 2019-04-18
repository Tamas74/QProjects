using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Edidev.FrameworkEDI;
using System.IO;

namespace gloBilling
{
    public partial class gloClaimManager
    {

        public string EDI837GenerationForUB(ArrayList SelectedTransactions, ArrayList SelectedMasterTransactions, string _BatchName, bool _IsUndo, Int64 _ContactID, dsEDIClaimdetails odsEDIClaimDetail, Int64 _nBatchID)
        {
            DataSet dsMaster = null;
            DataSet dsHeader = null;  

         
            string _result = "";
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string _ClaimStatus = "";        
            string sEdiFile, sPath;
            ediDocument oEdiDoc = null;
            ediSchema oSchema = null;
            ediSchemas oSchemas = null;
            ediInterchange oInterchange = null;
            ediGroup oGroup = null;
            ediTransactionSet oTransactionset = null;
            ediDataSegment oSegment = null;
            string sSEFFile = "";
            bool _IsSEFPresent = true;
            string _TypeOfData = "T";


            
                #region " Generate UB EDI "

                string sInstance = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
              //  gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                gloUB04 ogloUB04 = new gloUB04();//ub
                TransactionEDI oTransaction = null;               
                               
                try
                {
                    #region "Load EDI"

                 
                   // sPath = AppDomain.CurrentDomain.BaseDirectory;
                    sSEFFile = "837_X096A1.SEF";

                    oEdiDoc = new ediDocument();

                    // Change the cursor type from dynamic to forward to improve speed performance
                    oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                    // Disable the internal standard reference library to be memory efficient
                    oSchemas = oEdiDoc.GetSchemas();
                    oSchemas.EnableStandardReference = false;

                    // Load the SEF file
                    oSchema = oEdiDoc.ImportSchema(sSEFPath + sSEFFile, 0);


                    if (File.Exists(sSEFPath + sSEFFile) == false)
                    {
                        MessageBox.Show("837 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _IsSEFPresent = false;
                        return "";
                    }


                    oEdiDoc.SegmentTerminator = "~\r\n";
                    oEdiDoc.ElementTerminator = "*";
                    oEdiDoc.CompositeTerminator = ":";

                    #endregion

                    //Get Clearing House Information in Data table
                if (_IsSEFPresent == true)
                {

                    #region "Header Data - Dataset define in table"

                    dsHeader = GetHeader_EDI_UB_4010(_ContactID, _ClinicID, Convert.ToInt64(SelectedTransactions[SelectedTransactions.Count - 1]));
                    if (dsHeader == null)
                    {
                        return "";
                    }
                    if (dsHeader.Tables == null)
                    {
                        return "";
                    }

                    DataTable dtClearingHouse = dsHeader.Tables["ClearingHouseData"];
                    DataTable dtSubmitter = dsHeader.Tables["SubmitterData"];

                    #endregion              
                 
                    if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                    {
                        MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }
                                  
                    if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                    {
                        MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }                                                       

                    #region " Interchange Segment "
                    //Create the interchange segment
                    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                    if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                    {
                        _TypeOfData = "T";
                    }
                    else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                    {
                        _TypeOfData = "P";
                    }

                    oSegment.set_DataElementValue(1, 0, "00");
                    oSegment.set_DataElementValue(3, 0, "00");
                    oSegment.set_DataElementValue(5, 0, "ZZ");
                    oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SenderID.Trim());//"1234545");//
                    oSegment.set_DataElementValue(7, 0, "ZZ");
                    oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_ReceiverID.Trim().Replace("*",""));//"V2EL");//
                    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    oSegment.set_DataElementValue(11, 0, "U");
                    oSegment.set_DataElementValue(12, 0, "00401");
                    InterchangeHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                    oSegment.set_DataElementValue(14, 0, "0");
                    oSegment.set_DataElementValue(15, 0, _TypeOfData);
                    oSegment.set_DataElementValue(16, 0, ":");

                    #endregion " Interchange Segment "

                    #region " Functional Group "

                    //Create the functional group segment
                    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X096A1"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                    oSegment.set_DataElementValue(1, 0, "HC");
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));////_SenderName);
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//// _ReceiverCode.Trim());//"ClarEDI");
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    FunctionalGroupHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                    oSegment.set_DataElementValue(7, 0, "X");
                    oSegment.set_DataElementValue(8, 0, "004010X096A1");

                    #endregion " Functional Group "

                    #region ST - TRANSACTION SET HEADER

                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                    TransactionSetHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"

                    #endregion ST - TRANSACTION SET HEADER

                    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    //Beginning  Segment 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                    oSegment.set_DataElementValue(1, 0, "0019"); //Hierarchical Structure Code
                    oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                    oSegment.set_DataElementValue(3, 0, TransactionSetHeader);//"1234"); //Reference identification
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Date of claim
                    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //"1230");
                    oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    #region REF - TRANSMISSION TYPE IDENTIFICATION

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                    oSegment.set_DataElementValue(1, 0, "87");
                    oSegment.set_DataElementValue(2, 0, "004010X096A1");//"ReferenceID"

                    #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                    #region NM1 - SUBMITTER


                    //1000A SUBMITTER
                    //NM1 SUBMITTER

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim().Replace("*",""));//clinic code or Electronic Transmitter Identification No.
                    }

                    //PER SUBMITTER EDI CONTACT INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//txtSubmitterContactName.Text.Trim().Replace("*",""));//Contact person name of clinic
                    }
                    else
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    }

                    oSegment.set_DataElementValue(3, 0, "TE");
                    if (dtSubmitter != null && Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""));//txtSubmitterPhone.Text.Trim().Replace("*","").Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                    }

                    #endregion NM1 - SUBMITTER

                    #region NM1 - RECEIVER NAME

                    //1000B RECEIVER
                    //NM1 RECEIVER NAME
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "40");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sClearingHouseCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"GatewayEDI");//clearing house or contractor or carrier or FI name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]) != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.
                    }

                    #endregion NM1 - RECEIVER NAME

                    nHlCount = 0;

                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {
                            for (int i = 0; i < SelectedTransactions.Count; i++)
                            {
                                _ClaimStatus = "";
                                oTransaction = null;                               
                                TransactionLineEDI oTransLine = null;                                
                                UB04Transaction oUBTransaction = ogloUB04.GetUBClaim(Convert.ToInt64(SelectedMasterTransactions[i]), Convert.ToInt64(SelectedTransactions[i]));
                                if (oUBTransaction != null)
                                {
                                    oTransaction = oUBTransaction.Transaction;
                                }
                                #region "Master EDI data - Dataset data set in data table "

                                dsMaster = null;
                                dsMaster = GetMaster_EDI_UB_4010(oTransaction.ContactID, oTransaction.ProviderID, oTransaction.ResponsibilityNo,
                                    oTransaction.TransactionMasterID, Convert.ToInt64(oTransaction.FacilityCode), _ClinicID, oTransaction.IsSameAsBillingProvider,
                                    oTransaction.TransactionID, false);

                                DataTable dtPatientInsurances = dsMaster.Tables["PatientInsurance"];
                                DataTable dtFacility = dsMaster.Tables["Facility"];
                                DataTable dtBillingProvider = dsMaster.Tables["BillingProvider"];
                                DataTable dtPatientPaid = dsMaster.Tables["PatientPaid"];
                                DataTable dtDx = dsMaster.Tables["Diagnosis"];                             
                                DataTable dtMasterSetting = dsMaster.Tables["MasterSetting"];

                                #endregion


                                if (oUBTransaction != null && oTransaction != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    {

                                        string _ClaimNo = "";

                                        #region "Formatting the Claim Number"

                                        _ClaimNo = FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNumber));

                                        #endregion
                                                                                                                                                        
                                            if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                            {
                                                MessageBox.Show("Patient " + oTransaction.PatientFirstName + " " + oTransaction.PatientLastName + " Insurance details are missing for claim number " + _ClaimNo + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }                                                                                                                                                                 

                                        for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                        {
                                            //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                            nHlCount = nHlCount + 1;
                                            nHlProvParent = nHlCount;
                                            //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                            //HL-BILLING PROVIDER
                                            string _PayerResponsibilityName = "";
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                            oSegment.set_DataElementValue(3, 0, "20");
                                            oSegment.set_DataElementValue(4, 0, "1");
                                         
                                            #region Billing Provider
                                         
                                            //2010AA BILLING PROVIDER
                                            //NM1 BILLING PROVIDER NAME
                                            if (dtBillingProvider  != null && dtBillingProvider.Rows.Count > 0)
                                            {
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "85");
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtBillingProvider.Rows[0]["EntityType"]));
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                }
                                               
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                }                                            
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                }

                                                //N3 BILLING PROVIDER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address
                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address2"])));//Provider Address

                                                //N4 BILLING PROVIDER LOCATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state

                                                //oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", "").Replace("~","").Replace(":","").Replace("-",""));//Provider ZIP
                                                oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP


                                                //REF 
                                                string _OtherQualifier = "";
                                                string _OtherQualifierDesc = "";
                                                bool _IsOtherDefaultID = false;
                                                _OtherQualifierDesc = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierDescription"]));
                                                _IsOtherDefaultID = Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]);
                                                if ((_OtherQualifierDesc.Contains("SSN") || _OtherQualifierDesc.Contains("Social Security number")) && _IsOtherDefaultID == true)
                                                {                                                    
                                                    _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDISSNQUALIFIER"]);                                                 
                                                }
                                                else if (_OtherQualifierDesc.Contains("Employer's Identification Number") && _IsOtherDefaultID == true)
                                                {                                                   
                                                    _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDIEMPIDQUALIFIER"]);                                                    
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                    if (_OtherQualifier != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, _OtherQualifier);//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                    }
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));

                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }
                              

                                            #endregion

                                            //'******************************************************************************************************
                                            //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                            //'******************************************************************************************************
                                            #region Subscriber
                                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                            {
                                                string _strRelation = "";
                                                string _strInsuranceType = "";
                                                _strRelation = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                _strInsuranceType = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                              

                                                #region Subscriber HL Loop - 2000B

                                                nHlCount = nHlCount + 1;
                                                nHlSubscriberParent = nHlCount;

                                                //2000B SUBSCRIBER HL LOOP
                                                //HL-SUBSCRIBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(3, 0, "22");

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "0");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "1");

                                                }

                                                //SBR SUBSCRIBER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                                             
                                                oSegment.set_DataElementValue(1, 0, "P");                                              

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "18");                                        
                                                }

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludePlanname"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                }

                                                //This is Claim filling Indicator code in EDI implementation guide.
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                //2010BA SUBSCRIBER
                                                //NM1 SUBSCRIBER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                }
                                             
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "MI");
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Insurance Id"
                                                }

                                                //N3 SUBSCRIBER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"
                                                }

                                                //N4 SUBSCRIBER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                #endregion SubscriberHL Loop - 2000B
                                               
                                                string _SubscriberGender = "";
                                                //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION   

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");

                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"

                                                    _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    if (_SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        _SubscriberGender = "U";
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 1).ToUpper());//"SubscriberGender"
                                                }


                                                #region Payer Information Loop 2010BB
                                                //2010BC SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 34);

                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                }

                                                string str = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim();
                                                ////////N3 PAYER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceAddress"                                                

                                                ////////N4 PAYER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"InsuranceZip"
                                                #endregion

                                                if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sClaimOfficeNumber"] != null)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "FY");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()));
                                                }

                                                if (_strRelation != "18")
                                                {
                                                    nHlCount = nHlCount + 1;
                                                    //2000B DEPENDENT HL LOOP
                                                    //HL-DEPENDENT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                    oSegment.set_DataElementValue(3, 0, "23");
                                                    oSegment.set_DataElementValue(4, 0, "0");

                                                    //PAT - PATIENT/DEPENDENT INFORMATION

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //01 - Spouse 19 - Child

                                                    #region " Patient Info"

                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "QC");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientLastName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Last Name
                                                    oSegment.set_DataElementValue(4, 0, oTransaction.PatientFirstName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient First Name

                                                    if (oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Middle Name
                                                    }

                                                    //N3 - ADDRESS INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientAddress1.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Address"

                                                    //N4 - GEOGRAPHIC LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientCity.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"City"
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.PatientState.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"State"
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientZip.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"Zip"

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.PatientDOB.ToShortDateString())));
                                                    if (oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "" || oTransaction.PatientGender.Trim().Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"                                                                   
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberGender"
                                                    }

                                                    #endregion " Patient Info"

                                                }
                                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                  //  oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;
                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                    //oTransLine.Dispose();//UB04
                                                }

                                                _ClaimTotal = _claimAmount.ToString("#0.00");
                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }

                                                #region Claim Details - Loop 2300
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));

                                                oSegment.set_DataElementValue(1, 0, _ClaimNo); //Patient Account no         
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(5, 1, Convert.ToString(oUBTransaction.Facilitytypecode).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //21 - Inpatient Hospital

                                                if (oTransaction.IsRebill == true || oTransaction.IsReplacementClaim == true)
                                                {
                                                    _ClaimStatus = "7";
                                                }
                                                else if (Convert.ToString(oUBTransaction.Frequencytypecode).Trim() != "")
                                                {
                                                    _ClaimStatus = Convert.ToString(oUBTransaction.Frequencytypecode);
                                                }
                                                oSegment.set_DataElementValue(5, 2, "A");//UB HardCoded Facility Code Qualifier
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus); //Question......
                                                oSegment.set_DataElementValue(6, 0, "Y");
                                                
                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    _IsAccessAssignment = Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAccessAssignment"]);
                                                }

                                                if (_IsAccessAssignment == true)
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "A");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "C");
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAssignmentofBenifit"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "N");
                                                }
                                                oSegment.set_DataElementValue(9, 0, "Y");                                                
                                                oSegment.set_DataElementValue(18, 0, "Y");//UB04

                                                if (oTransaction.DelayReasonCodeID != "")
                                                {
                                                    oSegment.set_DataElementValue(20, 0, oTransaction.DelayReasonCodeID);
                                                }


                                                #region "Discharge Hour"

                                                if ((oUBTransaction.DischargeHour).ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "096");
                                                    oSegment.set_DataElementValue(2, 0, "TM");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString((oUBTransaction.DischargeHour).ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Statement Dates"

                                                if (oUBTransaction.MaxDOS.ToString() != "" && oUBTransaction.MinDOS.ToString() != "")
                                                {
                                                    string StatementDate = gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MaxDOS.ToShortDateString());
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "434");
                                                    oSegment.set_DataElementValue(2, 0, "RD8");
                                                    oSegment.set_DataElementValue(3, 0, StatementDate);//Claim Date
                                                }

                                                #endregion                                           

                                                #region "Admission Date"
                                                if ((oUBTransaction.MinDOS).ToShortDateString() != "" && oUBTransaction.MinDOS != DateTime.MaxValue)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "DT");
                                                    if (Convert.ToString(oUBTransaction.AdmissionHour).Trim() != "")
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())) + oUBTransaction.AdmissionHour.Trim());
                                                    else
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())));
                                                }

                                                #endregion

                                                #region "Admission Type Code"

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CL1"));

                                                if (Convert.ToString(oUBTransaction.AdmissionTypeCode) != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, oUBTransaction.AdmissionTypeCode);
                                                }

                                                if (Convert.ToString(oUBTransaction.AdmissionSource) != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, oUBTransaction.AdmissionSource);
                                                }

                                                if (Convert.ToString(oUBTransaction.DischargeStatus) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, oUBTransaction.DischargeStatus);
                                                }

                                                #endregion

                                                #region Patient Paid Amount.
                                               
                                                string _AmountPaid = String.Empty;
                                                if (dtPatientPaid != null && dtPatientPaid.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]).Trim() != "")
                                                        _AmountPaid = FormatAmount(Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]));
                                                }

                                                if (_AmountPaid.Trim() != string.Empty && _AmountPaid.Trim() != "0.00" && _AmountPaid.Trim() != "0.0" && _AmountPaid.Trim() != "0")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\AMT"));
                                                    oSegment.set_DataElementValue(1, 0, "F5");
                                                    oSegment.set_DataElementValue(2, 0, _AmountPaid);
                                                }

                                                #endregion

                                                #endregion

                                                #region Claim Remittance Reference #                                               

                                                if ((Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"])).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && _ClaimStatus == "7")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "F8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                }

                                                #endregion

                                                #region Service Authorization exception code

                                                if (oTransaction.ServiceAuthExceCode.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "4N");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.ServiceAuthExceCode);
                                                }

                                                #endregion
                                             
                                                #region "Prior Authorization Number"

                                          
                                                if (FormatString(oTransaction.PriorAuthorizationNo) != "")
                                                {
                                                    //REF CLEARING HOUSE CLAIM NUMBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.PriorAuthorizationNo)); 
                                                }                                             

                                                #endregion


                                                #region "BOX19 Note"

                                                if (FormatString(oTransaction.Box19NoteDescription) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NTE"));
                                                    oSegment.set_DataElementValue(1, 0, "ADD");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.Box19NoteDescription)); //Claim No
                                                }

                                                #endregion

                                                #region HI - Diagnosis

                                                //HI HEALTH CARE DIAGNOSIS CODES                                                                                                                                                                                         
                                                bool IsOtherDignosisAdded = false;
                                                int DxOtherIndex = 0;

                                                string code_no = "";
                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {
                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                oSegment.set_DataElementValue(1, 1, "BK");
                                                                oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));                                                                
                                                                oSegment.set_DataElementValue(2, 1, "BJ");
                                                                oSegment.set_DataElementValue(2, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {

                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (IsOtherDignosisAdded == false)
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                }
                                                                DxOtherIndex++;
                                                                IsOtherDignosisAdded = true;
                                                                oSegment.set_DataElementValue(DxOtherIndex, 1, "BF");
                                                                oSegment.set_DataElementValue(DxOtherIndex, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                    }

                                                    if (code_no != "" && _IsUndo != true)
                                                    {
                                                        code_no = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                                                    }
                                                    else
                                                    {
                                                        code_no = "";
                                                    }
                                                }

                                                if (code_no != "")
                                                {
                                                    string _message;

                                                    _message = "ICD9 is Invalid." + Environment.NewLine + "For Claim No :" + _ClaimNo + Environment.NewLine + "Code : " + code_no + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "                                                            

                                                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                    {
                                                        return "";
                                                    }
                                                }
                                                #endregion

                                                #region Attending Provider(Same as Billing Provider)
                                               
                                                //2010AA BILLING PROVIDER
                                                //NM1 BILLING PROVIDER NAME
                                                if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "71");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["EntityType"])));
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                    }
                                                   
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                    }
                                                    
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                    }
                                               

                                                    //REF 
                                                    string _OtherQualifier = "";
                                                    string _OtherQualifierDesc = "";
                                                    bool _IsOtherDefaultID = false;
                                                    _OtherQualifierDesc = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierDescription"]));
                                                    _IsOtherDefaultID = Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]);
                                                    if ((_OtherQualifierDesc.Contains("SSN") || _OtherQualifierDesc.Contains("Social Security number")) && _IsOtherDefaultID == true)
                                                    {                                                      
                                                        _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDISSNQUALIFIER"]);                                                      
                                                    }
                                                    else if (_OtherQualifierDesc.Contains("Employer's Identification Number") && _IsOtherDefaultID == true)
                                                    {                                                                                                               
                                                        _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDIEMPIDQUALIFIER"]);                                                      
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                        if (_OtherQualifier != "")
                                                        {
                                                            oSegment.set_DataElementValue(1, 0, _OtherQualifier);//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                        }
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));

                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return "";
                                                }

                                                #endregion
                                                                                                                   
                                                #region Facility - 2310D

                                                //2310E SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION

                                                if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bIsPOS"]) == true)
                                                {
                                                    if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                    {                                                        
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "FA");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["LastName"])));//"FacilityName"

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"]));//NPI code
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])));//NPI
                                                        }

                                                        //N3 SERVICE FACILITY ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["Address1"])));//"FacilityAddr"

                                                        //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["City"])));//"FacilityCity"
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["State"])));//"FacilityState"                                                        
                                                        oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])), FormatString(Convert.ToString(dtFacility.Rows[0]["AreaCode"]))));//"FacilityZip"

                                                        //Facility Secondary Identification
                                                        if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bISOtherID"]) == true)
                                                        {
                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]));//NPI code
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])));//NPI
                                                            }
                                                        }

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["City"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["State"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])) == "")
                                                        {
                                                            MessageBox.Show("For ClaimNo:" + _ClaimNo + " Facility Details(City/State/ZIP Code) is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }
                                                    }
                                                }
                                                #endregion

                                                #region Subscriber

                                                for (int _Insrow = 1; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320

                                                    //LOOP - 2320

                                                    if (_Insrow < 3)
                                                    {
                                                        #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                        //1.Payer Resposibilty Sequence No.

                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Primary")
                                                        {
                                                            _PayerResponsibilityName = "Primary";
                                                            oSegment.set_DataElementValue(1, 0, "P");//S- Secondary
                                                        }
                                                        else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Secondary")
                                                        {
                                                            _PayerResponsibilityName = "Secondary";
                                                            oSegment.set_DataElementValue(1, 0, "S");//S- Secondary
                                                        }
                                                        else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Tertiary")
                                                        {
                                                            _PayerResponsibilityName = "Tertiary";
                                                            oSegment.set_DataElementValue(1, 0, "T");//T - Tertiary
                                                        }

                                                        //2.Individual Relationship code
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                        //3.Refrence identification
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"22145");///Policy no

                                                        //4. Plan Name
                                                        if (Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIncludePlanname"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                        }

                                                        //9.Claim Filing Indicator
                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                        }

                                                        #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        #region DMG  - Demographic

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                                oSegment.set_DataElementValue(1, 0, "D8");

                                                                oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"
                                                                if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]).ToUpper().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "OTHER")
                                                                {
                                                                    oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + " subscriber gender is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + " subscriber date of birth is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }
                                                        #endregion DMG  - Demographic

                                                        #region OI - Other Insurance

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                        //Assignment of Benefit.
                                                        bool _bAssignmentofbenefit = false;
                                                        _bAssignmentofbenefit = Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bAssignmentofBenifit"]);
                                                        if (_bAssignmentofbenefit == true)
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "Y");
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "Y"); //UB04
                                                        }                                                     

                                                        if (oTransaction.SOF == true)
                                                        {
                                                            oSegment.set_DataElementValue(6, 0, "Y");
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(6, 0, "Y"); //UB04
                                                        }


                                                        #endregion OI - Other Insurance

                                                        //2330A SUBSCRIBER
                                                        #region NM1 SUBSCRIBER NAME - 2330A

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "IL");
                                                        oSegment.set_DataElementValue(2, 0, "1");

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                        }
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                        }

                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                        {
                                                            MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + "  Subscriber Last name is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }
                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "MI");
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("For Claim No: " + _ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }

                                                        //N3 SUBSCRIBER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"
                                                        }

                                                        //N4 SUBSCRIBER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                        #endregion NM1 SUBSCRIBER NAME

                                                        #region Payer Information - 2330B

                                                        //2330B SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"                                                                                                      

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                        }

                                                        if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"] != null)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "FY");
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()));
                                                        }

                                                        #endregion Payer Information
                                                    }

                                                    #endregion Subscriber Secondary Insurance
                                                }
                                               
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                   // oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV2 Institutional SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV2"));

                                                    //Revenue
                                                    if (oTransLine.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oTransLine.RevenueCode.ToString().Trim().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else if (oUBTransaction.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oUBTransaction.RevenueCode.ToString().Trim().Replace(".", ""));//"Admin revenue code"
                                                    }

                                                    //CPT
                                                    oSegment.set_DataElementValue(2, 1, "HC");
                                                    if (oTransLine.CPTCode.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString());
                                                    }

                                                    if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    if (oTransLine.Mod3Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 5, oTransLine.Mod3Code.ToString());//Modifier 3
                                                    }
                                                    if (oTransLine.Mod4Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 6, oTransLine.Mod4Code.ToString());//Modifier 4
                                                    }
                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(4, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(5, 0, FormatUnit(oTransLine.Unit.ToString()));//Unit/Quantity

                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    if (oTransLine.DateServiceTill != null)
                                                    {
                                                        if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) == Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString()))
                                                            || Convert.ToString(oTransLine.DateServiceTill) == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"                                                             
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "RD8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) + "-" + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                        }
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"
                                                    }

                                                    #endregion                                                                                                    

                                                #endregion
                                                 
                                                    #region " NDC Code Loop - 2410 "

                                                    if (oTransLine.NDCCode != null && oTransLine.NDCCode.Trim() != "")
                                                    {
                                                        //Start - Loop 2410 NDC Code implementation
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN"));
                                                        oSegment.set_DataElementValue(2, 0, oTransLine.NDCCodeQualifier.Trim()); //LIN - Qualifier
                                                        oSegment.set_DataElementValue(3, 0, oTransLine.NDCCode.Trim());//LIN - NDC Code 11 digit
                                                    }
                                                    if (oTransLine.NDCUnit != null && oTransLine.NDCUnitCode != null && oTransLine.NDCUnit.Trim() != "" && oTransLine.NDCUnitCode.Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\CTP"));
                                                        if (oTransLine.NDCUnitPricing == null || oTransLine.NDCUnitPricing.Trim() == "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "0.00"); //Unit Price
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, oTransLine.NDCUnitPricing); //Unit Price
                                                        }
                                                        oSegment.set_DataElementValue(4, 0, oTransLine.NDCUnit); //Quantity
                                                        oSegment.set_DataElementValue(5, 1, oTransLine.NDCUnitCode); //Unit or Basis of Measurement
                                                        //End - Loop 2410 NDC Code implementation
                                                    }


                                                    #endregion " NDC Code Loop - 2410 "

                                                }



                                            }//If loop for Patient Insurance
                                            #endregion
                                            //Transaction Line Loop
                                        }//Transaction SETS Loop
                                    }
                                }
                            }

                            #region " Save EDI File "

                            sPath = "";                          
                            sPath = gloSettings.FolderSettings.AppTempFolderPath + "837 EDI\\";
                            if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }

                            sEdiFile = GetEDIFileName(sPath, _BatchName);

                            oEdiDoc.Save(sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                            oWriter.Write(strData);
                            oWriter.Close();
                            _result = sEdiFile;

                            #endregion " Save EDI File "

                            #region " Update Claim Manager Table "
                            Int64 _date = 0;
                            Int64 _time = 0;
                            _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                            _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                            gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                            //Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                            ogloClaimManager.SetClaimManagerTVP(_nBatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID, odsEDIClaimDetail);
                            ogloClaimManager.Dispose();
                            #endregion

                            //DESTROYS OBJECTS
                            oSegment.Dispose();
                            oTransactionset.Dispose();
                            oGroup.Dispose();
                            oInterchange.Dispose();

                        }
                    }
                  }
                }//SEF File present IF loop
                catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
                {
                    string _strEx = "";
                    ediException oException = null;
                    oException = (ediException)Rex.WrappedException;
                    _strEx = oException.get_Description();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                    _result = "";
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    _result = "";
                }
                finally
                {
                  

                    if (oEdiDoc != null) { oEdiDoc.Dispose(); }
                    if (oInterchange != null) { oInterchange.Dispose(); }
                    if (oGroup != null) { oGroup.Dispose(); }
                    if (oTransactionset != null) { oTransactionset.Dispose(); }
                    if (oSegment != null) { oSegment.Dispose(); }
                    if (oSchema != null) { oSchema.Dispose(); }
                    if (oSchemas != null) { oSchemas.Dispose(); }

                }
                #endregion " Generate EDI "

           
            return _result;
        }

        public string EDI837GenerationForUB_5010(ArrayList SelectedTransactions, ArrayList SelectedMasterTransactions, string _BatchName, bool _IsUndo, Int64 _ContactID, dsEDIClaimdetails odsEDIClaimDetail, Int64 _nBatchID)
        {
            DataSet dsMaster = null;
            DataSet dsHeader = null;


            string _result = "";
            int IndexCount;
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string _ClaimStatus = "";
            string sEdiFile, sPath;
            ediDocument oEdiDoc = null;
            ediSchema oSchema = null;
            ediSchemas oSchemas = null;
            ediInterchange oInterchange = null;
            ediGroup oGroup = null;
            ediTransactionSet oTransactionset = null;
            ediDataSegment oSegment = null;
            string sSEFFile = "";
            bool _IsSEFPresent = true;
            string _TypeOfData = "T";
            bool _bIsCaptionize = false;

            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            string sReceiverQualifier = "ZZ";
            string sSenderQualifier = "ZZ";
            DataTable dtClearingHouseID = null;
            bool _bInclude_NDC_Desc_2400loop_UB = false;
            bool _IsIncludePrimaryDxInBox69 = false;
            #region " Generate UB EDI "

            string sInstance = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            gloUB04 ogloUB04 = new gloUB04();//ub
            TransactionEDI oTransaction = null;
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
             ogloSettings.GetSetting("bInclude_NDC_Desc_2400loop_UB", out value);
            if (value != null && Convert.ToString(value) != "")
            {
                _bInclude_NDC_Desc_2400loop_UB = Convert.ToBoolean(value);
                value = null;
            }
            if (ogloSettings != null)
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }
            _IsIncludePrimaryDxInBox69 = getBox69settings(_ContactID);
            try
            {
                #region "Load EDI"


                // sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "837_005010X223A2.SemRef.SEF";

                oEdiDoc = new ediDocument();

                // Change the cursor type from dynamic to forward to improve speed performance
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                // Disable the internal standard reference library to be memory efficient
                oSchemas = oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // Load the SEF file
                oSchema = oEdiDoc.ImportSchema(sSEFPath + sSEFFile, 0);


                if (File.Exists(sSEFPath + sSEFFile) == false)
                {
                    MessageBox.Show("837 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _IsSEFPresent = false;
                    return "";
                }


                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";

                #endregion

                //Get Clearing House Information in Data table
                if (_IsSEFPresent == true)
                {

                    #region "Header Data - Dataset define in table"

                    dsHeader = GetHeader_EDI_UB_5010(_ContactID, _ClinicID, Convert.ToInt64(SelectedTransactions[SelectedTransactions.Count - 1]));
                     
                    if (dsHeader == null)
                    {
                        return "";
                    }
                    if (dsHeader.Tables == null)
                    {
                        return "";
                    }

                    DataTable dtClearingHouse = dsHeader.Tables["ClearingHouseData"];
                    DataTable dtSubmitter = dsHeader.Tables["SubmitterData"];
                    DataTable dtbIsCaptionize = dsHeader.Tables["bIsCaptionize"];
                    if (dtbIsCaptionize != null && dtbIsCaptionize.Rows.Count > 0)
                    {
                        _bIsCaptionize = Convert.ToBoolean(dtbIsCaptionize.Rows[0]["sSettingsValue"]);
                    }
                    #endregion

                    //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    dtClearingHouseID = ogloBilling.GetClearingHouseSettings();

                    if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                    {
                        MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }

                    if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                    {
                        MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }

                    //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    if (dtClearingHouseID != null && dtClearingHouseID.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtClearingHouseID.Rows.Count; i++)
                        {
                            if (dtClearingHouseID.Rows[i]["bIsDefault"].ToString() == "True")
                            {
                                if (Convert.ToString(dtClearingHouseID.Rows[i]["sSenderIDQualifier"]) != "")
                                { sSenderQualifier = Convert.ToString(dtClearingHouseID.Rows[i]["sSenderIDQualifier"]); }

                                if (Convert.ToString(dtClearingHouseID.Rows[i]["sReceiverIDQualifier"]) != "")
                                { sReceiverQualifier = Convert.ToString(dtClearingHouseID.Rows[i]["sReceiverIDQualifier"]); }
                            }
                        }
                    }

                    #region " Interchange Segment "
                    //Create the interchange segment
                    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "005010"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                    if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                    {
                        _TypeOfData = "T";
                    }
                    else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                    {
                        _TypeOfData = "P";
                    }

                    oSegment.set_DataElementValue(1, 0, "00");
                    oSegment.set_DataElementValue(3, 0, "00");
                    oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SenderID.Trim());//"1234545");//
                    oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_ReceiverID.Trim().Replace("*",""));//"V2EL");//
                    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    oSegment.set_DataElementValue(11, 0, "^");
                    oSegment.set_DataElementValue(12, 0, "00501");
                    InterchangeHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                    oSegment.set_DataElementValue(14, 0, "0");
                    oSegment.set_DataElementValue(15, 0, _TypeOfData);
                    oSegment.set_DataElementValue(16, 0, ":");

                    #endregion " Interchange Segment "

                    #region " Functional Group "

                    //Create the functional group segment
                    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("005010X223A2"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                    oSegment.set_DataElementValue(1, 0, "HC");
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));////_SenderName);
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//// _ReceiverCode.Trim());//"ClarEDI");
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    FunctionalGroupHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                    oSegment.set_DataElementValue(7, 0, "X");
                    oSegment.set_DataElementValue(8, 0, "005010X223A2");

                    #endregion " Functional Group "

                    #region ST - TRANSACTION SET HEADER

                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                    TransactionSetHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"
                    oSegment.set_DataElementValue(3, 0, "005010X223A2");

                    #endregion ST - TRANSACTION SET HEADER

                    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    //Beginning  Segment 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                    oSegment.set_DataElementValue(1, 0, "0019"); //Hierarchical Structure Code
                    oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                    oSegment.set_DataElementValue(3, 0, TransactionSetHeader);//"1234"); //Reference identification
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Date of claim
                    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //"1230");
                    oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION
                 

                    #region NM1 - SUBMITTER


                    //1000A SUBMITTER
                    //NM1 SUBMITTER

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim().Replace("*",""));//clinic code or Electronic Transmitter Identification No.
                    }

                    //PER SUBMITTER EDI CONTACT INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//txtSubmitterContactName.Text.Trim().Replace("*",""));//Contact person name of clinic
                    }
                    else
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    }

                    oSegment.set_DataElementValue(3, 0, "TE");
                    if (dtSubmitter != null && Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""));//txtSubmitterPhone.Text.Trim().Replace("*","").Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                    }

                    #endregion NM1 - SUBMITTER

                    #region NM1 - RECEIVER NAME

                    //1000B RECEIVER
                    //NM1 RECEIVER NAME
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "40");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sClearingHouseCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"GatewayEDI");//clearing house or contractor or carrier or FI name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]) != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.
                    }

                    #endregion NM1 - RECEIVER NAME

                    nHlCount = 0;

                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {
                            for (int i = 0; i < SelectedTransactions.Count; i++)
                            {
                                _ClaimStatus = "";
                                oTransaction = null;
                                TransactionLineEDI oTransLine = null;
                                UB04Transaction oUBTransaction = ogloUB04.GetUBClaim(Convert.ToInt64(SelectedMasterTransactions[i]), Convert.ToInt64(SelectedTransactions[i]));
                                if (oUBTransaction != null)
                                {
                                    oTransaction = oUBTransaction.Transaction;
                                }
                                #region "Master EDI data - Dataset data set in data table "

                                dsMaster = null;
                                dsMaster = GetMaster_EDI_UB_5010(oTransaction.ContactID, oTransaction.ProviderID, oTransaction.ResponsibilityNo,
                                    oTransaction.TransactionMasterID, Convert.ToInt64(oTransaction.FacilityCode), _ClinicID, oTransaction.IsSameAsBillingProvider,
                                    oTransaction.TransactionID, false, oTransaction.ReferalProviderID_New, oTransaction.Lines[0].RenderingProviderId);

                                DataTable dtPatientInsurances = dsMaster.Tables["PatientInsurance"];
                                DataTable dtFacility = dsMaster.Tables["Facility"];
                                DataTable dtBillingProvider = dsMaster.Tables["BillingProvider"];
                                DataTable dtPatientPaid = dsMaster.Tables["PatientPaid"];
                                DataTable dtDx = dsMaster.Tables["Diagnosis"];
                                DataTable dtMasterSetting = dsMaster.Tables["MasterSetting"];
                                DataTable dtAttendingProvider = dsMaster.Tables["AttendingProvider"];
                                DataTable dtRefProvider = dsMaster.Tables["RefferingProvider"];
                                DataTable dtRendProvider = dsMaster.Tables["RenderingProvider"];
                                DataTable dtBillingProviderTaxonomy = dsMaster.Tables["BillingProviderTaxonomy"];
                                DataTable dtPWKData = dsMaster.Tables["PWKData"];
                                DataTable dtRenderringProviderAsAttendingTaxonomy = dsMaster.Tables["RenderringProviderAsAttendingTaxonomy"];
                                DataTable dtBillingProviderAsAttendingTaxonomy = dsMaster.Tables["BillingProviderAsAttendingTaxonomy"];

                                #endregion


                                if (oUBTransaction != null && oTransaction != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    {

                                        string _ClaimNo = "";

                                        #region "Formatting the Claim Number"

                                        _ClaimNo = FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNumber));

                                        #endregion

                                        if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                        {
                                            MessageBox.Show("Patient " + oTransaction.PatientFirstName + " " + oTransaction.PatientLastName + " Insurance details are missing for claim number " + _ClaimNo + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return "";
                                        }

                                        for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                        {
                                            //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                            nHlCount = nHlCount + 1;
                                            nHlProvParent = nHlCount;
                                            //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                            //HL-BILLING PROVIDER
                                            string _PayerResponsibilityName = "";
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                            oSegment.set_DataElementValue(3, 0, "20");
                                            oSegment.set_DataElementValue(4, 0, "1");

                                            #region Billing Provider

                                            string PrimaryBillingProviderID = "";

                                            //2010AA BILLING PROVIDER
                                            //NM1 BILLING PROVIDER NAME
                                            if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                            {
                                                if (dtBillingProviderTaxonomy != null && dtBillingProviderTaxonomy.Rows.Count > 0)
                                                {
                                                    if (FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != ""
                                                        && FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyQualifier"].ToString().Trim()) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PRV"));
                                                        oSegment.set_DataElementValue(1, 0, "BI");
                                                        oSegment.set_DataElementValue(2, 0, "PXC");
                                                        oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                    }
                                                }

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "85");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                }
                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                //}
                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                //}

                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                //}
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                    PrimaryBillingProviderID = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"]));
                                                }

                                                if (Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyState"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"]).Trim() == "")
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state

                                                    //oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", "").Replace("~","").Replace(":","").Replace("-",""));//Provider ZIP
                                                    oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP
                                                    if (Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim() != "")
                                                    {
                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["IncludeExtendedZip"])==true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, string.Concat(Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim(), Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]).Trim()));
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"])));//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"])));////Provider City
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyState"])));//Provider state                                               
                                                    oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]))));//Provider ZIP
                                                    if (Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim() != "")
                                                    {
                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["IncludeExtendedZip"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, String.Concat(Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim(),Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]).Trim()));
                                                        }
                                                        else
                                                        { oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim()); }
                                                    }
                                                }


                                                //Billing Provider 2010AA Provider Tax Identification
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));

                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                {
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "0B" || FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "1G")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                    }
                                                }

                                                #region "Billing Contact Information"

                                                //Contact Information for 5010
                                                if (FormatString(dtBillingProvider.Rows[0]["PhoneNo"].ToString().Trim()) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\PER"));
                                                    oSegment.set_DataElementValue(1, 0, "IC");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(dtBillingProvider.Rows[0]["ContactName"].ToString().Trim()));

                                                    oSegment.set_DataElementValue(3, 0, "TE");
                                                    oSegment.set_DataElementValue(4, 0, FormatString(dtBillingProvider.Rows[0]["PhoneNo"].ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Billing Provider Pay to Address "
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"]).Trim()) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"]).Trim()) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyState"]).Trim()) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"]).Trim()) != "")
                                                {

                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])) != "")
                                                    {
                                                        //In 5010 Billing Provider Pay to address 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "87");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["EntityType"])));

                                                        //N3 BILLING PROVIDER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address

                                                        //N4 BILLING PROVIDER LOCATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state

                                                        //oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", "").Replace("~","").Replace(":","").Replace("-",""));//Provider ZIP
                                                        oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP

                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim() != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim());
                                                        }
                                                    }
                                                }

                                                #endregion
                                             
                                            }
                                            else
                                            {
                                                MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }


                                            #endregion

                                            //'******************************************************************************************************
                                            //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                            //'******************************************************************************************************
                                            #region Subscriber
                                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                            {
                                                string _strRelation = "";
                                                string _strInsuranceType = "";
                                                _strRelation = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                _strInsuranceType = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");


                                                #region Subscriber HL Loop - 2000B

                                                nHlCount = nHlCount + 1;
                                                nHlSubscriberParent = nHlCount;

                                                //2000B SUBSCRIBER HL LOOP
                                                //HL-SUBSCRIBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(3, 0, "22");

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "0");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "1");

                                                }

                                                //SBR SUBSCRIBER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                                                                                            
                                                oSegment.set_DataElementValue(1, 0, "P");
                                                                                           
                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "18");
                                                }

                                                if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]))); //Commercial Insurance company
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludePlanname"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                }

                                                //This is Claim filling Indicator code in EDI implementation guide.
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }
                                                #endregion Subscriber HL Loop - 2000B

                                                //2010BA SUBSCRIBER
                                                //NM1 SUBSCRIBER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                if (_strRelation != "18" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsCompnay"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "2");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sCompanyName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberOrgName"                                                   
                                                }                                                
                                                else
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                    }

                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, "MI");
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Insurance Id"
                                                    }
                                                }
                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeSubscriberAddress"]) == true || _strRelation == "18")
                                                {
                                                    //N3 SUBSCRIBER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"
                                                    }

                                                    //N4 SUBSCRIBER CITY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCountryCode"]).Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCountryCode"]).Trim());
                                                    }

                                                

                                                    string _SubscriberGender = "";
                                                    //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION   

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");

                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"

                                                        _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                        if (_SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                        {
                                                            _SubscriberGender = "U";
                                                        }
                                                        oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 1).ToUpper());//"SubscriberGender"
                                                    }

                                                }
                                                #region Payer Information Loop 2010BB
                                                //2010BC SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 34);

                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                }

                                                string str = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim();
                                                ////////N3 PAYER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceAddress"                                                

                                                ////////N4 PAYER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"InsuranceZip"
                                                #endregion

                                                if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sClaimOfficeNumber"] != null)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                    if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"] != null)
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"]).Trim()));
                                                    }
                                                    else
                                                        oSegment.set_DataElementValue(1, 0, "FY");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()));
                                                }

                                                if (dtBillingProvider != null)
                                                {
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                    {
                                                        if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "0B" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "1G")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                        }
                                                    }
                                                }

                                                if (_strRelation != "18")
                                                {
                                                    nHlCount = nHlCount + 1;
                                                    //2000B DEPENDENT HL LOOP
                                                    //HL-DEPENDENT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                    oSegment.set_DataElementValue(3, 0, "23");
                                                    oSegment.set_DataElementValue(4, 0, "0");

                                                    //PAT - PATIENT/DEPENDENT INFORMATION

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //01 - Spouse 19 - Child

                                                    #region " Patient Info"

                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "QC");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientLastName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Last Name
                                                    oSegment.set_DataElementValue(4, 0, oTransaction.PatientFirstName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient First Name

                                                    if (oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Middle Name
                                                    }

                                                    //N3 - ADDRESS INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientAddress1.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Address"

                                                    //N4 - GEOGRAPHIC LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientCity.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"City"
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.PatientState.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"State"
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientZip.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"Zip"
                                                    if (oTransaction.PatientCountry.Trim().ToUpper() != "US")
                                                    {
                                                        gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
                                                        oSegment.set_DataElementValue(4, 0, oContact.getCountryCode(oTransaction.PatientCountry.Trim().ToUpper()));
                                                        oContact.Dispose();
                                                    }

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.PatientDOB.ToShortDateString())));
                                                    if (oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "" || oTransaction.PatientGender.Trim().Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"                                                                   
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberGender"
                                                    }

                                                    #endregion " Patient Info"

                                                }
                                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    //oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;
                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                    //oTransLine.Dispose();//UB04
                                                }

                                                _ClaimTotal = _claimAmount.ToString("#0.00");
                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }

                                                #region Claim Details - Loop 2300
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));

                                                if (gloGlobal.gloPMGlobal.IsUseClaimPrefix && gloGlobal.gloPMGlobal.sClaimPrefix != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, String.Concat(gloGlobal.gloPMGlobal.sClaimPrefix, _ClaimNo)); //Patient Account no                                                         
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(1, 0, _ClaimNo); //Patient Account no         
                                                }               
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(5, 1, Convert.ToString(oUBTransaction.Facilitytypecode).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //21 - Inpatient Hospital

                                                if (oTransaction.IsReplacementClaim == true && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsClaimFrequencyOne"]) == false)
                                                {
                                                    _ClaimStatus = "7";
                                                }
                                                else if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsClaimFrequencyOne"]) == true)
                                                {
                                                    _ClaimStatus = "1";
                                                }
                                                else if (Convert.ToString(oUBTransaction.Frequencytypecode).Trim() != "")
                                                {
                                                    _ClaimStatus = Convert.ToString(oUBTransaction.Frequencytypecode);
                                                }
                                                oSegment.set_DataElementValue(5, 2, "A");//UB HardCoded Facility Code Qualifier
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus); //Question......
                                               // oSegment.set_DataElementValue(6, 0, "Y");

                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    _IsAccessAssignment = Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAccessAssignment"]);
                                                }

                                                if (_IsAccessAssignment == true)
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "A");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "C");
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAssignmentofBenifit"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "N");
                                                }
                                                //Signature on file.
                                                if (oTransaction.SOF == true)
                                                {
                                                    oSegment.set_DataElementValue(9, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(9, 0, "I");
                                                }
                                               // oSegment.set_DataElementValue(18, 0, "Y");//UB04

                                                if (oTransaction.DelayReasonCodeID != "")
                                                {
                                                    oSegment.set_DataElementValue(20, 0, oTransaction.DelayReasonCodeID);
                                                }


                                                #region "Discharge Hour"

                                                if ((oUBTransaction.DischargeHour).ToString().Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04DischargeHour"]) == true)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "096");
                                                    oSegment.set_DataElementValue(2, 0, "TM"); 
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString((oUBTransaction.DischargeHour).ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Statement Dates"

                                                if (oUBTransaction.MaxDOS.ToString() != "" && oUBTransaction.MinDOS.ToString() != "")
                                                {
                                                    string StatementDate = gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MaxDOS.ToShortDateString());
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "434");
                                                    oSegment.set_DataElementValue(2, 0, "RD8");
                                                    oSegment.set_DataElementValue(3, 0, StatementDate);//Claim Date
                                                }

                                                #endregion

                                                #region "Admission Date"
                                                if ((oUBTransaction.MinDOS).ToShortDateString() != "" && oUBTransaction.MinDOS != DateTime.MaxValue)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    if(Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04AdmissionHour"]) == true)
                                                        oSegment.set_DataElementValue(2, 0, "DT");
                                                    else
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                    if (Convert.ToString(oUBTransaction.AdmissionHour).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04AdmissionHour"]) == true)
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())) + oUBTransaction.AdmissionHour.Trim());
                                                    else
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())));
                                                }

                                                #endregion

                                                #region "Admission Type Code"

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CL1"));

                                                if (Convert.ToString(oUBTransaction.AdmissionTypeCode) != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, oUBTransaction.AdmissionTypeCode);
                                                }

                                                if (Convert.ToString(oUBTransaction.AdmissionSource) != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, oUBTransaction.AdmissionSource);
                                                }

                                                if (Convert.ToString(oUBTransaction.DischargeStatus) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, oUBTransaction.DischargeStatus);
                                                }

                                                #endregion
                                              
                                                #region PWk Data
                                                if (dtPWKData != null && dtPWKData.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtPWKData.Rows[0]["ReportTypeCode"]).Trim() != "" && Convert.ToString(dtPWKData.Rows[0]["ReportTransmissionCode"]).Trim() != "" && Convert.ToString(dtPWKData.Rows[0]["AttachmentControlNumber"]).Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\PWK"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPWKData.Rows[0]["ReportTypeCode"]).Trim());
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPWKData.Rows[0]["ReportTransmissionCode"]).Trim());
                                                        oSegment.set_DataElementValue(5, 0, "AC");
                                                        oSegment.set_DataElementValue(6, 0, Convert.ToString(dtPWKData.Rows[0]["AttachmentControlNumber"]).Trim());
                                                    }
                                                }
                                                #endregion

                                                //Commented
                                                #region Patient Paid Amount.

                                                //string _AmountPaid = String.Empty;
                                                //if (dtPatientPaid != null && dtPatientPaid.Rows.Count > 0)
                                                //{
                                                //    if (Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]).Trim() != "")
                                                //        _AmountPaid = FormatAmount(Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]));
                                                //}

                                                //if (_AmountPaid.Trim() != string.Empty && _AmountPaid.Trim() != "0.00" && _AmountPaid.Trim() != "0.0" && _AmountPaid.Trim() != "0")
                                                //{
                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\AMT"));
                                                //    oSegment.set_DataElementValue(1, 0, "F5");
                                                //    oSegment.set_DataElementValue(2, 0, _AmountPaid);
                                                //}

                                                #endregion

                                                #endregion

                                                #region Claim Remittance Reference #

                                                if ((Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"])).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "F8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                }

                                                #endregion

                                                #region Service Authorization exception code

                                                if (oTransaction.ServiceAuthExceCode.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "4N");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.ServiceAuthExceCode);
                                                }

                                                #endregion

                                                #region "Prior Authorization Number"


                                                if (FormatString(oTransaction.PriorAuthorizationNo) != "")
                                                {
                                                    //REF CLEARING HOUSE CLAIM NUMBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.PriorAuthorizationNo));
                                                }

                                                #endregion

                                                #region Clinical Trial Number

                                                if (oTransaction.sIDENo.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "P4");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.sIDENo);
                                                }

                                                #endregion 

                                                #region "BOX19 Note"

                                                if (FormatString(oTransaction.Box19NoteDescription) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NTE"));
                                                    oSegment.set_DataElementValue(1, 0, "ADD");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.Box19NoteDescription)); //Claim No
                                                }

                                                #endregion

                                                #region HI - Diagnosis

                                                //HI HEALTH CARE DIAGNOSIS CODES                                                                                                                                                                                         
                                                bool IsOtherDignosisAdded = false;
                                                int DxOtherIndex = 0;

                                                string code_no = "";
                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {
                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "BK");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "ABK");
                                                                }
                                                                oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                                if (Convert.ToBoolean(_IsIncludePrimaryDxInBox69) == true)
                                                                {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                    if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 1, "BJ");
                                                                    }
                                                                    else
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 1, "ABJ");
                                                                    }
                                                                    oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                                }
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {

                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (IsOtherDignosisAdded == false)
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                }
                                                                DxOtherIndex++;
                                                                IsOtherDignosisAdded = true;
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "BF");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "ABF");
                                                                }
                                                                oSegment.set_DataElementValue(DxOtherIndex, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                    }

                                                    if (code_no != "" && _IsUndo != true)
                                                    {
                                                        code_no = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                                                    }
                                                    else
                                                    {
                                                        code_no = "";
                                                    }
                                                }

                                                if (code_no != "")
                                                {
                                                    string _message;

                                                    _message = "ICD9 is Invalid." + Environment.NewLine + "For Claim No :" + _ClaimNo + Environment.NewLine + "Code : " + code_no + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "                                                            

                                                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                    {
                                                        return "";
                                                    }
                                                }
 
                                                #endregion

                                                #region "Condition code,Occurrence span code,Occurrence code, Value Code"

                                                //Occurrence Span Code information
                                                if (oUBTransaction != null && oUBTransaction.dtOccurencespancodes != null && oUBTransaction.dtOccurencespancodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtOccurencespancodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["Codes"])) != ""
                                                            && FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"])) != "")
                                                        {

                                                            oSegment.set_DataElementValue(cnt+1, 1, "BI");
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["Codes"])));
                                                            oSegment.set_DataElementValue(cnt + 1, 3, "RD8");
                                                            if (FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["ToDate"])) != "")
                                                            {
                                                                oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]) + "-" + Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["ToDate"]));
                                                            }
                                                            else
                                                            {
                                                                oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]) + "-" + Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]));
                                                            }
                                                        }
                                                    }
                                                }
                                                //Occurrence Code
                                                if (oUBTransaction != null && oUBTransaction.dtOccurencecodes != null && oUBTransaction.dtOccurencecodes.Rows.Count > 0)
                                                {

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtOccurencecodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BH");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["Codes"])));
                                                        }
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["OccurrenceDate"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 3, "D8");
                                                            oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["OccurrenceDate"].ToString()).ToString());
                                                        }
                                                    }
                                                }

                                                //Value Code
                                                if (oUBTransaction != null && oUBTransaction.dtValuecodes != null && oUBTransaction.dtValuecodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtValuecodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BE");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Codes"])));
                                                        }
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Amount"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 5, FormatAmount(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Amount"])));
                                                        }

                                                    }
                                                }

                                                //Condition Code
                                                if (oUBTransaction != null && oUBTransaction.dtConditioncodes != null && oUBTransaction.dtConditioncodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtConditioncodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BG");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtConditioncodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtConditioncodes.Rows[cnt]["Codes"])));
                                                        }

                                                    }
                                                }

                                                #endregion


                                                #region Attending Provider(Same as Billing Provider)

                                                //2010AA BILLING PROVIDER
                                                //NM1 BILLING PROVIDER NAME
                                                string PrimaryAttendingProviderID = "";
                                                string sUBBox77Rendering = "";
                                                string sUBBox77Billing = "";
                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    sUBBox77Rendering = Convert.ToString(dtPatientInsurances.Rows[0]["sUBBox77Rendering"]);
                                                    sUBBox77Billing = Convert.ToString(dtPatientInsurances.Rows[0]["sUBBox77Billing"]);

                                                }

                                                #region Attending provider Or Operation provider setting wise

                                                if (sUBBox77Billing.Trim().ToUpper() == "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        PrimaryAttendingProviderID = FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"]));
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }

                                                        if (dtBillingProviderAsAttendingTaxonomy != null && dtBillingProviderAsAttendingTaxonomy.Rows.Count > 0 && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            if (FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "AT");
                                                                oSegment.set_DataElementValue(2, 0, "PXC");
                                                                oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                            }
                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"]));
                                                        }

                                                        if (FormatString(Convert.ToString(dtRenderringProviderAsAttendingTaxonomy.Rows[0]["Taxonomy"])) != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "AT");
                                                            oSegment.set_DataElementValue(2, 0, "PXC");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtRenderringProviderAsAttendingTaxonomy.Rows[0]["Taxonomy"])));
                                                           
                                                        }

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }
                                                }
                                                if (sUBBox77Billing.Trim().ToUpper() == "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                }

                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }

                                                }

                                                #endregion


                                                #region both operation and attending provider

                                                if (sUBBox77Billing.Trim().ToUpper() == "Both Operating and Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    #region Attendging provider As billing Provider

                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"]));
                                                        }

                                                        if (dtBillingProviderAsAttendingTaxonomy != null && dtBillingProviderAsAttendingTaxonomy.Rows.Count > 0 && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            if (FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != "")     
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "AT");
                                                                oSegment.set_DataElementValue(2, 0, "PXC");
                                                                oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                            }
                                                        }


                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }

                                                    #endregion

                                                    #region Operatin provider AS billing Provider

                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                    #endregion
                                                }

                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Both Operating and Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    #region Attendging provider As Rendering provider

                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"]));
                                                        }

                                                        if (FormatString(Convert.ToString(dtRenderringProviderAsAttendingTaxonomy.Rows[0]["Taxonomy"])) != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "AT");
                                                            oSegment.set_DataElementValue(2, 0, "PXC");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtRenderringProviderAsAttendingTaxonomy.Rows[0]["Taxonomy"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }

                                                    #endregion

                                                    #region Operating provider As Rendering provider
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                //else
                                                //{
                                                //    MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    return "";
                                                //}

                                                #endregion


                                                #region Referring Provider - 2310A

                                                if (oTransaction.ReferalProviderID_New > 0 || oTransaction.IsSameAsBillingProvider == true)
                                                {

                                                    if (dtRefProvider != null && dtRefProvider.Rows.Count > 0)
                                                    {
                                                        //2310B Referring PROVIDER
                                                        //NM1 Referring PROVIDER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "DN");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(dtRefProvider.Rows[0]["sLastName"].ToString())); //"ReferringLastname"
                                                        }
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(dtRefProvider.Rows[0]["sFirstName"].ToString()));//"ReferringFirstname"
                                                        }
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(dtRefProvider.Rows[0]["sMiddleName"].ToString()));
                                                        }
                                                        if (FormatString(dtRefProvider.Rows[0]["sNPI"].ToString()) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(dtRefProvider.Rows[0]["sNPI"].ToString()));//"NPI"
                                                        }

                                                        //PRV REFERRING PROVIDER INFORMATION

                                                        //if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sTaxonomy"])) != "")
                                                        //{
                                                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //    oSegment.set_DataElementValue(1, 0, "RF");
                                                        //    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        //    oSegment.set_DataElementValue(3, 0, FormatString(dtRefProvider.Rows[0]["sTaxonomy"].ToString()));//Reference Identification
                                                        //}

                                                        // REF
                                                        if (Convert.ToString(dtRefProvider.Rows[0]["Code"]).Trim() != "" && Convert.ToString(dtRefProvider.Rows[0]["Value"]).Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, Convert.ToString(dtRefProvider.Rows[0]["Code"]).Trim());
                                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtRefProvider.Rows[0]["Value"]).Trim());
                                                        }


                                                    }
                                                }


                                                #endregion Referring Provider

                                                #region Rendering Provider - 2310B

                                                //2310B RENDERING PROVIDER
                                                //NM1 RENDERING PROVIDER NAME
                                                if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                {
                                                    bool IsincludeRenderingProvider = false;
                                                    IsincludeRenderingProvider = Convert.ToBoolean(dtRendProvider.Rows[0]["bIncludeRenderingProvider"]);
                                                    if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != PrimaryAttendingProviderID || (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) == PrimaryAttendingProviderID && IsincludeRenderingProvider == true))
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "82");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                        ////PRV RENDERING PROVIDER INFORMATION
                                                        //if (Convert.ToString(dtRendProvider.Rows[0]["Taxonomy"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        //{
                                                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //    oSegment.set_DataElementValue(1, 0, "PE");
                                                        //    oSegment.set_DataElementValue(2, 0, "PXC");//Mutually Defined
                                                        //    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["Taxonomy"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Reference Identification
                                                        //}
                                                        //_Rendering = 2;
                                                    }
                                                }



                                                    #endregion

                                                #region Facility - 2310D

                                                //2310E SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION

                                                if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bIsPOS"]) == true)
                                                {
                                                    if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                    {
                                                        bool IsincludeFacility = false;
                                                        IsincludeFacility = Convert.ToBoolean(dtFacility.Rows[0]["bIncludeFacility"]);

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != PrimaryBillingProviderID || (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) == PrimaryBillingProviderID && IsincludeFacility == true))
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "77");
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["LastName"])));//"FacilityName"

                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != "")
                                                            {
                                                                oSegment.set_DataElementValue(8, 0, Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"]));//NPI code
                                                                oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])));//NPI
                                                            }

                                                            //N3 SERVICE FACILITY ADDRESS
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["Address1"])));//"FacilityAddr"

                                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["City"])));//"FacilityCity"
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["State"])));//"FacilityState"                                                        
                                                            oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])), FormatString(Convert.ToString(dtFacility.Rows[0]["AreaCode"]))));//"FacilityZip"                                                        
                                                            if (Convert.ToString(dtFacility.Rows[0]["CountryCode"]).Trim() != "")
                                                            {
                                                                oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["CountryCode"])));
                                                            }

                                                            //Facility Secondary Identification
                                                            if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bISOtherID"]) == true)
                                                            {
                                                                if (FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])) != "")
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]));//NPI code
                                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])));//NPI
                                                                }
                                                            }

                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["City"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["State"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])) == "")
                                                            {
                                                                MessageBox.Show("For ClaimNo:" + _ClaimNo + " Facility Details(City/State/ZIP Code) is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return "";
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion

                                                #region Subscriber

                                                for (int _Insrow = 1; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320

                                                    //LOOP - 2320
                                                    _strRelation = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    if (_Insrow < 3)
                                                    {
                                                        #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                        //1.Payer Responsibility Sequence No.

                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Primary")
                                                        {
                                                            _PayerResponsibilityName = "Primary";
                                                            oSegment.set_DataElementValue(1, 0, "P");//S- Secondary
                                                        }
                                                        else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Secondary")
                                                        {
                                                            _PayerResponsibilityName = "Secondary";
                                                            oSegment.set_DataElementValue(1, 0, "S");//S- Secondary
                                                        }
                                                        else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Tertiary")
                                                        {
                                                            _PayerResponsibilityName = "Tertiary";
                                                            oSegment.set_DataElementValue(1, 0, "T");//T - Tertiary
                                                        }

                                                        //2.Individual Relationship code
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                        //3.Refrence identification
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"22145");///Policy no

                                                        //4. Plan Name
                                                        if (Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIncludePlanname"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                        }

                                                        //9.Claim Filing Indicator
                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                        }

                                                        #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information
                                                    

                                                        #region OI - Other Insurance

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                        //Assignment of Benefit.
                                                        bool _bAssignmentofbenefit = false;
                                                        _bAssignmentofbenefit = Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bAssignmentofBenifit"]);
                                                        if (_bAssignmentofbenefit == true)
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "Y");
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "N"); //UB04
                                                        }

                                                        if (oTransaction.SOF == true)
                                                        {
                                                            oSegment.set_DataElementValue(6, 0, "Y");
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(6, 0, "I"); //UB04
                                                        }


                                                        #endregion OI - Other Insurance

                                                        //2330A SUBSCRIBER
                                                        #region NM1 SUBSCRIBER NAME - 2330A

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "IL");                                                       
                                                         if (_strRelation != "18" && Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIsCompnay"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sCompanyName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberOrgName"                                                           
                                                            if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"

                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("For Claim No: " + oTransaction.ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return "";
                                                            }

                                                        }                                                        
                                                         else
                                                         {
                                                             oSegment.set_DataElementValue(2, 0, "1");
                                                             if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                             {
                                                                 oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                             }
                                                             oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                             if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                             {
                                                                 oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                             }

                                                             if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                             {
                                                                 MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + "  Subscriber Last name is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                 return "";
                                                             }
                                                             if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                             {
                                                                 oSegment.set_DataElementValue(8, 0, "MI");
                                                                 oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"
                                                             }
                                                             else
                                                             {
                                                                 MessageBox.Show("For Claim No: " + _ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                 return "";
                                                             }
                                                         }
                                                        //N3 SUBSCRIBER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"
                                                        }

                                                        //N4 SUBSCRIBER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                        #endregion NM1 SUBSCRIBER NAME

                                                        #region Payer Information - 2330B

                                                        //2330B SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"                                                                                                      

                                                        if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                        }

                                                        if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"] != null)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                            if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"] != null)
                                                            {
                                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"]).Trim()));
                                                            }
                                                            else
                                                                oSegment.set_DataElementValue(1, 0, "FY");
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()));
                                                        }

                                                        #endregion Payer Information


                                                        #region "Set Table Index as per Master SP"

                                                        IndexCount = _Insrow + 12;

                                                        #endregion

                                                        //2330I Billing Provider                                                        
                                                        //NM1 BILLING PROVIDER NAME
                                                        if (dsMaster.Tables[IndexCount] != null && dsMaster.Tables[IndexCount].Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                if (FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "0B" && FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "1G")
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                    oSegment.set_DataElementValue(1, 0, "85");
                                                                    oSegment.set_DataElementValue(2, 0, "2");

                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])));
                                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifierValue"])));
                                                                }
                                                            }
                                                        }
                                                    }

                                                    #endregion Subscriber Secondary Insurance
                                                }

                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                   // oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV2 Institutional SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV2"));

                                                    //Revenue
                                                    if (oTransLine.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oTransLine.RevenueCode.ToString().Trim().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else if (oUBTransaction.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oUBTransaction.RevenueCode.ToString().Trim().Replace(".", ""));//"Admin revenue code"
                                                    }

                                                    //CPT
                                                    oSegment.set_DataElementValue(2, 1, "HC");
                                                    //Bug #51048: 00000399 : Claim set up
                                                    //Description: Replacement CPT not shown in SV2 segment if CPTCrosswalk is asociated to patient.
                                                    //So commented the code and add condition.
                                                    //if (oTransLine.CPTCode.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    //{
                                                    //    oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString());
                                                    //}
                                                    //Check the Crosswalk
                                                    if (oTransLine.CPTCode.ToString().Trim() == oTransLine.CrosswalkCPTCode.ToString().Trim() || oTransLine.CrosswalkCPTCode.ToString().Trim() == "" || oTransLine.CrosswalkCPTCode == null)
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CrosswalkCPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    }

                                                    if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    if (oTransLine.Mod3Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 5, oTransLine.Mod3Code.ToString());//Modifier 3
                                                    }
                                                    if (oTransLine.Mod4Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 6, oTransLine.Mod4Code.ToString());//Modifier 4
                                                    }
                                                    if (Convert.ToBoolean(_bInclude_NDC_Desc_2400loop_UB) == true)
                                                    {
                                                        if (FormatString(Convert.ToString(oTransLine.PrescriptionDesc)) != "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 7, FormatString(Convert.ToString(oTransLine.PrescriptionDesc).Replace("\r\n", " ").Replace("*", "").Replace("~", "").Replace(":", "")));
                                                        }
                                                    }

                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(4, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(5, 0, FormatUnit(oTransLine.Unit.ToString()));//Unit/Quantity

                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    if (oTransLine.DateServiceTill != null)
                                                    {
                                                        if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) == Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString()))
                                                            || Convert.ToString(oTransLine.DateServiceTill) == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"                                                             
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "RD8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) + "-" + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                        }
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"
                                                    }

                                                    #endregion

                                                #endregion

                                                #region " LINE ITEM CONTROL NUMBER "

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "6R"); //Provider Control Number
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.Lines[nLine].TransactionDetailID.ToString()); //Line Item Control Number

                                                 #endregion

                                                #region " NDC Code Loop - 2410 "

                                                    if (oTransLine.NDCCode != null && oTransLine.NDCCode.Trim() != "")
                                                    {
                                                        //Start - Loop 2410 NDC Code implementation
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN"));
                                                        oSegment.set_DataElementValue(2, 0, oTransLine.NDCCodeQualifier.Trim()); //LIN - Qualifier
                                                        oSegment.set_DataElementValue(3, 0, oTransLine.NDCCode.Trim());//LIN - NDC Code 11 digit
                                                    }
                                                    //if (oTransLine.NDCUnit != null && oTransLine.NDCUnitCode != null && oTransLine.NDCUnit.Trim() != "" && oTransLine.NDCUnitCode.Trim() != "")
                                                    //{
                                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\CTP"));
                                                    //    if (oTransLine.NDCUnitPricing == null || oTransLine.NDCUnitPricing.Trim() == "")
                                                    //    {
                                                    //        oSegment.set_DataElementValue(3, 0, "0.00"); //Unit Price
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        oSegment.set_DataElementValue(3, 0, oTransLine.NDCUnitPricing); //Unit Price
                                                    //    }
                                                    //    oSegment.set_DataElementValue(4, 0, oTransLine.NDCUnit); //Quantity
                                                    //    oSegment.set_DataElementValue(5, 1, oTransLine.NDCUnitCode); //Unit or Basis of Measurement
                                                    //    //End - Loop 2410 NDC Code implementation
                                                    //}
                                                    if (oTransLine.NDCUnit != null && oTransLine.NDCUnitCode != null && oTransLine.NDCUnit.Trim() != "" && oTransLine.NDCUnitCode.Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\CTP"));
                                                        oSegment.set_DataElementValue(4, 0, oTransLine.NDCUnit); //Quantity
                                                        oSegment.set_DataElementValue(5, 1, oTransLine.NDCUnitCode); //Unit or Basis of Measurement                                                       
                                                    }

                                                    //Prescription number
                                                    if (FormatString(oTransLine.Prescription) != null && FormatString(oTransLine.Prescription.Trim()) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "XZ");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(oTransLine.Prescription.Trim()));
                                                    }


                                                    #endregion " NDC Code Loop - 2410 "

                                                }



                                            }//If loop for Patient Insurance
                                            #endregion
                                            //Transaction Line Loop
                                        }//Transaction SETS Loop
                                    }
                                }
                            }

                            #region " Save EDI File "

                            sPath = "";
                            sPath = gloSettings.FolderSettings.AppTempFolderPath + "837 EDI\\";
                            if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }

                            sEdiFile = GetEDIFileName(sPath, _BatchName);

                            oEdiDoc.Save(sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                            if (_bIsCaptionize)
                            {
                                oWriter.Write(strData.ToUpper());
                            }
                            else
                            {
                                oWriter.Write(strData);
                            }
                            oWriter.Close();
                            _result = sEdiFile;

                            #endregion " Save EDI File "

                            #region " Update Claim Manager Table "
                            Int64 _date = 0;
                            Int64 _time = 0;
                            _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                            _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                            gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                            //Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                            ogloClaimManager.SetClaimManagerTVP(_nBatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID, odsEDIClaimDetail);
                            ogloClaimManager.Dispose();
                            #endregion

                            //DESTROYS OBJECTS
                            oSegment.Dispose();
                            oTransactionset.Dispose();
                            oGroup.Dispose();
                            oInterchange.Dispose();

                        }
                    }
                }
            }//SEF File present IF loop
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                _result = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = "";
            }
            finally
            {


                if (oEdiDoc != null) { oEdiDoc.Dispose(); }
                if (oInterchange != null) { oInterchange.Dispose(); }
                if (oGroup != null) { oGroup.Dispose(); }
                if (oTransactionset != null) { oTransactionset.Dispose(); }
                if (oSegment != null) { oSegment.Dispose(); }
                if (oSchema != null) { oSchema.Dispose(); }
                if (oSchemas != null) { oSchemas.Dispose(); }
                //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                if (dtClearingHouseID != null) { dtClearingHouseID.Dispose(); }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            #endregion " Generate EDI "


            return _result;
        }

        public string EDI837GenerationForUB_Secondary(ArrayList SelectedTransactions, ArrayList SelectedMasterTransactions, string _BatchName, bool _IsUndo, Int64 _ContactID, dsEDIClaimdetails odsEDIClaimDetail, Int64 _nBatchID)
        {
            DataSet dsMaster = null;
            DataSet dsHeader = null;  
           
            string _result = "";
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string _ClaimStatus = "";          
            Int64 _PrimaryInsuranceId = 0;
            Int64 _PrimaryContactID = 0;
            string sEdiFile, sPath;
            ediDocument oEdiDoc = null;
            ediSchema oSchema = null;
            ediSchemas oSchemas = null;
            ediInterchange oInterchange = null;
            ediGroup oGroup = null;
            ediTransactionSet oTransactionset = null;
            ediDataSegment oSegment = null;
            string sSEFFile = "";
            bool _IsSEFPresent = true;
            string _TypeOfData = "T";

           
                #region " Generate UB EDI "

                string sInstance = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                gloUB04 ogloUB04 = new gloUB04();//ub
                TransactionEDI oTransaction=new TransactionEDI();           
               
                try
                {
                    #region "Load EDI"

                   // sPath = AppDomain.CurrentDomain.BaseDirectory;
                    sSEFFile = "837_X096A1.SEF";

                    oEdiDoc = new ediDocument();

                    // Change the cursor type from dynamic to forward to improve speed performance
                    oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                    // Disable the internal standard reference library to be memory effecient
                    oSchemas = oEdiDoc.GetSchemas();
                    oSchemas.EnableStandardReference = false;

                    // Load the SEF file
                    oSchema = oEdiDoc.ImportSchema(sSEFPath + sSEFFile, 0);


                    if (File.Exists(sSEFPath + sSEFFile) == false)
                    {
                        MessageBox.Show("837 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _IsSEFPresent = false;
                        return "";
                    }


                    oEdiDoc.SegmentTerminator = "~\r\n";
                    oEdiDoc.ElementTerminator = "*";
                    oEdiDoc.CompositeTerminator = ":";

                    #endregion

                    //Get Clearing House Information in Data table
                if (_IsSEFPresent == true)
                {

                    #region "Header Data - Dataset define in table"

                    dsHeader = GetHeader_EDI_UB_4010(_ContactID, _ClinicID, Convert.ToInt64(SelectedTransactions[SelectedTransactions.Count - 1]));
                    if (dsHeader == null)
                    {
                        return "";
                    }
                    if (dsHeader.Tables == null)
                    {
                        return "";
                    }

                    DataTable dtClearingHouse = dsHeader.Tables["ClearingHouseData"];
                    DataTable dtSubmitter = dsHeader.Tables["SubmitterData"];

                    #endregion              
                  
                    if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                    {
                        MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }


                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {                         
                            if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                            {
                                MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return "";
                            }
                        }
                    }
                    
                    #region " Interchange Segment "
                    //Create the interchange segment
                    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                    if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                    {
                        _TypeOfData = "T";
                    }
                    else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                    {
                        _TypeOfData = "P";
                    }

                    oSegment.set_DataElementValue(1, 0, "00");
                    oSegment.set_DataElementValue(3, 0, "00");
                    oSegment.set_DataElementValue(5, 0, "ZZ");
                    oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SenderID.Trim());//"1234545");//
                    oSegment.set_DataElementValue(7, 0, "ZZ");
                    oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_ReceiverID.Trim().Replace("*",""));//"V2EL");//
                    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    oSegment.set_DataElementValue(11, 0, "U");
                    oSegment.set_DataElementValue(12, 0, "00401");
                    InterchangeHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                    oSegment.set_DataElementValue(14, 0, "0");
                    oSegment.set_DataElementValue(15, 0, _TypeOfData);
                    oSegment.set_DataElementValue(16, 0, ":");

                    #endregion " Interchange Segment "

                    #region " Functional Group "

                    //Create the functional group segment
                    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X096A1"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                    oSegment.set_DataElementValue(1, 0, "HC");
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));////_SenderName);
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//// _ReceiverCode.Trim());//"ClarEDI");
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    FunctionalGroupHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                    oSegment.set_DataElementValue(7, 0, "X");
                    oSegment.set_DataElementValue(8, 0, "004010X096A1");

                    #endregion " Functional Group "

                    #region ST - TRANSACTION SET HEADER

                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                    TransactionSetHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"

                    #endregion ST - TRANSACTION SET HEADER

                    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    //Begining Segment 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                    oSegment.set_DataElementValue(1, 0, "0019"); //Herarchical Structure Code
                    oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                    oSegment.set_DataElementValue(3, 0, TransactionSetHeader);//"1234"); //Reference identification
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Date of claim
                    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //"1230");
                    oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    #region REF - TRANSMISSION TYPE IDENTIFICATION

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                    oSegment.set_DataElementValue(1, 0, "87");
                    oSegment.set_DataElementValue(2, 0, "004010X096A1");//"ReferenceID"

                    #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                    #region NM1 - SUBMITTER


                    //1000A SUBMITTER
                    //NM1 SUBMITTER

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim().Replace("*",""));//clinic code or Electronic Transmitter Identification No.
                    }

                    //PER SUBMITTER EDI CONTACT INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//txtSubmitterContactName.Text.Trim().Replace("*",""));//Contact person name of clinic
                    }
                    else
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    }

                    oSegment.set_DataElementValue(3, 0, "TE");
                    if (dtSubmitter != null && Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""));//txtSubmitterPhone.Text.Trim().Replace("*","").Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                    }

                    #endregion NM1 - SUBMITTER

                    #region NM1 - RECEIVER NAME

                    //1000B RECEIVER
                    //NM1 RECEIVER NAME
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "40");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sClearingHouseCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"GatewayEDI");//clearing house or contractor or carrier or FI name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]) != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.
                    }

                    #endregion NM1 - RECEIVER NAME

                    nHlCount = 0;

                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {
                            for (int i = 0; i < SelectedTransactions.Count; i++)
                            {
                                _ClaimStatus = "";
                                oTransaction =null ;
                                TransactionLineEDI oTransLine = null;                                
                                UB04Transaction oUBTransaction = ogloUB04.GetUBClaim(Convert.ToInt64(SelectedMasterTransactions[i]), Convert.ToInt64(SelectedTransactions[i]));
                                if (oUBTransaction != null)
                                {
                                    oTransaction = oUBTransaction.Transaction;
                                }

                                #region "Master EDI data - Dataset data set in data table "

                                dsMaster = null;
                                dsMaster = GetMaster_EDI_UB_4010(oTransaction.ContactID, oTransaction.ProviderID, oTransaction.ResponsibilityNo,
                                    oTransaction.TransactionMasterID, Convert.ToInt64(oTransaction.FacilityCode), _ClinicID, oTransaction.IsSameAsBillingProvider,
                                    oTransaction.TransactionID, true );

                                DataTable dtPatientInsurances = dsMaster.Tables["PatientInsurance"];
                                DataTable dtFacility = dsMaster.Tables["Facility"];
                                DataTable dtBillingProvider = dsMaster.Tables["BillingProvider"];
                                DataTable dtPatientPaid = dsMaster.Tables["PatientPaid"];
                                DataTable dtDx = dsMaster.Tables["Diagnosis"];
                                DataTable dtMasterSetting = dsMaster.Tables["MasterSetting"];
                                DataTable _dtPayment = dsMaster.Tables["SVDData"];
                                DataTable dtAllcasdata = dsMaster.Tables["CASData"];

                                #endregion

                                if (oUBTransaction != null && oTransaction != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    {

                                        string _ClaimNo = "";                                       

                                        #region "Formatting the Claim Number"

                                        _ClaimNo = FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNumber));

                                        #endregion
                                                                                                                                                                                               
                                        if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                        {
                                            MessageBox.Show("Patient " + oTransaction.PatientFirstName + " " + oTransaction.PatientLastName + " Insurance details are missing for claim number " + _ClaimNo + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return "";
                                        }
                                                                                                                       
                                        for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                        {
                                            //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                            nHlCount = nHlCount + 1;
                                            nHlProvParent = nHlCount;
                                            //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                            //HL-BILLING PROVIDER
                                            string _PayerResponsibilityName = "";
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                            oSegment.set_DataElementValue(3, 0, "20");
                                            oSegment.set_DataElementValue(4, 0, "1");
                                            

                                            #region Billing Provider
                                           
                                                                                                                       
                                            //2010AA BILLING PROVIDER
                                            //NM1 BILLING PROVIDER NAME
                                            if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                            {
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "85");
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtBillingProvider.Rows[0]["EntityType"]));
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                }
                                             
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                }
                                              
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                }

                                                //N3 BILLING PROVIDER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address
                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address2"])));//Provider Address

                                                //N4 BILLING PROVIDER LOCATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state

                                                //oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", "").Replace("~","").Replace(":","").Replace("-",""));//Provider ZIP
                                                oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP


                                                //REF 
                                                string _OtherQualifier = "";
                                                string _OtherQualifierDesc = "";
                                                bool _IsOtherDefaultID = false;
                                                _OtherQualifierDesc = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierDescription"]));
                                                _IsOtherDefaultID = Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]);
                                                if ((_OtherQualifierDesc.Contains("SSN") || _OtherQualifierDesc.Contains("Social Security number")) && _IsOtherDefaultID == true)
                                                {                                                    
                                                    _OtherQualifier =Convert.ToString(dtMasterSetting.Rows[0]["EDISSNQUALIFIER"]);                                                     
                                                }
                                                else if (_OtherQualifierDesc.Contains("Employer's Identification Number") && _IsOtherDefaultID == true)
                                                {                                                
                                                    _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDIEMPIDQUALIFIER"]);                                                     
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                    if (_OtherQualifier != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, _OtherQualifier);//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                    }
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));

                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }
                                           

                                            #endregion

                                            //'******************************************************************************************************
                                            //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                            //'******************************************************************************************************
                                            #region Subscriber
                                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                            {
                                                string _strRelation = "";
                                                string _strInsuranceType = "";
                                                _strRelation = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                _strInsuranceType = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (_strInsuranceType == "MB")
                                                {
                                                    if (_strRelation != "18")
                                                    {
                                                        //_strRelation = "18"; 
                                                    }
                                                }

                                                #region Subscriber HL Loop - 2000B

                                                nHlCount = nHlCount + 1;
                                                nHlSubscriberParent = nHlCount;

                                                //2000B SUBSCRIBER HL LOOP
                                                //HL-SUBSCRIBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(3, 0, "22");

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "0");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "1");

                                                }

                                                //SBR SUBSCRIBER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));

                                                #region "Responsibility No"

                                                if (oTransaction.ResponsibilityNo == 1)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "P");//_SubscriberInsurancePST);//"P");
                                                }
                                                else if (oTransaction.ResponsibilityNo == 2)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "S");//_SubscriberInsurancePST);//"P");
                                                }
                                                else if (oTransaction.ResponsibilityNo == 3)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "T");//_SubscriberInsurancePST);//"P");
                                                }

                                                #endregion

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "18");//20091222                                                    
                                                }

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludePlanname"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                }


                                                //This is Claim filling Indicator code in EDI implementation guide.
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                //2010BA SUBSCRIBER
                                                //NM1 SUBSCRIBER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                }
                                                
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "MI");
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Insurance Id"
                                                }

                                                //N3 SUBSCRIBER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"
                                                }

                                                //N4 SUBSCRIBER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                #endregion SubscriberHL Loop - 2000B
                                                
                                                string _SubscriberGender = "";
                                                //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION   

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");

                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"

                                                    _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    if (_SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        _SubscriberGender = "U";
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 1).ToUpper());//"SubscriberGender"
                                                }

                                                #region Payer Information Loop 2010BB
                                                //2010BC SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 34);

                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                }

                                                string str = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim();
                                                ////////N3 PAYER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceAddress"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"

                                                ////////N4 PAYER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"InsuranceZip"
                                                #endregion

                                                if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sClaimOfficeNumber"] != null)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "FY");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()));
                                                }

                                                if (_strRelation != "18")
                                                {
                                                    nHlCount = nHlCount + 1;
                                                    //2000B DEPENDENT HL LOOP
                                                    //HL-DEPENDENT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                    oSegment.set_DataElementValue(3, 0, "23");
                                                    oSegment.set_DataElementValue(4, 0, "0");

                                                    //PAT - PATIENT/DEPENDENT INFORMATION

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //01 - Spouse 19 - Child

                                                    #region " Patient Info"

                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "QC");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientLastName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Last Name
                                                    oSegment.set_DataElementValue(4, 0, oTransaction.PatientFirstName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient First Name

                                                    if (oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Middle Name
                                                    }
                                                    //N3 - ADDRESS INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientAddress1.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Address"

                                                    //N4 - GEOGRAPHIC LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientCity.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"City"
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.PatientState.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"State"
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientZip.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"Zip"

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.PatientDOB.ToShortDateString())));
                                                    if (oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "" || oTransaction.PatientGender.Trim().Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"                                                                   
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberGender"
                                                    }

                                                    #endregion " Patient Info"

                                                }
                                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    //oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;
                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                    //oTransLine.Dispose();//UB04
                                                }

                                                _ClaimTotal = _claimAmount.ToString("#0.00");
                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }

                                                #region Claim Details - Loop 2300
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                oSegment.set_DataElementValue(1, 0, _ClaimNo); //Patient Account no         
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount
                                                oSegment.set_DataElementValue(5, 1, oUBTransaction.Facilitytypecode.Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //21 - Inpatient Hospital
                                                
                                                if (oTransaction.IsRebill == true || oTransaction.IsReplacementClaim == true)
                                                {
                                                    _ClaimStatus = "7";
                                                }
                                                else if (oUBTransaction.Frequencytypecode.Trim() != "")
                                                {
                                                    _ClaimStatus = oUBTransaction.Frequencytypecode.Trim();
                                                }
                                                oSegment.set_DataElementValue(5, 2, "A");//UB HardCoded Facility Code Qualifier
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus); //Question......
                                                oSegment.set_DataElementValue(6, 0, "Y");

                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    _IsAccessAssignment = Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAccessAssignment"]);
                                                }


                                                if (_IsAccessAssignment == true)
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "A");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "C");
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAssignmentofBenifit"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "N");
                                                }
                                                oSegment.set_DataElementValue(9, 0, "Y");                                         
                                                oSegment.set_DataElementValue(18, 0, "Y");//UB04
                                                if (oTransaction.DelayReasonCodeID != "")
                                                {
                                                    oSegment.set_DataElementValue(20, 0, oTransaction.DelayReasonCodeID);
                                                }

                                                #region "Discharge Hour"

                                                if ((oUBTransaction.DischargeHour).ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "096");
                                                    oSegment.set_DataElementValue(2, 0, "TM");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString((oUBTransaction.DischargeHour).ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Statement Dates"

                                                if (oUBTransaction.MaxDOS.ToString() != "" && oUBTransaction.MinDOS.ToString() != "")
                                                {
                                                    string StatementDate = gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MaxDOS.ToShortDateString());
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "434");
                                                    oSegment.set_DataElementValue(2, 0, "RD8");
                                                    oSegment.set_DataElementValue(3, 0, StatementDate);//Claim Date
                                                }

                                                #endregion
                                              
                                                #region "Admission Date"
                                                if ((oUBTransaction.MinDOS).ToShortDateString() != "" && oUBTransaction.MinDOS != DateTime.MaxValue)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    oSegment.set_DataElementValue(2, 0, "DT");
                                                    if (Convert.ToString(oUBTransaction.AdmissionHour) != "")
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())) + oUBTransaction.AdmissionHour.Trim());
                                                    else
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())));
                                                }

                                                #endregion

                                                #region "Admission Type Code"

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CL1"));

                                                if (Convert.ToString(oUBTransaction.AdmissionTypeCode) != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, oUBTransaction.AdmissionTypeCode);
                                                }

                                                if (Convert.ToString(oUBTransaction.AdmissionSource) != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, oUBTransaction.AdmissionSource);
                                                }

                                                if (Convert.ToString(oUBTransaction.DischargeStatus) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, oUBTransaction.DischargeStatus);
                                                }

                                                #endregion

                                                #region Patient Paid Amount.
                                               
                                                string _AmountPaid = String.Empty;
                                                if (dtPatientPaid != null && dtPatientPaid.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]).Trim() != "")
                                                        _AmountPaid = FormatAmount(Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]));
                                                }

                                                if (_AmountPaid.Trim() != string.Empty && _AmountPaid.Trim() != "0.00" && _AmountPaid.Trim() != "0.0" && _AmountPaid.Trim() != "0")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\AMT"));
                                                    oSegment.set_DataElementValue(1, 0, "F5");
                                                    oSegment.set_DataElementValue(2, 0, _AmountPaid);
                                                }

                                                #endregion

                                                #endregion

                                                #region Claim Remittance Reference #                                              

                                                if ((Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"])).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && _ClaimStatus == "7")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "F8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                }

                                                #endregion

                                                #region Service Authorization exception code

                                                if (oTransaction.ServiceAuthExceCode.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "4N");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.ServiceAuthExceCode);
                                                }

                                                #endregion

                                                #region "Prior Authorization Number"
                                             
                                                if (FormatString(oTransaction.PriorAuthorizationNo)  != "")
                                                {
                                                    //REF CLEARING HOUSE CLAIM NUMBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.PriorAuthorizationNo)); //Claim No
                                                }                                

                                                #endregion

                                                #region "BOX19 Note"

                                                if (FormatString(oTransaction.Box19NoteDescription) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NTE"));
                                                    oSegment.set_DataElementValue(1, 0, "ADD");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.Box19NoteDescription)); //Claim No
                                                }

                                                #endregion


                                                #region HI - Diagnosis

                                                //HI HEALTH CARE DIAGNOSIS CODES                                                                                                                                                                                         
                                                bool IsOtherDignosisAdded = false;
                                                int DxOtherIndex = 0;
                                                string code_no = "";
                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {
                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "BK");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "ABK");
                                                                }
                                                                oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(2, 1, "BJ");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(2, 1, "ABJ");
                                                                }
                                                                oSegment.set_DataElementValue(2, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (IsOtherDignosisAdded == false)
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                }
                                                                DxOtherIndex++;
                                                                IsOtherDignosisAdded = true;
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "BF");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "ABF");
                                                                }
                                                                oSegment.set_DataElementValue(DxOtherIndex, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                    }
                                                    if (code_no != "" && _IsUndo != true)
                                                    {
                                                        code_no = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                                                    }
                                                    else
                                                    {
                                                        code_no = "";
                                                    }
                                                }

                                                if (code_no != "")
                                                {
                                                    string _message;
                                                    _message = "ICD9 is Invalid." + Environment.NewLine + "For Claim No :" + _ClaimNo + Environment.NewLine + "Code : " + code_no + "  " + Environment.NewLine + "Do you want to Continue? ";
                                                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                    {
                                                        return "";
                                                    }
                                                }
                                                #endregion


                                                #region Attending Provider(Same as Billing Provider)
                                                                                               
                                                //2010AA BILLING PROVIDER
                                                //NM1 BILLING PROVIDER NAME
                                                if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "71");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["EntityType"])));
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                    }
                                                   
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                    }
                                                
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                    {
                                                        oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                    }
                                                  
                                                    //REF 
                                                    string _OtherQualifier = "";
                                                    string _OtherQualifierDesc = "";
                                                    bool _IsOtherDefaultID = false;
                                                    _OtherQualifierDesc = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierDescription"]));
                                                    _IsOtherDefaultID = Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]);
                                                    if ((_OtherQualifierDesc.Contains("SSN") || _OtherQualifierDesc.Contains("Social Security number")) && _IsOtherDefaultID == true)
                                                    {                                                  
                                                        _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDISSNQUALIFIER"]);                                                         
                                                    }
                                                    else if (_OtherQualifierDesc.Contains("Employer's Identification Number") && _IsOtherDefaultID == true)
                                                    {                                               
                                                        _OtherQualifier = Convert.ToString(dtMasterSetting.Rows[0]["EDIEMPIDQUALIFIER"]);                                                      
                                                    }
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                        if (_OtherQualifier != "")
                                                        {
                                                            oSegment.set_DataElementValue(1, 0, _OtherQualifier);//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                        }
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));

                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return "";
                                                }


                                                #endregion
                                                                                                                             
                                                #region Facility - 2310D

                                                //2310E SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION

                                                if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bIsPOS"]) == true)
                                                {
                                                    if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                    {
                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "FA");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["LastName"])));//"FacilityName"

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"]));//NPI code
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])));//NPI
                                                        }

                                                        //N3 SERVICE FACILITY ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["Address1"])));//"FacilityAddr"

                                                        //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["City"])));//"FacilityCity"
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["State"])));//"FacilityState"                                                        
                                                        oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])), FormatString(Convert.ToString(dtFacility.Rows[0]["AreaCode"]))));//"FacilityZip"

                                                        //Facility Secondary Identification
                                                        if (getSecondaryIDFacility(oTransaction.ContactID) == true)
                                                        {
                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]));//NPI code
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])));//NPI
                                                            }
                                                        }

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["City"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["State"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])) == "")
                                                        {
                                                            MessageBox.Show("For ClaimNo:" + _ClaimNo + " Facility Details(City/State/ZIP Code) is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }
                                                    }

                                                }
                                                #endregion

                                                #region SET PRIMARY INSURANCE IDS

                                                DataView _dv = dtPatientInsurances.DefaultView;

                                                _dv.RowFilter = "(sInsuranceFlag)='Primary'";

                                                DataTable _dtAllowed = _dv.ToTable();
                                                if (_dtAllowed != null && _dtAllowed.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(_dtAllowed.Rows[0]["nInsuranceID"]) != "")
                                                    {
                                                        _PrimaryInsuranceId = Convert.ToInt64(_dtAllowed.Rows[0]["nInsuranceID"]);
                                                    }
                                                    if (Convert.ToString(_dtAllowed.Rows[0]["nContactID"]) != "")
                                                    {
                                                        _PrimaryContactID = Convert.ToInt64(_dtAllowed.Rows[0]["nContactID"]);
                                                    }

                                                }

                                                #endregion
                                             
                                                #region GET Claim Allowed Amount

                                                string _ClaimAllowedAmount = getFilteredClaimAllowed(oTransaction.TransactionMasterID, _PrimaryInsuranceId, _PrimaryContactID, _dtPayment);
                                                _ClaimAllowedAmount = FormatAmount(_ClaimAllowedAmount);

                                                #endregion


                                                for (int _Insrow = 1; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320
                                                  
                                                    #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                    //1.Payer Responsibility Sequence No.

                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Primary")
                                                    {
                                                        _PayerResponsibilityName = "Primary";
                                                        oSegment.set_DataElementValue(1, 0, "P");//_OtherInsurancePST.Trim().Replace("*","")); //S- Secondary
                                                    }
                                                    else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Secondary")
                                                    {
                                                        _PayerResponsibilityName = "Secondary";
                                                        oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim().Replace("*","")); //S- Secondary
                                                    }
                                                    else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Tertiary")
                                                    {
                                                        _PayerResponsibilityName = "Tertiary";
                                                        oSegment.set_DataElementValue(1, 0, "T");//_OtherInsurancePST.Trim().Replace("*","")); //T - Tertiary
                                                    }

                                                    //2.Individual Relationship code
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                    //3.Refrence identification
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"22145");///Policy no

                                                    //4. Plan Name
                                                    if (Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIncludePlanname"]) == true)
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                    }
                                                   
                                                    //9.Claim Filing Indicator
                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                    }
                                                  
                                                    #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information
                                              

                                                    #region AMT - Amount Payer Paid

                                                    string _PayercontactID = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["nContactID"]);
                                                    string _PayerInsuranceID = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["nInsuranceID"]);

                                                    string _amt = getFilteredPayerPaid(oTransaction.TransactionMasterID, oTransaction.TransactionID, _PayercontactID, _PayerInsuranceID, _dtPayment);
                                                    _amt = FormatAmount(_amt);                                     

                                                    #endregion AMT - Amount

                                                    #region Allowed Amount
                                                
                                                    if (_amt != "" && _ClaimAllowedAmount.Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        oSegment.set_DataElementValue(1, 0, "B6");
                                                        oSegment.set_DataElementValue(2, 0, _ClaimAllowedAmount);
                                                    }

                                                    #endregion

                                                    #region DMG  - Demographic


                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]) != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                            oSegment.set_DataElementValue(1, 0, "D8");

                                                            oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"
                                                            if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]).ToUpper().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "OTHER")
                                                            {
                                                                oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + " subscriber gender is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + " subscriber date of birth is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        return "";
                                                    }
                                                    #endregion DMG  - Demographic

                                                    #region OI - Other Insurance

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                    //Assignment of Benefit.
                                                    bool _bAssignmentofbenefit = false;
                                                    _bAssignmentofbenefit = Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bAssignmentofBenifit"]);
                                                    if (_bAssignmentofbenefit == true)
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "Y");
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "Y"); //UB04
                                                    }                                                    

                                                    if (oTransaction.SOF == true)
                                                    {
                                                        oSegment.set_DataElementValue(6, 0, "Y");
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(6, 0, "Y"); //UB04
                                                    }

                                                    #endregion OI - Other Insurance

                                                    //2330A SUBSCRIBER
                                                    #region NM1 SUBSCRIBER NAME - 2330A

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "IL");
                                                    oSegment.set_DataElementValue(2, 0, "1");

                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                    }
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                    }

                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                    {
                                                        MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + "  Subscriber Last name is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        return "";
                                                    }
                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, "MI");
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("For Claim No: " + _ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        return "";
                                                    }

                                                    //N3 SUBSCRIBER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"
                                                    }

                                                    //N4 SUBSCRIBER CITY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                    #endregion NM1 SUBSCRIBER NAME

                                                    #region Payer Information - 2330B

                                                    //2330B SUBSCRIBER/PAYER
                                                    //NM1 PAYER NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "PR");
                                                    oSegment.set_DataElementValue(2, 0, "2");


                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"                                                                                                      
                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                    }
                                                   
                                                    if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"] != null)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "FY");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()));
                                                    }

                                                    #endregion Payer Information

                                                    //   }

                                                    #endregion Subscriber Secondary Insurance
                                                }//End for loop of Patient Insurance 
                                                //}//end of IF loop for POS
                                              
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                    //oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV2 Institution SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV2"));


                                                    //Revenue
                                                    if (oTransLine.RevenueCode.ToString().Trim() != "")// || oTransLine.CrosswalkCPTCode.ToString().Trim() == "" || oTransLine.CrosswalkCPTCode == null)
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oTransLine.RevenueCode.ToString().Trim().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else if (oUBTransaction.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oUBTransaction.RevenueCode.ToString().Trim().Replace(".", ""));//"Admin revenue code"
                                                    }

                                                    //CPT
                                                    oSegment.set_DataElementValue(2, 1, "HC");
                                                    if (oTransLine.CPTCode.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString());
                                                    }

                                                    if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    if (oTransLine.Mod3Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 5, oTransLine.Mod3Code.ToString());//Modifier 3
                                                    }
                                                    if (oTransLine.Mod4Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 6, oTransLine.Mod4Code.ToString());//Modifier 4
                                                    }
                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(4, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(5, 0, FormatUnit(oTransLine.Unit.ToString()));//Unit/Quantity

                                                 
                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    if (oTransLine.DateServiceTill != null)
                                                    {
                                                        if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) == Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString()))
                                                            || Convert.ToString(oTransLine.DateServiceTill) == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"                                                             
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "RD8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) + "-" + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                        }
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"
                                                    }


                                                    #endregion                                                 
                                                                                                                                                                                                  

                                                    #region " NDC Code Loop - 2410 "

                                                    if (oTransLine.NDCCode != null && oTransLine.NDCCode.Trim() != "")
                                                    {
                                                        //Start - Loop 2410 NDC Code implementation
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN"));
                                                        oSegment.set_DataElementValue(2, 0, oTransLine.NDCCodeQualifier.Trim()); //LIN - Qualifier
                                                        oSegment.set_DataElementValue(3, 0, oTransLine.NDCCode.Trim());//LIN - NDC Code 11 digit
                                                    }
                                                    if (oTransLine.NDCUnit != null && oTransLine.NDCUnitCode != null && oTransLine.NDCUnit.Trim() != "" && oTransLine.NDCUnitCode.Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\CTP"));
                                                        if (oTransLine.NDCUnitPricing == null || oTransLine.NDCUnitPricing == "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "0.00"); //Unit Price
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, oTransLine.NDCUnitPricing); //Unit Price
                                                        }
                                                        oSegment.set_DataElementValue(4, 0, oTransLine.NDCUnit); //Quantity
                                                        oSegment.set_DataElementValue(5, 1, oTransLine.NDCUnitCode); //Unit or Basis of Measurement
                                                        //End - Loop 2410 NDC Code implementation
                                                    }


                                                    #endregion " NDC Code Loop - 2410 "

                                                    //UB04

                                                    #region "SVD -LINE ADJUDICATION INFORMATION"

                                                    DataTable  dtSVDdata = null;
                                                    dtSVDdata = getFilteredSVDLine(oTransaction.TransactionMasterID, oTransaction.Lines[nLine].TransactionMasterDetailID, _dtPayment).Copy();
                                                    if (dtSVDdata != null && dtSVDdata.Rows.Count > 0)
                                                    {
                                                        for (int nSVD = 0; nSVD < dtSVDdata.Rows.Count; nSVD++)
                                                        {
                                                            if (Convert.ToString(dtPatientInsurances.Rows[0]["nInsuranceID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != Convert.ToString(dtSVDdata.Rows[nSVD]["InsuranceID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""))
                                                            {
                                                                if (Convert.ToString(dtSVDdata.Rows[nSVD]["InsPaidAmount"]).Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD"));
                                                                    oSegment.set_DataElementValue(1, 0, dtSVDdata.Rows[nSVD]["PayerID"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Other Payer identification code
                                                                    oSegment.set_DataElementValue(2, 0, FormatAmount(Convert.ToString(dtSVDdata.Rows[nSVD]["InsPaidAmount"])));//Service Line Paid Amount
                                                                    oSegment.set_DataElementValue(3, 1, "HC");//COMPOSITE MEDICAL PROCEDURE IDENTIFIER
                                                                    oSegment.set_DataElementValue(3, 2, dtSVDdata.Rows[nSVD]["CPTCode"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//CPT
                                                                    oSegment.set_DataElementValue(5, 0, FormatAmount(Convert.ToString(dtSVDdata.Rows[nSVD]["Unit"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")));//Quantity

                                                                    #region "CAS -LINE ADJUSTMENT"

                                                                    DataTable dtcasdata = new DataTable();
                                                                    dtcasdata = getFilteredCasData(oTransaction.TransactionMasterID, oTransaction.Lines[nLine].TransactionMasterDetailID, Convert.ToInt64(dtSVDdata.Rows[nSVD]["ContactID"]), Convert.ToInt64(dtSVDdata.Rows[nSVD]["InsuranceID"]), dtAllcasdata);

                                                                    if (dtcasdata != null && dtcasdata.Rows.Count > 0)
                                                                    {
                                                                        string _payerId = "";
                                                                        string _grpCode = "";
                                                                        Int64 _contactId = 0;

                                                                        for (int rIndex = 0; rIndex < dtcasdata.Rows.Count; rIndex++)
                                                                        {
                                                                            _payerId = Convert.ToString(dtcasdata.Rows[rIndex]["PayerID"]);
                                                                            _contactId = Convert.ToInt64(dtcasdata.Rows[rIndex]["ContactID"]);//20100416
                                                                            
                                                                            if (_contactId > 0)
                                                                            {
                                                                                _grpCode = Convert.ToString(dtcasdata.Rows[rIndex]["GroupCode"]);

                                                                                for (int dIndex = rIndex + 1; dIndex < dtcasdata.Rows.Count; dIndex++)
                                                                                {
                                                                                    if (_contactId == Convert.ToInt64(dtcasdata.Rows[dIndex]["ContactID"])
                                                                                    && _grpCode == Convert.ToString(dtcasdata.Rows[dIndex]["GroupCode"]))
                                                                                    {
                                                                                        dtcasdata.Rows[dIndex]["InsuranceID"] = -1;
                                                                                        dtcasdata.Rows[dIndex]["ContactID"] = -1;
                                                                                        dtcasdata.Rows[dIndex]["InsuranceName"] = "";
                                                                                        dtcasdata.Rows[dIndex]["PayerID"] = "";
                                                                                        dtcasdata.Rows[dIndex]["GroupCode"] = "";
                                                                                        dtcasdata.AcceptChanges();
                                                                                    }
                                                                                }
                                                                            }

                                                                        }
                                                                    }

                                                                    #region "Adding CAS"
                                                                   
                                                                    if (dtcasdata != null && dtcasdata.Rows.Count > 0)
                                                                    {
                                                                        for (int CASIndex = 0, ResonCodeIndex = 1; CASIndex < dtcasdata.Rows.Count; CASIndex++)
                                                                        {

                                                                            if (Convert.ToInt64(dtcasdata.Rows[CASIndex]["ContactID"]) != -1 &&
                                                                                Convert.ToString(dtcasdata.Rows[CASIndex]["GroupCode"]) != "")
                                                                            {
                                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD\\CAS"));
                                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["GroupCode"])); //
                                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["ReasonCode"]));

                                                                                #region "Amount Formatting"

                                                                                string _ClmTotal = Convert.ToString(dtcasdata.Rows[CASIndex]["Amount"]).Trim();
                                                                                _ClmTotal = FormatAmount(_ClmTotal);

                                                                                #endregion

                                                                                if (_ClmTotal != "")
                                                                                {
                                                                                    oSegment.set_DataElementValue(3, 0, _ClmTotal);
                                                                                    ResonCodeIndex = 5;
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string _Total = Convert.ToString((dtcasdata.Rows[CASIndex]["Amount"]));
                                                                                _Total = FormatAmount(_Total);

                                                                                if (_Total != "")
                                                                                {
                                                                                    oSegment.set_DataElementValue(ResonCodeIndex, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["ReasonCode"]));
                                                                                    ResonCodeIndex += 1;
                                                                                    oSegment.set_DataElementValue(ResonCodeIndex, 0, _Total);
                                                                                    ResonCodeIndex += 2;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    #endregion

                                                                    #endregion

                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD\\DTP"));
                                                                    oSegment.set_DataElementValue(1, 0, "573");
                                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                                    oSegment.set_DataElementValue(3, 0, dtSVDdata.Rows[nSVD]["ClaimPaidDate"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"ServiceDate"
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            #endregion " Subscriber "
                                            }//If loop for Patient Insurance
                                            //Transaction Line Loop
                                        }//Transaction SETS Loop
                                    }
                                }
                            }

                            #region " Save EDI File "

                            sPath = "";                            
                            sPath = gloSettings.FolderSettings.AppTempFolderPath + "837 EDI\\";
                            if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }
                            sEdiFile = GetEDIFileName(sPath, _BatchName);
                            oEdiDoc.Save(sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();
                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                            oWriter.Write(strData);
                            oWriter.Close();
                            _result = sEdiFile;

                            #endregion " Save EDI File "

                            #region " Update Claim Manager Table "
                            Int64 _date = 0;
                            Int64 _time = 0;
                            _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                            _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                            gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                            //Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                            ogloClaimManager.SetClaimManagerTVP(_nBatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID, odsEDIClaimDetail);
                            ogloClaimManager.Dispose();
                            #endregion
                            
                            oSegment.Dispose();
                            oTransactionset.Dispose();
                            oGroup.Dispose();
                            oInterchange.Dispose();

                        }
                    }
                  }
                }//SEF File present IF loop
                catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
                {
                    string _strEx = "";
                    ediException oException = null;
                    oException = (ediException)Rex.WrappedException;
                    _strEx = oException.get_Description();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                    _result = "";
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    _result = "";
                }
                finally
                {
                    if (dsHeader != null) { dsHeader.Dispose(); }
                    if (dsMaster != null) { dsMaster.Dispose(); }
                   
                    if (oEdiDoc != null) { oEdiDoc.Dispose(); }
                    if (oInterchange != null) { oInterchange.Dispose(); }
                    if (oGroup != null) { oGroup.Dispose(); }
                    if (oTransactionset != null) { oTransactionset.Dispose(); }
                    if (oSegment != null) { oSegment.Dispose(); }
                    if (oSchema != null) { oSchema.Dispose(); }
                    if (oSchemas != null) { oSchemas.Dispose(); }
                    if (ogloUB04 != null)
                    {
                        ogloUB04.Dispose();
                        ogloUB04 = null;
                    }
                }
                #endregion " Generate EDI "

           
            return _result;
        }

        public string EDI837GenerationForUB_Secondary_5010(ArrayList SelectedTransactions, ArrayList SelectedMasterTransactions, string _BatchName, bool _IsUndo, Int64 _ContactID, dsEDIClaimdetails odsEDIClaimDetail, Int64 _nBatchID)
        {
            DataSet dsMaster = null;
            DataSet dsHeader = null;

            string _result = "";
            int IndexCount;
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string _ClaimStatus = "";
            Int64 _PrimaryInsuranceId = 0;
            Int64 _PrimaryContactID = 0;
            string sEdiFile, sPath;
            ediDocument oEdiDoc = null;
            ediSchema oSchema = null;
            ediSchemas oSchemas = null;
            ediInterchange oInterchange = null;
            ediGroup oGroup = null;
            ediTransactionSet oTransactionset = null;
            ediDataSegment oSegment = null;
            string sSEFFile = "";
            bool _IsSEFPresent = true;
            string _TypeOfData = "T";
            bool _bIsCaptionize = false;
            bool _bInclude_NDC_Desc_2400loop_UB = false;
            bool _IsIncludePrimaryDxInBox69 = false;
            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            string sReceiverQualifier = "ZZ";
            string sSenderQualifier = "ZZ";
            DataTable dtClearingHouseID =null;

            #region " Generate UB EDI "

            string sInstance = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            gloUB04 ogloUB04 = new gloUB04();//ub
            TransactionEDI oTransaction = new TransactionEDI();
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            ogloSettings.GetSetting("bInclude_NDC_Desc_2400loop_UB", out value);
            if (value != null && Convert.ToString(value) != "")
            {
                _bInclude_NDC_Desc_2400loop_UB = Convert.ToBoolean(value);
                value = null;
            }
            if (ogloSettings != null)
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }
            _IsIncludePrimaryDxInBox69 = getBox69settings(_ContactID);
            try
            {
                #region "Load EDI"

                // sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "837_005010X223A2.SemRef.SEF";

                oEdiDoc = new ediDocument();

                // Change the cursor type from dynamic to forward to improve speed performance
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                // Disable the internal standard reference library to be memory efficient
                oSchemas = oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // Load the SEF file
                oSchema = oEdiDoc.ImportSchema(sSEFPath + sSEFFile, 0);


                if (File.Exists(sSEFPath + sSEFFile) == false)
                {
                    MessageBox.Show("837 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _IsSEFPresent = false;
                    return "";
                }


                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";

                #endregion

                //Get Clearing House Information in Data table
                if (_IsSEFPresent == true)
                {

                    #region "Header Data - Dataset define in table"

                    dsHeader = GetHeader_EDI_UB_5010(_ContactID, _ClinicID, Convert.ToInt64(SelectedTransactions[SelectedTransactions.Count - 1]));
                    if (dsHeader == null)
                    {
                        return "";
                    }
                    if (dsHeader.Tables == null)
                    {
                        return "";
                    }

                    DataTable dtClearingHouse = dsHeader.Tables["ClearingHouseData"];
                    DataTable dtSubmitter = dsHeader.Tables["SubmitterData"];
                    DataTable dtbIsCaptionize = dsHeader.Tables["bIsCaptionize"];
                    if (dtbIsCaptionize != null && dtbIsCaptionize.Rows.Count > 0)
                    {
                        _bIsCaptionize = Convert.ToBoolean(dtbIsCaptionize.Rows[0]["sSettingsValue"]);
                    }
                    #endregion

                    //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    dtClearingHouseID = ogloBilling.GetClearingHouseSettings();

                    if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                    {
                        MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }


                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {
                            if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                            {
                                MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return "";
                            }
                        }
                    }

                    //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    if (dtClearingHouseID != null && dtClearingHouseID.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtClearingHouseID.Rows.Count; i++)
                        {
                            if (dtClearingHouseID.Rows[i]["bIsDefault"].ToString() == "True")
                            {
                                if (Convert.ToString(dtClearingHouseID.Rows[i]["sSenderIDQualifier"]) != "")
                                { sSenderQualifier = Convert.ToString(dtClearingHouseID.Rows[i]["sSenderIDQualifier"]); }

                                if (Convert.ToString(dtClearingHouseID.Rows[i]["sReceiverIDQualifier"]) != "")
                                { sReceiverQualifier = Convert.ToString(dtClearingHouseID.Rows[i]["sReceiverIDQualifier"]); }
                            }
                        }
                    }

                    #region " Interchange Segment "
                    //Create the interchange segment
                    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "005010"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                    if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                    {
                        _TypeOfData = "T";
                    }
                    else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                    {
                        _TypeOfData = "P";
                    }

                    oSegment.set_DataElementValue(1, 0, "00");
                    oSegment.set_DataElementValue(3, 0, "00");
                    oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SenderID.Trim());//"1234545");//
                    oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_ReceiverID.Trim().Replace("*",""));//"V2EL");//
                    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    oSegment.set_DataElementValue(11, 0, "^");
                    oSegment.set_DataElementValue(12, 0, "00501");
                    InterchangeHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                    oSegment.set_DataElementValue(14, 0, "0");
                    oSegment.set_DataElementValue(15, 0, _TypeOfData);
                    oSegment.set_DataElementValue(16, 0, ":");

                    #endregion " Interchange Segment "

                    #region " Functional Group "

                    //Create the functional group segment
                    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("005010X223A2"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                    oSegment.set_DataElementValue(1, 0, "HC");
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));////_SenderName);
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//// _ReceiverCode.Trim());//"ClarEDI");
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    FunctionalGroupHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                    oSegment.set_DataElementValue(7, 0, "X");
                    oSegment.set_DataElementValue(8, 0, "005010X223A2");

                    #endregion " Functional Group "

                    #region ST - TRANSACTION SET HEADER

                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                    TransactionSetHeader = ControlNumberGeneration();
                    oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"
                    oSegment.set_DataElementValue(3, 0, "005010X223A2");

                    #endregion ST - TRANSACTION SET HEADER

                    #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                    //Beginning Segment 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                    oSegment.set_DataElementValue(1, 0, "0019"); //Hierarchical Structure Code
                    oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                    oSegment.set_DataElementValue(3, 0, TransactionSetHeader);//"1234"); //Reference identification
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Date of claim
                    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //"1230");
                    oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                    #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION
                

                    #region NM1 - SUBMITTER


                    //1000A SUBMITTER
                    //NM1 SUBMITTER

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "41");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim().Replace("*",""));//clinic code or Electronic Transmitter Identification No.
                    }

                    //PER SUBMITTER EDI CONTACT INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                    oSegment.set_DataElementValue(1, 0, "IC");
                    if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//txtSubmitterContactName.Text.Trim().Replace("*",""));//Contact person name of clinic
                    }
                    else
                    {
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                    }

                    oSegment.set_DataElementValue(3, 0, "TE");
                    if (dtSubmitter != null && Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                    {
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""));//txtSubmitterPhone.Text.Trim().Replace("*","").Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone
                    }

                    #endregion NM1 - SUBMITTER

                    #region NM1 - RECEIVER NAME

                    //1000B RECEIVER
                    //NM1 RECEIVER NAME
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                    oSegment.set_DataElementValue(1, 0, "40");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sClearingHouseCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"GatewayEDI");//clearing house or contractor or carrier or FI name
                    if (dtClearingHouse != null && Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]) != "")
                    {
                        oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.
                    }

                    #endregion NM1 - RECEIVER NAME

                    nHlCount = 0;

                    if (SelectedTransactions != null)
                    {
                        if (SelectedTransactions.Count > 0)
                        {
                            for (int i = 0; i < SelectedTransactions.Count; i++)
                            {
                                _ClaimStatus = "";
                                oTransaction = null;
                                TransactionLineEDI oTransLine = null;
                                UB04Transaction oUBTransaction = ogloUB04.GetUBClaim(Convert.ToInt64(SelectedMasterTransactions[i]), Convert.ToInt64(SelectedTransactions[i]));
                                if (oUBTransaction != null)
                                {
                                    oTransaction = oUBTransaction.Transaction;
                                }

                                #region "Master EDI data - Dataset data set in data table "

                                dsMaster = null;
                                dsMaster = GetMaster_EDI_UB_5010(oTransaction.ContactID, oTransaction.ProviderID, oTransaction.ResponsibilityNo,
                                    oTransaction.TransactionMasterID, Convert.ToInt64(oTransaction.FacilityCode), _ClinicID, oTransaction.IsSameAsBillingProvider,
                                    oTransaction.TransactionID, true,oTransaction.ReferalProviderID_New,oTransaction.Lines[0].RenderingProviderId);

                                DataTable dtPatientInsurances = dsMaster.Tables["PatientInsurance"];
                                DataTable dtFacility = dsMaster.Tables["Facility"];
                                DataTable dtBillingProvider = dsMaster.Tables["BillingProvider"];
                                DataTable dtPatientPaid = dsMaster.Tables["PatientPaid"];
                                DataTable dtDx = dsMaster.Tables["Diagnosis"];
                                DataTable dtMasterSetting = dsMaster.Tables["MasterSetting"];
                                DataTable _dtPayment = dsMaster.Tables["SVDData"];
                                DataTable dtAllcasdata = dsMaster.Tables["CASData"];
                                DataTable dtAttendingProvider = dsMaster.Tables["AttendingProvider"];
                                DataTable dtRefProvider = dsMaster.Tables["RefferingProvider"];
                                DataTable dtRendProvider = dsMaster.Tables["RenderingProvider"];
                                DataTable dtBillingProviderTaxonomy = dsMaster.Tables["BillingProviderTaxonomy"];
                                DataTable dtPWKData = dsMaster.Tables["PWKData"];
                                DataTable dtRenderringProviderTaxonomy = dsMaster.Tables["RenderringProviderAsAttendingTaxonomy"];
                                DataTable dtBillingProviderAsAttendingTaxonomy = dsMaster.Tables["BillingProviderAsAttendingTaxonomy"];
                                #endregion

                                if (oUBTransaction != null && oTransaction != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    {

                                        string _ClaimNo = "";

                                        #region "Formatting the Claim Number"

                                        _ClaimNo = FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNumber));

                                        #endregion

                                        if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                        {
                                            MessageBox.Show("Patient " + oTransaction.PatientFirstName + " " + oTransaction.PatientLastName + " Insurance details are missing for claim number " + _ClaimNo + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return "";
                                        }

                                        for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                        {
                                            //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                            nHlCount = nHlCount + 1;
                                            nHlProvParent = nHlCount;
                                            //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                            //HL-BILLING PROVIDER
                                            string _PayerResponsibilityName = "";
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                            oSegment.set_DataElementValue(3, 0, "20");
                                            oSegment.set_DataElementValue(4, 0, "1");


                                            #region Billing Provider

                                            string PrimaryBillingProviderID = "";

                                            //2010AA BILLING PROVIDER
                                            //NM1 BILLING PROVIDER NAME
                                            if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                            {
                                                if (dtBillingProviderTaxonomy != null && dtBillingProviderTaxonomy.Rows.Count > 0)
                                                {
                                                    if (FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != ""
                                                       && FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyQualifier"].ToString().Trim()) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PRV"));
                                                        oSegment.set_DataElementValue(1, 0, "BI");
                                                        oSegment.set_DataElementValue(2, 0, "PXC");
                                                        oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                    }
                                                }

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "85");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["LastName"])));//Billing provider name
                                                }
                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["FirstName"])));
                                                //}
                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["MiddleName"])));
                                                //}

                                                //if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])) != "")
                                                //{
                                                //    oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["sSuffix"])));

                                                //}

                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"])));
                                                }
                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])) != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"])));
                                                    PrimaryBillingProviderID = FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"]));
                                                }

                                                //Add Physical Address in Pay to address .
                                                if (Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyState"]).Trim() == "" || Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"]).Trim() == "")
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state                                                   
                                                    oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP
                                                    if (Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim() != "")
                                                    {
                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["IncludeExtendedZip"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, string.Concat(Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim(), Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]).Trim()));
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"])));//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"])));////Provider City
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyState"])));//Provider state                                               
                                                    oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]))));//Provider ZIP
                                                    if (Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim() != "")
                                                    {
                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["IncludeExtendedZip"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, String.Concat(Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim(), Convert.ToString(dtBillingProvider.Rows[0]["PhyAreaCode"]).Trim()));
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["PhyCountryCode"]).Trim());
                                                        }
                                                    }
                                                }              

                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                }

                                                if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                {
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "0B" || FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "1G")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                    }
                                                }



                                                #region "Billing Contact Information"
                                                //Contact Information 
                                                if (FormatString(dtBillingProvider.Rows[0]["PhoneNo"].ToString().Trim()) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\PER"));
                                                    oSegment.set_DataElementValue(1, 0, "IC");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(dtBillingProvider.Rows[0]["ContactName"].ToString().Trim()));

                                                    oSegment.set_DataElementValue(3, 0, "TE");
                                                    oSegment.set_DataElementValue(4, 0, FormatString(dtBillingProvider.Rows[0]["PhoneNo"].ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Billing Provider Pay to Address "

                                                if (Convert.ToString(dtBillingProvider.Rows[0]["PhyAddline1"]).Trim() != "" && Convert.ToString(dtBillingProvider.Rows[0]["PhyCity"]).Trim() != "" && Convert.ToString(dtBillingProvider.Rows[0]["PhyState"]).Trim() != "" && Convert.ToString(dtBillingProvider.Rows[0]["PhyZIP"]).Trim() != "")
                                                {

                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])) != "")
                                                    {
                                                        //In 5010 Billing Provider Pay to address 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "87");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["EntityType"])));

                                                        //N3 BILLING PROVIDER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["Address1"])));//Provider Address

                                                        //N4 BILLING PROVIDER LOCATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["City"])));////Provider City
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["State"])));//Provider state

                                                        //oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", "").Replace("~","").Replace(":","").Replace("-",""));//Provider ZIP
                                                        oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtBillingProvider.Rows[0]["ZIP"])), FormatString(Convert.ToString(dtBillingProvider.Rows[0]["AreaCode"]))));//Provider ZIP

                                                        if (Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim() != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtBillingProvider.Rows[0]["CountryCode"]).Trim());
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }


                                            #endregion

                                            #endregion
                                            //'******************************************************************************************************
                                            //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                            //'******************************************************************************************************
                                            #region Subscriber
                                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                            {
                                                string _strRelation = "";
                                                string _strInsuranceType = "";
                                                _strRelation = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                _strInsuranceType = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (_strInsuranceType == "MB")
                                                {
                                                    if (_strRelation != "18")
                                                    {
                                                        //_strRelation = "18"; 
                                                    }
                                                }

                                                #region Subscriber HL Loop - 2000B

                                                nHlCount = nHlCount + 1;
                                                nHlSubscriberParent = nHlCount;

                                                //2000B SUBSCRIBER HL LOOP
                                                //HL-SUBSCRIBER
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                oSegment.set_DataElementValue(3, 0, "22");

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "0");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "1");

                                                }

                                                //SBR SUBSCRIBER INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));

                                                #region "Responsibility No"

                                                if (oTransaction.ResponsibilityNo == 1)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "P");//_SubscriberInsurancePST);//"P");
                                                }
                                                else if (oTransaction.ResponsibilityNo == 2)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "S");//_SubscriberInsurancePST);//"P");
                                                }
                                                else if (oTransaction.ResponsibilityNo == 3)
                                                {
                                                    oSegment.set_DataElementValue(1, 0, "T");//_SubscriberInsurancePST);//"P");
                                                }

                                                #endregion

                                                if (_strRelation == "18")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "18");//20091222                                                    
                                                }

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludePlanname"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                }


                                                //This is Claim filling Indicator code in EDI implementation guide.
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                }

                                                //2010BA SUBSCRIBER
                                                //NM1 SUBSCRIBER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                if (_strRelation != "18" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsCompnay"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "2");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sCompanyName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberOrgName"                                                   
                                                }                                               
                                                else
                                                {
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                    }

                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, "MI");
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Insurance Id"
                                                    }
                                                }
                                                 #endregion SubscriberHL Loop - 2000B

                                                //N3 SUBSCRIBER ADDRESS
                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeSubscriberAddress"]) == true || _strRelation == "18")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"
                                                    }

                                                    //N4 SUBSCRIBER CITY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCountryCode"]).Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCountryCode"]).Trim());
                                                    }



                                                    string _SubscriberGender = "";
                                                    //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION   

                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");

                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"

                                                        _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                        if (_SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                        {
                                                            _SubscriberGender = "U";
                                                        }
                                                        oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 1).ToUpper());//"SubscriberGender"
                                                    }

                                                }
                                                #region Payer Information Loop 2010BB
                                                //2010BC SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Substring(0, 34);

                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"
                                                if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                }

                                                string str = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Trim();
                                                ////////N3 PAYER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceAddress"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress2"

                                                ////////N4 PAYER CITY
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceCity"
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"InsuranceState"
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"InsuranceZip"
                                                #endregion

                                                if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sClaimOfficeNumber"] != null)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                    if (FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"]).Trim()) != "" && dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"] != null)
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sEDIAltPayerIDType"]).Trim()));
                                                    }
                                                    else
                                                        oSegment.set_DataElementValue(1, 0, "FY");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[0]["sClaimOfficeNumber"]).Trim()));
                                                }


                                                if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                {
                                                    if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                    {
                                                        if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "0B" && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != "1G")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                        }
                                                    }
                                                }

                                                if (_strRelation != "18")
                                                {
                                                    nHlCount = nHlCount + 1;
                                                    //2000B DEPENDENT HL LOOP
                                                    //HL-DEPENDENT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                    oSegment.set_DataElementValue(3, 0, "23");
                                                    oSegment.set_DataElementValue(4, 0, "0");

                                                    //PAT - PATIENT/DEPENDENT INFORMATION

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //01 - Spouse 19 - Child

                                                    #region " Patient Info"

                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "QC");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientLastName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Last Name
                                                    oSegment.set_DataElementValue(4, 0, oTransaction.PatientFirstName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient First Name

                                                    if (oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, oTransaction.PatientMiddleName.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Patient Middle Name
                                                    }
                                                    //N3 - ADDRESS INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientAddress1.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"Address"

                                                    //N4 - GEOGRAPHIC LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, oTransaction.PatientCity.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"City"
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.PatientState.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"State"
                                                    oSegment.set_DataElementValue(3, 0, oTransaction.PatientZip.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"Zip"
                                                    if (oTransaction.PatientCountry.Trim().ToUpper() != "US")
                                                    {
                                                        gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
                                                        oSegment.set_DataElementValue(4, 0, oContact.getCountryCode(oTransaction.PatientCountry.Trim().ToUpper()));
                                                        oContact.Dispose();
                                                    }

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.PatientDOB.ToShortDateString())));
                                                    if (oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "" || oTransaction.PatientGender.Trim().Trim().Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "OTHER")
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"                                                                   
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberGender"
                                                    }

                                                    #endregion " Patient Info"

                                                }
                                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                  //  oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;
                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                    //oTransLine.Dispose();//UB04
                                                }

                                                _ClaimTotal = _claimAmount.ToString("#0.00");
                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }

                                                #region Claim Details - Loop 2300
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                if (gloGlobal.gloPMGlobal.IsUseClaimPrefix && gloGlobal.gloPMGlobal.sClaimPrefix != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, String.Concat(gloGlobal.gloPMGlobal.sClaimPrefix, _ClaimNo)); //Patient Account no                                                         
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(1, 0, _ClaimNo); //Patient Account no         
                                                }            
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", "").Replace("~", "").Replace(":", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount
                                                oSegment.set_DataElementValue(5, 1, oUBTransaction.Facilitytypecode.Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //21 - Inpatient Hospital

                                                if (oTransaction.IsReplacementClaim == true && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsClaimFrequencyOne"])==false)
                                                {
                                                    _ClaimStatus = "7";
                                                }
                                                else if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsClaimFrequencyOne"]) == true)
                                                {
                                                    _ClaimStatus = "1";
                                                }
                                                else if (oUBTransaction.Frequencytypecode.Trim() != "")
                                                {
                                                    _ClaimStatus = oUBTransaction.Frequencytypecode.Trim();
                                                }
                                                oSegment.set_DataElementValue(5, 2, "A");//UB HardCoded Facility Code Qualifier
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus); //Question......
                                               // oSegment.set_DataElementValue(6, 0, "Y");

                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    _IsAccessAssignment = Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAccessAssignment"]);
                                                }


                                                if (_IsAccessAssignment == true)
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "A");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(7, 0, "C");
                                                }

                                                if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bAssignmentofBenifit"]) == true)
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(8, 0, "N");
                                                }
                                                //Signature on file.
                                                if (oTransaction.SOF == true)
                                                {
                                                    oSegment.set_DataElementValue(9, 0, "Y");
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(9, 0, "I");
                                                }
                                                //oSegment.set_DataElementValue(18, 0, "Y");//UB04
                                                if (oTransaction.DelayReasonCodeID != "")
                                                {
                                                    oSegment.set_DataElementValue(20, 0, oTransaction.DelayReasonCodeID);
                                                }

                                                #region "Discharge Hour"

                                                if ((oUBTransaction.DischargeHour).ToString().Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04DischargeHour"]) == true)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "096");
                                                    oSegment.set_DataElementValue(2, 0, "TM");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString((oUBTransaction.DischargeHour).ToString().Trim()));
                                                }

                                                #endregion

                                                #region "Statement Dates"

                                                if (oUBTransaction.MaxDOS.ToString() != "" && oUBTransaction.MinDOS.ToString() != "")
                                                {
                                                    string StatementDate = gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MaxDOS.ToShortDateString());
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "434");
                                                    oSegment.set_DataElementValue(2, 0, "RD8");
                                                    oSegment.set_DataElementValue(3, 0, StatementDate);//Claim Date
                                                }

                                                #endregion

                                                #region "Admission Date"
                                                if ((oUBTransaction.MinDOS).ToShortDateString() != "" && oUBTransaction.MinDOS != DateTime.MaxValue)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "435");
                                                    if (Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04AdmissionHour"]) == true)
                                                        oSegment.set_DataElementValue(2, 0, "DT");
                                                    else
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                    if (Convert.ToString(oUBTransaction.AdmissionHour).Trim() != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeUB04AdmissionHour"]) == true)
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())) + oUBTransaction.AdmissionHour.Trim());
                                                    else
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oUBTransaction.MinDOS.ToShortDateString())));
                                                }

                                                #endregion

                                                #region "Admission Type Code"

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CL1"));

                                                if (Convert.ToString(oUBTransaction.AdmissionTypeCode) != "")
                                                {
                                                    oSegment.set_DataElementValue(1, 0, oUBTransaction.AdmissionTypeCode);
                                                }

                                                if (Convert.ToString(oUBTransaction.AdmissionSource) != "")
                                                {
                                                    oSegment.set_DataElementValue(2, 0, oUBTransaction.AdmissionSource);
                                                }

                                                if (Convert.ToString(oUBTransaction.DischargeStatus) != "")
                                                {
                                                    oSegment.set_DataElementValue(3, 0, oUBTransaction.DischargeStatus);
                                                }

                                                #endregion

                                                #region PWk Data
                                                if (dtPWKData != null && dtPWKData.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtPWKData.Rows[0]["ReportTypeCode"]).Trim() != "" && Convert.ToString(dtPWKData.Rows[0]["ReportTransmissionCode"]).Trim() != "" && Convert.ToString(dtPWKData.Rows[0]["AttachmentControlNumber"]).Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\PWK"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPWKData.Rows[0]["ReportTypeCode"]).Trim());
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPWKData.Rows[0]["ReportTransmissionCode"]).Trim());
                                                        oSegment.set_DataElementValue(5, 0, "AC");
                                                        oSegment.set_DataElementValue(6, 0, Convert.ToString(dtPWKData.Rows[0]["AttachmentControlNumber"]).Trim());
                                                    }
                                                }
                                                #endregion

                                                #region Patient Paid Amount.

                                                //string _AmountPaid = String.Empty;
                                                //if (dtPatientPaid != null && dtPatientPaid.Rows.Count > 0)
                                                //{
                                                //    if (Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]).Trim() != "")
                                                //        _AmountPaid = FormatAmount(Convert.ToString(dtPatientPaid.Rows[0]["TotalPaid"]));
                                                //}

                                                //if (_AmountPaid.Trim() != string.Empty && _AmountPaid.Trim() != "0.00" && _AmountPaid.Trim() != "0.0" && _AmountPaid.Trim() != "0")
                                                //{
                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\AMT"));
                                                //    oSegment.set_DataElementValue(1, 0, "F5");
                                                //    oSegment.set_DataElementValue(2, 0, _AmountPaid);
                                                //}

                                                #endregion

                                                #endregion

                                                #region Claim Remittance Reference #

                                                if ((Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"])).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "F8");
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                }

                                                #endregion

                                                #region Service Authorization exception code

                                                if (oTransaction.ServiceAuthExceCode.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "4N");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.ServiceAuthExceCode);
                                                }

                                                #endregion

                                                #region "Prior Authorization Number"

                                                if (FormatString(oTransaction.PriorAuthorizationNo) != "")
                                                {
                                                    //REF CLEARING HOUSE CLAIM NUMBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.PriorAuthorizationNo)); //Claim No
                                                }

                                                #endregion

                                                #region Clinical Trial Number

                                                if (oTransaction.sIDENo.ToString().Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "P4");
                                                    oSegment.set_DataElementValue(2, 0, oTransaction.sIDENo);
                                                }

                                                #endregion 

                                                #region "BOX19 Note"

                                                if (FormatString(oTransaction.Box19NoteDescription) != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NTE"));
                                                    oSegment.set_DataElementValue(1, 0, "ADD");
                                                    oSegment.set_DataElementValue(2, 0, FormatString(oTransaction.Box19NoteDescription)); //Claim No
                                                }

                                                #endregion

                                                #region HI - Diagnosis

                                                //HI HEALTH CARE DIAGNOSIS CODES                                                                                                                                                                                         
                                                bool IsOtherDignosisAdded = false;
                                                int DxOtherIndex = 0;

                                                string code_no = "";
                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {
                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "BK");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 1, "ABK");
                                                                }
                                                                oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                                if (Convert.ToBoolean(_IsIncludePrimaryDxInBox69) == true)
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                    if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 1, "BJ");
                                                                    }
                                                                    else
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 1, "ABJ");
                                                                    }
                                                                    oSegment.set_DataElementValue(1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                                }
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {

                                                                if (code_no == "")
                                                                {
                                                                    code_no = "'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                else
                                                                {
                                                                    code_no += ",'" + Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", "") + "'";
                                                                }
                                                                if (IsOtherDignosisAdded == false)
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                                }
                                                                DxOtherIndex++;
                                                                IsOtherDignosisAdded = true;
                                                                if (oTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "BF");
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(DxOtherIndex, 1, "ABF");
                                                                }
                                                                oSegment.set_DataElementValue(DxOtherIndex, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("~", "").Replace(":", ""));
                                                            }
                                                        }
                                                    }

                                                    if (code_no != "" && _IsUndo != true)
                                                    {
                                                        code_no = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                                                    }
                                                    else
                                                    {
                                                        code_no = "";
                                                    }
                                                }

                                                if (code_no != "")
                                                {
                                                    string _message;

                                                    _message = "ICD9 is Invalid." + Environment.NewLine + "For Claim No :" + _ClaimNo + Environment.NewLine + "Code : " + code_no + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "                                                            

                                                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                    {
                                                        return "";
                                                    }
                                                }

                                                #endregion

                                                #region "Condition code,Occurrence span code,Occurrence code, Value Code"

                                                //Occurrence Span Code information
                                                if (oUBTransaction != null && oUBTransaction.dtOccurencespancodes != null && oUBTransaction.dtOccurencespancodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtOccurencespancodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["Codes"])) != ""
                                                            && FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"])) != "")
                                                        {

                                                            oSegment.set_DataElementValue(cnt + 1, 1, "BI");
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["Codes"])));
                                                            oSegment.set_DataElementValue(cnt + 1, 3, "RD8");
                                                            if (FormatString(Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["ToDate"])) != "")
                                                            {
                                                                oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]) + "-" + Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["ToDate"]));
                                                            }
                                                            else
                                                            {
                                                                oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]) + "-" + Convert.ToString(oUBTransaction.dtOccurencespancodes.Rows[cnt]["FromDate"]));
                                                            }
                                                        }
                                                    }
                                                }
                                                //Occurrence Code
                                                if (oUBTransaction != null && oUBTransaction.dtOccurencecodes != null && oUBTransaction.dtOccurencecodes.Rows.Count > 0)
                                                {

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtOccurencecodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BH");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["Codes"])));
                                                        }
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["OccurrenceDate"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 3, "D8");
                                                            oSegment.set_DataElementValue(cnt + 1, 4, Convert.ToString(oUBTransaction.dtOccurencecodes.Rows[cnt]["OccurrenceDate"].ToString()).ToString());
                                                        }
                                                    }
                                                }

                                                //Value Code
                                                if (oUBTransaction != null && oUBTransaction.dtValuecodes != null && oUBTransaction.dtValuecodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtValuecodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BE");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Codes"])));
                                                        }
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Amount"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 5, FormatAmount(Convert.ToString(oUBTransaction.dtValuecodes.Rows[cnt]["Amount"])));
                                                        }

                                                    }
                                                }

                                                //Condition Code
                                                if (oUBTransaction != null && oUBTransaction.dtConditioncodes != null && oUBTransaction.dtConditioncodes.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                    for (int cnt = 0; oUBTransaction.dtConditioncodes.Rows.Count > cnt; cnt++)
                                                    {
                                                        oSegment.set_DataElementValue(cnt + 1, 1, "BG");
                                                        if (FormatString(Convert.ToString(oUBTransaction.dtConditioncodes.Rows[cnt]["Codes"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(cnt + 1, 2, FormatString(Convert.ToString(oUBTransaction.dtConditioncodes.Rows[cnt]["Codes"])));
                                                        }

                                                    }
                                                }

                                                #endregion

                                                #region Attending Provider(Same as Billing Provider)

                                                //2010AA BILLING PROVIDER
                                                //NM1 BILLING PROVIDER NAME
                                                string PrimaryAttendingProviderID = "";
                                                string sUBBox77Rendering = "";
                                                string sUBBox77Billing = "";
                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    sUBBox77Rendering = Convert.ToString(dtPatientInsurances.Rows[0]["sUBBox77Rendering"]);
                                                    sUBBox77Billing = Convert.ToString(dtPatientInsurances.Rows[0]["sUBBox77Billing"]);

                                                }

                                                #region Attending provider Or Operation provider setting wise

                                                if (sUBBox77Billing.Trim().ToUpper() == "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        PrimaryAttendingProviderID = FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"]));
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }
                                                        if (dtBillingProviderAsAttendingTaxonomy != null && dtBillingProviderAsAttendingTaxonomy.Rows.Count > 0 && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            if (FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "AT");
                                                                oSegment.set_DataElementValue(2, 0, "PXC");
                                                                oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                            }
                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"]));
                                                        }
                                                        if (FormatString(Convert.ToString(dtRenderringProviderTaxonomy.Rows[0]["Taxonomy"])) != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "AT");
                                                            oSegment.set_DataElementValue(2, 0, "PXC");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtRenderringProviderTaxonomy.Rows[0]["Taxonomy"])));

                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }
                                                }
                                                if (sUBBox77Billing.Trim().ToUpper() == "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                }

                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }

                                                }

                                                #endregion


                                                #region both operation and attending provider

                                                if (sUBBox77Billing.Trim().ToUpper() == "Both Operating and Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Rendering.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    #region Attendging provider As billing Provider

                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"]));
                                                        }
                                                        if (dtBillingProviderAsAttendingTaxonomy != null && dtBillingProviderAsAttendingTaxonomy.Rows.Count > 0 && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            if (FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "AT");
                                                                oSegment.set_DataElementValue(2, 0, "PXC");
                                                                oSegment.set_DataElementValue(3, 0, FormatString(dtBillingProviderAsAttendingTaxonomy.Rows[0]["sTaxonomyCode"].ToString().Trim()));
                                                            }
                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }

                                                    #endregion

                                                    #region Operatin provider AS billing Provider

                                                    if (dtAttendingProvider != null && dtAttendingProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sLastName"])));//Billing provider name
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sFirstName"])));
                                                        }
                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sMiddleName"])));
                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(7, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sSuffix"])));

                                                        }

                                                        if (FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtAttendingProvider.Rows[0]["sNPI"])));

                                                        }

                                                        if (dtBillingProvider != null && dtBillingProvider.Rows.Count > 0)
                                                        {
                                                            if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) != ""
                                                                && FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])) != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                                                                    Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                            else if (FormatString(Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"])) == "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])) != "" &&
                                                                    FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])) != "" &&
                                                                    Convert.ToBoolean(dtBillingProvider.Rows[0]["IsDefaultOther"]) == false)
                                                            {
                                                                //If setting is set as EmployerID or SSN in billing source from plan ID (Alternate ID settings) then this else will get executed.
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                if (Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "0B" ||
                                                                  Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"]).Trim() == "1G")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifier"])));
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 0, "G2");
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtBillingProvider.Rows[0]["TaxIdentifierValue"])));
                                                            }
                                                        }
                                                    }
                                                    #endregion
                                                }

                                                else if (sUBBox77Rendering.Trim().ToUpper() == "Both Operating and Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Attending".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Operating".ToUpper() && sUBBox77Billing.Trim().ToUpper() != "Both Operating and Attending".ToUpper())
                                                {
                                                    #region Attendging provider As Rendering provider

                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "71");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                            PrimaryAttendingProviderID = FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"]));
                                                        }
                                                        if (FormatString(Convert.ToString(dtRenderringProviderTaxonomy.Rows[0]["Taxonomy"])) != "" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]) == true)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "AT");
                                                            oSegment.set_DataElementValue(2, 0, "PXC");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtRenderringProviderTaxonomy.Rows[0]["Taxonomy"])));

                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }

                                                    #endregion

                                                    #region Operating provider As Rendering provider
                                                    if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "72");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                //else
                                                //{
                                                //    MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    return "";
                                                //}

                                                #endregion

                                                #region Referring Provider - 2310A

                                                if (oTransaction.ReferalProviderID_New > 0 || oTransaction.IsSameAsBillingProvider == true)
                                                {

                                                    if (dtRefProvider != null && dtRefProvider.Rows.Count > 0)
                                                    {
                                                        //2310B Referring PROVIDER
                                                        //NM1 Referring PROVIDER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "DN");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sLastName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, FormatString(dtRefProvider.Rows[0]["sLastName"].ToString())); //"ReferringLastname"
                                                        }
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sFirstName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(4, 0, FormatString(dtRefProvider.Rows[0]["sFirstName"].ToString()));//"ReferringFirstname"
                                                        }
                                                        if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sMiddleName"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(5, 0, FormatString(dtRefProvider.Rows[0]["sMiddleName"].ToString()));
                                                        }
                                                        if (FormatString(dtRefProvider.Rows[0]["sNPI"].ToString()) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(dtRefProvider.Rows[0]["sNPI"].ToString()));//"NPI"
                                                        }

                                                        //PRV REFERRING PROVIDER INFORMATION

                                                        //if (FormatString(Convert.ToString(dtRefProvider.Rows[0]["sTaxonomy"])) != "")
                                                        //{
                                                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //    oSegment.set_DataElementValue(1, 0, "RF");
                                                        //    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        //    oSegment.set_DataElementValue(3, 0, FormatString(dtRefProvider.Rows[0]["sTaxonomy"].ToString()));//Reference Identification
                                                        //}

                                                        // REF
                                                        if (Convert.ToString(dtRefProvider.Rows[0]["Code"]).Trim() != "" && Convert.ToString(dtRefProvider.Rows[0]["Value"]).Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, Convert.ToString(dtRefProvider.Rows[0]["Code"]).Trim());
                                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtRefProvider.Rows[0]["Value"]).Trim());
                                                        }


                                                    }
                                                }


                                                #endregion Referring Provider

                                                #region Rendering Provider - 2310B

                                                //2310B RENDERING PROVIDER
                                                //NM1 RENDERING PROVIDER NAME
                                                if (dtRendProvider != null && dtRendProvider.Rows.Count > 0)
                                                {
                                                    bool IsincludeRenderingProvider = false;
                                                    IsincludeRenderingProvider = Convert.ToBoolean(dtRendProvider.Rows[0]["bIncludeRenderingProvider"]);
                                                    if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != PrimaryAttendingProviderID || (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) == PrimaryAttendingProviderID && IsincludeRenderingProvider == true))
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "82");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["sLastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtRendProvider.Rows[0]["sFirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtRendProvider.Rows[0]["sMiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));

                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])) != "")
                                                        {
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["ProviderNPI"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }
                                                        if (FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])) != "" && FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])) != "" && Convert.ToInt64(dtRendProvider.Rows[0]["QualifierMstID"]) > 1)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["Qualifier"])));
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtRendProvider.Rows[0]["QualifierValue"])));//oProviderDetails.NPI);//Billing provider ID/NPI
                                                        }

                                                        ////PRV RENDERING PROVIDER INFORMATION
                                                        //if (Convert.ToString(dtRendProvider.Rows[0]["Taxonomy"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                        //{
                                                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //    oSegment.set_DataElementValue(1, 0, "PE");
                                                        //    oSegment.set_DataElementValue(2, 0, "PXC");//Mutually Defined
                                                        //    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtRendProvider.Rows[0]["Taxonomy"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//Reference Identification
                                                        //}
                                                        ////_Rendering = 2;
                                                    }
                                                }



                                                #endregion

                                                #region Facility - 2310D

                                                //2310E SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION

                                                if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bIsPOS"]) == true)
                                                {
                                                    if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                    {
                                                          bool IsincludeFacility = false;
                                                        IsincludeFacility = Convert.ToBoolean(dtFacility.Rows[0]["bIncludeFacility"]);

                                                        if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != PrimaryBillingProviderID || (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) == PrimaryBillingProviderID && IsincludeFacility == true))
                                                        {
                                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "77");
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["LastName"])));//"FacilityName"

                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])) != "")
                                                            {
                                                                oSegment.set_DataElementValue(8, 0, Convert.ToString(dtFacility.Rows[0]["PrimaryQualifier"]));//NPI code
                                                                oSegment.set_DataElementValue(9, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"])));//NPI
                                                            }

                                                            //N3 SERVICE FACILITY ADDRESS
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["Address1"])));//"FacilityAddr"

                                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["City"])));//"FacilityCity"
                                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["State"])));//"FacilityState"                                                        
                                                            oSegment.set_DataElementValue(3, 0, GetFormattedZipCode(FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])), FormatString(Convert.ToString(dtFacility.Rows[0]["AreaCode"]))));//"FacilityZip"
                                                            if (Convert.ToString(dtFacility.Rows[0]["CountryCode"]).Trim() != "")
                                                            {
                                                                oSegment.set_DataElementValue(4, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["CountryCode"])));
                                                            }

                                                            //Facility Secondary Identification
                                                            if (getSecondaryIDFacility(oTransaction.ContactID) == true)
                                                            {
                                                                if (FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])) != "")
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\REF"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]));//NPI code
                                                                    oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierValue"])));//NPI
                                                                }
                                                            }

                                                            if (FormatString(Convert.ToString(dtFacility.Rows[0]["City"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["State"])) == "" || FormatString(Convert.ToString(dtFacility.Rows[0]["Zip"])) == "")
                                                            {
                                                                MessageBox.Show("For ClaimNo:" + _ClaimNo + " Facility Details(City/State/ZIP Code) is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return "";
                                                            }
                                                       }
                                                    }

                                                }
                                                #endregion

                                                #region SET PRIMARY INSURANCE IDS

                                                DataView _dv = dtPatientInsurances.DefaultView;

                                                _dv.RowFilter = "(sInsuranceFlag)='Primary'";

                                                DataTable _dtAllowed = _dv.ToTable();
                                                if (_dtAllowed != null && _dtAllowed.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(_dtAllowed.Rows[0]["nInsuranceID"]) != "")
                                                    {
                                                        _PrimaryInsuranceId = Convert.ToInt64(_dtAllowed.Rows[0]["nInsuranceID"]);
                                                    }
                                                    if (Convert.ToString(_dtAllowed.Rows[0]["nContactID"]) != "")
                                                    {
                                                        _PrimaryContactID = Convert.ToInt64(_dtAllowed.Rows[0]["nContactID"]);
                                                    }

                                                }

                                                #endregion

                                                #region GET Claim Allowed Amount

                                                string _ClaimAllowedAmount = getFilteredClaimAllowed(oTransaction.TransactionMasterID, _PrimaryInsuranceId, _PrimaryContactID, _dtPayment);
                                                _ClaimAllowedAmount = FormatAmount(_ClaimAllowedAmount);

                                                #endregion


                                                for (int _Insrow = 1; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320
                                                    _strRelation = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                    //1.Payer Responsibility Sequence No.

                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Primary")
                                                    {
                                                        _PayerResponsibilityName = "Primary";
                                                        oSegment.set_DataElementValue(1, 0, "P");//_OtherInsurancePST.Trim().Replace("*","")); //S- Secondary
                                                    }
                                                    else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Secondary")
                                                    {
                                                        _PayerResponsibilityName = "Secondary";
                                                        oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim().Replace("*","")); //S- Secondary
                                                    }
                                                    else if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sInsuranceFlag"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "Tertiary")
                                                    {
                                                        _PayerResponsibilityName = "Tertiary";
                                                        oSegment.set_DataElementValue(1, 0, "T");//_OtherInsurancePST.Trim().Replace("*","")); //T - Tertiary
                                                    }

                                                    //2.Individual Relationship code
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                    //3.Refrence identification
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"22145");///Policy no

                                                    //4. Plan Name
                                                    if (Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIncludePlanname"]) == true)
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance name
                                                    }

                                                    //9.Claim Filing Indicator
                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Commercial Insurance company
                                                    }

                                                    #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information


                                                    #region AMT - Amount Payer Paid

                                                    string _PayercontactID = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["nContactID"]);
                                                    string _PayerInsuranceID = Convert.ToString(dtPatientInsurances.Rows[_Insrow]["nInsuranceID"]);

                                                    string _amt = getFilteredPayerPaid(oTransaction.TransactionMasterID, oTransaction.TransactionID, _PayercontactID, _PayerInsuranceID, _dtPayment);
                                                    _amt = FormatAmount(_amt);

                                                    #endregion AMT - Amount

                                                    #region Allowed Amount

                                                    //if (_amt != "" && _ClaimAllowedAmount.Trim() != "")
                                                    //{
                                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                    //    oSegment.set_DataElementValue(1, 0, "B6");
                                                    //    oSegment.set_DataElementValue(2, 0, _ClaimAllowedAmount);
                                                    //}
                                                   
                                                    if (_amt != "")// && _amt != "0.00" && _amt!="0")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        oSegment.set_DataElementValue(1, 0, "D");
                                                        oSegment.set_DataElementValue(2, 0, _amt);
                                                    }
                                                    
                                                    #endregion

                                                  

                                                    #region OI - Other Insurance

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                    //Assignment of Benefit.
                                                    bool _bAssignmentofbenefit = false;
                                                    _bAssignmentofbenefit = Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bAssignmentofBenifit"]);
                                                    if (_bAssignmentofbenefit == true)
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "Y");
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(3, 0, "N"); //UB04
                                                    }

                                                    if (oTransaction.SOF == true)
                                                    {
                                                        oSegment.set_DataElementValue(6, 0, "Y");
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(6, 0, "I"); //UB04
                                                    }

                                                    #endregion OI - Other Insurance

                                                    //2330A SUBSCRIBER
                                                    #region NM1 SUBSCRIBER NAME - 2330A

                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "IL");
                                                      if (_strRelation != "18" && Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIsCompnay"]) == true)
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sCompanyName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberOrgName"                                                           
                                                            if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"

                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("For Claim No: " + oTransaction.ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return "";
                                                            }

                                                        }                                                     
                                                      else
                                                      {
                                                          oSegment.set_DataElementValue(2, 0, "1");

                                                          if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                          {
                                                              oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberLastOrgName"
                                                          }
                                                          oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"

                                                          if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                          {
                                                              oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubMName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberFirstname"
                                                          }

                                                          if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                          {
                                                              MessageBox.Show("For ClaimNo:" + _ClaimNo + "  " + _PayerResponsibilityName + "  Subscriber Last name is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                              return "";
                                                          }
                                                          if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                          {
                                                              oSegment.set_DataElementValue(8, 0, "MI");
                                                              oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberMemberID"
                                                          }
                                                          else
                                                          {
                                                              MessageBox.Show("For Claim No: " + _ClaimNo + Environment.NewLine + "Insurance ID for " + _PayerResponsibilityName + " subscriber is not present. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                              return "";
                                                          }
                                                      }
                                                    //N3 SUBSCRIBER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"

                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberAddress"
                                                    }

                                                    //N4 SUBSCRIBER CITY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscriberCity"
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"SubscrberState"
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("-", ""));//"SubscriberZip"

                                                    if (Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCountryCode"]).Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCountryCode"]).Trim());
                                                    }

                                                    #endregion NM1 SUBSCRIBER NAME

                                                    #region Payer Information - 2330B

                                                    //2330B SUBSCRIBER/PAYER
                                                    //NM1 PAYER NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "PR");
                                                    oSegment.set_DataElementValue(2, 0, "2");


                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"PayerLastOrgName"                                                                                                      
                                                    if (dtPatientInsurances != null && Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        int _row = 0;
                                                        if (_dtPayment.Rows.Count > 0)
                                                        {
                                                            for (_row = 0; _row < _dtPayment.Rows.Count; _row++)
                                                            {
                                                                if (_dtPayment.Rows[_row]["ContactID"].ToString() == dtPatientInsurances.Rows[_Insrow]["nContactID"].ToString())
                                                                {
                                                                    break;
                                                                }

                                                            }
                                                        }


                                                        if (_row != _dtPayment.Rows.Count && dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToBoolean(dtPatientInsurances.Rows[_Insrow]["bIncludeEdiAltPayerID"]))
                                                        {
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                        }
                                                       // oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//PayerID
                                                    }

                                                    if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"] != null)
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                        if (FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"]).Trim()) != "" && dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"] != null)
                                                        {
                                                            oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sEDIAltPayerIDType"]).Trim()));
                                                        }
                                                        else
                                                            oSegment.set_DataElementValue(1, 0, "FY");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sClaimOfficeNumber"]).Trim()));
                                                    }

                                                  


                                                    #endregion Payer Information

                                                    #region "Set Table Index as per Master SP"

                                                    IndexCount = _Insrow + 14;

                                                    #endregion
                                                    //2330G Billing Provider                                                   
                                                    //NM1 BILLING PROVIDER NAME
                                                    if (dsMaster.Tables[IndexCount] != null && dsMaster.Tables[IndexCount].Rows.Count > 0)
                                                    {
                                                        if (FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "" && FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifierValue"])) != "")
                                                        {
                                                            if (FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "0B" && FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])) != "1G")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "85");
                                                                oSegment.set_DataElementValue(2, 0, "2");

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifier"])));
                                                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(dsMaster.Tables[IndexCount].Rows[0]["SecondaryQualifierValue"])));
                                                            }
                                                        }
                                                    }
                                               
                                                    #endregion Subscriber Secondary Insurance
                                                }//End for loop of Patient Insurance 
                                                //}//end of IF loop for POS

                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                    //oTransLine = new TransactionLineEDI();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "");
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV2 Institution SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV2"));


                                                    //Revenue
                                                    if (oTransLine.RevenueCode.ToString().Trim() != "")// || oTransLine.CrosswalkCPTCode.ToString().Trim() == "" || oTransLine.CrosswalkCPTCode == null)
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oTransLine.RevenueCode.ToString().Trim().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else if (oUBTransaction.RevenueCode.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 0, oUBTransaction.RevenueCode.ToString().Trim().Replace(".", ""));//"Admin revenue code"
                                                    }

                                                    //CPT
                                                    oSegment.set_DataElementValue(2, 1, "HC");
                                                    //Bug #51048: 00000399 : Claim set up
                                                    //Description: Replacement CPT not shown in SV2 segment if CPTCrosswalk is asociated to patient.
                                                    //So commented the code and add condition.
                                                    //if (oTransLine.CPTCode.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    //{
                                                    //    oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString());
                                                    //}
                                                    //Check the Crosswalk
                                                    if (oTransLine.CPTCode.ToString().Trim() == oTransLine.CrosswalkCPTCode.ToString().Trim() || oTransLine.CrosswalkCPTCode.ToString().Trim() == "" || oTransLine.CrosswalkCPTCode == null)
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 2, oTransLine.CrosswalkCPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    }

                                                    if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    if (oTransLine.Mod3Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 5, oTransLine.Mod3Code.ToString());//Modifier 3
                                                    }
                                                    if (oTransLine.Mod4Code.ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                                                    {
                                                        oSegment.set_DataElementValue(2, 6, oTransLine.Mod4Code.ToString());//Modifier 4
                                                    }
                                                    if (Convert.ToBoolean(_bInclude_NDC_Desc_2400loop_UB) == true)
                                                        if (FormatString(Convert.ToString(oTransLine.PrescriptionDesc)) != "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 7, FormatString(Convert.ToString(oTransLine.PrescriptionDesc).Replace("\r\n", " ").Replace("*", "").Replace("~", "").Replace(":", "")));
                                                        }
  
                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(4, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(5, 0, FormatUnit(oTransLine.Unit.ToString()));//Unit/Quantity


                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    if (oTransLine.DateServiceTill != null)
                                                    {
                                                        if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) == Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString()))
                                                            || Convert.ToString(oTransLine.DateServiceTill) == "")
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"                                                             
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(2, 0, "RD8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())) + "-" + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceTill.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                        }
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"
                                                    }

                                                    #region " LINE ITEM CONTROL NUMBER "

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "6R"); //Provider Control Number
                                                        oSegment.set_DataElementValue(2, 0, oTransaction.Lines[nLine].TransactionDetailID.ToString()); //Line Item Control Number

                                                    #endregion

                                                    #endregion

                                                    #region " NDC Code Loop - 2410 "

                                                    if (oTransLine.NDCCode != null && oTransLine.NDCCode.Trim() != "")
                                                    {
                                                        //Start - Loop 2410 NDC Code implementation
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN"));
                                                        oSegment.set_DataElementValue(2, 0, oTransLine.NDCCodeQualifier.Trim()); //LIN - Qualifier
                                                        oSegment.set_DataElementValue(3, 0, oTransLine.NDCCode.Trim());//LIN - NDC Code 11 digit
                                                    }

                                                    if (oTransLine.NDCUnit != null && oTransLine.NDCUnitCode != null && oTransLine.NDCUnit.Trim() != "" && oTransLine.NDCUnitCode.Trim() != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\CTP"));                                                       
                                                        oSegment.set_DataElementValue(4, 0, oTransLine.NDCUnit); //Quantity
                                                        oSegment.set_DataElementValue(5, 1, oTransLine.NDCUnitCode); //Unit or Basis of Measurement                                                        
                                                    }

                                                    //Add Prescription Number 
                                                    if (FormatString(oTransLine.Prescription) != null && FormatString(oTransLine.Prescription.Trim()) != "")
                                                    {
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LIN\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "XZ");
                                                        oSegment.set_DataElementValue(2, 0, FormatString(oTransLine.Prescription.Trim()));
                                                    }

                                                  
                                                    #endregion " NDC Code Loop - 2410 "                                              

                                                    #region "SVD -LINE ADJUDICATION INFORMATION"

                                                    DataTable dtSVDdata = null;
                                                    dtSVDdata = getFilteredSVDLine(oTransaction.TransactionMasterID, oTransaction.Lines[nLine].TransactionMasterDetailID, _dtPayment).Copy();
                                                    if (dtSVDdata != null && dtSVDdata.Rows.Count > 0)
                                                    {
                                                        for (int nSVD = 0; nSVD < dtSVDdata.Rows.Count; nSVD++)
                                                        {
                                                            if (Convert.ToString(dtPatientInsurances.Rows[0]["nInsuranceID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != Convert.ToString(dtSVDdata.Rows[nSVD]["InsuranceID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", ""))
                                                            {
                                                                if (Convert.ToString(dtSVDdata.Rows[nSVD]["InsPaidAmount"]).Replace("*", "").Replace("~", "").Replace(":", "").Trim() != "")
                                                                {
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD"));
                                                                    if (dtSVDdata.Rows[nSVD]["AlternatePayerID"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "" && Convert.ToBoolean(dtSVDdata.Rows[nSVD]["bIncludeEdiAltPayerID"]))
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 0, dtSVDdata.Rows[nSVD]["AlternatePayerID"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Other Payer identification code
                                                                    }
                                                                    else
                                                                    {
                                                                        oSegment.set_DataElementValue(1, 0, dtSVDdata.Rows[nSVD]["PayerID"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", "")); //Other Payer identification code
                                                                    }
                                                                    oSegment.set_DataElementValue(2, 0, FormatAmount(Convert.ToString(dtSVDdata.Rows[nSVD]["InsPaidAmount"])));//Service Line Paid Amount
                                                                    oSegment.set_DataElementValue(3, 1, "HC");//COMPOSITE MEDICAL PROCEDURE IDENTIFIER
                                                                    //Bug #51048: 00000399 : Claim set up
                                                                    //Description: Replacement CPT not shown in SVD segment if CPTCrosswalk is asociated to patient.
                                                                    //So commented the code and add condition as it is present for sending primary batch.
                                                                    //oSegment.set_DataElementValue(3, 2, dtSVDdata.Rows[nSVD]["CPTCode"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//CPT
                                                                    //Check the Crosswalk
                                                                    if (dtSVDdata.Rows[nSVD]["CPTCode"].ToString().Trim() == dtSVDdata.Rows[nSVD]["sCrossWalkCPTCode"].ToString().Trim() || dtSVDdata.Rows[nSVD]["sCrossWalkCPTCode"].ToString().Trim() == "" || dtSVDdata.Rows[nSVD]["sCrossWalkCPTCode"].ToString() == null)
                                                                    {
                                                                        oSegment.set_DataElementValue(3, 2, dtSVDdata.Rows[nSVD]["CPTCode"].ToString().Replace(".", ""));//"ServiceID"
                                                                    }
                                                                    else
                                                                    {
                                                                        oSegment.set_DataElementValue(3, 2, dtSVDdata.Rows[nSVD]["sCrossWalkCPTCode"].ToString().Replace(".", ""));//"ServiceID"
                                                                    }

                                                                    if (Convert.ToString(oTransLine.RevenueCode).Trim() != "")// || oTransLine.CrosswalkCPTCode.ToString().Trim() == "" || oTransLine.CrosswalkCPTCode == null)
                                                                    {
                                                                        oSegment.set_DataElementValue(4, 0, oTransLine.RevenueCode.ToString().Trim().Replace(".", ""));//"ServiceID"
                                                                    }
                                                                    else if (Convert.ToString(oUBTransaction.RevenueCode).ToString().Trim() != "")
                                                                    {
                                                                        oSegment.set_DataElementValue(4, 0, oUBTransaction.RevenueCode.ToString().Trim().Replace(".", ""));//"Admin revenue code"
                                                                    }
                                                                    oSegment.set_DataElementValue(5, 0, FormatAmount(Convert.ToString(dtSVDdata.Rows[nSVD]["Unit"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "")));//Quantity

                                                                    #region "CAS -LINE ADJUSTMENT"

                                                                    DataTable dtcasdata = new DataTable();
                                                                    dtcasdata = getFilteredCasData(oTransaction.TransactionMasterID, oTransaction.Lines[nLine].TransactionMasterDetailID, Convert.ToInt64(dtSVDdata.Rows[nSVD]["ContactID"]), Convert.ToInt64(dtSVDdata.Rows[nSVD]["InsuranceID"]), dtAllcasdata);

                                                                    if (dtcasdata != null && dtcasdata.Rows.Count > 0)
                                                                    {
                                                                        string _payerId = "";
                                                                        string _grpCode = "";
                                                                        Int64 _contactId = 0;

                                                                        for (int rIndex = 0; rIndex < dtcasdata.Rows.Count; rIndex++)
                                                                        {
                                                                            _payerId = Convert.ToString(dtcasdata.Rows[rIndex]["PayerID"]);
                                                                            _contactId = Convert.ToInt64(dtcasdata.Rows[rIndex]["ContactID"]);//20100416

                                                                            if (_contactId > 0)
                                                                            {
                                                                                _grpCode = Convert.ToString(dtcasdata.Rows[rIndex]["GroupCode"]);

                                                                                for (int dIndex = rIndex + 1; dIndex < dtcasdata.Rows.Count; dIndex++)
                                                                                {
                                                                                    if (_contactId == Convert.ToInt64(dtcasdata.Rows[dIndex]["ContactID"])
                                                                                    && _grpCode == Convert.ToString(dtcasdata.Rows[dIndex]["GroupCode"]))
                                                                                    {
                                                                                        dtcasdata.Rows[dIndex]["InsuranceID"] = -1;
                                                                                        dtcasdata.Rows[dIndex]["ContactID"] = -1;
                                                                                        dtcasdata.Rows[dIndex]["InsuranceName"] = "";
                                                                                        dtcasdata.Rows[dIndex]["PayerID"] = "";
                                                                                        dtcasdata.Rows[dIndex]["GroupCode"] = "";
                                                                                        dtcasdata.AcceptChanges();
                                                                                    }
                                                                                }
                                                                            }

                                                                        }
                                                                    }

                                                                    #region "Adding CAS"

                                                                    if (dtcasdata != null && dtcasdata.Rows.Count > 0)
                                                                    {
                                                                        for (int CASIndex = 0, ResonCodeIndex = 1; CASIndex < dtcasdata.Rows.Count; CASIndex++)
                                                                        {

                                                                            if (Convert.ToInt64(dtcasdata.Rows[CASIndex]["ContactID"]) != -1 &&
                                                                                Convert.ToString(dtcasdata.Rows[CASIndex]["GroupCode"]) != "")
                                                                            {
                                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD\\CAS"));
                                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["GroupCode"])); //
                                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["ReasonCode"]));

                                                                                #region "Amount Formatting"

                                                                                string _ClmTotal = Convert.ToString(dtcasdata.Rows[CASIndex]["Amount"]).Trim();
                                                                                _ClmTotal = FormatAmount(_ClmTotal);

                                                                                #endregion

                                                                                if (_ClmTotal != "")
                                                                                {
                                                                                    oSegment.set_DataElementValue(3, 0, _ClmTotal);
                                                                                    ResonCodeIndex = 5;
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string _Total = Convert.ToString((dtcasdata.Rows[CASIndex]["Amount"]));
                                                                                _Total = FormatAmount(_Total);

                                                                                if (_Total != "")
                                                                                {
                                                                                    oSegment.set_DataElementValue(ResonCodeIndex, 0, Convert.ToString(dtcasdata.Rows[CASIndex]["ReasonCode"]));
                                                                                    ResonCodeIndex += 1;
                                                                                    oSegment.set_DataElementValue(ResonCodeIndex, 0, _Total);
                                                                                    ResonCodeIndex += 2;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    #endregion

                                                                    #endregion

                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\SVD\\DTP"));
                                                                    oSegment.set_DataElementValue(1, 0, "573");
                                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                                    oSegment.set_DataElementValue(3, 0, dtSVDdata.Rows[nSVD]["ClaimPaidDate"].ToString().Trim().Replace("*", "").Replace("~", "").Replace(":", ""));//"ServiceDate"
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                }
                                            #endregion " Subscriber "
                                            }//If loop for Patient Insurance
                                            //Transaction Line Loop
                                        }//Transaction SETS Loop
                                    }
                                }
                            }

                            #region " Save EDI File "

                            sPath = "";
                            sPath = gloSettings.FolderSettings.AppTempFolderPath + "837 EDI\\";
                            if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }
                            sEdiFile = GetEDIFileName(sPath, _BatchName);
                            oEdiDoc.Save(sEdiFile);
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();
                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                            if (_bIsCaptionize)
                            {
                                oWriter.Write(strData.ToUpper());
                            }
                            else
                            {
                                oWriter.Write(strData);
                            }
                            oWriter.Close();
                            _result = sEdiFile;

                            #endregion " Save EDI File "

                            #region " Update Claim Manager Table "
                            Int64 _date = 0;
                            Int64 _time = 0;
                            _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                            _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                            gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                            //Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                            ogloClaimManager.SetClaimManagerTVP(_nBatchID, oTransaction.TransactionMasterID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID, odsEDIClaimDetail);
                            ogloClaimManager.Dispose();
                            #endregion

                            oSegment.Dispose();
                            oTransactionset.Dispose();
                            oGroup.Dispose();
                            oInterchange.Dispose();

                        }
                    }
                }
            }//SEF File present IF loop
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                _result = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = "";
            }
            finally
            {
                if (dsHeader != null) { dsHeader.Dispose(); }
                if (dsMaster != null) { dsMaster.Dispose(); }

                if (oEdiDoc != null) { oEdiDoc.Dispose(); }
                if (oInterchange != null) { oInterchange.Dispose(); }
                if (oGroup != null) { oGroup.Dispose(); }
                if (oTransactionset != null) { oTransactionset.Dispose(); }
                if (oSegment != null) { oSegment.Dispose(); }
                if (oSchema != null) { oSchema.Dispose(); }
                if (oSchemas != null) { oSchemas.Dispose(); }
                //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                if (dtClearingHouseID != null) { dtClearingHouseID.Dispose(); }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
                if (ogloUB04 != null)
                {
                    ogloUB04.Dispose();
                    ogloUB04 = null;
                }
            }
            #endregion " Generate EDI "

            return _result;
        }    

        private DataTable getEDIData(Int64 nBatchId, Int64 nClinicId, Int64 nUserID, string ValidTransactionIDs, string Responsibilitytype)
        {

            DataTable _dtEDIData = new DataTable();
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string sErrMessage = "";

            try
            {
                ODB.Connect(false);
                oParameters.Add("@nBatchId", nBatchId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicId", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TransactionIDs", ValidTransactionIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sErrMessage", sErrMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);
                if (Responsibilitytype.ToString().ToUpper().Trim() == "PRIMARY")
                {
                    //ODB.Retrive("EDI837_Generate", oParameters, out _dtEDIData, out sErrMessage);
                    ODB.Retrive("EDI_837P_PRIMARY", oParameters, out _dtEDIData, out sErrMessage);
                }
                else
                {
                    ODB.Retrive("EDI_837P_SECONDARY", oParameters, out _dtEDIData, out sErrMessage);
                }

                if (sErrMessage != "")
                {
                    MessageBox.Show(Convert.ToString(sErrMessage), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _dtEDIData = null;
                }
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                throw new Exception();
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                }
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }

            }
            return _dtEDIData;
        }

        private DataTable getEDIDataUB(Int64 nBatchId, Int64 nClinicId, Int64 nUserID, string ValidTransactionIDs, string Responsibilitytype)
        {

            DataTable _dtEDIData = new DataTable();
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string sErrMessage = "";

            try
            {
                ODB.Connect(false);
                oParameters.Add("@nBatchId", nBatchId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicId", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TransactionIDs", ValidTransactionIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sErrMessage", sErrMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);
                if (Responsibilitytype.ToString().ToUpper().Trim() == "PRIMARY")
                {
                    ODB.Retrive("EDI_837I_PRIMARY", oParameters, out _dtEDIData, out sErrMessage);
                }
                else
                {
                    ODB.Retrive("EDI_837I_SECONDARY", oParameters, out _dtEDIData, out sErrMessage);
                }

                if (sErrMessage.Trim() != "")
                {
                    _dtEDIData = null;
                    MessageBox.Show(sErrMessage.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                throw new Exception();
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                }
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }

            }
            return _dtEDIData;
        }

        /*
        public string EdiGenerateWithService(string _BatchName, Int64 BatchID, ArrayList validTransactionIDs, Int64 TransactionMasterID, Int64 ClaimNo, Int64 PatientID, EDIService.InsuranceType _InsuranceType, EDIService.EDIType _EDIType)
        {
            gloClaimManager objClaimManager = new gloClaimManager(_databaseconnectionstring, _emrdatabaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtDiagnosis = new DataTable();
            string _result = "";
            string code_no = "";
            DataTable _dtEDIData = null;
            sEdiFile = "";
            bool ICD9flag = false;
            bool IsICD9valid = false;
            string _strvalidTransactionIDs = "";
            EDIService.EDI OBJEDI = null;
            EDIService.gloWebEDI837 obj = null;

            try
            {
                if (validTransactionIDs != null && validTransactionIDs.Count > 0)
                {
                    for (int _Count = 0; _Count < validTransactionIDs.Count; _Count++)
                    {
                        if (_strvalidTransactionIDs.Trim() != "")
                        {
                            _strvalidTransactionIDs = _strvalidTransactionIDs + ",";
                        }
                        _strvalidTransactionIDs = _strvalidTransactionIDs + Convert.ToString(validTransactionIDs[_Count]);
                    }
                    if (_strvalidTransactionIDs.EndsWith(",") == true)
                    {
                        _strvalidTransactionIDs.Remove(_strvalidTransactionIDs.Length - 1, 1);
                    }

                    IsICD9valid = GetDistinctDiagnosisforSP(BatchID, _strvalidTransactionIDs);
                    if (IsICD9valid)
                    {
                        # region "EDI Generation With EDI FrameWork AND SP"

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        Object _objResult = null;
                        string strEDIServiceURL = "";
                        oSettings.GetSetting("EDISERVICEPATH", out _objResult);
                        if (_objResult != null)
                        {
                            // |Company|Practice|Business"
                            strEDIServiceURL = Convert.ToString(_objResult);
                        }

                        OBJEDI = new EDIService.EDI();
                        OBJEDI.ConnectionString = gloSettings.AppSettings.ConnectionStringPM;
                        OBJEDI.BatchID = BatchID;
                        OBJEDI.ClinicID = ClinicID;
                        OBJEDI.TransactionIDs = _strvalidTransactionIDs;
                        OBJEDI.EDIType = _EDIType;
                        OBJEDI.InsuranceType = _InsuranceType;

                        obj = new EDIService.gloWebEDI837();
                        //obj.Url = "http://m2k/gloWebEDI/gloWebEDI.asmx";
                        if (strEDIServiceURL.Trim() != "")
                        {
                            obj.Url = strEDIServiceURL.Trim();
                            OBJEDI = obj.GetEDI837(OBJEDI);
                        }
                        else
                        {
                            MessageBox.Show("EDI service path not found. ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        string sPath = "";

                        #region " Save EDI File "

                        if (OBJEDI.Message != null)
                        {
                            if (Convert.ToString(OBJEDI.Message).Trim() == "" && OBJEDI.IsGenrated == true)
                            {
                                sPath = appSettings["StartupPath"].ToString() + "\\" + "837 EDI\\";

                                if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }

                                sEdiFile = GetEDIFileName(sPath, _BatchName);


                                System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                                oWriter.Write(OBJEDI.EDIFile.Replace("\n", Environment.NewLine));
                                oWriter.Close();
                                _result = sEdiFile;
                                //MessageBox.Show("EDI claim generated successfully.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}


                        #endregion " Save EDI File "

                                #region " Update Claim Manager Table "
                                Int64 _date = 0;
                                Int64 _time = 0;
                                _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                                _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                                string FunctionalGroupHeader = OBJEDI.FunctionalGroupHeader;
                                string InterchangeHeader = OBJEDI.InterchangeHeader;
                                string TransactionSetHeader = OBJEDI.TransactionSetHeader;
                                gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                                //Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, TransactionMasterID, ClaimNo, PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                                Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, TransactionMasterID, ClaimNo, PatientID, 0, InterchangeHeader, "0", FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                                ogloClaimManager.Dispose();
                                #endregion
                            }
                            else if (OBJEDI.Message.Trim() != "")
                            {
                                MessageBox.Show(OBJEDI.Message.Trim(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        #endregion
                    }

                }

            }
            //catch (System.Web.Services.Protocols.SoapHeaderException)
            //{
            //}
            //catch (System.Web.Services.Protocols.SoapException)
            //{
            //}
            //catch (System.Net.Sockets.SocketException ex)
            //{ 
            //}
            catch (System.Net.WebException ex)
            {
                //string str=ex.Response.ToString();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Error occured in Web Service. ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                MessageBox.Show(Rex.Message);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSegment != null)
                {
                    oSegment.Dispose();
                }
                if (oTransactionset != null)
                {
                    oTransactionset.Dispose();
                }
                if (oGroup != null)
                {
                    oGroup.Dispose();
                }
                if (oInterchange != null)
                {
                    oInterchange.Dispose();
                }
                if (OBJEDI != null)
                {
                    OBJEDI = null;
                }
                if (obj != null) { obj.Dispose(); }
            }
            return _result;
        }
        */
   

        public bool IsInValidICD9(string ICD9Code)//Used For Validation
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object ReturnValue = new object();
            string _sqlQuery = "";
            bool _retVal = true;
            DataTable _DtIcd9 = new DataTable();
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI WITH(NOLOCK) where UPPER(sICD9Code) IN (" + ICD9Code.ToUpper() + ")";

                oDB.Retrive_Query(_sqlQuery, out  _DtIcd9);

                if (_DtIcd9.Rows.Count > 0)
                {
                    string Dx = "";
                    for (int i = 0; i < _DtIcd9.Rows.Count; i++)
                    {

                        Dx = Dx + Environment.NewLine + _DtIcd9.Rows[i]["sICD9Code"].ToString() + ",";

                    }
                    Dx = "ICD9 is Invalid." + Environment.NewLine + "Code : " + Dx + "  " + Environment.NewLine + "Do you want to Continue? ";
                    if (MessageBox.Show(Dx, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        _retVal = false;
                    }
                    else
                        _retVal = true;
                }



            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _retVal;
        }

        #region " Redesign All code for EDI generation "

        public DataSet GetHeader_EDI_4010(Int64 ContactID, Int64 ClinicID, Int64 TransactionID,Boolean IsValidation)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsHeaderEDI = null;
            try
            {
                oDBParameters.Add("@nContactId", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TransactionID", TransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsValidation", IsValidation, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Header_EDI_4010", oDBParameters, out dsHeaderEDI);
                oDB.Disconnect();
                dsHeaderEDI.Tables[0].TableName = "ClearingHouseData";
                dsHeaderEDI.Tables[1].TableName = "SubmitterData";
                if (IsValidation == true)
                {
                    dsHeaderEDI.Tables[2].TableName = "EDISetting";
                    dsHeaderEDI.Tables[3].TableName = "AlphaSetting";
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
                //dsHeaderEDI.Dispose();
                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return dsHeaderEDI;
        }

        public DataSet GetMaster_EDI_4010(Int64 ContactID, Int64 RenderingProviderID, Int64 BillingProviderID, Int16 ResponsibilityNo, Int64 TransMasterID, Int64 FacilityId, Int64 ClinicID, Boolean IsPhysician, Int64 TransactionID, Int64 RefferingProvider,bool IsSecondary)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI = null;
            try
            {
                oDBParameters.Add("@nRenderingProviderID", RenderingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nBillingProviderID", BillingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", TransMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", FacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician ", IsPhysician, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@RefferingProvider", RefferingProvider, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsSecondary", IsSecondary, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_4010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();
                dsMasterEDI.Tables[0].TableName = "MidLevelID";
                dsMasterEDI.Tables[1].TableName = "PatientInsurance";
                dsMasterEDI.Tables[2].TableName = "Facility";
                dsMasterEDI.Tables[3].TableName = "BillingProvider";
                dsMasterEDI.Tables[4].TableName = "PatientPaid";
                dsMasterEDI.Tables[5].TableName = "Diagnosis";
                dsMasterEDI.Tables[6].TableName = "RefferingProvider";
                dsMasterEDI.Tables[7].TableName = "RenderingProvider";
                dsMasterEDI.Tables[8].TableName = "MasterSetting";
                if (IsSecondary == true)
                {
                    dsMasterEDI.Tables[9].TableName = "SVDData";
                    dsMasterEDI.Tables[10].TableName = "CASData";
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
            }

            return dsMasterEDI;
        }

        public DataSet GetHeader_EDI_5010(Int64 ContactID, Int64 ClinicID, Int64 TransactionID,Boolean IsValidation)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsHeaderEDI = null;
            try
            {
                oDBParameters.Add("@nContactId", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TransactionID", TransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsValidation", IsValidation, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Header_EDI_5010", oDBParameters, out dsHeaderEDI);
                oDB.Disconnect();
                dsHeaderEDI.Tables[0].TableName = "ClearingHouseData";
                dsHeaderEDI.Tables[1].TableName = "SubmitterData";
                dsHeaderEDI.Tables[2].TableName = "ChargesSetting";
               // dsHeaderEDI.Tables[3].TableName = "EPSSTSetting";
                dsHeaderEDI.Tables[3].TableName = "bIsCaptionize";
                dsHeaderEDI.Tables[4].TableName = "ProviderSettings";
                if (IsValidation == true)
                {
                    dsHeaderEDI.Tables[5].TableName = "EDISetting";
                    dsHeaderEDI.Tables[6].TableName = "AlphaSetting";
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
                //dsHeaderEDI.Dispose();
                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return dsHeaderEDI;
        }

        public DataSet GetMaster_EDI_5010(Int64 ContactID, Int64 RenderingProviderID, Int64 BillingProviderID, Int16 ResponsibilityNo, Int64 TransMasterID, Int64 FacilityId, Int64 ClinicID, Boolean IsPhysician, Int64 TransactionID, Int64 RefferingProvider, Boolean IsSecondary)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI=null ;
            try
            {                
                oDBParameters.Add("@nRenderingProviderID", RenderingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nBillingProviderID", BillingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", ContactID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", TransMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", FacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician ", IsPhysician, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@RefferingProvider", RefferingProvider, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsSecondary", IsSecondary, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_5010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();
                dsMasterEDI.Tables[0].TableName = "MidLevelID";
                dsMasterEDI.Tables[1].TableName = "PatientInsurance";
                dsMasterEDI.Tables[2].TableName = "Facility";
                dsMasterEDI.Tables[3].TableName = "BillingProvider";
                dsMasterEDI.Tables[4].TableName = "PatientPaid";
                dsMasterEDI.Tables[5].TableName = "Diagnosis";
                dsMasterEDI.Tables[6].TableName = "RefferingProvider";
                dsMasterEDI.Tables[7].TableName = "RenderingProvider";
                dsMasterEDI.Tables[8].TableName = "MasterSetting";
                dsMasterEDI.Tables[9].TableName = "BillingProviderTaxonomy";
                dsMasterEDI.Tables[10].TableName = "ProviderReportingSetting";
                dsMasterEDI.Tables[11].TableName = "EPSDTSetting";
                dsMasterEDI.Tables[12].TableName = "PWKData";
                if (IsSecondary == true)
                {
                    dsMasterEDI.Tables[13].TableName = "SVDData";
                    dsMasterEDI.Tables[14].TableName = "CASData";
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
            }

            return dsMasterEDI;
        }

        public DataSet GetHeader_EDI_UB_4010(Int64 ContactID, Int64 ClinicID, Int64 TransactionID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsHeaderEDI = null;
            try
            {
                oDBParameters.Add("@nContactId", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TransactionID", TransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Header_EDI_UB_4010", oDBParameters, out dsHeaderEDI);
                oDB.Disconnect();
                dsHeaderEDI.Tables[0].TableName = "ClearingHouseData";
                dsHeaderEDI.Tables[1].TableName = "SubmitterData";
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
            }

            return dsHeaderEDI;
        }

        public DataSet GetMaster_EDI_UB_4010(Int64 ContactID, Int64 BillingProviderID, Int16 ResponsibilityNo, Int64 TransMasterID, Int64 FacilityId, Int64 ClinicID, Boolean IsPhysician, Int64 TransactionID, bool IsSecondary)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI = null;
            try
            {                
                oDBParameters.Add("@nBillingProviderID", BillingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", TransMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", FacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician ", IsPhysician, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);                
                oDBParameters.Add("@IsSecondary", IsSecondary, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_UB_4010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();              
                dsMasterEDI.Tables[0].TableName = "PatientInsurance";
                dsMasterEDI.Tables[1].TableName = "Facility";
                dsMasterEDI.Tables[2].TableName = "BillingProvider";
                dsMasterEDI.Tables[3].TableName = "PatientPaid";
                dsMasterEDI.Tables[4].TableName = "Diagnosis";                
                dsMasterEDI.Tables[5].TableName = "MasterSetting";
                if (IsSecondary == true)
                {
                    dsMasterEDI.Tables[6].TableName = "SVDData";
                    dsMasterEDI.Tables[7].TableName = "CASData";
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
            }

            return dsMasterEDI;
        }

        public DataSet GetHeader_EDI_UB_5010(Int64 ContactID, Int64 ClinicID, Int64 TransactionID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsHeaderEDI = null;
            try
            {
                oDBParameters.Add("@nContactId", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TransactionID", TransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Header_EDI_UB_5010", oDBParameters, out dsHeaderEDI);
                oDB.Disconnect();
                dsHeaderEDI.Tables[0].TableName = "ClearingHouseData";
                dsHeaderEDI.Tables[1].TableName = "SubmitterData";
                dsHeaderEDI.Tables[2].TableName = "bIsCaptionize";
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
            }

            return dsHeaderEDI;
        }

        public DataSet GetMaster_EDI_UB_5010(Int64 ContactID, Int64 BillingProviderID, Int16 ResponsibilityNo, Int64 TransMasterID, Int64 FacilityId, Int64 ClinicID, Boolean IsPhysician, Int64 TransactionID, bool IsSecondary,Int64 nRefferingProvider,Int64 nRenderingProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI = null;
            try
            {
                oDBParameters.Add("@nBillingProviderID", BillingProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", TransMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", FacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician", IsPhysician, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nRefferingProvider", nRefferingProvider, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nRenderingProviderID", nRenderingProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsSecondary", IsSecondary, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_UB_5010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();
                dsMasterEDI.Tables[0].TableName = "PatientInsurance";
                dsMasterEDI.Tables[1].TableName = "Facility";
                dsMasterEDI.Tables[2].TableName = "BillingProvider";
                dsMasterEDI.Tables[3].TableName = "PatientPaid";
                dsMasterEDI.Tables[4].TableName = "Diagnosis";
                dsMasterEDI.Tables[5].TableName = "MasterSetting";
                dsMasterEDI.Tables[6].TableName = "AttendingProvider";
                dsMasterEDI.Tables[7].TableName = "RefferingProvider";
                dsMasterEDI.Tables[8].TableName = "RenderingProvider";
                dsMasterEDI.Tables[9].TableName = "BillingProviderTaxonomy";
                dsMasterEDI.Tables[10].TableName = "PWKData";
                dsMasterEDI.Tables[11].TableName = "RenderringProviderAsAttendingTaxonomy";
                dsMasterEDI.Tables[12].TableName = "BillingProviderAsAttendingTaxonomy";
                if (IsSecondary == true)
                {
                    dsMasterEDI.Tables[13].TableName = "SVDData";
                    dsMasterEDI.Tables[14].TableName = "CASData";
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
            }

            return dsMasterEDI;
        }

        /////////////////////////// Fill Transaction Object ///////////////////////////////////

        public TransactionEDI GetChargesClaimDetails_EDI(Int64 TransactionID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            
            DataSet dsTransaction = new DataSet();
            TransactionEDI oTransaction = new TransactionEDI();
            TransactionLineEDI oLine = null;          
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);             
                oDB.Retrive("BL_SELECT_Transaction_Claim_MST_EDI", oDBParameters, out dsTransaction);
                oDB.Disconnect();
                dsTransaction.Tables[0].TableName = "ClaimData";
                dsTransaction.Tables[1].TableName = "ClaimLines";
                dsTransaction.Tables[2].TableName = "ClaimLineNotes";
                dsTransaction.Tables[3].TableName = "ClaimPatient";
                dsTransaction.Tables[4].TableName = "ClaimPriorAuthorization";
                if (dsTransaction.Tables["ClaimData"] != null)
                {
                    if (dsTransaction.Tables["ClaimData"].Rows.Count > 0)
                    {
                        oTransaction.TransactionID = TransactionID;
                        oTransaction.TransactionMasterID = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nTransactionMasterID"]);              
                        oTransaction.OnsiteDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nOnsiteDate"]);
                        oTransaction.InjuryDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nInjuryDate"]);
                        oTransaction.UnableToWorkFromDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nUnableToWorkFromDate"]);
                        oTransaction.UnableToWorkTillDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nUnableToWorkTillDate"]);
                        oTransaction.TransactionDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nTransactionDate"]);                        
                        oTransaction.ClaimNo = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nClaimNo"]);                           
                        oTransaction.PatientID = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nPatientID"]);                       
                        oTransaction.ProviderID = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nTransactionProviderID"]);                        
                        oTransaction.MaritalStatus = dsTransaction.Tables["ClaimData"].Rows[0]["sMaritalStatus"].ToString();
                        oTransaction.FacilityCode = dsTransaction.Tables["ClaimData"].Rows[0]["sFacilityCode"].ToString();
                        oTransaction.FacilityDescription = dsTransaction.Tables["ClaimData"].Rows[0]["sFacilityDescription"].ToString();                        
                        oTransaction.ClinicID = ClinicID;                                                                       
                        oTransaction.HospitalizationDateFrom = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nHopitalizationDateFrom"]);
                        oTransaction.HospitalizationDateTo = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nHopitalizationDateTo"]);                                                
                        oTransaction.WorkersComp = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bWorkersComp"]);
                        oTransaction.WorkersCompNo = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sWorkersCompNo"]);                        
                        oTransaction.AutoClaim = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bAutoClaim"]);
                        oTransaction.AccidentDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nAccidentDate"]);                                                                                           
                        oTransaction.State = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sState"]);
                        oTransaction.OtherAccident = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bOtherAccident"]);
                        oTransaction.OtherAccidentDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nOtherAccidentDate"]);                       
                        oTransaction.ContactID = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nContactID"]);
                        oTransaction.ResponsibilityNo = Convert.ToInt16(dsTransaction.Tables["ClaimData"].Rows[0]["nResponsibilityNo"]);                        
                        oTransaction.SubClaimNo = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["nSubClaimNo"]);                                                                                                                      
                        oTransaction.MainClaimNo = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sMainClaimNo"]);                        
                        oTransaction.IsRebill = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bIsRebilled"]);
                        oTransaction.IllnessDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nIllnessDate"]);
                        oTransaction.DelayReasonCodeID = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sDelayReasonCodeID"]).Trim();
                        oTransaction.ServiceAuthExceCode = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sServiceAuthExceCode"]).Trim();                                                                             
                        oTransaction.IsSameAsBillingProvider = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bSameAsBillingProvider"]);
                        oTransaction.providerQualifier = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sProviderQualifier"]);                        
                        oTransaction.ReferalProviderID_New = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nReferralProviderID"]);                        
                        oTransaction.IsReplacementClaim = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bIsReplacementClaim"]);                        
                        oTransaction.Box19NoteDescription = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sClaim19BoxNote"]).Trim();
                        if (Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["dtInitTreatmentDate"]).Trim() != "")
                        {
                            oTransaction.InitialTreatmentDate = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["dtInitTreatmentDate"]).Trim()));
                        }
                        else
                        {
                            oTransaction.InitialTreatmentDate = "";
                        }
                        oTransaction.EPSDTScreening = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bIsEPSDTScreening"]);
                        oTransaction.EPSDTReferral = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bIsEPSDTReferral"]);
                        oTransaction.ReferralType = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sReferralType"]);
                        oTransaction.bIsRefProvAsSupervisor = Convert.ToBoolean(dsTransaction.Tables["ClaimData"].Rows[0]["bIsRefProvAsSupervisor"]);
                        oTransaction.LastSeenDate = Convert.ToInt64(dsTransaction.Tables["ClaimData"].Rows[0]["nLastSeenDate"]);

                        if (Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["dtBox15Date"]).Trim() != "")
                        {
                            oTransaction.Box15Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["dtBox15Date"]).Trim()));
                        }
                        else
                        {
                            oTransaction.Box15Date = "";
                        }
                        oTransaction.Box15DateQualifier=dsTransaction.Tables["ClaimData"].Rows[0]["sBox15DateQualifier"].ToString().Trim();
                        oTransaction.sBox14DateQualifier = dsTransaction.Tables["ClaimData"].Rows[0]["sBox14DateQualifier"].ToString().Trim();
                        oTransaction.nICDRevision = Convert.ToInt16(dsTransaction.Tables["ClaimData"].Rows[0]["nICDRevision"]);
                        oTransaction.ClaimCLIANo = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["ClaimCLIANumber"]);
                        oTransaction.sMammogramCertNumber = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["MammogramCertNumber"]);
                        oTransaction.sIDENo = Convert.ToString(dsTransaction.Tables["ClaimData"].Rows[0]["sIDENo"]);
                    }
                }


                // -----------------  Claim Patient Data -----------------
                if (dsTransaction.Tables["ClaimPatient"] != null)
                {
                    if (dsTransaction.Tables["ClaimPatient"].Rows.Count > 0)
                    {
                            oTransaction.PatientFirstName=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientFirstName"]);
                            oTransaction.PatientMiddleName = Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientMiddleName"]);
                            oTransaction.PatientLastName=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientLastName"]);
                            oTransaction.PatientAddress1=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientAddress1"]);
                            oTransaction.PatientCity=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientCity"]);
                            oTransaction.PatientState=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientState"]);
                            oTransaction.PatientZip=Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientZip"]);
                            oTransaction.PatientDOB=Convert.ToDateTime(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientDOB"]);
                            oTransaction.PatientGender = Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientGender"]);
                            oTransaction.SOF = Convert.ToBoolean(dsTransaction.Tables["ClaimPatient"].Rows[0]["SOF"]);
                            oTransaction.PatientCountry = Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientCountry"]);
                            oTransaction.PatientSSN = Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientSSN"]);
                            oTransaction.PatientCode = Convert.ToString(dsTransaction.Tables["ClaimPatient"].Rows[0]["PatientCode"]);                                 
                    }
                }
                
               // ---------------- ----- Prior Authorization Number  ----------------------
                if (dsTransaction.Tables["ClaimPriorAuthorization"] != null)
                {
                    oTransaction.PriorAuthorizationNo = "";
                    if (dsTransaction.Tables["ClaimPriorAuthorization"].Rows.Count > 0)
                    {
                        oTransaction.PriorAuthorizationNo = Convert.ToString(dsTransaction.Tables["ClaimPriorAuthorization"].Rows[0]["sPriorAuthorizationNo"]);
                    }                   
                }
                    

                //-------------------- Transaction Lines Data ----------------               
                DataRow[] drClaimLineNote=null;
                GeneralNoteEDI oLineNote = null;
                if (dsTransaction.Tables["ClaimLines"] != null)
                {
                    if (dsTransaction.Tables["ClaimLines"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsTransaction.Tables["ClaimLines"].Rows.Count; i++)
                        {

                            oLine = new TransactionLineEDI();
                            oLine.TransactionId = TransactionID;
                            oLine.TransactionLineId = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nTransactionLineNo"]);
                            oLine.TransactionDetailID = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nTransactionDetailID"]);
                            oLine.TransactionMasterID = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nTransactionMasterID"]);  
                            oLine.TransactionMasterDetailID = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nTransactionMasterDetailID"]);  
                            oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                            if (dsTransaction.Tables["ClaimLines"].Rows[i]["nToDate"] != null && Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nToDate"]) > 0)
                            {
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nToDate"]));
                            }
                            else
                            {
                                oLine.DateServiceTillIsNull = true;
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                            }
                            oLine.POSCode = dsTransaction.Tables["ClaimLines"].Rows[i]["sPOSCode"].ToString();
                            oLine.CPTCode = dsTransaction.Tables["ClaimLines"].Rows[i]["sCPTCode"].ToString();
                            oLine.CPTDescription = dsTransaction.Tables["ClaimLines"].Rows[i]["sCPTDescription"].ToString();
                            oLine.CrosswalkCPTCode = dsTransaction.Tables["ClaimLines"].Rows[i]["sCrossWalkCPTCode"].ToString();
                            oLine.Dx1Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sDx1Code"].ToString();
                            oLine.Dx2Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sDx2Code"].ToString();
                            oLine.Dx3Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sDx3Code"].ToString();
                            oLine.Dx4Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sDx4Code"].ToString();
                            oLine.Mod1Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sMod1Code"].ToString();                            
                            oLine.Mod2Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sMod2Code"].ToString();                            
                            oLine.Mod3Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sMod3Code"].ToString();                            
                            oLine.Mod4Code = dsTransaction.Tables["ClaimLines"].Rows[i]["sMod4Code"].ToString();                            
                            oLine.Charges = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["dCharges"]);
                            oLine.BilledAmount = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["dBilliedAmount"]);
                            oLine.Unit = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["dUnit"]);
                            oLine.Total = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["dTotal"]);
                            oLine.AllowedCharges = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["dAllowed"]);                        
                            oLine.ClinicID = ClinicID;                                                       
                            oLine.IsLabCPT = Convert.ToBoolean(dsTransaction.Tables["ClaimLines"].Rows[i]["bIsLabCPT"]);
                            oLine.AuthorizationNo = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sAuthorizationNo"]);                                               
                            oLine.IsLineSplitted = Convert.ToBoolean(dsTransaction.Tables["ClaimLines"].Rows[i]["bIsSplitted"]);                                                                           
                            oLine.EMG = Convert.ToBoolean(dsTransaction.Tables["ClaimLines"].Rows[i]["bEMG"]);                            
                            oLine.NDCID = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nNDCID"]);
                            oLine.NDCCodeQualifier = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sNDCCodeQualifier"]);
                            oLine.NDCCode = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sNDCCode"]);                                                    
                            oLine.NDCUnit = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sNDCUnit"]);
                            oLine.NDCUnitCode = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sNDCUnitCode"]);
                            oLine.NDCUnitPricing = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sNDCUnitPricing"]);
                            oLine.Prescription = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["Prescription"]);                      
                            oLine.RevenueCode = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sRevenueCode"]);
                            oLine.RenderingProviderId = Convert.ToInt64(dsTransaction.Tables["ClaimLines"].Rows[i]["nProvider"]);
                            oLine.PrescriptionDesc = Convert.ToString(dsTransaction.Tables["ClaimLines"].Rows[i]["sPrescriptionDesc"]);
                            oLine.FamilyPlanningIndicator = Convert.ToBoolean(dsTransaction.Tables["ClaimLines"].Rows[i]["bIsFamilyPlanningIndicator"]);
                            oLine.ServiceResultofScreening = Convert.ToBoolean(dsTransaction.Tables["ClaimLines"].Rows[i]["bIsServiceResultofScreening"]);
                            oLine.AnesthesiaUnit = Convert.ToDecimal(dsTransaction.Tables["ClaimLines"].Rows[i]["AnesthesiaUnit"]);                                                                                                           

                            drClaimLineNote = dsTransaction.Tables["ClaimLineNotes"].Select("nTransactionDetailID=" + dsTransaction.Tables["ClaimLines"].Rows[i]["nTransactionMasterDetailID"]);
                            if (drClaimLineNote != null && drClaimLineNote.GetUpperBound(0) >= 0)
                            {                               
                                for (int j = 0; j < drClaimLineNote.Length; j++)
                                {
                                    oLineNote = new GeneralNoteEDI();
                                    oLineNote.TransactionID = TransactionID;
                                    oLineNote.TransactionLineId = Convert.ToInt64(drClaimLineNote[j]["nLineNo"]);
                                    oLineNote.NoteID = Convert.ToInt64(drClaimLineNote[j]["nNoteId"]);
                                    oLineNote.NoteDescription = Convert.ToString(drClaimLineNote[j]["sNoteDescription"]);
                                    oLineNote.BillingNoteType = (EOBPaymentSubType)(drClaimLineNote[j]["nBillingNoteType"]);
                                    oLine.LineNotes.Add(oLineNote);
                                    if (oLineNote != null)
                                    { oLineNote.Dispose(); }
                                }
                            }
                                                                         
                            oTransaction.Lines.Add(oLine);
                        }
                    }
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
                //oTransaction.Dispose();
                oDBParameters.Dispose();
                if (oDB != null)
                {                    
                    oDB.Dispose();
                }
                if (dsTransaction != null)
                {
                    dsTransaction.Tables.Clear();
                    dsTransaction.Dispose();
                    dsTransaction = null;
                }
            }
            return oTransaction;
        }

        #endregion

    }


   //////////////////////////     Transaction Object ///////////////////////////////////////////////

    public class TransactionEDI
    {
        #region "Constructor & Destructor"

        public TransactionEDI()
        {
            _Lines = new TransactionLinesEDI();
            SubClaimNo = "";
            MainClaimNo = "";                      
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
                    _Lines.Dispose();
                }
            }
            disposed = true;
        }

        ~TransactionEDI()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _IllnessDate;
        private TransactionLinesEDI _Lines;

        #endregion " Declarations "

        #region Property Procedures of Transaction Class

        public Int64 TransactionID { get; set; }

        public Int64 OnsiteDate { get; set; }

        public String sBox14DateQualifier { get; set; }

        public Int64 InjuryDate { get; set; }

        public Int64 UnableToWorkFromDate { get; set; }

        public Int64 UnableToWorkTillDate { get; set; }

        public Int64 TransactionDate { get; set; }

        public Int64 PatientID { get; set; }

        public Int64 ProviderID { get; set; }

        public String ProviderName { get; set; }       

        public Int64 ClaimNo { get; set; }

        public Int64 ClinicID { get; set; }

        public TransactionLinesEDI Lines
        {
                get { return _Lines; }
                set { _Lines = value; }
        }

        public string MaritalStatus { get; set; }

        public string FacilityCode { get; set; }

        public string FacilityDescription { get; set; }

        public Int64 HospitalizationDateFrom { get; set; }

        public Int64 HospitalizationDateTo { get; set; }              

        public Int64 RefferingProviderId { get; set; }

        public bool AutoClaim { get; set; }

        public Int64 AccidentDate { get; set; }

        public bool OtherAccident { get; set; }

        public Int64 OtherAccidentDate { get; set; }

        public bool WorkersComp { get; set; }

        public string WorkersCompNo { get; set; }    

        public Int64 TransactionMasterID { get; set; }

        public Int64 ContactID { get; set; }

        public Int16 ResponsibilityNo { get; set; }

        public String SubClaimNo { get; set; }                   

        public Boolean IsRebill { get; set; }

        public Boolean IsReplacementClaim { get; set; }     

        public Boolean IsSameAsBillingProvider { get; set; }

        public Int64 ReferalProviderID_New { get; set; }     

        public int NoOfServiceLine { get; set; }

        public int NoOfDiagnosis { get; set; }

        public String DelayReasonCodeID { get; set; }

        public String ServiceAuthExceCode { get; set; }
   

        public String MainClaimNo { get; set; }

        public string Box19NoteDescription { get; set; }

        public Boolean bIsRefProvAsSupervisor { get; set; }
        public string providerQualifier { get; set; }

        public Int64 LastSeenDate { get; set; }
       
        public String ClaimNumber
        {
            get
            {
                if (SubClaimNo.Trim() != "" && SubClaimNo.Contains("-") == false)
                {
                    return (FormattedClaimNumberGeneration(ClaimNo.ToString()) + '-' + SubClaimNo.ToString());
                }
                else
                {
                    if (SubClaimNo.Contains("-") == true && MainClaimNo != String.Empty)
                    { return (FormattedClaimNumberGeneration(ClaimNo.ToString()) + '-' + MainClaimNo.ToString()); }
                    else
                    { return FormattedClaimNumberGeneration(ClaimNo.ToString()); }
                }
            }
        }
     
        public Int64 IllnessDate
        { get { return _IllnessDate; } set { _IllnessDate = value; } }

        public string State { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientMiddleName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientAddress1{ get; set; }

        public string PatientCity{ get; set; }

        public string PatientState{ get; set; }

        public string PatientZip{ get; set; }

        public string PatientCountry{ get; set; }

        public DateTime PatientDOB { get; set; }

        public string PatientGender{ get; set; }

        public bool SOF { get; set; }

        public string PriorAuthorizationNo { get; set; }

        public string ClaimCLIANo { get; set; }

        public string sMammogramCertNumber { get; set; }

        public string PatientSSN { get; set; }

        public string PatientCode { get; set; }

        public string InitialTreatmentDate { get; set; }

        public string Box15Date { get; set; }
        public string Box15DateQualifier { get; set; }
        public int nICDRevision { get; set; }


        public bool EPSDTScreening { get; set; }

        public bool EPSDTReferral { get; set; }

        public string ReferralType { get; set; }

        public string sIDENo { get; set; }

        #endregion

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
               // NumberSize = NumberSize;
            }
            return NumberSize;
        }

    }

    public class TransactionLineEDI
    {
        #region " Constructor & Destructor "

        private bool disposed = false;

        public TransactionLineEDI()
        {
            _generalNotes = new GeneralNotesEDI();
        }

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
                    _generalNotes.Dispose();
                }
            }
            disposed = true;
        }

        ~TransactionLineEDI()
        {
            Dispose(false);
        }

        #endregion " Constructor & Destructor "

        #region " Private Variables "

        private GeneralNotesEDI _generalNotes = null;

        #endregion " Private Variables "

        #region " Public Property Procedures "

        public string CaseNo { get; set; }

        public Int64 TransactionId { get; set; }

        public Int64 TransactionLineId { get; set; }

        public Int64 TransactionDetailID { get; set; }

        public Int64 TransactionMasterID { get; set; }

        public Int64 TransactionMasterDetailID { get; set; }

        public DateTime DateServiceFrom { get; set; }

        public DateTime DateServiceTill { get; set; }

        public bool DateServiceFromIsNull { get; set; }

        public bool DateServiceTillIsNull { get; set; }

        public string CPTCode { get; set; }

        public string CPTDescription { get; set; }

        public string CrosswalkCPTCode { get; set; }

        public string CrosswalkCPTDescription { get; set; }

        public string Dx1Code { get; set; }

        public string Dx2Code { get; set; }

        public string Dx3Code { get; set; }

        public string Dx4Code { get; set; }

        public string Mod1Code { get; set; }      

        public string Mod2Code { get; set; }       

        public string Mod3Code { get; set; }        

        public string Mod4Code { get; set; }        

        public decimal Charges { get; set; }

        public decimal BilledAmount { get; set; }

        public decimal Unit { get; set; }

        public decimal Total { get; set; }

        public decimal AllowedCharges { get; set; }

        public GeneralNotesEDI LineNotes
        {
            get { return _generalNotes; }
            set { _generalNotes = value; }
        }

        public Int64 ClinicID { get; set; }

        public Int64 InsuranceID { get; set; }

        public bool IsLabCPT { get; set; }

        public Boolean IsLineSplitted { get; set; }      

        public string RevenueCode { get; set; }

        public string AuthorizationNo { get; set; }

        public Int64 RenderingProviderId { get; set; }

        public string POSCode { get; set; }

        public string NDCCode { get; set; }

        public Int64 NDCID { get; set; }

        public String NDCCodeQualifier { get; set; }

        public String NDCUnit { get; set; }

        public String NDCUnitPricing { get; set; }

        public String NDCUnitCode { get; set; }

        public String Prescription { get; set; }

        public bool EMG { get; set; }

        public String PrescriptionDesc { get; set; }

        public bool FamilyPlanningIndicator { get; set; }

        public bool ServiceResultofScreening { get; set; }

        public decimal AnesthesiaUnit { get; set; }
        #endregion " Public Property Procedures "

    }

    public class TransactionLinesEDI
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public TransactionLinesEDI()
        {
            _innerlist = new ArrayList();
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

        ~TransactionLinesEDI()
        {
            Dispose(false);
        }

        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(TransactionLineEDI item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(TransactionLineEDI item)
        {
            bool result = false;
            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public TransactionLineEDI this[int index]
        {
            get
            { return (TransactionLineEDI)_innerlist[index]; }
        }

        public bool Contains(TransactionLineEDI item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(TransactionLineEDI item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(TransactionLineEDI[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class GeneralNoteEDI
    {
        #region "Constructor & Destructor"

        public GeneralNoteEDI()
        {
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

        ~GeneralNoteEDI()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public Int64 NoteID { get; set; }

        public Int64 TransactionID { get; set; }

        public string NoteDescription { get; set; }

        public Int64 TransactionLineId { get; set; }

        public Int32 NoteRowID { get; set; }

        public EOBPaymentSubType BillingNoteType { get; set; }

        #endregion

    }

    public class GeneralNotesEDI
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor "

        public GeneralNotesEDI()
        {
            _innerlist = new ArrayList();
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

        ~GeneralNotesEDI()
        {
            Dispose(false);
        }

        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(GeneralNoteEDI item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(GeneralNoteEDI item)
        {
            bool result = false;
            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public GeneralNoteEDI this[int index]
        {
            get
            { return (GeneralNoteEDI)_innerlist[index]; }
        }

        public bool Contains(GeneralNoteEDI item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(GeneralNoteEDI item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(GeneralNoteEDI[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }


}








