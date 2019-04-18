using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloPatient.Classes;

namespace gloPatient
{
    public partial class frmMergePatients : Form
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        //private string _messageboxcaption = "gloPM";
        private string _messageboxcaption = String.Empty;

        private bool _isMergePatientSuccess = false;

        Int64 _clinicID = 0;
        string _controlType = string.Empty;

        //Added By MaheshB
        private gloListControl.gloListControl oListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        PatientAccounts oPatientAccounts = null;
        public bool _IsPatientAccountFeature = false;
       

        public frmMergePatients(String Databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = Databaseconnectionstring;

            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 1; }
            }
            else
            { _clinicID = 1; }
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion
        }

        private void frmMergePatients_Load(object sender, EventArgs e)
        {
            // FillDuplicatePatients();
        }

        //Not in used Commented From Form_Load
        private void FillDuplicatePatients()
        {
            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
            try
            {
                //DataTable dtPatients = ogloPatient.GetPatientsToMerge();
               
                cmbPatientToMerge.DataSource = null;
                cmbPatientToMerge.Items.Clear();
                
                cmbPatientToMergeIn.DataSource = null;
                cmbPatientToMergeIn.Items.Clear();
                DataTable dtPatients;
                if (rbAll.Checked)
                    dtPatients = ogloPatient.GetPatientsToMerge();
                else
                    dtPatients = ogloPatient.GetDuplicatePatients();


                if (dtPatients != null)
                {
                    cmbPatientToMerge.DataSource = dtPatients.Copy();
                    cmbPatientToMerge.DisplayMember = "PatientName";
                    cmbPatientToMerge.ValueMember = "PatientID";

                    cmbPatientToMergeIn.DataSource = dtPatients.Copy();
                    cmbPatientToMergeIn.DisplayMember = "PatientName";
                    cmbPatientToMergeIn.ValueMember = "PatientID";
                    dtPatients.Dispose();
                    dtPatients = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloPatient.Dispose();
                ogloPatient = null;
            }
        }

        #region "Tool Strip button events"

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnMerge_Click(object sender, EventArgs e)
        {
            MergePatient();

            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            if (_isMergePatientSuccess == true)
            { 
                ValidateAccounts(); 
                
            }

            ClearSource();
            ClearDestination();
            txtpatientdest.Tag = null;
            txtpatientsource.Tag = null;
            txtpatientdest.Text = "";
            txtpatientsource.Text = "";
        } 
        #endregion


        #region "Radio button events"

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        
        {
            if (rbAll.Checked == true)
            {
                rbAll.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbAll.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }

            //MaheshB
            //FillDuplicatePatients();
            //cmbPatientToMerge_SelectionChangeCommitted(null, null);
            //cmbPatientToMergeIn_SelectionChangeCommitted(null, null);
        }

        private void rbConflict_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConflict.Checked == true)
            {
                rbConflict.Font = gloGlobal.clsgloFont.gFont_BOLD ;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbConflict.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            
            
            //MaheshB
            //FillDuplicatePatients();
            //cmbPatientToMerge_SelectionChangeCommitted(null, null);
            //cmbPatientToMergeIn_SelectionChangeCommitted(null, null);
        }

        #endregion

        //private void cmbPatientToMerge_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    cmbPatientToMergeIn.DataSource = null;

        //    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
        //    try
        //    {
        //        if (cmbPatientToMerge.SelectedIndex != -1)
        //        {
        //            Patient oPatient = ogloPatient.GetPatientDemo(Convert.ToInt64(cmbPatientToMerge.SelectedValue));
        //            lblDOB_Source.Text = oPatient.DemographicsDetail.PatientDOB.ToShortDateString();
        //            lblSSN_Source.Text = oPatient.DemographicsDetail.PatientSSN;
        //            lblProvider_Source.Text = oPatient.DemographicsDetail.ProvideName;

        //            DataTable dtPatients = ogloPatient.GetPatientsToMerge();

        //            if (dtPatients != null)
        //            {
        //                cmbPatientToMergeIn.DataSource = dtPatients.Copy();
        //                cmbPatientToMergeIn.DisplayMember = "PatientName";
        //                cmbPatientToMergeIn.ValueMember = "PatientID";

        //                if (dtPatients.Rows.Count > 0)
        //                {
        //                    cmbPatientToMergeIn.SelectedIndex = 0;
        //                    cmbPatientToMergeIn_SelectionChangeCommitted(null, null);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        //Mukesh Patel  20090807

        private void ClearSource()
        {
            lblDOB_Source.Text = "";
            lblSSN_Source.Text = "";
            lblProvider_Source.Text = "";
        }

        private void ClearDestination()
        {
            lblDOB_Destination.Text = "";
            lblSSN_Destination.Text = "";
            lblProvider_Destination.Text = "";
        }

        private void cmbPatientToMerge_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ClearSource();
            
            try
            {
                if (cmbPatientToMerge.SelectedIndex != -1)
                {
                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                    Patient oPatient = ogloPatient.GetPatientDemo(Convert.ToInt64(cmbPatientToMerge.SelectedValue));
                    if (oPatient != null)
                    {
                        lblDOB_Source.Text = oPatient.DemographicsDetail.PatientDOB.ToShortDateString();
                        lblSSN_Source.Text = oPatient.DemographicsDetail.PatientSSN;
                        lblProvider_Source.Text = oPatient.DemographicsDetail.ProvideName;

                        if (cmbPatientToMergeIn.SelectedIndex != -1)
                        {
                            cmbPatientToMergeIn.SelectedIndex = 0;
                            cmbPatientToMergeIn_SelectionChangeCommitted(null, null);
                        }
                        oPatient.Dispose();
                        oPatient = null;
                    }

                    ogloPatient.Dispose();
                    ogloPatient = null;
                }
             }
             catch (Exception) // ex)
             {
                 //ex.ToString();
                 //ex = null;
             }
        }

        private void cmbPatientToMergeIn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ClearDestination();
            
            try
            {
                if (cmbPatientToMergeIn.SelectedIndex != -1)
                {
                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                    Patient oPatient = ogloPatient.GetPatientDemo(Convert.ToInt64(cmbPatientToMergeIn.SelectedValue));
                    if (oPatient != null)
                    {
                        lblDOB_Destination.Text = oPatient.DemographicsDetail.PatientDOB.ToShortDateString();
                        lblSSN_Destination.Text = oPatient.DemographicsDetail.PatientSSN;
                        lblProvider_Destination.Text = oPatient.DemographicsDetail.ProvideName;
                        oPatient.Dispose();
                        oPatient = null;
                    }
                    ogloPatient.Dispose();
                    ogloPatient = null;
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void MergePatient_Old()
        {
            try
            {
                if ((cmbPatientToMerge.SelectedValue == null) == false & (cmbPatientToMergeIn.SelectedValue == null) == false)
                {
                    if (Convert.ToInt64(cmbPatientToMerge.SelectedValue) == Convert.ToInt64(cmbPatientToMergeIn.SelectedValue))
                    {
                        MessageBox.Show("'Surviving patient record' and 'Patient record to remove' both should not be the same.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (MessageBox.Show("Are you sure you want to merge the patient's records?  ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                        {
                            if (ogloPatient.Merge_Patients(Convert.ToInt64(cmbPatientToMerge.SelectedValue), cmbPatientToMerge.Text,Convert.ToInt64(cmbPatientToMergeIn.SelectedValue), cmbPatientToMergeIn.Text) == true)
                            {
                                //Delete The Merged Patient need to be implemented
                                MessageBox.Show("Merging of patients has been done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FillDuplicatePatients();
                            }
                            else
                            {
                                MessageBox.Show("Merging of patients not done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        ogloPatient.Dispose();
                    }
                }
            }
            //Me.Close()
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MergePatient()
        {
            try
            {
                _isMergePatientSuccess = false;

                if ((txtpatientsource.Tag == null) == false & (txtpatientsource.Tag == null) == false)
                {
                    if (Convert.ToInt64(txtpatientsource.Tag) == Convert.ToInt64(txtpatientdest.Tag))
                    {
                        MessageBox.Show("'Patient 1' and 'Patient 2' both should not be the same.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //Added by Mayuri:20100513-to fix bugID:#1777
                    if ((txtpatientdest.Tag == null) == true)
                    {
                        MessageBox.Show("Select 'Patient 2'.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (MessageBox.Show("Are you sure you want to merge the patient's records?  ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                        {
                            _isMergePatientSuccess = ogloPatient.Merge_Patients(Convert.ToInt64(txtpatientsource.Tag), txtpatientsource.Text, Convert.ToInt64(txtpatientdest.Tag), txtpatientdest.Text);
                            if (_isMergePatientSuccess == true)
                            {                                   
                                ogloPatient.DeleteData(Convert.ToInt64(txtpatientsource.Tag),Convert.ToInt64 (txtpatientdest.Tag));
                                //Delete The Merged Patient need to be implemented

                                MessageBox.Show("Merging of patients has been done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //FillDuplicatePatients();
                               
                            }
                            else
                            {
                                MessageBox.Show("Merging of patients not done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        ogloPatient.Dispose();
                    }
                }
            }
            //Me.Close()
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemovePatient_Click(object sender, EventArgs e)
        {
            try
            {
                oListControl = new gloListControl.gloListControl();
                if (oListControl != null)
                {
                    for (int i = panel2.Controls.Count - 1; i >= 0; i--)
                    {
                        if (panel2.Controls[i].Name == oListControl.Name)
                        {
                            panel2.Controls.Remove(panel2.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                if (rbAll.Checked == true)
                {
                    _CurrentControlType = gloListControl.gloListControlType.Patient;
                }
                else
                {
                    _CurrentControlType = gloListControl.gloListControlType.ConflictingPatient;
                }
                _controlType = "Source";
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _CurrentControlType, false, this.Width);
                oListControl.ClinicID = _clinicID;
                oListControl.ControlHeader = " Patient";
                //_CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);


                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;   
                panel2.Show();

                pnlMain.Hide();
                panel1.Hide();
                panel3.Hide(); //add by Ojeswini
                pnl_tlsp.Hide();  
                //if (txtApp_Patient.Text.Trim() != "")
                //{
                //    if (txtApp_Patient.Tag != null)
                //    {
                //        oListControl.SelectedItems.Add(Convert.ToInt64(txtApp_Patient.Tag.ToString()), txtApp_Patient.Text);
                //    }
                //}
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);    
            }
            finally
            { 
            }
        }

        private void btnSurvivingPatient_Click(object sender, EventArgs e)
        {
            try
            {
                oListControl = new gloListControl.gloListControl();
                if (oListControl != null)
                {
                    for (int i = panel2.Controls.Count - 1; i >= 0; i--)
                    {
                        if (panel2.Controls[i].Name == oListControl.Name)
                        {
                            panel2.Controls.Remove(panel2.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                if (rbAll.Checked == true)
                {
                    _CurrentControlType = gloListControl.gloListControlType.Patient;
                }
                else
                {
                    _CurrentControlType = gloListControl.gloListControlType.ConflictingPatient;
                }
                _controlType = "Destination";
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _CurrentControlType, false, this.Width);
                oListControl.ClinicID = _clinicID;
                oListControl.ControlHeader = " Patient";
                //_CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;
                panel2.Show();

                pnlMain.Hide();
                panel1.Hide();
                panel3.Hide(); 
                pnl_tlsp.Hide();  
                //if (txtApp_Patient.Text.Trim() != "")
                //{
                //    if (txtApp_Patient.Tag != null)
                //    {
                //        oListControl.SelectedItems.Add(Convert.ToInt64(txtApp_Patient.Tag.ToString()), txtApp_Patient.Text);
                //    }
                //}
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ) //ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
            finally
            {
                panel2.Hide();

                pnlMain.Show();
                panel1.Show();
                panel3.Show();
                pnl_tlsp.Show();  
            }
        }
        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
           // int _Counter = 0;

            try
            {

            switch (_controlType)
            {
               
                case "Source":
                    {
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            ClearSource();
                           

                            txtpatientsource.Tag = oListControl.SelectedItems[0].ID;
                            txtpatientsource.Text =oListControl.SelectedItems[0].Code +"-"+ oListControl.SelectedItems[0].Description ;

                            #region Patient Demographics

                            //ClearSource();

                            try
                            {
                                if (txtpatientsource.Tag != null)
                                {

                                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                                    Patient oPatient = ogloPatient.GetPatientDemo(Convert.ToInt64(txtpatientsource.Tag));
                                    //gloPM5076 Date Format issue.
                                    if (oPatient != null)
                                    {
                                        lblDOB_Source.Text = oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                                        lblSSN_Source.Text = oPatient.DemographicsDetail.PatientSSN;
                                        lblProvider_Source.Text = oPatient.DemographicsDetail.ProvideName;
                                        oPatient.Dispose();
                                        oPatient = null;
                                    }
                                    ogloPatient.Dispose();
                                    ogloPatient = null;
                                }
                            }
                            catch (Exception) // ex)
                            {
                                //ex.ToString();
                                //ex = null;
                            }

                            #endregion

                          
                        }

                    }
                    break;

                case "Destination":
                    {
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            
                            ClearDestination();

                            txtpatientdest.Tag = oListControl.SelectedItems[0].ID;
                            txtpatientdest.Text = oListControl.SelectedItems[0].Code + "-" + oListControl.SelectedItems[0].Description;

                         

                            #region Patient Demographics



                            try
                            {
                                if (txtpatientdest.Tag != null)
                                {
                                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                                    Patient oPatient = ogloPatient.GetPatientDemo(Convert.ToInt64(txtpatientdest.Tag));
                                    //gloPM5076 Date Format issue.
                                    if (oPatient != null)
                                    {
                                        lblDOB_Destination.Text = oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                                        lblSSN_Destination.Text = oPatient.DemographicsDetail.PatientSSN;
                                        lblProvider_Destination.Text = oPatient.DemographicsDetail.ProvideName;
                                        oPatient.Dispose();
                                        oPatient = null;
                                    }
                                    ogloPatient.Dispose();
                                    ogloPatient = null;
                                }
                            }
                            catch (Exception)// ex)
                            {
                                //ex.ToString();
                                //ex = null;
                            }

                            #endregion


                        }

                    }
                    break;
              
              
                default:
                    {
                    }
                    break;
            }

           
            }
            catch(Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
            finally
            {
                panel2.Hide();

                pnlMain.Show();
                panel1.Show();
                panel3.Show();
                pnl_tlsp.Show();  
            }
        }

        private void btnclear1_Click(object sender, EventArgs e)
        {
            txtpatientsource.Text = "";
            txtpatientsource.Tag=null;

            lblDOB_Source.Text = "";
            lblSSN_Source.Text = "";
            lblProvider_Source.Text = "";

        }

        private void btnclear2_Click(object sender, EventArgs e)
        {
            txtpatientdest.Text = "";
            txtpatientdest.Tag=null;

            lblDOB_Destination.Text = "";
            lblSSN_Destination.Text = "";
            lblProvider_Destination.Text = "";

        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }            
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        #region "Patient Account Feature related Methods"

        private void ValidateAccounts()
        {
            try
            {
                oPatientAccounts = new PatientAccounts();
                gloAccount objgloAccount = new gloAccount(_databaseconnectionstring);
                string strMessage = "";
                Int64 fromAccountID = 0;
                Int64 toAccountID = 0;
                //Get Destination Patient Accounts.
                GetDestinationPatientAccounts();
                string DestPatientCode = txtpatientdest.Text.ToString().Split('-')[0];
                if (oPatientAccounts != null && oPatientAccounts.Count == 2)
                {
                    strMessage = "Patient  " + txtpatientdest.Text + "  now has two accounts:";
                    int ownAccCount = 0;
                    for (int i = 0; i < oPatientAccounts.Count; i++)
                    {
                        if (oPatientAccounts[i].OwnAccount == true)
                        {
                            bool existsMultiplePatientsPerAccount = false;
                            existsMultiplePatientsPerAccount = CheckMultiplePatientsForAccount(oPatientAccounts[i].PAccountID, oPatientAccounts[i].PatientID);
                            if (existsMultiplePatientsPerAccount == false)
                            {
                                if (DestPatientCode == oPatientAccounts[i].PatientCode)
                                {
                                    toAccountID = oPatientAccounts[i].PAccountID;
                                }
                                else
                                {
                                    fromAccountID = oPatientAccounts[i].PAccountID;
                                }
                                DataTable dt = objgloAccount.GetAccountDetailsById(oPatientAccounts[i].PAccountID);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    strMessage += "\n Acct.# : " + dt.Rows[0]["sAccountNo"] + "  - " + " Guarantor : " + dt.Rows[0]["sFirstName"] + ' ' + dt.Rows[0]["sLastName"] + " ";
                                    ownAccCount = ownAccCount + 1;
                                }
                            }
                        }

                    }
                    if (ownAccCount == 2)
                    {
                        _IsPatientAccountFeature = objgloAccount.GetPatientAccountFeatureSetting();
                        if (_IsPatientAccountFeature == true)
                        {
                            strMessage = strMessage + ".\nWould you like to merge these accounts into a single account?";
                            DialogResult res = MessageBox.Show(strMessage, _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {
                                //FromAccountID merged with ToAccounID, FromAccountID is deleted.
                                if (fromAccountID > 0 && toAccountID > 0)
                                {
                                    MergeAccount(fromAccountID, toAccountID);
                                    MessageBox.Show("Merging of accounts has been done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Merging of accounts failed.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                //..If the patients accounts are not merged then one of the account will have "PatientCode" of existing(merged) patient
                                //..so need to update the patient code in PA_Accounts_Patient table
                                UpdatePatientCodeOnAccounts();
                            }
                        }
                        else
                        {
                            // strMessage = strMessage + ".\nThese accounts are merged into single account.";
                            // MessageBox.Show(strMessage, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //FromAccountID merged with ToAccounID, FromAccountID is deleted.If PAF is off always patient have only one account
                            if (fromAccountID > 0 && toAccountID > 0)
                            {
                                MergeAccount(fromAccountID, toAccountID);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Merging of accounts failed in Merge Patient activity for Pat. ID : " + Convert.ToString(txtpatientdest.Tag) + "",false);
                            }
                        }
                    }
                }
                else if (oPatientAccounts != null && oPatientAccounts.Count > 2)
                {
                    strMessage = "Patient " + txtpatientdest.Text + "   now has more than two accounts.\nAccounts for Patient  " + txtpatientdest.Text + "  were not merged.\nPlease review Patient  " + txtpatientdest.Text + " ’s accounts.";
                    MessageBox.Show(strMessage, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //..If the patients accounts are not merged then one of the account will have "PatientCode" of existing(merged) patient
                    //..so need to update the patient code in PA_Accounts_Patient table
                    UpdatePatientCodeOnAccounts();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }

        private void GetDestinationPatientAccounts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPatientAccounts;
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "Select nAccountPatientId, nPAccountID, nPatientID," +
                                  " sAccountNo  as sAccountNo, " +
                                  " sPatientCode, dtAccountClosedDate," +
                                  " nClinicID, nSiteID, sMachineName, nUserID, dtRecordDate, bIsActive,bIsOwnAccount" +
                                  " From PA_Accounts_Patients" +
                                  " Where nPatientID = " + Convert.ToInt64(txtpatientdest.Tag) + " AND ISNULL(nClinicID,1) = " + _clinicID;

                oDB.Retrive_Query(_strSqlQuery, out dtPatientAccounts);

                if (dtPatientAccounts != null && dtPatientAccounts.Rows.Count > 0)
                {

                    for (int i = 0; i < dtPatientAccounts.Rows.Count; i++)
                    {
                        PatientAccount oPatientAccount = new PatientAccount();

                        oPatientAccount.AccountPatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nAccountPatientID"].ToString());
                        oPatientAccount.PAccountID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                        oPatientAccount.AccountNo = dtPatientAccounts.Rows[i]["sAccountNo"].ToString();
                        oPatientAccount.PatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPatientID"].ToString());
                        oPatientAccount.PatientCode = dtPatientAccounts.Rows[i]["sPatientCode"].ToString();
                        oPatientAccount.ClinicID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nClinicID"].ToString());
                        oPatientAccount.SiteID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nSiteID"].ToString());
                        oPatientAccount.MachineName = dtPatientAccounts.Rows[i]["sMachineName"].ToString();
                        oPatientAccount.UserID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nUserID"].ToString());
                        oPatientAccount.RecordDate = Convert.ToDateTime(dtPatientAccounts.Rows[i]["dtRecordDate"].ToString());
                        oPatientAccount.Active = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsActive"].ToString());
                        oPatientAccount.OwnAccount = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString());
                        oPatientAccounts.Add(oPatientAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        private void UpdatePatientCodeOnAccounts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (txtpatientdest.Tag != null && Convert.ToString(txtpatientdest.Tag).Trim() != "" && Convert.ToInt64(txtpatientdest.Tag) > 0)
                {

                    string _strSqlQuery = " UPDATE PA_Accounts_Patients " +
                                          " SET sPatientCode = (SELECT TOP 1 sPatientCode FROM Patient WHERE nPatientID = " + Convert.ToInt64(txtpatientdest.Tag) + ") " +
                                          " WHERE nPatientID = " + Convert.ToInt64(txtpatientdest.Tag) + "";

                    oDB.Connect(false);
                    oDB.Execute_Query(_strSqlQuery);
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        
        }

        public bool CheckMultiplePatientsForAccount(Int64 accountId, Int64 patientId)
        {
            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                string sqlQuery = "Select Count(nAccountPatientID) From PA_Accounts_Patients Where nPAccountID = " + accountId + "and nPatientID <> " + patientId;
                result = oDB.ExecuteScalar_Query(sqlQuery);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
            if (Convert.ToInt64(result) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void MergeAccount(Int64 FromAccountID, Int64 ToAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            string machineName = System.Environment.MachineName;
            Int64 userID = Convert.ToInt64(appSettings["UserID"].ToString());

            try
            {
                odbParams.Add("@From_nPAccountID", FromAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@To_nPAccountID", ToAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@MachineName", machineName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@nUserID", userID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nClinicID", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("PA_MergeAccount", odbParams);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }

        #endregion

    }
}