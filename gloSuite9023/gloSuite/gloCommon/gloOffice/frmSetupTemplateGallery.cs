using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace gloOffice
{


    public partial class frmSetupTemplateGallery : Form
    {

        #region  " Variable Declarations "

        String _databaseConnectionString = "";
        String _MessageBoxCaption = String.Empty;
        Wd.Document oCurDoc;
        //Wd.Document oTempDoc;
     Wd.Application oWordApp;
        String _LoadingTemplateName = "";
        String _sBibliographicinfo = "";
        String _sBibliographicDeveloper = "";
         object oMissing = System.Reflection.Missing.Value;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
      //  bool _Save = false; 

        Wd.ContentControl cntCtrl;
        #endregion " Variable Declarations "

        #region " Properties Procedures "

        private long _nCatagoryID = 0;

        public long CatagoryID
        {
            get { return _nCatagoryID; }
            set { _nCatagoryID = value; }
        }

        long _templateID = 0;

        public long TemplateID
        {
            get { return _templateID; }
            set { _templateID = value; }
        }

        #endregion " Properties Procedures "

        #region  " Form Constructors "

        public frmSetupTemplateGallery(String DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;
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

        public frmSetupTemplateGallery(String DatabaseConnectionString, long TemplateID)
        {
            //set the Constructor Parameters before initializing the Form.
            _databaseConnectionString = DatabaseConnectionString;
            _templateID = TemplateID;

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

        #endregion " Form Constructors "

        private void frmSetupTemplateGallery_Load(object sender, EventArgs e)
        {
            DataTable dttemp;
            _sBibliographicinfo = "";
            _sBibliographicDeveloper = "";
            try
            {
                Fill_DataDictionary();
                Fill_Categories();
                Fill_Providers();

                if (_templateID > 0)
                {
                    dttemp = new DataTable();
                    gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                    dttemp = ogloTemplate.GetSingleTemplate(_templateID);

                    if (dttemp != null && dttemp.Rows.Count > 0)
                    {
                        txtTemplateName.Text = dttemp.Rows[0]["sTemplateName"].ToString();
                        _LoadingTemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
                        _sBibliographicinfo = dttemp.Rows[0]["sBibliographicinfo"].ToString();
                        _sBibliographicDeveloper = dttemp.Rows[0]["sBibliographicDeveloper"].ToString();
                        cmbCategory.Text = dttemp.Rows[0]["CategoryName"].ToString();
                        //cmbCategory.SelectedValue = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
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
                    ogloTemplate = null;
                }
                else
                {
                    wdTemplate.CreateNew("Word.Document");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region " Private Methods "


        private bool SaveTemplate(bool isCloseTemplate)
        {
            bool _Result = false;
            try
            {
                // SUDHIR 20091103 // TO MAINTAIN FOCUS OF EXTERNAL WORD //
                if (gloWord.gloWord.CheckActiveWord(oCurDoc) == false)
                {
                    return false;

                }

                if (txtTemplateName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a name for the template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTemplateName.Focus();
                    return false;

                }

                if (cmbCategory.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a category.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCategory.Focus();
                    return false;
                }

                if (cmbProvider.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbProvider.Focus();
                    return false;
                }

                gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                ogloTemplate.TemplateID = _templateID;

                // SUDHIR - 20090123 -- TO CHECK WHETHER TEMPLATE EXIST IN CATEGORY. //

                if (ogloTemplate.IsTemplateNamePresent(txtTemplateName.Text.Trim(), cmbCategory.Text.ToString(), Convert.ToInt64(cmbProvider.SelectedValue)) == true)
                {
                    MessageBox.Show("Template name for this Category and Provider already exists.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTemplateName.SelectAll();
                    return false;
                }
                //-------------//

                String sFileName = gloOffice.Supporting.NewDocumentName();

                object oFileName = (object)sFileName;
                object missing = System.Reflection.Missing.Value;
                object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                wdTemplate.Close();
                _nCatagoryID = Convert.ToInt64(cmbCategory.SelectedValue);

                string sUserActivity = "";
                int sUserActivityType = 0;

                if (_templateID == 0)
                {
                    sUserActivity = "New Template (" + txtTemplateName.Text.Trim() + ") Created";
                    sUserActivityType = 1;
                }
                else
                {
                    sUserActivity = "Template (" + txtTemplateName.Text.Trim() + ") Modified";
                    sUserActivityType = 2;
                }

                _templateID = ogloTemplate.SaveTemplate(_templateID, txtTemplateName.Text.Trim(), _nCatagoryID, cmbCategory.Text.ToString(), Convert.ToInt64(cmbProvider.SelectedValue), sFileName, _sBibliographicinfo, _sBibliographicDeveloper);

                if (sUserActivityType == 1)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, sUserActivity, 0, _templateID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, sUserActivity, 0, _templateID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                }


                if (!isCloseTemplate)
                {
                    LoadWordControl(sFileName);
                    oCurDoc.Saved = true;
                }


                _Result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _Result;
        }
        private void Fill_DataDictionary()
        {
            gloOffice.gloTemplate ogloOffice = new gloTemplate(_databaseConnectionString);

            DataTable dt = new DataTable();
            dt = ogloOffice.Get_DataDictionaryTables();
            //sTableName, sTableCaption
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = dt.Rows[i]["sTableCaption"].ToString();
                        //oNode.Tag = dt.Rows[i]["sTableName"].ToString();
                        oNode.ImageIndex = 0;
                        oNode.SelectedImageIndex = 0;
                        trvDataDictionary.Nodes.Add(oNode);

                        DataTable dtFields = new DataTable();
                        dtFields = ogloOffice.Get_DataDictionaryFields(oNode.Text.ToString());
                        //nDictionaryID, sFieldName, sTableName, sCaption, sTableCaption 
                        if (dtFields != null)
                        {
                            if (dtFields.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtFields.Rows.Count; j++)
                                {
                                    // SUDHIR - TEMPERORY HIDING STATEMENT FORMFIELDS.
                                    if (dtFields.Rows[j]["sCaption"].ToString() == "Current Statement No" || dtFields.Rows[j]["sCaption"].ToString() == "Previous Statement No")
                                    {
                                        continue;
                                    }
                                    gloGeneralNode.gloGeneralNode oFieldNode = new gloGeneralNode.gloGeneralNode();
                                    oFieldNode.Text = dtFields.Rows[j]["sCaption"].ToString();
                                    oFieldNode.Code = dtFields.Rows[j]["sFieldName"].ToString();
                                    oFieldNode.ID = Convert.ToInt64(dtFields.Rows[j]["nDictionaryID"]);
                                    oNode.Nodes.Add(oFieldNode);
                                }
                            }
                        }
                    }

                }
            }

        }
        private void Fill_Categories()
        {
            gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            DataTable dt = new DataTable();
            //dt = ogloTemplate.GetList("Template");
            dt = ogloTemplate.GetTemplateCategoryList();
            //nCategoryID, sDescription
            if (dt != null)
            {
                // COMMENTED BY SUDHIR - 20090123
                DataView _dv = dt.DefaultView;
                _dv.RowFilter = "CategoryName <> 'MIS Reports'";
                dt = _dv.ToTable();

                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = dt.Columns["CategoryName"].ColumnName;
                cmbCategory.ValueMember = dt.Columns["CategoryID"].ColumnName;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["CategoryName"].ToString().Trim() != "MIS Reports")
                //    {
                //        cmbCategory.Items.Add(dt.Rows[i]["CategoryName"].ToString());
                //    }
                //}

                //if (cmbCategory.Items.Count > 0)
                //{
                //    cmbCategory.SelectedIndex = 0;
                //}
            }

        }
        private void Fill_Providers()
        {
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseConnectionString);
            DataTable dt = new DataTable();
            DataRow dr;
            dt = oResource.GetProviders();
            //nCategoryID, sDescription
            if (dt != null)
            {
                // Sudhir - 20090409 - All Provider //
                dr = dt.NewRow();
                dr["ProviderName"] = "All";
                dr["nProviderID"] = "0";
                dt.Rows.InsertAt(dr, 0);
                // End Sudhir //
                cmbProvider.DataSource = dt;
                cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                if (dt.Rows.Count > 0)
                {
                    cmbProvider.SelectedIndex = 0;
                }
            }

        }
        private bool IsFeasible()
        {
            try
            {

                if (oCurDoc.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments || oCurDoc.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdAllowOnlyFormFields || oCurDoc.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdAllowOnlyReading)
                {
                    MessageBox.Show("Current operation is invalid as document is under protection mode.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Selection.InlineShapes.AddOLEObject ClassType:="Excel.Sheet.12",
                if (oCurDoc.Application.Selection.InlineShapes.Count > 0)
                {
                    if (oCurDoc.Application.Selection.InlineShapes[1].Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapeEmbeddedOLEObject)
                    {
                        MessageBox.Show("Data dictionary cannot be used inside the embedded objects.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (oCurDoc.Application.Selection.Type == Microsoft.Office.Interop.Word.WdSelectionType.wdSelectionInlineShape)
                {
                    MessageBox.Show("Data dictionary cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (oCurDoc.Application.Selection.ShapeRange.Count > 0)
                {
                    MessageBox.Show("Data dictionary cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (oCurDoc.Application.Selection.HeaderFooter != null)
                {
                    //if (oCurDoc.Application.Selection.HeaderFooter.IsHeader)
                    //{
                    MessageBox.Show("Data dictionary cannot be used inside header or footer.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                    // }
                }


                Wd.Range r = oCurDoc.Application.Selection.Range;
                r.SetRange(oCurDoc.Application.Selection.Start, oCurDoc.Application.Selection.End + 1);
                if (r.ContentControls.Count == 1)
                {
                    //'If oCurDoc.Application.Selection.Range.ContentControls.Count > 0 Then
                    MessageBox.Show("Data dictionary cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (r.ParentContentControl != null)
                {
                    MessageBox.Show("Data dictionary cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                return true;
            }
            catch
            {

                return false;
            }
        }
        private void LoadWordControl(String strFileName)
        {
            try
            {
       //         wdTemplate.Open(strFileName);
                object thisObject = (object)strFileName;
            //    Wd.Application oWordApp = null;
                String strError =  gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                strFileName = (string)thisObject;
                if (strError != String.Empty)
                {
                    MessageBox.Show("Template cannot be open because there are problems with the contents.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wdTemplate.CreateNew("Word.Document");
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Template cannot be open because there are problems with the contents.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wdTemplate.CreateNew("Word.Document");
                //ex.ToString();
                //ex = null;
            }

        }
        private void OpenTemplate()
        {
            OpenFileDialog oFileDialog = new OpenFileDialog();
            oFileDialog.Multiselect = false;
            oFileDialog.Filter = "MS-Word Files (*.doc,*.dot,*.docx,*.dotx) | *.doc;*.dot;*.docx;*.dotx";
            if (oFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (File.Exists(oFileDialog.FileName))
                    LoadWordControl(oFileDialog.FileName);
            }
            oFileDialog.Dispose();
            oFileDialog = null;
        }
        private void UnDoChanges()
        {
            try
            {
                if (oCurDoc != null)
                    oCurDoc.Undo(ref oMissing);
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }
        private void ReDoChanges()
        {
            if (oCurDoc != null)
                oCurDoc.Redo(ref oMissing);


        }
        private void SaveAsTemplate()
        {
            _templateID = 0;
            txtTemplateName.Focus();
           
           
        }
        private void NewTemplate()
        {
            try
            {
                _templateID = 0;
                txtTemplateName.Text = "";
                cmbCategory.Text = "";
                txtTemplateName.Focus();
                wdTemplate.CreateNew("Word.Document");
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        private void InsertFile(String InsertItem)
        {
            try
            {
                OpenFileDialog oFileDialog = new OpenFileDialog();

                if (InsertItem == "InsertFile")
                {
                    oFileDialog.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf";
                    oFileDialog.FilterIndex = 3;
                    oFileDialog.Title = "Insert External Documents";
                    oFileDialog.Multiselect = false;
                    if (oFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        FileInfo oFile = new FileInfo(oFileDialog.FileName);
                        if (oFile.Extension.ToUpper() == ".TXT" || oFile.Extension.ToUpper() == ".DOC" || oFile.Extension.ToUpper() == ".DOCX" || oFile.Extension.ToUpper() == ".RTF")
                        {
                            oCurDoc.Application.Selection.InsertFile(oFile.FullName, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                        }
                    }
                }
                else if (InsertItem == "InsertImage")
                {
                    oFileDialog.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|TIFF Image (*.TIFF,*.TIF)|*.TIFF;*.TIF|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
                    oFileDialog.FilterIndex = 1;
                    oFileDialog.Title = "Insert External Image";
                    oFileDialog.Multiselect = true;
                    if (oFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        FileInfo oFile;
                        foreach (String CurFile in oFileDialog.FileNames)
                        {
                            oFile = new FileInfo(CurFile);
                            if (oFile.Extension.ToUpper() == ".JPEG" || oFile.Extension.ToUpper() == ".JPG" || oFile.Extension.ToUpper() == ".TIFF" || oFile.Extension.ToUpper() == ".TIF" || oFile.Extension.ToUpper() == ".BMP" || oFile.Extension.ToUpper() == ".PNG")
                            {
                                //oCurDoc.Application.Selection.InlineShapes.AddPicture(oFile.FullName, ref oMissing, ref oMissing, ref oMissing);
                                InsertImage(oFile.FullName);
                            }
                        }
                    }
                }

                if (oFileDialog != null)
                {
                    oFileDialog.Dispose();
                    oFileDialog = null;
                }
            }
            catch (COMException exCOM)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exCOM.ToString(), false);
                MessageBox.Show(exCOM.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Opening File " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private void InsertLogo()
        {
            try
            {
                string _ImagePath = "";
                gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
                gloOffice.Supporting.ClinicID = 1;
                gloOffice.Supporting.DocumentCategory = 2;
                _ImagePath = gloOffice.Supporting.GetDataFrom_DB("Clinic_MST.imgClinicLogo", "");
                _ImagePath = _ImagePath.Substring(0, _ImagePath.Length - 2);// Mid(ImagePath, 1, Len(ImagePath) - 2)

                if (oCurDoc == null)
                    return;

                if (oCurDoc.Application.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdNormalView || oCurDoc.Application.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdOutlineView)
                    oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView;

                oCurDoc.Activate();
                oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader;
                oCurDoc.Application.ActiveDocument.PageSetup.DifferentFirstPageHeaderFooter = 0;


                if (File.Exists(_ImagePath))
                    InsertImage(_ImagePath);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument;
            }


        }
        private void InsertImage(String ImagePath)
        {
            if (File.Exists(ImagePath))
                oCurDoc.ActiveWindow.SetFocus();
            gloWord.gloWord.InsertImageIntoSelectionField(ref oCurDoc, ImagePath,"");

            //try
            //{
            //    Image oImage = Image.FromFile(ImagePath);
            //    if (oImage != null)
            //    {
            //        try
            //        {
            //            gloWord.gloWord.GetClipboardData();
            //            //Clipboard.Clear();
            //            try
            //            {
            //                Clipboard.SetImage(oImage);
            //            }
            //            catch
            //            {
            //            }
            //        }
            //        catch //(Exception ex2)
            //        {
            //        }

            //        try
            //        {
            //            try
            //            {
            //                oCurDoc.Application.Selection.Paste();
            //            }
            //            catch
            //            {

            //            }
            //            //Clipboard.Clear();
            //            gloWord.gloWord.SetClipboardData();
            //        }
            //        catch //(Exception ex1)
            //        {

            //        }
            //        oImage.Dispose();
            //        oImage = null;
            //    }
            //}
            //catch //(Exception Ex)
            //{

            //}
        }

        private void InsertCheckBox()
        {
            try
            {
                if (oCurDoc != null)
                {
                    Wd.FormField oFormField;
                    oFormField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox);
                    oCurDoc.ActiveWindow.SetFocus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InsertDropDown()
        { 
            //if (oCurDoc != null)
            //{
            //    //Retrive list of drop down items from other window
            //    //System.Collections.ArrayList oDropDownItems;
            //    //string sDropDownTitle = "";

            //    //Temp list start
            //    string sDropDownTitle = "PM Drop Down";
            //    System.Collections.ArrayList oDropDownItems = new System.Collections.ArrayList();
            //    oDropDownItems.Add("Sagar Ghodke");
            //    oDropDownItems.Add("Saket Hire");
            //    oDropDownItems.Add("Mukesh Patel");
            //    oDropDownItems.Add("Pramod Nair");
            //    oDropDownItems.Add("Yamini Vanara");
            //    oDropDownItems.Add("Sandip Tidake");
            //    //Temp list finish

            //    object oRange =Type.Missing ;
            //    //oRange =oCurDoc.Application.Selection.Range;
            //    oCurDoc.Application.Selection.Range.ContentControls.Add(Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlDropdownList, ref oRange);
            //    oCurDoc.Application.Selection.ParentContentControl.Title = sDropDownTitle;
            //    oCurDoc.Application.Selection.ParentContentControl.Tag = "New Key";
            //    oCurDoc.Application.Selection.ParentContentControl.DropdownListEntries.Clear();

            //    for (int i = 0; i <= oDropDownItems.Count - 1; i++)
            //    {
            //        oCurDoc.Application.Selection.ParentContentControl.DropdownListEntries.Add(oDropDownItems[i].ToString(), oDropDownItems[i].ToString(), i);
            //    }
            //    oCurDoc.ActiveWindow.SetFocus();
            //}

            try
            {

                if ((oCurDoc != null))
                {
                    //Dim oNameField As Wd.FormField
                    //oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormDropDown)
                    //if (IsFeasible(enumControls.FormFieldControl))
                    //{
                    frmAddDropDown objDD = new frmAddDropDown();
                    objDD.GetDropdownItems = null;
                    objDD.GetDropdownTitle = "";
                    objDD.ShowDialog(this);
                    ArrayList m_arrlist = objDD.GetDropdownItems;

                    if ((m_arrlist != null))
                    {
                        object oRange = Type.Missing;
                        oRange = oCurDoc.Application.Selection.Range;
                        var _with1 = oCurDoc.Application.Selection;
                        _with1.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlDropdownList, ref oRange);
                        _with1.ParentContentControl.Title = objDD.GetDropdownTitle;
                        _with1.ParentContentControl.Tag = GetUniqueKey();
                        //_with1.ParentContentControl.Tag = "NAME";
                        //.ParentContentControl.LockContentControl = True
                        _with1.ParentContentControl.DropdownListEntries.Clear();
                        for (Int32 _cnt = 0; _cnt <= m_arrlist.Count - 1; _cnt++)
                        {
                            _with1.ParentContentControl.DropdownListEntries.Add(m_arrlist[_cnt].ToString(), m_arrlist[_cnt].ToString(), _cnt);
                        }

                        oCurDoc.ActiveWindow.SetFocus();
                        //}
                    }
                    objDD.Dispose();
                    objDD = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetUniqueKey()
        {
           // int maxSize = 10;
           //// int minSize = 5;
           // char[] chars = new char[11];
           // string a = null;
           // a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
           // chars = a.ToCharArray();
           // int size = maxSize;
           // byte[] data = new byte[1];
           // System.Security.Cryptography.RNGCryptoServiceProvider crypto = new System.Security.Cryptography.RNGCryptoServiceProvider();
           // crypto.GetNonZeroBytes(data);
           // size = maxSize;
           // data = new byte[size];
           // crypto.GetNonZeroBytes(data);
           // System.Text.StringBuilder result = new System.Text.StringBuilder(size);
           // foreach (byte b in data)
           // {
           //     result.Append(chars[b % (chars.Length - 1)]);
           // }
           // return result.ToString();
            return gloGlobal.clsFileExtensions.RNGCharacterMask(10);
        }
     
        #endregion

        private void trvDataDictionary_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (trvDataDictionary.SelectedNode != null)
                {
                    if (trvDataDictionary.SelectedNode.Parent != null)
                    {


                        // SUDHIR 20091103 // TO MAINTAIN FOCUS OF EXTERNAL WORD //
                        if (gloWord.gloWord.CheckActiveWord(oCurDoc) == false)
                        {
                            return;
                        }

                        if (IsFeasible() == false)
                        {
                            return;
                        }

                        Wd.FormField oFormField;
                        gloGeneralNode.gloGeneralNode oSelectedNode = new gloGeneralNode.gloGeneralNode();

                        oSelectedNode = (gloGeneralNode.gloGeneralNode)trvDataDictionary.SelectedNode;

                        if (chk_IncludeLable.Checked == true)
                        { oCurDoc.Application.Selection.TypeText(trvDataDictionary.SelectedNode.Text + ": "); }
                        oFormField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput);
                        oFormField.Result = oSelectedNode.Text; //Result To show caption
                        oFormField.StatusText = oSelectedNode.Code; //Status text to hold Table & field names 
                        oFormField.HelpText = oSelectedNode.Text; //Help text to hold group

                        oCurDoc.ActiveWindow.SetFocus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void wdTemplate_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;
            oWordApp = oCurDoc.Application;
            oWordApp.WindowSelectionChange += DDLCBEvent;
            oWordApp.WindowBeforeDoubleClick += oWordApp_WindowBeforeDoubleClick;
        }

        void oWordApp_WindowBeforeDoubleClick(Wd.Selection Sel, ref bool Cancel)
        {
            string sActiveDocumentName = Sel.Document.FullName;
            if (string.IsNullOrEmpty(sActiveDocumentName))
                return;
            Cancel = false;
            Wd.Range r = null;
            try
            {
                r = Sel.Range;
            }
            catch (Exception)
            {
            }
            if (r == null)
                return;
            try
            {
                r.SetRange(Sel.Start, Sel.End + 1);
            }
            catch (Exception)
            {
            }
            if (r == null)
                return;

            if (r.ContentControls.Count == 1)
            {
                cntCtrl = r.ContentControls[1];

                if (cntCtrl.Type == Wd.WdContentControlType.wdContentControlDropdownList && r.Application.Selection.ParentContentControl.Temporary == false)
                {
                    AccessControl();
                    Cancel = true;
                }
            }
            else if (!(r.ParentContentControl == null))
            {
                cntCtrl = r.ParentContentControl;
                if (cntCtrl.Type == Wd.WdContentControlType.wdContentControlDropdownList)
                {
                    AccessControl();
                    Cancel = true;
                }
            }
        }

        private void AccessControl()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(AccessControl));
            }
            else
            {
                OpenDropDown();
            }
        }

        private void OpenDropDown()
        {
            if (oCurDoc != null)
            {
                ArrayList arrayList = new ArrayList();
                foreach (Wd.ContentControlListEntry ddlEntry in cntCtrl.DropdownListEntries)
                {
                    arrayList.Add(ddlEntry.Text);
                }

                frmAddDropDown frmDropdown = new frmAddDropDown();
                frmDropdown.GetDropdownItems = arrayList;
                frmDropdown.GetDropdownTitle = cntCtrl.Title;
                frmDropdown.ShowDialog((frmDropdown.Parent == null) ? this : frmDropdown.Parent);

                ArrayList tempArrayList = frmDropdown.GetDropdownItems;
                if (tempArrayList != null)
                {
                    var with = cntCtrl;
                    with.Title = frmDropdown.GetDropdownTitle;
                    with.DropdownListEntries.Clear();
                    for (int i = 0; i < tempArrayList.Count; i++)
                    {
                        with.DropdownListEntries.Add(Text: tempArrayList[i].ToString(), Value: tempArrayList[i].ToString());
                    }
                    tempArrayList.Clear();
                    tempArrayList = null;
                    oCurDoc.ActiveWindow.SetFocus();
                }
                frmDropdown.Dispose();
                frmDropdown = null;
            }
        }

        private void DDLCBEvent(Wd.Selection Sel)
        {
            try
            {
                if (Sel == null)
                {
                    return;
                }
                if (Sel.Start == Sel.End)
                {
                    Wd.Range r = null;
                    try
                    {
                        r = Sel.Range;

                    }
                    catch (Exception )
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }
                    try
                    {
                        r.SetRange(Sel.Start, Sel.End + 1);

                    }
                    catch (Exception )
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }

                    //r.SetRange(Sel.Start, Sel.End + 1);

                    if (r.FormFields != null && r.FormFields.Count >= 1)
                    {
                       // object om = System.Reflection.Missing.Value;
                        Wd.FormField f = null;
                        try
                        {
                            object o = 1;
                            f = r.FormFields.get_Item(ref o);
                            o = null;
                        }
                        catch
                        {

                        }
                        //If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then
                        //    Dim dd As Wd.DropDown = f.DropDown
                        //    Dim iCurSel As Integer = dd.Value

                        //    Dim oPU As oOffice.CommandBar = oWordApp.CommandBars.Add("CustomFormFieldPopup", oOffice.MsoBarPosition.msoBarPopup, om, True)
                        //    If False Then
                        //        ''  oOffice.CommandBarComboBox oDD = oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, true) as oOffice.CommandBarComboBox;

                        //        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)
                        //        oDD.Style = oOffice.MsoComboStyle.msoComboLabel
                        //        oDD.DropDownLines = dd.ListEntries.Count
                        //        For Each le As Wd.ListEntry In dd.ListEntries
                        //            oDD.AddItem(le.Name, om)
                        //        Next
                        //        oDD.ListIndex = iCurSel
                        //        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)
                        //        dd.Value = oDD.ListIndex
                        //    Else
                        //        myidx = dd.Value
                        //        Dim iter As Integer = 1
                        //        For Each le As Wd.ListEntry In dd.ListEntries
                        //            Dim btn As oOffice.CommandBarButton
                        //            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)
                        //            '   btn = CType(ConversionHelpers.AsWorkaround(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), GetType(oOffice.CommandBarButton)), oOffice.CommandBarButton)
                        //            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic
                        //            btn.Caption = le.Name
                        //            btn.Enabled = True
                        //            If iter = myidx Then
                        //                btn.State = oOffice.MsoButtonState.msoButtonDown
                        //            End If
                        //            System.Math.Min(System.Threading.Interlocked.Increment(iter), iter - 1)
                        //            AddHandler btn.Click, AddressOf btn_Click
                        //        Next
                        //        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)
                        //        dd.Value = myidx
                        //    End If
                        //End If
                        if (f != null)
                        {
                            if (f.Type == Wd.WdFieldType.wdFieldFormCheckBox)
                            {
                                f.CheckBox.Value = !f.CheckBox.Value;
                                object oUnit = Wd.WdUnits.wdCharacter;
                                object oCnt = 1;
                                object oMove = Wd.WdMovementType.wdMove;
                                Sel.MoveRight(ref oUnit, ref oCnt, ref oMove);

                                //SLR: 6/27/2014: Check for type before checkin?
                                //16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                if (f.CheckBox.Value == false)
                                {
                                    //'AllowEditing = True And
                                    if (f.HelpText == "Group")
                                    {
                                        Wd.Style style = CreateTableStyleFalse();
                                        foreach (Wd.Table T1 in r.Tables)
                                        {
                                            FormatTables(style, T1);
                                        }
                                        style = null;
                                    }

                                }
                                else
                                {
                                    //'AllowEditing = True And
                                    if (f.HelpText == "Group")
                                    {
                                        Wd.Style style = CreateTableStyleTrue();
                                        foreach (Wd.Table T1 in r.Tables)
                                        {
                                            FormatTables(style, T1);
                                        }
                                        style = null;
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception excp)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        public Wd.Style CreateTableStyleFalse()
        {
            object styleTypeTable = Wd.WdStyleType.wdStyleTypeTable;
            string StyleName = "New Table Style" + Convert.ToString(DateTime.Now);
            Wd.Style styl = oCurDoc.Styles.Add(StyleName, ref styleTypeTable);
            styl.Font.Name = "Arial";
            styl.Font.Size = 10;
            Wd.TableStyle stylTbl = styl.Table;
            stylTbl.Borders.Enable = 0;
            //1

            Wd.ConditionalStyle evenrowbinding = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding);
            evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone;
            //evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            evenrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
            //Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

            //evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
            //evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //evenrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite


            Wd.ConditionalStyle oddrowbinding = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdOddRowBanding);
            oddrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone;
            //oddrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            oddrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
            // Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            oddrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            oddrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            oddrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            oddrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

            //oddrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
            //oddrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //oddrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
            //oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite

            Wd.ConditionalStyle FirstRow = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow);
            //FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            FirstRow.Font.Size = 12;
            FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto;
            FirstRow.Font.Bold = 1;

            stylTbl.RowStripe = 1;
            return styl;
        }

        public void FormatTables(Wd.Style tstyle, Wd.Table tb1)
        {
            //For Each t1 As Wd.Table In oCurDoc.Tables
            object objtStyl = (object)tstyle;
            tb1.Range.set_Style(ref objtStyl);
            //Next
        }

        public Wd.Style CreateTableStyleTrue()
        {
            object styleTypeTable = Wd.WdStyleType.wdStyleTypeTable;
            string StyleName = "New Table Style" + Convert.ToString(DateTime.Now);
            Wd.Style styl = oCurDoc.Styles.Add(StyleName, ref styleTypeTable);
            styl.Font.Name = "Arial";
            styl.Font.Size = 10;
            Wd.TableStyle stylTbl = styl.Table;
            stylTbl.Borders.Enable = 1;

            Wd.ConditionalStyle evenrowbinding = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding);
            evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone;
            evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            evenrowbinding.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

            Wd.ConditionalStyle FirstRow = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow);
            //FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;
            FirstRow.Font.Size = 14;
            FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto;
            FirstRow.Font.Bold = 1;

            stylTbl.RowStripe = 1;
            return styl;
        }

        #region " Button Click Events "
        private void tsb_SaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveTemplate(true))
                this.Close();
        }
        private void tsb_Close_Click(object sender, EventArgs e)
        {
            
            if (oCurDoc != null && !oCurDoc.Saved)
            {
                switch (MessageBox.Show("Do you want to save the changes to template?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            if (SaveTemplate(true))
                                this.Close();
                            break;
                        }

                    case DialogResult.No:
                        {
                            this.Close();
                            break;
                        }
                }
            }
            else
            {
                this.Close();
            }

        }
        private void tsb_PatientStatement_Click(object sender, EventArgs e)
        {
            // SUDHIR 20091103 // TO MAINTAIN FOCUS OF EXTERNAL WORD //
            if (gloWord.gloWord.CheckActiveWord(oCurDoc) == false)
            {
                return;
            }
            Form ofrm = new frmSetupPatientStatement(_databaseConnectionString, 0);
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowDialog(this);
            ofrm.Dispose();
            ofrm = null;
        }
        private void tsb_New_Click(object sender, EventArgs e)
        {
             
            if (oCurDoc != null && oCurDoc.Saved)
                NewTemplate();
            else
            {
                switch (MessageBox.Show("Do you want to save the changes to template?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        if (SaveTemplate(true))
                            NewTemplate();
                        break;
                    case DialogResult.No:
                        NewTemplate();
                        break;
                }
            }
             
        }
        private void tsb_Open_Click(object sender, EventArgs e)
        {
            OpenTemplate();

        }
        private void tsb_Undo_Click(object sender, EventArgs e)
        {
            UnDoChanges();

        }
        private void tsb_Redo_Click(object sender, EventArgs e)
        {
            ReDoChanges();

        }
        private void tsb_SaveAs_Click(object sender, EventArgs e)
        {
            if (SaveTemplate(false))
            {
                 SaveAsTemplate();
            }
            
           
       }
        private void tsb_InsertFile_Click(object sender, EventArgs e)
        {
            if (IsFeasible())
            {
                InsertFile("InsertFile");
            }
        }
        private void tsb_Header_Click(object sender, EventArgs e)
        {
            if (IsFeasible())
            {
                InsertLogo();
            }

        }
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            SaveTemplate(false);
        }

        private void tsb_InsertCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFeasible())
                {
                    InsertCheckBox();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "This command is not available.")
                {
                    MessageBox.Show("DataFields, Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsb_InsertDropDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFeasible())
                {
                    InsertDropDown();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "This command is not available.")
                {
                    MessageBox.Show("DataFields, Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void wdTemplate_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                //if ((oCurDoc != null))
                //{
                //    //RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent 
                //    //RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked 
                //    //UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick for oWordApp") 
                //    //  Marshal.FinalReleaseComObject(oWordApp);
                //    try
                //    {
                //        oCurDoc.Application.WindowSelectionChange -= DDLCBEvent;
                //    }
                //    catch
                //    {
                //    }
                     
                //}
                if ((oCurDoc != null))
                {
                    //' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit 
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
               
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

        private void wdTemplate_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            if ((oWordApp != null))
            {
                //oWordApp.WindowSelectionChange -= DDLCBEvent;
                //oWordApp.WindowBeforeDoubleClick -= OnFormClicked;
                //foreach (Wd.RecentFile oFile in oWordApp.RecentFiles)
                //{
                //    //if (oFile.Path == Application.StartupPath + "\\Temp")
                //    //{
                //    //    oFile.Delete();
                //    //}
                //    if (oFile.Path == gloSettings.FolderSettings.AppTempFolderPath)
                //    {
                //        oFile.Delete();
                //    }
                //}
                oWordApp.WindowSelectionChange -= DDLCBEvent;
                oWordApp.WindowBeforeDoubleClick -= oWordApp_WindowBeforeDoubleClick;
                foreach (Wd.RecentFile oFile in oWordApp.RecentFiles)
                {
                    if (oFile != null)
                    {
                        try
                        {
                            if (oFile.Path == gloSettings.FolderSettings.AppTempFolderPath)
                            {
                                try
                                {
                                    oFile.Delete();
                                }
                                catch //(Exception ex)
                                {
                                    // gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                                    // ex = null;
                                }
                            }
                        }
                        catch //(Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                            //ex = null;
                        }
                    }
                }

            }
        }

        private void trvDataDictionary_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Text == "Patient Statement")
                {
                    tsb_PatientStatement.Visible = true;
                }
                else
                {
                    tsb_PatientStatement.Visible = false;
                }
            }

        }

        private void txtSearchDataDictionary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDataDictionary.Text == "")
                {
                    trvDataDictionary.SelectedNode = null;
                    trvDataDictionary.CollapseAll();
                    return;
                }

                foreach (TreeNode oParentNode in trvDataDictionary.Nodes)
                {
                    foreach (TreeNode oChildNode in oParentNode.Nodes)
                    {
                        if (oChildNode.Text.Trim().ToUpper().Contains(txtSearchDataDictionary.Text.Trim().ToUpper()) == true)
                        {
                            oParentNode.Expand();
                            trvDataDictionary.SelectedNode = oChildNode;
                            return;
                        }
                        else
                        {
                            oParentNode.Collapse();
                            trvDataDictionary.SelectedNode = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchDataDictionary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // KeyChar = 8 for EnterKey //
            {
                trvDataDictionary.Select();
            }
            else
            {
                trvDataDictionary.SelectedNode = trvDataDictionary.Nodes[0].Nodes[0];
            }
        }

        private void trvDataDictionary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) // KeyChar = 8 for BackSpace //
            {
                txtSearchDataDictionary.Select();
            }
            else if (e.KeyChar == 13) // KeyChar = 8 for EnterKey //
            {
                trvDataDictionary_DoubleClick(null, null);
            }
        }

        private void frmSetupTemplateGallery_Shown(object sender, EventArgs e)
        {
            txtTemplateName.Focus();
        }












    }
}
