using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace gloGallery
{
    public class clsICD9
    {
        private DataView dv;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _messageBoxCaption = "";
        private Int64 _clinicID;
        private string _databaseConnectionString;
        public String gstrSQLError = "Error while establishing connection with the server";

        public clsICD9()
        {
        }

        public clsICD9(string sConnectionString)
        {
            _databaseConnectionString = sConnectionString;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

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

        ~clsICD9()
        {
            Dispose(false);
        }

        public void Search(DataView dv, int colIndex, string txtSearch)
        {
            if (dv != null)
            {

                dv.RowFilter = "" + dv.Table.Columns[colIndex].ColumnName + " Like '" + txtSearch + "%'";
            }
        }

        public DataView GetAllICD(Int64 SpecialityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@id", SpecialityID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_ViewICD9", oParameters, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            DataView dv = new DataView(dt);
            return dv;
        }

        public DataTable GetAllICD9(string Speciality = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@Speciality", Speciality, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetAllICD9", oParameters, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dt;
        }
        public DataView GetDataview
        {
            //Dv = Ds.Tables("Category_Mst").DefaultView
            //Return Ds
            get { return dv; }
        }

        public bool CheckDuplicate(string ICD9Code, string description)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            object oResult = null;
            try
            {
                string _strSQL = "";
                //ICD9-CPT duplicates issue.
                _strSQL = "SELECT COUNT(nICD9ID) FROM ICD9 WHERE sICD9Code = '" + ICD9Code.Replace("'", "''") + "'";
                oDB.Connect(false);
                oResult=oDB.ExecuteScalar_Query(_strSQL);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            if (Convert.ToInt32(oResult) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDuplicate(long ICD9ID, string DrugName, Int64 SpecialtyID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Int64 rowAffected = 0;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@ID", ICD9ID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ICD9Code", DrugName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@SpecialtyID", SpecialtyID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                rowAffected=Convert.ToInt64(oDB.ExecuteScalar("gsp_CheckICD9_MST", oParameters));
                oDB.Disconnect();
                if (rowAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SelectICD9(long ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = new DataTable();
            try
            {
                oParameters.Add("@ICD9ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_ScanICD9", oParameters, out dt);
                oDB.Disconnect();
                dv = dt.DefaultView;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public DataTable GetAllSpeciality()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_FillSpecialty_MST", out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt= null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dt;
        }

        public void AddNewICD9(long ID, string ICD9Code, string Description, long SpecialtyID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@ICD9ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ICD9Code", ICD9Code, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@SpecialtyID", SpecialtyID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@Inactive", false, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Execute("gsp_InUpICD9", oParameters);
                oDB.Disconnect();
                if (ID != 0)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Modify, "ICD9 Modified", gloAuditTrail.ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Added", gloAuditTrail.ActivityOutCome.Success);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }
        //Code Added by Mayuri:20091003
        //To delete ICD9 from either ICD9Gallery  or CurrentICD9
        public void DeletICD9(long ID, bool _isSelectedICD9Gallery)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                string _strSQL = "";
                if (_isSelectedICD9Gallery == true)
                {
                    _strSQL = "delete ICD9Gallery where nICD9ID=" + ID + "";
                }
                else
                {
                    _strSQL = "delete ICD9 where nICD9ID=" + ID + "";
                }
                oDB.Connect(false);
                oDB.Execute_Query(_strSQL);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }
        public void DeleteICD9(long ID, string ICD9Code)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@id", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("gsp_DeleteICD9", oParameters);
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Delete, "ICD9 Deleted", gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SetRowFilter(string txtSearch)
        {
            string strexpr = null;
            string str = null;
            str = dv.Sort;
            str = Splittext(str);
            str = str.Substring(2);
            str = str.Substring(1, str.Length - 1);

            strexpr = "" + dv.Table.Columns[str].ColumnName + "  Like '" + txtSearch + "%'";
            dv.RowFilter = strexpr;

        }
        public void SortDataview(string strsort, string strSortOrder = "")
        {
            //DCatview.Sort = strsort
            dv.Sort = "[" + strsort + "]" + strSortOrder;
        }
        private string Splittext(string strsplittext)
        {

            string[] arrstring = null;
            try
            {

                if (!string.IsNullOrEmpty(strsplittext.Trim()))
                {
                    char splitchar = ' ';
                    arrstring = strsplittext.Split(splitchar);
                    return arrstring[0];
                }
                else
                {
                    return strsplittext;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return strsplittext;
            }
        }
    }
}
