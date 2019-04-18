using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gloDatabaseLayer;

namespace gloBilling
{
    class PatientFinancialView
    {


        #region "Variable Declaration"
        private string _sqlDatabaseConnectionString = "";
        private Int64 _nPatientID = 0;
        private Int64 _nPAccountID = 0;
        private Int64 _nClinicID = 0;
    //    private bool _blnIsClaimVoided;


        #endregion

        #region "Constructors & destructor"

        public PatientFinancialView(string DatabaseConnectionString,Int64 patientID, Int64 clinicID)
        {
            _sqlDatabaseConnectionString = DatabaseConnectionString;
            _nPatientID = patientID;
            _nClinicID = clinicID;        
        }

        public PatientFinancialView(string DatabaseConnectionString, Int64 patientID, Int64 PAccountID, Int64 clinicID)
        {
            _sqlDatabaseConnectionString = DatabaseConnectionString;
            _nPatientID = patientID;
            _nPAccountID = PAccountID;
            _nClinicID = clinicID;
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

        ~PatientFinancialView()
        {
            Dispose(false);

        }

        #endregion

        #region "Methods"

        public long GetClaimTransactionID(Int64 _nClaimNo, string _subclaimNo, bool _isVoid)
        {

            #region "To Fetch the TransactionID of Claim"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            object _TransactionId = null;
            DataTable _dtTransID = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nClaimno", _nClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@sSubClaimno", _subclaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPatameters.Add("@bIsVoid", _isVoid, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("BL_Get_TransactionID", oDBPatameters, out _dtTransID);
                if (_dtTransID != null && _dtTransID.Rows.Count > 0)
                {
                    return Convert.ToInt64(_dtTransID.Rows[0]["nTransactionID"]);
                }
                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            if (_TransactionId != null)
                return Convert.ToInt64(_TransactionId);
            else
                return 0;
            #endregion
            // throw new Exception("The method or operation is not implemented.");
        }

        public Boolean ChkClaimVoided(Int64 _nTransactionMstID)
        {

            #region "To check Claim is voided or not"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            Boolean isVoided = false;
            DataTable _dtClaimVoided = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDB.Retrive_Query(" select bIsVoid from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID=" + _nTransactionMstID, out _dtClaimVoided);
                if (_dtClaimVoided != null && _dtClaimVoided.Rows.Count > 0)
                {
                    isVoided = Convert.ToBoolean(_dtClaimVoided.Rows[0]["bIsVoid"] == DBNull.Value ? false : true);
                }

                oDB.Disconnect();
                return isVoided;
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                return isVoided;
            }
            finally
            {
                oDB.Dispose();
            }

            #endregion
            // throw new Exception("The method or operation is not implemented.");
        }

        private DataSet fillgridData(string spName, DBParameters oParameters, DBLayer oDB)
        {
            if (oParameters.Count > 0)
            {
                oParameters.Clear();
            }

            DataSet dsdata = new DataSet();

            try
            {
                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                if (spName != "gSP_GET_Chronology")
                {
                    oParameters.Add("@nPAccountID", _nPAccountID , ParameterDirection.Input, SqlDbType.BigInt);
                }
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive(spName, oParameters, out dsdata);
                oDB.Disconnect();
                return dsdata;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;

            }
        }

        public string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST WITH (NOLOCK)");
            if (_Result.ToString() != "")
            { return _Result.ToString(); }
            else
            { return ""; }
        }

        //Bind the Patient Refund
        public DataTable FillPatRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            string _sqlQuery = "";
            DataTable dtPatientRefund = new DataTable();
            try
            {
                _sqlQuery = "select ISNULL(nRefundID,0) as nRefundID," +
                    " (Select ISNULL(Patient.sFirstName,'')+SPACE(1)+ISNULL(Patient.sLastName,'') " 
                           + " from PAtient Where nPatientId = BL_EOBPatient_Refund.nPayerID) AS sPatientName, "
                           + " convert(datetime,dbo.CONVERT_TO_DATE(nCloseDate)) as nCloseDate,"
                           + "sPaymentTrayDescription,sRefundTo,convert(datetime,dbo.CONVERT_TO_DATE(nRefundDate)) AS nRefundDate,nRefundAmount,sRefundNotes,"
                           + " sUserName,dtModifiedDateTime as dtCreatedDateTime,case bIsVoid WHEN '1' THEN 'Voided' ELSE '' END AS Status "
                           + "From BL_EOBPatient_Refund  WITH (NOLOCK) " 
                           +" Where nPAccountID = " + _nPAccountID;

                if (_nPatientID > 0)
                    _sqlQuery +=  " and nPayerID=" + _nPatientID;

                _sqlQuery += "  order by nCloseDate desc,dtCreatedDateTime desc";
                
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
                oDB.Disconnect();
                return dtPatientRefund;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();
                return null;

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        public DataTable FillInsReservesAssociatedWithPatient()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            DataTable _dtReserves = new DataTable();
            try
            {               
                oDB.Connect(false);
                oDB.Retrive_Query("Select InsuarnceCompanyName,nEOBPaymentID,nEOBID,nEOBDtlID,nEOBPaymentDetailID, " 
                                 + " nBillingTransactionID,nBillingTransactionDetailID,nBillingTransactionLineNo,nPatientID, " 
                                 + " nDOSFrom,nDOSTo,nAmount,nPayMode,nRefEOBPaymentID,nRefEOBPaymentDetailID,nResEOBPaymentID, "  
                                 + " nResEOBPaymentDetailID,nAccountID,nAccountType,nMSTAccountID,nMSTAccountType, nPaymentMode, " 
                                 + " CheckNumber,nCheckAmount,nCheckDate, nPayerID,PatientName,sNoteDescription,sNoteCode, " 
                                 + " nPaymentNoteSubType, sUserName ,UsedReserve ,AvailableReserve,nCloseDate,AssociationPatientID, "
                                 + " AssociationPatient,AssociationMSTTransactionID,AssociationnTransactionID,AssociationClaim "
                                 + " from view_PatientInsCompanyReserves where AssociationPatientID = " + _nPatientID, out _dtReserves);
                oDB.Disconnect();
                return _dtReserves;
               
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); return null; }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        public Boolean chkIsInsReserveRefundExist()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            DataTable _dtReserves = new DataTable();
            DataTable _dtRefundLog = new DataTable();
            Boolean _bIsDataExist = false;
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query(" SELECT  Reserves.nCreditID AS nEOBPaymentID "
                                + " FROM  BL_Reserve_Association WITH (NOLOCK) LEFT OUTER JOIN  Reserves "
                                + " ON   BL_Reserve_Association.nEOBPaymentID = Reserves.nCreditID "
                                + " WHERE (Reserves.nReserveType = 1) AND  BL_Reserve_Association.nPatientID =  " + _nPatientID, out _dtReserves);
                oDB.Disconnect();
                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    _bIsDataExist = true;
                }
                else
                {
                    _dtRefundLog = EOBPayment.gloEOBPaymentPatient.GetPatientPaymentRefundLog(_nPatientID, _nClinicID);
                    if (_dtRefundLog != null && _dtRefundLog.Rows.Count > 0)
                    {
                        _bIsDataExist = true;
                    }
                }
            }
            catch
            {
                _bIsDataExist = false;

            }
            finally
            {
                _dtReserves.Dispose();
                _dtRefundLog.Dispose();
                if (oDB.Connect(false))
                { oDB.Disconnect(); }
                if (oDB != null)
                { oDB.Dispose(); }
            }
            return _bIsDataExist;

        }

        public void Fill_PatientStatement(out DataSet dsPatStatement)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            dsPatStatement = new DataSet();
            try
            {
                //string sqlQuery = "";
               
                //sqlQuery = "SELECT CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay  WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed] '  ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
                //          " ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName, ISNULL(Patient.sFirstName,'') as sFirstName, ISNULL(Patient.sMiddleName,'') as sMiddleName, ISNULL(Patient.sLastName,'') as sLastName, " +
                //          " ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID, ISNULL(Patient.nPatientID,0) as nPatientID,sVoidNotes As Notes,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID" +
                //          " FROM Patient WITH (NOLOCK) INNER JOIN " +
                //          " BL_Batch_PatientStatement_DTL WITH (NOLOCK) ON Patient.nPatientID = BL_Batch_PatientStatement_DTL.nPatientID INNER JOIN " +
                //          " BL_Batch_PatientStatement_Mst WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where Patient.nPatientID=" + _nPatientID + " order by dtCreateDate desc";
                
                //oDB.Connect(false);
                //oDB.Retrive_Query(sqlQuery, out dsPatStatement);
                //oDB.Disconnect();
 
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nPatientID , ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Fill_PatientStatement", oParameters, out dsPatStatement);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        public bool IsPatientExclude() //ExcludeFromStament is related to account not patient
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            string _sqlQuery = "";
            bool _result;
            try
            {
                //_sqlQuery = " SELECT Count(*) FROM PatientSettings WITH (NOLOCK) WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";
                _sqlQuery = " Select Count(nPAccountID) From PA_Accounts WITH (NOLOCK) Where bIsExcludeStatement =1 and  nPAccountID = " + _nPAccountID;
                oDB.Connect(false);
                _result = (bool)oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                return _result;
            }
            catch
            {
                return false;
            }
        }

        public void GetSummary(char SortType, out DataSet dsFinanSummary)
        {
            gloAuditTrail.gloAuditTrail.SetAppConfig(_sqlDatabaseConnectionString);
            DBLayer oDB = new DBLayer(_sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            dsFinanSummary = new DataSet();
            
            //Code added by SaiKrishna for account feature.
            DataTable dtAccPatSummary = new DataTable("Summary");
            DataSet dsAccPatSummary = new DataSet();

            try
            {

            //oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
            //oParameters.Add("@nClinicID",_nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //oParameters.Add("@nSortBy", SortType, ParameterDirection.Input, SqlDbType.Char);
            //oDB.Connect(false);
            //oDB.Retrive("Patient_Financial_View_Summary", oParameters, out dsFinanSummary);
            //oDB.Disconnect();

            //dsFinanSummary.Tables[0].TableName = "Summary";
            //dsFinanSummary.Tables["Summary"].Columns.Add("Total", typeof(System.Decimal));
            //decimal decZeroThirty = 0;
            //decimal decThirtySixty = 0;
            //decimal decSixtyNinety = 0;
            //decimal decNinetyHundredTwenty = 0;
            //decimal decHundredTwentyPlus = 0;
            //decimal decTotal = 0;

            //if (dsFinanSummary.Tables["Summary"].Rows.Count > 0)
            //{

            //    for (int cntr = 0; cntr <= (dsFinanSummary.Tables["Summary"].Rows.Count - 1); cntr++)
            //    {
            //        dsFinanSummary.Tables["Summary"].Rows[cntr]["Total"] = Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"]);
            //        decZeroThirty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"]);
            //        decThirtySixty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"]);
            //        decSixtyNinety += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"]);
            //        decNinetyHundredTwenty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"]);
            //        decHundredTwentyPlus += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"]);
            //        decTotal += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["Total"]);
            //    }
            //    dsFinanSummary.Tables["Summary"].Rows.Add();
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["ResponsibleParty"] = "Total :";
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["0-30"] = decZeroThirty;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["31-60"] = decThirtySixty;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["61-90"] = decSixtyNinety;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["91-120"] = decNinetyHundredTwenty;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["120+"] = decHundredTwentyPlus;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["Total"] = decTotal;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
            //    dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;

            //}




            //Code added by SaiKrishna for account feature.Get the AccountPatients based on AccountId and PatientId
            DataTable dtAccPatients = new DataTable();
            oParameters.Add("@nPAccountId", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
            oParameters.Add("@nPatientId", _nPatientID , ParameterDirection.Input, SqlDbType.BigInt);
            oDB.Connect(false);
            oDB.Retrive("PA_Select_Accounts_Patients", oParameters, out dtAccPatients);
            oDB.Disconnect();
            if (dtAccPatients != null && dtAccPatients.Rows.Count > 0)
            {
                decimal grandTotDecZeroThirty = 0;
                decimal grandTotdecThirtySixty = 0;
                decimal grandTotdecSixtyNinety = 0;
                decimal grandTotdecNinetyHundredTwenty = 0;
                decimal grandTotdecHundredTwentyPlus = 0;
                decimal grandTotdecTotal = 0;
                decimal decTotal = 0;
                for (int i = 0; i < dtAccPatients.Rows.Count; i++)
                {
                    string sPatientName = dtAccPatients.Rows[i]["FirstName"].ToString() + ' ' + (dtAccPatients.Rows[i]["MiddleName"].ToString() != "" ? dtAccPatients.Rows[i]["MiddleName"].ToString() + ' ' : "") + dtAccPatients.Rows[i]["LastName"].ToString();
                    oParameters.Clear();
                    oParameters.Add("@nPatientID", Convert.ToInt64(dtAccPatients.Rows[i]["PatientID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", _nPAccountID  , ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", _nClinicID , ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nSortBy", SortType, ParameterDirection.Input, SqlDbType.Char);
                    oDB.Connect(false);
                    oDB.Retrive("PA_Patient_Financial_View_Summary", oParameters, out dsFinanSummary);
                    oDB.Disconnect();
                    dsFinanSummary.Tables[0].TableName = "Summary";
                    dsFinanSummary.Tables["Summary"].Columns.Add("Total", typeof(System.Decimal));
                    decimal decZeroThirty = 0;
                    decimal decThirtySixty = 0;
                    decimal decSixtyNinety = 0;
                    decimal decNinetyHundredTwenty = 0;
                    decimal decHundredTwentyPlus = 0;
                    decTotal = 0;
                    if (i == 0)
                    {
                        dtAccPatSummary = dsFinanSummary.Tables["Summary"].Clone();
                        dtAccPatSummary.Columns.Add("PatName", typeof(System.String));
                    }

                    DataRow dr = dtAccPatSummary.NewRow();
                    dr["PatName"] = sPatientName;
                    dtAccPatSummary.Rows.Add(dr);

                    if (dsFinanSummary.Tables["Summary"].Rows.Count > 0)
                    {

                        for ( int cntr = 0; cntr <= (dsFinanSummary.Tables["Summary"].Rows.Count - 1); cntr++)
                        {

                            dsFinanSummary.Tables["Summary"].Rows[cntr]["Total"] = Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"]) + Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"]);
                            decZeroThirty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["0-30"]);
                            decThirtySixty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["31-60"]);
                            decSixtyNinety += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["61-90"]);
                            decNinetyHundredTwenty += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["91-120"]);
                            decHundredTwentyPlus += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsFinanSummary.Tables["Summary"].Rows[cntr]["120+"]);
                            decTotal += Convert.ToDecimal(dsFinanSummary.Tables["Summary"].Rows[cntr]["Total"]);
                            dtAccPatSummary.ImportRow(dsFinanSummary.Tables["Summary"].Rows[cntr]);
                        }

                        dsFinanSummary.Tables["Summary"].Rows.Add();
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["ResponsibleParty"] = "Total :";
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["0-30"] = decZeroThirty;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["31-60"] = decThirtySixty;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["61-90"] = decSixtyNinety;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["91-120"] = decNinetyHundredTwenty;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["120+"] = decHundredTwentyPlus;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["Total"] = decTotal;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
                        dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;
                        dtAccPatSummary.ImportRow(dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]);


                        //calulation for grandtotal
                        grandTotDecZeroThirty += Convert.ToDecimal(decZeroThirty);
                        grandTotdecThirtySixty += Convert.ToDecimal(decThirtySixty);
                        grandTotdecSixtyNinety += Convert.ToDecimal(decSixtyNinety);
                        grandTotdecNinetyHundredTwenty += Convert.ToDecimal(decNinetyHundredTwenty);
                        grandTotdecHundredTwentyPlus += Convert.ToDecimal(decHundredTwentyPlus);
                        grandTotdecTotal += decTotal;
                    }

                }
                if (grandTotdecTotal != decTotal)
                {
                    dtAccPatSummary.Rows.Add();
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["ResponsibleParty"] = "Grand Total :";
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["0-30"] = grandTotDecZeroThirty;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["31-60"] = grandTotdecThirtySixty;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["61-90"] = grandTotdecSixtyNinety;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["91-120"] = grandTotdecNinetyHundredTwenty;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["120+"] = grandTotdecHundredTwentyPlus;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["Total"] = grandTotdecTotal;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
                    dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;
                }
            }
            dsAccPatSummary.Tables.Add(dtAccPatSummary);
            dsFinanSummary = dsAccPatSummary;

         }
            catch(Exception ex)
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
          }

        public void GetChronology(DBParameters oParameter, DBLayer oDB, out DataSet dsChronology)
        {
            gloAuditTrail.gloAuditTrail.SetAppConfig(_sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            dsChronology = new DataSet();
            
            try
            {
                dsChronology = this.fillgridData("gSP_GET_Chronology", oParameter, oDB);
                dsChronology.Tables[0].TableName = "Chronology";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        public void GetClaimsNCharges(short IsZeroFlag,int nSort,out DataSet dsClaimsNCharges)
        { 
             gloAuditTrail.gloAuditTrail.SetAppConfig(_sqlDatabaseConnectionString);
            DBLayer oDB = new DBLayer(_sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            dsClaimsNCharges = new DataSet();
            DataTable dtTotalReserves ;
             int cntr;

             try
             {
                 //Code Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                 oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                
                 oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                 oParameters.Add("@nClinicID", this._nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                 oParameters.Add("@ZeroBalFlag", IsZeroFlag, ParameterDirection.Input, SqlDbType.Bit);
                 oParameters.Add("@SortByFlag", nSort, ParameterDirection.Input, SqlDbType.Int);
                 oDB.Connect(false);
                 oDB.Retrive("PA_Patient_Financial_View_Claims_Charges", oParameters, out dsClaimsNCharges);
                 oDB.Disconnect();
                 dsClaimsNCharges.Tables[0].TableName = "Claims_Charges";

                 decimal decAmount = 0;
                 decimal decAdjs = 0;
                 decimal decInsPmnt = 0;
                 decimal decPatPmnt = 0;
                 decimal decInsPending = 0;
                 decimal decPatPending = 0;
                 if (dsClaimsNCharges.Tables["Claims_Charges"].Rows.Count > 0)
                 {

                     for (cntr = 0; cntr <= (dsClaimsNCharges.Tables["Claims_Charges"].Rows.Count - 1); cntr++)
                     {
                         decAmount += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["dTotal"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["dTotal"]);
                         decAdjs += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PreviousAdjustment"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PreviousAdjustment"]);
                         decInsPmnt += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["InsurancePayment"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["InsurancePayment"]);
                         decPatPmnt += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PatientPayment"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PatientPayment"]);
                         decInsPending += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["InsurancePending"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["InsurancePending"]);
                         decPatPending += Convert.ToDecimal(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PatientDue"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["PatientDue"]);
                         //if (Convert.ToBoolean(dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"] == DBNull.Value ? 0 : dsClaimsNCharges.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"]))
                         //{
                         //    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                         //    this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                         //}
                         //if (dsClaimsNCharges.Tables["Claims_Charges"].Rows.Count >= iChargesSelRow)
                         //    c1FlexGridChargesClaims.Row = iChargesSelRow;

                     }
                     dtTotalReserves = new DataTable();
                     dtTotalReserves = dsClaimsNCharges.Tables["Claims_Charges"].Clone();
                     dtTotalReserves.TableName = "TotalClaims_Charges";
                     dtTotalReserves.ImportRow(dsClaimsNCharges.Tables["Claims_Charges"].Rows[dsClaimsNCharges.Tables["Claims_Charges"].Rows.Count - 1]);
                     dsClaimsNCharges.Tables.Add(dtTotalReserves);
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["dTotal"] = decAmount;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["PreviousAdjustment"] = decAdjs;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["InsurancePayment"] = decInsPmnt;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["PatientPayment"] = decPatPmnt;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["InsurancePending"] = decInsPending;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["PatientDue"] = decPatPending;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["SplitClaimNumber"] = "Total :";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["DOS"] = DBNull.Value;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sCPTCode"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sMod1Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sMod2Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sDx1Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sDx2Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sDx3Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sDx4Code"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["nProviderName"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["nCloseDate"] = DBNull.Value;
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["Party"] = "";
                     dsClaimsNCharges.Tables["TotalClaims_Charges"].Rows[0]["sPatientName"] = "";
                 }
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
             }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }           
        
        }

        public void GetPatientReserve(DBParameters oParameter, DBLayer oDB, out DataSet dsPatReserves)
        {
            dsPatReserves = new DataSet();
            DataTable dtTotalReserves;
            int cntr;
         
            dsPatReserves = fillgridData("PA_Patient_Financial_View_Reserve", oParameter, oDB);
            dsPatReserves.Tables[0].TableName = "Reserves";
            decimal decAvailable = 0;

            if (dsPatReserves.Tables["Reserves"].Rows.Count > 0)
            {

                for (cntr = 0; cntr <= (dsPatReserves.Tables["Reserves"].Rows.Count - 1); cntr++)
                {
                    //decToReserves += Convert.ToDecimal(dsPatFinView.Tables["Reserves"].Rows[cntr]["nAmount"] == DBNull.Value ? 0 : dsPatFinView.Tables["Reserves"].Rows[cntr]["nAmount"]);
                    decAvailable += Convert.ToDecimal(dsPatReserves.Tables["Reserves"].Rows[cntr]["AvailableReserve"] == DBNull.Value ? 0 : dsPatReserves.Tables["Reserves"].Rows[cntr]["AvailableReserve"]);
                }

                dtTotalReserves = new DataTable();
                dtTotalReserves = dsPatReserves.Tables["Reserves"].Clone();
                dtTotalReserves.TableName = "TotalReserves";
                dtTotalReserves.Columns["nCloseDate"].DataType = System.Type.GetType("System.String");
                dtTotalReserves.ImportRow(dsPatReserves.Tables["Reserves"].Rows[dsPatReserves.Tables["Reserves"].Rows.Count - 1]);
                dsPatReserves.Tables.Add(dtTotalReserves);
                //dsPatFinView.Tables["TotalReserves"].Columns["nCloseDate"].DataType = System.Type.GetType("System.String");
                dsPatReserves.Tables["TotalReserves"].Rows[0]["OriginalAmount"] = "";

                dsPatReserves.Tables["TotalReserves"].Rows[0]["nAmount"] = DBNull.Value;
                dsPatReserves.Tables["TotalReserves"].Rows[0]["AvailableReserve"] = decAvailable;
                dsPatReserves.Tables["TotalReserves"].Rows[0]["nPaymentNoteSubType"] = DBNull.Value;
                dsPatReserves.Tables["TotalReserves"].Rows[0]["sNoteDescription"] = "";
                dsPatReserves.Tables["TotalReserves"].Rows[0]["nCloseDate"] = "Total :";
                //Code Added by SaiKrishna
                dsPatReserves.Tables["TotalReserves"].Rows[0]["sPatientName"] = "";


            }
        
        }

        public void GetPatientPayment(DBParameters oParameter, DBLayer oDB, out DataSet dsPayment)
        {
             dsPayment = new DataSet();
            int cntr;
            dsPayment = this.fillgridData("PA_Patient_Financial_View_PatientPayment", oParameter,oDB);
            dsPayment.Tables[0].TableName = "Payments";
            if (dsPayment.Tables["Payments"].Rows.Count > 0)
            {

                DataView dv = dsPayment.Tables["Payments"].DefaultView;
                DataTable dtUniqueData = dv.ToTable(true, "nEOBPaymentID");
                DataTable dtFilterData;
                dtFilterData = dsPayment.Tables["Payments"].Clone();
                for (cntr = 0; cntr <= dtUniqueData.Rows.Count - 1; cntr++)
                {
                    DataRow[] resultRows = null;
                    resultRows = dsPayment.Tables["Payments"].Select("nEOBPaymentID=" + dtUniqueData.Rows[cntr]["nEOBPaymentID"] + " and nPaymentNoteType=6");
                    if (resultRows.Length > 0)
                    {
                        foreach (DataRow dr in resultRows)
                        {
                            dtFilterData.ImportRow(dr);
                        }
                    }
                    else
                    {
                        resultRows = dsPayment.Tables["Payments"].Select("nEOBPaymentID=" + dtUniqueData.Rows[cntr]["nEOBPaymentID"]);
                        foreach (DataRow dr in resultRows)
                        {
                            dtFilterData.ImportRow(dr);
                        }
                    }
                }
                if (dtFilterData.Rows.Count > 0)
                {
                    dtFilterData.TableName = "Payments";
                    dsPayment.Tables.Clear();
                    dsPayment.Tables.Add(dtFilterData);
                }
            }

            decimal decAvailable = 0;

            if (dsPayment.Tables["Payments"].Rows.Count > 0)
            {
                decAvailable = 0;
                for (cntr = 0; cntr <= (dsPayment.Tables["Payments"].Rows.Count - 1); cntr++)
                {
                    decAvailable += Convert.ToDecimal(dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"] == DBNull.Value ? 0 : dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"]);
                }
                DataTable dtTotalPayment = new DataTable();
                dtTotalPayment = dsPayment.Tables["Payments"].Clone();
                dtTotalPayment.TableName = "TotalPayments";
                dtTotalPayment.ImportRow(dsPayment.Tables["Payments"].Rows[dsPayment.Tables["Payments"].Rows.Count - 1]);
                dsPayment.Tables.Add(dtTotalPayment);
                dsPayment.Tables["TotalPayments"].Rows[0]["nCloseDate"] = "Total :";
                dsPayment.Tables["TotalPayments"].Rows[0]["sPaymentTrayDescription"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nPaymentMode"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nCheckDate"] = DBNull.Value;
                dsPayment.Tables["TotalPayments"].Rows[0]["sCheckNumber"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nCheckAmount"] = decAvailable;
                dsPayment.Tables["TotalPayments"].Rows[0]["sNoteDescription"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["Status"] = "";
                //Code Added by SaiKrishna for showing patientname
                dsPayment.Tables["TotalPayments"].Rows[0]["sPatientName"] = "";

            }
        
        }

        public void FillClaimOnHold(DBParameters oParameter, DBLayer oDB, out DataTable dtClaimONHold)
        {
            dtClaimONHold = new DataTable();
            try
            {
              
                oParameter.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameter.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Patient_Financial_View_Header_ClaimOnHold", oParameter, out dtClaimONHold);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        public void GetChargeNotes(DBParameters oParameter, DBLayer oDB,Int64 nTransactionID,Int64 nTransactionDetailID, out DataSet dtChargeNote)
        { 
            dtChargeNote = new DataSet();
            try
            {
                oParameter.Add("@transactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameter.Add("@transactionDetailID", nTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_ChargeNotes", oParameter, out dtChargeNote);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }


        private DataSet GetTransactionID(DBParameters oParameter, DBLayer oDB, Int64 nMstTransactionID, Int64 nMstTransactionDetailID, out DataSet dsTransaction)
        {
            dsTransaction = new DataSet();
            try
            {

                oParameter.Add("@nMasterTransactionID", nMstTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameter.Add("@nMasterTransactionDetailID", nMstTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_TransactionDetail", oParameter, out dsTransaction);
                oDB.Disconnect();
                return dsTransaction;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();
                return null;
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
          
        }
        #endregion


    }
}
