using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmShow997Status : Form
    {
        #region Private Variables

        string _databaseconnectionstring = String.Empty;
        string _batchname = String.Empty;
        Int64 _ClinicID = 0;
        string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region Construstor

        public frmShow997Status(string batchname, string databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = databaseconnectionstring;
            _batchname = batchname;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        #endregion

        #region Form Load

        private void frmShow997Status_Load(object sender, EventArgs e)
        {
            show997Status();
        }

        #endregion

        #region Private Methods

        private void show997Status()
        {
            gloDatabaseLayer.DBLayer ODB=null;
            DataTable dtStatus=null;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string _strquery = "SELECT     BL_ClaimAcknowledgment.sAcknowledgmentCode, BL_ClaimAcknowledgment.sAcknowledgmentDate,"
                                 + " BL_ClaimAcknowledgment.nGroupControlNumber,BL_Transaction_Batch.sBatchName as sBatchName, BL_Transaction_Batch.nBatchID "
                                 + " FROM BL_Transaction_ClaimMgr_MST INNER JOIN BL_Transaction_Batch ON "
                                 + " BL_Transaction_ClaimMgr_MST.nBatchId = BL_Transaction_Batch.nBatchID INNER JOIN "
                                 + " BL_ClaimAcknowledgment ON BL_Transaction_ClaimMgr_MST.sFunctionalGroupNumber =Convert(Varchar(100),BL_ClaimAcknowledgment.nGroupControlNumber) where BL_Transaction_Batch.sBatchName='" + _batchname + "' ";
                dtStatus = new DataTable();
                ODB.Retrive_Query(_strquery, out dtStatus);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    lblBatchName.Text = Convert.ToString(dtStatus.Rows[0]["sBatchName"]);
                    string _status=String.Empty;
                    _status=Convert.ToString(dtStatus.Rows[0]["sAcknowledgmentCode"]).Trim();
                    switch (_status)
                    {
                        case "A":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Green;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Green;
                            lblBatchStatus.Text = "This Batch successfully recieved by the Payer.";
                            lblBatchStatusCode.Text = "A";
                            break;
                        case "R":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.Text = "R";
                            lblBatchStatus.Text = "This Batch Rejected By the Payer.";
                            break;
                        case "E":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Orange;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Orange;
                            lblBatchStatusCode.Text = "E";
                            lblBatchStatus.Text = "Accepted But Errors Were Noted.";
                            break;
                        case "M":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.Text = "M";
                            lblBatchStatus.Text = "Rejected, Message Authentication Code (MAC) Failed";
                            break;
                        case "W":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.Text = "W";
                            lblBatchStatus.Text = "Rejected, Assurance Failed Validity Tests";
                            break;
                        case "X":
                            lblBatchStatus.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.ForeColor = System.Drawing.Color.Red;
                            lblBatchStatusCode.Text = "X";
                            lblBatchStatus.Text = "Rejected, Content After Decryption Could Not Be Analyzed";
                            break;
                        default:
                            break;
                    }
                    lblBatchStatusDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtStatus.Rows[0]["sAcknowledgmentDate"])).ToShortDateString());
                }
                else
                {
                    MessageBox.Show("No Acknowledgment available for this Batch.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB = null;
                }
                if (dtStatus != null)
                {
                    dtStatus = null;
                }
            }
        }
        #endregion

        private void lblBatchName_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblBatchStatusCode_Click(object sender, EventArgs e)
        {

        }

        
    }
}