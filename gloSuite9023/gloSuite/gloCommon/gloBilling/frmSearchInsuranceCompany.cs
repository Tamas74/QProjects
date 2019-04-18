using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSearchInsuranceCompany : Form
    {

        #region " Variable Declarations "

        private string _DatabaseConnectionString = "";
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private Int64 _InsuranceCompanyID = 0;
        private String _InsuranceCompanyName = String.Empty;
        private DialogResult _frmDlgRst = DialogResult.None;
     //   private bool _isFormLoading = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public Int64 InsuranceCompanyID
        {
            set { _InsuranceCompanyID = value; }
            get { return _InsuranceCompanyID; }
        }

        public String InsuranceCompanyName
        {
            set { _InsuranceCompanyName = value; }
            get { return _InsuranceCompanyName; }
        }

        public DialogResult FrmDlgRst
        {
            get { return _frmDlgRst; }
            set { _frmDlgRst = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSearchInsuranceCompany()
        {
            //_isFormLoading = true;

            InitializeComponent();
            
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

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

            //_isFormLoading = false;
        }

        #endregion " Constructor "

        #region " Column Contants for C1.Flex.Grid "
        
        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_INSURANCECOMPANY = 2;
        const int COL_COLUMN = 3;

        #endregion " Column Contants for C1.Flex.Grid "

        #region " Form Load "
        
        private void frmSearchInsuranceCompany_Load(object sender, EventArgs e)
        {
            //_isFormLoading = true;
            FillInsuranceCompanies("");            
            txtSearch.Focus();
            //_isFormLoading = false;
        }

        #endregion " Form Load "

        #region " Tool Strip Item Click Event "

        private void tlsbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                gloContacts.frmInsuranceCompany ofrmInsuranceCompany = new gloContacts.frmInsuranceCompany(_DatabaseConnectionString);
                ofrmInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                ofrmInsuranceCompany.ShowDialog(this);
                ofrmInsuranceCompany.Dispose();
                ofrmInsuranceCompany = null;
                FillInsuranceCompanies("");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsbtnModify_Click(object sender, EventArgs e)
        {
           // Int64 _selectedValue = 0;

            try
            {
                if (c1InsuranceCompany.RowSel > 0)
                {
                    if (c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_INSURANCECOMPANY) != null && c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_INSURANCECOMPANY).ToString() != "")
                    {
                        _InsuranceCompanyID = Convert.ToInt64(c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_ID));
                        gloContacts.frmInsuranceCompany ofrmInsuranceCompany = new gloContacts.frmInsuranceCompany(_InsuranceCompanyID, _DatabaseConnectionString);
                        ofrmInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                        ofrmInsuranceCompany.ShowDialog(this);
                        ofrmInsuranceCompany.Dispose();
                        ofrmInsuranceCompany = null;
                        FillInsuranceCompanies("");
                    }
                    else
                    {
                        MessageBox.Show("Please select insurance company.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        C1FlexGrid1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsbtnSave_Click(object sender, EventArgs e)
        {
            SetInsuranceCompany();
        }

        private void SetInsuranceCompany()
        {
            try
            {
                if (c1InsuranceCompany.RowSel > 0)
                {
                    if (c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_INSURANCECOMPANY) != null && c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_INSURANCECOMPANY).ToString() != "")
                    {
                        _InsuranceCompanyID = Convert.ToInt64(c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_ID));
                        _InsuranceCompanyName = Convert.ToString(c1InsuranceCompany.GetData(c1InsuranceCompany.RowSel, COL_INSURANCECOMPANY));
                        _frmDlgRst = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select insurance company.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        C1FlexGrid1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsbtnClose_Click(object sender, EventArgs e)
        {
            _frmDlgRst = DialogResult.Cancel;
            this.Close();
        }

        #endregion " Tool Strip Item Click Event "

        #region " Design C1 Flex Grid "

        private void DesignGrid()
        {

            try
            {
               
                // c1InsuranceCompany.Col = COL_COLUMN+1;
                c1InsuranceCompany.Cols[0].Visible = false;
                c1InsuranceCompany.Cols[1].Visible = false;
                c1InsuranceCompany.Cols[2].Visible = true;

                c1InsuranceCompany.Cols[0].Caption = "ID";
                c1InsuranceCompany.Cols[1].Caption = "Code";
                c1InsuranceCompany.Cols[2].Caption = "Insurance Company";

                int _nWidth = 0;
                _nWidth = this.Width;
                c1InsuranceCompany.Cols[2].Width = Convert.ToInt32(_nWidth * 0.98);

                c1InsuranceCompany.Cols[2].AllowEditing = false;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            { }

        }

        private void FillInsuranceCompanies(String strSearch)
        {
            gloContacts.gloContact ogloContact = new gloContacts.gloContact(_DatabaseConnectionString);
            DataTable _dtInsCompanies = null;

            try
            {
               

                //c1InsuranceCompany.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
                _dtInsCompanies = ogloContact.GetInsuranceCompanies();                                

                if (_dtInsCompanies != null)
                {
                    DataView _dv =  _dtInsCompanies.DefaultView;
                    
                    // added by pankaj on 25022010
                    // to apply sorting for description column
                    _dv.Sort = "sDescription";
                    
                    if (strSearch != "")
                    {
                        strSearch = strSearch.Trim().Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                         _dv.RowFilter = "sDescription like '" + strSearch + "%'";
                    }
                    c1InsuranceCompany.DataSource = _dv;
                    DesignGrid();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
               
                if (ogloContact != null) { ogloContact.Dispose(); }
            }
        }
        
        #endregion " Design C1 Flex Grid "

        #region " Search "

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //if (_isFormLoading == false)
            { FillInsuranceCompanies(txtSearch.Text); }
        }

        #endregion " Search "

        #region " C1 Flex Grid Events "

        private void c1InsuranceCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1InsuranceCompany.HitTest(e.X, e.Y);            
            try
            {
                if (hitInfo.Row > 0)
                {
                    if (c1InsuranceCompany.GetData(hitInfo.Row, COL_INSURANCECOMPANY) != null && c1InsuranceCompany.GetData(hitInfo.Row, COL_INSURANCECOMPANY).ToString() != "")
                    {
                        _InsuranceCompanyID = Convert.ToInt64(c1InsuranceCompany.GetData(hitInfo.Row, COL_ID));
                        _InsuranceCompanyName = Convert.ToString(c1InsuranceCompany.GetData(hitInfo.Row, COL_INSURANCECOMPANY));
                        _frmDlgRst = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1InsuranceCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SetInsuranceCompany();
            }
        }
       
        #endregion " C1 Flex Grid Events "

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                c1InsuranceCompany.Focus();
            }
        }
    }
}