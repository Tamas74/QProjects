using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Edidev.FrameworkEDI;
namespace gloPatient
{
    public class gloPatientEiligibility : IDisposable
    {
        #region " Variable Declaration "
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;
        private Int64 _patientid = 0;
        private Int64 _patientInsid = 0;
        private Int64 _patientInsContactid = 0;
        private Int64 _providerid = 0;
        private string _username = "";
        private Int64 _UserID = 0;
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

        #endregion " Variable Declaration "

        #region "Constructor & Destructor"

        public gloPatientEiligibility(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _username = Convert.ToString(appSettings["UserName"]);
                }
            }
            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion
        }

        public gloPatientEiligibility(string DatabaseConnectionString,Int64 PatientID,Int64 PatientInsuranceID,Int64 ContactID,Int64 ProviderID)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _username = Convert.ToString(appSettings["UserName"]);
                }
            }
            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            this.PatientID = PatientID;
            this.PatientInsuranceID = PatientInsuranceID;
            this.PatientInsContactID = ContactID;
            this.ProviderID = ProviderID;
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

        ~gloPatientEiligibility()
        {
            Dispose(false);
        }

        #endregion

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 PatientID
        {
            get { return _patientid; }
            set { _patientid = value; }
        }
        public Int64 PatientInsuranceID
        {
            get { return _patientInsid; }
            set { _patientInsid = value; }
        }
        public Int64 PatientInsContactID 
        {
            get { return _patientInsContactid; }
            set { _patientInsContactid = value; }
        }
        public Int64 ProviderID
        {
            get { return _providerid; }
            set { _providerid = value; }
        }

        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        #endregion " Property Procedures "

        #region " Private & Public Methods "

        private string ControlNumberGeneration(string HeaderType)
        {
            string strNumber = DateTime.Now.ToString("hhmmss");
            //SLR: just return HeaderType + (strNumber.Trim().PadLeft(8,'0')) ;
            return HeaderType + (strNumber.Trim().PadLeft(8, '0'));
            
            //int _length = 0;
            //string NumberSize = "";
            //_length = strNumber.Trim().Length;
            //if (_length == 5)
            //{
            //    NumberSize = "000" + strNumber;
            //}
            //else if (_length == 6)
            //{
            //    NumberSize = "00" + strNumber;
            //}
            //else if (_length == 7)
            //{
            //    NumberSize = "0" + strNumber;
            //}
            //else if (_length == 8)
            //{
            //    NumberSize = strNumber;
            //}
            //NumberSize = HeaderType + NumberSize;
            //return NumberSize;
        }

        public string ControlNumberGeneration(bool _flag)
        {
            string strNumber = String.Empty;
            //SLR: this will be unneccessarily slow: Instead use like this: static readonly Random getrandom = new Random(); strNumber = getrandom .Next(1, 10) + String.Format("{0:d8}", (DateTime.Now.Ticks / 100) % 100000000);
            Random getrandom = new Random();
            strNumber = getrandom.Next(1, 10) + String.Format("{0:d8}", (DateTime.Now.Ticks / 100) % 100000000);
            
            //strNumber = "0";

            //while (strNumber.StartsWith("0") == true)
            //{
            //    //while loop added to avoid preceding zeros as X12 standards doesn't accepts leading zeros.
            //    strNumber = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            //}
            return strNumber;


            
        }

        private string FormattedTime(string TimeFormat)
        {
            //SLR: just return TimeFormat.PadLeft(4,'0')) ;
            return TimeFormat.PadLeft(4, '0');

            //int _length = 0;
            //_length = TimeFormat.Length;
            //if (_length == 0)
            //{
            //    TimeFormat = "0000";
            //}
            //if (_length == 1)
            //{
            //    TimeFormat = "000" + TimeFormat;
            //}
            //else if (_length == 2)
            //{
            //    TimeFormat = "00" + TimeFormat;
            //}
            //else if (_length == 3)
            //{
            //    TimeFormat = "0" + TimeFormat;
            //}
            //return TimeFormat;
        }

        public DataTable GetClearingHouseSettings(Int64 ContactId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtClearingHouse = null;
            try
            {                
                dtClearingHouse = new DataTable();
                oDB= new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = null;
                oDB.Connect(false);
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactId", ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicId", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("EDI_GetClearingHouse", oParameters, out dtClearingHouse);
                oDB.Disconnect();
                oParameters.Dispose();
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
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtClearingHouse;
        }

        public DataTable GetProvider(Int64 ProviderId, Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtProvider = null;
            string SettingType = String.Empty;
            try
            {
                if (ProviderId > 0)
                {
                    oDB.Connect(false);
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add("@nProviderID", ProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nUserID", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@SettingType", SettingType, ParameterDirection.InputOutput, SqlDbType.VarChar);
                    //SLR: Why to Allocate memory for dtProvider?
                    _dtProvider = null;
                    oDB.Retrive("BL_GET_Provider_EDI", oParameters, out _dtProvider);
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtProvider;
        }

        private DataTable GetPatient(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPatient = null;
            string _sqlQuery = "";

            try
            {
                if (PatientId > 0)
                {
                    _sqlQuery = "SELECT " +
                    " ISNULL(sPatientCode,'') AS PatientCode,ISNULL(nSSN,'') AS SSN, " +
                    " ISNULL(sFirstName,'') AS FirstName,ISNULL(sMiddleName,'') AS MiddleName, " +
                    " ISNULL(sLastName,'') AS LastName,dtDOB,ISNULL(sGender,'') AS Gender, " +
                    " ISNULL(sAddressLine1,'') AS AddrLn1, " +
                    " ISNULL(sAddressLine2,'') AS AddrLn2, " +
                    " ISNULL(sCity,'') AS City,ISNULL(sState,'') AS State, " +
                    " ISNULL(sZIP,'') AS Zip,ISNULL(nProviderID,0) AS PatientProviderID " +
                    " FROM " +
                    " 	 Patient " +
                    " WHERE " +
                    " 	 nPatientID = "+PatientId+"";

                    oDB.Connect(false);
                    //SLR: Alocate _DtPatient memory?
                    _dtPatient = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out _dtPatient);
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _dtPatient;
        }

        public DataTable GetPatientInsurance(Int64 ContactId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtContactIns = null;
            string _sqlQuery = "";

            try
            {
                if (ContactId > 0)
                {
                    _sqlQuery =
                                " SELECT " +
                                " ISNULL(Contacts_MST.sContact,'') as sContact,  " +
                                " ISNULL(Contacts_MST.sName,'') as sInsuranceName, " +
                                " ISNULL(Contacts_Insurance_DTL.sPayerId,'') as sPayerId , " +
                                " ISNULL(Contacts_Insurance_DTL.sInsEligibilityProviderID,'') AS sInsEligibilityProviderID, " +
                                " ISNULL(Contacts_Insurance_DTL.sInsEligibilityProviderType,'') AS sInsEligibilityProviderType, " +
                                " ISNULL(Contacts_Insurance_DTL.sInsEligibilityProvSecID,'') AS sInsEligibilityProvSecID, " +
                                " ISNULL(Contacts_Insurance_DTL.sInsEligibilityProviSecType,'') AS sInsEligibilityProviSecType " +
                                " FROM   " +
                                " Contacts_MST left outer  join Contacts_Insurance_DTL  " +
                                " ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID  " +
                                " WHERE   " +
                                " Contacts_MST.nContactID = " + ContactId + " " +
                                " AND Contacts_MST.nClinicID=  " + ClinicID + " ";
                    //" AND ISNULL(bIsBlocked,0) = 0 ";

                    oDB.Connect(false);
                    //SLR: Alocate _DtContactIns memory?
                    _dtContactIns = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out _dtContactIns);
                    oDB.Disconnect();
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _dtContactIns;
        }

        private DataTable GetInsuranceSubscriber(Int64 PatientId,Int64 InsuranceId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtSubscriber = null;
            string _sqlQuery = "";

            try
            {
                if (PatientId > 0 && InsuranceId > 0) 
                {
                    _sqlQuery =
                            " SELECT ISNULL(sInsuranceName,'') AS PatientInsuranceName," +
                            " ISNULL(sSubscriberID,0) AS SubscriberID, " +
                            " ISNULL(sSubFName,'') AS SubFName,ISNULL(sSubMName,'') AS SubMName,ISNULL(sSubLName,'') AS SubLName, " +
                            " ISNULL(sSubscriberPolicy#,'') AS SubscriberPolicy, " +
                            " ISNULL(sGroup,'') AS [Group],dtDOB, " +
                            " ISNULL(sSubscriberGender,'') AS SubGender, " +
                            " ISNULL(sSubscriberAddr1,'') AS SubAddrLn1,ISNULL(sSubscriberAddr2,'') AS SubAddrLn2, " +
                            " ISNULL(sSubscriberState,'') AS State,ISNULL(sSubscriberCity,'') AS City,ISNULL(sSubscriberZip,'') AS Zip, " +
                            " ISNULL(nRelationShipID,0) AS RelationShipID,ISNULL(sRelationShip,'') AS RelationShip, " +
                            " ISNULL(bIsSameAsPatient,'false') AS IsSameAsPatient, " +
                            " dtStartDate,dtEndDate ,ISNULL(bIsCompnay,0) AS bIsCompnay,ISNULL(sCompanyName,'') AS sCompanyName" +
                            " FROM PatientInsurance_DTL  " +
                            " WHERE nPatientID = " + PatientId + " AND nInsuranceID = " + InsuranceId + " ";

                    oDB.Connect(false);
                    //SLR: Alocate _DtSubscriber memory?
                    _dtSubscriber = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out _dtSubscriber);
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _dtSubscriber;
        }

        private DateTime GetServerDateTime()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DateTime _serverdt = DateTime.MinValue;
            string _sqlQuery = "";

            try
            {
                _sqlQuery = "SELECT dbo.gloGetDate() AS [DateTime]";
                oDB.Connect(false);
                _serverdt = Convert.ToDateTime(oDB.ExecuteScalar_Query(_sqlQuery));
                oDB.Disconnect();
            }
            catch (Exception)
            { _serverdt = DateTime.Now; }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _serverdt;
        }

        public EiligiblityData GetEiligibilityData(Int64 PatientId, Int64 ProviderId, Int64 InsuranceId, Int64 ContactId)
        {
            EiligiblityData _eligibilityData = null;
            DataTable _dtClearingHouse = null;
            DataTable _dtProvider = null;
            DataTable _dtPatient = null;
            DataTable _dtPatientInsurance = null;
            DataTable _dtSubscriber = null;

            try
            {
                if (PatientId > 0 && InsuranceId > 0 && ContactId > 0)
                {
                    //..Retrive all required details from database
                    _dtClearingHouse = GetClearingHouseSettings(ContactId,_ClinicID);
                    _dtProvider = GetProvider(ProviderId, ProviderId); //GetProvider(ProviderId,UserID);
                    _dtPatient = GetPatient(PatientId);
                    _dtPatientInsurance = GetPatientInsurance(ContactId);
                    _dtSubscriber = GetInsuranceSubscriber(PatientId, InsuranceId);

                    _eligibilityData = new EiligiblityData();

                    //..Set clearing house details
                    if (_dtClearingHouse != null)
                    {
                        if (_dtClearingHouse.Rows.Count > 0)
                        {

                            _eligibilityData.ClearingHouseReceiverID = Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.ClearingHouseSubmitterID = Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.ClearingHouseTypeOfData = Convert.ToString(_dtClearingHouse.Rows[0]["nTypeOfData"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.EligibilityUserName = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUserName"]).Trim();
                            _eligibilityData.EligibilityPassword = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityPassword"]).Trim();
                            _eligibilityData.EligibilityUrl = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUrl"]).Trim();
                            _eligibilityData.SubmitterID = Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]).Trim();
                            _eligibilityData.ClearingHouseType = Convert.ToInt32(_dtClearingHouse.Rows[0]["nClearingHouseType"]);

                            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                            _eligibilityData.SenderQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sSenderIDQualifier"]);
                            _eligibilityData.ReceiverQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverIDQualifier"]);



                            if (_eligibilityData.ClearingHouseTypeOfData.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") != "")
                            {
                                if (_eligibilityData.ClearingHouseTypeOfData.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") == "0" || _eligibilityData.ClearingHouseTypeOfData.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") == "1")
                                {
                                    _eligibilityData.ClearingHouseTypeOfData = "T";
                                }
                                else if (_eligibilityData.ClearingHouseTypeOfData.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") == "2")
                                {
                                    _eligibilityData.ClearingHouseTypeOfData = "P";
                                }
                            }

                       }
                  }


                    //..Set Contact Insurance detail
                    if(_dtPatientInsurance != null && _dtPatientInsurance.Rows.Count > 0)
                    {
                        _eligibilityData.PayerID = Convert.ToString(_dtPatientInsurance.Rows[0]["sPayerId"]);
                        _eligibilityData.InsEligibilityProviderID = Convert.ToString(_dtPatientInsurance.Rows[0]["sInsEligibilityProviderID"]);
                        _eligibilityData.InsEligibilityProviderType = Convert.ToString(_dtPatientInsurance.Rows[0]["sInsEligibilityProviderType"]);
                        _eligibilityData.InsEligibilityProvSecID = Convert.ToString(_dtPatientInsurance.Rows[0]["sInsEligibilityProvSecID"]);
                        _eligibilityData.InsEligibilityProviSecType = Convert.ToString(_dtPatientInsurance.Rows[0]["sInsEligibilityProviSecType"]);
                    }


                    //..Set Provider details
                    if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                    {
                        _eligibilityData.ProviderFName = Convert.ToString(_dtProvider.Rows[0]["FirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderMName = Convert.ToString(_dtProvider.Rows[0]["MiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderLName = Convert.ToString(_dtProvider.Rows[0]["LastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");


                        _eligibilityData.ProviderSSN = Convert.ToString(_dtProvider.Rows[0]["SSN"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        //_eligibilityData.ProviderNPI = Convert.ToString(_dtProvider.Rows[0]["NPI"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        string _result = GetAlternateProvider(ProviderId, ContactId);
                        if (_result != "")
                        {
                            _eligibilityData.ProviderNPI = Convert.ToString(_result).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        }
                        _eligibilityData.ProviderAddress = Convert.ToString(_dtProvider.Rows[0]["Address"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderCity = Convert.ToString(_dtProvider.Rows[0]["City"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderState = Convert.ToString(_dtProvider.Rows[0]["State"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderZip = Convert.ToString(_dtProvider.Rows[0]["Zip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderAreaCode = Convert.ToString(_dtProvider.Rows[0]["AreaCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderTaxId = Convert.ToString(_dtProvider.Rows[0]["TaxId"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.ProviderSettingValue = Convert.ToString(_dtProvider.Rows[0]["SettingValue"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                    }

                    //..Set Patient details
                    if (_dtPatient != null && _dtPatient.Rows.Count > 0)
                    {
                            _eligibilityData.PatientCode = Convert.ToString(_dtPatient.Rows[0]["PatientCode"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.PatientFName = Convert.ToString(_dtPatient.Rows[0]["FirstName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.PatientLName = Convert.ToString(_dtPatient.Rows[0]["LastName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                            _eligibilityData.PatientMName = Convert.ToString(_dtPatient.Rows[0]["MiddleName"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");

                            if (_dtPatient.Rows[0]["dtDOB"] != DBNull.Value && Convert.ToString(_dtPatient.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") != "")
                            { _eligibilityData.PatientDOB = Convert.ToDateTime(_dtPatient.Rows[0]["dtDOB"]).ToString("MM/dd/yyyy"); }

                            _eligibilityData.PatientGender = Convert.ToString(_dtPatient.Rows[0]["Gender"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");

                   
                        if (_eligibilityData.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") != "")
                        {
                            if (_eligibilityData.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "MALE")
                            { _eligibilityData.PatientGender = "M"; }
                            else if (_eligibilityData.PatientGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "FEMALE")
                            { _eligibilityData.PatientGender = "F"; }
                            else
                            { _eligibilityData.PatientGender = "U"; }
                        }
                        else
                        { _eligibilityData.PatientGender = "U"; }

                        _eligibilityData.PatientID = PatientId.ToString();
                        _eligibilityData.PatientContactInsID = ContactId.ToString();
                        _eligibilityData.PatientCity = Convert.ToString(_dtPatient.Rows[0]["City"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.PatientSSN = Convert.ToString(_dtPatient.Rows[0]["SSN"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.PatientState = Convert.ToString(_dtPatient.Rows[0]["State"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.PatientZip = Convert.ToString(_dtPatient.Rows[0]["Zip"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.PatientAddressLn1 = Convert.ToString(_dtPatient.Rows[0]["AddrLn1"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                        _eligibilityData.PatientAddressLn2 = Convert.ToString(_dtPatient.Rows[0]["AddrLn2"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                    }

                    //..Set Patient Insurance details
                    if (_dtSubscriber != null && _dtSubscriber.Rows.Count > 0)
                    {
                        _eligibilityData.PayerName = Convert.ToString(_dtSubscriber.Rows[0]["PatientInsuranceName"]);
                        _eligibilityData.Group = Convert.ToString(_dtSubscriber.Rows[0]["Group"]);
                        _eligibilityData.PatientSubscriberRelationShip = Convert.ToString(_dtSubscriber.Rows[0]["RelationShip"]);
                        _eligibilityData.SubscriberFName = Convert.ToString(_dtSubscriber.Rows[0]["SubFName"]);
                        _eligibilityData.SubscriberLName = Convert.ToString(_dtSubscriber.Rows[0]["SubLName"]);
                        _eligibilityData.SubscriberMName = Convert.ToString(_dtSubscriber.Rows[0]["SubMName"]);
                        _eligibilityData.SubscriberCompanyname = Convert.ToString(_dtSubscriber.Rows[0]["sCompanyName"]);
                        _eligibilityData.IsSubscriberCompany = Convert.ToBoolean(_dtSubscriber.Rows[0]["bIsCompnay"]);

                        if (_dtSubscriber.Rows[0]["dtDOB"] != DBNull.Value && Convert.ToString(_dtSubscriber.Rows[0]["dtDOB"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") != "")
                        { _eligibilityData.SubscriberDOB = Convert.ToDateTime(_dtSubscriber.Rows[0]["dtDOB"]).ToString("MM/dd/yyyy"); }

                        _eligibilityData.SubscriberGender = Convert.ToString(_dtSubscriber.Rows[0]["SubGender"]);

                        if (_eligibilityData.SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "") != "")
                        {
                            if (_eligibilityData.SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "MALE")
                            { _eligibilityData.SubscriberGender = "M"; }
                            else if (_eligibilityData.SubscriberGender.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").ToUpper() == "FEMALE")
                            { _eligibilityData.SubscriberGender = "F"; }
                            else
                            { _eligibilityData.SubscriberGender = "U"; }
                        }
                        else
                        { _eligibilityData.SubscriberGender = "U"; }


                        _eligibilityData.SubscriberID = Convert.ToString(_dtSubscriber.Rows[0]["SubscriberID"]);
                        _eligibilityData.SubscriberCity = Convert.ToString(_dtSubscriber.Rows[0]["City"]);
                        _eligibilityData.SubscriberSSN = "";
                        _eligibilityData.SubscriberState = Convert.ToString(_dtSubscriber.Rows[0]["State"]);
                        _eligibilityData.SubscriberZip = Convert.ToString(_dtSubscriber.Rows[0]["Zip"]);
                        _eligibilityData.SubscriberAddressLn1 = Convert.ToString(_dtSubscriber.Rows[0]["SubAddrLn1"]);
                        _eligibilityData.SubscriberAddressLn2 = Convert.ToString(_dtSubscriber.Rows[0]["SubAddrLn2"]);
                        _eligibilityData.IsSameAsPatient = Convert.ToBoolean(_dtSubscriber.Rows[0]["IsSameAsPatient"]);

                        if (_dtSubscriber.Rows[0]["dtStartDate"] != DBNull.Value && Convert.ToString(_dtSubscriber.Rows[0]["dtStartDate"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                        { _eligibilityData.SubscriberInsStartDate = Convert.ToDateTime(_dtSubscriber.Rows[0]["dtStartDate"]).ToString("MM/dd/yyyy"); }
                        else
                        { _eligibilityData.SubscriberInsStartDate = ""; }

                        if (_dtSubscriber.Rows[0]["dtEndDate"] != DBNull.Value && Convert.ToString(_dtSubscriber.Rows[0]["dtEndDate"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "") != "")
                        { _eligibilityData.SubscriberInsEndDate = Convert.ToDateTime(_dtSubscriber.Rows[0]["dtEndDate"]).ToString("MM/dd/yyyy"); }
                        else
                        { _eligibilityData.SubscriberInsEndDate = ""; }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dtClearingHouse != null) { _dtClearingHouse.Dispose(); }
                if (_dtProvider != null) { _dtProvider.Dispose(); }
                if (_dtPatient != null) { _dtPatient.Dispose(); }
                if (_dtPatientInsurance != null) { _dtPatientInsurance.Dispose(); }
                if (_dtSubscriber != null) { _dtSubscriber.Dispose(); }
            }

            return _eligibilityData;
        }

        private bool LoadEDIObject()
        {
            bool _retValue = true;
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "270_X092A1.SEF";
                sEdiFile = "270OUTPUT.x12";
                
                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument()); //SLR: when is the newly allocated edidocument freeed: Please resfer any guideline provided by library?
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (!System.IO.File.Exists(sPath + sSEFFile))
                {
                    MessageBox.Show("SEF file is not present in the base directory.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _retValue = false;
                }

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));

                //oEdiDoc.ImportSchema(sPath + sSEFFile, 0);
                //ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _retValue = false;
            }

            return _retValue;
        }

        public Boolean EDIGeneration_270(EiligiblityData EData,bool bIsInsuranceAdd=false)
        {
            string _TypeOfData = "T";
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string EdiFile = "";
            string _response = string.Empty;

            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            string sReceiverQualifier = "ZZ";
            string sSenderQualifier = "ZZ";
            bool bIsServiceSucceed = false;
            try
            {
                if (LoadEDIObject() == true)
                {
                    oEdiDoc.SegmentTerminator = "~\r\n";
                    oEdiDoc.ElementTerminator = "*";
                    oEdiDoc.CompositeTerminator = ":";

                    if (EData != null && ValidateDataEligibilityData(EData) == true)
                    {

                        #region " Interchange Segment "
                        //Create the interchange segment
                        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                        if (FormatString(EData.ClearingHouseTypeOfData).Trim() == "0" || FormatString(EData.ClearingHouseTypeOfData).Trim() == "1")
                        { _TypeOfData = "T"; }
                        else if (FormatString(EData.ClearingHouseTypeOfData).Trim() == "2")
                        { _TypeOfData = "P"; }

                        if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                        {
                            _TypeOfData = "P";
                        }

                        //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        if (EData.SenderQualifier != "")
                        { sSenderQualifier = EData.SenderQualifier; }

                        if (EData.ReceiverQualifier != "")
                        { sReceiverQualifier = EData.ReceiverQualifier; }

                        oSegment.set_DataElementValue(1, 0, "00");
                        oSegment.set_DataElementValue(3, 0, "00");
                        oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        oSegment.set_DataElementValue(6, 0, FormatString(EData.ClearingHouseSubmitterID).Trim());
                        oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        oSegment.set_DataElementValue(8, 0,  FormatString(EData.ClearingHouseReceiverID).Trim());
                        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                        oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));
                        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                        oSegment.set_DataElementValue(11, 0, "U");
                        oSegment.set_DataElementValue(12, 0, "00401");
                        InterchangeHeader = ControlNumberGeneration("1");
                        oSegment.set_DataElementValue(13, 0, InterchangeHeader);
                        oSegment.set_DataElementValue(14, 0, "1");
                        oSegment.set_DataElementValue(15, 0, _TypeOfData);
                        oSegment.set_DataElementValue(16, 0, ":");

                        #endregion " Interchange Segment "

                        #region " Functional Group "

                        //Create the functional group segment
                        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                        oSegment.set_DataElementValue(1, 0, "HS");
                        oSegment.set_DataElementValue(2, 0,FormatString(EData.ClearingHouseSubmitterID).Trim());
                        oSegment.set_DataElementValue(3, 0,  FormatString(EData.ClearingHouseReceiverID).Trim());
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                        FunctionalGroupHeader = ControlNumberGeneration("2");
                        oSegment.set_DataElementValue(6, 0,FormatString(FunctionalGroupHeader).Trim());
                        //oSegment.set_DataElementValue(6, 0, "1");
                        oSegment.set_DataElementValue(7, 0, "X");
                        oSegment.set_DataElementValue(8, 0, "004010X092A1");

                        #endregion " Functional Group "

                        #region "Transaction Set "
                        //HEADER
                        //ST TRANSACTION SET HEADER
                        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                        TransactionSetHeader = ControlNumberGeneration("3");
                        oSegment.set_DataElementValue(2, 0, FormatString(TransactionSetHeader).Trim());

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
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.PayerName).Trim());
                        oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                        oSegment.set_DataElementValue(9, 0, FormatString(EData.PayerID).Trim());//"77710");//PayerID

                        #endregion " Information Source "

                        #region " Receiver Loop "

                        //INFORMATION RECEIVER LEVEL
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "2");
                        oSegment.set_DataElementValue(2, 0, "1");
                        oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                        oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                        //INFORMATION RECEIVER NAME (It is the medical service Provider)

                        //if (EData.ProviderSettingValue.Trim().ToUpper() == "CLINIC")
                        //{
                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        //    oSegment.set_DataElementValue(1, 0, "1P");//FA=Facility
                        //    oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                        //    oSegment.set_DataElementValue(3, 0, EData.ProviderLName);//Clinic or Organization Name
                        //    //oSegment.set_DataElementValue(4, 0, EData.ProviderFName);//
                        //    //oSegment.set_DataElementValue(5, 0, EData.ProviderMName);
                        //    oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number
                        //    oSegment.set_DataElementValue(9, 0, EData.ProviderNPI);//"0202034");//Clinic NPI
                        //}
                        //else
                        //{
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                            oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                            oSegment.set_DataElementValue(2, 0, "1");//1=Person
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderLName).Trim());//Provider  LastName
                            oSegment.set_DataElementValue(4, 0, FormatString(EData.ProviderFName).Trim());//Provider FirstName

                            if (FormatString(EData.ProviderMName).Trim() != "")
                            { oSegment.set_DataElementValue(5, 0, FormatString(EData.ProviderMName).Trim()); }

                            if (FormatString(EData.InsEligibilityProviderType).Trim() != "" && FormatString(EData.ProviderNPI).Trim() != "")
                            {
                                oSegment.set_DataElementValue(8, 0, FormatString(EData.InsEligibilityProviderType).Trim());//SV=Service Provider Number
                                oSegment.set_DataElementValue(9, 0, FormatString(EData.ProviderNPI).Trim());//"0202034");//Service Provider No
                            }
                        //}

                        //Provider Secondary ID & Secondary Type
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                            if (FormatString(EData.InsEligibilityProviSecType).Trim() != "" && FormatString(EData.InsEligibilityProvSecID).Trim() != "")
                            {
                                oSegment.set_DataElementValue(1, 0, FormatString(EData.InsEligibilityProviSecType).Trim());
                                oSegment.set_DataElementValue(2, 0, FormatString(EData.InsEligibilityProvSecID).Trim());
                            }
                            

                        //INFORMATION RECEIVER ADDRESS
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderAddress).Trim());
                        //oSegment.set_DataElementValue(2, 0, "1");

                        //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderCity).Trim());
                        oSegment.set_DataElementValue(2, 0, FormatString(EData.ProviderState).Trim());
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderZip).Trim());

                        #endregion " Receiver Loop "

                        #region " Subscriber Loop "


                        //SUBSCRIBER LEVEL
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "3");
                        oSegment.set_DataElementValue(2, 0, "2");
                        oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        {
                            oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure
                        }
                        else
                        {
                            oSegment.set_DataElementValue(4, 0, "1");
                        }


                        //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF" && EData.IsSubscriberCompany == true)
                        {
                            oSegment.set_DataElementValue(2, 0, "2"); //1=Person
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberCompanyname).Trim());//"Subscriber Last Name");//
                            oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                            oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, "1"); //1=Person

                            oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberLName).Trim());//"Subscriber Last Name");//
                            oSegment.set_DataElementValue(4, 0, FormatString(EData.SubscriberFName).Trim());//"Subscriber First Name");
                            if (FormatString(EData.SubscriberMName).Trim() != "")
                            {
                                oSegment.set_DataElementValue(5, 0, FormatString(EData.SubscriberMName).Trim());//"Subscriber Middle Name");//
                            }
                            oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                            oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");

                        }
                        //SUBSCRIBER ADDITIONAL IDENTIFICATION-REF
                        if (FormatString(EData.Group).Trim() != "")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "1L");
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                        }
                        //else if (EData.PatientSSN != "" && EData.PatientSubscriberRelationShip == "SELF")
                        //{
                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                        //    oSegment.set_DataElementValue(1, 0, "SY");//"SY");//1L=Group or Policy Number
                        //    oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.PatientSSN));
                        //}

                        //SUBSCRIBER ADDRESS
                        //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberAddressLn1).Trim());
                        if (FormatString(EData.PatientAddressLn2).Trim() != "")
                        {
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberAddressLn2).Trim());
                        }

                        //SUBSCRIBER CITY,STATE and ZIP
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberCity).Trim());//"City");
                        oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberState).Trim());//"State");
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberZip).Trim());//"ZIP");



                        //SUBSCRIBER PROVIDER INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\PRV"));
                        oSegment.set_DataElementValue(1, 0, "PE");//PC=Primary Care Physician PE=Performing
                        oSegment.set_DataElementValue(2, 0, "HPI");
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());

                        //SUBSCRIBER DEMOGRAPHIC INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                        oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                        //oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(mtxtDOB.Text.Trim())));//"Subscriber Date of Birth"); //Date of Birth
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.SubscriberDOB.Replace("*", " "))));//"Subscriber Date of Birth"); //Date of Birth
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberGender).Trim().ToUpper());//"Gender"); //Gender

                        ///////

                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        {

                            if (EData.SubscriberCardIssueDate == null || EData.SubscriberCardIssueDate == "")                             
                            {
                                //SUBSCRIBER DATE
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "307");
                                oSegment.set_DataElementValue(2, 0, "D8");
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, EData.SubscriberCardIssueDate.Trim());
                            }

                        }

                        //MaheshB
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                            oSegment.set_DataElementValue(1, 0, "30");
                        }

                        #endregion " Subscriber Loop "

                        #region Dependent Loop
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF")
                        {
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\HL"));
                            oSegment.set_DataElementValue(1, 0, "4");
                            oSegment.set_DataElementValue(2, 0, "3");
                            oSegment.set_DataElementValue(3, 0, "23");
                            oSegment.set_DataElementValue(4, 0, "0");

                            //DEPENDENT NAME
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\NM1"));
                            oSegment.set_DataElementValue(1, 0, "03");
                            oSegment.set_DataElementValue(2, 0, "1");
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientLName).Trim());
                            oSegment.set_DataElementValue(4, 0, FormatString(EData.PatientFName).Trim());
                            if (FormatString(EData.PatientMName).Trim() != "")
                            {
                                oSegment.set_DataElementValue(5, 0, FormatString(EData.PatientMName).Trim());
                            }

                            //DEPENDENT ADDITIONAL IDENTIFICATION
                            if (FormatString(Convert.ToString(EData.PatientSSN)) != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "SY");
                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientSSN)));
                            }
                            else if (FormatString(EData.Group).Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "6P");
                                oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "EJ");
                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientCode)));
                            }

                            //End code comment on 20100902 ,Sagar Ghodke

                            //DEPENDENT ADDRESS
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N3"));
                            oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientAddressLn1).Trim());
                            if (FormatString(EData.PatientAddressLn2).Trim() != "")
                            {
                                oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientAddressLn2).Trim());
                            }

                            //DEPENDENT CITY/STATE/ZIP CODE
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N4"));
                            oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientCity).Trim());
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientState).Trim());
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientZip).Trim());

                            //PROVIDER INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\PRV"));
                            oSegment.set_DataElementValue(1, 0, "PE");
                            oSegment.set_DataElementValue(2, 0, "HPI");
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());

                            //DEPENDENT DEMOGRAPHIC INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\DMG"));
                            oSegment.set_DataElementValue(1, 0, "D8");
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.PatientDOB.Trim())));
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientGender).Trim().ToUpper());

                            //MaheshB
                            ////DEPENDENT RELATIONSHIP 2010D
                            if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                            {
                                ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\INS"));
                                oSegment.set_DataElementValue(1, 0, "N");


                                if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SPOUSE")
                                { oSegment.set_DataElementValue(2, 0, "01"); }
                                else if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "CHILD")
                                { oSegment.set_DataElementValue(2, 0, "19"); }
                                else
                                { oSegment.set_DataElementValue(2, 0, "34"); }
                            }

                            //SUBSCRIBER DATE
                            if (FormatString(EData.SubscriberInsStartDate).Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "102");
                                oSegment.set_DataElementValue(2, 0, "D8");
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(EData.SubscriberInsStartDate).ToShortDateString())));
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                                oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour
                            }

                            //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\EQ\\EQ"));
                            oSegment.set_DataElementValue(1, 0, "30"); // "98");//Service Type Code, 98 = Professional Visit - Office
                        }

                        #endregion

                        #region  " Save EDI File "

                        oEdiDoc.Save(gloSettings.FolderSettings.AppTempFolderPath + sEdiFile);
                        EdiFile = gloSettings.FolderSettings.AppTempFolderPath  + sEdiFile;

                       
                       
                            if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                            {
                                ClsPostEDI objPostEDI = null;
                                 try
                                {
                                    objPostEDI = new ClsPostEDI();
                                    objPostEDI._FilePath = EdiFile;
                                    objPostEDI.EligibilityUserName =EData.EligibilityUserName;
                                    objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                                    objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                                    objPostEDI.SubmitterID=EData.SubmitterID;
                                    bIsServiceSucceed = objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), bIsInsuranceAdd);
                                    _response = objPostEDI.PostEDIResult;
                                }
                                 catch (Exception)// ex)
                                {
                                    if ((objPostEDI != null))
                                    {
                                        objPostEDI.Dispose();
                                        objPostEDI = null;
                                    }
                                    //ex.ToString();
                                    //ex = null;
                                }
                                finally
                                {
                                    if ((objPostEDI != null))
                                    {
                                        objPostEDI.Dispose();
                                        objPostEDI = null;
                                    }
                                }
                            }
                            else if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                            { 
                                ClsPostEDIRealMed objPostEDI = null;
                                try
                                {
                                    objPostEDI = new ClsPostEDIRealMed();
                                    objPostEDI._FilePath = EdiFile;
                                    objPostEDI.EligibilityUserName=EData.EligibilityUserName;
                                    objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                                    objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                                    objPostEDI.SubmitterID = EData.SubmitterID;
                                    bIsServiceSucceed= objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID));
                                    _response = objPostEDI.PostEDIResult;
                                }
                                catch (Exception)// ex)
                                {
                                    if ((objPostEDI != null))
                                    {
                                        objPostEDI.Dispose();
                                        objPostEDI = null;
                                    }
                                    //ex.ToString();
                                    //ex = null;
                                }
                                finally
                                {
                                    if ((objPostEDI != null))
                                    {
                                        objPostEDI.Dispose();
                                        objPostEDI = null;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                           
                       
                       

                        #endregion  " Save EDI File "
                    }
                }
            }
            catch (Exception ex)
            {
                bIsServiceSucceed = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (EData != null)
                { EData.Dispose(); }

                if (oSegment != null)
                {
                    oSegment.Dispose();
                }


                if (oInterchange != null)
                {
                    oInterchange.Dispose();
                }

                if (oGroup != null)
                {
                    oGroup.Dispose();
                }

                if (oTransactionset != null)
                {
                    oTransactionset.Dispose();
                }



                if (oSchema != null)
                {
                    oSchema.Dispose();
                }

                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }

                if (oEdiDoc != null)
                {
                    oEdiDoc.Dispose();
                }


            }
            return bIsServiceSucceed;
        }


        public void EDIGeneration_270(EiligiblityData EData, Int64 _InsuranceID, Int64 _ProviderID)
        {
            //EDIService.
            EDIService.EligibilityData OBJEDI = null;
            EDIService.gloWebEDI837 obj = null;
            gloSettings.GeneralSettings oSettings = null;
            try
            {
                if (ValidateDataEligibilityData(EData) == true)
                {

                    ////GetInsuranceEligibility
                    if (FormatString(EData.PatientContactInsID.Trim()) != null && FormatString(Convert.ToString(EData.PatientContactInsID)) != "" && FormatString(EData.PatientID.Trim()) != null &&
                        Convert.ToString(EData.PatientID) != "")
                    {
                        OBJEDI = new EDIService.EligibilityData();
                        OBJEDI.ClinicID = _ClinicID;
                        OBJEDI.ConnectionString = _databaseconnectionstring;
                        OBJEDI.ContactID = Convert.ToInt64(EData.PatientContactInsID);
                        OBJEDI.InsuranceID = _InsuranceID;
                        OBJEDI.PatientID = Convert.ToInt64(EData.PatientID);
                        OBJEDI.PayerID = EData.PayerID;
                        OBJEDI.ProviderID = _ProviderID;
                        OBJEDI.ClearingHouseType = EData.ClearingHouseType;

                        //EDIService.



                        obj = new EDIService.gloWebEDI837();
                        oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        Object _objResult = null;
                        string strEDIServiceURL = "";
                        oSettings.GetSetting("EDISERVICEPATH", out _objResult);
                        if (_objResult != null)
                        {
                            strEDIServiceURL = Convert.ToString(_objResult);
                        }

                        if (strEDIServiceURL.Trim() != "")
                        {
                            obj.Url = strEDIServiceURL.Trim();
                            OBJEDI = obj.GetInsuranceEligibility(OBJEDI);
                            if (OBJEDI.IsGenrated == true)//&& OBJEDI._gloEligibilities != null)
                            {
                                if (OBJEDI.Is997 == true)
                                {
                                    MessageBox.Show(Convert.ToString(OBJEDI.Message), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Save997File(OBJEDI.EDIFile997, OBJEDI.EDIFileTA1);
                                }
                                else
                                {
                                    frmEligibilityResponse ofrm = new frmEligibilityResponse(_databaseconnectionstring, true, OBJEDI.gloEligibilityResponse, OBJEDI.EDIID);
                                    ofrm.BringToFront();
                                    ofrm.ShowDialog(ofrm.Parent);
                                    //SLR: ofrm.dispose
                                    ofrm.Dispose();
                                }
                            }
                            else if (OBJEDI.IsGenrated == false && OBJEDI.Message != null && Convert.ToString(OBJEDI.Message) != "")
                            {
                                MessageBox.Show(Convert.ToString(OBJEDI.Message), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("EDI service path not found. ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if (_objResult != null)
                        {
                            _objResult = null;
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
                if (obj != null)
                {
                    obj.Dispose();
                }
                //SLR: Free objEDI, oSettings, objResult
                if (OBJEDI != null)
                {
                    OBJEDI = null;
                }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                }
                
            }

        }

        private bool ValidateDataEligibilityData(EiligiblityData EData)
        {
            bool _Validated = false;
            string strMissingText = "";
            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;  //AppDomain.CurrentDomain.BaseDirectory;
            string strMissingTextHeader = "";

            try
            {
                strMissingTextHeader = "Eligibility request not sent due to missing : " + Environment.NewLine + Environment.NewLine + "";

                if ((EData.SubscriberFName == null || FormatString(EData.SubscriberFName.Trim()) == "") && EData.IsSubscriberCompany==false)
                { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }
                if ((EData.SubscriberLName == null || FormatString(EData.SubscriberLName.Trim()) == "" )&& EData.IsSubscriberCompany == false)
                { strMissingText += "Subscriber Last Name" + Environment.NewLine + ""; }
                if ((EData.SubscriberDOB == null || FormatString(EData.SubscriberDOB.Trim()) == "") && EData.IsSubscriberCompany == false) { strMissingText += "Subscriber Date of Birth" + Environment.NewLine + ""; }
                if (EData.SubscriberGender == null || EData.SubscriberGender == "" && EData.IsSubscriberCompany == false) { strMissingText += "Subscriber Gender" + Environment.NewLine + ""; }
                if (EData.PayerName == null || FormatString(EData.PayerName.Trim()) == "") { strMissingText += "Payer/Insurance Name" + Environment.NewLine + ""; }
                if (EData.SubscriberID == null || FormatString(EData.SubscriberID.Trim()) == "") { strMissingText += "Insurance ID" + Environment.NewLine + ""; }
                if (EData.PayerID == null || FormatString(EData.PayerID.Trim()) == "") { strMissingText += "Payer/Insurance ID" + Environment.NewLine + ""; }


                //if (EData.ProviderSettingValue.Trim().ToUpper() == "CLINIC")
                //{
                //    if (EData.ProviderLName == null || EData.ProviderLName == "") { strMissingText += "Clinic name" + Environment.NewLine + ""; }
                //    if (EData.ProviderCity == null || EData.ProviderCity == "") { strMissingText += "Clinic City" + Environment.NewLine + ""; }
                //    if (EData.ProviderNPI == null || EData.ProviderNPI == "") { strMissingText += "Clinic NPI" + Environment.NewLine + ""; }
                //    if (EData.ProviderState == null || EData.ProviderState == "") { strMissingText += "Clinic State" + Environment.NewLine + ""; }
                //    if (EData.ProviderZip == null || EData.ProviderZip == "") { strMissingText += "Clinic Zip" + Environment.NewLine + ""; }
                //    if (EData.ProviderAddress == null || EData.ProviderAddress == "") { strMissingText += "Clinic Address" + Environment.NewLine + ""; }
                //}
                //else
                //{
                    if (EData.ProviderFName == null || EData.ProviderFName == "") { strMissingText += "Provider First Name" + Environment.NewLine + ""; }
                    if (EData.ProviderLName == null || EData.ProviderLName == "") { strMissingText += "Provider Last Name" + Environment.NewLine + ""; }
                    if (EData.ProviderCity == null || EData.ProviderCity == "") { strMissingText += "Provider City" + Environment.NewLine + ""; }
                    if (EData.ProviderNPI == null || EData.ProviderNPI == "") { strMissingText += "Primary Eligibility ID" + Environment.NewLine + ""; }
                    if (EData.ProviderState == null || EData.ProviderState == "") { strMissingText += "Provider State" + Environment.NewLine + ""; }
                    if (EData.ProviderZip == null || EData.ProviderZip == "") { strMissingText += "Provider Zip" + Environment.NewLine + ""; }
                    if (EData.ProviderAddress == null || EData.ProviderAddress == "") { strMissingText += "Provider Address" + Environment.NewLine + ""; }
                //}




                
                //if (EData.SubscriberFName == null || EData.SubscriberFName.Trim().Replace("*", "").Replace("~", "").Replace(":", "") == "") { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }

                //if (EData.SubscriberDOB == null || EData.SubscriberDOB.Trim().Replace("*", "").Replace("~","").Replace(":","") == "") { strMissingText += "Subscriber DOB " + Environment.NewLine + ""; }

                //if (EData.Group == null || EData.Group.Trim().Replace("*", "").Replace("~","").Replace(":","") == "") { strMissingText += " Group " + Environment.NewLine + ""; }

                //if (EData.Group != null && EData.Group.Trim().Replace("*", "").Replace("~","").Replace(":","") != "" && EData.Group.Trim().Replace("*", "").Replace("~","").Replace(":","").IndexOfAny(new char[] { '*', ':', '!', '@' }) > -1)
                //{
                //    MessageBox.Show("Invalid values of Group.  ");
                //    return false;
                //}

                if (EData.PatientSubscriberRelationShip == null || EData.PatientSubscriberRelationShip.ToUpper() != "SELF")
                {
                    if (EData.PatientLName == null || EData.PatientLName == "") { strMissingText += "Patient Last Name" + Environment.NewLine + ""; }
                    if (EData.PatientDOB == null || EData.PatientDOB == "") { strMissingText += "Patient Date of Birth" + Environment.NewLine + ""; }
                    if (EData.PatientAddressLn1 + EData.PatientAddressLn2 == "") { strMissingText += "Patient Address" + Environment.NewLine + ""; }
                    if (EData.PatientCity == null || EData.PatientCity == "") { strMissingText += "Patient City" + Environment.NewLine + ""; }
                    if (EData.PatientState == null || EData.PatientState == "") { strMissingText += "Patient State" + Environment.NewLine + ""; }
                    if (EData.PatientZip == null || EData.PatientZip == "") { strMissingText += "Patient Zip" + Environment.NewLine + ""; }
                }


                if (FormatString(strMissingText.Trim()) != "")
                {
                    _Validated = false;
                    _FilePath = _FilePath + "270Validation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(strMissingTextHeader + strMissingText);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);
                }
                else
                { _Validated = true; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _Validated;
        }

        public Int64 GetPatientProviderID(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _providerid = 0;
            Object _retVal = null;

            try
            {

                if (PatientId > 0)  //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                {
                    _sqlQuery = " SELECT ISNULL(Patient.nProviderID, 0) AS nProviderID " +
                                " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " +
                                " WHERE (Patient.nPatientID = " + PatientId + ") AND  (Patient.nClinicID = " + ClinicID + " ) ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null)
                    { _providerid = Convert.ToInt64(_retVal); }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _providerid = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _providerid = 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //SLR: Free retval and then
                if (_retVal != null) {_retVal = null; }
            }

            return _providerid;
        }

        public Int64 GetInsuranceContactID(Int64 PatientInsuranceId,Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _contactid = 0;
            Object _retVal = null;

            try
            {

                if (PatientId > 0) //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                {
                    _sqlQuery = " SELECT ISNULL(nContactID,0) AS ContactID FROM PatientInsurance_DTL " +
                                " WHERE nInsuranceID = " + PatientInsuranceId + " AND nPatientID = " + PatientId + " ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();
                }

                if (_retVal != null && Convert.ToString(_retVal) != "")
                { _contactid = Convert.ToInt64(_retVal); }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _contactid = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _contactid = 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //SLR: Free retval and then
                if (_retVal != null) { _retVal = null; }
            }

            return _contactid;
        }

        public DataTable GetPatientInsuranceID(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dt = null;

            try
            {

                if (PatientId > 0) //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                {
                    _sqlQuery = " SELECT PatientInsurance_DTL.nContactID as nContactID, ISnull(PatientInsurance_DTL.nInsuranceID,0) as nPatientInsuranceID " +
                                " FROM         PatientInsurance_DTL INNER JOIN " +
                                " Contacts_MST ON PatientInsurance_DTL.nContactID = Contacts_MST.nContactID " +
                                " WHERE PatientInsurance_DTL.nPatientID = " + PatientId + " and Isnull(nInsuranceFlag,0) = 1 ";

                    oDB.Connect(false);
                    //SLR: Allocate memory for _dt
                    _dt = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out _dt);
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dt;
        }

        public string GetAlternateProvider(Int64 nProviderId,Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;            
            DataTable _dtInsurance = null;
            string _result = "";
            try
            {
                oDB.Connect(false);
                _sqlquery = "Select Case when ISNULL(sInsEligibilityProviderID,'') ='' then " +
                            "(Select top 1 ISNULL(sSettingsValue,'') as sSettingsValue from Settings where sSettingsname ='Eligibility Request Provider ID') " +
                            " else ISNULL(sInsEligibilityProviderID,'') end  " +
                            " as NPI from Contacts_Insurance_DTL where nContactID=" + ContactID + "";
                //SLR: Allocate memory for dtInsurance?
                _dtInsurance = new DataTable();
                oDB.Retrive_Query(_sqlquery, out _dtInsurance);
                if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                {
                    _result = Convert.ToString(_dtInsurance.Rows[0]["NPI"]);
                }
                if (_result.Trim() == "")
                {
                    _sqlquery = "Select ISNULL(sNPI,'') AS NPI FROM Provider_Mst WHERE nProviderID = " + nProviderId;
                    oDB.Retrive_Query(_sqlquery, out _dtInsurance);
                    if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                    {
                        _result = Convert.ToString(_dtInsurance.Rows[0]["NPI"]);
                    }
                }
               
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //SLR: FRee dtInsurance
                if (_dtInsurance != null)
                {
                    _dtInsurance.Dispose();
                }
            }
            return _result;
        }

        public void Save997File(string _s997,string _sTA1)
        {
            System.IO.StreamWriter oWriter=null;
            string sEdiFile = String.Empty;

            try
            {
                if (_s997 != null && Convert.ToString(_s997.Trim()) != "")
                {
                    sEdiFile = appSettings["StartupPath"].ToString() + "\\" + "997.txt";
                    oWriter = new System.IO.StreamWriter(sEdiFile);
                    oWriter.Write(_s997.Replace("\n", Environment.NewLine));
                    oWriter.Flush();
                    oWriter.Close();
                }
                else if (_sTA1 != null && Convert.ToString(_sTA1.Trim()) != "")
                {
                    sEdiFile = appSettings["StartupPath"].ToString() + "\\" + "TA1.txt";
                    oWriter = new System.IO.StreamWriter(sEdiFile);
                    oWriter.Write(_s997.Replace("\n", Environment.NewLine));
                    oWriter.Flush();
                    oWriter.Close();
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oWriter != null)
                {
                    oWriter.Dispose();
                }
            }
        }

        private string FormatString(string _Value)
        {
            string _result = "";
            if (_Value != null && _Value != "")
            {
                _result = Convert.ToString(_Value).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "");
            }
            return _result;
        }


        #region "ANSI VERSION 5010"

        public bool EDI5010Generation_270(EiligiblityData EData,gloSettings.ANSIVersions ANSIVersion,bool bIsInsuranceAdd=false)
        {
            string _TypeOfData = "T";
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";
            string EdiFile = "";
            string _response = string.Empty;

            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            string sReceiverQualifier = "ZZ";
            string sSenderQualifier = "ZZ";
            bool bIsServiceSucceed = false;
            try
            {
                if (LoadEDI5010Object() == true)
                {
                    oEdiDoc.SegmentTerminator = "~\r\n";
                    oEdiDoc.ElementTerminator = "*";
                    oEdiDoc.CompositeTerminator = ":";

                    if (EData != null && ValidateDataEligibilityData(EData) == true)
                    {

                        #region " Interchange Segment "
                        //Create the interchange segment
                        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "005010"));//
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                        if (FormatString(Convert.ToString(EData.ClearingHouseTypeOfData)).Trim() == "0" || FormatString(Convert.ToString(EData.ClearingHouseTypeOfData)).Trim() == "1")
                        { _TypeOfData = "T"; }
                        else if (FormatString(Convert.ToString(EData.ClearingHouseTypeOfData)).Trim() == "2")
                        { _TypeOfData = "P"; }

                        if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                        {
                            _TypeOfData = "P";
                        }

                        //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        if (EData.SenderQualifier != "")
                        { sSenderQualifier = EData.SenderQualifier; }

                        if (EData.ReceiverQualifier != "")
                        { sReceiverQualifier = EData.ReceiverQualifier; }

                        oSegment.set_DataElementValue(1, 0, "00");
                        oSegment.set_DataElementValue(3, 0, "00");
                        oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        oSegment.set_DataElementValue(6, 0, FormatString(EData.ClearingHouseSubmitterID).Trim());
                        oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                        oSegment.set_DataElementValue(8, 0, FormatString(EData.ClearingHouseReceiverID).Trim());
                        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                        oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));
                        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                        oSegment.set_DataElementValue(11, 0, "^");
                        oSegment.set_DataElementValue(12, 0, "00501");//
                        InterchangeHeader = ControlNumberGeneration("1");
                        oSegment.set_DataElementValue(13, 0, FormatString(InterchangeHeader).Trim());
                        oSegment.set_DataElementValue(14, 0, "1");
                        oSegment.set_DataElementValue(15, 0, _TypeOfData);
                        oSegment.set_DataElementValue(16, 0, ":");

                        #endregion " Interchange Segment "

                        #region " Functional Group "

                        //Create the functional group segment
                        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("005010X279A1"));//("004010X092A1"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                        oSegment.set_DataElementValue(1, 0, "HS");
                        oSegment.set_DataElementValue(2, 0, FormatString(EData.ClearingHouseSubmitterID).Trim());
                        oSegment.set_DataElementValue(3, 0,  FormatString(EData.ClearingHouseReceiverID).Trim());
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                        FunctionalGroupHeader = ControlNumberGeneration("2");
                        oSegment.set_DataElementValue(6, 0, FormatString(FunctionalGroupHeader).Trim());
                        //oSegment.set_DataElementValue(6, 0, "1");
                        oSegment.set_DataElementValue(7, 0, "X");
                        oSegment.set_DataElementValue(8, 0, "005010X279A1");//

                        #endregion " Functional Group "

                        #region "Transaction Set "
                        //HEADER
                        //ST TRANSACTION SET HEADER
                        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                        TransactionSetHeader = ControlNumberGeneration("3");
                        oSegment.set_DataElementValue(2, 0, FormatString(TransactionSetHeader).Trim());
                        oSegment.set_DataElementValue(3, 0, "005010X279A1");//New Segment-Implementation Convention Reference

                        #endregion "Transaction Set "

                        #region " BHT "

                        //Begining Segment 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                        oSegment.set_DataElementValue(1, 0, "0022");
                        oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request,01=Cancellation
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
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.PayerName).Trim());
                        oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                        oSegment.set_DataElementValue(9, 0, FormatString(EData.PayerID).Trim());//"77710");//PayerID

                        #endregion " Information Source "

                        #region " Receiver Loop "

                        //INFORMATION RECEIVER LEVEL
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "2");
                        oSegment.set_DataElementValue(2, 0, "1");
                        oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                        oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                        //INFORMATION RECEIVER NAME (It is the medical service Provider)

                        //if (EData.ProviderSettingValue.Trim().ToUpper() == "CLINIC")
                        //{
                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        //    oSegment.set_DataElementValue(1, 0, "1P");//FA=Facility
                        //    oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                        //    oSegment.set_DataElementValue(3, 0, EData.ProviderLName);//Clinic or Organization Name
                        //    //oSegment.set_DataElementValue(4, 0, EData.ProviderFName);//
                        //    //oSegment.set_DataElementValue(5, 0, EData.ProviderMName);
                        //    oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number
                        //    oSegment.set_DataElementValue(9, 0, EData.ProviderNPI);//"0202034");//Clinic NPI
                        //}
                        //else
                        //{
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                        oSegment.set_DataElementValue(2, 0, "1");//1=Person
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderLName).Trim());//Provider  LastName
                        oSegment.set_DataElementValue(4, 0, FormatString(EData.ProviderFName).Trim());//Provider FirstName

                        if (FormatString(EData.ProviderMName).Trim() != "")
                        { oSegment.set_DataElementValue(5, 0, FormatString(EData.ProviderMName).Trim()); }
                        if (FormatString(EData.InsEligibilityProviderType).Trim() != "" && FormatString(EData.ProviderNPI).Trim() != "")
                        {
                            oSegment.set_DataElementValue(8, 0,FormatString(EData.InsEligibilityProviderType).Trim());//SV=Service Provider Number
                            oSegment.set_DataElementValue(9, 0,FormatString(EData.ProviderNPI).Trim());//"0202034");//Service Provider No
                        }
                        //}

                        //Provider Secondary ID & Secondary Type
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        if (FormatString(EData.InsEligibilityProviSecType).Trim() != "" && FormatString(EData.InsEligibilityProvSecID).Trim()!= "")
                        {
                            oSegment.set_DataElementValue(1, 0, FormatString(EData.InsEligibilityProviSecType).Trim());
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.InsEligibilityProvSecID).Trim());
                        }

                        //INFORMATION RECEIVER ADDRESS
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderAddress).Trim());
                        //oSegment.set_DataElementValue(2, 0, "1");

                        //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderCity).Trim());
                        oSegment.set_DataElementValue(2, 0, FormatString(EData.ProviderState).Trim());
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderZip).Trim());

                        #endregion " Receiver Loop "

                        #region " Subscriber Loop "


                        //SUBSCRIBER LEVEL
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "3");
                        oSegment.set_DataElementValue(2, 0, "2");
                        oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        {
                            oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure
                        }
                        else
                        {
                            oSegment.set_DataElementValue(4, 0, "1");
                        }


                        //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                        oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF" && EData.IsSubscriberCompany == true)
                        {
                            oSegment.set_DataElementValue(2, 0, "2"); //1=Person
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberCompanyname).Trim());//"Subscriber Last Name");//
                            oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                            oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");

                        }
                        else
                        {

                            oSegment.set_DataElementValue(2, 0, "1"); //1=Person

                            oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberLName).Trim());//"Subscriber Last Name");//
                            oSegment.set_DataElementValue(4, 0, FormatString(EData.SubscriberFName).Trim());//"Subscriber First Name");
                            if (EData.SubscriberMName != "")
                            {
                                oSegment.set_DataElementValue(5, 0, FormatString(EData.SubscriberMName).Trim());//"Subscriber Middle Name");//
                            }
                            oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                            oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");
                        }

                        //SUBSCRIBER ADDITIONAL IDENTIFICATION-REF
                        if (FormatString(EData.Group).Trim() != "")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "1L");
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                        }
                        //else if (EData.PatientSSN != "" && EData.PatientSubscriberRelationShip == "SELF")
                        //{
                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                        //    oSegment.set_DataElementValue(1, 0, "SY");//"SY");//1L=Group or Policy Number
                        //    oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.PatientSSN));
                        //}

                        //SUBSCRIBER ADDRESS
                        //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberAddressLn1).Trim());
                        if (FormatString(EData.PatientAddressLn2).Trim() != "")
                        {
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberAddressLn2).Trim());
                        }

                        //SUBSCRIBER CITY,STATE and ZIP
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberCity).Trim());//"City");
                        oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberState).Trim());//"State");
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberZip).Trim());//"ZIP");



                        //SUBSCRIBER PROVIDER INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\PRV"));
                        oSegment.set_DataElementValue(1, 0, "PE");//PC=Primary Care Physician PE=Performing
                        oSegment.set_DataElementValue(2, 0, "HPI");
                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());
                        if (EData.IsSubscriberCompany == false)
                        {
                            //SUBSCRIBER DEMOGRAPHIC INFORMATION
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                            oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                            //oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(mtxtDOB.Text.Trim())));//"Subscriber Date of Birth"); //Date of Birth
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.SubscriberDOB.Replace("*", " "))));//"Subscriber Date of Birth"); //Date of Birth
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberGender).Trim().ToUpper());//"Gender"); //Gender
                        }
                        ///////

                        //if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        //{

                            //if (EData.SubscriberCardIssueDate == null || EData.SubscriberCardIssueDate.Trim() == "")
                            //{
                            //    //SUBSCRIBER DATE
                            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                            //    oSegment.set_DataElementValue(1, 0, "307");
                            //    oSegment.set_DataElementValue(2, 0, "D8");
                            //    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                            //}
                            //else
                            //{
                            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                            //    oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                            //    oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                            //    oSegment.set_DataElementValue(3, 0, EData.SubscriberCardIssueDate.Trim());
                            //}

                       // }

                        //MaheshB
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                            oSegment.set_DataElementValue(1, 0, "30");
                        }

                        #endregion " Subscriber Loop "

                        #region Dependent Loop
                        if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF")
                        {
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\HL"));
                            oSegment.set_DataElementValue(1, 0, "4");
                            oSegment.set_DataElementValue(2, 0, "3");
                            oSegment.set_DataElementValue(3, 0, "23");
                            oSegment.set_DataElementValue(4, 0, "0");

                            //DEPENDENT NAME
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\NM1"));
                            oSegment.set_DataElementValue(1, 0, "03");
                            oSegment.set_DataElementValue(2, 0, "1");
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientLName).Trim());
                            oSegment.set_DataElementValue(4, 0, FormatString(EData.PatientFName).Trim());
                            if (EData.PatientMName.Trim() != "")
                            {
                                oSegment.set_DataElementValue(5, 0,FormatString(EData.PatientMName).Trim());
                            }

                            //DEPENDENT ADDITIONAL IDENTIFICATION
                            if (FormatString(Convert.ToString(EData.PatientSSN))!= "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "SY");
                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientSSN)));
                            }
                            else if (FormatString(EData.Group).Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "6P");
                                oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                            }
                            else
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                oSegment.set_DataElementValue(1, 0, "EJ");
                                oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientCode)));
                            }

                            //End code comment on 20100902 ,Sagar Ghodke

                            //DEPENDENT ADDRESS
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N3"));
                            oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientAddressLn1).Trim());
                            if (FormatString(EData.PatientAddressLn2).Trim() != "")
                            {
                                oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientAddressLn2).Trim());
                            }

                            //DEPENDENT CITY/STATE/ZIP CODE
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N4"));
                            oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientCity).Trim());
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientState).Trim());
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientZip).Trim());

                            //PROVIDER INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\PRV"));
                            oSegment.set_DataElementValue(1, 0, "PE");
                            oSegment.set_DataElementValue(2, 0, "HPI");
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());

                            //DEPENDENT DEMOGRAPHIC INFORMATION
                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\DMG"));
                            oSegment.set_DataElementValue(1, 0, "D8");
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.PatientDOB.Trim())));
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientGender.Trim().ToUpper()));

                            //MaheshB
                            ////DEPENDENT RELATIONSHIP 2010D
                            if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                            {
                                ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\INS"));
                                oSegment.set_DataElementValue(1, 0, "N");


                                if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SPOUSE")
                                { oSegment.set_DataElementValue(2, 0, "01"); }
                                else if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "CHILD")
                                { oSegment.set_DataElementValue(2, 0, "19"); }
                                else
                                { oSegment.set_DataElementValue(2, 0, "34"); }
                            }

                            //SUBSCRIBER DATE
                            if (FormatString(EData.SubscriberInsStartDate).Trim() != "")
                            {
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                oSegment.set_DataElementValue(1, 0, "102");
                                oSegment.set_DataElementValue(2, 0, "D8");
                                oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(EData.SubscriberInsStartDate).ToShortDateString())));
                            }
                            //else
                            //{
                            //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                            //    oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                            //    oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                            //    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour
                            //}

                            //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\EQ\\EQ"));
                            oSegment.set_DataElementValue(1, 0, "30"); // "98");//Service Type Code, 98 = Professional Visit - Office
                        }

                        #endregion

                        #region  " Save EDI File "

                        oEdiDoc.Save(gloSettings.FolderSettings.AppTempFolderPath + sEdiFile);
                        EdiFile = gloSettings.FolderSettings.AppTempFolderPath + sEdiFile;



                        if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                        {
                            ClsPostEDI objPostEDI = null;
                            try
                            {
                                objPostEDI = new ClsPostEDI();
                                objPostEDI._FilePath = EdiFile;
                                objPostEDI.EligibilityUserName = EData.EligibilityUserName;
                                objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                                objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                                objPostEDI.SubmitterID = EData.SubmitterID;
                                bIsServiceSucceed = objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), bIsInsuranceAdd);

                                //objPostEDI.Post5010EDI(EData.PayerID, EData.PayerName, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), EData.ContactID, ANSIVersion);

                                _response = objPostEDI.PostEDIResult;
                            }
                            catch (Exception)// ex)
                            {
                                if ((objPostEDI != null))
                                {
                                    objPostEDI.Dispose();
                                    objPostEDI = null;
                                }
                                //ex.ToString();
                                //ex = null;
                            }
                            finally
                            {
                                if ((objPostEDI != null))
                                {
                                    objPostEDI.Dispose();
                                    objPostEDI = null;
                                }
                            }
                        }
                        else if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                        {
                            ClsPostEDIRealMed objPostEDI = null;
                            try
                            {
                                objPostEDI = new ClsPostEDIRealMed();
                                objPostEDI._FilePath = EdiFile;
                                objPostEDI.EligibilityUserName = EData.EligibilityUserName;
                                objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                                objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                                objPostEDI.SubmitterID = EData.SubmitterID;
                                bIsServiceSucceed = objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), bIsInsuranceAdd);
                                _response = objPostEDI.PostEDIResult;
                            }
                            catch (Exception)// ex)
                            {
                                if ((objPostEDI != null))
                                {
                                    objPostEDI.Dispose();
                                    objPostEDI = null;
                                }
                                //ex.ToString();
                                //ex = null;
                            }
                            finally
                            {
                                if ((objPostEDI != null))
                                {
                                    objPostEDI.Dispose();
                                    objPostEDI = null;
                                }
                            }
                        }
                        else
                        {
                            //MessageBox.Show("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time. Verify Clearinghouse parameters. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Clearinghouse parameters are missing. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        #endregion  " Save EDI File "
                    }
                }
            }
            catch (Exception ex)
            {
                bIsServiceSucceed = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (EData != null)
                { EData.Dispose(); }

                if (oSegment != null)
                {
                    oSegment.Dispose();
                }


                if (oInterchange != null)
                {
                    oInterchange.Dispose();
                }

                if (oGroup != null)
                {
                    oGroup.Dispose();
                }

                if (oTransactionset != null)
                {
                    oTransactionset.Dispose();
                }



                if (oSchema != null)
                {
                    oSchema.Dispose();
                }

                if (oSchemas != null)
                {
                    oSchemas.Dispose();
                }

                if (oEdiDoc != null)
                {
                    oEdiDoc.Dispose();
                }


            }
            return bIsServiceSucceed;

        }
        //270_005010X279A1.SemRef.SEF

        private bool LoadEDI5010Object()
        {
            bool _retValue = true;
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "270_005010X279A1.SemRef.SEF";
                sEdiFile = "270OUTPUT.x12";
                //oEdiDoc = new ediDocument();
                //oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                //oSchemas = oEdiDoc.GetSchemas();
                //oSchemas.EnableStandardReference = false;

                //oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);
                //System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                //if (ofile.Exists == false)
                //{
                //    MessageBox.Show("SEF file is not present in the base directory.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    _retValue = false;
                //}
                //oEdiDoc.ImportSchema(sPath + sSEFFile, 0);



                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument()); //SLR: when is the newly allocated edidocument freeed: Please resfer any guideline provided by library?
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (!System.IO.File.Exists(sPath + sSEFFile))
                {
                    MessageBox.Show("SEF file is not present in the base directory.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _retValue = false;
                }

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));

                //ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _retValue = false;
            }

            return _retValue;
        }

        #endregion

        # region "Batch Eligibility" 

            #region " 270 File Generation "

            public List<EiligiblityData> getBatchEligibilityPatientList(out DataTable dtPatientIDs)
            {
                List<EiligiblityData> objEiligiblityData = new List<EiligiblityData>();
                EiligiblityData EData = null;
                dtPatientIDs = null;

                try
                {
                    dtPatientIDs = getPatientList();
                    if (dtPatientIDs != null)
                    {
                        for (int _Count = 0; _Count < dtPatientIDs.Rows.Count; _Count++)
                        {
                            if (Convert.ToString(dtPatientIDs.Rows[_Count]["nPatientID"]) != "" && Convert.ToInt64(dtPatientIDs.Rows[_Count]["nPatientID"]) != 0)
                            {
                                Int64 _patientId = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nPatientID"]);
                                Int64 _patientProviderid = 0;//GetPatientProviderID(_patientId);
                                ////Int64 _insuranceContactid = GetInsuranceContactID(_patientinsuranceId, _patientId);
                                Int64 _patientinsuranceId = 0;
                                Int64 _ContactId = 0;
                                
                                if (Convert.ToString(dtPatientIDs.Rows[_Count]["nContactID"]) != "")
                                {
                                    _ContactId = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nContactID"]);
                                }
                                if (Convert.ToString(dtPatientIDs.Rows[_Count]["nPatientInsuranceID"]) != "")
                                {
                                    _patientinsuranceId = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nPatientInsuranceID"]);
                                }
                                if (Convert.ToString(dtPatientIDs.Rows[_Count]["nProviderID"]) != "")
                                {
                                    _patientProviderid = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nProviderID"]);
                                }
                                EData=null;
                                EData = GetEiligibilityData(_patientId, _patientProviderid, _patientinsuranceId , _ContactId);
                                if (EData != null)
                                {
                                    EData.BatchPatientID = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nBatchPatientID"]);
                                    EData.BatchDetailID = Convert.ToInt64(dtPatientIDs.Rows[_Count]["nBatchDetailID"]);
                                    EData.ContactID = _ContactId;
                                    objEiligiblityData.Add(EData);
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
                }
                return objEiligiblityData;
            }

            public DataTable getPatientList()
            {
                gloDatabaseLayer.DBLayer oDB = null;
                DataTable dtPatient = null;

                try
                {
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    dtPatient = new DataTable();
                    oDB.Retrive("EligibilityPatientList", out dtPatient);
                    oDB.Disconnect();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }
                return dtPatient;
            }


            public  void  ShowEligibility()
            {
                DataTable dtPatientIDs = null;
                List<EiligiblityData> objList = getBatchEligibilityPatientList(out dtPatientIDs);
                //SLR: Free DtPatientIDs and then
                if (dtPatientIDs != null)
                {
                    dtPatientIDs.Dispose();
                }
                string _result = EDI5010GenerationBatch_270(objList, out dtPatientIDs);
                //SLR: Free DtPatientIDs
                if (dtPatientIDs != null)
                {
                    dtPatientIDs.Dispose();
                }
                string _FilePath=AppDomain.CurrentDomain.BaseDirectory + "\\BatchEligibility.txt";
                System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                oStreamWriter.WriteLine(_result);
                oStreamWriter.Close();
                oStreamWriter.Dispose();
                System.Diagnostics.Process.Start(_FilePath);

            }

            public string EDI5010GenerationBatch_270(List<EiligiblityData> ListEData, out DataTable dtResultIDs)
            {
                string _TypeOfData = "T";
                string InterchangeHeader = "";
                string FunctionalGroupHeader = "";
                string TransactionSetHeader = "";
                EiligiblityData EData=null; // = new EiligiblityData(); //SLR: new not needed
                string _result=String.Empty;
                DataRow _dr = null;
                dtResultIDs = new DataTable();

                //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                string sReceiverQualifier = "ZZ";
                string sSenderQualifier = "ZZ";

                try
                {
                    if (LoadEDI5010Object() == true)
                    {
                        dtResultIDs.Columns.Add("nPatientID");
                        dtResultIDs.Columns.Add("nBatchPatientID");
                        dtResultIDs.Columns.Add("nContactID");
                        dtResultIDs.Columns.Add("nBatchDetailID");
                        dtResultIDs.AcceptChanges();

                        oEdiDoc.SegmentTerminator = "~\r\n";
                        oEdiDoc.ElementTerminator = "*";
                        oEdiDoc.CompositeTerminator = ":";

                        //Validation logic Ask Shannon. Exclude that patient or what? Yes

                        if (ListEData != null && ListEData.Count>0)
                        {
                            EData = ListEData[0];
                        }
                        for (int _count = ListEData.Count-1; _count >= 0; _count--)
                        {
                            if (ListEData != null && ListEData[_count] != null && ValidateBatchEligibilityData(ListEData[_count]) == false)
                            {
                                //break;
                                //return "";
                                ListEData.Remove(ListEData[_count]);
                            }
                        }

                        if (EData != null && ListEData != null && ListEData.Count > 0)
                        {

                            #region " Interchange Segment "
                            //Create the interchange segment
                            ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "005010"));//
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                            if (FormatString(EData.ClearingHouseTypeOfData.ToString()).Trim() == "0" || FormatString(EData.ClearingHouseTypeOfData.ToString()).Trim() == "1")
                            { _TypeOfData = "T"; }
                            else if (FormatString(EData.ClearingHouseTypeOfData.ToString()).Trim() == "2")
                            { _TypeOfData = "P"; }

                            if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                            {
                                _TypeOfData = "P";
                            }

                            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                            if (EData.SenderQualifier != "")
                            { sSenderQualifier = EData.SenderQualifier; }

                            if (EData.ReceiverQualifier != "")
                            { sReceiverQualifier = EData.ReceiverQualifier; }

                            oSegment.set_DataElementValue(1, 0, "00");
                            oSegment.set_DataElementValue(3, 0, "00");
                            oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                            oSegment.set_DataElementValue(6, 0, FormatString(EData.ClearingHouseSubmitterID).Trim());
                            oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                            oSegment.set_DataElementValue(8, 0, FormatString(EData.ClearingHouseReceiverID).Trim());
                            string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                            oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));
                            string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                            oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                            oSegment.set_DataElementValue(11, 0, "^");
                            oSegment.set_DataElementValue(12, 0, "00501");//
                            InterchangeHeader = ControlNumberGeneration("1");
                            oSegment.set_DataElementValue(13, 0, FormatString(InterchangeHeader).Trim());
                            oSegment.set_DataElementValue(14, 0, "1");
                            oSegment.set_DataElementValue(15, 0, _TypeOfData);
                            oSegment.set_DataElementValue(16, 0, ":");

                            #endregion " Interchange Segment "

                            #region " Functional Group "

                            //Create the functional group segment
                            ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("005010X279A1"));//("004010X092A1"));
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                            oSegment.set_DataElementValue(1, 0, "HS");
                            oSegment.set_DataElementValue(2, 0, FormatString(EData.ClearingHouseSubmitterID).Trim());
                            oSegment.set_DataElementValue(3, 0, FormatString(EData.ClearingHouseReceiverID).Trim());
                            oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                            string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                            oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                            FunctionalGroupHeader = ControlNumberGeneration("2");
                            oSegment.set_DataElementValue(6, 0, FormatString(FunctionalGroupHeader).Trim());
                            //oSegment.set_DataElementValue(6, 0, "1");
                            oSegment.set_DataElementValue(7, 0, "X");
                            oSegment.set_DataElementValue(8, 0, "005010X279A1");//

                            #endregion " Functional Group "

                            for (int _Counter = 0; _Counter < ListEData.Count; _Counter++)
                            {
                            
                                EData = null;
                                EData = ListEData[_Counter];
                                if (EData != null)
                                {
                                    #region " Transaction Set "

                                        //HEADER
                                        //ST TRANSACTION SET HEADER
                                        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                                        TransactionSetHeader = ControlNumberGeneration(true);
                                        oSegment.set_DataElementValue(2, 0, FormatString(TransactionSetHeader).Trim());
                                        oSegment.set_DataElementValue(3, 0, "005010X279A1");//New Segment-Implementation Convention Reference

                                    #endregion "Transaction Set "

                                    #region " BHT "

                                    //Begining Segment 
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                                    oSegment.set_DataElementValue(1, 0, "0022");
                                    oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request,01=Cancellation
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
                                    oSegment.set_DataElementValue(3, 0, FormatString(EData.PayerName).Trim());
                                    oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                                    oSegment.set_DataElementValue(9, 0, FormatString(EData.PayerID).Trim());//"77710");//PayerID

                                    #endregion " Information Source "

                                    #region " Receiver Loop "

                                    //INFORMATION RECEIVER LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    oSegment.set_DataElementValue(1, 0, "2");
                                    oSegment.set_DataElementValue(2, 0, "1");
                                    oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                                    oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                                    //INFORMATION RECEIVER NAME (It is the medical service Provider)

                                    //if (EData.ProviderSettingValue.Trim().ToUpper() == "CLINIC")
                                    //{
                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    //    oSegment.set_DataElementValue(1, 0, "1P");//FA=Facility
                                    //    oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                                    //    oSegment.set_DataElementValue(3, 0, EData.ProviderLName);//Clinic or Organization Name
                                    //    //oSegment.set_DataElementValue(4, 0, EData.ProviderFName);//
                                    //    //oSegment.set_DataElementValue(5, 0, EData.ProviderMName);
                                    //    oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number
                                    //    oSegment.set_DataElementValue(9, 0, EData.ProviderNPI);//"0202034");//Clinic NPI
                                    //}
                                    //else
                                    //{
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                                    oSegment.set_DataElementValue(2, 0, "1");//1=Person
                                    oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderLName).Trim());//Provider  LastName
                                    oSegment.set_DataElementValue(4, 0, FormatString(EData.ProviderFName).Trim());//Provider FirstName

                                    if (FormatString(EData.ProviderMName).Trim() != "")
                                    { oSegment.set_DataElementValue(5, 0, FormatString(EData.ProviderMName).Trim()); }
                                    if (FormatString(EData.InsEligibilityProviderType).Trim() != "" && FormatString(EData.ProviderNPI).Trim() != "")
                                    {
                                        oSegment.set_DataElementValue(8, 0, FormatString(EData.InsEligibilityProviderType).Trim());//SV=Service Provider Number
                                        oSegment.set_DataElementValue(9, 0, FormatString(EData.ProviderNPI).Trim());//"0202034");//Service Provider No
                                    }
                                    //}

                                    //Provider Secondary ID & Secondary Type
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    if (FormatString(EData.InsEligibilityProviSecType).Trim() != "" && FormatString(EData.InsEligibilityProvSecID).Trim() != "")
                                    {
                                        oSegment.set_DataElementValue(1, 0, FormatString(EData.InsEligibilityProviSecType).Trim());
                                        oSegment.set_DataElementValue(2, 0, FormatString(EData.InsEligibilityProvSecID).Trim());
                                    }

                                    //INFORMATION RECEIVER ADDRESS
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                                    oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderAddress).Trim());
                                    //oSegment.set_DataElementValue(2, 0, "1");

                                    //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                                    oSegment.set_DataElementValue(1, 0, FormatString(EData.ProviderCity).Trim());
                                    oSegment.set_DataElementValue(2, 0, FormatString(EData.ProviderState).Trim());
                                    oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderZip).Trim());

                                    #endregion " Receiver Loop "

                                    #region " Subscriber Loop "


                                    //SUBSCRIBER LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    oSegment.set_DataElementValue(1, 0, "3");
                                    oSegment.set_DataElementValue(2, 0, "2");
                                    oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                                    if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                                    {
                                        oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");
                                        //oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.BatchPatientID) + "-" + Convert.ToString(EData.PatientID));
                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.BatchDetailID));
                                        //oSegment.set_DataElementValue(3, 0, "1000000001");
                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(EData.BatchPatientID));
                                        //oSegment.set_DataElementValue(4, 0, "Batch Patient ID");
                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(EData.PatientID));
                                    }
                                    else
                                    {
                                        oSegment.set_DataElementValue(4, 0, "1");
                                    }


                                    //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                                    oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                                    if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF" && EData.IsSubscriberCompany == true)
                                    {
                                        oSegment.set_DataElementValue(2, 0, "2"); //1=Person
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberCompanyname).Trim());//"Subscriber Last Name");//
                                        oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                                        oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");
                                    }
                                    else
                                    {
                                        oSegment.set_DataElementValue(2, 0, "1"); //1=Person

                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberLName).Trim());//"Subscriber Last Name");//
                                        oSegment.set_DataElementValue(4, 0, FormatString(EData.SubscriberFName).Trim());//"Subscriber First Name");
                                        if (EData.SubscriberMName != "")
                                        {
                                            oSegment.set_DataElementValue(5, 0, FormatString(EData.SubscriberMName).Trim());//"Subscriber Middle Name");//
                                        }
                                        oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                                        oSegment.set_DataElementValue(9, 0, FormatString(EData.SubscriberID).Trim());//"11122333301");

                                    }
                                    //SUBSCRIBER ADDITIONAL IDENTIFICATION-REF
                                    if (FormatString(EData.Group).Trim() != "")
                                    {
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                                        oSegment.set_DataElementValue(1, 0, "1L");
                                        oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                                    }
                                    //else if (EData.PatientSSN != "" && EData.PatientSubscriberRelationShip == "SELF")
                                    //{
                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                                    //    oSegment.set_DataElementValue(1, 0, "SY");//"SY");//1L=Group or Policy Number
                                    //    oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.PatientSSN));
                                    //}

                                    //SUBSCRIBER ADDRESS
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));
                                    oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberAddressLn1).Trim());
                                    if (FormatString(EData.PatientAddressLn2).Trim() != "")
                                    {
                                        oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberAddressLn2).Trim());
                                    }

                                    //SUBSCRIBER CITY,STATE and ZIP
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                                    oSegment.set_DataElementValue(1, 0, FormatString(EData.SubscriberCity).Trim());//"City");
                                    oSegment.set_DataElementValue(2, 0, FormatString(EData.SubscriberState).Trim());//"State");
                                    oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberZip).Trim());//"ZIP");



                                    //SUBSCRIBER PROVIDER INFORMATION
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\PRV"));
                                    oSegment.set_DataElementValue(1, 0, "PE");//PC=Primary Care Physician PE=Performing
                                    oSegment.set_DataElementValue(2, 0, "HPI");
                                    oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());

                                    //SUBSCRIBER DEMOGRAPHIC INFORMATION
                                    if ( EData.IsSubscriberCompany == false)
                                    {
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                        //oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(mtxtDOB.Text.Trim())));//"Subscriber Date of Birth"); //Date of Birth
                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.SubscriberDOB.Replace("*", " "))));//"Subscriber Date of Birth"); //Date of Birth
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.SubscriberGender).Trim().ToUpper());//"Gender"); //Gender
                                    }
                                    ///////

                                    //if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                                    //{

                                    //if (EData.SubscriberCardIssueDate == null || EData.SubscriberCardIssueDate.Trim() == "")
                                    //{
                                    //    //SUBSCRIBER DATE
                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                    //    oSegment.set_DataElementValue(1, 0, "307");
                                    //    oSegment.set_DataElementValue(2, 0, "D8");
                                    //    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                                    //}
                                    //else
                                    //{
                                    //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                                    //    oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                                    //    oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                    //    oSegment.set_DataElementValue(3, 0, EData.SubscriberCardIssueDate.Trim());
                                    //}

                                    // }

                                    //MaheshB
                                    if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SELF")
                                    {
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                                        oSegment.set_DataElementValue(1, 0, "30");
                                    }

                                    #endregion " Subscriber Loop "

                                    #region Dependent Loop
                                    if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() != "SELF")
                                    {
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\HL"));
                                        oSegment.set_DataElementValue(1, 0, "4");
                                        oSegment.set_DataElementValue(2, 0, "3");
                                        oSegment.set_DataElementValue(3, 0, "23");
                                        oSegment.set_DataElementValue(4, 0, "0");

                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");
                                        //oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.BatchPatientID) + "-" + Convert.ToString(EData.PatientID));
                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(EData.BatchDetailID));
                                        //oSegment.set_DataElementValue(3, 0, "1000000001");
                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(EData.BatchPatientID));
                                        //oSegment.set_DataElementValue(4, 0, "Batch Patient ID");
                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(EData.PatientID));

                                        //DEPENDENT NAME
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "03");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientLName).Trim());
                                        oSegment.set_DataElementValue(4, 0, FormatString(EData.PatientFName).Trim());
                                        if (EData.PatientMName.Trim() != "")
                                        {
                                            oSegment.set_DataElementValue(5, 0, FormatString(EData.PatientMName).Trim());
                                        }

                                        //DEPENDENT ADDITIONAL IDENTIFICATION
                                        if (FormatString(Convert.ToString(EData.PatientSSN)) != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "SY");
                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientSSN)));
                                        }
                                        else if (FormatString(EData.Group).Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "6P");
                                            oSegment.set_DataElementValue(2, 0, FormatString(EData.Group).Trim());
                                        }
                                        else
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "EJ");
                                            oSegment.set_DataElementValue(2, 0, FormatString(Convert.ToString(EData.PatientCode)));
                                        }

                                        //End code comment on 20100902 ,Sagar Ghodke

                                        //DEPENDENT ADDRESS
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N3"));
                                        oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientAddressLn1).Trim());
                                        if (FormatString(EData.PatientAddressLn2).Trim() != "")
                                        {
                                            oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientAddressLn2).Trim());
                                        }

                                        //DEPENDENT CITY/STATE/ZIP CODE
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N4"));
                                        oSegment.set_DataElementValue(1, 0, FormatString(EData.PatientCity).Trim());
                                        oSegment.set_DataElementValue(2, 0, FormatString(EData.PatientState).Trim());
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientZip).Trim());

                                        //PROVIDER INFORMATION
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\PRV"));
                                        oSegment.set_DataElementValue(1, 0, "PE");
                                        oSegment.set_DataElementValue(2, 0, "HPI");
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.ProviderNPI).Trim());

                                        //DEPENDENT DEMOGRAPHIC INFORMATION
                                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");
                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(EData.PatientDOB.Trim())));
                                        oSegment.set_DataElementValue(3, 0, FormatString(EData.PatientGender.Trim().ToUpper()));

                                        //MaheshB
                                        ////DEPENDENT RELATIONSHIP 2010D
                                        if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                                        {
                                            ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\INS"));
                                            oSegment.set_DataElementValue(1, 0, "N");


                                            if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "SPOUSE")
                                            { oSegment.set_DataElementValue(2, 0, "01"); }
                                            else if (FormatString(EData.PatientSubscriberRelationShip).Trim().ToUpper() == "CHILD")
                                            { oSegment.set_DataElementValue(2, 0, "19"); }
                                            else
                                            { oSegment.set_DataElementValue(2, 0, "34"); }
                                        }

                                        //SUBSCRIBER DATE
                                        if (FormatString(EData.SubscriberInsStartDate).Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                            oSegment.set_DataElementValue(1, 0, "102");
                                            oSegment.set_DataElementValue(2, 0, "D8");
                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(EData.SubscriberInsStartDate).ToShortDateString())));
                                        }
                                        //else
                                        //{
                                        //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                                        //    oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                                        //    oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                                        //    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour
                                        //}

                                        //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\EQ\\EQ"));
                                        oSegment.set_DataElementValue(1, 0, "30"); // "98");//Service Type Code, 98 = Professional Visit - Office
                                    }

                                    #endregion

                                    # region " Collect Batch Patient ID "

                                    _dr = dtResultIDs.NewRow();
                                    _dr["nPatientID"] = Convert.ToString(ListEData[_Counter].PatientID);
                                    _dr["nBatchPatientID"] = Convert.ToString(ListEData[_Counter].BatchPatientID);
                                    _dr["nContactID"] = Convert.ToString(ListEData[_Counter].ContactID);
                                    _dr["nBatchDetailID"] = Convert.ToString(ListEData[_Counter].BatchDetailID);
                                    dtResultIDs.Rows.Add(_dr);
                                    dtResultIDs.AcceptChanges();

                                    #endregion
                                }
                            }

                            #region  " Save EDI File "

                            //oEdiDoc.Save(sPath + sEdiFile);
                            //EdiFile = sPath + sEdiFile;



                            //if (EData.ClearingHouseType == gloSettings.ClearingHouseType.GatewayEDI.GetHashCode())
                            //{
                            //    ClsPostEDI objPostEDI = null;
                            //    try
                            //    {
                            //        objPostEDI = new ClsPostEDI();
                            //        objPostEDI._FilePath = EdiFile;
                            //        objPostEDI.EligibilityUserName = EData.EligibilityUserName;
                            //        objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                            //        objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                            //        objPostEDI.SubmitterID = EData.SubmitterID;
                            //        objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), EData.ContactID);

                            //        //objPostEDI.Post5010EDI(EData.PayerID, EData.PayerName, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), EData.ContactID, ANSIVersion);

                            //        _response = objPostEDI.PostEDIResult;
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        if ((objPostEDI != null))
                            //        {
                            //            objPostEDI.Dispose();
                            //            objPostEDI = null;
                            //        }
                            //        ex.ToString();
                            //        ex = null;
                            //    }
                            //    finally
                            //    {
                            //        if ((objPostEDI != null))
                            //        {
                            //            objPostEDI.Dispose();
                            //            objPostEDI = null;
                            //        }
                            //    }
                            //}
                            //else if (EData.ClearingHouseType == gloSettings.ClearingHouseType.RealMed.GetHashCode())
                            //{
                            //    ClsPostEDIRealMed objPostEDI = null;
                            //    try
                            //    {
                            //        objPostEDI = new ClsPostEDIRealMed();
                            //        objPostEDI._FilePath = EdiFile;
                            //        objPostEDI.EligibilityUserName = EData.EligibilityUserName;
                            //        objPostEDI.EligibilityPassword = EData.EligibilityPassword;
                            //        objPostEDI.EligibilityUrl = EData.EligibilityUrl;
                            //        objPostEDI.SubmitterID = EData.SubmitterID;
                            //        objPostEDI.PostEDI(EData.PayerID, _databaseconnectionstring, Convert.ToInt64(EData.PatientID), Convert.ToInt64(EData.PatientContactInsID), EData.ContactID);
                            //        _response = objPostEDI.PostEDIResult;
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        if ((objPostEDI != null))
                            //        {
                            //            objPostEDI.Dispose();
                            //            objPostEDI = null;
                            //        }
                            //        ex.ToString();
                            //        ex = null;
                            //    }
                            //    finally
                            //    {
                            //        if ((objPostEDI != null))
                            //        {
                            //            objPostEDI.Dispose();
                            //            objPostEDI = null;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}





                            #endregion  " Save EDI File "

                            _result=Convert.ToString(oEdiDoc.GetEdiString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (ListEData != null)
                    { ListEData = null; }

                    if (oSegment != null)
                    {
                        oSegment.Dispose();
                    }


                    if (oInterchange != null)
                    {
                        oInterchange.Dispose();
                    }

                    if (oGroup != null)
                    {
                        oGroup.Dispose();
                    }

                    if (oTransactionset != null)
                    {
                        oTransactionset.Dispose();
                    }



                    if (oSchema != null)
                    {
                        oSchema.Dispose();
                    }

                    if (oSchemas != null)
                    {
                        oSchemas.Dispose();
                    }

                    if (oEdiDoc != null)
                    {
                        oEdiDoc.Dispose();
                    }


                }
                return _result;

            }

            private bool ValidateBatchEligibilityData(EiligiblityData EData)
            {
                bool _Validated = false;
                string strMissingText = "";
                string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
                string strMissingTextHeader = "";

                try
                {
                    strMissingTextHeader = "Eligibility request not sent due to missing : " + Environment.NewLine + Environment.NewLine + "";

                    if ((EData.SubscriberFName == null || FormatString(EData.SubscriberFName.Trim()) == "") && EData.IsSubscriberCompany == false)
                    { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }
                    if ((EData.SubscriberLName == null || FormatString(EData.SubscriberLName.Trim()) == "") && EData.IsSubscriberCompany == false)
                    { strMissingText += "Subscriber Last Name" + Environment.NewLine + ""; }
                    if ((EData.SubscriberDOB == null || FormatString(EData.SubscriberDOB.Trim()) == "") && EData.IsSubscriberCompany == false) { strMissingText += "Subscriber Date of Birth" + Environment.NewLine + ""; }
                    if (EData.SubscriberGender == null || EData.SubscriberGender == "") { strMissingText += "Subscriber Gender" + Environment.NewLine + ""; }
                    if (EData.PayerName == null || FormatString(EData.PayerName.Trim()) == "") { strMissingText += "Payer/Insurance Name" + Environment.NewLine + ""; }
                    if (EData.SubscriberID == null || FormatString(EData.SubscriberID.Trim()) == "") { strMissingText += "Insurance ID" + Environment.NewLine + ""; }
                    if (EData.PayerID == null || FormatString(EData.PayerID.Trim()) == "") { strMissingText += "Payer/Insurance ID" + Environment.NewLine + ""; }


               
                    if (EData.ProviderFName == null || EData.ProviderFName == "") { strMissingText += "Provider First Name" + Environment.NewLine + ""; }
                    if (EData.ProviderLName == null || EData.ProviderLName == "") { strMissingText += "Provider Last Name" + Environment.NewLine + ""; }
                    if (EData.ProviderCity == null || EData.ProviderCity == "") { strMissingText += "Provider City" + Environment.NewLine + ""; }
                    if (EData.ProviderNPI == null || EData.ProviderNPI == "") { strMissingText += "Primary Eligibility ID" + Environment.NewLine + ""; }
                    if (EData.ProviderState == null || EData.ProviderState == "") { strMissingText += "Provider State" + Environment.NewLine + ""; }
                    if (EData.ProviderZip == null || EData.ProviderZip == "") { strMissingText += "Provider Zip" + Environment.NewLine + ""; }
                    if (EData.ProviderAddress == null || EData.ProviderAddress == "") { strMissingText += "Provider Address" + Environment.NewLine + ""; }
                





                

                    if (EData.PatientSubscriberRelationShip == null || EData.PatientSubscriberRelationShip.ToUpper() == "SELF")
                    {
                        if (EData.PatientLName == null || EData.PatientLName == "") { strMissingText += "Patient Last Name" + Environment.NewLine + ""; }
                        if (EData.PatientDOB == null || EData.PatientDOB == "") { strMissingText += "Patient Date of Birth" + Environment.NewLine + ""; }
                        if (EData.PatientAddressLn1 + EData.PatientAddressLn2 == "") { strMissingText += "Patient Address" + Environment.NewLine + ""; }
                        if (EData.PatientCity == null || EData.PatientCity == "") { strMissingText += "Patient City" + Environment.NewLine + ""; }
                        if (EData.PatientState == null || EData.PatientState == "") { strMissingText += "Patient State" + Environment.NewLine + ""; }
                        if (EData.PatientZip == null || EData.PatientZip == "") { strMissingText += "Patient Zip" + Environment.NewLine + ""; }
                    }
                    else if (EData.PatientSubscriberRelationShip.ToUpper() != "SELF")
                    {
                        if (EData.SubscriberAddressLn1 + EData.SubscriberAddressLn2 == "") { strMissingText += "Subscriber Address" + Environment.NewLine + ""; }
                        if (EData.SubscriberCity == null || EData.SubscriberCity == "") { strMissingText += "Subscriber City" + Environment.NewLine + ""; }
                        if (EData.SubscriberState == null || EData.SubscriberState == "") { strMissingText += "Subscriber State" + Environment.NewLine + ""; }
                        if (EData.SubscriberZip == null || EData.SubscriberZip == "") { strMissingText += "Subscriber Zip" + Environment.NewLine + ""; }
                    }

                    if (FormatString(strMissingText.Trim()) != "")
                    {
                        _Validated = false;
                        //_FilePath = _FilePath + "270Validation.txt";
                        //System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                        //oStreamWriter.WriteLine(strMissingTextHeader + strMissingText);
                        //oStreamWriter.Close();
                        //oStreamWriter.Dispose();
                        //System.Diagnostics.Process.Start(_FilePath);
                    }
                    else
                    { _Validated = true; }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                return _Validated;
            }

        #endregion " 270 File Generation "

            #region " 271 File Generation "

                ediDocument oEdiDoc1 = null;

                private bool LoadEDIObject_271(bool _IsEligibilityTest,string _sEligibilityTestFile)
                {
                    bool _result = true;
               
                    string sSefFile = String.Empty;

                    try
                    {

                        ediDocument.Set(ref oEdiDoc1, new ediDocument());    //SLR:when is the newly allocated edidocument freeed: Please resfer any guideline provided by library? ? oEdiDoc = new ediDocument();
                        sPath = AppDomain.CurrentDomain.BaseDirectory;
                        sSefFile = "271_005010X279A1.SemRef.SEF";
                        //sEdiFile = "271.X12";
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

                        if (!_IsEligibilityTest)
                        {
                            if (System.IO.File.Exists(sPath + sSefFile))
                            {
                                oEdiDoc1.ImportSchema(sPath + sSefFile, 0);
                                oEdiDoc1.LoadEdiString(_sEligibilityTestFile);
                                _result = true;
                            }
                            else
                            {
                                MessageBox.Show("Selected file doesn't exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                _result = false;
                            }
                        }
                        else
                        {
                            if (_sEligibilityTestFile != "" && Convert.ToString(_sEligibilityTestFile) != "")
                            {
                                oEdiDoc1.ImportSchema(sPath + sSefFile, 0);
                                oEdiDoc1.LoadEdiString(_sEligibilityTestFile);
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
                        _result = false;
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                        _result = false;
                    }
                    return _result;
                }

                public bool SaveEligibility(List <gloEligibilityResponse> objgloEligibilityResponseList)
                {
                    EligibilityResponse oEligibility = new EligibilityResponse(_databaseconnectionstring);
                    bool _result = false;
                    try
                    {
                        if (objgloEligibilityResponseList != null)
                        {
                            for (int _Counter = 0; _Counter < objgloEligibilityResponseList.Count; _Counter++)
                            {
                                if (objgloEligibilityResponseList[_Counter] != null)
                                {
                                    Int64 _PatientID = 0;
                                    Int64 _ContactID = 0;
                                    Int64 _BatchPatientID = 0;
                                    string _strMismatchMessage = String.Empty;

                                    //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                                     _PatientID = Convert.ToInt64(objgloEligibilityResponseList[_Counter].PatientID);
                                     _ContactID = Convert.ToInt64(objgloEligibilityResponseList[_Counter].ContactID);
                                     _BatchPatientID = Convert.ToInt64(objgloEligibilityResponseList[_Counter].BatchPatientID);

                                    if (_PatientID != 0 && _ContactID != 0 && _BatchPatientID != 0)
                                    {
                                        oEligibility.AddEligibility(objgloEligibilityResponseList[_Counter]);
                                        _result = true;
                                    }
                                    else
                                    {
                                        if (_PatientID == 0)
                                        {
                                            if (_strMismatchMessage != "")
                                            {
                                                _strMismatchMessage = _strMismatchMessage + Environment.NewLine;
                                            }
                                            _strMismatchMessage = _strMismatchMessage + "271 File does not have PatientID. ";
                                        }
                                        if (_ContactID == 0)
                                        {
                                            if (_strMismatchMessage != "")
                                            {
                                                _strMismatchMessage = _strMismatchMessage + Environment.NewLine;
                                            }
                                            _strMismatchMessage = _strMismatchMessage + "271 File does not have ContactID. ";
                                        }
                                        if (_BatchPatientID == 0)
                                        {
                                            if (_strMismatchMessage != "")
                                            {
                                                _strMismatchMessage = _strMismatchMessage + Environment.NewLine;
                                            }
                                            _strMismatchMessage = _strMismatchMessage + "271 File does not have BatchPatientID. ";
                                        }
                                        if (_strMismatchMessage.Trim() != "")
                                        {
                                            RaiseMisMatch(_PatientID, _ContactID, _BatchPatientID, _strMismatchMessage, objgloEligibilityResponseList[_Counter].SubscriberName, objgloEligibilityResponseList[_Counter].SubscriberDOB, objgloEligibilityResponseList[_Counter].PayerName);
                                        }
                                        else 
                                        {
                                            _result = false;
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        _result = false;
                    }
                    finally
                    {
                        if (oEligibility != null)
                        {
                            oEligibility.Dispose();
                        }
                        if (objgloEligibilityResponseList != null)
                        {
                            objgloEligibilityResponseList = null;
                        }
                    }
                    return _result;
                }

                public void RaiseMisMatch(Int64 nPatientID, Int64 nContactID, Int64 nBatchPatientID, string sDescreption, 
                    string sPatientName, Int64 nPatientDOB, string sPayerName)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                    try
                    {
                        oDB.Connect(false);
                        oDBParameters.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nBatchPatientID", nBatchPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@sDescreption", sDescreption, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@sPatientName", sPatientName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@nPatientDOB", nPatientDOB, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@sPayerName", sPayerName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDB.Execute("BL_INUP_EligibilityBatchMisMatch", oDBParameters);
                        oDB.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (oDB != null)
                        { 
                            oDB.Disconnect();
                            oDB.Dispose();
                        }
                        if (oDBParameters != null)
                        {
                            oDBParameters.Dispose();
                        }
                    }
                }

                public bool DoEligibilty(bool _IsTest,string _strPath)
                {
                    bool _result = false;
                    try 
                    {
                        _result=Translate271Response(_IsTest, _strPath);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        _result = false;
                    }
                    finally
                    {
                    }
                    return _result;
                }

                //Todo : ContactID PatientID.....................
                private bool Translate271Response(bool IsTest, string _sFilePath)
                {
                    List<gloEligibilityResponse> objgloEligibilityResponseList = new List<gloEligibilityResponse>();

                    gloEligibilityResponse ogloEligibilityResponse = null;
                    gloEligibility ogloEligibility = new gloEligibility();
                    gloEligibilities ogloEligibilities = new gloEligibilities();
                    bool IsPatientDependent = false;
                    bool IsPatientSubscriber = false;
                    int nEBCounter = 0;
                    string _strError = String.Empty;
                    string _strFollowup = String.Empty;
                    string EDIReturnResult = String.Empty;
                    bool _result = false;

                    try
                    {
                        string sSegmentID = "";
                        string sLoopSection = "";
                    
                        string sEntity = "";
                        string Qlfr = "";

                        int nArea;

                        string sValue = "";
                    

                        #region " Load SEF Files "

                        if (LoadEDIObject_271(IsTest, _sFilePath))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            return _result;
                        }

                        #endregion

                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc1.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                        // This loop iterates though the EDI file a segment at a time
                        while (oSegment != null)
                        {
                            //SLR: Initialize ogloEligibilityresponse = null: otherwise previous memory will be added to list?


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
                                        sValue = oSegment.get_DataElementValue(11);   //Repetition Separator
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
                                        sValue = oSegment.get_DataElementValue(3);
                                    }
                                    else if (sSegmentID == "BHT")
                                    {
                                        ogloEligibilityResponse = new gloEligibilityResponse();
                                        //ogloEligibilityResponse.PatientID = _PatientId;
                                        ogloEligibilityResponse.ReferenceID = oSegment.get_DataElementValue(3);
                                        ogloEligibilityResponse.ClinicID = 1;
                                        ogloEligibilityResponse.dtEligibilityCheck = DateTime.Now;
                                        //ogloEligibilityResponse.ContactID = Convert.ToString(_ContactID);
                                        ogloEligibilityResponse.ANSIVersion = gloSettings.ANSIVersions.ANSI_5010.GetHashCode();
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
                                        //Payer/Information Source Name
                                        if (sSegmentID == "NM1")
                                        {
                                            ogloEligibilityResponse.PayerName = oSegment.get_DataElementValue(3);
                                            ogloEligibilityResponse.PayerID = oSegment.get_DataElementValue(9);
                                        }
                                        else if (sSegmentID == "PER")
                                        {
                                            ogloEligibilityResponse.PayerContactName = oSegment.get_DataElementValue(2);
                                            ogloEligibilityResponse.PayerContactNumber = oSegment.get_DataElementValue(4);

                                            string sPayerURL_04 = "";
                                            string sPayerURL_06 = "";
                                            string sPayersURL_08 = "";

                                            if (oSegment.get_DataElementValue(3) == "UR")
                                            {
                                                sPayerURL_04 = oSegment.get_DataElementValue(4);
                                            }

                                            if (oSegment.get_DataElementValue(5) == "UR")
                                            {
                                                sPayerURL_06 = oSegment.get_DataElementValue(6);
                                            }

                                            if (oSegment.get_DataElementValue(7) == "UR")
                                            {
                                                sPayersURL_08 = oSegment.get_DataElementValue(8);
                                            }

                                            if (sPayerURL_04 != "")
                                            {
                                                ogloEligibilityResponse.PayerURL = sPayerURL_04;
                                            }
                                            else if (sPayerURL_06 != "")
                                            {
                                                ogloEligibilityResponse.PayerURL = sPayerURL_06;
                                            }
                                            else if (sPayersURL_08 != "")
                                            {
                                                ogloEligibilityResponse.PayerURL = sPayersURL_08;
                                            }
                                        }
                                        else if (sSegmentID == "AAA")
                                        {
                                            if (oSegment.get_DataElementValue(1) == "N")
                                            {
                                                sValue = oSegment.get_DataElementValue(2);
                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                
                                                    if (GetSourceRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                    {
                                                        if (_strError != "") _strError = _strError + Environment.NewLine;
                                                        _strError = _strError + " Source Rejection Reason: " + GetSourceRejectionReason(oSegment.get_DataElementValue(3));
                                                    }
                                                    if (GetSourceFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                    {
                                                        if (_strError != "") _strError = _strError + Environment.NewLine;
                                                        _strFollowup = _strFollowup + " Source Follow up: " + (GetSourceFollowUp(oSegment.get_DataElementValue(4)));
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
                                        }
                                    }

                                    else if (sLoopSection == "HL;NM1")
                                    {
                                        //Receiver/Provider Name
                                        if (sSegmentID == "NM1")
                                        {
                                            if (oSegment.get_DataElementValue(5).Trim() != "")
                                            {
                                                ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            else
                                            {
                                                ogloEligibilityResponse.ReceiverName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            ogloEligibilityResponse.ReceiverID = oSegment.get_DataElementValue(9);
                                        }
                                        else if (sSegmentID == "REF")
                                        {
                                            ogloEligibilityResponse.ReceiverAdditionalID = oSegment.get_DataElementValue(2);
                                        }
                                        else if (sSegmentID == "AAA")
                                        {
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                if (GetReceiverRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                {
                                                    if (_strError != "") _strError = _strError + Environment.NewLine;
                                                    _strError = _strError + " Receiver Rejection Reason: " + GetReceiverRejectionReason(oSegment.get_DataElementValue(3));
                                                }
                                                if (GetReceiverFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                {
                                                    if (_strFollowup != "") _strFollowup = _strFollowup + Environment.NewLine;
                                                    _strFollowup = _strFollowup + " ReceiverFollowUp: " + GetReceiverFollowUp(oSegment.get_DataElementValue(4));
                                                }
                                            }

                                            EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                        }

                                    }
                                }
                                else if (sEntity == "22")
                                {
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                        }
                                        else if (sSegmentID == "TRN")
                                        {
                                             string _strTRNQualifier = oSegment.get_DataElementValue(1);
                                             if (_strTRNQualifier.Trim() != "" && _strTRNQualifier.Trim() == "2")
                                             {
                                                 string _IDs = "";
                                                 string[] _PatientIDs = null;

                                                 //Please check this condition is really required................
                                                 //if (Convert.ToString(oSegment.get_DataElementValue(1)) == "2")
                                                 _IDs = Convert.ToString(oSegment.get_DataElementValue(2));
                                                 _PatientIDs = _IDs.Split('-');
                                                 sValue = oSegment.get_DataElementValue(3);
                                                 Int64 _PatientID = 0;
                                                 Int64 _BatchPatientID = 0;

                                                 if (_PatientIDs != null && _PatientIDs.Length > 0)
                                                 {
                                                     if (_PatientIDs[0] != null && Convert.ToString(_PatientIDs[0]) != "")
                                                     {
                                                         _BatchPatientID = Convert.ToInt64(_PatientIDs[0]);
                                                     }
                                                     if (_PatientIDs.Length > 1 && _PatientIDs[1] != null && Convert.ToString(_PatientIDs[1]) != "")
                                                     {
                                                         _PatientID = Convert.ToInt64(_PatientIDs[1]);
                                                     }
                                                 }
                                                 ogloEligibilityResponse.PatientID = _PatientID;
                                                 ogloEligibilityResponse.BatchPatientID = _BatchPatientID;
                                                 ogloEligibilityResponse.ContactID = GetContactID(_BatchPatientID);

                                                 //This is how we send in 270
                                                 //Convert.ToString(EData.BatchPatientID) + "-" + Convert.ToString(EData.PatientID))
                                                 //ogloEligibilityResponse.PatientID = _PatientId;
                                             }
                                        }
                                    }

                                    else if (sLoopSection == "HL;NM1")
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (oSegment.get_DataElementValue(5).Trim() != "")
                                            {
                                                ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            else
                                            {
                                                ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            if (oSegment.get_DataElementValue(9).Trim() != "")
                                            {
                                                //Check for SubscriberID : .........
                                                ogloEligibilityResponse.SubscriberID = oSegment.get_DataElementValue(9);
                                            }
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
                                            sValue = oSegment.get_DataElementValue(2);
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                {
                                                    if (_strError != "") _strError = _strError + Environment.NewLine;
                                                    _strError = _strError + " Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                                }
                                                if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                {
                                                    //rtfError.AppendText(GetSubscriberRejectionReason(oSegment.get_DataElementValue(4)));
                                                    if (Convert.ToString(oSegment.get_DataElementValue(4)).Trim().ToUpper() == "C")
                                                    {
                                                        if (_strError != "") _strFollowup = _strFollowup + Environment.NewLine;
                                                        _strFollowup = _strFollowup + " Subscriber FollowUp: " + "Please Correct and Resubmit ";
                                                    }
                                                }
                                            }

                                            EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                            //}
                                        }
                                        else if (sSegmentID == "DMG")
                                        {
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
                                            int nRelationshipCode=0;
                                            //nRelationshipCode = getPatientRelationship(_PatientId);

                                            if (oSegment.get_DataElementValue(1).Trim() != "")
                                            {
                                                if (oSegment.get_DataElementValue(1).Trim() == "Y")
                                                {
                                                    if (nRelationshipCode != 18)
                                                    {
                                                        IsPatientDependent = true;
                                                        IsPatientSubscriber = false;
                                                    }
                                                    else
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = false;
                                                    }
                                                }
                                                else if (oSegment.get_DataElementValue(1).Trim() == "N")
                                                {
                                                    if (nRelationshipCode == 18)
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = true;
                                                    }
                                                    else
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = false;
                                                    }
                                                }
                                            }


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
                                                        //rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                                        ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]);
                                                    }
                                                }
                                            }
                                            else if (oSegment.get_DataElementValue(1) == "472")
                                            {
                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    string[] _dateRange = null;
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                                    _dateRange = _date.Split('-');

                                                    if (_dateRange != null && _dateRange.Length > 0)
                                                    {
                                                        //rtfeligibilityinfo.AppendText("     Eligibility Check As Of: " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString());
                                                        ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                                    }
                                                }
                                            }
                                        }
                                        else if (sSegmentID == "HSD")
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
                                            nEBCounter = nEBCounter + 1;

                                            if (ogloEligibility.BenefitInformation != null)
                                            {
                                                ogloEligibilities.Add(ogloEligibility);
                                            }
                                            ogloEligibility = new gloEligibility();
                                            ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                            if (oSegment.get_DataElementValue(2).Trim() != "")
                                            {
                                                ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                            }
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                            }
                                            if (nEBCounter == 1)
                                            {
                                                if (oSegment.get_DataElementValue(4).Trim() != "" && oSegment.get_DataElementValue(3).Trim() != "30" && oSegment.get_DataElementValue(5).Trim() != "")
                                                {
                                                    ogloEligibilityResponse.InsuranceTypeCode = Convert.ToString(oSegment.get_DataElementValue(4)).Trim();
                                                    ogloEligibilityResponse.InsuranceTypeDescription = GetInsuranceTypeDescription(ogloEligibilityResponse.InsuranceTypeCode);
                                                }
                                            }

                                            if (oSegment.get_DataElementValue(6).Trim() != "")
                                            {
                                                ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                            }

                                            string sBenefitAmount = "";
                                            sBenefitAmount = getBenefitAmount(oSegment.get_DataElementValue(6).Trim(), oSegment.get_DataElementValue(7).Trim(), oSegment.get_DataElementValue(8).Trim(), oSegment.get_DataElementValue(9).Trim(), oSegment.get_DataElementValue(10).Trim());

                                       
                                            if (oSegment.get_DataElementValue(7).Trim() != "")
                                            {
                                                ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                                ogloEligibility.EligibilityAmountFormatted = sBenefitAmount;
                                            }
                                        
                                            if (oSegment.get_DataElementValue(11).Trim() == "Y")
                                            {
                                                ogloEligibility.IsAuthRequire = true;
                                            }
                                            else if (oSegment.get_DataElementValue(11).Trim() == "N")
                                            {
                                                ogloEligibility.IsAuthRequire = false;
                                            }
                                            else if (oSegment.get_DataElementValue(11).Trim() == "U")
                                            {
                                                ogloEligibility.IsAuthRequire = false;
                                            }

                                            if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                            {
                                                ogloEligibility.IsPlanNetwork = true;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "N")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "U")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "W")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }

                                        }

                                        if (sSegmentID == "DTP")
                                        {
                                            if (oSegment.get_DataElementValue(2).Trim().ToUpper() == "D8")
                                            {

                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));

                                                    if (_date != null)
                                                    {
                                                        ogloEligibility.SubscriberDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_date)).ToShortDateString();
                                                    }
                                                }

                                            }
                                            else if (oSegment.get_DataElementValue(2).Trim().ToUpper() == "RD8")
                                            {
                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    string[] _dateRange = null;
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                                    _dateRange = _date.Split('-');

                                                    if (_dateRange != null && _dateRange.Length > 0)
                                                    {


                                                        if (_dateRange[0] == _dateRange[1])
                                                        {
                                                            ogloEligibility.SubscriberDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString();//Date
                                                        }
                                                        else
                                                        {
                                                            ogloEligibility.SubscriberDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[0])).ToShortDateString() + " - " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dateRange[1])).ToShortDateString();//Date
                                                        }
                                                    }
                                                }
                                            }

                                            // }
                                        }

                                        if (sSegmentID == "MSG")
                                        {
                                            ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                        }
                                        else if (sSegmentID == "AAA")
                                        {
                                            sValue = oSegment.get_DataElementValue(2);
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                {
                                                    if (_strError != "") _strError = _strError + Environment.NewLine;
                                                    _strError = _strError + " Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                                }
                                                if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                {
                                                    if (_strFollowup != "") _strFollowup = _strFollowup + Environment.NewLine;
                                                    _strFollowup = _strFollowup + " Subscriber FollowUp : " + (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                                }
                                            }
                                        }
                                        else if (sSegmentID == "REF")
                                        {
                                        }
                                        if (sLoopSection == "HL;NM1;EB;NM1")
                                        {
                                            if (sSegmentID == "III")
                                            {
                                            }
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
                                                sValue = Convert.ToString(oSegment.get_DataElementValue(4)) + " " + 
                                                    Convert.ToString(oSegment.get_DataElementValue(3)) + " " + 
                                                    Convert.ToString(oSegment.get_DataElementValue(5));
                                                ogloEligibilityResponse.PrimaryCarePhysicainName = sValue;
                                                sValue = "";
                                            }
                                        }
                                        else if (sSegmentID == "N3")
                                        {
                                            ogloEligibilityResponse.PrimaryCareAddress = Convert.ToString(oSegment.get_DataElementValue(1)) + Environment.NewLine + Convert.ToString(oSegment.get_DataElementValue(2));
                                        }
                                        else if (sSegmentID == "N4")
                                        {
                                            ogloEligibilityResponse.PrimaryCareCity = Convert.ToString(oSegment.get_DataElementValue(1));
                                            ogloEligibilityResponse.PrimaryCareState = Convert.ToString(oSegment.get_DataElementValue(2));
                                            ogloEligibilityResponse.PrimaryCareZip = Convert.ToString(oSegment.get_DataElementValue(3));
                                        }
                                        else if (sSegmentID == "PER")
                                        {
                                            sValue = oSegment.get_DataElementValue(2);
                                            ogloEligibilityResponse.PrimaryCarePhysicainContactName = Convert.ToString(sValue);
                                            sValue = oSegment.get_DataElementValue(4);
                                            ogloEligibilityResponse.PrimaryCarePhysicainContactNumber = Convert.ToString(sValue);
                                            sValue = "";
                                        }
                                    }
                                }
                                else if (sEntity == "23")
                                {

                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                        }
                                        else if (sSegmentID == "TRN")
                                        {
                                             string _strTRNQualifier = oSegment.get_DataElementValue(1);
                                             if (_strTRNQualifier.Trim() != "" && _strTRNQualifier.Trim() == "2")
                                             {
                                                 string _IDs = "";
                                                 string[] _PatientIDs = null;

                                                 //Please check this condition is really required................
                                                 //if (Convert.ToString(oSegment.get_DataElementValue(1)) == "2")
                                                 _IDs = Convert.ToString(oSegment.get_DataElementValue(2));
                                                 _PatientIDs = _IDs.Split('-');
                                                 sValue = oSegment.get_DataElementValue(3);
                                                 Int64 _PatientID = 0;
                                                 Int64 _BatchPatientID = 0;

                                                 if (_PatientIDs != null && _PatientIDs.Length > 0)
                                                 {
                                                     if (_PatientIDs[0] != null && Convert.ToString(_PatientIDs[0]) != "")
                                                     {
                                                         _BatchPatientID = Convert.ToInt64(_PatientIDs[0]);
                                                     }
                                                     if (_PatientIDs.Length > 1 && _PatientIDs[1] != null && Convert.ToString(_PatientIDs[1]) != "")
                                                     {
                                                         _PatientID = Convert.ToInt64(_PatientIDs[1]);
                                                     }
                                                 }
                                                 ogloEligibilityResponse.PatientID = _PatientID;
                                                 ogloEligibilityResponse.BatchPatientID = _BatchPatientID;
                                                 ogloEligibilityResponse.ContactID = GetContactID(_BatchPatientID);

                                                 //This is how we send in 270
                                                 //Convert.ToString(EData.BatchPatientID) + "-" + Convert.ToString(EData.PatientID))
                                                 //ogloEligibilityResponse.PatientID = _PatientId;
                                             }
                                        }
                                    }

                                    else if (sLoopSection == "HL;NM1")
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (oSegment.get_DataElementValue(5).Trim() != "")
                                            {
                                                ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(5) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            else
                                            {
                                                ogloEligibilityResponse.SubscriberName = oSegment.get_DataElementValue(4) + " " + oSegment.get_DataElementValue(3);
                                            }
                                            if (oSegment.get_DataElementValue(9).Trim() != "")
                                            {
                                                //SubscriberID:.......................
                                                //rtfeligibilityinfo.AppendText("     Identification Number: ");
                                                ogloEligibilityResponse.SubscriberID = oSegment.get_DataElementValue(9);
                                            }
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
                                            sValue = oSegment.get_DataElementValue(2);
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                {
                                                    if (_strError != "") _strError = _strError + Environment.NewLine ;
                                                    _strError = _strError + " Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                                }
                                                if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                {
                                                    if (_strFollowup != "") _strFollowup = _strFollowup + Environment.NewLine ;
                                                    _strFollowup = _strFollowup + " Subscriber Follow up: " + (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                                }
                                            }

                                            EDIReturnResult = oSegment.get_DataElementValue(3).Trim() + "-" + oSegment.get_DataElementValue(4).Trim();

                                            //}
                                        }
                                        else if (sSegmentID == "DMG")
                                        {
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
                                            int nRelationshipCode=0;
                                            //nRelationshipCode = getPatientRelationship(_PatientId);

                                            if (oSegment.get_DataElementValue(1).Trim() != "")
                                            {
                                                if (oSegment.get_DataElementValue(1).Trim() == "Y")
                                                {
                                                    if (nRelationshipCode != 18)
                                                    {

                                                        IsPatientDependent = true;
                                                        IsPatientSubscriber = false;
                                                    }
                                                    else
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = false;
                                                    }
                                                }
                                                else if (oSegment.get_DataElementValue(1).Trim() == "N")
                                                {
                                                    if (nRelationshipCode == 18)
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = true;
                                                    }
                                                    else
                                                    {
                                                        IsPatientDependent = false;
                                                        IsPatientSubscriber = false;

                                                    }
                                                }
                                            }
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
                                                        ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]); //Convert.ToInt64(oSegment.get_DataElementValue(3));
                                                    }
                                                }
                                            }
                                            else if (oSegment.get_DataElementValue(1) == "472")
                                            {
                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    string[] _dateRange = null;
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                                    _dateRange = _date.Split('-');

                                                    if (_dateRange != null && _dateRange.Length > 0)
                                                    {
                                                        ogloEligibilityResponse.EligibilityDate = Convert.ToInt64(_dateRange[0]);
                                                    }
                                                }
                                            }
                                        }
                                        else if (sSegmentID == "HSD")
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
                                            nEBCounter = nEBCounter + 1;
                                            if (ogloEligibility.BenefitInformation != null)
                                            {
                                                ogloEligibilities.Add(ogloEligibility);
                                            }
                                            ogloEligibility = new gloEligibility();

                                            //Benefit Information
                                            ogloEligibility.BenefitInformation = GetBenefitDescription(Qlfr);
                                            if (oSegment.get_DataElementValue(2).Trim() != "")
                                            {
                                                ogloEligibility.CoverageLevel = GetCoverageDescription(oSegment.get_DataElementValue(2));
                                            }
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                ogloEligibility.ServiceType = GetServiceTypeDescription(oSegment.get_DataElementValue(3));
                                            }

                                            sValue = oSegment.get_DataElementValue(4);
                                            if (nEBCounter == 1)
                                            {
                                                if (oSegment.get_DataElementValue(4).Trim() != "" && oSegment.get_DataElementValue(3).Trim() != "30" && oSegment.get_DataElementValue(5).Trim() != "")
                                                {
                                                    ogloEligibilityResponse.InsuranceTypeCode = Convert.ToString(oSegment.get_DataElementValue(4)).Trim();
                                                    ogloEligibilityResponse.InsuranceTypeDescription = GetInsuranceTypeDescription(ogloEligibilityResponse.InsuranceTypeCode);
                                                }
                                            }
                                            if (oSegment.get_DataElementValue(6).Trim() != "")
                                            {
                                                ogloEligibility.TimePeriod = oSegment.get_DataElementValue(6).Trim();
                                            }

                                            string sBenefitAmount = "";
                                            sBenefitAmount = getBenefitAmount(oSegment.get_DataElementValue(6).Trim(), oSegment.get_DataElementValue(7).Trim(), oSegment.get_DataElementValue(8).Trim(), oSegment.get_DataElementValue(9).Trim(), oSegment.get_DataElementValue(10).Trim());

                                            if (oSegment.get_DataElementValue(7).Trim() != "")
                                            {
                                                ogloEligibility.EligibilityAmountFormatted = sBenefitAmount;
                                                ogloEligibility.EligibilityAmount = Convert.ToInt64(Convert.ToDecimal(oSegment.get_DataElementValue(7)));
                                            }
                                        
                                            if (oSegment.get_DataElementValue(11).Trim() == "Y")
                                            {
                                                ogloEligibility.IsAuthRequire = true;
                                            }
                                            else if (oSegment.get_DataElementValue(11).Trim() == "N")
                                            {
                                                ogloEligibility.IsAuthRequire = false;
                                            }
                                            else if (oSegment.get_DataElementValue(11).Trim() == "U")
                                            {
                                                ogloEligibility.IsAuthRequire = false;
                                            }

                                            if (oSegment.get_DataElementValue(12).Trim() == "Y")
                                            {
                                                ogloEligibility.IsPlanNetwork = true;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "N")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "U")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }
                                            else if (oSegment.get_DataElementValue(12).Trim() == "W")
                                            {
                                                ogloEligibility.IsPlanNetwork = false;
                                            }

                                        }

                                        if (sSegmentID == "DTP")
                                        {
                                            if (oSegment.get_DataElementValue(2).Trim() == "D8")
                                            {

                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));

                                                    if (_date != null)
                                                    {
                                                        ogloEligibility.SubscriberDate = _date;
                                                    }
                                                }

                                            }
                                            else if (oSegment.get_DataElementValue(2).Trim() == "RD8")
                                            {
                                                if (oSegment.get_DataElementValue(3).Trim() != "")
                                                {
                                                    string _date = "";
                                                    string[] _dateRange = null;
                                                    _date = Convert.ToString(oSegment.get_DataElementValue(3));
                                                    _dateRange = _date.Split('-');

                                                    if (_dateRange != null && _dateRange.Length > 0)
                                                    {
                                                        if (_dateRange[0] == _dateRange[1])
                                                        {
                                                            ogloEligibility.SubscriberDate = _dateRange[0];
                                                        }
                                                        else
                                                        {
                                                            ogloEligibility.SubscriberDate = _dateRange[0] + " - " + _dateRange[1];
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                        if (sSegmentID == "MSG")
                                        {
                                            ogloEligibility.Message = oSegment.get_DataElementValue(1);
                                        }
                                        else if (sSegmentID == "AAA")
                                        {
                                            sValue = oSegment.get_DataElementValue(2);
                                            if (oSegment.get_DataElementValue(3).Trim() != "")
                                            {
                                                if (GetSubscriberRejectionReason(oSegment.get_DataElementValue(3)) != "")
                                                {
                                                    if (_strError != "") _strError = _strError + Environment.NewLine;
                                                    _strError = _strError + " Subscriber Rejection Reason: " + GetSubscriberRejectionReason(oSegment.get_DataElementValue(3));
                                                }
                                                if (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)) != "")
                                                {
                                                    if (_strFollowup != "") _strFollowup = _strFollowup + Environment.NewLine;
                                                    _strFollowup = _strFollowup + " Subscriber Follow up: " + (GetSubscriberFollowUp(oSegment.get_DataElementValue(4)));
                                                }
                                            }
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
                                    else if (sLoopSection == "HL;NM1;EB;NM1")
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (oSegment.get_DataElementValue(1) == "P3")
                                            {
                                                sValue = Convert.ToString(oSegment.get_DataElementValue(4)) + " " +
                                                    Convert.ToString(oSegment.get_DataElementValue(3)) + " " +
                                                    Convert.ToString(oSegment.get_DataElementValue(5));
                                                ogloEligibilityResponse.PrimaryCarePhysicainName = sValue;
                                                //sValue = oSegment.get_DataElementValue(1);
                                            }
                                        }
                                        else if (sSegmentID == "N3")
                                        {
                                            ogloEligibilityResponse.PrimaryCareAddress = Convert.ToString(oSegment.get_DataElementValue(1)) + Environment.NewLine + Convert.ToString(oSegment.get_DataElementValue(2));
                                        }
                                        else if (sSegmentID == "N4")
                                        {
                                            ogloEligibilityResponse.PrimaryCareCity = Convert.ToString(oSegment.get_DataElementValue(1));
                                            ogloEligibilityResponse.PrimaryCareState = Convert.ToString(oSegment.get_DataElementValue(2));
                                            ogloEligibilityResponse.PrimaryCareZip = Convert.ToString(oSegment.get_DataElementValue(3));
                                        }
                                        else if (sSegmentID == "PER")
                                        {
                                            sValue = oSegment.get_DataElementValue(2);
                                            ogloEligibilityResponse.PrimaryCarePhysicainContactName = sValue;
                                            sValue = oSegment.get_DataElementValue(4);
                                            ogloEligibilityResponse.PrimaryCarePhysicainContactNumber = sValue;
                                        }
                                    }
                                }
                                if (sSegmentID == "SE")
                                {
                                    if (ogloEligibilityResponse != null)
                                    {
                                        ogloEligibilityResponse.Eligibilities = ogloEligibilities;
                                    }

                                    if (objgloEligibilityResponseList != null && ogloEligibilityResponse != null)
                                    {
                                        objgloEligibilityResponseList.Add(ogloEligibilityResponse);
                                    }
                                }
                            }
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());

                        }

                        if (IsPatientDependent)
                        {
                            //rtfeligibilityinfo.AppendText("Note: ");
                            //rtfeligibilityinfo.AppendText("       Patient is a dependent but should be the subscriber. Please correct the Patient's Insurance.");
                        }
                        else if (IsPatientSubscriber)
                        {
                            //rtfeligibilityinfo.AppendText("Note: ");
                            //rtfeligibilityinfo.AppendText("       Patient is the subscriber but should be a dependent. Please correct the Patient's Insurance.");
                        }

                        SaveEligibility(objgloEligibilityResponseList);

                        #region //Show Errors
                 
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        if (!IsTest)
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
                        //SLR: Some of the following are added to list: hence the  freeing them is not correct. Please check
                        if (ogloEligibility != null)
                        {
                            ogloEligibility.Dispose();
                        }
                        //if (ogloEligibilityResponse != null)
                        //{
                        //    ogloEligibilityResponse.Dispose();
                        //}

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
                    return _result;
                }

                public string GetSourceRejectionReason(string _RejectCode)
                {
                    string _result = String.Empty;

                    switch(_RejectCode)
                    {
                       case "04":
                                { _result = "Authorized Quantity Exceeded";  break; }
                       case "41":
                                { _result = "Authorization/Access Restrictions"; break; }
                       case "42":
                                { _result = "Unable to Respond at Current Time"; break; }
                       case "79":
                                { _result = "Invalid Participant Identification"; break; }
                       case "80":
                                { _result = "No Response received - Transaction Terminated"; break; }
                       case "T4":
                                { _result = "Payer Name or Identifier Missing"; break; }
                    }
                    return _result;
               
                }

                public string GetSourceFollowUp(string _FollowUpCode)
                {
                    string _result = String.Empty;

                    switch (_FollowUpCode)
                    {
                        case "C":
                            { _result = "Please Correct and Resubmit"; break; }
                        case "N":
                            { _result = "Resubmission Not Allowed"; break; }
                        case "P":
                            { _result = "Please Resubmit Original Transaction"; break; }
                        case "R":
                            { _result = "Resubmission Allowed"; break; }
                        case "S":
                            { _result = "Do Not Resubmit; Inquiry Initiated to a Third Party"; break; }
                        case "W":
                            { _result = "Please Wait 30 Days and Resubmit"; break; }
                        case "X":
                            { _result = "Please Wait 10 Days and Resubmit"; break; }
                        case "Y":
                            { _result = "Do Not Resubmit; We Will Hold Your Request and Respond Again Shortly"; break; }
                    }
                    return _result;

                }

                public string GetReceiverRejectionReason(string _RejectCode)
                {
                    string _result = String.Empty;

                    switch (_RejectCode)
                    {
                        case "15":
                            { _result = "Required application data missing"; break; }
                        case "41":
                            { _result = "Authorization/Access Restrictions"; break; }
                        case "43":
                            { _result = "Invalid/Missing Provider Identification    "; break; }
                        case "44":
                            { _result = "Invalid/Missing Provider Name"; break; }
                        case "45":
                            { _result = "Invalid/Missing Provider Specialty"; break; }
                        case "46":
                            { _result = "Invalid/Missing Provider Phone Number"; break; }
                        case "47":
                            { _result = "Invalid/Missing Provider State"; break; }
                        case "48":
                            { _result = "Invalid/Missing Referring Provider Identification Number"; break; }
                        case "50":
                            { _result = "Provider Ineligible for Inquiries"; break; }
                        case "51":
                            { _result = "Provider Not on File"; break; }
                        case "79":
                            { _result = "Invalid Participant Identification"; break; }
                        case "97":
                            { _result = "Invalid or Missing Provider Address"; break; }
                        case "T4":
                            { _result = "Payer Name or Identifier Missing"; break; }
                    }
                    return _result;

                }

                public string GetReceiverFollowUp(string _FollowUpCode)
                {
                    string _result = String.Empty;

                    switch (_FollowUpCode)
                    {
                        case "C":
                            { _result = "Please Correct and Resubmit"; break; }
                        case "N":
                            { _result = "Resubmission Not Allowed"; break; }
                        case "R":
                            { _result = "Resubmission Allowed 1185"; break; }
                        case "S":
                            { _result = "Do Not Resubmit; Inquiry Initiated to a Third Party"; break; }
                        case "W":
                            { _result = "Please Wait 30 Days and Resubmit"; break; }
                        case "X":
                            { _result = "Please Wait 10 Days and Resubmit"; break; }
                        case "Y":
                            { _result = "Do Not Resubmit; We Will Hold Your Request and Respond Again Shortly 1185"; break; }
                    }
                    return _result;

                }

                public string GetSubscriberRejectionReason(string _RejectCode)
                {
                    string _result = String.Empty;

                    switch (_RejectCode)
                    {
                        case "15":
                            { _result = "Required application data missing"; break; }
                        case "42":
                            { _result = "Unable to Respond at Current Time"; break; }
                        case "43":
                            { _result = "Invalid/Missing Provider Identification"; break; }
                        case "45":
                            { _result = "Invalid/Missing Provider Specialty"; break; }
                        case "47":
                            { _result = "Invalid/Missing Provider State"; break; }
                        case "48":
                            { _result = "Invalid/Missing Referring Provider Identification Number"; break; }
                        case "49":
                            { _result = "Provider is Not Primary Care Physician"; break; }
                        case "51":
                            { _result = "Provider Not on File"; break; }
                        case "52":
                            { _result = "Service Dates Not Within Provider Plan Enrollment"; break; }
                        case "56":
                            { _result = "Inappropriate Date"; break; }
                        case "57":
                            { _result = "Invalid/Missing Date(s) of Service"; break; }
                        case "58":
                            { _result = "Invalid/Missing Date-of-Birth"; break; }
                        case "60":
                            { _result = "Date of Birth Follows Date(s) of Service"; break; }
                        case "61":
                            { _result = "Date of Death Precedes Date(s) of Service"; break; }
                        case "62":
                            { _result = "Date of Service Not Within Allowable Inquiry Period"; break; }
                        case "63":
                            { _result = "Date of Service in Future"; break; }
                        case "64":
                            { _result = "Invalid/Missing Patient ID"; break; }
                        case "65":
                            { _result = "Invalid/Missing Patient Name"; break; }
                        case "66":
                            { _result = "Invalid/Missing Patient Gender Code"; break; }
                        case "67":
                            { _result = "Patient Not Found"; break; }
                        case "68":
                            { _result = "Duplicate Patient ID Number"; break; }
                        case "71":
                            { _result = "Patient Birth Date Does Not Match That for the Patient on the Database"; break; }
                        case "72":
                            { _result = "Invalid/Missing Subscriber/Insured ID"; break; }
                        case "73":
                            { _result = "Invalid/Missing Subscriber/Insured Name"; break; }
                        case "74":
                            { _result = "Invalid/Missing Subscriber/Insured Gender Code"; break; }
                        case "75":
                            { _result = "Subscriber/Insured Not Found"; break; }
                        case "76":
                            { _result = "Duplicate Subscriber/Insured ID Number"; break; }
                        case "77":
                            { _result = "Subscriber Found, Patient Not Found"; break; }
                        case "78":
                            { _result = "Subscriber/Insured Not in Group/Plan Identified"; break; }
                    }
                    return _result;

                }

                public string GetSubscriberFollowUp(string _FollowUpCode)
                {
                    string _result = String.Empty;

                    switch (_FollowUpCode)
                    {
                        case "C":
                            { _result = "Please Correct and Resubmit"; break; }
                        case "N":
                            { _result = "Resubmission Not Allowed"; break; }
                        case "P":
                            { _result = "Please Resubmit Original Transaction"; break; }
                        case "R":
                            { _result = "Resubmission Allowed"; break; }
                        case "S":
                            { _result = "Do Not Resubmit; Inquiry Initiated to a Third Party"; break; }
                        case "W":
                            { _result = "Please Wait 30 Days and Resubmit"; break; }
                        case "X":
                            { _result = "Please Wait 10 Days and Resubmit"; break; }
                        case "Y":
                            { _result = "Do Not Resubmit; We Will Hold Your Request and Respond Again Shortly"; break; }
                    }
                    return _result;

                }

                public string GetBenefitDescription(string _BenefiteCode)
                {
                    string _result = String.Empty;

                    switch (_BenefiteCode)
                    {
                        case "1":
                            { _result = "Active Coverage"; break; }
                        case "2":
                            { _result = "Active - Full Risk Capitation"; break; }
                        case "3":
                            { _result = "Active - Services Capitated"; break; }
                        case "4":
                            { _result = "Active - Services Capitated to Primary Care Physician"; break; }
                        case "5":
                            { _result = "Active - Pending Investigation"; break; }
                        case "6":
                            { _result = "Inactive"; break; }
                        case "7":
                            { _result = "Inactive - Pending Eligibility Update"; break; }
                        case "8":
                            { _result = "Inactive - Pending Investigation"; break; }
                        case "A":
                            { _result = "Co-Insurance"; break; }
                        case "B":
                            { _result = "Co-Payment"; break; }
                        case "C":
                            { _result = "Deductible"; break; }
                        case "CB":
                            { _result = "Coverage Basis"; break; }
                        case "D":
                            { _result = "Benefit Description"; break; }
                        case "E":
                            { _result = "Exclusions"; break; }
                        case "F":
                            { _result = "Limitations"; break; }
                        case "G":
                            { _result = "Out of Pocket (Stop Loss)"; break; }
                        case "H":
                            { _result = "Unlimited"; break; }
                        case "I":
                            { _result = "Non-Covered"; break; }
                        case "J":
                            { _result = "Cost Containment"; break; }
                        case "K":
                            { _result = "Reserve"; break; }
                        case "L":
                            { _result = "Primary Care Provider"; break; }
                        case "M":
                            { _result = "Pre-existing Condition"; break; }
                        case "MC":
                            { _result = "Managed Care Coordinator"; break; }
                        case "N":
                            { _result = "Services Restricted to Following Provider"; break; }
                        case "O":
                            { _result = "Not Deemed a Medical Necessity"; break; }
                        case "P":
                            { _result = "Benefit Disclaimer"; break; }
                        case "Q":
                            { _result = "Second Surgical Opinion Required"; break; }
                        case "R":
                            { _result = "Other or Additional Payor"; break; }
                        case "S":
                            { _result = "Prior Year(s) History"; break; }
                        case "T":
                            { _result = "Card(s) Reported Lost/Stolen"; break; }
                        case "U":
                            { _result = "Contact Following Entity for Eligibility or Benefit Information"; break; }
                        case "V":
                            { _result = "Cannot Process"; break; }
                        case "W":
                            { _result = "Other Source of Data"; break; }
                        case "X":
                            { _result = "Health Care Facility"; break; }
                        case "Y":
                            { _result = "Active - Pending Investigation"; break; }
                        case "Z":
                            { _result = "Spend Down"; break; }
                    }
                    return _result;

                }

                private string GetServiceTypeDescription(string ServiceTypeCode)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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
                        //SLR: Free _result
                        if (_result != null)
                        {
                            _result = null;
                        }

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

                private string GetInsuranceTypeDescription(string InsuranceTypeCode)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    string _strSQL = "";
                    object _result = null;
                    string _strInusranceType = "";
                    try
                    {
                        oDB.Connect(false);
                        _strSQL = "SELECT sInsuranceTypeDesc FROM Eligibility_Insurance_Type WHERE sInsuranceTypeCode = '" + InsuranceTypeCode + "'";
                        _result = oDB.ExecuteScalar_Query(_strSQL);
                        if (_result != null)
                        {
                            _strInusranceType = Convert.ToString(_result);
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                    }
                    finally
                    {
                        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                        //SLR: Free _result
                        if (_result != null)
                        {
                            _result = null;
                        }
                    }
                    return _strInusranceType;
                }

                private int getPatientRelationship(Int64 PatientID)
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    int sRelationshipCode;
                    try
                    {
                        oDB.Connect(false);
                        string query = "SELECT sRelationshipCode FROM PatientRelationship INNER JOIN PatientInsurance_DTL ON PatientInsurance_DTL.nRelationShipId = PatientRelationship.nPatientRelID " +
                                       " WHERE nPatientID = " + PatientID + "";
                        sRelationshipCode = Convert.ToInt16(oDB.ExecuteScalar_Query(query));

                        return sRelationshipCode;
                    }
                    catch (Exception)// ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        return 0;
                    }
                    finally
                    {
                        //SLR: odb.disconnect and then
                        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                    }
                }

                private string getBenefitAmount(object TimePeriodDesc, object MonetaryAmount, object percentage, object QuantityQualifier, object Quantity)
                {
                    string sBenefitAmount = "";
                    //  string _percentage =Convert.ToString(percentage).Trim().Replace(".","");// Convert.ToString(percentage).Trim();
                    decimal _percentagevalue = 0;
                    if ( Convert.ToString(percentage) != "")
                    {
                        _percentagevalue = Convert.ToDecimal(percentage) * 100;
                    }
                    if (Convert.ToString(TimePeriodDesc).Trim() != "" && Convert.ToString(MonetaryAmount).Trim() != "" && Convert.ToString(_percentagevalue).Trim() != "" && _percentagevalue != 0 && Convert.ToString(QuantityQualifier).Trim() != "" && Convert.ToString(Quantity).Trim() != "")
                    {
                        sBenefitAmount = Convert.ToString(TimePeriodDesc).Trim() + "  $" + Convert.ToString(MonetaryAmount).Trim() + " " + _percentagevalue.ToString("#0.###############").Trim() + "% " + Convert.ToString(QuantityQualifier).Trim() + " " + Convert.ToString(Quantity).Trim();
                    }
                    else
                    {
                        string _tempQualifier = GetTimePeriodQualifier(Convert.ToString(TimePeriodDesc)).Trim();
                        if (_tempQualifier != "")
                        {
                            sBenefitAmount = _tempQualifier;
                        }

                        if (Convert.ToString(MonetaryAmount).Trim() != "")
                        {
                            sBenefitAmount = sBenefitAmount + " $" + Convert.ToString(MonetaryAmount).Trim();
                        }
                        else
                        {
                            sBenefitAmount = sBenefitAmount + "";
                        }

                        if (Convert.ToString(_percentagevalue).Trim() != "" && _percentagevalue != 0)
                        {
                            sBenefitAmount = sBenefitAmount + " " + _percentagevalue.ToString("#0.###############").Trim() + "% ";
                        }
                        else
                        {
                            sBenefitAmount = sBenefitAmount + "";
                        }

                        if (Convert.ToString(QuantityQualifier).Trim() != "")
                        {
                            if (GetQuantityQualifier(Convert.ToString(QuantityQualifier).Trim()) != "")
                            {
                                sBenefitAmount = sBenefitAmount + " " + GetQuantityQualifier(Convert.ToString(QuantityQualifier).Trim());
                            }
                            else
                            {
                                sBenefitAmount = sBenefitAmount + "";
                            }
                        }
                        else
                        {
                            sBenefitAmount = sBenefitAmount + "";
                        }

                        if (Convert.ToString(Quantity).Trim() != "")
                        {
                            sBenefitAmount = sBenefitAmount + " " + Convert.ToString(Quantity).Trim();
                        }
                        else
                        {
                            sBenefitAmount = sBenefitAmount + "";
                        }

                    }
                    return sBenefitAmount;


                }

                private string GetTimePeriodQualifier(string _TimePeriodQualifier)
                {
                    string _result = String.Empty;

                    switch (_TimePeriodQualifier)
                    {
                        case "6":
                            { _result = "Hour"; break; }
                        case "7":
                            { _result = "Day"; break; }
                        case "13":
                            { _result = "24 Hours"; break; }
                        case "21":
                            { _result = "Years"; break; }
                        case "22":
                            { _result = "Service Year"; break; }
                        case "23":
                            { _result = "Calendar Year"; break; }
                        case "24":
                            { _result = "Year to Date"; break; }
                        case "25":
                            { _result = "Contract"; break; }
                        case "26":
                            { _result = "Episode"; break; }
                        case "27":
                            { _result = "Visit"; break; }
                        case "28":
                            { _result = "Outlier"; break; }
                        case "29":
                            { _result = "Remaining"; break; }
                        case "30":
                            { _result = "Exceeded"; break; }
                        case "31":
                            { _result = "Not Exceeded"; break; }
                        case "32":
                            { _result = "Lifetime"; break; }
                        case "33":
                            { _result = "Lifetime Remaining"; break; }
                        case "34":
                            { _result = "Month"; break; }
                        case "35":
                            { _result = "Week"; break; }
                        case "36":
                            { _result = "Admission"; break; }
                    }
                    return _result;
                }

                private string GetQuantityQualifier(string _QuantityQualifier)
                {

                    string _result = String.Empty;

                    switch (_QuantityQualifier)
                    {
                        case "8H":
                            { _result = "Hour"; break; }
                        case "99":
                            { _result = "Day"; break; }
                        case "CA":
                            { _result = "24 Hours"; break; }
                        case "CE":
                            { _result = "Years"; break; }
                        case "D3":
                            { _result = "Service Year"; break; }
                        case "DB":
                            { _result = "Calendar Year"; break; }
                        case "DY":
                            { _result = "Year to Date"; break; }
                        case "HS":
                            { _result = "Contract"; break; }
                        case "LA":
                            { _result = "Episode"; break; }
                        case "LE":
                            { _result = "Visit"; break; }
                        case "M2":
                            { _result = "Outlier"; break; }
                        case "MN":
                            { _result = "Remaining"; break; }
                        case "P6":
                            { _result = "Exceeded"; break; }
                        case "QA":
                            { _result = "Not Exceeded"; break; }
                        case "S7":
                            { _result = "Lifetime"; break; }
                    }
                    return _result;
                }

                private Int64 GetContactID(Int64 _BatchPatientID)
                {
                    Int64 _result = 0;
                    gloDatabaseLayer.DBLayer oDB = null;
                    string _sqlQuery = "";

                    try
                    {
                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        oDB.Connect(false);
                        _sqlQuery = "Select ISNULL(nContactID,0) from EligibilityBatchDetails where nBatchPatientID=" + _BatchPatientID + "";
                        Object _objresult = oDB.ExecuteScalar_Query(_sqlQuery);
                        if (_objresult != null && Convert.ToString(_objresult) != "")
                        {
                            _result = Convert.ToInt64(_objresult);
                        }
                        if (_objresult != null)
                        {
                            _objresult = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        //SLR: Finaly disconnect ODB, free odb, objResult
                        if (oDB != null)
                        {
                            oDB.Disconnect();
                            oDB.Dispose();
                        }
                        
                    }
                    return _result;
                }
            #endregion " 271 File Generation "

        #endregion "Batch Eligibility"

        #endregion " Private & Public Methods "
    }

    public class EiligiblityData : IDisposable
    {
        #region "Constructor & Distructor"


        public EiligiblityData()
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

        ~EiligiblityData()
        {
            Dispose(false);
        }

        #endregion

        #region " Varible Declaration "

        public string ClearingHouseSubmitterID { get; set; }

        public string ClearingHouseReceiverID { get; set; }
        public string ClearingHouseTypeOfData { get; set; }
        public int ClearingHouseType { get; set; }

        public string PayerName { get; set; } //Insurance Plan Name
        public string PayerID { get; set; } //Insurance Payer ID
        public string Group { get; set; } //Insurance Group
        public Int64 ContactID { get; set; } 

        public string ProviderFName { get; set; }
        public string ProviderLName { get; set; }
        public string ProviderMName { get; set; }
        public string ProviderCity { get; set; }
        public string ProviderSSN { get; set; }
        public  string ProviderNPI {get; set;}
        public  string ProviderState {get; set;}
        public  string ProviderZip {get; set;}
        public  string ProviderAreaCode {get; set;}
        public  string ProviderAddress {get; set;}
        public  string ProviderAddressType {get; set;}
        public string ProviderTaxId { get; set; }
        public string ProviderSettingValue { get; set; }

        public  string PatientSubscriberRelationShip {get; set;}

        public  string SubscriberFName {get; set;}
        public  string SubscriberLName {get; set;}
        public  string SubscriberMName {get; set;}
        public string SubscriberCompanyname { get; set; }
        public bool IsSubscriberCompany { get; set; }
        public  string SubscriberDOB {get; set;}
        public  string SubscriberGender {get; set;}
        public  string SubscriberID {get; set;}
        public  string SubscriberCity {get; set;}
        public  string SubscriberSSN {get; set;}
        public  string SubscriberState {get; set;}
        public  string SubscriberZip {get; set;}
        public  string SubscriberAddressLn1 {get; set;}
        public string SubscriberAddressLn2 { get; set; }
        public bool IsSameAsPatient { get; set; }
        public string SubscriberCardIssueDate { get; set; }
        public string SubscriberInsStartDate { get; set; }
        public string SubscriberInsEndDate { get; set; }

        public  string PatientCode {get; set;}
        public  string PatientFName {get; set;}
        public  string PatientLName {get; set;}
        public  string PatientMName {get; set;}
        public  string PatientDOB {get; set;}
        public  string PatientGender {get; set;}
        public  string PatientID {get; set;}
        public string PatientContactInsID { get; set; }
        public  string PatientCity {get; set;}
        public  string PatientSSN {get; set;}
        public  string PatientState {get; set;}
        public  string PatientZip {get; set;}
        public  string PatientAddressLn1 {get; set;}
        public string PatientAddressLn2 { get; set; }

        public string EligibilityUserName { get; set; }
        public string EligibilityPassword { get; set; }
        public string EligibilityUrl { get; set; }
        public string SubmitterID { get; set; }

        public string InsEligibilityProviderID {get;set;}
        public string InsEligibilityProviderType {get;set;}
        public string InsEligibilityProvSecID {get;set;}
        public string InsEligibilityProviSecType { get; set; }

        public Int64 BatchPatientID { get; set; }

        public Int64 BatchDetailID { get; set; }

        //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
        public string ReceiverQualifier { get; set; }
        public string SenderQualifier { get; set; }

        #endregion " Varible Declaration "
    }
}
