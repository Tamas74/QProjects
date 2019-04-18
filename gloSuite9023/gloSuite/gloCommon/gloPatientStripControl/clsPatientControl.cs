using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using gloPatient;
using gloSettings;

namespace gloPatientStripControl
{
    public static class PatientStripControl
    {
        #region " Variable Declarations "

        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region " Methods & Procedures "

        public static Int64 GetPatientID(Int64 ClaimNumber, string SubClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            Int64 _patientID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT nPatientID FROM BL_Transaction_MST WITH (NOLOCK) " +
                            " WHERE nClaimNo = " + ClaimNumber + " and nClinicID = " + AppSettings.ClinicID + " ";

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

        public static DataRow GetPatientDetails(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;

            DataRow _patientData = null;

            try
            {
                _sqlQuery = " SELECT Patient.nPatientID, Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') " +
                            " AS PatientName, Patient.dtDOB AS DOB, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) " +
                            " + ISNULL(Provider_MST.sLastName, '') AS PrName, Patient.nProviderID AS ProviderID, ISNULL(Patient.sPhone, '') AS PatPhone, ISNULL(Patient.sOccupation, '') " +
                            " AS PatientOccupation, ISNULL(Patient.sMobile, '') AS PatientCellPhone, ISNULL(Patient.nSSN, '') AS SSN, ISNULL(Patient.sGender, '') AS Gender, " +
                            " ISNULL(Patient.sMaritalStatus, '') AS sMaritalStatus, ISNULL(Patient.sHandDominance, '') AS HandDominance " +
                            " FROM Patient WITH (NOLOCK) LEFT OUTER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID " +
                            " WHERE Patient.nPatientID = " + PatientID + " AND Patient.nClinicID = " + AppSettings.ClinicID + "";

                oDB.Connect(false);

                DataTable dtPatient = null;
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

        public static DataTable GetInsuranceParties(Int64 ClaimNumber, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable dtInsuranceParties = null;

            try
            {
                //Int64 _patientID = GetPatientID(ClaimNumber, SubClaimNumber);

                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClaimNo", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                oDB.Retrive("BL_SELECT_CLAIM_INSURANCES", oParameters, out dtInsuranceParties);
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

        public static DataTable GetSplittedClaims(Int64 ClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtSplittedClaims = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nClaimNo", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
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
       //         if (_dtSplittedClaims != null) { _dtSplittedClaims.Dispose(); }
            }
            return _dtSplittedClaims;
        }

        public static DataRow GetPatientBalances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataRow _patientBalance = null;
            DataTable _dtPatientBalance = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                oDB.Retrive("BL_GET_PATIENT_ACCOUNT_BALANCE_REVISED", oParameters, out _dtPatientBalance);
                oDB.Disconnect();

                if (_dtPatientBalance != null && _dtPatientBalance.Rows.Count > 0)
                {
                    _patientBalance = _dtPatientBalance.Rows[0];
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
       //         if (_dtPatientBalance != null) { _dtPatientBalance.Dispose(); }
            }
            return _patientBalance;
        }

        public static DataTable GetPatientResereveBalances(Int64 PatientID, out decimal Copay, out decimal Advance, out decimal Others)
        {
       //     System.Collections.SortedList _reserveList = new System.Collections.SortedList();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtPatientBalance = null;

            Copay = 0;
            Advance = 0;
            Others = 0;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
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
             //   if (_dtPatientBalance != null) { _dtPatientBalance.Dispose(); }
            }
            return _dtPatientBalance;
        }

        internal static bool IsInsurancePlanOnHold(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isOnHold = false;

            try
            {
                _sqlQuery = " select bIsHold from BL_Insurance_PlanHold WITH (NOLOCK) where nContactID = " + nContactID + "";

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

        public static bool IsRecordOpen(string nClaimNo, out string MachineName, out Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
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
                    oDB.Connect(false);
                    _MachineId = Environment.MachineName;
                    _sqlQuery = "SELECT ISNULL(nTransactionMasterID,0) AS nTransactionMasterID FROM BL_Transaction_Claim_MST WITH (NOLOCK) " +
                    " WHERE nClaimNo = " + nClaimNo;
                    oDB.Retrive_Query(_sqlQuery, out _dt);
                    oDB.Disconnect();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _nTransactionID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                    }
                    if (_nTransactionID > 0)
                    {
                        //_sqlQuery = "SELECT ISNULL(bIsOpened,'false') AS bIsOpened , ISNULL(sMachineID,'') AS sMachineID,ISNULL(nUserID,0) AS nUserID " +
                        //" FROM BL_Transaction_MST WITH (NOLOCK)" +
                        //" WHERE nTransactionID = " + _nTransactionID + " AND nClinicID = " + AppSettings.ClinicID + " ";
                        //oDB.Retrive_Query(_sqlQuery, out _dt);
                        //oDB.Disconnect();
                        //if (_dt != null && _dt.Rows.Count > 0)
                        //{
                        //    _isRecordOpen = Convert.ToBoolean(_dt.Rows[0]["bIsOpened"]);
                        //    _RecordMachineId = Convert.ToString(_dt.Rows[0]["sMachineID"]);
                        //    _RecordUserId = Convert.ToInt64(_dt.Rows[0]["nUserID"]);
                        //}
                        _sqlQuery = "SELECT ISNULL(bIsLocked,'false') AS bIsOpened , ISNULL(sMachineName,'') AS sMachineID,ISNULL(nUserID,0) AS nUserID " +
                            " FROM BL_Transaction_MST_Locks WITH (NOLOCK) " +
                            " WHERE nTransactionMasterID = " + _nTransactionID;
                        if (_dt != null)
                        {
                            _dt.Dispose();
                            _dt = null;
                        }
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
        #endregion
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
}
