using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Collections
{
    public partial class frmAutoFollowupUtility : Form
    {
        string _databaseconnectionstring;
        string _messageBoxCaption;
        DataTable dtAccounts = new DataTable();
        DataTable dtClaims = new DataTable();
        public frmAutoFollowupUtility()
        {
            InitializeComponent();
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
           
        }

        private void GetDataToProcess()
        {
           
            clsFollowUpUtility FollowUpUtility = new clsFollowUpUtility();           
            dtAccounts = FollowUpUtility.GetAllAccounts();        
            //ProcessAccounts(dtAccounts);         
        }


        

        private void ProcessAccounts(DataTable dtAccounts)
        {
            
            if (dtAccounts != null && dtAccounts.Rows.Count > 0)
            {
                clsFollowUpUtility FollowUpUtility = new clsFollowUpUtility();               
                DataTable dtSettings = FollowUpUtility.GetDefaultAccFollowupSetting();
                int StmtCountSet=0;
                int days=0; 
                string action=string.Empty  ;
                string actionDescription = string.Empty;

                if (dtSettings != null && dtSettings.Rows.Count > 0)  
                {
                    StmtCountSet=Convert.ToInt32(dtSettings.Rows[0][0]);
                    days= Convert.ToInt32(dtSettings.Rows[0][1]);
                    action=dtSettings.Rows[0][2].ToString();
                    actionDescription = dtSettings.Rows[0][3].ToString();
                }
                this.Cursor  = Cursors.WaitCursor;
                Int64 count =dtAccounts.Rows.Count ;
                for (int i = 0; i < dtAccounts.Rows.Count ; i++)
                {
                    lblStatus.Text = "Processing Account " + (i + 1).ToString() + " out of " + count.ToString();  
                    FollowUpUtility.ProcessAccount(Convert.ToInt64(dtAccounts.Rows[i][0].ToString()), StmtCountSet, days, action, actionDescription);                 
                    ProgressBar.Value = i + 1;
                    Application.DoEvents();
                }

            }
            lblAccountStatus.Text = "Completed";    
            Cursor.Current = Cursors.Default;
            btnAccount.Image = global::gloBilling.Properties.Resources.tick;     
            GetClaimData();
        }
        private void ProcessClaims(DataTable dtClaims)
        {           
            if (dtClaims != null && dtClaims.Rows.Count > 0)
            {
                clsFollowUpUtility FollowUpUtility = new clsFollowUpUtility();
                DataTable dtSettings = FollowUpUtility.GetDefaultClaimsFollowupSetting();
          
                int billdays = 0;
                string billaction = string.Empty;
                string billactionDescription = string.Empty;

                int rebilldays = 0;
                string rebillaction = string.Empty;
                string rebillactionDescription = string.Empty;

                if (dtSettings != null && dtSettings.Rows.Count > 0)
                {
                    billdays = Convert.ToInt32(dtSettings.Rows[0][0]);
                    billaction = dtSettings.Rows[0][1].ToString();
                    billactionDescription = dtSettings.Rows[0][2].ToString();

                    rebilldays = Convert.ToInt32(dtSettings.Rows[0][3]);
                    rebillaction = dtSettings.Rows[0][4].ToString();
                    rebillactionDescription = dtSettings.Rows[0][5].ToString();
                }
                this.Cursor = Cursors.WaitCursor;
                Int64 count = dtClaims.Rows.Count;
                for (int i = 0; i < dtClaims.Rows.Count  ; i++)
                {
                    lblStatus.Text = "Processing Claim " + (i + 1).ToString() + " out of " + count.ToString(); 
                    FollowUpUtility.ProcessClaim(Convert.ToInt64(dtClaims.Rows[i][0].ToString()),Convert.ToInt64(dtClaims.Rows[i][1].ToString()), Convert.ToBoolean(dtClaims.Rows[i][2].ToString()), billdays,  billaction, billactionDescription, rebilldays, rebillaction, rebillactionDescription);
                    ProgressBar.Value = i + 1;
                    Application.DoEvents();
                }

            }
            lblClaimStatus.Text = "Completed";           
            btnClaim.Image = global::gloBilling.Properties.Resources.tick;
            Application.DoEvents();
        }

        private void GetClaimData()
        {
            lblClaimStatus.Text = "In Progess";
            ProgressBar.Maximum = 0;
            clsFollowUpUtility FollowUpUtility = new clsFollowUpUtility();
            dtClaims = FollowUpUtility.GetAllClaims();
            if (dtClaims != null && dtClaims.Rows.Count > 0)
            {
                ProgressBar.Maximum = dtClaims.Rows.Count;
                lblStatus.Text = "Processing Claims";
                ProcessClaims(dtClaims);
                FinishProcess();
            }
        }

        private void FinishProcess()
        {           
            Cursor.Current = Cursors.Default;
            System.Threading.Thread.Sleep(2000); 
            this.Close();  
        }

        private void frmAutoFollowupUtility_Shown(object sender, EventArgs e)
        {
            GetDataToProcess();
            if (dtAccounts != null && dtAccounts.Rows.Count > 0)
            {
                ProgressBar.Maximum = dtAccounts.Rows.Count;
                lblStatus.Text = "Processing Accounts";
            }
            ProcessAccounts(dtAccounts);
        }

        private void frmAutoFollowupUtility_Load(object sender, EventArgs e)
        {
            lblAccount.Focus(); 

        }

    }
}
