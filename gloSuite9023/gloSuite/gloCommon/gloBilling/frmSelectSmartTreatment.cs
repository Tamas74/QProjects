using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq; 

namespace gloBilling
{
    public partial class frmSelectSmartTreatment : Form
    {
        #region " Variable Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
    //    private Int64 nTreatID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Int64 _SelectedSmartTreatment = 0;
        public ArrayList _SelectedTreatments = null;
        public bool _DialogResult = false;

        private DataView _dv;
        DataTable dtLoadTree;
        gloGlobal.gloICD.CodeRevision _CodeRevision;
        Hashtable ICD9hashtable = new Hashtable();
        Hashtable ICD10hashtable = new Hashtable();
        #endregion " Variable Declarations "

        #region  " C1 Grid Variable Declarations "

        private int COL_TREATID = 0;
        private int COL_TREATMENT = 1;
        private int COL_CHARGES = 2;
        private int COL_UNIT = 3;
        private int COL_TYPE = 4;

        private int COL_COUNT = 5;

        private int _Width = 0;

     //   private string _COL_TYPE_CPTHDR = "CH";
        private string _COL_TYPE_CPTITEM = "CI";
     //   private string _COL_TYPE_ICD9HDR = "IH";
        private string _COL_TYPE_ICD9ITM = "II";
        private string _COL_TYPE_ICD10ITM = "I10";
      //  private string _COL_TYPE_MODHDR = "MH";
        private string _COL_TYPE_MODITM = "MI";

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

        public frmSelectSmartTreatment(string databaseConnectionstring)
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

        public void DisposeonFormClose()
        {
            if (dtLoadTree != null) { dtLoadTree.Dispose(); dtLoadTree = null; }


        }
        #endregion

        #region " Toolstrip Click event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                        //Not in used "SelectAll" button ----------------
                    //case "Select":
                    //    {
                    //        //if (trvTreatment.SelectedNode == null || trvTreatment.SelectedNode.Level != 0)
                    //        if (trvTreatment.Nodes[0] != null)
                    //        {
                    //            _SelectedTreatments = new ArrayList();
                    //            for (int i = 0; i < trvTreatment.Nodes[0].Nodes.Count; i++)
                    //            {
                    //                if (trvTreatment.Nodes[0].Nodes[i].Checked == true)
                    //                {
                    //                    _SelectedTreatments.Add(Convert.ToInt64(trvTreatment.Nodes[0].Nodes[i].Tag));
                    //                }
                    //            }
                    //          _DialogResult = true;
                    //          gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Select, "Smart Treatment/Treatment's selected", gloAuditTrail.ActivityOutCome.Success);
                    //        }
                    //        this.Close();
                    //    }
                    //    break;---------------------------------

                    case "Cancel":  //Close
                        {
                            this.Close();
                        }
                        break;
                    case "ADD":  //Refresh
                        {
                            txtTreatment.Text = "";
                            C1TOSCPT.Clear();
                            designC1Grid();
                            FillCPTs();
                            FillTreatments();

                            trvTreatment.HideSelection = false;
                            if (trvTreatment.GetNodeCount(false) == 1)
                            {
                                if (trvTreatment.Nodes[0].GetNodeCount(false) >= 1)
                                {
                                    trvTreatment.Nodes[0].Nodes[0].Checked = true;
                                    trvTreatment.SelectedNode = trvTreatment.Nodes[0].Nodes[0];
                                }
                            }
                        }
                        break;
                    case "Save":  //Save&Cls
                        {
                            
                            //if (trvTreatment.Nodes.Count > 0)
                            //{
                                //if (ValidateMixTreatment())
                                //{
                                    _SelectedTreatments = new ArrayList();
                                    

                                    foreach (object key in ICD9hashtable.Keys)
                                    {
                                        _SelectedTreatments.Add(Convert.ToInt64(key));
                                    }

                                    foreach (object key in ICD10hashtable.Keys)
                                    {
                                        _SelectedTreatments.Add(Convert.ToInt64(key));
                                    }


                                    //for (int i = 0; i < trvTreatment.Nodes.Count; i++)
                                    //{
                                    //    if (trvTreatment.Nodes[i].Checked == true)
                                    //    {
                                    //        _SelectedTreatments.Add(Convert.ToInt64(trvTreatment.Nodes[i].Tag));
                                    //    }
                                    //}
                                    _DialogResult = true;
                                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Select, "Smart Treatment/Treatment's selected", gloAuditTrail.ActivityOutCome.Success);
                                    //Added Rahul on 20101012
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Select, "Smart Treatment/Treatment's selected", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    //
                                    this.Close();
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Please select either all ICD9 or ICD10 Treatment.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                            //}
                            

                        }
                        break;
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

        #endregion

        #region "Form Load Event "

        private void frmSelectSmartTreatment_Load(object sender, EventArgs e)
        {
            try
            {
                pnlEnterTreatment.Visible = true;
                pnlTOS.Visible = true;
                designC1Grid();
                FillControls();
                trvTreatment.HideSelection = false;
                if (trvTreatment.GetNodeCount(false) >= 1)
                {
                    //if (trvTreatment.Nodes[0].GetNodeCount(false) >= 1)
                    //{
                    //    trvTreatment.Nodes[0].Nodes[0].Checked = true;
                    //    trvTreatment.SelectedNode = trvTreatment.Nodes[0].Nodes[0];
                    //}


                    //trvTreatment.Nodes[0].Checked = true;                    
                    trvTreatment.SelectedNode = trvTreatment.Nodes[0];

                }

                if (gloGlobal.gloPMGlobal.CurrentICDRevision == gloGlobal.gloICD.CodeRevision.ICD10)
                {
                    rbICD10.Checked = true;
                    rbICD9.Checked = false;
                }
                else if (gloGlobal.gloPMGlobal.CurrentICDRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                {
                    rbICD10.Checked = false;
                    rbICD9.Checked = true;
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

        #endregion

        #region " Tree View Fill Methods "

        private void FillControls()
        {
            FillTreatments(true);
           // FillCPTs();
        }

        private void FillTreatments(bool IsFormLoad=false,string _search="")
        {
            gloDatabaseLayer.DBLayer oDB=null;
            DataTable dtTreatment = null;
            try
            {
                if (IsFormLoad)
                {
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    //dtLoadTree = null;
                    string strSQL = "SELECT nTreatmentID,sTreatmentName,ISNULL(nICDRevision,9) as nICDRevision FROM BL_SmartTreatment WITH(NOLOCK)";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strSQL, out dtLoadTree);
                    oDB.Disconnect();
                    
                }
                if (rbAll.Checked == true)
                {
                    dtTreatment = dtLoadTree;
                   
                }
                else
                {
                    if (dtLoadTree != null)
                    {

                        IEnumerable<DataRow> listTreatment = (from result in dtLoadTree.AsEnumerable()
                                                              where result.Field<int>("nICDRevision") == (int)_CodeRevision
                                                              select result);
                        if (listTreatment != null && listTreatment.Count() > 0)
                            dtTreatment = listTreatment.CopyToDataTable();
                        else
                            dtTreatment = null;

                        listTreatment = null;
                    }
                }
                if (dtTreatment != null && _search!="")
                {
                    IEnumerable<DataRow> listTreatment = from result in dtTreatment.AsEnumerable()
                                                         where Convert.ToString(result["sTreatmentName"]).IndexOf(_search, StringComparison.OrdinalIgnoreCase) >= 0
                                                         select result;
                    if (listTreatment != null && listTreatment.Count() > 0)
                        dtTreatment = listTreatment.CopyToDataTable();
                    else
                        dtTreatment = null;

                    listTreatment = null;
                    
                 
                }
                

                #region " Fill Treatments on Treeview "

                //Clear all nodes first.
                trvTreatment.Nodes.Clear();

                //// Add Node at Level 0.--------------
                //trvTreatment.Nodes.Add("Treatments");
                //trvTreatment.Nodes[0].ImageIndex = 1;
                //trvTreatment.Nodes[0].SelectedImageIndex = 1;
                //------------------

                // GET All Type Of Services and bind to treeview
                //Pass 0 to get all Type Of Service
                //dtTreatment = oTreatment.GetTOS(0);

                //Sorting 
                ///

                //Check for Table not null
                if (dtTreatment != null)
               {
                    //Check for Table not empty
                    if (dtTreatment.Rows.Count > 0)

                    {
                        _dv = dtTreatment.DefaultView;

                        _dv.Sort = _dv.Table.Columns["sTreatmentName"].ColumnName;

                        dtTreatment = _dv.ToTable();
                        for (int i = 0; i < dtTreatment.Rows.Count; i++)
                        {
                            //Create Node for each Table Item sTOSCode
                            TreeNode oNode = new TreeNode();
                            oNode.Text = dtTreatment.Rows[i]["sTreatmentName"].ToString();// +" - " + dtTreatment.Rows[i]["sDescription"].ToString();
                            oNode.Tag = dtTreatment.Rows[i]["nTreatmentID"].ToString();
                           
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;
                            oNode.ImageKey = dtTreatment.Rows[i]["nICDRevision"].ToString();
                            //Add Node to Type Of Service Tree
                            // trvTreatment.Nodes[0].Nodes.Add(oNode);-------------

                            if (ICD9hashtable.ContainsKey(oNode.Tag) ||ICD10hashtable.ContainsKey(oNode.Tag))
                                oNode.Checked = true;
                            
                            trvTreatment.Nodes.Add(oNode);
                            //

                            oNode = null;

                        }//for (int i = 0; i < dtTOS.Rows.Count ; i++)

                    }//if (dtTOS.Rows.Count > 0)

                    trvTreatment.ExpandAll();

                } //if (dtTOS != null) 

                #endregion " Treatment Tree Fill "
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
                if (dtTreatment != null)
                {
                    dtTreatment.Dispose();
                    dtTreatment = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }

        }

        private void FillCPTs()
        {
            //CPT oCPT = new CPT(_databaseconnectionstring);
            BillingAssociation oCPT = new BillingAssociation(_databaseconnectionstring);
            DataTable dt_cpt = null;
            btnICD9.Dock = DockStyle.Bottom;
            btnCPT.Dock = DockStyle.Top;
            btnModifier.Dock = DockStyle.Bottom;

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
                    if (btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (btnICD9.Dock == DockStyle.Top)
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
                            tNode.Text = dt_cpt.Rows[i]["sCPTCode"].ToString() + " - " + dt_cpt.Rows[i]["sDescription"].ToString();
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
                if (oCPT != null) { oCPT.Dispose(); oCPT = null; }
                if (dt_cpt != null) { dt_cpt.Dispose(); dt_cpt = null; }
            }

        }

        private void FillICD9s()
        {
            //ICD9 oICD9 = new ICD9(_databaseconnectionstring);
            BillingAssociation oICD9 = new BillingAssociation(_databaseconnectionstring);
            DataTable dtICD9 = null;

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
                dtICD9 = oICD9.GetICD9s();//.GetICD9s();


                #region " Sort Data "

                _dv = dtICD9.DefaultView;

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (btnICD9.Dock == DockStyle.Top)
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
                            tNode.Text = dtICD9.Rows[i]["sICD9Code"].ToString() + " - " + dtICD9.Rows[i]["sDescription"].ToString();
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
                if (oICD9 != null) { oICD9.Dispose(); oICD9 = null; }
                if (dtICD9 != null) { dtICD9.Dispose(); dtICD9 = null; }
            }
        }

        private void FillModifiers()
        {
            BillingAssociation oModifier = new BillingAssociation(_databaseconnectionstring);
            DataTable dt_Mod = null;

            try
            {
                #region " Modifier Tree Fill "

                //Clear Treee Nodes
                trvCPT.Nodes.Clear();

                // Add Parent Node
                trvCPT.Nodes.Add("Modifier");
                //
                trvCPT.Nodes[0].ImageIndex = 2;
                trvCPT.Nodes[0].SelectedImageIndex = 2;

                // GET All Modifier and bind to treeview
                dt_Mod = oModifier.GetModifiers();

                #region " Sort Data "

                _dv = dt_Mod.DefaultView;

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (btnModifier.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sModifierCode"].ColumnName;
                    }
                    if (btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                }
                else
                    _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;

                //

                dt_Mod = _dv.ToTable();

                #endregion " Sort Data "


                if (dt_Mod != null)
                {
                    if (dt_Mod.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dt_Mod.Rows.Count - 1; i++)
                        {
                            // create and set value to CPTs Nodes.
                            TreeNode tNode = new TreeNode();
                            tNode.Text = dt_Mod.Rows[i]["sModifierCode"].ToString() + " - " + dt_Mod.Rows[i]["sDescription"].ToString();
                            tNode.Tag = dt_Mod.Rows[i]["nModifierID"].ToString();
                            tNode.ImageIndex = 0;
                            tNode.SelectedImageIndex = 0;
                            // tNode.ImageKey = dt_cpt.Rows[i]["sCPTCode"].ToString();

                            // Add Node to Modifier tree.
                            trvCPT.Nodes[0].Nodes.Add(tNode);
                        }
                    }

                    // Show tree Expanded;
                    trvCPT.ExpandAll();
                    trvCPT.SelectedNode = trvCPT.Nodes[0];

                }

                #endregion " Modifier Tree Fill "
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
                if (oModifier != null) { oModifier.Dispose(); oModifier = null; }
                if (dt_Mod != null) { dt_Mod.Dispose(); dt_Mod = null; }
            }

        }

        #endregion " Tree View Fill Methods "

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

                C1TOSCPT.Tree.Column = COL_TREATMENT;

                // Set Property for TOS_ID
                C1TOSCPT.Cols[COL_TREATID].Width = 0;
                C1TOSCPT.Cols[COL_TREATID].AllowEditing = false;
                C1TOSCPT.SetData(0, COL_TREATID, "Treatment ID");
                //

                // Set Property for COL_TOSName
                //C1TOSCPT.Cols[COL_TOSNAME].Width = (int)(_Width * 0.5);
                C1TOSCPT.Cols[COL_TREATMENT].Width = (int)(_Width * 2.5);
                C1TOSCPT.Cols[COL_TREATMENT].AllowEditing = false;
                C1TOSCPT.SetData(0, COL_TREATMENT, "Treatment");
                //

                C1TOSCPT.Cols[COL_CHARGES].Width = 0;
                C1TOSCPT.Cols[COL_CHARGES].Visible = false;
                C1TOSCPT.Cols[COL_TYPE].Width = 0;
                C1TOSCPT.Cols[COL_TYPE].Visible = false;
                C1TOSCPT.Cols[COL_UNIT].Width = 0;
                C1TOSCPT.Cols[COL_UNIT].Visible = false;


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

        #region " Treatment After Select Treeview Event "

        private void trvTreatment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //if (trvTreatment.SelectedNode != trvTreatment.Nodes[0])
                //{
                //    designC1Grid();
                //   FillTreatmentInGrid();
                //}
                //else
                //{
                //    designC1Grid();
                //}
                if (trvTreatment.Nodes.Count > 0)
                {
                    if (trvTreatment.SelectedNode == null)
                    {
                        designC1Grid();
                    }
                    else
                    {
                        designC1Grid();
                        FillTreatmentInGrid();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool _boolChecked = true;
        private void trvTreatment_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (_boolChecked == true)
                {
                    //boolean for handling the Recursive Loops.
                    _boolChecked = false;
                    trvTreatment.SelectedNode = e.Node;
                    if (e.Node.Level == 0)
                    {
                        for (int i = 0; i < trvTreatment.Nodes[0].Nodes.Count; i++)
                        {
                            //set the Parent status for the All childs
                            trvTreatment.Nodes[0].Nodes[i].Checked = e.Node.Checked;
                        }
                        //boolean for handling the Recursive Loops.
                       
                    }
                    else
                    {
                        checkAllChilds(e.Node.Parent);
                    }

                    if (e.Node.ImageKey == ((int)gloGlobal.gloICD.CodeRevision.ICD9).ToString())
                    {
                        if (e.Node.Checked == true)
                        {
                            if (!ICD9hashtable.ContainsKey(e.Node.Tag))
                                ICD9hashtable.Add(e.Node.Tag, e.Node.Text);
                        }
                        else
                            ICD9hashtable.Remove(e.Node.Tag);
                    }
                    else
                    {
                        if (e.Node.Checked == true)
                        {
                        if (! ICD10hashtable.ContainsKey(e.Node.Tag))
                            ICD10hashtable.Add(e.Node.Tag, e.Node.Text);
                        }
                        else
                            ICD10hashtable.Remove(e.Node.Tag);
                    }

                    _boolChecked = true;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Summary
        //Check all the Parent Childs are checked.

        //Parameters.
        //TreeNode : Parent Node of treeview.

        //Return
        //If all childs are selected then Select Parent.
        private void checkAllChilds(TreeNode ParentNode)
        {
            bool _Checked = true;
            foreach (TreeNode tnode in ParentNode.Nodes)
            {
                if (tnode.Checked == false)
                {
                    _Checked = false;
                    break;
                }
            }
            ParentNode.Checked = _Checked;
        }

        #endregion

        #region " Method to Fill Treatment Details in Grid "

        private void FillTreatmentInGrid()
        {
            DataTable dtTreatmentCPT = null;
            DataTable dtTreatmentICD9 = null;
            DataTable dtTreatmentModifier = null;

            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                // On root Node Hit do nothing
                //if (trvTreatment.SelectedNode == null || trvTreatment.SelectedNode.Level == 0)-------
                //{
                //    trvTreatment.ExpandAll();
                //    return;
                //}------------------------------------------------------------------------------------
                if (trvTreatment.Nodes.Count > 0)
                {
                    if (trvTreatment.SelectedNode == null)
                    {
                        trvTreatment.ExpandAll();
                        return;
                    }
                    strSQL = "SELECT isnull(nICDRevision,9) FROM BL_SmartTreatment WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " ";
                    oDB.Connect(false);
                    object obj = oDB.ExecuteScalar_Query(strSQL);
                    if (obj != null && Convert.ToString(obj) != "")
                    {
                        if (Convert.ToInt16(obj) == (int)gloGlobal.gloICD.CodeRevision.ICD10)
                            _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD10;
                        else
                            _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD9;
                    }
                    else
                    {
                        _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD9;
                    }
                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription FROM BL_SmartTreatmentCPT WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " ";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                    oDB.Disconnect();


                    //if (trvTreatment.SelectedNode.Level == 1)----------
                    if (trvTreatment.SelectedNode.Level == 0)
                    {

                        if (dtTreatmentCPT.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTreatmentCPT.Rows.Count; i++)
                            {
                                C1TOSCPT.Rows.Add();
                                C1TOSCPT.Tree.Indent = 21;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[0];
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + " - " + dtTreatmentCPT.Rows[i]["sCPTDescription"].ToString().Replace("'", "''");
                                int _UpdateRowIndex = C1TOSCPT.Rows.Count - 1;
                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_CPTITEM);

                                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";
                                oDB.Connect(false);
                                oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                                //
                                oDB.Disconnect(); 
                                //

                                if (dtTreatmentICD9.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtTreatmentICD9.Rows.Count; j++)
                                    {
                                        C1TOSCPT.Rows.Add();
                                        C1TOSCPT.Tree.Indent = 21;
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[1];
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentICD9.Rows[j]["sICD9Code"].ToString() + " - " + dtTreatmentICD9.Rows[j]["sICD9Description"].ToString();
                                        int _UpdateRowIndex1 = C1TOSCPT.Rows.Count - 1;
                                        if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                                        {
                                            C1TOSCPT.SetData(_UpdateRowIndex1, COL_TYPE, _COL_TYPE_ICD9ITM);
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[1];
                                        }
                                        else
                                        {
                                            C1TOSCPT.SetData(_UpdateRowIndex1, COL_TYPE, _COL_TYPE_ICD10ITM);
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[3];
                                        }


                                        //strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'";
                                        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'"
                                       + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";

                                        oDB.Connect(false);
                                        oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                        oDB.Disconnect();

                                        if (dtTreatmentModifier.Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dtTreatmentModifier.Rows.Count; k++)
                                            {
                                                C1TOSCPT.Rows.Add();
                                                C1TOSCPT.Tree.Indent = 21;
                                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 2;
                                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[2];
                                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentModifier.Rows[k]["sModifierCode"].ToString() + " - " + dtTreatmentModifier.Rows[k]["sModifierDesc"].ToString();
                                                int _UpdateRowIndex2 = C1TOSCPT.Rows.Count - 1;
                                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_MODITM);
                                            }
                                        }
                                    }
                                }

                                //added by mitesh (Resolved Bug 7291)
                                else
                                {
                                    strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WITH(NOLOCK) WHERE nTreatmentID=" + trvTreatment.SelectedNode.Tag + " and isnull(sICD9Code,'') ='' " +
                                                   " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";

                                    oDB.Connect(false);
                                    oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                                    oDB.Disconnect();

                                    if (dtTreatmentModifier.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dtTreatmentModifier.Rows.Count; k++)
                                        {
                                            C1TOSCPT.Rows.Add();
                                            C1TOSCPT.Tree.Indent = 21;
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[2];
                                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentModifier.Rows[k]["sModifierCode"].ToString() + " - " + dtTreatmentModifier.Rows[k]["sModifierDesc"].ToString();
                                            int _UpdateRowIndex2 = C1TOSCPT.Rows.Count - 1;
                                            C1TOSCPT.SetData(_UpdateRowIndex2, COL_TYPE, _COL_TYPE_MODITM);
                                        }
                                    }
                                }
                                //------

                            }
                        }

                    }

                }






            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (dtTreatmentCPT != null) { dtTreatmentCPT.Dispose(); dtTreatmentCPT = null; }
                if (dtTreatmentICD9 != null) { dtTreatmentICD9.Dispose(); dtTreatmentICD9 = null; }
                if (dtTreatmentModifier != null) { dtTreatmentModifier.Dispose(); dtTreatmentModifier = null; }
               
            }
        }

        #endregion

        #region " Form Shown Event "

        private void frmSelectSmartTreatment_Shown(object sender, EventArgs e)
        {
            try
            {
                //designC1Grid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion

        #region " CPT Treeview Double Click "

        private void trvCPT_DoubleClick(object sender, EventArgs e)
        {
            AddCPTICD9();
        }

        private void AddCPTICD9()
        {
            try
            {
                string _CPTName = "CPT";
                string _ICDName = "ICD9";
                // string _MODName = "Modifier";
                bool _IsCPT = false;
                bool _IsICD9 = false;
                int _RowCntr = 0;
                C1.Win.C1FlexGrid.Node oC1Node = null;

                if (trvCPT.SelectedNode != null)
                {
                    if (trvCPT.SelectedNode.GetNodeCount(false) > 1)
                    {
                        if (trvCPT.SelectedNode.Text.ToUpper() == _CPTName.ToUpper())
                        {
                            _IsCPT = true;
                        }
                        else if (trvCPT.SelectedNode.Text.ToUpper() == _ICDName.ToUpper())
                        {
                            _IsICD9 = true;
                        }
                    }
                    else
                    {
                        if (trvCPT.SelectedNode.Parent.Text.ToUpper() == _CPTName.ToUpper())
                        {
                            _IsCPT = true;
                        }
                        else if (trvCPT.SelectedNode.Parent.Text.ToUpper() == _ICDName.ToUpper())
                        {
                            _IsICD9 = true;
                        }
                    }
                }

                if (_IsCPT == true)
                {
                    for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                    {
                        if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                        {
                            C1TOSCPT.Rows.Add();
                            C1TOSCPT.Tree.Indent = 21;
                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
                            C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = trvCPT.Nodes[0].Nodes[_RowCntr].Text;
                            int _UpdateRowIndex = C1TOSCPT.Rows.Count - 1;
                            C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_CPTITEM);
                        }
                    }
                }
                else if (_IsICD9 == true)
                {
                    if (C1TOSCPT.RowSel >= 0)
                    {
                        if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE) != null)
                        {
                            if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_CPTITEM.ToUpper())
                            {
                                oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node;
                            }
                            else
                            {
                                if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                            }
                        }
                    }

                    if (oC1Node != null)
                    {
                        for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                        {
                            if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                            {
                                oC1Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, trvCPT.Nodes[0].Nodes[_RowCntr].Text);
                                int _UpdateRowIndex = oC1Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_ICD9ITM);
                            }
                        }
                    }
                }
                else
                {
                    if (C1TOSCPT.RowSel >= 0)
                    {
                        if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE) != null)
                        {
                            if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper())
                            {
                                oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node;
                            }
                            else
                            {
                                if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_MODITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                            }
                        }
                    }

                    if (oC1Node != null)
                    {
                        for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                        {
                            if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                            {
                                oC1Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, trvCPT.Nodes[0].Nodes[_RowCntr].Text);
                                int _UpdateRowIndex = oC1Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_MODITM);
                            }
                        }
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

            }
        }

        #endregion

        #region " Private Method To Get CPTs Associated with Treatment "



        #endregion

        #region " Private Method to AddCPTs "

        private void AddCPTs()
        {
            gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();

            try
            {

                //Check if Parent node(TOS) is present if not return
                if (C1TOSCPT.Rows.Count == 1)
                {
                    MessageBox.Show("Please select a Type Of Service to Associate.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (btnCPT.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                        }
                        if (btnICD9.Dock == DockStyle.Top)
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
                        if (btnCPT.Dock == DockStyle.Top)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 2)
                            {
                                C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            }
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();

                        }
                        if (btnICD9.Dock == DockStyle.Top)
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
                        C1TOSCPT.SetData(C1TOSCPT.Row, COL_TREATID, oItems[j].ID.ToString());

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

        #endregion

        #region " CPT ,Modifier and ICD9 Button Clicks  "

        private void btnCPT_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_btnICD9.Dock = DockStyle.Bottom;
                pnl_btnCPT.Dock = DockStyle.Top;
                pnl_btnModifier.Dock = DockStyle.Bottom;

                FillCPTs();

                //first check if nodes are added to grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "ICD9" || C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "Modifier")
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

                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
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
                pnl_btnModifier.Dock = DockStyle.Bottom;
                pnl_btnICD9.Dock = DockStyle.Top;
                pnl_btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
                pnl_btnICD9.BackgroundImageLayout = ImageLayout.Stretch;


                FillICD9s();
                //first check if nodes are added to the grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "CPT" || C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "Modifier")
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

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_btnCPT.Dock = DockStyle.Bottom;
                pnl_btnICD9.Dock = DockStyle.Bottom;
                pnl_btnModifier.Dock = DockStyle.Top;
                trvCPT.BringToFront();
                FillModifiers();
                //first check if nodes are added to the grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level != 0)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "CPT" || C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString() == "ICD9")
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                        }
                    }
                }


                //deselect the check box
                chkBoxSelect.Checked = false;
                pnl_btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
                pnl_btnICD9.BackgroundImageLayout = ImageLayout.Stretch;

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

        #endregion

        #region "  Button Designer Events "

        private void btnICD9_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnICD9_MouseLeave(object sender, EventArgs e)
        {
            if (btnICD9.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnCPT_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCPT_MouseLeave(object sender, EventArgs e)
        {
            if (btnCPT.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnModifier_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_ButtonHover;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnModifier_MouseLeave(object sender, EventArgs e)
        {
            if (btnModifier.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        #endregion

        #region " CPT and ICD9 Remove Event "

        private void C1TOSCPT_MouseDown(object sender, MouseEventArgs e)
        {
            // Select the Current Row
            //if (C1TOSCPT.HitTest(e.X, e.Y).Row > 0)
            //{
            //    C1TOSCPT.Rows[C1TOSCPT.HitTest(e.X, e.Y).Row].Node.Select();

            //    // If Right Click Then show the menu with cursor location.
            //    if (e.Button.ToString() == "Right")
            //    {
            //        //cxtMS.Show(Cursor.Position.X, Cursor.Position.Y);
            //    }
            //}
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check type of Deletion selected
            try
            {

                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
                {
                    // Remove all  ICD9's  and Modifiers associated with this CPT 

                    if (MessageBox.Show("Are you sure you want to remove this ICD9 and its associate", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children > 0)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
                            C1TOSCPT.Rows[C1TOSCPT.Row].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Select();
                        }

                        //remove all child nodes if any of the first child(ICD9) of Parent(CPT)
                        for (int i = 0; C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children != 0; i++)
                        {
                            //first remove all Modifier nodes.
                            for (int j = 0; C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.Children != 0; j++)
                            {
                                C1TOSCPT.Rows[C1TOSCPT.Row + 2].Node.RemoveNode();
                            }
                            C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.RemoveNode();
                        }
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.RemoveNode();
                    }


                }
                else if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 1) //Remove all ICD9 nodes
                {
                    string nodeTitle = C1TOSCPT.Rows[C1TOSCPT.Row].Node.Data.ToString();
                    if (MessageBox.Show("Are you sure you want to remove all Associated " + nodeTitle + " ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //first remove all child nodes if any
                        for (int i = 0; C1TOSCPT.Rows[C1TOSCPT.Row].Node.Children != 0; i++)
                        {
                            C1TOSCPT.Rows[C1TOSCPT.Row + 1].Node.RemoveNode();
                        }
                        C1TOSCPT.Rows[C1TOSCPT.Row].Node.RemoveNode();
                    }
                }
                else // remove single item
                {
                    // For Delete the Modifiers selected.
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 2)
                    {
                        C1TOSCPT.RemoveItem(C1TOSCPT.Row);
                    }
                }

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }


        #endregion

        #region " Private Method Save Association "

        private void SaveAssociation()
        {
            gloGeneralItem.gloItem oItem=null;
            //gloGeneralItem.gloSubItems oSubItems;
            CPT oCPT = new CPT(_databaseconnectionstring);
            ICD9 oICD9 = new ICD9(_databaseconnectionstring);
          
            //TOSCPT oTOSCPT;

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
                        int rowIndex = C1TOSCPT.RowSel - 1;
                        int childs = C1TOSCPT.Rows[rowIndex].Node.Children;
                        for (int i = 1; i <= C1TOSCPT.Rows[rowIndex].Node.Children; i++)
                        {

                            oItem.ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TREATID));

                            //oSubItems = new gloGeneralItem.gloSubItems();
                            //dt = new DataTable();
                            DataTable dt = null;
                            Int64 tempCPTID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TREATID));
                            oCPT.CPTID = tempCPTID;
                            dt = oCPT.getCPT();
                            if (dt != null)
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 1; // Set subitem id to 1 to Identify CPT
                                oSubItem.Code = dt.Rows[0][0].ToString();
                                oSubItem.Description = dt.Rows[0][1].ToString();

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                dt.Dispose();
                                dt = null;
                            }
                        }

                        //

                        //select the last child for this TOS node i.e ICD9
                        C1TOSCPT.Rows[cntTOS].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Select();
                        rowIndex = C1TOSCPT.RowSel - 1;
                        childs = C1TOSCPT.Rows[rowIndex].Node.Children;
                        for (int i = 1; i <= C1TOSCPT.Rows[rowIndex].Node.Children; i++)
                        {
                            oItem.ID = Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TREATID));

                            //oSubItems = new gloGeneralItem.gloSubItems();
                            //dt = new DataTable();
                            DataTable dt = null;
                            Int64 tempICD9ID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TREATID));
                            dt = oICD9.GetICD9(tempICD9ID);
                            if (dt != null)
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 2; //set subitem id to  2 to identify ICD9
                                oSubItem.Code = dt.Rows[0][0].ToString();
                                oSubItem.Description = dt.Rows[0][1].ToString();

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        //

                        //oTOSCPT = new TOSCPT(_databaseconnectionstring);
                        //first remove existing association and then add the new 
                        //oTOSCPT.RemoveAssociation(Convert.ToInt64(C1TOSCPT.GetData(cntTOS, COL_TREATID)));
                        //oTOSCPT.Add(oItem);
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
               // if (dt != null) { dt.Dispose(); dt = null; }
                if (oItem != null) { oItem.Dispose(); oItem = null; }
                if (oCPT != null) { oCPT.Dispose(); oCPT = null; }
                if (oICD9 != null) { oICD9.Dispose(); oICD9 = null; }
            }
        }

        #endregion " Private Method Save Association "

        #region "  After Select CPT Treeview Event"

        private void trvCPT_AfterSelect(object sender, TreeViewEventArgs e)
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

        #endregion

        #region " Checkbox Select Event "

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

        #endregion

        #region " Search Funtionality - Text Change Events "

        private void txtCPTSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            BillingAssociation oAssociation = new BillingAssociation(_databaseconnectionstring);
            //ICD9 oICD9 = new ICD9(_databaseconnectionstring);

            try
            {
                string strSearch = "";

                if (txtCPTSearch.Text != "")
                {
                    strSearch = txtCPTSearch.Text.Replace("'", "''");
                    strSearch = strSearch.Replace("[", "") + "";
                }
                else
                {
                    strSearch = "";
                }
                //

                #region " CPT Filter "

                if (btnCPT.Dock == DockStyle.Top)
                {
                    dt = oAssociation.GetCPTs();
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

                if (btnICD9.Dock == DockStyle.Top)
                {
                    dt = oAssociation.GetICD9s();
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

                #region " Modifier Filter "

                if (btnModifier.Dock == DockStyle.Top)
                {
                    dt = oAssociation.GetModifiers();
                    _dv = dt.DefaultView;


                    if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                    {
                        if (rbCode.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sModifierCode"].ColumnName + " Like '%" + strSearch + "%'";
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
                            _dv.RowFilter = _dv.Table.Columns["sModifierCode"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        if (rbDescription.Checked == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                        }
                    }

                }

                #endregion " Modifier Filter "

                #region " Sort Filtered Data "

                //Sort Data
                if (rbCode.Checked == true)
                {
                    if (btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                    if (btnModifier.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sModifierCode"].ColumnName;
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
                if (btnCPT.Dock == DockStyle.Top)
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
                if (btnICD9.Dock == DockStyle.Top)
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

                if (btnModifier.Dock == DockStyle.Top)
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

                                tNode.Text = dt.Rows[i]["sModifierCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dt.Rows[i]["nModifierID"].ToString();

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
            finally
            {
                if (dt != null) { dt.Dispose(); dt = null; }
                if (oAssociation != null) { oAssociation.Dispose(); oAssociation = null; }
            }
        }

        private void txtTOSSearch_TextChanged(object sender, EventArgs e)
        {
            //gloDatabaseLayer.DBLayer oDB = null;

            try
            {
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
                FillTreatments(false,strSearch);
                //DataTable dtTreatment = dtLoadTree;
                //oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                //string strSQL = "SELECT nTreatmentID,sTreatmentName FROM BL_SmartTreatment WITH(NOLOCK)";
                //oDB.Connect(false);
                //oDB.Retrive_Query(strSQL, out dtTreatment);
                //oDB.Disconnect();

                //#region " Filter And Fill Treatments in Treeview "

                ////Clear all nodes first.
                //trvTreatment.Nodes.Clear();

                ////// Add Node at Level 0.
                ////trvTreatment.Nodes.Add("Treatments");
                ////trvTreatment.Nodes[0].ImageIndex = 1;
                ////trvTreatment.Nodes[0].SelectedImageIndex = 1;
                //////
                //if (dtTreatment != null)
                //{
                //    //Check for Table not empty
                //    if (dtTreatment.Rows.Count > 0)
                //    {
                //        _dv = dtTreatment.DefaultView;

                //        if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                //        {
                //            _dv.RowFilter = _dv.Table.Columns["sTreatmentName"].ColumnName + " Like '%" + strSearch + "%'";
                //        }
                //        else
                //        {
                //            _dv.RowFilter = _dv.Table.Columns["sTreatmentName"].ColumnName + " Like '%" + strSearch + "%'";
                //        }
                //        //Sorting 
                //        _dv.Sort = _dv.Table.Columns["sTreatmentName"].ColumnName;

                //        dtTreatment = _dv.ToTable();

                //        //Check for Table not null

                //        for (int i = 0; i < dtTreatment.Rows.Count; i++)
                //        {
                //            //Create Node for each Table Item sTreatmentName
                //            TreeNode oNode = new TreeNode();
                //            oNode.Text = dtTreatment.Rows[i]["sTreatmentName"].ToString();
                //            oNode.Tag = dtTreatment.Rows[i]["nTreatmentID"].ToString();
                //            //
                //            oNode.ImageIndex = 0;
                //            oNode.SelectedImageIndex = 0;
                //            oNode.ImageKey = dtTreatment.Rows[i]["nICDRevision"].ToString();
                //            ////Add Node to Treatment
                //            //trvTreatment.Nodes[0].Nodes.Add(oNode);
                //            ////
                //            if (ICD9hashtable.ContainsKey(oNode.Tag) || ICD10hashtable.ContainsKey(oNode.Tag))
                //                oNode.Checked = true;
                //            trvTreatment.Nodes.Add(oNode);

                //            oNode = null;

                //        }//for (int i = 0; i < dtTreatment.Rows.Count ; i++)

                //    }//if (dtTreatment.Rows.Count > 0)

                //    trvTreatment.ExpandAll();

                //} //if (dtTreatment!= null) 

                //#endregion " Treatment Tree Fill "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (oDB != null)
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}

            }
        }


        #endregion

        #region " RadioButton Check Event "

        private void rbCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCode.Checked == true)
            { rbCode.Font = gloGlobal.clsgloFont.gFont_BOLD;}// new Font("Tahoma", 9, FontStyle.Bold); }
            else
            { rbCode.Font = gloGlobal.clsgloFont.gFont;}//new Font("Tahoma", 9, FontStyle.Regular); }


            if (btnCPT.Dock == DockStyle.Top)
            { FillCPTs(); }
            else if (btnICD9.Dock == DockStyle.Top)
            { FillICD9s(); }
            else if (btnModifier.Dock == DockStyle.Top)
            { FillModifiers(); }
        }

        #endregion

        #region " Save Smart Treatments"

        private Int64 SaveSmartTreatment(string TreatmentName)
        {
            string strSQL = "";
            Int64 TreatID = 0;
            int _result = 0;
            object ID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                strSQL = "SELECT Max(nTreatmentID) FROM BL_SmartTreatment WITH(NOLOCK)";
                ID = oDB.ExecuteScalar_Query(strSQL);
                if (ID.ToString() == "")
                {
                    ID = 0;
                }
                TreatID = Convert.ToInt64(ID) + 1;
                if (TreatmentName != "")
                {
                    strSQL = "INSERT INTO BL_SmartTreatment (nTreatmentID,sTreatmentName)VALUES(" + TreatID + ",'" + TreatmentName + "')";
                    _result = oDB.Execute_Query(strSQL);
                    oDB.Disconnect();
                }
                return TreatID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        private void SaveSmartCPT_Treatment(Int64 TreatID)
        {
            gloGeneralItem.gloItem oItem = null;
            //gloGeneralItem.gloSubItems oSubItems;

            gloGeneralItem.gloItem oModItem = null;
            gloGeneralItem.gloSubItem oModSubItem = null;

            string strSQL = "";
            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (TreatID != 0)
                {
                    for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
                    {
                        C1.Win.C1FlexGrid.Node oParentNode = null;
                        oItem = new gloGeneralItem.gloItem();
                        
                        oParentNode = C1TOSCPT.Rows[cntTOS].Node;
                        //oSubItems = new gloGeneralItem.gloSubItems();
                        if (oParentNode.Level == 0)
                        {
                            Object CPT = oParentNode.Data;
                            oItem.Code = CPT.ToString();

                            string[] sCPT = null;
                            sCPT = oItem.Code.Split('-');
                            if (sCPT.Length > 1)
                            {
                                //strSQL = "INSERT INTO BL_SmartTreatmentCPT (nTreatmentID,sCPTCode,sCPTDescription) VALUES  ( " + TreatID + ",'" + sCPT[0].ToString().Trim() + "' ,'" + sCPT[1].ToString().Trim() + "' )";
                                //oDB.Connect(false);
                                //_result = oDB.Execute_Query(strSQL);
                                //oDB.Disconnect();
                            }
                            else
                            {
                                return;
                            }
                            C1.Win.C1FlexGrid.CellRange cellRng = oParentNode.GetCellRange();
                            C1.Win.C1FlexGrid.Node oChildNode = null;

                            gloGeneralItem.gloSubItem oSubItem = null;
                            oModItem = new gloGeneralItem.gloItem();

                            for (int j = cellRng.TopRow + 1; j <= cellRng.BottomRow; j++)
                            {
                                oSubItem = new gloGeneralItem.gloSubItem();
                                oChildNode = C1TOSCPT.Rows[j].Node;


                                Object ICD9 = oChildNode.Data;
                                oSubItem.Code = ICD9.ToString();

                                string[] sICD9 = null;
                                sICD9 = oSubItem.Code.Split('-');
                                if (sICD9.Length > 0)
                                {
                                    //strSQL = "INSERT INTO BL_SmartTreatmentICD9 (nTreatmentID ,sCPTCode,sCPTDescription  ,sICD9Code ,sICD9Description,dCharges ,nUnits) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim() + "', '" + sCPT[1].ToString().Trim() + "', '" + sICD9[0].ToString().Trim() + "','" + sICD9[1].ToString().Trim() + "',0,0 )";
                                    //oDB.Connect(false);
                                    //_result = oDB.Execute_Query(strSQL);
                                    //oDB.Disconnect();
                                }
                                else
                                {
                                    return;
                                }
                                C1.Win.C1FlexGrid.Node oMODNode = null;
                                C1.Win.C1FlexGrid.CellRange oModRange = oChildNode.GetCellRange();

                                for (int k = oModRange.TopRow + 1; k <= oModRange.BottomRow; k++)
                                {
                                    oModSubItem = new gloGeneralItem.gloSubItem();
                                    oMODNode = C1TOSCPT.Rows[k].Node;

                                    Object Modifier = oMODNode.Data;
                                    oModSubItem.Code = Modifier.ToString();

                                    oModItem.SubItems.Add(oModSubItem);
                                    oModSubItem.Dispose();

                                    string[] sMod = null;
                                    sMod = oModSubItem.Code.Split('-');
                                    if (sMod.Length > 0)
                                    {
                                        strSQL = "INSERT INTO BL_SmartTreatmentModifier (nTreatmentID,sCPTCode,sCPTDescription,sICD9Code,sICD9Description,sModifierCode,sModifierDesc) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim() + "', '" + sCPT[1].ToString().Trim() + "', '" + sICD9[0].ToString().Trim() + "','" + sICD9[1].ToString().Trim() + "','" + sMod[0].ToString().Trim() + "','" + sMod[1].ToString().Trim() + "')";
                                        oDB.Connect(false);
                                        _result = oDB.Execute_Query(strSQL);
                                        oDB.Disconnect();
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }

                                oItem.SubItems.Add(oSubItem);
                                oSubItem.Dispose();

                                j = oModRange.BottomRow;
                            }
                            cntTOS = cellRng.BottomRow;

                        }
                        if (oItem.Code != "")
                        {
                            if (TreatID != 0)
                            {
                                string[] sCPT = null;
                                sCPT = oItem.Code.Split('-');
                                if (sCPT.Length > 1)
                                {
                                    strSQL = "INSERT INTO BL_SmartTreatmentCPT (nTreatmentID,sCPTCode,sCPTDescription) VALUES  ( " + TreatID + ",'" + sCPT[0].ToString().Trim() + "' ,'" + sCPT[1].ToString().Trim() + "' )";
                                    oDB.Connect(false);
                                    _result = oDB.Execute_Query(strSQL);
                                    oDB.Disconnect();
                                }
                                if (oItem.SubItems.Count > 0)
                                {
                                    for (int i = 0; i < oItem.SubItems.Count; i++)
                                    {
                                        string[] sICD9 = null;
                                        sICD9 = oItem.SubItems[i].Code.Split('-');
                                        strSQL = "INSERT INTO BL_SmartTreatmentICD9 (nTreatmentID ,sCPTCode,sCPTDescription  ,sICD9Code ,sICD9Description,dCharges ,nUnits) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim() + "', '" + sCPT[1].ToString().Trim() + "', '" + sICD9[0].ToString().Trim() + "','" + sICD9[1].ToString().Trim() + "',0,0 )";
                                        oDB.Connect(false);
                                        _result = oDB.Execute_Query(strSQL);
                                        oDB.Disconnect();
                                        //if (oModItem.SubItems.Count > 0)
                                        //{
                                        //    for (int k = 0; k < oModItem.SubItems.Count; k++)
                                        //    {
                                        //        string[] sMod = null;
                                        //        sMod = oModItem.SubItems[k].Code.Split('-');
                                        //        strSQL = "INSERT INTO BL_SmartTreatmentModifier (nTreatmentID,sCPTCode,sCPTDescription,sICD9Code,sICD9Description,sModifierCode,sModifierDesc) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim() + "', '" + sCPT[1].ToString().Trim() + "', '" + sICD9[0].ToString().Trim() + "','" + sICD9[1].ToString().Trim() + "','" + sMod[0].ToString().Trim() + "','" + sMod[1].ToString().Trim() + "')";
                                        //        oDB.Connect(false);
                                        //        _result = oDB.Execute_Query(strSQL);
                                        //        oDB.Disconnect();
                                        //    }
                                        //}
                                    }
                                }

                            }
                        }
                        if (oModItem != null)
                        {
                            oModItem.SubItems.Clear();
                            oModItem.Dispose();
                            oModItem = null;
                        }
                        if (oItem != null)
                        {
                            oItem.SubItems.Clear();
                            oItem.Dispose();
                            oItem = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not add treatment", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) {oDB.Disconnect();oDB.Dispose();}
                if (oItem != null) {  oItem.Dispose(); oItem=null;}
                if (oModItem != null) {  oModItem.Dispose(); oModItem=null;}
                if (oModSubItem != null) {  oModSubItem.Dispose(); oModSubItem=null;} 
                
            }

        }

        #endregion

        private void rbDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescription.Checked == true)
            { rbDescription.Font = gloGlobal.clsgloFont.gFont_BOLD; }//new Font("Tahoma", 9, FontStyle.Bold); }
            else
            { rbDescription.Font = gloGlobal.clsgloFont.gFont; }//new Font("Tahoma", 9, FontStyle.Regular); }
        }

        private bool  ValidateMixTreatment()
        {
            bool _result = true;
            try
            {
                if (trvTreatment.Nodes.Count > 0)
                {

                    string  []str =(from TreeNode Result in trvTreatment.Nodes 
                              where Result.Checked
                              select Convert.ToString(Result.ImageKey)).ToArray();

                    if ((str != null) && (str.Contains(((int)gloGlobal.gloICD.CodeRevision.ICD9).ToString()) && str.Contains(((int)gloGlobal.gloICD.CodeRevision.ICD10).ToString())))
                    {
                        _result = false;
                    }
                }
            }
            catch //(Exception ex)
            {
                _result = false;
            }
            return _result;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            //txtTOSSearch.Text = "";
            if (rbAll.Checked == true)
            {
                rbAll.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbICD9.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                rbICD10.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            else if (rbICD9.Checked == true)
            {
                rbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbAll.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                rbICD10.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD9;
            }
            else
            {
                rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbAll.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                rbICD9.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD10;

            }
            //Following code line will fire search event as well as Fill treatment also
            txtTOSSearch_TextChanged(null, null);

            if (trvTreatment != null)
            {
                if (trvTreatment.GetNodeCount(false) >= 1)
                {
                    trvTreatment.SelectedNode = trvTreatment.Nodes[0];
                }
                else
                {
                    C1TOSCPT.Rows.RemoveRange(1,  C1TOSCPT.Rows.Count -1) ;
                    C1TOSCPT.Refresh();
                }
            }
            else
            {
                C1TOSCPT.Rows.RemoveRange(1, C1TOSCPT.Rows.Count - 1);
                C1TOSCPT.Refresh();
            }
        }
    }

}