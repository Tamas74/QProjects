using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;

namespace gloPatient
{
    public partial class frmMapUnMatchPatients : Form
    {
        
        // Property to save the patient 
        private long _SelectedPatientId = 0;
        public long SelectedPatientId
        {
            get { return _SelectedPatientId ;}
            set { _SelectedPatientId =value; }                        
        }
          
        // defaines class level variable to save patient details
        //private long _nPatientID = 0;--- Commented to remove warnings.
        //private string _sPatientCode="";
        private string _sFirstName = "";
        private string _MiddleName = "";
        private string _sLastName = "";
        private string _AddressLine1;
        private string _AddressLine2;
        private string _Phone;
        private string _City;
        private string _State;
        private string _Zip;
        private string _PharmacysNCPDPID;
        private Int64  _ProviderID;
        private string _Fax;
        private DateTime _dtDob ;
        private string _sGender = "";
        private string _ProviderName = "";
        string _MPDrugName=""; 
        string _DrugQty="";
        DateTime _DtReceiced; 
        string _RefillQty="";
       
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _DBConnectionString = "";
        private bool isChangeRequest = false;


        public enum FormAction
        { 
            MatchPatient,
            NewPatient,
            DenyRequest,
            Cancel,
            None
        }

        public FormAction CurrentAction { get; set; }

        public frmMapUnMatchPatients(string FirstName, string MiddleName, string LastName, DateTime DOB, string Gender)
        {
            InitializeComponent();

            _sFirstName = FirstName;
            _MiddleName = MiddleName;
            _sLastName = LastName;

            _dtDob = DOB;
            _sGender = Gender;
            
            isChangeRequest = true;

            if (appSettings != null)
            {
                _DBConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }            
        }

        public frmMapUnMatchPatients(string FirstName, string LastName, DateTime DOB, string Gender,
                                    string AddressLine1, string AddressLine2, string Phone, string City, string State, string Zip,
                                    string PharmacysNCPDPID, Int64 ProviderID, string Fax, String MiddleName, string ProviderName,
                                    string MPDrugName,string DrugQty,DateTime DtReceiced,string RefillQty)
        {
            
              _sFirstName = FirstName;
              _sLastName = LastName;
              _dtDob = DOB;
              _sGender = Gender;
              _MiddleName = MiddleName;
              _AddressLine1 =AddressLine1;
              _AddressLine2= AddressLine2;
              _Phone= Phone;
              _City= City;
              _State= State;
              _Zip= Zip;
              _PharmacysNCPDPID=PharmacysNCPDPID;
              _ProviderID=ProviderID;
              _Fax=Fax;
              _ProviderName = ProviderName;            
              _MPDrugName = MPDrugName;
               _DrugQty = DrugQty;
               _DtReceiced=DtReceiced;
               _RefillQty = RefillQty;               

            if (appSettings != null)
            {
                _DBConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }
            InitializeComponent();
        }
        

        private void frmMapUnMatchPatients_Load(object sender, EventArgs e)
        {
            CurrentAction = FormAction.None; 
          //  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
            try
            {
                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "open un match view form to map un match patients", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                // Making blanks patient details
                ClearFields();
               
                txtSearch.Focus();

                BindPatientGrid(sender, e);

                lblRefReqFirstName.Text = _sFirstName;
                lblRefReqMiddleName.Text = _MiddleName;
                lblRefReqLastName.Text = _sLastName;
                //lblRefReqProvider.Text = _ProviderName;
                lblRefReqDOB.Text = _dtDob.ToString("MM/dd/yyyy");               
                lblRefReqGender.Text = _sGender;

                //FillRequestedMedication();
                lblRequested.Text = _MPDrugName;
                lblRequestedQty.Text = _DrugQty;

                if (isChangeRequest)
                {
                    this.Text = "Patient Not Found for Change Request - Select a Patient, Create a New Patient, or Deny the Change Request";
                    lblPatientInfo.Text = "Change Request Patient Info";                    
                    lblTextContent.Text = "gloEMR cannot find the Patient for the Change Request. Select a patient from the list. " + Environment.NewLine + "If the correct patient is not showing, you can: 1) Search patient names 2) Deny the Request 3) Create a new patient.";

                    lblMedication.Visible = false;
                    lblRequested.Visible = false;
                    lblQuantity.Visible = false;
                    lblRequestedQty.Visible = false;
                }
                else
                {
                    this.Text = "Patient Not Found for Refill Request - Select a Patient, Create a New Patient, or Deny the Refill Request";
                    lblPatientInfo.Text = "Refill Request Patient Info";
                    lblMedication.Text = "Requested Med :";
                    lblTextContent.Text = "gloEMR cannot find the Patient for the Refill Request. Select a patient from the list. " + Environment.NewLine + "If the correct patient is not showing, you can: 1) Search patient names 2) Deny the Request 3) Create a new patient.";                                        
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Form Load : " + ex.ToString(), false);
            }
            finally
            {
                //if (oDB != null)
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}

            }                                

        }

        private void ClearFields()
        {
            try
            {
                lblFirstNameValue.Text = "";
                lblMiddleNameValue.Text = "";
                lblLastNameValue.Text = "";
                //lblProviderNameValue.Text = "";
                lblPatientDOBValue.Text = "";
                lblGenderValue.Text = "";
                Int64 patientid = 0;
                FillActiveMedication(ref patientid);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Form Load : " + ex.ToString(), false);
            }
        }

        private void BindPatientGrid(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPatient = GetMatchPatients();//GetPatients();

                if (dtPatient == null)
                {
                  //  c1PatientList.Clear();
                    c1PatientList.DataSource = null;
                    c1PatientList.Cols.Count = 0;
                }
                else
                {
                    c1PatientList.SelChange -= new System.EventHandler(this.c1PatientList_SelChange);
                    c1PatientList.DataSource = dtPatient;
                    CustomGridStyle();
                    c1PatientList.SelChange += new System.EventHandler(this.c1PatientList_SelChange);
                    if (dtPatient.Rows.Count > 0)
                    {
                        c1PatientList.RowSel = 1;
                        c1PatientList_Click(sender, e);
                    }
                    else
                    {
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("BindPatientGrid : " + ex.ToString(), true);
            }
        }

        private void CustomGridStyle()
        {
            try
            {
                
                if (c1PatientList.Cols.Count <= 0)
                    return;
                
                c1PatientList.Cols["sPatientCode"].Caption = "Code";
                c1PatientList.Cols["PatientName"].Caption = "Patient Name";
                c1PatientList.Cols["sFirstName"].Caption = "First Name";
                c1PatientList.Cols["sMiddleName"].Caption = "MI";
                c1PatientList.Cols["sLastName"].Caption = "Last Name";
                c1PatientList.Cols["nSSN"].Caption = "SSN";
                c1PatientList.Cols["dtDOB"].Caption ="DOB";
                c1PatientList.Cols["sGender"].Caption = "Gender";
                c1PatientList.Cols["sProviderName"].Caption = "Provider Name";
                c1PatientList.Cols["dtLastExam"].Caption = "Date Of Last Exam";
                c1PatientList.Cols["nPatientID"].Width = 0;
                c1PatientList.Cols["nProviderID"].Width = 0;

                // setting the width of patient Grid Columns
                c1PatientList.Cols["sPatientCode"].WidthDisplay = 90;
                c1PatientList.Cols["PatientName"].Width = 210;
                c1PatientList.Cols["sFirstName"].WidthDisplay = 0; //150;
                c1PatientList.Cols["sMiddleName"].WidthDisplay = 0; // 50;
                c1PatientList.Cols["sLastName"].WidthDisplay = 0; //150;
                c1PatientList.Cols["sGender"].WidthDisplay = 65;
                c1PatientList.Cols["nSSN"].WidthDisplay = 0; //110;
                c1PatientList.Cols["sProviderName"].WidthDisplay = 210;
                c1PatientList.Cols["dtDOB"].WidthDisplay = 80;
                c1PatientList.Cols["dtLastExam"].WidthDisplay = 116;
                c1PatientList.ExtendLastCol = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                throw ex;
            }
        }

       
        public DataTable GetLatestActiveMedication(ref Int64 _nPatient)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable result =null;
           // DataView dv = null;
            try 
            {
                oDB.Connect(false);
                oParameters.Add("@PatientID", _nPatient, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GetMedication", oParameters, out result);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

             return result.DefaultView.ToTable(false, new string[] { "sMedication", "sAmount","sStatus","dtmedicationdate" });

        }
        public DataTable GetMatchPatients()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable result = null;

            try
            {
                    oDB.Connect(false);
                    oParameters.Add("@FirstName", _sFirstName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@LastName", _sLastName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@SearchText", txtSearch.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@dtDOB", _dtDob, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Retrive("gsp_FindMatchPatients", oParameters, out result);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
            return result;
        }


        //private void FillRequestedMedication()
        //{
        //    try
        //    {
        //        DataTable dtRequestedMed = new DataTable("RequestedMed");
        //        DataRow row;
        //        dtRequestedMed.Columns.Add("Medication");
        //        dtRequestedMed.Columns.Add("Quantity");

        //        row = dtRequestedMed.NewRow();
        //        row["Medication"] = _MPDrugName;
        //        row["Quantity"] =  _DrugQty;
        //        dtRequestedMed.Rows.Add(row);

        //        row = null;

        //        c1RequestedMedication.Redraw = false;
        //        c1RequestedMedication.BeginUpdate();

        //        c1RequestedMedication.DataSource = dtRequestedMed;
               
        //        c1RequestedMedication.Cols["Medication"].Width = 260;

        //        c1RequestedMedication.ExtendLastCol = true;
              
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in FillRequestedMedication : " + ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        c1RequestedMedication.EndUpdate();
        //        c1RequestedMedication.Redraw = true;

        //    }
        //}


        private void FillActiveMedication(ref Int64 _nPatient)
        {
            try
            {
                DataTable dtMedication = GetLatestActiveMedication(ref _nPatient);
                DateTime dateMedication = Search(dtMedication);
                DataView dv = dtMedication.DefaultView;
                dv.RowFilter = "dtmedicationdate >=  '" + dateMedication.Date.ToShortDateString() + " 12:00:00 am" + "' and dtmedicationdate <= '" + dateMedication.Date.ToShortDateString() + " 11:59:59 pm" + "' " + " and sStatus='Active'";
                dv.Table.Columns["sMedication"].ColumnName = "Active Medications";
                dv.Table.Columns["sAmount"].ColumnName = "Quantity";
                dv.Table.Columns.Remove("sStatus");
                dv.Table.Columns.Remove("dtmedicationdate");
                c1ActiveMedication.DataSource = dv;

                c1ActiveMedication.ExtendLastCol = true;
                c1ActiveMedication.Cols[0].Width = 260;

               //for (int i = 0; i <= dtMedication.Rows.Count - 1; i++)
               //{
               //    TreeNode node = new TreeNode();
               //    node.NodeFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);//new Font(TrvNotes.Font, FontStyle.Bold);
               //    node.ForeColor = System.Drawing.Color.DarkBlue;
               //    node.Text = dtMedication.Rows[i]["Active Medications"].ToString() + "-" + Convert.ToString(dtMedication.Rows[i]["Quantity"]);
               //    trvMedication.Nodes.Add(node);   
               //}
               //trvMedication.ExpandAll();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in FillActiveMedication : " + ex.ToString(), true);
            }
            finally
            {
                
            }
        }

        public DateTime Search(DataTable dt)
        {
            DataRow[] dr = null;
            DateTime dtSelectedDate = DateTime.Now;
            try
            {               
                string _strSort = "dtMedicationDate desc";
                if ((dt != null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((dt != null))
                        {
                            dr = dt.Select("", _strSort);
                            if (dr.Length > 0)
                            {
                                 dtSelectedDate = Convert.ToDateTime(dr[0][3]);
                                return  dtSelectedDate;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Date Search : " + ex.ToString(), true);
            }
            finally
            {
                if ((dt != null))
                {
                    dt.Dispose();
                    dt = null;
                }
                if ((dr != null))
                { 
                    dr = null;
                }
            }
            return dtSelectedDate;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
               // GetPatients();
                GetMatchPatients();
                BindPatientGrid(sender, e);
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Click : " + ex.ToString(), true);
            }
        }


        private void c1PatientList_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1PatientList.Rows.Count > 1)
                {
                    if (c1PatientList.RowSel > 0)
                    {
                        lblFirstNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sFirstName"));
                        lblMiddleNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sMiddleName"));
                        lblLastNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sLastName"));
                        //lblProviderNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sProviderName"));
                        lblPatientDOBValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "dtDOB"));
                        lblGenderValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sGender"));
                        ComparePatientFields();
                        Int64 patientid=Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, "nPatientID"));
                        FillActiveMedication(ref patientid);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Click : " + ex.ToString(), true);
            }
        }

        private void tlbbtn_Close_Click(object sender, EventArgs e)
        {
            CurrentAction = FormAction.Cancel; 
            this.Close(); 
        }

        private void tlbbtn_MatchPatient_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentAction = FormAction.MatchPatient;
                if (c1PatientList.RowSel > 0)
                {
                    SelectedPatientId = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, "nPatientID"));
                    if (SelectedPatientId != 0)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Select patient from patient list. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in MatchPatient Click : " + ex.ToString(), true);
            }

        }

        private void tlbbtn_RegisterPatient_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentAction = FormAction.NewPatient;
                this.DialogResult = DialogResult.OK;                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in RegisterPatient Click : " + ex.ToString(), true);
            }
        }

        private void c1PatientList_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (c1PatientList.RowSel > 0)
                {
                    lblFirstNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sFirstName"));                    
                    lblMiddleNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sMiddleName"));
                    lblLastNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sLastName"));
                    //lblProviderNameValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sProviderName"));
                    lblPatientDOBValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "dtDOB"));                    
                    lblGenderValue.Text = Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sGender"));
                    Int64 patientid = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, "nPatientID"));
                    ComparePatientFields();
                    FillActiveMedication(ref patientid);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Sel Change : " + ex.ToString(), true);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear(); 
        }

        private void ComparePatientFields()
        {
            DateTime dateBirth;
            int comResult;
            try
            {
                comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sFirstName")), this._sFirstName, StringComparison.OrdinalIgnoreCase);
                if (comResult != 0)
                {
                    this.lblFirstNameValue.ForeColor = Color.Red;
                }
                else { this.lblFirstNameValue.ForeColor = Color.Green; }


                comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sMiddleName")), this._MiddleName, StringComparison.OrdinalIgnoreCase);
                if (comResult != 0)
                {
                    this.lblMiddleNameValue.ForeColor = Color.Red;
                }
                else { this.lblMiddleNameValue.ForeColor = Color.Green; }

                comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sLastName")), this._sLastName, StringComparison.OrdinalIgnoreCase);
                if (comResult != 0)
                {
                    this.lblLastNameValue.ForeColor = Color.Red;
                }
                else { this.lblLastNameValue.ForeColor = Color.Green; }

                //comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sProviderName")), this._ProviderName, StringComparison.OrdinalIgnoreCase);
                //if (comResult != 0)
                //{
                //    this.lblProviderNameValue.ForeColor = Color.Red;
                //}
                //else { this.lblProviderNameValue.ForeColor = Color.Green; }

                comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "sGender")), this._sGender, StringComparison.OrdinalIgnoreCase);
                if (comResult != 0)
                {
                    this.lblGenderValue.ForeColor = Color.Red;
                }
                else { this.lblGenderValue.ForeColor = Color.Green; }

                dateBirth = DateTime.Parse(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "dtDOB")));

                comResult = String.Compare(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, "dtDOB")), Convert.ToString(this._dtDob), StringComparison.OrdinalIgnoreCase);

                if ((dateBirth.Day != this._dtDob.Day) || (dateBirth.Month != this._dtDob.Month) || (dateBirth.Year != this._dtDob.Year))
                {
                    if (comResult != 0)
                    {
                        this.lblPatientDOBValue.ForeColor = Color.Red;
                    }
                }
                else { this.lblPatientDOBValue.ForeColor = Color.Green; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in Patient Grid Click ComparePatientFields : " + ex.ToString(), true);
            }

        }

        private void tlbbtn_DiscardPatient_Click(object sender, EventArgs e)
        {
           
            CurrentAction = FormAction.DenyRequest;
            this.DialogResult = DialogResult.OK;
        }

        private void trvMedication_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

       



    }
}
