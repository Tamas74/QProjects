using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloCCDLibrary;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace gloQRDAImport
{

    public partial class frmQRDAImport : Form
    {
        static String strConnectionString = "";
        Int64 _ClinicID = 0;
        string[] files;
      
        public frmQRDAImport()
        {
          //  strConnectionString = _conn;
            InitializeComponent();
        }

        public frmQRDAImport(string _conn)
        {
            strConnectionString = _conn;
            InitializeComponent();
        }



        private void FillProviders()
        {
            try
            {
                gloGlobal.gloPMGlobal.DatabaseConnectionString = strConnectionString;
                gloCCDLibrary.gloCCDDatabaseLayer db = new gloCCDDatabaseLayer();
                DataTable dtProvider = new DataTable();
                //Dim dr As DataRow
                //Dim i As Int16
                _ClinicID = 1;
                dtProvider = db.GetProviders(_ClinicID);
                if ((dtProvider != null))
                {
                    if (dtProvider.Rows.Count > 0)
                    {

                        cmbProviders.DataSource = dtProvider.Copy();
                        cmbProviders.ValueMember = dtProvider.Columns["nProviderID"].ColumnName;
                        cmbProviders.DisplayMember = dtProvider.Columns["ProviderName"].ColumnName;
                        cmbProviders.Refresh();
                        cmbProviders.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloQRDAImport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ResetProviders()
        {


            try
            {
                if (cmbProviders.Items.Count > 0)
                {
                    cmbProviders.DataSource = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloQRDAImport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            //string strConnectionString = string.Empty;
            //try
            //{
            //    //blnisValueChange = false;
            //    if (txtServer.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please enter SQL Server name", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtServer.Focus();
            //        return;
            //    }
            //    if (txtDatabase.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please enter Database name", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtDatabase.Focus();
            //        return;
            //    }
            //    if (chkSQLAuthentication.Checked == true)
            //    {
            //        if (txtUser.Text.Trim() == "")
            //        {
            //            MessageBox.Show("Please enter SQL Server User", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtUser.Focus();
            //            return;
            //        }
            //        if (txtPassword.Text.Trim() == "")
            //        {
            //            MessageBox.Show("Please enter SQL Server Password", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtPassword.Focus();
            //            return;
            //        }

            //    }


            //    if (chkSQLAuthentication.Checked == false)
            //    {
            //        if (IsConnect(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim(), false) == false)
            //        {
            //            ResetProviders();
            //            cmdImport.Enabled = false;
            //            btnjson.Enabled = false;   
            //            MessageBox.Show("Invalid Database Credentials.", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtServer.Focus();
            //            return;
            //        }
            //        else
            //        {
            //            FillProviders();
            //            cmdImport.Enabled = true;
            //            btnjson.Enabled = true;
            //            MessageBox.Show("Database connected successfully.", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    else
            //    {
            //        if (IsConnect(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim(), true) == false)
            //        {
            //            ResetProviders();
            //            cmdImport.Enabled = false;
            //            btnjson.Enabled = false;
            //            MessageBox.Show("Invalid Database Credentials.", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtServer.Focus();
            //            return;
            //        }
            //        else
            //        {
            //            FillProviders();
            //            cmdImport.Enabled = true;
            //            btnjson.Enabled = true;
            //            MessageBox.Show("Database connected successfully.", "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }



            //}
            //catch (Exception ex)
            //{

            //    gloStream.gloCCDA.CCDABusinessLayer.Classes.clsGeneral.UpdateLog("Exiting freshing settings value :" + ex.Message);
            //    MessageBox.Show("Error while refreshing settings . " + ex.Message.ToString(), "QRDA IMPORT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //public static bool IsConnect(string strSQLServerName, string strDatabase, string strUserName, string strPassword, bool check)
        //{

            //bool _bIsConnect = false;

            ////Create the object of SQL Connection class 
            //SqlConnection objCon = new SqlConnection();

            //try
            //{

            //    //Assign the connection string 


            //    if (check == false)
            //    {
            //        if (strSQLServerName.Trim() == "" || strDatabase.Trim() == "")
            //        {
            //            return false;
            //        }

            //        strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";Integrated Security=SSPI";
            //    }
            //    else
            //    {
            //        if (strSQLServerName.Trim() == "" || strDatabase.Trim() == "" || strUserName.Trim() == "" || strPassword.Trim() == "")
            //        {
            //            return false;
            //        }
            //        strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";USER id=" + strUserName + ";Password=" + strPassword;
            //    }

            //    objCon.ConnectionString = strConnectionString;

            //    //Open the connection 
            //    objCon.Open();
            //    _bIsConnect = true;
            //    //Connection to SQL Server database successfully established 

            //}
            //catch (SqlException Ex)
            //{
            //    _bIsConnect = false;
            //}
            //catch (Exception Ex)
            //{
            //    _bIsConnect = false;
            //}
            //finally
            //{
            //    objCon.Close();
            //    objCon = null;
            //}
            //return _bIsConnect;
        //}

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            try
            {

                DialogResult result = fbd.ShowDialog();
              
                if (fbd.SelectedPath != "")
                {
                  
                    files = Directory.GetFiles(fbd.SelectedPath);
                    txtPath.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fbd != null)
                {
                    fbd.Dispose();
                }

            }

        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;

           
              
            //cmdImport.Enabled = false;
            //gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
            //ReconcileList oreconsilelist = null;
            //gloCCDLibrary.gloQRDARegPatient qrdaregpatient = new gloQRDARegPatient();
            //try
            //{

            //    //gloCCDLibrary.Patient opatient= null;
            //    if (txtPath.Text == "")
            //    {
            //        MessageBox.Show("Please select Folder");
            //        return;
            //    }
            //    if (chkregprov.Checked == false)
            //    {
            //        if (cmbProviders.SelectedIndex == -1)
            //        {
            //            MessageBox.Show("Please select Provider");
            //            return;
            //        }
            //    }

               
            //    double dProgressBarValue = 0;
            //    lblProcessStatus.Text = "Importing QRDA 0 of 0";
            //    Int64 iRowCount = 0;
            //    Int64 iTotalRecords = 0;
            //    progressBar1.Value = 0;
            //    if (txtPath.Text != "")
            //    {
            //        files = Directory.GetFiles(txtPath.Text, "*.xml");
            //    }
                
            //    if (files.Length > 0)
            //    {
            //        iTotalRecords = Convert.ToInt64(files.Length);

            //        iRowCount = 1;
            //        lblTotalQRDAfiles.Text = "Total QRDA Files to Import : " + files.Length;
            //        for (int i = 0; i < files.Length; i++)
            //        {

            //            string filename = "";
            //            string actualfilename = "";
            //            actualfilename = Path.GetFileName(files[i]);
            //            lblProcessStatus.Text = "Importing QRDA " + iRowCount + " of " + iTotalRecords;
            //            lblProcessingfilename.Text = "Processing file '"+ actualfilename +"'";
            //            filename = Path.GetFullPath(files[i]);
            //            //oreconsilelist=qrdareader.ExtractCDA_DemographicsOnly(filename, Convert.ToInt64(cmbProviders.SelectedValue));
            //            oreconsilelist = qrdareader.ExtractCDA(filename, Convert.ToInt64(cmbProviders.SelectedValue),chkregprov.Checked  );
            //            qrdaregpatient.RegisterNew_Patient(oreconsilelist.mPatient, false, Convert.ToString(cmbProviders.Text), chkregprov.Checked);
                    

                    
            //            dProgressBarValue = iRowCount * 100 / iTotalRecords;

            //            if ((Convert.ToInt32(Math.Round(dProgressBarValue)) >= 0 & Convert.ToInt32(Math.Round(dProgressBarValue)) <= 100))
            //            {
            //                progressBar1.Value = Convert.ToInt32(Math.Round(dProgressBarValue));
            //                Application.DoEvents();
            //            }

            //            iRowCount += 1;
            //        }
            //        UpdateEmptypatientCode();
            //        MessageBox.Show(this, "All selected patient(s) are registered in the system.","gloEMR",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
            //    }
            //    else
            //    {
            //        MessageBox.Show("Files Not found");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    //if (files != null)
            //    //{
            //    //    files = null;
            //    //}
            //    if (qrdareader != null)
            //    {
            //        qrdareader.Dispose();
            //    }
            //    if (oreconsilelist != null)
            //    {
            //        oreconsilelist.Dispose();
            //    }
            //    this.Cursor = Cursors.Default;
            //    cmdImport.Enabled = true;
            //}
            Callnewfunction();
        }



        private List<ReconcileList>  newfunctionforCompareFiles(string[] files)
        {

            gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
            ReconcileList oreconsilelist = null;
            ReconcileList oreconsilelist1 = null;
            DataTable dtMeasureCodes = null;
            StringBuilder str = new StringBuilder();
            StringBuilder strMisMatch = new StringBuilder();
            StringBuilder strMulitpleMatch = new StringBuilder();
            DataView dv = new DataView();
            DataTable dtPatient = null;
            List<ReconcileList> lstreconsilelist = new List<ReconcileList>();
            List<ReconcileList> lsttemplist = new List<ReconcileList>();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                String Sourcefilename = "";
                String DestFilename = "";
                //if (txtSourcePath.Text.ToString() != "")
                //{

                    #region "Read files"

                    //bool _isMatched = false;
                    //gloCCDLibrary.Patient occdpatient1 = null;
                    //if (txtSourcePath.Text != "")
                    //{
                    //    files = Directory.GetFiles(txtSourcePath.Text);
                    //}



                    if ((files.Length > 0))
                    {
                        //if (files.Length > 0)
                        //{

                        strMisMatch.AppendLine("MisMatched :");
                        dtMeasureCodes = qrdareader.GetCQMMeasureCodes();
                        str.AppendLine("------------------------Start Compare-----------------------");
                        str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond);
                        bool founddata = true;
                        int innervaluecnt = 0;
                        for (int d = 0; d < files.Length; d++)
                        {
                            DestFilename = Convert.ToString(Path.GetFullPath(files[d]));
                          // qrdareader.CompareQRDA = true ;
                            oreconsilelist1 = qrdareader.ExtractCDA(DestFilename, Convert.ToInt64(cmbProviders.SelectedValue), chkregprov.Checked);
                         
                            //    oreconsilelist1 = qrdareader.ExtractCDA(DestFilename, 0);
                            founddata = true;
                            for (int m = 0; m < lsttemplist.Count ; m++)
                            {
                                founddata = true;
                                innervaluecnt = m;
                                string actualfilename = "";
                                actualfilename = Path.GetFileName(files[m]);

                                Sourcefilename = Path.GetFullPath(files[m]);
                          //      if (System.IO.Path.GetExtension(Sourcefilename) == ".xml")
                            //    {
                                    

                                    #region "Compare Files"


                                    oreconsilelist = null;
                                    oreconsilelist = lsttemplist[m];//qrdareader.ExtractCDA(Sourcefilename, Convert.ToInt64(cmbProviders.SelectedValue), chkregprov.Checked);


                                 

                                    bool _isEncounter = false;

                                    //string strmsg = "";
                                    #region Encounters
                                    if (oreconsilelist.PatProvNPI != oreconsilelist1.PatProvNPI)
                                    {
                                        founddata = false;
                                        continue;
                                    }
                                    if ((oreconsilelist.mPatient.PatientEncounters == null) && (oreconsilelist1.mPatient.PatientEncounters == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientEncounters == null) && (oreconsilelist1.mPatient.PatientEncounters != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientEncounters != null) && (oreconsilelist1.mPatient.PatientEncounters == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientEncounters != null) && (oreconsilelist1.mPatient.PatientEncounters != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientEncounters.Count != oreconsilelist1.mPatient.PatientEncounters.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }


                                       
                                        foreach (Encounters enc in oreconsilelist.mPatient.PatientEncounters)
                                        {
                                            // found = encountercount;
                                            //_isHCPCS = false;
                                            _isEncounter = false;
                                            //_isSnomed = false;
                                            foreach (Encounters enc1 in oreconsilelist1.mPatient.PatientEncounters)
                                            {


                                                if ((enc.DateOfService == enc1.DateOfService && enc.DischargeDate == enc1.DischargeDate) && (enc.EncounterCode == enc1.EncounterCode) && (enc.HcpcsCode == enc1.HcpcsCode) && (enc1.SnomedCode == enc.SnomedCode))
                                                {
                                                    _isEncounter = true;
                                                    break;


                                                }






                                            }

                                            if (_isEncounter == false)
                                            {
                                                break;

                                            }

                                        }
                                        if ((_isEncounter == false) && (oreconsilelist1.mPatient.PatientEncounters.Count > 0) && (oreconsilelist.mPatient.PatientEncounters.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }


                                    }



                                    #endregion

                                    #region Diagnosis
                                    //string strdiagmsg = "";


                                    bool _isproblem = false;

                                    if ((oreconsilelist.mPatient.PatientProblems == null) && (oreconsilelist1.mPatient.PatientProblems == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientProblems == null) && (oreconsilelist1.mPatient.PatientProblems != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientProblems != null) && (oreconsilelist1.mPatient.PatientProblems == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientProblems != null) && (oreconsilelist1.mPatient.PatientProblems != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientProblems.Count != oreconsilelist1.mPatient.PatientProblems.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }




                                        foreach (Problems Prob in oreconsilelist.mPatient.PatientProblems)
                                        {
                                            //bool isconceptidPresent = false;
                                            //bool isICDcodePresent = false;
                                            _isproblem = false;
                                            foreach (Problems Prob1 in oreconsilelist1.mPatient.PatientProblems)
                                            {
                                                if ((Prob.DateOfService == Prob1.DateOfService) && (Prob.DischargeDate == Prob1.DischargeDate) && (Prob.ConceptID == Prob1.ConceptID) && (Prob.ReasonConceptID == Prob1.ReasonConceptID) && (Prob.ICD9Code == Prob1.ICD9Code) && (Prob.ICD10Code == Prob1.ICD10Code))
                                                {
                                                    _isproblem = true;
                                                    break;

                                                }

                                            }
                                            if (_isproblem == false)
                                            {
                                                break;

                                            }


                                        }
                                        if ((_isproblem == false) && (oreconsilelist1.mPatient.PatientProblems.Count > 0) && (oreconsilelist.mPatient.PatientProblems.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }
                                    }



                                    #endregion

                                    #region History

                                    bool _isHistory = false;

                                    if ((oreconsilelist.mPatient.PatientHistory == null) && (oreconsilelist1.mPatient.PatientHistory == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientHistory == null) && (oreconsilelist1.mPatient.PatientHistory != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientHistory != null) && (oreconsilelist1.mPatient.PatientHistory == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientHistory != null) && (oreconsilelist1.mPatient.PatientHistory != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientHistory.Count != oreconsilelist1.mPatient.PatientHistory.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }



                                        foreach (gloPatientHistory Hist in oreconsilelist.mPatient.PatientHistory)
                                        {
                                            _isHistory = false;

                                            foreach (gloPatientHistory Hist1 in oreconsilelist1.mPatient.PatientHistory)
                                            {
                                                if (Hist.ConceptId == Hist1.ConceptId && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId && Hist.ICD9 == Hist1.ICD9 && Hist.ICD10 == Hist1.ICD10 && Hist.HCPCS == Hist1.HCPCS && Hist.CPT == Hist1.CPT)
                                                {
                                                    _isHistory = true;
                                                    break;

                                                }

                                            }

                                            if (_isHistory == false)
                                            {
                                                break;

                                            }

                                        }
                                        if ((_isHistory == false) && (oreconsilelist1.mPatient.PatientHistory.Count > 0) && (oreconsilelist.mPatient.PatientHistory.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }
                                    }
                                 

                                    #endregion

                                    #region "Immunizations"



                                    bool _isImmu = false;

                                    if ((oreconsilelist.mPatient.PatientImmunizations == null) && (oreconsilelist1.mPatient.PatientImmunizations == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientImmunizations == null) && (oreconsilelist1.mPatient.PatientImmunizations != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientImmunizations != null) && (oreconsilelist1.mPatient.PatientImmunizations == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientImmunizations != null) && (oreconsilelist1.mPatient.PatientImmunizations != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientImmunizations.Count != oreconsilelist1.mPatient.PatientImmunizations.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }

                                        foreach (Immunization imm in oreconsilelist.mPatient.PatientImmunizations)
                                        {

                                            _isImmu = false;

                                            foreach (Immunization imm1 in oreconsilelist1.mPatient.PatientImmunizations)
                                            {

                                                if (imm.VaccineCode == imm1.VaccineCode && (imm.ConceptID == imm1.ConceptID || imm.CPTCode == imm1.CPTCode) && imm.ImmunizationDate == imm1.ImmunizationDate && imm.ReasonConceptID == imm1.ReasonConceptID)
                                                {
                                                    _isImmu = true;
                                                    break;
                                                }

                                            }
                                            if (_isImmu == false)
                                            {
                                                break;

                                            }



                                        }
                                        if ((_isImmu == false) && (oreconsilelist1.mPatient.PatientImmunizations.Count > 0) && (oreconsilelist.mPatient.PatientImmunizations.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }


                                    }



                                    // }
                                    //else
                                    //{
                                    //    strmsg = strmsg + " Immunization Section is missing!!!!!!";
                                    //    // MessageBox.Show("Immunization Section is missing!!!!!!");
                                    //}
                                    // }
                                    // }
                                    // }
                                    //if (strmsg != "")
                                    //{
                                    //    str.AppendLine(strmsg);
                                    //}
                                    #endregion

                                    #region Medication
                                    //string strmedmsg = "";





                                    bool _isMed = false;

                                    if ((oreconsilelist.mPatient.PatientMedications == null) && (oreconsilelist1.mPatient.PatientMedications == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientMedications == null) && (oreconsilelist1.mPatient.PatientMedications != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientMedications != null) && (oreconsilelist1.mPatient.PatientMedications == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientMedications != null) && (oreconsilelist1.mPatient.PatientMedications != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientMedications.Count != oreconsilelist1.mPatient.PatientMedications.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }

                                        foreach (Medication med in oreconsilelist.mPatient.PatientMedications)
                                        {
                                            _isMed = false;
                                            foreach (Medication med1 in oreconsilelist1.mPatient.PatientMedications)
                                            {
                                                // if (!string.IsNullOrEmpty(med.RxNormCode) && !string.IsNullOrEmpty(med1.RxNormCode))
                                                //{
                                                if (med.Valueset == med1.Valueset && med.ReasonConceptID == med1.ReasonConceptID && med.StartDate == med1.StartDate && med.EndDate == med1.EndDate)
                                                {
                                                    _isMed = true;
                                                    break;
                                                }


                                            }

                                            if (_isMed == false)
                                            {
                                                break;

                                            }


                                        }

                                        if ((_isMed == false) && (oreconsilelist1.mPatient.PatientMedications.Count > 0) && (oreconsilelist.mPatient.PatientMedications.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }
                                    }






                                    #endregion

                                    #region LAbResults
                                    //if (oreconsilelist.mPatient.PatientLabResult != null)
                                    //{
                                    //    if (oreconsilelist.mPatient.PatientLabResult.Count > 0)
                                    //    {
                                    //        if (oreconsilelist1.mPatient.PatientLabResult != null)
                                    //        {
                                    //            string strlabmsg = "";
                                    //            int found = 0;
                                    //            int labcount = 0;
                                    //            if (oreconsilelist1.mPatient.PatientLabResult.Count > 0)
                                    //            {
                                 
                                bool _islabfound = false;
                                    if ((oreconsilelist.mPatient.PatientLabResult == null) && (oreconsilelist1.mPatient.PatientLabResult == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientLabResult == null) && (oreconsilelist1.mPatient.PatientLabResult != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientLabResult != null) && (oreconsilelist1.mPatient.PatientLabResult == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientLabResult != null) && (oreconsilelist1.mPatient.PatientLabResult != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientLabResult.Count != oreconsilelist1.mPatient.PatientLabResult.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }


                                        foreach (LabResults lab in oreconsilelist.mPatient.PatientLabResult)
                                        {
                                            _islabfound = false;
                                            foreach (LabResults lab1 in oreconsilelist1.mPatient.PatientLabResult)
                                            {
                                                if (lab.ResultValue == lab1.ResultValue && lab.ResultLOINCID == lab1.ResultLOINCID && lab.ResultReasonLOINC == lab1.ResultReasonLOINC && lab.ResultReasonICD9 == lab1.ResultReasonICD9 && lab.ResultReasonICD10 == lab1.ResultReasonICD10 && lab.ResultReasonConceptID == lab1.ResultReasonConceptID && lab.SpecimenDate == lab1.SpecimenDate && lab.ResultValue == lab1.ResultValue)
                                                {
                                                    _islabfound = true;
                                                    break;
                                                }
                                            }
                                            if (_islabfound == false)
                                            {
                                                break;

                                            }


                                        }
                                        if ((_islabfound == false) && (oreconsilelist1.mPatient.PatientLabResult.Count > 0) && (oreconsilelist.mPatient.PatientLabResult.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }
                                    }


                                    #endregion

                                    #region "Vitals"
                                    //strmsg = "";
                                    bool _blnvitals = false;

                                    //if (oreconsilelist.mPatient.PatientVitals != null)
                                    //{
                                    //    if (oreconsilelist.mPatient.PatientVitals.Count > 0)
                                    //    {
                                    //        if (oreconsilelist1.mPatient.PatientVitals != null)
                                    //        {
                                    //            if (oreconsilelist1.mPatient.PatientVitals.Count > 0)
                                    //            {
                                    //int vitcount = 0;
                                    //int found = 0;
                                    // strmsg = "";


                                    if ((oreconsilelist.mPatient.PatientVitals == null) && (oreconsilelist1.mPatient.PatientVitals == null))
                                    {
                                        founddata = true;
                                    }
                                    else
                                    {
                                        if ((oreconsilelist.mPatient.PatientVitals == null) && (oreconsilelist1.mPatient.PatientVitals != null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientVitals != null) && (oreconsilelist1.mPatient.PatientVitals == null))
                                        {
                                            founddata = false;
                                            continue;
                                        }
                                        if ((oreconsilelist.mPatient.PatientVitals != null) && (oreconsilelist1.mPatient.PatientVitals != null))
                                        {
                                            if ((oreconsilelist.mPatient.PatientVitals.Count != oreconsilelist1.mPatient.PatientVitals.Count))
                                            {
                                                founddata = false;
                                                continue;
                                            }
                                        }

                                        foreach (Vitals vit in oreconsilelist.mPatient.PatientVitals)
                                        {
                                            _blnvitals = false;

                                            foreach (Vitals vit1 in oreconsilelist1.mPatient.PatientVitals)
                                            {



                                                if ((Convert.ToDecimal(vit.BloodPressureSittingMin) == Convert.ToDecimal(vit1.BloodPressureSittingMin)) && vit.VitalDate == vit1.VitalDate && (Convert.ToDecimal(vit.BloodPressureSittingMax) == Convert.ToDecimal(vit1.BloodPressureSittingMax)) && (Convert.ToDecimal(vit.BMI) == Convert.ToDecimal(vit1.BMI)))
                                                {
                                                    _blnvitals = true;
                                                    break;
                                                }


                                            }
                                            if (_blnvitals == false)
                                            {
                                                break;

                                            }



                                        }

                                        if ((_blnvitals == false) && (oreconsilelist1.mPatient.PatientVitals.Count > 0) && (oreconsilelist.mPatient.PatientVitals.Count > 0))
                                        {
                                            founddata = false;
                                            continue;

                                        }

                                    }




                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    #endregion

                                    #endregion "Compare Files"

                               // }
                                //else if (dv.Count > 1)
                                //{
                                //    strMulitpleMatch.AppendLine(Path.GetFileName(files[m]));
                                //}
                                ////} dv.Count == 1
                                ////}// dv!=null


                                //if (_isMatched == false)
                                //{
                                //    strMisMatch.AppendLine(Path.GetFileName(files[m]));

                                //}
                                if (checkdupdemographics(oreconsilelist, oreconsilelist1) == false)
                                {
                                    founddata = false;
                                    continue;
                                }
                                
                                if (founddata == true)
                                {

                                    str.AppendLine("-----------------------------------------------");
                                    str.AppendLine("File " + files[m].ToString() + "   Replaces " + files[d]);
                                    str.AppendLine("-----------------------------------------------");
                                    break;
                                }

                            } //source files for end
                            lsttemplist.Add(oreconsilelist1);  
                            if (d == 0)
                            {
                                lstreconsilelist.Add(oreconsilelist1);
                                str.AppendLine(files[d].ToString());
                                str.AppendLine("");
                                // MessageBox.Show(files[d].ToString());
                            }
                            if ((founddata == false) && (d == innervaluecnt + 1))
                            {
                                lstreconsilelist.Add(oreconsilelist1);
                                str.AppendLine(files[d].ToString());
                                str.AppendLine("");
                                //      MessageBox.Show(files[d].ToString());
                            }
                        }//Dest files for end

                     //   RegisterPatient(lstreconsilelist);

                        String exceptionLogPath = Application.StartupPath + "\\Log\\CompareQRDADetails";
                        if (CreateDirectoryIfNotExists(exceptionLogPath))
                        {
                           // CMSNo++;
                            string _fileName = "";
                            _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + "-"   + ".log";
                            strMulitpleMatch.AppendLine("----------------------------------End Compare--------------------------------------");
                            //str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                            File.AppendAllText(exceptionLogPath + "\\" + _fileName, str.ToString() + "\n" + strMisMatch.ToString() + "\n" + "Multiple Matches :" + strMulitpleMatch + "\n");

                            //str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            //str.AppendLine("--------------------***End Compare***----------------------");
                            System.Diagnostics.Process.Start(exceptionLogPath + "\\" + _fileName);

                        }


                        // }
                    #endregion

                        // MessageBox.Show("Files Compared Successfully !!!");
                    }
                //}
                //else
                //{
                //    MessageBox.Show("Please select Source path to compare the files");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (files != null)
                {
                    files = null;
                }
                //if (filesdest != null)
                //{
                //    filesdest = null;
                //}
                if (dtMeasureCodes != null)
                {
                    dtMeasureCodes.Dispose();
                    dtMeasureCodes = null;
                }
                if (strMulitpleMatch != null)
                {
                    strMulitpleMatch.Clear();
                    strMulitpleMatch = null;
                }
                if (strMisMatch != null)
                {
                    strMisMatch.Clear();
                    strMisMatch = null;
                }
                if (str != null)
                {
                    str.Clear();
                    str = null;
                }
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                if (dv != null)
                {
                    dv.Dispose();
                    dv = null;
                }
                if (oreconsilelist != null)
                {
                    oreconsilelist.Dispose();
                }
                if (oreconsilelist1 != null)
                {
                    oreconsilelist1.Dispose();
                }
                if (qrdareader != null)
                {
                    qrdareader.Dispose();
                }

            }

            return lstreconsilelist;

        }
        private bool checkdupdemographics(ReconcileList r1,ReconcileList r2 )
        {
         int cntmatch=0;
         bool chkname = false; 
            //if ((r1.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName == "Christian") && (r2.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName == "Christian"))
         //{
         //    cntmatch = 0;
         //}

         //if ((r1.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName == "Stacey") && (r2.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName == "Stacey"))
         //{
         //    cntmatch = 0;
         //}
         //if (((gloCCDLibrary.PatientSupport)((new System.Collections.ArrayList.ArrayListDebugView(((System.Collections.CollectionBase)(r1.mPatient.PatientCareTeam)).InnerList)).Items[0])).PersonName._ProvNPI == ((gloCCDLibrary.PatientSupport)((new System.Collections.ArrayList.ArrayListDebugView(((System.Collections.CollectionBase)(r2.mPatient.PatientCareTeam)).InnerList)).Items[0])).PersonName._ProvNPI)
         //{
         //}
        
            if (r1.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName == r2.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName)
         {
             cntmatch += 1;
             chkname = true;
         }



         if (r1.mPatient.PatientDemographics.DemographicsDetail.PatientLastName == r2.mPatient.PatientDemographics.DemographicsDetail.PatientLastName)
         {
             cntmatch += 1;
             chkname = true;
         }
         if (chkname == false)
         {
             return false;
         }
            if( r1.mPatient.PatientDemographics.DemographicsDetail.PatientGender==r2.mPatient.PatientDemographics.DemographicsDetail.PatientGender)
                cntmatch+=1;
              if( r1.mPatient.PatientDemographics.DemographicsDetail.PatientRace==r2.mPatient.PatientDemographics.DemographicsDetail.PatientRace)
                cntmatch+=1;

              if (r1.mPatient.PatientDemographics.DemographicsDetail.PatientEthnicities == r2.mPatient.PatientDemographics.DemographicsDetail.PatientEthnicities)
                  cntmatch += 1;
               if( r1.mPatient.PatientDemographics.DemographicsDetail.PatientDOB==r2.mPatient.PatientDemographics.DemographicsDetail.PatientDOB)
                cntmatch+=1;
               
                   if (r1.mPatient.PatientDemographics.DemographicsDetail.PatientMiddleName == r2.mPatient.PatientDemographics.DemographicsDetail.PatientMiddleName)
                       cntmatch += 1;
              
            if (cntmatch >= 5)
               {
                   return true;
               }
               else
               {
                   return false;
               }

        }

        public static bool CreateDirectoryIfNotExists(string dirName)
        {
            bool exists = false;
            try
            {
                try
                {
                    exists = System.IO.Directory.Exists(dirName);
                    if (exists)
                    {
                        return true;
                    }
                }
                catch
                {
                }
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch
            {
            }
            try
            {
                exists = System.IO.Directory.Exists(dirName);
            }
            catch
            {
            }
            return exists;
        }

        
        
        private void Callnewfunction()
      {
          this.Cursor = Cursors.WaitCursor;



          cmdImport.Enabled = false;
          gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
          ReconcileList oreconsilelist = null;
          gloCCDLibrary.gloQRDARegPatient qrdaregpatient = new gloQRDARegPatient();
          try
          {

              //gloCCDLibrary.Patient opatient= null;
              if (txtPath.Text == "")
              {
                  MessageBox.Show("Please select Folder");
                  return;
              }
              if (chkregprov.Checked == false)
              {
                  if (cmbProviders.SelectedIndex == -1)
                  {
                      MessageBox.Show("Please select Provider");
                      return;
                  }
              }


              double dProgressBarValue = 0;
              lblProcessStatus.Text = "Importing QRDA 0 of 0";
              Int64 iRowCount = 0;
              Int64 iTotalRecords = 0;
              progressBar1.Value = 0;
              if (txtPath.Text != "")
              {
                  files = Directory.GetFiles(txtPath.Text, "*.xml");
              }

              if (files.Length > 0)
              {
                  iTotalRecords = Convert.ToInt64(files.Length);
                 
               List<ReconcileList> objlstrec=    newfunctionforCompareFiles(files);
              
                  iRowCount = 1;
                  lblTotalQRDAfiles.Text = "Total QRDA Files to Import : " + objlstrec.Count ;
                  iTotalRecords = objlstrec.Count; 
                  for (int i = 0; i < objlstrec.Count ; i++)
                  {

                      //string filename = "";
                      //string actualfilename = "";
                      //actualfilename = Path.GetFileName(files[i]);
                      lblProcessStatus.Text = "Importing QRDA " + iRowCount + " of " + objlstrec.Count;
                     // lblProcessingfilename.Text = "Processing file '" + actualfilename + "'";
                   //   filename = Path.GetFullPath(files[i]);
                      //oreconsilelist=qrdareader.ExtractCDA_DemographicsOnly(filename, Convert.ToInt64(cmbProviders.SelectedValue));
                    //  oreconsilelist = qrdareader.ExtractCDA(filename, Convert.ToInt64(cmbProviders.SelectedValue), chkregprov.Checked);
                      qrdaregpatient.RegisterNew_Patient(objlstrec[i].mPatient, false, Convert.ToString(cmbProviders.Text), chkregprov.Checked, objlstrec[i].RootguidID );



                      dProgressBarValue = iRowCount * 100 / iTotalRecords;

                      if ((Convert.ToInt32(Math.Round(dProgressBarValue)) >= 0 & Convert.ToInt32(Math.Round(dProgressBarValue)) <= 100))
                      {
                          progressBar1.Value = Convert.ToInt32(Math.Round(dProgressBarValue));
                          Application.DoEvents();
                      }

                      iRowCount += 1;
                  }
                  UpdateEmptypatientCode();
                  MessageBox.Show(this, "All selected patient(s) are registered in the system.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);

              }
              else
              {
                  MessageBox.Show("Files Not found");
              }
          }
          catch (Exception ex)
          {

              MessageBox.Show(ex.Message);
          }
          finally
          {
              //if (files != null)
              //{
              //    files = null;
              //}
              if (qrdareader != null)
              {
                  qrdareader.Dispose();
              }
              if (oreconsilelist != null)
              {
                  oreconsilelist.Dispose();
              }
              this.Cursor = Cursors.Default;
              cmdImport.Enabled = true;
          }
      
      }

        private void Form1_Load(object sender, EventArgs e)
        {

           
                  

            //if (IsNetworkPath(Application.StartupPath.ToString())==true) 
            //{
            //    MessageBox.Show("Copy the application to your local folder and then run it.");
            //    Application.Exit(); 
            //}
            FillProviders();
            btnCompareQRDA.Enabled = true;
            cmdImport.Enabled = true;
            btnjson.Enabled = true;
            btnExportProvider.Enabled = true;
         //   chkImportCQMData.Visible = true ;
            //if (GetRegistrySettings() == true)
            //{
            //    btnConnect_Click(null, null);
            //}
      

        }

        public bool IsNetworkPath(string path)
        {
            if (!path.StartsWith(@"/") && !path.StartsWith(@"\"))
            {
                string rootPath = System.IO.Path.GetPathRoot(path); // get drive's letter
                System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(rootPath); // get info about the drive
                return driveInfo.DriveType == DriveType.Network; // return true if a network drive
            }

            return true; // is a UNC path
        }

        private bool GetRegistrySettings()
        {
            Microsoft.Win32.RegistryKey regKey = null;
           ClsEncryption objEncryption = null;
            bool _Success = false;

            try
            {
                if ((Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\gloEMR", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl) != null))
                {
                    regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\gloEMR", true);
                    if (regKey.GetValue("SQLServer") != null && regKey.GetValue("Database") != null && regKey.GetValue("SQLUserEMR") != null && regKey.GetValue("SQLPasswordEMR") != null)
                    {
                        txtDatabase.Text = (String)regKey.GetValue("Database");
                        txtServer.Text = (String)regKey.GetValue("SQLServer");
                        txtUser.Text = (String)regKey.GetValue("SQLUserEMR");

                        objEncryption = new ClsEncryption();
                        txtPassword.Text = objEncryption.DecryptFromBase64String(Convert.ToString(regKey.GetValue("SQLPasswordEMR")), "12345678");
                        //clsEmdeonGeneral.sConnectionString = "SERVER=" + clsEmdeonGeneral.gstrgloEMRSQLServerName + ";DATABASE=" + clsEmdeonGeneral.gstrgloEMRDatabaseName + ";USER id=" + clsEmdeonGeneral.gstrUserId + ";Password=" + clsEmdeonGeneral.gstrPassword;
                        _Success = true;
                    }
                }
                else
                {
                    MessageBox.Show("gloEMR registry not found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Success = false;
            }
            finally
            {
                if (objEncryption != null) { objEncryption.Dispose(); objEncryption = null; }
                if (regKey != null) { regKey.Close(); regKey = null; }
            }

            return _Success;
        }

        private void btnjson_Click(object sender, EventArgs e)
        {
            frmMasterPatient frmobj = new frmMasterPatient();
            frmobj._Connstring = strConnectionString; 
            frmobj.ShowDialog(); 
        }

        private void btnCompareQRDA_Click(object sender, EventArgs e)
        {
            CompareQRDA frm = new CompareQRDA();
            frm.ShowDialog();
        }

        private void chkShowJson_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowJson.Checked)
            {
                btnCompareQRDA.Visible = true;
                btnjson.Visible = true;
                btnExportProvider.Visible = true;
            }
            else 
            {
                btnCompareQRDA.Visible = false;
                btnjson.Visible = false;
                btnExportProvider.Visible = false;
            }
        }
        private void btnExportProvider_Click(object sender, EventArgs e)
        {
            frmExportProviders frm = new frmExportProviders();
            frm.ShowDialog();
        }
        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloQRDAImport.Properties.Resources.Img_Orange;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloQRDAImport.Properties.Resources.Img_Button;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void cmbImportCQMData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImportCQMData .Checked)
            {
                tsb_ImportCQM .Visible = true;
               // btnjson.Visible = true;
            }
            else
            {
                tsb_ImportCQM.Visible = false;
            }
        }

        private void tsb_ImportCQM_Click(object sender, EventArgs e)
        {
            frmImportCQMData frm = new frmImportCQMData(strConnectionString);
            frm.ShowDialog();
        }
        public void UpdateEmptypatientCode()
        {
          
            SqlConnection con = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "gsp_updateQRDAPatientcode";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Dispose();
                con.Close();
                cmd.Dispose();

            }

        }

        private void tlbbtn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            }

    public class ClsEncryption : IDisposable
    {
        private byte[] _key;
        private byte[] _iv = { 18, 52, 86, 120, 144, 171, 205, 239 };

        #region Constructor and Destructor
        public ClsEncryption()
        {

        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~ClsEncryption()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>
        /// This method accepts the string to encrypt and encryption key
        /// and returns corresponding Encrypted string 
        /// </summary>
        /// <param name="stringToEncrypt"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string EncryptToBase64String(string stringToEncrypt, string encryptionKey)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
                //convert our input string to a byte array
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // Encrypt the bytearray
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string DecryptFromBase64String(string stringToDecrypt, string decryptionKey)
        {
            try
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length];
                _key = Encoding.UTF8.GetBytes(decryptionKey.Substring(0, 8));
                inputByteArray = Convert.FromBase64String(stringToDecrypt);

                MemoryStream ms = new MemoryStream();
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
      



    }
}
