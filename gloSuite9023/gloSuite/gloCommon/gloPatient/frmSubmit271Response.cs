using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace gloPatient
{
    public partial class frmSubmit271Response : Form
    {

        #region " Variable Declaration"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _MessageBoxCaption = "";
        string _databaseConnectionString = "";
        Int64 _PatientID;
        Int64 _ContactID;
        #endregion

        public frmSubmit271Response()
        {
            InitializeComponent();

        }

        public frmSubmit271Response(string DatabaseConnectionString,Int64 PatientID,Int64 ContactID)
        {

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

            _databaseConnectionString = DatabaseConnectionString;
            _PatientID = PatientID;
            _ContactID = ContactID;
            InitializeComponent();
        }

        private void frmSubmit271Response_Load(object sender, EventArgs e)
        {
            txtResponseFilePath.Text = Application.StartupPath.ToString();
        }

        private void btnBrowseResponseFile_Click(object sender, EventArgs e)
        {
            char[] charsToTrim = { '\\' };

            if (txtResponseFilePath.Text.EndsWith("\\"))
            {
                txtResponseFilePath.Text = txtResponseFilePath.Text.TrimEnd(charsToTrim);
            }

            dlg271ResponseBrowser.InitialDirectory = txtResponseFilePath.Text + "\\"; 
            dlg271ResponseBrowser.Filter = "txt files (*.X12)|*.x12|All files (*.*)|*.*";
            dlg271ResponseBrowser.FileName =  "";
            dlg271ResponseBrowser.CheckFileExists = true;
            dlg271ResponseBrowser.Multiselect = false;
            dlg271ResponseBrowser.ShowHelp = false;
            dlg271ResponseBrowser.ShowReadOnly = false;
            DialogResult dRes = dlg271ResponseBrowser.ShowDialog(this);

            if (dRes == DialogResult.OK)
            {
                txtResponseFilePath.Text = dlg271ResponseBrowser.FileName.ToString();
            }
            //dlg271ResponseBrowser.Dispose();
            //dlg271ResponseBrowser = null;
         
        }

        private void btnRemoveResponseFile_Click(object sender, EventArgs e)
        {
            txtResponseFilePath.Text = "";
        }

        private void tsbtnCheckEligibility_Click(object sender, EventArgs e)
        {
            string oVersionData = "";
            string sANSIVersion = "";
            string sFileType = "";

            if (System.IO.File.Exists((txtResponseFilePath.Text.ToString())))
            {
                if (txtResponseFilePath.Text.ToString() != "" & txtResponseFilePath.Text.ToString().ToUpper().EndsWith(".X12"))
                {
                    StreamReader oVersionStreamReader = new StreamReader(txtResponseFilePath.Text.ToString());

                    oVersionData = oVersionStreamReader.ReadToEnd().Trim();
                    sFileType=CheckFileType(oVersionData);
                    oVersionStreamReader.Close();
                    oVersionStreamReader.Dispose();

                    sANSIVersion = CheckFileVersion(oVersionData);
                    if (sFileType == "271")
                    {
                        if (sANSIVersion == "00401")
                        {
                            frmEligibilityResponse ofrm = new frmEligibilityResponse(_databaseConnectionString, _PatientID, _ContactID, true, txtResponseFilePath.Text.ToString());
                            ofrm.ShowDialog(this);
                            ofrm.Dispose();
                            ofrm = null;
                            //  ofrm.EDIReturnResult;
                        }
                        else if (sANSIVersion == "00501")
                        {
                            frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(_databaseConnectionString, _PatientID, _ContactID, true, txtResponseFilePath.Text.ToString());
                            ofrm.ShowDialog(this);
                            ofrm.Dispose();
                            ofrm = null;
                            //  ofrm.EDIReturnResult;
                        }
                        else
                        {
                            MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select 271 response file.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    }
                }
                else
                {
                    MessageBox.Show("Select file with .X12 extension.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selected file doesn't exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string CheckFileVersion(string strbData)
        {
            string _result = String.Empty;
            try
            {
                char[] arr = { '*' };
                string[] Array = Convert.ToString(strbData).Replace("\n", "").Split(arr, StringSplitOptions.RemoveEmptyEntries);
                if (Array.Length > 0)
                {

                    if (Array[0].Contains("ISA"))
                    {
                        _result = Array[12].ToString();
                    }

                }
                //sEdiFile.Split("*",StringSplitOptions.RemoveEmptyEntries)
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        public string CheckFileType(string strData)
        {
            string _result = String.Empty;
            try
            {
                char[] arr = { '*' };
                string[] Array = Convert.ToString(strData).Replace("\n", "").Replace("\r","").Split(arr, StringSplitOptions.RemoveEmptyEntries);
                if (Array.Length > 0)
                {
                    for (int _Count = 0; _Count < Array.Length; _Count++)
                    {
                        if (Convert.ToString(Array[_Count]).Contains("~ST"))
                        {
                            _result = Convert.ToString(Array[_Count + 1]);
                            break;
                        }
                    }

                }
                //sEdiFile.Split("*",StringSplitOptions.RemoveEmptyEntries)
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        private void tsbtnCheckBatchEligibility_Click(object sender, EventArgs e)
        {
            string oVersionData = "";
            string sANSIVersion = "";
            string sFileType = "";
            gloPatientEiligibility objEligibility = new gloPatientEiligibility(_databaseConnectionString);
            try 
            {
                if (System.IO.File.Exists((txtResponseFilePath.Text.ToString())))
                {
                    if (txtResponseFilePath.Text.ToString() != "" & txtResponseFilePath.Text.ToString().ToUpper().EndsWith(".X12"))
                    {
                        StreamReader oVersionStreamReader = new StreamReader(txtResponseFilePath.Text.ToString());

                        oVersionData = oVersionStreamReader.ReadToEnd().Trim();
                        sFileType=CheckFileType(oVersionData);
                        

                        sANSIVersion = CheckFileVersion(oVersionData);
                        if (sFileType == "271")
                        {
                            if (sANSIVersion == "00401")
                            {
                                MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (sANSIVersion == "00501")
                            {
                                bool _result = objEligibility.DoEligibilty(true, Convert.ToString(oVersionData));
                                if (_result == true)
                                {
                                    MessageBox.Show("Eligibility response stored successfully. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select 271 response file.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                        }
                        oVersionStreamReader.Close();
                        oVersionStreamReader.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select file with .X12 extension.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            else
            {
                MessageBox.Show("Selected file doesn't exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
            finally
            {
            }
            
        }

    }
}
