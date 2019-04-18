using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEDocumentV3.Forms
{
    public partial class frmEDocEvent_AuthorizeDlg : Form
    {
        #region " Variable Declarations "

        private DialogResult _oDlgResult = DialogResult.None;
        private string _Password = "";
        bool _PassworkCheck = false;
        private string _FileName = "";
        #endregion 

        public DialogResult DlgResult
        {
            get { return _oDlgResult; }
            set { _oDlgResult = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        #region " Property Procedures "



        #endregion 

        pdftron.PDF.PDFDoc _oPDFDocument;

        public frmEDocEvent_AuthorizeDlg(pdftron.PDF.PDFDoc oPDFDocument,string oFileName)
        {
            _oPDFDocument = oPDFDocument;
            _FileName = oFileName;
            InitializeComponent();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != "")
            {
                try
                {
                    _PassworkCheck = _oPDFDocument.InitStdSecurityHandler(txtPassword.Text.Trim());
                }
                catch (Exception)
                {

                    //Intetionally left Blank
                }
                

                if (_PassworkCheck == true)
                {
                    _Password = txtPassword.Text.Trim();
                    DlgResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The password is incorrect.Please make sure that Caps Lock is not on by" + Environment.NewLine +"mistake, and try again.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter the password", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            _Password = "";
            DlgResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmEDocEvent_AuthorizeDlg_Load(object sender, EventArgs e)
        {
            lblFileName.Text = "'" + _FileName + "' is protected.";
        }
    }
}