using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_AddCategory : Form
    {
        
        public string _ErrorMessage = "";
        public bool isShowAddCategory= false;
        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;
        public frmEDocEvent_AddCategory()
        {
            InitializeComponent();
        }

        private void frmEDocEvent_AddCategory_Load(object sender, EventArgs e)
        {
            if (isShowAddCategory == true)
            {
                designListView();
                ShowAddCategory();
            }
            else
            {

                designListView();
                txtCategory.Visible = false;
                lblMandatory.Visible = false ;
                lblCategory.Visible = false;
                lvwCategory.Visible = true;
                fillCategories();

                if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                {
                    this.Text = " RCM Category";
                    this.Icon = global:: gloEDocumentV3.Properties.Resources.RCMDocs;
                }
                else
                {
                    this.Text = " DMS Category";
                    this.Icon = global:: gloEDocumentV3.Properties.Resources.DMSCategory;
                }
            }
        }

        private void ErrorMessagees(string _ErrorMessage)
        {
            #region " Make Log Entry "
            try
            {
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
            }
            catch (Exception ex)
            {
                string _ErrorHere = ex.ToString();
            }

            #endregion " Make Log Entry "

        }

        private void DeleteCategory(Enumeration.enum_OpenExternalSource _OpenExternalSource)
        {
            ListView.SelectedListViewItemCollection oSelectedItems = new ListView.SelectedListViewItemCollection(lvwCategory);
            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);

            ListViewItem oListItem = null;
            int _CategoryId = 0;
            string _CategoryName = "";
            string _sqlQuery = "";
            Object _res = 0;
            try
            {
               
                oSelectedItems = lvwCategory.SelectedItems;

                if (oSelectedItems != null)
                {
                    if (oSelectedItems.Count > 0)
                    {
                        for (int i = 0; i < oSelectedItems.Count; i++)
                        {
                            oListItem = new ListViewItem();
                            if (oListItem != null)
                            {
                                oListItem = oSelectedItems[i];
                                //Developer: Mitesh Patel
                                //Date:22-Feb-2012'
                                //Bug ID: 21371
                                if (string.Compare(oListItem.SubItems[1].Text, "Vaccine Information Statements", true) == 0)
                                {
                                    MessageBox.Show("System defined categories can not be deleted. ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return ;
                                }
                                if (string.Compare(oListItem.SubItems[1].Text, "Amendments", true) == 0)
                                {
                                    MessageBox.Show("System defined categories can not be deleted. ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                _CategoryId = Convert.ToInt32(oListItem.Text);
                                _CategoryName = oListItem.SubItems[1].Text;

                                //SUDHIR 20081230 - NOT TO ALLOW DELETE CATEGORY IF DOCUMENTS EXIST FOR SAME.
                                if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                                {
                                    _sqlQuery = "select count(*) from eDocument_Details_V3_RCM where Category='" + _CategoryName + "'";
                                }
                                else
                                {
                                    _sqlQuery = "select count(*) from eDocument_Details_V3 where Category='" + _CategoryName + "'";
                                }
                                
                                if (oDB != null)
                                {
                                    if (oDB.Connect(false))
                                    {
                                        _res = oDB.ExecuteScalar_Query(_sqlQuery);
                                        if (Convert.ToInt16(_res) != 0)
                                        {
                                            MessageBox.Show("Category in use cannot be deleted", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        if (MessageBox.Show("Are you sure you want to delete Category ?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Int64 _result = oManager.DeleteCategory(_CategoryId, _OpenExternalSource);
                                            fillCategories();

                                            if (_result <= 0)
                                            {
                                                MessageBox.Show("Problem deleting Category", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            if (oListItem != null)
                                            {
                                                oListItem = null;
                                            }
                                        }
                                    }
                                }
                            }

                        }//end - for (int i = 0; i < oSelectedItems.Count; i++)

                    }//end - if (oSelectedItems.Count > 0)

                }//end - if (oSelectedItems != null)
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                { 
                    oDB.Disconnect(); 
                    oDB.Dispose();
                    oDB = null;
                }                
            }
        }

        private void designListView()
        {
            try
            {
                lvwCategory.Items.Clear();
                lvwCategory.Columns.Clear();


                lvwCategory.Columns.Add("CategoryID");
                lvwCategory.Columns.Add("CategoryName");

                lvwCategory.Columns[0].Width = 0;
                lvwCategory.Columns[1].Width = lvwCategory.Width;

                lvwCategory.Visible = false;
                txtCategory.Visible = true;

                txtCategory.Text = "";
                
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void fillCategories()
        {
            gloEDocumentV3.eDocManager.eDocGetList oDocList = new gloEDocumentV3.eDocManager.eDocGetList();
            gloEDocumentV3.Common.Categories oCategories = null;//new gloEDocumentV3.Common.Categories();

            try
            {
                lvwCategory.Items.Clear();
                if (oDocList != null)
                {
                    oCategories = oDocList.GetCategories(gloEDocV3Admin.gClinicID, _OpenExternalSource);
                    if (oCategories != null)
                    {
                        if (oCategories.Count > 0)
                        {
                            for (int i = 0; i < oCategories.Count; i++)
                            {
                                ListViewItem oItem = new ListViewItem();
                                if (oItem != null)
                                {
                                    oItem.Text = oCategories[i].CategoryID.ToString();
                                    oItem.SubItems.Add(oCategories[i].CategoryName.ToString());
                                    lvwCategory.Items.Add(oItem);
                                    if (oItem != null)
                                    {
                                        oItem = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
            }
            finally
            {
                if (oDocList != null)
                {
                    oDocList.Dispose();
                    oDocList = null;
                }
                if (oCategories != null)
                {
                    oCategories.Dispose();
                    oCategories = null;
                }
            }
        }

        private void addCategory()
        {
            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            string _CategoryName = "";

            try
            {
                if (oManager != null)
                {
                    _CategoryName = txtCategory.Text.Trim().Replace("'", "");
                    //Check if Category already exists
                    if (eDocManager.eDocValidator.IsCategoryExists(0, _CategoryName, gloEDocV3Admin.gClinicID, _OpenExternalSource))
                    {
                        MessageBox.Show("Category already exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        oManager.AddCategory(_CategoryName, _OpenExternalSource);
                    }
                    fillCategories();
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                if (oManager != null)
                {
                    oManager.Dispose();
                    oManager = null;
                }
                if (_CategoryName != null)
                {
                    _CategoryName = null;
                }
            }
        }

        public Boolean openModify()
        {
            
            ListView.SelectedListViewItemCollection oSelectedItems = new ListView.SelectedListViewItemCollection(lvwCategory);
            ListViewItem oListItem = null;
            int _CategoryId = 0;
            string _CategoryName = "";

            try
            {
                if (oSelectedItems != null && oSelectedItems.Count > 0)
                {
                    oListItem = new ListViewItem();
                    if (oListItem != null)
                    {
                        oListItem = oSelectedItems[0];

                        //Developer: Mitesh Patel
                        //Date:22-Feb-2012'
                        //Bug ID: 21371
                        if (string.Compare(oListItem.SubItems[1].Text,"Vaccine Information Statements",true)==0)
                        {
                            MessageBox.Show("System defined categories can not be modified. ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;                            
                        }
                        if (string.Compare(oListItem.SubItems[1].Text, "Amendments", true) == 0)
                        {
                            MessageBox.Show("System defined categories can not be modified. ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        _CategoryId = Convert.ToInt32(oListItem.Text);
                        _CategoryName = oListItem.SubItems[1].Text;

                        if (oListItem != null)
                        {
                            oListItem = null;
                        }

                    }
                    txtCategory.Text = _CategoryName;
                    txtCategory.Select(0, _CategoryName.Length);
                    txtCategory.Focus();
                    txtCategory.Tag = _CategoryId;

                    return true;
                }
                else
                {
                    MessageBox.Show("Select category to Modify", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                if (oSelectedItems.Count > 0)
                {
                    oSelectedItems.Clear();
                    
                }

                if (oSelectedItems != null)
                {
                    
                    oSelectedItems = null;
                }
            }
        }

        private void modifyCategory(int CatId,string CatName)
        {
            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            try
            {
                if (oManager != null)
                {
                    oManager.UpdateCategory(CatId, CatName, _OpenExternalSource);
                }
                fillCategories();
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oManager != null)
                {
                    oManager.Dispose();
                    oManager = null;
                }
            }
        }

        private void tls_MaintainDoc_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Add":
                        {  
                            ShowAddCategory();
                        }
                        break;
                    case "Modify":
                        {

                            if (lvwCategory.Items.Count > 0)//Sandip Darade 20090309 to check the no of categoris avalable
                            {
                                if (lvwCategory.SelectedItems.Count > 0)
                                {
                                    if (openModify() != false)
                                    {
                                        lvwCategory.Visible = false;
                                        txtCategory.Visible = true;
                                        lblCategory.Visible = true;
                                        //line addeded by dipak to set visible mandatory * for Category
                                        lblMandatory.Visible = true;
                                        tlb_Add.Visible = false;
                                        tlb_Modify.Visible = false;
                                        tlb_Delete.Visible = false;
                                        tlb_Close.Visible = false;
                                        tlb_Save.Visible = true;
                                        tlb_Cancel.Visible = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Select category to modify", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            
                        }
                        break;
                    case "Delete":
                        {
                            if (lvwCategory.SelectedItems.Count > 0)
                            {
                                DeleteCategory(_OpenExternalSource);
                            }
                        }
                        break;
                    case "Close":
                        {
                            this.Close();
                        }
                        break;
                    case "Save":
                        {
                            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
                            //Line commented and modified by dipak 20091024 to fix bug no :4490 In DMS category it takes blank space.
                            //if (txtCategory.Text == "")
                            if (txtCategory.Text.Trim()  == "")
                            {
                                MessageBox.Show("Enter the category name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (Convert.ToInt64(txtCategory.Tag) == 0)
                            {
                                if (eDocManager.eDocValidator.IsCategoryExists(0, txtCategory.Text.Trim(), gloEDocV3Admin.gClinicID, _OpenExternalSource))
                                {
                                    MessageBox.Show("Category already exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCategory.SelectAll();
                                    return;
                                }
                                else
                                {
                                    addCategory();
                                }

                            }
                            else if(Convert.ToInt64(txtCategory.Tag) > 0)
                            {
                                if (eDocManager.eDocValidator.IsCategoryExists(Convert.ToInt32(txtCategory.Tag), txtCategory.Text.Trim(), gloEDocV3Admin.gClinicID, _OpenExternalSource))
                                {
                                        MessageBox.Show("Category already exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtCategory.SelectAll();
                                        return;
                                }
                                else
                                {
                                    modifyCategory(Convert.ToInt32(txtCategory.Tag), txtCategory.Text.Trim());
                                }
                            }
                            oManager.Dispose();
                            //line addeded by dipak to set visible=false mandatory * for Category
                            lblMandatory.Visible = false ;
                            lvwCategory.Visible = true;
                            txtCategory.Visible = false;
                            lblCategory.Visible = false;
                            tlb_Add.Visible = true;
                            tlb_Modify.Visible = true;
                            tlb_Delete.Visible = true;
                            tlb_Close.Visible = true;
                            tlb_Save.Visible = false;
                            tlb_Cancel.Visible = false;

                        }
                        break;
                    case "Cancel":
                        {
                            //line addeded by dipak to set visible=false mandatory * for Category
                            lblMandatory.Visible = false ;
                            lvwCategory.Visible = true;
                            txtCategory.Visible = false;
                            lblCategory.Visible = false;
                            tlb_Add.Visible = true;
                            tlb_Modify.Visible = true;
                            tlb_Delete.Visible = true;
                            tlb_Close.Visible = true;
                            tlb_Save.Visible = false;
                            tlb_Cancel.Visible = false;
                        }
                        break;
                    case "View" :
                        break;

                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
            }

        }

        public  void ShowAddCategory()
        {
            //line addeded by dipak to set visible mandatory * for Category
            lblMandatory.Visible = true  ;
            lvwCategory.Visible = false;
            txtCategory.Visible = true;
            lblCategory.Visible = true;
            tlb_Add.Visible = false;
            tlb_Modify.Visible = false;
            tlb_Delete.Visible = false;
            tlb_Close.Visible = false;
            tlb_Save.Visible = true;
            tlb_Cancel.Visible = true;

            txtCategory.Tag = 0;
            txtCategory.Text = "";
            txtCategory.Select();
        }

        private void txtCategory_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string sFileName = txtCategory.Text.Trim();
                string sValidFileName = "";
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");

                if (sFileName != sValidFileName)
                {
                    txtCategory.Text = sValidFileName;
                    txtCategory.Select(txtCategory.Text.Length, 1);
                }
            }
            catch(Exception ex)
            {
                _ErrorMessage  = ex.Message;

                ErrorMessagees(_ErrorMessage);
            }
        }
      

    }
}
