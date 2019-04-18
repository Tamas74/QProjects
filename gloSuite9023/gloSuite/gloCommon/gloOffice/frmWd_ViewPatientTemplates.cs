using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloOffice
{
    public partial class frmWd_ViewPatientTemplates : Form
    {


        #region " Variable Declarations"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _PatientID = 0;
        int _rowselect = 0;
        private Int64 _nTrasactionID = 0 ;
        private Int64 _ClinicID = 0;

        private const int COL_nTransactionID = 0;
        private const int COL_nFromDate = 1;
        private const int COL_nToDate = 2;
        private const int COL_nCategoryID = 3;
        private const int COL_sCategoryName = 4;
        private const int COL_nTemplateID = 5;
        private const int COL_sTemplateName = 6;
        private const int COL_nProviderID = 7;
        private const int COL_nCount = 8;
        private const int COL_sPatFname = 9;
        private const int COL_sPatMname = 10;
        private const int COL_sPatLname = 11;
        private const int COL_nPatientID = 12;
        
        private const int COL_COLCOUNT = 13;

        private static frmWd_ViewPatientTemplates frm;
        private bool blnDisposed;

        public Tuple<Int64, string, string> SyncPatientId
        {
            get { return new Tuple<Int64, string, string>(_PatientID, oPatientControl.PatientCode, oPatientControl.PatientName); }
        }
        #endregion


        #region " Constructors"

        public frmWd_ViewPatientTemplates(Int64 PatientID , String  DataBaseConnectionString )
        {
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";;
                }
            }
            else
            { _messageboxcaption = "gloPM";; }

            #endregion

            _databaseconnectionstring = DataBaseConnectionString;
            _PatientID = PatientID;
            InitializeComponent();

           
        }

      

        public static frmWd_ViewPatientTemplates GetInstance(Int64 PatientID, String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ViewPatientTemplates(PatientID, DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #endregion


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

        ~frmWd_ViewPatientTemplates()
        {
            Dispose(false);
        }

        private void frmWd_ViewPatientTemplates_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmWd_ViewPatientTemplates_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1PatientTemplates, false);

            try
            {
                DesignGrid();
                //Fill_PatientTemplates(0);
                FillCatTemplates(tsb_CategoryTemplates);

                LoadPatientStrip(_PatientID, 0, true);


                //Added By Pramod Nair For MIS Reports Batch
                TreeNode oNode;

                oNode = new TreeNode();
                oNode.Text = "Patient Forms";
                oNode.Tag = "Patient Forms";
                oNode.ImageIndex = 3;
                oNode.SelectedImageIndex = 3;

                trv_viewTemplates.Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "MIS Reports";
                oNode.Tag = "MIS Reports";
                oNode.ImageIndex = 4;
                oNode.SelectedImageIndex = 4;
                trv_viewTemplates.Nodes.Add(oNode);

                if (trv_viewTemplates.Nodes.Count > 0) { trv_viewTemplates.SelectedNode = trv_viewTemplates.Nodes[0]; }
                pnl_Treeview.Visible = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void frm_Form_Closed(object sender, ToolStripItemClickedEventArgs e)
        {
            gloOffice.frmWd_PatientTemplate frm = null;
            try
            {
                frm = (gloOffice.frmWd_PatientTemplate)sender;
            }
            catch
            {
            }
            try
            {
                frm.Form_Closed -= new frmWd_PatientTemplate.FormClosed(frm_Form_Closed);
            }
            catch
            {
            }
            try
            {
                if (frm != null)
                {
                    frm.Close();
                }
                if (frm != null)
                {
                    frm.Dispose();
                    frm = null;
                }
            }
            catch
            {
            }
            Fill_PatientTemplates(0);
            c1PatientTemplates.Row = _rowselect;
        }

        #endregion


        #region " Fill Methods"


            #region "Fill Child Nodes Of Tree -- Added By Pramod Nair For MIS Reports Batch"

                private void FillSubTree()
                {

                    try
                    {
                        this.trv_viewTemplates.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trv_viewTemplates_AfterSelect);

                        TreeNode oNode = new TreeNode();
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        DataTable dtBatch = new DataTable();
                        oDB.Connect(false);
                        string strSQL = "";
                        strSQL = "select distinct(nFromDate) from PatientTemplates where sCategoryName='MIS Reports' and nTemplateID=0";
                        oDB.Retrive_Query(strSQL, out dtBatch);
                        oDB.Disconnect();
                        oDB.Dispose();


                        bool isnodePresent = false;

                        if (dtBatch != null)
                        {

                            for (int i = 0; i < dtBatch.Rows.Count; i++)
                            {
                                oNode = new TreeNode();
                                oNode.Text = "Batch - " + Convert.ToString(dtBatch.Rows[i]["nFromDate"]);
                                oNode.Tag = "Batch -" + "~" + dtBatch.Rows[i]["nFromDate"];
                                oNode.ImageIndex = 1;
                                oNode.SelectedImageIndex = 1;

                                foreach (TreeNode childNode in trv_viewTemplates.SelectedNode.Nodes)
                                {
                                    if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                                    {
                                        isnodePresent = true;
                                        break;
                                    }
                                }
                                if (isnodePresent == false)
                                {
                                    trv_viewTemplates.SelectedNode.Nodes.Add(oNode);
                                }

                            }

                            trv_viewTemplates.ExpandAll();

                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                    }
                    finally
                    {
                        this.trv_viewTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_viewTemplates_AfterSelect);
                    }


                }

                #endregion

            private void Fill_PatientTemplates(int mode)
            {
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);

                //Commented By Pramod Nair
                //string sqlQuery = "SELECT  ISNULL(nTransactionID,0)AS nTransactionID, nFromDate, dbo.convert_to_date(nToDate) as nToDate,ISNULL(nCategoryID,0) AS nCategoryID, ISNULL(sCategoryName,'') AS sCategoryName, ISNULL(nTemplateID,0) AS nTemplateID, ISNULL(sTemplateName,'') AS sTemplateName,ISNULL(nProviderID,0) AS nProviderID, ISNULL(nCount,0) AS nCount FROM PatientTemplates " +
                //                 " WHERE  nPatientID = " + _PatientID + " ORDER  BY nTransactionID ";


                //Added By Pramod Nair
                string sqlQuery = "";
                sqlQuery = "SELECT ISNULL(PatientTemplates.nTransactionID, 0) AS nTransactionID,PatientTemplates.nFromDate,CONVERT(DATETIME,dbo.CONVERT_TO_DATE(PatientTemplates.nToDate))" +
                                    " AS nToDate,ISNULL(PatientTemplates.nCategoryID, 0) AS nCategoryID,ISNULL(PatientTemplates.sCategoryName, '') AS sCategoryName, " +
                                    " ISNULL(PatientTemplates.nTemplateID, 0) AS nTemplateID,ISNULL(PatientTemplates.sTemplateName, '') AS sTemplateName, " +
                                    " ISNULL(PatientTemplates.nProviderID, 0) AS nProviderID,ISNULL(PatientTemplates.nCount, 0) AS nCount, " +
                                    " isnull(Patient.sFirstName,'-') as sFirstName,isnull(Patient.sMiddleName,'-') as sMiddleName,isnull(Patient.sLastName,'-') as sLastName,PatientTemplates.nPatientID " +
                                    " FROM PatientTemplates INNER JOIN Patient ON PatientTemplates.nPatientID = Patient.nPatientID";

                if (mode == 1)
                {
                    sqlQuery = sqlQuery + " WHERE(PatientTemplates.nFromDate= " + GetTagElement(trv_viewTemplates.SelectedNode.Tag.ToString(), '~', 2) + ") AND (sCategoryName = 'MIS Reports') "; //ORDER BY nTransactionID";

                    c1PatientTemplates.Cols[COL_sPatFname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatMname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatLname].Visible = true;
                    if (oPatientControl != null)
                        oPatientControl.Visible = false;
                }
                else if (mode == 0)
                {
                    sqlQuery = sqlQuery + " WHERE(PatientTemplates.nPatientID = " + _PatientID + ") AND (PatientTemplates.sCategoryName <> 'MIS Reports') "; // ORDER BY nTransactionID";

               


                    //Added By Pramod Nair
                    c1PatientTemplates.Cols[COL_sPatFname].Visible = false;
                    c1PatientTemplates.Cols[COL_sPatMname].Visible = false;
                    c1PatientTemplates.Cols[COL_sPatLname].Visible = false;

                    if (oPatientControl != null)
                        oPatientControl.Visible = true;
                }
                else
                {

                    sqlQuery = sqlQuery + " WHERE(PatientTemplates.nPatientID = 0) AND(sCategoryName = 'MIS Reports') ";//ORDER BY nTransactionID";

                    c1PatientTemplates.Cols[COL_sPatFname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatMname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatLname].Visible = true;
                    if (oPatientControl != null)
                        oPatientControl.Visible = false;

                }
                //Sorting added  against bugid 47922
                sqlQuery +="   ORDER BY PatientTemplates.nToDate Desc";
 

                DataTable dt = new DataTable();
                try
                {
                    ODB.Retrive_Query(sqlQuery, out dt);
                    ODB.Disconnect();
                    c1PatientTemplates.Rows.Count = 1;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        _rowselect = dt.Rows.Count;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            c1PatientTemplates.Rows.Add();
                            int rowIndex = c1PatientTemplates.Rows.Count - 1;
                            c1PatientTemplates.SetData(rowIndex, COL_nCount, Convert.ToString(dt.Rows[i]["nCount"]));
                            c1PatientTemplates.SetData(rowIndex, COL_nProviderID, Convert.ToString(dt.Rows[i]["nProviderID"]));
                            c1PatientTemplates.SetData(rowIndex, COL_nCategoryID, Convert.ToString(dt.Rows[i]["nCategoryID"]));
                            c1PatientTemplates.SetData(rowIndex, COL_sCategoryName, Convert.ToString(dt.Rows[i]["sCategoryName"]));
                            c1PatientTemplates.SetData(rowIndex, COL_nTemplateID, Convert.ToString(dt.Rows[i]["nTemplateID"]));
                            c1PatientTemplates.SetData(rowIndex, COL_sTemplateName, Convert.ToString(dt.Rows[i]["sTemplateName"]));
                            //c1PatientTemplates.SetData(rowIndex, COL_nToDate,gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nToDate"])));
                            c1PatientTemplates.SetData(rowIndex, COL_nToDate, dt.Rows[i]["nToDate"]);
                            c1PatientTemplates.SetData(rowIndex, COL_nFromDate, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nFromDate"])));
                            c1PatientTemplates.SetData(rowIndex, COL_nTransactionID, Convert.ToString(dt.Rows[i]["nTransactionID"]));

                            //Added By Pramod Nair
                            c1PatientTemplates.SetData(rowIndex, COL_sPatFname, Convert.ToString(dt.Rows[i]["sFirstName"]));
                            c1PatientTemplates.SetData(rowIndex, COL_sPatMname, Convert.ToString(dt.Rows[i]["sMiddleName"]));
                            c1PatientTemplates.SetData(rowIndex, COL_sPatLname, Convert.ToString(dt.Rows[i]["sLastName"]));
                            c1PatientTemplates.SetData(rowIndex, COL_nPatientID, Convert.ToString(dt.Rows[i]["nPatientID"]));

                        }
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (ODB != null) { ODB.Dispose(); }
                    if (dt != null) { dt.Dispose(); }
                }

            }

            private void Fill_PatientTemplates(String sFilter, int mode)
            {
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);

                //string sqlQuery = "SELECT  ISNULL(nTransactionID,0)AS nTransactionID, nFromDate, dbo.convert_to_date(nToDate) as nToDate,ISNULL(nCategoryID,0) AS nCategoryID, ISNULL(sCategoryName,'') AS sCategoryName, ISNULL(nTemplateID,0) AS nTemplateID, ISNULL(sTemplateName,'') AS sTemplateName,ISNULL(nProviderID,0) AS nProviderID, ISNULL(nCount,0) AS nCount FROM PatientTemplates " +
                //                 " WHERE  nPatientID = " + _PatientID + " " + sFilter + " ORDER  BY nTransactionID ";


                //Added By Pramod Nair
                string sqlQuery = "";
                Int64 BatchID = 0;
                if (mode == 1)
                {

                    sqlQuery = "SELECT TOP 1 PatientTemplates.nFromDate FROM PatientTemplates INNER JOIN Patient ON PatientTemplates.nPatientID = Patient.nPatientID  ";
                }
                else
                {
                    sqlQuery = "SELECT ISNULL(PatientTemplates.nTransactionID, 0) AS nTransactionID,PatientTemplates.nFromDate,CONVERT(DATETIME,dbo.CONVERT_TO_DATE(PatientTemplates.nToDate)) " +
                              " AS nToDate,ISNULL(PatientTemplates.nCategoryID, 0) AS nCategoryID,ISNULL(PatientTemplates.sCategoryName, '') AS sCategoryName, " +
                              " ISNULL(PatientTemplates.nTemplateID, 0) AS nTemplateID,ISNULL(PatientTemplates.sTemplateName, '') AS sTemplateName, " +
                              " ISNULL(PatientTemplates.nProviderID, 0) AS nProviderID,ISNULL(PatientTemplates.nCount, 0) AS nCount, " +
                              " isnull(Patient.sFirstName,'-') as sFirstName,isnull(Patient.sMiddleName,'-') as sMiddleName,isnull(Patient.sLastName,'-') as sLastName,PatientTemplates.nPatientID " +
                              " FROM PatientTemplates INNER JOIN Patient ON PatientTemplates.nPatientID = Patient.nPatientID ";
                }

                if (mode == 1)
                {
                    sqlQuery = sqlQuery + " WHERE(sCategoryName = 'MIS Reports') " + sFilter + " ORDER BY nFromDate asc";

                    c1PatientTemplates.Cols[COL_sPatFname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatMname].Visible = true;
                    c1PatientTemplates.Cols[COL_sPatLname].Visible = true;

                    if (oPatientControl != null)
                        oPatientControl.Visible = false;
                }
                else if (mode == 0)
                {

                    sqlQuery = sqlQuery + " WHERE(PatientTemplates.nPatientID = " + _PatientID + ") " + sFilter + "and sCategoryName <> 'MIS Reports' "; //ORDER BY nTransactionID";

                    //Added By Pramod Nair
                    c1PatientTemplates.Cols[COL_sPatFname].Visible = false;
                    c1PatientTemplates.Cols[COL_sPatMname].Visible = false;
                    c1PatientTemplates.Cols[COL_sPatLname].Visible = false;

                    if (oPatientControl != null)
                        oPatientControl.Visible = true;
                }
                else
                {
                    sqlQuery = sqlQuery + " WHERE(PatientTemplates.nPatientID = 0) " + sFilter + ""; // ORDER BY nTransactionID";
                }

                if (mode != 1)
                {
                    sqlQuery += "   ORDER BY PatientTemplates.nToDate Desc";
                }
                DataTable dt = new DataTable();
                try
                {
                    if (mode == 1)
                    {
                        Object _result;
                        _result= ODB.ExecuteScalar_Query(sqlQuery);
                        if (_result != null && Convert.ToString(_result).Trim() != "")
                        { BatchID = Convert.ToInt64(_result); }
                    }
                    else
                    {
                        ODB.Retrive_Query(sqlQuery, out dt);
                        c1PatientTemplates.Rows.Count = 1;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            _rowselect = dt.Rows.Count;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                c1PatientTemplates.Rows.Add();
                                int rowIndex = c1PatientTemplates.Rows.Count - 1;
                                c1PatientTemplates.SetData(rowIndex, COL_nCount, Convert.ToString(dt.Rows[i]["nCount"]));
                                c1PatientTemplates.SetData(rowIndex, COL_nProviderID, Convert.ToString(dt.Rows[i]["nProviderID"]));
                                c1PatientTemplates.SetData(rowIndex, COL_nCategoryID, Convert.ToString(dt.Rows[i]["nCategoryID"]));
                                c1PatientTemplates.SetData(rowIndex, COL_sCategoryName, Convert.ToString(dt.Rows[i]["sCategoryName"]));
                                c1PatientTemplates.SetData(rowIndex, COL_nTemplateID, Convert.ToString(dt.Rows[i]["nTemplateID"]));
                                c1PatientTemplates.SetData(rowIndex, COL_sTemplateName, Convert.ToString(dt.Rows[i]["sTemplateName"]));
                                //c1PatientTemplates.SetData(rowIndex, COL_nToDate, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nToDate"])));
                                c1PatientTemplates.SetData(rowIndex, COL_nToDate, Convert.ToString(dt.Rows[i]["nToDate"]));
                                c1PatientTemplates.SetData(rowIndex, COL_nFromDate, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nFromDate"])));
                                c1PatientTemplates.SetData(rowIndex, COL_nTransactionID, Convert.ToString(dt.Rows[i]["nTransactionID"]));


                                //Added By Pramod Nair
                                c1PatientTemplates.SetData(rowIndex, COL_sPatFname, Convert.ToString(dt.Rows[i]["sFirstName"]));
                                c1PatientTemplates.SetData(rowIndex, COL_sPatMname, Convert.ToString(dt.Rows[i]["sMiddleName"]));
                                c1PatientTemplates.SetData(rowIndex, COL_sPatLname, Convert.ToString(dt.Rows[i]["sLastName"]));

                                c1PatientTemplates.SetData(rowIndex, COL_nPatientID, Convert.ToString(dt.Rows[i]["nPatientID"]));
                            }

                            BatchID = Convert.ToInt64(dt.Rows[0]["nFromDate"].ToString());

                        }

                        else
                        {
                            if (mode == 1)
                            {
                                BatchID = Convert.ToInt64(GetTagElement(trv_viewTemplates.SelectedNode.Tag.ToString(), '~', 2));
                            }
                        }
                    }
                    if (mode == 1)
                    {
                        try
                        {
                            if (BatchID > 0)
                            {
                                trv_viewTemplates.SelectedNode = null;
                                //this.trv_viewTemplates.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trv_viewTemplates_AfterSelect);

                                for (int _index = 0; _index < trv_viewTemplates.Nodes[1].Nodes.Count; _index++)
                                {
                                    if (Convert.ToInt32((GetTagElement(trv_viewTemplates.Nodes[1].Nodes[_index].Tag.ToString(), '~', 2))) == BatchID)
                                    {
                                        trv_viewTemplates.SelectedNode = trv_viewTemplates.Nodes[1].Nodes[_index];
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                           
                            //this.trv_viewTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_viewTemplates_AfterSelect);
                        }
                    }


                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    ODB.Disconnect();
                    if (ODB != null) { ODB.Dispose(); }
                    if (dt != null) { dt.Dispose(); }
                }

            }

            private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
            {
                string[] temp;
                if (TagContent.Contains(Delimeter.ToString()))
                {
                    temp = TagContent.Split(Delimeter);
                    return temp[Position - 1];
                }
                else
                {
                    return TagContent;
                }
            }

            private void FillCatTemplates(ToolStripDropDownButton tsbCats)
            {
                gloTemplate oTemplate = new gloTemplate(_databaseconnectionstring);
                DataTable dtCategories = new DataTable();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtTemplates = new DataTable();
               // Int64 CategoryID = 0;
                String CategoryName = "";
                try
                {
                    oDB.Connect(false);
                    //dtCategories = oTemplate.GetList("Template");
                    dtCategories = oTemplate.GetTemplateCategoryList();
                    if (dtCategories != null && dtCategories.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCategories.Rows.Count; i++)
                        {
                            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();
                            CategoryName = dtCategories.Rows[i]["CategoryName"].ToString();
                            oCatMenuItem.Text = CategoryName;
                            //oCatMenuItem.Tag = dtCategories.Rows[i]["nCategoryID"];
                            //CategoryID = Convert.ToInt64(dtCategories.Rows[i]["nCategoryID"]);
                            oCatMenuItem.Font = gloGlobal.clsgloFont.getFontFromExistingSource(oCatMenuItem.Font, FontStyle.Regular);
                            oCatMenuItem.Image = Img_Templates.Images[0];
                            oCatMenuItem.ImageScaling = ToolStripItemImageScaling.None;
                            //Template - Start
                            //gloTemplate oTemplates = new gloTemplate(_databaseconnectionstring);

                            // COMMENTED BY SUDHIR
                            //string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
                            // "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') + SPACE(1)  " +
                            // " + ISNULL(Provider_MST.sMiddleName, '') AS sProviderName " +
                            // " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
                            // " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                            // " WHERE  TemplateGallery_MST.nCategoryID = " + CategoryID + " ";

                            string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
                            "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') + SPACE(1)  " +
                            " + ISNULL(Provider_MST.sMiddleName, '') AS sProviderName " +
                            " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
                            " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                            " WHERE  TemplateGallery_MST.sCategoryName = '" + CategoryName.Replace("'", "''") + "' ";
                            oDB.Retrive_Query(_sqlQuery, out dtTemplates);

                            if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtTemplates.Rows.Count; j++)
                                {
                                    ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                                    oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                                    oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                                    oTemplateItem.Font = gloGlobal.clsgloFont.getFontFromExistingSource(oCatMenuItem.Font, FontStyle.Regular);
                                    oTemplateItem.Image = Img_Templates.Images[1];
                                    oTemplateItem.ImageScaling = ToolStripItemImageScaling.None;
                                    oCatMenuItem.DropDownItems.Add(oTemplateItem);

                                    oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                                }
                            }

                            if (dtTemplates != null) { dtTemplates.Dispose(); }
                            //Template - Finish
                            tsbCats.DropDownItems.Add(oCatMenuItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                }
            }

            private void ModifyPatientTemplate()
            {
                frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_databaseconnectionstring, _nTrasactionID);
                frm.MdiParent = this.ParentForm;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }

            private void Delete_PatientTemplates()
            {
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string sqlQuery = " DELETE  FROM  PatientTemplates  WHERE  nClinicID =" + _ClinicID + " AND nPatientID = " + _PatientID + " AND  nTransactionID = " + _nTrasactionID + " ";
                try
                {
                    ODB.Execute_Query(sqlQuery);
                    ODB.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (ODB != null) { ODB.Dispose(); }
                }

            }


        #endregion


        #region " C1 Flex Grid"

        private void DesignGrid()
    {
        c1PatientTemplates.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
        c1PatientTemplates.Cols.Count = COL_COLCOUNT;
        c1PatientTemplates.Rows.Count = 1;
        c1PatientTemplates.Rows.Fixed= 1;


        c1PatientTemplates.SetData(0, COL_nCount, "Count");
        c1PatientTemplates.SetData(0, COL_nFromDate, "From Date");
        c1PatientTemplates.SetData(0, COL_nProviderID, "ProviderID");
        c1PatientTemplates.SetData(0, COL_nTemplateID, "TemplateID");
        c1PatientTemplates.SetData(0, COL_sTemplateName, "Template");
        c1PatientTemplates.SetData(0, COL_nCategoryID, "CategoryID");
        c1PatientTemplates.SetData(0, COL_sCategoryName, "Category");
        c1PatientTemplates.SetData(0, COL_nToDate, "To Date");
        c1PatientTemplates.SetData(0, COL_nTransactionID, "TransactionID");

        //Added by Pramod Nair
        c1PatientTemplates.SetData(0, COL_sPatFname, "First Name");
        c1PatientTemplates.SetData(0, COL_sPatMname, "MI");
        c1PatientTemplates.SetData(0, COL_sPatLname, "Last Name");
        c1PatientTemplates.SetData(0, COL_nPatientID, "PatientID");


        c1PatientTemplates.Cols[COL_nCount].Visible = false;
        c1PatientTemplates.Cols[COL_nFromDate].Visible = false;
        c1PatientTemplates.Cols[COL_nProviderID].Visible = false;
        c1PatientTemplates.Cols[COL_nTemplateID].Visible = false;
        c1PatientTemplates.Cols[COL_nCategoryID].Visible = false;
        c1PatientTemplates.Cols[COL_nTransactionID].Visible = false;

        c1PatientTemplates.Cols[COL_nPatientID].Visible = false;

        c1PatientTemplates.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

        int width = pnl_View.Width -1;
        c1PatientTemplates.Cols[COL_nCount].Width = 0;
        c1PatientTemplates.Cols[COL_nFromDate].Width = 0;
        c1PatientTemplates.Cols[COL_nProviderID].Width = 0;
        c1PatientTemplates.Cols[COL_nTemplateID].Width = 0;
        //c1PatientTemplates.Cols[COL_sTemplateName].Width = (int)(width * 0.63);
        c1PatientTemplates.Cols[COL_sTemplateName].Width = 450;
        c1PatientTemplates.Cols[COL_nCategoryID].Width = 0;
        //c1PatientTemplates.Cols[COL_sCategoryName].Width = (int)(width * 0.33);
        //c1PatientTemplates.Cols[COL_nToDate].Width = (int)(width * 0.33);
        c1PatientTemplates.Cols[COL_sCategoryName].Width = 150;
        c1PatientTemplates.Cols[COL_nToDate].Width = 80;
        c1PatientTemplates.Cols[COL_nTransactionID].Width = 0;

        c1PatientTemplates.Cols[COL_sPatFname].Width = 100;
        c1PatientTemplates.Cols[COL_sPatMname].Width = 100;
        c1PatientTemplates.Cols[COL_sPatLname].Width = 100;



    }

        private void c1PatientTemplates_DoubleClick(object sender, EventArgs e)
        {
            if (c1PatientTemplates.Rows.Count > 1)
            {
                _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));
                ModifyPatientTemplate();
            }
        }

        private void c1PatientTemplates_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1PatientTemplates_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {

        }


        #endregion


        #region " Search"

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {

            string strSearch = txt_Search.Text.Trim();
            strSearch.Replace("'", "");

            c1PatientTemplates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            try
            {
                
                String sFilter = "";
                if (strSearch.Trim() != "")
                {
                    strSearch = strSearch.Replace("'", "''");
                    //sFilter = " and (sTemplateName like '%" + strSearch.Trim() + "%' OR sCategoryName like '%" + strSearch.Trim() + "%' OR dbo.convert_to_date(nToDate) like '%" + strSearch.Trim() + "%' OR sFirstName like '%" + strSearch.Trim() + "%' OR sMiddleName like '%" + strSearch.Trim() + "%' OR sLastName like '%" + strSearch.Trim() + "%')";
                    //Apply filter only to coloumns visible not on patient name.
                    sFilter = " and (sTemplateName like '%" + strSearch.Trim() + "%' OR sCategoryName like '%" + strSearch.Trim() + "%' OR dbo.convert_to_date(nToDate) like '%" + strSearch.Trim() + "%')";
                    if (trv_viewTemplates.SelectedNode != null)
                    {
                        if (trv_viewTemplates.SelectedNode.Text == "Patient Forms")
                        {
                            Fill_PatientTemplates(sFilter, 0);
                        }
                        else if (trv_viewTemplates.SelectedNode.Text == "MIS Reports")
                        {
                            Fill_PatientTemplates(sFilter, 2);
                        }
                        else
                        {
                            Fill_PatientTemplates(sFilter, 1);
                        }
                    }
                }
                else
                {
                 
                    //Added By Pramod Nair
                    if (trv_viewTemplates.SelectedNode.Text == "Patient Forms")
                    {
                        c1PatientTemplates.Visible = true;
                        Fill_PatientTemplates(0);
                    }
                    else if (trv_viewTemplates.SelectedNode.Text == "MIS Reports")
                    {
                        //c1PatientTemplates.Clear();
                        c1PatientTemplates.DataSource = null;
                        FillSubTree();
                    }
                    else
                    {
                        c1PatientTemplates.Visible = true;
                        Fill_PatientTemplates(1);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion


        #region " Other Events"

        private void cmnuTemplateItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                ToolStripMenuItem cmnuTemplateItem = new ToolStripMenuItem();
                cmnuTemplateItem = (ToolStripMenuItem)sender;
                                
                
                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
                ogloTemplate.CategoryID = Convert.ToInt64(cmnuTemplateItem.OwnerItem.Tag);
                ogloTemplate.CategoryName = cmnuTemplateItem.OwnerItem.Text;
                ogloTemplate.TemplateID  = Convert.ToInt64(cmnuTemplateItem.Tag);
                ogloTemplate.PrimeryID= Convert.ToInt64(cmnuTemplateItem.Tag);
                ogloTemplate.TemplateName  = cmnuTemplateItem.Text ;
                if (trv_viewTemplates.SelectedNode.Text == "Patient Forms" || trv_viewTemplates.SelectedNode.Text == "MIS Reports")
                {
                    ogloTemplate.PatientID = Convert.ToInt64(_PatientID);
                }
                else
                {
                    ogloTemplate.PatientID=Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.Row,12));
                }
                
                gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring,ogloTemplate );
                frm.Form_Closed += new frmWd_PatientTemplate.FormClosed(frm_Form_Closed);
                frm.Text = ogloTemplate.TemplateName;
                frm.MdiParent = this.MdiParent; 
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }
        }

        private void tls_Strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
            {
                if (c1PatientTemplates.Rows.Selected != null)
                {
                    if (c1PatientTemplates.RowSel > 0)
                    {
                        _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));   
                    }
                }
            }
                        
            switch (e.ClickedItem.Tag.ToString())
            {
                
                case "Modify":
                    if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
                    {
                        if (c1PatientTemplates.Rows.Selected != null)
                        {
                            if (c1PatientTemplates.RowSel > 0)
                            {
                                if (_nTrasactionID > 0)
                                {
                                    ModifyPatientTemplate();
                                }
                            }
                        }
                    }
                    break;
                case "Delete":
                  
                    if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
                    {
                        if (c1PatientTemplates.Rows.Selected != null)
                        {
                            if (c1PatientTemplates.RowSel > 0)
                            {
                                if (_nTrasactionID > 0)
                                {
                                    if (MessageBox.Show("Are you sure you want to delete this template?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        Delete_PatientTemplates();
                                        // Solving Bug# 47912 : The selection should remain on the same row when "No" is selected.
                                        Fill_PatientTemplates(0);
                                    }
                                }
                            }
                        }
                    }
                   // Fill_PatientTemplates(0);
                    break;
                case "Refresh":
                    Fill_PatientTemplates(0);
                    break;
                case "Close":
                    this.Close();
                    break;
            }

        }

        private void pnl_View_SizeChanged(object sender, EventArgs e)
        {
            c1PatientTemplates.Width = pnl_View.Width - 1;

        }

        #endregion


        #region " Patient Strip Control Events "

        void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (oPatientControl.CmbSelectedPatientID > 0)
            {
                _PatientID = oPatientControl.PatientID;
                Fill_PatientTemplates(0);
            }
        }

        void oPatientControl_ControlSize_Changed(object sender, EventArgs e)
        {

        }

        private void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {
            if (oPatientControl.CmbSelectedPatientID > 0)
            {
                _PatientID = oPatientControl.PatientID;
                Fill_PatientTemplates(0);
            }
        }

        //private void oPatientControl_OnAccountPatientChanged(object sender, EventArgs e)
        //{
        //    if (oPatientControl.CmbSelectedPatientID > 0)
        //    {
        //        _PatientID = oPatientControl.PatientID;
        //        Fill_PatientTemplates(0);
        //    }

        //}

        private void oPatientControl_OnPatientChanged(object sender, EventArgs e)
        {
            if (oPatientControl.CmbSelectedPatientID > 0)
            {
                _PatientID = oPatientControl.PatientID;
                Fill_PatientTemplates(0);
            }
        }

        private void oPatientControl_OnPatientModified(object sender, EventArgs e)
        {
            try
            {

                if (oPatientControl.PatientID > 0)
                {
                    _PatientID = oPatientControl.PatientID;
                    Fill_PatientTemplates(0);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }


       
        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            oPatientControl.FillDetails(PatientId, gloStripControl.FormName.NewCharges);
            _PatientID = oPatientControl.PatientID;
        }

        #endregion " Patient Strip Control Events "
        
              
        #region " Tree View Events"

        private void trv_viewTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trv_viewTemplates.SelectedNode.Text == "Patient Forms")
            {
                c1PatientTemplates.Visible = true;
                panel2.Visible = true;

                Fill_PatientTemplates(0);
            }
            else if (trv_viewTemplates.SelectedNode.Text == "MIS Reports")
            {

               
                Fill_PatientTemplates(2);

                panel2.Visible = true;
                if (oPatientControl != null)
                    oPatientControl.Visible = false;

                FillSubTree();
            }
           
            else
            {
                c1PatientTemplates.Visible = true;
                panel2.Visible = true;
                Fill_PatientTemplates(1);
            }

        }

        #endregion

   
     

    }
}