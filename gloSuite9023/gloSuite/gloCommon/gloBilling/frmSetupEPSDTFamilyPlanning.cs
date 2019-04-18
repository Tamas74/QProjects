using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using gloBilling.Common;
using gloSettings;


namespace gloBilling
{
    public partial class frmSetupEPSDTFamilyPlanning : Form
    {

        #region "Variable Declarations"

        private ComboBox combo;
        private static frmSetupEPSDTFamilyPlanning frm; 

        #endregion

        #region "Properties"

        public Boolean bIsVoid { get; set; }
        public Boolean DialogResults { get; set; }
        public TransactionLine LineObject { get; set; }
        public EPSDTFamilyPlanningClaim ClaimEPSDT { get; set; }

        #endregion               

        #region "Constructor"

        public frmSetupEPSDTFamilyPlanning()
        {
            InitializeComponent();
        }


        public frmSetupEPSDTFamilyPlanning(TransactionLine oTransLine, EPSDTFamilyPlanningClaim oEPSDTFamilyPlanning)
        {
            InitializeComponent();

            LineObject = oTransLine;
            ClaimEPSDT = (oEPSDTFamilyPlanning == null ? new EPSDTFamilyPlanningClaim() : oEPSDTFamilyPlanning);
        } 

        #endregion

        #region "Form Events"

        private void frmSetupEPSDTFamilyPlanning_Load(object sender, EventArgs e)
        {                       
        //    GeneralSettings oSettings = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                if (bIsVoid)
                    tsb_Save.Visible = false;

                frm = new frmSetupEPSDTFamilyPlanning();
                cmbPatientGivenEPSDTReferral.DrawMode = DrawMode.OwnerDrawFixed;
                cmbPatientGivenEPSDTReferral.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

                cmbReferralCode.DrawMode = DrawMode.OwnerDrawFixed;
                cmbReferralCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

                FillReferralTypeCombo();
                FillReferralCodeCombo();

                if (LineObject != null)
                {
                    if (LineObject.CPTCode != null && LineObject.CPTCode != string.Empty)
                    {
                        lblCPTCodeText.Text = LineObject.CPTCode;
                    }
                    if (LineObject.CPTDescription != null && LineObject.CPTDescription != string.Empty)
                    {
                        lblCPTDescText.Text = LineObject.CPTDescription;
                    }

                    if (lblCPTDescText.Text.Length > 28)
                        toolTip1.SetToolTip(lblCPTDescText, lblCPTDescText.Text);

                    if (LineObject.DateServiceFrom != null)
                    {
                        lblDOSText.Text = Convert.ToDateTime(LineObject.DateServiceFrom).ToString("MM/dd/yyyy");
                    }

                    if (LineObject.Mod1Code != null)
                    {
                        lblMod1Text.Text = LineObject.Mod1Code;
                    }

                    if (LineObject.Mod2Code != null)
                    {
                        lblMod2Text.Text = LineObject.Mod2Code;
                    }

                   // if (LineObject.ServiceIsTheScreening != null)
                    {
                        chkThisServiceIsTheScreening.Checked = LineObject.ServiceIsTheScreening;
                    }

               //     if (LineObject.ServiceIsTheResultOfScreening!=null)
                    {
                        chkThisServiceIsTheResultOfScreening.Checked = LineObject.ServiceIsTheResultOfScreening;
                    }

                //    if (LineObject.ServiceFamilyPlanningIndicator != null)
                    {
                        chkServiceFamilyPlanningIndicator.Checked = LineObject.ServiceFamilyPlanningIndicator;
                    }

                }

                if (ClaimEPSDT != null)
                {
                    chkClaimIncludesanEPSDTScreening.Checked = ClaimEPSDT.ClaimIncludeEPSDTScreening;
                    chkPatientGivenEPSDTReferral.Checked = ClaimEPSDT.PatientGivenEPSDTReferral;

                    cmbPatientGivenEPSDTReferral.SelectedValue = (ClaimEPSDT.ReferralType != null && ClaimEPSDT.ReferralType != string.Empty ? ClaimEPSDT.ReferralType : "ST");
                    cmbReferralCode.SelectedValue = (ClaimEPSDT.ReferralCode != null ? ClaimEPSDT.ReferralCode : "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
           
        }

        private void frmSetupEPSDTFamilyPlanning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm != null) { frm.Dispose(); frm = null; }
            if (combo != null) { combo.Dispose(); combo = null; }
        }

        #endregion

        #region "Private and Public methods"

        private void FillReferralCodeCombo()
        {
            DataTable _dtCode = null;
            DataRow _dtRow = null;
            try
            {
                _dtCode = new DataTable();
                _dtCode.Columns.Add("sCode", typeof(System.String));
                _dtCode.Columns.Add("sDescription", typeof(System.String));

                _dtRow = _dtCode.NewRow();
                _dtRow["sCode"] = "ST";
                _dtRow["sDescription"] = "New Services Requested";
                _dtCode.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dtCode.NewRow();
                _dtRow["sCode"] = "AV";
                _dtRow["sDescription"] = "Patient refused referral";
                _dtCode.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dtCode.NewRow();
                _dtRow["sCode"] = "S2";
                _dtRow["sDescription"] = "Currently under treatment for referred diagnostic or corrective health problem";
                _dtCode.Rows.Add(_dtRow);
                _dtRow = null;

                cmbPatientGivenEPSDTReferral.DataSource = _dtCode.Copy();
                cmbPatientGivenEPSDTReferral.ValueMember = "sCode";
                cmbPatientGivenEPSDTReferral.DisplayMember = "sDescription";
                cmbPatientGivenEPSDTReferral.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_dtCode != null) { _dtCode.Dispose(); }
                if (_dtRow != null) { _dtRow = null; }
            }
        }

        private void FillReferralTypeCombo()
        {
            DataTable _dt = null;
            DataRow _dtRow = null;
            try
            {
                _dt = new DataTable();
                _dt.Columns.Add("sCode", typeof(System.String));
                _dt.Columns.Add("sDescription", typeof(System.String));

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "";
                _dtRow["sDescription"] = "";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YM";
                _dtRow["sDescription"] = "Medical";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YD";
                _dtRow["sDescription"] = "Dental";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YV";
                _dtRow["sDescription"] = "Vision";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YH";
                _dtRow["sDescription"] = "Hearing";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YB";
                _dtRow["sDescription"] = "Behavioral Health";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dtRow = _dt.NewRow();
                _dtRow["sCode"] = "YO";
                _dtRow["sDescription"] = "Other";
                _dt.Rows.Add(_dtRow);
                _dtRow = null;

                _dt.AcceptChanges();
                cmbReferralCode.DataSource = _dt.Copy();
                cmbReferralCode.ValueMember = "sCode";
                cmbReferralCode.DisplayMember = "sDescription";
                cmbReferralCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_dt != null) { _dt.Dispose(); }
                if (_dtRow != null) { _dtRow = null; }
            }

        } 

        #endregion

        #region  "Menu Events"

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkClaimIncludesanEPSDTScreening.Checked)
                {
                    ClaimEPSDT.ClaimIncludeEPSDTScreening = true;
                }
                else
                {
                    ClaimEPSDT.ClaimIncludeEPSDTScreening = false;
                }

                if (chkPatientGivenEPSDTReferral.Checked)
                {
                    ClaimEPSDT.PatientGivenEPSDTReferral = true;
                }
                else
                {
                    ClaimEPSDT.PatientGivenEPSDTReferral = false;
                }

                if (cmbPatientGivenEPSDTReferral.Text != string.Empty)
                {
                    ClaimEPSDT.ReferralType = Convert.ToString(cmbPatientGivenEPSDTReferral.SelectedValue);
                }
                else
                {
                    ClaimEPSDT.ReferralType = string.Empty;
                }

                if (cmbReferralCode.Text != string.Empty)
                {
                    ClaimEPSDT.ReferralCode = Convert.ToString(cmbReferralCode.SelectedValue);
                }
                else
                {
                    ClaimEPSDT.ReferralCode = string.Empty;
                }

                if (LineObject != null)
                {
                    if (chkThisServiceIsTheScreening.Checked)
                    {
                        LineObject.ServiceIsTheScreening = true;
                    }
                    else
                    {
                        LineObject.ServiceIsTheScreening = false;
                    }

                    if (chkThisServiceIsTheResultOfScreening.Checked)
                    {
                        LineObject.ServiceIsTheResultOfScreening = true;
                    }
                    else
                    {
                        LineObject.ServiceIsTheResultOfScreening = false;
                    }

                    if (chkServiceFamilyPlanningIndicator.Checked)
                    {
                        LineObject.ServiceFamilyPlanningIndicator = true;
                    }
                    else
                    {
                        LineObject.ServiceFamilyPlanningIndicator = false;
                    }
                }

                DialogResults = true;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResults = false;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        } 

        #endregion

        #region "Form Control's Event"

        private void cmbPatientGivenEPSDTReferral_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (combo.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sDescription"]), combo) >= combo.DropDownWidth - 20)
                    {
                        string txt = Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sDescription"]);
                        if (toolTip1.GetToolTip(combo) != txt)
                        {
                            toolTip1.SetToolTip(combo, txt);
                        }
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(combo, "");
                    }

                }
            }
            catch { }
           
            
        }

        private void cmbReferralCode_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (combo.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sDescription"]), combo) >= combo.DropDownWidth - 20)
                    {
                        string txt = Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sDescription"]);
                        if (toolTip1.GetToolTip(combo) != txt)
                        {
                            toolTip1.SetToolTip(combo, txt);
                        }
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(combo, "");
                    }

                }
            }
            catch { }
            
        }

        private void chkClaimIncludesanEPSDTScreening_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkClaimIncludesanEPSDTScreening.Checked)
                {
                    chkPatientGivenEPSDTReferral.Enabled = true;
                    chkThisServiceIsTheScreening.Enabled = true;
                }
                else
                {
                    chkPatientGivenEPSDTReferral.Enabled = false;


                    chkPatientGivenEPSDTReferral.Checked = false;
                    chkPatientGivenEPSDTReferral.Enabled = false;

                    cmbPatientGivenEPSDTReferral.SelectedIndex = 0;
                    cmbReferralCode.SelectedIndex = 0;

                    chkThisServiceIsTheScreening.Checked = false;
                    chkThisServiceIsTheScreening.Enabled = false;
                }
            }
            catch { }
            
        }

        private void chkPatientGivenEPSDTReferral_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPatientGivenEPSDTReferral.Checked)
                {
                    cmbPatientGivenEPSDTReferral.Enabled = true;
                    cmbReferralCode.Enabled = true;
                }
                else
                {
                    cmbPatientGivenEPSDTReferral.Enabled = false;
                    cmbReferralCode.Enabled = false;

                    cmbPatientGivenEPSDTReferral.SelectedIndex = 0;
                    cmbReferralCode.SelectedIndex = 0;
                }
            }
            catch { }
            
        }

        #endregion

        # region ToolTip
        //Event for showing the ToolTip on DropList 
        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (combo.Items.Count > 0 && e.Index >= 0)
                {

                    e.DrawBackground();
                    using (SolidBrush br = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                    }

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        if (combo.DroppedDown)
                        {
                            string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                            if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                            {
                                if (toolTip1.GetToolTip(combo) != txt)
                                {
                                    this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                                }
                            }
                            else
                            {
                                this.toolTip1.SetToolTip(combo, "");
                            }
                        }
                        else
                        {
                            this.toolTip1.Hide(combo);
                        }
                    }
                    else
                    {
                        //this.toolTip1.SetToolTip(combo,"");
                    }
                    e.DrawFocusRectangle();
                }
            }
            catch { }            
        }

        //Function For Calculating the Lenghth of the Items in the combo box
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            try
            {
                Graphics g = frm.CreateGraphics();
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            catch { }

            return width;
        }

        #endregion                                          
       
    }
}
