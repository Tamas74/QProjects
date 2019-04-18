using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace gloAddress
{
    //Sandip Darade  20091009
    public partial class gloAddressControl : UserControl
    {


        #region "Constructors"

        public gloAddressControl()
        {
             
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
        }

        public gloAddressControl(string Databaseconnectionstring, bool _IsContact)
        {
            isFormLoading = true;
            
            InitializeComponent();

            _databaseconnectionstring = Databaseconnectionstring;

            cmbCountry.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCountry.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);


            fillCountry();

            #region " Retrieve Country from AppSettings "

            if (appSettings["Country"] != null)
            {
                if (appSettings["Country"] != "")
                {
                    _Country = Convert.ToString(appSettings["Country"]);

                }
                else
                {
                    _Country = "US";
                }
            }
            else
            { _Country = "US"; }

            cmbCountry.SelectedValue = _Country;
            if (_Country == "Canada")
            {
                lblState.Text = "Province :";
                Point pt = new Point(168, 57);
                lblState.Location = pt;

                txtCounty.Visible = false;
                txtCounty.Text = string.Empty;
                lblCounty.Visible = false;
                txtZip.MaxLength = 6;

            }
            pnlAddDetails.BorderStyle = BorderStyle.None;
            label1.Visible = false;
            label2.Visible = false;
            label16.Visible = false;
            if (_IsContact == true)
            {
                txtCounty.Visible = false;
                txtCounty.Text = string.Empty;
                cmbCountry.Visible = false;
                lblCounty.Visible = false;
                lbl_Country.Visible = false;
            }

            fillStates();
      

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
            isFormLoading = false;
        }
        public gloAddressControl(string Databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = Databaseconnectionstring;


            fillCountry();

            cmbCountry.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCountry.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);


            #region " Retrieve Country from AppSettings "

            if (appSettings["Country"] != null)
            {
                if (appSettings["Country"] != "")
                {
                    _Country = Convert.ToString(appSettings["Country"]);

                }
                else
                {
                    _Country = "US";
                }
            }
            else
            { _Country = "US"; }

            cmbCountry.SelectedValue = _Country;
            if (_Country == "Canada")
            {
                lblState.Text = "Province :";
                Point pt = new Point(168, 57);
                lblState.Location = pt;

                txtCounty.Visible = false;
                txtCounty.Text = string.Empty;
                lblCounty.Visible = false;
                txtZip.MaxLength = 6;


            }
            pnlAddDetails.BorderStyle = BorderStyle.None;
            label1.Visible = false;
            label2.Visible = false;
            label16.Visible = false;

          
            fillStates();
            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
        }

        #endregion "Constructors"

        #region"Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _Country = "";
        private string _MessageBoxCaption = "";
        private string _databaseconnectionstring = "";
        //private bool _bShowBorder = true;
        public bool isFormLoading = false;
        private TextBox txtBox;

        private gloZipcontrol oZipcontrol;
        //Variables added for zip control 
        private string _TempZipText;
        private bool _isZipItemSelected = false;
       // private bool isSearchControlOpen = false;
        public bool _isTextBoxLoading = false;
        private ToolTip oToolTip = new ToolTip();

        private bool _AddressModified = false;
        public bool AddressModified
        {
            get { return _AddressModified; }
            set { _AddressModified = value; }
        }

        public bool isOCRdata = false;
        //public bool IsLoadEvent = false;
        private ComboBox combo;
      //  private static gloAddressControl gloAddress;

        #endregion"Declarations"

        #region "Zip control implemented  "


        #region " ZIP Text Events "

        private void txtZip_GotFocus(object sender, System.EventArgs e)
        {
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipText = txtZip.Text.Trim();
               
                //}
            }
            catch
            {
            }
        }

        private void txtZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;                        
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void txtZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _isMessageshown = false;
                //_ZipTextType = enumZipTextType.PatientZip;
                if (e.KeyChar == Convert.ToChar(13))
                {

                    //' HITS ENTER BUTTON ''
                    if (pnlInternalControl.Visible)
                    {
                        int len;
                        if (_Country == "US")
                        {
                            len = 5;
                        }
                        else if (_Country == "CA")
                        {
                            len = 7;
                        }
                        else
                        {
                            len = 10;
                        }

                        if (txtZip.Text.Trim().Length == len && oZipcontrol.C1GridList.Row != -1)
                        {
                            oZipcontrol_ItemSelected(null, null);
                        }
                        if (txtZip.Text.Trim() != "")
                        {
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            //isSearchControlOpen = false;
                            if (pnlInternalControl.Visible == true)
                            {
                                pnlInternalControl.Visible = false;
                                if (txtAreaCode.Visible == false)
                                    txtCity.Focus();
                                else
                                    txtAreaCode.Select();
                            }
                            _isMessageshown = true;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = true;



                        }

                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {

                    //' HITS ESCAPE ''

                    //Commented By Dhruv 20100310 To check directly on the Escape Sequence.
                    //if (txtCity.Text == "" && txtCounty.Text == "" && txtZip.Text == "")
                    ////if ( txtZip.Text == "")
                    //{
                    //    _TempZipText = txtZip.Text;

                    //}
                    //Comment End----------------------------------------------------Dhruv
                    txtZip.Text = _TempZipText;
                    if (txtAreaCode.Visible == false)
                        txtCity.Focus();
                    else
                        txtAreaCode.Select();
                }
                //Sandip Darade 200090912
                //we are allowing only alphanumeric charactors for according referring the information from the link below  
                // http://www.postalcodedownload.com/
                //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
                //an alphabetic character and "N" represents a numeric character. 

                //if (!(e.KeyChar == Convert.ToChar(8)))
                //{
                //    //if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z])([0-9a-zA-Z\s]*)$") == false)
                //    //if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z\s]*)$") == false)
                //    if (Regex.IsMatch(e.KeyChar.ToString(), @"^([0-9a-zA-Z]*)$") == false)
                //    {
                //        e.Handled = true;
                //    }
                //    if ((e.KeyChar == Convert.ToChar(32)))//Allow space 
                //    {
                //        if (txtZip.Text != "")
                //        {
                //            e.Handled = false;
                //        }

                //    }
                //}

                ///Sandip Darade 20100320

                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    if ((_Country == "US"))
                    {
                        _strRegex = "^([0-9]*)$";
                    }
                    else
                    {
                        //'allow alphanumerics if country is Canada 
                        _strRegex = "^([0-9a-zA-Z]*)$";
                    }
                    if (!(e.KeyChar == Convert.ToChar(8)))
                    {

                        if (Regex.IsMatch(e.KeyChar.ToString(), _strRegex) == false)
                        {
                            e.Handled = true;
                        }
                        if ((e.KeyChar == Convert.ToChar(32)))
                        {
                            //Allow space 
                            if (!string.IsNullOrEmpty(txtZip.Text))
                            {

                                e.Handled = false;
                            }
                        }
                    }
                }


            }
            catch
            {

            }
        }

        bool _isMessageshown = false;
        public void txtZip_LostFocus(object sender, System.EventArgs e)
        {
            if (ControlClosing == true)
            { return; }
            // bool _result;
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    // txtZip.Text = _TempZipText;
                    if (txtCity.Text == "" && txtCounty.Text == "" && txtZip.Text == "")
                    {
                        //_TempZipText = txtZip.Text;
                        txtZip.Text = _TempZipText;

                    }
                    else
                    {
                        //_TempZipText = txtZip.Text;
                        if (txtZip.Text == "")
                        {
                            pnlInternalControl.Visible = false;
                            _isTextBoxLoading = false;
                            return;
                        }
                        // txtZip.Text = ZipLeadingWithZero(txtZip);
                        int len;
                        if (_Country == "US")
                        {
                            len = 5;
                        }
                        else if (_Country == "CA")
                        {
                            len = 7;
                        }
                        else
                        {
                            len = 10;
                        }



                       
                       if (_isMessageshown == true)
                        {
                            return;
                        } 
                       if (txtZip.Text.Length <= len)
                       //if (txtZip.Text.Length <= 5)
                        {
                            //(checkZip(txtZip.Text) == false)
                            if (checkZip(txtZip.Text) == false)
                            {

                                if (MessageBox.Show("" + txtZip.Text.Trim() + " is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    txtZip.Text = _TempZipText;
                                    pnlInternalControl.Visible = false;
                                    _isTextBoxLoading = false;
                                    _isMessageshown = false;
                                    //return;
                                }
                            }

                        }

                    }
                    pnlInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }

        }

        private void txtZip_TextChanged(object sender, System.EventArgs e)
        {
            //
            if (isFormLoading == true || isOCRdata == true)
            {
                return;
            }
            try
            {
                ///Sandip Darade 20100320
                //'Remove Special character 

                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    if ((_Country == "US"))
                    {
                        _strRegex = "[0-9]";
                    }
                    else
                    {
                        //'allow alphanumerics if country is Canada 

                        // Problem# : 176
                        // Reason : RegExp changed to allow space for canadian zip codes

                        //_strRegex = "^([0-9a-zA-Z])";
                        _strRegex = "^[a-zA-Z0-9\\s.\\-]+$";

                        // END
                    }

                    string strZipcode = txtZip.Text;
                    foreach (char c in strZipcode)
                    {
                        if (Regex.IsMatch(c.ToString(), _strRegex) == false)
                        {
                            strZipcode = strZipcode.Replace(c.ToString(), "");
                        }
                    }
                    txtZip.Text = strZipcode;
                    txtZip.SelectionStart = txtZip.Text.Length;
                    strZipcode = null;
                }

                

                //_ZipTextType = enumZipTextType.PatientZip;
                pnlInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlInternalControl.Visible == false)
                    {
                        if (Convert.ToString(cmbCountry.SelectedValue).ToUpper() == "US" || Convert.ToString(cmbCountry.SelectedValue).ToUpper() == "CA")
                        {
                            pnlInternalControl.Visible = true;
                            OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");

                            // Problem# : 176
                            // Reason : RegExp changed to allow space for canadian zip codes

                           // oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()));
                            oZipcontrol.FillControl(Convert.ToString(txtZip.Text));
                           // END
                        }
                    }
                    else
                    {
                        if (txtZip.Text.Trim().Length == 0)
                        {
                            pnlInternalControl.Visible = false;
                        }
                        else
                        {
                            // Problem# : 176
                            // Reason : RegExp changed to allow space for canadian zip codes

                            //oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()));
                            oZipcontrol.FillControl(Convert.ToString(txtZip.Text));
                            //END
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipText = txtZip.Text;
                if (isFormLoading == false)
                    _AddressModified = true;
            }
        }

        private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (oZipcontrol.C1GridList.Row < 0)
                {
                    return;
                }
                string _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
                string _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
                string _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
                string _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
                string _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
                string _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

                _isTextBoxLoading = true;

                txtZip.Text = _Zip;
                txtZip.Tag = _ID;
                txtCity.Text = _City;
                txtCity.Tag = _AreaCode;
                txtCounty.Text = _County;
                cmbState.Text = _State;

                _isTextBoxLoading = false;
                _isZipItemSelected = true;
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    if (txtAreaCode.Visible == false)
                        txtCity.Focus();
                    else
                        txtAreaCode.Select();
                }

                _Zip = null;_City = null;_ID = null; _County = null; _State = null;_AreaCode = null;
            
                //isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void oZipcontrol_LostFocusEvent(object sender, EventArgs e)
        {
            if (txtZip.Focused == false)
            {
                //txtZip_LostFocus(null, null);
                pnlInternalControl.Visible = false;
            }
        }


        private void oZipcontrol_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void oZipcontrol_CloseBtnClick1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.pnlInternalControl.Visible == true)
                {
                    this.pnlInternalControl.Visible = false;
                }

                //isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            _isZipItemSelected = false;
            try
            {

                if (oZipcontrol != null)
                {
                    CloseInternalControl();
                }
                oZipcontrol = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
                oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
                oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
                oZipcontrol.LostFocusEvent += new gloZipcontrol.LostFocus(oZipcontrol_LostFocusEvent);
                oZipcontrol.ControlHeader = ControlHeader;
                oZipcontrol.ShowHeader = false;

                oZipcontrol.Dock = DockStyle.Fill;
                pnlInternalControl.BringToFront();
                pnlInternalControl.Visible = true;
                pnlInternalControl.Controls.Add(oZipcontrol);



                if (!string.IsNullOrEmpty(SearchText))
                {
                    oZipcontrol.Search(SearchText, SearchColumn.Code);
                }
                oZipcontrol.Show();
                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {

            }

            //isSearchControlOpen = true;
            return _result;
        }

        private bool CloseInternalControl()
        {
            if (oZipcontrol != null)
            {

                _isTextBoxLoading = true;
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }



                if (oZipcontrol != null)
                {
                    try
                    {
                        oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                        oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;
                        oZipcontrol.LostFocusEvent -= new gloZipcontrol.LostFocus(oZipcontrol_LostFocusEvent);
                    }
                    catch
                    {
                    }
                    oZipcontrol.Dispose();
                    oZipcontrol = null;
                }


                _isTextBoxLoading = false;

            }
            return _isTextBoxLoading;
        }

        #endregion


        #endregion "Zip control implemented  "
        private bool _controlClosing = false;
        public bool ControlClosing
        {
            get { return _controlClosing; }
            set
            {
                _controlClosing = value;
                if (_controlClosing == true)
                {
                    CloseInternalControl();
                }
            }
        }

        private void fillStates()
        {
          
            try
            {
                DataTable dtStates = null;

                dtStates = gloGlobal.gloPMMasters.GetStates(); 

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "ST";
                    cmbState.SelectedIndex = -1;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                
            }

        }

        bool _isCountryComboLoading = false;

        private void fillCountry()
        {

            try
            {
                _isCountryComboLoading = true;
                DataTable dtCountry = null;
                dtCountry = gloGlobal.gloPMMasters.GetCounrty();

                if (dtCountry != null)
                {
                    DataRow dr = dtCountry.NewRow();
                    dr["sCode"] = "";
                    dr["sSubCode"] = "";
                    dr["sName"] = "";
                    dr["sStateLabel"] = "State";
                    dtCountry.Rows.InsertAt(dr, 0);
                    dtCountry.AcceptChanges();

                    cmbCountry.DataSource = dtCountry;
                    cmbCountry.DisplayMember = "sName";
                    cmbCountry.ValueMember = "sCode";

                    cmbCountry.BeginUpdate();
                    cmbCountry.SelectedIndex = -1;
                    cmbCountry.EndUpdate();
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                _isCountryComboLoading = false;
            }

        }


        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            combo = (ComboBox)sender;
            if (cmbCountry.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCountry.Items[cmbCountry.SelectedIndex])["sName"]), cmbCountry) >= cmbCountry.DropDownWidth - 20)
                {
                    string txt = Convert.ToString(((DataRowView)cmbCountry.Items[cmbCountry.SelectedIndex])["sName"]);
                    if (tooltipZip.GetToolTip(cmbCountry) != txt)
                    {
                        tooltipZip.SetToolTip(cmbCountry, txt);
                    }
                    txt = null;
                }
                else
                {
                    this.tooltipZip.SetToolTip(cmbCountry, "");
                }

            }
            DataRowView _drCountrySelectedRow = null;
            string _StateLabel = "";
            bool _bIsSystemRecord = false;

            try
            {
                if (_isCountryComboLoading == false)
                {
                    #region " Code Changes "

                    lblState.Text = "";
                    //txtZip.Text = "";
                    //cmbState.SelectedIndex = -1;

                    if (cmbCountry.SelectedItem != null)
                    {
                        _drCountrySelectedRow = ((DataRowView)cmbCountry.SelectedItem);
                        _StateLabel = "";
                        _bIsSystemRecord = false;

                        _StateLabel = Convert.ToString(_drCountrySelectedRow["sStateLabel"]);
                        if (_StateLabel.Trim() != "") { _StateLabel = _StateLabel.Trim() + " :"; }
                        else { _StateLabel = "State :"; }
                        lblState.Text = _StateLabel;

                        if (_drCountrySelectedRow["bIsSystem"] != DBNull.Value && Convert.ToString(_drCountrySelectedRow["bIsSystem"]).Trim() != "")
                        {
                            _bIsSystemRecord = Convert.ToBoolean(_drCountrySelectedRow["bIsSystem"]);
                        }
                    }
                    //if (IsLoadEvent == false)
                    //{
                    //    this.txtZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
                    //    txtZip.Text = "";
                    //    this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
                    //}
                    ////Point pt = new Point(168, 57);
                    ////lblState.Location = pt;

                    if (_drCountrySelectedRow != null)
                    {
                        if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "US" && _bIsSystemRecord == true)
                        {
                            txtCounty.Visible = true;
                            lblCounty.Visible = true;
                            txtZip.MaxLength = 5;
                            cmbState.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "US";
                            //7022Items: Home Billing
                            //check for country only for US to disply area code.
                            SetAreaCode();
                        }
                        else if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "CA" && _bIsSystemRecord == true)
                        {
                                txtCounty.Visible = false;
                                lblCounty.Visible = false;
                                txtZip.MaxLength = 7;
                                cmbState.DropDownStyle = ComboBoxStyle.DropDownList;
                                _Country = "CA";
                                //7022Items: Home Billing
                                //check for country only for US to disply area code.
                                SetAreaCode();
                        }
                        else 
                        {
                                txtCounty.Visible = false;
                                txtCounty.Text = string.Empty;
                                lblCounty.Visible = false;
                                txtZip.MaxLength = 10;
                                cmbState.DropDownStyle = ComboBoxStyle.DropDown;
                                cmbState.MaxLength = 2;
                                _Country = "";
                                //7022Items: Home Billing
                                //check for country only for US to disply area code.
                                SetAreaCode();
                        }
                    }
                    else
                    {
                        txtCounty.Visible = false;
                        txtCounty.Text = string.Empty;
                        lblCounty.Visible = false;
                        txtZip.MaxLength = 10; 
                        cmbState.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbState.MaxLength = 2;
                        _Country = "";
                        //7022Items: Home Billing
                        //check for country only for US to disply area code.
                        SetAreaCode();
                    }

                    if (isFormLoading == false)
                        _AddressModified = true;

                    #endregion " Code Changes "
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                _drCountrySelectedRow = null;
                //IsLoadEvent = false;
                _StateLabel = null;
                ClearZipCodeForUSandCanadaIfLengthExceeded();
            }

        }

        public bool checkZip(string zip)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT count(*) FROM  CSZ_MST  WHERE ZIP = '" + zip + "' ";
                Object NoOfRec = oDB.ExecuteScalar_Query(_sqlQuery);
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _result = true;
                }
                oDB.Disconnect();
                oDB.Dispose();
                return _result;

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                _result = false;
                return _result;

            }
        }

        private void txtAllTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

        private void txtAreaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) & e.KeyChar <= Convert.ToChar(57)) | e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
                //7022Items: Home Billing
                //Bug #46970: gloPM - V7022 - Home Billing - Special Character gets added to City when tried to enter in Zip code field
                //commented line to resolve Bug.
                //txtCity.Focus();
            }
        }

        //private string ZipLeadingWithZero(TextBox textbox)
        //{
        //    string _spacer = "";
        //    string zipCodeValue = "";
        //    int _zipLength = default(Int16);
        //    int y = 0;
        //    try
        //    {
        //        _zipLength = textbox.Text.Length;
        //        if (_zipLength != 5)
        //        {
        //            //' ''checking the length of the value inside the text box
        //            //For y = 0 To 5 - _zipLength
        //            for (y = _zipLength; y <= 4; y++)
        //            {
        //                //'We are only providing the 5 digit so if the lesser then 5 digit is entered then it will check and enter the '0' uptil the value is not 5 digit
        //                //'variable is decalred for the inserting the zero
        //                _spacer = _spacer + 0;
        //            }
        //        }
        //        zipCodeValue = _spacer + textbox.Text;
        //        //'after that bind the value to the txtbox.
        //        //'it will returns the functions

        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    } 
        //    return zipCodeValue;
        //}

        /// <summary>
        /// Set the Value to the Zip text box without Validating and showing the Zip Control
        /// </summary>
        /// <param name="sZipCode"></param>
        public void SetZipWithOutZipControl(String sZipCode)
        {
            this.txtZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
            try
            {
                txtZip.Text = "";
                txtZip.Text = sZipCode;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            }
        }
        
        //7022Items: Home Billing
        //check for country only for US to disply area code.
        public bool UseAreaCodeForPatient { get; set; }

        //7022Items: Home Billing
        //comman method added to disply area code.
        public void SetAreaCode()
        {
            if (UseAreaCodeForPatient)
            {
                //7022Items: Home Billing
                //check for country only for US to disply area code.
                if (_Country == "US")
                {
                    this.txtAreaCode.Visible = true;
                    this.txtArea.Visible = true;
                    this.txtZip.Size = new Size(43, 22);
                }
                else
                {
                    this.txtAreaCode.Visible = false;
                    this.txtArea.Visible = false;
                    this.txtZip.Size = new Size(88, 22);
                }
            }
        }

        //Start abhisekh 6 sept 2010
        //Disable mouse right click

        private void txtAreaCode_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                txtAreaCode.ContextMenu = null;// new ContextMenu();
                return;
            }

        }

        private void txtAreaCode_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtAreaCode_Validated(object sender, EventArgs e)
        {
            if (txtAreaCode.Text.Length > 0 && txtAreaCode.Text.Length < 4)
            {
                if (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    txtAreaCode.Select();
                    txtAreaCode.Focus();
                }
                else
                {
                    txtCity.Select();
                    txtCity.Focus();
                }
            }

        }

        private void txtCity_MouseEnter(object sender, EventArgs e)
        {
           
            txtBox = (TextBox)sender;
            if (txtBox.Text != null && txtBox.Text != string.Empty)
            {  
                
                if (getWidthofListItems(txtCity.Text, txtCity) >= txtCity.Width)
                {
                  
                    C1SuperTooltip1.SetToolTip(txtCity,txtCity.Text);
                }
                else
                {
                    C1SuperTooltip1.SetToolTip(txtCity, "");
                }
            }
        }

        private Double getWidthofListItems(string _text, TextBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            Double width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = s.Width;
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void cmbCountry_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbCountry.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCountry.Items[cmbCountry.SelectedIndex])["sName"]), cmbCountry) >= cmbCountry.DropDownWidth - 20)
                {
                    string txt = Convert.ToString(((DataRowView)cmbCountry.Items[cmbCountry.SelectedIndex])["sName"]);
                    if (tooltipZip.GetToolTip(cmbCountry) != txt)
                    {
                        tooltipZip.SetToolTip(cmbCountry, txt);
                    }
                }
                else
                {
                    this.tooltipZip.SetToolTip(cmbCountry, "");
                }

            }
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            combo = (ComboBox)sender;
            if (combo.Items.Count > 0 && e.Index >= 0)
            {

                e.DrawBackground();
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if (combo.DroppedDown)
                    {
                        string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 30)
                        {
                            if (tooltipZip.GetToolTip(combo) != txt)
                            {
                                this.tooltipZip.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 0, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.tooltipZip.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.tooltipZip.Hide(combo);
                    }
                }
                else
                {
                   
                }
                e.DrawFocusRectangle();
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void cmbCountry_EnabledChanged(object sender, EventArgs e)
        {
            if (!cmbCountry.Enabled)
            {
                cmbCountry.BackColor = System.Drawing.Color.WhiteSmoke;
                cmbCountry.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                cmbCountry.BackColor = System.Drawing.Color.White;
                cmbCountry.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void ClearZipCodeForUSandCanadaIfLengthExceeded()
        {
            this.txtZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
            if (Convert.ToString(cmbCountry.SelectedValue) == "US")
            {
                if (Convert.ToString(txtZip.Text).Trim().Length > 5)
                {
                    txtZip.Text = "";
                }
            }
            else if (Convert.ToString(cmbCountry.SelectedValue) == "CA")
            {
                if (Convert.ToString(txtZip.Text).Trim().Length > 7)
                {
                    txtZip.Text = "";
                }
            }
            this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
        }

        public void ClearZipCode()
        {
            this.txtZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
            txtZip.Text = "";
            this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
        }

        public void ClearaddressControl()
        {
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtCounty.Text = "";
            cmbState.Text = "";
            ClearZipCode();
        }
    }


}
