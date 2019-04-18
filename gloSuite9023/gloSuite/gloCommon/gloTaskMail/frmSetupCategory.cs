using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Data;

namespace gloTaskMail
{
    public partial class frmSetupCategory : Form
    {
        #region "Declarations"
        private string _databaseconnectionstring = "";

        //private string _messageBoxCaption = "gloPM";
        
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        private Int64 _catID = 0;
        private string _categoryName = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        
        #endregion "Declarations"

        #region "Propery Procedure"

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 CatTypeId
        {
            get { return _catID; }
            set { _catID = value; }
        }

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        #endregion "Propery Procedure"

        public frmSetupCategory(Int64 CatID,string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _catID = CatID;
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);

            InitializeComponent();

            //Added By Pramod Nair For Messagebox Caption 
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

        private void frmSetupCategory_Load(object sender, EventArgs e)
        {
            
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Category  oCategory = new gloTasksMails.Common.Category();
            
            try
            {
                //if Category for modify.
                if (CatTypeId > 0)
                {
                    
                    oCategory = oTaskMail.GetCategory(CatTypeId);

                    if (oCategory != null)
                    {
                        txtCategoryType.Text = oCategory.Description;
                        Int32 _Color = oCategory.ColorCode;
                        txtAppColor.BackColor = Color.FromArgb(_Color);
                    
                    }


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oCategory.Dispose();
                oTaskMail.Dispose();
            }




        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (txtCategoryType.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the category type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCategoryType.Select();
                        break;
                    }
                    if (txtAppColor.BackColor == null)
                    {
                        MessageBox.Show("Please select a category type color.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBrowseAppColor.Focus();
                        break;
                    }

                    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();

                    
                    if (CatTypeId == 0)
                    {
                        if(oTaskMail.IsExistsCategory(txtCategoryType.Text.Trim()))
                        {
                            MessageBox.Show("Category already exists.  ");
                            return;
                        }
                        //Pass ID = 0 to Add Category
                        oCategory.ID = 0;
                        oCategory.Description = txtCategoryType.Text.Trim();
                        oCategory.ColorCode = txtAppColor.BackColor.ToArgb();
                        
                        if (oTaskMail.Add(oCategory) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Category type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategoryType.Select();
                            break;
                        }
    
                        
                    }
                    else
                    {
                        if (txtCategoryType.Text.Trim() != CategoryName)
                        {
                            if (oTaskMail.IsExistsCategory(txtCategoryType.Text.Trim()))
                            {
                                MessageBox.Show("Category already exists.  ");
                                return;
                            }
                        }
                        //Create Category object
                        oCategory.ID = CatTypeId;
                        oCategory.Description = txtCategoryType.Text.Trim();
                        oCategory.ColorCode = txtAppColor.BackColor.ToArgb();

                        if (! oTaskMail.Modify(oCategory))
                        {
                            MessageBox.Show("Category type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategoryType.Select();
                            break;
                        }

                      
                    }
                    oCategory.Dispose();
                    oTaskMail.Dispose();
                    this.Close();

                    break;
                case "Cancel":
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void btnBrowseAppColor_Click(object sender, EventArgs e)
        {
            try
            {
                clDlg.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            DialogResult ok = clDlg.ShowDialog(this);
            if ((ok == DialogResult.OK) || (ok == DialogResult.Yes))
            {
                txtAppColor.BackColor = clDlg.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = clDlg.CustomColors;
                }
                catch
                {
                }
            }
        }

        
    }
}