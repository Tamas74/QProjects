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
    public partial class frmEligiblityResponse_new : Form
    {

         
        //for Subscriber information C1 flex grid 
        private int COL_INFORMATIONsOURCENAME = 0;
        private int COL_INFORMATIONRECIEVERNAME = 1;
        private int COL_PBM_PAYERSOURCENAME = 2;
        private int COL_PAYERPARTICIPANTID = 3;
        private int COL_PBMPAYERMEMBERID= 4;
        private int COL_HEALTHPLANNAME = 5;
        private int COL_CARDHOLDERID = 6;
        private int COL_CARDHOLDERNAME = 7;
        private int COL_GROUPID = 8;
        private int COL_GROUPNAME = 9;
        private int COL_FORMULARLYLISTID = 10;
        private int COL_ALTERNATIVELISTID = 11;
        private int COL_COVERAGEID = 12;
        private int COL_BINNUMBER = 13;
        private int COL_COPAYID= 14;
        private int COL_ISDEMOGRAPHICCHANGED = 15;
        private int COL_ISPHARMACYELIGIBLE = 16;
        private int COL_PHARMACYCOVERAGENAME = 17;
        private int COL_PHELIGIBLITYBENEFITINFO = 18;
        private int COL_ISMAILORDRXDRUGELIGIBLE = 19;
        private int COL_MAILORDRDDRUGCOVERAGENAME = 20;
        private int COL_MAILORDRXDRUGELIGIBLITYORBENEFITINFO = 21;
        private int COL_DEPENDANTLASTNAME = 22;
        private int COL_DEPENDANTFIRSTNAME = 23;
        private int COL_DEPENDANTMIDDLENAME = 24;
        private int COL_DEPENDANTSTATE = 25;
        private int COL_DEPENDANTZIP = 26;
        private int COL_DEPENDANTDOB = 27;
        private int COL_DEPENDANTGENDER = 28;
        private int COL_DEPENDANTADDRESS1= 29;
        private int COL_DEPENDANTADDRESS2 = 30;

        private int COL_271INFOCOUNT = 31;


        private int COL_SUBSCRIBERLASTNAME = 0;
        private int COL_SUBSCRIBERFIRSTNAME = 1;
        private int COL_SUBSCRIBERMIDDLENAME = 2;
        private int COL_SUBSCRIBERCITY = 3;
        private int COL_SUBSCRIBERSTATE = 4;
        private int COL_SUBSCRIBERZIP = 5;
        private int COL_SUBSCRIBERDOB = 6;
        private int COL_SUBSCRIBERGENDER = 7;
        private int COL_SUBSCRIBERADDRESS1 = 8;
        private int COL_SUBSCRIBERADDRESS2 = 9;

        private int COL_SUBSCRIBERCOUNT = 10;


        #region " Constructors "
        public frmEligiblityResponse_new(String DatabaseConnectionString, Int64 PatientID, Int64 InsuranceID, string EDI271FilePath, ClsPatient objPatient)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _PatientId = PatientID;
            _InsuranceId = InsuranceID;
            _EDI271FilePath = EDI271FilePath;
            oclsPatient = objPatient;
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

        public frmEligiblityResponse_new(String DatabaseConnectionString)
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
        ClsPatient oclsPatient = new ClsPatient();
        ClsRxHubInterface oClsRxHubInterface ;
        public string EDIReturnResult = "";
        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _InsuranceId = 0;
        private Int64 _ClinicId = 0;
        //private string _messageBoxCaption = "gloPM";
        private string _EDI271FilePath = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        ediDocument oEdiDoc1 = null;
        //ediDataSegment oSegment = null;
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
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc1.GetSchemas());  //oSchemas = (ediSchemas) oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                oEdiDoc1.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)oEdiDoc1.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgmentoAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

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
               // sEdiFile = "EligibilityResponse.X12";
                //sEdiFile = "SingleCoverage.X12";
                //sEdiFile = "MultipleCoverage.X12";
                //sEdiFile = "PayeDependantatPayer.X12";
                //sEdiFile = "PatientNotFound.X12";
                sEdiFile = "ErrorAtPBM.X12";
                //sEdiFile = "270OUTPUT.X12";
                oEdiDoc1.LoadEdi(sPath + sEdiFile);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

//        private void Translate271Responce_Anil()
//        {
//            ediDocument oEdiDoc = null;
//            ediSchemas oSchemas = null;
//            ediDataSegment oSegment = null;
//            string sSegmentID;
//            string sLoopSection;
//            int nArea;
//            string sValue;

//            //CREATES EDIDOC OBJECT
//            ediDocument.Set(ref oEdiDoc, new ediDocument());

//            //THIS MAKES CERTAIN THAT FREDI ONLY USES THE SEF FILE PROVIDED, AND 

//THAT IT DOES 
//            //NOT USE ITS BUILT-IN STANDARD REFERENCE TABLE TO TRANSLATE THE EDI 

//FILE.
//            ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());  

////oSchemas = (ediSchemas) oEdiDoc.GetSchemas();
//            oSchemas.EnableStandardReference = false;

//            //THIS OPTIONS STOPS FREDI FROM KEEPING ALL THE SEGMENTS IN MEMORY
//            oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

//            //LOADS THE SEF FILE
//            oEdiDoc.ImportSchema("271_X092A1.SEF", 0);

//            //LOADS THE EDI FILE
//            oEdiDoc.LoadEdi("271 Multiple Coverage.X12");

//            //GETS THE FIRST DATA SEGMENT
//            ediDataSegment.Set(ref oSegment, 

//(ediDataSegment)oEdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) 

//oEdiDoc.FirstDataSegment

//            //LOOP THAT WILL TRAVERSE THRU EDI FILE FROM TOP TO BOTTOM
//            while (oSegment != null)
//            {
//                //DATA SEGMENTS WILL BE IDENTIFIED BY THEIR ID, THE LOOP SECTION 

//AND AREA
//                //(OR TABLE) NUMBER THAT THEY ARE IN.
//                sSegmentID = oSegment.ID;
//                sLoopSection = oSegment.LoopSection;
//                nArea = oSegment.Area;

//                if (nArea == 0)
//                {
//                    if (sLoopSection == "")
//                    {
//                        if (sSegmentID == "ISA")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Authorization Information Qualifier
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Authorization Information
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Security Information Qualifier
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Security Information
//                            sValue = oSegment.get_DataElementValue(5, 0);     

////Interchange ID Qualifier
//                            sValue = oSegment.get_DataElementValue(6, 0);     

////Interchange Sender ID
//                            sValue = oSegment.get_DataElementValue(7, 0);     

////Interchange ID Qualifier
//                            sValue = oSegment.get_DataElementValue(8, 0);     

////Interchange Receiver ID
//                            sValue = oSegment.get_DataElementValue(9, 0);     

////Interchange Date
//                            sValue = oSegment.get_DataElementValue(10, 0);     

////Interchange Time
//                            sValue = oSegment.get_DataElementValue(11, 0);     

////Interchange Control Standards Identifier
//                            sValue = oSegment.get_DataElementValue(12, 0);     

////Interchange Control Version Number
//                            sValue = oSegment.get_DataElementValue(13, 0);     

////Interchange Control Number
//                            sValue = oSegment.get_DataElementValue(14, 0);     

////Acknowledgment Requested
//                            sValue = oSegment.get_DataElementValue(15, 0);     

////Usage Indicator
//                            sValue = oSegment.get_DataElementValue(16, 0);     

////Component Element Separator
//                        }
//                        else if (sSegmentID == "GS")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Functional Identifier Code
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Application Sender's Code
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Application Receiver's Code
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Date
//                            sValue = oSegment.get_DataElementValue(5, 0);     

////Time
//                            sValue = oSegment.get_DataElementValue(6, 0);     

////Group Control Number
//                            sValue = oSegment.get_DataElementValue(7, 0);     

////Responsible Agency Code
//                            sValue = oSegment.get_DataElementValue(8, 0);     

////Version / Release / Industry Identifier Code
//                        }   //sSegmentID
//                    }    //sLoopSection
//                }
//                else if (nArea == 1)
//                {
//                    if (sLoopSection == "")
//                    {
//                        if (sSegmentID == "ST")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Transaction Set Identifier Code
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Transaction Set Control Number
//                        }
//                        else if (sSegmentID == "BHT")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Hierarchical Structure Code
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Transaction Set Purpose Code
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Reference Identification
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Date
//                            sValue = oSegment.get_DataElementValue(5, 0);     

////Time
//                        }   //sSegmentID
//                    }    //sLoopSection
//                }
//                else if (nArea == 2)
//                {
//                    if (sLoopSection == "HL")
//                    {
//                        //if loop has more that one instance, then you should 

//check for the qualifier that differentiates the loop instances here e.g.
//                        //if (sSegmentID == "HL")
//                        //{
//                        //   sLoopHLQlfr = oSegment.DataElementValue(3)   //In 

//most cases the loop qualifier is the first element of the first segment in the 

//loop, but not necessarily
//                        // }
//                        //if (sLoopHLQlfr == "QualifierValue")
//                        //{

//                        if (sSegmentID == "HL")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Hierarchical ID Number
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Hierarchical Parent ID Number
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Hierarchical Level Code
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Hierarchical Child Code
//                        }   //Segment ID
//                    }
//                    else if (sLoopSection == "HL;NM1")
//                    {
//                        //if loop has more that one instance, then you should 

//check for the qualifier that differentiates the loop instances here e.g.
//                        //if (sSegmentID == "NM1")
//                        //{
//                        //   sLoopQlfr = oSegment.DataElementValue(1)   //In most 

//cases the loop qualifier is the first element of the first segment in the loop, 

//but not necessarily
//                        //}
//                        //if (sLoopQlfr == "QualifierValue") 
//                        //{

//                        if (sSegmentID == "NM1")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Entity Identifier Code
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Entity Type Qualifier
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Name Last or Organization Name
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Name First
//                            sValue = oSegment.get_DataElementValue(5, 0);     

////Name Middle
//                            sValue = oSegment.get_DataElementValue(6, 0);     

////Name Prefix
//                            sValue = oSegment.get_DataElementValue(7, 0);     

////Name Suffix
//                            sValue = oSegment.get_DataElementValue(8, 0);     

////Identification Code Qualifier
//                            sValue = oSegment.get_DataElementValue(9, 0);     

////Identification Code
//                        }
//                        else if (sSegmentID == "REF")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Reference Identification Qualifier
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Reference Identification
//                        }
//                        else if (sSegmentID == "N3")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Address Information
//                        }
//                        else if (sSegmentID == "N4")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////City Name
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////State or Province Code
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Postal Code
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Country Code
//                        }
//                        else if (sSegmentID == "DMG")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Date Time Period Format Qualifier
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Date Time Period
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Gender Code
//                        }
//                        else if (sSegmentID == "INS")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Yes/No Condition or Response Code
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Individual Relationship Code
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Maintenance Type Code
//                            sValue = oSegment.get_DataElementValue(4, 0);     

////Maintenance Reason Code
//                            sValue = oSegment.get_DataElementValue(5, 0);     

////Benefit Status Code
//                            sValue = oSegment.get_DataElementValue(6, 0);     

////Medicare Plan Code
//                            sValue = oSegment.get_DataElementValue(7, 0);     

////Consolidated Omnibus Budget Reconciliation Act (COBRA) Qualify
//                            sValue = oSegment.get_DataElementValue(8, 0);     

////Employment Status Code
//                            sValue = oSegment.get_DataElementValue(9, 0);     

////Student Status Code
//                            sValue = oSegment.get_DataElementValue(10, 0);     

////Yes/No Condition or Response Code
//                        }
//                        else if (sSegmentID == "DTP")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Date/Time Qualifier
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Date Time Period Format Qualifier
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Date Time Period
//                        }   //Segment ID
//                    }
//                    else if (sLoopSection == "HL;NM1;EB")
//                    {
//                        if (sSegmentID == "EB")
//                        {
//                            sValue = oSegment.get_DataElementValue(1, 0);     

////Eligibility or Benefit Information
//                            sValue = oSegment.get_DataElementValue(2, 0);     

////Coverage Level Code
//                            sValue = oSegment.get_DataElementValue(3, 0);     

////Service Type Code
//                        }   //sSegmentID
//                    }   //sLoopSection
//                }   //nArea

//                //GETS THE NEXT DATA SEGMENT
//                ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next()); 

// //oSegment = (ediDataSegment) oSegment.Next();

//            }   //while

//        }


        //private void Translate271Response()
        //{
        //    try
        //    {
        //        int STLoopCnt = 0;

        //        string sSegmentID = "";
        //        string sLoopSection = "";
        //        string sLXID = "";
        //        string sPath = "";
        //        string sEntity = "";
        //        string Qlfr = "";

        //        string sRecieverID = "";
        //        string sSenderID = "";
        //        string sMemberID = "";
        //        string sPlanNumber = "";
        //        string sGroupNumber = "";
        //        string sFormularlyID = "";
        //        string sAlternativeID = "";
        //        string sBIN = "";
        //        string sPCN = "";
        //        string sdtEligiblityDate = "12:00:00 AM";
        //        bool IsDemographicChange = false;
        //        bool IsPharmacy = false;
        //        bool IsMailOrdRx = false;

        //        string strRejectionCode = "";
        //        string strFollowupCode = "";

        //        int nArea;

        //        StringBuilder sValue = new StringBuilder();
        //        //Int32 _nArea2RowCount = 0;
        //        //int Area2rowIndex = 0;
        //        //int rowIndex = 0;
        //        //int i = 0;
        //        // Gets the first data segment in the EDI files
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment)oEdiDoc.FirstDataSegment

        //        // This loop iterates though the EDI file a segment at a time
        //        while (oSegment != null)
        //        {
        //            // A segment is identified by its Area number, Loop section and segment id.
        //            sSegmentID = oSegment.ID;
        //            sLoopSection = oSegment.LoopSection;
        //            nArea = oSegment.Area;

        //            if (nArea == 0)
        //            {
        //                if (sLoopSection == "")
        //                {
        //                    if (sSegmentID == "ISA")
        //                    {
        //                        // map data elements of ISA segment in here

        //                        sValue.Append("Authorization Information Qualifier :" + oSegment.get_DataElementValue(1) + Environment.NewLine);    //Authorization Information Qualifier
        //                        sValue.Append("Authorization Information :" + oSegment.get_DataElementValue(2) + Environment.NewLine);    //Authorization Information
        //                        sValue.Append("Security Information Qualifier :" + oSegment.get_DataElementValue(3) + Environment.NewLine);    //Security Information Qualifier
        //                        sValue.Append("Security Information :" + oSegment.get_DataElementValue(4) + Environment.NewLine);    //Security Information
        //                        sValue.Append("Interchange ID Qualifier :" + oSegment.get_DataElementValue(5) + Environment.NewLine);    //Interchange ID Qualifier
        //                        sValue.Append("Interchange Sender ID :" + oSegment.get_DataElementValue(6) + Environment.NewLine);    //Interchange Sender ID
        //                        sSenderID = oSegment.get_DataElementValue(6).Trim();sValue.Append("Sender ID :" + sSenderID + Environment.NewLine);    //Interchange Sender ID
        //                        sValue.Append("Interchange ID Qualifier :" + oSegment.get_DataElementValue(7) + Environment.NewLine);    //Interchange ID Qualifier
        //                        sValue.Append("Interchange Receiver ID" + oSegment.get_DataElementValue(8) + Environment.NewLine);    //Interchange Receiver ID
        //                        sRecieverID = oSegment.get_DataElementValue(8).Trim();
        //                        sValue.Append("Receiver ID" + sRecieverID + Environment.NewLine);
        //                        sValue.Append("Interchange Date :" + oSegment.get_DataElementValue(9) + Environment.NewLine);    //Interchange Date
        //                        sValue.Append("Interchange Time :" + oSegment.get_DataElementValue(10) + Environment.NewLine);   //Interchange Time
        //                        sValue.Append("Interchange Control Standards Identifier :" + oSegment.get_DataElementValue(11) + Environment.NewLine);  //Interchange Control Standards Identifier
        //                        sValue.Append("Interchange Control Version Number :" + oSegment.get_DataElementValue(12) + Environment.NewLine);   //Interchange Control Version Number
        //                        sValue.Append("Interchange Control Number :" + oSegment.get_DataElementValue(13) + Environment.NewLine);   //Interchange Control Number
        //                        sValue.Append("Acknowledgment Requested :" + oSegment.get_DataElementValue(14) + Environment.NewLine);   //Acknowledgment Requested
        //                        sValue.Append("Usage Indicator :" + oSegment.get_DataElementValue(15) + Environment.NewLine);   //Usage Indicator
        //                        sValue.Append("Component Element Separator :" + oSegment.get_DataElementValue(16) + Environment.NewLine);   //Component Element Separator

        //                    }
        //                    else if (sSegmentID == "GS")
        //                    {
        //                        // map data elements of GS segment in here
        //                        sValue.Append("Functional Identifier Code :" + oSegment.get_DataElementValue(1) + Environment.NewLine);  //Functional Identifier Code
        //                        sValue.Append("Application Sender's Code :" + oSegment.get_DataElementValue(2) + Environment.NewLine);  //Application Sender's Code
        //                        sValue.Append("Application Receiver's Code :" + oSegment.get_DataElementValue(3) + Environment.NewLine);  //Application Receiver's Code
        //                        sValue.Append("Date :" + oSegment.get_DataElementValue(4) + Environment.NewLine);  //Date
        //                        sValue.Append("Time :" + oSegment.get_DataElementValue(5) + Environment.NewLine);  //Time
        //                        sdtEligiblityDate = oSegment.get_DataElementValue(4).Trim() + " " + oSegment.get_DataElementValue(5).Trim();
        //                        sValue.Append("Eligiblity Date : " + sdtEligiblityDate + Environment.NewLine);
        //                        sValue.Append("Group Control Number :" + oSegment.get_DataElementValue(6) + Environment.NewLine);  //Group Control Number
        //                        sValue.Append("Responsible Agency Code :" + oSegment.get_DataElementValue(7) + Environment.NewLine);  //Responsible Agency Code
        //                        sValue.Append("Version :" + oSegment.get_DataElementValue(8));  //Version / Release
        //                    }
        //                }
        //            }
        //            else if (nArea == 1)
        //            {
        //                if (sLoopSection == "")
        //                {
        //                    if (sSegmentID == "ST")
        //                    {
        //                        // map data element of ST segment in here
        //                        sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                        sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine); //00021
        //                        STLoopCnt = STLoopCnt + 1;
        //                    }
        //                    else if (sSegmentID == "BHT")
        //                    {

        //                    }
        //                    else if (sSegmentID == "REF")
        //                    {

        //                    }
        //                }

        //            }//Area ==1

        //            else if (nArea == 2)
        //            {
        //                if (sLoopSection == "HL" && sSegmentID == "HL")
        //                {
        //                    sEntity = oSegment.get_DataElementValue(3);
        //                }
        //                //****************************** Information Source 
        //                if (sEntity == "20")
        //                {
        //                    if (sLoopSection == "HL")
        //                    {
        //                        if (sSegmentID == "HL")
        //                        {

        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {

        //                        }

        //                    }//end loop section HL

        //                    else if (sLoopSection == "HL;NM1")
        //                    {

        //                        if (sSegmentID == "NM1")
        //                        {
        //                            txtInformationSourceName.Text = oSegment.get_DataElementValue(3);//Information Source Name
        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {

        //                        }
        //                        else if (sSegmentID == "PER")
        //                        {


        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            if (oSegment.get_DataElementValue(1) == "N")
        //                            {
        //                                sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                                if (oSegment.get_DataElementValue(3).Trim() != "")
        //                                {
        //                                    //listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
        //                                    //listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
        //                                }

        //                                EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                                //AddNote(_PatientId, EDIReturnResult);//
        //                            }
        //                        }

        //                    }//end loop section HL;NM1

        //                }

        //                    //**************************** Information Reciever - physician
        //                else if (sEntity == "21")
        //                {
        //                    if (sLoopSection == "HL")
        //                    {

        //                        if (sSegmentID == "HL")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                    }

        //                    else if (sLoopSection == "HL;NM1")
        //                    {

        //                        if (sSegmentID == "NM1")
        //                        {
        //                            txtInformationRecieverName.Text = oSegment.get_DataElementValue(3);//Information Reciever Name
        //                            sValue.Append("Payer :" + oSegment.get_DataElementValue(1) + Environment.NewLine);//PR=Payer
        //                            sValue.Append("Non-Person Entity :" + oSegment.get_DataElementValue(2) + Environment.NewLine);//2=Non-Person Entity
        //                            sValue.Append("Information Source Name :" + oSegment.get_DataElementValue(3) + Environment.NewLine);//"INFORMATION SOURCE NAME" );//"PBM"
        //                            txtPBMPayerName.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                            sValue.Append("Payer Identification :" + oSegment.get_DataElementValue(8) + Environment.NewLine);//PI=Payer Identification
        //                            sValue.Append("PayerID :" + oSegment.get_DataElementValue(9) + Environment.NewLine);//PayerID
        //                            txtPayerParticipantId.Text = oSegment.get_DataElementValue(9);//++++++++++++++++++++
        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {
                                    
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
                                     
        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            if (oSegment.get_DataElementValue(1) == "N")
        //                            {
        //                                sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                                if (oSegment.get_DataElementValue(3).Trim() != "")
        //                                {
        //                                    //listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
        //                                    //listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
        //                                }

        //                                EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                                //AddNote(_PatientId, EDIReturnResult);
        //                                //txtProviderInfo.Text = GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
        //                                //txtProviderResponse.Text = GetReceiverFollowUp(oSegment.get_DataElementValue(4));
        //                                //EDIReturnResult = EDIReturnResult + txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim();
        //                                //AddNote(_PatientId, txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim());
        //                            }
        //                        }
        //                    }

        //                }
        //                    //*****************************Subscriber loop
        //                else if (sEntity == "22")
        //                {
        //                    if (sLoopSection == "HL")
        //                    {
        //                        if (sSegmentID == "HL")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                        else if (sSegmentID == "TRN")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                    }

        //                    else if (sLoopSection == "HL;NM1")
        //                    {
        //                        if (sSegmentID == "NM1")
        //                        {
                                 
        //                            sValue.Append("Subscriber Last Name : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            txtSubscriberLastName.Text = oSegment.get_DataElementValue(3);//Subscriber Last Name
        //                            sValue.Append("Subscriber First Name : " + oSegment.get_DataElementValue(4) + Environment.NewLine);
        //                            txtSubscriberFirstName.Text = oSegment.get_DataElementValue(4);//Subscriber First Name
        //                            txtSubscriberMiddleName.Text = oSegment.get_DataElementValue(5);//Subscriber Middle Name
        //                            sValue.Append("Subscriber ID: " + oSegment.get_DataElementValue(9) + Environment.NewLine);
        //                            txtPBMPayerMemberId.Text = oSegment.get_DataElementValue(9);
        //                        }
        //                        else if (sSegmentID == "N3")
        //                        {
        //                            sValue.Append("Subscriber Address : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            txtSubscriberAddressLine1.Text = oSegment.get_DataElementValue(1);//Subscriber Address Line1
        //                            txtSubscriberAddressLine2.Text = oSegment.get_DataElementValue(2);//Subscriber Address Line2
        //                        }
        //                        else if (sSegmentID == "N4")
        //                        {
        //                            sValue.Append("Subscriber City : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            txtSubscriberCity.Text = oSegment.get_DataElementValue(1);//Subscriber City
        //                            sValue.Append("Subscriber State : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            txtSubscriberState.Text = oSegment.get_DataElementValue(2);//Subscriber State
        //                            sValue.Append("Subscriber Zip : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            txtSubscriberZip.Text = oSegment.get_DataElementValue(3);//Subscriber Zip

        //                        }
        //                        else if (sSegmentID == "PER")
        //                        {

        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {
        //                            Qlfr = oSegment.get_DataElementValue(1);
        //                            //If returned, health plan name in the REF03 must be displayed in the end user application per RxHub application guideline 3.6.
        //                            if (Qlfr == "18")
        //                            {
        //                                sPlanNumber = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Plan Number : " + sPlanNumber + Environment.NewLine);
        //                                txtHealthPlanName.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                            }
        //                            //If returned, cardholder ID in the REF02 and cardholder name in the REF03 will be used to populate drug history request and new prescription transactions.
        //                            else if (Qlfr == "1W")
        //                            {
        //                                sMemberID = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Member ID : " + sMemberID + Environment.NewLine);
        //                                txtCardHolderId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                txtCardHolderName.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                            }
        //                            //If returned, group ID in the REF02 will be used to populate drug history request and new prescription transactions.
        //                            else if (Qlfr == "6P")
        //                            {
        //                                sGroupNumber = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Group Number : " + sGroupNumber + Environment.NewLine);
        //                                txtGroupId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                            }
        //                            //If returned, formulary list ID in the REF02 and alternative list ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "IF")
        //                            {
        //                                sFormularlyID = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Formulary ID : " + sFormularlyID + Environment.NewLine);
        //                                txtFormularyListId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                sAlternativeID = oSegment.get_DataElementValue(3);
        //                                sValue.Append("Alternative ID : " + sAlternativeID + Environment.NewLine);
        //                                txtAlternativeListId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                            }
        //                            //If returned, coverage ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "1L")
        //                            {
        //                                //sFormularlyID = oSegment.get_DataElementValue(2);
        //                                //sValue.Append("Formulary ID : " + sFormularlyID + Environment.NewLine);
        //                                txtCoverageId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++

        //                            }
        //                            //If returned, BIN number in the REF02 will be used to populate new prescription transactions.
        //                            else if (Qlfr == "N6")
        //                            {
        //                                sBIN = oSegment.get_DataElementValue(2);
        //                                sValue.Append("BIN : " + sBIN + Environment.NewLine);
        //                                txtBinNumber.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                //sPCN = oSegment.get_DataElementValue(3);
        //                                //sValue.Append("PCN : " + sPCN + Environment.NewLine);
        //                            }
        //                            //If returned, copay ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "IG")
        //                            {
        //                                //sBIN = oSegment.get_DataElementValue(2);
        //                                //sValue.Append("BIN : " + sBIN + Environment.NewLine);
        //                                txtCopayId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                                //sPCN = oSegment.get_DataElementValue(3);
        //                                //sValue.Append("PCN : " + sPCN + Environment.NewLine);
        //                            }

        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            if (oSegment.get_DataElementValue(1) == "N")
        //                            {
        //                                sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                                if (oSegment.get_DataElementValue(3).Trim() != "")
        //                                {
        //                                    //listResponse.Items.Add("Payer Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
        //                                    //listResponse.Items.Add("Payer Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
        //                                }

        //                                EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                                AddNote(_PatientId, EDIReturnResult);
        //                                //txtSubscriberInfo.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
        //                                //txtSubscriberResponse.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(4));
        //                                //EDIReturnResult = EDIReturnResult + txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim();
        //                                //AddNote(_PatientId, txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim());
        //                            }
        //                        }
        //                        else if (sSegmentID == "DMG")
        //                        {
        //                            sValue.Append("Subscriber Demographic Information : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                           sValue.Append("Subscriber Date of Birth : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                           txtSubscriberDOB.Text = oSegment.get_DataElementValue(2);//Subscriber Date of birth
        //                            sValue.Append("Subscriber Gender : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            txtSubscriberGender.Text = oSegment.get_DataElementValue(3);//Subscriber Gender
        //                        }
        //                        else if (sSegmentID == "INS")
        //                        {
        //                            //If INS03 is populated with ‘001’ and INS04 is populated with ‘25’, some patient demographic information returned in the 271 response differs from what was submitted in the 270 request
        //                            if (oSegment.get_DataElementValue(3) == "001" && oSegment.get_DataElementValue(4) == "25")
        //                            {
        //                                txtIsDemographicsChanged.Text = "True";
        //                            }
        //                            else 
        //                            {
        //                                txtIsDemographicsChanged.Text = "False";
        //                            }

        //                            //if (oSegment.get_DataElementValue(1) == "Y")
        //                            //{
        //                            //    IsDemographicChange = true;
        //                            //    sValue.Append("INS : " + "Y" + Environment.NewLine);
        //                            //}
        //                            //else
        //                            //{
        //                            //    IsDemographicChange = false;
        //                            //    sValue.Append("INS : " + "N" + Environment.NewLine);
        //                            //}
        //                        }
        //                        else if (sSegmentID == "DTP")
        //                        {
        //                            sValue.Append("Subscriber Service : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append("Subscriber Date : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append("Subscriber Service Date : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                    }

        //                    else if (sLoopSection == "HL;NM1;EB")
        //                    {
        //                        if (sSegmentID == "EB")
        //                        {
        //                            Qlfr = oSegment.get_DataElementValue(1);
        //                        }
        //                        if (sSegmentID == "EB")
        //                        {
        //                            //Only if the Qlfr =1 tht means pahrmacy has active coverage for claim then make the IsPharmacy flag to true,
        //                            //else keep it false

        //                            if (Qlfr == "1")//Active Coverage
        //                            {
        //                                //listResponse.Items.Add("Active Coverage: " + oSegment.get_DataElementValue(3).Trim());
        //                                if (oSegment.get_DataElementValue(3) == "88")
        //                                {
        //                                    IsPharmacy = true;
        //                                    sValue.Append("Pharmacy : " + "1" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = true;
        //                                    sValue.Append("Mail Order Prescription : " + "1" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "6")//Inactive
        //                            {
        //                                //listResponse.Items.Add("Co-Payment: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "6" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "6" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "G")//Out Of Pocket (Stop Loss)
        //                            {
        //                                //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "G" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "G" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "V")//Cannot Process
        //                            {
        //                                //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "V" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "V" + Environment.NewLine);
        //                                }
        //                            }
        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            if (oSegment.get_DataElementValue(3).Trim() != "")
        //                            {
        //                                //listResponse.Items.Add("Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
        //                                //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
        //                            }

        //                            EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                            AddNote(_PatientId, EDIReturnResult);
        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {

        //                        }

        //                    }
        //                    else if (sLoopSection == "HL;NM1;EB;III")
        //                    {
        //                        if (sSegmentID == "III")
        //                        {

        //                        }
        //                    }

        //                }
        //                //*************************** Depandant Loop
        //                else if (sEntity == "23")
        //                {
        //                    if (sLoopSection == "HL")
        //                    {
        //                        if (sSegmentID == "HL")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                        else if (sSegmentID == "TRN")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(2)+ Environment.NewLine);
        //                            sValue.Append(oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                    }

        //                    else if (sLoopSection == "HL;NM1")
        //                    {
        //                        if (sSegmentID == "NM1")
        //                        {

        //                            //sValue.Append("Subscriber Last Name : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            //sValue.Append("Subscriber First Name : " + oSegment.get_DataElementValue(4) + Environment.NewLine);
        //                            //sValue.Append("Subscriber ID: " + oSegment.get_DataElementValue(9) + Environment.NewLine);
        //                            txtDependantLastName.Text = oSegment.get_DataElementValue(3);//Dependant Last Name
        //                           txtDependantFirstName.Text = oSegment.get_DataElementValue(4);//Dependant First Name
        //                           txtDependantMiddleName.Text = oSegment.get_DataElementValue(5);//Dependant Middle Name

        //                        }
        //                        else if (sSegmentID == "N3")
        //                        {
        //                            //sValue.Append("Subscriber Address : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            txtDependantAddressLine1.Text = oSegment.get_DataElementValue(1);//Dependant Address line1
        //                            txtDependantAddressLine2.Text = oSegment.get_DataElementValue(2);//Dependant Address line2
        //                        }
        //                        else if (sSegmentID == "N4")
        //                        {
        //                            //sValue.Append("Subscriber City : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            //sValue.Append("Subscriber State : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            //sValue.Append("Subscriber Zip : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            txtDependantCity.Text = oSegment.get_DataElementValue(1);//Dependant City
        //                            txtDependantState.Text = oSegment.get_DataElementValue(2);//Dependant State
        //                            txtDependantZip.Text = oSegment.get_DataElementValue(3);//Dependant Zip
        //                        }
        //                        else if (sSegmentID == "PER")
        //                        {

        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {
        //                            Qlfr = oSegment.get_DataElementValue(1);
        //                            //If returned, health plan name in the REF03 must be displayed in the end user application per RxHub application guideline 3.6.
        //                            if (Qlfr == "18")
        //                            {
        //                                sPlanNumber = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Plan Number : " + sPlanNumber + Environment.NewLine);
        //                                txtHealthPlanName.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++++++++++
        //                            }
        //                            //If returned, cardholder ID in the REF02 and cardholder name in the REF03 will be used to populate drug history request and new prescription transactions.
        //                            else if (Qlfr == "1W")
        //                            {
        //                                sMemberID = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Member ID : " + sMemberID + Environment.NewLine);
        //                                txtCardHolderId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                txtCardHolderName.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                            }
        //                            //If returned, group ID in the REF02 will be used to populate drug history request and new prescription transactions.
        //                            else if (Qlfr == "6P")
        //                            {
        //                                sGroupNumber = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Group Number : " + sGroupNumber + Environment.NewLine);
        //                                txtGroupId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                            }
        //                            //If returned, formulary list ID in the REF02 and alternative list ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "IF")
        //                            {
        //                                sFormularlyID = oSegment.get_DataElementValue(2);
        //                                sValue.Append("Formulary ID : " + sFormularlyID + Environment.NewLine);
        //                                txtFormularyListId.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                sAlternativeID = oSegment.get_DataElementValue(3);
        //                                sValue.Append("Alternative ID : " + sAlternativeID + Environment.NewLine);
        //                                txtAlternativeListId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                            }
        //                            //If returned, coverage ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "1L")
        //                            {
        //                                //sFormularlyID = oSegment.get_DataElementValue(2);
        //                                //sValue.Append("Formulary ID : " + sFormularlyID + Environment.NewLine);
        //                                txtCoverageId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
                                      
        //                            }
        //                            //If returned, BIN number in the REF02 will be used to populate new prescription transactions.
        //                            else if (Qlfr == "N6")
        //                            {
        //                                sBIN = oSegment.get_DataElementValue(2);
        //                                sValue.Append("BIN : " + sBIN + Environment.NewLine);
        //                                txtBinNumber.Text = oSegment.get_DataElementValue(2);//++++++++++++++++++++
        //                                //sPCN = oSegment.get_DataElementValue(3);
        //                                //sValue.Append("PCN : " + sPCN + Environment.NewLine);
        //                            }
        //                            //If returned, copay ID in the REF03 will be used to link to patient formulary and benefit data.
        //                            else if (Qlfr == "IG")
        //                            {
        //                                //sBIN = oSegment.get_DataElementValue(2);
        //                                //sValue.Append("BIN : " + sBIN + Environment.NewLine);
        //                                txtCopayId.Text = oSegment.get_DataElementValue(3);//++++++++++++++++++++
        //                                //sPCN = oSegment.get_DataElementValue(3);
        //                                //sValue.Append("PCN : " + sPCN + Environment.NewLine);
        //                            }

        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            if (oSegment.get_DataElementValue(1) == "N")
        //                            {
        //                                sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                                if (oSegment.get_DataElementValue(3).Trim() != "")
        //                                {
        //                                    //listResponse.Items.Add("Payer Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
        //                                    //listResponse.Items.Add("Payer Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
        //                                }

        //                                EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                                AddNote(_PatientId, EDIReturnResult);
        //                                //txtSubscriberInfo.Text = GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
        //                                //txtSubscriberResponse.Text =GetSubscriberRejectionReason(oSegment.get_DataElementValue(4));
        //                                //EDIReturnResult = EDIReturnResult + txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim();
        //                                //AddNote(_PatientId, txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim());
        //                            }
        //                        }
        //                        else if (sSegmentID == "DMG")
        //                        {
        //                            sValue.Append("Subscriber Demographic Information : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            sValue.Append("Subscriber Date of Birth : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            txtDependantDOB.Text = oSegment.get_DataElementValue(2);//Dependant Date of birth
        //                            sValue.Append("Subscriber Gender : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                            txtDependantGender.Text = oSegment.get_DataElementValue(3);//Dependant Gender
        //                        }
        //                        else if (sSegmentID == "INS")
        //                        {
        //                            //If INS03 is populated with ‘001’ and INS04 is populated with ‘25’, some patient demographic information returned in the 271 response differs from what was submitted in the 270 request
        //                            if (oSegment.get_DataElementValue(3) == "001" && oSegment.get_DataElementValue(4) == "25")
        //                            {
        //                                txtIsDemographicsChanged.Text = "True";
        //                            }
        //                            else
        //                            {
        //                                txtIsDemographicsChanged.Text = "False";
        //                            }

        //                            //if (oSegment.get_DataElementValue(1) == "Y")
        //                            //{
        //                            //    IsDemographicChange = true;
        //                            //    sValue.Append("INS : " + "Y" + Environment.NewLine);
        //                            //}
        //                            //else
        //                            //{
        //                            //    IsDemographicChange = false;
        //                            //    sValue.Append("INS : " + "N" + Environment.NewLine);
        //                            //}
        //                        }
        //                        else if (sSegmentID == "DTP")
        //                        {
        //                            //sValue.Append("Subscriber Service : " + oSegment.get_DataElementValue(1) + Environment.NewLine);
        //                            //sValue.Append("Subscriber Date : " + oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            //sValue.Append("Subscriber Service Date : " + oSegment.get_DataElementValue(3) + Environment.NewLine);
        //                        }
        //                    }

        //                    else if (sLoopSection == "HL;NM1;EB")
        //                    {
        //                        if (sSegmentID == "EB")
        //                        {
        //                            Qlfr = oSegment.get_DataElementValue(1);
        //                        }
        //                        if (sSegmentID == "EB")
        //                        {
        //                            //Only if the Qlfr =1 tht means pahrmacy has active coverage for claim then make the IsPharmacy flag to true,
        //                            //else keep it false

        //                            if (Qlfr == "1")//Active Coverage
        //                            {
        //                                //listResponse.Items.Add("Active Coverage: " + oSegment.get_DataElementValue(3).Trim());
        //                                if (oSegment.get_DataElementValue(3) == "88")
        //                                {
        //                                    IsPharmacy = true;
        //                                    sValue.Append("Pharmacy : " + "1" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = true;
        //                                    sValue.Append("Mail Order Prescription : " + "1" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "6")//Inactive
        //                            {
        //                                //listResponse.Items.Add("Co-Payment: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "6" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "6" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "G")//Out Of Pocket (Stop Loss)
        //                            {
        //                                //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "G" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "G" + Environment.NewLine);
        //                                }
        //                            }
        //                            else if (Qlfr == "V")//Cannot Process
        //                            {
        //                                //listResponse.Items.Add("Deductibles: " + oSegment.get_DataElementValue(1).Trim());
        //                                if (oSegment.get_DataElementValue(3).Trim() == "88")
        //                                {
        //                                    IsPharmacy = false;
        //                                    sValue.Append("Pharmacy : " + "V" + Environment.NewLine);
        //                                }
        //                                if (oSegment.get_DataElementValue(3) == "90")
        //                                {
        //                                    IsMailOrdRx = false;
        //                                    sValue.Append("Mail Order Prescription : " + "V" + Environment.NewLine);
        //                                }
        //                            }
        //                        }
        //                        else if (sSegmentID == "AAA")
        //                        {
        //                            sValue.Append(oSegment.get_DataElementValue(2) + Environment.NewLine);
        //                            if (oSegment.get_DataElementValue(3).Trim() != "")
        //                            {
        //                                //listResponse.Items.Add("Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
        //                                //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
        //                            }

        //                            EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
        //                            AddNote(_PatientId, EDIReturnResult);
        //                        }
        //                        else if (sSegmentID == "REF")
        //                        {

        //                        }

        //                    }
        //                    else if (sLoopSection == "HL;NM1;EB;III")
        //                    {
        //                        if (sSegmentID == "III")
        //                        {

        //                        }
        //                    }

        //                }
        //                //***************************
        //            }
        //            ediDataSegment.Set(ref oSegment,(ediDataSegment)oSegment.Next());
        //            if (STLoopCnt > 0)
        //            { 
        //               // oClsRxHubInterface.SaveEDIResponse271(
        //            }
        //        }
        //        // Checks the 997 acknowledgment file just created.
        //        // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
        //        // to translating any other EDI file.

        //        // Gets the first segment of the 997 acknowledgment file
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

        //        while (oSegment != null)
        //        {
        //            nArea = oSegment.Area;
        //            sLoopSection = oSegment.LoopSection;
        //            sSegmentID = oSegment.ID;

        //            if (nArea == 1)
        //            {
        //                if (sLoopSection == "")
        //                {
        //                    if (sSegmentID == "AK9")
        //                    {
        //                        if (oSegment.get_DataElementValue(1, 0) == "R")
        //                        {
        //                            //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
        //                        }
        //                    }
        //                }	// sLoopSection == ""
        //            }	//nArea == 1
        //            ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
        //        }	//oSegment != null

        //        //save the acknowledgment
        //        string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory +"997_277\\";
        //        if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
        //        oAck.Save(sDirectoryPath + "997_270.X12");

        //        if (sValue.Length > 0)
        //        {
        //            //listResponse.Items.Add(sValue);
        //            //rchtxtbxRead271.Text = sValue.ToString();


        //            //insert the data to the EDIResponse271 Table
        //            //InsertEDIResponse271(_PatientId, "0", sSenderID,sRecieverID, sdtEligiblityDate, sMemberID, sPlanNumber, sGroupNumber, sFormularlyID, sAlternativeID, sBIN, sPCN, IsDemographicChange, IsPharmacy, IsMailOrdRx, "");

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

//        private void Translate271Response_OLD()
//        {
//            try
//            {

//                string sSegmentID = "";
//                string sLoopSection = "";
//                string sLXID = "";
//                string sPath = "";
//                string sEntity = "";
//                string Qlfr = "";

//                string strRejectionCode = "";
//                string strFollowupCode = "";

//                int nArea;

//                string sValue = "";
//                //Int32 _nArea2RowCount = 0;
//                //int Area2rowIndex = 0;
//                //int rowIndex = 0;
//                //int i = 0;
//                // Gets the first data segment in the EDI files
//                ediDataSegment.Set(ref oSegment, 

//(ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) 

//oEdiDoc.FirstDataSegment

//                // This loop iterates though the EDI file a segment at a time
//                while (oSegment != null)
//                {
//                    // A segment is identified by its Area number, Loop section 

//and segment id.
//                    sSegmentID = oSegment.ID;
//                    sLoopSection = oSegment.LoopSection;
//                    nArea = oSegment.Area;

//                    if (nArea == 0)
//                    {
//                        if (sLoopSection == "")
//                        {
//                            if (sSegmentID == "ISA")
//                            {
//                                // map data elements of ISA segment in here

//                                sValue = oSegment.get_DataElementValue(1);    

////Authorization Information Qualifier
//                                sValue = oSegment.get_DataElementValue(2);    

////Authorization Information
//                                sValue = oSegment.get_DataElementValue(3);    

////Security Information Qualifier
//                                sValue = oSegment.get_DataElementValue(4);    

////Security Information
//                                sValue = oSegment.get_DataElementValue(5);    

////Interchange ID Qualifier
//                                sValue = oSegment.get_DataElementValue(6);    

////Interchange Sender ID
//                                sValue = oSegment.get_DataElementValue(7);    

////Interchange ID Qualifier
//                                sValue = oSegment.get_DataElementValue(8);    

////Interchange Receiver ID
//                                sValue = oSegment.get_DataElementValue(9);    

////Interchange Date
//                                sValue = oSegment.get_DataElementValue(10);   

////Interchange Time
//                                sValue = oSegment.get_DataElementValue(11);   

////Interchange Control Standards Identifier
//                                sValue = oSegment.get_DataElementValue(12);   

////Interchange Control Version Number
//                                sValue = oSegment.get_DataElementValue(13);   

////Interchange Control Number
//                                sValue = oSegment.get_DataElementValue(14);   

////Acknowledgment Requested
//                                sValue = oSegment.get_DataElementValue(15);   

////Usage Indicator
//                                sValue = oSegment.get_DataElementValue(16);   

////Component Element Separator

//                            }
//                            else if (sSegmentID == "GS")
//                            {
//                                // map data elements of GS segment in here
//                                sValue = oSegment.get_DataElementValue(1);  

////Functional Identifier Code
//                                sValue = oSegment.get_DataElementValue(2);  

////Application Sender's Code
//                                sValue = oSegment.get_DataElementValue(3);  

////Application Receiver's Code
//                                sValue = oSegment.get_DataElementValue(4);  //Date
//                                sValue = oSegment.get_DataElementValue(5);  //Time
//                                sValue = oSegment.get_DataElementValue(6);  

////Group Control Number
//                                sValue = oSegment.get_DataElementValue(7);  

////Responsible Agency Code
//                                sValue = oSegment.get_DataElementValue(8);  

////Version / Release
//                            }
//                        }
//                    }
//                    else if (nArea == 1)
//                    {
//                        if (sLoopSection == "")
//                        {
//                            if (sSegmentID == "ST")
//                            {
//                                // map data element of ST segment in here
//                                sValue = oSegment.get_DataElementValue(1);
//                                sValue = oSegment.get_DataElementValue(2);
//                            }
//                            else if (sSegmentID == "BHT")
//                            {

//                            }
//                            else if (sSegmentID == "REF")
//                            {

//                            }
//                        }

//                    }//Area ==1

//                    else if (nArea == 2)
//                    {
//                        if (sLoopSection == "HL" && sSegmentID == "HL")
//                        {
//                            sEntity = oSegment.get_DataElementValue(3);
//                        }
//                        if (sEntity == "20")
//                        {
//                            if (sLoopSection == "HL")
//                            {
//                                if (sSegmentID == "HL")
//                                {

//                                }
//                                else if (sSegmentID == "AAA")
//                                {

//                                }

//                            }//end loop section HL

//                            else if (sLoopSection == "HL;NM1")
//                            {

//                                if (sSegmentID == "NM1")
//                                {

//                                }
//                                else if (sSegmentID == "REF")
//                                {

//                                }
//                                else if (sSegmentID == "PER")
//                                {


//                                }
//                                else if (sSegmentID == "AAA")
//                                {
//                                    if (oSegment.get_DataElementValue(1) == "N")
//                                    {
//                                        sValue = oSegment.get_DataElementValue(2);
//                                        if 

//(oSegment.get_DataElementValue(3).Trim() != "")
//                                        {
//                                            //listResponse.Items.Add("Payer 

//Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
//                                            //listResponse.Items.Add("Payer Follow 

//up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
//                                        }

//                                        EDIReturnResult = 

//oSegment.get_DataElementValue(3).Trim() + "-" + 

//oSegment.get_DataElementValue(4).Trim();
//                                        AddNote(_PatientId, EDIReturnResult);//
//                                    }
//                                }

//                            }//end loop section HL;NM1

//                        }


//                        else if (sEntity == "21")
//                        {
//                            if (sLoopSection == "HL")
//                            {

//                                if (sSegmentID == "HL")
//                                {
//                                    sValue = oSegment.get_DataElementValue(1);
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    sValue = oSegment.get_DataElementValue(3);
//                                }
//                            }

//                            else if (sLoopSection == "HL;NM1")
//                            {

//                                if (sSegmentID == "NM1")
//                                {
//                                    sValue = oSegment.get_DataElementValue(1);
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    sValue = oSegment.get_DataElementValue(3);
//                                    sValue = oSegment.get_DataElementValue(8);
//                                    sValue = oSegment.get_DataElementValue(9);
//                                }
//                                else if (sSegmentID == "REF")
//                                {
//                                    sValue = oSegment.get_DataElementValue(1);
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    sValue = oSegment.get_DataElementValue(3);
//                                }
//                                else if (sSegmentID == "AAA")
//                                {
//                                    if (oSegment.get_DataElementValue(1) == "N")
//                                    {
//                                        sValue = oSegment.get_DataElementValue(2);
//                                        if 

//(oSegment.get_DataElementValue(3).Trim() != "")
//                                        {
//                                            //listResponse.Items.Add("Receiver 

//Rejection Reason: " + 

//GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
//                                            //listResponse.Items.Add("Receiver 

//Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
//                                        }

//                                        EDIReturnResult = 

//oSegment.get_DataElementValue(3).Trim() + "-" + 

//oSegment.get_DataElementValue(4).Trim();
//                                        AddNote(_PatientId, EDIReturnResult);
//                                        //txtProviderInfo.Text = 

//GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
//                                        //txtProviderResponse.Text = 

//GetReceiverFollowUp(oSegment.get_DataElementValue(4));
//                                        //EDIReturnResult = EDIReturnResult + 

//txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim();
//                                        //AddNote(_PatientId, 

//txtProviderInfo.Text.Trim() + "-" + txtProviderResponse.Text.Trim());
//                                    }
//                                }
//                            }

//                        }
//                        else if (sEntity == "22")
//                        {
//                            if (sLoopSection == "HL")
//                            {
//                                if (sSegmentID == "HL")
//                                {
//                                    sValue = oSegment.get_DataElementValue(1);
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    sValue = oSegment.get_DataElementValue(3);
//                                }
//                                else if (sSegmentID == "TRN")
//                                {
//                                    sValue = oSegment.get_DataElementValue(1);
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    sValue = oSegment.get_DataElementValue(3);
//                                }
//                            }

//                            else if (sLoopSection == "HL;NM1")
//                            {
//                                if (sSegmentID == "NM1")
//                                {

//                                }
//                                else if (sSegmentID == "N3")
//                                {

//                                }
//                                else if (sSegmentID == "N4")
//                                {

//                                }
//                                else if (sSegmentID == "PER")
//                                {

//                                }
//                                else if (sSegmentID == "AAA")
//                                {
//                                    if (oSegment.get_DataElementValue(1) == "N")
//                                    {
//                                        sValue = oSegment.get_DataElementValue(2);
//                                        if 

//(oSegment.get_DataElementValue(3).Trim() != "")
//                                        {
//                                            //listResponse.Items.Add("Payer 

//Rejection Reason: " + 

//GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
//                                            //listResponse.Items.Add("Payer Follow 

//up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
//                                        }

//                                        EDIReturnResult = 

//oSegment.get_DataElementValue(3).Trim() + "-" + 

//oSegment.get_DataElementValue(4).Trim();
//                                        AddNote(_PatientId, EDIReturnResult);
//                                        //txtSubscriberInfo.Text = 

//GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
//                                        //txtSubscriberResponse.Text = 

//GetSubscriberRejectionReason(oSegment.get_DataElementValue(4));
//                                        //EDIReturnResult = EDIReturnResult + 

//txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim();
//                                        //AddNote(_PatientId, 

//txtSubscriberInfo.Text.Trim() + "-" + txtSubscriberResponse.Text.Trim());
//                                    }
//                                }
//                                else if (sSegmentID == "DMG")
//                                {

//                                }
//                                else if (sSegmentID == "INS")
//                                {

//                                }
//                                else if (sSegmentID == "DTP")
//                                {

//                                }
//                            }

//                            else if (sLoopSection == "HL;NM1;EB")
//                            {
//                                if (sSegmentID == "EB")
//                                {
//                                    Qlfr = oSegment.get_DataElementValue(1);
//                                }
//                                if (sSegmentID == "EB")
//                                {
//                                    if (Qlfr == "1")//Active Coverage
//                                    {
//                                        //listResponse.Items.Add("Active Coverage: 

//" + oSegment.get_DataElementValue(1).Trim());
//                                    }
//                                    else if (Qlfr == "B")//Co-payment
//                                    {
//                                        //listResponse.Items.Add("Co-Payment: " + 

//oSegment.get_DataElementValue(1).Trim());
//                                    }
//                                    else if (Qlfr == "C")//Deductible
//                                    {
//                                        //listResponse.Items.Add("Deductibles: " + 

//oSegment.get_DataElementValue(1).Trim());
//                                    }
//                                }
//                                else if (sSegmentID == "AAA")
//                                {
//                                    sValue = oSegment.get_DataElementValue(2);
//                                    if (oSegment.get_DataElementValue(3).Trim() != 

//"")
//                                    {
//                                        //listResponse.Items.Add("Eligibility 

//Rejection Reason: " + 

//GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
//                                        //listResponse.Items.Add("Eligibility 

//Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
//                                    }

//                                    EDIReturnResult = 

//oSegment.get_DataElementValue(3).Trim() + "-" + 

//oSegment.get_DataElementValue(4).Trim();
//                                    AddNote(_PatientId, EDIReturnResult);
//                                }
//                                else if (sSegmentID == "REF")
//                                {

//                                }

//                            }
//                            else if (sLoopSection == "HL;NM1;EB;III")
//                            {
//                                if (sSegmentID == "III")
//                                {

//                                }
//                            }

//                        }
//                    }
//                    ediDataSegment.Set(ref oSegment, 

//(ediDataSegment)oSegment.Next());
//                }
//                // Checks the 997 acknowledgment file just created.
//                // The 997 file is an EDI file, so the logic to read the 997 

//Functional Acknowledgemnt file is similar
//                // to translating any other EDI file.

//                // Gets the first segment of the 997 acknowledgment file
//                ediDataSegment.Set(ref oSegment, 

//(ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) 

//oAck.GetFirst997DataSegment();

//                while (oSegment != null)
//                {
//                    nArea = oSegment.Area;
//                    sLoopSection = oSegment.LoopSection;
//                    sSegmentID = oSegment.ID;

//                    if (nArea == 1)
//                    {
//                        if (sLoopSection == "")
//                        {
//                            if (sSegmentID == "AK9")
//                            {
//                                if (oSegment.get_DataElementValue(1, 0) == "R")
//                                {
                                    

////MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIco

//n.Information);
//                                }
//                            }
//                        }	// sLoopSection == ""
//                    }	//nArea == 1
//                    ediDataSegment.Set(ref oSegment, 

//(ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) 

//oSegment.Next();
//                }	//oSegment != null

//                //save the acknowledgment
//                string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + 

//"997_277\\";
//                if (System.IO.Directory.Exists(sDirectoryPath) == false) { 

//System.IO.Directory.CreateDirectory(sDirectoryPath); }
//                oAck.Save(sDirectoryPath + "997_270.X12");
//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
//            }
//        }

//        public Int64 AddNote(Int64 PatientID, string Notes)
//        {
//            Int64 _result = 0;
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            gloDatabaseLayer.DBParameters oDBParameters = new 

//gloDatabaseLayer.DBParameters();
//            oDB.Connect(false);

//            try
//            {

//                oDBParameters.Clear();
//                object _intresult = 0;
//                oDBParameters.Add("@nPNotesID", 0, 

//System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@nPatientID", PatientID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@nDate", 

//gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@nTime", 

//gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())

//, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@sNotes", Notes, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@nNotesType", 1, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);//Enum will be 

//taken later 0=None, 1=EDI
//                oDBParameters.Add("@nClinicID", _ClinicId, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
//                int result = oDB.Execute("SP_INUP_Patient_Notes", oDBParameters, 

//out  _intresult);


//                if (_intresult != null)
//                {
//                    if (_intresult.ToString().Trim() != "")
//                    {
//                        if (Convert.ToInt64(_intresult) > 0)
//                        {
//                            _result = Convert.ToInt64(_intresult);
//                            // 

//gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, 

//gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Add, "Add 

//notes from Eligibility Response", PatientID, 0, 0, 

//gloAuditTrail.ActivityOutCome.Success);
//                        }
//                    }
//                }
//            }

//            catch (gloDatabaseLayer.DBException DBErr)
//            {
//                DBErr.ERROR_Log(DBErr.ToString());
//                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", 

//System.Windows.Forms.MessageBoxButtons.OK, 

//System.Windows.Forms.MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
//            }
//            finally
//            {
//                oDB.Disconnect();
//                oDBParameters.Dispose();
//                oDB.Dispose();
//            }
//            return _result;
//        }

//        public Int64 InsertEDIResponse271(Int64 PatientID, string TranCntrlReqID, string SenderID, string RecieverID, string dtEligiblityDate, string MemberID, string PlanNumber, string GroupNumber, string FormularyID, string AlternativeID, string BIN, string PCN, bool IsdemographicChange, bool IsPharmacy, bool IsMORx, string ResponseType)
//        {
//            Int64 _result = 0;
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            gloDatabaseLayer.DBParameters oDBParameters = new 

//gloDatabaseLayer.DBParameters();
//            oDB.Connect(false);

//            try
//            {


//                oDBParameters.Clear();
//                object _intresult = 0;
//                oDBParameters.Add("@nPatientID", PatientID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@nTransactionControlID", 0, 

//System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
//                oDBParameters.Add("@sTransactionControlRequestID", TranCntrlReqID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sSenderID", SenderID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sRecieverID", RecieverID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@dtEligiblityDate", dtEligiblityDate, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sMemberID", MemberID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//Enum will 

//be taken later 0=None, 1=EDI
//                oDBParameters.Add("@sPlanNumber", PlanNumber, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sGroupNumber", GroupNumber, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sFormularyID", FormularyID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sAlternativeID", AlternativeID, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@BIN", BIN, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@PCN", PCN, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@IsDemographicChange", IsdemographicChange, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@IsPharmacy", IsPharmacy, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@IsMailOrdRx", IsMORx, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                oDBParameters.Add("@sResponseType", ResponseType, 

//System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
//                int result = oDB.Execute("sp_InUpEDIResp271_1", oDBParameters, out 

// _intresult);


//                if (_intresult != null)
//                {
//                    if (_intresult.ToString().Trim() != "")
//                    {
//                        if (Convert.ToInt64(_intresult) > 0)
//                        {
//                            _result = Convert.ToInt64(_intresult);
//                            // 

//gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, 

//gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Add, "Add 

//notes from Eligibility Response", PatientID, 0, 0, 

//gloAuditTrail.ActivityOutCome.Success);
//                        }
//                    }
//                }
//            }

//            catch (gloDatabaseLayer.DBException DBErr)
//            {
//                DBErr.ERROR_Log(DBErr.ToString());
//                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloRxHub", 

//System.Windows.Forms.MessageBoxButtons.OK, 

//System.Windows.Forms.MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
//            }
//            finally
//            {
//                oDB.Disconnect();
//                oDBParameters.Dispose();
//                oDB.Dispose();
//            }
//            return _result;
//        }

//        private string GetSourceRejectionReason(string _RejectCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strRejectionReason = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_SourceRejectionCode Where 

//sCode='" + _RejectCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strRejectionReason = Convert.ToString(_result);
//                }
//                else
//                {
//                    strRejectionReason = "";
//                }

//                return strRejectionReason;

//            }
//            catch (Exception ex)
//            {
//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

//        private string GetSourceFollowUp(string _FollowUpCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strFollowupDesc = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_SourceFollowupCode Where 

//sCode='" + _FollowUpCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strFollowupDesc = Convert.ToString(_result);
//                }
//                else
//                {
//                    strFollowupDesc = "";
//                }

//                return strFollowupDesc;

//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

//        private string GetReceiverRejectionReason(string _RejectCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strRejectionReason = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_ReceiverRejectionCode Where 

//sCode='" + _RejectCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strRejectionReason = Convert.ToString(_result);
//                }
//                else
//                {
//                    strRejectionReason = "";
//                }

//                return strRejectionReason;

//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

//        private string GetReceiverFollowUp(string _FollowUpCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strFollowupDesc = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_ReceiverFollowupCode Where 

//sCode='" + _FollowUpCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strFollowupDesc = Convert.ToString(_result);
//                }
//                else
//                {
//                    strFollowupDesc = "";
//                }

//                return strFollowupDesc;

//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

//        private string GetSubscriberRejectionReason(string _RejectCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strRejectionReason = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_SubscriberRejectionCode 

//Where sCode='" + _RejectCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strRejectionReason = Convert.ToString(_result);
//                }
//                else
//                {
//                    strRejectionReason = "";
//                }

//                return strRejectionReason;

//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

//        private string GetSubscriberFollowUp(string _FollowUpCode)
//        {
//            gloDatabaseLayer.DBLayer oDB = new 

//gloDatabaseLayer.DBLayer(_databaseConnectionString);
//            Object _result = null;
//            string strFollowupDesc = "";
//            string strSQL = "";
//            try
//            {
//                strSQL = "SELECT sDescription FROM BL_SubscriberFollowupCode Where 

//sCode='" + _FollowUpCode + "'";
//                oDB.Connect(false);
//                _result = oDB.ExecuteScalar_Query(strSQL);
//                if (_result != null)
//                {
//                    strFollowupDesc = Convert.ToString(_result);
//                }
//                else
//                {
//                    strFollowupDesc = "";
//                }

//                return strFollowupDesc;

//            }
//            catch (Exception ex)
//            {
//                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

//                return "";
//            }
//            finally
//            {
//                if (oDB != null) { oDB.Dispose(); }
//            }
//        }

        #endregion " Private Methods "

        private void tsb271_SingleCovrgPhBenefit_Click(object sender, EventArgs e)
        {
            //Translate271Response();
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       

        private void Set271InfoFlxgrdColoumns()
        {
            FlexGrd271Details.Rows.Count = 1;
            FlexGrd271Details.Cols.Count = COL_271INFOCOUNT;
            FlexGrd271Details.Cols.Fixed = 0;
            FlexGrd271Details.Rows.Fixed = 1;

            //Flex grid colouring 
            FlexGrd271Details.Font = new System.Drawing.Font("Verdana", 9,System.Drawing.FontStyle.Regular);
            FlexGrd271Details.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            FlexGrd271Details.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(181, 216, 241);
            FlexGrd271Details.Styles.Alternate.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Alternate.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrd271Details.Styles.Editor.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Editor.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.EmptyArea.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            FlexGrd271Details.Styles.EmptyArea.Border.Color = System.Drawing.SystemColors.ControlDarkDark;
            FlexGrd271Details.Styles.EmptyArea.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(4, 96, 162);
            FlexGrd271Details.Styles.Fixed.Border.Color = System.Drawing.SystemColors.ControlDark;
            FlexGrd271Details.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            FlexGrd271Details.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrd271Details.Styles.Focus.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Focus.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.Frozen.BackColor = System.Drawing.Color.Beige;
            FlexGrd271Details.Styles.Frozen.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Frozen.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrd271Details.Styles.Highlight.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Highlight.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.NewRow.BackColor = System.Drawing.SystemColors.Window;
            FlexGrd271Details.Styles.NewRow.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.NewRow.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGrd271Details.Styles.Normal.BackColor = System.Drawing.SystemColors.Window;
            FlexGrd271Details.Styles.Normal.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Normal.ForeColor = System.Drawing.SystemColors.WindowText;


            FlexGrd271Details.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrd271Details.Styles.Search.Border.Color = System.Drawing.Color.Black;
            FlexGrd271Details.Styles.Search.ForeColor = System.Drawing.SystemColors.HighlightText;
            //For i As Integer = 0 To COL_PUBCOUNT - 1 

            // 'setting coloumns text alignment 
            // FlexGrdPubInfo.Cols(i).TextAlign =       C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter ;
            //Next 

      
      
            
            FlexGrd271Details.Cols[COL_INFORMATIONsOURCENAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_INFORMATIONRECIEVERNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_PBM_PAYERSOURCENAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_PAYERPARTICIPANTID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_PBMPAYERMEMBERID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_HEALTHPLANNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_CARDHOLDERID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_CARDHOLDERNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_GROUPID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_GROUPNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_FORMULARLYLISTID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_ALTERNATIVELISTID].DataType =typeof(System.String);
            FlexGrd271Details.Cols[COL_COVERAGEID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_BINNUMBER].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_COPAYID].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_ISDEMOGRAPHICCHANGED].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_ISPHARMACYELIGIBLE].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_PHARMACYCOVERAGENAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_PHELIGIBLITYBENEFITINFO].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_ISMAILORDRXDRUGELIGIBLE].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_MAILORDRDDRUGCOVERAGENAME].DataType = typeof(System.String);
            

            FlexGrd271Details.Cols[COL_MAILORDRXDRUGELIGIBLITYORBENEFITINFO].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTLASTNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTMIDDLENAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTFIRSTNAME].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTADDRESS1].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTADDRESS2].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTSTATE].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTZIP].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTDOB].DataType = typeof(System.String);
            FlexGrd271Details.Cols[COL_DEPENDANTGENDER].DataType = typeof(System.String);
          


        //    //setting coloumn width 

            FlexGrd271Details.Cols[COL_INFORMATIONsOURCENAME].Width = 150;
            FlexGrd271Details.Cols[COL_INFORMATIONRECIEVERNAME].Width = 120;
            FlexGrd271Details.Cols[COL_PBM_PAYERSOURCENAME].Width = 110;
            FlexGrd271Details.Cols[COL_PAYERPARTICIPANTID].Width = 100;
            FlexGrd271Details.Cols[COL_PBMPAYERMEMBERID].Width = 100;
            FlexGrd271Details.Cols[COL_HEALTHPLANNAME].Width = 100;
            FlexGrd271Details.Cols[COL_CARDHOLDERID].Width = 100;
            FlexGrd271Details.Cols[COL_CARDHOLDERNAME].Width = 100;
            FlexGrd271Details.Cols[COL_GROUPID].Width = 100;
            FlexGrd271Details.Cols[COL_GROUPNAME].Width = 100;
            FlexGrd271Details.Cols[COL_FORMULARLYLISTID].Width = 100;
            FlexGrd271Details.Cols[COL_ALTERNATIVELISTID].Width = 100;
            FlexGrd271Details.Cols[COL_COVERAGEID].Width = 100;
            FlexGrd271Details.Cols[COL_BINNUMBER].Width = 100;
            FlexGrd271Details.Cols[COL_COPAYID].Width = 100;
            FlexGrd271Details.Cols[COL_ISDEMOGRAPHICCHANGED].Width = 100;
            FlexGrd271Details.Cols[COL_ISPHARMACYELIGIBLE].Width = 100;
            FlexGrd271Details.Cols[COL_PHARMACYCOVERAGENAME].Width = 100;
            FlexGrd271Details.Cols[COL_PHELIGIBLITYBENEFITINFO].Width = 100;
            FlexGrd271Details.Cols[COL_ISMAILORDRXDRUGELIGIBLE].Width = 100;
            FlexGrd271Details.Cols[COL_MAILORDRDDRUGCOVERAGENAME].Width = 100;
            FlexGrd271Details.Cols[COL_MAILORDRXDRUGELIGIBLITYORBENEFITINFO].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTLASTNAME].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTMIDDLENAME].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTFIRSTNAME].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTADDRESS1].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTADDRESS2].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTSTATE].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTZIP].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTDOB].Width = 100;
            FlexGrd271Details.Cols[COL_DEPENDANTGENDER].Width = 100;

        //    //FlexGrd271Details.Cols(COL_SUB_SERVERNAME).Sort = C1.Win.C1FlexGrid.SortFlags.Ascending;

        //    ////-----------these 2 prperties are used to make the flex grid coloumn as password coloumn 
        //    //this.FlexGrd271Details.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
        //    //this.FlexGrd271Details.Cols(COL_SUB_SERVERPASSWORD).Editor = txtPassword;
        //    ////--------------- 

            FlexGrd271Details.SetData(0, COL_INFORMATIONsOURCENAME, "Information Source Name");
            FlexGrd271Details.SetData(0, COL_INFORMATIONRECIEVERNAME, "Information Reciever Name");
            FlexGrd271Details.SetData(0, COL_PBM_PAYERSOURCENAME, "PBM/Payer Source Name");
            FlexGrd271Details.SetData(0, COL_PAYERPARTICIPANTID, "Participant ID");
            FlexGrd271Details.SetData(0, COL_PBMPAYERMEMBERID, "PBM Payer Member ID");
            FlexGrd271Details.SetData(0, COL_HEALTHPLANNAME, "Health Plan Name");
            FlexGrd271Details.SetData(0, COL_CARDHOLDERID, "Card Holder ID");
            FlexGrd271Details.SetData(0, COL_CARDHOLDERNAME, "Card Holder Name");
            FlexGrd271Details.SetData(0, COL_GROUPID, "Group ID");
            FlexGrd271Details.SetData(0, COL_GROUPNAME, "Group Name");
            FlexGrd271Details.SetData(0, COL_FORMULARLYLISTID, "Formularly List ID");
            FlexGrd271Details.SetData(0, COL_ALTERNATIVELISTID, "Alternative List ID");
            FlexGrd271Details.SetData(0, COL_COVERAGEID, "Coverage ID");
            FlexGrd271Details.SetData(0, COL_BINNUMBER, "BIN Number");
            FlexGrd271Details.SetData(0, COL_COPAYID, "Copay ID");
            FlexGrd271Details.SetData(0, COL_ISDEMOGRAPHICCHANGED, "Is Demographic Changed");
            FlexGrd271Details.SetData(0, COL_ISPHARMACYELIGIBLE, "is Pharmacy Eligible");
            FlexGrd271Details.SetData(0, COL_PHARMACYCOVERAGENAME, "Pharmacy Coverage Name");
            FlexGrd271Details.SetData(0, COL_PHELIGIBLITYBENEFITINFO, "Pharmacy Eligiblity benefit Info");
            FlexGrd271Details.SetData(0, COL_ISMAILORDRXDRUGELIGIBLE, "Is Mail Ord Rx Drug Eligible");
            FlexGrd271Details.SetData(0, COL_MAILORDRDDRUGCOVERAGENAME, "Mail Ord Rx Drug Coverage Name");
            FlexGrd271Details.SetData(0, COL_MAILORDRXDRUGELIGIBLITYORBENEFITINFO, "Mail Ord Rx Drug Eligiblity Info");
            FlexGrd271Details.SetData(0, COL_DEPENDANTLASTNAME, "Dependant LastName");
           
            FlexGrd271Details.SetData(0, COL_DEPENDANTMIDDLENAME, "Dependant LastName");
            FlexGrd271Details.SetData(0, COL_DEPENDANTFIRSTNAME, "Dependant FirstName");
            FlexGrd271Details.SetData(0, COL_DEPENDANTMIDDLENAME, "Dependant MiddleName");
            FlexGrd271Details.SetData(0, COL_DEPENDANTADDRESS1, "Dependant Address1");
            FlexGrd271Details.SetData(0, COL_DEPENDANTADDRESS2, "Dependant Address2");
            FlexGrd271Details.SetData(0, COL_DEPENDANTSTATE, "Dependant State");
            FlexGrd271Details.SetData(0, COL_DEPENDANTZIP, "Dependant Zip");
            FlexGrd271Details.SetData(0, COL_DEPENDANTDOB, "Dependant DOB");
            FlexGrd271Details.SetData(0, COL_DEPENDANTGENDER, "Dependant Gender");



        }

        private void SetMultipleSubsFlxgrdColoumns()
        {
            FlexGridSubscriber.Rows.Count = 1;
            FlexGridSubscriber.Cols.Count = COL_SUBSCRIBERCOUNT;
            FlexGridSubscriber.Cols.Fixed = 0;
            FlexGridSubscriber.Rows.Fixed = 1;

            //Flex grid colouring 
            FlexGridSubscriber.Font = new System.Drawing.Font("Verdana", 9, System.Drawing.FontStyle.Regular);
            FlexGridSubscriber.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            FlexGridSubscriber.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(181, 216, 241);
            FlexGridSubscriber.Styles.Alternate.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Alternate.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGridSubscriber.Styles.Editor.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Editor.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.EmptyArea.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            FlexGridSubscriber.Styles.EmptyArea.Border.Color = System.Drawing.SystemColors.ControlDarkDark;
            FlexGridSubscriber.Styles.EmptyArea.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(4, 96, 162);
            FlexGridSubscriber.Styles.Fixed.Border.Color = System.Drawing.SystemColors.ControlDark;
            FlexGridSubscriber.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            FlexGridSubscriber.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGridSubscriber.Styles.Focus.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Focus.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.Frozen.BackColor = System.Drawing.Color.Beige;
            FlexGridSubscriber.Styles.Frozen.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Frozen.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGridSubscriber.Styles.Highlight.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Highlight.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.NewRow.BackColor = System.Drawing.SystemColors.Window;
            FlexGridSubscriber.Styles.NewRow.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.NewRow.ForeColor = System.Drawing.SystemColors.WindowText;

            FlexGridSubscriber.Styles.Normal.BackColor = System.Drawing.SystemColors.Window;
            FlexGridSubscriber.Styles.Normal.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Normal.ForeColor = System.Drawing.SystemColors.WindowText;


            FlexGridSubscriber.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGridSubscriber.Styles.Search.Border.Color = System.Drawing.Color.Black;
            FlexGridSubscriber.Styles.Search.ForeColor = System.Drawing.SystemColors.HighlightText;
            //For i As Integer = 0 To COL_PUBCOUNT - 1 

            // 'setting coloumns text alignment 
            // FlexGrdPubInfo.Cols(i).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter 
            //Next 




            FlexGridSubscriber.Cols[COL_SUBSCRIBERLASTNAME].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERFIRSTNAME].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERMIDDLENAME].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERSTATE].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERCITY].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERZIP].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERDOB].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERGENDER].DataType =typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERADDRESS1].DataType = typeof(System.String);
            FlexGridSubscriber.Cols[COL_SUBSCRIBERADDRESS2].DataType = typeof(System.String);
            

            //setting coloumn width 

            FlexGridSubscriber.Cols[COL_SUBSCRIBERLASTNAME].Width = 150;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERFIRSTNAME].Width = 120;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERMIDDLENAME].Width = 110;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERSTATE].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERCITY].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERZIP].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERDOB].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERGENDER].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERADDRESS1].Width = 100;
            FlexGridSubscriber.Cols[COL_SUBSCRIBERADDRESS2].Width = 100;
           

            //FlexGridSubscriber.Cols(COL_SUB_SERVERNAME).Sort = C1.Win.C1FlexGrid.SortFlags.Ascending;

            ////-----------these 2 prperties are used to make the flex grid coloumn as password coloumn 
            //this.FlexGridSubscriber.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            //this.FlexGridSubscriber.Cols(COL_SUB_SERVERPASSWORD).Editor = txtPassword;
            ////--------------- 

            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERLASTNAME, "Subscriber LastName");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERFIRSTNAME, "Subscriber FirstName");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERMIDDLENAME, "Subscriber MiddleName");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERADDRESS1, "Subscriber Address1");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERADDRESS2, "Subscriber Address2");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERSTATE, "Subscriber State");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERCITY, "Subscriber City");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERZIP, "Subscriber Zip");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERDOB, "Subscriber DOB");
            FlexGridSubscriber.SetData(0, COL_SUBSCRIBERGENDER, "Subscriber Gender");
          
           
        } 



        private void frmEligiblityResponse_new_Load(object sender, EventArgs e)
        {
            Set271InfoFlxgrdColoumns();
            SetMultipleSubsFlxgrdColoumns();

            oClsRxHubInterface = new ClsRxHubInterface(_PatientId);
            oClsRxHubInterface.Patient = oclsPatient;
            ClsRxH_271Master oClsRxH_271Master = new ClsRxH_271Master();
            Cls271Information oCls271Information = new Cls271Information();
            try
            {
                //in case of NAK first check the file is NAK otherwise it will not be loaded in EDI framework
                if (oClsRxHubInterface.LoadEDIObject_271(_EDI271FilePath) == false)
                {
                    //means file is not valid EDI file and is not able to get loaded in the EDI doc object.
                    //therefore this must be the NAK file
                    MessageBox.Show("NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                { 
                    //file is valid edi file and has got loaded in the ediDOC object
                }
                
                //LoadEDIObject();
                //oClsRxH_271Master = oClsRxHubInterface.Translate271Response();
                oCls271Information = oClsRxHubInterface.Translate271Response();
                


                if (oCls271Information.Count > 0)
                {
                    for (int i = 0; i <= oCls271Information.Count - 1; i++)
                    {
                        FlexGrd271Details.Rows.Add();
                        FlexGrd271Details.SetData(i + 1,COL_INFORMATIONsOURCENAME,oCls271Information[i].InformationSourceName);
                        FlexGrd271Details.SetData(i + 1,COL_INFORMATIONRECIEVERNAME, oCls271Information[i].InformationRecieverName);
                        FlexGrd271Details.SetData(i + 1, COL_PBM_PAYERSOURCENAME,oCls271Information[i].PayerName);
                        FlexGrd271Details.SetData(i + 1, COL_PAYERPARTICIPANTID,oCls271Information[i].PayerParticipantId);
                        FlexGrd271Details.SetData(i + 1, COL_PAYERPARTICIPANTID,oCls271Information[i].PayerParticipantId);
                        FlexGrd271Details.SetData(i + 1, COL_PBMPAYERMEMBERID,oCls271Information[i].MemberID);
                        FlexGrd271Details.SetData(i + 1, COL_HEALTHPLANNAME, oCls271Information[i].HealthPlanName);
                        FlexGrd271Details.SetData(i + 1, COL_CARDHOLDERID, oCls271Information[i].CardHolderId);
                        FlexGrd271Details.SetData(i + 1, COL_CARDHOLDERNAME,oCls271Information[i].CardHolderName);
                        FlexGrd271Details.SetData(i + 1, COL_GROUPID, oCls271Information[i].GroupId);
                        FlexGrd271Details.SetData(i + 1, COL_GROUPNAME,oCls271Information[i].GroupName);
                        FlexGrd271Details.SetData(i + 1, COL_FORMULARLYLISTID,oCls271Information[i].FormularyListId);
                        FlexGrd271Details.SetData(i + 1, COL_ALTERNATIVELISTID, oCls271Information[i].AlternativeListId);
                        FlexGrd271Details.SetData(i + 1, COL_COVERAGEID, oCls271Information[i].CoverageId);
                        FlexGrd271Details.SetData(i + 1, COL_BINNUMBER, oCls271Information[i].BINNumber);
                        FlexGrd271Details.SetData(i + 1, COL_COPAYID, oCls271Information[i].CopayId);
                        //FlexGrd271Details.SetData(i + 1, COL_ISDEMOGRAPHICCHANGED,oCls271Information[i].IsDemographicsChanged);
                        FlexGrd271Details.SetData(i + 1, COL_ISPHARMACYELIGIBLE, oCls271Information[i].IsPharmacyEligible);
                        FlexGrd271Details.SetData(i + 1, COL_PHARMACYCOVERAGENAME, oCls271Information[i].PharmacyCoveragePlanName);
                        FlexGrd271Details.SetData(i + 1, COL_PHELIGIBLITYBENEFITINFO, oCls271Information[i].PharmacyEligiblityorBenefitInfo);
                        FlexGrd271Details.SetData(i + 1, COL_ISMAILORDRXDRUGELIGIBLE, oCls271Information[i].IsMailOrdRxDrugEligible);
                        FlexGrd271Details.SetData(i + 1, COL_MAILORDRDDRUGCOVERAGENAME, oCls271Information[i].MailOrdRxDrugCoveragePlanName);
                        FlexGrd271Details.SetData(i + 1, COL_MAILORDRXDRUGELIGIBLITYORBENEFITINFO, oCls271Information[i].MailOrdRxDrugEligiblityorBenefitInfo);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTLASTNAME, oCls271Information[i].DLastName);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTMIDDLENAME,oCls271Information[i].DMiddleName);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTFIRSTNAME, oCls271Information[i].DFirstName);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTADDRESS1, oCls271Information[i].DAddress1);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTADDRESS2,oCls271Information[i].DAddress2);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTSTATE, oCls271Information[i].DState);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTZIP, oCls271Information[i].DZip);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTDOB, oCls271Information[i].DDOB);
                        FlexGrd271Details.SetData(i + 1, COL_DEPENDANTGENDER,oCls271Information[i].DGender);


                    }

                }


                if (oCls271Information.Count > 0)
                {
                    
                    for (int i = 0; i <= oCls271Information.Count - 1; i++)
                    {
                        if (oCls271Information[i].RxH_271Details.Count > 0)
                        {
                            if (i == 1)
                            {
                                return;
                            }
                            for (int j = 0; j <= oCls271Information[i].RxH_271Details.Count - 1; j++)
                            {
                                FlexGridSubscriber.Rows.Add();
                                FlexGridSubscriber.SetData(j + 1,COL_SUBSCRIBERLASTNAME, oCls271Information[i].RxH_271Details[j].SubscriberLastName);
                                FlexGridSubscriber.SetData(j + 1,COL_SUBSCRIBERFIRSTNAME, oCls271Information[i].RxH_271Details[j].SubscriberFirstName);
                                FlexGridSubscriber.SetData(j + 1,COL_SUBSCRIBERMIDDLENAME, oCls271Information[i].RxH_271Details[j].SubscriberMiddleName);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERADDRESS1, oCls271Information[i].RxH_271Details[j].SubscriberAddress1);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERADDRESS2, oCls271Information[i].RxH_271Details[j].SubscriberAddress2);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERCITY, oCls271Information[i].RxH_271Details[j].SubscriberCity);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERSTATE, oCls271Information[i].RxH_271Details[j].SubscriberState);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERZIP, oCls271Information[i].RxH_271Details[j].SubscriberZip);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERDOB, oCls271Information[i].RxH_271Details[j].SubscriberDOB);
                                FlexGridSubscriber.SetData(j + 1, COL_SUBSCRIBERGENDER, oCls271Information[i].RxH_271Details[j].SubscriberGender);
                                
                            }
                                
                        }
                    
                    }

                }

            

            }
            catch (ClsRxHubDBLayerException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;

            }
            catch (ClsRxHubInterfaceException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
                
            }
           
        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            //ClsRxHubInterface oClsRxHubInterface = new ClsRxHubInterface();
            //oClsRxHubInterface.SaveEDIResponse271();
 
        }

        private void txtSubscriberLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDependantLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDependantAddressLine1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDependantAddressLine2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDependantCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDependantGender_TextChanged(object sender, EventArgs e)
        {

        }

    

       

       
    }
}
