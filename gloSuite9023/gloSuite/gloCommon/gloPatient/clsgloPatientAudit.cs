using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Linq;

namespace gloPatient
{
    public class clsgloPatientAudit
    {

        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";
        private string _username = "";

        #region "Constructor & Destructor"

        public clsgloPatientAudit(string DatabaseConnectionString)
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

        ~clsgloPatientAudit()
        {
            Dispose(false);
        }

        #endregion

        public bool SavePatientAuditDetails(long patientID, long AuditTrailId, string UserAction, string sInboundHospital = "", string sInboundtranCare="")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            bool _result = false;
            try
            {
                oDB.Connect(false);
                odbParams.Add("@AuditTrailID", AuditTrailId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@UserAction", UserAction, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sInboundHospital", sInboundHospital, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sInboundtranCare", sInboundtranCare, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("PA_IN_PatientAudit", odbParams);
                _result = true;

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }

            return _result;

            //return true;
        }

        //public void SavePatientAuditDetails(long patientID, long AuditTrailId, string UserAction)
        //{
        //    #region "Patient Audit Entry"
        //    clsgloPatientAuditData oAuditData = new clsgloPatientAuditData();
        //    try
        //    {
        //        oAuditData.AuditTrailId = AuditTrailId;
        //        oAuditData.UserAction = UserAction;
        //        oAuditData.PatientID = Convert.ToInt64(patientID);
        //        oAuditData.PatientCode = oPatient.DemographicsDetail.PatientCode;
        //        oAuditData.PatientSSN = oPatient.DemographicsDetail.PatientSSN;
        //        oAuditData.PatientFirstName = oPatient.DemographicsDetail.PatientFirstName;
        //        oAuditData.PatientMiddleName = oPatient.DemographicsDetail.PatientMiddleName;
        //        oAuditData.PatientLastName = oPatient.DemographicsDetail.PatientLastName;
        //        oAuditData.PatientSuffix = oPatient.DemographicsDetail.PatientSuffix;
        //        oAuditData.PatientDOB = oPatient.DemographicsDetail.PatientDOB;
        //        oAuditData.BirthTime = oPatient.DemographicsDetail.BirthTime;
        //        oAuditData.PatientGender = oPatient.DemographicsDetail.PatientGender;
        //        oAuditData.PatientMaritalStatus = oPatient.DemographicsDetail.PatientMaritalStatus;
        //        oAuditData.PatientRace = oPatient.DemographicsDetail.PatientRace;
        //        oAuditData.PatientLanguage = oPatient.DemographicsDetail.PatientLanguage;
        //        oAuditData.PatientEthnicities = oPatient.DemographicsDetail.PatientEthnicities;
        //        oAuditData.PatientHandDominance = oPatient.DemographicsDetail.PatientHandDominance;
        //        oAuditData.PatientCommunicationPrefence = oPatient.DemographicsDetail.PatientCommunicationPrefence;
        //        oAuditData.PatientAddress1 = oPatient.DemographicsDetail.PatientAddress1;
        //        oAuditData.PatientAddress2 = oPatient.DemographicsDetail.PatientAddress2;
        //        oAuditData.PatientCity = oPatient.DemographicsDetail.PatientCity;
        //        oAuditData.PatientState = oPatient.DemographicsDetail.PatientState;
        //        oAuditData.PatientZip = oPatient.DemographicsDetail.PatientZip;
        //        oAuditData.AreaCode = oPatient.DemographicsDetail.AreaCode;
        //        oAuditData.PatientCountry = oPatient.DemographicsDetail.PatientCountry;
        //        oAuditData.PatientCounty = oPatient.DemographicsDetail.PatientCounty;
        //        oAuditData.PatientPhone = oPatient.DemographicsDetail.PatientPhone;
        //        oAuditData.PatientMobile = oPatient.DemographicsDetail.PatientMobile;
        //        oAuditData.PatientEmail = oPatient.DemographicsDetail.PatientEmail;
        //        oAuditData.PatientFax = oPatient.DemographicsDetail.PatientFax;
        //        oAuditData.PatientProviderID = oPatient.DemographicsDetail.PatientProviderID;
        //        oAuditData.EmergencyContact = oPatient.DemographicsDetail.EmergencyContact;
        //        oAuditData.EmergencyPhone = oPatient.DemographicsDetail.EmergencyPhone;
        //        oAuditData.EmergencyMobile = oPatient.DemographicsDetail.EmergencyMobile;
        //        oAuditData.EmergencyRelationshipCode = oPatient.DemographicsDetail.EmergencyRelationshipCode;
        //        oAuditData.EmergencyRelationshipDesc = oPatient.DemographicsDetail.EmergencyRelationshipDesc;
        //        oAuditData.SexualOrientationID = oPatient.PatientDemographicOtherInfo.SexualOrientationID;
        //        oAuditData.SexualOrientationCode = oPatient.PatientDemographicOtherInfo.SexualOrientationCode;
        //        oAuditData.SexualOrientationDesc = oPatient.PatientDemographicOtherInfo.SexualOrientationDesc;
        //        oAuditData.SexualOrientationOtherSpecification = oPatient.PatientDemographicOtherInfo.SexualOrientationOtherSpecification;
        //        oAuditData.GenderIdentityID = oPatient.PatientDemographicOtherInfo.GenderIdentityID;
        //        oAuditData.GenderIdentityCode = oPatient.PatientDemographicOtherInfo.GenderIdentityCode;
        //        oAuditData.GenderIdentityDesc = oPatient.PatientDemographicOtherInfo.GenderIdentityDesc;
        //        oAuditData.GenderIdentityOtherSpecification = oPatient.PatientDemographicOtherInfo.GenderIdentityOtherSpecification;
        //        oAuditData.sPatientPrevFName = oPatient.PatientDemographicOtherInfo.sPatientPrevFName;
        //        oAuditData.sPatientPrevMName = oPatient.PatientDemographicOtherInfo.sPatientPrevMName;
        //        oAuditData.sPatientPrevLName = oPatient.PatientDemographicOtherInfo.sPatientPrevLName;
        //        oAuditData.PatientBirthSex = oPatient.PatientDemographicOtherInfo.PatientBirthSex;
        //        oAuditData.sMultipleBirthIndicator = oPatient.PatientDemographicOtherInfo.sMultipleBirthIndicator;
        //        oAuditData.BirthOrder = oPatient.PatientDemographicOtherInfo.BirthOrder;
        //        oAuditData.SignatureOnFile = oPatient.PatientDemographicOtherInfo.SOF;
        //        oAuditData.Directive = oPatient.DemographicsDetail.Directive;
        //        oAuditData.ExemptFromReport = oPatient.DemographicsDetail.ExemptFromReport;

        //        clsgloPatientAudit oAudit = new clsgloPatientAudit(_databaseconnectionstring);
        //        oAudit.InsertPatientAuditLog(oAuditData);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    #endregion
        //}
        //private long GetUniqueId()
        //{
        //    long UniqueId = 0;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);
        //        oParameters.Add("@ID", UniqueId, ParameterDirection.Output, SqlDbType.BigInt);
        //        object _oresult = new object();
        //        oDB.Execute("gSP_GetUniqueID", oParameters, out _oresult);
        //        if (Convert.ToInt64(_oresult) > 0 && _oresult != null)
        //        {
        //            UniqueId = Convert.ToInt64(_oresult);
        //        }
        //        return UniqueId;
        //    }
        //    catch (Exception)
        //    {
        //        return UniqueId;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        oParameters.Dispose();
                
        //    }

        //}
    }


    public class clsgloPatientAuditData
    {

        #region "Constructor & Distructor"

        public clsgloPatientAuditData()
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

        ~clsgloPatientAuditData()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private Int64 _LoginSessionId;
        private Int64 _AuditTrailId;
        private string _UserAction = "";
        private Int64 _nPatientID;
        private string _sPatientCode = "";
        private string _nSSN = "";

        private string _sFirstName = "";
        private string _sMiddleName = "";
        private string _sLastName = "";
        private string _sSuffix = "";
        private DateTime _dtDOB;
        private string sBirthTime = "";
        private string _sGender = "";
        private string _sMaritalStatus = "";
        private string _sRace = "";
        private string _sEthn = "";
        private string _sLang = "";
        private string _sHandDominance = "";
        private string _CommPref = "";//Communication Preference
        private string _sAddress1 = "";
        private string _sAddress2 = "";
        private string _sCity = "";
        private string _sState = "";
        private string _sZip = "";
        private string _sAreaCode = "";
        private string _sCounty = "";
        private string _sCountry = "";
        private string _sPhone = "";
        private string _sMobile = "";
        private string _sEmail = "";
        private string _sFax = "";
        private Int64 _nProviderID = 0;
        private string _sEmergencyContact = "";
        private string _sEmergencyPhone = "";
        private string _sEmergencyMobile = "";
        private string _sEmergencyRelationshipCode = "";
        private string _sEmergencyRelationshipDesc = "";

        private Int64 _nSexualOrientationID = 0;
        private string _sSexualOrientationCode = "";
        private string _sSexualOrientationDesc = "";
        private string _sSexualOrientationOtherSpecification = "";
        private Int64 _nGenderIdentityID = 0;
        private string _sGenderIdentityCode = "";
        private string _sGenderIdentityDesc = "";
        private string _sGenderIdentityOtherSpecification = "";
        private string _sPatientPrevFName = "";
        private string _sPatientPrevMName = "";
        private string _sPatientPrevLName = "";
        private string _PatientBirthSex = "";
        private string _sMultipleBirthIndicator = "";
        private Int64 _BirthOrder = 0;


        private bool _bSignatureOnFile = false;
        private bool _exemptFromReport;
        private bool _directive;
        #endregion

        #region " Property Procedures "

        public Int64 LoginSessionId
        {
            get { return _LoginSessionId; }
            set { _LoginSessionId = value; }
        }

        public Int64 AuditTrailId
        {
            get { return _AuditTrailId; }
            set { _AuditTrailId = value; }
        }


        public string UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public string PatientCode
        {
            get { return _sPatientCode; }
            set { _sPatientCode = value; }
        }

        public string PatientSSN
        {
            get { return _nSSN; }
            set { _nSSN = value; }
        }

        public string PatientFirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        public string PatientMiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }

        public string PatientLastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        public string PatientSuffix
        {
            get { return _sSuffix; }
            set { _sSuffix = value; }
        }

        public DateTime PatientDOB
        {
            get { return _dtDOB; }
            set { _dtDOB = value; }
        }

        public string BirthTime
        {
            get { return sBirthTime; }
            set { sBirthTime = value; }
        }

        public string PatientGender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        public string PatientMaritalStatus
        {
            get { return _sMaritalStatus; }
            set { _sMaritalStatus = value; }
        }

        public string PatientRace
        {
            get { return _sRace; }
            set { _sRace = value; }
        }

        public string PatientEthnicities
        {
            get { return _sEthn; }
            set { _sEthn = value; }
        }

        public string PatientLanguage
        {
            get { return _sLang; }
            set { _sLang = value; }
        }

        public string PatientHandDominance
        {
            get { return _sHandDominance; }
            set { _sHandDominance = value; }
        }

        public string PatientCommunicationPrefence
        {
            get { return _CommPref; }
            set { _CommPref = value; }
        }

        public string PatientAddress1
        {
            get { return _sAddress1; }
            set { _sAddress1 = value; }
        }

        public string PatientAddress2
        {
            get { return _sAddress2; }
            set { _sAddress2 = value; }
        }

        public string PatientCity
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        public string PatientState
        {
            get { return _sState; }
            set { _sState = value; }
        }

        public string PatientZip
        {
            get { return _sZip; }
            set { _sZip = value; }
        }

        public string AreaCode
        {
            get { return _sAreaCode; }
            set { _sAreaCode = value; }
        }

        public string PatientCounty
        {
            get { return _sCounty; }
            set { _sCounty = value; }
        }

        public string PatientCountry
        {
            get { return _sCountry; }
            set { _sCountry = value; }
        }

        public string PatientPhone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }

        public string PatientMobile
        {
            get { return _sMobile; }
            set { _sMobile = value; }
        }

        public string PatientEmail
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string PatientFax
        {
            get { return _sFax; }
            set { _sFax = value; }
        }

        public Int64 PatientProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public string EmergencyContact
        {
            get { return _sEmergencyContact; }
            set { _sEmergencyContact = value; }
        }

        public string EmergencyPhone
        {
            get { return _sEmergencyPhone; }
            set { _sEmergencyPhone = value; }
        }

        public string EmergencyMobile
        {
            get { return _sEmergencyMobile; }
            set { _sEmergencyMobile = value; }
        }

        public string EmergencyRelationshipCode
        {
            get { return _sEmergencyRelationshipCode; }
            set { _sEmergencyRelationshipCode = value; }
        }

        public string EmergencyRelationshipDesc
        {
            get { return _sEmergencyRelationshipDesc; }
            set { _sEmergencyRelationshipDesc = value; }
        }


        public Int64 SexualOrientationID
        {
            get { return _nSexualOrientationID; }
            set { _nSexualOrientationID = value; }
        }

        public string SexualOrientationCode
        {
            get { return _sSexualOrientationCode; }
            set { _sSexualOrientationCode = value; }
        }

        public string SexualOrientationDesc
        {
            get { return _sSexualOrientationDesc; }
            set { _sSexualOrientationDesc = value; }
        }

        public string SexualOrientationOtherSpecification
        {
            get { return _sSexualOrientationOtherSpecification; }
            set { _sSexualOrientationOtherSpecification = value; }
        }

        public Int64 GenderIdentityID
        {
            get { return _nGenderIdentityID; }
            set { _nGenderIdentityID = value; }
        }

        public string GenderIdentityCode
        {
            get { return _sGenderIdentityCode; }
            set { _sGenderIdentityCode = value; }
        }

        public string GenderIdentityDesc
        {
            get { return _sGenderIdentityDesc; }
            set { _sGenderIdentityDesc = value; }
        }

        public string GenderIdentityOtherSpecification
        {
            get { return _sGenderIdentityOtherSpecification; }
            set { _sGenderIdentityOtherSpecification = value; }
        }

        public string sPatientPrevFName
        {
            get { return _sPatientPrevFName; }
            set { _sPatientPrevFName = value; }
        }

        public string sPatientPrevMName
        {
            get { return _sPatientPrevMName; }
            set { _sPatientPrevMName = value; }
        }

        public string sPatientPrevLName
        {
            get { return _sPatientPrevLName; }
            set { _sPatientPrevLName = value; }
        }

        public string PatientBirthSex
        {
            get { return _PatientBirthSex; }
            set { _PatientBirthSex = value; }
        }

        public string sMultipleBirthIndicator
        {
            get { return _sMultipleBirthIndicator; }
            set { _sMultipleBirthIndicator = value; }
        }

        public Int64 BirthOrder
        {
            get { return _BirthOrder; }
            set { _BirthOrder = value; }

        }

        public bool SignatureOnFile
        {
            get { return _bSignatureOnFile; }
            set { _bSignatureOnFile = value; }
        }

        public bool ExemptFromReport
        {
            get { return _exemptFromReport; }
            set { _exemptFromReport = value; }
        }

        public bool Directive
        {
            get { return _directive; }
            set { _directive = value; }
        }

        #endregion " Property Procedures "
    }
}
