using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;
using System.Collections;
using gloPatientPortalCommon;
using System.Data.SqlClient;

namespace gloEmdeonInterface.Forms
{
    public partial class frmIntuitPatientReview : Form
    {
        #region "Intuit Patient Grid columns"
            private const Int16 COL_IntuitTransactionID = 0;
            private const Int16 COL_IntuitMemberID = 1;
            private const Int16 COL_IntuitPatientName = 3;
            private const Int16 COL_IntuitPatientType = 4;
            private const Int16 COL_COUNT_IntuitPatientListGrid = 5;
            //Task #67686: Date wise data – show date in Left panel – desc order.
            //Added column to show Date and change column no.
            private const Int16 COL_IntuitPatientImportedDate = 2;
        #endregion "Intuit Patient Grid columns"

        #region "Patient Details Grid Columns"
            private const Int16 COL_Select = 0;
            private const Int16 COL_FieldName = 1;            
            private const Int16 COL_IntuitPatient = 2;
            private const Int16 COL_EMRPatient = 3;
            private const Int16 COL_Field = 4;            
            private const Int16 COL_COUNT_PatientDetailsGrid = 5;
        #endregion "Patient Details Grid Columns"

        #region "Variables Declaration"

            private string _dataBaseConnectionString = string.Empty;
            private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string gstrMessageBoxCaption = "gloEMR";
           
            private Boolean _IsDataLoaded = false;

            private long _nMatchedPatientID = 0;
            private long _nMatchedPatientProviderID = 0;

            private Boolean _IsGridIntuitValueEdited = false;

            private string _CountryList = " |US|Canada";
            private string _GenderList = " |Female|Male|Other";
            private string _MaritalStatusList = " |Divorced|Married|Single|UnMarried|Widowed";
            private string _RaceList = " |";
            private string _LanguageList = " |";
            private string _EthnicityList = " |";
            private bool _bMultipleRace = false;
            Boolean bIsPatientPortalEnabled = false;
            bool bIntuitEnabled = false;
            ToolTip toolTip = null; 
            //Task #67685: Search
            //Added search logic on PatientName column in c1IntuitPatientList.
            Timer oTimer = new Timer();
            DateTime _CurrentTime;

        #endregion "Variables Declaration"

        #region "Constructor"

            public frmIntuitPatientReview(bool bMultipleRace,bool IsIntuitEnabled)
            {
                InitializeComponent();

                _bMultipleRace = bMultipleRace;

                bIntuitEnabled = IsIntuitEnabled;

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
            }

        #endregion "Constructor"

        #region "Grid Design"

            //Design Intuit Patient List Grid
            private void DesignIntuitPatientListGrid()
            {
                // function which is used design the Intuit Patient List Grid
                try
                {
                   // c1IntuitPatientList.Clear();
                    c1IntuitPatientList.DataSource = null;
                    c1IntuitPatientList.Clear();

                    // setfont
                    c1IntuitPatientList.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                    c1IntuitPatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                    c1IntuitPatientList.BackColor = Color.White;
                    c1IntuitPatientList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                    c1IntuitPatientList.Cols.Count = COL_COUNT_IntuitPatientListGrid;
                    c1IntuitPatientList.Cols.Fixed = 1;
                    c1IntuitPatientList.Rows.Count = 1;
                    c1IntuitPatientList.Rows.Fixed = 1;

                    // Setting width display for columns
                    c1IntuitPatientList.Cols[COL_IntuitTransactionID].WidthDisplay = 0;
                    c1IntuitPatientList.Cols[COL_IntuitMemberID].WidthDisplay = 0;
                    c1IntuitPatientList.Cols[COL_IntuitPatientName].WidthDisplay = 100;
                    c1IntuitPatientList.Cols[COL_IntuitPatientType].WidthDisplay = 80;
                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.Cols[COL_IntuitPatientImportedDate].WidthDisplay = 75;

                    // set visibility of column
                    c1IntuitPatientList.Cols[COL_IntuitTransactionID].Visible = false;
                    c1IntuitPatientList.Cols[COL_IntuitMemberID].Visible = false;
                    c1IntuitPatientList.Cols[COL_IntuitPatientName].Visible = true;
                    c1IntuitPatientList.Cols[COL_IntuitPatientType].Visible = true;
                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.Cols[COL_IntuitPatientImportedDate].Visible = true;
               
                    ////setting text alignment for a columns
                    c1IntuitPatientList.Cols[COL_IntuitPatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    c1IntuitPatientList.Cols[COL_IntuitPatientType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.Cols[COL_IntuitPatientImportedDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                

                    // set column editing
                    c1IntuitPatientList.Cols[COL_IntuitTransactionID].AllowEditing = false;
                    c1IntuitPatientList.Cols[COL_IntuitMemberID].AllowEditing = false;
                    c1IntuitPatientList.Cols[COL_IntuitPatientName].AllowEditing = false;
                    c1IntuitPatientList.Cols[COL_IntuitPatientType].AllowEditing = false;
                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.Cols[COL_IntuitPatientImportedDate].AllowEditing = false;
              
                    //set Heading
                    c1IntuitPatientList.SetData(0, COL_IntuitTransactionID, "Transaction ID");
                    c1IntuitPatientList.SetData(0, COL_IntuitMemberID, "Member ID");
                    c1IntuitPatientList.SetData(0, COL_IntuitPatientName, "Patient Name");
                    c1IntuitPatientList.SetData(0, COL_IntuitPatientType, "Type");
                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.SetData(0, COL_IntuitPatientImportedDate, "Date");
                    
                    c1IntuitPatientList.ExtendLastCol = true;

                    //Task #67686: Date wise data – show date in Left panel – desc order.
                    //Added column to show Date and change column no.
                    c1IntuitPatientList.ScrollBars = ScrollBars.Both;

                }
                catch (Exception ex)
                {                
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Designing Portal Patient List Grid: " + ex.ToString(), false);
                }
            }
            //End of Design Intuit Patient List grid

            //Design Patient details grid
            private void DesignPatientDetailsGrid()
            {
                // function which is used design the Intuit Patient List Grid
                try
                {
                   // c1PatientDetails.Clear();                  
                    c1PatientDetails.DataSource = null;
                    c1PatientDetails.Clear();

                    // set font
                    c1PatientDetails.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                    c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                    c1PatientDetails.BackColor = Color.White;
                    c1PatientDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                    c1PatientDetails.Cols.Count = COL_COUNT_PatientDetailsGrid;
                    c1PatientDetails.Cols.Fixed = 0;
                    c1PatientDetails.Rows.Count = 1;
                    c1PatientDetails.Rows.Fixed = 1;

                    //set Heading                    
                    c1PatientDetails.SetData(0, COL_Select, "Select");
                    c1PatientDetails.SetData(0, COL_FieldName, "Field Name");                
                    c1PatientDetails.SetData(0, COL_IntuitPatient, "Portal Patient");
                    c1PatientDetails.SetData(0, COL_EMRPatient, "gloEMR Patient");

                    //Set Column data type
                    c1PatientDetails.Cols[COL_Select].DataType = typeof(bool);

                    // set column editing
                    c1PatientDetails.Cols[COL_Select].AllowEditing = true;
                    c1PatientDetails.Cols[COL_FieldName].AllowEditing = false;
                    c1PatientDetails.Cols[COL_Field].AllowEditing = false;
                    c1PatientDetails.Cols[COL_IntuitPatient].AllowEditing = true;
                    c1PatientDetails.Cols[COL_EMRPatient].AllowEditing = false;

                    // set visibility of column
                    c1PatientDetails.Cols[COL_Select].Visible = true;
                    c1PatientDetails.Cols[COL_FieldName].Visible = true;
                    c1PatientDetails.Cols[COL_Field].Visible = false;
                    c1PatientDetails.Cols[COL_IntuitPatient].Visible = true;
                    c1PatientDetails.Cols[COL_EMRPatient].Visible = true;

                    // Setting width display for columns
                    c1PatientDetails.Cols[COL_Select].WidthDisplay = 70;
                    c1PatientDetails.Cols[COL_FieldName].WidthDisplay = 110;
                    c1PatientDetails.Cols[COL_Field].WidthDisplay = 0;
                    c1PatientDetails.Cols[COL_IntuitPatient].WidthDisplay = 220;
                    c1PatientDetails.Cols[COL_EMRPatient].WidthDisplay = 220;

                    ////setting text alignment for a columns
                    c1PatientDetails.Cols[COL_Select].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    c1PatientDetails.Cols[COL_FieldName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    c1PatientDetails.Cols[COL_Field].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    c1PatientDetails.Cols[COL_IntuitPatient].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    c1PatientDetails.Cols[COL_EMRPatient].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                  
                    c1PatientDetails.ExtendLastCol = true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Designing Patient Details Grid: " + ex.ToString(), false);
                    ex = null;
                }
            }
            //End of Design Patient details grid                

        #endregion "Grid Design"

        #region "Events"   

            private void frmIntuitPatientReview_Load(object sender, EventArgs e)
            {
                try
                {
                    //Task #67685: Search
                    //Added search logic on PatientName column in c1IntuitPatientList.
                    oTimer.Tick += new EventHandler(oTimer_Tick);

                    FillRace();
                    bIsPatientPortalEnabled = IsPatientPortalEnabled();
                    if (bIsPatientPortalEnabled == true)
                    {
                        FillLanguage();
                        FillEthnicity();
                        getPatientPortalSettings();
                    }
                    FillProvider(); //Fill provider once when form load 
                    LoadData();                    
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while loading Portal patient review form: " + ex.ToString(), false);
                    ex = null;
                }
                finally
                {
                    _IsDataLoaded = true;
                }
            }

            private void ts_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {
                try
                {
                    if (e.ClickedItem.Name == tlbbtn_MatchPatient.Name || e.ClickedItem.Name == tlbbtn_AcceptPatient.Name || 
                        e.ClickedItem.Name == tlbbtn_RejectPatient.Name || e.ClickedItem.Name == tlbbtn_UnmatchPatient.Name )
                    {
                        //checking patient is available and selected before processing it
                        if (c1IntuitPatientList.Rows.Count < 2)
                        {
                            MessageBox.Show("No patient available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (c1IntuitPatientList.RowSel < 1)
                        {
                            MessageBox.Show("Select a patient from Portal Patient List.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (e.ClickedItem.Tag != null)
                    {                    
                        switch (e.ClickedItem.Tag.ToString().ToUpper())
                        {
                            case "MATCHPATIENT":
                                    c1PatientDetails.FinishEditing();
                                    MatchPatient();
                                    break;
                            case "NEW&ACCEPT":
                                    c1PatientDetails.FinishEditing();
                                    AcceptPatient();
                                    break;
                            case "REJECT":
                                    if (MessageBox.Show("Are you sure to reject this patient data?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        long nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                                        long nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));
                                        RejectPatient(nTransactionID, nMemberID, clsGeneral.IntuitPatientProcessedAction.RejectdPatient);
                                    }
                                    break;
                            case "UNMATCH":
                                    ClearEMRPatientData();
                                    break;
                            case "REFRESH":
                                
                                    long nLastTransactionID = 0;
                                    // stored last selected Intuit patient  
                                    if (c1IntuitPatientList.Rows.Count > 1)
                                    {
                                        nLastTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                                    }
                                
                                    //reload data
                                    LoadData();
                                 
                                    //reselect last patient which was selected before hit refresh
                                    Int32 nFoundRow = 0;
                                    if (c1IntuitPatientList.Rows.Count > 1)
                                    {
                                        nFoundRow = c1IntuitPatientList.FindRow(Convert.ToString(nLastTransactionID), 1, COL_IntuitTransactionID,false,true,true);
                                        if (nFoundRow > 1)
                                        {
                                            c1IntuitPatientList.Select(nFoundRow, COL_IntuitPatientName);
                                        }
                                    }                                
                                    break;
                            case "CLOSE":
                                    this.Close();
                                    break;
                            default:
                                    break;
                        }
                }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred on menu click item " + e.ClickedItem.Text + ": " + ex.ToString(), false);
                    ex = null;
                }

            }
    
            private void btnDeleteProvider_Click(object sender, EventArgs e)
             {
                 try
                 {
                     if (cmbProvider.Items.Count > 0)
                     {
                         cmbProvider.SelectedIndex = 0;                         
                     }
                 }
                 catch //(Exception ex)
                 {
                     //ex = null;   
                 }                                
             }

            private void c1IntuitPatientList_RowColChange(object sender, EventArgs e)
            {
                try
                {
                    if (_IsDataLoaded)
                    {                       
                        if (c1IntuitPatientList.Rows.Count < 2)
                        {
                            MessageBox.Show("No patient available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (c1IntuitPatientList.RowSel < 1)
                        {
                            MessageBox.Show("Select a patient from Portal Patient List.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        long nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                        long nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));
                        LoadIntuitPatientDetails(nTransactionID, nMemberID);
                    }
                }
                catch //(Exception ex)
                {
                   // ex = null;
                }
            }                       

            private void chkSelectAll_Click(object sender, EventArgs e)
            {
                try
                {
                    if (c1PatientDetails.Rows.Count > 1)
                    {
                        if (chkSelectAll.Checked)
                        {
                            for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                            {
                                c1PatientDetails.SetData(rCnt, COL_Select, true);
                            }
                        }
                        else
                        {
                            for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                            {
                                c1PatientDetails.SetData(rCnt, COL_Select, false);
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                   // ex = null;
                }
            }

            private void rdbtnMatched_CheckedChanged(object sender, EventArgs e)
             {
                 try
                 {                    
                      LoadData();
                 }
                 catch //(Exception ex)
                 {
                     //ex = null;      
                 }
             }

            private void c1PatientDetails_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
             {
                 try
                 {
                     if (e.Row > 0 )
                     {
                         if (e.Col == COL_IntuitPatient)
                         {
                             _IsGridIntuitValueEdited = true;

                             switch(Convert.ToString(c1PatientDetails.GetData(e.Row, COL_Field)).ToLower())
                             {
                                 case "sgender":
                                      
                                        C1.Win.C1FlexGrid.CellStyle _cstyleGender;
                                         //_cstyleGender = c1PatientDetails.Styles.Add("GenderRed");
                                         try
                                         {
                                             if (c1PatientDetails.Styles.Contains("GenderRed"))
                                             {
                                                 _cstyleGender = c1PatientDetails.Styles["GenderRed"];
                                             }
                                             else
                                             {
                                                 _cstyleGender = c1PatientDetails.Styles.Add("GenderRed");

                                             }

                                         }
                                         catch
                                         {
                                             _cstyleGender = c1PatientDetails.Styles.Add("GenderRed");

                                         }
                                        string sGender = string.Empty;
                                        sGender = c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower();
                                        if (string.Compare(sGender, "female", true) == 0 || string.Compare(sGender, "male", true) == 0 ||
                                            string.Compare(sGender, "other", true) == 0 || sGender == "")
                                        {
                                            _cstyleGender.ForeColor = c1PatientDetails.ForeColor;    
                                        }
                                        else
                                        {
                                            _cstyleGender.ForeColor = Color.Red;
                                        }
                                        c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleGender); 
                                     break;
                                 case "smaritalstatus":
                                       
                                        C1.Win.C1FlexGrid.CellStyle _cstyleMarital;
                                       // _cstyleMarital = c1PatientDetails.Styles.Add("MaritalRed");
                                        try
                                        {
                                            if (c1PatientDetails.Styles.Contains("MaritalRed"))
                                            {
                                                _cstyleMarital = c1PatientDetails.Styles["MaritalRed"];
                                            }
                                            else
                                            {
                                                _cstyleMarital = c1PatientDetails.Styles.Add("MaritalRed");

                                            }

                                        }
                                        catch
                                        {
                                            _cstyleMarital = c1PatientDetails.Styles.Add("MaritalRed");

                                        }
                                        string sMaritalStatus = string.Empty;
                                        sMaritalStatus = c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower();
                                        if (string.Compare(sMaritalStatus, "unmarried", true) == 0 || string.Compare(sMaritalStatus, "married", true) == 0 ||
                                            string.Compare(sMaritalStatus, "single", true) == 0 || string.Compare(sMaritalStatus, "widowed", true) == 0 ||
                                            string.Compare(sMaritalStatus, "divorced", true) == 0 || sMaritalStatus=="")
                                        {
                                            _cstyleMarital.ForeColor = c1PatientDetails.ForeColor;    
                                        }
                                        else
                                        {
                                            _cstyleMarital.ForeColor = Color.Red;
                                        }
                                        c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleMarital); 
                                     break;
                                 case "srace":
                                        
                                       C1.Win.C1FlexGrid.CellStyle _cstyleRace;
                                       // _cstyleRace = c1PatientDetails.Styles.Add("RaceRed");
                                        try
                                        {
                                            if (c1PatientDetails.Styles.Contains("RaceRed"))
                                            {
                                                _cstyleRace = c1PatientDetails.Styles["RaceRed"];
                                            }
                                            else
                                            {
                                                _cstyleRace = c1PatientDetails.Styles.Add("RaceRed");

                                            }

                                        }
                                        catch
                                        {
                                            _cstyleRace = c1PatientDetails.Styles.Add("RaceRed");

                                        }
                                        string [] aRace = _RaceList.Split('|');

                                        IEnumerable<string> result = from r in aRace
                                                                     where r.Trim().ToLower() ==
                                                                         c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower()
                                                                     select r;                                       
                                        if (result.Count() < 1)
                                        {
                                            _cstyleRace.ForeColor = Color.Red;
                                        }
                                        else
                                        {
                                            _cstyleRace.ForeColor = c1PatientDetails.ForeColor;
                                        }
                                        c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleRace);
                                     break;

                                 case "slang":

                                     C1.Win.C1FlexGrid.CellStyle _cstyleLang;
                                     //_cstyleLang = c1PatientDetails.Styles.Add("LangRed");
                                     try
                                     {
                                         if (c1PatientDetails.Styles.Contains("LangRed"))
                                         {
                                             _cstyleLang = c1PatientDetails.Styles["LangRed"];
                                         }
                                         else
                                         {
                                             _cstyleLang = c1PatientDetails.Styles.Add("LangRed");

                                         }

                                     }
                                     catch
                                     {
                                         _cstyleLang = c1PatientDetails.Styles.Add("LangRed");

                                     }
                                     string[] aLang = _LanguageList.Split('|');

                                     IEnumerable<string> resultdata = from r in aLang
                                                                  where r.Trim().ToLower() ==
                                                                      c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower()
                                                                  select r;
                                     if (resultdata.Count() < 1)
                                     {
                                         _cstyleLang.ForeColor = Color.Red;
                                     }
                                     else
                                     {
                                         _cstyleLang.ForeColor = c1PatientDetails.ForeColor;
                                     }
                                     c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleLang);
                                     break;

                                 case "sethn":

                                     C1.Win.C1FlexGrid.CellStyle _cstyleethn;
                                   //  _cstyleethn = c1PatientDetails.Styles.Add("EthnRed");
                                     try
                                     {
                                         if (c1PatientDetails.Styles.Contains("EthnRed"))
                                         {
                                             _cstyleethn = c1PatientDetails.Styles["EthnRed"];
                                         }
                                         else
                                         {
                                             _cstyleethn = c1PatientDetails.Styles.Add("EthnRed");

                                         }

                                     }
                                     catch
                                     {
                                         _cstyleethn = c1PatientDetails.Styles.Add("EthnRed");

                                     }
                                     string[] aEthn = _EthnicityList.Split('|');

                                     IEnumerable<string> resultEhtn = from r in aEthn
                                                                      where r.Trim().ToLower() ==
                                                                          c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower()
                                                                      select r;
                                     if (resultEhtn.Count() < 1)
                                     {
                                         _cstyleethn.ForeColor = Color.Red;
                                     }
                                     else
                                     {
                                         _cstyleethn.ForeColor = c1PatientDetails.ForeColor;
                                     }
                                     c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleethn);
                                     break;



                                 case "scountry":                                        
                                       
                                        C1.Win.C1FlexGrid.CellStyle _cstyleCountry;
                                       // _cstyleCountry = c1PatientDetails.Styles.Add("CountryRed");
                                        try
                                        {
                                            if (c1PatientDetails.Styles.Contains("CountryRed"))
                                            {
                                                _cstyleCountry = c1PatientDetails.Styles["CountryRed"];
                                            }
                                            else
                                            {
                                                _cstyleCountry = c1PatientDetails.Styles.Add("CountryRed");

                                            }

                                        }
                                        catch
                                        {
                                            _cstyleCountry = c1PatientDetails.Styles.Add("CountryRed");

                                        }
                                         string sCountry = string.Empty;
                                         sCountry = c1PatientDetails.GetData(e.Row, COL_IntuitPatient).ToString().Trim().ToLower();

                                         if (sCountry == "us" || sCountry == "canada" || sCountry == "")
                                        {
                                            _cstyleCountry.ForeColor = c1PatientDetails.ForeColor;                                            
                                        }
                                        else
                                        {
                                            _cstyleCountry.ForeColor = Color.Red;
                                        }
                                         c1PatientDetails.SetCellStyle(e.Row, COL_FieldName, _cstyleCountry); 
                                     break;                                                                         
                             }
                         }
                         else if (e.Col == COL_Select)
                         {
                             if (Convert.ToBoolean(c1PatientDetails.GetData(e.Row, COL_Select)) == false && chkSelectAll.Checked == true)
                             {
                                 chkSelectAll.Checked = false;
                             }
                             else if (Convert.ToBoolean(c1PatientDetails.GetData(e.Row, COL_Select)) == true && chkSelectAll.Checked == false)
                             {
                                 Boolean bIsAllChecked = true;
                                 for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                                 {
                                     if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)) == false)
                                     {
                                         bIsAllChecked = false;
                                         break;
                                     }
                                 }

                                 if (bIsAllChecked)
                                 {
                                     chkSelectAll.Checked = true;
                                 }
                             }
                         }
                     }
                 }
                 catch //(Exception ex)
                 {
                     //ex = null;
                 }
             }

            private void c1IntuitPatientList_KeyDown(object sender, KeyEventArgs e)
             {
                 if (c1IntuitPatientList.Rows.Count > 1)
                 {
                     if (_IsGridIntuitValueEdited)
                     {
                         if (MessageBox.Show("You have unsaved changes to this patient. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                         {
                             e.SuppressKeyPress = true;
                             return;
                         }
                     }
                 }
             }

            private void c1IntuitPatientList_BeforeMouseDown(object sender, C1.Win.C1FlexGrid.BeforeMouseDownEventArgs e)
             {
                 //if (c1IntuitPatientList.Rows.Count > 1)
                 //{
                 //    if (e.Button == System.Windows.Forms.MouseButtons.Left && _IsGridIntuitValueEdited)
                 //    {
                 //        if (MessageBox.Show("You have unsaved changes to this patient. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                 //        {
                 //            e.Cancel = true;
                 //            return;
                 //        }
                 //    }
                 //}                
             }

        #endregion   "Events"

        #region "Methods"

             private Boolean LoadData()
             {
                 Boolean bIsloaded = false;
                 try
                 {
                     _IsDataLoaded = false;

                     //Task #67685: Search
                     //Added search logic on PatientName column in c1IntuitPatientList.
                     if (txtSearch.Text.ToString()!="")
                     {  txtSearch.Text = "";    }

                     //Designing grid
                     DesignIntuitPatientListGrid();
                     DesignPatientDetailsGrid();

                     //filling Intuit patient list
                     DataTable dtInuitPatients = null;
                     dtInuitPatients = FillIntuitPatientListGrid();
                     if (dtInuitPatients != null)
                     {
                         dtInuitPatients.Dispose();
                         dtInuitPatients = null;
                     }

                     if (c1IntuitPatientList.Rows.Count > 1)
                     {
                         //select top 1 patient from Intuit Patient List
                         c1IntuitPatientList.Select(1, 0);
                         long nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                         long nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));

                         //Loading Details of Intuit Patient List
                         LoadIntuitPatientDetails(nTransactionID, nMemberID);
                     }

                     bIsloaded = true;
                     return bIsloaded;
                 }
                 catch //(Exception ex)
                 {
                     return bIsloaded;
                 }
                 finally
                 {
                     _IsDataLoaded = true;
                 }
             }

             private DataTable FillIntuitPatientListGrid()
             {
                 DataTable dtInuitPatients = null;
                 gloDatabaseLayer.DBLayer oDB = null;
                 try
                 {
                     DesignIntuitPatientListGrid();
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     oDB.Retrive("gsp_CF_ListUnProcessedPatients", out dtInuitPatients);
                     oDB.Disconnect();

                     string sFilterString = string.Empty;
                     
                     Boolean bIsAll = true;
                     if (rdbtnAll.Checked)
                     {
                         bIsAll = true;
                     }
                     else
                     {
                         bIsAll = false;
                     }

                     if (rdbtnMatched.Checked)
                     {
                         sFilterString = "matched";
                     }
                     else if (rdbtnUnMatched.Checked)
                     {
                         sFilterString = "unmatched";
                     }


                     for (int iRow = 0; iRow <= dtInuitPatients.Rows.Count - 1; iRow++)
                     {
                         // Loop for setting data into grid 
                         if (bIsAll || dtInuitPatients.Rows[iRow]["PatientType"].ToString().ToLower() == sFilterString)
                         {
                             c1IntuitPatientList.Rows.Add();
                             Int32 _Row = c1IntuitPatientList.Rows.Count - 1;
                             c1IntuitPatientList.SetData(_Row, COL_IntuitTransactionID, dtInuitPatients.Rows[iRow]["nTransactionID"].ToString());
                             c1IntuitPatientList.SetData(_Row, COL_IntuitMemberID, dtInuitPatients.Rows[iRow]["nMemberID"].ToString());
                             c1IntuitPatientList.SetData(_Row, COL_IntuitPatientName, dtInuitPatients.Rows[iRow]["PatientName"].ToString());
                             c1IntuitPatientList.SetData(_Row, COL_IntuitPatientType, dtInuitPatients.Rows[iRow]["PatientType"].ToString());
                             //Task #67686: Date wise data – show date in Left panel – desc order.
                             //Added column to show Date and change column no.
                             c1IntuitPatientList.SetData(_Row, COL_IntuitPatientImportedDate,Convert.ToDateTime(dtInuitPatients.Rows[iRow]["Date"]).ToString("MM/dd/yyyy"));
                         }
                     }
                    
                     return dtInuitPatients;
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving Portal patient list: " + ex.ToString(), false);
                     ex = null;
                     return dtInuitPatients;
                 }
                 finally
                 {
                     if (dtInuitPatients != null)
                     {
                         dtInuitPatients.Dispose();
                         dtInuitPatients = null;
                     }
                 }
             }

             private Boolean LoadIntuitPatientDetails(long nTransactionID, long nMemberID)
             {
                 Boolean bReturnValue = false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 gloDatabaseLayer.DBParameters oDBParams = null;
                 DataTable dtPatientRepositoryDetails = null;
                 DataTable dtPatientEMRDetails = null;
             //    DataTable dtFinalData = null;
                 try
                 {
                     //resetting select all check box
                     _IsGridIntuitValueEdited = false;
                     chkSelectAll.Checked = false;
                     
                     List<string> lFields;
                     //Creating Table for fixed columns names list

                     if (bIsPatientPortalEnabled == false)
                     {
                         lFields = new List<string> { "slastname", "sfirstname", "smiddlename", "dtdob", "sgender", "smaritalstatus",
                                    "srace", "saddressline1", "saddressline2", "scity", "sstate", "szip", "scountry", "sphone","smobile", "semail","sworkphone" };
                     }
                     else
                     {
                         lFields = new List<string> { "slastname", "sfirstname", "smiddlename", "dtdob", "sgender", "smaritalstatus",
                                    "srace", "saddressline1", "saddressline2", "scity", "sstate", "szip", "scountry", "sphone","smobile", "semail","sworkphone","slang","sethn" };
                     }
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDBParams = new gloDatabaseLayer.DBParameters();
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt));
                     oDB.Connect(false);
                     oDB.Retrive("gsp_CFGetPatientRepositoryDetails", oDBParams, out dtPatientRepositoryDetails);

                     oDBParams.Clear();
                     oDBParams = new gloDatabaseLayer.DBParameters();
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@nMemberID", nMemberID, ParameterDirection.Input, SqlDbType.BigInt));
                     oDB.Retrive("gsp_CFGetPatientEMRDetails", oDBParams, out dtPatientEMRDetails);
                     oDB.Disconnect();

                     if (dtPatientEMRDetails != null && dtPatientEMRDetails.Rows.Count == 1)
                     {
                         _nMatchedPatientID = Convert.ToInt64(dtPatientEMRDetails.Rows[0]["npatientid"]);

                         _nMatchedPatientProviderID = Convert.ToInt64(dtPatientEMRDetails.Rows[0]["nProviderID"]);
                         if (cmbProvider.Items.Count > 0)
                         {
                             cmbProvider.SelectedValue = _nMatchedPatientProviderID;
                         }

                         DataRow rowSchema = dtPatientRepositoryDetails.NewRow();
                         DataTable dtPatientEMRDetailsClone = dtPatientRepositoryDetails.Clone();

                         var FieldList = from r in dtPatientRepositoryDetails.AsEnumerable()
                                         select new { Name = r.Field<string>("FieldName") };

                         foreach (var m in FieldList)
                         {
                             rowSchema = dtPatientEMRDetailsClone.NewRow();
                             rowSchema["FieldName"] = m.Name;
                             if (string.Compare(m.Name, "dtdob", true) == 0)
                             {
                                 object oDOB = dtPatientEMRDetails.Rows[0][m.Name];
                                 DateTime dtDOB;
                                 DateTime.TryParse(oDOB.ToString(), out dtDOB);
                                 rowSchema["FieldValue"] = dtDOB.ToString("MM/dd/yyyy");
                             }
                             else
                             {
                                 rowSchema["FieldValue"] = Convert.ToString(dtPatientEMRDetails.Rows[0][m.Name]);
                             }
                             dtPatientEMRDetailsClone.Rows.Add(rowSchema);
                             rowSchema = null;
                         }

                         var query = from r in dtPatientRepositoryDetails.AsEnumerable()
                                     join c in dtPatientEMRDetailsClone.AsEnumerable()
                                         on r.Field<string>("FieldName") equals c.Field<string>("FieldName")
                                     select new
                                     {
                                         Select = false,
                                         FieldName = r.Field<string>("FieldName"),
                                         IntuitValue = r.Field<string>("FieldValue"),
                                         EMRValue = c.Field<string>("FieldValue")
                                     };

                         dtPatientEMRDetailsClone.Dispose();
                         dtPatientEMRDetailsClone = null;

                         DataTable dtPatient = dtPatientRepositoryDetails.Clone();
                         DataColumn dtCol = new DataColumn("Select");
                         dtCol.DataType = System.Type.GetType("System.Boolean");
                         dtPatient.Columns.Add(dtCol);

                         dtCol = new DataColumn("EMRValue");
                         dtCol.DataType = System.Type.GetType("System.String");
                         dtPatient.Columns.Add(dtCol);

                         //dtCol.Dispose();
                         //dtCol = null;

                         foreach (var q in query)
                         {
                             rowSchema = dtPatient.NewRow();
                             rowSchema["FieldName"] = q.FieldName;
                             rowSchema["FieldValue"] = q.IntuitValue;
                             rowSchema["Select"] = q.Select;
                             rowSchema["EMRValue"] = q.EMRValue;
                             dtPatient.Rows.Add(rowSchema);
                             rowSchema = null;
                         }

                         DesignPatientDetailsGrid();

                         foreach (string sField in lFields)
                         {
                             c1PatientDetails.Rows.Add();
                             Int32 _Row = c1PatientDetails.Rows.Count - 1;

                             c1PatientDetails.SetCellStyle(_Row, COL_Select, c1PatientDetails.Styles["CS_CheckBox"]);
                             c1PatientDetails.SetData(_Row, COL_Field, sField);
                             c1PatientDetails.SetData(_Row, COL_FieldName, GetFieldCaption(sField));

                             var sResult = from r in dtPatient.AsEnumerable()
                                           where r.Field<string>("FieldName").ToLower() == sField
                                           select new
                                               {
                                                   FieldValue = r.Field<string>("FieldValue").Trim(),
                                                   EMRValue = r.Field<string>("EMRValue").Trim()
                                               };

                             string sFieldValue = string.Empty;
                             string sEMRValue = string.Empty;
                             if (sResult.Count() > 0)
                             {
                                 sFieldValue = sResult.First().FieldValue;
                                 sEMRValue = sResult.First().EMRValue;
                             }

                             if (sFieldValue.Length > 0 && string.Compare(sFieldValue, sEMRValue, true) != 0)
                             {
                                 c1PatientDetails.SetData(_Row, COL_Select, true);
                             }
                             else
                             {
                                 c1PatientDetails.SetData(_Row, COL_Select, false);
                             }

                             if (string.Compare(sField, "dtdob", true) == 0 && sFieldValue.Length > 0)
                             {
                                 object oDOB = sFieldValue;
                                 DateTime dtDOB;
                                 DateTime.TryParse(oDOB.ToString(), out dtDOB);
                                 c1PatientDetails.SetData(_Row, COL_IntuitPatient, dtDOB.ToString("MM/dd/yyyy"));

                                 if (string.Compare(dtDOB.ToString("MM/dd/yyyy"), sEMRValue, true) != 0)
                                 {
                                     c1PatientDetails.SetData(_Row, COL_Select, true);
                                 }
                                 else
                                 {
                                     c1PatientDetails.SetData(_Row, COL_Select, false);
                                 }
                             }
                             else if (string.Compare(sField, "sCountry", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("CountryRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("CountryRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["CountryRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("CountryRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("CountryRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aCountry = _CountryList.Split('|');

                                 IEnumerable<string> result = from c in aCountry where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                 string sCountryList = "";
                                 if (result.Count() > 0)
                                 {
                                     sCountryList = _CountryList;
                                 }
                                 else
                                 {
                                     sCountryList = _CountryList + "|" + sFieldValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sCountryList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }
                             else if (string.Compare(sField, "sGender", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("GenderRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("GenderRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["GenderRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("GenderRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("GenderRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aGender = _GenderList.Split('|');

                                 IEnumerable<string> result = from c in aGender where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                 string sGenderList = "";
                                 if (result.Count() > 0)
                                 {
                                     sGenderList = _GenderList;
                                 }
                                 else
                                 {
                                     sGenderList = _GenderList + "|" + sFieldValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sGenderList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }
                             else if (string.Compare(sField, "smaritalstatus", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("MaritalRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("MaritalRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["MaritalRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("MaritalRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("MaritalRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aMaritalStatus = _MaritalStatusList.Split('|');

                                 IEnumerable<string> result = from c in aMaritalStatus where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                 string sMaritalStatusList = "";
                                 if (result.Count() > 0)
                                 {
                                     sMaritalStatusList = _MaritalStatusList;
                                 }
                                 else
                                 {
                                     sMaritalStatusList = _MaritalStatusList + "|" + sFieldValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sMaritalStatusList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }
                             else if (string.Compare(sField, "srace", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("RaceRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("RaceRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["RaceRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("RaceRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("RaceRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;
                                string[] aRace = _RaceList.Split('|');
                                string sRaceList = "";

                                if (bIsPatientPortalEnabled == true)
                                {
                                    string strRace = Convert.ToString(sFieldValue).Trim();
                                    if (strRace != "" && Convert.ToString(strRace).Contains("|"))
                                    {
                                        string[] selectedRace = strRace.Split('|');
                                        if (selectedRace.Length > 0)
                                        {
                                            int foundcnt = 0;
                                            for (int i = 0; i < selectedRace.Length; i++)
                                            {
                                                for (int j = 0; j < aRace.Length; j++)
                                                {
                                                    if (Convert.ToString(selectedRace[i]).Trim().ToLower() == Convert.ToString(aRace[j]).Trim().ToLower())
                                                    {
                                                        foundcnt += 1;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (foundcnt == selectedRace.Length)
                                            {
                                                sRaceList = _RaceList;
                                            }
                                            else
                                            {
                                                sRaceList = _RaceList + "|" + sFieldValue;
                                                _cstyle.ForeColor = Color.Red;
                                            }
                                        }
                                    }
                                    else if (strRace != "" && strRace.Length > 0)
                                    {
                                        bool IsFound = false;
                                        for (int i = 0; i < aRace.Length; i++)
                                        {
                                            if (strRace.ToLower() == Convert.ToString(aRace[i]).Trim().ToLower())
                                            {
                                                IsFound = true;
                                                break;
                                            }
                                        }
                                        if (IsFound == true)
                                        {
                                            sRaceList = _RaceList;
                                        }
                                        else
                                        {
                                            sRaceList = _RaceList + "|" + sFieldValue;
                                            _cstyle.ForeColor = Color.Red;
                                        }
                                    }
                                    else if(strRace == "")
                                    {
                                        sRaceList = _RaceList;
                                    }
                                }
                                else
                                { 
                                    IEnumerable<string> result = from c in aRace where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                     if (result.Count() > 0)
                                     {
                                         sRaceList = _RaceList;
                                     }
                                     else
                                     {
                                         sRaceList = _RaceList + "|" + sFieldValue;
                                         _cstyle.ForeColor = Color.Red;
                                     }
                                }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sRaceList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }
                             else if (string.Compare(sField, "slang", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                               //  _cstyle = c1PatientDetails.Styles.Add("LangRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("LangRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["LangRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("LangRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("LangRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aLang = _LanguageList.Split('|');

                                 IEnumerable<string> result = from c in aLang where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                 string sLangList = "";
                                 if (result.Count() > 0)
                                 {
                                     sLangList = _LanguageList;
                                 }
                                 else
                                 {
                                     sLangList = _LanguageList + "|" + sFieldValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sLangList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }
                             else if (string.Compare(sField, "sethn", true) == 0)
                             {
                               //  C1.Win.C1FlexGrid.CellStyle _cstyle;
                               // // _cstyle = c1PatientDetails.Styles.Add("EthnRed");
                               //  try
                               //  {
                               //      if (c1PatientDetails.Styles.Contains("EthnRed"))
                               //      {
                               //          _cstyle = c1PatientDetails.Styles["EthnRed"];
                               //      }
                               //      else
                               //      {
                               //          _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                               //      }

                               //  }
                               //  catch
                               //  {
                               //      _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                               //  }
                               //  _cstyle.ForeColor = c1PatientDetails.ForeColor;

                               //  string[] aEhtn = _EthnicityList.Split('|');

                               //  IEnumerable<string> result = from c in aEhtn where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                               //  string sEthnList = "";
                               //  if (result.Count() > 0)
                               //  {
                               //      sEthnList = _EthnicityList;
                               //  }
                               //  else
                               //  {
                               //      sEthnList = _EthnicityList + "|" + sFieldValue;
                               //      _cstyle.ForeColor = Color.Red;
                               //  }

                               //  c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                               //  C1.Win.C1FlexGrid.CellStyle cstyle;
                               //  C1.Win.C1FlexGrid.CellRange ocell;

                               ////  cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                               //  string mystring = "BubbleValues" + _Row.ToString();
                               //  //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                               //  try
                               //  {
                               //      if (c1PatientDetails.Styles.Contains(mystring))
                               //      {
                               //          cstyle = c1PatientDetails.Styles[mystring];
                               //      }
                               //      else
                               //      {
                               //          cstyle = c1PatientDetails.Styles.Add(mystring);

                               //      }

                               //  }
                               //  catch
                               //  {
                               //      cstyle = c1PatientDetails.Styles.Add(mystring);

                               //  }
                               //  cstyle.ComboList = sEthnList;
                               //  ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                               //  ocell.Style = cstyle;
                               //  ocell.Data = sFieldValue;
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                 // _cstyle = c1PatientDetails.Styles.Add("RaceRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("EthnRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["EthnRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;
                                 string[] aEhtn = _EthnicityList.Split('|');
                                 string sEthnicityList = "";

                                 if (bIsPatientPortalEnabled == true)
                                 {
                                     string strRace = Convert.ToString(sFieldValue).Trim();
                                     if (strRace != "" && Convert.ToString(strRace).Contains("|"))
                                     {
                                         string[] selectedRace = strRace.Split('|');
                                         if (selectedRace.Length > 0)
                                         {
                                             int foundcnt = 0;
                                             for (int i = 0; i < selectedRace.Length; i++)
                                             {
                                                 for (int j = 0; j < aEhtn.Length; j++)
                                                 {
                                                     if (Convert.ToString(selectedRace[i]).Trim().ToLower() == Convert.ToString(aEhtn[j]).Trim().ToLower())
                                                     {
                                                         foundcnt += 1;
                                                         break;
                                                     }
                                                 }
                                             }
                                             if (foundcnt == selectedRace.Length)
                                             {
                                                 sEthnicityList = _RaceList;
                                             }
                                             else
                                             {
                                                 sEthnicityList = _RaceList + "|" + sFieldValue;
                                                 _cstyle.ForeColor = Color.Red;
                                             }
                                         }
                                     }
                                     else if (strRace != "" && strRace.Length > 0)
                                     {
                                         bool IsFound = false;
                                         for (int i = 0; i < aEhtn.Length; i++)
                                         {
                                             if (strRace.ToLower() == Convert.ToString(aEhtn[i]).Trim().ToLower())
                                             {
                                                 IsFound = true;
                                                 break;
                                             }
                                         }
                                         if (IsFound == true)
                                         {
                                             sEthnicityList = _EthnicityList;
                                         }
                                         else
                                         {
                                             sEthnicityList = _EthnicityList + "|" + sFieldValue;
                                             _cstyle.ForeColor = Color.Red;
                                         }
                                     }
                                     else if (strRace == "")
                                     {
                                         sEthnicityList = _EthnicityList;
                                     }
                                 }
                                 else
                                 {
                                     IEnumerable<string> result = from c in aEhtn where c.Trim().ToLower() == sFieldValue.ToLower() select c;

                                     if (result.Count() > 0)
                                     {
                                         sEthnicityList = _EthnicityList;
                                     }
                                     else
                                     {
                                         sEthnicityList = _EthnicityList + "|" + sFieldValue;
                                         _cstyle.ForeColor = Color.Red;
                                     }
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                 // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sEthnicityList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sFieldValue;
                             }

                             else
                             {
                                 c1PatientDetails.SetData(_Row, COL_IntuitPatient, sFieldValue);
                             }

                             c1PatientDetails.SetData(_Row, COL_EMRPatient, sEMRValue);

                             sResult = null;
                             sFieldValue = null;
                             sEMRValue = null;
                         }
                         if (dtPatient != null)
                         {
                             dtPatient.Dispose();
                             dtPatient = null;
                         }

                         if (c1PatientDetails.Rows.Count > 1)
                         {
                             Boolean bIsAnyUncheck = false;
                             for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                             {
                                 if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)) == false)
                                 {
                                     bIsAnyUncheck = true;
                                     break;
                                 }
                             }

                             if (bIsAnyUncheck)
                             {
                                 chkSelectAll.Checked = false;
                             }
                             else
                             {
                                 chkSelectAll.Checked = true;
                             }
                         }
                     }
                     else
                     {
                         DesignPatientDetailsGrid();

                         //resetting provider
                         if (cmbProvider.Items.Count > 0)
                         {
                             cmbProvider.SelectedIndex = 0;
                         }

                         _nMatchedPatientID = 0;
                         _nMatchedPatientProviderID = 0;

                         foreach (string sField in lFields)
                         {
                             c1PatientDetails.Rows.Add();
                             Int32 _Row = c1PatientDetails.Rows.Count - 1;

                             c1PatientDetails.SetCellStyle(_Row, COL_Select, c1PatientDetails.Styles["CS_CheckBox"]);
                             c1PatientDetails.SetData(_Row, COL_Field, sField);
                             c1PatientDetails.SetData(_Row, COL_FieldName, GetFieldCaption(sField));
                             c1PatientDetails.SetData(_Row, COL_EMRPatient, string.Empty);

                             IEnumerable<string> sResult = from r in dtPatientRepositoryDetails.AsEnumerable()
                                                           where r.Field<string>("FieldName").ToLower() == sField
                                                           select r.Field<string>("FieldValue").Trim();

                             string sValue = string.Empty;
                             if (sResult.Count() > 0)
                                 sValue = sResult.First();

                             if (sValue.Length > 0)
                             {   //if Intuit Data is not empty then select check box
                                 c1PatientDetails.SetData(_Row, COL_Select, true);
                             }
                             else
                             {
                                 c1PatientDetails.SetData(_Row, COL_Select, false);
                             }

                             if (string.Compare(sField, "dtdob", true) == 0 && sValue.Length > 0)
                             {
                                 object oDOB = sValue;
                                 DateTime dtDOB;
                                 DateTime.TryParse(oDOB.ToString(), out dtDOB);
                                 c1PatientDetails.SetData(_Row, COL_IntuitPatient, dtDOB.ToString("MM/dd/yyyy"));

                                 oDB = null;
                             }
                             else if (string.Compare(sField, "sCountry", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("CountryRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("CountryRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["CountryRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("CountryRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("CountryRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aCountry = _CountryList.Split('|');

                                 IEnumerable<string> result = from c in aCountry where c.Trim().ToLower() == sValue.ToLower() select c;

                                 string sCountryList = "";
                                 if (result.Count() > 0)
                                 {
                                     sCountryList = _CountryList;
                                 }
                                 else
                                 {
                                     sCountryList = _CountryList + "|" + sValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sCountryList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }
                             else if (string.Compare(sField, "sGender", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("GenderRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("GenderRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["GenderRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("GenderRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("GenderRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aGender = _GenderList.Split('|');

                                 IEnumerable<string> result = from c in aGender where c.Trim().ToLower() == sValue.ToLower() select c;

                                 string sGenderList = "";
                                 if (result.Count() > 0)
                                 {
                                     sGenderList = _GenderList;
                                 }
                                 else
                                 {
                                     sGenderList = _GenderList + "|" + sValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sGenderList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }
                             else if (string.Compare(sField, "smaritalstatus", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                 //_cstyle = c1PatientDetails.Styles.Add("MaritalRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("MaritalRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["MaritalRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("MaritalRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("MaritalRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aMaritalStatus = _MaritalStatusList.Split('|');

                                 IEnumerable<string> result = from c in aMaritalStatus where c.Trim().ToLower() == sValue.ToLower() select c;

                                 string sMaritalStatusList = "";
                                 if (result.Count() > 0)
                                 {
                                     sMaritalStatusList = _MaritalStatusList;
                                 }
                                 else
                                 {
                                     sMaritalStatusList = _MaritalStatusList + "|" + sValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                               //  cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sMaritalStatusList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }
                             else if (string.Compare(sField, "srace", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                                // _cstyle = c1PatientDetails.Styles.Add("RaceRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("RaceRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["RaceRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("RaceRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("RaceRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aRace = _RaceList.Split('|');
                                 string sRaceList = "";

                                 if (bIsPatientPortalEnabled == true)
                                 {
                                     string strRace = Convert.ToString(sValue).Trim();
                                     if (strRace != "" && Convert.ToString(strRace).Contains("|"))
                                     {
                                         string[] selectedRace = strRace.Split('|');
                                         if (selectedRace.Length > 0)
                                         {
                                             int foundcnt = 0;
                                             for (int i = 0; i < selectedRace.Length; i++)
                                             {
                                                 for (int j = 0; j < aRace.Length; j++)
                                                 {
                                                     if (Convert.ToString(selectedRace[i]).Trim().ToLower() == Convert.ToString(aRace[j]).Trim().ToLower())
                                                     {
                                                         foundcnt += 1;
                                                         break;
                                                     }
                                                 }
                                             }
                                             if (foundcnt == selectedRace.Length)
                                             {
                                                 sRaceList = _RaceList;
                                             }
                                             else
                                             {
                                                 sRaceList = _RaceList + "|" + sValue;
                                                 _cstyle.ForeColor = Color.Red;
                                             }
                                         }
                                     }
                                     else if (strRace != "" && strRace.Length > 0)
                                     {
                                         bool IsFound = false;
                                         for (int i = 0; i < aRace.Length; i++)
                                         {
                                             if (strRace.ToLower() == Convert.ToString(aRace[i]).Trim().ToLower())
                                             {
                                                 IsFound = true;
                                                 break;
                                             }
                                         }
                                         if (IsFound == true)
                                         {
                                             sRaceList = _RaceList;
                                         }
                                         else
                                         {
                                             sRaceList = _RaceList + "|" + sValue;
                                             _cstyle.ForeColor = Color.Red;
                                         }
                                     }
                                     else if(strRace == "")
                                     {
                                         sRaceList = _RaceList;
                                     }
                                 }
                                 else
                                 { 
                                     IEnumerable<string> result = from c in aRace where c.Trim().ToLower() == sValue.ToLower() select c;
                                     if (result.Count() > 0)
                                     {
                                         sRaceList = _RaceList;
                                     }
                                     else
                                     {
                                         sRaceList = _RaceList + "|" + sValue;
                                         _cstyle.ForeColor = Color.Red;
                                     }
                                 }
                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sRaceList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }
                             else if (string.Compare(sField, "slang", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                               //  _cstyle = c1PatientDetails.Styles.Add("LangRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("LangRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["LangRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("LangRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("LangRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aLang = _LanguageList.Split('|');

                                 IEnumerable<string> result = from c in aLang where c.Trim().ToLower() == sValue.ToLower() select c;

                                 string slangList = "";
                                 if (result.Count() > 0)
                                 {
                                     slangList = _LanguageList;
                                 }
                                 else
                                 {
                                     slangList = _LanguageList + "|" + sValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                // cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = slangList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }
                             else if (string.Compare(sField, "sethn", true) == 0)
                             {
                                 C1.Win.C1FlexGrid.CellStyle _cstyle;
                               //  _cstyle = c1PatientDetails.Styles.Add("EthnRed");
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains("EthnRed"))
                                     {
                                         _cstyle = c1PatientDetails.Styles["EthnRed"];
                                     }
                                     else
                                     {
                                         _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                                     }

                                 }
                                 catch
                                 {
                                     _cstyle = c1PatientDetails.Styles.Add("EthnRed");

                                 }
                                 _cstyle.ForeColor = c1PatientDetails.ForeColor;

                                 string[] aEthn = _EthnicityList.Split('|');

                                 IEnumerable<string> result = from c in aEthn where c.Trim().ToLower() == sValue.ToLower() select c;

                                 string sEthnList = "";
                                 if (result.Count() > 0)
                                 {
                                     sEthnList = _EthnicityList;
                                 }
                                 else
                                 {
                                     sEthnList = _EthnicityList + "|" + sValue;
                                     _cstyle.ForeColor = Color.Red;
                                 }

                                 c1PatientDetails.SetCellStyle(_Row, COL_FieldName, _cstyle);

                                 C1.Win.C1FlexGrid.CellStyle cstyle;
                                 C1.Win.C1FlexGrid.CellRange ocell;

                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 string mystring = "BubbleValues" + _Row.ToString();
                                 //cstyle = c1PatientDetails.Styles.Add("BubbleValues" + _Row.ToString());
                                 try
                                 {
                                     if (c1PatientDetails.Styles.Contains(mystring))
                                     {
                                         cstyle = c1PatientDetails.Styles[mystring];
                                     }
                                     else
                                     {
                                         cstyle = c1PatientDetails.Styles.Add(mystring);

                                     }

                                 }
                                 catch
                                 {
                                     cstyle = c1PatientDetails.Styles.Add(mystring);

                                 }
                                 cstyle.ComboList = sEthnList;
                                 ocell = c1PatientDetails.GetCellRange(_Row, COL_IntuitPatient, _Row, COL_IntuitPatient);
                                 ocell.Style = cstyle;
                                 ocell.Data = sValue;
                             }

                             else
                             {
                                 c1PatientDetails.SetData(_Row, COL_IntuitPatient, sValue);
                             }

                             sResult = null;
                             sValue = null;
                         }

                         if (c1PatientDetails.Rows.Count > 1)
                         {
                             Boolean bIsAnyUncheck = false;
                             for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                             {
                                 if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)) == false)
                                 {
                                     bIsAnyUncheck = true;
                                     break;
                                 }
                             }

                             if (bIsAnyUncheck)
                             {
                                 chkSelectAll.Checked = false;
                             }
                             else
                             {
                                 chkSelectAll.Checked = true;
                             }
                         }
                     }

                     bReturnValue = true;
                     return bReturnValue;
                 }
                 catch //(Exception ex)
                 {
                    // ex = null;
                     return bReturnValue;
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                     }
                     if (dtPatientRepositoryDetails != null)
                     {
                         dtPatientRepositoryDetails.Dispose();
                         dtPatientRepositoryDetails = null;
                     }
                     if (dtPatientEMRDetails != null)
                     {
                         dtPatientEMRDetails.Dispose();
                         dtPatientEMRDetails = null;
                     }
                 }
             }

             private void MatchPatient()
             {
                 List<clsMatchingParameters> oclsMatchingParameters = null;
                 List<clsMatchingParameters> oMatchedPatientData = null;
                 frmMatchIntuitPatient ofrmMatchIntuitPatient = null;
                 try
                 {
                     oclsMatchingParameters = new List<clsMatchingParameters>();

                     for (int rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                     {
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sFirstName", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sFirstName", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sMiddleName", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sMiddleName", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sLastName", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sLastName", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "dtDOB", true) == 0)
                         {
                             DateTime dtTemp;
                            if (DateTime.TryParse(c1PatientDetails.Rows[rCnt][COL_IntuitPatient].ToString().Trim(), out dtTemp))
                             {
                                 if (!(dtTemp.Date > DateTime.Now.Date || dtTemp.Date < Convert.ToDateTime("01/01/1900").Date))
                                 {
                                     oclsMatchingParameters.Add(new clsMatchingParameters("dtDOB", dtTemp.ToString("MM/dd/yyyy")));
                                 }
                                 else
                                 {
                                     MessageBox.Show("Enter a valid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     c1PatientDetails.Select(rCnt, COL_IntuitPatient);  
                                     return;
                                 }
                             }
                             else
                             {
                                 MessageBox.Show("Enter a valid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                 c1PatientDetails.Select(rCnt, COL_IntuitPatient);  
                                 return;
                             }                            
                         }                         
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sEmail", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sEmail", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sGender", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sGender", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                         if (string.Compare(Convert.ToString(c1PatientDetails.Rows[rCnt][COL_Field]), "sZip", true) == 0)
                         {
                             oclsMatchingParameters.Add(new clsMatchingParameters("sZip", Convert.ToString(c1PatientDetails.Rows[rCnt][COL_IntuitPatient])));
                         }
                     }

                     oMatchedPatientData = new List<clsMatchingParameters>();
                     ofrmMatchIntuitPatient = new frmMatchIntuitPatient(oclsMatchingParameters);
                     ofrmMatchIntuitPatient.ShowDialog(this);
                     oMatchedPatientData = ofrmMatchIntuitPatient.MatchedPatientData;
                     ofrmMatchIntuitPatient.Dispose();
                     ofrmMatchIntuitPatient = null;

                     try
                     {
                         if (oMatchedPatientData != null && oMatchedPatientData.Count > 0)
                         {
                             var result = from v in oMatchedPatientData
                                          where v.FieldName.ToLower() == "npatientid" || v.FieldName.ToLower() == "nproviderid"
                                          select new
                                              {
                                                  FieldName = v.FieldName.ToLower(),
                                                  FieldValue = Convert.ToInt64(v.FieldValue)
                                              };
                             long nTempMatchedID = 0;
                             long nTempPatientProviderID = 0;
                             foreach (var r in result)
                             {
                                 switch (r.FieldName)
                                 {
                                     case "npatientid":
                                         nTempMatchedID = r.FieldValue;
                                         break;
                                     case "nproviderid":
                                         nTempPatientProviderID = r.FieldValue;
                                         break;
                                 }
                             }
                             
                             if (nTempMatchedID > 0)
                             {
                                 _nMatchedPatientID = nTempMatchedID;
                                 _nMatchedPatientProviderID = nTempPatientProviderID;

                                 if (cmbProvider.Items.Count > 0)
                                 {
                                     cmbProvider.SelectedValue = _nMatchedPatientProviderID;
                                 }

                                 for (int rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                                 {
                                     string sFieldName = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_Field)).ToLower();
                                     IEnumerable<string> query = from v in oMatchedPatientData
                                                                 where v.FieldName.ToLower() == sFieldName.ToLower()
                                                                 select v.FieldValue;
                                     string sValue = string.Empty;
                                     if (query.Count() > 0)
                                     {
                                         sValue = query.First();
                                     }
                                     c1PatientDetails.SetData(rCnt, COL_EMRPatient, sValue);
                                     if (c1PatientDetails.GetData(rCnt, COL_IntuitPatient).ToString().Trim().Length > 0 &&
                                         string.Compare(c1PatientDetails.GetData(rCnt, COL_IntuitPatient).ToString().Trim(), sValue.Trim(), true) != 0)
                                     {
                                         c1PatientDetails.SetData(rCnt, COL_Select, true);
                                     }
                                     else
                                     {
                                         c1PatientDetails.SetData(rCnt, COL_Select, false);
                                     }
                                 }

                                 if (c1PatientDetails.Rows.Count > 1)
                                 {
                                     Boolean bIsAnyUncheck = false;
                                     for (Int32 rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                                     {
                                         if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)) == false)
                                         {
                                             bIsAnyUncheck = true;
                                             break;
                                         }
                                     }

                                     if (bIsAnyUncheck)
                                     {
                                         chkSelectAll.Checked = false;
                                     }
                                     else
                                     {
                                         chkSelectAll.Checked = true;
                                     }
                                 }
                             }
                             else
                             {
                                 MessageBox.Show("Patient ID of Matched Patient not found", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             }
                         }                         
                     }
                     catch (Exception ex)
                     {
                         gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while populating matched patient data into grid: " + ex.ToString(), true);
                         ex = null;
                     }                     
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while matching Portal patient with gloEMR patient: " + ex.ToString(), false);
                 }
                 finally
                 {
                     if (ofrmMatchIntuitPatient != null)
                     {
                         ofrmMatchIntuitPatient.Dispose();
                         ofrmMatchIntuitPatient = null;
                     }
                     if (oclsMatchingParameters != null)
                     {
                         oclsMatchingParameters.Clear();
                         oclsMatchingParameters = null;
                     }
                     if (oMatchedPatientData != null)
                     {
                         oMatchedPatientData.Clear();
                         oMatchedPatientData = null;
                     }
                 }
             }  

             private void AcceptPatient()
             {                
                 try
                 {
                     if (c1PatientDetails.Rows.Count < 2)
                     {
                         MessageBox.Show("Patient details are not available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                     }
                     if (_nMatchedPatientID==0 )  //Registered as new patient
                     {  
                         RegisterPatient();
                     }
                     else // Update data for selected EMR patient (Modify Patient)
                     {
                         if (_nMatchedPatientID > 0)
                         {
                             UpdatePatient();
                         }
                         else
                         {
                             MessageBox.Show("No EMR patient found to update data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                     }
                 }
                 catch //(Exception ex)
                 {
                     //ex = null;
                 }
                 finally
                 {
                    
                 }
             }

             private void RegisterPatient()
             {
                 gloPatient.Patient oPatient = null;
                 gloPatient.gloPatient ogloPatient = null;
                 try
                 {
                     //Validating Data
                     if (ValidateData(true))
                         return;

                     if (Convert.ToInt64(cmbProvider.SelectedValue) <= 0)
                     {
                         MessageBox.Show("Please select the provider for this patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         cmbProvider.Focus();                         
                         return;
                     }

                     long nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                     long nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));

                     DataTable dtExistingExternalCode = null;
                     dtExistingExternalCode = ValidatePatientExternalCodes(nMemberID, 0, "register");
                     if (dtExistingExternalCode != null && dtExistingExternalCode.Rows.Count > 0)
                     {
                         if (MessageBox.Show("This Portal Account is mapped to different patient. Do you wish to change this mapping, existing links will be removed?",
                             gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                         {
                             dtExistingExternalCode.Dispose();
                             dtExistingExternalCode = null;
                             return;
                         }
                     }
                     if (dtExistingExternalCode != null)
                     {
                         dtExistingExternalCode.Dispose();
                         dtExistingExternalCode = null;
                     }
                    

                     oPatient = new gloPatient.Patient();
                     ogloPatient = new gloPatient.gloPatient(_dataBaseConnectionString);

                     string sGridValue = string.Empty;

                     for (int rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                     {
                         if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)))
                         {
                             sGridValue = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_IntuitPatient)).Trim();

                             switch (Convert.ToString(c1PatientDetails.GetData(rCnt, COL_Field)).ToLower())
                             {
                                 case "sfirstname":
                                     oPatient.DemographicsDetail.PatientFirstName = sGridValue;
                                     break;
                                 case "smiddlename":
                                     
                                     if(sGridValue.Length>0)
                                     {
                                         oPatient.DemographicsDetail.PatientMiddleName = sGridValue;
                                     }
                                     break;
                                 case "slastname":
                                     oPatient.DemographicsDetail.PatientLastName = sGridValue;
                                     break;
                                 case "dtdob":
                                     DateTime dt;
                                     DateTime.TryParse(sGridValue, out dt);
                                     oPatient.DemographicsDetail.PatientDOB = Convert.ToDateTime(dt.ToString("MM/dd/yyyy"));                                     
                                     break;
                                 case "sgender":
                                     oPatient.DemographicsDetail.PatientGender = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_IntuitPatient));
                                     break;
                                 case "semail":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientEmail = sGridValue;
                                     }
                                     break;                                 
                                 case "smaritalstatus":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientMaritalStatus = sGridValue;
                                     }
                                     break;
                                 case "srace":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientRace = sGridValue;
                                     }
                                     break;                               
                                 case "saddressline1":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientAddress1 = sGridValue;
                                     }
                                     break;
                                 case "saddressline2":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientAddress2 = sGridValue;
                                     }
                                     break;
                                 case "scity":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientCity = sGridValue;
                                     }
                                     break;
                                 case "sstate":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientState = sGridValue;
                                     }
                                     break;
                                 case "szip":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientZip = sGridValue;
                                     }
                                     break;
                                 case "scountry":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientCountry = sGridValue;
                                     }
                                     break;
                                 case "sphone":
                                     if (sGridValue.Length > 0)
                                     {
                                         oPatient.DemographicsDetail.PatientPhone = sGridValue;
                                     }
                                     break;
                                 case "smobile":
                                     if(sGridValue.Length>0)
                                     {
                                         oPatient.DemographicsDetail.PatientMobile = sGridValue;
                                     }
                                     break;
                                 case "sworkphone":
                                     if(sGridValue.Length>0)
                                     {
                                         oPatient.OccupationDetail.PatientWorkPhone = sGridValue;
                                     }
                                     break;
                             }
                         }
                     }

                     sGridValue = null;

                     //Provider code
                     oPatient.DemographicsDetail.PatientProviderID = Convert.ToInt64(cmbProvider.SelectedValue);

                     //Generate patient code.
                     string sPatientCode = ogloPatient.GeneratePatientCode();

                     if (sPatientCode.Length > 0)
                     {
                         oPatient.DemographicsDetail.PatientCode = sPatientCode;
                     }
                     else
                     {
                         MessageBox.Show("Unable to registered the patient, Patient code generation failed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }

                     long nPatientID = 0;
                     try
                     {
                         nPatientID = ogloPatient.Add(oPatient);
                     }
                     catch //(Exception ex)
                     {
                        // ex = null;
                     }
                     
                     if (nPatientID > 0)
                     {
                         //Updating process action
                         UpdateProcessAction(nTransactionID, nMemberID, clsGeneral.IntuitPatientProcessedAction.NewPatient, nPatientID);

                         //saving patient external code
                         nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                         nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));
                         SavePatientExternalCodes(nPatientID, nMemberID.ToString(), "MemberId");
                         if (bIsPatientPortalEnabled)
                         {
                             SendPatientPortalEmails(nPatientID);
                         }
                         //Family account
                         InsertUpdateFamilyAccount(nPatientID);

                         //Insert Message Queue Health forms
                         if (bIntuitEnabled)
                            GenrateMessageQueue_ForCustomForms(nPatientID, nMemberID, nTransactionID.ToString(), "New Patient"); 

                         //MessageBox.Show("Patient registered successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                         long AUditTrailId = 0;
                         AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.SetupPatient, gloAuditTrail.ActivityType.Register, "Register Patient-Portal", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                         gloPatient.clsgloPatientAudit oAudit = new gloPatient.clsgloPatientAudit(_dataBaseConnectionString);
                         oAudit.SavePatientAuditDetails(nPatientID, AUditTrailId, "Register Patient-Portal");
                         oAudit.Dispose();
                         oAudit = null;

                         //reload data
                         LoadData();
                     }
                     else
                     {
                         MessageBox.Show("Failed patient registration.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while registering Portal patient into gloEMR: " + ex.ToString(), true);
                     ex = null;
                 }
                 finally
                 {
                     if (oPatient != null)
                     {
                         oPatient.Dispose();
                         oPatient = null;
                     }
                     if (ogloPatient != null)
                     {
                         ogloPatient.Dispose();
                         ogloPatient = null;
                     }
                 }
             }

             private void UpdatePatient()
             {                
                 gloDatabaseLayer.DBLayer oDB = null;
                 long nTransactionID = 0;
                 long nMemberID = 0;
                 DataTable dtExternalCodeInfo =null;
                 string sPatientExistingRace = string.Empty; //Added by manoj jadhav on 20130401 for Mu2 Race changes
                 string sPatientExistingEthnicity = string.Empty;
                 
                 string strFirstName = ""; 
                 string strMiddleName= ""; 
                 string strLastName= ""; 
                 DateTime? dtDateOfBirth = null ; 
                 string strGender = ""; 
                 string strAddressLine1 = ""; 
                 string strAddressLine2 = ""; 
                 string strCity = ""; 
                 string strState = ""; 
                 string strZIP = ""; 
                 string strCounty = ""; 
                 string strPhone = "";
                 string strLoginName = "Patient Portal";


                 try
                 {
                     if (ValidateData(false))
                         return;

                     if (Convert.ToInt64(cmbProvider.SelectedValue) <= 0)
                     {
                         MessageBox.Show("Please select the provider for this patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         cmbProvider.Focus();
                         return;
                     }
                     if (_nMatchedPatientProviderID > 0 && Convert.ToInt64(cmbProvider.SelectedValue) != _nMatchedPatientProviderID)
                     {
                         if (MessageBox.Show("Provider for matched gloEMR patient is other than selected " + cmbProvider.SelectedText + ".\n Do you want to proceed updating patient provider? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes)
                         {
                             cmbProvider.Focus();
                             return;
                         }
                     }

                     nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                     nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));

                     dtExternalCodeInfo = ValidatePatientExternalCodes(0, _nMatchedPatientID, "update");
                     if(dtExternalCodeInfo !=null && dtExternalCodeInfo.Rows.Count>0 )
                     {
                         if(nMemberID.ToString() != Convert.ToString(dtExternalCodeInfo.Rows[0]["sExternalvalue"]))
                         {
                             if (MessageBox.Show("This patient is already mapped to different Portal Account. Do you wish to change this mapping, existing links will be removed?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                             {
                                  
                                     dtExternalCodeInfo.Dispose();
                                     dtExternalCodeInfo = null;
                                  
                                 return;
                             }
                         }
                     }
                     if (dtExternalCodeInfo != null)
                     {
                         dtExternalCodeInfo.Dispose();
                         dtExternalCodeInfo = null;
                     }
                     
                     dtExternalCodeInfo = ValidatePatientExternalCodes(nMemberID, 0, "register");

                     if(dtExternalCodeInfo !=null && dtExternalCodeInfo.Rows.Count>0 )
                     {
                         IEnumerable<long> query = from r in dtExternalCodeInfo.AsEnumerable() where Convert.ToInt64( r["npatientid"]) != _nMatchedPatientID
                                                   select Convert.ToInt64(r["npatientid"]);
                         if (query != null && query.Count() > 0)
                         {
                             if (MessageBox.Show("This Portal Account is mapped to different patient. Do you wish to change this mapping, existing links will be removed?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                             {
                                 dtExternalCodeInfo.Dispose();
                                 dtExternalCodeInfo = null;
                                 return;
                             }
                         }
                     }
                     if (dtExternalCodeInfo != null)
                     {
                         dtExternalCodeInfo.Dispose();
                         dtExternalCodeInfo = null;
                     }
                     string sFridValue = string.Empty;
                     string sBuildQuery = string.Empty;
                     for (int rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                     {
                         if (Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select)))
                         {
                             sFridValue = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_IntuitPatient)).Trim();

                             switch (Convert.ToString(c1PatientDetails.GetData(rCnt, COL_Field)).ToLower())
                             {
                                 case "sfirstname":
                                     if (sFridValue.Length>0)
                                     {
                                         sBuildQuery = sBuildQuery + " sfirstname ='" + sFridValue.Replace("'","''") + "',";
                                         strFirstName = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "smiddlename":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " smiddlename ='" + sFridValue.Replace("'", "''") + "',";
                                         strMiddleName = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "slastname":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " slastname ='" + sFridValue.Replace("'", "''") + "',";
                                         strLastName = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "dtdob":
                                     DateTime dt;
                                     if(sFridValue.Length > 0)
                                     {
                                         DateTime.TryParse(sFridValue,out dt);
                                         sBuildQuery = sBuildQuery + " dtdob ='" + dt.ToString("MM/dd/yyyy") + "',";
                                         dtDateOfBirth = dt;
                                     }
                                     break;
                                 case "sgender":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " sgender ='" + sFridValue.Replace("'", "''") + "',";
                                         strGender = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "semail":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " semail ='" + sFridValue.Replace("'", "''") + "',";
                                     }
                                     break;                                
                                 case "smaritalstatus":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " smaritalstatus ='" + sFridValue.Replace("'", "''") + "',";
                                     }
                                     break;
                                 case "srace":
                                     //if (sFridValue.Length > 0)
                                     {
                                         //if (!_bMultipleRace)   //Start of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         //{
                                         //    sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',"; 
                                         //}
                                         //else
                                         //{
                                         //    if (string.Compare(sFridValue, "Declined to Specify", true) == 0)
                                         //    {
                                         //        sBuildQuery = sBuildQuery + " srace='Declined to Specify',";
                                         //    }
                                         //    else
                                         //    {
                                         //        if (bIsPatientPortalEnabled == true)
                                         //        {
                                         //            //Get gloEMR existing race
                                         //            sPatientExistingRace = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                         //            //check if its blank or Declined to specify
                                                     
                                         //            string sPortalRace = Convert.ToString(sFridValue).Trim();

                                         //            if ((string.IsNullOrEmpty(sPatientExistingRace)) || (string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0))
                                         //            {
                                         //                sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',";
                                         //            }
                                         //            else if (sPatientExistingRace != "" && string.Compare(sPatientExistingRace, "Declined to Specify", true) != 0)
                                         //                {
                                         //                    sBuildQuery = sBuildQuery + " srace ='" + sPortalRace.Trim('|') + "',";
                                         //                }
                                         //           }
                                         //        else
                                         //            {
                                         //            //Get gloEMR existing race
                                         //            sPatientExistingRace = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                         //            //check if its blank or Declined to specify
                                         //            if ((string.IsNullOrEmpty(sPatientExistingRace)) || (string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0))
                                         //            {
                                         //                sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',";
                                         //            }
                                         //            else if (!sPatientExistingRace.Contains(sFridValue))
                                         //            {
                                         //                sBuildQuery = sBuildQuery + " srace = srace + '|" + sFridValue.Replace("'", "''") + "',";
                                         //            }
                                         //        }
                                         //    }
                                         //}//end of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         string sRaceValue = string.Empty;
                                         if (!_bMultipleRace)   //Start of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         {
                                             sRaceValue = sFridValue.Replace("'", "''");
                                             sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',";
                                         }
                                         else
                                         {
                                             if (string.Compare(sFridValue, "Declined to Specify", true) == 0)
                                             {
                                                 sRaceValue = "'Declined to Specify'";
                                                 sBuildQuery = sBuildQuery + " srace='Declined to Specify',";
                                             }
                                             else if (string.Compare(sFridValue, "Unknown", true) == 0)
                                             {
                                                 sRaceValue = "'Unknown'";
                                                 sBuildQuery = sBuildQuery + " srace='Unknown',";
                                             }
                                             else
                                             {
                                                 if (bIsPatientPortalEnabled == true)
                                                 {
                                                     //Get gloEMR existing race
                                                     sPatientExistingRace = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                                     //check if its blank or Declined to specify

                                                     string sPortalRace = Convert.ToString(sFridValue).Trim();

                                                     if ((string.IsNullOrEmpty(sPatientExistingRace)) || (string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0))
                                                     {
                                                         sRaceValue = sFridValue.Replace("'", "''");
                                                         sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                     else if (sPatientExistingRace != "" && string.Compare(sPatientExistingRace, "Declined to Specify", true) != 0)
                                                     {
                                                         sRaceValue = sPortalRace.Trim('|');
                                                         sBuildQuery = sBuildQuery + " srace ='" + sPortalRace.Trim('|') + "',";
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //Get gloEMR existing race
                                                     sPatientExistingRace = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                                     //check if its blank or Declined to specify
                                                     if ((string.IsNullOrEmpty(sPatientExistingRace)) || (string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0))
                                                     {
                                                         sRaceValue = sFridValue.Replace("'", "''");
                                                         sBuildQuery = sBuildQuery + " srace ='" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                     else if (!sPatientExistingRace.Contains(sFridValue))
                                                     {
                                                         sRaceValue="'|" + sFridValue.Replace("'", "''") + "'";
                                                         sBuildQuery = sBuildQuery + " srace = srace + '|" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                 }
                                             }
                                         }//end of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         InsertRaceEthnicitySpecification("Race", sRaceValue,_nMatchedPatientID);
                                     }
                                     break;
                                 case "slang":
                                     if (bIsPatientPortalEnabled == true)
                                     {
                                         // Allow to Set Blank as well
                                         sBuildQuery = sBuildQuery + " slang ='" + Convert.ToString(sFridValue).Replace("'", "''") + "',";
                                     }
                                     break;
                                 case "sethn":
                                     if (bIsPatientPortalEnabled == true)
                                     {
                                         // Allow to Set Blank as well
                                         
                                         string sEthnicityValue = string.Empty;
                                         if (!_bMultipleRace)   //Start of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         {
                                             sEthnicityValue = sFridValue.Replace("'", "''");
                                             sBuildQuery = sBuildQuery + " sethn ='" + Convert.ToString(sFridValue).Replace("'", "''") + "',";
                                         }
                                         else
                                         {
                                             if (string.Compare(sFridValue, "Declined to Specify", true) == 0)
                                             {
                                                 sEthnicityValue = "'Declined to Specify'";
                                                 sBuildQuery = sBuildQuery + " sethn='Declined to Specify',";
                                             }
                                             else if (string.Compare(sFridValue, "Unknown", true) == 0)
                                             {
                                                 sEthnicityValue = "'Unknown'";
                                                 sBuildQuery = sBuildQuery + " sethn='Unknown',";
                                             }
                                             else
                                             {
                                                 if (bIsPatientPortalEnabled == true)
                                                 {
                                                     //Get gloEMR existing race
                                                     sPatientExistingEthnicity = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                                     //check if its blank or Declined to specify

                                                     string sPortalRace = Convert.ToString(sFridValue).Trim();

                                                     if ((string.IsNullOrEmpty(sPatientExistingEthnicity)) || (string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) == 0))
                                                     {
                                                         sEthnicityValue = sFridValue.Replace("'", "''");
                                                         sBuildQuery = sBuildQuery + " sethn ='" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                     else if (sPatientExistingEthnicity != "" && string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) != 0)
                                                     {
                                                         sEthnicityValue = sPortalRace.Trim('|');
                                                         sBuildQuery = sBuildQuery + " sethn ='" + sPortalRace.Trim('|') + "',";
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //Get gloEMR existing race
                                                     sPatientExistingEthnicity = Convert.ToString(c1PatientDetails.GetData(rCnt, COL_EMRPatient)).Trim();
                                                     //check if its blank or Declined to specify
                                                     if ((string.IsNullOrEmpty(sPatientExistingEthnicity)) || (string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) == 0))
                                                     {
                                                         sEthnicityValue = sFridValue.Replace("'", "''");
                                                         sBuildQuery = sBuildQuery + " sethn ='" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                     else if (!sPatientExistingEthnicity.Contains(sFridValue))
                                                     {
                                                         sEthnicityValue = "'|" + sFridValue.Replace("'", "''") + "'";
                                                         sBuildQuery = sBuildQuery + " sethn = srace + '|" + sFridValue.Replace("'", "''") + "',";
                                                     }
                                                 }
                                             }
                                         }//end of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                         InsertRaceEthnicitySpecification("Ethnicity", sEthnicityValue, _nMatchedPatientID);
                                     }
                                     break;

                                 case "saddressline1":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " saddressline1 ='" + sFridValue.Replace("'", "''") + "',";
                                         strAddressLine1 = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "saddressline2":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " saddressline2 ='" + sFridValue.Replace("'", "''") + "',";
                                         strAddressLine2 = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "scity":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " scity ='" + sFridValue.Replace("'", "''") + "',";
                                         strCity = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "sstate":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " sstate ='" + sFridValue.Replace("'", "''") + "',";
                                         strState = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "szip":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " szip ='" + sFridValue.Replace("'", "''") + "',";
                                         strZIP = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "scountry":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " scountry ='" + sFridValue.Replace("'", "''") + "',";
                                         strCounty = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "sphone":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " sphone ='" + sFridValue.Replace("'", "''") + "',";
                                         strPhone = sFridValue.Replace("'", "''");
                                     }
                                     break;
                                 case "smobile":
                                    if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " smobile ='" + sFridValue.Replace("'", "''") + "',";
                                     }
                                     break;
                                 case "sworkphone":
                                     if (sFridValue.Length > 0)
                                     {
                                         sBuildQuery = sBuildQuery + " sworkphone ='" + sFridValue.Replace("'", "''") + "',";
                                     }
                                     break;
                             }
                         }
                     }

                     sBuildQuery = sBuildQuery.Trim();
                     if (sBuildQuery.Length > 0)
                     {
                         char ch = sBuildQuery[sBuildQuery.Length - 1];
                         if (ch == ',')
                         {
                             sBuildQuery = sBuildQuery.Remove(sBuildQuery.Length - 1, 1);
                         }
                     }

                     if (_nMatchedPatientProviderID > 0)
                     {
                         if (sBuildQuery.Length > 0)
                         {
                             if (Convert.ToInt64(cmbProvider.SelectedValue) != _nMatchedPatientProviderID)
                             {
                                 sBuildQuery = sBuildQuery + ", nproviderid=" + Convert.ToInt64(cmbProvider.SelectedValue);
                             }
                         }
                         else
                         {
                             if (Convert.ToInt64(cmbProvider.SelectedValue) != _nMatchedPatientProviderID)
                             {
                                 sBuildQuery = " nproviderid=" + Convert.ToInt64(cmbProvider.SelectedValue);
                             }
                         }
                     }

                     if (sBuildQuery.Length > 0)
                     {
                         oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                         sBuildQuery = " update patient set " + sBuildQuery + " where npatientid=" + _nMatchedPatientID.ToString();
                         oDB.Connect(false);
                         oDB.Execute_Query(sBuildQuery);
                         oDB.Disconnect();
                         oDB.Dispose();
                         oDB = null;
                         //Family account
                         InsertUpdateFamilyAccount(_nMatchedPatientID);

                         //11-May-15 Aniket: Bug #81521: Change Request : 00000373
                         //Update Patient History
                         UpdatePatientHistory(_nMatchedPatientID, DateTime.Now, strFirstName, strMiddleName, strLastName, dtDateOfBirth, strGender, strAddressLine1, strAddressLine2, strCity, strState, strZIP, strCounty, strPhone, strLoginName);


                         long AUditTrailId = 0;
                         AUditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.SetupPatient, gloAuditTrail.ActivityType.Register, "Modify Patient-Portal", _nMatchedPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                         gloPatient.clsgloPatientAudit oAudit = new gloPatient.clsgloPatientAudit(_dataBaseConnectionString);
                         oAudit.SavePatientAuditDetails(_nMatchedPatientID, AUditTrailId, "Modify Patient-Portal");
                         oAudit.Dispose();
                         oAudit = null;

                     }

                     //Updating process action
                     UpdateProcessAction(nTransactionID, nMemberID, clsGeneral.IntuitPatientProcessedAction.MatchedPatient, _nMatchedPatientID);

                     //saving patient external code                         
                     SavePatientExternalCodes(_nMatchedPatientID, nMemberID.ToString(), "MemberId");

                     //Insert Message Queue Health forms
                     if(bIntuitEnabled)
                        GenrateMessageQueue_ForCustomForms(_nMatchedPatientID, nMemberID, nTransactionID.ToString(), "Update Patient"); 

                     //reload data
                     LoadData();
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while update Portal patient data: " + ex.ToString(), true);
                     ex = null;
                 }
                 finally
                 {
                     sPatientExistingRace = string.Empty;
 
                     if(oDB!=null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                 }
             }

             private void InsertRaceEthnicitySpecification(string sValueFor, string sValue, long nMatchedPatientID)
             {
                 //Boolean bIsSuceessed = false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 gloDatabaseLayer.DBParameters oDBParams = null;
                 try
                 {
                     if (sValue == "'Declined to Specify'") { sValue = "Declined to Specify"; }
                     if (sValue == "'Unknown'") { sValue = "Unknown"; }

                     if (sValueFor.ToUpper()=="RACE")
                     {
                         oDBParams = new gloDatabaseLayer.DBParameters();
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@nPatientID", nMatchedPatientID, ParameterDirection.Input, SqlDbType.BigInt));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@sRace", sValue, ParameterDirection.Input, SqlDbType.VarChar,1000));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@Flag", 1, ParameterDirection.Input, SqlDbType.Int));
                         oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                         oDB.Connect(false);
                         oDB.Execute("gsp_InupPatient_RaceSpecification", oDBParams); 
                     }

                     if (sValueFor.ToUpper()=="ETHNICITY")
                     {
                         oDBParams = new gloDatabaseLayer.DBParameters();
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@nPatientID", nMatchedPatientID, ParameterDirection.Input, SqlDbType.BigInt));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@sEthn", sValue, ParameterDirection.Input, SqlDbType.VarChar, 1000));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@Flag", 1, ParameterDirection.Input, SqlDbType.Int));
                         oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                         oDB.Connect(false);
                         oDB.Execute("gsp_InupPatient_EthnicitySpecification", oDBParams); 
                     }
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while inserting/updating family account for Portal patient: " + ex.ToString(), false);
                     ex = null;
                     
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Disconnect();
                         oDB.Dispose();
                         oDB = null;
                     }
                     if (oDBParams != null)
                     {
                         oDBParams.Dispose();
                         oDBParams = null;
                     }
                 }
             }

             private Boolean RejectPatient(long nTransactionID, long nMemberID, clsGeneral.IntuitPatientProcessedAction enumProcessAction)
             {
                 UpdateProcessAction(nTransactionID, nMemberID, enumProcessAction,0);
                 //reload data
                 LoadData();

                 return true;
             }

             private void ClearEMRPatientData()
             {
                 try
                 {
                    for (int rCnt = 1; rCnt < c1PatientDetails.Rows.Count; rCnt++)
                    {                       
                        c1PatientDetails.SetData(rCnt, COL_EMRPatient, string.Empty);
                        if (c1PatientDetails.GetData(rCnt, COL_IntuitPatient).ToString().Trim().Length>0 &&
                            Convert.ToBoolean(c1PatientDetails.GetData(rCnt, COL_Select))== false)
                        {
                            c1PatientDetails.SetData(rCnt, COL_Select, true);
                        }                        
                    }
                    _nMatchedPatientID = 0;
                    _nMatchedPatientProviderID = 0;
                    if (cmbProvider.Items.Count > 0)
                    {
                        cmbProvider.SelectedIndex = 0;
                    }
                 }
                 catch //(Exception ex)
                 {
                    // ex = null;
                 }
             }

             //11-May-15 Aniket: Bug #81521: Change Request : 00000373
             private void UpdatePatientHistory(long intPatientID, DateTime dtChangedDateTime, string strFirstName, string strMiddleName, string strLastName, DateTime? dtDateOfBirth, string strGender, string strAddressLine1, string strAddressLine2, string strCity, string strState, string strZIP, string strCounty, string strPhone, string strLoginName)
             {
                 
                 gloDatabaseLayer.DBLayer oDB = null;
                 gloDatabaseLayer.DBParameters oDBParams = null;
                 
                 try
                 {
                     if (intPatientID != 0)
                     {
                         oDBParams = new gloDatabaseLayer.DBParameters();
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@PatientID", intPatientID, ParameterDirection.Input, SqlDbType.BigInt));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@dtChangeDateTime", dtChangedDateTime, ParameterDirection.Input, SqlDbType.DateTime));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@FirstName", strFirstName, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@MiddleName", strMiddleName, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@LastName", strLastName, ParameterDirection.Input, SqlDbType.VarChar));

                         if (dtDateOfBirth != null)
                         {
                             oDBParams.Add(new gloDatabaseLayer.DBParameter("@dtDOB", dtDateOfBirth, ParameterDirection.Input, SqlDbType.DateTime));
                         }
                         else
                         {
                             oDBParams.Add(new gloDatabaseLayer.DBParameter("@dtDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime));
                         }

                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@Gender", strGender, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@AddressLine1", strAddressLine1, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@AddressLine2", strAddressLine2, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@City", strCity, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@State", strState, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@ZIP", strZIP, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@County", strCounty, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@Phone", strPhone, ParameterDirection.Input, SqlDbType.VarChar));
                         oDBParams.Add(new gloDatabaseLayer.DBParameter("@sLoginName", strLoginName, ParameterDirection.Input, SqlDbType.VarChar));



                         object objValue = null;
                         oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                         oDB.Connect(false);
                         oDB.Execute("gsp_Insert_PatientChangeHistory", oDBParams, out objValue);

                         objValue = null;
                     }
                 }

                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while updating Patient DemoHX: " + ex.ToString(), false);
                     ex = null;
                     
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                     if (oDBParams != null)
                     {
                         oDBParams.Dispose();
                         oDBParams = null;
                     }
                 }
             }

             private Boolean UpdateProcessAction(long nTransactionID, long nMemberID,clsGeneral.IntuitPatientProcessedAction enumProcessAction,long nPatientID)
             {
                 Boolean bIsSuceessed = false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 gloDatabaseLayer.DBParameters oDBParams = null;
                 try
                 {
                     oDBParams = new gloDatabaseLayer.DBParameters();
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@TransactionID", nTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt));
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@MemberID", nMemberID, ParameterDirection.Input, SqlDbType.BigInt));
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@Processed", 1, ParameterDirection.Input, SqlDbType.BigInt));
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@ProcessedAction", enumProcessAction.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt));
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt));
                      
                     object objValue = null;
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     oDB.Execute("gsp_InUpCFPatientMst", oDBParams, out objValue);
                     bIsSuceessed = true;
                     objValue = null;
                     return bIsSuceessed;
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while updating processed action on repository data: " + ex.ToString(), false);
                     ex = null;
                     return bIsSuceessed;
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                     if (oDBParams != null)
                     {
                         oDBParams.Dispose();
                         oDBParams = null;
                     }
                 }
             }

             private string GetFieldCaption(string sFieldName)
             {
                 string sReturnCaption = string.Empty;
                 try
                 {
                     sReturnCaption = sFieldName;

                     switch (sFieldName.ToLower())
                     {
                         case "sfirstname":
                             sReturnCaption = "First Name";
                             break;
                         case "smiddlename":
                             sReturnCaption = "Middle Name";
                             break;
                         case "slastname":
                             sReturnCaption = "Last Name";
                             break;
                         case "dtdob":
                             sReturnCaption = "Date of Birth";
                             break;
                         case "sgender":
                             sReturnCaption = "Gender";
                             break;
                         case "semail":
                             sReturnCaption = "Email";
                             break;                        
                         case "smaritalstatus":
                             sReturnCaption = "Marital Status";
                             break;
                         case "srace":
                             sReturnCaption = "Race";
                             break;
                         case "slang":
                             sReturnCaption = "Language";
                             break;
                         case "sethn":
                             sReturnCaption = "Ethnicity";
                             break;
                         case "saddressline1":
                             sReturnCaption = "Address 1";
                             break;
                         case "saddressline2":
                             sReturnCaption = "Address 2";
                             break;
                         case "scity":
                             sReturnCaption = "City";
                             break;
                         case "sstate":
                             sReturnCaption = "State";
                             break;
                         case "szip":
                             sReturnCaption = "Zip";
                             break;
                         case "scountry":
                             sReturnCaption = "Country";
                             break;
                         case "sphone":
                             sReturnCaption = "Phone";
                             break;
                         case "smobile":
                             sReturnCaption = "Mobile";
                             break;
                         case "sworkphone":
                             sReturnCaption = "Work Phone";
                             break;
                         default :
                             sReturnCaption = sFieldName;
                             break;
                     }

                     return sReturnCaption;
                 }
                 catch //(Exception ex)
                 {
                     return sReturnCaption;
                     //ex = null;
                 }
                 finally
                 {
                     sReturnCaption = null;
                 }                 
             }

             private Boolean InsertUpdateFamilyAccount(long nPatientID)
             {
                 Boolean bIsSuceessed = false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 gloDatabaseLayer.DBParameters oDBParams = null;
                 try
                 {
                     oDBParams = new gloDatabaseLayer.DBParameters();
                     oDBParams.Add(new gloDatabaseLayer.DBParameter("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt));                                          
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     oDB.Execute("PA_Accounts_CreateAccounts", oDBParams);
                     bIsSuceessed = true;                     
                     return bIsSuceessed;
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred while inserting/updating family account for Portal patient: " + ex.ToString(), false);
                     ex = null;
                     return bIsSuceessed;
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                     if (oDBParams != null)
                     {
                         oDBParams.Dispose();
                         oDBParams = null;
                     }
                 }
             }

             public bool SavePatientExternalCodes(Int64 nPatientId, string sPatientExternalCode, string sExternalType)
             {
                 gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                 gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                 bool blnResult = false;
                 try
                 {
                     oDBLayer.Connect(false);
                     //Patient Portal
                     if (bIsPatientPortalEnabled == true)
                     {
                         oDBParameters.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                         oDBParameters.Add("@sExternalType", "PatientPortal", ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalSubType", sExternalType, ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalValue", sPatientExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalSystemName", "PatientPortal", ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sModuleName", "PatientPortal", ParameterDirection.Input, SqlDbType.VarChar);
                     }
                     //Patient Portal
                     else
                     {
                         oDBParameters.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                         oDBParameters.Add("@sExternalType", "MEDFUSION", ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalSubType", sExternalType, ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalValue", sPatientExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sExternalSystemName", "MEDFUSION", ParameterDirection.Input, SqlDbType.VarChar);
                         oDBParameters.Add("@sModuleName", "Interface", ParameterDirection.Input, SqlDbType.VarChar);
                     }
                     oDBLayer.ExecuteScalar("gsp_INUP_CFPatientExternalCode", oDBParameters);

                     blnResult = true;
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog("Error occurred storing Portal patient external code: " + ex.ToString(), false);
                    ex=null;
                 }
                 finally
                 {
                     if (oDBLayer != null)
                     {
                         oDBLayer.Dispose();
                     }
                     if (oDBParameters != null)
                     {
                         oDBParameters.Dispose();
                     }
                 }
                 return blnResult;
             }
             //Patient Portal
             public string _MachineName = System.Windows.Forms.SystemInformation.ComputerName;
             string strPatientPortalEmailService = "";
             string strPatientPortalSiteNm = "";
             Int64 _ClinicID = 1;
             public void getPatientPortalSettings()
             {
                 DataTable dt = GetSetting("PatientPortalEmailService");
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         strPatientPortalEmailService = dt.Rows[0]["sSettingsValue"].ToString();
                     }
                     dt.Dispose();
                     dt = null;
                 }
                 dt = GetSetting("PatientPortalSiteName");
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         strPatientPortalSiteNm = dt.Rows[0]["sSettingsValue"].ToString();
                     }
                     dt.Dispose();
                     dt = null;
                 }
             }
            // ClsMessageQueue oclsMessageQueue;
             private void SendPatientPortalEmails(Int64 PatientID)
             {
                 //if (bIsPatientPortalEnabled)
                 //{
                 //    long nUserId = 0;
                 //    if (appSettings["UserID"] != null)
                 //    {
                 //        if (appSettings["UserID"] != "")
                 //        { nUserId = Convert.ToInt64(appSettings["UserID"]); }
                 //    }
                 //    IsClientAccess(_MachineName);
                     //'slr  Free oGneralInterface
                     //if ((oclsMessageQueue != null))
                     //{
                     //    oclsMessageQueue = null;
                     //}
                     //oclsMessageQueue = new ClsMessageQueue();
                     //oclsMessageQueue.InsertInMessageQueue(PatientID, nUserId, _dataBaseConnectionString, ClientMachineID, _MachineName, true, true, true);
                     //GetMessageQueue(PatientID);
                 //}
             }
             private void GetMessageQueue(Int64 PatientID)
             {

                 //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                 //gloDatabaseLayer.DBParameters oParamater = new gloDatabaseLayer.DBParameters();
                 //DataTable oDataTable = null;
                 //Int64 nPatientID = 0;
                 //Int64 nMessageID = 0;
                 //string sMessageName = "";
                 //string str = "";
                 //string sEmail = "";
                 //string sZip = "";

                 //if (oDB != null)
                 //{
                 //    if (oDB.Connect(false))
                 //    {
                 //        try
                 //        {
                 //            oDB.Retrive_Query("SELECT gl_MessageQueue.nMessageID,gl_MessageQueue.npatientID,isnull(patient.sEmail,'') sEmail,isnull(patient.sZip,'') sZip,gl_MessageQueue.sMessageName   FROM gl_MessageQueue inner join patient on gl_MessageQueue.npatientID = patient.npatientID WHERE (smessagename='PATIENTREGISTRATION') and sservicename = 'PatientPortal' and (nStatus = 1) and gl_MessageQueue.npatientid = " + PatientID.ToString(), out oDataTable);

                 //            if (oDataTable != null)
                 //            {

                 //                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                 //                {
                 //                    nMessageID = Convert.ToInt64(oDataTable.Rows[i]["nMessageID"]);
                 //                    sMessageName = oDataTable.Rows[i]["sMessageName"].ToString();
                 //                    nPatientID = Convert.ToInt64(oDataTable.Rows[i]["npatientID"]);
                 //                    sEmail = oDataTable.Rows[i]["sEmail"].ToString().Trim();
                 //                    sZip = oDataTable.Rows[i]["sZip"].ToString().Trim();

                 //                    if ((string.IsNullOrEmpty(sEmail) | string.IsNullOrEmpty(sZip)))
                 //                    {
                 //                        str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + nMessageID.ToString();
                 //                    }
                 //                    else
                 //                    {
                 //                        clsgloPatientPortalEmail clsgloPatientPortalEmail = new clsgloPatientPortalEmail(_dataBaseConnectionString);
                 //                        //'Convert.ToString(System.Configuration.ConfigurationManager.AppSettings.Get("EmailSeviceUri"))

                 //                        string strServiceURI = strPatientPortalEmailService;
                 //                        string strpagename = "/PatientActivation.html";
                 //                        if (sMessageName == "PATIENTREGISTRATION")
                 //                        {
                 //                            strpagename = "/index.html";
                 //                        }
                 //                        if (clsgloPatientPortalEmail.CreateMail(sEmail, nPatientID, "Patient Registration", strServiceURI, strPatientPortalSiteNm + strpagename, _ClinicID))
                 //                        {
                 //                            str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + DateTime.Now.ToString() + "'    WHERE nMessageID=  " + nMessageID.ToString();
                 //                        }
                 //                        else
                 //                        {
                 //                            str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + nMessageID.ToString();
                 //                        }
                 //                    }

                 //                    oDB.Execute_Query(str);

                 //                }

                 //            }

                 //        }
                 //        catch (gloDatabaseLayer.DBException ex)
                 //        {
                 //            ex.ERROR_Log(ex.ToString());
                 //        }
                 //        catch (Exception ex)
                 //        {
                 //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                 //        }
                 //        finally
                 //        {
                 //            if (oDataTable != null)
                 //            {
                 //                oDataTable.Dispose();
                 //                oDataTable = null;
                 //            }
                 //            if (oParamater != null)
                 //            {
                 //                oParamater.Dispose();
                 //                oParamater = null;
                 //            }
                 //            if (oDB != null)
                 //            {
                 //                oDB.Disconnect();
                 //                oDB.Dispose();
                 //                oDB = null;
                 //            }
                 //        }
                 //    }
                 //}

             }

             private DataTable GetSetting(string SettingName)
             {
                 SqlConnection objCon = new SqlConnection();
                 SqlCommand objCmd = new SqlCommand();
                 DataTable dtTable = new DataTable();
                 try
                 {
                     objCon.ConnectionString = _dataBaseConnectionString;
                     objCmd.CommandType = CommandType.Text;
                     objCmd.CommandText = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "'  AND nClinicID = " + _ClinicID + "";
                     objCmd.Connection = objCon;
                     objCmd.Connection = objCon;
                     objCon.Open();
                     SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
                     objDA.Fill(dtTable);
                     objCon.Close();

                     objDA.Dispose();
                     objDA = null;

                     return dtTable;

                 }
                 catch //(Exception ex)
                 {
                     return null;
                 }
                 finally
                 {
                     if ((objCon != null))
                     {
                         objCon.Dispose();
                         objCon = null;
                     }

                     if ((objCmd != null))
                     {
                         objCmd.Parameters.Clear();
                         objCmd.Dispose();
                         objCmd = null;
                     }

                     if ((dtTable == null))
                     {
                         dtTable = null;
                     }
                 }

             }
             string ClientMachineID = "";
             public bool IsClientAccess(string strClientMachineName)
             {
                 //gnPrefixTransactionID = 0
                 //Dim nLoginUsers As Byte
                 SqlConnection objCon = new SqlConnection();
                 objCon.ConnectionString = _dataBaseConnectionString;
                 SqlCommand objCmd = new SqlCommand();
                 objCmd.CommandType = CommandType.StoredProcedure;

                 //Aniket Renamed gsp_CheckClientMachinePermission to sp_CheckClientMachinePermission as it is necessary for backward compatibility in multiple databases
                 objCmd.CommandText = "sp_CheckClientMachinePermission";
                 objCmd.Connection = objCon;

                 SqlParameter objParaClientMachineName = new SqlParameter();
                 var _with1 = objParaClientMachineName;
                 _with1.ParameterName = "@MachineName";
                 _with1.Value = strClientMachineName;
                 _with1.Direction = ParameterDirection.Input;
                 _with1.SqlDbType = SqlDbType.VarChar;
                 objCmd.Parameters.Add(objParaClientMachineName);
                 objParaClientMachineName = null;

                 //'Sandip Darade 20091113
                 SqlParameter objParaProductCode = new SqlParameter();
                 var _with2 = objParaProductCode;
                 _with2.ParameterName = "@sProductCode";
                 _with2.Value = "1";
                 _with2.Direction = ParameterDirection.Input;
                 _with2.SqlDbType = SqlDbType.VarChar;
                 objCmd.Parameters.Add(objParaProductCode);
                 objParaProductCode = null;

                 ClientMachineID = "";
                 objCon.Open();
                 object machineid = objCmd.ExecuteScalar();
                 ClientMachineID = Convert.ToString(machineid);
                 objCon.Close();
                 if ((ClientMachineID == null))
                 {
                     ClientMachineID = "";
                 }
                 objCmd.Parameters.Clear();
                 objCmd.Dispose();
                 objCmd = null;
                 objCon.Dispose();
                 objCon = null;
                 if (ClientMachineID == "")
                 {
                     return false;
                 }
                 else
                 {
                     return true;
                 }
             }
             //Patient Portal

             public DataTable ValidatePatientExternalCodes(long memberID,long patientID, string sProcessAction)
             {
                 gloDatabaseLayer.DBLayer oDB =null;
                 DataTable dt =null;
                 string sQuery = string.Empty;
                 try
                 {
                     switch(sProcessAction.ToLower())
                     {
                         case "register":
                             sQuery = "select nPatientID,sExternalvalue from patientexternalcodes where sExternalSubType= 'MemberId' and sExternalType ='MEDFUSION' and sExternalvalue='" + memberID.ToString() + "'";
                             break;
                         case "update":
                             sQuery = "select nPatientID,sExternalvalue from patientexternalcodes where sExternalSubType= 'MemberId' and sExternalType ='MEDFUSION' and nPatientID=" + patientID.ToString();
                             break;
                         default :
                             sQuery = string.Empty;
                             break;
                     }

                     if (sQuery.Length > 0)
                     {
                         oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                         oDB.Connect(false);
                         oDB.Retrive_Query(sQuery, out dt);
                         oDB.Disconnect();
                     }
                     return dt;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                     sQuery = null;
                 }
             }

             private Boolean ValidateData(Boolean bIsNewPatient)
             {
                 Boolean IsNotValidData =false;

                 try 
	            {	        
		          //For New patient
                     //Mandatory field must be selected and data should be available for that

                    for (Int32 nRow = 1; nRow < c1PatientDetails.Rows.Count; nRow++)
                    {
                        switch (c1PatientDetails.GetData(nRow, COL_Field).ToString().ToLower())
                        {
                            case "slastname":
                                if (bIsNewPatient)
                                {
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == false ||
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length == 0)
                                    {
                                        IsNotValidData = true;                                                                         
                                        MessageBox.Show("Last name is required for adding the patient in gloEMR", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                    }                                    
                                }                                   
                                
                                if(Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                {
                                    IsNotValidData = true;                                    
                                    MessageBox.Show("Last name should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                }
                                break;
                            case "sfirstname":
                                    if (bIsNewPatient)
                                    {
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == false ||
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length == 0)
                                        {
                                            IsNotValidData = true;                                           
                                            MessageBox.Show("First name is required for adding the patient in gloEMR", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }                                        
                                    }
                                    
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                       c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    {
                                        IsNotValidData = true;
                                        MessageBox.Show("First name should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "smiddlename":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                         c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    {                                       
                                            IsNotValidData = true;
                                            MessageBox.Show("Middle name should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "dtdob" :
                                    if (bIsNewPatient)
                                    {
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == false ||
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length == 0)
                                        {
                                            IsNotValidData = true;                                            
                                            MessageBox.Show("Date of birth is required for adding the patient in gloEMR", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                        else
                                        {
                                            DateTime dtTemp;
                                            if (DateTime.TryParse(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), out dtTemp))
                                            {
                                                if (dtTemp.Date > DateTime.Now.Date || dtTemp.Date < Convert.ToDateTime("01/01/1900").Date)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("Invalid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                                }
                                            }
                                            else
                                            {
                                                IsNotValidData = true;
                                                MessageBox.Show("Invalid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                        {
                                            DateTime dtTemp;
                                            if (DateTime.TryParse(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), out dtTemp))
                                            {
                                                if (dtTemp.Date > DateTime.Now.Date || dtTemp.Date < Convert.ToDateTime("01/01/1900").Date)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("Invalid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                                }
                                            }
                                            else
                                            {
                                                IsNotValidData = true;
                                                MessageBox.Show("Invalid date of birth", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                            }
                                        }
                                    }
                                    break;
                            case "sgender" :
                                    if (bIsNewPatient)
                                    {
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == false ||
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length == 0)
                                        {
                                            IsNotValidData = true;                                            
                                            MessageBox.Show("Gender is required for adding the patient in gloEMR", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                        {
                                            string sGender = string.Empty;
                                            sGender = c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim();
                                            if (!(string.Compare(sGender, "Female", true) == 0 || string.Compare(sGender, "Male", true) == 0
                                                || string.Compare(sGender, "Unknown", true) == 0 ||string.Compare(sGender, "Other", true) == 0))
                                            {
                                                IsNotValidData = true;
                                                MessageBox.Show("Select valid gender", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                            c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                        {
                                            string sGender = string.Empty;
                                            sGender = c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim();
                                            if (!(string.Compare(sGender, "Female", true) == 0 || string.Compare(sGender, "Male", true) == 0
                                                || string.Compare(sGender, "Unknown", true) == 0 || string.Compare(sGender, "Other", true) == 0))
                                            {
                                                IsNotValidData = true;
                                                MessageBox.Show("Select valid gender", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                            }
                                        }
                                    }                                    
                                    break;
                            case "smaritalstatus":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    {                                   
                                        string sMaritalStatus = string.Empty;
                                        sMaritalStatus = c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim();
                                        if (!(string.Compare(sMaritalStatus, "UnMarried", true) == 0 || string.Compare(sMaritalStatus, "Married", true) == 0 ||
                                            string.Compare(sMaritalStatus, "Single", true) == 0 || string.Compare(sMaritalStatus, "Widowed", true) == 0 ||
                                            string.Compare(sMaritalStatus, "Divorced", true) == 0))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Select valid marital status", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                    }
                                    break;
                            case "semail":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    {
                                        string sEmail = string.Empty;
                                        sEmail = c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim();
                                        if (!System.Text.RegularExpressions.Regex.IsMatch(sEmail, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Invalid Email ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                        else if (c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Email ID should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                    }                                    

                                    break;                                                                                                  
                            case "srace":
                                    //Start of Commented by manoj jadhav on 20130401 for Mu2 Race changes
                                    //if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                    //  c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    //{
                                    //    IsNotValidData = true;
                                    //    MessageBox.Show("Race should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    //}
                                    //if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                    //  c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    //{
                                    //    string [] aRace = _RaceList.Split('|');

                                    //    IEnumerable<string> result = from r in aRace
                                    //                                 where r.Trim().ToLower() ==
                                    //                                     c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().ToLower()
                                    //                                 select r;

                                    //    if (result.Count() <1)
                                    //    {
                                    //        IsNotValidData = true;
                                    //        MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    //    }
                                    //}
                                    //end of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                    //Start of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true)
                                    {
                                        string sPatientExistingRace = string.Empty;
                                        string sPortalRace = string.Empty;
                                        string[] aRace = null;
                                        IEnumerable<string> result = null;
                                        try
                                        {
                                            
                                            sPortalRace = Convert.ToString(c1PatientDetails.GetData(nRow, COL_IntuitPatient)).Trim();
                                            if (sPortalRace.Length > 0)
                                            {
                                                
                                                if ( ! _bMultipleRace)
                                                {
                                                  sPatientExistingRace = sPortalRace;
                                                }
                                                else
                                                {
                                                     sPatientExistingRace = Convert.ToString(c1PatientDetails.GetData(nRow, COL_EMRPatient)).Trim();

                                                    // Multiple Race, Portal Related changes.
                                                     if (bIsPatientPortalEnabled == true)
                                                     {
                                                         if (string.Compare(sPortalRace, "Declined to Specify", true) == 0)
                                                         {
                                                             sPatientExistingRace = sPortalRace;
                                                         }
                                                         else if ((string.IsNullOrEmpty(sPatientExistingRace)) || string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0)
                                                         {
                                                             sPatientExistingRace = sPortalRace;
                                                         }
                                                         else if (sPatientExistingRace != "" && sPatientExistingRace.Contains('|') && string.Compare(sPatientExistingRace, "Declined to Specify", true) != 0)
                                                         {
                                                             if (sPortalRace != "" && sPortalRace.Contains("|"))
                                                             {
                                                                 string[] strPortalRace = sPortalRace.Split('|');
                                                                 
                                                                 if (strPortalRace.Length > 0)
                                                                 {
                                                                     for (int i = 0; i < strPortalRace.Length; i++)
                                                                     {
                                                                         if (!sPatientExistingRace.ToLower().Contains(Convert.ToString(strPortalRace[i]).Trim().ToLower()))
                                                                         {
                                                                             sPatientExistingRace = sPatientExistingRace + "|" + Convert.ToString(strPortalRace[i]).Trim();
                                                                         }
                                                                     }
                                                                 }
                                                             }
                                                             else if(sPortalRace !="" && sPortalRace.Length > 0)
                                                             {
                                                                 if (!sPatientExistingRace.ToLower().Contains(Convert.ToString(sPortalRace).Trim().ToLower()))
                                                                 {
                                                                     sPatientExistingRace = sPatientExistingRace + "|" + Convert.ToString(sPortalRace).Trim();
                                                                 }
                                                             }
                                                         }
                                                         else if (sPatientExistingRace != "" && sPatientExistingRace.Length > 0 && string.Compare(sPatientExistingRace, "Declined to Specify", true) != 0)
                                                         {
                                                             if (sPortalRace != "" && sPortalRace.Contains("|"))
                                                             {
                                                                 string[] strPortalRace = sPortalRace.Split('|');

                                                                 if (strPortalRace.Length > 0)
                                                                 {
                                                                     for (int i = 0; i < strPortalRace.Length; i++)
                                                                     {
                                                                         if (!sPatientExistingRace.ToLower().Contains(Convert.ToString(strPortalRace[i]).Trim().ToLower()))
                                                                         {
                                                                             sPatientExistingRace = sPatientExistingRace + "|" + Convert.ToString(strPortalRace[i]).Trim();
                                                                         }
                                                                     }
                                                                 }
                                                             }
                                                             else if (sPortalRace != "" && sPortalRace.Length > 0)
                                                             {
                                                                 if (!sPatientExistingRace.ToLower().Contains(Convert.ToString(sPortalRace).Trim().ToLower()))
                                                                 {
                                                                     sPatientExistingRace = sPatientExistingRace + "|" + Convert.ToString(sPortalRace).Trim();
                                                                 }
                                                             }
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (string.Compare(sPortalRace, "Declined to Specify", true) == 0)
                                                         {
                                                             sPatientExistingRace = sPortalRace;
                                                         }
                                                         else if (string.Compare(sPatientExistingRace, "Declined to Specify", true) == 0)
                                                         {
                                                             sPatientExistingRace = sPortalRace;
                                                         }
                                                         else if (!sPatientExistingRace.Contains(sPortalRace))
                                                         {
                                                             sPatientExistingRace = sPatientExistingRace + "|" + sPortalRace;
                                                         }
                                                     }
               
                                                } //else

                                                //check size of new Race
                                                if (sPatientExistingRace.Length > 1000)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("Race should not exceed 1000 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                }
                                                
                                                //check is race exist in category master
                                                aRace = _RaceList.Split('|');

                                                if (bIsPatientPortalEnabled == true)
                                                {
                                                    // Check Multiple Race Existance in category master
                                                    string strRace = Convert.ToString(sPortalRace).Trim();
                                                    if (strRace != "" && Convert.ToString(strRace).Contains("|"))
                                                    {
                                                        string[] selectedRace = strRace.Split('|');
                                                        if (selectedRace.Length > 0)
                                                        {
                                                            int foundcnt = 0;
                                                            for (int i = 0; i < selectedRace.Length; i++)
                                                            {
                                                                for (int j = 0; j < aRace.Length; j++)
                                                                {
                                                                    if (Convert.ToString(selectedRace[i]).Trim().ToLower() == Convert.ToString(aRace[j]).Trim().ToLower())
                                                                    {
                                                                        foundcnt += 1;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            if (foundcnt != selectedRace.Length)
                                                            {
                                                                IsNotValidData = true;
                                                                MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                            }
                                                        }
                                                    }
                                                    // Check Single Race Existance in category master
                                                    else if (strRace != "" && strRace.Length > 0) 
                                                    {
                                                        bool IsFound = false;
                                                        for (int i = 0; i < aRace.Length; i++)
                                                        {
                                                            if (strRace.ToLower() == Convert.ToString(aRace[i]).Trim().ToLower())
                                                            {
                                                                IsFound = true;
                                                                break;
                                                            }
                                                        }
                                                        if (IsFound != true)
                                                        {
                                                            IsNotValidData = true;
                                                            MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                        }
                                                    }
                                                    else 
                                                    {
                                                        IsNotValidData = true;
                                                        MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                    }
                                                }
                                                else
                                                {
                                                    result = from r in aRace
                                                             where r.Trim().ToLower() == sPortalRace.ToLower()
                                                             select r;
                                                    if (result.Count() < 1)
                                                    {
                                                        IsNotValidData = true;
                                                        MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                    }
                                                }
                                          } //end of if                                                                                                                                     
                                        }//endfo try
                                        catch (Exception)
                                        { }
                                        finally
                                        {
                                            sPatientExistingRace = string.Empty;
                                            sPortalRace = string.Empty;
                                            aRace = null;
                                            result = null; 
                                        }
                                        //end of Added by manoj jadhav on 20130401 for Mu2 Race changes
                                    }
                                    break;
                            case "slang":
                                  
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true)
                                    {
                                        string sPatientExistingLang = string.Empty;
                                        string sPortallang = string.Empty;
                                        string[] aLang = null;
                                        IEnumerable<string> result = null;
                                        try
                                        {
                                            sPortallang = Convert.ToString(c1PatientDetails.GetData(nRow, COL_IntuitPatient)).Trim();
                                        
                                                
                                                    sPatientExistingLang = Convert.ToString(c1PatientDetails.GetData(nRow, COL_EMRPatient)).Trim();

                                                    if (string.Compare(sPortallang, "Declined to Specify", true) == 0)
                                                    {
                                                        sPatientExistingLang = sPortallang;
                                                    }
                                                    else if (string.Compare(sPatientExistingLang, "Declined to Specify", true) == 0)
                                                    {
                                                        sPatientExistingLang = sPortallang;
                                                    }
                                                    else if (!sPatientExistingLang.Contains(sPortallang))
                                                    {
                                                        sPatientExistingLang = sPatientExistingLang + "|" + sPortallang;
                                                    }
                                            
                                                //check size of new lang
                                                if (sPatientExistingLang.Length > 50)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("Language should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                }

                                                //check is Lang exist in category master
                                                aLang = _LanguageList.Split('|');
                                                result = from r in aLang
                                                         where r.Trim().ToLower() == sPortallang.ToLower()
                                                         select r;
                                                if (result.Count() < 1)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("select valid Language", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                }

                                        }//endfo try
                                        catch (Exception)
                                        { }
                                        finally
                                        {
                                            sPatientExistingLang = string.Empty;
                                            sPortallang = string.Empty;
                                            aLang = null;
                                            result = null;
                                        }
                                    }
                                    break;

                            case "sethn":

                                    //if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true)
                                    //{
                                    //    string sPatientExistingEthn = string.Empty;
                                    //    string sPortalEthn = string.Empty;
                                    //    string[] aLang = null;
                                    //    IEnumerable<string> result = null;
                                    //    try
                                    //    {
                                    //        sPortalEthn = Convert.ToString(c1PatientDetails.GetData(nRow, COL_IntuitPatient)).Trim();
                                          
                                    //                sPatientExistingEthn = Convert.ToString(c1PatientDetails.GetData(nRow, COL_EMRPatient)).Trim();

                                    //                if (string.Compare(sPortalEthn, "Declined to Specify", true) == 0)
                                    //                {
                                    //                    sPatientExistingEthn = sPortalEthn;
                                    //                }
                                    //                else if (string.Compare(sPatientExistingEthn, "Declined to Specify", true) == 0)
                                    //                {
                                    //                    sPatientExistingEthn = sPortalEthn;
                                    //                }
                                    //                else if (!sPatientExistingEthn.Contains(sPortalEthn))
                                    //                {
                                    //                    sPatientExistingEthn = sPatientExistingEthn + "|" + sPortalEthn;
                                    //                }

                                    //            //check size of new Ethnicity
                                    //            if (sPatientExistingEthn.Length > 50)
                                    //            {
                                    //                IsNotValidData = true;
                                    //                MessageBox.Show("Ethnicity should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //                c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                    //            }

                                    //            //check is Ethnicity exist in category master
                                    //            aLang = _EthnicityList.Split('|');
                                    //            result = from r in aLang
                                    //                     where r.Trim().ToLower() == sPortalEthn.ToLower()
                                    //                     select r;
                                    //            if (result.Count() < 1)
                                    //            {
                                    //                IsNotValidData = true;
                                    //                MessageBox.Show("select valid Ethnicity", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //                c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                    //            }

                                    //    }//endfo try
                                    //    catch (Exception)
                                    //    { }
                                    //    finally
                                    //    {
                                    //        sPatientExistingEthn = string.Empty;
                                    //        sPortalEthn = string.Empty;
                                    //        aLang = null;
                                    //        result = null;
                                    //    }
                                    //}

                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true)
                                    {
                                        string sPatientExistingEthnicity = string.Empty;
                                        string sPortalEthnicity = string.Empty;
                                        //string[] aRace = null;
                                        string[] aEthnicity = null;
                                        IEnumerable<string> result = null;
                                        try
                                        {

                                            sPortalEthnicity = Convert.ToString(c1PatientDetails.GetData(nRow, COL_IntuitPatient)).Trim();
                                            if (sPortalEthnicity.Length > 0)
                                            {

                                                if (!_bMultipleRace)
                                                {
                                                    sPatientExistingEthnicity = sPortalEthnicity;
                                                }
                                                else
                                                {
                                                    sPatientExistingEthnicity = Convert.ToString(c1PatientDetails.GetData(nRow, COL_EMRPatient)).Trim();

                                                    // Multiple Race, Portal Related changes.
                                                    if (bIsPatientPortalEnabled == true)
                                                    {
                                                        if (string.Compare(sPortalEthnicity, "Declined to Specify", true) == 0)
                                                        {
                                                            sPatientExistingEthnicity = sPortalEthnicity;
                                                        }
                                                        else if ((string.IsNullOrEmpty(sPatientExistingEthnicity)) || string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) == 0)
                                                        {
                                                            sPatientExistingEthnicity = sPortalEthnicity;
                                                        }
                                                        else if (sPatientExistingEthnicity != "" && sPatientExistingEthnicity.Contains('|') && string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) != 0)
                                                        {
                                                            if (sPortalEthnicity != "" && sPortalEthnicity.Contains("|"))
                                                            {
                                                                string[] strPortalethnicity = sPortalEthnicity.Split('|');

                                                                if (strPortalethnicity.Length > 0)
                                                                {
                                                                    for (int i = 0; i < strPortalethnicity.Length; i++)
                                                                    {
                                                                        if (!sPatientExistingEthnicity.ToLower().Contains(Convert.ToString(strPortalethnicity[i]).Trim().ToLower()))
                                                                        {
                                                                            sPatientExistingEthnicity = sPatientExistingEthnicity + "|" + Convert.ToString(strPortalethnicity[i]).Trim();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (sPortalEthnicity != "" && sPortalEthnicity.Length > 0)
                                                            {
                                                                if (!sPatientExistingEthnicity.ToLower().Contains(Convert.ToString(sPortalEthnicity).Trim().ToLower()))
                                                                {
                                                                    sPatientExistingEthnicity = sPatientExistingEthnicity + "|" + Convert.ToString(sPortalEthnicity).Trim();
                                                                }
                                                            }
                                                        }
                                                        else if (sPatientExistingEthnicity != "" && sPatientExistingEthnicity.Length > 0 && string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) != 0)
                                                        {
                                                            if (sPortalEthnicity != "" && sPortalEthnicity.Contains("|"))
                                                            {
                                                                string[] strPortalethnicity = sPortalEthnicity.Split('|');

                                                                if (strPortalethnicity.Length > 0)
                                                                {
                                                                    for (int i = 0; i < strPortalethnicity.Length; i++)
                                                                    {
                                                                        if (!sPatientExistingEthnicity.ToLower().Contains(Convert.ToString(strPortalethnicity[i]).Trim().ToLower()))
                                                                        {
                                                                            sPatientExistingEthnicity = sPatientExistingEthnicity + "|" + Convert.ToString(strPortalethnicity[i]).Trim();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (sPortalEthnicity != "" && sPortalEthnicity.Length > 0)
                                                            {
                                                                if (!sPatientExistingEthnicity.ToLower().Contains(Convert.ToString(sPortalEthnicity).Trim().ToLower()))
                                                                {
                                                                    sPatientExistingEthnicity = sPatientExistingEthnicity + "|" + Convert.ToString(sPortalEthnicity).Trim();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (string.Compare(sPortalEthnicity, "Declined to Specify", true) == 0)
                                                        {
                                                            sPatientExistingEthnicity = sPortalEthnicity;
                                                        }
                                                        else if (string.Compare(sPatientExistingEthnicity, "Declined to Specify", true) == 0)
                                                        {
                                                            sPatientExistingEthnicity = sPortalEthnicity;
                                                        }
                                                        else if (!sPatientExistingEthnicity.Contains(sPortalEthnicity))
                                                        {
                                                            sPatientExistingEthnicity = sPatientExistingEthnicity + "|" + sPortalEthnicity;
                                                        }
                                                    }

                                                } //else

                                                //check size of new Race
                                                if (sPatientExistingEthnicity.Length > 1000)
                                                {
                                                    IsNotValidData = true;
                                                    MessageBox.Show("Ethnicity should not exceed 1000 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                }

                                                //check is race exist in category master
                                                aEthnicity = _EthnicityList.Split('|');

                                                if (bIsPatientPortalEnabled == true)
                                                {
                                                    // Check Multiple Race Existance in category master
                                                    string strEthnicity = Convert.ToString(sPortalEthnicity).Trim();
                                                    if (strEthnicity != "" && Convert.ToString(strEthnicity).Contains("|"))
                                                    {
                                                        string[] selectedEthnicity = strEthnicity.Split('|');
                                                        if (selectedEthnicity.Length > 0)
                                                        {
                                                            int foundcnt = 0;
                                                            for (int i = 0; i < selectedEthnicity.Length; i++)
                                                            {
                                                                for (int j = 0; j < aEthnicity.Length; j++)
                                                                {
                                                                    if (Convert.ToString(selectedEthnicity[i]).Trim().ToLower() == Convert.ToString(aEthnicity[j]).Trim().ToLower())
                                                                    {
                                                                        foundcnt += 1;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            if (foundcnt != selectedEthnicity.Length)
                                                            {
                                                                IsNotValidData = true;
                                                                MessageBox.Show("select valid race", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                            }
                                                        }
                                                    }
                                                    // Check Single Race Existance in category master
                                                    else if (strEthnicity != "" && strEthnicity.Length > 0)
                                                    {
                                                        bool IsFound = false;
                                                        for (int i = 0; i < aEthnicity.Length; i++)
                                                        {
                                                            if (strEthnicity.ToLower() == Convert.ToString(aEthnicity[i]).Trim().ToLower())
                                                            {
                                                                IsFound = true;
                                                                break;
                                                            }
                                                        }
                                                        if (IsFound != true)
                                                        {
                                                            IsNotValidData = true;
                                                            MessageBox.Show("select valid ethnicity", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IsNotValidData = true;
                                                        MessageBox.Show("select valid ethnicity", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                    }
                                                }
                                                else
                                                {
                                                    result = from r in aEthnicity
                                                             where r.Trim().ToLower() == sPortalEthnicity.ToLower()
                                                             select r;
                                                    if (result.Count() < 1)
                                                    {
                                                        IsNotValidData = true;
                                                        MessageBox.Show("select valid ethnicity", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                                    }
                                                }
                                            } //end of if                                                                                                                                     
                                        }//endfo try
                                        catch (Exception)
                                        { }
                                        finally
                                        {
                                            sPatientExistingEthnicity = string.Empty;
                                            sPortalEthnicity = string.Empty;
                                            //aRace = null;
                                            result = null;
                                        }
                                    }
                                    break;


                            case "saddressline1" :
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                     c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 100)
                                    {
                                        IsNotValidData = true;
                                        MessageBox.Show("Address 1 should not exceed 100 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "saddressline2":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    {
                                        IsNotValidData = true;
                                        MessageBox.Show("Address 2 should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "scity":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                           c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    {
                                        IsNotValidData = true;
                                        MessageBox.Show("City name should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "sstate":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                          c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 50)
                                    {
                                        IsNotValidData = true;
                                        MessageBox.Show("State name should not exceed 50 characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                    }
                                    break;
                            case "szip":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                         c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    {
                                        string sZIP = string.Empty;
                                        sZIP = c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim();
                                        if (!IsValidZip(sZIP))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Invalid ZIP", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                    }
                                    break;    
                            case "scountry" :
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                      c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length>0 )
                                    {
                                        if (!(string.Compare(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), "US", true) == 0 ||
                                        string.Compare(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), "Canada", true) == 0))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Country should be either US or Canada.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                    } 
                                    break;
                            case "sphone":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length >0)
                                    {
                                        if (!System.Text.RegularExpressions.Regex.IsMatch(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), "^[0-9]*$"))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Phone number should be numeric.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                        else if (c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length != 10)
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Phone number should be 10 digits.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);    
                                        }
                                    } 
                                    break;     
                            case"smobile":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    {
                                        if (!System.Text.RegularExpressions.Regex.IsMatch(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), "^[0-9]*$"))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Mobile number should be numeric.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                        }
                                        else if (c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length != 10)
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Mobile number should be 10 digits.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                        }
                                    } 
                                    break;
                            case "sworkphone":
                                    if (Convert.ToBoolean(c1PatientDetails.GetData(nRow, COL_Select)) == true &&
                                        c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length > 0)
                                    {
                                        if (!System.Text.RegularExpressions.Regex.IsMatch(c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim(), "^[0-9]*$"))
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Work phone number should be numeric.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                        }
                                        else if (c1PatientDetails.GetData(nRow, COL_IntuitPatient).ToString().Trim().Length != 10)
                                        {
                                            IsNotValidData = true;
                                            MessageBox.Show("Work phone number should be 10 digits.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1PatientDetails.Select(nRow, COL_IntuitPatient);
                                        }
                                    }
                                    break;
                        }
                        if (IsNotValidData)
                            break; // break for loop if invalid data found.
                    }

	            }
	            catch //(Exception ex)
	            {
		     		//  ex=null;
	            }
                 return IsNotValidData;
             }

             private Boolean IsValidZip(string sZip)
             {
                 Boolean bIsValidZip =false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 try
                 {
                     string sQuery = "select count(zip) from CSZ_MST where ZIP ='" + sZip.Replace("'", "''") + "'";
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     object oCount = oDB.ExecuteScalar_Query(sQuery);                     
                     if (oCount != null && Convert.ToInt64(oCount) > 0)
                     {
                         bIsValidZip = true;
                     }
                     else
                     {
                         bIsValidZip = false;
                     }
                     oDB.Disconnect();
                 }
                 catch //(Exception ex)
                 {
                    // ex = null;
                 }
                 finally
                 {
                     if(oDB!=null)
                     {
                         oDB.Dispose();
                         oDB=null;
                     }
                 }
                 return bIsValidZip;
             }

             private Boolean FillProvider()
             {
                 gloDatabaseLayer.DBLayer oDB = null;
                 DataTable dtProvider = null;
                 try
                 {
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     string _sqlQuery = "SELECT nProviderID, ISNULL(sFirstName,'')+ ' ' +CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When ISNULL(Provider_MST.sMiddleName,'') then " +
                                        " ISNULL(Provider_MST.sMiddleName,'')  END+ ' '+ISNULL(sLastName,'') AS sName From Provider_MST WHERE  bIsblocked='FALSE' order by sFirstName,sMiddleName,sLastName ";
                     oDB.Retrive_Query(_sqlQuery, out dtProvider);
                     oDB.Disconnect();

                     if (dtProvider != null)
                     {
                         DataRow dr = dtProvider.NewRow();
                         dr["nProviderID"] = 0;
                         dr["sName"] = "";
                         dtProvider.Rows.InsertAt(dr, 0);
                         dtProvider.AcceptChanges();

                         cmbProvider.DataSource = dtProvider;
                         cmbProvider.DisplayMember = "sName";
                         cmbProvider.ValueMember = "nProviderID";                        
                     }
                 }
                 catch (gloDatabaseLayer.DBException ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 finally
                 {
                     if (oDB != null) 
                     {
                         oDB.Dispose();
                     }                   
                 }

                 return true;
             }

             private Boolean FillRace()
             {
                 gloDatabaseLayer.DBLayer oDB = null;
                 DataTable dtRace = null;
                 try
                 {
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     string _sqlQuery = "select sdescription from category_mst where scategorytype in ('Race','Race Specification') and sdescription is not null and len(rtrim(ltrim(sdescription)))>0"; 
                     if (_bMultipleRace)  //Added by manoj jadhav on 20130401 for MU2 multiple race changes
                         _sqlQuery = _sqlQuery + " Union Select 'Declined to Specify'"; 
                     oDB.Retrive_Query(_sqlQuery, out dtRace);
                     oDB.Disconnect();

                     string sRace = string.Empty;
                     if (dtRace != null)
                     {
                         IEnumerable<string> result = from r in dtRace.AsEnumerable() select r.Field<string>("sdescription");

                         sRace = string.Join("|", result);
                     }

                     _RaceList = _RaceList + sRace;
                 }
                 catch (gloDatabaseLayer.DBException ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                     }
                     if (dtRace != null)
                     {
                         dtRace.Dispose();
                     }
                 }

                 return true;
             }

             private Boolean FillLanguage()
             {
                 gloDatabaseLayer.DBLayer oDB = null;
                 DataTable dtlanguage = null;
                 try
                 {
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     string _sqlQuery = "select sdescription from category_mst where scategorytype='Language' and sdescription is not null and len(rtrim(ltrim(sdescription)))>0";
                     if (_bMultipleRace)  //Added by manoj jadhav on 20130401 for MU2 multiple race changes
                         _sqlQuery = _sqlQuery + " Union Select 'Declined to Specify'";
                     oDB.Retrive_Query(_sqlQuery, out dtlanguage);
                     oDB.Disconnect();

                     string sLanguage = string.Empty;
                     if (dtlanguage != null)
                     {
                         IEnumerable<string> result = from r in dtlanguage.AsEnumerable() select r.Field<string>("sdescription");

                         sLanguage = string.Join("|", result);
                     }

                     _LanguageList = _LanguageList + sLanguage;
                 }
                 catch (gloDatabaseLayer.DBException ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                     }
                     if (dtlanguage != null)
                     {
                         dtlanguage.Dispose();
                     }
                 }

                 return true;
             }

             private Boolean FillEthnicity()
             {
                 gloDatabaseLayer.DBLayer oDB = null;
                 DataTable dtEthnicity = null;
                 try
                 {
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     string _sqlQuery = "select sdescription from category_mst where scategorytype in ('Ethnicity','Ethnicity Specification') and sdescription is not null and len(rtrim(ltrim(sdescription)))>0";
                     if (_bMultipleRace)  //Added by manoj jadhav on 20130401 for MU2 multiple race changes
                         _sqlQuery = _sqlQuery + " Union Select 'Declined to Specify'";
                     oDB.Retrive_Query(_sqlQuery, out dtEthnicity);
                     oDB.Disconnect();

                     string sEthnicity = string.Empty;
                     if (dtEthnicity != null)
                     {
                         IEnumerable<string> result = from r in dtEthnicity.AsEnumerable() select r.Field<string>("sdescription");

                         sEthnicity = string.Join("|", result);
                     }
                     _EthnicityList = _EthnicityList + sEthnicity;
                 }
                 catch (gloDatabaseLayer.DBException ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                     }
                     if (dtEthnicity != null)
                     {
                         dtEthnicity.Dispose();
                     }
                 }
                 return true;
             }

             /// <summary>
             /// Returns patient portal Enable status from Setting.
             /// </summary>
             /// <returns>IsPatient Portal Enabled true or false</returns>
           
             private Boolean IsPatientPortalEnabled()
             {
                 Boolean IsEnabled = false;
                 gloDatabaseLayer.DBLayer oDB = null;
                 try
                 {
                     string sQuery = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings  WHERE sSettingsName = 'PatientPortalEnabled'";
                     oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                     oDB.Connect(false);
                     object oCount = oDB.ExecuteScalar_Query(sQuery);
                     if (oCount != null && Convert.ToString(oCount) != "" && Convert.ToString(oCount).ToLower() != "false")
                     {
                         IsEnabled = true;
                     }
                     else
                     {
                         IsEnabled = false;
                     }
                     oDB.Disconnect();
                 }
                 catch //(Exception ex)
                 {
                    // ex = null;
                 }
                 finally
                 {
                     if (oDB != null)
                     {
                         oDB.Dispose();
                         oDB = null;
                     }
                 }
                 return IsEnabled;
             }

                   
        /// <summary>
        /// Genrate Message Queue for Custom Forms Download
        /// Added By Manoj Jadhav On 20140509
        /// </summary>
        /// <returns></returns>

        private void GenrateMessageQueue_ForCustomForms(Int64 nPatientID, Int64 nOtherID, string sField1, string sField2)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters=null;
            try
            {
                
                oDBParameters= new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nMessageID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtDateTimeStamp", DateTime.Now , ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@sMachineName", Environment.MachineName , ParameterDirection.Input, SqlDbType.VarChar,50);
                oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nOtherID", nOtherID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nStatus",1, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@sField1", sField1, ParameterDirection.Input, SqlDbType.VarChar,500);
                oDBParameters.Add("@sField2", sField2, ParameterDirection.Input, SqlDbType.VarChar,500);
                oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_FusionInUPHFMsgQ", oDBParameters);
                oDBLayer.Disconnect();                                 
                 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }               
            }

        }

        #endregion "Methods"                         

        #region "Search"
        //Task #67685: Search
        //Added search logic on PatientName column in c1IntuitPatientList.

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // THIS LOGIC WILL START SEARCH AFTER 700 MILLISECONDS // 
                // SEARCH IS IMPLEMENTED ON TIMER TICK //
                _CurrentTime = DateTime.Now;
                oTimer.Stop();
                oTimer.Interval = 700;
                oTimer.Enabled = true;
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // THIS CONDITION WILL OCCURE IF TEXT IS PASTED IN SEARCH BOX //
                if (oTimer.Enabled == false)
                {
                    oTimer.Stop();
                    oTimer.Enabled = true;
                }

                this.Cursor = Cursors.Default;
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

        void oTimer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                // IF LAST KEY PRESS TIME DIFFERENCE IS 100 MILLISECONDS THEN SEARCHING WILL BE START //
                if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    oTimer.Stop();
                    FillPatients();
                }
            }
            else
            {
                oTimer.Stop();
                FillPatients();
            }

        }

        public void FillPatients()
        {
            this.SuspendLayout();

            DataTable dtPatient = null;
            DataView dvPatient = null;
            try
            {

            
                #region "Get patient list and Bind to Datagrid view"
                _IsDataLoaded = false;
                dtPatient = FillIntuitPatientListGrid();
                _IsDataLoaded = false;

                if (dtPatient != null)
                {
                    dvPatient = dtPatient.Copy(). DefaultView;
                   
                    c1IntuitPatientList.DataSource = dvPatient;
                }
                #endregion

                if (c1IntuitPatientList.Cols.Count > 0)
                {
                    if (c1IntuitPatientList.DataSource != null)
                    {
                        ((DataView)c1IntuitPatientList.DataSource).RowFilter = dvPatient.Table.Columns[2].ColumnName + " like '%" + txtSearch.Text.Replace("'", "''").ToString() + "%'";

                        fillPatientgrid((DataView)c1IntuitPatientList.DataSource);
                    }
                }
            }//try
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                if (dvPatient != null)
                {
                    dvPatient.Dispose();
                    dvPatient = null;
                }
                this.ResumeLayout();
            }
        }

        private void fillPatientgrid(DataView dvPatient)
        {
            DataTable _dt = null;
            DataView _dv = null;
            try
            {
                _IsDataLoaded = false;
                DesignIntuitPatientListGrid();
                string sFilterString = string.Empty;

                _dv = dvPatient;
                _dt = _dv.ToTable();
                Boolean bIsAll = true;
                if (rdbtnAll.Checked)
                {
                    bIsAll = true;
                }
                else
                {
                    bIsAll = false;
                }

                if (rdbtnMatched.Checked)
                {
                    sFilterString = "matched";
                }
                else if (rdbtnUnMatched.Checked)
                {
                    sFilterString = "unmatched";
                }


                for (int iRow = 0; iRow <= _dt.Rows.Count - 1; iRow++)
                {
                    // Loop for setting data into grid 
                    if (bIsAll || _dt.Rows[iRow]["PatientType"].ToString().ToLower() == sFilterString)
                    {
                        c1IntuitPatientList.Rows.Add();
                        Int32 _Row = c1IntuitPatientList.Rows.Count - 1;
                        c1IntuitPatientList.SetData(_Row, COL_IntuitTransactionID, _dt.Rows[iRow]["nTransactionID"].ToString());
                        c1IntuitPatientList.SetData(_Row, COL_IntuitMemberID, _dt.Rows[iRow]["nMemberID"].ToString());
                        c1IntuitPatientList.SetData(_Row, COL_IntuitPatientName, _dt.Rows[iRow]["PatientName"].ToString());
                        c1IntuitPatientList.SetData(_Row, COL_IntuitPatientType, _dt.Rows[iRow]["PatientType"].ToString());
                        c1IntuitPatientList.SetData(_Row, COL_IntuitPatientImportedDate, Convert.ToDateTime(_dt.Rows[iRow]["Date"]).ToString("MM/dd/yyyy"));
                    }
                }
                DesignPatientDetailsGrid();
                if (c1IntuitPatientList.Rows.Count > 1)
                {
                    //select top 1 patient from Intuit Patient List
                    c1IntuitPatientList.Select(1, 0);
                    long nTransactionID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitTransactionID));
                    long nMemberID = Convert.ToInt64(c1IntuitPatientList.GetData(c1IntuitPatientList.RowSel, COL_IntuitMemberID));

                    //Loading Details of Intuit Patient List
                    LoadIntuitPatientDetails(nTransactionID, nMemberID);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (_dt!=null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_dv!=null)
                {
                    _dv.Dispose();
                    _dv = null;
                }
                _IsDataLoaded = true;
            }
        }

        private void btnSearchClose_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();

            if (toolTip == null)
            {
                toolTip = new ToolTip();
            }
            toolTip.SetToolTip(this.btnSearchClose, "Clear Search");
        }

        #endregion
    }
}
