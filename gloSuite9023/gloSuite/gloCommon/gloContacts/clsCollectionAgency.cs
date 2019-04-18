using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloContacts
{

    public enum CollectionFeeType
    {
        PercentofSelfPayBalance = 1,
        FlatFee = 2
    }

    public class clsCollectionAgency
    {
         private string _databaseconnectionstring = "";
         private string _MessageBoxCaption =gloGlobal.gloPMGlobal.MessageBoxCaption;
         private bool disposed = false;

         private string _Address1 = "";
         public string Address1
         {
             get { return _Address1; }
             set { _Address1 = value; }
         }

         private string _Address2 = "";
         public string Address2
         {
             get { return _Address2; }
             set { _Address2 = value; }
         }

         private string _ContactType = "";
         public string ContactType
         {
             get { return _ContactType; }
             set { _ContactType = value; }
         }


        private Int32 _nCollectionFeeType = 0;
        public Int32 nCollectionFeeType
        {
            get { return _nCollectionFeeType; }
            set { _nCollectionFeeType = value; }
        }

        private double  _SelfPayBalancePercent = 0;
        public double SelfPayBalancePercent
        {
            get { return _SelfPayBalancePercent; }
            set { _SelfPayBalancePercent = value; }
        }

        private double _FlatFee = 0;
        public double FlatFee
        {
            get { return _FlatFee; }
            set { _FlatFee = value; }
        }


        public clsCollectionAgency(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
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

        ~clsCollectionAgency()
        {
            Dispose(false);
        }

        public Int64 Add(Contact oContact)
        {
            Int64 _result = 0;
            object _intresult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                


                oDBParameters.Add("@nContactId", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sName", oContact.Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sContact", oContact.ContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine1", Address1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine2", Address2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCity", oContact.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sState", oContact.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sZip", oContact.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPhone", oContact.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sFax", oContact.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sEmail", oContact.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sURL", oContact.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sContactType", ContactType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nBadDebtFeeType", nCollectionFeeType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nPercentofSelfPayBalance", SelfPayBalancePercent, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Float);
                oDBParameters.Add("@nFlatFee", FlatFee, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Float);
                
                long result = oDB.Execute("gsp_INUP_CollectionAgancy", oDBParameters, out  _intresult);
                //

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                            if (oContact.ContactID == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Add, "Collection agency added", 0, _result, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Modify, "Collection agency modified", 0, _result, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                            }
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB.Dispose();
                oDB = null;
                _intresult = null;
            }
            return _result;
        }

        public DataTable GetCollectionAgency(long ContactId,bool isBlocked=false )
        {
            DataTable dtCollectionAgency = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nContactId", ContactId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bisActive", isBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDB.Retrive("gsp_GetCollectionAgancy", oDBParameters, out dtCollectionAgency);
                return dtCollectionAgency;
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB.Dispose();
                oDB = null;

                if (dtCollectionAgency != null)
                {
                    dtCollectionAgency.Dispose();
                    dtCollectionAgency = null;
                }
            }


        }
        public  DataTable GetCollectionAgencyforPayment()
        {
            DataTable dtCollectionAgency = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);               
                oDB.Retrive("gsp_GetCollectionAgencyforPayment", oDBParameters, out dtCollectionAgency);
                return dtCollectionAgency;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB.Dispose();
                oDB = null;

                if (dtCollectionAgency != null)
                {
                    dtCollectionAgency.Dispose();
                    dtCollectionAgency = null;
                }
            }
        }
     

        public bool DeleteCollectionAgency(long ContactId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from Contacts_mst where nContactId=" + ContactId + "";
                int isDeleted = oDB.Execute_Query(strQuery);
                if (isDeleted > 0)
                {
                    strQuery = "Delete from CollectionAgency_Charges where nContactId=" + ContactId + "";
                    isDeleted = oDB.Execute_Query(strQuery);
                    if (isDeleted > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Delete, "Collection agency deleted", 0, ContactId, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        MessageBox.Show("Collection agency deleted successfully.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error while deleting collection agency.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Error while deleting collection agency.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }

        }

        public bool ActivateCollectionAgency(long ContactId, int isActivate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            string msgActivate = "";
            string msgActivateError = "";

            try
            {
                oDB.Connect(false);
                strQuery = "Update Contacts_mst set bisBlocked=" + isActivate + " where nContactId=" + ContactId + "";
                if (isActivate == 0)
                {
                    msgActivate = "Collection agency activated successfully.";
                    msgActivateError = "Error while activating collection agency.";
                }
                else
                {
                    msgActivate = "Collection agency deactivated successfully.";
                    msgActivateError = "Error while deactivating collection agency.";
                }
                int isBlocked = oDB.Execute_Query(strQuery);
                if (isBlocked > 0)
                {
                    if (isActivate==1)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.DeActivate, "Collection agency deactivated", 0, ContactId, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Activate, "Collection agency activated", 0, ContactId, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }

                    MessageBox.Show(msgActivate, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {

                    MessageBox.Show(msgActivateError, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
                msgActivate = null;
                msgActivateError = null;
            }
        }

        public bool CheckCollectionAgency(long ContactID,string collectionagency)
        {
            bool flag = false;
            object result = null;
            //  DataSet _ds = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (!String.IsNullOrEmpty(collectionagency))
                {
                    oDB.Connect(false);
                    oParameters.Add("@sCollectionAgencyName", collectionagency, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    result = oDB.ExecuteScalar("BL_CheckForCollectionAgencyExist", oParameters);
                    if (result != null && Convert.ToInt16(result) > 0)
                    {
                        flag = true;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
                oParameters.Dispose();
                oParameters = null;
                result = null;
            }
            return flag;
        }
    }
}
