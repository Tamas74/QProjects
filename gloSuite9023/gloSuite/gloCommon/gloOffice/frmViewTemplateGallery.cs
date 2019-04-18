using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloOffice
{
    public partial class frmViewTemplateGallery : Form
    {

        #region " Global declarations "

        private String _messageBoxCaption = String.Empty;
        private String _databaseconnectionstring = "";

        private static frmViewTemplateGallery frm;
        private bool blnDisposed;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ProviderID = 0;

        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public String Databaseconnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public String MessageBoxCaption
        {
            get { return _messageBoxCaption; }
            set { _messageBoxCaption = value; }
        }

        #endregion " Global declarations "

        #region " Property Procedures "

        private long _templateID = 0;
        private long _categoryID = 0;

        public long CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        public long TemplateID
        {
            get { return _templateID; }
            set { _templateID = value; }
        }

        #endregion " Property Procedures "

        public delegate void CloseButtonClick(object sender, EventArgs e);
        public event CloseButtonClick CloseButton_Click;

        #region " Constructor "

        private frmViewTemplateGallery(string DataBaseConnetionString)
        {
            _databaseconnectionstring = DataBaseConnetionString;
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            if (appSettings["ProviderID"] != null)
            {
                if (appSettings["ProviderID"] != "")
                { _ProviderID = Convert.ToInt64(appSettings["ProviderID"]); }
                else
                { _ProviderID = 0; }
            }
            else
            { _ProviderID = 0; }
        }

        #endregion " Constructor "

        public static frmViewTemplateGallery GetInstance(string DataBaseConnetionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmViewTemplateGallery(DataBaseConnetionString);
                }
            }
            finally
            {

            }
            return frm;
        }


        #region " C1 Constants "

        //nTemplateID, sTemplateName, nCategoryID, nProviderID, sDescription
        const int COL_TEMPLATEID = 0;
        const int COL_TEMPLATENAME = 1;
        const int COL_TEMPLATEPROVIDERID = 2;
        const int COL_TEMPLATEPROVIDERNAME = 3;
        const int COL_TEMPLATECATNAME = 4;
        //const int COL_TEMPLATEDESC = 0;

        const int COL_COUNTTEMPLATE = 5;


        private void DesignTemplateGrid()
        {
            //    //Define Rows and Cols
            //    c1TemplateGallery.Row = 1;
            //    c1TemplateGallery.Cols.Count = COL_COUNTTEMPLATE;

            
            
            c1TemplateGallery.Rows.Fixed = 1;

            //Set Headers
            //nTemplateID, sTemplateName, nCategoryID, nProviderID, sDescription

            c1TemplateGallery.SetData(0, COL_TEMPLATEID, "Template ID");
            c1TemplateGallery.Cols[COL_TEMPLATEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1TemplateGallery.SetData(0, COL_TEMPLATENAME, "Template Name");
            c1TemplateGallery.Cols[COL_TEMPLATENAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1TemplateGallery.SetData(0, COL_TEMPLATEPROVIDERID, "Template ProviderID");
            c1TemplateGallery.Cols[COL_TEMPLATEPROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1TemplateGallery.SetData(0, COL_TEMPLATEPROVIDERNAME, "Provider");
            c1TemplateGallery.Cols[COL_TEMPLATEPROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1TemplateGallery.SetData(0, COL_TEMPLATECATNAME, "Category Name");
            c1TemplateGallery.Cols[COL_TEMPLATECATNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            //Set visibilities and Behaviors
            c1TemplateGallery.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1TemplateGallery.AllowEditing = false;
            c1TemplateGallery.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1TemplateGallery.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            c1TemplateGallery.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1TemplateGallery.Cols[COL_TEMPLATEID].Visible = false;
            c1TemplateGallery.Cols[COL_TEMPLATEPROVIDERID].Visible = false;
            c1TemplateGallery.Cols[COL_TEMPLATECATNAME].Visible = false;


            //Set Widths
            int c1Width = pnlMain.Width - 1;
            c1TemplateGallery.Cols[COL_TEMPLATEID].Width = 0;
            c1TemplateGallery.Cols[COL_TEMPLATENAME].Width = (int)(c1Width * 0.5) - 1;
            c1TemplateGallery.Cols[COL_TEMPLATECATNAME].Width = 0;
            c1TemplateGallery.Cols[COL_TEMPLATEPROVIDERID].Width = 0;
            c1TemplateGallery.Cols[COL_TEMPLATEPROVIDERNAME].Width = (int)(c1Width * 0.5);


        }
        #endregion  " C1 Constants "

        #region " Form Load "

        private void frmViewTemplateGallery_Load(object sender, EventArgs e)
        {
            //DesignTemplateGrid();
           // RefreshTemplates(null, null);
            Fill_categories();
            FillControls();

            //Select at least one Cat - start.
            if (trvCategory.Nodes.Count > 0)
            {
                trvCategory.SelectedNode = trvCategory.Nodes[0];
            }

            if (trvCategory.SelectedNode != null)
            {
                FillTemplatesofCat(trvCategory.SelectedNode.Text.ToString());
                DesignTemplateGrid();
            }
            //Select at least one Cat - finish.

            //FillCatTemplates(tsbCatTemplates);
        }

        #endregion " Form Load "

        #region " Form Fills "

        private void FillCatTemplates(ToolStripDropDownButton tsbCats)
        {
            gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            DataTable dtCategories = new DataTable();
            try
            {
                //dtCategories = ogloTemplate.GetList("Template");
                dtCategories = ogloTemplate.GetTemplateCategoryList();
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCategories.Rows.Count; i++)
                    {
                        ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();
                        //oCatMenuItem.Text = dtCategories.Rows[i]["sDescription"].ToString();
                        oCatMenuItem.Text = dtCategories.Rows[i]["CategoryName"].ToString();

                        //Template - Start
                        // gloTemplate oTemplates = new gloTemplate(_databaseconnectionstring);
                        DataTable dtTemplates = new DataTable();

                        //dtTemplates = ogloTemplate.GetTemplates(Convert.ToInt64(dtCategories.Rows[i]["nCategoryID"].ToString()));
                        dtTemplates = ogloTemplate.GetTemplates(dtCategories.Rows[i]["CategoryName"].ToString());
                        if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtTemplates.Rows.Count; j++)
                            {
                                oCatMenuItem.DropDownItems.Add(dtTemplates.Rows[j]["sTemplateName"].ToString());
                            }
                        }
                        ogloTemplate.Dispose();
                        dtTemplates.Dispose();

                        //Template - Finish
                        tsbCats.DropDownItems.Add(oCatMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Fill_categories()
        {
            gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            DataTable dt = new DataTable();
            //dt = ogloTemplate.GetList("Template");
            dt = ogloTemplate.GetTemplateCategoryList();
            //nCategoryID, sDescription
            if (dt != null)
            {
                int i;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode oNode = new TreeNode();
                    if (dt.Rows[i]["CategoryName"].ToString().Trim() != "MIS Reports")
                    {
                        oNode.Text = dt.Rows[i]["CategoryName"].ToString();
                        //oNode.Tag = dt.Rows[i]["nCategoryID"];
                        trvCategory.Nodes.Add(oNode);
                    }
                                        
                }
            }

        }

        private void FillControls()
        {
            FillProviders();
        }

        private void FillProviders()
        {
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable _dtProviders = new DataTable();
            try
            {
                _dtProviders = oResource.GetProviders();

                DataTable dtProviders;
                if (_dtProviders != null && _dtProviders.Rows.Count > 0)
                {
                    dtProviders = new DataTable();
                    dtProviders.Columns.Add("nProviderID");
                    dtProviders.Columns.Add("ProviderName");

                    dtProviders.Clear();
                    dtProviders.Rows.Add(0, "All Providers");

                    for (int i = 0; i < _dtProviders.Rows.Count; i++)
                    {
                        dtProviders.Rows.Add(_dtProviders.Rows[i]["nProviderID"], _dtProviders.Rows[i]["ProviderName"]);
                    }

                    cmbProviderSearch.DataSource = dtProviders;
                    cmbProviderSearch.DisplayMember = "ProviderName";
                    cmbProviderSearch.ValueMember = "nProviderID";

                    if (_ProviderID != 0)
                    {
                        cmbProviderSearch.SelectedValue = _ProviderID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oResource != null)
                {
                    oResource.Dispose();
                }

                if (_dtProviders != null)
                {
                    _dtProviders.Dispose();
                }

            }


        }

        #endregion " Form Fills "

        #region " Tool Strip Events "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                frmSetupTemplateGallery ofrmSetupTemplateGallery;
                if (e.ClickedItem.Tag != null)
                {
                    switch (e.ClickedItem.Tag.ToString().Trim())
                    {
                        //New
                        case "New":
                            ofrmSetupTemplateGallery = new frmSetupTemplateGallery(_databaseconnectionstring);
                            ofrmSetupTemplateGallery.MdiParent = this.MdiParent;
                            ofrmSetupTemplateGallery.WindowState = FormWindowState.Maximized;
                            ofrmSetupTemplateGallery.FormClosed += RefreshTemplates;
                            ofrmSetupTemplateGallery.Show();
                            //if (trvCategory.SelectedNode != null && Convert.ToInt64(trvCategory.SelectedNode.Tag) > 0)
                            //{
                            //    FillTemplatesofCat(Convert.ToInt64(trvCategory.SelectedNode.Tag));
                            //}

                            break;

                        //Modify
                        case "Modify":

                            ModifyTemplate();
                            //if (trvCategory.SelectedNode != null && Convert.ToInt64(trvCategory.SelectedNode.Tag) > 0)
                            //{
                            //    FillTemplatesofCat(Convert.ToInt64(trvCategory.SelectedNode.Tag));
                            //}

                            break;

                        //Refresh
                        case "Refresh":
                            RefreshTemplates(null,null);
                            txtTemplateSearch.Text = "";
                            break;

                        //delete
                        case "Delete":

                            if (c1TemplateGallery.RowSel < c1TemplateGallery.Rows.Count)
                            {
                                if (c1TemplateGallery.Rows.Selected != null)
                                {
                                    if (c1TemplateGallery.RowSel > 0)
                                    {
                                        _templateID = Convert.ToInt64(c1TemplateGallery.GetData(c1TemplateGallery.RowSel, COL_TEMPLATEID));
                                        if (_templateID > 0)
                                        {
                                            if (MessageBox.Show("Are you sure you want to delete this template?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {

                                                string TemplateName = Convert.ToString(c1TemplateGallery.GetData(c1TemplateGallery.RowSel, COL_TEMPLATENAME));

                                                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
                                                ogloTemplate.DeleteTemplate(_templateID);
                                                ogloTemplate = null;
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, "Template (" + TemplateName.Trim() + ") Deleted", 0,_templateID,  0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                                                RefreshTemplates(null,null);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        //close
                        case "Close":

                            this.Close();

                            break;

                        //default
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion "Tool Strip Events"

        #region " Controls Events "

        private void trvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (trvCategory.SelectedNode != null)
                {
                    txtTemplateSearch.Text = "";
                    FillTemplatesofCat(trvCategory.SelectedNode.Text.ToString());
                }
                //DesignTemplateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void c1TemplateGallery_DoubleClick(object sender, EventArgs e)
        {
            //ModifyTemplate();
            //if (c1TemplateGallery.RowSel < c1TemplateGallery.Rows.Count)
            //{
            //    if (c1TemplateGallery.Rows.Selected != null)
            //    {
            //        if (c1TemplateGallery.RowSel > 0)
            //        {
            //            _templateID = Convert.ToInt64(c1TemplateGallery.GetData(c1TemplateGallery.RowSel, COL_TEMPLATEID));
            //            frmSetupTemplateGallery ofrmSetupTemplateGallery = new frmSetupTemplateGallery(_databaseconnectionstring, _templateID);
            //            //ofrmSetupTemplateGallery.onSetupTemplateSave_Click +=new frmSetupTemplateGallery.onSetupTemplateSave(ofrmSetupTemplateGallery_onSetupTemplateSave_Click); 
            //            ofrmSetupTemplateGallery.MdiParent = this.MdiParent;
            //            ofrmSetupTemplateGallery.WindowState = FormWindowState.Maximized;
            //            ofrmSetupTemplateGallery.Show();

            //            if (trvCategory.SelectedNode != null)
            //            {
            //                FillTemplatesofCat(trvCategory.SelectedNode.Text.ToString());
            //            }
            //        }
            //    }
            //}
        }

        //private void ofrmSetupTemplateGallery_onSetupTemplateSave_Click(object sender, EventArgs e)
        //{

        //}

        #endregion " Controls Events "

        #region " Supporting Methods "

        private void ModifyTemplate()
        {
            if (c1TemplateGallery.RowSel < c1TemplateGallery.Rows.Count)
            {
                if (c1TemplateGallery.Rows.Selected != null)
                {
                    if (c1TemplateGallery.RowSel > 0)
                    {
                        _templateID = Convert.ToInt64(c1TemplateGallery.GetData(c1TemplateGallery.RowSel, COL_TEMPLATEID));
                        if (_templateID > 0)
                        {
                            frmSetupTemplateGallery ofrmSetupTemplateGallery = new frmSetupTemplateGallery(_databaseconnectionstring, _templateID);
                            ofrmSetupTemplateGallery.MdiParent = this.MdiParent;
                            ofrmSetupTemplateGallery.WindowState = FormWindowState.Maximized;
                            ofrmSetupTemplateGallery.FormClosed += RefreshTemplates;
                            ofrmSetupTemplateGallery.Show();
                        }
                    }
                }
            }

        }

        private void RefreshTemplates(object sender, FormClosedEventArgs e)
        {
            frmSetupTemplateGallery  ofrmSetupTemplateGallery = null; 
            try
            {
                ofrmSetupTemplateGallery = (frmSetupTemplateGallery) sender;
            }
            catch
            {
            }
            try
            {
                ofrmSetupTemplateGallery.FormClosed -= RefreshTemplates;
            }
            catch
            {
            }

            try
            {
                if (ofrmSetupTemplateGallery != null)
                {
                    ofrmSetupTemplateGallery.Close();
                }
                if (ofrmSetupTemplateGallery != null)
                {
                    ofrmSetupTemplateGallery.Dispose();
                    ofrmSetupTemplateGallery = null;
                }

            }
            catch
            {
            }
            //Select at least one Category - start.
            try
            {
                if (trvCategory.SelectedNode == null && trvCategory.Nodes.Count > 0)
                {
                    trvCategory.SelectedNode = trvCategory.Nodes[0];
                }

                if (trvCategory.SelectedNode != null)
                {
                    
                    if (c1TemplateGallery.DataSource != null)
                    {
                        //TODO:
                    }

                    FillTemplatesofCat(trvCategory.SelectedNode.Text.ToString());
                    DesignTemplateGrid();
                }

            }
            catch (Exception)
            {
            }
        }

        private void FillTemplatesofCat(String CategoryName)
        {
            DataView dtv;
            gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            DataTable dtTemplates;
            try
            {
                //Retrieve and fill all templates for a given Category,
                //c1TemplateGallery.Dispose();
                //c1TemplateGallery = new C1.Win.C1FlexGrid.C1FlexGrid();
                //c1TemplateGallery.Clear();
                //if (c1TemplateGallery == null)
                //{
                //    return; 
                //}

                

                //if (c1TemplateGallery.DataSource != null)
                //{
              //  c1TemplateGallery.Clear();
                    c1TemplateGallery.DataSource = null;
                //}

                dtTemplates = ogloTemplate.GetTemplates(CategoryName);                

                #region "//Commented for Datasource"
                //Commented for Datasource
                //if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtTemplates.Rows.Count; i++)
                //    {
                //        c1TemplateGallery.Rows.Add();

                //        //COL_TEMPLATEID ,  COL_TEMPLATENAME, COL_TEMPLATECATEID, 
                //        //COL_TEMPLATEPROVIDERID , COL_TEMPLATEPROVIDERNAME, 
                //        c1TemplateGallery.SetData(i + 1, COL_TEMPLATEID, dtTemplates.Rows[i]["nTemplateID"]);
                //        c1TemplateGallery.SetData(i + 1, COL_TEMPLATENAME, dtTemplates.Rows[i]["sTemplatName"]);
                //        c1TemplateGallery.SetData(i + 1, COL_TEMPLATECATID, dtTemplates.Rows[i]["nCategoryID"]);
                //        c1TemplateGallery.SetData(i + 1, COL_TEMPLATEPROVIDERID, dtTemplates.Rows[i]["nProviderID"]);
                //        c1TemplateGallery.SetData(i + 1, COL_TEMPLATEPROVIDERNAME, dtTemplates.Rows[i]["sProviderName"]);   
                //    }
                //}
                //**Commented for Datasource
                #endregion "//Commented for Datasource"

                dtv = new DataView(dtTemplates);

                if (Convert.ToInt64(cmbProviderSearch.SelectedValue) > 0)
                {
                    dtv.RowFilter = " sProviderName LIKE '" + cmbProviderSearch.Text.Replace("'","''").ToString() + "%'";
                }

                if (dtv != null)
                {
                    //if (c1TemplateGallery.DataSource != null)
                    //{
                        c1TemplateGallery.DataSource = dtv;
                        DesignTemplateGrid();
                    //}
                }

                //Dispose calls.               
                ogloTemplate.Dispose();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                MessageBox.Show(dbex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                //Dispose calls.
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                }
            }
        }

        #endregion " Supporting Methods "

        private void cmbProviderSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (trvCategory.SelectedNode != null)
                {
                    FillTemplatesofCat(trvCategory.SelectedNode.Text.ToString());
                }
                //DesignTemplateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_GetData_Click(object sender, EventArgs e)
        {
            //gloBilling.frmWd_PatientTemplate ofrm = new gloBilling.frmWd_PatientTemplate(_databaseconnectionstring );
            //ofrm.Show();

        }

        private void txtTemplateSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
            DataView _dv;
            string strSearch = "";
            try
            {
                if (txtTemplateSearch.Text != "")
                {
                    strSearch = txtTemplateSearch.Text.Replace("'", "''");
                    strSearch = strSearch.Replace("[", "") + "";
                }
                else
                {
                    strSearch = "";
                }

                dt = ogloTemplate.GetAllTemplates();
                _dv = dt.DefaultView;

                if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                {
                    _dv.RowFilter = _dv.Table.Columns["sTemplateName"].ColumnName + " Like '%" + strSearch + "%' and CategoryName='"+trvCategory.SelectedNode.Text+"'";
                }
                else
                {
                    _dv.RowFilter = _dv.Table.Columns["sTemplateName"].ColumnName + " Like '" + strSearch + "%' and CategoryName='" + trvCategory.SelectedNode.Text + "'";
                }
                c1TemplateGallery.DataSource = _dv;
                DesignTemplateGrid();
            }

            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
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

        ~frmViewTemplateGallery()
        {
            Dispose(false);
        }

        

        #endregion

        private void frmViewTemplateGallery_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void c1TemplateGallery_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           //Code Added by Mayuri:20091203
            //To fix issue:444-To Auto Refresh grid after modifying record
            Point ptPoint = new Point(e.X, e.Y);
            C1 .Win .C1FlexGrid .HitTestInfo htInfo= c1TemplateGallery.HitTest(ptPoint);
            if (htInfo.Type == C1.Win.C1FlexGrid.HitTestTypeEnum.Cell )

            {
               
                ModifyTemplate();
            }

            // Code added to clear the reset search text 
            txtTemplateSearch.TextChanged -= new EventHandler(txtTemplateSearch_TextChanged);
            txtTemplateSearch.Text = string.Empty;
            txtTemplateSearch.TextChanged += new EventHandler(txtTemplateSearch_TextChanged);
        }

        private void frmViewTemplateGallery_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseButton_Click != null)
            {
                CloseButton_Click(null, null);
            }
        }
        //End code Added by Mayuri:20091203



    }
}
