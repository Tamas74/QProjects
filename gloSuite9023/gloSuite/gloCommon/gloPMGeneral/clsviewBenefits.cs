using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace gloPMGeneral
{
    public class clsviewBenefits
    {
        #region "Private Variables"
        private bool disposed = false;
        private string _databaseconnectionstring = "";
        #endregion

        #region "Constructor & Destructor"
        public clsviewBenefits(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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

        ~clsviewBenefits()
        {
            Dispose(false);
        }
        #endregion

        #region "Private Methods"
        public DataSet GetPatientDemographicInformation(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            //DataTable dtPatientNote = new DataTable();
            //DataTable _dtPatientNote = new DataTable();
            try
            {
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("PAT_GetDemographics", oParameters, out dtSet);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtSet;
        }
        public DataTable getPatientInsuranceDetails(Int64 _PatientId, Int64 _InsuranceId)
        {
            DataTable _PatientInsuranceDetails = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _strQuery = " SELECT  nPatientID, ISNULL(sInsuranceName, '') AS InsuranceName, nInsuranceID," +
                    " nDeductableamount  AS Deductableamount, " +
                    " nCoveragePercent AS CoveragePercent, nCoPay AS CoPay, " +
                    " ISNULL(bAssignmentofBenifit, 0) AS AssignmentofBenifit,  dtStartDate, dtEndDate,     " +
                    " ISNULL(sInsTypeDescriptionDefault,'') AS sInsTypeDescriptionDefault, " +
                    " ISNULL(sEligibiltyInsuranceNote,'') AS InsuranceNote, " +
                    " ISNULL(PatientInsurance_DTL.sSubFName,'') AS SubFName,ISNULL(PatientInsurance_DTL.sSubMName,'') AS SubMName,ISNULL(PatientInsurance_DTL.sSubLName,'') AS SubLName," +
                    " ISNULL(PatientInsurance_DTL.sSubscriberID,'') AS SubscriberID,PatientInsurance_DTL.dtDOB, sUserName,dtReviewedDateTime,ISNULL(sGroup,'') As sGroup,ISNULL(bIsCompnay,0) AS bIsCompnay,ISNULL(sCompanyName,'') AS sCompany ,ISNULL(sRelationShip,'') AS sRelationShip" +
                    " FROM PatientInsurance_DTL WITH (NOLOCK)  WHERE nPatientID = " + _PatientId + " and nInsuranceID =" + _InsuranceId;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _PatientInsuranceDetails);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); }
            }
            return _PatientInsuranceDetails;

        }
        public DataTable getPatientInsurance(Int64 _PatientId)
        {
            DataTable _PatientInsurance = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _strQuery = " select ISNULL(nInsuranceID,0) AS InsuranceID,ISNULL(sInsuranceName,'') AS InsuranceName,"+ 
                                     "CASE ISNULL(nInsuranceFlag,0) "+
                                      " WHEN 0 THEN 4 "+
                                       " ELSE nInsuranceFlag END  AS SortOrder FROM dbo.PatientInsurance_DTL WHERE nPatientID=" + _PatientId + " ORDER BY SortOrder";
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _PatientInsurance);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); }
            }
            return _PatientInsurance;

        }
        public Int16 getPatientInsurance(Int64 _PatientId,Int64 _InsuranceID)
        {
            DataTable _PatientInsurance = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _Value = null;
            Int16 nInsuranceFlag=0;

            try
            {
                string _strQuery = "select ISNULL(nInsuranceFlag,0) AS InsuranceFlag FROM dbo.PatientInsurance_DTL WHERE nPatientID=" + _PatientId + " AND nInsuranceID=" + _InsuranceID;
                oDB.Connect(false);
                _Value=oDB.ExecuteScalar_Query(_strQuery);
                if (_Value != null)
                    nInsuranceFlag = Convert.ToInt16(_Value);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); }
            }
            return nInsuranceFlag;

        }
        public DataTable getPlaneligibilityInsurance(Int64 _PatientId, Int64 _InsuranceID)
        {
            DataTable _PatientEligibilityPlanDetails = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _strQuery = " SELECT ISNULL(Contacts_MST.sName,'') AS InsuranceName,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyContact,'') AS InsEligibiltyContact, dbo.formatPhone(ISNULL(Contacts_Insurance_DTL.sInsEligibiltyPhone,''),0) AS InsEligibiltyPhone," +
                                   " ISNULL(Contacts_Insurance_DTL.sInsEligibiltyWesite,'') AS InsEligibiltyWesite ,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyNote,'') AS InsEligibiltyNote ," +
                                   " ISNULL(dbo.Contacts_Insurance_DTL.sPayerId,'') AS PayerID,ISNULL(dbo.Contacts_Insurance_DTL.sInsuranceTypeDesc,'') AS Insurancetype " +
                                   " FROM  Contacts_MST left outer  join  dbo.Contacts_Insurance_DTL ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID WHERE Contacts_MST.nContactID=(SELECT TOP 1 nContactID  FROM dbo.PatientInsurance_DTL WHERE  nInsuranceID=" + _InsuranceID + "  AND nPatientID=" + _PatientId + ")";
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _PatientEligibilityPlanDetails);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); }
            }
            return _PatientEligibilityPlanDetails;

        }
        public bool UpdatePatientInsurancesBenefit(Int64 nPatientID, Int64 nInsuranceID, Decimal Copay, Decimal Deductible, Decimal CoInsurabce, DateTime dtStartDate, DateTime dtEndDate, bool IsNotStartDat, bool IsNotEndDate, string sPatintBenefitNote, string sUserName, DateTime dtReviewdDateTime)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            int Count = 0;
            bool Result = false;
            try
            {
                oDB.Connect(false);

                odbParams.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@InsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                // odbParams.Add("@SubscriberName", oInsuranceDetails.InsurancesDetails[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);

                odbParams.Add("@Deductableamount", Deductible, ParameterDirection.Input, SqlDbType.Decimal);
                //@CoveragePercent Decimal(18,0), 
                odbParams.Add("@CoveragePercent", CoInsurabce, ParameterDirection.Input, SqlDbType.Decimal);
                //@CoPay Decimal(18,0),
                odbParams.Add("@CoPay", Copay, ParameterDirection.Input, SqlDbType.Decimal);
                //@AssignmentofBenifit Bit,                   
                //@IsNotStartDate   bit,
                odbParams.Add("@IsNotStartDate", IsNotStartDat, ParameterDirection.Input, SqlDbType.Bit);
                //@dtStartDate DateTime, 
                odbParams.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.DateTime);
                //@IsNotEndDate   bit,
                odbParams.Add("@IsNotEndDate", IsNotEndDate, ParameterDirection.Input, SqlDbType.Bit);
                //@dtEndDate DateTime
                odbParams.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.DateTime);
                //@dtEndDate DateTime

                odbParams.Add("@sEligibilityInsuranceNote", sPatintBenefitNote, ParameterDirection.Input, SqlDbType.VarChar);


                odbParams.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@dtReviewedDateTime", dtReviewdDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                odbParams.Add("@bIsmarkReviewCheck", true, ParameterDirection.Input, SqlDbType.Bit);
                //@dtEndDate DateTime





                Count = oDB.Execute("PA_Update_InsurancesBenefit", odbParams);
                if (Count > 0)
                    Result = true;
                else
                    Result = false;
            }


            catch (gloDatabaseLayer.DBException dbex)
            {
                Result = false;
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                Result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB=null; }
                if (odbParams != null) { odbParams.Dispose(); odbParams = null; }
            }
            return Result;
        }
        public bool UpdatePatientInsurancesBenefit(Int64 nPatientID, Int64 nInsuranceID, Decimal Copay, Decimal Deductible, Decimal CoInsurabce, DateTime dtStartDate, DateTime dtEndDate, bool IsNotStartDat, bool IsNotEndDate, string sPatintBenefitNote)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            int Count = 0;
            bool Result = false;
            try
            {
                oDB.Connect(false);

                odbParams.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@InsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                // odbParams.Add("@SubscriberName", oInsuranceDetails.InsurancesDetails[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);

                odbParams.Add("@Deductableamount", Deductible, ParameterDirection.Input, SqlDbType.Decimal);
                //@CoveragePercent Decimal(18,0), 
                odbParams.Add("@CoveragePercent", CoInsurabce, ParameterDirection.Input, SqlDbType.Decimal);
                //@CoPay Decimal(18,0),
                odbParams.Add("@CoPay", Copay, ParameterDirection.Input, SqlDbType.Decimal);
                //@AssignmentofBenifit Bit,                   
                //@IsNotStartDate   bit,
                odbParams.Add("@IsNotStartDate", IsNotStartDat, ParameterDirection.Input, SqlDbType.Bit);
                //@dtStartDate DateTime, 
                odbParams.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.DateTime);
                //@IsNotEndDate   bit,
                odbParams.Add("@IsNotEndDate", IsNotEndDate, ParameterDirection.Input, SqlDbType.Bit);
                //@dtEndDate DateTime
                odbParams.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.DateTime);
                //@dtEndDate DateTime

                odbParams.Add("@sEligibilityInsuranceNote", sPatintBenefitNote, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sUserName", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@dtReviewedDateTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);

                odbParams.Add("@bIsmarkReviewCheck", false, ParameterDirection.Input, SqlDbType.Bit);
                Count = oDB.Execute("PA_Update_InsurancesBenefit", odbParams);
                if (Count > 0)
                    Result = true;
                else
                    Result = false;
            }


            catch (gloDatabaseLayer.DBException dbex)
            {
                Result = false;
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                Result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (odbParams != null) { odbParams.Dispose(); odbParams = null; }
            }
            return Result;
        }
        public DataSet GeteEligibilityAreaDetails(Int64 PatientID,Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            //DataTable dtPatientNote = new DataTable();
            //DataTable _dtPatientNote = new DataTable();
            string _strQuery = "";
            object nContactID = null;
            try
            {
                _strQuery = "SELECT TOP 1 nContactID  FROM dbo.PatientInsurance_DTL WHERE  nInsuranceID=" + InsuranceID + "  AND nPatientID=" + PatientID;
                oDB.Connect(false);
                nContactID = oDB.ExecuteScalar_Query(_strQuery);
                if (Convert.ToString(nContactID) != "" && nContactID != null)
                {
                    oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@ContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("EligibilityResponse", oParameters, out dtSet);
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtSet;
        }
        #endregion
    }
}
