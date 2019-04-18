using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using gloPatient;
using gloSettings;

namespace gloStripControl
{
    public class DateOfBirthIntegers
    {
        public Int32 DOBYears { get; set; }
        public Int32 DOBMonths { get; set; }
        public Int32 DOBDays { get; set; }
    }

    public static class PatientStripControl
    {
        #region " Variable Declarations "

        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
       
        public static  Boolean gblnAddModPatient = false;        

        #endregion

        #region " Methods & Procedures "

        public static Int64 GetPatientID(Int64 ClaimNumber, string SubClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _patientID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT nPatientID FROM BL_Transaction_MST " +
                            " WHERE nClaimNo = " + ClaimNumber + " and nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + " ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _patientID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _patientID;
        }

        public static Int64 GetPatientAccountID(Int64 ClaimNumber, string SubClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _nPAccountID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT nPAccountID FROM BL_Transaction_MST " +
                            " WHERE nClaimNo = " + ClaimNumber + " and nClinicID = " +  gloGlobal.gloPMGlobal.ClinicID + " ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _nPAccountID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _nPAccountID;
        }

        public static DataRow GetPatientDetails(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;

            DataRow _patientData = null;

            try
            {
                _sqlQuery = " SELECT Patient.nPatientID, Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') " +
                            " AS PatientName, Patient.dtDOB AS DOB, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) " +
                            " + ISNULL(Provider_MST.sLastName, '') AS PrName, Patient.nProviderID AS ProviderID, ISNULL(Patient.sPhone, '') AS PatPhone, ISNULL(Patient.sOccupation, '') " +
                            " AS PatientOccupation, ISNULL(Patient.sMobile, '') AS PatientCellPhone, ISNULL(Patient.nSSN, '') AS SSN, ISNULL(Patient.sGender, '') AS Gender, " +
                            " ISNULL(Patient.sMaritalStatus, '') AS sMaritalStatus, ISNULL(Patient.sHandDominance, '') AS HandDominance " +
                            " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " +
                            " WHERE Patient.nPatientID = " + PatientID + " AND Patient.nClinicID = " +  gloGlobal.gloPMGlobal.ClinicID + "";

                oDB.Connect(false);

                DataTable dtPatient = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dtPatient);

                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    _patientData = dtPatient.Rows[0];
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _patientData;
        }

        public static DataRow GetPatientAccountDetails(Int64 AccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;

            DataRow _AccountData = null;

            try
            {
                _sqlQuery = "select nPAccountID,sAccountDesc ,(Select LTrim(RTrim(sFirstName +' '+sLastName)) from Patient_OtherContacts "
                            + " where nPatientContactID = nGuarantorId)as sGuarantorName, "
                            + " (select PA_Accounts.sAccountNo + '-' +(Select LTrim(RTrim(sFirstName + ' ' + sLastName)) from Patient_OtherContacts "
                            + "  where nPatientContactID = nGuarantorId)) as sAccount, "
                            + " nGuarantorID "
                            + " from PA_Accounts where nPAccountID=" + AccountID + " AND PA_Accounts.nClinicID = " +  gloGlobal.gloPMGlobal.ClinicID + "";

                oDB.Connect(false);

                DataTable dtAccount = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dtAccount);

                if (dtAccount != null && dtAccount.Rows.Count > 0)
                {
                    _AccountData = dtAccount.Rows[0];
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _AccountData;
        }

        public static DataTable GetInsuranceParties(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable dtInsuranceParties = null;

            try
            {
                //Int64 _patientID = GetPatientID(ClaimNumber, SubClaimNumber);

                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                
                oDB.Retrive("BL_SELECT_CLAIM_INSURANCES_REVISED_V2", oParameters, out dtInsuranceParties);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtInsuranceParties;
        }
        public static string GetCollectionAgencyname(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            string strCollectionAgency = "";
            try
            {
                oDB.Connect(false);
                string sqlQuery = "SELECT ISNULL(sName,'') AS collectionagency FROM dbo.Contacts_MST WHERE nContactID=" + nContactID;

                oDB.Retrive_Query(sqlQuery, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        strCollectionAgency = Convert.ToString(dt.Rows[0][0]);
                    }
                    else
                    {
                        strCollectionAgency = "";
                    }
                }
                else
                {
                    strCollectionAgency = "";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return strCollectionAgency;
        }
        public static DataTable GetSplittedClaims(Int64 ClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtSplittedClaims = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nClaimNo", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID",  gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                oDB.Retrive("BL_SELECT_SplitClaimsByClaimNumber", oParameters, out _dtSplittedClaims);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtSplittedClaims != null) { _dtSplittedClaims.Dispose(); }
            }
            return _dtSplittedClaims;
        }

        public static DataTable GetPatientResereveBalances(Int64 PatientID, out decimal Copay, out decimal Advance, out decimal Others)
        {
            System.Collections.SortedList _reserveList = new System.Collections.SortedList();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtPatientBalance = new DataTable();

            Copay = 0;
            Advance = 0;
            Others = 0;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID",  gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                oDB.Retrive("dbo.BL_GET_PATIENT_RESERVE_BALANCE", oParameters, out _dtPatientBalance);
                oDB.Disconnect();

                if (_dtPatientBalance != null)
                {
                    Int32 _reserveType = 0;
                    decimal _reserveAmount = 0;

                    foreach (DataRow row in _dtPatientBalance.Rows)
                    {
                        _reserveType = Convert.ToInt32(row["nPaymentNoteSubType"]);

                        if (!String.IsNullOrEmpty(Convert.ToString(row["AvailableReserve"])))
                        { _reserveAmount = Convert.ToDecimal(row["AvailableReserve"]); }

                        if (_reserveType.Equals(2))
                        {
                            Copay = _reserveAmount;
                        }
                        else if (_reserveType.Equals(3))
                        {
                            Advance = _reserveAmount;
                        }
                        else if (_reserveType.Equals(10))
                        {
                            Others = _reserveAmount;
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
                if (oDB != null) { oDB.Dispose(); }
                if (_dtPatientBalance != null) { _dtPatientBalance.Dispose(); }
            }
            return _dtPatientBalance;
        }

        internal static bool IsInsurancePlanOnHold(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isOnHold = false;

            try
            {
                _sqlQuery = " select bIsHold from BL_Insurance_PlanHold where nContactID = " + nContactID + "";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isOnHold = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isOnHold;
        }

        public static string FormatAge(DateTime BirthDate)
        {
            DateTime _BDate = BirthDate;
            // Compute the difference between BirthDate 'CODE FROM gloPM
            //year and end year. 
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
            string _AgeStr = "";

            if (years == 0)
            {
                if (months == 0)
                {


                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Months " + days + " Days";
                    }
                }
            }
            else if (years == 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Days";
                    }
                }
            }
            else if (years > 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Days";
                    }
                }
            }
            return _AgeStr;
        }

        public static string GetFormattedClaimPaymentNumber(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
                //NumberSize = NumberSize;
            }
            return NumberSize;
        }

        public static DataSet GetPatientDemographicInformation(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("PAT_GetDemographics", oParameters, out dtSet);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtSet;
        }

        public static DataSet GetAccountFollowUp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("CL_GetAccountFollowUp", oParameters, out dtSet);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtSet;
        }

        public static DataTable GetPatientName(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatName = new DataTable();
            try
            {
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("PAT_getName", oParameters, out dtPatName);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtPatName;
        }

        public static DataTable GetPatientAccounts(long PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtAccounts = null;

            try
            {
                if (PatientID > 0)
                {
                    oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID",  gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("PA_Select_PatientsAccounts", oParameters, out dtAccounts);
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtAccounts;
        }

        public static DataTable GetAccountPatients(long AccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = null;
            try
            {
                if (AccountID > 0)
                {
                    oParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID",  gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("PA_Select_PatientsAccounts", oParameters, out dtPatients);
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtPatients;
        }

        public static DataSet GetPatientBalances(Int64 PatientID, Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("PA_GetPatientAccountBalances_V2", oParameters, out dtSet);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtSet;
        }

        public static DataSet GetAccountBalances(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {

                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("PA_GetAccountBalances_V2", oParameters, out dtSet);
                oDB.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtSet;
        }

        //added by mahesh S(Apollo): Get Account Count for patient. 
        public static bool GetPatientAccountCount(Int64 gridPatientId)
        {
            Int32 nAccountCount = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool blnRetvalue = false;
            try
            {
                oParameters.Add("@nPatientId", gridPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                nAccountCount = Convert.ToInt16(oDB.ExecuteScalar("PA_GetPatientAccountCount", oParameters));
                oDB.Disconnect();
                if (nAccountCount < 1)
                {
                    blnRetvalue = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
            return blnRetvalue;
        }

        public static Int64 GetAccountStatementCount(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;
            Int64 _nStmtCount = 0;
            try
            {
                _sqlQuery = "SELECT ISNULL(nStatementCount,0) AS nStmtCount FROM dbo.CL_StatementCount WHERE nPAccountID = " + PAccountID;
                        //"SELECT  dbo.PA_Aging_Get_StatementCount_LastPayment_V2(" + PAccountID + ",dbo.gloGetDate()) AS nStmtCount ";
                        oDB.Connect(false);
                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        oDB.Disconnect();
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            _nStmtCount = Convert.ToInt64(_dt.Rows[0]["nStmtCount"]);
                        }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dt != null) { _dt.Dispose(); }
            }
            return _nStmtCount;
        }

        public static DataTable GetClaimsOnHold(Int64 PatientID, Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtClaimOnHold = new DataTable();
            try
            {
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID",  gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("Patient_Financial_View_Header_ClaimOnHold_V2", oParameters, out dtClaimOnHold);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
            return dtClaimOnHold;
        }

        public static bool Lockclaims(string ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;
            bool _isUpdateRecordStatus = false;
            string[] mainClaimno = null;
            Int64 _nTransactionID = 0;
            try
            {
                if (ClaimNo != "")
                {
                    
                    mainClaimno = ClaimNo.Split('-');
                    if (mainClaimno != null)
                    {

                        Int64 nClaimNo = Convert.ToInt64(mainClaimno[0]);

                        _sqlQuery = "SELECT ISNULL(nTransactionMasterID,0) AS nTransactionMasterID FROM BL_Transaction_Claim_MST WITH (NOLOCK) " +
                        " WHERE nClaimNo = " + nClaimNo;
                        oDB.Connect(false);
                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        oDB.Disconnect();
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            _nTransactionID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                        }

                        _isUpdateRecordStatus = UpdateRecordStatus(_nTransactionID, nClaimNo, true);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dt != null) { _dt.Dispose(); }
            }
            return _isUpdateRecordStatus;
        }

        public static bool UpdateRecordStatus(Int64 TransactionMSTId, Int64 nClaimNo, bool LockStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            bool _isUpdateStatus = false;
            Int64 _result=0;

            try
            {
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionMasterID", TransactionMSTId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClaimNo", nClaimNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sMachineName", Environment.MachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sUserName", gloSettings.AppSettings.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Connect(false);
                _result= oDB.Execute("BL_LockClaims", oDBParameters);
                oDB.Disconnect();
                if (_result > 0)
                {
                    _isUpdateStatus = true;
                }

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
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
            return _isUpdateStatus;
        }

        public static bool IsRecordOpen(string nClaimNo, out string MachineName, out Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;
            bool _isRecordOpen = false;
            string _MachineId = "";
            string _RecordMachineId = "";
            Int64 _RecordUserId = 0;
            Int64 _nTransactionID = 0;


            try
            {
                if (nClaimNo != "")
                {
                    
                    //...Start : Code added by Sagar Ghodke, Date : 12/31/2012
                    //...Claim search showing claim locked message even if the claim was not opened on other machine.
                    //...E.x. If searched for claim "204-2" and another claim "202" (use same numbers for claim or same type 
                    //... of claim numbers with difference of 2 (204 minus 202 = 2)). 
                    //... the sql query converts "204-2" into "202" and returns that the claim is locked.
                    //... Solution : Splitting the searched claim number and sending only the main claim#
                    if (!String.IsNullOrEmpty(nClaimNo))
                    {
                        string[] _claim = nClaimNo.Split('-');

                        if (_claim.Length.Equals(2))
                        {
                            nClaimNo = Convert.ToString(_claim[0]);
                        }

                    }
                    //...End (9) : Code change finished Sagar Ghodke Date : 12/31/2012

                    oDB.Connect(false);
                    _MachineId = Environment.MachineName;
                    _sqlQuery = "SELECT ISNULL(nTransactionMasterID,0) AS nTransactionMasterID FROM BL_Transaction_Claim_MST WITH (NOLOCK) " +
                        " WHERE nClaimNo = " + nClaimNo;
                    //" WHERE nClaimNo = " + nClaimNo;
                    oDB.Retrive_Query(_sqlQuery, out _dt);
                    oDB.Disconnect();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _nTransactionID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                    }
                    if (_nTransactionID > 0)
                    {
                        _sqlQuery = "SELECT ISNULL(bIsLocked,'false') AS bIsOpened , ISNULL(sMachineName,'') AS sMachineID,ISNULL(nUserID,0) AS nUserID " +
                            " FROM BL_Transaction_MST_Locks WITH (NOLOCK) " +
                            " WHERE nTransactionMasterID = " + _nTransactionID +" AND sMachineName <> '" + _MachineId + "' ";;

                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        oDB.Disconnect();
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            _isRecordOpen = Convert.ToBoolean(_dt.Rows[0]["bIsOpened"]);
                            _RecordMachineId = Convert.ToString(_dt.Rows[0]["sMachineID"]);
                            _RecordUserId = Convert.ToInt64(_dt.Rows[0]["nUserID"]);
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dt != null) { _dt.Dispose(); }
            }

            MachineName = _RecordMachineId;
            UserId = _RecordUserId;
            return _isRecordOpen;
        }

        #region "HL7"

        public static void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string _ConnectionString)
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", "1", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);
                // string strTestName = "";

                oDBParamters.Add("@sField1", "", ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBLayer != null) { oDBLayer.Dispose(); }
                if (oDBParamters != null) { oDBParamters.Dispose(); }

            }
        }

     

        #endregion

        public static bool IsFollowUpEnable()
        {
            bool IsFollowUpEnable = false;
            Object _retSettingValue = null;
            try
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { IsFollowUpEnable = Convert.ToBoolean(_retSettingValue); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return IsFollowUpEnable;
        }

        public static bool ShowEMRAlertsOnPatientBanner()
        {
            bool isAllowed = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(sSettingsValue, '') AS sSettingsValue FROM Settings  WHERE sSettingsName = 'ShowEMRAlertsOnPMPatientBanner'  AND nClinicID =1  ";
                oDB.Retrive_Query(_sqlQuery, out _dt);
                oDB.Disconnect();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToString(_dt.Rows[0]["sSettingsValue"]) != "")
                    {
                        isAllowed = Convert.ToBoolean(_dt.Rows[0]["sSettingsValue"]);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, true);
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null) { _dt.Dispose(); _dt = null; }
                _sqlQuery = null;
            }
            return isAllowed;
        }

        #endregion " Methods & Procedures "
    }

    public class InsuranceSelectedArgs : EventArgs
    {
        Int64 _InsuranceID = 0;
        public Int64 InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }
        Int32 _InsuraceSelfMode = 0;
        public Int32 InsuraceSelfMode
        {
            get { return _InsuraceSelfMode; }
            set { _InsuraceSelfMode = value; }
        }
        Int64 _ContactID = 0;
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }
        string _InsurancePlanName = string.Empty;
        public string SelectedInsurancePlan
        {
            get { return _InsurancePlanName; }
            set { _InsurancePlanName = value; }
        }
        bool _IsSelectedPlanOnHold = false;
        public bool IsSelectedPlanOnHold
        {
            get { return _IsSelectedPlanOnHold; }
            set { _IsSelectedPlanOnHold = value; }
        }
    }

    public class AgeDetail
    {
        public string Age;
        public Int16 Years;
        public Int16 Months;
        public Int16 Days;
        public Int64 Hours;


        public AgeDetail() : base()
        {
        }

        #region New Date Calculation Functionality

        bool _ShowAgeInDays = false;
        Int64 _AgeLimit = 0;

        public TimeSpan GetAgeInHrs(System.DateTime _DateOfBirth, string _BirthTime)
        {
            TimeSpan AgeDiff = new TimeSpan();
            try
            {
                string sDateTime = "";
                System.DateTime Bdate = default(System.DateTime);
                Bdate = _DateOfBirth.Date;
                sDateTime = Bdate.ToShortDateString() + " " + _BirthTime;
                AgeDiff = DateTime.Now.Subtract(Convert.ToDateTime(sDateTime));
                return AgeDiff;
            }
            catch (Exception)
            {
                return AgeDiff;
            }
        }

        public AgeDetail FormatAge_New(DateTime BirthDate)
        {
            bool IsBirthDateLeap = false;
            DateTime Now = System.DateTime.Now;
            int years = Now.Year - BirthDate.Year;
            int months = 0;
            int days = 0;

            if (BirthDate.Day == 29 & BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }

            if (Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);

            if (BirthDate.Year == Now.Year)
            {
                months = Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + Now.Month;
            }

            if (Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);

            days = (Now - BirthDate).Days;

            if (IsBirthDateLeap == true)
            {
                days -= 1;
                if (Now.Day == 29 & Now.Month == 2)
                {
                    days += 1;
                }
                else if (Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 & Now.Year % 4 != 0)
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

            AgeDetail age = new AgeDetail();
            string _AgeStr = "";
            if (_ShowAgeInDays == true & _AgeLimit >= BirthDate.Subtract(Now).Days)// DateDiff(DateInterval.Day, Convert.ToDateTime(_DateOfBirth), System.DateTime.Now.Date))
            {
                if (years == 0)
                {
                    if (months == 0)
                    {
                        if (days <= 1)
                        {
                            _AgeStr = days + " Day";
                        }
                        else
                        {
                            _AgeStr = days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years == 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Month ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Months ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years > 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Days";
                        }
                    }
                }

            }
            else
            {
                if (years == 0)
                {
                    if (months == 1)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = months + " Months";
                    }
                }
                else if (years == 1)
                {
                    if (months == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (months == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                }
                else if (years > 1)
                {
                    if (months == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (months == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month ";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = years + " Years " + months + " Months ";
                    }
                }
                if (years == 0 & months == 0)
                {
                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
            }
            age.Age = _AgeStr;
            age.Years = Convert.ToInt16(years);
            age.Months = Convert.ToInt16(months);
            age.Days = Convert.ToInt16(days);
            return age;
        }

        public string CalculateAge_New(DateTime birthDate,string BirthTime ="")
        {
            string birthtime = "";
            if (BirthTime != "" && BirthTime != " ")
            {
                DateTime _Time = Convert.ToDateTime(BirthTime);
                birthtime = _Time.ToString("H:mm:ss");
            }


            var time = birthDate.ToString("H:mm:ss");
            AgeDetail _Age = new AgeDetail();
            TimeSpan _TimeSpan = new TimeSpan();

            if (BirthTime != "" && BirthTime != " ")
            {
                _TimeSpan = GetAgeInHrs(birthDate, birthtime);
            }
            else
            {
                _TimeSpan = GetAgeInHrs(birthDate, time);
            }

            if ((_TimeSpan != null))
            {
                if (_TimeSpan.TotalDays < 4)
                {
                    _Age.Hours = Convert.ToInt64(_TimeSpan.TotalHours);
                    _Age.Age = _TimeSpan.TotalHours.ToString("0") + " Hours";
                    ///'Hours
                }
                else if (_TimeSpan.TotalDays > 4 & (_TimeSpan.TotalDays <= 28 | _TimeSpan.Hours == 0))
                {
                    _ShowAgeInDays = true;
                    _Age = FormatAge_New(birthDate);
                    ///'days
                }
                else if (_TimeSpan.TotalDays > 28 & _TimeSpan.TotalDays <= 90)
                {

                    double TotalDay = _TimeSpan.Days;
                    double WeekDay = 7;
                    double TotalWeek = TotalDay / WeekDay;
                    TotalWeek = Math.Round(TotalWeek);//Rounding is done to get Output which match to QEMR Weeks Result
                    _Age.Age = TotalWeek.ToString("0") + " Weeks";

                    //_Age.Age = (_TimeSpan.Days / 7).ToString("0") + " Weeks";
                    ///' weeks
                }
                else
                {
                    _Age = FormatAge_New(birthDate);
                    if (_Age.Years < 2 & _Age.Months >= 0)
                    {
                        _Age.Age = (_Age.Years * 12) + _Age.Months + " Months";
                    }
                }

            }
            return _Age.Age;
        }

        #endregion
        //protected override void Finalize()
        //{
        //    base.Finalize();
        //}

    }
}
