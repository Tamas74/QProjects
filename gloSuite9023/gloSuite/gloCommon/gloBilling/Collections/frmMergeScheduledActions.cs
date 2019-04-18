using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Collections
{
    public partial class frmMergeScheduledActions : Form
    {

        private int COL_To = 0;
        private int COL_Select = 1;
        private int COL_ID = 2;
        private int COL_Code = 3;
        private int COL_Description = 4;
        private int COL_TemplateName = 5;
        private int COL_DefaultNextAction = 6;
        private int COL_FollowupActionDays = 7;
        private int COL_SystemType = 8;
        //private int COL_Count = 8;
        private string sConnectionString = String.Empty;
        private string _messageboxcaption = String.Empty;
        CollectionEnums.FollowUpType varFollowupType;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmMergeScheduledActions(string strConnectionString)
        {
            InitializeComponent();
            sConnectionString = strConnectionString;
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
        private Boolean IsScheduleActionsActive(Int64 ID)
        {

            DataTable dt = null;
            Boolean result = false;
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sConnectionString);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;
                _sqlRetrieveQuery = "SELECT isnull(bIsActive,0) As bIsActive   FROM dbo.CL_FollowUpAction_Mst WITH ( NOLOCK ) WHERE nFollowUpActionID=" + ID;
                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }
                oDB.Dispose();
                oDB = null;

                if (dt != null && dt.Rows.Count > 0)
                { result = Convert.ToBoolean(dt.Rows[0]["bIsActive"]); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return result;
        }

        private void tsbtn_Merge_Click(object sender, EventArgs e)
        {
            Int64 nMergeInToScheduledActionId = 0;
            string sMergeFromScheduledActionIds = "";
            CL_FollowUpCode oFollowUpCode = null;
            try
            {
                int _nMergeintoRow = 0;
                int _nMergeFromRow = 0;
                string sMergeToAction=string.Empty, sMergeFromAction=string.Empty;

                if (c1ScheduledActionList.Rows.Count > 0)
                {
                    _nMergeintoRow = c1ScheduledActionList.FindRow(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode(), 1, COL_To, true);
                    _nMergeFromRow = c1ScheduledActionList.FindRow(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode(), 1, COL_Select, true);
                }
                if (_nMergeintoRow <= 0)
                {
                    MessageBox.Show("Select \"Merge To\" Scheduled Action.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    nMergeInToScheduledActionId = Convert.ToInt64(c1ScheduledActionList.GetData(_nMergeintoRow, COL_ID));
                    sMergeToAction=Convert.ToString(c1ScheduledActionList.GetData(_nMergeintoRow,COL_Code));
                    if (nMergeInToScheduledActionId != 0)
                    {
                        if (IsScheduleActionsActive(nMergeInToScheduledActionId) == false)
                        {
                            MessageBox.Show("Error : Merge FollowUp action failed.\n \"Merge To\" followup scheduled action should be active.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                }
                if (_nMergeFromRow <= 0)
                {
                    MessageBox.Show("Select at least one \"Merge From\" Scheduled Action for merge.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 1; i <= c1ScheduledActionList.Rows.Count - 1; i++)
                {
                    if (c1ScheduledActionList.GetCellCheck(i, COL_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        sMergeFromScheduledActionIds = sMergeFromScheduledActionIds + "," + Convert.ToString(c1ScheduledActionList.GetData(i, COL_ID));
                        sMergeFromAction = sMergeFromAction +"- "+ Convert.ToString(c1ScheduledActionList.GetData(i, COL_Code)) + "\n";
                    }

                }

                if (!string.IsNullOrEmpty(sMergeFromScheduledActionIds))
                {
                    bool Result = false;
                    sMergeFromScheduledActionIds = sMergeFromScheduledActionIds.Substring(1, sMergeFromScheduledActionIds.Length - 1);
                    oFollowUpCode = new CL_FollowUpCode();
                    Result =oFollowUpCode.MergeScheduledActions(nMergeInToScheduledActionId, sMergeFromScheduledActionIds, varFollowupType);

                    if (Result)
                    {
                        string sMessage = string.Format("Follow up scheduled action: \n{0} \nMerged into \"{1}\".", sMergeFromAction, sMergeToAction);
                        MessageBox.Show(sMessage, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadScheduledAction(varFollowupType);
                    }
                    else
                        MessageBox.Show("Follow up scheduled action merge failed.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oFollowUpCode != null) { oFollowUpCode.Dispose(); oFollowUpCode = null; }
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdInsClaimFollowup_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInsClaimFollowup.Checked)
            {
                varFollowupType = CollectionEnums.FollowUpType.Claim;
                LoadScheduledAction(varFollowupType);
            }
        }

        private void rdPatAccountFolloup_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPatAccountFolloup.Checked)
            {
                varFollowupType = CollectionEnums.FollowUpType.PatientAccount;
                LoadScheduledAction(varFollowupType);
            }
        }

        private void frmMergeScheduledActions_Load(object sender, EventArgs e)
        {
            rdInsClaimFollowup.Checked = true;
        }

        private void LoadScheduledAction(CollectionEnums.FollowUpType followUpType)
        {
            try
            {
                RefreshGrid(followUpType);
                DesignGrid(c1ScheduledActionList);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void RefreshGrid(CollectionEnums.FollowUpType followUpType)
        {
            DataTable dtScheduledAction = null;
            CL_FollowUpCode oFollowUpCode = null;
            try
            {
                oFollowUpCode=new CL_FollowUpCode();
                dtScheduledAction = oFollowUpCode.GetFollowUpAction_Merge(followUpType);
                if (dtScheduledAction != null && dtScheduledAction.Rows.Count > 0)
                {
                    c1ScheduledActionList.DataSource = null;
                    c1ScheduledActionList.BeginUpdate();
                    c1ScheduledActionList.DataSource = dtScheduledAction.Copy().DefaultView;
                    int nWidth = c1ScheduledActionList.Width;

                    c1ScheduledActionList.Cols[COL_Code].Width = Convert.ToInt32(nWidth * 0.17 - 5);
                    c1ScheduledActionList.Cols[COL_Description].Width = Convert.ToInt32(nWidth * 0.2 - 5);
                    c1ScheduledActionList.Cols[COL_TemplateName].Width = Convert.ToInt32(nWidth * 0.15 - 5);
                    c1ScheduledActionList.Cols[COL_DefaultNextAction].Width = Convert.ToInt32(nWidth * 0.15 - 5);
                    c1ScheduledActionList.Cols[COL_FollowupActionDays].Width = Convert.ToInt32(nWidth * 0.16 - 5);
                    c1ScheduledActionList.Cols[COL_SystemType].Width = 0;
                    c1ScheduledActionList.EndUpdate();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtScheduledAction != null)
                {
                    dtScheduledAction.Dispose();
                    dtScheduledAction = null;
                }
                if (oFollowUpCode != null) { oFollowUpCode.Dispose(); oFollowUpCode = null; }
                c1ScheduledActionList.EndUpdate();
            }
        }

        private void DesignGrid(C1.Win.C1FlexGrid.C1FlexGrid c1ScheduledActionList)
        {

            c1ScheduledActionList.SetData(0, COL_To, "Merge To");
            c1ScheduledActionList.SetData(0, COL_Select, "Merge From");
            c1ScheduledActionList.SetData(0, COL_ID, "ID");
            c1ScheduledActionList.SetData(0, COL_Code, "Code");
            c1ScheduledActionList.SetData(0, COL_Description, "Description");
            c1ScheduledActionList.SetData(0, COL_TemplateName, "Template");
            c1ScheduledActionList.SetData(0, COL_DefaultNextAction, "Default Next Action");
            c1ScheduledActionList.SetData(0, COL_FollowupActionDays, "Default Next Action Days");
            c1ScheduledActionList.SetData(0, COL_SystemType, "System Type");
            for (int i = COL_ID; i <= c1ScheduledActionList.Cols.Count - 1; i++)
            {
                c1ScheduledActionList.Cols[i].AllowEditing = false;
            }

            c1ScheduledActionList.Cols[COL_To].Visible = true;
            c1ScheduledActionList.Cols[COL_Select].Visible = true;
            c1ScheduledActionList.Cols[COL_ID].Visible = false;
            c1ScheduledActionList.Cols[COL_To].DataType = typeof(bool);
            c1ScheduledActionList.Cols[COL_Select].DataType = typeof(bool);
            btnClear.Visible = false;
            TxtMergeScheduledAction.Visible = false;

            int _width = Width;

            c1ScheduledActionList.Rows.Fixed = 1;
        }

        private void c1ScheduledActionList_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (c1ScheduledActionList.Rows.Count > 0)
                {
                    if (e.Col == COL_To)
                    {
                        for (int i = 1; i <= c1ScheduledActionList.Rows.Count - 1; i++)
                        {
                            if (c1ScheduledActionList.GetCellCheck(i, COL_To) == C1.Win.C1FlexGrid.CheckEnum.Checked & c1ScheduledActionList.RowSel == i)
                            {
                                c1ScheduledActionList.SetCellCheck(i, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                c1ScheduledActionList.Rows[i].AllowEditing = false;
                                TxtMergeScheduledAction.Text = Convert.ToString(c1ScheduledActionList.GetData(i, COL_Code));
                            }
                            else
                            {
                                c1ScheduledActionList.Rows[i].AllowEditing = true;
                                c1ScheduledActionList.SetCellCheck(i, COL_To, C1.Win.C1FlexGrid.CheckEnum.Unchecked);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1ScheduledActionList_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            try
            {
                if ((c1ScheduledActionList.Rows.Count > 1))
                {
                    //Code changes for maintaining the selected row after sorting 
                    int _index = c1ScheduledActionList.FindRow(Convert.ToString(_id), 0, COL_ID, false, false, false);
                    c1ScheduledActionList.ShowCell(_index, COL_Code);
                    c1ScheduledActionList.Row = _index;
                    c1ScheduledActionList.Select();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
        Int64 _id;
        private void c1ScheduledActionList_BeforeSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            if ((c1ScheduledActionList.Rows.Count > 1))
            {
                _id =Convert.ToInt64(c1ScheduledActionList.Rows[c1ScheduledActionList.RowSel][COL_ID]);
            }
        }

        private void c1ScheduledActionList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }

        
    }
}
