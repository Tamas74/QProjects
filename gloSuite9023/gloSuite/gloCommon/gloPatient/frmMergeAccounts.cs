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
    public partial class frmMergeAccounts : Form
    {
        #region " Variables "

        //added by mahesh s on 02-may-2011
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        string _databaseconnectionstring = "";
        string _messageboxcaption = String.Empty;
        Int64 _clinicID = 0;
        Int64 _nPatientID;
        Int64 _nToBeMergePatientID;
        Int64 _nRemoveAccountID = 0;
        Int64 _nToBeMergeAccountID = 0;

        #endregion

        #region " Properties "

        public Int64 nPatientID
        {
            get{return _nPatientID;}
            set{_nPatientID = value;}
        }

        public Int64 nToBeMergePatientID
        {
            get { return _nToBeMergePatientID; }
            set { _nToBeMergePatientID = value; }
        }

        #endregion

        #region " Constructors "

        //added mahesh s on 05-may-2011 for set connectionstring, PatientID.
        public frmMergeAccounts(String Databaseconnectionstring, Int64 PatientID)
        {
            InitializeComponent();

            _databaseconnectionstring = Databaseconnectionstring;
            _nPatientID = PatientID;
            _nToBeMergePatientID = PatientID;
            //changed my mahesh s on 26/may/2011.
            _messageboxcaption = appSettings["MessageBOXCaption"] == null ? "gloPM" : appSettings["MessageBOXCaption"] == "" ? "gloPM" : Convert.ToString(appSettings["MessageBOXCaption"]) ;
            _clinicID = appSettings["ClinicID"] == null ? 1 : appSettings["ClinicID"] == "" ? 1 : Convert.ToInt64(appSettings["ClinicID"]);
        }

        #endregion

        #region " Form Events "

        private void frmMergeAccounts_Load(object sender, EventArgs e)
        {
            //Set Context Patient.
            SetContextPatient_Load();
            label21.Text = "Patient Account 1 will be removed and its contents will be placed into Patient Account 2.";
            label18.Text = "Patient Account 2 will keep its guarantor information.  All statement history will be marked voided so that Patient Account 2’s “Statement Count” will reset to 0.";
            lblGuarantorDetails.Text = "";
            lblToBeGuarantorDetails.Text = "";
            lblBusinessCenter1.Text = "";
            lblBusinessCenter2.Text = "";

            Boolean _IsRequireBusinessCenterOnPAccounts = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
            if (_IsRequireBusinessCenterOnPAccounts)
            {
                pnlBusinessCenter.Visible = true;
            }
            else
            {
                pnlBusinessCenter.Visible = false;
            }
        }

        #endregion

        #region " Button Events "

        //added by mahesh s on 02-may-2011 for select patient
        private void btnPatient_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick1);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_RemoveAccountSelectClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ToBeMergeAccountSelectClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _CurrentControlType, false, this.Width);
                oListControl.ClinicID = _clinicID;
                oListControl.ControlHeader = " Patient";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;
                panel2.Show();
                pnlMain.Hide();
                pnlToBeMergeMain.Hide();
                panel3.Hide();
                pnlGIContactDetails.Hide();
                pnlMergeAccount.Hide();
                pnl_tlsp.Hide();
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
           
        }


        private void btnToBeMergePatient_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick1);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_RemoveAccountSelectClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ToBeMergeAccountSelectClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _CurrentControlType, false, this.Width);
                oListControl.ClinicID = _clinicID;
                oListControl.ControlHeader = " Patient";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick1);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;
                panel2.Show();
                pnlMain.Hide();
                pnlToBeMergeMain.Hide();
                panel3.Hide();
                pnlGIContactDetails.Hide();
                pnlMergeAccount.Hide();
                pnl_tlsp.Hide();
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }




        //clear all controls.
        private void btnclear1_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnclear2_Click(object sender, EventArgs e)
        {
            ClearToBeMergeControls();
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //select patient account 1 for remove.
        private void btnRemoveAccount_Click(object sender, EventArgs e)
        {
            if (_nPatientID == 0)
            {
                MessageBox.Show("Select Patient.", _messageboxcaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (oListControl != null)
                {
                    for (Int32 iControlCnt = this.Controls.Count - 1; iControlCnt >= 0; iControlCnt--)
                    {
                        if (this.Controls[iControlCnt].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[iControlCnt]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick1);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_RemoveAccountSelectClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ToBeMergeAccountSelectClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientAccounts, false, this.Width, _nPatientID, 0);
                oListControl.ControlHeader = "Patient Accounts";
                oListControl.PatientID = _nPatientID;
                oListControl.IsOwnAccounts = true;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_RemoveAccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;
                panel2.Show();
                pnlMain.Hide();
                pnlToBeMergeMain.Hide();
                panel3.Hide();
                pnlGIContactDetails.Hide();
                pnlMergeAccount.Hide();
                pnl_tlsp.Hide();
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void btnToBeMergeRemoveAccount_Click(object sender, EventArgs e)
        {
            if (_nToBeMergePatientID == 0)
            {
                MessageBox.Show("Select Patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (oListControl != null)
                {
                    for (Int32 iControlCnt = this.Controls.Count - 1; iControlCnt >= 0; iControlCnt--)
                    {
                        if (this.Controls[iControlCnt].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[iControlCnt]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick1);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_RemoveAccountSelectClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ToBeMergeAccountSelectClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientAccounts, false, this.Width, _nPatientID, 0);
                oListControl.ControlHeader = "Patient Accounts";
                oListControl.PatientID = _nToBeMergePatientID;
                oListControl.IsOwnAccounts = true;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ToBeMergeAccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                panel2.Controls.Add(oListControl);
                panel2.Dock = DockStyle.Fill;
                panel2.Show();
                pnlMain.Hide();
                pnlToBeMergeMain.Hide();
                panel3.Hide();
                pnlGIContactDetails.Hide();
                pnlMergeAccount.Hide();
                pnl_tlsp.Hide();
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        //select patient account 2. 
        private void btnToBeMergeAccount_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region " Tool Strip button events "

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnMerge_Click(object sender, EventArgs e)
        {
            if (ValidateData() == true)
            {
                string strMessage = "Merging account  " + txtPatAccount1.Text.ToString() + "  into  " + txtPatAccount2.Text.ToString() + ". Continue?";
                DialogResult mergeRes = MessageBox.Show(strMessage, _messageboxcaption , MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                
                if (mergeRes == DialogResult.OK)
                {
                    MergeAccount(_nRemoveAccountID, _nToBeMergeAccountID);
                    MessageBox.Show("Merging of accounts has been done successfully.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearControls();
                    ClearToBeMergeControls();
                }
                else
                {
                    MessageBox.Show("Merging of accounts not done successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                return;
            }
        } 

        #endregion

        #region " List Control Events "

        //aded by mahesh s on 05-may-2011 for list control close.
        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();                
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();
            }
        }

        //list control patient select.
        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                {
                    ClearControls();
                    _nPatientID = oListControl.SelectedItems[0].ID;
                    _nToBeMergePatientID = _nPatientID;

                    txtPatientName.Tag = oListControl.SelectedItems[0].Description;
                    txtPatientName.Text = oListControl.SelectedItems[0].Code + "-" + oListControl.SelectedItems[0].Description;


                    txtToBeMergePatientName.Tag = txtPatientName.Tag;
                    txtToBeMergePatientName.Text = txtPatientName.Text;



                    if (_nPatientID != 0)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                        Patient oPatient = ogloPatient.GetPatientDemo(_nPatientID);
                        if (oPatient != null)
                        {
                            //_nPatientID = oPatient.PatientID;
                            //changed by mahesh s on 19/may/2011 for date format.
                            lblDOB_Source.Text = oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") == "01/01/0001" ? string.Empty : oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                            lblSSN_Source.Text = oPatient.DemographicsDetail.PatientSSN;

                            lblToBeMergeDOB.Text = lblDOB_Source.Text;
                            lblToBeMergeSSN.Text = lblSSN_Source.Text;
                            oPatient.Dispose();
                            oPatient = null;
                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message.ToString(), false);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();                
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();
            }
        }


        //list control patient select.
        void oListControl_ItemSelectedClick1(object sender, EventArgs e)
        {
            try
            {
                {
                    ClearToBeMergeControls();
                    _nToBeMergePatientID = oListControl.SelectedItems[0].ID;
                    txtToBeMergePatientName.Tag = oListControl.SelectedItems[0].Description;
                    txtToBeMergePatientName.Text = oListControl.SelectedItems[0].Code + "-" + oListControl.SelectedItems[0].Description;



                    if (_nToBeMergePatientID != 0)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);

                        Patient oPatient = ogloPatient.GetPatientDemo(_nToBeMergePatientID);
                        if (oPatient != null)
                        {
                            //_nPatientID = oPatient.PatientID;
                            //changed by mahesh s on 19/may/2011 for date format.
                            lblToBeMergeDOB.Text = oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") == "01/01/0001" ? string.Empty : oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                            lblToBeMergeSSN.Text = oPatient.DemographicsDetail.PatientSSN;
                            oPatient.Dispose();
                            oPatient = null;
                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message.ToString(), false);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();
            }
        }



        //added by mahesh s on 05-may-2011 for set the context patient at form load.
        void SetContextPatient_Load()
        {
            try
            {
                {
                    

                    if (_nPatientID != 0)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                        Patient oPatient = ogloPatient.GetPatientDemo(_nPatientID);
                        if (oPatient != null)
                        {
                            txtPatientName.Tag = oPatient.DemographicsDetail.PatientFirstName;
                            txtPatientName.Text = oPatient.DemographicsDetail.PatientCode + "-" 
                                                  + oPatient.DemographicsDetail.PatientFirstName + " " 
                                                  + oPatient.DemographicsDetail.PatientLastName;
                            //changed by mahesh s on 19/may/2011 for date format.
                            lblDOB_Source.Text = oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy")=="01/01/0001" ? string.Empty : oPatient.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy");
                            lblSSN_Source.Text = oPatient.DemographicsDetail.PatientSSN;

                            txtToBeMergePatientName.Tag = txtPatientName.Tag;
                            txtToBeMergePatientName.Text = txtPatientName.Text;
                            lblToBeMergeDOB.Text = lblDOB_Source.Text;
                            lblToBeMergeSSN.Text = lblSSN_Source.Text;
                            oPatient.Dispose();
                            oPatient = null;
                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message.ToString(), false);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();                
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();
            }
        }

        //list control patient account 1 select.
        private void oListControl_RemoveAccountSelectClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int32 _itemsCnt = 0; _itemsCnt <= oListControl.SelectedItems.Count - 1; _itemsCnt++)
                    {
                        //AccountId
                        _nRemoveAccountID = oListControl.SelectedItems[_itemsCnt].ID;
                        txtPatAccount1.Text = oListControl.SelectedItems[_itemsCnt].Code;
                        txtAccount1Desc.Text = oListControl.SelectedItems[_itemsCnt].Description;
                        dt = GetAccountDetailsById(Convert.ToInt64(_nRemoveAccountID));
                        if (dt != null && dt.Rows.Count > 0)
                        {

                            string guarantordetails = " ";
                            string _tempstr = "";
                            string _sBusinessCenter1="";

                            _sBusinessCenter1 = (dt.Rows[0]["BusinessCenter"] == DBNull.Value ? string.Empty : dt.Rows[0]["BusinessCenter"].ToString().Trim());
                            _tempstr = (dt.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dt.Rows[0]["GuarantorName"].ToString().Trim());
                            guarantordetails = guarantordetails + _tempstr;

                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address1"].ToString().Trim() == "" ? string.Empty : Environment.NewLine + (dt.Rows[0]["Address1"].ToString().Trim() + ','));

                            if (_tempstr.Trim() != "")
                            { guarantordetails = guarantordetails + " " + _tempstr; }
                            else
                            { guarantordetails = guarantordetails + Environment.NewLine; }

                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address2"].ToString().Trim() == "" ? string.Empty : (dt.Rows[0]["Address2"].ToString() + ',')) + " ";

                            guarantordetails = guarantordetails + " " + _tempstr;

                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["City"] == DBNull.Value ? string.Empty : dt.Rows[0]["City"].ToString() == "" ? string.Empty : dt.Rows[0]["City"].ToString() + ',' + Environment.NewLine)
                                + (dt.Rows[0]["State"] == DBNull.Value ? string.Empty : dt.Rows[0]["State"].ToString() == "" ? string.Empty : dt.Rows[0]["State"].ToString() + '-')
                                + (dt.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dt.Rows[0]["Zip"].ToString());

                            guarantordetails = guarantordetails + _tempstr;
                            lblGuarantorDetails.Text = guarantordetails.TrimEnd().TrimEnd(',') + ".";
                            lblBusinessCenter1.Text = _sBusinessCenter1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();                
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();
                if (dt != null)dt.Dispose();
            }
        }

        //list control patient account 2 select.
        private void oListControl_ToBeMergeAccountSelectClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int32 _itemsCnt = 0; _itemsCnt <= oListControl.SelectedItems.Count - 1; _itemsCnt++)
                    {
                        //AccountId
                        _nToBeMergeAccountID = oListControl.SelectedItems[_itemsCnt].ID;
                        txtPatAccount2.Text = oListControl.SelectedItems[_itemsCnt].Code;
                        txtAccount2Desc.Text = oListControl.SelectedItems[_itemsCnt].Description;
                        dt = GetAccountDetailsById(Convert.ToInt64(_nToBeMergeAccountID));
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string guarantordetails = " ";
                            string _tempstr = "";
                            string _sBusinessCenter="";

                            _sBusinessCenter=(dt.Rows[0]["BusinessCenter"] == DBNull.Value ? string.Empty : dt.Rows[0]["BusinessCenter"].ToString().Trim());
                            _tempstr = (dt.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dt.Rows[0]["GuarantorName"].ToString().Trim());
                            guarantordetails = guarantordetails + _tempstr;
                            
                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address1"].ToString().Trim() == "" ? string.Empty : Environment.NewLine + (dt.Rows[0]["Address1"].ToString().Trim() + ','));

                            if (_tempstr.Trim() != "")
                            { guarantordetails = guarantordetails + " " + _tempstr; }
                            else
                            { guarantordetails = guarantordetails + Environment.NewLine; }

                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address2"].ToString().Trim() == "" ? string.Empty : (dt.Rows[0]["Address2"].ToString() + ',')) + " ";

                            guarantordetails = guarantordetails + " " + _tempstr;

                            _tempstr = "";
                            _tempstr = (dt.Rows[0]["City"] == DBNull.Value ? string.Empty : dt.Rows[0]["City"].ToString() == "" ? string.Empty : dt.Rows[0]["City"].ToString() + ',' + Environment.NewLine)
                                + (dt.Rows[0]["State"] == DBNull.Value ? string.Empty : dt.Rows[0]["State"].ToString() == "" ? string.Empty : dt.Rows[0]["State"].ToString() + '-')
                                + (dt.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dt.Rows[0]["Zip"].ToString());

                            guarantordetails = guarantordetails + _tempstr;

                            //guarantordetails =guarantordetails  + (dt.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dt.Rows[0]["GuarantorName"].ToString().Trim()) ;
                            //guarantordetails = guarantordetails + " " + (dt.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address1"].ToString().Trim() == "" ? string.Empty : Environment.NewLine + (dt.Rows[0]["Address1"].ToString().Trim() + ','));
                            //guarantordetails = guarantordetails + " " + (dt.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["Address2"].ToString().Trim() == "" ? string.Empty : (dt.Rows[0]["Address2"].ToString() + ',')) + " ";
                            //guarantordetails = guarantordetails + (dt.Rows[0]["City"] == DBNull.Value ? string.Empty : dt.Rows[0]["City"].ToString() == "" ? string.Empty : dt.Rows[0]["City"].ToString() + ',' + Environment.NewLine )
                            //    + (dt.Rows[0]["State"] == DBNull.Value ? string.Empty : dt.Rows[0]["State"].ToString() == "" ? string.Empty : dt.Rows[0]["State"].ToString() + '-')
                            //    + (dt.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dt.Rows[0]["Zip"].ToString());

                            lblToBeGuarantorDetails.Text = guarantordetails.TrimEnd().TrimEnd(',') + ".";
                            lblBusinessCenter2.Text = _sBusinessCenter;

                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                panel2.Hide();
                pnlMain.Show();                
                panel3.Show();
                pnlToBeMergeMain.Show();
                pnlGIContactDetails.Show();
                pnlMergeAccount.Show();
                pnl_tlsp.Show();

                if (dt != null)
                    dt.Dispose();
            }
        }

        #endregion

        #region " Private Methods "

        //added by mahesh s on 03-may-2011 for clear all form controls.
        private void ClearControls()
        {
            txtPatientName.Clear();
            lblDOB_Source.Text = "";
            lblSSN_Source.Text = "";
            txtPatAccount1.Clear();
            txtAccount1Desc.Text = "";
            lblGuarantorDetails.Text = "";
            lblBusinessCenter1.Text = "";
            _nPatientID = 0;
            _nRemoveAccountID = 0;

            //txtPatAccount2.Clear();
            //txtAccount2Desc.Text = "" ;
            //lblToBeGuarantorDetails.Text = "";
            //_nToBeMergePatientID = 0;
            //_nToBeMergeAccountID = 0;
        }

        private void ClearToBeMergeControls()
        {
            txtToBeMergePatientName.Clear();
            lblToBeMergeDOB.Text = "";
            lblToBeMergeSSN.Text = "";
            txtPatAccount2.Clear();
            txtAccount2Desc.Text = "";
            lblToBeGuarantorDetails.Text = "";
            lblBusinessCenter2.Text = "";
            _nToBeMergePatientID = 0;
            _nToBeMergeAccountID = 0;
        }

        //validate date before merge accounts.
        private bool ValidateData()
        {
            try
            {
                if (txtPatientName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Select Patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatientName.Focus();
                    return false;
                }
                else if (txtToBeMergePatientName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Select Patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToBeMergePatientName.Focus();
                    return false;
                }
                else if (txtPatAccount1.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Select the Patient Account 1 which you would like to merge the account.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatAccount1.Focus();
                    return false;
                }
                else if (txtPatAccount2.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Select the Patient Account 2 into which you would like to merge the Patient Account 1.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatAccount2.Focus();
                    return false;
                }
                else if (txtPatAccount1.Text.Trim() == txtPatAccount2.Text.Trim())
                {
                    MessageBox.Show("Patient Account 1 and Patient Account 2 should be different.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatAccount2.Focus();
                    return false;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        public void MergeAccount(Int64 FromAccountID,Int64 ToAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            string machineName = System.Environment.MachineName;
            Int64 userID = Convert.ToInt64(appSettings["UserID"].ToString());
            string _username="";
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _username = Convert.ToString(appSettings["UserName"]);
                }
            }
            try
            {
                odbParams.Add("@From_nPAccountID", FromAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@To_nPAccountID", ToAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@MachineName", machineName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@nUserID", userID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nClinicID", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@UserName", _username, ParameterDirection.Input, SqlDbType.VarChar);
               
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
                oDB.Dispose();
                odbParams.Dispose();
            }
        }

        //get guarantor details.
        private DataTable GetAccountDetailsById(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtAccountDetails = new DataTable();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountId", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts", oParameters, out dtAccountDetails);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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
            return dtAccountDetails;
        }


        #endregion
    }
    
}