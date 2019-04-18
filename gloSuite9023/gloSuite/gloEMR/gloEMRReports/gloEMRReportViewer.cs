using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloEMRReports
{
    public partial class gloEMRReportViewer : UserControl
    {
        //private string _databaseconnectionstring = gloEMRGeneralLibrary.glogeneral.clsgeneral.GetConnectionstring(); 
        //SqlCommand _sqlcommand = null;
        //SqlConnection oConnection = new SqlConnection();
        //SqlDataAdapter da;
        //DataSet ds = new DataSet();



        #region "Delegates"

            //Delegates For Close Event
            public delegate void onReportsCloseClicked(object sender, EventArgs e);
            public event onReportsCloseClicked onReportsClose_Clicked;

            //Delegates For Generate Report Event
            public delegate void onGenerateReportClicked(object sender, EventArgs e);
            public event onGenerateReportClicked onGenerateReport_Clicked;

        #endregion


        #region "Crystal Report Properties"
       
        public object ReportViewer
        {
            //Property For Setting the Report source 
            get { return crvReportViewer.ReportSource; }
            set { crvReportViewer.ReportSource = value; }
        }

      
        #endregion


        #region "Constructors"

        public gloEMRReportViewer()
        {
            InitializeComponent();
        }

        #endregion


        #region "Events "

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
            {
                onGenerateReport_Clicked(sender, e);
            }

            private void tsb_Print_Click(object sender, EventArgs e)
            {
                crvReportViewer.PrintReport();
            }

            private void tsb_Cancel_Click(object sender, EventArgs e)
            {
                onReportsClose_Clicked(sender, e);
            }

        #endregion


        #region "ShowHide Properties"

        public Boolean showAgingCriteria
        {
            get { return pnlAgingCriteria.Visible; }
            set { pnlAgingCriteria.Visible = value; }
        }

        public Boolean showMedication
        {
            get { return pnlMedication .Visible; }
            set { pnlMedication.Visible = value; }
        }

        public String sAge
        {
            //Property For storing the FacilityCode
            get
            {

                if (cmbAgingCritera.SelectedItem != null)
                    return Convert.ToString(cmbAgingCritera.SelectedItem);
                   
                else
                    return "";

            }

            set { cmbAgingCritera.SelectedValue = value; }
        }


        public String sMedication
        {
            //Property For storing the FacilityCode
            get
            {

                if (cmbMedication.SelectedValue != null)
                    if (cmbMedication.SelectedValue.ToString() != "")
                        return cmbMedication.SelectedValue.ToString();
                    else
                        return "";
                else
                    return "";

            }

            set { cmbMedication.SelectedValue = value; }
        }

        #endregion


        #region "Fill Methods"

         
        private void FillAging()
        {
            cmbAgingCritera.Items.Clear();
            cmbAgingCritera.Items.Add("");
            cmbAgingCritera.Items.Add("> 20");
            cmbAgingCritera.Items.Add("< 20");

        }


       
        private void FillMedication()
        {
            SqlCommand _sqlcommand = null;
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {

                oConnection.ConnectionString = gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetConnectionstring().ToString();

                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = " SELECT  distinct sMedication FROM Medication order by sMedication";


                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(ds);

                

                DataTable dtMedication;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtMedication = new DataTable();
                    dtMedication.Columns.Add("sMedication");

                    dtMedication.Clear();
                    dtMedication.Rows.Add("");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dtMedication.Rows.Add(ds.Tables[0].Rows[i]["sMedication"]);
                    }

                    cmbMedication.DataSource = dtMedication;
                    cmbMedication.DisplayMember = "sMedication";
                    cmbMedication.ValueMember = "sMedication";
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null)
                {
                    oConnection.Close();
                    oConnection.Dispose();
                    oConnection = null;
                }
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                
            }
        }

        #endregion


        #region "Dictionary"

       

        #endregion


        #region "Form Events"

        private void gloEMRReportViewer_Load(object sender, EventArgs e)
        {
            FillAging();
            FillMedication();
        }

        private void gloEMRReportViewer_Paint(object sender, PaintEventArgs e)
        {
            AdjustCriteriaPanelHight();
        }

        private void AdjustCriteriaPanelHight()
        {
            try
            {
                Int32 FlowPanelHight = 0;
                for (Int32 i = 0; i < fpnlCriteria.Controls.Count; i++)
                {
                    if (fpnlCriteria.Controls[i].Visible == true)
                    {
                        if (FlowPanelHight < (fpnlCriteria.Controls[i].Location.Y + fpnlCriteria.Controls[i].Height))
                            FlowPanelHight = fpnlCriteria.Controls[i].Location.Y + fpnlCriteria.Controls[i].Height;
                    }
                }
                fpnlCriteria.Height = FlowPanelHight + 5;
                fpnlCriteria.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

       


    }
}
