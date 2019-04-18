using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.InteropServices;
using gloCCDLibrary;
using gloCCDSchema;
using System.IO;
using System.Windows.Forms;

namespace gloEmdeonInterface.Classes
{
    public class clsGeneral
    {
        // by Abhijeet on 20100429,declared database connection string,appsetting variable 
        private string _dataBaseConnectionString = "";

        private string _TestList = string.Empty;
        public string TestlistOnly = "";
        public string TestList
        {
            get { return _TestList; }
            set { _TestList = value; }
        }

        //Commented by madan on 20100520
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #region "Enum definitions"

         public enum IntuitPatientProcessedAction
        {
            None=0,
            MatchedPatient=1,
            NewPatient=2,
            RejectdPatient=3,
            OverWritten=4
        }

        public enum InternetConnectivity
        {
            None = 0,
            Success = 1,
            NoInternet = 2,
            ServerNotresponding = 3
        }

        public enum OrderBillingType   // added by abhijeet on date 20100331
        { //Used For defining billing type           
            Client = 0,   //C Client 
            Patient = 1,  //P Patient
            ThirdParty = 2//T Third party
        }

        public enum OrderStatus  // added by abhijeet on date 20100331
        { // Used For defining Order Status                          
            CancelledbyClient = 13, //XC Cancelled by Client change from 0 to 13 
            CancelledbyLab = 1, //XL Cancelled by Lab
            Corrected = 2, //C Corrected
            Draft = 3,   //D Draft
            Entered = 4,  //E Entered
            Error = 5,   //X Error
            FinalReported = 6, //F Final Reported
            Inactive = 7,  //I Inactive
            PartialReported = 8, //P Partial Reported
            ReadyToTransmit = 9, //R Ready to Transmit
            ResultsReceived = 10, //NA Results Received
            TransmissionError = 11, //TX Transmission Error
            Transmitted = 12 //T Transmitted
        }

        //Added by madan on 20100618
        //Added for locking records.
        public enum TrnType
        {
            None = 0,
            PatientRegistration = 1,
            PatientROS = 2,
            PatientHistory = 3,
            Medication = 4,
            Prescription = 5,
            PatientVitals = 6,
            Radiology = 7,
            Labs = 8,
            Messages = 9,
            Letters = 10,
            PTProtocol = 11,
            PatientConsent = 12,
            Flowsheet = 13,
            Task = 14,
            Immunization = 15,
            ReferralLetters = 16,
            DMS = 17,
            ProblemList = 18,
            DisclosureManagement = 19,
            NurseNotes = 20,
            Triage = 21
        }
        #endregion  "Enum definitions"

        //Madan Added this comment not needed .. 20100505
        #region "Constructor & Destructor"
        public clsGeneral()
        {
            // Code by : Abhijeet Farkande on date : 20100429
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }
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

        ~clsGeneral()
        {
            Dispose(false);
        }
        #endregion "Constructor & Destructor"
        //Audit trail--Added by Madan on 20100402
        public void UpdateLog(string strLogMessage)
        {
            try
            {
                string strApplPath = System.IO.Directory.GetCurrentDirectory();
                // string strApplPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(strApplPath + "\\EmdeonInterfaceLog.Log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + strLogMessage);
                objFile.Close();
                objFile.Dispose();
                objFile = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //added by madan on 20100702
        #region InternetConnectivity

        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public InternetConnectivity IsInternetConnectionAvailable()
        {
            #region Removed
            //Boolean isAvailable = false;
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://cli-cert.emdeon.com/");
            //    request.Timeout = 5000;
            //    request.Credentials = CredentialCache.DefaultNetworkCredentials;
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        Console.WriteLine("IsSIPServerAvailable: " + response.StatusCode);
            //        isAvailable = true;
            //    }
            //    response.Close();
            //    request.Abort();
            //    return isAvailable;
            //}
            //catch (Exception ex)
            //{
            //    //UpdateLog(ex.ToString());
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    return false;
            //}

            #endregion

            int Desc;
            if (InternetGetConnectedState(out Desc, 0))
            {
                if (CheckConnectionToServerAvailabllity(clsEmdeonGeneral.emdeonURL.ToString().Trim()))
                {
                    return InternetConnectivity.Success;
                }
                else
                {
                    return InternetConnectivity.ServerNotresponding;
                }
            }
            else
            {
                return InternetConnectivity.NoInternet;
            }
        }

        public InternetConnectivity IsInternetConnectionAvailable(string URLValue)
        {

            #region "Previous Reference"
            //Boolean isAvailable = false;
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://cli-cert.emdeon.com/");
            //    request.Timeout = 5000;
            //    request.Credentials = CredentialCache.DefaultNetworkCredentials;
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        Console.WriteLine("IsSIPServerAvailable: " + response.StatusCode);
            //        isAvailable = true;
            //    }
            //    response.Close();
            //    request.Abort();
            //    return isAvailable;
            //}
            //catch (Exception ex)
            //{
            //    //UpdateLog(ex.ToString());
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    return false;
            //}

            #endregion "Previous Reference"

            //Creating a function that uses the API function...
            int Desc;
            if (InternetGetConnectedState(out Desc, 0))
            {
                if (CheckConnectionToServerAvailabllity(clsEmdeonGeneral.emdeonURL.ToString().Trim()))
                {
                    return InternetConnectivity.Success;
                }
                else
                {
                    return InternetConnectivity.ServerNotresponding;
                }
            }
            else
            {
                return InternetConnectivity.NoInternet;
            }


        }

        private bool CheckConnectionToServerAvailabllity(string _strURL)
        {
            HttpWebRequest reqFP;
            HttpWebResponse rspFP;
            try
            {
                reqFP = (HttpWebRequest)HttpWebRequest.Create(_strURL);

                rspFP = (HttpWebResponse)reqFP.GetResponse();

                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Internet connection available, server online

                    rspFP.Close();
                    
                    return true;
                }
                else
                {
                    // Other status - Server or connection not available
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                // Exception - connection not available
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                // Exception - connection not available
                return false;
            }
            finally
            {
                reqFP = null;
                rspFP = null;
            }

        }

        #endregion
        //***added by madan on 20100702
        private string _FromDate = string.Empty;

        private string _ToDate = string.Empty;
        public string GenerateCDA(Int64 PatientID, Int64 _LoginUserId)
        {
            gloCCDLibrary.gloCDADataExtraction oCDADataExtraction = null;
            string strFilePath = String.Empty;

            try
            {

                _FromDate = null;
                _ToDate = null;

                if (gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath != "")
                {
                    string strCCDFilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath;
                    if (Directory.Exists(strCCDFilePath) == true)
                    {
                        

                        string strCCDDirectory = "";

                        if (strCCDFilePath == "")
                        {
                            strCCDDirectory = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath;
                        }
                        else
                        {
                            strCCDDirectory = System.IO.Path.GetDirectoryName(strCCDFilePath);
                        }

                        if (System.IO.File.Exists(strCCDDirectory + "/gloCCDAcss_MU2.xsl") == false)
                        {
                            System.IO.File.Copy(Application.StartupPath + "/gloCCDAcss_MU2.xsl", strCCDDirectory + @"\gloCCDAcss_MU2.xsl", true);
                        }



                        gloCDAWriterParameters objCDAWriterParameters = new gloCDAWriterParameters();
                        objCDAWriterParameters.StyleSheetPath = "gloCCDAcss_MU2.xsl";
                        objCDAWriterParameters.CDAFileType = CDAFileTypeEnum.AmbulatorySummary;
                        objCDAWriterParameters.Demographics = true;
                        objCDAWriterParameters.Problems = true;
                        objCDAWriterParameters.Allergies = true;
                        objCDAWriterParameters.CareTeamMember = true;
                        objCDAWriterParameters.Procedures = true;
                        objCDAWriterParameters.VitalSigns = true;
                        objCDAWriterParameters.LaboratoryResult = true;
                        objCDAWriterParameters.LaboratoryTest = true;
                        objCDAWriterParameters.Medications = true;
                        objCDAWriterParameters.FamilyHistory = true;
                        objCDAWriterParameters.SocialHistory = true;
                        objCDAWriterParameters.ClinicalInstructions = true;
                        objCDAWriterParameters.Implant = true;
                        objCDAWriterParameters.Goals = true;
                        objCDAWriterParameters.HealthConcern = true;
                        objCDAWriterParameters.TreatmentPlan = true;
                        objCDAWriterParameters.Assessments = true;
                        objCDAWriterParameters.Visit_DateAndLocation = true;

                        objCDAWriterParameters.ProviderName = true;


                        objCDAWriterParameters.OfficeContact = true;
                        objCDAWriterParameters.Immunizations = true;
                        objCDAWriterParameters.EncounterDiagnoses = true;
                        objCDAWriterParameters.CognitiveStatus = true;
                        objCDAWriterParameters.ReasonForReferral = true;

                        objCDAWriterParameters.ReferringProvider = true;


                        objCDAWriterParameters.FunctionalStatus = true;


                        oCDADataExtraction = new gloCCDLibrary.gloCDADataExtraction();

                        strFilePath = oCDADataExtraction.GenerateClinicalInformation(PatientID, _LoginUserId, objCDAWriterParameters, 0, _FromDate, _ToDate, "");

                        oCDADataExtraction.SaveExportedCDA(PatientID, strFilePath, "CDA sent through Orders", "", false, false, DateTime.Now.ToString(), "CDA", CDAFileTypeEnum.AmbulatorySummary.GetHashCode(), 0, true);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, "CDA file generated through Orders Acknowledgement.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                       // this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Invalid CCDA file path. Set a valid CCDA path from gloEMR Admin.", "gloEMR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("Please set valid CCDA file path from gloEMR Admin.", "gloEMR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message, "gloEMR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            finally
            {
                if (oCDADataExtraction != null)
                {
                    oCDADataExtraction.Dispose();
                    oCDADataExtraction = null;
                }



            }




            return strFilePath;
        }
        // Function added by Abhijeet on date 20100330 for Getting order status description
        public string GetOrderStatus(Int32 OrderStatusValue, int returnFormat)
        {
            string strOrderStatus = "";
            try
            {
                switch (OrderStatusValue)
                {
                    case 13:  //"XC"  //0 change to 13 for cancel status
                        {
                            strOrderStatus = returnFormat == 0 ? "Cancelled by Client" : "XC";
                            break;
                        }
                    case 1: //"XL"
                        {
                            strOrderStatus = returnFormat == 0 ? "Cancelled by Lab" : "XL";
                            break;
                        }
                    case 2: //"C"
                        {
                            strOrderStatus = returnFormat == 0 ? "Corrected" : "C";
                            break;
                        }
                    case 3: //"D"
                        {
                            strOrderStatus = returnFormat == 0 ? "Draft" : "D";
                            break;
                        }
                    case 4: //"E"
                        {
                            //strOrderStatus = returnFormat == 0 ? "Entered" : "E";                   
                            strOrderStatus = returnFormat == 0 ? "Not Sent" : "E";
                            break;
                        }
                    case 5:  //"X"
                        {
                            strOrderStatus = returnFormat == 0 ? "Error" : "X";
                            break;
                        }
                    case 6:  //"F"
                        {
                            strOrderStatus = returnFormat == 0 ? "Final Reported" : "F";
                            break;
                        }
                    case 7:  //"I"
                        {
                            strOrderStatus = returnFormat == 0 ? "Inactive" : "I";
                            break;
                        }
                    case 8: //"P"
                        {
                            strOrderStatus = returnFormat == 0 ? "Partial Reported" : "P";
                            break;
                        }
                    case 9:  //"R"
                        {
                            strOrderStatus = returnFormat == 0 ? "Ready to Transmit" : "R";
                            break;
                        }
                    case 10:  //"NA"
                        {
                            strOrderStatus = returnFormat == 0 ? "Results Received" : "NA";
                            break;
                        }
                    case 11:  //"TX"
                        {
                            strOrderStatus = returnFormat == 0 ? "Transmission Error" : "TX";
                            break;
                        }
                    case 12:  //"T"
                        {
                            // strOrderStatus =  returnFormat == 0 ?   "Transmitted" :   "T";                          
                            strOrderStatus = returnFormat == 0 ? "Sent to Lab" : "T";
                            break;
                        }
                    default:
                        {
                            strOrderStatus = "";
                            break;
                        }
                }

                return strOrderStatus;
            }
            catch (Exception ex)
            {
                //clsGeneral objclsGeneral = new clsGeneral();
                //objclsGeneral.UpdateLog("Error at Function for Get Order Status" +ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }
        }

        // Function added by Abhijeet on date 20100330 for Getting order billing type description
        public string GetBillingType(Int32 OrderBillingTypeValue, int returnFormat)
        {
            string strBillingType = "";
            try
            {
                switch (OrderBillingTypeValue)
                {
                    case 0:
                        {
                            strBillingType = returnFormat == 0 ? "Client" : "C";
                            break;
                        }
                    case 1:
                        {
                            strBillingType = returnFormat == 0 ? "Patient" : "P";
                            break;
                        }
                    case 2:
                        {
                            strBillingType = returnFormat == 0 ? "ThirdParty" : "T";
                            break;
                        }
                    default:
                        {
                            strBillingType = "";
                            break;
                        }
                }

                return strBillingType;
            }
            catch (Exception ex)
            {
                // clsGeneral objclsGeneral = new clsGeneral();
                // objclsGeneral.UpdateLog("Error at Function for Get Order Billing Type" + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }
        }

        // Function added by Abhijeet on date 20100331 for Getting OrderBillingType enum from code
        public OrderBillingType GetBillingTypeEnum(string BillingTypeCode)
        {
            OrderBillingType eBillingType = default(OrderBillingType);
            try
            {
                switch (BillingTypeCode)
                {
                    case "C":
                        {
                            eBillingType = OrderBillingType.Client;
                            break;
                        }
                    case "P":
                        {
                            eBillingType = OrderBillingType.Patient;
                            break;
                        }
                    case "T":
                        {
                            eBillingType = OrderBillingType.ThirdParty;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                return eBillingType;
            }
            catch (Exception ex)
            {
                //UpdateLog("Error at Function for Get Order Billing Type Enum" +  ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return eBillingType;
            }
        }

        // Function added by Abhijeet on date 20100331 for Getting orderstatus enum from code
        public OrderStatus GetOrderStatusEnum(string OrderStatusCode)
        {
            OrderStatus eOrderStatus = default(OrderStatus);
            try
            {
                switch (OrderStatusCode)
                {
                    case "XC":
                        {
                            eOrderStatus = OrderStatus.CancelledbyClient;
                            break;
                        }
                    case "XL":
                        {
                            eOrderStatus = OrderStatus.CancelledbyLab;
                            break;
                        }
                    case "C":
                        {
                            eOrderStatus = OrderStatus.Corrected;
                            break;
                        }
                    case "D":
                        {
                            eOrderStatus = OrderStatus.Draft;
                            break;
                        }
                    case "E":
                        {
                            eOrderStatus = OrderStatus.Entered;
                            break;
                        }
                    case "X":
                        {
                            eOrderStatus = OrderStatus.Error;
                            break;
                        }
                    case "F":
                        {
                            eOrderStatus = OrderStatus.FinalReported;
                            break;
                        }
                    case "I":
                        {
                            eOrderStatus = OrderStatus.Inactive;
                            break;
                        }
                    case "P":
                        {
                            eOrderStatus = OrderStatus.PartialReported;
                            break;
                        }
                    case "R":
                        {
                            eOrderStatus = OrderStatus.ReadyToTransmit;
                            break;
                        }
                    case "NA":
                        {
                            eOrderStatus = OrderStatus.ResultsReceived;
                            break;
                        }
                    case "TX":
                        {
                            eOrderStatus = OrderStatus.TransmissionError;
                            break;
                        }
                    case "T":
                        {
                            eOrderStatus = OrderStatus.Transmitted;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                return eOrderStatus;
            }
            catch (Exception ex)
            {
                //UpdateLog("Error at Function for Get Order Status Enum" + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return eOrderStatus;
            }
        }


        public void formatAge(DateTime BirthDate, out int Days, out int Months, out int Years)
        {   // function written by Abhijeet on  date 20100415 to find out Patient age 

            try
            {
                DateTime _BDate = BirthDate;

                bool IsBirthDateLeap = false;
                int years = DateTime.Now.Year - BirthDate.Year;
                int months = 0;
                int days = 0;
                //Test if BirthDay for LeapYear.
                if (BirthDate.Day == 29 & BirthDate.Month == 2)
                {
                    IsBirthDateLeap = true;
                }
                // Check if the last year was a full year. 
                if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
                {
                    years -= 1;
                }
                BirthDate = BirthDate.AddYears(years);
                // Now we know BirthDate <= end and the diff between them 
                // is < 1 year. 
                if (BirthDate.Year == DateTime.Now.Year)
                {
                    months = DateTime.Now.Month - BirthDate.Month;
                }
                else
                {
                    months = (12 - BirthDate.Month) + DateTime.Now.Month;
                }
                // Check if the last month was a full month. 
                if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
                {
                    months -= 1;
                }
                BirthDate = BirthDate.AddMonths(months);
                // Now we know that BirthDate < end and is within 1 month 
                // of each other. 
                days = (DateTime.Now - BirthDate).Days;

                //To Adjust Age if BirthDate is 29th Feb in leap year
                if (IsBirthDateLeap == true)
                {
                    //'Sequence of following IF code is too important.. DON'T MODIFY
                    days -= 1;
                    if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                    {
                        days += 1;
                    }
                    else if (DateTime.Now.Year % 4 == 0)
                    {
                        days += 1;
                    }
                    if (days < 0 & DateTime.Now.Year % 4 != 0)
                    {
                        days = 30;
                        months = months - 1;
                        if (months < 0)
                        {
                            months = 11;
                            years = years - 1;
                        }
                    }
                    if (months == 12)
                    {
                        days = 30;
                        months = 11;
                    }
                }

                Years = years;
                Months = months;
                Days = days;
            }
            catch (Exception ex)
            {
                Years = 0;
                Months = 0;
                Days = 0;
                //UpdateLog("Error at Function for getting patient age" + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        public Boolean IsgloLabRegisteredPatient(long patientID, out string Person, out string PersonHsi)
        { //function written by Abhijeet on 20100429,to check patient registration with Emdeon & written person,personhsi
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Boolean boolflag = false;
            try
            {
                DataTable dt = null;
                string strQry = "select a.nPatientId,b.sExternalSubType,b.sExternalValue from Patient a inner join PatientExternalCodes b on a.nPatientId=b.nPatientId  where  b.sExternalType = 'EMDEON' AND   a.nPatientId=" + patientID.ToString().Trim();
                oDB.Connect(false);
                oDB.Retrive_Query(strQry, out dt);
                oDB.Disconnect();

                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    string strPesron = string.Empty;
                    string strPersonhsi = string.Empty;

                    for (int rowCnt = 0; rowCnt < dt.Rows.Count; rowCnt++)
                    {
                        switch (Convert.ToString(dt.Rows[rowCnt]["sExternalSubType"]).ToUpper())
                        {
                            case "PERSON":
                                strPesron = Convert.ToString(dt.Rows[rowCnt]["sExternalValue"]);
                                break;
                            case "PERSONHSI":
                                strPersonhsi = Convert.ToString(dt.Rows[rowCnt]["sExternalValue"]);
                                break;
                        }
                    }
                    if (string.IsNullOrEmpty(strPesron) || string.IsNullOrEmpty(strPersonhsi))
                    {
                        Person = string.Empty;
                        PersonHsi = string.Empty;
                        boolflag = false;
                    }
                    else
                    {
                        Person = strPesron;
                        PersonHsi = strPersonhsi;
                        boolflag = true;
                    }
                }
                else
                {
                    Person = string.Empty;
                    PersonHsi = string.Empty;
                    boolflag = false;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                return boolflag;
            }
            catch (Exception ex)
            {
                Person = string.Empty;
                PersonHsi = string.Empty;
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("Error during setting Person & Pesronhsi values for patient: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
        }
        //Method added for retriving provider name according to provider id
        public string GetProviderName(Int64 _providerID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            object _intresult = null;
            string _result = "";
            string _strSQL = "";
            try
            {
                _result = "";
                oDB.Connect(false);

                _strSQL = "SELECT (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') +  space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                " FROM Provider_MST WHERE nProviderID  = " + _providerID + " AND nClinicID = " + ClinicID + "";

                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = _intresult.ToString();
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        /// <summary>
        /// Added by madan on 20100505
        /// </summary>
        /// <param name="nProviderID"></param>
        /// <returns></returns>
        public string GetProvidergloLabId(Int64 nProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strgloLabID = string.Empty;
            string strQry = string.Empty;
            object _objResult = null;
            try
            {
                oDB.Connect(false);
                strQry = "select sgloLabId from Provider_MST where nProviderID=" + nProviderID + "";
                _objResult = oDB.ExecuteScalar_Query(strQry);
                if (_objResult != null)
                {
                    _strgloLabID = Convert.ToString(_objResult);
                }
                else
                {
                    _strgloLabID = string.Empty;
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strgloLabID = string.Empty;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQry = string.Empty;
                _objResult = null;
            }
            return _strgloLabID;
        }
        /// Added by madan on 20100505
        /// </summary>
        /// <param name="nPatientProviderID"></param>
        /// <param name="nPatientID"></param>
        /// <returns></returns>
        public bool changePatientProvider(Int64 nPatientProviderID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQry = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                if (nPatientProviderID != 0)
                {
                    oDB.Connect(false);

                    strQry = "update Patient set nProviderID='" + nPatientProviderID + "' where nPatientID='" + nPatientID + "' ";
                    _Result = oDB.Execute_Query(strQry);
                    if (_Result < 0)
                        _boolResult = false;
                    else if (_Result >= 0)
                        _boolResult = true;
                    else
                        _boolResult = false;
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            return _boolResult;
        }

        #region  HL7 Message creation methods


        public bool InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, ref Int32 intTotalRecords, ArrayList arrTestName)
        {
            SqlConnection conn = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            bool boolIsSend = false;
            SqlParameter objParam = default(SqlParameter);
            try
            {
                conn = new SqlConnection(_dataBaseConnectionString);
                cmd = new SqlCommand("HL7_InsertMessageQueue", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                
                cmd.Parameters.Clear();
                objParam = cmd.Parameters.Add("@dtDatetimeStamp", SqlDbType.DateTime);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = DateTime.Now;

                objParam = null;

                objParam = cmd.Parameters.Add("@MessageName", SqlDbType.VarChar, 50);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = strMessageName;


                objParam = cmd.Parameters.Add("@sMachineID", SqlDbType.VarChar, 50);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 0; //*//gnClientMachineID.ToString;

                string strMachieName = System.Environment.MachineName;
                objParam = cmd.Parameters.Add("@sMachinename", SqlDbType.VarChar, 50);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = strMachieName; //*// gstrClientMachineName;


                objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = PatientID;


                objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = OtherID;


                objParam = cmd.Parameters.Add("@Status", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;


                objParam = cmd.Parameters.Add("@sField1", SqlDbType.VarChar, 5000);
                objParam.Direction = ParameterDirection.Input;
                string strTestName = "";
                if ((arrTestName != null))
                {
                    for (Int32 icnt = 0; icnt <= arrTestName.Count - 1; icnt++)
                    {
                        if (string.IsNullOrEmpty(strTestName))
                        {
                            strTestName = arrTestName[icnt].ToString();
                        }
                        else
                        {
                            strTestName = strTestName + "|" + arrTestName[icnt].ToString();
                        }
                    }
                }
                objParam.Value = strTestName;

                objParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = GetPrefixTransactionID(PatientID);


                cmd.Connection = conn;
                if (cmd.ExecuteNonQuery() > 0)
                    boolIsSend = true;
                else
                    boolIsSend = false;

                intTotalRecords = intTotalRecords + 1;
                return boolIsSend;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (objParam != null)
                {
                    objParam = null;
                }

                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        private Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";

                    oDB.Connect(false);
                    _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                    oDB.Disconnect();

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }

                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

                return _Result;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }
        #endregion  HL7 Message creation methods
        //added by madan on 20100614
        /// <summary>
        /// Added this code for generating lab task..
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="PatientProviderID"></param>
        /// <param name="LoginProviderID"></param>
        /// Modifed by madan on 20100726... by adding ExamReferanceId to method paramerters, to Show task details in exam module
        /// Modified by madan on 20100731-- by adding Task type to method parameters
        public long AssignTaskToUser(Int64 PatientID, Int64 PatientProviderID, Int64 LoginProviderID, Int64 ExamReferanceId,gloTaskMail.TaskType enumTaskType)
        {
            Int64 _defaultLabTaskUserId = 0;
            long lngNewTaskID = 0;
            try
            {
                if (LoginProviderID > 0)
                {
                    _defaultLabTaskUserId = GetDefaultLabTaskUserForProvider(LoginProviderID);
                }

                gloTaskMail.frmTask frmTask = new gloTaskMail.frmTask(_dataBaseConnectionString, 0, _defaultLabTaskUserId, true, ExamReferanceId,enumTaskType);
                try
                {
                    frmTask.PatientID = PatientID;
                    frmTask.ProviderID = PatientProviderID;
                    frmTask.TaskInsertedID = 0;
                    if (_TestList != "" && _TestList != null && _TestList.Length > 0)
                    {

                        //Developer:Sanjog Dhamke
                        //Date: 2011 Dec 14
                        //Bug ID/PRD Name/Salesforce Case: Lab Usability (6060) to show Lab test with comma separates in Description pane 
                        //Reason: To show upto 1000 character from Description string, so we increase it from 200 to 1000

                        if (_TestList.Length > 999)
                        {
                            _TestList = _TestList.Substring(0, 989);
                            _TestList = _TestList + "..(Cont)";
                            frmTask.LabTestList = _TestList;
                        }
                        else
                        {
                            frmTask.LabTestList = _TestList;
                        }
                    }
                    frmTask._sNotesExt = TestlistOnly;
                    frmTask.ShowDialog(frmTask.Parent);

                    lngNewTaskID = frmTask.TaskInsertedID; // Added by Abhijeet on 20100625

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    lngNewTaskID = 0; // Added by Abhijeet on 20100625
                }
                finally
                {
                    if (frmTask != null)
                    {
                        frmTask.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                lngNewTaskID = 0;  // Added by Abhijeet on 20100625
            }
            finally
            {

            }
            return lngNewTaskID;
        }

        public long AssignTaskToUser(Int64 PatientID, Int64 PatientProviderID, Int64 LoginProviderID, Int64 OrderId, gloTaskMail.TaskType enumTaskType , string strSubject, string strPriority)
        {
            Int64 _defaultLabTaskUserId = 0;
            long lngNewTaskID = 0;
            try
            {
                if (LoginProviderID > 0)
                {
                    _defaultLabTaskUserId = GetDefaultLabTaskUserForProvider(LoginProviderID);
                }

                gloTaskMail.frmTask frmTask = new gloTaskMail.frmTask(_dataBaseConnectionString, 0, _defaultLabTaskUserId, true, OrderId, enumTaskType);
                try
                {
                    frmTask.PatientID = PatientID;
                    frmTask.ProviderID = PatientProviderID;
                    frmTask.TaskInsertedID = 0;
                    if (_TestList != "" && _TestList != null && _TestList.Length > 0)
                    {

                        //Developer:Sanjog Dhamke
                        //Date: 2011 Dec 14
                        //Bug ID/PRD Name/Salesforce Case: Lab Usability (6060) to show Lab test with comma separates in Description pane 
                        //Reason: To show upto 1000 character from Description string, so we increase it from 200 to 1000

                        if (_TestList.Length > 999)
                        {
                            _TestList = _TestList.Substring(0, 989);
                            _TestList = _TestList + "..(Cont)";
                            frmTask.LabTestList = _TestList;
                        }
                        else
                        {
                            frmTask.LabTestList = _TestList;
                        }
                    }
                    frmTask._sNotesExt = TestlistOnly;
                    frmTask._tskSubjectForExternalOrder = strSubject;
                    frmTask.strPriority = strPriority;
                    frmTask.ShowDialog(frmTask.Parent);

                    lngNewTaskID = frmTask.TaskInsertedID; // Added by Abhijeet on 20100625

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    lngNewTaskID = 0; // Added by Abhijeet on 20100625
                }
                finally
                {
                    if (frmTask != null)
                    {
                        frmTask.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                lngNewTaskID = 0;  // Added by Abhijeet on 20100625
            }
            finally
            {

            }
            return lngNewTaskID;
        }

        //added by madan on 20100726
        public long AssignTaskToUser(Int64 PatientID, Int64 PatientProviderID, Int64 LoginProviderID)
        {
            Int64 _nTaskId = 0;
            try
            {
                _nTaskId = AssignTaskToUser(PatientID, PatientProviderID, LoginProviderID,0,gloTaskMail.TaskType.LabOrder);
            }
            catch (Exception ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _nTaskId = 0;  
            }

            return _nTaskId;
        }        

        //added by madan on 20100614
        /// <summary>
        /// Written for getting default user for provider for lab tasks.
        /// </summary>
        /// <param name="LoginProviderId"></param>
        /// <returns></returns>
        public Int64 GetDefaultLabTaskUserForProvider(Int64 LoginProviderId)
        {
            Int64 _defaultLabUserId = 0;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            string strQuery = string.Empty;
            object objResult = null;

            try
            {
                strQuery = "SELECT ISNULL(nOthersID,0) as  nOthersID FROM ProviderSettings Where UPPER(sSettingsType)='LABUSER' AND nProviderID=" + LoginProviderId;


                objDbLayer.Connect(false);
                objResult = objDbLayer.ExecuteScalar_Query(strQuery);

                if (objResult != null && objResult.ToString() != "")
                {
                    _defaultLabUserId = Convert.ToInt64(objResult);
                }

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _defaultLabUserId = 0;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }

                if (objResult != null)
                {
                    objResult = null;
                }

                strQuery = string.Empty;
            }
            return _defaultLabUserId;
        }

        //Added by madan on 20100618
        /// <summary>
        /// This method used to unlock records if it is locked.
        /// </summary>
        /// <param name="TransactionType"></param>
        /// <param name="RecordID"></param>
        /// <param name="VisitID"></param>
        /// <param name="VisitDate"></param>
        public void UnLockRecords(TrnType TransactionType, Int64 OrderID, long VisitID, DateTime VisitDate)
        {
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                objDbLayer.Connect(false);
                objDbParameters.Add("@nRecordID", OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                objDbParameters.Add("@nVisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                objDbParameters.Add("@dtVisitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);
                objDbParameters.Add("@nTrnType", TransactionType, ParameterDirection.Input, SqlDbType.Int);

                objDbLayer.ExecuteScalar("gsp_UnLock_Record", objDbParameters);
                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                if (objDbParameters != null)
                {
                    objDbParameters.Dispose();
                }
            }

        }
        /// <summary>
        /// This method used to lock the order.... is the order is already locked it will return that order is locked.
        /// Added by madan on 20100618
        /// </summary>
        /// <param name="TransactionType"></param>
        /// <param name="RecordID"></param>
        /// <param name="VisitID"></param>
        /// <param name="VisitDate"></param>
        /// <param name="MachineName"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public clsLockOrders Scan_n_Lock_Transaction(TrnType TransactionType, Int64 OrderID, long VisitID, DateTime VisitDate, string MachineName, string Username)
        {
            object _ObjUsername = null;
            object _ObjMachinename = null;
            clsLockOrders objClsLockOrders = new clsLockOrders();
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString());
            gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                objDbLayer.Connect(false);
                objDbParameters.Add("@nRecordID", OrderID, ParameterDirection.Input, SqlDbType.BigInt);
                objDbParameters.Add("@nVisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                objDbParameters.Add("@dtVisitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);
                objDbParameters.Add("@nTrnType", TransactionType, ParameterDirection.Input, SqlDbType.Int);
                objDbParameters.Add("@sUserName", Username, ParameterDirection.InputOutput, SqlDbType.VarChar, 50);
                objDbParameters.Add("@sMachineName", MachineName, ParameterDirection.InputOutput, SqlDbType.VarChar, 50);

                objDbLayer.Execute("gsp_Scan_n_Lock_Record", objDbParameters, out _ObjUsername, out _ObjMachinename);
                if (_ObjUsername != null && _ObjUsername.ToString() != "")
                {
                    objClsLockOrders.UserName = Convert.ToString(_ObjUsername).Trim();
                }
                else
                {
                    objClsLockOrders.UserName = string.Empty;
                }
                if (_ObjMachinename != null && _ObjMachinename.ToString() != "")
                {
                    objClsLockOrders.MachineName = Convert.ToString(_ObjMachinename).Trim();
                }
                else
                {
                    objClsLockOrders.MachineName = string.Empty;
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_ObjUsername != null)
                {
                    _ObjUsername = null;
                }
                if (_ObjMachinename != null)
                {
                    _ObjMachinename = null;
                }
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                if (objDbParameters != null)
                {
                    objDbParameters.Dispose();
                }
            }
            return objClsLockOrders;

        }

        public Int64 GetDataBaseConnectionIdFromHL7DB()
        { //This function written by Abhijeet on 20101109

            Int64 nDBId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());

            try
            {
                string strServerName = string.Empty;
                string strDatabaseName = string.Empty;

                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

                if (appSettings != null)
                {
                    strServerName = Convert.ToString(appSettings["SQLServerName"]);
                    strDatabaseName = Convert.ToString(appSettings["DatabaseName"]);
                }
                else
                {
                    strServerName = string.Empty;
                    strDatabaseName = string.Empty;
                }

                if (strServerName.Length == 0 || strDatabaseName.Length == 0)
                {
                    nDBId = 0;
                }
                else
                {
                    string strQry = "select nDBConnectionId from DBSettings where sServerName='" + strServerName + "' and sDatabaseName='" + strDatabaseName + "'";
                    oDB.Connect(false);
                    object objDbId = oDB.ExecuteScalar_Query(strQry);

                    if (objDbId != null)
                        nDBId = Convert.ToInt64(objDbId);
                    else
                        nDBId = 0;
                }

                if (nDBId == 0)
                {                    
                    UpdateLog("gloEMR Database ID not found in HL7 database ");
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Error while getting Database Id in HL7 database " + ex.ToString());
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return nDBId;
        }

        public string GetHL7Setting(long nDBConnectionID)
        { // Added by Abhijeet on 20110511 for getting HL7 setting value for an passed setting name to function

            string sReturnSettingValue = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());

            try
            {
                string strSettingName = string.Empty;

                //string strQry = "select isnull(sSettingsvalue,'') as sSettingvalue from HL7_Settings where sSettingsName='" + sSettingName.Replace("'", "''") + "' and nDBConnectionID=" + nDBConnectionID.ToString() ;
                string strQry = "SELECT Location FROM  HL7_PathConfigMst WHERE Inbound=1 AND nDBConnectionId=" + nDBConnectionID.ToString();
                    oDB.Connect(false);
                    object objDbId = oDB.ExecuteScalar_Query(strQry);

                    if (objDbId != null)
                        sReturnSettingValue = Convert.ToString(objDbId);
                    else
                        sReturnSettingValue = "";

                    oDB.Disconnect();
            }
            catch (Exception ex)
            {
                UpdateLog("Error while reading particular HL7 setting value : " + ex.ToString());
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return sReturnSettingValue;
        }

        public DataTable GetTaskDetail_OfLab(Int64 LabOrderID)
        {
            SqlConnection Conn = new SqlConnection(_dataBaseConnectionString);
            SqlCommand Cmd;
            SqlDataAdapter adpt = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlParameter objParam = default(SqlParameter);
            try
            {
                Conn.Open();
                Cmd = new SqlCommand("gsp_GetModifyTaskDetail", Conn);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

            

                objParam = Cmd.Parameters.Add("@ReferenceID1", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = LabOrderID;

                adpt.Fill(dt);
                Conn.Close();
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                return dt;

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return dt;
            }
            finally
            {
                if (objParam != null)
                {
                    objParam = null;
                }
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((adpt != null))
                {
                    adpt.Dispose();
                    adpt = null;
                }



                //if ((dt != null))
                //{
                //    dt.Dispose();
                //    dt = null;
                //}
            }
        }
        public DataTable GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            string strQuery = string.Empty;
            DataTable _dtResult = null;

            try
            {
                strQuery = "gsp_GetProviderDetails";

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }

            }
            return _dtResult;
        }

        //Method added for retriving provider name according to provider id
   
       
        public bool ConfirmNull(string strValue)
        {
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnCheck;
        }

        internal void GetResultSetCommentsFontSettings(out double LabResultSetCommentFontSize, out string LabResultSetCommentFontName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strSQL = string.Empty;
            DataTable _datatable = null;
            LabResultSetCommentFontSize = 0.0;
            LabResultSetCommentFontName = string.Empty;

            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT sSettingsname, sSettingsvalue FROM Settings WHERE ssettingsname in ('LabResultSetCommentFontName','LabResultSetCommentFontSize') ";

                oDB.Retrive_Query(_strSQL, out _datatable);
                oDB.Disconnect();

                if (_datatable != null)
                {
                    if (_datatable.Rows.Count > 0)
                    {
                        for (int i = 0; i < _datatable.Rows.Count; i++)
                        {
                            if (Convert.ToString(_datatable.Rows[i]["sSettingsname"]) == "LabResultSetCommentFontName")
                            {
                                LabResultSetCommentFontName = Convert.ToString(_datatable.Rows[i]["sSettingsvalue"]).Trim();
                            }
                            if (Convert.ToString(_datatable.Rows[i]["sSettingsname"]) == "LabResultSetCommentFontSize")
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(_datatable.Rows[i]["sSettingsvalue"])))
                                {
                                    string size = Convert.ToString(_datatable.Rows[i]["sSettingsvalue"]).Trim();
                                    if (size.Length > 0) { Double.TryParse(size, out LabResultSetCommentFontSize); }
                                }
                            }

                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error reading settings for Fonts for ResultSet Comment : GetResultSetCommentsFontSettings" + ex.ToString(), false);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
        }

    }

    /// <summary>
    /// Added by madan on 20100618
    /// this class is used for lock records
    /// /// </summary>
    public class clsLockOrders
    {
        private string _machineName = string.Empty;

        public string MachineName
        {
            get { return _machineName; }
            set { _machineName = value; }
        }

        private string _UserName = string.Empty;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }

    #region LabDemonstration

    public class clsLabDemonstration
    {

        #region Variables

        private Int64 _PatientId = 0;
        private string _DBConnectionString = string.Empty;
        private string _DemoOrdersFilePath = string.Empty;

        public string DemoOrdersFilePath
        {
            get { return _DemoOrdersFilePath; }
            set { _DemoOrdersFilePath = value; }
        }

        private string _DemoOrderMstFilePath = string.Empty;

        public string DemoOrderMstFilePath
        {
            get { return _DemoOrderMstFilePath; }
            set { _DemoOrderMstFilePath = value; }
        }
        private string _DemoTestMstFilePath = string.Empty;

        public string DemoTestMstFilePath
        {
            get { return _DemoTestMstFilePath; }
            set { _DemoTestMstFilePath = value; }
        }

        private string _ConnectionString = string.Empty;

        private bool disposed = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion

        #region Constructor & Destructor

        public clsLabDemonstration()
        {
            if (appSettings != null)
            {
                _ConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }
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

                }
            }
            disposed = true;
        }

        ~clsLabDemonstration()
        {
            Dispose(false);
        }

        #endregion

        public void GetAllLatestOrder(long PatiendId, string DBConnectionString, long UserID, string Username)
        {
            _PatientId = PatiendId;
            _DBConnectionString = DBConnectionString;


            gloPatient.Patient objpatient = null;
            gloPatient.gloPatient objgloPatient = new gloPatient.gloPatient(_DBConnectionString);
            objpatient = objgloPatient.GetPatient(_PatientId);
            objgloPatient.Dispose();
            objgloPatient = null;

            string strFileName = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);

            DataSet dsAllOrders = new DataSet();
            DataSet dsOrderTest = null;
            try
            {

                strFileName = DemoOrderMstFilePath;
                dsAllOrders.ReadXml(strFileName);

                if (dsAllOrders != null && dsAllOrders.Tables.Count > 1)
                {
                    //Read All Database Orders 
                    GloLabOrder_Msts oEmdeonOrder_Msts = new GloLabOrder_Msts();

                    oDB.Connect(false);
                    //Read All Database Orders                 
                    //dsAllOrders.Tables[1].DefaultView.RowFilter = "patient_id='" + objpatient.DemographicsDetail.PatientCode + "'";

                    ////Read All Database Orders 
                    //oDB.Connect(false);                  

                    if (dsAllOrders.Tables["OBJECT"] != null && dsAllOrders.Tables["OBJECT"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsAllOrders.Tables["OBJECT"].DefaultView.Count; i++)
                        {
                            string _EmdeonOrderID = dsAllOrders.Tables[1].DefaultView[i]["order"].ToString();


                            strFileName = DemoTestMstFilePath;


                            dsOrderTest = new DataSet();
                            dsOrderTest.ReadXml(strFileName);
                            if (dsOrderTest != null && dsOrderTest.Tables.Count > 1)
                            {

                                if (dsOrderTest.Tables["OBJECT"] != null && dsOrderTest.Tables["OBJECT"].Rows.Count > 0)
                                {

                                    DataTable objdt = null;
                                    objdt = SelectDistinct("OrderCode", dsOrderTest.Tables["OBJECT"], "order_code");
                                    if (objdt != null)
                                    {
                                        GloLabOrder_tests objEmdeonOrder_tests = new GloLabOrder_tests();

                                        for (int k = 0; k < objdt.Rows.Count; k++)
                                        {
                                            GloLabOrder_test objEmdeonOrder_test = new GloLabOrder_test();

                                            DataTable DTOrderTest = null;
                                            DTOrderTest = dsOrderTest.Tables["OBJECT"];
                                            DTOrderTest.DefaultView.RowFilter = "order_code='" + Convert.ToString(objdt.Rows[k]["order_code"]) + "'";
                                            DataTable DTFilterOrderTest = null;
                                            DTFilterOrderTest = DTOrderTest.DefaultView.ToTable();

                                            string _EmdeonTestCode = Convert.ToString(DTFilterOrderTest.Rows[0]["order_code"]);
                                            string _EmdeonTestName = Convert.ToString(DTFilterOrderTest.Rows[0]["orderable_description"]);

                                            objEmdeonOrder_test.GloLabTestName = _EmdeonTestName;
                                            objEmdeonOrder_test.GloLabTestCode = _EmdeonTestCode;

                                            // code for getting collection of Diagnosis
                                            GloLabOrder_test_diagnoses oGloLabTestDiagnoses = new GloLabOrder_test_diagnoses();
                                            string _EmdeonICDCode = string.Empty;
                                            string _EmdeonDiagDesc = string.Empty;
                                            
                                            for (int j = 0; j < DTFilterOrderTest.Rows.Count; j++)
                                            {
                                                GloLabOrder_test_diagnosis oGloLabTestDiagnosis = new GloLabOrder_test_diagnosis();
                                                _EmdeonICDCode = string.Empty;
                                                _EmdeonDiagDesc = string.Empty;
                                                if ((Convert.ToString(DTFilterOrderTest.Rows[j]["icd_9_cm_code"]).Trim() != ""))
                                                {
                                                     oGloLabTestDiagnosis.nICDRevision = 9;
                                                    _EmdeonICDCode = Convert.ToString(DTFilterOrderTest.Rows[j]["icd_9_cm_code"]);
                                                    _EmdeonDiagDesc = Convert.ToString(DTFilterOrderTest.Rows[j]["order_diag_description"]);
                                                }
                                                else  //added for icd10 feature through emdeon
                                                {
                                                     oGloLabTestDiagnosis.nICDRevision = 10;
                                                    _EmdeonICDCode = Convert.ToString(DTFilterOrderTest.Rows[j]["icd_10_cm_code"]);
                                                    _EmdeonDiagDesc = Convert.ToString(DTFilterOrderTest.Rows[j]["order_diag_description"]);
                                         
                                                }
                                                oGloLabTestDiagnosis.GloLabICD9Code = _EmdeonICDCode;
                                                oGloLabTestDiagnosis.GloLabDiagDescription = _EmdeonDiagDesc;
                                                oGloLabTestDiagnoses.Add(oGloLabTestDiagnosis);
                                            }
                                            objEmdeonOrder_test.GloLabTestsDiagnoses = oGloLabTestDiagnoses;
                                            // end of code for getting collection of Diagnosis

                                            objEmdeonOrder_tests.Add(objEmdeonOrder_test);
                                            //**Add Recieved Data -- Tests                                      
                                            DTFilterOrderTest.Dispose();
                                            DTFilterOrderTest = null;
                                        }
                                        oEmdeonOrder_Msts.Add(Convert.ToDouble(_EmdeonOrderID), objEmdeonOrder_tests);
                                        objdt.Dispose();
                                        objdt = null;
                                    }
                                    else
                                    {
                                        oEmdeonOrder_Msts.Add(Convert.ToDouble(_EmdeonOrderID), new GloLabOrder_test());

                                    }
                                    oEmdeonOrder_Msts[i].PatientAgeYear = objpatient.DemographicsDetail.PatientDOB.Year;/// gloUC_PatientStrip1.PatientAge.Years;
                                    oEmdeonOrder_Msts[i].PatientAgeMonth = objpatient.DemographicsDetail.PatientDOB.Month;/// gloUC_PatientStrip1.PatientAge.Months;
                                    oEmdeonOrder_Msts[i].PatientAgeDays = objpatient.DemographicsDetail.PatientDOB.Day; // gloUC_PatientStrip1.PatientAge.Days;
                                    oEmdeonOrder_Msts[i].GloLabOrderID = Convert.ToDouble(_EmdeonOrderID);
                                    oEmdeonOrder_Msts[i].PatientID = _PatientId;

                                    oEmdeonOrder_Msts[i].PatientProviderID = objpatient.DemographicsDetail.PatientProviderID;       //gloUC_PatientStrip1.ProviderID;
                                    oEmdeonOrder_Msts[i].TransactionDate = Convert.ToDateTime(dsAllOrders.Tables["OBJECT"].DefaultView[i]["collection_datetime"].ToString());
                                    oEmdeonOrder_Msts[i].GloLabOrderNumber = dsAllOrders.Tables["OBJECT"].DefaultView[i]["placer_order_number"].ToString();
                                    // Added by Abhijeet on date 20100330
                                    // saving the Order status & billing type
                                    oEmdeonOrder_Msts[i].BillingType = dsAllOrders.Tables["OBJECT"].DefaultView[i]["bill_type"].ToString();
                                    oEmdeonOrder_Msts[i].OrderStatus = dsAllOrders.Tables["OBJECT"].DefaultView[i]["order_status"].ToString();
                                    // End of changes by Abhijeet

                                }
                            }
                            if (dsOrderTest != null)
                            {
                                dsOrderTest.Dispose();
                                dsOrderTest = null;
                            }


                        }
                    }

                    //by Abhijeet on 20100512 
                    if (oEmdeonOrder_Msts != null)
                    {
                        clsInsertEmdeonData oInsertEmdeonData;

                        oInsertEmdeonData = new clsInsertEmdeonData(_DBConnectionString, oEmdeonOrder_Msts, UserID, Username);

                        oInsertEmdeonData.SaveOrdTestDtl();
                        oInsertEmdeonData = null;
                        oEmdeonOrder_Msts.Clear();
                        oEmdeonOrder_Msts.Dispose();
                    }
                }
                if (dsAllOrders != null)
                {
                    dsAllOrders.Dispose();
                    dsAllOrders = null;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (objpatient != null)
                {
                    objpatient.Dispose();
                    objpatient = null;
                }
            }
        }

        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            try
            {
                DataTable dt = new DataTable(TableName);
                dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

                object LastValue = null;
                foreach (DataRow dr in SourceTable.Select("", FieldName))
                {
                    if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                    {
                        LastValue = dr[FieldName];
                        dt.Rows.Add(new object[] { LastValue });
                    }
                }
                return dt;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL24 - Insert Emdeon Data :" + ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL25 - Insert Emdeon Data :" + ex.ToString());
                return null;
            }
        }

        private bool ColumnEqual(object A, object B)
        {

            try
            {
                if (A == DBNull.Value && B == DBNull.Value)
                    return true;
                if (A == DBNull.Value || B == DBNull.Value)
                    return false;
                return (A.Equals(B));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL25 - " + ex.ToString(), false);
            }
            return false;
        }

        // added by madan on 20100621
        public bool CheckForDemoApplication()
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_ConnectionString.ToString().Trim());
            string strQry = string.Empty;
            Int64 _result = 0;
            object _objReult = null;
            try
            {
                strQry = "select sSettingsValue from settings where sSettingsName='DEMO APPLICATION'";
                objDbLayer.Connect(false);

                _objReult = objDbLayer.ExecuteScalar_Query(strQry);

                if (_objReult != null && _objReult.ToString() != "")
                {
                    _result = Convert.ToInt64(_objReult);

                    if (_result == 1)
                    {
                        _blnResult = true;
                    }
                    else
                    {
                        return false;
                    }
                }



                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                strQry = string.Empty;
                _result = 0;
            }
            return _blnResult;
        }
        /// <summary>
        /// Added by madan on 20100621
        /// This method is used to activate the demo labs.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public bool ActivateDemoLabs(Int64 PatientID)
        {
            bool _blnResult = false;
            string strQuery = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            Int64 _orderId = 0;
            object _objResult = null;
            try
            {
                if (PatientID < 0)
                {
                    return false;
                }

                //strQuery = "Select labom_OrderID from lab_Order_Mst where labom_PatientID=" + PatientID + " AND labom_dgloLabOrderID ='31312306.0000'";
                strQuery = "Select labom_OrderID from lab_Order_Mst where labom_dgloLabOrderID ='123456789.0000'";

                oDB.Connect(false);

                _objResult = oDB.ExecuteScalar_Query(strQuery);

                if (_objResult != null && _objResult.ToString() != "")
                {
                    _orderId = Convert.ToInt64(_objResult);

                }

                if (_orderId > 0)
                {
                    strQuery = "DELETE FROM LAB_ORDER_MST WHERE labom_OrderID =" + _orderId;
                    if (DeleteTable(strQuery))
                    {
                        strQuery = "DELETE FROM Lab_Order_TestDtl WHERE labotd_OrderID = " + _orderId;
                        if (DeleteTable(strQuery))
                        {
                            strQuery = string.Empty;
                            strQuery = "DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID =" + _orderId;
                            if (DeleteTable(strQuery))
                            {
                                strQuery = string.Empty;
                                strQuery = "DELETE FROM Lab_Order_Test_Result where labotr_OrderID = " + _orderId;
                                if (DeleteTable(strQuery))
                                {
                                    strQuery = string.Empty;
                                    strQuery = "DELETE FROM Lab_Order_Test_ResultDtl where labotrd_OrderID = " + _orderId;
                                    if (DeleteTable(strQuery))
                                    {
                                        _blnResult = true;
                                    }
                                    else
                                        return false;
                                }
                                else
                                    return false;
                            }
                            else
                                return false;
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
                else
                {
                    _blnResult = true;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                if (_objResult != null)
                {
                    _objResult = null;
                }

                _orderId = 0;
                strQuery = string.Empty;

            }
            return _blnResult;
        }

        private bool DeleteTable(string strQuery)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            int _result = 0;
            try
            {
                oDB.Connect(false);
                if (strQuery != "")
                {
                    _result = oDB.Execute_Query(strQuery);
                    if (_result < 0)
                        _blnResult = false;
                    else if (_result >= 0)
                        _blnResult = true;
                    else
                        _blnResult = false;
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQuery = string.Empty;
            }
            return _blnResult;

        }

        public bool CheckDemoOrderFileStatus()
        {
            bool _result = false;
            try
            {
                if (System.IO.Directory.Exists(DemoOrdersFilePath))
                {
                    if ((System.IO.File.Exists(DemoOrderMstFilePath)) && (System.IO.File.Exists(DemoTestMstFilePath)))
                    {
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
            }
            catch (System.IO.IOException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
            }
            return _result;
        }

    }

    #endregion
}
