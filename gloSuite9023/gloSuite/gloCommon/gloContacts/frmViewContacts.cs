using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloPMContacts;

namespace gloContacts
{

    public partial class frmViewContacts : Form
    {
        //Shubhangi
        public Boolean IsForModify = false;
        //End
        #region "Private Variables"

        // for Connection String;
        private string _databaseconnectionstring = "";

        // for Messagebox Caption;
        private string _messgeBoxCaption = String.Empty;

        //ContactID
        private Int64 _Contactid = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _UserID = 0;
        private string _UserName = "";
        // SHUBHANGI // 20100312 
        // THIS VARIABLE IS USED TO TAKE THE VALUE OF SELECTED CONTACT. 
        // WE MAINTAIN THIS VARIABLE COZ WHEN WE CLICK ON THE ROOT NODE OF THECONTACT IT IS DISPLYING THE DETAILS OF PREVIOUSLY SELECTED CONTACT . 
        // SO HANDLE ALL THE EVENTS RELATED TO THAT.
        string _SelectedContact = "";


        #endregion "Private Variables"

        #region "Constants For Grid"
        //ContactID,PhysicianName,LastName,sName,ContactName,Gender,AddressLine1,AddressLine2,City, State, ZIP,Phone,FAX,Mobile,Email 
        ////InsuranceTypeDesc ,InsuranceTypeCode
        //private const int COL_ContactID = 0;
        //private const int COL_PhyisicianName = 1;
        //private const int COL_FirstName = 2;
        //private const int COL_LastName = 3;
        //private const int COL_Name = 4;
        //private const int COL_ContactName = 5;
        //private const int COL_Gender = 6;
        //private const int COL_AddressLine1 = 7;
        //private const int COL_AddressLine2 = 8;
        //private const int COL_City = 9;
        //private const int COL_State = 10;
        //private const int COL_ZIP = 11;
        //private const int COL_Phone = 12;
        //private const int COL_Fax = 13;
        //private const int COL_Mobile = 14;
        //private const int COL_Email = 15;
        //private const int COL_InsuranceTypeDesc = 16;
        //private const int COL_InsuranceTypeCode = 17;
        //private const int COL_COUNT = 18;

        private const int COL_ContactID = 0;
        private const int COL_PhyisicianName = 1;
        private const int COL_LastName = 2;

        private const int COL_Name = 3;
        private const int COL_ContactName = 4;
        private const int COL_Gender = 5;
        private const int COL_AddressLine1 = 6;
        private const int COL_AddressLine2 = 7;
        private const int COL_City = 8;
        private const int COL_State = 9;
        private const int COL_ZIP = 10;
        private const int COL_Phone = 11;
        private const int COL_Fax = 12;
        private const int COL_Mobile = 13;
        private const int COL_Email = 14;       
        private const int COL_SpecialityType1 = 15;
        private const int COL_SpecialityType2 = 16;
        private const int COL_SpecialityType3 = 17;
        private const int COL_SpecialityType4 = 18;
        private const int COL_NCPDPID = 19;
        private const int COL_InsuranceTypeDesc = 20;
        private const int COL_InsuranceTypeCode = 21;        
        private const int COL_COUNT = 22;
       

        //SHUBHANGI 20100618 added for physician
        private const int COL_PHYContactID = 0;
        private const int COL_PHYPhyisicianName = 1;
        private const int COL_PHYFirstName = 2;
        private const int COL_PHYLastName = 3;
        private const int COL_PHYName = 4;
        private const int COL_PHYContactName = 5;
        private const int COL_PHYGender = 6;
        private const int COL_PHYAddressLine1 = 7;
        private const int COL_PHYAddressLine2 = 8;
        private const int COL_PHYCity = 9;
        private const int COL_PHYState = 10;
        private const int COL_PHYZIP = 11;
        private const int COL_PHYPhone = 12;
        private const int COL_PHYFax = 13;
        private const int COL_PHYMobile = 14;
        private const int COL_PHYEmail = 15;
        private const int COL_SPI = 16;
        private const int COL_DIRECTADDRESS = 17;
        private const int COL_Specialtytype = 18;
        private const int COL_ClinicName = 19;
        private const int COL_NPI = 20;
        private const int COL_PHYCOUNT = 21;



        private const int COL_InsPlan_ContactID = 0;
        private const int COL_InsPlan_PhyisicianName = 1;
        private const int COL_InsPlan_LastName = 2;
        private const int COL_InsPlan_Name = 3;
        private const int COL_InsPlan_Company = 4;
        private const int COL_InsPlan_sPayerId = 5;
        private const int COL_InsPlan_ReportingCategory = 6;
        private const int COL_InsPlan_InsuranceTypeDesc = 7;
        private const int COL_InsPlan_Gender = 8;
        private const int COL_InsPlan_AddressLine1 = 9;
        private const int COL_InsPlan_AddressLine2 = 10;
        private const int COL_InsPlan_City = 11;
        private const int COL_InsPlan_State = 12;
        private const int COL_InsPlan_ZIP = 13;
        private const int COL_InsPlan_ContactName = 14;
        private const int COL_InsPlan_Phone = 15;
        private const int COL_InsPlan_Fax = 16;
        private const int COL_InsPlan_Mobile = 17;
        private const int COL_InsPlan_Email = 18;
        private const int COL_InsPlan_InsuranceTypeCode = 19;
        private const int COL_InsPlan_COUNT = 20;
        private const int COL_CPT_Mapping = 21;

        private const int COL_CollectionAgency_ContactID = 0;
        private const int COL_CollectionAgency_Name = 1;
        private const int COL_CollectionAgency_Contact = 2;
        private const int COL_CollectionAgency_AddressLine1 = 3;
        private const int COL_CollectionAgency_AddressLine2 = 4;
        private const int COL_CollectionAgency_City = 5;
        private const int COL_CollectionAgency_State = 6;
        private const int COL_CollectionAgency_ZIP = 7;
        private const int COL_CollectionAgency_Phone = 8;
        private const int COL_CollectionAgency_Fax = 9;
        private const int COL_CollectionAgency_Email = 10;
        private const int COL_CollectionAgency_URL = 11;
        private const int COL_CollectionAgency_ContactType = 12;
        private const int COL_CollectionAgency_BadDebtFeeType = 13;
        private const int COL_CollectionAgency_PercentofSelfPayBalance = 14;
        private const int COL_CollectionAgency_Flatfee = 15;
       
        

        #endregion "Constants For Grid"

        private Timer timerOwnAddress = new Timer();
        DateTime _CurrentTime;



        #region "Constructor"

        //Default Constructor 
        public frmViewContacts()
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messgeBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messgeBoxCaption = "gloPM";
                }
            }
            else
            { _messgeBoxCaption = "gloPM"; }

            #endregion

            #region "Retreive UserId & UserName "

            //Added By Pramod Nair For UserRights 20090720

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            //
            #endregion
        }

        //Constructor with Connection String
        public frmViewContacts(string Databaseconnectionstring)
        {
            InitializeComponent();

            //set the private variable for connection String;
            _databaseconnectionstring = Databaseconnectionstring;

            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messgeBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messgeBoxCaption = "gloPM";
                }
            }
            else
            { _messgeBoxCaption = "gloPM"; }

            #endregion

            #region "Retreive UserId & UserName "

            //Added By Pramod Nair For UserRights 20090720

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            //
            #endregion
        }

        //check for instance of form bugs no: 4373
        private static frmViewContacts _frm = null;

        public static frmViewContacts GetInstance()
        {

            if (_frm != null)
            {
                return _frm;
            }
            else
            {
                _frm = new frmViewContacts();
                return _frm;
            }

        }

        bool blnDisposed;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (blnDisposed == false)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            _frm = null;
            blnDisposed = true;
        }

        private void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #endregion "Constructor"

        #region "Public Properties"

        // Property for Database Connection String.
        public string Databaseconnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion "Public Properties"

        #region "Form Load"

        private void frmViewContacts_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1ViewContacts, false);



            // Fill the Types in Treeview of form from database.
            FillContactTypeTree();
            AssignUserRights();
           // FillSpecialityType();

            timerOwnAddress.Tick += new System.EventHandler(timerOwnAddress_Tick);

        }

        #endregion "Form Load"

        private void timerOwnAddress_Tick(object sender, EventArgs e)
        {
            timerOwnAddress.Stop();
            string strSearch = txt_search.Text.Trim();
            this.picProgress.Visible = true;
            Application.DoEvents();

            if (strSearch.Trim() != "")
            {
                if (System.DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {                    
                    if (this.Visible == true)
                    {
                        FilterDirectPhysician(strSearch.Trim()); ;
                    }                 
                }
            }
            else
            {
                //timerOwnAddress.Stop();
                
                if (this.Visible == true)
                {
                    FilterDirectPhysician(strSearch.Trim()); ;
                }                
            }
            this.picProgress.Visible = false;
            strSearch = null;
        }

        private void DesignGrid(Int64 ID,string sSpecialityType="")
        {
            gloContacts.gloContact Contact = new gloContact(_databaseconnectionstring);
            try
            {
                //Sanjog -Commented on 2011 Jan 17 to hide the button for e-pharmacy
                //tls_btnModify.Enabled = true;

                DataTable dt = null;
                DataView _dv = null;
                // Get the Contacts from Database.
                //Shubhangi 20091104
                // Check for Insurance company
                // Add reset search text box to reset text box after clicking on refresh
                txt_search.ResetText();
                //20100312
                // if (trvContacts.SelectedNode.Text == "Insurance Company")
                if (_SelectedContact == "Insurance Company")
                {
                    // frmInsuranceCompany ofrmcmpny = new frmInsuranceCompany(_databaseconnectionstring);
                    dt = Contact.GetInsuranceCompanyDetails();
                    _dv = dt.DefaultView;


                    if (_dv != null)
                    {
                        //c1ViewContacts.Clear();
                        c1ViewContacts.DataSource = null;
                        c1ViewContacts.DataSource = _dv;

                        c1ViewContacts.SetData(0, 2, "Company Name");
                        c1ViewContacts.SetData(0, 4, "Insurance Type");
                        c1ViewContacts.SetData(0, 6, "Address 1");
                        c1ViewContacts.SetData(0, 7, "Address 2");
                        c1ViewContacts.SetData(0, 8, "City");
                        c1ViewContacts.SetData(0, 9, "State");
                        c1ViewContacts.SetData(0, 10, "Zip");
                        c1ViewContacts.SetData(0, 13, "CPT Crosswalk");
                        c1ViewContacts.SetData(0, 14, "Max Charges Per Claim");
                        c1ViewContacts.SetData(0, 15, "Max Diagnosis Per Claim");

                        if (Width < 1000)
                        {
                            c1ViewContacts.Cols[2].Width = 1280 / 6;
                            c1ViewContacts.Cols[6].Width = 1280 / 6;
                            c1ViewContacts.Cols[7].Width = 1280 / 6;
                            c1ViewContacts.Cols[8].Width = 1280 / 12;
                            c1ViewContacts.Cols[9].Width = 1280 / 12;
                            c1ViewContacts.Cols[10].Width = 1280 / 12;
                            c1ViewContacts.Cols[14].Width = Width / 6;
                            c1ViewContacts.Cols[15].Width = Width / 6;
                        }
                        else
                        {
                            c1ViewContacts.Cols[2].Width = Width / 6;
                            c1ViewContacts.Cols[6].Width = Width / 6;
                            c1ViewContacts.Cols[7].Width = Width / 6;
                            c1ViewContacts.Cols[8].Width = Width / 12;
                            c1ViewContacts.Cols[9].Width = Width / 12;
                            c1ViewContacts.Cols[10].Width = Width / 12;
                            c1ViewContacts.Cols[14].Width = Width / 6;
                            c1ViewContacts.Cols[15].Width = Width / 6;
                        }
                        c1ViewContacts.Cols[0].Visible = false;
                        c1ViewContacts.Cols[1].Visible = false;
                        c1ViewContacts.Cols[3].Visible = false;
                        c1ViewContacts.Cols[4].Visible = false;


                        c1ViewContacts.Cols[5].Visible = false;
                        c1ViewContacts.Cols[11].Visible = false;
                        c1ViewContacts.Cols[12].Visible = false;


                        c1ViewContacts.AllowEditing = false;
                        c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                        c1ViewContacts.Cols[10].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                    }

                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }

                }
                //End Shubhangi
                //Abhisekh 10/02/2010 --------------start
                // Check for Insurance Reporting Category
                // Add reset search text box to reset text box after clicking on refresh
                //20100312
                //else if (trvContacts.SelectedNode.Text == "Insurance Reporting Category")
                else if (_SelectedContact == "Insurance Reporting Category")
                {
                    // frmInsuranceCompany ofrmcmpny = new frmInsuranceCompany(_databaseconnectionstring);
                    //DataTable dt = new DataTable();
                    //DataView _dv = new DataView();
                    //20100312
                    //dt = Contact.GetContacts(trvContacts.SelectedNode.Text);
                    dt = Contact.GetContacts(_SelectedContact);
                    _dv = dt.DefaultView;

                    if (_dv != null)
                    {
                        c1ViewContacts.Redraw = false;
                     //   c1ViewContacts.Clear();
                        c1ViewContacts.DataSource = null;
                        c1ViewContacts.DataSource = _dv;
                        c1ViewContacts.Cols[0].Visible = false;
                        c1ViewContacts.Cols[1].Visible = false;
                        c1ViewContacts.Cols[3].Visible = false;
                        c1ViewContacts.SetData(0, 1, "Code");
                        c1ViewContacts.SetData(0, 2, "Insurance Reporting Category");
                        c1ViewContacts.Cols[2].Width = c1ViewContacts.Width;
                        c1ViewContacts.AllowEditing = false;
                        c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                        c1ViewContacts.Redraw = true;


                    }

                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }

                }
                //--------------------End Abhisekh
                //20100212
                //else if (trvContacts.SelectedNode.Text == "Insurance Plan")
                else if (_SelectedContact == "Insurance Plan")
                {
                    cmbInsuranceCompany.Visible = true;
                    FillInsuranceCompany();
                    c1ViewContacts.AllowEditing = false;
                    c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }

                    if (c1ViewContacts.Rows.Count == 1)
                    {
                        tls_btnInActive.Enabled = false;
                        tls_btnModify.Enabled = false;
                    }
                    else
                    {
                        tls_btnInActive.Enabled = true;
                        tls_btnModify.Enabled = true;
                    }
                }

                else if (_SelectedContact == "Inactive Insurance Plan")
                {
                    cmbInsuranceCompany.Visible = true;
                    FillInsuranceCompany();
                    c1ViewContacts.AllowEditing = false;
                    c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;

                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }

                    if (c1ViewContacts.Rows.Count == 1)
                    {
                        tls_btnActive.Enabled = false;
                        tls_btnModify.Enabled = false;
                    }

                }
                else if (_SelectedContact == "Collection Agency")
                {

                    FillCollectionAgency();
                    cmbInsuranceCompany.Visible = false;
                    c1ViewContacts.AllowEditing = false;
                    c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }
                    ID1 = null;
                    if (c1ViewContacts.Rows.Count == 1)
                    {
                        tls_btnInActive.Enabled = false;
                        tls_btnModify.Enabled = false;
                        tls_btnDelete.Visible = false;
                    }
                    else
                    {
                        tls_btnInActive.Enabled = true;
                        tls_btnModify.Enabled = true;
                        tls_btnDelete.Visible = false;
                    }
                }
                else if (_SelectedContact == "Inactive Collection Agency")
                {
                    FillCollectionAgency(true);
                    cmbInsuranceCompany.Visible = false;
                    c1ViewContacts.AllowEditing = false;
                    c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                    // show highlited default first row
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                    {
                        c1ViewContacts.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                        c1ViewContacts.Row = RowIndex;
                    }

                    if (c1ViewContacts.Rows.Count == 1)
                    {
                        tls_btnActive.Enabled = false;
                        tls_btnModify.Enabled = false;
                    }
                }

                else if (_SelectedContact == "Insurance Reporting Category")
                {

                }
                else
                {

                    if (_SelectedContact == "Direct Physician")
                    {
                        dt = Contact.GetDirectPhysician();
                    }
                    else
                    {
                        dt = Contact.GetContacts(_SelectedContact, sSpecialityType);
                    }

                    _dv = dt.DefaultView;

                    if (_dv != null)
                    {

                        int width = pnlMain.Width;
                        //SHUBHANGI  20100618 
                        if (_SelectedContact == "Physician" || _SelectedContact == "Direct Physician")
                        {
                            c1ViewContacts.Redraw = false;
                          //  c1ViewContacts.Clear();
                            c1ViewContacts.DataSource = null;
                            c1ViewContacts.DataSource = _dv;
                            c1ViewContacts.Rows.Fixed = 1;

                            c1ViewContacts.SetData(0, COL_PHYContactID, "ContactID");
                            c1ViewContacts.SetData(0, COL_PHYPhyisicianName, "Name");
                            c1ViewContacts.SetData(0, COL_PHYFirstName, "FirstName");
                            c1ViewContacts.SetData(0, COL_PHYLastName, "LastName");
                            c1ViewContacts.SetData(0, COL_PHYName, "Name");
                            c1ViewContacts.SetData(0, COL_PHYContactName, "Contact");
                            c1ViewContacts.SetData(0, COL_PHYGender, "Gender");
                            c1ViewContacts.SetData(0, COL_PHYAddressLine1, "Address 1");
                            c1ViewContacts.SetData(0, COL_PHYAddressLine2, "Address 2");
                            c1ViewContacts.SetData(0, COL_PHYCity, "City");
                            c1ViewContacts.SetData(0, COL_PHYState, "State");
                            c1ViewContacts.SetData(0, COL_PHYZIP, "Zip");
                            c1ViewContacts.SetData(0, COL_PHYPhone, "Phone");
                            c1ViewContacts.SetData(0, COL_PHYFax, "Fax");
                            c1ViewContacts.SetData(0, COL_PHYMobile, "Mobile");
                            c1ViewContacts.SetData(0, COL_PHYEmail, "Email");

                            if (_SelectedContact == "Direct Physician")
                            {
                                c1ViewContacts.SetData(0, COL_SPI, "SPI");
                                c1ViewContacts.Cols[COL_SPI].Visible = true;
                                c1ViewContacts.Cols[COL_SPI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                                c1ViewContacts.SetData(0, COL_DIRECTADDRESS, "Direct Address");
                                c1ViewContacts.Cols[COL_DIRECTADDRESS].Visible = true;
                                c1ViewContacts.Cols[COL_DIRECTADDRESS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                                c1ViewContacts.SetData(0, COL_Specialtytype, "Specialty Type1");
                                c1ViewContacts.Cols[COL_Specialtytype].Visible = true;
                                c1ViewContacts.Cols[COL_Specialtytype].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                                c1ViewContacts.SetData(0, COL_ClinicName, "Clinic Name");
                                c1ViewContacts.Cols[COL_ClinicName].Visible = true;
                                c1ViewContacts.Cols[COL_ClinicName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                                c1ViewContacts.SetData(0, COL_NPI, "NPI");
                                c1ViewContacts.Cols[COL_NPI].Visible = true;
                                c1ViewContacts.Cols[COL_NPI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                            }


                            c1ViewContacts.AutoSizeCol(COL_Name);
                            c1ViewContacts.AutoSizeCol(COL_PhyisicianName);

                            c1ViewContacts.Cols[COL_PHYName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYGender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYAddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYAddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYState].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_PHYPhone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_PHYFax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_PHYMobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_PHYEmail].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYContactName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_PHYGender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                            //int width = pnlMain.Width;
                            //c1ViewContacts.Cols[COL_Name].Width = (int)(width * 0.15);
                            //c1ViewContacts.Cols[COL_Gender].Width = 0;
                            //c1ViewContacts.Cols[COL_AddressLine1].Width = (int)(width * 0.09);
                            //c1ViewContacts.Cols[COL_AddressLine2].Width = (int)(width * 0.09);
                            //c1ViewContacts.Cols[COL_State].Width = (int)(width * 0.06);
                            //c1ViewContacts.Cols[COL_ZIP].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Phone].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Fax].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Mobile].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Email].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_ContactName].Width = (int)(width * 0.08);


                            c1ViewContacts.Cols[COL_PHYContactID].Visible = false;
                            c1ViewContacts.Cols[COL_PHYPhyisicianName].Visible = false;
                            c1ViewContacts.Cols[COL_PHYContactName].Visible = true;
                            c1ViewContacts.Cols[COL_PHYName].Visible = true;
                            //c1ViewContacts.Cols[COL_FirstName].Visible = false;
                            c1ViewContacts.Cols[COL_PHYLastName].Visible = false;
                            c1ViewContacts.Cols[COL_PHYGender].Visible = true;
                            c1ViewContacts.Cols[COL_PHYAddressLine1].Visible = true;
                            c1ViewContacts.Cols[COL_PHYAddressLine2].Visible = true;
                            c1ViewContacts.Cols[COL_PHYCity].Visible = true;
                            c1ViewContacts.Cols[COL_PHYState].Visible = true;
                            c1ViewContacts.Cols[COL_PHYZIP].Visible = true;
                            c1ViewContacts.Cols[COL_PHYFax].Visible = true;
                            c1ViewContacts.Cols[COL_PHYEmail].Visible = true;


                            c1ViewContacts.AllowEditing = false;
                            c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                            c1ViewContacts.Rows[0].AllowEditing = false;
                            c1ViewContacts.Redraw = true;


                        }
                        else
                        {

                            c1ViewContacts.Redraw = false;
                           // c1ViewContacts.Clear();
                            c1ViewContacts.DataSource = null;
                            c1ViewContacts.DataSource = _dv;
                            c1ViewContacts.Rows.Fixed = 1;

                            c1ViewContacts.SetData(0, COL_ContactID, "ContactID");
                            c1ViewContacts.SetData(0, COL_PhyisicianName, "Name");
                            //c1ViewContacts.SetData(0, COL_FirstName, "FirstName");
                            c1ViewContacts.SetData(0, COL_LastName, "LastName");
                            c1ViewContacts.SetData(0, COL_Name, "Name");
                            c1ViewContacts.SetData(0, COL_ContactName, "Contact");
                            c1ViewContacts.SetData(0, COL_Gender, "Gender");
                            c1ViewContacts.SetData(0, COL_AddressLine1, "Address 1");
                            c1ViewContacts.SetData(0, COL_AddressLine2, "Address 2");
                            c1ViewContacts.SetData(0, COL_City, "City");
                            c1ViewContacts.SetData(0, COL_State, "State");
                            c1ViewContacts.SetData(0, COL_ZIP, "Zip");
                            c1ViewContacts.SetData(0, COL_Phone, "Phone");
                            c1ViewContacts.SetData(0, COL_Fax, "Fax");
                            c1ViewContacts.SetData(0, COL_Mobile, "Mobile");
                            c1ViewContacts.SetData(0, COL_Email, "Email");
                            c1ViewContacts.SetData(0, COL_SpecialityType1, "Speciality Type1");
                            c1ViewContacts.SetData(0, COL_SpecialityType2, "Speciality Type2");
                            c1ViewContacts.SetData(0, COL_SpecialityType3, "Speciality Type3");
                            c1ViewContacts.SetData(0, COL_SpecialityType4, "Speciality Type4");
                            c1ViewContacts.SetData(0, COL_NCPDPID, "NCPDPID");

                            c1ViewContacts.AutoSizeCol(COL_Name);
                            c1ViewContacts.AutoSizeCol(COL_PhyisicianName);

                            c1ViewContacts.Cols[COL_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_Gender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_Fax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                            c1ViewContacts.Cols[COL_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_ContactName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_Gender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_SpecialityType1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_SpecialityType2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_SpecialityType3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_SpecialityType4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            c1ViewContacts.Cols[COL_NCPDPID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                            //int width = pnlMain.Width;
                            //c1ViewContacts.Cols[COL_Name].Width = (int)(width * 0.15);
                            //c1ViewContacts.Cols[COL_Gender].Width = 0;
                            //c1ViewContacts.Cols[COL_AddressLine1].Width = (int)(width * 0.09);
                            //c1ViewContacts.Cols[COL_AddressLine2].Width = (int)(width * 0.09);
                            //c1ViewContacts.Cols[COL_State].Width = (int)(width * 0.06);
                            //c1ViewContacts.Cols[COL_ZIP].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Phone].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Fax].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Mobile].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_Email].Width = (int)(width * 0.08);
                            //c1ViewContacts.Cols[COL_ContactName].Width = (int)(width * 0.08);


                            c1ViewContacts.Cols[COL_ContactID].Visible = false;
                            c1ViewContacts.Cols[COL_PhyisicianName].Visible = false;
                            c1ViewContacts.Cols[COL_ContactName].Visible = true;
                            c1ViewContacts.Cols[COL_Name].Visible = true;
                            //c1ViewContacts.Cols[COL_FirstName].Visible = false;
                            c1ViewContacts.Cols[COL_LastName].Visible = false;
                            c1ViewContacts.Cols[COL_Gender].Visible = false;
                            c1ViewContacts.Cols[COL_AddressLine1].Visible = true;
                            c1ViewContacts.Cols[COL_AddressLine2].Visible = true;
                            c1ViewContacts.Cols[COL_City].Visible = true;
                            c1ViewContacts.Cols[COL_State].Visible = true;
                            c1ViewContacts.Cols[COL_ZIP].Visible = true;
                            c1ViewContacts.Cols[COL_Fax].Visible = true;
                            c1ViewContacts.Cols[COL_Email].Visible = true;


                            if (_SelectedContact == "Pharmacy" || _SelectedContact == "Others" || _SelectedContact == "Hospital")
                            {
                                lblSpecialityType.Visible = false;
                                cmbSpeciality.Visible = false;

                                c1ViewContacts.Cols[COL_SpecialityType1].Visible = false;
                                c1ViewContacts.Cols[COL_SpecialityType2].Visible = false;
                                c1ViewContacts.Cols[COL_SpecialityType3].Visible = false;
                                c1ViewContacts.Cols[COL_SpecialityType4].Visible = false;
                                c1ViewContacts.Cols[COL_NCPDPID].Visible = false;
                            }
                            else
                            {
                                lblSpecialityType.Visible = true;
                                cmbSpeciality.Visible = true;

                                c1ViewContacts.Cols[COL_SpecialityType1].Visible = true;
                                c1ViewContacts.Cols[COL_SpecialityType2].Visible = true;
                                c1ViewContacts.Cols[COL_SpecialityType3].Visible = true;
                                c1ViewContacts.Cols[COL_SpecialityType4].Visible = true;
                                c1ViewContacts.Cols[COL_NCPDPID].Visible = true;
                            }
                            c1ViewContacts.AllowEditing = false;
                            //c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                            c1ViewContacts.Rows[0].AllowEditing = false;
                            c1ViewContacts.Redraw = true;

                        }

                        if (_SelectedContact == "Physician" || _SelectedContact == "Direct Physician")
                        //if (trvContacts.SelectedNode.Text == "Physician")
                        {
                            c1ViewContacts.Cols[COL_PHYPhyisicianName].Visible = true;
                            c1ViewContacts.Cols[COL_PHYGender].Visible = true;
                            c1ViewContacts.Cols[COL_PHYContactName].Visible = false;
                            c1ViewContacts.Cols[COL_PHYName].Visible = false;
                            c1ViewContacts.Cols[COL_PHYFirstName].Visible = false;
                            if (_SelectedContact == "Direct Physician")
                            {

                                c1ViewContacts.Cols[COL_NPI].Visible = true;


                                c1ViewContacts.Cols[COL_DIRECTADDRESS].Visible = true;
                                c1ViewContacts.Cols[COL_SPI].Visible = true;

                                c1ViewContacts.Cols[COL_Specialtytype].Visible = true;
                                c1ViewContacts.Cols[COL_ClinicName].Visible = true;
                            }

                            //c1ViewContacts.Cols[COL_Gender].Width = (int)(width * 0.09);
                            //c1ViewContacts.Cols[COL_PhyisicianName].Width = (int)(width * 0.15);
                            //c1ViewContacts.Cols[COL_ContactName].Width = 0 ;
                            //c1ViewContacts.Cols[COL_Name].Width = 0;

                        }

                        string ID1 = Convert.ToString(ID);
                        if (ID == 0 && c1ViewContacts.Rows.Count > 1)
                        {
                            c1ViewContacts.Row = 1;
                        }
                        else
                        {
                            int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                            c1ViewContacts.Row = RowIndex;
                        }

                        //To set the buttons enability  
                        if (c1ViewContacts.Rows.Count > 1)
                        {
                            //if ((Convert.ToInt64(trvContacts.SelectedNode.Tag) != 5 && Convert.ToInt64(trvContacts.SelectedNode.Tag) != 9) && trvContacts.SelectedNode.Level != 2)
                            if (_SelectedContact != "e-Pharmacy" && _SelectedContact != "Zip code" && _SelectedContact != "New Rx" && _SelectedContact != "Controlled Substance" && _SelectedContact != "New Rx & Refill Request" && _SelectedContact != "Other" && _SelectedContact != "Direct Physician")
                            {
                                tls_btnAdd.Enabled = true;
                                tls_btnActive.Enabled = true;
                                tls_btnModify.Enabled = true;
                                //Sanjog -Added on 2011 Jan 17 to hide the button for e-pharmacy
                                tls_btnDelete.Enabled = true;
                            }
                            //if ((Convert.ToInt64(trvContacts.SelectedNode.Tag) == 5))
                            if (_SelectedContact == "e-Pharmacy")
                            {
                                tls_btnView.Enabled = true;
                                tls_btnModify.Enabled = false;
                                //Sanjog -Added on 2011 Jan 17 to hide the button for e-pharmacy
                                tls_btnDelete.Enabled = false;
                            }
                            if (_SelectedContact == "Direct Physician")
                            {
                                tls_btnView.Visible = false;
                                tls_btnDelete.Enabled = false;
                            }

                        }
                        if (c1ViewContacts.Rows.Count <= 1)
                        {
                            //tls_btnAdd.Enabled = false;
                            tls_btnActive.Enabled = false;
                            tls_btnModify.Enabled = false;
                            tls_btnView.Enabled = false;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (Contact != null) { Contact.Dispose(); Contact = null; }
            }

        }

        private void FilterDirectPhysician(string sSearchText = "")
        {            
            DataTable dt = null;
            DataView _dv = null;
            gloContacts.gloContact Contact = new gloContact(_databaseconnectionstring);

            dt = Contact.GetDirectPhysician(sSearchText);
            _dv = dt.DefaultView;
            if (Contact != null) { Contact.Dispose(); Contact = null; }
            if (_dv != null)
            {

                int width = pnlMain.Width;
                c1ViewContacts.Redraw = false;
              //  c1ViewContacts.Clear();
                c1ViewContacts.DataSource = null;
                c1ViewContacts.DataSource = _dv;
                c1ViewContacts.Rows.Fixed = 1;

                c1ViewContacts.SetData(0, COL_PHYContactID, "ContactID");
                c1ViewContacts.SetData(0, COL_PHYPhyisicianName, "Name");
                c1ViewContacts.SetData(0, COL_PHYFirstName, "FirstName");
                c1ViewContacts.SetData(0, COL_PHYLastName, "LastName");
                c1ViewContacts.SetData(0, COL_PHYName, "Name");
                c1ViewContacts.SetData(0, COL_PHYContactName, "Contact");
                c1ViewContacts.SetData(0, COL_PHYGender, "Gender");
                c1ViewContacts.SetData(0, COL_PHYAddressLine1, "Address 1");
                c1ViewContacts.SetData(0, COL_PHYAddressLine2, "Address 2");
                c1ViewContacts.SetData(0, COL_PHYCity, "City");
                c1ViewContacts.SetData(0, COL_PHYState, "State");
                c1ViewContacts.SetData(0, COL_PHYZIP, "Zip");
                c1ViewContacts.SetData(0, COL_PHYPhone, "Phone");
                c1ViewContacts.SetData(0, COL_PHYFax, "Fax");
                c1ViewContacts.SetData(0, COL_PHYMobile, "Mobile");
                c1ViewContacts.SetData(0, COL_PHYEmail, "Email");

                if (_SelectedContact == "Direct Physician")
                {
                    c1ViewContacts.SetData(0, COL_SPI, "SPI");
                    c1ViewContacts.Cols[COL_SPI].Visible = true;
                    c1ViewContacts.Cols[COL_SPI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1ViewContacts.SetData(0, COL_DIRECTADDRESS, "Direct Address");
                    c1ViewContacts.Cols[COL_DIRECTADDRESS].Visible = true;
                    c1ViewContacts.Cols[COL_DIRECTADDRESS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1ViewContacts.SetData(0, COL_Specialtytype, "Specialty Type1");
                    c1ViewContacts.Cols[COL_Specialtytype].Visible = true;
                    c1ViewContacts.Cols[COL_Specialtytype].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1ViewContacts.SetData(0, COL_ClinicName, "Clinic Name");
                    c1ViewContacts.Cols[COL_ClinicName].Visible = true;
                    c1ViewContacts.Cols[COL_ClinicName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1ViewContacts.SetData(0, COL_NPI, "NPI");
                    c1ViewContacts.Cols[COL_NPI].Visible = true;
                    c1ViewContacts.Cols[COL_NPI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                }

                c1ViewContacts.AutoSizeCol(COL_Name);
                c1ViewContacts.AutoSizeCol(COL_PhyisicianName);

                c1ViewContacts.Cols[COL_PHYName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYGender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYAddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYAddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYState].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ViewContacts.Cols[COL_PHYPhone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ViewContacts.Cols[COL_PHYFax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ViewContacts.Cols[COL_PHYMobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ViewContacts.Cols[COL_PHYEmail].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYContactName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ViewContacts.Cols[COL_PHYGender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1ViewContacts.Cols[COL_PHYContactID].Visible = false;
                c1ViewContacts.Cols[COL_PHYPhyisicianName].Visible = false;
                c1ViewContacts.Cols[COL_PHYContactName].Visible = true;
                c1ViewContacts.Cols[COL_PHYName].Visible = true;
                c1ViewContacts.Cols[COL_PHYLastName].Visible = false;
                c1ViewContacts.Cols[COL_PHYGender].Visible = true;
                c1ViewContacts.Cols[COL_PHYAddressLine1].Visible = true;
                c1ViewContacts.Cols[COL_PHYAddressLine2].Visible = true;
                c1ViewContacts.Cols[COL_PHYCity].Visible = true;
                c1ViewContacts.Cols[COL_PHYState].Visible = true;
                c1ViewContacts.Cols[COL_PHYZIP].Visible = true;
                c1ViewContacts.Cols[COL_PHYFax].Visible = true;
                c1ViewContacts.Cols[COL_PHYEmail].Visible = true;

                c1ViewContacts.AllowEditing = false;
                c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                c1ViewContacts.Rows[0].AllowEditing = false;
                c1ViewContacts.Redraw = true;

                c1ViewContacts.Cols[COL_PHYPhyisicianName].Visible = true;
                c1ViewContacts.Cols[COL_PHYGender].Visible = true;
                c1ViewContacts.Cols[COL_PHYContactName].Visible = false;
                c1ViewContacts.Cols[COL_PHYName].Visible = false;
                c1ViewContacts.Cols[COL_PHYFirstName].Visible = false;

                if (_SelectedContact == "Direct Physician")
                {
                    c1ViewContacts.Cols[COL_NPI].Visible = true;
                    c1ViewContacts.Cols[COL_DIRECTADDRESS].Visible = true;
                    c1ViewContacts.Cols[COL_SPI].Visible = true;
                    c1ViewContacts.Cols[COL_Specialtytype].Visible = true;
                    c1ViewContacts.Cols[COL_ClinicName].Visible = true;
                }

                if (c1ViewContacts.Rows.Count > 1)
                {
                    tls_btnView.Visible = false;
                    tls_btnDelete.Enabled = false;
                }
                if (c1ViewContacts.Rows.Count <= 1)
                {
                    tls_btnActive.Enabled = false;
                    tls_btnModify.Enabled = false;
                    tls_btnView.Enabled = false;
                }
            }            
        }


        // COMMENT BY SUDHIR 20100326 //
        //MaheshB
        //private void DesignGridForZip()
        //{
        //    gloDatabaseLayer.DBLayer ODB = null;
        //    DataTable dtZip = null;
        //    this.Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        dtZip = new DataTable();
        //        ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        ODB.Connect(false);
        //        string _strquery = "Select  nID, City, ST, ZIP, AreaCode, County from CSZ_MST";
        //        ODB.Retrive_Query(_strquery, out dtZip);
        //        if (dtZip != null && dtZip.Rows.Count > 0)
        //        {
        //            DataView _dv = null;
        //            _dv = dtZip.DefaultView;

        //            if (_dv != null)
        //            {
        //                c1ViewContacts.DataSource = null;
        //                c1ViewContacts.DataSource = _dv;
        //                c1ViewContacts.Rows.Fixed = 1;
        //                const int COL_ContactID = 0;
        //                c1ViewContacts.Cols.Count = 6;
        //                c1ViewContacts.SetData(0, COL_ContactID, "nID");
        //                c1ViewContacts.SetData(0, 1, "City");
        //                c1ViewContacts.SetData(0, 2, "State");
        //                c1ViewContacts.SetData(0, 3, "ZIP");
        //                c1ViewContacts.SetData(0, 4, "Area Code");
        //                c1ViewContacts.SetData(0, 5, "County");
        //                //c1ViewContacts.SetData(0, 6, "County");
        //                c1ViewContacts.Cols[0].Visible = false;
        //                c1ViewContacts.Cols[1].Visible = true;
        //                c1ViewContacts.Cols[2].Visible = true;
        //                c1ViewContacts.Cols[3].Visible = true;
        //                c1ViewContacts.Cols[4].Visible = true;
        //                c1ViewContacts.Cols[5].Visible = true;
        //                //c1ViewContacts.Cols[6].Visible=true;

        //            }
        //        }
        //        ODB.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messgeBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        if (ODB != null)
        //        {
        //            ODB.Dispose();
        //        }
        //        if (dtZip != null)
        //        {
        //            dtZip.Dispose();
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        private void DesignGridForZip(string sSearchText, bool RefreshGrid)
        {
            this.Cursor = Cursors.WaitCursor;

            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtZip=null;
            string _SearchString = "";
            string[] strSearchArray = null;
            string _SelectQuery = "";
            string _WhereQuery = "";
            try
            {

                if (RefreshGrid == true && txt_search.Text != "")
                {
                    txt_search.Clear();
                    return; // ON TEXT CLEAR THIS FUNCTION WILL CALL ON TEXT CHANGE, NO NEED TO EXECUTE CODE AGAIN
                }


                _SelectQuery = " SELECT TOP(100) nID, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST, ISNULL(Zip,'') As zip, ISNULL(AreaCode,'') AS AreaCode, ISNULL(county,'') AS county FROM CSZ_MST ";

                _SearchString = sSearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (_SearchString.StartsWith("*") == true)
                    _SearchString = _SearchString.Replace("*", "%");

                _SearchString = _SearchString.Replace("*", "[*]");

                //if (_SearchString.Length > 1)
                //    _SearchString = _SearchString.Substring(0, 1) + _SearchString.Substring(1);

                if (_SearchString.Trim() != "")
                    strSearchArray = _SearchString.Split(',');


                if (_SearchString.Trim() != "")
                {

                    if (strSearchArray.Length == 1)
                    {
                        _SearchString = strSearchArray[0].Trim();
                        _WhereQuery = _WhereQuery + " WHERE City Like '" + _SearchString + "%' OR " +
                                        " ST Like '" + _SearchString + "%' OR " +
                                        " zip Like '" + _SearchString + "%' OR " +
                                        " AreaCode Like '" + _SearchString + "%' OR " +
                                        " county Like '" + _SearchString + "%'";
                    }
                    else
                    {
                        //For Comma separated  value search
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            _SearchString = strSearchArray[i].Trim();
                            if (_SearchString.Trim() != "")
                            {
                                if (i == 0)
                                    _WhereQuery = _WhereQuery + " WHERE ";
                                else
                                    _WhereQuery = _WhereQuery + " AND ";

                                _WhereQuery = _WhereQuery + " (City Like '" + _SearchString + "%' OR " +
                                        " ST Like '" + _SearchString + "%' OR " +
                                        " zip Like '" + _SearchString + "%' OR " +
                                        " AreaCode Like '" + _SearchString + "%' OR " +
                                        " county Like '" + _SearchString + "%') ";
                            }
                        }
                    }

                }




              //  dtZip = new DataTable();
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _Query = _SelectQuery + " " + _WhereQuery + " ORDER BY Zip";

                oDB.Retrive_Query(_Query, out dtZip);
                oDB.Disconnect();
               
                    if (dtZip != null)
                    {
                       // c1ViewContacts.Clear();
                        c1ViewContacts.DataSource = null;
                        c1ViewContacts.DataSource = dtZip.DefaultView;
                        c1ViewContacts.Rows.Fixed = 1;
                        c1ViewContacts.Cols.Count = 6;
                        c1ViewContacts.SetData(0, 0, "nID");
                        c1ViewContacts.SetData(0, 1, "City");
                        c1ViewContacts.SetData(0, 2, "State");
                        c1ViewContacts.SetData(0, 3, "Zip");
                        c1ViewContacts.SetData(0, 4, "Area Code");
                        c1ViewContacts.SetData(0, 5, "County");
                        c1ViewContacts.Cols[0].Visible = false;
                        c1ViewContacts.Cols[1].Visible = true;
                        c1ViewContacts.Cols[2].Visible = true;
                        c1ViewContacts.Cols[3].Visible = true;
                        c1ViewContacts.Cols[4].Visible = true;
                        c1ViewContacts.Cols[5].Visible = true;

                    }
                    _Query = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                _SearchString = null;
                 strSearchArray = null;
                 _SelectQuery = null;
                 _WhereQuery = null;

                this.Cursor = Cursors.Default;
            }
        }

        private void DesignGridForCountry(string sSearchText, bool RefreshGrid,Int64 CountryID)
        {
            this.Cursor = Cursors.WaitCursor;
         
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtZip=null;
            string _SearchString = "";
            string[] strSearchArray = null;
            string _SelectQuery = "";
            string _WhereQuery = "";
            try
            {

                if (RefreshGrid == true && txt_search.Text != "")
                {
                    txt_search.Clear();
                    return; // ON TEXT CLEAR THIS FUNCTION WILL CALL ON TEXT CHANGE, NO NEED TO EXECUTE CODE AGAIN
                }


                _SelectQuery = " SELECT nID, ISNULL(sName,'') As Name,ISNULL(sCode,'') AS Code, ISNULL(sSubCode,'') AS SubCode,ISNULL(bIsBlocked,'') AS IsBlocked FROM Contacts_Country_MST ";

                _SearchString = sSearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (_SearchString.StartsWith("*") == true)
                    _SearchString = _SearchString.Replace("*", "%");

                _SearchString = _SearchString.Replace("*", "[*]");

                //if (_SearchString.Length > 1)
                //    _SearchString = _SearchString.Substring(0, 1) + _SearchString.Substring(1);

                if (_SearchString.Trim() != "")
                    strSearchArray = _SearchString.Split(',');


                if (_SearchString.Trim() != "")
                {

                    if (strSearchArray.Length == 1)
                    {
                        _SearchString = strSearchArray[0].Trim();
                        _WhereQuery = _WhereQuery + " WHERE sName Like '" + _SearchString + "%' OR " +
                                        " sCode Like '" + _SearchString + "%'";
                    }
                    else
                    {
                        //For Comma separated  value search
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            _SearchString = strSearchArray[i].Trim();
                            if (_SearchString.Trim() != "")
                            {
                                if (i == 0)
                                    _WhereQuery = _WhereQuery + " WHERE ";
                                else
                                    _WhereQuery = _WhereQuery + " AND ";

                                _WhereQuery = _WhereQuery + " (sName Like '" + _SearchString + "%' OR " +
                                        " sCode Like '" + _SearchString + "%') ";
                            }
                        }
                    }

                }




               // dtZip = new DataTable();
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _Query = _SelectQuery + " " + _WhereQuery + " ORDER BY sName";

                oDB.Retrive_Query(_Query, out dtZip);
             

                 if (dtZip != null)
                {
                   // c1ViewContacts.Clear();
                    c1ViewContacts.DataSource = null;
                    c1ViewContacts.DataSource = dtZip.DefaultView;
                    c1ViewContacts.Rows.Fixed = 1;
                    c1ViewContacts.Cols.Count = 5;

                    c1ViewContacts.SetData(0, 0, "nID");
                    c1ViewContacts.SetData(0, 1, "Country");
                    c1ViewContacts.SetData(0, 2, "Code");
                    c1ViewContacts.SetData(0, 3, "SubCode");
                    c1ViewContacts.SetData(0, 4, "Blocked");
                    
                    c1ViewContacts.Cols[0].Visible = false;
                    c1ViewContacts.Cols[1].Visible = true;
                    c1ViewContacts.Cols[2].Visible = true;
                    c1ViewContacts.Cols[3].Visible = false;
                    c1ViewContacts.Cols[4].Visible = true;

                    c1ViewContacts.Cols[1].Width = 500;
                    c1ViewContacts.Cols[2].Width = 50;
                    c1ViewContacts.Cols[4].Width = 75;

                  
                    c1ViewContacts.Cols[c1ViewContacts.Cols["IsBlocked"].Index].Move(1);
                    c1ViewContacts.Cols[c1ViewContacts.Cols["Name"].Index].Move(3);
                    
                    c1ViewContacts.ExtendLastCol = true;

                }
                 if (CountryID > 0)
                 {
                     _Contactid = CountryID;
                     string ID1 = Convert.ToString(CountryID);
                     if (CountryID == 0 && c1ViewContacts.Rows.Count > 1)
                     {
                         c1ViewContacts.Row = 1;
                     }
                     else
                     {
                         int RowIndex = c1ViewContacts.FindRow(ID1, 0, COL_ContactID, false, false, false);
                         c1ViewContacts.Row = RowIndex;
                     }
                 }
                 else
                 {
                     if (c1ViewContacts.Row != -1)
                     {
                         _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                     }
                 }
                if (_Contactid != 0)
                {

                    gloContact ogloContacts = new gloContact(_databaseconnectionstring);
                    bool _result = ogloContacts.IsCountryBlocked(_Contactid);
                    if (_result  )
                    {                        
                            tls_btnActive.Enabled = true;
                            tls_btnInActive.Enabled = false;
                        
                    }
                    else 
                    {  
                            tls_btnActive.Enabled = false;
                            tls_btnInActive.Enabled = true;                        
                    }
                    if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                _SearchString = null;
                 strSearchArray = null;
                 _SelectQuery = null;
                 _WhereQuery = null;

                this.Cursor = Cursors.Default;
            }
        }


        public void FillContactTypeTree()
        {
            try
            {
                //Class instance of Contact.
                //gloContacts.gloContact ContactType = new gloContact(_databaseconnectionstring);

                //DataTable dt = new DataTable();

                //// Get the Contact types from Database.
                //dt = ContactType.GetContactTypes();
                //if (dt != null)
                //{
                //    //Binding to Tree Views..
                //    if (dt.Rows.Count > 0)
                //    {

                TreeNode oNode;
                //Clear any previous nodes.
                trvContacts.Nodes.Clear();

                //Start creating treeview.
                trvContacts.Nodes.Add("Contact Type");

                //Shubhangi 20091103
                //Add Insurance Company Master
                oNode = new TreeNode();
                oNode.Text = "Insurance Company";
                oNode.Tag = 8;
                oNode.ImageIndex = 9;
                oNode.SelectedImageIndex = 9;
                trvContacts.Nodes[0].Nodes.Add(oNode);
                //End Shubhangi

                //----------------Abhisekh 10/02/2010 start------------------------
                //Add Insurance Reporting Category Master
                oNode = new TreeNode();
                oNode.Text = "Insurance Reporting Category";
                oNode.Tag = 11;
                oNode.ImageIndex = 10;
                oNode.SelectedImageIndex = 10;
                trvContacts.Nodes[0].Nodes.Add(oNode);
                //End Abhisekh---------------------------------------------------------

                ////Setting the treenode environment.
                //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //{
                oNode = new TreeNode();
                oNode.Text = "Insurance Plan"; //Physician ,Pharmacy , Hospital //dt.Rows[i][0].ToString();
                oNode.Tag = 1;
                oNode.ImageIndex = 14;
                oNode.SelectedImageIndex = 14;
                //trvContacts.Nodes[0].Nodes.Add(oNode);

                TreeNode oNodeChild = new TreeNode();
                oNodeChild.Text = "Inactive Insurance Plan";
                oNodeChild.Tag = 12;
                oNodeChild.ImageIndex = 15;
                oNodeChild.SelectedImageIndex = 15;
                oNode.Nodes.Add(oNodeChild);
                trvContacts.Nodes[0].Nodes.Add(oNode);


                oNode = new TreeNode();
                oNode.Text = "Collection Agency";
                oNode.Tag = 15;
                oNode.ImageIndex = 19;
                oNode.SelectedImageIndex = 19;

                TreeNode oNodeChild_CA = new TreeNode();
                oNodeChild_CA.Text = "Inactive Collection Agency";
                oNodeChild_CA.Tag = 16;
                oNodeChild_CA.ImageIndex = 18;
                oNodeChild_CA.SelectedImageIndex = 18;
                oNode.Nodes.Add(oNodeChild_CA);
                trvContacts.Nodes[0].Nodes.Add(oNode);


                oNode = new TreeNode();
                oNode.Text = "Physician";// ,Pharmacy , Hospital //dt.Rows[i][0].ToString();
                oNode.Tag = 2;
                oNode.ImageIndex = 3;
                oNode.SelectedImageIndex = 3;
                trvContacts.Nodes[0].Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "Hospital"; //dt.Rows[i][0].ToString();
                oNode.Tag = 4;
                oNode.ImageIndex = 4;
                oNode.SelectedImageIndex = 4;
                trvContacts.Nodes[0].Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "Pharmacy"; // , Hospital //dt.Rows[i][0].ToString();
                oNode.Tag = 3;
                oNode.ImageIndex = 1;
                oNode.SelectedImageIndex = 1;
                trvContacts.Nodes[0].Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "e-Pharmacy"; // e -Pharmacy
                oNode.Tag = 5;
                oNode.ImageIndex = 5;
                oNode.SelectedImageIndex = 5;
                trvContacts.Nodes[0].Nodes.Add(oNode);

                TreeNode oNode1 = new TreeNode(); //New Rx
                oNode1.Text = "New Rx";
                oNode1.Tag = 6;
                oNode1.ImageIndex = 6;
                oNode1.SelectedImageIndex = 6;
                oNode.Nodes.Add(oNode1);

                oNode1 = new TreeNode();
                oNode1.Text = "New Rx & Refill Request";
                oNode1.Tag = 7;
                oNode1.ImageIndex = 6;
                oNode1.SelectedImageIndex = 6;
                oNode.Nodes.Add(oNode1);

                oNode1 = new TreeNode();
                oNode1.Text = "Controlled Substance";
                oNode1.Tag = 7;
                oNode1.ImageIndex = 6;
                oNode1.SelectedImageIndex = 6;
                oNode.Nodes.Add(oNode1);

                oNode1 = new TreeNode();
                oNode1.Text = "Other";
                oNode1.Tag = 7;
                oNode1.ImageIndex = 6;
                oNode1.SelectedImageIndex = 6;
                oNode.Nodes.Add(oNode1);

                oNode = new TreeNode();
                oNode.Text = "Others"; // , Hospital //dt.Rows[i][0].ToString();
                oNode.Tag = 10;
                oNode.ImageIndex = 11;
                oNode.SelectedImageIndex = 11;
                trvContacts.Nodes[0].Nodes.Add(oNode);

                // Zip Code Master in Contact is only for gloPM 
                //it will be activate only for gloPM                
                if (_messgeBoxCaption == "gloPM")
                {
                    trvContacts.Nodes.Add("Zip Code");
                    trvContacts.Nodes[1].ImageIndex = 8;
                    trvContacts.Nodes[1].SelectedImageIndex = 8;

                }

                oNode = new TreeNode();
                oNode.Text = "Country"; // , Hospital //dt.Rows[i][0].ToString();
                oNode.Tag = 13;
                oNode.ImageIndex = 16;
                oNode.SelectedImageIndex = 16;
                trvContacts.Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "Surescripts Catalogue"; //"Direct Physician"; 
                oNode.Tag = 14;
                oNode.ImageIndex = 17;
                oNode.SelectedImageIndex = 17;
                trvContacts.Nodes[0].Nodes.Add(oNode);


                //Select first node.
                trvContacts.SelectedNode = trvContacts.Nodes[0].FirstNode;
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void AssignUserRights()
        {
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            try
            {
              

                if (_UserName.Trim() != "")
                {
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);

                    if (trvContacts.Nodes[0].GetNodeCount(false) > 0)
                    {
                        for (int i = trvContacts.Nodes[0].GetNodeCount(false) - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(trvContacts.Nodes[0].Nodes[i].Tag) != "8") //Added By MaheshB For Zip Control
                            {
                                if (trvContacts.Nodes[0].Nodes[i].Text == "Insurance Plan")
                                {
                                    if (!oClsgloUserRights.Contacts_Insurance)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                                else if (trvContacts.Nodes[0].Nodes[i].Text == "Physician")
                                {
                                    if (!oClsgloUserRights.Contacts_Physician)
                                        trvContacts.Nodes[0].Nodes[i].Remove();

                                }
                                else if (trvContacts.Nodes[0].Nodes[i].Text == "Hospital")
                                {
                                    if (!oClsgloUserRights.Contacts_Hospital)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                                else if (trvContacts.Nodes[0].Nodes[i].Text == "Pharmacy")
                                {
                                    if (!oClsgloUserRights.Contacts_Pharmacy)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                                else if (trvContacts.Nodes[0].Nodes[i].Text == "e-Pharmacy")
                                {
                                    if (!oClsgloUserRights.Contacts_ePharmacy)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                                //Shubhangi
                                else if (trvContacts.Nodes[0].Nodes[i].Text == "Others")
                                {
                                    if (!oClsgloUserRights.Contact_Others)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                                //else if (trvContacts.Nodes[0].Nodes[i].Text == "Insurance Company")
                                //{
                                //    if (!oClsgloUserRights.Contact_InsuranceCompany)
                                //        trvContacts.Nodes[0].Nodes[i].Remove();
                                //}
                                //End
                            }
                            else
                            {
                                if (trvContacts.Nodes[0].Nodes[i].Text == "Insurance Company")
                                {
                                    if (!oClsgloUserRights.Contact_InsuranceCompany)
                                        trvContacts.Nodes[0].Nodes[i].Remove();
                                }
                            }
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }
        //Shubhangi
        private void FillInsuranceCompany()
        {
            gloContact ogloContacts = null;
            try
            {
                int InsuranceCompanySelectIndex = 0;
                if (cmbInsuranceCompany.SelectedIndex != -1)
                {
                    InsuranceCompanySelectIndex = cmbInsuranceCompany.SelectedIndex;
                }
                ogloContacts = new gloContact(_databaseconnectionstring);
                DataTable dtInsuranceCompany = ogloContacts.GetInsuranceCompanyDetails();
                DataRow dr = dtInsuranceCompany.NewRow();
                dr["nID"] = 0;
                dr["sDescription"] = "All";
                dtInsuranceCompany.Rows.InsertAt(dr, 0);
                dtInsuranceCompany.AcceptChanges();

                cmbInsuranceCompany.DisplayMember = "sDescription";
                cmbInsuranceCompany.DataSource = dtInsuranceCompany;

                cmbInsuranceCompany.ValueMember = "nID";
                cmbInsuranceCompany.Refresh();
                cmbInsuranceCompany.SelectedIndex = InsuranceCompanySelectIndex;

                cmbInsuranceCompany_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
        }
        //End

        private void FillCollectionAgency(bool isBlocked = false)
        {
            clsCollectionAgency oCollectionAgency = new clsCollectionAgency(_databaseconnectionstring);
            DataTable dtCollectionAgency = oCollectionAgency.GetCollectionAgency(0, isBlocked);
            try
            {

                if (dtCollectionAgency != null)
                {
                    DesignGridforCollectionAgency(dtCollectionAgency);
                }

                if (c1ViewContacts.Rows.Count == 1)
                {
                    tls_btnInActive.Enabled = false;
                    tls_btnModify.Enabled = false;
                }
                else
                {
                    tls_btnInActive.Enabled = true;
                    tls_btnModify.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oCollectionAgency != null)
                {
                    oCollectionAgency.Dispose();
                    oCollectionAgency = null;
                }

                if (dtCollectionAgency != null)
                {
                    dtCollectionAgency.Dispose();
                    dtCollectionAgency = null;
                }
            }
        }

        private void DesignGridforCollectionAgency(DataTable dtCollectionAgency)
        {

          //  c1ViewContacts.Clear();
            c1ViewContacts.DataSource = null;

            c1ViewContacts.Clear();

            c1ViewContacts.DataSource = dtCollectionAgency.DefaultView;
            c1ViewContacts.Rows.Fixed = 1;

          
          

            c1ViewContacts.SetData(0, COL_CollectionAgency_ContactID, "Contact ID");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Name, "Collection Agency");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Contact, "Contact");
            c1ViewContacts.SetData(0, COL_CollectionAgency_AddressLine1, "Address 1");
            c1ViewContacts.SetData(0, COL_CollectionAgency_AddressLine2, "Address 2");
            c1ViewContacts.SetData(0, COL_CollectionAgency_City, "City");
            c1ViewContacts.SetData(0, COL_CollectionAgency_State, "State");
            c1ViewContacts.SetData(0, COL_CollectionAgency_ZIP, "Zip");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Phone, "Phone");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Fax, "Fax");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Email, "Email");
            c1ViewContacts.SetData(0, COL_CollectionAgency_URL, "URL");
            c1ViewContacts.SetData(0, COL_CollectionAgency_ContactType, "Contact Type");
            c1ViewContacts.SetData(0, COL_CollectionAgency_BadDebtFeeType, "Collection Fee Type");
            c1ViewContacts.SetData(0, COL_CollectionAgency_PercentofSelfPayBalance, "% of Self Pay Balance");
            c1ViewContacts.SetData(0, COL_CollectionAgency_Flatfee, "Flat fee");


            c1ViewContacts.Cols[COL_CollectionAgency_ContactID].Visible = false;
            c1ViewContacts.Cols[COL_CollectionAgency_ContactType].Visible = false;
            c1ViewContacts.Cols[COL_CollectionAgency_BadDebtFeeType].Visible = false;

            c1ViewContacts.Cols[COL_CollectionAgency_ContactID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Contact].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_City].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Fax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_URL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_ContactType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_BadDebtFeeType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_PercentofSelfPayBalance].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_CollectionAgency_Flatfee].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;



            c1ViewContacts.Cols[COL_CollectionAgency_ContactID].Width = 0;
            c1ViewContacts.Cols[COL_CollectionAgency_Name].Width = Width / 6;
            c1ViewContacts.Cols[COL_CollectionAgency_Contact].Width = Width / 12;
            c1ViewContacts.Cols[COL_CollectionAgency_AddressLine1].Width = Width / 7;
            c1ViewContacts.Cols[COL_CollectionAgency_AddressLine2].Width = Width / 7;
            c1ViewContacts.Cols[COL_CollectionAgency_City].Width = Width / 11;
            c1ViewContacts.Cols[COL_CollectionAgency_State].Width = Width / 11;
            c1ViewContacts.Cols[COL_CollectionAgency_ZIP].Width = Width / 14;
            c1ViewContacts.Cols[COL_CollectionAgency_Phone].Width = Width / 14;
            c1ViewContacts.Cols[COL_CollectionAgency_Fax].Width = Width / 13;
            c1ViewContacts.Cols[COL_CollectionAgency_Email].Width = Width / 9;
            c1ViewContacts.Cols[COL_CollectionAgency_URL].Width = Width / 9;
            c1ViewContacts.Cols[COL_CollectionAgency_ContactType].Width = 0;
            c1ViewContacts.Cols[COL_CollectionAgency_BadDebtFeeType].Width = 0;
            c1ViewContacts.Cols[COL_CollectionAgency_PercentofSelfPayBalance].Width = Width / 9;
            c1ViewContacts.Cols[COL_CollectionAgency_Flatfee].Width = Width / 9;
            
        }



        private void FillSpecialityType()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtSpeciality = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "Select nID,sDownloadName  from SpecialityTypeMst where bIsDisplay='True' Order by sDownloadName";

                oDB.Retrive_Query(strQuery, out dtSpeciality);
                if (dtSpeciality != null)
                {
                    cmbSpeciality.SelectionChangeCommitted  -= new EventHandler(cmbSpeciality_SelectionChangeCommitted);
                    cmbSpeciality.DataSource = dtSpeciality.Copy();
                    cmbSpeciality.ValueMember = dtSpeciality.Columns["nID"].ColumnName;
                    cmbSpeciality.DisplayMember = dtSpeciality.Columns["sDownloadName"].ColumnName;
                    cmbSpeciality.Refresh();
                    cmbSpeciality.SelectedIndex = 0;
                    cmbSpeciality.SelectionChangeCommitted += new EventHandler(cmbSpeciality_SelectionChangeCommitted);
                    
                }

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

                if (dtSpeciality != null)
                {
                    dtSpeciality.Dispose();
                    dtSpeciality = null;
                }

                strQuery = null;
            }
        }

        private void trvContacts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblInsuranceCompany.Visible = false;
            cmbInsuranceCompany.Visible = false;
            lblSpecialityType.Visible = false;
            cmbSpeciality.Visible = false; 
            tls_btnDelete.Enabled = true;
            tls_btnDelete.Visible = true;
            tls_btnActive.Visible = false;
            tls_btnInActive.Visible = false;

            txt_search.Clear();
            if (trvContacts.SelectedNode.Text == "e-Pharmacy" || trvContacts.SelectedNode.Text == "New Rx" || trvContacts.SelectedNode.Text == "New Rx & Refill Request" || trvContacts.SelectedNode.Text == "Other" || trvContacts.SelectedNode.Text == "Controlled Substance")
            {
                if (cmbSpeciality.DataSource == null)
                {
                    FillSpecialityType();
                }
                else
                {
                    cmbSpeciality.SelectionChangeCommitted -= new EventHandler(cmbSpeciality_SelectionChangeCommitted);
                    cmbSpeciality.Refresh();
                    cmbSpeciality.SelectedIndex = 0;
                    cmbSpeciality.SelectionChangeCommitted += new EventHandler(cmbSpeciality_SelectionChangeCommitted);
                }
            }

            tls_btnRemoveDuplicate.Visible = false;
            if (trvContacts.SelectedNode.Text == "Zip Code")//if Selected node is parent node
            {
               _SelectedContact = trvContacts.SelectedNode.Text.ToString();
                //tls_btnView.Visible = false;
                //lbl_Search.Text = "  ";
                //tls_btnAdd.Enabled = false;
                //tls_btnDelete.Enabled = false;
                //tls_btnModify.Enabled = false;
                //tls_btnRefresh.Enabled = false;
                //pnl_Search.Visible = false;
                //tls_btnClose.Enabled = true;
                //c1ViewContacts.DataSource = null;
                //c1ViewContacts.Rows.Count = 0;
            }
            else if (trvContacts.SelectedNode.Text == "Country")//if Selected node is parent node
            {
                _SelectedContact = trvContacts.SelectedNode.Text.ToString();
                //tls_btnView.Visible = false;
                //lbl_Search.Text = "  ";
                //tls_btnAdd.Enabled = false;
                //tls_btnDelete.Enabled = false;
                //tls_btnModify.Enabled = false;
                //tls_btnRefresh.Enabled = false;
                //pnl_Search.Visible = false;
                //tls_btnClose.Enabled = true;
                //c1ViewContacts.DataSource = null;
                //c1ViewContacts.Rows.Count = 0;
            }
            else if (trvContacts.SelectedNode.Level == 1)//if Selected node is a contact type 
            {
                if (string.Compare(trvContacts.SelectedNode.Text.ToString(),"Surescripts Catalogue",true)==0)  //(trvContacts.SelectedNode.Text.ToString() == "Surescripts Catalogue")
                {  _SelectedContact = "Direct Physician"; }
                else
                {  _SelectedContact = trvContacts.SelectedNode.Text.ToString(); }

                //but not e-pharmacy(tag = 5) or other (tag = 9)
                if (Convert.ToInt64(trvContacts.SelectedNode.Tag) != 5 && Convert.ToInt64(trvContacts.SelectedNode.Tag) != 9 && Convert.ToInt64(trvContacts.SelectedNode.Tag) != 14)
                {
                    if (Convert.ToInt64(trvContacts.SelectedNode.Tag) == 1 || Convert.ToInt64(trvContacts.SelectedNode.Tag) == 15)
                    {
                        tls_btnDelete.Visible = false;
                        tls_btnActive.Enabled = false;
                        tls_btnActive.Visible = false;
                        tls_btnInActive.Enabled = true;
                        tls_btnInActive.Visible = true;
                    }

                    tls_btnView.Visible = false;
                    tls_btnAdd.Enabled = true;
                    tls_btnAdd.Visible = true;
                    tls_btnClose.Enabled = true;
                    
                    tls_btnModify.Enabled = true;
                    tls_btnModify.Visible = true;
                    tls_btnRefresh.Enabled = true;
                    tls_Strip.Enabled = true;
                    pnl_Search.Visible = true;

                    if (Convert.ToInt64(trvContacts.SelectedNode.Tag) == 1)
                    {
                        tls_btnCopyAs.Visible = true;
                        tls_btnCopyMultiple.Visible = true;
                        tls_btnAssignFeeScheduled.Visible = true;
                    }
                    else
                    {
                        tls_btnCopyAs.Visible = false;
                        tls_btnCopyMultiple.Visible = false;
                        tls_btnAssignFeeScheduled.Visible = false;
                    }
                }
                else //if selected node is child of e-pharmacy
                {
                    lbl_Search.Text = "  Search : ";
                    tls_btnAdd.Enabled = false;
                    tls_btnActive.Enabled = false;
                    tls_btnModify.Enabled = true;
                    tls_btnRefresh.Enabled = true;
                    pnl_Search.Visible = true;
                    tls_btnClose.Enabled = true;
                    tls_btnView.Visible = true;
                }
            }

            else if (trvContacts.SelectedNode.Level == 2)
            {
                _SelectedContact = trvContacts.SelectedNode.Text.ToString();
                if (Convert.ToInt64(trvContacts.SelectedNode.Tag) == 12 || Convert.ToInt64(trvContacts.SelectedNode.Tag) == 16)
                {
                    tls_btnDelete.Visible = false;
                    tls_btnView.Visible = false;
                    tls_btnAdd.Visible = false;
                    tls_btnClose.Enabled = true;
                    tls_btnActive.Visible = true;
                    tls_btnActive.Enabled = true;
                    tls_btnInActive.Visible = false;
                    tls_btnInActive.Enabled = false;
                    //tls_btnModify.Enabled = false;
                    //tls_btnModify.Visible = false;
                    tls_btnRefresh.Enabled = true;
                    tls_Strip.Enabled = true;
                    pnl_Search.Visible = true;
                    tls_btnCopyAs.Visible = false;
                    tls_btnCopyMultiple.Visible = false;
                    tls_btnAssignFeeScheduled.Visible = false;
                }
                else
                {
                    _SelectedContact = trvContacts.SelectedNode.Text.ToString();
                    lbl_Search.Text = "  Search : ";
                    tls_btnAdd.Enabled = false;
                    tls_btnActive.Enabled = false;
                    tls_btnModify.Enabled = false;
                    tls_btnRefresh.Enabled = true;
                    pnl_Search.Visible = true;
                    tls_btnClose.Enabled = true;
                    tls_btnView.Visible = true;
                    //sanjog-Added on 2011 Jan 18 to enable the delete button for the e-pharmacy
                    if (Convert.ToInt64(trvContacts.SelectedNode.Parent.Tag) == 5)
                    {
                        tls_btnDelete.Enabled = false;
                    }
                    else
                    {
                        tls_btnDelete.Enabled = true;
                    }
                    //sanjog-Added on 2011 Jan 18 to enable the delete button for the e-pharmacy
                    tls_btnCopyAs.Visible = false;
                    tls_btnCopyMultiple.Visible = false;
                    tls_btnAssignFeeScheduled.Visible = false;
                }
            }

            //Change search label text 
            //20100312
            //if (trvContacts.SelectedNode.Text.ToString() == "Insurance Plan")
            if (_SelectedContact == "Insurance Plan")
            {
                lblInsuranceCompany.Visible = true;
                cmbInsuranceCompany.Visible = true;
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                // _SelectedContact = "Insurance Plan";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Plan", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //20100312
            // else if (trvContacts.SelectedNode.Text.ToString() == "Insurance Reporting Category")
            else if (_SelectedContact == "Inactive Insurance Plan")
            {
                lblInsuranceCompany.Visible = true;
                cmbInsuranceCompany.Visible = true;            
            }
            else if (_SelectedContact == "Collection Agency")
            {
                lblInsuranceCompany.Visible = false;
                cmbInsuranceCompany.Visible = false;
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                // _SelectedContact = "Insurance Plan";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.View, "View Collection Agency", 0, 0, 0, ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                //
            }
            else if (_SelectedContact == "Inactive Collection Agency")
            {
                lblInsuranceCompany.Visible = false;
                cmbInsuranceCompany.Visible = false;
            }
            else if (_SelectedContact == "Insurance Reporting Category")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                // _SelectedContact = "Insurance Reporting Category";
               // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Reporting Category", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Reporting Category", 0, 0, 0, ActivityOutCome.Success);
                //

            }
            //Shubhangi 20091103
            // else if (trvContacts.SelectedNode.Text.ToString() == "Insurance Company")
            else if (_SelectedContact == "Insurance Company")
            {

                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //_SelectedContact = "Insurance Company";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Company", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Company", 0, 0, 0, ActivityOutCome.Success);
                //

            }
            //20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "Physician")
            else if (_SelectedContact == "Physician" || _SelectedContact ==  "Direct Physician")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //  _SelectedContact = "Physician";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.View, "View Physician", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.View, "View Physician", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "Pharmacy")
            else if (_SelectedContact == "Pharmacy")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //_SelectedContact = "Pharmacy";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.View, "View Pharmacy", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.View, "View Pharmacy", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "Hospital")
            else if (_SelectedContact == "Hospital")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //_SelectedContact = "Hospital";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //Shubhangi 20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "Others")
            else if (_SelectedContact == "Others")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //_SelectedContact = "Others";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Others", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //End
            else if (Convert.ToString(trvContacts.SelectedNode.Tag) == "8")
            {
                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";

                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
            }
            else if (_SelectedContact == "Country")
            {

                tls_btnRemoveDuplicate.Visible = false;
                lbl_Search.Text = "  Search : ";
                //_SelectedContact = "Insurance Company";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Company", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.View, "View Insurance Company", 0, 0, 0, ActivityOutCome.Success);
                //

            }

            //Added for e-pharmacy  and other By Sandip Darade
            #region "e-pharmacy and other"
            //20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "e-Pharmacy")
            else if (_SelectedContact == "e-Pharmacy")
            {
                lbl_Search.Text = "  Search : ";
                // _SelectedContact = "e-Pharmacy";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View e-Pharmacy", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            //20100312
            //else if (trvContacts.SelectedNode.Text.ToString() == "New Rx")
            else if (_SelectedContact == "New Rx")
            {
                lbl_Search.Text = "  Search : ";
                //  _SelectedContact = "New Rx";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View New Rx", 0, 0, 0, ActivityOutCome.Success);
                //
            }

           // else if (trvContacts.SelectedNode.Text.ToString() == "New Rx & Refill Request")
            else if (_SelectedContact == "New Rx & Refill Request")
            {
                lbl_Search.Text = "  Search : ";
                //  _SelectedContact = "New Rx & Refill Request";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View New Rx & Refill Request", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            // else if (trvContacts.SelectedNode.Text.ToString() == "New Rx & Refill Request")
            else if (_SelectedContact == "Controlled Substance")
            {
                lbl_Search.Text = "  Search : ";
                //  _SelectedContact = "New Rx & Refill Request";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Controlled Substance", 0, 0, 0, ActivityOutCome.Success);
                //
            }
            // else if (trvContacts.SelectedNode.Text.ToString() == "Others")
            else if (_SelectedContact == "Others")
            {
                lbl_Search.Text = "  Search : ";
                //  _SelectedContact = "Others";
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Hospital", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.View, "View Others", 0, 0, 0, ActivityOutCome.Success);
                //
            }


            #endregion"e-pharmacy and other"


            //For clearing the variable to avoid any show select after treenode is cahnged.
            _Contactid = 0;
            if (e.Node.Level != 0) //8 For Zip //MaheshB
            {
                if (cmbSpeciality.DataSource != null)
                {
                    txt_search.Text = "";
                    if (cmbSpeciality.Text == "All Without MailOrder")
                    {
                        DesignGrid(0, "0");
                    }
                    else
                    {
                        DesignGrid(0, cmbSpeciality.Text);
                    }
                    c1ViewContacts.Focus();

                }
                else
                {
                    DesignGrid(0);
                }
            }
            else if (e.Node.Text == "Zip Code")
            {
                //e.Node.ImageIndex = 8;
                lbl_Search.Text = "  Search : ";
                _SelectedContact = "Zip Code";
                tls_btnView.Visible = false;
                tls_btnAdd.Enabled = true;
                tls_btnClose.Enabled = true;
                tls_btnActive.Enabled = true;
                tls_btnModify.Enabled = true;
                tls_btnRefresh.Enabled = true;
                tls_Strip.Enabled = true;
                pnl_Search.Visible = true;
                DesignGridForZip("", true);
            }
            else if (e.Node.Text == "Country")
            {
                //e.Node.ImageIndex = 8;
                lbl_Search.Text = "  Search : ";
                _SelectedContact = "Country";
                tls_btnView.Visible = false;
                tls_btnAdd.Enabled = true;
                tls_btnClose.Enabled = true; 
                tls_btnActive.Visible = true;
                tls_btnActive.Enabled = true;
                tls_btnInActive.Visible = true;
                tls_btnInActive.Enabled = true;
                tls_btnModify.Enabled = true;
                tls_btnRefresh.Enabled = true;
                tls_Strip.Enabled = true;
                tls_btnDelete.Visible = false;
                pnl_Search.Visible = true;
                DesignGridForCountry("", true,0);
            }


        }

        #region "Tool Strip Events"

        private void tls_Strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {

                if (c1ViewContacts.Row != -1)
                {
                    _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                }
                
                //20100312
                //switch (Convert.ToString(trvContacts.SelectedNode.Text))
                switch (Convert.ToString(_SelectedContact))
                {
                    case "Physician":
                    case "Direct Physician":
                        switch (e.ClickedItem.Tag.ToString())
                        {

                            case "Add":
                                {
                                    frmSetupPhysician ofrmAddContact = new frmSetupPhysician(_databaseconnectionstring);
                                    ofrmAddContact.CallFrom = _SelectedContact;
                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);

                                }
                                break;
                            case "Modify":
                                {

                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                        frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                                        ofrmModifyContact.CallFrom = _SelectedContact;
                                        ofrmModifyContact.ShowDialog(this);
                                        ofrmModifyContact.Dispose();
                                        DesignGrid(_Contactid);
                                    }

                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (_Contactid != 0)
                                        {
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            try
                                            {

                                                if (ogloContactsforDelete.IscontactAssignToPatient(_Contactid) == true)
                                                {
                                                    MessageBox.Show(this, "This contact cannot be deleted as it is already assigned to a patient. ", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    return;
                                                }

                                                if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                {

                                                    // ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Physician deleted", 0, _Contactid, 0, ActivityOutCome.Success);
                                                    DesignGrid(0);

                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete physician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                ogloContactsforDelete.Dispose();
                                            }
                                        }

                                    }
                                }
                                // DesignGrid(0);
                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate physician?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Physician);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete duplicate hysician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":                               
                                DesignGrid(0);
                                //if (c1ViewContacts.Rows.Count > 1)
                                //{
                                //    c1ViewContacts.Row = 1;
                                //}
                                break;

                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;
                                break;
                        }
                        break;
                    //Shubhangi 20091103
                    //Add case for Insurance company
                    case "Insurance Company":
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmInsuranceCompany ofrmAddInsuranceCompany = new frmInsuranceCompany(_databaseconnectionstring);
                                        ofrmAddInsuranceCompany.ShowInTaskbar = false;
                                        ofrmAddInsuranceCompany.ShowDialog(this);

                                        ofrmAddInsuranceCompany.Dispose();
                                        DesignGrid(0);
                                    }
                                    break;
                                case "Modify":
                                    {
                                        try
                                        {
                                            if (c1ViewContacts.Rows.Count > 1)
                                            {
                                                frmInsuranceCompany ofrmcmpny = new frmInsuranceCompany(_Contactid, _databaseconnectionstring);
                                                ofrmcmpny.ShowDialog(this);
                                                ofrmcmpny.Dispose();
                                                DesignGrid(0);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                    }
                                    break;
                                case "Delete":
                                    {
                                        try
                                        {
                                            if (c1ViewContacts.Rows.Count > 1)
                                            {
                                                gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);

                                                //Problem# 382 Integrated from 7022 - If insurance company is used for billing then connot delete that insurance company.
                                                if (oContact.IsInsuranceCompanyUsed(_Contactid) == true)
                                                {
                                                    MessageBox.Show("Cannot remove this insurance company. Insurance company is used for billing.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return;
                                                }
                                                //
                                                string _WarningString;

                                                if (oContact.IsInsuranceCompanyHasPlans(_Contactid) == true)
                                                    _WarningString = "Insurance company has plans associated, do you still want to delete this contact?";
                                                else
                                                    _WarningString = "Are you sure you want to delete this contact?";

                                                if (MessageBox.Show(this, _WarningString, _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                {
                                                    gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);

                                                    try
                                                    {
                                                        if (_Contactid > 0)
                                                        {
                                                            ogloContacts.DeleteInsuranceCompany(_Contactid);
                                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                            DesignGrid(0);

                                                        }
                                                    }

                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                                    }
                                                    finally
                                                    {
                                                        ogloContacts.Dispose();
                                                        ogloContacts = null;
                                                        oContact.Dispose();
                                                        oContact = null;
                                                    }
                                                }


                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }


                                    }

                                    break;
                                case "Refresh":
                                    {
                                        txt_search.Clear();
                                        txt_search.Focus();
                                        DesignGrid(0);
                                    }
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            }

                        }

                        break;


                    case "Insurance Reporting Category":
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmInsuranceReportingCategory ofrmAddInsuranceReportingCategory = new frmInsuranceReportingCategory(_databaseconnectionstring);
                                        ofrmAddInsuranceReportingCategory.ShowInTaskbar = false;
                                        ofrmAddInsuranceReportingCategory.ShowDialog(this);

                                        ofrmAddInsuranceReportingCategory.Dispose();
                                        DesignGrid(0);
                                    }
                                    break;
                                case "Modify":
                                    {
                                        try
                                        {
                                            if (c1ViewContacts.Rows.Count > 1)
                                            {
                                                frmInsuranceReportingCategory ofrmAddInsuranceRptCtgry = new frmInsuranceReportingCategory(_Contactid, _databaseconnectionstring);
                                                ofrmAddInsuranceRptCtgry.ShowDialog(this);
                                                ofrmAddInsuranceRptCtgry.Dispose();
                                                DesignGrid(0);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                    }
                                    break;
                                case "Delete":
                                    {
                                        try
                                        {
                                            if (c1ViewContacts.Rows.Count > 1)
                                            {
                                                //20100220 
                                                string _WarningString;
                                                gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
                                                if (oContact.IsReportingCategoryHasPlans(_Contactid) == true)

                                                    _WarningString = "Insurance reporting category has plans associated, do you still want to delete this contact?";
                                                else
                                                    _WarningString = "Are you sure you want to delete this contact?";

                                                if (MessageBox.Show(this, _WarningString, _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                {
                                                    gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                                    try
                                                    {
                                                        //if (c1ViewContacts.Rows.Count > 0)
                                                        //{
                                                        if (_Contactid != 0)
                                                        {
                                                            ogloContacts.DeleteInsuranceReportingCategory(_Contactid);
                                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance Reporting Category", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                            DesignGrid(0);

                                                        }
                                                    }

                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                                    }
                                                    finally
                                                    {
                                                        ogloContacts.Dispose();
                                                        ogloContacts = null;
                                                        oContact.Dispose();
                                                        oContact = null;
                                                    }
                                                }

                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }


                                    }

                                    break;
                                case "Refresh":
                                    {
                                        txt_search.Clear();
                                        txt_search.Focus();
                                        DesignGrid(0);
                                    }
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            }

                        }

                        break;

                    case "Insurance Plan":
                    case "Inactive Insurance Plan":

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupInsurance ofrmAddContact = new frmSetupInsurance(_databaseconnectionstring);
                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);
                                }
                                break;
                            case "Modify":
                                {
                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                            if (IsPlanExists(_Contactid))
                                            {
                                                frmSetupInsurance ofrmModifyContact = new frmSetupInsurance(_Contactid, _databaseconnectionstring);
                                                ofrmModifyContact.ShowDialog(this);
                                                ofrmModifyContact.Dispose();
                                                DesignGrid(_Contactid);
                                            }
                                            else
                                            {
                                                MessageBox.Show("selected insurance plan either merged by another user or not present in the system.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                DesignGrid(0);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    }

                                }
                                break;
                            case "Copy":
                                {
                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                            if(IsPlanExists(_Contactid))
                                            {
                                            frmSetupInsurance ofrmModifyContact = new frmSetupInsurance(_Contactid, _databaseconnectionstring, true);
                                            ofrmModifyContact.ShowDialog(this);
                                            ofrmModifyContact.Dispose();
                                            DesignGrid(_Contactid);
                                            }
                                            else
                                            {
                                                MessageBox.Show("selected insurance plan either merged by another user or not present in the system.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                DesignGrid(0);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    }

                                }
                                break;
                            case "Copy Multiple":
                                {
                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                            frmInsuranceMultipleCopy ofrmInsuranceMultipleCopy = new frmInsuranceMultipleCopy(_databaseconnectionstring);
                                            ofrmInsuranceMultipleCopy.ShowDialog(this);
                                            ofrmInsuranceMultipleCopy.Dispose();
                                            DesignGrid(0);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    }

                                }
                                break;
                            case "Inactive":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        //gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);

                                        // COMMENT BY SUDHIR 20100510 //
                                        //if (oContact.IsInsurancePlanUsed(_Contactid) == true)
                                        //{
                                        //    MessageBox.Show("Cannot remove this insurance plan. Insurance plan is used for billing.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return;
                                        //}

                                        gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                        try
                                        {

                                            if (MessageBox.Show(this, "Are you sure you want to deactivate this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {
                                                //Get the contact id from the Seleted row;                                            
                                                //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);

                                                if (_Contactid != 0)
                                                {
                                                    // ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    DesignGrid(0);
                                                    if (c1ViewContacts.Rows.Count == 1)
                                                    {
                                                        tls_btnModify.Enabled = false;
                                                        tls_btnInActive.Enabled = false;
                                                    }
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Insurance Inactive", 0, _Contactid, 0, ActivityOutCome.Success);

                                                }
                                            }
                                        }


                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                                        }
                                        finally
                                        {
                                            ogloContactsforDelete.Dispose();
                                        }

                                    }
                                }

                                break;
                            case "Active":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        //gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);

                                        // COMMENT BY SUDHIR 20100510 //
                                        //if (oContact.IsInsurancePlanUsed(_Contactid) == true)
                                        //{
                                        //    MessageBox.Show("Cannot remove this insurance plan. Insurance plan is used for billing.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return;
                                        //}

                                        gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                        try
                                        {

                                            if (MessageBox.Show(this, "Are you sure you want to activate this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {
                                                //Get the contact id from the Seleted row;                                            
                                                //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);

                                                if (_Contactid != 0)
                                                {
                                                    // ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.UnBlock(_Contactid, _SelectedContact);
                                                    DesignGrid(0);
                                                    if (c1ViewContacts.Rows.Count == 1)
                                                    {
                                                        tls_btnModify.Enabled = false;
                                                        tls_btnActive.Enabled = false;
                                                    }
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Insurance Active", 0, _Contactid, 0, ActivityOutCome.Success);

                                                }
                                            }
                                        }


                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                                        }
                                        finally
                                        {
                                            ogloContactsforDelete.Dispose();
                                        }

                                    }
                                }

                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate insurance?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Insurance);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Delete, "Delete duplicate insurance", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":
                                DesignGrid(0);

                                break;
                            case "AssociateInsPlan":
                                {
                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            frmAssignFeeScheduled ofrmAssignFeeScheduled = new frmAssignFeeScheduled();
                                            ofrmAssignFeeScheduled.ShowDialog(this);
                                            ofrmAssignFeeScheduled.Dispose();
                                            DesignGrid(0);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    }

                                }
                                break;
                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;
                                break;
                        }
                        break;
                    case "Collection Agency" :
                        long _CollectionContactid =0;
                        if (c1ViewContacts.RowSel >= 0)
                            _CollectionContactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                        
                        clsCollectionAgency oclsCollectionAgency = new clsCollectionAgency(_databaseconnectionstring);
                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupCollectionAgency oCollectionagency = new frmSetupCollectionAgency(_databaseconnectionstring);
                                    oCollectionagency.ShowDialog(this);
                                    oCollectionagency.Dispose();
                                    FillCollectionAgency();
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Add, "Added Active Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                }
                                break;
                            case "Modify":
                                try
                                {
                                    
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {

                                        frmSetupCollectionAgency oCollectionAgency = new frmSetupCollectionAgency(_databaseconnectionstring, _CollectionContactid);
                                        oCollectionAgency.ShowDialog(this);
                                        oCollectionAgency.Dispose();
                                        FillCollectionAgency();
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Modify, "Modified Active Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                }
                                break;
                            case "Inactive":
                                if (c1ViewContacts.Rows.Count > 1)
                                {
                                     DialogResult drResult = MessageBox.Show("Do you want to deactivate collection agency?", "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                                     if (drResult.ToString() == "Yes")
                                     {
                                         oclsCollectionAgency.ActivateCollectionAgency(_CollectionContactid, 1);
                                         FillCollectionAgency();
                                         gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.DeActivate, "Deactivated Active Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                     }
                                }
                                break;
                            case "Delete":
                                if (c1ViewContacts.Rows.Count > 1)
                                {
                                     DialogResult drResult = MessageBox.Show("Do you want to delete collection agency?", "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                                     if (drResult.ToString() == "Yes")
                                     {
                                         oclsCollectionAgency.DeleteCollectionAgency(_CollectionContactid);
                                         FillCollectionAgency();
                                         gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Delete, "Deleted Active Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                     }
                                }
                                break;
                            case "Refresh":
                                FillCollectionAgency();
                                break;
                            case "Close":
                                this.Close();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Inactive Collection Agency":

                        long _CollectionIAContactid =0;
                        if (c1ViewContacts.RowSel >= 0)
                            _CollectionIAContactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                        
                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Modify":
                                try
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {

                                        frmSetupCollectionAgency oCollectionAgency = new frmSetupCollectionAgency(_databaseconnectionstring, _CollectionIAContactid);
                                        oCollectionAgency.ShowDialog(this);
                                        oCollectionAgency.Dispose();
                                        FillCollectionAgency(true);
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Modify, "Modified Inactive Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                }
                                break;
                            case "Active":
                                if (c1ViewContacts.Rows.Count > 1)
                                {
                                    DialogResult drResult = MessageBox.Show("Do you want to activate collection agency?", "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                                    if (drResult.ToString() == "Yes")
                                    {
                                        clsCollectionAgency oclsCollectionAgency1 = new clsCollectionAgency(_databaseconnectionstring);
                                        oclsCollectionAgency1.ActivateCollectionAgency(_CollectionIAContactid,0);
                                        FillCollectionAgency(true);
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Activate, "Activated Inactive Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                    }
                                }
                            
                                break;
                            case "Refresh":
                                FillCollectionAgency(true);
                                break;
                            case "Close":
                                this.Close();
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Pharmacy":

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupPharmacy ofrmAddContact = new frmSetupPharmacy(_databaseconnectionstring);
                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);
                                }
                                break;
                            case "Modify":
                                {

                                    //check if no row present.

                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                            frmSetupPharmacy ofrmModifyContact = new frmSetupPharmacy(_Contactid, _databaseconnectionstring);
                                            ofrmModifyContact.ShowDialog(this);
                                            ofrmModifyContact.Dispose();
                                            DesignGrid(_Contactid);
                                        }
                                    }
                                    catch (Exception ex)    //Show the information message if no Row is present and clicked modify,
                                    {
                                        MessageBox.Show("Please select the item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            try
                                            {



                                                //Get the contact id from the Seleted row;                                            
                                                //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);


                                                if (_Contactid != 0)
                                                {
                                                    // ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Delete, "Pharmacy delete", 0, _Contactid, 0, ActivityOutCome.Success);
                                                    DesignGrid(0);
                                                }
                                            }

                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Delete, "Delete pharmacy", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                ogloContactsforDelete.Dispose();
                                            }
                                        }

                                    }
                                }
                                //DesignGrid(0);
                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate pharmacy?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Pharmacy);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Delete, "Delete duplicate pharmacy", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":
                                if (cmbSpeciality.Items.Count > 1)//added to resolve bugid 74282
                                {
                                    cmbSpeciality.SelectedIndex = 1;
                                }
                                  DesignGrid(0);

                                break;

                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;
                                break;
                        }
                        break;
                    case "Hospital":

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupHospital ofrmAddContact = new frmSetupHospital(_databaseconnectionstring);
                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);
                                }
                                break;
                            case "Modify":
                                {

                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            frmSetupHospital ofrmModifyContact = new frmSetupHospital(_Contactid, _databaseconnectionstring);
                                            ofrmModifyContact.ShowDialog(this);
                                            ofrmModifyContact.Dispose();
                                            DesignGrid(_Contactid);
                                        }
                                    }
                                    catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                                    {
                                        MessageBox.Show("Select the item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            try
                                            {

                                                //Get the contact id from the Seleted row;                                            
                                                //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);

                                                if (_Contactid != 0)
                                                {
                                                    //ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Delete, "Hospital deleted ", 0, _Contactid, 0, ActivityOutCome.Success);
                                                    DesignGrid(0);
                                                }
                                            }


                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Delete, "Delete hospital", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                ogloContactsforDelete.Dispose();
                                            }
                                        }
                                    }
                                }
                                //DesignGrid(0);
                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate hospital?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Hospital);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Delete, "Delete duplicate hospital", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":
                                DesignGrid(0);

                                break;

                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;

                                break;

                        }
                        break;
                    //Shubhangi

                    case "Others":
                        switch (e.ClickedItem.Tag.ToString())
                        {

                            case "Add":
                                {
                                    frmSetupHospital ofrmAddContact = new frmSetupHospital(_databaseconnectionstring);
                                    ofrmAddContact.Text = "Others";

                                    //IsForModify = true;
                                    ofrmAddContact.IsOtherContact = true;
                                    ofrmAddContact.Icon = global::gloContacts.Properties.Resources.Other;
                                    ofrmAddContact.txtNPI.Visible = false;
                                    ofrmAddContact.lblNPI.Visible = false;
                                    ofrmAddContact.GBox_GeneralInfo.Height = 78;
                                    ofrmAddContact.GBox_Companyadrs.Location = new System.Drawing.Point(20, 105);
                                    ofrmAddContact.gBoxComContact.Location = new System.Drawing.Point(19, 246);
                                    ofrmAddContact.Size = new System.Drawing.Size(479, 494);

                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);

                                }
                                break;
                            case "Modify":
                                {

                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            frmSetupHospital ofrmModifyContact = new frmSetupHospital(_Contactid, _databaseconnectionstring);
                                            ofrmModifyContact.Text = "Others";
                                            ofrmModifyContact.IsOtherContact = true;
                                            ofrmModifyContact.Icon = global::gloContacts.Properties.Resources.Other;
                                            ofrmModifyContact.txtNPI.Visible = false;
                                            ofrmModifyContact.lblNPI.Visible = false;
                                            ofrmModifyContact.GBox_GeneralInfo.Height = 78;
                                            ofrmModifyContact.GBox_Companyadrs.Location = new System.Drawing.Point(20, 105);
                                            ofrmModifyContact.gBoxComContact.Location = new System.Drawing.Point(19, 246);
                                            ofrmModifyContact.Size = new System.Drawing.Size(479, 494);

                                            ofrmModifyContact.ShowDialog(this);
                                            ofrmModifyContact.Dispose();
                                            DesignGrid(_Contactid);
                                        }
                                    }
                                    catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                                    {
                                        MessageBox.Show("Select the item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }

                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            try
                                            {
                                                if (_Contactid != 0)
                                                {
                                                    // ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Physician deleted", 0, _Contactid, 0, ActivityOutCome.Success);
                                                    DesignGrid(0);
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete physician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                ogloContactsforDelete.Dispose();
                                            }
                                        }
                                    }
                                }
                                //DesignGrid(0);
                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate physician?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Physician);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete duplicate hysician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":
                                DesignGrid(0);
                                //if (c1ViewContacts.Rows.Count > 1)
                                //{
                                //    c1ViewContacts.Row = 1;
                                //}
                                break;

                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;
                                break;
                        }
                        break;
                        //End
                        /*
                        switch (e.ClickedItem.Tag.ToString())
                        {

                            case "Add":
                                {
                                    frmSetupPhysician ofrmAddContact = new frmSetupPhysician(_databaseconnectionstring);
                                    ofrmAddContact.ShowDialog(this);
                                    ofrmAddContact.Dispose();
                                    DesignGrid(ofrmAddContact.ContactID);

                                }
                                break;
                            case "Modify":
                                {

                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                        frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                                        ofrmModifyContact.ShowDialog(this);
                                        ofrmModifyContact.Dispose();
                                        DesignGrid(_Contactid);
                                    }

                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            try
                                            {
                                                if (_Contactid != 0)
                                                {
                                                    //   ogloContactsforDelete.Block(_Contactid, trvContacts.SelectedNode.Text);
                                                    ogloContactsforDelete.Block(_Contactid, _SelectedContact);
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Physician deleted", 0, _Contactid, 0, ActivityOutCome.Success);
                                                    DesignGrid(0);
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete physician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                ogloContactsforDelete.Dispose();
                                            }
                                        }
                                    }
                                }
                                //  DesignGrid(0);
                                break;
                            case "RemoveDuplicate":
                                {
                                    if (MessageBox.Show(this, "Are you sure to delete duplicate physician?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        gloPMContacts.gloContacts ogloRemoveDuplicate = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                        try
                                        {
                                            ogloRemoveDuplicate.RemoveDuplicateContacts(gloPMContacts.ContactType.Physician);
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Delete, "Delete duplicate hysician", 0, _Contactid, 0, ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                        }
                                        finally
                                        {
                                            ogloRemoveDuplicate.Dispose();
                                        }
                                    }
                                }
                                DesignGrid(0);
                                break;
                            case "Refresh":
                                DesignGrid(0);
                                //if (c1ViewContacts.Rows.Count > 1)
                                //{
                                //    c1ViewContacts.Row = 1;
                                //}
                                break;

                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:
                                //default;
                                break;
                        }
                        break;
                        */

                    case "Contact Type":

                        switch (e.ClickedItem.Tag.ToString())
                        {


                            case "Close":
                                //close the current form
                                this.Close();
                                break;

                            default:

                                break;
                        }
                        break;

                    case "Zip Code":

                        #region
                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupZipCode ofrmZipCode = new frmSetupZipCode(0, _databaseconnectionstring);
                                    ofrmZipCode.ShowDialog(this);
                                    ofrmZipCode.Dispose();
                                    DesignGridForZip("",true);
                                }
                                break;
                            case "Modify":
                                {

                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            frmSetupZipCode ofrmZipCode = new frmSetupZipCode(_Contactid, _databaseconnectionstring);
                                            ofrmZipCode.ShowDialog(this);
                                            ofrmZipCode.Dispose();
                                            DesignGridForZip("", true);
                                        }
                                    }
                                    catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                                    {
                                        MessageBox.Show("Select the item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                }
                                break;
                            case "Delete":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {
                                        if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            //gloPMContacts.gloContacts ogloContactsforDelete = new gloPMContacts.gloContacts(_databaseconnectionstring);
                                            //gloContacts.gloContact ogloContactsforDelete = new gloContacts.gloContact(_databaseconnectionstring);
                                            gloDatabaseLayer.DBLayer ODB = null;
                                            try
                                            {

                                                //Get the contact id from the Seleted row;                                            
                                                //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);

                                                if (_Contactid != 0)
                                                {
                                                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                    ODB.Connect(false);
                                                    string _strquery = "Delete from CSZ_MST where nID='" + _Contactid + "'";
                                                    int _result = ODB.Execute_Query(_strquery);
                                                    if (_result > 0)
                                                    {
                                                        DesignGridForZip("", true);
                                                    }
                                                    if (ODB != null)
                                                    {
                                                        ODB.Disconnect();
                                                        ODB.Dispose();
                                                        ODB = null;
                                                    }
                                                }
                                            }



                                            catch (Exception ex)
                                            {
                                                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Delete, "Delete hospital", 0, _Contactid, 0, ActivityOutCome.Failure);
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                            }
                                            finally
                                            {
                                                //ogloContactsforDelete.Dispose();
                                            }
                                        }
                                    }
                                }
                                break;
                            //Code Added by Mayuri:20091201
                            //Added Refresh functionality
                            case "Refresh":
                                DesignGridForZip("", true);
                                txt_search.Text = "";
                                txt_search.Focus();
                                break;
                            //end code Added by Mayuri:20091201
                            case "Close":
                                {
                                    this.Close();
                                }
                                //DesignGrid(0);
                                break;
                        }
                        #endregion

                        break;

                    case "Country":

                        #region
                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    frmSetupCountry ofrmCountry = new frmSetupCountry(0);
                                    ofrmCountry.ShowDialog(this);
                                    ofrmCountry.Dispose();
                                    DesignGridForCountry("", true,ofrmCountry.CountryID);

                                }
                                break;
                            case "Modify":
                                {

                                    try
                                    {
                                        if (c1ViewContacts.Rows.Count > 1)
                                        {
                                            frmSetupCountry ofrmCountry = new frmSetupCountry(_Contactid);
                                            ofrmCountry.ShowDialog(this);
                                            ofrmCountry.Dispose();
                                            DesignGridForCountry("", true,0);                                           
                                        }
                                    }
                                    catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                                    {
                                        MessageBox.Show("Select the item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                }
                                break;
                            //case "Delete":
                            //    //Delete;
                            //    {
                            //        if (c1ViewContacts.Rows.Count > 1)
                            //        {
                            //            if (MessageBox.Show(this, "Are you sure you want to delete this contact?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            //            {
                            //                gloDatabaseLayer.DBLayer ODB = null;
                            //                try
                            //                {

                            //                    if (_Contactid != 0)
                            //                    {
                            //                        ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            //                        ODB.Connect(false);
                            //                        string _strquery = "UPDATE Contacts_Country_MST SET bIsBlocked = 'True' where nID='" + _Contactid + "'";
                            //                        int _result = ODB.Execute_Query(_strquery);
                            //                        if (_result > 0)
                            //                        {
                            //                            DesignGridForCountry("", true);
                            //                        }
                            //                        if (ODB != null)
                            //                        {
                            //                            ODB.Disconnect();
                            //                            ODB.Dispose();
                            //                        }
                            //                    }
                            //                }



                            //                catch (Exception ex)
                            //                {
                                                
                            //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //                }
                                          
                            //            }
                            //        }
                            //    }
                            //    break;
                            case "Inactive":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {

                                        if (MessageBox.Show(this, "Are you sure you want to deactivate this Country?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            //Get the contact id from the Seleted row;                                            
                                            //_Contactid = Convert.ToInt64(_CurrentRow.Cells[0].Value);

                                            gloDatabaseLayer.DBLayer ODB = null;
                                            try
                                            {

                                                if (_Contactid != 0)
                                                {
                                                    string _CountryName = "";
                                                    _CountryName = Convert.ToString(c1ViewContacts.GetData(c1ViewContacts.RowSel, 1));

                                                    gloContact ogloContacts = new gloContact(_databaseconnectionstring);


                                                    if (!ogloContacts.IsCountryInUse(_CountryName.Replace("'","''")))
                                                    {
                                                        ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                        ODB.Connect(false);
                                                        string _strquery = "UPDATE Contacts_Country_MST SET bIsBlocked = 'True' where nID='" + _Contactid + "'";
                                                        int _result = ODB.Execute_Query(_strquery);
                                                        if (_result > 0)
                                                        {
                                                            DesignGridForCountry("", true,0);
                                                            gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country);
                                                        }
                                                        if (ODB != null)
                                                        {
                                                            ODB.Disconnect();
                                                            ODB.Dispose();
                                                            ODB = null;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show(this, "Country is in use cannot deactivate it.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                   
                                                    }
                                                    //if (c1ViewContacts.Rows.Count == 1)
                                                    //{
                                                    //    tls_btnModify.Enabled = false;
                                                    //    tls_btnInActive.Enabled = false;
                                                    //}
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
                                    }
                                }

                                break;
                            case "Active":
                                //Delete;
                                {
                                    if (c1ViewContacts.Rows.Count > 1)
                                    {

                                        if (MessageBox.Show(this, "Are you sure you want to activate this Country?", _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            gloDatabaseLayer.DBLayer ODB = null;
                                            try
                                            {

                                                if (_Contactid != 0)
                                                {
                                                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                    ODB.Connect(false);
                                                    string _strquery = "UPDATE Contacts_Country_MST SET bIsBlocked = 'False' where nID='" + _Contactid + "'";
                                                    int _result = ODB.Execute_Query(_strquery);
                                                    if (_result > 0)
                                                    {
                                                        DesignGridForCountry("", true,0);
                                                        gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country);  
                                                    }
                                                    if (ODB != null)
                                                    {
                                                        ODB.Disconnect();
                                                        ODB.Dispose();
                                                        ODB = null;
                                                    }
                                                    //if (c1ViewContacts.Rows.Count == 1)
                                                    //{
                                                    //    tls_btnModify.Enabled = false;
                                                    //    tls_btnActive.Enabled = false;
                                                    //}
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                                            }
                                        }



                                    }
                                }

                                break;
                            case "Refresh":
                                DesignGridForCountry("", true,0);
                                txt_search.Text = "";
                                txt_search.Focus();
                                break;
                            case "Close":
                                {
                                    this.Close();
                                }
                                break;
                        }
                        #endregion

                        break;


                    default://for ePharmacy and other
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "View":
                                    {
                                        //check if no row present.
                                        try
                                        {
                                            if (c1ViewContacts.Rows.Count > 1)
                                            {
                                                //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                                frmSetupPharmacy ofrmViewPharmacy = new frmSetupPharmacy(_Contactid, true, _databaseconnectionstring);
                                                ofrmViewPharmacy.ShowDialog(this);
                                                ofrmViewPharmacy.Dispose();

                                                // Commented to resolve an issue #3932: Pharmacy screen, from the erx area
                                                // In case of view, not required to refresh the grid again.
                                                //DesignGrid(_Contactid);
                                            }
                                        }
                                        catch (Exception ex)    //Show the information message if no Row is present and clicked modify,
                                        {
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        }
                                    }
                                    break;
                                case "Refresh":
                                    cmbSpeciality.SelectedIndex = 1;
                                    DesignGrid(0);                                    
                                    break;

                                case "Close":
                                    //close the current form
                                    this.Close();
                                    break;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion "Tool Strip Events"

        #region "Search Method "

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string[] strSearchArray = null;
            string sFilter = "";
            DataView _dv = new DataView();
            _dv = (DataView)c1ViewContacts.DataSource;
            c1ViewContacts.DataSource = _dv;
            if (_dv == null) return;
            this.Cursor = Cursors.WaitCursor;
            string strSearch = txt_search.Text.Trim();
            try
            {

                //COMMENTED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (strSearch.StartsWith("*") == true)
                { strSearch = strSearch.Replace("*", "%"); }

                //ADDED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                // strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                strSearch = strSearch.Replace("*", "[*]");

                if (strSearch.Length > 1)
                {
                    //string str = strSearch.Substring(1).Replace("%", "");
                    string str = strSearch.Substring(1);
                    strSearch = strSearch.Substring(0, 1) + str;
                }
                if (strSearch.Trim() != "")
                {
                    strSearchArray = strSearch.Split(',');
                }
                //20100312
                //switch (Convert.ToString(trvContacts.SelectedNode.Text))
                switch (_SelectedContact)
                {
                    //Insurance,Physician ,Pharmacy , Hospital
                    case "Insurance Plan":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCPTCrosswalk"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%'  OR " +
                                                    _dv.Table.Columns["sPayerId"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +

                                                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCPTCrosswalk"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +

                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["sCPTCrosswalk"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;

                    //abhisekh pandey 10/02/2010 
                    //Search for insurance reporting Category
                    case "Insurance Reporting Category":
                        {
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";


                                }
                                else
                                {
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%')";


                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";
                                                sFilter = sFilter + " (" + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%')";


                                            }
                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }

                        break;
                    //End abhisekh

                    //Shubhangi 20091104
                    //Search for insurance company
                    case "Insurance Company":
                        {
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sCPTMappingName"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' ";


                                }
                                else
                                {
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%') ";


                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";
                                                sFilter = sFilter + " (" + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%')";


                                            }
                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }

                        break;
                    //End Shubhangi
                    case "Physician":
                        {

                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {

                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                    //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {

                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }


                        }
                        break;

                    case "Direct Physician":
                        {

                            if (timerOwnAddress.Enabled == false)
                            {
                                timerOwnAddress.Stop();
                                timerOwnAddress.Enabled = true;
                            }


                            //   if (strSearch.Trim() != "")
                            //   {

                            //    if (strSearchArray.Length == 1)
                            //    {

                            //        strSearch = strSearchArray[0].Trim();
                            //        _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR "+
                            //                         _dv.Table.Columns["sSPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                             _dv.Table.Columns["sNPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sDirectAddress"].ColumnName + " Like '" + strSearch + "%'";

                            //    }
                            //    else
                            //    {

                            //        //For Comma separated  value search
                            //        for (int i = 0; i < strSearchArray.Length; i++)
                            //        {
                            //            strSearch = strSearchArray[i].Trim();
                            //            if (strSearch.Trim() != "")
                            //            {
                            //                if (i == 0)
                            //                {
                            //                    sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                         _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR "+
                            //                         _dv.Table.Columns["sSPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["sNPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sDirectAddress"].ColumnName + " Like '" + strSearch + "%' )";
                            //                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                            //                }
                            //                else
                            //                {
                            //                    if (sFilter != "")
                            //                        sFilter = sFilter + " AND ";

                            //                    sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                         _dv.Table.Columns["sSPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                           _dv.Table.Columns["sNPI"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["sDirectAddress"].ColumnName + " Like '" + strSearch + "%' )";
                            //                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                            //                }

                            //            }
                            //        }
                            //        _dv.RowFilter = sFilter;
                            //    }

                            //}
                            //else
                            //{
                            //    _dv.RowFilter = "";
                            //}


                        }
                        break;
                    case "Pharmacy":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";

                                    //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    case "Hospital":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                    //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + "  (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    case "e-Pharmacy":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    if (cmbSpeciality.Text != "MailOrder")
                                    {
                                        _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                            //Bug #65546: 00000652 : Pharmacy search issue
                                            //Remove NCPDID from search query.
                                            //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                    }
                                    else
                                    {
                                        _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                            //Bug #65546: 00000652 : Pharmacy search issue
                                            //Remove NCPDID from search query.
                                            //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                    }
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                if (cmbSpeciality.Text != "MailOrder")
                                                {
                                                    sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        //Bug #65546: 00000652 : Pharmacy search issue
                                                        //Remove NCPDID from search query.
                                                        //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                }
                                                else
                                                {
                                                    sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                         _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        //Bug #65546: 00000652 : Pharmacy search issue
                                                        //Remove NCPDID from search query.
                                                        //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                }
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";
                                                if (cmbSpeciality.Text != "MailOrder")
                                                {
                                                    sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        //Bug #65546: 00000652 : Pharmacy search issue
                                                        //Remove NCPDID from search query.
                                                        //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                }
                                                else
                                                {
                                                    sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                        //Bug #65546: 00000652 : Pharmacy search issue
                                                        //Remove NCPDID from search query.
                                                        //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                       _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%' )";
                                                }
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;


                    //Added By MaheshB

                    case "Zip Code":
                        {

                            #region " COMMENTED CODE "
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //if (strSearch.Trim() != "")
                            //{

                            //    if (strSearchArray.Length == 1)
                            //    {
                            //        strSearch = strSearchArray[0].Trim();
                            //        _dv.RowFilter = _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ST"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AreaCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["County"].ColumnName + " Like '" + strSearch + "%'";

                            //    }
                            //    else
                            //    {
                            //        //For Comma separated  value search
                            //        for (int i = 0; i < strSearchArray.Length; i++)
                            //        {
                            //            strSearch = strSearchArray[i].Trim();
                            //            if (strSearch.Trim() != "")
                            //            {
                            //                if (i == 0)
                            //                {
                            //                    sFilter = " ( " + _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ST"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AreaCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                         _dv.Table.Columns["County"].ColumnName + " Like '" + strSearch + "%' )";

                            //                }
                            //                else
                            //                {
                            //                    if (sFilter != "")
                            //                        sFilter = sFilter + " AND ";

                            //                    sFilter = sFilter + " (" + _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ST"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["ZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["County"].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                        _dv.Table.Columns["AreaCode"].ColumnName + " Like '" + strSearch + "%')";

                            //                }

                            //            }
                            //        }
                            //        _dv.RowFilter = sFilter;
                            //    }

                            //}
                            //else
                            //{
                            //    _dv.RowFilter = "";
                            //}

                            #endregion

                            DesignGridForZip(txt_search.Text.Trim(), false);
                        }
                        break;

                    case "Country":
                        {
                            DesignGridForCountry(txt_search.Text.Trim(), false, 0);
                        }
                        break;
                    case "Collection Agency":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter =
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%' ";

                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                      _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;

                    case "Inactive Collection Agency":
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter =
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%' ";

                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                      _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sContactType"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sContact"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sFAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sURL"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmail"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    default:
                        {
                            ////default search for e-Pharmacy and other
                            //   if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //       _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            //   else
                            //       _dv.RowFilter = _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%'";
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0].Trim();
                                    _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        //Bug #65546: 00000652 : Pharmacy search issue
                                        //Remove NCPDID from search query.
                                        //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                    // _dv.Table.Columns["sPayerId"].ColumnName + " Like '" + strSearch + "%'";
                                    //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    //Bug #65546: 00000652 : Pharmacy search issue
                                                    //Remove NCPDID from search query.
                                                    //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                                // _dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";

                                                sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["ContactName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["AddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["FAX"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    //Bug #65546: 00000652 : Pharmacy search issue
                                                    //Remove NCPDID from search query.
                                                    //_dv.Table.Columns["NCPDPID"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'";
                                                //_dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                                                //_dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                //_dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }

                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;

                }
                this.Cursor = Cursors.Default;

            }

            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                strSearchArray = null;
                sFilter = null;
                strSearch = null;
            }

        }

        #endregion

        private void c1ViewContacts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1ViewContacts.Row == -1)
                {
                    return;
                }
                //if (trvContacts.SelectedNode.Level != 0)
                //{
                //    _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                //}
                //else if (trvContacts.SelectedNode.Text == "Zip Code")
                if (_SelectedContact == "Zip Code")
                {
                    _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, 0));
                }
                else
                {
                    _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                }
                //20100312
                //switch (Convert.ToString(trvContacts.SelectedNode.Text))
                switch (Convert.ToString(_SelectedContact))
                {
                    case "Physician":
                    case "Direct Physician":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.CallFrom = _SelectedContact;
                                ofrmModifyContact.ShowDialog(this);                               
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)
                        {
                            // MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);    
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        break;

                    //Shubhangi 20091104
                    //Add event for Insurance Reporting Category
                    case "Insurance Reporting Category":

                        try
                        {
                            frmInsuranceReportingCategory ofrmRptCtgry = new frmInsuranceReportingCategory(_Contactid, _databaseconnectionstring);
                            ofrmRptCtgry.ShowInTaskbar = false;
                            ofrmRptCtgry.ShowDialog(this);


                            ofrmRptCtgry.Dispose();
                            //DesignGrid(0);
                            DesignGrid(_Contactid);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        break;
                    //End abhisekh



                    //Shubhangi 20091104
                    //Add event for Insurance Company
                    case "Insurance Company":

                        try
                        {
                            frmInsuranceCompany ofrmcmpny = new frmInsuranceCompany(_Contactid, _databaseconnectionstring);
                            ofrmcmpny.ShowInTaskbar = false;
                            ofrmcmpny.ShowDialog(this);


                            ofrmcmpny.Dispose();
                            //DesignGrid(0);
                            DesignGrid(_Contactid);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        break;
                    //End Shubhangi
                    case "Insurance Plan":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupInsurance ofrmModifyContact = new frmSetupInsurance(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.ShowDialog(this);
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "Collection Agency":
                         try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {

                                frmSetupCollectionAgency oCollectionAgency = new frmSetupCollectionAgency(_databaseconnectionstring, _Contactid);
                                oCollectionAgency.ShowDialog(this);
                                oCollectionAgency.Dispose();
                                FillCollectionAgency();
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Modify, "Modified Active Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "Inactive Collection Agency":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {

                                frmSetupCollectionAgency oCollectionAgency = new frmSetupCollectionAgency(_databaseconnectionstring, _Contactid);
                                oCollectionAgency.ShowDialog(this);
                                oCollectionAgency.Dispose();
                                FillCollectionAgency(true);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Modify, "Modified Inactive Collection Agency", 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "Inactive Insurance Plan":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupInsurance ofrmModifyContact = new frmSetupInsurance(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.ShowDialog(this);
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "Pharmacy":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupPharmacy ofrmModifyContact = new frmSetupPharmacy(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.ShowDialog(this);
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "Hospital":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupHospital ofrmModifyContact = new frmSetupHospital(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.ShowDialog(this);
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "Others":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupHospital ofrmModifyContact = new frmSetupHospital(_Contactid, _databaseconnectionstring);
                                ofrmModifyContact.Text = "Others";
                                ofrmModifyContact.IsOtherContact = true;
                                ofrmModifyContact.Icon = global::gloContacts.Properties.Resources.Other;
                                ofrmModifyContact.txtNPI.Visible = false;
                                ofrmModifyContact.lblNPI.Visible = false;
                                ofrmModifyContact.GBox_GeneralInfo.Height = 78;
                                ofrmModifyContact.GBox_Companyadrs.Location = new System.Drawing.Point(20, 105);
                                ofrmModifyContact.gBoxComContact.Location = new System.Drawing.Point(19, 246);
                                ofrmModifyContact.Size = new System.Drawing.Size(479, 494);
                                ofrmModifyContact.ShowDialog(this);
                                ofrmModifyContact.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;


                    //Added By MaheshB

                    case "Zip Code":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupZipCode ofrmZipCode = new frmSetupZipCode(_Contactid, _databaseconnectionstring);
                                ofrmZipCode.ShowDialog(this);
                                ofrmZipCode.Dispose();
                                DesignGridForZip("", true);
                            }
                        }
                        catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "Country":
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                frmSetupCountry ofrmCountry = new frmSetupCountry(_Contactid);
                                ofrmCountry.ShowDialog(this);
                                ofrmCountry.Dispose();
                                DesignGridForCountry("", true,0);
                            }
                        }
                        catch (Exception ex)   //Show the information message if no Row is present and clicked modify,
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            //MessageBox.Show("Please select the Item to modify" + ex.ToString(), _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    default://for e-pharmacy and other
                        //check if no row present.
                        try
                        {
                            if (c1ViewContacts.Rows.Count > 1)
                            {
                                //_Contactid = Convert.ToInt64(currentRow.Cells[0].Value);
                                frmSetupPharmacy ofrmViewPharmacy = new frmSetupPharmacy(_Contactid, true, _databaseconnectionstring);
                                ofrmViewPharmacy.ShowDialog(this);
                                ofrmViewPharmacy.Dispose();
                                DesignGrid(_Contactid);
                            }
                        }
                        catch (Exception ex)    //Show the information message if no Row is present and clicked modify,
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                        break;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1ViewContacts_AfterResizeColumn(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1ViewContacts.Row = -1;
        }

        private void c1ViewContacts_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            // BUG ID 72131 V 8030 - Code changes for maintaining the selected row position after sorting 
            //c1ViewContacts.Row = -1;
            int _index;
            try
            {
                _index = c1ViewContacts.FindRow(_ID.ToString(), 0, 0, false, false, false);
            }
            catch (Exception)
            {
                _index = 0;
            }
            
            c1ViewContacts.ShowCell(_index, 0);
            c1ViewContacts.Row = _index;
            c1ViewContacts.Select();
        }

        private void tls_btnAdd_Click(object sender, EventArgs e)
        {

        }


        private void DesignGridforInsuranceCompany(DataTable dtInsurances)
        {
           // c1ViewContacts.Clear();
            c1ViewContacts.DataSource = null;

            c1ViewContacts.Clear();

            c1ViewContacts.DataSource = dtInsurances.DefaultView;
            c1ViewContacts.Rows.Fixed = 1;

            c1ViewContacts.SetData(0, COL_InsPlan_ContactID, "Contact ID");
            c1ViewContacts.SetData(0, COL_InsPlan_PhyisicianName, "Name");
            c1ViewContacts.SetData(0, COL_InsPlan_LastName, "Last Name");
            c1ViewContacts.SetData(0, COL_InsPlan_Name, "Insurance Plan");
            c1ViewContacts.SetData(0, COL_InsPlan_Company, "Insurance Company");
            c1ViewContacts.SetData(0, COL_InsPlan_sPayerId, "Payer ID");

            c1ViewContacts.SetData(0, COL_InsPlan_ReportingCategory, "Reporting Category");
            c1ViewContacts.SetData(0, COL_InsPlan_ContactName, "Contact");
            c1ViewContacts.SetData(0, COL_InsPlan_Gender, "Gender");
            c1ViewContacts.SetData(0, COL_InsPlan_AddressLine1, "Address 1");
            c1ViewContacts.SetData(0, COL_InsPlan_AddressLine2, "Address 2");
            c1ViewContacts.SetData(0, COL_InsPlan_City, "City");
            c1ViewContacts.SetData(0, COL_InsPlan_State, "State");
            c1ViewContacts.SetData(0, COL_InsPlan_ZIP, "Zip");
            c1ViewContacts.SetData(0, COL_InsPlan_Phone, "Phone");
            c1ViewContacts.SetData(0, COL_InsPlan_Fax, "Fax");
            c1ViewContacts.SetData(0, COL_InsPlan_Mobile, "Mobile");
            c1ViewContacts.SetData(0, COL_InsPlan_Email, "Email");
            c1ViewContacts.SetData(0, COL_InsPlan_InsuranceTypeDesc, "Insurance Type");
            c1ViewContacts.SetData(0, COL_InsPlan_InsuranceTypeCode, "Insurance Code");
            c1ViewContacts.SetData(0, "sCPTCrosswalk", "CPT Crosswalk");
            //Expanded Claim 
            c1ViewContacts.SetData(0, "nServiceLines", "Max Charges Per Claim");
            c1ViewContacts.SetData(0, "nDiagnosis", "Max Diagnosis Per Claim");
            
                

            c1ViewContacts.Cols[COL_InsPlan_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_Gender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_InsPlan_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_InsPlan_Fax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_InsPlan_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1ViewContacts.Cols[COL_InsPlan_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_ContactName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1ViewContacts.Cols[COL_InsPlan_Gender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            c1ViewContacts.Cols[COL_InsPlan_ContactID].Visible = false;
            c1ViewContacts.Cols[COL_InsPlan_PhyisicianName].Visible = false;
            c1ViewContacts.Cols[COL_InsPlan_LastName].Visible = false;
            c1ViewContacts.Cols[COL_InsPlan_Mobile].Visible = false;
            c1ViewContacts.Cols[COL_InsPlan_Gender].Visible = false;
            c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeCode].Visible = false;

            c1ViewContacts.Cols[COL_InsPlan_Name].Width = Width / 4;
            c1ViewContacts.Cols[COL_InsPlan_Company].Width = Width / 9;
            c1ViewContacts.Cols[COL_InsPlan_ReportingCategory].Width = Width / 7;
            c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeDesc].Width = Width / 6;
            c1ViewContacts.Cols[COL_InsPlan_AddressLine1].Width = Width / 5;
            c1ViewContacts.Cols[COL_InsPlan_AddressLine2].Width = Width / 5;
            c1ViewContacts.Cols[COL_InsPlan_City].Width = Width / 9;
            c1ViewContacts.Cols[COL_InsPlan_State].Width = Width / 15;
            c1ViewContacts.Cols[COL_InsPlan_ZIP].Width = Width / 9;
            c1ViewContacts.Cols[COL_InsPlan_ContactName].Width = Width / 12;
            c1ViewContacts.Cols[COL_InsPlan_Phone].Width = Width / 12;
            c1ViewContacts.Cols[COL_InsPlan_Fax].Width = Width / 12;
            c1ViewContacts.Cols[COL_InsPlan_Email].Width = Width / 9;
            c1ViewContacts.Cols["sCPTCrosswalk"].Width = Width / 10;
            c1ViewContacts.Cols["nServiceLines"].Width = Width / 8;
            c1ViewContacts.Cols["nDiagnosis"].Width = Width / 8;
        }

        
        private void cmbInsuranceCompany_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (trvContacts.SelectedNode.Text == "Insurance Plan")
                {
                    if (cmbInsuranceCompany.Visible == true)
                    {
                        txt_search.Text = "";
                        gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
                        DataTable dtInsurances = oContact.GetInsurancePlans(Convert.ToInt64(cmbInsuranceCompany.SelectedValue.ToString()));
                        oContact.Dispose();
                        oContact = null;

                        if (dtInsurances != null)
                        {
                            DesignGridforInsuranceCompany(dtInsurances);
                        }
                        if (dtInsurances != null) { dtInsurances.Dispose(); dtInsurances = null; }

                        if (c1ViewContacts.Rows.Count == 1)
                        {
                            tls_btnInActive.Enabled = false;
                            tls_btnModify.Enabled = false;
                        }
                        else
                        {
                            tls_btnInActive.Enabled = true;
                            tls_btnModify.Enabled = true;
                        }
                    }

                }
                else if (trvContacts.SelectedNode.Text == "Inactive Insurance Plan")
                {
                    if (cmbInsuranceCompany.Visible == true)
                    {
                        txt_search.Text = "";
                        gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
                        DataTable dtInsurances = oContact.GetInactiveContacts(Convert.ToInt64(cmbInsuranceCompany.SelectedValue.ToString()), "Insurance");
                        oContact.Dispose();
                        oContact = null;

                        if (dtInsurances != null)
                        {
                            DesignGridforInsuranceCompany(dtInsurances);
                        }
                        
                    }
                }
                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbSpeciality_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSpeciality.Visible == true)
                {
                    txt_search.Text = "";
                    //gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
                    //DataTable dtInsurances = oContact.GetInsurancePlans(Convert.ToInt64(cmbInsuranceCompany.SelectedValue.ToString()));
                    //oContact.Dispose();
                    //oContact = null;
                    if (cmbSpeciality.Text == "All Without MailOrder")
                    {
                        DesignGrid(0, "0");
                    }
                    else
                    {
                        DesignGrid(0, cmbSpeciality.Text);
                    }
                    c1ViewContacts.Focus(); 

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
             
        }

        private void c1ViewContacts_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            txt_search.Clear();
            txt_search.Focus();
        }

        private void c1ViewContacts_Click(object sender, EventArgs e)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _Code = "";
            gloContact ogloContacts = null;
            try
            {
                if (c1ViewContacts.Row != -1)
                {
                    _Contactid = Convert.ToInt64(c1ViewContacts.GetData(c1ViewContacts.RowSel, COL_ContactID));
                   _Code = Convert.ToString(c1ViewContacts.GetData(c1ViewContacts.RowSel,2));
                }
                //// Problem# - 239 - select the insurance from the list by highlighting it the ACTIVATE button grays out 
                ////                  and the user is not able to activate the insurance plan.  
                //// adding If condition that when we select Country then only go into this if part else not.
                if (_SelectedContact == "Country")
                {
                    if (_Contactid != 0 && _Code != "CA" && _Code != "US")
                    {
                        ogloContacts = new gloContact(_databaseconnectionstring);
                        bool _result = ogloContacts.IsCountryBlocked(_Contactid);
                        if (_result)
                        {
                            tls_btnActive.Enabled = true;
                            tls_btnInActive.Enabled = false;
                        }
                        else
                        {
                            tls_btnActive.Enabled = false;
                            tls_btnInActive.Enabled = true;
                        }
                    }
                    else
                    {
                        tls_btnActive.Enabled = false;
                        tls_btnInActive.Enabled = false;

                    }
                }

            }

            catch //(Exception ex)
            { }

            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
                _Code = null;
            }

           
        }
        // BUG ID 72131  V 8030 - Code changes for maintaining the selected row position after sorting 
        Int64 _ID; 
        private void c1ViewContacts_BeforeSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            

            if (c1ViewContacts.Rows.Count > 1)
            {
                try
                {
                    _ID = Convert.ToInt64(c1ViewContacts.Rows[c1ViewContacts.RowSel][0]);
                }
                catch (Exception)
                {
                    _ID = 0;
                }
               
            }    
        }


        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_SelectedContact == "Direct Physician")
                {

                    if (e.KeyCode == Keys.Enter)
                    {
                        //..Check if there are rows after the search is done 
                        //..if yes then set focus to the grid else keep in the search text box.
                        if (c1ViewContacts.Rows.Count > 0)
                        { c1ViewContacts.Focus(); }
                        else
                        {
                            txt_search.SelectAll();
                            txt_search.Focus();
                        }
                    }


                    if (e.Alt
                        || e.Control
                        || e.KeyCode.Equals(Keys.Left)
                        || e.KeyCode.Equals(Keys.Right)
                        || e.KeyCode.Equals(Keys.Up)
                        || e.KeyCode.Equals(Keys.Down)
                        || e.KeyCode.Equals(Keys.Home)
                        || e.KeyCode.Equals(Keys.End))
                    { return; }

                    if (IsValidKey(e.KeyValue) == false)
                    { return; }

                    timerOwnAddress.Stop();
                    timerOwnAddress.Interval = 500;
                    timerOwnAddress.Enabled = true;
                    _CurrentTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private bool IsValidKey(int _KeyValue)
        {
            bool _ValidKey = false;

            if (_KeyValue >= 32 && _KeyValue <= 47)
            { _ValidKey = true; }

            if (_KeyValue >= 48 && _KeyValue <= 57)
            { _ValidKey = true; }

            if ((_KeyValue >= 65 && _KeyValue <= 90) || (_KeyValue >= 97 && _KeyValue <= 122))
            { _ValidKey = true; }

            return _ValidKey;
        }

        public bool IsPlanExists(Int64 ContactID)
        {
            bool _IsAvailable = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            object oPlanAvailable = new object();

            try
            {
                oDB.Connect(false);

                //nTransactionID, nTransactionDetailID, nTransactionLineNo, nStatusDate, nStatusTime, sStatusNote, nClinicID, nStatusID
                if (ContactID > 0)
                {
                    _sqlQuery = " select sName from Contacts_MST where sContactType='Insurance' and nContactID=" + ContactID + "";

                    oPlanAvailable = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oPlanAvailable != null && Convert.ToString(oPlanAvailable) != "")
                    {
                        if (Convert.ToString(oPlanAvailable).ToUpper() != "")
                            _IsAvailable = true;
                        else
                            _IsAvailable = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                _IsAvailable = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oPlanAvailable != null) { oPlanAvailable = null; }
                _sqlQuery = null;
            }
            return _IsAvailable;

        }

               
    }
}

