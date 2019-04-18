using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Collections;

namespace gloOffice
{
    public partial class frmSetupPatientStatement : Form
    {
        #region  " Variable Declarations "

        String _databaseConnectionString = "";
        String _MessageBoxCaption = String.Empty;

        Wd.Document oCurDoc;
        //Wd.Document oTempDoc;
        Wd.Application oWordApp;

        String _LoadingTemplateName = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Variable Declarations "

        #region " Properties Procedures "

        long _TemplateID = 0;

        public long TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        #endregion " Properties Procedures "

        #region " Constructor "
        public frmSetupPatientStatement(String DataBaseConnectionString, Int64 TemplateID)
        {
            InitializeComponent();
            _TemplateID = TemplateID;
            _databaseConnectionString = DataBaseConnectionString;

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

        private void frmSetupPatientStatement_Load(object sender, EventArgs e)
        {
            DataTable dttemp;
            try
            {
                Fill_DataDictionary();
                Fill_Providers();

                //  HARD CODED TEMPLATE ID //
                _TemplateID = 12301;
                // ----------------------- //

                if (_TemplateID > 0)
                {
                    dttemp = new DataTable();
                    gloOffice.gloPatientStatement ogloPatientStatement = new gloPatientStatement(_databaseConnectionString);
                    gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                    dttemp = ogloPatientStatement.GetSingleTemplate(_TemplateID);

                    if (dttemp != null && dttemp.Rows.Count > 0)
                    {
                        txtTemplateName.Text = dttemp.Rows[0]["sTemplateName"].ToString();
                        _LoadingTemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
                        cmbProvider.SelectedValue = Convert.ToInt64(dttemp.Rows[0]["nProviderID"]);

                        //Set the File to control
                        string strNewDocumentName = "";
                        strNewDocumentName = gloOffice.Supporting.NewDocumentName();

                        object objTemplateDocument;

                        if (dttemp.Rows[0]["sDescription"] != null)
                        {
                            objTemplateDocument = dttemp.Rows[0]["sDescription"];
                            ogloTemplate.ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);
                        
                            //  wdTemplate.Open(strNewDocumentName);
                            object thisObject = (object)strNewDocumentName;
                           // Wd.Application oWordApp = null;
                            gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                            strNewDocumentName = (string)thisObject;
                        }

                    }
                    else
                    {
                        wdTemplate.CreateNew("Word.Document");
                    }

                    ogloPatientStatement = null;
                    ogloTemplate = null;
                }
                else
                {
                    wdTemplate.CreateNew("Word.Document");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Fill_DataDictionary()
        {
            TreeNode oPatientStatementNode = new TreeNode();
            gloGeneralNode.gloGeneralNode oFieldNode;

            oPatientStatementNode.Text = "Patient Statement";
            oPatientStatementNode.Tag = 0;

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Date";
            oFieldNode.Code = "PatientStatement.Date";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "CPT";
            oFieldNode.Code = "PatientStatement.CPT";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "CPT with Payment";
            oFieldNode.Code = "PatientStatement.CPTWithPayment";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Description";
            oFieldNode.Code = "PatientStatement.Description";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Dx";
            oFieldNode.Code = "PatientStatement.Dx";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Charges";
            oFieldNode.Code = "PatientStatement.Charges";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Paid";
            oFieldNode.Code = "PatientStatement.Paid";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Balance";
            oFieldNode.Code = "PatientStatement.Balance";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = new gloGeneralNode.gloGeneralNode();
            oFieldNode.Text = "Due Table";
            oFieldNode.Code = "PatientStatement.DueTable";
            oFieldNode.ID = 0;
            oPatientStatementNode.Nodes.Add(oFieldNode);

            oFieldNode = null;

            trvDataDictionary.Nodes.Add(oPatientStatementNode);
            oPatientStatementNode.ExpandAll();
        }

        private void Fill_Providers()
        {
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseConnectionString);
            DataTable dt = new DataTable();
            dt = oResource.GetProviders();
            //nCategoryID, sDescription
            if (dt != null)
            {
                cmbProvider.DataSource = dt;
                cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                if (dt.Rows.Count > 0)
                {
                    cmbProvider.SelectedIndex = 0;
                }
            }

        }

        private ArrayList GetDueList()
        {
            ArrayList arrDueList = new ArrayList();
            try
            {
                gloGeneralNode.gloGeneralNode oGeneralNode;

                oGeneralNode = new gloGeneralNode.gloGeneralNode();
                oGeneralNode.Text = "Current";
                oGeneralNode.Code = "DueTable.Current";
                arrDueList.Add(oGeneralNode);

                oGeneralNode = new gloGeneralNode.gloGeneralNode();
                oGeneralNode.Text = "31-60 Days";
                oGeneralNode.Code = "DueTable.3160Days";
                arrDueList.Add(oGeneralNode);

                oGeneralNode = new gloGeneralNode.gloGeneralNode();
                oGeneralNode.Text = "61-90 Days";
                oGeneralNode.Code = "DueTable.6190Days";
                arrDueList.Add(oGeneralNode);

                oGeneralNode = new gloGeneralNode.gloGeneralNode();
                oGeneralNode.Text = "91-120 Days";
                oGeneralNode.Code = "DueTable.91120Days";
                arrDueList.Add(oGeneralNode);

                oGeneralNode = new gloGeneralNode.gloGeneralNode();
                oGeneralNode.Text = "Over 120 Days";
                oGeneralNode.Code = "DueTable.Over120Days";
                arrDueList.Add(oGeneralNode);

                oGeneralNode = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            return arrDueList;
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTemplateName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a name for the template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtTemplateName.Focus();
                    return;
                }

                if (cmbProvider.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbProvider.Focus();
                    return;
                }

                gloOffice.gloPatientStatement ogloPatientStatement = new gloPatientStatement(_databaseConnectionString);

                // SUDHIR - 20090123 -- TO CHECK WHETHER TEMPLATE EXIST IN CATEGORY. //

                //if (ogloTemplate.IsTemplateNamePresent(txtTemplateName.Text.Trim(), cmbCategory.Text.ToString()) == true && _LoadingTemplateName != txtTemplateName.Text.Trim())
                //{
                //    MessageBox.Show("Template Name already present in Category ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtTemplateName.SelectAll();
                //    return;
                //}
                //-------------//

                String sFileName = gloOffice.Supporting.NewDocumentName();

                object oFileName = (object)sFileName;
                object missing = System.Reflection.Missing.Value;
                object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                wdTemplate.Close();

                ogloPatientStatement.SaveTemplate(_TemplateID, txtTemplateName.Text.Trim(), 0, Convert.ToInt64(cmbProvider.SelectedValue), sFileName);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_CreateTable_Click(object sender, EventArgs e)
        {
            ArrayList arrList = new ArrayList();
            ArrayList arrDueList = new ArrayList();
            bool NodeFound = false;
            bool DueNodeFound = false;
            try
            {
                // Create ArrayList of Checked Nodes to send to Create Table.
                foreach (gloGeneralNode.gloGeneralNode oNode in trvDataDictionary.Nodes[0].Nodes)
                {
                    if (oNode.Checked == true)
                    {
                        if (oNode.Text != "Due Table")
                        {
                            arrList.Add(oNode);
                            NodeFound = true;
                        }
                        else
                        {
                            DueNodeFound = true;
                        }
                    }
                }

                if (NodeFound == false)
                {
                    MessageBox.Show("Please select formfields.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Pre Validation for Duplicate FormFields.
                gloOffice.Supporting.CurrentDocument = oCurDoc;
                if (gloOffice.Supporting.IsDuplicateFormFields(arrList) == true)
                {
                    MessageBox.Show("FormField(s) are already present in Document.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gloWord.gloWord.CurrentDoc = oCurDoc;
                if (arrList != null)
                {
                    gloWord.gloWord.InsertTable(arrList, true);
                }

                if (DueNodeFound == true)
                {
                    arrDueList = GetDueList();
                    if (arrDueList != null)
                    {
                        if (arrDueList.Count > 0)
                        {
                            gloWord.gloWord.InsertTable(arrDueList, false);
                        }
                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trvDataDictionary_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (trvDataDictionary.SelectedNode != null)
                {
                    if (trvDataDictionary.SelectedNode.Parent != null)
                    {
                        ArrayList arrList = new ArrayList();
                        gloOffice.Supporting.CurrentDocument = oCurDoc;
                        gloWord.gloWord.CurrentDoc = oCurDoc;

                        #region " Insert Due Table"
                        if (trvDataDictionary.SelectedNode.Text == "Due Table")
                        {
                            arrList = GetDueList();
                            if (gloOffice.Supporting.IsDuplicateFormFields(arrList) == true)
                            {
                                MessageBox.Show("FormFields for due table are already present in Document.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                arrList = null;
                                return;
                            }
                            gloWord.gloWord.InsertTable(arrList, false);
                            arrList = null;
                            return;
                        }
                        #endregion

                        #region " Insert Other FormFields "
                        Wd.FormField oFormField;
                        gloGeneralNode.gloGeneralNode oSelectedNode = new gloGeneralNode.gloGeneralNode();

                        oSelectedNode = (gloGeneralNode.gloGeneralNode)trvDataDictionary.SelectedNode;

                        //Pre Validation for Duplicate FormFields.
                        arrList.Add(oSelectedNode);
                        if (gloOffice.Supporting.IsDuplicateFormFields(arrList) == true)
                        {
                            MessageBox.Show("FormField is already present in Document.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            arrList = null;
                            return;
                        }
                        arrList = null;

                        if (chk_IncludeLable.Checked == true)
                        { oCurDoc.Application.Selection.TypeText(trvDataDictionary.SelectedNode.Text + ": "); }
                        oFormField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput);
                        oFormField.Result = oSelectedNode.Text; //Result To show caption
                        oFormField.StatusText = oSelectedNode.Code; //Status text to hold Table & field names 
                        oFormField.HelpText = oSelectedNode.Text; //Help text to hold group

                        oCurDoc.ActiveWindow.SetFocus();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void trvDataDictionary_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Level == 0)
                {
                    foreach (TreeNode oNode in e.Node.Nodes)
                    {
                        oNode.Checked = e.Node.Checked;
                        if (oNode.Text == "CPT with Payment")
                        {
                            oNode.Checked = false;
                        }
                    }
                }
            }
        }

        private void wdTemplate_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;
            oWordApp = oCurDoc.Application;
        }

        private void wdTemplate_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                if ((oCurDoc != null))
                {
                    //' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit 
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
                //if ((oWordApp != null))
                //{
                //    //RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent 
                //    //RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked 
                //    //UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick for oWordApp") 
                //   // Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch// (Exception ex)
            {

            }
        }

        private void wdTemplate_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            //if ((oWordApp != null))
            //{
            //    //oWordApp.WindowSelectionChange -= DDLCBEvent;
            //    //oWordApp.WindowBeforeDoubleClick -= OnFormClicked;
            //    foreach (Wd.RecentFile oFile in oWordApp.RecentFiles)
            //    {
            //        if (oFile.Path == Application.StartupPath + "\\Temp")
            //        {
            //            oFile.Delete();
            //        }
            //    }

            //}
        }

        private void txtSearchAppBook_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchAppBook.Text == "")
            {
                trvDataDictionary.SelectedNode = null;
                return;
            }

            foreach (TreeNode oSearchNode in trvDataDictionary.Nodes[0].Nodes)
            {
                if (oSearchNode.Text.ToUpper().Contains(txtSearchAppBook.Text.Trim().ToUpper()))
                {
                    trvDataDictionary.SelectedNode = oSearchNode;
                    return;
                }
            }
            trvDataDictionary.SelectedNode = null;
        }

        #region " Button Events "
        private void btnUp_Click(object sender, EventArgs e)
        {
            TreeNode oNode = trvDataDictionary.SelectedNode;
            TreeNode TempNode;
            trvDataDictionary.BeginUpdate();
            try
            {
                if (oNode != null)
                {
                    if (oNode.Level == 1 && oNode != trvDataDictionary.Nodes[0].FirstNode)
                    {                        
                        TempNode = oNode;
                        if (TempNode == trvDataDictionary.Nodes[0].LastNode)
                        {
                            oNode.Remove();
                            trvDataDictionary.Nodes[0].Nodes.Insert(trvDataDictionary.Nodes[0].LastNode.Index, (gloGeneralNode.gloGeneralNode)TempNode);
                        }
                        else
                        {
                            oNode.Remove();
                            trvDataDictionary.Nodes[0].Nodes.Insert(trvDataDictionary.SelectedNode.Index - 1, (gloGeneralNode.gloGeneralNode)TempNode);
                        }
                        trvDataDictionary.SelectedNode = TempNode;
                    }
                }
            }
            catch
            { }
            trvDataDictionary.EndUpdate();

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            TreeNode oNode = trvDataDictionary.SelectedNode;
            TreeNode TempNode;
            trvDataDictionary.BeginUpdate();
            try
            {
                if (oNode != null)
                {
                    if (oNode.Level == 1 && oNode != trvDataDictionary.Nodes[0].LastNode)
                    {
                        TempNode = oNode;
                        oNode.Remove();
                        trvDataDictionary.Nodes[0].Nodes.Insert(trvDataDictionary.SelectedNode.Index + 1, (gloGeneralNode.gloGeneralNode)TempNode);
                        trvDataDictionary.SelectedNode = TempNode;
                    }
                }
            }
            catch
            { }
            trvDataDictionary.EndUpdate();

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            trvDataDictionary.Nodes[0].Checked = false;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            trvDataDictionary.Nodes[0].Checked = true;
        }
        #endregion
    }
}