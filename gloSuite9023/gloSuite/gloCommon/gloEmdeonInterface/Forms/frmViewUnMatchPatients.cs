using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloEmdeonInterface.Classes;
using System.IO;
using gloUserControlLibrary;

namespace gloEmdeonInterface.Forms
{
    public partial class frmViewUnMatchPatients : Form
    {
      
        // THis form is created by Abhijeet 
        // Purpose : form shows the tasks list which are created for unmatch patients. & then allow to performs
        // actions like register patient,match patient with existing one,discard patient.after performing
        // action task will get completed.

        
        // Constant variables for Patient Display grid
        private const Int16 COL_PATIENTID = 0;
        private const Int16 COL_PATIENTCODE = 1;
        private const Int16 COL_PATIENTFIRSTNAME = 2;
        private const Int16 COL_PATIENTMIDDLENAME = 3;
        private const Int16 COL_PATIENTLASTNAME = 4;
        private const Int16 COL_PATIENTSSN = 5;
        private const Int16 COL_PATIENTPROVIDERID = 6;
        private const Int16 COL_PATIENTPROVIDERNAME = 7;        
        private const Int16 COL_PATIENTDOB = 8;
        private const Int16 COL_COUNT = 9;

        // Constant variables for Task Display grid
        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        private const Int16 Col_Task_No = 3;
        private const Int16 Col_Task_TaskID = 2;
        private const Int16 Col_Task_Subject = 1;
        private const Int16 Col_Task_DueDate = 0;
        private const Int16 Col_Task_Status = 4;
        private const Int16 Col_Task_Completed = 5;
        private const Int16 Col_Task_Priority = 6;
        private const Int16 Col_Task_TaskDate = 7;
        private const Int16 Col_Task_PatientID = 8;
        private const Int16 Col_Task_Assigned = 9;
        private const Int16 Col_Task_ColCount = 10;

        // class level variables for patient id & Task Id,which are refresh on each 
        //action like match,new patient, discard patienttask select 
        //private long _lngPatientId = 0;-- Commented by madan.. to remove  the warnings... on 20100520
        private long _lngTaskID = 0;
       
        // Variables for saving Task Details
        private long _nOrderID = 0;
        private string _sPatientCode = "";
        private string _sFirstName = "";
        private string _sMiddleName = "";
        private string _sLastName = "";
        private DateTime _dtDOB = default(DateTime);
        private string _nSSN = "";
        private string _SGender = "";
        private long _nProviderID = 0;
        private string _MessageID = "";
        private string _HL7Message = "";

        // by Abhijeet on 20100514 declared variable for message box caption
        string gstrMessageBoxCaption = string.Empty;
            
        // Variable used to save Emr Login ID
        private long _lngLoginID = 0;

        private DataTable dtTasksList = null;

        // enum for task assign status
        enum enumTaskAssignStatus
        {
            Accepted = 1,
            Rejected = 2,
            Hold = 3
        } 
        
        private string _dataBaseConnectionString = "";
        //Bwlow code is commeted by madan on 20100520
        //private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;        
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;        
       
        public frmViewUnMatchPatients(long taskID,long LoginID)
        {
            _lngTaskID = taskID;
            _lngLoginID=LoginID;
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
            InitializeComponent();
        }

         public frmViewUnMatchPatients(long taskID)
        {
            _lngTaskID = taskID;
            _lngLoginID=1;
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
            InitializeComponent();

        }

        public frmViewUnMatchPatients()
        {
            _lngTaskID = 0;
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
            InitializeComponent();
        }

        private void frmViewUnMatchPatients_Load(object sender, EventArgs e)
        {
            //by Abhijeet on 20100430
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "viewed labs un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            //Incident #58149: 00019311
            //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
            //Optimize flow for displaying match patient form.
            LoadData();
           
        }


        private void DesignGrid()
        {
            // function which is used design the grid for patient.
            try
            {
               // c1PatientList.Clear();
                c1PatientList.DataSource = null;
                c1PatientList.Clear();

                // setfont
                c1PatientList.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1PatientList.BackColor = Color.White;
                c1PatientList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;


                c1PatientList.Cols.Count = COL_COUNT;
                c1PatientList.Cols.Fixed = 1;
                c1PatientList.Rows.Count = 1;
                c1PatientList.Rows.Fixed = 1;
                           
                // set visibility of column
                c1PatientList.Cols[COL_PATIENTID].Visible = false;
                c1PatientList.Cols[COL_PATIENTCODE].Visible = true;
                c1PatientList.Cols[COL_PATIENTFIRSTNAME].Visible = true;
                c1PatientList.Cols[COL_PATIENTMIDDLENAME].Visible = true;
                c1PatientList.Cols[COL_PATIENTLASTNAME].Visible = true;
                c1PatientList.Cols[COL_PATIENTSSN].Visible = true;
                c1PatientList.Cols[COL_PATIENTPROVIDERID].Visible = false;
                c1PatientList.Cols[COL_PATIENTPROVIDERNAME].Visible = true;
                c1PatientList.Cols[COL_PATIENTDOB].Visible = true;

             
                // Setting width display for columns
                c1PatientList.Cols[COL_PATIENTCODE].WidthDisplay = 110;
                c1PatientList.Cols[COL_PATIENTFIRSTNAME].WidthDisplay = 150;
                c1PatientList.Cols[COL_PATIENTMIDDLENAME].WidthDisplay = 50;
                c1PatientList.Cols[COL_PATIENTLASTNAME].WidthDisplay = 150;
                c1PatientList.Cols[COL_PATIENTSSN].WidthDisplay = 110;             
                c1PatientList.Cols[COL_PATIENTPROVIDERNAME].WidthDisplay = 200;
                c1PatientList.Cols[COL_PATIENTDOB].WidthDisplay = 100;
                
                ////setting text alignment for a columns
                c1PatientList.Cols[COL_PATIENTCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;              
                c1PatientList.Cols[COL_PATIENTSSN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                c1PatientList.Cols[COL_PATIENTPROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop; 
              

                // set column editing
                c1PatientList.Cols[COL_PATIENTID].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTCODE].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTFIRSTNAME].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTMIDDLENAME].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTLASTNAME].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTSSN].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTPROVIDERID].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTPROVIDERNAME].AllowEditing = false;
                c1PatientList.Cols[COL_PATIENTDOB].AllowEditing = false;


                //set Heading
                c1PatientList.SetData(0, COL_PATIENTID, "Patient ID");
                c1PatientList.SetData(0, COL_PATIENTCODE, "Code");
                c1PatientList.SetData(0, COL_PATIENTFIRSTNAME, "First Name");
                c1PatientList.SetData(0, COL_PATIENTMIDDLENAME, "MI");
                c1PatientList.SetData(0, COL_PATIENTLASTNAME, "Last Name");
                c1PatientList.SetData(0, COL_PATIENTSSN, "SSN");
                c1PatientList.SetData(0, COL_PATIENTPROVIDERID, "Provider ID");
                c1PatientList.SetData(0, COL_PATIENTPROVIDERNAME, "Provider");
                c1PatientList.SetData(0, COL_PATIENTDOB, "DOB");

                c1PatientList.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Designing Grid : " + ex.ToString()); //20100504
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error In Designing Grid " + ex.ToString(), false); 
            }
        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        private void FillPatientsGrid(Int64 TaskID)
        {    
            DataTable dtPatient = new DataTable();
            try
            {
                // design the gird for showing patient Details
                DesignGrid();
                
                // Getting Orders for Patient
                dtPatient = GetPatients(TaskID);

                for (int iRow = 0; iRow <= dtPatient.Rows.Count - 1; iRow++)
                {
                    // Loop for setting data into grid 

                    c1PatientList.Rows.Add();
                    Int32 _Row = c1PatientList.Rows.Count - 1;

                    c1PatientList.SetData(_Row, COL_PATIENTID, dtPatient.Rows[iRow]["nPatientID"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTCODE, dtPatient.Rows[iRow]["sPatientCode"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTFIRSTNAME, dtPatient.Rows[iRow]["sFirstName"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTMIDDLENAME, dtPatient.Rows[iRow]["sMiddleName"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTLASTNAME, dtPatient.Rows[iRow]["sLastName"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTSSN, dtPatient.Rows[iRow]["nSSN"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTPROVIDERID, dtPatient.Rows[iRow]["nProviderID"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTPROVIDERNAME, dtPatient.Rows[iRow]["sProviderName"].ToString());
                    c1PatientList.SetData(_Row, COL_PATIENTDOB, dtPatient.Rows[iRow]["dtDOB"].ToString());
                }

            }
            catch (Exception ex)
            {
                // gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                // obj.UpdateLog(" Error In Designing Grid " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error In Designing Grid " + ex.ToString(), false);
            }
            finally
            {
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                }
            }                               
        }

        private DataTable GetPatients(Int64 TaskID)
        {
            DataTable dtPatient;         
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {                                          
                //string strSql = "select a.nPatientID,a.sPatientCode,a.sFirstName,a.sMiddleName,a.sLastName," +
                //       "a.nSSN,convert(varchar,a.dtDOB,101) as dtDOB, a.nProviderID,rtrim(b.sFirstName) +' '+ rtrim(b.sMiddleName)+' '+" +
                //        "rtrim(b.sLastName) 'sProviderName' from patient a left outer join  provider_mst b "+
                //        "on a.nProviderID=b.nProviderID where a.npatientid="+_lngPatientId.ToString();     //a.nclinicid="+"-1";           

                // change Query by Abhijeet on 20100417 for taking SSN with format .
                string strSql = "select nPatientID=0,a.sPatientCode,a.sFirstName,a.sMiddleName,a.sLastName, " +
                "nSSN=case a.nSSN when '0' then '' else isnull(a.nSSN,'') end,convert(varchar,a.dtDOB,101) as dtDOB, a.nProviderID,rtrim(b.sFirstName) +' '+ rtrim(b.sMiddleName)+' '+ " +
                "rtrim(b.sLastName) 'sProviderName' from PatientsUnmatchedInLab a left outer join  provider_mst b " +
                "on a.nProviderID=b.nProviderID where a.nTaskId= " + Convert.ToString(TaskID); 


                oDB.Connect(false);
                oDB.Retrive_Query(strSql, out dtPatient);
                oDB.Disconnect();
                return dtPatient;                
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error In Designing Grid " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error In Designing Grid " + ex.ToString(), false);
                return null;
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
      

        private void ts_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {           
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            
            try
            {
                if (e.ClickedItem.Name == tlbbtn_RegisterPatient.Name || e.ClickedItem.Name == tlbbtn_MatchPatient.Name || e.ClickedItem.Name == tlbbtn_DiscardPatient.Name)
                { 
   
                   //checking Patient selected or not

                   if (c1PatientList.Rows.Count < 2)
                   {
                       MessageBox.Show("No patient available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                       return;
                   }
                   if (c1PatientList.RowSel <1 )
                    {
                        MessageBox.Show("Please select patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                                                       
               }
                                
                if (e.ClickedItem.Name == tlbbtn_RegisterPatient.Name) // Registrering New patient
                {
                    //// Added by Abhijeet on 20100621
                    //if (MessageBox.Show("Are you sure to resgister this patient?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    return;
                    //}
                    
                    // Code for registering un match patient 
                    RegisterAcceptNewPatient();                                   
                   
                }
                else if (e.ClickedItem.Name == tlbbtn_MatchPatient.Name) // Matching  Patient
                {

                    clsGeneral objcls = new clsGeneral();
                    Int64 nDBId = 0;
                    nDBId = objcls.GetDataBaseConnectionIdFromHL7DB();
                    objcls.Dispose();

                    if (nDBId == 0)
                    {
                        MessageBox.Show("gloEMR database connection ID not found in HL7 database.Please add gloEMR database in HL7 Database setting. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Code for mapping un match patient with gloEMR patient
                        MatchPatient();   
                    }                                    
                       
                }
                else if (e.ClickedItem.Name == tlbbtn_DiscardPatient.Name) // Discarding the Patient
                {
                    //// Added by Abhijeet on 20100621
                    //if (MessageBox.Show("Are you sure to discard this lab results?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    return;
                    //}

                    // Code for discarding patient
                    DiscardLabReport();                                                                                                    
                    
                }
                else if (e.ClickedItem.Name == tlbbtn_Close.Name) // Closing the form
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog(" Error in menu item clicked : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in menu item clicked : " + ex.ToString(), false);
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

        private Boolean ConfirmPatientMatching(long PatientIdToBeMatch,long PatientIdSelectedToMatch)
        {   // function used to get patient match confirmation from user 
           //Madan added on 20100520 for fixing the warnings.
            bool _blnResult = false;
            try
            {
                FrmMatchConfirmation objfrmMatchConfirmation = new FrmMatchConfirmation(PatientIdToBeMatch, PatientIdSelectedToMatch, _dataBaseConnectionString);
                objfrmMatchConfirmation.ShowDialog(this);                
                if (objfrmMatchConfirmation.MatchPatient)
                {
                    _blnResult = true;
                    //return true;
                }
                else
                {
                    _blnResult = false;
                    //return false;
                }
                objfrmMatchConfirmation.Close();
                objfrmMatchConfirmation.Dispose();
                objfrmMatchConfirmation = null;
                                          
            }
            catch (Exception ex)
            {
                // clsGeneral objclsGen = new clsGeneral();
                // objclsGen.UpdateLog(" Error in confirmation for matching patient : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in confirmation for matching patient : " + ex.ToString(), false);
                return false;
            }
            finally
            {
                
            }
            return _blnResult;    
        }


        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        public void LoadData()
        {
            DataTable dtTask = default(DataTable);//= new DataTable();

            //C1UserTasks.RowColChange -= new EventHandler(C1UserTasks_RowColChange);

            try
            {
                C1UserTasks.Redraw = false;
                this.Cursor = Cursors.WaitCursor;

                #region "Design grid"
               // C1UserTasks.Clear();
                C1UserTasks.DataSource = null;
                C1UserTasks.Cols.Count = Col_Task_ColCount;
                C1UserTasks.Cols.Fixed = 0;
                C1UserTasks.Rows.Count = 1;

                C1.Win.C1FlexGrid.CellStyle csProvider;// = C1UserTasks.Styles.Add("cs_Parent");
                try
                {
                    if (C1UserTasks.Styles.Contains("cs_Parent"))
                    {
                        csProvider = C1UserTasks.Styles["cs_Parent"];
                    }
                    else
                    {
                        csProvider = C1UserTasks.Styles.Add("cs_Parent");
                        csProvider.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte((0))));
                        //New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)));
                        csProvider.BackColor = Color.FromArgb(222, 231, 250);
                        csProvider.ForeColor = Color.FromArgb(21, 66, 139);
                        csProvider.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                        csProvider.ImageSpacing = 2;
                        csProvider.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                        csProvider.Border.Color = Color.FromArgb(159, 181, 221);
                    }

                }
                catch
                {
                    csProvider = C1UserTasks.Styles.Add("cs_Parent");
                    csProvider.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte((0))));
                    //New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)));
                    csProvider.BackColor = Color.FromArgb(222, 231, 250);
                    csProvider.ForeColor = Color.FromArgb(21, 66, 139);
                    csProvider.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                    csProvider.ImageSpacing = 2;
                    csProvider.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    csProvider.Border.Color = Color.FromArgb(159, 181, 221);
                }
              

                C1.Win.C1FlexGrid.CellStyle csAppointment1;// = C1UserTasks.Styles.Add("cs_Child_Bold");
                try
                {
                    if (C1UserTasks.Styles.Contains("cs_Child_Bold"))
                    {
                        csAppointment1 = C1UserTasks.Styles["cs_Child_Bold"];
                    }
                    else
                    {
                        csAppointment1 = C1UserTasks.Styles.Add("cs_Child_Bold");
                        csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte((0))));
                        //New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                        csAppointment1.BackColor = Color.FromArgb(240, 247, 255);
                        csAppointment1.ForeColor = Color.Black;
                        csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                        csAppointment1.ImageSpacing = 2;
                        csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    }

                }
                catch
                {
                    csAppointment1 = C1UserTasks.Styles.Add("cs_Child_Bold");
                    csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte((0))));
                    //New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    csAppointment1.BackColor = Color.FromArgb(240, 247, 255);
                    csAppointment1.ForeColor = Color.Black;
                    csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                    csAppointment1.ImageSpacing = 2;
                    csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                }
          

                C1.Win.C1FlexGrid.CellStyle csAppointment ;//= C1UserTasks.Styles.Add("cs_Child");
                try
                {
                    if (C1UserTasks.Styles.Contains("cs_Child"))
                    {
                        csAppointment = C1UserTasks.Styles["cs_Child"];
                    }
                    else
                    {
                        csAppointment = C1UserTasks.Styles.Add("cs_Child");
                        csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL;// new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csAppointment.BackColor = Color.FromArgb(240, 247, 255);
                        csAppointment.ForeColor = Color.Black;
                        csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                        csAppointment.ImageSpacing = 2;
                        csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    }

                }
                catch
                {
                    csAppointment = C1UserTasks.Styles.Add("cs_Child");
                    csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL;// new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csAppointment.BackColor = Color.FromArgb(240, 247, 255);
                    csAppointment.ForeColor = Color.Black;
                    csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                    csAppointment.ImageSpacing = 2;
                    csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                }
               

                C1UserTasks.Tree.Column = 1;
                C1UserTasks.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                C1UserTasks.Tree.Indent = 20;
                C1UserTasks.Tree.LineColor = Color.Transparent;

                C1UserTasks.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
                C1UserTasks.Rows[0].Visible = false;

                #endregion

                #region "Bind the Grid"

                gloTaskMail.gloTask ogloTasks = new gloTaskMail.gloTask(_dataBaseConnectionString);
                dtTask = ogloTasks.GetUnFinishedUnmatchedTasks(_lngLoginID);

                ogloTasks.Dispose();
                ogloTasks = null;

                C1UserTasks.DataSource = dtTask;

                dtTasksList = dtTask;
                #endregion

                #region "Setup a column display"

                C1UserTasks.Rows[0].Visible = false;
                C1UserTasks.Cols[Col_Task_TaskID].Visible = false;
                C1UserTasks.Cols[Col_Task_No].Visible = false;
                C1UserTasks.Cols[Col_Task_Status].Visible = false;
                C1UserTasks.Cols[Col_Task_Completed].Visible = false;
                C1UserTasks.Cols[Col_Task_TaskDate].Visible = false;
                C1UserTasks.Cols[Col_Task_PatientID].Visible = false;
                C1UserTasks.Cols[Col_Task_Assigned].Visible = false;
                C1UserTasks.Cols[Col_Task_ColCount].Visible = false;

                int _width = pnlTask.Width - 2; // C1UserTasks.Width - 2;

                C1UserTasks.Cols[Col_Task_Subject].Width = Convert.ToInt32(_width - 36);
                //'Subject
                C1UserTasks.Cols[Col_Task_Completed].Width = Convert.ToInt32(_width * 0);
                //'% Completed Hidden
                C1UserTasks.Cols[Col_Task_Priority].Width = 20;

                _width = 0;

                C1UserTasks.Cols[0].Visible = false;
                C1UserTasks.AllowEditing = false;

                //'// build the outline
                dynamic total = C1UserTasks.Cols[Col_Task_Priority].Index;
                C1UserTasks.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Clear);
                C1UserTasks.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.None, 0, 0, 0, "{0}");

                #endregion

                #region "Binding an Images to the grid "


                for (int i = 0; i < C1UserTasks.Rows.Count; i++)
                {
                    C1UserTasks.Rows[i].Height = 21;
                    if (C1UserTasks.Rows[i].IsNode)
                    {
                        C1UserTasks.Rows[i].ImageAndText = true;
                        C1UserTasks.Rows[i].Node.Image = imgList_Common.Images[13];
                        C1UserTasks.Rows[i].Node.Level = 0;
                        C1UserTasks.Rows[i].Node.Row.Style = C1UserTasks.Styles["cs_Parent"];
                        C1UserTasks.Rows[i].Node.Data = C1UserTasks.Rows[i + 1][Col_Task_DueDate].ToString();
                    }
                    else
                    {
                        #region "Default Row Selection "

                        if (i > 0)
                        {
                            Int64 _taskId = Convert.ToInt64(C1UserTasks.GetData(i, 2));
                            if (_taskId == _lngTaskID)
                            {
                                C1UserTasks.Select(i, Col_Task_TaskID);

                            }
                            //if (_taskId == _lngTaskID)
                            //{
                            //    // Selects the unmatch task which was clicked from dashboard
                            //    C1UserTasks.Select(i, Col_Task_TaskID);

                            //    // Load the selected task patient details on the right side grid.
                            //    LoadSelectedPatient(_taskId);
                            //}
                            //else
                            //{
                            //    if ((i == 2 && _lngTaskID == 0))
                            //    {
                            //        // Selects the unmatch task which was clicked from dashboard
                            //        C1UserTasks.Select(i, Col_Task_TaskID);
                            //        if (Convert.ToString(C1UserTasks.GetData(i, Col_Task_Status)) == "Not Yet Accepted")
                            //        {
                            //            // Load the selected task patient details on the right side grid.
                            //            LoadSelectedPatient(_lngTaskID);
                            //        }
                            //        else
                            //        {
                            //            // Load the selected task patient details on the right side grid.
                            //            LoadSelectedPatient(_taskId);
                            //        }
                            //        //// Load the selected task patient details on the right side grid.
                            //        //LoadSelectedPatient(_taskId);
                            //    }
                            //}
                        }

                        #endregion

                        C1UserTasks.Cols[Col_Task_Priority].ImageAndText = false;
                        //C1UserTasks.Rows[i].ImageAndText = true;
                        if (Convert.ToString(C1UserTasks.GetData(i, Col_Task_Status)) == "Not Yet Accepted")
                        {
                            C1UserTasks.Rows[i].Style = C1UserTasks.Styles["cs_Child_Bold"];
                        }
                        else
                        {
                            C1UserTasks.Rows[i].Style = C1UserTasks.Styles["cs_Child"];
                        }

                        switch (C1UserTasks.Rows[i][Col_Task_Priority].ToString())
                        {
                            case "High Priority":
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[20]);
                                //C1UserTasks.SetData(i, Col_Task_Priority, "High Priority");
                                break;
                            case "Low Priority":
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[19]);
                                //C1UserTasks.SetData(i, Col_Task_Priority, "Low Priority");
                                break;
                            default:
                                //' Default Normal 
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[1]);
                                //C1UserTasks.SetData(i, Col_Task_Priority, "Normal Priority");
                                break;
                        }
                    }
                }

                LoadSelectedPatient();

                #endregion

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchTasksList();
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Load, "Load Labs un match patients tasks", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
            catch (Exception ex)
            {
                gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                obj.UpdateLog("Error during show task " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                C1UserTasks.Redraw = true;
                this.Cursor = Cursors.Default;
                C1UserTasks.ScrollBars = ScrollBars.Both;
                //C1UserTasks.RowColChange += new EventHandler(C1UserTasks_RowColChange);
            }
        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        // displaying patient details for which task is selected
        private void LoadSelectedPatient()
        {
            Int64 TaskID = 0;
            //Bug #62552: gloEMR: Lab - Unmatched patient task - Application is showing an exception in log file once we accept the unmatched task.
            //Check for row count >1.
            if (C1UserTasks.Rows.Count > 1)
            {
                TaskID = Convert.ToInt64(C1UserTasks.GetData(C1UserTasks.Row, 2));
            }
            
                
            if (TaskID != 0)
            {
                {
                    int intTaskStatus = GetTaskStatus(TaskID);
                    Int64 intNewTaskID = 0;

                    if (intTaskStatus != 1)
                    {
                        DialogResult diagResult;
                        diagResult = MessageBox.Show("Would you like to continue by accepting task.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (diagResult == DialogResult.Yes)
                        {
                            intNewTaskID = AcceptTask(TaskID);

                            if (intNewTaskID != 0)
                            {
                                TaskID = intNewTaskID;
                                C1UserTasks.SetData(C1UserTasks.RowSel, Col_Task_TaskID, intNewTaskID);
                                C1UserTasks.Rows[C1UserTasks.RowSel].Style = C1UserTasks.Styles["cs_Child"];
                            }
                        }
                        else
                        { TaskID = 0; }
                    }
                }
            }

            if (C1UserTasks.Rows.Count > 1)
            {
                FillPatientsGrid(TaskID);
                //LoadData();
            }
            else
            {
             //   c1PatientList.Clear();
                c1PatientList.DataSource = null;
                c1PatientList.Clear();
                c1PatientList.Rows.Count = 1;
                //Bug #62552: gloEMR: Lab - Unmatched patient task - Application is showing an exception in log file once we accept the unmatched task.
                //close the form if last task is match.
                this.Close();
            }
        }

        public DataTable GetPatientCodeAndName(Int64 TaskID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                SqlConnection Con = new SqlConnection(_dataBaseConnectionString);
                // by Abhijeet On date 20100415
                //string strQuery = "SELECT sPatientCode As PatientCode, ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,'') As PatientName, ISNULL(sFirstName,'') As FirstName , ISNULL(sMiddleName,'') As MiddleName, ISNULL(sLastName,'') As LastName FROM Patient WHERE nPatientID = " + PatientID.ToString();
                string strQuery = "SELECT sPatientCode As PatientCode, ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,'') As PatientName, ISNULL(sFirstName,'') As FirstName , ISNULL(sMiddleName,'') As MiddleName, ISNULL(sLastName,'') As LastName FROM PatientsUnmatchedInLab WHERE ntaskID = " + TaskID.ToString();
                // end changes by Abhijeet On date 20100415

                //SqlCommand cmd = new SqlCommand(Query, Con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();

                oDB.Connect(false);
                oDB.Retrive_Query(strQuery,out dt);
                oDB.Disconnect();
              //  da.Fill(dt);
                if ((dt != null))
                {
                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in getting patient code and name " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in getting patient code and name " + ex.ToString(), false); 
                return null;
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

        private void C1UserTasks_DoubleClick(object sender, EventArgs e)
        {
              
                     
        }

        private long GetPatientID(long taskID)
        {
            try
            {                
                long lngPatientId = 0;
             
                gloTaskMail.Task oTempTask = new gloTaskMail.Task();
                gloTaskMail.gloTask ogloTasks = new gloTaskMail.gloTask(_dataBaseConnectionString);
                oTempTask = ogloTasks.GetTask(taskID);

                lngPatientId = oTempTask.PatientID;                
                return lngPatientId;
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error In Designing Grid " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error In Designing Grid " + ex.ToString(), false);
                return 0;
            }                       

        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        private void C1UserTasks_Click(object sender, EventArgs e)
        {
            LoadSelectedPatient();                                              
        }


        public long AcceptTask(long taskID)
        {            
            gloTaskMail.gloTask ogloTask = new gloTaskMail.gloTask(_dataBaseConnectionString);
            //Commented the below code to remove warnings... by madan on 20100520
           // Int64 lngTaskID = default(Int64);
            Int64 TaskFromID=0; 
            gloTaskMail.Task oTask = new gloTaskMail.Task();
            gloTaskMail.TaskAssign oTaskAssign = new gloTaskMail.TaskAssign();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                oTask = ogloTask.GetTask(taskID);
               TaskFromID=Convert.ToInt64(oTask.OwnerID);  
                //If the User Accepts the Task -New Entry for the Task is made 
                //against the user. 
                //So we get the assigned Task first & then set its TaskID =0, 
                //also change the OwnerID to current userID & some other fields 
                //as done below 

                if ((oTask == null) == false)
                {
                    //Clear the previous assignments for this Task 
                    oTask.Assignment.Clear();
                    oTask.TaskID = 0;
                    oTask.UserID = _lngLoginID;
                    //Also set the Current User who is accepting this task 
                    //as the owner of the task. 
                    oTask.OwnerID = _lngLoginID;

                    oTaskAssign.TaskID = 0;
                    oTaskAssign.AssignToID = _lngLoginID;
                    oTaskAssign.AssignFromID = TaskFromID;
                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    oTask.Assignment.Add(oTaskAssign);
                }

                //New Entry for Task. 
                Int64 _result = ogloTask.Add(oTask);

                //To Tell Task Sender That Task Has Accepted. 
                if (_result > 0)
                {
                    if (ogloTask.AcceptTask(taskID, _lngLoginID))
                    {                        
                        //Activate Reminder for this Task if reminder is present 
                        //gloReminder.Reminder oReminder = new gloReminder.Reminder();
                        //oReminder.ActivateTaskReminder(gnLoginID, tempTaskID, _result);
                    }


                    // Replace New one Old task id with new one in PatientsUnmatchedInLab table
                    string strQry = "Update PatientsUnmatchedInLab set nTaskID=" + _result.ToString() + " where nTaskID='" + taskID.ToString() + "'";
                    oDB.Connect(false);
                    oDB.Execute_Query(strQry);
                    oDB.Disconnect();

                    return _result;
                }
                else
                {
                    MessageBox.Show("ERROR : Record not added. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                                
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in accepting task " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in accepting task " + ex.ToString(), false); 
                return 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
              
                if(ogloTask!=null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
                if(oTask!=null)
                {
                    oTask.Dispose();
                    oTask = null;
                }
                if(oTaskAssign!=null)
                {
                    oTaskAssign.Dispose();
                    oTaskAssign = null;
                }

            } 
        }


        public void RejectTask(long taskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                string strQry = "update TM_Task_Assign set nAcceptRejectHold=2 where nTaskID=" + taskId.ToString();
                oDB.Connect(false);
                oDB.Execute_Query(strQry);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error during show task " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error during show task " + ex.ToString(), false);
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

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        //private void RefreshTaskDetails()
        //{
                      
        //   try
        //    {
            
        //       if (C1UserTasks.Row > 0)
        //       {
        //           //if it is chield Node (Task Node) 
        //          if (C1UserTasks.Rows[C1UserTasks.Row].Node.Children == 0)
        //          {
        //             Int64 _taskId = 0;
        //             Int64 intNewTaskID = 0;
        //             _taskId = (Int64)C1UserTasks.GetData(C1UserTasks.Row, 1);

     
        //             Int32 intTaskStatus = 0;
        //             intTaskStatus = GetTaskStatus(_taskId);
        //                if (intTaskStatus != 1)
        //                {
        //                    DialogResult diagResult;
        //                    //diagResult = MessageBox.Show("Task is unaccepted." + "\n \n" +
        //                    //                " To accept, click Yes. \n To reject, click No. "
        //                    //                , "gloEMR", MessageBoxButtons.YesNoCancel,
        //                    //                MessageBoxIcon.Question);

        //                    diagResult = MessageBox.Show("Would you like to continue by accepting task.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //                    if (diagResult == DialogResult.Yes)
        //                    {
        //                        intNewTaskID = AcceptTask(_taskId);                                
        //                    }
        //                    //else if (diagResult == DialogResult.No)
        //                    //{
        //                    //    RejectTask(_taskId);
        //                    //    intNewTaskID = 0;
        //                    //}

        //                }

        //                if (intNewTaskID != 0)
        //                    _taskId = intNewTaskID;

        //          // Refreshing the Patients & tasks on form

        //                _lngTaskID = _taskId;
        //                LoadData();    
        //        }
        //     }
        //   }
        //   catch (Exception ex)
        //   {

        //       //Int32 intNewTaskID = 0; //Commented the below line to remove warnings. by madan on 20100520
        //       //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
        //       //obj.UpdateLog(" Error on task click " + ex.ToString());
        //       gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in refreshing task details " + ex.ToString(), false); 
              

        //  }
        //}


        private Int32 GetTaskStatus(long taskID)
        {
            // function find & returns the status of a Task (1=Accepted,2=Rejected,3=Hold)

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                string strQry = "Select nAcceptRejectHold from TM_Task_Assign where nTaskID=" + taskID.ToString();
                Int32 intTaskStatus = 0;
                oDB.Connect(false);
                intTaskStatus = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry));
                oDB.Disconnect();
                return intTaskStatus;
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in get task status " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in get task status " + ex.ToString(), false);                 

                return 0;
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

        private void C1UserTasks_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)Keys.Down || e.KeyChar == (char)Keys.Up)
            //    return;

        }

        private void C1UserTasks_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
          //  RefreshTaskDetails();   
        }

        private void C1UserTasks_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
           // RefreshTaskDetails();
        }

        private void MatchPatient()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            //Added by Abhijeet on 20101109 for HL7 hybrid database connection string
            gloDatabaseLayer.DBLayer oDBHL7 = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());
            //End of changes by Abhijeet on 20101109 for HL7 hybrid database connection string
            try
            {
                Int64 taskId = 0;
                if (C1UserTasks.Row > 0)
                {
                    //if it is chield Node (Task Node) 
                    if (C1UserTasks.Rows[C1UserTasks.Row].Node.Children == 0)
                    {
                        //Incident #58149: 00019311
                        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
                        //Optimize flow for displaying match patient form.
                        taskId = Convert.ToInt64(C1UserTasks.GetData(C1UserTasks.Row, 2));
                    }
                }
                else
                {
                    MessageBox.Show("No task available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GetTaskStatus(taskId) != 1) // get status of the Task
                {
                    MessageBox.Show("Task is not accepted. You can not proceed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "View patient to map un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);


                // long lngSelectedPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, COL_PATIENTID));

                // Calling form to match patient
                //frmMapUnMatchPatients objfrmMapUnMatchPatients = new frmMapUnMatchPatients(lngSelectedPatientID);
                frmMapUnMatchPatients objfrmMapUnMatchPatients = new frmMapUnMatchPatients(taskId);

                objfrmMapUnMatchPatients.ShowDialog(this);

                long lngMappedPatientId = objfrmMapUnMatchPatients.SelectedPatientId;

                if (lngMappedPatientId != 0) //if patient selected for matching
                {
                    Boolean boolIsConfirm = false;
                    // get confirmation from user
                    //boolIsConfirm = ConfirmPatientMatching(lngSelectedPatientID, lngMappedPatientId);
                    boolIsConfirm = ConfirmPatientMatching(taskId, lngMappedPatientId);

                    if (boolIsConfirm) // confirmed
                    {
                        //Check similar unprocessed tasks for same patient
                        DataTable dtTasks = new DataTable();
                        List<long> TaskIds = new List<long>();
                        TaskIds.Add(taskId);

                        dtTasks = GetUnmatchedTasksHavingSameDemographics(taskId, _lngLoginID);

                        if (dtTasks != null && dtTasks.Rows.Count > 1)
                        {
                            DialogResult oResult = MessageBox.Show("Identified " + (dtTasks.Rows.Count - 1) + " other unmatched results with same demographics for the lab result you are matching now." + Environment.NewLine + "Do you want to process all identified unmatched results?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                            if (oResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                TaskIds.Clear();

                                foreach (DataRow drRow in dtTasks.Rows)
                                {
                                    TaskIds.Add(Convert.ToInt64(drRow["nTaskId"]));
                                }
                            }
                            else if (oResult == System.Windows.Forms.DialogResult.Cancel)
                            {
                                return;
                            }
                        }

                        foreach (var ntaskid in TaskIds)
                        {
                            taskId = ntaskid;

                            // Take TasK Details
                            GetTaskDetails(taskId);

                            oDB.Connect(false);

                            string sqlQry = "";
                            sqlQry = "update PatientsUnmatchedInLab set nStatusUnmatched=2,sStatusUnmatchedDesc='Match Patient',nMappedPatientID=" + lngMappedPatientId.ToString() + ",Processed=1 where nTaskID=" + taskId.ToString();
                            oDB.Execute_Query(sqlQry);

                            //** Changes for Order ID conflict ***//
                            //// string sqlQry = "update Lab_order_mst set labom_patientID =" + lngMappedPatientId.ToString() + " where labom_patientID=" + lngSelectedPatientID.ToString();
                            //// string sqlQry = "update Lab_order_mst set labom_patientID =" + lngMappedPatientId.ToString() + " where labom_OrderID=" + _nOrderID.ToString();
                            //// oDB.Execute_Query(sqlQry);

                            sqlQry = "select count(*) from hl7_hl7data where MessageID='" + _MessageID + "'";

                            //Added by Abhijeet on 20101109 for HL7 Hybrid datanase connection string
                            oDBHL7.Connect(false);
                            //object objTmp = oDB.ExecuteScalar_Query(sqlQry);
                            object objTmp = oDBHL7.ExecuteScalar_Query(sqlQry);
                            //End of changes by Abhijeet on 20101109 for HL7 Hybrid datanase connection string

                            if (Convert.ToInt64(objTmp) > 0)
                            { // Update processed status to Zero for reprocess the message in HL7_HL7Data table

                                sqlQry = "update HL7_HL7Data set processed=0 where MessageID='" + _MessageID + "'";

                                //Added by Abhijeet on 20101109 for HL7 Hybrid datanase connection string
                                //oDB.Execute_Query(sqlQry);
                                oDBHL7.Execute_Query(sqlQry);
                                //End of changes by Abhijeet on 20101109 for HL7 Hybrid datanase connection string
                            }
                            else
                            { // Create HL7 file

                                //by Abhijeet on 20100430
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Start to Reprocess HL7 file after un match patient registration", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                                string strFileName = lngMappedPatientId.ToString() + " " + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".hl7"; // DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".hl7";
                                string strFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon";

                                //Added by Abhijeet on 20110511 to remove hardcoded CLC path for reprocessing of lab file
                                //string strDestinationPath = "C:\\Program Files\\Clinician\\Local Client Service\\local\\local_result\\archive";
                                string strDestinationPath = string.Empty;
                               clsGeneral objClsGeneral = new clsGeneral();
                                long nDBID = 0;
                                nDBID = objClsGeneral.GetDataBaseConnectionIdFromHL7DB();

                                if (nDBID > 0)
                                {
                                    strDestinationPath = objClsGeneral.GetHL7Setting(nDBID);
                                }
                                else
                                {
                                    strDestinationPath = string.Empty;
                                }
                                objClsGeneral.Dispose();
                                objClsGeneral = null;
                                // End of changes by Abhijeet on 20110511 to remove hardcoded CLC path for reprocessing of lab file

                                if (strDestinationPath.Trim() != "")
                                {
                                    if (System.IO.Directory.Exists(strFilePath) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(strFilePath);
                                    }

                                    // creatin the file at selected path
                                    FileStream objflestream = new FileStream(strFilePath + @"\" + strFileName, FileMode.Create, FileAccess.ReadWrite);

                                    // writting the file using streamwriter class
                                    StreamWriter objstrmWtr = new StreamWriter(objflestream);
                                    objstrmWtr.WriteLine(_HL7Message);
                                    objstrmWtr.Close();

                                    File.Copy(strFilePath + @"\" + strFileName, strDestinationPath + @"\" + strFileName);
                                    Application.DoEvents();
                                    File.Delete(strFilePath + @"\" + strFileName);
                                    //by Abhijeet on 20100430
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Completed reprocess of HL7 file after un match patient registration", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                }
                                else
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Failed to reprocess lab result file after mapping patient due to database inbox path for HL7 file found empty.", lngMappedPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                                }

                            }

                            //** Changes for Order ID conflict ***//



                            //sqlQry = "update patient set nclinicid=" + "-2" + " where npatientid=" + lngSelectedPatientID.ToString();
                            //oDB.Execute_Query(sqlQry);

                            //sqlQry = "update patient_dtl set nClinicID=" + "-2" + " where npatientid=" + lngSelectedPatientID.ToString();
                            //oDB.Execute_Query(sqlQry);

                            //sqlQry = "update Patient_OtherContacts set nClinicID=" + "-2" + " where nPatientID=" + lngSelectedPatientID.ToString();
                            //oDB.Execute_Query(sqlQry);                                              

                            Int32 statusID = 0;
                            statusID = gloTaskMail.frmTask.StatusType.Completed.GetHashCode();
                            sqlQry = "UPDATE TM_Task_Progress SET dComplete = " + "100" + ", nStatusID = " + statusID + " where nTaskID = " + taskId + " ";
                            oDB.Execute_Query(sqlQry);

                            oDB.Disconnect();
                            oDBHL7.Disconnect();
                        }
                        //by Abhijeet on 20100430
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Successfully map un match patient to gloEMR patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        
                        // Refreshing the Patients in grid
                        // make _lngTaskID variable to zero before refreshing data.  
                        _lngTaskID = 0;
                        LoadData();

                        dtTasks.Dispose();
                        dtTasks = null;
                        TaskIds = null;
                    }
                    else
                    {
                        //by Abhijeet on 20100430
                        //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Did not confirm operation to map un match patient", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                        
                    }
                }
                else
                {
                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Cancel operation to map un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);


                }
                objfrmMapUnMatchPatients.Dispose();
                objfrmMapUnMatchPatients = null;
                
            }
            catch (Exception ex)
            {
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Error in to map un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in Match Patient: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Match Patient: " + ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {                   
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBHL7 != null)
                {                    
                    oDBHL7.Dispose();
                    oDBHL7 = null;
                }
            }
        }

        private DataTable GetUnmatchedTasksHavingSameDemographics(long TaskID, long UserID)
        {
            gloDatabaseLayer.DBLayer oDB =  null;
            DataTable dtTasks = new DataTable();

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

                string strQry = "EXEC dbo.gsp_GetSimilarPatientUnmatchedTasks @nTaskID = " + TaskID + ", @nUserId = " + UserID;
                
                oDB.Connect(false);
                oDB.Retrive_Query(strQry, out dtTasks);
                oDB.Disconnect();

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
                    oDB = null;
                }
            }

            return dtTasks;
        }

        private Boolean GetTaskDetails(long TaskID)
        {
            // This function is used for Taking Task details

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                DataTable dt = new DataTable();
                //string strQry = "Select * from PatientsUnmatchedInLab where nTaskID='" + TaskID.ToString() + "'";  //Remove select *
                string strQry = "Select sPatientCode, sFirstName, sMiddleName, sLastName, dtDOB, nSSN, sGender, nProviderID, nOrderId, MessageID, HL7Message from PatientsUnmatchedInLab where nTaskID='" + TaskID.ToString() + "'";
                oDB.Connect(false);
                oDB.Retrive_Query(strQry, out dt);
                oDB.Disconnect();
                if (dt != null && dt.Rows.Count > 0)
                {
                    _nOrderID = Convert.ToInt64(dt.Rows[0]["nOrderId"]);
                    _sPatientCode = Convert.ToString(dt.Rows[0]["sPatientCode"]);
                    _sFirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                    _sMiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                    _sLastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                    _dtDOB = Convert.ToDateTime(dt.Rows[0]["dtDOB"]);
                    _nSSN = Convert.ToString(dt.Rows[0]["nSSN"]);
                    _SGender = Convert.ToString(dt.Rows[0]["sGender"]);
                    _nProviderID = Convert.ToInt64(dt.Rows[0]["nProviderID"]);
                    _MessageID = Convert.ToString(dt.Rows[0]["MessageID"]);
                    _HL7Message = Convert.ToString(dt.Rows[0]["HL7Message"]);
                    return true;
                }
                else
                {
                    _nOrderID = 0;
                    _sPatientCode = "";
                    _sFirstName = "";
                    _sMiddleName = "";
                    _sLastName = "";
                    _dtDOB = default(DateTime);
                    _nSSN = "";
                    _SGender = "";
                    _nProviderID = 0;
                    _MessageID = "";
                    _HL7Message = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                obj.UpdateLog("Error get task details: " + ex.ToString());
                _nOrderID = 0;
                _sPatientCode = "";
                _sFirstName = "";
                _sMiddleName = "";
                _sLastName = "";
                _dtDOB = default(DateTime);
                _nSSN = "";
                _SGender = "";
                _nProviderID = 0;
                _MessageID = "";
                _HL7Message = "";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;

            }
            finally
            {
                if (oDB!=null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void DiscardLabReport()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                //  Code for discarding patient

                Int64 taskId = 0;
                if (C1UserTasks.Row > 0)
                {
                    //if it is chield Node (Task Node) 
                    if (C1UserTasks.Rows[C1UserTasks.Row].Node.Children == 0)
                    {
                        //Incident #58149: 00019311
                        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
                        //Optimize flow for displaying match patient form.
                        taskId = Convert.ToInt64(C1UserTasks.GetData(C1UserTasks.Row, 2));
                    }
                }
                else
                {
                    MessageBox.Show("No task available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GetTaskStatus(taskId) != 1)
                {
                    MessageBox.Show("Task is not accepted. You can not proceed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Added by Abhijeet on 20100621
                if (MessageBox.Show("Are you sure to discard this lab results?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }


                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Open for Discarding un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                // long lngPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, COL_PATIENTID));

                DataTable dtTasks = new DataTable();
                List<long> TaskIds = new List<long>();
                TaskIds.Add(taskId);

                dtTasks = GetUnmatchedTasksHavingSameDemographics(taskId, _lngLoginID);

                if (dtTasks != null && dtTasks.Rows.Count > 1)
                {
                    DialogResult oResult = MessageBox.Show("Identified " + (dtTasks.Rows.Count - 1) + " other unmatched results with same demographics for the lab result you are denying now." + Environment.NewLine + "Do you want to process all identified unmatched results?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (oResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        TaskIds.Clear();

                        foreach (DataRow drRow in dtTasks.Rows)
                        {
                            TaskIds.Add(Convert.ToInt64(drRow["nTaskId"]));
                        }
                    }
                    else if (oResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                foreach (var ntaskid in TaskIds)
                {
                    taskId = ntaskid;
                    // Take TasK Details
                    GetTaskDetails(taskId);

                    oDB.Connect(false);

                    string sqlQry = "";

                    //** changes for order id conflict **//
                    //string sqlQry = "Delete from Lab_order_mst where labom_OrderID=" + _nOrderID.ToString();
                    //oDB.Execute_Query(sqlQry);

                    //sqlQry = "Delete from Lab_Order_TestDtl where labotd_OrderID=" + _nOrderID.ToString();
                    //oDB.Execute_Query(sqlQry);

                    //sqlQry = "Delete from Lab_Order_Test_Result where labotr_OrderID=" + _nOrderID.ToString();
                    //oDB.Execute_Query(sqlQry);

                    //sqlQry = "Delete from Lab_Order_Test_ResultDtl where labotrd_OrderID=" + _nOrderID.ToString();
                    //oDB.Execute_Query(sqlQry);

                    //sqlQry = "Delete from Lab_Order_TestDtl_DiagCPT where labodtl_OrderID=" + _nOrderID.ToString();
                    //oDB.Execute_Query(sqlQry);
                    //** changes for order id conflict **//

                    sqlQry = "update PatientsUnmatchedInLab set nStatusUnmatched=3,sStatusUnmatchedDesc='Discard Lab Report',Processed=1 where nTaskID=" + taskId.ToString();
                    oDB.Execute_Query(sqlQry);

                    // code to update the seletced task to 100 % complete           
                    Int32 statusID = 0;
                    statusID = gloTaskMail.frmTask.StatusType.Completed.GetHashCode();
                    sqlQry = "UPDATE TM_Task_Progress SET dComplete = " + "100" + ", nStatusID = " + statusID + " where nTaskID = " + taskId + " ";
                    oDB.Execute_Query(sqlQry);
                    //}
                }
                oDB.Disconnect();
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Delete, "Successfully discard un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                //string sqlQry = "update patient set nclinicid=" + "-2" + " where npatientid=" + lngPatientID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "update patient_dtl set nClinicID=" + "-2" + " where npatientid=" + lngPatientID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "update Patient_OtherContacts set nClinicID=" + "-2" + " where nPatientID=" + lngPatientID.ToString();
                //oDB.Execute_Query(sqlQry);

                // Refreshing the Patients in grid
                // make _lngTaskID variable to zero before refreshing data.  
                _lngTaskID = 0;
                LoadData();

                dtTasks.Dispose();
                dtTasks = null;
                TaskIds = null;
            }
            catch (Exception ex)
            {
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Delete, "Failure to discard un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error in discard lab report: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in discard lab report: " + ex.ToString(), false);

            }
            finally
            {
                if(oDB!=null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
                      
        }

        private void RegisterAcceptNewPatient()
        {
            //Actions :

            // Read respective patient from PatientsUnmatchedInLab
            // With minimum information Register patient
            // Delete Order against the task in this table
            // Check HL7 Message still in database 
                // If not add from same table.
            // Set flag to Unprocessed for this message in HL7data table
            // In HL7 service set processed flag in this table after processing.
            // Update tasks against this order.  
            // Set notification or information task.

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            //Added by Abhijeet on 20101109 for HL7 hybrid database connection string
            gloDatabaseLayer.DBLayer oDBHL7 = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());             
            //End of changes by Abhijeet on 20101109 for HL7 hybird database connection string
            try
            {
                //  Code for registering patient
                Int64 taskId = 0;
                if (C1UserTasks.Row > 0)
                {
                    //if it is chield Node (Task Node) 
                    if (C1UserTasks.Rows[C1UserTasks.Row].Node.Children == 0)
                    {
                        //Incident #58149: 00019311
                        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
                        //Optimize flow for displaying match patient form.
                        taskId = Convert.ToInt64(C1UserTasks.GetData(C1UserTasks.Row, 2));
                    }
                }
                else
                {
                    MessageBox.Show("No task available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GetTaskStatus(taskId) != 1)
                {
                    MessageBox.Show("Task is not accepted. You can not proceed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Added by Abhijeet on 20100621
                //if (MessageBox.Show("Are you sure to resgister this patient?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                //modified message by madan on 20100802
                if (MessageBox.Show("Are you sure you want to register this patient?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }



                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Open for registering un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

               

                //  long lngPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, COL_PATIENTID));

                // obtain the task details
                GetTaskDetails(taskId);

                oDB.Connect(false);
                oDBHL7.Connect(false);

                string strQry = "Select count(nPatientID) from  patient where sPatientCode='" + _sPatientCode.Replace("'", "''") + "'";
                object objTmp = oDB.ExecuteScalar_Query(strQry);
                if (objTmp != null)
                {
                    if (Convert.ToInt64(objTmp) > 0)
                    {
                        MessageBox.Show("Patient Code '" + _sPatientCode + "' already in use," + Environment.NewLine + " Choose either 'Match Patient' or 'Discard Lab Report' for this result.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string sqlQuery = "update PatientsUnmatchedInLab set nStatusUnmatched=5,sStatusUnmatchedDesc='Not Registered Due to Duplicate Patient Code',Processed=1 where nTaskID=" + taskId.ToString();
                        oDB.Execute_Query(sqlQuery);
                       
                       
                        // coomented code by Abhijeet on 20100619
                        //Int32 statusID = 0;
                        //statusID = gloTaskMail.frmTask.StatusType.Completed.GetHashCode();
                        //sqlQuery = "UPDATE TM_Task_Progress SET dComplete = " + "100" + ", nStatusID = " + statusID + " where nTaskID = " + taskId + " ";
                        //oDB.Execute_Query(sqlQuery);
                        //oDB.Disconnect();

                        oDB.Disconnect();
                        
                        //by Abhijeet on 20100430
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Failed un match patient registration due to duplicate code", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);                        

                        // Refreshing the Patients & Task in grid by Abhijeet On Date 20100416
                        // make _lngTaskID variable to zero before refreshing data.  
                        _lngTaskID = 0;
                        LoadData();

                        return;
                    }
                }

                DataTable dtTasks = new DataTable();
                List<long> TaskIds = new List<long>();
                TaskIds.Add(taskId);

                dtTasks = GetUnmatchedTasksHavingSameDemographics(taskId, _lngLoginID);

                if (dtTasks != null && dtTasks.Rows.Count > 1)
                {
                    DialogResult oResult = MessageBox.Show("Identified " + (dtTasks.Rows.Count - 1) + " other unmatched results with same demographics for the lab result you are adding now." + Environment.NewLine + "Do you want to process all identified unmatched results?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (oResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        TaskIds.Clear();

                        foreach (DataRow drRow in dtTasks.Rows)
                        {
                            TaskIds.Add(Convert.ToInt64(drRow["nTaskId"]));
                        }
                    }
                    else if (oResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                long PatientID = 0;
                PatientID = GenerateUniqueId("Patient", "nPatientID");
                if (PatientID == 0)
                {
                    oDB.Disconnect();
                    MessageBox.Show("Error in Patient ID Generation.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Failed un match patient registration due to error in patient ID generation", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

       
                    return;
                }

                _nSSN = _nSSN.Trim() == "0" ? "" : _nSSN;

                string sqlQry = "insert into patient (nPatientID,sPatientCode,sFirstName,sMiddleName,sLastName," +
                                                    "nSSN,dtDOB,sGender,nProviderID,nclinicid,bSettoCollection) " +
                                    " values(" + Convert.ToString(PatientID) + ",'" + _sPatientCode.Replace("'", "''") + "','" + _sFirstName.Replace("'", "''") + "','" + _sMiddleName.Replace("'", "''") + "','" + _sLastName.Replace("'", "''") +
                                    "','" + _nSSN.Replace("'", "''") + "','" + _dtDOB.ToString() + "','" + _SGender.Replace("'", "''") + "'," + _nProviderID.ToString() + ",1,0)";

                oDB.Execute_Query(sqlQry);

                //Code Added by Abhijeet on 20110806 for Family Account Implemenation for new patient   
                gloDatabaseLayer.DBParameters oDBParas = new gloDatabaseLayer.DBParameters();
                oDBParas.Clear();
                oDBParas.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("PA_Accounts_CreateAccounts", oDBParas);
                oDBParas.Clear();
                if (oDBParas != null)
                {
                    oDBParas.Dispose();
                }
                //End of changes by Abhijeet on 20110806 for family account implementation for new patient

               //** changes for order id Conflict **/

                //sqlQry = "Delete from Lab_order_mst where labom_OrderID=" + _nOrderID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "Delete from Lab_Order_TestDtl where labotd_OrderID=" + _nOrderID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "Delete from Lab_Order_Test_Result where labotr_OrderID=" + _nOrderID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "Delete from Lab_Order_Test_ResultDtl where labotrd_OrderID=" + _nOrderID.ToString();
                //oDB.Execute_Query(sqlQry);

                //sqlQry = "Delete from Lab_Order_TestDtl_DiagCPT where labodtl_OrderID=" + _nOrderID.ToString();
                //oDB.Execute_Query(sqlQry);

                //** changes for order id Conflict **/

                //Check similar unprocessed tasks for same patient
                       

                        foreach (var ntaskid in TaskIds)
                        {

                            taskId = ntaskid;

                            if (TaskIds.Count > 1) { GetTaskDetails(taskId); }
               
                sqlQry = "select count(MessageID) from hl7_hl7data where MessageID='" + _MessageID + "'";
                //Added by Abhijeet on 20101109 for HL7 hybrid database connection string
                //objTmp = oDB.ExecuteScalar_Query(sqlQry);
                //oDBHL7.Connect(false);
                objTmp = oDBHL7.ExecuteScalar_Query(sqlQry);
                //End of changes by Abhijeet on 20101109 for HL7 hybrid database connection string
                
                if (Convert.ToInt64(objTmp) > 0)
                { // Update processed status to Zero for reprocess the message in HL7_HL7Data table
                   
                    sqlQry = "update HL7_HL7Data set processed=0 where MessageID='" + _MessageID + "'";

                    //Added by Abhijeet on 20101109 for HL7 hybrid database connection string
                    //oDB.Execute_Query(sqlQry);
                    oDBHL7.Execute_Query(sqlQry);
                    //End of changes by Abhijeet on 20101109 for HL7 hybrid database connection string                    
                }
                else
                { // Create HL7 file

                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Start to Reprocess HL7 file after un match patient registration", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    string strFileName = PatientID.ToString() + " " + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".hl7"; //+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".hl7";
                    string strFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon";

                    //Added by Abhijeet on 20110511 to remove hardcoded CLC path for reprocessing of lab file
                    //string strDestinationPath = "C:\\Program Files\\Clinician\\Local Client Service\\local\\local_result\\archive";
                  
                    string strDestinationPath = string.Empty;
                    clsGeneral objClsGeneral = new clsGeneral();
                    long nDBID = 0;
                    nDBID = objClsGeneral.GetDataBaseConnectionIdFromHL7DB();

                    if (nDBID > 0)
                    {
                        strDestinationPath = objClsGeneral.GetHL7Setting(nDBID);
                    }
                    else
                    {
                        strDestinationPath = string.Empty;
                    }
                    objClsGeneral.Dispose();
                    objClsGeneral = null;
                    // End of changes by Abhijeet on 20110511 to remove hardcoded CLC path for reprocessing of lab file

                    if (strDestinationPath.Trim() != "")
                    {
                            if (System.IO.Directory.Exists(strFilePath) == false)
                            {
                                System.IO.Directory.CreateDirectory(strFilePath);
                            }

                            // creatin the file at selected path
                            FileStream objflestream = new FileStream(strFilePath + @"\" + strFileName, FileMode.Create, FileAccess.ReadWrite);

                            // writting the file using streamwriter class
                            StreamWriter objstrmWtr = new StreamWriter(objflestream);
                            objstrmWtr.WriteLine(_HL7Message);
                            objstrmWtr.Close();

                            File.Copy(strFilePath + @"\" + strFileName, strDestinationPath + @"\" + strFileName);
                            Application.DoEvents();
                            File.Delete(strFilePath + @"\" + strFileName);
                            //by Abhijeet on 20100430
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Completed reprocess of HL7 file after un match patient registration", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Failed to reprocess lab result file after registering patient due to database inbox path for HL7 file found empty.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    

                }
                
                //  by Abhijeet on 20100619
                // update newly generated patient id to overcome circular task generation for un match patient
                sqlQry = "update PatientsUnmatchedInLab set nStatusUnmatched=1,sStatusUnmatchedDesc='New Patient',Processed=1,bitIsBlocked='True',nNewPatientID=" + PatientID.ToString() + " where nTaskID=" + taskId.ToString();
                // end of changes by Abhijeet on 20100619 for updating newly generated patient ID also
                oDB.Execute_Query(sqlQry);

                Int32 _statusID = 0;
                _statusID = gloTaskMail.frmTask.StatusType.Completed.GetHashCode();
                sqlQry = "UPDATE TM_Task_Progress SET dComplete = " + "100" + ", nStatusID = " + _statusID + " where nTaskID = " + taskId + " ";
                oDB.Execute_Query(sqlQry);

                        }
                oDB.Disconnect();
                oDBHL7.Disconnect();
                        
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Successfully registered un match patient", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                // Refreshing the Patients & Task in grid
                // make _lngTaskID variable to zero before refreshing data.  
                _lngTaskID = 0;
                LoadData();

                dtTasks.Dispose();
                dtTasks = null;
                TaskIds = null;
            }
            catch (Exception ex)
            {               
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterEMRPatient, "Error in registering un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);

                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in registering patient: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in registering patient: " + ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {   
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBHL7 != null)
                {                    
                    oDBHL7.Dispose();
                    oDBHL7 = null;
                }
            }
            
        }

        protected long GenerateUniqueId(string Tablename, string fieldname) 
        { 
           
            Int64 MachineID = 0; 
            Int64 _ID = 0; 
            string strquery = "";
            object objTmp;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                MachineID = GetPrefixTransactionID(DateTime.Now);

                strquery = "SELECT " + fieldname + " FROM " + Tablename + " WHERE convert(varchar(18)," + fieldname + " ) Like convert(varchar(18)," + MachineID + " )+ '%'";
                // Check if there is already PatientId against this MachineID 

                //Get this ID 
                _ID = 0;
                oDB.Connect(false);
                objTmp = oDB.ExecuteScalar_Query(strquery);
                oDB.Disconnect();

                if (objTmp == null)
                {
                    _ID = 0;
                }
                if (objTmp.ToString() == "")
                {
                    _ID = 0;
                }
                else
                {
                    _ID = Convert.ToInt64(objTmp);
                }

                strquery = "";

                //Generate Patient ID 
                if (_ID == 0)
                {
                    strquery = "select convert(numeric(18,0), convert(varchar(18)," + MachineID + ") + '01')";
                }
                else
                {
                    strquery = "select isnull(max(" + fieldname + " ),0)+1 from " + Tablename + " where convert(varchar(18)," + fieldname + ") Like convert(varchar(18)," + MachineID + " )+ '%'";
                }

                _ID = 0;
                oDB.Connect(false);
                objTmp = oDB.ExecuteScalar_Query(strquery);
                oDB.Disconnect();
                if (objTmp == null)
                {
                    _ID = 0;
                }
                else
                {
                    _ID = Convert.ToInt64(objTmp);
                }


                return _ID;

            }
            catch (Exception ex)
            {

                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in registering patient: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Generating unique Id while registering patient: " + ex.ToString(), false);

                return 0;
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

        public long GetPrefixTransactionID(DateTime PatientDOB)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

           // object _internalresult = null;-- commented the  code to fix the warnings--- by madan on 20100520
            // string _strSQL = "";-- commented the  code to fix the warnings--- by madan on 20100520

           // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            
            try
            {                                  
                if (PatientDOB != null)
                {
                    if (PatientDOB.GetType() != typeof(System.DBNull))
                    {
                        if (PatientDOB.ToString() != "")
                        {
                            _PatientDOB = Convert.ToDateTime(PatientDOB);
                        }
                    }                       
                }
             
                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

                  return _Result;

            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog(" Error in registering patient: " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in generating prefix transaction ID : " + ex.ToString(), false);              
                 return 0;
            }                                   
        }

        //Incident #58149: 00019311
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        private void pnlTask_Resize(object sender, EventArgs e)
        {
            try
            {
                int _width = pnlTask.Width - 2; // C1UserTasks.Width - 2;

                C1UserTasks.Cols[Col_Task_Subject].Width = Convert.ToInt32(_width - 36);
                //'Subject
                C1UserTasks.Cols[Col_Task_Completed].Width = Convert.ToInt32(_width * 0);
                //'% Completed Hidden
                C1UserTasks.Cols[Col_Task_Priority].Width = 20;
            }
            catch
            { }

        }


        private void SearchTasksList()
        {
            DataTable dt = null;
            gloTaskMail.gloTask ogloTasks = null;
            DataView dv = null;

            try
            {
                string strSearch = txtSearch.Text.Trim();
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");

                if (dtTasksList != null && dtTasksList.Rows.Count > 0)
                {
                    dt = dtTasksList;
                }
                else
                {
                    ogloTasks = new gloTaskMail.gloTask(_dataBaseConnectionString);
                    dt = ogloTasks.GetUnFinishedUnmatchedTasks(_lngLoginID);
                    dtTasksList = dt;
                }

                dv = dt.DefaultView;

                dv.RowFilter = dv.Table.Columns["Subject"].ColumnName + " Like '%" + strSearch + "%'";

                C1UserTasks.DataSource = dv;

                #region "Setup a column display"

                C1UserTasks.Rows[0].Visible = false;
                C1UserTasks.Cols[Col_Task_TaskID].Visible = false;
                C1UserTasks.Cols[Col_Task_No].Visible = false;
                C1UserTasks.Cols[Col_Task_Status].Visible = false;
                C1UserTasks.Cols[Col_Task_Completed].Visible = false;
                C1UserTasks.Cols[Col_Task_TaskDate].Visible = false;
                C1UserTasks.Cols[Col_Task_PatientID].Visible = false;
                C1UserTasks.Cols[Col_Task_Assigned].Visible = false;
                C1UserTasks.Cols[Col_Task_ColCount].Visible = false;

                int _width = pnlTask.Width - 2; // C1UserTasks.Width - 2;

                C1UserTasks.Cols[Col_Task_Subject].Width = Convert.ToInt32(_width - 36);
                //'Subject
                C1UserTasks.Cols[Col_Task_Completed].Width = Convert.ToInt32(_width * 0);
                //'% Completed Hidden
                C1UserTasks.Cols[Col_Task_Priority].Width = 20;

                _width = 0;

                C1UserTasks.Cols[0].Visible = false;
                C1UserTasks.AllowEditing = false;

                //'// build the outline
                dynamic total = C1UserTasks.Cols[Col_Task_Priority].Index;
                C1UserTasks.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Clear);
                C1UserTasks.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.None, 0, 0, 0, "{0}");

                #endregion

                #region "Binding an Images to the grid "


                for (int i = 0; i < C1UserTasks.Rows.Count; i++)
                {
                    C1UserTasks.Rows[i].Height = 21;
                    if (C1UserTasks.Rows[i].IsNode)
                    {
                        C1UserTasks.Rows[i].ImageAndText = true;
                        C1UserTasks.Rows[i].Node.Image = imgList_Common.Images[13];
                        C1UserTasks.Rows[i].Node.Level = 0;
                        C1UserTasks.Rows[i].Node.Row.Style = C1UserTasks.Styles["cs_Parent"];
                        C1UserTasks.Rows[i].Node.Data = C1UserTasks.Rows[i + 1][Col_Task_DueDate].ToString();
                    }
                    else
                    {
                        #region "Default Row Selection "

                        if (i > 0)
                        {
                            Int64 _taskId = Convert.ToInt64(C1UserTasks.GetData(i, 2));
                            if (_taskId == _lngTaskID)
                            {
                                C1UserTasks.Select(i, Col_Task_TaskID);
                            }
                        }

                        #endregion

                        C1UserTasks.Cols[Col_Task_Priority].ImageAndText = false;

                        if (Convert.ToString(C1UserTasks.GetData(i, Col_Task_Status)) == "Not Yet Accepted")
                        {
                            C1UserTasks.Rows[i].Style = C1UserTasks.Styles["cs_Child_Bold"];
                        }
                        else
                        {
                            C1UserTasks.Rows[i].Style = C1UserTasks.Styles["cs_Child"];
                        }

                        switch (C1UserTasks.Rows[i][Col_Task_Priority].ToString())
                        {
                            case "High Priority":
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[20]);
                                break;
                            case "Low Priority":
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[19]);
                                break;
                            default:
                                C1UserTasks.SetCellImage(i, Col_Task_Priority, imgList_Common.Images[1]);
                                break;
                        }
                    }
                }

                if (C1UserTasks.Rows.Count > 1)
                {
                    LoadSelectedPatient();
                }
                else
                {
                    if (c1PatientList.Rows.Count > 1)
                    {
                        c1PatientList.Rows.Remove(1);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error while searching unmatched patient tasks list: " + ex.ToString(), false);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

                if (ogloTasks != null)
                {
                    ogloTasks.Dispose();
                    ogloTasks = null;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error while clearing unmatched patient tasks search text: " + ex.ToString(), false);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchTasksList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void C1UserTasks_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(c1SuperTooltip1, C1UserTasks, e.Location);
        } 
    }
}