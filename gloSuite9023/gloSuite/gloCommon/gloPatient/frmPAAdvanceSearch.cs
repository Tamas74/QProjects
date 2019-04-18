using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace gloPatient
{
    internal partial class frmPAAdvanceSearch : Form
    {
        public frmPAAdvanceSearch()
        {
            InitializeComponent();
            _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }
        string _FName = "";
        string _LName = "";
        string _Code = "";
        string _SSN = "";
        string _Phone = "";
        string _Mobile = "";
        string _EmployersPhone = "";
        System.DateTime _DOB;
        bool _ISDOB = false;
        string _MFName = "";
        string _MLName = "";
        string _MCellNo = "";
        string _MPhone = "";
        string _FFName = "";
        string _FLName = "";
        string _FCellNo = "";
        string _FPhone = "";
        bool _IsGardianinfo = false;
        private DataTable _dtPatients = new DataTable();
        public DataTable FilteredPatients
        {
            get { return _dtPatients; }
            set { _dtPatients = value; }
        }
        public string FName
        {

            // Mother's first name 
            //Mother's last name 
            //Mother's cell no 
            //Mother's phone 

            // Father's first name 
            //Father's last name 
            //Father's cell no 
            //Father's phone 

            //flag to check if guardian search is ON 


            get { return _FName; }
            set { _FName = value; }
        }

        public string LName
        {

            get { return _LName; }
            set { _LName = value; }
        }
        public string Code
        {

            get { return _Code; }
            set { _Code = value; }
        }
        public string SSN
        {

            get { return _SSN; }
            set { _SSN = value; }
        }
        public string Phone
        {

            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Mobile
        {

            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public string EmployersPhone
        {

            get { return _EmployersPhone; }
            set { _EmployersPhone = value; }
        }

        public System.DateTime DOB
        {

            get { return _DOB; }
            set { _DOB = value; }
        }
        public bool ISDOB
        {

            get { return _ISDOB; }
            set { _ISDOB = value; }
        }
        public string MotherLastName
        {
            get { return _MLName; }
            set { _MLName = value; }
        }
        public string MotherFirstName
        {
            get { return _MFName; }
            set { _MFName = value; }
        }
        public string MotherCellNo
        {
            get { return _MCellNo; }
            set { _MCellNo = value; }
        }
        public string MotherPhoneNo
        {
            get { return _MPhone; }
            set { _MPhone = value; }
        }
        public string FatherLastName
        {
            get { return _FLName; }
            set { _FLName = value; }
        }
        public string FatherFirstName
        {
            get { return _FFName; }
            set { _FFName = value; }
        }
        public string FatherCellNo
        {
            get { return _FCellNo; }
            set { _FCellNo = value; }
        }
        public string FatherPhoneNo
        {
            get { return _FPhone; }
            set { _FPhone = value; }
        }
        public bool IsGuardianinfo
        {
            get { return _IsGardianinfo; }
            set { _IsGardianinfo = value; }
        }
        //private string _MessageBoxCaption = "gloPM";
        private string _MessageBoxCaption = String.Empty;
        private string _DataBaseConnectionString = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private void frmPAAdvanceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDOB.Format = DateTimePickerFormat.Custom;
                // 
                if (_ISDOB == false)
                {

                    dtpDOB.Value = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else
                {
                    dtpDOB.Value = Convert.ToDateTime(_DOB.ToString("MM/dd/yyyy"));
                }
                dtpDOB.Checked = _ISDOB;
                txtPhone.Text = _Phone;
                txtMobile.Text = _Mobile;
                txtEmployersPhone.Text = _EmployersPhone;
                txtCode.Text = _Code;
                txtLName.Text = _LName;
                txtFName.Text = _FName;
                txtSSN.Text = _SSN;

                if (_Code == "" & _FName == "" & _LName == "" & _SSN == "")
                {
                    chkAdvSearch.Checked = false;
                    chkAdvSearch.Enabled = false;
                }
                else
                {
                    chkAdvSearch.Checked = true;
                    chkAdvSearch.Enabled = true;
                }
                chkGardianInfo_Click(sender, e);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAdvSearch.Checked == true)
                {
                    _Code = txtCode.Text.Trim();
                    _LName = txtLName.Text.Trim();
                    _FName = txtFName.Text.Trim();
                    _SSN = txtSSN.Text.Trim();
                }//if
                else
                {
                    _Code = "";
                    _LName = "";
                    _FName = "";
                    _SSN = "";
                }//else


                _Phone = txtPhone.Text.Trim();
                _Mobile = txtMobile.Text.Trim();
                _EmployersPhone = txtEmployersPhone.Text.Trim();
                if (dtpDOB.Checked == true)
                {
                    _ISDOB = true;
                    _DOB = dtpDOB.Value;
                }
                else
                {
                    _ISDOB = false;
                }

                //' Added On 20070128 
                if (_IsGardianinfo == true)
                {
                    _MFName = txtMFName.Text.Trim();
                    _MLName = txtMLName.Text.Trim();
                    _MCellNo = txtMCellNo.Text.Trim();
                    _MPhone = txtMPhone.Text.Trim();
                    _FFName = txtFFName.Text.Trim();
                    _FLName = txtFLName.Text.Trim();
                    _FCellNo = txtFCellNo.Text.Trim();
                    _FPhone = txtFPhone.Text.Trim();
                }
                else
                {
                    _MFName = "";
                    _MLName = "";
                    _MCellNo = "";
                    _MPhone = "";
                    _FFName = "";
                    _FLName = "";
                    _FCellNo = "";
                    _FPhone = "";
                }

                this.Close();

            }//try
            catch (gloDatabaseLayer.DBException DBErr)
            {
                MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _Code = "";
            _LName = "";
            _FName = "";
            _SSN = "";

            _Phone = "";
            _Mobile = "";
            _EmployersPhone = "";
            _MFName = "";
            _MLName = "";
            _MCellNo = "";
            _MPhone = "";
            _FFName = "";
            _FLName = "";
            _FCellNo = "";
            _FPhone = "";

            this.Close();
        }

        private void chkGardianInfo_Click(object sender, EventArgs e)
        {

            try
            {
                if (chkGardianInfo.CheckState == CheckState.Checked)
                {
                    pnlGuardian.Visible = true;
                    //pnlGardian.Dock = DockStyle.Fill 
                    //pnlGardian.BringToFront() 
                    this.Height = 540;
                    //lblHeader.Height + pnlGardian.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height - 10 
                    _IsGardianinfo = true;
                }
                else if (chkGardianInfo.CheckState == CheckState.Unchecked)
                {

                    pnlGuardian.Visible = false;
                    this.Height = 300;
                    //lblHeader.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height 
                    //Me.Height.Height = pnlMain.Height - pnlGardian.Height 
                    _IsGardianinfo = false;
                }
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }

        }

        private void frmPAAdvanceSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Code = _Code + "";
            _LName = _LName + "";
            _FName = _FName + "";
            _SSN = _SSN + "";

            _Phone = _Phone + "";
            _Mobile = _Mobile + "";
            _EmployersPhone = _EmployersPhone + "";
            _MFName = _MFName + "";
            _MLName = _MLName + "";
            _MCellNo = _MCellNo + "";
            _MPhone = _MPhone + "";
            _FFName = _FFName + "";
            _FLName = _FLName + "";
            _FCellNo = _FCellNo + "";
            _FPhone = _FPhone + "";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Search":
                        {
                            if (chkAdvSearch.Checked == true)
                            {
                                _Code = txtCode.Text.Trim();
                                _LName = txtLName.Text.Trim();
                                _FName = txtFName.Text.Trim();
                                _SSN = txtSSN.Text.Trim();
                            }//if
                            else
                            {
                                _Code = "";
                                _LName = "";
                                _FName = "";
                                _SSN = "";
                            }//else


                            _Phone = txtPhone.Text.Trim();
                            _Mobile = txtMobile.Text.Trim();
                            _EmployersPhone = txtEmployersPhone.Text.Trim();
                            if (dtpDOB.Checked == true)
                            {
                                _ISDOB = true;
                                _DOB = dtpDOB.Value;
                            }
                            else
                            {
                                _ISDOB = false;
                            }

                            //' Added On 20070128 
                            if (_IsGardianinfo == true)
                            {
                                //This Replace code added by Mayuri:20091127
                                //To fix Bug ID:#448-Enter mother's name with single quote ,gives an exception
                                _MFName = txtMFName.Text;
                                _MLName = txtMLName.Text;
                                _MCellNo = txtMCellNo.Text;
                                _MPhone = txtMPhone.Text;
                                _FFName = txtFFName.Text;
                                _FLName = txtFLName.Text;
                                _FCellNo = txtFCellNo.Text;
                                _FPhone = txtFPhone.Text;
                            }
                            else
                            {
                                _MFName = "";
                                _MLName = "";
                                _MCellNo = "";
                                _MPhone = "";
                                _FFName = "";
                                _FLName = "";
                                _FCellNo = "";
                                _FPhone = "";
                            }
                            _dtPatients = GetPatientID();  //Method Added by Mayuri:20100123-Get PatientID of Advanced search Patients
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                        }
                        break;
                    case "Cancel":
                        {
                            _Code = "";
                            _LName = "";
                            _FName = "";
                            _SSN = "";

                            _Phone = "";
                            _Mobile = "";
                            _EmployersPhone = "";
                            _MFName = "";
                            _MLName = "";
                            _MCellNo = "";
                            _MPhone = "";
                            _FFName = "";
                            _FLName = "";
                            _FCellNo = "";
                            _FPhone = "";

                            this.Close();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        //Added by Mayuri:20100123-Get PatientID of Advanced search Patients
        #region "Search Patients"
        public DataTable GetPatientID()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            String _strSQL = "";
            try
            {

                if (dtpDOB.Checked == false && _Phone == "" && _Mobile == "" && _EmployersPhone == "" && _MFName == "" && _MLName == "" && _MCellNo == "" && _MPhone == "" && _FFName == "" && _FLName == "" && _FPhone == "" && _FCellNo == "")
                {
                    return null;
                }
                _strSQL = "SELECT nPatientID FROM Patient WHERE";
                if (_DOB != null & dtpDOB.Checked == true)
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " CONVERT(VARCHAR,dtDOB,101) =  '" + _DOB.ToString("MM/dd/yyyy")  + "' ";
                    else
                        _strSQL = _strSQL + " AND CONVERT(VARCHAR,dtDOB,101) =  '" + _DOB.ToString("MM/dd/yyyy") + "' ";

                if (_Phone != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sPhone = '" + _Phone + "' ";
                    else
                        _strSQL = _strSQL + " AND sPhone = '" + _Phone + "' ";

                if (_Mobile != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sMobile = '" + _Mobile + "' ";
                    else
                        _strSQL = _strSQL + " AND sMobile = '" + _Mobile + "' ";

                if (_EmployersPhone != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sWorkPhone = '" + _EmployersPhone + "' ";
                    else
                        _strSQL = _strSQL + " AND sWorkPhone = '" + _EmployersPhone + "' ";

                if (_MFName != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sMother_fName = '" + _MFName.Trim().Replace("'", "''") + "' ";
                    else
                        _strSQL = _strSQL + " AND sMother_fName = '" + _MFName.Trim().Replace("'", "''") + "' ";

                if (_MLName != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sMother_lName = '" + _MLName.Trim().Replace("'", "''") + "' ";
                    else
                        _strSQL = _strSQL + " AND sMother_lName = '" + _MLName.Trim().Replace("'", "''") + "' ";

                if (_MCellNo != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sMother_Mobile = '" + _MCellNo + "' ";
                    else
                        _strSQL = _strSQL + " AND sMother_Mobile = '" + _MCellNo + "' ";

                if (_MPhone != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sMother_Phone = '" + _MPhone + "' ";
                    else
                        _strSQL = _strSQL + " AND sMother_Phone = '" + _MPhone + "' ";

                if (_FFName != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sFather_fName = '" + _FFName.Trim().Replace("'", "''") + "' ";
                    else
                        _strSQL = _strSQL + " AND sFather_fName = '" + _FFName.Trim().Replace("'", "''") + "' ";

                if (_FLName != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sFather_lName = '" + _FLName.Trim().Replace("'", "''") + "' ";
                    else
                        _strSQL = _strSQL + " AND sFather_lName = '" + _FLName.Trim().Replace("'", "''") + "' ";

                if (_FPhone != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sFather_Phone = '" + _FPhone + "' ";
                    else
                        _strSQL = _strSQL + " AND sFather_Phone = '" + _FPhone + "' ";


                if (_FCellNo != "")
                    if (_strSQL.EndsWith("WHERE") == true)
                        _strSQL = _strSQL + " sFather_Mobile = '" + _FCellNo + "' ";
                    else
                        _strSQL = _strSQL + " AND sFather_Mobile = '" + _FCellNo + "' ";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out _dtPatients);
                oDB.Disconnect();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                    return _dtPatients;
                else
                    return null;
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
                    oDB.Dispose();
                    oDB = null;
                }
            }


        }
        #endregion
        //End Code Added by Mayuri:20100123







    }//class
}//namespace