using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatientPortal.Classes;
using C1.Win.C1FlexGrid;
using System.IO;


namespace gloPatientPortal.UserControls
{
    public partial class UcHealthFormAddEdit : UserControl
    {

        #region "Variable Declaration"

        // Connection String for the database
        string _strConnectionString = string.Empty;
         string _strDMSConnectionString = string.Empty;
        // Unique Id of user logged in
        long _nLoginID = 0;
        long _nClinicID = 1;
        // Unique Id of Patient form 
        long _nPFListId = 0;
        // Flag to indicate whether the form is opened in Add/Edit Mode
        bool _IsModify = false;

        // Column Indec for C1Association Grid
        int ncolToRemoveFromGrid = 0;
        int ncolGroup = 1;
        int ncolQuestion = 2;
        int ncolIsRequired = 3;
        int ncolIsActive = 4;
        int ncolOrder = 5;
        int ncolPageNo = 6;
        int ncolnPFLibId = 7;
        int ncolsCategoryType = 8;
        int ncolnGroupId = 9;
        int ncolPageNo1 = 10;
        bool callValidation = true;

        // Flag to indicate if cell change event functionality to execute or not
        Boolean validatecellchange = true;

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

        #region "Constructors & Load"

        //default Constructor
        public UcHealthFormAddEdit()
        {
            InitializeComponent();
        }

        //Constructor for initializing Connection String & Unique Id for logged in User
        public UcHealthFormAddEdit(string strConnectionString, long nLoginID, string strDMSConnectionString)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
             _strDMSConnectionString =  strDMSConnectionString;
            
        }

        //Load Event
        private void UcHealthFrmFormBinding_Load(object sender, EventArgs e)
        {
            if (!_IsModify)
            {
                lblHeading1.Text = "Online Patient Forms";
                ts_New.Visible = false;
            }
            c1AssociationDetails.Rows.Add();
            DesignC1AssociationGrid();
            callValidation = false;
            cmbDownloadFormat.SelectedIndex = 0;
            callValidation = true;
            FillComboGroups();
            FillComboQuestions();
            
            FillDMScategory();
            if (tvGroups.Nodes.Count > 0)
            {
                tvGroups.SelectedNode = tvGroups.Nodes[0];
                tvGroups.Select();
                fillQuestion(tvGroups.SelectedNode);
            }
            TxtHealthFormName.Focus();
        }

        #endregion

        #region "Methods and Functions"

        //Form Add Edit
        public void FormAddEdit(long nPFListId, Boolean IsModify)
        {
            //cmbGroups.SelectedValue = "-1";
            //cmbQuestions.SelectedValue = "-1";
            FillDMScategory();
            _nPFListId = nPFListId;
            _IsModify = IsModify;
            if (!_IsModify)
            {
                panel1.Hide();
                panel2.Hide();
                panel5.Show();
                ts_New.Visible = false;

            }
            else
            {
                txtSearchGroup.Text = "";
                txtSearchQuestion.Text = "";
                SmallForm();
                ts_New.Visible = true;

                DesignC1AssociationGrid();
                FillAssociation();
                if (tvGroups.Nodes.Count > 0)
                {
                    tvGroups.SelectedNode = tvGroups.Nodes[0];
                    tvGroups.Select();
                    fillQuestion(tvGroups.SelectedNode);
                }
            }

            ts_Save.Visible = true;
            ts_Publish.Visible = false;
            ts_btnClose.Visible = false;
            pnlwebbrowser.Visible = false;


        }

        public void SmallForm()
        {
            panel1.Show();
            panel2.Show();
            panel5.Show();
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            panel5.Dock = DockStyle.Top;
            cbActive.Visible = false;
            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
            //Changes done for newly added column
            cbEnableTaskNotification.Visible = false;
            label22.Text = "Name:";
            label46.Left = 10;
            label22.Left = (label46.Left + label46.Width) + 5;
            TxtHealthFormName.Left = (label22.Left + label22.Width) + 5;
            //TxtHealthFormName.Width = (TxtHealthFormName.Width - 15);
            TxtHealthFormName.Width = (panel5.Width/100)*25;
            label16.Top = label22.Top;
            cmbDownloadFormat.Top = TxtHealthFormName.Top;
            label16.Left = (TxtHealthFormName.Left + TxtHealthFormName.Width) + 10;
            cmbDownloadFormat.Left = (label16.Left + label16.Width) + 5;
            cmbDownloadFormat.Width = (panel5.Width / 100) * 25;
            lblDMSCategory.Top = label22.Top;
            lblDMSCategory.Left = (cmbDownloadFormat.Left + cmbDownloadFormat.Width) + 10;
            cmbDMScategory.Top = TxtHealthFormName.Top;
            cmbDMScategory.Left = (lblDMSCategory.Left + lblDMSCategory.Width) + 5;
            //cmbDMScategory.Width = (cmbDMScategory.Width - 15);
            cmbDMScategory.Width = (panel5.Width / 100) * 20;
            panel5.Height = 50;
        }

        private void txtSearchGroup_TextChanged(object sender, EventArgs e)
        {
            FillComboGroups();
            tvQuestions.Nodes.Clear();

        }
        DataTable dtGroups = null;

        //Fill Groups in the ComboList

        private Int64 getItemType()
        {
            if (cmbDownloadFormat.SelectedIndex == 3)
                return 0;
            else
                return 1;
        }

        private void FillComboGroups()
        {
            tvGroups.Nodes.Clear();
            DataRow[] arrdr = null;
            clsHealthForm oclsHealthForm = null;
            try
            {
                //Load history category
                oclsHealthForm = new clsHealthForm();
                dtGroups = oclsHealthForm.GetGroups(_strConnectionString);
                //if (dtGroups != null && dtGroups.Rows.Count > 0)
                //{
                //    DataRow dr = dtGroups.NewRow();
                //    dr["nPFLibID"] = -1;
                //    dr["Publish Name"] = "Select Group";

                //    dtGroups.Rows.InsertAt(dr, 0);
                //    dr = null;
                //    cmbGroups.DataSource = dtGroups.DefaultView;
                //    cmbGroups.DisplayMember = "Publish Name";
                //    cmbGroups.ValueMember = "nPFLibID";
                //    cmbGroups.SelectedIndex = 0;
                //}

                if (dtGroups != null)
                {
                    if (txtSearchGroup.Text != "")
                        if (getItemType() == 0)
                            arrdr = dtGroups.Select("[Publish Name] like '%" + txtSearchGroup.Text + "%' and Active = 1 and nGroupType = 2 ");
                        else
                            arrdr = dtGroups.Select("[Publish Name] like '%" + txtSearchGroup.Text + "%' and Active = 1 and (nGroupType = 1 or nGroupType = 3)");
                    else
                        if (getItemType() == 0)
                            arrdr = dtGroups.Select("Active = 1 and nGroupType = 2 ");
                        else
                            arrdr = dtGroups.Select("Active = 1 and (nGroupType = 1 or nGroupType = 3)");

                    for (int i = 0; i <= arrdr.Length - 1; i++)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = arrdr[i]["Publish Name"].ToString();
                        tn.Tag = arrdr[i]["nPFLibID"].ToString();
                        tvGroups.Nodes.Add(tn);
                        tn = null;
                    }
                }

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
                //if (dtGroups != null)
                //{
                //    dtGroups.Dispose();
                //    dtGroups = null;
                //}
                if (arrdr != null)
                    arrdr = null;
            }
        }

        private void txtSearchQuestion_TextChanged(object sender, EventArgs e)
        {
            FillComboQuestions();
        }

        private void FillDMScategory()
        {
            clsHealthForm oclsHealthForm = null;
            DataTable dtDMScategory = null;
            try
            {
                oclsHealthForm = new clsHealthForm();
                dtDMScategory = oclsHealthForm.GetDMSCategory(_strDMSConnectionString,true);
                if (dtDMScategory != null)
                {
                    cmbDMScategory.DataSource = dtDMScategory;
                    cmbDMScategory.DisplayMember = "CategoryName";
                    cmbDMScategory.ValueMember = "CategoryId";
                    cmbDMScategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
            }
        }

        //Fill Questions in the ComboList
        private void FillComboQuestions()
        {
            //single tree node not visible issue.
            //tvQuestions.Nodes.Clear();
            DataRow[] arrdr = null;
            clsHealthForm oclsHealthForm = null;
            DataTable dtQuestions = null;
            try
            {
                //Load history category
                oclsHealthForm = new clsHealthForm();
                dtQuestions = oclsHealthForm.GetQuestions(_strConnectionString);
                //if (dtQuestions != null && dtQuestions.Rows.Count > 0)
                //{
                //    DataRow dr = dtQuestions.NewRow();
                //    dr["nPFLibId"] = -1;
                //    dr["Publish Name"] = "Select Questions";

                //    dtQuestions.Rows.InsertAt(dr, 0);
                //    dr = null;
                //    cmbQuestions.DataSource = dtQuestions.DefaultView;
                //    cmbQuestions.DisplayMember = "Publish Name";
                //    cmbQuestions.ValueMember = "nPFLibId";
                //    cmbQuestions.SelectedIndex = 0;
                //}
                //

                if (dtQuestions != null)
                {

                    if (getItemType() == 0)
                    {

                        if (txtSearchQuestion.Text.Trim() != "")
                            if (getItemType() == 0)
                                arrdr = dtQuestions.Select("[Publish Name] like '%" + txtSearchQuestion.Text + "%' and Active = 1 and nGroupType = 2 ");
                            else
                                arrdr = dtQuestions.Select("[Publish Name] like '%" + txtSearchQuestion.Text + "%' and Active = 1 and (nGroupType = 1 or nGroupType = 3)");
                        else
                            if (getItemType() == 0)
                                arrdr = dtQuestions.Select("Active = 1 and nGroupType = 2 ");
                            else
                                arrdr = dtQuestions.Select("Active = 1 and (nGroupType = 1 or nGroupType = 3)");
                    }
                    else
                    {

                        if (nCategoryId == 0)
                        {
                            if (txtSearchQuestion.Text.Trim() != "")
                                if (getItemType() == 0)
                                    arrdr = dtQuestions.Select("[Publish Name] like '%" + txtSearchQuestion.Text + "%' and Active = 1 and nGroupType = 2 ");
                                else
                                    arrdr = dtQuestions.Select("[Publish Name] like '%" + txtSearchQuestion.Text + "%' and Active = 1 and (nGroupType = 1 or nGroupType = 3)");

                            else
                                if (getItemType() == 0)
                                    arrdr = dtQuestions.Select("Active = 1 and nGroupType = 2 ");
                                else
                                    arrdr = dtQuestions.Select("Active = 1 and (nGroupType = 1 or nGroupType = 3)");

                        }
                        else
                        {

                            if (nCategoryId != 0 && txtSearchQuestion.Text.Trim() == "")
                            {
                                if (getItemType() == 0)
                                    arrdr = dtQuestions.Select("[nCategoryID] = " + nCategoryId + " and Active = 1 and nGroupType = 2 ");
                                else
                                    arrdr = dtQuestions.Select("[nCategoryID] = " + nCategoryId + " and Active = 1 and (nGroupType = 1 or nGroupType = 3)");

                            }
                            else if (nCategoryId != 0 && txtSearchQuestion.Text.Trim() != "")
                            {
                                if (getItemType() == 0)
                                    arrdr = dtQuestions.Select("[nCategoryID] = " + nCategoryId + "  and Active = 1 and [Publish Name] like '%" + txtSearchQuestion.Text + "%' and nGroupType = 2 ");
                                else
                                    arrdr = dtQuestions.Select("[nCategoryID] = " + nCategoryId + "  and Active = 1 and [Publish Name] like '%" + txtSearchQuestion.Text + "%' and (nGroupType = 1 or nGroupType = 3)");

                            }
                            else
                                if (getItemType() == 0)
                                    arrdr = dtQuestions.Select("Active = 1 and nGroupType = 2 ");
                                else
                                    arrdr = dtQuestions.Select("Active = 1 and (nGroupType = 1 or nGroupType = 3)");



                        }
                    }
                    
                    //single tree node not visible issue.
                    try
                    {
                        if (tvQuestions.Nodes.Count > 0)
                        {
                            for (int i = tvQuestions.Nodes.Count - 1; i >= 0; i--)
                            {
                                tvQuestions.Nodes.RemoveAt(i);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "gloEMRAdmin");
                    }

                    for (int i = 0; i <= arrdr.Length - 1; i++)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = arrdr[i]["Publish Name"].ToString();
                        tn.Tag = arrdr[i]["nPFLibID"].ToString();
                        tvQuestions.Nodes.Add(tn);
                        tn = null;
                    }
                }

                //if (dtQuestions != null)
                //{
                //    for (int i = 0; i <= dtQuestions.Rows.Count - 1; i++)
                //    {
                //        TreeNode tn = new TreeNode();
                //        tn.Text = dtQuestions.Rows[i]["Publish Name"].ToString();
                //        tn.Tag = dtQuestions.Rows[i]["nPFLibID"].ToString();
                //        tvQuestions.Nodes.Add(tn);
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
                if (dtQuestions != null)
                {
                    dtQuestions.Dispose();
                    dtQuestions = null;
                }
                if (arrdr != null)
                    arrdr = null;
            }
        }
      
        //Adding a ComboBox to C1Grid cell
        private void AddComboToC1GridCell(string colname, int row, int col, string combolist)
        {
            CellStyle cs = c1AssociationDetails.Styles.Add(colname);
            cs.DataType = typeof(string);
            cs.ComboList = combolist;
            CellRange rg = c1AssociationDetails.GetCellRange(row, col);
            rg.Style = c1AssociationDetails.Styles[colname];
        }

        //Adding a Button to C1Grid cell
        private void AddButtonToC1GridCell(string colname, int row, int col)
        {
            CellStyle csButton = c1AssociationDetails.Styles.Add(colname);
            csButton.DataType = typeof(string);
            csButton.ComboList = "...";
            csButton.TextAlign = TextAlignEnum.LeftCenter;
            CellRange rgButton = c1AssociationDetails.GetCellRange(row, col);
            rgButton.Style = c1AssociationDetails.Styles[colname];
        }

        //Desingning C1Association grid
        private void DesignC1AssociationGrid()
        {
            gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
            objgloC1FlexStyle.Style(c1AssociationDetails);
            try
            {
                c1AssociationDetails.Cols.Count = 11;
                c1AssociationDetails.SetData(0, ncolGroup, "Group");
                c1AssociationDetails.SetData(0, ncolQuestion, "Question");
                c1AssociationDetails.SetData(0, ncolIsRequired, "Mandatory");
                c1AssociationDetails.SetData(0, ncolIsActive, "Active");
                c1AssociationDetails.SetData(0, ncolOrder, "Display Order");
                validatecellchange = false;
                c1AssociationDetails.SetData(0, ncolPageNo, "Page No");
                validatecellchange = true;
                c1AssociationDetails.SetData(0, ncolnPFLibId, "nPFLibId");
                c1AssociationDetails.SetData(0, ncolsCategoryType, "sCategoryType");
                c1AssociationDetails.SetData(0, ncolnGroupId, "nGroupId");
                c1AssociationDetails.SetData(0, ncolToRemoveFromGrid, "Delete");
                c1AssociationDetails.SetData(0, ncolPageNo1, "Page No");

                c1AssociationDetails.Cols[ncolnPFLibId].Visible = false;
                c1AssociationDetails.Cols[ncolsCategoryType].Visible = false;
                c1AssociationDetails.Cols[ncolnGroupId].Visible = false;
                c1AssociationDetails.Cols[ncolPageNo1].Visible = false;

                c1AssociationDetails.Cols[ncolGroup].AllowEditing = false;
                c1AssociationDetails.Cols[ncolGroup].Width = Convert.ToInt32(Width * 0.19);
                c1AssociationDetails.Cols[ncolQuestion].AllowEditing = false;
                c1AssociationDetails.Cols[ncolQuestion].Width = Convert.ToInt32(Width * 0.2);
                c1AssociationDetails.Cols[ncolIsRequired].Width = Convert.ToInt32(Width * 0.07);
                c1AssociationDetails.Cols[ncolIsActive].Width = Convert.ToInt32(Width * 0.05);
                c1AssociationDetails.Cols[ncolOrder].Width = Convert.ToInt32(Width * 0.08);
                c1AssociationDetails.Cols[ncolOrder].AllowEditing = false;
                c1AssociationDetails.Cols[ncolPageNo].Width = Convert.ToInt32(Width * 0.06);
                c1AssociationDetails.Cols[ncolToRemoveFromGrid].AllowEditing = false;
                c1AssociationDetails.Cols[ncolToRemoveFromGrid].Width = Convert.ToInt32(Width * 0.04);
                c1AssociationDetails.Rows[0].AllowEditing = false;


                C1.Win.C1FlexGrid.CellStyle csNumber = c1AssociationDetails.Styles.Add("cs_Number");
                csNumber.DataType = typeof(Int16);
                csNumber.Format = "0";
                c1AssociationDetails.Cols[ncolOrder].Style = csNumber;
                CellStyle csgroup = c1AssociationDetails.Styles.Add("cs_groupfont");
                csgroup.Font = new Font(c1AssociationDetails.Font, FontStyle.Bold);
                c1AssociationDetails.Cols[ncolGroup].Style = csgroup;
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

        //Fill the C1Association grid from the Database
        public void FillAssociation()
        {

            c1AssociationDetails.Rows.RemoveRange(1, c1AssociationDetails.Rows.Count - 1);

            clsHealthForm oclsHealthForm = null;
            DataTable dtHealthForms = null;
            DataTable dtHealthFormsDetail = null;
            try
            {
                oclsHealthForm = new clsHealthForm();
                dtHealthForms = oclsHealthForm.GetHealthForms(_strConnectionString, _nPFListId);
                if (dtHealthForms != null && dtHealthForms.Rows.Count > 0)
                {
                    TxtHealthFormName.Text = dtHealthForms.Rows[0]["sFormName"].ToString();
                    cbActive.Checked = Convert.ToBoolean(dtHealthForms.Rows[0]["bIsActive"]);
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    //Changes done for newly added column
                    cbEnableTaskNotification.Checked = Convert.ToBoolean(dtHealthForms.Rows[0]["bIsEnableTaskNotification"]);
                    callValidation = false;
                    cmbDownloadFormat.SelectedItem = dtHealthForms.Rows[0]["DownloadFormat"].ToString();
                    callValidation = true;
                    cmbDMScategory.SelectedValue = 0;
                    if (cmbDMScategory.DataSource != null)
                    {
                        if (dtHealthForms.Rows[0]["DMSCategoryId"] != DBNull.Value)
                        {
                            cmbDMScategory.SelectedValue = Convert.ToInt32(dtHealthForms.Rows[0]["DMSCategoryId"]);
                        }
                    }
                    DisplayHealthFormInformation(dtHealthForms.Rows[0]["CreatedDate"],dtHealthForms.Rows[0]["PublishedDate"]);
                }

                dtHealthFormsDetail = oclsHealthForm.GetHealthFormDetails(_strConnectionString, _nPFListId);
                if (dtHealthFormsDetail != null && dtHealthFormsDetail.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtHealthFormsDetail.Rows.Count - 1; i++)
                    {
                        if (dtHealthFormsDetail.Rows[i]["sCategoryType"].ToString() == "G")
                        {
                            AddGroupToC1AssociationGrid(c1AssociationDetails.Rows.Count, dtHealthFormsDetail.Rows[i]["Question"].ToString(), dtHealthFormsDetail.Rows[i]["nPFLibId"].ToString(), Convert.ToInt16(dtHealthFormsDetail.Rows[i]["Order"]), Convert.ToBoolean(dtHealthFormsDetail.Rows[i]["IsRequired"]), Convert.ToBoolean(dtHealthFormsDetail.Rows[i]["bIsBlocked"]), Convert.ToInt16(dtHealthFormsDetail.Rows[i]["nPageNo"]));
                        }
                        else if (dtHealthFormsDetail.Rows[i]["sCategoryType"].ToString() == "Q")
                        {
                            AddQuestionToC1AssociationGrid(c1AssociationDetails.Rows.Count, dtHealthFormsDetail.Rows[i]["Question"].ToString(), dtHealthFormsDetail.Rows[i]["nPFLibId"].ToString(), dtHealthFormsDetail.Rows[i]["nGroupId"].ToString(), Convert.ToInt16(dtHealthFormsDetail.Rows[i]["Order"]), Convert.ToBoolean(dtHealthFormsDetail.Rows[i]["IsRequired"]), Convert.ToBoolean(dtHealthFormsDetail.Rows[i]["bIsBlocked"]), Convert.ToInt16(dtHealthFormsDetail.Rows[i]["nPageNo"]));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHealthForm != null)
                    oclsHealthForm = null;
                if (dtHealthForms != null)
                {
                    dtHealthForms.Dispose();
                    dtHealthForms = null;
                }
                if (dtHealthFormsDetail != null)
                {
                    dtHealthFormsDetail.Dispose();
                    dtHealthFormsDetail = null;
                }
                SetPageNo();
            }

        }

        //Displaying Patient form Information
        private void DisplayHealthFormInformation(object CreatedDate, object PublishedDate)
        {
            lblHeading.Text = "";
            lblHeading1.Text = "";
            lblHeading2.Text = "";
            lblHeading.Text = "Patient form name: ";
            lblHeading.Text += TxtHealthFormName.Text.Trim().ToString();
            if (cbActive.Checked)
                lblHeading1.Text += "     Status: Active";
            else
                lblHeading1.Text += "     Status: InActive";

            if (PublishedDate == null)
                lblHeading2.Text += "     Created Date: " + CreatedDate;
            else
            {
                if (PublishedDate.ToString().Trim() == "")
                    lblHeading2.Text += "     Created Date: " + CreatedDate;
                else
                    lblHeading2.Text += "     Published Date: " + PublishedDate;
            }

            if (cbActive.Checked && PublishedDate.ToString().Trim() != "")
                pnlPublishNote.Visible = true;
            else
                pnlPublishNote.Visible = false;
            setheadersize();
        }
        private void setheadersize()
        {
            lblHeading.Width = ((panel4.Width * 50) / 100);
            lblHeading.Location = new Point(1, 1);
            lblHeading1.Width = ((panel4.Width * 20) / 100);
            lblHeading1.Location = new Point(((panel4.Width * 50) / 100), 1);
            lblHeading2.Width = ((panel4.Width * 30) / 100);
            lblHeading2.Location = new Point(((panel4.Width * 70) / 100), 1);

        }

        //Adding Association Groups to C1Association Grid
        private void AddGroupToC1AssociationGrid(int row, string group, string groupvalue, Int64 order, Boolean IsRequired, Boolean IsActive, Int64 PageNo)
        {
            c1AssociationDetails.Rows.Insert(row);
            c1AssociationDetails.SetData(row, ncolGroup, group);
           
            c1AssociationDetails.SetData(row, ncolQuestion, "");
            if (order == 0)
            {
                c1AssociationDetails.SetData(row, ncolOrder, "");
            }
            else
            {
                c1AssociationDetails.SetData(row, ncolOrder, order);
            }
            c1AssociationDetails.SetData(row, ncolnPFLibId, groupvalue);
            c1AssociationDetails.SetData(row, ncolsCategoryType, "G");
            c1AssociationDetails.SetData(row, ncolnGroupId, 0);

            AddComboToC1GridCell("IsRequired", row, ncolIsRequired, "Yes|No");
            if (IsRequired)
                c1AssociationDetails.SetData(row, ncolIsRequired, "Yes");
            else
                c1AssociationDetails.SetData(row, ncolIsRequired, "No");
            
            AddComboToC1GridCell("IsActive", row, ncolIsActive, "Yes|No");
            if (IsActive)
                c1AssociationDetails.SetData(row, ncolIsActive, "Yes");
            else
                c1AssociationDetails.SetData(row, ncolIsActive, "No");
            //AddButtonToC1GridCell("Button", row, ncolRemoveBtn);
            c1AssociationDetails.SetCellImage(row, ncolToRemoveFromGrid, imgList.Images[0]);
            string strpageno = "";
            Int64 temppageno = 1;
            for (int i = 1; i <= PageNo; i++)
            {
                temppageno = i;
                strpageno += temppageno.ToString() + "|";
            }
            for (int i = row + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    if (temppageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                    {
                        continue;
                    }
                    else
                    {
                        temppageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                        strpageno += temppageno.ToString() + "|";
                    }
                }
            }
            //if (temppageno < 10)
            {
                temppageno += 1;
                strpageno += temppageno.ToString();
            }
            AddComboToC1GridCell("nPageNo", row, ncolPageNo, strpageno);
            validatecellchange = false;
            c1AssociationDetails.SetData(row, ncolPageNo, PageNo);
            c1AssociationDetails.SetData(row, ncolPageNo1, PageNo);
            validatecellchange = true;
            
        }

        //Adding Association Question to C1Association Grid
        private void AddQuestionToC1AssociationGrid(int row, string Question, string Questionvalue, string groupvalue, Int64 order, Boolean IsRequired, Boolean IsActive, int PageNo)
        {
            c1AssociationDetails.Rows.Insert(row);
            c1AssociationDetails.SetData(row, ncolGroup, "");
            c1AssociationDetails.SetData(row, ncolQuestion, Question);
            if (order == 0)
            {
                c1AssociationDetails.SetData(row, ncolOrder, "");
            }
            else
            {
                c1AssociationDetails.SetData(row, ncolOrder, order);
            }
            c1AssociationDetails.SetData(row, ncolnPFLibId, Questionvalue);
            c1AssociationDetails.SetData(row, ncolsCategoryType, "Q");
            c1AssociationDetails.SetData(row, ncolnGroupId, groupvalue);

            AddComboToC1GridCell("IsRequired", row, ncolIsRequired, "Yes|No");
            if (IsRequired)
                c1AssociationDetails.SetData(row, ncolIsRequired, "Yes");
            else
                c1AssociationDetails.SetData(row, ncolIsRequired, "No");
            AddComboToC1GridCell("IsActive", row, ncolIsActive, "Yes|No");
            if (IsActive)
                c1AssociationDetails.SetData(row, ncolIsActive, "Yes");
            else
                c1AssociationDetails.SetData(row, ncolIsActive, "No");
            //AddButtonToC1GridCell("Button", row, ncolRemoveBtn);
            c1AssociationDetails.SetCellImage(row, ncolToRemoveFromGrid, imgList.Images[0]);
        }

        //Save Association
        private long SaveAssociation()
        {
            long id = 0;
            DataTable dtAssociation = null;
            clsHealthForm oclsHealthForm = null;
            try
            {
                dtAssociation = new DataTable();
                dtAssociation.Columns.Add("nPFAssociationId", System.Type.GetType("System.Int64"));
                dtAssociation.Columns.Add("nPFLibId", System.Type.GetType("System.Int64"));
                dtAssociation.Columns.Add("sCategoryType", System.Type.GetType("System.String"));
                dtAssociation.Columns.Add("nParentId", System.Type.GetType("System.Int64"));
                dtAssociation.Columns.Add("bIsRequired", System.Type.GetType("System.Boolean"));
                dtAssociation.Columns.Add("nOrderId", System.Type.GetType("System.Int64"));
                dtAssociation.Columns.Add("bIsBlocked", System.Type.GetType("System.Boolean"));
                dtAssociation.Columns.Add("nPageNo", System.Type.GetType("System.Int64"));

                Int64 npageno = 0;
                for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
                {
                    DataRow row = dtAssociation.NewRow();
                    row["nPFAssociationId"] = 0;
                    row["nPFLibId"] = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolnPFLibId));
                    row["sCategoryType"] = Convert.ToString(c1AssociationDetails.GetData(i, ncolsCategoryType));
                    row["nParentId"] = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolnGroupId));
                    if (Convert.ToString(c1AssociationDetails.GetData(i, ncolIsRequired)) == "Yes")
                        row["bIsRequired"] = true;
                    else
                        row["bIsRequired"] = false;
                    row["nOrderId"] = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolOrder));

                    // As Database Column is (bIsBlocked) and Form Control Label is (IsActive), so values are reverted
                    if (Convert.ToString(c1AssociationDetails.GetData(i, ncolIsActive)) == "No") 
                        row["bIsBlocked"] = true;
                    else
                        row["bIsBlocked"] = false;
                    // As Database Column is (bIsBlocked) and Form Control Label is (IsActive), so values are reverted

                    if (Convert.ToString(c1AssociationDetails.GetData(i, ncolsCategoryType)) == "G".ToString())
                    {
                        npageno = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolPageNo));
                        row["nPageNo"] = npageno;
                    }
                    else
                        row["nPageNo"] = npageno;

                    dtAssociation.Rows.Add(row);
                    row = null;
                }

                if ((!_IsModify && dtAssociation != null && dtAssociation.Rows.Count > 0) || (_IsModify))
                {
                    oclsHealthForm = new clsHealthForm();
                    id = oclsHealthForm.InsertAssociation(dtAssociation, _strConnectionString, "PF_HealthFormDetailInsertUpdate", _nLoginID, _nPFListId, _IsModify);
                    //ResetControls();
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
                if (dtAssociation != null)
                {
                    dtAssociation.Dispose();
                    dtAssociation = null;
                }
            }
            if (c1AssociationDetails.Rows.Count == 1)
                return 1;
            else
                return id;
        }

        //Resetting the Controls
        public void ResetControls()
        {
            TxtHealthFormName.Text = "";
            cbActive.Checked = false;
            _nPFListId = 0;
            _IsModify = false;
            c1AssociationDetails.Clear();
            c1AssociationDetails.Rows.Count = 1;
        }

        private Boolean Validationform()
        {
            //if (_IsModify)
            //{
            //    string grouplabel = "";
            //    string questionlabels = "";
            //    clsHealthForm oclsHistory = new clsHealthForm();

            //    for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
            //    {
            //        long nMstId = 0;
            //        string sType = "";
            //        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
            //        {
            //            sType = "G";
            //            nMstId = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolnPFLibId));
            //            string tempgrp = oclsHistory.IsValidPatientForm(_strConnectionString, "WS_IsValidPatientForm", nMstId, sType);
            //            if (tempgrp != "")
            //            {
            //                grouplabel += tempgrp + ",";
            //            }
            //        }
            //        else
            //        {
            //            string temp = "";
            //            sType = "Q";
            //            nMstId = Convert.ToInt64(c1AssociationDetails.GetData(i, ncolnPFLibId));
            //            string tempgrp = oclsHistory.IsValidPatientForm(_strConnectionString, "WS_IsValidPatientForm", nMstId, sType);
            //            if (tempgrp != "")
            //            {
            //                questionlabels += tempgrp + ",";
            //            }
            //        }
            //    }

            //    string strMsg = "";
            //    if (grouplabel != "")
            //    {
            //        strMsg = "History Category for below group(s) does not exists in gloEMR:" + Environment.NewLine;
            //        strMsg += Environment.NewLine + grouplabel.ToString().Trim(',');

            //    }
            //    if (questionlabels != "")
            //    {
            //        if (strMsg != "")
            //        {
            //            strMsg += Environment.NewLine + Environment.NewLine + Environment.NewLine;
            //        }
            //        strMsg += "History items for below question(s) does not exists in gloEMR: " + Environment.NewLine;
            //        strMsg += Environment.NewLine + questionlabels.ToString().Trim(',');
            //    }

            //    if (strMsg != "")
            //    {
            //        MessageBox.Show(strMsg, "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}
            return true;
        }


        //Validation for C1Association Grid
        private Boolean Validation()
        {
            if (TxtHealthFormName.Text.Trim() == "")
            {
                MessageBox.Show("Enter patient form name", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtHealthFormName.Focus();
                return false;
            }
            if (TxtHealthFormName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Patient form name should be maximum 100 characters", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtHealthFormName.Focus();
                return false;
            }
            if (cmbDownloadFormat.SelectedIndex == 0)
            {
                MessageBox.Show("Select Download Format", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbDownloadFormat.Focus();
                return false;
            }
            if (cmbDMScategory.Visible == true && cmbDMScategory.SelectedIndex == 0)
            {
                MessageBox.Show("Select DMS Category", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbDMScategory.Focus();
                return false;
            }
            if (_IsModify)
            {
                if (c1AssociationDetails.Rows.Count == 1)
                {
                    MessageBox.Show("Select Group & Answers", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cmbGroups.Focus();
                    tvGroups.Focus();
                    return false;
                }
               

                for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
                {

                    if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                    {
                        int cntquestions = 0;
                        for (int j = 1; j < c1AssociationDetails.Rows.Count; j++)
                        {
                            if (c1AssociationDetails.GetData(j, ncolsCategoryType).ToString() == "G")
                                continue;
                            if (c1AssociationDetails.Rows[j][ncolnGroupId].ToString() == c1AssociationDetails.Rows[i][ncolnPFLibId].ToString())
                                cntquestions += 1;
                        }
                        if (cntquestions == 0)
                        {
                            MessageBox.Show("Select Questions for Group - " + c1AssociationDetails.GetData(i, ncolGroup).ToString() + "", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                    }

                    if ((c1AssociationDetails.GetData(i, ncolIsRequired)) == null)
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                            MessageBox.Show("Select value for IsRequired combo for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Select value for IsRequired combo for the Question - " + c1AssociationDetails.GetData(i, ncolQuestion), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                    if ((c1AssociationDetails.GetData(i, ncolIsActive)) == null)
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                            MessageBox.Show("Select value for IsActive combo for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Select value for IsActive combo for the Question - " + c1AssociationDetails.GetData(i, ncolQuestion), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                    if ((c1AssociationDetails.GetData(i, ncolOrder) == null))
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                            MessageBox.Show("Enter Order for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Enter Order for the Question - " + c1AssociationDetails.GetData(i, ncolQuestion), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if ((c1AssociationDetails.GetData(i, ncolOrder).ToString() == ""))
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                            MessageBox.Show("Enter Order for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Enter Order for the Question - " + c1AssociationDetails.GetData(i, ncolQuestion), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if (Convert.ToInt16(c1AssociationDetails.GetData(i, ncolOrder)) <= 0)
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                            MessageBox.Show("Enter Order for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Enter Order for the Question - " + c1AssociationDetails.GetData(i, ncolQuestion), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if ((c1AssociationDetails.GetData(i, ncolPageNo) == null))
                    {
                        if (c1AssociationDetails.GetData(i, ncolsCategoryType).ToString() == "G")
                        {
                            MessageBox.Show("Select Page No for the Group - " + c1AssociationDetails.GetData(i, ncolGroup), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }

                }
                //if (Validationform() == false)
                //    return false;

            }

            return true;
        }

        //Get Group Order No for the newly added group
        private Int64 getGroupOrderNo()
        {
            Int64 grouporder = 0;
            if (c1AssociationDetails.Rows.Count == 1)
                grouporder = 0;
            else if (c1AssociationDetails.RowSel == 0)
            {
                for (int i = c1AssociationDetails.Rows.Count - 1; i > 0; i--)
                {
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G".ToString())
                    {
                        grouporder = Convert.ToInt64((c1AssociationDetails.Rows[i][ncolOrder].ToString()));
                        break;
                    }
                }
            }
            else
            {

                Int64 _PageNo = 1;
                Boolean IsGroupFound = false;
                if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
                {
                    grouporder = Convert.ToInt64((c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolOrder].ToString()));
                    _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo1]);
                    for (int i = c1AssociationDetails.RowSel; i < c1AssociationDetails.Rows.Count; i++)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G".ToString() && _PageNo == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]))
                        {
                            IsGroupFound = true;
                            grouporder = Convert.ToInt64((c1AssociationDetails.Rows[i][ncolOrder].ToString()));
                        }
                    }

                }
                else
                {
                    for (int i = c1AssociationDetails.RowSel; i > 0; i--)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                        {
                            IsGroupFound = true;
                            _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]);
                            break;
                        }
                    }

                    if (IsGroupFound)
                    {
                        for (int i = c1AssociationDetails.Rows.Count - 1; i > 0; i--)
                        {
                            if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G".ToString() && _PageNo == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]))
                            {
                                grouporder = Convert.ToInt64((c1AssociationDetails.Rows[i][ncolOrder].ToString()));
                                break;
                            }
                        }
                    }

                }
            
            }
            return grouporder + 1;
        }


        //Get Question Order No for the newly added question
        private Int64 getQuestionOrderNo()
        {
            Int64 questionorder = 0;
            if (c1AssociationDetails.Rows.Count == 1)
                questionorder = 0;
            else
            {
                int groupind = 0;
                for (int i = c1AssociationDetails.RowSel; i > 0; i--)
                {
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                    {
                        groupind = i;
                        break;
                    }
                }
                for (int i = groupind + 1; i < c1AssociationDetails.Rows.Count; i++)
                {
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                    {
                        break;
                    }
                    questionorder = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolOrder]);
                }

            }


            return questionorder + 1;
        }


        #endregion

        #region "Events"

        //Save Association Details to the Database
        private void ts_Save_Click(object sender, EventArgs e)
        {

          Save();
        }

        public Boolean Save()
        {
            if (!SaveHealthForm())
                return false;
            else
                IsNewModify = false;

            return true;
        }

        private Boolean SaveHealthForm()
        { 
            clsHealthForm oclsHistory = null;
            try
            {
                if (!Validation())
                    return false;
                Int16 dmsCategoryID = 0;

                if (cmbDownloadFormat.SelectedIndex > 1)
                {
                    dmsCategoryID = Convert.ToInt16(cmbDMScategory.SelectedValue);
                }


                oclsHistory = new clsHealthForm();
                if (_IsModify != true)
                {
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    //Changes done for newly added column
                    _nPFListId = oclsHistory.AddPFHealthForm(TxtHealthFormName.Text.Trim(), _nLoginID, _strConnectionString, _IsModify, _nPFListId, cbActive.Checked, cmbDownloadFormat.SelectedItem.ToString(), dmsCategoryID, cbEnableTaskNotification.Checked);

                    if (_nPFListId > 0)
                    {
                        //MessageBox.Show("Patient form added successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillAssociation();
                        _IsModify = true;
                        ts_New.Visible = true;

                        panel1.Show();
                        panel2.Show();
                        panel5.Hide();
                    }
                    else if (_nPFListId == -1)
                    {

                        MessageBox.Show("Patient form name already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TxtHealthFormName.Focus();
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to add patient form", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    //Changes done for newly added column
                    _nPFListId = oclsHistory.AddPFHealthForm(TxtHealthFormName.Text.Trim(), _nLoginID, _strConnectionString, _IsModify, _nPFListId, cbActive.Checked, cmbDownloadFormat.SelectedItem.ToString(), dmsCategoryID, cbEnableTaskNotification.Checked);
                    if (_nPFListId > 0)
                    {
                        if (SaveAssociation() > 0)
                        {
                            //MessageBox.Show("Patient form updated successfully", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillAssociation();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update patient form", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        ts_Save.Text = "Save";
                    }
                    else if (_nPFListId == -1)
                    {
                        MessageBox.Show("Patient form name already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TxtHealthFormName.Focus();
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient form", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
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
            return true;
        }
        Int64 nCategoryId = 0;
        private void tvGroups_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            fillQuestion(e.Node);

        }

        private void fillQuestion(TreeNode e)
        {
            Int64 tempid = Convert.ToInt64(e.Tag);

            nCategoryId = Convert.ToInt64(((DataRow[])dtGroups.Select("[nPFLibId] = " + tempid + ""))[0]["nCategoryId"]);
            FillComboQuestions();


            c1AssociationDetails.RowSel = 0;
            c1AssociationDetails.Select(0, 0);
            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                c1AssociationDetails.Rows[i].Selected = false;
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && c1AssociationDetails.Rows[i][ncolGroup].ToString() == e.Text.ToString())
                {
                    c1AssociationDetails.RowSel = i;
                    c1AssociationDetails.Rows[i].Selected = true;
                    c1AssociationDetails.Select(i, 0);
                }
            }
        }

        private void tvGroups_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (tvGroupsTag.ToString() == "-1")
            //{
            //    MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    tvGroups.Focus();
            //    return;
            //}
            if (tvGroups.SelectedNode == null)
            {
                return;
            }
            if (c1AssociationDetails.FindRow(tvGroups.SelectedNode.Tag.ToString(), 0, ncolnPFLibId, false, true, false) >= 0)
            {
                MessageBox.Show("Group already added", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tvGroups.Focus();
                return;
            }
            Int64 _PageNo = 1;
            int indtoinsert = -1;
            if (c1AssociationDetails.RowSel == 0)
            {
                indtoinsert = c1AssociationDetails.Rows.Count;
                if (c1AssociationDetails.Rows.Count > 1)
                {
                    for (int i = c1AssociationDetails.Rows.Count - 1; i > 0; i--)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                        {
                            _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                            break;
                        }
                    }
                }
            }
            else
            {
                if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
                {
                    _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]);
                }
                else
                {
                    for (int i = c1AssociationDetails.RowSel; i > 0; i--)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                        {
                            _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                            break;
                        }
                    }
                }

                if (_PageNo == 0)
                {
                    indtoinsert = c1AssociationDetails.Rows.Count;
                }
                else
                {
                    bool found = false;
                    for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && _PageNo != Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                        {
                            found = true;
                            indtoinsert = i;
                            break;
                        }
                    }
                    if (!found)
                        indtoinsert = c1AssociationDetails.Rows.Count;
                    if (indtoinsert == -1)
                        indtoinsert = c1AssociationDetails.Rows.Count;
                }

            }




            AddGroupToC1AssociationGrid(indtoinsert, tvGroups.SelectedNode.Text.ToString(), tvGroups.SelectedNode.Tag.ToString(), getGroupOrderNo(), false, true, _PageNo);
            tvGroups_NodeMouseClick(sender, e);
        }

     
        //Adding the selected Group of the ComboBox to C1Association grid
        //private void btnAddGroup_Click(object sender, EventArgs e)
        //{
        //    if (cmbGroups.SelectedValue.ToString() == "-1")
        //    {
        //        MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        cmbGroups.Focus();
        //        return;
        //    }
        //    if (c1AssociationDetails.FindRow(cmbGroups.SelectedValue.ToString(), 0, ncolnPFLibId, false, true, false) >= 0)
        //    {
        //        MessageBox.Show("Group already added", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        cmbGroups.Focus();
        //        return;
        //    }
        //    Int64 _PageNo = 1;
        //    int indtoinsert = -1;
        //    if (c1AssociationDetails.RowSel == 0)
        //    {
        //        indtoinsert = c1AssociationDetails.Rows.Count;
        //        if (c1AssociationDetails.Rows.Count > 1)
        //        {
        //            for (int i = c1AssociationDetails.Rows.Count - 1; i > 0; i--)
        //            {
        //                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
        //                {
        //                    _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
        //        {
        //            _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]);
        //        }
        //        else
        //        {
        //            for (int i = c1AssociationDetails.RowSel; i > 0; i--)
        //            {
        //                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
        //                {
        //                    _PageNo = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
        //                    break;
        //                }
        //            }
        //        }

        //        if (_PageNo == 0)
        //        {
        //            indtoinsert = c1AssociationDetails.Rows.Count;
        //        }
        //        else
        //        {
        //            bool found = false;
        //            for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
        //            {
        //                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && _PageNo != Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
        //                {
        //                    found = true;
        //                    indtoinsert = i;
        //                    break;
        //                }
        //            }
        //            if (!found)
        //                indtoinsert = c1AssociationDetails.Rows.Count;
        //            if (indtoinsert == -1)
        //                indtoinsert = c1AssociationDetails.Rows.Count;
        //        }

        //    }




        //    AddGroupToC1AssociationGrid(indtoinsert, cmbGroups.Text, cmbGroups.SelectedValue.ToString(), getGroupOrderNo(), false, true, _PageNo);
        //}

    
        private void tvQuestions_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tvQuestions.SelectedNode == null)
            {
                return;
            }
            if (c1AssociationDetails.Rows.Count == 1)
            {
                MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                c1AssociationDetails.Focus();
                return;
            }
            if (c1AssociationDetails.RowSel == 0)
            {
                MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                c1AssociationDetails.Focus();
                return;
            }

          

            //if (tvQuestions.Tag.ToString() == "-1")
            //{
            //    MessageBox.Show("Select Question", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    tvQuestions.Focus();
            //    return;
            //}
            Int64 _GroupId = 0;
            if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
                _GroupId = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnPFLibId]);
            else
                _GroupId = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnGroupId]);

        
            if (tvGroups.Nodes.Count > 0)
            {
                if (tvGroups.SelectedNode != null)
                {
                    if (tvGroups.SelectedNode.Tag != null)
                    {
                        if (_GroupId.ToString() != tvGroups.SelectedNode.Tag.ToString())
                        {
                            MessageBox.Show("Question cannot be added to a different group.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }
         

            for (int i = 0; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if ((c1AssociationDetails.Rows[i][ncolnGroupId].ToString() == _GroupId.ToString()) && (c1AssociationDetails.Rows[i][ncolnPFLibId].ToString() == tvQuestions.SelectedNode.Tag.ToString()))
                {
                    MessageBox.Show("Question already added", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tvQuestions.Focus();
                    return;
                }
            }

            int indtoinsert = c1AssociationDetails.RowSel;
            for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                indtoinsert = i;
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    --indtoinsert;
                    break;
                }
            }
            AddQuestionToC1AssociationGrid(indtoinsert + 1, tvQuestions.SelectedNode.Text, tvQuestions.SelectedNode.Tag.ToString(), _GroupId.ToString(), getQuestionOrderNo(), false, true, 1);
      
        }

        //Adding the selected Question of ComboBox to C1Association grid
        //private void btnAddQuestion_Click(object sender, EventArgs e)
        //{
        //    if (c1AssociationDetails.Rows.Count == 1)
        //    {
        //        MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        c1AssociationDetails.Focus();
        //        return;
        //    }
        //    if (c1AssociationDetails.RowSel == 0)
        //    {
        //        MessageBox.Show("Select Group", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        c1AssociationDetails.Focus();
        //        return;
        //    }
        //    if (cmbQuestions.SelectedValue.ToString() == "-1")
        //    {
        //        MessageBox.Show("Select Question", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        cmbQuestions.Focus();
        //        return;
        //    }
        //    Int64 _GroupId = 0;
        //    if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
        //        _GroupId = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnPFLibId]);
        //    else
        //        _GroupId = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnGroupId]);

        //    for (int i = 0; i <= c1AssociationDetails.Rows.Count - 1; i++)
        //    {
        //        if ((c1AssociationDetails.Rows[i][ncolnGroupId].ToString() == _GroupId.ToString()) && (c1AssociationDetails.Rows[i][ncolnPFLibId].ToString() == cmbQuestions.SelectedValue.ToString()))
        //        {
        //            MessageBox.Show("Question already added", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            cmbQuestions.Focus();
        //            return;
        //        }
        //    }

        //    int indtoinsert = c1AssociationDetails.RowSel;
        //    for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
        //    {
        //        indtoinsert = i;
        //        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
        //        {
        //            --indtoinsert;
        //            break;
        //        }
        //    }
        //    AddQuestionToC1AssociationGrid(indtoinsert + 1, cmbQuestions.Text, cmbQuestions.SelectedValue.ToString(), _GroupId.ToString(), getQuestionOrderNo(), false, true, 1);
        //}

        //Removing Groups and Questions from the C1Association grid
        private void c1AssociationDetails_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                c1AssociationDetails.Rows[i].Selected = false;
            }
            if (c1AssociationDetails.ColSel == ncolToRemoveFromGrid && c1AssociationDetails.RowSel > 0)
            {
                Int64 currpageno = 0;
                if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "G")
                {

                    if (MessageBox.Show("Do you want to delete Group - " + c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolGroup].ToString() + "?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                      currpageno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]);
                        string temppflibid = c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnPFLibId].ToString();
                        c1AssociationDetails.Rows.Remove(c1AssociationDetails.RowSel);
                        for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                        {
                            if (string.Compare(temppflibid.ToString(), c1AssociationDetails.Rows[i][ncolnGroupId].ToString()) == 0)
                            {
                                c1AssociationDetails.Rows.Remove(i);
                                i--;
                            }
                        }
                        if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel <= c1AssociationDetails.Rows.Count - 1)
                        {

                            Int64 totgrps = 0;
                            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                            {
                                if (currpageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                                {
                                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                                    {
                                        totgrps += 1;
                                    }
                                }
                            }
                            if (totgrps == 0)
                            {
                                for (int i = c1AssociationDetails.RowSel; i <= c1AssociationDetails.Rows.Count - 1; i++)
                                {
                                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                                    {
                                        Int64 newpageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]) - 1;
                                        if (newpageno <= 0)
                                            continue;
                                        setpagenocombo(newpageno, i);
                                        validatecellchange = false;
                                        c1AssociationDetails.SetData(i, ncolPageNo, newpageno);
                                        c1AssociationDetails.SetData(i, ncolPageNo1, newpageno);
                                        validatecellchange = true;
                                    }

                                }
                            }
                        }


                        //if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel <= c1AssociationDetails.Rows.Count - 1)
                        //{
                        //    Int64 pageno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]);

                        //    for (int i = c1AssociationDetails.RowSel; i <= c1AssociationDetails.Rows.Count - 1; i++)
                        //    {
                        //        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "Q")
                        //            break;
                        //        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && pageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                        //        {
                        //            Int64 orderno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolOrder]) - 1;
                        //            if (orderno <= 0)
                        //                continue;
                        //            c1AssociationDetails.SetData(i, ncolOrder, orderno);
                        //            //c1AssociationDetails.Rows.MoveRange(c1AssociationDetails.RowSel, 1, c1AssociationDetails.RowSel - 1);
                        //        }

                        //    }  
                        //}
                      if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel <= c1AssociationDetails.Rows.Count - 1)
                        SetOrderNo(currpageno);
                    }

                }
                else
                {
                    if (MessageBox.Show("Do you want to delete Question - " + c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolQuestion].ToString() + "?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        currpageno = 0;
                        for (int i = c1AssociationDetails.RowSel; i > 0 ; i--)
                        {
                            if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                            {
                                currpageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                                break;
                            }
                        }
                        c1AssociationDetails.Rows.Remove(c1AssociationDetails.RowSel);

                        if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel <= c1AssociationDetails.Rows.Count - 1)
                        SetOrderNo(currpageno);
                    }
                }
            }
            
            {
                //for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                //{
                //    c1AssociationDetails.Rows[i].Selected = false;

                //}
                for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                {
                    c1AssociationDetails.Rows[i].Selected = false;
                    if (i == c1AssociationDetails.RowSel)
                    {
                        c1AssociationDetails.RowSel = i;
                        c1AssociationDetails.Rows[i].Selected = true;
                        c1AssociationDetails.Select(i, 1);
                    }
                }
                for (int i = 0; i <= tvGroups.Nodes.Count - 1; i++)
                {
                    string strType = Convert.ToString(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType]).Trim().ToUpper();
                    string groupID = "";
                    if (strType == "G")
                    {
                       groupID  = Convert.ToString(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnPFLibId]);
                    }
                    else if (strType == "Q")
                    {
                        groupID = Convert.ToString(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolnGroupId]);
                    }

                    //if (tvGroups.Nodes[i].Text.Trim() == c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolGroup].ToString().Trim())
                    if (Convert.ToString(tvGroups.Nodes[i].Tag) == groupID)
                    {
                        tvGroups.SelectedNode = tvGroups.Nodes[i];
                        tvGroups.Select();
                        Int64 tempid = Convert.ToInt64(tvGroups.SelectedNode.Tag);

                        nCategoryId = Convert.ToInt64(((DataRow[])dtGroups.Select("[nPFLibId] = " + tempid + ""))[0]["nCategoryId"]);
                        FillComboQuestions();
                        c1AssociationDetails.Focus(); 
                        break;
                    }

                }
            }
        }
        private void SetPageNo()
        {
            Int64 pageno = 0;
            Int64 temppageno = 0;
            Int64 temppageno1 = 0;
            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    temppageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                    if ((pageno) == temppageno)
                    {
                        continue;
                    }
                    else {
                        if (temppageno != temppageno1)
                        { 
                          pageno += 1;
                        }
                      
                        if ((pageno) != temppageno)
                        {
                            c1AssociationDetails.SetData(i, ncolPageNo, pageno);
                        
                        }
                    }
                    temppageno1 = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                 
                }
            
            }
            pageno = 0;
            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    pageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                    SetOrderNo(pageno);
                }

            }
        }
        private void SetOrderNo(Int64 pageno)
        {
            //if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel <= c1AssociationDetails.Rows.Count - 1)
            {
                Int64 grpOrderno = 1;
                Int64 quesOrderno = 0;
                Int64 temppageno = 0;
                for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                {
                 
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                    {
                        temppageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                        if (pageno != temppageno)
                        {
                            continue;
                        }
                        c1AssociationDetails.SetData(i, ncolOrder, grpOrderno);
                        grpOrderno += 1;
                        quesOrderno = 1;
                    }
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "Q")
                    {
                        if (pageno != temppageno)
                        {
                            continue;
                        }
                        c1AssociationDetails.SetData(i, ncolOrder, quesOrderno);
                        quesOrderno += 1;
                    }

                }
            }
           

        }
        //Hiding the C1Association User Control
        private void ts_Hide_Click(object sender, EventArgs e)
        {
            this.Hide();
            IsNewModify = false;
            panel1.Show();
            panel2.Show();
            panel5.Hide();
        }

        //Clear Search TextBox
        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtHealthFormName.Text = "";
        }

        //Moving Up Group/Question
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            Int64 orderno = 0;
            if (c1AssociationDetails.RowSel > 1)
            {
                if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "Q")
                {
                    if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel - 1][ncolsCategoryType].ToString() == "Q")
                    {
                        orderno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolOrder]);
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel - 1][ncolOrder]));
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel - 1, ncolOrder, orderno);
                        c1AssociationDetails.Rows.MoveRange(c1AssociationDetails.RowSel, 1, c1AssociationDetails.RowSel - 1);
                    }
                    else
                    {
                        MessageBox.Show("Cannot move the question upwards. The question you have selected is first in its group.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    int groupind = -1;
                    for (int i = c1AssociationDetails.RowSel - 1; i > 0; i--)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]) == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                        {
                            groupind = i;
                            break;
                        }
                    }

                    if (groupind != -1)
                    {
                        int quescnt = 1;
                        for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                        {
                            if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                                break;
                            quescnt += 1;
                        }
                        orderno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolOrder]);
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[groupind][ncolOrder]));
                        c1AssociationDetails.SetData(groupind, ncolOrder, orderno);
                        c1AssociationDetails.Rows.MoveRange(c1AssociationDetails.RowSel, quescnt, groupind);
                    }
                    else
                    {
                        MessageBox.Show("Cannot move the group upwards. The group you have selected is first on its page.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Moving down Group/Question
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            Int64 orderno = 0;
            if (c1AssociationDetails.RowSel > 0 && c1AssociationDetails.RowSel < c1AssociationDetails.Rows.Count - 1)
            {
                if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolsCategoryType].ToString() == "Q")
                {
                    if (c1AssociationDetails.Rows[c1AssociationDetails.RowSel + 1][ncolsCategoryType].ToString() == "Q")
                    {
                        orderno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolOrder]);
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel + 1][ncolOrder]));
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel + 1, ncolOrder, orderno);
                        c1AssociationDetails.Rows.MoveRange(c1AssociationDetails.RowSel, 1, c1AssociationDetails.RowSel + 1);
                    }
                    else
                    {
                        MessageBox.Show("Cannot move the question downwards. The question you have selected is last in its group.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    int groupind = -1;
                    for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                    {
                        if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolPageNo]) == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                        {
                            groupind = i;
                            break;
                        }
                    }
                   
                    if (groupind != -1)
                    {
                        int quescnt = 1;
                        for (int i = groupind + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
                        {
                            if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                                break;
                            quescnt += 1;
                        }
                        orderno = Convert.ToInt64(c1AssociationDetails.Rows[c1AssociationDetails.RowSel][ncolOrder]);
                        c1AssociationDetails.SetData(c1AssociationDetails.RowSel, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[groupind][ncolOrder]));
                        c1AssociationDetails.SetData(groupind, ncolOrder, orderno);
                        c1AssociationDetails.Rows.MoveRange(groupind, quescnt, c1AssociationDetails.RowSel);
                    }
                    else
                    {
                        MessageBox.Show("Cannot move the group downwards. The group you have selected is last on its page.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Functionality for change of Page No
        private void c1AssociationDetails_CellChanged(object sender, RowColEventArgs e)
        {

           
            
            
            if (!validatecellchange) return;

            for (int i = 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                c1AssociationDetails.Rows[i].Selected = false;
            }   
            if (e.Col == ncolIsRequired)
            {
                if (e.Row == 0 || c1AssociationDetails.RowSel == 0)
                    return;
                if (c1AssociationDetails.Rows[e.Row][ncolsCategoryType].ToString() != "G")
                    return;
                Int64 nGroupId = Convert.ToInt64(c1AssociationDetails.Rows[e.Row][ncolnPFLibId]);
                if (c1AssociationDetails.Rows[e.Row][ncolIsRequired].ToString().ToLower() == "yes")
                {
                    if (MessageBox.Show("The questions under this group will be changed to Mandatory. Do you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(e.Row, ncolIsRequired, "No");
                        validatecellchange = true;
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("The questions under this group will be changed to non Mandatory. Do you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(e.Row, ncolIsRequired, "Yes");
                        validatecellchange = true;
                        return;
                    }
                }
                for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
                {
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "Q" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolnGroupId]) == nGroupId)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(i, ncolIsRequired, c1AssociationDetails.Rows[e.Row][ncolIsRequired]);
                        validatecellchange = true;
                    }
                }


                return;
            }
            if (e.Row == 0 || e.Col != ncolPageNo || c1AssociationDetails.RowSel == 0)
                return;
            if (c1AssociationDetails.Rows[e.Row][ncolsCategoryType].ToString() != "G")
                return;



            
            Int64 newpageno = Convert.ToInt64(c1AssociationDetails.Rows[e.Row][e.Col]);
            Int64 oldpageno = Convert.ToInt64(c1AssociationDetails.Rows[e.Row][ncolPageNo1]);

            Int64 cntquestions = 0;
            if (oldpageno != newpageno)
            {
                for (int i = 0; i < c1AssociationDetails.Rows.Count; i++)
                {
                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) == oldpageno)
                    {
                        cntquestions += 1;
                    }
                }
                if (cntquestions == 1)
                {
                    if (MessageBox.Show("Page No - " + oldpageno.ToString() + " contains a single question. By proceeding further, Page Nos will be rearranged. Do you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(e.Row, ncolPageNo, oldpageno);
                        c1AssociationDetails.SetData(e.Row, ncolPageNo1, oldpageno);
                        validatecellchange = true;
                        return;
                    }
                }

            }
            else
                return;
            int bdirection = 0;
            if (oldpageno > newpageno)
                bdirection = 1;
            else
                bdirection = 2;
            int groupind = -1;
            for (int i = c1AssociationDetails.Rows.Count - 1; i > 0; i--)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) == (newpageno))
                    break;
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) == (newpageno + 1))
                    groupind = i;

            }

            if (groupind == -1)
            {
                groupind = c1AssociationDetails.Rows.Count;
            }

            int quescnt = 1;
            for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                    break;
                quescnt += 1;
            }

            int indtoinsert = -1;
            if (bdirection == 1)
                indtoinsert = groupind;
            else
                indtoinsert = groupind - quescnt;

            for (int i = c1AssociationDetails.RowSel + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && oldpageno != Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                    break;
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && oldpageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                {
                    c1AssociationDetails.SetData(i, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolOrder]) - 1);
                }
            }
            c1AssociationDetails.SetData(e.Row, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[e.Row][ncolOrder]) + 1);
            validatecellchange = false;
            c1AssociationDetails.SetData(e.Row, ncolPageNo, newpageno);
            c1AssociationDetails.SetData(e.Row, ncolPageNo1, newpageno);
            validatecellchange = true;
            c1AssociationDetails.Rows.MoveRange(e.Row, quescnt, indtoinsert);

            Boolean found = false;
            for (int i = indtoinsert - 1; i > 0; i--)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && newpageno != Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                    break;
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && newpageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                {
                    found = true;
                    c1AssociationDetails.SetData(indtoinsert, ncolOrder, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolOrder]) + 1);
                    break;
                }
            }
            if (!found)
                c1AssociationDetails.SetData(indtoinsert, ncolOrder, 1);


            Int64 newpageno1 = newpageno;
            if (cntquestions == 1 && bdirection == 2)
            {
                newpageno1 = newpageno - 1;
                for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
                {

                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) >= oldpageno)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(i, ncolPageNo, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]) - 1);
                        c1AssociationDetails.SetData(i, ncolPageNo1, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) - 1);
                        validatecellchange = true;

                    }
                }
            }
            else if (cntquestions == 1 && bdirection == 1)
            {
                for (int i = 1; i < c1AssociationDetails.Rows.Count; i++)
                {

                    if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G" && Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) > oldpageno)
                    {
                        validatecellchange = false;
                        c1AssociationDetails.SetData(i, ncolPageNo, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]) - 1);
                        c1AssociationDetails.SetData(i, ncolPageNo1, Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo1]) - 1);
                        validatecellchange = true;

                    }
                }
            }

            string strpageno = "";
            Int64 temppageno = 1;
            for (int i = 1; i <= newpageno1; i++)
            {
                temppageno = i;
                strpageno += temppageno.ToString() + "|";
            }
            for (int i = indtoinsert + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    if (temppageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                    {
                        continue;
                    }
                    else
                    {
                        temppageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                        strpageno += temppageno.ToString() + "|";
                    }
                }
            }
            //if (temppageno < 10)
            {
                temppageno += 1;
                strpageno += temppageno.ToString();
            }
            AddComboToC1GridCell("nPageNo", indtoinsert, ncolPageNo, strpageno);

            validatecellchange = false;
            c1AssociationDetails.SetData(indtoinsert, ncolPageNo, newpageno1);
            c1AssociationDetails.SetData(indtoinsert, ncolPageNo1, newpageno1);
            validatecellchange = true;
        }

        private void setpagenocombo(Int64 pageno,int rowind)
        {
            string strpageno = "";
            Int64 temppageno = 1;
            for (int i = 1; i <= pageno; i++)
            {
                temppageno = i;
                strpageno += temppageno.ToString() + "|";
            }
            for (int i = rowind + 1; i <= c1AssociationDetails.Rows.Count - 1; i++)
            {
                if (c1AssociationDetails.Rows[i][ncolsCategoryType].ToString() == "G")
                {
                    if (temppageno == Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]))
                    {
                        continue;
                    }
                    else
                    {
                        temppageno = Convert.ToInt64(c1AssociationDetails.Rows[i][ncolPageNo]);
                        strpageno += temppageno.ToString() + "|";
                    }
                }
            }
            //if (temppageno < 10)
            {
                temppageno += 1;
                strpageno += temppageno.ToString();
            }
            AddComboToC1GridCell("nPageNo", rowind, ncolPageNo, strpageno);
        }

        #endregion

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtSearchGroup.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearchQuestion.Text = "";
        }
       



        private void ts_New_Click(object sender, EventArgs e)
        {
            SaveandPreview();

        }


        public Boolean SaveandPreview()
        {

            if (!SaveHealthForm())
                return false;
            ts_Save.Visible = false;
            ts_New.Visible = false;
            ts_Publish.Visible = true;
            ts_btnClose.Visible = true;
            pnlwebbrowser.Visible = true;
            pnlwebbrowser.BringToFront();

            panel5.Hide();
            LoadHealthFormPreview();

            return true;
           

        }


        private void ts_Publish_Click(object sender, EventArgs e)
        {
            Publish();
            
        }
        public Boolean Publish()
        {

            if (MessageBox.Show("The \""+ TxtHealthFormName.Text.Trim() + "\" form will be published to the portal and allows patient to submit information" + System.Environment.NewLine + "Do you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //  ogloSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)
                string isPortalEnable = string.Empty;
                isPortalEnable = getHealthFormSetting(_strConnectionString, "PatientPortalEnabled", _nLoginID, _nClinicID);
                SmallForm();
                if (isPortalEnable.ToLower() == "false")
                {
                    {
                        MessageBox.Show("Patient Portal should be enable to publish the patient form.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                //if (Validationform() == false)
                //    return;

                ts_Save.Visible = true;
                ts_New.Visible = true;
                ts_Publish.Visible = false;
                ts_btnClose.Visible = false;
                pnlwebbrowser.Visible = false;

                clsHealthForm oclsHistory = null;

                oclsHistory = new clsHealthForm();
                Int64 npublishid = oclsHistory.PublishForm(_nPFListId, htmlcontent, _strConnectionString, "PF_PublishForm");
                if (npublishid > 0)
                {
                    cbActive.Checked = true;
                    lblHeading1.Text = "     Status: Active";
                    MessageBox.Show("You have successfully published "+TxtHealthFormName.Text.Trim() +" form to the portal.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
          
            return false;
            
        }
      

        public string getHealthFormSetting(string _strConnectionString,string SettingName, Int64 UserID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_strConnectionString);
            string Value = string.Empty;
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "' AND nClinicID = " + ClinicID + "";
                Value = Convert.ToString(oDB.ExecuteScalar_Query(_sqlQuery));
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = string.Empty;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = string.Empty;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return Value;
        }

        string tab = string.Empty;
        string tabcontrols = string.Empty;
        string htmlcontent = string.Empty;
        string tabcommentstart = "<!--<Tab>-->";
        string tabcommentstop = "<!--</Tab>-->";
        string tabcontrolscommentstart = "<!--<TabControls>-->";
        string tabcontrolscommentstop = "<!--</TabControls>-->";
        private void ts_btnClose_Click(object sender, EventArgs e)
        {
          Close();
        }
        public void Close()
        {
            ts_Save.Visible = true;
            ts_New.Visible = true;
            ts_Publish.Visible = false;
            ts_btnClose.Visible = false;
            pnlwebbrowser.Visible = false;
            SmallForm();
        }
        private void LoadHealthFormPreview()
        {
            pictureBox2.Visible = true;
            pictureBox2.Left = pnlwebbrowser.Width / 2;
            pictureBox2.BringToFront();
            clsHealthformContent oclsHealthformContent = new clsHealthformContent(_strConnectionString);
            tab = oclsHealthformContent.GetTab(_nPFListId);
            tabcontrols = oclsHealthformContent.GetTabControls(_nPFListId);
            htmlcontent = "";
            htmlcontent = tabcommentstart ;
            htmlcontent += tab;
            htmlcontent += tabcommentstop;
            htmlcontent += tabcontrolscommentstart;
            htmlcontent += tabcontrols;
            htmlcontent += tabcontrolscommentstop;



            string filepath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.ToString().LastIndexOf("\\")) + @"\HealthFormPreview\healthformpreview.htm";
            //string filestr = System.IO.File.ReadAllText(filepath);
            //Int64 indtabstart = -1;
            //Int64 indtabstop = -1;
            //Int64 indtabcontrolsstart = -1;
            //Int64 indtabcontrolsstop = -1;
            //indtabstart = filestr.IndexOf(tabcommentstart);
            //indtabstop = filestr.IndexOf(tabcommentstop) + tabcommentstop.Length;

            //System.IO.File.WriteAllText(filepath, htmlcontent);
            DynamicHealthFormUpdate(filepath, tab, tabcontrols, filepath, filepath);
            wbpatientportal.Url = new Uri(filepath);

        }
        public string DynamicHealthFormUpdate(string FilenametoUpdate, string strReplaceContent1, string strReplaceContent2, string PracticeIDfolder, string filepath)
        {
            try
            {
                string FileUpdatedpath = "";
                // path to Actual HealthForm 
                // Place Holder in HealthForm to be replaced
                // Content to be Replace in Actual File in place of Place Holder
                //string strpath = System.Configuration.ConfigurationManager.AppSettings["CCDAPath"];
                string HealthFormPath = "";
                string strHealthFormFileName = FilenametoUpdate;

                string strHealthFormTemplatefileFullPath = "";
                string strActualHealthformupdateFilePath = "";
                string strFileData = "";

                string PlaceHolder1Start = "<!--<Tab>-->";
                string PlaceHolder1End = "<!--</Tab>-->";

                string PlaceHolder2Start = "<!--<TabControls>-->";
                string PlaceHolder2End = "<!--</TabControls>-->";

                string ReplaceContent1 = strReplaceContent1;
                string ReplaceContent2 = strReplaceContent2;

                //if (strpath != string.Empty)
                //{
                //    HealthFormPath = GetParentDirectory(strpath, 2);
                //}
                if (filepath != "")
                {
                    //strHealthFormTemplatefileFullPath = Path.Combine(HealthFormPath, "Healthforms", "HealthFormTemplate.html");
                    strHealthFormTemplatefileFullPath = filepath;
                    if (File.Exists(strHealthFormTemplatefileFullPath))
                    {
                        strFileData = File.ReadAllText(strHealthFormTemplatefileFullPath);
                        if (strFileData != "")
                        {

                            string strContent1ToReplace = "";
                            string strContent2ToReplace = "";

                            if (strFileData.Contains(PlaceHolder1Start) && strFileData.Contains(PlaceHolder1End) && strFileData.Contains(PlaceHolder2Start) && strFileData.Contains(PlaceHolder2End))
                            {
                                int StartIndex = strFileData.IndexOf(PlaceHolder1Start) + PlaceHolder1Start.Length;
                                int EndIndex = strFileData.IndexOf(PlaceHolder1End);

                                strFileData = strFileData.Remove(StartIndex, EndIndex - StartIndex);
                                strFileData = strFileData.Insert(StartIndex, ReplaceContent1);


                                //strContent1ToReplace = strFileData.Substring(StartIndex, EndIndex - StartIndex);

                                //if (strContent1ToReplace != string.Empty && ReplaceContent1 != string.Empty)
                                //{
                                //    strFileData = strFileData.Replace(strContent1ToReplace, ReplaceContent1);
                                //}

                                int StartIndex2 = strFileData.IndexOf(PlaceHolder2Start) + PlaceHolder2Start.Length;
                                int EndIndex2 = strFileData.IndexOf(PlaceHolder2End);


                                strFileData = strFileData.Remove(StartIndex2, EndIndex2 - StartIndex2);
                                strFileData = strFileData.Insert(StartIndex2, ReplaceContent2);

                                //strContent2ToReplace = strFileData.Remove(StartIndex2, EndIndex2 - StartIndex2);
                                //strContent2ToReplace = strFileData.Insert(StartIndex2, ReplaceContent2);

                                //strContent2ToReplace = strFileData.Substring(StartIndex2, EndIndex2 - StartIndex2);

                                //if (strContent2ToReplace != string.Empty && ReplaceContent2 != string.Empty)
                                //{
                                //    strFileData = strFileData.Replace(strContent2ToReplace, ReplaceContent2);
                                //}

                                //if (strContent1ToReplace != string.Empty && strContent2ToReplace != string.Empty)
                                if (strFileData != string.Empty)
                                {
                                    //strActualHealthformupdateFilePath = Path.Combine(HealthFormPath, "Healthforms", PracticeIDfolder, strHealthFormFileName);
                                    //string PracticeFolder = Path.Combine(HealthFormPath, "Healthforms", PracticeIDfolder);

                                    //// Check if Practice Folder Exists or not
                                    //if (!Directory.Exists(PracticeFolder))
                                    //{
                                    //    Directory.CreateDirectory(PracticeFolder);
                                    //}
                                    strActualHealthformupdateFilePath = filepath;
                                    if (File.Exists(strActualHealthformupdateFilePath))
                                    {
                                        // It will take care of readonly file.
                                        bool isFileReadOnly = (File.GetAttributes(strActualHealthformupdateFilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                                        if (isFileReadOnly)
                                        {
                                            File.SetAttributes(strActualHealthformupdateFilePath, File.GetAttributes(strActualHealthformupdateFilePath) & ~FileAttributes.ReadOnly);
                                        }
                                    }

                                    File.WriteAllText(strActualHealthformupdateFilePath, strFileData);
                                    FileUpdatedpath = strActualHealthformupdateFilePath;
                                }
                            }
                        }
                    }
                }
                return strActualHealthformupdateFilePath;
            }
            catch (Exception)
            {
                return "";
            }

        }

        private void c1AssociationDetails_Click(object sender, EventArgs e)
        {

        }

       

        private void wbpatientportal_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            pictureBox2.Visible = false;
            

        }

        int cmbDownloadFormat_SelectedIndex = 0;
        private void cmbDownloadFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsModify && callValidation)
           {

               if (c1AssociationDetails.Rows.Count > 1)
               {
                   if (((cmbDownloadFormat_SelectedIndex == 3) && (cmbDownloadFormat.SelectedIndex != 3)) || ((cmbDownloadFormat_SelectedIndex != 3) && (cmbDownloadFormat.SelectedIndex == 3)))
                   {
                       if (MessageBox.Show("Existing information will be lost by changing download format type." + Environment.NewLine + "Would you like to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                       {
                           callValidation = false;
                           cmbDownloadFormat.SelectedIndex = cmbDownloadFormat_SelectedIndex;
                           callValidation = true;
                           return;
                       }
                       else
                       {
                           //c1AssociationDetails.Clear();
                           c1AssociationDetails.Rows.Count = 1;
                           DesignC1AssociationGrid();
                       }
                   }
               }
           }
            
            
            //if (cmbDownloadFormat.SelectedIndex >= 0)
            //{       
                if (cmbDownloadFormat.SelectedIndex <= 1)
                {
                    lblDMSCategory.Visible = false;
                    cmbDMScategory.Visible = false;
                }
                else
                {
                    lblDMSCategory.Visible = true;
                    cmbDMScategory.Visible = true;
                }
           // }

            if (cmbDownloadFormat.SelectedIndex > 0)
            {
                FillComboGroups();
                FillComboQuestions();
            }
            else
            {
                tvGroups.Nodes.Clear();
                tvQuestions.Nodes.Clear();
            }

            cmbDownloadFormat_SelectedIndex = cmbDownloadFormat.SelectedIndex;
        }

        private void TxtHealthFormName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFileName = TxtHealthFormName.Text.Trim();
                string sValidFileName = "";

                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "").Replace("|", "");
                if (sFileName != sValidFileName)
                {
                    TxtHealthFormName.Text = sValidFileName;
                    TxtHealthFormName.Select(TxtHealthFormName.Text.Length, 1);
                }
            }
            catch (Exception)
            {
            }
        }

        private void TxtHealthFormName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string sFileName = TxtHealthFormName.Text.Trim();
                string sValidFileName = "";

                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                if (sFileName != sValidFileName)
                {
                    TxtHealthFormName.Text = sValidFileName;
                    TxtHealthFormName.Select(TxtHealthFormName.Text.Length, 1);
                }
            }
            catch (Exception)
            {
            }
        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            setheadersize();
        }

     

     


    }
}
