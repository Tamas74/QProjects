using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Windows;
using gloUserControlLibrary;

namespace gloSurescriptSecureMessage
{
    public partial class frmToList : Form
    {
        #region "Variable Declarations"

        private const int  Col_Select =0;
        private const int  Col_ID = 1;
        private const int  Col_Prefix =2;
        private const int  Col_Fname = 3;
        private const int  Col_Mname =4;
        private const int  Col_Lname =5;
        private const int  Col_Suffix =6;
        private const int  Col_Gender =7;
        private const int  Col_Addr1 =8;
        private const int  Col_Addr2 =9;
        private const int  Col_City =10;
        private const int  Col_State =11;
        private const int  Col_Zip =12;
        private const int  Col_Phone =13;
        private const int  Col_Fax =14;
        private const int  Col_Mobile = 15;
        private const int Col_Email = 16;
        private const int Col_NPI = 17;
        private const int Col_DirectAddress = 18;
        private const int Col_SPI = 19;
        private const int Col_SType1 = 20;
        private const int Col_ClinicName = 21;


        private long _patientID = 0;
        public long PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }       

        private string _DatabaseConnection = "";
        public string DatabaseConnection
        {
            get { return _DatabaseConnection; }
            set { _DatabaseConnection = value; }
        }


        private DataTable _dtTable;

        public DataTable dtTable
        {
            get { return _dtTable; }
            set { _dtTable = value; }
        }

        public bool flagIsChangePatient 
        { get; set; }


        public bool flagIsToListOrAttachmentExists
        { get; set; }

        public bool flagIsBtnPatientListVisible
        { get; set; }

        #endregion

        #region "Constructor"

        public frmToList()
        {
            InitializeComponent();
        }    

       
        private Boolean bParentTrigger = true;

        private Boolean bChildTrigger = true;        

        DateTime _CurrentTime;

        DataView _dvTimer = null;
        DataView _dvOwnAddress = null;
        DataView _dvOtherAddress = null;

        #endregion

        #region "Form Load"

        private void frmToList_Load(object sender, EventArgs e)
        {

            myTreeNode rootnode = null;
            myTreeNode rootnode1 = null;
            myTreeNode rootnode2 = null;
            myTreeNode rootnode3 = null;
            myTreeNode rootnode4 = null;

            try
            {

                if (flagIsBtnPatientListVisible == false)
                {
                    btnPatientList.Visible = false;
                }
                else
                {
                    btnPatientList.Visible = true;
                }

                rootnode = default(myTreeNode);
                rootnode = new myTreeNode("Patient's Providers", -1);
                rootnode.ImageIndex = 0;
                rootnode.SelectedImageIndex = 0;
                trvRefferals.Nodes.Add(rootnode);
                rootnode = null;

                rootnode1 = default(myTreeNode);
                rootnode1 = new myTreeNode("Primary Care Physician", 0);
                rootnode1.ImageIndex = 1;
                rootnode1.SelectedImageIndex = 1;
                trvRefferals.Nodes[0].Nodes.Add(rootnode1);
                rootnode1 = null;

                rootnode2 = default(myTreeNode);
                rootnode2 = new myTreeNode("Referrals", 1);
                rootnode2.ImageIndex = 2;
                rootnode2.SelectedImageIndex = 2;
                trvRefferals.Nodes[0].Nodes.Add(rootnode2);
                rootnode2 = null;

                rootnode3 = default(myTreeNode);
                rootnode3 = new myTreeNode("Other Care Team", 2);
                rootnode3.ImageIndex = 3;
                rootnode3.SelectedImageIndex = 3;
                trvRefferals.Nodes[0].Nodes.Add(rootnode3);
                rootnode3 = null;

                rootnode4 = default(myTreeNode);
                rootnode4 = new myTreeNode("Order & Result Referrals", 3);
                rootnode4.ImageIndex = 4;
                rootnode4.SelectedImageIndex = 4;
                trvRefferals.Nodes[0].Nodes.Add(rootnode4);
                rootnode4 = null;

                if (PatientID > 0)
                {
                    LoadTreeview(PatientID);
                }

                trvRefferals.ExpandAll();

                //Search logic changed to resolve Bug #88949: surescript Catalog search
                FillOwnAddress("");
                FillOtherAddress("");

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);               
            }
            finally
            {
                if (rootnode4 != null)
                {
                    rootnode4 = null;
                }
                if (rootnode3 != null)
                {
                    rootnode3 = null;
                }
                if (rootnode2 != null)
                {
                    rootnode2 = null;
                }
                if (rootnode1 != null)
                {
                    rootnode1 = null;
                }
                if (rootnode != null)
                {
                    rootnode = null;
                }
               
            }
           
        }

        #endregion

        #region "Public and Private Functions"

        private void LoadTreeview(long PatientID)
        {
            FillReferrals(PatientID);
            FillOrderResult(PatientID);
        }


        private void FillOwnAddress(string sSearchText)
        {
            //Added parameter sSearchText to resolve Bug #88949: surescript Catalog search - Added parameter

            clsSecureMessageDB clsDb = null;
            DataTable dtOwnAddress = null;

            try
            {
                _dvOwnAddress = new DataView();

                c1OwnAddress.Cols.Count = 22;
                c1OwnAddress.Rows.Count = 1;
                c1OwnAddress.Rows.Fixed = 1;

                //06-Mar-15 Aniket: Resolving Bug #80191: gloEMR: Provider direct message- Application gives exception
                c1OwnAddress.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                clsDb = new clsSecureMessageDB();
                dtOwnAddress = clsDb.GetOwnAddress(sSearchText);
                _dvOwnAddress = dtOwnAddress.DefaultView;
                c1OwnAddress.DataSource = _dvOwnAddress;

                FillGrid(c1OwnAddress);
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (dtOwnAddress != null) { dtOwnAddress.Dispose(); dtOwnAddress = null; }
                if (clsDb != null) { clsDb.Dispose(); clsDb = null; }
            }
        }

        private void FilterOwnAddress(string sSearchText)
        {
            //Added parameter sSearchText to resolve Bug #88949: surescript Catalog search - Added parameter
            clsSecureMessageDB clsDb = null;
            DataTable dtOwnAddress = null;
            try
            {
                _dvOwnAddress = new DataView();
                //06-Mar-15 Aniket: Resolving Bug #80191: gloEMR: Provider direct message- Application gives exception
                c1OwnAddress.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                clsDb = new clsSecureMessageDB();
                dtOwnAddress = clsDb.GetOwnAddress(sSearchText);
                _dvOwnAddress = dtOwnAddress.DefaultView;
                c1OwnAddress.DataSource = _dvOwnAddress;
                FillGrid(c1OwnAddress);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (dtOwnAddress != null) { dtOwnAddress.Dispose(); dtOwnAddress = null; }
                if (clsDb != null) { clsDb.Dispose(); clsDb = null; }
            }


        }


        private void FillOtherAddress(string sSearchText)
        {
            //Added parameter sSearchText to resolve Bug #88949: surescript Catalog search - Added parameter

            clsSecureMessageDB clsDb = null;
            DataTable dtOtherAddress = null;

            try
            {
                _dvOtherAddress = new DataView();
                c1OtherAddress.Cols.Count = 22;
                c1OtherAddress.Rows.Count = 1;
                c1OtherAddress.Rows.Fixed = 1;

                //06-Mar-15 Aniket: Resolving Bug #80191: gloEMR: Provider direct message- Application gives exception
                c1OtherAddress.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                clsDb = new clsSecureMessageDB();
                dtOtherAddress = clsDb.GetOtherAddressList(sSearchText);
                _dvOtherAddress = dtOtherAddress.DefaultView;
                c1OtherAddress.DataSource = _dvOtherAddress;
                FillGrid(c1OtherAddress);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (dtOtherAddress != null) { dtOtherAddress.Dispose(); dtOtherAddress = null; }
                if (clsDb != null) { clsDb.Dispose(); clsDb = null; }
            }
        }

        //Added to Resolve Bug #88949: surescript Catalog search
        private void FilterOtherAddress(string sSearchText)
        {
            clsSecureMessageDB clsDb = null;
            DataTable dtOtherAddress = null;
            try
            {
                this.picProgress.Visible = true;
                System.Windows.Forms.Application.DoEvents();

                _dvOtherAddress = new DataView();

                //06-Mar-15 Aniket: Resolving Bug #80191: gloEMR: Provider direct message- Application gives exception
                c1OtherAddress.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                clsDb = new clsSecureMessageDB();

                dtOtherAddress = clsDb.GetOtherAddressList(sSearchText);
                _dvOtherAddress = dtOtherAddress.DefaultView;
                c1OtherAddress.DataSource = _dvOtherAddress;

                FillGrid(c1OtherAddress);
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                this.picProgress.Visible = false;
                if (dtOtherAddress != null) { dtOtherAddress.Dispose(); dtOtherAddress = null; }
                if (clsDb != null) { clsDb.Dispose(); clsDb = null; }
            }
        }

        private void FillGrid(C1.Win.C1FlexGrid.C1FlexGrid c1Address)
        {
                   
            c1Address.Cols[Col_Select].Visible = true;
            c1Address.Cols[Col_Select].DataType = Type.GetType("System.Boolean");
            c1Address.Cols[Col_Select].AllowEditing = true;
          

            c1Address.Cols[Col_ID].Visible = false;
            c1Address.Cols[Col_ID].AllowEditing = false;

            c1Address.Cols[Col_Prefix].Visible = true;
            c1Address.Cols[Col_Prefix].AllowEditing = false;

            c1Address.Cols[Col_Fname].Visible = true;
            c1Address.Cols[Col_Fname].AllowEditing = false;

            c1Address.Cols[Col_Mname].Visible = true;
            c1Address.Cols[Col_Mname].AllowEditing = false;

            c1Address.Cols[Col_Lname].Visible = true;
            c1Address.Cols[Col_Lname].AllowEditing = false;

            c1Address.Cols[Col_Suffix].Visible = true;
            c1Address.Cols[Col_Suffix].AllowEditing = false;

            c1Address.Cols[Col_Gender].Visible = true;
            c1Address.Cols[Col_Gender].AllowEditing = false;

            c1Address.Cols[Col_Addr1].Visible = true;
            c1Address.Cols[Col_Addr1].AllowEditing = false;

            c1Address.Cols[Col_Addr2].Visible = true;
            c1Address.Cols[Col_Addr2].AllowEditing = false;

            c1Address.Cols[Col_City].Visible = true;
            c1Address.Cols[Col_City].AllowEditing = false;

            c1Address.Cols[Col_State].Visible = true;
            c1Address.Cols[Col_State].AllowEditing = false;

            c1Address.Cols[Col_Zip].Visible = true;
            c1Address.Cols[Col_Zip].AllowEditing = false;

            c1Address.Cols[Col_Phone].Visible = true;
            c1Address.Cols[Col_Phone].AllowEditing = false;

            c1Address.Cols[Col_Fax].Visible = true;
            c1Address.Cols[Col_Fax].AllowEditing = false;

            c1Address.Cols[Col_Mobile].Visible = true;
            c1Address.Cols[Col_Mobile].AllowEditing = false;

            c1Address.Cols[Col_Email].Visible = true;
            c1Address.Cols[Col_Email].AllowEditing = false;
                       
            c1Address.Cols[Col_NPI].Visible = true;
            c1Address.Cols[Col_NPI].AllowEditing = false;

            c1Address.Cols[Col_DirectAddress].Visible = true;
            c1Address.Cols[Col_DirectAddress].AllowEditing = false;

            c1Address.Cols[Col_SPI].Visible = true;
            c1Address.Cols[Col_SPI].AllowEditing = false;

            c1Address.Cols[Col_SType1].Visible = true;
            c1Address.Cols[Col_SType1].AllowEditing = false;            

            c1Address.Cols[Col_ClinicName].Visible = true;
            c1Address.Cols[Col_ClinicName].AllowEditing = false;

            int _width = (750 - 21) / 9;
            c1Address.Cols[Col_Select].Width = _width * 1;
            c1Address.Cols[Col_Prefix].Width = _width * 1;
            c1Address.Cols[Col_Fname].Width = _width * 1;
            c1Address.Cols[Col_Mname].Width = _width * 1;
            c1Address.Cols[Col_Lname].Width = _width * 1;
            c1Address.Cols[Col_Suffix].Width = _width * 1;
            c1Address.Cols[Col_Gender].Width = _width * 1;
            c1Address.Cols[Col_Addr1].Width = _width * 1;
            c1Address.Cols[Col_Addr2].Width = _width * 1;
            c1Address.Cols[Col_City].Width = _width * 1;
            c1Address.Cols[Col_State].Width = _width * 1;
            c1Address.Cols[Col_Zip].Width = _width * 1;
            c1Address.Cols[Col_Phone].Width = _width * 1;
            c1Address.Cols[Col_Fax].Width = _width * 1;
            c1Address.Cols[Col_Mobile].Width = _width * 1;
            c1Address.Cols[Col_Email].Width = _width * 1;
            c1Address.Cols[Col_NPI].Width = _width * 1;
            c1Address.Cols[Col_DirectAddress].Width = _width * 1;
            c1Address.Cols[Col_SPI].Width = _width * 1;
            c1Address.Cols[Col_SType1].Width = _width * 1;
            c1Address.Cols[Col_ClinicName].Width = _width * 1;



            c1Address.Cols[Col_Select].Caption = "Select";
            c1Address.Cols[Col_Prefix].Caption = "Prefix";
            c1Address.Cols[Col_Fname].Caption = "First Name";
            c1Address.Cols[Col_Mname].Caption = "Middle Name";
            c1Address.Cols[Col_Lname].Caption = "Last Name";
            c1Address.Cols[Col_Suffix].Caption = "Suffix";
            c1Address.Cols[Col_Gender].Caption = "Gender";
            c1Address.Cols[Col_Addr1].Caption = "Addr 1";
            c1Address.Cols[Col_Addr2].Caption = "Addr 2";
            c1Address.Cols[Col_City].Caption = "City";
            c1Address.Cols[Col_State].Caption = "State";
            c1Address.Cols[Col_Zip].Caption = "Zip";
            c1Address.Cols[Col_Phone].Caption = "Phone";
            c1Address.Cols[Col_Fax].Caption = "Fax";
            c1Address.Cols[Col_Mobile].Caption = "Mobile";
            c1Address.Cols[Col_Email].Caption = "Email";
            c1Address.Cols[Col_NPI].Caption = "NPI";
            c1Address.Cols[Col_DirectAddress].Caption = "Direct Address";
            c1Address.Cols[Col_SPI].Caption = "SPI";
            c1Address.Cols[Col_SType1].Caption = "Specialty Type";
            c1Address.Cols[Col_ClinicName].Caption = "Clinic Name";

        }      

        public DataTable GetAllPatientReferalsByPatientID(long nPatientID)
        {
           
            clsSecureMessageDB clsDb = null;
            DataTable dtReference = null;

            try
            {
                clsDb = new clsSecureMessageDB();
                dtReference = clsDb.GetAllPatientReferalsByPatientID(nPatientID);
                return dtReference; 
            }

             catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return dtReference;  
            }

            finally
            {

                if (dtReference != null)
                {
                    dtReference.Dispose();
                    dtReference = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }
            }
     
           
        }

        public DataTable GetAllOrderResultByPatientID(long nPatientID)
        {

            clsSecureMessageDB clsDb = null;
            DataTable dtReference = null;

            try
            {
                clsDb = new clsSecureMessageDB();
                dtReference = clsDb.GetAllOrderResultByPatientID(nPatientID);
                return dtReference;
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return dtReference;
            }

            finally
            {

                if (dtReference != null)
                {
                    dtReference.Dispose();
                    dtReference = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }
            }


        }

        private void FillReferrals(long nPatientID)
        {
            DataTable dt = null;
            int i = 0;
            myTreeNode mychildnode = null;
            gloUserControlLibrary.myList myNodeList=null;
            ArrayList arrlist = null;
            
            try
            {
                mychildnode = default(myTreeNode);
                dt = GetAllPatientReferalsByPatientID(nPatientID);

                if ((dt != null))
                {
                    // trvRefferals.Nodes.Clear();
                    trvRefferals.Nodes[0].Nodes[0].Nodes.Clear();
                    trvRefferals.Nodes[0].Nodes[1].Nodes.Clear();
                    trvRefferals.Nodes[0].Nodes[2].Nodes.Clear();
                    trvRefferals.Nodes[0].Nodes[3].Nodes.Clear();

                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        myNodeList = new myList();
                        arrlist = new ArrayList();

                        myNodeList.ContactFirstName = Convert.ToString(dt.Rows[i]["fName"]);
                        myNodeList.ContactLastName = Convert.ToString(dt.Rows[i]["lName"]);
                        myNodeList.NPI = Convert.ToString(dt.Rows[i]["sNPI"]);
                        myNodeList.Email = Convert.ToString(dt.Rows[i]["email"]);
                        myNodeList.FlagType = Convert.ToInt16(dt.Rows[i]["flagType"]);
                        myNodeList.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        arrlist.Add(myNodeList);

                        mychildnode = new myTreeNode();
                        mychildnode.Key = Convert.ToInt64(dt.Rows[i]["ID"]);
                        mychildnode.arrRefferalDetails = arrlist;

                        mychildnode.Text = Convert.ToString(dt.Rows[i]["fName"]) + " " + Convert.ToString(dt.Rows[i]["lName"]);
                        mychildnode.NodeName = Convert.ToString(dt.Rows[i]["fName"]) + " " + Convert.ToString(dt.Rows[i]["lName"]);
                        mychildnode.ImageIndex = 5;
                        mychildnode.SelectedImageIndex = 5;

                        Int16 flagType = 0;
                        flagType = Convert.ToInt16(dt.Rows[i]["flagType"]);

                        if (flagType == 2)
                        {
                            trvRefferals.Nodes[0].Nodes[0].Nodes.Add(mychildnode);
                        }
                        else if (flagType == 3)
                        {
                            trvRefferals.Nodes[0].Nodes[1].Nodes.Add(mychildnode);
                        }
                        else if (flagType == 4)
                        {
                            trvRefferals.Nodes[0].Nodes[2].Nodes.Add(mychildnode);
                        }

                        arrlist = null;
                        myNodeList = null;
                      
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
              
                if (arrlist != null)
                {                    
                    arrlist = null;
                }
                if (myNodeList != null)
                {
                    myNodeList = null;
                }
                if (mychildnode != null)
                {
                    mychildnode = null;
                }
                if (dt != null)
                {
                    dt = null;
                }
            }
           

        }

        private void FillOrderResult(long nPatientID)
        {

            DataTable dt = null;
            int i = 0;
            myTreeNode mychildnode = null;
            gloUserControlLibrary.myList myNodeList = null;
            ArrayList arrlist = null;

            try
            {
                mychildnode = default(myTreeNode);
                dt = GetAllOrderResultByPatientID(nPatientID);

                if ((dt != null))
                {
                    trvRefferals.Nodes[0].Nodes[3].Nodes.Clear();

                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        myNodeList = new myList();
                        arrlist = new ArrayList();


                        myNodeList.ContactFirstName = Convert.ToString(dt.Rows[i]["fName"]);
                        myNodeList.ContactLastName = Convert.ToString(dt.Rows[i]["lName"]);
                        myNodeList.NPI = Convert.ToString(dt.Rows[i]["sNPI"]);
                        myNodeList.Email = Convert.ToString(dt.Rows[i]["email"]);
                        myNodeList.FlagType = 0;
                        myNodeList.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        arrlist.Add(myNodeList);

                        mychildnode = new myTreeNode();
                        mychildnode.Key = Convert.ToInt64(dt.Rows[i]["ID"]);
                        mychildnode.arrRefferalDetails = arrlist;

                        mychildnode.Text = Convert.ToString(dt.Rows[i]["fName"]) + " " + Convert.ToString(dt.Rows[i]["lName"]);
                        mychildnode.NodeName = Convert.ToString(dt.Rows[i]["fName"]) + " " + Convert.ToString(dt.Rows[i]["lName"]);
                        mychildnode.ImageIndex = 5;
                        mychildnode.SelectedImageIndex = 5;

                        trvRefferals.Nodes[0].Nodes[3].Nodes.Add(mychildnode);
                        arrlist = null;
                        myNodeList = null;
                    }
                }

                //  trvRefferals.ExpandAll();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {

                if (arrlist != null)
                {
                    arrlist = null;
                }
                if (myNodeList != null)
                {
                    myNodeList = null;
                }
                if (mychildnode != null)
                {
                    mychildnode = null;
                }
                if (dt != null)
                {
                    dt = null;
                }
            }

        }

        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }

        private void CheckAllChildren(TreeNode tn, Boolean bCheck)
        {
            bParentTrigger = false;
            foreach (TreeNode ctn in tn.Nodes)
            {
                bChildTrigger = false;
                ctn.Checked = bCheck;
                bChildTrigger = true;

                CheckAllChildren(ctn, bCheck);
            }
            bParentTrigger = true;
        }

        private void CheckMyParent(TreeNode tn, Boolean bCheck)
        {
            if (tn == null)
            {
                return;
            }
            if (tn.Parent == null)
            {
                return;
            }

            bChildTrigger = false;
            bParentTrigger = false;

            if (bCheck)
            {
                bool bNodeFound = false;
                foreach (TreeNode _Node in tn.Parent.Nodes)
                {
                    if (_Node.Checked == false)
                    {
                        tn.Parent.Checked = false;
                        bNodeFound = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bNodeFound == false)
                {
                    tn.Parent.Checked = true;
                }
            }
            else
            {
                tn.Parent.Checked = bCheck;
            }

            CheckMyParent(tn.Parent, bCheck);
            bParentTrigger = true;
            bChildTrigger = true;
        }

        private void trvRefferals_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode _Node = new TreeNode();
           // try
           // {
               // _Node = new TreeNode();
                _Node = e.Node;

                if (bChildTrigger == true)
                {
                    CheckAllChildren(e.Node, e.Node.Checked);
                }

                if (bParentTrigger == true)
                {
                    CheckMyParent(e.Node, e.Node.Checked);
                }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);               
            //}
            //finally
            //{
            //    if (_Node != null)
            //    {
            //        _Node = null;
            //    }
            //}
        }

        private void txtOwnSearch_TextChanged(object sender, EventArgs e)
        {
            if (timerOwnAddress.Enabled == false)
            {
                timerOwnAddress.Stop();
                timerOwnAddress.Enabled = true;
            }
            return;
        }

        private void timerOwnAddress_Tick(object sender, EventArgs e)
        {
            if (txtOwnSearch.Text.Trim() != "")
            {
                if (System.DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    timerOwnAddress.Stop();
                    this.Cursor = Cursors.WaitCursor;
                    if (this.Visible == true)
                    {
                        TimerCode_OwnAddress(txtOwnSearch);
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                timerOwnAddress.Stop();
                this.Cursor = Cursors.WaitCursor;
                if (this.Visible == true)
                {
                    TimerCode_OwnAddress(txtOwnSearch);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void TimerCode_OtherAddress(TextBox txtOtherSearch)
        {
            //Added to resolve Bug #88949: surescript Catalog search
            FilterOtherAddress(txtOtherSearch.Text.Trim());
        }

        private void TimerCode_OwnAddress(TextBox txtOwnSearch)
        {
            //Added to resolve Bug #88949: surescript Catalog search
            FilterOwnAddress(txtOwnSearch.Text.Trim());
        }

        //private void TimerCode(TextBox txtOwnSearch, C1.Win.C1FlexGrid.C1FlexGrid c1Address)
        //{
        //    string[] strSearchArray = null;
        //    string sFilter = "";
        //   // _dvTimer = new DataView();
        //    _dvTimer = (DataView)c1Address.DataSource;
        //    c1Address.DataSource = _dvTimer;
        //    if (_dvTimer == null) return;

        //    string strSearch = txtOwnSearch.Text.Trim();

        //    //COMMENTED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
        //    strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

        //    if (strSearch.StartsWith("*") == true)
        //    { strSearch = strSearch.Replace("*", "%"); }

        //    //ADDED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
        //    // strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

        //    strSearch = strSearch.Replace("*", "[*]");

        //    if (strSearch.Length > 1)
        //    {
        //        //string str = strSearch.Substring(1).Replace("%", "");
        //        string str = strSearch.Substring(1);
        //        strSearch = strSearch.Substring(0, 1) + str;
        //    }
        //    if (strSearch.Trim() != "")
        //    {
        //        strSearchArray = strSearch.Split(',');
        //    }


        //    if (strSearch.Trim() != "")
        //    {
        //        if (strSearchArray.Length == 1)
        //        {
        //            strSearch = strSearchArray[0].Trim();




        //            _dvTimer.RowFilter = _dvTimer.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["MiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr2"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Fax"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'  OR " +
        //                            _dvTimer.Table.Columns["DirectAddress"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%'";
        //        }
        //        else
        //        {

        //            //For Comma separated  value search
        //            for (int i = 0; i < strSearchArray.Length; i++)
        //            {
        //                strSearch = strSearchArray[i].Trim();
        //                if (strSearch.Trim() != "")
        //                {
        //                    if (i == 0)
        //                    {
        //                        sFilter = " ( " + _dvTimer.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["MiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr2"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Fax"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'  OR " +
        //                            _dvTimer.Table.Columns["DirectAddress"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%' )";
                               
        //                    }
        //                    else
        //                    {
        //                        if (sFilter != "")
        //                            sFilter = sFilter + " AND ";

        //                        sFilter = sFilter + " (" + _dvTimer.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["MiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Gender"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Addr2"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["City"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["State"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Zip"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Phone"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Fax"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Mobile"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["Email"].ColumnName + " Like '" + strSearch + "%'  OR " +
        //                            _dvTimer.Table.Columns["DirectAddress"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["SpecialtyType1"].ColumnName + " Like '" + strSearch + "%' OR " +
        //                            _dvTimer.Table.Columns["ClinicName"].ColumnName + " Like '" + strSearch + "%' )";
                                
        //                    }

        //                }
        //            }
        //            _dvTimer.RowFilter = sFilter;
        //        }

        //    }
        //    else
        //    {
        //        _dvTimer.RowFilter = "";
        //    }
        //}

        private void txtOwnSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    //..Check if there are rows after the search is done 
                    //..if yes then set focus to the grid else keep in the search text box.
                    if (c1OwnAddress.Rows.Count > 0)
                    { c1OwnAddress.Focus(); }
                    else
                    {
                        txtOwnSearch.SelectAll();
                        txtOwnSearch.Focus();
                    }
                }

                
                if (e.Alt
                    || e.Control
                    || e.KeyCode.Equals(Keys.Left)
                    || e.KeyCode.Equals(Keys.Right)
                    || e.KeyCode.Equals(Keys.Up)
                    || e.KeyCode.Equals(Keys.Down)
                    || e.KeyCode.Equals(Keys.Home)
                    || e.KeyCode.Equals(Keys.End))
                { return; }

                if (IsValidKey(e.KeyValue) == false)
                { return; }

                _CurrentTime = DateTime.Now;
                timerOwnAddress.Stop();
                timerOwnAddress.Interval = 500;
                timerOwnAddress.Enabled = true;
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        private bool IsValidKey(int _KeyValue)
        {
            bool _ValidKey = false;

            if (_KeyValue >= 32 && _KeyValue <= 47)
            { _ValidKey = true; }

            if (_KeyValue >= 48 && _KeyValue <= 57)
            { _ValidKey = true; }

            if ((_KeyValue >= 65 && _KeyValue <= 90) || (_KeyValue >= 97 && _KeyValue <= 122))
            { _ValidKey = true; }

            return _ValidKey;
        }

        private void txtOtherSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    //..Check if there are rows after the search is done 
                    //..if yes then set focus to the grid else keep in the search text box.
                    if (c1OtherAddress.Rows.Count > 0)
                    { c1OtherAddress.Focus(); }
                    else
                    {
                        txtOtherSearch.SelectAll();
                        txtOtherSearch.Focus();
                    }
                }


                if (e.Alt
                    || e.Control
                    || e.KeyCode.Equals(Keys.Left)
                    || e.KeyCode.Equals(Keys.Right)
                    || e.KeyCode.Equals(Keys.Up)
                    || e.KeyCode.Equals(Keys.Down)
                    || e.KeyCode.Equals(Keys.Home)
                    || e.KeyCode.Equals(Keys.End))
                { return; }

                if (IsValidKey(e.KeyValue) == false)
                { return; }

                _CurrentTime = DateTime.Now;
                timerOtherAddress.Stop();
                timerOtherAddress.Interval = 500;
                timerOtherAddress.Enabled = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void txtOtherSearch_TextChanged(object sender, EventArgs e)
        {
            if (timerOtherAddress.Enabled == false)
            {
                timerOtherAddress.Stop();
                timerOtherAddress.Enabled = true;
            }
            return;
        }

        private void timerOtherAddress_Tick(object sender, EventArgs e)
        {
            if (txtOtherSearch.Text.Trim() != "")
            {
                if (System.DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    timerOtherAddress.Stop();                                        
                    if (this.Visible == true)
                    {
                        //Changed to resolve Bug #88949: surescript Catalog search
                        TimerCode_OtherAddress(txtOtherSearch);

                    }                    
                }
            }
            else
            {
                timerOtherAddress.Stop();                                
                if (this.Visible == true)
                {
                    //Changed to resolve Bug #88949: surescript Catalog search
                    TimerCode_OtherAddress(txtOtherSearch);

                }                
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.dtTable  = null;
            PatientID = 0;
            flagIsChangePatient = false;
            this.Close();
        }

        private void btnCloseOthAdd_Click(object sender, EventArgs e)
        {
            txtOtherSearch.Text = "";
        }

        private void btnCloseOwnAdd_Click(object sender, EventArgs e)
        {
            txtOwnSearch.Text = "";
        }

        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            myTreeNode myNode = null;
            myTreeNode myChildNode = null;
            DataTable finalTable = null;
            gloUserControlLibrary.myList arr = null;
            
            try
            {
                
                //myNode = new myTreeNode();
                finalTable = new DataTable();
                finalTable.Columns.Add("Name", typeof(string));
                finalTable.Columns.Add("ContactID", typeof(long));
                finalTable.Columns.Add("NPI", typeof(string));
                finalTable.Columns.Add("Email", typeof(string));

                string firstName = string.Empty;
                string lastName = string.Empty;
                string fullName = string.Empty;
                long contactID = 0;
                string NPI = string.Empty;
                string email = string.Empty;

                // Reading TreeView Values
                for (int i = 0; i < trvRefferals.Nodes[0].GetNodeCount(false); i++)
                {
                    myNode = (myTreeNode)(trvRefferals.Nodes[0].Nodes[i]);

                    if (myNode.GetNodeCount(true) > 0)
                    {
                        for (int j = 0; j < myNode.GetNodeCount(true); j++) // 
                        {
                           // myChildNode = new myTreeNode();
                            myChildNode = (myTreeNode)(myNode.Nodes[j]);
                            if (myChildNode.Checked == true)
                            {
                                arr = new gloUserControlLibrary.myList();
                                arr = (gloUserControlLibrary.myList)myChildNode.arrRefferalDetails[0];

                                firstName = arr.ContactFirstName;
                                lastName = arr.ContactLastName;
                                contactID = arr.ID;
                                email = arr.Email;
                                NPI = arr.NPI;
                                fullName = string.Empty;

                                if (firstName != string.Empty && lastName != string.Empty)
                                {
                                    fullName = firstName.Trim() + " " + lastName.Trim();
                                }
                                if (firstName == string.Empty && lastName != string.Empty)
                                {
                                    fullName = lastName;
                                }
                                if (firstName != string.Empty && lastName == string.Empty)
                                {
                                    fullName = firstName;
                                }

                                finalTable.Rows.Add(fullName, contactID, NPI, email);
                            }

                        }
                    }

                }

                // Reading OwnAddress Values 
                for (int cntOwn = 0; cntOwn <= c1OwnAddress.Rows.Count - 1; cntOwn++)
                {
                    if (c1OwnAddress.GetCellCheck(cntOwn, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        firstName = c1OwnAddress.GetData(cntOwn, Col_Fname).ToString();
                        lastName = c1OwnAddress.GetData(cntOwn, Col_Lname).ToString();
                        contactID = Convert.ToInt64(c1OwnAddress.GetData(cntOwn, Col_ID).ToString());
                        NPI = c1OwnAddress.GetData(cntOwn, Col_NPI).ToString();
                        email = c1OwnAddress.GetData(cntOwn, Col_DirectAddress).ToString();
                        fullName = string.Empty;

                        if (firstName != string.Empty && lastName != string.Empty)
                        {
                            fullName = firstName.Trim() + " " + lastName.Trim();
                        }
                        if (firstName == string.Empty && lastName != string.Empty)
                        {
                            fullName = lastName;
                        }
                        if (firstName != string.Empty && lastName == string.Empty)
                        {
                            fullName = firstName;
                        }

                        finalTable.Rows.Add(fullName, contactID, NPI, email);
                    }
                }

                // Reading OtherAddress Values 
                for (int cntOther = 0; cntOther <= c1OtherAddress.Rows.Count - 1; cntOther++)
                {
                    if (c1OtherAddress.GetCellCheck(cntOther, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        firstName = c1OtherAddress.GetData(cntOther, Col_Fname).ToString();
                        lastName = c1OtherAddress.GetData(cntOther, Col_Lname).ToString();
                        contactID = Convert.ToInt64(c1OtherAddress.GetData(cntOther, Col_ID).ToString());
                        NPI = c1OtherAddress.GetData(cntOther, Col_NPI).ToString();
                        email = c1OtherAddress.GetData(cntOther, Col_DirectAddress).ToString();
                        fullName = string.Empty;

                        if (firstName != string.Empty && lastName != string.Empty)
                        {
                            fullName = firstName.Trim() + " " + lastName.Trim();
                        }
                        if (firstName == string.Empty && lastName != string.Empty)
                        {
                            fullName = lastName;
                        }
                        if (firstName != string.Empty && lastName == string.Empty)
                        {
                            fullName = firstName;
                        }

                        finalTable.Rows.Add(fullName, contactID, NPI, email);
                    }
                }

                if (finalTable.Rows.Count > 0)
                {
                    if (flagIsChangePatient == true)
                    {
                        if (flagIsToListOrAttachmentExists == true)
                        {

                            if ((System.Windows.MessageBox.Show("Message patient has been changed."+System.Environment.NewLine+"Removing any addresses in the To List and any message Attachments.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OKCancel, MessageBoxImage.Information)) == MessageBoxResult.OK)
                            {
                                this.dtTable = finalTable;
                                this.PatientID = PatientID;
                                this.Close();
                            }
                            else
                            {
                                this.dtTable = null;
                                flagIsChangePatient = false;
                                this.Close();
                            }
                        }
                        else
                        {
                            this.dtTable = finalTable;
                            this.PatientID = PatientID;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.dtTable = finalTable;
                        this.PatientID = PatientID;
                        this.Close();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please select DIRECT Address.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (arr != null)
                {
                    arr= null;
                }
                if (myChildNode != null)
                {                   
                    myChildNode = null;
                }
                if (myNode != null)
                {
                    myNode = null;
                }
                if (finalTable != null)
                {
                    finalTable.Dispose();
                    finalTable = null;
                }
            }

        }

        public void removeDuplicatesRows(DataTable dt)
         {
             DataTable uniqueCols = dt.DefaultView.ToTable(true, "Name", "ContactID", "NPI", "Email");
         }

        private void btnPatientList_Click(object sender, EventArgs e)
         {
             frmPatientList frm = null;
             try
             {

                 frm = new frmPatientList();
                 frm.DatabaseConnection = gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString;
                 frm.UserName = gloSurescriptSecureMessage.SecureMessageProperties.UserName;
                 frm.ShowDialog(this);
                 long _patID = frm.PatientID;
                                  
                 if (_patID > 0)
                 {
                     flagIsChangePatient = false;


                     //if (_patID != PatientID && PatientID > 0)
                        if (_patID != PatientID) 
                         {
                             //if (flagIsToListOrAttachmentExists == true)
                             //{
                                 //if ((System.Windows.MessageBox.Show("To list and Attachment will be deleted", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                                 //{
                                     flagIsChangePatient = true;
                                     AddPatient(_patID);
                                 //}
                            // }
                             //else
                             //{
                             //    flagIsChangePatient = true;
                             //    AddPatient(_patID);
                             //}
                         }                    
                         else
                         {
                           AddPatient(_patID);
                         }
                  }
             }

             catch (Exception ex)
             {
                 System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
             }

             finally
             {
                 if (frm != null)
                 {
                     frm.Dispose();
                     frm = null;
                 }

             }

         }

        private void AddPatient(long patientID)
         {             
             try
             {
                 PatientID = patientID;
                 UncheckAllNodes(trvRefferals.Nodes);
                 LoadTreeview(PatientID);
                 trvRefferals.ExpandAll();
             }

             catch (Exception ex)
             {
                 System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
             }

             finally
             {
                

             }

         }

        private void frmToList_FormClosing(object sender, FormClosingEventArgs e)
        {           
            if (_dvTimer != null)
            {
                _dvTimer.Dispose();
                _dvTimer = null;
            }

            if (_dvOwnAddress != null)
            {
                _dvOwnAddress.Dispose();
                _dvOwnAddress = null;
            }

            if (_dvOtherAddress != null)
            {
                _dvOtherAddress.Dispose();
                _dvOtherAddress = null;
            }
        }

        #endregion


    }
}
