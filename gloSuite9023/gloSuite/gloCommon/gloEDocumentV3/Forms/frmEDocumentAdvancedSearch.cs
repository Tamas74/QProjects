using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEDocumentV3.Forms
{
    public partial class frmEDocumentAdvancedSearch : Form
    {
        public frmEDocumentAdvancedSearch()
        {
            InitializeComponent();
        }

        public string _ErrorMessage = "";
        string _FName;
        string _LName;
        string _Code;
        string _SSN;
        string _Phone;
        System.DateTime _DOB;
        bool _ISDOB = false;

        string _MFName;
        // Mother's first name 
        string _MLName;
        //Mother's last name 
        string _MCellNo;
        //Mother's cell no 
        string _MPhone;
        //Mother's phone 

        string _FFName;
        // Father's first name 
        string _FLName;
        //Father's last name 
        string _FCellNo;
        //Father's cell no 
        string _FPhone;
        //Father's phone 

        bool _IsGardianinfo = false;
        //flag to check if guardian search is ON 

        //20090821: Mayuri- PatientMobile, EmployersPhone field Added in Adv Search 
        string _Mobile;
        string _EMPPhone;
        //

        public string FName
        {
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

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string EMPPhone
        {
            get { return _EMPPhone; }
            set { _EMPPhone = value; }
        }


        private void frmEDocumentAdvancedSearch_Load(object sender, EventArgs e)
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
                txtCode.Text = _Code;
                txtLName.Text = _LName;
                txtFName.Text = _FName;
                txtSSN.Text = _SSN;
                txtMobile.Text = _Mobile;
                txtEMPPhone.Text = EMPPhone;
                //txt.Text = _SSN;


                if ((_Code == "" || _Code == null) & (_FName == "" || _FName == null) & (_LName == "" || _LName == null) & (_SSN == "" || _SSN == null))
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
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "
                MessageBox.Show(ex.Message, "Advanced search");
            }
        }
        
        private void tls_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "Search":
                    this.DialogResult = DialogResult.Yes;
                    PatientSearch();

                    break;
                case "Cancel":
                    this.DialogResult = DialogResult.No;
                    this.Close();
                    break;
            }

        }

        private void PatientSearch()
        {
            try
            {
                if (chkAdvSearch.Checked == true)
                {
                    _Code = txtCode.Text.Trim();
                    _LName = txtLName.Text.Trim();
                    _FName = txtFName.Text.Trim();
                    _SSN = txtSSN.Text.Trim();
                                  }
                else
                {
                    _Code = "";
                                     _LName = "";
                                      _FName = "";
                                      _SSN = "";
                   
                }
               
                if (dtpDOB.Checked == true)
                {
                    _ISDOB = true;
                    _DOB = dtpDOB.Value;
                }
                else
                {
                    _ISDOB = false;
                }

                    _Phone = txtPhone.Text.Trim();
                _Mobile = txtMobile.Text.Trim();
                _EMPPhone = txtEMPPhone.Text.Trim();

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
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "
                MessageBox.Show(ex.Message, "Advanced search");
            }
        }

        private void chkGardianInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkGardianInfo.CheckState == CheckState.Checked)
                {
                    pnlGardian.Visible = true;
                    //pnlGardian.Dock = DockStyle.Fill 
                    //pnlGardian.BringToFront() 
                    //this.Height = 516;
                    this.Height = 509;
                    //lblheader.Height + pnlGardian.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height - 10 
                    _IsGardianinfo = true;
                }
                else if (chkGardianInfo.CheckState == CheckState.Unchecked)
                {

                    pnlGardian.Visible = false;
                    //this.Height = 232;
                    this.Height = 273;
                    //lblHeader.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height 
                    //Me.Height.Height = pnlMain.Height - pnlGardian.Height 
                    _IsGardianinfo = false;
                }
            }

            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.Message, "Advanced search");
            }
        }
            
    }
}