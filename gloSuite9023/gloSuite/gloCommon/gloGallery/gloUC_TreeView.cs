using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloGallery
{
    public partial class gloUC_TreeView : UserControl
    {
        #region " Enumaration "
        public enum enumDisplayType
        {
            Code = 1,
            Descripation = 2,
            Code_Description = 3,
            Code_Description_Unit = 4,
            Code_Description_DrugForm = 5
        }

        public enum enumSortType
        {
            ByCode = 1,
            ByDescription = 2,
            ByUnit = 3,
            ByDrugForm = 4
        }

        public enum enumSearchType
        {
            Simple = 1,
            Instring = 2
        }
        #endregion

        #region " Shared Variables"
        //Use Shared Variable B'coz we want to acess this variable in gloEMR project
        public static bool blnResetSearch = false;
        bool gblngloTreeViewLoading = false;

        #endregion " Shared Variables"

        #region " Private Variables "

        private Int16 _DrugFlag = 16;
        private bool IsDrugSearch = false;
        private DataTable _DTMaster;
        private string _ValueMember;
        private string _CodeMember;
        private string _DescriptionMember;
        //GLO2010-0005444' Indicator
        private string _sIndicator;

        //'GLO2011-0010684
        //'Variable used to hold the ROS comments 

        private string _sComment;
        private string _ParentMember;
        private string _UnitMember;
        private string _DrugFormMember;
        private string _RouteMember;
        private string _FrequencyMember;
        private string _DurationMember;
        private string _NDCCodeMember;
        private string _DrugQtyQualifierMember;
        private string _IsNarcoticsMember;
        private enumDisplayType _DisplayType = enumDisplayType.Descripation;
        private enumSortType _SortType = enumSortType.ByDescription;
        private enumSearchType _SearchType = enumSearchType.Instring;
        private Int32 _MaxNode = 1000;
        private bool _SearchBox = true;
        private int _ParentImageIndex;
        private int _SelectedParentImageIndex;
        private string _ImageObject;
        private string _ConceptID;

        private string _CPTDeactivationDate;
        private string _CPTActivationDate;

        private string _Tag = "";
        private string _MessageBoxCaption="";
        private string _databaseconnectionstring = "";
        private const string col_DisplayText = "DISPLAY_TEXT";
        private DataView dvSort;
        private DataTable dtSort;
        Microsoft.VisualBasic.Collection oColSelectedNodes = new Microsoft.VisualBasic.Collection();
        private ArrayList arrSelectedNodeIDs = new ArrayList();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool isTreeLoading = false;
        //'Sandip Darade  20091014
        private bool _IsDrug = false;

        #endregion " Private Variables "


        #region " Public Properties "
        /// <summary>
        /// Sets the Data Source to the control.
        /// </summary>
        public DataTable DataSource
        {
            set { _DTMaster = value; }
        }

        /// <summary>
        /// Gets or sets the value member to the control.
        /// </summary>    
        public string ValueMember
        {
            get { return _ValueMember; }
            set { _ValueMember = value; }
        }

        /// <summary>
        /// Gets or sets the code member to the control.
        /// </summary>

        public string Indicator
        {
            //'GLO2010-0005444' sIndicator
            get { return _sIndicator; }
            set { _sIndicator = value; }
        }

        //'GLO2011-0010684
        //' Property used to hold the ROS Comments 
        public string Comment
        {
            get { return _sComment; }
            set { _sComment = value; }
        }

        public string CodeMember
        {
            get { return _CodeMember; }
            set { _CodeMember = value; }
        }

        /// <summary>
        /// Gets or sets the description member to the control.
        /// </summary>
        public string DescriptionMember
        {
            get { return _DescriptionMember; }
            set { _DescriptionMember = value; }
        }

        /// <summary>
        /// Gets or sets the Image member to the control.
        /// </summary>
        public string ImageObject
        {
            get { return _ImageObject; }
            set { _ImageObject = value; }
        }

        /// <summary>
        /// Gets or sets the parent member to the control which will inserted as a parent nodes.
        /// </summary>
        public string ParentMember
        {
            get { return _ParentMember; }
            set { _ParentMember = value; }
        }

        /// <summary>
        /// Gets or sets the unit member to the control.
        /// </summary>
        public string UnitMember
        {
            get { return _UnitMember; }
            set { _UnitMember = value; }
        }

        /// <summary>
        /// Gets or sets the drug form member to the control.
        /// </summary>
        public string DrugFormMember
        {
            get { return _DrugFormMember; }
            set { _DrugFormMember = value; }
        }

        /// <summary>
        /// Gets or sets the route member to the control.
        /// </summary>
        public string RouteMember
        {
            get { return _RouteMember; }
            set { _RouteMember = value; }
        }

        /// <summary>
        /// Gets or sets the frequency member to the control.
        /// </summary>
        public string FrequencyMember
        {
            get { return _FrequencyMember; }
            set { _FrequencyMember = value; }
        }

        /// <summary>
        /// Gets or sets the duration member to the control.
        /// </summary>
        public string DurationMember
        {
            get { return _DurationMember; }
            set { _DurationMember = value; }
        }

        /// <summary>
        /// Gets or sets the NDC code member to the control.
        /// </summary>
        public string NDCCodeMember
        {
            get { return _NDCCodeMember; }
            set { _NDCCodeMember = value; }
        }

        public string ConceptID
        {
            get { return _ConceptID; }
            set { _ConceptID = value; }
        }


        /// <summary>
        /// Gets or sets the drug quantity qualifier member to the control.
        /// </summary>
        public string DrugQtyQualifierMember
        {
            get { return _DrugQtyQualifierMember; }
            set { _DrugQtyQualifierMember = value; }
        }

        /// <summary>
        /// Gets or sets the IsNarcotics member to the control.
        /// </summary>
        public string IsNarcoticsMember
        {
            get { return _IsNarcoticsMember; }
            set { _IsNarcoticsMember = value; }
        }

        /// <summary>
        /// Gets or sets the display type of the control.
        /// </summary>
        public enumDisplayType DisplayType
        {
            get { return _DisplayType; }
            set { _DisplayType = value; }
        }

        /// <summary>
        /// Gets or sets the sort type of the control.
        /// </summary>
        public enumSortType Sort
        {
            get { return _SortType; }
            set { _SortType = value; }
        }

        /// <summary>
        /// Gets or sets the search type of the control.
        /// </summary>
        public enumSearchType Search
        {
            get { return _SearchType; }
            set { _SearchType = value; }
        }

        /// <summary>
        /// Gets or sets a value indication number of nodes to be displayed on gloUC_TreeView control.
        /// </summary>
        public Int32 MaximumNodes
        {
            get { return _MaxNode; }
            set { _MaxNode = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the gloUC_TreeView control.
        /// </summary>
        public bool CheckBoxes
        {
            get { return trvMain.CheckBoxes; }
            set { trvMain.CheckBoxes = value; }
        }

        /// <summary>
        /// Gets the tree node that is currently selected in the gloUC_TreeView control.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public TreeNode SelectedNode
        {
            get { return trvMain.SelectedNode; }
            set { trvMain.SelectedNode = value; }
        }

        /// <summary>
        /// Gets the collection of checked nodes in the gloUC_TreeView control.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Microsoft.VisualBasic.Collection SelectedNodes
        {
            get { return oColSelectedNodes; }
        }

        /// <summary>
        /// Gets or sets a value indication whether seach box is displayed.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SearchBox
        {
            get { return _SearchBox; }
            set
            {
                pnlSearch.Visible = value;
                _SearchBox = value;
            }
        }

        ///' <summary>
        ///' Property to Get or Set the value in Search box of control
        ///' </summary>
        ///' <remarks></remarks>
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }


        /// <summary>
        /// Gets or sets the System.Windows.Forms.ImageList that contains the System.Drawing.Image objects used by the tree nodes.
        /// </summary>
        /// <returns>
        /// The System.Windows.Forms.ImageList that contains the System.Drawing.Image objects used by the tree nodes. The default value is null.
        /// </returns>
        public ImageList ImageList
        {

            get { return trvMain.ImageList; }
            set { trvMain.ImageList = value; }
        }

        /// <summary>
        /// Gets or sets the image-list index value of the default image that is displayed by the tree nodes.
        /// </summary>
        /// <returns>
        /// A zero-based index that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList. The default is zero.
        /// </returns>
        public int ImageIndex
        {
            get { return trvMain.ImageIndex; }
            set { trvMain.ImageIndex = value; }
        }

        /// <summary>
        /// Gets or sets the image list index value of the image that is displayed when a tree node is selected.
        /// </summary>
        /// <returns>
        /// A zero-based index value that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList.
        /// </returns>
        public int SelectedImageIndex
        {
            get { return trvMain.SelectedImageIndex; }
            set { trvMain.SelectedImageIndex = value; }
        }

        /// <summary>
        /// Gets or sets the image-list index value of the default image that is displayed by the parent tree nodes.
        /// </summary>
        /// <returns>
        /// A zero-based index that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList. The default is zero.
        /// </returns>
        public int ParentImageIndex
        {
            get { return _ParentImageIndex; }
            set { _ParentImageIndex = value; }
        }

        /// <summary>
        /// Gets or sets the image list index value of the image that is displayed when a parent tree node is selected.
        /// </summary>
        /// <returns>
        /// A zero-based index value that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList.
        /// </returns>
        public int SelectedParentImageIndex
        {
            get { return _SelectedParentImageIndex; }
            set { _SelectedParentImageIndex = value; }
        }

        /// <summary>
        /// Gets the collection of tree nodes that are assigned to the gloUC_TreeView control.
        /// </summary>
        public TreeNodeCollection Nodes
        {
            get { return trvMain.Nodes; }
        }

        /// <summary>
        /// Gets or sets the arraylist of node IDs which will be selected nodes on the gloUC_TreeView control.
        /// </summary>
        public ArrayList SelectedNodeIDs
        {
            get { return arrSelectedNodeIDs; }
            set
            {
                if ((value == null))
                {
                    arrSelectedNodeIDs.Clear();
                }
                else
                {
                    arrSelectedNodeIDs = value;
                }
            }
        }

        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        //'Sandip Darade  20091014
        public bool IsDrug
        {
            get { return _IsDrug; }
            set { _IsDrug = value; }
        }

        public Int16 DrugFlag
        {
            get { return _DrugFlag; }
            set { _DrugFlag = value; }
        }

        public string CPTDeactivationDate
        {
            get { return this._CPTDeactivationDate; }
            set { this._CPTDeactivationDate = value; }
        }

        public string CPTActivationDate
        {
            get { return this._CPTActivationDate; }
            set { this._CPTActivationDate = value; }
        }

        #endregion

        #region " Constructor "

        public gloUC_TreeView()
        {
            InitializeComponent();

            _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
        }

        #endregion " Constructor "

        #region " Control Events "

        public event NodeMouseDoubleClickEventHandler NodeMouseDoubleClick;
        public delegate void NodeMouseDoubleClickEventHandler(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e);
        public new event EventHandler<KeyPressEventArgs> KeyPress;
        public event AfterSelectEventHandler AfterSelect;
        public delegate void AfterSelectEventHandler(System.Object sender, System.Windows.Forms.TreeViewEventArgs e);
        public event MouseDownEventHandler MouseDown;
        public delegate void MouseDownEventHandler(System.Object sender, System.Windows.Forms.MouseEventArgs e);
        public event NodeAddedEventHandler NodeAdded;
        public delegate void NodeAddedEventHandler(myTreeNode ChildNode);

        private void trvMain_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (isTreeLoading == false)
            {
                if (e.Node.Checked)
                {
                    //' NODE CHECKED ''
                    if (!string.IsNullOrEmpty(_ParentMember) & e.Node.Level == 0)
                    {
                        //' PARENT NODE CHECKED ''
                        foreach (TreeNode oNode in e.Node.Nodes)
                        {
                            oNode.Checked = true;
                        }
                    }
                    else
                    {
                        //' CHILD NODE CHECKED ''
                        Int64 _ID = ((myTreeNode)e.Node).ID;
                        if (arrSelectedNodeIDs.Contains(_ID) == false)
                        {
                            oColSelectedNodes.Add(e.Node);
                            arrSelectedNodeIDs.Add(_ID);
                        }
                    }

                }
                else
                {
                    //' NODE UNCHECKED ''
                    if (!string.IsNullOrEmpty(_ParentMember) & e.Node.Level == 0)
                    {
                        //' PARENT NODE UNCHECKED ''
                        foreach (TreeNode oNode in e.Node.Nodes)
                        {
                            oNode.Checked = false;
                        }
                    }
                    else
                    {
                        //' CHILD NODE UNCHECKED ''
                        for (int i = 1; i <= oColSelectedNodes.Count; i++)
                        {
                            Int64 _ID = ((myTreeNode)e.Node).ID;
                            if (((myTreeNode)oColSelectedNodes[i]).ID == _ID)
                            {
                                oColSelectedNodes.Remove(i);
                                arrSelectedNodeIDs.Remove(_ID);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void trvMain_AfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                trvMain.SelectedNode = e.Node;
                if (AfterSelect != null)
                {
                    AfterSelect(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trvMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtsearch.Focus();
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                if (KeyPress != null)
                {
                    KeyPress(sender, e);
                }
                // txtsearch.ResetText()
                txtsearch.Focus();
            }
        }

        private void trvMain_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                trvMain.SelectedNode = e.Node;
            }
        }

        private void trvMain_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            try
            {
                trvMain.SelectedNode = e.Node;
                if (NodeMouseDoubleClick != null)
                {
                    NodeMouseDoubleClick(sender, e);
                }
                //'Shubhangi
                //txtsearch.ResetText()
                txtsearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //If e.KeyChar = "'" Or e.KeyChar = "[" Or e.KeyChar = "^" Or e.KeyChar = "*" Or e.KeyChar = "%" Then
            if (e.KeyChar.ToString() == "[" || e.KeyChar.ToString() == "^" || e.KeyChar.ToString() == "*" || e.KeyChar.ToString() == "%")
            {
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                if (trvMain.Nodes.Count > 0)
                {
                    trvMain.Focus();
                    if ((trvMain.SelectedNode == null))
                    {
                        trvMain.SelectedNode = trvMain.Nodes[0];
                    }
                }
            }
        }

        private void txtsearch_TextChanged(System.Object sender, System.EventArgs e)
        {

            try
            {
                //'Sandip darade  2001014
                //'Pull thadrug from database 
                IsDrugSearch = false;
                if ((IsDrug == true))
                {
                    if ((_DTMaster != null))
                    {
                        _DTMaster = null;
                    }
                    _DTMaster = FillDrugs(txtsearch.Text);
                    IsDrugSearch = true;
                    FillTreeView();
                    return;
                }
                if ((_DTMaster == null) == false)
                {
                    if (_DTMaster.Rows.Count > 0)
                    {
                        FillTree(txtsearch.Text.Trim());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearSearchText()
        {
            try
            {
                txtsearch.TextChanged -= new System.EventHandler(txtsearch_TextChanged);
                txtsearch.Text = string.Empty;
                txtsearch.TextChanged += new System.EventHandler(txtsearch_TextChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void trvMain_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                trvMain.SelectedNode = trvMain.GetNodeAt(e.X, e.Y);
                if (MouseDown != null)
                {
                    MouseDown(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion " Control Events "

        #region " Public Methods "
        /// <summary>
        /// Fills the gloUC_TreeView according to bound data table and its properties.
        /// </summary>
        public void FillTreeView()
        {
            if (gblngloTreeViewLoading)
            {
                return;
            }

            gblngloTreeViewLoading = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                if (ValidateProperties() == false)
                {
                    return;
                }

                oColSelectedNodes.Clear();
                arrSelectedNodeIDs.Clear();

                //' SORTING DATAVIEW
                dvSort = _DTMaster.DefaultView;
                switch (_SortType)
                {
                    case enumSortType.ByCode:
                        dvSort.Sort = _CodeMember;
                        break;
                    case enumSortType.ByDescription:
                        dvSort.Sort = _DescriptionMember;
                        break;
                    case enumSortType.ByUnit:
                        dvSort.Sort = _UnitMember;
                        break;
                    //' added on 20091008
                    case enumSortType.ByDrugForm:
                        dvSort.Sort = _DrugFormMember;
                        break;
                }
                _DTMaster = dvSort.ToTable();
                //' END SORT ''

                Application.DoEvents();
                //' DO NOT MOVE THIS LINE OF CODE, IT MAY LEAD TO EXCEPTION WHILE MULTITHREAD ''

                //' CREATE NEW COLUMN OF DISPLAY_TEXT
                DataColumn oColumn = new DataColumn(col_DisplayText);
                _DTMaster.Columns.Add(oColumn);


                int _loopCount = _DTMaster.Rows.Count - 1;
                if (_DTMaster.Rows.Count > _MaxNode)
                {
                    _loopCount = _MaxNode - 1;
                }
                else
                {
                    _loopCount = _DTMaster.Rows.Count - 1;
                }

                if ((IsDrug == true))
                {
                    _loopCount = _DTMaster.Rows.Count - 1;
                }
                //' FILL NEW COLUMN RECORDS ''

                _DTMaster.BeginLoadData();
                switch (_DisplayType)
                {
                    case enumDisplayType.Code:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember];
                        }


                        break;
                    case enumDisplayType.Descripation:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_DescriptionMember];
                        }


                        break;
                    case enumDisplayType.Code_Description:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            // _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)" - " & _DTMaster.Rows(index)(_DescriptionMember)

                            //'Sandip Darade 20090623
                            //'check if description is blank
                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember].ToString().Trim();
                            if ((!string.IsNullOrEmpty(_DTMaster.Rows[index][_DescriptionMember].ToString().Trim())))
                            {
                                if (!string.IsNullOrEmpty(_DTMaster.Rows[index][col_DisplayText].ToString().Trim()))
                                {
                                    if ((_DTMaster.Columns["IsDrug"] == null) == false)
                                    {

                                        if (Convert.ToInt32(_DTMaster.Rows[index]["IsDrug"]) == 1)
                                        {
                                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                                    }

                                    // _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember) & " - " & _DTMaster.Rows(index)(col_DisplayText).ToString.Trim
                                    //
                                }
                                else
                                {
                                    _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_DescriptionMember];
                                }

                            }

                        }


                        break;
                    case enumDisplayType.Code_Description_Unit:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            //_DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember) & " - " & _DTMaster.Rows(index)(_DescriptionMember) & " - " & dtSort.Rows(index)(_UnitMember)
                            //'Sandip Darade 20090623
                            //'check if description or unit or both are  blank
                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember].ToString().Trim();

                            if ((!string.IsNullOrEmpty(_DTMaster.Rows[index][_DescriptionMember].ToString().Trim())))
                            {
                                _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                            }

                            if ((!string.IsNullOrEmpty(_DTMaster.Rows[index][_UnitMember].ToString().Trim())))
                            {
                                _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_UnitMember].ToString().Trim();
                            }
                        }


                        break;
                    //' Added on 20091008 
                    //' For Drugs we have to show Drug from alonfg with its name & Sig info
                    case enumDisplayType.Code_Description_DrugForm:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember].ToString().Trim();

                            if ((!string.IsNullOrEmpty(_DTMaster.Rows[index][_DescriptionMember].ToString().Trim())))
                            {
                                _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                            }

                            if ((!string.IsNullOrEmpty(_DTMaster.Rows[index][_DrugFormMember].ToString().Trim())))
                            {
                                _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DrugFormMember].ToString().Trim();
                            }
                        }

                        break;
                    default:
                        for (int index = 0; index <= _loopCount; index++)
                        {
                            dtSort.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                        }


                        break;
                }
                _DTMaster.EndLoadData();
                Application.DoEvents();

                dvSort = _DTMaster.DefaultView;

                //If blnResetSearch = False Then
                //    txtsearch.Text = _SearchText
                //End If
                //Check variable of reset text 
                if ((IsDrugSearch == false))
                {
                    if (blnResetSearch == true)
                    {
                        txtsearch.Clear();
                    }
                }


                FillTree(txtsearch.Text.Trim());
                if ((IsDrug != true))
                {
                    if (_DTMaster.Rows.Count > _MaxNode)
                    {
                        Application.DoEvents();
                        AttachDisplayTextToDataTable(_loopCount - 1);
                        Application.DoEvents();
                    }
                }
                txtsearch.Focus();
                txtsearch.DeselectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gblngloTreeViewLoading = false;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Forces the control to invalidate its client area and immediately redraw itself and any child controls.
        /// </summary>
        public override void Refresh()
        {
            txtsearch.Clear();
            oColSelectedNodes.Clear();
            arrSelectedNodeIDs.Clear();
            FillTree(txtsearch.Text.Trim());
        }

        /// <summary>
        /// Check all visible nodes of gloUC_TreeView control.
        /// </summary>
        /// <remarks></remarks>
        public void CheckAllNodes()
        {
            try
            {
                if (trvMain.CheckBoxes)
                {
                    oColSelectedNodes.Clear();
                    arrSelectedNodeIDs.Clear();

                    //' CHECK ALL VISIBLE NODES ''
                    foreach (TreeNode _Node in trvMain.Nodes)
                    {
                        _Node.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Uncheck all the nodes of gloUC_TreeView control.
        /// </summary>
        /// <remarks></remarks>
        public void UncheckAllNodes()
        {
            if (trvMain.CheckBoxes)
            {
                isTreeLoading = true;
                try
                {
                    foreach (TreeNode oNode in trvMain.Nodes)
                    {
                        oNode.Checked = false;
                    }

                    //' IF LEVEL ONE TREE ''
                    if (!string.IsNullOrEmpty(_ParentMember))
                    {
                        foreach (TreeNode oParentNode in trvMain.Nodes)
                        {
                            foreach (TreeNode oChildNode in oParentNode.Nodes)
                            {
                                oChildNode.Checked = false;
                            }
                        }
                    }

                    oColSelectedNodes.Clear();
                    arrSelectedNodeIDs.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isTreeLoading = false;
            }
        }

        /// <summary>
        /// Sets input focus to the tree view of gloUC_TreeView control.
        /// </summary>
        public void FocusTreeView()
        {
            trvMain.Focus();
        }

        /// <summary>
        /// Sets input focus to the search box of gloUC_TreeView control.
        /// </summary>
        public void FocusSearchBox()
        {
            txtsearch.Focus();
        }

        /// <summary>
        /// Clear all the values and members of gloUC_TreeView control and removes all nodes from tree.
        /// </summary>
        /// <remarks></remarks>
        public void Clear()
        {
            _DTMaster = null;
            _ValueMember = null;
            _DescriptionMember = null;
            _CodeMember = null;
            _DrugFormMember = null;
            _RouteMember = null;
            _NDCCodeMember = null;
            _IsNarcoticsMember = null;
            _FrequencyMember = null;
            _DurationMember = null;
            _DrugQtyQualifierMember = null;
            //_DisplayType = null;
            _UnitMember = null;
            _ParentMember = null;
            _Tag = null;
            _IsDrug = false;
            _DrugFlag = 0;
            _ImageObject = null;

            //txtsearch.Clear()
            if (blnResetSearch == false)
            {
                // _SearchText = txtsearch.Text.Trim
            }
            else
            {
                txtsearch.Clear();
                // _SearchText = ""
            }

            trvMain.Nodes.Clear();
            oColSelectedNodes.Clear();
            arrSelectedNodeIDs.Clear();
        }
        //' solving sales force case- 0009512
        public void SetFocus()
        {
            txtsearch.Focus();
            txtsearch.Select();
        }
        //' end

        /// <summary>
        /// Collapses all the tree nodes.
        /// </summary>
        /// <remarks></remarks>
        public void CollapseAll()
        {
            trvMain.CollapseAll();
            if (trvMain.Nodes.Count > 0)
            {
                trvMain.SelectedNode = trvMain.Nodes[0];
            }
        }

        /// <summary>
        /// Expands all the tree nodes.
        /// </summary>
        /// <remarks></remarks>
        public void ExpandAll()
        {
            trvMain.ExpandAll();
            if (trvMain.Nodes.Count > 0)
            {
                trvMain.SelectedNode = trvMain.Nodes[0];
            }
        }

        #endregion " Public Methods "

        #region " Private Methods "

        private bool ValidateProperties()
        {

            string _ErrorString = "";

            //' VALIDATION FOR DATATABLE
            if ((_DTMaster == null))
            {
                _ErrorString = "DataSource cannot be empty." + Environment.NewLine;
            }
            else
            {
                //Commented by Mayuri:20091005
                //To display treeview as blank if no Record associated with particular specilaity in frmICD9CPTGallery
                //If _DTMaster.Rows.Count = 0 Then
                //    Return False
                //End If
            }

            //' VALIDATION FOR VALUE MEMBER '' 
            if (string.IsNullOrEmpty(_ValueMember))
            {
                _ErrorString = _ErrorString + "ValueMember Property is not set" + Environment.NewLine;
            }
            else
            {
                if (_DTMaster.Columns.Contains(_ValueMember))
                {
                    if (object.ReferenceEquals(_DTMaster.Columns[_ValueMember].DataType, Type.GetType("System.String")))
                    {
                        _ErrorString = _ErrorString + "Column '" + _ValueMember + "' does not contain numeric values at ValueMember property." + Environment.NewLine;
                    }
                }
                else
                {
                    _ErrorString = _ErrorString + "Column '" + _ValueMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATION FOR ISNARCOTICS MEMBER ''
            if (!string.IsNullOrEmpty(_IsNarcoticsMember))
            {
                if (_DTMaster.Columns.Contains(_IsNarcoticsMember))
                {
                    if (object.ReferenceEquals(_DTMaster.Columns[_IsNarcoticsMember].DataType, Type.GetType("System.String")))
                    {
                        _ErrorString = _ErrorString + "Column '" + _IsNarcoticsMember + "' does not contain numeric values at IsNarcoticsMember property." + Environment.NewLine;
                    }
                }
                else
                {
                    _ErrorString = _ErrorString + "Column '" + _IsNarcoticsMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR CODE MEMBER ''
            if (!string.IsNullOrEmpty(_CodeMember))
            {
                if (_DTMaster.Columns.Contains(_CodeMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _CodeMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR DESCRIPTION MEMBER ''
            if (!string.IsNullOrEmpty(_DescriptionMember))
            {
                if (_DTMaster.Columns.Contains(_DescriptionMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _DescriptionMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            // ''GLO2010-0005444' VALIDATE FOR INDICATOR MEMBER ''
            if (!string.IsNullOrEmpty(_sIndicator))
            {
                if (_DTMaster.Columns.Contains(_sIndicator) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _sIndicator + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' GLO2011-0010684
            //' Check for the Comments Field is available in the DataTable (_DTMaster)
            if (!string.IsNullOrEmpty(_sComment))
            {
                if (_DTMaster.Columns.Contains(_sComment) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _sComment + "' does not belong to table" + Environment.NewLine;
                }
            }


            //' VALIDATE FOR PARENT MEMBER ''
            if (!string.IsNullOrEmpty(_ParentMember))
            {
                if (_DTMaster.Columns.Contains(_ParentMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _ParentMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR UNIT MEMBER ''
            if (!string.IsNullOrEmpty(_UnitMember))
            {
                if (_DTMaster.Columns.Contains(_UnitMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _UnitMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR DRUG FORM MEMBER ''
            if (!string.IsNullOrEmpty(_DrugFormMember))
            {
                if (_DTMaster.Columns.Contains(_DrugFormMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _DrugFormMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR ROUTE MEMBER ''
            if (!string.IsNullOrEmpty(_RouteMember))
            {
                if (_DTMaster.Columns.Contains(_RouteMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _RouteMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR FRQUENCY MEMBER ''
            if (!string.IsNullOrEmpty(_FrequencyMember))
            {
                if (_DTMaster.Columns.Contains(_FrequencyMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _FrequencyMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR DURATION MEMBER ''
            if (!string.IsNullOrEmpty(_DurationMember))
            {
                if (_DTMaster.Columns.Contains(_DurationMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _DurationMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR NDC CODE MEMBER ''
            if (!string.IsNullOrEmpty(_NDCCodeMember))
            {
                if (_DTMaster.Columns.Contains(_NDCCodeMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _NDCCodeMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR DRUG QUANTITY QUALIFIER MEMBER ''
            if (!string.IsNullOrEmpty(_DrugQtyQualifierMember))
            {
                if (_DTMaster.Columns.Contains(_DrugQtyQualifierMember) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _DrugQtyQualifierMember + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR CPT DEACTIVATION DATE ''
            if (!string.IsNullOrEmpty(_CPTDeactivationDate))
            {
                if (_DTMaster.Columns.Contains(_CPTDeactivationDate) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _CPTDeactivationDate + "' does not belong to table" + Environment.NewLine;
                }
            }

            //' VALIDATE FOR CPT ACTIVATION DATE ''
            if (!string.IsNullOrEmpty(_CPTActivationDate))
            {
                if (_DTMaster.Columns.Contains(_CPTActivationDate) == false)
                {
                    _ErrorString = _ErrorString + "Column '" + _CPTActivationDate + "' does not belong to table" + Environment.NewLine;
                }
            }

            //'VALIDATION FOR DISPLAY MEMBER REQUIRMENT
            switch (_DisplayType)
            {
                case enumDisplayType.Code:
                    if (string.IsNullOrEmpty(_CodeMember))
                    {
                        _ErrorString = _ErrorString + "CodeMember Property is not set" + Environment.NewLine;
                    }
                    break;
                case enumDisplayType.Descripation:
                    if (string.IsNullOrEmpty(_DescriptionMember))
                    {
                        _ErrorString = _ErrorString + "DescriptionMember Property is not set" + Environment.NewLine;
                    }
                    break;
                case enumDisplayType.Code_Description:
                    if (string.IsNullOrEmpty(_CodeMember))
                    {
                        _ErrorString = _ErrorString + "CodeMember Property is not set" + Environment.NewLine;
                    }
                    else if (string.IsNullOrEmpty(_DescriptionMember))
                    {
                        _ErrorString = _ErrorString + "DescriptionMember Property is not set" + Environment.NewLine;
                    }
                    break;
                case enumDisplayType.Code_Description_Unit:
                    if (string.IsNullOrEmpty(_CodeMember))
                    {
                        _ErrorString = _ErrorString + "CodeMember Property is not set" + Environment.NewLine;
                    }
                    else if (string.IsNullOrEmpty(_DescriptionMember))
                    {
                        _ErrorString = _ErrorString + "DescriptionMember Property is not set" + Environment.NewLine;
                    }
                    else if (string.IsNullOrEmpty(_UnitMember))
                    {
                        _ErrorString = _ErrorString + "UnitMember Property is not set" + Environment.NewLine;
                    }
                    break;
            }

            //' VALIDATION FOR SORT TYPE
            switch (_SortType)
            {
                case enumSortType.ByCode:
                    if (string.IsNullOrEmpty(_CodeMember))
                    {
                        _ErrorString = _ErrorString + "CodeMember Property is not set" + Environment.NewLine;
                    }
                    break;
                case enumSortType.ByDescription:
                    if (string.IsNullOrEmpty(_DescriptionMember))
                    {
                        _ErrorString = _ErrorString + "DescriptionMember Property is not set" + Environment.NewLine;
                    }
                    break;
                case enumSortType.ByUnit:
                    if (string.IsNullOrEmpty(_UnitMember))
                    {
                        _ErrorString = _ErrorString + "UnitMember Property is not set" + Environment.NewLine;
                    }
                    break;
            }

            //' VALIDATION FAILED ''
            if (!string.IsNullOrEmpty(_ErrorString.Trim()))
            {
                Exception oEX = new Exception(_ErrorString);
                throw oEX;
                //return false;
            }

            //' SUCCESSFULL VALIDATIONS ''
            return true;

        }

        public void FillTree(string searchText = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    //'Sandip Darade 20090607
                    //'Replace special operators to avoid errors
                    searchText = searchText.Replace("'", "''");
                    searchText = searchText.Replace("[", "") + "";
                    searchText = ReplaceSpecialCharacters(searchText);
                    if (_SearchType == enumSearchType.Simple)
                    {
                        dvSort.RowFilter = col_DisplayText + " LIKE '" + searchText.Trim() + "%'";
                    }
                    else if (_SearchType == enumSearchType.Instring)
                    {
                        dvSort.RowFilter = col_DisplayText + " LIKE '%" + searchText.Trim() + "%'";
                    }
                }
                else
                {
                    dvSort.RowFilter = null;
                }

                //' GET SORTED / FILTERED DATAVIEW
                dtSort = dvSort.ToTable();

                //' FILLING TREE VIEW 
                trvMain.BeginUpdate();
                //code line added by dipak 20091005 Scrollable =false before update

                //trvMain.Scrollable = False '' Line commented by Sandip Darade  20091015 as it was taking time 

                trvMain.Nodes.Clear();

                if (string.IsNullOrEmpty(_ParentMember))
                {
                    FillLevelZeroTree();
                }
                else
                {
                    FillLevelOneTree();
                }

                CheckSelectedNodes();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //code added by dipak 20091005 Scrollable =true after update
                trvMain.Scrollable = true;
                trvMain.EndUpdate();
            }

        }

        private string ReplaceSpecialCharacters(string strSpecialChar)
        {

            try
            {
                strSpecialChar = strSpecialChar.Replace("#", "[#]") + "";
                strSpecialChar = strSpecialChar.Replace( "$", "[$]") + "";
                strSpecialChar = strSpecialChar.Replace( "%", "[%]") + "";
                strSpecialChar = strSpecialChar.Replace( "^", "[^]") + "";
                strSpecialChar = strSpecialChar.Replace( "&", "[&]") + "";

                //' Was Commented Before 2090602
                //' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
                //' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
                strSpecialChar = strSpecialChar.Replace( "~", "[~]") + "";
                strSpecialChar = strSpecialChar.Replace( "!", "[!]") + "";
                strSpecialChar = strSpecialChar.Replace( "*", "[*]") + "";
                strSpecialChar = strSpecialChar.Replace( ";", "[;]") + "";
                strSpecialChar = strSpecialChar.Replace( "/", "[/]") + "";
                strSpecialChar = strSpecialChar.Replace( "?", "[?]") + "";
                strSpecialChar = strSpecialChar.Replace( ">", "[>]") + "";
                strSpecialChar = strSpecialChar.Replace( "<", "[<]") + "";
                strSpecialChar = strSpecialChar.Replace( "\\", "[\\]") + "";
                strSpecialChar = strSpecialChar.Replace( "|", "[|]") + "";
                strSpecialChar = strSpecialChar.Replace( "{", "[{]") + "";
                strSpecialChar = strSpecialChar.Replace( "}", "[}]") + "";
                strSpecialChar = strSpecialChar.Replace( "-", "[-]") + "";
                strSpecialChar = strSpecialChar.Replace( "_", "[_]") + "";
                //'END Was Commented Before 2090602
                return strSpecialChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void FillLevelZeroTree()
        {
            myTreeNode oNode = default(myTreeNode);

            for (int index = 0; index <= dtSort.Rows.Count - 1; index++)
            {
                //' MAXIMUM NODES VALIDATION ''
                if (index >= _MaxNode)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }

                oNode = new myTreeNode();

                //' BIND ID '' 
                if (!string.IsNullOrEmpty(_ValueMember))
                {
                    oNode.ID = Convert.ToInt64(dtSort.Rows[index][_ValueMember]);
                }


                //' BIND Tag '' 
                if (!string.IsNullOrEmpty(_Tag))
                {
                    oNode.Tag = (object)dtSort.Rows[index][_Tag];
                }
                //' BIND Tag '' 
                if (!string.IsNullOrEmpty(_ConceptID))
                {
                    oNode.ConceptID = Convert.ToString(dtSort.Rows[index][_ConceptID]);
                }
                //' BIND CODE '' 
                if (!string.IsNullOrEmpty(_CodeMember))
                {
                    oNode.Code = Convert.ToString(dtSort.Rows[index][_CodeMember]);
                }

                //' BIND DESCRIPTION '' 
                if (!string.IsNullOrEmpty(_DescriptionMember))
                {
                    oNode.Description = Convert.ToString(dtSort.Rows[index][_DescriptionMember]);
                }

                //GLO2010-0005444' BIND Indicator '' 
                if (!string.IsNullOrEmpty(_sIndicator))
                {
                    oNode.Indicator = Convert.ToString(dtSort.Rows[index][_sIndicator]);
                }

                //' GLO2011-0010684
                //' Bind the Comments field to the Comment node 
                if (!string.IsNullOrEmpty(_sComment))
                {
                    oNode.Comments = Convert.ToString(dtSort.Rows[index][_sComment]);
                }

                //' BIND UNIT ''
                if (!string.IsNullOrEmpty(_UnitMember))
                {
                    oNode.Unit = Convert.ToString(dtSort.Rows[index][_UnitMember]);
                }

                //' BIND DRUG FORM ''
                if (!string.IsNullOrEmpty(_DrugFormMember))
                {
                    oNode.DrugForm = Convert.ToString(dtSort.Rows[index][_DrugFormMember]);
                }

                //' BIND ROUTE ''
                if (!string.IsNullOrEmpty(_RouteMember))
                {
                    oNode.Route = Convert.ToString(dtSort.Rows[index][_RouteMember]);
                }

                //' BIND FREQUENCY ''
                if (!string.IsNullOrEmpty(_FrequencyMember))
                {
                    oNode.Frequency = Convert.ToString(dtSort.Rows[index][_FrequencyMember]);
                }

                //' BIND DURATION ''
                if (!string.IsNullOrEmpty(_DurationMember))
                {
                    oNode.Duration = Convert.ToString(dtSort.Rows[index][_DurationMember]);
                }

                //' BIND NDC CODE ''
                if (!string.IsNullOrEmpty(_NDCCodeMember))
                {
                    oNode.NDCCode = Convert.ToString(dtSort.Rows[index][_NDCCodeMember]);
                }

                //' BIND DRUG QUANTITY QUALIFIER ''
                if (!string.IsNullOrEmpty(_DrugQtyQualifierMember))
                {
                    oNode.DrugQtyQualifier = Convert.ToString(dtSort.Rows[index][_DrugQtyQualifierMember]);
                }

                //' BIND IS NARCOTICS ''
                if (!string.IsNullOrEmpty(_IsNarcoticsMember))
                {
                    oNode.IsNarcotics = (Int16)dtSort.Rows[index][_IsNarcoticsMember];
                }

                //' BIND IMAGE OBJECT TO NODE ''
                if (!string.IsNullOrEmpty(_ImageObject))
                {
                    oNode.TemplateResult = dtSort.Rows[index][_ImageObject];
                }

                //' BIND DISPLAY TEXT ''
                oNode.Text = Convert.ToString(dtSort.Rows[index][col_DisplayText]);

                //' BIND CPT ACTIVATION DATE TO NODE ''
                if (!string.IsNullOrEmpty(_CPTActivationDate))
                {
                    if (dtSort.Rows[index][_CPTActivationDate] != DBNull.Value)
                    { oNode.CPTActivationDate = Convert.ToDateTime(dtSort.Rows[index][_CPTActivationDate]); }                    
                }

                //' BIND CPT DEACTIVATION DATE TO NODE ''
                if (!string.IsNullOrEmpty(_CPTDeactivationDate))
                {
                    if (dtSort.Rows[index][_CPTDeactivationDate] != DBNull.Value)
                    { 
                        oNode.CPTDeactivationDate = Convert.ToDateTime(dtSort.Rows[index][_CPTDeactivationDate]);

                        if (oNode.CPTDeactivationDate <= DateTime.Today)
                        { oNode.ForeColor = Color.Red; }
                    }                    
                }                

                //' ADD NODE ''
                trvMain.Nodes.Add(oNode);

                //' To give the extra functionality of identifying the node
                //' Raise the event after node is added to the treeview
                if (NodeAdded != null)
                {
                    NodeAdded(oNode);
                }
                //'
            }
        }

        private void FillLevelOneTree()
        {

            try
            {
                myTreeNode oParentNode = default(myTreeNode);
                myTreeNode oChildNode = default(myTreeNode);
                ArrayList arrParentNodes = new ArrayList();
                DataView dvTemp = null;
                DataTable dtParent = null;
                DataTable dtChild = null;
                int maxNodeCount = 0;

                dvTemp = dtSort.DefaultView;
                //' ALREADY SORTED/FILTERED TABLE
                dtParent = dvSort.ToTable(true, _ParentMember);
                //' WILL CONTAIN ONLY PARENT NODE (ROWS) ''  '' WILL GIVE DISTINCT RECORDS ''

                if ((dtParent == null) == false)
                {
                    for (int iParent = 0; iParent <= dtParent.Rows.Count - 1; iParent++)
                    {
                        oParentNode = new myTreeNode();
                        oParentNode.Text = Convert.ToString(dtParent.Rows[iParent][_ParentMember]);
                        oParentNode.ImageIndex = _ParentImageIndex;
                        oParentNode.SelectedImageIndex = _SelectedParentImageIndex;

                        //' VALIDATION FOR DUPLICATE PARENT NODES DUE TO CASE SENSITIVE DISTINCT ''
                        if (arrParentNodes.Contains(oParentNode.Text.ToLower()) == true)
                        {
                            //arrParentNodes.Dispose();
                            arrParentNodes = null;
                            continue;
                        }

                        //' FILTER WITH CURRENT PARENT ''
                        dvTemp.RowFilter = null;
                        //Apply Replace to Handle Single quote for sql query to apply row filter
                        dvTemp.RowFilter = _ParentMember + " = '" + dtParent.Rows[iParent][_ParentMember].ToString().Replace("'", "''") + "'";
                        dtChild = dvTemp.ToTable();


                        for (int index = 0; index <= dtChild.Rows.Count - 1; index++)
                        {
                            //' MAXIMUM NODES VALIDATION ''
                            maxNodeCount += 1;
                            if (maxNodeCount >= _MaxNode)
                            {
                                break; // TODO: might not be correct. Was : Exit For
                            }

                            oChildNode = new myTreeNode();

                            //' BIND ID '' 
                            if (!string.IsNullOrEmpty(_ValueMember))
                            {
                                oChildNode.ID = (Int64)dtChild.Rows[index][_ValueMember];
                            }

                            //' BIND Tag '' 
                            if (!string.IsNullOrEmpty(_Tag))
                            {
                                oChildNode.Tag = (object)dtChild.Rows[index][_Tag];
                            }
                            //' BIND Tag '' 
                            if (!string.IsNullOrEmpty(_ConceptID))
                            {
                                oChildNode.ConceptID = Convert.ToString(dtChild.Rows[index][_ConceptID]);
                            }
                            //' BIND CODE '' 
                            if (!string.IsNullOrEmpty(_CodeMember))
                            {
                                oChildNode.Code = Convert.ToString(dtChild.Rows[index][_CodeMember]);
                            }

                            //' BIND DESCRIPTION '' 
                            if (!string.IsNullOrEmpty(_DescriptionMember))
                            {
                                oChildNode.Description = Convert.ToString(dtChild.Rows[index][_DescriptionMember]);
                            }

                            // ''GLO2010-0005444' BIND INDICATOR '' 
                            if (!string.IsNullOrEmpty(_sIndicator))
                            {
                                oChildNode.Indicator = Convert.ToString(dtChild.Rows[index][_sIndicator]);
                            }

                            //' GLO2011-0010684
                            //' Bind the Comments field to the Comment node 
                            if (!string.IsNullOrEmpty(_sComment))
                            {
                                oChildNode.Comments = Convert.ToString(dtChild.Rows[index][_sComment]);
                            }

                            //' BIND UNIT ''
                            if (!string.IsNullOrEmpty(_UnitMember))
                            {
                                oChildNode.Unit = Convert.ToString(dtChild.Rows[index][_UnitMember]);
                            }

                            //' BIND DRUG FORM ''
                            if (!string.IsNullOrEmpty(_DrugFormMember))
                            {
                                oChildNode.DrugForm = Convert.ToString(dtChild.Rows[index][_DrugFormMember]);
                            }

                            //' BIND ROUTE ''
                            if (!string.IsNullOrEmpty(_RouteMember))
                            {
                                oChildNode.Route = Convert.ToString(dtChild.Rows[index][_RouteMember]);
                            }

                            //' BIND FREQUENCY ''
                            if (!string.IsNullOrEmpty(_FrequencyMember))
                            {
                                oChildNode.Frequency = Convert.ToString(dtChild.Rows[index][_FrequencyMember]);
                            }

                            //' BIND DURATION ''
                            if (!string.IsNullOrEmpty(_DurationMember))
                            {
                                oChildNode.Duration = Convert.ToString(dtChild.Rows[index][_DurationMember]);
                            }

                            //' BIND NDC CODE ''
                            if (!string.IsNullOrEmpty(_NDCCodeMember))
                            {
                                oChildNode.NDCCode = Convert.ToString(dtChild.Rows[index][_NDCCodeMember]);
                            }

                            //' BIND DRUG QUANTITY QUALIFIER ''
                            if (!string.IsNullOrEmpty(_DrugQtyQualifierMember))
                            {
                                oChildNode.DrugQtyQualifier = Convert.ToString(dtChild.Rows[index][_DrugQtyQualifierMember]);
                            }

                            //' BIND IS NARCOTICS ''
                            if (!string.IsNullOrEmpty(_IsNarcoticsMember))
                            {
                                oChildNode.IsNarcotics = (Int16)dtChild.Rows[index][_IsNarcoticsMember];
                            }

                            //' BIND IMAGE OBJECT TO NODE ''
                            if (!string.IsNullOrEmpty(_ImageObject))
                            {
                                oChildNode.TemplateResult = dtSort.Rows[index][_ImageObject];
                            }

                            //' BIND DISPLAY TEXT ''
                            oChildNode.Text = Convert.ToString(dtChild.Rows[index][col_DisplayText]);

                            //' ADD NODE TO PARENT ''
                            oParentNode.Nodes.Add(oChildNode);

                            //' 
                            //' To give the extra functionality of identifying the node
                            //' Raise the event after node is added to the treeview
                            if (NodeAdded != null)
                            {
                                NodeAdded(oChildNode);
                            }
                        }

                        //' ADD PARENT NODE TO MAIN TREEVIEW''
                        trvMain.Nodes.Add(oParentNode);
                        arrParentNodes.Add(oParentNode.Text.ToLower());

                        if (maxNodeCount >= _MaxNode)
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }

                trvMain.ExpandAll();
                //'Sandip Darade 20090601
                //'select the first node of the tree
                if (trvMain.Nodes.Count > 0)
                {
                    trvMain.SelectedNode = trvMain.Nodes[0];
                    trvMain.SelectedNode.EnsureVisible();
                }

                trvMain.Sort();
                arrParentNodes.Clear();
                arrParentNodes = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CheckSelectedNodes()
        {
            //' IF CHECKBOXES = TRUE '' THEN CHECK SELECTED NODES IN TREE ''
            if (trvMain.CheckBoxes & arrSelectedNodeIDs.Count > 0)
            {
                isTreeLoading = true;

                if (!string.IsNullOrEmpty(_ParentMember))
                {
                    //' LEVEL 1 TREE ''
                    foreach (TreeNode oParent in trvMain.Nodes)
                    {
                        for (int iTree = 0; iTree <= oParent.Nodes.Count - 1; iTree++)
                        {
                            if (arrSelectedNodeIDs.Contains(((myTreeNode)oParent.Nodes[iTree]).ID))
                            {
                                oParent.Nodes[iTree].Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    //' LEVEL 0 TREE ''
                    for (int iTree = 0; iTree <= trvMain.Nodes.Count - 1; iTree++)
                    {
                        if (arrSelectedNodeIDs.Contains(((myTreeNode)trvMain.Nodes[iTree]).ID))
                        {
                            trvMain.Nodes[iTree].Checked = true;
                        }
                    }
                }

                isTreeLoading = false;
            }
        }

        private void AttachDisplayTextToDataTable(int loopCount)
        {
            //' WHILE APPLICATION DOEVENT, DTMASTER LOSE DISPLAY COLUMN, THEN ADD IT IF NOT PRESENT ''
            if (_DTMaster.Columns.Contains(col_DisplayText) == false)
            {
                DataColumn oColumn = new DataColumn(col_DisplayText);
                _DTMaster.Columns.Add(oColumn);
            }

            //' FILL NEW COLUMN RECORDS ''
            _DTMaster.BeginLoadData();
            switch (_DisplayType)
            {
                case enumDisplayType.Code:
                    for (int index = loopCount; index <= _DTMaster.Rows.Count - 1; index++)
                    {
                        _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember];
                    }


                    break;
                case enumDisplayType.Descripation:
                    for (int index = loopCount; index <= _DTMaster.Rows.Count - 1; index++)
                    {
                        _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_DescriptionMember];
                    }


                    break;
                case enumDisplayType.Code_Description:
                    for (int index = loopCount; index <= _DTMaster.Rows.Count - 1; index++)
                    {
                        //_DTMaster.Rows[index](col_DisplayText) = _DTMaster.Rows[index](_CodeMember) & " - " & _DTMaster.Rows[index](_DescriptionMember)
                        //'Sandip Darade 20090623
                        //'check if description is blank
                        _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember];
                        if ((!string.IsNullOrEmpty(Convert.ToString(_DTMaster.Rows[index][_DescriptionMember]))))
                        {
                            _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember];
                        }
                    }


                    break;
                case enumDisplayType.Code_Description_Unit:
                    for (int index = loopCount; index <= _DTMaster.Rows.Count - 1; index++)
                    {
                        // _DTMaster.Rows[index](col_DisplayText) = _DTMaster.Rows[index](_CodeMember) & " - " & _DTMaster.Rows[index](_DescriptionMember) & " - " & dtSort.Rows(index)(_UnitMember)
                        //'Sandip Darade 20090623
                        //'check if description or unit or both are  blank
                        _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember];

                        if ((!string.IsNullOrEmpty(Convert.ToString(_DTMaster.Rows[index][_DescriptionMember]))))
                        {
                            _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember];
                        }

                        if ((!string.IsNullOrEmpty(Convert.ToString(_DTMaster.Rows[index][_UnitMember]))))
                        {
                            _DTMaster.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_UnitMember];
                        }
                    }


                    break;
                default:
                    //For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    //    dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows[index](_DescriptionMember)
                    //Next
                    //'Sandip Darade 20091012
                    //'above code commented  replacing  it with the code below
                    for (int index = loopCount; index <= _DTMaster.Rows.Count - 1; index++)
                    {
                        _DTMaster.Rows[index][col_DisplayText] = _DTMaster.Rows[index][_CodeMember].ToString().Trim();

                        if ((!string.IsNullOrEmpty(dtSort.Rows[index][_DescriptionMember].ToString().Trim())))
                        {
                            dtSort.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DescriptionMember].ToString().Trim();
                        }

                        if ((!string.IsNullOrEmpty(dtSort.Rows[index][_DrugFormMember].ToString().Trim())))
                        {
                            dtSort.Rows[index][col_DisplayText] += " - " + _DTMaster.Rows[index][_DrugFormMember].ToString().Trim();
                        }
                    }

                    break;
            }
            _DTMaster.EndLoadData();
            dvSort = _DTMaster.DefaultView;
        }

        //SHUBHANGI 20090930
        //Use Clear button to clear search text box
        private void btnClear_Click(System.Object sender, System.EventArgs e)
        {
            txtsearch.ResetText();
            txtsearch.Focus();
        }

        public DataTable FillDrugs(string strsearch = "")
        {

            //' 'gsp_FillDrugs_Mst' pulls top 40 records replace it with 'gsp_FillAllDrugs_Mst' pulling all records
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtdrugs = new DataTable();
            strsearch = strsearch.Replace("'", "''");
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@drugletter", strsearch, ParameterDirection.Input, SqlDbType.Char);
                oParameters.Add("@flag", DrugFlag, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_FillAllDrugs_Mst",oParameters,out dtdrugs);
                oDB.Disconnect();
                return dtdrugs;
            }
            catch //(Exception ex)
            {
                return null;
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        #endregion " Private Methods "
    }
}
