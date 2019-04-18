using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C1.Win.C1FlexGrid;

namespace gloBilling.WC_Forms
{
    public partial class frmWCPatientClaims : Form
    {

        #region " Variable Declarations "
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;
        private string _UserName = string.Empty;

        public string _databaseconnectionstring = string.Empty;
        public string _messageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        DataView dvClaims = new DataView();
        DataTable dtClaims = new DataTable();
        //DataView dvNext = new DataView();

        //BEGIN : C1 constants for Grid columns
        //private const int COL_PAT_ID = 0;
        //private const int COL_PAT_Code = 1;
        //private const int COL_PAT_FirstName = 2;
        //private const int COL_PAT_MI = 3;
        //private const int COL_PAT_LastName = 4;
        //private const int COL_PAT_Provider = 5;
        //private const int COL_PAT_SSN = 6;
        //private const int COL_PAT_DOB = 7;
        //private const int COL_PAT_Phone = 8;
        //private const int COL_PAT_Mobile = 9;
        //END : C1 constants for Grid columns

        #endregion " Variable Declarations "


        #region " Property Procedures "

        public Int64 TransactionID { get; set; }
        public Int64 TransactionMasterID { get; set; }
        public string ClaimNo { get; set; }
        public bool flgOk { get; set; }
        public Int64 PatientId { get; set; } 
        
        #endregion " Property Procedures "

        Regex CheckPattern = new Regex(@"^[0-9-]+$");

        public frmWCPatientClaims(Int64 PatientID)
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            InitializeComponent();

            PatientId = PatientID;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
            }
            else
            { _UserID = 0; }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]); }
            }
            else
            { _UserName = ""; }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                { _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]); }
                else
                { _messageBoxCaption = ""; }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

        }

        private void frmWCPatientClaims_Load(object sender, EventArgs e)
        {
            txtClaimNo.Focus();
            txtClaimNo.Select();

            gloC1FlexStyle.Style(c1Claims);

            LoadClaims();
            this.Text = "Patient Claims";
        }


        private void LoadClaims()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {

                //Search Patient in database
                oDB.Connect(false);

                dtClaims = GetClaimsForPatient(PatientId);

                if (dtClaims != null)
                {
                    gloGlobal.gloPMGlobal.SplitClaimColumn(dtClaims, dtClaims.Columns.IndexOf("Claim"));

                    dvClaims = dtClaims.DefaultView;

                    dvClaims.Sort = "SortClaim Desc,SortSubClaim ASC";

                    c1Claims.DataSource = dvClaims;
                    //dtClaims.Dispose();
                }

                DesignClaimGrid();
                c1Claims.Visible = true;



                if (c1Claims.Rows.Count > 1)
                {
                    c1Claims.Select(1, 0);
                }
                c1Claims.Focus();

            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //if (dtClaims != null) { dtClaims.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

        }

        public void DesignClaimGrid()
        {
            try
            {
                c1Claims.Hide();

                c1Claims.Cols["TransactionMasterID"].Visible = false;
                c1Claims.Cols["TransactionID"].Visible = false;
                c1Claims.Cols["MasterAppointmentID"].Visible = false;
                c1Claims.Cols["AppointmentID"].Visible = false;
                c1Claims.Cols["ClaimNo"].Visible = false;
                c1Claims.Cols["Claim"].Visible = true;
                c1Claims.Cols["SubClaimNo"].Visible = false;
                c1Claims.Cols["bAutoClaim"].Visible = false;
                c1Claims.Cols["ReferralID"].Visible = false;
                c1Claims.Cols["bIsOpened"].Visible = false;
                c1Claims.Cols["ContactID"].Visible = false;
                c1Claims.Cols["ClinicID"].Visible = false;
                c1Claims.Cols["PatientID"].Visible = false;
                c1Claims.Cols["TransactionProviderID"].Visible = false;
                c1Claims.Cols["Status"].Visible = true;
                c1Claims.Cols["nResponsibilityNo"].Visible = false;
                c1Claims.Cols["row_number"].Visible = false;
                c1Claims.Cols["FacilityCode"].Visible = false;
                c1Claims.Cols["PatientCode"].Visible = false;
                
                


                c1Claims.Cols["Date"].Visible = false;
                c1Claims.Cols["Facility"].Visible = true;
                c1Claims.Cols["ReferralID"].Visible = false;
                c1Claims.Cols["ProviderFName"].Visible = true;
                c1Claims.Cols["ProviderMName"].Visible = true;
                c1Claims.Cols["ProviderLName"].Visible = true;
                c1Claims.Cols["PatientFName"].Visible = false;
                c1Claims.Cols["PatientMName"].Visible = false;
                c1Claims.Cols["PatientLName"].Visible = false;
                c1Claims.Cols["InsuranceID"].Visible = true;
                c1Claims.Cols["Insurance"].Visible = true;
                c1Claims.Cols["Status"].Visible = false;
                c1Claims.Cols["SortClaim"].Visible = false;
                c1Claims.Cols["SortSubClaim"].Visible = false;

                c1Claims.Cols["TransactionMasterID"].Width = 0;
                c1Claims.Cols["TransactionID"].Width = 0;
                c1Claims.Cols["MasterAppointmentID"].Width = 0;
                c1Claims.Cols["AppointmentID"].Width = 0;
                c1Claims.Cols["ClaimNo"].Width = 0;
                c1Claims.Cols["Claim"].Width = 80;
                c1Claims.Cols["SubClaimNo"].Width = 0;
                c1Claims.Cols["bAutoClaim"].Width = 0;
                c1Claims.Cols["ReferralID"].Width = 0;
                c1Claims.Cols["bIsOpened"].Width = 0;
                c1Claims.Cols["ContactID"].Width = 0;
                c1Claims.Cols["ClinicID"].Width = 0;
                c1Claims.Cols["PatientID"].Width = 0;
                c1Claims.Cols["TransactionProviderID"].Width = 0;
                c1Claims.Cols["Status"].Width = 0;
                c1Claims.Cols["nResponsibilityNo"].Width = 0;
                c1Claims.Cols["row_number"].Width = 0;
                c1Claims.Cols["FacilityCode"].Width = 0;

                c1Claims.Cols["Date"].Width = 80;
                c1Claims.Cols["Facility"].Width = 150;
                c1Claims.Cols["ReferralID"].Width = 0;
                c1Claims.Cols["ProviderFName"].Width = 100;
                c1Claims.Cols["ProviderMName"].Width = 80;
                c1Claims.Cols["ProviderLName"].Width = 100;
                c1Claims.Cols["PatientFName"].Width = 0;
                c1Claims.Cols["PatientMName"].Width = 0;
                c1Claims.Cols["PatientLName"].Width = 0;
                c1Claims.Cols["InsuranceID"].Width = 0;
                c1Claims.Cols["Insurance"].Width = 200;
                c1Claims.Cols["SortClaim"].Width = 0;
                c1Claims.Cols["SortSubClaim"].Width = 0;

                c1Claims.Cols["Date"].Caption = "Date";
                c1Claims.Cols["Claim"].Caption = "Claim #";
                c1Claims.Cols["Facility"].Caption = "Facility";
                c1Claims.Cols["ReferralID"].Caption = "ReferralID";
                c1Claims.Cols["ProviderFName"].Caption = "Provider First";
                c1Claims.Cols["ProviderMName"].Caption = "MI";
                c1Claims.Cols["ProviderLName"].Caption = "Last";
                c1Claims.Cols["PatientFName"].Caption = "Patient FN";
                c1Claims.Cols["PatientMName"].Caption = "Patient MN";
                c1Claims.Cols["PatientLName"].Caption = "Patient LN";
                c1Claims.Cols["InsuranceID"].Caption = "Insurance ID";
                c1Claims.Cols["Insurance"].Caption = "Insurance Plan";
                c1Claims.Cols["DOS"].Caption = "DOS";
                c1Claims.Cols["SortClaim"].Caption = "SortClaim";
                c1Claims.Cols["SortSubClaim"].Caption = "SortSubClaim";
                
                //c1Claims.Cols["Claim"].Sort = C1.Win.C1FlexGrid.SortFlags.Ascending;

                for (int i = 0; i < c1Claims.Rows.Count; i++)
                {
                    c1Claims.Rows[i].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom;
                }
                for (int i = 1; i < c1Claims.Cols.Count; i++)
                {
                    c1Claims.Cols[i].AllowEditing = false;
                }
                c1Claims.Cols["Date"].Format = "MM/dd/yyyy";
                c1Claims.Cols["DOS"].Format = "MM/dd/yyyy";

                c1Claims.Cols["DOS"].Move(0);



                gloC1FlexStyle.Style(c1Claims, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1Claims.Show();
            }

        }
        private DataTable GetClaimsForPatient(Int64 _nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtClaims = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_ClaimsForPatient", oDBParameters, out dtClaims);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtClaims;
        }

       private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            flgOk = false;
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void SelectClaim()
        {
            try
            {
                if (c1Claims.Rows.Count >= 0 && c1Claims.RowSel > 0)
                {
                    flgOk = true;
                    TransactionID = Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                    TransactionMasterID = Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));
                    ClaimNo = Convert.ToString(c1Claims.GetData(c1Claims.RowSel, 5));

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Claim not selected in order to load billing data.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { }
        }

        private void tsb_SaveAndClose_Click(object sender, EventArgs e)
        {
            SelectClaim();
        }

        private void c1Claims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rowDoubleClicked = c1Claims.HitTest(e.X, e.Y).Row;
            if (rowDoubleClicked == c1Claims.RowSel)
            {
                SelectClaim();
            }
        }

        private void txtClearSearch_Click(object sender, EventArgs e)
        {
            txtClaimNo.Clear();
            txtClaimNo.Focus();
        }

        private void txtClaimNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtClaimNo.Text != "")
                {
                    if (CheckPattern.IsMatch(txtClaimNo.Text.Trim()))
                    {
                        dvClaims.RowFilter = "Claim like '" + txtClaimNo.Text.Replace("'", "''") + "%'";
                    }
                    else
                    {
                        MessageBox.Show("Only numeric values allowed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClaimNo.Focus();
                        return;
                    }
                }
                else
                { dvClaims.RowFilter = String.Empty; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { }
        }

        private void c1Claims_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            try
            {
                if (c1Claims.DataSource != null)
                {
                    if (e.Col == c1Claims.Cols["Claim"].Index)
                    {
                        c1Claims.Cols[c1Claims.Cols.Count - 2].Sort = e.Order;
                        c1Claims.Cols[c1Claims.Cols.Count - 1].Sort = SortFlags.Ascending;

                        c1Claims.Sort(SortFlags.UseColSort, c1Claims.Cols.Count - 2, c1Claims.Cols.Count - 1);
                    }
                }
            }
            catch (Exception ex)
            {                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }            
        }

    }
}
