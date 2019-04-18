using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using gloCommunity.Classes;

namespace gloCommunity
{
    public partial class frmEmailConfig : Form
    {
        string _MessageBoxCaption = "gloEMR";
        public frmEmailConfig()
        {
            InitializeComponent();
        }

        private void frmEmailConfig_Load(object sender, EventArgs e)
        {

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Please enter the E-mail", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            if (CheckEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter valid E-mail address", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            SearchResult result = null;
            try
            {
                //Set user email code
                result = clsGetADUser.GetADuser();
                if (result != null && result.Properties.Count > 0)
                {
                    DirectoryEntry entryToUpdate = result.GetDirectoryEntry();
                    entryToUpdate.Properties["mail"].Value = txtEmail.Text;
                    entryToUpdate.CommitChanges();
                    this.Close();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show("Windows Login User Does Not have Rights to Add E-mail Address in Active Directory.\nPlease Contact System Administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {                
                if (result != null)
                    result = null;
            }

        }

        private void frmEmailConfig_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private bool CheckEmail(string EmailAddress)
        {
            string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern))

            { return true; }
            return false;
        }
    }
}
