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

namespace gloQRDAImport
{
    public partial class frmImportCQMData : Form
    {
        string[] files;
        gloCCDLibrary.gloCQMReader oReader;
        string strConnectionString = "";
        public frmImportCQMData(string _conn)
        {
            strConnectionString = _conn;
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtPath.Text != "")
            {
                files = Directory.GetFiles(txtPath.Text);
            }
            oReader = new gloCQMReader();
            if (files.Length > 0)
            {
              //  iTotalRecords = Convert.ToInt64(files.Length);

              //  iRowCount = 1;
              //  lblTotalQRDAfiles.Text = "Total QRDA Files to Import : " + files.Length;
                DataTable dtFinal=new DataTable ();
                dtFinal.Columns.Add("Root");
            dtFinal.Columns.Add("Extension");
            dtFinal.Columns.Add("ValuSet");
            dtFinal.Columns.Add("Title");
            dtFinal.Columns.Add("Criteria");
            dtFinal.Columns.Add("CMSID");
            dtFinal.Columns.Add("nCriteria");
           // dtFinal.Columns.Add("CMSID");
            //dtFinal.Columns.Add("AgeMin");
            //dtFinal.Columns.Add("AgeMinDetails");
            //dtFinal.Columns.Add("AgeMax");
            //dtFinal.Columns.Add("AgeMaxDetails");
            //dtFinal.Columns.Add("LineNo");
                 DataTable dtPlanDetails=new DataTable ();

                 dtPlanDetails.Columns.Add("CMSID");
                 dtPlanDetails.Columns.Add("AgeMin");
                 dtPlanDetails.Columns.Add("AgeMinDetails");
                 dtPlanDetails.Columns.Add("AgeMax");
                 dtPlanDetails.Columns.Add("AgeMaxDetails");
                 dtPlanDetails.Columns.Add("BPMin");
                 dtPlanDetails.Columns.Add("BPMax");
                 dtPlanDetails.Columns.Add("BMIMin");
                 dtPlanDetails.Columns.Add("BMIMax");

                for (int i = 0; i < files.Length; i++)
                {

                    string filename = "";
                    string actualfilename = "";
                    actualfilename = Path.GetFileName(files[i]);
                    //lblProcessStatus.Text = "Importing QRDA " + iRowCount + " of " + iTotalRecords;
                   // lblProcessingfilename.Text = "Processing file '" + actualfilename + "'";
                    filename = Path.GetFullPath(files[i]);
                    oReader.ExtractCQMEmeasure(filename, 0, dtFinal, Path.GetFileNameWithoutExtension(files[i]), dtPlanDetails);
                  
                    //oreconsilelist=qrdareader.ExtractCDA_DemographicsOnly(filename, Convert.ToInt64(cmbProviders.SelectedValue));
                    //oreconsilelist = qrdareader.ExtractCDA(filename, Convert.ToInt64(cmbProviders.SelectedValue), chkregprov.Checked);
                   // qrdaregpatient.RegisterNew_Patient(oreconsilelist.mPatient, false, Convert.ToString(cmbProviders.Text), chkregprov.Checked);


                  //  dProgressBarValue = iRowCount * 100 / iTotalRecords;

                    //if ((Convert.ToInt32(Math.Round(dProgressBarValue)) >= 0 & Convert.ToInt32(Math.Round(dProgressBarValue)) <= 100))
                    //{
                    //    progressBar1.Value = Convert.ToInt32(Math.Round(dProgressBarValue));
                    //    Application.DoEvents();
                    //}

                    //iRowCount += 1;
                }
                dtFinal.Columns.Remove("Root");
                dtFinal.Columns.Remove("Extension");
              
                dtFinal.Columns[0].ColumnName = "ValueSet";
                   //DataRow [] drRow = dtFinal.Select("Title= 'Patient Characteristic Birthdate'");
                   //if (drRow.Length > 0)
                   //{
                   //    for (Int16 i = 0; i <= drRow.Length - 1; i++)
                   //    {
                   //        dtFinal.Rows.Remove(drRow[i]);
                   //        break;
                   //    }

                   //}
                   dtFinal.Columns.Remove("Title");
                for(Int16 i=0 ; i<=dtPlanDetails .Rows .Count-1;i++)
                {
                    if (Convert.ToString ( dtPlanDetails.Rows [i]["CMSID"])=="69")

                {
                    dtPlanDetails.Rows[i]["BMIMin"] = Convert.ToDecimal ("18.5");
                    dtPlanDetails.Rows[i]["BMIMax"] = Convert.ToDecimal("25");
                   
                }
                    if (Convert.ToString(dtPlanDetails.Rows[i]["CMSID"]) == "22")
                    {
                        dtPlanDetails.Rows[i]["BPMax"] = Convert.ToDecimal("120");
                        dtPlanDetails.Rows[i]["BPMin"] = Convert.ToDecimal("80");
                       
                    }
                }
                 
                UpdateCQMCriteria(dtFinal,dtPlanDetails );
                MessageBox.Show("Population Criteria updated for the selected CQM's");

            }
        }
        private void UpdateCQMCriteria(DataTable dtFinal,DataTable dtPlanDetails)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(strConnectionString);

            try
            {
                gloDatabaseLayer.DBParameter oParam=null;
                gloDatabaseLayer.DBParameters oParams = new gloDatabaseLayer.DBParameters ();
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameter();
                oParam.ParameterName = "@TVP_CQMCriteria";
                oParam.ParameterDirection = ParameterDirection.Input;
                oParam.DataType = SqlDbType.Structured;
                oParam.Value = dtFinal;
                oParams.Add(oParam);
                oParam = null;

                oParam = new gloDatabaseLayer.DBParameter();
                oParam.ParameterName = "@TVP_CQMPlanCriteria";
                oParam.ParameterDirection = ParameterDirection.Input;
                oParam.DataType = SqlDbType.Structured;
                oParam.Value = dtPlanDetails ;
                oParams.Add(oParam);
                oParam = null;
                oDB.Execute("CQM_InUpCriteria", oParams);
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }
        private void btnAddSourcePath_Click(object sender, EventArgs e)
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
    }
}
