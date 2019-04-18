using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;
using Edidev.FrameworkEDI;
using System.Windows.Forms;
using System.IO;
namespace gloRxHub
{
    public partial class frmRxhub270EDIGeneration : Form
    {
        #region " Constructors "


        public frmRxhub270EDIGeneration()
        {
            InitializeComponent();
        }
        private gloRxHub.ClsPatient oPatient = null;
        // IDisposable


        public gloRxHub.ClsPatient Patient
        {
            get { return oPatient; }
            set { oPatient = value; }
        }

        public frmRxhub270EDIGeneration(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            InitializeComponent();
        }

        #endregion " Constructors "

        #region " Public And Private variables "

        gloRxHub.ClsRxHubInterface oClsRxHubInterface;

        public string strFileName;
        public string strEDIFileName;
        string _databaseconnectionstring = "";
        string _messageboxcaption = "gloEMR";
        string sPath = "";
        string sSEFFile = "";
        string sEdiFile = "";
        ediDocument oEdiDoc = null;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionset = null;
        ediDataSegment oSegment = null;
        ediSchema oSchema = null;
        ediSchemas oSchemas = null;
        //ediWarnings oWarnings = null;
        //ediWarning oWarning = null;
        //ediAcknowledgment oAck = null;

        public delegate void HandleToolStripClick(string strFileName);
        public event HandleToolStripClick ToolStrip_Click;

        #endregion " Public And Private variables "

        #region " Procedures And Functions "

        //public void Generate270EDI()
        //{
        //    try
        //    {
        //        string sEntity = "";
        //        string sInstance = "";

        //        oEdiDoc.New();
        //        oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
        //        oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

        //        oEdiDoc.SegmentTerminator = "~\r\n";
        //        oEdiDoc.ElementTerminator = "*";
        //        oEdiDoc.CompositeTerminator = ":";


        //        #region " Interchange Segment "
        //        //Create the interchange segment
        //        gloRxHub.ClsgloRxHubGeneral.UpdateLog("STARTED Creating the interchange segment");
        //        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
        //        gloRxHub.ClsgloRxHubGeneral.UpdateLog("COMPLETED Creating the interchange segment");
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

        //        oSegment.set_DataElementValue(1, 0, "00");
        //        oSegment.set_DataElementValue(3, 0, "01");
        //        //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
        //        //From RxHub, this is the password for RxHub to get to the POC/PPMS 
        //        oSegment.set_DataElementValue(4, 0, "Password");
        //        oSegment.set_DataElementValue(5, 0, "ZZ");
        //        //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
        //        //From RxHub, this is the RxHub's participant ID
        //        oSegment.set_DataElementValue(6, 0, "Sender ID");//
        //        oSegment.set_DataElementValue(7, 0, "ZZ");
        //        //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
        //        //From RxHub, this is the PBM's participant ID
        //        oSegment.set_DataElementValue(8, 0, "RXHUB");
        //        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
        //        oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
        //        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
        //        oSegment.set_DataElementValue(11, 0, "U");
        //        oSegment.set_DataElementValue(12, 0, "00401");
        //        oSegment.set_DataElementValue(13, 0, "000000020");
        //        oSegment.set_DataElementValue(14, 0, "1");
        //        oSegment.set_DataElementValue(15, 0, "T");
        //        oSegment.set_DataElementValue(16, 0, ":");

        //        #endregion " Interchange Segment "

        //        #region " Functional Group "

        //        //Create the functional group segment
        //        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(1, 0, "HS");
        //        oSegment.set_DataElementValue(2, 0, "Sender ID");
        //        oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
        //        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
        //        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
        //        oSegment.set_DataElementValue(6, 0, "1");
        //        oSegment.set_DataElementValue(7, 0, "X");
        //        oSegment.set_DataElementValue(8, 0, "004010X092A1");

        //        #endregion " Functional Group "

        //        #region "Transaction Set "
        //        //HEADER
        //        //ST TRANSACTION SET HEADER
        //        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(2, 0, "00021");

        //        #endregion "Transaction Set "

        //        #region " BHT "

        //        //Begining Segment 
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
        //        oSegment.set_DataElementValue(1, 0, "0022");
        //        oSegment.set_DataElementValue(2, 0, "13");//13=Request
        //        oSegment.set_DataElementValue(3, 0, "10001234");//Submitter Transaction Identifier
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
        //        string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
        //        oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

        //        #endregion " BHT "

        //        #region " Information Source "

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //        oSegment.set_DataElementValue(1, 0, "1");
        //        oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
        //        oSegment.set_DataElementValue(4, 0, "1");

        //        //INFORMATION SOURCE NAME 
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

        //        oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
        //        oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
        //        oSegment.set_DataElementValue(3, 0, "RxHub LLC");//"INFORMATION SOURCE NAME" );//"PBM"
        //        oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
        //        oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID

        //        #endregion " Information Source "

        //        #region " Receiver Loop "

        //        //INFORMATION RECEIVER LEVEL
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "2");
        //        oSegment.set_DataElementValue(2, 0, "1");
        //        oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
        //        oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

        //        //INFORMATION RECEIVER NAME (It is the medical service Provider)
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
        //        oSegment.set_DataElementValue(2, 0, "1");//1=Person

        //        //"Provider L Name"
        //        if (oPatient.Provider.ProviderLastName != null && oPatient.Provider.ProviderLastName != "")
        //        {
        //            oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName); //"Provider L Name");//Provider  LastName
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(3, 0, ""); //"Provider L Name");//Provider  LastName
        //        }

        //        //"Provider F Name"
        //        if (oPatient.Provider.ProviderFirstName != null && oPatient.Provider.ProviderFirstName != "")
        //        {
        //            oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"Provider F Name");//Provider FirstName
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(4, 0, "");//"Provider F Name");//Provider FirstName
        //        }

        //        oSegment.set_DataElementValue(5, 0, "");//Provider M Name
        //        oSegment.set_DataElementValue(8, 0, "SV");//SV=Service Provider Number

        //        // "DEA Number"
        //        if (oPatient.Provider.ProviderDEA != null && oPatient.Provider.ProviderDEA != "")
        //        {
        //            oSegment.set_DataElementValue(9, 0, oPatient.Provider.ProviderDEA);// "DEA Number");//Service Provider No
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(9, 0, "");// "DEA Number");//Service Provider No
        //        }


        //        //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
        //        oSegment.set_DataElementValue(1, 0, "EO");
        //        oSegment.set_DataElementValue(2, 0, "Provider SSN");

        //        //INFORMATION RECEIVER ADDRESS
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));

        //        // "Provider Address"
        //        if (oPatient.Provider.ProviderAddress.AddressLine1 != null && oPatient.Provider.ProviderAddress.AddressLine1 != "")
        //        {
        //            oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.AddressLine1);// "Provider Address");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(1, 0, "");// "Provider Address");
        //        }


        //        //INFORMATION RECEIVER CITY/STATE/ZIP CODE
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));

        //        // "Provider City"
        //        if (oPatient.Provider.ProviderAddress.City != null && oPatient.Provider.ProviderAddress.City != "")
        //        {
        //            oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);// "Provider City");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(1, 0, "");// "Provider City");
        //        }

        //        //"Provider State"
        //        if (oPatient.Provider.ProviderAddress.State != null && oPatient.Provider.ProviderAddress.State != "")
        //        {
        //            oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//"Provider State");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(2, 0, "");//"Provider State");
        //        }

        //        // "Provider Zip"
        //        if (oPatient.Provider.ProviderAddress.Zip != null && oPatient.Provider.ProviderAddress.Zip != "")
        //        {
        //            oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);// "Provider Zip");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(3, 0, "");// "Provider Zip");
        //        }


        //        #endregion " Receiver Loop "

        //        #region " Subscriber Loop "

        //        //SUBSCRIBER LEVEL
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "3");
        //        oSegment.set_DataElementValue(2, 0, "2");
        //        oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
        //        oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

        //        //SUBSCRIBER TRACE NUMBER
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
        //        oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
        //        oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


        //        //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
        //        oSegment.set_DataElementValue(2, 0, "1"); //1=Person
        //        if (oPatient.Subscriber.Count > 0)
        //        {
        //            oSegment.set_DataElementValue(3, 0, oPatient.Subscriber[0].SubscriberLastName);// "Subscriber Last Name");
        //            oSegment.set_DataElementValue(4, 0, oPatient.Subscriber[0].SubscriberFirstName);//"Subscriber First Name");
        //            oSegment.set_DataElementValue(5, 0, "");//"Subscriber Middle Name");
        //        }
        //            else
        //        {
        //            oSegment.set_DataElementValue(3, 0, "");// "Subscriber Last Name");
        //            oSegment.set_DataElementValue(4, 0, "");//"Subscriber First Name");
        //            oSegment.set_DataElementValue(5, 0, "");//"Subscriber Middle Name");
        //        }

        //        oSegment.set_DataElementValue(8, 0, "MI");
        //        oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");


        //        #endregion " Subscriber Loop "

        //        #region " Subscriber Additional Identification Loop "

        //        //SUBSCRIBER ADDITIONAL IDENTIFICATION

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

        //        oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
        //        oSegment.set_DataElementValue(2, 0, "345342432");  //SSN

        //        //SUBSCRIBER ADDRESS
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

        //        if (oPatient.Subscriber.Count > 0)
        //        {
        //            oSegment.set_DataElementValue(1, 0, oPatient.Subscriber[0].SubscriberAddress.AddressLine1);// "Address1");//"Subscriber Address");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(1, 0, "");// "Address1");//"Subscriber Address");
        //        }


        //        //SUBSCRIBER CITY,STATE and ZIP
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

        //        if (oPatient.Subscriber.Count > 0)
        //        {
        //             oSegment.set_DataElementValue(1, 0, oPatient.Subscriber[0].SubscriberAddress.City);//"City");//"City");
        //             oSegment.set_DataElementValue(2, 0, oPatient.Subscriber[0].SubscriberAddress.State);//"State");//"State");
        //             oSegment.set_DataElementValue(3, 0, oPatient.Subscriber[0].SubscriberAddress.Zip);//"Zip");//"ZIP");
        //        }
        //        else
        //        {
        //            oSegment.set_DataElementValue(1, 0, "");//"City");//"City");
        //            oSegment.set_DataElementValue(2, 0, "");//"State");//"State");
        //            oSegment.set_DataElementValue(3, 0, "");//"Zip");//"ZIP");
        //        }



        //        //SUBSCRIBER DEMOGRAPHIC INFORMATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
        //        oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD

        //        oSegment.set_DataElementValue(2, 0, Convert.ToString(oPatient.DOB.Date));// "Patient DOB"); //Date of Birth
        //        oSegment.set_DataElementValue(3, 0, oPatient.Gender);//"Gender"); //Gender

        //        //SUBSCRIBER DATE
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
        //        oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
        //        oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
        //        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

        //        //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
        //        oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
        //        oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

        //        #endregion " Subscriber Loop "

        //        #region  " Save EDI File "

        //        //Save to a file
        //         oEdiDoc.Save(sPath + sEdiFile);
        //        string EdiFile = "";

        //        EdiFile = sPath + sEdiFile;
        //        txtEDIOutput.Text  = oEdiDoc.GetEdiString(); ;
        //        #endregion  " Save EDI File "

        //    }
        //    catch (Exception ex)
        //    {
        //        gloRxHub.ClsgloRxHubGeneral.UpdateLog(ex.ToString());
        //    }
        //    finally
        //    {

        //    }
        //}
        #region "Working Code"
        //private void Generate270EDI()
        //{
        //    try
        //    {
        //        string sEntity = "";
        //        string sInstance = "";

        //        oEdiDoc.New();
        //        oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
        //        oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

        //        oEdiDoc.SegmentTerminator = "~\r\n";
        //        oEdiDoc.ElementTerminator = "*";
        //        oEdiDoc.CompositeTerminator = ":";

        //        #region " Interchange Segment "
        //        //Create the interchange segment
        //        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

        //        oSegment.set_DataElementValue(1, 0, "00");
        //        oSegment.set_DataElementValue(3, 0, "01");
        //        //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
        //        //From RxHub, this is the password for RxHub to get to the POC/PPMS 
        //        oSegment.set_DataElementValue(4, 0, "Password");
        //        oSegment.set_DataElementValue(5, 0, "ZZ");
        //        //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
        //        //From RxHub, this is the RxHub's participant ID
        //        oSegment.set_DataElementValue(6, 0, "Sender ID");//
        //        oSegment.set_DataElementValue(7, 0, "ZZ");
        //        //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
        //        //From RxHub, this is the PBM's participant ID
        //        oSegment.set_DataElementValue(8, 0, "RXHUB");
        //        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
        //        oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
        //        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
        //        oSegment.set_DataElementValue(11, 0, "U");
        //        oSegment.set_DataElementValue(12, 0, "00401");
        //        oSegment.set_DataElementValue(13, 0, "000000020");
        //        oSegment.set_DataElementValue(14, 0, "1");
        //        oSegment.set_DataElementValue(15, 0, "T");
        //        oSegment.set_DataElementValue(16, 0, ":");

        //        #endregion " Interchange Segment "

        //        #region " Functional Group "

        //        //Create the functional group segment
        //        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(1, 0, "HS");
        //        oSegment.set_DataElementValue(2, 0, "Sender ID");
        //        oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
        //        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
        //        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
        //        oSegment.set_DataElementValue(6, 0, "1");
        //        oSegment.set_DataElementValue(7, 0, "X");
        //        oSegment.set_DataElementValue(8, 0, "004010X092A1");

        //        #endregion " Functional Group "

        //        #region "Transaction Set "
        //        //HEADER
        //        //ST TRANSACTION SET HEADER
        //        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(2, 0, "00021");

        //        #endregion "Transaction Set "

        //        #region " BHT "

        //        //Begining Segment 
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
        //        oSegment.set_DataElementValue(1, 0, "0022");
        //        oSegment.set_DataElementValue(2, 0, "13");//13=Request
        //        oSegment.set_DataElementValue(3, 0, "10001234");//Submitter Transaction Identifier
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
        //        string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
        //        oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

        //        #endregion " BHT "

        //        #region " Information Source "

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //        oSegment.set_DataElementValue(1, 0, "1");
        //        oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
        //        oSegment.set_DataElementValue(4, 0, "1");

        //        //INFORMATION SOURCE NAME 
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

        //        oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
        //        oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
        //        oSegment.set_DataElementValue(3, 0, "RxHub LLC");//"INFORMATION SOURCE NAME" );//"PBM"
        //        oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
        //        oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID

        //        #endregion " Information Source "

        //        #region " Receiver Loop "

        //        //INFORMATION RECEIVER LEVEL
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "2");
        //        oSegment.set_DataElementValue(2, 0, "1");
        //        oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
        //        oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

        //        //INFORMATION RECEIVER NAME (It is the medical service Provider)
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
        //        oSegment.set_DataElementValue(2, 0, "1");//1=Person
        //        oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
        //        oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
        //        oSegment.set_DataElementValue(5, 0, "Provider M Name");
        //        oSegment.set_DataElementValue(8, 0, "SV");//SV=Service Provider Number
        //        oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No

        //        //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
        //        oSegment.set_DataElementValue(1, 0, "EO");
        //        oSegment.set_DataElementValue(2, 0, "Provider SSN");

        //        //INFORMATION RECEIVER ADDRESS
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
        //        oSegment.set_DataElementValue(1, 0, "Provider Address");

        //        //INFORMATION RECEIVER CITY/STATE/ZIP CODE
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
        //        oSegment.set_DataElementValue(1, 0, "Provider City");
        //        oSegment.set_DataElementValue(2, 0, "Provider State");
        //        oSegment.set_DataElementValue(3, 0, "Provider Zip");

        //        #endregion " Receiver Loop "

        //        #region " Subscriber Loop "

        //        //SUBSCRIBER LEVEL
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "3");
        //        oSegment.set_DataElementValue(2, 0, "2");
        //        oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
        //        oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

        //        //SUBSCRIBER TRACE NUMBER
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
        //        oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
        //        oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


        //        //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
        //        oSegment.set_DataElementValue(2, 0, "1"); //1=Person
        //        oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
        //        oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
        //        oSegment.set_DataElementValue(5, 0, "Subscriber Middle Name");
        //        oSegment.set_DataElementValue(8, 0, "MI");
        //        oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");

        //        #endregion " Subscriber Loop "

        //        #region " Subscriber Additional Identification Loop "

        //        //SUBSCRIBER ADDITIONAL IDENTIFICATION

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

        //        oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
        //        oSegment.set_DataElementValue(2, 0, "345342432");  //SSN

        //        //SUBSCRIBER ADDRESS
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
        //        oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");

        //        //SUBSCRIBER CITY,STATE and ZIP
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
        //        oSegment.set_DataElementValue(1, 0, "City");//"City");
        //        oSegment.set_DataElementValue(2, 0, "State");//"State");
        //        oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");

        //        //SUBSCRIBER DEMOGRAPHIC INFORMATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
        //        oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
        //        oSegment.set_DataElementValue(2, 0, "Patient DOB"); //Date of Birth
        //        oSegment.set_DataElementValue(3, 0, "Gender"); //Gender

        //        //SUBSCRIBER DATE
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
        //        oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
        //        oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
        //        oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

        //        //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
        //        oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
        //        oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

        //        #endregion " Subscriber Loop "

        //        #region  " Save EDI File "

        //        //Save to a file
        //        oEdiDoc.Save(sPath + sEdiFile);
        //        string EdiFile = "";

        //        EdiFile = sPath + sEdiFile;
        //        txtEDIOutput.Text = oEdiDoc.GetEdiString(); ;
        //        #endregion  " Save EDI File "

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        # endregion
        #region "New function"
        public  void Generate270EDI()
        {
            try
            {
                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(4, 0, "Password");
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "Sender ID");//
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "000000020");
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "Sender ID");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "00021");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                oSegment.set_DataElementValue(3, 0, "10001234");//Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RxHub LLC");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID

                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//Provider FirstName

                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(5, 0, "");

                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, oPatient.Provider.ProviderDEA);//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                //oSegment.set_DataElementValue(1, 0, "EO");
                //oSegment.set_DataElementValue(2, 0, "Provider SSN");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres);

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                else
                { 

                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "MI");

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID);


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                oSegment.set_DataElementValue(2, 0, "345342432");  //SSN

                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"ZIP");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(2, 0, ""); //Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                oSegment.set_DataElementValue(3, 0, oPatient.Gender); //Gender

                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString(); ;
                #endregion  " Save EDI File "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        public string Generate270SingleCoveragePharmacyBenefitEDI(string RxhubParticipantId,string RxHubPassword)
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;
            string _270FilePath = "";
            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");

                //************************************************************
                //depending on the Staging or Production pass the password
                oSegment.set_DataElementValue(4, 0, RxHubPassword);//"FXTXGJVZ0W"
                //************************************************************

                oSegment.set_DataElementValue(5, 0, "ZZ");

                //************************************************************ pass either Staging or production participant ID
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, RxhubParticipantId);//"T00000000020315"
                //************************************************************

                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");

                //************************************************************
                //please change this value according to Stating or Production. if it is pointing to staging then Pass "T" or else if it is pointing to Production then pass "P"
                if (RxhubParticipantId.StartsWith("T"))
                {
                    oSegment.set_DataElementValue(15, 0, "T");
                }
                else
                {
                    oSegment.set_DataElementValue(15, 0, "P");
                }
                //************************************************************

                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");

                //************************************************************ pass either Staging or production participant ID
                oSegment.set_DataElementValue(2, 0, RxhubParticipantId);//"T00000000020315"
                //************************************************************ 

                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");

                if (ClsgloRxHubGeneral.gblnSend270UsingDEA == true)////true means the NPI info is not accepted and if file returns 997 then try sending the 270 file againg using the DEA number
                {
                    
                    oSegment.set_DataElementValue(8, 0, "SV");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                    //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                    oSegment.set_DataElementValue(9, 0, oPatient.Provider.ProviderDEA);//  //"1679678759");//"6666666"//Service Provider No
                }
                else//////if false then send the 270 file with NPI info else Resend it using the DEA number
                {
                    oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                    //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                    oSegment.set_DataElementValue(9, 0, oPatient.Provider.ProviderNPI );// "1720049901" oPatient.Provider.ProviderDEA//"1679678759");//"6666666"//Service Provider No
                }

                

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");

                //************************************************************ add the staging or participant id
                oSegment.set_DataElementValue(2, 0, RxhubParticipantId);//"T00000000020315"
                //************************************************************ 

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                if (oPatient.Provider.ProviderAddress.Zip != "" && oPatient.Provider.ProviderAddress.Zip.Length > 5) 
                {
                    oPatient.Provider.ProviderAddress.Zip=oPatient.Provider.ProviderAddress.Zip.Substring(0, 5);
                }

                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    for (int i = 0; i < oPatient.Subscriber.Count; i++)
                    {
                        if (oPatient.Subscriber[i].SubscriberFirstName != "")
                        {
                            oSubscriber = oPatient.Subscriber[i];
                            break;
                        }
                        
                    }
                        //oSubscriber = oPatient.Subscriber[i]; 
                }

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                //oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"
                oSegment.set_DataElementValue(3, 0, oPatient.LastName);//"PALTROW"

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                //oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"
                oSegment.set_DataElementValue(4, 0, oPatient.FirstName);//"BRUCE"

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                //oSegment.set_DataElementValue(5, 0, oSubscriber.SubscriberMiddleName);
                oSegment.set_DataElementValue(5, 0, oPatient.MiddleName);


                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //this was discussed with yaw on 12 march 09 Thursday and he asked to add the patient ssn no. previously we were passing subscriberId.
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                //string SubscriberAddres = "";
                //SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;
                //oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");

                string PatAddres = "";
                PatAddres = oPatient.PatientAddress.AddressLine1 + " " + oPatient.PatientAddress.AddressLine2;

                oSegment.set_DataElementValue(1, 0, PatAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                ////oSegment.set_DataElementValue(1, 0, "City");//"City");
                //oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");
                oSegment.set_DataElementValue(1, 0, oPatient.PatientAddress.City);

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                ////oSegment.set_DataElementValue(2, 0, "State");//"State");
                //oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");
                oSegment.set_DataElementValue(2, 0, oPatient.PatientAddress.State);

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change
                ////                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                //if (oSubscriber.SubscriberAddress.Zip != "" && oSubscriber.SubscriberAddress.Zip.Length > 5)
                //{
                //    oSubscriber.SubscriberAddress.Zip = oSubscriber.SubscriberAddress.Zip.Substring(0, 5);
                //}
                //oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");

                if (oPatient.PatientAddress.Zip != "" && oPatient.PatientAddress.Zip.Length > 5)
                {
                    oPatient.PatientAddress.Zip = oPatient.PatientAddress.Zip.Substring(0, 5);
                }
                oSegment.set_DataElementValue(3, 0, oPatient.PatientAddress.Zip);//"54360"//"ZIP");


                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD

                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change, patient DOB code again uncommented
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString(); //commented for GLO2011-0011225
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth commented for GLO2011-0011225
                //string _SubscriberDOB = gloDateMaster.gloDate.DateAsNumber(oSubscriber.SubscriberDOB.ToShortDateString()).ToString();//GLO2011-0011225--pass the subscriber Fname, Mname, LName,DOB, Gender, ZIPcode information
                //oSegment.set_DataElementValue(2, 0, _SubscriberDOB);



                //send patient info instead of subscriber info, Email from D.N. dated 11 Aug 2011 sub: Surescripts Eligibility Request Change, patient Gender code again uncommented
                //Commented for GLO2011-0011225--pass the subscriber Fname, Mname, LName,DOB, Gender, ZIPcode information
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }

                ////GLO2011-0011225--pass the subscriber Fname, Mname, LName,DOB, Gender, ZIPcode information
                //if (oSubscriber.SubscriberGender != "")
                //{
                //    if (oSubscriber.SubscriberGender == "Female")
                //    {
                //        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                //    }
                //    else if (oSubscriber.SubscriberGender == "Male")
                //    {
                //        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                //    }
                //    else if (oSubscriber.SubscriberGender == "Other")
                //    {
                //        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                //    }
                //}
                //else
                //{
                //    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                //}


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";

                string EdiFile = "";
                EdiFile = sPath + sEdiFile;
                oEdiDoc.Save(EdiFile);//sPath + sEdiFile
               


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                _270FilePath = strFileName;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "

                
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                //gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, _strEx, gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
            finally
            {
                
            }
            return _270FilePath;
            
        }

        private void Generate270MultipleCoveragePharmacyBenefitEDI()
        {

            #region "Old Generate270MultipleCoveragePharmacyBenefitEDI() function"
            //try
            //{
            //    string sEntity = "";
            //    string sInstance = "";

            //    oEdiDoc.New();
            //    oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
            //    oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

            //    oEdiDoc.SegmentTerminator = "~\r\n";
            //    oEdiDoc.ElementTerminator = "*";
            //    oEdiDoc.CompositeTerminator = ":";

            //    #region " Interchange Segment "
            //    //Create the interchange segment
            //    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

            //    oSegment.set_DataElementValue(1, 0, "00");
            //    oSegment.set_DataElementValue(3, 0, "01");
            //    //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
            //    //From RxHub, this is the password for RxHub to get to the POC/PPMS 
            //    oSegment.set_DataElementValue(4, 0, "Password");
            //    oSegment.set_DataElementValue(5, 0, "ZZ");
            //    //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
            //    //From RxHub, this is the RxHub's participant ID
            //    oSegment.set_DataElementValue(6, 0, "T00000000000109");//
            //    oSegment.set_DataElementValue(7, 0, "ZZ");
            //    //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
            //    //From RxHub, this is the PBM's participant ID
            //    oSegment.set_DataElementValue(8, 0, "RXHUB");
            //    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
            //    oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
            //    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
            //    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
            //    oSegment.set_DataElementValue(11, 0, "U");
            //    oSegment.set_DataElementValue(12, 0, "00401");
            //    oSegment.set_DataElementValue(13, 0, "736698812");
            //    oSegment.set_DataElementValue(14, 0, "1");
            //    oSegment.set_DataElementValue(15, 0, "T");
            //    oSegment.set_DataElementValue(16, 0, ":");

            //    #endregion " Interchange Segment "

            //    #region " Functional Group "

            //    //Create the functional group segment
            //    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
            //    oSegment.set_DataElementValue(1, 0, "HS");
            //    oSegment.set_DataElementValue(2, 0, "T00000000000109");
            //    oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
            //    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
            //    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
            //    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
            //    oSegment.set_DataElementValue(6, 0, "000000001");
            //    oSegment.set_DataElementValue(7, 0, "X");
            //    oSegment.set_DataElementValue(8, 0, "004010X092A1");

            //    #endregion " Functional Group "

            //    #region "Transaction Set "
            //    //HEADER
            //    //ST TRANSACTION SET HEADER
            //    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
            //    oSegment.set_DataElementValue(2, 0, "000000001");

            //    #endregion "Transaction Set "

            //    #region " BHT "

            //    //Begining Segment 
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
            //    oSegment.set_DataElementValue(1, 0, "0022");
            //    oSegment.set_DataElementValue(2, 0, "13");//13=Request
            //    oSegment.set_DataElementValue(3, 0, "000000001");//Submitter Transaction Identifier
            //    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
            //    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
            //    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

            //    #endregion " BHT "

            //    #region " Information Source "

            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
            //    oSegment.set_DataElementValue(1, 0, "1");
            //    oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
            //    oSegment.set_DataElementValue(4, 0, "1");

            //    //INFORMATION SOURCE NAME 
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

            //    oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
            //    oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
            //    oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
            //    oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
            //    oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


            //    #endregion " Information Source "

            //    #region " Receiver Loop "

            //    //INFORMATION RECEIVER LEVEL
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
            //    oSegment.set_DataElementValue(1, 0, "2");
            //    oSegment.set_DataElementValue(2, 0, "1");
            //    oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
            //    oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

            //    //INFORMATION RECEIVER NAME (It is the medical service Provider)
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
            //    oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
            //    oSegment.set_DataElementValue(2, 0, "1");//1=Person

            //    //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
            //    oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

            //    //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
            //    oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

            //    //oSegment.set_DataElementValue(5, 0, "Provider M Name");
            //    oSegment.set_DataElementValue(5, 0, "");
            //    oSegment.set_DataElementValue(6, 0, "");
            //    oSegment.set_DataElementValue(7, 0, "MD");


            //    oSegment.set_DataElementValue(8, 0, "SV");//SV=Service Provider Number

            //    //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
            //    oSegment.set_DataElementValue(9, 0, oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

            //    //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
            //    oSegment.set_DataElementValue(1, 0, "EO");
            //    oSegment.set_DataElementValue(2, 0, "T00000000000109");

            //    //INFORMATION RECEIVER ADDRESS
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
            //    string ProviderAddres = "";
            //    ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
            //    // oSegment.set_DataElementValue(1, 0, "Provider Address");
            //    oSegment.set_DataElementValue(1, 0, ProviderAddres);//""

            //    //INFORMATION RECEIVER CITY/STATE/ZIP CODE
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
            //    //oSegment.set_DataElementValue(1, 0, "Provider City");
            //    oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

            //    //oSegment.set_DataElementValue(2, 0, "Provider State");
            //    oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

            //    //oSegment.set_DataElementValue(3, 0, "Provider Zip");
            //    oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//""

            //    #endregion " Receiver Loop "

            //    #region " Subscriber Loop "

            //    //SUBSCRIBER LEVEL
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
            //    oSegment.set_DataElementValue(1, 0, "3");
            //    oSegment.set_DataElementValue(2, 0, "2");
            //    oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
            //    oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

            //    //SUBSCRIBER TRACE NUMBER
            //    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
            //    //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
            //    //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
            //    //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


            //    //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
            //    oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
            //    oSegment.set_DataElementValue(2, 0, "1"); //1=Person
            //    gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
            //    if (oPatient.Subscriber.Count > 0)
            //    {
            //        oSubscriber = oPatient.Subscriber[0];
            //    }
            //    //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
            //    oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

            //    //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
            //    oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

            //    oSegment.set_DataElementValue(5, 0, "K");
            //    oSegment.set_DataElementValue(8, 0, "");

            //    //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
            //    oSegment.set_DataElementValue(9, 0, "");


            //    #endregion " Subscriber Loop "

            //    #region " Subscriber Additional Identification Loop "

            //    //SUBSCRIBER ADDITIONAL IDENTIFICATION

            //    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

            //    //oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
            //    //oSegment.set_DataElementValue(2, 0, "345342432");  //SSN

            //    //SUBSCRIBER ADDRESS
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

            //    //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
            //    string SubscriberAddres = "";
            //    SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

            //    //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
            //    oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


            //    //SUBSCRIBER CITY,STATE and ZIP
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

            //    //oSegment.set_DataElementValue(1, 0, "City");//"City");
            //    oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

            //    //oSegment.set_DataElementValue(2, 0, "State");//"State");
            //    oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

            //    //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
            //    oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
            //    oSegment.set_DataElementValue(4, 0, "US");//"County");

            //    //SUBSCRIBER DEMOGRAPHIC INFORMATION
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
            //    oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
            //    string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
            //    oSegment.set_DataElementValue(2, 0, _patDOB); //"19450201"//Date of Birth
            //    //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

            //    //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
            //    if (oPatient.Gender != "")
            //    {
            //        if (oPatient.Gender == "Female")
            //        {
            //            oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
            //        }
            //        else
            //        {
            //            oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
            //        }
            //    }
            //    else
            //    {
            //        oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
            //    }

            //    //SUBSCRIBER DATE
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
            //    oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
            //    oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
            //    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

            //    //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
            //    oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
            //    oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

            //    #endregion " Subscriber Loop "

            //    #region  " Save EDI File "

            //    //Save to a file
            //    oEdiDoc.Save(sPath + sEdiFile);
            //    string EdiFile = "";

            //    EdiFile = sPath + sEdiFile;
            //    txtEDIOutput.Text = oEdiDoc.GetEdiString(); ;
            //    #endregion  " Save EDI File "

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{

            //}
            #endregion "Old Generate270MultipleCoveragePharmacyBenefitEDI() function"
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }


        private void Generate270PatDeterminedatPayer()
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        private void Generate270_997atRxHub()
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                //***************GENERATE 997 Error***********************************
                //oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No
                //***************GENERATE 997 Error***********************************
                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        private void Generate270_TA1MessageatRxHub()
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                //**************************************TO GENERATE THE TA1 MESSAGE
                oSegment.set_DataElementValue(5, 0, "Z");
                //**************************************TO GENERATE THE TA1 MESSAGE
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        private void Generate270_PatientNotFoundatRxHub()
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        private void Generate270_NAKErroratRxHub()
        {
            gloRxHub.clsgloRxHubDBLayer oclsgloRxHubDBLayer;

            string BHT_TransactionRef = "";
            try
            {
                //oPatient = objPatient;

                //string sEntity = "";
                //string sInstance = "";

                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~";//"~\r\n"
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";//">"

                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                //From POC/PPMS this is the Password assigned by RxHub for POC/PPMS
                //From RxHub, this is the password for RxHub to get to the POC/PPMS 
                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "01");
                oSegment.set_DataElementValue(4, 0, "FXTXGJVZ0W");//"Password"
                oSegment.set_DataElementValue(5, 0, "ZZ");
                //From POC/PPMS this is the POC/PPMS participant ID as assigned by RxHub
                //From RxHub, this is the RxHub's participant ID
                oSegment.set_DataElementValue(6, 0, "T00000000020315");//T00000000000109
                oSegment.set_DataElementValue(7, 0, "ZZ");
                //From POC/PPMS this is the RxHub's participant ID as assigned by RxHub
                //From RxHub, this is the PBM's participant ID
                oSegment.set_DataElementValue(8, 0, "RXHUB");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                //Create a unique control number as per the doc. it is 9 digit
                //DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                //for generating the unique control no we hv take Milliseconds/Seconds/Minutes/and(hour + 100) so tht we can generate 9 digit unique value.
                string strControlNo = DateTime.Now.Millisecond.ToString("#000") + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(13, 0, strControlNo);
                oSegment.set_DataElementValue(14, 0, "1");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");
                oSegment.set_DataElementValue(3, 0, "RXHUB");//Receiver ID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                oSegment.set_DataElementValue(6, 0, "1");//"000000001"-------claredi------GS*HS*T00000000000109*RXHUB*20090418*1135*000000001*X*004010X092A1~       //H10014:Leading zeros detected in GS06. The X12 syntax requires the suppression of leading zeros for numeric elements
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X092A1");

                #endregion " Functional Group "

                #region "Transaction Set "
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000000001");

                #endregion "Transaction Set "

                #region " BHT "

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022");
                oSegment.set_DataElementValue(2, 0, "13");//13=Request
                BHT_TransactionRef = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(); //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(3, 0, BHT_TransactionRef);//"000000001"Submitter Transaction Identifier
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());

                #endregion " BHT "

                #region " Information Source "

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                oSegment.set_DataElementValue(1, 0, "1");
                oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                oSegment.set_DataElementValue(4, 0, "1");

                //INFORMATION SOURCE NAME 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                oSegment.set_DataElementValue(1, 0, "2B");//PR=Payer
                oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                oSegment.set_DataElementValue(3, 0, "RXHUB");//"INFORMATION SOURCE NAME" );//"PBM"
                oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                oSegment.set_DataElementValue(9, 0, "RXHUB");//PayerID


                #endregion " Information Source "

                #region " Receiver Loop "

                //INFORMATION RECEIVER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "2");
                oSegment.set_DataElementValue(2, 0, "1");
                oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                //INFORMATION RECEIVER NAME (It is the medical service Provider)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                oSegment.set_DataElementValue(2, 0, "1");//1=Person

                //oSegment.set_DataElementValue(3, 0, "Provider L Name");//Provider  LastName
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderLastName);//"JONES"//Provider  LastName

                //oSegment.set_DataElementValue(4, 0, "Provider F Name");//Provider FirstName
                oSegment.set_DataElementValue(4, 0, oPatient.Provider.ProviderFirstName);//"MARK"//Provider FirstName

                oSegment.set_DataElementValue(5, 0, oPatient.Provider.ProviderMiddleName);//"MARK"//Provider FirstName
                //oSegment.set_DataElementValue(5, 0, "Provider M Name");
                oSegment.set_DataElementValue(6, 0, "");
                oSegment.set_DataElementValue(7, 0, "");
                // oSegment.set_DataElementValue(7, 0, "MD");


                oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number-------claredi------NM1*1P*1*Shette*Dharmaji***MD*SV*5463~        H21084:Invalid qualifier 'SV' found in '2100B NM108'. Only 'XX' is valid because the National Provider Identifier (NPI) is now mandated for use.

                //oSegment.set_DataElementValue(9, 0, "DEA Number");//Service Provider No
                oSegment.set_DataElementValue(9, 0, "1679678759");////oPatient.Provider.ProviderDEA);//"6666666"//Service Provider No

                //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                oSegment.set_DataElementValue(1, 0, "EO");
                oSegment.set_DataElementValue(2, 0, "T00000000020315");

                //INFORMATION RECEIVER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                string ProviderAddres = "";
                ProviderAddres = oPatient.Provider.ProviderAddress.AddressLine1 + " " + oPatient.Provider.ProviderAddress.AddressLine2;
                // oSegment.set_DataElementValue(1, 0, "Provider Address");
                oSegment.set_DataElementValue(1, 0, ProviderAddres.Trim());//""

                //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                //oSegment.set_DataElementValue(1, 0, "Provider City");
                oSegment.set_DataElementValue(1, 0, oPatient.Provider.ProviderAddress.City);//""

                //oSegment.set_DataElementValue(2, 0, "Provider State");
                oSegment.set_DataElementValue(2, 0, oPatient.Provider.ProviderAddress.State);//""

                //oSegment.set_DataElementValue(3, 0, "Provider Zip");
                oSegment.set_DataElementValue(3, 0, oPatient.Provider.ProviderAddress.Zip);//-------claredi------N4*New york*Mi*43324~      H50002:Invalid State/Province Code ('Mi')       B51124:This Zip Code is not valid for this State.

                #endregion " Receiver Loop "

                #region " Subscriber Loop "

                //SUBSCRIBER LEVEL
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "3");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure

                //SUBSCRIBER TRACE NUMBER
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                gloRxHub.ClsSubscriber oSubscriber = new gloRxHub.ClsSubscriber();
                if (oPatient.Subscriber.Count > 0)
                {
                    oSubscriber = oPatient.Subscriber[0];
                }
                //                oSegment.set_DataElementValue(3, 0, "Subscriber Last Name");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberLastName);//"PALTROW"

                //oSegment.set_DataElementValue(4, 0, "Subscriber First Name");
                oSegment.set_DataElementValue(4, 0, oSubscriber.SubscriberFirstName);//"BRUCE"

                oSegment.set_DataElementValue(5, 0, "");
                oSegment.set_DataElementValue(8, 0, "ZZ");//MI

                //oSegment.set_DataElementValue(9, 0, "SubscriberPrimaryID");
                if (oPatient.SSN.Trim() != "")
                {
                    //oSegment.set_DataElementValue(9, 0, oSubscriber.SubscriberID.Trim());
                    oSegment.set_DataElementValue(9, 0, oPatient.SSN);
                }


                #endregion " Subscriber Loop "

                #region " Subscriber Additional Identification Loop "

                //SUBSCRIBER ADDITIONAL IDENTIFICATION

                if (oPatient.SSN.Trim() != "")
                {
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));

                    oSegment.set_DataElementValue(1, 0, "SY");//SY=SS Number
                    oSegment.set_DataElementValue(2, 0, oPatient.SSN.Trim());  //SSN

                }
                //SUBSCRIBER ADDRESS
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                string SubscriberAddres = "";
                SubscriberAddres = oSubscriber.SubscriberAddress.AddressLine1 + " " + oSubscriber.SubscriberAddress.AddressLine2;

                //oSegment.set_DataElementValue(1, 0, "Address1");//"Subscriber Address");
                oSegment.set_DataElementValue(1, 0, SubscriberAddres);//"2645 MULBERRY LANE"//"Subscriber Address");


                //SUBSCRIBER CITY,STATE and ZIP
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));

                //oSegment.set_DataElementValue(1, 0, "City");//"City");
                oSegment.set_DataElementValue(1, 0, oSubscriber.SubscriberAddress.City);//"TOLEDO"//"City");

                //oSegment.set_DataElementValue(2, 0, "State");//"State");
                oSegment.set_DataElementValue(2, 0, oSubscriber.SubscriberAddress.State);//"OH"//"State");

                //                oSegment.set_DataElementValue(3, 0, "Zip");//"ZIP");
                oSegment.set_DataElementValue(3, 0, oSubscriber.SubscriberAddress.Zip);//"54360"//"ZIP");
                oSegment.set_DataElementValue(4, 0, "US");//"County");

                //SUBSCRIBER DEMOGRAPHIC INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                string _patDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.DOB.ToShortDateString()).ToString();
                oSegment.set_DataElementValue(2, 0, _patDOB);// oPatient.DOB.ToString()); //"19450201"//Date of Birth
                //oSegment.set_DataElementValue(2, 0, oPatient.DOB.ToString()); //Date of Birth

                //oSegment.set_DataElementValue(3, 0, "Gender"); //Gender
                if (oPatient.Gender != "")
                {
                    if (oPatient.Gender == "Female")
                    {
                        oSegment.set_DataElementValue(3, 0, "F");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Male")
                    {
                        oSegment.set_DataElementValue(3, 0, "M");//"M" //Gender
                    }
                    else if (oPatient.Gender == "Other")
                    {
                        oSegment.set_DataElementValue(3, 0, "O");//"Other"
                    }
                }
                else
                {
                    oSegment.set_DataElementValue(3, 0, "");//"M" //Gender
                }


                //SUBSCRIBER DATE
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                oSegment.set_DataElementValue(1, 0, "472");//472=Service,102=Issue,307=Eligibility,435=Admission
                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"20020801"//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour

                //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "88"); // Pharmacy (recommended by RxHub)

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                oSegment.set_DataElementValue(1, 0, "90");//Mail Order Prescription Drug

                #endregion " Subscriber Loop "

                #region  " Save EDI File "

                //Save to a file
                sPath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "\\Outbox\\";
                oEdiDoc.Save(sPath + sEdiFile);
                string EdiFile = "";

                EdiFile = sPath + sEdiFile;


                //strEDIFileName = BHT_TransactionRef;
                //EdiFile = sPath + strEDIFileName + ".X12";
                //File.Create(EdiFile);
                //oEdiDoc.Save(EdiFile);
                strFileName = EdiFile;
                txtEDIOutput.Text = oEdiDoc.GetEdiString();
                #endregion  " Save EDI File "

                #region  " Save 270 Info to database "

                // oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", BHT_TransactionRef, oPatient);


                oclsgloRxHubDBLayer = new gloRxHub.clsgloRxHubDBLayer();
                oclsgloRxHubDBLayer.Connect(gloRxHub.ClsgloRxHubGeneral.ConnectionString);
                //Message ID       //TransactionID 
                oclsgloRxHubDBLayer.InsertEDIResquest270("gsp_InUpRxH_270Request_Details", strControlNo, BHT_TransactionRef, oPatient);

                #endregion  " Save 270 Info to database "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }
        }

        #endregion




        public  void LoadEDIObject()
        {
            string sAssemblyName = "";
            try
            {
                /////since the same func is called from RxSniffer, therefore if this func is called from gloEMR then show error message boxes else dont show them.
                sAssemblyName = System.Reflection.Assembly.GetCallingAssembly().FullName;

                sPath = AppDomain.CurrentDomain.BaseDirectory;

                sSEFFile = "270_X092A1.SEF";
                //sEdiFile = "270OUTPUT.x12";

                //strEDIFileName = DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".X12"; //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                sEdiFile = "EDI270_" + DateTime.Now.Date.ToString("MMddyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".X12"; //Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())) + Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();

                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                //oEdiDoc = new ediDocument();
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //oEdiDoc.SegmentTerminator = "~\r\n";
                //oEdiDoc.ElementTerminator = "*";
                //oEdiDoc.CompositeTerminator = ":";

                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (ofile.Exists == false)
                {
                    if (sAssemblyName.Contains("gloEMR"))
                    {
                        MessageBox.Show("SEF file is not present in the base directory.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    return;
                }

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + "270_X092A1.SEF", 0));
                               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            _length = TimeFormat.Length;
            if (_length == 0)
            {
                TimeFormat = "0000";
            }
            if (_length == 1)
            {
                TimeFormat = "000" + TimeFormat;
            }
            else if (_length == 2)
            {
                TimeFormat = "00" + TimeFormat;
            }
            else if (_length == 3)
            {
                TimeFormat = "0" + TimeFormat;
            }
            else if (_length == 4)
            {
                TimeFormat = TimeFormat;
            }
            return TimeFormat;
        }

        #endregion " Procedures And Functions "

        #region " Form Load "

        private void frmRxhub270EDIGeneration_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEDIObject();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        #endregion " Form Load "

        #region " Button Events "

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }


        }

        private void tsb_EDI270_Click(object sender, EventArgs e)
        {
            try
            {
                Generate270EDI();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        //singleCoverageBenefit
        private void tsb_SingleCovrgPhBenefit_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
               // Generate270SingleCoveragePharmacyBenefitEDI();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }


        private void singleCoverageBenefitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    //Generate270SingleCoveragePharmacyBenefitEDI();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }


        }
        //singleCoverageBenefit


        //patientDeterminedAtPayer
        private void tsb_PatDeterminedatPayer_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270PatDeterminedatPayer();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void patientDeterminedAtPayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270PatDeterminedatPayer();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }

        }
        //patientDeterminedAtPayer

        //multipleCoverageBenefit
        private void tsb_MultipleCovrgPhBenefit_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270MultipleCoveragePharmacyBenefitEDI();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void multipleCoverageBenefitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {

                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270MultipleCoveragePharmacyBenefitEDI();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }

        }
        //multipleCoverageBenefit

        //EDI997atRxHub
        private void tsb_997atRxHub_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270_997atRxHub();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void EDI997atRxHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270_997atRxHub();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }

        }
        //EDI997atRxHub


        //TA1Msg
        private void tsb_TA1Msg_RxHub_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270_TA1MessageatRxHub();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void tA1MessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270_TA1MessageatRxHub();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }

        }
        //TA1Msg

        //PatientNotFound
        private void tsb_PatientNotFound_RxHub_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270_PatientNotFoundatRxHub();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void patientNotFoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270_PatientNotFoundatRxHub();
                }
                else
                {
                    MessageBox.Show("Outbox Directory in not present!.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }

        }
        //PatientNotFound


        //NAKErr
        private void tsb_NAKErr_RxHub_Click(object sender, EventArgs e)
        {
            try
            {
                //call the appropriate generate EDI function
                Generate270_NAKErroratRxHub();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
        }

        private void nAKErrorAtRxHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oClsRxHubInterface = new gloRxHub.ClsRxHubInterface();
            try
            {
                //before generating the 270 file, check whether the OUTBOX folder is present. if not create it and save the file in that OUTBOX folder.
                if (oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath))
                {
                    //call the appropriate generate EDI function
                    Generate270_NAKErroratRxHub();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                oClsRxHubInterface.Dispose();
            }
        }
        //NAKErr

        private void tsb_POSTEDI270_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

        }


        #endregion " Button Events "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Post EDI 270":
                        {
                            ToolStrip_Click(strFileName);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw;
            }
        }






    }
}