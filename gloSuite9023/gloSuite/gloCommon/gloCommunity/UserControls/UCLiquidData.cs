using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Net;

namespace gloCommunity.UserControls
{
    public partial class UCLiquidData : UserControl
    {
        clsGeneral.ControlType enumControlType = default(clsGeneral.ControlType);
        string _selectedCategory = "";
        string _strAction = "";
        string LiquidDataSRV = clsGeneral.gstrLiquidDataFNm;
        gloLists.Lists gloList;
        DataTable dtGlobalMerge = new DataTable();
        public bool IsClinicRepository = true;//check IsClinicRepository flag while Show Liquid Data from LiquidDataXML (gloCommunityDownload only)
        public UCLiquidData(string strAction)
        {
            _strAction = strAction;
            InitializeComponent();
        }

        private void UCLiquidData_Load(object sender, EventArgs e)
        {
            FillLiquidDataType();
            FillLiquidDataCategory();
            dgTableField.ReadOnly = true;
            FillControlType();
            dgItemList.Columns[2].Visible = false;
            FillControlType();

            if (_strAction == "Upload")
                FillLiquidData();
            else
            {
                tlbClinicRepository_Click(null, null);
            }
        }

        private void trvDiscrete_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void FillLiquidData()
        {
            myTreeNode trvRootNode = default(myTreeNode);
            myTreeNode trvChildNode = default(myTreeNode);
            clsLiquidData_Upload objLiquidData = new clsLiquidData_Upload();
            //'User defined tree node to hold custom values

            try
            {
                //'Get the Liquid data  for binding it to Treeview
                DataTable objData = objLiquidData.GetLiquidData();
                if ((objData != null))
                {
                    var _with1 = trvDiscrete;
                    _with1.Nodes.Clear();
                    //'Add the default tree node
                    trvRootNode = new myTreeNode();
                    trvRootNode.Text = "Liquid Data";
                    trvRootNode.ImageIndex = 0;
                    trvRootNode.SelectedImageIndex = 0;
                    _with1.Nodes.Add(trvRootNode);
                    //'Add the Data fileds under the parent node
                    for (Int32 _inc = 0; _inc <= objData.Rows.Count - 1; _inc++)
                    {
                        // trvChildNode = New myTreeNode
                        Int64 _Flag = 0;
                        //'check for Mandatory field and set the flag
                        if (Convert.ToBoolean(objData.Rows[_inc]["bIsMandatory"]) == true)
                        {
                            _Flag = 1;
                        }
                        //'Key Refers to Element Id, Strname refers to Element name, _Flag refers to IsMandatory 
                        string _strElementName = "";

                        if (objData.Rows[_inc]["sElementName"].ToString().Trim().Contains("˜"))
                            _strElementName = objData.Rows[_inc]["sElementName"].ToString().Trim().Replace('˜', '≈');
                        else
                            _strElementName = objData.Rows[_inc]["sElementName"].ToString().Trim();

                        trvChildNode = new myTreeNode(_strElementName, Convert.ToInt64(objData.Rows[_inc]["nElementID"]), _Flag);
                        trvChildNode.ImageIndex = 2;
                        trvChildNode.SelectedImageIndex = 2;
                        trvChildNode.NodeName = objData.Rows[_inc]["sElementType"].ToString();
                        trvRootNode.Nodes.Add(trvChildNode);
                        trvChildNode = null;
                    }
                    // .SelectedNode = trvRootNode

                    _with1.ExpandAll();
                    //trvDiscrete.SelectedNode = trvDiscrete.Nodes(0)
                }
                TreeNode n = trvDiscrete.Nodes[0];

                trvDiscrete.SelectedNode = n;
                trvDiscrete.Focus();

            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //clsGeneral.UpdateLog("glocomm Error While Filling Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                trvRootNode = null;
                objLiquidData = null;
            }
        }

        private void FillLiquidDataType()
        {
            var _with1 = cmbDataType.Items;
            //'Clear the items before adding the items into combobox
            _with1.Clear();
            _with1.Add(" ");
            _with1.Add("Boolean");
            _with1.Add("Single Selection");
            _with1.Add("Multiple Selection");
            //.Add("Linked Selection")
            _with1.Add("Text");
            _with1.Add("Table");
            _with1.Add("Group");
            //'Select the first Item 
            cmbDataType.SelectedIndex = 0;
        }

        public void FillLiquidDataCategory()
        {
            var _with1 = cmbFieldCategory.Items;
            _with1.Clear();
            _with1.Add(" ");
            _with1.Add("General");
            _with1.Add("History");
            _with1.Add("Physical Examination");
            _with1.Add("Medical Decision-Making");
            _with1.Add("HPI");
            _with1.Add("Management option");
            _with1.Add("Labs");
            _with1.Add("X-Ray/Radiology");
            _with1.Add("Other Diagonsis Tests");
            _with1.Add("ROS");
            //'Select the first Item 
            cmbFieldCategory.SelectedIndex = 0;
        }

        private void SetDataUpload(myTreeNode SelectedNode)
        {
            long m_ElementId = 0;
            clsLiquidData_Upload objLiquidData = new clsLiquidData_Upload();
            try
            {
                if (txtItem.Enabled == false)
                {
                    txtItem.Enabled = false;
                }
                if (dgItemList.Rows.Count < 2)
                {
                    txtItem.Enabled = false;
                }



                if (SelectedNode.Text != "Liquid Data")
                {
                    //if (IsfrmExam == false)
                    //{
                    m_ElementId = SelectedNode.Key;
                    //}

                    if (m_ElementId == 0)
                    {
                        if (trvDiscrete.SelectedNode == null)
                        {
                            return;
                        }

                        if (trvDiscrete.SelectedNode.Level == 0)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    //Commented for fixed Bug # : 29219 on 20120704
                    //MessageBox.Show("Select record to modify", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //pnl_ToolStrip.Visible = false;
                //blnModify = true;

                txtcategory.Enabled = false;
                txtCatItem.Enabled = false;
                CmbControl.Enabled = false;
                ResetEditPanel();
                DataTable objdata = null;

                objdata = objLiquidData.GetDataField(m_ElementId);

                if ((objdata != null))
                {
                    dgTableField.Rows.Clear();

                    for (Int32 _inc = 0; _inc <= objdata.Rows.Count - 1; _inc++)
                    {
                        if (Convert.ToInt64(objdata.Rows[_inc]["nGroupID"]) == 0)
                        {
                            txtField.Text = objdata.Rows[_inc]["sElementName"].ToString();
                            cmbDataType.Text = objdata.Rows[_inc]["sElementType"].ToString();
                            if (cmbDataType.Text == "Multiple Selection")
                            {
                                dgItemList.Columns[2].Visible = true;
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }
                            else
                            {
                                dgItemList.Columns[2].Visible = false;
                            }

                            if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.None.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.None.GetHashCode();

                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.General.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.General.GetHashCode();

                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Hitory.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Hitory.GetHashCode();

                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Physical_Examination.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Physical_Examination.GetHashCode();

                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Medical_Decision_Making.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Medical_Decision_Making.GetHashCode();
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.HPI.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.HPI.GetHashCode();
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Management_option.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Management_option.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Labs.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Labs.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.X_Ray_Radiology.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.X_Ray_Radiology.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.Other_Diagonsis_Tests.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Other_Diagonsis_Tests.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (Convert.ToInt32(objdata.Rows[_inc]["sCategoryName"]) == clsGeneral.CategoryType.ROS.GetHashCode())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.ROS.GetHashCode();
                                fillcombobox();
                            }

                            chckRequired.Checked = Convert.ToBoolean(objdata.Rows[_inc]["bIsMandatory"]);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    for (Int32 _inc = 0; _inc <= objdata.Rows.Count - 1; _inc++)
                    {
                        if (Convert.ToInt64(objdata.Rows[_inc]["nGroupID"]) != 0)
                        {
                            //'The SubItems are to be binded to the list
                            //'If the datatype is of text then no need to show list box

                            if (objdata.Rows[_inc]["sElementType"].ToString() == "Text")
                            {
                                ValidateDatatype(objdata.Rows[_inc]["sElementType"].ToString());
                                txtItem.Text = objdata.Rows[_inc]["sElementName"].ToString();
                                pnlFieldValues.Dock = DockStyle.Top;

                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Table" | objdata.Rows[_inc]["sElementType"].ToString() == "Group")
                            {
                                ValidateDatatype(objdata.Rows[_inc]["sElementType"].ToString());
                                if (!string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociateditem"].ToString()))
                                {
                                    chkAssociateStd.Visible = true;
                                    chkAssociateStd.Checked = true;
                                    pnlStandardEM.Visible = true;
                                    dgTableField.Columns[5].Visible = true;
                                    dgTableField.Columns[6].Visible = true;
                                }
                                else
                                {
                                    chkAssociateStd.Checked = false;
                                    chkAssociateStd.Visible = false;
                                }

                                ////((clsGeneral.ControlType)objdata.Rows[_inc]["nControlType"]).GetHashCode()
                                InsertData(objdata.Rows[_inc]["sCategoryName"].ToString(), objdata.Rows[_inc]["sItemName"].ToString(), Convert.ToInt32(objdata.Rows[_inc]["nControlType"]), objdata.Rows[_inc]["sAssociatedCategory"].ToString(), objdata.Rows[_inc]["sAssociateditem"].ToString(), objdata.Rows[_inc]["sAssociatedProperty"].ToString());
                                txtCaption.Text = objdata.Rows[_inc]["sElementName"].ToString();
                                pnlFieldValues.Dock = DockStyle.Top;
                                txtItem.Text = objdata.Rows[_inc]["sElementName"].ToString();

                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Multiple Selection")
                            {
                                var _with1 = dgItemList;
                                _with1.Rows.Add();
                                _with1[0, _with1.Rows.Count - 1].Value = ((clsGeneral.ControlType)Convert.ToInt32(objdata.Rows[_inc]["nControlType"])); //((clsGeneral.ControlType)objdata.Rows[_inc]["nControlType"]).GetHashCode().ToString();
                                _with1[1, _with1.Rows.Count - 1].Value = objdata.Rows[_inc]["sElementName"].ToString();
                                _with1[2, _with1.Rows.Count - 1].Value = ((clsGeneral.ControlType)Convert.ToInt32(objdata.Rows[_inc]["nControlType"]));
                                if (!string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociatedProperty"].ToString()))
                                {
                                    _with1[3, _with1.Rows.Count - 1].Value = objdata.Rows[_inc]["sAssociatedProperty"].ToString();
                                    _with1.Columns[3].Visible = true;
                                    chkAssociatestddata.Checked = true;
                                    chkAssociatestddata.Visible = true;
                                    pnlassociateStdItem.Visible = true;
                                    chkAssociatestddata.Enabled = false;
                                }
                                else
                                {
                                    _with1.Columns[3].Visible = false;
                                    chkAssociatestddata.Checked = false;
                                    chkAssociatestddata.Visible = false;
                                    pnlassociateStdItem.Visible = false;
                                    chkAssociatestddata.Enabled = false;
                                }
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Text" | objdata.Rows[_inc]["sElementType"].ToString() == "Boolean" | objdata.Rows[_inc]["sElementType"].ToString() == "Single Selection" | objdata.Rows[_inc]["sElementType"].ToString() == "Group")
                            {
                                dgItemList.Rows.Add(0, objdata.Rows[_inc]["sElementName"].ToString());
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean" & !string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociatedProperty"].ToString()))
                                {
                                    dgItemList[3, dgItemList.Rows.Count - 1].Value = objdata.Rows[_inc]["sAssociatedProperty"].ToString();
                                    dgItemList.Columns[3].Visible = true;
                                    chkAssociatestddata.Visible = true;
                                    chkAssociatestddata.Checked = true;

                                    pnlassociateStdItem.Visible = true;
                                }
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }

                        }
                    }
                    if (objdata.Rows.Count > 0)
                    {
                        ValidateDatatype(objdata.Rows[0]["sElementType"].ToString());
                    }
                }

                if (cmbFieldCategory.Text == "HPI" | cmbFieldCategory.Text == "Management option" | cmbFieldCategory.Text == "X-Ray/Radiology" | cmbFieldCategory.Text == "Other Diagonsis Tests" | cmbFieldCategory.Text == "ROS" | cmbFieldCategory.Text == "Labs")
                {
                    if (cmbDataType.Text == "Table" | cmbDataType.Text == "Group" | cmbDataType.Text == "Single Selection" | cmbDataType.Text == "Boolean" | cmbDataType.Text == "Text")
                    {
                        chkAssociatestddata.Visible = false;
                    }
                    else
                    {
                        chkAssociatestddata.Visible = true;
                    }

                }
                else if (cmbFieldCategory.Text == "Physical Examination")
                {
                    if (cmbDataType.Text == "Group" | cmbDataType.Text == "Table")
                    {
                        chkAssociateStd.Visible = true;
                    }
                    else
                    {
                        chkAssociatestddata.Visible = false;
                    }

                }
                else if (cmbDataType.Text == "Boolean")
                {
                    if (cmbFieldCategory.Text == "History" | cmbFieldCategory.Text == "Management option" | cmbFieldCategory.Text == "X-Ray/Radiology" | cmbFieldCategory.Text == "Other Diagonsis Tests")//| cmbAssociatedCategory.Text == "Labs"
                    {
                        chkAssociatestddata.Visible = true;
                    }
                    else
                    {
                        //Field type as 'Boolean' n Category as General
                        chkAssociatestddata.Visible = false;
                    }
                }
                else
                {
                    chkAssociateStd.Checked = false;
                    chkAssociateStd.Visible = false;
                    chkAssociatestddata.Checked = false;
                    chkAssociatestddata.Visible = false;
                }
                pnlEdit.Visible = true;
                txtField.Focus();
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //clsGeneral.UpdateLog("glocomm Error While DataUpload   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objLiquidData = null;
            }
        }

        private void ResetEditPanel()
        {
            //'Reset all the controls
            txtField.Text = string.Empty;

            txtItem.Text = string.Empty;
            dgItemList.Rows.Clear();
            //'Make Edit Buttons panel and Field values panel visible true
            pnlFieldValues.Visible = true;

            dgTableField.Rows.Clear();
            txtcategory.Text = "";
            txtCatItem.Text = "";
            txtItem.Text = "";
            CmbControl.SelectedIndex = 0;
            cmbFieldCategory.SelectedIndex = 0;
            cmbDataType.SelectedIndex = 0;
            txtCaption.Text = "";
            chckRequired.Checked = false;
            txtItem.Enabled = false;
            txtcategory.Enabled = false;
            txtCaption.Enabled = false;
            txtCatItem.Enabled = false;
            CmbControl.Enabled = false;
            if (Panel5.Visible == false)
            {
                Panel5.Visible = true;
            }
        }

        private Array SplitTextHypen(string NodeValue)
        {
            try
            {
                string[] _result = null;
                _result = NodeValue.Split('(');
                return _result;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Splliting Hypen   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
        }

        private void fillbooleancombobox()
        {

            string strprop = "";
            Type pType = null;

            //For Management Option
            if ((cmbFieldCategory.Text == "Management option"))
            {
                cmbstddata.Items.Clear();

                //Declare a variable for PropertyInfo
                PropertyInfo[] propertyInfos = null;
                propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    strprop = propertyInfo.Name;
                    pType = propertyInfo.PropertyType;


                    if (strprop == "DecisionObtainMedicalRecsOther" | strprop == "ReviewMedicalRecsOther" | strprop == "DiscussCaseWHealthProvider")
                    {
                        //Add Items in combo box
                        //'FullName = "System.Boolean"
                        if (pType.Name == "Boolean")
                        {
                            cmbstddata.Items.Add(strprop);
                        }

                    }

                }
                chkAssociatestddata.Text = "Associated Management Option";

            }
            //For Labs
            if ((cmbFieldCategory.Text == "Labs"))
            {
                cmbstddata.Items.Clear();

                PropertyInfo[] propertyInfos = null;
                propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    strprop = propertyInfo.Name;
                    pType = propertyInfo.PropertyType;

                    if (strprop == "IndependentVisualTest" | strprop == "DiscussionWPerformingPhys")
                    {
                        if (pType.Name == "Boolean")
                        {
                            cmbstddata.Items.Add(strprop);
                        }
                    }
                }

                chkAssociatestddata.Text = "Associated Labs";
            }

            //For X-Ray/Radiology
            if ((cmbFieldCategory.Text == "X-Ray/Radiology"))
            {
                cmbstddata.Items.Clear();
                PropertyInfo[] propertyInfos = null;
                propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    strprop = propertyInfo.Name;
                    pType = propertyInfo.PropertyType;


                    if (strprop == "IndependentVisualTest" | strprop == "DiscussWPerformingPhys")
                    {
                        if (pType.Name == "Boolean")
                        {
                            cmbstddata.Items.Add(strprop);
                        }
                    }

                }
                chkAssociatestddata.Text = "Associated X-Ray/Radiology";
            }

            //For Other Diagonsis Tests
            if ((cmbFieldCategory.Text == "Other Diagonsis Tests"))
            {
                cmbstddata.Items.Clear();

                PropertyInfo[] propertyInfos = null;
                propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    strprop = propertyInfo.Name;
                    pType = propertyInfo.PropertyType;

                    if (strprop == "IndependentVisualTest" | strprop == "DiscussWPerformingPhys")
                    {
                        if (pType.Name == "Boolean")
                        {
                            cmbstddata.Items.Add(strprop);
                        }
                    }

                }
                chkAssociatestddata.Text = "Associated Other Diagonsis Tests";
            }
        }

        private void fillcombobox()
        {

            try
            {
                string strprop = "";
                Type pType = null;
                //Check Combo box for Management option

                if ((cmbFieldCategory.Text == "Management option"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;
                        //Add Items in combo box
                        //'FullName = "System.Boolean"
                        if (pType.Name == "Boolean")
                        {
                            cmbstddata.Items.Add(strprop);
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard Management option";

                    //Check Combo box for Lab option

                }
                else if ((cmbFieldCategory.Text == "Labs"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;
                        //Add Items in combo box
                        //'FullName = "System.Boolean"
                        if (pType.Name == "Boolean")
                        {
                            if (strprop.EndsWith("Urgent") != true)
                            {
                                cmbstddata.Items.Add(strprop);
                            }
                        }

                    }
                    chkAssociatestddata.Text = "Associate standard Labs";

                    //Check Combo box for X-Ray/Radiology option

                }
                else if ((cmbFieldCategory.Text == "X-Ray/Radiology"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;

                        //Add Items in combo box
                        //'FullName = "System.Boolean"
                        if (pType.Name == "Boolean")
                        {
                            if (strprop.EndsWith("Urgent") != true)
                            {
                                cmbstddata.Items.Add(strprop);
                            }
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard X-Ray/Radiology";

                    //Check Combo box for OtheDiagnosisTests option

                }
                else if ((cmbFieldCategory.Text == "Other Diagonsis Tests"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;

                        //Add Items in combo box
                        //'FullName = "System.Boolean"
                        if (pType.Name == "Boolean")
                        {
                            if (strprop.EndsWith("Urgent") != true)
                            {
                                cmbstddata.Items.Add(strprop);
                            }
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard Other Diagonsis Tests";

                    //Check Combo box for ROS option
                }
                else if ((cmbFieldCategory.Text == "ROS"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;
                        if (strprop.Contains("Ros"))
                        {
                            //Add Items in combo box
                            if (pType.Name == "Boolean")
                            {
                                strprop = strprop.Substring(3);
                                //'FullName = "System.Boolean"
                                cmbstddata.Items.Add(strprop);
                            }
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard ROS";

                    //Check Combo box for HPI option
                }
                else if ((cmbFieldCategory.Text == "HPI"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;

                        pType = propertyInfo.PropertyType;
                        if (strprop.Contains("Hpi"))
                        {
                            //Add Items in combo box
                            if (pType.Name == "Boolean")
                            {
                                strprop = strprop.Substring(3);
                                //'FullName = "System.Boolean"
                                cmbstddata.Items.Add(strprop);
                            }
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard HPI";

                    //Check Combo box for History option
                }
                else if ((cmbFieldCategory.Text == "History"))
                {
                    //Clear Associated Item combo box
                    cmbstddata.Items.Clear();

                    //Declare a variable for PropertyInfo
                    PropertyInfo[] propertyInfos = null;
                    propertyInfos = typeof(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding | BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        strprop = propertyInfo.Name;
                        pType = propertyInfo.PropertyType;

                        if (strprop == "UnableComprehensiveHistory")
                        {
                            if (strprop.Contains("History"))
                            {
                                //Add Items in combo box
                                //'FullName = "System.Boolean"
                                if (pType.Name == "Boolean")
                                {
                                    cmbstddata.Items.Add(strprop);
                                }
                            }
                        }
                    }
                    chkAssociatestddata.Text = "Associate standard History";
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("glocomm Error While FillCombobox   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void ValidateDatatype(string strDataType)
        {
            //'To validate the user with necessary actions based on datatype
            switch (strDataType)
            {
                case "Boolean":
                    //'If Boolean datatype, user should be restricted to enter two values only
                    Panel2.Height = 131;
                    pnlFieldValues.Visible = true;
                    pnlFieldValues.BringToFront();
                    txtItem.Visible = true;
                    Label36.Visible = true;
                    Label3.Visible = true;
                    //'check the items in list box to warn the user for not more than two items
                    //if (blnBooleanFlag)
                    //{
                    //    if (dgItemList.Rows.Count > 2)
                    //    {
                    //        blnBooleanFlag = false;
                    //        MessageBox.Show("Boolean DataField accepts two Field Values only", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //    }
                    //}
                    //pnlTableEntry.Visible = false;
                    chkAssociatestddata.Visible = true;

                    //Panel5.Visible = false;
                    if (dgItemList.ColumnCount > 1)
                    {
                        if (cmbFieldCategory.Text == "History" | cmbFieldCategory.Text == "Management option" | cmbFieldCategory.Text == "X-Ray/Radiology" | cmbFieldCategory.Text == "Other Diagonsis Tests" | cmbFieldCategory.Text == "Labs")
                        {
                            dgItemList.Columns[2].Visible = false;
                            dgItemList.Columns[3].Visible = true;

                        }
                        else
                        {
                            dgItemList.Columns[2].Visible = false;
                            dgItemList.Columns[3].Visible = false;
                        }
                    }
                    break;
                //dgItemList.Columns.Item(2).Visible = False
                //chkAssociatestddata.Visible = False
                //pnlassociateStdItem.Visible = False
                //pnlStandardEM.Visible = False
                case "Text":
                    ////'If Text datatype - hide the Field values list and add/modify/delete buttons
                    Panel2.Height = 139;
                    pnlTableEntry.Visible = false;
                    pnlFieldValues.Visible = false;
                    Label3.Visible = true;
                    txtItem.Visible = true;
                    Label36.Visible = true;
                    //'pnlBtns.Visible = True

                    pnlFieldValues.Visible = false;
                    ////'pnlBtns.Visible = False
                    Panel5.Visible = false;
                    dgItemList.Columns[2].Visible = false;
                    chkAssociatestddata.Visible = false;
                    pnlassociateStdItem.Visible = false;
                    break;
                //pnlStandardEM.Visible = False
                case "Table":
                    Panel2.Height = 107;
                    pnlTableEntry.Visible = true;
                    pnlFieldValues.Visible = false;
                    Label3.Visible = false;
                    txtItem.Visible = false;
                    Label36.Visible = false;
                    lblcaption.Visible = false;
                    //txtcaption.visible = false;
                    //lblcontrol.visible = true;
                    //cmbcontrol.visible = true;
                    //cmbcontrol.selectedindex = 0;

                    Panel5.Visible = true;
                    if (Panel2.Controls.Contains(Panel5) == true)
                    {
                        //    //'Panel2.Controls.Remove(Panel5)
                        pnlAddCategory.Controls.Add(Panel5);
                        //    //Panel5.Location = New System.Drawing.Point(62, 92) ''  (366, 85)
                    }
                    Panel5.Location = new System.Drawing.Point(62, 92);
                    ////'  (366, 85)
                    //if (blnModify == false)
                    //{
                    //    chkAssociatestddata.Visible = false;
                    //    pnlassociateStdItem.Visible = false;
                    //    chkAssociateStd.Checked = false;
                    //    chkAssociateStd.Visible = false;
                    //}
                    chkAssociatestddata.Visible = false;
                    pnlassociateStdItem.Visible = false;
                    break;
                case "Group":
                    Panel2.Height = 107;
                    pnlTableEntry.Visible = true;
                    pnlFieldValues.Visible = false;
                    Label3.Visible = false;
                    txtItem.Visible = false;
                    Label36.Visible = false;
                    //To make lavel of txtItem field
                    //'pnlBtns.Visible = False
                    lblcaption.Visible = true;
                    txtCaption.Visible = true;

                    Panel5.Visible = true;
                    //PANEL2 IS A PANEL CONTAINS UPPER AREA NOT ANY DG
                    if (Panel2.Controls.Contains(Panel5) == true)
                    {
                        Panel2.Controls.Remove(Panel5);
                        pnlAddCategory.Controls.Add(Panel5);
                        //    //Panel5.Location = New System.Drawing.Point(62, 92) ''  (366, 85)
                    }
                    Panel5.Location = new System.Drawing.Point(62, 92);

                    //if (blnModify == false)
                    //{
                    //    chkAssociatestddata.Visible = false;
                    //    pnlassociateStdItem.Visible = false;
                    //    chkAssociateStd.Checked = false;

                    //}
                    chkAssociatestddata.Visible = false;
                    pnlassociateStdItem.Visible = false;
                    break;

                case "Single Selection":
                    Panel2.Height = 139;
                    //blnBooleanFlag = false;
                    pnlFieldValues.Visible = true;
                    ////'pnlBtns.Visible = True
                    pnlFieldValues.BringToFront();
                    pnlTableEntry.Visible = false;

                    Label3.Visible = true;
                    txtItem.Visible = true;
                    Label36.Visible = true;
                    //'pnlBtns.Visible = True

                    //Panel5.Visible = false;

                    if (dgItemList.ColumnCount > 1)
                    {
                        dgItemList.Columns[2].Visible = false;
                        dgItemList.Columns[3].Visible = false;
                    }
                    chkAssociatestddata.Visible = false;
                    //chkAssociateStd.Visible = false;
                    pnlassociateStdItem.Visible = false;
                    break;
                //pnlStandardEM.Visible = False
                default:
                    //blnBooleanFlag = false;
                    Panel2.Height = 169;
                    pnlFieldValues.Visible = true;
                    ////'pnlBtns.Visible = True
                    pnlFieldValues.BringToFront();
                    pnlTableEntry.Visible = false;

                    Label3.Visible = true;
                    txtItem.Visible = true;
                    Label36.Visible = true;
                    //'pnlBtns.Visible = True

                    Panel5.Visible = true;
                    if (pnlAddCategory.Controls.Contains(Panel5) == true)
                    {
                        pnlAddCategory.Controls.Remove(Panel5);
                        Panel2.Controls.Add(Panel5);
                        Panel5.Location = new System.Drawing.Point(61, 130);
                        //'  (366, 106)
                    }
                    dgItemList.Columns[2].Visible = true;
                    //if (blnModify == false)
                    //{
                    //    chkAssociatestddata.Visible = false;
                    //    pnlassociateStdItem.Visible = false;
                    //}
                    fillcombobox();

                    break;
            }
        }

        private void FillControlType()
        {
            var _with1 = CmbControl.Items;
            _with1.Clear();
            _with1.Add("Choose an item");
            _with1.Add("Check Box");
            _with1.Add("Text");
            //'Select the first Item 
            CmbControl.SelectedIndex = 0;
        }

        public bool InsertData(string CategoryName, string ItemName, int nSelectedIndex, string AssociateCategory, string AssociatedItem, string AssocaitedProperty = "")
        {

            try
            {
                bool IsCategoryPresent = false;
                if (nSelectedIndex == 1)
                {
                    enumControlType = clsGeneral.ControlType.CheckBox;
                }
                else if (nSelectedIndex == 2)
                {
                    enumControlType = clsGeneral.ControlType.Text;
                }
                else
                {
                    enumControlType = clsGeneral.ControlType.None;
                }

                string strProperty = "";
                //If cmbAssociatedCategory.Items.Count > 0 Then
                if (cmbFieldCategory.Text == "Physical Examination")
                {
                    //if (string.IsNullOrEmpty(AssocaitedProperty) & chkAssociateStd.Checked == true & cmbAssociateSubItem.Visible == false)
                    //{
                    //    strProperty = ((clsAssocatedLiquidData)cmbAssociatedCategory.SelectedItem).GrouppropertyId + ((clsAssocatedLiquidData)cmbAssoicatedItem.SelectedItem).ItempropertyId;
                    //}
                    //else if (string.IsNullOrEmpty(AssocaitedProperty) & chkAssociateStd.Checked == true & cmbAssociateSubItem.Visible == true)
                    //{
                    //    strProperty = ((clsAssocatedLiquidData)cmbAssociatedCategory.SelectedItem).GrouppropertyId + ((clsAssocatedLiquidData)cmbAssoicatedItem.SelectedItem).ItempropertyId + ((clsAssocatedLiquidData)cmbAssociateSubItem.SelectedItem).SubElementproperty;

                    //}
                    //else
                    //{
                    strProperty = AssocaitedProperty;
                    //}
                }
                else
                {
                    strProperty = "";
                    AssociateCategory = "";
                    AssociatedItem = "";

                }

                ///' If the grid is empty 
                if (dgTableField.RowCount == 0)
                {
                    var _with1 = dgTableField;
                    _with1.Rows.Add();
                    _with1[0, _with1.Rows.Count - 1].Value = CategoryName;
                    //' Category 
                    _with1[2, _with1.Rows.Count - 1].Value = CategoryName;
                    //' Hidden category
                    _with1[5, _with1.Rows.Count - 1].Value = AssociateCategory;
                    //' Category 
                    _with1[7, _with1.Rows.Count - 1].Value = AssociateCategory;
                    //' Hidden category
                    _with1.Rows.Add();
                    _with1[1, _with1.Rows.Count - 1].Value = ItemName;
                    //' Item
                    _with1[2, _with1.Rows.Count - 1].Value = CategoryName;
                    //' Hiddent Category
                    _with1[3, _with1.Rows.Count - 1].Value = enumControlType.GetHashCode();
                    _with1[4, _with1.Rows.Count - 1].Value = enumControlType.ToString();
                    _with1[6, _with1.Rows.Count - 1].Value = AssociatedItem;
                    //' Item
                    _with1[7, _with1.Rows.Count - 1].Value = AssociateCategory;
                    //' Hidden category
                    _with1[8, _with1.Rows.Count - 1].Value = strProperty;
                    txtCatItem.Text = "";

                }
                else
                {
                    var _with2 = dgTableField;
                    IsCategoryPresent = false;
                    int i = 0;
                    //Commented by SHUBHANGI 20090921
                    //If control type is different then allow to add same records in dgTableField 
                    //' Check for category is present
                    for (i = 0; i <= dgTableField.Rows.Count - 1; i++)
                    {
                        if (_with2[2, i].Value.ToString() == CategoryName)
                        {
                            //' Check for Item is already exists & control is already present 
                            if (_with2[1, i].Value != null && _with2[4, i].Value != null)
                            {
                                if (_with2[1, i].Value.ToString() == ItemName && _with2[4, i].Value.ToString() == enumControlType.ToString())
                                {
                                    //MessageBox.Show("Item is already present under this category", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCatItem.Focus();
                                    return false;
                                }
                            }
                            IsCategoryPresent = true;
                            ///' Find the last row of the category to insert new item at end of the category list
                            if (i != dgTableField.Rows.Count - 1)
                            {
                                if (_with2[2, i + 1].Value.ToString() != CategoryName)
                                {
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        else
                        {
                            IsCategoryPresent = false;
                        }
                    }

                    ///' If category is not present the add category and item
                    if (IsCategoryPresent == false)
                    {
                        _with2.Rows.Add();
                        _with2[0, _with2.Rows.Count - 1].Value = CategoryName;
                        _with2[2, _with2.Rows.Count - 1].Value = CategoryName;
                        _with2[5, _with2.Rows.Count - 1].Value = AssociateCategory;
                        //' Category 
                        _with2[7, _with2.Rows.Count - 1].Value = AssociateCategory;
                        //' Hidden category
                        _with2.Rows.Add();
                        _with2[1, _with2.Rows.Count - 1].Value = ItemName;
                        _with2[2, _with2.Rows.Count - 1].Value = CategoryName;
                        _with2[3, _with2.Rows.Count - 1].Value = enumControlType.GetHashCode();
                        _with2[4, _with2.Rows.Count - 1].Value = enumControlType.ToString();
                        _with2[6, _with2.Rows.Count - 1].Value = AssociatedItem;
                        //' Item
                        _with2[7, _with2.Rows.Count - 1].Value = AssociateCategory;
                        //' Hidden category
                        _with2[8, _with2.Rows.Count - 1].Value = strProperty;
                    }
                    else
                    {
                        try
                        {
                            ///' If the new row is and the end of the grid
                            if (i == _with2.Rows.Count)
                            {
                                _with2.Rows.Insert(i, 1);
                                _with2[1, i].Value = ItemName;
                                _with2[2, i].Value = CategoryName;
                                _with2[3, i].Value = enumControlType.GetHashCode();
                                _with2[4, i].Value = enumControlType.ToString();
                                _with2[6, i].Value = AssociatedItem;
                                //' Item
                                _with2[7, i].Value = AssociateCategory;
                                //' Hidden category
                                _with2[8, i].Value = strProperty;
                                ///' If the new row is in between the grid rows.
                            }
                            else
                            {
                                _with2.Rows.Insert(i + 1, 1);
                                _with2[1, i + 1].Value = ItemName;
                                _with2[2, i + 1].Value = CategoryName;
                                _with2[3, i + 1].Value = enumControlType.GetHashCode();
                                _with2[4, i + 1].Value = enumControlType.ToString();
                                _with2[6, i + 1].Value = AssociatedItem;
                                //' Item
                                _with2[7, i + 1].Value = AssociateCategory;
                                //' Hidden category
                                _with2[8, i + 1].Value = strProperty;
                            }

                        }
                        catch (Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //clsGeneral.UpdateLog("glocomm Error While Inserting Liquid Data   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            return false;

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("glocomm Error While Inserting Liquid Data   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
        }

        private void cmbFieldCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
            //It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event


            //IsforModify = false;

            //'DISPLAY MESSAGE WHEN WE CHANGE CATEGORY
            ///'' for making radiobuttons visible ''''
            if (cmbDataType.ToString().Trim() != "Choose an item" & cmbFieldCategory.Text.Trim() == "HPI" & cmbDataType.Text.Trim() == "Text")
            {
                pnlHPIExtended.Visible = true;
                RdbtnBrief.Checked = false;
                RdbtnExtended.Checked = false;
            }
            else
            {
                pnlHPIExtended.Visible = false;
                RdbtnBrief.Checked = false;
                RdbtnExtended.Checked = false;
            }
            if (cmbFieldCategory.ToString().Trim() != "Choose an item" & dgItemList.Rows.Count != 0)
            {
                if (Panel2.Visible == true & pnlAddCategory.Visible == true & pnlFieldValues.Visible == true)
                {
                    //If Panel2.Visible = True And pnlAddCategory.Visible = False And pnlFieldValues.Visible = True Then
                    //MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    if (MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        dgTableField.Rows.Clear();
                    }
                    else
                    {
                        cmbFieldCategory.Text = _selectedCategory;
                        return;
                    }
                }
            }
            if (cmbFieldCategory.ToString().Trim() != "Choose an item" & dgTableField.Rows.Count != 0)
            {
                //If Panel2.Visible = True And pnlAddCategory.Visible = True And pnlFieldValues.Visible = True Then
                if (Panel2.Visible == true & pnlAddCategory.Visible == true & pnlFieldValues.Visible == true)
                {
                    if (MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        dgTableField.Rows.Clear();
                    }
                    else
                    {
                        cmbFieldCategory.Text = _selectedCategory;
                        return;
                    }
                    dgTableField.RowCount = 0;
                    dgTableField.Rows.Clear();
                }
            }
            //If Category is Physical Examination 
            if (cmbFieldCategory.Text.ToString().Trim() == "Physical Examination")
            {
                chkAssociateStd.Visible = true;
            }
            else
            {
                chkAssociateStd.Visible = false;
                chkAssociateStd.Checked = false;
                dgTableField.Columns[5].Visible = false;
                dgTableField.Columns[6].Visible = false;
            }

            //If Category is Management Option and Field Type is Multiple Selection Then
            if (cmbFieldCategory.Text.ToString().Trim() == "Management option" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard Managment option";

                //If Category is Labs and Field Type is Multiple Selection Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Labs" & cmbDataType.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard Labs";

                //If Category is X-Ray/Radiology and Field Type is Multiple Selection Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "X-Ray/Radiology" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard X-Ray/Radiology";

                //If Category is Other Diagnosis Test and Field Type is Multiple Selection Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Other Diagonsis Tests" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard Other Diagonsis Tests";

                //If Category is HPI and Field Type is Multiple Selection Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "HPI" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard HPI";

                //If Category is ROS and Field Type is Multiple Selection Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "ROS" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard ROS";

                //Else

                //    chkAssociatestddata.Checked = False
                //    chkAssociatestddata.Visible = False
                //    pnlStandardEM.Visible = False
            }

            //If Category is History and Field Type is Boolean Then
            if (cmbFieldCategory.Text.ToString().Trim() == "History" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillcombobox();
                chkAssociatestddata.Text = "Associate standard Histroy";

                //If Category is Labs and Field Type is Boolean Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Labs" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillbooleancombobox();
                chkAssociatestddata.Text = "Associated Labs";

                //If Category is Management Option and Field Type is Boolean Then
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Management option" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillbooleancombobox();
                chkAssociatestddata.Text = "Associated Management Option";

            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "X-Ray/Radiology" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillbooleancombobox();
                chkAssociatestddata.Text = "Associated X-Ray/Radiology";

            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Other Diagonsis Tests" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = true;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
                fillbooleancombobox();
                chkAssociatestddata.Text = "Associated Other Diagonsis Tests";
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "General" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Choose an item" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Physical Examination" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "ROS" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "HPI" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Medical Decision-Making" & cmbDataType.Text.ToString().Trim() == "Boolean")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }

            if (cmbFieldCategory.Text.ToString().Trim() == "Medical Decision-Making" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "General" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }
            else if (cmbFieldCategory.Text.ToString().Trim() == "Choose an item" & cmbDataType.Text.ToString().Trim() == "Multiple Selection")
            {
                pnlFieldValues.Dock = DockStyle.Fill;
                chkAssociatestddata.Visible = false;
                chkAssociatestddata.Checked = false;
                pnlStandardEM.Visible = false;
            }

        }

        private void cmbFieldCategory_Click(object sender, EventArgs e)
        {
            _selectedCategory = cmbFieldCategory.Text;
        }

        private bool DownloadLiquidDataXml(string DownloadPath, string IsFrom, string Title)
        {
            //trvDiscrete.Nodes.Clear();
            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataSRV + ".xml";
            bool IsDownloadXml = false;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            //
            try
            {
                IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                if (IsDownloadXml == true)
                {
                    string _TableName = "Table";
                    DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        //Merging all Xml data for Global Repository Liquid Data.
                        if (dtGlobalMerge == null)
                            dtGlobalMerge = dt;
                        else
                            dtGlobalMerge.Merge(dt);
                        //
                        FillgloCommunityLiquidData(dt, IsFrom, Title);
                    }
                }
            }
            catch (Exception ex)
            {
                IsDownloadXml = false;
                //clsGeneral.UpdateLog("glocomm Error While download Liquid Data XML   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objgloCommunity = null;
                objDlLiquidData = null;
            }
            return IsDownloadXml;
        }

        private void SetDataDownload(myTreeNode SelectedNode)
        {
            long m_ElementId = 0;
            clsLiquidData_Upload objLiquidData = new clsLiquidData_Upload();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            try
            {
                if (txtItem.Enabled == false)
                {
                    txtItem.Enabled = false;
                }
                if (dgItemList.Rows.Count < 2)
                {
                    txtItem.Enabled = false;
                }



                if (SelectedNode.Text != "Liquid Data")
                {
                    //if (IsfrmExam == false)
                    //{
                    m_ElementId = SelectedNode.Key;
                    //}

                    if (m_ElementId == 0)
                    {
                        if (trvDiscrete.SelectedNode == null)
                        {
                            return;
                        }

                        if (trvDiscrete.SelectedNode.Level == 0)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    //Commented for fixed Bug # : 29219 on 20120704
                    //MessageBox.Show("Select record to modify", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //pnl_ToolStrip.Visible = false;
                //blnModify = true;

                txtcategory.Enabled = false;
                txtCatItem.Enabled = false;
                CmbControl.Enabled = false;
                ResetEditPanel();

                //Getting Selected Liquid data category from LiquidDataXml.
                DataTable objdata = null;
                objdata = new DataTable();
                string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataSRV + ".xml";

                string _TableName = "Table";
                DataTable dtXml = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                objdata = dtXml.Clone();
                objdata.Rows.Clear();
                if (dtXml.Rows.Count > 0 && dtXml != null)
                {
                   // DataRow[] drXml = null;
                    foreach (DataRow dr in dtXml.Rows)
                    {
                        if (IsClinicRepository == false)
                        {
                            if ((dr["sElementName"].ToString().Trim() == SelectedNode.Text.Trim().Replace('≈', '˜')) && (dr["Specialty"].ToString().Trim() == SelectedNode.Parent.Parent.Text.Trim()) || ((dr["sElementName"].ToString().Trim().Replace('˜', '≈') == SelectedNode.Text.Trim()) && (dr["Specialty"].ToString().Trim() == SelectedNode.Parent.Parent.Text.Trim())))
                            {
                                string _FilterExpr = "";
                                // if (IsClinicRepository == true)
                                //_FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim() + "') AND ClinicName = '" + clsGeneral.gstrClinicName + "'";
                                //   _FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim() + "')";// AND ClinicName = '" + clsGeneral.gstrClinicName + "'";
                                //else
                                if (dr["sElementName"].ToString().Trim().Contains('≈') == false)
                                {
                                    _FilterExpr = "((nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "') AND Specialty = '" + SelectedNode.Parent.Parent.Text + "') OR (nGroupID ='0' AND sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "')";
                                }
                                else

                                    _FilterExpr = "((nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim() + "') AND Specialty = '" + SelectedNode.Parent.Parent.Text + "') OR (nGroupID ='0' AND sElementName = '" + SelectedNode.Text.Trim() + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "')";

                                //getting Liquid data according to ElementID & ElementName

                                //Problem : 00000163
                                //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                                        //Furthermore, when pulling the liquid data fields into a template, they also do not pull into any type of order (it is also random). 
                                        //This is a new issue in 605x and above.
                                //Change : Commented the below logic which filters the datatable and new logic added which uses dataview to filter datatable as it maintains the original row sequence.

                                //drXml = null;
                                //drXml = dtXml.Select(_FilterExpr);

                                //objdata.AcceptChanges();
                                //for (int i = 0; i < drXml.Length; i++)
                                //{
                                //    objdata.ImportRow(drXml[i]);
                                //}

                                //objdata.AcceptChanges();

                                DataView dv = new DataView(dtXml);

                                dv.RowFilter = _FilterExpr;

                                objdata = dv.ToTable();

                                objdata.AcceptChanges();

                                dv.Dispose();
                                dv = null;

                                //////Add Category
                                ////if (IsClinicRepository == true)
                                ////    _FilterExpr = "nGroupID = " + dr["nGroupID"] + " AND sElementName = '" + SelectedNode.Text.Trim() + "' AND ClinicName = '" + clsGeneral.gstrClinicName + "'";
                                ////else
                                ////    _FilterExpr = "nGroupID = " + dr["nGroupID"] + " AND sElementName = '" + SelectedNode.Text.Trim() + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "'";
                                ////drXml = dtXml.Select(_FilterExpr);
                                ////if (drXml.Length > 0)
                                ////    objdata.ImportRow(drXml[0]);
                                //////End
                                break;
                            }
                        }

                        else
                        {

                            if ((dr["sElementName"].ToString().Trim().Replace('˜', '≈') == SelectedNode.Text.Trim()))
                            {
                                string _FilterExpr = "";
                                //   if (IsClinicRepository == true)
                                //_FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim() + "') AND ClinicName = '" + clsGeneral.gstrClinicName + "'";
                                //   _FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "')";// AND ClinicName = '" + clsGeneral.gstrClinicName + "'";


                                if (dr["sElementName"].ToString().Trim().Contains('≈') == false)
                                {
                                    _FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "')";// AND Specialty = '" + SelectedNode.Parent.Parent.Text + "') OR (nGroupID ='0' AND sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "')";
                                }
                                else

                                    _FilterExpr = "(nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim() + "')";// AND Specialty = '" + SelectedNode.Parent.Parent.Text + "') OR (nGroupID ='0' AND sElementName = '" + SelectedNode.Text.Trim() + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "')";




                                // else
                                //    _FilterExpr = "((nGroupID = " + dr["nElementID"] + " OR sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "') AND Specialty = '" + SelectedNode.Parent.Parent.Text + "') OR (nGroupID ='0' AND sElementName = '" + SelectedNode.Text.Trim().Replace('≈', '˜') + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "')";
                                //getting Liquid data according to ElementID & ElementName

                                //Problem : 00000163
                                //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                                        //Furthermore, when pulling the liquid data fields into a template, they also do not pull into any type of order (it is also random). 
                                        //This is a new issue in 605x and above.
                                //Change : Commented the below logic which filters the datatable and new logic added which uses dataview to filter datatable as it maintains the original row sequence.

                                #region "Commented old Logic"
                                //drXml = null;
                                //drXml = dtXml.Select(_FilterExpr);
                                //objdata.AcceptChanges();
                                //for (int i = 0; i < drXml.Length; i++)
                                //{
                                //    objdata.ImportRow(drXml[i]);
                                //}
                                //objdata.AcceptChanges();
                                #endregion

                                DataView dv = new DataView(dtXml);
                                dv.RowFilter = _FilterExpr;
                                objdata = dv.ToTable();
                                objdata.AcceptChanges();
                                dv.Dispose();
                                dv = null;

                                //////Add Category
                                ////if (IsClinicRepository == true)
                                ////    _FilterExpr = "nGroupID = " + dr["nGroupID"] + " AND sElementName = '" + SelectedNode.Text.Trim() + "' AND ClinicName = '" + clsGeneral.gstrClinicName + "'";
                                ////else
                                ////    _FilterExpr = "nGroupID = " + dr["nGroupID"] + " AND sElementName = '" + SelectedNode.Text.Trim() + "' AND Specialty = '" + SelectedNode.Parent.Parent.Text + "'";
                                ////drXml = dtXml.Select(_FilterExpr);
                                ////if (drXml.Length > 0)
                                ////    objdata.ImportRow(drXml[0]);
                                //////End
                                break;
                            }

                        }
                    }
                }
                //End

                if ((objdata != null))
                {
                    dgTableField.Rows.Clear();

                    for (Int32 _inc = 0; _inc <= objdata.Rows.Count - 1; _inc++)
                    {
                        if (Convert.ToInt64(objdata.Rows[_inc]["nGroupID"]) == 0)
                        {
                            txtField.Text = objdata.Rows[_inc]["sElementName"].ToString();
                            cmbDataType.Text = objdata.Rows[_inc]["sElementType"].ToString();
                            if (cmbDataType.Text == "Multiple Selection")
                            {
                                dgItemList.Columns[2].Visible = true;
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }
                            else
                            {
                                dgItemList.Columns[2].Visible = false;
                            }

                            if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.None.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.None.GetHashCode();

                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.General.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.General.GetHashCode();

                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Hitory.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Hitory.GetHashCode();

                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Physical_Examination.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Physical_Examination.GetHashCode();

                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Medical_Decision_Making.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Medical_Decision_Making.GetHashCode();
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.HPI.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.HPI.GetHashCode();
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Management_option.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Management_option.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Labs.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Labs.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.X_Ray_Radiology.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.X_Ray_Radiology.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.Other_Diagonsis_Tests.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.Other_Diagonsis_Tests.GetHashCode();
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean")
                                {
                                    fillbooleancombobox();
                                }
                                else
                                {
                                    fillcombobox();
                                }
                            }
                            else if (objdata.Rows[_inc]["sCategoryName"].ToString() == clsGeneral.CategoryType.ROS.ToString())
                            {
                                cmbFieldCategory.SelectedIndex = clsGeneral.CategoryType.ROS.GetHashCode();
                                fillcombobox();
                            }

                            chckRequired.Checked = Convert.ToBoolean(objdata.Rows[_inc]["bIsMandatory"]);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    for (Int32 _inc = 0; _inc <= objdata.Rows.Count - 1; _inc++)
                    {
                        if (Convert.ToInt64(objdata.Rows[_inc]["nGroupID"]) != 0)
                        {
                            //'The SubItems are to be binded to the list
                            //'If the datatype is of text then no need to show list box

                            if (objdata.Rows[_inc]["sElementType"].ToString() == "Text")
                            {
                                ValidateDatatype(objdata.Rows[_inc]["sElementType"].ToString());
                                txtItem.Text = objdata.Rows[_inc]["sElementName"].ToString();
                                pnlFieldValues.Dock = DockStyle.Top;

                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Table" | objdata.Rows[_inc]["sElementType"].ToString() == "Group")
                            {
                                ValidateDatatype(objdata.Rows[_inc]["sElementType"].ToString());
                                if (!string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociateditem"].ToString()))
                                {
                                    chkAssociateStd.Visible = true;
                                    chkAssociateStd.Checked = true;
                                    pnlStandardEM.Visible = true;
                                    dgTableField.Columns[5].Visible = true;
                                    dgTableField.Columns[6].Visible = true;
                                }
                                else
                                {
                                    chkAssociateStd.Checked = false;
                                    chkAssociateStd.Visible = false;
                                }

                                ////((clsGeneral.ControlType)objdata.Rows[_inc]["nControlType"]).GetHashCode()
                                InsertData(objdata.Rows[_inc]["sCategoryName"].ToString(), objdata.Rows[_inc]["sItemName"].ToString(), Convert.ToInt32(objdata.Rows[_inc]["nControlType"]), objdata.Rows[_inc]["sAssociatedCategory"].ToString(), objdata.Rows[_inc]["sAssociateditem"].ToString(), objdata.Rows[_inc]["sAssociatedProperty"].ToString());
                                txtCaption.Text = objdata.Rows[_inc]["sElementName"].ToString();
                                pnlFieldValues.Dock = DockStyle.Top;
                                txtItem.Text = objdata.Rows[_inc]["sElementName"].ToString();

                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Multiple Selection")
                            {
                                var _with1 = dgItemList;
                                _with1.Rows.Add();
                                _with1[0, _with1.Rows.Count - 1].Value = ((clsGeneral.ControlType)Convert.ToInt32(objdata.Rows[_inc]["nControlType"])); //((clsGeneral.ControlType)objdata.Rows[_inc]["nControlType"]).GetHashCode().ToString();
                                _with1[1, _with1.Rows.Count - 1].Value = objdata.Rows[_inc]["sElementName"].ToString();
                                _with1[2, _with1.Rows.Count - 1].Value = ((clsGeneral.ControlType)Convert.ToInt32(objdata.Rows[_inc]["nControlType"]));
                                if (!string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociatedProperty"].ToString()))
                                {
                                    _with1[3, _with1.Rows.Count - 1].Value = objdata.Rows[_inc]["sAssociatedProperty"].ToString();
                                    _with1.Columns[3].Visible = true;
                                    chkAssociatestddata.Checked = true;
                                    chkAssociatestddata.Visible = true;
                                    pnlassociateStdItem.Visible = true;
                                    chkAssociatestddata.Enabled = false;
                                }
                                else
                                {
                                    _with1.Columns[3].Visible = false;
                                    chkAssociatestddata.Checked = false;
                                    chkAssociatestddata.Visible = false;
                                    pnlassociateStdItem.Visible = false;
                                    chkAssociatestddata.Enabled = false;
                                }
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }
                            else if (objdata.Rows[_inc]["sElementType"].ToString() == "Text" | objdata.Rows[_inc]["sElementType"].ToString() == "Boolean" | objdata.Rows[_inc]["sElementType"].ToString() == "Single Selection" | objdata.Rows[_inc]["sElementType"].ToString() == "Group")
                            {
                                dgItemList.Rows.Add(0, objdata.Rows[_inc]["sElementName"].ToString());
                                if (objdata.Rows[_inc]["sElementType"].ToString() == "Boolean" & !string.IsNullOrEmpty(objdata.Rows[_inc]["sAssociatedProperty"].ToString()))
                                {
                                    dgItemList[3, dgItemList.Rows.Count - 1].Value = objdata.Rows[_inc]["sAssociatedProperty"].ToString();
                                    dgItemList.Columns[3].Visible = true;
                                    chkAssociatestddata.Visible = true;
                                    chkAssociatestddata.Checked = true;

                                    pnlassociateStdItem.Visible = true;
                                }
                                pnlFieldValues.Dock = DockStyle.Fill;
                            }

                        }
                    }
                    if (objdata.Rows.Count > 0)
                    {
                        ValidateDatatype(objdata.Rows[0]["sElementType"].ToString());
                    }
                }

                if (cmbFieldCategory.Text == "HPI" | cmbFieldCategory.Text == "Management option" | cmbFieldCategory.Text == "X-Ray/Radiology" | cmbFieldCategory.Text == "Other Diagonsis Tests" | cmbFieldCategory.Text == "ROS" | cmbFieldCategory.Text == "Labs")
                {
                    if (cmbDataType.Text == "Table" | cmbDataType.Text == "Group" | cmbDataType.Text == "Single Selection" | cmbDataType.Text == "Boolean" | cmbDataType.Text == "Text")
                    {
                        chkAssociatestddata.Visible = false;
                    }
                    else
                    {
                        chkAssociatestddata.Visible = true;
                    }

                }
                else if (cmbFieldCategory.Text == "Physical Examination")
                {
                    if (cmbDataType.Text == "Group" | cmbDataType.Text == "Table")
                    {
                        chkAssociateStd.Visible = true;
                    }
                    else
                    {
                        chkAssociatestddata.Visible = false;
                    }

                }
                else if (cmbDataType.Text == "Boolean")
                {
                    if (cmbFieldCategory.Text == "History" | cmbFieldCategory.Text == "Management option" | cmbFieldCategory.Text == "X-Ray/Radiology" | cmbFieldCategory.Text == "Other Diagonsis Tests")//| cmbAssociatedCategory.Text == "Labs"
                    {
                        chkAssociatestddata.Visible = true;
                    }
                    else
                    {
                        //Field type as 'Boolean' n Category as General
                        chkAssociatestddata.Visible = false;
                    }
                }
                else
                {
                    chkAssociateStd.Checked = false;
                    chkAssociateStd.Visible = false;
                    chkAssociatestddata.Checked = false;
                    chkAssociatestddata.Visible = false;
                }
                pnlEdit.Visible = true;
                txtField.Focus();
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //clsGeneral.UpdateLog("glocomm Error While Setdatadownload   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objLiquidData = null;
                objDlLiquidData = null;
            }
        }

        private void ClearData()
        {
            txtField.Text = "";
            chckRequired.Checked = false;
            cmbDataType.SelectedIndex = 0;
            cmbFieldCategory.SelectedIndex = 0;
            txtItem.Text = "";
            RdbtnBrief.Checked = false;
            RdbtnExtended.Checked = false;
            chkAssociatestddata.Checked = false;
            cmbstddata.SelectedIndex = -1;
            dgTableField.Rows.Clear();
            dgItemList.Rows.Clear();
        }

        private void FillgloCommunityLiquidData(DataTable dt, string IsFrom, string Title)
        {
            myTreeNode trvRootNode = default(myTreeNode);//Clinic Name
            myTreeNode trvChildNode = default(myTreeNode);//Liquid Data Text
            myTreeNode trvChildNode1 = default(myTreeNode);//Liquid Data Category Name

            try
            {
                //'Get the Liquid data  for binding it to Treeview
                DataTable objData = dt;
                if ((objData != null))
                {
                    var _with1 = trvDiscrete;
                    //_with1.Nodes.Clear();
                    //'Add the default tree node,Text is Clinic Name. 
                    trvRootNode = new myTreeNode();
                    trvRootNode.Text = Title;
                    trvRootNode.ImageIndex = 0;
                    trvRootNode.SelectedImageIndex = 0;
                    trvRootNode.Checked = false;
                    _with1.Nodes.Add(trvRootNode);
                    //

                    //'Add the default tree node,Text is Liquid Data
                    trvChildNode = new myTreeNode();
                    //trvRootNode = new myTreeNode();
                    trvChildNode.Text = "Liquid Data";
                    trvChildNode.ImageIndex = 0;
                    trvChildNode.SelectedImageIndex = 0;
                    trvRootNode.Nodes.Add(trvChildNode);
                    //

                    //'Add the Data fileds under the parent node
                    for (Int32 _inc = 0; _inc <= objData.Rows.Count - 1; _inc++)
                    {
                        if (objData.Rows[_inc]["nGroupID"].ToString().Trim() == "0")
                        {
                            // trvChildNode = New myTreeNode
                            Int64 _Flag = 0;
                            //trvRootNode = new myTreeNode();
                            //'check for Mandatory field and set the flag
                            if (Convert.ToBoolean(objData.Rows[_inc]["bIsMandatory"]) == true)
                            {
                                _Flag = 1;
                            }
                            //'Key Refers to Element Id, Strname refers to Element name, _Flag refers to IsMandatory 
                            string _strElementName = "";

                            if (objData.Rows[_inc]["sElementName"].ToString().Trim().Contains("˜"))
                                _strElementName = objData.Rows[_inc]["sElementName"].ToString().Trim().Replace('˜', '≈');
                            else
                                _strElementName = objData.Rows[_inc]["sElementName"].ToString().Trim();

                            trvChildNode1 = new myTreeNode(_strElementName, Convert.ToInt64(objData.Rows[_inc]["nElementID"]), _Flag);
                            trvChildNode1.ImageIndex = 2;
                            trvChildNode1.SelectedImageIndex = 2;
                            trvChildNode1.NodeName = objData.Rows[_inc]["sElementType"].ToString();
                            trvChildNode.Nodes.Add(trvChildNode1);
                            trvChildNode1 = null;
                        }
                    }
                    // .SelectedNode = trvRootNode

                    _with1.ExpandAll();
                    //trvDiscrete.SelectedNode = trvDiscrete.Nodes(0)
                }
                TreeNode n = trvDiscrete.Nodes[0];

                trvDiscrete.SelectedNode = n;
                trvDiscrete.Focus();

            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //clsGeneral.UpdateLog("glocomm Error Filling LiquidData   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                trvRootNode = null;
                dt = null;
                //ObjclsgloCommunity = null;
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            IsClinicRepository = false;
            this.Cursor = Cursors.WaitCursor;
            trvDiscrete.Nodes.Clear();
            ClearData();
            string strSitepath = "";
            string strSitefolder = "";
            string strFrom = "";
            clsgloCommunity ObjclsgloCommunity = new clsgloCommunity();

            try
            {
                strSitepath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                strSitefolder = clsGeneral.WebGlobalXmlFolder;
                strFrom = "Global";

                gloList = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    gloList.UseDefaultCredentials = true;
                else
                {
                     //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        gloList.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        gloList.CookieContainer = clsGeneral.oCookie;
                    }
                }

                gloList.Url = strSitepath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;

                System.Xml.XmlNode node = gloList.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {
                        if (xmlnode.Attributes["Title"].Value.ToString() == strSitefolder)
                        {
                            DataTable dtGlobalRepository = new DataTable();
                            dtGlobalRepository = ObjclsgloCommunity.GetList(xmlnode.Attributes["Title"].Value.ToString(), strSitepath);
                            if (dtGlobalRepository.Rows.Count > 0 && dtGlobalRepository != null)
                            {
                                foreach (DataRow dr in dtGlobalRepository.Rows)
                                {
                                    if (dr["ContentType"].ToString().Trim() == "Folder")
                                    {
                                        string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + dr["Title"].ToString().Trim() + "/" + clsGeneral.gstrLiquidDataFNm + "/" + LiquidDataSRV + ".xml";
                                        DownloadLiquidDataXml(DownloadPath, strFrom, dr["Title"].ToString().Trim());
                                    }
                                }
                            }
                        }
                    }
                }

                //Create Xml for all Global Repository Liquid Data.
                if (dtGlobalMerge != null && dtGlobalMerge.Rows.Count > 0)
                {
                    dtGlobalMerge.TableName = "Table";
                    dtGlobalMerge.WriteXml(gloSettings.FolderSettings.AppTempFolderPath + LiquidDataSRV + ".xml");
                }
                //End
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error  while Clicking on Global Repository   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                gloList = null;
                ObjclsgloCommunity = null;
                if (dtGlobalMerge != null)
                    dtGlobalMerge = null;
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            IsClinicRepository = true;
            this.Cursor = Cursors.WaitCursor;
            trvDiscrete.Nodes.Clear();
            ClearData();
            try
            {
                string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrLiquidDataFNm + "/" + LiquidDataSRV + ".xml";
                DownloadLiquidDataXml(DownloadPath, "User", clsGeneral.gstrClinicName);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);

                //clsGeneral.UpdateLog("glocomm Error  while Clicking on Clinic Repository   in Liquid Data  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (dtGlobalMerge != null)
                    dtGlobalMerge = null;
            }
            this.Cursor = Cursors.Default;
        }

        private void trvDiscrete_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text.Trim() == "Liquid Data")
            {
                if (_strAction == "Upload")
                    ChkUnchkChilds(trvDiscrete.Nodes[0]);
                else
                    ChkUnchkChilds(trvDiscrete.Nodes[0].Nodes[0]);
            }
        }

        private void ChkUnchkChilds(TreeNode trvDiscrete)
        {
            for (int i = 0; i <= trvDiscrete.Nodes.Count - 1; i++)
            {
                if (trvDiscrete.Checked == true)
                    trvDiscrete.Nodes[i].Checked = true;
                else
                    trvDiscrete.Nodes[i].Checked = false;
            }
        }

        private void trvDiscrete_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //  myTreeNode SelectedLiquid = (myTreeNode)trvDiscrete.SelectedNode;
                trvDiscrete.SelectedNode = e.Node;
                //'SelectedLiquid
                if ((e.Node == null) == false)
                {
                    if (_strAction == "Upload")
                        SetDataUpload((myTreeNode)e.Node);//get data from database.
                    else
                    {
                        try
                        {
                            if (e.Node.Parent.Parent != null)
                                SetDataDownload((myTreeNode)e.Node);//get data from LiquidDataXml.
                        }
                        catch
                        {
                        }
                    }
                }

                //For boolean we should enter only two Values
                if (cmbDataType.Text == "Boolean" & dgItemList.RowCount == 2)
                {
                    txtItem.Enabled = false;
                }
                else
                {
                    txtItem.Enabled = false;
                }
                //''for making radiobuttons visible ''''
                if (cmbDataType.Text.Trim() != "Choose an item" & cmbFieldCategory.Text.Trim() == "HPI")
                {
                    pnlHPIExtended.Visible = true;
                    RdbtnBrief.Checked = false;
                    RdbtnExtended.Checked = false;
                }
                else
                {
                    pnlHPIExtended.Visible = false;
                }
                // for splitting text and checking respective radiobutton '''
                if (txtField.Text.Contains("Brief"))
                {
                    string[] retval = (string[])SplitTextHypen(txtField.Text);
                    if ((retval != null))
                    {
                        if (retval.Length > 1)
                        {
                            txtField.Text = retval[0];
                            RdbtnBrief.Checked = true;
                        }
                    }
                }
                else if (txtField.Text.Contains("Extended"))
                {
                    string[] retval = (string[])SplitTextHypen(txtField.Text);
                    if ((retval != null))
                    {
                        if (retval.Length > 1)
                        {
                            txtField.Text = retval[0];
                            RdbtnExtended.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                //clsGeneral.UpdateLog("glocomm Error While Treeview Node Double Click in Liquid Data  Usercontrol : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;


        }
    }
}
