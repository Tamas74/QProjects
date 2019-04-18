using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloEmdeonInterface.Forms
{  
    public partial class frmMatchIntuitPatient : Form
    {
        #region "Variables"
           List<clsMatchingParameters> _oclsMatchingParameters;
           string sMatchingCriteria = null;
           private string _dataBaseConnectionString = string.Empty;
           private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
           private string gstrMessageBoxCaption = "gloEMR";

           public List<clsMatchingParameters> MatchedPatientData;
        #endregion "Variables"

           #region "Grid columns"

           const Int32 Col_PatientCode = 0;
           const Int32 Col_FirstName = 1;
           const Int32 Col_MiddleName = 2;
           const Int32 Col_LastName = 3;
           const Int32 Col_DOB = 4;
           const Int32 Col_Gender = 5;
           const Int32 Col_ZIP = 6;
           const Int32 Col_Email = 7;         
           const Int32 Col_MaritalStatus = 8;
           const Int32 Col_Race = 9;
           const Int32 Col_AddressLine1 = 10;
           const Int32 Col_AddressLine2 = 11;
           const Int32 Col_City = 12;
           const Int32 Col_State = 13;
           const Int32 Col_Country = 14;
           const Int32 Col_Phone = 15;
           const Int32 Col_Mobile = 16;
           const Int32 Col_WorkPhone=17;
           const Int32 Col_ProviderName = 18;
           const Int32 Col_PatientID = 19;
           const Int32 Col_ProviderID = 20;
           const Int32 Col_Count = 21;

           #endregion "Grid columns"

           public frmMatchIntuitPatient(List<clsMatchingParameters>  oclsMatchingParameters)
        {          
            InitializeComponent();

            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {
                gstrMessageBoxCaption = "gloEMR";
            }

            _oclsMatchingParameters = oclsMatchingParameters;           
        }

        #region "Design Patient List Grid"

            private void DesignPatientListGrid()
            {              
                try
                {
                    //c1PatientList.Clear();        
                     c1PatientList.DataSource = null;
                     c1PatientList.Clear();

                     // set font
                     c1PatientList.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                     c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                     c1PatientList.BackColor = Color.White;
                     c1PatientList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                     c1PatientList.Cols.Count = Col_Count;
                     c1PatientList.Cols.Fixed = 0;
                     c1PatientList.Rows.Count = 1;
                     c1PatientList.Rows.Fixed = 1;
                    
                     //Setting for hidden columns
                     c1PatientList.Cols[Col_PatientID].Width = 0;
                     c1PatientList.Cols[Col_ProviderID].Width = 0;                     
                     c1PatientList.Cols[Col_MaritalStatus].Width = 0;
                     c1PatientList.Cols[Col_Race].Width = 0;
                     c1PatientList.Cols[Col_AddressLine1].Width = 0;
                     c1PatientList.Cols[Col_AddressLine2].Width = 0;
                     c1PatientList.Cols[Col_City].Width = 0;
                     c1PatientList.Cols[Col_State].Width = 0;
                     c1PatientList.Cols[Col_Country].Width = 0;
                     c1PatientList.Cols[Col_Phone].Width = 0;
                     c1PatientList.Cols[Col_Mobile].Width = 0;
                     c1PatientList.Cols[Col_WorkPhone].Width = 0;

                     c1PatientList.Cols[Col_PatientCode].WidthDisplay = 90;
                     c1PatientList.Cols[Col_FirstName].WidthDisplay = 115;
                     c1PatientList.Cols[Col_MiddleName].WidthDisplay = 30;
                     c1PatientList.Cols[Col_LastName].WidthDisplay = 115;
                     c1PatientList.Cols[Col_DOB].WidthDisplay = 80;
                     c1PatientList.Cols[Col_Gender].WidthDisplay = 60;
                     c1PatientList.Cols[Col_ZIP].WidthDisplay = 50;
                     c1PatientList.Cols[Col_Email].WidthDisplay = 180;                     
                     c1PatientList.Cols[Col_ProviderName].WidthDisplay = 100;

                     //Setting for visible columns
                     c1PatientList.Cols[Col_PatientID].Visible = false;
                     c1PatientList.Cols[Col_ProviderID].Visible = false;                     
                     c1PatientList.Cols[Col_MaritalStatus].Visible = false;
                     c1PatientList.Cols[Col_Race].Visible = false;
                     c1PatientList.Cols[Col_AddressLine1].Visible = false;
                     c1PatientList.Cols[Col_AddressLine2].Visible = false;
                     c1PatientList.Cols[Col_City].Visible = false;
                     c1PatientList.Cols[Col_State].Visible = false;
                     c1PatientList.Cols[Col_Country].Visible = false;
                     c1PatientList.Cols[Col_Phone].Visible = false;
                     c1PatientList.Cols[Col_Mobile].Visible = false;
                     c1PatientList.Cols[Col_WorkPhone].Visible = false;

                     c1PatientList.Cols[Col_PatientCode].Visible = true;
                     c1PatientList.Cols[Col_FirstName].Visible = true;
                     c1PatientList.Cols[Col_MiddleName].Visible = true;
                     c1PatientList.Cols[Col_LastName].Visible = true;
                     c1PatientList.Cols[Col_DOB].Visible = true;
                     c1PatientList.Cols[Col_Gender].Visible = true;
                     c1PatientList.Cols[Col_ZIP].Visible = true;
                     c1PatientList.Cols[Col_Email].Visible = true;                    
                     c1PatientList.Cols[Col_ProviderName].Visible = true;
                    
                     //set Heading
                     c1PatientList.SetData(0, Col_PatientCode, "Patient Code");
                     c1PatientList.SetData(0, Col_FirstName, "First Name");
                     c1PatientList.SetData(0, Col_MiddleName, "MI");
                     c1PatientList.SetData(0, Col_LastName, "Last Name");
                     c1PatientList.SetData(0, Col_DOB, "DOB");
                     c1PatientList.SetData(0, Col_Gender, "Gender");
                     c1PatientList.SetData(0,Col_ZIP , "ZIP");
                     c1PatientList.SetData(0, Col_Email, "Email");                     
                     c1PatientList.SetData(0, Col_MaritalStatus, "Marital Status");
                     c1PatientList.SetData(0,Col_Race , "Race");
                     c1PatientList.SetData(0, Col_AddressLine1, "Address 1");
                     c1PatientList.SetData(0, Col_AddressLine2, "Address 2");
                     c1PatientList.SetData(0, Col_City, "City");
                     c1PatientList.SetData(0,Col_State , "State");
                     c1PatientList.SetData(0,Col_Country , "Country");
                     c1PatientList.SetData(0,Col_Phone , "Phone");
                     c1PatientList.SetData(0, Col_Mobile, "Mobile");
                     c1PatientList.SetData(0, Col_WorkPhone, "Work Phone");
                     c1PatientList.SetData(0, Col_ProviderName, "Provider Name");
                     c1PatientList.SetData(0,Col_PatientID , "ID1");
                     c1PatientList.SetData(0,Col_ProviderID , "ID2");

                      // set column editing
                     c1PatientList.Cols[Col_PatientCode].AllowEditing = false;
                     c1PatientList.Cols[Col_FirstName].AllowEditing = false;
                     c1PatientList.Cols[Col_MiddleName].AllowEditing = false;
                     c1PatientList.Cols[Col_LastName].AllowEditing = false;
                     c1PatientList.Cols[Col_DOB].AllowEditing = false;
                     c1PatientList.Cols[Col_Gender].AllowEditing = false;
                     c1PatientList.Cols[Col_ZIP].AllowEditing = false;
                     c1PatientList.Cols[Col_Email].AllowEditing = false;                     
                     c1PatientList.Cols[Col_MaritalStatus].AllowEditing = false;
                     c1PatientList.Cols[Col_Race].AllowEditing = false;
                     c1PatientList.Cols[Col_AddressLine1].AllowEditing = false;
                     c1PatientList.Cols[Col_AddressLine2].AllowEditing = false;
                     c1PatientList.Cols[Col_City].AllowEditing = false;
                     c1PatientList.Cols[Col_State].AllowEditing = false;
                     c1PatientList.Cols[Col_Country].AllowEditing = false;
                     c1PatientList.Cols[Col_Phone].AllowEditing = false;
                     c1PatientList.Cols[Col_Mobile].AllowEditing = false;
                     c1PatientList.Cols[Col_WorkPhone].AllowEditing = false;
                     c1PatientList.Cols[Col_ProviderName].AllowEditing = false;
                     c1PatientList.Cols[Col_PatientID].AllowEditing = false;
                     c1PatientList.Cols[Col_ProviderID].AllowEditing = false;                     

                      ////setting text alignment for a columns
                     c1PatientList.Cols[Col_PatientCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_FirstName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_MiddleName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_LastName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_DOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Gender].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Email].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;                     
                     c1PatientList.Cols[Col_MaritalStatus].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Race].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_AddressLine1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_AddressLine2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_City].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Country].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_WorkPhone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_ProviderName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_PatientID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                     c1PatientList.Cols[Col_ProviderID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                   
                     c1PatientList.ExtendLastCol = true;                   
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error Occurred while designing patient list grid : " + ex.ToString(), false);
                    ex = null;
                }
            }

        #endregion "Design Patient List Grid"

        #region "Events"
            
            private void frmMatchIntuitPatient_Load(object sender, EventArgs e)
            {
                try
                {
                    DesignPatientListGrid();

                    //Making labels values blank                  
                    lblNameValue.Text=string.Empty;
                    lblGenderValue.Text=string.Empty;
                    lblDOBValue.Text=string.Empty;
                    lblZIPValue.Text=string.Empty;
                    lblEmailValue.Text=string.Empty;
                    //lblSSNValue.Text = string.Empty;                                           
                    //End for making labels values blank

                    string sFN = string.Empty, sMN = string.Empty, sLN = string.Empty;

                    //Assigning value to label for Intuit searching patient
                    foreach (clsMatchingParameters obj in _oclsMatchingParameters)
                    {
                        switch (obj.FieldName.ToUpper())
                        {
                            case "SFIRSTNAME":
                                sFN = obj.FieldValue;
                                break;
                            case "SMIDDLENAME":
                                sMN = obj.FieldValue;
                                break;
                            case "SLASTNAME":
                                sLN = obj.FieldValue;
                                break;
                            case "DTDOB":
                                lblDOBValue.Text = obj.FieldValue;
                                break;
                            case "SEMAIL":
                                lblEmailValue.Text = obj.FieldValue;
                                break;
                            case "SGENDER":
                                lblGenderValue.Text = obj.FieldValue;
                                break;                            
                            case "SZIP":
                                lblZIPValue.Text=obj.FieldValue;
                                break;
                            default:
                                break;
                        }                        
                    }
                    lblNameValue.Text = sLN + " " + sFN + " " + sMN;
                    //End of Assigning value to label for Intuit searching patient

                    //Focusing Search TextBox
                    txtSearch.Focus();

                    //Making Matching Criteria
                    sMatchingCriteria = GetMatchingCriteria(_oclsMatchingParameters);

                    //Populating matched patients

                    DataTable dtPatients = GetMatchedPatients();

                    if (dtPatients != null)
                    {
                        Int32 _Row = 0;
                        for (Int32 nRow = 0; nRow < dtPatients.Rows.Count; nRow++)
                        {
                            c1PatientList.Rows.Add();
                            _Row = c1PatientList.Rows.Count - 1;

                            c1PatientList.SetData(_Row, Col_PatientCode, Convert.ToString(dtPatients.Rows[nRow]["PatientCode"]));
                            c1PatientList.SetData(_Row, Col_FirstName, Convert.ToString(dtPatients.Rows[nRow]["FirstName"]));
                            c1PatientList.SetData(_Row, Col_MiddleName, Convert.ToString(dtPatients.Rows[nRow]["MI"]));
                            c1PatientList.SetData(_Row, Col_LastName, Convert.ToString(dtPatients.Rows[nRow]["LastName"]));
                            c1PatientList.SetData(_Row, Col_DOB, Convert.ToString(dtPatients.Rows[nRow]["DOB"]));
                            c1PatientList.SetData(_Row, Col_Gender, Convert.ToString(dtPatients.Rows[nRow]["Gender"]));
                            c1PatientList.SetData(_Row, Col_ZIP, Convert.ToString(dtPatients.Rows[nRow]["ZIP"]));
                            c1PatientList.SetData(_Row, Col_Email, Convert.ToString(dtPatients.Rows[nRow]["Email"]));                            
                            c1PatientList.SetData(_Row, Col_MaritalStatus, Convert.ToString(dtPatients.Rows[nRow]["sMaritalStatus"]));
                            c1PatientList.SetData(_Row, Col_Race, Convert.ToString(dtPatients.Rows[nRow]["sRace"]));
                            c1PatientList.SetData(_Row, Col_AddressLine1, Convert.ToString(dtPatients.Rows[nRow]["sAddressLine1"]));
                            c1PatientList.SetData(_Row, Col_AddressLine2, Convert.ToString(dtPatients.Rows[nRow]["sAddressLine2"]));
                            c1PatientList.SetData(_Row, Col_City, Convert.ToString(dtPatients.Rows[nRow]["sCity"]));
                            c1PatientList.SetData(_Row, Col_State, Convert.ToString(dtPatients.Rows[nRow]["sState"]));
                            c1PatientList.SetData(_Row, Col_Country, Convert.ToString(dtPatients.Rows[nRow]["sCountry"]));
                            c1PatientList.SetData(_Row, Col_Phone, Convert.ToString(dtPatients.Rows[nRow]["sPhone"]));
                            c1PatientList.SetData(_Row, Col_Mobile, Convert.ToString(dtPatients.Rows[nRow]["sMobile"]));
                            c1PatientList.SetData(_Row, Col_WorkPhone, Convert.ToString(dtPatients.Rows[nRow]["sWorkPhone"]));
                            c1PatientList.SetData(_Row, Col_ProviderName, Convert.ToString(dtPatients.Rows[nRow]["ProviderName"]));
                            c1PatientList.SetData(_Row, Col_PatientID, Convert.ToString(dtPatients.Rows[nRow]["nPatientID"]));
                            c1PatientList.SetData(_Row, Col_ProviderID, Convert.ToString(dtPatients.Rows[nRow]["nProviderID"]));
                        }
                    }                                     
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error Occurred while loading form of match Portal patient : " + ex.ToString(), false);
                    ex = null;
                }
            }
            
            private void ts_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {
                try
                {
                    switch (e.ClickedItem.Tag.ToString().ToUpper())
                    {
                        case "SAVE&CLOSE" :

                            if (c1PatientList.Rows.Count > 1)
                            {
                                if (c1PatientList.RowSel > 0)
                                {
                                    Int32 curRow = c1PatientList.RowSel;
                                    MatchedPatientData = new List<clsMatchingParameters>();

                                    MatchedPatientData.Add(new clsMatchingParameters("sPatientCode", Convert.ToString(c1PatientList.GetData(curRow, Col_PatientCode))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sFirstName", Convert.ToString(c1PatientList.GetData(curRow, Col_FirstName))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sMiddleName", Convert.ToString(c1PatientList.GetData(curRow, Col_MiddleName))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sLastName", Convert.ToString(c1PatientList.GetData(curRow, Col_LastName))));
                                    MatchedPatientData.Add(new clsMatchingParameters("dtDOB", Convert.ToString(c1PatientList.GetData(curRow, Col_DOB))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sGender", Convert.ToString(c1PatientList.GetData(curRow, Col_Gender))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sZIP", Convert.ToString(c1PatientList.GetData(curRow, Col_ZIP))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sEmail", Convert.ToString(c1PatientList.GetData(curRow, Col_Email))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sMaritalStatus", Convert.ToString(c1PatientList.GetData(curRow, Col_MaritalStatus))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sRace", Convert.ToString(c1PatientList.GetData(curRow, Col_Race))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sAddressLine1", Convert.ToString(c1PatientList.GetData(curRow, Col_AddressLine1))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sAddressLine2", Convert.ToString(c1PatientList.GetData(curRow, Col_AddressLine2))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sCity", Convert.ToString(c1PatientList.GetData(curRow, Col_City))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sState", Convert.ToString(c1PatientList.GetData(curRow, Col_State))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sCountry", Convert.ToString(c1PatientList.GetData(curRow, Col_Country))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sPhone", Convert.ToString(c1PatientList.GetData(curRow, Col_Phone))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sMobile", Convert.ToString(c1PatientList.GetData(curRow, Col_Mobile))));
                                    MatchedPatientData.Add(new clsMatchingParameters("sWorkPhone", Convert.ToString(c1PatientList.GetData(curRow, Col_WorkPhone))));
                                    MatchedPatientData.Add(new clsMatchingParameters("nPatientID", Convert.ToString(c1PatientList.GetData(curRow, Col_PatientID))));
                                    MatchedPatientData.Add(new clsMatchingParameters("ProviderName", Convert.ToString(c1PatientList.GetData(curRow, Col_ProviderName))));
                                    MatchedPatientData.Add(new clsMatchingParameters("nProviderID", Convert.ToString(c1PatientList.GetData(curRow, Col_ProviderID))));

                                    this.Close();                                                                          
                                }
                                else
                                {
                                    MessageBox.Show("Select patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No patient available for matching.",gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            break;
                        case "CLOSE":
                            this.Close();
                            break;
                        default:
                            break;                        
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error Occurred on click of menu item of Portal patient match: " + e.ClickedItem.Tag.ToString() +" : "  + ex.ToString(), false);
                    ex = null;
                }
            }

            private void c1PatientList_DoubleClick(object sender, EventArgs e)
            {
                try
                {
                    if (c1PatientList.Rows.Count > 1)
                    {
                        if (c1PatientList.RowSel >0)
                        {
                            Int32 curRow = c1PatientList.RowSel;
                            MatchedPatientData = new List<clsMatchingParameters>();
                            
                            MatchedPatientData.Add(new clsMatchingParameters("sPatientCode", Convert.ToString(c1PatientList.GetData(curRow, Col_PatientCode))));
                            MatchedPatientData.Add(new clsMatchingParameters("sFirstName", Convert.ToString(c1PatientList.GetData(curRow, Col_FirstName))));
                            MatchedPatientData.Add(new clsMatchingParameters("sMiddleName", Convert.ToString(c1PatientList.GetData(curRow, Col_MiddleName))));
                            MatchedPatientData.Add(new clsMatchingParameters("sLastName", Convert.ToString(c1PatientList.GetData(curRow, Col_LastName))));
                            MatchedPatientData.Add(new clsMatchingParameters("dtDOB", Convert.ToString(c1PatientList.GetData(curRow, Col_DOB))));
                            MatchedPatientData.Add(new clsMatchingParameters("sGender", Convert.ToString(c1PatientList.GetData(curRow, Col_Gender))));
                            MatchedPatientData.Add(new clsMatchingParameters("sZIP", Convert.ToString(c1PatientList.GetData(curRow, Col_ZIP))));
                            MatchedPatientData.Add(new clsMatchingParameters("sEmail", Convert.ToString(c1PatientList.GetData(curRow, Col_Email))));
                            MatchedPatientData.Add(new clsMatchingParameters("sMaritalStatus", Convert.ToString(c1PatientList.GetData(curRow, Col_MaritalStatus))));
                            MatchedPatientData.Add(new clsMatchingParameters("sRace", Convert.ToString(c1PatientList.GetData(curRow, Col_Race))));
                            MatchedPatientData.Add(new clsMatchingParameters("sAddressLine1", Convert.ToString(c1PatientList.GetData(curRow, Col_AddressLine1))));
                            MatchedPatientData.Add(new clsMatchingParameters("sAddressLine2", Convert.ToString(c1PatientList.GetData(curRow, Col_AddressLine2))));
                            MatchedPatientData.Add(new clsMatchingParameters("sCity", Convert.ToString(c1PatientList.GetData(curRow, Col_City))));
                            MatchedPatientData.Add(new clsMatchingParameters("sState", Convert.ToString(c1PatientList.GetData(curRow, Col_State))));
                            MatchedPatientData.Add(new clsMatchingParameters("sCountry", Convert.ToString(c1PatientList.GetData(curRow, Col_Country))));
                            MatchedPatientData.Add(new clsMatchingParameters("sPhone", Convert.ToString(c1PatientList.GetData(curRow, Col_Phone))));
                            MatchedPatientData.Add(new clsMatchingParameters("sMobile", Convert.ToString(c1PatientList.GetData(curRow, Col_Mobile))));
                            MatchedPatientData.Add(new clsMatchingParameters("sWorkPhone", Convert.ToString(c1PatientList.GetData(curRow, Col_WorkPhone))));
                            MatchedPatientData.Add(new clsMatchingParameters("nPatientID", Convert.ToString(c1PatientList.GetData(curRow, Col_PatientID))));
                            MatchedPatientData.Add(new clsMatchingParameters("ProviderName", Convert.ToString(c1PatientList.GetData(curRow, Col_ProviderName))));
                            MatchedPatientData.Add(new clsMatchingParameters("nProviderID", Convert.ToString(c1PatientList.GetData(curRow, Col_ProviderID))));
                                                            
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while selecting matched Portal patient: " + ex.ToString(), false);
                    ex = null;
                }                                
            }

            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                DataTable dtResultsPatients = new DataTable();
                DataView dv = new DataView();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                try
                {                   
                    if (txtSearch.Text.Trim().Length > 0)
                    {
                        string[] strSearchArray = null;
                        string strPatientSearch = "";
                        if (this.txtSearch.Text.Trim() != "")
                        {
                            // if search not blank then replace the ',[,],%,* with space character.
                            strPatientSearch = this.txtSearch.Text.Trim();
                            strPatientSearch = strPatientSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                        }
                        if (strPatientSearch.Length > 1)
                        {
                            string str = strPatientSearch.Substring(1).Replace("%", "");
                            strPatientSearch = strPatientSearch.Substring(0, 1) + str;
                        }
                        if (strPatientSearch.Trim() != "")
                        {
                            // split the search text with comma character  for multiple search
                            char[] splitchar = { ',' };
                            strSearchArray = strPatientSearch.Split(splitchar);
                        }

                        string sCondition = string.Empty;

                        if (strPatientSearch.Trim() != "")
                        {

                            if (strSearchArray.Length == 1) // single search 
                            {
                                strPatientSearch = strSearchArray[0].Trim();

                                sCondition = " a.sPatientCode Like '" + strPatientSearch + "%' or a.sFirstName Like '" + strPatientSearch + "%' or " +
                                             " a.sMiddleName Like '" + strPatientSearch + "%' or a.sLastName Like '" + strPatientSearch + "%' or " +
                                             " convert(varchar,a.dtDOB,101) Like '" + strPatientSearch + "%' or " +
                                             " a.sGender Like '" + strPatientSearch + "%' or a.sEmail Like '" + strPatientSearch + "%' or " +
                                             " a.sZip Like '" + strPatientSearch + "%' or b.sFirstName Like '" + strPatientSearch + "%'";
                            }
                            else  // multiple search separated by comma
                            {
                                for (int loopCnt = 0; loopCnt <= strSearchArray.Length - 1; loopCnt++)
                                {
                                    strPatientSearch = strSearchArray[loopCnt].Trim();
                                    if (strPatientSearch.Trim() != "")
                                    {
                                        if (loopCnt == 0)
                                        {
                                            sCondition = "";

                                            sCondition = " (a.sPatientCode Like '" + strPatientSearch + "%' or a.sFirstName Like '" + strPatientSearch + "%' or " +
                                                     " a.sMiddleName Like '" + strPatientSearch + "%' or a.sLastName Like '" + strPatientSearch + "%' or " +
                                                     " convert(varchar,a.dtDOB,101) Like '" + strPatientSearch + "%' or " +
                                                      " a.sGender Like '" + strPatientSearch + "%' or a.sEmail Like '" + strPatientSearch + "%' or " +
                                                      " a.sZip Like '" + strPatientSearch + "%' or b.sFirstName Like '" + strPatientSearch + "%')";

                                        }
                                        else if (sCondition != "")
                                        {
                                            sCondition = sCondition + " AND";

                                            sCondition = sCondition + " (a.sPatientCode Like '" + strPatientSearch + "%' or " +
                                                   " a.sFirstName Like '" + strPatientSearch + "%' or  a.sMiddleName Like '" + strPatientSearch + "%' or " +
                                                   " a.sLastName Like '" + strPatientSearch + "%' or " +
                                                   " a.sGender Like '" + strPatientSearch + "%' or a.sEmail Like '" + strPatientSearch + "%' or " +
                                                   " a.sZip Like '" + strPatientSearch + "%' or convert(varchar,a.dtDOB,101) Like '" + strPatientSearch + "%' or  b.sFirstName Like '" + strPatientSearch + "%')";
                                        }
                                    }
                                }
                            }
                        }

                        if (sCondition.Length > 0)
                        {
                            sCondition = " and " + sCondition;
                        }

                        string sQuery = "select top 100 a.sPatientCode as [PatientCode],a.sFirstName as [FirstName] ,a.sMiddleName as [MI],a.sLastName as [LastName] ,convert(varchar,a.dtDOB,101) as [DOB]," +
                                " isnull(a.sGender,'') as Gender,isnull(a.sZip,'') as ZIP,isnull(a.sEmail,'') as Email, " +
                                " isnull(a.sMaritalStatus,'') as sMaritalStatus, isnull(dbo.fn_GetRaceEthnicity(a.nPatientID,'race','|'),'') as sRace, isnull(a.sAddressLine1,'') as sAddressLine1, " +
                                " isnull(a.sAddressLine2,'') as sAddressLine2, isnull(a.sCity,'') as sCity,isnull(a.sState,'') as sState, " +
                                " isnull(a.sCountry,'') as sCountry, isnull(a.sPhone,'') as sPhone, isnull(a.sMobile,'') as sMobile, isnull(a.sWorkPhone,'') as sWorkPhone, " +
                                " rtrim(b.sFirstName) +' '+ rtrim(b.sMiddleName)+' '+ rtrim(b.sLastName) as [ProviderName], " +
                                " a.nPatientID,a.nProviderID from patient a left outer join  provider_mst b " +
                                " on a.nProviderID=b.nProviderID where a.nclinicid >=" + "0  " + sCondition.Trim() + " order by a.sFirstName";

                       
                        oDB.Connect(false);
                        oDB.Retrive_Query(sQuery, out dtResultsPatients);
                        oDB.Disconnect();

                        DesignPatientListGrid();
                        if (dtResultsPatients != null)
                        {
                            DesignPatientListGrid();

                            Int32 _Row = 0;
                            for (Int32 nRow = 0; nRow < dtResultsPatients.Rows.Count; nRow++)
                            {
                                c1PatientList.Rows.Add();
                                _Row = c1PatientList.Rows.Count - 1;

                                c1PatientList.SetData(_Row, Col_PatientCode, Convert.ToString(dtResultsPatients.Rows[nRow]["PatientCode"]));
                                c1PatientList.SetData(_Row, Col_FirstName, Convert.ToString(dtResultsPatients.Rows[nRow]["FirstName"]));
                                c1PatientList.SetData(_Row, Col_MiddleName, Convert.ToString(dtResultsPatients.Rows[nRow]["MI"]));
                                c1PatientList.SetData(_Row, Col_LastName, Convert.ToString(dtResultsPatients.Rows[nRow]["LastName"]));
                                c1PatientList.SetData(_Row, Col_DOB, Convert.ToString(dtResultsPatients.Rows[nRow]["DOB"]));
                                c1PatientList.SetData(_Row, Col_Gender, Convert.ToString(dtResultsPatients.Rows[nRow]["Gender"]));
                                c1PatientList.SetData(_Row, Col_ZIP, Convert.ToString(dtResultsPatients.Rows[nRow]["ZIP"]));
                                c1PatientList.SetData(_Row, Col_Email, Convert.ToString(dtResultsPatients.Rows[nRow]["Email"]));                                                               
                                c1PatientList.SetData(_Row, Col_MaritalStatus, Convert.ToString(dtResultsPatients.Rows[nRow]["sMaritalStatus"]));
                                c1PatientList.SetData(_Row, Col_Race, Convert.ToString(dtResultsPatients.Rows[nRow]["sRace"]));
                                c1PatientList.SetData(_Row, Col_AddressLine1, Convert.ToString(dtResultsPatients.Rows[nRow]["sAddressLine1"]));
                                c1PatientList.SetData(_Row, Col_AddressLine2, Convert.ToString(dtResultsPatients.Rows[nRow]["sAddressLine2"]));
                                c1PatientList.SetData(_Row, Col_City, Convert.ToString(dtResultsPatients.Rows[nRow]["sCity"]));
                                c1PatientList.SetData(_Row, Col_State, Convert.ToString(dtResultsPatients.Rows[nRow]["sState"]));
                                c1PatientList.SetData(_Row, Col_Country, Convert.ToString(dtResultsPatients.Rows[nRow]["sCountry"]));
                                c1PatientList.SetData(_Row, Col_Phone, Convert.ToString(dtResultsPatients.Rows[nRow]["sPhone"]));
                                c1PatientList.SetData(_Row, Col_Mobile, Convert.ToString(dtResultsPatients.Rows[nRow]["sMobile"]));
                                c1PatientList.SetData(_Row, Col_WorkPhone, Convert.ToString(dtResultsPatients.Rows[nRow]["sWorkPhone"]));
                                c1PatientList.SetData(_Row, Col_ProviderName, Convert.ToString(dtResultsPatients.Rows[nRow]["ProviderName"]));
                                c1PatientList.SetData(_Row, Col_PatientID, Convert.ToString(dtResultsPatients.Rows[nRow]["nPatientID"]));
                                c1PatientList.SetData(_Row, Col_ProviderID, Convert.ToString(dtResultsPatients.Rows[nRow]["nProviderID"]));
                            }
                        }
                        
                        strSearchArray = null;
                        strPatientSearch = null;
                        sQuery = null;
                    }
                    else
                    {
                        dtResultsPatients = GetMatchedPatients();

                        DesignPatientListGrid();

                        if (dtResultsPatients != null)
                        {
                            Int32 _Row = 0;
                            for (Int32 nRow = 0; nRow < dtResultsPatients.Rows.Count; nRow++)
                            {
                                c1PatientList.Rows.Add();
                                _Row = c1PatientList.Rows.Count - 1;

                                c1PatientList.SetData(_Row, Col_PatientCode, Convert.ToString(dtResultsPatients.Rows[nRow]["PatientCode"]));
                                c1PatientList.SetData(_Row, Col_FirstName, Convert.ToString(dtResultsPatients.Rows[nRow]["FirstName"]));
                                c1PatientList.SetData(_Row, Col_MiddleName, Convert.ToString(dtResultsPatients.Rows[nRow]["MI"]));
                                c1PatientList.SetData(_Row, Col_LastName, Convert.ToString(dtResultsPatients.Rows[nRow]["LastName"]));
                                c1PatientList.SetData(_Row, Col_DOB, Convert.ToString(dtResultsPatients.Rows[nRow]["DOB"]));
                                c1PatientList.SetData(_Row, Col_Gender, Convert.ToString(dtResultsPatients.Rows[nRow]["Gender"]));
                                c1PatientList.SetData(_Row, Col_ZIP, Convert.ToString(dtResultsPatients.Rows[nRow]["ZIP"]));
                                c1PatientList.SetData(_Row, Col_Email, Convert.ToString(dtResultsPatients.Rows[nRow]["Email"]));                                                               
                                c1PatientList.SetData(_Row, Col_MaritalStatus, Convert.ToString(dtResultsPatients.Rows[nRow]["sMaritalStatus"]));
                                c1PatientList.SetData(_Row, Col_Race, Convert.ToString(dtResultsPatients.Rows[nRow]["sRace"]));
                                c1PatientList.SetData(_Row, Col_AddressLine1, Convert.ToString(dtResultsPatients.Rows[nRow]["sAddressLine1"]));
                                c1PatientList.SetData(_Row, Col_AddressLine2, Convert.ToString(dtResultsPatients.Rows[nRow]["sAddressLine2"]));
                                c1PatientList.SetData(_Row, Col_City, Convert.ToString(dtResultsPatients.Rows[nRow]["sCity"]));
                                c1PatientList.SetData(_Row, Col_State, Convert.ToString(dtResultsPatients.Rows[nRow]["sState"]));
                                c1PatientList.SetData(_Row, Col_Country, Convert.ToString(dtResultsPatients.Rows[nRow]["sCountry"]));
                                c1PatientList.SetData(_Row, Col_Phone, Convert.ToString(dtResultsPatients.Rows[nRow]["sPhone"]));
                                c1PatientList.SetData(_Row, Col_Mobile, Convert.ToString(dtResultsPatients.Rows[nRow]["sMobile"]));
                                c1PatientList.SetData(_Row, Col_WorkPhone, Convert.ToString(dtResultsPatients.Rows[nRow]["sWorkPhone"]));
                                c1PatientList.SetData(_Row, Col_ProviderName, Convert.ToString(dtResultsPatients.Rows[nRow]["ProviderName"]));
                                c1PatientList.SetData(_Row, Col_PatientID, Convert.ToString(dtResultsPatients.Rows[nRow]["nPatientID"]));
                                c1PatientList.SetData(_Row, Col_ProviderID, Convert.ToString(dtResultsPatients.Rows[nRow]["nProviderID"]));
                            }  
                        }                       
                    }
                }
                catch //(Exception ex)
                {
                  //  ex = null;
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

        #endregion "Events"

        #region "Methods"

            private DataTable GetMatchedPatients()
            {
                DataTable dtPatients = null;
                gloDatabaseLayer.DBLayer oDB = null;
                try
                {
                    string strSql =string.Empty;
                    string strCondition =string.Empty;
                    string strTop100 =string.Empty;

                    if (txtSearch.Text.Trim().Length ==0)
                    {
                        // when  search is blank then search records according Intuit Patient Details
                            // take the top 100 records
                            strTop100 = " Top 100 ";
                            // make condition according to  HL7 Setting
                            if (sMatchingCriteria != null)
                            {
                                strCondition = sMatchingCriteria;
                            }
                            else
                            {
                                strCondition = string.Empty;
                            }
                    }
                    else
                    {
                        // when  search is blank then don't consider matching criteria as Intuit patient and don't consider top clause
                        strTop100 = string.Empty;
                        strCondition = string.Empty;
                    }

                    // Making the query according to condition
                    strSql = "select " + strTop100 + "a.sPatientCode as [PatientCode]  ,a.sFirstName as [FirstName] ,a.sMiddleName as [MI] ,a.sLastName as [LastName],convert(varchar,a.dtDOB,101) as [DOB]," +
                            " isnull(a.sGender,'') as [Gender],isnull(a.sZip,'') as [ZIP],isnull(a.sEmail,'') as [Email], " +
                            " isnull(a.sMaritalStatus,'') as sMaritalStatus, isnull(dbo.fn_GetRaceEthnicity(a.nPatientID,'race','|'),'') as sRace, isnull(a.sAddressLine1,'') as sAddressLine1, " +
                            " isnull(a.sAddressLine2,'') as sAddressLine2, isnull(a.sCity,'') as sCity,isnull(a.sState,'') as sState, " +
                            " isnull(a.sCountry,'') as sCountry, isnull(a.sPhone,'') as sPhone, isnull(a.sMobile,'') as sMobile, isnull(a.sWorkPhone,'') as sWorkPhone, " +
                            " rtrim(b.sFirstName) +' '+ rtrim(b.sMiddleName)+' '+ rtrim(b.sLastName) as [ProviderName], " +
                            " a.nPatientID,a.nProviderID from patient a left outer join  provider_mst b " +
                            " on a.nProviderID=b.nProviderID where a.nclinicid >=" + "0  " + strCondition.Trim() + " order by a.sFirstName";

                    oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                    oDB.Connect(false);
                    oDB.Retrive_Query(strSql, out dtPatients);
                    oDB.Disconnect();                  
                    return dtPatients;
                }
                catch// (Exception ex)
                {
                    //ex = null;
                    return dtPatients;
                }
                finally
                {
                    if (dtPatients!=null)
                    {
                        dtPatients.Dispose();
                        dtPatients = null;
                    }
                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                }
            }

        private string GetMatchingCriteria(List<clsMatchingParameters> _oclsMatchingParameters)
        {
            string sCondition = string.Empty;

            try
            {
                foreach (clsMatchingParameters obj in _oclsMatchingParameters)
                {
                    if (obj.FieldValue.Trim().Length > 0)
                    {                    
                        switch (obj.FieldName.ToUpper())
                        {
                            case "SFIRSTNAME":                      
                                    if (sCondition.Length == 0)
                                        sCondition = " a.sFirstName ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                    else
                                        sCondition = sCondition + " or a.sLastName ='" + obj.FieldValue.Replace("'", "''") + "' ";                      
                                break;
                            case "SMIDDLENAME":
                                if (sCondition.Length == 0)
                                    sCondition = " a.sMiddleName ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                else
                                    sCondition = sCondition + " or a.sMiddleName ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                break;
                            case "SLASTNAME":
                                if (sCondition.Length == 0)
                                    sCondition = " a.sLastName ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                else
                                    sCondition = sCondition + " or a.sLastName ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                break;
                            case "DTDOB":
                                if (sCondition.Length == 0)
                                    sCondition = " a.dtDOB ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                else
                                    sCondition = sCondition + " or a.dtDOB ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                break;
                            case "SEMAIL":
                                if (sCondition.Length == 0)
                                    sCondition = " a.sEmail ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                else
                                    sCondition = sCondition + " or a.sEmail ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                break;
                            case "SGENDER":
                                if (sCondition.Length == 0)
                                    sCondition = " a.sGender ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                else
                                    sCondition = sCondition + " or a.sGender ='" + obj.FieldValue.Replace("'", "''") + "' ";
                                break;                            
                            default:
                                break;
                        }
                    }
                }

                if (sCondition.Length > 0)
                    sCondition = " and (" + sCondition.Trim() + ")";

                return sCondition;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error Occurred while making matching criteria string:  " + ex.ToString(), false);
                ex = null;
                return sCondition;
            }
            finally
            {
                if (_oclsMatchingParameters!=null)
                {
                    //_oclsMatchingParameters
                    _oclsMatchingParameters = null;
                }
            }
        }
        #endregion "Methods"        

    }

    public class clsMatchingParameters : IDisposable
    {
        #region "Variables"
        private IntPtr handle;
        private Component component = new Component();
        private bool disposed = false;
        #endregion "Variables"

        #region "Constructor"
        public clsMatchingParameters(string sFieldName, string sFieldValue)
        {
            FieldName = sFieldName;
            FieldValue = sFieldValue;

            sFieldName = string.Empty;
            sFieldValue = string.Empty;
        }

        public clsMatchingParameters(IntPtr handle)
        {
            this.handle = handle;
        }

        #endregion "Constructor"

        #region "Destructor"
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        protected virtual void Dispose(Boolean disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    component.Dispose();
                }
                CloseHandle(handle);
                handle = IntPtr.Zero;

                // Note disposing has been done.
                disposed = true;
            }
        }
        ~clsMatchingParameters()
        {
            Dispose(false);
        }
        #endregion "Destructor"

        #region "Properties"
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        #endregion "Properties"
    }
}
