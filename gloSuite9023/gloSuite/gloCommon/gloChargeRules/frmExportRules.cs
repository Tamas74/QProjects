using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChargeRules
{
    public partial class frmExportRules : Form
    {
        #region Constructor and Properties
        public string DBString { get; set; }
        private string _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;

        DataTable dt = null;

        public frmExportRules()
        {
            InitializeComponent();
            this.DBString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
        }
        #endregion

        #region Form Loading and Closing
        private void frmExportRules_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            try
            {
                this.BindRules(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
              
                this.chkRules.DataSource = null;
                this.chkRules.Items.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Browse Button Click
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                { txtFolderPath.Text = folderBrowserDialog.SelectedPath; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Directory Check
        private bool ExecuteValidations()
        {
            bool bPassed = false;

            try
            {
                if (txtFolderPath.Text.Length != 0)
                {
                    if (Directory.Exists(txtFolderPath.Text))
                    {
                        if (chkRules.CheckedItems.Count == 0)
                        {
                            MessageBox.Show("Please select a rule to export.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            chkRules.Focus();
                        }
                        else
                        { bPassed = true; }

                    }
                    else
                    {
                        MessageBox.Show("Selected directory path is not valid.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_Browse.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Please enter the directory path.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Browse.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return bPassed;
        }
        #endregion

        #region Export Button Click
        private void tsb_Export_Click(object sender, EventArgs e)
        {
            DataTable dtTVP = null;
            DataSet ds = null;
            ClsRuleEngine ruleEngine = null;

            try
            {
                if (!this.ExecuteValidations()) { return; }

                ruleEngine = new ClsRuleEngine();

                dtTVP = this.GetSelectedValues();
                ds = ruleEngine.GetRules(dtTVP);

                if (ds != null && ds.Tables.Count == 2)
                {
                    DataTable dtMaster = ds.Tables[0];
                    DataTable dtConditions = ds.Tables[1];

                    foreach (DataRow element in dtMaster.Rows)
                    {
                        Rule rule = new Rule();

                        rule.RuleName = Convert.ToString(element["sRuleName"]);
                        rule.RuleDescription = Convert.ToString(element["sRuleDescription"]);
                        rule.EvaluationLogic = Convert.ToInt32(element["nEvaluationLogic"]);
                        rule.RuleExpression = Convert.ToString(element["sExpression"]);
                        rule.RuleMessage = Convert.ToString(element["sErrorMessage"]);
                        rule.Version = Convert.ToString(element["sVersion"]);
                        rule.RuleTypeInfo = (gloUIControlLibrary.Classes.ClaimRules.RuleType)Convert.ToInt32(element["nRuleType"]);

                        foreach (DataRow row in dtConditions.AsEnumerable()
                                                            .Where(p => Convert.ToInt64(p["nRuleID"]) == Convert.ToInt64(element["nRuleID"])))
                        {
                            RuleCondition ruleCondition = new RuleCondition();

                            ruleCondition.ConditionIndex = Convert.ToInt32(row["nConditionIndex"]);
                            ruleCondition.Predicate = Convert.ToString(row["sPredicate"]);
                            ruleCondition.PropertyID = Convert.ToInt32(row["nPropertyID"]);

                            ruleCondition.Operator = (Operator)Enum.Parse(typeof(Operator), Convert.ToString(row["sOperatorDisplayText"]));

                            ruleCondition.PropertyDisplayName = Convert.ToString(row["sPropertyDisplayName"]);
                            ruleCondition.OperatorDisplayText = Convert.ToString(row["sOperatorDisplayText"]);
                            ruleCondition.sValue = Convert.ToString(row["sValue"]);

                            rule.RuleConditions.Add(ruleCondition);
                            ruleCondition = null;
                        }

                        this.WriteOutputFile(rule);

                        rule.Dispose();
                        rule = null;
                    }

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(
                                                              gloAuditTrail.ActivityModule.ChargeRule,
                                                              gloAuditTrail.ActivityCategory.ChargeRuleSetup,
                                                              gloAuditTrail.ActivityType.Export,
                                                              dtMaster.Rows.Count.ToString() + " rule(s) exported.",
                                                              gloAuditTrail.ActivityOutCome.Success
                                                              );


                    MessageBox.Show(dtMaster.Rows.Count.ToString() + " rule(s) successfully exported.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindRules(txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

                if (dtTVP != null)
                {
                    dtTVP.Dispose();
                    dtTVP = null;
                }

                if (ruleEngine != null)
                {
                    ruleEngine.Dispose();
                    ruleEngine = null;
                }
            }
        }
        #endregion

        #region Write Output File
        private void WriteOutputFile(Rule Rule)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlSerializer xmlSerializer = null;
            Encoding utf8EncodingWithOutBOM = null;
            XmlDocument xDoc = null;

            string sFileName = string.Empty;
            Regex regex = null; ;

            try
            {
                utf8EncodingWithOutBOM = new UTF8Encoding(false);
                settings.Encoding = utf8EncodingWithOutBOM;

                regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"]))", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                sFileName = regex.Replace(Rule.RuleName, string.Empty);

                xmlSerializer = new XmlSerializer(typeof(Rule));

                using (XmlWriter xmlWriter = XmlWriter.Create(txtFolderPath.Text + "\\" + sFileName + "_" + DateTime.Now.ToString("MMddyyyyhhssfff") + ".xml", settings))
                {
                    xmlSerializer.Serialize(xmlWriter, Rule);
                    xDoc = new XmlDocument();
                    xDoc.WriteTo(xmlWriter);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (settings != null)
                { settings = null; }

                if (xmlSerializer != null)
                { xmlSerializer = null; }

                if (utf8EncodingWithOutBOM != null)
                { utf8EncodingWithOutBOM = null; }

                regex = null;

                if (xDoc != null)
                { xDoc = null; }
            }
        }
        #endregion

        #region Get Selected Checkboxes Values
        private DataTable GetSelectedValues()
        {
            DataTable dtTVP = new DataTable();
            System.Windows.Forms.CheckedListBox.CheckedItemCollection checkedCollection = null;

            try
            {
                dtTVP.Columns.Add("nRuleID");

                if (chkRules.CheckedItems != null)
                {
                    checkedCollection = chkRules.CheckedItems;

                    foreach (var element in chkRules.CheckedItems)
                    {
                        if (element is DataRowView)
                        {
                            DataRowView dRowView = (DataRowView)element;

                            DataRow AddedRow = dtTVP.Rows.Add();
                            AddedRow["nRuleID"] = Convert.ToInt64(dRowView.Row[0]);
                            AddedRow = null;

                            dRowView = null;
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
                checkedCollection = null;
            }

            return dtTVP;
        }
        #endregion

        #region Bind Datatable to Listbox
        private void BindRules(string Text)
        {
            ClsRuleEngine ruleEngine = null;
            DataSet ds = null;

            try
            {
                chkRules.DataSource = null;
                ruleEngine = new ClsRuleEngine();
                ds = ruleEngine.GetRules(Text);

                this.dt = ds.Tables[0];

                chkRules.DataSource = ds.Tables[0];
                chkRules.DisplayMember = "sRuleName";
                chkRules.ValueMember = "nRuleID";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Search Functionality
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            IEnumerable<DataRow> DataRows = null;

            try
            {
                List<string> lstItems = new List<string>();
                for (int i = 0; i < chkRules.Items.Count; i++)
                {
                    if (chkRules.GetItemCheckState(i) == CheckState.Checked)
                    {
                        lstItems.Add(chkRules.GetItemText(chkRules.Items[i]));
                    }
                }
                for (int i = 0; i < chkRules.Items.Count; i++) chkRules.SetItemCheckState(i, CheckState.Unchecked);

                DataRows = this.dt.AsEnumerable()
                    .Where(p => Convert.ToString(p["sRuleName"]).ToLower().Contains(txtSearch.Text.Trim().ToLower()));

                if (DataRows.Any())
                {
                    chkRules.DataSource = DataRows.CopyToDataTable<DataRow>();
                    chkRules.DisplayMember = "sRuleName";
                    chkRules.ValueMember = "nRuleID";

                    for (int i = 0; i < chkRules.Items.Count; i++)
                    {
                        string itemValue = chkRules.GetItemText(chkRules.Items[i]);
                        for (int j = 0; j < lstItems.Count; j++)
                        {
                            string selectedItemValue = lstItems[j];
                            if (itemValue == selectedItemValue)
                            {
                                chkRules.SetItemCheckState(i, CheckState.Checked);
                            }
                        }
                    }
                }
                else
                {
                   
                    chkRules.DataSource = null;
                    chkRules.Items.Clear();
                    chkRules.DisplayMember = null;
                    chkRules.ValueMember = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Select all/Unselect all 
        private void tsb_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkRules.Items.Count>0)
                {
                    if (sender.ToString() == "&Unselect All")
                    {
                        for (int i = 0; i < chkRules.Items.Count; i++) chkRules.SetItemCheckState(i, CheckState.Unchecked);
                        tsb_SelectAll.Text = "Select &All";
                        tsb_SelectAll.Image = global::ChargeRules.Properties.Resources.SelectAllRules;
                        tsb_SelectAll.Checked = false;

                    }
                    else if (sender.ToString() == "Select &All")
                    {
                        for (int i = 0; i < chkRules.Items.Count; i++)
                            chkRules.SetItemCheckState(i, CheckState.Checked);

                        tsb_SelectAll.Image = global::ChargeRules.Properties.Resources.DeselectAllRules;
                        tsb_SelectAll.Text = "&Unselect All";
                        tsb_SelectAll.Checked = true;
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

        private void chkRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bIsSingleCheckBoxUncheck = false;
            for (int i = 0; i < chkRules.Items.Count; i++)
            {
                if (chkRules.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    bIsSingleCheckBoxUncheck = true;
                    break;
                }
            }

            if (bIsSingleCheckBoxUncheck)
            {
                tsb_SelectAll.Text = "Select &All";
                tsb_SelectAll.Image = global::ChargeRules.Properties.Resources.SelectAllRules;
                tsb_SelectAll.Checked = false;
            }
            else
            {
                tsb_SelectAll.Image = global::ChargeRules.Properties.Resources.DeselectAllRules;
                tsb_SelectAll.Text = "&Unselect All";
                tsb_SelectAll.Checked = true;
            }
        }

       
    }
}
