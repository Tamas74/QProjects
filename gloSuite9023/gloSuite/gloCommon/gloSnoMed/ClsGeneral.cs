using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Collections;



namespace gloSnoMed
{
    public class ClsGeneral: IDisposable
    {

        private bool disposed = false;

        ~ClsGeneral()
        {
              Dispose(false);
        }

        public void Dispose()
        {
            //Disconnect();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (ofrm1 != null)
                    {
                        ofrm1.Dispose();
                        ofrm1 = null;
                    }

                }
            }
            disposed = true;
        }

      

        public String ConnectionString;
        public String EMRConnString;
       
        Boolean IsDrug = false;
        Frm1 ofrm1;
        public Boolean _isICDtreeFilling = false;

        public Boolean IsConnect(String _con, string _EmrConString)
        {
            ConnectionString = _con;
            EMRConnString = _EmrConString;
            try
            {
                ofrm1 = new Frm1();

                return true;
            }
            catch (Exception ex)
            {
               
                UpdateLog("Error while connecting to Database :" + ex.ToString());
                return false;
            }
            
        }

        public void UpdateLog(string strLogMessage)
        {
            StreamWriter objFile;
            try
            {
                objFile = new System.IO.StreamWriter(Application.StartupPath + "\\SnoMed.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "\t" + strLogMessage);
                objFile.Close();
                objFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SearchSnomed(String SearchText, Boolean IsDrugItem, TreeView trFindings, String SearchBy)
        {
           
            trFindings.ImageList = ofrm1.imgList1;
            SqlConnection oConn = new SqlConnection(EMRConnString);
            DataTable dbResult = new DataTable();
            DataSet dsResult = new DataSet();
            try
            {
                //if (IsConnect(ConnectionString))  commented by nilesh GLO2009-0003508
                {
                    
                    oConn.Open();

                    IsDrug = IsDrugItem;
                   
                 
                    trFindings.Nodes.Clear();
                    if (SearchBy == "ConceptID")
                    {


                        if (IsDrug == false)
                        {
                            dbResult = IndexSearch_New(SearchText);
                            if (dbResult != null)
                            {
                                for (int i = 0; i < dbResult.Rows.Count; i++)
                                {

                                    //trFindings.Nodes.Add(dbResult.Rows[i][0].ToString());
                                    TreeNode oNode = new TreeNode();

                                    oNode.Text = dbResult.Rows[i][0].ToString();
                                    oNode.Tag = dbResult.Rows[i][1].ToString();
                                    oNode.Name = dbResult.Rows[i][0].ToString();

                                    oNode.ImageIndex = 0;
                                    oNode.SelectedImageIndex = 0;
                                    trFindings.Nodes.Add(oNode);

                                    TreeNode oChildNode = new TreeNode();
                                    oChildNode.Text = "TempNode";

                                    oChildNode.Tag = "TempNode9999*";


                                    oNode.Nodes.Add(oChildNode);
                                    oNode = null;


                                }
                            }
                           
                        }
                        else
                        {
                            String SearchValue = SearchText.Replace(" ", "%");
                            String qString = "SELECT DISTINCT TOP(100) CONVERT(VARCHAR(1000),TD.TERM),TD.CONCEPTID,LEN(CONVERT(VARCHAR(1000),TD.TERM)) FROM TERM_DRUGS_CONCEPTS_US TC INNER JOIN TERM_DRUGS_DESCRIPTIONS TD ON TC.CONCEPTID = TD.CONCEPTID INNER JOIN TERM_DRUG_RELATIONSHIPS_US TR ON TC.CONCEPTID = TR.CONCEPTID1 WHERE TD.TERM LIKE '%" + SearchValue + "%' AND TR.REFINABILITY = '0' AND TD.DESCRIPTIONSTATUS <> 8 AND TD.DESCRIPTIONTYPE <> 3 ORDER BY LEN(CONVERT(VARCHAR(1000),TD.TERM))";
                            SqlDataAdapter dbAdapt = new SqlDataAdapter(qString, oConn);
                            dbAdapt.Fill(dbResult);
                            dbAdapt.Dispose();
                            dbAdapt = null;
                        }
                    }
                    else if (SearchBy == "ICD9" || SearchBy == "ICD10")
                    {
                        dsResult  = Fill_SnomedDetailsByConceptID(SearchText, SearchBy,"");
                        if (dsResult != null)
                        {
                            if (dsResult.Tables.Count > 0)
                            {
                                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                                {

                                    TreeNode oNode = new TreeNode();

                                    oNode.Text = dsResult.Tables[0].Rows[i][0].ToString();
                                    oNode.Name = dsResult.Tables[0].Rows[i][0].ToString();
                                    oNode.ImageIndex = 0;
                                    oNode.SelectedImageIndex = 0;
                                    trFindings.Nodes.Add(oNode);

                                    TreeNode oChildNode = new TreeNode();
                                    oChildNode.Text = "ICDTempNode";
                                    oChildNode.Tag = "ICDTempNode9999*";
                                    oNode.Nodes.Add(oChildNode);
                                    oNode = null;

                                    //   FillSubtypeHierarchy_ByICD9(dsResult.Tables[1], oNode, dsResult.Tables[0].Rows[i][1].ToString());

                                }
                            }
                         
                        }
                       
                    }
                   
                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Error in Searching Snomed Data :" + ex.ToString());
                return false;
            }
            finally
            {
                

                if (oConn.State == System.Data.ConnectionState.Open)
                {
                    oConn.Close();
                    oConn.Dispose();
                    oConn = null;
                }
                if (dbResult != null)
                {
                    dbResult.Dispose();
                    dbResult = null;
                }
                if (dsResult  != null)
                {
                    dsResult.Dispose();
                    dsResult = null;
                }


            }
            //return true;
        }
        public Boolean Fill_Snomed_ISA_Definition(String strsubTypeConceptID, TreeView trSnomed)
        {
          
            trSnomed.ImageList = ofrm1.imgList1;
            SqlCommand objCmd = new SqlCommand();
            SqlConnection oConn = new SqlConnection(EMRConnString);
            DataSet Definition = new DataSet();
            try
            {

                oConn.Open();
                trSnomed.Nodes.Clear();

               
                if (IsDrug == false)
                {
                    //Code Start-Added by kanchan on 20100907 to remove inactive concept
                    //Definition = PDataset("select tc.FULLYSPECIFIEDNAME,tr.RELATIONSHIPTYPE  from Term_Relationships_Core tr inner join Term_Concepts_Core tc on tr.CONCEPTID2=tc.CONCEPTID  where CONCEPTID1='" + strsubTypeConceptID + "' and CHARACTERISTICTYPE='0'");

                    //Start new '20102010'
                    //Definition = PDataset("select tc.FULLYSPECIFIEDNAME,tr.RELATIONSHIPTYPE  from Term_Relationships_Core tr inner join Term_Concepts_Core tc on tr.CONCEPTID2=tc.CONCEPTID  where CONCEPTID1='" + strsubTypeConceptID + "' and CHARACTERISTICTYPE='0' and FULLYSPECIFIEDNAME not like '%inactive%concept%'");
                    Definition = PDataset("SELECT TC.FULLYSPECIFIEDNAME,TR.RELATIONSHIPTYPE  FROM TERM_RELATIONSHIPS_CORE TR INNER JOIN TERM_CONCEPTS_CORE TC ON TR.CONCEPTID2 = TC.CONCEPTID  WHERE TR.CONCEPTID1 = " + strsubTypeConceptID + " AND CHARACTERISTICTYPE = 0 AND FULLYSPECIFIEDNAME NOT LIKE '%INACTIVE%CONCEPT%'");
                    //ENd new '20102010'
                }
                else
                {
                    Definition = PDataset("select tc.FULLYSPECIFIEDNAME,tr.RELATIONSHIPTYPE  from Term_Drug_Relationships_US tr inner join Term_Drugs_Concepts_US tc on tr.CONCEPTID2=tc.CONCEPTID  where CONCEPTID1='" + strsubTypeConceptID + "' and CHARACTERISTICTYPE='0'");
                }

                if (Definition.Tables[0].Rows.Count > 0)
                {
                    TreeNode trDefinition = new TreeNode();
                    trDefinition.Text = "Definition : Fully Defined as...";
                    trDefinition.ImageIndex = 2;
                    trDefinition.SelectedImageIndex = 2;
                    foreach (DataRow dr in Definition.Tables[0].Rows)
                    {
                        if (dr["FULLYSPECIFIEDNAME"].ToString() != "")
                        {
                            TreeNode tnChild = new TreeNode();


                            objCmd.CommandType = CommandType.Text;
                            if (IsDrug == false)
                            {
                                //Start new '20102010'
                                //objCmd.CommandText = "select term from Term_Descriptions where CONCEPTID = " + dr["RELATIONSHIPTYPE"] + " and DESCRIPTIONTYPE ='1'";
                                objCmd.CommandText = "SELECT TERM FROM TERM_DESCRIPTIONS WHERE CONCEPTID = " + dr["RELATIONSHIPTYPE"] + " AND DESCRIPTIONTYPE = 1";
                                //ENd new '20102010'
                            }
                            else
                            {
                                objCmd.CommandText = "select term from Term_Drugs_Descriptions where CONCEPTID = '" + dr["RELATIONSHIPTYPE"] + "' and DESCRIPTIONTYPE ='1'";
                            }

                            objCmd.Connection = oConn;
                            Object result = objCmd.ExecuteScalar();
                            if (result != null)
                            {
                                tnChild.Text = result.ToString();
                                tnChild.ImageIndex = 0;
                                tnChild.SelectedImageIndex = 0;
                            }

                            tnChild.Nodes.Add(dr["FULLYSPECIFIEDNAME"].ToString());
                            tnChild.Nodes[0].ImageIndex = 1;
                            tnChild.Nodes[0].SelectedImageIndex = 1;

                            trDefinition.Nodes.Add(tnChild);
                        }
                    }
                    trSnomed.Nodes.Add(trDefinition);
                }
                trSnomed.Focus();
                trSnomed.ExpandAll();

            }
            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Description :" + ex.ToString());
                return false;
            }
            finally
            {
              
                if (oConn.State == System.Data.ConnectionState.Open)
                {
                    oConn.Close();
                    oConn.Dispose();
                    oConn = null;
                }
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (Definition != null)
                {
                    Definition.Dispose();
                    Definition = null;
                }
            }

            return true;

        }
        public DataSet Fill_SnomedDetailsByConceptID(String SearchText, String _SearchBy,string _ICDCode)
        {
           SqlConnection _con = new SqlConnection(EMRConnString );
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                // DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                _con.Open();

                _sqlcmd = new SqlCommand("History_SearchByConceptID", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;

                _sqlcmd.Parameters.Add("@SearchText", SqlDbType.NVarChar).Value = SearchText;
                _sqlcmd.Parameters.Add("@SearchBy", SqlDbType.NVarChar).Value = _SearchBy;
                _sqlcmd.Parameters.Add("@ICDCode", SqlDbType.NVarChar).Value = _ICDCode;
                _sqlda.SelectCommand = _sqlcmd;
                _sqlda.Fill(ds);

                _con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Details :" + ex.ToString());
                return null;
            }
            finally
            {
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

                if (_con != null)
                {
                    _con.Dispose();
                    _con = null;
                }
                _sqlda.Dispose();
                _sqlda = null;
             }

        }

        public DataSet Fill_TreeOnExpand_ICD(String SearchText, String _SearchBy, string _ICDCode)
        {
            SqlConnection _con = new SqlConnection(EMRConnString);
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                // DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                _con.Open();

                _sqlcmd = new SqlCommand("History_SearchICD", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@SearchText", SqlDbType.NVarChar).Value = SearchText;
                _sqlcmd.Parameters.Add("@SearchBy", SqlDbType.NVarChar).Value = _SearchBy;
                _sqlcmd.Parameters.Add("@ICDCode", SqlDbType.NVarChar).Value = _ICDCode;

                _sqlda.SelectCommand = _sqlcmd;

                _sqlda.Fill(ds);
                ds.Tables[0].TableName = "Child";

                _con.Close();
                return ds;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Details :" + ex.ToString());
                return null;
            }
            finally
            {
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

                if (_con != null)
                {
                    _con.Dispose();
                    _con = null;
                }
                _sqlda.Dispose();
                _sqlda = null;



            }

        }
        public DataSet Fill_SubTypes(String ConceptID, Boolean IsParent, String ConceptDesc="", Boolean IsGetConceptIDs=false)
        {
            SqlConnection _con = new SqlConnection(EMRConnString );
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                DataSet ds = new DataSet();

                _con.Open();

                _sqlcmd = new SqlCommand("gsp_SearchSnomed", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@ConceptID", SqlDbType.NVarChar).Value = ConceptID;
                _sqlcmd.Parameters.Add("@IsParent", SqlDbType.Bit).Value = IsParent;

                //_sqlcmd.Parameters.Add("@ConceptDesc", SqlDbType.NVarChar).Value = ConceptDesc;
                //_sqlcmd.Parameters.Add("@IsGetConceptIDs", SqlDbType.Bit).Value = IsGetConceptIDs;


                _sqlda.SelectCommand = _sqlcmd;

                _sqlda.Fill(ds);

                if (IsParent == true)
                {
                    ds.Tables[0].TableName = "Parent";
                    //ds.Tables[1].TableName = "IsDefinition";
                }
                else
                {
                    ds.Tables[0].TableName = "Child";
                }
                
                _con.Close();
                
                return ds;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Details :" + ex.ToString());
                return null;
            }
            finally
            {
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

                if (_con != null)
                {
                    _con.Dispose();
                    _con = null;
                }
                _sqlda.Dispose();
                _sqlda = null;
            }
        }
        
       
       

        public DataTable IndexSearch_New(String temp)
        {
            DataTable dtFinalWord = new DataTable();

            //22-Jul-16 Aniket: Resolving Bug #98568: gloEMR : Snomed (History) :Application should not display any list of codes when user enter space twice in search box of snomed\ICD10
            if (temp.Trim() != "")
            {
                temp = temp.ToUpper().Trim();
                dtFinalWord = SearchSnomedString(temp);
            }
            
            return dtFinalWord;
        }

        public DataTable SearchSnomedString(String SearchText)
        {
            SqlConnection _con = new SqlConnection(EMRConnString);
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                DataTable dt = new DataTable();
                _con.Open();
                _sqlcmd = new SqlCommand("History_SearchsnomedString", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@tempstring", SqlDbType.NVarChar).Value = SearchText;
                _sqlda.SelectCommand = _sqlcmd;
                _sqlda.Fill(dt);
                _con.Close();
                return dt;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while searching Snomed String :" + ex.ToString());
                return null;
            }
            finally
            {
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

                if (_con != null)
                {
                    _con.Dispose();
                    _con = null;
                }
                _sqlda.Dispose();
                _sqlda = null;

            }

        }
        public DataSet Fill_SnomedDetails(String ConceptID,String  isDrugItem,String Term,Boolean _IsShowDefinition)
        {
            SqlConnection _con = new SqlConnection(EMRConnString );
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                DataSet ds = new DataSet();
                _con.Open();
                _sqlcmd = new SqlCommand("Fill_SnomedDetails", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@ConceptCode", SqlDbType.NVarChar).Value = ConceptID;
                if (isDrugItem=="False")
                {
                    _sqlcmd.Parameters.Add("@isDrug", SqlDbType.Bit).Value = 0;
                }
                else{
                    _sqlcmd.Parameters.Add("@isDrug", SqlDbType.Bit).Value = 1;
                }

                _sqlcmd.Parameters.Add("@Term", SqlDbType.NVarChar).Value = Term;
                if (_IsShowDefinition)
                {
                    _sqlcmd.Parameters.Add("@isShowDefinition", SqlDbType.Bit).Value = 1 ;
                }
                else
                {
                    _sqlcmd.Parameters.Add("@isShowDefinition", SqlDbType.Bit).Value = 0;
                }

                _sqlda.SelectCommand = _sqlcmd;

                _sqlda.Fill(ds);
                if (_IsShowDefinition)
                {
                    ds.Tables[0].TableName = "Descriptions";
                    ds.Tables[1].TableName = "Definition";
                    //ds.Tables[2].TableName = "Qualifiers";
                    //ds.Tables[3].TableName = "IsDefinition";
                    ds.Tables[2].TableName = "SnomedCodes";
                    //ds.Tables[5].TableName = "ICD9";
                    //ds.Tables[5].TableName = "RxNormNDC";
                    //ds.Tables[6].TableName = "ICD10";
                }
                else
                {
                    //ICD 9 Code Deprecated
                    ds.Tables[0].TableName = "ICD9";

                    ds.Tables[1].TableName = "RxNormNDC";
                    ds.Tables[2].TableName = "ICD10";
                }
                //_sqlcmd.Dispose();
                _con.Close();
                return ds;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Details :" + ex.ToString());
                return null;
            }
            finally
            {
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

                if (_con != null)
                {
                    _con.Dispose();
                    _con = null;
                }
                _sqlda.Dispose();
                _sqlda = null;
            }
        }
       
        public Boolean FillParentNodes(TreeView trFindings, TreeNode oNode, DataSet dsTreeview,Boolean _isProblem)
        {

            oNode.Nodes.Clear();
          
            foreach (DataRow dr in dsTreeview.Tables["Parent"].Rows)
            {
                if (dr["TERM"].ToString() != "")
                {
                    myTreeNode tnParent = new myTreeNode();
                    tnParent.Text = dr["TERM"].ToString();
                    tnParent.Name = dr["TERM"].ToString();
                    tnParent.Tag = dr["CONCEPTID"].ToString();
                    tnParent.ConceptID = dr["CONCEPTID"].ToString();
                    //tnParent.DescriptionID = dr["DESCRIPTIONID"].ToString();
                    //tnParent.SnoMedID = dr["SNOMEDID"].ToString();
                    tnParent.ImageIndex = 0;
                    tnParent.SelectedImageIndex = 0;
                    tnParent.Expand();
                    trFindings.Nodes[oNode.Index].Nodes.Add(tnParent);



                }
            }
            if (_isProblem==false )
            {
                if (dsTreeview.Tables["Parent"].Rows.Count > 1)
                {
                    int index = trFindings.Nodes[oNode.Index].Nodes.Count - 1;
                    TreeNode oChildNode = new TreeNode();
                    oChildNode.Text = "TempNode";
                    oChildNode.Tag = "TempNode999*";

                    trFindings.Nodes[oNode.Index].Nodes[index].Nodes.Add(oChildNode);
                }
            }
           
            return true;
        }
        public Int32 count_node = 0;
        
        public Boolean FillSubtypeHierarchy_New(String ConceptID, DataSet dsTreeView, TreeNode enode)
        {
            DataView dvTree = new DataView();
            try
            {

                
                dvTree = dsTreeView.Tables["Child"].DefaultView;
                dvTree.RowFilter = "CONCEPTID= '" + ConceptID + "'";

                //  PrSet1 = PDataset("SELECT TOP(100) TD.TERM,TD.CONCEPTID,TD.DESCRIPTIONID,TC.SNOMEDID FROM TERM_DESCRIPTIONS TD INNER JOIN TERM_CONCEPTS_CORE TC ON TD.CONCEPTID = TC.CONCEPTID WHERE TD.CONCEPTID = " + ConceptID + " AND DESCRIPTIONTYPE = 1 AND FULLYSPECIFIEDNAME NOT LIKE '%INACTIVE%CONCEPT%'");

                count_node = 0;
                if (dsTreeView.Tables["Child"].Rows.Count > 1) //IF only one child node is there in icd then dont add that node
                {

                    if (dvTree.ToTable().Rows.Count > 0)
                    {
                        myTreeNode tnParent = new myTreeNode();
                        foreach (DataRow dr in dvTree.ToTable().Rows)
                        {
                            if (dr["Term"].ToString() != "")
                            {
                                tnParent.Text = dr["TERM"].ToString();
                                tnParent.Name = dr["TERM"].ToString();

                                tnParent.Tag = dr["CONCEPTID"].ToString();
                                tnParent.ConceptID = dr["CONCEPTID"].ToString();
                                tnParent.DescriptionID = dr["DESCRIPTIONID"].ToString();
                                tnParent.SnoMedID = dr["SNOMEDID"].ToString();
                                tnParent.ImageIndex = 0;
                                tnParent.SelectedImageIndex = 0;
                                tnParent.Expand();
                                FillChild_New(tnParent, dsTreeView);
                            }
                        }
                        enode.Nodes.Add(tnParent);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());
                return false;
            }
            finally
            {
                if (dvTree != null)
                {
                    dvTree.Dispose();
                    dvTree = null;
                }
                if (dsTreeView != null)
                {
                    dsTreeView.Dispose();
                    dsTreeView = null;
                }
            }

            return true;
        }

        public Boolean FillSubtypeHierarchy_ByICD9( DataTable  dsTreeView, TreeNode enode)
        {
            DataView dvTree = new DataView();
            try
            {



                dvTree = dsTreeView.DefaultView;
                

                if (dvTree.ToTable().Rows.Count > 0)
                {

                    foreach (DataRow dr in dvTree.ToTable().Rows)
                    {
                        if (dr["Term"].ToString() != "")
                        {
                            myTreeNode tnParent = new myTreeNode();
                            tnParent.Text = dr["TERM"].ToString();
                            tnParent.Name = dr["TERM"].ToString();

                            tnParent.Tag = dr["CONCEPTID"].ToString();
                            tnParent.ConceptID = dr["CONCEPTID"].ToString();

                            tnParent.ImageIndex = 0;
                            tnParent.SelectedImageIndex = 0;
                            enode.Nodes.Add(tnParent);

                        }
                    }
                    // enode.Nodes.Add(tnParent);
                }
                int index = enode.Nodes[enode.Index].Nodes.Count - 1;
                TreeNode oChildNode = new TreeNode();
                oChildNode.Text = "TempNode";
                oChildNode.Tag = "TempNode999*";

                enode.Nodes[enode.Index].Nodes[index].Nodes.Add(oChildNode);
                count_node = 0;




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());
                return false;
            }
            finally
            {
                if (dvTree != null)
                {
                    dvTree.Dispose();
                    dvTree = null;
                }
                if (dsTreeView != null)
                {
                    dsTreeView.Dispose();
                    dsTreeView = null;
                }
            }

            return true;
        }
     



        public int FillChild_New(TreeNode parent, DataSet dsTree)
        {
            DataView dvTree = new DataView();
            DataTable dtTerm = null;
            try{

            dvTree = dsTree.Tables["Child"].DefaultView;
           
                dvTree.RowFilter = " RootTerm = " + "'" + parent.Text.Replace ("'","''") + "'" + " AND CONCEPTID <> RootCONCEPTID AND RootCONCEPTID= " + parent.Tag;
                 dtTerm = new DataTable();
                dtTerm = dvTree.ToTable(true, new string[] { "Term", "CONCEPTID", "DESCRIPTIONID", "SNOMEDID", "RootCONCEPTID", "RootTerm" });
                if (dtTerm != null)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        dvTree = dtTerm.DefaultView;
                    }

                }
            
            if (dvTree.ToTable().Rows.Count > 0)
            {
                foreach (DataRow dr in dvTree.ToTable().Rows)
                {
                    myTreeNode tnChild = new myTreeNode();
                    tnChild.Text = dr["TERM"].ToString();
                    tnChild.Name = dr["TERM"].ToString();
                   
                    tnChild.Tag = dr["CONCEPTID"].ToString();
                    tnChild.ConceptID = dr["CONCEPTID"].ToString();
                    tnChild.DescriptionID = dr["DESCRIPTIONID"].ToString();
                    tnChild.SnoMedID = dr["SNOMEDID"].ToString();
                    parent.ImageIndex = 0;
                    parent.SelectedImageIndex = 0;
                    tnChild.ImageIndex = 1;
                    tnChild.SelectedImageIndex = 1;
                    tnChild.Collapse();
                    parent.Nodes.Add(tnChild);
                    count_node += 1;
                    if (count_node >= 50)
                        break;
                   FillChild_New(tnChild, dsTree);
                }
                return 0;
            }
            else
            {
                return 0;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());
                return 0;
            }
            finally
            {
                if (dvTree != null)
                {
                    dvTree.Dispose();
                    dvTree = null;
                }
                if (dsTree  != null)
                {
                    dsTree.Dispose();
                    dsTree = null;
                }
                if (dtTerm != null)
                {
                    dtTerm.Dispose();
                    dtTerm = null;
                }
            }
        }
       

        protected DataSet PDataset(string select_statement)
        {
            SqlConnection oConn = new SqlConnection(EMRConnString);
            SqlDataAdapter ad = new SqlDataAdapter(select_statement, oConn); 
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (oConn != null)
            {
                oConn.Dispose();
                oConn = null;
            }
            if (ad != null)
            {
                ad.Dispose();
                ad = null;
            }
            return ds;
        }


        public string GetConnectionString(string strSQLServerName, string strDatabaseName, string strSQLUserID, string strSQLPassword, bool isSQLAuthentication)
        {
            string strConnectionString = string.Empty;
            if (isSQLAuthentication == false)
            {
               strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabaseName + ";Integrated Security=SSPI";
            }
            else
            {
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabaseName + ";USER ID =" + strSQLUserID + ";PASSWORD=" + strSQLPassword;
            }
            return strConnectionString;

        }

   
        

        public void Fill_ICD9(String Conceptid, String Conceptdesc, TreeView trICD9, DataSet dsSnomed, TreeView trICD10)
        {
            _isICDtreeFilling = true;
           
            trICD9.ImageList = ofrm1.imgList1;
            trICD9.Nodes.Clear();
            DataView dvTree=null;
            DataTable dtTerm=null;
            try
            {
          
             if (dsSnomed.Tables["ICD9"].Rows.Count > 0)
            {
                 dvTree =  dsSnomed.Tables["ICD9"].DefaultView;
                 dtTerm =  dvTree.ToTable(true, new string[] { "sICD9", "sDescription" });
                if (dtTerm != null)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        dvTree = dtTerm.DefaultView;
                    }

                }
                if (dvTree.ToTable().Rows.Count > 0)
                {
                    foreach (DataRow dr in dvTree.ToTable().Rows)
                    {
                       
                        if (dr["sICD9"].ToString() != "" )
                        {
                           
                           String[] _multipleicd9=dr["sICD9"].ToString ().Split('|');

                           if (_multipleicd9.Length > 0)
                           {


                               for (Int16 i = 0; i <= _multipleicd9.Length - 1; i++)
                               {
                                   TreeNode child = new TreeNode();
                                   if (dr["sDescription"].ToString().Trim() == "")
                                   {
                                       child.Text = _multipleicd9[i].ToString() + " : " + "No Description";
                                   }
                                   else
                                   {
                                       child.Text = _multipleicd9[i].ToString() + " : " + dr["sDescription"].ToString().Trim() + " " + "(Deprecated)";
                                   }

                                   child.Tag = _multipleicd9[i].ToString();
                                   child.ImageIndex = 4;
                                   child.SelectedImageIndex = 4;
                                   child.Collapse();
                                   trICD9.Nodes.Add(child);
                                   child = null;
                                   
                               }
                           }
                           _multipleicd9 = null;

                        }
                       
                    }

                }
                dtTerm.Dispose();
                dtTerm = null;
                dvTree.Dispose();
                dvTree = null;
           
            }
             _isICDtreeFilling = false;
                  }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());
               
            }
            finally
            {
                if (dsSnomed  != null)
                {
                    dsSnomed.Dispose();
                    dsSnomed = null;
                }
               
               
            }
         
        }

        public void Fill_ICD10(String Conceptid, String Conceptdesc, TreeView trICD10, DataSet dsSnomed, TreeView trICD9)
        {
            _isICDtreeFilling = true;
           
            trICD10.ImageList = ofrm1.imgList1;
            trICD10.Nodes.Clear();
            DataView dvTree = null;
            DataTable dtTerm = null;
            try
            {
                if (dsSnomed.Tables["ICD10"].Rows.Count > 0)
                {
                     dvTree = dsSnomed.Tables["ICD10"].DefaultView;
                     dtTerm = dvTree.ToTable(true, new string[] { "sICD10", "sICD10Description" });
                    if (dtTerm != null)
                    {
                        if (dtTerm.Rows.Count > 0)
                        {
                            dvTree = dtTerm.DefaultView;
                        }

                    }
                    if (dvTree.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow dr in dvTree.ToTable().Rows)
                        {
                            if (dr["sICD10"].ToString() != "")
                            {
                                TreeNode child = new TreeNode();
                                if (dr["sICD10Description"].ToString().Trim() == "")
                                {
                                    child.Text = dr["sICD10"].ToString() + " : " + "No Description";
                                }
                                else
                                {
                                    child.Text = dr["sICD10"].ToString() + " : " + dr["sICD10Description"].ToString().Trim();
                                }


                                child.Tag = dr["sICD10"].ToString();
                                child.ImageIndex = 9;
                                child.SelectedImageIndex = 9;
                                child.Collapse();
                                trICD10.Nodes.Add(child);
                                child = null;
                            }

                          
                        }

                    }
                    dtTerm.Dispose();
                    dtTerm = null;
                    dvTree.Dispose();
                    dvTree = null;
                }

                trICD10.ExpandAll();
               
                _isICDtreeFilling = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());

            }
            finally
            {
                if (dsSnomed  != null)
                {
                    dsSnomed.Dispose();
                    dsSnomed = null;
                }
               
               
            }
        }

        

        public void Fill_LIONIC(String Conceptid, String Conceptdesc, TreeView trloinc)
        {
            trloinc.Nodes.Clear();
            trloinc.Nodes.Add(Conceptdesc);
            DataSet ds = new DataSet();
            try
            {
                if (IsDrug == false)
                {
                    ds = PDataset("select lc.LOINC_NUM,lc.COMPONENT from dbo.Integration_LOINC "
                     + " lc inner join Term_Descriptions td on lc.CONCEPTID = td.CONCEPTID "
                    + " where lc.CONCEPTID=" + Conceptid + " and DESCRIPTIONTYPE = 3");
                }
                else
                {
                    ds = PDataset("select lc.LOINC_NUM,lc.COMPONENT from dbo.Integration_LOINC "
                     + " lc inner join Term_Drugs_Descriptions td on convert(varchar(1000),lc.CONCEPTID) = td.CONCEPTID "
                    + " where lc.CONCEPTID=" + Conceptid + " and DESCRIPTIONTYPE = 3");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode child = new TreeNode();
                        child.Text = dr["LOINC_NUM"].ToString() + " : " + dr["COMPONENT"].ToString();
                        child.Tag = dr["COMPONENT"].ToString();
                        child.Collapse();
                        trloinc.Nodes[0].Nodes.Add(child);
                        //FillChild(child);
                    }

                }
                trloinc.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                UpdateLog("Error in Filling Subtype Hierarchy :" + ex.ToString());

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

            }
        }
        public Boolean Fill_snomedDescription(String strsubTypeConceptID, TreeView trSnomed, DataSet dsSnomed)
        {
          
            trSnomed.ImageList = ofrm1.imgList1;
            SqlConnection oConn = new SqlConnection(EMRConnString);
            try
            {

                oConn.Open();
                
                trSnomed.Nodes.Clear();

                if (dsSnomed.Tables["Descriptions"].Rows.Count > 0)
                {
                    trSnomed.Nodes.Add("Descriptions");

                    trSnomed.Nodes[0].ImageIndex = 3;
                    trSnomed.Nodes[0].SelectedImageIndex = 3;
                    trSnomed.Nodes[0].Tag = "1";
                    TreeNode tnParent = new TreeNode();
                    tnParent.Text = "Lang : en-US";

                    foreach (DataRow dr in dsSnomed.Tables["Descriptions"].Rows)
                    {
                        if (dr["TERM"].ToString() != "")
                        {
                            TreeNode tnChild = new TreeNode();
                            tnChild.Text = dr["DESCRIPTIONTYPE"].ToString() + " : " + dr["TERM"].ToString();
                            tnChild.Tag = dr["DESCRIPTIONTYPE"].ToString();
                            tnChild.ImageIndex = 1;
                            tnChild.SelectedImageIndex = 1;
                            tnChild.Expand();
                            tnParent.Nodes.Add(tnChild);
                            tnChild = null;
                        }
                    }
                    trSnomed.Nodes[0].Nodes.Add(tnParent);
                    tnParent = null;
                }

                

                if (dsSnomed.Tables["Definition"].Rows.Count > 0)
                {
                    string[] strHeader = null;
                   
                    string[] strDefination = null;
                 

                    if (Convert.ToString(dsSnomed.Tables["Definition"].Rows[0][0]) != "")
                    {
                        TreeNode trDefinition = new TreeNode();
                        trDefinition.Text = "Definition : Fully Defined as...";
                        trDefinition.Tag = "2";
                        trDefinition.ImageIndex = 2;
                        trDefinition.SelectedImageIndex = 2;
                        strHeader = dsSnomed.Tables["Definition"].Rows[0][0].ToString().Split(new string[] { "|" }, StringSplitOptions.None);

                        if (strHeader.Length > 0)
                        {
                            myTreeNode oParenetNode1;
                            oParenetNode1 = new myTreeNode();

                            for (int i = 0; i <= strHeader.Length - 1; i++)
                            {
                                strDefination = strHeader.GetValue(i).ToString().Split(new string[] { ":" }, StringSplitOptions.None);
                                if (strDefination.Length > 0)
                                {
                                    myTreeNode oIsNode = new myTreeNode();
                                    myTreeNode oDescr = new myTreeNode();

                                    //oIsNode = new myTreeNode();
                                    oIsNode.Text = strDefination.GetValue(0).ToString();
                                    oIsNode.ImageIndex = 0;
                                    oIsNode.SelectedImageIndex = 0;
                                    trDefinition.Nodes.Add(oIsNode);
                                  
                                    //oDescr = new myTreeNode();
                                    oDescr.Text = strDefination.GetValue(1).ToString();
                                    oDescr.ImageIndex = 1;
                                    oDescr.SelectedImageIndex = 1;
                                    oIsNode.Nodes.Add(oDescr);
                                    oIsNode = null;
                                    oDescr = null;
                                }
                            }
                        }
                        strHeader = null;
                        strDefination = null;
                        trSnomed.Nodes.Add(trDefinition);
                        trDefinition = null;
                    }
                }

                //if (dsSnomed.Tables["Qualifiers"].Rows.Count > 0)
                //{
                //    TreeNode trQualifiers = new TreeNode();
                //    trQualifiers.Text = "Qualifiers";
                //    trQualifiers.Tag = "3";
                //    trQualifiers.ImageIndex = 7;
                //    trQualifiers.SelectedImageIndex = 7;
                //    for (int i = 0; i <= dsSnomed.Tables["Qualifiers"].Rows.Count - 1; i++)
                //    {
                //        if (dsSnomed.Tables["Qualifiers"].Rows[i][0].ToString() != "")
                //        {
                //            TreeNode tnChild = new TreeNode();


                //            if (dsSnomed.Tables["Qualifiers"].Rows[i][1].ToString() != null)
                //            {
                //                tnChild.Text = dsSnomed.Tables["Qualifiers"].Rows[i][0].ToString();
                //                tnChild.ImageIndex = 0;
                //                tnChild.SelectedImageIndex = 0;
                //            }
                //            tnChild.Nodes.Add(dsSnomed.Tables["Qualifiers"].Rows[i][1].ToString());
                //            tnChild.Nodes[0].ImageIndex = 1;
                //            tnChild.Nodes[0].SelectedImageIndex = 1;

                //            trQualifiers.Nodes.Add(tnChild);
                //            tnChild = null;
                //        }
                //    }
                //    trSnomed.Nodes.Add(trQualifiers);
                //    trQualifiers = null;
                //}

                //Start 20121119
                
                if (dsSnomed.Tables["SnomedCodes"].Rows.Count > 0)
                {
                    DataView dvTree = dsSnomed.Tables["SnomedCodes"].DefaultView;
                    DataTable dtTerm =  dvTree.ToTable(true, new string[] { "SNOMEDID","CTV3ID" });
                    if (dtTerm != null)
                    {
                        if (dtTerm.Rows.Count > 0)
                        {
                            dvTree = dtTerm.DefaultView;
                        }

                    }
                    if (dvTree.ToTable().Rows.Count > 0)
                    {
                        TreeNode trCodes = new TreeNode();
                        trCodes.Text = "Codes";
                        trCodes.Tag = "4";
                        trCodes.ImageIndex = 8;
                        trCodes.SelectedImageIndex = 8;
                        foreach (DataRow dr in dvTree.ToTable().Rows)
                        {
                            if (dr["SNOMEDID"].ToString() != "")
                            {
                                TreeNode tnChild = new TreeNode();
                                tnChild.Text = "Original SNOMED Id : " + dr["SNOMEDID"].ToString();
                                tnChild.ImageIndex = 0;
                                tnChild.SelectedImageIndex = 0;
                                trCodes.Nodes.Add(tnChild);
                                tnChild = null;
                                tnChild = new TreeNode();
                                tnChild.ImageIndex = 0;
                                tnChild.SelectedImageIndex = 0;
                                tnChild.Text = "Read Code (Ctv3Id) : " + dr["CTV3ID"].ToString();
                                trCodes.Nodes.Add(tnChild);
                                tnChild = null;
                            }
                        }
                        trSnomed.Nodes.Add(trCodes);
                        trCodes = null;
                    }
                    if (dvTree != null)
                    {
                        dvTree.Dispose();
                    dvTree = null;
                    }
                    if (dtTerm != null)
                    {
                        dtTerm.Dispose();
                        dtTerm = null;
                    }
                    if (dsSnomed  != null)
                    {
                        dsSnomed.Dispose();
                        dsSnomed = null;
                    }
                    
                }
               
                //End 20121119

               

            }
            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Description :" + ex.ToString());
                return false;
            }
            finally
            {
                trSnomed.ExpandAll();
                if (oConn.State == System.Data.ConnectionState.Open)
                {
                    oConn.Close();
                    oConn.Dispose();
                    oConn = null;
                }
            }

            return true;

        }

        public DataTable Fill_RXNorm_NDC(String _ConceptID,String _ConnectionString,String _RxNormServer,String _RxNormDB,String _EMRDB,String _EMRServername)
        {
            SqlConnection _con=null;
            DataTable dt = new DataTable();
            SqlDataAdapter sdap = null; ;
            try
            {
                _con = new SqlConnection(_ConnectionString);
               
               
                
                
                string strQry = "select ConceptID,RxNorm,NDCCode,DrugName from " +
               " (select distinct SCUI as ConceptID,r.RXCUI as RxNorm,rt.ATV as NDCCode,str as DrugName from [" +
                _RxNormServer.Trim() + "].[" + _RxNormDB + "].dbo.RXNCONSO r inner join [" + _RxNormDB + "].dbo.rxnsat rt on rt.RXCUI =r.RXCUI" +
               " where r.sab like 'snomedct' and r.TTY ='PT' and r.SCUI ='" + _ConceptID + "') tmp " +
               " where NDCCode in (select distinct sndccode from [" + _EMRServername + "].[" + _EMRDB + "].dbo.drugs_mst)";
                sdap = new SqlDataAdapter(strQry, _con);



                sdap.Fill(dt);
            }
            catch (Exception ex)
            {
                UpdateLog("Error while Filling RXNorm_NDC :" + ex.ToString());
                return null ;
            }
            finally
            {
                if (_con.State == System.Data.ConnectionState.Open)
                {
                    _con.Close();
                    _con.Dispose();
                    _con = null;
                }
                if (sdap != null)
                {
                    sdap.Dispose();
                    sdap = null;
                }
               
                
            }
            return dt;
        }


        public String Fill_ICD9(String ICD9CodenDescription)
        {
            string ICD9Description = "";
            SqlConnection oConn = null;
            SqlCommand _sqlcmd =null;
            try
            {
                oConn = new SqlConnection(EMRConnString);
                oConn.Open();
                _sqlcmd = new SqlCommand();
                _sqlcmd = new SqlCommand("Concept_FillICD9", oConn);
                _sqlcmd.CommandType = CommandType.StoredProcedure;

                _sqlcmd.Parameters.Add("@ICD9", SqlDbType.NVarChar).Value = ICD9CodenDescription;
                _sqlcmd.Parameters.Add("@IsReturnCode", SqlDbType.Bit).Value = 1;


                Object result = _sqlcmd.ExecuteScalar();
                if (result != null)
                {
                    ICD9Description = result.ToString();
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Error in Filling Description ID :" + ex.ToString());
            }
            finally
            {
                if (oConn.State == System.Data.ConnectionState.Open)
                {
                    oConn.Close();
                    oConn.Dispose();
                    oConn = null;

                }
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }

            }
            return ICD9Description;
        }
        public String Fill_ICD9Description(String ICD9CodenDescription)
        {
            string ICD9Description = "";
            SqlCommand _sqlcmd =null;
            SqlConnection oConn = new SqlConnection(EMRConnString);
            try
            {
               
                oConn.Open();
                _sqlcmd = new SqlCommand();
                _sqlcmd = new SqlCommand("History_FillICD9Description", oConn);
                _sqlcmd.CommandType = CommandType.StoredProcedure;

                _sqlcmd.Parameters.Add("@ICD9", SqlDbType.NVarChar).Value = ICD9CodenDescription;
                                
                Object result = _sqlcmd.ExecuteScalar();
                oConn.Close();
                if (result != null)
                {
                    ICD9Description = result.ToString();
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Error in Filling Description ID :" + ex.ToString());
            }
            finally
            {
                if (oConn !=null)
                {
                    
                    oConn.Dispose();
                    oConn = null;
                }
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }
            }
            return ICD9Description;
        }

        #region "CORE Problem Set"
        public Boolean SearchCORESnomed(String SearchText, TreeView trFindings, String SearchBy)
        {
          
            trFindings.ImageList = ofrm1.imgList1;
            DataTable dbResult = new DataTable();
            DataSet dsResult = new DataSet();
            try
            {

                
               
                trFindings.Nodes.Clear();
                if (SearchBy == "ConceptID")
                {
                    dbResult = GetCORESnomedData(SearchText);
                    if (dbResult != null)
                    {
                        for (int i = 0; i < dbResult.Rows.Count; i++)
                        {
                            TreeNode oNode = new TreeNode();
                            oNode.Text = dbResult.Rows[i][0].ToString();
                            oNode.Tag = dbResult.Rows[i][1].ToString();
                            oNode.Name = dbResult.Rows[i][0].ToString();
                            oNode.ImageIndex = 0;
                            oNode.SelectedImageIndex = 0;
                            trFindings.Nodes.Add(oNode);
                            oNode = null;

                        }
                    }


                }
                else if (SearchBy == "ICD9" || SearchBy == "ICD10")
                {
                    dsResult = Fill_CORESearchICD(SearchText, SearchBy, "");
                    if (dsResult != null)
                    {
                        if (dsResult.Tables.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {

                                TreeNode oNode = new TreeNode();

                                oNode.Text = dsResult.Tables[0].Rows[i][0].ToString();
                                oNode.Name = dsResult.Tables[0].Rows[i][0].ToString();
                                oNode.ImageIndex = 0;
                                oNode.SelectedImageIndex = 0;
                                trFindings.Nodes.Add(oNode);


                                TreeNode oChildNode = new TreeNode();
                                oChildNode.Text = "ICDTempNode";
                                oChildNode.Tag = "ICDTempNode9999*";
                                oNode.Nodes.Add(oChildNode);
                                oNode = null;
                                oChildNode = null;



                            }
                        }
                       
                    }

                }

                dbResult.Dispose();
                dbResult = null;
                dsResult.Dispose();
                dsResult = null;
                return true;

            }
            catch (Exception ex)
            {
                UpdateLog("Error in Searching Snomed Data :" + ex.ToString());
                return false;
            }
            finally
            {
                

            }
           
        }

        public DataTable GetCORESnomedData(String SearchText)
        {
            SqlConnection _con = new SqlConnection(EMRConnString );
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                DataTable dt = new DataTable();

                _con.Open();

                _sqlcmd = new SqlCommand("gsp_SearchCOREConceptID", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;

                _sqlcmd.Parameters.Add("@tempstring", SqlDbType.NVarChar).Value = SearchText;
                _sqlda.SelectCommand = _sqlcmd;
                _sqlda.Fill(dt);
                _con.Close();
                return dt;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while searching Snomed String :" + ex.ToString());
                return null;
            }
            finally
            {
                _con.Dispose();
                _con = null;
                _sqlda.Dispose();
                _sqlda = null;

                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }
            }

        }

       
        public DataSet  GetCOREICDData(String SearchText,String ICdCode,String ICDSearch="")
        {
            SqlConnection _con = new SqlConnection(EMRConnString);
            SqlCommand _sqlcmd=null;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                DataSet  ds = new DataSet ();
                _con.Open();
                _sqlcmd = new SqlCommand("gsp_FillCOREICD", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = SearchText;
                
                if (ICDSearch == "ICD9")
                {
                    _sqlcmd.Parameters.Add("@ICD9Code", SqlDbType.NVarChar).Value = ICdCode;
                }
                else
                {
                    _sqlcmd.Parameters.Add("@ICD10Code", SqlDbType.NVarChar).Value = ICdCode;
                }
                
                _sqlda.SelectCommand = _sqlcmd;
                _sqlda.Fill(ds);
               ds.Tables[0].TableName = "ICD9";
                ds.Tables[1].TableName = "ICD10";
                //ds.Tables[0].TableName = "ICD9";
                ds.Tables[2].TableName = "RxNormNDC";
                _sqlcmd.Dispose();
                _con.Close();
                return ds;

            }

            catch (Exception ex)
            {
                UpdateLog("Error while searching Snomed String :" + ex.ToString());
                return null;
            }
            finally
            {
                _con.Dispose();
                _con = null;
                _sqlda.Dispose(); 
                _sqlda = null;
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }
            }

        }

        public DataSet Fill_CORESearchICD(String SearchText, String _SearchBy, string _ICDCode)
        {
            SqlConnection _con = new SqlConnection(EMRConnString);
            SqlCommand _sqlcmd=null ;
            SqlDataAdapter _sqlda = new SqlDataAdapter();
            try
            {
                // DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                _con.Open();
                _sqlcmd = new SqlCommand("gsp_SearchCOREICD", _con);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.Add("@SearchText", SqlDbType.NVarChar).Value = SearchText;
                _sqlcmd.Parameters.Add("@SearchBy", SqlDbType.NVarChar).Value = _SearchBy;
                _sqlcmd.Parameters.Add("@ICDCode", SqlDbType.NVarChar).Value = _ICDCode;
                _sqlda.SelectCommand = _sqlcmd;
                _sqlda.Fill(ds);
                _con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                UpdateLog("Error while Filling Snomed Details :" + ex.ToString());
                return null;
            }
            finally
            {
                _con.Dispose();
                _con = null;
                _sqlda.Dispose();
                _sqlda = null;
                if (_sqlcmd != null)
                {
                    _sqlcmd.Parameters.Clear();
                    _sqlcmd.Dispose();
                    _sqlcmd = null;
                }
            }

        }
        #endregion
    }


}
