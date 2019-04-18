using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloUIControlLibrary;

namespace ChargeRules
{
    public partial class frmClaimRuleVerification : Form
    {
        #region  Private Variables
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;
        private string _username = "";
        private Int64 _UserID = 0;
      //  private bool _IsValidDate = true;
        private const int COL_LineNo = 0;
        private const int COL_ChargeFromDOS = 1;
        private const int COL_ChargeToDOS = 2;
        private const int COL_POSCODE = 3;
        private const int COL_CPTCode = 4;
        private const int COL_Dx1Code = 5;
        private const int COL_Dx2Code = 6;
        private const int COL_Dx3Code = 7;
        private const int COL_Dx4Code = 8;
        private const int COL_Mod1Code = 9;
        private const int COL_Mod2Code = 10;
        private const int COL_Mod3Code = 11;
        private const int COL_Mod4Code = 12;
        private const int COL_Unit = 13;
        private const int COL_RenderingProvider = 14;
        private const int COL_RenderingProviderNPI = 15;
        private Int64 nRuleID = 0;
        #endregion

        #region Constructor
        public frmClaimRuleVerification(string DatabaseConnectionString, Int64 nRuleID)
        {
            InitializeComponent();
            this._databaseconnectionstring = DatabaseConnectionString;
            this._ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            this._messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            this._UserID = gloGlobal.gloPMGlobal.UserID;
            this._username = gloGlobal.gloPMGlobal.UserName;
            this.nRuleID = nRuleID;
        }
        #endregion

        #region Form events
        private void frmClaimRuleVerification_Load(object sender, EventArgs e)
        {
          
            try
            {
                SetFormLoadData();
                
                }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure you want to Close ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tls_btnAddLine_Click(object sender, EventArgs e)
        {
            try
            {
                c1Charge.Rows.Add();
                c1Charge.SetData(c1Charge.Rows.Count - 1, COL_LineNo, c1Charge.Rows.Count - 1);
                c1Charge.SetData(c1Charge.Rows.Count - 1, COL_ChargeFromDOS, System.DateTime.Now.ToShortDateString());
                c1Charge.SetData(c1Charge.Rows.Count - 1, COL_ChargeToDOS, System.DateTime.Now.ToShortDateString());
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tls_btnRemoveLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Charge.Rows.Count > 1)
                {
                    if (MessageBox.Show("Are you sure you want to remove selected service line?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        c1Charge.Rows.Remove(c1Charge.RowSel);
                    }
                }
                else
                {
                    MessageBox.Show("No service line available to remove.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods
        private void  SetFormLoadData()
        {
            try
            {
                //DataSet _dsChargesData = null;
                //_dsChargesData = ClsRuleEngine.GetChargesFormData(5);
                FillInsurancePlanName();
                FillInsuranceCompanyName();
                FillRelationToSubscriber();
                FillFacilities();
                FillBillingProviders();
                FillProviderTypes();
                FillReferralProvider();
                FillClaimDateQualifiers();
                FillOtherClaimDateQualifiers();
                cmbPatientAgeDays.SelectedIndex = 0;
                cmbPatientAgeMonth.SelectedIndex=0;
                cmbPatientAgeYears.SelectedIndex = 0;
                cmbGender.SelectedIndex = 0;
                DesignGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region  called in above SetFormloadMethods
        private void DesignGrid()
        {
            try
            {
                c1Charge.Cols.Count = 16;
                int _width;
                _width = pnlChargeGrid.Width;
                c1Charge.Rows.Count = 2;
                c1Charge.Rows.Fixed = 1;
                c1Charge.Cols.Fixed = 1;
                #region Header
                c1Charge.SetData(0, COL_LineNo, "No.");
                
                c1Charge.SetData(0, COL_ChargeFromDOS, "DOS");
                c1Charge.SetData(0, COL_ChargeToDOS, "To DOS");
                c1Charge.SetData(1, COL_LineNo, c1Charge.Rows.Count - 1);
                c1Charge.SetData(1, COL_ChargeFromDOS,System.DateTime.Now.ToShortDateString() );
                c1Charge.SetData(1, COL_ChargeToDOS, System.DateTime.Now.ToShortDateString());
                c1Charge.SetData(0, COL_POSCODE, "POS");
                c1Charge.SetData(0, COL_CPTCode, "CPT");
                c1Charge.SetData(0, COL_Dx1Code, "Dx1");
                c1Charge.SetData(0, COL_Dx2Code, "Dx2");
                c1Charge.SetData(0, COL_Dx3Code, "Dx3");
                c1Charge.SetData(0, COL_Dx4Code, "Dx4");
                c1Charge.SetData(0, COL_Mod1Code, "M1");
                c1Charge.SetData(0, COL_Mod2Code, "M2");
                c1Charge.SetData(0, COL_Mod3Code, "M3");
                c1Charge.SetData(0, COL_Mod4Code, "M4");
                c1Charge.SetData(0, COL_Unit, "Unit");
                c1Charge.SetData(0, COL_RenderingProvider, "Provider");
                c1Charge.SetData(0, COL_RenderingProviderNPI, "ProviderID");
                
                #endregion

                #region Column Datatypes

                c1Charge.Cols[COL_LineNo].DataType = typeof(System.Int32);
                c1Charge.Cols[COL_ChargeFromDOS].DataType = typeof(System.DateTime);
                c1Charge.Cols[COL_ChargeToDOS].DataType = typeof(System.DateTime);
                 c1Charge.Cols[COL_RenderingProviderNPI].DataType = typeof(System.Int64);

                foreach (C1.Win.C1FlexGrid.Column itemColumn in c1Charge.Cols)
                {
                    itemColumn.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                }

                #endregion

                #region Widths
                c1Charge.Cols[COL_LineNo].Width = Convert.ToInt32(_width * 0.030);

                c1Charge.Cols[COL_ChargeFromDOS].Width = Convert.ToInt32(_width * 0.10);
                c1Charge.Cols[COL_ChargeToDOS].Width = Convert.ToInt32(_width * 0.10);
                c1Charge.Cols[COL_POSCODE].Width = Convert.ToInt32(_width * 0.05);
                c1Charge.Cols[COL_CPTCode].Width = Convert.ToInt32(_width * 0.10);

                c1Charge.Cols[COL_Dx1Code].Width = Convert.ToInt32(_width * 0.09);
                c1Charge.Cols[COL_Dx2Code].Width = Convert.ToInt32(_width * 0.09);
                c1Charge.Cols[COL_Dx3Code].Width = Convert.ToInt32(_width * 0.09);
                c1Charge.Cols[COL_Dx4Code].Width = Convert.ToInt32(_width * 0.09);

                c1Charge.Cols[COL_Mod1Code].Width = Convert.ToInt32(_width * 0.05);
                c1Charge.Cols[COL_Mod2Code].Width = Convert.ToInt32(_width * 0.05);
                c1Charge.Cols[COL_Mod3Code].Width = Convert.ToInt32(_width * 0.05);
                c1Charge.Cols[COL_Mod4Code].Width = Convert.ToInt32(_width * 0.05);

                c1Charge.Cols[COL_Unit].Width = Convert.ToInt32(_width * 0.05);
                c1Charge.Cols[COL_RenderingProvider].Width = Convert.ToInt32(_width * 0.15);                
                c1Charge.Cols[COL_RenderingProviderNPI].Width = Convert.ToInt32(0);
                c1Charge.ExtendLastCol = false;
                #endregion

                #region "Styles"
                c1Charge.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                c1Charge.BackColor = Color.FromArgb(222, 231, 250);
                c1Charge.ForeColor = Color.FromArgb(21, 66, 139);
                c1Charge.Height = 300;

                CellStyle csStyle;// = c1Charge.Styles.Add("rsTransactionLine");
                try
                {
                    if (c1Charge.Styles.Contains("rsTransactionLine"))
                    {
                        csStyle = c1Charge.Styles["rsTransactionLine"];
                    }
                    else
                    {
                        csStyle = c1Charge.Styles.Add("rsTransactionLine");

                    }

                }
                catch
                {
                    csStyle = c1Charge.Styles.Add("rsTransactionLine");


                }


                CellStyle csCurrency;// = c1Charge.Styles.Add("csCurrencyCell");
                try
                {
                    if (c1Charge.Styles.Contains("csCurrencyCell"))
                    {
                        csCurrency = c1Charge.Styles["csCurrencyCell"];
                    }
                    else
                    {
                        csCurrency = c1Charge.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csCurrency = c1Charge.Styles.Add("csCurrencyCell");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "c";
                    csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }

                #endregion "Styles"

                #region "allow Sorthing "
                c1Charge.Cols[COL_LineNo].AllowEditing = false;
                c1Charge.Cols[COL_LineNo].AllowSorting = false;
                c1Charge.Cols[COL_LineNo].AllowSorting = false;
                c1Charge.Cols[COL_ChargeFromDOS].AllowSorting = false;
                c1Charge.Cols[COL_ChargeToDOS].AllowSorting = false;
                c1Charge.Cols[COL_POSCODE].AllowSorting = false;
                c1Charge.Cols[COL_CPTCode].AllowSorting = false;
                c1Charge.Cols[COL_Dx1Code].AllowSorting = false;
                c1Charge.Cols[COL_Dx2Code].AllowSorting = false;
                c1Charge.Cols[COL_Dx3Code].AllowSorting = false;
                c1Charge.Cols[COL_Dx4Code].AllowSorting = false;
                c1Charge.Cols[COL_Mod1Code].AllowSorting = false;
                c1Charge.Cols[COL_Mod2Code].AllowSorting = false;
                c1Charge.Cols[COL_Mod3Code].AllowSorting = false;
                c1Charge.Cols[COL_Mod4Code].AllowSorting = false;
                c1Charge.Cols[COL_Unit].AllowSorting = false;
                c1Charge.Cols[COL_RenderingProvider].AllowSorting = false;
                c1Charge.Cols[COL_RenderingProviderNPI].AllowSorting = false;
                 #endregion

                #region "Alignments "
                c1Charge.Cols[COL_LineNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_LineNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_LineNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_ChargeFromDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_ChargeToDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_POSCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_CPTCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Dx1Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Dx2Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Dx3Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Dx4Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Mod1Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Mod2Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Mod3Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Mod4Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_Unit].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Charge.Cols[COL_RenderingProvider].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FillInsurancePlanName()
        {
            DataTable _dtInsurancePlan = null;
            try
            {
                _dtInsurancePlan = ClsRuleEngine.GetFieldValues("Insurance Plan Name", "");
                _dtInsurancePlan.DefaultView.Sort = "@sFieldName ASC";
                if (_dtInsurancePlan != null && _dtInsurancePlan.Rows.Count > 0)
                {
                    cmbInsurancePlanName.BeginUpdate();
                    cmbInsurancePlanName.DataSource = _dtInsurancePlan;
                    cmbInsurancePlanName.DisplayMember = _dtInsurancePlan.Columns["@sFieldName"].ColumnName;
                    cmbInsurancePlanName.ValueMember = _dtInsurancePlan.Columns["ValueID"].ColumnName;
                    cmbInsurancePlanName.EndUpdate();
                    cmbInsurancePlanName.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillInsuranceCompanyName()
        {

            DataTable dtInsuranceCompanyName = null;
            try
            {
                dtInsuranceCompanyName = ClsRuleEngine.GetInsuranceCompanies();
                dtInsuranceCompanyName.DefaultView.Sort = "sDescription ASC";
                if (dtInsuranceCompanyName != null)
                {
                    cmbInsuranceCompanyName.BeginUpdate();
                    cmbInsuranceCompanyName.DataSource = dtInsuranceCompanyName;
                    cmbInsuranceCompanyName.ValueMember = dtInsuranceCompanyName.Columns["nID"].ColumnName;
                    cmbInsuranceCompanyName.DisplayMember = dtInsuranceCompanyName.Columns["sDescription"].ColumnName;
                    cmbInsuranceCompanyName.EndUpdate();
                    cmbInsuranceCompanyName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FillRelationToSubscriber()
        {
            DataTable _dtPatientRelation = null;
            try
            {
                _dtPatientRelation = ClsRuleEngine.GetFieldValues("Patient RelationShip To Subscriber", "");

                if (_dtPatientRelation != null && _dtPatientRelation.Rows.Count > 0)
                {
                    cmbRelationToSubscriber.BeginUpdate();
                    cmbRelationToSubscriber.DataSource = _dtPatientRelation;
                    cmbRelationToSubscriber.DisplayMember = _dtPatientRelation.Columns["Patient RelationShip To Subscriber"].ColumnName;
                    cmbRelationToSubscriber.ValueMember = _dtPatientRelation.Columns["ValueID"].ColumnName;
                    cmbRelationToSubscriber.EndUpdate();
                    cmbRelationToSubscriber.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillFacilities()
        {
            DataTable dtFacilities = null;
            try
            {
                dtFacilities = ClsRuleEngine.GetCachedFacilities();

                if (dtFacilities != null)
                {
                    cmbFacility.BeginUpdate();
                    cmbFacility.DataSource = dtFacilities;
                    cmbFacility.ValueMember = dtFacilities.Columns["sFacilityCode"].ColumnName;
                    cmbFacility.DisplayMember = dtFacilities.Columns["sFacilityName"].ColumnName;
                    cmbFacility.EndUpdate();
                    cmbFacility.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (dtFacilities != null)
            //    {
            //        dtFacilities.Dispose();
            //        dtFacilities = null;
            //    }
            //}
        }
        private void FillBillingProviders()
        {
            DataTable _dtBillProviders = null;
            try
            {
                _dtBillProviders = ClsRuleEngine.GetCachedProviders();
                if (_dtBillProviders != null)
                {
                    if (_dtBillProviders.Rows.Count > 0)
                    {
                        cmbBillingProvider.BeginUpdate();
                        cmbBillingProvider.DataSource = _dtBillProviders;
                        cmbBillingProvider.ValueMember = _dtBillProviders.Columns["nProviderID"].ColumnName;
                        cmbBillingProvider.DisplayMember = _dtBillProviders.Columns["sProviderName"].ColumnName;
                        cmbBillingProvider.EndUpdate();
                        cmbBillingProvider.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (dtProviders != null)
            //    {
            //        dtProviders.Dispose();
            //        dtProviders = null;
            //    }
            //}
        }
        private void FillProviderTypes()
        {
            DataTable _dtReportingProviderTypes = null;
            try
            {
                _dtReportingProviderTypes = gloGlobal.gloPMMasters.GetProviderReportingQualifier();

                if (_dtReportingProviderTypes != null && _dtReportingProviderTypes.Rows.Count > 0)
                {
                    cmbProviderType.BeginUpdate();
                    cmbProviderType.DataSource = _dtReportingProviderTypes;
                    cmbProviderType.DisplayMember = _dtReportingProviderTypes.Columns["sDescription"].ColumnName;
                    cmbProviderType.ValueMember = _dtReportingProviderTypes.Columns["sQualifier"].ColumnName;
                    cmbProviderType.EndUpdate();


                    //if (DefaultProviderQualifierCode != null && DefaultProviderQualifierCode.Trim() != "")
                    //{ cmbProviderType.SelectedValue = DefaultProviderQualifierCode; }
                    //else
                    { cmbProviderType.SelectedValue = "DN"; } //DN-Referring 
                }
                cmbRelationToSubscriber.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (_dtReportingProviderTypes != null)
            //    {
            //        _dtReportingProviderTypes.Dispose();
            //        _dtReportingProviderTypes = null;
            //    }
            //}

        }
        private void FillReferralProvider()
        {
            DataTable _dtReferingprovider = null;
            try
            {
                _dtReferingprovider = ClsRuleEngine.GetFieldValues("Referring Provider Name", "");

                if (_dtReferingprovider != null && _dtReferingprovider.Rows.Count > 0)
                {
                    cmbReferralProvider.BeginUpdate();
                    cmbReferralProvider.DataSource = _dtReferingprovider;
                    cmbReferralProvider.DisplayMember = _dtReferingprovider.Columns["@sFieldName"].ColumnName;
                    cmbReferralProvider.ValueMember = _dtReferingprovider.Columns["ValueID"].ColumnName;
                    cmbReferralProvider.EndUpdate();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
            try
            {
                _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetBox14DatesQualifiers();

                if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
                {
                    cmbBox14DateQualifier.BeginUpdate();
                    cmbBox14DateQualifier.DataSource = _dtClaimDatesQualifiers;
                    cmbBox14DateQualifier.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                    cmbBox14DateQualifier.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                    cmbBox14DateQualifier.EndUpdate();

                    //Set Onset as default as per previous implementation
                    cmbBox14DateQualifier.SelectedValue = "431";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (_dtClaimDatesQualifiers != null)
            //    {
            //        _dtClaimDatesQualifiers.Dispose();
            //        _dtClaimDatesQualifiers = null;
            //    }
            //}

        }
        private void FillOtherClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
            try
            {
                _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetClaimDatesQualifiers();

                if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
                {
                    cmbBox15DateQualifier.BeginUpdate();
                    cmbBox15DateQualifier.DataSource = _dtClaimDatesQualifiers;
                    cmbBox15DateQualifier.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                    cmbBox15DateQualifier.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                    cmbBox15DateQualifier.EndUpdate();
                    cmbBox15DateQualifier.SelectedIndex = 0;

                    //if (DefaultDateQualifierCode != null && DefaultDateQualifierCode.Trim() != "")
                    //{
                    //    cmbBox15DateQualifier.SelectedValue = DefaultDateQualifierCode;
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (_dtClaimDatesQualifiers != null)
            //    {
            //        _dtClaimDatesQualifiers.Dispose();
            //        _dtClaimDatesQualifiers = null;
            //    }
            //}
        }
        #endregion

        #endregion

        #region " Date Validation form mskdate fields"

        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }

        private void mskDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
              //  _IsValidDate = true;
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid
                          //  _IsValidDate = false;
                            e.Cancel = true;
                        }

                    }


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Specifies that the Date is InValid
             //   _IsValidDate = false;
                e.Cancel = true;
            }
        }

        private void mskDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }


        #endregion " Date Validation "

        #region Related To intrenal controls

        #region Properties fo internal Controls
        public gloListControl ogloGridListControl = null;
        public bool IsInternalControlActive
        {
            get { return pnlInternalControl.Visible; }
            set
            {
                pnlInternalControl.Visible = value;

                if (!pnlInternalControl.Visible)
                {
                    CloseInternalControl();

                }
            }
        }
        public Int32 CurrentRow
        {
            get { return c1Charge.RowSel; }
        }
        public Int32 CurrentColumn
        {
            get { return c1Charge.ColSel; }

        }
        #endregion


        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText, int pnlwidth)
        {
            bool _result = false;
            try
            {
                pnlInternalControl.Width = pnlwidth;
                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloListControl(ControlType, ControlHeader, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                { ogloGridListControl.Search(SearchText); }
                ogloGridListControl.Show();
                #region Heamnt

                int leftX = c1Charge.Cols[ColIndex].Left;
                int rightX = c1Charge.Cols[ColIndex].Right;
                int topY = c1Charge.Rows[RowIndex].Top;
                int bottomY = c1Charge.Rows[RowIndex].Bottom;
                int totWdth = pnlChargeGrid.Width;
                int totHeight = pnlChargeGrid.Height;
                int actualWidth = pnlInternalControl.Width;
                int actualHeight = pnlInternalControl.Height;

                int _RemaingX = totWdth - leftX;
                int _RemaingY = totHeight - bottomY;

                if (_RemaingX < actualWidth)
                {
                    int newX = rightX - actualWidth;

                    if (_RemaingY < actualHeight)
                    {
                        int newTop = topY - actualHeight;
                        pnlInternalControl.SetBounds(newX, newTop, 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        pnlInternalControl.SetBounds(newX, bottomY, 0, 0, BoundsSpecified.Location);
                    }

                }
                else
                {
                    if (_RemaingY < actualHeight)
                    {
                        int newTop = topY - actualHeight;
                        pnlInternalControl.SetBounds(leftX, newTop, 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        pnlInternalControl.SetBounds(leftX, bottomY, 0, 0, BoundsSpecified.Location);

                    }
                }


                #endregion




                #region "Odl Keep it Code"
                int _x = c1Charge.Cols[ColIndex].Left;
                int _y = c1Charge.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _parentleft = pnlChargeGrid.Bounds.Left; //this.Parent.Bounds.Left;
                int _parentwidth = pnlChargeGrid.Bounds.Width;//Hemant
                int _diffFactor = _parentwidth - _x;//added by hemant
                int _c1ChargeWidth = c1Charge.Bounds.Width;
                _x = _c1ChargeWidth - c1Charge.Cols[ColIndex].Width - c1Charge.Cols[COL_Unit].Width;//needed
                int _parentsReamainingHieght = c1Charge.Height - _y;
                int _parentRemainingWidth = pnlInternalControl.Parent.Width - _x;
                if (_parentRemainingWidth < pnlInternalControl.Width)
                {
                    _x = _x - pnlInternalControl.Width;
                }
                if (_height - 10 > _parentsReamainingHieght)
                {
                    _y = _y - _height - 20;
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                { pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location); }
                #endregion
                pnlInternalControl.Visible = true;
                {
                    pnlInternalControl.BringToFront();
                    ogloGridListControl.Focus();
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               RePositionInternalControl();
            }

            return _result;
        }
        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected += new gloListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown += new gloListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _result;
        }
       
        private void RePositionInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                {
                    //if (c1Charge.Rows[CurrentRow].Bottom + c1Charge.ScrollPosition.Y > 220)
                    //{
                    //    pnlInternalControl.SetBounds((c1Charge.Cols[CurrentColumn].Left + c1Charge.ScrollPosition.X), (c1Charge.Rows[CurrentRow].Bottom + c1Charge.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    //}
                    //else
                    //{
                    //    pnlInternalControl.SetBounds((c1Charge.Cols[CurrentColumn].Left + c1Charge.ScrollPosition.X), (c1Charge.Rows[CurrentRow].Bottom + c1Charge.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    //}
                    int _x = c1Charge.Cols[CurrentColumn].Left;
                    int _y = c1Charge.Rows[CurrentRow].Bottom;
                    int _c1ChargeWidth = c1Charge.Bounds.Width;
                    // _x = _c1ChargeWidth - c1Charge.Cols[CurrentColumn].Width - c1Charge.Cols[COL_Unit].Width;//needed
                    _x = c1Charge.Cols[CurrentColumn].Left;
                     int _RequiredWidth = c1Charge.Cols[CurrentColumn].Left + pnlInternalControl.Width;
                     if (_RequiredWidth > c1Charge.Width)
                     {
                         _x = c1Charge.Cols[CurrentColumn].Right-pnlInternalControl.Width;
                     }

                    if ((c1Charge.Bottom - c1Charge.Rows[CurrentRow].Bottom) - c1Charge.ScrollPosition.Y > pnlInternalControl.Height)
                    {
                        //if ((this.Bottom - c1Charge.Rows[CurrentRow].Bottom) - c1Charge.ScrollPosition.Y < pnlInternalControl.Height)
                        //{ 
                        //    pnlInternalControl.Height = (this.Bottom - c1Charge.Rows[CurrentRow].Bottom) - c1Charge.ScrollPosition.Y; 
                        //}

                        pnlInternalControl.SetBounds(_x + c1Charge.ScrollPosition.X, (c1Charge.Rows[CurrentRow].Bottom + c1Charge.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        pnlInternalControl.SetBounds(_x + c1Charge.ScrollPosition.X, (c1Charge.Rows[CurrentRow].Top - pnlInternalControl.Height) + c1Charge.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }
        private string ReplaceField(string sFieldName)
        {
            switch (sFieldName)
            {
                case "No.":
                    sFieldName = "";
                    break;
                case "DOS":
                    sFieldName = "";
                    break;
                case "To DOS":
                    sFieldName = "";
                    break;
                case "POS":
                    sFieldName = "POS";
                    break;
                case "CPT":
                    sFieldName = "CPT Code";
                    break;
                case "Dx1":
                    sFieldName = "Dx1 Code";
                    break;
                case "Dx2":
                    sFieldName = "Dx2 Code";
                    break;
                case "Dx3":
                    sFieldName = "Dx3 Code";
                    break;
                case "Dx4":
                    sFieldName = "Dx4 Code";
                    break;
                case "M1":
                    sFieldName = "Mod1 Code";
                    break;
                case "M2":
                    sFieldName = "Mod2 Code";
                    break;
                case "M3":
                    sFieldName = "Mod3 Code";
                    break;
                case "M4":
                    sFieldName = "Mod4 Code";
                    break;
                case "Unit":
                    sFieldName = "";
                    break;

                default:
                    break;
            }

            return sFieldName;


        }

        public void ogloGridListControl_ItemSelected(object sender, EventArgs e, int x)
        {
            try
            {
                if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                {
                    if (CurrentColumn == COL_RenderingProvider)
                    {
                        c1Charge.SetData(x, COL_RenderingProvider, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                        c1Charge.SetData(x,COL_RenderingProviderNPI , Convert.ToString(ogloGridListControl.SelectedItems[0].ID));
                    }
                    else
                    {
                        c1Charge.SetData(x, CurrentColumn, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                    }
                    this.pnlInternalControl.Hide();
                    c1Charge.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            { IsInternalControlActive = false; }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void c1Charge_StartEdit(object sender, RowColEventArgs e)
        {
            try
            { //Renderring Provider Name
                 string sFieldName="";
                if((c1Charge != null) && (c1Charge.Cols.Count > 0))
                {
                    if (CurrentColumn== COL_RenderingProvider) 
                    {
                        sFieldName="Rendering Provider Name";
                    }
                    else
                    {
                        sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                    }
                }
                else
                {
                    sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                }
                if (!String.IsNullOrEmpty(sFieldName))
                {


                    string _SearchText = "";
                    int pnlwidth = c1Charge.Cols[e.Col].Width * 3;
                    int pnlwidth2 = this.pnlInternalControl.Width;
                    OpenInternalControl(gloGridListControlType.COL_Value, sFieldName, false, e.Row, e.Col, _SearchText, pnlwidth2);
                    if (c1Charge != null && c1Charge.Rows.Count > 0)
                    {
                        _SearchText = Convert.ToString(c1Charge.GetData(CurrentRow, CurrentColumn));
                        if (_SearchText != "" && ogloGridListControl != null)
                        {

                            ogloGridListControl.FillControl(sFieldName, _SearchText);

                        }
                    }



                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void c1Charge_LeaveEdit(object sender, RowColEventArgs e)
        {
            try
            {
                string sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                if (CurrentColumn != COL_ChargeFromDOS && CurrentColumn != COL_ChargeToDOS)
                {
                    if (c1Charge.Cols[CurrentColumn].Caption != "Unit")
                    {
                        if (c1Charge.Editor != null)
                        {
                            c1Charge.ChangeEdit -= new System.EventHandler(this.c1Charge_ChangeEdit);
                            c1Charge.Editor.Text = "";
                            c1Charge.ChangeEdit += new System.EventHandler(this.c1Charge_ChangeEdit);
                        }
                    }
                }
                //else 
                //{

                //    string a = ((C1.Win.C1FlexGrid.C1FlexGridBase)(sender)).Editor.Text;
                //    if (!String.IsNullOrEmpty(a))
                //    {
                //        if (Convert.ToInt32(a) < 0 || Convert.ToInt32(a) > 10)
                //        {
                //            MessageBox.Show("Invalid Value For Unit.Vlaid Range is :{0-9}", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            ((C1.Win.C1FlexGrid.C1FlexGridBase)(sender)).Editor.Text = "";
                //            CellStyle RedS = c1Charge.Styles.Add("RedS");
                //            RedS.BackColor = System.Drawing.Color.Red;
                //            c1Charge.SetCellStyle(e.Row, e.Col, RedS);

                //            e.Cancel = true;
                //        }
                //        else
                //        {
                //            c1Charge.SetCellStyle(e.Row, e.Col,c1Charge.Styles.Normal);
                //        }
                //    }
                // }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void c1Charge_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = c1Charge.Editor.Text;

                string sFieldName = "";
                if ((c1Charge != null) && (c1Charge.Cols.Count > 0))
                {
                    if (CurrentColumn == COL_RenderingProvider)
                    {
                        sFieldName = "Rendering Provider Name";
                    }
                    else
                    {
                        sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                    }
                }
                else
                {
                    sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                }

              //  string sFieldName = ReplaceField(c1Charge.Cols[CurrentColumn].Caption);
                if (ogloGridListControl != null)
                {
                    ogloGridListControl.FillControl(sFieldName, _strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void c1Charge_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.GetCurrentSelectedItem();

                        }
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion

                    if (!pnlInternalControl.Visible)
                    {
                        e.SuppressKeyPress = false;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;

                    #region "Escape Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            IsInternalControlActive = false;

                        }
                    }



                    #endregion

                    if (pnlInternalControl.Visible)
                    {
                        pnlInternalControl.Visible = false;
                        e.SuppressKeyPress = false;
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (c1Charge.ColSel == CurrentColumn)
                    {
                        if (c1Charge.RowSel > 0)
                        {
                            c1Charge.SetData(c1Charge.RowSel, c1Charge.ColSel, "");
                        }
                    }
                }


                else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
                {

                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                    IsInternalControlActive = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void c1Charge_Click(object sender, EventArgs e)
        {
            try
            {
                IsInternalControlActive = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void c1Charge_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
               RePositionInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void c1Charge_BeforeScroll(object sender, RangeEventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible)
                {
                    CloseInternalControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        public void SetSelectedText(string Text)
        {
            this.lblSelectedText.Text = Text;
        }

    

        private void c1Charge_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            try
            {
                if (e.Col == COL_Unit)
                {


                    if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b') //The  character represents a backspace
                    {
                        e.Handled = false; //Do not reject the input
                    }
                    else
                    {
                        e.Handled = true; //Reject the input
                        MessageBox.Show(" Can not be other than number {0-9}", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void tls_Execute_Click(object sender, EventArgs e)
        {
           try{ ValidateRules();
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
               MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }
        private Boolean ValidateRules()
        {
            List<ChargeRules.Claim> ClaimsList = null;
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRuleInfo = null;
            ChargeRules.BusinessOperation businessOperation = null;
            Boolean dlgResultReviewScren = true;
            try
            {
                ClaimsList = this.GenerateClaimRuleObject();
                businessOperation = new BusinessOperation();
                if (nRuleID > 0)
                {
                    triggeredRuleInfo = businessOperation.TestRules(ClaimsList,nRuleID,false);
                }
                else
                {
                    triggeredRuleInfo = businessOperation.TestRules(ClaimsList, 0, false);
                }
                if (triggeredRuleInfo.Count > 0)
                {
                    gloUIControlLibrary.WPFForms.frmTriggeredRules frmTriggeredRules = new gloUIControlLibrary.WPFForms.frmTriggeredRules(triggeredRuleInfo, true);
                    System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmTriggeredRules);
                    _interophelper.Owner = this.Handle;
                    frmTriggeredRules.ShowDialog();
                    dlgResultReviewScren = Convert.ToBoolean(frmTriggeredRules.DialogResult);
                }
                else
                {
                    MessageBox.Show("No rules are Triggered.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return dlgResultReviewScren;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;
            }
        }

        private List<ChargeRules.Claim> GenerateClaimRuleObject()
        {
           List<ChargeRules.Claim> claims = new List<Claim>();

           string sInsuranceCompanyName = string.Empty;
           string sReferringProviderNPI = string.Empty;
           string sOrderingProviderNPI = string.Empty;
           string sSupervisingProviderNPI = string.Empty;
           string sBillingProviderNPI = string.Empty;
           string sPriorAuthorization = string.Empty;

           string sPatientRelationshipToSubscriber = string.Empty;
           string sInsurancePlanName = string.Empty;
           string sPlanType = string.Empty;
           string sReportingCategory = string.Empty;
           string sInsCompanyReportingCategory = string.Empty;         
           List<Insurance> lstInsurance = null;
           try
           {

               if (c1Charge != null && c1Charge.Rows.Count > 0)
               {
                   sInsuranceCompanyName = cmbInsuranceCompanyName.Text.Trim();
                   if (Convert.ToString(cmbProviderType.SelectedValue) == "DN")
                   {
                       sReferringProviderNPI = ClsRuleEngine.GetProviderNPI(Convert.ToInt64(cmbReferralProvider.SelectedValue));
                   }
                   else if (Convert.ToString(cmbProviderType.SelectedValue) == "DK")
                   {
                       sOrderingProviderNPI = ClsRuleEngine.GetProviderNPI(Convert.ToInt64(cmbReferralProvider.SelectedValue));
                   }
                   else
                   {
                       sSupervisingProviderNPI = ClsRuleEngine.GetProviderNPI(Convert.ToInt64(cmbReferralProvider.SelectedValue));
                   }
                   sBillingProviderNPI = ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(cmbBillingProvider.SelectedValue));
                   sPatientRelationshipToSubscriber = cmbRelationToSubscriber.Text.Trim();
                   sInsurancePlanName = cmbInsurancePlanName.Text.Trim();
                   sPlanType = ClsRuleEngine.GetPlanType(Convert.ToInt64(cmbInsurancePlanName.SelectedValue));
                   sPriorAuthorization = txtPriorAuthorization.Text;

                   string sPayerId = ClsRuleEngine.GetPayerId(Convert.ToInt64(cmbInsurancePlanName.SelectedValue));
                   sReportingCategory = ClsRuleEngine.GetReportingCategory(Convert.ToInt64(cmbInsurancePlanName.SelectedValue));
                   sInsCompanyReportingCategory = ClsRuleEngine.GetInsCompanyReportingCategory(Convert.ToInt64(cmbInsurancePlanName.SelectedValue));
                   lstInsurance = new List<Insurance>();

                   ChargeRules.Insurance insurance = new Insurance();
                   insurance.InsurancePayerID = sPayerId;
                   insurance.InsurancePlanName = sInsurancePlanName;
                   insurance.InsurancePlanType = sPlanType;
                   insurance.InsuranceReportingCategory = sReportingCategory;
                   insurance.InsuranceCompanyName = sInsuranceCompanyName;
                   insurance.InsuranceCompanyReportingCategory = sInsCompanyReportingCategory;

                   insurance.ContactID = Convert.ToInt64(cmbInsurancePlanName.SelectedValue);
                   insurance.InsuranceID = Convert.ToInt64(cmbInsuranceCompanyName.SelectedValue);

                   lstInsurance.Add(insurance);
                   insurance = null;

                   for (int i = 1; i < c1Charge.Rows.Count; i++)
                   {


                       ChargeRules.Claim claim = new Claim();


                       claim.InsuranceReportingCategory = sReportingCategory;
                       claim.InsuranceCompanyReportingCategory = sInsCompanyReportingCategory;
                       claim.InsurancePlanType = sPlanType;
                       claim.BillingProviderName = cmbBillingProvider.Text.Trim();
                       claim.BillingProviderNPI = sBillingProviderNPI;
                       claim.InsuranceCompanyName = sInsuranceCompanyName.Trim();
                       claim.InsurancePlanName = sInsurancePlanName;
                       claim.InsurancePayerID = sPayerId;
                       claim.PatientRelationshipToSubscriber = sPatientRelationshipToSubscriber;
                       claim.InsuranceList.AddRange(lstInsurance);


                       if (Convert.ToString(cmbProviderType.SelectedValue) == "DN")
                       {
                           claim.ReferringProviderName = cmbReferralProvider.Text.Trim();
                           claim.ReferringProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                           claim.ReferringProviderNPI = sReferringProviderNPI.Trim();
                       }
                       else if (Convert.ToString(cmbProviderType.SelectedValue) == "DK")
                       {
                           claim.OrderingProviderName = cmbReferralProvider.Text.Trim();
                           claim.OrderingProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                           claim.OrderingProviderNPI = sOrderingProviderNPI.Trim();
                       }
                       else
                       {
                           claim.SupervisingProviderName = cmbReferralProvider.Text.Trim();
                           claim.SupervisingProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                           claim.SupervisingProviderNPI = sSupervisingProviderNPI.Trim();
                       }

                       if (IsValidDate(mskClaimDate.Text))
                       { claim.ClaimDate = Convert.ToDateTime(mskClaimDate.Text.ToString()); }
                       claim.ClaimDateQualifier = Convert.ToString(cmbBox14DateQualifier.SelectedValue);

                       if (IsValidDate(mskOtherClaimDate.Text))
                       { claim.OtherClaimDate = Convert.ToDateTime(mskOtherClaimDate.Text.ToString()); }

                       claim.OtherClaimDateQualifier = Convert.ToString(cmbBox15DateQualifier.SelectedValue);
                       claim.CPTCode = Convert.ToString(c1Charge.GetData(i, COL_CPTCode)).Trim();

                       for (Int32 c = 1; c < c1Charge.Rows.Count; c++)
                       { 
                           claim.CPTCodes.Add(new CPT_Code() 
                           { 
                               CPTCode = Convert.ToString(c1Charge.GetData(c, COL_CPTCode)).Trim() 
                           });


                           if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(c1Charge.GetData(c, COL_Dx1Code)).Trim()))
                           {
                               claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                               {
                                   ClaimDiagnosis = Convert.ToString(c1Charge.GetData(c, COL_Dx1Code)).Trim()
                               });
                           }

                           if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(c1Charge.GetData(c, COL_Dx2Code)).Trim()))
                           {
                               claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                               {
                                   ClaimDiagnosis = Convert.ToString(c1Charge.GetData(c, COL_Dx2Code)).Trim()
                               });
                           }

                           if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(c1Charge.GetData(c, COL_Dx3Code)).Trim()))
                           {
                               claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                               {
                                   ClaimDiagnosis = Convert.ToString(c1Charge.GetData(c, COL_Dx3Code)).Trim()
                               });
                           }

                           if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(c1Charge.GetData(c, COL_Dx4Code)).Trim()))
                           {
                               claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                               {
                                   ClaimDiagnosis = Convert.ToString(c1Charge.GetData(c, COL_Dx4Code)).Trim()
                               });
                           }


                           if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(c1Charge.GetData(c, COL_Mod1Code)).Trim()))
                           {
                               claim.ClaimModfiers.Add(new Claim_Modfier()
                               {
                                   ClaimModifier = Convert.ToString(c1Charge.GetData(c, COL_Mod1Code)).Trim()
                               });

                           }

                           if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(c1Charge.GetData(c, COL_Mod2Code)).Trim()))
                           {
                               claim.ClaimModfiers.Add(new Claim_Modfier()
                               {
                                   ClaimModifier = Convert.ToString(c1Charge.GetData(c, COL_Mod2Code)).Trim()
                               });

                           }

                           if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(c1Charge.GetData(c, COL_Mod3Code)).Trim()))
                           {
                               claim.ClaimModfiers.Add(new Claim_Modfier()
                               {
                                   ClaimModifier = Convert.ToString(c1Charge.GetData(c, COL_Mod3Code)).Trim()
                               });

                           }

                          // bool a = claim.oPropertyValueWithExistsOperator.Any(t => t.PropertyCode == "");

                           //PropertyValueWithExistsOperator otest = new PropertyValueWithExistsOperator();
                           //otest.PropertyCode="";
                           //IEnumerable<PropertyValueWithExistsOperator> oPpertyValueWithExistsOperator = from PropertyValue in claim.oPropertyValueWithExistsOperator.AsEnumerable() where PropertyValue.PropertyCode == Convert.ToString("") select PropertyValue;

                           //var test = claim.oPropertyValueWithExistsOperator.Select(t => t.PropertyCode == otest.PropertyCode);

                           if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(c1Charge.GetData(c, COL_Mod4Code)).Trim()))
                           {
                               claim.ClaimModfiers.Add(new Claim_Modfier()
                               {
                                   ClaimModifier = Convert.ToString(c1Charge.GetData(c, COL_Mod4Code)).Trim()
                               });

                           }

                           if (!claim.RenderringProviderNames.Any(t => t.RenderingProviderName == Convert.ToString(c1Charge.GetData(c, COL_RenderingProvider)).Trim()))
                           {
                               claim.RenderringProviderNames.Add(new RenderringProvider_Name()
                               {
                                   RenderingProviderName = Convert.ToString(c1Charge.GetData(c, COL_RenderingProvider)).Trim()
                               });

                           }

                           if (!claim.RenderringProviderNPIs.Any(t => t.RenderingProviderNPI == Convert.ToString(ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(c1Charge.GetData(c, COL_RenderingProviderNPI)))).Trim()))
                           {
                               claim.RenderringProviderNPIs.Add(new RenderringProvider_NPI()
                               {
                                   RenderingProviderNPI = Convert.ToString(ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(c1Charge.GetData(c, COL_RenderingProviderNPI)))).Trim()
                               });

                           }  
                       }

                       claim.Dx1Code = Convert.ToString(c1Charge.GetData(i, COL_Dx1Code)).Trim();
                       claim.Dx2Code = Convert.ToString(c1Charge.GetData(i, COL_Dx2Code)).Trim();
                       claim.Dx3Code = Convert.ToString(c1Charge.GetData(i, COL_Dx3Code)).Trim();
                       claim.Dx4Code = Convert.ToString(c1Charge.GetData(i, COL_Dx4Code)).Trim();

                       claim.Mod1Code = Convert.ToString(c1Charge.GetData(i, COL_Mod1Code)).Trim();
                       claim.Mod2Code = Convert.ToString(c1Charge.GetData(i, COL_Mod2Code)).Trim();
                       claim.Mod3Code = Convert.ToString(c1Charge.GetData(i, COL_Mod3Code)).Trim();
                       claim.Mod4Code = Convert.ToString(c1Charge.GetData(i, COL_Mod4Code)).Trim();
                       claim.POS = Convert.ToString(c1Charge.GetData(i, COL_POSCODE)).Trim();
                       claim.RenderingProviderName = Convert.ToString(c1Charge.GetData(i, COL_RenderingProvider)).Trim();
                       claim.RenderingProviderNPI = ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64((c1Charge.GetData(i, COL_RenderingProviderNPI))));  

                       if (Convert.ToString(c1Charge.GetData(i, COL_Unit)) == "")
                       {
                           claim.Unit = 0;
                       }
                       else
                       {
                           claim.Unit = Convert.ToDecimal(c1Charge.GetData(i, COL_Unit));
                       }
                       claim.FacilityName = Convert.ToString(cmbFacility.Text).Trim();

                       claim.ChargeFromDOS = Convert.ToDateTime(DateAsDate(DateAsNumber(Convert.ToString(c1Charge.GetData(i, COL_ChargeFromDOS)).Trim())));
                       claim.ChargeToDOS = Convert.ToDateTime(DateAsDate(DateAsNumber(Convert.ToString(c1Charge.GetData(i, COL_ChargeToDOS)).Trim())));

                       if (IsValidDate(mskHospitaliztionFrom.Text))
                       { claim.HospitalizationFromDOS = Convert.ToDateTime(mskHospitaliztionFrom.Text.ToString()); }

                       if (IsValidDate(mskHospitaliztionTo.Text))
                       { claim.HospitalizationToDOS = Convert.ToDateTime(mskHospitaliztionTo.Text.ToString()); }

                       if (cmbPatientAgeYears.Text.Trim() != "")
                       {
                           claim.AgeYears = Convert.ToInt32(cmbPatientAgeYears.Text.Trim());
                       }
                       if (cmbPatientAgeMonth.Text.Trim() != "")
                       {
                           claim.AgeMonths = Convert.ToInt32(cmbPatientAgeMonth.Text.Trim());
                       }
                       if (cmbPatientAgeDays.Text.Trim() != "")
                       {
                           claim.AgeDays = Convert.ToInt32(cmbPatientAgeDays.Text.Trim());
                       }

                       if (cmbGender.Text.Trim() == "Male") { claim.PatientGender = enumGender.Male.ToString(); }
                       else if (cmbGender.Text.Trim() == "Female") { claim.PatientGender = enumGender.Female.ToString(); }
                       else { claim.PatientGender = enumGender.Other.ToString(); }

                       claim.PriorAuthorization = sPriorAuthorization;
                       claims.Add(claim);

                       claim = null;
                   }
               }

           }
           catch (Exception ex)
           {
               MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           finally
           {
               if (lstInsurance != null)
               {
                   lstInsurance.Clear();
                   lstInsurance = null;
               }
           }

            return claims;
        }
        public static Int32 DateAsNumber(string datevalue)
        {
            Int32 _result = 0;
            DateTime _internaldate = Convert.ToDateTime(datevalue);
            datevalue = string.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy");
            try
            {
                if (datevalue.Length == 10)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(6, 4) + datevalue.Substring(0, 2) + datevalue.Substring(3, 2);
                    _result = Convert.ToInt32(_internalresult);
                }
                else if (datevalue.Length == 9)
                {
                    string _internalresult = "";
                    if (_internaldate.Month <= 9) // 1/11/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + "0" + datevalue.Substring(0, 1) + datevalue.Substring(2, 2);
                    }
                    else if (_internaldate.Day <= 9) // 11/2/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + datevalue.Substring(0, 2) + "0" + datevalue.Substring(3, 1);
                    }


                    _result = Convert.ToInt32(_internalresult);
                }
                else if (datevalue.Length == 8)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(4, 4) + "0" + datevalue.Substring(0, 1) + "0" + datevalue.Substring(2, 1);
                    _result = Convert.ToInt32(_internalresult);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }
        public static DateTime DateAsDate(Int64 datevalue)
        {
            DateTime _result = DateTime.Now;
            try
            {
                if (datevalue.ToString().Length == 8)
                {
                    string _internalresult = datevalue.ToString();
                    string _internaldate = "";
                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                    _result = Convert.ToDateTime(_internaldate);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }
    }
}
