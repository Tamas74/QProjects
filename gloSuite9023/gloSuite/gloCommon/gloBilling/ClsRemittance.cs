using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edidev.FrameworkEDI;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace gloBilling
{
    public enum RemittanceProcessed
    {
        NotProcessed = 0,
        Processed = 1
    }

    class ClsRemittance
    {
    }

    class gloRemittance : IDisposable
    {
        #region " Constructor & Destructor "

        public gloRemittance(String DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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

        ~gloRemittance()
        {
            Dispose(false);
        }

        #endregion " Constructor & Destructor "

        #region " Private Variables "

        private string _databaseconnectionstring = "";
        ediDocument EdiDoc = null;
        ediDataSegment oSegment = null;
        ediAcknowledgment oAck = null;
        ediSchemas oSchemas = null;
        ediSchema oSchema = null;
        bool _IsError = false;

        #endregion " Private Variables "

        #region " Property Procedures "
        public bool IsError
        {
            get { return _IsError; }
            set { _IsError = value; }
        }
        #endregion " Property Procedures "

        #region " Private And Public Methods "

        public void LoadEDISchema()
        {
            string sPath = "";
            //string sEntity = "";
            //string sQlfr = "";
            string sSefFile = "";
            string sEdiFile = "";
            try
            {
                EdiDoc = new ediDocument();
                ediDocument.Set(ref EdiDoc, new ediDocument());    // EdiDoc = new ediDocument();
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSefFile = "835_X091A1.SEF";
                sEdiFile = "835.X12";
                // Disabling the internal standard reference library to makes sure that 
                // FREDI uses only the SEF file provided
                ediSchemas.Set(ref oSchemas, (ediSchemas)EdiDoc.GetSchemas());    //oSchemas = (ediSchemas) EdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                //oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                //oAck.EnableFunctionalAcknowledgment = true;

                //// Set the starting point of the control numbers in the acknowledgment
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                //// Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                //// using the MapDataElementLevelError method.
                //oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                // All SEF files required for processing the EDI files must be provided by calling the LoadSchema.  
                // The "LoadSchema" method does not actually load the SEF files at this time, but are actually 
                // loaded during the LoadEdi method.
                EdiDoc.LoadSchema(sSefFile, 0);    //for EDI (810) file
                //EdiDoc.LoadSchema("997_X12-4010.SEF", 0);    //for Ack (997) file

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public Remittance ReadRemittances(string EDIFileName)
        {
            string sSegmentID = "";
            string sLoopSection = "";
            string sLXID = "";
            string sEntity = "";
            string sEdiFile = "";
            int nArea;
            string sValue = "";
            Int32 _nArea2RowCount = 0;
            int Area2rowIndex = 0;
            int rowIndex = 0;
            int i = 0;
            int _Index = 0;
            Claim oClaim = new Claim();
            ClaimServiceLine oClaimServiceLine = new ClaimServiceLine();
            ClaimServiceLines oClaimServiceLines = new ClaimServiceLines();
            Claims oClaims = new Claims();
            Remittance oRemittance = new Remittance();

            try
            {
                EdiDoc.LoadSchema("997_X12-4010.SEF", 0);    //for Ack (997) file

                EdiDoc = new ediDocument();

                //// If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                //// property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                ////// Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                //Set the cursor type to write an acknowledgement file.
                //EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                ////// Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                ////// using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");


                if (EDIFileName.Trim() != "")
                {
                    EdiDoc.LoadEdi(EDIFileName);
                }
                else
                {
                    return null;
                }


                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)EdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) EdiDoc.FirstDataSegment

                // This loop iterates though the EDI file a segment at a time
                while (oSegment != null)
                {
                    // A segment is identified by its Area number, Loop section and segment id.
                    //sSegmentID = oSegment.ID;
                    //sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                // map data elements of ISA segment in here
                                #region " ISA Segment "
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
                                #endregion " ISA Segment "
                            }
                            else if (sSegmentID == "GS")
                            {
                                #region " GS Segment "
                                // map data elements of GS segment in here
                                sValue = oSegment.get_DataElementValue(1);     //Functional Identifier Code
                                sValue = oSegment.get_DataElementValue(2);    //Application Sender's Code
                                sValue = oSegment.get_DataElementValue(3);    //Application Receiver's Code
                                sValue = oSegment.get_DataElementValue(4);   //Date
                                sValue = oSegment.get_DataElementValue(5);   //Time
                                sValue = oSegment.get_DataElementValue(6);  //Group Control Number
                                sValue = oSegment.get_DataElementValue(7);  //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8);   //Version / Rele
                                #endregion " GS Segment "
                            }
                        }
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                #region " ST Segment "
                                // map data element of ST segment in here
                                sValue = oSegment.get_DataElementValue(1);     //Transaction Set Identifier Code
                                sValue = oSegment.get_DataElementValue(2);     //Transaction Set Control Number
                                #endregion " ST Segment "
                            }
                            else if (sSegmentID == "BPR")
                            {
                                oRemittance = new Remittance();
                                #region " BPR Segment "
                                if (oSegment.get_DataElementValue(1).ToString() == "I")//It was "C" changed to "I"
                                {
                                    oRemittance.ProviderPayment = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(2);  //Monetary Amount
                                    sValue = oSegment.get_DataElementValue(3);  ////////Credit/Debit Flag Code
                                    oRemittance.PaymentMethod = oSegment.get_DataElementValue(4).ToString();
                                    sValue = oSegment.get_DataElementValue(4);   //Payment Method Code
                                    sValue = oSegment.get_DataElementValue(5);    //Payment Format Code
                                    sValue = oSegment.get_DataElementValue(6);  //(DFI) ID Number Qualifier
                                    oRemittance.SenderID = oSegment.get_DataElementValue(7).ToString();
                                    sValue = oSegment.get_DataElementValue(8);    //Account Number Qualifier
                                    oRemittance.SenderAccountNo = oSegment.get_DataElementValue(9).ToString();
                                    oRemittance.PayerID = oSegment.get_DataElementValue(10).ToString();
                                    sValue = oSegment.get_DataElementValue(11);   //Originating Company Supplemental Code
                                    sValue = oSegment.get_DataElementValue(12);   //(DFI) ID Number Qualifier
                                    oRemittance.ProviderBankID = oSegment.get_DataElementValue(13).ToString();
                                    sValue = oSegment.get_DataElementValue(14);     //Account Number Qualifier
                                    oRemittance.ProviderAccountNo = oSegment.get_DataElementValue(15).ToString();

                                    if (oSegment.get_DataElementValue(16) != "")
                                    {
                                        oRemittance.CheckIssueDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(16))).ToShortDateString();
                                    }

                                }
                                #endregion " BPR Segment "
                            }
                            else if (sSegmentID == "TRN")
                            {
                                #region " TRN Segment "
                                oRemittance.CheckNumber = oSegment.get_DataElementValue(2).ToString();
                                oRemittance.PayerID = oSegment.get_DataElementValue(3);
                                #endregion " TRN Segment "
                            }
                            else if (sSegmentID == "DTM")
                            {
                                #region " DTM Segment "
                                sValue = oSegment.get_DataElementValue(1);    //Date/Time Qualifier
                                if (oSegment.get_DataElementValue(2) != "")
                                {
                                    oRemittance.ProductionDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                }
                                #endregion " DTM Segment "
                            }

                        } // sLoopSection == ""

                        else if (sLoopSection == "N1")
                        {
                            if (sSegmentID == "N1")
                            {
                                sEntity = oSegment.get_DataElementValue(1); //get loop entity qualifier to identity each N1 loop instances
                            }

                            if (sEntity == "PR") //payer information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payer N1 Segment "
                                    sValue = oSegment.get_DataElementValue(1);  // Entity Identifier Code (98) 
                                    oRemittance.PayerName = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(3);  // Identification Code Qualifier (66) 
                                    sValue = oSegment.get_DataElementValue(4);  // Identification Code (67) 
                                    sValue = oSegment.get_DataElementValue(5);  // Entity Relationship Code (706) 
                                    sValue = oSegment.get_DataElementValue(6);  // Entity Identifier Code (98) 
                                    #endregion " Payer N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payer N3 Segment "
                                    oRemittance.PayerAddress = oSegment.get_DataElementValue(1).ToString();
                                    sValue = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payer N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payer N4 Segment "
                                    oRemittance.PayerCity = oSegment.get_DataElementValue(1).ToString();
                                    oRemittance.PayerState = oSegment.get_DataElementValue(2).ToString();
                                    oRemittance.PayerZip = oSegment.get_DataElementValue(3).ToString();
                                    sValue = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    sValue = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    sValue = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    #endregion " Payer N4 Segment "
                                }
                                else if (sSegmentID == "REF")
                                {
                                    #region " Payer REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "EO")//It was "2U" changed to "EO"
                                    {
                                        oRemittance.AdditionalPayerID = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    if (oSegment.get_DataElementValue(1) == "2U")//
                                    {
                                        oRemittance.AdditionalPayerID = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Payer REF Segment "
                                }
                            }
                            else if (sEntity == "PE")//payee information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payee N1 Segment "
                                    sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                    oRemittance.PayeeName = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(3);   //Identification Code Qualifier (66) 
                                    oRemittance.PayeeFederalTaxID = oSegment.get_DataElementValue(4).ToString();//It could be NPI of Provider
                                    sValue = oSegment.get_DataElementValue(5); // Entity Relationship Code (706) 
                                    sValue = oSegment.get_DataElementValue(6); // Entity Identifier Code (98) 
                                    #endregion " Payee N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payee N3 Segment "
                                    oRemittance.PayeeAddress = oSegment.get_DataElementValue(1).ToString() + oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payee N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payee N4 Segment "
                                    oRemittance.PayeeCity = oSegment.get_DataElementValue(1).ToString();
                                    oRemittance.PayeeState = oSegment.get_DataElementValue(2).ToString();
                                    oRemittance.PayeeZip = oSegment.get_DataElementValue(3).ToString();
                                    sValue = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    sValue = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    sValue = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    #endregion " Payee N4 Segment "
                                }
                            }//sEntity

                        }//sLoopSection

                    } // nArea == 1

                    else if (nArea == 2)
                    {
                        if (sSegmentID == "LX")
                        {
                            sLXID = oSegment.get_DataElementValue(1);
                        }

                        if (sLXID == "1" || sLXID == "0")//It was "961221" changed to "1"
                        {
                            if (sLoopSection == "LX")
                            {
                                if (sSegmentID == "TS3")
                                {
                                    #region " TS3 Segment "
                                    string str = oSegment.get_DataElementValue(16);
                                    string str1 = oSegment.get_DataElementValue(19);
                                    oClaim.TotalCoinsuranceAmount = oSegment.get_DataElementValue(16);//oClaim.TotalCoinsuranceAmount = "10";//Added By MaheshB
                                    oClaim.TotalDeductibleAmount = oSegment.get_DataElementValue(19);//oClaim.TotalDeductibleAmount = "2";
                                    //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                                    //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                                    //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                                    //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                                    //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                                    //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                                    #endregion " TS3 Segment "
                                }
                                else if (sSegmentID == "TS2")
                                {
                                    #region " TS2 Segment "
                                    //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                                    //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                                    //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                                    //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                                    //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                                    #endregion " TS2 Segment "
                                }
                            }
                            else if (sLoopSection == "LX;CLP")
                            {
                                if (sSegmentID == "CLP")
                                {
                                    if (oClaim == null)
                                    {
                                        oClaim = new Claim();
                                    }
                                    _Index = 0;
                                    if (_nArea2RowCount == 0)
                                    {

                                        _nArea2RowCount++;
                                        i++;
                                    }
                                    #region " CLP Segment "
                                    oClaim.ClaimNumber = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);  // Claim Status Code (1029) 
                                    oClaim.ClaimStatus = oSegment.get_DataElementValue(2);
                                    oClaim.TotalClaimAmount = oSegment.get_DataElementValue(3);
                                    oClaim.ClaimPaymentAmount = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                                    sValue = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                                    oClaim.PayerControlNumber = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                                    sValue = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                                    sValue = oSegment.get_DataElementValue(12);  //Quantity (380) 
                                    sValue = oSegment.get_DataElementValue(13); // Percent (954) 
                                    #endregion " CLP Segment "
                                }
                                else if (sSegmentID == "CAS")
                                {
                                    #region " CAS Segment "
                                    if (oSegment.get_DataElementValue(1) == "CO")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                                        oClaim.ContractualObligation = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PR")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.PatientResposibility = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PI")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.ContractualObligation = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "OA")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.OtherAdjustments = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    #endregion " CAS Segment "
                                }
                                else if (sSegmentID == "NM1")
                                {
                                    if (oSegment.get_DataElementValue(1) == "QC")
                                    {
                                        #region " Patient NM1 Segment "
                                        sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                        sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                                        oClaim.PatientLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                                        sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                                        sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                                        sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                                        oClaim.PatientID = oSegment.get_DataElementValue(9).ToString();
                                        sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                                        sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                                        #endregion " Patient NM1 Segment "
                                    }
                                    if (oSegment.get_DataElementValue(1) == "IL")
                                    {
                                        #region " Subscriber NM1 Segment "
                                        sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                        sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                                        oClaim.SubscriberLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                                        sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                                        sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                                        sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                                        oClaim.SubscriberID = oSegment.get_DataElementValue(9).ToString();
                                        sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                                        sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                                        #endregion " Subscriber NM1 Segment "
                                    }
                                }
                                else if (sSegmentID == "MIA")
                                {
                                    if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                                    {
                                    }
                                }
                                else if (sSegmentID == "MOA")
                                {

                                }
                                else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                                {
                                    #region " Rendering Provider REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "1A")//Blue Cross Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1B")//Blue Shield Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1C")//Medicare Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1D")//Medicaid Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1G")//Provider UPIN Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1H")//CHAMPUS Identification Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "G2")//Provider Commercial Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Rendering Provider REF Segment "

                                    #region " Claim Related Identification REF Segment "
                                    //For Claim Related Identification
                                    else if (oSegment.get_DataElementValue(1) == "1L")//Group or Policy Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1W")//Member identification number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "9A")//Repriced claim reference number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "9C")//Adjusted Repriced claim reference number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "A6")//Employee identification number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "BB")//Authorization Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "EA")//Medical Record identification Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "F8")//Original Reference Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "G1")//Prior Authorization Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "IG")//Insurance Policy Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "SY")//Social Security Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Related Identification REF Segment "
                                }
                                else if (sSegmentID == "DTM")
                                {
                                    #region " Claim Dates DTM Segment "
                                    if (oSegment.get_DataElementValue(1) == "232")
                                    {
                                        if (oSegment.get_DataElementValue(2) != "")
                                        {
                                            oClaim.ClaimStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2).ToString())).ToShortDateString();
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "233")
                                    {
                                        if (oSegment.get_DataElementValue(3) != "")
                                        {
                                            oClaim.ClaimEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3).ToString())).ToShortDateString();
                                        }
                                    }
                                    #endregion " Claim Dates DTM Segment "
                                }
                                else if (sSegmentID == "AMT")
                                {
                                    #region " Claim Supplemental Info AMT Segment "
                                    if (oSegment.get_DataElementValue(1) == "AU")
                                    {
                                        oClaim.CoverageAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "I")
                                    {
                                        oClaim.InterestAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "T")
                                    {
                                        oClaim.TaxAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "F5")
                                    {
                                        oClaim.PatientPaidAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Supplemental Info AMT Segment "
                                }
                                else if (sSegmentID == "MIA")
                                {
                                    //if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                                    //{
                                    //}
                                }
                            }
                            else if (sLoopSection == "LX;CLP;SVC")
                            {
                                if (sSegmentID == "SVC")
                                {
                                    oClaimServiceLines.Add(oClaimServiceLine);
                                    oClaimServiceLine = new ClaimServiceLine();
                                    _Index++;
                                    _nArea2RowCount = 0;

                                    #region " Claim Service Line SVC Segment "
                                    if (oSegment.get_DataElementValue(1, 1) == "HC")
                                    {
                                        sValue = oSegment.get_DataElementValue(1, 1);
                                        oClaimServiceLines[_Index - 1].ServiceProcedureCode = oSegment.get_DataElementValue(1, 2).ToString();
                                        oClaimServiceLines[_Index - 1].ServiceModifier1 = oSegment.get_DataElementValue(1, 3).ToString();
                                        oClaimServiceLines[_Index - 1].ServiceModifier2 = oSegment.get_DataElementValue(1, 4).ToString();
                                        oClaimServiceLines[_Index - 1].LineItemAmount = oSegment.get_DataElementValue(2).ToString();
                                        oClaimServiceLines[_Index - 1].LineProviderPaymentAmount = oSegment.get_DataElementValue(3).ToString();
                                    }
                                    #endregion " Claim Service Line SVC Segment "

                                }
                                else if (sSegmentID == "DTM")
                                {
                                    #region " Claim Service Line Date DTM Segment "
                                    if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        if (oSegment.get_DataElementValue(1) != "")
                                        {
                                            oClaimServiceLines[_Index - 1].ServiceDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                        }
                                    }
                                    #endregion " Claim Service Line Date DTM Segment "
                                }
                                else if (sSegmentID == "CAS")
                                {
                                    #region " Claim Service Line CAS Segment "
                                    if (oSegment.get_DataElementValue(1) == "CO")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                                        oClaimServiceLines[_Index - 1].ServiceLineContractualObligation = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PR")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLinePatientResponsibility = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PI")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLinePayerInitiatedReduction = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "OA")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLineOtherAdjustments = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    #endregion " Claim Service Line CAS Segment "
                                }
                                else if (sSegmentID == "AMT")
                                {
                                    #region " Claim Service Line AMT Segment "
                                    if (oSegment.get_DataElementValue(1) == "B6")
                                    {
                                        oClaimServiceLines[_Index - 1].LineAllowedAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Service Line AMT Segment "
                                }
                                else if (sSegmentID == "REF")
                                {
                                    #region " Claim Service Line REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "6R")
                                    {
                                        oClaimServiceLines[_Index - 1].ServiceProviderControlNo = oSegment.get_DataElementValue(2);
                                    }
                                    if (oSegment.get_DataElementValue(1) == "LU")
                                    {
                                        //oClaimServiceLines[_Index - 1].ServiceProcedureCode = oSegment.get_DataElementValue(2);
                                    }
                                    #endregion " Claim Service Line REF Segment "
                                }
                                else if (sSegmentID == "LQ")
                                {
                                    #region " LQ Segment "
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    #endregion " LQ Segment "
                                }
                            }
                        }
                        else if (sLXID == "")// It was 961213
                        {
                            #region " Commented Code "
                            //if (sLoopSection == "LX")
                            //{
                            //    if (sSegmentID == "TS3")
                            //    {
                            //        //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                            //        //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                            //        //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                            //        //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                            //        //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                            //        //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                            //    }
                            //    else if (sSegmentID == "TS2")
                            //    {
                            //        //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                            //        //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                            //        //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                            //        //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                            //        //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                            //    }
                            //}
                            //else if (sLoopSection == "LX;CLP")
                            //{
                            //    if (sSegmentID == "CLP")
                            //    {
                            //        oPatientRemit = new PatientRemit();
                            //        if (_nArea2RowCount == 0)
                            //        {
                            //            C1Claim835Data.Rows.Add();
                            //            rowIndex = C1Claim835Data.Rows.Count - 1;
                            //            _nArea2RowCount++;
                            //            i++;
                            //        }
                            //        oPatientRemit.PatientControlNo = oSegment.get_DataElementValue(1);
                            //        oPatientRemit.ClaimStatus = oSegment.get_DataElementValue(2);
                            //        oPatientRemit.TotalClaimAmount = oSegment.get_DataElementValue(3);
                            //        oPatientRemit.ClaimPaymentAmount = oSegment.get_DataElementValue(4);
                            //        sValue = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                            //        sValue = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                            //        oPatientRemit.PayerControlNumber = oSegment.get_DataElementValue(7);
                            //        sValue = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                            //        sValue = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                            //        sValue = oSegment.get_DataElementValue(12);  //Quantity (380) 
                            //        sValue = oSegment.get_DataElementValue(13); // Percent (954) 
                            //    }
                            //    else if (sSegmentID == "CAS")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "CO")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                            //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PR")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.PatientResposibility = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PI")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "OA")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.OtherAdjustments = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }

                            //    }
                            //    else if (sSegmentID == "NM1")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "QC")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                            //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                            //            oPatientRemit.PatientLName=oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                            //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                            //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                            //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                            //            oPatientRemit.PatientID=oSegment.get_DataElementValue(9).ToString();
                            //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                            //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                            //        }
                            //        if (oSegment.get_DataElementValue(1) == "IL")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                            //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                            //            oPatientRemit.SubscriberLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                            //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                            //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                            //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                            //            oPatientRemit.SubscriberID = oSegment.get_DataElementValue(9).ToString();
                            //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                            //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                            //        }
                            //    }
                            //    else if (sSegmentID == "MIA")
                            //    {
                            //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                            //        {
                            //        }
                            //    }
                            //    else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "1A")//Blue Cross Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1B")//Blue Shield Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1C")//Medicare Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1D")//Medicaid Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1G")//Provider UPIN Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1H")//CHAMPUS Identification Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "G2")//Provider Commercial Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }

                            //        //For Claim Related Identification
                            //        else if (oSegment.get_DataElementValue(1) == "1L")//Group or Policy Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1W")//Member identification number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "9A")//Repriced claim reference number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "9C")//Adjusted Repriced claim reference number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "A6")//Employee identification number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "BB")//Authorization Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "EA")//Medical Record identification Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "F8")//Original Reference Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "G1")//Prior Authorization Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "IG")//Insurance Policy Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "SY")//Social Security Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "DTM")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "232")
                            //        {
                            //            if (oSegment.get_DataElementValue(2) != "")
                            //            {
                            //                oPatientRemit.ClaimStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2).ToString())).ToShortDateString();
                            //            }
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "233")
                            //        {
                            //            if (oSegment.get_DataElementValue(3) != "")
                            //            {
                            //                oPatientRemit.ClaimEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3).ToString())).ToShortDateString();
                            //            }

                            //        }
                            //    }
                            //    else if (sSegmentID == "AMT")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "AU")
                            //        {
                            //            oPatientRemit.CoverageAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "I")
                            //        {
                            //            oPatientRemit.InterestAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "T")
                            //        {
                            //            oPatientRemit.TaxAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "F5")
                            //        {
                            //            oPatientRemit.PatientPaidAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "MIA")
                            //    {
                            //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                            //        {
                            //        }
                            //    }

                            //}
                            //else if (sLoopSection == "LX;CLP;SVC")
                            //{
                            //    if (sSegmentID == "SVC")
                            //    {
                            //        oPatientRemitServiceLine = new PatientRemitServiceLine();
                            //        _nArea2RowCount = 0;
                            //        if (oSegment.get_DataElementValue(1, 1) == "HC")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1, 1);
                            //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(1, 2).ToString();
                            //            oPatientRemitServiceLine.ServiceModifier1 = oSegment.get_DataElementValue(1, 3).ToString();
                            //            oPatientRemitServiceLine.ServiceModifier2 = oSegment.get_DataElementValue(1, 4).ToString();
                            //            oPatientRemitServiceLine.LineItemAmount = oSegment.get_DataElementValue(2).ToString();
                            //            oPatientRemitServiceLine.LineProviderPaymentAmount = oSegment.get_DataElementValue(3).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "DTM")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "472")
                            //        {
                            //            if (oSegment.get_DataElementValue(1) != "")
                            //            {
                            //                oPatientRemitServiceLine.ServiceDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                            //            }
                            //        }
                            //    }
                            //    else if (sSegmentID == "CAS")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "CO")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                            //            oPatientRemitServiceLine.ServiceLineContractualObligation = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PR")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLinePatientResponsibility = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PI")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLinePayerInitiatedReduction = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "OA")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLineOtherAdjustments = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //    }
                            //    else if (sSegmentID == "AMT")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "B6")
                            //        {
                            //            oPatientRemitServiceLine.LineAllowedAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "REF")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "6R")
                            //        {
                            //            oPatientRemitServiceLine.ServiceProviderControlNo = oSegment.get_DataElementValue(2);
                            //        }
                            //        if (oSegment.get_DataElementValue(1) == "LU")
                            //        {
                            //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(2);
                            //        }
                            //    }

                            //}
                            #endregion " Commented Code "
                        }

                    }//Area==2
                    else if (nArea == 3)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "PLB")
                            {
                                #region " PLB Segment "
                                oClaim.ProviderID = oSegment.get_DataElementValue(1);// Reference Identification (127) 
                                if (oSegment.get_DataElementValue(2) != "")
                                {
                                    oClaim.FiscalDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                }
                                sValue = oSegment.get_DataElementValue(3, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(3, 2);  // Reference Identification (127) 
                                oClaim.ProviderAdjustment = oSegment.get_DataElementValue(4);
                                sValue = oSegment.get_DataElementValue(5, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(5, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(6);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(7, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(7, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(8);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(9, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(9, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(10);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(11, 1);   // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(11, 2);   // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(12);   // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(13, 1);   // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(13, 2);   // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(14);   // Monetary Amount (782) 

                                #endregion " PLB Segment "
                            }
                        }
                    }

                    //Get next segment
                    //Use the set method of the object to dispose of the previous instance of the object before instantiation
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                    if (sSegmentID != "IEA")
                    {
                        sSegmentID = oSegment.ID;
                        sLoopSection = oSegment.LoopSection;
                        nArea = oSegment.Area;
                        if (oSegment.ID == "CLP" || oSegment.ID == "SE")
                        {
                            if (oClaimServiceLines != null && oClaimServiceLines.Count > 0)
                            {
                                oClaim.ClaimServiceLines = oClaimServiceLines;
                                oRemittance.Claims.Add(oClaim);
                                //oClaims.Add(oRemittance);
                                oClaimServiceLines = new ClaimServiceLines();
                                oClaimServiceLine = new ClaimServiceLine();
                                oClaim = new Claim();
                            }
                        }
                    }
                }

                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.
                #region " Read Acknowledgement which got created "
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
                FileInfo oFile = new FileInfo(EDIFileName);
                string _FileName = oFile.Name.Replace(".RMT", "_997.txt");

                string sDirectoryPath = ClsGeneralClaimManager.PM_ClaimManagement_OutBox_997Acknowledgement;
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.GetEdiString();
                oAck.Save(sDirectoryPath + "\\" + _FileName);
                //System.Collections.ArrayList _remittances = (System.Collections.ArrayList)oPatientRemittances;

                #endregion " Read Acknowledgement which got created "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (EdiDoc != null)
                {
                    EdiDoc.Dispose();
                }
                if (oSegment != null)
                {
                    oSegment.Dispose();
                }
                if (oAck != null)
                {
                    oAck.Dispose();
                }
                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }
                if (oSchema != null)
                {
                    oSchema.Dispose();
                }

            }
            return oRemittance;
        }

        public Int64 SaveRemittance(Remittance oRemittance)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _objectID = null;
            Int64 _RemitID = 0;
            try
            {
                //nRemitID, sPaymentMethod, sSenderID, sSenderAccountNo, sProviderBankID, sProviderAccountNo, 
                //sCheckIssueDate, sCheckNo, sPayerID, 
                //sProductionDate, sPayerName, sPayerAddress, sPayerCity, sPayerState, sPayerZip, sAdditionalPayerID, 
                //sPayeeName, sPayeeCity, sPayeeState, sPayeeZip, sPayeeFedralTaxID, nPaymentProcessed, nClinicID

                oDB.Connect(false);
                string PayerName_Date = oRemittance.PayerName + " " + oRemittance.ProductionDate.ToString() + " " + oRemittance.CheckNumber.ToString();
                oDBParameters.Add("@nRemitID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@sPaymentMethod", oRemittance.PaymentMethod, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sSenderID", oRemittance.SenderID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sSenderAccountNo", oRemittance.SenderAccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sProviderBankID", oRemittance.ProviderBankID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sProviderAccountNo", oRemittance.ProviderAccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCheckIssueDate", oRemittance.CheckIssueDate, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCheckNo", oRemittance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerID", oRemittance.PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sProductionDate", oRemittance.ProductionDate, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerName", PayerName_Date, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerAddress", oRemittance.PayerAddress, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerCity", oRemittance.PayerCity, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerState", oRemittance.PayerState, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerZip", oRemittance.PayerZip, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAdditionalPayerID", oRemittance.AdditionalPayerID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayeeName", oRemittance.PayeeName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayeeCity", oRemittance.PayeeCity, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayeeState", oRemittance.PayeeState, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayeeZip", oRemittance.PayeeZip, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayeeFedralTaxID", oRemittance.PayeeFederalTaxID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nPaymentProcessed", RemittanceProcessed.Processed.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nClinicID", ClsGeneralClaimManager.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_Transaction_Remittance_MST", oDBParameters, out  _objectID);

                if (_objectID == null)
                { return 0; }
                _RemitID = (Int64)_objectID;


                for (int _Index = 0; _Index < oRemittance.Claims.Count; _Index++)
                {
                    //nRemitID, sClaimNumber, sClaimStatus, sTotalClaimAmount, sClaimPaymentAmount, sPayerControlNumber, 
                    //sContractualObligation, sCorrectionReversals, sOtherAdjustments, sPatientResponsibility, sInsuranceID, 
                    //sClaimStartDate, sClaimEndDate, sCoverageAmount, sDiscountAmount, sPatientPaidAmount, sInterestAmount,
                    //sTaxAmount, sOtherClaimID, sRenderingProviderID, sProviderID, sFiscalDate, sProviderAdjustment, nClinicID
                    oDBParameters.Clear();
                    oDBParameters.Add("@nRemitID", _RemitID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sClaimNumber", oRemittance.Claims[_Index].ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sClaimStatus", oRemittance.Claims[_Index].ClaimStatus, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sTotalClaimAmount", oRemittance.Claims[_Index].TotalClaimAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sClaimPaymentAmount", oRemittance.Claims[_Index].ClaimPaymentAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPayerControlNumber", oRemittance.Claims[_Index].PayerControlNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sContractualObligation", oRemittance.Claims[_Index].ContractualObligation, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sCorrectionReversals", oRemittance.Claims[_Index].CorrectionReversals, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sOtherAdjustments", oRemittance.Claims[_Index].OtherAdjustments, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPatientResponsibility", oRemittance.Claims[_Index].PatientResposibility, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sInsuranceID", oRemittance.Claims[_Index].SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sClaimStartDate", oRemittance.Claims[_Index].ClaimStartDate, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sClaimEndDate", oRemittance.Claims[_Index].ClaimEndDate, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sCoverageAmount", oRemittance.Claims[_Index].CoverageAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sDiscountAmount", oRemittance.Claims[_Index].DiscountAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPatientPaidAmount", oRemittance.Claims[_Index].PatientPaidAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sInterestAmount", oRemittance.Claims[_Index].InterestAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sTaxAmount", oRemittance.Claims[_Index].TaxAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sOtherClaimID", oRemittance.Claims[_Index].OtherClaimID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sRenderingProviderID", oRemittance.Claims[_Index].RenderingProviderID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sProviderID", oRemittance.Claims[_Index].ProviderID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sFiscalDate", oRemittance.Claims[_Index].FiscalDate, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sProviderAdjustment", oRemittance.Claims[_Index].ProviderAdjustment, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nClinicID", ClsGeneralClaimManager.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@TotalCoinsuranceAmount", oRemittance.Claims[_Index].TotalCoinsuranceAmount, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@TotalDeductibleAmount", oRemittance.Claims[_Index].TotalDeductibleAmount, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Execute("BL_INSERT_Transaction_Remittance_Claims", oDBParameters);


                    for (int _LineIndex = 0; _LineIndex < oRemittance.Claims[_Index].ClaimServiceLines.Count; _LineIndex++)
                    {
                        //nRemitID, sClaimNumber, sLineAmount, sLineProviderPayment, sLineAllowedAmount, 
                        //sServiceDate, sServiceProcedureCode, sModifier1, sModifier2, sLineContractualObligation, 
                        //sLineCorrectionReversal, sLinePayerInitiatedReduction, sLinePatientResponsibility, 
                        //sLineOtherAdjustments, sServiceProviderControlNo, sServiceLocation, nClinicID
                        oDBParameters.Clear();
                        oDBParameters.Add("@nRemitID", _RemitID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@sClaimNumber", oRemittance.Claims[_Index].ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineAmount", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].LineItemAmount, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineProviderPayment", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].LineProviderPaymentAmount, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineAllowedAmount", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].LineAllowedAmount, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sServiceDate", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceDate, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sServiceProcedureCode", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceProcedureCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sModifier1", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceModifier1, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sModifier2", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceModifier2, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineContractualObligation", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLineContractualObligation, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineCorrectionReversal", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLineCorrectionReversal, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLinePayerInitiatedReduction", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLinePayerInitiatedReduction, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLinePatientResponsibility", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLinePatientResponsibility, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sLineOtherAdjustments", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLineOtherAdjustments, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sServiceProviderControlNo", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceProviderControlNo, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sServiceLocation", oRemittance.Claims[_Index].ClaimServiceLines[_LineIndex].ServiceLocationOrPOS, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nClinicID", ClsGeneralClaimManager.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Execute("BL_INSERT_Transaction_Remittance_ClaimLines", oDBParameters);

                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                _IsError = true;
            }
            return _RemitID;
        }

        //Added By MaheshB for ProcessAll
        public Remittance ReadRemittancesAll(string EDIFileName)
        {
            string sSegmentID = "";
            string sLoopSection = "";
            string sLXID = "";
            string sEntity = "";
            string sEdiFile = "";
            int nArea;
            string sValue = "";
            Int32 _nArea2RowCount = 0;
            int Area2rowIndex = 0;
            int rowIndex = 0;
            int i = 0;
            int _Index = 0;
            Claim oClaim = new Claim();
            ClaimServiceLine oClaimServiceLine = new ClaimServiceLine();
            ClaimServiceLines oClaimServiceLines = new ClaimServiceLines();
            Claims oClaims = new Claims();
            Remittance oRemittance = new Remittance();

            try
            {
                EdiDoc.LoadSchema("997_X12-4010.SEF", 0);    //for Ack (997) file

                EdiDoc = new ediDocument();

                //// If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                //// property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                ////// Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                //Set the cursor type to write an acknowledgement file.
                //EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                ////// Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                ////// using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");


                if (EDIFileName.Trim() != "")
                {
                    EdiDoc.LoadEdi(EDIFileName);
                }
                else
                {
                    return null;
                }


                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)EdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) EdiDoc.FirstDataSegment

                // This loop iterates though the EDI file a segment at a time
                while (oSegment != null)
                {
                    // A segment is identified by its Area number, Loop section and segment id.
                    //sSegmentID = oSegment.ID;
                    //sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                // map data elements of ISA segment in here
                                #region " ISA Segment "
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
                                #endregion " ISA Segment "
                            }
                            else if (sSegmentID == "GS")
                            {
                                #region " GS Segment "
                                // map data elements of GS segment in here
                                sValue = oSegment.get_DataElementValue(1);     //Functional Identifier Code
                                sValue = oSegment.get_DataElementValue(2);    //Application Sender's Code
                                sValue = oSegment.get_DataElementValue(3);    //Application Receiver's Code
                                sValue = oSegment.get_DataElementValue(4);   //Date
                                sValue = oSegment.get_DataElementValue(5);   //Time
                                sValue = oSegment.get_DataElementValue(6);  //Group Control Number
                                sValue = oSegment.get_DataElementValue(7);  //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8);   //Version / Rele
                                #endregion " GS Segment "
                            }
                        }
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                #region " ST Segment "
                                // map data element of ST segment in here
                                sValue = oSegment.get_DataElementValue(1);     //Transaction Set Identifier Code
                                sValue = oSegment.get_DataElementValue(2);     //Transaction Set Control Number
                                #endregion " ST Segment "
                            }
                            else if (sSegmentID == "BPR")
                            {
                                oRemittance = new Remittance();
                                #region " BPR Segment "
                                if (oSegment.get_DataElementValue(1).ToString() == "I")//It was "C" changed to "I"
                                {
                                    oRemittance.ProviderPayment = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(2);  //Monetary Amount
                                    sValue = oSegment.get_DataElementValue(3);  ////////Credit/Debit Flag Code
                                    oRemittance.PaymentMethod = oSegment.get_DataElementValue(4).ToString();
                                    sValue = oSegment.get_DataElementValue(4);   //Payment Method Code
                                    sValue = oSegment.get_DataElementValue(5);    //Payment Format Code
                                    sValue = oSegment.get_DataElementValue(6);  //(DFI) ID Number Qualifier
                                    oRemittance.SenderID = oSegment.get_DataElementValue(7).ToString();
                                    sValue = oSegment.get_DataElementValue(8);    //Account Number Qualifier
                                    oRemittance.SenderAccountNo = oSegment.get_DataElementValue(9).ToString();
                                    oRemittance.PayerID = oSegment.get_DataElementValue(10).ToString();
                                    sValue = oSegment.get_DataElementValue(11);   //Originating Company Supplemental Code
                                    sValue = oSegment.get_DataElementValue(12);   //(DFI) ID Number Qualifier
                                    oRemittance.ProviderBankID = oSegment.get_DataElementValue(13).ToString();
                                    sValue = oSegment.get_DataElementValue(14);     //Account Number Qualifier
                                    oRemittance.ProviderAccountNo = oSegment.get_DataElementValue(15).ToString();

                                    if (oSegment.get_DataElementValue(16) != "")
                                    {
                                        oRemittance.CheckIssueDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(16))).ToShortDateString();
                                    }

                                }
                                #endregion " BPR Segment "
                            }
                            else if (sSegmentID == "TRN")
                            {
                                #region " TRN Segment "
                                oRemittance.CheckNumber = oSegment.get_DataElementValue(2).ToString();
                                oRemittance.PayerID = oSegment.get_DataElementValue(3);
                                #endregion " TRN Segment "
                            }
                            else if (sSegmentID == "DTM")
                            {
                                #region " DTM Segment "
                                sValue = oSegment.get_DataElementValue(1);    //Date/Time Qualifier
                                if (oSegment.get_DataElementValue(2) != "")
                                {
                                    oRemittance.ProductionDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                }
                                #endregion " DTM Segment "
                            }

                        } // sLoopSection == ""

                        else if (sLoopSection == "N1")
                        {
                            if (sSegmentID == "N1")
                            {
                                sEntity = oSegment.get_DataElementValue(1); //get loop entity qualifier to identity each N1 loop instances
                            }

                            if (sEntity == "PR") //payer information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payer N1 Segment "
                                    sValue = oSegment.get_DataElementValue(1);  // Entity Identifier Code (98) 
                                    oRemittance.PayerName = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(3);  // Identification Code Qualifier (66) 
                                    sValue = oSegment.get_DataElementValue(4);  // Identification Code (67) 
                                    sValue = oSegment.get_DataElementValue(5);  // Entity Relationship Code (706) 
                                    sValue = oSegment.get_DataElementValue(6);  // Entity Identifier Code (98) 
                                    #endregion " Payer N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payer N3 Segment "
                                    oRemittance.PayerAddress = oSegment.get_DataElementValue(1).ToString();
                                    sValue = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payer N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payer N4 Segment "
                                    oRemittance.PayerCity = oSegment.get_DataElementValue(1).ToString();
                                    oRemittance.PayerState = oSegment.get_DataElementValue(2).ToString();
                                    oRemittance.PayerZip = oSegment.get_DataElementValue(3).ToString();
                                    sValue = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    sValue = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    sValue = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    #endregion " Payer N4 Segment "
                                }
                                else if (sSegmentID == "REF")
                                {
                                    #region " Payer REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "EO")//It was "2U" changed to "EO"
                                    {
                                        oRemittance.AdditionalPayerID = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    if (oSegment.get_DataElementValue(1) == "2U")//
                                    {
                                        oRemittance.AdditionalPayerID = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Payer REF Segment "
                                }
                            }
                            else if (sEntity == "PE")//payee information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payee N1 Segment "
                                    sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                    oRemittance.PayeeName = oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(3);   //Identification Code Qualifier (66) 
                                    oRemittance.PayeeFederalTaxID = oSegment.get_DataElementValue(4).ToString();//It could be NPI of Provider
                                    sValue = oSegment.get_DataElementValue(5); // Entity Relationship Code (706) 
                                    sValue = oSegment.get_DataElementValue(6); // Entity Identifier Code (98) 
                                    #endregion " Payee N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payee N3 Segment "
                                    oRemittance.PayeeAddress = oSegment.get_DataElementValue(1).ToString() + oSegment.get_DataElementValue(2).ToString();
                                    sValue = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payee N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payee N4 Segment "
                                    oRemittance.PayeeCity = oSegment.get_DataElementValue(1).ToString();
                                    oRemittance.PayeeState = oSegment.get_DataElementValue(2).ToString();
                                    oRemittance.PayeeZip = oSegment.get_DataElementValue(3).ToString();
                                    sValue = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    sValue = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    sValue = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    #endregion " Payee N4 Segment "
                                }
                            }//sEntity

                        }//sLoopSection

                    } // nArea == 1

                    else if (nArea == 2)
                    {
                        if (sSegmentID == "LX")
                        {
                            sLXID = oSegment.get_DataElementValue(1);
                        }

                        if (sLXID == "1" || sLXID == "0")//It was "961221" changed to "1"
                        {
                            if (sLoopSection == "LX")
                            {
                                if (sSegmentID == "TS3")
                                {
                                    #region " TS3 Segment "
                                    oClaim.TotalCoinsuranceAmount = oSegment.get_DataElementValue(16);//oClaim.TotalCoinsuranceAmount = "10";//Added By MaheshB
                                    oClaim.TotalDeductibleAmount = oSegment.get_DataElementValue(19); //oClaim.TotalDeductibleAmount = "2";
                                    //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                                    //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                                    //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                                    //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                                    //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                                    //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                                    #endregion " TS3 Segment "
                                }
                                else if (sSegmentID == "TS2")
                                {
                                    #region " TS2 Segment "
                                    //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                                    //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                                    //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                                    //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                                    //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                                    #endregion " TS2 Segment "
                                }
                            }
                            else if (sLoopSection == "LX;CLP")
                            {
                                if (sSegmentID == "CLP")
                                {
                                    //oClaim = new Claim();
                                    _Index = 0;
                                    if (_nArea2RowCount == 0)
                                    {

                                        _nArea2RowCount++;
                                        i++;
                                    }
                                    #region " CLP Segment "
                                    oClaim.ClaimNumber = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);  // Claim Status Code (1029) 
                                    oClaim.ClaimStatus = oSegment.get_DataElementValue(2);
                                    oClaim.TotalClaimAmount = oSegment.get_DataElementValue(3);
                                    oClaim.ClaimPaymentAmount = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                                    sValue = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                                    oClaim.PayerControlNumber = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                                    sValue = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                                    sValue = oSegment.get_DataElementValue(12);  //Quantity (380) 
                                    sValue = oSegment.get_DataElementValue(13); // Percent (954) 
                                    #endregion " CLP Segment "
                                }
                                else if (sSegmentID == "CAS")
                                {
                                    #region " CAS Segment "
                                    if (oSegment.get_DataElementValue(1) == "CO")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                                        oClaim.ContractualObligation = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PR")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.PatientResposibility = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PI")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.ContractualObligation = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "OA")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaim.OtherAdjustments = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    #endregion " CAS Segment "
                                }
                                else if (sSegmentID == "NM1")
                                {
                                    if (oSegment.get_DataElementValue(1) == "QC")
                                    {
                                        #region " Patient NM1 Segment "
                                        sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                        sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                                        oClaim.PatientLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                                        sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                                        sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                                        sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                                        oClaim.PatientID = oSegment.get_DataElementValue(9).ToString();
                                        sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                                        sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                                        #endregion " Patient NM1 Segment "
                                    }
                                    if (oSegment.get_DataElementValue(1) == "IL")
                                    {
                                        #region " Subscriber NM1 Segment "
                                        sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                        sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                                        oClaim.SubscriberLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                                        sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                                        sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                                        sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                                        oClaim.SubscriberID = oSegment.get_DataElementValue(9).ToString();
                                        sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                                        sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                                        #endregion " Subscriber NM1 Segment "
                                    }
                                }
                                else if (sSegmentID == "MIA")
                                {
                                    if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                                    {
                                    }
                                }
                                else if (sSegmentID == "MOA")
                                {

                                }
                                else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                                {
                                    #region " Rendering Provider REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "1A")//Blue Cross Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1B")//Blue Shield Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1C")//Medicare Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1D")//Medicaid Provider Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1G")//Provider UPIN Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1H")//CHAMPUS Identification Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "G2")//Provider Commercial Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Rendering Provider REF Segment "

                                    #region " Claim Related Identification REF Segment "
                                    //For Claim Related Identification
                                    else if (oSegment.get_DataElementValue(1) == "1L")//Group or Policy Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "1W")//Member identification number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "9A")//Repriced claim reference number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "9C")//Adjusted Repriced claim reference number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "A6")//Employee identification number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "BB")//Authorization Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "EA")//Medical Record identification Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "F8")//Original Reference Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "G1")//Prior Authorization Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "IG")//Insurance Policy Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "SY")//Social Security Number
                                    {
                                        oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Related Identification REF Segment "
                                }
                                else if (sSegmentID == "DTM")
                                {
                                    #region " Claim Dates DTM Segment "
                                    if (oSegment.get_DataElementValue(1) == "232")
                                    {
                                        if (oSegment.get_DataElementValue(2) != "")
                                        {
                                            oClaim.ClaimStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2).ToString())).ToShortDateString();
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "233")
                                    {
                                        if (oSegment.get_DataElementValue(3) != "")
                                        {
                                            oClaim.ClaimEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3).ToString())).ToShortDateString();
                                        }
                                    }
                                    #endregion " Claim Dates DTM Segment "
                                }
                                else if (sSegmentID == "AMT")
                                {
                                    #region " Claim Supplemental Info AMT Segment "
                                    if (oSegment.get_DataElementValue(1) == "AU")
                                    {
                                        oClaim.CoverageAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "I")
                                    {
                                        oClaim.InterestAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "T")
                                    {
                                        oClaim.TaxAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "F5")
                                    {
                                        oClaim.PatientPaidAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Supplemental Info AMT Segment "
                                }
                                else if (sSegmentID == "MIA")
                                {
                                    //if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                                    //{
                                    //}
                                }
                            }
                            else if (sLoopSection == "LX;CLP;SVC")
                            {
                                if (sSegmentID == "SVC")
                                {
                                    oClaimServiceLines.Add(oClaimServiceLine);
                                    oClaimServiceLine = new ClaimServiceLine();
                                    _Index++;
                                    _nArea2RowCount = 0;

                                    #region " Claim Service Line SVC Segment "
                                    if (oSegment.get_DataElementValue(1, 1) == "HC")
                                    {
                                        sValue = oSegment.get_DataElementValue(1, 1);
                                        oClaimServiceLines[_Index - 1].ServiceProcedureCode = oSegment.get_DataElementValue(1, 2).ToString();
                                        oClaimServiceLines[_Index - 1].ServiceModifier1 = oSegment.get_DataElementValue(1, 3).ToString();
                                        oClaimServiceLines[_Index - 1].ServiceModifier2 = oSegment.get_DataElementValue(1, 4).ToString();
                                        oClaimServiceLines[_Index - 1].LineItemAmount = oSegment.get_DataElementValue(2).ToString();
                                        oClaimServiceLines[_Index - 1].LineProviderPaymentAmount = oSegment.get_DataElementValue(3).ToString();
                                    }
                                    #endregion " Claim Service Line SVC Segment "

                                }
                                else if (sSegmentID == "DTM")
                                {
                                    #region " Claim Service Line Date DTM Segment "
                                    if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        if (oSegment.get_DataElementValue(1) != "")
                                        {
                                            oClaimServiceLines[_Index - 1].ServiceDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                        }
                                    }
                                    #endregion " Claim Service Line Date DTM Segment "
                                }
                                else if (sSegmentID == "CAS")
                                {
                                    #region " Claim Service Line CAS Segment "
                                    if (oSegment.get_DataElementValue(1) == "CO")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                                        oClaimServiceLines[_Index - 1].ServiceLineContractualObligation = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PR")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLinePatientResponsibility = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "PI")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLinePayerInitiatedReduction = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "OA")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);  // 
                                        oClaimServiceLines[_Index - 1].ServiceLineOtherAdjustments = oSegment.get_DataElementValue(3).ToString();
                                        sValue = oSegment.get_DataElementValue(4);   // 
                                    }
                                    #endregion " Claim Service Line CAS Segment "
                                }
                                else if (sSegmentID == "AMT")
                                {
                                    #region " Claim Service Line AMT Segment "
                                    if (oSegment.get_DataElementValue(1) == "B6")
                                    {
                                        oClaimServiceLines[_Index - 1].LineAllowedAmount = oSegment.get_DataElementValue(2).ToString();
                                    }
                                    #endregion " Claim Service Line AMT Segment "
                                }
                                else if (sSegmentID == "REF")
                                {
                                    #region " Claim Service Line REF Segment "
                                    if (oSegment.get_DataElementValue(1) == "6R")
                                    {
                                        oClaimServiceLines[_Index - 1].ServiceProviderControlNo = oSegment.get_DataElementValue(2);
                                    }
                                    if (oSegment.get_DataElementValue(1) == "LU")
                                    {
                                        //oClaimServiceLines[_Index - 1].ServiceProcedureCode = oSegment.get_DataElementValue(2);
                                    }
                                    #endregion " Claim Service Line REF Segment "
                                }
                                else if (sSegmentID == "LQ")
                                {
                                    #region " LQ Segment "
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    #endregion " LQ Segment "
                                }
                            }
                        }
                        else if (sLXID == "")// It was 961213
                        {
                            #region " Commented Code "
                            //if (sLoopSection == "LX")
                            //{
                            //    if (sSegmentID == "TS3")
                            //    {
                            //        //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                            //        //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                            //        //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                            //        //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                            //        //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                            //        //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                            //    }
                            //    else if (sSegmentID == "TS2")
                            //    {
                            //        //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                            //        //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                            //        //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                            //        //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                            //        //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                            //    }
                            //}
                            //else if (sLoopSection == "LX;CLP")
                            //{
                            //    if (sSegmentID == "CLP")
                            //    {
                            //        oPatientRemit = new PatientRemit();
                            //        if (_nArea2RowCount == 0)
                            //        {
                            //            C1Claim835Data.Rows.Add();
                            //            rowIndex = C1Claim835Data.Rows.Count - 1;
                            //            _nArea2RowCount++;
                            //            i++;
                            //        }
                            //        oPatientRemit.PatientControlNo = oSegment.get_DataElementValue(1);
                            //        oPatientRemit.ClaimStatus = oSegment.get_DataElementValue(2);
                            //        oPatientRemit.TotalClaimAmount = oSegment.get_DataElementValue(3);
                            //        oPatientRemit.ClaimPaymentAmount = oSegment.get_DataElementValue(4);
                            //        sValue = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                            //        sValue = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                            //        oPatientRemit.PayerControlNumber = oSegment.get_DataElementValue(7);
                            //        sValue = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                            //        sValue = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                            //        sValue = oSegment.get_DataElementValue(12);  //Quantity (380) 
                            //        sValue = oSegment.get_DataElementValue(13); // Percent (954) 
                            //    }
                            //    else if (sSegmentID == "CAS")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "CO")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                            //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PR")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.PatientResposibility = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PI")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "OA")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemit.OtherAdjustments = oSegment.get_DataElementValue(3);
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }

                            //    }
                            //    else if (sSegmentID == "NM1")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "QC")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                            //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                            //            oPatientRemit.PatientLName=oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                            //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                            //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                            //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                            //            oPatientRemit.PatientID=oSegment.get_DataElementValue(9).ToString();
                            //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                            //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                            //        }
                            //        if (oSegment.get_DataElementValue(1) == "IL")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                            //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                            //            oPatientRemit.SubscriberLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                            //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                            //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                            //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                            //            oPatientRemit.SubscriberID = oSegment.get_DataElementValue(9).ToString();
                            //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                            //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                            //        }
                            //    }
                            //    else if (sSegmentID == "MIA")
                            //    {
                            //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                            //        {
                            //        }
                            //    }
                            //    else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "1A")//Blue Cross Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1B")//Blue Shield Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1C")//Medicare Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1D")//Medicaid Provider Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1G")//Provider UPIN Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1H")//CHAMPUS Identification Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "G2")//Provider Commercial Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }

                            //        //For Claim Related Identification
                            //        else if (oSegment.get_DataElementValue(1) == "1L")//Group or Policy Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "1W")//Member identification number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "9A")//Repriced claim reference number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "9C")//Adjusted Repriced claim reference number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "A6")//Employee identification number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "BB")//Authorization Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "EA")//Medical Record identification Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "F8")//Original Reference Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "G1")//Prior Authorization Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "IG")//Insurance Policy Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "SY")//Social Security Number
                            //        {
                            //            oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "DTM")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "232")
                            //        {
                            //            if (oSegment.get_DataElementValue(2) != "")
                            //            {
                            //                oPatientRemit.ClaimStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2).ToString())).ToShortDateString();
                            //            }
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "233")
                            //        {
                            //            if (oSegment.get_DataElementValue(3) != "")
                            //            {
                            //                oPatientRemit.ClaimEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3).ToString())).ToShortDateString();
                            //            }

                            //        }
                            //    }
                            //    else if (sSegmentID == "AMT")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "AU")
                            //        {
                            //            oPatientRemit.CoverageAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "I")
                            //        {
                            //            oPatientRemit.InterestAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "T")
                            //        {
                            //            oPatientRemit.TaxAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "F5")
                            //        {
                            //            oPatientRemit.PatientPaidAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "MIA")
                            //    {
                            //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                            //        {
                            //        }
                            //    }

                            //}
                            //else if (sLoopSection == "LX;CLP;SVC")
                            //{
                            //    if (sSegmentID == "SVC")
                            //    {
                            //        oPatientRemitServiceLine = new PatientRemitServiceLine();
                            //        _nArea2RowCount = 0;
                            //        if (oSegment.get_DataElementValue(1, 1) == "HC")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(1, 1);
                            //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(1, 2).ToString();
                            //            oPatientRemitServiceLine.ServiceModifier1 = oSegment.get_DataElementValue(1, 3).ToString();
                            //            oPatientRemitServiceLine.ServiceModifier2 = oSegment.get_DataElementValue(1, 4).ToString();
                            //            oPatientRemitServiceLine.LineItemAmount = oSegment.get_DataElementValue(2).ToString();
                            //            oPatientRemitServiceLine.LineProviderPaymentAmount = oSegment.get_DataElementValue(3).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "DTM")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "472")
                            //        {
                            //            if (oSegment.get_DataElementValue(1) != "")
                            //            {
                            //                oPatientRemitServiceLine.ServiceDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                            //            }
                            //        }
                            //    }
                            //    else if (sSegmentID == "CAS")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "CO")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                            //            oPatientRemitServiceLine.ServiceLineContractualObligation = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PR")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLinePatientResponsibility = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "PI")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLinePayerInitiatedReduction = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //        else if (oSegment.get_DataElementValue(1) == "OA")
                            //        {
                            //            sValue = oSegment.get_DataElementValue(2);  // 
                            //            oPatientRemitServiceLine.ServiceLineOtherAdjustments = oSegment.get_DataElementValue(3).ToString();
                            //            sValue = oSegment.get_DataElementValue(4);   // 
                            //        }
                            //    }
                            //    else if (sSegmentID == "AMT")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "B6")
                            //        {
                            //            oPatientRemitServiceLine.LineAllowedAmount = oSegment.get_DataElementValue(2).ToString();
                            //        }
                            //    }
                            //    else if (sSegmentID == "REF")
                            //    {
                            //        if (oSegment.get_DataElementValue(1) == "6R")
                            //        {
                            //            oPatientRemitServiceLine.ServiceProviderControlNo = oSegment.get_DataElementValue(2);
                            //        }
                            //        if (oSegment.get_DataElementValue(1) == "LU")
                            //        {
                            //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(2);
                            //        }
                            //    }

                            //}
                            #endregion " Commented Code "
                        }

                    }//Area==2
                    else if (nArea == 3)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "PLB")
                            {
                                #region " PLB Segment "
                                oClaim.ProviderID = oSegment.get_DataElementValue(1);// Reference Identification (127) 
                                if (oSegment.get_DataElementValue(2) != "")
                                {
                                    oClaim.FiscalDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                                }
                                sValue = oSegment.get_DataElementValue(3, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(3, 2);  // Reference Identification (127) 
                                oClaim.ProviderAdjustment = oSegment.get_DataElementValue(4);
                                sValue = oSegment.get_DataElementValue(5, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(5, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(6);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(7, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(7, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(8);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(9, 1);  // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(9, 2);  // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(10);  // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(11, 1);   // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(11, 2);   // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(12);   // Monetary Amount (782) 
                                sValue = oSegment.get_DataElementValue(13, 1);   // Adjustment Reason Code (426) 
                                sValue = oSegment.get_DataElementValue(13, 2);   // Reference Identification (127) 
                                sValue = oSegment.get_DataElementValue(14);   // Monetary Amount (782) 

                                #endregion " PLB Segment "
                            }
                        }
                    }

                    //Get next segment
                    //Use the set method of the object to dispose of the previous instance of the object before instantiation
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                    if (sSegmentID != "IEA")
                    {
                        sSegmentID = oSegment.ID;
                        sLoopSection = oSegment.LoopSection;
                        nArea = oSegment.Area;
                        if (oSegment.ID == "CLP" || oSegment.ID == "SE")
                        {
                            if (oClaimServiceLines != null && oClaimServiceLines.Count > 0)
                            {
                                oClaim.ClaimServiceLines = oClaimServiceLines;
                                oRemittance.Claims.Add(oClaim);
                                //oClaims.Add(oRemittance);
                                oClaimServiceLines = new ClaimServiceLines();
                                oClaimServiceLine = new ClaimServiceLine();
                                oClaim = new Claim();
                            }
                        }
                    }
                }

                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.
                #region " Read Acknowledgement which got created "
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
                FileInfo oFile = new FileInfo(EDIFileName);
                string _FileName = oFile.Name.Replace(".RMT", "_997.txt");

                string sDirectoryPath = ClsGeneralClaimManager.PM_ClaimManagement_OutBox_997Acknowledgement;
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.GetEdiString();
                oAck.Save(sDirectoryPath + "\\" + _FileName);
                //System.Collections.ArrayList _remittances = (System.Collections.ArrayList)oPatientRemittances;

                #endregion " Read Acknowledgement which got created "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsError = true;
            }
            finally
            {
                if (EdiDoc != null)
                {
                    EdiDoc.Dispose();
                }
                if (oSegment != null)
                {
                    oSegment.Dispose();
                }
                if (oAck != null)
                {
                    oAck.Dispose();
                }
                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }
                if (oSchema != null)
                {
                    oSchema.Dispose();
                }

            }
            return oRemittance;
        }

        public void TranslateEDI277ClaimStatus(string EDIFileName)
        {
            string strClaimNo = "";
            string strStatusCategory = "";
            string strStatus = "";
            string strSQL = "";
            string strClaimMessage = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Int64 _result = 0;
            string _strFileName = "";
            try
            {
                string sSegmentID = "";
                string sLoopSection = "";
                string sLXID = "";
                string sPath = "";
                string sEntity = "";
                string Qlfr = "";

                string strRejectionCode = "";
                string strFollowupCode = "";

                int nArea;
                string sLoopQlfr = "";
                string sValue = "";

                EdiDoc = new ediDocument();

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                if (EDIFileName.Trim() != "")
                {
                    EdiDoc.LoadEdi(EDIFileName);
                    oDB.Connect(false);
                    FileInfo oFileinfo = new FileInfo(EDIFileName);
                    _strFileName = oFileinfo.Name;
                }
                else
                {
                    return;
                }

                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)EdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) EdiDoc.FirstDataSegment

                while (oSegment != null)
                {    //DATA SEGMENTS WILL BE IDENTIFIED BY THEIR ID, THE LOOP SECTION AND AREA
                    //(OR TABLE) NUMBER THAT THEY ARE IN.
                    sSegmentID = oSegment.ID;
                    sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Authorization Information Qualifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Authorization Information
                                sValue = oSegment.get_DataElementValue(3, 0);     //Security Information Qualifier
                                sValue = oSegment.get_DataElementValue(4, 0);     //Security Information
                                sValue = oSegment.get_DataElementValue(5, 0);     //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(6, 0);     //Interchange Sender ID
                                sValue = oSegment.get_DataElementValue(7, 0);     //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(8, 0);     //Interchange Receiver ID
                                sValue = oSegment.get_DataElementValue(9, 0);     //Interchange Date
                                sValue = oSegment.get_DataElementValue(10, 0);     //Interchange Time
                                sValue = oSegment.get_DataElementValue(11, 0);     //Interchange Control Standards Identifier
                                sValue = oSegment.get_DataElementValue(12, 0);     //Interchange Control Version Number
                                sValue = oSegment.get_DataElementValue(13, 0);     //Interchange Control Number
                                sValue = oSegment.get_DataElementValue(14, 0);     //Acknowledgment Requested
                                sValue = oSegment.get_DataElementValue(15, 0);     //Usage Indicator
                                sValue = oSegment.get_DataElementValue(16, 0);     //Component Element Separator
                            }
                            else if (sSegmentID == "GS")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Functional Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Application Sender's Code
                                sValue = oSegment.get_DataElementValue(3, 0);     //Application Receiver's Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Date
                                sValue = oSegment.get_DataElementValue(5, 0);     //Time
                                sValue = oSegment.get_DataElementValue(6, 0);     //Group Control Number
                                sValue = oSegment.get_DataElementValue(7, 0);     //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8, 0);     //Version / Release / Industry Identifier Code
                            }   //sSegmentID
                        }    //sLoopSection
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Transaction Set Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Transaction Set Control Number
                            }
                            else if (sSegmentID == "BHT")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Hierarchical Structure Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Transaction Set Purpose Code
                                sValue = oSegment.get_DataElementValue(3, 0);     //Reference Identification
                                sValue = oSegment.get_DataElementValue(4, 0);     //Date
                                sValue = oSegment.get_DataElementValue(5, 0);     //Time
                                sValue = oSegment.get_DataElementValue(6, 0);     //Transaction Type Code
                            }   //sSegmentID
                        }    //sLoopSection
                    }
                    else if (nArea == 2)
                    {
                        if (sLoopSection == "HL")
                        {
                            //if loop has more that one instance, then you should check for the qualifier that differentiates the loop instances here e.g.
                            //if (sSegmentID == "HL")
                            //{
                            //    sLoopHLQlfr = oSegment.DataElementValue(3);  //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
                            //}
                            //if (sLoopHLQlfr == "QualifierValue")
                            //{

                            if (sSegmentID == "HL")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                sValue = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                sValue = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code
                            }   //Segment ID
                            else if (sSegmentID == "DMG")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Date Quaifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date of Birth
                                sValue = oSegment.get_DataElementValue(3, 0);     //Gender
                            }
                        }
                        else if (sLoopSection == "HL;NM1")
                        {
                            //if loop has more that one instance, then you should check for the qualifier that differentiates the loop instances here e.g.
                            if (sSegmentID == "NM1")
                            {
                                sLoopQlfr = oSegment.get_DataElementValue(1);   //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
                            }
                            if (sLoopQlfr == "PR")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "41")
                            {

                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "1P")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "QC")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                        }
                        else if (sLoopSection == "HL;TRN")
                        {
                            if (sSegmentID == "TRN")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Trace Type Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Reference Identification
                                strClaimNo = oSegment.get_DataElementValue(2, 0);

                            }
                            else if (sSegmentID == "STC")
                            {
                                sValue = oSegment.get_DataElementValue(1, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(1, 2);     //Industry Code

                                strStatusCategory = oSegment.get_DataElementValue(1, 1);
                                strStatus = oSegment.get_DataElementValue(1, 2);

                                sValue = oSegment.get_DataElementValue(1, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date
                                sValue = oSegment.get_DataElementValue(3, 0);     //Action Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Monetary Amount
                                sValue = oSegment.get_DataElementValue(5, 0);     //Monetary Amount
                                sValue = oSegment.get_DataElementValue(6, 0);     //Date
                                sValue = oSegment.get_DataElementValue(7, 0);     //Payment Method Code
                                sValue = oSegment.get_DataElementValue(8, 0);     //Date
                                sValue = oSegment.get_DataElementValue(9, 0);     //Check Number
                                sValue = oSegment.get_DataElementValue(10, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(10, 2);     //Industry Code
                                sValue = oSegment.get_DataElementValue(10, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(11, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(11, 2);     //Industry Code
                                sValue = oSegment.get_DataElementValue(11, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(12, 0);     //Free-Form Message Text
                                strClaimMessage = oSegment.get_DataElementValue(12, 0);

                                strSQL = "INSERT INTO BL_ClaimStatusResponse (sFileName,sClaimNo, sClaimCategory, sClaimStatus, sClaimMessage) " +
                                         " VALUES     ('" + _strFileName + "','" + strClaimNo + "', '" + strStatusCategory + "', '" + strStatus + "', '" + strClaimMessage.Replace("'", "''") + "') ";

                                _result = oDB.Execute_Query(strSQL);
                            }
                            else if (sSegmentID == "DTP")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Date/Time Qualifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date Time Period Format Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Date Time Period
                            }   //sSegmentID

                        }  //sLoopSection

                    }   //nArea

                    //GETS THE NEXT DATA SEGMENT
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());  //oSegment = (ediDataSegment) oSegment.Next();

                }//while

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
                //string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "997_277\\";
                //if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                //oAck.Save(sDirectoryPath + "997_270.X12");
                FileInfo oFile = new FileInfo(EDIFileName);
                string _FileName = oFile.Name.Replace(".277", "_277_997.txt");

                string sDirectoryPath = ClsGeneralClaimManager.PM_ClaimManagement_OutBox_997Acknowledgement;
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.GetEdiString();
                oAck.Save(sDirectoryPath + "\\" + _FileName);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        public void Load277EDIObject()
        {
            try
            {
                string sSefFile = "";

                ediDocument.Set(ref EdiDoc, new ediDocument());    // EdiDoc = new ediDocument();
                //sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSefFile = "277_X093A1.SEF";
                //sEdiFile = "277.X12";
                // Disabling the internal standard reference library to makes sure that 
                // FREDI uses only the SEF file provided
                ediSchemas.Set(ref oSchemas, (ediSchemas)EdiDoc.GetSchemas());    //oSchemas = (ediSchemas) EdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                EdiDoc.LoadSchema(sSefFile, 0);
                EdiDoc.LoadSchema("997_X12-4010.SEF", 0);	//for Ack (997) file

                EdiDoc = new ediDocument();
                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");


                // Loads EDI file and the corresponding SEF file

                //OpenFileDialog oDialog = new OpenFileDialog();
                //if (oDialog.ShowDialog() == DialogResult.OK)
                //{
                //    string _FileName = "";
                //    _FileName = oDialog.FileName;
                //    if (System.IO.File.Exists(_FileName) == true)
                //    {
                //        sEdiFile = _FileName;
                //        EdiDoc.LoadEdi(sEdiFile);
                //    }
                //}

                //sEdiFile = "EligibilityResponse.X12";
                //EdiDoc.LoadEdi(sPath + sEdiFile);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public void LoadAcknowledgementEDI()
        {
            try
            {
                //string _ServerPath = this.ServerPath;
                //string _BaseFolder = "Claim Management";
                //string _OutInFolder = "Inbox";
                //string _ClaimFolder = "997 Acknowledgement";
                //string _claimFolderPath = "";


                //_claimFolderPath = _ServerPath + "\\" + _BaseFolder + "\\" + _OutInFolder + "\\" + _ClaimFolder;

                //if (System.IO.Directory.Exists(_claimFolderPath) == false)
                //{
                //    MessageBox.Show("Path for acknowledgement file does not exist", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{

                //    string[] _AcknowledgementFiles = System.IO.Directory.GetFiles(_claimFolderPath, "*.997");
                //    if (_AcknowledgementFiles != null && _AcknowledgementFiles.Length > 0)
                //    {
                //        sEdiFile = _AcknowledgementFiles[0];
                //        if (File.Exists(sEdiFile) == true)
                //        {
                //            oEdiDoc.LoadSchema("997_X12-4010.SEF", 0);
                //            oEdiDoc.New();
                //            oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);
                //            oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;
                //            oEdiDoc.LoadEdi(sEdiFile);
                //        }
                //        else
                //        { MessageBox.Show("997 EDI File does not exist", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //    }
                //    else
                //    { MessageBox.Show("No new acknowledgement files found.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public int ReadAcknowledgement(string EDIFileName)
        {
            string sSegmentID = "";
            string sLoopSection = "";
            string sLXID = "";
            string sPath = "";
            string sEntity = "";
            string Qlfr = "";
            string strRejectionCode = "";
            string strFollowupCode = "";
            int nArea;
            string sValue = "";
            string _strFileName = "";
            Acknowledgment Objack = null;
            string _FileType = "";
            int _result = 0;
            try
            {
                EdiDoc = new ediDocument();

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                //Added By MaheshB
                Objack = new Acknowledgment();

                if (EDIFileName.Trim() != "")
                {
                    EdiDoc.LoadEdi(EDIFileName);
                    FileInfo oFileinfo = new FileInfo(EDIFileName);
                    _strFileName = oFileinfo.Name;
                }
                else
                {
                    return 0;
                }
                // Gets the first segment of the 997 acknowledgment file
                ediDataSegment.Set(ref oSegment, (ediDataSegment)EdiDoc.FirstDataSegment);
                Objack.sAcknowledgmentCode = "A";
                while (oSegment != null)
                {
                    nArea = oSegment.Area;
                    sLoopSection = oSegment.LoopSection;
                    sSegmentID = oSegment.ID;

                    if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            switch (sSegmentID)
                            {
                                case "AK9":
                                    {
                                        #region " Read Segment AK9 "

                                        if (oSegment.get_DataElementValue(1, 0) != null && oSegment.get_DataElementValue(1, 0).Trim() != "")
                                        {

                                            sValue += oSegment.get_DataElementValue(1) + "*";
                                            sValue += oSegment.get_DataElementValue(2) + "*";
                                            sValue += oSegment.get_DataElementValue(3) + "*";
                                            sValue += oSegment.get_DataElementValue(4);

                                            if (oSegment.get_DataElementValue(1, 0).ToUpper() == "R")
                                            {
                                                Objack.sAcknowledgmentCode = "R";
                                                //MessageBox.Show("Rejected", "Claim Management", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                            }
                                            else if (oSegment.get_DataElementValue(1, 0).ToUpper() == "A")
                                            {
                                                //Objack.sAcknowledgmentCode = "A";
                                                //MessageBox.Show("Accepted", "Claim Management", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                            }
                                        }

                                        #endregion " Read Segment AK9 "
                                    }
                                    break;
                                case "AK1":
                                    {
                                        sValue += oSegment.get_DataElementValue(1) + "*";
                                        sValue += oSegment.get_DataElementValue(2) + Environment.NewLine;
                                        Objack.nGroupControlNumber = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                    }
                                    break;
                                case "AK2":
                                    {
                                        sValue += oSegment.get_DataElementValue(1) + "*";
                                        sValue += oSegment.get_DataElementValue(2) + Environment.NewLine;
                                        //_FileType=oSegment.get_DataElementValue(2);
                                    }
                                    break;
                                case "AK5":
                                    {
                                        sValue += oSegment.get_DataElementValue(1) + Environment.NewLine;
                                        if (oSegment.get_DataElementValue(1, 0).ToUpper() == "R")
                                        {
                                            Objack.sAcknowledgmentCode = "R";
                                            //MessageBox.Show("Rejected", "Claim Management", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                        }
                                        else if (oSegment.get_DataElementValue(1, 0).ToUpper() == "A")
                                        {
                                            //Objack.sAcknowledgmentCode = "A";
                                            //MessageBox.Show("Accepted", "Claim Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    break;



                            }

                        }	// sLoopSection == ""
                        else if (sLoopSection == "AK2")
                        {
                            if (oSegment.get_DataElementValue(1, 0) == "837")
                            {
                                _FileType = oSegment.get_DataElementValue(1, 0);
                            }
                        }
                    }
                    else
                    {
                        if (sSegmentID == "GS")
                        {
                            Objack.sAcknowledgmentDate = oSegment.get_DataElementValue(4);
                        }
                        //break;  //Obj         ect object=new Object();
                    }//nArea == 1
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                }	//oSegment != null

                //Save AcknowledgmentData

                if (_FileType == "837")
                {
                    _result = SaveAcknowledgementData(Objack);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _result;
        }

        //Added By MaheshB
        public int SaveAcknowledgementData(Acknowledgment Objack)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            int _result = 0;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string strquery = String.Empty;
                strquery = "Insert into BL_ClaimAcknowledgment(nGroupControlNumber, sAcknowledgmentCode, sAcknowledgmentDate) values ('" + Objack.nGroupControlNumber + "','" + Objack.sAcknowledgmentCode + "','" + Objack.sAcknowledgmentDate + "')";
                _result = ODB.Execute_Query(strquery);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
            }
            return _result;
        }
        #endregion "  Private And Public Methods "
    }

    public class Remittance : IDisposable
    {

        #region " Constructor & Destructor "

        public Remittance()
        {
            _Claims = new Claims();
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

        ~Remittance()
        {
            Dispose(false);
        }

        #endregion " Constructor & Destructor "

        #region " Private variables "

        private string _PaymentMethod = "";
        private string _SenderID = "";
        private string _SenderAccountNo = "";
        private string _ProviderBankID = "";
        private string _ProviderAccountNo = "";
        private string _CheckIssueDate = "";
        private string _CheckNumber = "";
        private string _PayerID = "";
        private string _ProductionDate = "";
        private string _PayerName = "";
        private string _PayerAddress = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";
        private string _AdditionalPayerID = "";
        private string _PayeeName = "";
        private string _PayeeAddress = "";
        private string _PayeeFederalTaxID = "";
        private string _PayeeCity = "";
        private string _PayeeState = "";
        private string _PayeeZip = "";
        private string _ProviderPayment = "";
        private RemittanceProcessed _RemittanceProcessed;
        private Claims _Claims = null;

        #endregion " Private variables "

        #region " Property Procedures "

        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
        public string SenderID
        {
            get { return _SenderID; }
            set { _SenderID = value; }
        }
        public string SenderAccountNo
        {
            get { return _SenderAccountNo; }
            set { _SenderAccountNo = value; }
        }
        public string ProviderBankID
        {
            get { return _ProviderBankID; }
            set { _ProviderBankID = value; }
        }
        public string ProviderAccountNo
        {
            get { return _ProviderAccountNo; }
            set { _ProviderAccountNo = value; }
        }
        public string CheckIssueDate
        {
            get { return _CheckIssueDate; }
            set { _CheckIssueDate = value; }
        }
        public string CheckNumber
        {
            get { return _CheckNumber; }
            set { _CheckNumber = value; }
        }
        public string PayerID
        {
            get { return _PayerID; }
            set { _PayerID = value; }
        }
        public string ProductionDate
        {
            get { return _ProductionDate; }
            set { _ProductionDate = value; }
        }
        public string PayerName
        {
            get { return _PayerName; }
            set { _PayerName = value; }
        }
        public string PayerAddress
        {
            get { return _PayerAddress; }
            set { _PayerAddress = value; }
        }
        public string PayerCity
        {
            get { return _PayerCity; }
            set { _PayerCity = value; }
        }
        public string PayerState
        {
            get { return _PayerState; }
            set { _PayerState = value; }
        }
        public string PayerZip
        {
            get { return _PayerZip; }
            set { _PayerZip = value; }
        }
        public string AdditionalPayerID
        {
            get { return _AdditionalPayerID; }
            set { _AdditionalPayerID = value; }
        }
        public string PayeeName
        {
            get { return _PayeeName; }
            set { _PayeeName = value; }
        }
        public string PayeeAddress
        {
            get { return _PayeeAddress; }
            set { _PayeeAddress = value; }
        }
        public string PayeeFederalTaxID
        {
            get { return _PayeeFederalTaxID; }
            set { _PayeeFederalTaxID = value; }
        }
        public string PayeeCity
        {
            get { return _PayeeCity; }
            set { _PayeeCity = value; }
        }
        public string PayeeState
        {
            get { return _PayeeState; }
            set { _PayeeState = value; }
        }
        public string PayeeZip
        {
            get { return _PayeeZip; }
            set { _PayeeZip = value; }
        }
        public string ProviderPayment
        {
            get { return _ProviderPayment; }
            set { _ProviderPayment = value; }
        }
        public Claims Claims
        {
            get { return _Claims; }
            set { _Claims = value; }
        }
        public RemittanceProcessed RemittanceProcessed
        {
            get { return _RemittanceProcessed; }
            set { _RemittanceProcessed = value; }
        }

        #endregion " Property Procedures "

    }

    public class Claim : IDisposable
    {
        #region " Constructor & Destructor "

        public Claim()
        {
            _PatientServiceLines = new ClaimServiceLines();
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

        ~Claim()
        {
            Dispose(false);
        }

        #endregion " Constructor & Destructor "

        #region " Private variables "

        private string _ClaimNo = "";
        private string _ClaimStatus = "";
        private string _TotalClaimAmount = "";
        private string _ClaimPaymentAmount = "";
        private string _PayerControlNumber = "";
        private string _ContractualObligation = "";
        private string _CorrectionReversals = "";
        private string _OtherAdjustments = "";
        private string _PatientResposibility = "";
        private string _PatientFName = "";
        private string _PatientLName = "";
        private string _PatientMName = "";
        private string _PatientID = "";
        private string _SubscriberFName = "";
        private string _SubscriberMName = "";
        private string _SubscriberLName = "";
        private string _SubscriberID = "";
        private string _ClaimStartDate = "";
        private string _ClaimEndDate = "";
        private string _CoverageAmount = "";
        private string _DiscountAmount = "";
        private string _PatientPaidAmount = "";
        private string _InterestAmount = "";
        private string _TaxAmount = "";
        private string _OtherClaimID = "";
        private string _RenderingProviderID = "";
        private string _ProviderID = "";
        private string _FiscalDate = "";
        private string _ProviderAdjustment = "";
        private string _TotalCoinsuranceAmount = "";//Added By MaheshB
        private string _TotalDeductibleAmount = "";


        private ClaimServiceLines _PatientServiceLines = null;

        #endregion " Private variables "

        #region " Property Procedures "

        public ClaimServiceLines ClaimServiceLines
        {
            get { return _PatientServiceLines; }
            set { _PatientServiceLines = value; }
        }
        public string ClaimNumber
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }
        public string ClaimStatus
        {
            get { return _ClaimStatus; }
            set { _ClaimStatus = value; }
        }
        public string TotalClaimAmount
        {
            get { return _TotalClaimAmount; }
            set { _TotalClaimAmount = value; }
        }
        public string ClaimPaymentAmount
        {
            get { return _ClaimPaymentAmount; }
            set { _ClaimPaymentAmount = value; }
        }
        public string PayerControlNumber
        {
            get { return _PayerControlNumber; }
            set { _PayerControlNumber = value; }
        }
        public string ContractualObligation
        {
            get { return _ContractualObligation; }
            set { _ContractualObligation = value; }
        }
        public string CorrectionReversals
        {
            get { return _CorrectionReversals; }
            set { _CorrectionReversals = value; }
        }
        public string OtherAdjustments
        {
            get { return _OtherAdjustments; }
            set { _OtherAdjustments = value; }
        }
        public string PatientResposibility
        {
            get { return _PatientResposibility; }
            set { _PatientResposibility = value; }
        }
        public string PatientFName
        {
            get { return _PatientFName; }
            set { _PatientFName = value; }
        }
        public string PatientLName
        {
            get { return _PatientLName; }
            set { _PatientLName = value; }
        }
        public string PatientMName
        {
            get { return _PatientMName; }
            set { _PatientMName = value; }
        }
        public string PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public string SubscriberFName
        {
            get { return _SubscriberFName; }
            set { _SubscriberFName = value; }
        }
        public string SubscriberLName
        {
            get { return _SubscriberLName; }
            set { _SubscriberLName = value; }
        }
        public string SubscriberMName
        {
            get { return _SubscriberMName; }
            set { _SubscriberMName = value; }
        }
        public string SubscriberID
        {
            get { return _SubscriberID; }
            set { _SubscriberID = value; }
        }
        public string ClaimStartDate
        {
            get { return _ClaimStartDate; }
            set { _ClaimStartDate = value; }
        }
        public string ClaimEndDate
        {
            get { return _ClaimEndDate; }
            set { _ClaimEndDate = value; }
        }
        public string CoverageAmount
        {
            get { return _CoverageAmount; }
            set { _CoverageAmount = value; }
        }
        public string DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        public string PatientPaidAmount
        {
            get { return _PatientPaidAmount; }
            set { _PatientPaidAmount = value; }
        }
        public string InterestAmount
        {
            get { return _InterestAmount; }
            set { _InterestAmount = value; }
        }
        public string TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
        }
        public string OtherClaimID
        {
            get { return _OtherClaimID; }
            set { _OtherClaimID = value; }
        }
        public string RenderingProviderID
        {
            get { return _RenderingProviderID; }
            set { _RenderingProviderID = value; }
        }
        public string ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        public string ProviderAdjustment
        {
            get { return _ProviderAdjustment; }
            set { _ProviderAdjustment = value; }
        }
        public string FiscalDate
        {
            get { return _FiscalDate; }
            set { _FiscalDate = value; }
        }
        public string TotalCoinsuranceAmount
        {
            get { return _TotalCoinsuranceAmount; }
            set { _TotalCoinsuranceAmount = value; }
        }
        public string TotalDeductibleAmount
        {
            get { return _TotalDeductibleAmount; }
            set { _TotalDeductibleAmount = value; }
        }
        #endregion " Property Procedures "

    }

    public class ClaimServiceLine : IDisposable
    {
        #region " Constructor & Destructor "

        public ClaimServiceLine()
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

        ~ClaimServiceLine()
        {
            Dispose(false);
        }

        #endregion " Constructor & Destructor "

        #region " Private variables "

        private string _LineItemAmount = "";
        private string _LineProviderPaymentAmount = "";
        private string _LineAllowedAmount = "";
        private string _ServiceDate = "";
        private string _ServiceProcedureCode = "";
        private string _ServiceModifier1 = "";
        private string _ServiceModifier2 = "";
        private string _ServiceLineContractualObligation = "";
        private string _ServiceLineCorrectionReversal = "";
        private string _ServiceLinePayerInitiatedReduction = "";
        private string _ServiceLinePatientResponsibility = "";
        private string _ServiceLineOtherAdjustments = "";
        private string _ServiceProviderControlNo = "";
        private string _ServiceLocationOrPOS = "";

        #endregion " Private variables "

        #region " Property Procedure "

        public string LineProviderPaymentAmount
        {
            get { return _LineProviderPaymentAmount; }
            set { _LineProviderPaymentAmount = value; }
        }
        public string LineItemAmount
        {
            get { return _LineItemAmount; }
            set { _LineItemAmount = value; }
        }
        public string LineAllowedAmount
        {
            get { return _LineAllowedAmount; }
            set { _LineAllowedAmount = value; }
        }
        public string ServiceDate
        {
            get { return _ServiceDate; }
            set { _ServiceDate = value; }
        }
        public string ServiceProcedureCode
        {
            get { return _ServiceProcedureCode; }
            set { _ServiceProcedureCode = value; }
        }
        public string ServiceModifier1
        {
            get { return _ServiceModifier1; }
            set { _ServiceModifier1 = value; }
        }
        public string ServiceModifier2
        {
            get { return _ServiceModifier2; }
            set { _ServiceModifier2 = value; }
        }
        public string ServiceLineContractualObligation
        {
            get { return _ServiceLineContractualObligation; }
            set { _ServiceLineContractualObligation = value; }
        }
        public string ServiceLineCorrectionReversal
        {
            get { return _ServiceLineCorrectionReversal; }
            set { _ServiceLineCorrectionReversal = value; }
        }
        public string ServiceLineOtherAdjustments
        {
            get { return _ServiceLineOtherAdjustments; }
            set { _ServiceLineOtherAdjustments = value; }
        }
        public string ServiceLinePatientResponsibility
        {
            get { return _ServiceLinePatientResponsibility; }
            set { _ServiceLinePatientResponsibility = value; }
        }
        public string ServiceLinePayerInitiatedReduction
        {
            get { return _ServiceLinePayerInitiatedReduction; }
            set { _ServiceLinePayerInitiatedReduction = value; }
        }
        public string ServiceProviderControlNo
        {
            get { return _ServiceProviderControlNo; }
            set { _ServiceProviderControlNo = value; }
        }
        public string ServiceLocationOrPOS
        {
            get { return _ServiceLocationOrPOS; }
            set { _ServiceLocationOrPOS = value; }
        }

        #endregion " Property Procedure "
    }

    public class ClaimServiceLines : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public ClaimServiceLines()
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


        ~ClaimServiceLines()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ClaimServiceLine item)
        {
            _innerlist.Add(item);
        }

        //Remark - Work Remaining for comparision
        public bool Remove(ClaimServiceLine item)
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

        public ClaimServiceLine this[int index]
        {
            get
            { return (ClaimServiceLine)_innerlist[index]; }
        }

        public bool Contains(ClaimServiceLine item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(ClaimServiceLine item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(ClaimServiceLine[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class Claims : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Claims()
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


        ~Claims()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Claim item)
        {
            _innerlist.Add(item);
        }

        //Remark - Work Remaining for comparision
        public bool Remove(Claim item)
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

        public Claim this[int index]
        {
            get
            { return (Claim)_innerlist[index]; }
        }

        public bool Contains(Claim item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Claim item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Claim[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class Acknowledgment
    {
        #region private variables

        Int64 _nGroupControlNumber;
        string _sAcknowledgmentCode = "";
        string _sAcknowledgmentDate = "";

        #endregion

        #region Properties

        public Int64 nGroupControlNumber
        {
            //get { return _nGroupControlNumber;}
            //set ( _nGroupControlNumber = value ;)

            get { return _nGroupControlNumber; }
            set { _nGroupControlNumber = value; }
        }

        public string sAcknowledgmentCode
        {
            get { return _sAcknowledgmentCode; }
            set { _sAcknowledgmentCode = value; }
        }

        public string sAcknowledgmentDate
        {
            //get { return _nGroupControlNumber;}
            //set ( _nGroupControlNumber = value ;)

            get { return _sAcknowledgmentDate; }
            set { _sAcknowledgmentDate = value; }
        }

        #endregion

        #region Constructor

        public Acknowledgment()
        {

        }

        #endregion
    }
}
