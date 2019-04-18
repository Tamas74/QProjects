using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloSecurity
{
    public partial class frmViewUsers : Form
    {
        private string  _databaseConnectionString = "";
        //private string _messageBoxCaption = "View Users";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Int16 SelectedView;
        public gloUser ogloUsers;
        DataView _dv;
       
        public frmViewUsers(string  databaseConnectionString)
        {

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

            InitializeComponent();
            _databaseConnectionString = databaseConnectionString;
        }

        private void ViewUsers_Load(object sender, EventArgs e)
        {
            Fill_MastersTreeview();
        }

        private void Fill_MastersTreeview()
        {
            try
            {
                TreeNode oNode;
                //Clear all nodes
                trvMasters.Nodes.Clear();
                //First node gloPMSUsers
                oNode = new TreeNode();
                oNode.Text = "gloPMSUsers";
                oNode.Tag = 0;
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 1;
                trvMasters.Nodes.Add(oNode);               
                trvMasters.SelectedNode = oNode;

                //Active Users Node
                oNode = new TreeNode();
                oNode.Text = "Active Users";
                oNode.Tag = 1;
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 1;
                trvMasters.Nodes[0].Nodes.Add(oNode);

                //Blocked Users Node
                oNode = new TreeNode();
                oNode.Text = "Blocked Users";
                oNode.Tag = 2;
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 1;
                trvMasters.Nodes[0].Nodes.Add(oNode);

                //All Users Node
                oNode = new TreeNode();
                oNode.Text = "All Users";
                oNode.Tag = 3;
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 1;
                trvMasters.Nodes[0].Nodes.Add(oNode);

                trvMasters.ExpandAll();                
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

              
            }
        }

        /// <summary>
        /// Display All Active in datagrid
        /// </summary>
        private void Fill_ActiveUsers()
        {
            ogloUsers = new gloUser(_databaseConnectionString);
            DataTable dtActiveUsers = ogloUsers.getActiveUsers();
            _dv = dtActiveUsers.DefaultView;
            dgUsers.DataSource = _dv;


            dgUsers.Columns[0].HeaderText = "UserId";
            dgUsers.Columns[1].HeaderText = "Login Name";
            dgUsers.Columns[2].HeaderText = "Full Name";
            dgUsers.Columns[3].HeaderText = "Phone No";
            dgUsers.Columns[4].HeaderText = "Mobile No";
            dgUsers.Columns[5].HeaderText = "Is Admin";


            dgUsers.Columns[0].Visible = false;
            dgUsers.Columns[1].Visible = true;
            dgUsers.Columns[2].Visible = true;
            dgUsers.Columns[3].Visible = true;
            dgUsers.Columns[4].Visible = true;
            dgUsers.Columns[5].Visible = true;

            int nWidth = dgUsers.Width;
            dgUsers.Columns[0].Width = 0;
            dgUsers.Columns[1].Width = nWidth / 5;
            dgUsers.Columns[2].Width = nWidth / 5;
            dgUsers.Columns[3].Width = nWidth / 5;
            dgUsers.Columns[4].Width = nWidth / 5;
            dgUsers.Columns[5].Width = nWidth / 5;
        }

        /// <summary>
        /// Display All Blocked in datagrid
        /// </summary>
        private void Fill_BlockedUsers()
        {
            ogloUsers = new gloUser(_databaseConnectionString);
            DataTable dtBlockedUsers = ogloUsers.getBlockedUsers();
            _dv = dtBlockedUsers.DefaultView;
            dgUsers.DataSource = _dv;


            dgUsers.Columns[0].HeaderText = "UserId";
            dgUsers.Columns[1].HeaderText = "Login Name";
            dgUsers.Columns[2].HeaderText = "Full Name";
            dgUsers.Columns[3].HeaderText = "Phone No";
            dgUsers.Columns[4].HeaderText = "Mobile No";
            dgUsers.Columns[5].HeaderText = "Is Admin";


            dgUsers.Columns[0].Visible = false;
            dgUsers.Columns[1].Visible = true;
            dgUsers.Columns[2].Visible = true;
            dgUsers.Columns[3].Visible = true;
            dgUsers.Columns[4].Visible = true;
            dgUsers.Columns[5].Visible = true;

            int nWidth = dgUsers.Width;
            dgUsers.Columns[0].Width = 0;
            dgUsers.Columns[1].Width = nWidth / 5;
            dgUsers.Columns[2].Width = nWidth / 5;
            dgUsers.Columns[3].Width = nWidth / 5;
            dgUsers.Columns[4].Width = nWidth / 5;
            dgUsers.Columns[5].Width = nWidth / 5;
        }

        /// <summary>
        /// Display All users in datagrid
        /// </summary>
        private void Fill_AllUsers()
        {            
            ogloUsers = new gloUser(_databaseConnectionString);
            DataTable dtAllUsers = ogloUsers.getAllUsers();
            _dv = dtAllUsers.DefaultView;
            dgUsers.DataSource = _dv;


            dgUsers.Columns[0].HeaderText = "UserId";
            dgUsers.Columns[1].HeaderText = "Login Name";
            dgUsers.Columns[2].HeaderText = "Full Name";
            dgUsers.Columns[3].HeaderText = "Phone No";
            dgUsers.Columns[4].HeaderText = "Mobile No";
            dgUsers.Columns[5].HeaderText = "Is Admin";

           
            dgUsers.Columns[0].Visible = false;
            dgUsers.Columns[1].Visible = true;
            dgUsers.Columns[2].Visible = true;
            dgUsers.Columns[3].Visible = true;
            dgUsers.Columns[4].Visible = true;
            dgUsers.Columns[5].Visible = true;


            int nWidth = dgUsers.Width;
            dgUsers.Columns[0].Width = 0;
            dgUsers.Columns[1].Width = nWidth/5;
            dgUsers.Columns[2].Width = nWidth / 5;
            dgUsers.Columns[3].Width = nWidth / 5;
            dgUsers.Columns[4].Width = nWidth / 5;
            dgUsers.Columns[5].Width = nWidth / 5;
                      
        }

        /// <summary>
        /// Action to perform after selecting a node form tree view
        /// fill the data grid with selected user type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvMasters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //Check whether node is selected or not
                if (trvMasters.SelectedNode != null)
                {
                    //Get the node name in string variable(SelectedView)
                    SelectedView = Convert.ToInt16(trvMasters.SelectedNode.Tag.ToString());

                    //Switch to node which is selected
                    switch (SelectedView)
                    {
                        case 1: // Fill Active Users  
                           Fill_ActiveUsers();
                            break;
                        case 2: // Fill Blocked Users
                            Fill_BlockedUsers();
                            break;
                        case 3: // Fill All Users
                            Fill_AllUsers();
                            break;
                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// select action to perform on the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            frmUserMgt ofrmUserMgt;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {                   
                    case "Add": // Add new User

                        ofrmUserMgt = new frmUserMgt(_databaseConnectionString);
                        ofrmUserMgt.ShowDialog(this);
                        ofrmUserMgt.Dispose();
                        ofrmUserMgt = null;
                        trvMasters_AfterSelect(null, null);
                        break;
                    
                    case "Modify": // Modify User                        
                        if (dgUsers.SelectedRows.Count > 0)
                        {
                            if (dgUsers.SelectedRows[0].Cells[0].Value.ToString() != "" || dgUsers.SelectedRows[0].Cells[0].Value.ToString() != "0")
                            {
                                Int64  userId = Convert.ToInt64(dgUsers.SelectedRows[0].Cells[0].Value);
                                ofrmUserMgt = new frmUserMgt(userId, _databaseConnectionString);
                                ofrmUserMgt.ShowDialog(this);
                                ofrmUserMgt.Dispose();
                                ofrmUserMgt = null;
                            }
                            else
                            {
                                MessageBox.Show(" Can not modify User ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        trvMasters_AfterSelect(null, null);
                        break;

                    case "Delete":
                        frmRestoreDB oRestore = new frmRestoreDB(_databaseConnectionString);
                        oRestore.ShowDialog(this);
                        oRestore.Dispose();
                        break;

                    case "Refresh":
                        trvMasters_AfterSelect(null, null);
                        trvMasters.SelectedNode = trvMasters.Nodes[0];
                        trvMasters.CollapseAll();
                        break;

                    case "Close":
                        this.Close();
                        break;

                    case "Help":
                       
                        break;

                    default:
                        break;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                MessageBox.Show(DBErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// this event used to search the user from datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int index;
                if (lblSearch.Text == "Full Name")
                    index = 2;
                else
                    index = 1;

                DataView dv = (DataView)dgUsers.DataSource;
                dgUsers.DataSource = dv;
                if (dv != null)
                {
                    string strSearch = txtSearch.Text.Trim();
                    strSearch.Replace("'", "");

                    if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                    {
                        dv.RowFilter = dv.Table.Columns[index].ColumnName + " Like '%" + strSearch + "%'";

                    }
                    else
                    {
                        dv.RowFilter = dv.Table.Columns[index].ColumnName + " Like '" + strSearch + "%'";
                    }
                    dgUsers.DataSource = dv;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;

            }
        }

        /// <summary>
        /// Display the column name on which search is to be performed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
                lblSearch.Text = "Login Name";
            if (e.ColumnIndex == 2)
                lblSearch.Text = "Full Name";
        }
              
    }
}