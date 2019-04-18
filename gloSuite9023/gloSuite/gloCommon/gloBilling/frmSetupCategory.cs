using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Data.SqlClient;

namespace gloBilling
{
    internal partial class frmSetupCategory : Form
    {
        #region "Variables"
         //MessageBox Caption
           private string _messageBoxCaption = String.Empty;


           //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
           System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
           private Int64 _ClinicID = 0;
           public Int64 ClinicID
           {
               get { return _ClinicID; }
               set { _ClinicID = value; }
           }
           //

        #endregion "Variables"
        
        #region "Property Procedures"

        private Int64 _catID = 0;
        private string _catOriginalDesc = string.Empty;

            private string _databaseconnectionstring = "";

            public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
            public Int64 catID
        {
            get { return _catID; }
            set { _catID = value; }
        }

        #endregion "Property Procedures"

        #region "Constuctor"
         
          //Constructor with conn String
            public frmSetupCategory(string DatabaseConnectionString)
            {
                InitializeComponent();
                _databaseconnectionstring = DatabaseConnectionString;
                //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

          //Constructor with conn String and category Id
            public frmSetupCategory(Int64 CatID, string DatabaseConnectionString)
            {
                InitializeComponent();
                
                //Variable Initialization
                _catID = CatID;
                _databaseconnectionstring = DatabaseConnectionString;
                //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        #endregion "Constuctor"

        #region "Form Load Event"
            private void frmSetupCategory_Load(object sender, EventArgs e)
        {

            txtCategory.Select();
            try
            {
                //Call Procedure to fill controls on page load.
                _FillControls();

                //For modify show existing information on form.
                if (_catID != 0)
                {
                    //create object to hold value from database.
                    Category oCat = new Category(_databaseconnectionstring);
                    DataTable dtCat;

                    //Fetch information of category to be modified from database.
                    dtCat = oCat.GetCategery(_catID);

                    //dispose object after getting values from database to table.
                    oCat.Dispose();

                    if (dtCat != null)
                    {
                        if (dtCat.Rows.Count != 0)
                        {
                            //Set control values on form.
                            txtCode.Text = Convert.ToString(dtCat.Rows[0]["sCode"]);
                            txtCategory.Text = dtCat.Rows[0]["sDescription"].ToString();
                            _catOriginalDesc = txtCategory.Text.Trim();
                            cmbCategoryType.SelectedValue = dtCat.Rows[0]["sCategoryType"].ToString();
                            chkFavourite.Checked = Convert.ToBoolean(dtCat.Rows[0]["bFavorites"]);
                            if (cmbCategoryType.Text == "Ethnicity Specification" || cmbCategoryType.Text == "Race Specification")
                            {
                                fillRaceEthnicityParentCoder();
                                cmbParentRace.SelectedValue = Convert.ToInt64(dtCat.Rows[0]["nParentCategoryId"]);
                            }
                           
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
      
        }
        #endregion "Form Load Event"

        #region "Toolstrip Button Events"
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Category oCategory = new Category(_databaseconnectionstring);
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        bool IsCPTCatInUseWithCPT = false;

                        //Validation for Category master.
                        if (txtCategory.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the category description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategory.Focus();
                            return;
                        }

                        //Validation of Text Length for Birth Sex Category
                        if (cmbCategoryType.Text == "Gender" && txtCategory.Text.Length > 10)
                        {
                            MessageBox.Show("Category description length should not be greater than 10.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategory.Focus();
                            return;
                        }

                        //Set values to Object
                        oCategory.CategoryDescription = txtCategory.Text.Trim();

                        oCategory.CategoryType = cmbCategoryType.Text;
                        oCategory.Code = txtCode.Text;
                        if (cmbCategoryType.Text == "Ethnicity Specification" ||cmbCategoryType.Text == "Race Specification")
                        {
                            string sParentCode = string.Empty;
                            DataRowView drv = (DataRowView)cmbParentRace.SelectedItem;
                            if ((drv == null) == false)
                            {
                                sParentCode = Convert.ToString(drv.Row[5]);
                            }
                            oCategory.ParentId = Convert.ToInt64(cmbParentRace.SelectedValue);
                            oCategory.ParentCode = sParentCode;
                            oCategory.IsFavorite = chkFavourite.Checked;
                        }
                        else if (cmbCategoryType.Text == "Ethnicity" || cmbCategoryType.Text == "Race")
                        {
                            oCategory.IsFavorite = chkFavourite.Checked;
                        }
                        //_catID = 4;  //test for modify
                        
                        if (_catID == 0)
                        {
                            //Check for category is already exist
                            if (oCategory.IsExists(0, oCategory.CategoryDescription.ToString(),oCategory.CategoryType) == true)
                            {
                                MessageBox.Show(this, "Category is already in use by another entry.  Select a unique category.  ", _messageBoxCaption);
                                return;
                            }

                            IsCPTCatInUseWithCPT = ValidateCPTCategoryData(_catID, _catOriginalDesc);
                            if (IsCPTCatInUseWithCPT == false)
                            {
                                //if TRUE is returned then that means update the CPT MST, 
                                //therfore let the Main Category_Mst table get updated which will be happening by executing the below [if (oCategory.Add() == 0)] condition
                                //if FALSE returned that means user do not want to update the CPT Category and hence return from here so that the below [if (oCategory.Add() == 0)] condition will not be executed
                                return;
                            }

                            _catID = oCategory.Add();
                            if (_catID == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                                //Hit message on failing to add category.
                                MessageBox.Show("Category not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Success);

                                //Since CatID =0 there is not need to call the  [oCategory.UpdateCPTCategoryInUse(_catID)] functionality
                            }

                         
                        }
                        else
                        {
                            oCategory.CategoryID = _catID;

                            //Check for category is already exist
                            if (oCategory.IsExists(_catID, oCategory.CategoryDescription.Trim(),oCategory.CategoryType) == true)
                            {
                                MessageBox.Show(this, "Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption);
                                return;
                            }

                            IsCPTCatInUseWithCPT = ValidateCPTCategoryData(_catID, _catOriginalDesc);
                            if (IsCPTCatInUseWithCPT == false)
                            {
                                //if TRUE is returned then that means update the CPT MST, 
                                //therfore let the Main Category_Mst table get updated which will be happening by executing the below [if (oCategory.Add() == 0)] condition
                                //if FALSE returned that means user do not want to update the CPT Category and hence return from here so that the below [if (oCategory.Add() == 0)] condition will not be executed
                                return;
                            }
                            

                            if (oCategory.Add() == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                                //Hit message on failing to add category.
                                MessageBox.Show("Category not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Success);

                                //check the IsCPTCatInUseWithCPT variable if, CPT category not in use then no need to update the CPT master
                                if (IsCPTCatInUseWithCPT == true)
                                {
                                    //update the respective CPT's Category description in CPT_Mst with Category description of Category_Mst when Category type = 'CPT'
                                   oCategory.UpdateCPTCategoryInUse(_catID);
                                 
                                }
                                
                            }

                            
                        }

           
                        //Close the form.
                        this.Close();

                        //Dispose the object.
                        oCategory.Dispose();

                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":

                        //Validation for Category master.
                        if (txtCategory.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the category description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategory.Focus();
                            return;
                        }

                        //Validation of Text Length for Birth Sex Category
                        if (cmbCategoryType.Text == "Gender" && txtCategory.Text.Length > 10)
                        {
                            MessageBox.Show("Category description length should not be greater than 10.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCategory.Focus();
                            return;
                        }

                        //Set values to Object
                        oCategory.CategoryDescription = txtCategory.Text.Trim();

                        oCategory.CategoryType = cmbCategoryType.Text;
                        if (cmbCategoryType.Text == "Ethnicity Specification" || cmbCategoryType.Text == "Race Specification")
                        {
                            string sParentCode = string.Empty;
                            DataRowView drv = (DataRowView)cmbParentRace.SelectedItem;
                            if ((drv == null) == false)
                            {
                                sParentCode = Convert.ToString(drv.Row[5]);
                            }
                            oCategory.ParentId = Convert.ToInt64(cmbParentRace.SelectedValue);
                            oCategory.ParentCode = sParentCode;
                            oCategory.IsFavorite = chkFavourite.Checked;
                        }
                        else if (cmbCategoryType.Text == "Ethnicity" || cmbCategoryType.Text == "Race")
                        {
                            oCategory.IsFavorite = chkFavourite.Checked;
                        }


                        //_catID = 4;  //test for modify

                        if (_catID == 0)
                        {
                            //Check for category is already exist
                            if (oCategory.IsExists(0, oCategory.CategoryDescription.ToString(), oCategory.CategoryType) == true)
                            {
                                MessageBox.Show(this, "Category is already in use by another entry.  Select a unique category.  ", _messageBoxCaption);
                                return;
                            }
                            _catID = oCategory.Add();
                            if (_catID == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                                //Hit message on failing to add category.
                                MessageBox.Show("Category not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                txtCategory.Text = "";
                                cmbCategoryType.SelectedIndex = 0;
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Success);
                            }
                        }
                        else
                        {
                            oCategory.CategoryID = _catID;

                            //Check for category is already exist
                            if (oCategory.IsExists(_catID, oCategory.CategoryDescription.Trim(), oCategory.CategoryType) == true)
                            {
                                MessageBox.Show(this, "Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption);
                                return;
                            }


                            if (oCategory.Add() == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                                //Hit message on failing to add category.
                                MessageBox.Show("Category not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                txtCategory.Text = "";
                                cmbCategoryType.SelectedIndex = 0;
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Success);
                            }
                        }
                        //Close the form.
                        //this.Close();

                        //Dispose the object.
                        oCategory.Dispose();

                      
                        _catID = 0;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              
            }



        }
        #endregion "Toolstrip Button Events"

        #region "Fill Procedures"
        private void _FillControls()
        {
            //Create Object of dbLayer
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            oDB.Connect(false);
            try
            {
                DataTable dt = null;

                //Fill datatable with Category Types with store procedure call.
                oDB.Retrive("BL_Fill_CategoryTypes", out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //Set values to combo read in datatable.
                        cmbCategoryType.DataSource = dt;
                        cmbCategoryType.ValueMember = dt.Columns[1].ColumnName;
                        cmbCategoryType.DisplayMember = dt.Columns[1].ColumnName;

                        //set index to first element in combo.
                        cmbCategoryType.SelectedIndex = 1;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException  ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //Dispose the dbLayer Object
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        private void fillRaceEthnicityParentCoder()
        {
            DataTable dtParent = new DataTable();
            try
            {
                dtParent = GetRaceEthnicityParentList(cmbCategoryType.Text);
                if (dtParent != null && dtParent.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtParent.Rows)
                    {
                        if (Convert.ToInt64(dr["nCategoryID"]) == _catID)
                        {
                            dr.Delete();
                        }
                    }
                    dtParent.AcceptChanges();

                    cmbParentRace.DataSource = dtParent;
                    cmbParentRace.DisplayMember = dtParent.Columns[1].ColumnName;
                    cmbParentRace.ValueMember = dtParent.Columns[0].ColumnName;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        public DataTable GetRaceEthnicityParentList(string sCategoryDesc)
        {
            SqlCommand Cmd = null;
            SqlDataAdapter adpt = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection Conn=new SqlConnection(DatabaseConnectionString);

            try
            {
                Conn.Open();
                Cmd = new SqlCommand("gsp_GetRaceList", Conn);
                SqlParameter objParam = default(SqlParameter);
                objParam = Cmd.Parameters.Add("@sCategoryDesc", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCategoryDesc;
                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;


                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0].Copy();
            }
            catch (SqlException)
            {
                Conn.Close();

                return null;
            }
            catch (Exception)
            {
                Conn.Close();
                //MessageBox.Show(ex.ToString, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if ((Cmd == null) == false)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                if ((adpt == null) == false)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if ((ds == null) == false)
                {
                    ds.Dispose();
                    ds = null;
                }
            }

        }

        private bool ValidateCPTCategoryData(long CategoryID, string OriginalCategoryDescription)
        {
            bool blnRetval = false;
            Category oCategory = new Category(_databaseconnectionstring);
            try
            { 
                //validation to check whether the cpt category in use.
                if (txtCategory.Text != "")
                {

                    if (oCategory.IsCPTCategoryInUse(CategoryID, OriginalCategoryDescription))
                    {
                        oCategory.Dispose();
                        DialogResult oDlgResult = DialogResult.None;
                        oDlgResult = MessageBox.Show("CPT Category is in use. Do you want to modify and update each CPT record?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (oDlgResult == System.Windows.Forms.DialogResult.Yes)
                        {


                            blnRetval = true;//user do not want to update hence keep it as it is
                        }
                        else
                        {
                            blnRetval = false;//return false so that the main Category Master table will also get updated
                        }
                        txtCategory.Focus();
                        return blnRetval;
                    }
                    else
                    {
                        blnRetval = true;//if category not in use then user should be allowed to modify it.
                    }

                }

                return blnRetval;
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               if (oCategory != null)
               {
                   oCategory.Dispose();
               }
               return blnRetval; 
                
            }
            finally
            {
                if (oCategory != null)
                {
                    oCategory.Dispose();
                }
            }
            
        }
        #endregion "Fill Procedures"

        private void cmbCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoryType.Text == "Race Specification")
            {
                fillRaceEthnicityParentCoder();
                pnlFavourite.Visible = true;
                pnlParentRace.Visible = true;
                pnlParentRace.BringToFront();
                Label7.Text = "Parent Race :";
                Label7.Location = new System.Drawing.Point(61, 9);
            }
            else if (cmbCategoryType.Text == "Ethnicity Specification")
            {
                fillRaceEthnicityParentCoder();
                pnlFavourite.Visible = true;
                pnlParentRace.Visible = true;
                pnlParentRace.BringToFront();
                Label7.Text = "Parent Ethnicity :";
                Label7.Location = new System.Drawing.Point(42, 9);
            }
            else if (cmbCategoryType.Text == "Ethnicity" || cmbCategoryType.Text == "Race")
            {
                pnlFavourite.Visible = true;
            }
            else
            {
                pnlFavourite.Visible = false;
                pnlParentRace.Visible = false;
            }
        }
    }
}