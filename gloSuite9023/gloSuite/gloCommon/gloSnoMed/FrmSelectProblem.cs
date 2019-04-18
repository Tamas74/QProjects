using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing; 
namespace gloSnoMed
{
    public partial class FrmSelectProblem : Form
    {
        #region "Private Variables"
        
        private gloSnoMed.ClsGeneral objclsgeneral=new ClsGeneral() ;

        string _ModuleName = "";
        bool _blnnewProblem = false;
        public bool _DialogResult = false;
        System.DateTime _CurrentTime;
        public string gstrSMDBConnstr;
        public string EMRConnString;

        bool _isFormLoading = false;
        private string _SearchBy = "";
        private Boolean bTrigger = true;
        #endregion

        #region "Public Variables"
        public bool blnnewProblem
        {
            get { return _blnnewProblem; }
            set { _blnnewProblem = value; }
        }

        public string strProblem = "";
        public string strICD9 = "";
        public string strICD10 = "";
        public string strSelectedFindings = "";

        public string strSelectedConceptID = "";
        public string strSelectedSnoMedID = "";
        public string strSelectedDescriptionID = "";
        public string strSelectedDescription = "";

        public string strSelectedDefination = "";

        public string strConceptID = "";
        public string strDefination = "";
        public string strDescriptionID = "";
        public string StrSnoMedID = "";
        public string strNDCCode = "";
        public string strRxNormCode = "";
        public string strConceptDesc = "";
        public string strCodeSystem = "SNOMED";
        public Boolean blnIsProblem = false;
        
        #endregion


        #region"Form Events"
        private void FrmSelectProblem_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void FrmSelectProblem_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
        }


        private void FrmSelectProblem_Load(object sender, EventArgs e)
        {
            int myScreenWidth = 0;
            int myScreenHeight = 0;
            toolTip1.SetToolTip(btnAdvanceDef, "Show Advanced Definition");
            btnAdvanceDef.Tag = "Show";
            _isFormLoading = true;

            try
            {
                lblConceptID.Text = "";
                myScreenWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                myScreenHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight-20; //added for bottom margin

                if (this.Width > myScreenWidth | this.Height > myScreenHeight)
                {
                    this.MaximumSize = new System.Drawing.Size(myScreenWidth, myScreenHeight);
                    this.AutoScroll = true;

                }

                txtSMSearch.Select();
              
                if (gstrSMDBConnstr != string.Empty)
                {
                    objclsgeneral.IsConnect(gstrSMDBConnstr, EMRConnString);
                }

                if (blnIsProblem)
                {
                    chkCOREProblem.Checked = true;
                    if (strCodeSystem == "ICD9")
                    {
                        _SearchBy = RbICD9.Tag.ToString();
                        RbICD9.Checked = true;

                    }
                    else if (strCodeSystem == "ICD10")
                    {
                        _SearchBy = RbICD10.Tag.ToString();
                        RbICD10.Checked = true;

                    }
                    else
                    {
                        _SearchBy = RbConceptID.Tag.ToString();
                        RbConceptID.Checked = true;

                    }

                }
                else
                {
                    _SearchBy = RbConceptID.Tag.ToString();
                    RbConceptID.Checked = true;

                }

                this.trICD10.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _isFormLoading = false;
                
            }

        }

        #endregion


        #region "Constructor"
       
        public FrmSelectProblem()
	  {
		
		InitializeComponent();

	  }
        public FrmSelectProblem(string ModuleName, string SnomedConString, string _EMRConnString)
	   {
       
		InitializeComponent();
		gstrSMDBConnstr = SnomedConString;
		
		this.Text = ModuleName;
		_ModuleName = ModuleName;
		EMRConnString = _EMRConnString;


	   }
        #endregion

        #region "Private methods"
        
        private void ClearData()
        {
            trvFindings.Nodes.Clear();
            trvSubtype.Nodes.Clear();
            trvSnoMed.Nodes.Clear();
            trICD9.Nodes.Clear();
            trICD10.Nodes.Clear();
            lblConceptID.Text = "";
            lblSnoMedID.Text = "";
            lblDescriptionID.Text = "";
            strSelectedSnoMedID = "";
            strSelectedDescriptionID = "";
        }
        private void saveProblem()
        {
            foreach (TreeNode oParentNode in trICD10.Nodes)
            {

                if (oParentNode.Checked)
                {
                    strICD10 = oParentNode.Text;
                    return;
                }

            }

            foreach (TreeNode oParentNode in trICD9.Nodes)
            {

                if (oParentNode.Checked)
                {
                    strICD9 = oParentNode.Text;
                    return;
                }

            }


        }  
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSMSearch.Text = "";
            tlAddFields.SetToolTip(btnClear, "Clear");
        }

        private void tls_SM_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
     
		switch (e.ClickedItem.Tag.ToString()) {
			case "Close":
				
				this.DialogResult = System.Windows.Forms.DialogResult.Yes;
				
				_DialogResult = false;

				this.Close();
				break;
			
			case "Save":
				
				if ((trvFindings.SelectedNode != null)) {
                  
                    if (trvFindings.SelectedNode.Tag != null)
                    {
                        strSelectedFindings = trvFindings.SelectedNode.Name;
                        strProblem = trvFindings.SelectedNode.Name;
                    }
					
				}

				strDefination = "";

                strSelectedDescription = strProblem;

				if ((!string.IsNullOrEmpty(strSelectedFindings)) ) {
					saveProblem();
					this.Close();
				} else {
					MessageBox.Show("Please select a SNOMED Concept before clicking ‘Save & Close’. When a SNOMED Concept is selected, the description will show in the center panel of the selection window." +"\n"+ "To exit the screen without saving, click ‘Close’", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtSMSearch.Focus();
					return;

				}
				_DialogResult = true;
				break;
			
		}
	}

        private void mnuFindings_Click(object sender, EventArgs e)
        {
            TreeNode oNode = default(TreeNode);
            try
            {

                trICD9.Nodes.Clear();
                trICD10.Nodes.Clear();

                lblConceptID.Text = "";
                lblSnoMedID.Text = "";
                lblDescriptionID.Text = "";
                strSelectedSnoMedID = "";
                strSelectedDescriptionID = "";
                oNode = trvFindings.SelectedNode;
                if ((oNode != null))
                {
                    if ((oNode.Tag != null))
                    {
                        strConceptID = oNode.Tag.ToString();
                        string ICD9Description = "";

                        if (string.IsNullOrEmpty(oNode.Name))
                        {
                            oNode.Name = oNode.Text;
                        }
                        ICD9Description = oNode.Name.Trim();
                        if (chkCOREProblem.Checked)
                        {
                            DataSet dsICD = null;
                            if (oNode.Parent != null)
                            {
                                String ICDCode = objclsgeneral.Fill_ICD9(oNode.Parent.Text);
                                if (RbICD9.Checked)
                                {
                                    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD9");
                                }
                                else
                                {
                                    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD10");
                                }



                            }
                            else
                            {
                                // String ICDCode = objclsgeneral.Fill_ICD9(oNode.Parent.Text);
                                dsICD = objclsgeneral.GetCOREICDData(strConceptID, "");
                            }
                            if (dsICD != null)
                            {
                                 //AS ICD is deprecated
                                objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsICD, trICD10);
                                objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsICD, trICD9);
                                if (dsICD.Tables["RxNormNDC"].Rows.Count > 0)
                                {
                                    strNDCCode = dsICD.Tables["RxNormNDC"].Rows[0]["NDCCode"].ToString();
                                    strRxNormCode = dsICD.Tables["RxNormNDC"].Rows[0]["RxNorm"].ToString();
                                }
                                else
                                {
                                    strNDCCode = "";
                                    strRxNormCode = "";
                                }
                                dsICD.Dispose();
                                dsICD = null;
                            }
                            else
                            {
                                strNDCCode = "";
                                strRxNormCode = "";

                            }
                        }
                        else
                        {
                            DataSet dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, false);
                            if (dsSnomed != null)
                            {
                                //AS ICD is deprecated we show codes from complex map
                                objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsSnomed, trICD10);
                                objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsSnomed, trICD9);
                                if (dsSnomed.Tables["RxNormNDC"].Rows.Count > 0)
                                {
                                    strNDCCode = dsSnomed.Tables["RxNormNDC"].Rows[0]["NDCCode"].ToString();
                                    strRxNormCode = dsSnomed.Tables["RxNormNDC"].Rows[0]["RxNorm"].ToString();
                                }
                                else
                                {
                                    strNDCCode = "";
                                    strRxNormCode = "";
                                }
                                dsSnomed.Dispose();
                                dsSnomed = null;
                            }
                            else
                            {
                                strNDCCode = "";
                                strRxNormCode = "";
                            }

                        }

                        strSelectedConceptID = strConceptID;
                        if (!string.IsNullOrEmpty(oNode.Name))
                        {
                            //ICD9Desc = objclsgeneral.Fill_ICD9Description(oNode.Name);
                            lblConceptID.Text = strSelectedConceptID + " - " + oNode.Name;
                        }
                        else
                        {
                            lblConceptID.Text = strSelectedConceptID;
                        }



                        strProblem = oNode.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oNode !=null)
                {
                    oNode =null ;
                }
            }

        }
        private void FillDefination()
        {
            TreeNode oNode = default(TreeNode);
            DataSet dsSnomed = new DataSet();
            trvSubtype.Nodes.Clear();
            trvSnoMed.Nodes.Clear();

            oNode = trvFindings.SelectedNode;
            if ((oNode != null))
            {
                if ((oNode.Tag != null))
                {
                    strConceptID = oNode.Tag.ToString();
                    string ICD9Description = "";

                    if (string.IsNullOrEmpty(oNode.Name))
                    {
                        oNode.Name = oNode.Text;
                    }
                    ICD9Description = oNode.Name.Trim();
                    oNode = null;
                        dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, true);
                   
                    if ((dsSnomed == null) == false)
                    {

                           

                            objclsgeneral.Fill_snomedDescription(strConceptID, trvSnoMed, dsSnomed);


                            //strSelectedDescription not used 
                            //if (dsSnomed.Tables["IsDefinition"].Rows.Count > 0)
                            //{
                            //    strSelectedDescription = dsSnomed.Tables["IsDefinition"].Rows[0]["FULLYSPECIFIEDNAME"].ToString();
                            //}
                        
                       

                    }
                    if (dsSnomed != null)
                    {
                        dsSnomed.Dispose();
                        dsSnomed = null;
                    }
                  
                }
            }

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {

                if (!string.IsNullOrEmpty(this.Text.Trim()))
                {
                    if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                    {
                        Timer1.Stop();

                        
                        mnuFindings_Click(null, null);
                        if (pnlmiddle.Visible)
                        {
                            FillDefination();
                        }
                       
                    }

                }
                else
                {
                    Timer1.Stop();
                 
                  mnuFindings_Click(null, null);
                    if (pnlmiddle.Visible)
                        {
                            FillDefination();
                        }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void trvFindings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Timer1.Enabled == false)
            {
                Timer1.Stop();
                Timer1.Enabled = true;
            }
        }

        private void trvFindings_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            _CurrentTime = DateTime.Now;
            Timer1.Stop();
            Timer1.Interval = 700;
            Timer1.Enabled = true;
        }

        private void trvFindings_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if ((e.Node == null) == false)
                {
                   
                   // TreeNode eNode = new TreeNode();
                    TreeNode eNode = e.Node;
                    DataSet dsTreeview = null; // new DataSet();
                    if ((eNode != null))
                    {
                        if ((e.Node.Parent == null) == false)
                        {
                            if (e.Node.Nodes[0].Tag.ToString() == "TempNode999*")
                            {
                                trvFindings.Nodes.Remove(e.Node.Nodes[0]);
                                if (chkCOREProblem.Checked == false)
                                {
                                    if (RbICD9.Checked || RbICD10.Checked)
                                    {
                                        if (e.Node.Parent.Nodes[0].Tag.ToString() != null)
                                        {
                                            // dsTreeview = objclsgeneral.Fill_SubTypes(e.Node.Parent.Nodes[0].Tag .ToString(), false);
                                            string ICD9Desc = objclsgeneral.Fill_ICD9(e.Node.Parent.Text.ToString());

                                            //Pass rootnode conceptid while expanding child nodes  ''e.Node.Parent.Nodes[0].Tag
                                            dsTreeview = objclsgeneral.Fill_TreeOnExpand_ICD(e.Node.Parent.Nodes[0].Tag.ToString(), _SearchBy, ICD9Desc);


                                            objclsgeneral.FillSubtypeHierarchy_New(e.Node.Parent.Nodes[0].Tag.ToString(), dsTreeview, eNode);
                                        }
                                    }
                                    else
                                    {
                                        dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), false);
                                        objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode);
                                    }
                                }
                                  

                            }
                        }
                        else if (e.Node.Nodes[0].Tag.ToString () == "TempNode9999*")
                            {

                                dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), true);
                               objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview,chkCOREProblem.Checked);
                               if (eNode.Tag != null)
                               {
                                   strProblem = eNode.Name;
                                   strSelectedConceptID = eNode.Tag.ToString();

                                   if ((dsTreeview == null) == false)
                                   {
                                       if (dsTreeview.Tables["Parent"].Rows.Count > 0)
                                       {
                                           strDescriptionID = dsTreeview.Tables["Parent"].Rows[0]["DESCRIPTIONID"].ToString();
                                           StrSnoMedID = dsTreeview.Tables["Parent"].Rows[0]["SNOMEDID"].ToString();
                                       }
                                       //if (dsTreeview.Tables["IsDefinition"].Rows.Count > 0)
                                       //{
                                       //    strSelectedDescription = dsTreeview.Tables["IsDefinition"].Rows[0]["FULLYSPECIFIEDNAME"].ToString();
                                       //}
                                   }
                                //   string ICD9Desc = "";
                                   if (!string.IsNullOrEmpty(eNode.Name.Trim()))
                                   {
                                       //ICD9Desc = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim());
                                       lblConceptID.Text = eNode.Tag + " - " + eNode.Name.Trim();
                                   }
                                   else
                                   {
                                       lblConceptID.Text = eNode.Tag.ToString();
                                   }
                               }
                               

                                lblSnoMedID.Text = strSelectedSnoMedID;
                                lblDescriptionID.Text = strSelectedDescriptionID;

                            }
                        else if (e.Node.Nodes[0].Tag.ToString () == "ICDTempNode9999*")
                        {
                            trvFindings.Nodes.Remove(e.Node.Nodes[0]);
                           String ICD9Desc = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim());
                            string ICDCode =objclsgeneral .Fill_ICD9(eNode.Name.Trim ());
                            if (chkCOREProblem.Checked)
                            {
                                dsTreeview = objclsgeneral.Fill_CORESearchICD(ICD9Desc, _SearchBy, ICDCode);
                            }
                            else
                            {
                                dsTreeview = objclsgeneral.Fill_SnomedDetailsByConceptID(ICD9Desc, _SearchBy, ICDCode);
                            }
                             
                            
                           
                           if (dsTreeview != null)
                           {
                               if (dsTreeview.Tables.Count > 0)
                               {
                                  // objclsgeneral.FillSubtypeHierarchy_ByICD9(dsTreeview.Tables[1], eNode);
                                   dsTreeview.Tables[1].TableName = "Parent";
                                   objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview,chkCOREProblem.Checked );
                               }
                           }
                        }

                       
                       
                    }


                    if (dsTreeview != null)
                    {
                        dsTreeview.Dispose();
                        dsTreeview = null;
                    }
                   
                    if ((eNode != null))
                    {
                        eNode = null;
                    }
                }
            }
            catch// (Exception ex)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtSMSearch_SearchFired()
        {
            string _Term = null;
            
            try
            {
              
                if (txtSMSearch.Text.Length > 1)
                {
                    trvFindings.BeginUpdate();
                    this.Cursor = Cursors.WaitCursor;
                    _Term = "";
                    if (chkCOREProblem.Checked)
                    {
                       
                            objclsgeneral.SearchCORESnomed(txtSMSearch.Text.Trim(), trvFindings, _SearchBy );
                           
                    }
                    else
                    {
                        
                            objclsgeneral.SearchSnomed(txtSMSearch.Text.Trim(), false, trvFindings, _SearchBy );
                        
                    }
                    
                        _Term = strConceptDesc;

                      if (chkCOREProblem.Checked && (strCodeSystem == "ICD9" || strCodeSystem == "ICD10"))
                        {

                            if (trvFindings.Nodes.Count > 0)
                            {
                               
                                if (trvFindings.Nodes[0].Nodes.Count > 0)
                                {
                                    if (trvFindings.Nodes.Count == 1)
                                    {
                                        TreeNode oNode = trvFindings.Nodes[0];
                                        trvFindings.SelectedNode = oNode;
                                        trvFindings.Nodes[0].ExpandAll();
                                        foreach (TreeNode onode in trvFindings.Nodes[0].Nodes)
                                        {
                                            if ((onode == null) == false)
                                            {

                                                if (Convert.ToString(onode.Tag) == strConceptID)
                                                {
                                                    if (Convert.ToString(onode.Tag) == strConceptID & onode.Text.Trim() == _Term.Trim())
                                                    {
                                                        trvFindings.SelectedNode = onode;
                                                        break; // TODO: might not be correct. Was : Exit For
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }

                            }
                        }
                        else
                        {

                            foreach (TreeNode onode in trvFindings.Nodes)
                            {
                                if ((onode == null) == false)
                                {
                                    if (Convert.ToString(onode.Tag) == strConceptID)
                                    {
                                        if (Convert.ToString(onode.Tag) == strConceptID & onode.Name.Trim() == _Term.Trim())
                                        {
                                            trvFindings.SelectedNode = onode;
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                    }
                                }

                            }
                        }
                   // }
                    this.Cursor = Cursors.Default;


                    trvFindings.EndUpdate();
                }
                else
                {
                    trvFindings.Nodes.Clear();
                    trvSubtype.Nodes.Clear();
                    trvSnoMed.Nodes.Clear();
                    trICD9.Nodes.Clear();
                    trICD10.Nodes.Clear();

                    //Sanjog
                    lblConceptID.Text = "";
                    lblSnoMedID.Text = "";
                    lblDescriptionID.Text = "";
                    strSelectedSnoMedID = "";
                    strSelectedDescriptionID = "";
                    //Sanjog
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                
                this.Cursor = Cursors.Default;
            }
        }

        #region "CORE Problem"

        private void chkCOREProblem_CheckedChanged(object sender, EventArgs e)
        {
            
            if (_isFormLoading == false)
            {
                ClearData();
               
                txtSMSearch_SearchFired();
            }

        }
        #endregion

        private void trICD9_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (bTrigger)
            {

                if (objclsgeneral._isICDtreeFilling == false)
                {

                    foreach (TreeNode _node in trICD9.Nodes)
                    {
                        bTrigger = false;
                        if (_node == e.Node)
                        {
                        }
                        else
                        {
                            _node.Checked = false;
                        }
                        bTrigger = true;
                    }
                    foreach (TreeNode _node in trICD10.Nodes)
                    {
                        bTrigger = false;
                        if (_node == e.Node)
                        {
                        }
                        else
                        {
                            _node.Checked = false;
                        }
                        bTrigger = true;
                    }
                }
            }
        }

        private void trICD10_AfterCheck(object sender, TreeViewEventArgs e)
        {
            

            if (e.Node.Text.Contains("?") == true && e.Node.Checked == true)
            {
                this.trICD10.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);
                e.Node.Checked = false;
                MessageBox.Show("This is not a complete ICD10 code and hence it cannot be selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.trICD10.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);
                return;
            }

            

            if (bTrigger)
            {

                if (objclsgeneral._isICDtreeFilling == false)
                {

                    foreach (TreeNode _node in trICD10.Nodes)
                    {
                        bTrigger = false;
                        if (_node == e.Node)
                        {
                        }
                        else
                        {
                            _node.Checked = false;
                        }
                        bTrigger = true;
                    }
                    foreach (TreeNode _node in trICD9.Nodes)
                    {
                        bTrigger = false;
                        if (_node == e.Node)
                        {
                        }
                        else
                        {
                            _node.Checked = false;
                        }
                        bTrigger = true;
                    }
                }
            }

           
        }

        

        private void btnAdvanceDef_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (btnAdvanceDef.Tag.ToString() == "Show")
                {
                    toolTip1.SetToolTip(btnAdvanceDef, "Hide Advanced Definition");
                    btnAdvanceDef.Tag = "Hide";
                    btnAdvanceDef.Image = Resource1.HIDE_ADVANCE_DEFINATION;
                    FillDefination();
                    pnlmiddle.Visible = true;
                }
                else
                {
                    toolTip1.SetToolTip(btnAdvanceDef, "Show Advanced Definition");
                    btnAdvanceDef.Tag = "Show";
                    btnAdvanceDef.Image = Resource1.SHOW_ADVANCE_DEFINATION;
                    pnlmiddle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void RbConceptID_Click(object sender, EventArgs e)
        {
            _SearchBy = RbConceptID.Tag.ToString();
            ClearData();

            //  LoadData();
           
            txtSMSearch_SearchFired();
        }

        private void RbICD9_Click(object sender, EventArgs e)
        {
            _SearchBy = RbICD9.Tag.ToString();
            ClearData();

            
            txtSMSearch_SearchFired();
        }

        private void RbICD10_Click(object sender, EventArgs e)
        {
            _SearchBy = RbICD10.Tag.ToString();
            ClearData();

           
            txtSMSearch_SearchFired();
        }

        private void RbConceptID_CheckedChanged(object sender, EventArgs e)
        {
            if (RbConceptID.Checked == true)
            {
                RbConceptID.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                RbConceptID.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void RbICD9_CheckedChanged(object sender, EventArgs e)
        {
            if (RbICD9.Checked == true)
            {
                RbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                RbICD9.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void RbICD10_CheckedChanged(object sender, EventArgs e)
        {
            if (RbICD10.Checked == true)
            {
                RbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                RbICD10.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
        #endregion

        private void txtSMSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //20-Jul-16 Aniket: Resolving Bug #98510: gloEMR : SnoMed Database Update : When user put '[' bracket at last digit, code gets filter out
            if (e.KeyChar.ToString() == "[" || e.KeyChar.ToString() == "]")
            {
                e.Handled = true;
            }

        }

        private void trICD10_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //this.trICD10.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);

            //if (e.Node.Text.Contains("?") == true && e.Node.Checked ==true )
            //{
            //    MessageBox.Show("This is not a complete ICD10 code and hence it cannot be selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Cancel = true;
            //}

            //this.trICD10.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);
        }

        private void FrmSelectProblem_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.trICD10.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trICD10_AfterCheck);
        }


    }
        
    
}
