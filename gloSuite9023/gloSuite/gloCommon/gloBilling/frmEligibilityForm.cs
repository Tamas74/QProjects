using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmEligibilityForm : Form
    {
        #region " Constructors "

        public frmEligibilityForm(String DatabaseConnectionString, Int64 PatientID, Int64 InsuranceID)
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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion " Constructors "

        #region " Variables "

        
        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _InsuranceId = 0;
        private Int64 _ClinicId = 0;
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloListControl.gloListControl oListControl;

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
      //  ediWarnings oWarnings = null;
       // ediWarning oWarning = null;

        private string _SubscriberFName = "";
        private string _SubscriberLName = "";
        private string _SubscriberMName = "";
        private string _SubscriberDOB = "";
      //  private string _SubscriberCity = "";
      //  private string _SubscriberState = "";
      //  private string _SubscriberZip = "";
      //  private string _SubscriberGender = "";
        private string _PayerName = "";
        private string _SubscriberPrimaryID = "";
        private string _SubscriberAdditionalID = "";
     //   private string _SubscriberAdditionalIDQualifier = "1L";
        private string _PayerID = "";
     //   private string _SubscriberAddress = "";
        private string _SubscriberRelationShip = "";
        private string _SubscriberCardIssueDate = "";
    //    private string _ProviderID = "";
        private string _ProviderFName = "";
        private string _ProviderLName = "";
        private string _ProviderMName = "";
        private string _ProviderCity = "";
        private string _ProviderSSN = "";
        private string _ProviderNPI = "";
        private string _ProviderState = "";
        private string _ProviderZip = "";
        private string _ProviderAddress = "";

        private string _InsuranceType = "";
        private string _InsurancePlanCode = "";
   //     private string _ServiceTypeCode = "";

    //    private string _CoverageDescription = "";

        private string strEligibilityDetails = "";

        public string EligibilityDetails
        {
            get { return strEligibilityDetails; }
            set { strEligibilityDetails = value; }
        }
        #endregion " Variables "

        #region " Methods "

        private void FillInsurances()
        {
            
            DataTable dtPatientInsurances = null;

            try
            {
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                dtPatientInsurances = ogloPatient.getPatientInsurances(_PatientId);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (dtPatientInsurances != null)
                {
                    if (dtPatientInsurances.Rows.Count > 0)
                    {
                        for (int _Index = 0; _Index < dtPatientInsurances.Rows.Count; _Index++)
                        {
                            
                                if (Convert.ToString(dtPatientInsurances.Rows[_Index]["nInsuranceFlag"]) == "1" || Convert.ToString(dtPatientInsurances.Rows[_Index]["nInsuranceFlag"]) == "2" || Convert.ToString(dtPatientInsurances.Rows[_Index]["nInsuranceFlag"]) == "3")
                                {
                                    if (Convert.ToInt64(dtPatientInsurances.Rows[_Index]["nInsuranceID"].ToString()) == _InsuranceId)
                                    {
                                    _PayerName = dtPatientInsurances.Rows[_Index]["InsuranceName"].ToString();
                                    _InsurancePlanCode = GetInsurancePlanCode(_PayerName);
                                    _PayerID = Convert.ToString(dtPatientInsurances.Rows[_Index]["PayerID"]);
                                    _SubscriberAdditionalID = Convert.ToString(dtPatientInsurances.Rows[_Index]["sGroup"]);
                                    _InsuranceType = Convert.ToString(dtPatientInsurances.Rows[_Index]["InsuranceTypeCode"]);
                                    _SubscriberRelationShip = Convert.ToString(dtPatientInsurances.Rows[_Index]["RelationshipCode"]);
                                    if (Convert.ToString(dtPatientInsurances.Rows[_Index]["dtStartDate"]) != "")//&& Convert.ToString(dtPatientInsurances.Rows[_Index]["dtEndDate"]) != "")
                                    {
                                        _SubscriberCardIssueDate = (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(Convert.ToDateTime(dtPatientInsurances.Rows[_Index]["dtStartDate"])))));// + "-" + Convert.ToString((gloDateMaster.gloDate.DateAsNumber(Convert.ToString(Convert.ToDateTime(dtPatientInsurances.Rows[_Index]["dtEndDate"]))))));
                                    }
                                    else
                                    {
                                        _SubscriberCardIssueDate = "";
                                    }
                                    string[] SubsName = dtPatientInsurances.Rows[_Index]["sSubscriberName"].ToString().Split(' ');
                                    if (SubsName.Length > 0)
                                    {
                                        if (SubsName.Length > 1 && SubsName.Length < 2)
                                        {
                                            _SubscriberFName = SubsName[0].ToString();
                                            _SubscriberLName = SubsName[1].ToString();
                                        }
                                        if (SubsName.Length > 2)
                                        {
                                            _SubscriberFName = SubsName[0].ToString();
                                            _SubscriberMName = SubsName[1].ToString();
                                            _SubscriberLName = SubsName[2].ToString();
                                        }
                                    }
                                    if (Convert.ToString(dtPatientInsurances.Rows[_Index]["dtDOB"]) != "")
                                    {
                                        _SubscriberDOB = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtPatientInsurances.Rows[_Index]["dtDOB"].ToString()));
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Subscriber date of birth is required for eligibilty check.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    _SubscriberPrimaryID = Convert.ToString(dtPatientInsurances.Rows[_Index]["sSubscriberID"]);

                                }
                            }
                        }
                    }
                    dtPatientInsurances.Dispose();
                    dtPatientInsurances = null;
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


        }

        private void FillServiceTypes()
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = pnlBottom.Controls.Count - 1; i >= 0; i--)
                    {
                        if (pnlBottom.Controls[i].Name == oListControl.Name)
                        {
                            pnlBottom.Controls.Remove(pnlBottom.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.InsuranceServiceType, false, pnlBottom.Width);
                oListControl.ClinicID = _ClinicId;
                oListControl.ControlHeader = "Insurance Service Type";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                pnlBottom.Controls.Add(oListControl);

                oListControl.OpenControl();

                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillProviders(Int64 _ProviderID)
        {
            gloAppointmentBook.Books.Resource oProvider = new gloAppointmentBook.Books.Resource(_databaseConnectionString);
            gloAppointmentBook.Books.Provider Provider = new gloAppointmentBook.Books.Provider(_databaseConnectionString);
            DataTable dtProviders = new DataTable();

            try
            {
                Provider = oProvider.GetProviderDetail(_ProviderID);
                if (Provider != null )
                {
                    _ProviderFName = Provider.FirstName;
                    _ProviderLName = Provider.LastName;
                    _ProviderMName = Provider.MiddleName;
                    _ProviderCity = Provider.BMCity;
                    _ProviderState = Provider.BMState;
                    _ProviderZip = Provider.BMZIP;
                    _ProviderSSN = Provider.StateMedicalNo;
                    _ProviderNPI = Provider.NPI;
                    _ProviderAddress = Provider.BMAddress1;
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
     //           TimeFormat = TimeFormat;
            }
            return TimeFormat;
        }

        private string ControlNumberGeneration(string HeaderType)
        {
            string strNumber = DateTime.Now.ToString("hhmmss");
            int _length = 0;
            string NumberSize = "";
            _length = strNumber.Trim().Length;
            if (_length == 5)
            {
                NumberSize = "000" + strNumber;
            }
            else if (_length == 6)
            {
                NumberSize = "00" + strNumber;
            }
            else if (_length == 7)
            {
                NumberSize = "0" + strNumber;
            }
            else if (_length == 8)
            {
                NumberSize = strNumber;
            }
            NumberSize = HeaderType + NumberSize;
            return NumberSize;
        }

        private void Generate270EDI()
        {
            try
            {
               // string sEntity = "";
               // string sInstance = "";
                string InterchangeHeader = "";
                string FunctionalGroupHeader = "";
                string TransactionSetHeader = "";
                
             
                
                //oPatient = new gloPatient.Patient();
                // oEdiDoc = new ediDocument();
                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";
                string _TypeOfData = "T";
                gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
                DataTable dtClearingHouse =  ogloBilling.GetClearingHouseSettings();
                ogloBilling.Dispose();
                ogloBilling = null;
                if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                {
                    MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                gloPatient.Patient oPatient = ogloPatient.GetPatientDemo(_PatientId);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (oPatient != null)
                {
                    FillProviders(oPatient.DemographicsDetail.PatientProviderID);
                    if (ValidateData(oPatient))
                    {
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
                        oSegment.set_DataElementValue(5, 0, "12");
                        oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//"1C26");//txtSenderID.Text.Trim());// "Sender");
                        oSegment.set_DataElementValue(7, 0, "12");
                        oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", ""));//"V2EL");//txtReceiverID.Text.Trim());//"ReceiverID");
                        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                        oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
                        oSegment.set_DataElementValue(11, 0, "U");
                        oSegment.set_DataElementValue(12, 0, "00401");
                        InterchangeHeader = ControlNumberGeneration("1");
                        oSegment.set_DataElementValue(13, 0, InterchangeHeader);
                        //oSegment.set_DataElementValue(13, 0, "000000020");//txtControlNo.Text.Trim());
                        oSegment.set_DataElementValue(14, 0, "1");
                        oSegment.set_DataElementValue(15, 0, _TypeOfData);
                        oSegment.set_DataElementValue(16, 0, ":");

                        #endregion " Interchange Segment "

                        #region " Functional Group "

                        //Create the functional group segment
                        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                        oSegment.set_DataElementValue(1, 0, "HS");
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//"IC26");
                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", ""));//"V2EL");
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                        FunctionalGroupHeader = ControlNumberGeneration("2");
                        oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                        //oSegment.set_DataElementValue(6, 0, "1");
                        oSegment.set_DataElementValue(7, 0, "X");
                        oSegment.set_DataElementValue(8, 0, "004010X092A1");

                        #endregion " Functional Group "

                        #region "Transaction Set "
                        //HEADER
                        //ST TRANSACTION SET HEADER
                        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                        //oSegment.set_DataElementValue(2, 0, "00021");
                        TransactionSetHeader = ControlNumberGeneration("3");
                        oSegment.set_DataElementValue(2, 0, TransactionSetHeader);

                        #endregion "Transaction Set "

                        #region " BHT "

                        //Begining Segment 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                        oSegment.set_DataElementValue(1, 0, "0022");
                        oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request,01=Cancellation,36=Authority to deduct(Reply)
                        oSegment.set_DataElementValue(3, 0, ControlNumberGeneration("12"));//ReferenceID
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"19990501");//Date
                        string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                        oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());//"1319");


                        #endregion " BHT "

                        #region " Information Source "

                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                        oSegment.set_DataElementValue(1, 0, "1");
                        oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                        oSegment.set_DataElementValue(4, 0, "1");

                        //INFORMATION SOURCE NAME 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                        oSegment.set_DataElementValue(1, 0, "PR");//PR=Payer
                        oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                        oSegment.set_DataElementValue(3, 0, _PayerName);//"INFORMATION SOURCE NAME" );//Payer organisation Name
                        oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                        oSegment.set_DataElementValue(9, 0, _PayerID);//"842610001");//PayerID

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
                        oSegment.set_DataElementValue(3, 0, _ProviderLName);//Provider  LastName
                        oSegment.set_DataElementValue(4, 0, _ProviderFName);//Provider FirstName
                        oSegment.set_DataElementValue(5, 0, _ProviderMName);
                        oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number
                        oSegment.set_DataElementValue(9, 0, _ProviderNPI);//"0202034");//Service Provider No

                        //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                        //oSegment.set_DataElementValue(1, 0, "SY");
                        //oSegment.set_DataElementValue(2, 0, _ProviderSSN);
                        ////oSegment.set_DataElementValue(3, 0, "");
                        //oSegment.set_DataElementValue(4, 0, "");

                        //INFORMATION RECEIVER ADDRESS
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, _ProviderAddress);
                        //oSegment.set_DataElementValue(2, 0, "1");

                        //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, _ProviderCity);
                        oSegment.set_DataElementValue(2, 0, _ProviderState);
                        oSegment.set_DataElementValue(3, 0, _ProviderZip);
                        //oSegment.set_DataElementValue(4, 0, "1");

                        ////INFORMATION RECEIVER CONTACT INFORMATION
                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\PER"));
                        //string[] RecContactQualifier = cmbRecContactFunctionCode.Text.Split('-');
                        //oSegment.set_DataElementValue(1, 0, RecContactQualifier[0].Trim());
                        //oSegment.set_DataElementValue(2, 0, txtReceiverContactName.Text.Trim());
                        //string[] RecContactCommQualifier = cmbRecCommNoQualifier.Text.Split('-');
                        //oSegment.set_DataElementValue(3, 0, RecContactCommQualifier[0].Trim());
                        //oSegment.set_DataElementValue(4, 0, txtReceiverCommNo.Text.Trim());

                        ////INFORMATION RECEIVER PROVIDER INFORMATION
                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\PRV"));
                        //string[] RecProviderCode = cmbReceiverProviderCode.Text.Split('-');
                        //oSegment.set_DataElementValue(1, 0, RecProviderCode[0].Trim());
                        //string[] RecProviderIDQualifier = cmbRecProvRefIDQualifier.Text.Split('-');
                        //oSegment.set_DataElementValue(2, 0, RecProviderIDQualifier[0].Trim());
                        //oSegment.set_DataElementValue(3, 0, txtRecProviderSpecialtyCode.Text.Trim());


                        #endregion " Receiver Loop "

                        #region " Subscriber Loop "


                        //SUBSCRIBER LEVEL
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "3");
                        oSegment.set_DataElementValue(2, 0, "2");
                        oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                        if (_SubscriberRelationShip.Trim() == "18")
                        {
                            oSegment.set_DataElementValue(4, 0, "1");//0=No Subordinate HL Data segment in this Herarchical structure
                        }
                        else
                        {
                            oSegment.set_DataElementValue(4, 0, "0");
                        }

                        //SUBSCRIBER TRACE NUMBER
                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                        //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                        //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                        //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                        oSegment.set_DataElementValue(2, 0, "1"); //1=Person
                        oSegment.set_DataElementValue(3, 0, _SubscriberLName);//"Subscriber Last Name");//
                        oSegment.set_DataElementValue(4, 0, _SubscriberFName);//"Subscriber First Name");
                        if (_SubscriberMName != "")
                        {
                            oSegment.set_DataElementValue(5, 0, _SubscriberMName);//"Subscriber Middle Name");//
                        }


                        string _SubscriberIDCode = "MI";
                        if (rdb_Aetna.Checked == true)
                        {
                            //_SubscriberIDCode = "ZZ";
                            _SubscriberPrimaryID = Convert.ToString(txtServiceType.Tag);
                            oSegment.set_DataElementValue(9, 0, _SubscriberPrimaryID);
                            oSegment.set_DataElementValue(8, 0, _SubscriberIDCode);
                        }
                        else
                        {
                            oSegment.set_DataElementValue(8, 0, _SubscriberIDCode);//MI=Member Identification number,ZZ=Mutually Defined
                            oSegment.set_DataElementValue(9, 0, _SubscriberPrimaryID);//"11122333301");
                        }


                        //SUBSCRIBER ADDITIONAL IDENTIFICATION
                        if (oPatient.DemographicsDetail.PatientSSN.ToString().Trim() != "")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "SY");//SY= Social Security Number
                            oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientSSN.ToString().Trim());  //
                        }
                        else if (_SubscriberAdditionalID.Trim() != "" && _SubscriberRelationShip.Trim() == "18")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "6P");//^P=Group Number
                            oSegment.set_DataElementValue(2, 0, _SubscriberAdditionalID.Trim());  //
                        }
                        //SUBSCRIBER ADDRESS
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientAddress1.Trim());//"Subscriber Address");
                        if (oPatient.DemographicsDetail.PatientAddress2.Trim() != "")
                        {
                            oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientAddress2.Trim());
                        }


                        //SUBSCRIBER CITY,STATE and ZIP
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientCity);//"City");
                        oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientState);//"State");
                        oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientZip);//"ZIP");

                        //SUBSCRIBER PROVIDER INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\PRV"));
                        oSegment.set_DataElementValue(1, 0, "PE");//PC=Primary Care Physician
                        oSegment.set_DataElementValue(2, 0, "HPI");
                        oSegment.set_DataElementValue(3, 0, _ProviderNPI);
                       
                        //SUBSCRIBER DEMOGRAPHIC INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                        oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())));//"Subscriber Date of Birth"); //Date of Birth
                        oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientGender);//"Gender"); //Gender

                        if (_SubscriberRelationShip.Trim() == "18")
                        {
                            
                            ////SUBSCRIBER RELATIONSHIP
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\INS"));
                            oSegment.set_DataElementValue(1, 0, "Y");//Y-Yes or N-No
                            oSegment.set_DataElementValue(2, 0, "18");//18=Self
                            oSegment.set_DataElementValue(17, 0, "1");


                            //SUBSCRIBER DATE

                            if (_SubscriberCardIssueDate.Trim() == "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, _SubscriberCardIssueDate.Trim());
                            }
                        }
                            //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION

                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
  /**/                          oSegment.set_DataElementValue(1, 0, "30");
                            if (txtServiceType.Tag != null && Convert.ToString(txtServiceType.Tag) != "")
                            {
                                //oSegment.set_DataElementValue(1, 0, Convert.ToString(txtServiceType.Tag));
                            }
                            else
                            {
                               // oSegment.set_DataElementValue(1, 0, "30");
                            }
                            //oSegment.set_DataElementValue(3, 0, "FAM");// "FAM");//FAM= Family
                            //oSegment.set_DataElementValue(4, 0, _InsuranceType);//

                       


                        #endregion " Subscriber Loop "

                        #region Dependent Loop
                        //Added By MaheshB

                        if (_SubscriberRelationShip.ToString().ToUpper() != "18")
                        {
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\HL"));
                            oSegment.set_DataElementValue(1, 0, "4");
                            oSegment.set_DataElementValue(2, 0, "3");
                            oSegment.set_DataElementValue(3, 0, "23");
                            oSegment.set_DataElementValue(4, 0, "0");

                            ////DEPENDENT TRACE NUMBER
                            //ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL\\TRN"));
                            //oSegment.set_DataElementValue(1, 0, "1");
                            //oSegment.set_DataElementValue(2, 0, "98175-02157");
                            //oSegment.set_DataElementValue(3, 0, "9877281234");
                            //oSegment.set_DataElementValue(4, 0, "RADIOLOGY");

                            //DEPENDENT NAME
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\NM1"));
                            oSegment.set_DataElementValue(1, 0, "03");
                            oSegment.set_DataElementValue(2, 0, "1");
                            oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientLastName.Trim());
                            oSegment.set_DataElementValue(4, 0, oPatient.DemographicsDetail.PatientFirstName.Trim());
                            if (oPatient.DemographicsDetail.PatientMiddleName.Trim() != "")
                            {
                                oSegment.set_DataElementValue(5, 0, oPatient.DemographicsDetail.PatientMiddleName.Trim());
                            }

                            //DEPENDENT ADDITIONAL IDENTIFICATION
                            if (oPatient.DemographicsDetail.PatientSSN.Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "SY");
                                oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientSSN.Trim());
                            }
                            else if (_SubscriberAdditionalID.Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "6P");//^P=Group Number
                                oSegment.set_DataElementValue(2, 0, _SubscriberAdditionalID.Trim());  //
                            }

                            //DEPENDENT ADDRESS
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N3"));
                            oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientAddress1.Trim());
                            if (oPatient.DemographicsDetail.PatientAddress2.Trim() != "")
                            {
                                oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientAddress2.Trim());
                            }

                            //DEPENDENT CITY/STATE/ZIP CODE
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N4"));
                            oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientCity.Trim());
                            oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientState.Trim());
                            oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientZip.Trim());


                            //PROVIDER INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\PRV"));
                            oSegment.set_DataElementValue(1, 0, "PE");
                            oSegment.set_DataElementValue(2, 0, "HPI");
                            oSegment.set_DataElementValue(3, 0, _ProviderNPI.Trim());

                            //DEPENDENT DEMOGRAPHIC INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\DMG"));
                            oSegment.set_DataElementValue(1, 0, "D8");
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())));
                            oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientGender.Trim().ToUpper());

                            ////SUBSCRIBER RELATIONSHIP
                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\INS"));
                            //oSegment.set_DataElementValue(1, 0, "N");//Y-Yes (for subscriber) or N-No (for Dependent)
                            //oSegment.set_DataElementValue(2, 0, _SubscriberRelationShip.Trim());
                            //oSegment.set_DataElementValue(17, 0, "1");

                            //DEPENDENT DATE
                            if (_SubscriberCardIssueDate.Trim() == "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, _SubscriberCardIssueDate.Trim());
                            }


                            //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\EQ\\EQ"));
                            oSegment.set_DataElementValue(1, 0, "30"); // "98");//Service Type Code, 98 = Professional Visit - Office
                            //oSegment.set_DataElementValue(3, 0, "");// "FAM");//FAM= Family
                            //oSegment.set_DataElementValue(4, 0, _InsuranceType.Trim());//

                        }//if for dependent

                        #endregion


                        #region  " Save EDI File "

                        //Save to a file
                        oEdiDoc.Save(sPath + sEdiFile);
                        string EdiFile = "";
                        EdiFile = sPath + sEdiFile;

                        /////MessageBox.Show("File is sent for Eligibility Inquiry", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClsPostEDI objPostEDI = null;
                        try
                        {
                            objPostEDI = new ClsPostEDI();
                            objPostEDI._FilePath = EdiFile;
                            //objPostEDI.PostEDI("9999", _databaseConnectionString, _PatientId);
                            objPostEDI.PostEDI(_PayerID, _databaseConnectionString, _PatientId);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.GenerateEDI, "Generate EDI 270", _PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                            strEligibilityDetails = "Eligibility is Check for " + _PayerName + "  on " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString();
                        }
                        catch (Exception)// ex)
                        {
                            
                            //ex.ToString();
                            //ex = null;
                        }
                        if ((objPostEDI != null))
                        {
                            objPostEDI.Dispose();
                            objPostEDI = null;
                        }
                        #endregion  " Save EDI File " */

                    }//if for ValidateData()
                    oPatient.Dispose();
                    oPatient = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
                            
    }
     
        private string GetInsurancePlanCode(string InsuranceName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string strSQL = "";
            Object _result = null;
            try
            {
                strSQL = " SELECT BL_InsurancePlanCodes_MST.sPlanCode FROM Contacts_MST INNER JOIN " +
                         " BL_InsurancePlanCodes_MST ON Contacts_MST.sState = BL_InsurancePlanCodes_MST.sState " +
                         " WHERE (Contacts_MST.sName = '" + InsuranceName.Replace("'","''") + "') AND (Contacts_MST.nClinicID = " + _ClinicId + ") ";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                oDB.Disconnect();
                return Convert.ToString(_result);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return "";
            }
        }

        private void LoadEDIObject()
        {
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "270_X092A1.SEF";
                sEdiFile = "270OUTPUT.x12";
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

                //ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("270_X092A1.SEF", 0));
                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));

                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (ofile.Exists == false)
                {
                    MessageBox.Show("SEF file is not present in the base directory.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool ValidateData(gloPatient.Patient oPatient)
        {
            bool _Validated = false;
            string strMissingText = "";
            //string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;
            try
            {
                if (_SubscriberFName == "") { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }
                if (_SubscriberLName == "") { strMissingText += "Subscriber Last Name" + Environment.NewLine + ""; }
                //if (_SubscriberMName == "") { strMissingText += "Subscriber Middle Name" + Environment.NewLine + ""; }
                if (_SubscriberDOB == "") { strMissingText += "Subscriber Date of Birth" + Environment.NewLine + ""; }
                if (_SubscriberRelationShip == "") { strMissingText += "Subscriber Relationship" + Environment.NewLine + ""; }
                if (_InsuranceType == "") { strMissingText += "Insurance Type" + Environment.NewLine + ""; }
                //if (_SubscriberZip == "") { strMissingText += "Subscriber Zip" + Environment.NewLine + ""; }
                //if (_SubscriberGender == "") { strMissingText += "Subscriber Gender" + Environment.NewLine + ""; }
                if (_PayerName == "") { strMissingText += "Payer/Insurance Name" + Environment.NewLine + ""; }
                if (_SubscriberPrimaryID == "") { strMissingText += "Subscriber Insurance ID" + Environment.NewLine + ""; }
                //if (_SubscriberAdditionalID == "") { strMissingText += "Group Number" + Environment.NewLine + ""; }
                //if (_SubscriberAdditionalIDQualifier == "") { strMissingText += " " + Environment.NewLine + ""; }
                if (_PayerID == "") { strMissingText += "Payer ID" + Environment.NewLine + ""; }
                //if (_SubscriberAddress == "") { strMissingText += "Subscriber Address" + Environment.NewLine + ""; }
                //if (_SubscriberCardIssueDate == "") { strMissingText += "Subscriber Card Issue Date" + Environment.NewLine + ""; }
                //if (_ProviderID == "") { strMissingText += "Provider ID" + Environment.NewLine + ""; }
                if (_ProviderFName == "") { strMissingText += "Provider First Name" + Environment.NewLine + ""; }
                if (_ProviderLName == "") { strMissingText += "Provider Last Name" + Environment.NewLine + ""; }
                //if (_ProviderMName == "") { strMissingText += "Provider Middle Name" + Environment.NewLine + ""; }
                if (_ProviderCity == "") { strMissingText += "Provider City" + Environment.NewLine + ""; }
                //if (_ProviderSSN == "") { strMissingText += "Provider SSN" + Environment.NewLine + ""; }
                if (_ProviderNPI == "") { strMissingText += "Provider NPI" + Environment.NewLine + ""; }
                if (_ProviderState == "") { strMissingText += "Provider State" + Environment.NewLine + ""; }
                if (_ProviderZip == "") { strMissingText += "Provider Zip" + Environment.NewLine + ""; }
                if (_ProviderAddress == "") { strMissingText += "Provider Address" + Environment.NewLine + ""; }

                if (_SubscriberRelationShip.ToString().ToUpper() != "18")
                {
                    if (oPatient.DemographicsDetail.PatientFirstName.Trim() == "") { strMissingText += "Patient First Name" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientMiddleName.Trim() == "") { strMissingText += "Patient Middle Name" + Environment.NewLine + ""; }
                    if (oPatient.DemographicsDetail.PatientLastName.Trim() == "") { strMissingText += "Patient LastName Name" + Environment.NewLine + ""; }
                    if (oPatient.DemographicsDetail.PatientAddress1.Trim() == "" && oPatient.DemographicsDetail.PatientAddress2.Trim() == "") { strMissingText += "Patient Address" + Environment.NewLine + ""; }
                    
                    if (oPatient.DemographicsDetail.PatientCity.Trim() == "") { strMissingText += "Patient City" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientCounty.Trim() == "") { strMissingText += "Patient Country" + Environment.NewLine + ""; }
                    if (oPatient.DemographicsDetail.PatientDOB.ToShortDateString() == "") { strMissingText += "Patient Date Of Birth" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientEmail.Trim() == "") { strMissingText += "Patient Fax" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientFax.Trim() == "") { strMissingText += "Patient First Name" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientGuarantor.Trim() == "") { strMissingText += "Patient Guarantor" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientMaritalStatus.Trim() == "") { strMissingText += "Patient MaritalStatus" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientMobile.Trim() == "") { strMissingText += "Patient Mobile" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientOccupation.Trim() == "") { strMissingText += "Patient Occupation" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientSSN.Trim() == "") { strMissingText += "Patient PatientSSN" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientState.Trim() == "") { strMissingText += "Patient State" + Environment.NewLine + ""; }
                    //if (oPatient.DemographicsDetail.PatientRace.Trim() == "") { strMissingText += "Patient First Name" + Environment.NewLine + ""; }
                }

                if (strMissingText.Trim() != "")
                {
                    string _Message = "Following fields are missing for Patient:" + Environment.NewLine + Environment.NewLine;
                    _Message = _Message + strMissingText;
                    _Validated = false;
                    _FilePath = _FilePath + "270Validation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(_Message);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);
                }
                else
                {
                    _Validated = true;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _Validated;
        }

        #endregion " Methods "

        #region " Form Load "

        private void frmEligibilityForm_Load(object sender, EventArgs e)
        {
            FillInsurances();
            FillServiceTypes();
            txtServiceType.Text = "Health Benefit Plan Coverage";
            txtServiceType.Tag = 30;
            this.Size = new Size(716, 205);
            LoadEDIObject();
        }

        #endregion " Form Load "

        #region " Other Events "

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnCheckEligibility_Click(object sender, EventArgs e)
        {
            Generate270EDI();
        }

        private void rdb_Aetna_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatRadioStatus(rdb_Aetna);

                if (rdb_Aetna.Checked == true)
                {
                    _SubscriberPrimaryID = Convert.ToString(txtServiceType.Tag);
                    FillServiceTypes();
                    this.Size = new Size(725, 545);  
                }
                else if (rdbMailHandlers.Checked == true)
                {
                    //_SubscriberAdditionalIDQualifier = "6P";
                    this.Size = new Size(716, 205);  
                }
                else if (rdbMedicaidCalifornia.Checked == true)
                {
                    this.Size = new Size(716, 205);  
                }
                else if (rdbMedicare.Checked == true)
                {
                    _SubscriberPrimaryID = Convert.ToString(txtServiceType.Tag);
                    FillServiceTypes();
                    this.Size = new Size(725, 545);  
                }
                else if (rdbMutualOfOmaha.Checked == true)
                {
                    //_SubscriberAdditionalIDQualifier = "6P";
                    this.Size = new Size(716, 205);  
                }
                else if (rdbTricare.Checked == true)
                {
                   // _SubscriberAdditionalIDQualifier = "18";
                    _SubscriberAdditionalID = _InsurancePlanCode;
                    this.Size = new Size(725, 200);  
                }
                this.Refresh();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        private void rdbMedicare_CheckedChanged(object sender, EventArgs e)
        {
            FormatRadioStatus(rdbMedicare);
            if (rdbMedicare.Checked == true)
            {
                _SubscriberPrimaryID = Convert.ToString(txtServiceType.Tag);
                FillServiceTypes();
                this.Size = new Size(725, 545);
            }
            else
            {
                this.Size = new Size(716, 205);
            }
        }

        private void FormatRadioStatus(RadioButton oControl)
        {
            rdb_Aetna.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            rdbMedicaidCalifornia.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            rdbMedicare.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            rdbMailHandlers.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            rdbMutualOfOmaha.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            rdbTricare.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);

            oControl.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtServiceType.Text = "";
                txtServiceType.Tag = null;
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        txtServiceType.Tag = oListControl.SelectedItems[i].Code;
                        txtServiceType.Text = oListControl.SelectedItems[i].Description;
                    }
                }
                FillServiceTypes();

                if (rdb_Aetna.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMailHandlers.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMedicaidCalifornia.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMedicare.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMutualOfOmaha.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbTricare.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {

            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                txtServiceType.Text = "";
                txtServiceType.Tag = null;

                if (rdb_Aetna.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMailHandlers.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMedicaidCalifornia.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMedicare.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbMutualOfOmaha.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
                else if (rdbTricare.Checked == true)
                {
                    this.Size = new Size(716, 205);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion " Other Events "

        #region " Radio Button Events "

        private void rdbMedicaidCalifornia_CheckedChanged(object sender, EventArgs e)
        {
            FormatRadioStatus(rdbMedicaidCalifornia);
        }

        private void rdbMailHandlers_CheckedChanged(object sender, EventArgs e)
        {
            FormatRadioStatus(rdbMailHandlers);
        }

        private void rdbMutualOfOmaha_CheckedChanged(object sender, EventArgs e)
        {
            FormatRadioStatus(rdbMutualOfOmaha);
        }

        private void rdbTricare_CheckedChanged(object sender, EventArgs e)
        {
            FormatRadioStatus(rdbTricare);
        }

        #endregion " Radio Button Events "

    }
}