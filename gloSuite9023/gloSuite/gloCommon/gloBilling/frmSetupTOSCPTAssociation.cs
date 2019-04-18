using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupTOSCPTAssociation : Form
    {

        #region " Variable Declarations "

            private string _databaseconnectionstring = "";
            private string _messageBoxCaption = String.Empty;
            private Int64 _ClinicID = 0;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            private DataView _dv;
            
        #endregion " Variable Declarations "

        #region  " C1 Grid Variable Declarations "

        private int COL_TOSID = 0;
        private int COL_TOSNAME = 1;

        private int COL_COUNT = 2;

        private int _Width = 0;

        #endregion  " C1 Grid Variable Declarations "

        #region " Property Procedures "

            public string DatabaseConnectionString
            {
                get { return _databaseconnectionstring; }
                set { _databaseconnectionstring = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            #endregion " Property Procedures "
        
        #region " Constructor "

            public frmSetupTOSCPTAssociation(string databaseConnectionstring)
            {
                InitializeComponent();

                _databaseconnectionstring = databaseConnectionstring;
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

        #endregion " Constructor "

        #region " Tool Strip Click Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            SaveAssociation();
                            
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "ADD":
                        {
                            AddCPTs();
                            
                        }
                        break ;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            { 
                
            }
        }

       
        #endregion " Tool Strip Click Event "

        #region " Tree View Fill Methods "

        private void FillControls()
        {
            FillTOS();
            FillCPTs();
        }

        private void FillTOS()
        {
            //Initialize Type of Service Class Object 
            CLsBL_TOSPOS oTOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            DataTable dtTOS = new DataTable();
            //

            try
            {
                #region " Type Of Service Tree Fill "

                //Clear all nodes first.
                trvTOS.Nodes.Clear();

                // Add Node at Level 0.
                trvTOS.Nodes.Add("Type Of Service");
                trvTOS.Nodes[0].ImageIndex = 1;
                trvTOS.Nodes[0].SelectedImageIndex = 1;
                //
                

                // GET All Type Of Services and bind to treeview
                //Pass 0 to get all Type Of Service
                dtTOS = oTOS.GetTOS(0);


                //Sorting 
                _dv = dtTOS.DefaultView;

                if (rbTOSCode.Checked == true)
                {
                    _dv.Sort = _dv.Table.Columns["sTOSCode"].ColumnName;
                }
                else
                {
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;
                }

                    dtTOS = _dv.ToTable();
                ///


                //Check for Table not null
                if (dtTOS != null)
                {
                    //Check for Table not empty
                    if (dtTOS.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTOS.Rows.Count; i++)
                        {
                            //Create Node for each Table Item sTOSCode
                            TreeNode oNode = new TreeNode();
                            oNode.Text = dtTOS.Rows[i]["sTOSCode"].ToString() + " - " + dtTOS.Rows[i]["sDescription"].ToString();
                            oNode.Tag = dtTOS.Rows[i]["nTOSID"].ToString();
                            //
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;

                            //Add Node to Type Of Service Tree
                            trvTOS.Nodes[0].Nodes.Add(oNode);
                            //

                            oNode = null;

                        }//for (int i = 0; i < dtTOS.Rows.Count ; i++)

                    }//if (dtTOS.Rows.Count > 0)

                    trvTOS.ExpandAll();

                } //if (dtTOS != null) 

                #endregion " Type Of Service Tree Fill "
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oTOS.Dispose();
                dtTOS.Dispose();
                
            }

        }

        private void FillCPTs()
        {
            CPT oCPT = new CPT(_databaseconnectionstring);
            DataTable dt_cpt = new DataTable();

            try
            {
               #region " CPT Tree Fill "

                //Clear Treee Nodes
                trvCPT.Nodes.Clear();

                // Add Parent Node
                trvCPT.Nodes.Add("CPT");
                //
                trvCPT.Nodes[0].ImageIndex = 2;
                trvCPT.Nodes[0].SelectedImageIndex = 2;

                // GET All CPT and bind to treeview
                dt_cpt = oCPT.GetCPTs();

                #region " Sort Data "

                _dv = dt_cpt.DefaultView;

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                }
                else
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;

                //

                dt_cpt = _dv.ToTable();

                #endregion " Sort Data " 
                

                if (dt_cpt != null)
                {
                    if (dt_cpt.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dt_cpt.Rows.Count - 1; i++)
                        {
                            // create and set value to CPTs Nodes.
                            TreeNode tNode = new TreeNode();
                            tNode.Text = dt_cpt.Rows[i]["sCPTCode"].ToString() +" - "+ dt_cpt.Rows[i]["sDescription"].ToString();
                            tNode.Tag = dt_cpt.Rows[i]["nCPTID"].ToString();
                            tNode.ImageIndex = 0;
                            tNode.SelectedImageIndex = 0;
                           // tNode.ImageKey = dt_cpt.Rows[i]["sCPTCode"].ToString();
                                                        
                            // Add Node to CPT tree.
                            trvCPT.Nodes[0].Nodes.Add(tNode);
                         }
                    }

                    // Show tree Expanded;
                    trvCPT.ExpandAll();
                    trvCPT.SelectedNode = trvCPT.Nodes[0];
                    
                }

                #endregion " CPT Tree Fill "

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oCPT.Dispose();
                dt_cpt.Dispose();
            }
            
        }

        private void FillICD9s()
        {
            ICD9 oICD9 = new ICD9(_databaseconnectionstring);
            DataTable dtICD9 = new DataTable();

            try
            {
                #region " ICD9 Tree Fill "

                //Clear Treee Nodes
                trvCPT.Nodes.Clear();

                // Add Parent Node
                trvCPT.Nodes.Add("ICD9");
                //
                trvCPT.Nodes[0].ImageIndex = 3;
                trvCPT.Nodes[0].SelectedImageIndex = 3;
                //

                // GET All CPT and bind to treeview
                dtICD9 = oICD9.GetICD9s();


                #region " Sort Data "

                _dv = dtICD9.DefaultView;

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                }
                else
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;

                //

                dtICD9 = _dv.ToTable();

                #endregion " Sort Data " 


                if (dtICD9 != null)
                {
                    if (dtICD9.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtICD9.Rows.Count - 1; i++)
                        {
                            // create and set value to CPTs Nodes.
                            TreeNode tNode = new TreeNode();
                            tNode.Text = dtICD9.Rows[i]["sICD9Code"].ToString() +" - "+ dtICD9.Rows[i]["sDescription"].ToString();
                            tNode.Tag = dtICD9.Rows[i]["nICD9ID"].ToString();

                            tNode.ImageIndex = 0;
                            tNode.SelectedImageIndex = 0;

                            // Add Node to CPT tree.
                            trvCPT.Nodes[0].Nodes.Add(tNode);
                        }
                    }

                    // Show tree Expanded;
                    trvCPT.ExpandAll();
                    trvCPT.SelectedNode = trvCPT.Nodes[0];
                }

                #endregion " CPT Tree Fill "

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oICD9.Dispose();
                dtICD9.Dispose();
            }
        }

        #endregion " Tree View Fill Methods "

        #region " Form Load Event "

        private void frmSetupTOSCPTAssociation_Load(object sender, EventArgs e)
        {

            try
            {
                FillControls();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
                
            }

        }

        #endregion " Form Load Event "

        #region " Design C1 Grid "

        private void designC1Grid()
        {
            try
            {
                _Width = pnlMiddle.Width;

                C1TOSCPT.Rows.Fixed = 1;
                C1TOSCPT.Cols.Fixed = 0;
                C1TOSCPT.Cols.Count = COL_COUNT;
                C1TOSCPT.Rows.Count = 1;

                C1TOSCPT.Tree.Column = COL_TOSNAME;

                // Set Property for TOS_ID
                C1TOSCPT.Cols[COL_TOSID].Width = 0;
                C1TOSCPT.Cols[COL_TOSID].AllowEditing = false;
                C1TOSCPT.SetData(0, COL_TOSID, "TOS ID");
                //

                // Set Property for COL_TOSName
                //C1TOSCPT.Cols[COL_TOSNAME].Width = (int)(_Width * 0.5);
                C1TOSCPT.Cols[COL_TOSNAME].Width = (int)(_Width);
                C1TOSCPT.Cols[COL_TOSNAME].AllowEditing = false;
                C1TOSCPT.SetData(0, COL_TOSNAME, "Type Of Service Name");
                //

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
                
            }
        }

        #endregion " Design C1 Grid "

        #region " Type Of Service Tree Double Click Event "
        
        //private void trvTOS_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // On root Node Hit do nothing
        //        if (trvTOS.SelectedNode == null || trvTOS.SelectedNode.Level == 0)
        //        {
        //            trvTOS.ExpandAll();
        //            return;
        //        }

        //        Boolean boolINSExist = false;

        //        // Before Adding TOS node check is already present
        //        for (int cnt = 1; cnt <= C1TOSCPT.Rows.Count - 1; cnt++)
        //        {

        //            // if current node is not TOS node then continue;
        //            if (C1TOSCPT.Rows[cnt].Node.Level != 0)
        //                continue;


        //            // current node is TOS node and check already exist;
        //            if (C1TOSCPT.Rows[cnt].Node.Data.ToString() == trvTOS.SelectedNode.Text)
        //            {
        //                // Quit for loop on already exist after selecting;
        //                boolINSExist = true;

        //                C1TOSCPT.Rows[cnt].Node.Select();
        //                break;
        //            }
        //        }


        //        // Node is not in grid so to add node to grid block of code is
        //        if (boolINSExist == false)
        //        {
        //            string strcategoryName = trvTOS.SelectedNode.Text;
        //            // Add new row to C1Flexgrid.
        //            C1TOSCPT.Rows.Add();
                    
        //            C1TOSCPT.Tree.Indent = 21;
        //            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
        //            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
        //            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
        //            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = strcategoryName;

                    
        //            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Select();
                    
        //            // Set ID to row
        //            C1TOSCPT.SetData(C1TOSCPT.Rows.Count - 1, COL_TOSID, trvTOS.SelectedNode.Tag.ToString());
                    
        //            // Procedure Get and Bind the associated CPT and other Information.
        //            GetAssociatedCPTS();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void trvTOS_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // On root Node Hit do nothing
                if (trvTOS.SelectedNode == null || trvTOS.SelectedNode.Level == 0)
                {
                    trvTOS.ExpandAll();
                    return;
                }

                Boolean boolINSExist = false;

                // Before Adding TOS node check is already present
                for (int cnt = 1; cnt <= C1TOSCPT.Rows.Count - 1; cnt++)
                {

                    // if current node is not TOS node then continue;
                    if (C1TOSCPT.Rows[cnt].Node.Level != 0)
                        continue;


                    // current node is TOS node and check already exist;
                    if (C1TOSCPT.Rows[cnt].Node.Data.ToString() == trvTOS.SelectedNode.Text)
                    {
                        // Quit for loop on already exist after selecting;
                        boolINSExist = true;

                        C1TOSCPT.Rows[cnt].Node.Select();
                        break;
                    }
                }


                // Node is not in grid so to add node to grid block of code is
                if (boolINSExist == false)
                {
                    string strcategoryName = trvTOS.SelectedNode.Text;
                    // Add new row to C1Flexgrid.
                    C1TOSCPT.Rows.Add();

                    C1TOSCPT.Tree.Indent = 21;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = strcategoryName;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Key = trvTOS.SelectedNode.Tag.ToString();


                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Select();

                    // Set ID to row
                    C1TOSCPT.SetData(C1TOSCPT.Rows.Count - 1, COL_TOSID, trvTOS.SelectedNode.Tag.ToString());

                    //add CPT Node
                    C1TOSCPT.Rows.Add();
                    C1TOSCPT.Tree.Indent = 21;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = "CPT";

                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Select();
                    //

                    //add ICD9 Node
                    C1TOSCPT.Rows.Add();
                    C1TOSCPT.Tree.Indent = 21;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = "ICD9";

                    //C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Select();
                    //

                    ////select the parent node

                    //C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();

                    ////


                    // Procedure Get and Bind the associated CPT and other Information.
                    GetAssociatedCPTS();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        #endregion " Type Of Service Tree Double Click Event "

        #region " Form Shown Event "

        private void frmSetupTOSCPTAssociation_Shown(object sender, EventArgs e)
        {
            try
            {
                designC1Grid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion " Form Shown Event "

        #region " CPT Tree Double Click Event "

        //private void trvCPT_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        //On root Node Hit or Before Adding Type Of Service CPT is added.
        //        if (trvCPT.SelectedNode == null || trvCPT.SelectedNode.Level == 0 || C1TOSCPT.Rows.Count == 1)
        //        {
        //            trvCPT.ExpandAll();
        //            return;
        //        }
        //        /// Get the parent(TOS) of the current node(CPT) 
        //        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
        //        {
        //            //Select Parent node
        //            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
        //        }

        //        //Get child count for node
        //        int a = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children;

        //        //Check if CPT is already present under current TOS node
        //        Boolean boolCPTExist = false;
        //        for (int cnt = 1; cnt <= a; cnt++)
        //        {
        //            if (C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Data.ToString() == trvCPT.SelectedNode.Text)
        //            {
        //                //Quit for loop on already exist after selected;
        //                boolCPTExist = true;

        //                C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Select();
        //                break;
        //            }

        //        }

        //        //Add node as not present under current TOS node
        //        if (boolCPTExist == false)
        //        {
        //            string stCPTName = trvCPT.SelectedNode.Text;
        //            Int32 tempRow;
        //            tempRow = C1TOSCPT.Row;
        //            C1TOSCPT.Tree.Indent = 21;

        //            //Add node as Child for selected node is parent (TOS)
        //            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
        //            {
        //                //If Select Row is TOS Node
        //                C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, stCPTName,trvCPT.Nodes[0].Text,null);
        //                C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

        //            }
        //            else if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1)
        //            {
        //                //Add node as sibling for selected node is Child(CPT) 
        //                // For Selected Node IS CPT Node
        //                C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastSibling, stCPTName,trvCPT.Nodes[0].Text,null);
        //                C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastSibling).Select();
        //            }

        //            //Set ID to row
        //            C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, trvCPT.SelectedNode.Tag.ToString());

        //        }

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {

        //    }
        //}

        private void trvCPT_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                //Check for if Parent Node(Type Of Service) is added if not do not add the CPT

                if (trvCPT.SelectedNode == null || trvCPT.SelectedNode.Level == 0 || C1TOSCPT.Rows.Count == 1)
                {
                    trvCPT.ExpandAll();
                    if (C1TOSCPT.Rows.Count == 1)
                    {
                        MessageBox.Show("Please select type of service to associate.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                //

                //If the level is 0 i.e if Parent(TOS) node if selected then
                //select the respective child (i.e CPT or ICD9) depending upon
                //wether CPT or ICD9 tree if filled(right).
                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
                {
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    { 
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                        int i = C1TOSCPT.RowSel;
                    }
                }
                //


                /// Get the parent(TOS) of the current node(CPT) 
                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level >= 1)
                {
                    //Select Parent node
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    {
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 2) 
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        }
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();

                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 2)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        }
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

                    }
                }
                //



                //Get child count for node
                int a = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children;
                //

                //Check if CPT is already present under current TOS node
                Boolean boolCPTExist = false;
                for (int cnt = 1; cnt <= a; cnt++)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Data.ToString() == trvCPT.SelectedNode.Text)
                    {
                        //Quit for loop on already exist after selected;
                        boolCPTExist = true;
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        //
                        break;
                    }

                }
                //

                //Add node as not present under current TOS node
                if (boolCPTExist == false)
                {
                    string stCPTName = trvCPT.SelectedNode.Text;
                    Int32 tempRow;
                    tempRow = C1TOSCPT.Row;
                    C1TOSCPT.Tree.Indent = 21;

                    //Add node as Child for selected node is parent (TOS)
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1)
                    {
                        //If Select Row is TOS Node
                        C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, stCPTName, trvCPT.Nodes[0].Text, null);
                        C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

                    }
                    
                    //Set ID to row
                    C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, trvCPT.SelectedNode.Tag.ToString());

                    //select the respective parent node of the added node
                    C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                    //

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion " CPT Tree Double Click Event "

        #region " Private method to get Associated CPT of Type Of Service "

        private void GetAssociatedCPTS()
        {
            TOSCPT oTOSCPT = new TOSCPT(_databaseconnectionstring);
            DataTable dt;
            Int64 _tosId = 0;
            try
            {
                //get the selected Type of service ID;
                _tosId = Convert.ToInt64(trvTOS.SelectedNode.Tag);
                //
               
                //first get the CPT's associated with the Type of service if any
                dt = oTOSCPT.GetAssociatedCPT(_tosId);
                //

                //Check for dt != null
                if (dt != null & dt.Rows.Count > 0)
                {
                    //add the cpt's to the grid
                    for (int i = 0; i < dt.Rows.Count ; i++)
                    {
                        for (int j = 1; j < C1TOSCPT.Rows.Count; j++)
                        {
                            if (C1TOSCPT.Rows[j].Node.Level == 1 && C1TOSCPT.Rows[j].Node.Data.ToString() == "CPT" )                          
                            {
                                if (C1TOSCPT.Rows[j].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Key.ToString() == _tosId.ToString())
                                {

                                    C1TOSCPT.Rows[j].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dt.Rows[i]["sCPTCode"].ToString() + " - " + dt.Rows[i]["sCPTDesc"].ToString(), "CPT", null);
                                    C1TOSCPT.Rows[j].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                                    //Set ID to row
                                    Int64 cptid = oTOSCPT.GetCPTID(dt.Rows[i]["sCPTCode"].ToString());
                                    C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, cptid);
                                }
                            }
                        }
                    }
                }
                dt.Dispose();
                //

                //Get the ICD9's associated with the this Type Of Service if any

                dt = new DataTable();
                dt = oTOSCPT.GetAssciatedICD9(_tosId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 1; j < C1TOSCPT.Rows.Count; j++)
                        {

                            if (C1TOSCPT.Rows[j].Node.Level == 1 && C1TOSCPT.Rows[j].Node.Data.ToString() == "ICD9")
                            {
                                if (C1TOSCPT.Rows[j].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Key.ToString() == _tosId.ToString())
                                {
                                    C1TOSCPT.Rows[j].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dt.Rows[i]["sICD9Code"].ToString() + " - " + dt.Rows[i]["sICD9Desc"].ToString(), "ICD9", null);
                                    C1TOSCPT.Rows[j].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                                    Int64 icd9id = oTOSCPT.GetICD9ID(dt.Rows[i]["sICD9Code"].ToString());
                                    //C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, icd9id);
                                    C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, icd9id);
                                }
                            }
                        }
                    }
                }

                
                //select the respective parent node of the added node
                int rowIndex = C1TOSCPT.RowSel;
                //C1TOSCPT.Rows[].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                //
                if (C1TOSCPT.Rows[rowIndex].Node.Level == 2)
                { 
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                }
                else if (C1TOSCPT.Rows[rowIndex].Node.Level == 1)
                {
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
                
            }
        }

        #endregion " Private method to get Associated CPT of Type Of Service "

        #region  " Private Method AddCPTs "

        private void AddCPTs()
        {
            gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();

            try
            {

                //Check if Parent node(TOS) is present if not return
                if (C1TOSCPT.Rows.Count == 1)
                {
                    MessageBox.Show("Please select a type of service to associate.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //Get all selected CPTs
                for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
                {
                    if (trvCPT.Nodes[0].Nodes[i].Checked == true)
                    {
                        oItems.Add(Convert.ToInt64(trvCPT.Nodes[0].Nodes[i].Tag), trvCPT.Nodes[0].Nodes[i].Text);
                    }
                }

                ////On root Node Hit or Before Adding Insurance CPT is added.
                //if (trvCPT.SelectedNode == null || trvCPT.SelectedNode.Level == 0)
                //{
                //    trvCPT.ExpandAll();
                //    return;
                //}
                if (oItems == null || oItems.Count <= 0)
                {
                    trvCPT.ExpandAll();
                    return;
                }



                for (int j = 0; j < oItems.Count; j++)
                {
                    //If the level is 0 i.e if Parent(TOS) node if selected then
                    //select the respective child (i.e CPT or ICD9) depending upon
                    //wether CPT or ICD9 tree if filled(right).
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
                    {
                        if (pnl_btnCPT.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                        }
                        if (pnl_btnICD9.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                            int i = C1TOSCPT.RowSel;
                        }
                    }
                    //


                    /// Get the parent(TOS) of the current node(CPT) 
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level >= 1)
                    {
                        //Select Parent node
                        if (pnl_btnCPT.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 2)
                            {
                                C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            }
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();

                        }
                        if (pnl_btnICD9.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 2)
                            {
                                C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            }
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

                        }
                    }
                    //



                    //Get child count for node
                    int a = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children;
                    //

                    //Check if CPT is already present under current TOS node
                    Boolean boolCPTExist = false;
                    for (int cnt = 1; cnt <= a; cnt++)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Data.ToString() == oItems[j].Description)
                        {
                            //Quit for loop on already exist after selected;
                            boolCPTExist = true;
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            //
                            break;
                        }

                    }
                    //

                    //Add node as not present under current TOS node
                    if (boolCPTExist == false)
                    {
                        //string stCPTName = trvCPT.SelectedNode.Text;
                        string stCPTName = oItems[j].Description;
                        Int32 tempRow;
                        tempRow = C1TOSCPT.Row;
                        C1TOSCPT.Tree.Indent = 21;

                        //Add node as Child for selected node is parent (TOS)
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1)
                        {
                            //If Select Row is TOS Node
                            C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, stCPTName, trvCPT.Nodes[0].Text, null);
                            C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

                        }

                        //Set ID to row
                        //C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, trvCPT.SelectedNode.Tag.ToString());
                        C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, oItems[j].ID.ToString());

                        //select the respective parent node of the added node
                        C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        //

                    }


                }//for (int i = 0; i < oItems.Count; i++)


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oItems != null)
                {
                    oItems.Clear();
                    oItems.Dispose();
                    oItems = null;
                }
            }
        }

        #region  ' commented old add method '
        //private void AddCPTs()
        //{
        //    gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();

        //    try
        //    {

        //        //Check if Parent node(TOS) is present if not return
        //        if (C1TOSCPT.Rows.Count == 1)
        //        {
        //            MessageBox.Show("Please select a Type Of Service first", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }


        //        //Get all selected CPTs
        //        for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
        //        {
        //            if (trvCPT.Nodes[0].Nodes[i].Checked == true)
        //            {
        //                oItems.Add(Convert.ToInt64(trvCPT.Nodes[0].Nodes[i].Tag), trvCPT.Nodes[0].Nodes[i].Text);
        //            }
        //        }

        //        ////On root Node Hit or Before Adding Insurance CPT is added.
        //        //if (trvCPT.SelectedNode == null || trvCPT.SelectedNode.Level == 0)
        //        //{
        //        //    trvCPT.ExpandAll();
        //        //    return;
        //        //}
        //        if (oItems == null || oItems.Count <= 0)
        //        {
        //            trvCPT.ExpandAll();
        //            return;
        //        }



        //        for (int i = 0; i < oItems.Count; i++)
        //        {
        //            /// Get the parent(TOS) of the current node(CPT) 
        //            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
        //            {
        //                //Select Parent node
        //                C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
        //            }

        //            //Get child count for node
        //            int a = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children;
        //            //

        //            //Check if CPT is already present under current insurance node
        //            Boolean boolCPTExist = false;

        //            for (int cnt = 1; cnt <= a; cnt++)
        //            {
        //                if (C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Data.ToString() == oItems[i].Description)
        //                {
        //                    //Quit for loop on already exist after selected;
        //                    boolCPTExist = true;
        //                    C1TOSCPT.Rows[C1TOSCPT.Row + cnt].Node.Select();
        //                    break;
        //                }

        //            }

        //            //Add node as not present under current TOS node
        //            if (boolCPTExist == false)
        //            {
        //                //string stCPTName = trvCPT.SelectedNode.Text;
        //                string stCPTName = oItems[i].Description;
        //                Int32 tempRow;
        //                tempRow = C1TOSCPT.Row;
        //                C1TOSCPT.Tree.Indent = 21;

        //                //Add node as Child for selected node is parent (TOS)
        //                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
        //                {
        //                    //If Select Row is TOS Node
        //                    C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, stCPTName, trvCPT.Nodes[0].Text, null);
        //                    C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();

        //                }
        //                else if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1)
        //                {
        //                    //Add node as sibling for selected node is Child(CPT) 
        //                    // For Selected Node IS CPT Node
        //                    C1TOSCPT.Rows[tempRow].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastSibling, stCPTName, trvCPT.Nodes[0].Text, null);
        //                    C1TOSCPT.Rows[tempRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastSibling).Select();
        //                }

        //                //Set ID to row
        //                C1TOSCPT.SetData(C1TOSCPT.Row, COL_TOSID, oItems[i].ID.ToString());

        //            }//if (boolCPTExist == false)


        //        }//for (int i = 0; i < oItems.Count; i++)


        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {

        //    }
        //}
        #endregion  ' commented old add method '

        #endregion  " Private Method AddCPTs "

        #region " CPT ICD9 Button Click Events "

        private void btnCPT_Click(object sender, EventArgs e)
        {
            try
            {
               pnl_btnICD9.Dock = DockStyle.Bottom;
               pnl_btnCPT.Dock = DockStyle.Top;
               btnICD9.Dock = DockStyle.Bottom;
               btnCPT.Dock = DockStyle.Top;
                
                FillCPTs();

                //first check if nodes are added to grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "ICD9")
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                        }
                    }
                }

                //deselect the check box
                chkBoxSelect.Checked = false;
                //

                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
                
            }
        }

        private void btnICD9_Click(object sender, EventArgs e)
        {
            try
            {
               pnl_btnCPT.Dock = DockStyle.Bottom;
               pnl_btnICD9.Dock = DockStyle.Top;
               
                btnCPT.Dock = DockStyle.Bottom;
                btnICD9.Dock = DockStyle.Top;

                //btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                //btnCPT.BackgroundImageLayout = ImageLayout.Stretch;
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;



                FillICD9s();
                //first check if nodes are added to the grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "CPT")
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                        }
                    }
                }


                //deselect the check box
                chkBoxSelect.Checked = false;
                //
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion " CPT ICD9 Button Click Events "

        #region " Designer Events "

        private void btnCPT_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCPT_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnCPT.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnICD9_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnICD9_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnICD9.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        #endregion " Designer Events "

        #region "Mouse Events Remove CPT or ICD9"

        private void C1TOSCPT_MouseDown(object sender, MouseEventArgs e)
        {

            // Select the Current Row
            if (C1TOSCPT.HitTest(e.X, e.Y).Row > 0)
            {
                C1TOSCPT.Rows[C1TOSCPT.HitTest(e.X, e.Y).Row].Node.Select();

                C1.Win.C1FlexGrid.CellRange cellRange = C1TOSCPT.Rows[C1TOSCPT.HitTest(e.X, e.Y).Row].Node.GetCellRange();
                C1.Win.C1FlexGrid.Node oNode = C1TOSCPT.Rows[C1TOSCPT.HitTest(e.X, e.Y).Row].Node;
                bool _showContextMenu = false;

                if (oNode.Level == 0)
                {
                    for (int i = 0; i < oNode.Children; i++)
                    {
                       C1.Win.C1FlexGrid.Node oTempCPTNode = oNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild);
                       C1.Win.C1FlexGrid.Node oTempICDNode = oNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild);
                       if (oTempCPTNode.Children > 0 || oTempICDNode.Children > 0)
                       {
                           _showContextMenu = true;
                           break;
                       }
                    }
                }
                else if (oNode.Level == 1)
                {
                    if (oNode.Children > 0)
                    {
                        _showContextMenu = true;
                    }
                }
                else if (oNode.Level == 2)
                {
                    _showContextMenu = true;
                }


                // If Right Click Then show the menu with cursor location.
                if (e.Button.ToString() == "Right" && _showContextMenu == true)
                {
                    cxtMS.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check type of Deletion selected
            // Remove all CPT's & ICD9's associated with the TOS if Parent Node selected (ie TOS node)
            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
            {
                //For the insurance Node Delete the Cpts with it
                if(MessageBox.Show("Are you sure you want to remove all items associated with this type of service?",_messageBoxCaption,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                    //first remove all child nodes if any of the first child(CPT) of Parent(TOS)
                    for (int i = 0; C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children != 0; i++)
                    {
                        C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.RemoveNode();
                    }
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                    C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                    //first remove all child nodes if any of the first child(CPT) of Parent(TOS)
                    for (int i = 0; C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children != 0; i++)
                    {
                        C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.RemoveNode();
                    }
                }
                
            }
            else if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1) //Remove all CPT's or ICD9's child nodes
            {
                string nodeTitle = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Data.ToString();
                
                
                    if (MessageBox.Show("Are you sure you want to remove all associated " + nodeTitle + "?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //first remove all child nodes if any
                        for (int i = 0; C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children != 0; i++)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.RemoveNode();
                        }
                    }
                
            }
            else // remove single item
            {
                // For Delete the Cpts selected.
                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 2)
                {
                    C1TOSCPT.RemoveItem(C1TOSCPT.Row);
                }
            }
        }

        #endregion "Mouse Events for Delete"

        #region " Private Method Save Association "

        //private void SaveAssociation()
        //{
        //    gloGeneralItem.gloItem oItem;
        //    gloGeneralItem.gloSubItems oSubItems;
        //    CPT oCPT = new CPT(_databaseconnectionstring);
        //    ICD9 oICD9 = new ICD9(_databaseconnectionstring);
        //    DataTable dt;
        //    TOSCPT oTOSCPT;

        //    try
        //    {
        //        // Scan every node of C1 Flex Grid to get values from Rows.
        //        for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
        //        {
        //            // Scan every CPT/ICD9 information if node is TOS node only of C1 Flex Grid.
        //            if (C1TOSCPT.Rows[cntTOS].Node.Level == 0)
        //            {
        //                oItem = new gloGeneralItem.gloItem();

        //                // Scan every CPT information of C1 Flex Grid.
        //                for (int cntCPT = 1; cntCPT <= C1TOSCPT.Rows[cntTOS].Node.Children; cntCPT++)
        //                {
        //                    // set Object for TOS-CPT/ICD9 Entry
        //                    oItem.ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TOSID));

        //                    // Set collected info to the Object.
        //                    string item_type = C1TOSCPT.Rows[cntTOS+cntCPT].Node.Key.ToString();  //GetData(cntTOS + cntCPT, COL_TOSID).ToString();
        //                    switch (item_type )
        //                    {
                                 
        //                        case "CPT":
        //                            {
        //                                oSubItems = new gloGeneralItem.gloSubItems();
        //                                dt = new DataTable();

        //                                Int64 tempCPTID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS + cntCPT, COL_TOSID));
        //                                oCPT.CPTID = tempCPTID;
        //                                dt = oCPT.getCPT();
        //                                if (dt != null)
        //                                {
        //                                    gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

        //                                    //oSubItem.ID = tempCPTID; //Convert.ToInt64(dt.Rows[0][0]);
        //                                    oSubItem.ID = 1; // Set 1 to Identify CPT
        //                                    oSubItem.Code = dt.Rows[0][0].ToString();
        //                                    oSubItem.Description = dt.Rows[0][1].ToString();
                                            
        //                                    oItem.SubItems.Add(oSubItem);
                                            
        //                                    oSubItem.Dispose();
        //                                }
                                        
   
        //                            }
        //                            break;
        //                        case "ICD9" :
        //                            {
        //                                oSubItems = new gloGeneralItem.gloSubItems();
        //                                dt = new DataTable();

        //                                Int64 tempICD9ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS + cntCPT, COL_TOSID));
        //                                dt = oICD9.GetICD9(tempICD9ID);
        //                                if (dt != null)
        //                                {
        //                                    gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

        //                                    //oSubItem.ID = tempICD9ID;  //Convert.ToInt64(dt.Rows[0][0]);
        //                                    oSubItem.ID = 2; //set 2 to identify ICD9
        //                                    oSubItem.Code = dt.Rows[0][0].ToString();
        //                                    oSubItem.Description = dt.Rows[0][1].ToString();

        //                                    oItem.SubItems.Add(oSubItem);
                                            
        //                                    oSubItem.Dispose();
        //                                }
        //                            }
        //                            break;
                                
        //                    }

                            
        //                    //oItem.SubItems.Add(Convert.ToInt64(C1TOSCPT.GetData(cntTOS + cntCPT, COL_TOSID)),"code","desc");

        //                }
                        
        //                oTOSCPT = new TOSCPT(_databaseconnectionstring);
        //                oTOSCPT.Add(oItem);
        //                oItem.Dispose();

        //                // Skip scanning the CPT/ICD9 Nodes.
        //                cntTOS = cntTOS + C1TOSCPT.Rows[cntTOS].Node.Children;
        //            }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }
        //    finally
        //    { 
                
        //    }
        //}

        private void SaveAssociation()
        {
            gloGeneralItem.gloItem oItem;
         //   gloGeneralItem.gloSubItems oSubItems;
            CPT oCPT = new CPT(_databaseconnectionstring);
            ICD9 oICD9 = new ICD9(_databaseconnectionstring);

            TOSCPT oTOSCPT = new TOSCPT(_databaseconnectionstring);

            try
            {

                // Scan every node of C1 Flex Grid to get values from Rows.
                for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
                {
                    // Scan every CPT/ICD9 information if node is TOS node only of C1 Flex Grid.
                    if (C1TOSCPT.Rows[cntTOS].Node.Level == 0)
                    {
                        oItem = new gloGeneralItem.gloItem();

                        //select the first child of the TOS node i.e CPT node
                        C1TOSCPT.Rows[cntTOS].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                        int rowIndex = C1TOSCPT.RowSel;
                        int childs = C1TOSCPT.Rows[rowIndex].Node.Children;
                        for (int i = 1; i <= C1TOSCPT.Rows[rowIndex].Node.Children; i++)
                        {

                            oItem.ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TOSID));

                           // oSubItems = new gloGeneralItem.gloSubItems();
                            DataTable dt;

                            Int64 tempCPTID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TOSID));
                            oCPT.CPTID = tempCPTID;
                            dt = oCPT.getCPT();
                            if (dt != null )
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 1; // Set subitem id to 1 to Identify CPT

                                //Code Chages made on 20081104 By - Sagar Ghodke
                                
                                //oSubItem.Code = dt.Rows[0][0].ToString();
                                //oSubItem.Description = dt.Rows[0][1].ToString();
                                if (dt.Rows.Count >= 1)
                                {
                                    oSubItem.Code = dt.Rows[0]["sCPTCode"].ToString();
                                    oSubItem.Description = dt.Rows[0]["sDescription"].ToString();
                                }

                                //End Code Changes - 20081104

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                dt.Dispose();
                                dt = null;
                            }
                        }

                        //

                        //select the last child for this TOS node i.e ICD9
                        C1TOSCPT.Rows[cntTOS].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                        rowIndex = C1TOSCPT.RowSel;
                        childs = C1TOSCPT.Rows[rowIndex].Node.Children;
                        for (int i = 1; i <= C1TOSCPT.Rows[rowIndex].Node.Children; i++)
                        {
                            oItem.ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TOSID));

                          //  oSubItems = new gloGeneralItem.gloSubItems();
                            DataTable dt;

                            Int64 tempICD9ID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TOSID));
                            dt = oICD9.GetICD9(tempICD9ID);
                            if (dt != null)
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 2; //set subitem id to  2 to identify ICD9
                                if (dt.Rows.Count >= 1)
                                {
                                    oSubItem.Code = dt.Rows[0]["sICD9Code"].ToString();
                                    oSubItem.Description = dt.Rows[0]["sDescription"].ToString();
                                }

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        //

                        //oTOSCPT = new TOSCPT(_databaseconnectionstring);
                        //first remove existing association and then add the new 
                        oTOSCPT.RemoveAssociation(Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TOSID)));
                        oTOSCPT.Add(oItem);
                        oItem.SubItems.Clear();
                        oItem.Dispose();

                        // Skip scanning the CPT/ICD9 Nodes.
                        //int ir = C1TOSCPT.RowSel;
                        //cntTOS = ir;
                        cntTOS = C1TOSCPT.RowSel;
                        //

                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if(  oCPT != null )
                {
                    oCPT.Dispose();
                    oCPT = null;
                }
                if (oICD9 != null)
                {
                    oICD9.Dispose();
                    oICD9 = null;
                }
                if (oTOSCPT != null)
                {
                    oTOSCPT.Dispose();
                    oTOSCPT = null;
                }

            }
        }

        #endregion " Private Method Save Association "

        #region " Tree CPT After Check Event "

        private void trvCPT_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool _isAllSelected = true;

            for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
            {
                if (trvCPT.Nodes[0].Nodes[i].Checked == false)
                {
                    _isAllSelected = false;
                }
            }

            chkBoxSelect.Checked = _isAllSelected;
        }

        #endregion " Tree CPT After Check Event "

        #region " Check Box Select All Click Event "

        private void chkBoxSelect_Click(object sender, EventArgs e)
        {
            if (chkBoxSelect.Checked == true)
            {
                for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
                {
                    trvCPT.Nodes[0].Nodes[i].Checked = true;
                }
                trvCPT.Nodes[0].Checked = true;
            }
            else
            {
                for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
                {
                    trvCPT.Nodes[0].Nodes[i].Checked = false;
                }
                trvCPT.Nodes[0].Checked = false;
            }

        }

        #endregion " Check Box Select All Click Event "

        #region " Search Funtionality - Text Change Events "

        private void txtCPTSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            CPT oCPT = new CPT(_databaseconnectionstring);
            ICD9 oICD9 = new ICD9(_databaseconnectionstring);

            try
            {
                //string strSearch = txtCPTSearch.Text.Trim();
                //strSearch.Replace("'", "''");
                string strSearch="";

                if (txtCPTSearch.Text != "")
                {
                    strSearch = txtCPTSearch.Text.Replace("'", "''");
                    strSearch = strSearch.Replace("[", "") +"";
                }
                else
                {
                    strSearch = "";
                }
                //

                #region " CPT Filter "

                if (pnl_btnCPT.Dock == DockStyle.Top)
                {
                    dt = oCPT.GetCPTs();
                    _dv = dt.DefaultView;

                    if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                    {
                        if (rbCode.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sCPTCode"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                        if (rbDescription.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                    }
                    else
                    {
                        if (rbCode.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sCPTCode"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        if (rbDescription.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                        }
                    }
                }

                #endregion " CPT Filter "

                #region " ICD9 Filter "

                if (pnl_btnICD9.Dock == DockStyle.Top)
                {
                    dt = oICD9.GetICD9s();
                    _dv = dt.DefaultView;


                    if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                    {
                        if (rbCode.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                        if (rbDescription.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                    }
                    else
                    {
                        if (rbCode.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        if (rbDescription.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                        }
                    }

                }

                #endregion " ICD9 Filter "

                #region " Sort Filtered Data "

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                }
                else
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;

                //

                #endregion " Sort Filtered Data " 

                // Get only the filtered row/row's
                dt = _dv.ToTable();
                //
           
                #region "  Tree Fill "

                //Clear Treee Nodes
                trvCPT.Nodes.Clear();

                // Add Parent Node
                if (pnl_btnCPT.Dock == DockStyle.Top)
                {
                    trvCPT.Nodes.Add("CPT");
                    //
                    trvCPT.Nodes[0].ImageIndex = 2;
                    trvCPT.Nodes[0].SelectedImageIndex = 2;
                    //

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                // create and set value to CPTs Nodes.
                                TreeNode tNode = new TreeNode();

                                tNode.Text = dt.Rows[i]["sCPTCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dt.Rows[i]["nCPTID"].ToString();

                                //
                                tNode.ImageIndex = 0;
                                tNode.SelectedImageIndex = 0;
                                //

                                // Add Node to CPT tree.
                                trvCPT.Nodes[0].Nodes.Add(tNode);
                            }
                        }

                    }
                }
                if (pnl_btnICD9.Dock == DockStyle.Top)
                {
                    trvCPT.Nodes.Add("ICD9");
                    //
                    trvCPT.Nodes[0].ImageIndex = 3;
                    trvCPT.Nodes[0].SelectedImageIndex = 3;
                    //

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                // create and set value to CPTs Nodes.
                                TreeNode tNode = new TreeNode();
                                tNode.Text = dt.Rows[i]["sICD9Code"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dt.Rows[i]["nICD9ID"].ToString();
                                //
                                tNode.ImageIndex = 0;
                                tNode.SelectedImageIndex = 0;
                                //
                                // Add Node to CPT tree.
                                trvCPT.Nodes[0].Nodes.Add(tNode);
                            }
                        }

                    }
                }
                //

                // Show tree Expanded;
                trvCPT.ExpandAll();
                trvCPT.SelectedNode = trvCPT.Nodes[0];



                #endregion " Tree Fill "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void txtTOSSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            CLsBL_TOSPOS oTOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            try
            {
                //string strSearch = txtTOSSearch.Text.Trim();
                //strSearch.Replace("'", "");
                //
                string strSearch = "";

                if (txtTOSSearch.Text != "")
                {
                    strSearch = txtTOSSearch.Text.Replace("'", "''");
                    strSearch = strSearch.Replace("[", "") + "";
                }
                else
                {
                    strSearch = "";
                }
                //

                //Clear all nodes first.
                trvTOS.Nodes.Clear();

                // Add Node at Level 0.
                trvTOS.Nodes.Add("Type Of Service");
                trvTOS.Nodes[0].ImageIndex = 1;
                trvTOS.Nodes[0].SelectedImageIndex = 1;
                //

                // GET All Type Of Services and bind to treeview
                //Pass 0 to get all Type Of Service
                dt = oTOS.GetTOS(0);
                _dv = dt.DefaultView;

                if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                {
                    if (rbTOSCode.Checked == true)
                    {
                        _dv.RowFilter = _dv.Table.Columns["sTOSCode"].ColumnName + " Like '%" + strSearch + "%'";
                    }
                    if (rbTOSDesc.Checked == true)
                    {
                        _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
                    }
                }
                else
                {
                    if (rbTOSCode.Checked == true)
                    {
                        _dv.RowFilter = _dv.Table.Columns["sTOSCode"].ColumnName + " Like '" + strSearch + "%'";
                    }
                    if (rbTOSDesc.Checked == true)
                    {
                        _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                    }
                }
                
                //Sorting 
                if (rbTOSCode.Checked == true)
                {
                    _dv.Sort = _dv.Table.Columns["sTOSCode"].ColumnName;
                }
                else
                {
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;
                }

                ///



                dt = _dv.ToTable();

                //Check for Table not null
                if (dt != null)
                {
                    //Check for Table not empty
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //Create Node for each Table Item
                            TreeNode oNode = new TreeNode();
                            oNode.Text = dt.Rows[i]["sTOSCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                            oNode.Tag = dt.Rows[i]["nTOSID"].ToString();
                            //
                            
                            //
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;
                            //

                            //Add Node to Type Of Service Tree
                            trvTOS.Nodes[0].Nodes.Add(oNode);
                            
                            //

                            oNode = null;

                        }//for (int i = 0; i < dt.Rows.Count ; i++)

                    }//if (dt.Rows.Count > 0)

                    trvTOS.ExpandAll();

                } //if (dt != null) 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Search Funtionality - Text Change Events"

        #region " Radio Buttons Checked Change events "

        private void rbTOSCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTOSCode.Checked == true)
            {
                rbTOSCode.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbTOSDesc.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            else
            {
                rbTOSCode.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                rbTOSDesc.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }

            FillTOS();
        }

        private void rbCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCode.Checked == true)
            {
                rbCode.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbDescription.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            else
            {
                rbCode.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                rbDescription.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }

            if (pnl_btnCPT.Dock == DockStyle.Top)
            {
                FillCPTs();
            }
            else
            {
                FillICD9s();
            }
        }

       #endregion " Radio Buttons Checked Change events "
        
    }
}