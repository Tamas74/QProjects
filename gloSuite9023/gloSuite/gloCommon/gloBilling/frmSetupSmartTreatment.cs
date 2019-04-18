using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloUIControlLibrary;

namespace gloBilling
{
    public partial class frmSetupSmartTreatment : Form
    {
        #region " Constant Declarations "
        //const int MaxCPT = 6, MaxICD9 = 4, MaxModifier = 2;
        const int MaxCPT = 100000, MaxICD9 = 100000, MaxModifier = 2;
        #endregion

        #region " Variable Declarations "
        DataTable dt = null;
        DataTable dtCpt = null;
        //DataTable dtIcd = new DataTable();
        DataTable dtModifiers = null;


        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        private Int64 nTreatID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _TreatmentName = "";
        private DataView _dv;
        gloGlobal.gloICD.CodeRevision _CodeRevision;
        string _ICDType = "ICD";
        bool _isFormLoading = false;

        private gloUIControlLibrary.ICDSubCodeControl wpfICD10UserControl = null;
        #endregion " Variable Declarations "

        #region  " C1 Grid Variable Declarations "

        private int COL_TREATID = 0;
        private int COL_TREATMENT = 1;
        private int COL_CHARGES = 2;
        private int COL_UNIT = 3;
        private int COL_TYPE = 4;

        private int COL_COUNT = 5;

        private int _Width = 0;

    //    private string _COL_TYPE_CPTHDR = "CH";
        private string _COL_TYPE_CPTITEM = "CI";
    //    private string _COL_TYPE_ICD9HDR = "IH";
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

        public frmSetupSmartTreatment(string databaseConnectionstring, Int64 TreatmentID, string Treatment)
        {
            InitializeComponent();
            nTreatID = TreatmentID;
            _TreatmentName = Treatment;
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

        public void FormDispose()
        {
            if (dt != null) { dt.Dispose(); dt = null;}
            if (dtCpt != null) { dtCpt.Dispose(); dtCpt = null; }
            if (dtModifiers != null){dtModifiers.Dispose();dtModifiers = null;}

            try
            {
                if (wpfICD10UserControl != null)
                {
                    this.wpfICD10UserControl.SearchFired -= wpfICD10UserControl_SearchFired;
                    this.wpfICD10UserControl.CodeAddedToCurrent -= wpfICD10UserControl_CodeSelectedForImport;
                    this.wpfICD10UserControl.CodeSelectedForImport -= wpfICD10UserControl_CodeSelectedForImport;

                    wpfICD10UserControl.Dispose();
                    wpfICD10UserControl = null;
                }

                if (elementHostICD10 != null)
                {
                    this.elementHostICD10.Dispose();
                    this.elementHostICD10 = null;
                }
            }
            catch (Exception ex)
            {
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
           

            
       }
        #endregion

        public delegate void CloseButtonClick(object sender, EventArgs e);
        public event CloseButtonClick CloseButton_Click;

        #region " Toolstrip Click event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            //if (txtTreatment.Text.Trim() != "")
                            //{
                            //    nTreatID = SaveSmartTreatment(txtTreatment.Text.Trim());
                            //    SaveSmartCPT_Treatment(nTreatID);
                            //    txtTreatment.Text = "";
                            //    C1TOSCPT.Clear();
                            //    designC1Grid();
                            //    FillCPTs();
                            //    FillTreatments();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Enter the Treatment Name", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}

                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "ADD":
                        {
                            txtTreatment.Text = "";
                            C1TOSCPT.Clear();
                            designC1Grid();
                            FillCPTs();
                            FillTreatments();
                        }
                        break;
                    case "Save":
                        {
                            if (SaveTreatment(txtTreatment.Text.Trim()) == true)
                            {
                                //MessageBox.Show("Treatment Saved successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtTreatment.Text = "";
                                txtTreatment.Tag = null;
                                txtTreatment.Text = "";
                                nTreatID = 0;
                                _TreatmentName = "";
                                C1TOSCPT.Clear();
                                designC1Grid();
                                FillCPTs();
                                FillTreatments();
                            }
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

        private void frmSmartTreatment_Load(object sender, EventArgs e)
        {

            //gloC1FlexStyle.Style()
            try
            {
                _isFormLoading = true;
                pnlEnterTreatment.Visible = true;
                
                //pnlTOS.Visible = true;
                FillControls();

                designC1Grid();
                FillTreatmentInGrid(nTreatID);
                txtTreatment.Text = _TreatmentName;
                txtTreatment.Tag = nTreatID;
                if (nTreatID !=0)
                    SetICDDefault(false);
                else
                    SetICDDefault();                

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
                _isFormLoading = false;
            }
        }

        #endregion

        #region " Tree View Fill Methods "

        private void FillControls()
        {
            FillTreatments();
            FillCPTs();
        }

        private void FillTreatments()
        {
            DataTable dtTreatment = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            TreeNode oNode = null;
            try
            {
                string strSQL = "SELECT nTreatmentID,sTreatmentName FROM BL_SmartTreatment";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtTreatment);
                oDB.Disconnect();


                #region " Fill Treatments on Treeview "

                //Clear all nodes first.
                trvTreatment.Nodes.Clear();

                // Add Node at Level 0.
                trvTreatment.Nodes.Add("Treatments");
                trvTreatment.Nodes[0].ImageIndex = 1;
                trvTreatment.Nodes[0].SelectedImageIndex = 1;
                //


                // GET All Type Of Services and bind to treeview
                //Pass 0 to get all Type Of Service
                //dtTreatment = oTreatment.GetTOS(0);


                //Sorting 
                _dv = dtTreatment.DefaultView;

                _dv.Sort = _dv.Table.Columns["sTreatmentName"].ColumnName;

                dtTreatment = _dv.ToTable();
                ///

                
                //Check for Table not null
                if (dtTreatment != null)
                {
                    //Check for Table not empty
                    if (dtTreatment.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTreatment.Rows.Count; i++)
                        {
                            //Create Node for each Table Item sTOSCode
                            oNode = new TreeNode();
                            oNode.Text = dtTreatment.Rows[i]["sTreatmentName"].ToString();// +" - " + dtTreatment.Rows[i]["sDescription"].ToString();
                            oNode.Tag = dtTreatment.Rows[i]["nTreatmentID"].ToString();
                            //
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;

                            //Add Node to Type Of Service Tree
                            trvTreatment.Nodes[0].Nodes.Add(oNode);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dtTreatment != null)
                {
                    dtTreatment.Dispose();
                    dtTreatment = null;
                }
                oNode = null;
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
            TreeNode tNode=null;
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
                    //if (btnCPT.Dock == DockStyle.Top)
                    if (pnl_btnCPT.Dock == DockStyle.Top)
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
                            tNode = new TreeNode();
                            tNode.Text = dt_cpt.Rows[i]["sCPTCode"].ToString() + " - " + dt_cpt.Rows[i]["sDescription"].ToString();
                            tNode.Tag = dt_cpt.Rows[i]["nCPTID"].ToString();
                            tNode.ImageIndex = 0;
                            tNode.SelectedImageIndex = 0;
                            // tNode.ImageKey = dt_cpt.Rows[i]["sCPTCode"].ToString();

                            // Add Node to CPT tree.
                            trvCPT.Nodes[0].Nodes.Add(tNode);
                            tNode = null;
                        }
                    }

                    // Show tree Expanded;
                    trvCPT.ExpandAll();
                    trvCPT.SelectedNode = trvCPT.Nodes[0];

                }
                dtCpt = dt_cpt;
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
                if (oCPT != null)
                {
                    oCPT.Dispose();
                    oCPT = null;
                }
                if (dt_cpt != null)
                {
                    dt_cpt.Dispose();
                    dt_cpt = null;
                }
                tNode = null;
            }

        }

        private void FillICD9s(string _searchtext="")
        {
            //ICD9 oICD9 = new ICD9(_databaseconnectionstring);
            BillingAssociation oICD9 = new BillingAssociation(_databaseconnectionstring);
            DataTable dtICD9 = null;
            TreeNode pnode=null;
            TreeNode tNode = null;
            try
            {
                if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD10)
                {
                    if (this.wpfICD10UserControl == null)
                    {
                        this.wpfICD10UserControl = new ICDSubCodeControl();
                        this.wpfICD10UserControl.SearchFired += new ICDSubCodeControl.searchFunctionFired(wpfICD10UserControl_SearchFired);
                        this.wpfICD10UserControl.CodeAddedToCurrent += new ICDSubCodeControl.billableCodeAddedToCurrent(wpfICD10UserControl_CodeSelectedForImport);
                        this.wpfICD10UserControl.CodeSelectedForImport +=new ICDSubCodeControl.codeImported(wpfICD10UserControl_CodeSelectedForImport);

                        this.elementHostICD10.Child = wpfICD10UserControl;
                    }

                    if (!wpfICD10UserControl.DisplayRedesignedForSmartTreatment)
                    { wpfICD10UserControl.RedesignDisplay(); }
                    wpfICD10UserControl.btnClearSearch_Click(this, new System.Windows.RoutedEventArgs());

                    dtICD9 = oICD9.GetICD10(_searchtext);

                    wpfICD10UserControl.BindTreeNodes(dtICD9);

                    elementHostICD10.Visible = true;
                    elementHostICD10.BringToFront();

                    rbDescription.Visible = false;
                    rbCode.Visible = false;

                    pnlSearch.Visible = false;
                    //pnlSelect.Visible = false;

                    
                    pnl_trvCPT.Visible = false;
                    trvCPT.SendToBack();

                    
                }
                else
                {

                    elementHostICD10.Visible = false;
                    elementHostICD10.SendToBack();

                    rbDescription.Visible = true;
                    rbCode.Visible = true;

                    pnlSearch.Visible = true;
                    //pnlSelect.Visible = true;

                    pnl_trvCPT.Visible = true;
                    trvCPT.BringToFront();


                    #region " ICD9 Tree Fill "

                    //Clear Treee Nodes
                    trvCPT.Nodes.Clear();


                    pnode = new TreeNode(_ICDType);

                    // Add Parent Node
                    pnode.ImageIndex = 3;
                    pnode.SelectedImageIndex = 3;
                    //

                    // GET All CPT and bind to treeview
                    dtICD9 = oICD9.GetICD9s(_searchtext, (int)_CodeRevision);//.GetICD9s();


                    #region " Sort Data "

                    _dv = dtICD9.DefaultView;

                    //Sort Data
                    if (rbCode.Checked == true)
                    {
                        //if (btnCPT.Dock == DockStyle.Top)
                        if (pnl_btnCPT.Dock == DockStyle.Top)
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
                                tNode = new TreeNode();
                                tNode.Text = dtICD9.Rows[i]["sICD9Code"].ToString() + " - " + dtICD9.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dtICD9.Rows[i]["nICD9ID"].ToString();

                                tNode.ImageIndex = 0;
                                tNode.SelectedImageIndex = 0;

                                pnode.Nodes.Add(tNode);
                                // Add Node to CPT tree.
                                tNode = null;
                            }
                        }

                        trvCPT.Nodes.Add(pnode);
                        // Show tree Expanded;
                        trvCPT.ExpandAll();
                        trvCPT.SelectedNode = trvCPT.Nodes[0];
                    }

                    //dtIcd = dtICD9;

                    #endregion " CPT Tree Fill "

                
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
                if (oICD9 != null)
                {
                    oICD9.Dispose();
                    oICD9 = null;
                }
                if (dtICD9 != null)
                {
                    dtICD9.Dispose();
                    dtICD9 = null;
                }
                pnode = null;
                tNode = null;
            }
        }

        void wpfICD10UserControl_CodeSelectedForImport()
        {
            gloUIControlLibrary.Classes.ICD10.clsICD10 SelectedCode = null;
            BillingAssociation oICD9 = new BillingAssociation(_databaseconnectionstring);
            DataTable dtBillableCodes = null;
            C1.Win.C1FlexGrid.Node oC1Node = null;

            string sICDCode = string.Empty;
            string sDescription = string.Empty;

            try
            {
                SelectedCode = wpfICD10UserControl.GetSelectedICDCode;

                if (SelectedCode != null && SelectedCode.ICD10Code != null && SelectedCode.ICD10Code.Trim() != "")
                {
                    dtBillableCodes = oICD9.GetBillableICD10Codes(SelectedCode.ICD10Code);

                    foreach (DataRow row in dtBillableCodes.Rows)
                    {
                        sICDCode = row["sICD10Code"].ToString();
                        sDescription = row["sDescriptionLong"].ToString();

                        if (C1TOSCPT.RowSel >= 0)
                        {
                            if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE) != null)
                            {
                                if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_CPTITEM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node;
                                }
                                else if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper() || C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD10ITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                                else if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_MODITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                            }
                        }


                        if (oC1Node != null)
                        {
                            oC1Node.Select();

                            if (ValidateModifier(sICDCode + " - " + sDescription, false) == false)
                            {
                                oC1Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, sICDCode + " - " + sDescription);
                                int _UpdateRowIndex = oC1Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;

                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_MODITM);

                                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_ICD10ITM);
                                C1TOSCPT.SetCellImage(_UpdateRowIndex, COL_TREATMENT, imgLstTOS.Images[3]);
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select ICD10 code or category to add.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString());
            }
        }

        void wpfICD10UserControl_SearchFired(string Text)
        {
            BillingAssociation oICD9 = new BillingAssociation(_databaseconnectionstring);
            DataTable dtICD9 = null;

            try
            {
                dtICD9 = oICD9.GetICD10(Text);
                this.wpfICD10UserControl.BindTreeNodes(dtICD9);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString());
            }
            finally
            {
                if (oICD9 != null)
                {
                    oICD9.Dispose();
                    oICD9 = null;
                }

                if (dtICD9 != null)
                {
                    dtICD9.Dispose();
                    dtICD9 = null;
                }
            }
        }

        private void FillModifiers()
        {
            BillingAssociation oModifier = new BillingAssociation(_databaseconnectionstring);
            DataTable dt_Mod = null;
            TreeNode tNode=null;
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
                            tNode = new TreeNode();
                            tNode.Text = dt_Mod.Rows[i]["sModifierCode"].ToString() + " - " + dt_Mod.Rows[i]["sDescription"].ToString();
                            tNode.Tag = dt_Mod.Rows[i]["nModifierID"].ToString();
                            tNode.ImageIndex = 0;
                            tNode.SelectedImageIndex = 0;
                            // tNode.ImageKey = dt_cpt.Rows[i]["sCPTCode"].ToString();

                            // Add Node to Modifier tree.
                            trvCPT.Nodes[0].Nodes.Add(tNode);
                            tNode = null;
                        }
                    }

                    // Show tree Expanded;
                    trvCPT.ExpandAll();
                    trvCPT.SelectedNode = trvCPT.Nodes[0];

                }
                dtModifiers = dt_Mod;
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
                if (oModifier != null)
                {
                    oModifier.Dispose();
                    oModifier = null;
                }
                if (dt_Mod != null)
                {
                    dt_Mod.Dispose();
                    dt_Mod = null;
                }
                tNode = null;
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
                C1TOSCPT.Cols[COL_TREATMENT].Width = (int)(_Width);
                C1TOSCPT.Cols[COL_TREATMENT].AllowEditing = false;
                C1TOSCPT.SetData(0, COL_TREATMENT, "Smart Treatment");
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

            //try
            //{
            //    if (trvTreatment.SelectedNode != trvTreatment.Nodes[0])
            //    {
            //        designC1Grid();
            //        FillTreatmentInGrid(nTreatID);
            //        txtTreatment.Text = e.Node.Text.ToString();
            //        txtTreatment.Tag = e.Node.Tag;
            //    }
            //    else
            //    {
            //        designC1Grid();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
        }

        #endregion

        #region " Method to Fill Treatment Details in Grid "

        //private void FillTreatmentInGrid(Int64 TreatmentID)
        //{
        //    DataTable dtTreatmentCPT = new DataTable();
        //    DataTable dtTreatmentICD9 = new DataTable();
        //    DataTable dtTreatmentModifier = new DataTable();

        //    string strSQL = "";
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    try
        //    {
        //        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription FROM BL_SmartTreatmentCPT WHERE nTreatmentID=" + TreatmentID + " ";
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

        //        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WHERE nTreatmentID=" + TreatmentID + "";
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

        //        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + "";
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
        //        oDB.Disconnect();


        //        if (dtTreatmentCPT.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtTreatmentCPT.Rows.Count; i++)
        //            {
        //                C1TOSCPT.Rows.Add();
        //                C1TOSCPT.Tree.Indent = 21;
        //                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
        //                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
        //                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
        //                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[0];
        //                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentCPT.Rows[i]["sCPTCode"].ToString() + " - " + dtTreatmentCPT.Rows[i]["sCPTDescription"].ToString().Replace("'", "''");
        //                int _UpdateRowIndex = C1TOSCPT.Rows.Count - 1;
        //                C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_CPTITEM);

        //                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WHERE nTreatmentID=" + TreatmentID + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";
        //                oDB.Connect(false);
        //                oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

        //                if (dtTreatmentICD9.Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < dtTreatmentICD9.Rows.Count; j++)
        //                    {
        //                        C1TOSCPT.Rows.Add();
        //                        C1TOSCPT.Tree.Indent = 21;
        //                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
        //                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
        //                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
        //                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[1];
        //                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentICD9.Rows[j]["sICD9Code"].ToString() + " - " + dtTreatmentICD9.Rows[j]["sICD9Description"].ToString();
        //                        int _UpdateRowIndex1 = C1TOSCPT.Rows.Count - 1;
        //                        C1TOSCPT.SetData(_UpdateRowIndex1, COL_TYPE, _COL_TYPE_ICD9ITM);


        //                        //strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'";
        //                        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'"
        //                               + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";

        //                        oDB.Connect(false);
        //                        oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
        //                        oDB.Disconnect();

        //                        if (dtTreatmentModifier.Rows.Count > 0)
        //                        {
        //                            for (int k = 0; k < dtTreatmentModifier.Rows.Count; k++)
        //                            {
        //                                C1TOSCPT.Rows.Add();
        //                                C1TOSCPT.Tree.Indent = 21;
        //                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
        //                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
        //                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
        //                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[2];
        //                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentModifier.Rows[k]["sModifierCode"].ToString() + " - " + dtTreatmentModifier.Rows[k]["sModifierDesc"].ToString();
        //                                int _UpdateRowIndex2 = C1TOSCPT.Rows.Count - 1;
        //                                C1TOSCPT.SetData(_UpdateRowIndex2, COL_TYPE, _COL_TYPE_MODITM);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //    }
        //}


        private void FillTreatmentInGrid(Int64 TreatmentID)
        {
            DataTable dtTreatmentCPT = null;
            DataTable dtTreatmentICD9 = null;
            DataTable dtTreatmentModifier = null;
            object obj=null;
            string strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                strSQL = "SELECT isnull(nICDRevision,9) FROM BL_SmartTreatment WHERE nTreatmentID=" + TreatmentID + " ";
                oDB.Connect(false);
                obj =oDB.ExecuteScalar_Query (strSQL);
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
                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription FROM BL_SmartTreatmentCPT WHERE nTreatmentID=" + TreatmentID + " ";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtTreatmentCPT);

                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WHERE nTreatmentID=" + TreatmentID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtTreatmentModifier);
                oDB.Disconnect();


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
                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentCPT.Rows[i]["sCPTCode"].ToString() + " - " + dtTreatmentCPT.Rows[i]["sCPTDescription"].ToString().Replace("'", "''");
                        int _UpdateRowIndex = C1TOSCPT.Rows.Count - 1;
                        C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_CPTITEM);

                        strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, dCharges, nUnits FROM BL_SmartTreatmentICD9 WHERE nTreatmentID=" + TreatmentID + " AND sCPTCode='" + dtTreatmentCPT.Rows[i]["sCPTCode"].ToString().Replace("'", "''") + "'";
                        oDB.Connect(false);
                        oDB.Retrive_Query(strSQL, out dtTreatmentICD9);

                        if (dtTreatmentICD9.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtTreatmentICD9.Rows.Count; j++)
                            {
                                C1TOSCPT.Rows.Add();
                                C1TOSCPT.Tree.Indent = 21;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                                
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

                                //strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'";
                                strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + " AND sICD9Code='" + dtTreatmentICD9.Rows[j]["sICD9Code"].ToString().Replace("'", "''") + "'"
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
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 1;
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[2];
                                        C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = dtTreatmentModifier.Rows[k]["sModifierCode"].ToString() + " - " + dtTreatmentModifier.Rows[k]["sModifierDesc"].ToString();
                                        int _UpdateRowIndex2 = C1TOSCPT.Rows.Count - 1;
                                        C1TOSCPT.SetData(_UpdateRowIndex2, COL_TYPE, _COL_TYPE_MODITM);
                                    }
                                }
                            }
                        }
                //added by mitesh (Resolved Bug 7291)
                        else
                        {
                            strSQL = "SELECT nTreatmentID, sCPTCode, sCPTDescription, sICD9Code, sICD9Description, sModifierCode, sModifierDesc FROM BL_SmartTreatmentModifier WHERE nTreatmentID=" + TreatmentID + " and isnull(sICD9Code,'') ='' " +
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                obj = null;
                strSQL = string.Empty;
                if (dtTreatmentCPT != null)
                {
                    dtTreatmentCPT.Dispose();
                    dtTreatmentCPT = null;
                }
                if (dtTreatmentICD9 != null)
                {
                    dtTreatmentICD9.Dispose();
                    dtTreatmentICD9 = null;
                }
                if (dtTreatmentModifier != null)
                {
                    dtTreatmentModifier.Dispose();
                    dtTreatmentModifier = null;
                }
            }
        }

        #endregion

        #region " Form Shown Event "

        //private void frmSmartTreatment_Shown(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //designC1Grid();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        #endregion

        #region " CPT Treeview Double Click "

        private void trvCPT_DoubleClick(object sender, EventArgs e)
        {
            //if (trvCPT.SelectedNode.Index > 0)
            //{
            AddCPTICD9();
            //}
        }

        #region " Validate CPT "

        private Boolean ValidateCPT(TreeNode oNode)
        {
            for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 0)
                {
                    Object CPT = C1TOSCPT.Rows[cntTOS].Node.Data;
                    if (oNode.Text == CPT.ToString())
                    {
                        MessageBox.Show("'" + oNode.Text + "' CPT \n already present in this Smart Treatment. Select other CPT.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CPT = null;
                        return true;
                    }
                    CPT = null;
                }
            }
            return false;
        }

        private Boolean ValidateMaxCPT()
        {
            int _CPT = 0;
            for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 0)
                {
                    _CPT = _CPT + 1;
                    if (_CPT >= MaxCPT)
                    {
                        MessageBox.Show("Maximum " + MaxCPT + " CPT allowed per smart treatment.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region " Validate ICD9 "

        private Boolean ValidateICD9(TreeNode oNode)
        {
            //C1TOSCPT.Rows[cntTOS].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
            int rowIndex = C1TOSCPT.RowSel;
            int childs = C1TOSCPT.Rows[rowIndex].Node.Children;
            for (int cntTOS = rowIndex + 1; cntTOS <= (childs + rowIndex); cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 1)
                {
                    Object ICD9 = C1TOSCPT.Rows[cntTOS].Node.Data;
                    if (oNode.Text == ICD9.ToString())
                    {
                        MessageBox.Show("'" + oNode.Text + "' " + _ICDType + " \n already present in this CPT. Select other " + _ICDType + ".", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ICD9 = null;
                        return true;
                    }
                    ICD9 = null;
                }
            }
            return false;
        }

        private Boolean ValidateMaxICD9(TreeNode oNode)
        {

            // int _ICD9 = 0;
            String sICD9 = "";
            for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 1 && (C1TOSCPT.GetData(cntTOS, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper() || C1TOSCPT.GetData(cntTOS, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD10ITM.ToUpper()))
                {
                    Object ICD9 = C1TOSCPT.Rows[cntTOS].Node.Data;
                    if (sICD9.Contains(C1TOSCPT.Rows[cntTOS].Node.Data.ToString()) == false)
                    {
                        if (sICD9 == "")
                            sICD9 = sICD9 + C1TOSCPT.Rows[cntTOS].Node.Data.ToString();
                        else
                            sICD9 = sICD9 + "~" + C1TOSCPT.Rows[cntTOS].Node.Data.ToString();
                    }
                    ICD9 = null;
                }
            }

            if (sICD9.Contains(oNode.Text.ToString()) == false)
            {
                if (sICD9 == "")
                    sICD9 = sICD9 + oNode.Text.ToString();
                else
                    sICD9 = sICD9 + "~" + oNode.Text.ToString();
            }


            String[] aICD9 = sICD9.Split('~');

            if (aICD9.Length > MaxICD9)
            {
                MessageBox.Show("Maximum " + MaxICD9 + " ICD9/10 allowed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                sICD9 = string.Empty;
                aICD9 = null;
                return true;
            }
            else
            {
                sICD9 = string.Empty;
                aICD9 = null;
                return false;
            }

        }

        #endregion

        #region " Validate Modifier "

        private Boolean ValidateModifier(string Content, bool ShowMessageBox = true)
        {
            //C1TOSCPT.Rows[cntTOS].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Select();
            int rowIndex = C1TOSCPT.RowSel;
            int childs = C1TOSCPT.Rows[rowIndex].Node.Children;
            for (int cntTOS = rowIndex + 1; cntTOS <= (childs + rowIndex); cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 1)
                {
                    Object ICD9 = C1TOSCPT.Rows[cntTOS].Node.Data;
                    if (Content == ICD9.ToString())
                    {
                        if (ShowMessageBox)
                        { MessageBox.Show("'" + Content + "' Modifier \n already present in this CPT. Select other Modifier.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }                        
                        ICD9 = null;
                        return true;
                    }
                    ICD9 = null;
                }
            }
            return false;
        }

        private Boolean ValidateMaxModifier(TreeNode oNode)
        {
            //int _Modifier = 0;
            String sModifier = "";
            for (int cntTOS = 1; cntTOS < C1TOSCPT.Rows.Count; cntTOS++)
            {
                if (C1TOSCPT.Rows[cntTOS].Node.Level == 1 && C1TOSCPT.GetData(cntTOS, COL_TYPE).ToString().ToUpper() == _COL_TYPE_MODITM.ToUpper())
                {
                    Object Modifier = C1TOSCPT.Rows[cntTOS].Node.Data;
                    if (sModifier.Contains(C1TOSCPT.Rows[cntTOS].Node.Data.ToString()) == false)
                    {
                        if (sModifier == "")
                            sModifier = sModifier + C1TOSCPT.Rows[cntTOS].Node.Data.ToString();
                        else
                            sModifier = sModifier + "~" + C1TOSCPT.Rows[cntTOS].Node.Data.ToString();
                    }
                    Modifier = null;
                }
            }

            if (sModifier.Contains(oNode.Text.ToString()) == false)
            {
                if (sModifier == "")
                    sModifier = sModifier + oNode.Text.ToString();
                else
                    sModifier = sModifier + "~" + oNode.Text.ToString();
            }


            String[] aModifier = sModifier.Split('~');

            if (aModifier.Length > MaxModifier)
            {
                MessageBox.Show("Maximum " + MaxModifier + " Modifier allowed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                sModifier = string.Empty;
                aModifier = null;
                return true;
            }
            else
            {
                sModifier = string.Empty;
                aModifier = null;
                return false;
            }
        }

        #endregion

        private void AddCPTICD9()
        {
            string _CPTName = "CPT";
            //string _ICDName = "ICD9";
            //string _ICD10Name = "ICD10";
            // string _MODName = "Modifier";
            bool _IsCPT = false;
            bool _IsICD9 = false;
            int _RowCntr = 0;
            C1.Win.C1FlexGrid.Node oC1Node = null;
            try
            {
                

                if (trvCPT.SelectedNode != null)
                {
                    if (trvCPT.SelectedNode.GetNodeCount(false) > 1)
                    {
                        if (trvCPT.SelectedNode.Text.ToUpper() == _CPTName.ToUpper())
                        {
                            _IsCPT = true;
                        }
                        else if (trvCPT.SelectedNode.Text.ToUpper() == _ICDType.ToUpper())
                        {
                            _IsICD9 = true;
                        }
                    }
                    else
                    {
                        if (trvCPT.SelectedNode.Parent != null)
                        {
                            if (trvCPT.SelectedNode.Parent.Text.ToUpper() == _CPTName.ToUpper())
                            {
                                _IsCPT = true;
                            }
                            else if ((trvCPT.SelectedNode.Parent.Text.ToUpper() == _ICDType.ToUpper()) )
                            {
                                _IsICD9 = true;
                            }
                        }
                    }
                }

                if (_IsCPT == true)
                {
                    Boolean _IsMaxCPT = false;
                    for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                    {
                        if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                        {
                            if (_IsMaxCPT == false)
                            {
                                if (ValidateMaxCPT() == true)
                                {
                                    _IsMaxCPT = true;
                                }
                                else if (ValidateCPT(trvCPT.Nodes[0].Nodes[_RowCntr]) == false)
                                {
                                    C1TOSCPT.Rows.Add();
                                    C1TOSCPT.Tree.Indent = 21;
                                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].AllowEditing = false;
                                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].IsNode = true;
                                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Level = 0;
                                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Image = imgLstTOS.Images[0];
                                    C1TOSCPT.Rows[C1TOSCPT.Rows.Count - 1].Node.Data = trvCPT.Nodes[0].Nodes[_RowCntr].Text;
                                    int _UpdateRowIndex = C1TOSCPT.Rows.Count - 1;
                                    C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_CPTITEM);
                                }
                            }
                            trvCPT.Nodes[0].Nodes[_RowCntr].Checked = false;
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
                                if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper() || C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD10ITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                                else if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_MODITM.ToUpper())
                                {
                                    oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                                }
                            }
                        }
                    }
                    if (oC1Node != null)
                    {
                        oC1Node.Select();
                        Boolean _IsMaxICD9 = false;
                        for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                        {
                            if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                            {
                                if (_IsMaxICD9 == false)
                                {
                                    if (ValidateMaxICD9(trvCPT.Nodes[0].Nodes[_RowCntr]) == true)
                                    {
                                        _IsMaxICD9 = true;
                                    }
                                    else if (ValidateICD9(trvCPT.Nodes[0].Nodes[_RowCntr]) == false)
                                    {
                                        oC1Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, trvCPT.Nodes[0].Nodes[_RowCntr].Text);
                                        int _UpdateRowIndex = oC1Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;


                                        if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                                        {
                                            C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_ICD9ITM);
                                            C1TOSCPT.SetCellImage(_UpdateRowIndex, COL_TREATMENT, imgLstTOS.Images[1]);
                                        }
                                        else
                                        {
                                            C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_ICD10ITM);
                                            C1TOSCPT.SetCellImage(_UpdateRowIndex, COL_TREATMENT, imgLstTOS.Images[3]);
                                        }
                                    }
                                }
                                trvCPT.Nodes[0].Nodes[_RowCntr].Checked = false;
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
                            if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_CPTITEM.ToUpper())
                            {
                                oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node;
                            }
                            else if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper() || C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD10ITM.ToUpper())
                            {
                                oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                            }
                            else if (C1TOSCPT.GetData(C1TOSCPT.RowSel, COL_TYPE).ToString().ToUpper() == _COL_TYPE_MODITM.ToUpper())
                            {
                                oC1Node = C1TOSCPT.Rows[C1TOSCPT.RowSel].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent);
                            }
                        }
                    }

                    if (oC1Node != null)
                    {
                        oC1Node.Select();
                        Boolean _IsMaxModifier = false;
                        for (_RowCntr = 0; _RowCntr <= trvCPT.Nodes[0].GetNodeCount(false) - 1; _RowCntr++)
                        {
                            if (trvCPT.Nodes[0].Nodes[_RowCntr].Checked == true)
                            {
                                if (_IsMaxModifier == false)
                                {
                                    if (ValidateMaxModifier(trvCPT.Nodes[0].Nodes[_RowCntr]) == true)
                                    {
                                        _IsMaxModifier = true;
                                    }
                                    else if (ValidateModifier(trvCPT.Nodes[0].Nodes[_RowCntr].Text) == false)
                                    {
                                        oC1Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, trvCPT.Nodes[0].Nodes[_RowCntr].Text);
                                        int _UpdateRowIndex = oC1Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                        C1TOSCPT.SetData(_UpdateRowIndex, COL_TYPE, _COL_TYPE_MODITM);
                                        C1TOSCPT.SetCellImage(_UpdateRowIndex, COL_TREATMENT, imgLstTOS.Images[2]);
                                    }
                                }
                                trvCPT.Nodes[0].Nodes[_RowCntr].Checked = false;
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
                _CPTName = string.Empty;
                oC1Node = null;
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
                    MessageBox.Show("Select a type of service to associate.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                elementHostICD10.Visible = false;
                elementHostICD10.SendToBack();

                rbDescription.Visible = true;
                rbCode.Visible = true;

                pnlSearch.Visible = true;
                //pnlSelect.Visible = true;

                pnl_trvCPT.Visible = true;
                trvCPT.BringToFront();

                pnl_btnICD9.Dock = DockStyle.Bottom;
                pnl_btnCPT.Dock = DockStyle.Top;
                pnl_btnModifier.Dock = DockStyle.Bottom;
                rbCPT.Checked = true;
                rbModifier.Checked = false;

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

                btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                btnCPT.BackgroundImageLayout = ImageLayout.Stretch;
                btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnICD9.BackgroundImageLayout = ImageLayout.Stretch;
                btnModifier.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnModifier.BackgroundImageLayout = ImageLayout.Stretch;
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

                //rbICD9.Checked = true;
                rbModifier.Checked = false;
                FillICD9s();
                //first check if nodes are added to the grid
                if (C1TOSCPT.Rows.Count != 1)
                {
                    if (C1TOSCPT.Rows[C1TOSCPT.Row].Node != null)
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
                }


                //deselect the check box
                chkBoxSelect.Checked = false;
                btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                btnICD9.BackgroundImageLayout = ImageLayout.Stretch;

                btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnCPT.BackgroundImageLayout = ImageLayout.Stretch;
                btnModifier.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnModifier.BackgroundImageLayout = ImageLayout.Stretch;
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

                elementHostICD10.Visible = false;
                elementHostICD10.SendToBack();

                rbDescription.Visible = true;
                rbCode.Visible = true;

                pnlSearch.Visible = true;
                //pnlSelect.Visible = true;

                pnl_trvCPT.Visible = true;
                

                pnl_btnCPT.Dock = DockStyle.Bottom;
                pnl_btnICD9.Dock = DockStyle.Bottom;
                pnl_btnModifier.Dock = DockStyle.Top;
                trvCPT.BringToFront();

                rbModifier.Checked = true;

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
                btnModifier.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                btnModifier.BackgroundImageLayout = ImageLayout.Stretch;
                btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnCPT.BackgroundImageLayout = ImageLayout.Stretch;
                btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                btnICD9.BackgroundImageLayout = ImageLayout.Stretch;
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
            if (pnl_btnICD9.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnICD9_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnICD9.Dock != DockStyle.Top)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnCPT_MouseHover(object sender, EventArgs e)
        {
            if (rbCPT.Checked == false)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnCPT_MouseLeave(object sender, EventArgs e)
        {
            if (rbCPT.Checked == false)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnModifier_MouseHover(object sender, EventArgs e)
        {
            if (rbModifier.Checked == false)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btnModifier_MouseLeave(object sender, EventArgs e)
        {
            if (rbModifier.Checked == false)
            {
                ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        #endregion

        #region " CPT and ICD9 Remove Event "

        private void C1TOSCPT_MouseDown(object sender, MouseEventArgs e)
        {
            // Select the Current Row
            if (C1TOSCPT.HitTest(e.X, e.Y).Row > 0)
            {
                C1TOSCPT.Rows[C1TOSCPT.HitTest(e.X, e.Y).Row].Node.Select();

                // If Right Click Then show the menu with cursor location.
                if (e.Button.ToString() == "Right")
                {
                    cxtMS.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check type of Deletion selected
            try
            {


                if (C1TOSCPT.Rows[C1TOSCPT.Row].Node.Level == 0)
                {
                    // Remove all  ICD9's  and Modifiers associated with this CPT 
                    if (MessageBox.Show("Are you sure you want to remove this CPT and its associate?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    if (MessageBox.Show("Are you sure you want to remove all associated " + nodeTitle + ".  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        #region "  After Select CPT Treeview Event"
        bool _isSelectClicked = false;

        private void trvCPT_AfterSelect(object sender, TreeViewEventArgs e)
        {

       
        }
        private void trvCPT_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (_isSelectClicked == false)
                {
                    _isSelectClicked = true;

                    if (e.Node.Level == 0)
                    {
                        SelectAllChilds();
                    }
                    else
                    {
                        SelectedAllChilds();
                    }

                    _isSelectClicked = false;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void SelectAllChilds()
        {
            try
            {
                bool _SetAllChilds = false;
                if (trvCPT.Nodes[0].Nodes.Count > 0)
                {
                    _SetAllChilds = trvCPT.Nodes[0].Checked;
                    for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
                    {
                        trvCPT.Nodes[0].Nodes[i].Checked = _SetAllChilds;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }

        private void SelectedAllChilds()
        {
            try
            {
                bool _AllChilds = true;
                for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
                {
                    if (!trvCPT.Nodes[0].Nodes[i].Checked)
                    {
                        _AllChilds = false;
                        break;
                    }
                }
                trvCPT.Nodes[0].Checked = _AllChilds;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void trvCPT_Click(object sender, EventArgs e)
        {
            //if (trvCPT.SelectedNode.Text == "CPT")
            //{
            //    if (trvCPT.Nodes[0].Nodes.Count > 0)
            //    {
            //        if (trvCPT.Nodes[0].Checked == true)
            //        {
            //            for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
            //            {
            //                trvCPT.Nodes[0].Nodes[i].Checked = true;
            //            }
            //            trvCPT.Nodes[0].Checked = true;
            //        }
            //        else
            //        {
            //            for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
            //            {
            //                trvCPT.Nodes[0].Nodes[i].Checked = false;
            //            }
            //            trvCPT.Nodes[0].Checked = false;
            //        }
            //    }
            //    else
            //    {
            //        //chkBoxSelect.Checked = false;
            //    }

            //}

            //if (trvCPT.Nodes[0].Nodes.Count > 0)
            //{
            //    if (trvCPT.TopNode.Checked == true)
            //    {
            //        for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
            //        {
            //            trvCPT.Nodes[0].Nodes[i].Checked = true;
            //        }
            //        trvCPT.Nodes[0].Checked = true;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < trvCPT.Nodes[0].Nodes.Count; i++)
            //        {
            //            trvCPT.Nodes[0].Nodes[i].Checked = false;
            //        }
            //        trvCPT.Nodes[0].Checked = false;
            //    }
            //}
            //else
            //{
            //    //chkBoxSelect.Checked = false;
            //}



        }

        #endregion

        #region " Checkbox Select Event "

        private void chkBoxSelect_Click(object sender, EventArgs e)
        {

            if (trvCPT.Nodes[0].Nodes.Count > 0)
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
            else
            {
                chkBoxSelect.Checked = false;
            }
        }

        #endregion

        #region " Search Funtionality - Text Change Events "

        private void txtCPTSearch_TextChanged(object sender, EventArgs e)
        {
            // bool pending = false;
            //DataTable dt = new DataTable();
            //BillingAssociation oAssociation = new BillingAssociation(_databaseconnectionstring);
            //ICD9 oICD9 = new ICD9(_databaseconnectionstring);
            chkBoxSelect.Checked = false;
            try
            {
                string strSearch = "";

                if (txtCPTSearch.Text != "")
                {
                    strSearch = Convert.ToString(txtCPTSearch.Text.Trim());
                    //strSearch = txtCPTSearch.Text.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%").Replace("^", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace("]", "").Replace("_","");
                    strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                    //strSearch = strSearch.Replace("[", "") + "";
                }
                else
                {
                    strSearch = "";
                }
                //*********************************************************************
                //Added By Debasish Das on 15th March 2010
                // this Search is taking long time hence system is getting loacked
                //*********************************************************************               
                //if (!backgroundSearch.IsBusy)
                //{
                // backgroundSearch.RunWorkerAsync(strSearch);
                //}
                //else
                //{
                // Application.DoEvents();

                //}
                //******************** Ends Here **************************************

                //*********************************************************************
                //Added By Roopali on 3rd May 2010
                //Logic change to solve,Search is taking long time hence system is getting loacked
                //As above logic skips search creteria. 
                //*********************************************************************              

                Fill(strSearch);
                fillFilteredResult();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //private void Fill(string strSearch)
        //{

        //    BillingAssociation oAssociation = new BillingAssociation(_databaseconnectionstring);
        //    try
        //    {
        //        #region " CPT Filter "

        //        if (rbCPT.Checked == true)
        //        {
        //            dt = oAssociation.GetCPTs();
        //            _dv = dt.DefaultView;

        //            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sCPTCode"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //            }
        //            else
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sCPTCode"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //            }
        //        }

        //        #endregion " CPT Filter "

        //        #region " ICD9 Filter "

        //        if (rbICD9.Checked == true)
        //        {
        //            dt = oAssociation.GetICD9s();
        //            _dv = dt.DefaultView;


        //            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //            }
        //            else
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //            }

        //        }

        //        #endregion " ICD9 Filter "

        //        #region " Modifier Filter "

        //        if (rbModifier.Checked == true)
        //        {
        //            dt = oAssociation.GetModifiers();
        //            _dv = dt.DefaultView;


        //            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sModifierCode"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
        //                }
        //            }
        //            else
        //            {
        //                if (rbCode.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sModifierCode"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //                if (rbDescription.Checked == true)
        //                {
        //                    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
        //                }
        //            }

        //        }

        //        #endregion " Modifier Filter "

        //        #region " Sort Filtered Data "

        //        //Sort Data
        //        if (rbCode.Checked == true)
        //        {
        //            //if (btnCPT.Dock == DockStyle.Top)
        //            if (pnl_btnCPT.Dock == DockStyle.Top)
        //            {
        //                _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
        //            }
        //            if (pnl_btnICD9.Dock == DockStyle.Top)
        //            {
        //                _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
        //            }
        //            if (pnl_btnModifier.Dock == DockStyle.Top)
        //            {
        //                _dv.Sort = _dv.Table.Columns["sModifierCode"].ColumnName;
        //            }
        //        }
        //        else
        //            _dv.Sort = _dv.Table.Columns["sDescription"].ColumnName;

        //        //

        //        #endregion " Sort Filtered Data "

        //        // Get only the filtered row/row's
        //        dt = _dv.ToTable();
        //        //


        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        _dv.Dispose();
        //    }
        //}


        private void Fill(string strSearch)
        {

            try
            {
                #region " CPT Filter "

                if (rbCPT.Checked == true)
                {
                    dt = new DataTable();
                    _dv = new DataView();
                    //dt = dtCpt;
                    dt = dtCpt.Copy();
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
                    FillICD9s(strSearch);
                    //dt = new DataTable();
                    //_dv = new DataView();
                    //dt = dtIcd.Copy();
                    //_dv = dt.DefaultView;


                    //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                    //{
                    //    if (rbCode.Checked == true)
                    //    {
                    //        _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '%" + strSearch + "%'";
                    //    }
                    //    if (rbDescription.Checked == true)
                    //    {
                    //        _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '%" + strSearch + "%'";
                    //    }
                    //}
                    //else
                    //{
                    //    if (rbCode.Checked == true)
                    //    {
                    //        _dv.RowFilter = _dv.Table.Columns["sICD9Code"].ColumnName + " Like '" + strSearch + "%'";
                    //    }
                    //    if (rbDescription.Checked == true)
                    //    {
                    //        _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                    //    }
                    //}

                }

                #endregion " ICD9 Filter "

                #region " Modifier Filter "

                if (rbModifier.Checked == true)
                {
                    dt = new DataTable();
                    _dv = new DataView();
                    dt = dtModifiers.Copy();
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
                    //if (btnCPT.Dock == DockStyle.Top)
                    if (pnl_btnCPT.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sCPTCode"].ColumnName;
                    }
                    if (pnl_btnICD9.Dock == DockStyle.Top)
                    {
                        _dv.Sort = _dv.Table.Columns["sICD9Code"].ColumnName;
                    }
                    if (pnl_btnModifier.Dock == DockStyle.Top)
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


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dv != null)
                {
                    _dv.Dispose();
                    _dv = null;
                }
                
            }
        }


        private void txtTOSSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTreatment=null;
            TreeNode oNode=null;
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

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                string strSQL = "SELECT nTreatmentID,sTreatmentName FROM BL_SmartTreatment";
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtTreatment);
                oDB.Disconnect();

                #region " Filter And Fill Treatments in Treeview "

                //Clear all nodes first.
                trvTreatment.Nodes.Clear();

                // Add Node at Level 0.
                trvTreatment.Nodes.Add("Treatments");
                trvTreatment.Nodes[0].ImageIndex = 1;
                trvTreatment.Nodes[0].SelectedImageIndex = 1;
                //
                if (dtTreatment != null)
                {
                    //Check for Table not empty
                    if (dtTreatment.Rows.Count > 0)
                    {
                        _dv = dtTreatment.DefaultView;

                        if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                        {
                            _dv.RowFilter = _dv.Table.Columns["sTreatmentName"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                        else
                        {
                            _dv.RowFilter = _dv.Table.Columns["sTreatmentName"].ColumnName + " Like '%" + strSearch + "%'";
                        }
                        //Sorting 
                        _dv.Sort = _dv.Table.Columns["sTreatmentName"].ColumnName;

                        dtTreatment = _dv.ToTable();

                        //Check for Table not null

                        for (int i = 0; i < dtTreatment.Rows.Count; i++)
                        {
                            //Create Node for each Table Item sTreatmentName
                            oNode = new TreeNode();
                            oNode.Text = dtTreatment.Rows[i]["sTreatmentName"].ToString();
                            oNode.Tag = dtTreatment.Rows[i]["nTreatmentID"].ToString();
                            //
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;

                            //Add Node to Treatment
                            trvTreatment.Nodes[0].Nodes.Add(oNode);
                            //

                            oNode = null;

                        }//for (int i = 0; i < dtTreatment.Rows.Count ; i++)

                    }//if (dtTreatment.Rows.Count > 0)

                    trvTreatment.ExpandAll();

                } //if (dtTreatment!= null) 

                #endregion " Treatment Tree Fill "

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
                oNode=null;
            }
        }


        #endregion

        #region " RadioButton Check Event "

        private void rbCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCode.Checked == true)
            {
                rbCode.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbDescription.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            else
            {
                rbDescription.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbCode.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }



            if (pnl_btnCPT.Dock == DockStyle.Top)
            {
                FillCPTs();
            }
            else if (pnl_btnICD9.Dock == DockStyle.Top)
            {
                FillICD9s();
            }
            else if (pnl_btnModifier.Dock == DockStyle.Top)
            {
                FillModifiers();
            }
        }



        #endregion

        #region " Save Smart Treatments"

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
                            DataTable _dt = null;

                            Int64 tempCPTID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TREATID));
                            oCPT.CPTID = tempCPTID;
                            _dt = oCPT.getCPT();
                            if (_dt != null)
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 1; // Set subitem id to 1 to Identify CPT
                                oSubItem.Code = _dt.Rows[0][0].ToString();
                                oSubItem.Description = _dt.Rows[0][1].ToString();

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                _dt.Dispose();
                                _dt = null;
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
                            DataTable _dt = null;

                            Int64 tempICD9ID = Convert.ToInt64(C1TOSCPT.GetData(rowIndex + i, COL_TREATID));
                            _dt = oICD9.GetICD9(tempICD9ID);
                            if (_dt != null)
                            {
                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();

                                oSubItem.ID = 2; //set subitem id to  2 to identify ICD9
                                oSubItem.Code = _dt.Rows[0][0].ToString();
                                oSubItem.Description = _dt.Rows[0][1].ToString();

                                oItem.SubItems.Add(oSubItem);

                                oSubItem.Dispose();
                                _dt.Dispose();
                                _dt = null;
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
                if (oItem != null)
                {
                    oItem.Dispose();
                    oItem = null;
                }
                if (oCPT != null)
                {
                    oCPT.Dispose();
                    oCPT = null;
                }
                if (oICD9 != null)
                {
                    oICD9.Dispose();
                    oICD9 = null;
                }
                //if (_dt != null)
                //{
                //    _dt.Dispose();
                //    _dt = null;
                //}
            }
        }

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
                strSQL = "SELECT Max(nTreatmentID) FROM BL_SmartTreatment";
                ID = oDB.ExecuteScalar_Query(strSQL);
                if (ID.ToString() == "")
                {
                    ID = 0;
                }
                TreatID = Convert.ToInt64(ID) + 1;
                if (TreatmentName != "")
                {
                    strSQL = "INSERT INTO BL_SmartTreatment (nTreatmentID,sTreatmentName)VALUES(" + TreatID + ",'" + TreatmentName.Replace("'", "''").Trim() + "')";
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                ID = null;
            }

        }

        private void SaveSmartCPT_Treatment(Int64 TreatID)
        {
            gloGeneralItem.gloItem oItem = null;
            //gloGeneralItem.gloSubItems oSubItems;
            gloGeneralItem.gloSubItem oSubItem = null;

            gloGeneralItem.gloItem oModItem = null;
            gloGeneralItem.gloSubItem oModSubItem = null;
            C1.Win.C1FlexGrid.Node oMODNode = null;
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
                                
                                C1.Win.C1FlexGrid.CellRange oModRange = oChildNode.GetCellRange();
                                oChildNode = null;
                                for (int k = oModRange.TopRow + 1; k <= oModRange.BottomRow; k++)
                                {
                                    oModSubItem = new gloGeneralItem.gloSubItem();
                                    oMODNode = C1TOSCPT.Rows[k].Node;

                                    Object Modifier = oMODNode.Data;
                                    oModSubItem.Code = Modifier.ToString();
                                    Modifier = null;
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
                                    strSQL = "INSERT INTO BL_SmartTreatmentCPT (nTreatmentID,sCPTCode,sCPTDescription) VALUES  ( " + TreatID + ",'" + sCPT[0].ToString().Trim().Replace("'", "''") + "' ,'" + sCPT[1].ToString().Trim().Replace("'", "''") + "' )";
                                    oDB.Connect(false);
                                    _result = oDB.Execute_Query(strSQL);
                                    oDB.Disconnect();
                                }
                                if (oItem.SubItems.Count > 0)
                                {
                                    string _ICD9Code = "";
                                    string _ICD9Description = "";
                                    for (int i = 0; i < oItem.SubItems.Count; i++)
                                    {
                                        string[] sICD9 = null;
                                        string[] sMOD = null;
                                        sICD9 = oItem.SubItems[i].Code.Split('-');
                                        sMOD = sICD9[0].ToString().Split('.');
                                        if (sMOD[0].Trim().Length > 2)
                                        {
                                            _ICD9Code = sICD9[0].Trim();
                                            _ICD9Description = sICD9[1].Trim();
                                            if (sICD9.Length > 2)
                                            {
                                                _ICD9Description = sICD9[1].Trim() + "-" + sICD9[2].Trim();
                                            }
                                            strSQL = "INSERT INTO BL_SmartTreatmentICD9 (nTreatmentID ,sCPTCode,sCPTDescription  ,sICD9Code ,sICD9Description,dCharges ,nUnits) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim().Replace("'", "''") + "', '" + sCPT[1].ToString().Trim().Replace("'", "''") + "', '" + _ICD9Code.Replace("'", "''") + "','" + _ICD9Description.Replace("'", "''") + "',0,0 )";
                                            oDB.Connect(false);
                                            _result = oDB.Execute_Query(strSQL);
                                            oDB.Disconnect();
                                        }
                                        else
                                        {
                                            strSQL = "INSERT INTO BL_SmartTreatmentModifier (nTreatmentID,sCPTCode,sCPTDescription,sICD9Code,sICD9Description,sModifierCode,sModifierDesc) VALUES(" + TreatID + ", '" + sCPT[0].ToString().Trim().Replace("'", "''") + "', '" + sCPT[1].ToString().Trim().Replace("'", "''") + "', '" + _ICD9Code.Replace("'", "''").Replace("'", "''") + "','" + _ICD9Description.Replace("'", "''") + "','" + sICD9[0].ToString().Trim().Replace("'", "''") + "','" + sICD9[1].ToString().Trim().Replace("'", "''") + "')";
                                            oDB.Connect(false);
                                            _result = oDB.Execute_Query(strSQL);
                                            oDB.Disconnect();
                                        }

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
                            oItem.SubItems.Dispose();
                            oItem.Dispose();
                            oItem = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not add smart treatment.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oItem != null)
                {
                    oItem.Dispose();
                    oItem = null;
                }
                if (oSubItem != null)
                {
                    
                    oSubItem.Dispose();
                    oSubItem = null;
                }
                if (oModItem != null)
                {
                    oModItem.Dispose();
                    oModItem = null;
                }
                if (oModSubItem != null)
                {

                    oModSubItem.Dispose();
                    oModSubItem = null;
                }
                oMODNode = null;
            }

        }

        private bool IsExistsTreatment(Int64 TreatmentId, string TreatmentName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object retValue = new object();
            bool _IsExits = false;

            try
            {
                oDB.Connect(false);
                if (TreatmentId > 0)
                {
                    _sqlQuery = " SELECT ISNULL(COUNT(nTreatmentID),0) FROM BL_SmartTreatment " +
                                " WHERE UPPER(sTreatmentName) = '" + TreatmentName.ToUpper().Replace("'", "''").Trim() + "' AND nTreatmentID <> " + TreatmentId + "";
                }
                else
                {
                    _sqlQuery = " SELECT ISNULL(COUNT(nTreatmentID),0) FROM BL_SmartTreatment " +
                                " WHERE UPPER(sTreatmentName) = '" + TreatmentName.ToUpper().Replace("'", "''").Trim() + "'";
                }
                retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retValue != null && retValue != DBNull.Value && Convert.ToInt64(retValue) > 0)
                {
                    _IsExits = true;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (retValue != null) { retValue = null; }
            }
            return _IsExits;
        }

        private bool SaveTreatment(string TreatmentName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retValue = null;
            string _sqlQuery = "";
            Int64 _TreatmentId = 0;
            bool _IsSaved = false;
            int _ret = 0;

            try
            {
                oDB.Connect(false);

                if (ValidateSave() == true)
                {
                    if (txtTreatment.Tag != null && Convert.ToInt64(txtTreatment.Tag) > 0)
                    {
                        //For Edit
                        _TreatmentId = Convert.ToInt64(txtTreatment.Tag);
                        _sqlQuery = "Update BL_SmartTreatment set sTreatmentName='" + txtTreatment.Text.Replace("'", "''").Trim() + "',nICDRevision=" + (int)_CodeRevision + " where nTreatmentID = " + _TreatmentId + "";
                        oDB.Execute_Query(_sqlQuery);
                        //1.Delete existing association for the Treatment
                        _sqlQuery = "DELETE FROM BL_SmartTreatmentCPT WHERE nTreatmentID = " + _TreatmentId + " ";
                        oDB.Execute_Query(_sqlQuery);
                        _sqlQuery = "DELETE FROM BL_SmartTreatmentICD9 WHERE nTreatmentID = " + _TreatmentId + " ";
                        oDB.Execute_Query(_sqlQuery);
                        _sqlQuery = "DELETE FROM BL_SmartTreatmentModifier WHERE nTreatmentID = " + _TreatmentId + " ";
                        oDB.Execute_Query(_sqlQuery);

                        SaveSmartCPT_Treatment(_TreatmentId);
                        _IsSaved = true;

                    }
                    else
                    {
                        //For New Save

                        //1. Save the Treatment Name in BL_SmartTreatment
                        _sqlQuery = "SELECT ISNULL(MAX(nTreatmentID),0) + 1 FROM BL_SmartTreatment";
                        retValue = new object();
                        retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                        if (retValue != null && retValue != DBNull.Value && Convert.ToInt64(retValue) > 0)
                        {
                            _TreatmentId = Convert.ToInt64(retValue);
                            _sqlQuery = "INSERT INTO BL_SmartTreatment(nTreatmentID, sTreatmentName,nICDRevision) VALUES (" + _TreatmentId + ",'" + TreatmentName.Replace("'", "''").Trim() + "'," + (int) _CodeRevision  + ")";
                            _ret = oDB.Execute_Query(_sqlQuery);

                            if (_ret > 0)
                            {
                                SaveSmartCPT_Treatment(_TreatmentId);
                                _IsSaved = true;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                retValue = null;
            }
            return _IsSaved;
        }

        private bool ValidateSave()
        {
            bool _retValue = true;

            try
            {
                if (txtTreatment.Text.Trim() == "")
                {
                    MessageBox.Show("Enter the smart treatment name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTreatment.Focus();
                    _retValue = false;
                }
                else if (txtTreatment.Text.Trim() != "")
                {
                    if (IsExistsTreatment(Convert.ToInt64(txtTreatment.Tag), txtTreatment.Text.Trim()) == true)
                    {
                        MessageBox.Show("Smart Treatment name is already in use by another entry. Select a unique name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTreatment.Focus();
                        txtTreatment.SelectAll();
                        _retValue = false;
                    }
                    else if (C1TOSCPT.Rows.Count <= 1)
                    {
                        MessageBox.Show("Enter the associated CPT's,ICD9/10 or modifiers.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _retValue = false;
                    }
                    if (ValidateICD9AndICD10() == false)
                    {
                        MessageBox.Show("ICD Type Mismatch.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                        //    MessageBox.Show("ICD 9 codes cannot be mixed with ICD 10 codes.\nPlease remove ICD 10 codes before saving.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //else
                        //    MessageBox.Show("ICD 9 codes cannot be mixed with ICD 10 codes.\nPlease remove ICD 9 codes before saving.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _retValue = false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }

            return _retValue;
        }

        #endregion

        private void tsb_New_Click(object sender, EventArgs e)
        {
            try
            {
                designC1Grid();
                txtTreatment.Text = "";
                txtTreatment.Tag = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (SaveTreatment(txtTreatment.Text.Trim()) == true)
            {
                //MessageBox.Show("Treatment Saved successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                designC1Grid();
                txtTreatment.Text = "";
                txtTreatment.Tag = null;
                //FillCPTs();
                //FillTreatments();
                this.Close();
            }
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            //int _result = 0;
            object ID = null;
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this smart treatment?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (trvTreatment.SelectedNode != null)
                    {
                        if (trvTreatment.SelectedNode.Text.Trim() != "")
                        {
                            oDB.Connect(false);
                            strSQL = "Delete FROM BL_SmartTreatment Where (nTreatmentID=" + Convert.ToInt64(txtTreatment.Tag) + ") AND sTreatmentName='" + txtTreatment.Text + "'";
                            ID = oDB.ExecuteScalar_Query(strSQL);
                            if (ID.ToString() == "")
                            {
                                ID = 0;
                            }
                            designC1Grid();
                            FillTreatments();
                            txtTreatment.Text = "";
                            txtTreatment.Tag = null;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                ID = null;
            }
        }

        private void frmSetupSmartTreatment_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                CloseButton_Click(null, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        //private void backgroundSearch_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        String sSearchString = e.Argument.ToString();
        //        Fill(sSearchString, true);
        //    }
        //    catch
        //    {

        //    }
        //}

        private void fillFilteredResult()
        {
            TreeNode tNode=null;
            try
            {
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
                        if (dt.Rows.Count > 0 && dt.Columns.Contains("sCPTCode") && dt.Columns.Contains("sDescription") && dt.Columns.Contains("nCPTID"))
                        {

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                // create and set value to CPTs Nodes.
                                tNode = new TreeNode();

                                tNode.Text = dt.Rows[i]["sCPTCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dt.Rows[i]["nCPTID"].ToString();

                                //
                                tNode.ImageIndex = 0;
                                tNode.SelectedImageIndex = 0;
                                //

                                // Add Node to CPT tree.
                                trvCPT.Nodes[0].Nodes.Add(tNode);
                                tNode = null;
                            }
                        }

                    }
                }
                if (pnl_btnICD9.Dock == DockStyle.Top && dt.Columns.Contains("sICD9Code") && dt.Columns.Contains("sDescription") && dt.Columns.Contains("nICD9ID"))
                {
                    //trvCPT.Nodes.Add("ICD9");
                    TreeNode pnode;
                    pnode = new TreeNode(_ICDType);
                    //
                    pnode.ImageIndex = 3;
                    pnode.SelectedImageIndex = 3;
                    //
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                // create and set value to CPTs Nodes.
                                tNode = new TreeNode();
                                tNode.Text = dt.Rows[i]["sICD9Code"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
                                tNode.Tag = dt.Rows[i]["nICD9ID"].ToString();
                                //
                                tNode.ImageIndex = 0;
                                tNode.SelectedImageIndex = 0;
                                //
                                // Add Node to CPT tree.
                                pnode.Nodes.Add(tNode);
                            }
                            trvCPT.Nodes.Add(pnode);
                        }

                    }
                }

                if (pnl_btnModifier.Dock == DockStyle.Top && dt.Columns.Contains("sModifierCode") && dt.Columns.Contains("sDescription") && dt.Columns.Contains("nModifierID"))
                {
                    trvCPT.Nodes.Add("Modifier");
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
                                tNode = new TreeNode();

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
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                tNode = null;
            }
        }


        private void frmSetupSmartTreatment_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        
        private void rbICD9_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD9.Checked == true)
            {
                rbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD9;
                _ICDType = "ICD9";
            }
            else
            {
                rbICD9.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                _CodeRevision = gloGlobal.gloICD.CodeRevision.ICD10;
                _ICDType = "ICD10";
            }
            btnICD9.Text = _ICDType;
            txtCPTSearch.Text = "";
            if (_isFormLoading == false)
                btnICD9_Click(null, null);
        }

        private void SetICDDefault(bool _ISDefault = true)
        {
            gloBilling ogloBilling = null;
            long _DOS;
            try
            {
                if (_ISDefault)
                {
                    _DOS = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                    ogloBilling = new gloBilling(_databaseconnectionstring, "");
                    _CodeRevision = ogloBilling.GetICDCodeType(0, _DOS);
                }
                                
                if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                {
                    rbICD9.Checked = true;
                    _ICDType = "ICD9";
                }
                else
                {
                    rbICD10.Checked = true;
                    _ICDType = "ICD10";
                }
                btnICD9.Text = _ICDType;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }
        private bool ValidateICD9AndICD10()
        {
            bool _reslt = true ;
            C1.Win.C1FlexGrid.Node oParentNode = null;
            
            try
            {
                if(C1TOSCPT !=null)
                    if (C1TOSCPT.Rows.Count > 1)
                    {
                        //for (int i = 1; i <= C1TOSCPT.Rows.Count; i++)
                        for (int i = 1; i < C1TOSCPT.Rows.Count; i++) //SLR: 10/21/2016 count can not reach to last:
                        {
                            oParentNode = C1TOSCPT.Rows[i].Node;
                           
                            if (oParentNode.Level == 0)
                            {
                               C1.Win.C1FlexGrid.CellRange cellRng = oParentNode.GetCellRange();
                               
                                for (int j = cellRng.TopRow + 1; j <= cellRng.BottomRow; j++)
                                {
                                    if (C1TOSCPT.GetData(j, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD9ITM.ToUpper() || C1TOSCPT.GetData(j, COL_TYPE).ToString().ToUpper() == _COL_TYPE_ICD10ITM.ToUpper())
                                    {
                                        if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                                        {
                                            if (C1TOSCPT.GetData(j, COL_TYPE).ToString().ToUpper() != _COL_TYPE_ICD9ITM.ToUpper())
                                            {
                                                _reslt = false;
                                                break;
                                            }
                                        }
                                        else if (_CodeRevision == gloGlobal.gloICD.CodeRevision.ICD10)
                                        {
                                            if (C1TOSCPT.GetData(j, COL_TYPE).ToString().ToUpper() != _COL_TYPE_ICD10ITM.ToUpper())
                                            {
                                                _reslt = false;
                                                break;
                                            }
                                        }
                                    } 
                                }
                            }
                            oParentNode = null;
                        }
                    }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oParentNode = null;
            }
            return _reslt;
        }
        //    private void backgroundSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //    {
        //        try
        //        {
        //            #region "  Tree Fill "

        //            //Clear Treee Nodes
        //            trvCPT.Nodes.Clear();

        //            // Add Parent Node
        //            if (pnl_btnCPT.Dock == DockStyle.Top)
        //            {
        //                trvCPT.Nodes.Add("CPT");
        //                //
        //                trvCPT.Nodes[0].ImageIndex = 2;
        //                trvCPT.Nodes[0].SelectedImageIndex = 2;
        //                //

        //                if (dt != null)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                        {
        //                            // create and set value to CPTs Nodes.
        //                            TreeNode tNode = new TreeNode();

        //                            tNode.Text = dt.Rows[i]["sCPTCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
        //                            tNode.Tag = dt.Rows[i]["nCPTID"].ToString();

        //                            //
        //                            tNode.ImageIndex = 0;
        //                            tNode.SelectedImageIndex = 0;
        //                            //

        //                            // Add Node to CPT tree.
        //                            trvCPT.Nodes[0].Nodes.Add(tNode);
        //                        }
        //                    }

        //                }
        //            }
        //            if (pnl_btnICD9.Dock == DockStyle.Top)
        //            {
        //                trvCPT.Nodes.Add("ICD9");
        //                //
        //                trvCPT.Nodes[0].ImageIndex = 3;
        //                trvCPT.Nodes[0].SelectedImageIndex = 3;
        //                //

        //                if (dt != null)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                        {
        //                            // create and set value to CPTs Nodes.
        //                            TreeNode tNode = new TreeNode();
        //                            tNode.Text = dt.Rows[i]["sICD9Code"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
        //                            tNode.Tag = dt.Rows[i]["nICD9ID"].ToString();
        //                            //
        //                            tNode.ImageIndex = 0;
        //                            tNode.SelectedImageIndex = 0;
        //                            //
        //                            // Add Node to CPT tree.
        //                            trvCPT.Nodes[0].Nodes.Add(tNode);
        //                        }
        //                    }

        //                }
        //            }

        //            if (pnl_btnModifier.Dock == DockStyle.Top)
        //            {
        //                trvCPT.Nodes.Add("Modifier");
        //                //
        //                trvCPT.Nodes[0].ImageIndex = 2;
        //                trvCPT.Nodes[0].SelectedImageIndex = 2;
        //                //

        //                if (dt != null)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                        {
        //                            // create and set value to CPTs Nodes.
        //                            TreeNode tNode = new TreeNode();

        //                            tNode.Text = dt.Rows[i]["sModifierCode"].ToString() + " - " + dt.Rows[i]["sDescription"].ToString();
        //                            tNode.Tag = dt.Rows[i]["nModifierID"].ToString();

        //                            //
        //                            tNode.ImageIndex = 0;
        //                            tNode.SelectedImageIndex = 0;
        //                            //

        //                            // Add Node to CPT tree.
        //                            trvCPT.Nodes[0].Nodes.Add(tNode);
        //                        }
        //                    }

        //                }
        //            }
        //            //

        //            // Show tree Expanded;
        //            trvCPT.ExpandAll();
        //            trvCPT.SelectedNode = trvCPT.Nodes[0];



        //            #endregion " Tree Fill "
        //        }
        //        catch (Exception Ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
        //            Ex = null; 
        //        }
        //    }


        //}

    }
}