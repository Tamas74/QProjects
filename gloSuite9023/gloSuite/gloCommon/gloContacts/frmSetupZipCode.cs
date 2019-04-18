using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace gloContacts
{
    public partial class frmSetupZipCode : Form
    {
        private string _databaseconnectionstring = String.Empty;

        private string _MessageBoxCaption = String.Empty;

        Int64 _ClinicID = 0;

        Int64 _ZIPID = 0;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmSetupZipCode(Int64 ZipID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
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

            _ZIPID = ZipID;
        }

        private void FillData(Int64 ZipID)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable dtZip = null;
            try
            {
                //dtZip = new DataTable();
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string _strquery = "Select City, ST, ZIP, AreaCode, County, nID from CSZ_MST where nID='" + ZipID + "'  ";
                ODB.Retrive_Query(_strquery, out dtZip);
                if (dtZip != null && dtZip.Rows.Count > 0)
                {
                    txtZIPCode.Text = Convert.ToString(dtZip.Rows[0]["ZIP"]);
                    txtCity.Text = Convert.ToString(dtZip.Rows[0]["City"]);
                    cmbState.Text = Convert.ToString(dtZip.Rows[0]["ST"]);
                    txtCounty.Text = Convert.ToString(dtZip.Rows[0]["County"]);
                    txtAreacode.Text = Convert.ToString(dtZip.Rows[0]["AreaCode"]);
                    txtZIPCode.Tag = Convert.ToString(dtZip.Rows[0]["nID"]);
                    //txtCounty.ReadOnly = true;
                    txtCounty.BackColor = Color.White;
                    //txtAreacode.ReadOnly = true;
                    txtAreacode.BackColor = Color.White;
                }
                _strquery = null;
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
                if (dtZip != null)
                {
                    dtZip.Dispose();
                }
            }
        }

        private void fillstatecombo()
        {
            //bool _result = false;
            
            DataTable dtZip = null;
            try
            {
               // dtZip = new DataTable();
                
                dtZip = gloGlobal.gloPMMasters.GetStates();
 
                cmbState.Items.Clear();
                //cmbState.Items.Insert(0, "");
                if (dtZip != null && dtZip.Rows.Count > 0)
                {
                    cmbState.DataSource = dtZip;
                    cmbState.DisplayMember = dtZip.Columns[0].ColumnName;
                    cmbState.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
               
                if (dtZip != null)
                {
                    //dtZip.Dispose();
                }
            }
        }

        //private ZipCode GetData()
        //{
        //    ZipCode OBJZip = new ZipCode();
        //    try
        //    {
        //        OBJZip.city = txtCity.Text;
        //        OBJZip.code = txtZIPCode.Text;
        //        OBJZip.country = txtCounty.Text;
        //        OBJZip.state = cmbState.Text;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        if(OBJZip!=null)
        //        {
        //            OBJZip.Dispose();
        //        }
        //    }
        //    return OBJZip;
        //}

        public bool IsrecordPresent(Int64 _Id, string _City, string _State, string _ZIPCode, string _County, string _Areacode)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable dtZip = null;
            try
            {
                _City = _City.Replace("'", "''");
                _State = _State.Replace("'", "''");
                _County = _County.Replace("'", "''");
                string _strquery = "Select nID FROM CSZ_MST WHERE City='" + _City + "' and ST = '" + _State + "' AND Zip = '" + _ZIPCode + "' and  county ='" + _County + "' AND AreaCode = '" + _Areacode + "' and nID<>'" + _Id + "'";
                //dtZip = new DataTable();
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_strquery, out dtZip);
                if (dtZip != null && dtZip.Rows.Count > 0)
                {
                    _result = true;
                }
                ODB.Disconnect();
            }
            catch 
            {
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
                if (dtZip != null)
                {
                    dtZip.Dispose();
                }
            }
            return _result;

        }

        private Int64 UpdateZipCode(Int64 _ZIPCode)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer ODB = null;
            //DataTable dtZip = null;
            string _query = "";

            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                _query = "Update CSZ_MST set City='" + txtCity.Text.Replace("'", "''") + "', ST='" + cmbState.Text.Replace("'", "''") + "',ZIP='" + txtZIPCode.Text.Replace("'", "''") + "',AreaCode='" + txtAreacode.Text.Replace("'", "''") + "', County='" + txtCounty.Text.Replace("'", "''") + "' where nID='" + Convert.ToInt64(_ZIPCode) + "'";
                _result = ODB.Execute_Query(_query);
                
            }
            catch 
            {
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
                _query = null;
            }
            return _result;
        }

        private Int64 AddZipCode()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer ODB = null;
            //DataTable dtZip = null;
            try
            {
                string _strquery = "SELECT MAX(ISNULL(nID,0)) + 1 as nID From csz_mst ";
                DataTable dtID = null;
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_strquery, out dtID);
                if (dtID != null && dtID.Rows.Count > 0)
                {
                    Int64 _tempID = Convert.ToInt64(dtID.Rows[0]["nID"]);
                    _strquery = "";
                    _strquery = "insert into CSZ_MST (City, ST, ZIP, AreaCode, County, nID) values ('" + txtCity.Text.Replace("'", "''") + "','" + cmbState.Text.Replace("'", "''") + "','" + txtZIPCode.Text.Replace("'", "''") + "','" + txtAreacode.Text.Replace("'", "''") + "','" + txtCounty.Text.Replace("'", "''") + "','" + _tempID + "')";
                    //dtZip = new DataTable();
                    //ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    //ODB.Connect(false);
                    int ii = ODB.Execute_Query(_strquery);
                    if (ii > 0)
                    {
                        _result = ii;
                    }
                }
                _strquery = null;
                ODB.Disconnect();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                }
                //if (dtZip != null)
                //{
                //    dtZip.Dispose();
                //}
            }
            return _result;
        }

        private bool ValidateData()
        {
            if ((txtZIPCode.Text).Trim() == "")
            {
                MessageBox.Show("ZIP code must be entered. ", "ZIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtZIPCode.Focus();
                return false;
            }

            if ((txtCity.Text).Trim() == "")
            {
                MessageBox.Show("ZIP Code must have City. ", "ZIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCity.Focus();
                return false;
            }
            if ((cmbState.Text).Trim() == "")
            {
                MessageBox.Show("ZIP Code must have State. ", "ZIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbState.Focus();
                return false;
            }
            return true;
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                Int64 _result = 0;
                Int64 Areacode = 0;
                if (txtAreacode.Text == "")
                {
                    Areacode = 0;
                }
                else
                {
                    Int64 _Areacode = 0;
                    if (Int64.TryParse(txtAreacode.Text.ToString(), out _Areacode))
                    {
                        Areacode = Convert.ToInt64(txtAreacode.Text);
                    }
                    else
                    {
                        MessageBox.Show("Enter valid area code. ", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        txtAreacode.Focus(); 
                        return; 
                    }

                }

                if (IsrecordPresent(_ZIPID, txtCity.Text, cmbState.Text, txtZIPCode.Text, txtCounty.Text, Areacode.ToString()) == true)
                {
                    MessageBox.Show("Zip Code with same city is already present. ", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    //ZipCode OBJZip = new ZipCode();
                    //OBJZip=GetData();
                   // if (_ZIPID != null)
                    {
                        if (Convert.ToInt64(_ZIPID) > 0)
                        {
                            _result = UpdateZipCode(Convert.ToInt64(_ZIPID));
                        }

                        else
                        {
                            _result = AddZipCode();
                        }
                    }
                }

                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.States);  

                if (_result > 0)
                {
                    this.Close();
                }
            }


        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetupZipCode_Load(object sender, EventArgs e)
        {
            fillstatecombo();
            FillData(_ZIPID);
        }

        private void txtAreacode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {

                e.Handled = true;
            }

        }

        private void ts_btnSaveOnly_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                Int64 _result = 0;
                Int64 Areacode = 0;
                if (txtAreacode.Text == "")
                {
                    Areacode = 0;
                }
                else
                {
                    Areacode = Convert.ToInt64(txtAreacode.Text);
                }

                if (IsrecordPresent(_ZIPID, txtCity.Text, cmbState.Text, txtZIPCode.Text, txtCounty.Text, Areacode.ToString()) == true)
                {
                    MessageBox.Show("Zip Code with same city is already present", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    //ZipCode OBJZip = new ZipCode();
                    //OBJZip=GetData();
                   // if (_ZIPID != null)
                    {
                        if (Convert.ToInt64(_ZIPID) > 0)
                        {
                            _result = UpdateZipCode(Convert.ToInt64(_ZIPID));
                        }

                        else
                        {
                            _result = AddZipCode();
                        }
                    }
                }
                if (_result > 0)
                {
                    _ZIPID = 0;
                    txtCity.Text = "";
                    txtZIPCode.Text = "";
                    txtCounty.Text = "";
                    txtAreacode.Text = "";

                    txtCity.Enabled = true;
                    txtZIPCode.Enabled = true;
                    txtCounty.Enabled = true;
                    txtAreacode.Enabled = true;

                }
            }

        }

        private void txtZIPCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Added by Mayuri:20091130
            //To fix issue:#393
            //we are allowing only alphanumeric characters for according referring the information from the link below  
            // http://www.postalcodedownload.com/
            //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
            //an alphabetic character and "N" represents a numeric character. 

            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                //if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z])([0-9a-zA-Z\s]*)$") == false)
                //if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z\s]*)$") == false)
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z]*)$") == false)
                {
                    e.Handled = true;
                }
                if ((e.KeyChar == Convert.ToChar(32)))//Allow space 
                {
                    if (txtZIPCode.Text != "")
                    {
                        e.Handled = false;
                    }

                }
            }
            //End code Added by Mayuri:20091130
        }


    }
}