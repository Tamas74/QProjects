using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ChargeRules
{
    public partial class frmImportRules : Form
    {
        private string _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        private Font boldFont = null;
        private Font normalFont = null;

        #region Constructor and Properties
        DataTable dtTVP = null;

        public frmImportRules()
        {
            InitializeComponent();

            dtTVP = new DataTable();
            dtTVP.Columns.Add("nRuleID");
            dtTVP.Columns.Add("nRuleDetailID");
            dtTVP.Columns.Add("nConditionIndex");
            dtTVP.Columns.Add("sPredicate");
            dtTVP.Columns.Add("sPropertyDisplayName");
            dtTVP.Columns.Add("sOperatorDisplayText");
            dtTVP.Columns.Add("sValue");
            dtTVP.Columns.Add("nValueId");

            boldFont = rbtDirectory.Font;
            normalFont = rbtFile.Font;
        }
        #endregion

        #region Browse Button Click
        private void btnDirectoryPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtDirectoryPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Import file subroutine
        private bool ImportFile(string filePath)
        {
            ClsRuleEngine ruleEngine = null;
            Int64 nRuleID = 0;

            bool bReturned = false;
            object dObject = null;
            XmlSerializer deserializer = null;
            try
            {
                deserializer = new XmlSerializer(typeof(ChargeRules.Rule));
                ruleEngine = new ClsRuleEngine();

                using (XmlReader xmlreader = XmlReader.Create(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        try { dObject = deserializer.Deserialize(reader); }
                        catch (Exception)
                        {
                            rtStatus.AppendText("File " + filePath + " is invalid. Import skipped." + Environment.NewLine);
                            rtStatus.AppendText(Environment.NewLine);
                        }                        

                        if (dObject != null && dObject is ChargeRules.Rule)
                        {
                            using (Rule dRule = (Rule)dObject)
                            {
                                if (Convert.ToInt16(gloGlobal.gloPMGlobal.ApplicationVersion.Replace(".", "")) < Convert.ToInt16(dRule.Version.Replace(".", "")))
                                {
                                    rtStatus.AppendText("File " + filePath + " is invalid. Import skipped." + Environment.NewLine);
                                    rtStatus.AppendText(Environment.NewLine);
                                    rtStatus.AppendText("Higher version(" + dRule.Version + ") rule can't imported in lower verion(" + gloGlobal.gloPMGlobal.ApplicationVersion + ").");
                                    return false;
                                }

                                if (dRule.RuleConditions.Count != 0 && dRule.RuleExpression.Any())
                                {
                                    rtStatus.AppendText("Importing rule: " + dRule.RuleName + Environment.NewLine);

                                    nRuleID = ruleEngine.SaveRuleMasterData(
                                                                            0,
                                                                            dRule.RuleName,
                                                                            dRule.RuleDescription,
                                                                            dRule.EvaluationLogic,
                                                                            dRule.RuleExpression,
                                                                            dRule.RuleMessage,
                                                                            dRule.RuleTypeInfo.GetHashCode()
                                                                            );                                    

                                    if (nRuleID > 0)
                                    {
                                        dtTVP.Clear();
                                        foreach (RuleCondition condition in dRule.RuleConditions)
                                        {
                                            DataRow dRow = dtTVP.Rows.Add();
                                            dRow["nRuleID"] = nRuleID;
                                            dRow["nRuleDetailID"] = 0;

                                            dRow["nConditionIndex"] = Convert.ToInt16(condition.ConditionIndex);
                                            dRow["sPredicate"] = Convert.ToString(condition.Predicate);
                                            dRow["sPropertyDisplayName"] = Convert.ToString(condition.PropertyDisplayName);

                                            dRow["sOperatorDisplayText"] = condition.Operator.ToString();
                                            dRow["sValue"] = Convert.ToString(condition.sValue);
                                            dRow["nValueId"] = 0;
                                            dRow = null;
                                        }

                                        ruleEngine.SaveRuleDetailsData(dtTVP);
                                        dtTVP.Clear();
                                        bReturned = true;
                                    }

                                    rtStatus.AppendText("Rule: " + dRule.RuleName + " imported." + Environment.NewLine);
                                    rtStatus.AppendText(Environment.NewLine);

                                }
                                else
                                {
                                    bReturned = false;
                                    rtStatus.AppendText("Rule: " + dRule.RuleName + " is invalid. Import skipped." + Environment.NewLine);
                                    rtStatus.AppendText(Environment.NewLine);
                                }
                            }
                        }                        
                    }
                }

                return bReturned;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                bReturned = false;
                return bReturned;
            }
            finally
            {
                dObject = null;
                if (ruleEngine != null) { ruleEngine.Dispose(); ruleEngine = null; }
                if (deserializer != null) { deserializer = null; }
            }
        }
        #endregion

        #region Import Button Click
        private void tsb_Import_Click(object sender, EventArgs e)
        {
            string sPathName = string.Empty;
            Int32 nNumberOfFilesImported = 0;

            try
            {
                if (rbtDirectory.Checked)
                {
                    sPathName = txtDirectoryPath.Text;

                    if (sPathName.Length == 0)
                    {
                        MessageBox.Show("Please enter the directory path.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!Directory.Exists(sPathName))
                    {
                        MessageBox.Show("Specified Directory does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (rbtFile.Checked)
                {
                    sPathName = txtFilePath.Text;

                    if (sPathName.Length == 0)
                    {
                        MessageBox.Show("Please enter a file path for importing.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!File.Exists(sPathName))
                    {
                        MessageBox.Show("Specified file does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                this.Cursor = Cursors.WaitCursor;

                this.Height = 400;
                this.pnlStatus.Visible = true;
                rtStatus.Clear();

                if (rbtDirectory.Checked)
                {

                    foreach (string filePath in Directory.EnumerateFiles(sPathName, "*.xml", SearchOption.AllDirectories))
                    {
                        if (File.Exists(filePath))
                        {
                            if (this.ImportFile(filePath))
                            { nNumberOfFilesImported = nNumberOfFilesImported + 1; }
                        }
                    }

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(
                                                                gloAuditTrail.ActivityModule.ChargeRule,
                                                                gloAuditTrail.ActivityCategory.ChargeRuleSetup,
                                                                gloAuditTrail.ActivityType.Load,
                                                                nNumberOfFilesImported.ToString() + " rule(s) imported.",
                                                                gloAuditTrail.ActivityOutCome.Success
                                                                );

                    MessageBox.Show(nNumberOfFilesImported.ToString() + " rule(s) imported.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (rbtFile.Checked)
                {
                    this.ImportFile(sPathName);

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(
                                                                gloAuditTrail.ActivityModule.ChargeRule,
                                                                gloAuditTrail.ActivityCategory.ChargeRuleSetup,
                                                                gloAuditTrail.ActivityType.Load,
                                                                sPathName + " Rule imported.",
                                                                gloAuditTrail.ActivityOutCome.Success
                                                                );
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region Form Closing
        private void frmImportRules_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dtTVP != null)
                {
                    dtTVP.Dispose();
                    dtTVP = null;
                }

                if (this.boldFont != null)
                {
                    this.boldFont.Dispose();
                    this.boldFont = null;
                }

                if (this.normalFont != null)
                {
                    this.normalFont.Dispose();
                    this.normalFont = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        { this.Close(); }
        #endregion

        #region Option button check changed
        private void rbtDirectory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtDirectoryPath.Enabled = rbtDirectory.Checked;
                btnDirectoryPath.Enabled = rbtDirectory.Checked;

                if (rbtDirectory.Checked)
                {
                    rbtDirectory.Font = boldFont;
                    rbtFile.Font = normalFont;
                }

                rbtFile.Checked = !rbtDirectory.Checked;
                txtFilePath.Enabled = !rbtDirectory.Checked;
                btnFilePath.Enabled = !rbtDirectory.Checked;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtFile_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtFilePath.Enabled = rbtFile.Checked;
                btnFilePath.Enabled = rbtFile.Checked;

                if (rbtFile.Checked)
                {
                    rbtFile.Font = boldFont;
                    rbtDirectory.Font = normalFont;
                }

                rbtDirectory.Checked = !rbtFile.Checked;
                txtDirectoryPath.Enabled = !rbtFile.Checked;
                btnDirectoryPath.Enabled = !rbtFile.Checked;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
