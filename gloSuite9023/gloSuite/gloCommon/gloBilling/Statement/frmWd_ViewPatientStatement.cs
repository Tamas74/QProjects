using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloOffice;

namespace gloBilling
{
    public partial class frmWd_ViewPatientStatement : Form
    {


        #region " Variable Declarations"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _PatientID = 0;
        private Int64 _PatientID_To_Delete = 0;
        private DataView _dv=new DataView ();
        private string[] strSearchArray;
        gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        int _rowselect = 0;
        private Int64 _nTrasactionID = 0 ;
        private Int64 _ClinicID = 0;
         //Code Added by Mayuri:20091209
        private const int COL_dtStatementDate = 0;
        private const int COL_dtCreateDate = 1;
        private const int COL_sBatchName = 2;        
        private const int COL_sUserName = 3;
        private const int COL_sFirstName = 4;
        private const int COL_sMiddleName = 5;
        private const int COL_sLastName = 6;
        private const int COL_nBatchPateintStatMstID = 7;      
        private const int COL_nTemplateTranID = 8;
        private const int COL_PatientID = 9;        
        //End code Added by Mayuri:20091209

        private const int COL_bIsVoid = 10;
        private const int COL_nMasterBatchId = 11;
        private const int COL_nMasterDetailId = 12;
        private const int COL_Colcount = 13;

        private static frmWd_ViewPatientStatement frm;
        private bool blnDisposed;

        
        #endregion


        #region " Constructors"

        public frmWd_ViewPatientStatement(Int64 PatientID , String  DataBaseConnectionString )
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

        public static frmWd_ViewPatientStatement GetInstance(Int64 PatientID, String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_ViewPatientStatement(PatientID, DataBaseConnectionString);
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
           
            if (!(this.blnDisposed))
            {
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                  
                }
              
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmWd_ViewPatientStatement()
        {
            Dispose(false);
        }

        private void frmWd_ViewPatientStatement_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmWd_ViewPatientStatement_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1PatientTemplates, false);

            try
            {               
                LoadPatientStrip(_PatientID, 0, true);
                Fill_PatientTemplate();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void frm_Form_Closed(object sender, ToolStripItemClickedEventArgs e)
        {
            Fill_PatientTemplate();
            c1PatientTemplates.Row = _rowselect;
        }

        #endregion


        #region " Fill Methods"


          
        //Code Added by Mayuri:20091209
        //To display data against particular patient
        private void Fill_PatientTemplate()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);

            DataTable dt = new DataTable();
            try
            {

               
                string sqlQuery = "";
                //
                sqlQuery = "SELECT Convert(varchar,BL_Batch_PatientStatement_Mst.dtStatementDate,101) As dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
                          "ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName, ISNULL(Patient.sFirstName,'') as sFirstName, ISNULL(Patient.sMiddleName,'') as sMiddleName, ISNULL(Patient.sLastName,'') as sLastName, " +
                          "ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID, ISNULL(Patient.nPatientID,0) as nPatientID,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID     FROM Patient WITH (NOLOCK) INNER JOIN " +
                          "BL_Batch_PatientStatement_DTL WITH (NOLOCK) ON Patient.nPatientID = BL_Batch_PatientStatement_DTL.nPatientID INNER JOIN " +
                          " BL_Batch_PatientStatement_Mst WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where Patient.nPatientID=" + _PatientID + " order by dtCreateDate desc";
                ODB.Retrive_Query(sqlQuery, out dt);
                ODB.Disconnect();
                _dv = dt.DefaultView;
                c1PatientTemplates.DataSource = _dv;
                DesignGrid();
             
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
        //End code Added by Mayuri:20001209
         

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

                        
                            string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
                            "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') + SPACE(1)  " +
                            " + ISNULL(Provider_MST.sMiddleName, '') AS sProviderName " +
                            " FROM  TemplateGallery_MST WITH (NOLOCK) LEFT OUTER JOIN " +
                            " Provider_MST WITH (NOLOCK) ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
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
            //Code Added by Mayuri:20091209
            //Open Template to view purpose only
            private void ViewPatientTemplate()
            {
                frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_databaseconnectionstring, _nTrasactionID);
                frm.MdiParent = this.ParentForm;
                frm.IsView = true;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }
            //End code Added by Mayuri:20091209
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
                string sqlQuery = " DELETE  FROM  PatientTemplates  WHERE  nClinicID =" + _ClinicID + " AND nPatientID = " + _PatientID_To_Delete + " AND  nTransactionID = " + _nTrasactionID + " ";
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
            c1PatientTemplates.SetData(0, COL_sBatchName, "Batch Name");
            c1PatientTemplates.SetData(0, COL_dtCreateDate, "Create Date");
            c1PatientTemplates.SetData(0, COL_dtStatementDate, "Statement Date");
            c1PatientTemplates.SetData(0, COL_sUserName, "User Name");
            c1PatientTemplates.SetData(0, COL_sFirstName, "First Name");
            c1PatientTemplates.SetData(0, COL_sMiddleName, "Middle Name");
            c1PatientTemplates.SetData(0, COL_sLastName, "Last Name");
            c1PatientTemplates.SetData(0, COL_nBatchPateintStatMstID, "Batch ID");
            c1PatientTemplates.SetData(0, COL_PatientID , "Patient ID");
            c1PatientTemplates.SetData(0, COL_nTemplateTranID, "Template Transaction ID");
            c1PatientTemplates.Cols[COL_bIsVoid].Caption = "Status";
            c1PatientTemplates.Cols[COL_sBatchName].Visible = true;
            c1PatientTemplates.Cols[COL_dtCreateDate].Visible = true;
            c1PatientTemplates.Cols[COL_dtStatementDate].Visible = true;
            c1PatientTemplates.Cols[COL_sUserName].Visible = true;
            c1PatientTemplates.Cols[COL_sFirstName].Visible = false;
            c1PatientTemplates.Cols[COL_sMiddleName].Visible = false;
            c1PatientTemplates.Cols[COL_sLastName].Visible = false;
            c1PatientTemplates.Cols[COL_nBatchPateintStatMstID].Visible = false;
            c1PatientTemplates.Cols[COL_PatientID].Visible = false;
            c1PatientTemplates.Cols[COL_nTemplateTranID].Visible = false;
            c1PatientTemplates.Cols[COL_nMasterBatchId].Visible = false;
            c1PatientTemplates.Cols[COL_nMasterDetailId].Visible = false;


            int width = pnl_View.Width - 1;
            c1PatientTemplates.Cols[COL_nBatchPateintStatMstID].Width = 0;

            c1PatientTemplates.Cols[COL_sBatchName].Width = 200;

            c1PatientTemplates.Cols[COL_dtCreateDate].Width = 100;
            c1PatientTemplates.Cols[COL_dtStatementDate].Width = 150;
            c1PatientTemplates.Cols[COL_sUserName].Width = 150;

            c1PatientTemplates.Cols[COL_sFirstName].Width = 150;
            c1PatientTemplates.Cols[COL_sMiddleName].Width = 150;
            c1PatientTemplates.Cols[COL_sLastName].Width = 150;
            c1PatientTemplates.Cols[COL_PatientID].Width = 0;
            c1PatientTemplates.Cols[COL_nTemplateTranID].Width = 150;
           
            //End code Added by Mayuri:20091209


        }

            private void c1PatientTemplates_DoubleClick(object sender, EventArgs e)
            {
                if (c1PatientTemplates.Rows.Count > 1)
                {
                    _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTemplateTranID));
                    //Code Added by Mayuri:20091210
                    ViewPatientTemplate();                   
                    //End code Added by Mayuri:20091210
                }
            }

            private void c1PatientTemplates_MouseMove(object sender, MouseEventArgs e)
            {
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }


        #endregion


        #region " Search"

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFilter = "";
                c1PatientTemplates.DataSource = _dv;
                
                string strSearch = txt_Search.Text.Trim();
                //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");

                //Added By Mukesh Patel For implement Instring Search 20090720
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                
                if (strSearch.Trim() != "")
                {
                    strSearchArray = strSearch.Split(',');
                }
                //
                if (strSearch.Trim() != "")
                {
                    if (strSearchArray.Length == 1)
                    {
                        //For Single value search 
                        strSearch = strSearchArray[0].Trim();
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                        _dv.RowFilter = _dv.Table.Columns["sBatchName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        _dv.Table.Columns["dtCreateDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                         _dv.Table.Columns["dtStatementDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                         _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%' " ;                                         
                                  

                    }
                    else
                    {
                        //For Comma separated  value search
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            strSearch = strSearchArray[i].Trim();
                            if (strSearch.Length > 1)
                            {
                                string str = strSearch.Substring(1).Replace("%", "");
                                strSearch = strSearch.Substring(0, 1) + str;
                            }
                            if (strSearch.Trim() != "")
                            {


                                if (sFilter == "")//(i == 0)
                                {
                                    sFilter = " ( " + _dv.Table.Columns["sBatchName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["dtCreateDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                  _dv.Table.Columns["dtStatementDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                  _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%') ";
                                       
                                }
                                else
                                {
                                    sFilter = sFilter + " AND (" + _dv.Table.Columns["sBatchName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        _dv.Table.Columns["dtCreateDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                         _dv.Table.Columns["dtStatementDate"].ColumnName + " Like '" + strSearch + "%' OR " +
                                         _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%' )";                                      
                                      
                                }

                            }
                        }
                        _dv.RowFilter = sFilter;

                    }
                }
                else
                {
                    _dv.RowFilter = "";
                }
                //
               

            }
            
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
        }

        #endregion


        #region " Other Events"

        private void pnl_View_SizeChanged(object sender, EventArgs e)
        {
            c1PatientTemplates.Width = pnl_View.Width - 1;
        }

        #endregion


        #region " Patient Strip Control Events "

        void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (oPatientControl.PatientID > 0)
            {
                _PatientID = oPatientControl.PatientID;
                oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 1, false);
                Fill_PatientTemplate();
            }
        }    

        void oPatientControl_PatientModified(object sender, EventArgs e)
        {
            try
            {

                if (oPatientControl.PatientID > 0)
                {
                    _PatientID = oPatientControl.PatientID;                  
                    Fill_PatientTemplate();
                }
            }
            catch (Exception ex)                                                                                                                                                                                                                                                        
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {

                if (oPatientControl != null)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (oPatientControl.Name == this.Controls[i].Name)
                        {
                            this.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    try
                    {
                        oPatientControl.OnPatientSearchKeyPress -= new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                        oPatientControl.PatientModified -= new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);

                    }
                    catch { }
                    oPatientControl.Dispose();
                    oPatientControl = null;
                }
                oPatientControl = new gloPatientStripControl.gloPatientStripControl(_databaseconnectionstring, SearchEnable);
                //oPatientControl.ControlSize_Changed += new gloPatientStripControl.gloPatientStripControl.ControlSizeChanged(oPatientControl_ControlSize_Changed);
                oPatientControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                oPatientControl.PatientModified += new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);
                oPatientControl.btnDownEnable = true;
                oPatientControl.btnUpEnable = true;
                oPatientControl.DTP.Visible = false;
                oPatientControl.FillDetails(PatientId, gloPatientStripControl.FormName.None, PatientProviderId, false);
                pnlTemplateDetails.Controls.Add(oPatientControl);
                oPatientControl.Dock = DockStyle.Top;
                oPatientControl.Padding = new Padding(0, 0, 3, 0);
                oPatientControl.BringToFront();
                panel2.BringToFront();  

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion " Patient Strip Control Events "
        
              
        #region " Tree View Events"

        private void trv_viewTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {           
            Fill_PatientTemplate();              
        }

        #endregion

        
        private void tls_btnView_Click(object sender, EventArgs e)
        {
            if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
            {
                if (c1PatientTemplates.Rows.Selected != null)
                {
                    if (c1PatientTemplates.RowSel > 0)
                    {
                        _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTemplateTranID));
                        //_PatientID_To_Delete = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nPatientID));
                    }
                }
            }
            if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
            {
                if (c1PatientTemplates.Rows.Selected != null)
                {
                    if (c1PatientTemplates.RowSel > 0)
                    {
                        if (_nTrasactionID > 0)
                        {
                            //ModifyPatientTemplate();
                            ViewPatientTemplate();
                        }
                    }
                }
            }
        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_VoidStatment_Click(object sender, EventArgs e)
        {
            try
            {

                Int64 MasterId = 0;
                Int64 DetailId = 0;
                String Status = "";
                if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
                {
                    if (c1PatientTemplates.Rows.Selected != null)
                    {
                        if (c1PatientTemplates.RowSel > 0)
                        {
                            //_nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));
                            DetailId = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nMasterDetailId));
                            MasterId = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nMasterBatchId));
                            Status = (c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_bIsVoid)).ToString();
                            //_PatientID_To_Delete = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nPatientID));
                        }
                    }
                }
                if (Status != "Voided")
                {
                    DialogResult dlgRst = MessageBox.Show("Do you want to void the statement? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dlgRst == DialogResult.Yes)
                    {
                        frmVoidStatmentBatch Objfrm = new frmVoidStatmentBatch(MasterId, DetailId, false);
                        Objfrm.ShowDialog(this);
                        Objfrm.Dispose();
                        Objfrm = null;
                        Fill_PatientTemplate();
                    }
                }
                else
                {
                    MessageBox.Show("Statement is already voided", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }


    }
}