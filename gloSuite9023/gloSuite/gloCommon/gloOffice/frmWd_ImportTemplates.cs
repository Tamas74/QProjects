using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloOffice
{
    public partial class frmWd_ImportTemplates : Form
    {

        #region " Global Variables "
        private String _MessageBoxCaption = String.Empty;
        private String _ConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private static frmWd_ImportTemplates frm;
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

        public delegate void CloseButtonClick(object sender, EventArgs e);
        public event CloseButtonClick CloseButton_Click;

        #region " Constructors "
        private frmWd_ImportTemplates()
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

        private frmWd_ImportTemplates(String DataBaseConnectionString)
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

        public static frmWd_ImportTemplates GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ImportTemplates();
                }
            }
            finally
            {

            }
            return frm;
        }

        public static frmWd_ImportTemplates GetInstance(String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ImportTemplates(DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #region " Private Functions "

        private void Fill_Categories()
        {
            gloTemplate ogloTemplate = new gloTemplate(_ConnectionString);
            DataTable dt = new DataTable();
            dt = ogloTemplate.GetTemplateCategoryList();
            //nCategoryID, sDescription
            if (dt != null)
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

        private void Fill_Providers()
        {
            gloAppointmentBook.gloAppointmentBook oAppointmentBook = new gloAppointmentBook.gloAppointmentBook(_ConnectionString);
            DataTable dt = new DataTable();
            dt = oAppointmentBook.getProviders();
            //nProviderID, ProviderName
            if (dt != null)
            {
                DataRow objrow;
                objrow = dt.NewRow();
                objrow[0] = 0;
                objrow[1] = "All";
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

        private void ImportTemplate()
        {
            StringBuilder strinvalidtemplatelist = new StringBuilder() ;
            try
            {
                if (string.IsNullOrEmpty(txtDirectoryPath.Text.Trim()))
                {
                    MessageBox.Show("Please select the Import Directory", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDirectoryPath.Focus();
                    return;
                }
                if (System.IO.Directory.Exists(txtDirectoryPath.Text.Trim()) == false)
                {
                    MessageBox.Show("Please select the valid Import Directory Path", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDirectoryPath.Focus();
                    return;
                }
                if (trvCategory.SelectedNode == null)
                {
                    MessageBox.Show("Please select the Category in which you want to import the templates", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    trvCategory.Focus();
                    return;
                }
                Int32 nCount;
                Int32 nTotalTemplatesSelected = 0;

                for (nCount = 0; nCount <= trvTemplates.GetNodeCount(true) - 1; nCount++)
                {
                    if (trvTemplates.Nodes[nCount].Checked)
                    {
                        nTotalTemplatesSelected = nTotalTemplatesSelected + 1;
                    }
                }
                if (nTotalTemplatesSelected <= 0)
                {
                    MessageBox.Show("Please select at least one template which you want to import", _MessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    trvTemplates.Focus();
                    return;
                }

                Cursor = Cursors.WaitCursor;
                gloTemplate oTemplate = new gloTemplate(_ConnectionString);
                String strTemplateName = "";
                String strFullPath = "";
                Int64 category_ID = 0;

                //00000601 :  Import templates from gloPM  not show in gloEMR
                // get the category id of selected tree node.
                category_ID  = oTemplate.GetCategoryID(trvCategory.SelectedNode.Text.ToString());  
                //category_ID = GetCategoryID(trvCategory.SelectedNode.Text.ToString());
                // End

                for (nCount = 0; nCount <= trvTemplates.GetNodeCount(true) - 1; nCount++)
                {
                    if (trvTemplates.Nodes[nCount].Checked)
                    {
                        strTemplateName = trvTemplates.Nodes[nCount].Text.Trim();
                        strFullPath = trvTemplates.Nodes[nCount].Tag.ToString();
                        Int32 cnt = 1;
                     
                                   if( strTemplateName.Trim().Length >= 45)//  ''added condition for bugid 73816 to not import templates having more than 45 characters for maintaining unique name
                                   {
                                       strinvalidtemplatelist.Append(strTemplateName +"\n");
                                          continue ;

                                   }

                        while (oTemplate.IsTemplateNamePresent(strTemplateName, trvCategory.SelectedNode.Text.ToString(), Convert.ToInt64(cmbProvider.SelectedValue)) == true)
                        {
                            strTemplateName = trvTemplates.Nodes[nCount].Text.ToString() + "_" + cnt;
                            cnt = cnt + 1;
                        }
                        oTemplate.SaveTemplate(0, strTemplateName, category_ID, trvCategory.SelectedNode.Text.ToString(), Convert.ToInt32(cmbProvider.SelectedValue), strFullPath, "", "");
                    }
                }
                if (strinvalidtemplatelist.ToString().Trim() == String.Empty)
                {

                    MessageBox.Show("All Templates Imported Successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Following templates are not loaded as the length of their names are more than 45 characters. Please give smaller names to the template documents and reload again." + "\n" +"\n" + strinvalidtemplatelist.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
                }
                Cursor = Cursors.Default;
                strinvalidtemplatelist = null;
                if (oTemplate != null)
                {
                    oTemplate.Dispose();
                }
            }
            catch (Exception)// Ex)
            {
                MessageBox.Show("All Templates are not Imported", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Ex.ToString();
                //Ex = null;
            }
            Cursor = Cursors.Default;
        }

        #endregion  

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Import":
                        {
                            ImportTemplate();
                            for (int i = 0; i < trvTemplates.Nodes.Count; i++)
                            {
                                trvTemplates.Nodes[i].Checked = false;
                            }
                            tsb_ClearAll.Visible = false;
                            tsb_SelectAll.Visible = true;
                        }
                        break;
                    case "Close":
                        { this.Close(); }
                        break;
                    case "SelectAll":
                        {
                            if (trvTemplates.Nodes.Count > 0)
                            {
                                for (int i = 0; i < trvTemplates.Nodes.Count; i++)
                                {
                                    trvTemplates.Nodes[i].Checked = true;
                                }
                                tsb_ClearAll.Visible = true;
                                tsb_SelectAll.Visible = false;
                            }
                        }
                        break;
                    case "ClearAll":
                        {
                            for (int i = 0; i < trvTemplates.Nodes.Count; i++)
                            {
                                trvTemplates.Nodes[i].Checked = false;
                            }
                            tsb_ClearAll.Visible = false;
                            tsb_SelectAll.Visible = true;
                        }
                        break;
                      
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmWd_ImportTemplates_Load(object sender, EventArgs e)
        {
            Fill_Categories();
            Fill_Providers();
        }

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    folderBrowserDialog1.Description = "Select Directory in which Templates to Import";
                    folderBrowserDialog1.ShowNewFolderButton = true;
                }
                if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    txtDirectoryPath.Text = folderBrowserDialog1.SelectedPath;

                    System.IO.DirectoryInfo objDirectory = new System.IO.DirectoryInfo(txtDirectoryPath.Text);
                    //System.IO.FileInfo objFile;
                    {
                        trvTemplates.BeginUpdate();
                        trvTemplates.Nodes.Clear();
                       // Int32 nCount;
                        //nCount = 0 To objFiles.GetUpperBound(0)

                        foreach (System.IO.FileInfo objFile in objDirectory.GetFiles("*.doc*"))
                        {
                            TreeNode myNode = new TreeNode();                            
                            myNode.Text = objFile.Name.Replace(objFile.Extension, "");
                            myNode.Tag = objFile.FullName;
                            trvTemplates.Nodes.Add(myNode);
                        }
                        trvTemplates.ExpandAll();
                        trvTemplates.EndUpdate();
                    }
                    objDirectory = null;

                    lblTotalTemplates.Text = "  Total Templates : " + trvTemplates.Nodes.Count;

                    tsb_SelectAll.Visible = true;
                    tsb_ClearAll.Visible = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to browse the Export Directory Path due to " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        ~frmWd_ImportTemplates()
        {
            Dispose(false);
        }

        private void frmWd_ImportTemplates_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        private void frmWd_ImportTemplates_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseButton_Click != null)
            {
                CloseButton_Click(null, null); 
            }
        }

        private void txtDirectoryPath_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtDirectoryPath.Text.Trim()))
                    {
                        MessageBox.Show("Please select the Import Directory", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDirectoryPath.Focus();
                        return;
                    }
                    if (System.IO.Directory.Exists(txtDirectoryPath.Text.Trim()) == false)
                    {
                        MessageBox.Show("Please select the valid Import Directory Path", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDirectoryPath.Focus();
                        return;
                    }
                    else
                    {
                        System.IO.DirectoryInfo objDirectory = new System.IO.DirectoryInfo(txtDirectoryPath.Text);
                        {
                            trvTemplates.BeginUpdate();
                            trvTemplates.Nodes.Clear();
                            foreach (System.IO.FileInfo objFile in objDirectory.GetFiles("*.doc*"))
                            {
                                TreeNode myNode = new TreeNode();
                                myNode.Text = objFile.Name.Replace(objFile.Extension, "");
                                myNode.Tag = objFile.FullName;
                                trvTemplates.Nodes.Add(myNode);
                            }
                            trvTemplates.ExpandAll();
                            trvTemplates.EndUpdate();
                        }
                        objDirectory = null;

                        lblTotalTemplates.Text = "  Total Templates : " + trvTemplates.Nodes.Count;
                        tsb_SelectAll.Visible = true;
                        tsb_ClearAll.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get files from Path due to " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        

    }
}