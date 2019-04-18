using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using gloPatientPortal.Classes;
using C1.Win.C1FlexGrid;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

namespace gloPatientPortal.UserControls
{
    public partial class UcHealthFrmQueAns : UserControl
    {
        #region Variables declaration
        public bool IsPatientHistoryRelated = false;
        public bool _fillCategoryOnRadiobuttonchange = true;
        public int nRbtType = 0;
        //Int16 _nRbttype = 0;
        string _strConnectionString = string.Empty;
        long _nLoginID;
        long _nLibId;
        bool _IsModify = false;
        bool _IsAnswerModify = false;
        bool _IsAssociatedAnswerModify = false;
        bool _IsDuplicateAnswer = false;
        long _nPFAnswerId;
        long _nPFAnsLibId;
        string _sPFAnsAnswerLabel;
        bool _bIsFollowedQuestion;
        string _sPFAnsfollowedQuestionLabel;
        long _nPFAnsOrderId;
        long _nPFAnsHistoryItemId;
        long _nPFAnsOtherId;

        //C1Question columns
        int COL_PFQLibId = 0;
        int COL_PFQHistoryID = 1;
        int COL_PFQCategoryID = 2;
        int COL_PFQPubName = 3;
        int COL_PFQCatType = 4;
        int COL_PFQPreText = 5;
        int COL_PFQPostText = 6;
        int COL_PFQAnswerType = 7;
        int COL_PFQUserId = 8;
        int COL_PFQHistoryItem = 9;
        int COL_PFQIsMandatory = 10;
        int COL_PFQGenderType = 11;
        int COL_PFActiveInActive = 12;
        int COL_PFQQuestionType = 13;
        int COL_PFQHistoryCategory = 14;
        int COL_PFQHistoryCategoryID = 15;
        int COL_bIsPatientHistoryRelated = 16;
        int COL_RbtType = 17;
        int COL_GroupType = 18;
        int COL_PFQDelete = 19;
        int COL_PFQImgAnswerType = 20;
       
        //end C1Question

        //C1Answer columns
        int COL_PFAAnswerId = 0;
        int COL_PFALibId = 1;
        int COL_PFAAnswerLable = 2;
        int COL_PFAIsFollwedText = 3;
        int COL_PFAFollowedQues = 4;
        int COL_PFAAnsType = 5;
        int COL_PFAnAnsType = 6;
        int COL_PFAnOrderId = 7;
        int COL_PFAnHistoryItemId = 8;
        int COL_PFAnOtherID = 9;
        int COL_PFAnsDelete = 10;
        //end C1Answer

        //C1AnswerAssociated 
        int COLAS_PFAnswerID = 0;
        int COLAS_PFLibId = 1;
        int COLAS_PFAnsLabel = 2;
        int COLAS_PFIsFollwedText = 3;
        int COLAS_PFFollwText = 4;
        int COLAS_PFAnsType = 5;
        int COLAS_PFnAnsType = 6;
        int COLAS_PFnOrderId = 7;
        int COLAS_PFnHistoryItemId = 8;
        int COLAS_PFnOtherID = 9;
        int COLAS_PFDelete = 10;
        //end C1AnswerAssociated
        #endregion

        bool _IsNewModify = false;
        bool showConfirmation = true;
        
        public bool IsNewModify
        {
            get
            {
                return _IsNewModify;
            }
            set
            {
                _IsNewModify = value;
            }
        }

        #region Counstroctors
        public UcHealthFrmQueAns()
        {
            InitializeComponent();
        }

        public UcHealthFrmQueAns(string strConnectionString, long nLoginID)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
        }
        #endregion

        #region events
        private void UcHealthFrmQueAns_Load(object sender, EventArgs e)
        {
            // Keep this for Event Binding for History Related & History Non related Start
            this.rdoHistory.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged);
            this.rdoNonHistory.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged);
            this.rdoROS.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged); 
            //rdoHistory.Checked = true;
            // End
            pnlGroup.Dock = DockStyle.None;
            pnlAnswer.Dock = DockStyle.None;
            ts_ShowHide.Text = "Show";
            lblHeading.Text = "Online Patient Form - Questions";

            this.cmbHisCategory.SelectedIndexChanged -= new EventHandler(cmbHisCategory_SelectedIndexChanged);
            FillCategory();
            this.cmbHisCategory.SelectedIndexChanged += new EventHandler(cmbHisCategory_SelectedIndexChanged);
            this.cmbAnsAllergies.SelectedIndexChanged -= new EventHandler(cmbAnsAllergies_SelectedIndexChanged);
            this.cmbAnsFamilyMember.SelectedIndexChanged -= new EventHandler(cmbAnsFamilyMember_SelectedIndexChanged);
            FillCombo();
            this.cmbAnsAllergies.SelectedIndexChanged += new EventHandler(cmbAnsAllergies_SelectedIndexChanged);
            this.cmbAnsFamilyMember.SelectedIndexChanged += new EventHandler(cmbAnsFamilyMember_SelectedIndexChanged);
            DesignGrid();
         
            if (c1Question.Rows.Count>1)
            {
                if (c1Question.RowSel>0)
                {
                    if (_nLibId == 0)
                    {
                        _nLibId = Convert.ToInt64(((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource)).Row.ItemArray[0]);
                       
                        DesignAssociatedAnswersGrid();
                        pnlAnswer.Visible = true;
                        pnlAnswer.Dock = DockStyle.Bottom;
                    }
                }
            }
           
            FillGenderType();
            FillAnswerType();

            cmbHisCategory.Focus();
        }

        private void cmbHisCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCategory = Convert.ToString(((System.Data.DataRowView)(cmbHisCategory.SelectedItem)).Row.ItemArray[1]).Trim();
            if (strCategory != "Select")
            {

                this.cmbHisItem.SelectedIndexChanged -= new EventHandler(cmbHisItem_SelectedIndexChanged);
                FillHistoryItmes(Convert.ToInt64(cmbHisCategory.SelectedValue));
                this.cmbHisItem.SelectedIndexChanged += new EventHandler(cmbHisItem_SelectedIndexChanged);
                this.cmbAnsHistoryItem.SelectedIndexChanged -= new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);
                FillAnsHistoryItmes(Convert.ToInt64(cmbHisCategory.SelectedValue));
                this.cmbAnsHistoryItem.SelectedIndexChanged += new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);

                txtPublishNm.Text = string.Empty;
                //ValidateHistoryCategoryControlsVisibility();
            }
            else
            {
                cmbHisItem.DataSource = null;
                cmbHisItem.Items.Clear();
                cmbHisItem.DropDownStyle = ComboBoxStyle.Simple;
                cmbHisItem.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbAnsHistoryItem.DataSource = null;
                cmbAnsHistoryItem.Items.Clear();
                cmbAnsHistoryItem.DropDownStyle = ComboBoxStyle.Simple;
                cmbAnsHistoryItem.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            ValidateHistoryCategoryControlsVisibility();
        }

        public void SetModifyEnabled()
        {
            bool IsModifyToEnable = false;

            if (c1Question.Rows.Count > 1)
            {
                IsModifyToEnable = true;
            }
            else
            {
                IsModifyToEnable = false;
            }

            frmHealthForm objfrmHealthForm = (frmHealthForm)this.ParentForm;
            objfrmHealthForm.SetModifyEnabled(IsModifyToEnable);
        }

        private void FillCombo()
        {
            clsHealthForm oclsHistory = null;
            DataTable dtAllergies = null;
            DataTable dtRelations = null;
            try
            {
                //Load history items
                oclsHistory = new clsHealthForm();

                dtAllergies = oclsHistory.FillAllergies(_strConnectionString);
                cmbAnsAllergies.DataSource = null;
                cmbAnsAllergies.Items.Clear();

                if (dtAllergies != null && dtAllergies.Rows.Count > 0)
                {

                    DataRow dr = dtAllergies.NewRow();
                    dr["nCategoryID"] = -1;
                    dr["sDescription"] = "Select";

                    dtAllergies.Rows.InsertAt(dr, 0);

                    cmbAnsAllergies.DataSource = dtAllergies.DefaultView;
                    cmbAnsAllergies.DisplayMember = "sDescription";
                    cmbAnsAllergies.ValueMember = "nCategoryID";
                    cmbAnsAllergies.SelectedIndex = 0;
                }

                dtRelations = oclsHistory.FillRelations(_strConnectionString, 0);
                cmbAnsFamilyMember.DataSource = null;
                cmbAnsFamilyMember.Items.Clear();

                if (dtRelations != null && dtRelations.Rows.Count > 0)
                {

                    DataRow dr = dtRelations.NewRow();
                    dr["ID"] = -1;
                    dr["Description"] = "Select";

                    dtRelations.Rows.InsertAt(dr, 0);

                    cmbAnsFamilyMember.DataSource = dtRelations.DefaultView;
                    cmbAnsFamilyMember.DisplayMember = "Relation";
                    cmbAnsFamilyMember.ValueMember = "ID";
                    cmbAnsFamilyMember.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
                if (dtAllergies != null)
                {
                    dtAllergies.Dispose();
                    dtAllergies = null;
                }
                if (dtRelations != null)
                {
                    dtRelations.Dispose();
                    dtRelations = null;
                }
            }
        }

        private void cmbHisItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHisItem.SelectedItem != null)
            {
                string strCategory = Convert.ToString(((System.Data.DataRowView)(cmbHisItem.SelectedItem)).Row.ItemArray[1]).Trim();
                if (strCategory != "Select")
                    txtPublishNm.Text = strCategory;
                else
                    txtPublishNm.Text = string.Empty;
            }
        }

        private void ts_Save_Click(object sender, EventArgs e)
        {
            if (_IsModify)
            {
                DialogResult _result = CheckQuestionFormAssociation();
                if (_result == DialogResult.Yes)
                {
                    SaveQuestion();
                    //SaveAnswers();
                    c1Question.Enabled = true;
                    IsNewModify = false;
                    return;
                }
                else if (_result == DialogResult.No)
                {
                    pnlGroup.Dock = DockStyle.None;
                    DesignGrid();
                    DesignAssociatedAnswersGrid();
                    ClearAll();
                    c1Question.Enabled = true;
                    IsNewModify = false;
                    return;
                }
                else if (_result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            { 
            SaveQuestion();
            //SaveAnswers();
            c1Question.Enabled = true;
            IsNewModify = false;
            }
        }

        private void c1Question_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (c1Question.Rows.Count > 1 && c1Question.RowSel != -1)
            {
                c1Question.Enabled = false;
                cmbHisCategory.Enabled = true;
                IsNewModify = true;
                ts_Save.Text = "Save";
                BindQuestion();
                DesignAssociatedAnswersGrid();
                BindAssociatedAnswers();
                DisableHistoryCategory();
            }
            else
            {
                MessageBox.Show("No record exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void DisableHistoryCategory()
        {
            clsHealthForm oclsHealthForm = new clsHealthForm();
            Int32 nQuestionAccociated = 1;
            DataRowView drv = ((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource));
            if (drv != null && drv.Row.Table.Rows.Count > 0)
            {
                nQuestionAccociated = oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[0]));
            }
            if (nQuestionAccociated == 0)
            {
                grpHistoryRelation.Enabled = false;
                cmbHisCategory.Enabled = false;
            }
            else
            {
               
                grpHistoryRelation.Enabled = true;
            }
        }

        public DialogResult CheckQuestionFormAssociation()
        {
            DialogResult Result=DialogResult.No;
            clsHealthForm oclsHealthForm = null;
            try
            {
                if (c1Question.Rows.Count > 1)
                {
                    DataRowView drv = ((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource));
                    if (drv != null && drv.Row.Table.Rows.Count > 0)
                    {
                        oclsHealthForm = new clsHealthForm();
                        DataTable dt_AssociatedForm = oclsHealthForm.GetQuestionAssociatedForm(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[0]), true);

                        if (dt_AssociatedForm != null && dt_AssociatedForm.Rows.Count > 0)
                        {
                            string sformNames = string.Empty;

                            for (int i = 0; i < dt_AssociatedForm.Rows.Count; i++)
                            {
                                if (sformNames == "")
                                {
                                    sformNames = (i + 1).ToString() + ". " + dt_AssociatedForm.Rows[i]["sFormName"].ToString();
                                }
                                else
                                {
                                    sformNames += "\n\r" + (i + 1).ToString() + ". " + dt_AssociatedForm.Rows[i]["sFormName"].ToString();
                                }
                            }

                            string message = "This question has been associated to the following active forms. Any new changes need to re-publish the form to get this effective on the portal.\n\r" + sformNames + "\n\r\n\rYes : Save changes and Deactivates form (need to re-publish).\n\rNo  : Discard current changes.\n\r\n\r* Re-Publish: (Online Patient Form >> Modify >> Save & Preview >> Publish)";
                            Result = MessageBox.Show(message, "gloEMRAdmin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Result = DialogResult.Yes;
                        }
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }

            return Result;
        }

        private void ts_ShowHide_Click(object sender, EventArgs e)
        {
            if (pnlQuestion.Visible == true)
            {
                if (ts_ShowHide.Text == "Show")
                {
                    pnlGroup.Dock = DockStyle.Right;
                    ts_ShowHide.Text = "&Close";
                }
                else
                {
                    pnlGroup.Dock = DockStyle.None;
                    //pnlAnswer.Dock = DockStyle.None;
                    ts_ShowHide.Text = "Show";
                    lblHeading.Text = "Online Patient Form - Questions";
                }
            }
            else//Answers 
            {
                if (ts_ShowHide.Text == "Show")
                {
                    pnlGroup.Dock = DockStyle.Right;
                    ts_ShowHide.Text = "&Close";
                    pnlAns.Visible = false;
                    ResetAnswers();
                    validateonanswers();
                }
                else
                {
                    pnlGroup.Dock = DockStyle.None;
                    pnlAnswer.Dock = DockStyle.None;
                    ts_ShowHide.Text = "Show";
                    pnlAns.Visible = false;
                    ResetAnswers();
                    validateonanswers();
                }
            }
            c1Question.Enabled = true;
            IsNewModify = false;
            ClearAll();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void ts_AddAnswer_Click(object sender, EventArgs e)
        {
            DataView dv = ((System.Data.DataView)(c1AnswerAssociated.DataSource));
            if (dv != null && dv.Table.Rows.Count > 0)
            {
                DialogResult Result;
                Result = MessageBox.Show("If you add new answers it will lose old answers, \n do you really want to add answer?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (Result == DialogResult.Yes)
                {
                    if (rdManual.Checked)
                    {
                        pnlAnsHistoryItem.Visible = false;
                    }
                    else
                    {
                        pnlAnsHistoryItem.Visible = true;
                    }
                    //pnlQuestion.Visible = false;
                    FillAnswerType();
                    ResetAnswers();
                    _IsAssociatedAnswerModify = false;
                    _nPFAnswerId = 0;

                    c1AnswerAssociated.DataSource = null;
                    c1AnswerAssociated.Rows.Count = 1;
                    //EnableAnswerType(true);
                }
            }
            else
            {
                if (rdManual.Checked)
                {
                    pnlAnsHistoryItem.Visible = false;
                }
                else
                {
                    pnlAnsHistoryItem.Visible = true;
                }
                //pnlQuestion.Visible = false;
                FillAnswerType();
                ResetAnswers();
                _IsAssociatedAnswerModify = false;
                _nPFAnswerId = 0;
                //EnableAnswerType(true);
            }

        }

        int LastselectedIndex = -1;
        Boolean IsPreviousMultioption = false;
        Boolean CallAnswerTypeValidation = true;
        private void cmbAnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CallAnswerTypeValidation == false)
            {
                return;
            }
            if (LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
            {
                IsPreviousMultioption = false;
            }
            else if (LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))// || LastselectedIndex == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox))
            {
                IsPreviousMultioption = true;
            }

            if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
            {
                if (IsPatientHistoryRelated == true)
                {
                    pnlQQuesType.Visible = false;
                    pnlHistoryItem.Visible = true;
                }
                else
                {
                    pnlQQuesType.Visible = false;
                    pnlHistoryItem.Visible = false; // should not visible for non History
                }
            }
            else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
            {
                if (IsPatientHistoryRelated == true)
                {
                    pnlQQuesType.Visible = true;
                    if (rdHistoryItem.Checked)
                    {
                        pnlHistoryItem.Visible = false;
                    }
                    else
                    {
                        pnlHistoryItem.Visible = true;
                    }
                }
                else
                {
                    pnlQQuesType.Visible = false;
                    pnlHistoryItem.Visible = false;
                }
            }
            ShowHideAnswerPanels("Add", true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
        }

        private void rbYesFollowQ_CheckedChanged(object sender, EventArgs e)
        {

            if (rbYesFollowQ.Checked == true)
            {
                rbYesFollowQ.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbYesFollowQ.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }

            if (rbYesFollowQ.Checked)
            {
                pnlFollwedLabel.Visible = true;
                pnlFollwedYesNo.BringToFront();
                //pnlAnswerGrid.Visible = true;
                //DesignAnswerGrid();
            }
        }

        private void rbNoFollowQ_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoFollowQ.Checked == true)
            {
                rbNoFollowQ.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbNoFollowQ.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
            if (rbNoFollowQ.Checked)
            {
                pnlFollwedLabel.Visible = false;
                txtFollowedQLab.Text = string.Empty;
            }
        }

        private void c1AnswerAssociated_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //BindAssociatedAnswers();
        }

        private void lnkAddOpt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private bool ValidateOption(bool bIsFromModify)
        {
            if (pnlAns.Visible)
            {
                if (c1Answers.DataSource == null)
                {
                    if (pnlAnsHistoryItem.Visible)
                    {
                        //if (c1Answers.Rows.Count <= 2)
                        //{
                        if ((cmbAnsHistoryItem.SelectedIndex == 0 || cmbAnsHistoryItem.SelectedIndex == -1)&& rdoHistory.Checked)
                        {
                            MessageBox.Show("Please select history item in answer section.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAnsHistoryItem.Focus();
                            return false;
                        }

                        if ((cmbAnsHistoryItem.SelectedIndex == 0 || cmbAnsHistoryItem.SelectedIndex == -1) && rdoROS.Checked)
                        {
                            MessageBox.Show("Please select ROS item in answer section.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAnsHistoryItem.Focus();
                            return false;
                        }
                        //}
                    }

                    if (pnlAnsFamilyMember.Visible)
                    {
                        if (cmbAnsFamilyMember.SelectedIndex == 0 || cmbAnsFamilyMember.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please select family member.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAnsFamilyMember.Focus();
                            return false;
                        }
                    }
                    if (pnlAnsReaction.Visible)
                    {
                        if (cmbAnsAllergies.SelectedIndex == 0 || cmbAnsAllergies.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please select reaction.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbAnsAllergies.Focus();
                            return false;
                        }
                    }

                    if (!_IsModify)
                    {
                        if (c1Answers.Rows.Count <= 2)
                        {
                            if (txtAnswerLabel.Text == "" || txtAnswerLabel.Text.Trim() == "")
                            {
                                MessageBox.Show("Please enter answer label.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAnswerLabel.Focus();
                                return false;
                            }
                        }

                    }
                    else
                    {
                        if (txtAnswerLabel.Text == "" || txtAnswerLabel.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter answer label.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAnswerLabel.Focus();
                            return false;
                        }
                    }

                    if (txtAnswerLabel.Text != "" && txtAnswerLabel.TextLength > 50)
                    {
                        MessageBox.Show("Answer label length should be maximum 50 characters.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAnswerLabel.Focus();
                        return false;
                    }

                    if (txtAnswerLabel.Text.Trim() != "")
                    {
                        if (!clsGeneral.ContainsHtml(txtAnswerLabel.Text.Trim(), "Answer label"))
                        {
                            txtAnswerLabel.Focus();
                            return false;
                        }
                    }


                    if (rbYesFollowQ.Checked)
                    {
                        if (txtFollowedQLab.Text == string.Empty || txtFollowedQLab.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter followed question label.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFollowedQLab.Focus();
                            return false;
                        }
                        if (txtFollowedQLab.Text.Trim() != "")
                        {
                            if (!clsGeneral.ContainsHtml(txtFollowedQLab.Text.Trim(), "followed question label"))
                            {
                                txtFollowedQLab.Focus();
                                return false;
                            }
                        }

                    }
                    if (IsPatientHistoryRelated)
                    {
                        if (rdHistoryItem.Checked)
                        {
                            for (int i = 1; i < c1Answers.Rows.Count; i++)
                            {
                                if (bIsFromModify)
                                {
                                    if (i == c1Answers.RowSel)
                                        continue;
                                }
                                if (Convert.ToString(c1Answers.Rows[i][9]) == cmbAnsHistoryItem.SelectedValue.ToString())
                                {
                                    string strHistoryItem = Convert.ToString(((System.Data.DataRowView)(cmbAnsHistoryItem.SelectedItem)).Row.ItemArray[1]).Trim();
                                    MessageBox.Show("Histoty Item '" + strHistoryItem + "' is already associated to Answer label '" + Convert.ToString(c1Answers.Rows[i][3]) + "'.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }

                        if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
                        {
                            for (int i = 1; i < c1Answers.Rows.Count; i++)
                            {
                                if (bIsFromModify)
                                {
                                    if (i == c1Answers.RowSel)
                                        continue;
                                }
                                if (nQuestionType==1)
                                {
                                    if (Convert.ToString(c1Answers.Rows[i][10]) == cmbAnsFamilyMember.SelectedValue.ToString())
                                    {
                                        string strRelation = Convert.ToString(((System.Data.DataRowView)(cmbAnsFamilyMember.SelectedItem)).Row.ItemArray[1]).Trim();
                                        MessageBox.Show("Family member '" + strRelation + "' is already associated to Answer label '" + Convert.ToString(c1Answers.Rows[i][3]) + "'.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtAnswerLabel.Focus();
                                        return false;
                                    }
                                }
                            }
                        }
                        if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
                        {

                            for (int i = 1; i < c1Answers.Rows.Count; i++)
                            {
                                if (bIsFromModify)
                                {
                                    if (i == c1Answers.RowSel)
                                        continue;
                                }
                                if (nQuestionType==1)
                                {
                                    if (Convert.ToString(c1Answers.Rows[i][10]) == cmbAnsAllergies.SelectedValue.ToString())
                                    {
                                        string strReaction = Convert.ToString(((System.Data.DataRowView)(cmbAnsAllergies.SelectedItem)).Row.ItemArray[1]).Trim();
                                        MessageBox.Show("Reaction '" + strReaction + "' is already associated to Answer label '" + Convert.ToString(c1Answers.Rows[i][3]) + "'.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtAnswerLabel.Focus();
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 1; i < c1Answers.Rows.Count; i++)
                    {
                        if (bIsFromModify)
                        {
                            if (i == c1Answers.RowSel)
                                continue;
                        }
                        if (Convert.ToString(c1Answers.Rows[i][3]) == txtAnswerLabel.Text.ToString())
                        {
                            MessageBox.Show("Answer label already exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAnswerLabel.Focus();
                            return false;
                        }
                    }

                }


            }


            return true;
        }

        private void lnkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //DialogResult Result;
            //Result = MessageBox.Show("It will lose unsaved answers, \n do you really want to reset?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //if (Result == DialogResult.Yes)
            //{
            //    ResetAnswers();
            //    EnableAnswerType(true);
            //}
        }

        private void c1Answers_AfterEdit(object sender, RowColEventArgs e)
        {
            //_IsDuplicateAnswer = false;
            //DataView dv = ((System.Data.DataView)(c1Answers.DataSource));
            //DataTable dt = null;
            //DataRow[] results = null;
            //if (dv != null && dv.Table.Rows.Count > 0)
            //{
            //    dt = dv.ToTable();
            //    results = dt.Select("[Answer Label] = '" + Convert.ToString(((System.Data.DataRowView)(c1Answers.Rows[e.Row].DataSource)).Row.ItemArray[2]) + "' AND [nPFAnswerId] <> " + Convert.ToInt64(((System.Data.DataRowView)(c1Answers.Rows[e.Row].DataSource)).Row.ItemArray[0]));
            //    if (results.Count() > 0)
            //    {
            //        MessageBox.Show("Record already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        _IsDuplicateAnswer = true;
            //    }
            //}
        }

        private void c1Question_Click(object sender, EventArgs e)
        {
            clsHealthForm oclsHealthForm = null;
            try
            {
                if (c1Question.Rows.Count > 1)
                {
                    DataRowView drv = ((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource));
                    if (drv != null && drv.Row.Table.Rows.Count > 0)
                    {
                        if (c1Question.ColSel == 0)
                        {
                            DialogResult Result;
                            Result = MessageBox.Show("Do you want to delete question?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (DialogResult.Yes == Result)
                            {
                                oclsHealthForm = new clsHealthForm();
                                Int32 nQuestionAccociated = oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[0]));

                                if (nQuestionAccociated == 1)
                                {
                                    oclsHealthForm = new clsHealthForm();
                                    oclsHealthForm.DeleteQuestion(_strConnectionString, "PF_DeleteQuestion", Convert.ToInt64(drv.Row.ItemArray[0]), "Q");
                                    DesignGrid();
                                    DesignAssociatedAnswersGrid();
                                }
                                else
                                {
                                    if (Convert.ToBoolean(drv.Row.ItemArray[12]))
                                    {
                                        Result = MessageBox.Show("Question already associated in build form, do you want to Inactivate?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (DialogResult.Yes == Result)
                                        {
                                            oclsHealthForm = new clsHealthForm();
                                            oclsHealthForm.DeleteQuestion(_strConnectionString, "PF_DeleteQuestion", Convert.ToInt64(drv.Row.ItemArray[0]), "Q");
                                            DesignGrid();
                                            DesignAssociatedAnswersGrid();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Question is already associated in build form.\nQuestion can't be deleted.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                        else if (c1Question.ColSel == 13)
                        {
                            oclsHealthForm = new clsHealthForm();
                            oclsHealthForm.UpdateStatus(_strConnectionString, "PF_UpdateStatus", Convert.ToInt64(drv.Row.ItemArray[0]), Convert.ToBoolean(drv.Row.ItemArray[12]));
                        }
                        else
                        {
                            //BindQuestion();
                            _nLibId = Convert.ToInt64(((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource)).Row.ItemArray[0]);
                            pnlGroup.Dock = DockStyle.None;
                            DesignAssociatedAnswersGrid();
                        }
                        if (Convert.ToInt16(drv.Row.ItemArray[COL_PFQAnswerType]) != -1)
                        {
                            if (Convert.ToInt16(drv.Row.ItemArray[COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || Convert.ToInt16(drv.Row.ItemArray[COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                            {
                                pnlAnswer.Dock = DockStyle.None;
                            }
                            else
                            {
                                if (_nLibId == 0)
                                {
                                    _nLibId = Convert.ToInt64(((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource)).Row.ItemArray[0]);
                                    DesignAssociatedAnswersGrid();
                                }

                                pnlAnswer.Visible = true;
                                pnlAnswer.Dock = DockStyle.Bottom;
                            }
                        }
                        else
                        {
                            c1AnswerAssociated.Visible = false;
                            pnlAnswer.Dock = DockStyle.Bottom;
                            lblInfo.Visible = true;
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }
        }

        private void c1AnswerAssociated_Click(object sender, EventArgs e)
        {
            //clsHealthForm oclsHealthForm = null;
            //try
            //{
            //    if (c1AnswerAssociated.ColSel == 0)
            //    {
            //        if (c1AnswerAssociated.Rows.Count > 1)
            //        {
            //            DataRowView drv = ((System.Data.DataRowView)(c1AnswerAssociated.Rows[c1AnswerAssociated.RowSel].DataSource));
            //            if (drv != null && drv.Row.Table.Rows.Count > 0)
            //            {
            //                DialogResult Result;
            //                Result = MessageBox.Show("Do you want to delete Answer?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //                if (DialogResult.Yes == Result)
            //                {
            //                    oclsHealthForm = new clsHealthForm();
            //                    //Check if Question-Answer associated 
            //                    if (oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[1])) == 0)
            //                    {
            //                        Result = MessageBox.Show("Answer already associated.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }//End
            //                    else
            //                    {
            //                        oclsHealthForm = new clsHealthForm();
            //                        oclsHealthForm.DeleteAnswer(_strConnectionString, "PF_DeleteAnswer", Convert.ToInt64(drv.Row.ItemArray[0]), Convert.ToInt64(drv.Row.ItemArray[1]));
            //                        DesignAssociatedAnswersGrid();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //finally
            //{
            //    if (oclsHealthForm != null)
            //        oclsHealthForm = null;
            //}
        }

        //private void c1Answers_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (c1Answers.Rows.Count > 1)
        //        {
        //            DataRowView drv = ((System.Data.DataRowView)(c1Answers.Rows[c1Answers.RowSel].DataSource));
        //            if (drv != null && drv.Row.Table.Rows.Count > 0)
        //            {
        //                pnlQuestion.Visible = false;
        //                pnlAnswers.Visible = true;
        //                FillAnswerType();

        //                this.cmbAnswerType.SelectedIndexChanged -= new EventHandler(cmbAnswerType_SelectedIndexChanged);
        //                cmbAnswerType.SelectedItem = Enum.ToObject(typeof(clsHealthForm.QuestionType), Convert.ToInt32(drv.Row.ItemArray[6]));
        //                this.cmbAnswerType.SelectedIndexChanged += new EventHandler(cmbAnswerType_SelectedIndexChanged);
        //                cmbAnswerType.Enabled = false;

        //                txtAnswerLabel.Text = Convert.ToString(drv.Row.ItemArray[2]);

        //                if (Convert.ToBoolean(drv.Row.ItemArray[3]) == true)
        //                {
        //                    rbYesFollowQ.Checked = true;
        //                    txtFollowedQLab.Text = Convert.ToString(drv.Row.ItemArray[4]);
        //                }
        //                else
        //                {
        //                    rbNoFollowQ.Checked = true;
        //                    txtFollowedQLab.Text = string.Empty;
        //                }
        //                ShowHideAnswerPanels("Modify", false);
        //                _IsAssociatedAnswerModify = true;
        //                _nPFAnswerId = Convert.ToInt64(drv.Row.ItemArray[0]);

        //                if (Convert.ToInt32(drv.Row.ItemArray[6]) == 2 || Convert.ToInt32(drv.Row.ItemArray[6]) == 3 || Convert.ToInt32(drv.Row.ItemArray[6]) == 4)
        //                    btnModify.Visible = true;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private void btnModify_Click(object sender, EventArgs e)
        //{
        //    ModifyAnswerGrid();
        //}
        #endregion

        #region Methods
        /// <summary>
        //Fill history category
        /// <summary>
        private void FillCategory(string _filtertype = "History")
        {
            clsHealthForm oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                //Load history category
                oclsHistory = new clsHealthForm();

                dtHisCategory = oclsHistory.FillControls(_strConnectionString, _filtertype);
                if (dtHisCategory != null && dtHisCategory.Rows.Count > 0)
                {
                    DataRow dr = dtHisCategory.NewRow();
                    dr["nCategoryId"] = -1;
                    dr["sDescription"] = "Select";

                    dtHisCategory.Rows.InsertAt(dr, 0);

                    cmbHisCategory.DataSource = dtHisCategory.DefaultView;
                    cmbHisCategory.DisplayMember = "sDescription";
                    cmbHisCategory.ValueMember = "nCategoryID";
                    cmbHisCategory.SelectedIndex = 0;
                    dr = null;
                }
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
        }

        /// <summary>
        //Fill history items
        /// <summary>
        private void FillHistoryItmes(long nCategoryId)
        {
            clsHealthForm oclsHistory = null;

            try
            {
                //Load history items Int16 _nRbttype
                oclsHistory = new clsHealthForm();
                DataTable dtHisItems = null;
                //if (rdoHistory.Checked)
                //{
                //    dtHisItems = oclsHistory.FillHistoryItems(_strConnectionString, nCategoryId);
                    
                //}
                //else if (rdoROS.Checked)
                //{
                //    dtHisItems = oclsHistory.FillROSItems(_strConnectionString, nCategoryId);
                    
                //}


                if (nRbtType == 1)
                {
                    dtHisItems = oclsHistory.FillHistoryItems(_strConnectionString, nCategoryId);

                }
                else if (nRbtType == 3)
                {
                    dtHisItems = oclsHistory.FillROSItems(_strConnectionString, nCategoryId);
                    dtHisItems.Columns[0].ColumnName = "nHistoryID";

                }
                cmbHisItem.DataSource = null;
                cmbHisItem.Items.Clear();
                if (dtHisItems != null && dtHisItems.Rows.Count > 0)
                {
                    DataRow dr = dtHisItems.NewRow();
                    dr["nHistoryID"] = -1;
                    dr["sDescription"] = "Select";

                    dtHisItems.Rows.InsertAt(dr, 0);

                    cmbHisItem.DataSource = dtHisItems.DefaultView;
                    cmbHisItem.DisplayMember = "sDescription";
                    cmbHisItem.ValueMember = "nHistoryID";
                    cmbHisItem.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
        }

      
        private void FillAnsHistoryItmes(long nCategoryId)
        {
            clsHealthForm oclsHistory = null;

            try
            {
                //Load history items
                oclsHistory = new clsHealthForm();
                DataTable dtAnsHistoryItem = null;
                //if (rdoHistory.Checked)
                //{
                //     dtAnsHistoryItem = oclsHistory.FillHistoryItems(_strConnectionString, nCategoryId);

                //}
                //else if (rdoROS.Checked)
                //{
                //    dtAnsHistoryItem = oclsHistory.FillROSItems(_strConnectionString, nCategoryId);

                //}
                if (nRbtType == 1)
                {
                    dtAnsHistoryItem = oclsHistory.FillHistoryItems(_strConnectionString, nCategoryId);

                }
                else if (nRbtType == 3)
                {
                    dtAnsHistoryItem = oclsHistory.FillROSItems(_strConnectionString, nCategoryId);
                    dtAnsHistoryItem.Columns[0].ColumnName = "nHistoryID";

                }
             

                cmbAnsHistoryItem.DataSource = null;
                cmbAnsHistoryItem.Items.Clear();


                if (dtAnsHistoryItem != null && dtAnsHistoryItem.Rows.Count > 0)
                {

                    DataRow dr = dtAnsHistoryItem.NewRow();
                    dr["nHistoryID"] = -1;
                    dr["sDescription"] = "Select";

                    dtAnsHistoryItem.Rows.InsertAt(dr, 0);

                    cmbAnsHistoryItem.DataSource = dtAnsHistoryItem.DefaultView;
                    cmbAnsHistoryItem.DisplayMember = "sDescription";
                    cmbAnsHistoryItem.ValueMember = "nHistoryID";
                    cmbAnsHistoryItem.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
        }
        /// <summary>
        //Clear controls
        /// <summary>
        public void ClearAll()
        {
            cmbHisCategory.SelectedIndex = 0;
            cmbHisItem.DataSource = null;
            cmbHisItem.Items.Clear();
            cmbHisItem.DropDownStyle = ComboBoxStyle.Simple;
            cmbHisItem.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbAnsHistoryItem.DataSource = null;
            cmbAnsHistoryItem.Items.Clear();
            cmbAnsHistoryItem.DropDownStyle = ComboBoxStyle.Simple;
            cmbAnsHistoryItem.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbAnsAllergies.SelectedIndex = 0;
            cmbAnsFamilyMember.SelectedIndex = 0;

            txtAnswerLabel.Text = string.Empty;
            txtPublishNm.Text = string.Empty;
            txtPostText.Text = string.Empty;
            txtPreText.Text = string.Empty;
            chkIsMandatory.Checked = false;
            ts_AddAnswer.Visible = false;
            txtFollowedQLab.Text = string.Empty;
            rbNoFollowQ.Checked = true;
            pnlAnswerGrid.Visible = false;
            pnlAddCancel.Visible = true;
            pnlModifyCancel.Visible = false;

            cmbAnswerType.SelectedIndexChanged -= new EventHandler(cmbAnswerType_SelectedIndexChanged);
            cmbAnswerType.SelectedIndex = 0;
            cmbAnswerType.SelectedIndexChanged += new EventHandler(cmbAnswerType_SelectedIndexChanged);

            LastselectedIndex = -1;
            c1Answers.Clear(ClearFlags.All);
            c1Answers.DataSource = null;
            for (int i = c1Answers.Rows.Count - 1; i >= 1; i--)
            {
                c1Answers.Rows.Remove(i);
            }
            validateonanswers();

        }

        /// <summary>
        //Design grid
        /// <summary>
        private void DesignGrid()
        {
            gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
            objgloC1FlexStyle.Style(c1Question);
            clsHealthForm oclsHistory = null;
            try
            {
                oclsHistory = new clsHealthForm();
                c1Question.Cols.Count = 21;
                c1Question.SetData(0, COL_PFQLibId, "nPFLibId");
                c1Question.Cols[COL_PFQLibId].Width = 0;

                c1Question.SetData(0, COL_PFQHistoryID, "nHistoryID");
                c1Question.Cols[COL_PFQHistoryID].Width = 0;

                c1Question.SetData(0, COL_PFQCategoryID, "nCategoryID");
                c1Question.Cols[COL_PFQCategoryID].Width = 0;

                c1Question.SetData(0, COL_PFQPubName, "Publish name");
                c1Question.SetData(0, COL_PFQCatType, "Category type");
                c1Question.SetData(0, COL_PFQPreText, "Pre text");
                c1Question.SetData(0, COL_PFQPostText, "Post text");

                c1Question.SetData(0, COL_PFQAnswerType, "Answer type");
                c1Question.Cols[COL_PFQAnswerType].Width = 0;

                c1Question.SetData(0, COL_PFQUserId, "nUserId");
                c1Question.Cols[COL_PFQUserId].Width = 0;

                c1Question.SetData(0, COL_PFQHistoryItem, "History item");

                c1Question.SetData(0, COL_PFQIsMandatory, "IsMandatory");
                c1Question.Cols[COL_PFQIsMandatory].Width = 0;

                c1Question.SetData(0, COL_PFQGenderType, "nGenderType");
                c1Question.Cols[COL_PFQGenderType].Width = 0;

                c1Question.SetData(0, COL_PFQQuestionType, "nQuestionType");
                c1Question.Cols[COL_PFQQuestionType].Width = 0;

                c1Question.SetData(0, COL_PFQImgAnswerType, "Question type");
                c1Question.Cols[COL_PFQImgAnswerType].Width = 0;

                c1Question.SetData(0, COL_PFQHistoryCategory, "History category");
                //c1Question.Cols[COL_PFQHistoryCategory].Width = 0;

                c1Question.SetData(0, COL_PFQHistoryCategoryID, "History category ID");
                c1Question.Cols[COL_PFQImgAnswerType].Width = 0;

                c1Question.SetData(0, COL_bIsPatientHistoryRelated, "History Related");
                c1Question.Cols[COL_bIsPatientHistoryRelated].Width = 0;



                DataTable dt = oclsHistory.GetQuestions(_strConnectionString);
                //if (dt != null & dt.Rows.Count > 0)
                //{
                DataColumn colButton = new DataColumn("Delete", System.Type.GetType("System.String"));
                dt.Columns.Add(colButton);

                DataColumn colButton1 = new DataColumn("Answer type", System.Type.GetType("System.String"));
                dt.Columns.Add(colButton1);

                c1Question.DataSource = dt.DefaultView;
                c1Question.Cols[COL_PFQPubName].Caption = "Question label";
                c1Question.Cols[COL_bIsPatientHistoryRelated].Caption = "History related";
                c1Question.Cols[COL_PFQLibId].Width = 0;
                c1Question.Cols[COL_PFQLibId].AllowEditing = false;
                c1Question.Cols[COL_PFQHistoryID].Width = 0;
                c1Question.Cols[COL_PFQHistoryID].AllowEditing = false;
                c1Question.Cols[COL_PFQCategoryID].Width = 0;
                c1Question.Cols[COL_PFQCategoryID].AllowEditing = false;
                c1Question.Cols[COL_PFQCatType].Width = 0;
                c1Question.Cols[COL_PFQCatType].AllowEditing = false;
                c1Question.Cols[COL_PFQAnswerType].Width = 0;
                c1Question.Cols[COL_PFQAnswerType].AllowEditing = false;
                c1Question.Cols[COL_PFQUserId].Width = 0;
                c1Question.Cols[COL_PFQUserId].AllowEditing = false;
                c1Question.Cols[COL_PFQIsMandatory].Width = 0;
                c1Question.Cols[COL_PFQIsMandatory].AllowEditing = false;
                c1Question.Cols[COL_PFQGenderType].Width = 0;
                c1Question.Cols[COL_PFQGenderType].AllowEditing = false;
                //c1Question.Cols[COL_PFQQuestionType].Width = 0;
                c1Question.Cols[COL_PFQQuestionType].AllowEditing = false;
                c1Question.Cols[COL_PFQHistoryCategory].AllowEditing = false;
                c1Question.Cols[COL_PFQHistoryCategory].Caption = "Category";
                c1Question.Cols[COL_PFQHistoryCategoryID].Width = 0;

                c1Question.Cols[COL_PFQPubName].Width = 300;
                c1Question.Cols[COL_PFQPubName].AllowEditing = false;
                c1Question.Cols[COL_PFQHistoryCategory].Width = 150;
                c1Question.Cols[COL_PFQHistoryItem].Width = 160;
                c1Question.Cols[COL_PFQHistoryItem].Caption = "Item";
                c1Question.Cols[COL_PFActiveInActive].Width = 50;
                c1Question.Cols[COL_bIsPatientHistoryRelated].Width = 0;
                c1Question.Cols[COL_bIsPatientHistoryRelated].Visible = false;
                c1Question.Cols[COL_PFQImgAnswerType].Width = 100;
                c1Question.Cols[COL_PFQPreText].AllowEditing = false;
                c1Question.Cols[COL_PFQPostText].AllowEditing = false;
                c1Question.Cols[COL_PFQImgAnswerType].AllowSorting = false;
                c1Question.Cols[COL_bIsPatientHistoryRelated].AllowEditing = false;
                //c1Question.Cols[COL_bIsPatientHistoryRelated].AllowSorting = false;
                c1Question.Cols[COL_PFQHistoryItem].AllowEditing = false;

                c1Question.Cols[COL_PFActiveInActive].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;

                c1Question.Cols[COL_PFQDelete].AllowEditing = false;
                c1Question.Cols[COL_PFQDelete].Width = 48;
                c1Question.Cols[COL_PFQDelete].ImageAlign = ImageAlignEnum.CenterCenter;

              
                c1Question.Cols[COL_RbtType].Width = 0;
                c1Question.Cols[COL_RbtType].Visible = false;
                c1Question.Cols[COL_GroupType].AllowEditing = false;
                c1Question.Cols[COL_GroupType].Width = 130;
               
           

                if (dt.Rows.Count > 1)
                {
                    C1.Win.C1FlexGrid.CellStyle cStyle = c1Question.Styles.Add("Button");
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Question.GetCellRange(1, COL_PFQDelete, dt.Rows.Count, COL_PFQDelete);
                    rgReaction.Style = cStyle;
                }
                for (int i = 1; i <= dt.Rows.Count; i++)
                    c1Question.SetCellImage(i, COL_PFQDelete, imgList.Images[0]);

                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    if (c1Question.Rows[i][COL_PFQAnswerType].ToString().ToUpper() == "NULL" || c1Question.Rows[i][COL_PFQAnswerType].ToString() == "")
                    {
                        c1Question.SetData(i, COL_PFQImgAnswerType, "");
                    }
                    else
                    {
                        if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.Textbox))
                        {
                            c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[0]);
                        }
                        if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                        {
                            c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[1]);
                        }
                        if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox))
                        {
                            c1Question.SetCellImage(i, COL_PFQImgAnswerType, imgList.Images[1]);
                        }
                        if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.Radio))
                        {
                            c1Question.SetCellImage(i, COL_PFQImgAnswerType, imgList.Images[2]);
                        }
                        if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType]) == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                        {
                            c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[2]);
                        }
                    }
                }








                c1Question.Cols[COL_PFQDelete].AllowResizing = false;
                //COL_PFQDelete
                c1Question.Cols[COL_PFQDelete].Move(0);
         
                //COL_PFQHistoryCategory
                c1Question.Cols[COL_PFQHistoryCategory + 1].Move(1);
                //COL_PFQQuestionType
                c1Question.Cols[COL_PFQQuestionType + 2].Move(2);
                //COL_PFQHistoryItem
                c1Question.Cols[COL_PFQHistoryItem + 3].Move(3);
                //COL_PFQPubName
                c1Question.Cols[COL_PFQPubName + 4].Move(4);
                c1Question.Cols[COL_PFAAnsType].Move(10);

                c1Question.Cols[COL_PFQImgAnswerType].Move(19);

                c1Question.Cols[COL_GroupType+2].Move(1);
         
               
         

            
                //c1Question.Cols[COL_PFQPreText + 5].Move(5);
                ////COL_PFQPostTexts
                //c1Question.Cols[COL_PFQPostText + 6].Move(6);

                //c1Question.Cols[COL_PFQHistoryCategory].Move(1);
                //c1Question.Cols[COL_PFQHistoryItem].Move(2);


                SetModifyEnabled();

                //}
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;
            }
        }
        private void setimages()
        {
            for (int i = 1; i < c1Question.Rows.Count; i++)
            {
                if (c1Question.Rows[i][COL_PFQAnswerType + 5].ToString().ToUpper() == "NULL" || c1Question.Rows[i][COL_PFQAnswerType + 5].ToString() == "")
                {
                    c1Question.SetData(i, COL_PFQImgAnswerType, "");
                }
                else
                {
                    if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType + 5]) == Convert.ToInt16(clsHealthForm.QuestionType.Textbox))
                    {
                        c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[0]);
                    }
                    if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType + 5]) == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                    {
                        c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[1]);
                    }
                    if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType + 5]) == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox))
                    {
                        c1Question.SetCellImage(i, COL_PFQImgAnswerType, imgList.Images[1]);
                    }
                    if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType + 5]) == Convert.ToInt16(clsHealthForm.QuestionType.Radio))
                    {
                        c1Question.SetCellImage(i, COL_PFQImgAnswerType, imgList.Images[2]);
                    }
                    if (Convert.ToInt16(c1Question.Rows[i][COL_PFQAnswerType + 5]) == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                    {
                        c1Question.SetCellImage(i, COL_PFQImgAnswerType, ImgList24.Images[2]);
                    }
                }
            }

        }
        /// <summary>
        //Design answer grid
        /// <summary>
        private void DesignAnswerGrid()
        {
            gloC1FlexStyle objgloC1FlexStyle = null;
            try
            {
                c1Answers.DataSource = null;
                c1Answers.Rows.Count = 1;
                objgloC1FlexStyle = new gloC1FlexStyle();
                objgloC1FlexStyle.Style(c1Answers);
                //c1Answers.Rows.Add();

                c1Answers.SetData(0, COL_PFAAnswerId, "nPFAnswerId");
                c1Answers.Cols[COL_PFAAnswerId].Width = 0;

                c1Answers.SetData(0, COL_PFALibId, "nPFLibId");
                c1Answers.Cols[COL_PFALibId].Width = 0;

                c1Answers.SetData(0, COL_PFAAnswerLable, "Answer label");
                c1Answers.Cols[COL_PFAAnswerLable].Width = 150;
                c1Answers.Cols[COL_PFAAnswerLable].TextAlign = TextAlignEnum.LeftCenter;

                c1Answers.SetData(0, COL_PFAIsFollwedText, "bIsFollwedText");
                c1Answers.Cols[COL_PFAIsFollwedText].Width = 0;

                c1Answers.SetData(0, COL_PFAFollowedQues, "Followed question label");
                c1Answers.Cols[COL_PFAFollowedQues].Width = Convert.ToInt32(Width * 0.2);
                c1Answers.Cols[COL_PFAFollowedQues].TextAlign = TextAlignEnum.LeftCenter;
                //c1Answers.SetData(0, COL_PFAAnsType, "Answer Type");
                //c1Answers.SetData(0, COL_PFAnAnsType, "nAnswerType");//Textbox,checkbox etc Enum 
                c1Answers.SetData(0, COL_PFAnOrderId, "nOrderId");
                c1Answers.Cols[COL_PFAnOrderId].Width = 0;
                c1Answers.SetData(0, COL_PFAnHistoryItemId, "nHistoryItemId");
                c1Answers.Cols[COL_PFAnHistoryItemId].Width = 0;
                c1Answers.SetData(0, COL_PFAnOtherID, "nOtherID");
                c1Answers.Cols[COL_PFAnOtherID].Width = 0;
                c1Answers.SetData(0, COL_PFAnsDelete, "Delete");
                c1Answers.Cols[COL_PFAnAnsType].Width = 0;
                c1Answers.Cols[COL_PFAAnsType].Width = 0;
                if (c1Answers.Rows.Count > 1)
                {
                    C1.Win.C1FlexGrid.CellStyle cStyle = c1Answers.Styles.Add("Button");
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Answers.GetCellRange(1, COL_PFAnsDelete, c1Answers.Rows.Count - 1, COL_PFAnsDelete);
                    rgReaction.Style = cStyle;
                    c1Answers.SetCellImage(c1Answers.Rows.Count - 1, COL_PFAnsDelete, imgList.Images[0]);
                }

                c1Answers.Cols[COL_PFAnsDelete].Width = 48;
                c1Answers.Cols[COL_PFAnsDelete].Move(0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;
            }

        }


        private void DesignAnswerGrid(bool showAnswer = false, long _nPFLibID = 0, DataTable dtUpdated = null)
        {
            gloC1FlexStyle objgloC1FlexStyle = null;
            DataTable dt = null;
            try
            {
                c1Answers.DataSource = null;
                c1Answers.Rows.Count = 1;
                objgloC1FlexStyle = new gloC1FlexStyle();
                objgloC1FlexStyle.Style(c1Answers);
                c1Answers.Rows.Add();

                c1Answers.SetData(0, COL_PFAAnswerId, "nPFAnswerId");
                c1Answers.SetData(0, COL_PFALibId, "nPFLibId");
                c1Answers.SetData(0, COL_PFAAnswerLable, "Answer lable");
                c1Answers.SetData(0, COL_PFAIsFollwedText, "bIsFollwedText");
                c1Answers.SetData(0, COL_PFAFollowedQues, "Followed question label");
                c1Answers.Cols[COL_PFAFollowedQues].Width = Convert.ToInt32(Width * 0.2);
                //c1Answers.SetData(0, COL_PFAAnsType, "Answer Type");
                //c1Answers.SetData(0, COL_PFAnAnsType, "nAnswerType");//Textbox,checkbox etc Enum 
                c1Answers.SetData(0, COL_PFAnOrderId, "nOrderId");
                c1Answers.SetData(0, COL_PFAnHistoryItemId, "nHistoryItemId");
                c1Answers.SetData(0, COL_PFAnOtherID, "nOtherID");

                if (showAnswer)
                {
                    clsHealthForm oclsHistory = null;
                    try
                    {
                        oclsHistory = new clsHealthForm();
                        dt = oclsHistory.GetAnswers(_strConnectionString, _nPFLibID);
                        DataColumn colButton = new DataColumn("Delete", System.Type.GetType("System.String"));
                        dt.Columns.Add(colButton);
                        if (dt != null & dt.Rows.Count > 0)
                        {
                            c1Answers.Visible = true;
                            lblInfo.Visible = false;

                            c1Answers.DataSource = dt.DefaultView;
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        if (oclsHistory != null)
                            oclsHistory = null;
                        if (objgloC1FlexStyle != null)
                            objgloC1FlexStyle = null;
                    }
                }


                if (dt == null && c1Answers.DataSource == null)
                {
                    dt = dtUpdated;
                }

                c1Answers.DataSource = dt.DefaultView;

                c1Answers.Cols[COL_PFAAnswerId].Width = 0;
                c1Answers.Cols[COL_PFALibId].Width = 0;
                c1Answers.Cols[COL_PFAIsFollwedText].Width = 0;

                c1Answers.Cols[COL_PFAnOrderId].Width = 0;
                c1Answers.Cols[COL_PFAnHistoryItemId].Width = 0;
                c1Answers.Cols[COL_PFAnOtherID].Width = 0;
                c1Answers.Cols[COL_PFAnAnsType].Width = 0;
                c1Answers.Cols[COL_PFAAnsType].Width = 0;
                c1Answers.Cols[COL_PFAAnswerLable].Width = Convert.ToInt32(Width * 0.2);


                C1.Win.C1FlexGrid.CellStyle cStyle = c1Answers.Styles.Add("Button");

                if (dt != null && dt.Rows.Count > 1)
                {
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Answers.GetCellRange(1, COL_PFAnsDelete, c1Answers.Rows.Count - 1, COL_PFAnsDelete);
                    rgReaction.Style = cStyle;
                }
                else
                {
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Answers.GetCellRange(0, COL_PFAnsDelete, c1Answers.Rows.Count - 1, COL_PFAnsDelete);
                    rgReaction.Style = cStyle;
                }


                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    c1Answers.SetCellImage(i, COL_PFAnsDelete, imgList.Images[0]);
                }

                c1Answers.Cols[COL_PFAnsDelete].Width = 48;
                c1Answers.Cols[COL_PFAnsDelete].Move(0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;
            }

        }
        /// <summary>
        //Bind answer to grid
        /// <summary>
        private void BindAnswerGrid()
        {
            try
            {
                if (txtAnswerLabel.Text != string.Empty || txtFollowedQLab.Text != string.Empty)
                {
                    DataView dv;
                    DataTable dt;
                    if (ValidateAnswersGrid(out dv, out dt) == true)
                    {
                        AddAnswers();
                        //DesignAnswerGrid(false, 0, c1Answers.DataSource);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        //Bind answer to grid
        /// <summary>
        private void ModifyAnswerGrid(long _nPFAnswerId, long _nPFAnsLibId, string _sPFAnsAnswerLabel, bool _bIsFollowedQuestion, string _sPFAnsfollowedQuestionLabel, long _nPFAnsOrderId, long _nPFAnsHistoryItemId, long _nPFAnsOtherId)
        {
            bool bIsFollowedQuestion = false;
            try
            {
                if (rbYesFollowQ.Checked)
                {
                    bIsFollowedQuestion = true;
                }

                AddAnswerInGrid(c1Answers.RowSel, true, _nPFAnswerId, _nPFAnsLibId, txtAnswerLabel.Text.Trim(), bIsFollowedQuestion, txtFollowedQLab.Text.Trim(), _nPFAnsOrderId, _nPFAnsHistoryItemId, _nPFAnsOtherId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //private void ModifyAnswerGrid(Int64 nAnswerID)
        //{

        //}

        //private DataTable ModifyAnswers(Int64 nAnswerId, DataView dv, DataTable dt)
        //{
        //    if (_IsAssociatedAnswerModify == true)
        //    {

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (Convert.ToInt64(dr["nPFAnswerId"]) == nAnswerId)
        //            {
        //                dr["Answer Label"] = txtAnswerLabel.Text;
        //                if (rbYesFollowQ.Checked)
        //                {
        //                    dr["bIsFollwedText"] = true;
        //                }
        //                else
        //                {
        //                    dr["bIsFollwedText"] = false;
        //                }
        //                dr["Followed question label"] = txtFollowedQLab.Text;
        //                dr["Answer Type"] = cmbAnswerType.SelectedIndex.ToString();
        //                if (rdManual.Checked)
        //                {
        //                    dr["nHistoryItemId"] = cmbHisItem.SelectedValue.ToString();
        //                }
        //                else
        //                {
        //                    dr["nHistoryItemId"] = cmbAnsHistoryItem.SelectedValue.ToString();
        //                }
        //                dr["nOtherID"] = cmbAnsFamilyMember.SelectedValue.ToString();
        //            }
        //        }
        //        dt.AcceptChanges();





        //        //dv.RowFilter = "nPFAnswerId = " + _nPFAnswerId;
        //        //dt = dv.ToTable();
        //        //c1Answers.DataSource = dt.DefaultView;
        //    }
        //    return dt;
        //}

        private void AddAnswers()
        {
            //cmbAnswerType.Enabled = false;

            bool blnFollowedQuestion = false;
            if (c1Answers.Rows.Count > 1)
            {
                if (c1Answers.Rows[1][COL_PFAAnswerId + 1] != null)
                    c1Answers.Rows.Add();
            }
            else
                c1Answers.Rows.Add();

            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAAnswerId + 1, 0);//nPFAnswerId
            c1Answers.Cols[COL_PFAAnswerId + 1].Width = 0;//nPFAnswerId

            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFALibId + 1, _nLibId);//nPFLibId
            c1Answers.Cols[COL_PFALibId + 1].Width = 0;

            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAAnswerLable + 1, txtAnswerLabel.Text);//AnswerLable
            if (rbYesFollowQ.Checked)
                blnFollowedQuestion = true;
            else
                blnFollowedQuestion = false;
            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAIsFollwedText + 1, blnFollowedQuestion);//bIsFollwedText
            c1Answers.Cols[COL_PFAIsFollwedText + 1].Width = 0;

            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAFollowedQues + 1, txtFollowedQLab.Text);//FollowedQuestion
            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnOrderId + 1, 0);//order id
            c1Answers.Cols[COL_PFAnOrderId + 1].Width = 0;

            if (rdManual.Checked)
            {
                c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnHistoryItemId + 1, 0);//historyitemid.
            }
            else
            {
                c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnHistoryItemId + 1, cmbAnsHistoryItem.SelectedValue);//historyitemid.
            }
            c1Answers.Cols[COL_PFAnHistoryItemId + 1].Width = 0;

            if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
            {
                c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnOtherID + 1, cmbAnsAllergies.SelectedValue);//other id
            }
            else if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
            {
                c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnOtherID + 1, cmbAnsFamilyMember.SelectedValue);//other id
            }
            else
            {
                c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnOtherID + 1, 0);//other id
            }

            c1Answers.Cols[COL_PFAnOtherID + 1].Width = 0;


            c1Answers.SetData(c1Answers.Rows.Count - 1, COL_PFAnAnsType + 1, Convert.ToString(cmbAnswerType.SelectedIndex));//nAnswerType
            c1Answers.SetCellImage(c1Answers.Rows.Count - 1, 0, imgList.Images[0]);

            c1Answers.Cols[COL_PFAnAnsType + 1].Width = 0;
            c1Answers.Cols[COL_PFAAnsType + 1].Width = 0;
            pnlAnswerGrid.Visible = true;
            txtAnswerLabel.Text = string.Empty;
            txtFollowedQLab.Text = string.Empty;
            validateonanswers();

        }

        private bool ValidateAnswersGrid(out DataView dv, out DataTable dt)
        {
            bool IsValidate = false;
            dv = ((System.Data.DataView)(c1Answers.DataSource));
            dt = null;
            DataRow[] results = null;
            if (dv != null && dv.Table.Rows.Count > 0)
            {
                dt = dv.ToTable();
                //results = dt.Select("[Answer Label] = '" + txtAnswerLabel.Text + "' AND [Answer Type] = '" + Convert.ToString(cmbAnswerType.SelectedItem) + "' AND [Followed Text Label] = '" + txtFollowedQLab.Text + "'");
                results = dt.Select("[Answer Label] = '" + txtAnswerLabel.Text + "'");
                if (results.Count() > 0)
                {
                    MessageBox.Show("Record already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return IsValidate;
                }
                IsValidate = true;
            }
            else
                IsValidate = true;

            return IsValidate;
        }


        private bool ValidateModifiedAnswersGrid(out DataView dv, out DataTable dt)
        {
            bool IsValidate = false;
            dv = ((System.Data.DataView)(c1Answers.DataSource));

            dt = null;
            DataRow[] results = null;
            if (dv != null && dv.Table.Rows.Count > 0)
            {
                dt = dv.ToTable();
                //results = dt.Select("[Answer Label] = '" + txtAnswerLabel.Text + "' AND [Answer Type] = '" + Convert.ToString(cmbAnswerType.SelectedItem) + "' AND [Followed Text Label] = '" + txtFollowedQLab.Text + "'");
                results = dt.Select("[Answer Label] = '" + txtAnswerLabel.Text + "' AND [Followed question label] = '" + txtFollowedQLab.Text + "'AND [nOtherID] = '" + cmbAnsFamilyMember.SelectedValue.ToString() + "'");
                if (results.Count() > 0)
                {
                    MessageBox.Show("Record already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return IsValidate;
                }
                IsValidate = true;
            }
            else
                IsValidate = true;

            return IsValidate;
        }

        /// <summary>
        //Search Grid
        /// <summary>
        private void SearchGrid()
        {
            try
            {
                string strSearch = null;
                var _with1 = txtSearch;
                if (!string.IsNullOrEmpty(_with1.Text.Trim()))
                {
                    strSearch = _with1.Text.Replace("'", "''");
                }
                else
                {
                    strSearch = "";
                }

                DataView dvQuestion = null;
                if (c1Question.DataSource != null)
                {
                    dvQuestion = (DataView)c1Question.DataSource;
                    if (dvQuestion != null && dvQuestion.Table.Rows.Count > 0)
                    {
                        dvQuestion.RowFilter = "[Type] Like '%" + strSearch.Trim().Replace("'", "''") + "%' OR [Publish Name] Like '%" + strSearch.Trim().Replace("'", "''") + "%' OR [History Item] Like '%" + strSearch.Trim().Replace("'", "''") + "%'  OR [History Category] Like '%" + strSearch.Trim().Replace("'", "''") + "%' ";
                        c1Question.DataSource = dvQuestion;

                        if (dvQuestion.ToTable().Rows.Count > 0)
                        {
                            C1.Win.C1FlexGrid.CellStyle cStyle = c1Question.Styles.Add("Button");
                            C1.Win.C1FlexGrid.CellRange rgReaction = c1Question.GetCellRange(1, COL_PFQDelete, dvQuestion.ToTable().Rows.Count, COL_PFQDelete);
                            rgReaction.Style = cStyle;
                            for (int i = 1; i <= dvQuestion.ToTable().Rows.Count; i++)
                                c1Question.SetCellImage(i, 0, imgList.Images[0]);
                        }
                    }
                }

                SetModifyEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            setimages();
        }

        /// <summary>
        //Fill gender specific question type 
        /// <summary>
        private void FillGenderType()
        {
            cmbGender.DataSource = Enum.GetValues(typeof(clsHealthForm.GenderType));
        }

        /// <summary>
        //Save question
        /// <summary>
        private void SaveQuestion()
        {
            clsHealthForm oclsHistory = null;
            Int64 nHistoryItemID = 0;
            Int64 queID = 0;
            DataTable _dt = null;
            try
            {
                if (!ValidateQuestion())
                {
                    return;
                }

                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                {
                    nHistoryItemID = Convert.ToInt64(cmbHisItem.SelectedValue);
                    nQuestionType = 1;
                }
                else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                {
                    _dt = SaveAnswers();
                    if (rdHistoryItem.Checked)
                    {
                        nHistoryItemID = 0;
                    }
                    else
                    {
                        nHistoryItemID = Convert.ToInt64(cmbHisItem.SelectedValue);
                    }

                }

                oclsHistory = new clsHealthForm();
                int _rbttype = 0;
                if (rdoHistory.Checked)
                {
                    _rbttype = 1;
                }
                else if (rdoNonHistory.Checked)
                {
                    _rbttype = 2;

                }
                else if (rdoROS.Checked)
                {
                    _rbttype = 3;

                }
                if (_IsModify != true)
                {
                    Int64 nhistoryID = 0;
                    if (IsPatientHistoryRelated)
                    {
                        nhistoryID = Convert.ToInt64(cmbHisCategory.SelectedValue);
                    }
                    else
                    {
                        nhistoryID = 0;
                    }

                   
                    queID = oclsHistory.AddPFLibrary(nHistoryItemID, txtPublishNm.Text, "Q", txtPreText.Text, txtPostText.Text, cmbAnswerType.SelectedIndex, _nLoginID, _strConnectionString, 0, false, chkIsMandatory.Checked, Convert.ToInt16(cmbGender.SelectedItem), nQuestionType, nhistoryID, false, _dt, IsPatientHistoryRelated, _rbttype);

                    if (queID > 0)
                    {
                        //MessageBox.Show("Question added successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlGroup.Dock = DockStyle.None;
                    }
                    else if (queID == -1)
                        MessageBox.Show("Question already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to add question", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DesignGrid();
                    _nLibId = queID;
                    DesignAssociatedAnswersGrid();
                    pnlAnswer.Visible = true;
                    pnlAnswer.Dock = DockStyle.Bottom;
                    ClearAll();
                    //pnlGroup.Dock = DockStyle.None;
                }
                else
                {
                    Int64 nhistoryID = 0;
                    if (IsPatientHistoryRelated)
                    {
                        nhistoryID = Convert.ToInt64(cmbHisCategory.SelectedValue);
                    }
                    else
                    {
                        nhistoryID = 0;
                    }
                    queID = oclsHistory.AddPFLibrary(nHistoryItemID, txtPublishNm.Text, "Q", txtPreText.Text, txtPostText.Text, cmbAnswerType.SelectedIndex, _nLoginID, _strConnectionString, _nLibId, true, chkIsMandatory.Checked, Convert.ToInt16(cmbGender.SelectedItem), nQuestionType, nhistoryID, false, _dt, IsPatientHistoryRelated, _rbttype);
                    if (queID > 0)
                    {
                        //MessageBox.Show("Question updated successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlGroup.Dock = DockStyle.None;
                    }
                    else
                        MessageBox.Show("Failed to update question", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DesignGrid();
                    DesignAssociatedAnswersGrid();
                    ClearAll();
                    //pnlGroup.Dock = DockStyle.None;
                    //ts_Save.Text = "Save";
                    _IsModify = false;
                }
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }
        }

        private bool ValidateQuestion()
        {
            if (IsPatientHistoryRelated)
            {
                //For History Category.
                if (cmbHisCategory.SelectedIndex == 0 && rdoHistory.Checked)
                {
                    MessageBox.Show("Please select history category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisCategory.Focus();
                    return false;
                }

                if (cmbHisCategory.SelectedIndex == 0 && rdoROS.Checked)
                {
                    MessageBox.Show("Please select ROS category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisCategory.Focus();
                    return false;
                }


                //For History Item.
                if (rdManual.Checked && (cmbHisItem.SelectedIndex == 0 || cmbHisItem.SelectedIndex == -1) && rdoHistory.Checked)
                {
                    MessageBox.Show("Please select history item in question section.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisItem.Focus();
                    return false;
                }

                if (rdManual.Checked && (cmbHisItem.SelectedIndex == 0 || cmbHisItem.SelectedIndex == -1) && rdoROS.Checked)
                {
                    MessageBox.Show("Please select ROS item in question section.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisItem.Focus();
                    return false;
                }
            }

            if (txtPublishNm.Text == "" || txtPublishNm.Text.Trim() == "")
            {
                MessageBox.Show("Please enter question label.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPublishNm.Focus();
                return false;
            }

            if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
            {
                if (c1Answers.Rows.Count <= 1)
                {
                    MessageBox.Show("Please enter answer.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (txtPublishNm.Text.Trim() != "")
            {
                if (!clsGeneral.ContainsHtml(txtPublishNm.Text.Trim(), "Question label"))
                {
                    txtPublishNm.Focus();
                    return false;
                }
            }

            if (txtPreText.Text.Trim() != "")
            {
                if (!clsGeneral.ContainsHtml(txtPreText.Text.Trim(), "Pre text"))
                {
                    txtPreText.Focus();
                    return false;
                }
            }


            if (txtPostText.Text.Trim() != "")
            {
                if (!clsGeneral.ContainsHtml(txtPostText.Text.Trim(), "Post text"))
                {
                    txtPostText.Focus();
                    return false;
                }
            }




            return true;
        }

        /// <summary>
        //Fill answer type i.e. Textbox, Checkbox, Radio etc
        /// <summary>
        private void FillAnswerType()
        {
            try
            {
                this.cmbAnswerType.SelectedIndexChanged -= new EventHandler(cmbAnswerType_SelectedIndexChanged);
                //cmbAnswerType.DataSource = Enum.GetValues(typeof(clsHealthForm.QuestionType));              
                cmbAnswerType.DataSource = Enum.GetValues(typeof(clsHealthForm.QuestionType))
                       .Cast<Enum>()
                       .Select(value => new KeyValuePair<int, string>(Convert.ToInt32(value), (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description))
                    //{
                    //    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    //    value
                    //})                       
                       .ToList();
                cmbAnswerType.DisplayMember = "Value";
                cmbAnswerType.ValueMember = "Key";

                this.cmbAnswerType.SelectedIndexChanged += new EventHandler(cmbAnswerType_SelectedIndexChanged);
            }
            catch (Exception)
            {
                throw;
            }
        }






        /// <summary>
        //Show hide answer panels according to answertype
        /// <summary>
        private void ShowHideAnswerPanels(string IsFrom, bool IsDesignAnsGrid)
        {
            bool showControls = false;
            if (IsPreviousMultioption)
            {
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                {
                    if (c1Answers.Rows.Count > 1)
                    {
                        if (c1Answers.Rows[1][1] != null)
                        {
                            DialogResult Result;
                            Result = MessageBox.Show("Existing information will be lost by changing answer type.\nWould you like to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (Result == DialogResult.Yes)
                            {
                                showControls = true;
                                ResetAnswers();
                                validateonanswers();
                            }
                        }
                        else
                            showControls = true;
                    }
                    else
                        showControls = true;

                    if (showControls)
                    {
                        AnswerShowHidePanels(false);
                        pnlAns.Visible = false;
                        pnlAnswer.Visible = false;


                        LastselectedIndex = cmbAnswerType.SelectedIndex;

                        DesignAnswerGrid();
                        _IsAssociatedAnswerModify = false;
                        _nPFAnswerId = 0;

                        c1AnswerAssociated.DataSource = null;
                        c1AnswerAssociated.Rows.Count = 1;
                    }
                    else
                    {
                        CallAnswerTypeValidation = false;
                        cmbAnswerType.SelectedIndex = LastselectedIndex;
                        pnlQQuesType.Visible = true;
                        if (rdHistoryItem.Checked)
                        {
                            pnlHistoryItem.Visible = false;
                        }
                        else
                        {
                            pnlHistoryItem.Visible = true;
                        }
                        CallAnswerTypeValidation = true;
                    }
                    // AnswerShowHidePanels(false);
                }
                else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                {
                    AnswerShowHidePanels(true);
                    pnlAns.Visible = true;
                    pnlAnswer.Visible = true;
                    if (c1Answers.Rows.Count < 2)
                    {
                        DesignAnswerGrid();
                    }
                    LastselectedIndex = cmbAnswerType.SelectedIndex;
                }
            }
            else
            {
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                {
                    //show message
                    if (c1Answers.Rows.Count > 1)
                    {
                        if (c1Answers.Rows[1][1] != null)
                        {
                            DialogResult Result;
                            Result = MessageBox.Show("Existing answer will be get deleted as your changing answer type.\nDo you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (Result == DialogResult.Yes)
                            {
                                showControls = true;
                                ResetAnswers();
                                validateonanswers();
                            }
                        }
                        else
                            showControls = true;
                    }
                    else
                        showControls = true;

                    if (showControls)
                    {
                        AnswerShowHidePanels(true);
                        pnlAns.Visible = true;
                        pnlAnswer.Visible = true;

                        LastselectedIndex = cmbAnswerType.SelectedIndex;

                        DesignAnswerGrid();
                        _IsAssociatedAnswerModify = false;
                        _nPFAnswerId = 0;

                        c1AnswerAssociated.DataSource = null;
                        c1AnswerAssociated.Rows.Count = 1;
                    }
                    else
                    {
                        cmbAnswerType.SelectedIndex = LastselectedIndex;
                    }
                }
                else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                {
                    AnswerShowHidePanels(false);
                    pnlAns.Visible = false;
                    pnlAnswer.Visible = false;
                    LastselectedIndex = cmbAnswerType.SelectedIndex;
                }
            }

            if (IsFrom == "Modify")
            {
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                    pnlAnswerGrid.Visible = false;
                else
                    pnlAnswerGrid.Visible = true;
            }

        }

        private void ValidateAnswerPanelVisibility()
        {

            if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
            {
                AnswerShowHidePanels(false);
                pnlAns.Visible = false;
                pnlAnswer.Visible = false;
            }
            else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
            {
                AnswerShowHidePanels(true);
                pnlAns.Visible = true;
                pnlAnswer.Visible = true;
            }

        }

        private Int16 getAnswerTypeValue()
        {
            return Convert.ToInt16(((System.Collections.Generic.KeyValuePair<int, string>)(cmbAnswerType.SelectedItem)).Key);
        }

        private void AnswerShowHidePanels(bool IsShow)
        {
            pnlFollwedYesNo.Visible = IsShow;
            pnlAddCancel.Visible = IsShow;
            pnlAnswerGrid.Visible = IsShow;

            if (rbYesFollowQ.Checked)
                pnlFollwedLabel.Visible = true;
            else
                pnlFollwedLabel.Visible = false;
        }


        /// <summary>
        //Save question
        /// <summary>
        private DataTable SaveAnswers()
        {
            DataTable dtDyAnswers = null;
            clsHealthForm oclsHealthForm = null;
            string Msg = "added";
            string MsgFail = "add";
            //Int64 ansID = 0;
            try
            {

                dtDyAnswers = new DataTable();
                //Create columns for this DataTable
                DataColumn colnPFAnswerId = new DataColumn("nPFAnswerId", System.Type.GetType("System.Int64"));
                DataColumn colnPFLibId = new DataColumn("nPFLibId", System.Type.GetType("System.Int64"));
                DataColumn colsAnsLabel = new DataColumn("sAnsLabel", System.Type.GetType("System.String"));
                DataColumn colbIsFollwedText = new DataColumn("bIsFollwedText", System.Type.GetType("System.Boolean"));
                DataColumn colsFollowedTextLabel = new DataColumn("sFollowedTextLabel", System.Type.GetType("System.String"));
                // DataColumn colnAnswerType = new DataColumn("nAnswerType", System.Type.GetType("System.String"));
                DataColumn colnIdAnswerType = new DataColumn("nAnswerTypeID", System.Type.GetType("System.String"));
                DataColumn colnOrderId = new DataColumn("nOrderId", System.Type.GetType("System.Int32"));
                DataColumn colnHistoryItemId = new DataColumn("nHistoryItemId", System.Type.GetType("System.Int64"));
                DataColumn colnOtherID = new DataColumn("nOtherID", System.Type.GetType("System.Int64"));

                //Add column to datatable
                dtDyAnswers.Columns.Add(colnPFAnswerId);
                dtDyAnswers.Columns.Add(colnPFLibId);
                dtDyAnswers.Columns.Add(colsAnsLabel);
                dtDyAnswers.Columns.Add(colbIsFollwedText);
                dtDyAnswers.Columns.Add(colsFollowedTextLabel);
                //dtDyAnswers.Columns.Add(colnAnswerType);
                dtDyAnswers.Columns.Add(colnIdAnswerType);
                dtDyAnswers.Columns.Add(colnOrderId);
                dtDyAnswers.Columns.Add(colnHistoryItemId);
                dtDyAnswers.Columns.Add(colnOtherID);


                //c1Answers.ColSel = 0;//for updataing grid data
                tls_DyHealthForm.Select();
                if (_IsDuplicateAnswer == false)
                {
                    if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                    {
                        DataRow row = dtDyAnswers.NewRow();
                        row[colnPFAnswerId] = 0;
                        row[colnPFLibId] = _nLibId;
                        row[colsAnsLabel] = txtAnswerLabel.Text.Trim();
                        row[colbIsFollwedText] = false;
                        row[colsFollowedTextLabel] = string.Empty;
                        row[colnIdAnswerType] = getAnswerTypeValue();

                        row[colnOrderId] = 1;
                        if (rdManual.Checked)
                        {
                            row[colnHistoryItemId] = Convert.ToInt64(cmbHisItem.SelectedValue);
                        }
                        else
                        {
                            row[colnHistoryItemId] = Convert.ToInt64(cmbAnsHistoryItem.SelectedValue);
                        }

                        if (nQuestionType==1)
                        {
                            if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
                            {
                                row[colnOtherID] = Convert.ToInt64(cmbAnsAllergies.SelectedValue);
                            }
                            else if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
                            {
                                row[colnOtherID] = Convert.ToInt64(cmbAnsFamilyMember.SelectedValue);
                            }
                            else
                            {
                                row[colnOtherID] = 0;
                            }
                        }
                        else
                        {
                            row[colnOtherID] = 0;
                        }
                        dtDyAnswers.Rows.Add(row);
                    }
                    else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                    {
                        for (int i = 1; i < c1Answers.Rows.Count; i++)
                        {
                            //Create a Row in the DataTable table
                            DataRow row = dtDyAnswers.NewRow();
                            row[colnPFAnswerId] = Convert.ToInt64(c1Answers.GetData(i, 1));
                            row[colnPFLibId] = Convert.ToInt64(c1Answers.GetData(i, 2));
                            row[colsAnsLabel] = Convert.ToString(c1Answers.GetData(i, 3));

                            if (Convert.ToString(c1Answers.GetData(i, 5)).Trim() != string.Empty)
                                row[colbIsFollwedText] = true;
                            else
                                row[colbIsFollwedText] = false;
                            //row[colbIsFollwedText] = Convert.ToBoolean(c1Answers.GetData(i, 3));

                            row[colsFollowedTextLabel] = Convert.ToString(c1Answers.GetData(i, 5));
                            row[colnIdAnswerType] = Convert.ToInt16(c1Answers.GetData(i, 7));

                            row[colnOrderId] = i;
                            row[colnHistoryItemId] = Convert.ToInt64(c1Answers.GetData(i, 9));
                            if (nQuestionType==1)
                            {
                                if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
                                {
                                    row[colnOtherID] = Convert.ToInt64(c1Answers.GetData(i, 10));
                                }
                                else if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
                                {
                                    row[colnOtherID] = Convert.ToInt64(c1Answers.GetData(i, 10));
                                }
                                else
                                {
                                    row[colnOtherID] = 0;
                                }
                            }
                            else
                            {
                                row[colnOtherID] = 0;
                            }

                            dtDyAnswers.Rows.Add(row);
                        }
                    }
                }//end _IsDuplicateAnswer

                validateonanswers();

            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }

            return dtDyAnswers;
        }

        /// <summary>
        //Reset answers panel
        /// <summary>
        private void ResetAnswers()
        {
            txtAnswerLabel.Text = string.Empty;
            rbYesFollowQ.Checked = false;
            rbNoFollowQ.Checked = false;
            txtFollowedQLab.Text = string.Empty;

            pnlFollwedYesNo.Visible = false;
            pnlFollwedLabel.Visible = false;
            pnlAddCancel.Visible = false;

            DesignAnswerGrid();
        }

        /// <summary>
        //Show question 
        /// <summary>
        public Int64 BindQuestion()
        {
            try
            {
                //FillCategory();
                //FillCombo();
                if (c1Question.Rows.Count > 1 && c1Question.RowSel != -1)
                {
                    lblHeading.Text = "Online Patient Form - Modify question";
                    DataRowView drv = ((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource));
                    if (drv != null && drv.Row.Table.Rows.Count > 0)
                    {
                        pnlQuestion.Visible = true;

                        //cmbAnswerType.Enabled = true;
                        pnlGroup.Dock = DockStyle.Right;
                        ts_ShowHide.Text = "&Close";
                        //ts_Save.Text = "Save";
                        //pnlQuestion.Dock = DockStyle.Right;
                        _nLibId = Convert.ToInt64(drv.Row.ItemArray[0]);

                        nRbtType = Convert.ToInt16(drv.Row.ItemArray[17]);
                        if (nRbtType == 3)
                        {
                            FillCategory("ROS");
                        }
                        else
                        {
                            FillCategory();
                            FillCombo();
                        }
                        
              
                        this.cmbHisCategory.SelectedIndexChanged -= new EventHandler(cmbHisCategory_SelectedIndexChanged);
                        cmbHisCategory.SelectedValue = drv.Row.ItemArray[15];
                        this.cmbHisCategory.SelectedIndexChanged += new EventHandler(cmbHisCategory_SelectedIndexChanged);

                        this.cmbAnswerType.SelectedIndexChanged -= new EventHandler(cmbAnswerType_SelectedIndexChanged);
                        //   cmbAnswerType.SelectedItem = Enum.ToObject(typeof(clsHealthForm.QuestionType), Convert.ToInt32(drv.Row.ItemArray[7]));
                        string enumdesc = "";
                        if (Convert.ToInt32(drv.Row.ItemArray[7]).ToString() == "0")
                            enumdesc = "Free text";
                        if (Convert.ToInt32(drv.Row.ItemArray[7]).ToString() == "1")
                            enumdesc = "Free text (Long)";
                        if (Convert.ToInt32(drv.Row.ItemArray[7]).ToString() == "2")
                            enumdesc = "Multiple options (Checkbox)";
                        if (Convert.ToInt32(drv.Row.ItemArray[7]).ToString() == "3")
                            enumdesc = "Multiple options (Radio button)";
                        if (Convert.ToInt32(drv.Row.ItemArray[7]).ToString() == "4")
                            enumdesc = "Multiple options (Dropdown)";
                        cmbAnswerType.SelectedItem = new System.Collections.Generic.KeyValuePair<int, string>(Convert.ToInt32(drv.Row.ItemArray[7]), enumdesc);
                        LastselectedIndex = cmbAnswerType.SelectedIndex;
                        this.cmbAnswerType.SelectedIndexChanged += new EventHandler(cmbAnswerType_SelectedIndexChanged);

                         FillHistoryItmes(Convert.ToInt64(drv.Row.ItemArray[15]));
                         FillAnsHistoryItmes(Convert.ToInt64(drv.Row.ItemArray[15]));
                            
                       

                        cmbHisItem.SelectedValue = drv.Row.ItemArray[1];

                        txtPublishNm.Text = Convert.ToString(drv.Row.ItemArray[3]);
                        txtPreText.Text = Convert.ToString(drv.Row.ItemArray[5]);
                        txtPostText.Text = Convert.ToString(drv.Row.ItemArray[6]);
                        chkIsMandatory.Checked = Convert.ToBoolean(drv.Row.ItemArray[10]);
                        if (Convert.ToString(drv.Row.ItemArray[13]) == "Manual")
                        {
                            rdManual.Checked = true;
                            cmbHisItem.SelectedValue = drv.Row.ItemArray[1];
                        }
                        else
                        {
                            rdHistoryItem.Checked = true;
                        }

                        cmbGender.SelectedItem = Enum.ToObject(typeof(clsHealthForm.GenderType), drv.Row.ItemArray[11]);
                        _IsModify = true;

                        // Keep below varible for skipping confirmation
                        showConfirmation = false;
                        //if (Convert.ToBoolean(drv.Row["bIsPatientHistoryrelated"]) == true)
                        if (nRbtType == 1)
                        {
                            // if already same radio selected then event won't fire,for that showhidecontrol condition added
                            IsPatientHistoryRelated = true;
                            if (rdoHistory.Checked)
                            {
                                ShowHideControl();
                            }
                            else
                            {
                                _fillCategoryOnRadiobuttonchange = false;
                                rdoHistory.Checked = true;
                                _fillCategoryOnRadiobuttonchange = true;
                            }
                        }
                        else if (nRbtType == 3)
                        {
                            // if already same radio selected then event won't fire,for that showhidecontrol condition added
                            IsPatientHistoryRelated = true;
                            if (rdoROS.Checked)
                            {
                                ShowHideControl();
                            }
                            else
                            {
                                _fillCategoryOnRadiobuttonchange = false;
                                rdoROS.Checked = true;
                                _fillCategoryOnRadiobuttonchange = true;
                            }
                        }
                        else if (nRbtType == 2)
                        {
                            IsPatientHistoryRelated = false;
                            if (rdoNonHistory.Checked)
                            {
                                ShowHideControl();
                            }
                            else
                            {
                                rdoNonHistory.Checked = true;
                            }
                        }
                        showConfirmation = true;
                        //showConfirmation = true;
                        //ts_AddAnswer.Visible = true;
                        //if (Convert.ToString(drv.Row.ItemArray[3]).Trim() == string.Empty)
                        //    lblQuesNm.Text = "Answers for question: " + Convert.ToString(drv.Row.ItemArray[9]).Trim();
                        //else
                        //    lblQuesNm.Text = "Answers for question: " + Convert.ToString(drv.Row.ItemArray[3]).Trim();
                    }
                    else
                    {
                        pnlQuestion.Visible = true;
                        //cmbAnswerType.Enabled = true;
                        pnlGroup.Dock = DockStyle.Right;
                        ts_ShowHide.Text = "&Close";
                        //ts_Save.Text = "Save";
                    }
                    return _nLibId;
                    cmbHisCategory.Focus();
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        //Reset answers panel
        /// <summary>
        public void DesignAssociatedAnswersGrid()
        {
            //ResetAnswers();
            gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
            objgloC1FlexStyle.Style(c1AnswerAssociated);
            clsHealthForm oclsHistory = null;


            long _LibID = 0;
            try
            {
                c1AnswerAssociated.DataSource = null;
                c1AnswerAssociated.Rows.Count = 1;

                oclsHistory = new clsHealthForm();
                c1AnswerAssociated.Cols.Count = 9;

                DataTable dt = oclsHistory.GetAnswers(_strConnectionString, _nLibId);
                if (dt != null & dt.Rows.Count > 0)
                {

                    c1AnswerAssociated.Visible = true;
                    lblInfo.Visible = false;

                    DataColumn colButton = new DataColumn("Delete", System.Type.GetType("System.String"));
                    dt.Columns.Add(colButton);
                    c1AnswerAssociated.DataSource = dt.DefaultView;


                    c1AnswerAssociated.Cols[COLAS_PFAnswerID].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFLibId].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFIsFollwedText].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFnAnsType].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFAnsType].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFnOrderId].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFnHistoryItemId].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFnOtherID].Width = 0;
                    c1AnswerAssociated.Cols[COLAS_PFDelete].Width = 0;

                    c1AnswerAssociated.Cols[COLAS_PFAnsLabel].Width = 400;
                    c1AnswerAssociated.Cols[COLAS_PFFollwText].Width = Convert.ToInt32(Width * 0.2);

                }
                else
                {
                    c1AnswerAssociated.Visible = false;
                    pnlAnswer.Dock = DockStyle.Bottom;
                    lblInfo.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;
            }
        }

        /// <summary>
        //Add new Question
        /// <summary>
        public void AddNew()
        {
            cmbHisCategory.Enabled = true;
            grpHistoryRelation.Enabled = true;
            FillCategory();
            FillCombo();
            lblHeading.Text = "Online Patient Form - New question";
            pnlQuestion.Visible = true;
            //cmbAnswerType.Enabled = true;
            cmbGender.SelectedIndex = 0;
            pnlGroup.Dock = DockStyle.Right;
            ts_ShowHide.Text = "&Close";
            ts_Save.Text = "Save";
            //cmbAnswerType.SelectedIndexChanged -= new EventHandler(cmbAnswerType_SelectedIndexChanged);
            //cmbAnswerType.SelectedIndex = 0;
            //cmbAnswerType.SelectedIndexChanged += new EventHandler(cmbAnswerType_SelectedIndexChanged);

            ClearAll();
            ts_Save.Text = "Save";

            _IsModify = false;
            rdManual.Checked = true;

            ValidateAnswerPanelVisibility();

            if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
            {
                pnlQQuesType.Visible = false;
                pnlHistoryItem.Visible = true;
            }
            else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown) || rdHistoryItem.Checked)
            {
                pnlQQuesType.Visible = true;
                pnlHistoryItem.Visible = false;
            }

            c1Question.Enabled = false;

            cmbHisCategory.Focus();
            this.rdoHistory.CheckedChanged -= new EventHandler(HistoryRadioButtons_CheckedChanged);
            IsPatientHistoryRelated = true;
            rdoHistory.Checked = true;
            nRbtType = 1;
            rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ShowHideControl();
            this.rdoHistory.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged);
        }


        /// <summary>
        //Show associated answers
        /// <summary>
        public void BindAssociatedAnswers()
        {
            try
            {
                ts_ShowHide.Text = "&Close";
                ts_AddAnswer.Visible = false;

                pnlGroup.Dock = DockStyle.Right;
                pnlQuestion.Visible = true;
                //FillAnswerType();
                rbYesFollowQ.Checked = false;
                rbNoFollowQ.Checked = true;
                pnlFollwedLabel.Visible = false;

                ValidateHistoryCategoryControlsVisibility();


                if (c1AnswerAssociated.Rows.Count > 1)
                {
                    ValidateAnswerPanelVisibility();
                    ShowAssociatedAnswers();
                    _IsAssociatedAnswerModify = true;
                }
                else
                {
                    if (Convert.ToInt16(c1Question.Rows[c1Question.RowSel][COL_PFQAnswerType + 5]) != -1)
                    {
                        //cmbAnswerType.SelectedIndex = Convert.ToInt16(c1Question.Rows[c1Question.RowSel][12]);
                        // 12 is UserID
                        cmbAnswerType.SelectedIndex = Convert.ToInt16(c1Question.Rows[c1Question.RowSel][COL_PFQAnswerType + 5]);
                    }
                    else
                    {
                        cmbAnswerType.SelectedIndex = 0;

                    }
                    ValidateAnswerPanelVisibility();
                }
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                {
                    pnlQQuesType.Visible = false;
                    if (IsPatientHistoryRelated == true)
                    {
                        pnlHistoryItem.Visible = true;
                    }
                    else
                    {
                        pnlHistoryItem.Visible = false;
                    }
                }
                else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                {
                    //pnlQQuesType.Visible = true;
                    //if (rdHistoryItem.Checked)
                    //{
                    //    pnlHistoryItem.Visible = false;
                    //    pnlAnsHistoryItem.Visible = true;
                    //}
                    //else
                    //{
                    //    pnlHistoryItem.Visible = true;
                    //    pnlAnsHistoryItem.Visible = false;
                    //}

                    if (IsPatientHistoryRelated == true)
                    {
                        pnlQQuesType.Visible = true;

                        if (rdHistoryItem.Checked)
                        {
                            pnlHistoryItem.Visible = false;
                            pnlAnsHistoryItem.Visible = true;
                        }
                        else
                        {
                            pnlHistoryItem.Visible = true;
                            pnlAnsHistoryItem.Visible = false;
                        }
                    }
                    else
                    {
                        pnlQQuesType.Visible = false;
                    }
                }
                c1Answers.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidateHistoryCategoryControlsVisibility()
        {
            string sCategoryType = getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue));
            if (IsPatientHistoryRelated)
            {
                if (nQuestionType==2)
                {
                    pnlAnsFamilyMember.Visible = false;
                    pnlAnsReaction.Visible = false;
                }
                else
                {
                    if (sCategoryType == "All")
                    {
                        pnlAnsFamilyMember.Visible = false;
                        pnlAnsReaction.Visible = true;
                    }
                    else if (sCategoryType == "Fam")
                    {
                        pnlAnsFamilyMember.Visible = true;
                        pnlAnsReaction.Visible = false;
                    }
                    else
                    {
                        pnlAnsFamilyMember.Visible = false;
                        pnlAnsReaction.Visible = false;
                    }
                }
                
            }
            else
            {
                pnlAnsFamilyMember.Visible = false;
                pnlAnsReaction.Visible = false;
            }
        }

        private string getHistoryType(long CategoryID)
        {
            string _caterogyType = string.Empty;
            clsHealthForm oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                oclsHistory = new clsHealthForm();
                dtHisCategory = oclsHistory.GetHistoryType(_strConnectionString, CategoryID);
                if (dtHisCategory != null && dtHisCategory.Rows.Count > 0)
                {
                    _caterogyType = Convert.ToString(dtHisCategory.Rows[0]["sHistoryType"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
            return _caterogyType;
        }

        /// <summary>
        //Show associated answers
        /// <summary>
        private void ShowAssociatedAnswers()
        {
            gloC1FlexStyle objgloC1FlexStyle = null;
            try
            {
                objgloC1FlexStyle = new gloC1FlexStyle();
                objgloC1FlexStyle.Style(c1Answers);

                DataView dv = ((System.Data.DataView)(c1AnswerAssociated.DataSource));
                if (dv != null && dv.Table.Rows.Count > 0)
                {
                    DataTable dt = dv.ToTable();
                    //c1Answers.Cols.Count = 9;
                    //c1Answers.DataSource = dt.DefaultView;

                    c1Answers.SetData(0, COL_PFAAnswerId, "nPFAnswerId");
                    c1Answers.Cols[COL_PFAAnswerId].Width = 0;
                    c1Answers.Cols[COL_PFAAnswerId].AllowEditing = false;

                    c1Answers.SetData(0, COL_PFALibId, "nPFLibId");
                    c1Answers.Cols[COL_PFALibId].Width = 0;
                    c1Answers.Cols[COL_PFALibId].AllowEditing = false;

                    c1Answers.SetData(0, COL_PFAAnswerLable, "Answer label");
                    c1Answers.Cols[COL_PFAAnswerLable].Width = Convert.ToInt32(Width * 0.2);
                    c1Answers.Cols[COL_PFAAnswerLable].TextAlign = TextAlignEnum.LeftCenter;

                    c1Answers.SetData(0, COL_PFAIsFollwedText, "bIsFollwedText");
                    c1Answers.Cols[COL_PFAIsFollwedText].Width = 0;
                    c1Answers.Cols[COL_PFAIsFollwedText].AllowEditing = false;

                    c1Answers.SetData(0, COL_PFAFollowedQues, "Followed question label");
                    c1Answers.Cols[COL_PFAFollowedQues].Width = Convert.ToInt32(Width * 0.2);
                    c1Answers.Cols[COL_PFAFollowedQues].TextAlign = TextAlignEnum.LeftCenter;
                    //c1Answers.SetData(0, COL_PFAAnsType, "Answer Type");
                    //c1Answers.SetData(0, COL_PFAnAnsType, "nAnswerType");//Textbox,checkbox etc Enum 
                    c1Answers.SetData(0, COL_PFAnOrderId, "nOrderId");
                    c1Answers.Cols[COL_PFAnOrderId].Width = 0;
                    c1Answers.SetData(0, COL_PFAnHistoryItemId, "nHistoryItemId");
                    c1Answers.Cols[COL_PFAnHistoryItemId].Width = 0;
                    c1Answers.SetData(0, COL_PFAnOtherID, "nOtherID");
                    c1Answers.Cols[COL_PFAnOtherID].Width = 0;
                    c1Answers.SetData(0, COL_PFAnsDelete, "Delete");
                    c1Answers.Cols[COL_PFAnAnsType].Width = 0;
                    c1Answers.Cols[COL_PFAAnsType].Width = 0;

                    for (int i = c1Answers.Rows.Count - 1; i >= 1; i--)
                    {
                        c1Answers.Rows.Remove(i);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddAnswerInGrid(-1, false, Convert.ToInt64(dt.Rows[i]["nPFAnswerId"]), Convert.ToInt64(dt.Rows[i]["nPFLibId"]), Convert.ToString(dt.Rows[i]["Answer label"]), Convert.ToBoolean(dt.Rows[i]["bIsFollwedText"]), Convert.ToString(dt.Rows[i]["Followed question label"]), Convert.ToInt64(dt.Rows[i]["nOrderId"]), Convert.ToInt64(dt.Rows[i]["nHistoryItemId"]), Convert.ToInt64(dt.Rows[i]["nOtherID"]));
                    }
                }

                c1Answers.Cols[COL_PFAnsDelete].AllowEditing = false;
                c1Answers.Cols[COL_PFAnsDelete].Width = 48;
                c1Answers.Cols[COL_PFAnsDelete].ImageAlign = ImageAlignEnum.CenterCenter;

                if (c1Answers.DataSource != null)
                {
                    C1.Win.C1FlexGrid.CellStyle cStyle = c1Answers.Styles.Add("Button");
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Answers.GetCellRange(1, COL_PFAnsDelete, c1Answers.Rows.Count, COL_PFAnsDelete);
                    rgReaction.Style = cStyle;
                }

                for (int i = 1; i < c1Answers.Rows.Count; i++)
                    c1Answers.SetCellImage(i, COL_PFAnsDelete, imgList.Images[0]);

                c1Answers.Cols[COL_PFAnsDelete].Move(0);

                c1Answers.Cols[COL_PFAAnsType].AllowEditing = false;

                validateonanswers();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;
            }
        }
        private void validateonanswers()
        {
            cmbHisCategory.Enabled = true;
            rdManual.Enabled = true;
            rdHistoryItem.Enabled = true;

            if (c1Answers.Rows.Count > 1)
            {
                cmbHisCategory.Enabled = false;
                rdManual.Enabled = false;
                rdHistoryItem.Enabled = false;
            }
        }
        private void AddAnswerInGrid(int rowindex, bool IsForModify, Int64 nPFAnswerId, Int64 nPFLibId, string sAnswerLabel, bool bIsFollwedText, string sFollowedQuestionLabel, Int64 nOrderId, Int64 nHistoryItemId, Int64 nOtherID)
        {
            if (rowindex == -1)
            {
                c1Answers.Rows.Add();
                rowindex = c1Answers.Rows.Count - 1;
            }

            if (IsForModify)
            {
                c1Answers.SetData(rowindex, COL_PFAAnswerId + 1, Convert.ToString(nPFAnswerId));
                c1Answers.SetData(rowindex, COL_PFALibId + 1, Convert.ToString(nPFLibId));
                c1Answers.SetData(rowindex, COL_PFAAnswerLable + 1, Convert.ToString(sAnswerLabel));
                c1Answers.SetData(rowindex, COL_PFAIsFollwedText + 1, Convert.ToString(bIsFollwedText));
                c1Answers.SetData(rowindex, COL_PFAFollowedQues + 1, Convert.ToString(sFollowedQuestionLabel));
                c1Answers.SetData(rowindex, COL_PFAnOrderId + 1, Convert.ToString(nOrderId));
                c1Answers.SetData(rowindex, COL_PFAnHistoryItemId + 1, Convert.ToString(nHistoryItemId));
                c1Answers.SetData(rowindex, COL_PFAnOtherID + 1, Convert.ToString(nOtherID));
            }
            else
            {
                c1Answers.SetData(rowindex, COL_PFAAnswerId, Convert.ToString(nPFAnswerId));
                c1Answers.SetData(rowindex, COL_PFALibId, Convert.ToString(nPFLibId));
                c1Answers.SetData(rowindex, COL_PFAAnswerLable, Convert.ToString(sAnswerLabel));
                c1Answers.SetData(rowindex, COL_PFAIsFollwedText, Convert.ToString(bIsFollwedText));
                c1Answers.SetData(rowindex, COL_PFAFollowedQues, Convert.ToString(sFollowedQuestionLabel));
                c1Answers.SetData(rowindex, COL_PFAnOrderId, Convert.ToString(nOrderId));
                c1Answers.SetData(rowindex, COL_PFAnHistoryItemId, Convert.ToString(nHistoryItemId));
                c1Answers.SetData(rowindex, COL_PFAnOtherID, Convert.ToString(nOtherID));
            }
        }

        /// <summary>
        //Enable answer type
        /// <summary>
        private void EnableAnswerType(bool IsEnable)
        {
            //cmbAnswerType.Enabled = IsEnable;
            if (IsEnable != false)
            {
                if (cmbAnswerType.Items.Count > 0)
                    cmbAnswerType.SelectedIndex = 0;
            }
        }
        #endregion
        int nQuestionType;//1- Manual,2- HistoryItem
        private void rdManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdManual.Checked == true)
            {
                rdManual.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rdManual.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }

            pnlHistoryItem.Visible = true;
            pnlAnsHistoryItem.Visible = false;



            if (rdManual.Checked)
            {
                nQuestionType = 1;
                cmbAnsHistoryItem.SelectedIndexChanged -= new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);
                ValidateHistoryCategoryControlsVisibility();
            }
        }

        private void rdHistoryItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdHistoryItem.Checked == true)
            {
                rdHistoryItem.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rdHistoryItem.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }



            if (rdHistoryItem.Checked)
            {
                nQuestionType = 2;
                cmbAnsHistoryItem.SelectedIndexChanged += new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                {
                    pnlHistoryItem.Visible = true;
                    pnlAnsHistoryItem.Visible = false;
                }
                else
                {
                    pnlHistoryItem.Visible = false;
                    pnlAnsHistoryItem.Visible = true;
                }
                ValidateHistoryCategoryControlsVisibility();
            }
        }



        private void c1Question_CellChecked(object sender, RowColEventArgs e)
        {
            if (c1Question.ColSel != COL_PFActiveInActive + 4)
                return;
            if (c1Question.Rows[c1Question.RowSel][COL_PFActiveInActive + 4].ToString() == "True")
            {
                if (MessageBox.Show("Do you want to activate selected question?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    c1Question.SetData(c1Question.RowSel, COL_PFActiveInActive + 4, "False");
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Do you want to deactivate selected question?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    c1Question.SetData(c1Question.RowSel, COL_PFActiveInActive + 4, "True");
                    return;
                }
            }

            clsHealthForm oclsHealthForm = null;
            try
            {
                oclsHealthForm = new clsHealthForm();
                DataRowView drv = ((System.Data.DataRowView)(c1Question.Rows[c1Question.RowSel].DataSource));
                oclsHealthForm.UpdateStatus(_strConnectionString, "PF_UpdateStatus", Convert.ToInt64(drv.Row.ItemArray[0]), Convert.ToBoolean(drv.Row.ItemArray[12]));

                Boolean bActive = false;
                if (drv.Row.ItemArray[12].ToString() == "True")
                    bActive = true;

                if (bActive)
                    MessageBox.Show("Selected question is activated.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Selected question is deactivated.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }
        }

        private void cmbAnswerType_Enter(object sender, EventArgs e)
        {

        }

        void cmbAnsFamilyMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdManual.Checked)
            {
                if (cmbAnsFamilyMember.SelectedItem != null)
                {
                    string strFamilyMember = Convert.ToString(((System.Data.DataRowView)(cmbAnsFamilyMember.SelectedItem)).Row.ItemArray[1]).Trim();
                    if (strFamilyMember != "Select")
                        txtAnswerLabel.Text = strFamilyMember;
                    //else
                    //    txtAnswerLabel.Text = string.Empty;
                }
            }
        }

        void cmbAnsHistoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAnsHistoryItem.SelectedItem != null)
            {
                string strHistoryItem = Convert.ToString(((System.Data.DataRowView)(cmbAnsHistoryItem.SelectedItem)).Row.ItemArray[1]).Trim();
                if (strHistoryItem != "Select")
                    txtAnswerLabel.Text = strHistoryItem;
                else
                    txtAnswerLabel.Text = string.Empty;
            }
        }

        void cmbAnsAllergies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdManual.Checked)
            {
                if (cmbAnsAllergies.SelectedItem != null)
                {
                    string strAllergies = Convert.ToString(((System.Data.DataRowView)(cmbAnsAllergies.SelectedItem)).Row.ItemArray[1]).Trim();
                    if (strAllergies != "Select")
                        txtAnswerLabel.Text = strAllergies;
                    else
                        txtAnswerLabel.Text = string.Empty;
                }
            }
        }

        private void c1Answers_Click(object sender, EventArgs e)
        {
            clsHealthForm oclsHealthForm = null;
            try
            {
                if (c1Answers.ColSel == 0)
                {
                    if (c1Answers.Rows.Count > 1)
                    {
                        if (c1Answers.RowSel > 0)
                        {
                            DialogResult Result;
                            Result = MessageBox.Show("Do you want to delete answer?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (DialogResult.Yes == Result)
                            {
                                //oclsHealthForm = new clsHealthForm();
                                ////Check if Question-Answer associated 
                                //if (oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][COL_PFALibId + 1])) == 0)
                                //{
                                //    Result = MessageBox.Show("Question is already associated in build form.\nAnswer can't be deleted.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}//End
                                //else
                                //{
                                    c1Answers.Rows.Remove(c1Answers.RowSel);
                                //}
                            }
                        }
                    }
                }
                validateonanswers();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }
        }

        private void c1Answers_DoubleClick(object sender, EventArgs e)
        {
            if (c1Answers.Rows.Count > 1 && c1Answers.RowSel != -1)
            {
                if (Convert.ToString(c1Answers.Rows[c1Answers.RowSel][3]) != "")
                {
                    _nPFAnswerId = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][1]);
                    _nPFAnsLibId = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][2]);
                    _sPFAnsAnswerLabel = Convert.ToString(c1Answers.Rows[c1Answers.RowSel][3]);
                    _bIsFollowedQuestion = Convert.ToBoolean(c1Answers.Rows[c1Answers.RowSel][4]);
                    _sPFAnsfollowedQuestionLabel = Convert.ToString(c1Answers.Rows[c1Answers.RowSel][5]);
                    _nPFAnsOrderId = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][8]);
                    _nPFAnsHistoryItemId = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][9]);
                    _nPFAnsOtherId = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][10]);

                    if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
                    {
                        cmbAnsAllergies.SelectedValue = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][10]);
                    }
                    else if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
                    {
                        cmbAnsFamilyMember.SelectedValue = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][10]);
                    }
                    else
                    {
                        cmbAnsFamilyMember.SelectedValue = -1;
                        cmbAnsAllergies.SelectedValue = -1;
                    }



                    this.cmbAnsHistoryItem.SelectedIndexChanged -= new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);
                    cmbAnsHistoryItem.SelectedValue = Convert.ToInt64(c1Answers.Rows[c1Answers.RowSel][9]);
                    this.cmbAnsHistoryItem.SelectedIndexChanged += new EventHandler(cmbAnsHistoryItem_SelectedIndexChanged);

                    txtAnswerLabel.Text = Convert.ToString(c1Answers.Rows[c1Answers.RowSel][3]);

                    if (Convert.ToBoolean(c1Answers.Rows[c1Answers.RowSel][4]))
                    {
                        rbYesFollowQ.Checked = true;
                        txtFollowedQLab.Text = Convert.ToString(c1Answers.Rows[c1Answers.RowSel][5]);
                    }
                    else
                    {
                        rbNoFollowQ.Checked = true;
                    }

                    _IsAssociatedAnswerModify = true;
                    pnlAddCancel.Visible = false;
                    pnlModifyCancel.Visible = true;
                    c1Answers.Enabled = false;

                }

            }
        }

        private void lnkModify_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lnkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnModifyOption_Click(object sender, EventArgs e)
        {
            if (ValidateOption(true))
            {
                _nPFAnsHistoryItemId = 0;
                if (rdHistoryItem.Checked)
                {
                    _nPFAnsHistoryItemId = Convert.ToInt64(cmbAnsHistoryItem.SelectedValue);
                }

                if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "All")
                {
                    _nPFAnsOtherId = Convert.ToInt64(cmbAnsAllergies.SelectedValue);
                }
                else if (getHistoryType(Convert.ToInt64(cmbHisCategory.SelectedValue)) == "Fam")
                {
                    _nPFAnsOtherId = Convert.ToInt64(cmbAnsFamilyMember.SelectedValue);
                }
                else
                {
                    _nPFAnsOtherId = 0;
                }
                ModifyAnswerGrid(_nPFAnswerId, _nPFAnsLibId, _sPFAnsAnswerLabel, _bIsFollowedQuestion, _sPFAnsfollowedQuestionLabel, _nPFAnsOrderId, _nPFAnsHistoryItemId, _nPFAnsOtherId);
                ClearAnswerControls();
                c1Answers.Enabled = true;
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAnswerControls();
            c1Answers.Enabled = true;
        }

        private void ClearAnswerControls()
        {
            cmbAnsFamilyMember.SelectedValue = -1;
            cmbAnsAllergies.SelectedValue = -1;
            cmbAnsHistoryItem.SelectedValue = -1;
            txtAnswerLabel.Text = "";
            txtFollowedQLab.Text = "";
            rbNoFollowQ.Checked = true;
            pnlAddCancel.Visible = true;
            pnlModifyCancel.Visible = false;
        }

        private void btnAddOption_Click(object sender, EventArgs e)
        {
           
                if (cmbHisCategory.SelectedIndex == 0 && rdoHistory.Checked)
                {
                    MessageBox.Show("Please select history category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisCategory.Focus();
                    return;
                }
                if (cmbHisCategory.SelectedIndex == 0 && rdoROS.Checked)
                {
                    MessageBox.Show("Please select ROS category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHisCategory.Focus();
                    return;
                }
            

            if (ValidateOption(false))
            {
                BindAnswerGrid();
                ClearAnswerControls();
            }

        }

        private void txtPublishNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = clsGeneral.IsHtmlCharcter(e.KeyChar);
        }

        private void txtPreText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = clsGeneral.IsHtmlCharcter(e.KeyChar);
        }

        private void txtPostText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = clsGeneral.IsHtmlCharcter(e.KeyChar);
        }

        private void txtAnswerLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = clsGeneral.IsHtmlCharcter(e.KeyChar);
        }

        private void txtFollowedQLab_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = clsGeneral.IsHtmlCharcter(e.KeyChar);
        }

        private void c1Question_AfterSort(object sender, SortColEventArgs e)
        {
            setimages();
        }

        private void HistoryRadioButtons_CheckedChanged(Object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RadioButton rb = (RadioButton)sender;
                bool ResetControl = false; 
                if (showConfirmation)
                {
                    if (IsValueSet())
                    {
                        if (MessageBox.Show("Existing information will be lost by changing type." + Environment.NewLine + "Would you like to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            // SET Previous radio 
                            this.rdoHistory.CheckedChanged -= new EventHandler(HistoryRadioButtons_CheckedChanged);
                            this.rdoNonHistory.CheckedChanged -= new EventHandler(HistoryRadioButtons_CheckedChanged);
                            this.rdoROS.CheckedChanged -= new EventHandler(HistoryRadioButtons_CheckedChanged); 
                            //if (rb.Name.ToString().Trim().ToUpper() == "RDOHISTORY")
                            //{
                            //    rdoNonHistory.Checked = true;
                            //    rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //    rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //}
                            //else
                            //{
                            //    rdoHistory.Checked = true;
                            //    rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //    rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //}
                            if (nRbtType==1)
                            {
                                rdoHistory.Checked = true;
                                rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else if (nRbtType==2)
                            {
                                rdoNonHistory.Checked = true;
                                rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else if (nRbtType==3)
                            {
                                rdoROS.Checked = true;
                                rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            this.rdoHistory.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged);
                            this.rdoNonHistory.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged);
                            this.rdoROS.CheckedChanged += new EventHandler(HistoryRadioButtons_CheckedChanged); 

                            // END
                            return;
                        }
                        else
                        {
                            ResetControl = true;
                        }
                    }
                }
                showConfirmation = true;
                if (rb.Name.ToString().Trim().ToUpper() == "RDOHISTORY")
                {
                    nRbtType = 1;
                    if (_fillCategoryOnRadiobuttonchange)
                    {
                        FillCategory();
                    }
                    if (rb.Checked == true)
                    {
                        IsPatientHistoryRelated = true;
                    }
                    else
                    {
                        IsPatientHistoryRelated = false;
                    }
                    rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                else if (rb.Name.ToString().Trim().ToUpper() == "RDONONHISTORY")
                {
                    nRbtType = 2;

                    if (rb.Checked == true)
                    {
                        IsPatientHistoryRelated = false;
                    }
                    else
                    {
                        IsPatientHistoryRelated = true;
                    }
                    rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

                else if (rb.Name.ToString().Trim().ToUpper() == "RDOROS")
                {
                    nRbtType = 3;
                    if (_fillCategoryOnRadiobuttonchange)
                    {
                        FillCategory("ROS");
                    }
                    if (rb.Checked == true)
                    {
                        IsPatientHistoryRelated = true;
                    }
                    //else
                    //{
                    //    IsPatientHistoryRelated = false;
                    //}
                    rdoROS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoNonHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    rdoHistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                ShowHideControl();
                if (ResetControl == true)
                {
                    ClearAll();

                    if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                    {
                        pnlQQuesType.Visible = false;
                        if (IsPatientHistoryRelated)
                        {
                            pnlHistoryItem.Visible = true;
                        }
                        else {
                            pnlHistoryItem.Visible = false;
                        }
                    }
                    AnswerShowHidePanels(false);
                    pnlAns.Visible = false;
                    pnlAnswer.Visible = false;
                }
            }
        }

        /// <summary>
        /// Show Hide Question & Answer Control
        /// </summary>
        public void ShowHideControl()
        {
            if (IsPatientHistoryRelated == true)
            {
                pnlHistoryCategory.Visible = true;
                //pnlQQuesType.Visible = true;
                //pnlHistoryItem.Visible = true;
                //pnlAnsReaction.Visible = true;
                //pnlAnsHistoryItem.Visible = true;
                //pnlAnsFamilyMember.Visible = true;

                if (cmbHisCategory.DataSource !=null)
                {
                    ValidateHistoryCategoryControlsVisibility();
                }

                if (cmbAnswerType.DataSource == null)
                {
                    cmbAnswerType.SelectedIndex = -1;
                }
                else
                {
                    if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Textbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.LongTextbox))
                    {
                        pnlQQuesType.Visible = false;
                        pnlHistoryItem.Visible = true;
                        AnswerShowHidePanels(false);
                        pnlAns.Visible = false;
                        pnlAnswer.Visible = false;
                    }
                    else if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                    {
                        pnlQQuesType.Visible = true;
                        if (rdHistoryItem.Checked)
                        {
                            pnlHistoryItem.Visible = false;
                            pnlAnsHistoryItem.Visible = true;
                        }
                        else
                        {
                            pnlHistoryItem.Visible = true;
                            pnlAnsHistoryItem.Visible = false;
                        }
                        AnswerShowHidePanels(true);
                        pnlAns.Visible = true;
                        pnlAnswer.Visible = true;
                    }
                }
            }
            else
            {
                pnlHistoryCategory.Visible = false;
                pnlQQuesType.Visible = false;
                pnlHistoryItem.Visible = false;
                pnlAnsHistoryItem.Visible = false;
                pnlAnsReaction.Visible = false;
                pnlAnsFamilyMember.Visible = false;
            }
        }
        
        /// <summary>
        /// During Radio change, check if any value set. 
        /// </summary>
        /// <returns></returns>
        public bool IsValueSet()
        {
            try
            {
                if (cmbHisCategory.DataSource != null)
                {
                    if (cmbHisCategory.SelectedIndex > 0)
                    {
                        return true;
                    }
                }
                if (cmbHisItem.DataSource != null)
                {
                    if (rdManual.Checked && cmbHisItem.SelectedIndex > 0)
                    {
                        return true;
                    }
                }
                if (txtPublishNm.Text.Trim() != "")
                {
                    return true;
                }
                if (txtPreText.Text.Trim() != "")
                {
                    return true;
                }
                if (txtPostText.Text.Trim() != "")
                {
                    return true;
                }
                if (getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Checkbox) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Radio) || getAnswerTypeValue() == Convert.ToInt16(clsHealthForm.QuestionType.Dropdown))
                {
                    if (c1Answers.Rows.Count > 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnPreGroup_Click(object sender, EventArgs e)
        {
            frmPrePost objPrePost = new frmPrePost();
            objPrePost.textItems = txtPreText.Text.ToString();
            objPrePost.strLabel = "Pre text for Question";
            objPrePost.ShowDialog();
            txtPreText.Text = objPrePost.textItems;
        }

        private void btnPostGroup_Click(object sender, EventArgs e)
        {
            frmPrePost objPrePost = new frmPrePost();
            objPrePost.textItems = txtPostText.Text.ToString();
            objPrePost.strLabel = "Post text for Question";
            objPrePost.ShowDialog();
            txtPostText.Text = objPrePost.textItems;
        }

    }
}

