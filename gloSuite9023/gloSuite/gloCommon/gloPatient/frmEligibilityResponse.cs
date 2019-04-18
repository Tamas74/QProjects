using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;
using gloPatient.Classes;

namespace gloPatient
{
    public partial class frmEligibilityResponse : Form
    {
        public string EDIReturnResult = "";
        public Int64 _EDIID = 0;
        private bool _IsEligibilityTest = false;
        private string _sEligibilityTestFilePath = "";
        int _LastHeight = 0;
        int _LastWidth = 0;

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

            //Added By Pramod Nair For Messagebox Caption 
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
        public frmEligibilityResponse(String DatabaseConnectionString, Int64 PatientId, Int64 ContactID)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _PatientId = PatientId;
            _ContactID = ContactID;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            //Added By MaheshB
            if (_PatientId == 0)
            {
                tls_btnCheckResponse.Enabled = false;
            }
            else
            {
                tls_btnCheckResponse.Enabled = true;
            }
            #endregion

            //Added By Pramod Nair For Messagebox Caption 
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

        public frmEligibilityResponse(String DatabaseConnectionString, Int64 PatientId, Int64 ContactID, bool IsEligibilityTest, string sEligibilityTestFilePath)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _PatientId = PatientId;
            _ContactID = ContactID;
            _IsEligibilityTest = IsEligibilityTest;
            _sEligibilityTestFilePath = sEligibilityTestFilePath;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            //Added By MaheshB
            if (_PatientId == 0)
            {
                tls_btnCheckResponse.Enabled = false;
            }
            else
            {
                tls_btnCheckResponse.Enabled = true;
            }
            #endregion

            //Added By Pramod Nair For Messagebox Caption 
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

        public frmEligibilityResponse(String DatabaseConnectionString, bool IsService, EDIService.gloEligibilityResponse _ogloEligibilyResponse, Int64 EDIID)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _IsService = IsService;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            //Added By MaheshB
            if (_PatientId == 0)
            {
                tls_btnCheckResponse.Enabled = false;
            }
            else
            {
                tls_btnCheckResponse.Enabled = true;
            }
            #endregion

            //Added By Pramod Nair For Messagebox Caption 
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
            ogloEligibilities = new EDIService.gloEligibilities();
            ogloEligibilityResponse = _ogloEligibilyResponse;
            _EDIID = EDIID;

        }


        //public frmEligibilityResponse(int ProviderID)
        //{
        //    _ProviderID = ProviderID;
        //}
        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _ContactID = 0;
        private Int64 _ClinicId = 0;
        //private Int64 _ProviderID = 0;
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;
        bool _IsService = false;
        EDIService.gloEligibilities ogloEligibilities = null;
        EDIService.gloEligibilityResponse ogloEligibilityResponse = null;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        ediDocument oEdiDoc1 = null;
        ediDataSegment oSegment = null;
        ediSchemas oSchemas = null;
        // ediAcknowledgment oAck = null;
        //ediSchema oSchema = null;
        string sSefFile = "";
        string sEdiFile = "";
        string sPath = "";
        int textLengthBefore;
        int textLengthAfter;


        #region "Constants For Grid"

        private const int COL_SRNO = 0;
        private const int COL_BENEFIT = 1;
        private const int COL_COVERAGE_LEVEL = 2;
        private const int COL_SERVICETYPE = 3;
        private const int COL_BENEFITAMOUNT = 4;
        private const int COL_MESSAGE = 5;

        private const int COL_COUNT = 6;

        #endregion "Constants For Grid"



        #region " Design Grid "


        private void DesignGrid()
        {
            try
            {
                c1Response.Rows.Count = 1;
                c1Response.Cols.Count = COL_COUNT;
                c1Response.Rows.Fixed = 1;
                c1Response.Cols.Fixed = 0;

                c1Response.SetData(0, COL_SRNO, "Sr.No.");
                c1Response.SetData(0, COL_BENEFIT, "Benefit");
                c1Response.SetData(0, COL_COVERAGE_LEVEL, "Coverage Type");
                c1Response.SetData(0, COL_MESSAGE, "Message");
                c1Response.SetData(0, COL_SERVICETYPE, "Service Type");
                c1Response.SetData(0, COL_BENEFITAMOUNT, "Benefit Amount");

                c1Response.Cols[COL_SRNO].Visible = false;
                c1Response.Cols[COL_BENEFIT].Width = 150;
                c1Response.Cols[COL_COVERAGE_LEVEL].Width = 160;
                c1Response.Cols[COL_SERVICETYPE].Width = 200;
                c1Response.Cols[COL_BENEFITAMOUNT].Width = 110;
                c1Response.Cols[COL_MESSAGE].Width = 400;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion " Design Grid "

        public string CheckEligibilityRejections()
        {
            string sEligibilityRejection = string.Empty;
            LoadEDIObject();
            DesignGrid();
            sEligibilityRejection = Translate271Response_EligibilityRejection();
            return sEligibilityRejection;
        }
        private bool LoadEDIObject()
        {
            bool _result = true;
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
                //oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                oEdiDoc1.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                //oAck = (ediAcknowledgment)oEdiDoc1.GetAcknowledgment();
                //oAck.EnableFunctionalAcknowledgment = true;

                //// Set the starting point of the control numbers in the acknowledgment
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                //oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                if (!_IsEligibilityTest)
                {
                    //oEdiDoc1.LoadSchema(sPath + sSefFile, 0);
                    oEdiDoc1.ImportSchema(sPath + sSefFile, 0);
                    //oEdiDoc1.LoadSchema(sPath + "997_X12-4010.SEF", 0);
                    //ediSchema.Set(ref oSchemas, oEdiDoc.LoadSchema("997_X12-4010.SEF", 0));	//for Ack (997) file
                    //oEdiDoc1 = new ediDocument();
                    //sEdiFile = "EligibilityResponse.X12";
                    oEdiDoc1.LoadEdi(gloSettings.FolderSettings.AppTempFolderPath  + sEdiFile);
                    _result = true;
                }
                else
                {
                    if (System.IO.File.Exists(_sEligibilityTestFilePath))
                    {
                        //oEdiDoc1.LoadSchema(sPath + sSefFile, 0);
                        oEdiDoc1.ImportSchema(sPath + sSefFile, 0);
                        //oEdiDoc1.LoadSchema(sPath + "997_X12-4010.SEF", 0);
                        //ediSchema.Set(ref oSchemas, oEdiDoc.LoadSchema("997_X12-4010.SEF", 0));	//for Ack (997) file
                        //oEdiDoc1 = new ediDocument();
                        //sEdiFile = "EligibilityResponse.X12";
                        oEdiDoc1.LoadEdi(_sEligibilityTestFilePath);
                        _result = true;
                    }
                    else
                    {
                        MessageBox.Show("Selected file doesn't exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        _result = false;
                    }
                }
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                //_result = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _result;
        }

        /*      private void Translate271Response()
              {
                  gloEligibilityResponse ogloEligibilityResponse = new gloEligibilityResponse();
                  gloEligibility ogloEligibility = new gloEligibility();
                  gloEligibilities ogloEligibilities = new gloEligibilities();
                  EligibilityResponse oEligibility = new EligibilityResponse(_databaseConnectionString);
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
                      int _rowIndex = 0;
                      int nArea;

                      string sValue = "";
                      //Int32 _nArea2RowCount = 0;
                      //int Area2rowIndex = 0;
                      //int rowIndex = 0;
                      //int i = 0;
                      // Gets the first data segment in the EDI files
                      ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                      DesignGrid();
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
                                      sValue = oSegment.get_DataElementValue(1);
                                      sValue = oSegment.get_DataElementValue(2);
                                      sValue = oSegment.get_DataElementValue(3);
                                      ogloEligibilityResponse = new gloEligibilityResponse();
                                      ogloEligibilityResponse.PatientID = _PatientId;
                                      ogloEligibilityResponse.ReferenceID = oSegment.get_DataElementValue(3);
                                      ogloEligibilityResponse.ClinicID = _ClinicId;
                                      ogloEligibilityResponse.dtEligibilityCheck = DateTime.Now;
                                      sValue = oSegment.get_DataElementValue(4);
                                      sValue = oSegment.get_DataElementValue(5);
                                      sValue = oSegment.get_DataElementValue(6);

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
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);

                                      }
                                      else if (sSegmentID == "AAA")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                      }

                                  }//end loop section HL

                                  else if (sLoopSection == "HL;NM1")
                                  {
                                      //Payer/Information Source Name
                                      if (sSegmentID == "NM1")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          txtInsuranceName.Text = oSegment.get_DataElementValue(3);
                                          ogloEligibilityResponse.PayerName = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);
                                          ogloEligibilityResponse.PayerID = oSegment.get_DataElementValue(9);
                                      }
                                      else if (sSegmentID == "REF")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);

                                      }
                                      else if (sSegmentID == "PER")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          ogloEligibilityResponse.PayerContactName = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          ogloEligibilityResponse.PayerContactNumber = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);

                                      }
                                      else if (sSegmentID == "AAA")
                                      {
                                          if (oSegment.get_DataElementValue(1) == "N")
                                          {
                                              sValue = oSegment.get_DataElementValue(2);
                                              if (oSegment.get_DataElementValue(3).Trim() != "")
                                              {
                                                  listResponse.Visible = true;
                                                  c1Response.Visible = false;
                                                  listResponse.Dock = DockStyle.Fill;
                                                  if (GetSourceRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                  {
                                                      listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                                  }
                                                  if (GetSourceFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                  {
                                                      listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                                  }
                                              }

                                              EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

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
                                      //Receiver/Provider Name
                                      if (sSegmentID == "NM1")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          if (oSegment.get_DataElementValue(5).Trim() != "")
                                          {
                                              ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                          }
                                          else
                                          {
                                              ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                          }
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);
                                          ogloEligibilityResponse.ReceiverID = oSegment.get_DataElementValue(9);
                                      }
                                      else if (sSegmentID == "REF")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          ogloEligibilityResponse.ReceiverAdditionalID = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                      }
                                      else if (sSegmentID == "AAA")
                                      {
                                          //if (oSegment.get_DataElementValue(1) == "N")
                                          //{
                                          sValue = oSegment.get_DataElementValue(2);
                                          if (oSegment.get_DataElementValue(3).Trim() != "")
                                          {
                                              listResponse.Visible = true;
                                              c1Response.Visible = false;
                                              listResponse.Dock = DockStyle.Fill;
                                              if (GetReceiverRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                              {
                                                  listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                              }
                                              if (GetReceiverFollowUp(oSegment.get_DataElementValue(4)) != "")
                                              {
                                                  listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                              }
                                          }

                                          EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                          //}
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
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          txtPatientName.Text = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                          if (oSegment.get_DataElementValue(5).Trim() != "")
                                          {
                                              ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                          }
                                          else
                                          {
                                              ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                          }
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);
                                      }
                                      else if (sSegmentID == "N3")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);

                                      }
                                      else if (sSegmentID == "N4")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);

                                      }
                                      else if (sSegmentID == "PER")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);
                                      }
                                      else if (sSegmentID == "AAA")
                                      {
                                          //if (oSegment.get_DataElementValue(1) == "N")
                                          //{
                                          sValue = oSegment.get_DataElementValue(2);
                                          if (oSegment.get_DataElementValue(3).Trim() != "")
                                          {
                                              listResponse.Visible = true;
                                              c1Response.Visible = false;
                                              listResponse.Dock = DockStyle.Fill;
                                              if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                              {
                                                  listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                              }
                                              if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                              {
                                                  listResponse.Items.Add("Subscriber Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                              }
                                          }

                                          EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                          //}
                                      }
                                      else if (sSegmentID == "DMG")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          if (oSegment.get_DataElementValue(2).Trim() != "")
                                          {
                                              ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                          }
                                          string _strGender = "";
                                          if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "M")
                                          {
                                              _strGender = "Male";
                                          }
                                          else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "F")
                                          {
                                              _strGender = "Female";
                                          }
                                          else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "U")
                                          {
                                              _strGender = "Unknown";
                                          }

                                          ogloEligibilityResponse.SubscriberGender = _strGender;

                                      }
                                      else if (sSegmentID == "INS")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(9);
                                          sValue = oSegment.get_DataElementValue(10);
                                      }
                                      else if (sSegmentID == "DTP")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          if (oSegment.get_DataElementValue(1) == "307")
                                          {
                                              if (oSegment.get_DataElementValue(3).Trim() != "")
                                              {
                                                  txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                                  ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(oSegment.get_DataElementValue(3));
                                              }
                                          }
                                          else if (oSegment.get_DataElementValue(1) == "472")
                                          {
                                              txtServiceDate.Text = oSegment.get_DataElementValue(3);
                                          }
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                      }
                                      else if (sSegmentID == "HSD")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          sValue = oSegment.get_DataElementValue(6);
                                          sValue = oSegment.get_DataElementValue(7);
                                          sValue = oSegment.get_DataElementValue(8);

                                      }
                                  }

                                  else if (sLoopSection == "HL;NM1;EB")
                                  {
                                      string strCoverageLevel = "";

                                      if (sSegmentID == "EB")
                                      {
                                          Qlfr = oSegment.get_DataElementValue(1);
                                      }
                                      if (sSegmentID == "EB")
                                      {
                                          if (ogloEligibility.BenefitInformation != null && ogloEligibility.BenefitInformation.Trim()!="")
                                          {
                                             //ogloEligibilities = new gloEligibilities();
                                              ogloEligibilities.Add(ogloEligibility);

                                          }
                                          ogloEligibility = new gloEligibility();
                                          c1Response.Rows.Add();
                                          _rowIndex = c1Response.Rows.Count - 1;
                                          c1Response.SetData(_rowIndex, COL_BENEFIT, GetBenefitDescription(Qlfr));//Benefit Information
                                          ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                          if (oSegment.get_DataElementValue(2).Trim() != "")
                                          {
                                              c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, GetCoverageDescription(oSegment.get_DataElementValue(2)));//Coverage Level code
                                              ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                          }
                                          if (oSegment.get_DataElementValue(3).Trim() != "")
                                          {
                                              c1Response.SetData(_rowIndex, COL_SERVICETYPE, GetServiceTypeDescription(oSegment.get_DataElementValue(3)));//Service Type
                                              ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                          }
                                          sValue = oSegment.get_DataElementValue(4);
                                          sValue = oSegment.get_DataElementValue(5);
                                          sValue = oSegment.get_DataElementValue(6);
                                          if (oSegment.get_DataElementValue(6).Trim() != "")
                                          {
                                              ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                          }
                                          c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, oSegment.get_DataElementValue(7));
                                          if (oSegment.get_DataElementValue(7).Trim() != "")
                                          {
                                              ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                          }
                                          sValue = oSegment.get_DataElementValue(8);
                                          sValue = oSegment.get_DataElementValue(9);
                                          if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                          {
                                              ogloEligibility.IsPlanNetwork = true;
                                          }
                                          else
                                          {
                                              ogloEligibility.IsPlanNetwork = false;
                                          }


                                      }

                                      if (sSegmentID == "MSG")
                                      {
                                          c1Response.SetData(_rowIndex, COL_MESSAGE, oSegment.get_DataElementValue(1));
                                          ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                      }
                                      else if (sSegmentID == "AAA")
                                      {
                                          sValue = oSegment.get_DataElementValue(2);
                                          if (oSegment.get_DataElementValue(3).Trim() != "")
                                          {
                                              listResponse.Visible = true;
                                              c1Response.Visible = false;
                                              listResponse.Dock = DockStyle.Fill;
                                              if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                              {
                                                  listResponse.Items.Add("Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                              }
                                              if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                              {
                                                  listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                              }
                                          }
                                      }
                                      else if (sSegmentID == "REF")
                                      {
                                          sValue = oSegment.get_DataElementValue(1);
                                          sValue = oSegment.get_DataElementValue(2);
                                          sValue = oSegment.get_DataElementValue(3);
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
                      string str=GetEligibilityDate();
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
                      ogloEligibilityResponse.Eligibilities = ogloEligibilities;
                      oEligibility.AddEligibility(ogloEligibilityResponse);

               
                  }
                  catch (Exception ex)
                  {
                      gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                  }
                  finally
                  {
                      if (ogloEligibility != null)
                      {
                          ogloEligibility.Dispose();
                      }
                      if (ogloEligibilityResponse != null)
                      {
                          ogloEligibilityResponse.Dispose();
                      }
                      if (oEligibility != null)
                      {
                          oEligibility.Dispose();
                      }
                  }
              } */
        private void Translate271Response()
        {
            gloEligibilityResponse ogloEligibilityResponse = new gloEligibilityResponse();
            gloEligibility ogloEligibility = new gloEligibility();
            gloEligibilities ogloEligibilities = new gloEligibilities();
            EligibilityResponse oEligibility = new EligibilityResponse(_databaseConnectionString);
            try
            {
                string sSegmentID = "";
                string sLoopSection = "";
                // string sLXID = "";
                // string sPath = "";
                string sEntity = "";
                string Qlfr = "";

                // string strRejectionCode = "";
                //string strFollowupCode = "";
                int _rowIndex = 0;
                int nArea;

                string sValue = "";
                rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);
                //rtfError.Text = "";

                //rtfeligibilityinfo.ForeColor = System.Drawing.Color.AliceBlue;
                //rtfeligibilityinfo.Font.Style = System.Drawing.FontStyle.Bold;
                //Int32 _nArea2RowCount = 0;
                //int Area2rowIndex = 0;
                //int rowIndex = 0;
                //int i = 0;
                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                textLengthBefore = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.AppendText("Patient Eligibility Response:");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 14.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                bool isPCPSegment = false;

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
                                sValue = oSegment.get_DataElementValue(1);
                                sValue = oSegment.get_DataElementValue(2);
                                sValue = oSegment.get_DataElementValue(3);
                                ogloEligibilityResponse = new gloEligibilityResponse();
                                ogloEligibilityResponse.PatientID = _PatientId;
                                ogloEligibilityResponse.ReferenceID = oSegment.get_DataElementValue(3);
                                ogloEligibilityResponse.ClinicID = _ClinicId;
                                ogloEligibilityResponse.dtEligibilityCheck = DateTime.Now;
                                ogloEligibilityResponse.ContactID = Convert.ToInt64(_ContactID);
                                ogloEligibilityResponse.ANSIVersion = gloSettings.ANSIVersions.ANSI_4010.GetHashCode();
                                sValue = oSegment.get_DataElementValue(4);
                                sValue = oSegment.get_DataElementValue(5);
                                sValue = oSegment.get_DataElementValue(6);

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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                }

                            }//end loop section HL

                            else if (sLoopSection == "HL;NM1")
                            {
                                //Payer/Information Source Name
                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    //txtInsuranceName.Text = oSegment.get_DataElementValue(3);
                                    //rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    //rtfeligibilityinfo.Font.Style = System.Drawing.FontStyle.Bold;
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("Payer Information: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                                    rtfeligibilityinfo.SelectionColor = Color.White;
                                    rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);

                                    //rtfeligibilityinfo.SelectionColor = Color.BlueViolet;

                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText("       ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.AppendText("Name: ");
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(3));
                                    ogloEligibilityResponse.PayerName = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.PayerID = oSegment.get_DataElementValue(9);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText("       ");

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("Payer Identification: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    //rtfeligibilityinfo.ForeColor = System.Drawing.Color.Blue;
                                    rtfeligibilityinfo.AppendText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PayerContactName = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    ogloEligibilityResponse.PayerContactNumber = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            //listResponse.Visible = true;
                                            c1Response.Visible = false;
                                            panel1.Visible = false;
                                            splitter1.Visible = false;
                                            //listResponse.Dock = DockStyle.Fill;
                                            if (GetSourceRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                            {

                                                rtfError.AppendText(Environment.NewLine);

                                                textLengthBefore = rtfeligibilityinfo.TextLength;
                                                rtfError.AppendText("       Payer Rejection Reason: ");
                                                rtfError.AppendText(GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                                textLengthAfter = rtfeligibilityinfo.TextLength;
                                                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                                //listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                            }
                                            if (GetSourceFollowUp(oSegment.get_DataElementValue(4)) != "")
                                            {

                                                rtfError.AppendText(Environment.NewLine);

                                                textLengthBefore = rtfeligibilityinfo.TextLength;
                                                rtfError.AppendText("       Payer Follow up: ");
                                                rtfError.AppendText(GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                                textLengthAfter = rtfeligibilityinfo.TextLength;
                                                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                                //listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                            }
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

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
                                //Receiver/Provider Name
                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    else
                                    {
                                        ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.ReceiverID = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.ReceiverAdditionalID = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetReceiverRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Receiver Rejection Reason:");
                                            rtfError.AppendText(GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetReceiverFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Receiver Follow up: ");
                                            rtfError.AppendText(GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                    //}
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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    //txtPatientName.Text = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    //rtfeligibilityinfo.Font = System.Drawing.FontStyle.Bold;

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("Insured/Subscriber Information: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                                    rtfeligibilityinfo.SelectionColor = Color.White;
                                    rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);

                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);

                                    //rtfeligibilityinfo.Font = System.Drawing.FontStyle.Regular;
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("     Name: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3));
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        //rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);
                                        //textLengthBefore = rtfeligibilityinfo.TextLength;
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                        //textLengthAfter = rtfeligibilityinfo.TextLength;
                                        //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        //rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    }
                                    else
                                    {

                                        //rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);
                                        //textLengthBefore = rtfeligibilityinfo.TextLength;
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                        //textLengthAfter = rtfeligibilityinfo.TextLength;
                                        //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        //rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("     Identification Number: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);


                                    //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.SubscriberID = sValue;
                                }
                                else if (sSegmentID == "N3")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);

                                }
                                else if (sSegmentID == "N4")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            //rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Subscriber Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);
                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Subscriber Follow up: ");
                                            //rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                                            if (Convert.ToString(oSegment.get_DataElementValue(4)).Trim().ToUpper() == "C")
                                            {
                                                rtfError.AppendText("Please Correct and Resubmit");
                                            }
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                            //listResponse.Items.Add("" + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                    //}
                                }
                                else if (sSegmentID == "DMG")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Date of Birth: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString());
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                    }
                                    string _strGender = "";
                                    if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "M")
                                    {
                                        _strGender = "Male";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "F")
                                    {
                                        _strGender = "Female";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "U")
                                    {
                                        _strGender = "Unknown";
                                    }

                                    ogloEligibilityResponse.SubscriberGender = _strGender;

                                }
                                else if (sSegmentID == "INS")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(9);
                                    sValue = oSegment.get_DataElementValue(10);
                                }
                                else if (sSegmentID == "DTP")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (oSegment.get_DataElementValue(1) == "307")
                                    {
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            string _date = "";
                                            string[] _dateRange = null;
                                            _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                            _dateRange = _date.Split('-');


                                            rtfeligibilityinfo.AppendText(Environment.NewLine);
                                            rtfeligibilityinfo.AppendText(Environment.NewLine);
                                            //rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString());
                                            if (_dateRange != null && _dateRange.Length > 0)
                                            {
                                                textLengthAfter = rtfeligibilityinfo.TextLength;
                                                rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                                //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                                ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                            }
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        string _date = "";
                                        string[] _dateRange = null;
                                        _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                        _dateRange = _date.Split('-');

                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        //rtfeligibilityinfo.AppendText("        ");
                                        if (_dateRange != null && _dateRange.Length > 0)
                                        {
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                            //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                            ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                        }
                                        //txtServiceDate.Text = oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "HSD")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);

                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                //string strCoverageLevel = "";

                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }

                                if (sSegmentID == "EB")
                                {

                                    ogloEligibility = new gloEligibility();
                                    c1Response.Rows.Add();
                                    _rowIndex = c1Response.Rows.Count - 1;
                                    c1Response.SetData(_rowIndex, COL_BENEFIT, GetBenefitDescription(Qlfr));//Benefit Information
                                    //Benefit Information
                                    ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                    if (ogloEligibility.BenefitInformation != null)
                                    {
                                        //ogloEligibilities = new gloEligibilities();
                                        ogloEligibilities.Add(ogloEligibility);

                                    }
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, GetCoverageDescription(oSegment.get_DataElementValue(2)));//Coverage Level code
                                        ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                    }
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_SERVICETYPE, GetServiceTypeDescription(oSegment.get_DataElementValue(3)));//Service Type
                                        ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                    }
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    if (oSegment.get_DataElementValue(6).Trim() != "")
                                    {
                                        ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                    }
                                    c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, oSegment.get_DataElementValue(7));
                                    if (oSegment.get_DataElementValue(7).Trim() != "")
                                    {
                                        ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                    }
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                    {
                                        ogloEligibility.IsPlanNetwork = true;
                                    }
                                    else
                                    {
                                        ogloEligibility.IsPlanNetwork = false;
                                    }


                                }

                                if (sSegmentID == "MSG")
                                {
                                    c1Response.SetData(_rowIndex, COL_MESSAGE, oSegment.get_DataElementValue(1));
                                    ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add(" " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                            //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }
                            else if (sLoopSection == "HL;NM1;EB;NM1")
                            {
                                if (sSegmentID == "NM1")
                                {
                                    if (oSegment.get_DataElementValue(1) == "P3")
                                    {
                                        isPCPSegment = true;

                                        sValue = Convert.ToString(oSegment.get_DataElementValue(4)) + " " +
                                            Convert.ToString(oSegment.get_DataElementValue(3)) + " " +
                                            Convert.ToString(oSegment.get_DataElementValue(5));
                                        ogloEligibilityResponse.PrimaryCarePhysicainName = sValue;

                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("Primary Care Provider: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                        rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                                        rtfeligibilityinfo.SelectionColor = Color.White;
                                        rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Name: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(sValue);
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;

                                        sValue = "";
                                        sValue = oSegment.get_DataElementValue(9);

                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Identification Number: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(sValue);
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;
                                    }
                                    else
                                    { isPCPSegment = false; }
                                }
                                else if (sSegmentID == "N3" && isPCPSegment == true)
                                {
                                    ogloEligibilityResponse.PrimaryCareAddress = Convert.ToString(oSegment.get_DataElementValue(1)) + Environment.NewLine + Convert.ToString(oSegment.get_DataElementValue(2));
                                }
                                else if (sSegmentID == "N4" && isPCPSegment == true)
                                {
                                    ogloEligibilityResponse.PrimaryCareCity = Convert.ToString(oSegment.get_DataElementValue(1));
                                    ogloEligibilityResponse.PrimaryCareState = Convert.ToString(oSegment.get_DataElementValue(2));
                                    ogloEligibilityResponse.PrimaryCareZip = Convert.ToString(oSegment.get_DataElementValue(3));
                                }
                                else if (sSegmentID == "PER" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PrimaryCarePhysicainContactName = Convert.ToString(sValue);
                                    sValue = oSegment.get_DataElementValue(4);
                                    ogloEligibilityResponse.PrimaryCarePhysicainContactNumber = Convert.ToString(sValue);
                                    sValue = "";
                                }
                            }
                            else if (sLoopSection == "HL;NM1;EB;LS;NM1")
                            {


                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (sValue == "P3") //Primary Care Provider
                                    {
                                        isPCPSegment = true;
                                        sValue = oSegment.get_DataElementValue(2);
                                        sValue = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);
                                        sValue = oSegment.get_DataElementValue(5);

                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("Primary Care Provider: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                        rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                                        rtfeligibilityinfo.SelectionColor = Color.White;
                                        rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Name: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3));
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                        if (oSegment.get_DataElementValue(5).Trim() != "")
                                        {
                                            ogloEligibilityResponse.PrimaryCarePhysicainName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                        }
                                        else
                                        {
                                            ogloEligibilityResponse.PrimaryCarePhysicainName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                        }

                                        sValue = oSegment.get_DataElementValue(6);
                                        sValue = oSegment.get_DataElementValue(7);
                                        sValue = oSegment.get_DataElementValue(8);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Identification Number: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);


                                        //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                        sValue = oSegment.get_DataElementValue(9);
                                        //ogloEligibilityResponse.primarycarep = sValue;
                                    }
                                    else { isPCPSegment = false; }

                                }
                                else if (sSegmentID == "N3" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    ogloEligibilityResponse.PrimaryCareAddress = sValue;
                                    sValue = oSegment.get_DataElementValue(2);
                                }
                                else if (sSegmentID == "N4" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    ogloEligibilityResponse.PrimaryCareCity = sValue;

                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PrimaryCareState = sValue;

                                    sValue = oSegment.get_DataElementValue(3);
                                    ogloEligibilityResponse.PrimaryCareZip = sValue;
                                }
                                else if (sSegmentID == "PER" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }


                            }
                            //if (sLoopSection == "HL;NM1;DTP")
                            //{

                            //}


                        }
                        else if (sEntity == "23")
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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    //txtPatientName.Text = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    //rtfeligibilityinfo.Font = System.Drawing.FontStyle.Bold;

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("Insured/Subscriber Information: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                                    rtfeligibilityinfo.SelectionColor = Color.White;
                                    rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);

                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);

                                    //rtfeligibilityinfo.Font = System.Drawing.FontStyle.Regular;
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("     Name: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3));
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        //rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);
                                        //textLengthBefore = rtfeligibilityinfo.TextLength;
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                        //textLengthAfter = rtfeligibilityinfo.TextLength;
                                        //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        //rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    }
                                    else
                                    {

                                        //rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);
                                        //textLengthBefore = rtfeligibilityinfo.TextLength;
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                        //textLengthAfter = rtfeligibilityinfo.TextLength;
                                        //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        //rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText("     Identification Number: ");
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);


                                    //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    textLengthBefore = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                                    textLengthAfter = rtfeligibilityinfo.TextLength;
                                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "N3")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);

                                }
                                else if (sSegmentID == "N4")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            //rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Subscriber Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);
                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Subscriber Follow up: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                            //listResponse.Items.Add("" + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                    //}
                                }
                                else if (sSegmentID == "DMG")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText("     Date of Birth: ");
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                        textLengthBefore = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.AppendText(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString());
                                        textLengthAfter = rtfeligibilityinfo.TextLength;
                                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                    }
                                    string _strGender = "";
                                    if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "M")
                                    {
                                        _strGender = "Male";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "F")
                                    {
                                        _strGender = "Female";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "U")
                                    {
                                        _strGender = "Unknown";
                                    }

                                    ogloEligibilityResponse.SubscriberGender = _strGender;

                                }
                                else if (sSegmentID == "INS")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(9);
                                    sValue = oSegment.get_DataElementValue(10);
                                }
                                else if (sSegmentID == "DTP")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (oSegment.get_DataElementValue(1) == "307")
                                    {
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            string _date = "";
                                            string[] _dateRange = null;
                                            _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                            _dateRange = _date.Split('-');


                                            rtfeligibilityinfo.AppendText(Environment.NewLine);
                                            rtfeligibilityinfo.AppendText(Environment.NewLine);
                                            //rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString());
                                            if (_dateRange != null && _dateRange.Length > 0)
                                            {
                                                textLengthAfter = rtfeligibilityinfo.TextLength;
                                                rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                                //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                                ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                            }
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        string _date = "";
                                        string[] _dateRange = null;
                                        _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                        _dateRange = _date.Split('-');

                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        rtfeligibilityinfo.AppendText(Environment.NewLine);
                                        //rtfeligibilityinfo.AppendText("        ");
                                        if (_dateRange != null && _dateRange.Length > 0)
                                        {
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                                            //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                            ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                        }
                                        //txtServiceDate.Text = oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "HSD")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);

                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                //string strCoverageLevel = "";

                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }
                                if (sSegmentID == "EB")
                                {

                                    ogloEligibility = new gloEligibility();
                                    c1Response.Rows.Add();
                                    _rowIndex = c1Response.Rows.Count - 1;
                                    c1Response.SetData(_rowIndex, COL_BENEFIT, GetBenefitDescription(Qlfr));//Benefit Information
                                    //Benefit Information
                                    ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                    if (ogloEligibility.BenefitInformation != null)
                                    {
                                        //ogloEligibilities = new gloEligibilities();
                                        ogloEligibilities.Add(ogloEligibility);

                                    }
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, GetCoverageDescription(oSegment.get_DataElementValue(2)));//Coverage Level code
                                        ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                    }
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_SERVICETYPE, GetServiceTypeDescription(oSegment.get_DataElementValue(3)));//Service Type
                                        ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                    }
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    if (oSegment.get_DataElementValue(6).Trim() != "")
                                    {
                                        ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                    }
                                    c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, oSegment.get_DataElementValue(7));
                                    if (oSegment.get_DataElementValue(7).Trim() != "")
                                    {
                                        ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                    }
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                    {
                                        ogloEligibility.IsPlanNetwork = true;
                                    }
                                    else
                                    {
                                        ogloEligibility.IsPlanNetwork = false;
                                    }


                                }

                                if (sSegmentID == "MSG")
                                {
                                    c1Response.SetData(_rowIndex, COL_MESSAGE, oSegment.get_DataElementValue(1));
                                    ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                                            //listResponse.Items.Add(" " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            textLengthBefore = rtfeligibilityinfo.TextLength;
                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                            textLengthAfter = rtfeligibilityinfo.TextLength;
                                            rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                                            rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                                            //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }
                            //if (sLoopSection == "HL;NM1;DTP")
                            //{

                            //}
                        }
                        else
                        {
                            //if (sLoopSection == "HL;NM1;EB;III")
                            //{

                            //}

                        }
                    }
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());

                }
                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.

                // Gets the first segment of the 997 acknowledgment file
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                //while (oSegment != null)
                //{
                //    nArea = oSegment.Area;
                //    sLoopSection = oSegment.LoopSection;
                //    sSegmentID = oSegment.ID;

                //    if (nArea == 1)
                //    {
                //        if (sLoopSection == "")
                //        {
                //            if (sSegmentID == "AK9")
                //            {
                //                if (oSegment.get_DataElementValue(1, 0) == "R")
                //                {
                //                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                //                }
                //            }
                //        }	// sLoopSection == ""
                //    }	//nArea == 1
                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                //}	//oSegment != null

                #region //Show Errors

                if (rtfError.Text != "")
                {
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    rtfeligibilityinfo.AppendText("Request Not Valid:");
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
                    rtfeligibilityinfo.SelectionColor = Color.Maroon;

                    rtfeligibilityinfo.AppendText(Environment.NewLine);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText(rtfError.Text);
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                    //rtfeligibilityinfo.SelectionColor(Color.Maroon);
                }



                #endregion

                ////save the acknowledgment
                ////string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "997_277\\";
                //string sDirectoryPath =appSettings["StartupPath"].ToString()+"\\" + "997_277\\";
                //if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                //oAck.Save(sDirectoryPath + "997_270.X12");
                ogloEligibilityResponse.Eligibilities = ogloEligibilities;


                oEligibility.AddEligibility(ogloEligibilityResponse);

                #region "Resize Screen"

                Graphics g = this.rtfeligibilityinfo.CreateGraphics();
                SizeF result = g.MeasureString(this.rtfeligibilityinfo.Text, this.rtfeligibilityinfo.Font, this.rtfeligibilityinfo.ClientSize.Width);

                if (result != null)
                {
                    if (Convert.ToInt32(result.Height) > 0)
                    {
                        panel2.Height = Convert.ToInt32(result.Height + 2);
                        panel2.Refresh();

                        this.rtfeligibilityinfo.Height = Convert.ToInt32(result.Height + 1);
                        rtfeligibilityinfo.Refresh();
                    }
                    if (this.Height < _LastHeight)
                    {
                        this.Height = Convert.ToInt32(_LastHeight);
                        this.Refresh();
                    }
                    if (Convert.ToInt32(result.Width) > 0)
                    {
                        panel2.Width = Convert.ToInt32(result.Width + 2);
                        panel2.Refresh();

                        this.rtfeligibilityinfo.Width = Convert.ToInt32(result.Width + 1);
                        rtfeligibilityinfo.Refresh();
                    }
                    if (this.Width < _LastWidth)
                    {
                        this.Width = Convert.ToInt32(_LastWidth);
                        this.Refresh();
                    }

                }

                if (g != null)
                {
                    g.Dispose();
                }

                if (c1Response.Visible == false)
                {
                    this.Height = rtfeligibilityinfo.Height + 100;
                }

                #endregion

            }
            catch (Exception ex)
            {
                if (!_IsEligibilityTest)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }
            finally
            {
                if (ogloEligibility != null)
                {
                    ogloEligibility.Dispose();
                }
                if (ogloEligibilityResponse != null)
                {
                    ogloEligibilityResponse.Dispose();
                }
                if (oEligibility != null)
                {
                    oEligibility.Dispose();
                }

                if (oSegment != null)
                {
                    oSegment.Dispose();
                }

                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }

                if (oEdiDoc1 != null)
                {
                    oEdiDoc1.Close();
                    oEdiDoc1.Dispose();
                }
            }
        }

        private string Translate271Response_EligibilityRejection()
        {
            string RejectionDetails = string.Empty;
            gloEligibilityResponse ogloEligibilityResponse = new gloEligibilityResponse();
            gloEligibility ogloEligibility = new gloEligibility();
            gloEligibilities ogloEligibilities = new gloEligibilities();
            EligibilityResponse oEligibility = new EligibilityResponse(_databaseConnectionString);
            try
            {
                string sSegmentID = "";
                string sLoopSection = "";
                // string sLXID = "";
                // string sPath = "";
                string sEntity = "";
                string Qlfr = "";

                // string strRejectionCode = "";
                //string strFollowupCode = "";
                int _rowIndex = 0;
                int nArea;

                string sValue = "";
                //rtfError.Text = "";

                //Int32 _nArea2RowCount = 0;
                //int Area2rowIndex = 0;
                //int rowIndex = 0;
                //int i = 0;
                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                bool isPCPSegment = false;

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
                                sValue = oSegment.get_DataElementValue(1);
                                sValue = oSegment.get_DataElementValue(2);
                                sValue = oSegment.get_DataElementValue(3);
                                ogloEligibilityResponse = new gloEligibilityResponse();
                                ogloEligibilityResponse.PatientID = _PatientId;
                                ogloEligibilityResponse.ReferenceID = oSegment.get_DataElementValue(3);
                                ogloEligibilityResponse.ClinicID = _ClinicId;
                                ogloEligibilityResponse.dtEligibilityCheck = DateTime.Now;
                                ogloEligibilityResponse.ContactID = Convert.ToInt64(_ContactID);
                                ogloEligibilityResponse.ANSIVersion = gloSettings.ANSIVersions.ANSI_4010.GetHashCode();
                                sValue = oSegment.get_DataElementValue(4);
                                sValue = oSegment.get_DataElementValue(5);
                                sValue = oSegment.get_DataElementValue(6);

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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                }

                            }//end loop section HL

                            else if (sLoopSection == "HL;NM1")
                            {
                                //Payer/Information Source Name
                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    //txtInsuranceName.Text = oSegment.get_DataElementValue(3);
                                    ogloEligibilityResponse.PayerName = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.PayerID = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PayerContactName = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    ogloEligibilityResponse.PayerContactNumber = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);

                                }
                                else if (sSegmentID == "AAA")
                                {
                                    if (oSegment.get_DataElementValue(1) == "N")
                                    {
                                        sValue = oSegment.get_DataElementValue(2);
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            //listResponse.Visible = true;
                                            c1Response.Visible = false;
                                            panel1.Visible = false;
                                            splitter1.Visible = false;
                                            //listResponse.Dock = DockStyle.Fill;
                                            if (GetSourceRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                            {

                                                rtfError.AppendText(Environment.NewLine);

                                                rtfError.AppendText("       Payer Rejection Reason: ");
                                                rtfError.AppendText(GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                                //listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                                            }
                                            if (GetSourceFollowUp(oSegment.get_DataElementValue(4)) != "")
                                            {

                                                rtfError.AppendText(Environment.NewLine);

                                                rtfError.AppendText("       Payer Follow up: ");
                                                rtfError.AppendText(GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                                //listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                                            }
                                        }

                                        EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                        RejectionDetails = "Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3));
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
                                //Receiver/Provider Name
                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    else
                                    {
                                        ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.ReceiverID = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.ReceiverAdditionalID = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetReceiverRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Receiver Rejection Reason:");
                                            rtfError.AppendText(GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                            //listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetReceiverFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Receiver Follow up: ");
                                            rtfError.AppendText(GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                            //listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                    RejectionDetails = "Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
                                    //}
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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    else
                                    {
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    ogloEligibilityResponse.SubscriberID = sValue;
                                }
                                else if (sSegmentID == "N3")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);

                                }
                                else if (sSegmentID == "N4")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            //rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Subscriber Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            //listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);
                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Subscriber Follow up: ");
                                            //rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                                            if (Convert.ToString(oSegment.get_DataElementValue(4)).Trim().ToUpper() == "C")
                                            {
                                                rtfError.AppendText("Please Correct and Resubmit");
                                            }
                                            //listResponse.Items.Add("" + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                    RejectionDetails = "Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                    //}
                                }
                                else if (sSegmentID == "DMG")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                    }
                                    string _strGender = "";
                                    if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "M")
                                    {
                                        _strGender = "Male";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "F")
                                    {
                                        _strGender = "Female";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "U")
                                    {
                                        _strGender = "Unknown";
                                    }

                                    ogloEligibilityResponse.SubscriberGender = _strGender;

                                }
                                else if (sSegmentID == "INS")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(9);
                                    sValue = oSegment.get_DataElementValue(10);
                                }
                                else if (sSegmentID == "DTP")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (oSegment.get_DataElementValue(1) == "307")
                                    {
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            string _date = "";
                                            string[] _dateRange = null;
                                            _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                            _dateRange = _date.Split('-');


                                            if (_dateRange != null && _dateRange.Length > 0)
                                            {
                                                //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                                ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                            }
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        string _date = "";
                                        string[] _dateRange = null;
                                        _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                        _dateRange = _date.Split('-');

                                        if (_dateRange != null && _dateRange.Length > 0)
                                        {
                                            //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                            ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                        }
                                        //txtServiceDate.Text = oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "HSD")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);

                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                //string strCoverageLevel = "";

                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }

                                if (sSegmentID == "EB")
                                {

                                    ogloEligibility = new gloEligibility();
                                    c1Response.Rows.Add();
                                    _rowIndex = c1Response.Rows.Count - 1;
                                    c1Response.SetData(_rowIndex, COL_BENEFIT, GetBenefitDescription(Qlfr));//Benefit Information
                                    //Benefit Information
                                    ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                    if (ogloEligibility.BenefitInformation != null)
                                    {
                                        //ogloEligibilities = new gloEligibilities();
                                        ogloEligibilities.Add(ogloEligibility);

                                    }
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, GetCoverageDescription(oSegment.get_DataElementValue(2)));//Coverage Level code
                                        ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                    }
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_SERVICETYPE, GetServiceTypeDescription(oSegment.get_DataElementValue(3)));//Service Type
                                        ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                    }
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    if (oSegment.get_DataElementValue(6).Trim() != "")
                                    {
                                        ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                    }
                                    c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, oSegment.get_DataElementValue(7));
                                    if (oSegment.get_DataElementValue(7).Trim() != "")
                                    {
                                        ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                    }
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                    {
                                        ogloEligibility.IsPlanNetwork = true;
                                    }
                                    else
                                    {
                                        ogloEligibility.IsPlanNetwork = false;
                                    }


                                }

                                if (sSegmentID == "MSG")
                                {
                                    c1Response.SetData(_rowIndex, COL_MESSAGE, oSegment.get_DataElementValue(1));
                                    ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            //listResponse.Items.Add(" " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                            //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                        RejectionDetails = "Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                    }
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }
                            else if (sLoopSection == "HL;NM1;EB;NM1")
                            {
                                if (sSegmentID == "NM1")
                                {
                                    if (oSegment.get_DataElementValue(1) == "P3")
                                    {
                                        isPCPSegment = true;

                                        sValue = Convert.ToString(oSegment.get_DataElementValue(4)) + " " +
                                            Convert.ToString(oSegment.get_DataElementValue(3)) + " " +
                                            Convert.ToString(oSegment.get_DataElementValue(5));
                                        ogloEligibilityResponse.PrimaryCarePhysicainName = sValue;

                                        sValue = "";
                                        sValue = oSegment.get_DataElementValue(9);

                                    }
                                    else
                                    { isPCPSegment = false; }
                                }
                                else if (sSegmentID == "N3" && isPCPSegment == true)
                                {
                                    ogloEligibilityResponse.PrimaryCareAddress = Convert.ToString(oSegment.get_DataElementValue(1)) + Environment.NewLine + Convert.ToString(oSegment.get_DataElementValue(2));
                                }
                                else if (sSegmentID == "N4" && isPCPSegment == true)
                                {
                                    ogloEligibilityResponse.PrimaryCareCity = Convert.ToString(oSegment.get_DataElementValue(1));
                                    ogloEligibilityResponse.PrimaryCareState = Convert.ToString(oSegment.get_DataElementValue(2));
                                    ogloEligibilityResponse.PrimaryCareZip = Convert.ToString(oSegment.get_DataElementValue(3));
                                }
                                else if (sSegmentID == "PER" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PrimaryCarePhysicainContactName = Convert.ToString(sValue);
                                    sValue = oSegment.get_DataElementValue(4);
                                    ogloEligibilityResponse.PrimaryCarePhysicainContactNumber = Convert.ToString(sValue);
                                    sValue = "";
                                }
                            }
                            else if (sLoopSection == "HL;NM1;EB;LS;NM1")
                            {


                                if (sSegmentID == "NM1")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (sValue == "P3") //Primary Care Provider
                                    {
                                        isPCPSegment = true;
                                        sValue = oSegment.get_DataElementValue(2);
                                        sValue = oSegment.get_DataElementValue(3);
                                        sValue = oSegment.get_DataElementValue(4);
                                        sValue = oSegment.get_DataElementValue(5);


                                        if (oSegment.get_DataElementValue(5).Trim() != "")
                                        {
                                            ogloEligibilityResponse.PrimaryCarePhysicainName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                        }
                                        else
                                        {
                                            ogloEligibilityResponse.PrimaryCarePhysicainName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                        }

                                        sValue = oSegment.get_DataElementValue(6);
                                        sValue = oSegment.get_DataElementValue(7);
                                        sValue = oSegment.get_DataElementValue(8);
                                        sValue = oSegment.get_DataElementValue(9);
                                        //ogloEligibilityResponse.primarycarep = sValue;
                                    }
                                    else { isPCPSegment = false; }

                                }
                                else if (sSegmentID == "N3" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    ogloEligibilityResponse.PrimaryCareAddress = sValue;
                                    sValue = oSegment.get_DataElementValue(2);
                                }
                                else if (sSegmentID == "N4" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    ogloEligibilityResponse.PrimaryCareCity = sValue;

                                    sValue = oSegment.get_DataElementValue(2);
                                    ogloEligibilityResponse.PrimaryCareState = sValue;

                                    sValue = oSegment.get_DataElementValue(3);
                                    ogloEligibilityResponse.PrimaryCareZip = sValue;
                                }
                                else if (sSegmentID == "PER" && isPCPSegment == true)
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }


                            }
                            //if (sLoopSection == "HL;NM1;DTP")
                            //{

                            //}


                        }
                        else if (sEntity == "23")
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
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    //txtPatientName.Text = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);

                                    if (oSegment.get_DataElementValue(5).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    else
                                    {

                                        ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "N3")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);

                                }
                                else if (sSegmentID == "N4")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);

                                }
                                else if (sSegmentID == "PER")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    //if (oSegment.get_DataElementValue(1) == "N")
                                    //{
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {
                                            //rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Subscriber Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                            //listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);
                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Subscriber Follow up: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                                            //listResponse.Items.Add("" + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                    }

                                    EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();
                                    RejectionDetails = "Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                    //}
                                }
                                else if (sSegmentID == "DMG")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                                    }
                                    string _strGender = "";
                                    if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "M")
                                    {
                                        _strGender = "Male";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "F")
                                    {
                                        _strGender = "Female";
                                    }
                                    else if (oSegment.get_DataElementValue(3).Trim().ToUpper() == "U")
                                    {
                                        _strGender = "Unknown";
                                    }

                                    ogloEligibilityResponse.SubscriberGender = _strGender;

                                }
                                else if (sSegmentID == "INS")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(9);
                                    sValue = oSegment.get_DataElementValue(10);
                                }
                                else if (sSegmentID == "DTP")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    if (oSegment.get_DataElementValue(1) == "307")
                                    {
                                        if (oSegment.get_DataElementValue(3).Trim() != "")
                                        {
                                            string _date = "";
                                            string[] _dateRange = null;
                                            _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                            _dateRange = _date.Split('-');


                                            if (_dateRange != null && _dateRange.Length > 0)
                                            {
                                                //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                                ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                            }
                                        }
                                    }
                                    else if (oSegment.get_DataElementValue(1) == "472")
                                    {
                                        string _date = "";
                                        string[] _dateRange = null;
                                        _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                        _dateRange = _date.Split('-');

                                        if (_dateRange != null && _dateRange.Length > 0)
                                        {
                                            //txtEligibilityDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3))).ToShortDateString();
                                            ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                        }
                                        //txtServiceDate.Text = oSegment.get_DataElementValue(3);
                                    }
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }
                                else if (sSegmentID == "HSD")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    sValue = oSegment.get_DataElementValue(7);
                                    sValue = oSegment.get_DataElementValue(8);

                                }
                            }

                            else if (sLoopSection == "HL;NM1;EB")
                            {
                                //string strCoverageLevel = "";

                                if (sSegmentID == "EB")
                                {
                                    Qlfr = oSegment.get_DataElementValue(1);
                                }
                                if (sSegmentID == "EB")
                                {

                                    ogloEligibility = new gloEligibility();
                                    c1Response.Rows.Add();
                                    _rowIndex = c1Response.Rows.Count - 1;
                                    c1Response.SetData(_rowIndex, COL_BENEFIT, GetBenefitDescription(Qlfr));//Benefit Information
                                    //Benefit Information
                                    ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                    if (ogloEligibility.BenefitInformation != null)
                                    {
                                        //ogloEligibilities = new gloEligibilities();
                                        ogloEligibilities.Add(ogloEligibility);

                                    }
                                    if (oSegment.get_DataElementValue(2).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, GetCoverageDescription(oSegment.get_DataElementValue(2)));//Coverage Level code
                                        ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                    }
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        c1Response.SetData(_rowIndex, COL_SERVICETYPE, GetServiceTypeDescription(oSegment.get_DataElementValue(3)));//Service Type
                                        ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                    }
                                    sValue = oSegment.get_DataElementValue(4);
                                    sValue = oSegment.get_DataElementValue(5);
                                    sValue = oSegment.get_DataElementValue(6);
                                    if (oSegment.get_DataElementValue(6).Trim() != "")
                                    {
                                        ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                    }
                                    c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, oSegment.get_DataElementValue(7));
                                    if (oSegment.get_DataElementValue(7).Trim() != "")
                                    {
                                        ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                    }
                                    sValue = oSegment.get_DataElementValue(8);
                                    sValue = oSegment.get_DataElementValue(9);
                                    if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                    {
                                        ogloEligibility.IsPlanNetwork = true;
                                    }
                                    else
                                    {
                                        ogloEligibility.IsPlanNetwork = false;
                                    }


                                }

                                if (sSegmentID == "MSG")
                                {
                                    c1Response.SetData(_rowIndex, COL_MESSAGE, oSegment.get_DataElementValue(1));
                                    ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                }
                                else if (sSegmentID == "AAA")
                                {
                                    sValue = oSegment.get_DataElementValue(2);
                                    if (oSegment.get_DataElementValue(3).Trim() != "")
                                    {
                                        //listResponse.Visible = true;
                                        c1Response.Visible = false;
                                        panel1.Visible = false;
                                        splitter1.Visible = false;
                                        //listResponse.Dock = DockStyle.Fill;
                                        if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                        {

                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));

                                            //listResponse.Items.Add(" " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                                        }
                                        if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                        {
                                            rtfError.AppendText(Environment.NewLine);

                                            rtfError.AppendText("       Eligibility Rejection Reason: ");
                                            rtfError.AppendText(GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                            //listResponse.Items.Add("Eligibility Follow up: " + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                        }
                                        RejectionDetails = "Eligibility Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                    }
                                }
                                else if (sSegmentID == "REF")
                                {
                                    sValue = oSegment.get_DataElementValue(1);
                                    sValue = oSegment.get_DataElementValue(2);
                                    sValue = oSegment.get_DataElementValue(3);
                                }

                            }
                            else if (sLoopSection == "HL;NM1;EB;III")
                            {
                                if (sSegmentID == "III")
                                {

                                }
                            }
                            //if (sLoopSection == "HL;NM1;DTP")
                            //{

                            //}
                        }
                        else
                        {
                            //if (sLoopSection == "HL;NM1;EB;III")
                            //{

                            //}

                        }
                    }
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());

                }
                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.

                // Gets the first segment of the 997 acknowledgment file
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                //while (oSegment != null)
                //{
                //    nArea = oSegment.Area;
                //    sLoopSection = oSegment.LoopSection;
                //    sSegmentID = oSegment.ID;

                //    if (nArea == 1)
                //    {
                //        if (sLoopSection == "")
                //        {
                //            if (sSegmentID == "AK9")
                //            {
                //                if (oSegment.get_DataElementValue(1, 0) == "R")
                //                {
                //                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                //                }
                //            }
                //        }	// sLoopSection == ""
                //    }	//nArea == 1
                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                //}	//oSegment != null

                #region //Show Errors

                if (rtfError.Text != "")
                {
                    ogloEligibilityResponse.ErrorNote = rtfError.Text.ToString().Replace('\n', ' ');
                }



                #endregion
            }
            catch (Exception ex)
            {
                RejectionDetails = ex.ToString();
                if (!_IsEligibilityTest)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }
            finally
            {
                if (ogloEligibility != null)
                {
                    ogloEligibility.Dispose();
                }
                if (ogloEligibilityResponse != null)
                {
                    ogloEligibilityResponse.Dispose();
                }
                if (oEligibility != null)
                {
                    oEligibility.Dispose();
                }

                if (oSegment != null)
                {
                    oSegment.Dispose();
                }

                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }

                if (oEdiDoc1 != null)
                {
                    oEdiDoc1.Close();
                    oEdiDoc1.Dispose();
                }
            }
            return RejectionDetails;
        }
        private void Translate271Response(bool _IsService)
        {
            //gloEligibilityResponse ogloEligibilityResponse = new gloEligibilityResponse();
            //gloEligibility ogloEligibility = new gloEligibility();
            //gloEligibilities ogloEligibilities = new gloEligibilities();
            //EligibilityResponse oEligibility = new EligibilityResponse(_databaseConnectionString);
            try
            {
                //ogloEligibilityResponse=ogloEligibilities.
                //if(ogloEligibilityResponse!=null )//&& Convert.ToString(ogloEl)
             //   string sValue = "";
                rtfeligibilityinfo.ForeColor = Color.FromArgb(31, 73, 125);

                textLengthBefore = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.AppendText("Patient Eligibility Response:");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 14.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);

                textLengthBefore = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.AppendText("Payer Information: ");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                rtfeligibilityinfo.SelectionColor = Color.White;
                rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);

                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText("       ");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                rtfeligibilityinfo.AppendText("Name: ");
                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                //rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(3));
                rtfeligibilityinfo.AppendText(Convert.ToString(ogloEligibilityResponse.PayerName));
                //ogloEligibilityResponse.PayerName = oSegment.get_DataElementValue(3);

                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText("       ");

                textLengthBefore = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.AppendText("Payer Identification: ");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                textLengthBefore = rtfeligibilityinfo.TextLength;
                //rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                rtfeligibilityinfo.AppendText(Convert.ToString(ogloEligibilityResponse.PayerID));
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);
                //rtfeligibilityinfo.ForeColor = System.Drawing.Color.Blue;
                rtfeligibilityinfo.AppendText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                if (Convert.ToString(ogloEligibilityResponse.PayerRejectionReason) != "")
                {
                    //listResponse.Visible = true;
                    c1Response.Visible = false;
                    //listResponse.Dock = DockStyle.Fill;
                    if (Convert.ToString(ogloEligibilityResponse.PayerRejectionReason) != "")
                    {

                        rtfError.AppendText(Environment.NewLine);

                        textLengthBefore = rtfeligibilityinfo.TextLength;
                        rtfError.AppendText("       Payer Rejection Reason: ");
                        //rtfError.AppendText(GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                        rtfError.AppendText(Convert.ToString(ogloEligibilityResponse.PayerRejectionReason));
                        textLengthAfter = rtfeligibilityinfo.TextLength;
                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                        //listResponse.Items.Add("Payer Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3)));
                    }
                    if (Convert.ToString(ogloEligibilityResponse.FollowUp) != "")
                    {

                        rtfError.AppendText(Environment.NewLine);

                        textLengthBefore = rtfeligibilityinfo.TextLength;
                        rtfError.AppendText("       Payer Follow up: ");
                        //rtfError.AppendText(GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                        rtfError.AppendText(Convert.ToString(ogloEligibilityResponse.FollowUp));
                        textLengthAfter = rtfeligibilityinfo.TextLength;
                        rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                        rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                        //listResponse.Items.Add("Payer Follow up: " + GetSourceFollowUp(oSegment.get_DataElementValue(4)));
                    }
                }


                textLengthBefore = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.AppendText("Insured/Subscriber Information: ");
                textLengthAfter = rtfeligibilityinfo.TextLength;
                rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                rtfeligibilityinfo.SelectionFont = new System.Drawing.Font("Tahoma", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);
                rtfeligibilityinfo.SelectionAlignment = HorizontalAlignment.Left;
                rtfeligibilityinfo.SelectionColor = Color.White;
                rtfeligibilityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204);

                rtfeligibilityinfo.AppendText(Environment.NewLine);
                rtfeligibilityinfo.AppendText(Environment.NewLine);

                if (Convert.ToString(ogloEligibilityResponse.SubscriberName).Trim() != "")
                {
                    //rtfeligibilityinfo.Font = System.Drawing.FontStyle.Regular;
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText("     Name: ");
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    //rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                    rtfeligibilityinfo.AppendText(Convert.ToString(ogloEligibilityResponse.SubscriberName).Trim());
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                }
                if (Convert.ToString(ogloEligibilityResponse.SubscriberID).Trim() != "")
                {
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText("     Identification Number: ");
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    //rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    //rtfeligibilityinfo.AppendText(oSegment.get_DataElementValue(9));
                    rtfeligibilityinfo.AppendText(Convert.ToString(ogloEligibilityResponse.SubscriberID).Trim());
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//n
                }

                if (Convert.ToString(ogloEligibilityResponse.SubscriberDOB).Trim() != "")
                {
                    //ogloEligibilityResponse.SubscriberDOB = Convert.ToInt64(oSegment.get_DataElementValue(2));
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText("     Date of Birth: ");
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    //rtfeligibilityinfo.AppendText(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString());
                    //rtfeligibilityinfo.AppendText(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(ogloEligibilityResponse.SubscriberDOB)).ToShortDateString());
                    if (Convert.ToInt64(ogloEligibilityResponse.SubscriberDOB) != 0)
                    {
                        rtfeligibilityinfo.AppendText(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(ogloEligibilityResponse.SubscriberDOB)).ToShortDateString());
                    }
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                }

                if (Convert.ToString(ogloEligibilityResponse.SubscriberRejectionReason).Trim() != "")
                {
                    //rtfError.AppendText(Environment.NewLine);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfError.AppendText("       Subscriber Rejection Reason: ");
                    //rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                    rtfError.AppendText(Convert.ToString(ogloEligibilityResponse.SubscriberRejectionReason).Trim());
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    //listResponse.Items.Add("Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)));
                }
                //if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                if (Convert.ToString(ogloEligibilityResponse.SubscriberFollowUp).Trim() != "")
                {
                    rtfError.AppendText(Environment.NewLine);
                    rtfError.AppendText(Environment.NewLine);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfError.AppendText("       Subscriber Follow up: ");
                    //rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                    //if (Convert.ToString(oSegment.get_DataElementValue(4)).Trim().ToUpper() == "C")
                    if (Convert.ToString(Convert.ToString(ogloEligibilityResponse.SubscriberFollowUp).Trim()).Trim().ToUpper() == "C")
                    {
                        rtfError.AppendText("Please Correct and Resubmit");
                    }
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);
                    //listResponse.Items.Add("" + GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                }
                //listResponse.Dock = DockStyle.Fill;
                //if (GetReceiverRejectionReason(oSegment.get_DataElementValue(3)) != "")
                if (Convert.ToString(ogloEligibilityResponse.ReceiverRejectionReason) != "")
                {
                    c1Response.Visible = false;
                    rtfError.AppendText(Environment.NewLine);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfError.AppendText("       Receiver Rejection Reason:");
                    rtfError.AppendText(Convert.ToString(ogloEligibilityResponse.ReceiverRejectionReason));
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    //listResponse.Items.Add("Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3)));
                }
                if (Convert.ToString(ogloEligibilityResponse.ReceiverFollowUp) != "")
                {

                    rtfError.AppendText(Environment.NewLine);

                    textLengthBefore = rtfeligibilityinfo.TextLength;
                    rtfError.AppendText("       Receiver Follow up: ");
                    rtfError.AppendText(Convert.ToString(ogloEligibilityResponse.ReceiverFollowUp));
                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)0);

                    //listResponse.Items.Add("Receiver Follow up: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4)));
                }

                if (ogloEligibilityResponse.EligibilityDate != 0)
                {
                    rtfeligibilityinfo.AppendText(Environment.NewLine);
                    rtfeligibilityinfo.AppendText(Environment.NewLine);

                    textLengthAfter = rtfeligibilityinfo.TextLength;
                    rtfeligibilityinfo.AppendText("     EligibilityDate: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(ogloEligibilityResponse.EligibilityDate)).ToShortDateString());

                    rtfeligibilityinfo.Select(textLengthBefore, textLengthAfter);
                    rtfeligibilityinfo.SelectionFont = gloGlobal.clsgloFont.gFont_BOLD;
                }

                //ogloEligibility = new gloEligibility();
                DataTable _dtBenefit = null;
                _dtBenefit = GetBenefitData();
                int _rowIndex = 0;
                if (_dtBenefit != null && _dtBenefit.Rows.Count > 0)
                {
                    c1Response.Visible = true;
                    for (int _Count = 0; _Count < _dtBenefit.Rows.Count; _Count++)
                    {

                        c1Response.Rows.Add();
                        _rowIndex = c1Response.Rows.Count - 1;
                        //ogloEligibility = null;//Check
                        c1Response.SetData(_rowIndex, COL_BENEFIT, Convert.ToString(_dtBenefit.Rows[_Count]["sBenefitInformation"]).Trim());//Benefit Information
                        //Benefit Information
                        if (_dtBenefit != null)
                        {
                            //ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                            //if (ogloEligibility.CoverageLevel != null && Convert.ToString(ogloEligibility.CoverageLevel).Trim() != "")
                            if (Convert.ToString(_dtBenefit.Rows[_Count]["sCoverageLevel"]).Trim() != "")
                            {
                                c1Response.SetData(_rowIndex, COL_COVERAGE_LEVEL, Convert.ToString(_dtBenefit.Rows[_Count]["sCoverageLevel"]).Trim());//Coverage Level code
                                //ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                            }
                            //if (ogloEligibility.CoverageLevel != null && Convert.ToString(ogloEligibility.ServiceType).Trim() != "")
                            if (Convert.ToString(_dtBenefit.Rows[_Count]["sServiceType"]).Trim() != "")
                            {
                                c1Response.SetData(_rowIndex, COL_SERVICETYPE, Convert.ToString(_dtBenefit.Rows[_Count]["sServiceType"]).Trim());//Service Type
                                //ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                            }
                            //if (ogloEligibility.EligibilityAmount != null && Convert.ToString(ogloEligibility.EligibilityAmount).Trim() != "")
                            if (Convert.ToString(_dtBenefit.Rows[_Count]["nEligibilityAmount"]).Trim() != "")
                            {
                                c1Response.SetData(_rowIndex, COL_BENEFITAMOUNT, Convert.ToString(_dtBenefit.Rows[_Count]["nEligibilityAmount"]).Trim());
                            }
                            //ogloEligibility = null;

                            //if (ogloEligibility.Message != null && Convert.ToString(ogloEligibility.Message) != "")
                            if (Convert.ToString(_dtBenefit.Rows[_Count]["sMessage"]).Trim() != "")
                            {
                                c1Response.SetData(_rowIndex, COL_MESSAGE, Convert.ToString(_dtBenefit.Rows[_Count]["sMessage"]).Trim());
                                //ogloEligibility.Message = oSegment.get_DataElementValue(1);
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (ogloEligibility != null)
                //{
                //    ogloEligibility.Dispose();
                //}
                //if (ogloEligibilityResponse != null)
                //{
                //    ogloEligibilityResponse.Dispose();
                //}
                if (ogloEligibilities != null)
                {
                    //ogloEligibilities.Dispose();
                }

            }
        }

        //public Int64 AddNote(Int64 PatientID, string Notes)
        //{
        //    Int64 _result = 0;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    oDB.Connect(false);

        //    try
        //    {

        //        oDBParameters.Clear();
        //        object _intresult = 0;
        //        oDBParameters.Add("@nPNotesID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nTime", gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@sNotes", Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //        oDBParameters.Add("@nNotesType", 1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);//Enum will be taken later 0=None, 1=EDI
        //        oDBParameters.Add("@nClinicID", _ClinicId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        int result = oDB.Execute("SP_INUP_Patient_Notes", oDBParameters, out  _intresult);


        //        if (_intresult != null)
        //        {
        //            if (_intresult.ToString().Trim() != "")
        //            {
        //                if (Convert.ToInt64(_intresult) > 0)
        //                {
        //                    _result = Convert.ToInt64(_intresult);
        //                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Add, "Add notes from Eligibility Response", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
        //                }
        //            }
        //        }
        //    }

        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        DBErr.ERROR_Log(DBErr.ToString());
        //        System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _messageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        private DataTable GetBenefitData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string strSQL = "";
            DataTable _result = null;
            try
            {
                strSQL = "Select nPatientID, sReferenceID, Isnull(sBenefitInformation,'') as sBenefitInformation,Isnull( sCoverageLevel,'') as sCoverageLevel, " +
                         " Isnull(sServiceType,'') as sServiceType, Isnull(sTimePeriod,'') as sTimePeriod, Isnull(nEligibilityAmount,0) as nEligibilityAmount, bIsPlanNetwork, Isnull(sMessage,'') as sMessage, nClinicID, nEligibilityResponseID " +
                         " from  BL_EligibilityResponse_DTL where nEligibilityResponseID=" + _EDIID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out _result);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
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
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }


        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnCheckResponse_Click(object sender, EventArgs e)
        {
            //Translate271Response();
        }

        private void frmEligibilityResponse_Load(object sender, EventArgs e)
        {
            try
            {
                if (_IsService == false)
                {
                    LoadLastSaveSettings();
                    LoadEDIObject();
                    DesignGrid();
                    Translate271Response();
                }
                else
                {
                    LoadLastSaveSettings();
                    DesignGrid();
                    Translate271Response(_IsService);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }


        private string GetBenefitDescription(string BenefitCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string _strSQL = "";
            object _result = null;
            string _strServiceType = "";
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT sBenefitDescription FROM BL_EligibilityBenefitInformation WHERE sBenefitCode = '" + BenefitCode + "'";
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _strServiceType = Convert.ToString(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _strServiceType;
        }

        private string GetServiceTypeDescription(string ServiceTypeCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string _strSQL = "";
            object _result = null;
            string _strServiceType = "";
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT sServiceTypeDesc FROM BL_InsuranceServiceType WHERE sServiceTypeCode = '" + ServiceTypeCode + "'";
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _strServiceType = Convert.ToString(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _strServiceType;
        }

        private string GetCoverageDescription(string CoverageCode)
        {
            string strCoverageDescription = "";
            try
            {
                if (CoverageCode == "CHD")//CHD Children Only
                {
                    strCoverageDescription = "Children Only";
                }
                else if (CoverageCode == "DEP")// DEP Dependents Only
                {
                    strCoverageDescription = "Dependents Only";
                }
                else if (CoverageCode == "ECH")//ECH Employee and Children
                {
                    strCoverageDescription = "Employee and Children";
                }
                else if (CoverageCode == "EMP")//EMP Employee Only
                {
                    strCoverageDescription = "Employee Only";
                }
                else if (CoverageCode == "ESP")//ESP Employee and Spouse
                {
                    strCoverageDescription = "Employee and Spouse";
                }
                else if (CoverageCode == "FAM")//FAM Family
                {
                    strCoverageDescription = "Family";
                }
                else if (CoverageCode == "IND")//IND Individual
                {
                    strCoverageDescription = "Individual";
                }
                else if (CoverageCode == "SPC")//SPC Spouse and Children
                {
                    strCoverageDescription = "Spouse and Children";
                }
                else if (CoverageCode == "SPO")//SPO Spouse Only
                {
                    strCoverageDescription = "Spouse Only";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return strCoverageDescription;
        }

        private string GetEligibilityDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dtEligibility = new DataTable();
            try
            {
                oDB.Connect(false);
                string query = "Select max(dtEligibilityCheck) as dtEligibilityCheck from BL_EligibilityResponse_MST where nPatientID='" + _PatientId + "'";
                oDB.Retrive_Query(query, out dtEligibility);
                if (dtEligibility.Rows.Count > 0 && dtEligibility.Rows[0]["dtEligibilityCheck"].ToString() != "")
                {
                    return "Eligibility Check was done On '" + Convert.ToDateTime(dtEligibility.Rows[0]["dtEligibilityCheck"]).Date.ToShortDateString() + "' at  '" + Convert.ToDateTime(dtEligibility.Rows[0]["dtEligibilityCheck"]).TimeOfDay.ToString().Remove(8) + "'";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        private void c1Response_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void frmEligibilityResponse_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSetting = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                oSetting.WriteSettings_XML("EligibilityResponse4010", "EligibilityResponse4010ScreenHeight", this.Height.ToString());
                oSetting.WriteSettings_XML("EligibilityResponse4010", "EligibilityResponse4010ScreenWidth", this.Width.ToString());
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        public void LoadLastSaveSettings()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            string _sValue;
            try
            {
                _sValue = Convert.ToString(oSettings.ReadSettings_XML("EligibilityResponse4010", "EligibilityResponse4010ScreenHeight"));
                if (_sValue.Trim() != "")
                {
                    _LastHeight = Convert.ToInt16(_sValue);
                }

                _sValue = Convert.ToString(oSettings.ReadSettings_XML("EligibilityResponse4010", "EligibilityResponse4010ScreenWidth"));
                if (_sValue.Trim() != "")
                {
                    _LastWidth = Convert.ToInt16(_sValue);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            panel2.Refresh();
            panel1.Refresh();
        }


    }
}