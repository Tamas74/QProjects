using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Statement
{
    public partial class frmVoidStatementInfoDialog : Form
    {
        #region "Variable Declarations"

        private string _databaseconnectionstring = "";
        public DateTime _VoidDateTime = new DateTime();
        public string _UserName = "";
        public Int64 _ClinicID = 0;
        public string _VoidedNotes = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region "Constructor"

        public frmVoidStatementInfoDialog(DateTime VoidDateTime, string UserName, string VoidedNotes, int _xCord, int _yCord)
        {
            InitializeComponent();

            this.Location = new Point(_xCord - 442, _yCord);
            this.panel3.Location = new Point(_xCord - 442, _yCord); 

            #region " Retrive ClinicID from AppSettings "

            _VoidDateTime = VoidDateTime;
            lblUserName.Text = UserName;
            txtNotes.Text = VoidedNotes;
            lblVoidDate.Text = Convert.ToDateTime(VoidDateTime).ToString("MM/dd/yyyy h:mm tt");

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else 
            { _ClinicID = 1; }

            #endregion " Retrive ClinicID from AppSettings "


            _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);

        }
        
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
