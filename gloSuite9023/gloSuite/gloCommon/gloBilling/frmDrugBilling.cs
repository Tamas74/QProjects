using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloDatabaseLayer;
using gloBilling.Common;

namespace gloBilling
{
    public partial class frmDrugBilling : Form
    {


        #region "Variable declaration"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _sqlDatabaseConnectionString = "";
        public TransactionLine _oTransLine = new TransactionLine();
        string _strMessageBoxCaption = string.Empty;
        public Boolean oDialogResult = false;
        private Boolean _bIsVoid = false;
        private Boolean _bIsOpenforModify = false;
        private Boolean IsIncDescriptionCheck = false;

        #endregion

        #region "Properties"

        public Boolean bIsVoid
        {
            get { return _bIsVoid; }
            set { _bIsVoid = value; }
        }

        //Specifies whether its open for modify or new 
        public Boolean bIsOpenforModify
        {
            get { return _bIsOpenforModify; }
            set { _bIsOpenforModify = value; }
        }

        #endregion

        #region " Constructor "

        public frmDrugBilling(string databaseconnectionstring, TransactionLine oTransLine, Boolean IsIncDescriptionCheck=false)
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _strMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _strMessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _strMessageBoxCaption = "gloPM"; ; }

            #endregion

            _sqlDatabaseConnectionString = databaseconnectionstring;
            _oTransLine = oTransLine;
            this.IsIncDescriptionCheck = IsIncDescriptionCheck;

        }

        #endregion

        #region " Form Load "

        private void frmDrugBilling_Load(object sender, EventArgs e)
        {
            this.txtNDCCode.TextChanged -= new System.EventHandler(this.txtNDCCode_TextChanged);
            try
            {
                FillUnitofMeasurement();

                //To Disable the copy paste on right click of the text box
                txtNDCCode.ContextMenu = null;// new ContextMenu();
                txtNDCQty.ContextMenu = null;// new ContextMenu();


                if (_bIsVoid == true)
                    tlb_Ok.Enabled = false;

                if (_oTransLine != null)
                {
                    lblCPTCodeText.Text = _oTransLine.CPTCode;
                    lblCPTDescText.Text = _oTransLine.CPTDescription;
                    if (lblCPTDescText.Text.Length > 28)
                        toolTip1.SetToolTip(lblCPTDescText, lblCPTDescText.Text);

                    lblDOSText.Text = Convert.ToDateTime(_oTransLine.DateServiceFrom).ToString("MM/dd/yyyy");
                    lblMod1Text.Text = _oTransLine.Mod1Code;
                    lblMod2Text.Text = _oTransLine.Mod2Code;
                    txtNDCCode.Text = _oTransLine.NDCCode;
                    txtNDCQty.Text = _oTransLine.NDCUnit;
                    //Prescription number
                    txtPrescription.Text = _oTransLine.Prescription;

                    if (_oTransLine.PrescriptionDescription != null && Convert.ToString(_oTransLine.PrescriptionDescription) != "")
                    {
                        chkIncDesc.Checked = true;
                        txtPrescriptionDesc.Text = _oTransLine.PrescriptionDescription;
                        txtPrescriptionDesc.ReadOnly = false;
                    }
                    else if (bIsOpenforModify == false && this.IsIncDescriptionCheck && gloCharges.GetInsDescCheckStaus(_oTransLine.CPTCode))
                    {
                        chkIncDesc.Checked = true;
                    }

                    if (_oTransLine.NDCUnitCode != null && _oTransLine.NDCUnitCode != "")
                    {
                        if (_oTransLine.NDCUnitDescription != null && _oTransLine.NDCUnitDescription != string.Empty)
                            cmbUnit.Text = _oTransLine.NDCUnitCode + "-" + _oTransLine.NDCUnitDescription;
                        else
                            cmbUnit.Text = _oTransLine.NDCUnitCode;
                    }
                    else
                    {
                        cmbUnit.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                this.txtNDCCode.TextChanged += new System.EventHandler(this.txtNDCCode_TextChanged);
            }

        }

        #endregion

        #region " Public & Private Methods "

        private void FillUnitofMeasurement()
        {
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            DataSet dsMeasureUnit = new DataSet();
            try
            {
               
                oDB.Connect(false);
                oDB.Retrive_Query("Select nUnitID,sUnitOfMeasurement+ case isnull(sUnitDescription,'') when '' then '' else '-' + sUnitDescription end as sUnitOfMeasurement  from BL_Measurement_Unit WITH(NOLOCK)", out dsMeasureUnit);
                if (dsMeasureUnit.Tables[0].Rows.Count > 0)
                {
                    cmbUnit.Items.Clear();
                    cmbUnit.DataSource = dsMeasureUnit.Tables[0];
                    cmbUnit.DisplayMember = "sUnitOfMeasurement";
                    cmbUnit.ValueMember = "nUnitID";
                }
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        #endregion

        #region " Form Controls Events "

        private void btnAddNDCCode_Click(object sender, EventArgs e)
        {
            frmBillingNDCCodeSelection ofrmBillingNDCCodeSelection = new frmBillingNDCCodeSelection(_sqlDatabaseConnectionString);
            try
            {
                //ofrmBillingNDCCodeSelection.WindowState = FormWindowState.Normal;
                //ofrmBillingNDCCodeSelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingNDCCodeSelection.ShowDialog(this);
                if (ofrmBillingNDCCodeSelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingNDCCodeSelection.SelectedTrayID > 0)
                    {

                        cmbNDCCode.SelectedValue = ofrmBillingNDCCodeSelection.SelectedTrayID;
                        lblNDCCodetext.Text = ofrmBillingNDCCodeSelection.SelectedTrayName;
                    }
                    else
                    { cmbNDCCode.SelectedValue = -1; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                ofrmBillingNDCCodeSelection.Dispose();
            }
           
            
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            lblNDCCodetext.Text = "";
        }

        private void txtNDCQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //To Validate the Decimal Places
                int IndexofDecimal = (txtNDCQty.Text.Contains(".") == false ? -1 : txtNDCQty.Text.Trim().Substring(txtNDCQty.Text.Trim().IndexOf(".")).Length);
                if (((txtNDCQty.Text.Contains(".")) && (e.KeyChar == 46)))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 46;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }


        private void txtNDCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //To Exclude the special characters from typing
                String abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                if ((abc.IndexOf(e.KeyChar.ToString().ToLower()) == -1) && (e.KeyChar != '\b'))
                {
                    e.Handled = true;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void txtNDCQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //To Disable the Copy paste For NDC Qty
                if (e.Modifiers == Keys.Control)
                {
                    e.Handled = true;
                    txtNDCQty.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }


        private void txtNDCCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //To Disable the Copy paste for NDC Code
                if (e.Modifiers == Keys.Control)
                {
                    e.Handled = true;

                    txtNDCCode.SelectionLength = 0;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

  

        #endregion

        #region "Tool Strip Events"

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNDCCode.Text.Trim() != "")
                {
                    if (txtNDCCode.Text.Trim().Length < 11)
                    {
                        MessageBox.Show("NDC Code cannot be less than 11 characters.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNDCCode.Focus();
                        return;
                    }
                   
                    if (txtNDCQty.Text.Trim() != string.Empty)
                    {
                        if ((txtNDCQty.Text.Trim() == ".") || (txtNDCQty.Text.Trim().IndexOf(".") > 9) || (txtNDCQty.Text.Trim().IndexOf(".") == txtNDCQty.Text.Trim().Length - 1))
                        {
                            MessageBox.Show("Invalid NDC Quantity.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNDCQty.Focus();
                            return;
                        }


                        if ((txtNDCQty.Text.Trim().Length > 9) && (txtNDCQty.Text.Trim().IndexOf(".") < 0))
                        {
                            MessageBox.Show("Invalid NDC Quantity.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNDCQty.Focus();
                            return;
                        }
                        if (txtNDCQty.Text.Trim().Contains("."))
                        {
                            if (txtNDCQty.Text.Trim().IndexOf(".") < (txtNDCQty.Text.Trim().Length - 4))
                            {
                                MessageBox.Show("Invalid NDC Quantity.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNDCQty.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (txtNDCQty.Text.Trim() != string.Empty)
                    {
                        
                        MessageBox.Show("Enter NDC Code.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNDCCode.Focus();
                        return;
                        
                    }

                    if (txtPrescription.Text.Trim() != string.Empty)
                    {
                        MessageBox.Show("Enter NDC Code.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPrescription.Focus();
                        return;

                    }
                }
                if (chkIncDesc.Checked && txtPrescriptionDesc.Text.Trim() == "") 
                {
                    MessageBox.Show("Enter Inc. Desc.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrescriptionDesc.Focus();
                    return;
                }

                
                string NDCQty = "";
                NDCQty = txtNDCQty.Text;
                if (txtNDCQty.Text.Contains("."))
                {
                    if (txtNDCQty.Text.IndexOf(".") == 0)
                        NDCQty = "0" + txtNDCQty.Text;
                }
                
                _oTransLine.NDCCode = txtNDCCode.Text;
                _oTransLine.NDCUnit = (NDCQty==""?"":(Convert.ToDouble(NDCQty.Trim())).ToString());
                _oTransLine.NDCUnitCode = (cmbUnit.Text.Contains("-")==true?cmbUnit.Text.Substring(0,cmbUnit.Text.IndexOf("-")):cmbUnit.Text);
                _oTransLine.Prescription = txtPrescription.Text; //Prescription number
                _oTransLine.PrescriptionDescription = txtPrescriptionDesc.Text; //Prescription Description

                
                if (cmbUnit.Text.Contains("-"))
                    _oTransLine.NDCUnitDescription = cmbUnit.Text.Substring(cmbUnit.Text.IndexOf("-") + 1);
                else
                    _oTransLine.NDCUnitDescription = null;

                // if user removed NDC Code from Modify Charges then clear quantity and unit als0
                if (txtNDCCode.Text.Trim() == "")
                {
                    _oTransLine.NDCUnit = null;
                    _oTransLine.NDCUnitDescription= null;
                    _oTransLine.NDCUnitCode = null;
                    _oTransLine.Prescription = null;  //Prescription number
                    //_oTransLine.PrescriptionDescription = null;  //Prescription Description
                }              
                //end+             
               oDialogResult = true;
               this.Close();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void tlb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                oDialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        private void chkIncDesc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncDesc.Checked)
            {
                if (_oTransLine.CPTDescription != string.Empty)
                {
                    txtPrescriptionDesc.Text = _oTransLine.CPTDescription;
                    txtPrescriptionDesc.ReadOnly = false;
                }
            }
            else
            {
                txtPrescriptionDesc.Text = "";
                txtPrescriptionDesc.ReadOnly = true;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupCharges, gloAuditTrail.ActivityType.Modify, "User has uncheck Inc.Description field", 0, _oTransLine.TransactionId, 0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM, true);
 
            }

        }

        private void txtNDCCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtNDCCode.Text.Length > 0)
            //{
            //    chkIncDesc.Checked = true;
            //}
            //else
            //{
            //    chkIncDesc.Checked = false;
            //}
        }

    }
}
