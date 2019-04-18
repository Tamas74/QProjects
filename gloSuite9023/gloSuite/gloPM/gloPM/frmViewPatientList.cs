using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloPM
{
    public partial class frmViewPatientList : Form
    {
        #region "Variable Declarations"
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "gloPM";
        private string _PatientlistType = "Patient";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloGlobal.gloICD.CodeRevision _CodeRevision;
        #endregion

        #region "Constants For Grid For Insurances,Patient,Guarantor "

        private const int COL_ID = 0;
        private const int COL_Gurantor = 1;
        private const int COL_Name = 2;
        private const int COL_GenderContact = 3;
        private const int COL_AddressLine1 = 4;
        private const int COL_AddressLine2 = 5;
        private const int COL_City = 6;
        private const int COL_State = 7;
        private const int COL_ZIP = 8;
        private const int COL_Phone = 9;
        private const int COL_Mobile = 10;
        private const int COL_Email = 11;
        private const int COL_Fax = 12;
        private const int COL_InsuranceTypeDesc = 13;
        private const int COL_InsuranceTypeCode = 14;
        private const int COL_COUNT = 15;
        #endregion 

        #region "Constants For Grid For Billing Code And Diagnosis"

        private const int COL_ID1 = 0;
        private const int COL_Code = 1;
        private const int COL_Desc = 2;
        private const int COL_COUNT1 = 3;

        #endregion

        #region "Constructor"

        public frmViewPatientList()
        {
            InitializeComponent();
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

        public frmViewPatientList(string Databaseconnectionstring, string PatientlistType)
        {
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

            InitializeComponent();

            _PatientlistType = PatientlistType;

            _databaseconnectionstring = Databaseconnectionstring;
            
            this.Text = _PatientlistType;
        } 
        #endregion

        #region " Form Load Event "

        private void frmViewPatientList_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1ViewPatientList, false);

            pnlDiagnosis.Visible = false;
            if (_PatientlistType == "Patient" || _PatientlistType == "Guarantor")
            {
                pnl_AddCriteria.Visible = true;
                DesignGrid_PatientORGurantor();
            }
            if (_PatientlistType == "Insurances")
            {
                pnl_AddCriteria.Visible = true;
                cmb_Gender.Visible = false;
                lbl_gender.Visible = false;
                DesignGrid_Insurances();
            }
            if (_PatientlistType == "Billing Code")
            {
                pnl_AddCriteria.Visible = false;
                DesignGrid_BilingcodeORDiagnosis();
            }
            if (_PatientlistType == "Diagnosis")
            {
                gloBilling.gloBilling objBilling=null;
                long _DOS;
                try
                {
                    pnl_AddCriteria.Visible = false;
                    pnlDiagnosis.Visible = true;
                    if (sender != null)
                    {
                        _DOS = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                        objBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
                        _CodeRevision = objBilling.GetICDCodeType(0, _DOS);
                        if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                            rbICD9.Checked = true;
                        else
                            rbICD10.Checked = true;

                        _DOS = 0;
                    }
                    DesignGrid_BilingcodeORDiagnosis();
                }
                catch //(Exception ex)
                {
                    //ex = null;
                }
                finally
                {
                    _DOS = 0;
                    if (objBilling != null)
                    {
                        objBilling.Dispose();
                        objBilling = null;
                    }
                }
            }
            if (c1ViewPatientList.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
            } 
        }
        
        #endregion

        #region "Get data and Design Grid  "

        private void DesignGrid_PatientORGurantor()
        {
            try
            {
                DataTable dt = new DataTable();
                DataView _dv = new DataView();
                dt = GetPatientlist();

                _dv = dt.DefaultView;

                if (_dv != null)
                {
                    int width = pnl_main.Width;

                    c1ViewPatientList.DataSource = null;
                    c1ViewPatientList.DataSource = _dv;
                    c1ViewPatientList.Rows.Fixed = 1;
                    c1ViewPatientList.Cols.Count = COL_COUNT;
                    c1ViewPatientList.SetData(0, COL_ID, "ID");
                    c1ViewPatientList.SetData(0, COL_Name, "Patient");
                    c1ViewPatientList.SetData(0, COL_Gurantor, "Guarantor");
                    c1ViewPatientList.SetData(0, COL_GenderContact, "Gender");
                    c1ViewPatientList.SetData(0, COL_AddressLine1, "Address 1");
                    c1ViewPatientList.SetData(0, COL_AddressLine2, "Address 2");
                    c1ViewPatientList.SetData(0, COL_City, "City");
                    c1ViewPatientList.SetData(0, COL_State, "State");
                    c1ViewPatientList.SetData(0, COL_ZIP, "ZIP");
                    c1ViewPatientList.SetData(0, COL_Phone, "Phone");
                    c1ViewPatientList.SetData(0, COL_Mobile, "Mobile");
                    c1ViewPatientList.SetData(0, COL_Email, "Email");
                    c1ViewPatientList.SetData(0, COL_Fax, "Fax");
                    //c1ViewPatientList.AutoSizeCol(COL_Name);

                    c1ViewPatientList.Cols[COL_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_GenderContact].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Fax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;



                    c1ViewPatientList.Cols[COL_ID].Visible = false;
                    c1ViewPatientList.Cols[COL_GenderContact].Visible = true;
                    c1ViewPatientList.Cols[COL_AddressLine1].Visible = true;
                    c1ViewPatientList.Cols[COL_AddressLine2].Visible = true;
                    c1ViewPatientList.Cols[COL_City].Visible = true;
                    c1ViewPatientList.Cols[COL_State].Visible = true;
                    c1ViewPatientList.Cols[COL_ZIP].Visible = true;
                    c1ViewPatientList.Cols[COL_Fax].Visible = true;
                    c1ViewPatientList.Cols[COL_Email].Visible = true;
                    c1ViewPatientList.Cols[COL_Phone].Visible = true;
                    c1ViewPatientList.Cols[COL_Mobile].Visible = true;
                    c1ViewPatientList.Cols[COL_InsuranceTypeCode].Visible = false;
                    c1ViewPatientList.Cols[COL_InsuranceTypeDesc].Visible = false;
                    c1ViewPatientList.Cols[COL_Gurantor].Visible = false;


                    c1ViewPatientList.Cols[COL_Name].Width = (int)(width * 0.22);
                    c1ViewPatientList.Cols[COL_Gurantor].Width = (int)(width * 0.22);

                    //c1ViewPatientList.Cols[COL_AddressLine1].Width = (int)(width * 0.12);
                    //c1ViewPatientList.Cols[COL_AddressLine2].Width = (int)(width * 0.12);
                    //c1ViewPatientList.Cols[COL_State].Width = (int)(width * 0.06);
                    //c1ViewPatientList.Cols[COL_ZIP].Width = (int)(width * 0.08);
                    //c1ViewPatientList.Cols[COL_Phone].Width = (int)(width * 0.10);
                    //c1ViewPatientList.Cols[COL_Fax].Width = (int)(width * 0.10);
                    //c1ViewPatientList.Cols[COL_Mobile].Width = (int)(width * 0.08);
                    //c1ViewPatientList.Cols[COL_Email].Width = (int)(width * 0.08);
                    //c1ViewPatientList.Cols[COL_GenderContact].Width = (int)(width * 0.08);
                    //c1ViewPatientList.Cols[COL_Gurantor].Width =0;

                    if (_PatientlistType == "Guarantor")
                    {

                        c1ViewPatientList.Cols[COL_Gurantor].Visible = true;
                        c1ViewPatientList.Cols[COL_GenderContact].Visible = false;
                        //c1ViewPatientList.Cols[COL_GenderContact].Width = 0;
                        //c1ViewPatientList.Cols[COL_Gurantor].Width = (int)(width * 0.15);
                    }

                    c1ViewPatientList.AllowEditing = false;
                    c1ViewPatientList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    c1ViewPatientList.Rows[0].AllowEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DesignGrid_Insurances()
        {
            try
            {
                DataTable dt = new DataTable();
                DataView _dv = new DataView();
                dt = GetPatientlist();

                _dv = dt.DefaultView;

                if (_dv != null)
                {
                    c1ViewPatientList.DataSource = null;
                    c1ViewPatientList.DataSource = _dv;
                    c1ViewPatientList.Rows.Fixed = 1;
                    c1ViewPatientList.Cols.Count = COL_COUNT;
                    c1ViewPatientList.SetData(0, COL_ID, "ID");
                    c1ViewPatientList.SetData(0, COL_Gurantor, "Contact Type");
                    c1ViewPatientList.SetData(0, COL_Name, "Name");
                    c1ViewPatientList.SetData(0, COL_GenderContact, "Contact");
                    c1ViewPatientList.SetData(0, COL_AddressLine1, "Address 1");
                    c1ViewPatientList.SetData(0, COL_AddressLine2, "Address 2");
                    c1ViewPatientList.SetData(0, COL_City, "City");
                    c1ViewPatientList.SetData(0, COL_State, "State");
                    c1ViewPatientList.SetData(0, COL_ZIP, "ZIP");
                    c1ViewPatientList.SetData(0, COL_Phone, "Phone");
                    c1ViewPatientList.SetData(0, COL_Mobile, "Mobile");
                    c1ViewPatientList.SetData(0, COL_Email, "Email");
                    c1ViewPatientList.SetData(0, COL_Fax, "Fax");
                    c1ViewPatientList.SetData(0, COL_InsuranceTypeCode, "Insurance Type Code");
                    c1ViewPatientList.SetData(0, COL_InsuranceTypeDesc, "Plan Type");

                    c1ViewPatientList.Cols[COL_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_GenderContact].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Fax].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1ViewPatientList.Cols[COL_ID].Visible = false;
                    c1ViewPatientList.Cols[COL_Gurantor].Visible = false;
                    c1ViewPatientList.Cols[COL_GenderContact].Visible = true;
                    c1ViewPatientList.Cols[COL_AddressLine1].Visible = true;
                    c1ViewPatientList.Cols[COL_AddressLine2].Visible = true;
                    c1ViewPatientList.Cols[COL_City].Visible = true;
                    c1ViewPatientList.Cols[COL_State].Visible = true;
                    c1ViewPatientList.Cols[COL_ZIP].Visible = true;
                    c1ViewPatientList.Cols[COL_Fax].Visible = true;
                    c1ViewPatientList.Cols[COL_Email].Visible = true;
                    c1ViewPatientList.Cols[COL_Phone].Visible = true;
                    c1ViewPatientList.Cols[COL_Mobile].Visible = true;
                    c1ViewPatientList.Cols[COL_InsuranceTypeCode].Visible = false;
                    c1ViewPatientList.Cols[COL_InsuranceTypeDesc].Visible = true;

                    //int width = pnl_main.Width;
                    //c1ViewPatientList.Cols[COL_Name].Width = (int)(width * 0.3);

                    //c1ViewPatientList.Cols[COL_AddressLine1].Width = (int)(width * 0.12);
                    //c1ViewPatientList.Cols[COL_AddressLine2].Width = (int)(width * 0.12);
                    //c1ViewPatientList.Cols[COL_State].Width = (int)(width * 0.07);
                    //c1ViewPatientList.Cols[COL_ZIP].Width = (int)(width * 0.07);
                    //c1ViewPatientList.Cols[COL_Phone].Width = (int)(width * 0.07);
                    //c1ViewPatientList.Cols[COL_Fax].Width = (int)(width * 0.07);
                    //c1ViewPatientList.Cols[COL_Mobile].Width = (int)(width * 0.06);
                    //c1ViewPatientList.Cols[COL_Email].Width = (int)(width * 0.06);
                    //c1ViewPatientList.Cols[COL_GenderContact].Width = (int)(width * 0.10);
                    //c1ViewPatientList.Cols[COL_InsuranceTypeDesc].Width = (int)(width * 0.11);


                    c1ViewPatientList.AllowEditing = false;
                    c1ViewPatientList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                    //c1ViewPatientList.AutoSizeCol(COL_Name);
                    //c1ViewPatientList.Cols.Move(COL_Name, COL_GenderContact);
                    c1ViewPatientList.Rows[0].AllowEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DesignGrid_BilingcodeORDiagnosis()
        {
            try
            {
                DataTable dt = new DataTable();
                DataView _dv = new DataView();
                dt = GetPatientlist();
                _dv = dt.DefaultView;
                if (_dv != null)
                {
                    c1ViewPatientList.DataSource = null;
                    c1ViewPatientList.DataSource = _dv;
                    c1ViewPatientList.Rows.Fixed = 1;
                    c1ViewPatientList.Cols.Count = COL_COUNT1;

                    c1ViewPatientList.SetData(0, COL_ID1, "ID");
                    c1ViewPatientList.SetData(0, COL_Code, "Code");
                    c1ViewPatientList.SetData(0, COL_Desc, "Description");

                    c1ViewPatientList.Cols[COL_ID1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1ViewPatientList.Cols[COL_Desc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                    c1ViewPatientList.Cols[COL_ID1].Visible = false;
                    c1ViewPatientList.Cols[COL_Code].Visible = true;
                    c1ViewPatientList.Cols[COL_Desc].Visible = true;
                    int width = pnl_main.Width;
                    c1ViewPatientList.Cols[COL_ID1].Width = 0;

                    c1ViewPatientList.Cols[COL_Code].Width = (int)(width * 0.25);
                    c1ViewPatientList.Cols[COL_Desc].Width = (int)(width * 0.50);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable GetPatientlist()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = new DataTable();
            string _strSQL = "";

            try
            {
                switch (_PatientlistType)
                {
                    case "Patient":
                        {
                            _strSQL = "SELECT  ISNULL(nPatientID,0)AS nPatientID,ISNULL(sGuarantor,'')AS sGuarantor,ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') " +
                                      " + SPACE(1) + ISNULL(sLastName,'') AS PatientName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS sAddressLine1, " +
                                      "ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS City,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS ZIP,ISNULL(sPhone,'') AS Phone,ISNULL(sMobile,'') AS Mobile,ISNULL(sEmail,'') AS Email,ISNULL(sFAX,'') AS Fax FROM  Patient WHERE nClinicID =" + _ClinicID +
                                       // solving sales force case - GLO2010-0006070
                                      //" AND Patient.nPatientID NOT in (select nPatientID from PatientSettings where upper(ISNULL(sName,'')) = 'EXEMPT FROM REPORT' AND PatientSettings.sValue = 1)";                              
                                      " AND nExemptFromReport <> 1";
                                     // End
                            // nExemptFromReport flag is saved & used from patient table only & not from PatientSettings.

                            if (txt_City.Text != "")
                            {
                                _strSQL += " AND  sCity ='" + txt_City.Text.Replace("'", "''") + "'";   // handle the single quote(')
                            }
                            if (txt_State.Text != "")
                            {
                                _strSQL += " AND  sState ='" + txt_State.Text.Replace("'", "''") + "'";  // handle the single quote(')
                            }
                            if (txt_ZIP.Text != "")
                            {
                                _strSQL += " AND  sZIP ='" + txt_ZIP.Text.Replace("'", "''") + "'";    // handle the single quote(')
                            }
                            if (cmb_Gender.Text != "")
                            {
                                _strSQL += " AND  sGender ='" + cmb_Gender.Text + "'";
                            }

                            _strSQL += " ORDER BY PatientName";
                            oDB.Retrive_Query(_strSQL, out dt);
                        }
                        break;
                    case "Guarantor":
                        {
                            //_strSQL = "SELECT   ISNULL(d.nPatientID,0)AS nPatientID, ISNULL(d.sGuarantor,'')AS sGuarantor,ISNULL(d.sFirstName,'') + SPACE(1) + ISNULL(e.sMiddleName,'')  + SPACE(1) + " +
                            //    " ISNULL(d.sLastName,'') AS PatientName,ISNULL(e.sGender,'') AS Gender,ISNULL(e.sAddressLine1,'') AS sAddressLine1, " +
                            //    "ISNULL(e.sAddressLine2,'') AS sAddressLine2,ISNULL(e.sCity,'') AS City,ISNULL(e.sState,'') AS sState,ISNULL(e.sZIP,'') AS ZIP," +
                            //    "ISNULL(e.sPhone,'') AS Phone,ISNULL(e.sMobile,'') AS Mobile,ISNULL(e.sEmail,'') AS Email,ISNULL(e.sFAX,'') AS Fax " +
                            //    "FROM   Patient as e inner join  Patient as d on e.nPatientID = d.nGuarantorID " +
                            //   "  WHERE e.nClinicID='" + _ClinicID + "'";


                            // Problem# 00000300 - Change the logic to retrive the guarantor list as per current architecure
                            _strSQL = "SELECT distinct * FROM (SELECT ISNULL(Patient_OtherContacts.nPatientID, 0) AS nPatientID  ," +


                                      " ISNULL(Patient_OtherContacts.sFirstName, '') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName, '') + " +
                                      " SPACE(1) + ISNULL(Patient_OtherContacts.sLastName, '') AS sGuarantor  ," +

                                       "ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') +" +
                                       "SPACE(1) + ISNULL(Patient.sLastName, '') AS PatientName ," +

                                       "ISNULL(Patient_OtherContacts.sGender, '') AS sGender ," +

                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sAddressLine1, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sAddressLine1, '')  " +
                                       "END AS sAddressLine1," +

                                       "CASE  " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sAddressLine2, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sAddressLine2, '')    " +
                                       "END AS sAddressLine2," +

                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sCity, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sCity, '')   " +
                                       "END AS sCity," +


                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sState, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sState, '')  " +
                                       "END AS sState," +

                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sZIP, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sZIP, '') " +
                                       "END AS sZIP," +

                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sPhone, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sPhone, '')  " +
                                       "END AS Phone," +


                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sMobile, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sMobile, '')" +
                                       "END AS Mobile," +


                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sEmail, '') " +
                                       "ELSE ISNULL(Patient_OtherContacts.sEmail, '')" +
                                       "END AS Email," +


                                       "CASE " +
                                       "WHEN ISNULL(Patient_OtherContacts.sRelation,'')= 'Self' THEN ISNULL(Patient.sFAX, '')" +
                                       "ELSE ISNULL(Patient_OtherContacts.sFAX, '')" +
                                       "END AS FAX, Patient_OtherContacts.nClinicID  FROM  Patient INNER JOIN Patient_OtherContacts ON Patient.nPatientID = Patient_OtherContacts.nPatientID) " +
                                       "AS Temp WHERE Temp.nClinicID = " + _ClinicID + "";

                            //Add BitIsGurantor or not ?
                            if (txt_City.Text != "")
                            {
                                _strSQL += " AND  Temp.sCity ='" + txt_City.Text.Replace("'", "''") + "'";
                            }
                            if (txt_State.Text != "")
                            {
                                _strSQL += " AND  Temp.sState ='" + txt_State.Text.Replace("'", "''") + "'";
                            }
                            if (txt_ZIP.Text != "")
                            {
                                _strSQL += " AND  Temp.sZIP ='" + txt_ZIP.Text.Replace("'", "''") + "'";
                            }
                            if (cmb_Gender.Text != "")
                            {
                                _strSQL += " AND  Temp.sGender ='" + cmb_Gender.Text + "'";
                            }
                            _strSQL += " ORDER BY Temp.sGuarantor";
                            oDB.Retrive_Query(_strSQL, out dt);

                            // End
                        }
                        break;
                    case "Insurances":
                        {
                            _strSQL = "";
                            _strSQL = " SELECT  ISNULL(Contacts_MST.nContactID,0) AS ContactID,ISNULL(Contacts_MST.sContactType, '') AS sContactType,ISNULL(Contacts_MST.sName, '') AS sName,  " +
                           " ISNULL(Contacts_MST.sContact, '') AS ContactName,ISNULL(Contacts_MST.sAddressLine1, '') AS AddressLine1,  " +
                            " ISNULL(Contacts_MST.sAddressLine2, '') AS AddressLine2, ISNULL(Contacts_MST.sCity, '') AS City, ISNULL(Contacts_MST.sState, '') AS State, " +
                           " ISNULL(Contacts_MST.sZIP, '') AS ZIP, ISNULL(Contacts_MST.sPhone, '') AS Phone, ISNULL(Contacts_MST.sFax, '') AS FAX, ISNULL(Contacts_MST.sMobile, '')  " +
                            " AS Mobile, ISNULL(Contacts_MST.sEmail, '') AS Email,  ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc,'') AS InsuranceTypeDesc , ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode,'') AS InsuranceTypeCode " +
                            " FROM  Contacts_MST LEFT OUTER JOIN Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID  " +
                            " WHERE  Contacts_MST.bIsBlocked = 0 AND Contacts_MST.sContactType= 'Insurance' AND Contacts_MST.nClinicID='" + _ClinicID + "'";

                            if (txt_City.Text != "")
                            {
                                _strSQL += " AND  Contacts_MST.sCity ='" + txt_City.Text + "'";
                            }
                            if (txt_State.Text != "")
                            {
                                _strSQL += " AND  Contacts_MST.sState ='" + txt_State.Text + "'";
                            }
                            if (txt_ZIP.Text != "")
                            {
                                _strSQL += " AND  Contacts_MST.sZIP ='" + txt_ZIP.Text + "'";
                            }
                            _strSQL += "  ORDER BY sName ";
                            oDB.Retrive_Query(_strSQL, out dt);
                        }
                        break;
                    case "Billing Code":
                        {
                            _strSQL = "SELECT  ISNULL(nCPTID,0)AS nCPTID,ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription FROM  CPT_MST " +
                                       "  WHERE  nClinicID = " + _ClinicID + " ORDER BY sCPTCode ";
                            oDB.Retrive_Query(_strSQL, out dt);
                        }
                        break;
                    case "Diagnosis":
                        {
                            _strSQL = "SELECT ISNULL(nICD9ID,0) AS nICD9ID, ISNULL(sICD9Code,'') AS sICD9Code,ISNULL(sDescription,'') AS sDescription" +
                                      " FROM ICD9 WHERE (bIsBlocked = 'false' OR bIsBlocked IS NULL)  AND nClinicID = " + _ClinicID + " AND ISNULL(nICDRevision,9)= " + (int)_CodeRevision +
                                      " ORDER BY sICD9Code ";
                            oDB.Retrive_Query(_strSQL, out dt);
                            
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dt;
        }

        #endregion

        #region  " ToolStrip Button Click Events  "

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            frmViewPatientList_Load(null, null);
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (c1ViewPatientList!= null && c1ViewPatientList.Rows.Count > 1)
            {
                ExportReportToExcel();
            }
            
        }

        private void ExportReportToExcel()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    _Checked = false;
                }
                _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                oSettings.Dispose();

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

                if (_DefaultLocationPath != "" && _Checked == true)
                {
                    if (_DefaultLocationPath.EndsWith("\\"))
                    {

                        _DefaultLocationPath = _DefaultLocationPath.Replace("\\", "");
                    }
                    // If not exist create directory
                    if (Directory.Exists(_DefaultLocationPath) == false)
                    {
                        Directory.CreateDirectory(_DefaultLocationPath);
                    }
                    saveFileDialog.InitialDirectory = _DefaultLocationPath;

                }

                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
              
                c1ViewPatientList.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                MessageBox.Show("File saved successfully.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (IOException)// ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ioEx.ToString();
                //ioEx = null;
            }
            catch (ArgumentException)// ArgEx)
            {
                MessageBox.Show("Cannot export diagnosis list; row count exceeded maximum limit.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //ArgEx.ToString();
                //ArgEx = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
 
        #endregion

        private void c1ViewPatientList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void rbICD9_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD9.Checked == true)
            {
                rbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbICD10.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD9;
            }
            else
            {
                rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbICD9.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD10;
            }
        }

    }
}