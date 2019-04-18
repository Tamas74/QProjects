using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace gloBilling.Statement
{
    //Class Created by Roopali  4:29 PM 9/6/2010 
    //

    public class gloStatment : IDisposable
    {

        #region " Private variables "

        private string _databaseconnectionstring = "";
        private bool disposed = false;
        private Int64 _UserID = 0;
        public string _UserName = "";
        private Int64 _ClinicID = 0;
        private string _sqlQuery = string.Empty;
        private bool _isBusinessCenterEnable = false;
        private Int64 _nBusinessCenterID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public bool IsBusinessCenterEnable
        {
            get { return _isBusinessCenterEnable; }
            set { _isBusinessCenterEnable = value; }
        }
        public Int64 BusinessCenterID
        {
            get { return _nBusinessCenterID; }
            set { _nBusinessCenterID = value; }
        }
        #endregion " Private variables "

        #region " Constructor & Destructor "

        public gloStatment()
        {

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

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);


            #endregion

            #region " Retrive ClinicID from appSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

        }

        ~gloStatment()
        {
            Dispose(false);
        }


        #endregion " Constructor & Destructor "

        #region IDisposable Members

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

                }
            }
            disposed = true;
        }
        #endregion

        #region Void Statment and statment Batch
        public void VoidBatch(Int64 _MasterId, String _VoidNotes)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);

            //Update master table
            _sqlQuery = "";
            _sqlQuery = "update BL_Batch_PatientStatement_Mst WITH (READPAST) set bIsVoid='true',dtVoidDate=dbo.gloGetDate(),nVoidUserId=" + _UserID + ",sVoidUserName='" + _UserName + "' where nBatchPateintStatMstID=" + _MasterId;
            try
            {
                oDB.Execute_Query(_sqlQuery);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            //update detail table
            _sqlQuery = "";
            _sqlQuery = "update BL_Batch_PatientStatement_DTL WITH (READPAST) set bIsVoid='true',dtVoidDate=dbo.gloGetDate(),nVoidUserId=" + _UserID + ",sVoidUserName='" + _UserName + "',sVoidNotes='" + _VoidNotes.Trim().Replace("'", "''") + "' where nBatchPateintStatMstID=" + _MasterId + " And  isnull(bIsVoid,0)=0 ";
            try
            {
                oDB.Execute_Query(_sqlQuery);
            }
            catch (Exception ex)
            {
                //oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            //throw new NotImplementedException();
        }

        public void VoidSingleStatment(Int64 _MasterId, Int64 _DetailId, String _VoidNotes)
        {
            //update detail table
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            _sqlQuery = "";
            _sqlQuery = "update BL_Batch_PatientStatement_DTL WITH (READPAST) set bIsVoid='true',dtVoidDate=dbo.gloGetDate(),nVoidUserId=" + _UserID + ",sVoidUserName='" + _UserName + "',sVoidNotes='" + _VoidNotes.Trim().Replace("'", "''").ToString() + "' where nBatchPateintStatMstID=" + _MasterId + " AND nBatchPateintStatDtlID=" + _DetailId + " And  isnull(bIsVoid,0)=0 "; ;
            try
            {
                oDB.Execute_Query(_sqlQuery);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            //if this is last stament of batch to be voied then mark batch master also as voided.
            CheckAllStatmentsVoidFromBatch(_MasterId);

        }

        private void CheckAllStatmentsVoidFromBatch(Int64 _MasterId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            _sqlQuery = "";
            _sqlQuery = "select count(*) from BL_Batch_PatientStatement_DTL WITH (NOLOCK) where isnull(bIsVoid,'false')=lower('false') and nBatchPateintStatMstID=" + _MasterId;

            object _result = oDB.ExecuteScalar_Query(_sqlQuery);
            if (_result != null && Convert.ToString(_result).Trim() != "")
            {
                if (Convert.ToInt64(_result) == 0)
                {
                    _sqlQuery = "";
                    _sqlQuery = "update BL_Batch_PatientStatement_Mst WITH (READPAST) set bIsVoid='true',dtVoidDate=dbo.gloGetDate(),nVoidUserId=" + _UserID + ",sVoidUserName='" + _UserName + "' where nBatchPateintStatMstID=" + _MasterId;
                    try
                    {
                        oDB.Execute_Query(_sqlQuery);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                }
            }

            oDB.Disconnect();
            oDB.Dispose();
            //throw new NotImplementedException();
        }

        #endregion

        public string GetServerPath()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = new object();
            string _sqlQuery = "";
            string _isValidPath = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value)
                {
                    _isValidPath = Convert.ToString(retVal);
                    try
                    {
                        if (System.IO.Directory.Exists(_isValidPath) == false)
                        { _isValidPath = ""; }
                    }
                    catch (Exception ex)
                    {
                        _isValidPath = "";
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    }
                }
                else
                { _isValidPath = ""; }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (retVal != null) { retVal = null; }
            }
            return _isValidPath;
        }

        #region "Main Statement"

        private void fetchRevisedRemitDetails(Int64 CriteriaID, dsRevisedPatientStatement _dsPatientStatementInfo, Int32 _StatementCount, Int64 PatientID, Int64 PAccountID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {

                if (PatientID == 0)
                {
                    string _sqlQuery = string.Empty;
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    _sqlQuery = "select nPatientID  from PA_Accounts_Patients where nPAccountID = " + PAccountID + " and bIsOwnAccount =1 ";
                    object _result = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_result != null && Convert.ToString(_result) != "")
                        PatientID = Convert.ToInt64(_result);

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }

                }

                oConnection.ConnectionString = _databaseconnectionstring;

                //_sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sRemitName, ISNULL(sRemitAddress1,'') as sRemitAddress1," +
                //                          " ISNULL(sRemitAddress2,'') as sRemitAddress2," +
                //                          " ISNULL(sRemitCity,'') as sRemitCity ," +
                //                          " ISNULL(sRemitState,'') as sRemitState ," +
                //                          " ISNULL(sRemitZip,'') as sRemitZip," +
                //                          " case when " + _StatementCount + " =0 then ISNULL(sClinicMessage1,'') when " + _StatementCount + " =1 then ISNULL(sStatementClinicMsg1,'') " +
                //                          " when " + _StatementCount + " =2 then ISNULL(sStatementClinicMsg2,'') " +
                //                          " when " + _StatementCount + " =3 then ISNULL(sStatementClinicMsg3,'') " +
                //                          " when " + _StatementCount + " =4 then ISNULL(sStatementClinicMsg4,'') " +
                //                          " when " + _StatementCount + " =5 then ISNULL(sStatementClinicMsg5,'') " +
                //                          " end as sClinicMessage1 " +
                //                          " from RPT_PatStatementCriteria_Display WITH (NOLOCK) where nStatementCriteriaID =" + CriteriaID + "";
                _sqlcommand.CommandText = "EXECUTE BL_Statement_RemitInfo " + CriteriaID + "," + _StatementCount + "," + PatientID;
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatementInfo, "dt_RemitInfo");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }

                if (da != null) { da.Dispose(); }
            }
        }

        private void fetchPayToDetails(Int64 CriteriaID, Int64 PatientID, dsRevisedPatientStatement _dsPatientStatementInfo, Int64 PAccountID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            Int64 nPayableTo = 0;
            Int64 nRemitTo = 0;
            DataTable dtTemp = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {

                _sqlQuery = "SELECT ISNULL(nPayableTo,0) as nPayableTo,ISNULL(nRemitTo,0) as nRemitTo  from RPT_PatStatementCriteria_MST WITH (NOLOCK) where nStatementCriteriaID =" + CriteriaID + "";
                oDB.Connect(false);

                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    nPayableTo = Convert.ToInt64(dtTemp.Rows[0]["nPayableTo"]);
                    nRemitTo = Convert.ToInt64(dtTemp.Rows[0]["nRemitTo"]);
                }

                oConnection.ConnectionString = _databaseconnectionstring;

                if (PatientID == 0)
                {
                    _sqlQuery = string.Empty;
                    _sqlQuery = "select nPatientID  from PA_Accounts_Patients where nPAccountID = " + PAccountID + " and bIsOwnAccount =1 ";
                    object _result = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_result != null && Convert.ToString(_result) != "")
                        PatientID = Convert.ToInt64(_result);
                }


                //Payer to as other address.
                if (nPayableTo == 2)
                {
                    _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sPayToName, ISNULL(sRemitAddress1,'') as sPayToAddress1," +
                                                " ISNULL(sRemitAddress2,'') as sPayToAddress2," +
                                                " ISNULL(sRemitCity,'') as sPayToCity ," +
                                                " ISNULL(sRemitState,'') as sPayToState ," +
                                                " ISNULL(sRemitZip,'') as sPayToZip " +
                                                " from RPT_PatStatementCriteria_Display WITH (NOLOCK) where nStatementCriteriaID =" + CriteriaID + " AND nAddressType = " + nPayableTo + "";

                }
                //Payer to as remit address.
                else if (nPayableTo == 1)
                {
                    //when Pyer to is selected as Remit to and remet to is other address
                    if (nRemitTo == (Int32)RemitAddressType.OtherAddress)
                        _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sPayToName, ISNULL(sRemitAddress1,'') as sPayToAddress1," +
                                                  " ISNULL(sRemitAddress2,'') as sPayToAddress2," +
                                                  " ISNULL(sRemitCity,'') as sPayToCity ," +
                                                  " ISNULL(sRemitState,'') as sPayToState ," +
                                                  " ISNULL(sRemitZip,'') as sPayToZip " +
                                                  " from RPT_PatStatementCriteria_Display WITH (NOLOCK) where nStatementCriteriaID =" + CriteriaID + " AND nAddressType = " + nPayableTo + "";
                    else
                        //when Pyer to is selected as Remit to and remet to is provider address
                        _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                      " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                      " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                      " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                      " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sPayToName, " +
                                      " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sPayToAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sPayToAddress2," +
                                      " ISNULL(Provider_MST.sPracticeCity, '''') AS sPayToCity,ISNULL(Provider_MST.sPracticeState, '''') AS sPayToState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sPayToZip, " +
                                      " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                      " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                      " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                      " FROM Patient WITH (NOLOCK) INNER JOIN " +
                                      " Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                      " Clinic_MST WITH (NOLOCK) ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                      " WHERE Patient.nPatientID= " + PatientID;
                }
                //when Pyer to as provider address
                else if (nPayableTo == 0)
                {

                    _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                      " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                      " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                      " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                      " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sPayToName, " +
                                      " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sPayToAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sPayToAddress2," +
                                      " ISNULL(Provider_MST.sPracticeCity, '''') AS sPayToCity,ISNULL(Provider_MST.sPracticeState, '''') AS sPayToState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sPayToZip, " +
                                      " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                      " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                      " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                      " FROM Patient WITH (NOLOCK) INNER JOIN " +
                                      " Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                      " Clinic_MST WITH (NOLOCK) ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                      " WHERE Patient.nPatientID= " + PatientID;

                }
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatementInfo, "dt_PayTo");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
                if (dtTemp != null) { dtTemp.Dispose(); }
            }
        }

        //Code Changed by SaiKrishna on 04-12-2011(mm-dd-yyyy)._nPAccountID parameter added and Removed PatientID.
        private void fetchRevisedDisplaySettings(Int64 _nPAccountID, Int64 _nPatientID, dsRevisedPatientStatement _dsPatientStatementInfo, Int64 _CriteriaId)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;

                _sqlcommand.CommandText = "PA_GET_Patient_DisplaySettings";

                //SqlParameter ParaPatient = new SqlParameter();
                //{
                //    ParaPatient.ParameterName = "@nPatientID";
                //    ParaPatient.Value = PatientID;
                //    ParaPatient.Direction = ParameterDirection.Input;
                //    ParaPatient.SqlDbType = SqlDbType.BigInt;
                //}
                //_sqlcommand.Parameters.Add(ParaPatient);

                //Code addded by SaiKrishna on 04-12-2011(mm-dd-yyyy).
                SqlParameter ParaAccount = new SqlParameter();
                {
                    ParaAccount.ParameterName = "@nPAccountID";
                    ParaAccount.Value = _nPAccountID;
                    ParaAccount.Direction = ParameterDirection.Input;
                    ParaAccount.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaAccount);

                //added by mahesh s on 20/may/2011 for display patientname/guarntorname based on condition on report.
                SqlParameter ParaPatient = new SqlParameter();
                {
                    ParaPatient.ParameterName = "@nPatientID";
                    ParaPatient.Value = _nPatientID;
                    ParaPatient.Direction = ParameterDirection.Input;
                    ParaPatient.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaPatient);

                SqlParameter ParaCriteria = new SqlParameter();
                {
                    ParaCriteria.ParameterName = "@nCriteriaId";
                    ParaCriteria.Value = _CriteriaId;
                    ParaCriteria.Direction = ParameterDirection.Input;
                    ParaCriteria.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaCriteria);

                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                oConnection.Open();
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatementInfo, "dt_DisplaySettings");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { if (_sqlcommand.Parameters != null) { _sqlcommand.Parameters.Clear(); } _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
            }
        }


        public int GetStatementCount(Int64 AccountID, Int32 Statementdate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            object statementcount = null;
            int iCount = 0;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select dbo.PA_Get_StatementCount_V2(" + AccountID + ",'" + gloDateMaster.gloDate.DateAsDate(Statementdate) + "')";
                statementcount = oDB.ExecuteScalar_Query(_sqlQuery);
                iCount = Convert.ToInt32(statementcount);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (statementcount != null) { statementcount = null; }
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
            }

            return iCount;
        }

        public void fillRevisedPatientStatement(Int64 PatientID, Int64 _nPAccountID, dsRevisedPatientStatement _dsPatientStatementInfo, int Statementdate)
        {
            try
            {
                Int64 _CriteriaId = 0;
                if (IsBusinessCenterEnable && BusinessCenterID > 0)
                { _CriteriaId = GetBusinessCenterDisplaySettings(BusinessCenterID); }
                else { _CriteriaId = GetStatmentCriteriaID(); }

                int _StatementCount = 0;
                _StatementCount = GetStatementCount(_nPAccountID, Statementdate);
                fetchRevisedRemitDetails(_CriteriaId, _dsPatientStatementInfo, _StatementCount, PatientID, _nPAccountID);
                fetchPayToDetails(_CriteriaId, PatientID, _dsPatientStatementInfo, _nPAccountID);
                fetchRevisedDisplaySettings(_nPAccountID, PatientID, _dsPatientStatementInfo, _CriteriaId);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }

        }

        public string GetStatmentNotes(int StatmentEndDate, Int64 PatientId, Int64 AccountID)
        {
            string _sStatementNotes = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            oDB.Connect(false);
            try
            {
                if (PatientId == 0)
                {

                    ClsgloElectronic oElectronic = new ClsgloElectronic(_databaseconnectionstring);
                    PatientId = oElectronic.GetAccountOwnerID(AccountID);

                }

                _sqlQuery = "SELECT sStatementNote FROM Patient_Statement_Notes WITH (NOLOCK) WHERE nPatientID = " + PatientId + " AND nClinicID = " + _ClinicID + " AND ( nfromdate <= " + StatmentEndDate + " AND nToDate >= " + StatmentEndDate + ")";

                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        _sStatementNotes = _sStatementNotes + Convert.ToString(dtTemp.Rows[i]["sStatementNote"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                dtTemp.Dispose();
            }
            return _sStatementNotes;

        }

        //Code Changed by SaiKrishna on 04-12-2011(mm-dd-yyyy).
        //ExcludeFromStatement is related to account, paramerter is changed from _nPatientID to _nPAccountID
        public bool ExcludeFromStatment(Int64 _nPAccountID)
        {
            #region "Exclude from Statement"

            bool _isExclude = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            oDB.Connect(false);
            try
            {
                //_sqlQuery = " SELECT nPatientID FROM PatientSettings WITH (NOLOCK) WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";

                _sqlQuery = "Select nPAccountID From PA_Accounts WITH (NOLOCK) where bIsExcludeStatement = 1 and nPAccountID = " + _nPAccountID;
                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp.Rows.Count > 0)
                    _isExclude = true;
                else
                    _isExclude = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                dtTemp.Dispose();
            }

            return _isExclude;
            #endregion "Exclude from Statement"
        }

        public bool IsBusinessCenterAssociated(Int64 _nPAccountID)
        {
            #region "Validation on generating individual statement if no Business Center Associated"

            bool _isBusinessCenterAssociated = false;
            object _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                _sqlQuery = "SELECT CASE ISNULL(nBusinessCenterID, 0) " +
                            "  WHEN 0 THEN 'False' " +
                            "  ELSE 'True' " +
                            " END AS IsBusinessCenterAssociated FROM PA_Accounts WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID;

                _result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    _isBusinessCenterAssociated = Convert.ToBoolean(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _isBusinessCenterAssociated;

            #endregion "Validation on generating individual statement if no Business Center Associated"
        }

        public bool ExcludeFromStatment_Patient(Int64 _nPatientID)
        {
            #region "Exclude from Statement"

            bool _isExclude = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            oDB.Connect(false);
            try
            {
                //_sqlQuery = " SELECT nPatientID FROM PatientSettings WITH (NOLOCK) WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";

                _sqlQuery = "select * from PatientSettings where sName ='Exclude from Statement' and nPatientID = " + _nPatientID + " and sValue ='1'";
                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp.Rows.Count > 0)
                    _isExclude = true;
                else
                    _isExclude = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                dtTemp.Dispose();
            }

            return _isExclude;
            #endregion "Exclude from Statement"
        }

        public Int64 DeleteBatch(Int64 nBatchId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _result = 0;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nBatchMstId", nBatchId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDB.Execute("DELETE_Blank_PatientStatementBatch_Mst", oDBParameters, out _result);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

                if (oDB != null) { oDBParameters.Dispose(); }

            }
            if (_result != null && Convert.ToString(_result) != "")
                return Convert.ToInt64(_result);
            else
                return nBatchId;
        }

        public Int64 GetStatmentCriteriaID()
        {
            Int64 nStatementCriteriaID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            oDB.Connect(false);
            try
            {
                _sqlQuery = " SELECT nStatementCriteriaID FROM RPT_Patstatementcriteria_MST WITH (NOLOCK) WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                oDB.Retrive_Query(_sqlQuery, out dtTemp);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                    nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                else
                    nStatementCriteriaID = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
            }

            return nStatementCriteriaID;
        }

        public Int64 GetBusinessCenterDisplaySettings(Int64 BusinessCenterID)
        {
            Int64 nStatementDisplaySettingsID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            DataTable dtTemp = new DataTable();

            oDB.Connect(false);
            try
            {
                _sqlQuery = " SELECT TOP 1 ISNULL(nStatementCriteriaID,0) AS nStatementCriteriaID " +
                            " FROM    RPT_Patstatementcriteria_MST WITH ( NOLOCK ) " +
                            "        INNER JOIN dbo.BL_BusinessCenterCodes WITH ( NOLOCK ) " +
                            "        ON nStatementDisplaySettingsID = nStatementCriteriaID " +
                            " WHERE   criteriaType = 'DISPLAY' " +
                            " AND nBusinessCenterID = " + BusinessCenterID;

                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                    nStatementDisplaySettingsID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                else
                    nStatementDisplaySettingsID = GetStatmentCriteriaID();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
            }

            return nStatementDisplaySettingsID;
        }

        public Int32 getCopyEDIFiles()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            DataTable dtversion = new DataTable();
            dtversion = oSetting.GetSetting("COPY_EDI_FILES", 0);
            if (dtversion != null && dtversion.Rows.Count > 0)
            {
                return Convert.ToInt32(dtversion.Rows[0]["sSettingsValue"]);
            }
            else
            {
                return 1;
            }
        }


        public Int64 GetAccountOwnerID(Int64 nAccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            object nID = null;
            Int64 nAccountOwmerID = 0;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT TOP(1) nPatientID FROM PA_Accounts_Patients WHERE nPAccountID = " + nAccountID + " AND bIsOwnAccount = 1 ";
                nID = oDB.ExecuteScalar_Query(_sqlQuery);
                if (nID != null && Convert.ToString(nID) != "")
                {
                    nAccountOwmerID = Convert.ToInt64(nID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
                if (nID != null) { nID = null; }
            }

            return nAccountOwmerID;

        }


        public string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WITH (NOLOCK) WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                if (dtPatient.Rows.Count > 0)
                {
                    _result = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _result;

        }

        public System.Data.DataTable FillDetails(string cmbSettings)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                sqlQuery = "select sUserName,dtCreateDate ,dtStatementDate,sSettingName from BL_Batch_PatientStatement_MST WITH (NOLOCK) where sBatchName like '" + cmbSettings.Replace("'", "''") + "%' AND ISNULL(bIsVoid,0) = 0 order by  dtCreateDate desc ";
                ODB.Retrive_Query(sqlQuery, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
            }
            return dt;
        }

        //Code added by SaiKrishna on 04-15-2011(mm-dd-yyyy). _nPAccountID,_nPatientID Parameters added.
        public System.Data.DataTable FillIndividualDetails(string cmbPatients, Int64 _nPAccountID, Int64 _nPatientID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                //sqlQuery = "select sUserName, dtCreateDate,dtStatementDate from BL_Batch_PatientStatement_Mst WITH (NOLOCK) where sBatchName like '" + cmbPatients + "%' order by dtcreateDate desc";
                sqlQuery = "Select sUserName, dtCreateDate,dtStatementDate from BL_Batch_PatientStatement_Mst where nBatchPateintStatMstID in (Select nBatchPateintStatMstID from  BL_Batch_PatientStatement_DTL" +
                           " Where nPAccountID =" + _nPAccountID + " and nPatientID=" + _nPatientID + ")and sBatchName like '" + cmbPatients + "%' order by dtcreateDate desc";
                ODB.Retrive_Query(sqlQuery, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
            return dt;
        }

        //Code added by SaiKrishna on 04-12-2011(mm-dd-yyyy). _nPAccountID Parameter added and proc name changed. 
        public Decimal GetIndividualPatientBalance(Int64 _nPatientID, Int64 _ClinicID, int _CloseDate, Int64 _nPAccountID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
            Decimal _TotalPatientDue = 0;
            DataTable dtTotalPatientDue = new DataTable();
            try
            {

                ODB.Connect(false);
                oDBPatameters.Add("@nPatientId", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(_CloseDate), ParameterDirection.Input, SqlDbType.DateTime);
                oDBPatameters.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                ODB.Retrive("PA_BL_GET_PATIENT_BALANCE_V2", oDBPatameters, out dtTotalPatientDue);
                if (dtTotalPatientDue != null && dtTotalPatientDue.Rows.Count > 0)
                {
                    _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["PatientDue"].ToString()) - Convert.ToDecimal(dtTotalPatientDue.Rows[0]["AvailableReserve"].ToString());
                }
                else
                {
                    _TotalPatientDue = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (oDBPatameters != null) { oDBPatameters.Dispose(); }
                if (dtTotalPatientDue != null) { dtTotalPatientDue.Dispose(); }
            }
            return _TotalPatientDue;
        }

        public void CreateReport(Int64 PatientID, Int64 _nPAccountID, int EndDate, ref dsRevisedPatientStatement _dsPatientStatementInfo)
        {
            SqlConnection oConnection = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlParameter ParAccount;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dsLocal = new DataSet();
            try
            {
                Int64 _CriteriaId = 0;
                if (IsBusinessCenterEnable && BusinessCenterID > 0)
                { _CriteriaId = GetBusinessCenterDisplaySettings(BusinessCenterID); }
                else { _CriteriaId = GetStatmentCriteriaID(); }

                oConnection.ConnectionString = _databaseconnectionstring;

                #region "Fetch Ageing Bucket Info and patient account balance"

                oDBParameters.Clear();
                if (PatientID == 0)
                {
                    oDBParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                }
                oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(EndDate), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("PA_BL_SELECT_AgingBuckets_V2", oDBParameters, out _dsLocal);


                if (_dsLocal.Tables.Count >= 1)
                {
                    _dsPatientStatementInfo.Tables["dt_AgeingBucket"].Merge(_dsLocal.Tables[0]);
                }
                if (_dsLocal.Tables.Count >= 2)
                {
                    _dsPatientStatementInfo.Tables["dt_PatientReserve"].Merge(_dsLocal.Tables[1]);
                }

                #endregion "Fetch Ageing Bucket Info and patient account balance"

                #region "Fetch Patient Statement Info"

                //Fill the dt_PatientCharges_payment table in dataset present in gloReports using store procedure 

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "PA_RPT_PatientStatement_Revised_V2";
                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@nPatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@dtDate";
                    ParaEndDate.Value = gloDateMaster.gloDate.DateAsDate(EndDate);
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.DateTime;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                //Code added by SaiKrishna on 04-12-2011(mm-dd-yyyy)
                ParAccount = new SqlParameter();
                {
                    ParAccount.ParameterName = "@nPAccountID";
                    ParAccount.Value = _nPAccountID;
                    ParAccount.Direction = ParameterDirection.Input;
                    ParAccount.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParAccount);

                SqlParameter ParCriteriaId = new SqlParameter();
                {
                    ParCriteriaId.ParameterName = "@nCriteriaId";
                    ParCriteriaId.Value = _CriteriaId;
                    ParCriteriaId.Direction = ParameterDirection.Input;
                    ParCriteriaId.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParCriteriaId);

                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(_dsPatientStatementInfo, "dt_PatientStatement_Revised");

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (sqlCmd != null) { if (sqlCmd.Parameters != null) { sqlCmd.Parameters.Clear(); } sqlCmd.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        public string getCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays WITH (NOLOCK) ");
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        public bool IsDayClosed(Int64 dtpCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isDayClosed = false;
            object _value = null;
            try
            {
                oDB.Connect(false);
                _value = oDB.ExecuteScalar_Query("SELECT COUNT(nCloseDayDate) As CloseDate from BL_CloseDays WITH (NOLOCK) WHERE nCloseDayDate = " + dtpCloseDate);

                if (_value != null && Convert.ToString(_value).Trim() != "")
                {
                    if (Convert.ToInt64(_value) > 0)
                    {
                        _isDayClosed = true;
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _isDayClosed;
        }

        public string getTransactionCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT dbo.Convert_to_date(max(nTransactionDate - 1)) As CloseDate from dbo.BL_Transaction_Claim_MST WITH (NOLOCK) WHERE ISNULL(bIsVoid,0) = 0");
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        public bool IsGenerateStatementOnUnclosedDay()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _result = null;
            bool _IsSettingPresent = false;
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = 'UNCLOSEDDAYSTATEMENT'";
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    _IsSettingPresent = Convert.ToBoolean(_result);
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
            return _IsSettingPresent;
        }

        //Code added by SaiKrishna on 04-12-2011(mm-dd-yyyy) for fill patientaccounts
        public System.Data.DataTable FillAccounts(Int64 _nPatientID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            ODB.Connect(false);
            DataTable dt = new DataTable();

            try
            {
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                ODB.Retrive("PA_Select_PatientsAccounts", oParameters, out dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
            return dt;
        }

        public System.Data.DataTable FillAccountsForBCFeatureDisabled(Int64 _nPatientID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            ODB.Connect(false);
            DataTable dt = new DataTable();

            try
            {
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                ODB.Retrive("PA_Select_PatientsAccountsForStatement", oParameters, out dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
            return dt;
        }

        //Code added by SaiKrishna on 04-12-2011(mm-dd-yyyy) for fill accountpatients
        public System.Data.DataTable FillPatients(Int64 _nPAccountID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                oParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                ODB.Retrive("PA_Select_PatientsAccounts", oParameters, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
            return dt;

        }

        //7022Items:.STA extension for GatewayEDI statements
        //Function is added to get statement file extension for default clearing house set in gloPMAdmin.
        public string getStatementFileExtension()
        {
            string _sFileExtent = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _sqlQuery = "SELECT sStatementFileExtension FROM dbo.BL_ClearingHouse_MST WHERE bIsDefault= 1 AND nClinicID= " + gloGlobal.gloPMGlobal.ClinicID;
                oDB.Connect(false);
                _sFileExtent = oDB.ExecuteScalar_Query(_sqlQuery).ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _sFileExtent;
        }

        #endregion "Main Statement"

        //Bug #68473: CR00000352 : RCM Queue issue
        // function added to get statement count
        public Int64 GetStatementCount(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            int cntsttmnt = 0;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT isnull(nStatementcount,0) from CL_STATEMENTCOUNT WHERE nPAccountID =" + PAccountID;
                object result = oDB.ExecuteScalar_Query(strQuery);
                if (result != null)
                {
                    int.TryParse(result.ToString(), out cntsttmnt);
                    //cntsttmnt = Convert.ToInt64(result);
                }

                return cntsttmnt;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                //intensionally kept -1
                cntsttmnt = -1;
                return cntsttmnt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //intensionally kept -1
                cntsttmnt = -1;
                return cntsttmnt;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        //Bug #68473: CR00000352 : RCM Queue issue
        // function added to Check Is Payment Plan Present.
        public Boolean GetIsPaymentPlan(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT dPlanAmount FROM CL_PaymentPlan WHERE nAccountID = " + PAccountID;
                object result = oDB.ExecuteScalar_Query(strQuery);
                double dPayAmt;
                if (result != null)
                {
                    if (double.TryParse(result.ToString(), out dPayAmt))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return true;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool ResetStatementCount(Int64 _nPAccountID, DateTime _dtCloseDate, bool _isManual)
        {
            bool _result = false;
            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intResult = 0;
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtCloseDate", _dtCloseDate.Date, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@IsManual", _isManual, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Execute("gsp_SetReset_Patient_Statement_Count", oParameters, out _intResult);

                if (Convert.ToInt64(_intResult) > 0)
                { _result = true; }


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _result;
        }

        public bool ResetStatementCountAfterStatementSend(Int64 _nPAccountID, DateTime _dtCloseDate, bool _isManual)
        {
            bool _result = false;
            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intResult = 0;
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtCloseDate", _dtCloseDate.Date, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@IsManual", _isManual, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Execute("gsp_INUP_Patient_Statement_Count_AfterSendStatement", oParameters, out _intResult);

                if (Convert.ToInt64(_intResult) > 0)
                { _result = true; }


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _result;
        }

        public DataTable GetBatchAccountsAndPatients(Int64 nMasterID)
        {
            DataTable oDataTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";

            try
            {
                _sqlQuery = "SELECT nPatientID,nPAccountID FROM BL_Batch_PatientStatement_DTL WHERE nBatchPateintStatMstID = " + nMasterID;
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out oDataTable);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
            }
            return oDataTable;
        }

        #region "Statement Revised functions"

        public void FetchStatementDetails(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID, int StatementDate, ref dsRevisedPatientStatement dsMain)
        {
            SqlConnection oConnection = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlParameter ParAccount;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataSet _dsLocal = new DataSet();
            DataSet _dsAgeing = new DataSet();
            DataSet _dsDispSetting = new DataSet();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;

                #region "Fetch Patient Statement Info"

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "PA_RPT_PatientStatement_Revised_V2";
                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@nPatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@dtDate";
                    ParaEndDate.Value = gloDateMaster.gloDate.DateAsDate(StatementDate);
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.DateTime;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                ParAccount = new SqlParameter();
                {
                    ParAccount.ParameterName = "@nPAccountID";
                    ParAccount.Value = AccountID;
                    ParAccount.Direction = ParameterDirection.Input;
                    ParAccount.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParAccount);
                SqlParameter ParCriteriaId = new SqlParameter();
                {
                    ParCriteriaId.ParameterName = "@nCriteriaId";
                    ParCriteriaId.Value = StatementCriteriaID;
                    ParCriteriaId.Direction = ParameterDirection.Input;
                    ParCriteriaId.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParCriteriaId);

                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;

                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsMain, "dt_PatientStatement_Revised");

                #endregion

                #region "Fetch Ageing Bucket Info and patient account balance"

                oDBParameters.Clear();

                if (PatientID == 0)
                { oDBParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt); }

                oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(StatementDate), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("PA_BL_SELECT_AgingBuckets_V2", oDBParameters, out _dsAgeing);

                if (_dsAgeing.Tables.Count >= 1)
                { dsMain.Tables["dt_AgeingBucket"].Merge(_dsAgeing.Tables[0]); }
                if (_dsAgeing.Tables.Count >= 2)
                { dsMain.Tables["dt_PatientReserve"].Merge(_dsAgeing.Tables[1]); }

                #endregion "Fetch Ageing Bucket Info and patient account balance"

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (sqlCmd != null) { if (sqlCmd.Parameters != null) { sqlCmd.Parameters.Clear(); } sqlCmd.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters = null; }
                if (_dsAgeing != null) { _dsAgeing.Dispose(); _dsAgeing = null; }
            }
        }

        // TODO : Move all SP's to a SP & Dataset to Datatable
        public DataSet GetPayToInfo_old(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            Int64 nPayableTo = 0;
            Int64 nRemitTo = 0;

            DataSet dsPayToInfo = new DataSet();

            DataTable dtTemp = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                _sqlQuery = "SELECT ISNULL(nPayableTo,0) as nPayableTo,ISNULL(nRemitTo,0) as nRemitTo  from RPT_PatStatementCriteria_MST WITH (NOLOCK) where nStatementCriteriaID =" + StatementCriteriaID + "";
                oDB.Connect(false);

                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    nPayableTo = Convert.ToInt64(dtTemp.Rows[0]["nPayableTo"]);
                    nRemitTo = Convert.ToInt64(dtTemp.Rows[0]["nRemitTo"]);
                }

                oConnection.ConnectionString = _databaseconnectionstring;

                if (PatientID == 0)
                {
                    _sqlQuery = string.Empty;
                    _sqlQuery = "select nPatientID  from PA_Accounts_Patients where nPAccountID = " + AccountID + " and bIsOwnAccount =1 ";
                    object _result = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_result != null && Convert.ToString(_result) != "")
                        PatientID = Convert.ToInt64(_result);
                }

                //Payer to as other address.
                if (nPayableTo == 2)
                {
                    _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sPayToName, ISNULL(sRemitAddress1,'') as sPayToAddress1," +
                                                " ISNULL(sRemitAddress2,'') as sPayToAddress2," +
                                                " ISNULL(sRemitCity,'') as sPayToCity ," +
                                                " ISNULL(sRemitState,'') as sPayToState ," +
                                                " ISNULL(sRemitZip,'') as sPayToZip " +
                                                " from RPT_PatStatementCriteria_Display WITH (NOLOCK) where nStatementCriteriaID =" + StatementCriteriaID + " AND nAddressType = " + nPayableTo + "";

                }

                //Payer to as remit address.
                else if (nPayableTo == 1)
                {
                    //when Pyer to is selected as Remit to and remet to is other address
                    if (nRemitTo == (Int32)RemitAddressType.OtherAddress)
                        _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sPayToName, ISNULL(sRemitAddress1,'') as sPayToAddress1," +
                                                  " ISNULL(sRemitAddress2,'') as sPayToAddress2," +
                                                  " ISNULL(sRemitCity,'') as sPayToCity ," +
                                                  " ISNULL(sRemitState,'') as sPayToState ," +
                                                  " ISNULL(sRemitZip,'') as sPayToZip " +
                                                  " from RPT_PatStatementCriteria_Display WITH (NOLOCK) where nStatementCriteriaID =" + StatementCriteriaID + " AND nAddressType = " + nPayableTo + "";
                    else
                        //when Pyer to is selected as Remit to and remet to is provider address
                        _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                      " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                      " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                      " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                      " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sPayToName, " +
                                      " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sPayToAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sPayToAddress2," +
                                      " ISNULL(Provider_MST.sPracticeCity, '''') AS sPayToCity,ISNULL(Provider_MST.sPracticeState, '''') AS sPayToState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sPayToZip, " +
                                      " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                      " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                      " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                      " FROM Patient WITH (NOLOCK) INNER JOIN " +
                                      " Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                      " Clinic_MST WITH (NOLOCK) ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                      " WHERE Patient.nPatientID= " + PatientID;
                }
                //when Pyer to as provider address
                else if (nPayableTo == 0)
                {

                    _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                      " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                      " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                      " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                      " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sPayToName, " +
                                      " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sPayToAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sPayToAddress2," +
                                      " ISNULL(Provider_MST.sPracticeCity, '''') AS sPayToCity,ISNULL(Provider_MST.sPracticeState, '''') AS sPayToState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sPayToZip, " +
                                      " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                      " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                      " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                      " FROM Patient WITH (NOLOCK) INNER JOIN " +
                                      " Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                      " Clinic_MST WITH (NOLOCK) ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                      " WHERE Patient.nPatientID= " + PatientID;

                }
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsPayToInfo, "dt_PayTo");

                return dsPayToInfo;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
                if (dtTemp != null) { dtTemp.Dispose(); }
            }
        }

        public DataTable GetPayToInfo(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtPayToInfo;

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCriteriaID", StatementCriteriaID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("PA_GET_PayToInfo", oDBParameters, out dtPayToInfo);
                oDB.Disconnect();

                return dtPayToInfo;
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
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }


        // TODO : Need to re-verify
        public DataTable GetRemitDetails(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID, int StatementCount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtRemitInfo = new DataTable();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nCriteriaID", StatementCriteriaID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nStatementCount", StatementCount, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicId", 1, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_Statement_RemitInfo_Ret", oDBParameters, out dtRemitInfo);
                oDB.Disconnect();

                return dtRemitInfo;
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
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }

        // TODO : Need to re-verify
        public DataSet GetGuarantorInfo(Int64 AccountID, int StatementDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataSet dsGuaratorInfo = new DataSet();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicId", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", StatementDate, ParameterDirection.Input, SqlDbType.Int);

                oDB.Retrive("Get_Electronicfileinfo", oDBParameters, out dsGuaratorInfo);
                oDB.Disconnect();

                return dsGuaratorInfo;
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
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }

        public DataSet GetPatientInfo(Int64 PatientID, int StatementDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataSet dsPatientInfo = new DataSet();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicId", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", StatementDate, ParameterDirection.Input, SqlDbType.Int);

                oDB.Retrive("BL_Statement_PatientInfo", oDBParameters, out dsPatientInfo);
                oDB.Disconnect();

                return dsPatientInfo;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        public DataTable SelectFutureAppointment(int StatementDate, Int64 PatientID, long ClinicID)
        {
            DataTable dtFutureAppointment = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";

            try
            {
                _sqlQuery = " SELECT Top 1 ISNULL(Patient.sFirstName,'')  + '|' + ISNULL(Patient.sLastName,'') sPatientName, ISNULL(AS_Appointment_MST.sASBaseDesc,'') AS sProviderName, " +
                                  " dbo.CONVERT_TO_DATE( AS_Appointment_DTL.dtStartDate) AS dtStartDate, dbo.CONVERT_TO_TIME( AS_Appointment_DTL.dtStartTime) AS dtStartTime, " +
                                  " ISNULL(AS_Appointment_DTL.sLocationName,'') AS sLocationName, ISNULL(AB_Location.sAddressLine1,'') + '|' + ISNULL(AB_Location.sAddressLine2,'') + '|'+ ISNULL(AB_Location.sCity,'') + '|'+ ISNULL(AB_Location.sState,'') + '|' + ISNULL(AB_Location.sZIP,'') As LocationAddress " +
                                  " FROM AS_Appointment_MST INNER JOIN  AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " +
                                  " Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN " +
                                  " AB_Location ON AS_Appointment_DTL.nLocationID = AB_Location.nLocationID WHERE AS_Appointment_DTL.dtStartDate > " + StatementDate + " AND  Patient.nPatientID = " + PatientID.ToString() + " AND Patient.nClinicID =" + ClinicID +
                                  " AND (AS_Appointment_DTL.nUsedStatus <> 5 AND AS_Appointment_DTL.nUsedStatus <>6 AND AS_Appointment_DTL.nUsedStatus <>7 ) AND AS_Appointment_DTL.nASBaseFlag = 1 Order by AS_Appointment_DTL.dtStartDate, AS_Appointment_DTL.dtStartTime ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtFutureAppointment);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
            }
            return dtFutureAppointment;
        }

        public DataTable GetDisplaySettings(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtDisplaySettings = new DataTable();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCriteriaID", StatementCriteriaID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("PA_GET_Patient_DisplaySettings", oDBParameters, out dtDisplaySettings);
                oDB.Disconnect();

                return dtDisplaySettings;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        public DataTable GetLastAccountPayment(Int64 AccountID, Int64 PatientID, int StatementDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtLastAccountPayment = new DataTable();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDateTime(StatementDate), ParameterDirection.Input, SqlDbType.DateTime);

                oDB.Retrive("PA_BL_ElectronicStatement_LastPatientRemit_V2", oDBParameters, out dtLastAccountPayment);
                oDB.Disconnect();

                return dtLastAccountPayment;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        public class StatementSettings
        {
            public StatementSettings()
            { }

            public StatementSettings(string suiteVersion, string statementVersion)
            {
                gloSuiteVersion = suiteVersion;
                StatementVersion = statementVersion;
            }
            public string gloSuiteVersion { get; set; }
            public string StatementVersion { get; set; }
        }

        public StatementSettings GetStatementSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtStatementSettings = new DataTable();
            StatementSettings settings = null;

            try
            {
                oDB.Connect(false);
                oDB.Retrive("PA_GET_StatementSettings", out dtStatementSettings);
                oDB.Disconnect();

                if (dtStatementSettings != null && dtStatementSettings.Rows.Count > 0)
                {
                    settings = new StatementSettings(Convert.ToString(dtStatementSettings.Rows[0]["pmVersion"]), Convert.ToString(dtStatementSettings.Rows[0]["statementVersion"]));
                }
                return settings;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (dtStatementSettings != null) { dtStatementSettings.Dispose(); dtStatementSettings = null; }
            }
        }


        public Int64 SavePatientTemplate(string templateName, Int64 categoryID, string categoryName, Int64 PatientID, Int64 AccountID, Byte[] fileBytes)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            oDBParameters.Add("@nTransactionID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
            oDBParameters.Add("@nTemplateID", 0, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sTemplateName", templateName, ParameterDirection.Input, SqlDbType.VarChar, 50);

            oDBParameters.Add("@nCategoryID", categoryID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sCategoryName", categoryName, ParameterDirection.Input, SqlDbType.VarChar, 50);

            oDBParameters.Add("@nFromDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nToDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);

            oDBParameters.Add("@nProviderID", 0, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@iTemplate", fileBytes, ParameterDirection.Input, SqlDbType.Image);

            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);

            Object oResult;
            oDB.Execute("PA_sp_INUP_PatientTemplate", oDBParameters, out oResult);

            oDB.Disconnect();
            oDB.Dispose();

            if (oResult != null && oResult.ToString() != "")
            { return Convert.ToInt64(oResult); }
            else
            { return 0; }
        }

        #endregion
    }

    #region "Patinet Statement Criteria"
    public class PatinetStatementCriteria
    {
        private Int64 _nStatementCriteriaID = 0;
        private string _sStatementCriteriaName = "";

        private string _sPracAddress1 = "";
        private string _sPracAddress2 = "";
        private string _sPracCity = "";
        private string _sPracState = "";
        private string _sPracZip = "";

        private string _sCreditCard = "";

        private string _sBillingContactName = "";
        private string _sBillingContactPhone = "";
        private string _sBillingURL = "";

        private string _sBillingEmail = "";
        private Int64 _dtOfficeStartTime;
        private Int64 _dtOfficeEndTime;

        private string _sOfficeStartTime = "";
        private string _sOfficeEndTime = "";

        private string _sPracticeTaxID = "";


        private string _sRemitName = "";
        private string _sRemitAddress1 = "";
        private string _sRemitAddress2 = "";
        private string _sRemitCity = "";
        private string _sRemitState = "";
        private string _sRemitZip = "";

        private string _sOtherName = "";
        private string _sOtherAddress1 = "";
        private string _sOtherAddress2 = "";
        private string _sOtherCity = "";
        private string _sOtherState = "";
        private string _sOtherZip = "";

        private bool _IsPendingInsurance = false;

        private string _sClinicMessage1 = "";
        private string _sClinicMessage2 = "";
        private string _criteriaType = string.Empty;

        private string _sStatementClinicMsg1 = "";
        private string _sStatementClinicMsg2 = "";
        private string _sStatementClinicMsg3 = "";
        private string _sStatementClinicMsg4 = "";
        private string _sStatementClinicMsg5 = "";
        private string _sDetachandReturnInstructions = "";

        private bool _bIsGuarantorIndicator = false;
        private bool _bIsIncludeInsuranceRemit = false;
        private bool _bIsIncludeClaim = false;
        private bool _bIsDefault = false;

        private DataTable _PatStatementCriteriaFilter;

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";

        private Int64 _nPayableTo;
        private Int32 _nRemitTo;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #region Properties

        public Int64 StatementCriteriaID
        {
            get { return _nStatementCriteriaID; }
            set { _nStatementCriteriaID = value; }
        }

        public string StatementCriteriaName
        {
            get { return _sStatementCriteriaName; }
            set { _sStatementCriteriaName = value; }
        }

        public string PracAddress1
        {
            get { return _sPracAddress1; }
            set { _sPracAddress1 = value; }
        }

        public string PracAddress2
        {
            get { return _sPracAddress2; }
            set { _sPracAddress2 = value; }
        }

        public string PracCity
        {
            get { return _sPracCity; }
            set { _sPracCity = value; }
        }

        public string PracState
        {
            get { return _sPracState; }
            set { _sPracState = value; }
        }

        public string PracZip
        {
            get { return _sPracZip; }
            set { _sPracZip = value; }
        }

        public string CreditCard
        {
            get { return _sCreditCard; }
            set { _sCreditCard = value; }
        }

        public string BillingContactName
        {
            get { return _sBillingContactName; }
            set { _sBillingContactName = value; }
        }

        public string BillingContactPhone
        {
            get { return _sBillingContactPhone; }
            set { _sBillingContactPhone = value; }
        }

        public string BillingURL
        {
            get { return _sBillingURL; }
            set { _sBillingURL = value; }
        }

        public string BillingEmail
        {
            get { return _sBillingEmail; }
            set { _sBillingEmail = value; }
        }

        public Int64 OfficeStartTime
        {
            get { return _dtOfficeStartTime; }
            set { _dtOfficeStartTime = value; }
        }

        public Int64 OfficeEndTime
        {
            get { return _dtOfficeEndTime; }
            set { _dtOfficeEndTime = value; }
        }

        public string OfficeStart
        {
            get { return _sOfficeStartTime; }
        }

        public string OfficeEnd
        {
            get { return _sOfficeEndTime; }
        }

        public string PracticeTaxID
        {
            get { return _sPracticeTaxID; }
            set { _sPracticeTaxID = value; }
        }

        public string RemitName
        {
            get { return _sRemitName; }
            set { _sRemitName = value; }
        }
        public string RemitAddress1
        {
            get { return _sRemitAddress1; }
            set { _sRemitAddress1 = value; }
        }

        public string RemitAddress2
        {
            get { return _sRemitAddress2; }
            set { _sRemitAddress2 = value; }
        }

        public string RemitCity
        {
            get { return _sRemitCity; }
            set { _sRemitCity = value; }
        }

        public string RemitState
        {
            get { return _sRemitState; }
            set { _sRemitState = value; }
        }

        public string RemitZip
        {
            get { return _sRemitZip; }
            set { _sRemitZip = value; }
        }


        public bool IsPendingInsurance
        {
            get { return _IsPendingInsurance; }
            set { _IsPendingInsurance = value; }
        }

        public string ClinicMessage1
        {
            get { return _sClinicMessage1; }
            set { _sClinicMessage1 = value; }
        }

        public string ClinicMessage2
        {
            get { return _sClinicMessage2; }
            set { _sClinicMessage2 = value; }
        }


        public string StatementClinicMsg1
        {
            get { return _sStatementClinicMsg1; }
            set { _sStatementClinicMsg1 = value; }
        }

        public string StatementClinicMsg2
        {
            get { return _sStatementClinicMsg2; }
            set { _sStatementClinicMsg2 = value; }
        }

        public string StatementClinicMsg3
        {
            get { return _sStatementClinicMsg3; }
            set { _sStatementClinicMsg3 = value; }
        }

        public string StatementClinicMsg4
        {
            get { return _sStatementClinicMsg4; }
            set { _sStatementClinicMsg4 = value; }
        }

        public string StatementClinicMsg5
        {
            get { return _sStatementClinicMsg5; }
            set { _sStatementClinicMsg5 = value; }
        }

        public string DetachandReturnInstructions
        {
            get { return _sDetachandReturnInstructions; }
            set { _sDetachandReturnInstructions = value; }
        }

        public bool IsGuarantorIndicator
        {
            get { return _bIsGuarantorIndicator; }
            set { _bIsGuarantorIndicator = value; }
        }

        public bool IsIncludeInsuranceRemit
        {
            get { return _bIsIncludeInsuranceRemit; }
            set { _bIsIncludeInsuranceRemit = value; }
        }

        public bool IsIncludeClaim
        {
            get { return _bIsIncludeClaim; }
            set { _bIsIncludeClaim = value; }
        }

        public bool IsDefault
        {
            get { return _bIsDefault; }
            set { _bIsDefault = value; }
        }

        public DataTable PatStatementCriteriaFilter
        {
            get { return _PatStatementCriteriaFilter; }
            set { _PatStatementCriteriaFilter = value; }
        }

        public string CriteriaType
        {
            get { return _criteriaType; }
            set { _criteriaType = value; }
        }
        public string OtherName
        {
            get { return _sOtherName; }
            set { _sOtherName = value; }
        }
        public string OtherAddress1
        {
            get { return _sOtherAddress1; }
            set { _sOtherAddress1 = value; }
        }

        public string OtherAddress2
        {
            get { return _sOtherAddress2; }
            set { _sOtherAddress2 = value; }
        }

        public string OtherCity
        {
            get { return _sOtherCity; }
            set { _sOtherCity = value; }
        }

        public string OtherState
        {
            get { return _sOtherState; }
            set { _sOtherState = value; }
        }

        public string OtherZip
        {
            get { return _sOtherZip; }
            set { _sOtherZip = value; }
        }
        public Int64 PayableTo
        {
            get { return _nPayableTo; }
            set { _nPayableTo = value; }
        }

        public Int32 RemitTo
        {
            get { return _nRemitTo; }
            set { _nRemitTo = value; }
        }
        public bool bIsIncludeOnEveryStatement { get; set; }

        #endregion

        #region "Constructor & Destructor"


        public PatinetStatementCriteria(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        ~PatinetStatementCriteria()
        {
            Dispose(false);
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// this method insert new Patient Statement Criteria to database   
        /// and also Update old Patient Statement Criteria 
        /// </summary>
        /// <returns>Int64 --> Patient Statement Criteria ID</returns>
        public Int64 Add(string sSettingsName)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object _intresult = 0;
                oDB.Connect(false);

                // StatementCriteriaID = 0 for inserting new Statement Criteria 
                oDBParameters.Add("@nStatementCriteriaID", this.StatementCriteriaID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sStatementCriteriaName", this.StatementCriteriaName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@bitIsDefault", this.IsDefault.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                oDBParameters.Add("@sPracAddress1", this.PracAddress1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPracAddress2", this.PracAddress2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPracCity", this.PracCity, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPracState", this.PracState, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPracZip", this.PracZip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCreditCard", this.CreditCard, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBillingContactName", this.BillingContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBillingContactPhone", this.BillingContactPhone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBillingURL", this._sBillingURL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBillingEmail", this._sBillingEmail, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@nOfficeStartTime", this.OfficeStartTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nOfficeEndTime", this.OfficeEndTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                oDBParameters.Add("@sPracticeTaxID", this.PracticeTaxID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitName", this.RemitName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitAddress1", this.RemitAddress1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitAddress2", this.RemitAddress2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitCity", this.RemitCity, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitZip", this.RemitZip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemitState", this.RemitState, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@bitIsPendingInsurance", this.IsPendingInsurance.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDBParameters.Add("@sClinicMessage1", this.ClinicMessage1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sClinicMessage2", this.ClinicMessage2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@bitIsGuarantorIndicator", this.IsGuarantorIndicator.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                oDBParameters.Add("@ClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCriteriaType", this._criteriaType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@sOtherName", this.OtherName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOtherAddress1", this.OtherAddress1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOtherAddress2", this.OtherAddress2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOtherCity", this.OtherCity, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOtherZip", this.OtherZip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOtherState", this.OtherState, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@nPayableTo", this.PayableTo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nRemitTo", this.RemitTo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDBParameters.Add("@bIsIncludeInsuranceRemit", this.IsIncludeInsuranceRemit, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@bIsIncludeClaim", this.IsIncludeClaim, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@sSettingsName", sSettingsName, ParameterDirection.Input, SqlDbType.VarChar);

                oDBParameters.Add("@sStatementClinicMsg1", this.StatementClinicMsg1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sStatementClinicMsg2", this.StatementClinicMsg2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sStatementClinicMsg3", this.StatementClinicMsg3, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sStatementClinicMsg4", this.StatementClinicMsg4, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sStatementClinicMsg5", this.StatementClinicMsg5, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sDetachandReturnInstructions", this.DetachandReturnInstructions, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIncludeonEveryStatement", this.bIsIncludeOnEveryStatement, ParameterDirection.Input, SqlDbType.Bit);

                _result = oDB.Execute("RPT_INUP_PatStatementCriteria_MST", oDBParameters, out _intresult);


                if (_intresult != null)
                {
                    oDB.Execute_Query("Delete from RPT_PatStatementCriteria_Filter where nStatementCriteriaID=" + Convert.ToInt64(_intresult));

                    if (_PatStatementCriteriaFilter != null)
                    {
                        for (int i = 0; i < _PatStatementCriteriaFilter.Rows.Count; i++)
                        {
                            DataRow dr = _PatStatementCriteriaFilter.Rows[i];
                            gloDatabaseLayer.DBParameters oDBParameter = new gloDatabaseLayer.DBParameters();
                            oDBParameter.Add("@nStatementCriteriaID", Convert.ToInt64(_intresult), System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                            oDBParameter.Add("@nCritetiaName", Convert.ToString(dr[0]), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameter.Add("@sValueId", Convert.ToString(dr[1]), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameter.Add("@sValueCode", Convert.ToString(dr[2]), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameter.Add("@sValueDesc", Convert.ToString(dr[3]), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameter.Add("@ClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Execute("RPT_INUP_PatStatementCriteria_Filter", oDBParameter);
                            oDBParameter.Dispose();
                        }
                    }


                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _result;
        }

        public bool GetPatinetStatementCriteria(long StatementCriteriaID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  nStatementCriteriaID,isnull(sStatementCriteriaName,'') as sStatementCriteriaName,isnull(bitIsDefault,0) as bitIsDefault,isnull(nClinicId,0) as nClinicId,ISNULL(nPayableTo,1) AS nPayableTo,ISNULL(nRemitTo,1) AS nRemitTo ,ISNULL(bIsIncludeInsuranceRemit,0) AS bIsIncludeInsuranceRemit, ISNULL(bIsIncludeClaim,0)  AS bIsIncludeClaim  FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE nStatementCriteriaID = " + StatementCriteriaID;
                DataTable _result = new DataTable();
                oDB.Retrive_Query(strQuery, out _result);
                if (_result != null && _result.Rows.Count > 0)
                {
                    _nStatementCriteriaID = StatementCriteriaID;
                    _sStatementCriteriaName = Convert.ToString(_result.Rows[0]["sStatementCriteriaName"]);
                    _ClinicID = Convert.ToInt64(_result.Rows[0]["nClinicId"]);
                    _bIsDefault = Convert.ToBoolean(_result.Rows[0]["bitIsDefault"].GetHashCode());
                    _nPayableTo = Convert.ToInt64(_result.Rows[0]["nPayableTo"]);
                    _nRemitTo = Convert.ToInt32(_result.Rows[0]["nRemitTo"]);
                    _bIsIncludeInsuranceRemit = Convert.ToBoolean(_result.Rows[0]["bIsIncludeInsuranceRemit"].GetHashCode());
                    _bIsIncludeClaim = Convert.ToBoolean(_result.Rows[0]["bIsIncludeClaim"].GetHashCode());
                }
                _result = null;


                strQuery = "SELECT isnull(nStatementCriteriaID,0) as nStatementCriteriaID,isnull(sPracAddress1,'') as sPracAddress1,isnull(sPracAddress2,'') as sPracAddress2,isnull(sPracCity,'') as sPracCity,isnull(sPracState,'') as sPracState,isnull(sPracZip,'') as sPracZip,"
                           + "isnull(sCreditCard,'') as sCreditCard,isnull(sBillingContactName,'') as sBillingContactName,isnull(sBillingContactPhone,'') as sBillingContactPhone,nOfficeStartTime ,nOfficeEndTime,"
                           + "isnull(sPracticeTaxID,'') as sPracticeTaxID,isnull(sRemitName,'') as sRemitName,isnull(sRemitAddress1,'') as sRemitAddress1,isnull(sRemitAddress2,'') as sRemitAddress2,isnull(sRemitCity,'') as sRemitCity,isnull(sRemitState,'') as sRemitState,isnull(sRemitZip,'')as sRemitZip,"
                           + "isnull(bitIsPendingInsurance,0) as bitIsPendingInsurance,isnull(sClinicMessage1,'') as sClinicMessage1,isnull(sClinicMessage2,'') as sClinicMessage2,isnull(bitIsGuarantorIndicator,0) as bitIsGuarantorIndicator,isnull(nClinicId,1) as nClinicId "
                           + " , isnull(nAddressType,0) as AddressType, ISNULL(sBillingURL,'') AS sBillingURL, ISNULL(sBillingEmail,'') AS sBillingEmail,  ISNULL(sStatementClinicMsg1,'') as sStatementClinicMsg1,ISNULL(sStatementClinicMsg2,'') as sStatementClinicMsg2 ,isnull(sStatementClinicMsg3,'') as sStatementClinicMsg3,isnull(sStatementClinicMsg4,'') as sStatementClinicMsg4,isnull(sStatementClinicMsg5,'') as sStatementClinicMsg5, ISNULL(sDetachandReturnInstructions,'') AS sDetachandReturnInstructions,ISNULL(bIncludeonEveryStatement,0) AS bIncludeonEveryStatement "
                           + " FROM RPT_PatStatementCriteria_Display WITH (NOLOCK) WHERE nStatementCriteriaID = " + StatementCriteriaID;
                _result = new DataTable();
                oDB.Retrive_Query(strQuery, out _result);
                if (_result != null && _result.Rows.Count > 0)
                {
                    _sPracAddress1 = Convert.ToString(_result.Rows[0]["sPracAddress1"]);
                    _sPracAddress2 = Convert.ToString(_result.Rows[0]["sPracAddress2"]);
                    _sPracCity = Convert.ToString(_result.Rows[0]["sPracCity"]);
                    _sPracState = Convert.ToString(_result.Rows[0]["sPracState"]);
                    _sPracZip = Convert.ToString(_result.Rows[0]["sPracZip"]);

                    _sCreditCard = Convert.ToString(_result.Rows[0]["sCreditCard"]);

                    _sBillingContactName = Convert.ToString(_result.Rows[0]["sBillingContactName"]);
                    _sBillingContactPhone = Convert.ToString(_result.Rows[0]["sBillingContactPhone"]);
                    _sBillingURL = Convert.ToString(_result.Rows[0]["sBillingURL"]);
                    _sBillingEmail = Convert.ToString(_result.Rows[0]["sBillingEmail"]);
                    _dtOfficeStartTime = Convert.ToInt64(_result.Rows[0]["nOfficeStartTime"]);
                    _dtOfficeEndTime = Convert.ToInt64(_result.Rows[0]["nOfficeEndTime"]);

                    _sOfficeStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Today, Convert.ToInt32(_dtOfficeStartTime)).ToShortTimeString();
                    _sOfficeEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Today, Convert.ToInt32(_dtOfficeEndTime)).ToShortTimeString();


                    _sPracticeTaxID = Convert.ToString(_result.Rows[0]["sPracticeTaxID"]);

                    _IsPendingInsurance = Convert.ToBoolean(_result.Rows[0]["bitIsPendingInsurance"].GetHashCode());

                    _sClinicMessage1 = Convert.ToString(_result.Rows[0]["sClinicMessage1"]);
                    _sClinicMessage2 = Convert.ToString(_result.Rows[0]["sClinicMessage2"]);

                    _sStatementClinicMsg1 = Convert.ToString(_result.Rows[0]["sStatementClinicMsg1"]);
                    _sStatementClinicMsg2 = Convert.ToString(_result.Rows[0]["sStatementClinicMsg2"]);
                    _sStatementClinicMsg3 = Convert.ToString(_result.Rows[0]["sStatementClinicMsg3"]);
                    _sStatementClinicMsg4 = Convert.ToString(_result.Rows[0]["sStatementClinicMsg4"]);
                    _sStatementClinicMsg5 = Convert.ToString(_result.Rows[0]["sStatementClinicMsg5"]);
                    _sDetachandReturnInstructions = Convert.ToString(_result.Rows[0]["sDetachandReturnInstructions"]);
                     bIsIncludeOnEveryStatement = Convert.ToBoolean(_result.Rows[0]["bIncludeonEveryStatement"]);
                    _bIsGuarantorIndicator = Convert.ToBoolean(_result.Rows[0]["bitIsGuarantorIndicator"].GetHashCode());

                    for (int i = 0; i < _result.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(_result.Rows[i]["AddressType"]) == PaymentAddressType.RemitAddress.GetHashCode())
                        {
                            _sRemitName = Convert.ToString(_result.Rows[i]["sRemitName"]);
                            _sRemitAddress1 = Convert.ToString(_result.Rows[i]["sRemitAddress1"]);
                            _sRemitAddress2 = Convert.ToString(_result.Rows[i]["sRemitAddress2"]);
                            _sRemitCity = Convert.ToString(_result.Rows[i]["sRemitCity"]);
                            _sRemitState = Convert.ToString(_result.Rows[i]["sRemitState"]);
                            _sRemitZip = Convert.ToString(_result.Rows[i]["sRemitZip"]);


                        }
                        else if (Convert.ToInt64(_result.Rows[i]["AddressType"]) == PaymentAddressType.OtherAddress.GetHashCode())
                        {
                            _sOtherName = Convert.ToString(_result.Rows[i]["sRemitName"]);
                            _sOtherAddress1 = Convert.ToString(_result.Rows[i]["sRemitAddress1"]);
                            _sOtherAddress2 = Convert.ToString(_result.Rows[i]["sRemitAddress2"]);
                            _sOtherCity = Convert.ToString(_result.Rows[i]["sRemitCity"]);
                            _sOtherState = Convert.ToString(_result.Rows[i]["sRemitState"]);
                            _sOtherZip = Convert.ToString(_result.Rows[i]["sRemitZip"]);

                        }
                    }

                }
                _result = null;

                strQuery = "SELECT isnull(nCritetiaName,'') as CritetiaName,isnull(sValueId,'') as ValueId,isnull(sValueCode,'') as ValueCode,isnull(sValueDesc,'') as ValueDesc FROM RPT_PatStatementCriteria_Filter WITH (NOLOCK) WHERE nStatementCriteriaID = " + StatementCriteriaID;
                _result = new DataTable();
                oDB.Retrive_Query(strQuery, out _result);
                if (_result != null)
                {
                    _PatStatementCriteriaFilter = _result;
                }
                _result = null;
                return true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
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

        public DataTable GetPatinetStatementCriterias()
        {
            DataTable _result = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  nStatementCriteriaID,isnull(sStatementCriteriaName,'') as sStatementCriteriaName,isnull(nClinicId,0) as nClinicId, " +
                                "  CASE  bitIsDefault WHEN 'true' THEN 'Default'ELSE ''END AS [Default], bitIsDefault As  isDefault" +
                                " FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) ORDER BY sStatementCriteriaName";
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public DataTable GetPatinetStatementFilter()
        {
            DataTable _result = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  nStatementCriteriaID,isnull(sStatementCriteriaName,'') as sStatementCriteriaName,isnull(nClinicId,0) as nClinicId, " +
                                "  CASE  bitIsDefault WHEN 'true' THEN 'Default'ELSE ''END AS [Default], bitIsDefault As  isDefault" +
                                " FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE CriteriaType = 'Filter' ORDER BY sStatementCriteriaName";
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public DataTable GetBusinessCenterFilter()
        {
            DataTable _result = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  nBusinessCenterID ," +
                                  "          sBusinessCenterCode ," +
                                  "          sDescription ," +
                                  "          bIsActive ," +
                                  "          nStatementDisplaySettingsID" +
                                  "  FROM    dbo.BL_BusinessCenterCodes WITH ( NOLOCK )" +
                                  "  WHERE   bIsActive = 1 " +
                                  "  ORDER BY sBusinessCenterCode ";
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public DataTable GetPatinetStatementDisplaySettings()
        {
            DataTable _result = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  nStatementCriteriaID,isnull(sStatementCriteriaName,'') as sStatementCriteriaName,isnull(nClinicId,0) as nClinicId, " +
                                "  CASE  bitIsDefault WHEN 'true' THEN 'Default'ELSE ''END AS [Default], bitIsDefault As  isDefault, CriteriaType as Type" +
                                " FROM RPT_PatStatementCriteria_MST WITH(NOLOCK) WHERE CriteriaType = 'Display' ORDER BY sStatementCriteriaName";
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _intresult = 0;
            Int64 _result = 0;

            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from RPT_PatStatementCriteria_MST where nStatementCriteriaID =" + ID;
                _result = oDB.Execute_Query(strQuery);

                strQuery = "delete from RPT_PatStatementCriteria_Filter where nStatementCriteriaID =" + ID;
                _result = oDB.Execute_Query(strQuery);

                strQuery = "delete from RPT_PatStatementCriteria_Display where nStatementCriteriaID =" + ID;
                _result = oDB.Execute_Query(strQuery);

                if (_result > 0)
                {
                    return true;
                }
                return false;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public bool IsExists(Int64 StatementCriteriaID, string StatementCriteriaName)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {

                string _sqlQuery = "";
                if (StatementCriteriaID == 0)
                {
                    //for add 
                    // _sqlQuery = "SELECT Count(nResourceID) FROM AB_Resource_MST WHERE sDescription ='" + ResourceDescription + "'";
                    _sqlQuery = "SELECT Count(nStatementCriteriaID) FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE sStatementCriteriaName ='" + StatementCriteriaName.Replace("'", "''") + "' AND nClinicID =" + this.ClinicID + " ";
                    //
                }
                else
                {
                    //for modify
                    _sqlQuery = "SELECT Count(nStatementCriteriaID) FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE (sStatementCriteriaName ='" + StatementCriteriaName.Replace("'", "''") + "' AND nStatementCriteriaID <> " + StatementCriteriaID + ") AND nClinicID = " + this.ClinicID + " ";
                    //
                }

                object _intresult = null;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _result;
        }

        public bool IsExists(Int64 StatementCriteriaID, string StatementCriteriaName, string CriteriaType)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {

                string _sqlQuery = "";
                if (StatementCriteriaID == 0)
                {
                    //for add 
                    // _sqlQuery = "SELECT Count(nResourceID) FROM AB_Resource_MST WHERE sDescription ='" + ResourceDescription + "'";
                    _sqlQuery = "SELECT Count(nStatementCriteriaID) FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE sStatementCriteriaName ='" + StatementCriteriaName.Replace("'", "''") + "' AND nClinicID =" + this.ClinicID + " AND criteriaType = '" + CriteriaType + "' ";
                    //
                }
                else
                {
                    //for modify
                    _sqlQuery = "SELECT Count(nStatementCriteriaID) FROM RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE (sStatementCriteriaName ='" + StatementCriteriaName.Replace("'", "''") + "' AND nStatementCriteriaID <> " + StatementCriteriaID + ") AND nClinicID = " + this.ClinicID + " AND criteriaType = '" + CriteriaType + "' ";
                    //
                }

                object _intresult = null;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _result;
        }

        //Added by Rahul Patel on 15-09-2010
        // for checking the default value validation
        public bool IsDefaultChanged(string _description)
        {
            DataTable dtDefault = new DataTable();
            string strQuery = "SELECT sStatementCriteriaName,bitIsDefault  FROM  RPT_PatStatementCriteria_MST WITH (NOLOCK) WHERE (bitIsDefault = 1) AND (criteriaType = 'Filter') ";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                oDB.Retrive_Query(strQuery, out dtDefault);

                if (dtDefault != null)
                {
                    if (dtDefault.Rows.Count > 0)
                    {
                        if (dtDefault.Rows[0]["sStatementCriteriaName"].ToString().Trim() == _description.Trim())
                            return false;
                        else
                            return true;
                    }
                }
                else
                {
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return false;
        } // IsDefaultChanged

        public bool VerifyforDeleteDisplaySettingsCode(Int64 StatementCriteriaID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT 1 FROM dbo.BL_BusinessCenterCodes WHERE nStatementDisplaySettingsID =  " + StatementCriteriaID +
                           " UNION SELECT 1 FROM dbo.RPT_PatStatementCriteria_MST WHERE nStatementCriteriaID = " + StatementCriteriaID + " AND bitIsDefault = 1";

                string result = Convert.ToString(oDB.ExecuteScalar_Query(strQuery));

                if (result != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
            }
        }

        #endregion

    }
    #endregion

}
