using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class frmMapUnMatchPatients : Form
    {
        // This form is created by Abhijeet
        // Purpose : to unmatch patient with existing one this form is used.
        // It display the all patient in grid which are match with unmatch patient information according to HL7
        // patient setting, when search text is blank.
        // when search text available then it display matching patients from patient table.


        // Property to save the patient 
        private long _SelectedPatientId = 0;
        public long SelectedPatientId
        {
            get { return _SelectedPatientId ;}
            set { _SelectedPatientId =value; }                        
        }
          
        // defaines class level variable to save patient details
        //private long _nPatientID = 0;--- Commented to remove warnings.
        private string _sPatientCode="";
        private string _sFirstName = "";
        private string _sLastName = "";
        private DateTime _dtDob ;
        private string _sGender = "";
        private string   _nSSN = "";

        private long _nTaskID = 0;
        //Below code is commeted by madan on 20100520
        //private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;      
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _DBConnectionString = "";        

       // DataTable _dtPatient = new DataTable();
        DataTable _dtSearchPatient = new DataTable();
        DataTable _dtFilterPatient = new DataTable();

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize Search for displaying patient grid.
        Timer oTimer = new Timer();
        DateTime _CurrentTime;

        public frmMapUnMatchPatients(long patientID)
        {
            
            //_nPatientID = patientID;

            _nTaskID = patientID;
            if (appSettings != null)
            {
                _DBConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }
            InitializeComponent();
        }
        

        private void frmMapUnMatchPatients_Load(object sender, EventArgs e)
        {                       
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);            
            try
            {
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "open un match view form to map un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    // Making blanks patient details
                    lblPatientCodeValue.Text="" ;
                    lblPatientNameValue.Text = "";
                    lblProviderNameValue.Text = "";
                    lblPatientDOBValue.Text = "";
                 //   lblPatientGenderValue.Text = "";
                    lblSSNValue.Text = "";
                            

                    SqlDataReader sqlDr;

                    //before filling patient grid Access the details of patient which is to be match              
                    //string strQry = "select a.*,b.sFirstName 'ProvsFirstName',b.sMiddleName 'ProvsMiddleName',b.sLastName 'ProvsLastName' from patient a left outer join provider_mst b on a.nProviderID = b.nProviderID where  a.npatientid=" + _nPatientID.ToString();
                 //   string strQry = "select a.*,b.sFirstName 'ProvsFirstName',b.sMiddleName 'ProvsMiddleName',b.sLastName 'ProvsLastName' from patient a left outer join provider_mst b on a.nProviderID = b.nProviderID where  a.npatientid=" + _nPatientID.ToString();


                    string strQry = "select nPatientID=0,a.sPatientCode,a.sFirstName,a.sMiddleName,a.sLastName, " +
                   "nSSN=case a.nSSN when '0' then '' else isnull(a.nSSN,'') end,convert(varchar,a.dtDOB,101) as dtDOB, a.nProviderID,b.sFirstName 'ProvsFirstName',b.sMiddleName 'ProvsMiddleName',b.sLastName 'ProvsLastName' " +
                   ",a.sGender from PatientsUnmatchedInLab a left outer join  provider_mst b " +
                   "on a.nProviderID=b.nProviderID where a.nTaskId= " + _nTaskID.ToString(); 

                    
                    oDB.Connect(false);
                    oDB.Retrive_Query(strQry, out sqlDr);

                    if (sqlDr.HasRows)
                    {
                        if (sqlDr.Read())
                        {
                           
                            _sPatientCode = Convert.ToString(sqlDr["sPatientCode"]);
                            _sFirstName = Convert.ToString(sqlDr["sFirstName"]);
                            _sLastName = Convert.ToString(sqlDr["sLastName"]);
                            _dtDob = Convert.ToDateTime(sqlDr["dtDOB"]);
                            _sGender = Convert.ToString(sqlDr["sGender"]);
                            _nSSN = Convert.ToString(sqlDr["nSSN"]);

                            string strPtMiddleName = Convert.ToString(sqlDr["sMiddleName"]);
                            string strPrfirstName = sqlDr["ProvsFirstName"] != null ? Convert.ToString(sqlDr["ProvsFirstName"]) : " " ;
                            string strPrmiddleName = sqlDr["ProvsFirstName"] != null ? Convert.ToString(sqlDr["ProvsMiddleName"]) : " ";
                            string strPrlastName = sqlDr["ProvsFirstName"] != null ? Convert.ToString(sqlDr["ProvsLastName"]) : " ";                            

                            lblPatientCodeValue.Text = _sPatientCode;
                            lblPatientNameValue.Text = _sFirstName.Trim() + " " + strPtMiddleName.Trim() + " " + _sLastName.Trim();
                            lblProviderNameValue.Text = strPrfirstName.Trim() + " " + strPrmiddleName.Trim() + " " + strPrlastName.Trim(); ;
                            lblPatientDOBValue.Text = _dtDob.ToShortDateString();
                          //  lblPatientGenderValue.Text = _sGender;
                            lblSSNValue.Text = _nSSN.Trim()=="0" ? " ":_nSSN.Trim();
                        }
                    }
                    sqlDr.Dispose();
                    oDB.Disconnect();

                    txtSearch.Focus();

                    // To design grid and fill grid with all patient entry     

                    // by ABhijeet on date 20100412 ,commented & written new code
                    //DataView dv = new DataView();
                    //dv = GetPatients().DefaultView;

                    DataTable returnDT = new DataTable();
                    returnDT = GetPatients();

                    if (returnDT == null)
                    {
                       // c1PatientList1.Clear();
                        c1PatientList1.DataSource = null;                                                
                        c1PatientList1.Cols.Count = 0;
                    }
                    else
                    {
                        DataView dv = new DataView();
                        dv = returnDT.DefaultView;

                        c1PatientList1.DataSource = dv;
                        c1PatientList1.Select(-1, -1); 
                    }
                    // end of changes by Abhijeet on date 20100412 
                   
                    CustomGridStyle();

                    //Incident #58149: 00019311
                    //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
                    //Optimize Search for displaying patient grid.
                    oTimer.Tick += new EventHandler(oTimer_Tick);

                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Load, "loaded un match view form to map un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                  
            }
            catch(Exception ex)
            {
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog(" Error in Patient Grid Double Click : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Double Click : " + ex.ToString(), false);  
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }                                

        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize Search for displaying patient grid.
        void oTimer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                // IF LAST KEY PRESS TIME DIFFERENCE IS 100 MILLISECONDS THEN SEARCHING WILL BE START //
                if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    oTimer.Stop();
                    searchPatient();

                }
            }
            else
            {
                oTimer.Stop();
                searchPatient();

            }

        }

        private void CustomGridStyle()
        {
            // function used to set the custom style of grid.

            try
            {
                // condition added by manoj on 20121227 for bug no 42444
                if (c1PatientList1.Cols.Count <= 0)
                    return;
                // Assign Captions to patient Grid Columns
                c1PatientList1.Cols["sPatientCode"].Caption = "Code";
                c1PatientList1.Cols["sFirstName"].Caption = "First Name";
                c1PatientList1.Cols["sMiddleName"].Caption = "MI";
                c1PatientList1.Cols["sLastName"].Caption = "Last Name";
                c1PatientList1.Cols["nSSN"].Caption = "SSN";
                c1PatientList1.Cols["dtDOB"].Caption = "DOB";
                c1PatientList1.Cols["sProviderName"].Caption = "Provider Name";
                c1PatientList1.Cols["nPatientID"].Width = 0;
                c1PatientList1.Cols["nProviderID"].Width = 0;

                // setting the width of patient Grid Columns
                c1PatientList1.Cols["sPatientCode"].WidthDisplay = 110;
                c1PatientList1.Cols["sFirstName"].WidthDisplay = 150;
                c1PatientList1.Cols["sMiddleName"].WidthDisplay = 50;
                c1PatientList1.Cols["sLastName"].WidthDisplay = 150;
                c1PatientList1.Cols["nSSN"].WidthDisplay = 110;
                c1PatientList1.Cols["sProviderName"].WidthDisplay = 200;
                c1PatientList1.Cols["dtDOB"].WidthDisplay = 100;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
                throw ex;
            }        
        }
                
        private DataTable GetPatients()
        {
            // It is function used to get the data view of patient according to condition
        
            DataTable dtResultsPatients = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
         
            try
            {               
                string strSql = "";
                string strCondition = "";
                
                string strTop100 = "";
                if (txtSearch.Text.Trim() == "")
                {
                    // when  search is blank then search records according to  HL7 setting and take top 100 in query

                    // take the top 100 records
                    strTop100 = " Top 100 ";
                    // make condition according to  HL7 Setting
                    strCondition = GetHL7SettingCondition();                                      
                }
                else 
                {
                    // when  search is blank then don't consider HL7 setting and don't consider top caluse
                     
                    strTop100 = " ";                    
                    strCondition = " ";
                }              

               // Making the query according to condition
                strSql = "select "+ strTop100 + "a.sPatientCode,a.sFirstName,a.sMiddleName,a.sLastName," +
                        "nSSN=case a.nSSN when '0' then '' else isnull(a.nSSN,'') end,rtrim(b.sFirstName) +' '+ rtrim(b.sMiddleName)+' '+" +
                        "rtrim(b.sLastName) 'sProviderName',convert(varchar,a.dtDOB,101) as dtDOB,a.nPatientID,a.nProviderID from patient a left outer join  provider_mst b " +
                        "on a.nProviderID=b.nProviderID where a.nclinicid >=" + "0  " + strCondition.Trim() + " order by a.sFirstName";
           
                oDB.Connect(false);
                oDB.Retrive_Query(strSql, out dtResultsPatients);

                if (dtResultsPatients != null && dtResultsPatients.Rows.Count>0)
                { return dtResultsPatients; }
                else
                { return null; }   
            
            }
            catch (Exception ex)
            {
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog(" Error in Patient Grid Double Click : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Double Click : " + ex.ToString(), false); 
                return null;
            }
            finally
            {               
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private string GetHL7SettingCondition()
        {
            // it is function Used to obtain condition for  matching patients according to HL7 patient match Settings

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
            gloDatabaseLayer.DBLayer oDBHL7 = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());

            string strCondition = "";                         
            try
            {
                string strExtraxtPatientData = ""; // store extract patient Data setting
                string strPatientDemographicFileds = "";  // store patient Demographic Fields of HL7 setting
                string strSql = "";
               
                
                //Reading Extract patient Data setting from HL7
                clsGeneral objcls = new clsGeneral();
                Int64 nDBId = 0;
                nDBId = objcls.GetDataBaseConnectionIdFromHL7DB();
                objcls.Dispose();
                // strSql = "select * from hl7_settings where sSettingsName='Extract Patient Data' and nDBConnectionID=" + nDBId.ToString(); //Remove select *
                strSql = "select sSettingsValue from hl7_settings where sSettingsName='Extract Patient Data' and nDBConnectionID=" + nDBId.ToString();
                SqlDataReader dr ;

                //Added by Abhijeet on 20101109 for HL7 Hybrid database connection string
                //oDB.Connect(false);
                //oDB.Retrive_Query(strSql, out dr);
                oDBHL7.Connect(false);
                oDBHL7.Retrive_Query(strSql, out dr);
                //end of changes by Abhijeet on 20101109 for HL7 Hybrid database connection string

                if (dr.HasRows)
                {
                    if (dr.Read()) // if setting available
                    {
                        strExtraxtPatientData = Convert.ToString(dr["sSettingsValue"]).ToUpper();

                        if (dr != null)
                        {
                            dr.Close();
                            dr.Dispose();
                        }

                        if (strExtraxtPatientData.Trim().ToUpper() != "PatientCode".ToUpper())
                        {   //if Extreact patient data setting is not "Patient code" then 
                            //access selected demographics fileds

                            clsGeneral objclsgeneral = new clsGeneral();
                            Int64 intDBId = 0;
                            intDBId = objclsgeneral.GetDataBaseConnectionIdFromHL7DB();
                            objclsgeneral.Dispose();
                            //strSql = "select * from hl7_settings where sSettingsName='Patient Demographic Fields' and nDBConnectionID=" + intDBId.ToString(); //Remove select *
                            strSql = "select sSettingsValue from hl7_settings where sSettingsName='Patient Demographic Fields' and nDBConnectionID=" + intDBId.ToString();
                            SqlDataReader sqldr;

                            //Added by Abhijeet on 20101109 for HL7 Hybrid database connection string
                            //oDB.Retrive_Query(strSql, out sqldr);
                            oDBHL7.Retrive_Query(strSql, out sqldr);
                            //end of changes by Abhijeet on 20101109 for HL7 Hybrid database connection string
                            
                            if (sqldr.HasRows)
                            {
                                if (sqldr.Read())
                                {
                                    strPatientDemographicFileds = Convert.ToString(sqldr["sSettingsValue"]);
                                }

                                if(sqldr!=null)
                                {
                                sqldr.Close();
                                sqldr.Dispose();
                                }
                            }
                            else
                            {
                                strPatientDemographicFileds = "";
                            }
                        }
                    }
                }
                else // if setting not available
                {
                    strExtraxtPatientData = "";
                    strExtraxtPatientData = "";
                }

                //if(oDB!=null)
                //{
                //oDB.Disconnect();
                //oDB.Dispose();
                //}

                if (oDBHL7 != null)
                {
                    oDBHL7.Disconnect();
                    oDBHL7.Dispose();
                }

                if (strExtraxtPatientData == "PatientCode".ToUpper()) // if patient code setting
                {
                    strCondition = " spatientcode='" + _sPatientCode.Trim() + "'";
                }
                else if (strExtraxtPatientData == "PatientDemographic".ToUpper()) // if patient Demographic  setting
                {
                    if (strPatientDemographicFileds.Length > 0)
                    {
                        // code for making condition according to selected fields
                        string[] strArr = null;
                        char[] splitchar = { ',' };
                        strArr = strPatientDemographicFileds.Split(splitchar);

                        for (int intcnt = 0; intcnt < strArr.Length - 1; intcnt++)
                        {
                            if (strArr[intcnt].Trim().ToUpper() == "sLastName".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sLastName ='" + _sLastName.Replace("'","''") + "' ";
                                else
                                    strCondition = strCondition + " or a.sLastName ='" + _sLastName.Replace("'","''") + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "dtDOB".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.dtDOB ='" + Convert.ToString(_dtDob) + "' ";
                                else
                                    strCondition = strCondition + " or a.dtDOB ='" + Convert.ToString(_dtDob) + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "sGender".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sGender ='" + _sGender + "' ";
                                else
                                    strCondition = strCondition + " or a.sGender ='" + _sGender + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "sFirstName".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sFirstName ='" + _sFirstName.Replace("'","''") + "' ";
                                else
                                    strCondition = strCondition + " or a.sFirstName ='" + _sFirstName.Replace("'","''") + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "nSSN".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.nSSN ='" + _nSSN.ToString() + "' ";
                                else
                                    strCondition = strCondition + " or a.nSSN ='" + _nSSN.ToString() + "' ";
                        }

                    }

                }
                else if (strExtraxtPatientData == "PatientCodeDemographic".ToUpper()) // if patientcode & Demographic  setting
                {
                    if (strPatientDemographicFileds.Length > 0)
                    {
                        // code for making condition according to selected fields & patiend code

                        // first making condition with selected fileds
                        string[] strArr = null;
                        char[] splitchar = { ',' };
                        strArr = strPatientDemographicFileds.Split(splitchar);

                        for (int intcnt = 0; intcnt < strArr.Length - 1; intcnt++)
                        {
                            if (strArr[intcnt].Trim().ToUpper() == "sLastName".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sLastName ='" + _sLastName.Replace("'","''") + "' ";
                                else
                                    strCondition = strCondition + " or a.sLastName ='" + _sLastName.Replace("'","''") + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "dtDOB".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.dtDOB ='" + Convert.ToString(_dtDob) + "' ";
                                else
                                    strCondition = strCondition + " or a.dtDOB ='" + Convert.ToString(_dtDob) + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "sGender".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sGender ='" + _sGender + "' ";
                                else
                                    strCondition = strCondition + " or a.sGender ='" + _sGender + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "sFirstName".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.sFirstName ='" + _sFirstName.Replace("'","''") + "' ";
                                else
                                    strCondition = strCondition + " or a.sFirstName ='" + _sFirstName.Replace("'","''") + "' ";
                            else if (strArr[intcnt].Trim().ToUpper() == "nSSN".ToUpper())
                                if (strCondition == "")
                                    strCondition = " a.nSSN ='" + _nSSN.ToString() + "' ";
                                else
                                    strCondition = strCondition + " or a.nSSN ='" + _nSSN.ToString() + "' ";
                        }

                    }
                    // Adding Patiend code in condition
                    if (strCondition.Trim() == "")
                        strCondition = " a.sPatientCode ='" + _sPatientCode.ToString() + "' ";
                    else
                        strCondition = strCondition + " or a.sPatientCode ='" + _sPatientCode.ToString() + "' ";
                }
                else // if Setting not available then keep condition  blank
                {
                    strCondition = "";
                }

                if (strCondition != "")
                    strCondition = " and (" + strCondition.Trim() + ")";
          
                return strCondition;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
                strCondition = "";
                throw ex;               
            }
            finally
            {
                //if (oDB != null)
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}

                if (oDBHL7 != null)
                {
                    oDBHL7.Disconnect();
                    oDBHL7.Dispose();
                }
            }                  
        }

           
        private void ts_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == tlbbtn_Close.Name) // if close button clicked
            {
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Did not Selected gloEMR patient to map un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                this.Close();
            }
            if(e.ClickedItem.Name == tlbbtn_SaveAndClose.Name) // if save & close clicked
            {              
                    // code for calling form will get matched selected patient Id back.
                    // After that close the current  form.
                try
                {
                    if (c1PatientList1.RowSel > 0)
                    {
                        long lngPatientID = Convert.ToInt64(c1PatientList1.GetData(c1PatientList1.RowSel, "nPatientID"));
                        SelectedPatientId = lngPatientID;
                        //by Abhijeet on 20100430
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Select, "Selected the gloEMR patient to map un match patients", lngPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                    

                    }
                    else
                    {
                        SelectedPatientId = 0;
                        //by Abhijeet on 20100430
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Did not Selected gloEMR patient to map un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                        
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(" Error during Save & close : " + ex.ToString(), false); 
                    SelectedPatientId = 0;
                    //clsGeneral objclsGen = new clsGeneral();
                    //objclsGen.UpdateLog(" Error during Save & close : " + ex.ToString());
                }                 
            }
        }

                       
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e) // key press on search text box
        {
            //Incident #58149: 00019311
            //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
            //Optimize Search for displaying patient grid.
            //Commented code as done for search patient on dashboard.
            //try
            //{ 
            //      // if more than zero records & enter key press then select grid.
            //    if(c1PatientList1.Rows.Count>0)
            //    {                               
            //        if (e.KeyChar == (char) Keys.Enter )
            //        {
            //            c1PatientList1.Select();
            //        }                              
            //    }                 
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    //clsGeneral objclsGen = new clsGeneral();
            //    //objclsGen.UpdateLog(" Error in Patient Grid Double Click : " + ex.ToString());
            //}                                                                
        }

      
        private void c1PatientList1_DoubleClick(object sender, EventArgs e)
        {           
            try
            {
                // on bouble click of grid record set the property SelectedPatientId so that
                // calling form will get matched selected patient Id back.
                // After that close the form.

                if (c1PatientList1.RowSel > 0)
                {
                    long lngPatientID = Convert.ToInt64(c1PatientList1.GetData(c1PatientList1.RowSel, "nPatientID"));
                    SelectedPatientId = lngPatientID;
                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Select, "Selected the gloEMR patient to map un match patients", lngPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                        
                }
                else
                {
                    SelectedPatientId = 0;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Double Click : " + ex.ToString(), false); 
                SelectedPatientId = 0;
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog(" Error in Patient Grid Double Click : " + ex.ToString());
            }                                    
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Incident #58149: 00019311
            //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
            //Optimize Search for displaying patient grid.
            //Move all code for search patient to new serchPatient().
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

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize Search for displaying patient grid.
        //added new method for serch patient.
        private void searchPatient()
        {
            // on Text changed  filter the records from patient table according to search condition and 
            // display only top 100 records
            // here is code for making the filter condition to be apply on dataview of grid.

            try
            {
                DataView dvPatient = null;
                string[] strSearchArray = null;
                string sFilter = "";
                c1PatientList1.Cursor = Cursors.WaitCursor;

                DataTable dtTemp = GetPatients();

                // by ABhijeet on date 20100412
                if (dtTemp == null)
                {
                    dvPatient = null;
                    c1PatientList1.DataSource = dvPatient;
                    CustomGridStyle();
                    if (dtTemp != null)
                    { 
                        dtTemp.Dispose();
                        dtTemp = null;
                    }
                    c1PatientList1.Cols.Count = 0;
                    return;
                }
                // end of changes by Abhijeet on date 20100412 

                dvPatient = dtTemp.DefaultView;
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                    dtTemp = null;
                }

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
                    // split the search text with comma charatcer  for multiple serach
                    char[] splitchar = { ',' };
                    strSearchArray = strPatientSearch.Split(splitchar);
                }


                if (strPatientSearch.Trim() != "")
                {

                    if (strSearchArray.Length == 1) // single serach 
                    {
                        strPatientSearch = strSearchArray[0].Trim();

                        dvPatient.RowFilter = dvPatient.Table.Columns["sPatientCode"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sFirstName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sMiddleName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sLastName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["nSSN"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["dtDOB"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sProviderName"].ColumnName + " Like '" + strPatientSearch + "%'";
                    }
                    else  // multiple serach separated by comma
                    {
                        for (int loopCnt = 0; loopCnt <= strSearchArray.Length - 1; loopCnt++)
                        {
                            strPatientSearch = strSearchArray[loopCnt].Trim();
                            if (strPatientSearch.Trim() != "")
                            {
                                if (loopCnt == 0)
                                {
                                    sFilter = "";

                                    sFilter = "(" + dvPatient.Table.Columns["sPatientCode"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sFirstName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sMiddleName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sLastName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["nSSN"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["dtDOB"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                             dvPatient.Table.Columns["sProviderName"].ColumnName + " Like '" + strPatientSearch + "%')";

                                }
                                else if (sFilter != "")
                                {
                                    sFilter = sFilter + " AND";

                                    sFilter = sFilter + "(" + dvPatient.Table.Columns["sPatientCode"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["sFirstName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["sMiddleName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["sLastName"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["nSSN"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["dtDOB"].ColumnName + " Like '" + strPatientSearch + "%' or " +
                                           dvPatient.Table.Columns["sProviderName"].ColumnName + " Like '" + strPatientSearch + "%')";

                                }

                            }
                        }

                        dvPatient.RowFilter = sFilter;
                    }

                }
                else
                {
                    dvPatient.RowFilter = "";
                }

                //if (strPatientSearch.Trim() != "")
                //{
                //    // when search is not blank then take top 100 records

                //    // Taking Top 100 records                                            
                //    DataTable TblClone = dvPatient.Table.Clone();
                //    for (int i = 0; i < 100; i++)
                //    {
                //        if (i >= dvPatient.Count)
                //        {
                //            break;
                //        }
                //        TblClone.ImportRow(dvPatient[i].Row);
                //    }
                //    DataView dvSelectedRecord = new DataView(TblClone);
                //    c1PatientList1.DataSource = dvSelectedRecord;
                //} no else found hence commented
                {
                    c1PatientList1.DataSource = dvPatient;
                }
                CustomGridStyle();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in text search : " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog(" Error in text search : " + ex.ToString());
            }
            finally
            {
                c1PatientList1.Cursor = Cursors.Default;
            }
        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize Search for displaying patient grid.
        //add new event to set interval for timer to search patient.
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // THIS LOGIC WILL START SEARCH AFTER 700 MILLISECONDS // 
                // SEARCH IS IMPLEMENTED ON TIMER TICK //
                _CurrentTime = DateTime.Now;
                oTimer.Stop();
                oTimer.Interval = 400;
                oTimer.Enabled = true;


                if (e.KeyCode == Keys.Enter)
                {
                    c1PatientList1.Select();
                }
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }
        }

    }
}