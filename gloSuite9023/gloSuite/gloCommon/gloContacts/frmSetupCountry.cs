using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace gloContacts
{
    public partial class frmSetupCountry : Form
    {

        Int64 _CountryID = 0;
        string _code = "";
        string _SubCode = "";
        string _Name = "";
        bool _IsBlocked = false;
        bool _IsSystem = false;
        string _Statelable = "";

        public frmSetupCountry()
        {
            InitializeComponent();
        }
        public frmSetupCountry(Int64 CountryID,bool IsBlocked)
        {
            InitializeComponent();
            _CountryID = CountryID;
            _IsBlocked = IsBlocked;
        }

        public Int64 CountryID
        {  get;  set;    }

        public frmSetupCountry(Int64 CountryID)
        {
            InitializeComponent();
            _CountryID = CountryID;
         
        }

        private void frmSetupCountry_Load(object sender, EventArgs e)
        {
            try
            {

                FillCountryDetails(_CountryID);
            
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            
        }
        private void ts_btnSave_Click(object sender, EventArgs e)
        {
          
            _code = txtCode.Text.Trim();
            _SubCode = txtSubCode.Text.Trim();
            _Name = txtCountryName.Text.Trim();
            _Statelable = txtStatelable.Text.Trim();

            if (ValidateData())
            {
                SaveData(_CountryID, _code, _SubCode, _Name, _IsBlocked, _Statelable);
                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country);
                this.Close();
            }
        }

        public void SaveData(Int64 CountryID, string Code, string SubCode, string Name, bool IsBlocked, string Statelable)
        { 
                Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                oDBParameters.Clear();
                object _intresult = 0;
                oDBParameters.Add("@nId", CountryID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@Code",Code, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@SubCode", SubCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Name", Name, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@bIsBlocked", IsBlocked, ParameterDirection.Input, SqlDbType.Bit); 
                oDBParameters.Add("@Statelabel", Statelable, ParameterDirection.Input, SqlDbType.VarChar);                    
                int result = oDB.Execute("CO_INUP_CountryMST", oDBParameters, out  _intresult);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                            this.CountryID= _result;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
           
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null)
                { oDBParameters.Dispose(); oDBParameters = null; }
            }

            
        
        }

        public bool ValidateData()
        {
            
            if ((txtCountryName.Text).Trim() == "")
            {
                MessageBox.Show("Country name must be entered. ", "Country", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCountryName.Focus();
                return false;
            }

            if ((txtCode.Text).Trim() == "")
            {
                MessageBox.Show("Country code must be entered. ", "Country", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                return false;
            }

            if (IsCountryPresent())
            {
                MessageBox.Show("Country name already present. ", "Country", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCountryName.Focus();
                return false;            
            } 

            return true;        
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  void FillCountryDetails(Int64 Contactid)
        {
         
            DataTable dtCountry = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);          
            oDB.Connect(false);

            try
            {
                string _strQry = "";
                _strQry = "SELECT sCode AS Code,sSubCode AS SubCode,sName AS Name,bIsBlocked AS IsBlocked,ISNULL(bIsSystem,0) AS IsSystem,sStatelabel as Statelabel FROM Contacts_Country_MST WHERE nID =  " + Contactid;
                oDB.Retrive_Query(_strQry, out dtCountry);

                if (dtCountry != null && dtCountry.Rows.Count > 0)
                {
                    _code = Convert.ToString(dtCountry.Rows[0]["Code"]);
                    _Name = Convert.ToString(dtCountry.Rows[0]["Name"]);
                    _SubCode = Convert.ToString(dtCountry.Rows[0]["SubCode"]);
                    _IsBlocked = Convert.ToBoolean(dtCountry.Rows[0]["IsBlocked"]);
                    _IsSystem = Convert.ToBoolean(dtCountry.Rows[0]["IsSystem"]);
                    _Statelable = Convert.ToString(dtCountry.Rows[0]["Statelabel"]);

                    txtCode.Text = _code;
                    txtCountryName.Text = _Name;
                    txtSubCode.Text = _SubCode;
                    txtStatelable.Text = _Statelable;

                    gloContact ogloContacts = new gloContact(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    if (_IsSystem)
                    {
                        txtCountryName.Enabled = false;
                        txtCode.Enabled = false;
                    }
                    else if (ogloContacts.IsCountryInUse(_Name.Replace("'", "''")))
                    {
                        txtCountryName.Enabled = false;
                        txtCode.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                if (dtCountry != null)
                {
                    dtCountry.Dispose();
                }
            }
           
        }

        private bool IsCountryPresent()
        {
            DataTable dtCountry = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            bool _result = false;
            try
            {
                string _strQry = "";
                _strQry = "SELECT 1 FROM Contacts_Country_MST WHERE sName =  '" + txtCountryName.Text.Replace("'","''").Trim() + "' AND nID <> " + _CountryID ;
                oDB.Retrive_Query(_strQry, out dtCountry);

                if (dtCountry != null && dtCountry.Rows.Count > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                if (dtCountry != null)
                { dtCountry.Dispose(); dtCountry = null; }
            }
            return _result;
        }

        private void txtCountryName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.txtCountryName, Convert.ToString(txtCountryName.Text));
        }
    }
}
