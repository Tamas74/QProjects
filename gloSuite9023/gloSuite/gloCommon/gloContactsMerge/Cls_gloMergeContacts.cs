using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using gloSecurity;
using System.IO;
using System.Windows.Forms;

namespace gloContactsMerge
{
    public class MergeContacts
    {
        #region "Variable Diclarations"

        string _databaseconnectionstring = string.Empty;
        private const string _encryptionKey = "12345678";
        #endregion

        #region " Constractor and Destractor "

        public MergeContacts(string sDatabaseConnectionString)
        {
            _databaseconnectionstring = sDatabaseConnectionString;
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

        ~MergeContacts()
        {
            Dispose(false);
        } 

        #endregion

        #region " Public Methods "

        public Boolean MergeInsurancePlan(Int64 nRemoveContactID, Int64 nRemainContactID, string sRemainContactName)
        {
            Boolean bReturn = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nRemoveContactID", nRemoveContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nRemainContactID", nRemainContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sContactName", sRemainContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);


                int i = oDB.Execute("gsp_MERGE_INSURANCE_PLANS", oDBParameters);

                if (i > 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                bReturn = false;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                bReturn = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return bReturn;

        }

        public bool MergeInsurancePlan(Int64 nRemoveContactID, Int64 nRemainContactID, string sRemainContactName, string sRemoveContactName, ref string sReturn)
        {
            sReturn = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nRemoveContactID", nRemoveContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nRemainContactID", nRemainContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sRemainContactName", sRemainContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRemoveContactName", sRemoveContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sErrorMsg", "", System.Data.ParameterDirection.Output, System.Data.SqlDbType.VarChar,8000);

                Hashtable htOut = oDB.Execute("gsp_MERGE_INSURANCE_PLANS", oDBParameters, true);

                if (Convert.ToString(htOut["@sErrorMsg"]) != string.Empty)
                {
                    sReturn = Convert.ToString(htOut["@sErrorMsg"]);
                }
                else
                {
                    sReturn = string.Empty;
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                sReturn = string.Empty;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                sReturn = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            if (sReturn != string.Empty) { return false; } else { return true; }

        }

        public Int64 RemoveDuplicateInsurancePlan()
        {
            Int64 nReturn = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@N_DELETE_COUNT", 0, System.Data.ParameterDirection.Output, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@B_COUNT_ONLY", false, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                Hashtable hsOutParam = oDB.Execute("gsp_REMOVE_DUPLICATE_INSURANCE", oDBParameters, true);

                nReturn = Convert.ToInt64(hsOutParam["@N_DELETE_COUNT"]);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                nReturn = 0;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                nReturn = 0;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return nReturn;

        }

        public DataTable GetDuplicatePlans()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dt = new DataTable();

            try
            {

                oDB.Connect(false);
                oDBParameters.Add("@N_DELETE_COUNT", 0, System.Data.ParameterDirection.Output, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@B_COUNT_ONLY", true, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                oDB.Retrive("gsp_REMOVE_DUPLICATE_INSURANCE", oDBParameters, out _dt);


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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _dt;
        }

        public DataTable GetContactDetails(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            DataTable _dt = new DataTable();

            try
            {

                _strSQL = " SELECT sPayerId,sAddressLine1,sAddressLine2,sCity,sState,sZIP,dbo.FormatPhone(sPhone,0) as sPhone, " +
                            " (SELECT COUNT(DISTINCT nPatientID) FROM PatientInsurance_DTL WHERE nContactID = " + nContactID + ") AS nPatientCount" +
                            " FROM Contacts_mst,Contacts_insurance_dtl" +
                            " WHERE Contacts_mst.nContactID = Contacts_insurance_dtl.nContactID" +
                            " AND Contacts_mst.nContactID = " + nContactID;

                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out _dt);
                oDB.Disconnect();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    return _dt;
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
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }
        public DataTable GetInsuranceContactList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtInsuranceList = null;
            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_GetInsuranceList_Merge", out dtInsuranceList);
                oDB.Disconnect();
                return dtInsuranceList;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtInsuranceList != null) { dtInsuranceList.Dispose(); }
            }
        }
        
        #endregion
    }
}
