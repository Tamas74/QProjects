using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmSelectGuarantor : Form
    {
        gloPatient.Patient oPat = new gloPatient.Patient();
        string Connectionstring = "";
        Int64 Patientid = 0;
        public Int64 nPAcnt = 0;
        public frmSelectGuarantor()
        {
            InitializeComponent();
        }

        public frmSelectGuarantor(Int64 PatID, gloPatient.Patient oPatient,string Con)
        {
            InitializeComponent();
            oPat = oPatient;
            Connectionstring = Con;
            Patientid = PatID;
        }

        private void frmSelectGuarantor_Load(object sender, EventArgs e)
        {
            lblPatName.Text=oPat.DemographicsDetail.PatientFirstName.ToString()+" "+oPat.DemographicsDetail.PatientLastName.ToString();
            FillCombo();
        }
        private void FillCombo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(Connectionstring);
            DataTable dt=new DataTable();
            string strQry = "SELECT DISTINCT PA_Accounts.nPAccountID,sAccountNo +' - '+PA_Accounts.sFirstName +''+CASE ISNULL(PA_Accounts.sMiddleName,'') WHEN '' THEN '' ELSE +' '+PA_Accounts.sMiddleName END +' '+PA_Accounts.sLastName as Name FROM Patient_OtherContacts INNER JOIN PA_Accounts  ON dbo.Patient_OtherContacts.nPAccountID = dbo.PA_Accounts.nPAccountID WHERE Patient_OtherContacts.nPAccountID IN (SELECT nPAccountID  FROM PA_Accounts_Patients WHERE nPatientID ="+Patientid+")";
            try
            { 
                oDB.Connect(false);
                oDB.Retrive_Query(strQry,out dt);
                if (dt.Rows.Count > 1)
                {
                    GuarantorInfo[] GInfo = new GuarantorInfo[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GInfo[i] = new GuarantorInfo(dt.Rows[i]["Name"].ToString(),Convert.ToInt64( dt.Rows[i]["nPAccountID"]));
                    }

                    cmbGuarantor.DataSource = GInfo;
                    cmbGuarantor.DisplayMember = "GuarantorName";
                    cmbGuarantor.ValueMember = "GuarantorID";
                    cmbGuarantor.SelectedIndex = 0;
                }
            }
            catch //(Exception exc)
            {

            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            nPAcnt =Convert.ToInt64(cmbGuarantor.SelectedValue);
            this.Close();
        }
    }

    public class GuarantorInfo
    {
        public string GuarantorName { get; set; }
        public Int64 GuarantorID { get; set; }

        public GuarantorInfo(string str, Int64 Id)
        {
            this.GuarantorName = str;
            this.GuarantorID = Id;
        }

        public GuarantorInfo()
        {
        }
    }
}
