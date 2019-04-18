using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO; 
using System.Collections;

namespace Project_Reportview
{
    public partial class frmArrangeReport : Form
    {
        #region "Declaration"
        //private TreeNode sourceNode;
        string _strSelectedNode = String.Empty;
        string _pID = String.Empty;
        private string _databaseconnectionstring = "";
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        DataSet ds = new DataSet();
        string sReportType ;
        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;

        #endregion
        public frmArrangeReport(string _sReportType, string _gstrSQLServerName, string _gstrDatabaseName, bool _gblnSQLAuthentication, string _gstrSQLUser, string _gstrSQLPassword)
        {
            InitializeComponent();
            sReportType = _sReportType;
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;
        }

        #region "Fill Treeview "
        protected DataSet PDataset(string select_statement)
        {
            SqlConnection _con = new SqlConnection(_databaseconnectionstring);
            SqlDataAdapter ad = new SqlDataAdapter(select_statement, _con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            _con.Close();
            return ds;
        }


        public void Load_tree()
        {
            DataSet PrSet = PDataset("SELECT  ReportsID, ReportName,ReportsParentID FROM SSRSReports_MST where ReportType = '" + sReportType + "' Order By Reportsortorder");
            trv_Reports.Nodes.Clear();
            foreach (DataRow dr in PrSet.Tables[0].Rows)
            {
                if ((decimal)dr["ReportsParentID"] == 0)
                {
                    TreeNode tnParent = new TreeNode();
                    tnParent.Text = dr["ReportName"].ToString();
                    tnParent.Tag = dr["ReportsID"].ToString();
                    //string value = dr["ReportsID"].ToString();
                    trv_Reports.Nodes.Add(tnParent);
                    //tnParent.Expand();
                    // trv_Reports.Nodes.Add(dr["ReportsID"].ToString(), dr["ReportName"].ToString());

                    //tnParent = trv_Reports.Nodes[dr["ReportsID"].ToString()];  

                    //trv_Reports.Nodes.Find(dr["ReportsID"].ToString(), true);
                    FillChild(tnParent, dr["ReportsID"].ToString());
                    trv_Reports.ExpandAll();
                }
            }
        }

        public int FillChild(TreeNode parent, string IID)
        {

            DataSet ds = PDataset("SELECT ReportsID, ReportName FROM SSRSReports_MST WHERE ReportsParentID =" + IID + " And ReportType = '" + sReportType + "' Order By Reportsortorder ");
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TreeNode child = new TreeNode();
                    child.Text = dr["ReportName"].ToString().Trim();
                    child.Tag = dr["ReportsID"].ToString();
                    string temp = dr["ReportsID"].ToString();
                    child.Collapse();
                    parent.Nodes.Add(child);
                    FillChild(child, temp);
                }
                return 0;
            }
            else
            {
                return 0;
            }

        }
        #endregion

        #region "Form Load "
        private void frmArrangeReport_Load(object sender, EventArgs e)
        {
            Load_tree();
            ds = null;
        }
        #endregion

        #region "Comment Drag Drop "

        //private void trv_Reports_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    sourceNode = (TreeNode)e.Item;
        //    DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);

        //}

        //private void trv_Reports_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.Text))
        //    {
        //        e.Effect = DragDropEffects.Move;
        //    }
        //    else
        //    {
        //        e.Effect = DragDropEffects.None;
        //    }
        //}

        //private void trv_Reports_DragDrop(object sender, DragEventArgs e)
        //{
        //    Point pos = trv_Reports.PointToClient(new Point(e.X, e.Y));
        //    TreeNode targetNode = trv_Reports.GetNodeAt(pos);
        //    TreeNode nodeCopy;
        //    //string iTag = nodeCopy.Tag.ToString() ;
        //    if (targetNode != null)
        //    {
        //        nodeCopy = new TreeNode(sourceNode.Text, sourceNode.ImageIndex, sourceNode.SelectedImageIndex);

        //        if (sourceNode.Index > targetNode.Index)
        //        {

        //            targetNode.Parent.Nodes.Insert(targetNode.Index, nodeCopy);
        //            // targetNode.Parent.Nodes[targetNode.Index].Tag = nodeCopy.Tag.ToString();


        //        }
        //        else
        //            targetNode.Parent.Nodes.Insert(targetNode.Index + 1, nodeCopy);

        //        sourceNode.Remove();
        //        trv_Reports.Invalidate();
        //    }

        //}
      #endregion

        #region "Mouse Node Click"
        private void trv_Reports_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode trvnd = e.Node;

                if (trvnd != null)
                {
                    _strSelectedNode = string.Empty; 
                    _strSelectedNode = trvnd.Tag.ToString();            
                    trv_Reports.SelectedNode = trvnd; 
                    trv_Reports.Select();
                    

                }
                //trvBatch.SelectedNode = trvnd;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region "Edit Report Click "
        private void mnuItem_EditReport_Click(object sender, EventArgs e)
        {
            if (_strSelectedNode == "" || _strSelectedNode == "1" || _strSelectedNode == "2")
            {
                MessageBox.Show("No Reports to Edit.", sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }
            frmEditSSRSReport Objfrm = new frmEditSSRSReport(_strSelectedNode, "Edit", _databaseconnectionstring, null, sReportType, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword);
            Objfrm.ShowDialog(this);
            Objfrm.Dispose();
            Objfrm = null;
            Load_tree();

        }
        #endregion


        #region "Add Report Click "
        private void mnuItem_AddReport_Click(object sender, EventArgs e)
        {
            
            frmEditSSRSReport Objfrm = new frmEditSSRSReport(_strSelectedNode, "Add", _databaseconnectionstring, trv_Reports, sReportType, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword);
            Objfrm.ShowDialog(this);
            Objfrm.Dispose();
            Objfrm = null;         
            Load_tree();
            
        }
        #endregion

        #region "Delete Report Click"

        private void mnuItem_DeleteReport_Click(object sender, EventArgs e)
        {
            if (trv_Reports.SelectedNode == null)
            {
                MessageBox.Show("No Reports to Delete.", sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }
            gloDatabaseLayer.DBLayer ODB = null;
            ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            string _sQuery = "Select Count(*) as ReportName  from SSRSReports_MST where ReportsParentID= '" + trv_Reports.SelectedNode.Tag + "' and ReportType= '" + sReportType + "'";
            int intCount = (int)ODB.ExecuteScalar_Query(_sQuery);

            if (ODB != null) { ODB.Dispose(); ODB = null; }
            _sQuery = null;

            if (intCount != 0)
            {
                MessageBox.Show("Submenu can not be deleted because it contains reports under it. Delete all the reports first.", sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                if (trv_Reports.SelectedNode.Tag.ToString() == "1" || trv_Reports.SelectedNode.Tag.ToString() == "2")
                {
                    MessageBox.Show("Root menu cannot be deleted. ", sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Do you want to delete the report? ", sReportType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    objDbLayer.Connect(false);
                    objDbParameters.Add("@ReportsID", trv_Reports.SelectedNode.Tag, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@ReportName", "", ParameterDirection.Input, SqlDbType.VarChar, 255);
                    objDbParameters.Add("@ReportFileName", "", ParameterDirection.Input, SqlDbType.VarChar, 255);
                    objDbParameters.Add("@ReportSortOrder", 0, ParameterDirection.Input, SqlDbType.Int);
                    objDbParameters.Add("@ReportsParentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@IsgloStreamReport", "N", ParameterDirection.Input, SqlDbType.Char, 1);
                    objDbParameters.Add("@ReportType", sReportType, ParameterDirection.Input, SqlDbType.VarChar, 10);
                    objDbParameters.Add("@Mode", "Delete", ParameterDirection.Input, SqlDbType.VarChar, 10);

                    objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                    objDbLayer.Disconnect();
                    if (objDbParameters != null) { objDbParameters.Dispose(); objDbParameters = null; }
                    trv_Reports.Nodes.Remove(trv_Reports.SelectedNode);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }


        }

        #endregion

        #region "cancel Click "
        private void tls_btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void trv_Reports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trv_Reports.SelectedNode.Tag != null)
            {
                 _strSelectedNode = trv_Reports.SelectedNode.Tag.ToString();

            }
          
        }
        #region "UP Click"
        private void btn_UP_Click(object sender, EventArgs e)
        {
            if (_strSelectedNode == "" || trv_Reports.SelectedNode == null)
            {
                return;
            }
            TreeNode Node = trv_Reports.SelectedNode;
            TreeNode PrevNode = Node.PrevNode;
            if (PrevNode != null)
            {

                TreeNode NewNode = (TreeNode)Node.Clone();
                NewNode.Tag = Node.Tag;
                if (Node.Parent == null)
                {
                    trv_Reports.Nodes.Insert(PrevNode.Index, NewNode);
                }
                else
                {
                    Node.Parent.Nodes.Insert(PrevNode.Index, NewNode);
                }
                Node.Remove();
                trv_Reports.SelectedNode = NewNode;
            }
        }
#endregion

        #region "Down Click "
        private void btn_Down_Click(object sender, EventArgs e)
        {
            if (_strSelectedNode == "" || trv_Reports.SelectedNode == null)
            {
                return;
            }

            TreeNode Node = trv_Reports.SelectedNode;
            TreeNode NextNode = Node.NextNode;
            if (NextNode != null)
            {

                TreeNode NewNode = (TreeNode)Node.Clone();
                NewNode.Tag = Node.Tag;
                if (Node.Parent == null)
                {
                    trv_Reports.Nodes.Insert(NextNode.Index + 1, NewNode);
                }
                else
                {
                    Node.Parent.Nodes.Insert(NextNode.Index + 1, NewNode);
                }
                Node.Remove();
                trv_Reports.SelectedNode = NewNode;
            }
            Node =null ;
            NextNode = null;
        }
        #endregion

        #region "Save & Close"
        private void tls_btn_SaveCls_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer ODB = null;
           
            ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            //string _strqry = "Select ReportsID from SSRSReports_MST  Where ReportType= '" + sReportType + "' ";
            string _strqry = "Select [ReportsID],[ReportName],[ReportFileName],[ReportSortOrder],[ReportsParentID],[IsgloStreamReport],[ReportType] from SSRSReports_MST  Where ReportType= '" + sReportType + "' ";
            ODB.Retrive_Query(_strqry, out ds);
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count == 1)
            {
                MessageBox.Show("No reports found.", sReportType, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _strqry = null;


            ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            string _strquery = "Delete from SSRSReports_MST where ReportsParentID <> '0'  and ReportType= '" + sReportType + "'";
            ODB.Execute_Query(_strquery);
            ODB = null;
            _strquery = null;

            //to save  node

            FindNodeInHierarchy(trv_Reports.Nodes[0]);

            //MessageBox.Show("Record Saved.");
            this.Close();

           // label1.Text = GetTotalNodes(trv_Reports).ToString();
            
        }
       // ArrayList temp = new ArrayList();
        #endregion

        #region "Recursive Function FindNodeInHierarchy"
        private void FindNodeInHierarchy(TreeNode Node)
        {
            
            //TreeNode root = trv_Reports.Nodes[0];

            for (int iCount = 0; iCount < Node.Nodes.Count; iCount++)
            {
                //save
                //Node.Nodes[iCount].Text;
                //Node.Nodes[iCount].Parent.Tag

                //save
                gloDatabaseLayer.DBParameters objDbParameters = null;
                string sFileName = null;
                try
                {


                    gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    objDbParameters = new gloDatabaseLayer.DBParameters();
                    objDbLayer.Connect(false);
                    Int32 rID = Convert.ToInt32(Node.Nodes[iCount].Tag);
                    DataRow[] drFileName = ds.Tables[0].Select("ReportName = '" + Node.Nodes[iCount].Text + "'");
                    if (drFileName.Count() > 0)
                    {
                        DataRow dr = drFileName[0];
                        if (Convert.ToString(dr["ReportFileName"]) == "")
                        {
                            sFileName = null;
                        }
                        else
                        {
                            sFileName = Convert.ToString(dr["ReportFileName"]);
                        }
                    }
                    drFileName = null;

                    //DataView dv = new DataView(ds.Tables[0], "ReportName = '" + Node.Nodes[iCount].Text + "'", "ReportsID", DataViewRowState.CurrentRows);

                    //if (Convert.ToString(dv.Table.Rows[0]["ReportFileName"]) == "")
                    //   {
                    //       sFileName = null;
                    //   }
                    //   else
                    //   {
                    //       sFileName = Convert.ToString(dv.Table.Rows[0]["ReportFileName"]);
                    //   }
                    objDbParameters.Add("@ReportsID", rID, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@ReportName", Node.Nodes[iCount].Text, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    objDbParameters.Add("@ReportFileName", sFileName, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    objDbParameters.Add("@ReportSortOrder", iCount, ParameterDirection.Input, SqlDbType.Int);
                    objDbParameters.Add("@ReportsParentID", Node.Nodes[iCount].Parent.Tag, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@IsgloStreamReport", "N", ParameterDirection.Input, SqlDbType.Char, 1);
                    objDbParameters.Add("@ReportType", sReportType, ParameterDirection.Input, SqlDbType.VarChar, 10);
                    objDbParameters.Add("@Mode", "Add", ParameterDirection.Input, SqlDbType.VarChar, 10);


                    //DeployReport(txtReportName.Text, txtFileName.Text, sReportfolder, false);


                    objDbLayer.Execute("INUP_SSRSReports_MST", objDbParameters);
                    objDbLayer.Disconnect();




                    FindNodeInHierarchy(Node.Nodes[iCount]);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (objDbParameters != null) { objDbParameters.Dispose(); objDbParameters = null; }
                    sFileName = null;
                }

            }



        }

        #endregion




    }
}
