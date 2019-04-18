using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloTaskMail
{
    partial class frmSignature : Form
    {

        #region "Declarations"

        //private string _messageBoxCaption = "gloPM";

        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        private string _databaseconnectionstring = ""; //"Server=sakarserver;Database=gloPMSData_20080130;Uid=sa;Pwd=sasakar;";
        private Int64 _nUserId = 0;
        private Int64 _ClinicID = 0;
        private Int64 _nSignatureID = 0;
        gloEditor.gloEditorControl TextControl;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Declarations"

        #region "Enumeration DataType"

        public enum SignatureDefault
        {
            True = 1,
            False = 0
        }

        public enum CategoryType
        {
            Task = 0,
            Mail = 1,
        }



        #endregion "Enumeration DataType"

        #region "Propery Procedures"

        private string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        private Int64 UserID
        {
            get { return _nUserId; }
            set { _nUserId = value; }
        }

        #endregion "Propery Procedures"

        #region "Constructor & Destructor "

        public frmSignature(string databaseConnectionString)
        {
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _nUserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _nUserId = 0; }
            }
            else
            { _nUserId = 0; }

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            _databaseconnectionstring = databaseConnectionString;
            InitializeComponent();
        }

        public frmSignature(string databaseConnectionString,Int64 SignatureID)
        {
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _nUserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _nUserId = 0; }
            }
            else
            { _nUserId = 0; }

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            _databaseconnectionstring = databaseConnectionString;
            _nSignatureID = SignatureID; 
            InitializeComponent();
        }

        #endregion "Constructor & Destructor "

        #region "Form Load event"

        private void frmSignature_Load(object sender, EventArgs e)
        {
            try
            {
                //Create new Instance of Control.
                TextControl = new  gloEditor.gloEditorControl();
                this.panel2.Controls.Add(TextControl);
                TextControl.Dock = DockStyle.Fill;
                TextControl.BringToFront();
                //fill the Signature list with all User Signatures.
                fill_lstSignature();
                lst_Signature.Focus();


                if (appSettings["UserName"] != null)
                {
                    lblUserName.Text = "Signature For " + Convert.ToString(appSettings["UserName"]);
                }

                FillSignature();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        #endregion "Form Load event"

        #region "List Control Events"

        private void lst_Signature_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Shows the signature contents of selected Singnature.
            try
            {

                gloTaskMail.clsSignature oSignature = new clsSignature(_databaseconnectionstring);

                DataTable dtSignature = null;

                if (lst_Signature.SelectedValue != null)
                {
                    //getAllSignatures gives the signature of User by passing UserID
                    dtSignature = oSignature.getAllSignatures(0, Convert.ToInt64(lst_Signature.SelectedValue));
                }

                if (dtSignature != null)
                {
                    //set signature data to TextControl.
                    TextControl.clearText();
                    Byte[] tempSign = (Byte[])dtSignature.Rows[0]["iSignature"];
                    TextControl.setData((Byte[])dtSignature.Rows[0]["iSignature"]);

                    if (Convert.ToBoolean(dtSignature.Rows[0]["bIsDefault"]))
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion "List Control Events"

        #region " Form Button Events "

        private void btn_NewSign_Click(object sender, EventArgs e)
        {
            string response = "";
            //string temp = "";
            try
            {
                //Get the Signature Name for the new signature.
                response = Microsoft.VisualBasic.Interaction.InputBox("Signature Name : ", _messageBoxCaption, "", this.Location.X + 150, this.Location.Y + 300);

                if (response != "")
                {
                    //Check if the Signature Name already exists if yes return 
                    for (int i = 0; i < lst_Signature.Items.Count; i++)
                    {

                        DataRowView row = (DataRowView)lst_Signature.Items[i];
                        if (Convert.ToString(row[5]) == response)
                        {

                            MessageBox.Show("Signature name already exists.  ",_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    //If signature name does not already exists save signature.
                    TextControl.clearText();
                    clsSignature oSignature = new clsSignature(_databaseconnectionstring);
                    Object retObj = new object();
                    //saveSignature saves the signature and returns the signature id
                    //pass nSingnatureID=0 to create new signature.
                    retObj = oSignature.saveSignature(0, UserID, TextControl.getData(), Convert.ToInt64(CategoryType.Mail), Convert.ToBoolean(SignatureDefault.True), response);

                    if (retObj != null)
                    {
                        fill_lstSignature();
                        lst_Signature.SelectedValue = Convert.ToInt64(retObj);


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btn_SaveSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(lst_Signature.Items.Count))
                {
                    MessageBox.Show("Please add new signature first.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Byte[] sign = TextControl.getData();
                lst_Signature.Tag = sign;
                string signName = lst_Signature.Text;
                clsSignature oSignature = new clsSignature(_databaseconnectionstring);
                Object retObj = new object();

                retObj = oSignature.saveSignature(Convert.ToInt64(lst_Signature.SelectedValue), UserID, sign, Convert.ToInt64(CategoryType.Mail), checkBox1.Checked, signName);


                if (retObj != null)
                {
                    fill_lstSignature();
                    lst_Signature.SelectedValue = Convert.ToInt64(retObj);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btn_DeleteSign_Click(object sender, EventArgs e)
        {

            clsSignature oSignature = new clsSignature(_databaseconnectionstring);
            try
            {
                DataRowView row = (DataRowView)lst_Signature.SelectedItem;
                if (row != null)
                {
                    bool result = oSignature.deleteSignature(Convert.ToInt64(row[0]));
                    if (!result)
                    {
                        MessageBox.Show("Error deleting signature.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        fill_lstSignature();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btn_SaveAsSign_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Convert.ToBoolean(lst_Signature.Items.Count))
                {
                    MessageBox.Show("No existing signature available.  ",_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRowView selRow = (DataRowView)lst_Signature.SelectedItem;
                string response = Microsoft.VisualBasic.Interaction.InputBox("Please enter Signature name : ", "Signature - SaveAs", Convert.ToString(selRow[5]), this.Location.X + 50, this.Location.Y + 150);
                if (response != "")
                {
                    for (int i = 0; i < lst_Signature.Items.Count; i++)
                    {

                        DataRowView row = (DataRowView)lst_Signature.Items[i];
                        if (Convert.ToString(row[5]) == response)
                        {

                            MessageBox.Show("Signature name already exists.  ",_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Byte[] sign = TextControl.getData();

                    clsSignature oSignature = new clsSignature(_databaseconnectionstring);
                    Object retObj = new object();
                    retObj = oSignature.saveSignature(0, UserID, sign, Convert.ToInt64(CategoryType.Mail), Convert.ToBoolean(SignatureDefault.False), response);


                    if (retObj != null)
                    {
                        fill_lstSignature();
                        lst_Signature.SelectedValue = Convert.ToInt64(retObj);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       
        #endregion " Form Button Events "

        #region "Private Methods"

        private void fill_lstSignature()
        {

            clsSignature oSignature = new clsSignature(_databaseconnectionstring);
            try
            {

                DataTable dtSignature = null;
                dtSignature = oSignature.getAllSignatures(UserID, 0);

                if (dtSignature != null)
                {
                    lst_Signature.DisplayMember = dtSignature.Columns["sSignatureName"].ColumnName;
                    lst_Signature.ValueMember = dtSignature.Columns["nSignatureID"].ColumnName;
                    lst_Signature.DataSource = dtSignature;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString());
            }

        }

        #endregion "Private Methods"
      
        #region "Tool Strip Events"

        private void tsb_OK_Click(object sender, EventArgs e)
        {
           if(SaveSignature() == true)
            this.Close();
        }
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (SaveSignature() == true)
            {
                _nSignatureID = 0;
                txtSignatureName.Text="";
                checkBox1.Checked=false;
                TextControl.clearText();
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        #endregion "Tool Strip Events"

        private bool SaveSignature()
        {
            bool _result = false; 
            try
            {
                if (txtSignatureName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the signature name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSignatureName.Focus(); 
                    return _result;
                }

                clsSignature oSignature = new clsSignature(_databaseconnectionstring);


                //Check if the Signature Name already exists if yes return 
                if (oSignature.IsSignatureNameExists(_nUserId,_nSignatureID, txtSignatureName.Text.Trim()) == true)
                {
                    MessageBox.Show("Signature name already exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSignatureName.Focus();
                    return _result;
                }

               
                Object retObj = new object();

                //saveSignature saves the signature and returns the signature id
                //pass nSingnatureID=0 to create new signature.
                retObj = oSignature.saveSignature(_nSignatureID, UserID, TextControl.getData(), Convert.ToInt64(CategoryType.Mail), Convert.ToBoolean(SignatureDefault.True), txtSignatureName.Text.Trim());
                if (retObj != null)
                {
                    _result = true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return _result; 
        }

        private void FillSignature()
        {
            //Shows the signature contents of selected Singnature.
            try
            {
                TextControl.clearText();
                
                gloTaskMail.clsSignature oSignature = new clsSignature(_databaseconnectionstring);
                DataTable dtSignature = null;

                if (lst_Signature.SelectedValue != null)
                {
                    //getAllSignatures gives the signature of User by passing UserID
                    dtSignature = oSignature.getAllSignatures(0, _nSignatureID);
                }

                if (dtSignature != null)
                {
                    //set signature data to TextControl.
                    
                    Byte[] tempSign = (Byte[])dtSignature.Rows[0]["iSignature"];
                    TextControl.setData((Byte[])dtSignature.Rows[0]["iSignature"]);

                    txtSignatureName.Text = Convert.ToString(dtSignature.Rows[0]["sSignatureName"]);  
 
                    if (Convert.ToBoolean(dtSignature.Rows[0]["bIsDefault"]))
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

       
    }
}