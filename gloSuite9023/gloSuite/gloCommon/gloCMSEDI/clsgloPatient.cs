using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace gloCMSEDI
{
    #region "Enum Declarations"

    public enum PatientOtherContactType
    {
        None = 0,
        Guarantor = 1,
        Patient = 2
    }

    public enum TypeOfBilling
    {
        None = 0,
        Electronic = 1,
        Paper = 2
    }

    public enum StudentStatus
    {
        FullTime = 1,
        PartTime = 2
    }

    public enum PatientContactType
    {
        None = 0,
        Pharmacy = 1,
        PrimaryCarePhysician = 2,
        Referral = 3
    }

    public enum WorkersComp
    {
        None = 0,
        WokersComp = 1,
        Autoclaim = 2,
    }

    public enum ModifyPatientDetailType
    {
        None = 0,
        Insurance = 1,
        Guarantor = 2,
        Guardian = 3,
        Occupation = 4,
        OtherInfo = 5,
        Referral = 6
    }

    #endregion

    public class clsgloPatient : IDisposable 
    {
        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";


        public clsgloPatient(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
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

        ~clsgloPatient()
        {
            Dispose(false);
        }

        #endregion

        public DataTable getPatientInsurances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID,isnull(Contacts_MST.sName,'')as InsuranceName ,ISNULL(sSubscriberID,'') AS sSubscriberID, ISNULL(sSubscriberName,'') AS sSubscriberName,ISNULL(sSubscriberPolicy#,'') As sSubscriberPolicy#, ISNULL(sGroup,'') as sGroup,PatientInsurance_DTL.sPhone ,bPrimaryFlag,PatientInsurance_DTL.dtDOB,PatientInsurance_DTL.dtEffectiveDate,PatientInsurance_DTL.dtExpiryDate,PatientInsurance_DTL.sSubscriberID FROM   Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID where nPatientID='" + PatientID + "'";
                // Changed on  20080926 

                //*********************************************************************************************
                //Code Changes on - 20081011 By - Sagar Ghodke
                //Below Commented Code is previous logic
                //Code Changed for not loading the Insurances which are inactive

                #region " Previous Code "
                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID,isnull(Contacts_MST.sName,'')as InsuranceName ,ISNULL(sSubscriberID,'') AS sSubscriberID, (isnull(sSubFName,'') + Space(1) + ISNULL(sSubMName,'') + Space(1) + ISNULL(sSubLName,'')) AS sSubscriberName, ISNULL(sSubscriberPolicy#,'') As sSubscriberPolicy#, ISNULL(sGroup,'') as sGroup, " +
                //    " PatientInsurance_DTL.sPhone ,bPrimaryFlag,PatientInsurance_DTL.dtDOB,PatientInsurance_DTL.dtEffectiveDate,PatientInsurance_DTL.dtExpiryDate,PatientInsurance_DTL.sSubscriberID, " +
                //    " isnull(sSubFName,'') As SubFName, ISNULL(sSubMName,'') AS SubMName, ISNULL(sSubLName,'') as SubLName, ISNULL(nRelationShipID,0) as RelationShipID , ISNULL(sRelationShip,'') AS RelationShip, ISNULL(nDeductableamount,0) as Deductableamount, ISNULL(nCoveragePercent,0) as CoveragePercent, ISNULL(nCoPay,0) as CoPay, ISNULL(bAssignmentofBenifit,0) as AssignmentofBenifit, dtStartDate, dtEndDate " +
                //    " FROM   Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID where nPatientID='" + PatientID + "'";
                #endregion " Previous Code "

                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID, ISNULL(Contacts_MST.sName, '') AS InsuranceName, ISNULL(PatientInsurance_DTL.sSubscriberID, '')  "+
                //      " AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) "+
                //      " + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, "+
                //      " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, PatientInsurance_DTL.sPhone, PatientInsurance_DTL.bPrimaryFlag, "+
                //      " PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, "+
                //      " PatientInsurance_DTL.sSubscriberID AS Expr1, ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, "+
                //      " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, "+
                //      " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, "+
                //      " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, "+
                //      " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, "+
                //      " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, CASE isnull(PatientInsurance_DTL.nInsuranceFlag, 0)  "+
                //      " WHEN 0 THEN 4 ELSE PatientInsurance_DTL.nInsuranceFlag END AS nInsuranceFlag,  "+
                //      " PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID ,ISNULL(Contacts_MST.sPayerID,'') AS PayerID, ISNULL(Patient.sCity,'') AS sCity, "+ 
                //      " ISNULL(Patient.sState,'') AS sState, ISNULL(Patient.sZIP,'') AS sZIP, ISNULL(Patient.sAddressLine1,'') AS sAddress1, ISNULL(Patient.sAddressLine2,'') AS sAddress2," +
                //      " ISNULL(Contacts_MST.sInsuranceTypeCode,'') AS InsuranceTypeCode, ISNULL(PatientRelationship.sRelationshipCode,'') AS RelationshipCode "+
                //      " FROM Contacts_MST INNER JOIN " +
                //      " PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID INNER JOIN "+
                //      " Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN "+
                //      " PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID where PatientInsurance_DTL.nPatientID='" + PatientID + "'  ORDER BY nInsuranceFlag";

                //string _strQuery = "SELECT  PatientInsurance_DTL.nInsuranceID, ISNULL(Contacts_MST.sName, '') AS InsuranceName, ISNULL(PatientInsurance_DTL.sSubscriberID, '') "
                //+ " AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)  "
                //+ " + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#,  "
                //+ " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, PatientInsurance_DTL.sPhone,PatientInsurance_DTL.bPrimaryFlag,  "
                //+ " PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate,  "
                //+ " PatientInsurance_DTL.sSubscriberID AS Expr1, ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  "
                //+ " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,  "
                //+ " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  "
                //+ " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, "
                //+ " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  "
                //+ " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, CASE isnull(PatientInsurance_DTL.nInsuranceFlag, 0)  "
                //+ " WHEN 0 THEN 4 ELSE PatientInsurance_DTL.nInsuranceFlag END AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender,  "
                //+ " PatientInsurance_DTL.sPayerID, ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP,  "
                //+ " ISNULL(Patient.sAddressLine1, '') AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '')  "
                //+ " AS RelationshipCode, Contacts_MST.nContactID, ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode,  "
                //+ " ISNULL(Contacts_Insurance_DTL.sPayerId, '') AS PayerID, "
                //+ " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'  "
                //+ " ELSE '' END  AS sInsuranceFlag "
                //+ " FROM Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID  "
                //+ " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  "
                //+ " INNER JOIN PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  "
                //+ " LEFT OUTER JOIN Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID" +
                //" where PatientInsurance_DTL.nPatientID='" + PatientID + "'  ORDER BY nInsuranceFlag";

                //string _strQuery =  " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag  " +
                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " INNER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY nInsuranceFlag ";

                //string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag,  " +

                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 4 " +
                //                    " ELSE nInsuranceFlag END  AS SortOrder  " +

                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " LEFT OUTER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY SortOrder ";

                string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                                    " PatientInsurance_DTL.sPhone, " +
                                    " PatientInsurance_DTL.dtDOB,  " +
                                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                                    " PatientInsurance_DTL.dtExpiryDate,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                                    " PatientInsurance_DTL.sSubscriberGender,  " +
                                    " PatientInsurance_DTL.sPayerID,  " +
                                    " ISNULL(Patient.sCity, '') AS sCity, " +
                                    " ISNULL(Patient.sState, '') AS sState,  " +
                                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                                    " ELSE '' END  AS sInsuranceFlag,  " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 4 " +
                                    " ELSE nInsuranceFlag END  AS SortOrder,  " +
                                    " PatientInsurance_DTL.sInsurancePhone, " +
                                    " ISNULL(PatientInsurance_DTL.bworkerscomp,0) AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim,0) AS bAutoClaim, " +
                                    " ISNULL(PatientInsurance_DTL.sEmployer, '') AS sEmployer " +
                                    " FROM PatientInsurance_DTL WITH (NOLOCK) " +
                                    " INNER JOIN Patient WITH (NOLOCK) ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                                    " LEFT OUTER JOIN PatientRelationship  WITH (NOLOCK) ON  " +
                                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY SortOrder ";


                //End Code Changes - 20081011 By - Sagar Ghodke
                //*********************************************************************************************

                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }


        //MaheshB 20100104
        public DataTable getTransactionInsurances(Int64 _nTransID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                string _strQuery = "SELECT DISTINCT BL_Transaction_InsPlan.nResponsibilityNo, " +
                      " PatientInsurance_DTL.nInsuranceID AS Expr1, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberID, '') AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) " +
                      " + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, " +
                      " PatientInsurance_DTL.sPhone, PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, " +
                      " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                      " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, " +
                      " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                      " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, " +
                      " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, " +
                      " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID, " +
                      " ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP, ISNULL(Patient.sAddressLine1, '') " +
                      " AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '') AS RelationshipCode, " +
                      " ISNULL(PatientInsurance_DTL.nContactID, 0) AS nContactID, ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                      " ISNULL(PatientInsurance_DTL.sPayerID, '') AS PayerID, ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2, ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState, ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip, " +
                      " ISNULL(PatientInsurance_DTL.sZIP, '') AS PayerZip, ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity, ISNULL(PatientInsurance_DTL.sState, '') " +
                      " AS PayerState, ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1,PatientInsurance_DTL.sInsurancePhone, ISNULL(PatientInsurance_DTL.bworkerscomp, 0) " +
                      " AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim, 0) AS bAutoClaim, BL_Transaction_InsPlan.nTransactionID, " +
                      " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, CASE ISNULL(BL_Transaction_InsPlan.nResponsibilityNo, 0) " +
                      " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE '' END AS sInsuranceFlag," +
                      " BL_Transaction_InsPlan.nInsuranceID,IsNull(PatientInsurance_DTL.bAssignmentofBenifit,0) as bAssignmentofBenifit, " +
                      " ISNULL(PatientInsurance_DTL.bworkerscomp,0) AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim,0) AS bAutoClaim, " +
                      " ISNULL(PatientInsurance_DTL.sEmployer, '') AS sEmployer " +
                      " FROM PatientInsurance_DTL WITH (NOLOCK) INNER JOIN " +
                      " Patient WITH (NOLOCK) ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                     "  BL_Transaction_InsPlan WITH (NOLOCK) ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_InsPlan.nInsuranceID LEFT OUTER JOIN " +
                     "  PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID " +
                      "   WHERE (BL_Transaction_InsPlan.nTransactionID = '" + _nTransID + "' ) and  nResponsibilityNo<>0 and BL_Transaction_InsPlan.nInsuranceID <>0 order by BL_Transaction_InsPlan.nResponsibilityNo";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        //MaheshB 20100311
        public DataTable getTransactionInsurances(Int64 _nTransID, int _nResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                string _strQuery = "SELECT DISTINCT BL_Transaction_InsPlan.nResponsibilityNo, " +
                      " PatientInsurance_DTL.nInsuranceID AS Expr1, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberID, '') AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) " +
                      " + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, " +
                      " PatientInsurance_DTL.sPhone, PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, " +
                      " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                      " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, " +
                      " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                      " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, " +
                      " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, " +
                      " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID, " +
                      " ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP, ISNULL(Patient.sAddressLine1, '') " +
                      " AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '') AS RelationshipCode, " +
                      " ISNULL(PatientInsurance_DTL.nContactID, 0) AS nContactID, ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                      " ISNULL(PatientInsurance_DTL.sPayerID, '') AS PayerID, ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2, ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState, ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip, " +
                      " ISNULL(PatientInsurance_DTL.sZIP, '') AS PayerZip, ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity, ISNULL(PatientInsurance_DTL.sState, '') " +
                      " AS PayerState, ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1,PatientInsurance_DTL.sInsurancePhone, ISNULL(PatientInsurance_DTL.bworkerscomp, 0) " +
                      " AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim, 0) AS bAutoClaim, BL_Transaction_InsPlan.nTransactionID, " +
                      " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, CASE ISNULL(BL_Transaction_InsPlan.nResponsibilityNo, 0) " +
                      " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE '' END AS sInsuranceFlag," +
                      " BL_Transaction_InsPlan.nInsuranceID,IsNull(PatientInsurance_DTL.bAssignmentofBenifit,0) as bAssignmentofBenifit, " +
                      "(case when BL_Transaction_InsPlan.nResponsibilityNo = " + _nResponsibilityNo + " then 0 else nResponsibilityNo end) as ResponsibilityNo ," +
                      " ISNULL(PatientInsurance_DTL.sEmployer, '') AS sEmployer, " +
                      //" ISNULL(BL_Transaction_InsPlan.sClaimRemittanceRefNo, '') AS sClaimRemittanceRefNo " +
                      " dbo.GET_ClaimRemittanceRef(BL_Transaction_InsPlan.nTransactionID,BL_Transaction_InsPlan.nContactID,BL_Transaction_InsPlan.nInsuranceID) as sClaimRemittanceRefNo " +
                      " FROM PatientInsurance_DTL WITH (NOLOCK) INNER JOIN " +
                      " Patient WITH (NOLOCK) ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                     "  BL_Transaction_InsPlan WITH (NOLOCK) ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_InsPlan.nInsuranceID " +
                     "  And PatientInsurance_DTL.nPatientID = BL_Transaction_InsPlan.nPatientID LEFT OUTER JOIN " +
                     "  PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID " +
                      "   WHERE (BL_Transaction_InsPlan.nTransactionID = '" + _nTransID + "' ) and  nResponsibilityNo<>0 and BL_Transaction_InsPlan.nInsuranceID <>0 order by ResponsibilityNo,BL_Transaction_InsPlan.nResponsibilityNo";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtInsurance);
                oDB.Disconnect();
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }

        public DataTable getTransactionInsurancesCMS(Int64 _nTransID, int _nResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtTrans = null;

            try
            {
                oDBParameters.Add("@nTransactionID", _nTransID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", _nResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Transaction_InsPlan_CMS", oDBParameters, out dtTrans);

            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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
            return dtTrans;
        }
    }

    public class gloInsurance
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        public gloInsurance(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //Sandip Darade 20090428
            //read messageboxcaption from  application settings
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
            //

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

        ~gloInsurance()
        {
            Dispose(false);
        }

        #endregion

  
        public System.Data.DataTable GetInsurance(Int64 InsuranceID)
        {
            System.Data.DataTable _result = new System.Data.DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string _sqlQuery = "SELECT nContactID, Isnull(sContact,'') as Contact, Isnull(sName,'')as Name, Isnull(sStreet,'')as Street, Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax, Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL, Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked FROM Contacts_MST where bIsBlocked=0 and sContactType='Insurance' and  nContactID='" + InsuranceID.ToString() + "'";

                string _sqlQuery = " SELECT nInsuranceID, " +
                    " ISNULL(sInsuranceName,'') AS Name, " +
                    " ISNULL(sAddressLine1,'') AS sAddressLine1, " +
                    " ISNULL(sAddressLine2,'') AS sAddressLine2, " +
                    " Isnull(sCity,'') as City,  " +
                    " Isnull(sState,'') as State, " +
                    " Isnull(sZIP,'') as Zip, " +
                    " Isnull(sPhone,'') as Phone,  " +
                    " Isnull(sFax,'') as Fax, " +
                    " Isnull(sEmail,'') as Email, " +
                    " Isnull(sURL,'') as URL,  " +
                    " ISNULL(nInsuranceFlag,0) AS nInsuranceFlag " +
                    " FROM  " +
                    " PatientInsurance_DTL WITH (NOLOCK) " +
                    " WHERE nInsuranceID = " + InsuranceID.ToString() + " ";

                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch //(gloDatabaseLayer.DBException DBErr)
            {
            }
            //catch (Exception ex)
            //{
            //}
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

      
      
   
    }

    public class GeneralSettings
    {
        #region "Constructor & Distructor"

        private string _databaseConnectionString = "";

        //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        public GeneralSettings(string DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;

            //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //Sandip Darade 20090428
            //read messageboxcaption from  application settings
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
            //

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

        ~GeneralSettings()
        {
            Dispose(false);
        }
        #endregion

        public void GetSetting(string SettingName, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = '" + SettingName.Trim().ToUpper() + "' AND nClinicID = " + _ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
        }

        public void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "' AND nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                //Code Added on 20091205-Mayuri
                //gloAudittrail dll added
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
    }

    public class gloClinicalQueueFunctions
    {
        private static bool _isTSPrintDialogOpen = false;

        public static bool CopyPrintDoc(String outputFile,String claimType, String PrintType)
        {
            try
            {
                if (outputFile != null)
                {
                    List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> SplitDocList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();


                    gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                    physicalDoc.PdfFileName = outputFile;
                    physicalDoc.SrcFileName = outputFile;
                    physicalDoc.footerInfo = null;
                    SplitDocList.Add(physicalDoc);




                    //'Generate MetaData File
                    //Dim PDFWithoutPath As String = PDFFileName.Substring(PDFFileName.LastIndexOf("\") + 1)
                    // Dim strMetaDataFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".xml", "MMddyyyyHHmmssffff")

                    String strMetaDataFilePath = "";
                    Boolean MetaDataGenerated = false;
                    if (!gloGlobal.gloTSPrint.UseZippedMetadata)
                    {
                        strMetaDataFilePath = gloGlobal.gloTSPrint.TempPath + "01" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml";
                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, claimType, PrintType, bUseFileZip:false);
                    }

                    //'Copy Files to mapped virtual drive
                    if (MetaDataGenerated || gloGlobal.gloTSPrint.UseZippedMetadata)
                    {
                        gloAuditTrail.gloAuditTrail.PrintLog(strException: "MetaData file generated for Word Printing.", ShowMessageBox: false);
                        if (gloGlobal.gloTSPrint.isMapped())
                        {
                            //File.Copy(PDFFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                            String PDFWithoutPath = "";
                            bool First = true;
                            for (int fileCntr = 0; fileCntr <= SplitDocList.Count - 1; fileCntr++)
                            {
                                PDFWithoutPath = SplitDocList[fileCntr].PdfFileName.Substring(SplitDocList[fileCntr].PdfFileName.LastIndexOf("\\") + 1);
                                gloGlobal.gloTSPrint.CopyFileToNetworkShare(SplitDocList[fileCntr].PdfFileName, gloGlobal.gloTSPrint.AppFolderPath + "\\" + PDFWithoutPath);
                                if (First)
                                {
                                    if (!gloGlobal.gloTSPrint.UseZippedMetadata)
                                    {
                                        gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\\") + 1));
                                    }
                                    else
                                    {
                                        strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath + "\\01" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xmz";
                                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, claimType, PrintType, bUseFileZip: true);
                                    }
                                    First = false;
                                }
                            }
                            
                            gloAuditTrail.gloAuditTrail.PrintLog(strException: "PDF and MetaData files copied to virtual drive for Word Printing.", ShowMessageBox: false);
                        }
                        else
                        {
                            if (_isTSPrintDialogOpen == false)
                            {
                                _isTSPrintDialogOpen = true;
                                DialogResult s = MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (s == System.Windows.Forms.DialogResult.Yes)
                                {
                                    _isTSPrintDialogOpen = false;
                                    gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found. Using RDP printer", ShowMessageBox: false);
                                    return false;
                                }
                                else
                                {
                                    _isTSPrintDialogOpen = false;
                                    gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found. Document Not Printed", ShowMessageBox: false);
                                    return true;
                                }
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found messagebox already active. Document Not Printed", ShowMessageBox: false);
                                return true;
                            }


                        }

                        return true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.PrintLog(strException: "Error in MetaData file generation for Word Printing.", ShowMessageBox: false);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
                return false;
            }
            finally
            {
                gloGlobal.gloTSPrint.SetTestPatient();
            }
        }

        private static bool GenerateMetaDataFile(string strFilePath, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> PhysicalFile, String claimType, String PrintType, Boolean bUseFileZip = false)
        {
            gloClinicalQueueGeneral.gloQueueMetadatawriter _QueueWriter = new gloClinicalQueueGeneral.gloQueueMetadatawriter();
            gloClinicalQueueGeneral.Queue QueueDoc = null;
            try
            {
                //Dim strFilePath As String = GenerateClinicalChartFileName(ds, 0, True)

                QueueDoc = _QueueWriter.GenerateWordMetaDataFile(gloGlobal.gloTSPrint.PatientName, gloGlobal.gloTSPrint.PatientDOB, gloGlobal.gloTSPrint.AddFooterInService, PhysicalFile, strFilePath.Substring(strFilePath.LastIndexOf("\\") + 1),isClaim:true,claimType:claimType,PrintType:PrintType);
                try
                {
                    gloQueueSchema.gloSerialization.SetClinicalDocument(strFilePath, QueueDoc, bUseFileZip);
                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                    ex = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
                return false;
            }
            finally
            {
                if ((_QueueWriter != null))
                {
                    _QueueWriter.Dispose();
                    _QueueWriter = null;
                }
                if ((QueueDoc != null))
                {
                    QueueDoc = null;
                }
            }

        }
    }


}
