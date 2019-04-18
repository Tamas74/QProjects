using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class Provider_Old : IDisposable
        {

            #region "Constructor & Destructor"

            public Provider_Old()
            {
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _nClinicID = 0; }
                }
                else
                { _nClinicID = 0; }


                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

                #endregion
      
            }

            public Provider_Old(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _nClinicID = 0; }
                }
                else
                { _nClinicID = 0; }


                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

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

            ~Provider_Old()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            Int64 _nProviderID = 0;
            string _sFirstName = "";
            string _sMiddleName = "";
            string _sLastName = "";
            string _sGender = "";
            string _sDEA = "";
            string _NPI = "";
            string _UPIN = "";
            string _StateMedicalNo = "";
           
            string _sBMAddress1 = "";
            string _sBMAddress2 = "";
            string _sBMCity = "";
            string _sBMState = "";
            string _sBMZIP = "";
            string _sBPracAddress1 = "";
            string _sBPracAddress2 = "";
            string _sBPracCity = "";
            string _sBPracState = "";
            string _sBPracZIP = "";
            string _sBMPhone = "";
            string _sBMFAX = "";
            string _sBMPager = "";
            string _sBMEmail = "";
            string _sBMURL = "";
            string _sBpracPhone = "";
            string _sBpracFAX = "";
            string _sBPracPager = "";
            string _sBPracEmail = "";
            string _sBPracURL = "";
            string _Taxonomy = "";

            string _sMobile = "";
            Image _imgSignature = null;

            string _Prefix = "";
            Int64 _nProviderTypeID = 0;
            string _sUserName = "";
            string _sPassword = "";
            string _sNickName = "";

            private string _sExternalCode = "";

            private Int64 _nClinicID = 0;

            //private variables for Equipment
            string _sCode = "";
            string _sDescription = "";


            Int64 _nResourceTypeID = 0;

            string _MessageBoxCaption = string.Empty;


            private string _databaseconnectionstring = "";
            //  _encryptionKey Temporaryly Declared & Assigner Here 
            // has to Add it in Application Config Filie
            private const string _encryptionKey = "12345678";
            //

            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            #endregion

            #region "Properties"
            public Int64 ProviderID
            {
                get { return _nProviderID; }
                set { _nProviderID = value; }
            }
            public string FirstName
            {
                get { return _sFirstName; }
                set { _sFirstName = value; }
            }
            public string MiddleName
            {
                get { return _sMiddleName; }
                set { _sMiddleName = value; }
            }
            public string LastName
            {
                get { return _sLastName; }
                set { _sLastName = value; }
            }
            public string Gender
            {
                get { return _sGender; }
                set { _sGender = value; }
            }
            public string DEA
            {
                get { return _sDEA; }
                set { _sDEA = value; }
            }
          
            public string Address
            {
                get { return _sBMAddress1; }
                set { _sBMAddress1 = value; }
            }
            public string Street
            {
                get { return _sBMAddress2; }
                set { _sBMAddress2 = value; }
            }
            public string City
            {
                get { return _sBMCity; }
                set { _sBMCity = value; }
            }
            public string State
            {
                get { return _sBMState; }
                set { _sBMState = value; }
            }
            public string ZIP
            {
                get { return _sBMZIP; }
                set { _sBMZIP = value; }
            }

            public string BPracAddress1
            {
                get { return _sBPracAddress1; }
                set { _sBPracAddress1 = value; }
            }
            public string BPracAddress2
            {
                get { return _sBPracAddress2; }
                set { _sBPracAddress2 = value; }
            }
            public string BPracCity
            {
                get { return _sBPracCity; }
                set { _sBPracCity = value; }
            }
            public string BPracState
            {
                get { return _sBPracState; }
                set { _sBPracState = value; }
            }
            public string BPracZIP
            {
                get { return _sBPracZIP; }
                set { _sBPracZIP = value; }
            }

            public string Phone
            {
                get { return _sBMPhone; }
                set { _sBMPhone = value; }
            }
            public string FAX
            {
                get { return _sBMFAX; }
                set { _sBMFAX = value; }
            }
            public string Pager
            {
                get { return _sBMPager; }
                set { _sBMPager = value; }
            }
            public string Email
            {
                get { return _sBMEmail; }
                set { _sBMEmail = value; }
            }
            public string URL
            {
                get { return _sBMURL; }
                set { _sBMURL = value; }
            }

            public string BPracPhone
            {
                get { return _sBpracPhone; }
                set { _sBpracPhone = value; }
            }
            public string BPracFAX
            {
                get { return _sBpracFAX; }
                set { _sBpracFAX = value; }
            }
            public string BPracPager
            {
                get { return _sBPracPager; }
                set { _sBPracPager = value; }
            }
            public string BPracEmail
            {
                get { return _sBPracEmail; }
                set { _sBPracEmail = value; }
            }
            public string BPracURL
            {
                get { return _sBPracURL; }
                set { _sBPracURL = value; }
            }

            public string Taxonomy
            {
                get { return _Taxonomy; }
                set { _Taxonomy = value; }
            }



            public string Mobile
            {
                get { return _sMobile; }
                set { _sMobile = value; }
            }
            
            public Image Signature
            {
                get { return _imgSignature; }
                set { _imgSignature = value; }
            }
            public string NPI
            {
                get { return _NPI; }
                set { _NPI = value; }
            }
            public string UPIN
            {

                get { return _UPIN; }
                set { _UPIN = value; }
            }
            public string StateMedicalNo
            {

                get { return _StateMedicalNo; }
                set { _StateMedicalNo = value; }
            }

            public string Prefix
            {

                get { return _Prefix; }
                set { _Prefix = value; }
            }
            public Int64 ProviderTypeID
            {

                get { return _nProviderTypeID; }
                set { _nProviderTypeID = value; }
            }
            public string UserName
            {

                get { return _sUserName; }
                set { _sUserName = value; }
            }
            public string Password
            {
                get { return _sPassword; }
                set { _sPassword = value; }
            }
            public string NickName
            {
                get { return _sNickName; }
                set { _sNickName = value; }
            }

            public string ExternalCode
            {
                get { return _sExternalCode; }
                set { _sExternalCode = value; }
            }

            public Int64 ClinicID
            {
                get { return _nClinicID; }
                set { _nClinicID = value; }
            }


            //for Equipment
            public string Code
            {
                get { return _sCode; }
                set { _sCode = value; }
            }

            public string Description
            {
                get { return _sDescription; }
                set { _sDescription = value; }
            }
            public Int64 ResourceTypeID
            {
                get { return _nResourceTypeID; }
                set { _nResourceTypeID = value; }
            }

            #endregion

        }//class Provider

        public class Provider : IDisposable
        {

            #region "Constructor & Destructor"

            private string _databaseconnectionstring = "";

            public Provider()
            {
            }

            public Provider(string DatabaseConnectionString)
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

            ~Provider()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            Int64 _nProviderID = 0;
            string _sFirstName = "";
            string _sMiddleName = "";
            string _sLastName = "";
            string _sGender = "";
            string _sDEA = "";
            string _NPI = "";
            string _SSN = "";
            string _EmployerID = "";
            string _UPIN = "";
            string _StateMedicalNo = "";
            string _sBMAddress1 = "";
            string _sBMAddress2 = "";

            string _sBMCity = "";
            string _sBMState = "";
            string _sBMZIP = "";
            string _sBMAreaCode = "";
            string _sBPracAddress1 = "";
            string _sBPracAddress2 = "";
            //string _sBPracStreet = "";
            string _sBPracCity = "";
            string _sBPracState = "";
            string _sBPracZIP = "";
            string _sBPracAreaCode = "";

            string _sBMPhone = "";
            string _sBMFAX = "";
            string _sBMPager = "";
            string _sBMEmail = "";
            string _sBMURL = "";

            string _sBpracPhone = "";
            string _sBpracFAX = "";
            string _sBPracPager = "";
            string _sBPracEmail = "";
            string _sBPracURL = "";

            string _sCompanyName = "";
            string _sCompanyContactName = "";
            string _sCompanyAddress1 = "";
            string _sCompanyAddress2 = "";
            string _sCompanyCity = "";
            string _sCompanyState = "";
            string _sCompanyZip = "";
            string _sCompanyAreaCode = "";
            string _sCompanyPhone = "";
            string _sCompanyFax = "";
            string _sCompanyEmail = "";
            //Two fields added by Anil on 20090310
            string _sCompanyNPI = "";
            string _sCompanyTaxID = "";
            //
            string _sMobile = "";
            Image _imgSignature = null;
            string _Taxonomy = "";
            //sTaxonomyDesc
            string _TaxonomyDesc = "";
            string _Prefix = "";
            string _Suffix = "";
            Int64 _nProviderTypeID = 0;
            Int64 _nUserID = 0;
            string _sUserName = "";
            string _sPassword = "";
            string _sNickName = "";

            private string _sExternalCode = "";

            private Int64 _nClinicID = 0;

            //private variables for Equipment
            string _sCode = "";
            string _sDescription = "";
            string _Qualifier = "";


            Int64 _nResourceTypeID = 0;
            #endregion

            #region "Properties"
            public Int64 ProviderID
            {
                get { return _nProviderID; }
                set { _nProviderID = value; }
            }
            public string FirstName
            {
                get { return _sFirstName; }
                set { _sFirstName = value; }
            }
            public string MiddleName
            {
                get { return _sMiddleName; }
                set { _sMiddleName = value; }
            }
            public string LastName
            {
                get { return _sLastName; }
                set { _sLastName = value; }
            }
            public string Gender
            {
                get { return _sGender; }
                set { _sGender = value; }
            }
            public string DEA
            {
                get { return _sDEA; }
                set { _sDEA = value; }
            }

            public string BMAddress1
            {
                get { return _sBMAddress1; }
                set { _sBMAddress1 = value; }
            }
            public string BMAddress2
            {
                get { return _sBMAddress2; }
                set { _sBMAddress2 = value; }
            }
            public string BMCity
            {
                get { return _sBMCity; }
                set { _sBMCity = value; }
            }
            public string BMState
            {
                get { return _sBMState; }
                set { _sBMState = value; }
            }
            public string BMZIP
            {
                get { return _sBMZIP; }
                set { _sBMZIP = value; }
            }

            public string BMAreaCode
            {
                get { return _sBMAreaCode; }
                set { _sBMAreaCode = value; }
            }

            public string BPracAddress1
            {
                get { return _sBPracAddress1; }
                set { _sBPracAddress1 = value; }
            }
            public string BPracAddress2
            {
                get { return _sBPracAddress2; }
                set { _sBPracAddress2 = value; }
            }
            public string BPracCity
            {
                get { return _sBPracCity; }
                set { _sBPracCity = value; }
            }
            public string BPracState
            {
                get { return _sBPracState; }
                set { _sBPracState = value; }
            }
            public string BPracZIP
            {
                get { return _sBPracZIP; }
                set { _sBPracZIP = value; }
            }

            public string BPracAreaCode
            {
                get { return _sBPracAreaCode; }
                set { _sBPracAreaCode = value; }
            }

            public string BMPhone
            {
                get { return _sBMPhone; }
                set { _sBMPhone = value; }
            }
            public string BMFAX
            {
                get { return _sBMFAX; }
                set { _sBMFAX = value; }
            }
            public string BMPager
            {
                get { return _sBMPager; }
                set { _sBMPager = value; }
            }
            public string BMEmail
            {
                get { return _sBMEmail; }
                set { _sBMEmail = value; }
            }
            public string BMURL
            {
                get { return _sBMURL; }
                set { _sBMURL = value; }
            }

            public string BPracPhone
            {
                get { return _sBpracPhone; }
                set { _sBpracPhone = value; }
            }
            public string BPracFAX
            {
                get { return _sBpracFAX; }
                set { _sBpracFAX = value; }
            }
            public string BPracPager
            {
                get { return _sBPracPager; }
                set { _sBPracPager = value; }
            }
            public string BPracEmail
            {
                get { return _sBPracEmail; }
                set { _sBPracEmail = value; }
            }
            public string BPracURL
            {
                get { return _sBPracURL; }
                set { _sBPracURL = value; }
            }

            public string Taxonomy
            {
                get { return _Taxonomy; }
                set { _Taxonomy = value; }
            }

            public string Qualifier
            {
                get { return _Qualifier; }
                set { _Qualifier = value; }
            }

            public string TaxonomyDesc
            {
                get { return _TaxonomyDesc; }
                set { _TaxonomyDesc = value; }
            }

            public string Mobile
            {
                get { return _sMobile; }
                set { _sMobile = value; }
            }
            public Image Signature
            {
                get { return _imgSignature; }
                set { _imgSignature = value; }
            }

            public string NPI
            {
                get { return _NPI; }
                set { _NPI = value; }
            }
            public string UPIN
            {

                get { return _UPIN; }
                set { _UPIN = value; }
            }
            public string SSN
            {
                get { return _SSN; }
                set { _SSN = value; }
            }
            public string EmployerID
            {
                get { return _EmployerID; }
                set { _EmployerID = value; }
            }
            public string StateMedicalNo
            {

                get { return _StateMedicalNo; }
                set { _StateMedicalNo = value; }
            }

            public string Prefix
            {

                get { return _Prefix; }
                set { _Prefix = value; }
            }

            public string Suffix
            {

                get { return _Suffix; }
                set { _Suffix = value; }
            }
            public Int64 ProviderTypeID
            {

                get { return _nProviderTypeID; }
                set { _nProviderTypeID = value; }
            }

            public Int64 UserID
            {

                get { return _nUserID; }
                set { _nUserID = value; }
            }

            public string UserName
            {

                get { return _sUserName; }
                set { _sUserName = value; }
            }
            public string Password
            {
                get { return _sPassword; }
                set { _sPassword = value; }
            }
            public string NickName
            {
                get { return _sNickName; }
                set { _sNickName = value; }
            }

            public string ExternalCode
            {
                get { return _sExternalCode; }
                set { _sExternalCode = value; }
            }

            public Int64 ClinicID
            {
                get { return _nClinicID; }
                set { _nClinicID = value; }
            }


            //for Equipment
            public string Code
            {
                get { return _sCode; }
                set { _sCode = value; }
            }

            public string Description
            {
                get { return _sDescription; }
                set { _sDescription = value; }
            }
            public Int64 ResourceTypeID
            {
                get { return _nResourceTypeID; }
                set { _nResourceTypeID = value; }
            }


            public string CompanyName
            {
                get { return _sCompanyName; }
                set { _sCompanyName = value; }
            }

            public string CompanyContactName
            {
                get { return _sCompanyContactName; }
                set { _sCompanyContactName = value; }
            }
            public string CompanyAddress1
            {
                get { return _sCompanyAddress1; }
                set { _sCompanyAddress1 = value; }
            }
            public string CompanyAddress2
            {
                get { return _sCompanyAddress2; }
                set { _sCompanyAddress2 = value; }
            }
            public string CompanyCity
            {
                get { return _sCompanyCity; }
                set { _sCompanyCity = value; }
            }
            public string CompanyState
            {
                get { return _sCompanyState; }
                set { _sCompanyState = value; }
            }
            public string CompanyZip
            {
                get { return _sCompanyZip; }
                set { _sCompanyZip = value; }
            }

            public string CompanyAreaCode
            {
                get { return _sCompanyAreaCode; }
                set { _sCompanyAreaCode = value; }
            }

            public string CompanyPhone
            {
                get { return _sCompanyPhone; }
                set { _sCompanyPhone = value; }
            }
            public string CompanyFax
            {
                get { return _sCompanyFax; }
                set { _sCompanyFax = value; }
            }
            public string CompanyEmail
            {
                get { return _sCompanyEmail; }
                set { _sCompanyEmail = value; }
            }

            //Two fields added by Anil on 20090310
            public string CompanyNPI
            {
                get { return _sCompanyNPI; }
                set { _sCompanyNPI = value; }
            }

            public string CompanyTaxID
            {
                get { return _sCompanyTaxID; }
                set { _sCompanyTaxID = value; }
            }
            //
            #endregion

        }

        public class Resource : IDisposable
        {
            #region " Declarations "

            private Int64 _ResourceID = 0;
            private string _Code = "";
            private string _Description = "";
            private string _ResourceTypeDescription = "";
            private ResourceType _ResourceType = ResourceType.Other;
            private Int64 _ResourceTypeID = 0;
            private bool _IsBlocked = false;
            private Int64 _ClinicID = 0;
            private Provider _Provider = null;
            private string _UserName = "";
            private bool _bIsTurnOffReminder = false;
            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            //  _encryptionKey Temporaryly Declared & Assigner Here 
            // has to Add it in Application Config Filie
            private const string _encryptionKey = "12345678";
            //

            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


            #endregion " Declarations "

            #region "Constructor & Destructor"

            public Resource(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
                _Provider = new Provider(_databaseconnectionstring);
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }


                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

                #endregion
      
            }

            public Resource(Int64 ResourceID, string DatabaseConnectionString)
            {
                _ResourceID = ResourceID;
                _databaseconnectionstring = DatabaseConnectionString;
                _Provider = new Provider(_databaseconnectionstring);
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

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

            ~Resource()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 ResourceID
            {
                get { return _ResourceID; }
                set { _ResourceID = value; }
            }

            public string Code
            {
                get { return _Code; }
                set { _Code = value; }
            }

            public string Description
            {
                get { return _Description; }
                set { _Description = value; }
            }

            public string ResourceTypeDescription
            {
                get { return _ResourceTypeDescription; }
                set { _ResourceTypeDescription = value; }
            }

            public ResourceType ResourceType
            {
                get { return _ResourceType; }
                set { _ResourceType = value; }
            }

            public Int64 ResourceTypeID
            {
                get { return _ResourceTypeID; }
                set { _ResourceTypeID = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public bool IsBlocked
            {
                get { return _IsBlocked; }
                set { _IsBlocked = value; }

            }

            public Provider ProviderDetail
            {
                get { return _Provider; }
                set { _Provider = value; }
            }

            public string UserName
            {
                get { return _UserName; }
                set { _UserName = value; }
            }

            public bool IsTurnOffReminder
            {
                get { return _bIsTurnOffReminder; }
                set { _bIsTurnOffReminder = value; }

            }
            #endregion

            #region Provider Methods

            public Int64 Add()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);

                //declare a variable for getting the resourceid for the inserted record.
                Object ResourceID;
                Int64 nResourceID = 0;

                try
                {
                    //first insert into the resource_mst table
                    oDBParameters.Add("@ResourceID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@Code", _Code, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@Description", _Description, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ResourceTypeID", _ResourceType.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@UserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@IsTurnOffReminders", _bIsTurnOffReminder, ParameterDirection.Input, SqlDbType.Bit);

                    oDB.Execute("AB_INUP_ResourceMst", oDBParameters, out  ResourceID);

                    if (ResourceID != null)
                        nResourceID = Convert.ToInt64(ResourceID);

                    if (_ResourceType == ResourceType.Provider)
                    {
                        if (nResourceID > 0)
                        {
                            //Temp code added to make User_MST Entry
                            //oDB.Execute_Query("Insert into user_MST (sLoginName,sPassword,nProviderID)Values('"+ _Provider.UserName.ToString() +"','"+ _Provider.Password.ToString() +"',"+ nResourceID +")");
                            //Temp code added to make User_MST Entry
                            if (AddProvider(nResourceID, false) == false)
                            { nResourceID = 0; }
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return 0;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                    ResourceID = null;
                }
                return nResourceID;
            }

            public bool Modify(Int64 ResourceID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                bool _result = false;
                oDB.Connect(false);

                try
                {
                    oDBParameters.Add("@ResourceID", ResourceID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@Code", _Code, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@Description", _Description, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ResourceTypeID", _ResourceType.GetHashCode() , ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@UserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@IsTurnOffReminders", _bIsTurnOffReminder, ParameterDirection.Input, SqlDbType.Bit);

                    int _r = oDB.Execute("AB_INUP_ResourceMst", oDBParameters);
                    if (_r > 0)
                    {

                        if (_ResourceType == ResourceType.Provider)
                        {
                            if (ResourceID > 0)
                            {
                                if (AddProvider(ResourceID, true) == true)
                                { _result = true; }
                            }
                        }
                        else
                        {
                            _result = true;
                        }
                    }

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
                return _result;
            }

            private bool AddProvider(Int64 ResourceID, bool saveflag)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                bool _result = false;
                byte[] arrImage = null;
                DataTable _dtIfPresent = null;
                gloSecurity.ClsEncryption oEncryption = null;
                try
                {
                    oDBParameters = new gloDatabaseLayer.DBParameters();

                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@ProviderID", ResourceID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@FirstName", _Provider.FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@MiddleName", _Provider.MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@LastName", _Provider.LastName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@Gender", _Provider.Gender, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@DEA", _Provider.DEA, ParameterDirection.Input, SqlDbType.VarChar);

                    oDBParameters.Add("@BMAddress1", _Provider.BMAddress1, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMAddress2", _Provider.BMAddress2, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMCity", _Provider.BMCity, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMState", _Provider.BMState, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMZIP", _Provider.BMZIP, ParameterDirection.Input, SqlDbType.VarChar);

                    oDBParameters.Add("@BPracAddress1", _Provider.BPracAddress1, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracAddress2", _Provider.BPracAddress2, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracCity", _Provider.BPracCity, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracState", _Provider.BPracState, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracZIP", _Provider.BPracZIP, ParameterDirection.Input, SqlDbType.VarChar);
                    //@BmPhoneNo,@BMFAX ,@BMPagerNo ,@BMEmail ,@BMURL,@BPracPhoneNo ,@BPracFAX ,@BPracPagerNo , @BPracEmail,@BPracURL

                    oDBParameters.Add("@BmPhoneNo", _Provider.BMPhone, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMFAX", _Provider.BMFAX, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMPagerNo", _Provider.BMPager, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMEmail", _Provider.BMEmail, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BMURL", _Provider.BMURL, ParameterDirection.Input, SqlDbType.VarChar);

                    oDBParameters.Add("@BPracPhoneNo", _Provider.BPracPhone, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracFAX", _Provider.BPracFAX, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracPagerNo", _Provider.BPracPager, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracEmail", _Provider.BPracEmail, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@BPracURL", _Provider.BPracURL, ParameterDirection.Input, SqlDbType.VarChar);

                    oDBParameters.Add("@Taxonomy", _Provider.Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@MobileNo", _Provider.Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    //if signature image is null then it would take null value by default else pass the value
                    if (_Provider.Signature != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        _Provider.Signature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        try
                        {
                            ms.Flush();
                        }
                        catch
                        {
                        }
                        arrImage = ms.ToArray();
                       
                        oDBParameters.Add("@Signature", arrImage, ParameterDirection.Input, SqlDbType.Image);
                        try
                        {
                            ms.Close();
                            ms.Dispose();
                            ms = null;
                        }
                        catch
                        {
                        }
                    }



                    oDBParameters.Add("@ProviderTypeID", _Provider.ProviderTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sNPI", _Provider.NPI, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sUPIN", _Provider.UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sMedicalLicenseNo", _Provider.StateMedicalNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@Prefix", _Provider.Prefix, ParameterDirection.Input, SqlDbType.VarChar);

                    //pass editflag = 0 for inserting provider record
                    oDBParameters.Add("@editflag", saveflag, ParameterDirection.Input, SqlDbType.Bit);

                    //execute the Insert Provider stored procedure
                    int _r = oDB.Execute("PA_InUpProvider", oDBParameters);

                    if (_r > 0)
                    {
                        //Temp code added to make User_MST Entry

                        //_dtIfPresent = new DataTable();
                        oEncryption = new gloSecurity.ClsEncryption();
                        // Is Provider newly added as Resource then set max as PROVIDERID    
                        Int64 _idRes = 0;
                        if (_ResourceID == 0)
                        {
                            _idRes = Convert.ToInt64(oDB.ExecuteScalar_Query("select max(nProviderId) as proID from Provider_MST WITH(NOLOCK)"));
                        }
                        else
                            _idRes = _ResourceID;


                       // oDB.Retrive_Query("Select * from user_MST where nProviderID= " + _idRes + "", out _dtIfPresent); //Removed Select *
                        oDB.Retrive_Query("Select nUserID from user_MST  WITH(NOLOCK) where nProviderID= " + _idRes + "", out _dtIfPresent);
                        if (_dtIfPresent != null && _dtIfPresent.Rows.Count > 0)
                        {
                            //oDB.Execute_Query("Update user_MST SET sLoginName = '" + _Provider.UserName.ToString() + "',sPassword = '" + oEncryption.EncryptToBase64String(_Provider.Password.ToString(), _encryptionKey) + "' where nProviderID= " + _idRes + "");
                            oDB.Execute_Query("Update user_MST SET sLoginName = '" + _Provider.UserName.ToString() + "',sPassword = '" + oEncryption.EncryptToBase64String(_Provider.Password.ToString(), _encryptionKey) + "', sFirstName = '" + _Provider.FirstName + "',sMiddleName = '" + _Provider.MiddleName + "', sLastName = '" + _Provider.LastName + "' ," +
                                             " sGender = '" + _Provider.Gender + "',sAddress = '" + _Provider.BMAddress1 + "',sAddress2 = '" + _Provider.BMAddress2+ "', sCity = '" + _Provider.BMCity + "',sState = '" + _Provider.BMState + " ', sZIP = '" + _Provider.BMZIP + "',sPhoneNo = '" + _Provider.BMPhone + "',sMobileNo = '" + _Provider.Mobile + "',sFAX = '" + _Provider.BMFAX + "',sEmail = '" + _Provider.BMEmail + "'" +
                                             " where nProviderID= " + _idRes + "");

                        }
                        else
                        {
                            Int64 _intUserID = Convert.ToInt64(oDB.ExecuteScalar_Query("select max(nuserID) from user_MST as nuserID  WITH(NOLOCK) ")) + 1;
                            //// oDB.Execute_Query("Insert into user_MST (nuserID,sLoginName,sPassword,nProviderID,nAccessDenied, nAdministrator, IsAuditTrail)Values(" + _intUserID + ",'" + _Provider.UserName.ToString() + "','" + oEncryption.EncryptToBase64String(_Provider.Password.ToString(), _encryptionKey) + "'," + _idRes + ",'FALSE','TRUE','TRUE')");
                            //oDB.Execute_Query("Insert into user_MST (nuserID,sLoginName,sPassword,nProviderID,sFirstName,sMiddleName,sLastName,sGender,sAddress,sAddress2,sCity,sState,sZIP,sPhoneNo,sMobileNo,sFAX,sEmail,nAccessDenied, nAdministrator, IsAuditTrail) " +
                            //                " Values(" + _intUserID + ",'" + _Provider.UserName.ToString() + "','" + oEncryption.EncryptToBase64String(_Provider.Password.ToString(), _encryptionKey) + "'," + _idRes + ",'" + _Provider.FirstName + "','" + _Provider.MiddleName + "','" + _Provider.LastName + "','" + _Provider.Gender + "','" + _Provider.Address + "','" + _Provider.Street +
                            //                "','" + _Provider.City + "','" + _Provider.State + "','" + _Provider.ZIP + "','" + _Provider.Phone + "','" + _Provider.Mobile + "','" + _Provider.FAX + "','" + _Provider.Email + "','FALSE','FALSE','FALSE')");

                            oDB.Execute_Query("Insert into user_MST (nuserID,sLoginName,sPassword,nProviderID,sFirstName,sMiddleName,sLastName,sGender,sAddress,sAddress2,sCity,sState,sZIP,sPhoneNo,sMobileNo,sFAX,sEmail,nAccessDenied, nAdministrator, IsAuditTrail,nClinicID) " +
                                            " Values(" + _intUserID + ",'" + _Provider.UserName.ToString() + "','" + oEncryption.EncryptToBase64String(_Provider.Password.ToString(), _encryptionKey) + "'," + _idRes + ",'" + _Provider.FirstName + "','" + _Provider.MiddleName + "','" + _Provider.LastName + "','" + _Provider.Gender + "','" + _Provider.BMAddress1 + "','" + _Provider.BMAddress2+
                                            "','" + _Provider.BMCity + "','" + _Provider.BMState + "','" + _Provider.BMZIP + "','" + _Provider.BMPhone + "','" + _Provider.Mobile + "','" + _Provider.BMFAX + "','" + _Provider.BMEmail + "','FALSE','FALSE','FALSE'," + this.ClinicID + ")");
                        }

                        //Temp code added to make User_MST Entry
                        _result = true;
                    }

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                    arrImage = null;
                    if (_dtIfPresent != null) { _dtIfPresent.Dispose(); _dtIfPresent = null; }
                    if (oEncryption != null) { oEncryption.Dispose(); oEncryption = null; }
                }
                return _result;

            }

            public bool Block(Int64 ResourceID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                bool _result = false;
                oDB.Connect(false);

                try
                {
                    _strSQL = "Update AB_Resource_MST set  bitIsBlocked = 1 where nResourceID = " + ResourceID;
                    int _r = oDB.Execute_Query(_strSQL);
                    if (_r > 0)
                    {
                        //When Resource is Provider & if its blocked also its Access Is Denied to System.
                        _strSQL = "Update user_MST set  nAccessdenied = 1 where nproviderID = " + ResourceID;
                        _r = oDB.Execute_Query(_strSQL);

                        _result = true;
                    }
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    _strSQL = null;
                }
                return _result;
            }

            public bool Unblock(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                string _strSQL = "";
                int _r = 0;
                try
                {
                    oDB.Connect(false);
                    strQuery = " UPDATE AB_Resource_MST SET bitIsBlocked = '" + false + "' WHERE  nResourceID = " + ID + " ";
                    int _result = oDB.Execute_Query(strQuery);
                    if (_result > 0)
                    {
                        //Make Access Denied false to allow access to system if resource is provider.
                        _strSQL = "Update user_MST set  nAccessdenied = 0 where nproviderID = " + ID;
                        _r = oDB.Execute_Query(_strSQL);

                        //_result = true;
                    }
                    return Convert.ToBoolean(_result);
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    _strSQL = null;
                }
            }

            /* public bool Delete(Int64 ID)
             {
                 gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                 string strQuery = "";
                 try
                 {
                     //Transaction need to be implemented here.

                     //delete from master table.s
                     oDB.Connect(false);
                     strQuery = "delete from AB_Resource_MST where nResourceID =" + ID;

                     int result = oDB.Execute_Query(strQuery);
                     if (result <= 0)
                     {
                         return false;
                     }
                     //delete from Resource_Provider table for resource is provider
                     strQuery = "delete from Provider_MST where nProviderID =" + ID;

                      result = oDB.Execute_Query(strQuery);
                     if (result <= 0)
                     {
                         return false;
                     }
                     //delete from User_MST if resource is provider
                     strQuery = "delete from User_MST where nProviderID =" + ID;

                      result = oDB.Execute_Query(strQuery);
                     if (result <= 0)
                     {
                         return false;
                     }
                     return true;
                 }
                 catch (gloDatabaseLayer.DBException dbErr)
                 {
                     dbErr.ERROR_Log(dbErr.ToString());
                     return false;

                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("ERROR : " + ex.Message, "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }
                 finally
                 {
                     oDB.Disconnect();
                     oDB.Dispose();
                 }
             } */

            public bool Delete(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameter = new gloDatabaseLayer.DBParameters();
                int result = 0;
                try
                {
                    oDB.Connect(false);

                    //Resource ID parameter 
                    //oParameter.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                    string SqlQuery = " Delete  from AB_Resource_MST WHERE nResourceID= " + ID + " ";
                    result =(Int32)oDB.Execute_Query(SqlQuery);
                    SqlQuery = null;
                    if (result <= 0)
                    {
                        return false;
                    }
                    return true;
                }
                catch (gloDatabaseLayer.DBException dbErr)
                {
                    dbErr.ERROR_Log(dbErr.ToString());
                    return false;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oParameter.Dispose();
                }
            }

            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                int _result = 0;
                gloDatabaseLayer.DBParameters oParameter;
                DataTable dt = null;
                try
                {
                    oDB.Connect(false);
                    strQuery = "select nResourceID from AB_Resource_MST  WITH(NOLOCK) where bitIsBlocked='true' AND nClinicID= " + this.ClinicID + " ";
                    oDB.Retrive_Query(strQuery, out dt);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Int64 _tempResourceId = 0;
                            _tempResourceId = Convert.ToInt64(dt.Rows[i][0]);

                            if (CanDelete(_tempResourceId))
                            {
                                oParameter = new gloDatabaseLayer.DBParameters();
                                oParameter.Add("@ID", _tempResourceId, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AB_Delete_Resource", oParameter);
                                oParameter.Dispose();
                            }
                        }
                    }

                    return Convert.ToBoolean(_result);

                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dt.Dispose();
                    strQuery = null;
                }
            }

            //public bool DeleteAll()
            //{
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    string strQuery = "";
            //    int _result = 0;
            //    DataTable dt = new DataTable();
            //    try
            //    {
            //        oDB.Connect(false);
            //        strQuery = " select * FROM AB_Resource_MST WHERE bitIsBlocked = '" + true + "'AND nClinicID = " + this.ClinicID + "";
            //        oDB.Retrive_Query(strQuery, out dt);
            //        //Addes
            //        //---Add All  tables for the Resource master
            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //            {
            //                strQuery = "delete from Provider_MST where nProviderID =" + Convert.ToInt64(dt.Rows[i]["nResourceID"]);
            //                _result = oDB.Execute_Query(strQuery);
            //                if (_result <= 0)
            //                {
            //                    return false;
            //                }

            //                strQuery = "delete from User_MST where nProviderID =" + Convert.ToInt64(dt.Rows[i]["nResourceID"]);
            //                _result = oDB.Execute_Query(strQuery);
            //                if (_result <= 0)
            //                {
            //                    return false;
            //                }

            //            }
            //        }

            //        //Delete the Associated resources from Master Table.
            //        strQuery = " DELETE FROM AB_Resource_MST WHERE bitIsBlocked = '" + true + "'AND nClinicID = " + this.ClinicID + " ";
            //        _result = oDB.Execute_Query(strQuery);

            //        return Convert.ToBoolean(_result);

            //    }
            //    catch (gloDatabaseLayer.DBException dbEx)
            //    {
            //        dbEx.ERROR_Log(dbEx.ToString());
            //        return false;
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }
            //}

            public bool IsExists(Int64 ResourceID, string ResourceDescription)
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";
                object _intresult = null;
                try
                {

                    if (ResourceID == 0)
                    {
                        //for add 
                        // _sqlQuery = "SELECT Count(nResourceID) FROM AB_Resource_MST WHERE sDescription ='" + ResourceDescription + "'";
                        _sqlQuery = "SELECT Count(nResourceID) FROM AB_Resource_MST  WITH(NOLOCK) WHERE sDescription ='" + ResourceDescription.Replace("'", "''") + "' AND nClinicID =" + this.ClinicID + " ";
                        //
                    }
                    else
                    {
                        //for modify
                        _sqlQuery = "SELECT Count(nResourceID) FROM AB_Resource_MST  WITH(NOLOCK) WHERE (sDescription ='" + ResourceDescription.Replace("'", "''") + "' AND nResourceID <> " + ResourceID + ") AND nClinicID = " + this.ClinicID + " ";
                        //
                    }

                    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = true;
                            }
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    _sqlQuery = null;
                    _intresult = null;
                }
                return _result;
            }

            public void GetResource(Int64 ResourceID)
            {
                //use the _nProviderID
                DataTable dtResource = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //int _ResType = 0;
                String _strSQL = "";

                oDB.Connect(false);

                try
                {
                    //get the provider details in the datatable -- dtProvider
                   // _strSQL = "select * from AB_Resource_MST where nResourceID =" + ResourceID; //Removed  Select * 
                    _strSQL = "select sCode,sDescription,sUserName,nResourceTypeID,bitIsBlocked,nClinicID,bIsTurnOffReminders from AB_Resource_MST  WITH(NOLOCK) where nResourceID =" + ResourceID;
                    oDB.Retrive_Query(_strSQL, out dtResource);

                    if (dtResource.Rows[0]["sCode"] != null)
                        _Code = dtResource.Rows[0]["sCode"].ToString();

                    if (dtResource.Rows[0]["sDescription"] != null)
                        _Description = dtResource.Rows[0]["sDescription"].ToString();

                    if (dtResource.Rows[0]["sUserName"] != null)
                        _UserName = dtResource.Rows[0]["sUserName"].ToString();

                    if (dtResource.Rows[0]["nResourceTypeID"] != null)
                        _ResourceTypeID = Convert.ToInt64(dtResource.Rows[0]["nResourceTypeID"]);

                    if (dtResource.Rows[0]["bitIsBlocked"] != null)
                    {
                        if (Convert.ToInt16(dtResource.Rows[0]["bitIsBlocked"]) == 0)
                            _IsBlocked = false;
                        else
                            _IsBlocked = true;
                    }

                    if (dtResource.Rows[0]["bIsTurnOffReminders"] != null)
                    {
                        if (Convert.ToInt16(dtResource.Rows[0]["bIsTurnOffReminders"]) == 0)
                            _bIsTurnOffReminder = false;
                        else
                            _bIsTurnOffReminder = true;
                    }

                    

                    if (dtResource.Rows[0]["nClinicID"] != null)
                        _ClinicID = Convert.ToInt64(dtResource.Rows[0]["nClinicID"]);

                    _ResourceType = ResourceType.General;

                    //DataTable oDT = new DataTable();

                    //_strSQL = "SELECT ISNULL(AB_ResourceType_MST.nResourceType, 0) AS nResourceType, ISNULL(AB_ResourceType_MST.sResourceTypeDescription,'') AS sResourceTypeDescription FROM AB_Resource_MST INNER JOIN AB_ResourceType_MST ON AB_Resource_MST.nResourceTypeID = AB_ResourceType_MST.nResourceTypeID where nResourceID =" + ResourceID;

                    //oDB.Retrive_Query(_strSQL, out oDT);

                    //if (oDT != null)
                    //{
                    //    if (oDT.Rows.Count > 0)
                    //    {
                    //        _ResType = Convert.ToInt32(oDT.Rows[0]["nResourceType"].ToString());
                    //        _ResourceTypeDescription = oDT.Rows[0]["sResourceTypeDescription"].ToString();

                    //        if (_ResType == 1) { _ResourceType = ResourceType.Provider; }
                    //        else if (_ResType == 2) { _ResourceType = ResourceType.Equipment; }
                    //        else if (_ResType == 3) { _ResourceType = ResourceType.Other; }

                    //    }
                    //}
                    //oDT.Dispose();


                    //if (_ResType == ResourceType.Provider.GetHashCode())
                    //{
                    //    GetProviderDetails(ResourceID);
                    //}

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {

                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    if (dtResource != null) { dtResource.Dispose(); dtResource = null; }
                    _strSQL = null;
                }
            }

            public void GetProviderDetails(Int64 ResourceID)
            {
                //use the _nProviderID
                DataTable dtProvider = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                Byte[] arrImage = null;
                try
                {

                    //_Provider

                    oDB.Connect(false);

                    //get the provider details in the datatable -- dtProvider
                   // _strSQL = "select * from Provider_MST where nProviderID =" + ResourceID;
                      _strSQL = "select sFirstName, sMiddleName, sLastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, " +
                                " imgSignature, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode  " +
                                " from Provider_MST  WITH(NOLOCK) where nProviderID =" + ResourceID;
                    oDB.Retrive_Query(_strSQL, out dtProvider);

                    //fill the Provider object with the Provider details from the datatable
                    //and return the Provider object
                    if (dtProvider != null && dtProvider.Rows.Count > 0)
                    {
                        //  oProvider._nProviderID = dtProvider.Rows[0]["nProviderID"];
                        if (dtProvider.Rows[0]["sFirstName"] != null)
                            _Provider.FirstName = dtProvider.Rows[0]["sFirstName"].ToString();

                        if (dtProvider.Rows[0]["sMiddleName"] != null)
                            _Provider.MiddleName = dtProvider.Rows[0]["sMiddleName"].ToString();

                        if (dtProvider.Rows[0]["sLastName"] != null)
                            _Provider.LastName = dtProvider.Rows[0]["sLastName"].ToString();

                        if (dtProvider.Rows[0]["sGender"] != null)
                            _Provider.Gender = dtProvider.Rows[0]["sGender"].ToString();

                        if (dtProvider.Rows[0]["sDEA"] != null)
                            _Provider.DEA = dtProvider.Rows[0]["sDEA"].ToString();

                        if (dtProvider.Rows[0]["sAddress"] != null)
                            _Provider.BMAddress1 = dtProvider.Rows[0]["sAddress"].ToString();

                        if (dtProvider.Rows[0]["sStreet"] != null)
                            _Provider.BMAddress2 = dtProvider.Rows[0]["sStreet"].ToString();

                        if (dtProvider.Rows[0]["sCity"] != null)
                            _Provider.BMCity = dtProvider.Rows[0]["sCity"].ToString();

                        if (dtProvider.Rows[0]["sState"] != null)
                            _Provider.BMState = dtProvider.Rows[0]["sState"].ToString();

                        if (dtProvider.Rows[0]["sZIP"] != null)
                            _Provider.BMZIP = dtProvider.Rows[0]["sZIP"].ToString();

                        //if (dtProvider.Rows[0]["sZIP"] != null)
                        //    _Provider.BMZIP = dtProvider.Rows[0]["sZIP"].ToString();

                        if (dtProvider.Rows[0]["sPhoneNo"] != null)
                            _Provider.BMPhone = dtProvider.Rows[0]["sPhoneNo"].ToString();

                        if (dtProvider.Rows[0]["sFAX"] != null)
                            _Provider.BMFAX = dtProvider.Rows[0]["sFAX"].ToString();

                        if (dtProvider.Rows[0]["sMobileNo"] != null)
                            _Provider.Mobile = dtProvider.Rows[0]["sMobileNo"].ToString();

                        if (dtProvider.Rows[0]["sPagerNo"] != null)
                            _Provider.BMPager = dtProvider.Rows[0]["sPagerNo"].ToString();

                        if (dtProvider.Rows[0]["sEmail"] != null)
                            _Provider.BMEmail = dtProvider.Rows[0]["sEmail"].ToString();

                        if (dtProvider.Rows[0]["sURL"] != null)
                            _Provider.BMURL = dtProvider.Rows[0]["sURL"].ToString();
                        //if (dtProvider.Rows[0]["imgSignature"] != null)
                        //    _Provider.Signature = dtProvider.Rows[0]["imgSignature"];
                        // //   _Provider.Signature  = null;
                        ////   _imgSignature  = dtProvider.Rows[0]["imgSignature"];
                        if (System.DBNull.Value != dtProvider.Rows[0]["imgSignature"])
                        {
                            arrImage = (Byte[])((dtProvider.Rows[0]["imgSignature"]));
                            MemoryStream ms = new MemoryStream(arrImage);
                            _Provider.Signature = Image.FromStream(ms);
                            try
                            {
                                ms.Close();
                                ms.Dispose();
                                ms = null;
                            }
                            catch
                            {

                            }
                        }


                        if (dtProvider.Rows[0]["nProviderType"] != null)
                            _Provider.ProviderTypeID = Convert.ToInt64(dtProvider.Rows[0]["nProviderType"]);

                        if (dtProvider.Rows[0]["sNPI"] != null)
                            _Provider.NPI = dtProvider.Rows[0]["sNPI"].ToString();

                        if (dtProvider.Rows[0]["sUPIN"] != null)
                            _Provider.UPIN = dtProvider.Rows[0]["sUPIN"].ToString();

                        if (dtProvider.Rows[0]["sMedicalLicenseNo"] != null)
                            _Provider.StateMedicalNo = dtProvider.Rows[0]["sMedicalLicenseNo"].ToString();


                        if (dtProvider.Rows[0]["sPrefix"] != null)
                            _Provider.Prefix = dtProvider.Rows[0]["sPrefix"].ToString();

                        if (dtProvider.Rows[0]["sExternalCode"] != null)
                            _Provider.ExternalCode = dtProvider.Rows[0]["sExternalCode"].ToString();
                        //Temp code added to make User_MST Entry
                        dtProvider = new DataTable();
                        _strSQL = "";
                       // _strSQL = "select * from user_MST where nProviderID =" + ResourceID;
                        _strSQL = "select sLoginName,sPassword from user_MST  WITH(NOLOCK) where nProviderID =" + ResourceID;
                        oDB.Retrive_Query(_strSQL, out dtProvider);
                        if (dtProvider.Rows.Count > 0)
                            if (dtProvider.Rows[0]["sLoginName"] != null)
                            {
                                _Provider.UserName = dtProvider.Rows[0]["sLoginName"].ToString();

                                gloSecurity.ClsEncryption oEncryption = new gloSecurity.ClsEncryption();
                                _Provider.Password = oEncryption.DecryptFromBase64String(dtProvider.Rows[0]["sPassword"].ToString(), _encryptionKey);
                                if (oEncryption != null) { oEncryption.Dispose(); oEncryption = null; }
                            }

                        //Temp code added to make User_MST Entry
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {

                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                    _strSQL = null;
                    arrImage = null;
                }



            }//GetProviderDetails()

            public Provider GetProviderDetail(Int64 ResourceID)
            {
                //use the _nProviderID
                DataTable dtProvider = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";

                try
                {

                    //_Provider

                    oDB.Connect(false);

                    ////get the provider details in the datatable -- dtProvider
                    //_strSQL = "SELECT  nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, "+
                    //          " sBusinessState, sBusinessZIP, sPracticeAddressline1, sPracticeAddressline2, sPracticeCity, sPracticeState, sPracticeZIP, sBusPhoneNo, sBusFAX, "+
                    //          " sBusPagerNo, sBusEmail, sBusURL, sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL, sMobileNo, imgSignature, nProviderType, "+
                    //          " sNPI, sUPIN, sSSN, sEmployerID, sMedicalLicenseNo, sPrefix, sExternalCode, sServiceLevel, dtActiveStartTime, dtActiveEndTime, sTaxonomy, sSPIID, nClinicID " +
                    //          " , sTaxonomyDesc, sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity, sCompanyState, sCompanyZIP," +
                    //          " sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName,sCompanyNPI,sCompanyTaxID, sBusinessContactName, sPracContactName, sSuffix, bIsblocked " +
                    //          " FROM Provider_MST where nProviderID =" + ResourceID;

                    _strSQL = " SELECT " +
                    " nProviderID, sFirstName, sMiddleName, sLastName,sGender, sDEA,  " +
                    " sMobileNo,imgSignature, nProviderType,sNPI, sUPIN, sSSN, sEmployerID, sMedicalLicenseNo, sPrefix,  " +
                    " sExternalCode, sServiceLevel, dtActiveStartTime, dtActiveEndTime, sTaxonomy, sSPIID, nClinicID,  " +
                    " sTaxonomyDesc,sSuffix, bIsblocked, " +
                    
                    //" --Business Address Details " +
                    " sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, " +
                    " sBusinessState, sBusinessZIP,ISNULL(sBusinessAreaCode,'') AS sBusinessAreaCode, " +
                    " sBusPhoneNo, sBusFAX,sBusPagerNo, sBusEmail, sBusURL,sBusinessContactName,  " +
                    
                    //" --Practice Address Details " +
                    " sPracticeAddressline1, sPracticeAddressline2, sPracticeCity,  " +
                    " sPracticeState, sPracticeZIP,ISNULL(sPracticeAreaCode,'') AS sPracticeAreaCode, " +
                    " sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL,sPracContactName, " +
                   
                    //" --Company Address Details " +
                    " sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity,  " +
                    " sCompanyState, sCompanyZIP,ISNULL(sCompanyAreaCode,'') AS sCompanyAreaCode, " +
                    " sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName,sCompanyNPI,sCompanyTaxID " +
                    " FROM Provider_MST  WITH(NOLOCK) where nProviderID =" + ResourceID + "";

                    oDB.Retrive_Query(_strSQL, out dtProvider);
                    if (dtProvider != null && dtProvider.Rows.Count > 0)
                    {
                        //fill the Provider object with the Provider details from the datatable
                        //and return the Provider object

                        //  oProvider._nProviderID = dtProvider.Rows[0]["nProviderID"];
                        if (dtProvider.Rows[0]["sFirstName"] != null)
                            _Provider.FirstName = dtProvider.Rows[0]["sFirstName"].ToString();

                        if (dtProvider.Rows[0]["sMiddleName"] != null)
                            _Provider.MiddleName = dtProvider.Rows[0]["sMiddleName"].ToString();

                        if (dtProvider.Rows[0]["sLastName"] != null)
                            _Provider.LastName = dtProvider.Rows[0]["sLastName"].ToString();

                        if (dtProvider.Rows[0]["sGender"] != null)
                            _Provider.Gender = dtProvider.Rows[0]["sGender"].ToString();

                        if (dtProvider.Rows[0]["sDEA"] != null)
                            _Provider.DEA = dtProvider.Rows[0]["sDEA"].ToString();

                        if (dtProvider.Rows[0]["sBusinessAddressline1"] != null)
                            _Provider.BMAddress1 = dtProvider.Rows[0]["sBusinessAddressline1"].ToString();

                        if (dtProvider.Rows[0]["sBusinessAddressline2"] != null)
                            _Provider.BMAddress2 = dtProvider.Rows[0]["sBusinessAddressline2"].ToString();

                        if (dtProvider.Rows[0]["sBusinessCity"] != null)
                            _Provider.BMCity = dtProvider.Rows[0]["sBusinessCity"].ToString();

                        if (dtProvider.Rows[0]["sBusinessState"] != null)
                            _Provider.BMState = dtProvider.Rows[0]["sBusinessState"].ToString();

                        if (dtProvider.Rows[0]["sBusinessZIP"] != null)
                            _Provider.BMZIP = dtProvider.Rows[0]["sBusinessZIP"].ToString();

                        if (dtProvider.Rows[0]["sBusinessAreaCode"] != null)
                            _Provider.BMAreaCode = dtProvider.Rows[0]["sBusinessAreaCode"].ToString();
                        
                        if (dtProvider.Rows[0]["sBusPhoneNo"] != null)
                            _Provider.BMPhone = dtProvider.Rows[0]["sBusPhoneNo"].ToString();

                        if (dtProvider.Rows[0]["sBusFAX"] != null)
                            _Provider.BMFAX = dtProvider.Rows[0]["sBusFAX"].ToString();

                        if (dtProvider.Rows[0]["sMobileNo"] != null)
                            _Provider.Mobile = dtProvider.Rows[0]["sMobileNo"].ToString();

                        if (dtProvider.Rows[0]["sBusPagerNo"] != null)
                            _Provider.BMPager = dtProvider.Rows[0]["sBusPagerNo"].ToString();

                        if (dtProvider.Rows[0]["sBusEmail"] != null)
                            _Provider.BMEmail = dtProvider.Rows[0]["sBusEmail"].ToString();

                        if (dtProvider.Rows[0]["sBusURL"] != null)
                            _Provider.BMURL = dtProvider.Rows[0]["sBusURL"].ToString();

                        ////Added by Anil on 20090219 For setting these properties
          
                       

                        ////Retrieve Practice location details
                        if (dtProvider.Rows[0]["sPracticeAddressline1"] != null)
                            _Provider.BPracAddress1 = dtProvider.Rows[0]["sPracticeAddressline1"].ToString();

                        if (dtProvider.Rows[0]["sPracticeAddressline2"] != null)
                            _Provider.BPracAddress2 = dtProvider.Rows[0]["sPracticeAddressline2"].ToString();

                        if (dtProvider.Rows[0]["sPracticeCity"] != null)
                            _Provider.BPracCity = dtProvider.Rows[0]["sPracticeCity"].ToString();

                        if (dtProvider.Rows[0]["sPracticeState"] != null)
                            _Provider.BPracState = dtProvider.Rows[0]["sPracticeState"].ToString();

                        if (dtProvider.Rows[0]["sPracticeZIP"] != null)
                            _Provider.BPracZIP = dtProvider.Rows[0]["sPracticeZIP"].ToString();

                        if (dtProvider.Rows[0]["sPracticeAreaCode"] != null)
                            _Provider.BPracAreaCode = dtProvider.Rows[0]["sPracticeAreaCode"].ToString();

                        if (dtProvider.Rows[0]["sPracPhoneNo"] != null)
                            _Provider.BPracPhone = dtProvider.Rows[0]["sPracPhoneNo"].ToString();

                        if (dtProvider.Rows[0]["sPracFAX"] != null)
                            _Provider.BPracFAX = dtProvider.Rows[0]["sPracFAX"].ToString();

                        if (dtProvider.Rows[0]["sPracPagerNo"] != null)
                            _Provider.BPracPager = dtProvider.Rows[0]["sPracPagerNo"].ToString();

                        if (dtProvider.Rows[0]["sPracEmail"] != null)
                            _Provider.BPracEmail = dtProvider.Rows[0]["sPracEmail"].ToString();

                        if (dtProvider.Rows[0]["sPracURL"] != null)
                            _Provider.BPracURL = dtProvider.Rows[0]["sPracURL"].ToString();

 //      " , sTaxonomyDesc, sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity, sCompanyState, sCompanyZIP," +
 //      " sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName, sBusinessContactName, sPracContactName, sSuffix, bIsblocked " +

                        ////Retrieve company details
                        if (dtProvider.Rows[0]["sCompanyName"] != null)
                            _Provider.CompanyName = dtProvider.Rows[0]["sCompanyName"].ToString();

                        if (dtProvider.Rows[0]["sCompanyContactName"] != null)
                            _Provider.CompanyContactName = dtProvider.Rows[0]["sCompanyContactName"].ToString();

                        if (dtProvider.Rows[0]["sCompanyAddressline1"] != null)
                            _Provider.CompanyAddress1 = dtProvider.Rows[0]["sCompanyAddressline1"].ToString();

                        if (dtProvider.Rows[0]["sCompanyAddressline2"] != null)
                            _Provider.CompanyAddress2 = dtProvider.Rows[0]["sCompanyAddressline2"].ToString();

                        if (dtProvider.Rows[0]["sCompanyCity"] != null)
                            _Provider.CompanyCity = dtProvider.Rows[0]["sCompanyCity"].ToString();

                        if (dtProvider.Rows[0]["sCompanyState"] != null)
                            _Provider.CompanyState = dtProvider.Rows[0]["sCompanyState"].ToString();

                        if (dtProvider.Rows[0]["sCompanyZIP"] != null)
                            _Provider.CompanyZip = dtProvider.Rows[0]["sCompanyZIP"].ToString();

                        if (dtProvider.Rows[0]["sCompanyAreaCode"] != null)
                            _Provider.CompanyAreaCode = dtProvider.Rows[0]["sCompanyAreaCode"].ToString();

                        if (dtProvider.Rows[0]["sCompanyPhone"] != null)
                            _Provider.CompanyPhone = dtProvider.Rows[0]["sCompanyPhone"].ToString();

                        if (dtProvider.Rows[0]["sCompanyFax"] != null)
                            _Provider.CompanyFax = dtProvider.Rows[0]["sCompanyFax"].ToString();

                        if (dtProvider.Rows[0]["sCompanyEmail"] != null)
                            _Provider.CompanyEmail = dtProvider.Rows[0]["sCompanyEmail"].ToString();

                        //Added by Anil on 20090310
                        if (dtProvider.Rows[0]["sCompanyNPI"] != null)
                            _Provider.CompanyNPI = dtProvider.Rows[0]["sCompanyNPI"].ToString();

                        if (dtProvider.Rows[0]["sCompanyTaxID"] != null)
                            _Provider.CompanyTaxID = dtProvider.Rows[0]["sCompanyTaxID"].ToString();

                        /////////////////////////////////////////////////////////// Upto here Added by Anil on 20090219


                        //if (dtProvider.Rows[0]["imgSignature"] != null)
                        //    _Provider.Signature = dtProvider.Rows[0]["imgSignature"];
                        // //   _Provider.Signature  = null;
                        ////   _imgSignature  = dtProvider.Rows[0]["imgSignature"];
                        if (System.DBNull.Value != dtProvider.Rows[0]["imgSignature"])
                        {
                            Byte[] arrImage = (Byte[])((dtProvider.Rows[0]["imgSignature"]));
                            MemoryStream ms = new MemoryStream(arrImage);
                            _Provider.Signature = Image.FromStream(ms);
                            try
                            {
                                ms.Close();
                                ms.Dispose();
                                ms = null;
                            }
                            catch
                            {

                            }
                            finally
                            { arrImage = null; }
                        }


                        if (dtProvider.Rows[0]["nProviderType"] != null)
                            _Provider.ProviderTypeID = Convert.ToInt64(dtProvider.Rows[0]["nProviderType"]);

                        if (dtProvider.Rows[0]["sNPI"] != null)
                            _Provider.NPI = dtProvider.Rows[0]["sNPI"].ToString();

                        if (dtProvider.Rows[0]["sUPIN"] != null)
                            _Provider.UPIN = dtProvider.Rows[0]["sUPIN"].ToString();

                        if (dtProvider.Rows[0]["sSSN"] != null)
                            _Provider.SSN = dtProvider.Rows[0]["sSSN"].ToString();

                        if (dtProvider.Rows[0]["sEmployerID"] != null)
                            _Provider.EmployerID = dtProvider.Rows[0]["sEmployerID"].ToString();

                        if (dtProvider.Rows[0]["sMedicalLicenseNo"] != null)
                            _Provider.StateMedicalNo = dtProvider.Rows[0]["sMedicalLicenseNo"].ToString();

                        if (dtProvider.Rows[0]["sTaxonomy"] != null)
                            _Provider.Taxonomy = dtProvider.Rows[0]["sTaxonomy"].ToString();


                        if (dtProvider.Rows[0]["sPrefix"] != null)
                            _Provider.Prefix = dtProvider.Rows[0]["sPrefix"].ToString();

                        if (dtProvider.Rows[0]["sExternalCode"] != null)
                            _Provider.ExternalCode = dtProvider.Rows[0]["sExternalCode"].ToString();

                        if (dtProvider.Rows[0]["sSuffix"] != null)
                            _Provider.Suffix = dtProvider.Rows[0]["sSuffix"].ToString();

                        //Temp code added to make User_MST Entry
                        dtProvider = new DataTable();
                        _strSQL = "";
                        //_strSQL = "select * from user_MST where nProviderID =" + ResourceID; //Removed select *
                        _strSQL = "select sLoginName,sPassword  from user_MST  WITH(NOLOCK) where nProviderID =" + ResourceID;
                        oDB.Retrive_Query(_strSQL, out dtProvider);
                        if (dtProvider.Rows.Count > 0)
                            if (dtProvider.Rows[0]["sLoginName"] != null)
                            {
                                _Provider.UserName = dtProvider.Rows[0]["sLoginName"].ToString();

                                gloSecurity.ClsEncryption oEncryption = new gloSecurity.ClsEncryption();
                                _Provider.Password = oEncryption.DecryptFromBase64String(dtProvider.Rows[0]["sPassword"].ToString(), _encryptionKey);
                                if (oEncryption != null) { oEncryption.Dispose(); oEncryption = null; }
                            }

                        //Temp code added to make User_MST Entry
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {

                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                    _strSQL = null;
                }
                return _Provider;


            }//GetProviderDetails()

            public Provider GetProvider(Int64 ProviderID)
            {
                Provider oProvider = new Provider();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                DataTable dtProvider = null;

                try
                {

                    _strSQL = " SELECT nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, sBusinessState, sBusinessZIP, sPracticeAddressline1,sPracticeAddressline2, sPracticeCity, sPracticeState, sPracticeZIP,sBusPhoneNo, sBusFAX, sBusPagerNo, sBusEmail, sBusURL,sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL,sMobileNo, "
                            + " nProviderType, sNPI, sSSN, sEmployerID, imgSignature ,sUPIN, sMedicalLicenseNo, sPrefix, nClinicID, sSPIID, sExternalCode,sTaxonomy,sTaxonomyDesc, "
                            + " sCompanyName,sCompanyAddressline1,sCompanyAddressline2,sCompanyCity,sCompanyState,sCompanyZIP,sCompanyNPI,sCompanyTaxID,sCompanyPhone,sCompanyContactName,sCompanyFax,sCompanyEmail, "
                            + " ISNULL(sBusinessAreaCode,'') AS sBusinessAreaCode,ISNULL(sPracticeAreaCode,'') AS sPracticeAreaCode,ISNULL(sCompanyAreaCode,'') AS sCompanyAreaCode "
                            + " FROM Provider_MST  WITH(NOLOCK)  WHERE  nProviderID = " + ProviderID + " AND nClinicID = " + _ClinicID + " Order By sFirstName ";
                    oDB.Connect(false); 
                    //get all the Provider types

                    oDB.Retrive_Query(_strSQL, out dtProvider);

                    if (dtProvider != null)
                    {
                        if (dtProvider.Rows.Count > 0)
                        {
                            oProvider.ProviderID = Convert.ToInt64(dtProvider.Rows[0]["nProviderID"]);
                            oProvider.Prefix = Convert.ToString(dtProvider.Rows[0]["sPrefix"]);
                            oProvider.FirstName = Convert.ToString(dtProvider.Rows[0]["sFirstName"]);
                            oProvider.MiddleName = Convert.ToString(dtProvider.Rows[0]["sMiddleName"]);
                            oProvider.LastName = Convert.ToString(dtProvider.Rows[0]["sLastName"]);
                            oProvider.Gender = Convert.ToString(dtProvider.Rows[0]["sGender"]);
                            oProvider.DEA = Convert.ToString(dtProvider.Rows[0]["sDEA"]);
                            oProvider.BMAddress1 = Convert.ToString(dtProvider.Rows[0]["sBusinessAddressline1"]);
                            oProvider.BMAddress2 = Convert.ToString(dtProvider.Rows[0]["sBusinessAddressline2"]);
                            oProvider.BMCity = Convert.ToString(dtProvider.Rows[0]["sBusinessCity"]);
                            oProvider.BMState = Convert.ToString(dtProvider.Rows[0]["sBusinessState"]);
                            oProvider.BMZIP = Convert.ToString(dtProvider.Rows[0]["sBusinessZIP"]);
                            oProvider.BMAreaCode = Convert.ToString(dtProvider.Rows[0]["sBusinessAreaCode"]);

                            oProvider.BPracAddress1 = Convert.ToString(dtProvider.Rows[0]["sPracticeAddressline1"]);
                            oProvider.BPracAddress2 = Convert.ToString(dtProvider.Rows[0]["sPracticeAddressline2"]);
                            oProvider.BPracCity = Convert.ToString(dtProvider.Rows[0]["sPracticeCity"]);
                            oProvider.BPracState = Convert.ToString(dtProvider.Rows[0]["sPracticeState"]);
                            oProvider.BPracZIP = Convert.ToString(dtProvider.Rows[0]["sPracticeZIP"]);
                            oProvider.BPracAreaCode = Convert.ToString(dtProvider.Rows[0]["sPracticeAreaCode"]);

                            //sBusPhoneNo, sBusFAX, sBusPagerNo, sBusEmail, sBusURL,sMobileNo,
                            //oProvider.Phone = Convert.ToString(dtProvider.Rows[0]["sPhoneNo"]);
                            //oProvider.FAX = Convert.ToString(dtProvider.Rows[0]["sFAX"]);
                            //oProvider.Mobile = Convert.ToString(dtProvider.Rows[0]["sMobileNo"]);
                            //oProvider.Pager = Convert.ToString(dtProvider.Rows[0]["sPagerNo"]);
                            //oProvider.Email = Convert.ToString(dtProvider.Rows[0]["sEmail"]);
                            //oProvider.URL = Convert.ToString(dtProvider.Rows[0]["sURL"]);
                            //oProvider.Taxonomy = Convert.ToString(dtProvider.Rows[0]["sTaxonomy"]);

                            oProvider.BMPhone = Convert.ToString(dtProvider.Rows[0]["sBusPhoneNo"]);
                            oProvider.BMFAX = Convert.ToString(dtProvider.Rows[0]["sBusFAX"]);
                            oProvider.Mobile = Convert.ToString(dtProvider.Rows[0]["sMobileNo"]);
                            oProvider.BMPager = Convert.ToString(dtProvider.Rows[0]["sBusPagerNo"]);
                            oProvider.BMEmail = Convert.ToString(dtProvider.Rows[0]["sBusEmail"]);
                            oProvider.BMURL = Convert.ToString(dtProvider.Rows[0]["sBusURL"]);

                            //sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL
                            oProvider.BPracPhone = Convert.ToString(dtProvider.Rows[0]["sPracPhoneNo"]);
                            oProvider.BPracFAX = Convert.ToString(dtProvider.Rows[0]["sPracFAX"]);
                            oProvider.BPracPager = Convert.ToString(dtProvider.Rows[0]["sPracPagerNo"]);
                            oProvider.BPracEmail = Convert.ToString(dtProvider.Rows[0]["sPracEmail"]);
                            oProvider.BPracURL = Convert.ToString(dtProvider.Rows[0]["sPracURL"]);

                            oProvider.CompanyName = Convert.ToString(dtProvider.Rows[0]["sCompanyName"]);
                            oProvider.CompanyContactName = Convert.ToString(dtProvider.Rows[0]["sCompanyContactName"]);
                            oProvider.CompanyAddress1 = Convert.ToString(dtProvider.Rows[0]["sCompanyAddressline1"]);
                            oProvider.CompanyAddress2 = Convert.ToString(dtProvider.Rows[0]["sCompanyAddressline2"]);
                            oProvider.CompanyCity = Convert.ToString(dtProvider.Rows[0]["sCompanyCity"]);
                            oProvider.CompanyState = Convert.ToString(dtProvider.Rows[0]["sCompanyState"]);
                            oProvider.CompanyZip = Convert.ToString(dtProvider.Rows[0]["sCompanyZIP"]);

                            oProvider.CompanyAreaCode = Convert.ToString(dtProvider.Rows[0]["sCompanyAreaCode"]);

                            oProvider.CompanyPhone = Convert.ToString(dtProvider.Rows[0]["sCompanyPhone"]);
                            oProvider.CompanyFax = Convert.ToString(dtProvider.Rows[0]["sCompanyFax"]);
                            oProvider.CompanyEmail = Convert.ToString(dtProvider.Rows[0]["sCompanyEmail"]);
                            //Added by Anil on 20090310
                            oProvider.CompanyNPI = Convert.ToString(dtProvider.Rows[0]["sCompanyTaxID"]);
                            oProvider.CompanyTaxID = Convert.ToString(dtProvider.Rows[0]["sCompanyNPI"]);

                            oProvider.Taxonomy = Convert.ToString(dtProvider.Rows[0]["sTaxonomy"]);
                            oProvider.TaxonomyDesc = Convert.ToString(dtProvider.Rows[0]["sTaxonomyDesc"]);
                            if (System.DBNull.Value != dtProvider.Rows[0]["imgSignature"])
                            {
                                Byte[] arrImage = (Byte[])((dtProvider.Rows[0]["imgSignature"]));
                                MemoryStream ms = new MemoryStream(arrImage);
                                oProvider.Signature = Image.FromStream(ms);
                                try
                                {
                                    ms.Close();
                                    ms.Dispose();
                                    ms = null;
                                }
                                catch
                                {

                                }
                            }

                            if (dtProvider.Rows[0]["nProviderType"] != null)
                            {
                                oProvider.ProviderTypeID = Convert.ToInt64(dtProvider.Rows[0]["nProviderType"]);
                            }

                            oProvider.NPI = Convert.ToString(dtProvider.Rows[0]["sNPI"]);
                            oProvider.UPIN = Convert.ToString(dtProvider.Rows[0]["sUPIN"]);
                            oProvider.SSN = Convert.ToString(dtProvider.Rows[0]["sSSN"]);
                            oProvider.EmployerID = Convert.ToString(dtProvider.Rows[0]["sEmployerID"]);
                            oProvider.StateMedicalNo = Convert.ToString(dtProvider.Rows[0]["sMedicalLicenseNo"]);
                            oProvider.ExternalCode = Convert.ToString(dtProvider.Rows[0]["sExternalCode"]);
                            oProvider.ClinicID = Convert.ToInt64(dtProvider.Rows[0]["nClinicID"]);
                            oProvider.UserID = 0;
                            oProvider.UserName = "";
                            oProvider.Password = "";
                        }
                        else
                        {
                            oProvider = null;
                        }
                    }
                    else
                    {
                        oProvider = null;
                    }

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    _strSQL = null;
                    if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                }
                return oProvider;
            }

            public string GetProviderName(Int64 providerID)
            {

                DataTable dtProvider = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                string _result = "";
                try
                {
                    oDB.Connect(false);

                    //get the provider details in the datatable -- dtProvider
                     //isnull(Provider_MST.sFirstName,'''')+space(2)+isnull(Provider_MST.sMiddleName,'''')+space(2)+isnull(Provider_MST.sLastName,'''') AS Provider
                    //_strSQL = "select * from Provider_MST where nProviderID =" + providerID;
                    
                    //_strSQL = "select isnull(Provider_MST.sFirstName,'')+space(1)+isnull(Provider_MST.sMiddleName,'')+space(1)+isnull(Provider_MST.sLastName,'') AS ProviderName from Provider_MST where nProviderID =" + providerID;
                    //CHECK WHETHER MIDDLE NAME IS BLANK  FOR THE CASE  GLO2010-0004942 ON 20100612
                    _strSQL = "select isnull(Provider_MST.sFirstName,'')+space(1)+CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then " +
                                "Provider_MST.sMiddleName  + SPACE(1) END +isnull(Provider_MST.sLastName,'') AS ProviderName from Provider_MST  WITH(NOLOCK) where nProviderID =" + providerID + "";

                    oDB.Retrive_Query(_strSQL, out dtProvider);

                    //if (dtProvider != null && dtProvider.Rows.Count > 0)
                    //{
                    //    string firstName = "", midName = "", lastName = "";
                    //    //  oProvider._nProviderID = dtProvider.Rows[0]["nProviderID"];
                    //    if (dtProvider.Rows[0]["sFirstName"] != null)
                    //        firstName = dtProvider.Rows[0]["sFirstName"].ToString();

                    //    if (dtProvider.Rows[0]["sMiddleName"] != null)
                    //        midName = dtProvider.Rows[0]["sMiddleName"].ToString();

                    //    if (dtProvider.Rows[0]["sLastName"] != null)
                    //        lastName = dtProvider.Rows[0]["sLastName"].ToString();

                    //    _result = firstName + " " + midName + " " + lastName;

                    //}

                    if (dtProvider != null && dtProvider.Rows.Count > 0)
                    {
                        _result = dtProvider.Rows[0]["ProviderName"].ToString();
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {

                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                    _strSQL = null;
                }

                return _result;

            }

            public string GetUserName(Int64 UserID)
            {

                DataTable dtUser = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                string _result = "";
                try
                {
                    oDB.Connect(false);
                    //isnull(AB_Resource_Provider.sFirstName,'''')+space(2)+isnull(AB_Resource_Provider.sMiddleName,'''')+space(2)+isnull(AB_Resource_Provider.sLastName,'''')
                   // _strSQL = "select sFirstName,sMiddleName,sLastName from User_MST where nUserID =" + UserID;
                    _strSQL = "select isnull(sFirstName,'''')+space(1)+isnull(sMiddleName,'''')+space(1)+isnull(sLastName,'''') as UserName from User_MST  WITH(NOLOCK) where nUserID =" + UserID;

                    oDB.Retrive_Query(_strSQL, out dtUser);

                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        _result = dtUser.Rows[0]["UserName"].ToString();
                    }
                    
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {

                    oDB.Disconnect();
                    if (oDB != null)
                        oDB.Dispose();
                    if (dtUser != null) { dtUser.Dispose(); dtUser = null; }
                    _strSQL = null;
                }

                return _result;

            }

            public string GetLoginName(Int64 UserId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                Object retVal = null;
                string _sqlQuery = "";
                string _loginName = "";

                try
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT ISNULL(sLoginName,'') AS sLoginName FROM User_MST  WITH(NOLOCK) " +
                    " WHERE nUserID = "+UserId+" AND nClinicID = "+this.ClinicID+" ";
                    retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (Convert.ToString(retVal).Trim() != "")
                    { _loginName = Convert.ToString(retVal); }
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (retVal != null) { retVal = null; }
                    _sqlQuery = null;
                }

                return _loginName;
            }


            public bool isGloCollectUser(Int64 UserId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                Object retVal = null;
                string _sqlQuery = "";
                bool _bIsGloCollect = false;

                try
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT ISNULL(bIsGloCollect,0) AS bIsGloCollect FROM User_MST  WITH(NOLOCK) " +
                    " WHERE nUserID = " + UserId + " AND nClinicID = " + this.ClinicID + " ";
                    retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (Convert.ToString(retVal).Trim() != "")
                    { _bIsGloCollect = Convert.ToBoolean(retVal); }
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (retVal != null) { retVal = null; }
                    _sqlQuery = null;
                }

                return _bIsGloCollect;
            }
            public DataTable GetProviderTypes()
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                DataTable dtProviderType = null;

                try
                {
                    //_strSQL = "select nProviderTypeID,sProviderType from AB_Resource_ProviderType where bIsBlocked=0";
                    _strSQL = "select nProviderTypeID,sProviderType from AB_Resource_ProviderType  WITH(NOLOCK) where bIsBlocked=0 AND nClinicID = " + this.ClinicID + " ";
                    //

                    oDB.Connect(false);
                    //get all the Provider types

                    oDB.Retrive_Query(_strSQL, out dtProviderType);

                    oDB.Disconnect();
                    if (dtProviderType != null && dtProviderType.Rows.Count > 0)
                    {
                        return dtProviderType;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                        oDB.Dispose();
                    _strSQL = null;
                }
            }//FillProviderType()

            public DataTable GetEPCSProvider()
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtProviderType = null;
                try
                {
                    oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("rptScanProvidersEpcs", oDBParameters, out  dtProviderType);


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
                    oDBParameters.Dispose();
                    oDBParameters = null;
                    oDB = null;
                }
                return dtProviderType;
                
            }
            public DataTable GetProviders()
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                DataTable dtProviderType = null;

                try
                {
                    //*******************************************
                    //Changes made on 20081014 - By Sagar Ghodke
                    //Below commented code is the previous Code

                    ////_strSQL = "SELECT nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, imgSignature, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode , (ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') + ' ' +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  ";
                    //if(this._ClinicID == 0)
                    //    _strSQL = "SELECT * , (ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') + ' ' +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST ";
                    //else 
                    //    _strSQL = "SELECT * , (ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') + ' ' +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE nClinicID = " + this.ClinicID + " ";
                    ////

                    if (this._ClinicID == 0)
                        _strSQL = "SELECT nProviderID, ISNULL(sFirstName,'') as FirstName, ISNULL(sMiddleName,'') as MiddleName, ISNULL(sLastName,'') as LastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode, sServiceLevel, dtActiveStartTime, dtActiveEndTime, sSPIID, nClinicID, bIsblocked, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, sBusinessState, sBusinessZIP, sPracticeAddressline1, sPracticeAddressline2, sPracticeCity, sPracticeState, sPracticeZIP, sBusPhoneNo, sBusFAX, sBusPagerNo, sBusEmail, sBusURL, sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL, sTaxonomy, sTaxonomyDesc, sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity, sCompanyState, sCompanyZIP, sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName, sBusinessContactName, sPracContactName, sSSN, sEmployerID, sSuffix, sCompanyNPI, sCompanyTaxID , (ISNULL(sFirstName,'')+ SPACE(1) + CASE sMiddleName WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST   WITH(NOLOCK) WHERE  ISNULL(bIsblocked,'false')='FALSE' ORDER BY ProviderName";
                    else
                        _strSQL = "SELECT nProviderID, ISNULL(sFirstName,'') as FirstName, ISNULL(sMiddleName,'') as MiddleName, ISNULL(sLastName,'') as LastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode, sServiceLevel, dtActiveStartTime, dtActiveEndTime, sSPIID, nClinicID, bIsblocked, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, sBusinessState, sBusinessZIP, sPracticeAddressline1, sPracticeAddressline2, sPracticeCity, sPracticeState, sPracticeZIP, sBusPhoneNo, sBusFAX, sBusPagerNo, sBusEmail, sBusURL, sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL, sTaxonomy, sTaxonomyDesc, sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity, sCompanyState, sCompanyZIP, sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName, sBusinessContactName, sPracContactName, sSSN, sEmployerID, sSuffix, sCompanyNPI, sCompanyTaxID , (ISNULL(sFirstName,'')+ SPACE(1) + CASE sMiddleName WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  WITH(NOLOCK) WHERE nClinicID = " + this.ClinicID + " AND ISNULL(bIsblocked,'false')='FALSE' ORDER BY ProviderName";

                    //End Changes made on 20081014 - By Sagar Ghodke
                    //*********************************************

                    oDB.Connect(false);
                    //get all the Provider types

                    oDB.Retrive_Query(_strSQL, out dtProviderType);

                    oDB.Disconnect();
                    if (dtProviderType != null && dtProviderType.Rows.Count > 0)
                    {
                        return dtProviderType;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                        oDB.Dispose();
                    _strSQL = null;
                }
            }//FillProviderType()


            public DataTable GetAllActiveInactiveProviders(Boolean IsActiveProvider = false)
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtProviders = null;
                try
                {
                    oDBParameters.Add("@IsActiveProvider", IsActiveProvider, ParameterDirection.Input, SqlDbType.BigInt);
                    
                    oDB.Connect(false);
                    oDB.Retrive("GetAllActiveInactiveProviders", oDBParameters, out  dtProviders);
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
                    oDBParameters.Dispose();
                    oDBParameters = null;
                    oDB = null;
                }
                return dtProviders;

            }



            //Added By Pramod Nair For Getting the ProviderId and Provider Name
            public DataTable GetBillingProviders()
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String _strSQL = "";
                DataTable dtProviderType = null;

                try
                {
                    
                    if (this._ClinicID == 0)
                        _strSQL = "SELECT isnull(nProviderID,0) as nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  WITH(NOLOCK) WHERE  bIsblocked='FALSE' ORDER BY ProviderName";
                    else
                        _strSQL = "SELECT isnull(nProviderID,0) as nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST   WITH(NOLOCK) WHERE nClinicID = " + this.ClinicID + " AND bIsblocked='FALSE' ORDER BY ProviderName";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dtProviderType);
                    oDB.Disconnect();
                    if (dtProviderType != null && dtProviderType.Rows.Count > 0)
                    {
                        return dtProviderType;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                        oDB.Dispose();
                    _strSQL = null;
                }
            }//FillProviderType()

            public DataTable GetResources()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtResources = null;
                string _strSQL = "";

                oDB.Connect(false);

                try
                {

                    _strSQL = " SELECT AB_Resource_MST.nResourceID, AB_Resource_MST.sDescription, AB_Resource_MST.nResourceTypeID, AB_Resource_MST.sCode " +
                           " FROM AB_Resource_MST  WITH(NOLOCK) INNER JOIN AB_ResourceType_MST  WITH(NOLOCK) ON AB_Resource_MST.nResourceTypeID = AB_ResourceType_MST.nResourceTypeID " +
                           " WHERE ((AB_Resource_MST.bitIsBlocked = 0) AND AB_ResourceType_MST.nResourceType <>  " + ResourceType.Provider.GetHashCode() + ") " +
                           " AND AB_Resource_MST.nClinicID = " + this.ClinicID + " " +
                           " ORDER BY sDescription ";

                    //_strSQL = "select AB_Resource_MST.nResourceID, AB_Resource_MST.sDescription , AB_Resource_MST.nResourceTypeID from dbo.AB_Resource_MST inner join dbo.AB_ResourceType_MST on AB_Resource_MST.nResourceTypeID = AB_ResourceType_MST.nResourceTypeID where AB_Resource_MST.bitIsBlocked = 0 and AB_ResourceType_MST.nResourceType <> " + ResourceType.Provider.GetHashCode();

                    oDB.Retrive_Query(_strSQL, out dtResources);


                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    _strSQL = null;
                }
                return dtResources;
            }//GetResources()

            public DataTable GetBlockedResources()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtResources = null;
                //string _strSQL = "";

                oDB.Connect(false);

                try
                {

                    String strQuery = "SELECT DISTINCT  AB_Resource_MST.nResourceID AS nResourceID, " +
                                  "isnull(Provider_MST.sFirstName,'') + ' ' + " +
                                  "isnull(Provider_MST.sMiddleName,'') + ' ' + " +
                                  "isnull(Provider_MST.sLastName,'') AS sDescription, " +
                                  "AB_ResourceType_MST.sResourceTypeDescription  AS sResourceTypeDescription, " +
                                  "AB_ResourceType_MST.nResourceType AS nResourceType, " +
                                  "AB_ResourceType_MST.nResourceTypeID AS nResourceTypeID " +
                                  "FROM   AB_ResourceType_MST  WITH(NOLOCK) INNER JOIN AB_Resource_MST  WITH(NOLOCK) ON " +
                                  "AB_ResourceType_MST.nResourceTypeID = AB_Resource_MST.nResourceTypeID " +
                                  "INNER JOIN  Provider_MST  WITH(NOLOCK) ON AB_Resource_MST.nResourceID = Provider_MST.nProviderID " +
                                  "WHERE (AB_Resource_MST.bitIsBlocked=1  AND sDescription ='')AND AB_Resource_MST.nClinicID = " + this.ClinicID + " " +
                                  "UNION SELECT DISTINCT AB_Resource_MST.nResourceID AS nResourceID,AB_Resource_MST.sDescription AS sDescription, AB_ResourceType_MST.sResourceTypeDescription AS sResourceTypeDescription, AB_ResourceType_MST.nResourceType AS nResourceType,AB_ResourceType_MST.nResourceTypeID AS nResourceTypeID FROM         AB_ResourceType_MST  WITH(NOLOCK) INNER JOIN AB_Resource_MST  WITH(NOLOCK) ON AB_ResourceType_MST.nResourceTypeID = AB_Resource_MST.nResourceTypeID WHERE AB_Resource_MST.bitIsBlocked = 1 AND sDescription <>'' AND AB_Resource_MST.nClinicID = " + this.ClinicID + "  order by AB_ResourceType_MST.nResourceTypeID, sDescription";
                    //

                    //_strSQL = "select AB_Resource_MST.nResourceID, AB_Resource_MST.sDescription , AB_Resource_MST.nResourceTypeID from dbo.AB_Resource_MST inner join dbo.AB_ResourceType_MST on AB_Resource_MST.nResourceTypeID = AB_ResourceType_MST.nResourceTypeID where AB_Resource_MST.bitIsBlocked = 0 and AB_ResourceType_MST.nResourceType <> " + ResourceType.Provider.GetHashCode();

                    oDB.Retrive_Query(strQuery, out dtResources);

                    strQuery = null;
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                }
                return dtResources;
            }//GetResources()

            public bool CheckProviderExistsAsUser(string loginName, Int64 ProviderId)
            {
                int count;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                String strQuery = "";
                try
                {
                    oDB.Connect(false);
                    if (ProviderId != 0)
                    {
                        //strQuery = "SELECT Count(*) FROM User_MST WHERE sLoginName = '" + loginName + "' and nProviderId <> " + ProviderId;
                        strQuery = "SELECT Count(*) FROM User_MST WHERE (sLoginName = '" + loginName + "' and nProviderId <> " + ProviderId + ") AND nClinicID = " + this.ClinicID + " ";
                        //

                    }
                    else
                    {
                        //strQuery = "SELECT Count(*) FROM User_MST WHERE sLoginName = '" + loginName + "'";
                        strQuery = "SELECT Count(*) FROM User_MST WHERE sLoginName = '" + loginName + "' AND nClinicID = " + this.ClinicID + " ";
                        //
                    }
                    count = (Int32)oDB.ExecuteScalar_Query(strQuery);
                    if (count > 0)
                    {
                        //Login name exists
                        return true;
                    }
                    else
                    {
                        //Login name does not exists
                        return false;
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
                return true;
            }

            public bool CanDelete(Int64 ResourceID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                string SqlQuery = "";
                try
                {
                    oDB.Connect(false);

                    //Check in AS_Appointment_DTL_Resources
                    //SqlQuery = "SELECT  nAppointmentID  FROM AS_Appointment_DTL_Resources WHERE nResourceID = " + ResourceID;
                    SqlQuery = "SELECT  scode  FROM AB_Resource_MST  WITH(NOLOCK) WHERE nResourceID =" + ResourceID + "";
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }

                    //Check in AS_Appointment_DTL_Providers
                    SqlQuery = "SELECT nAppointmentID FROM AS_Appointment_DTL_Providers  WITH(NOLOCK)  WHERE nProviderID = " + ResourceID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }



                    //Check in AS_Schedule_DTL_Resources
                    SqlQuery = "SELECT  nScheduleID  FROM AS_Schedule_DTL_Resources  WITH(NOLOCK) WHERE nResourceID = " + ResourceID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }

                    //Check in AS_Schedule_DTL_Procedures
                    SqlQuery = "SELECT nScheduleID FROM  AS_Schedule_DTL_Providers  WITH(NOLOCK) WHERE nProviderID =" + ResourceID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }

                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                    SqlQuery = null;
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
                return Result;
            }

            public DataTable GetActiveProviders()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " SELECT DISTINCT nASBaseID AS nProviderID, sASBaseDesc AS sProviderName FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) "
                             + " WHERE   dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()) + " AND nClinicID = " + _ClinicID + " "
                             + " ORDER BY sProviderName";
                    oDB.Retrive_Query(strQuery, out dt);
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    strQuery = null;
                    oDB.Dispose();

                }
                return dt;
            }

            #endregion Provider Methods

            #region " TOS & POS Methods "

            public DataTable GetTOS(Int64 TOSId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtTOS = null;
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    if (TOSId > 0)
                    {
                        strQuery = "select nTOSID,sDescription from dbo.BL_TOS_MST  WITH(NOLOCK) where nTOSID= " + TOSId;
                    }
                    else
                    {
                        strQuery = "select nTOSID,sDescription from dbo.BL_TOS_MST  WITH(NOLOCK) ";
                    }
                    oDB.Retrive_Query(strQuery, out dtTOS);
                    return dtTOS;


                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dtTOS.Dispose();
                    strQuery = null;
                }
            }

            public DataTable GetPOS(Int64 POSId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtPOS = null;
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    if (POSId > 0)
                    {
                        strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from dbo.BL_POS_MST  WITH(NOLOCK) where nPOSID= " + POSId;
                    }
                    else
                    {
                        strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from dbo.BL_POS_MST  WITH(NOLOCK) ";
                    }
                    oDB.Retrive_Query(strQuery, out dtPOS);
                    return dtPOS;

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dtPOS.Dispose();
                    strQuery = null;
                }
            }

            public Int64 AddModifyPOS(Int64 POSId, string sCode, string sName, string sDesc)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                Object _oResult = new object();
                try
                {
                    //Pass 0 to Add
                    oDB.Connect(false);

                    oParameters.Add("@nPOSID", POSId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@sPOSCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sPOSName", sName, ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@sPOSDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                    oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("AB_INUP_POS", oParameters, out _oResult);

                    return Convert.ToInt64(_oResult);

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return 0;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oParameters.Dispose();
                    _oResult = null;
                }
            }

            public Int64 AddModifyTOS(Int64 TOSId, string sDescription)
            {

                //Pass 0 to Add
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                Object _oResult = new object();

                try
                {
                    oDB.Connect(false);

                    oParameters.Add("@nTOSID", TOSId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@sDescription", sDescription, ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("AB_INUP_TOS", oParameters, out _oResult);

                    return Convert.ToInt64(_oResult);
                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return 0;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oParameters.Dispose();
                    _oResult = null;
                }

            }

            public bool IsExistsPOS(Int64 POSId, string Code, string Name)
            {
                bool _result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";

                try
                {
                    oDB.Connect(false);
                    if (POSId == 0)
                    {
                        strQuery = " select count(nPOSID) from BL_POS_MST  WITH(NOLOCK)  where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                    }
                    else
                    {
                        strQuery = " select count(nPOSID) from BL_POS_MST   WITH(NOLOCK) where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') and nPOSID <> " + POSId + " ";
                    }

                    object _intResult = null;
                    //_intResult = oDB.Execute_Query(strQuery);
                    _intResult = oDB.ExecuteScalar_Query(strQuery);

                    if (_intResult != null)
                    {
                        if (_intResult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intResult) > 0)
                            {
                                _result = true;
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }

                return _result;
            }

            public bool IsExistsTOS(Int64 TOSId, string Description)
            {
                bool _result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                object _intResult = null;

                try
                {
                    oDB.Connect(false);
                    if (TOSId == 0)
                    {
                        strQuery = " select count(nTOSID) from BL_TOS_MST  WITH(NOLOCK) where sDescription = '" + Description + "'";
                    }
                    else
                    {
                        strQuery = " select count(nTOSID) from BL_TOS_MST  WITH(NOLOCK) where (sDescription='" + Description + "' and nTOSID <> " + TOSId + ")";
                    }

                    //_intResult = oDB.Execute_Query(strQuery);
                    _intResult = oDB.ExecuteScalar_Query(strQuery);

                    if (_intResult != null)
                    {
                        if (_intResult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intResult) > 0)
                            {
                                _result = true;
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    _intResult = null;
                }

                return _result;
            }

            #endregion " TOS & POS Methods "

           
        }


    }
}
