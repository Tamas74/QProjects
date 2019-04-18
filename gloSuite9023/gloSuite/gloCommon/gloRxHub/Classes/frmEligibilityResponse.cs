using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;

namespace gloRxHub
{
    public partial class frmEligibilityResponse : Form
    {
        #region " Constructors "

        public frmEligibilityResponse(String DatabaseConnectionString, Int64 PatientID, Int64 InsuranceID)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _PatientId = PatientID;
            _InsuranceId = InsuranceID;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            #endregion
        }

        public frmEligibilityResponse(String DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            #endregion
        }

        #endregion " Constructors "

        #region " Private and Public Variables "

        public string EDIReturnResult = "";
        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _InsuranceId = 0;
        private Int64 _ClinicId = 0;
        //private string _messageBoxCaption = "gloPM";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        ediDocument oEdiDoc1 = null;
        ediDataSegment oSegment = null;
        ediSchemas oSchemas = null;
        ediAcknowledgment oAck = null;
        //ediSchema oSchema = null;
        string sSefFile = "";
        string sEdiFile = "";
        string sPath = "";

        #endregion " Private and Public Variables "

        #region " Private Methods "

        private void LoadEDIObject()
        {
            try
            {

                ediDocument.Set(ref oEdiDoc1, new ediDocument());    // oEdiDoc = new ediDocument();
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSefFile = "271_X092A1.SEF";
                sEdiFile = "271.X12";
                // Disabling the internal standard reference library to makes sure that 
                // FREDI uses only the SEF file provided
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc1.GetSchemas());    //oSchemas = (ediSchemas) oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                oEdiDoc1.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)oEdiDoc1.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                oEdiDoc1.LoadSchema(sSefFile, 0);
                oEdiDoc1.LoadSchema("997_X12-4010.SEF", 0);
                //ediSchema.Set(ref oSchemas, oEdiDoc.LoadSchema("997_X12-4010.SEF", 0));	//for Ack (997) file

                // Loads EDI file and the corresponding SEF file
                //OpenFileDialog oDialog = new OpenFileDialog();
                //if (oDialog.ShowDialog() == DialogResult.OK)
                //{
                //    string _FileName = "";
                //    _FileName = oDialog.FileName;
                //    if (System.IO.File.Exists(_FileName) == true)
                //    {
                //        sEdiFile = _FileName;
                //        oEdiDoc.LoadEdi(sEdiFile);
                //    }
                //}
                oEdiDoc1 = new ediDocument();
                sEdiFile = "EligibilityResponse.X12";
                oEdiDoc1.LoadEdi(sPath + sEdiFile);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void Translate271Response()
        {
            try
            {

                string sSegmentID = "";
                string sLoopSection = "";
                //string sLXID = "";
                //string sPath = "";
                string sEntity = "";
                string Qlfr = "";

                string sRecieverID = "";
                string sSenderID = "";
                string sMemberID = "";
                string sPlanNumber = "";
                string sGroupNumber = "";
                string sFormularlyID = "";
                string sAlternativeID = "";
                string sBIN = "";
                string sPCN = "";
                string sdtEligiblityDate = "12:00:00 AM";
                bool IsDemographicChange = false;
                bool IsPharmacy = false ;
                bool IsMailOrdRx = false;

                //string strRejectionCode = "";
                //string strFollowupCode = "";

                int nArea;

                StringBuilder sValue = new StringBuilder();
                //Int32 _nArea2RowCount = 0;
                //int Area2rowIndex = 0;
                //int rowIndex = 0;
                //int i = 0;
                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                // This loop iterates though the EDI file a segment at a time
                while (oSegment != null)
                {
                    // A segment is identified by its Area number, Loop section and segment id.
                    sSegmentID = oSegment.ID;
                    sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                // map data elements of ISA segment in here

                                sValue.Append("Authorization Information Qualifier :" + oSegment.get_DataElementValue(1) + Environment.NewLine);    //Authorization Information Qualifier
                                sValue.Append("Authorization Information :" + oSegment.get_DataElementValue(2) + Environment.NewLine);    //Authorization Information
                                sValue.Append("Security Information Qualifier :" + oSegment.get_DataElementValue(3) + Environment.NewLine);    //Security Information Qualifier
                                sValue.Append("Security Information :" + oSegment.get_DataElementValue(4) + Environment.NewLine);    //Security Information
                                sValue.Append("Interchange ID Qualifier :" + oSegment.get_DataElementValue(5) + Environment.NewLine);    //Interchange ID Qualifier
                                sValue.Append("Interchange Sender ID :" + oSegment.get_DataElementValue(6) + Environment.NewLine);    //Interchange Sender ID
                                sSenderID = oSegment.get_DataElementValue(6).Trim();
                                sValue.Append("Sender ID :" + sSenderID + Environment.NewLine);    //Interchange Sender ID
                                sValue.Append("Interchange ID Qualifier :" + oSegment.get_DataElementValue(7) + Environment.NewLine);    //Interchange ID Qualifier
                                sValue.Append("Interchange Receiver ID" + oSegment.get_DataElementValue(8) + Environment.NewLine);    //Interchange Receiver ID
                                sRecieverID = oSegment.get_DataElementValue(8).Trim();
                                sValue.Append("Receiver ID" + sRecieverID + Environment.NewLine);   
                                sValue.Append("Interchange Date :" + oSegment.get_DataElementValue(9) + Environment.NewLine);    //Interchange Date
                                sValue.Append("Interchange Time :" + oSegment.get_DataElementValue(10) + Environment.NewLine);   //Interchange Time
                                sValue.Append("Interchange Control Standards Identifier :" + oSegment.get_DataElementValue(11) + Environment.NewLine);   //Interchange Control Standards Identifier
                                sValue.Append("Interchange Control Version Number :" + oSegment.get_DataElementValue(12) + Environment.NewLine);   //Interchange Control Version Number
                                sValue.Append("Interchange Control Number :" + oSegment.get_DataElementValue(13) + Environment.NewLine);   //Interchange Control Number
                                sValue.Append("Acknowledgment Requested :" + oSegment.get_DataElementValue(14) + Environment.NewLine);   //Acknowledgment Requested
                                sValue.Append("Usage Indicator :" + oSegment.get_DataElementValue(15) + Environment.NewLine);   //Usage Indicator
                                sValue.Append("Component Element Separator :" + oSegment.get_DataElementValue(16) + Environment.NewLine);   //Component Element Separator

                            }
                            else if (sSegmentID == "GS")
                            {
                                // map data elements of GS segment in here
                                sValue.Append("Functional Identifier Code :" + oSegment.get_DataElementValue(1) + Environment.NewLine);  //Functional Identifier Code
                                sValue.Append("Application Sender's Code :" + oSegment.get_DataElementValue(2) + Environment.NewLine);  //Application Sender's Code
                                sValue.Append("Application Receiver's Code :" + oSegment.get_DataElementValue(3) + Environment.NewLine);  //Application Receiver's Code
                                sValue.Append("Date :" + oSegment.get_DataElementValue(4) + Environment.NewLine);  //Date
                                sValue.Append("Time :" + oSegment.get_DataElementValue(5) + Environment.NewLine);  //Time
                                sdtEligiblityDate = oSegment.get_DataElementValue(4).Trim() + " " + oSegment.get_DataElementValue(5).Trim();
                                sValue.Append("Eligiblity Date : " + sdtEligiblityDate + Environment.NewLine);
                                sValue.Append("Group Control Number :" + oSegment.get_DataElementValue(6) + Environment.NewLine);  //Group Control Number
                                sValue.Append("Responsible Agency Code :" + oSegment.get_DataElementValue(7) + Environment.NewLine);  //Responsible Agency Code
                                sValue.Append("Version :" + oSegment.get_DataElementValue(8));  //Version / Release
                            }
                        }
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                // map data element of ST segment in here
                                sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
                                sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine); //00021
                            }
                            else if (sSegmentID == "BHT")
                            {

                            }
                            else if (sSegmentID == "REF")
                            {

                            }
                        }

                    }//Area ==1

                    else if (nArea == 2)
                    {
                        if (sLoopSection == "HL" && sSegmentID == "HL")
                        {
                            sEntity = oSegment.get_DataElementValue(3);
                        }
                        if (sEntity == "20")
                        {
                            if (sLoopSection == "HL")
                            {
                                if (sSegmentID == "HL")
                                {

                                }
                                else if (sSegmentID == "AAA")
                                {

                                }

                            }//end loop section HL

                            else if (sLoopSection == "HL;NM1")
                            {

                                if (sSegmentID == "NM1")
                                {

                                }
                                else if (sSegmentID == "REF")
                                {

                                }
                                else if (sSegmentID == "PER")
                                {


                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);//
                                    }
                                }

                            }//end loop section HL;NM1

                        }


                        else if (sEntity == "21")
                        {
                            if (sLoopSection == "HL")
                            {

                                if (sSegmentID == "HL")
                                {
                                    sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                            }

                            else if (sLoopSection == "HL;NM1")
                            {

                                if (sSegmentID == "NM1")
                                {
                                    sValue.Append("Payer :" + oSegment.get_DataElementValue(1) + Environment.NewLine);//PR=Payer
                                    sValue.Append("Non-Person Entity :" + oSegment.get_DataElementValue(2) + Environment.NewLine);//2=Non-Person Entity
                                    sValue.Append("Information Source Name :" + oSegment.get_DataElementValue(3) + Environment.NewLine);//"INFORMATION SOURCE NAME" );//"PBM"
                                    sValue.Append("Payer Identification :" + oSegment.get_DataElementValue(8) + Environment.NewLine);//PI=Payer Identification
                                    sValue.Append("PayerID :" + oSegment.get_DataElementValue(9) + Environment.NewLine);//PayerID
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);
                                        //txtProviderInfo.Text = GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
                                        //txtProviderResponse.Text = GetReceiverFollowUp(oSegment.get_DataElementValue(4));
                                        //EDIReturnResult = EDIReturnResult + txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim();
                                        //AddNote(_PatientId, txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim());
                                    }
                                }
                            }

                        }
                        else if (sEntity == "22")
                        {
                            if (sLoopSection == "HL")
                            {
                                if (sSegmentID == "HL")
                                {
                                    sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                                else if (sSegmentID == "TRN")
                                {
                                    sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                            }

                            else if (sLoopSection == "HL;NM1")
                            {
                                if (sSegmentID == "NM1")
                                {
                                    sValue.Append("Subscriber Last Name : " + oSegment.get_DataElementValue(7) + Environment.NewLine);
                                    sValue.Append("Subscriber First Name : " + oSegment.get_DataElementValue(8) + Environment.NewLine);
                                    sValue.Append("Subscriber ID: " + oSegment.get_DataElementValue(9) + Environment.NewLine);

                                }
                                else if (sSegmentID == "N3")
                                {
                                    sValue.Append("Subscriber Address : " + oSegment.get_DataElementValue(1) + Environment.NewLine);

                                }
                                else if (sSegmentID == "N4")
                                {
                                    sValue.Append("Subscriber City : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append("Subscriber State : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append("Subscriber Zip : " + oSegment.get_DataElementValue(3) + Environment.NewLine);

                                }
                                else if (sSegmentID == "PER")
                                {

                                }
                                else if (sSegmentID == "REF")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                    if (Qlfr == "18")
                                    {
                                        sPlanNumber = oSegment.get_DataElementValue(2);
                                        sValue.Append("Plan Number : " + sPlanNumber + Environment.NewLine);
                                    }
                                    else if (Qlfr == "1W")
                                    {
                                        sMemberID = oSegment.get_DataElementValue(2);
                                        sValue.Append("Member ID : " + sMemberID + Environment.NewLine);
                                    }
                                    else if (Qlfr == "6P")
                                    {
                                        sGroupNumber = oSegment.get_DataElementValue(2);
                                        sValue.Append("Group Number : " + sGroupNumber + Environment.NewLine);
                                    }
                                    else if (Qlfr == "IF")
                                    {
                                        sFormularlyID = oSegment.get_DataElementValue(2);
                                        sValue.Append("Formulary ID : " + sFormularlyID + Environment.NewLine);
                                        sAlternativeID = oSegment.get_DataElementValue(3);
                                        sValue.Append("Alternative ID : " + sAlternativeID + Environment.NewLine);
                                    }
                                    else if (Qlfr == "N6")
                                    {
                                        sBIN = oSegment.get_DataElementValue(2);
                                        sValue.Append("BIN : " + sBIN + Environment.NewLine);
                                        sPCN = oSegment.get_DataElementValue(3);
                                        sValue.Append("PCN : " + sPCN + Environment.NewLine);
                                    }

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Payer Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Payer Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);
                                        //txtSubscriberInfo.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                        //txtSubscriberResponse.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(4));
                                        //EDIReturnResult = EDIReturnResult + txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim();
                                        //AddNote(_PatientId, txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim());
                                    }
                                }
                                else if (sSegmentID == "DMG")
                                {
                                    sValue.Append("Subscriber Demographic Information : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append("Subscriber Date of Birth : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append("Subscriber Gender : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                                else if (sSegmentID == "INS")
                                {
                                    if (oSegment.get_DataElementValue(1) == "Y")
                                    {
                                        IsDemographicChange = true;
                                        sValue.Append("INS : " + "Y" + Environment.NewLine);
                                    }
                                    else
                                    {
                                        IsDemographicChange = false;
                                        sValue.Append("INS : " + "N" + Environment.NewLine);
                                    }
                                }
                                else if (sSegmentID == "DTP")
                                {
                                    sValue.Append("Subscriber Service : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
                                    sValue.Append("Subscriber Date : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    sValue.Append("Subscriber Service Date : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }
                                if (sSegmentID == "EB")
                                {
                                    //Only if the Qlfr =1 tht means pahrmacy has active coverage for claim then make the IsPharmacy flag to true,
                                    //else keep it false

                                    if (Qlfr == "1")//Active Coverage
                                    {
                                        //listResponse.Items.Add("Active Coverage: " + oSegment.get_DataElementValue(3).Trim());
                                        if (oSegment.get_DataElementValue(3) == "88" )
                                        {
                                            IsPharmacy = true;
                                            sValue.Append("Pharmacy : " + "1" + Environment.NewLine);
                                        }
                                        if (oSegment.get_DataElementValue(3) == "90")
                                        {
                                            IsMailOrdRx = true;
                                            sValue.Append("Mail Order Prescription : " + "1" + Environment.NewLine);
                                        }
                                    }
                                    else if (Qlfr == "6")//Inactive
                                    {
                                        //listResponse.Items.Add("Co-Payment: " + oSegment.get_DataElementValue(1).Trim());
                                        if (oSegment.get_DataElementValue(3).Trim() == "88" )
                                        {
                                            IsPharmacy = false;
                                            sValue.Append("Pharmacy : " + "6" + Environment.NewLine);
                                        }
                                        if (oSegment.get_DataElementValue(3) == "90")
                                        {
                                            IsMailOrdRx = false;
                                            sValue.Append("Mail Order Prescription : " + "6" + Environment.NewLine);
                                        }
                                    }
                                    else if (Qlfr == "G")//Out Of Pocket (Stop Loss)
                                    {
                                        //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
                                        if (oSegment.get_DataElementValue(3).Trim() == "88")
                                        {
                                            IsPharmacy = false;
                                            sValue.Append("Pharmacy : " + "G" + Environment.NewLine);
                                        }
                                        if (oSegment.get_DataElementValue(3) == "90")
                                        {
                                            IsMailOrdRx = false;
                                            sValue.Append("Mail Order Prescription : " + "G" + Environment.NewLine);
                                        }
                                    }
                                    else if (Qlfr == "V")//Cannot Process
                                    {
                                        //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
                                        if (oSegment.get_DataElementValue(3).Trim() == "88")
                                        {
                                            IsPharmacy = false;
                                            sValue.Append("Pharmacy : " + "V" + Environment.NewLine);
                                        }
                                        if (oSegment.get_DataElementValue(3) == "90")
                                        {
                                            IsMailOrdRx = false;
                                            sValue.Append("Mail Order Prescription : " + "V" + Environment.NewLine);
                                        }
                                    }
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        listResponse.Items.Add("Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                    AddNote(_PatientId, EDIReturnResult);
                                }
                                else if (sSegmentID == "REF")
                                {

                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }

                        }
                    }
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                }
                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.

                // Gets the first segment of the 997 acknowledgment file
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                while (oSegment != null)
                {
                    nArea = oSegment.Area;
                    sLoopSection = oSegment.LoopSection;
                    sSegmentID = oSegment.ID;

                    if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "AK9")
                            {
                                if (oSegment.get_DataElementValue(1, 0) == "R")
                                {
                                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                }
                            }
                        }	// sLoopSection == ""
                    }	//nArea == 1
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                }	//oSegment != null

                //save the acknowledgment
                string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "997_277\\";
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.Save(sDirectoryPath + "997_270.X12");

                if (sValue.Length > 0)
                {
                    //listResponse.Items.Add(sValue);
                    rchtxtbxRead271.Text = sValue.ToString();


                    //insert the data to the EDIResponse271 Table
                    InsertEDIResponse271(_PatientId,"0",sSenderID,sRecieverID,sdtEligiblityDate,sMemberID,sPlanNumber,sGroupNumber,sFormularlyID,sAlternativeID,sBIN,sPCN,IsDemographicChange,IsPharmacy,IsMailOrdRx,"");

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

      

        private void Translate271Response_OLD()
        {
            try
            {

                string sSegmentID = "";
                string sLoopSection = "";
                //string sLXID = "";
                //string sPath = "";
                string sEntity = "";
                string Qlfr = "";

                //string strRejectionCode = "";
                //string strFollowupCode = "";

                int nArea;

                string sValue = "";
                //Int32 _nArea2RowCount = 0;
                //int Area2rowIndex = 0;
                //int rowIndex = 0;
                //int i = 0;
                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                // This loop iterates though the EDI file a segment at a time
                while (oSegment != null)
                {
                    // A segment is identified by its Area number, Loop section and segment id.
                    sSegmentID = oSegment.ID;
                    sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                // map data elements of ISA segment in here

                                sValue = oSegment.get_DataElementValue(1);    //Authorization Information Qualifier
                                sValue = oSegment.get_DataElementValue(2);    //Authorization Information
                                sValue = oSegment.get_DataElementValue(3);    //Security Information Qualifier
                                sValue = oSegment.get_DataElementValue(4);    //Security Information
                                sValue = oSegment.get_DataElementValue(5);    //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(6);    //Interchange Sender ID
                                sValue = oSegment.get_DataElementValue(7);    //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(8);    //Interchange Receiver ID
                                sValue = oSegment.get_DataElementValue(9);    //Interchange Date
                                sValue = oSegment.get_DataElementValue(10);   //Interchange Time
                                sValue = oSegment.get_DataElementValue(11);   //Interchange Control Standards Identifier
                                sValue = oSegment.get_DataElementValue(12);   //Interchange Control Version Number
                                sValue = oSegment.get_DataElementValue(13);   //Interchange Control Number
                                sValue = oSegment.get_DataElementValue(14);   //Acknowledgment Requested
                                sValue = oSegment.get_DataElementValue(15);   //Usage Indicator
                                sValue = oSegment.get_DataElementValue(16);   //Component Element Separator

                            }
                            else if (sSegmentID == "GS")
                            {
                                // map data elements of GS segment in here
                                sValue = oSegment.get_DataElementValue(1);  //Functional Identifier Code
                                sValue = oSegment.get_DataElementValue(2);  //Application Sender's Code
                                sValue = oSegment.get_DataElementValue(3);  //Application Receiver's Code
                                sValue = oSegment.get_DataElementValue(4);  //Date
                                sValue = oSegment.get_DataElementValue(5);  //Time
                                sValue = oSegment.get_DataElementValue(6);  //Group Control Number
                                sValue = oSegment.get_DataElementValue(7);  //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8);  //Version / Release
                            }
                        }
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                // map data element of ST segment in here
                                sValue = oSegment.get_DataElementValue(1);
                                sValue = oSegment.get_DataElementValue(2);
                            }
                            else if (sSegmentID == "BHT")
                            {

                            }
                            else if (sSegmentID == "REF")
                            {

                            }
                        }

                    }//Area ==1

                    else if (nArea == 2)
                    {
                        if (sLoopSection == "HL" && sSegmentID == "HL")
                        {
                            sEntity = oSegment.get_DataElementValue(3);
                        }
                        if (sEntity == "20")
                        {
                            if (sLoopSection == "HL")
                            {
                                if (sSegmentID == "HL")
                                {

                                }
                                else if (sSegmentID == "AAA")
                                {

                                }

                            }//end loop section HL

                            else if (sLoopSection == "HL;NM1")
                            {

                                if (sSegmentID == "NM1")
                                {

                                }
                                else if (sSegmentID == "REF")
                                {

                                }
                                else if (sSegmentID == "PER")
                                {


                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);//
                                    }
                                }

                            }//end loop section HL;NM1

                        }


                        else if (sEntity == "21")
                        {
                            if (sLoopSection == "HL")
                            {

                                if (sSegmentID == "HL")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                            }

                            else if (sLoopSection == "HL;NM1")
                            {

                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);
                                        //txtProviderInfo.Text = GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
                                        //txtProviderResponse.Text = GetReceiverFollowUp(oSegment.get_DataElementValue(4));
                                        //EDIReturnResult = EDIReturnResult + txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim();
                                        //AddNote(_PatientId, txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim());
                                    }
                                }
                            }

                        }
                        else if (sEntity == "22")
                        {
                            if (sLoopSection == "HL")
                            {
                                if (sSegmentID == "HL")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "TRN")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                            }

                            else if (sLoopSection == "HL;NM1")
                            {
                                if (sSegmentID == "NM1")
                                {

                                }
                                else if (sSegmentID == "N3")
                                {

                                }
                                else if (sSegmentID == "N4")
                                {

                                }
                                else if (sSegmentID == "PER")
                                {

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            listResponse.Items.Add("Payer Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            listResponse.Items.Add("Payer Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        AddNote(_PatientId, EDIReturnResult);
                                        //txtSubscriberInfo.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                        //txtSubscriberResponse.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(4));
                                        //EDIReturnResult = EDIReturnResult + txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim();
                                        //AddNote(_PatientId, txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim());
                                    }
                                }
                                else if (sSegmentID == "DMG")
                                {

                                }
                                else if (sSegmentID == "INS")
                                {

                                }
                                else if (sSegmentID == "DTP")
                                {

                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }
                                if (sSegmentID == "EB")
                                {
                                    if (Qlfr == "1")//Active Coverage
                                    {
                                        listResponse.Items.Add("Active Coverage: " + oSegment.get_DataElementValue(1).Trim());
                                    }
                                    else if (Qlfr == "B")//Co-payment
                                    {
                                        listResponse.Items.Add("Co-Payment: " + oSegment.get_DataElementValue(1).Trim());
                                    }
                                    else if (Qlfr == "C")//Deductible
                                    {
                                        listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
                                    }
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        listResponse.Items.Add("Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                    AddNote(_PatientId, EDIReturnResult);
                                }
                                else if (sSegmentID == "REF")
                                {

                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }

                        }
                    }
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                }
                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.

                // Gets the first segment of the 997 acknowledgment file
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                while (oSegment != null)
                {
                    nArea = oSegment.Area;
                    sLoopSection = oSegment.LoopSection;
                    sSegmentID = oSegment.ID;

                    if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "AK9")
                            {
                                if (oSegment.get_DataElementValue(1, 0) == "R")
                                {
                                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                }
                            }
                        }	// sLoopSection == ""
                    }	//nArea == 1
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                }	//oSegment != null

                //save the acknowledgment
                string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "997_277\\";
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.Save(sDirectoryPath + "997_270.X12");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        public Int64 AddNote(Int64 PatientID, string Notes)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {

                oDBParameters.Clear();
                object _intresult = 0;
                oDBParameters.Add("@nPNotesID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nTime", gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sNotes", Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nNotesType", 1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);//Enum will be taken later 0=None, 1=EDI
                oDBParameters.Add("@nClinicID", _ClinicId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                int result = oDB.Execute("gsp_INUP_Patient_Notes", oDBParameters, out  _intresult);


                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Add, "Add notes from Eligibility Response", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                    }
                }
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, DBErr.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }


        public Int64 InsertEDIResponse271(Int64 PatientID, string TranCntrlReqID,string SenderID, string RecieverID,string dtEligiblityDate, string MemberID,string PlanNumber,string GroupNumber,string FormularyID, string AlternativeID, string BIN,string PCN, bool IsdemographicChange, bool IsPharmacy,bool IsMORx,string ResponseType)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                                 

                oDBParameters.Clear();
                object _intresult = 0;
                oDBParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionControlID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sTransactionControlRequestID", TranCntrlReqID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sSenderID", SenderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRecieverID", RecieverID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@dtEligiblityDate", dtEligiblityDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sMemberID", MemberID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//Enum will be taken later 0=None, 1=EDI
                oDBParameters.Add("@sPlanNumber", PlanNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGroupNumber", GroupNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sFormularyID", FormularyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAlternativeID", AlternativeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BIN", BIN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PCN", PCN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsDemographicChange", IsdemographicChange, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsPharmacy", IsPharmacy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsMailOrdRx", IsMORx, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sResponseType", ResponseType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                int result = oDB.Execute("gsp_InUpEDIResp271_1", oDBParameters, out  _intresult);


                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Add, "Add notes from Eligibility Response", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                    }
                }
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, DBErr.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        private string GetSourceRejectionReason(string _RejectCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strRejectionReason = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_SourceRejectionCode Where sCode='" + _RejectCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strRejectionReason = Convert.ToString(_result);
                }
                else
                {
                    strRejectionReason = "";
                }

                return strRejectionReason;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetSourceFollowUp(string _FollowUpCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strFollowupDesc = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_SourceFollowupCode Where sCode='" + _FollowUpCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strFollowupDesc = Convert.ToString(_result);
                }
                else
                {
                    strFollowupDesc = "";
                }

                return strFollowupDesc;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetReceiverRejectionReason(string _RejectCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strRejectionReason = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_ReceiverRejectionCode Where sCode='" + _RejectCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strRejectionReason = Convert.ToString(_result);
                }
                else
                {
                    strRejectionReason = "";
                }

                return strRejectionReason;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetReceiverFollowUp(string _FollowUpCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strFollowupDesc = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_ReceiverFollowupCode Where sCode='" + _FollowUpCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strFollowupDesc = Convert.ToString(_result);
                }
                else
                {
                    strFollowupDesc = "";
                }

                return strFollowupDesc;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetSubscriberRejectionReason(string _RejectCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strRejectionReason = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_SubscriberRejectionCode Where sCode='" + _RejectCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strRejectionReason = Convert.ToString(_result);
                }
                else
                {
                    strRejectionReason = "";
                }

                return strRejectionReason;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetSubscriberFollowUp(string _FollowUpCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Object _result = null;
            string strFollowupDesc = "";
            string strSQL = "";
            try
            {
                strSQL = "SELECT sDescription FROM BL_SubscriberFollowupCode Where sCode='" + _FollowUpCode + "'";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                if (_result != null)
                {
                    strFollowupDesc = Convert.ToString(_result);
                }
                else
                {
                    strFollowupDesc = "";
                }

                return strFollowupDesc;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        #endregion " Private Methods "


        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnCheckResponse_Click(object sender, EventArgs e)
        {
            Translate271Response();
        }

        private void frmEligibilityResponse_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEDIObject();
                Translate271Response();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }
    }
}