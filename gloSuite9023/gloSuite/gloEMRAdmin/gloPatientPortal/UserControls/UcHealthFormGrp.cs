using System;
using System.Data;
using System.Windows.Forms;
using gloPatientPortal.Classes;
using System.Drawing;

namespace gloPatientPortal.UserControls
{
    public partial class UcHealthFormGrp : UserControl
    {
        #region Varibale declaration
        string _strConnectionString = string.Empty;
        long _nLoginID;
        long _nLibId;
        bool _IsModify = false;
        int COL_PFLibId = 0;
        int COL_PFCategoryID = 1;
        int COL_PFDescription = 2;
        int COL_PFPubName = 3;
        int COL_PFCatType = 4;
        int COL_PFPreText = 5;
        int COL_PFPostText = 6;
        int COL_PFAnswerType = 7;
        int COL_PFUserId = 8;
        int COL_PFActiveInActive = 9;
        int COL_PFDataTable = 10;
        int COL_PFIsPatientHistoryRelated = 11;
        int COL_PFDelete = 14;
        int COL_RbtType = 12;
        int COL_GroupType = 13;

        #endregion

        bool _IsNewModify = false;
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


        #region Constructor
        public UcHealthFormGrp()
        {
            InitializeComponent();
        }

        public UcHealthFormGrp(string strConnectionString, long nLoginID)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
        }
        #endregion

        #region Events
        private void UcHealthFormGrp_Load(object sender, EventArgs e)
        {
            FillCategory();
            DesignGrid();
            pnlGroup.Dock = DockStyle.None;
            ts_ShowHide.Text = "Show";
            lblHeading.Text = "Online Patient Form - Groups";
        }

        private void c1Group_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (c1Group.Rows.Count > 1 && c1Group.RowSel != -1)
            {
                c1Group.Enabled = false;
                IsNewModify = true;
                ShowGroup();
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
            DataRowView drv = ((System.Data.DataRowView)(c1Group.Rows[c1Group.RowSel].DataSource));
            if (drv != null && drv.Row.Table.Rows.Count > 0)
            {
                nQuestionAccociated = oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[0]));
            }
            if (nQuestionAccociated == 0)
            {
                cmbHisCategory.Enabled = false;
                panelHistory.Enabled = false;
            }
            else
            {
                cmbHisCategory.Enabled = true;
                panelHistory.Enabled = true;
            }
        }

        private void cmbHisCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHisCategory.SelectedItem != null)
            {
                string strCategory = Convert.ToString(((System.Data.DataRowView)(cmbHisCategory.SelectedItem)).Row.ItemArray[1]).Trim();

                if (strCategory != "Select")
                    txtPublishNm.Text = strCategory;
                else
                        txtPublishNm.Text = string.Empty;

                        txtPreText.Text = string.Empty;
                        txtPostText.Text = string.Empty;
                    
                  
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }
        private bool ValidateGroup()
        {
            //For History Category.
            if (cmbHisCategory.SelectedIndex == 0 && rbtPatientHistoryrelated.Checked)
            {
                MessageBox.Show("Please select history category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbHisCategory.Focus();
                return false;
            }
            if (cmbHisCategory.SelectedIndex == 0 && rbtROS.Checked)
            {
                MessageBox.Show("Please select ROS category.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbHisCategory.Focus();
                return false;
            }
            if (txtPublishNm.Text == "" || txtPublishNm.Text.Trim() == "")
            {
                MessageBox.Show("Please enter group label.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPublishNm.Focus();
                return false;
            }

            if (txtPublishNm.Text.Trim() != "")
            {
                if (!clsGeneral.ContainsHtml(txtPublishNm.Text.Trim(), "Group label"))
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

        private Boolean IsPatientHistoryrelated()
        {
            if (rbtPatientHistoryrelated.Checked)
                return true;
            else
                return false;
        }

        private void ts_Save_Click(object sender, EventArgs e)
        {
            if (_IsModify)
            {
                DialogResult _result = CheckQuestionFormAssociation();

                if (_result == DialogResult.Yes)
                {
                    if (!ValidateGroup())
                    {
                        return;
                    }

                    SaveGroup();
                }
                else if (_result == DialogResult.No)
                {
                    pnlGroup.Dock = DockStyle.None;
                    DesignGrid();
                    ClearAll();
                    c1Group.Enabled = true;
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
                if (!ValidateGroup())
                {
                    return;
                }

                SaveGroup();
            }
        }

        private void SaveGroup()
        {
            clsHealthForm oclsHistory = null;
            try
            {
                long Id = 0;
                oclsHistory = new clsHealthForm();
                Int64 hisCategoryID = Convert.ToInt64(cmbHisCategory.SelectedValue);
                if (hisCategoryID == -1)
                {
                    hisCategoryID = 0;
                }

                if (_IsModify != true)
                {

                    Id = oclsHistory.AddPFLibrary(hisCategoryID, txtPublishNm.Text, "G", txtPreText.Text, txtPostText.Text, 0, _nLoginID, _strConnectionString, 0, false, false, 0, 0, 0, Convert.ToBoolean(chkIsDataTable.Checked), null, IsPatientHistoryrelated(),rbtType);

                    if (Id > 0)
                    {
                        //MessageBox.Show("Group added successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DesignGrid();
                        ClearAll();
                        pnlGroup.Dock = DockStyle.None;
                    }
                    else if (Id == -1)
                        MessageBox.Show("Group already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to add group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Id = oclsHistory.AddPFLibrary(hisCategoryID, txtPublishNm.Text, "G", txtPreText.Text, txtPostText.Text, 0, _nLoginID, _strConnectionString, _nLibId, true, false, 0, 0, 0, Convert.ToBoolean(chkIsDataTable.Checked), null, IsPatientHistoryrelated(), rbtType);
                    if (Id > 0)
                    {
                        //MessageBox.Show("Group updated successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DesignGrid();
                        ClearAll();
                        pnlGroup.Dock = DockStyle.None;
                        //ts_Save.Text = "Save";
                        _IsModify = false;
                    }
                    else
                        MessageBox.Show("Failed to update group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
            c1Group.Enabled = true;
            IsNewModify = false;
        }

        public DialogResult CheckQuestionFormAssociation()
        {
            DialogResult Result = DialogResult.No;
            clsHealthForm oclsHealthForm = null;
            try
            {
                if (c1Group.Rows.Count > 1)
                {
                    DataRowView drv = ((System.Data.DataRowView)(c1Group.Rows[c1Group.RowSel].DataSource));
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

                            string message = "This group has been associated to the following active forms, any change need to re-publish the form to get this effective on the portal.\n\r" + sformNames + "\n\r\n\rYes: Will save changes and Deactivates form (need to re-publish).\n\rNo: Will discard current changes.\n\r\n\r* Re-Publish: (Online Patient Form >> Modify >> Save & Preview >> Publish)";
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
            if (ts_ShowHide.Text == "Show")
            {
                pnlGroup.Dock = DockStyle.Right;
                ts_ShowHide.Text = "&Close";
            }
            else
            {
                pnlGroup.Dock = DockStyle.None;
                ts_ShowHide.Text = "Show";
                lblHeading.Text = "Online Patient Form - Groups";
            }
            c1Group.Enabled = true;
            ClearAll();
            IsNewModify = false;
        }

        private void c1Group_Click(object sender, EventArgs e)
        {
            clsHealthForm oclsHealthForm = null;
            try
            {
                if (c1Group.Rows.Count > 1)
                {
                    DataRowView drv = ((System.Data.DataRowView)(c1Group.Rows[c1Group.RowSel].DataSource));
                    if (drv != null && drv.Row.Table.Rows.Count > 0)
                    {
                        if (c1Group.ColSel == 0)
                        {
                            DialogResult Result;
                            Result = MessageBox.Show("Do you want to delete Group?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (DialogResult.Yes == Result)
                            {
                                oclsHealthForm = new clsHealthForm();
                                Int32 nQuestionAccociated = oclsHealthForm.CheckAssociatedQuesAns(_strConnectionString, "PF_CheckQuesAnsAssociation", Convert.ToInt64(drv.Row.ItemArray[0]));

                                if (nQuestionAccociated == 1)
                                {
                                    oclsHealthForm = new clsHealthForm();
                                    oclsHealthForm.DeleteQuestion(_strConnectionString, "PF_DeleteQuestion", Convert.ToInt64(drv.Row.ItemArray[0]), "G");
                                    DesignGrid();
                                }
                                else
                                {
                                    if (Convert.ToBoolean(drv.Row.ItemArray[9]))
                                    {
                                        Result = MessageBox.Show("Group already associated,do you want to Inactive?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DialogResult.Yes == Result)
                                        {
                                            oclsHealthForm = new clsHealthForm();
                                            oclsHealthForm.DeleteQuestion(_strConnectionString, "PF_DeleteQuestion", Convert.ToInt64(drv.Row.ItemArray[0]), "G");
                                            DesignGrid();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Group is already associated in build form.\nGroup can't be deleted.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                }

                                c1Group.ColSel = 1;
                            }
                        }
                        else if (c1Group.ColSel == 10)
                        {
                            oclsHealthForm = new clsHealthForm();
                            oclsHealthForm.UpdateStatus(_strConnectionString, "PF_UpdateStatus", Convert.ToInt64(drv.Row.ItemArray[0]), Convert.ToBoolean(drv.Row.ItemArray[9]));
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
        #endregion

        #region Methods
        /// <summary>
        //Fill history category
        /// <summary>
        private void FillCategory()
        {
            clsHealthForm oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                //Load history category
                oclsHistory = new clsHealthForm();
                if (rbtPatientHistoryrelated.Checked)
                {
                    dtHisCategory = oclsHistory.FillControls(_strConnectionString, "History");

                }
                else if (rbtROS.Checked)
                {
                    dtHisCategory = oclsHistory.FillControls(_strConnectionString, "ROS");

                }

                if (dtHisCategory != null && dtHisCategory.Rows.Count > 0)
                {
                    DataRow dr = dtHisCategory.NewRow();
                    dr["nCategoryId"] = -1;
                    dr["sDescription"] = "Select";

                    dtHisCategory.Rows.InsertAt(dr, 0);
                    this.cmbHisCategory.SelectedIndexChanged -= new EventHandler(cmbHisCategory_SelectedIndexChanged);

                    cmbHisCategory.DataSource = dtHisCategory.DefaultView;
                    cmbHisCategory.DisplayMember = "sDescription";
                    cmbHisCategory.ValueMember = "nCategoryID";
                    cmbHisCategory.SelectedIndex = 0;
                    this.cmbHisCategory.SelectedIndexChanged += new EventHandler(cmbHisCategory_SelectedIndexChanged);
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
        private void FillCategoryHistory()
        {
            clsHealthForm oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                //Load history category
                oclsHistory = new clsHealthForm();
                    dtHisCategory = oclsHistory.FillControls(_strConnectionString, "History");
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
        private void FillCategoryROS()
        {
            clsHealthForm oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                //Load ROS category
                oclsHistory = new clsHealthForm();
                    dtHisCategory = oclsHistory.FillControls(_strConnectionString, "ROS");

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
        //Design data grid
        /// <summary>
        private void DesignGrid()
        {
            gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
            objgloC1FlexStyle.Style(c1Group);
            clsHealthForm oclsHistory = null;
            c1Group.Cols.Count = 14;
            try
            {
                oclsHistory = new clsHealthForm();

                c1Group.SetData(0, COL_PFLibId, "nPFLibId");
                c1Group.Cols[COL_PFLibId].Width = 0;
                c1Group.Cols[COL_PFLibId].AllowEditing = false;

                c1Group.SetData(0, COL_PFCategoryID, "nCategoryID");
                c1Group.Cols[COL_PFCategoryID].Width = 0;
                c1Group.Cols[COL_PFCategoryID].AllowEditing = false;

                c1Group.SetData(0, COL_PFPubName, "Publish name");
                c1Group.Cols[COL_PFPubName].AllowEditing = false;

                c1Group.SetData(0, COL_PFCatType, "Category type");
                c1Group.Cols[COL_PFCatType].AllowEditing = false;

                c1Group.SetData(0, COL_PFPreText, "Pre text");
                c1Group.Cols[COL_PFPreText].AllowEditing = false;

                c1Group.SetData(0, COL_PFPostText, "Post text");
                c1Group.Cols[COL_PFPostText].AllowEditing = false;

                c1Group.SetData(0, COL_PFAnswerType, "nAnswerType");
                c1Group.Cols[COL_PFAnswerType].Width = 0;
                c1Group.Cols[COL_PFAnswerType].AllowEditing = false;

                c1Group.SetData(0, COL_PFUserId, "nUserId");
                c1Group.Cols[COL_PFUserId].Width = 0;
                c1Group.Cols[COL_PFUserId].AllowEditing = false;

                c1Group.SetData(0, COL_PFDescription, "Category");//HD
                c1Group.Cols[COL_PFDescription].AllowEditing = false;

                c1Group.SetData(0, COL_PFDataTable, "Data table");
                c1Group.Cols[COL_PFDataTable].AllowEditing = false;

                //c1Group.SetData(0, COL_PFIsPatientHistoryRelated, "bIsPatientHistoryrelated");
                //c1Group.Cols[COL_PFIsPatientHistoryRelated].Caption = "History related";

                DataTable dt = oclsHistory.GetGroups(_strConnectionString);
                //if (dt != null & dt.Rows.Count > 0)
                //{
                DataColumn colButton = new DataColumn("Delete", System.Type.GetType("System.String"));
                dt.Columns.Add(colButton);

                c1Group.DataSource = dt.DefaultView;
                c1Group.Cols[COL_PFPubName].Caption = "Group label";
                c1Group.Cols[COL_PFLibId].Width = 0;
                c1Group.Cols[COL_PFLibId].AllowEditing = false;

                c1Group.Cols[COL_PFCategoryID].Width = 0;
                c1Group.Cols[COL_PFCategoryID].AllowEditing = false;
                c1Group.Cols[COL_PFAnswerType].Width = 0;
                c1Group.Cols[COL_PFAnswerType].AllowEditing = false;
                c1Group.Cols[COL_PFUserId].Width = 0;
                c1Group.Cols[COL_PFUserId].AllowEditing = false;
                c1Group.Cols[COL_PFCatType].Width = 0;
                c1Group.Cols[COL_PFCatType].AllowEditing = false;

                c1Group.Cols[COL_PFPubName].Width = Convert.ToInt32(Width * 0.22);
                c1Group.Cols[COL_PFPubName].AllowEditing = false;
                c1Group.Cols[COL_PFPreText].Width = Convert.ToInt32(Width * 0.16);
                c1Group.Cols[COL_PFPreText].AllowEditing = false;
                c1Group.Cols[COL_PFPostText].Width = Convert.ToInt32(Width * 0.16);
                c1Group.Cols[COL_PFPostText].AllowEditing = false;
                c1Group.Cols[COL_PFDescription].Width = Convert.ToInt32(Width * 0.10);
                c1Group.Cols[COL_PFDescription].AllowEditing = false;
                c1Group.Cols[COL_PFDataTable].Width = Convert.ToInt32(Width * 0.08);
                //c1Group.Cols[COL_PFActiveInActive].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop;
                c1Group.Cols[COL_PFActiveInActive].Width = Convert.ToInt32(Width * 0.05);
                c1Group.Cols[COL_PFIsPatientHistoryRelated].Caption = "History Related";
                c1Group.Cols[COL_PFDescription].Caption = "Category";
                c1Group.Cols[COL_PFIsPatientHistoryRelated].AllowEditing = false;
                c1Group.Cols[COL_PFIsPatientHistoryRelated].Width = 0;// Convert.ToInt32(Width * 0.14);HD
                c1Group.Cols[COL_PFIsPatientHistoryRelated].Visible = false;//HD

                c1Group.Cols[COL_RbtType].AllowEditing = false;
                c1Group.Cols[COL_RbtType].Width = 0;
                c1Group.Cols[COL_RbtType].Visible = false;
                c1Group.Cols[COL_GroupType].AllowEditing = false;
                c1Group.Cols[COL_GroupType].Width = Convert.ToInt32(Width * 0.08);

                c1Group.Cols[COL_PFDelete].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;

                if (dt.Rows.Count > 1)
                {
                    C1.Win.C1FlexGrid.CellStyle cStyle = c1Group.Styles.Add("Button");
                    C1.Win.C1FlexGrid.CellRange rgReaction = c1Group.GetCellRange(1, COL_PFDelete, dt.Rows.Count, COL_PFDelete);
                    rgReaction.Style = cStyle;

                }
                for (int i = 1; i <= dt.Rows.Count; i++)
                    c1Group.SetCellImage(i, COL_PFDelete, imgList.Images[0]);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Data Table"].ToString().ToLower() == "true")
                    {
                        c1Group.SetData(i + 1, COL_PFDataTable, "True");
                    }
                    else
                    {
                        c1Group.SetData(i + 1, COL_PFDataTable, "False");
                    }
                }

                //c1Group.Cols[COL_PFDelete].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;
                c1Group.Cols[COL_PFDelete].Width = 48;
                c1Group.Cols[COL_PFDelete].AllowEditing = false;
                c1Group.Cols[COL_PFDelete].AllowResizing = false;
                c1Group.Cols[COL_PFDelete].Move(0);
                c1Group.Cols[COL_PFDataTable + 1].AllowEditing = false;
                c1Group.Cols[COL_GroupType+1].Move(1);//HD

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

                DataView dvGroup = null;
                if (c1Group.DataSource != null)
                {
                    dvGroup = (DataView)c1Group.DataSource;
                    if (dvGroup != null && dvGroup.Table.Rows.Count > 0)
                    {
                        dvGroup.RowFilter = "[Type] Like '%" + strSearch.Trim().Replace("'", "''") + "%' OR [History Category] Like '%" + strSearch.Trim().Replace("'", "''") + "%' OR [Publish Name] Like '%" + strSearch.Trim().Replace("'", "''") + "%' ";
                        c1Group.DataSource = dvGroup;

                        if (dvGroup.ToTable().Rows.Count > 0)
                        {
                            C1.Win.C1FlexGrid.CellStyle cStyle = c1Group.Styles.Add("Button");
                            C1.Win.C1FlexGrid.CellRange rgReaction = c1Group.GetCellRange(1, COL_PFDelete, dvGroup.ToTable().Rows.Count, COL_PFDelete);
                            rgReaction.Style = cStyle;
                            for (int i = 1; i <= dvGroup.ToTable().Rows.Count; i++)
                                c1Group.SetCellImage(i, 0, imgList.Images[0]);
                        }
                    }
                }



                SetModifyEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetModifyEnabled()
        {
            bool IsModifyToEnable = false;

            if (c1Group.Rows.Count > 1)
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

        /// <summary>
        //Clear controls
        /// <summary>
        public void ClearAll()
        {
           
            cmbHisCategory.SelectedIndex = 0;
            txtPublishNm.Text = string.Empty;
            txtPostText.Text = string.Empty;
            txtPreText.Text = string.Empty;
            chkIsDataTable.Checked = false;
        }

        /// <summary>
        //Add new Group
        /// <summary>
        public void AddNew()
        {
            FillCategory();
            ClearAll();
            ts_Save.Text = "Save";
            pnlGroup.Dock = DockStyle.Right;
            ts_ShowHide.Text = "&Close";
            lblHeading.Text = "Online Patient Form - New group";
            //Fixed bug id 48065 on 20130326
            _IsModify = false;
            this.rbtPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
            this.rbtNonPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
            this.rbtROS.CheckedChanged -= new EventHandler(rbROS_CheckedChanged);

            rbtPatientHistoryrelated.Checked = true;
            FillCategory();
            rbtType = 1;
            this.rbtPatientHistoryrelated.CheckedChanged += new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
            this.rbtNonPatientHistoryrelated.CheckedChanged += new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
            this.rbtROS.CheckedChanged += new EventHandler(rbROS_CheckedChanged);
            c1Group.Enabled = false;

            rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
            rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
            rbtROS.Font = new Font("Tahoma", 9, FontStyle.Regular);

            if (rbtNonPatientHistoryrelated.Checked)
            {
                rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = false;
                cmbHisCategory.SelectedIndex = 0;
            }
            if (rbtPatientHistoryrelated.Checked)
            {
                rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = true;
            }
            if (rbtROS.Checked)
            {
                rbtROS.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = false;
                cmbHisCategory.SelectedIndex = 0;
            }
            cmbHisCategory.Enabled = true;
            panelHistory.Enabled = true;
        }

        /// <summary>
        //Show Group
        /// <summary>
        public Int64 ShowGroup()
        {
            FillCategory();
            if (c1Group.Rows.Count > 1 && c1Group.RowSel != -1)
            {
                lblHeading.Text = "Online Patient Form - Modify group";
                DataRowView drv = ((System.Data.DataRowView)(c1Group.Rows[c1Group.RowSel].DataSource));
                if (drv != null && drv.Row.Table.Rows.Count > 0)
                {
                    ts_Save.Text = "&Save";
                    pnlGroup.Dock = DockStyle.Right;
                    ts_ShowHide.Text = "&Close";
                    _nLibId = Convert.ToInt64(drv.Row.ItemArray[0]);
                    if (Convert.ToInt32(drv.Row.ItemArray[12]) == 3)
                    {
                        FillCategoryROS();
                        cmbHisCategory.SelectedValue = drv.Row.ItemArray[1];
                    }
                    else if (Convert.ToBoolean(drv.Row.ItemArray[11]))
                    {
                        FillCategoryHistory();
                        cmbHisCategory.SelectedValue = drv.Row.ItemArray[1];
                    }
                    txtPublishNm.Text = Convert.ToString(drv.Row.ItemArray[3]);
                    txtPreText.Text = Convert.ToString(drv.Row.ItemArray[5]);
                    txtPostText.Text = Convert.ToString(drv.Row.ItemArray[6]);

                    if (Convert.ToBoolean(drv.Row.ItemArray[10]))
                        chkIsDataTable.Checked = true;
                    else
                        chkIsDataTable.Checked = false;
                    if (Convert.ToInt32(drv.Row.ItemArray[12]) == 3)
                    {
                        this.rbtPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                        this.rbtNonPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                        this.rbtROS.CheckedChanged -= new EventHandler(rbROS_CheckedChanged);
                        rbtROS.Checked = true;
                        rbtType = 3;
                        this.rbtPatientHistoryrelated.CheckedChanged += new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                        this.rbtNonPatientHistoryrelated.CheckedChanged += new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                        this.rbtROS.CheckedChanged += new EventHandler(rbROS_CheckedChanged);
                    }
                    else
                    {

                        if (Convert.ToBoolean(drv.Row.ItemArray[11]))
                        {
                            this.rbtPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                            this.rbtNonPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                            this.rbtROS.CheckedChanged -= new EventHandler(rbROS_CheckedChanged);

                            rbtPatientHistoryrelated.Checked = true;
                            rbtType = 1;
                            this.rbtPatientHistoryrelated.CheckedChanged += new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                            this.rbtNonPatientHistoryrelated.CheckedChanged += new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                            this.rbtROS.CheckedChanged += new EventHandler(rbROS_CheckedChanged);

                        }
                        else
                        {
                            this.rbtNonPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                            this.rbtPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                            rbtNonPatientHistoryrelated.Checked = true;
                            this.rbtROS.CheckedChanged -= new EventHandler(rbROS_CheckedChanged);

                            rbtType = 2;
                            this.rbtNonPatientHistoryrelated.CheckedChanged += new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                            this.rbtPatientHistoryrelated.CheckedChanged += new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                            this.rbtROS.CheckedChanged += new EventHandler(rbROS_CheckedChanged);

                        }
                    }

                    _IsModify = true;

                    rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
                    rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
                    rbtROS.Font = new Font("Tahoma", 9, FontStyle.Regular);


                    if (rbtNonPatientHistoryrelated.Checked)
                    {

                        rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        panel6.Visible = false;
                        cmbHisCategory.SelectedIndex = 0;

                    }
                    if (rbtPatientHistoryrelated.Checked)
                    {
                        rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        panel6.Visible = true;
                    }
                    if (rbtROS.Checked)
                    {
                         rbtROS.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        panel6.Visible = true;
                      
                    }

                    cmbHisCategory.Focus();
                }
                else
                {
                    ts_Save.Text = "&Save";
                    pnlGroup.Dock = DockStyle.Right;
                    ts_ShowHide.Text = " &Close";
                    cmbHisCategory.Focus();
                }
                return _nLibId;
            }
            else
                return 0;
        }
        #endregion

        private void c1Group_CellChecked(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (c1Group.ColSel != COL_PFActiveInActive + 2)
                return;
            if (c1Group.Rows[c1Group.RowSel][COL_PFActiveInActive + 2].ToString() == "True")
            {
                if (MessageBox.Show("Do you want to activate selected group?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    c1Group.SetData(c1Group.RowSel, COL_PFActiveInActive + 2, "False");
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Do you want to deactivate selected group?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    c1Group.SetData(c1Group.RowSel, COL_PFActiveInActive + 2, "True");
                    return;
                }
            }

            clsHealthForm oclsHealthForm = null;
            try
            {
                oclsHealthForm = new clsHealthForm();
                DataRowView drv = ((System.Data.DataRowView)(c1Group.Rows[c1Group.RowSel].DataSource));
                oclsHealthForm.UpdateStatus(_strConnectionString, "PF_UpdateStatus", Convert.ToInt64(drv.Row.ItemArray[COL_PFLibId]), Convert.ToBoolean(drv.Row.ItemArray[COL_PFActiveInActive]));

                Boolean bActive = false;
                if (drv.Row.ItemArray[COL_PFActiveInActive].ToString() == "True")
                    bActive = true;
                if (bActive)
                    MessageBox.Show("Selected group is activated.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Selected group is deactivated.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmHelpinfo objhelp = new frmHelpinfo(1);
            objhelp.StartPosition = FormStartPosition.CenterParent;
            //objhelp.Location = new Point(Convert.ToInt32(this.ClientSize.Width / 4), Convert.ToInt32(this.ClientSize.Height / 1.3));
            //objhelp.Anchor = AnchorStyles.None;
            objhelp.ShowDialog();

        }

        private void rbtNonPatientHistoryrelated_CheckedChanged(object sender, EventArgs e)
        {
           
            if (rbtNonPatientHistoryrelated.Checked)
            {
                SetHistoryItem();
                            
            }
            if (rbtNonPatientHistoryrelated.Checked)
            {
                rbtType = 2;
            }
        }
        int rbtType = 0;
     
        private void SetHistoryItem()
        {
            //if (rbtNonPatientHistoryrelated.Checked)
            //{
                if (cmbHisCategory.SelectedIndex > 0 || txtPublishNm.Text.Trim() != "" || txtPreText.Text.Trim() != "" || txtPostText.Text.Trim() != "")
                {
                    if (MessageBox.Show("Existing information will be lost by changing type." + Environment.NewLine + "Would you like to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        txtPublishNm.Text = string.Empty;
                        txtPreText.Text = string.Empty;
                        txtPostText.Text = string.Empty;
                        cmbHisCategory.SelectedIndex = 0;
                        chkIsDataTable.Checked = false;
                    }
                    else
                    {
                        this.rbtPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                        this.rbtNonPatientHistoryrelated.CheckedChanged -= new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                        this.rbtROS.CheckedChanged -= new EventHandler(rbROS_CheckedChanged);
                        if (rbtType == 1)
                        {
                            rbtPatientHistoryrelated.Checked = true;
                        }
                        else if (rbtType == 3)
                        {
                            rbtROS.Checked = true;
                        }
                        else if (rbtType == 2)
                        {
                            rbtNonPatientHistoryrelated.Checked = true;
                        }
                        this.rbtPatientHistoryrelated.CheckedChanged += new EventHandler(rbtPatientHistoryrelated_CheckedChanged);
                        this.rbtNonPatientHistoryrelated.CheckedChanged += new EventHandler(rbtNonPatientHistoryrelated_CheckedChanged);
                        this.rbtROS.CheckedChanged += new EventHandler(rbROS_CheckedChanged);
                        return;
                    }

                }
           // }



            rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
            rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Regular);
            rbtROS.Font = new Font("Tahoma", 9, FontStyle.Regular);


            if (rbtNonPatientHistoryrelated.Checked)
            {
                rbtNonPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = false;
                this.cmbHisCategory.SelectedIndexChanged -= new EventHandler(cmbHisCategory_SelectedIndexChanged);
                cmbHisCategory.SelectedIndex = 0;
                this.cmbHisCategory.SelectedIndexChanged += new EventHandler(cmbHisCategory_SelectedIndexChanged);
               

            }
            if (rbtPatientHistoryrelated.Checked)
            {
                rbtPatientHistoryrelated.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = true;
            }
            if (rbtROS.Checked)
            {
                rbtROS.Font = new Font("Tahoma", 9, FontStyle.Bold);
                panel6.Visible = true;
            }

        }

        private void rbtPatientHistoryrelated_CheckedChanged(object sender, EventArgs e)
        {
          
            if (rbtPatientHistoryrelated.Checked)
            {
                SetHistoryItem();
            }
            if (rbtPatientHistoryrelated.Checked)
            {
                FillCategory();
                rbtType = 1;
            }
        }
        private void rbROS_CheckedChanged(object sender, EventArgs e)
        {
         
            if (rbtROS.Checked)
            {
                SetHistoryItem();
            }
            if (rbtROS.Checked)
            {
                FillCategory();
                rbtType = 3;
            }
              

        }

        private void btnPostGroup_Click(object sender, EventArgs e)
        {
            frmPrePost objPrePost = new frmPrePost();
            objPrePost.textItems = txtPostText.Text.ToString();
            objPrePost.strLabel = "Post text for Groups";
            objPrePost.ShowDialog();
            txtPostText.Text = objPrePost.textItems;
        }

        private void btnPreGroup_Click(object sender, EventArgs e)
        {
            frmPrePost objPrePost = new frmPrePost();
            objPrePost.textItems = txtPreText.Text.ToString();
            objPrePost.strLabel = "Pre text for Groups";
            objPrePost.ShowDialog();
            txtPreText.Text = objPrePost.textItems;
        }


    }
}
