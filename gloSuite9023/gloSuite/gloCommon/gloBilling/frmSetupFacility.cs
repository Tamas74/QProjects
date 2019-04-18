using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloAddress;
using gloSettings;

namespace gloBilling
{
    public partial class frmSetupFacility : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _FacilityID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        gloAddress.gloAddressControl oAddresscontrol;
        //Added By Pramod Nair For Filling The States 20090716
        private String sState = "";
        private Int64 _UserID = 0;
        private bool bValidationFailed = false;
        public gloAddress.gloAddressControl oPLAddressContol;
        private bool bParentTrigger = true;
        private bool bChildTrigger = true;
        gloListControl.gloListControl oListControl;
     //   private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private String _sMammogramCertNumber = "";
        #endregion " Declarations "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 FacilityID
        {
            get { return _FacilityID; }
            set { _FacilityID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Boolean ValidationFailed
        {
            get { return bValidationFailed; }
            set { bValidationFailed = value; }
        }

        public String sMammogramCertNumber 
        {
            get { return _sMammogramCertNumber; }
            set { _sMammogramCertNumber  = value; }


        }
        #endregion 

        #region " Constructor "

        public frmSetupFacility(Int64 FacilityID,string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _FacilityID = FacilityID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

          
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupFacility_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
            tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

            try
            {
                oPLAddressContol = new gloAddressControl(_databaseconnectionstring);
                oPLAddressContol.Dock = DockStyle.Fill;
                oPLAddressContol.txtAreaCode.Visible = true;
                oPLAddressContol.txtArea.Visible = true;
                oPLAddressContol.txtZip.Size = new Size(43, 22);
                pnlPLAddresssControl.Controls.Add(oPLAddressContol);

                oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
                oAddresscontrol.Dock = DockStyle.Fill;
                oAddresscontrol.txtAreaCode.Visible = true;
                oAddresscontrol.txtArea.Visible = true;
                oAddresscontrol.txtZip.Size = new Size(43, 22);
                pnlAddresControl.Controls.Add(oAddresscontrol);
                cmbFacilityType.Items.Add("");
                cmbFacilityType.Items.Add(FacilityType.Facility.ToString());
                cmbFacilityType.Items.Add(FacilityType.NonFacility.ToString());

                FillPOS();
                FillFacility(FacilityID);
                AddLocationTree();

                fillStates(sState);
                DesignFacilityGrid();
                FillFacilityQF();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            { 
            
            }
        }

        //7022Items: Home Billing
        private void cmbPOS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Data.DataRowView rowView = (System.Data.DataRowView)(cmbPOS.SelectedItem);
            if (rowView != null)
            {
                if (Convert.ToString(rowView["sPOSCode"]) == "12")
                {
                    chkReportPatientAddress.Checked = true;
                }
                else
                {
                    chkReportPatientAddress.Checked = false;
                }
            }
            rowView = null;
        }
        #endregion 

        #region " Tool Strip Events "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


            Int64 _tempResult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {

                            if (Validate())
                            {
                                gloFacility ogloFacility = new gloFacility(_databaseconnectionstring); 
                                ////Check if the Facility already exists if yes dont add.
                                //if ((ogloFacility.IsExistsFacility(this.FacilityID, txtFacilityCode.Text, txtFacilityName.Text)))
                                //{
                                //    MessageBox.Show("The Facility Code or Name entered already exists", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}

                                if (FacilityID > 0)
                                {
                                    //Modify

                                    _tempResult = ogloFacility.AddModify(GetFacility());
                                    //Adding facility for no of locations selected
                                   //for (int i = 0; i < trv_Locations.SelectedNode.Nodes.Count-1; i++)
                                    //{
                                      
                                    //}

                                    if (_tempResult > 0)
                                    {
                                        FacilityID = _tempResult;
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        SaveFacility_Location();
                                        SaveFacilityQualifier();
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Success);
                                    }
                                    else 
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Failure);
                                    }

                                }
                                else
                                {
                                    //Add New
                                    _tempResult = ogloFacility.AddModify(GetFacility());
                                    _FacilityID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        FacilityID = _tempResult;
                                        SaveFacility_Location();
                                        SaveFacilityQualifier();
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Failure);
                                    }

                                }

                                //Clear Facility Cache if Facility is added or modified
                                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Facilities); 

                                this.Close();
                            }
                            
                        }//Case "OK"
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        {
                            if (Validate())
                            {
                                gloFacility ogloFacility = new gloFacility(_databaseconnectionstring);
                                ////Check if the Facility already exists if yes dont add.
                                //if ((ogloFacility.IsExistsFacility(this.FacilityID, txtFacilityCode.Text, txtFacilityName.Text)))
                                //{
                                //    MessageBox.Show("The Facility Code or Name entered already exists", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}

                                if (FacilityID > 0)
                                {
                                    //Modify

                                    _tempResult = ogloFacility.AddModify(GetFacility());
                                    
                                    //Adding facility for no of locations selected
                                    //for (int i = 0; i < trv_Locations.SelectedNode.Nodes.Count-1; i++)
                                    //{

                                    //}

                                    if (_tempResult > 0)
                                    {
                                        FacilityID = _tempResult;
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        SaveFacility_Location();
                                        SaveFacilityQualifier();
                                        _FacilityID = 0;
                                        txtFacilityName.Text="";
                                        txtNPI.Text="";
                                        //txtMedicadID.Text="";
                                        //txtBlueShieldID.Text="";
                                        //txtMedicareID.Text="";
                                        txtTaxID.Text="";
                                        cmbPOS.SelectedIndex=-1;
                                        //txtAddressLine1.Text="";
                                        //txtAddressLine2.Text="";
                                        //txtCity.Text="";
                                        //cmbState.SelectedIndex=-1;
                                        //txtZip.Text="";
                                        oAddresscontrol.txtAddress1.Text = "";
                                        oAddresscontrol.txtAddress2.Text = "";
                                        oAddresscontrol.txtCity.Text = "";
                                        oAddresscontrol.cmbState.SelectedIndex = -1;
                                        oAddresscontrol.txtZip.Text = "";
                                        oAddresscontrol.txtCounty.Text = "";
                                        oAddresscontrol.txtAreaCode.Text = "";
                                        txtPhoneNo.Text="";
                                        txtFax.Text="";
                                        cmbFacilityType.SelectedIndex=-1;
                                        txtFacilityCLIANo.Text="";
                                        txtMammogramCertNo.Text = "";
                                        txtTaxonomy.Text = "";
                                        foreach(TreeNode trvnode in trv_Locations.Nodes)
                                        {
                                            trvnode.Checked = false;
                                        }
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Failure);
                                    }

                                }
                                else
                                {
                                    //Add New
                                    _tempResult = ogloFacility.AddModify(GetFacility());
                                    _FacilityID = _tempResult;
                                    
                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        FacilityID = _tempResult;
                                        SaveFacility_Location();
                                        SaveFacilityQualifier();
                                        _FacilityID = 0;
                                        txtFacilityName.Text = "";
                                        txtNPI.Text = "";
                                        //txtMedicadID.Text = "";
                                        //txtBlueShieldID.Text = "";
                                        //txtMedicareID.Text = "";
                                        txtTaxID.Text = "";
                                        cmbPOS.SelectedIndex = -1;
                                        //txtAddressLine1.Text = "";
                                        //txtAddressLine2.Text = "";
                                        //txtCity.Text = "";
                                        //cmbState.SelectedIndex = -1;
                                        //txtZip.Text = "";
                                        oAddresscontrol.txtAddress1.Text = "";
                                        oAddresscontrol.txtAddress2.Text = "";
                                        oAddresscontrol.txtCity.Text = "";
                                        oAddresscontrol.cmbState.SelectedIndex = -1;
                                        oAddresscontrol.ClearZipCode();
                                        oAddresscontrol.txtAreaCode.Text = "";
                                        oAddresscontrol.txtCounty.Text = "";
                                        txtPhoneNo.Text = "";
                                        txtFax.Text = "";
                                        cmbFacilityType.SelectedIndex = -1;
                                        txtFacilityCLIANo.Text = "";
                                        txtMammogramCertNo.Text = "";
                                        txtTaxonomy.Text = "";
                                        foreach (TreeNode trvnode in trv_Locations.Nodes)
                                        {
                                            trvnode.Checked = false;
                                        }
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Failure);
                                    }

                                }


                                //Clear Facility Cache if Facility is added or modified
                                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Facilities); 

                            }
                            
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Facility, ActivityType.Add, "Add Facility", 0, FacilityID, 0, ActivityOutCome.Failure);


            }

        }

        #endregion " Tool Strip Events "

        #region Column Contants for C1.Flex.Grid

        // To Define Column Constant for Insurance Grid
        const int COL_SETTINGID = 0;
        const int COL_SETTINCODE = 1;
        const int COL_SETTINGNAME = 2;
        const int COL_SETTINGVALUE = 3;
        const int COL_BISSYSTEM = 4;
        const int CO_COUNT = 5;

        #endregion

        #region " Design Grid Methods"

        private void DesignFacilityGrid()
        {

            try
            {
                
                c1FacilityQF.Cols.Count = 5;
                c1FacilityQF.Rows.Count = 1;
                c1FacilityQF.Rows.Fixed = 1;

               
                c1FacilityQF.SetData(0, COL_SETTINGID, "");
                c1FacilityQF.SetData(0, COL_SETTINCODE, "Code");
                c1FacilityQF.SetData(0, COL_SETTINGNAME, "ID");
                c1FacilityQF.SetData(0, COL_SETTINGVALUE, "Value");
                c1FacilityQF.SetData(0, COL_BISSYSTEM, "Value");

                c1FacilityQF.Cols[COL_SETTINGID].AllowEditing = false;
                c1FacilityQF.Cols[COL_SETTINCODE].AllowEditing = false;
                c1FacilityQF.Cols[COL_SETTINGNAME].AllowEditing = false;
                c1FacilityQF.Cols[COL_SETTINGVALUE].AllowEditing = true;
                c1FacilityQF.Cols[COL_BISSYSTEM].AllowEditing = false;

                
                c1FacilityQF.Cols[COL_SETTINGID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FacilityQF.Cols[COL_SETTINCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FacilityQF.Cols[COL_SETTINGNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FacilityQF.Cols[COL_SETTINGVALUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
              
                c1FacilityQF.Cols[COL_SETTINGID].Width = 0;
                c1FacilityQF.Cols[COL_SETTINCODE].Width = 100;
                c1FacilityQF.Cols[COL_SETTINGNAME].Width = 350;
                c1FacilityQF.Cols[COL_SETTINGVALUE].Width = 100;
                c1FacilityQF.Cols[COL_BISSYSTEM].Width = 0;

                c1FacilityQF.Cols[COL_SETTINGID].Visible = false;
                c1FacilityQF.Cols[COL_SETTINCODE].Visible = false;
                c1FacilityQF.Cols[COL_SETTINGNAME].Visible = true;
                c1FacilityQF.Cols[COL_SETTINGVALUE].Visible = true;
                c1FacilityQF.Cols[COL_BISSYSTEM].Visible = false;

                c1FacilityQF.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                c1FacilityQF.ExtendLastCol = true;
                c1FacilityQF.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                c1FacilityQF.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }


        }
        
        #endregion

        #region " Private & Public Methods "

        private void FillFacility(Int64 FacilityId)
        {
            Facility oFacility = new Facility();
            gloFacility ogloFacility = new gloFacility(_databaseconnectionstring);

            try
            {
                if (FacilityId > 0)
                {
                    oFacility = ogloFacility.GetFacility(FacilityId);
                    if (oFacility != null)
                    {
                        txtFacilityCode.Text = oFacility.FacilityCode;
                        txtFacilityCode.Tag = oFacility.FacilityID;
                        txtFacilityName.Text = oFacility.FacilityName;
                        txtNPI.Text = oFacility.NPI;                   
                        //txtMedicadID.Text = oFacility.MedicadID;
                        //txtBlueShieldID.Text = oFacility.BlueShieldID;
                        //txtMedicareID.Text = oFacility.MedicareID;
                        txtTaxID.Text = oFacility.TaxID;
                        //7022Items: Home Billing
                        //cmbPOS.Text = oFacility.POS;
                        cmbPOS.SelectedValue = oFacility.POSID;
                        if (oFacility.ReportPatientAddress)
                        {   chkReportPatientAddress.Checked = true;   }
                        else
                        {   chkReportPatientAddress.Checked = false;  }

                        //txtAddressLine1.Text = oFacility.Address1;
                        //txtAddressLine2.Text = oFacility.Address2;
                        //txtZip.Text = oFacility.Zip;
                        //txtCity.Text = oFacility.City;
                        ////txtState.Text = oFacility.State;
                        //sState = oFacility.State;
                        oAddresscontrol.txtAddress1.Text = oFacility.Address1;
                        oAddresscontrol.txtAddress2.Text = oFacility.Address2;
                        oAddresscontrol.isFormLoading = true;
                        oAddresscontrol.txtZip.Text = oFacility.Zip;
                        oAddresscontrol.isFormLoading = false;
                        oAddresscontrol.txtCity.Text = oFacility.City;


                        for (int nCountryCount = 0; nCountryCount <= oAddresscontrol.cmbCountry.Items.Count - 1; nCountryCount++)
                        {
                            if (oFacility.Country.Trim().ToString() == Convert.ToString(((DataRowView)oAddresscontrol.cmbCountry.Items[nCountryCount])["sName"]))
                            {
                                oAddresscontrol.cmbCountry.SelectedIndex = nCountryCount;
                                break;
                            }
                        }

                        oAddresscontrol.txtCounty.Text = oFacility.County;
                        //oAddresscontrol.cmbCountry.Text = oFacility.Country.Trim().ToString();

                        oAddresscontrol.cmbState.Text = oFacility.State;
                        oAddresscontrol.txtAreaCode.Text = oFacility.AreaCode;

                        
                        sState = oFacility.State;
                        txtPhoneNo.Text = oFacility.Phone;
                        txtFax.Text = oFacility.Fax;
                        txtFacilityCLIANo.Text = oFacility.FaclityCLIANumber;
                        txtMammogramCertNo.Text = oFacility.sMammogramCertNumber;
                        if(oFacility.FacilityType != FacilityType.None)   
                            cmbFacilityType.Text = oFacility.FacilityType.ToString(); 
                        txtPLContactName.Text = oFacility.PhysicalAddContactName;
                        oPLAddressContol.isFormLoading = true;
                        oPLAddressContol.txtAddress1.Text = oFacility.PhysicalAddressline1;
                        oPLAddressContol.txtAddress2.Text = oFacility.PhysicalAddressline2;
                        oPLAddressContol.txtCity.Text = oFacility.PhysicalCity;
                        oPLAddressContol.txtCounty.Text = oFacility.PhysicalCounty;
                        oPLAddressContol.cmbCountry.Text = oFacility.PhysicalCountry;         
                        oPLAddressContol.cmbState.Text = oFacility.PhysicalState;
                        oPLAddressContol.txtZip.Text = oFacility.PhysicalZIP;
                        oPLAddressContol.txtAreaCode.Text = oFacility.PhysicalAreaCode;                                     
                        oPLAddressContol.isFormLoading = false;
                        mskPLPager.Text = oFacility.PhysicalPagerNo;
                        maskedPLPhno.Text = oFacility.PhysicalPhoneNo;
                        mskPLFax.Text = oFacility.PhysicalFAX;
                        txtPLEMail.Text = oFacility.PhysicalEmail;
                        txtPLUrl.Text = oFacility.PhysicalURL;
                        txtTaxonomy.Text = oFacility.TaxonomyCode.Trim();     
                        
                    } 
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }

        private void FillPOS()
        {
            CLsBL_TOSPOS oPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            DataTable dtPOS = new DataTable();
            try
            {
                dtPOS = oPOS.GetPOS(0);
                if (dtPOS != null && dtPOS.Rows.Count > 0)
                {
                    cmbPOS.DataSource = dtPOS;
                    cmbPOS.ValueMember = dtPOS.Columns[0].ColumnName;
                    cmbPOS.DisplayMember = dtPOS.Columns[2].ColumnName;
                    //cmbPOS.SelectedIndex = -1;
                }
                 
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oPOS.Dispose();
                //dtPOS.Dispose();
            }
        }

        private Facility GetFacility()
        {
            try
            {
                Facility oFacility = new Facility();

                oFacility.FacilityID = this.FacilityID;
                oFacility.FacilityCode = txtFacilityCode.Text;
                oFacility.FacilityName = txtFacilityName.Text;
                oFacility.NPI = txtNPI.Text;                
                //oFacility.MedicadID = txtMedicadID.Text;
                //oFacility.BlueShieldID = txtBlueShieldID.Text;
                //oFacility.MedicareID = txtMedicareID.Text;
                oFacility.TaxID = txtTaxID.Text;
                oFacility.POSID = Convert.ToInt64(cmbPOS.SelectedValue);
                //7022Items: Home Billing
                if (chkReportPatientAddress.Checked == true)
                {   oFacility.ReportPatientAddress = true;    }
                else
                {   oFacility.ReportPatientAddress = false;   }

                //oFacility.Address1 = txtAddressLine1.Text;
                //oFacility.Address2 = txtAddressLine2.Text;
                //oFacility.Zip = txtZip.Text;
                //oFacility.City = txtCity.Text;

                //if (cmbState.SelectedIndex != -1)
                //{
                //    oFacility.State = cmbState.Text;
                //}
                oFacility.Address1 = oAddresscontrol.txtAddress1.Text;
                oFacility.Address2 = oAddresscontrol.txtAddress2.Text;
                oFacility.Zip = oAddresscontrol.txtZip.Text;
                oFacility.City = oAddresscontrol.txtCity.Text;                
                //if (oAddresscontrol.cmbState.SelectedIndex != -1)
                //{
                //    oFacility.State = oAddresscontrol.cmbState.Text;
                //}

                if (oAddresscontrol.cmbState.Text.Trim() != "")
                {
                    oFacility.State = oAddresscontrol.cmbState.Text;
                }

                oFacility.AreaCode = oAddresscontrol.txtAreaCode.Text;
                oFacility.Country = Convert.ToString(oAddresscontrol.cmbCountry.Text);
                //if ((oAddresscontrol.cmbCountry.Text.Trim().ToUpper() == "US"))
                //{
                //    oFacility.County = Convert.ToString(oAddresscontrol.txtCounty.Text.Trim());
                //}
                //else
                //{
                //    oFacility.County = "";
                //}

                oFacility.County = Convert.ToString(oAddresscontrol.txtCounty.Text.Trim());
                oFacility.Phone = txtPhoneNo.Text;
                oFacility.Fax = txtFax.Text;
                oFacility.ClinicID = this.ClinicID;
                oFacility.FaclityCLIANumber = txtFacilityCLIANo.Text.Trim();
                oFacility.sMammogramCertNumber = txtMammogramCertNo.Text.Trim();

                if (cmbFacilityType.Text.Trim() == FacilityType.Facility.ToString())
                {
                    oFacility.FacilityType = FacilityType.Facility;
                }
                else if (cmbFacilityType.Text.Trim() == FacilityType.NonFacility.ToString())
                {
                    oFacility.FacilityType = FacilityType.NonFacility;
                }
                else
                {
                    oFacility.FacilityType = FacilityType.None;
                }             

                //Facility Physical Address

                oFacility.PhysicalAddContactName = txtPLContactName.Text.Trim();
                oFacility.PhysicalAddressline1 = oPLAddressContol.txtAddress1.Text.Trim();
                oFacility.PhysicalAddressline2 = oPLAddressContol.txtAddress2.Text.Trim();
                oFacility.PhysicalCity = oPLAddressContol.txtCity.Text.Trim();
                oFacility.PhysicalState = oPLAddressContol.cmbState.Text.Trim();
                oFacility.PhysicalZIP = oPLAddressContol.txtZip.Text.Trim();
                oFacility.PhysicalAreaCode = oPLAddressContol.txtAreaCode.Text.Trim();
                oFacility.PhysicalCountry = oPLAddressContol.cmbCountry.Text.Trim();
                if ((oPLAddressContol.cmbCountry.Text.Trim().ToUpper() == "US"))
                {
                    oFacility.PhysicalCounty = oPLAddressContol.txtCounty.Text.Trim();
                }
                else
                {
                    oFacility.PhysicalCounty = "";
                }
                oFacility.PhysicalPagerNo = mskPLPager.Text.Trim();
                oFacility.PhysicalPhoneNo = maskedPLPhno.Text.Trim();
                oFacility.PhysicalFAX = mskPLFax.Text.Trim();
                oFacility.PhysicalEmail = txtPLEMail.Text.Trim();
                oFacility.PhysicalURL = txtPLUrl.Text.Trim();

                if (!string.IsNullOrEmpty(txtTaxonomy.Text.Trim()))
                {
                    String[] _splitter = null;
                    _splitter = txtTaxonomy.Text.Split('-');
                    oFacility.TaxonomyCode = _splitter[0];
                }
                else
                {
                    oFacility.TaxonomyCode = "";
                }

                return oFacility;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;

            }
            finally
            {
                
            }

        }

        private bool Validate()
        {
            try
            {
                //if (txtFacilityCode.Text == "")
                //{
                //    MessageBox.Show("Please enter Facility Code", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtFacilityCode.Focus();
                //    return false;
                //}
                if (txtFacilityName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the name of the facility.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFacilityName.Focus();
                    return false;
                }
                if (oAddresscontrol.txtAreaCode.TextLength > 0 && oAddresscontrol.txtAreaCode.TextLength < 4)
                {
                    if (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    { return true; }
                    else
                    {
                        oAddresscontrol.txtAreaCode.Select();
                        oAddresscontrol.txtAreaCode.Focus();
                        return false;
                    }
                }
                //if (txtMedicadID.Text == "")
                //{
                //    MessageBox.Show("Please enter Medicad ID", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtMedicadID.Focus();
                //    return false;
                //}
                //if (txtBlueShieldID.Text == "")
                //{
                //    MessageBox.Show("Please enter BlueShield ID", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtBlueShieldID.Focus();
                //    return false;
                //}
                //if (txtMedicareID.Text == "")
                //{
                //    MessageBox.Show("Please enter Medicare ID", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtMedicareID.Focus();
                //    return false;
                //}
                //if (cmbPOS.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Please select the Place Of Service", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    cmbPOS.Focus();
                //    return false;
                //}
                //validate phone number
                //if (txtPhoneNo.Text.Trim().Length > 0 && txtPhoneNo.Text.Trim().Length < 10)
                //{
                //    //MessageBox.Show("Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Phone No is incomplete.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtPhoneNo.Focus();
                //    return false;
                //}
               
                if (txtPhoneNo.Text != "")
                {
                    if (!txtPhoneNo.IsValidated)
                    {
                        return false;
                    }
                }

                if (txtFax.Text != "")
                {
                    if (!txtFax.IsValidated)
                    {
                        return false;
                    }
                }

                if (mskPLPager.Text != "")
                {
                    if (!mskPLPager.IsValidated)
                    {
                        return false;
                    }
                }

                if (mskPLFax.Text != "")
                {
                    if (!mskPLFax.IsValidated)
                    {
                        return false;
                    }
                }

                if (maskedPLPhno.Text != "")
                {
                    if (!maskedPLPhno.IsValidated)
                    {
                        return false;
                    }
                }

                if ((!string.IsNullOrEmpty(txtPLEMail.Text.Trim())))
                {
                    // If Regex.IsMatch(txtEmailAddress.Text.Trim(), "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") = False Then
                    if ((GeneralSettings.ValidateEmailAddress(txtPLEMail.Text.Trim()) == false))
                    {
                        tbFacilitySetup.SelectedIndex = 1;
                        MessageBox.Show("Please enter a valid email id.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPLEMail.Focus();
                        return false;
                    }
                }

                if ((!string.IsNullOrEmpty(txtPLUrl.Text.Trim())))
                {
                    //If Regex.IsMatch(txtURL.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                    if ((GeneralSettings.ValidateURLAddress(txtPLUrl.Text.Trim()) == false))
                    {
                        tbFacilitySetup.SelectedIndex = 1;
                        MessageBox.Show("Please enter a valid url.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPLUrl.Focus();
                        return false;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return false;
            }
            finally
            {

            }
        }
        //Adding Location tree
        private void AddLocationTree()
        {
            gloFacility ogloFacility = new gloFacility(_databaseconnectionstring);
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable dt = new DataTable();
            dt = oLocation.GetList();
            TreeNode oNode;
            trv_Locations.Nodes.Clear();
            trv_Locations.CheckBoxes = true;
            oNode = new TreeNode();
            oNode.Text = "Location";
            oNode.Tag = "Location";
            oNode.ImageIndex = 0;
            oNode.SelectedImageIndex = 0;
            trv_Locations.Nodes.Add(oNode);

           
                
                //if any locations retrieved
                if (dt != null && dt.Rows.Count > 0)
                {
                    trv_Locations.Nodes[0].Nodes.Clear();
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oNode = new TreeNode();
                        oNode.Text = Convert.ToString(dt.Rows[i]["sLocation"]);
                        oNode.Tag = dt.Rows[i]["nLocationId"];
                        oNode.ImageIndex = 0;
                        oNode.SelectedImageIndex = 0;

                        //Adding Location Name to the Location Tree
                        trv_Locations.Nodes[0].Nodes.Add(oNode);
                    }
                }
                trv_Locations.ExpandAll();
            
            if(FacilityID!=0)
            {
                dt = null;
                dt = ogloFacility.GetFacility_Location(FacilityID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (TreeNode n in trv_Locations.Nodes[0].Nodes)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (n.Text == Convert.ToString(dt.Rows[i]["sLocationDesc"]) && Convert.ToInt64(n.Tag) == Convert.ToInt64(dt.Rows[i]["nLocationID"]))
                            {
                                n.Checked = true;
                                break;
                            }

                        }

                    }

                }
 
            }
        }

        private void SaveFacility_Location()
        {
            gloFacility ogloFacility = new gloFacility(_databaseconnectionstring);
            ogloFacility.DeleteFacility_Location(FacilityID);
            foreach (TreeNode n in trv_Locations.Nodes[0].Nodes)
            {
                string _facilitycode = txtFacilityCode.Text;
                string _facilitydesc = txtFacilityName.Text;

                if (n.Checked == true)
                {
                    ogloFacility.AddFacility_Location(FacilityID, _facilitycode, _facilitydesc, Convert.ToInt64(n.Tag), "", Convert.ToString(n.Text));

                }

            }
                                        
        }

        #endregion " Private & Public Methods "

        private void trv_Locations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //TreeNode oNode;
            //gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            //try
            //{
            //    //if selectednode is Location 
            //    if (trv_Locations.SelectedNode.Level == 0)
            //    {
            //        DataTable dt = new DataTable();
            //        dt = oLocation.GetList();
            //        //if any locations retrieved
            //        if( dt!=null &&dt.Rows.Count > 0)
            //        {
            //            trv_Locations.Nodes[0].Nodes.Clear();
            //            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //            {
            //                oNode = new TreeNode();
            //                oNode.Text =Convert.ToString(dt.Rows[i]["sLocation"]);
            //                oNode.Tag = dt.Rows[i]["nLocationId"];
            //                oNode.ImageIndex = 0;
            //                oNode.SelectedImageIndex = 0;

            //            //Adding Location Name to the Location Tree
            //                trv_Locations.Nodes[0].Nodes.Add(oNode);
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    oLocation.Dispose();
            //}
          
        }

        private void trv_Locations_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.Level == 0)
            //{
            //    foreach (TreeNode n in trv_Locations.Nodes[0].Nodes)
            //    {
            //        n.Checked = e.Node.Checked;  
            //    }
            //}
            // Code added for check uncheck all chekbox of Location Tree view Sameeer 16-May-2014 
            if (bChildTrigger)
            {
                CheckAllChildren(e.Node, e.Node.Checked);
            }
            if (bParentTrigger)
            {
                CheckMyParent(e.Node, e.Node.Checked);
            }
        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code to allow nos only 
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        
        //Added By Pramod For Filling The States 20090716
        #region "Fill Methods"

        private void fillStates(String _States)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "ST";
                }

                if (_States != "")
                {
                    cmbState.Text = _States;
                }



            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        private void FillFacilityQF()
        {
            try
            {
                DataTable dtFacilityQF=null;
                gloSettings.GeneralSettings osettings = new gloSettings.GeneralSettings(DatabaseConnectionString);
                dtFacilityQF = osettings.getIDQualifiers(1,FacilityID,true);
                osettings.Dispose();
                osettings = null;
                if (dtFacilityQF != null)
                {
                    int rowIndex = 0;

                    for (int i = 0; i < dtFacilityQF.Rows.Count; i++)
                    {
                        c1FacilityQF.Rows.Add();
                        rowIndex = c1FacilityQF.Rows.Count - 1;

                        //c1PatientEMRExams.SetCellCheck(rowIndex, COL_EXAM_SELECT,CheckEnum.Unchecked);
                        c1FacilityQF.SetData(rowIndex, COL_SETTINGID, Convert.ToInt64(dtFacilityQF.Rows[i]["nQualifierID"]));
                        c1FacilityQF.SetData(rowIndex, COL_SETTINCODE, Convert.ToString(dtFacilityQF.Rows[i]["sCode"]));
                        c1FacilityQF.SetData(rowIndex, COL_SETTINGNAME, Convert.ToString(dtFacilityQF.Rows[i]["sAdditionalDescription"]));
                        c1FacilityQF.SetData(rowIndex, COL_SETTINGVALUE, Convert.ToString(dtFacilityQF.Rows[i]["sValue"]));
                        c1FacilityQF.SetData(rowIndex, COL_BISSYSTEM, Convert.ToString(dtFacilityQF.Rows[i]["bIsSystem"]));

                    }
                    dtFacilityQF.Dispose();
                    dtFacilityQF = null;
                }
            }
            catch // (Exception ex)
            {
                
                
            }
        }
        

        #endregion


        public bool SaveFacilityQualifier()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            Int64 _nFacilityID = FacilityID;
            try
            {

                c1FacilityQF.FinishEditing();
                oDB.Connect(false);
                string strQuery = "delete from BL_Facility_ID_Qualifiers where nFacilityID= " + FacilityID;
                oDB.Execute_Query(strQuery);
                for (int i = 1; i <= c1FacilityQF.Rows.Count-1; i++)
                {

                    Int64 _nQualifierID=Convert.ToInt64(c1FacilityQF.GetData(i, COL_SETTINGID));
                    //string strQuery = "delete from BL_Facility_ID_Qualifiers where nFacilityID= " + FacilityID + " AND nQualifierID=" + _nQualifierID;
                    //oDB.Execute_Query(strQuery);

                    if( Convert.ToString(c1FacilityQF.GetData(i, COL_SETTINGVALUE)) != "" || Convert.ToString(c1FacilityQF.GetData(i, COL_SETTINGVALUE)) != String.Empty)
                    {
                        oParameters.Clear();

                        oParameters.Add("@nFacilityID", _nFacilityID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nQualifierID",_nQualifierID , ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sValue", Convert.ToString(c1FacilityQF.GetData(i, COL_SETTINGVALUE)), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@bIsSystem", Convert.ToBoolean(c1FacilityQF.GetData(i, COL_BISSYSTEM)), ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("BL_INUP_FacilityQualifierIDs", oParameters);
                    }
                   
                }
                oParameters.Clear();
                //NPI
                oParameters.Add("@nFacilityID", _nFacilityID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nQualifierID", 1, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sValue", txtNPI.Text, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIsSystem", true, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INUP_FacilityQualifierIDs", oParameters);

                oParameters.Clear();
                //TAX ID
                oParameters.Add("@nFacilityID", _nFacilityID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@nQualifierID", 5, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nQualifierID", 3, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sValue", txtTaxID.Text, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIsSystem", true, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INUP_FacilityQualifierIDs", oParameters);

            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                MessageBox.Show("ERROR : " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                _oResult = null;
            }
            return true;
        }



        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    //Code modified by Mayuri:20091203
                    //To fix issue:#427-Enter zip code manually and press tab 
                    //string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtZip.Text.Trim() + "";
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtZip.Text.Trim() + "'";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //Commented By Pramod Nair For Filling The States 20090716
                        //txtState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        //Added By Pramod Nair For Filling The States 20090716
                        cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        
                        if (txtCity.Text.Trim() == "" )
                            txtCity.Text = Convert.ToString(dt.Rows[0]["City"]);

                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                //txtState.Text = "";
                txtCity.Text = "";
            }
        }

        private void txtTaxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void c1FacilityQF_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void c1FacilityQF_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_SETTINGVALUE)
            {

                ((TextBox)c1FacilityQF.Editor).MaxLength = 250;
                
            }
        }

        private void c1FacilityQF_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1FacilityQF.Editor = (TextBox)c1FacilityQF.Editor;
        }

        private void txtPhoneNo_ErrorMessageInvoked(object sender, EventArgs e)
        {
            ValidationFailed = true;
        }

        private void txtFax_ErrorMessageInvoked(object sender, EventArgs e)
        {
            ValidationFailed = true;
        }

        private void mskPLPager_ErrorMessageInvoked(object sender, EventArgs e)
        {
            ValidationFailed = true;
        }

        private void mskPLFax_ErrorMessageInvoked(object sender, EventArgs e)
        {
            ValidationFailed = true;
        }

        private void maskedPLPhno_ErrorMessageInvoked(object sender, EventArgs e)
        {
            ValidationFailed = true;
        }

        private void tbFacilitySetup_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = ValidationFailed;
            ValidationFailed = false;
        }

        private void btn_BrowseCmpTaxonomy_Click(object sender, EventArgs e)
        {
            try
            {

                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Taxonomy, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Taxonomy";
                //_CurrentControlType = gloListControl.gloListControlType.Taxonomy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                oListControl.OpenControl();
              
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                pnltlsStrip.Visible=false;
                tbFacilitySetup.Visible = false;
            }
            catch //(Exception ex)
            {
                
            }

        }

        private void btn_ClearCmpTaxonomy_Click(object sender, EventArgs e)
        {
            txtTaxonomy.Clear();
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtTaxonomy.Clear();
                if (oListControl.SelectedItems.Count > 0)
                {
                    txtTaxonomy.Text = (oListControl.SelectedItems[0].Code.ToString() + "-") + oListControl.SelectedItems[0].Description.ToString();
                }
            }
            catch
            {
            }
            finally
            {
                pnltlsStrip.Visible = true;
                tbFacilitySetup.Visible = true;
            }
        }
        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            pnltlsStrip.Visible = true;
            tbFacilitySetup.Visible = true;
        }
        // Code added for check uncheck all chekbox of Location Tree view Sameeer 16-May-2014 
        private void CheckMyParent(TreeNode tn, Boolean bCheck)
        {

            if (tn == null)
            {
                return;
            }
            if (tn.Parent == null)
            {
                return;
            }

            bChildTrigger = false;
            bParentTrigger = false;

            if (bCheck)
            {
                bool bNodeFound = false;
                foreach (TreeNode _Node in tn.Parent.Nodes)
                {
                    if (_Node.Checked == false)
                    {
                        tn.Parent.Checked = false;
                        bNodeFound = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bNodeFound == false)
                {
                    tn.Parent.Checked = true;
                }
            }
            else
            {
                tn.Parent.Checked = bCheck;
            }

            CheckMyParent(tn.Parent, bCheck);
            bParentTrigger = true;
            bChildTrigger = true;

        }
        private void CheckAllChildren(TreeNode tn, Boolean bCheck)
        {

            foreach (TreeNode ctn in tn.Nodes)
            {
                ctn.Checked = bCheck;
                CheckAllChildren(ctn, bCheck);
            }

        }


        private void txtMammogramCertNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }

        }

       
      

        
    }
}