using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloAccountsV2
{
    public partial class frmCGPaymentPlanList : Form
    {
        public frmCGPaymentPlanList()
        {
            InitializeComponent();
        }
        public string PaymentPlanID { get; set; }
        public Int64 PatientID { get; set; }
        public Int64 PAccountID { get; set; }
        public ClearGage.SSO.SsoHelper ssoHelper { get; set; }
        private void tsb_btnSaveNClose_Click(object sender, EventArgs e)
        {

            PaymentPlanID = Convert.ToString(c1PaymentPlanList.GetData(c1PaymentPlanList.RowSel, 1));
            this.Close();
        }

		private void tsb_btnClose_Click(object sender, EventArgs e)
		{
            PaymentPlanID = string.Empty;
            this.Close();
		}

        private void frmCGPaymentPlanList_Load(object sender, EventArgs e)
        {
            FillOnlineActvity();
        }
        private void FillOnlineActvity()
        {
            try
            {
                ClearGage.SSO.Patient oPatient = GetPatientInfo(PatientID);
                ClearGage.SSO.Response.PaymentPlan[] oPlans = null;
                try
                {
                    oPlans = ssoHelper.GetPaymentPlans(oPatient.PatientId);
                }
                catch (Exception ex)
                {
                }
                if (oPlans != null && oPlans.Length > 0)
                {
                    c1PaymentPlanList.DataSource = oPlans;
                }
                
                //foreach (ClearGage.SSO.Response.PaymentPlan item in plans)
                //{

                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private ClearGage.SSO.Patient GetPatientInfo(long nPatientID)
        {
            ClearGage.SSO.Patient oPat = null;
            DataTable dt = null;
            gloPatient.gloPatient oPatient = null;
            try
            {
                oPat = new ClearGage.SSO.Patient();
                oPatient = new gloPatient.gloPatient(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                dt = oPatient.GetPatientDemographics(nPatientID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        oPat.FirstName = Convert.ToString(dr["sFirstName"]);
                        oPat.LastName = Convert.ToString(dr["sLastName"]);
                        oPat.BirthDate = Convert.ToString(dr["dtDOB"]);
                        oPat.Gender = Convert.ToString(dr["sGender"]).Substring(0, 1);
                        oPat.Address1 = Convert.ToString(dr["sAddressLine1"]);
                        oPat.Address2 = Convert.ToString(dr["sAddressLine2"]);
                        oPat.City = Convert.ToString(dr["sCity"]);
                        oPat.State = Convert.ToString(dr["sState"]);
                        oPat.Zip = Convert.ToString(dr["sZip"]);
                        oPat.Ssn = Convert.ToString(dr["nSSN"]);
                        oPat.EmailAddress = Convert.ToString(dr["sEmail"]);
                        oPat.MobilePhone = Convert.ToString(dr["sMobile"]);
                        oPat.Phone = Convert.ToString(dr["sPhone"]);
                        oPat.DriversLicenseNumber = "";
                        oPat.DriversLicenseState = "";
                        oPat.PatientId = Convert.ToString(PAccountID);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return oPat;
        } 
    }
}
