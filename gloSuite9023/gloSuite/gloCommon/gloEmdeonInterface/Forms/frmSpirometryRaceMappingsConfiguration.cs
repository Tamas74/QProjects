using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloUserControlLibrary;
namespace gloEmdeonInterface.Forms
{
    public partial class frmSpirometryRaceMappingsConfiguration : Form
    {
        #region "Declaration"
        private string _GloEMRConnectionString = string.Empty;
        private string _DeviceConnectionString = string.Empty;
        private string _gstrMessageBoxCaption = string.Empty;
        DataTable dtMappedRaceTemplate = null;
        TreeNode TempNodes;
        #endregion

        #region "form Events"

        // form Constructor
        public frmSpirometryRaceMappingsConfiguration(string DeviceConnectionString, string GloEMRConnectionString)
        {
            InitializeComponent();
            _DeviceConnectionString = DeviceConnectionString;
            _GloEMRConnectionString = GloEMRConnectionString;
            System.Collections.Specialized.NameValueCollection appSettings =null;

            #region " Retrieve MessageBoxCaption from AppSettings "
            try
            {
                appSettings = System.Configuration.ConfigurationManager.AppSettings;
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"].Length > 0)
                    {
                        _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { _gstrMessageBoxCaption = "gloEMR"; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error while reading values from AppSetting" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                appSettings = null;
            }
            #endregion
        }

        // form load Event
        private void frmSpiroTest_RaceConfiguration1_Load(object sender, EventArgs e)
        {
            LoadTreeview();
        }

        //toolstrib item clicked Event
        private void tblStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (Convert.ToString(e.ClickedItem.Tag).Trim())
            {
                case "AddNew":
                    {
                        
                        frmViewSpirometryRace obj = new frmViewSpirometryRace(_GloEMRConnectionString, _DeviceConnectionString,TrvMappedRace);
                        obj.ShowDialog(this);
                        if (obj != null)
                        {
                            obj.Dispose();
                            obj = null;
                        }
                        RefreshSpiroFeild();
                        break;
                    }
                case "Save&Close":
                    {
                        if (IsDataChanged())
                        {
                            if (SaveData() == true)
                            {
                                this.Close();
                            }

                        }
                        else
                        {
                            this.Close();
                        }

                        break;
                    }
                case "Refresh":
                    {
                        LoadTreeview();

                        break;
                    }
                case "Close":
                    {
                        this.Close();
                        break;
                    }

            }

        }

        // cusotom menu click event for deleting EMR node field
        private void MenuDeleteGloEMRFiled_Click(object sender, EventArgs e)
        {
            if (TempNodes != null)
            {
                if (Convert.ToInt64(Convert.ToString(TempNodes.Tag)) != 0)
                {
                    TrvMappedRace.Nodes.Remove(TempNodes);
                    //GetEMRRace();
                    return;
                }

            }

            if (TrvMappedRace.SelectedNode != null)
            {
                if (Convert.ToInt64(Convert.ToString(TrvMappedRace.SelectedNode.Tag)) != 0)
                {
                    TrvMappedRace.SelectedNode.Remove();
                    //GetEMRRace();
                    return;
                }
            }



        }

        // cusotom menu click event for deleting SPIRO node field
        private void MenuDeleteSpiroField_Click(object sender, EventArgs e)
        {
            if (TempNodes != null)
            {
                if (Convert.ToInt64(Convert.ToString(TempNodes.Tag)) != 0)
                {
                    TrvMappedRace.Nodes.Remove(TempNodes);
                    return;
                }

            }
            if (TrvMappedRace.SelectedNode != null)
            {
                if (Convert.ToInt64(Convert.ToString(TrvMappedRace.SelectedNode.Tag)) != 0)
                {
                    TrvMappedRace.SelectedNode.Remove();
                    return;
                }
            }



        }

        // Search EMR Race
        private void tbxSearchgloEMRRace_TextChanged(object sender, EventArgs e)
        {
            TrvgloEMRRace.SelectedNode = GetMachedNode(TrvgloEMRRace, tbxSearchgloEMRRace.Text.Trim());
        }

        // search SPIRO Race
        private void tbxSearchSpiroRace_TextChanged(object sender, EventArgs e)
        {

            TrvSpiroRace.SelectedNode = GetMachedNode(TrvSpiroRace, tbxSearchSpiroRace.Text.Trim());

        }

        //add EMR tree node Mapped treenode
        private void TrvgloEMRRace_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           
            // added condition by manoj on to check mouse button click 03/6/2011 
            if (e.Button == MouseButtons.Left)
            {
                TreeNode trv = new TreeNode(e.Node.Text, 0, 0);
                trv.Tag = e.Node.Tag;
                trv.ContextMenuStrip = Cntxt_gloEMR;
                if (Convert.ToInt64(Convert.ToString(e.Node.Tag)) != 0)
                {
                    bool IsPresent = false;
                    // check if Race Is Alredy Configured
                    foreach (TreeNode mynode in TrvMappedRace.Nodes[0].Nodes)
                    {
                        // if present
                        if (Convert.ToInt64(trv.Tag.ToString()) == Convert.ToInt64(mynode.Tag.ToString()))
                        {
                            IsPresent = true;
                            TrvMappedRace.SelectedNode = mynode;
                            break;
                        }

                    }// for eatch
                    if (IsPresent == false)
                    {
                        TrvMappedRace.Nodes[0].Nodes.Add(trv);
                        TrvMappedRace.SelectedNode = trv;

                    }
                    else
                    { MessageBox.Show("Selected race already mapped.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); } // if present

                }// if tag
                //GetEMRRace();
            }// end of button click
        }

        //add spiro tree node to Mapped treenode
        private void TrvSpiroRace_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // added condition by manoj on to check mouse button click 03/6/2011 
            if (e.Button == MouseButtons.Left)
            {
                // Race Is selected in Maped Treeview
                if (TrvMappedRace.SelectedNode != null)
                {
                    // check if mapeed race is root nodes

                    if (Convert.ToInt64(Convert.ToString(TrvMappedRace.SelectedNode.Tag)) != 0)
                    {

                        TreeNode trv = new TreeNode(e.Node.Text, 6, 6);
                        trv.Tag = e.Node.Tag;
                        trv.ContextMenuStrip = Cntxt_Spiro;
                        // check if not root not
                        if (Convert.ToInt64(Convert.ToString(e.Node.Tag)) != 0)
                        {

                            // check Race Is from EMR
                            if (Convert.ToInt64(Convert.ToString(TrvMappedRace.SelectedNode.Parent.Tag)) == 0)
                            {

                                //remove all sub nodes if it have
                                foreach (TreeNode mynode in TrvMappedRace.SelectedNode.Nodes)
                                {
                                    mynode.Remove();
                                }
                                // add sub node

                                TrvMappedRace.SelectedNode.Nodes.Add(trv);


                            }
                            // else if spiro race
                            else
                            {
                                TrvMappedRace.SelectedNode.Parent.Nodes.Add(trv);
                                TrvMappedRace.SelectedNode.Parent.Nodes.Remove(TrvMappedRace.SelectedNode);

                                //if (TrvMappedRace.SelectedNode.Parent.Nodes.Count == 1)
                                //{
                                //    MessageBox.Show("You can only map one spirometry device race to one gloEMR race.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                                //else
                                //{
                                //    TrvMappedRace.SelectedNode.Parent.Nodes.Add(trv);
                                //    TrvMappedRace.SelectedNode.Parent.Nodes.Remove(TrvMappedRace.SelectedNode);
                                //}


                            }


                            TrvMappedRace.SelectedNode = trv;
                        }//end of check if not root not

                    }


                } // TrvMappedRace.SelectedNode != null
            }
        }

        // eventnt for key selection
        private void TrvMappedRace_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TempNodes = e.Node;
        }

        //event for mouse selection
        private void TrvMappedRace_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TempNodes = e.Node;
        }

        //event for mouse selection
        private void frmSpirometryRaceMappingsConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDataChanged())
            {


                switch (MessageBox.Show("Do you want to save changes.", _gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            if (SaveData() == false)
                            {
                                e.Cancel = true;
                            }
                            break;
                        }
                    case DialogResult.No:
                        {
                            e.Cancel = false;
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                }

            }// if data changed

        }

        # region "Drag drop Event code"
        //// event to drag GloEMR ITem item
        //private void TrvgloEMRRace_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    TrvMappedRace.SelectedNode = null;  
        //    DoDragDrop(e.Item, DragDropEffects.All);
        //}

        // // event to drop EMR item
        // private void TrvMappedRace_DragDrop(object sender, DragEventArgs e)
        // {
        //     // if drag data is present
        //     if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
        //     {
        //         TreeNode _seletednode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

        //         // cehck if it is not null
        //         if (_seletednode != null)
        //         {
        //             // check if it root node
        //             if (Convert.ToInt64(Convert.ToString(_seletednode.Tag)) != 0)
        //             {
        //                 TreeNode _NewNode = new TreeNode(_seletednode.Text, _seletednode.Parent.ImageIndex, _seletednode.Parent.SelectedImageIndex);
        //                 _NewNode.Tag = _seletednode.Tag;

        //                 // check for EMRField 
        //                 if (_seletednode.Parent.Text.Trim().ToUpper() == "gloEMR Race".ToUpper())
        //                 {

        //                     bool _IsPresent = false;


        //                     // check if EMR Race Field Is ALredy Available
        //                     foreach (TreeNode mynode in TrvMappedRace.Nodes[0].Nodes)
        //                     {
        //                         // if present
        //                         if (Convert.ToInt64(_NewNode.Tag.ToString()) == Convert.ToInt64(mynode.Tag.ToString()))
        //                         {
        //                             _IsPresent = true;
        //                             TrvMappedRace.SelectedNode = mynode;
        //                             break;
        //                         }

        //                     }// for eatch

        //                     // if not present
        //                     if (_IsPresent == false)
        //                     {
        //                         // add EMR context menu to  node
        //                         _NewNode.ContextMenuStrip = Cntxt_gloEMR;


        //                         // get current point of screen
        //                         Point formP = TrvMappedRace.PointToClient(new Point(e.X, e.Y));
        //                         TreeNode DestinationNode = TrvMappedRace.GetNodeAt(formP);

        //                         // check if destination node found
        //                         if (DestinationNode != null)
        //                         {
        //                             // check if its root node 
        //                             if (DestinationNode.Parent == null)
        //                             {
        //                                 // add to last location
        //                                 TrvMappedRace.Nodes[0].Nodes.Add(_NewNode);
        //                                 TrvMappedRace.SelectedNode = _NewNode;
        //                             }
        //                             // if destination node is EMR Node
        //                             else if (Convert.ToInt64(Convert.ToString(DestinationNode.Parent.Tag)) == 0)
        //                             {
        //                                 // coppy all sub node into new dragged node
        //                                 _NewNode.Nodes.Clear();
        //                                 foreach (TreeNode sbnode in DestinationNode.Nodes)
        //                                 {
        //                                     TreeNode tempsbnode = new TreeNode(sbnode.Text, sbnode.ImageIndex, sbnode.SelectedImageIndex);
        //                                     tempsbnode.Tag = sbnode.Tag;
        //                                     _NewNode.Nodes.Add(tempsbnode);
        //                                 }
        //                                 _NewNode.Expand();
        //                                 // insert treenode in beetween index
        //                                 TrvMappedRace.Nodes[0].Nodes.Insert(DestinationNode.Index, _NewNode);
        //                                 TrvMappedRace.Nodes[0].Nodes.Remove(DestinationNode);
        //                                 TrvMappedRace.SelectedNode = _NewNode;

        //                             }
        //                             // if destination node is spirometry race
        //                             else if (Convert.ToInt64(Convert.ToString(DestinationNode.Parent.Tag)) != 0)
        //                             {
        //                                 _NewNode.Expand();
        //                                 TrvMappedRace.SelectedNode = DestinationNode.Parent;
        //                                 TrvMappedRace.SelectedNode.Nodes.Clear();
        //                                 TrvMappedRace.SelectedNode.Nodes.Add(_NewNode);  
        //                             }


        //                         }// end if destination node found
        //                         else
        //                         {
        //                             // add to last location
        //                             TrvMappedRace.Nodes[0].Nodes.Add(_NewNode);
        //                             TrvMappedRace.SelectedNode = _NewNode;
        //                         }
        //                     }
        //                     else
        //                     {
        //                         MessageBox.Show("Alredy Configured", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                     }// end if of present
        //                    // GetEMRRace();

        //                 }// end of emrarace
        //                 // cehck for spirometry filed
        //                 else if (_seletednode.Parent.Text.Trim().ToUpper() == "Spirometry Race".ToUpper())
        //                 {


        //                     // add Spirometry context menu to new node
        //                     _NewNode.ContextMenuStrip = Cntxt_Spiro;

        //                     // get current point of screen
        //                     Point formP = TrvMappedRace.PointToClient(new Point(e.X, e.Y));
        //                     TreeNode DestinationNode = TrvMappedRace.GetNodeAt(formP);
        //                     if (DestinationNode != null)
        //                     {
        //                         // check if its root node 
        //                         if (DestinationNode.Parent != null)
        //                         {
        //                             // if its EMR Field
        //                             if (Convert.ToInt64(Convert.ToString(DestinationNode.Parent.Tag)) == 0)
        //                             {
        //                                 TrvMappedRace.SelectedNode = DestinationNode;
        //                             }
        //                             // if destination field is Spirometry Field
        //                             else if (Convert.ToInt64(Convert.ToString(DestinationNode.Parent.Parent.Tag)) == 0)
        //                             {
        //                                 TrvMappedRace.SelectedNode = DestinationNode.Parent;
        //                             }
        //                             // remove all sub node associted with it
        //                             TrvMappedRace.SelectedNode.Nodes.Clear();
        //                             TrvMappedRace.SelectedNode.Nodes.Add(_NewNode);
        //                             TrvMappedRace.SelectedNode.Expand();

        //                         }// end of root node


        //                     }// end of DestinationNode


        //                 }// end of spirometry race


        //             }// end of parent node

        //         } // end of null node


        //     }// end  if drag data is present

        //}

        //event to enter drag in mapped tree node
        //private void TrvMappedRace_DragEnter(object sender, DragEventArgs e)
        //{
        //    e.Effect = DragDropEffects.Copy;
        //}

        //// event to drag spirometry ITem item
        //private void TrvSpiroRace_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    TrvMappedRace.SelectedNode = null;  
        //    DoDragDrop(e.Item, DragDropEffects.All);
        //}

        // event to Search Node Text

        #endregion


        private void btnEMRSearch_Click(object sender, EventArgs e)
        {
            TrvgloEMRRace.Focus();
        }

        // event to Search Node Text
        private void btnSpiroSearch_Click(object sender, EventArgs e)
        {
            TrvSpiroRace.Focus();
        }


        #endregion


        #region "Events and Methods"

        // method to Bind all treeview 
        private void LoadTreeview()
        {
            gloEmdeonInterface.Classes.clsCategoryMST objcls = null;
            DataTable dtEMRRace = null;
            DataTable dtMappedRace = null;
            DataTable dtSpiroRace = null;
            try
            {
                objcls = new gloEmdeonInterface.Classes.clsCategoryMST(_GloEMRConnectionString);
                // retrieve EMR Race
                objcls.ConnectionString = _GloEMRConnectionString;
                dtEMRRace = objcls.RetrieveEMRRace();
                // retrieve mapped race
                objcls.ConnectionString = _DeviceConnectionString;
                dtMappedRace = objcls.RetiriveMappedRace();
                // retrieve Spiromry Race
                objcls.ConnectionString = _DeviceConnectionString;
                dtSpiroRace = objcls.RetriveSpiroRace();
                // bind mappedrace treeview
                BindMappedTreeview(dtEMRRace, dtSpiroRace, dtMappedRace);
                dtMappedRaceTemplate = dtMappedRace;
                // bind EMR treeview
                // BindUnmappedEMRTreeView(dtEMRRace);
                BindEMRTreeView(dtEMRRace);
                // bind spiro race treeview
                BindSpiroTreeView(dtSpiroRace);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingsConfiguration.LoadTreeview() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                if (objcls != null)
                {
                    objcls.Dispose();
                    objcls = null;
                }
                if (dtMappedRace != null)
                {
                    dtMappedRace.Dispose();
                    dtMappedRace = null;
                }
                if (dtSpiroRace != null)
                {
                    dtSpiroRace.Dispose();
                    dtSpiroRace = null;
                }
                if (dtEMRRace != null)
                {
                    dtEMRRace.Dispose();
                    dtEMRRace = null;
                }
            }

        }

        // method to bind EMR field treeview
        private void BindEMRTreeView(DataTable dtbindEMRtree)
        {
            TrvgloEMRRace.Nodes.Clear();
            TreeNode RootNode = new TreeNode("gloEMR Race", 0, 0);
            RootNode.Tag = 0;
            RootNode.ImageIndex = 0;
            RootNode.SelectedImageIndex = 0;
            TreeNode tr1 = null;
            for (int i = 0; i < dtbindEMRtree.Rows.Count; i++)
            {
                tr1 = new TreeNode(dtbindEMRtree.Rows[i][1].ToString(), 7, 7);
                tr1.Tag = dtbindEMRtree.Rows[i][0].ToString();
                RootNode.Nodes.Add(tr1);
            }
            TrvgloEMRRace.Nodes.Add(RootNode);
            TrvgloEMRRace.ExpandAll();
            tr1 = null;
        }

        // method to bind Spiro Field Treeview
        private void BindSpiroTreeView(DataTable _dtSpiroRace)
        {
            TrvSpiroRace.Nodes.Clear();
            TreeNode RootNode = new TreeNode("Spirometry Race", 6, 6);
            RootNode.Tag = "0";
            TreeNode tr1 = null;
            for (int i = 0; i < _dtSpiroRace.Rows.Count; i++)
            {
                tr1 = new TreeNode(_dtSpiroRace.Rows[i]["sCategoryName"].ToString(), 7, 7);
                tr1.Tag = _dtSpiroRace.Rows[i]["nCategoryId"].ToString();
                RootNode.Nodes.Add(tr1);
            }
            TrvSpiroRace.Nodes.Add(RootNode);
            TrvSpiroRace.ExpandAll();
            tr1 = null;
        }

        // method to bind mapped treeview
        private void BindMappedTreeview(DataTable _dtEMRRace, DataTable _dtSpiroRace, DataTable _dtMappedRace)
        {
           TreeNode RootNode = null;
            long nSpiroraceID = 0;
            long nEMRRaceID = 0;
            DataRow[] EMRDr = null;
            DataRow[] SpiroDr = null;
            TreeNode ETN = null;
            TreeNode STN = null;
            try
            {
                TrvMappedRace.Nodes.Clear();
                RootNode = new TreeNode("Mapped Race", 4, 4);
                RootNode.Tag = "0";
                for (int i = 0; i < _dtMappedRace.Rows.Count; i++)
                {
                    nSpiroraceID = 0;
                    nEMRRaceID = 0;
                    EMRDr = null;
                    SpiroDr = null;
                    long.TryParse(_dtMappedRace.Rows[i][0].ToString(), out nSpiroraceID);
                    long.TryParse(_dtMappedRace.Rows[i][1].ToString(), out nEMRRaceID);
                    if (nEMRRaceID != 0 || nSpiroraceID != 0)
                    {
                        EMRDr = _dtEMRRace.Select("nCategoryId =" + nEMRRaceID + " ");
                        SpiroDr = _dtSpiroRace.Select("nCategoryId= " + nSpiroraceID + "");
                        if (EMRDr.Length == 1 && SpiroDr.Length == 1)
                        {
                            ETN = new TreeNode(Convert.ToString(EMRDr[0]["sDescription"]), 0, 0);
                            ETN.Tag = nEMRRaceID.ToString();
                            ETN.ContextMenuStrip = Cntxt_gloEMR;
                            STN = new TreeNode(Convert.ToString(SpiroDr[0]["sCategoryName"]), 6, 6);
                            STN.Tag = nSpiroraceID.ToString();
                            STN.ContextMenuStrip = Cntxt_Spiro;
                            ETN.Nodes.Add(STN);
                            RootNode.Nodes.Add(ETN);
                        }

                    }

                }
                TrvMappedRace.Nodes.Add(RootNode);
                TrvMappedRace.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error While loading Mapped tree view " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                RootNode = null;
                nSpiroraceID = 0;
                nEMRRaceID = 0;
                EMRDr = null;
                SpiroDr = null;
                 ETN = null;
                 STN = null;
            }

        }

        // funaction to validate mapping
        private bool ValidateData()
        {
            bool _Validate = true;
            foreach (TreeNode EMRRaceNode in TrvMappedRace.Nodes[0].Nodes)
            {

                if (EMRRaceNode.Nodes.Count == 0)
                {
                    _Validate = false;
                    TrvMappedRace.SelectedNode = EMRRaceNode;
                    break;
                }

            }

            return _Validate;

        }

        // method to add mapping 
        private void AddMapingData()
        {
            gloEmdeonInterface.Classes.clsCategoryMST objcls = null;
            try
            {
                objcls = new gloEmdeonInterface.Classes.clsCategoryMST(_DeviceConnectionString);
                objcls.ConnectionString = _DeviceConnectionString;
                if (objcls.DeleteMapping(0, 0))
                {
                    //add one by one Mapped node
                    foreach (TreeNode MappedNode in TrvMappedRace.Nodes[0].Nodes)
                    {

                        long _EMRRaceID = 0;
                        long _SpiroRaceID = 0;
                        long.TryParse(Convert.ToString(MappedNode.Tag), out  _EMRRaceID);
                        long.TryParse(Convert.ToString(MappedNode.Nodes[0].Tag), out _SpiroRaceID);
                        // if both id present
                        if (_EMRRaceID != 0 && _SpiroRaceID != 0)
                        {
                            objcls.ConnectionString = _DeviceConnectionString;
                            objcls.SaveMapping(_EMRRaceID, _SpiroRaceID);
                        }// o condition

                    }// for
                }// deletion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingsConfiguration.AddMapingData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (objcls != null)
                {
                    objcls.Dispose();
                    objcls = null;
                }
            }

         
        }

        // funcation to return seleted node
        private TreeNode GetMachedNode(TreeView TR_Search, string SerachText)
        {
            TreeNode _GetMachedNode = null;
            // check if search string is empty
            if (SerachText.Trim().Length > 0)
            {
                // search one by one node
                foreach (TreeNode mynode in TR_Search.Nodes[0].Nodes)
                {
                    if (mynode.Text.Length >= SerachText.Trim().Length)
                    {
                        // if node text mached with serach text
                        if (mynode.Text.Substring(0, SerachText.Trim().Length).ToUpper() == SerachText.Trim().ToUpper())
                        {
                            _GetMachedNode = mynode;
                            break;
                        }
                    } // lenght if

                }// for
            }// if search text is enterd

            return _GetMachedNode;
        }

        private void RefreshSpiroFeild()
        {
            gloEmdeonInterface.Classes.clsCategoryMST Objcls = null;
            DataTable dtSpiroRace = null;
            try
            {
                Objcls = new gloEmdeonInterface.Classes.clsCategoryMST(_DeviceConnectionString);
                dtSpiroRace = Objcls.RetriveSpiroRace();
                Objcls.Dispose();
                Objcls = null;
                //Bind Spiro treenode Again
                BindSpiroTreeView(dtSpiroRace);
                long nSpiroNodeID = 0;
                DataRow[] SpiroDr = null;
                // Refresh nodes in mapped treeview
                // loop to get all EMR Field Race
                foreach (TreeNode EMRNode in TrvMappedRace.Nodes[0].Nodes)
                {
                    foreach (TreeNode Spironode in EMRNode.Nodes)
                    {
                        nSpiroNodeID = 0;   
                        long.TryParse(Convert.ToString(Spironode.Tag), out nSpiroNodeID);
                        if (nSpiroNodeID != 0)
                        {
                             SpiroDr = dtSpiroRace.Select("nCategoryId= " + nSpiroNodeID + "");
                            if (SpiroDr.Length == 1)
                            {
                                Spironode.Text = Convert.ToString(SpiroDr[0]["sCategoryName"]);
                            }
                        }// end if SpiroNodeID!0

                    }// end of loop SPIRO field



                }// end of loop EMR field

            }
            catch (Exception)
            {
            }
            finally
            {
                if (dtSpiroRace != null)
                {
                    dtSpiroRace.Dispose();
                    dtSpiroRace = null;
                }
                if (Objcls != null)
                {
                    Objcls.Dispose();
                    Objcls = null;
                }
                
            }
        }



        private bool IsDataChanged()
        {
            bool _isDataChanged = false;
            try
            {
                if (TrvMappedRace.Nodes[0].Nodes.Count != dtMappedRaceTemplate.Rows.Count)
                {
                    _isDataChanged = true;
                }
                else
                {
                    long _EMRRaceID = 0;
                    long _SpiroRaceID = 0;
                    long _MPSpiroID = 0;
                    foreach (TreeNode MappedNode in TrvMappedRace.Nodes[0].Nodes)
                    {
                        if (MappedNode.Nodes[0] == null)
                        {
                            _isDataChanged = true;
                            break;
                        }
                        else
                        {
                            long.TryParse(Convert.ToString(MappedNode.Tag), out  _EMRRaceID);
                            long.TryParse(Convert.ToString(MappedNode.Nodes[0].Tag), out  _SpiroRaceID);
                            DataRow[] Dr = dtMappedRaceTemplate.Select("ngloCategoryId=" + _EMRRaceID + "");
                            if (Dr.Length == 1)
                            {
                                long.TryParse(Convert.ToString(Dr[0]["eCategoryId"]), out _MPSpiroID);
                                if (_SpiroRaceID != _MPSpiroID)
                                {
                                    _isDataChanged = true;
                                    break;
                                }

                            }
                            else
                            {
                                _isDataChanged = true;
                                break;
                            }// end dr length


                        } // end of null




                    } // for


                }//end else

            }
            catch (Exception ex)
            {
                _isDataChanged = true;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingsConfiguration.IsDataChanged() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }

            return _isDataChanged;
        }

        private bool SaveData()
        {
            bool _SaveData = false;
            gloEmdeonInterface.Classes.clsCategoryMST objcls = null;
            try
            {
                if (ValidateData())
                {
                    AddMapingData();
                    objcls = new gloEmdeonInterface.Classes.clsCategoryMST(_DeviceConnectionString);
                    dtMappedRaceTemplate = objcls.RetiriveMappedRace();
                    _SaveData = true;
                }
                else
                {
                    _SaveData = false;
                    MessageBox.Show("Selected race is not mapped.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {   
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in Saving mapped race " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                if (objcls != null)
                {
                    objcls.Dispose();
                    objcls = null;
                }
            }
            return _SaveData;
        }

        //private void GetEMRRace()
        //{
        //    gloEmdeonInterface.Classes.ClsRaceMaping objcls = new gloEmdeonInterface.Classes.ClsRaceMaping();
        //    // retrive EMR Race
        //    objcls.Connectionstring = _GloEMRConnectionString;
        //    DataTable dtEMRRace = objcls.RetrieveEMRRace();
        //    objcls.Dispose();
        //    objcls=null;
        //    BindUnmappedEMRTreeView(dtEMRRace);
        //}


        //// method to bind EMR field treeview
        //private void BindUnmappedEMRTreeView(DataTable dtEMRRace)
        //{
        //    TrvgloEMRRace.Nodes.Clear();
        //    TreeNode RootNode = new TreeNode("gloEMR Race", 0, 0);
        //    RootNode.Tag = 0;
        //    for (int i = 0; i < dtEMRRace.Rows.Count; i++)
        //    {
        //        long nEMRRaceID = 0;
        //        bool IsPresent = false;
        //        long.TryParse(dtEMRRace.Rows[i][0].ToString(), out nEMRRaceID);
        //        if (nEMRRaceID != 0)
        //        {

        //            // search if node is present in Mapped tree node
        //            foreach (TreeNode mynode in TrvMappedRace.Nodes[0].Nodes)
        //            {
        //                if (Convert.ToString(mynode.Tag) != null)
        //                {
        //                    if (Convert.ToInt64(Convert.ToString(mynode.Tag)) == nEMRRaceID)
        //                    {
        //                        IsPresent = true;
        //                        break;
        //                    }

        //                }// null

        //            }// for
        //            if (IsPresent == false)
        //            {

        //                TreeNode tr1 = new TreeNode(dtEMRRace.Rows[i][1].ToString(), 7, 0);
        //                tr1.Tag = dtEMRRace.Rows[i][0].ToString();
        //                RootNode.Nodes.Add(tr1);

        //            }// present

        //        }// nEMRRaceID!0

        //    }// for
        //    TrvgloEMRRace.Nodes.Add(RootNode);
        //    TrvgloEMRRace.ExpandAll();
        //}
        #endregion



        //code added by RK on 20110606 to clear the text for seraching
        private void btnCleargloEMRSerch_Click(object sender, EventArgs e)
        {
            tbxSearchgloEMRRace.Text = string.Empty;
            tbxSearchgloEMRRace.Focus();
        }

        private void btnClearSpiroSearch_Click(object sender, EventArgs e)
        {
            tbxSearchSpiroRace.Text = string.Empty;
            tbxSearchSpiroRace.Focus();
        }

      
        //end of code added by RK
       




    }
}
