using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gloDatabaseLayer;
using gloPatientPortal.Classes;
using System.Data;
using System.ComponentModel;

namespace gloPatientPortal.UserControls
{
    /// <summary>
    /// Interaction logic for UcHealthFormGrp.xaml
    /// </summary>
    public partial class UcHealthFormGrp : UserControl
    {
        string _strConnectionString = string.Empty;
        long _nLoginID;
        long _nLibId;

        public UcHealthFormGrp()
        {
            InitializeComponent();
        }

        public UcHealthFormGrp(string strConnectionString, long nLoginID)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;

        }

        public UcHealthFormGrp(long nCategoryId, string strPublishName, string strPreText, string strPostText)
        {
            InitializeComponent();

            cmbHisCategory.SelectedValue = nCategoryId;
            txtPublishNm.Text = strPublishName;
            txtPreText.Text = strPreText;
            txtPostText.Text = strPostText;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //UcC1grid oUcC1grid = null;
                //if (oUcC1grid == null)
                //{
                //    oUcC1grid = new UcC1grid(_strConnectionString);
                //    //stkGrid.Child = oUcC1grid;
                //}
                FillCategory();
                DesignGrid();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void FillCategory()
        {
            clsHistory oclsHistory = null;
            DataTable dtHisCategory = null;
            try
            {
                //Load history category
                oclsHistory = new clsHistory();
                dtHisCategory = oclsHistory.FillControls(_strConnectionString);
                if (dtHisCategory != null && dtHisCategory.Rows.Count > 0)
                {
                    DataRow dr = dtHisCategory.NewRow();
                    dr["nCategoryId"] = -1;
                    dr["sDescription"] = "Select category";

                    dtHisCategory.Rows.InsertAt(dr, 0);

                    cmbHisCategory.ItemsSource = dtHisCategory.DefaultView;
                    cmbHisCategory.DisplayMemberPath = "sDescription";
                    cmbHisCategory.SelectedValuePath = "nCategoryID";
                    cmbHisCategory.SelectedIndex = 0;
                }
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMRAdmin");
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
        }

        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            AddGroup();
        }

        /// <summary>
        //Add group
        /// <summary>
        private void AddGroup()
        {
            if (cmbHisCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category.", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            clsHistory oclsHistory = null;
            try
            {
                long Id = 0;
                oclsHistory = new clsHistory();
                if (Convert.ToString(btnAddGroup.Content) == "Add Group")
                {
                    Id = oclsHistory.AddPFLibrary(Convert.ToInt64(cmbHisCategory.SelectedValue), txtPublishNm.Text, "G", txtPreText.Text, txtPostText.Text, 0, _nLoginID, _strConnectionString, 0, false);

                    if (Id > 0)
                    {
                        MessageBox.Show("Group added successfully", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                        DesignGrid();
                        cmbHisCategory.SelectedIndex = 0;
                        txtPublishNm.Text = string.Empty;
                        txtPostText.Text = string.Empty;
                        txtPreText.Text = string.Empty;
                    }
                    else if (Id == -1)
                        MessageBox.Show("Group allready exist", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Failed to add group", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Id = oclsHistory.AddPFLibrary(Convert.ToInt64(cmbHisCategory.SelectedValue), txtPublishNm.Text, "G", txtPreText.Text, txtPostText.Text, 0, _nLoginID, _strConnectionString, _nLibId, true);
                    if (Id > 0)
                    {
                        MessageBox.Show("Group updated successfully", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                        DesignGrid();
                        cmbHisCategory.SelectedIndex = 0;
                        txtPublishNm.Text = string.Empty;
                        txtPostText.Text = string.Empty;
                        txtPreText.Text = string.Empty;
                    }
                    else
                        MessageBox.Show("Failed to update group", "gloEMRAdmin", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (oclsHistory != null)
                    oclsHistory = null;
            }
        }

        private void cmbHisCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strCategory = Convert.ToString(((System.Data.DataRowView)(cmbHisCategory.SelectedItem)).Row.ItemArray[1]).Trim();
            if (strCategory != "Select category")
                txtPublishNm.Text = strCategory;
            else
                txtPublishNm.Text = string.Empty;
        }

        /// <summary>
        //Design data grid
        /// <summary>
        private void DesignGrid()
        {
            clsHistory oclsHistory = null;
            try
            {
                oclsHistory = new clsHistory();

                DataTable dt = oclsHistory.GetGroups(_strConnectionString);

                dgGroup.ItemsSource = null;

                dgGroup.RowHeight = 30;
                dgGroup.Items.Clear();

                dgGroup.Columns.Clear();


                //TaskPriorityImageSelector s1 = new TaskPriorityImageSelector();
                DataGridTextColumn nLibIdCol = new DataGridTextColumn();
                var _with1 = nLibIdCol;
                _with1.Width = 0;
                _with1.Visibility = System.Windows.Visibility.Hidden;
                _with1.Binding = new Binding(dt.Columns[0].ColumnName);
                _with1.Header = " ";
                _with1.Visibility = System.Windows.Visibility.Hidden;
                _with1.IsReadOnly = true;
                dgGroup.Columns.Add(nLibIdCol);


                DataGridTextColumn nCategoryIDCol = new DataGridTextColumn();
                var _with3 = nCategoryIDCol;
                _with3.Width = 0;
                _with3.Binding = new Binding(dt.Columns[1].ColumnName);
                _with3.Header = "";
                _with3.Visibility = System.Windows.Visibility.Hidden;
                _with3.IsReadOnly = true;
                dgGroup.Columns.Add(nCategoryIDCol);

                DataGridTextColumn sPublishNameCol = new DataGridTextColumn();
                var _with4 = sPublishNameCol;
                _with4.Width = 50;
                _with4.Binding = new Binding(dt.Columns[2].ColumnName);
                _with4.Header = "Publish Name";
                _with4.IsReadOnly = true;
                dgGroup.Columns.Add(sPublishNameCol);

                DataGridTextColumn sCategoryTypeCol = new DataGridTextColumn();
                var _with5 = sCategoryTypeCol;
                _with5.Width = 50;
                _with5.Binding = new Binding(dt.Columns[3].ColumnName);
                _with5.Header = "Category Type";
                _with5.IsReadOnly = true;
                dgGroup.Columns.Add(sCategoryTypeCol);

                DataGridTextColumn sPreTextCol = new DataGridTextColumn();
                var _with6 = sPreTextCol;
                _with6.Width = 50;
                _with6.Binding = new Binding(dt.Columns[4].ColumnName);
                _with6.Header = "Pre Text";
                _with6.IsReadOnly = true;
                dgGroup.Columns.Add(sPreTextCol);

                DataGridTextColumn sPostTextCol = new DataGridTextColumn();
                var _with7 = sPostTextCol;
                _with7.Width = 50;
                _with7.Binding = new Binding(dt.Columns[5].ColumnName);
                _with7.Header = "Post Text";
                _with7.IsReadOnly = true;
                dgGroup.Columns.Add(sPostTextCol);

                DataGridTextColumn nAnswerTypeCol = new DataGridTextColumn();
                var _with8 = nAnswerTypeCol;
                _with8.Width = 50;
                _with8.Binding = new Binding(dt.Columns[6].ColumnName);
                _with8.Header = "Answer Type";
                _with8.IsReadOnly = true;
                dgGroup.Columns.Add(nAnswerTypeCol);

                DataGridTextColumn nUserIdCol = new DataGridTextColumn();
                var _with9 = nUserIdCol;
                _with9.Width = 50;
                _with9.Binding = new Binding(dt.Columns[7].ColumnName);
                _with9.Header = "User Id";
                _with9.Visibility = System.Windows.Visibility.Hidden;
                _with9.IsReadOnly = true;
                dgGroup.Columns.Add(nUserIdCol);

                DataGridTextColumn sDescriptionCol = new DataGridTextColumn();
                var _with10 = sDescriptionCol;
                _with10.Width = 50;
                _with10.Binding = new Binding(dt.Columns[8].ColumnName);
                _with10.Header = "Category";
                _with10.IsReadOnly = true;
                dgGroup.Columns.Add(sDescriptionCol);


                DataGridTemplateColumn EditCol = new DataGridTemplateColumn();
                //accountColumn.Header = "Edit";
                EditCol.Width = 60;

                Binding bind = new Binding("Edit");
                bind.Mode = BindingMode.OneTime;

                // Create the Button
                Binding buttonBind = new Binding("Edit");
                buttonBind.Mode = BindingMode.OneWay;

                FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
                buttonFactory.SetValue(Button.NameProperty, "btnEdit");
                buttonFactory.SetValue(Button.ContentProperty, "Edit");
                buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnEdit_Click));

                DataTemplate buttonTemplate = new DataTemplate();
                buttonTemplate.VisualTree = buttonFactory;
                buttonTemplate.Seal();

                // Set the Templates to the Column
                EditCol.CellEditingTemplate = buttonTemplate;
                dgGroup.Columns.Add(EditCol);

                dgGroup.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditGroup();
        }

        /// <summary>
        //Edit group
        /// <summary>
        private void EditGroup()
        {
            DataRowView drv = ((System.Data.DataRowView)(dgGroup.SelectedItem));
            if (drv.Row.ItemArray.Count() > 0)
            {
                btnAddGroup.Content = "Edit Group";
                _nLibId = Convert.ToInt64(drv.Row.ItemArray[0]);
                cmbHisCategory.SelectedValue = drv.Row.ItemArray[1];
                txtPreText.Text = Convert.ToString(drv.Row.ItemArray[4]);
                txtPostText.Text = Convert.ToString(drv.Row.ItemArray[5]);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchGrid();
        }

        /// <summary>
        //Search Grid
        /// <summary>
        private void SearchGrid()
        {
            try
            {
                string strSearch = null;
                var _with1 = txtSearch;
                if (!string.IsNullOrEmpty(_with1.Text.Trim()))
                {
                    strSearch = _with1.Text.Replace("'", "''");
                }
                else
                {
                    strSearch = "";
                }

                DataView dvGroup = null;

                dvGroup = (DataView)dgGroup.ItemsSource;
                dvGroup.RowFilter = "[Publish Name] Like '%" + strSearch.Trim().Replace("'", "''") + "%' ";
                dgGroup.ItemsSource = dvGroup;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
