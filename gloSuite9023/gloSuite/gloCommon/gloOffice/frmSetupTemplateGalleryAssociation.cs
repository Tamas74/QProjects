using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloOffice
{

    #region " Association Enumerations "

    public enum AssociationCategories
    {
        //**When you add AssociationCategories to enum please also add that Association 
        //**category to Associatees Combo 
        CheckIn = 1,
        Copay = 2,
        Coverage = 3,
        AdvancePayment = 4,
        AppointmentPrint = 5,
        PatientReceipt = 6,
        AppointmentLetters = 7,
        UnscheduledReminder = 8,
        ViewApptHistory = 9,
        OpioidAgreement = 10
    }

    #endregion " Association Enumerations "

    public partial class frmSetupTemplateGalleryAssociation : Form
    {

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnectionString = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _clinicId = 0;
        private string _clinicName = "";
        private Int64 _userId = 0;
        private string _userName = "";
        private bool _isTreeLoading = false;
        private bool _isComboLoading = false;

        #endregion " Variable Declarations "

        #region " Property Procedures Declarations "

        public string DatabaseConnectionString
        {
            get { return _databaseConnectionString; }
            set { _databaseConnectionString = value; }
        }

        public Int64 ClinicID
        {
            get { return _clinicId; }
            set { _clinicId = value; }
        }

        public string ClinicName
        {
            get { return _clinicName; }
            set { _clinicName = value; }
        }

        public Int64 UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        #endregion " Property Procedures Declarations "

        #region " Constructor "

        public frmSetupTemplateGalleryAssociation(string DatabaseConnectionString)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 1; }
            }
            else
            { _clinicId = 1; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _databaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _databaseConnectionString = "";
                }
            }
            else
            {
                _databaseConnectionString = "";
            }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _userId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _userId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _userName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _userName = "";
            }

            #endregion

            _databaseConnectionString = DatabaseConnectionString;

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
        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupTemplateGalleryAssociation_Load(object sender, EventArgs e)
        {
            try
            {
                FillAssociationCategoryCombo();
                FillCategoryTemplateTree();





                if (cmbAssociates.Text.Trim() != "")
                {
                    LoadAssociation(((AssociationCategories)cmbAssociates.SelectedValue));

                    //LoadAssociation(((AssociationCategories)cmbAssociates.SelectedItem));

                    if (cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.CheckIn.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.PatientReceipt.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.AppointmentLetters.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.ViewApptHistory.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.UnscheduledReminder.GetHashCode())
                    {
                        trvCategoryTemplates.CheckBoxes = false;
                        tsb_Clear.Visible = false;
                        tsb_SelectAll.Visible = false;
                    }
                    trvCategoryTemplates.ExpandAll();
                    trvCategoryTemplates.Focus();
                }

                //  trvCategoryTemplates.CollapseAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion " Form Load "

        #region " Private & Public Methods "

        private void FillCategoryTemplateTree()
        {
            #region " Local Variable Declarations "

            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            DataTable _dtCategories = null;
            DataTable _dtCatTemplates = null;
            TreeNode _CategoryNode = null;
            TreeNode _CategoryTemplateNode = null;
            string _categoryName = "";
            string _templateName = "";
            Int64 _templateId = 0;
            Int64 _templateProviderId = 0;
            string _templateProviderName = "";

            #endregion

            try
            {
                _isTreeLoading = true;

                _dtCategories = ogloTemplate.GetTemplateCategoryList();
                if (_dtCategories != null && _dtCategories.Rows.Count > 0)
                {
                    for (int catIndex = 0; catIndex < _dtCategories.Rows.Count; catIndex++)
                    {
                        if (_dtCategories.Rows[catIndex]["CategoryName"].ToString().Trim() != "MIS Reports")
                        {
                            _categoryName = Convert.ToString(_dtCategories.Rows[catIndex]["CategoryName"]);

                            #region " Add Category Node to Tree "

                            //Check for bug no: 5189 (should not add blank Parent node)
                            if (_categoryName != null && _categoryName != DBNull.Value.ToString() && _categoryName.Trim() != "")
                            {
                                _CategoryNode = new TreeNode();
                                _CategoryNode.Name = _categoryName;
                                _CategoryNode.Text = _categoryName;
                                _CategoryNode.ImageIndex = 0;
                                _CategoryNode.SelectedImageIndex = 0;
                                _CategoryNode.Tag = _categoryName.ToUpper();
                                trvCategoryTemplates.Nodes.Add(_CategoryNode);

                            }

                            #endregion " Add Category Node to Tree "

                            if (_categoryName != null && _categoryName != DBNull.Value.ToString() && _categoryName.Trim() != "")
                            {
                                _dtCatTemplates = ogloTemplate.GetTemplates(_categoryName);

                                if (_dtCatTemplates != null && _dtCatTemplates.Rows.Count > 0)
                                {
                                    for (int temlateIndex = 0; temlateIndex < _dtCatTemplates.Rows.Count; temlateIndex++)
                                    {
                                        _templateId = Convert.ToInt64(_dtCatTemplates.Rows[temlateIndex]["nTemplateID"]);
                                        _templateName = Convert.ToString(_dtCatTemplates.Rows[temlateIndex]["sTemplateName"]);
                                        _templateProviderId = Convert.ToInt64(_dtCatTemplates.Rows[temlateIndex]["nProviderID"]);
                                        _templateProviderName = Convert.ToString(_dtCatTemplates.Rows[temlateIndex]["sProviderName"]);

                                        #region  " Add Template Node to Category Node "

                                        if (_templateId > 0)
                                        {
                                            //Check for adding child node (should not add blank child node): bug no-5189 
                                            if (_templateName != null && _templateName != DBNull.Value.ToString() && _templateName.Trim() != "")
                                            {
                                                _CategoryTemplateNode = new TreeNode();
                                                _CategoryTemplateNode.Name = _templateName;
                                                _CategoryTemplateNode.Text = _templateName;
                                                _CategoryTemplateNode.Tag = _templateId;
                                                _CategoryTemplateNode.ImageIndex = 0;
                                                _CategoryTemplateNode.SelectedImageIndex = 0;
                                                if (_CategoryNode != null) { _CategoryNode.Nodes.Add(_CategoryTemplateNode); }
                                                _CategoryTemplateNode = null;
                                            }
                                        }

                                        #endregion

                                        _templateId = 0; _templateName = ""; _templateProviderId = 0; _templateProviderName = "";
                                    }
                                }
                            }
                            _CategoryNode = null;
                            _categoryName = "";
                        }
                    }

                    trvCategoryTemplates.ExpandAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (_dtCategories != null) { _dtCategories.Dispose(); }
                if (_dtCatTemplates != null) { _dtCatTemplates.Dispose(); }
                if (_CategoryNode != null) { _CategoryNode = null; }
                if (_CategoryTemplateNode != null) { _CategoryTemplateNode = null; }
                _isTreeLoading = false;
            }
        }

        private void FillAssociationCategoryCombo()
        {
            try
            {
                _isComboLoading = true;




                //** Whene add Item to cmbAssociates combo make sure that 
                //** you add the item to AssociationCategories Enumerations declared above


                //cmbAssociates.Items.Add(AssociationCategories.CheckIn);
                ////cmbAssociates.Items.Add(AssociationCategories.Copay);
                ////cmbAssociates.Items.Add(AssociationCategories.Coverage);
                ////cmbAssociates.Items.Add(AssociationCategories.AdvancePayment);
                //cmbAssociates.Items.Add(AssociationCategories.AppointmentPrint);
                //cmbAssociates.Items.Add(AssociationCategories.PatientReceipt);
                ////SHUBHANGI 
                ////Added By Shweta 20101009
                //cmbAssociates.Items.Add(AssociationCategories.AppointmentLetters);
                ////Shweta



                //-------

                ArrayList _Associateslist = new ArrayList();
                _Associateslist.Add(new AddValue("Check In", AssociationCategories.CheckIn));
                _Associateslist.Add(new AddValue("Appointment Print", AssociationCategories.AppointmentPrint));
                _Associateslist.Add(new AddValue("Patient Receipt", AssociationCategories.PatientReceipt));
                _Associateslist.Add(new AddValue("Appointment Letters", AssociationCategories.AppointmentLetters));
                _Associateslist.Add(new AddValue("Unscheduled-Reminder", AssociationCategories.UnscheduledReminder));
                _Associateslist.Add(new AddValue("Opioid Agreement", AssociationCategories.OpioidAgreement));
                this.cmbAssociates.DataSource = _Associateslist;
                this.cmbAssociates.DisplayMember = "Display";
                this.cmbAssociates.ValueMember = "Value";
                //---x----


                if (cmbAssociates.Items.Count > 0)
                {
                    cmbAssociates.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _isComboLoading = false;
            }
        }

        private bool ValidateAssociation()
        {
            bool _isValid = true;

            try
            {
                if (cmbAssociates.Text.Trim() == "")
                {
                    MessageBox.Show("Select the association category", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbAssociates.Focus();
                    _isValid = false;
                }
                else
                {
                    if (cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.CheckIn.GetHashCode())
                    {
                        bool _isNodeChecked = false;
                        foreach (TreeNode oParentNode in trvCategoryTemplates.Nodes)
                        {
                            if (oParentNode.Checked == true) { _isNodeChecked = true; break; }
                            else
                            {
                                if (oParentNode.Nodes != null && oParentNode.Nodes.Count > 0)
                                {
                                    foreach (TreeNode oChild in oParentNode.Nodes)
                                    { if (oChild.Checked == true) { _isNodeChecked = true; break; } }
                                    if (_isNodeChecked) { break; }
                                }
                            }
                        }
                        if (_isNodeChecked == false)
                        {
                            MessageBox.Show("Select the Template to associate", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValid = false;
                            trvCategoryTemplates.Focus();
                        }
                    }
                    else if (trvCategoryTemplates.CheckBoxes == true)
                    {
                        bool _isNodeChecked = false;
                        foreach (TreeNode oParentNode in trvCategoryTemplates.Nodes)
                        {
                            if (oParentNode.Checked == true) { _isNodeChecked = true; break; }
                            else
                            {
                                if (oParentNode.Nodes != null && oParentNode.Nodes.Count > 0)
                                {
                                    foreach (TreeNode oChild in oParentNode.Nodes)
                                    { if (oChild.Checked == true) { _isNodeChecked = true; break; } }
                                    if (_isNodeChecked) { break; }
                                }
                            }
                        }
                        if (_isNodeChecked == false)
                        {
                            MessageBox.Show("Select the Template to associate", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValid = false;
                            trvCategoryTemplates.Focus();
                        }

                    }
                    else
                    {
                        //Shubhangi 20091230
                        //Change the condition trvSelectedCategory for bug no: 4864.
                        //if (trvCategoryTemplates.SelectedNode != trvCategoryTemplates.SelectedNode.Parent  && trvCategoryTemplates.SelectedNode.Level != 1)
                        if (trvCategoryTemplates.SelectedNode.Level == 0)
                        {
                            MessageBox.Show("Select the Template to associate", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValid = false;
                            trvCategoryTemplates.Focus();
                        }
                    }
                }

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                _isValid = false;
                //ex.ToString();
                //ex = null;
            }

            return _isValid;
        }

        //private void LoadAssociation(AssociationCategories associationCategory)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    DataTable _dtAssociation = null;
        //    string _sqlQuery = "";
        //    Int64 _categoryId = 0;
        //    string _categoryName = "";
        //    Int64 _templateId = 0;

        //    try
        //    {
        //        oDB.Connect(false);

        //        _sqlQuery = " SELECT DISTINCT ISNULL(TemplateGallery_Association.nTemplateCategoryID,0) AS nTemplateCategoryID, " +
        //        " ISNULL(TemplateGallery_Association.nTemplateID,0) AS nTemplateID,ISNULL(TemplateGallery_Association.nProviderID,0) AS nProviderID,  " +
        //        " ISNULL(TemplateGallery_Association.sTemplateCategoryName,'') AS sDescription " +
        //        " FROM   TemplateGallery_Association " +
        //        " WHERE (TemplateGallery_Association.nClinicID = " + this.ClinicID + ") "+
        //        " AND (TemplateGallery_Association.nAssociatedCategoryID = " + associationCategory.GetHashCode() + ")";

        //        oDB.Retrive_Query(_sqlQuery, out _dtAssociation);
        //        oDB.Disconnect();

        //        if (_dtAssociation != null && _dtAssociation.Rows.Count > 0)
        //        {
        //            bool _isfound = false;

        //            for (int rowIndex = 0; rowIndex < _dtAssociation.Rows.Count; rowIndex++)
        //            {
        //                _categoryId = Convert.ToInt64(_dtAssociation.Rows[rowIndex]["nTemplateCategoryID"]);
        //                _categoryName = Convert.ToString(_dtAssociation.Rows[rowIndex]["sDescription"]);
        //                _templateId = Convert.ToInt64(_dtAssociation.Rows[rowIndex]["nTemplateID"]);
        //                _isfound = false;

        //                foreach (TreeNode CategoryNode in trvCategoryTemplates.Nodes)
        //                {
        //                    if (Convert.ToString(CategoryNode.Tag) == _categoryName.ToString().ToUpper())
        //                    {
        //                        foreach (TreeNode TemplateNode in CategoryNode.Nodes)
        //                        {
        //                            if (Convert.ToInt64(TemplateNode.Tag) == _templateId)
        //                            {
        //                                TemplateNode.Checked = true;
        //                                if (cmbAssociates.SelectedItem.GetHashCode() != AssociationCategories.CheckIn.GetHashCode())
        //                                { trvCategoryTemplates.SelectedNode = TemplateNode; }
        //                                _isfound = true;
        //                                break;
        //                            }
        //                        }
        //                        if (_isfound) { break; }
        //                    }
        //                }

        //                _categoryId = 0;
        //                _categoryName = "";
        //                _templateId = 0;

        //            }
        //        }

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    { dbEx.ERROR_Log(dbEx.ToString()); }
        //    catch (Exception ex)
        //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (_dtAssociation != null) { _dtAssociation.Dispose(); }
        //    }

        //}

        private void LoadAssociation(AssociationCategories associationCategory)
        {
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            DataTable _dtAssociation = null;

            Int64 _categoryId = 0;
            string _categoryName = "";
            Int64 _templateId = 0;
            bool _ntopCategory = false;
            try
            {
                _dtAssociation = ogloTemplate.GetAssociation(associationCategory);

                if (_dtAssociation != null && _dtAssociation.Rows.Count > 0)
                {
                    bool _isfound = false;

                    for (int rowIndex = 0; rowIndex < _dtAssociation.Rows.Count; rowIndex++)
                    {

                        _categoryId = Convert.ToInt64(_dtAssociation.Rows[rowIndex]["nTemplateCategoryID"]);
                        _categoryName = Convert.ToString(_dtAssociation.Rows[rowIndex]["sDescription"]);
                        _templateId = Convert.ToInt64(_dtAssociation.Rows[rowIndex]["nTemplateID"]);
                        _isfound = false;

                        foreach (TreeNode CategoryNode in trvCategoryTemplates.Nodes)
                        {
                            if (Convert.ToString(CategoryNode.Tag) == _categoryName.ToString().ToUpper())
                            {
                                foreach (TreeNode TemplateNode in CategoryNode.Nodes)
                                {
                                    if (Convert.ToInt64(TemplateNode.Tag) == _templateId)
                                    {

                                        TemplateNode.Checked = true;

                                        if (_ntopCategory == false)
                                            trvCategoryTemplates.SelectedNode = TemplateNode;
                                        _ntopCategory = true;

                                        if (Convert.ToInt32(_dtAssociation.Rows[rowIndex]["bIsDefault"]) == 1)
                                        {
                                            TemplateNode.ToolTipText = "Default";

                                            if (cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.CheckIn.GetHashCode())
                                            {
                                                trvCategoryTemplates.SelectedNode = TemplateNode;
                                            }
                                        }



                                        _isfound = true;

                                        break;

                                    }
                                }
                                if (_isfound) { break; }
                            }
                        }

                        _categoryId = 0;
                        _categoryName = "";
                        _templateId = 0;

                    }
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (_dtAssociation != null) { _dtAssociation.Dispose(); }
            }

        }

        private void DeleteAssociation(int AssociatedCategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "DELETE FROM TemplateGallery_Association WHERE nAssociatedCategoryID = " + AssociatedCategoryID + " " +
                " AND nClinicID = " + this.ClinicID + " ";
                oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private bool SaveTemplateAssociation()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            StringBuilder Builder = new StringBuilder();
            string _sqlQuery = "";
            Int64 _categoryId = 0;
            string _categoryName = "";
            Int64 _templateId = 0;
            Int64 _providerId = 0;
            bool _isAssociationSaved = false;
            int _Default = 0;

            try
            {
                oDB.Connect(false);

                if (cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.CheckIn.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.PatientReceipt.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.AppointmentLetters.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.UnscheduledReminder.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.OpioidAgreement.GetHashCode())
                {
                    foreach (TreeNode oParentNode in trvCategoryTemplates.Nodes)
                    {
                        _categoryName = oParentNode.Text;

                        if (oParentNode.Nodes != null && oParentNode.Nodes.Count > 0)
                        {
                            foreach (TreeNode oChild in oParentNode.Nodes)
                            {
                                if (oChild.Checked == true)
                                {
                                    _templateId = Convert.ToInt64(oChild.Tag);
                                    if (oChild.ToolTipText == "Default")
                                    {
                                        _Default = 1;
                                    }
                                    else
                                    {
                                        _Default = 0;
                                    }
                                    _sqlQuery = "INSERT INTO TemplateGallery_Association(nAssociatedCategoryID, nTemplateID, nTemplateCategoryID,sTemplateCategoryName, nClinicID, nProviderID,bIsDefault) " +
                                    " VALUES(" + cmbAssociates.SelectedValue.GetHashCode() + "," + _templateId + "," + _categoryId + ",'" + _categoryName.Replace("'", "''") + "'," + this.ClinicID + "," + _providerId + "," + _Default + ");";
                                    Builder.Append(_sqlQuery);
                                    //oDB.Execute_Query(_sqlQuery);
                                }
                            }
                        }
                    }
                }
                else
                {

                    _categoryName = trvCategoryTemplates.SelectedNode.Parent.Text;
                    _templateId = Convert.ToInt64(trvCategoryTemplates.SelectedNode.Tag);
                    _sqlQuery = "INSERT INTO TemplateGallery_Association(nAssociatedCategoryID, nTemplateID, nTemplateCategoryID,sTemplateCategoryName, nClinicID, nProviderID) " +
                    " VALUES(" + cmbAssociates.SelectedValue.GetHashCode() + "," + _templateId + "," + _categoryId + ",'" + _categoryName.Replace("'", "''") + "'," + this.ClinicID + "," + _providerId + ");";
                    Builder.Append(_sqlQuery);
                    //oDB.Execute_Query(_sqlQuery);

                }
                //code added to minimize database trips and improve performance 6020
                oParameters.Add("@strSQL", Builder.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_SaveTemplateAssociation", oParameters);
                _isAssociationSaved = true;
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (Builder != null) { Builder = null; }
            }
            return _isAssociationSaved;
        }


        private bool SaveAsDefault(Int64 AssCategoryId, Int64 TemplateId, string CategoryName, Int64 CategoryId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string _sqlQuery = "";
            Int64 _AsscategoryId = AssCategoryId;
            string _categoryName = CategoryName;
            Int64 _templateId = 0;
            Int64 _providerId = 0;
            bool _isAssociationSaved = false;
            Int64 _categoryId = CategoryId;

            try
            {

                _templateId = Convert.ToInt64(TemplateId);

                oDB.Connect(false);
                object _Ret = null;
                bool _RetInsert = true;
                _sqlQuery = "SELECT COUNT(*) as nCount FROM TemplateGallery_Association WHERE nAssociatedCategoryID=" + cmbAssociates.SelectedValue.GetHashCode() + " AND nTemplateID = " + _templateId + "";
                _Ret = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_Ret != null && _Ret.ToString().Trim() != null && _Ret.ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt64(_Ret.ToString().Trim()) > 0)
                    {
                        _RetInsert = false;
                    }
                }
                _Ret = null;


                if (_RetInsert == false)
                {
                    _sqlQuery = "Update TemplateGallery_Association SET bIsDefault=1 WHERE nAssociatedCategoryID = " + cmbAssociates.SelectedValue.GetHashCode() + " AND nTemplateID =" + _templateId + " AND nClinicID= " + this.ClinicID + "";
                }
                else
                {
                    _sqlQuery = "INSERT INTO TemplateGallery_Association(nAssociatedCategoryID, nTemplateID, nTemplateCategoryID,sTemplateCategoryName, nClinicID, nProviderID,bIsDefault) " +
                    " VALUES(" + cmbAssociates.SelectedValue.GetHashCode() + "," + _templateId + "," + _categoryId + ",'" + _categoryName + "'," + this.ClinicID + "," + _providerId + ",1)";
                }
                oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();
                _isAssociationSaved = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _isAssociationSaved;
        }

        #endregion " Private & Public Methods "

        #region " ToolStrip Button Click Events "

        private void tsb_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvCategoryTemplates.Nodes != null && trvCategoryTemplates.Nodes.Count > 0)
                {
                    if (cmbAssociates.SelectedValue != null)
                    {
                        foreach (TreeNode oNode in trvCategoryTemplates.Nodes)
                        { oNode.Checked = false; }
                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tsb_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvCategoryTemplates.Nodes != null && trvCategoryTemplates.Nodes.Count > 0)
                {
                    if (cmbAssociates.SelectedValue != null)
                    {
                        foreach (TreeNode oNode in trvCategoryTemplates.Nodes)
                        { oNode.Checked = true; }
                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvCategoryTemplates.Nodes != null && trvCategoryTemplates.Nodes.Count > 0)
                {
                    if (cmbAssociates.SelectedValue != null)
                    {
                        DialogResult _dlgRst = DialogResult.None;
                        _dlgRst = MessageBox.Show("Are you sure you want to delete previously saved association?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (_dlgRst == DialogResult.Yes)
                        {
                            //if (cmbAssociates.SelectedItem != null)
                            //{
                            DeleteAssociation(cmbAssociates.SelectedValue.GetHashCode());
                            foreach (TreeNode oNode in trvCategoryTemplates.Nodes)
                            { oNode.Checked = false; }
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

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (ValidateAssociation())
            {
                DeleteAssociation(cmbAssociates.SelectedValue.GetHashCode());
                //gloAuditTrail.gloAuditTrail.UpdatePILog("start");   
                if (SaveTemplateAssociation() == true)
                {
                    //gloAuditTrail.gloAuditTrail.UpdatePILog("end");  
                    MessageBox.Show("Association saved successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " ToolStrip Button Click Events "

        #region " Tree View Events "

        private void trvCategoryTemplates_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (_isTreeLoading == false)
                {
                    this.trvCategoryTemplates.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvCategoryTemplates_AfterCheck);
                    if (e.Node != null)
                    {
                        if (e.Node.Level == 0)
                        {
                            foreach (TreeNode oChildNode in e.Node.Nodes)
                            {
                                oChildNode.Checked = e.Node.Checked;
                            }
                        }
                        else if (e.Node.Level == 1)
                        {
                            bool _CheckValue = true;

                            foreach (TreeNode oChildNode in e.Node.Parent.Nodes)
                            {
                                if (_CheckValue != oChildNode.Checked)
                                {
                                    _CheckValue = false;
                                    break;
                                }
                            }
                            e.Node.Parent.Checked = _CheckValue;
                        }
                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (_isTreeLoading == false)
                { this.trvCategoryTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCategoryTemplates_AfterCheck); }
            }
        }

        #endregion " Tree View Events "

        #region " Form Control Events "

        private void cmbAssociates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (_isComboLoading == false)
                {
                    tsb_Clear.Visible = true;
                    tsb_SelectAll.Visible = true;
                    tsb_Default.Visible = true;
                    trvCategoryTemplates.CheckBoxes = true;

                    tsb_Clear_Click(null, null);
                    if (cmbAssociates.Text.Trim() != "")
                    {
                        trvCategoryTemplates.ExpandAll();
                        trvCategoryTemplates.Focus();
                        LoadAssociation(((AssociationCategories)cmbAssociates.SelectedValue));

                        //LoadAssociation(((AssociationCategories)cmbAssociates.SelectedItem));
                        if (cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.CheckIn.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.PatientReceipt.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.AppointmentLetters.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.UnscheduledReminder.GetHashCode() && cmbAssociates.SelectedValue.GetHashCode() != AssociationCategories.OpioidAgreement.GetHashCode())
                        {
                            trvCategoryTemplates.CheckBoxes = false;
                            tsb_Clear.Visible = false;
                            tsb_SelectAll.Visible = false;
                        }
                        if (cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.CheckIn.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.AppointmentPrint.GetHashCode() || cmbAssociates.SelectedValue.GetHashCode() == AssociationCategories.AppointmentLetters.GetHashCode())
                        {
                            tsb_Default.Visible = false;
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

        #endregion " Form Control Events "

        private void tsb_Default_Click(object sender, EventArgs e)
        {

            Int64 _templateId = 0;
            //Boolean IsSaved = false;

            if (cmbAssociates.SelectedValue != null && cmbAssociates.SelectedValue.ToString() != "")
            {

                foreach (TreeNode oParentNode in trvCategoryTemplates.Nodes)
                {

                    if (oParentNode.Nodes != null && oParentNode.Nodes.Count > 0)
                    {
                        foreach (TreeNode oChild in oParentNode.Nodes)
                        {
                            if (oChild.Checked == true)
                            {
                                _templateId = Convert.ToInt64(oChild.Tag);

                            }

                            if (oChild.IsSelected)
                            {
                                oChild.ToolTipText = "Default";
                            }
                            else
                            {
                                oChild.ToolTipText = "";
                            }
                        }


                    }
                }

            }
            else
            {
                MessageBox.Show("Select the template type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }

    public class AddValue
    {
        private String m_Display;
        private AssociationCategories m_Value;
        public AddValue(String Display, AssociationCategories Value)
        {
            m_Display = Display;
            m_Value = Value;
        }
        public String Display
        {
            get { return m_Display; }
        }
        public AssociationCategories Value
        {
            get { return m_Value; }
        }
    }
}
