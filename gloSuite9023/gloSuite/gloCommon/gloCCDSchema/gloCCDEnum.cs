using System;
using System.Data; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloCCDSchema
{

    public static class CodeSystem
    {
        private const string _sParticipantFunction = "2.16.840.1.113883.5.88";
        private const string _sICD9codeSystem = "2.16.840.1.113883.6.103";
        private const string _sICD10codeSystem = "2.16.840.1.113883.6.90";
        private const string _sSNOMEDCTcodeSystem = "2.16.840.1.113883.6.96";
        private const string _sRxNORMcodeSystem = "2.16.840.1.113883.6.88";
        private const string _sLOINCcodeSystem = "2.16.840.1.113883.6.1";
        private const string _sHL7RouteOfAdministration = "2.16.840.1.113883.5.112";
        private const string _sFDARouteOfAdministration = "2.16.840.1.113883.3.26.1.1";
        private const string _sAdministrativeGender = "2.16.840.1.113883.5.1";
        private const string _sMaritalStatus = "2.16.840.1.113883.5.2";
        private const string _sRaceAndEthnicity = "2.16.840.1.113883.6.238";
        private const string _sLanguageAbilityMode = "2.16.840.1.113883.5.60";
        private const string _sCVXCodeSystem = "2.16.840.1.113883.12.292";
        private const string _sPaymentTopology = "2.16.840.1.113883.3.221.5";
        private const string _sNDCcodeSystem ="2.16.840.1.113883.6.69";
        
        private const string _sCPTCodeSystem = "2.16.840.1.113883.6.12";
        private const string _sHCPTCodeSystem = "2.16.840.1.113883.6.14";
        private const string _sHCPCSCodeSystem = "2.16.840.1.113883.6.285";
        
        private const string _sGPICodeSystem = "2.16.840.1.113883.6.68";
        private const string _sCoverageRoleTypeSystem = "2.16.840.1.113883.5.111";

        // Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
        private const string _ssectiontemplateIdroot = "2.16.840.1.113883.3.3251.1.5";
        private const string _sAuthortemplateIdroot = "2.16.840.1.113883.3.3251.1.2";
        private const string _sassignedAuthortemplateIdroot = "2.16.840.1.113883.3.3251.1.3";
        private const string _sassignedAuthoridroot = "2.16.840.1.113883.4.6";
        private const string _srepresentedOrganizationidroot = "2.16.840.1.113883.19.5";
        private const string _sentrytemplateIdroot = "2.16.840.1.113883.3.3251.1.9";
        private const string _sorganizertemplateIdroot = "2.16.840.1.113883.3.3251.1.4";
        private const string _sobservationtemplateIdroot1 = "2.16.840.1.113883.3.445.21";
        private const string _sobservationtemplateIdroot2 = "2.16.840.1.113883.3.445.12";
        private const string _sobservationtemplateIdroot3 = "2.16.840.1.113883.3.445.23";
        private const string _sobservationtemplateIdroot4 = "2.16.840.1.113883.3.445.22";
        private const string _sobservationtemplateIdroot5 = "2.16.840.1.113883.3.445.14";
        private const string _sobservationvaluecodesystem = "2.16.840.1.113883.5.1063";
        private const string _sobservationvaluecodesystem1 = "2.16.840.1.113883.1.11.20471";
        private const string _sobservationvaluecodesystemname = "SecurityObservationValueCodeSystem";
        private const string _sconfidentialityCodesystem = "2.16.840.1.113883.5.25";
        private const string _sconfidentialityCodesystemname = "HL7 Confidentiality";
        private const string _sHL7ActNoImmunizationReason = "2.16.840.1.113883.5.8";
        private const string _Assertion = "2.16.840.1.113883.5.4";
        private const string _taxanomyCodeSystem = "2.16.840.1.113883.6.101";
        ////private const string _sSECCLASSOBScodecodeSystem = "2.16.840.1.113883.1.11.20457";
        ////private const string _sSECCLASSOBSvaluecodeSystem = "2.16.840.1.113883.5.1063";


        public static String TaxanomyCodeSystem 
        {
          get
          {
             return _taxanomyCodeSystem;
          }
        
        }
        public static String Assertion
        {
            get
            {
                return _Assertion;
            }
        }
        public static string NoImmunizationReason
        {
            get { return _sHL7ActNoImmunizationReason; }
        }
        public static string sectiontemplateIdroot
        {
            get { return _ssectiontemplateIdroot; }
        }
        public static string AuthortemplateId1root
        {
            get { return _sAuthortemplateIdroot; }
        }
        public static string sassignedAuthortemplateIdroot
        {
            get { return _sassignedAuthortemplateIdroot; }
        }
        public static string assignedAuthoridroot
        {
            get { return _sassignedAuthoridroot; }
        }
        public static string representedOrganizationidroot
        {
            get { return _srepresentedOrganizationidroot; }
        }
        public static string entrytemplateIdroot
        {
            get { return _sentrytemplateIdroot; }
        }
        public static string organizertemplateIdroot
        {
            get { return _sorganizertemplateIdroot; }
        }
        public static string sobservationtemplateIdroot11
        {
            get { return _sobservationtemplateIdroot1; }
        }
        public static string sobservationtemplateIdroot12
        {
            get { return _sobservationtemplateIdroot2; }
        }
        public static string sobservationtemplateIdroot22
        {
            get { return _sobservationtemplateIdroot3; }
        }
        public static string sobservationtemplateIdroot32
        {
            get { return _sobservationtemplateIdroot4; }
        }
        public static string sobservationtemplateIdroot15
        {
            get { return _sobservationtemplateIdroot5; }
        }
        public static string sobservationvaluecodesystem
        {
            get { return _sobservationvaluecodesystem; }
        }
        public static string sobservationvaluecodesystem1
        {
            get { return _sobservationvaluecodesystem1; }
        }
        public static string sobservationvaluecodesystemname
        {
            get { return _sobservationvaluecodesystemname ; }
        }
        public static string sconfidentialityCodesystem
        {
            get { return _sconfidentialityCodesystem; }
        }
        public static string sconfidentialityCodesystemname
        {
            get { return _sconfidentialityCodesystemname; }
        } 
        // Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End
        public static string CoverageRoleType
        {
            get { return _sCoverageRoleTypeSystem ; }
        }
        public static string NDC
        {
            get { return _sNDCcodeSystem; }
        }
        public static string HCPCS
        {
            get { return _sHCPCSCodeSystem ; }
        }
        public static string HCP
        {
            get { return _sHCPTCodeSystem; }
        }
        public static string CPT
        {
            get { return _sCPTCodeSystem; }
        }
        public static string ICD9
        {
            get { return _sICD9codeSystem; }
        }
        public static string ParticipantFunction
        {
            get { return _sParticipantFunction ; }
        }
        public static string ICD10
        {
            get { return _sICD10codeSystem; }
        }
        public static string SNOMED_CT
        {
            get { return _sSNOMEDCTcodeSystem; }
        }
        public static string RxNorm
        {
            get { return _sRxNORMcodeSystem; }
        }
        public static string GPI
        {
            get { return _sGPICodeSystem; }
        }
        public static string LOINC
        {
            get { return _sLOINCcodeSystem; }
        }
        public static string HL7RouteCode
        {
            get { return _sHL7RouteOfAdministration; }
        }
        public static string FDARouteCode
        {
            get { return _sFDARouteOfAdministration; }
        }
        public static string AdministrativeGender
        {
            get { return _sAdministrativeGender; }
        }
        public static string MaritalStatus
        {
            get { return _sMaritalStatus; }
        }
        public static string RaceAndEthnicity
        {
            get { return _sRaceAndEthnicity; }
        }
        public static string LanguageAbilityMode
        {
            get { return _sLanguageAbilityMode; }
        }
        public static string CVXCode
        {
            get { return _sCVXCodeSystem ; }
        }
        public static string PaymentTopology
        {
            get { return _sPaymentTopology ; }
        }
    }

    public class CodeSystemItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string CodingSystem { get; set; }
        public string CodingSystemName { get; set; }
        public string Unit { get; set; }

    }
    public class SmokingStatusItem
    {
        public string SmokingStatusCode { get; set; }
        public string SmokingStatusDesc { get; set; }
    }

    public class CodeSystemMaster : IDisposable
    {
        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
           
            Dispose(true);
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dtCodes.Dispose(); 
                }
            }
            disposed = true;
        }
        #endregion

        DataTable _dtCodes = null;
        DataTable _dtMappingCodes = null;
       
        public CodeSystemMaster()
        {
            LoadCodeSystem();
            LoadMappedSmokingCodes();
        }

        private void LoadMappedSmokingCodes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT SMappingCode,sMappingDesc,sSmokingCode,sSmokingDesc FROM CCDA_SmokingStatusMapping";
               // _sqlQuery = "SELECT 'Race and Ethnicity - CDC' as sCodeSystemName,CodeSystemOID as sCodeSystem,ConceptCode as sCode,ConceptName as sDisplayName,ConceptCode as sEMRCode,ConceptName as sEMRDisplayName FROM RaceEthnicityMap where ConceptName='Hispanic Or Latino'";


                oDB.Retrive_Query(_sqlQuery, out _dtMappingCodes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
        }

        public SmokingStatusItem getSmokingStatusCode(string MappingCode)
        {
            SmokingStatusItem SmokingItem = null;
            try
            {
              
                if (_dtMappingCodes != null && _dtMappingCodes.Rows.Count > 0)
                {

                    var _CodeQuery = (from dt in _dtMappingCodes.AsEnumerable()
                                      where dt.Field<String>("SMappingCode") == MappingCode

                                      select new { sSmokingStatusCode = dt.Field<String>("sSmokingCode"),
                                                   sSmokingDesc = dt.Field<String>("sSmokingDesc"),
                                      }).Take(1);
                    foreach (var item in _CodeQuery)
                    {
                        SmokingItem = new SmokingStatusItem();
                        SmokingItem.SmokingStatusCode = item.sSmokingStatusCode;
                        SmokingItem.SmokingStatusDesc = item.sSmokingDesc;
                    }
                }
                return SmokingItem;
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        private void LoadCodeSystem()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sCodeSystemName,sCodeSystem,sCode,sDisplayName,sEMRCode,sEMRDisplayName,sUnit FROM CCD_CodingSystem";
               // _sqlQuery = "SELECT 'Race and Ethnicity - CDC' as sCodeSystemName,CodeSystemOID as sCodeSystem,ConceptCode as sCode,ConceptName as sDisplayName,ConceptCode as sEMRCode,ConceptName as sEMRDisplayName FROM RaceEthnicityMap where ConceptName='Hispanic Or Latino'";
                
                
                oDB.Retrive_Query(_sqlQuery,out _dtCodes );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
        }
      

        
        public CodeSystemItem GetbyCode(string sCodeSystem, string sEMRCode)
        {

            CodeSystemItem oResult = new CodeSystemItem();

            try
            {
                if (_dtCodes != null && _dtCodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtCodes.AsEnumerable()
                                      where dt.Field<String>("sCodeSystem") == sCodeSystem
                                      && dt.Field<String>("sCode") == sEMRCode
                                      select new
                                     {
                                         sCodeSystem = dt.Field<String>("sCodeSystem"),
                                         sCodeSystemName = dt.Field<String>("sCodeSystemName"),
                                         sCode = dt.Field<String>("sCode"),
                                         sDisplayName = dt.Field<String>("sDisplayName")
                                     }).Take(1);

                    
                    foreach (var item in _CodeQuery)
                    {
                        oResult.CodingSystem = item.sCodeSystem;
                        oResult.CodingSystemName = item.sCodeSystemName;
                        oResult.Code = item.sCode;
                        oResult.Description = item.sDisplayName;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oResult;
        }

        public CodeSystemItem GetbyDescription(string sCodeSystem, string sEMRDescription)
        {

            CodeSystemItem oResult = new CodeSystemItem();

            try
            {
                if (_dtCodes != null && _dtCodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtCodes.AsEnumerable()
                                      where dt.Field<String>("sCodeSystem") == sCodeSystem
                                      && dt.Field<String>("sEMRDisplayName") == sEMRDescription
                                      select new
                                      {
                                          sCodeSystem = dt.Field<String>("sCodeSystem"),
                                          sCodeSystemName = dt.Field<String>("sCodeSystemName"),
                                          sCode = dt.Field<String>("sCode"),
                                          sDisplayName = dt.Field<String>("sDisplayName"),
                                          sUnit = dt.Field<String>("sUnit")
                                      }).Take(1);


                    foreach (var item in _CodeQuery)
                    {
                        oResult.CodingSystem = item.sCodeSystem;
                        oResult.CodingSystemName = item.sCodeSystemName;
                        oResult.Code = item.sCode;
                        oResult.Description = item.sDisplayName;
                        oResult.Unit = item.sUnit;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oResult;
        }
    }

    public class TemplateIDMaster : IDisposable
    {
        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
           
            Dispose(true);
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_dtCodes!=null)
                    {
                        _dtCodes.Dispose();
                    }
                    if (_dtQRDACodes!=null)
                    {
                        _dtQRDACodes.Dispose();
                    }
                   
                }
            }
            disposed = true;
        }
        #endregion

        DataTable _dtCodes = null;
        DataTable _dtQRDACodes = null;
        public TemplateIDMaster()
        {
            LoadTemplateIds();
        }
        public TemplateIDMaster(int QRDA)
        {

            if (QRDA==1)
            {
                LoadQRDATemplateIds();
            }
        }

        private void LoadTemplateIds()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select SectionName,CDATemplateID,TemplateType from CCD_Modules where TemplateType is not null ";


                oDB.Retrive_Query(_sqlQuery, out _dtCodes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
        }
        private void LoadQRDATemplateIds()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select SectionName,CDATemplateID,TemplateType from QRDA_Modules where TemplateType is not null ";


                oDB.Retrive_Query(_sqlQuery, out _dtQRDACodes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
        }
        public String GetEntryID(string sEntryName)
        {

            String sResult = "";

            try
            {
                if (_dtCodes != null && _dtCodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtCodes.AsEnumerable()
                                      where dt.Field<String>("SectionName") == sEntryName
                                      && dt.Field<String>("TemplateType") == "entry"
                                      select new
                                      {
                                          sTemplateID = dt.Field<String>("CDATemplateID"),
                                      }).Take(1);


                    foreach (var item in _CodeQuery)
                    {
                        sResult = item.sTemplateID;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        public String GetSectionID(string sSectionName)
        {

            String sResult = "";

            try
            {
                if (_dtCodes != null && _dtCodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtCodes.AsEnumerable()
                                      where dt.Field<String>("SectionName") == sSectionName
                                      && dt.Field<String>("TemplateType") == "section"
                                      select new
                                      {
                                          sTemplateID = dt.Field<String>("CDATemplateID"),
                                      }).Take(1);


                    foreach (var item in _CodeQuery)
                    {
                        sResult = item.sTemplateID;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }
        public String GetQRDAEntryID(string sEntryName)
        {

            String sResult = "";

            try
            {
                if (_dtQRDACodes != null && _dtQRDACodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtQRDACodes.AsEnumerable()
                                      where dt.Field<String>("SectionName") == sEntryName
                                      && dt.Field<String>("TemplateType") == "entry"
                                      select new
                                      {
                                          sTemplateID = dt.Field<String>("CDATemplateID"),
                                      }).Take(1);


                    foreach (var item in _CodeQuery)
                    {
                        sResult = item.sTemplateID;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        public String GetQRDASectionID(string sSectionName)
        {

            String sResult = "";

            try
            {
                if (_dtQRDACodes != null && _dtQRDACodes.Rows.Count > 0)
                {
                    var _CodeQuery = (from dt in _dtQRDACodes.AsEnumerable()
                                      where dt.Field<String>("SectionName") == sSectionName
                                      && dt.Field<String>("TemplateType") == "section"
                                      select new
                                      {
                                          sTemplateID = dt.Field<String>("CDATemplateID"),
                                      }).Take(1);


                    foreach (var item in _CodeQuery)
                    {
                        sResult = item.sTemplateID;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

    }

    public enum CDAFileTypeEnum
    {
        None = 0,
        ClinicalSummary = 1,
        AmbulatorySummary = 2,
        CareRecordSummary = 3,
        QRDA1 = 4,
        QRDA3 = 5,
        CarePlan=6,
        UnstructuredCDA = 7
        
    }

    public class gloCDAWriterParameters : IDisposable
    {

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {

                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
         public static string  CDAStyleSheetPath = "";
        //private string _StyleSheetPath = @"http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl";
         private string _StyleSheetPath = CDAStyleSheetPath;
        public string StyleSheetPath 
        { 
            get{ return _StyleSheetPath; } 
            set{ _StyleSheetPath = value; }
        }

        public CDAFileTypeEnum CDAFileType { get; set; }

        public bool Demographics { get; set; }
        public bool SmokingStatus { get; set; }
        public bool Problems { get; set; }
        public bool Medications { get; set; }
        public bool Allergies { get; set; }
        public bool LaboratoryTest { get; set; }
        public bool LaboratoryResult { get; set; }
        public bool VitalSigns { get; set; }
        public bool CarePlan_GoalsAndInstructions { get; set; }
        public bool Procedures { get; set; }
        public bool CareTeamMember { get; set; }
        public bool ProviderName { get; set; }
        public bool OfficeContact { get; set; }
        public bool Visit_DateAndLocation { get; set; }
        public bool ChiefComplaint { get; set; }
        public bool ImmunizationsAdministered { get; set; }
        public bool MedicationsAdministered { get; set; }
        public bool DiagnosticTestsPending { get; set; }
        public bool ClinicalInstructions { get; set; }
        public bool FutureAppointments { get; set; }
        public bool ReferralsToOtherProviders { get; set; }
        public bool FutureScheduledTests { get; set; }
        public bool RecommendedPatientDecisionAids { get; set; }
        public bool EncounterDiagnoses { get; set; }
        public bool Immunizations { get; set; }
        public bool CognitiveStatus { get; set; }
        public bool FunctionalStatus { get; set; }
        public bool ReasonForReferral { get; set; }
        public bool ReferringProvider { get; set; }
        public bool CareProvider { get; set; }
        public bool CareOfficeContact { get; set; }
        public bool FamilyHistory { get; set; }
        public bool SocialHistory { get; set; }
        public bool MarkPatientCopy { get; set; }
        public bool Implant { get; set; }
        public bool Goals { get; set; }
        public bool HealthConcern { get; set; }
        public bool TreatmentPlan { get; set; }
        public bool Assessments { get; set; }
        public bool Outcomes { get; set; }
        public bool Interventions { get; set; }
        public string getFileType()
        {
            string _FileType="";
            try
            {
                if (CDAFileType == CDAFileTypeEnum.AmbulatorySummary)
                    _FileType = "Ambulatory Summary";
                else if (CDAFileType == CDAFileTypeEnum.ClinicalSummary)
                    _FileType = "Clinical Summary";
                else if (CDAFileType == CDAFileTypeEnum.CareRecordSummary)
                    _FileType = "Care Record Summary";
                else if (CDAFileType == CDAFileTypeEnum.CarePlan)
                    _FileType = "Care Plan";
                
            }
            catch //(Exception ex)
            {
                _FileType = "";
            }
            finally
            {
            }
            return _FileType;
        }
    }
}
