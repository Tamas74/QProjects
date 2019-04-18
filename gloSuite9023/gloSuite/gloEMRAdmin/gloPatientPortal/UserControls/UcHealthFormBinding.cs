using System;
using System.Data;
using System.Windows.Forms;
using gloPatientPortal.Classes;
using C1.Win.C1FlexGrid;
using System.Drawing;
using System.Text;

namespace gloPatientPortal.UserControls
{
    public partial class UcHealthFormBinding : UserControl
    {

        #region "Variable Declaration"
        
        // Connection String for the database
        string _strConnectionString = string.Empty;
        string _strDMSConnectionString = string.Empty;
        // Unique Id of user logged in
        long _nLoginID = 0;
        // Unique Id of Patient form 
        long _nPFListId = 0;
        // Patient form Instance called on modify
        UcHealthFormAddEdit ObjUcHealthFormAddEdit = null;

        //// Column Index for C1Association Grid
        //int ncolPFListId = 0;
        //int ncolFormName = 1;
        //int ncolTotalGroups = 2;
        //int ncolTotalQuestions = 3;
        //int ncolActive = 4;
        ////User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
        ////Added new column and change no for other column.
        //int ncolEnableTaskNotification = 5;
        //int ncolPublish = 6;
        //int ncolPublishedDate = 7;
        //int ncolCreatedDate = 8;
        //int ncolnUserId = 9;
        //int ncolDownloadFormat = 10;
        //int nDMSCategoryID = 11;
        //int ncolCreatedBy = 12;
        //int ncolbIsRepublishRequired = 13;
        //int ncolDMSCategory = 14;
        ////int ncolDownloadFormat_1 = 11;

        //Column index changed to rearrange the column
        int ncolPFListId = 0;
        int ncolFormName = 1;
        int ncolActive = 2;
        int ncolPublish = 3;
        int ncolPublishedDate = 4;
        int ncolCreatedDate = 5;
        int ncolTotalGroups = 6;
        int ncolTotalQuestions = 7;
        int ncolEnableTaskNotification = 8;
        int ncolnUserId = 9;
        int ncolDownloadFormat = 10;
        int nDMSCategoryID = 11;
        int ncolbIsRepublishRequired = 12;
        int ncolDMSCategory = 13;

        int ncolCreatedBy = 14;

       

        #endregion

        #region "Constructors & Load"

        //default Constructor
        public UcHealthFormBinding()
        {
            InitializeComponent();
        }

        //Constructor for initializing Connection String & Unique Id for logged in User
        public UcHealthFormBinding(string strConnectionString, long nLoginID, string strDMSConnectionString)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
            _strDMSConnectionString = strDMSConnectionString;
        }

        //Load Event
        private void UcHealthFormBinding_Load(object sender, EventArgs e)
        {
            FillHealthForms();
            panel3.Show();
            lblHeading.Text = "Online Patient Forms";
        }

        #endregion

        
      

        #region "Methods and Functions"

        //Search Patient form in the grid
        private void SearchHealthForm()
        {
            DataView dvHealthForms = null;
            try
            {
                dvHealthForms = (DataView)c1Association.DataSource;
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    dvHealthForms.RowFilter = "[sFormName] Like '%" + txtSearch.Text.Trim().Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "") + "%' ";
                else
                    dvHealthForms.RowFilter = "";
                c1Association.DataSource = dvHealthForms;
                setimage();

                SetModifyEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dvHealthForms != null)
                    dvHealthForms = null;
            }
        }

        public void MatchDMSCategory()
        {
            try
            {
                clsHealthForm oclsHealthForm = null;
                DataTable dtHealthForms = null;
                DataTable dtDMScategory = null;
                oclsHealthForm = new clsHealthForm();
                dtHealthForms = oclsHealthForm.GetHealthForms(_strConnectionString, 0);
                dtDMScategory = oclsHealthForm.GetDMSCategory(_strDMSConnectionString, false);
                if (dtHealthForms != null && dtHealthForms.Rows.Count > 0)
                {

                    foreach (DataRow drform in dtHealthForms.Rows)
                    {
                        bool IsupdatedNeeded = true;
                        Int64 nPFListId = Convert.ToInt64(drform["nPFListId"]);
                        //drform.IsNull("DMSCategoryId")
                        Int16 DMSCategoryId = 0;
                        if (drform.IsNull("DMSCategoryId") == false)
                        {
                           DMSCategoryId = Convert.ToInt16(drform["DMSCategoryId"]);
                        }
                        string DownloadFormat = Convert.ToString(drform["DownloadFormat"]);

                        if (DownloadFormat.Trim() != getDownloadFormat(1))
                        {
                            if (dtDMScategory != null)
                            {
                                foreach (DataRow drDMS in dtDMScategory.Rows)
                                {
                                    if (Convert.ToInt32(drDMS["CategoryId"]) == DMSCategoryId)
                                    {
                                        IsupdatedNeeded = false;
                                    }
                                }
                            }
                            if (IsupdatedNeeded)
                            {
                                Int16 IdtoUpdate = GetIDtoUpdate(DMSCategoryId);
                                if (IdtoUpdate > 0)
                                {
                                    oclsHealthForm.UpdateDMScategoryforPFList(_strConnectionString, nPFListId, IdtoUpdate);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
        }

        public Int16 GetIDtoUpdate(Int16 DMSCategoryId)
        {
            try
            {
                Int16 ID = 0;
                clsHealthForm oclsHealthForm = null;
                oclsHealthForm = new clsHealthForm();
                DataTable dt = new DataTable();
                dt = oclsHealthForm.GetDMScategorytoUpdate(_strDMSConnectionString, DMSCategoryId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int16 IsUpdated = Convert.ToInt16(dt.Rows[0]["IsPatientForm"]);
                    Int16 DMSCategorytoUpdated = Convert.ToInt16(dt.Rows[0]["DMSCategoryID"]);
                    string DMSCategoryName = Convert.ToString(dt.Rows[0]["DMSCategoryName"]);
                    if (IsUpdated > 0)
                    {
                        ID = DMSCategorytoUpdated;
                    }
                }
                return ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
                return 0;
            }
        }

        //Fill the Patient form 
        public void FillHealthForms()
        {
            gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
            objgloC1FlexStyle.Style(c1Association);
            clsHealthForm oclsHealthForm = null;
            DataTable dtHealthForms = null;
            DataTable dtDMScategory = null;
            try
            {

                MatchDMSCategory();

                oclsHealthForm = new clsHealthForm();
                dtHealthForms = oclsHealthForm.GetHealthForms(_strConnectionString, 0);
                dtDMScategory = oclsHealthForm.GetDMSCategory(_strDMSConnectionString,false);
               
                if (dtHealthForms != null)
                {
                    c1Association.DataSource = dtHealthForms.DefaultView;
                    c1Association.AutoResize = true;
                    c1Association.Cols[ncolPFListId].Width = 0;
                    c1Association.Cols[ncolPFListId].Visible = false;
                    c1Association.Cols[ncolFormName].AllowEditing = false;
                    c1Association.Cols[ncolFormName].Width = Convert.ToInt32(Width * 0.25);
                    c1Association.Cols[ncolTotalGroups].AllowEditing = false;
                    c1Association.Cols[ncolTotalQuestions].AllowEditing = false;
                    c1Association.Cols[ncolTotalQuestions].Width = Convert.ToInt32(Width * 0.08);
                    c1Association.Cols[ncolTotalGroups].Width = Convert.ToInt32(Width * 0.08);
                    c1Association.Cols[ncolActive].Width = Convert.ToInt32(Width * 0.05);
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    //Set width for newly added column.
                    c1Association.Cols[ncolEnableTaskNotification].Width = Convert.ToInt32(Width * 0.11);
                    c1Association.Cols[ncolPublish].Width = Convert.ToInt32(Width * 0.06);
                    c1Association.Cols[ncolPublishedDate].AllowEditing = false;
                    c1Association.Cols[ncolPublishedDate].Width = Convert.ToInt32(Width * 0.1);
                    c1Association.Cols[ncolCreatedDate].AllowEditing = false;
                    c1Association.Cols[ncolnUserId].Width = 0;
                    c1Association.Cols[ncolnUserId].Visible = false;
                    c1Association.Cols[ncolDownloadFormat].Width = Convert.ToInt32(Width * 0.13);
                    c1Association.Cols[nDMSCategoryID].Width = Convert.ToInt32(Width * 0.1);
                    c1Association.Cols[ncolCreatedBy].AllowEditing = false;
                    c1Association.Cols[ncolPublish].AllowEditing = false;
                    c1Association.Cols[ncolbIsRepublishRequired].AllowEditing = false;
                    c1Association.Cols[ncolbIsRepublishRequired].Visible = false;
                    c1Association.Cols[ncolPublish].ImageAlign = ImageAlignEnum.CenterCenter;
                    c1Association.Cols[ncolDMSCategory].AllowEditing = false;
                    c1Association.Cols[ncolDMSCategory].Width = Convert.ToInt32(Width * 0.13);

                    c1Association.SetData(0, ncolPFListId, "nPFListId");
                    c1Association.SetData(0, ncolFormName, "Form Name");
                    c1Association.SetData(0, ncolTotalGroups, "Group(s)");
                    c1Association.SetData(0, ncolTotalQuestions, "Question(s)");
                    c1Association.SetData(0, ncolActive, "Active");
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    c1Association.SetData(0, ncolEnableTaskNotification, "Task Notification");
                    c1Association.SetData(0, ncolPublish, "Publish");
                    c1Association.SetData(0, ncolPublishedDate, "Published Date");
                    c1Association.SetData(0, ncolCreatedDate, "Created Date");
                    c1Association.SetData(0, ncolnUserId, "nUserId");
                    c1Association.SetData(0, ncolDownloadFormat, "Download Format");
                    c1Association.SetData(0, nDMSCategoryID, "DMS Category id");
                    c1Association.SetData(0, ncolCreatedBy, "User");
                    c1Association.SetData(0, ncolbIsRepublishRequired, "Republish Required");
                   
                    c1Association.Cols[nDMSCategoryID].Visible = false;
                  
                
                    setimage();
                    for (int i = 0; i <= dtHealthForms.Rows.Count - 1; i++)
                    {
                       
                        if (dtHealthForms.Rows[i]["DownloadFormat"] != null)
                        {
                           c1Association.SetData(i + 1, ncolDownloadFormat, dtHealthForms.Rows[i]["DownloadFormat"]);                           
                        }
                        if (dtHealthForms.Rows[i]["DownloadFormat"].ToString().Trim() != getDownloadFormat(1))
                        {
                            if (dtHealthForms.Rows[i].IsNull("DMSCategoryId") == false)
                            {
                                if (Convert.ToInt32(dtHealthForms.Rows[i]["DMSCategoryId"]) > 0)
                                {                                    
                                    for (int l = 0; l <= dtDMScategory.Rows.Count - 1; l++)
                                    {
                                        if (Convert.ToInt32(dtHealthForms.Rows[i]["DMSCategoryId"]) == Convert.ToInt32(dtDMScategory.Rows[l]["CategoryId"]))
                                        {
                                            c1Association.SetData(i + 1, ncolDMSCategory, dtDMScategory.Rows[l]["CategoryName"]);
                                            break;
                                        }
                                    }
                                }                                
                            }
                        }
                        else
                        {
                            c1Association.SetData(i + 1, nDMSCategoryID, "");
                        }
                    }
                    //c1Association.Cols[ncolCreatedBy].Move(ncolDMSCategory);
                
                    SetModifyEnabled();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;

                if (oclsHealthForm != null)
                    oclsHealthForm = null;
                if (dtHealthForms != null)
                {
                    dtHealthForms.Dispose();
                    dtHealthForms = null;
                }               

            }         

        }        
          

        public string getDownloadFormat(int value)
        {
            if (value == 1)
            {
                return "Discrete Data";
            }
            else if (value == 2)
            {
                return "PDF Document of Discrete Data";
            }
            else if (value == 3)
            {
                return "PDF Document of Non-Discrete Data";
            }
            else if (value == 4)
            {
                 return "Discrete Data & PDF Document";
            }
            return "";
        }

        public void SetModifyEnabled()
        {
            bool IsModifyToEnable = false;

            if (c1Association.Rows.Count > 1)
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


        private void setimage()
        {
            for (int i = 1; i <= c1Association.Rows.Count - 1; i++)
            {
                //Bug #92430: patient portal >> Patient form show publish column color green on online patient form screen in EMR admin application
                //Set publish image on bIsRepublishRequired flag. if false--> gray and true-->green.
                if (Convert.ToString(c1Association.Rows[i]["bIsRepublishRequired"]).ToUpper().Trim() == "TRUE")
                {
                    c1Association.SetCellImage(i, ncolPublish, imgList.Images[1]);
                }
                else
                {
                    c1Association.SetCellImage(i, ncolPublish, imgList.Images[0]);
                }
            }
        }

        //Adding a ComboBox to C1Grid cell
        private void AddComboToC1GridCell(string colname, int row, int col, string combolist)
        {
            CellStyle cs = c1Association.Styles.Add(colname);
            cs.DataType = typeof(string);
            cs.ComboList = combolist;
            CellRange rg = c1Association.GetCellRange(row, col);
            rg.Style = c1Association.Styles[colname];
        }

        private void AddDMSComboToC1GridCell(string colname, int row, int col, DataTable dtcombolist)
        {
            System.Collections.Hashtable dtMap = new System.Collections.Hashtable();
            for (int i = 0; i < dtcombolist.Rows.Count ; i++)
            {
                dtMap.Add(Convert.ToInt32(dtcombolist.Rows[i]["CategoryId"]), Convert.ToString(dtcombolist.Rows[i]["CategoryName"]));
            }
            
            CellStyle cs = c1Association.Styles.Add(colname);
            cs.DataType = typeof(Int32);
            cs.DataMap = dtMap;
            CellRange rg = c1Association.GetCellRange(row, col);
            rg.Style = c1Association.Styles[colname];

        }

        //Get Selected Patient form
        public Int64 GetHealthFormId()
        {
            if (c1Association.RowSel != -1)
            {
                return Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]);
            }
            else
                return 0;
        }

        #endregion

        #region "Events"

        //Creating a new instance of Patient form (Add/Edit) and displaying the selected record to modify
        private void c1Association_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (c1Association.RowSel < 1)
                return;

            if (c1Association.RowSel != 0)
            {
                panel1.Hide();
                pnlSearch.Hide();
                panel3.Hide();
                try
                {
                    if (ObjUcHealthFormAddEdit == null)
                    {

                        ObjUcHealthFormAddEdit = new UcHealthFormAddEdit(_strConnectionString, _nLoginID,_strDMSConnectionString);
                        ObjUcHealthFormAddEdit.BringToFront();
                        ObjUcHealthFormAddEdit.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(ObjUcHealthFormAddEdit);
                        ObjUcHealthFormAddEdit.FormAddEdit(Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]), true);
                     
                    }
                    else
                    {
                        ObjUcHealthFormAddEdit.FormAddEdit(Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]), true);
                        ObjUcHealthFormAddEdit.Show();
                    }

                    frmHealthForm objfrmHealthForm = (frmHealthForm)this.ParentForm;
                    objfrmHealthForm.setpfmodifybtns();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        //Calling the method to search Patient forms based on the text entered in Search textbox.
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchHealthForm();
        }

        //Modify Patient form Status
        private void c1Association_CellChecked(object sender, RowColEventArgs e)
        {
            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
            //Changes done for newly added column
            if (c1Association.ColSel == 11) return;
            if (c1Association.ColSel != ncolActive && c1Association.ColSel != ncolEnableTaskNotification)
                return;

            if (c1Association.ColSel == ncolActive)
            {
                if (c1Association.Rows[c1Association.RowSel][ncolActive].ToString() == "True")
                {
                    if (c1Association.Rows[c1Association.RowSel][ncolbIsRepublishRequired].ToString() == "True")
                    {
                        string message = "The current form has been deactivate due to changes in the Group/Question associated.\n\rYou can republish to activate the form:\n\r *Re-Publish: (Online Patient Form >> Modify >> Save and Preview >> Publish)";
                        DialogResult Result = MessageBox.Show(message, "gloEMRAdmin", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        if (Result == DialogResult.OK)
                        {
                            c1Association_MouseDoubleClick(sender, null);
                            return;
                        }
                        else
                        {
                            c1Association.SetData(c1Association.RowSel, ncolActive, "False");
                            return;
                        }
                    }
                    if (MessageBox.Show("Do you want to activate - " + c1Association.Rows[c1Association.RowSel][ncolFormName].ToString() + "?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        c1Association.SetData(c1Association.RowSel, ncolActive, "False");
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to deactivate - " + c1Association.Rows[c1Association.RowSel][ncolFormName].ToString() + "?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        c1Association.SetData(c1Association.RowSel, ncolActive, "True");
                        return;
                    }
                }
            }
            if (c1Association.ColSel == ncolEnableTaskNotification)
            {
                if (c1Association.Rows[c1Association.RowSel][ncolEnableTaskNotification].ToString() == "True")
                {
                    if (MessageBox.Show("Do you want to enable task notification for \"" + c1Association.Rows[c1Association.RowSel][ncolFormName].ToString() + "\"?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        c1Association.SetData(c1Association.RowSel, ncolEnableTaskNotification, "False");
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to disable task notification for \"" + c1Association.Rows[c1Association.RowSel][ncolFormName].ToString() + "\"?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        c1Association.SetData(c1Association.RowSel, ncolEnableTaskNotification, "True");
                        return;
                    }
                }
            }
            clsHealthForm oclsHistory = null;
            try
            {
                oclsHistory = new clsHealthForm();
                _nPFListId = Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]);
                Boolean bActive = false;
                Boolean bEnableTaskNotification = false;
                if (c1Association.Rows[c1Association.RowSel][ncolActive].ToString() == "True")
                    bActive = true;

                if (c1Association.Rows[c1Association.RowSel][ncolEnableTaskNotification].ToString() == "True")
                    bEnableTaskNotification = true;

                Int16 dmsCategoryID = 0;
                if (c1Association.Rows[c1Association.RowSel][nDMSCategoryID] != null)
                {
                    if (c1Association.Rows[c1Association.RowSel][nDMSCategoryID].ToString().Trim() != "")
                    {
                        dmsCategoryID = Convert.ToInt16(c1Association.Rows[c1Association.RowSel][nDMSCategoryID]);
                    }
                }
                _nPFListId = oclsHistory.AddPFHealthForm(c1Association.Rows[c1Association.RowSel][ncolFormName].ToString(), _nLoginID, _strConnectionString, true, _nPFListId, bActive, c1Association.Rows[c1Association.RowSel][ncolDownloadFormat].ToString(), dmsCategoryID, bEnableTaskNotification);
                if (_nPFListId > 0)
                {
                    if (c1Association.ColSel == ncolActive)
                    {
                        if (bActive)
                            MessageBox.Show("Patient form activated", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Patient form deactivated", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (c1Association.ColSel == ncolEnableTaskNotification)
                    {
                        if (bEnableTaskNotification)
                            MessageBox.Show("Task notification enabled", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Task notification disabled", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Failed to update patient form", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        //Clear Search
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            SearchHealthForm();
            txtSearch.Focus();
        }

       

        private void c1Association_ComboDropDown(object sender, RowColEventArgs e)
        {
            if (c1Association.ColSel == 10) return;
            if (c1Association.ColSel == ncolDownloadFormat && c1Association.RowSel > 0)
            {
                TempDownloadFormat = Convert.ToString(c1Association.GetData(c1Association.RowSel, ncolDownloadFormat));
            }
            else if (c1Association.ColSel == nDMSCategoryID && c1Association.RowSel > 0)
            {
                string downloadformattype = Convert.ToString(c1Association.GetData(c1Association.RowSel, ncolDownloadFormat));
                if (downloadformattype.Trim() !=getDownloadFormat(1))
                {
                    if (c1Association.Rows[c1Association.RowSel][nDMSCategoryID] == DBNull.Value)
                    {
                        TempDMSCategory = 0;
                    }
                    else
                    {
                        TempDMSCategory = Convert.ToInt32(c1Association.Rows[c1Association.RowSel][nDMSCategoryID]);
                    }
                }
                else
                {
                    e.Cancel = true;
                    IsValidDMS = false;
                    MessageBox.Show("DMS category selection is only available for :" + Environment.NewLine + Environment.NewLine + "1) " + getDownloadFormat(2) + " " + Environment.NewLine + "2) " + getDownloadFormat(3) + " " + Environment.NewLine + "3) " + getDownloadFormat(4), "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Update Donwload Format
        string TempDownloadFormat = "";
        int TempDMSCategory = 0;
        bool IsValidDMS = true;

        private void c1Association_AfterEdit(object sender, RowColEventArgs e)
        {
            if (c1Association.ColSel == 10) return;
            clsHealthForm oclsHistory = null;
            try
            {
                if (c1Association.ColSel == ncolDownloadFormat && e.Row > 0 && e.Row == c1Association.RowSel)  
                {

                    string downloadformat = string.Empty;
                    downloadformat = c1Association.Rows[c1Association.RowSel][ncolDownloadFormat].ToString();
                    if (TempDownloadFormat != downloadformat && TempDownloadFormat != "" && downloadformat != "")
                    {
                        oclsHistory = new clsHealthForm();
                        _nPFListId = Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]);
                        Boolean bActive = false;
                        Boolean bEnableTaskNotification = false;
                        if (c1Association.Rows[c1Association.RowSel][ncolActive].ToString() == "True")
                            bActive = true;

                        //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                        //Changes done for newly added column
                        if (c1Association.Rows[c1Association.RowSel][ncolEnableTaskNotification].ToString() == "True")
                            bEnableTaskNotification = true;

                        if (downloadformat.Trim() != getDownloadFormat(1))
                        {
                            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                            //Changes done for newly added column
                            _nPFListId = oclsHistory.AddPFHealthForm(c1Association.Rows[c1Association.RowSel][ncolFormName].ToString(), _nLoginID, _strConnectionString, true, _nPFListId, bActive, downloadformat, (c1Association.Rows[c1Association.RowSel][nDMSCategoryID] == DBNull.Value) ? 0 : Convert.ToInt32(c1Association.Rows[c1Association.RowSel][nDMSCategoryID]), bEnableTaskNotification);
                        }
                        else
                        {
                            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                            //Changes done for newly added column
                            _nPFListId = oclsHistory.AddPFHealthForm(c1Association.Rows[c1Association.RowSel][ncolFormName].ToString(), _nLoginID, _strConnectionString, true, _nPFListId, bActive, downloadformat, 0, bEnableTaskNotification);
                        }
                        if (_nPFListId > 0)
                        {
                            MessageBox.Show("Download Format changed", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillHealthForms();
                        }
                        else
                        {
                            MessageBox.Show("Failed to change Download Format", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    
                }
                else if (c1Association.ColSel == nDMSCategoryID && e.Row > 0 && e.Row == c1Association.RowSel)  
                {
                    oclsHistory = new clsHealthForm();
                    _nPFListId = Convert.ToInt64(c1Association.Rows[c1Association.RowSel][ncolPFListId]);
                    Boolean bActive = false;
                    Boolean bEnableTaskNotification = false;
                    if (c1Association.Rows[c1Association.RowSel][ncolActive].ToString() == "True")
                        bActive = true;

                    if (c1Association.Rows[c1Association.RowSel][ncolEnableTaskNotification].ToString() == "True")
                        bEnableTaskNotification = true;
                    
                    //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                    //Changes done for newly added column
                    string downloadformat = string.Empty;
                    downloadformat = c1Association.Rows[c1Association.RowSel][ncolDownloadFormat].ToString();

                    int AfterDMSval = 0;
                    if (c1Association.Rows[c1Association.RowSel][nDMSCategoryID] != DBNull.Value)
                    {
                        AfterDMSval = Convert.ToInt32(c1Association.Rows[c1Association.RowSel][nDMSCategoryID]);
                    }

                    if (TempDMSCategory != AfterDMSval && AfterDMSval != 0 && IsValidDMS == true)
                    {
                        if (downloadformat.Trim() != getDownloadFormat(1))
                        {
                            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                            //Changes done for newly added column
                            _nPFListId = oclsHistory.AddPFHealthForm(c1Association.Rows[c1Association.RowSel][ncolFormName].ToString(), _nLoginID, _strConnectionString, true, _nPFListId, bActive, downloadformat, (c1Association.Rows[c1Association.RowSel][nDMSCategoryID] == DBNull.Value) ? 0 : Convert.ToInt32(c1Association.Rows[c1Association.RowSel][nDMSCategoryID]), bEnableTaskNotification);
                        }
                        else
                        {
                            //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                            //Changes done for newly added column
                            _nPFListId = oclsHistory.AddPFHealthForm(c1Association.Rows[c1Association.RowSel][ncolFormName].ToString(), _nLoginID, _strConnectionString, true, _nPFListId, bActive, downloadformat, 0, bEnableTaskNotification);
                        }

                        if (_nPFListId > 0)
                        {
                            MessageBox.Show("DMS Category changed", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillHealthForms();
                        }
                        else
                        {
                            MessageBox.Show("Failed to change DMS Category", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    return;
                }

                TempDownloadFormat = "";
                TempDMSCategory = 0;
                IsValidDMS = true;
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
        }

        #endregion

        private void c1Association_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {

            try
            {
                if (c1Association.DataSource != null && c1Association.Rows.Count > 0)
                {
                    if (e.Row <= 0)
                        return;

                    if (c1Association.Cols[e.Col].Caption == "Form Name")
                    {

                        CellStyle NewStyle2;
                        NewStyle2 = c1Association.Styles.Add("NewStyle2");
                        //NewStyle2.BackColor = Color.Red;
                        NewStyle2.ForeColor = Color.Red;
                        
                        if (Convert.ToString(c1Association.Rows[e.Row]["bIsRepublishRequired"]).ToUpper().Trim() == "TRUE")
                        {
                            e.Style = NewStyle2;
                            e.Text = e.Text + "  [ Publish Required ]";
                           
                        }
                    }
                    if (c1Association.Cols[e.Col].Caption == "Publish")
                    {
                        if (Convert.ToString(c1Association.Rows[e.Row]["bIsRepublishRequired"]).ToUpper().Trim() == "TRUE")
                        {
                            e.Image = imgList.Images[1];
                        }
                        else
                        {
                            e.Image = imgList.Images[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void c1Association_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (c1Association.ColSel == 10) return;
            try
            {
                if (c1Association.DataSource != null && c1Association.Rows.Count > 0)
                {
                    if (e.Row <= 0)
                        return;

                    if (c1Association.Cols[e.Col].Caption == "DMS Category")
                    {
                        string downloadformat = Convert.ToString(c1Association.Cols[ncolDownloadFormat][e.Row]).Trim();

                        if (downloadformat.Trim() != getDownloadFormat(1))
                        {                       
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Boolean SaveHealthform()
        {
            return ObjUcHealthFormAddEdit.Save();
        }

        public Boolean PublishHealthform()
        {
            return ObjUcHealthFormAddEdit.Publish();
        }

        public void CloseHealthform()
        {
            ObjUcHealthFormAddEdit.Close();
        }

        public Boolean SaveandPreviewHealthform()
        {
            if (!ObjUcHealthFormAddEdit.SaveandPreview())
            {
                return false;
            }
            return true;
        }

    }
}
