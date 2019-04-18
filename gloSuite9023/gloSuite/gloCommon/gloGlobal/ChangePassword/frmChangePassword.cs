using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace gloGlobal.ChangePassword
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        string _strSQL = "";
        string pwd = "";

        bool blngenpass = false;
        string _LoginUserName = "";

        int numdigits;
        int numletters;
        int numspchars;
        int numcapletters;
        int numminlength;
        int numdays;
        bool blnPassComplexity = false;
        
        public const string constEncryptDecryptKey = "12345678";

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtConfirmPass.Enabled = false;
            clsEncryption objEncryption = new clsEncryption();
            string str = objEncryption.DecryptFromBase64String("Z95WSXmg7aCKg3hC8ryjGA==", constEncryptDecryptKey);
            objEncryption = null;
            tsbtnGenPass.Enabled = false;
            checkPasswordComplexity();
            txtChangePassword.GotFocus+=new EventHandler(txtChangePassword_GotFocus);
        }


        private bool UpdatePassword(string genPass)
        {

            if (CheckPassSetting())
            {
                SqlCommand cmd = null;
                SqlTransaction myTrans = null;


                _LoginUserName = gloPMGlobal.UserName;
                clsEncryption objEncryption = new clsEncryption();
                pwd = objEncryption.EncryptToBase64String(genPass, constEncryptDecryptKey);
                objEncryption = null;
                SqlConnection conn = new SqlConnection(gloPMGlobal.DatabaseConnectionString);
                try
                {
                    // _strSQL = "Update User_MST set sPassword='" & pwd & "' from  where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"


                    // cmd = New SqlClient.SqlCommand(_strSQL, conn)
                    conn.Open();

                    // cmd.ExecuteNonQuery()

                    myTrans = conn.BeginTransaction();

                    cmd = conn.CreateCommand();
                    cmd.Transaction = myTrans;

                    _strSQL = "update User_MST set sPassword = '" + pwd + "' , IsPasswordReset = 0 where sLoginName = '" + gloPMGlobal.UserName.Replace("'", "''") + "'";

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = _strSQL;

                    cmd.ExecuteNonQuery();


                    // MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    //make the entry of the changed passord and the creation date and time in the RecentPwd_MST table
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertUserNamePwd";

                    //add the parameters
                    SqlParameter objLoginName = new SqlParameter();
                    var _with1 = objLoginName;
                    _with1.ParameterName = "@LoginName";
                    _with1.Value = gloPMGlobal.UserName;
                    _with1.Direction = ParameterDirection.Input;
                    _with1.SqlDbType = SqlDbType.VarChar;
                    cmd.Parameters.Add(objLoginName);

                    SqlParameter objPassword = new SqlParameter();
                    var _with2 = objPassword;
                    _with2.ParameterName = "@sPassword";
                    _with2.Value = pwd;
                    _with2.Direction = ParameterDirection.Input;
                    _with2.SqlDbType = SqlDbType.VarChar;
                    cmd.Parameters.Add(objPassword);

                    SqlParameter objPwdCreationTime = new SqlParameter();
                    var _with3 = objPwdCreationTime;
                    _with3.ParameterName = "@PwdCreationDate";
                    _with3.Value = DateTime.Now;
                    _with3.Direction = ParameterDirection.Input;
                    _with3.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(objPwdCreationTime);

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    myTrans.Commit();

                    objLoginName = null;
                    objPassword = null;
                    objPwdCreationTime = null;
                    try
                    {
                        //Application.DoEvents()
                        //this.Dispose();
                        //txtChangePassword.Text = genPass;
                        //txtConfirmPass.Text = genPass;
                        txtConfirmPass.Focus();
                        this.Hide();
                    }
                    catch (Exception exdispose)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, exdispose.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            Interaction.MsgBox("An exception of type " + ex1.GetType().ToString() + "has occured while attempting to roll back the transaction.");
                            //" was encountered while attempting to roll back the transaction.")
                            //ErrorMessage = ex.Message
                        }
                    }
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                    if (myTrans != null)
                    {
                        myTrans.Dispose();
                        myTrans = null;
                    }

                    txtConfirmPass.Enabled = false;
                    txtChangePassword.Enabled = true;
                    tsbtnGenPass.Enabled = false;
                }
            }
            else
            {
                return false;
            }

        }

        public string GenRandomStr()
        {
            Guid str = default(Guid);
            string sPwdSrting = null;

            try
            {
                str = System.Guid.NewGuid();
                sPwdSrting = Strings.Mid(str.ToString(), 1, 8);

                return sPwdSrting;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
        }

        private void checkPasswordComplexity()
        {

            SqlDataAdapter oda = default(SqlDataAdapter);
            DataTable dt = default(DataTable);

            try
            {

                _strSQL = "select sSettingsValue from Settings where sSettingsName = 'PASSWORD COMPLEXITY'";
                oda = new SqlDataAdapter(_strSQL, gloPMGlobal.DatabaseConnectionString);
                dt = new DataTable();
                oda.Fill(dt);
                if ((dt != null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString().Trim() == "1")
                        {
                            blnPassComplexity = true;
                        }
                        else
                        {
                            blnPassComplexity = false;
                        }
                    }
                    dt.Dispose();
                    dt = null;
                }
                oda.Dispose();
                oda = null;



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }


            //blnPassComplexity

        }

        private void txtChangePassword_GotFocus(object sender, System.EventArgs e)
        {
            if (bIsPasswordChanged==true)
            {
                return;
            }
            if (MatchOldPassword()==false)
            {
                MessageBox.Show("Enter the Correct Password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //End If
        }

        private Boolean MatchOldPassword()
        {
            Boolean bIsPasswordMatch = false;
            SqlCommand cmd = null;
            clsEncryption objEncryption = new clsEncryption();
            SqlConnection conn = new SqlConnection(gloPMGlobal.DatabaseConnectionString);
            pwd = objEncryption.EncryptToBase64String(txtOldPass.Text.Trim(), constEncryptDecryptKey);
            objEncryption = null;
            _LoginUserName = gloPMGlobal.UserName;
            _strSQL = "select sPassword from User_MST where sLoginName = '" + _LoginUserName.Replace("'", "''") + "'";

            try
            {
                cmd = new SqlCommand(_strSQL, conn);
                conn.Open();



                dynamic obj = cmd.ExecuteScalar();
                if (pwd == obj.ToString())
                {
                    txtConfirmPass.Enabled = true;
                    txtChangePassword.Enabled = true;
                    tsbtnGenPass.Enabled = true;
                    bIsPasswordMatch = true;
                }
                else
                {
                    txtOldPass.Text = "";
                    txtChangePassword.Enabled = true;
                    txtConfirmPass.Enabled = false;
                    tsbtnGenPass.Enabled = false;
                    txtOldPass.Focus();
                    bIsPasswordMatch = false;

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                bIsPasswordMatch = false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }

            }
            return bIsPasswordMatch;
        }
        private bool CheckPassSetting()
        {
            SqlDataAdapter oda = null;
            DataTable dt = null;

            try
            {
                if (blnPassComplexity == false | blngenpass == true)
                {
                    return true;
                }
                //  conn.Open()
                _strSQL = "select ExpCapitalLetters, ExpNoOfLetters, ExpNoOfDigits, ExpNoOfSpecChars, ExpPwdLength, ExpTimeFrameinDays from PwdSettings";
                oda = new SqlDataAdapter(_strSQL, gloPMGlobal.DatabaseConnectionString);
                dt = new DataTable();
                oda.Fill(dt);

                if ((dt != null))
                {

                    if (dt.Rows.Count > 0)
                    {
                        if (!Information.IsDBNull(dt.Rows[0]["ExpCapitalLetters"]))
                        {
                            numcapletters = Convert.ToInt32(dt.Rows[0]["ExpCapitalLetters"]);
                        }
                        else
                        {
                            numcapletters = 0;
                        }
                        if (!Information.IsDBNull(dt.Rows[0]["ExpNoOfLetters"]))
                        {
                            numletters = Convert.ToInt32(dt.Rows[0]["ExpNoOfLetters"]);
                        }
                        else
                        {
                            numletters = 0;
                        }
                        if (!Information.IsDBNull(dt.Rows[0]["ExpNoOfDigits"]))
                        {
                            numdigits = Convert.ToInt32(dt.Rows[0]["ExpNoOfDigits"]);
                        }
                        else
                        {
                            numdigits = 0;
                        }
                        if (!Information.IsDBNull(dt.Rows[0]["ExpNoOfSpecChars"]))
                        {
                            numspchars = Convert.ToInt32(dt.Rows[0]["ExpNoOfSpecChars"]);
                        }
                        else
                        {
                            numspchars = 0;
                        }
                        if (!Information.IsDBNull(dt.Rows[0]["ExpPwdLength"]))
                        {
                            numminlength = Convert.ToInt32(dt.Rows[0]["ExpPwdLength"]);
                        }
                        else
                        {
                            numminlength = 0;
                        }
                        if (!Information.IsDBNull(dt.Rows[0]["ExpTimeFrameinDays"]))
                        {
                            numdays = Convert.ToInt32(dt.Rows[0]["ExpTimeFrameinDays"]);
                        }
                        else
                        {
                            numdays = 0;
                        }

                    }
                    else
                    {
                        return true;
                    }

                }

                if (ValidatePassword(txtChangePassword.Text.Trim(), numminlength, numcapletters, 0, numdigits, numspchars, null, numletters))
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                if (oda != null)
                {
                    oda.Dispose();
                    oda = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        public bool ValidatePassword(string pwd, int minLength = 8, int numUpper = 0, int numLower = 0, int numNumbers = 1, int numSpecial = 0, string[] resStrs = null, int numLetters = 1, int numofdays = 0)
        {


            try
            {



                // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
                System.Text.RegularExpressions.Regex upper = new System.Text.RegularExpressions.Regex("[A-Z]");
                System.Text.RegularExpressions.Regex lower = new System.Text.RegularExpressions.Regex("[a-z]");
                System.Text.RegularExpressions.Regex letters = new System.Text.RegularExpressions.Regex("[a-zA-Z]");
                System.Text.RegularExpressions.Regex number = new System.Text.RegularExpressions.Regex("[0-9]");
                // Special is "none of the above".
                System.Text.RegularExpressions.Regex special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");


                // Check the length.
                if (Strings.Len(pwd) < minLength)
                {
                    // MsgBox("The  length of the password  should be atleast  " & minLength)
                    MessageBox.Show("The  length of the password  should be atleast  " + minLength, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // txtPassword.Text = ""
                    return false;
                }

                // Check for minimum number of occurrences.

                if (upper.Matches(pwd).Count < numUpper)
                {
                    MessageBox.Show("The password should contain atleast " + numUpper + " upper case letter", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //  txtPassword.Text = ""
                    return false;
                }



                if (lower.Matches(pwd).Count < numLower)
                {
                    MessageBox.Show("The password should contain atleast " + numLower + " lower case letter", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // txtPassword.Text = ""
                    return false;
                }

                if (number.Matches(pwd).Count < numNumbers)
                {
                    //MsgBox("The password should contain atleast " & numNumbers & " digits")
                    MessageBox.Show("The password should contain atleast " + numNumbers + " digits", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //  txtPassword.Text = ""
                    return false;
                }

                if (special.Matches(pwd).Count < numSpecial)
                {
                    //   MsgBox("The password should contain atleast " & numSpecial & " special characters")
                    MessageBox.Show("The password should contain atleast " + numSpecial + " special characters", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                }

                //If InStr(UCase(pwd), UCase(txtUserName.Text.Trim())) Then
                //    MsgBox("The password should not contain your login name")
                //    Return False
                //End If

                if (Strings.UCase(pwd) == Strings.UCase(gloPMGlobal.UserName))
                {
                    MessageBox.Show("The password should not be same as your login name", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (letters.Matches(pwd).Count < numLetters)
                {
                    //  MsgBox("The password should contain atleast " & numLetters & " alphabet")
                    MessageBox.Show("The password should contain atleast " + numLetters + " alphabet", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                //' Check whether the pwd is one of the recent pwds
                if (GetRecentPwds(pwd))
                {
                    //   MsgBox("You have already used this password recently, so select another password")
                    MessageBox.Show("You have already used this password recently, so select another password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                }

                //' Passed all checks.
                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                Interaction.MsgBox(ex.Message);
                return false;

            }
            finally
            {
            }
        }


        public bool GetRecentPwds(string strpwd)
        {
            //if the pwd exists in the recent pwds then return true 

            //  Dim conn As New SqlConnection(gloPMGlobal.DatabaseConnectionString)
            //  Dim cmd As SqlCommand
            string _strSQL = "";
            DataTable dtRecentPwds = new DataTable();
            DataTable dtval = new DataTable();
            SqlDataAdapter da = default(SqlDataAdapter);
            SqlDataAdapter da2 = default(SqlDataAdapter);

            Collection PwdStr = new Collection();
            bool blnisexists = false;

            try
            {
                _strSQL = "select sPassword, PwdCreationDate from RecentPwd_MST where LoginName ='" + gloPMGlobal.UserName.Replace("'", "''") + "'";

                da = new SqlDataAdapter(_strSQL, gloPMGlobal.DatabaseConnectionString);
                da.Fill(dtRecentPwds);



                DateTime Pwddate = default(DateTime);

                clsEncryption objEncryption = new clsEncryption();

                for (int i = 0; i <= dtRecentPwds.Rows.Count - 1; i++)
                {
                    Int64 noofdays = 0;
                    Pwddate = Convert.ToDateTime(dtRecentPwds.Rows[i]["PwdCreationDate"]);
                    _strSQL = "SELECT DATEDIFF(day,'" + dtRecentPwds.Rows[i]["PwdCreationDate"] + "', dbo.gloGetDate()) AS no_of_days";
                    //  cmd = New SqlCommand(_strSQL, conn)
                    da2 = new SqlDataAdapter(_strSQL, gloPMGlobal.DatabaseConnectionString);
                    da2.Fill(dtval);

                    noofdays = Convert.ToInt64(dtval.Rows[0][0]);

                    if (noofdays <= 30)
                    {
                        //    If noofdays <= 15 Then
                        PwdStr.Add(objEncryption.DecryptFromBase64String(Convert.ToString(dtRecentPwds.Rows[i]["sPassword"]), constEncryptDecryptKey));
                    }
                    da2.Dispose();
                    dtval.Dispose();
                }
                objEncryption = null;
                for (int i = 1; i <= PwdStr.Count; i++)
                {
                    if (strpwd == Convert.ToString(PwdStr[i]))
                    {
                        blnisexists = true;
                    }
                }
                //MsgBox(PwdStr)
                da.Dispose();
                dtRecentPwds.Dispose();
                return blnisexists;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                Interaction.MsgBox(ex.Message);
                return false;
            }
            finally
            {
                //conn.Close()
            }

        }

        Boolean bIsPasswordChanged = false;
        private void btn_tls_Ok_Click(System.Object sender, System.EventArgs e)
        {
            if (MatchOldPassword() == false)
            {
                MessageBox.Show("Please Enter the Password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtChangePassword.Enabled == true && !string.IsNullOrEmpty(txtChangePassword.Text.Trim()))
            {
                if (txtConfirmPass.Text.Trim() == txtChangePassword.Text.Trim())
                {

                    if (CheckPassSetting())
                    {
                        _LoginUserName = gloPMGlobal.UserName;
                        clsEncryption objEncryption = new clsEncryption();
                        SqlTransaction myTrans = null;
                        SqlCommand cmd = null;
                        SqlConnection conn = new SqlConnection(gloPMGlobal.DatabaseConnectionString);
                        pwd = objEncryption.EncryptToBase64String(txtConfirmPass.Text.Trim(), constEncryptDecryptKey);
                        objEncryption = null;

                        try
                        {
                            // _strSQL = "Update User_MST set sPassword='" & pwd & "' from  where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"


                            // cmd = New SqlClient.SqlCommand(_strSQL, conn)
                            conn.Open();

                            // cmd.ExecuteNonQuery()

                            myTrans = conn.BeginTransaction();

                            cmd = conn.CreateCommand();
                            cmd.Transaction = myTrans;

                            _strSQL = "update User_MST set sPassword = '" + pwd + "' , IsPasswordReset = 0 where sLoginName = '" + gloPMGlobal.UserName.Replace("'", "''") + "'";

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = _strSQL;

                            cmd.ExecuteNonQuery();


                            //MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            //make the entry of the changed passord and the creation date and time in the RecentPwd_MST table
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "InsertUserNamePwd";

                            //add the parameters
                            SqlParameter objLoginName = new SqlParameter();
                            var _with4 = objLoginName;
                            _with4.ParameterName = "@LoginName";
                            _with4.Value = gloPMGlobal.UserName;
                            _with4.Direction = ParameterDirection.Input;
                            _with4.SqlDbType = SqlDbType.VarChar;
                            cmd.Parameters.Add(objLoginName);

                            SqlParameter objPassword = new SqlParameter();
                            var _with5 = objPassword;
                            _with5.ParameterName = "@sPassword";
                            _with5.Value = pwd;
                            _with5.Direction = ParameterDirection.Input;
                            _with5.SqlDbType = SqlDbType.VarChar;
                            cmd.Parameters.Add(objPassword);

                            SqlParameter objPwdCreationTime = new SqlParameter();
                            var _with6 = objPwdCreationTime;
                            _with6.ParameterName = "@PwdCreationDate";
                            _with6.Value = DateTime.Now;
                            _with6.Direction = ParameterDirection.Input;
                            _with6.SqlDbType = SqlDbType.DateTime;
                            cmd.Parameters.Add(objPwdCreationTime);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            myTrans.Commit();

                            conn.Close();

                            objPassword = null;
                            objLoginName = null;
                            objPwdCreationTime = null;

                            this.DialogResult = DialogResult.OK;
                            bIsPasswordChanged = true;
                            MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Change password Successful ", gloPMGlobal.UserID, 0, gloPMGlobal.LoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                myTrans.Rollback();
                            }
                            catch (SqlException ex1)
                            {
                                if ((myTrans.Connection != null))
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                    Interaction.MsgBox("An exception of type " + ex1.GetType().ToString() + "has occured while attempting to roll back the transaction.");
                                    //" was encountered while attempting to roll back the transaction.")
                                    //ErrorMessage = ex.Message
                                }
                            }

                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                            conn = null;

                            txtOldPass.Text = "";
                            txtChangePassword.Text = "";
                            txtConfirmPass.Text = "";
                            txtConfirmPass.Enabled = false;
                            txtChangePassword.Enabled = true;
                            tsbtnGenPass.Enabled = false;
                            if (cmd != null)
                            {
                                cmd.Parameters.Clear();
                                cmd.Dispose();
                                cmd = null;
                            }
                            if (myTrans != null)
                            {
                                myTrans.Dispose();
                                myTrans = null;
                            }
                        }
                    }
                    else
                    {
                        txtOldPass.Text = "";
                        txtChangePassword.Text = "";
                        txtConfirmPass.Text = "";
                        txtConfirmPass.Enabled = false;
                        txtChangePassword.Enabled = true;
                        tsbtnGenPass.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("New and ConfirmPassword should be same", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Please Enter the Password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void tsbtnGenPass_Click(System.Object sender, System.EventArgs e)
        {
            if (MatchOldPassword()==false)
            {
                MessageBox.Show("Please Enter the Password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtOldPass.Text.Trim()))
            {
                MessageBox.Show("Please Enter the Password", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string strgenpwd = null;

            try
            {
                strgenpwd = GenRandomStr();
                blngenpass = true;
                if (UpdatePassword(strgenpwd))
                {
                    MessageBox.Show("The password of the user : " + gloPMGlobal.UserName.Trim() + " has been reset to " + strgenpwd, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;

                    MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                tsbtnGenPass.Enabled = false;
                txtConfirmPass.Text = "";
                txtChangePassword.Text = "";
                txtOldPass.Text = "";
                txtOldPass.Focus();
                txtConfirmPass.Enabled = false;
                blngenpass = false;
            }
        }

        private void btn_tls_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Change password closed", gloPMGlobal.UserID, 0, gloPMGlobal.LoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
        }
    }
}
