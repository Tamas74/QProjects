using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloDatabaseLayer;
using System.Data;
using System.Data.SqlClient;
using gloGlobal;
using System.Windows.Forms;


namespace gloAccountsV2
{
    class gloPatientFinancialViewV2
    {

        #region "Variable Declaration"

        private Int64 _nPatientID = 0;
        private Int64 _nPAccountID = 0;
        #endregion

        #region "Constructors & destructor"
        public gloPatientFinancialViewV2(Int64 patientID)
        {
            _nPatientID = patientID;
        }

        public gloPatientFinancialViewV2(Int64 patientID, Int64 PAccountID)
        {
            _nPatientID = patientID;
            _nPAccountID = PAccountID;
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

        ~gloPatientFinancialViewV2()
        {
            Dispose(false);
        }
        #endregion

        #region "Methods"

        public DataTable GetAccountLog(Int64 _nPatientAccountID,string StartDate,string EndDate)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtLog = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nPAccountID", _nPatientAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@dtFromDate", StartDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBPatameters.Add("@dtToDate", EndDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Retrive("PA_Patient_Financial_View_ACCOUNTLOG", oDBPatameters, out _dtLog);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return _dtLog;
        }

        public DataTable GetPatientAccountLog(Int64 _nPatientID, Int64 _nPatientAccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtLog = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nPAccountID", _nPatientAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Patient_Financial_View_AccountPatientLog", oDBPatameters, out _dtLog);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return _dtLog;
        }

        public long GetClaimTransactionID(Int64 _nClaimNo, string _subclaimNo, bool _isVoid)
        {
            #region "To Fetch the TransactionID of Claim"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            object _TransactionId = null;
            DataTable _dtTransID = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nClaimno", _nClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@sSubClaimno", _subclaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPatameters.Add("@bIsVoid", _isVoid, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("BL_Get_TransactionID_V2", oDBPatameters, out _dtTransID);
                if (_dtTransID != null && _dtTransID.Rows.Count > 0)
                {
                    return Convert.ToInt64(_dtTransID.Rows[0]["nTransactionID"]);
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        public Boolean ChkClaimVoided(Int64 _nTransactionMstID)
        {

            #region "To check Claim is voided or not"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            Boolean isVoided = false;
            DataTable _dtClaimVoided = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return isVoided;
            }
            finally
            {
                oDB.Dispose();
            }

            #endregion
        }

        private DataSet fillgridData(string spName)
        {

            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsdata = new DataSet();

            try
            {
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
               // oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive(spName, oParameters, out dsdata);
                oDB.Disconnect();
                return dsdata;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
             

                return null;

            }
        }

        public string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable dtPatientRefund = new DataTable();
            try
            {
                _sqlQuery =" SELECT ISNULL(nRefundID,0) as nRefundID, "
                         + " ISNULL(Patient.sFirstName,'')+SPACE(1)+ISNULL(Patient.sLastName,'') AS sPatientName, "
                         + " Credits.dtCloseDate as nCloseDate,Credits.sPaymentTrayDesc ,sRefundTo,dtRefundDate AS nRefundDate,nRefundAmount , "
                         + " sRefundNotes ,Credits.sUserName,Credits_EXT.dtModifiedDateTime AS dtCreatedDateTime , "
                         + " CASE Credits.bIsPaymentVoid WHEN '1' THEN 'Voided' ELSE '' END AS Status,  "
                         + " Refunds.nPatientID AS nPatientID ,Contacts_mst.sName as [Collection Agency] "
                         + " From Refunds  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Refunds.nPayerID =Patient.nPatientID  INNER JOIN "
                         + " Credits  WITH (NOLOCK) ON Credits.nCreditID = Refunds.nCreditID INNER JOIN Credits_EXT WITH (NOLOCK) ON Credits.nCreditID = Credits_Ext.nCreditID  "
                         + " LEFT OUTER JOIN Contacts_mst WITH (NOLOCK) ON Contacts_mst.nContactId=Refunds.nCollectionAgencyContactId "
                         + " Where Refunds.nPAccountID = " + _nPAccountID;

            if (_nPatientID > 0)
                    _sqlQuery += " and Refunds.nPayerID=" + _nPatientID;

                _sqlQuery += "  order by nCloseDate desc,dtCreatedDateTime desc";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
                oDB.Disconnect();
                return dtPatientRefund;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);   MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            DataTable _dtReserves = new DataTable();
            try
            {
                oDB.Connect(false);
                string _strQuery = "";

                if (_nPatientID > 0)
                {
                    _strQuery = "SELECT InsuarnceCompanyName,nEOBPaymentID,nAmount,nPayMode,CheckNumber,nCheckAmount,nCheckDate, nPayerID, "
                                + " sNoteDescription,sNoteCode,  nPaymentNoteSubType, sUserName ,UsedReserve ,AvailableReserve,nCloseDate, "
                                + " nPAccountID,nAccountPatientID,nGuarantorID, AssociationPatientID,AssociationPatient,AssociationMSTTransactionID, "
                                 + " AssociationnTransactionID,AssociationClaim FROM view_PatientInsCompanyReserves_V2  WITH (NOLOCK)"
                                + " WHERE AssociationPatientID = " + _nPatientID;

                }
                else
                {
                    _strQuery = "SELECT InsuarnceCompanyName,nEOBPaymentID,nAmount,nPayMode,CheckNumber,nCheckAmount,nCheckDate, nPayerID, "
                               + " sNoteDescription,sNoteCode,  nPaymentNoteSubType, sUserName ,UsedReserve ,AvailableReserve,nCloseDate, "
                               + " nPAccountID,nAccountPatientID,nGuarantorID, AssociationPatientID,AssociationPatient,AssociationMSTTransactionID, "
                                + " AssociationnTransactionID,AssociationClaim FROM view_PatientInsCompanyReserves_V2  WITH (NOLOCK)"
                               + " WHERE AssociationPatientID IN (SELECT nPatientID FROM dbo.PA_Accounts_Patients  WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID +")";
                               

 
                }
                //oDB.Retrive_Query("SELECT InsuarnceCompanyName,nEOBPaymentID,nAmount,nPayMode,CheckNumber,nCheckAmount,nCheckDate, nPayerID, " 
                //                + " sNoteDescription,sNoteCode,  nPaymentNoteSubType, sUserName ,UsedReserve ,AvailableReserve,nCloseDate, "
                //                + " nPAccountID,nAccountPatientID,nGuarantorID, AssociationPatientID,AssociationPatient,AssociationMSTTransactionID, "
                //                 + " AssociationnTransactionID,AssociationClaim FROM view_PatientInsCompanyReserves_V2  WITH (NOLOCK)"
                //                + " WHERE AssociationPatientID IN (SELECT nPatientID FROM dbo.PA_Accounts_Patients  WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID   
                //                + " AND ( " + _nPatientID + "= 0 OR nPatientID = " + _nPatientID + "))" , out _dtReserves);

                oDB.Retrive_Query(_strQuery, out _dtReserves);
                oDB.Disconnect();
                
                return _dtReserves;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); return null; }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        public Boolean chkIsInsReserveRefundExist()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            DataTable _dtReserves = new DataTable();
            DataTable _dtRefundLog = new DataTable();
            Boolean _bIsDataExist = false;
            try
            {
                oDB.Connect(false);
                string _strQuery = "";
                if (_nPatientID > 0)
                {
                    _strQuery = " SELECT  Reserves.nCreditID AS nEOBPaymentID "
                                    + " FROM  BL_Reserve_Association WITH (NOLOCK) LEFT OUTER JOIN  Reserves  WITH (NOLOCK)"
                                    + " ON   BL_Reserve_Association.nEOBPaymentID = Reserves.nCreditID "
                                    + " WHERE (Reserves.nReserveType = 1) AND  BL_Reserve_Association.nPatientID =  " + _nPatientID;
                }
                else
                {
                    _strQuery = " SELECT  Reserves.nCreditID AS nEOBPaymentID "
                                   + " FROM  BL_Reserve_Association WITH (NOLOCK) LEFT OUTER JOIN  Reserves  WITH (NOLOCK)"
                                   + " ON   BL_Reserve_Association.nEOBPaymentID = Reserves.nCreditID "
                                   + " WHERE (Reserves.nReserveType = 1) AND  BL_Reserve_Association.nPatientID IN (SELECT nPatientID FROM dbo.PA_Accounts_Patients  WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID + ")";
                }

                oDB.Retrive_Query(_strQuery, out _dtReserves);
                oDB.Disconnect();
                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    _bIsDataExist = true;
                }
                else
                {
                    _dtRefundLog = GetPatientPaymentRefundLog(_nPatientID,_nPAccountID, gloPMGlobal.ClinicID);
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


        public DataTable GetPatientPaymentRefundLog(Int64 nPatientID, Int64 nPAccountID, Int64 nClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPaymentLog = new DataTable();

            try
            {
                string _sqlQuery = "";

                _sqlQuery = " SELECT CloseDate,Tray,Company,CheckNumber,PaymentDate,Amount,sNoteDescription,[User Name],nUserID, "
                             + " nEOBPaymentID,Status,RefundDateTime,nRefundId,nPayerID,Claim,Amount FROM view_InsuranceCompanyReFunds_V2 WITH (NOLOCK) "
                             + " where nPatientID IN (SELECT nPatientID FROM dbo.PA_Accounts_Patients WITH (NOLOCK) WHERE nPAccountID = " + nPAccountID
                             + " AND ( " + nPatientID + "= 0 OR nPatientID = " + nPatientID + "))  order by CloseDate desc";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentLog);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtPaymentLog;
        }

        public void Fill_PatientStatement(out DataSet dsPatStatement)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            dsPatStatement = new DataSet();
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Fill_PatientStatement", oParameters, out dsPatStatement);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        public bool IsPatientExclude() //ExcludeFromStament is related to account not patient
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
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
            //gloAuditTrail.gloAuditTrail.SetAppConfig(gloPMGlobal.DatabaseConnectionString);
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            dsFinanSummary = new DataSet();

            //Code added by SaiKrishna for account feature.
            DataTable dtAccPatSummary = new DataTable("Summary");
            DataSet dsAccPatSummary = new DataSet();

            try
            {

                
                decimal grandTotDecZeroThirty = 0;
                decimal grandTotdecThirtySixty = 0;
                decimal grandTotdecSixtyNinety = 0;
                decimal grandTotdecNinetyHundredTwenty = 0;
                decimal grandTotdecHundredTwentyPlus = 0;
                decimal grandTotdecTotal = 0;
                decimal decTotal = 0;

                oParameters.Clear();
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nSortBy", SortType, ParameterDirection.Input, SqlDbType.Char);
                oDB.Connect(false);
                oDB.Retrive("PA_Patient_Financial_View_Summary_V2", oParameters, out dsFinanSummary);
                oDB.Disconnect();
                dsFinanSummary.Tables[0].TableName = "Patient";
                dsFinanSummary.Tables[1].TableName = "Summary";
            
                for (int i = 0; i < dsFinanSummary.Tables["Patient"].Rows.Count; i++)
                {
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
                    dr["PatName"] = Convert.ToString(dsFinanSummary.Tables["Patient"].Rows[i]["PatientName"]);
                    dtAccPatSummary.Rows.Add(dr);

                    if (dsFinanSummary.Tables["Summary"].Rows.Count > 0)
                    {
                        DataRow[] _drPatientSummary = null;

                        _drPatientSummary = dsFinanSummary.Tables["Summary"].Select("PatientID = " + Convert.ToInt64(dsFinanSummary.Tables["Patient"].Rows[i]["PatientID"] + ""));

                        if (_drPatientSummary != null && _drPatientSummary.Length > 0)
                        {
                            foreach (DataRow _dr in _drPatientSummary)
                            {
                                decZeroThirty += Convert.ToDecimal(_dr["0-30"] == DBNull.Value ? 0 : _dr["0-30"]);
                                decThirtySixty += Convert.ToDecimal(_dr["31-60"] == DBNull.Value ? 0 : _dr["31-60"]);
                                decSixtyNinety += Convert.ToDecimal(_dr["61-90"] == DBNull.Value ? 0 : _dr["61-90"]);
                                decNinetyHundredTwenty += Convert.ToDecimal(_dr["91-120"] == DBNull.Value ? 0 : _dr["91-120"]);
                                decHundredTwentyPlus += Convert.ToDecimal(_dr["120+"] == DBNull.Value ? 0 : _dr["120+"]);
                                decTotal += Convert.ToDecimal(_dr["Total"] == DBNull.Value ? 0 : _dr["Total"]);
                                dtAccPatSummary.ImportRow(_dr);
                            }
                        }

                        dsFinanSummary.Tables["Summary"].Rows.Add();
                        //dsFinanSummary.Tables["Summary"].Rows[dsFinanSummary.Tables["Summary"].Rows.Count - 1]["patName"] = "Total :";
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
                dsAccPatSummary.Tables.Add(dtAccPatSummary);
                dsFinanSummary = dsAccPatSummary;
                
            }


            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {

                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
        }

        public void GetClaimsNCharges( out DataSet dsClaimsNCharges)
        {
            //gloAuditTrail.gloAuditTrail.SetAppConfig( gloPMGlobal.DatabaseConnectionString);
            DBLayer oDB = new DBLayer( gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            dsClaimsNCharges = new DataSet();
        //    DataTable dtTotalReserves;
        //    int cntr;

            try
            {
                //Code Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                oParameters.Add("@nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@ZeroBalFlag", IsZeroFlag, ParameterDirection.Input, SqlDbType.Bit);
                //oParameters.Add("@SortByFlag", nSort, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("PA_Patient_Financial_View_Claims_Charges_V2", oParameters, out dsClaimsNCharges);
                oDB.Disconnect();
                dsClaimsNCharges.Tables[0].TableName = "Claims_Charges";

             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        public void GetPatientReserve( out DataSet dsPatReserves)
        {
            dsPatReserves = new DataSet();
            DataTable dtTotalReserves;
         
            int cntr;

            dsPatReserves = fillgridData("PA_Patient_Financial_View_Reserve_V2");
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
                dsPatReserves.Tables["TotalReserves"].Rows[0]["nCloseDate"] = "";
                //Code Added by SaiKrishna
                dsPatReserves.Tables["TotalReserves"].Rows[0]["PatientName"] = "Total :";


            }

        }

        public void GetPatientPayment(out DataSet dsPayment)
        {
            dsPayment = new DataSet();
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            int cntr;

            dsPayment = this.fillgridData("PA_Patient_Financial_View_PatientPayment_V2");
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
                    if(dsPayment.Tables["Payments"].Rows[cntr]["Status"].ToString() != "Voided") 
                    decAvailable += Convert.ToDecimal(dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"] == DBNull.Value ? 0 : dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"]);
                }
                DataTable dtTotalPayment = new DataTable();
                dtTotalPayment = dsPayment.Tables["Payments"].Clone();
                dtTotalPayment.TableName = "TotalPayments";
                dtTotalPayment.ImportRow(dsPayment.Tables["Payments"].Rows[dsPayment.Tables["Payments"].Rows.Count - 1]);
                dsPayment.Tables.Add(dtTotalPayment);
                dsPayment.Tables["TotalPayments"].Rows[0]["nCloseDate"] = DBNull.Value;
                dsPayment.Tables["TotalPayments"].Rows[0]["sPaymentTrayDescription"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nPaymentMode"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nCheckDate"] = DBNull.Value;
                dsPayment.Tables["TotalPayments"].Rows[0]["sCheckNumber"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["nCheckAmount"] = decAvailable;
                dsPayment.Tables["TotalPayments"].Rows[0]["sNoteDescription"] = "";
                dsPayment.Tables["TotalPayments"].Rows[0]["Status"] = "";
                //Code Added by SaiKrishna for showing patientname
                dsPayment.Tables["TotalPayments"].Rows[0]["sPatientName"] = "Total :";

            }

        }


        public void GetChargeNotes(Int64 nTransactionID, Int64 nTransactionDetailID, out DataSet dtChargeNote)
        {
            dtChargeNote = new DataSet();
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameter = new DBParameters();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);   MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (oDB != null)
                    oDB.Dispose();
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        private DataSet GetTransactionID(Int64 nMstTransactionID, Int64 nMstTransactionDetailID, out DataSet dsTransaction)
        {
            dsTransaction = new DataSet();
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameter = new DBParameters();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);   MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public DataRow GetInsurancePaymentLogDetails(Int64 EOBPaymentID, Int64 TransactionID, Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);


            DataTable _dtPaymentLog = new DataTable();
            DataRow _drPaymentLog = null;

            try
            {
                string _sqlQuery = "SELECT nEOBPaymentID,nPayerID,Company,nPaymentTrayID,Tray,nUserID,sUserName,CheckNumber, "
                                 + " PaymentDate,CloseDate,Amount,DebitAmount,Remaining,nClinicID,nPaymentMode,VoidType, "
                                 + " VoidCloseDate,sNoteDescription,Status,sClaimRemittanceRefNo "
                                 + " FROM VW_Patient_Financial_View_InsuranceRemitHeader_V2  WITH (NOLOCK) where nEOBPaymentID = " + EOBPaymentID + " AND nBillingTransactionID = " + TransactionID + " AND nContactID = " + ContactID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentLog);
                oDB.Disconnect();

                if (_dtPaymentLog != null && _dtPaymentLog.Rows.Count > 0)
                {
                    _drPaymentLog = _dtPaymentLog.Rows[0];
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _drPaymentLog;
        }


        public  DataTable GetVoidedInsurancePayment(Int64 EobPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPaymentVoid = new DataTable();

            try
            {
                string _sqlQuery = "SELECT sUserName,dbo.CONVERT_TO_DATE(nVoidCloseDate) AS nVoidCloseDate,sNoteDescription FROM BL_EOBPaymentVoid_Notes WITH (NOLOCK) where nEOBPaymentID = " + EobPaymentId;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentVoid);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtPaymentVoid;
        }

        public DataTable GetEOBPaymentSummary(Int64 EOBPaymentID, Int64 EOBID)
        {
            DataTable _dtEOBPayment = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nCreditID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBID", EOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", 4, ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("BL_SELECT_EOBSummary", oParameters, out _dtEOBPayment);
                oDB.Retrive("BL_PatientFinView_SELECT_EOBSummary_V2", oParameters, out _dtEOBPayment);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                //if (_dtEOBPayment != null) { _dtEOBPayment.Dispose(); }
            }

            return _dtEOBPayment;
        }

        public DataTable GetEOBPaymentReason(Int64 nEOBID, Int64 nEOBPaymentID, Int64 nBillingTransactionID, Int64 nBillingTransactionDetailID)
        {
            DataTable _dtEOBPaymentReason = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingTransactionDetailID", nBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", 4, ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("BL_SELECT_EOBSummary", oParameters, out _dtEOBPayment);
                oDB.Retrive("BL_PatientFinView_SELECT_Reason_V2", oParameters, out _dtEOBPaymentReason);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                //if (_dtEOBPayment != null) { _dtEOBPayment.Dispose(); }
            }

            return _dtEOBPaymentReason;
        }

        public DataTable GetEOBPaymentRemark(Int64 nEOBID, Int64 nEOBPaymentID, Int64 nBillingTransactionID, Int64 nBillingTransactionDetailID, String sReasonCode)
        {
            DataTable _dtEOBPaymentRemark = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingTransactionDetailID", nBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", 4, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sReasonCode", sReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_PatientFinView_SELECT_Remark_V2", oParameters, out _dtEOBPaymentRemark);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtEOBPaymentRemark;
        }

        public Int64 VoidPatientRefund(Int64 CreditID, Int64 PatientId, string PatientName, string CloseDate, string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 refundID, string UserName, Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string sErrorMessage = string.Empty;
            bool showErrorMsg = false;
            object _retVal = null;
            try
            {
                if (CreditID > 0)
                {
                    oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);

                    oParameters.Add("@CreditVoidType", VoidTypeV2.PatientPaymentRefundVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@CreditVoidTrayDesc", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@CreditVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@Credits_DTLnEntryType", PaymentEntryTypeV2.PatientRefund.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);

                    oParameters.Add("@nRefundID", refundID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@RefundVoidNote", VoidNote.Replace("'", "''").Trim(), ParameterDirection.Input, SqlDbType.VarChar);

                    oParameters.Add("@ReserveVoidType", VoidTypeV2.PatientPaymentRefundVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@VoidUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VoidUserName", UserName.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@error_message", sErrorMessage, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                    oDB.Connect(false);
                    oDB.Execute("BL_VoidRefund_V2", oParameters, out _retVal);
                    oDB.Disconnect();
                    if (_retVal != null)
                    { sErrorMessage = Convert.ToString(_retVal); }

                }
                if (sErrorMessage != "Success")
                {
                    showErrorMsg = true;
                }
                else
                {
                    showErrorMsg = false;
                }
            }

            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), showErrorMsg); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return 0;
        }

        public Int64 GetAccountOwnerID()
        {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query(" SELECT nPatientID from PA_Accounts_Patients  WITH (NOLOCK) where ISNULL(bIsOwnAccount,0) = 1 and nPAccountID = " + _nPAccountID);
            if (Convert.ToInt64(_Result) != 0)
            { return Convert.ToInt64(_Result); }
            else
            { return 0; }
        
        }
        #endregion
    }
}
