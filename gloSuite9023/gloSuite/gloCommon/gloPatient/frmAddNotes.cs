using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//test1
namespace gloPatient
{
    public partial class frmAddNotes : Form
    {

        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        string _nNote;
        

        public string Note
        {
            get { return _nNote; }
            set { _nNote = value; }
        }
  


 


        public frmAddNotes(String Note,string FormText)
        {
            InitializeComponent();

            if (FormText == "")
            {
                this.Text = "Add Notes";
            }
            else
            {
                this.Text = FormText + " Notes";
            }
            _nNote = Note;
            txtNotes.Text = Note;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
         
        }

       

        private void frmAddNotes_Load(object sender, EventArgs e)
        {
            
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {


            _nNote = txtNotes.Text;
            this.Close(); 
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

      
    }
}