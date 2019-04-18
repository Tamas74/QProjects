using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Data.SqlClient;

namespace gloCommunity.Classes
{
    public class clsgloCommunityUsers
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public long InsertGCUSer(long gc_nUserId, string gc_sUserName, string gc_sPassword, bool IsStaging)
        {
            long Id = 0;

            string gc_sEnvironment = "staging";
            if(IsStaging == false)
                gc_sEnvironment = "production";

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_nUserId";
                oParamater.Value = gc_nUserId;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_sUserName";
                oParamater.Value = gc_sUserName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_sPassword";
                oParamater.Value = gc_sPassword;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_sEnvironment";
                oParamater.Value = gc_sEnvironment;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "GC_InsertgloCommunityUser";
                Id = Convert.ToInt64(oDB.GetDataValue(strQuery));
                return Id;
            }
            catch
            {
                return Id;
            }
            finally
            {
                if (oParamater != null)
                    oParamater = null;
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public long ChangeGCUSerPassword(long gc_nUserId, string gc_sUserName, string gc_sPassword)
        {
            long Id = 0;

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_nUserId";
                oParamater.Value = gc_nUserId;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_sUserName";
                oParamater.Value = gc_sUserName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@gc_sPassword";
                oParamater.Value = gc_sPassword;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "GC_ChangegloCommunityUserPwd";
                Id = Convert.ToInt64(oDB.GetDataValue(strQuery));
                return Id;
            }
            catch
            {
                return Id;
            }
            finally
            {
                if (oParamater != null)
                    oParamater = null;
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public DataTable getGCUser(long gc_nUserId, bool IsStaging)
        {
            DataTable dt = null;
            SqlDataAdapter sqladpt = null;

            string gc_sEnvironment = "staging";
            if (IsStaging == false)
                gc_sEnvironment = "production";

            try
            {
                dt = new DataTable();
                sqladpt = new SqlDataAdapter();
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);

                Cmd = new System.Data.SqlClient.SqlCommand("GC_IsExistGCUser", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@gc_nUserId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = gc_nUserId;

                objParam = Cmd.Parameters.Add("@gc_sEnvironment", SqlDbType.Char);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = gc_sEnvironment;

                sqladpt.Fill(dt);
                Conn.Close();
                if (objParam != null)
                    objParam = null;
                return dt;
                
            }
            catch //(SqlException ex)
            {
                return null;
            }
            //catch (Exception ex)
            //{
            //    return null;
            //}
            finally
            {
                Conn.Close();
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (sqladpt != null)
                {
                    sqladpt.Dispose();
                    sqladpt = null;
                }
            }
        }

        public DataTable getClinicData()
        {
            DataTable dt = null;
            SqlDataAdapter sqladpt = null;
            try
            {
                dt = new DataTable();
                sqladpt = new SqlDataAdapter();
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);

                Cmd = new System.Data.SqlClient.SqlCommand("GC_getClinic", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                //SqlParameter objParam = default(SqlParameter);

                //objParam = Cmd.Parameters.Add("@gc_nUserId", SqlDbType.BigInt);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = gc_nUserId;

                sqladpt.Fill(dt);
                Conn.Close();
                return dt;
            }
            catch //(SqlException ex)
            {
                return null;
            }
            //catch (Exception ex)
            //{
            //    return null;
            //}
            finally
            {
                Conn.Close();
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (sqladpt != null)
                {
                    sqladpt.Dispose();
                    sqladpt = null;
                }
            }
        }

        public bool CheckAuthentication()
        {
            clsEncryption oclsencryption = new clsEncryption();
            clsGetADUser oclsgloCommunity = new clsGetADUser();
            bool _IsUserClose = false;
            try
            {
                //'Added for check Login user available on SharePoint server if not then add on 20120727

                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloCommunity.Classes.clsgloCommunityUsers clsGCUSer = new gloCommunity.Classes.clsgloCommunityUsers();
                    DataTable dtUSer = clsGCUSer.getGCUser(clsGeneral.gnLoginID, clsGeneral.gblnIscommunityStaging);
                    if ((dtUSer != null) & dtUSer.Rows.Count > 0)
                    {
                        //'get gloCommunity username & password as per login id
                        string _encryptionKey = "12345678";
                        string _strEncryptPWD = oclsencryption.DecryptFromBase64String(dtUSer.Rows[0]["gc_sPassword"].ToString(), _encryptionKey);
                        gloCommunity.Classes.clsGeneral.gstrGCUserName = dtUSer.Rows[0]["gc_sUserName"].ToString();
                        gloCommunity.Classes.clsGeneral.gstrGCPassword = _strEncryptPWD;
                    }
                    else
                    {
                        //'add gloCommunity user
                        //mnuSharepoint.HideDropDown();
                        gloCommunity.Forms.frmAddGCUser frmAddUser = new gloCommunity.Forms.frmAddGCUser();
                        // this.Visible = false;
                        try
                        {
                            if (frmAddUser.ShowDialog(frmAddUser.Parent) == System.Windows.Forms.DialogResult.Cancel)
                            {
                                _IsUserClose = true;
                                //MessageBox.Show("Login User did not register to gloCommunity. Please close this form and register again", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtUSer = clsGCUSer.getGCUser(clsGeneral.gnLoginID, clsGeneral.gblnIscommunityStaging);
                                if ((dtUSer != null) & dtUSer.Rows.Count > 0)
                                {
                                    //'get gloCommunity username & password as per login id
                                    string _encryptionKey = "12345678";
                                    string _strEncryptPWD = oclsencryption.DecryptFromBase64String(dtUSer.Rows[0]["gc_sPassword"].ToString(), _encryptionKey);
                                    gloCommunity.Classes.clsGeneral.gstrGCUserName = dtUSer.Rows[0]["gc_sUserName"].ToString();
                                    gloCommunity.Classes.clsGeneral.gstrGCPassword = _strEncryptPWD;
                                }
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                        catch //(Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            if (frmAddUser != null)
                            {
                                frmAddUser.Dispose();
                                frmAddUser = null;
                            }
                            if (oclsencryption != null)
                                oclsencryption = null;
                            if (oclsgloCommunity != null)
                                oclsgloCommunity = null;
                        }

                        //'mnuSharepoint.ShowDropDown()
                    }
                }
                else
                {
                    if (oclsgloCommunity.CheckADUserEmail() == false)
                    {
                        //mnuSharepoint.HideDropDown();
                    }
                }
            }
            //'End
            catch (Exception)
            {
            }
            finally
            {
            }
            return _IsUserClose;
        }
    }
}
