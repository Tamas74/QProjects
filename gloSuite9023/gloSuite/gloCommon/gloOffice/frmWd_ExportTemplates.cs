using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloOffice
{
    public partial class frmWd_ExportTemplates : Form
    {

        #region " Global Variables "
        private String _MessageBoxCaption = String.Empty;
        private String _ConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private static frmWd_ExportTemplates frm;
        private bool blnDisposed;

        public String ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        public String MessageBoxCaption
        {
            get { return _MessageBoxCaption; }
            set { _MessageBoxCaption = value; }
        }
        #endregion

        #region " C1 Constants "
        //nTemplateID, sTemplateName, nCategoryID, nProviderID, sDescription
        const int COL_TEMPLATEID = 0;
        const int COL_TEMPLATESELECT = 1;
        const int COL_TEMPLATENAME = 2;
        const int COL_TEMPLATECATAGORY = 3;
        const int COL_TEMPLATEPROVIDERNAME = 4;
        const int COL_TEMPLATETOTAL = 5;
        #endregion

        #region " Constructors "

        private frmWd_ExportTemplates()
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        private frmWd_ExportTemplates(String DataBaseConnectionString)
        {
            _ConnectionString = DataBaseConnectionString;
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion

        public static frmWd_ExportTemplates GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ExportTemplates();
                }
            }
            finally
            {

            }
            return frm;
        }

        public static frmWd_ExportTemplates GetInstance(String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ExportTemplates(DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }



        #region " Private Functions "

        private void Fill_Templates()
        {
            Int32 nSelectedCategoriesCount = 0;
            DataSet dsAllTemplates = new DataSet();
            DataTable dt = new DataTable();
            DataView dtv;
            Int32 nCount = 0;
            gloTemplate oTemplate = new gloTemplate(_ConnectionString);
            for (nCount = 0; nCount <= trvCategory.GetNodeCount(true) - 1; nCount++)
            {
                if (trvCategory.Nodes[nCount].Checked)
                {
                    nSelectedCategoriesCount = nSelectedCategoriesCount + 1;
                    dsAllTemplates.Merge(oTemplate.GetTemplates(trvCategory.Nodes[nCount].Text.ToString()));
                }

            }                      


            // To Fill C1 by Template List.
            if (nSelectedCategoriesCount >= 1)
            {
                dtv = new DataView(dsAllTemplates.Tables[0]);

                if (Convert.ToInt64(cmbProvider.SelectedValue) > 0)
                {
                    dtv.RowFilter = " nProviderID = " + cmbProvider.SelectedValue ;
                }

                dt = dtv.ToTable();
                oTemplate = null;
                {
                    c1Templates.Rows.Count = 1;
                    for (nCount = 0; nCount <= dt.Rows.Count - 1; nCount++)
                    {
                        c1Templates.Rows.Add();
                        c1Templates.SetData(c1Templates.Rows.Count - 1, COL_TEMPLATEID, dt.Rows[nCount]["nTemplateID"].ToString());
                        c1Templates.SetData(c1Templates.Rows.Count - 1, COL_TEMPLATESELECT, true);
                        c1Templates.SetData(c1Templates.Rows.Count - 1, COL_TEMPLATENAME, dt.Rows[nCount]["sTemplateName"].ToString());
                        c1Templates.SetData(c1Templates.Rows.Count - 1, COL_TEMPLATECATAGORY, dt.Rows[nCount]["CategoryName"].ToString());
                        c1Templates.SetData(c1Templates.Rows.Count - 1, COL_TEMPLATEPROVIDERNAME, dt.Rows[nCount]["sProviderName"].ToString());
                    }
                }


            }
            else
            {
                DesignTemplateGrid();
            }

            // To Change Button State of SelectAllClearAll on Refresh.
            if (c1Templates.Rows.Count > 1)
            {
                tsb_ClearAllDocument.Visible = true;
                tsb_SelectAllDocument.Visible = false;
            }
            else
            {
                tsb_SelectAllCategory.Visible = true;
                tsb_ClearAllCategory.Visible = false;
                tsb_SelectAllDocument.Visible = false;
                tsb_ClearAllDocument.Visible = false;
            }

            // To display Total Template Count.
            lblTotalTemplates.Text = "  Total Templates : " + (c1Templates.Rows.Count - 1);

        }

        private void Fill_Categories()
        {
            gloTemplate ogloTemplate = new gloTemplate(_ConnectionString);
            DataTable dt = new DataTable();
            dt = ogloTemplate.GetTemplateCategoryList();
            //nCategoryID, sDescription
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int i;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = dt.Rows[i]["CategoryName"].ToString();                        
                        trvCategory.Nodes.Add(oNode);
                    }
                }
            }

        }

        private void fill_Providers()
        {
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_ConnectionString);
            DataTable dt = new DataTable();
            dt = oResource.GetProviders();
            //nProviderID, ProviderName
            if (dt != null)
            {
                DataRow objrow;
                objrow = dt.NewRow();
                objrow[0] = 0;
                objrow["ProviderName"] = "All";
                dt.Rows.InsertAt(objrow, 0);
                cmbProvider.DataSource = dt;
                cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                if (dt.Rows.Count > 0)
                {
                    cmbProvider.SelectedIndex = 0;
                }
            }
        }

        private void DesignTemplateGrid()
        {
            c1Templates.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
            c1Templates.Cols.Count = COL_TEMPLATETOTAL;
            c1Templates.Rows.Count = 1;
            c1Templates.Rows.Fixed = 1;

            c1Templates.SetData(0, COL_TEMPLATEID, "Template ID");
            c1Templates.SetData(0, COL_TEMPLATESELECT, "Select");
            c1Templates.SetData(0, COL_TEMPLATENAME, "Template Name");
            c1Templates.SetData(0, COL_TEMPLATECATAGORY, "Category");
            c1Templates.SetData(0, COL_TEMPLATEPROVIDERNAME, "Provider");

            c1Templates.Cols[COL_TEMPLATEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Templates.Cols[COL_TEMPLATENAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Templates.Cols[COL_TEMPLATECATAGORY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Templates.Cols[COL_TEMPLATEPROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            //Set visibilities and Behaviors
            c1Templates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1Templates.AllowEditing = true;
            c1Templates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1Templates.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            c1Templates.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1Templates.Cols[COL_TEMPLATEID].Visible = false;
            
            c1Templates.Cols[COL_TEMPLATESELECT].DataType = Type.GetType("System.Boolean");
            c1Templates.Cols[COL_TEMPLATEID].AllowEditing = false;
            c1Templates.Cols[COL_TEMPLATESELECT].AllowEditing = true;
            c1Templates.Cols[COL_TEMPLATENAME].AllowEditing = false;
            c1Templates.Cols[COL_TEMPLATECATAGORY].AllowEditing = false;
            c1Templates.Cols[COL_TEMPLATEPROVIDERNAME].AllowEditing = false;

            //Set Widths
            int c1Width = c1Templates.Width - 1;
            c1Templates.Cols[COL_TEMPLATEID].Width = 0;
            c1Templates.Cols[COL_TEMPLATESELECT].Width = 50;
            c1Templates.Cols[COL_TEMPLATENAME].Width = (int)(c1Width * 0.45) - 1;
            c1Templates.Cols[COL_TEMPLATECATAGORY].Width = (int)(c1Width * 0.2) - 1; ;
            c1Templates.Cols[COL_TEMPLATEPROVIDERNAME].Width = (int)(c1Width * 0.3);
        }

        private string ValidFileDirectoryName(string strFileDirectoryName)
        {            
            return strFileDirectoryName.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("!", "").ToString();
        }

        private void ExportTemplate()
        {
            if (string.IsNullOrEmpty(txtDirectoryPath.Text.Trim()))
            {
                MessageBox.Show("Please select the Export Directory", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDirectoryPath.Focus();
                return;
            }
            if (System.IO.Directory.Exists(txtDirectoryPath.Text.Trim()) == false)
            {
                MessageBox.Show("Please select the valid Export Directory Path", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDirectoryPath.Focus();
                return;
            }

            bool _IsChecked = false;
            for (int i = 1; i < c1Templates.Rows.Count; i++)
            {
                if (c1Templates.GetCellCheck(i, COL_TEMPLATESELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    _IsChecked = true;
                    break;
                }
            }
            if (_IsChecked == false)
            {
                MessageBox.Show("Please select the Template to Export", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (c1Templates.Rows.Count > 1)
            {
                gloTemplate oTemplate = new gloTemplate(_ConnectionString);

                DataTable dtTemplate;
                String strFileName = "";
                Int64 nTemplateID;
                String strCategoryName = "";
                String strTemplateName = "";
                bool _Success = true;

                Cursor = Cursors.WaitCursor;

                for (int i = 1; i < c1Templates.Rows.Count; i++)
                {
                    if (c1Templates.GetCellCheck(i, COL_TEMPLATESELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        try
                        {
                            dtTemplate = new DataTable();

                            nTemplateID = Convert.ToInt64(c1Templates.Rows[i][COL_TEMPLATEID].ToString());
                            strCategoryName = c1Templates.Rows[i][COL_TEMPLATECATAGORY].ToString();
                            strCategoryName = ValidFileDirectoryName(strCategoryName);
                            strTemplateName = ValidFileDirectoryName(c1Templates.Rows[i][COL_TEMPLATENAME].ToString());

                            if (System.IO.Directory.Exists(txtDirectoryPath.Text + "\\" + strCategoryName) == false)
                            {
                                System.IO.Directory.CreateDirectory(txtDirectoryPath.Text + "\\" + strCategoryName);
                            }

                            dtTemplate = oTemplate.GetSingleTemplate(nTemplateID);
                            if (dtTemplate != null)
                            {
                                if (dtTemplate.Rows.Count > 0)
                                {
                                    strFileName = txtDirectoryPath.Text + "\\" + strCategoryName + "\\" + strTemplateName + ".docx";
                                    Int32 j = 1;
                                    while (System.IO.File.Exists(strFileName))
                                    {
                                        strFileName = txtDirectoryPath.Text + "\\" + strCategoryName + "\\" + strTemplateName + "_" + j + ".docx";
                                        j = j + 1;
                                    }
                                    oTemplate.ConvertBinaryToFile(dtTemplate.Rows[0]["sDescription"], strFileName);
                                    dtTemplate = null;
                                }
                            }
                        }
                        catch (Exception)// Ex)
                        {
                            MessageBox.Show("Template " + c1Templates.Rows[i][COL_TEMPLATENAME].ToString() + " has not been Exported ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _Success = false;
                            //Ex.ToString();
                            //Ex = null;
                        }
                    }
                }

                if (oTemplate != null)
                {
                    oTemplate.Dispose();
                }
                Cursor = Cursors.Default;
                if (_Success == true)
                {
                    MessageBox.Show("All Templates Exported Successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All Templates not Exported ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #region " Initial Buttons "
                for (int i = 0; i < trvCategory.Nodes.Count; i++)
                {
                    trvCategory.Nodes[i].Checked = false;
                }
                tsb_SelectAllCategory.Visible = true;
                tsb_ClearAllCategory.Visible = false;
                tsb_ClearAllDocument.Visible = false;
                tsb_SelectAllDocument.Visible = false;
                #endregion
            }
        }              

        #endregion

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Export":
                        {
                            ExportTemplate();                            
                        }
                        break;
                    case "Close":
                        { this.Close(); }
                        break;
                    case "SelectAllCat":
                        {
                            for (int i = 0; i < trvCategory.Nodes.Count; i++)
                            {
                                trvCategory.Nodes[i].Checked = true;
                            }
                            tsb_SelectAllCategory.Visible = false;
                            tsb_ClearAllCategory.Visible = true;
                            tsb_SelectAllDocument.Visible = false;
                            tsb_ClearAllDocument.Visible = true;
                        }
                        break;
                    case "ClearAllCat":
                        {
                            for (int i = 0; i < trvCategory.Nodes.Count; i++)
                            {
                                trvCategory.Nodes[i].Checked = false;
                            }
                            tsb_SelectAllCategory.Visible = true;
                            tsb_ClearAllCategory.Visible = false;
                            tsb_SelectAllDocument.Visible = false;
                            tsb_ClearAllDocument.Visible = false;
                        }
                        break;
                    case "SelectAllDoc":
                        {
                            for (int i = 1; i < c1Templates.Rows.Count; i++)
                            {
                                c1Templates.SetCellCheck(i, COL_TEMPLATESELECT, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            tsb_SelectAllDocument.Visible = false;
                            tsb_ClearAllDocument.Visible = true;
                        }
                        break;
                    case "ClearAllDoc":
                        {
                            for (int i = 1; i < c1Templates.Rows.Count; i++)
                            {
                                c1Templates.SetCellCheck(i, COL_TEMPLATESELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                            tsb_SelectAllDocument.Visible = true;
                            tsb_ClearAllDocument.Visible = false;
                        }
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmWd_ExportTemplates_Load(object sender, EventArgs e)
        {
            try
            {
                Fill_Categories();
                fill_Providers();
                DesignTemplateGrid();
            }
            catch (Exception)// Ex)
            {
                //Ex.ToString();
                //Ex = null;
            }
        }

        private void trvCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                Fill_Templates();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    folderBrowserDialog1.Description = "Select Directory in which Templates to Export";
                    folderBrowserDialog1.ShowNewFolderButton = true;
                }
                if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    txtDirectoryPath.Text = folderBrowserDialog1.SelectedPath;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to browse the Export Directory Path due to " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbProvider_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fill_Templates();
        }


        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }


        #region " Form Events"

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    try
                    {
                        if (folderBrowserDialog1 != null)
                        {

                            folderBrowserDialog1.Dispose();
                            folderBrowserDialog1 = null;
                        }
                    }
                    catch
                    {
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmWd_ExportTemplates()
        {
            Dispose(false);
        }

        private void frmWd_ExportTemplates_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion
        
    }
}