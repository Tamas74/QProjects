using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using C1.Win.C1FlexGrid;
using glc = gloListControl;

namespace ChargeRules
{
    public partial class frmPracticeRuleEditor : Form 
    {

       #region "PrivateVariables"
        
            private string _databaseconnectionstring = "";
            private string _messageBoxCaption = "gloPM";
            private Int64 _ClinicID = 0;           
            private string _username = "";
            private Int64 _UserID = 0;
            DataTable dtProperties = null;
            private Int64 nRuleID = 0;
            private Int64 _nPracticeID = 0;
            private DataTable _dtPracticesList = null;
            private string _sAUSID = "";
            private DataSet _dsMasters = null;
            private Int64 _nCategoryID = 0;
            private string _sState = "";
            private string _sTaxonomyCodes = "";
            private string _sRuleCode=string.Empty;
            private int COL_PropertyName = 0;
            private  int COL_LineNo=1;
            private int COL_Remove = 2;
            private int COL_Group = 3;
            private int COL_AndOr = 4;
            private int COL_Field = 5;
            private int COL_Operator = 6;
            private int COL_Value = 7;
            private int COL_Browse = 8;
            private int COL_RuleDetailID = 9;
            private int COL_PropertyID = 10;
            private int COL_ParentID = 11;
            private int COL_ValueID = 12;
            private int COL_InternalPredicate = 13;
            Hashtable htRuleGroups = new Hashtable();
            DataTable dtRuleGroups = null;
            private string sQCommunicatorServiceURL = string.Empty;

            public string QCommunicatorServiceURL
            {
                get { return sQCommunicatorServiceURL; }
                set { sQCommunicatorServiceURL = value; }
            }
            public Boolean IsCopy { get; set; }

            public Dictionary<string, string> PropertyDictionary;

            public Int64 RuleID
            {
                get { return nRuleID; }
                set { nRuleID = value; }
            }
            public Int64 nPracticeID
            {
                get { return _nPracticeID; }
                set { _nPracticeID = value; }
            }
            public DataTable dtPracticesList
            {
                get { return _dtPracticesList; }
                set { _dtPracticesList = value; }
            }
            public string sAUSID
            {
                get { return _sAUSID; }
                set { _sAUSID = value; }
            }

            public DataSet dsMasters
            {
                get { return _dsMasters; }
                set { _dsMasters = value; }
            }
            public Int64 nCategoryID
            {
                get { return _nCategoryID; }
                set { _nCategoryID = value; }
            }
            public string sState
            {
                get { return _sState; }
                set { _sState = value; }
            }
            public string sTaxonomyCodes
            {
                get { return _sTaxonomyCodes; }
                set { _sTaxonomyCodes = value; }
            }
            public string sRuleCode
            {
                get { return _sRuleCode; }
                set { _sRuleCode = value; }
            }
        # endregion

       #region "Public Enum"
            public enum EvaluationLogic
            {
                And = 1,
                Or = 2,
                Custom = 3
            }

            public enum RuleType
            {
                Error = 1,
                Warning = 2,
                Information = 3
            }
            
       #endregion
    
       #region "Constructor"

            public frmPracticeRuleEditor(Int64 RuleID)
            {
                InitializeComponent();

                SetWindowAsPerScreenResolution();

                this.nRuleID = RuleID;
                this._databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                this._ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                this._messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
                this._UserID = gloGlobal.gloPMGlobal.UserID;
                this._username = gloGlobal.gloPMGlobal.UserName;
            }

       #endregion

       #region "Form Load Event"

            private void frmPracticeRuleEditor_Load(object sender, EventArgs e)
            {
               
                ClsRuleEngine objClasRuleEngine = new ClsRuleEngine();
                try
                {

                    dtRuleGroups = new DataTable();
                    DataColumn dCol1 = new DataColumn("GroupId");
                    DataColumn dCol2 = new DataColumn("GroupStart");
                    DataColumn dCol3 = new DataColumn("GroupEnd");
                    DataColumn dCol4 = new DataColumn("GroupLevel");
                    dCol4.DataType = Type.GetType("System.Int16");
                    dtRuleGroups.Columns.Add(dCol1);
                    dtRuleGroups.Columns.Add(dCol2);
                    dtRuleGroups.Columns.Add(dCol3);
                    dtRuleGroups.Columns.Add(dCol4);
                    

                    DataTable _dtEvaluation = new DataTable();
                    
                    _dtEvaluation.Columns.Add("Selection");
                    _dtEvaluation.Rows.Add();
                    _dtEvaluation.Rows[_dtEvaluation.Rows.Count - 1]["Selection"] = "And";
                    _dtEvaluation.Rows.Add();
                    _dtEvaluation.Rows[_dtEvaluation.Rows.Count - 1]["Selection"] = "Or";
                    _dtEvaluation.AcceptChanges();


                    dtProperties = objClasRuleEngine.GetOperators();
                    ComboBox AndOr = new ComboBox();
                    AndOr.DropDownStyle = ComboBoxStyle.DropDownList;
                    AndOr.DataSource = _dtEvaluation;
                    AndOr.DisplayMember = "Selection";
                    c1RuleGrid.Cols[COL_AndOr].Editor = AndOr;

                    PropertyDictionary = dtProperties.AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<string>("sPropertyName"), row => row.Field<string>("sPropertyDisplayName"));
                   
                    CommonLoad();
                    DesignGrid();
                    AddCellStyleForGroups();
                    // c1RuleGrid.SetCellStyle(c1RuleGrid.Rows.Count - 1, COL_Operator, cslist);
                    if (nRuleID > 0)
                    {

                        setRuleData();
                        ExpressionEvaluation();
                       

                    }
                    if (IsCopy)
                    {
                        this.nRuleID = 0;
                    }

                    rdbAnd.Checked = true;
                    txtRuleExpression.ReadOnly = true;

                    if (dtPracticesList != null && dtPracticesList.Rows.Count>0)
                    {
                        cmbPracticeList.DataSource = dtPracticesList;
                        cmbPracticeList.DisplayMember = "sPracticeName";
                        cmbPracticeList.ValueMember = "nPracticeID";
                    }
                    if (nPracticeID != 0)
                    {
                        DataTable dt = null;
                        dt = (DataTable)cmbPracticeList.DataSource;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count ; i++)
                            {
                                if (Convert.ToInt64(dt.Rows[i]["nPracticeID"]) == nPracticeID)
                                {
                                    cmbPracticeList.SelectedIndex = i;
                                }
                            }
                        }
                    }
                    setCategoryStateTaxomony();
                    txtRuleCode.Text = sRuleCode;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                   
                    if (objClasRuleEngine != null)
                    {
                        objClasRuleEngine.Dispose();
                        objClasRuleEngine = null;
                    }
                }
            }

            private void setCategoryStateTaxomony()
            {
                DataTable dtCategory = null;
                DataTable dtState = null;
                DataTable dtTaxonomy = null;
                try
                {

                    if (dsMasters != null && dsMasters.Tables.Count > 0)
                    {
                        dtState = dsMasters.Tables[0];
                        dtCategory = dsMasters.Tables[1];
                        dtTaxonomy = dsMasters.Tables[2];
                        if (dtCategory != null && dtCategory.Rows.Count > 0)
                        {
                            DataRow dr = dtCategory.NewRow();
                            dr["nCategoryID"] = 0;
                            dr["sCategoryName"] = "Select";
                            dtCategory.Rows.InsertAt(dr, 0);
                            dtCategory.AcceptChanges();

                            cmbEditsCategory.DataSource = dtCategory;
                            cmbEditsCategory.ValueMember = "nCategoryID";
                            cmbEditsCategory.DisplayMember = "sCategoryName";

                        }
                        if (nCategoryID != 0)
                        {
                            cmbEditsCategory.SelectedValue = nCategoryID;
                        }

                        if (dtState != null && dtState.Rows.Count > 0)
                        {
                            DataRow dr = dtState.NewRow();
                           
                            dr["StateName"] = "ALL";
                            dtState.Rows.InsertAt(dr, 0);
                            dtState.AcceptChanges();

                            cmbCoveringState.DataSource = dtState;
                            cmbCoveringState.ValueMember = "StateName";
                            cmbCoveringState.DisplayMember = "StateName";
                            //cmbCoveringState.SelectedValue = sState;
                        }
                        if (sState != "")
                        {
                            cmbCoveringState.SelectedValue = sState;
                        }

                        if (dtTaxonomy!=null&&dtTaxonomy.Rows.Count>0 && sTaxonomyCodes=="")
                        {
                            fillSpeciality();
                            //cmbCoveringSpeciality.DataSource = null;
                            //cmbCoveringSpeciality.DisplayMember = "sTaxonomyCode";
                            //cmbCoveringSpeciality.ValueMember = "nSpecialtyID";
                        }
                        if (sTaxonomyCodes != "")
                        {
                            //dtTaxonomy.Select("nTaxonomyID in (" + sTaxonomyCodes + ")").CopyToDataTable();
                            DataTable dtnew = new DataTable();
                            dtnew = dtTaxonomy.Select("nSpecialtyID in (" + sTaxonomyCodes + ")").CopyToDataTable();
                            cmbCoveringSpeciality.DataSource = dtnew;
                            cmbCoveringSpeciality.DisplayMember = "sTaxonomyCode";
                            cmbCoveringSpeciality.ValueMember = "nSpecialtyID";
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                }
            }
       #endregion

       #region "Form Events"

            private bool SaveRule()
            {
                ClsRuleEngine oclsRuleengine = null;
                int nEvaluationLogic = 0;
                int nRuleType = 0;
                tlsClaimRule.Select();
                try
                {
                    if (txtName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter rule name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                        return false;
                    }
                    if (txtErrorMessage.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter error message.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtErrorMessage.Focus();
                        return false;
                    }
                    if (CheckforRuleExist(txtName.Text.Trim()))
                    {
                        MessageBox.Show("Rule name already Exist.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                        return false;
                    }
                    if (Convert.ToInt64(cmbEditsCategory.SelectedValue)==0)
                    {
                        MessageBox.Show("Select rule edits category.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbEditsCategory.Focus();
                        return false;
                    }
                    if (Convert.ToString(cmbCoveringState.Text)=="")
                    {
                        MessageBox.Show("Select covering state.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCoveringState.Focus();
                        return false;
                    }
                    if (cmbCoveringSpeciality.Text=="")
                    {
                        MessageBox.Show("Select atleast one covering speciality.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCoveringState.Focus();
                        return false;
                    }

                    if (cmbPracticeList.SelectedIndex!=null)
                    {
                        _nPracticeID = Convert.ToInt64(cmbPracticeList.SelectedValue);
                    }
                    if (cmbEditsCategory.SelectedIndex!=0)
                    {
                        _nCategoryID = Convert.ToInt64(cmbEditsCategory.SelectedValue);
                    }
                    if (Convert.ToString(cmbCoveringState.Text) != "")
                    {
                        _sState = Convert.ToString(cmbCoveringState.Text);
                    }
                    if (cmbCoveringSpeciality.Text!="")
                    {
                        string sTaxonomyID = string.Empty;
                        if (cmbCoveringSpeciality.Items.Count > 0)
                        {
                            foreach (var item in cmbCoveringSpeciality.Items)
                            {
                                string sItemTaxonomyID=((System.Data.DataRowView)(item)).Row.ItemArray[0].ToString();
                                if (sTaxonomyID == "")
                                {
                                    sTaxonomyID = sItemTaxonomyID;
                                }
                                else
                                {
                                    sTaxonomyID = sTaxonomyID + "," + sItemTaxonomyID;
                                }
                            }
                        }
                        else
                        {
                            sTaxonomyID = "0";
                        }
                        _sTaxonomyCodes = sTaxonomyID;
                    }
                    sRuleCode = txtRuleCode.Text.Trim();
                    if (c1RuleGrid.Rows.Count > 1)
                    {

                        for (int nrow = 1; nrow < c1RuleGrid.Rows.Count; nrow++)
                        {
                            if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Field]).Trim() == "Patient Age Years")
                            {
                                if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Value]).Trim() == "")
                                {
                                    MessageBox.Show("Select value for Patient Age Years to evaluate rule condition.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1RuleGrid.Select(nrow, COL_Value);
                                    return false;
                                }
                            }
                            if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Field]).Trim() == "Patient Age Months")
                            {
                                if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Value]).Trim() == "")
                                {
                                    MessageBox.Show("Select value for Patient Age Months to evaluate rule condition.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1RuleGrid.Select(nrow, COL_Value);
                                    return false;
                                }

                            }
                            if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Field]).Trim() == "Patient Age Days")
                            {
                                if (Convert.ToString(c1RuleGrid.Rows[nrow][COL_Value]).Trim() == "")
                                {
                                    MessageBox.Show("Select value for Patient Age Days to evaluate rule condition.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1RuleGrid.Select(nrow, COL_Value);
                                    return false;
                                }

                            }
                        }

                        oclsRuleengine = new ClsRuleEngine();

                        int drblankOperators = 0;
                        drblankOperators = c1RuleGrid.FindRow(string.Empty, 1, COL_Operator, true);
                        int drblankOperator = c1RuleGrid.FindRow(null, 1, COL_Operator, true);//

                        if (drblankOperators > 0 || drblankOperator > 0)//operator caolumn can not be blank
                        {
                            MessageBox.Show("Select operator to evaluate rule condition.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (drblankOperators > 0)
                            {
                                c1RuleGrid.Select(drblankOperators, COL_Operator);
                            }
                            if (drblankOperator > 0)
                            {
                                c1RuleGrid.Select(drblankOperator, COL_Operator);
                            }
                            return false;
                        }
                        if (rdbAnd.Checked)
                        {
                            nEvaluationLogic = EvaluationLogic.And.GetHashCode();
                        }
                        else if (rdbOr.Checked)
                        {
                            nEvaluationLogic = EvaluationLogic.Or.GetHashCode();
                        }
                        else
                        {
                            nEvaluationLogic = EvaluationLogic.Custom.GetHashCode();
                        }
                        if (rdbError.Checked)
                        {
                            nRuleType = RuleType.Error.GetHashCode();
                        }
                        else if (rdbWarning.Checked)
                        {
                            nRuleType = RuleType.Warning.GetHashCode();
                        }
                        else
                        {
                            nRuleType = RuleType.Information.GetHashCode();
                        }
                        nRuleID = oclsRuleengine.SaveRuleMasterData(nRuleID, txtName.Text.Trim(), txtDescription.Text.Trim(), nEvaluationLogic, txtRuleExpression.Text.Trim(), txtErrorMessage.Text.Trim(), nRuleType);
                        if (nRuleID > 0)
                        {
                            DataTable dtRuleConditions = new DataTable();
                            dtRuleConditions.Columns.Add("nRuleID");
                            dtRuleConditions.Columns.Add("nRuleDetailID");
                            dtRuleConditions.Columns.Add("nConditionIndex");
                            dtRuleConditions.Columns.Add("sPredicate");
                            dtRuleConditions.Columns.Add("sPropertyDisplayName");
                            dtRuleConditions.Columns.Add("sOperatorDisplayText");
                            dtRuleConditions.Columns.Add("sValue");
                            dtRuleConditions.Columns.Add("nValueId");

                            for (int nRuleCondition = 1; nRuleCondition < c1RuleGrid.Rows.Count; nRuleCondition++)
                            {
                                if (c1RuleGrid.GetCellStyle(nRuleCondition, COL_Value) != null && ((c1RuleGrid.GetCellStyle(nRuleCondition, COL_Value)).Editor).GetType().Name == "ComboBox")
                                {
                                    DataTable dtValue = (DataTable)((ComboBox)(c1RuleGrid.GetCellStyle(nRuleCondition, COL_Value)).Editor).DataSource;
                                   
                                    DataView dv = dtValue.DefaultView;
                                    dv.Sort = "Code";
                                    dtValue = dv.ToTable();
                                    dv.Dispose();
                                    dv = null;

                                    if (dtValue != null && dtValue.Rows.Count >0 )
                                    {
                                        for (int nRuleConditionValue = 1; nRuleConditionValue <= dtValue.Rows.Count; nRuleConditionValue++)
                                        {
                                            dtRuleConditions.Rows.Add();
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleID"] = nRuleID;
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleDetailID"] = Convert.ToInt64(c1RuleGrid.Rows[nRuleCondition][COL_RuleDetailID]);
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nConditionIndex"] = Convert.ToInt16(c1RuleGrid.Rows[nRuleCondition][COL_LineNo]);
                                            if (nRuleConditionValue == 1)
                                            {
                                                if (nRuleCondition == 1)
                                                {
                                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = "";
                                                }
                                                else
                                                {
                                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_AndOr]).Trim();
                                                }
                                            }
                                            else
                                            {
                                                dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition]["InternalPredicate"]).Trim();
                                            }
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPropertyDisplayName"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Field]).Trim();
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sOperatorDisplayText"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Operator]).Trim();
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sValue"] = Convert.ToString(dtValue.Rows[nRuleConditionValue - 1]["Code"]).Trim();
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nValueId"] = Convert.ToString(dtValue.Rows[nRuleConditionValue - 1]["Id"]).Trim();
                                        }
                                    }
                                    else
                                    {
                                        dtRuleConditions.Rows.Add();
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleID"] = nRuleID;
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleDetailID"] = Convert.ToInt64(c1RuleGrid.Rows[nRuleCondition][COL_RuleDetailID]);
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nConditionIndex"] = Convert.ToInt16(c1RuleGrid.Rows[nRuleCondition][COL_LineNo]);
                                        if (nRuleCondition == 1)
                                        {
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = "";
                                        }
                                        else
                                        {
                                            dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_AndOr]).Trim();
                                        }
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPropertyDisplayName"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Field]).Trim();
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sOperatorDisplayText"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Operator]).Trim();
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sValue"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Value]).Trim();
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nValueId"] = Convert.ToInt64(c1RuleGrid.Rows[nRuleCondition][COL_ValueID]);
                                    }
                                    //dtValue.Dispose();
                                    //dtValue = null;
                                }
                                else
                                {
                                    dtRuleConditions.Rows.Add();
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleID"] = nRuleID;
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nRuleDetailID"] = Convert.ToInt64(c1RuleGrid.Rows[nRuleCondition][COL_RuleDetailID]);
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nConditionIndex"] = Convert.ToInt16(c1RuleGrid.Rows[nRuleCondition][COL_LineNo]);
                                    if (nRuleCondition == 1)
                                    {
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = "";
                                    }
                                    else
                                    {
                                        dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPredicate"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_AndOr]).Trim();
                                    }
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sPropertyDisplayName"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Field]).Trim();
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sOperatorDisplayText"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Operator]).Trim();
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["sValue"] = Convert.ToString(c1RuleGrid.Rows[nRuleCondition][COL_Value]).Trim();
                                    dtRuleConditions.Rows[dtRuleConditions.Rows.Count - 1]["nValueId"] = Convert.ToInt64(c1RuleGrid.Rows[nRuleCondition][COL_ValueID]);
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            oclsRuleengine.SaveRuleDetailsData(dtRuleConditions);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetup, gloAuditTrail.ActivityType.Save, "Rule data saved", 0, nRuleID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                         //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Enter at least one criteria to save rule.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    if (oclsRuleengine != null)
                    {
                        oclsRuleengine.Dispose();
                        oclsRuleengine = null;
                    }
                }
            }

        private void tlsDM_Close_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                if (SaveRule())
                {
                    this.Close();
                }
            }
            else if (res == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                this.Close();
            }
        }
        
        private void tlsDM_Save_Click(object sender, EventArgs e)
        {
            SaveRule();
        }


        private void trvProperties_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node != null && e.Node.Nodes.Count == 0)
                {
                    AddRuleCondition(((mynode)e.Node));
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetup, gloAuditTrail.ActivityType.Ruleconditionadded, "Rule Condition added", 0, nRuleID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void c1RuleGrid_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_AndOr)
            {
                BuildRuleExpression();
            }

            //Below  for internal control
            if (c1RuleGrid.GetData(c1RuleGrid.RowSel, COL_Value) != null)
            {
                if (Convert.ToString(c1RuleGrid.GetData(c1RuleGrid.RowSel, COL_Value)) == "")
                {
                    c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_Value, "");
                }
            }
        }

        private void c1RuleGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Int32 nCol = c1RuleGrid.HitTest(e.X, e.Y).Column;
            Int32 nRow = c1RuleGrid.HitTest(e.X, e.Y).Row;
            string[] CurrentColName;
            int currentGroupLevel;

            try
            {
                if (e.Button.ToString() != "Right")
                {
                  //  c1RuleGrid.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
                    
                    if (nCol > -1 && nRow > -1)
                    {
                        if (c1RuleGrid.Cols[nCol].Name.Contains("Level ") == true && c1RuleGrid.GetCellImage(nRow, nCol) != null)
                        {
                            CurrentColName = c1RuleGrid.Cols[nCol].Name.Split(' ');
                            if (CurrentColName.Length > 1)
                            {
                                currentGroupLevel = Convert.ToInt32(CurrentColName[1]);
                                if (MessageBox.Show("Are you sure you want to delete this group clause ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    DeleteGroup(nCol, nRow, currentGroupLevel);
                                    Int64 nrulecondtionId = Convert.ToInt64(c1RuleGrid.GetData(nRow, COL_RuleDetailID));
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetup, gloAuditTrail.ActivityType.Groupclausedeleted, "Group clause deleted", 0, nRuleID, nrulecondtionId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                                }
                                c1RuleGrid.Select(1, COL_LineNo);
                            }
                        }


                        else
                        {
                            if (nCol == COL_Group && nRow == 0)
                            {
                                CreateGroup();

                                Int64 nrulecondtionId = Convert.ToInt64(c1RuleGrid.GetData(nRow, COL_RuleDetailID));
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetup, gloAuditTrail.ActivityType.Groupclauseadded, "Group clause added", 0, nRuleID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                            }
                            else if (nCol == COL_Browse && c1RuleGrid.GetCellImage(nRow, nCol) != null)
                            {
                                string sFieldName = (string)c1RuleGrid.GetData(nRow, COL_Field);
                                int pnlwidth = c1RuleGrid.Cols[c1RuleGrid.ColSel].Width + c1RuleGrid.Cols[COL_Value].Width;
                                string sInternalPredicate = Convert.ToString(c1RuleGrid.GetData(nRow, "InternalPredicate"));
                                OpenListControl(gloGridListControlType.COL_Value, sFieldName, false, nRow, nCol, "", pnlwidth, sInternalPredicate);
                                sFieldName = string.Empty;
                            }
                            else if (nCol == COL_Remove)
                            {
                                if (nRow > 0)
                                {
                                    if (MessageBox.Show("Are you sure you want to delete this rule condition ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        //Bug #96711: gloPM>>Application showing exception on Claim rule editor
                                        //if (ogloGridListControl != null)
                                        //{
                                        //    CloseInternalControl();
                                        //    ClosePracticeInternalControl();
                                        //}
                                        if (ogloPracticeGridListControl != null)
                                        {
                                            CloseInternalControl();
                                            ClosePracticeInternalControl();
                                        }
                                        Int64 nrulecondtionId = Convert.ToInt64(c1RuleGrid.GetData(nRow, COL_RuleDetailID));
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetup, gloAuditTrail.ActivityType.Ruleconditiondeleted, "Rule condition deleted", 0, nRuleID, nrulecondtionId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        DeleteRuleCondition(nRow);

                                    }
                                }
                            }
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CurrentColName = null;                
            }
        }

       #endregion

        #region "Private Methods"

        private void AddCellStyleForGroups()
        {
            C1.Win.C1FlexGrid.CellStyle csCustom;
            C1.Win.C1FlexGrid.CellStyle csNormal;
            C1.Win.C1FlexGrid.CellStyle csAlternate;
            C1.Win.C1FlexGrid.CellStyle cs1;
            C1.Win.C1FlexGrid.CellStyle cs2;
            C1.Win.C1FlexGrid.CellStyle cs3;
            C1.Win.C1FlexGrid.CellStyle cs4;
            C1.Win.C1FlexGrid.CellStyle cs5;
            C1.Win.C1FlexGrid.CellStyle cs6;
            C1.Win.C1FlexGrid.CellStyle cs7;
            C1.Win.C1FlexGrid.CellStyle cs8;
            C1.Win.C1FlexGrid.CellStyle cs9;
            C1.Win.C1FlexGrid.CellStyle cs10;
            C1.Win.C1FlexGrid.CellStyle cs11;
            C1.Win.C1FlexGrid.CellStyle cs12;
            C1.Win.C1FlexGrid.CellStyle cs13;
            C1.Win.C1FlexGrid.CellStyle cs14;
            C1.Win.C1FlexGrid.CellStyle cs15;
            C1.Win.C1FlexGrid.CellStyle cs16;
            C1.Win.C1FlexGrid.CellStyle cs17;
            C1.Win.C1FlexGrid.CellStyle cs18;
            C1.Win.C1FlexGrid.CellStyle cs19;
            C1.Win.C1FlexGrid.CellStyle cs20;
            try
            {

                if (c1RuleGrid.Styles.Contains("csCustom"))
                {
                    csCustom = c1RuleGrid.Styles["csCustom"];
                }
                else
                {
                    csCustom = c1RuleGrid.Styles.Add("csCustom");
                    csCustom.BackColor = Color.White;
                    csCustom.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None;
                }

                if (c1RuleGrid.Styles.Contains("Normal"))
                {
                    csNormal = c1RuleGrid.Styles["Normal"];
                }
                else
                {
                    csNormal = c1RuleGrid.Styles.Add("Normal");
                    csNormal.BackColor = Color.FromArgb(240, 247, 255); // Color.FromName("#90ee90");
                }

                if (c1RuleGrid.Styles.Contains("Alternate"))
                {
                    csAlternate = c1RuleGrid.Styles["Alternate"];
                }
                else
                {
                    csAlternate = c1RuleGrid.Styles.Add("Alternate");
                    csAlternate.BackColor = Color.FromArgb(222, 231, 250); // Color.FromName("#90ee90");
                }

                if (c1RuleGrid.Styles.Contains("cs1"))
                {
                    cs1 = c1RuleGrid.Styles["cs1"];
                }
                else
                {
                    cs1 = c1RuleGrid.Styles.Add("cs1");
                    cs1.BackColor = Color.FromArgb(255, 149, 0); // Color.FromName("#90ee90");
                    cs1.Border.Color = Color.FromArgb(255, 149, 0);
                    cs1.Border.Direction = BorderDirEnum.Horizontal;

                }

                if (c1RuleGrid.Styles.Contains("cs2"))
                {
                    cs2 = c1RuleGrid.Styles["cs2"];
                }
                else
                {
                    cs2 = c1RuleGrid.Styles.Add("cs2");
                    cs2.BackColor = Color.FromArgb(174, 136, 119);//Color.FromName("#ffa07a");
                    cs2.Border.Color = Color.FromArgb(174, 136, 119);
                }

                if (c1RuleGrid.Styles.Contains("cs3"))
                {
                    cs3 = c1RuleGrid.Styles["cs3"];
                }
                else
                {
                    cs3 = c1RuleGrid.Styles.Add("cs3");
                    cs3.BackColor = Color.FromArgb(126, 56, 121);// Color.FromName("#dda0dd");
                    cs3.Border.Color = Color.FromArgb(126, 56, 121);
                }

                if (c1RuleGrid.Styles.Contains("cs4"))
                {
                    cs4 = c1RuleGrid.Styles["cs4"];
                }
                else
                {
                    cs4 = c1RuleGrid.Styles.Add("cs4");
                    cs4.BackColor = Color.FromArgb(77, 130, 184);//Color.FromName("#ffc0cb"); ;
                    cs4.Border.Color = Color.FromArgb(77, 130, 184);
                }

                if (c1RuleGrid.Styles.Contains("cs5"))
                {
                    cs5 = c1RuleGrid.Styles["cs5"];
                }
                else
                {
                    cs5 = c1RuleGrid.Styles.Add("cs5");
                    cs5.BackColor = Color.FromArgb(255, 204, 0);
                    cs5.Border.Color = Color.FromArgb(255, 204, 0);
                }


                if (c1RuleGrid.Styles.Contains("cs6"))
                {
                    cs6 = c1RuleGrid.Styles["cs6"];
                }
                else
                {
                    cs6 = c1RuleGrid.Styles.Add("cs6");
                    cs6.BackColor = Color.FromArgb(255, 45, 85);
                    cs6.Border.Color = Color.FromArgb(255, 45, 85);
                }
                if (c1RuleGrid.Styles.Contains("cs7"))
                {
                    cs7 = c1RuleGrid.Styles["cs7"];
                }
                else
                {
                    cs7 = c1RuleGrid.Styles.Add("cs7");
                    cs7.BackColor = Color.FromArgb(0, 222, 255);
                    cs7.Border.Color = Color.FromArgb(0, 222, 255);
                }

                if (c1RuleGrid.Styles.Contains("cs8"))
                {
                    cs8 = c1RuleGrid.Styles["cs8"];
                }
                else
                {
                    cs8 = c1RuleGrid.Styles.Add("cs8");
                    cs8.BackColor = Color.FromArgb(142, 142, 147);
                    cs8.Border.Color = Color.FromArgb(142, 142, 147);
                }
                if (c1RuleGrid.Styles.Contains("cs9"))
                {
                    cs9 = c1RuleGrid.Styles["cs1"];
                }
                else
                {
                    cs9 = c1RuleGrid.Styles.Add("cs9");
                    cs9.BackColor = Color.FromArgb(76, 217, 100);
                    cs9.Border.Color = Color.FromArgb(76, 217, 100);

                }
                if (c1RuleGrid.Styles.Contains("cs10"))
                {
                    cs10 = c1RuleGrid.Styles["cs10"];
                }
                else
                {
                    cs10 = c1RuleGrid.Styles.Add("cs10");
                    cs10.BackColor = Color.FromArgb(254, 101, 207);
                    cs10.Border.Color = Color.FromArgb(254, 101, 207);

                }

                if (c1RuleGrid.Styles.Contains("cs11"))
                {
                    cs11 = c1RuleGrid.Styles["cs11"];
                }
                else
                {
                    cs11 = c1RuleGrid.Styles.Add("cs11");
                    cs11.BackColor = Color.FromArgb(189, 189, 189);
                    cs11.Border.Color = Color.FromArgb(189, 189, 189);

                }

                if (c1RuleGrid.Styles.Contains("cs12"))
                {
                    cs12 = c1RuleGrid.Styles["cs12"];
                }
                else
                {
                    cs12 = c1RuleGrid.Styles.Add("cs12");
                    cs12.BackColor = Color.FromArgb(255, 120, 91);
                    cs12.Border.Color = Color.FromArgb(255, 120, 91);

                }
               
                if (c1RuleGrid.Styles.Contains("cs13"))
                {
                    cs13 = c1RuleGrid.Styles["cs13"];
                }
                else
                {
                    cs13 = c1RuleGrid.Styles.Add("cs13");
                    cs13.BackColor = Color.FromArgb(0, 177, 172);
                    cs13.Border.Color = Color.FromArgb(0, 177, 172);

                }
                if (c1RuleGrid.Styles.Contains("cs14"))
                {
                    cs14 = c1RuleGrid.Styles["cs14"];
                }
                else
                {
                    cs14 = c1RuleGrid.Styles.Add("cs14");
                    cs14.BackColor = Color.FromArgb(174, 165, 0);
                    cs14.Border.Color = Color.FromArgb(174, 165, 0);

                }
                if (c1RuleGrid.Styles.Contains("cs15"))
                {
                    cs15 = c1RuleGrid.Styles["cs15"];
                }
                else
                {
                    cs15 = c1RuleGrid.Styles.Add("cs15");
                    cs15.BackColor = Color.FromArgb(85, 165, 28);
                    cs15.Border.Color = Color.FromArgb(85, 165, 28);

                }

                if (c1RuleGrid.Styles.Contains("cs16"))
                {
                    cs16 = c1RuleGrid.Styles["cs16"];
                }
                else
                {
                    cs16 = c1RuleGrid.Styles.Add("cs16");
                    cs16.BackColor = Color.FromArgb(86, 144, 153);
                    cs16.Border.Color = Color.FromArgb(86, 144, 153);

                }
                if (c1RuleGrid.Styles.Contains("cs17"))
                {
                    cs17 = c1RuleGrid.Styles["cs17"];
                }
                else
                {
                    cs17 = c1RuleGrid.Styles.Add("cs17");
                    cs17.BackColor = Color.FromArgb(176, 128, 208);
                    cs17.Border.Color = Color.FromArgb(176, 128, 208);

                }
                if (c1RuleGrid.Styles.Contains("cs18"))
                {
                    cs18 = c1RuleGrid.Styles["cs18"];
                }
                else
                {
                    cs18 = c1RuleGrid.Styles.Add("cs18");
                    cs18.BackColor = Color.FromArgb(132, 53, 73);
                    cs18.Border.Color = Color.FromArgb(132, 53, 73);

                }
                if (c1RuleGrid.Styles.Contains("cs19"))
                {
                    cs19 = c1RuleGrid.Styles["cs19"];
                }
                else
                {
                    cs19 = c1RuleGrid.Styles.Add("cs19");
                    cs19.BackColor = Color.FromArgb(170, 110, 75);
                    cs19.Border.Color = Color.FromArgb(170, 110, 75);

                }
                if (c1RuleGrid.Styles.Contains("cs20"))
                {
                    cs20 = c1RuleGrid.Styles["cs20"];
                }
                else
                {
                    cs20 = c1RuleGrid.Styles.Add("cs20");
                    cs20.BackColor = Color.FromArgb(243, 235, 123);
                    cs20.Border.Color = Color.FromArgb(243, 235, 123);

                }
            }
            catch
            {
                cs1 = c1RuleGrid.Styles.Add("cs1");
                cs2 = c1RuleGrid.Styles.Add("cs2");
                cs3 = c1RuleGrid.Styles.Add("cs3");
                cs4 = c1RuleGrid.Styles.Add("cs4");
                cs5 = c1RuleGrid.Styles.Add("cs5");
                cs6 = c1RuleGrid.Styles.Add("cs6");
                cs7 = c1RuleGrid.Styles.Add("cs7");
                cs8 = c1RuleGrid.Styles.Add("cs8");
                cs9 = c1RuleGrid.Styles.Add("cs9");
                cs10 = c1RuleGrid.Styles.Add("cs10");
                cs10 = c1RuleGrid.Styles.Add("cs11");
                cs10 = c1RuleGrid.Styles.Add("cs12");
                cs10 = c1RuleGrid.Styles.Add("cs13");
                cs10 = c1RuleGrid.Styles.Add("cs14");
                cs10 = c1RuleGrid.Styles.Add("cs15");
                cs10 = c1RuleGrid.Styles.Add("cs16");
                cs10 = c1RuleGrid.Styles.Add("cs17");
                cs10 = c1RuleGrid.Styles.Add("cs18");
                cs10 = c1RuleGrid.Styles.Add("cs19");
                cs10 = c1RuleGrid.Styles.Add("cs20");
                csNormal = c1RuleGrid.Styles.Add("csNormal");
                csAlternate = c1RuleGrid.Styles.Add("csAlternate");
                csCustom = c1RuleGrid.Styles.Add("csCustom");
            }
        }

        private void DesignGrid()
        {
            try
            {
                #region "Header"

                c1RuleGrid.SetData(0, COL_Group, "[=");
                c1RuleGrid.SetData(0, COL_AndOr, "And/Or");
                c1RuleGrid.SetData(0, COL_Field, "Field");
                c1RuleGrid.SetData(0, COL_Operator, "Operator");
                c1RuleGrid.SetData(0, COL_Value, "Value");
                #endregion

                #region "Width"
                int _width = pnlRuleGrid.Width;
                c1RuleGrid.Cols[COL_LineNo].Width = Convert.ToInt32(_width * 0.02);
                c1RuleGrid.Cols[COL_Remove].Width = Convert.ToInt32(_width * 0.017);
                c1RuleGrid.Cols[COL_Group].Width = Convert.ToInt32(_width * 0.03);
                c1RuleGrid.Cols[COL_AndOr].Width = Convert.ToInt32(_width * 0.07);
                c1RuleGrid.Cols[COL_Operator].Width = Convert.ToInt32(_width * 0.18);
                c1RuleGrid.Cols[COL_Field].Width = Convert.ToInt32(_width * 0.35);
                c1RuleGrid.Cols[COL_Value].Width = Convert.ToInt32(_width * 0.29);
                c1RuleGrid.Cols[COL_Browse].Width = Convert.ToInt32(_width * 0.03);
                c1RuleGrid.Cols[COL_RuleDetailID].Width = 0;
                c1RuleGrid.Cols[COL_PropertyID].Width = 0;
                c1RuleGrid.Cols[COL_ParentID].Width = 0;
                c1RuleGrid.Cols[COL_ValueID].Width = 0;
                c1RuleGrid.Cols[COL_InternalPredicate].Width = 0;
                #endregion

                c1RuleGrid.Cols[COL_InternalPredicate].Name = "InternalPredicate";

                #region "DataType"
                c1RuleGrid.Cols[COL_Group].DataType = typeof(Boolean);
                c1RuleGrid.Cols[COL_LineNo].AllowEditing = false;
                c1RuleGrid.Cols[COL_Field].AllowEditing = false;
                c1RuleGrid.Cols[COL_Remove].AllowEditing = false;


                c1RuleGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                c1RuleGrid.Cols[COL_ValueID].DataType = typeof(Int64);
                #endregion
                c1RuleGrid.Cols[COL_Browse].DataType = typeof(Image);
                c1RuleGrid.Cols[COL_Remove].DataType = typeof(Image);
                
                c1RuleGrid.Cols[COL_Browse].AllowEditing = false; 
                
                c1RuleGrid.Cols[COL_Value].TextAlign = TextAlignEnum.LeftCenter;
                //c1RuleGrid.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbOperator_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.ComboBox)(sender)).Text.Contains("Exist"))
                {
                    if (c1RuleGrid.GetCellStyle(c1RuleGrid.RowSel, COL_Value) != null)
                    {
                        DataTable dtValue_1 = (DataTable)((ComboBox)(c1RuleGrid.GetCellStyle(c1RuleGrid.RowSel, COL_Browse - 1)).Editor).DataSource;
                        if (dtValue_1 != null && dtValue_1.Rows.Count > 1)
                        {
                            return;
                        }
                    }

                    if ((Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_PropertyID]) == "60" ||
                             Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_PropertyID]) == "57" ||
                             Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_PropertyID]) == "28") &&
                             ((System.Windows.Forms.ComboBox)(sender)).Text.Contains("Exist"))
                    {

                        c1RuleGrid.SetCellImage(c1RuleGrid.RowSel, COL_Browse, global::ChargeRules.Properties.Resources.Browse);

                        DataTable dtValue = new DataTable();
                        DataColumn dCol1 = new DataColumn();
                        dCol1.ColumnName = "Code";
                        dtValue.Columns.Add(dCol1);

                        DataColumn dCol2 = new DataColumn();
                        dCol2.ColumnName = "Id";
                        dtValue.Columns.Add(dCol2);


                        if (c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_Value] != null && c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_ValueID] != null)
                        {
                            dtValue.Rows.Add();
                            dtValue.Rows[dtValue.Rows.Count - 1]["Code"] = Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_Value]);
                            dtValue.Rows[dtValue.Rows.Count - 1]["Id"] = Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_ValueID]);


                        }
                        SetCellStyleValueColumn(dtValue, c1RuleGrid.RowSel);
                    }




                    //dCol1.Dispose();
                    //dCol1 = null;

                    //dCol2.Dispose();
                    //dCol2 = null;

                    //dtValue.Dispose();
                    //dtValue = null;
                }
                else
                {
                    if (c1RuleGrid.GetCellStyle(c1RuleGrid.RowSel, COL_Browse - 1) != null)
                    {
                        if (((c1RuleGrid.GetCellStyle(c1RuleGrid.RowSel, COL_Browse - 1)).Editor).GetType().Name == "ComboBox")
                        {

                            DataTable dtValue = (DataTable)((ComboBox)(c1RuleGrid.GetCellStyle(c1RuleGrid.RowSel, COL_Browse - 1)).Editor).DataSource;
                            if (dtValue != null)
                            {
                                if (dtValue.Rows.Count > 1 && (Convert.ToString(((System.Windows.Forms.ComboBox)(sender)).Text).Trim().Contains("Equal")))
                                {
                                    MessageBox.Show("" + ((System.Windows.Forms.ComboBox)(sender)).Text + " Operator not allowed while multiple values are selected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    ((System.Windows.Forms.ComboBox)(sender)).Text = Convert.ToString(c1RuleGrid.Rows[c1RuleGrid.RowSel][COL_Operator]).Trim();
                                }
                                else
                                {
                                    if (((System.Windows.Forms.ComboBox)(sender)).Text.Contains("Equal"))
                                    {
                                        c1RuleGrid.SetCellImage(c1RuleGrid.RowSel, COL_Browse, null);
                                        C1.Win.C1FlexGrid.CellStyle cslist = null;
                                        c1RuleGrid.SetCellStyle(c1RuleGrid.RowSel, COL_Value, cslist);
                                        if (dtValue.Rows.Count > 0)
                                        {
                                            c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_Value, Convert.ToString(dtValue.Rows[0]["Code"]));
                                            c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_ValueID, Convert.ToString(dtValue.Rows[0]["Id"]));
                                        }
                                    }
                                }
                                //dtValue.Dispose();
                                //dtValue = null;
                            }
                        }

                    }
                    else
                    {
                        c1RuleGrid.SetCellImage(c1RuleGrid.RowSel, COL_Browse, null);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetCellStyleValueColumn(DataTable dtValue, int Index)
        {
            try
            {
                if (dtValue != null)
                {
                    ComboBox cmbValue = new ComboBox();
                    cmbValue.DropDownStyle = ComboBoxStyle.DropDownList;
                    c1RuleGrid.BeginUpdate();
                    C1.Win.C1FlexGrid.CellStyle cslist;
                    Random stringRandom = new Random(1);
                    cslist = this.c1RuleGrid.Styles.Add(stringRandom.GetHashCode().ToString());
                    cslist.Editor = cmbValue;

                    cmbValue.DataSource = null;
                    cmbValue.Items.Clear();
                    cmbValue.ValueMember = "Id";
                    cmbValue.DisplayMember = "Code";
                    cmbValue.DataSource = dtValue;
                    c1RuleGrid.SetCellStyle(Index, COL_Value, cslist);
                    c1RuleGrid.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetCellStyleOperatorColumn(string sOperators, int Index)
        {
            try
            {
                if (sOperators != string.Empty)
                {
                    ComboBox cmbOperator = new ComboBox();
                    cmbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
                    c1RuleGrid.BeginUpdate();
                    C1.Win.C1FlexGrid.CellStyle cslist;
                    Random stringRandom = new Random(1);
                    cslist = this.c1RuleGrid.Styles.Add(stringRandom.GetHashCode().ToString());
                    cslist.Editor = cmbOperator;
                    cmbOperator.SelectedIndexChanged += new System.EventHandler(cmbOperator_SelectedIndexChanged);

                    string[] arr = sOperators.Split(',');
                 
                    cmbOperator.DataSource = null;
                    cmbOperator.Items.Clear();
                    cmbOperator.DataSource = arr;
                    c1RuleGrid.SetCellStyle(Index, COL_Operator, cslist);
                    c1RuleGrid.EndUpdate();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CommonLoad()
        {
            PopulateTree();
        }

        private void PopulateTree()
        {
            mynode parentNode = null;
            mynode childNode = null;
            Font parentNodeFontStyle = null;
            try
            {
                trvProperties.BeginUpdate();

                if (dtProperties != null)
                {
                    parentNodeFontStyle = new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Bold);

                    foreach (DataRow dataRow in dtProperties.Select("nParentId IS NULL"))
                    {
                        parentNode = new mynode();
                        parentNode.NodeFont = parentNodeFontStyle;
                        parentNode.Text = Convert.ToString(dataRow["sPropertyName"]);
                        parentNode.Tag = Convert.ToString(dataRow["sPropertyDisplayName"]);

                        foreach (DataRow drChildRow in dtProperties.Select("nParentId=" + dataRow["nPropertyId"]))
                        {
                            childNode = new mynode();
                            childNode.Text = Convert.ToString(drChildRow["sPropertyName"]);
                            childNode.Tag = Convert.ToString(drChildRow["sPropertyDisplayName"]);
                            childNode.sOperators = Convert.ToString(drChildRow["Operator"]);
                            parentNode.Nodes.Add(childNode);
                            childNode = null;
                        }

                        trvProperties.Nodes.Add(parentNode);
                        parentNode = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                trvProperties.ExpandAll();
                trvProperties.EndUpdate();
                //if (parentNodeFontStyle != null) { parentNodeFontStyle.Dispose(); parentNodeFontStyle = null; }
                childNode = null;
                parentNode = null;
            }
        }


        private void BuildRuleExpression()
        {
            String CodeExpression = "";
            try
            {
                for (int i = 1; i < c1RuleGrid.Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        CodeExpression = Convert.ToString(c1RuleGrid.Rows[i][COL_LineNo]);
                    }
                    else
                    {
                        CodeExpression = CodeExpression + " " + Convert.ToString(c1RuleGrid.Rows[i][COL_AndOr]) + " " + Convert.ToString(c1RuleGrid.Rows[i][COL_LineNo]);
                    }
                }
                if (dtRuleGroups.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRuleGroups.Rows.Count; j++)
                    {
                        int tGroupStart = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupStart"]);
                        int tGroupEnd = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupEnd"]);
                        int ind = 0;

                        ind = CodeExpression.IndexOf(tGroupStart.ToString());
                        var aStringBuilder1 = new StringBuilder(CodeExpression);
                        aStringBuilder1.Remove(ind, tGroupStart.ToString().Length);
                        aStringBuilder1.Insert(ind, "(" + tGroupStart.ToString());
                        CodeExpression = aStringBuilder1.ToString();

                        ind = CodeExpression.IndexOf(tGroupEnd.ToString());
                        var aStringBuilder2 = new StringBuilder(CodeExpression);
                        aStringBuilder2.Remove(ind, tGroupEnd.ToString().Length);
                        aStringBuilder2.Insert(ind, tGroupEnd.ToString() + ")");
                        CodeExpression = aStringBuilder2.ToString();

                    }
                }
                txtRuleExpression.Text = CodeExpression.Trim();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRuleCondition(mynode selectednode)
        {
            int selectedPropertyParentId = 0;
            int selectedPropertyId = 0;

            int PropertyParentIdOnGrid = 0;
            int PropertyIdOnGrid = 0;

            // int ParentCollectionStart = 0;
            //int ParentCollectionEnd = 0;

            int RuleInsertAt = 0;

            try
            {
                if (selectednode != null)
                {

                    if (dtProperties != null && dtProperties.Rows.Count > 0)
                    {
                        // Get selected Property Id and Parent Id 
                        DataRow drPropertyDetails = (from myRow in dtProperties.AsEnumerable()
                                                     where myRow.Field<string>("sPropertyDisplayName") == selectednode.Tag.ToString()
                                                     select myRow).FirstOrDefault();
                        if (Convert.ToString(drPropertyDetails["nParentId"]) != "")
                            selectedPropertyParentId = Convert.ToInt32(drPropertyDetails["nParentId"]);

                        selectedPropertyId = Convert.ToInt32(drPropertyDetails["nPropertyId"]);

                        //Searchng Property in the rule grid
                        PropertyParentIdOnGrid = c1RuleGrid.FindRow(Convert.ToString(selectedPropertyParentId), 0, COL_ParentID, false);
                        PropertyIdOnGrid = c1RuleGrid.FindRow(Convert.ToString(selectedPropertyId), 0, COL_PropertyID, false);

                        //ArrayList ParentRows = new ArrayList();

                        if (PropertyParentIdOnGrid > 0)
                        {
                            //ParentRows.Add(PropertyParentIdOnGrid);
                            for (int i = PropertyParentIdOnGrid + 1; i <= c1RuleGrid.Rows.Count - 1; i++)
                            {
                                ////Filling Properties from same parent group in the list
                                //if (Convert.ToString(c1RuleGrid.Rows[i][COL_ParentID]) != "")
                                //{
                                //    if (Convert.ToInt32(c1RuleGrid.Rows[i][COL_ParentID]) == Convert.ToInt32(selectedPropertyParentId))
                                //        ParentRows.Add(i);
                                //}
                                //Getting location of Selected Properties duplicate in Rule grid
                                if (Convert.ToInt32(c1RuleGrid.Rows[i][COL_PropertyID]) == Convert.ToInt32(selectedPropertyId))
                                    PropertyIdOnGrid = i;
                            }
                            //if (ParentRows.Count > 0)
                            //{
                            //    ParentCollectionStart = Convert.ToInt32(ParentRows[0]);
                            //    ParentCollectionEnd = Convert.ToInt32(ParentRows[ParentRows.Count - 1]);
                            //}
                        }
                        //ParentRows = null;
                    }


                    if (PropertyIdOnGrid >= 0)
                    {
                        //If same property in present in the grid then insert property next to it in the grid
                        c1RuleGrid.Rows.Insert(PropertyIdOnGrid + 1);
                        RuleInsertAt = PropertyIdOnGrid + 1;
                    }
                    else
                    {
                        //If same property from same parent group is present then add selected property at the end of parent group property.
                        //if (ParentCollectionEnd > 0)
                        //{
                        //    c1RuleGrid.Rows.Insert(ParentCollectionEnd + 1);
                        //    RuleInsertAt = ParentCollectionEnd + 1;
                        //}
                        //else
                        //{
                        //If property not present  or noa ny property with same parent grou p present then add selected property at the end of Rule grid
                        c1RuleGrid.Rows.Add();
                        RuleInsertAt = c1RuleGrid.Rows.Count - 1;
                        //}
                    }
                    c1RuleGrid.SetData(RuleInsertAt, COL_PropertyName, selectednode.Text);
                    c1RuleGrid.SetData(RuleInsertAt, COL_Field, selectednode.Tag);
                    c1RuleGrid.SetData(RuleInsertAt, COL_ParentID, Convert.ToString(selectedPropertyParentId));
                    c1RuleGrid.SetData(RuleInsertAt, COL_PropertyID, Convert.ToString(selectedPropertyId));
                    SetCellStyleOperatorColumn(selectednode.sOperators, RuleInsertAt);
                    SetDateTimePicker(Convert.ToString(selectednode.Tag), RuleInsertAt, COL_Value);
                    //c1RuleGrid.SetData(DataInsertAt, COL_LineNo, Convert.ToString(c1RuleGrid.Rows.Count - 1));
                    //if (selectednode.sOperators.Contains("Exists"))
                    //{
                    //    c1RuleGrid.SetCellImage(RuleInsertAt, COL_Browse, global::ChargeRules.Properties.Resources.Browse);
                    //}
                    c1RuleGrid.SetCellImage(RuleInsertAt, COL_Remove, global::ChargeRules.Properties.Resources.CloseTFS1);

                    //Update Line Number in grid
                    for (int i = 1; i < c1RuleGrid.Rows.Count; i++)
                    {
                        c1RuleGrid.SetData(i, COL_LineNo, i);
                    }

                    if (c1RuleGrid.Rows.Count > 2) //if first row/condition then do not add predicate (AND/OR)
                    {
                        if (rdbOr.Checked)
                        { c1RuleGrid.SetData(RuleInsertAt, COL_AndOr, "Or"); }
                        else
                        { c1RuleGrid.SetData(RuleInsertAt, COL_AndOr, "And"); }
                    }

                    //If new row inserted the Update Group Style
                    if (dtRuleGroups.Rows.Count > 0)
                    {
                        if (PropertyIdOnGrid > 0)
                            UpdateGroup(RuleInsertAt);
                    }
                    BuildRuleExpression();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                selectednode = null;
            }
        }

        private void RuleConditionInsertOnSelection(int RuleInsertAt, mynode selectednode)
        {
            int selectedPropertyParentId = 0;
            int selectedPropertyId = 0;
            try
            {
                if (selectednode != null)
                {

                    if (dtProperties != null && dtProperties.Rows.Count > 0)
                    {
                        // Get selected Property Id and Parent Id 
                        DataRow drPropertyDetails = (from myRow in dtProperties.AsEnumerable()
                                                     where myRow.Field<string>("sPropertyDisplayName") == selectednode.Tag.ToString()
                                                     select myRow).FirstOrDefault();
                        if (Convert.ToString(drPropertyDetails["nParentId"]) != "")
                            selectedPropertyParentId = Convert.ToInt32(drPropertyDetails["nParentId"]);

                        selectedPropertyId = Convert.ToInt32(drPropertyDetails["nPropertyId"]);
                    }
                }
                if (c1RuleGrid.Rows.Count > 1)
                {
                    c1RuleGrid.Rows.Insert(RuleInsertAt);
                   
                    c1RuleGrid.SetData(RuleInsertAt, COL_Field, selectednode.Tag);
                    c1RuleGrid.SetData(RuleInsertAt, COL_ParentID, Convert.ToString(selectedPropertyParentId));
                    c1RuleGrid.SetData(RuleInsertAt, COL_PropertyID, Convert.ToString(selectedPropertyId));                    
                    SetCellStyleOperatorColumn(selectednode.sOperators, RuleInsertAt);
                    SetDateTimePicker(Convert.ToString(selectednode.Tag), RuleInsertAt, COL_Value);               
                    //c1RuleGrid.SetCellImage(RuleInsertAt, COL_Browse, global::ChargeRules.Properties.Resources.Browse);
                    c1RuleGrid.SetCellImage(RuleInsertAt, COL_Remove, global::ChargeRules.Properties.Resources.CloseTFS1);

                    //Update Line Number in grid
                    for (int i = 1; i < c1RuleGrid.Rows.Count; i++)
                    {
                        c1RuleGrid.SetData(i, COL_LineNo, i);
                    }

                    if (c1RuleGrid.Rows.Count > 2 && RuleInsertAt > 1) //if first row/condition then do not add predicate (AND/OR)
                    {
                        if (rdbOr.Checked)
                        { c1RuleGrid.SetData(RuleInsertAt, COL_AndOr, "Or"); }
                        else
                        { c1RuleGrid.SetData(RuleInsertAt, COL_AndOr, "And"); }
                    }
                    if (c1RuleGrid.Rows.Count > 2 && RuleInsertAt == 1)
                    {
                        if (rdbOr.Checked)
                        { c1RuleGrid.SetData(RuleInsertAt+1, COL_AndOr, "Or"); }
                        else
                        { c1RuleGrid.SetData(RuleInsertAt+1, COL_AndOr, "And"); }
                    }

                    //If new row inserted the Update Group Style
                    if (dtRuleGroups.Rows.Count > 0)
                    {
                        if (selectedPropertyId > 0)
                            UpdateGroup(RuleInsertAt);
                    }
                    BuildRuleExpression();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                selectednode = null;
            }
        }
        
       // private void SetDateTimePicker(Int16 nPropertyID, int rowNumber, int columnNumber)
        private void SetDateTimePicker(string FieldName, int rowNumber, int columnNumber)
        {
            if (FieldName == Convert.ToString(PropertyDictionary["HospitalizationFromDOS"]) ||
                FieldName == Convert.ToString(PropertyDictionary["HospitalizationToDOS"]) ||
                FieldName == Convert.ToString(PropertyDictionary["ChargeFromDOS"]) ||
                FieldName == Convert.ToString(PropertyDictionary["ChargeToDOS"]) ||
                FieldName == Convert.ToString(PropertyDictionary["ClaimDate"]) ||
                FieldName == Convert.ToString(PropertyDictionary["OtherClaimDate"]))
            {
                DateTimePicker dtPicker = new DateTimePicker();
                dtPicker.Format = DateTimePickerFormat.Custom;
                dtPicker.CustomFormat = "MM/dd/yyyy";

                C1.Win.C1FlexGrid.CellStyle csdtate;
                Random stringRandom = new Random(1);
                csdtate = this.c1RuleGrid.Styles.Add(stringRandom.GetHashCode().ToString());
                csdtate.Editor = dtPicker;

                c1RuleGrid.SetCellStyle(rowNumber, columnNumber, csdtate);
            }
        }

        private void DeleteRuleCondition(int nRow)
        {
            int tLineNo = 0;
            bool isGoupRemoved = false;
            int groupCounter = 0;
            string groupNames = "";

            try
            {
                if (nRow > 0)
                {

                    tLineNo = Convert.ToInt32(c1RuleGrid.GetData(nRow, COL_LineNo));
                    c1RuleGrid.Rows.RemoveRange(tLineNo, 1); //Line Removed from Grid

                    //Update Line Number in grid
                    for (int i = 1; i < c1RuleGrid.Rows.Count; i++)
                    {
                        c1RuleGrid.SetData(i, COL_LineNo, i);
                    }

                    ArrayList groupsToRemove = new ArrayList();
                    //if (dtRuleGroups.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                    //    {
                    //        int tGroupStart = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]);
                    //        int tGroupEnd = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]);
                    //        if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]) >tLineNo)
                    //            dtRuleGroups.Rows[i]["GroupStart"] = tGroupStart - 1;
                    //        if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) >= tLineNo)
                    //            dtRuleGroups.Rows[i]["GroupEnd"] = tGroupEnd - 1;
                    //        dtRuleGroups.Rows[i]["GroupLevel"] = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) - Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]);
                    //        if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupLevel"]) <= 0)
                    //            groupsToRemove.Add(Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"]));
                    //        else
                    //        {
                    //            IEnumerable<DataRow> drRepeat = (from myRow in dtRuleGroups.AsEnumerable()
                    //                                             where myRow.Field<string>("GroupStart") == Convert.ToString(dtRuleGroups.Rows[i]["GroupStart"]) &&
                    //                                                   myRow.Field<string>("GroupEnd") == Convert.ToString(dtRuleGroups.Rows[i]["GroupEnd"])
                    //                                             select myRow).ToList();
                    //            int drReapeatCount = drRepeat.Count();
                    //            if (drRepeat != null && drReapeatCount > 1)
                    //            {
                    //                if (groupsToRemove.Contains(Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"])) == false)
                    //                    groupsToRemove.Add(Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"]));
                    //            }
                    //        }
                    //    }
                    //    if (groupsToRemove.Count > 0)
                    //    {
                    //        for (int m = 0; m <= groupsToRemove.Count - 1; m++)
                    //        {
                    //            dtRuleGroups.Rows.RemoveAt(Convert.ToInt32(groupsToRemove[m]));
                    //            for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                    //            {
                    //                if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"]) > Convert.ToInt32(groupsToRemove[m]))
                    //                    dtRuleGroups.Rows[i]["GroupId"] = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"]) - 1;
                    //            }
                    //        }
                    //    }
                    //}
                    //ApplyGroupStyle_DeleteCondition();
                    //BuildRuleExpression();

                    //Remove Group from Temp Datatable 

                    if (dtRuleGroups.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                        {
                            if (tLineNo >= Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]) && tLineNo <= Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]))
                            {
                                groupCounter = groupCounter + 1;
                                if (groupNames == "")
                                    groupNames = i.ToString();
                                else
                                    groupNames = groupNames + "," + i.ToString();
                            }
                        }

                        if (groupCounter > 0)
                        {

                            string[] groups = groupNames.Trim().Split(',');

                            for (int k = 0; k <= groups.Length - 1; k++)
                            {
                                int groupId = Convert.ToInt32(groups[k]);
                                int tGroupStart = Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupStart"]);
                                int tGroupEnd = Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupEnd"]);

                                if ((tGroupStart == tLineNo) || (tGroupEnd == tLineNo))
                                {
                                    if (tGroupEnd - tGroupStart == 1)
                                    {
                                        if (isGoupRemoved == false)
                                        {
                                            if (groupsToRemove.Contains(groupId) == false)
                                            {
                                                groupsToRemove.Add(groupId);
                                                isGoupRemoved = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (tGroupStart == tLineNo)
                                        {
                                            DataRow drRepeat = (from myRow in dtRuleGroups.AsEnumerable()
                                                                where myRow.Field<string>("GroupStart") == Convert.ToString(tGroupStart + 1) &&
                                                                      myRow.Field<string>("GroupEnd") == Convert.ToString(tGroupEnd)
                                                                select myRow).FirstOrDefault();
                                            if (drRepeat != null)
                                            {
                                                if (groupsToRemove.Contains(groupId) == false)
                                                    groupsToRemove.Add(groupId);
                                            }
                                            else
                                            {
                                                dtRuleGroups.Rows[groupId]["GroupStart"] = tGroupStart + 1;
                                                dtRuleGroups.Rows[groupId]["GroupLevel"] = Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupEnd"]) - Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupStart"]);
                                            }
                                        }
                                        else if (tGroupEnd == tLineNo)
                                        {
                                            DataRow drRepeat = (from myRow in dtRuleGroups.AsEnumerable()
                                                                where myRow.Field<string>("GroupStart") == Convert.ToString(tGroupStart) &&
                                                                      myRow.Field<string>("GroupEnd") == Convert.ToString(tGroupEnd - 1)
                                                                select myRow).FirstOrDefault();
                                            if (drRepeat != null)
                                            {
                                                if (groupsToRemove.Contains(groupId) == false)
                                                    groupsToRemove.Add(groupId);
                                            }
                                            else
                                            {
                                                dtRuleGroups.Rows[groupId]["GroupEnd"] = tGroupEnd - 1;
                                                dtRuleGroups.Rows[groupId]["GroupLevel"] = Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupEnd"]) - Convert.ToInt32(dtRuleGroups.Rows[groupId]["GroupStart"]);
                                            }
                                        }
                                    }
                                }
                            }

                            if (groupsToRemove.Count > 0)
                            {
                                for (int m = 0; m <= groupsToRemove.Count - 1; m++)
                                {
                                    dtRuleGroups.Rows.RemoveAt(Convert.ToInt32(groupsToRemove[m]));
                                }
                            }
                        }
                        for (int j = 0; j < dtRuleGroups.Rows.Count; j++)
                        {
                            int tGroupStart1 = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupStart"]);
                            int tGroupEnd1 = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupEnd"]);

                            if (tGroupStart1 > tLineNo)
                            {
                                dtRuleGroups.Rows[j]["GroupStart"] = tGroupStart1 - 1;
                                tGroupStart1 = tGroupStart1 - 1;
                                dtRuleGroups.Rows[j]["GroupLevel"] = tGroupEnd1 - tGroupStart1;
                            }
                            if (tGroupEnd1 > tLineNo)
                            {
                                dtRuleGroups.Rows[j]["GroupEnd"] = tGroupEnd1 - 1;
                                tGroupEnd1 = tGroupEnd1 - 1;
                                dtRuleGroups.Rows[j]["GroupLevel"] = tGroupEnd1 - tGroupStart1;
                            }
                        }
                    }
                    ApplyGroupStyle_DeleteCondition();
                    //ApplyGroupStyle_Load();
                    BuildRuleExpression();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CreateGroup()
        {
            int CurrentGroupStart = 0;
            int CurrentGroupEnd = 0;
            int NewGroupStart = 0;
            int NewGroupEnd = 0;
            int TempGroupStart = 0;
            int TempGroupEnd = 0;
            string CodeExpression = "";
            bool blnInvalidGroup = false;

            try
            {

                CodeExpression = txtRuleExpression.Text;
                foreach (C1.Win.C1FlexGrid.Row row in c1RuleGrid.Rows)
                {
                    if (row.Index > 0)
                    {
                        if (Convert.ToBoolean(row[COL_Group]))
                        {
                            if (CurrentGroupStart == 0)
                            {
                                CurrentGroupStart = Convert.ToInt32(row[COL_LineNo]);
                            }
                            CurrentGroupEnd = Convert.ToInt32(row[COL_LineNo]);
                            row[COL_Group] = false;
                        }
                    }
                }
                if (CurrentGroupStart > 0 && CurrentGroupEnd > 0)
                {
                    if (CurrentGroupStart < CurrentGroupEnd)
                    {
                        if (CodeExpression.Contains(CurrentGroupStart.ToString()) && CodeExpression.Contains(CurrentGroupEnd.ToString()))
                        {
                            if (dtRuleGroups.Rows.Count > 0)
                            {
                                TempGroupStart = 0;
                                TempGroupEnd = 0;
                                for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]) == CurrentGroupStart &&
                                        Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) == CurrentGroupEnd)
                                    {
                                        blnInvalidGroup = true;
                                        MessageBox.Show("Group already present", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;// break;
                                    }
                                    else
                                    {
                                        TempGroupStart = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]);
                                        TempGroupEnd = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]);

                                        if (CurrentGroupStart == TempGroupEnd)
                                        {
                                            blnInvalidGroup = true;
                                            MessageBox.Show("Invalid selection, cannot create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;// break;break;
                                        }
                                        if (CurrentGroupEnd == TempGroupStart)
                                        {
                                            blnInvalidGroup = true;
                                            MessageBox.Show("Invalid selection, cannot create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;// break;
                                        }

                                        if (CurrentGroupStart < Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]) &&
                                                                                  CurrentGroupEnd > Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]))
                                        {
                                            if (CurrentGroupEnd < TempGroupEnd)
                                            {
                                                blnInvalidGroup = true;
                                                MessageBox.Show("Invalid selection, cannot create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;// break;
                                            }
                                        }

                                        if (CurrentGroupStart < Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) &&
                                           CurrentGroupEnd > Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]))
                                        {
                                            if (CurrentGroupStart > TempGroupStart)
                                            {
                                                blnInvalidGroup = true;
                                                MessageBox.Show("Invalid selection, cannot create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;// break;
                                            }
                                        }

                                        NewGroupStart = CurrentGroupStart;
                                        NewGroupEnd = CurrentGroupEnd;
                                    }
                                }

                                if ((TempGroupStart == CurrentGroupStart) && (TempGroupEnd == CurrentGroupEnd))
                                {
                                    blnInvalidGroup = true;
                                    MessageBox.Show("Group already present", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;// break;
                                }
                            }
                            else
                            {
                                NewGroupStart = CurrentGroupStart;
                                NewGroupEnd = CurrentGroupEnd;
                            }


                            if (NewGroupEnd <= NewGroupStart)
                            {
                                blnInvalidGroup = true;
                                MessageBox.Show("Invalid selection, cannot create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;// break;
                            }

                            if (blnInvalidGroup == false)
                            {
                                int ind = 0;

                                ind = CodeExpression.IndexOf(NewGroupStart.ToString());
                                var aStringBuilder1 = new StringBuilder(CodeExpression);
                                aStringBuilder1.Remove(ind, NewGroupStart.ToString().Length);
                                aStringBuilder1.Insert(ind, "(" + NewGroupStart.ToString());
                                CodeExpression = aStringBuilder1.ToString();

                                ind = CodeExpression.IndexOf(NewGroupEnd.ToString());
                                var aStringBuilder2 = new StringBuilder(CodeExpression);
                                aStringBuilder2.Remove(ind, NewGroupEnd.ToString().Length);
                                aStringBuilder2.Insert(ind, NewGroupEnd.ToString() + ")");
                                CodeExpression = aStringBuilder2.ToString();

                                DataRow dr = dtRuleGroups.NewRow();
                                dr["GroupId"] = dtRuleGroups.Rows.Count;
                                dr["GroupStart"] = NewGroupStart;
                                dr["GroupEnd"] = NewGroupEnd;
                                dr["GroupLevel"] = NewGroupEnd - NewGroupStart;
                                dtRuleGroups.Rows.Add(dr);

                                txtRuleExpression.Text = CodeExpression;
                                ApplyGroupStyle_Load();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid selection, could not create group", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;// break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteGroup(int GroupColumn, int Group, int DeleteGroupLevel)
        {
            string GroupToDelete = "";
            int dtgroupLevel = 0;
            int dtgroupLevelCounter = 0;

            try
            {
                foreach (DataRow row in dtRuleGroups.Rows)
                {
                    if (Convert.ToInt32(row["GroupStart"]) <= Group && Convert.ToInt32(row["GroupEnd"]) >= Group)
                    {
                        if (Convert.ToInt32(row["GroupLevel"]) == DeleteGroupLevel)
                        {
                            dtgroupLevelCounter = dtgroupLevelCounter + 1;
                            if (GroupToDelete == "")
                            {
                                GroupToDelete = Convert.ToString(row["GroupId"]);
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(row["GroupLevel"]) == DeleteGroupLevel)
                            dtgroupLevelCounter = dtgroupLevelCounter + 1;
                    }
                }

                int delGroupStart = 0;
                int delGroupEnd = 0;
                int tempIndex = 0;
                if (GroupToDelete != "")
                {
                    for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupId"]) == Convert.ToInt32(GroupToDelete))
                        {
                            tempIndex = i;
                            delGroupStart = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]);
                            delGroupEnd = Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]);
                            dtgroupLevel = delGroupEnd - delGroupStart;
                            break;
                        }
                    }
                    dtRuleGroups.Rows.RemoveAt(Convert.ToInt32(tempIndex));
                    BuildRuleExpression();

                    int tmpLevel = delGroupEnd - delGroupStart;
                    if (c1RuleGrid.Cols.Contains("Level " + tmpLevel.ToString()))
                    {
                        for (int j = c1RuleGrid.Cols.IndexOf("Level " + tmpLevel.ToString()); j < COL_AndOr; j++)
                        {
                            RemoveGroupStyle(delGroupStart, delGroupEnd, j);
                        }
                    }

                  //  RemoveGroupStyle(delGroupStart, delGroupEnd, GroupColumn);
                    ApplyGroupStyle_Load();
                    if (dtgroupLevelCounter == 1)
                    {
                        c1RuleGrid.Cols.Remove("Level " + dtgroupLevel.ToString());
                        COL_AndOr = COL_AndOr - 1;
                        COL_Field = COL_Field - 1;
                        COL_Operator = COL_Operator - 1;
                        COL_Value = COL_Value - 1;
                        COL_Browse = COL_Browse - 1;
                        COL_RuleDetailID = COL_RuleDetailID - 1;
                        COL_PropertyID = COL_PropertyID - 1;
                        COL_ParentID = COL_ParentID - 1;
                        COL_ValueID = COL_ValueID - 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                MessageBox.Show(Ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateGroup(int RuleInsertedAt)
        {
            try
            {
                //Update groups in the datatable 
                for (int j = 0; j < dtRuleGroups.Rows.Count; j++)
                {
                    int tGroupStart1 = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupStart"]);
                    int tGroupEnd1 = Convert.ToInt32(dtRuleGroups.Rows[j]["GroupEnd"]);

                    if (tGroupStart1 >= RuleInsertedAt)
                    {
                        dtRuleGroups.Rows[j]["GroupStart"] = tGroupStart1 + 1;
                        tGroupStart1 = tGroupStart1 + 1;
                        dtRuleGroups.Rows[j]["GroupLevel"] = tGroupEnd1 - tGroupStart1;
                    }
                    if (tGroupEnd1 >= RuleInsertedAt)
                    {
                        dtRuleGroups.Rows[j]["GroupEnd"] = tGroupEnd1 + 1;
                        tGroupEnd1 = tGroupEnd1 + 1;
                        dtRuleGroups.Rows[j]["GroupLevel"] = tGroupEnd1 - tGroupStart1;
                    }

                    if (Convert.ToInt32(dtRuleGroups.Rows[j]["GroupLevel"]) <= 0)
                    {
                        gloAuditTrail.gloAuditTrail.ActivityLog("Wrong group deleted from table :" + Convert.ToString(dtRuleGroups.Rows[j]["GroupStart"]) + "," + Convert.ToString(dtRuleGroups.Rows[j]["GroupEnd"]) + "");
                        dtRuleGroups.Rows.RemoveAt(j);
                        j = j - 1;
                    }
                }

                for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]) <= (RuleInsertedAt - 1) && Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) >= (RuleInsertedAt - 1))
                    {
                        if (Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]) == (RuleInsertedAt - 1))
                        {
                            dtRuleGroups.Rows[i]["GroupEnd"] = RuleInsertedAt;
                            dtRuleGroups.Rows[i]["GroupLevel"] = RuleInsertedAt - Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]);
                        }
                    }
                }
                //Apply group styles on new groups
                ApplyGroupStyle_DeleteCondition();
               // ApplyGroupStyle_Load();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RemoveGroupStyle(int start, int end, int Col)
        {
            try
            {
                for (int i = start; i <= end; i++)
                {
                    if (i % 2 == 0)
                        c1RuleGrid.SetCellStyle(i, Col, "Normal");
                    else
                        c1RuleGrid.SetCellStyle(i, Col, "Alternate");
                }
                c1RuleGrid.SetCellImage(start, Col, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyGroupStyle(int start, int end)
        {

            int tmpLevel = end - start;
            int tmpColInsertAt = COL_Group + 1;
            string[] tempColName;
            int _width = pnlRuleGrid.Width;
            try
            {
                if (c1RuleGrid.Rows.Count > 1)
                {
                    if (c1RuleGrid.Cols.Contains("Level " + tmpLevel.ToString()) == false)
                    {
                       
                        c1RuleGrid.Cols.Add();
                        c1RuleGrid.Cols[c1RuleGrid.Cols.Count - 1].Name = "Level " + tmpLevel.ToString();
                        c1RuleGrid.Cols[c1RuleGrid.Cols.Count - 1].AllowSorting = false;
                        c1RuleGrid.Cols[c1RuleGrid.Cols.Count - 1].AllowEditing = false;
                        c1RuleGrid.Cols[c1RuleGrid.Cols.Count - 1].DataType = typeof(Image);
                        c1RuleGrid.Cols[c1RuleGrid.Cols.Count - 1].Width = Convert.ToInt32(_width * 0.017);

                        for (int i = COL_Group + 1; i < COL_AndOr; i++)
                        {
                            tempColName = c1RuleGrid.Cols[i].Name.Split(' ');
                            if (tempColName.Length > 1)
                            {
                                if (Convert.ToInt32(tempColName[1]) < tmpLevel)
                                {
                                    tmpColInsertAt = c1RuleGrid.Cols[i].Index;
                                    if (tmpLevel - Convert.ToInt32(tempColName[1]) >= 1)
                                        break;
                                }
                                else if (Convert.ToInt32(tempColName[1]) > tmpLevel)
                                    tmpColInsertAt = c1RuleGrid.Cols[i].Index + 1;
                            }
                        }
                        c1RuleGrid.Cols.MoveRange(c1RuleGrid.Cols.Count - 1, 1, tmpColInsertAt);

                        COL_AndOr = COL_AndOr + 1;
                        COL_Field = COL_Field + 1;
                        COL_Operator = COL_Operator + 1;
                        COL_Value = COL_Value + 1;
                        COL_Browse = COL_Browse + 1;
                        COL_RuleDetailID = COL_RuleDetailID + 1;
                        COL_PropertyID = COL_PropertyID + 1;
                        COL_ParentID = COL_ParentID + 1;
                        COL_ValueID = COL_ValueID + 1;

                        for (int i = start; i <= end; i++)
                        {

                            int tmpLevelModified = tmpLevel;
                            if (Convert.ToInt16(tmpLevel) > 20)
                            {
                                tmpLevelModified = Convert.ToInt16(tmpLevel.ToString().Substring(tmpLevel.ToString().Length - 1, 1));
                                if (tmpLevelModified == 0)
                                {
                                    tmpLevelModified = 20;
                                }
                            }
                            if (i < c1RuleGrid.Rows.Count)
                            {
                                c1RuleGrid.SetCellStyle(i, tmpColInsertAt, "cs" + tmpLevelModified.ToString());
                                for (int j = tmpColInsertAt; j < COL_AndOr; j++)
                                {
                                    if (c1RuleGrid.GetCellStyle(i, j) != null && c1RuleGrid.GetCellStyle(i, j).Name.Contains("cs"))
                                    {
                                        c1RuleGrid.SetCellStyle(i, j, c1RuleGrid.GetCellStyle(i, j));
                                    }
                                   else
                                   {
                                    c1RuleGrid.SetCellStyle(i, j, "cs" + tmpLevelModified.ToString());
                                   }
                                }
                            }
                        }
                        c1RuleGrid.SetCellImage(start, tmpColInsertAt, global::ChargeRules.Properties.Resources.GroupInTwo10);
                    }
                    else
                    {
                       
                        for (int i = start; i <= end; i++)
                        {
                            int tmpLevelModified = tmpLevel;
                            if (Convert.ToInt16(tmpLevel) > 20)
                            {
                                tmpLevelModified = Convert.ToInt16(tmpLevel.ToString().Substring(tmpLevelModified.ToString().Length - 1, 1));
                                if (tmpLevelModified == 0)
                                {
                                    tmpLevelModified = 20;
                                }
                            }
                            if (i < c1RuleGrid.Rows.Count)
                            {
                                c1RuleGrid.SetCellStyle(i, c1RuleGrid.Cols.IndexOf("Level " + tmpLevel.ToString()), "cs" + tmpLevelModified.ToString());

                                for (int j = c1RuleGrid.Cols.IndexOf("Level " + tmpLevel.ToString()); j < COL_AndOr; j++)
                                {
                                    if (c1RuleGrid.GetCellStyle(i, j) != null && c1RuleGrid.GetCellStyle(i, j).Name.Contains("cs"))
                                    {
                                        c1RuleGrid.SetCellStyle(i, j, c1RuleGrid.GetCellStyle(i, j));
                                    }
                                    else
                                    {
                                        c1RuleGrid.SetCellStyle(i, j, "cs" + tmpLevelModified.ToString());
                                    }
                                }
                            }
                        }
                        c1RuleGrid.SetCellImage(start, c1RuleGrid.Cols.IndexOf("Level " + tmpLevel.ToString()), global::ChargeRules.Properties.Resources.GroupInTwo10);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tempColName = null;
            }
        }

        private void ApplyGroupStyle_Load()
        {
            if (dtRuleGroups.Rows.Count > 0)
            {
              
                for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                {
                    int tmpLevel = Convert.ToInt16(dtRuleGroups.Rows[i]["GroupEnd"]) - Convert.ToInt16(dtRuleGroups.Rows[i]["GroupStart"]);
                   if (c1RuleGrid.Cols.Contains("Level " + tmpLevel.ToString()))
                    {
                        for (int j = c1RuleGrid.Cols.IndexOf("Level " + tmpLevel.ToString()); j < COL_AndOr; j++)
                        {
                            RemoveGroupStyle(Convert.ToInt16(dtRuleGroups.Rows[i]["GroupStart"]), Convert.ToInt16(dtRuleGroups.Rows[i]["GroupEnd"]), j);
                        }
                    }
                }
                dtRuleGroups.DefaultView.Sort = "GroupLevel";
                dtRuleGroups = dtRuleGroups.DefaultView.ToTable(true);
              

                for (int i = 0; i < dtRuleGroups.Rows.Count; i++)
                    {
                        ApplyGroupStyle(Convert.ToInt32(dtRuleGroups.Rows[i]["GroupStart"]), Convert.ToInt32(dtRuleGroups.Rows[i]["GroupEnd"]));
                    }
            }
        }

        private void ApplyGroupStyle_DeleteCondition()
        {
            for (int i = COL_Group + 1; i < COL_AndOr; i++)
            {
                if (i < c1RuleGrid.Cols.Count)
                {
                    if (c1RuleGrid.Cols[i].Name.Contains("Level"))
                    {
                        c1RuleGrid.Cols.Remove(i);
                        i = i - 1;
                    }
                }
            }

            COL_AndOr = 4;
            COL_Field = 5;
            COL_Operator = 6;
            COL_Value = 7;
            COL_Browse = 8;
            COL_RuleDetailID = 9;
            COL_PropertyID = 10;
            COL_ParentID = 11;
            COL_ValueID = 12;
            ApplyGroupStyle_Load();
        }


        private void ExpressionEvaluation()
        {
            Stack st = new Stack();
            try
            {

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9]");
                string str1 = null;
                List<string> groupStr1 = new List<string>();
                string[] SplitedString = new string[100];

                int nGroup = 0;

                foreach (char ch in txtRuleExpression.Text.Trim())
                {
                    if (ch != ')')
                    {
                        if (regex.IsMatch(ch.ToString()) || ch == '(' || ch == ' ' || ch == '\t')
                            st.Push(ch);
                    }
                    else if (ch == ')')
                    {
                        str1 = "";
                        while (Convert.ToString(st.Peek()) != "(" || Convert.ToChar(st.Peek()) != '(')
                        {
                            {
                                str1 = Convert.ToString(st.Pop()) + str1;
                            }
                        }
                        st.Pop();
                        groupStr1.Add(str1);
                        st.Push(str1);
                        nGroup++;

                    }
                }

                char[] whitespace = new char[] { ' ', '\t' };
                dtRuleGroups.Clear();
                foreach (string s in groupStr1)
                {
                    SplitedString = s.Trim().Split(whitespace);

                    if (SplitedString != null && SplitedString.Length > 0)
                    {
                        if (SplitedString[0] != null && SplitedString[SplitedString.Length - 1] != null)
                        {

                            DataRow dr = dtRuleGroups.NewRow();
                            dr["GroupId"] = dtRuleGroups.Rows.Count;
                            dr["GroupStart"] = SplitedString[0];
                            dr["GroupEnd"] = SplitedString[SplitedString.Length - 1];
                            dr["GroupLevel"] = Convert.ToInt16(SplitedString[SplitedString.Length - 1]) - Convert.ToInt16(SplitedString[0]);
                            dtRuleGroups.Rows.Add(dr);
                            dtRuleGroups.AcceptChanges();
                        }
                    }

                }

                if (dtRuleGroups != null && dtRuleGroups.Rows.Count > 0)
                {
                    ApplyGroupStyle_Load();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                st = null;
            }

        }

        private void setRuleData()
        {
            ClsRuleEngine oClsRuleEngine = new ClsRuleEngine();
            DataSet dsRule = null;
            DataTable dtRuleMaster = null;
            DataTable dtRuleConditions = null;
            try
            {
                dsRule = oClsRuleEngine.getRuleData(nRuleID);

                if (dsRule != null)
                {

                    if (dsRule.Tables.Count == 2)
                    {
                        dtRuleMaster = dsRule.Tables[0];
                        dtRuleConditions = dsRule.Tables[1];

                    }

                    if (dtRuleMaster != null)
                    {
                        if (dtRuleMaster.Rows.Count > 0)
                        {
                            txtName.Text = Convert.ToString(dtRuleMaster.Rows[0]["sRuleName"]);
                            txtDescription.Text = Convert.ToString(dtRuleMaster.Rows[0]["sRuleDescription"]);
                            txtRuleExpression.Text = Convert.ToString(dtRuleMaster.Rows[0]["sExpression"]);
                            txtErrorMessage.Text = Convert.ToString(dtRuleMaster.Rows[0]["sErrorMessage"]);

                            switch (Convert.ToInt16(dtRuleMaster.Rows[0]["nEvaluationLogic"]))
                            {
                                case 1:
                                    rdbAnd.Checked = true;
                                    break;
                                case 2:
                                    rdbOr.Checked = true;
                                    break;
                                case 3:
                                    rdbCustom.Checked = true;
                                    break;
                                default:
                                    rdbCustom.Checked = true;
                                    break;
                            }

                            switch (Convert.ToInt16(dtRuleMaster.Rows[0]["nRuleType"]))
                            {
                                case 1:
                                    rdbError.Checked = true;
                                    break;
                                case 2:
                                    rdbWarning.Checked = true;
                                    break;
                                case 3:
                                    rdbInformation.Checked = true;
                                    break;
                                default:
                                    rdbError.Checked = true;
                                    break;
                            }
                        }
                    }


                    if (dtRuleConditions != null)
                    {
                        if (dtRuleConditions.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtRuleConditions.Rows.Count; i++)
                            {
                                DataTable dtValue = new DataTable();
                                DataColumn dCol1 = new DataColumn();
                                dCol1.ColumnName = "Code";
                                dtValue.Columns.Add(dCol1);

                                DataColumn dCol2 = new DataColumn();
                                dCol2.ColumnName = "Id";
                                dtValue.Columns.Add(dCol2);

                                DataColumn dCol3 = new DataColumn();
                                dCol3.ColumnName = "sPredicate";
                                dtValue.Columns.Add(dCol3);

                                DataColumn dCol4 = new DataColumn();
                                dCol4.ColumnName = "nConditionIndex";
                                dtValue.Columns.Add(dCol4);


                                if ((Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimDiagnosis"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimModifier"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["CPTCode"])) && Convert.ToString(dtRuleConditions.Rows[i]["sSelectedOperator"]).Contains("Exist"))
                                {
                                    int cIndex = Convert.ToInt16(dtRuleConditions.Rows[i]["nConditionIndex"]);
                                    for (int j = 0; j < dtRuleConditions.Rows.Count; j++)
                                    {
                                        if (cIndex == Convert.ToInt16(dtRuleConditions.Rows[j]["nConditionIndex"]))
                                        {
                                            dtValue.Rows.Add();
                                            dtValue.Rows[dtValue.Rows.Count - 1]["Code"] = Convert.ToString(dtRuleConditions.Rows[j]["sValue"]);
                                            dtValue.Rows[dtValue.Rows.Count - 1]["Id"] = Convert.ToString(dtRuleConditions.Rows[j]["nValueId"]);
                                            dtValue.Rows[dtValue.Rows.Count - 1]["sPredicate"] = Convert.ToString(dtRuleConditions.Rows[j]["sPredicate"]);
                                            dtValue.Rows[dtValue.Rows.Count - 1]["nConditionIndex"] = Convert.ToString(dtRuleConditions.Rows[j]["nConditionIndex"]);
                                        }
                                    }
                                    DataView dv = dtValue.DefaultView;
                                    dv.Sort = "Code";
                                    dtValue = dv.ToTable();
                                    dv.Dispose();
                                    dv = null;
                                }

                                string PropertyName = "";
                                string PropertyPredicate = string.Empty;
                                string InternalPredicate = string.Empty;
                                if (c1RuleGrid != null && c1RuleGrid.Rows.Count > 0)
                                {
                                    c1RuleGrid.Rows.Add();
                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_LineNo, Convert.ToString(c1RuleGrid.Rows.Count - 1));
                                    try
                                    {
                                        if (dtValue != null)
                                        {
                                            if (dtValue.Rows.Count > 0)
                                            {
                                                PropertyPredicate = (from r in dtRuleConditions.AsEnumerable()
                                                                     where r.Field<string>("sValue") == Convert.ToString(dtValue.Rows[0]["Code"])
                                                                     select r.Field<string>("sPredicate")).First<string>();
                                                InternalPredicate = (from r in dtRuleConditions.AsEnumerable()
                                                                     where r.Field<Int32>("nConditionIndex") == Convert.ToInt32(dtValue.Rows[0]["nConditionIndex"])
                                                                     && r.Field<string>("sPredicate") != PropertyPredicate
                                                                     select r.Field<string>("sPredicate")).FirstOrDefault<string>();
                                                if (InternalPredicate == null)
                                                {
                                                    InternalPredicate = PropertyPredicate;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                    }

                                    if (PropertyPredicate == string.Empty)
                                    {
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_AndOr, Convert.ToString(dtRuleConditions.Rows[i]["sPredicate"]));
                                    }
                                    else
                                    {
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_AndOr, PropertyPredicate);
                                    }
                                   
                                    PropertyDictionary.TryGetValue(Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]), out PropertyName);
                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_PropertyName, Convert.ToString(PropertyName));
                                    
                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_Field, Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]));
                                    SetCellStyleOperatorColumn(Convert.ToString(dtRuleConditions.Rows[i]["Operator"]), c1RuleGrid.Rows.Count - 1);
                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_Operator, Convert.ToString(dtRuleConditions.Rows[i]["sSelectedOperator"]));
                                    if ((Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimDiagnosis"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimModifier"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["CPTCode"])) && Convert.ToString(dtRuleConditions.Rows[i]["sSelectedOperator"]).Contains("Exist"))
                                    {
                                        SetCellStyleValueColumn(dtValue, c1RuleGrid.Rows.Count - 1);
                                        i = i + dtValue.Rows.Count - 1;
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_Value, Convert.ToString(dtValue.Rows[0]["Code"]));
                                    }
                                    else
                                    {
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_Value, Convert.ToString(dtRuleConditions.Rows[i]["sValue"]));
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_ValueID, Convert.ToString(dtRuleConditions.Rows[i]["nValueId"]));
                                    }

                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_PropertyID, Convert.ToString(dtRuleConditions.Rows[i]["nPropertyId"]));
                                    c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, COL_ParentID, Convert.ToString(dtRuleConditions.Rows[i]["nParentId"]));
                                    SetDateTimePicker(Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]), c1RuleGrid.Rows.Count - 1, COL_Value);

                                    c1RuleGrid.SetCellImage(c1RuleGrid.Rows.Count - 1, COL_Remove, global::ChargeRules.Properties.Resources.CloseTFS1);

                                    if ((Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimDiagnosis"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["ClaimModifier"]) ||
                                           Convert.ToString(dtRuleConditions.Rows[i]["sPropertyDisplayName"]) == Convert.ToString(PropertyDictionary["CPTCode"])) && Convert.ToString(dtRuleConditions.Rows[i]["sSelectedOperator"]).Contains("Exist"))
                                    {
                                        c1RuleGrid.SetCellImage(c1RuleGrid.Rows.Count - 1, COL_Browse, global::ChargeRules.Properties.Resources.Browse);
                                        c1RuleGrid.SetData(c1RuleGrid.Rows.Count - 1, "InternalPredicate", InternalPredicate);
                                    }

                                }
                            }
                        }
                    }
                }


                // Modify claim rule Code 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oClsRuleEngine != null)
                {
                    oClsRuleEngine.Dispose();
                    oClsRuleEngine = null;
                }

                if (dsRule != null)
                {
                    dsRule.Dispose();
                    dsRule = null;
                }
                if (dtRuleMaster != null)
                {
                    dtRuleMaster.Dispose();
                    dtRuleMaster = null;
                }
                if (dtRuleConditions != null)
                {
                    dtRuleConditions.Dispose();
                    dtRuleConditions = null;
                }
            }
        }


        //Hemant updated belwo with StoredProcedure instade inline query
        private bool CheckforRuleExist(string checkRuleName)
        {
            bool flag = false;
            object result = null;
          //  DataSet _ds = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (!String.IsNullOrEmpty(checkRuleName))
                {
                    oDB.Connect(false);
                    oParameters.Add("@checkRuleName", checkRuleName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.BigInt);
                    result = oDB.ExecuteScalar("BL_CheckForRuleExist", oParameters);
                    if (result != null && Convert.ToInt16(result) > 0)
                    {
                        flag = true;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
                oParameters.Dispose();
                oParameters = null;
                result = null;
            }
            return flag;
        }
       //Hemant

        private void SetWindowAsPerScreenResolution()
        {
            try
            {
                Int32 myScreenHeight = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99);
                if (myScreenHeight < this.Height)
                { this.Height = myScreenHeight; }

                Int32 myScreenWidth = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63);
                if (myScreenWidth > this.Width)
                { this.Width = myScreenWidth; }
            }
            catch
            {
            }
        }


    //    Private Sub changeHeightAsPerResolution()

    //    Dim myScreenHeight As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99)
    //    If myScreenHeight < Me.Height Then
    //        Me.Height = myScreenHeight
    //    End If
    //    Dim myScreenWidth As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63)
    //    If myScreenWidth > Me.Width Then
    //        Me.Width = myScreenWidth
    //    End If
    //End Sub

        #endregion

        #region for internal lising control
        public gloListControl ogloGridListControl = null;
        public gloPracticeListControl ogloPracticeGridListControl = null;
        
        #region  Properties
        public Int32 CurrentTransactionLine
        {
            get { return c1RuleGrid.RowSel; }
        }
        public bool IsInternalControlActive
        {
            get { return pnlInternalControl.Visible; }
            set
            {
                pnlInternalControl.Visible = value;

                if (!pnlInternalControl.Visible)
                {
                    CloseInternalControl();
                    ClosePracticeInternalControl();
                }
            }
        }

        # endregion
        #region grid events
        private void c1RuleGrid_AfterScroll(object sender, RangeEventArgs e)
        {
           // RePositionInternalControl();
            RePositionPracticeInternalControl();
        }

        private void c1RuleGrid_BeforeScroll(object sender, RangeEventArgs e)
        {
            if (pnlInternalControl.Visible)
            {
                CloseInternalControl();
                ClosePracticeInternalControl();
            }
        }

        private void c1RuleGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Int32 nCol = c1RuleGrid.HitTest(e.X, e.Y).Column;
            Int32 nRow = c1RuleGrid.HitTest(e.X, e.Y).Row;

            try
            {
                if (nCol > -1 && nRow > -1)
                {
                    if (c1RuleGrid.Cols[nCol].Name.Contains("Level ") == true)
                    {
                        if (c1RuleGrid.GetCellImage(nRow, nCol) != null)
                        {
                            C1SuperTooltipDx.SetToolTip(c1RuleGrid, "Ungroup clauses");
                            this.Cursor = Cursors.Hand;
                        }
                        else
                        {
                            C1SuperTooltipDx.SetToolTip(c1RuleGrid, "");
                            this.Cursor = Cursors.Arrow;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void c1RuleGrid_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            try
            {
                #region "Numeric Validation"
                string sFieldName = (string)c1RuleGrid.GetData(c1RuleGrid.RowSel, COL_Field);
                Int16 nPropertyID = Convert.ToInt16(c1RuleGrid.GetData(c1RuleGrid.RowSel, COL_PropertyID));
                if (sFieldName == Convert.ToString(PropertyDictionary["Unit"]))
                {
                    if (c1RuleGrid.ColSel == COL_Value)
                    {
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9]");
                        if (!regex.IsMatch(e.KeyChar.ToString()))
                        {
                            e.Handled = true;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void c1RuleGrid_Click(object sender, EventArgs e)
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
       
        private void c1RuleGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (pnlInternalControl.Visible)
                {
                    //if (ogloGridListControl != null)
                    //{
                    //    ogloGridListControl.GetCurrentSelectedItem();

                    //}
                    if (ogloPracticeGridListControl != null)
                    {
                        ogloPracticeGridListControl.GetCurrentSelectedItem();

                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                #region "Down Key"
                if (pnlInternalControl.Visible)
                {
                    //if (ogloGridListControl != null)
                    //{
                    //    ogloGridListControl.Focus();
                    //}
                    if (ogloPracticeGridListControl != null)
                    {
                        ogloPracticeGridListControl.Focus();
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
                    //if (ogloGridListControl != null)
                    //{
                    //    IsInternalControlActive = false;

                    //}
                    if (ogloPracticeGridListControl != null)
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
                if (c1RuleGrid.ColSel == COL_Value)
                {
                    if (c1RuleGrid.RowSel > 0)
                    {
                        c1RuleGrid.SetData(c1RuleGrid.RowSel, c1RuleGrid.ColSel, "");
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

        private void c1RuleGrid_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
               string sFieldName = (string)c1RuleGrid.GetData(e.Row, COL_Field);
               Int16 nPropertyID = Convert.ToInt16(c1RuleGrid.GetData(e.Row, COL_PropertyID));

                if (!String.IsNullOrEmpty(sFieldName))
                {
                    
                    if (e.Col == COL_Value &&
                        sFieldName != Convert.ToString(PropertyDictionary["HospitalizationFromDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["HospitalizationToDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ChargeFromDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ChargeToDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["Unit"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ClaimDate"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["OtherClaimDate"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["PriorAuthorization"]))
                    {
                        if (sFieldName == Convert.ToString(PropertyDictionary["CPTCode"]) ||
                            sFieldName == Convert.ToString(PropertyDictionary["ClaimModifier"]) ||
                            sFieldName == Convert.ToString(PropertyDictionary["ClaimDiagnosis"]))
                        {
                            if (c1RuleGrid.GetCellImage(e.Row, COL_Browse) != null) //|| Convert.ToString(c1RuleGrid.GetData(CurrentTransactionLine, COL_Operator)).Contains("Exist")
                            {
                                return;
                            }
                        }
                        string _SearchText = "";
                        int pnlwidth = c1RuleGrid.Cols[c1RuleGrid.ColSel].Width;
                        OpenInternalControl(gloGridListControlType.COL_Value, sFieldName, false, e.Row, e.Col, _SearchText, pnlwidth);
                        if (c1RuleGrid != null && c1RuleGrid.Rows.Count > 0)
                        {
                            _SearchText = Convert.ToString(c1RuleGrid.GetData(CurrentTransactionLine, COL_Value));
                            //if (_SearchText != "" && ogloGridListControl != null)
                            //{
                            //    // ogloGridListControl2.FillControl(_SearchText);
                            //    ogloGridListControl.FillControl(sFieldName, _SearchText);

                            //}
                            if (_SearchText != "" && ogloPracticeGridListControl != null)
                            {
                                // ogloGridListControl2.FillControl(_SearchText);
                                ogloPracticeGridListControl.FillControl(sFieldName, _SearchText);

                            }
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

        private void c1RuleGrid_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = c1RuleGrid.Editor.Text;
                string sFieldName = (string)c1RuleGrid.GetData(c1RuleGrid.Row, COL_Field);
                if (ogloPracticeGridListControl != null)
                {
                    ogloPracticeGridListControl.FillControl(sFieldName, _strSearchString);
                    //if (ogloGridListControl.IsHideControl)
                    //{
                    //    //for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                    //    //{
                    //    //    pnlInternalControl.Controls.RemoveAt(i);
                    //    //}
                    //    pnlInternalControl.Visible = false;
                    //    pnlInternalControl.SendToBack();
                    //}
                    //else
                    //{
                    //    // int pnlwidth = c1RuleGrid.Cols[c1RuleGrid.ColSel].Width;
                    //    //// OpenInternalControl(gloGridListControlType.COL_Value, sFieldName, false, c1RuleGrid.RowSel, c1RuleGrid.ColSel, _strSearchString, pnlwidth);
                    //    // ogloGridListControl.FillControl(sFieldName, _strSearchString);
                    //    pnlInternalControl.Visible = true;
                    //    pnlInternalControl.BringToFront();

                    //}
                }
               
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void c1RuleGrid_LeaveEdit(object sender, RowColEventArgs e)
        {
            try
            {
               string sFieldName = (string)c1RuleGrid.GetData(e.Row, COL_Field);
               Int16 nPropertyID = Convert.ToInt16(c1RuleGrid.GetData(e.Row, COL_PropertyID));

               if (e.Col == COL_Value &&
                  sFieldName != Convert.ToString(PropertyDictionary["HospitalizationFromDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["HospitalizationToDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ChargeFromDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ChargeToDOS"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["Unit"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["ClaimDate"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["OtherClaimDate"]) &&
                        sFieldName != Convert.ToString(PropertyDictionary["PriorAuthorization"]))
                {

                    if (c1RuleGrid.Editor != null)
                    {
                        c1RuleGrid.ChangeEdit -= new System.EventHandler(this.c1RuleGrid_ChangeEdit);
                        c1RuleGrid.Editor.Text = "";
                        c1RuleGrid.ChangeEdit += new System.EventHandler(this.c1RuleGrid_ChangeEdit);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Internal control Events

        glc.gloListControl oglcListControl = null;

        void oglcListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            DataTable dtValue = new DataTable();
            DataColumn dCol1 = new DataColumn();
            dCol1.ColumnName = "Code";
            dtValue.Columns.Add(dCol1);

            DataColumn dCol2 = new DataColumn();
            dCol2.ColumnName = "Id";
            dtValue.Columns.Add(dCol2);

            DataColumn dCol3 = new DataColumn();
            dCol3.ColumnName = "sPredicate";
            dtValue.Columns.Add(dCol3);

            if (oglcListControl.SelectedItems.Count > 0)
            {
                for (int i = 0; i <= oglcListControl.SelectedItems.Count - 1; i++)
                {
                    dtValue.Rows.Add();
                    dtValue.Rows[dtValue.Rows.Count - 1]["Code"] = oglcListControl.SelectedItems[i].Code;
                    dtValue.Rows[dtValue.Rows.Count - 1]["Id"] = oglcListControl.SelectedItems[i].ID;
                    dtValue.Rows[dtValue.Rows.Count - 1]["sPredicate"] = oglcListControl.sPredicateType;
                }
                DataView dv = dtValue.DefaultView;
                dv.Sort = "Code";
                dtValue = dv.ToTable();
                dv.Dispose();
                dv = null;

                SetCellStyleValueColumn(dtValue, c1RuleGrid.RowSel);
                c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_Value, Convert.ToString(dtValue.Rows[0]["Code"]));
                c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_ValueID, Convert.ToString(dtValue.Rows[0]["Id"]));
                c1RuleGrid.SetData(c1RuleGrid.RowSel, "InternalPredicate", Convert.ToString(dtValue.Rows[0][2]));
            }
            else
            {
                C1.Win.C1FlexGrid.CellStyle cslist = null;
                c1RuleGrid.SetCellStyle(c1RuleGrid.RowSel, COL_Value, cslist);
                c1RuleGrid.SetData(c1RuleGrid.RowSel, COL_Value, "");
              
            }

            oglcListControl_ItemClosedClick(null, null);

            //dCol1.Dispose();
            //dCol1 = null;

            //dCol2.Dispose();
            //dCol2 = null;
        }

        void oglcListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeListControl();
        }

        private void removeListControl()
        {
            if (oglcListControl != null)
            {
                if (pnlListControl.Controls.Contains(oglcListControl))
                {
                    pnlListControl.Controls.Remove(oglcListControl);
                }
                try
                {
                    oglcListControl.ItemSelectedClick -= new glc.gloListControl.ItemSelected(oglcListControl_ItemSelectedClick);
                    oglcListControl.ItemClosedClick -= new glc.gloListControl.ItemClosed(oglcListControl_ItemClosedClick);
                }
                catch
                {
                }

                oglcListControl.Dispose();
                oglcListControl = null;
            }

            pnlListControl.Visible = false;
            pnlListControl.SendToBack();
            pnlRuleGrid.BringToFront();
            trvProperties.Enabled = true;
        }

        private void OpenListControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText, int pnlwidth, string InternalPredicate)
        {
            try
            {
                if (ControlHeader == "Claim Diagnosis")
                {
                    oglcListControl = new glc.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, glc.gloListControlType.Diagnosis, true, this.Width, "Claim Rule");
                    oglcListControl.QuickNoteTypes = Convert.ToInt32(glc.gloListControlType.Diagnosis);
                    oglcListControl.ShowAllDiagnosis = true;
                    oglcListControl._DiagnosisType = 1;
                }
                else if (ControlHeader == "CPT Code")
                {
                    oglcListControl = new glc.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, glc.gloListControlType.CPT, true, this.Width, "Claim Rule");
                    oglcListControl.QuickNoteTypes = Convert.ToInt32(glc.gloListControlType.CPT);
                }
                else if (ControlHeader == "Claim Modifier")
                {
                    oglcListControl = new glc.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, glc.gloListControlType.Modifier, true, this.Width, "Claim Rule");
                    oglcListControl.QuickNoteTypes = Convert.ToInt32(glc.gloListControlType.Modifier);
                }
                if (oglcListControl != null)
                {
                    oglcListControl.ClinicID = _ClinicID;
                    oglcListControl.ControlHeader = ControlHeader;
                    oglcListControl.sPredicateType = InternalPredicate;
                    oglcListControl.CloseOnDoubleClick = false;
                    oglcListControl.ItemSelectedClick += new glc.gloListControl.ItemSelected(oglcListControl_ItemSelectedClick);
                    oglcListControl.ItemClosedClick += new glc.gloListControl.ItemClosed(oglcListControl_ItemClosedClick);

                    if (c1RuleGrid.GetCellStyle(RowIndex, ColIndex - 1) != null)
                    {
                        DataTable dtValue = (DataTable)((ComboBox)(c1RuleGrid.GetCellStyle(RowIndex, ColIndex - 1)).Editor).DataSource;
                        if (dtValue != null)
                        {
                            for (int nRuleConditionValue = 0; nRuleConditionValue < dtValue.Rows.Count; nRuleConditionValue++)
                            {
                                oglcListControl.SelectedItems.Add(Convert.ToInt64(dtValue.Rows[nRuleConditionValue]["Id"]), Convert.ToString(dtValue.Rows[nRuleConditionValue]["Code"]), "");
                            }
                            //dtValue.Dispose();
                            //dtValue = null;
                        }
                    }
                    pnlListControl.Controls.Add(oglcListControl);
                    pnlListControl.Visible = true;
                    pnlListControl.BringToFront();
                    oglcListControl.OpenControl();
                    oglcListControl.Dock = DockStyle.Fill;
                    oglcListControl.BringToFront();

                    trvProperties.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText, int pnlwidth)
        {
            bool _result = false;
            try
            {
                pnlInternalControl.Width = pnlwidth;
                if (ogloPracticeGridListControl != null)
                {
                    CloseInternalControl();
                    ClosePracticeInternalControl();
                }
                ogloPracticeGridListControl = new gloPracticeListControl(ControlType, ControlHeader, true, pnlInternalControl.Width, RowIndex, ColIndex,_sAUSID,sQCommunicatorServiceURL);
                //ogloPracticeGridListControl.ItemSelected += new ogloPracticeGridListControl.Item_Selected(ogloGridListControl2_ItemSelected);
                ogloPracticeGridListControl.ItemSelected += new gloPracticeListControl.Item_Selected(ogloPracticeGridListControl_ItemSelected);
                ogloPracticeGridListControl.InternalGridKeyDown += new gloPracticeListControl.Key_Down(ogloPracticeGridListControl_InternalGridKeyDown);
                //  ogloPracticeGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloPracticeGridListControl);
                ogloPracticeGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloPracticeGridListControl.Search(SearchText);
                    // ogloGridListControl2.Search2(ControlHeader,SearchText);
                }
                ogloPracticeGridListControl.Show();
              

                int _x = c1RuleGrid.Cols[ColIndex].Left;

                int _y = c1RuleGrid.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;
                int _parentleft = pnlRuleGrid.Bounds.Left; //this.Parent.Bounds.Left;

                int _parentwidth = pnlRuleGrid.Bounds.Width;//Hemant
                int _diffFactor = _parentwidth - _x;//added by hemant

                /////Below commented by hemant
                int _c1RuleGridWidth = c1RuleGrid.Bounds.Width;

                _x = _c1RuleGridWidth - c1RuleGrid.Cols[ColIndex].Width - c1RuleGrid.Cols[COL_Browse].Width;

                int _parentsReamainingHieght = c1RuleGrid.Height - _y;
                int _parentRemainingWidth = pnlInternalControl.Parent.Width - _x;

                if (_height - 10 > _parentsReamainingHieght)
                {
                    _y = _y - _height - 20;
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //uncomented by hemant 
                //above commented by hemant 
                //if (_diffFactor < _width)
                //{
                //    _x = this.Parent.Bounds.Width + (_diffFactor);
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}
                //else
                //{
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}

                //uncomented by hemant 

                pnlInternalControl.Visible = true;
                // if (! ogloPracticeGridListControl.IsHideControl)
                {
                    pnlInternalControl.BringToFront();
                    ogloPracticeGridListControl.Focus();
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
                //RePositionInternalControl();
                RePositionPracticeInternalControl();
            }

            return _result;
        }
        public void ogloGridListControl_ItemSelected(object sender, EventArgs e, int x)
        {

            try
            {
                if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                {
                    c1RuleGrid.SetData(x, COL_Value, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                    c1RuleGrid.SetData(x, COL_ValueID, Convert.ToInt64(ogloGridListControl.SelectedItems[0].ID));
                    this.pnlInternalControl.Hide();
                    c1RuleGrid.Focus();
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
            {

                IsInternalControlActive = false; //CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void ogloPracticeGridListControl_ItemSelected(object sender, EventArgs e, int x)
        {

            try
            {
                if (ogloPracticeGridListControl.SelectedItems != null && ogloPracticeGridListControl.SelectedItems.Count > 0)
                {
                    c1RuleGrid.SetData(x, COL_Value, Convert.ToString(ogloPracticeGridListControl.SelectedItems[0].Code));
                    c1RuleGrid.SetData(x, COL_ValueID, Convert.ToInt64(ogloPracticeGridListControl.SelectedItems[0].ID));
                    this.pnlInternalControl.Hide();
                    c1RuleGrid.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void ogloPracticeGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {

                IsInternalControlActive = false; //CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {


                      //  ogloGridListControl.ItemSelected += new gloListControl.Item_Selected(ogloGridListControl_ItemSelected);
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
        private bool ClosePracticeInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloPracticeGridListControl != null)
                {
                    try
                    {


                        //  ogloGridListControl.ItemSelected += new gloListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloPracticeGridListControl.ItemSelected += new gloPracticeListControl.Item_Selected(ogloPracticeGridListControl_ItemSelected);
                        ogloPracticeGridListControl.InternalGridKeyDown += new gloPracticeListControl.Key_Down(ogloPracticeGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloPracticeGridListControl.Dispose();
                    ogloPracticeGridListControl = null;
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

        private bool CloseTaxonomyInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlTaxonomyInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlTaxonomyInternalControl.Controls.RemoveAt(i);
                }
                if (oglcListControl != null)
                {
                    try
                    {
                        oglcListControl.ItemSelectedClick += new glc.gloListControl.ItemSelected(oglcListControl_ItemSelectedClick);
                        oglcListControl.ItemClosedClick += new glc.gloListControl.ItemClosed(oglcListControl_ItemClosedClick);

                    }
                    catch { }
                    oglcListControl.Dispose();
                    oglcListControl = null;
                }
                pnlTaxonomyInternalControl.Visible = false;
                pnlTaxonomyInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _result;
        }
       
        #endregion


        #endregion

        public Int32 CurrentColumn
        {
            get { return c1RuleGrid.ColSel; }

        }

        private void RePositionInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                {
                    //if (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y > 220)
                    //{
                    //    pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    //}
                    //else
                    //{
                    //    pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    //}

                    if ((this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y > (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y+ pnlInternalControl.Height)
                    {
                        if ((this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        if ((c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) - c1RuleGrid.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Top - pnlInternalControl.Height) + c1RuleGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void RePositionPracticeInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloPracticeGridListControl != null)
                {
                    //if (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y > 220)
                    //{
                    //    pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    //}
                    //else
                    //{
                    //    pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    //}

                    if ((this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y > (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y + pnlInternalControl.Height)
                    {
                        if ((this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (this.Bottom - c1RuleGrid.Rows[CurrentTransactionLine].Bottom) - c1RuleGrid.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Bottom + c1RuleGrid.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);

                    }
                    else
                    {
                        if ((c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) + c1RuleGrid.ScrollPosition.Y; }
                        //pnlInternalControl.Height = (c1RuleGrid.Rows[CurrentTransactionLine].Top - this.Top) - c1RuleGrid.ScrollPosition.Y;
                        pnlInternalControl.SetBounds((c1RuleGrid.Cols[CurrentColumn].Left + c1RuleGrid.ScrollPosition.X), (c1RuleGrid.Rows[CurrentTransactionLine].Top - pnlInternalControl.Height) + c1RuleGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }
  

        private void trvProperties_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
               
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        TreeNode trvNode = default(TreeNode);
                        trvNode = trvProperties.GetNodeAt(e.X, e.Y);

                        int selectedPropertyParentId = 0;
                        int selectedPropertyId = 0;


                        if ((trvNode == null) == false)
                        {
                            trvProperties.SelectedNode = trvNode;

                            if (trvProperties.SelectedNode != null)
                            {

                                if (dtProperties != null && dtProperties.Rows.Count > 0)
                                {
                                    // Get selected Property Id and Parent Id 
                                    DataRow drPropertyDetails = (from myRow in dtProperties.AsEnumerable()
                                                                 where myRow.Field<string>("sPropertyDisplayName") == trvProperties.SelectedNode.Tag.ToString()
                                                                 select myRow).FirstOrDefault();
                                    if (Convert.ToString(drPropertyDetails["nParentId"]) != "")
                                        selectedPropertyParentId = Convert.ToInt32(drPropertyDetails["nParentId"]);

                                    selectedPropertyId = Convert.ToInt32(drPropertyDetails["nPropertyId"]);
                                }
                            }
                        }


                        if ((trvProperties.SelectedNode != null))
                        {
                            if (selectedPropertyParentId > 0)
                            {
                                if (c1RuleGrid.Rows.Count > 1)
                                {
                                    trvProperties.ContextMenu = cntRuleCondtionInsert;
                                }
                                else
                                {
                                    trvProperties.ContextMenu = null;
                                }
                            }
                            else
                            {
                                trvProperties.ContextMenu = null;
                            }
                        }
                    }
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void mnuInsertAboveSlectedRow_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                //if ((!object.ReferenceEquals(trvProperties, trvProperties.SelectedNode)) || (!object.ReferenceEquals(trvProperties.SelectedNode.Parent, trvProperties.SelectedNode)))
               if(trvProperties!=null && trvProperties.SelectedNode!=null)
                {
                    if (c1RuleGrid.RowSel > 0)
                    {
                        RuleConditionInsertOnSelection(c1RuleGrid.RowSel, (mynode)trvProperties.SelectedNode);
                    }
                    else
                    {
                        MessageBox.Show("select rule condtion to insert above", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
			
		
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void mnuInsertBelowSlectedRow_Click(System.Object sender, System.EventArgs e)
        {
            if (trvProperties != null && trvProperties.SelectedNode != null)
            {
                if (c1RuleGrid.RowSel > 0)
                {
                    RuleConditionInsertOnSelection(c1RuleGrid.RowSel + 1, (mynode)trvProperties.SelectedNode);
                }
                else
                {
                    MessageBox.Show("select rule condtion to insert below", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void cmbPracticeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPracticeList.SelectedIndex != 0)
            {
                DataTable dt = (DataTable)cmbPracticeList.DataSource;
                DataRow[] drRows = dt.Select();// ("nPracticeID=" + cmbPracticeList.SelectedItem);
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt64(dr["nPracticeID"])==Convert.ToInt64(cmbPracticeList.SelectedValue))
                    {
                        _sAUSID = Convert.ToString(dr["sAUSID"]);
                    }
                    
                }
            }

        }

        private void btn_BrowseCmpTaxonomy_Click(object sender, EventArgs e)
        {
            bool _result = false;
            try
            {
                //pnlTaxonomyInternalControl.Height = 569;
                //pnlTaxonomyInternalControl.Width = 675;
                if (oglcListControl != null)
                {
                    CloseTaxonomyInternalControl();
                }
                oglcListControl = new glc.gloListControl(_databaseconnectionstring, glc.gloListControlType.Taxonomy, true, pnlTaxonomyInternalControl.Width);
                //ogloTaxonomyListControl.ItemSelected += new ogloTaxonomyListControl.Item_Selected(ogloGridListControl2_ItemSelected);
                oglcListControl.ItemSelectedClick += new glc.gloListControl.ItemSelected(oglcListControl_TaxonomyItemSelectedClick);
                oglcListControl.ItemClosedClick += new glc.gloListControl.ItemClosed(oglcListControl_TaxonomyItemClosedClick);
                //  ogloTaxonomyListControl.ControlHeader = ControlHeader;
                foreach (var item in cmbCoveringSpeciality.Items)
                {
                    Int64 nItemTaxonomyID = Convert.ToInt64(((System.Data.DataRowView)(item)).Row.ItemArray[0]);
                    oglcListControl.SelectedItems.Add(nItemTaxonomyID, cmbCoveringSpeciality.Text);
                }
                pnlTaxonomyInternalControl.Controls.Add(oglcListControl);
                pnlTaxonomyInternalControl.Visible = true;
                pnlTaxonomyInternalControl.BringToFront();
                oglcListControl.ControlHeader = "Covering Speciality";
                oglcListControl.OpenControl();
                oglcListControl.Dock = DockStyle.Fill;
                oglcListControl.BringToFront();
                //ogloPracticeGridListControl.Dock = DockStyle.Fill;
                
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void oglcListControl_TaxonomyItemSelectedClick(object sender, EventArgs e)
        {
            DataTable dtValue = new DataTable();
            

            DataColumn dCol2 = new DataColumn();
            dCol2.ColumnName = "Id";
            dtValue.Columns.Add(dCol2);

            DataColumn dCol1 = new DataColumn();
            dCol1.ColumnName = "Code";
            dtValue.Columns.Add(dCol1);

            if (oglcListControl.SelectedItems.Count > 0)
            {
                for (int i = 0; i <= oglcListControl.SelectedItems.Count - 1; i++)
                {
                    dtValue.Rows.Add();
                    dtValue.Rows[dtValue.Rows.Count - 1]["Code"] = oglcListControl.SelectedItems[i].Code;
                    dtValue.Rows[dtValue.Rows.Count - 1]["Id"] = oglcListControl.SelectedItems[i].ID;
                }
                DataView dv = dtValue.DefaultView;
                dv.Sort = "Code";
                dtValue = dv.ToTable();
                dv.Dispose();
                dv = null;

                cmbCoveringSpeciality.DataSource = dtValue;
                cmbCoveringSpeciality.DisplayMember = "Code";
                cmbCoveringSpeciality.ValueMember = "Id";
            }
            //else
            //{
            //    cmbCoveringSpeciality.DataSource = null;
            //    //cmbCoveringSpeciality.DisplayMember = "Code";
            //    //cmbCoveringSpeciality.ValueMember = "Code";
            //}

            oglcListControl_TaxonomyItemClosedClick(null, null);

            //dCol1.Dispose();
            //dCol1 = null;

            //dCol2.Dispose();
            //dCol2 = null;
        }

        void oglcListControl_TaxonomyItemClosedClick(object sender, EventArgs e)
        {
            removeTaxonomyListControl();
        }
        private void removeTaxonomyListControl()
        {
            if (oglcListControl != null)
            {
                if (pnlTaxonomyInternalControl.Controls.Contains(oglcListControl))
                {
                    pnlTaxonomyInternalControl.Controls.Remove(oglcListControl);
                }
                try
                {
                    oglcListControl.ItemSelectedClick -= new glc.gloListControl.ItemSelected(oglcListControl_ItemSelectedClick);
                    oglcListControl.ItemClosedClick -= new glc.gloListControl.ItemClosed(oglcListControl_ItemClosedClick);
                }
                catch
                {
                }

                oglcListControl.Dispose();
                oglcListControl = null;
            }

            pnlTaxonomyInternalControl.Visible = false;
            pnlTaxonomyInternalControl.SendToBack();
        }

        private void btn_ClearCmpTaxonomy_Click(object sender, EventArgs e)
        {
            fillSpeciality();
        }

        private void fillSpeciality()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtRace = null;
                string _sqlQuery = "";

                _sqlQuery = "SELECT 0 as nSpecialtyID, 'ALL' as sTaxonomyCode ";
                oDB.Retrive_Query(_sqlQuery, out dtRace);
                oDB.Disconnect();

                if (dtRace != null)
                {
                    //DataRow dr = dtRace.NewRow();
                    //dr["nSpecialtyID"] = 0;
                    //dr["sTaxonomyCode"] = "";
                    //dtRace.Rows.InsertAt(dr, 0);
                    //dtRace.AcceptChanges();

                    cmbCoveringSpeciality.DataSource = dtRace;
                    cmbCoveringSpeciality.DisplayMember = "sTaxonomyCode";
                    cmbCoveringSpeciality.ValueMember = "nSpecialtyID";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            //cmbPARace.Items.Add("American");
            //cmbPARace.Items.Add("Black American");
        }
    }
}



