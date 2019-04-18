using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    /// <summary>
    /// Anil 20071229
    /// Form for Associating CPT with different ICD9's and Modifier's
    /// </summary>
    internal partial class frmSetupAssociation : Form
    {
        #region Constructors
       
        /// <summary>
        /// Constructors
        /// </summary>
        public frmSetupAssociation()
        {
            InitializeComponent();
        }

        public frmSetupAssociation(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            #region " Retrieve MessageBoxCaption from AppSettings "
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
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
         
        #endregion

        #region Private Variables
        //Private variables
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;

        #endregion

        #region Properties
         
        
         //Property for connection string
         public string DatabaseConnectionString
         {
             get { return _databaseconnectionstring; }
             set { _databaseconnectionstring = value; }
         }

         #endregion


        /// <summary>
        /// Form Load 
        /// Fill TreeViews for CPT ICD9 and Modifier
        /// </summary>
         /// <param name="sender">object</param>
         /// <param name="e">EventArgs</param>
         private void frmSetupAssociation_Load(object sender, EventArgs e)
         {
             try
             {
                 gloGeneralNode.gloGeneralNode myTreeNode;
                 //Set root node for Association treeview
                 myTreeNode = new gloGeneralNode.gloGeneralNode("CPT Association", -1);
                 myTreeNode.ImageIndex = 2;
                 myTreeNode.SelectedImageIndex = 2;
                 trvAssociation.Nodes.Add(myTreeNode);

                 //Fill treeview for CPT
                 FillCPTTree();

                 //Fill treeview for ICD9
                 FillICD9Tree();
             }
             catch (gloDatabaseLayer.DBException ex)
             {
                 ex.ERROR_Log(ex.ToString());
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
             }
         }


       
        /// <summary>
        /// Fill all CPT's in a TreeView 
        /// </summary>
        public void FillCPTTree()
        {
            try
            {
                //Declare treenode
                gloGeneralNode.gloGeneralNode myTreeNode;
                //Set root node for CPT treeview
                myTreeNode = new gloGeneralNode.gloGeneralNode("CPT", -1);
                //myTreeNode.Text = "CPT";
                myTreeNode.ImageIndex = 1;
                myTreeNode.SelectedImageIndex = 1;
                //Add rootnode
                trvCPT.Nodes.Add(myTreeNode);

                //Declare object of a class to get all CPT's in a Datatable from database
                DataTable dtCPT = new DataTable();
                BillingAssociation oBA = new BillingAssociation(_databaseconnectionstring);
                //Function to get CPT's from database
                dtCPT = oBA.GetCPTs();
                //Check datatable is empty  or not
                if (dtCPT != null)
                {
                    Int32 i;
                    for (i = 0; i <= dtCPT.Rows.Count - 1; i++)
                    {
                        //Declare treenode
                        gloGeneralNode.gloGeneralNode myNode;
                        //Get the value of the node
                        myNode = new gloGeneralNode.gloGeneralNode(dtCPT.Rows[i][1].ToString(), dtCPT.Rows[i][2].ToString(), Convert.ToInt64(dtCPT.Rows[i][0].ToString()));
                        //Set the node value in a treeview
                        trvCPT.Nodes[0].Nodes.Add(myNode);
                    }
                    //Select the rootnode
                    trvCPT.SelectedNode = trvCPT.Nodes[0];
                    //expand the treeview
                    trvCPT.ExpandAll();
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Fill all ICD9s in a TreeView 
        /// </summary>
        public void FillICD9Tree()
        {
            try
            {
                //declare tree node
                gloGeneralNode.gloGeneralNode myTreeNode;
                //Set root node for ICD9 treeview
                myTreeNode = new gloGeneralNode.gloGeneralNode("ICD9", -1);
                myTreeNode.ImageIndex = 1;
                myTreeNode.SelectedImageIndex = 1;
                //add node as root node
                trvAssociates.Nodes.Add(myTreeNode);

                //Declare object of a class to get all ICD9's in a Datatable from database
                DataTable dtICD9 = new DataTable();
                BillingAssociation oBA = new BillingAssociation(_databaseconnectionstring);
                //Function to get ICD9s from database
                dtICD9 = oBA.GetICD9s();
                if (dtICD9 != null)
                {
                    Int32 i;
                    for (i = 0; i <= dtICD9.Rows.Count - 1; i++)
                    {
                        gloGeneralNode.gloGeneralNode myNode;

                        //Get the value of a node
                        myNode = new gloGeneralNode.gloGeneralNode(dtICD9.Rows[i][1].ToString(), dtICD9.Rows[i][2].ToString(), Convert.ToInt64(dtICD9.Rows[i][0].ToString()));
                        //Add node in a tree
                        trvAssociates.Nodes[0].Nodes.Add(myNode);
                    }
                    //Select the root node
                    trvAssociates.SelectedNode = trvCPT.Nodes[0];
                    //Expand the treeview
                    trvAssociates.ExpandAll();
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Fill all Modifiers in a TreeView 
        /// </summary>
        public void FillModifierTree()
        {
            try
            {
                //declare tree node
                gloGeneralNode.gloGeneralNode myTreeNode;
                //Set root node for Modifier treeview
                myTreeNode = new gloGeneralNode.gloGeneralNode("Modifier", -1);
                myTreeNode.ImageIndex = 1;
                myTreeNode.SelectedImageIndex = 1;
                //add node as root node
                trvAssociates.Nodes.Add(myTreeNode);

                //Declare object of a class to get all Modifiers's in a Datatable from database
                DataTable dtModifier = new DataTable();
                BillingAssociation oBA = new BillingAssociation(_databaseconnectionstring);
                //Function to get Modifiers from database
                dtModifier = oBA.GetModifiers();
                if (dtModifier != null)
                {
                    Int32 i;
                    for (i = 0; i <= dtModifier.Rows.Count - 1; i++)
                    {
                        gloGeneralNode.gloGeneralNode myNode;
                        //Get the value of a node
                        myNode = new gloGeneralNode.gloGeneralNode(dtModifier.Rows[i][1].ToString(), dtModifier.Rows[i][2].ToString(), Convert.ToInt64(dtModifier.Rows[i][0].ToString()));
                        //Add node in a tree
                        trvAssociates.Nodes[0].Nodes.Add(myNode);
                    }
                    //Select the root node
                    trvAssociates.SelectedNode = trvCPT.Nodes[0];
                    //Expand the treeview
                    trvAssociates.ExpandAll();
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        

        /// <summary>
        /// Function to add Node to the Association treeview from CPT treeview
        /// </summary>
        /// <param name="myNode">gloGeneralNode.gloGeneralNode</param>
        public void AddNode(gloGeneralNode.gloGeneralNode myNode)
        {
            try
            {
                //Check which node is selected, it should not be a root node.
                if (myNode.Parent == trvCPT.Nodes[0])
                {
                    String str = "";
                    str = myNode.KEY.ToString();

                    //declare a treenode object
                    gloGeneralNode.gloGeneralNode myAssociateNode;
                    //assign the values of selected node to a new node object 
                    myAssociateNode = (gloGeneralNode.gloGeneralNode)myNode.Clone();
                    myAssociateNode.KEY = myNode.KEY;
                    myAssociateNode.Text = myNode.Text;
                    myAssociateNode.ImageIndex = 1;
                    myAssociateNode.SelectedImageIndex = 1;

                    //Add this new node to a treeview
                    trvAssociation.Nodes[0].Nodes.Add(myAssociateNode);

                    //declare a treenode object as a child node to trvAssociation root node
                    gloGeneralNode.gloGeneralNode MyChild;

                    //assign values to a treenode
                    MyChild = new gloGeneralNode.gloGeneralNode();
                    MyChild.Text = "ICD9";
                    MyChild.KEY = -1;
                    MyChild.ImageIndex = 2;
                    MyChild.SelectedImageIndex = 2;
                    //add node to a treeview
                    myAssociateNode.Nodes.Add(MyChild);

                    //add another node to a treeview
                    MyChild = new gloGeneralNode.gloGeneralNode();
                    MyChild.Text = "Modifier";
                    MyChild.KEY = -1;
                    MyChild.ImageIndex = 2;
                    MyChild.SelectedImageIndex = 2;
                    myAssociateNode.Nodes.Add(MyChild);


                    //Declare a datatable
                    DataTable dt = new DataTable();
                    //declare object of a class to retrieve data from database
                    BillingAssociation oBA = new BillingAssociation(_databaseconnectionstring);
                    //Function to Fetch ICD9 from database
                    dt = oBA.FetchICD9(Convert.ToInt64(myNode.Tag));
                    Int32 i;
                    //Add nodes to a treeview
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        gloGeneralNode.gloGeneralNode myTreenode;
                        myTreenode = new gloGeneralNode.gloGeneralNode(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), Convert.ToInt64(dt.Rows[i][0].ToString()));
                        myAssociateNode.Nodes[0].Nodes.Add(myTreenode);
                    }
                    //expand a treeview
                    trvAssociation.ExpandAll();
                    trvAssociation.Select();
                    myAssociateNode.EnsureVisible();
                    trvAssociation.SelectedNode = myAssociateNode.Nodes[0];
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// On double clicking the nodes of a CPT treeview
        /// add that node to a Association treeview
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void trvCPT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                gloGeneralNode.gloGeneralNode mynode;
                mynode = (gloGeneralNode.gloGeneralNode)trvCPT.SelectedNode;
                if (mynode != null)
                {
                    AddNode(mynode);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// On double clicking the node at a Associate treeview
        /// Add that node to a Association treeview
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void trvAssociates_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //Declare node to be added
                gloGeneralNode.gloGeneralNode mynode;//=new gloGeneralNode.gloGeneralNode();

                //declare node where to be added
                gloGeneralNode.gloGeneralNode targetnode;// = new gloGeneralNode.gloGeneralNode(); 
                //target node where a node to be added
                targetnode = (gloGeneralNode.gloGeneralNode)trvAssociation.SelectedNode;
                //node which is to be added to a target node
                mynode = (gloGeneralNode.gloGeneralNode)trvAssociates.SelectedNode;

                //check whether node to be added is null or not
                if (mynode != null)
                {
                    //Call Function to add node
                    AddAssociates(mynode,targetnode);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Function to add node to a ICD9 or Modifier treenodes of a Association treeview
        /// The target node and a node to be added is passed to the function.
        /// </summary>
        /// <param name="mynode">(gloGeneralNode.gloGeneralNode)Node to be added</param>
        /// <param name="targetnode">(gloGeneralNode.gloGeneralNode)Target node</param>
        public void AddAssociates(gloGeneralNode.gloGeneralNode mynode, gloGeneralNode.gloGeneralNode targetnode)
        { 
            try
            {
                //check that the target node and node to be added are not root nodes 
                if (mynode != trvAssociates.Nodes[0] && targetnode != trvAssociation.Nodes[0])
                {
                    //declare a tree node object
                    gloGeneralNode.gloGeneralNode mytreenode = new gloGeneralNode.gloGeneralNode();

                    //check whether target node is correct or not
                    if (targetnode == trvAssociation.Nodes[0].Nodes[0].Nodes[0] || targetnode.KEY == -1)
                    {
                        if (targetnode.Parent == trvAssociation.Nodes[0])
                            mytreenode = targetnode;
                        else
                            mytreenode = (gloGeneralNode.gloGeneralNode)targetnode.Parent;
                    }

                    String str = "";
                    str = mynode.KEY.ToString();
                    //Declare a treenode
                    gloGeneralNode.gloGeneralNode myAssociate;

                    //clone the node and assign values to new node
                    myAssociate = (gloGeneralNode.gloGeneralNode)mynode.Clone();
                    myAssociate.KEY = mynode.KEY;
                    myAssociate.Text = mynode.Text;
                    //Add a new node to a target node
                    mytreenode.Nodes[0].Nodes.Add(myAssociate);
                    mynode.EnsureVisible();
                    trvAssociation.ExpandAll();
                    trvAssociation.SelectedNode = mynode;
                }
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

      

       
    }
}