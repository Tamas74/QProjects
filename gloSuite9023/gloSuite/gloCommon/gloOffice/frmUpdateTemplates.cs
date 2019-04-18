using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;

namespace gloOffice
{
    public partial class frmUpdateTemplates : Form
    {

        #region " Private Variables "
        //const string _MessageBoxCaption = null;
        string _MessageBoxCaption = String.Empty;
        string _DatabaseConnectionString = "";
       // Int64 _TemplateID = 0;
        String _sBibliographicinfo = "";
        String _sBibliographicDeveloper = "";
        wd.Document oCurDoc;
      //  wd.Application oWordApplication;


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion 

        #region " Constructor "
        public frmUpdateTemplates(string connectionString)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionString;

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
        
        private void frmUpdateTemplates_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCategory;
                gloTemplate oTemplate = new gloTemplate(_DatabaseConnectionString);
                dtCategory = oTemplate.GetTemplateCategoryList();
                oTemplate.Dispose();
                oTemplate = null;
                if (dtCategory != null && dtCategory.Rows.Count>0)
                {
                    //cmbCategory.Items.Clear();
                    //for (int i = 0; i <= dtCategory.Rows.Count-1; i++)
                    //{
                    //    cmbCategory.Items.Add(dtCategory.Rows[i]["CategoryName"]);                    
                    //}
                    //if (cmbCategory.Items.Count > 0)
                    //{
                    //    cmbCategory.SelectedIndex = 0;
                    //}

                    trvCategory.Nodes.Clear();
                    for (int iRow = 0; iRow <= dtCategory.Rows.Count-1; iRow++)
                    {
                        trvCategory.Nodes.Add(dtCategory.Rows[iRow]["CategoryName"].ToString());                
                    }
                }
                if (dtCategory != null)
                {
                    dtCategory.Dispose();
                    dtCategory = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region " Public Functions "

        //public void UpdateTemplates(string categoryName)
        //{
        //    try
        //    {
        //        gloTemplate oTemplate = new gloTemplate(_DatabaseConnectionString);
        //        DataTable dtTemplate;
        //        dtTemplate = oTemplate.GetTemplates(categoryName);
        //        if (dtTemplate != null && dtTemplate.Rows.Count > 0)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            pgrbarStatus.Minimum = 0;
        //            pgrbarStatus.Maximum = dtTemplate.Rows.Count;
        //            pgrbarStatus.Value = 0;

        //            for (int iRow = 0; iRow <= dtTemplate.Rows.Count - 1; iRow++)
        //            {
        //                lblStatus.Text = "Updating ...  " + dtTemplate.Rows[iRow]["sTemplateName"].ToString();
        //                this.Refresh();
        //                UpdateTemplate(Convert.ToInt64(dtTemplate.Rows[iRow]["nTemplateID"]));
        //                pgrbarStatus.Value = iRow + 1;
        //            }
        //            lblStatus.Text = "Done";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblStatus.Text = "";
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        pgrbarStatus.Value = 0;
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        public void UpdateTemplates()
        {
            gloTemplate oTemplate = new gloTemplate(_DatabaseConnectionString);
            try
            {
                for (int iCategory = 0; iCategory <= trvCategory.Nodes.Count - 1; iCategory++)
                {
                    if (trvCategory.Nodes[iCategory].Checked == true)
                    {
                       
                        DataTable dtTemplate;
                        dtTemplate = oTemplate.GetTemplates(trvCategory.Nodes[iCategory].Text);
                        if (dtTemplate != null && dtTemplate.Rows.Count > 0)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            for (int iRow = 0; iRow <= dtTemplate.Rows.Count - 1; iRow++)
                            {
                                lblStatus.Text = "Updating " + dtTemplate.Rows[iRow]["sTemplateName"].ToString();
                                this.Refresh();
                                UpdateTemplate(Convert.ToInt64(dtTemplate.Rows[iRow]["nTemplateID"]));
                                pgrbarStatus.Increment(1);
                            }
                        }
                        if (dtTemplate != null)
                        {
                            dtTemplate.Dispose();
                            dtTemplate = null;
                        }
                    }
                }

                lblStatus.Text = "Done";

                // UNCHECK ALL NODES //
                tlb_ClearAll_Click(null, null);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "";
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pgrbarStatus.Value = 0;
                oTemplate.Dispose();
                oTemplate = null;
                this.Cursor = Cursors.Default;
            }
        }

        public void UpdateTemplate(Int64 templateID)
        {
            gloTemplate oTemplate = new gloTemplate(_DatabaseConnectionString);
            _sBibliographicinfo = "";
            _sBibliographicDeveloper = "";
            DataTable dtTemplate;
            Int64 Update_category_ID = 0;
            
            try
            {
                string strOldFileName = "";
                string strNewFileName = "";
                dtTemplate = oTemplate.GetSingleTemplate(templateID);
                if (dtTemplate != null && dtTemplate.Rows.Count > 0)
                {
                    gloOffice.Supporting.DataBaseConnectionString = _DatabaseConnectionString;
                    strOldFileName = gloOffice.Supporting.NewDocumentName();
                    oTemplate.ConvertBinaryToFile(dtTemplate.Rows[0]["sDescription"], strOldFileName);
                //    wdTemplate.Open(strOldFileName);
                    object thisObject = (object)strOldFileName;
                    wd.Application oWordApplication = null;
                    gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApplication);
                    strOldFileName = (string)thisObject;
                   
                    _sBibliographicinfo = Convert.ToString(dtTemplate.Rows[0]["sBibliographicinfo"]);
                    _sBibliographicDeveloper = Convert.ToString(dtTemplate.Rows[0]["sBibliographicDeveloper"]);
                    oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;
                    oCurDoc.ActiveWindow.SetFocus();
                   // oWordApplication = oCurDoc.Application;

                    // 00000601 :  Import templates from gloPM  not show in gloEMR
                    // get the category id of selected tree node.
                    Update_category_ID = oTemplate.GetCategoryID(dtTemplate.Rows[0]["CategoryName"].ToString());
                    // END

                    gloOffice.Supporting.WdApplication = oCurDoc.Application;
                    gloOffice.Supporting.CurrentDocument = oCurDoc;

                    if (gloOffice.Supporting.UpdateFormFields() == true)   // UPDATE FORMFIELDS //
                    {
                        strNewFileName = gloOffice.Supporting.NewDocumentName();
                        object oFileName = (object)strNewFileName;
                        object missing = System.Reflection.Missing.Value;
                        object oFileFormat = (object)wd.WdSaveFormat.wdFormatXMLDocument;
                        oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        wdTemplate.Close();

                        oTemplate.SaveTemplate(templateID, dtTemplate.Rows[0]["sTemplateName"].ToString(), Update_category_ID, dtTemplate.Rows[0]["CategoryName"].ToString(), Convert.ToInt64(dtTemplate.Rows[0]["nProviderID"]), strNewFileName, _sBibliographicinfo, _sBibliographicDeveloper); // REPLACE OLD TEMPLATE WITH NEW ONE. //                   
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Template - " + dtTemplate.Rows[0]["sTemplateName"].ToString() + " Updated (Repaired)", gloAuditTrail.ActivityOutCome.Success);

                        if (File.Exists(strNewFileName)) // DELETE TEMP FILES //
                        { File.Delete(strNewFileName); }
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Template - " + dtTemplate.Rows[0]["sTemplateName"].ToString() + " not updated (repaired)", gloAuditTrail.ActivityOutCome.Failure);
                        wdTemplate.Close();
                    }

                    if (File.Exists(strOldFileName))
                    { File.Delete(strOldFileName); }
                    

                }
                if (dtTemplate != null)
                {
                    dtTemplate.Dispose();
                    dtTemplate = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Template not updated (repaired)" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);                
            }
            finally
            {
                if (oTemplate != null)
                {
                    oTemplate.Dispose();
                    oTemplate = null;
                }
            }
        } 
        #endregion

        #region " ToolStrip Events "

        private void tlb_Update_Click(object sender, EventArgs e)
        {
            //UpdateTemplates(cmbCategory.Text.Trim());
            if (SetProgressBar() == true)
            {
                UpdateTemplates();
            }
            else
            {
                MessageBox.Show("Please select template categories.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tlb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode oNode in trvCategory.Nodes)
                {
                    oNode.Checked = true;
                }
                tlb_SelectAll.Visible = false;
                tlb_ClearAll.Visible = true;
            }
            catch 
            {}
        }

        private void tlb_ClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode oNode in trvCategory.Nodes)
                {
                    oNode.Checked = false;
                }
                tlb_ClearAll.Visible = false;
                tlb_SelectAll.Visible = true;
            }
            catch
            { }
        }

        #endregion 
               
        #region " Private Functions "
        private bool SetProgressBar()
        {
            SqlCommand cmd = null;
            SqlConnection con = null;
            try
            {
                string _Category = "";
                for (int i = 0; i <= trvCategory.Nodes.Count - 1; i++)
                {
                    if (trvCategory.Nodes[i].Checked == true)
                    {
                        _Category = _Category + "'" + trvCategory.Nodes[i].Text + "',";
                    }
                }

                if (_Category == "")
                {
                    return false;
                }
                else
                {
                    _Category = _Category.Remove(_Category.Length - 1);
                }

                string _Query = "SELECT COUNT(nTemplateID) FROM TemplateGallery_MST WHERE sCategoryName IN (" + _Category + ")";
                con = new SqlConnection(_DatabaseConnectionString);
                cmd = new SqlCommand(_Query, con);
                Object oResult;
                con.Open();
                oResult = cmd.ExecuteScalar();
               
                if (oResult != null && oResult.ToString() != "")
                {
                    pgrbarStatus.Minimum = 0;
                    pgrbarStatus.Maximum = Convert.ToInt32(oResult); ;
                    pgrbarStatus.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return true;
        }
        #endregion

        private void wdTemplate_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;
         //   oWordApplication = oCurDoc.Application;            
        }
        private void wdTemplate_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {                
                if ((oCurDoc != null))
                {
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
                //if ((oWordApplication != null))
                //{                    
                //   // Marshal.FinalReleaseComObject(oWordApplication);
                //    oWordApplication = null;
                //}
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }


       
    }
}