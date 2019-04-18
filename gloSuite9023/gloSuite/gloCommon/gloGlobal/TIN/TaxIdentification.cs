using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace gloGlobal.TIN
{
    class TaxIdentification
    {
    }

   
    public class clsSelectProviderTaxID
    {
        #region "Private Variables declaration"

        string sDatabaseconnectionstring = "";
        Int64 nProviderID = 0;

        #endregion

        #region "Public enum"
        public enum TransactionType
        {
            PatientExam = 1,
            OrderAndResult = 2,
            Medication = 3,
            Prescription = 4,
            SendSecureMessageMessageID=5,
            SendSecureMessageCommDetailID = 6,
            PatientEducation=7,
            PatientPortalInvitation=8,
            PatientPortalQuickActivate=9,
            PatientPortalAllowManualAccess=10,
            ExportCCDADocument=11,
            CCDAReconciliationMedication=12,
            CCDAReconciliationMedicationAllergy = 13,
            CCDAReconciliationProblemList = 14,
            ManualReconciliationMedication = 15,
            ManualReconciliationHistory = 16,
            ManualReconciliationProblemList = 17,
            InboundHospital=18,
            InboundTrasactionOfCare = 19,
            APIAccessActivate = 20,
            PatientPortalBulkInvitations=21,
            APIAccessBulkActivates=22,
            APIAccessAllowManually=23,
            SummaryCareRecord=24
        }




        #endregion

        #region "constructor and destructor"

        public clsSelectProviderTaxID(Int64 nProviderID, string sDatabaseconnectionstring = "")
        {
            this.sDatabaseconnectionstring = sDatabaseconnectionstring;
            this.nProviderID = nProviderID;
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

        ~clsSelectProviderTaxID()
        {
            Dispose(false);
        }

        #endregion

        #region "Pubclic Methods"
        public bool getProviderTaxID(out string sTaxID, out Int64 nTaxAssociationID)
        {
            sTaxID = "";
            nTaxAssociationID = 0;
            try
            {
                DialogResult oResult = System.Windows.Forms.DialogResult.OK;
                frmSelectProviderTaxID oForm = new frmSelectProviderTaxID(this.nProviderID);
                if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count > 1)
                {
                    oForm.ShowDialog();
                    oResult = oForm.DialogResult;
                    nTaxAssociationID = oForm.nAssociationID;
                    sTaxID = oForm.sProviderTaxID;
                    oForm = null;

                }
                else if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count == 1)
                {
                    oResult = oForm.DialogResult;
                    nTaxAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows[0]["nAssociationID"]);
                    sTaxID = oForm.sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows[0]["sTIN"]);
                    oForm = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

            }
        }

        public Boolean InsertProviderTaxID(Int64 nAssociationID, Int64 nTransactionID, string sProviderTaxID, Int64 nProviderID, Int64 nLoginProviderID, TransactionType nTransactionType, string sTransactionTypeDesc = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object oResult = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nAssociationID", nAssociationID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sTaxID", sProviderTaxID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nLoginProviderID", nLoginProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionTypeCode", nTransactionType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransactionTypeDesc", Enum.GetName(typeof(TransactionType), nTransactionType), ParameterDirection.Input, SqlDbType.VarChar);
                oResult = oDB.Execute("gsp_SaveTransactionProviderTaxID", oDBParameters);
                if (oResult != null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB.Dispose();
                oDB = null;

            }
        }
      
        public static  Int64 getPatientProviderID(Int64 nPatientID)
        {
             
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            object oResult = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(nProviderID,0) AS nProviderID  FROM  dbo.Patient WHERE dbo.Patient.nPatientID=" + nPatientID + "";
                oResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oResult != null) //&& oResult !="")
                {
                    return Convert.ToInt64(oResult);
                }
                else
                    return 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

            }
        }
        #endregion


    }
}
