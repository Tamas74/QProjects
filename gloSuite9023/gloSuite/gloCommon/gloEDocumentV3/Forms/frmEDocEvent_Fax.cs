using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloUserControlLibrary;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using gloEDocumentV3.DocumentContextMenu;
using gloEDocumentV3.Database;
using Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using gloOffice;
using gloWord;
using System.Runtime.InteropServices;
namespace gloEDocumentV3.Forms
{
    public enum enmFAXPriority
      {
        //To set priority as either 0 or 1
        NormalPriority,//0
        SendImmediately//1
      }

    public   enum enmFAXDocType
      {
          //To Fax DMS Documentr of any other PDF document from other module
          DMSDoc,//0
          OtherPDFDoc//1
      }

    public partial  class frmEDocEvent_Fax : Form 
    {
        ArrayList myArrLst = null;

        //var to close the form
        bool _bIsClose = true;
        
        //System.Drawing.Printing.PrintDocument oprintdoc = null;
       
        string ConverPageTemplateName = "";
        Wd.Document oCurDoc;
        Wd.Application oWordApp;
        string pdfFilepath = "";
        //code start by nilesh on 20110328 for case GLO2010-0008730
        private bool _IsInternetFax = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //code end by nilesh on 20110328 for case GLO2010-0008730
        //string strFAXType = "";

        enmFAXPriority CurrentSendingFAXPriority = enmFAXPriority.NormalPriority;
        private bool _IsRefresh = false;
        //
        private enmFAXDocType _FAXDocumentType = enmFAXDocType.DMSDoc ;
        string _FaxType = "DMS";
        string _PDFDocPath = "";
        public bool OpenFromCDA = false;//added to implement fax functionality from cda
        public string pdfFileName = ""; //added to implement fax functionality from cda
        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;
        gloUC_PatientStrip _PatientStrip = null;
        public frmEDocEvent_Fax()
        {
            InitializeComponent();
            _SelectedDocuments = new  eContextDocuments();
            _DestinationDocument = new eContextEventParameter();

            //code start by nilesh on 20110328 for case GLO2010-0008730
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

            #endregion  
            //code end by nilesh on 20110328 for case GLO2010-0008730
            
        }

        public frmEDocEvent_Fax(enmFAXDocType FAXDocType, string FilePath)
        {
            InitializeComponent();
            _SelectedDocuments = new  eContextDocuments();
            _DestinationDocument = new eContextEventParameter();
            _FAXDocumentType = FAXDocType;
            _PDFDocPath = FilePath;
            if (_FAXDocumentType == enmFAXDocType.OtherPDFDoc)
            {
                _FaxType = "Clinical Charts";
            }

            //code start by nilesh on 20110328 for case GLO2010-0008730
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

            #endregion  
            //code end by nilesh on 20110328 for case GLO2010-0008730
        }
        #region "Page Manuipulation Variables"

        public bool oDialogResultIsOK = false;
        public Int64 oDialogDocumentID = 0;
        public Int64 oDialogContainerID = 0;
        public Int64 oClinicID = 0;

        [DefaultValue(false)]
        public bool DocumentFaxed { get; set; }

        private   eContextDocuments  _SelectedDocuments = null;
        private eContextEventParameter _DestinationDocument = null;
        private DataTable _dtFaxDetails = null;
        public DataTable dtFaxDetails
        {
            get { return _dtFaxDetails; }
            set { _dtFaxDetails = value; }
        }

        public eContextDocuments oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        public eContextEventParameter oDestinationDocuments
        {
            get { return _DestinationDocument; }
            set { _DestinationDocument = value; }
        }

    

        #endregion

       
        #region Declare Variables

       // string DocumentPath;
        //string Cat_Contacts = "Contacts";
        string Cat_Physician = "Physician";
        string Cat_Hospital = "Hospital";
        string Cat_Insurance = "Insurance";
        string Cat_Pharmacy = "Pharmacy";
        string Cat_ePharmacy = "e Pharmacy";
        string Cat_NewRx = "New Rx";
        string Cat_NewRxRefillRqst = "New Rx & Refill Request";
        string Cat_Other = "Other Service Level";
        string Cat_Others = "Others";
        string Cat_PatientContacts = "Patient Contacts";
        string Cat_Referrals = "Referrals";
        string Cat_PrimaryCarePhysician = "Primary Care Physician";
        string Cat_OtherCareTeam = "Other Care Team";
        //const int COL_FIRSTNAME = 0;
        //const int COL_MIDDLECONTACT = 1;
        //const int COL_LASTNAME = 2;
        //const int COL_PHONE = 3;
        //const int COL_FAX = 4;
        //const int COL_ID = 5;
        //const int COL_COUNT = 6;

        //Declared Variables by Mayuri:20090915
        //Decalaration of ContactID for All categories such as Physician,Hospital,Insurance,Pharmacy & others
        const int COL_ID = 0;
        //Declaration for category physician
        const int COL_PREFIX = 1;
        const int COL_PHYFIRSTNAME = 2;
        const int COL_PHYMIDDLECONTACT = 3;
        const int COL_PHYLASTNAME = 4;
        const int COL_SUFFIX = 5;
        const int COL_PHYPHONE = 6;
        const int COL_PHYMOBILE = 7;
        const int COL_PHYEMAIL = 8;
        const int COL_PHYURL = 9;
        const int COL_PHYFAX = 10;
        const int COL_PHYDEGREE = 11;
        //Declaration for categories other than physician
        const int COL_NAME = 1;
        const int COL_CONTACT = 2;
        const int COL_PHONE = 3;
        const int COL_FAX = 7;
        const int COL_MOBILE = 4;
        const int COL_EMAIL = 5;
        const int COL_URL = 6;
       
        //end Variables declared by Mayuri:20090915

        //DMS Fax
        Boolean multipleRecipients = false;
        ArrayList gstrFAXContacts = new ArrayList();
        String gstrFAXContactPerson ="";
        String gstrFAXContactPersonFAXNo = "";
      //  String gstrFAXOutputDirectory = "";
        public string _ErrorMessage = "";
        private Int64 _PatientID = 0;
     

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
      
        //DMS Fax
        #endregion

        private void frmEDocEvent_Fax_Load(object sender, EventArgs e)
        {
            try
            {

               btnUp1.Visible = true;
               btnUp1.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
               btnUp1.BackgroundImageLayout = ImageLayout.Center;
               btnDown1.Visible = false;
               btnUp.Visible = true;
               btnUp.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
               btnUp.BackgroundImageLayout = ImageLayout.Center;
               btnDown.Visible = false;

               gstrFAXContacts.Clear();
               //gstrFAXOutputDirectory = gloEDocV3Admin.gstrFaxOutputDirectory;
               gstrFAXContactPerson = "";
               gstrFAXContactPersonFAXNo = "";
               multipleRecipients = false;
               //GetFaxDirSettings();
               //Code Added by Mayuri:20090914
               //Instead of combo Box We have Used Treeview control to display categories in order to make consistency between EMR Fax & DMS Fax
               //Function declaration to add nodes into treeview control trvContactType
               FillTreeView();
                //Commented by Mayuri:20090930
                //In order to make consistency between Contacts form and DMS fax
               //trvContactType.ExpandAll();
               //end Code Added by Mayuri
               myTreeNode  rootNode;
                if (trvFaxTo.Nodes.Count == 0)
                {
                    rootNode = new myTreeNode ();
                    rootNode.Text = "Selected Contacts";
                    rootNode.ImageIndex = 0;
                    rootNode.SelectedImageIndex = 0;
                    trvFaxTo.Nodes.Add(rootNode);
                }
                // Set Document Path
                //DocumentPath = "";

                 // GetFaxCategory();
                 //comeneted by Mayuri:20090916
                 // GetFaxList2();

                 //Code Added by Mayuri:20090914
                 //By default Focus should be set on first node of treeview(trvContactType) control i.e.Physician
                //myTreeNode myNode;
                LoadPatientStrip();
                if (trvContactType.Nodes[0].Nodes .Count >0)
                {
                    //myNode = trvContactType.SelectedNode .Nodes[0].Nodes[0];
                    //trvContactType.SelectedNode = myNode;
                    trvContactType.SelectedNode = trvContactType.Nodes[0].Nodes[0];
                }
                GetFaxList();
                //end Code Added by Mayuri:20090914
                if (gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage == true)
                {
                    //label1.Visible = true;
                    //Code Added by Mayuri:20091012
                    cmbTemplate.Visible = true;
                    //cmbCoverPage.Visible = true;
                    //cmbCoverPage.BringToFront();
                    FillCovertLetter();
                }
                //Code Added by Mayuri:20091014
                else
                {
                    pnldsoFAXPreview.Visible = false;
                    pnlc1FaxListHeader.Dock = DockStyle.Fill;
                    panel5.Dock = DockStyle.Bottom;
                    if (pnldsoFAXPreview.Visible == false)
                    {
                        pnlc1FaxListHeader.BringToFront();
                    }
                    panel5.Visible = false;
                   // panel9.Visible = false;
                   // panel5.Visible = false;
                   // pnlCoverPage.Visible = false;
                    
                }
                switch (CurrentSendingFAXPriority)
                {
                    case enmFAXPriority.NormalPriority:
                        optNormalPriority.Checked = true;
                        break;
                    case enmFAXPriority.SendImmediately:
                        optHighPriority.Checked = true;
                        break;
                }
                //Sandip Darade 201010204
                lblFaxDetails.Text="";
                mskFaxNo.Text = "";
                c1FaxList.Row  = 0;

                //code start by nilesh on 20110328 for case GLO2010-0008730
                if (_IsInternetFax == false)
                {
                    mskFaxNo.MaskType = gloMaskControl.gloMaskType.Other;
                }
                //code end by nilesh on 20110328 for case GLO2010-0008730
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LoadPatientStrip()
        {
            if ((_PatientStrip) != null)
            {
                pnlMain.Controls.Remove(_PatientStrip);
                _PatientStrip.Dispose();
                _PatientStrip = null;
            }
            _PatientStrip = new gloUC_PatientStrip();
            _PatientStrip.ShowDetail(PatientID, gloUC_PatientStrip.enumFormName.Fax);
            _PatientStrip.Dock = DockStyle.Top;
            _PatientStrip.Padding = new Padding(3, 0, 3, 0);
            pnlMain.Controls.Add(_PatientStrip);
        }
        public void Navigate(string strstring)
        {

            //foreach (Form frm in Application.OpenForms)
            //{
            oCurDoc.ActiveWindow.SetFocus();
            gloWord.gloWord.FindAndReplace(MyApp: oCurDoc.Application, FindText: strstring, ReplaceWith: " ", Forward: true, Wrap: 1, Replace: 0, MatchWildCards: false, MatchWholeWord: false);

            //}
        }
        //Code Added by Mayuri:20090914
        //To Add Nodes such as Contacts,Physician,Hospital etc.into trvContactType treeview control
        private void FillTreeView()
        {
            //To insert Node Contacts 
            myTreeNode myRequestNode;
            myTreeNode mynode = new myTreeNode("Contacts", 0);
            mynode.ImageIndex = 4;
            mynode.SelectedImageIndex = 4;
            trvContactType.Nodes.Add(mynode);
            myTreeNode mychildnode;
            //To insert Child Nodes under Contacts Node
            mychildnode =new myTreeNode("Physician", 0);
            mychildnode.ImageIndex = 3;
            mychildnode.SelectedImageIndex = 3;
            mynode.Nodes.Add(mychildnode);
            trvContactType.SelectedNode = mychildnode;

            
            mychildnode =new myTreeNode("Hospital", 1);
            mychildnode.ImageIndex = 5;
            mychildnode.SelectedImageIndex = 5;
            mynode.Nodes.Add(mychildnode);

            
            mychildnode =new myTreeNode("Insurance", 2);
            mychildnode.ImageIndex = 0;
            mychildnode.SelectedImageIndex = 0;
            mynode.Nodes.Add(mychildnode);

             
            mychildnode =new myTreeNode("Pharmacy", 3);
            mychildnode.ImageIndex = 2;
            mychildnode.SelectedImageIndex = 2;
            mynode.Nodes.Add(mychildnode);
            //Code added by Mayuri:20090929
            //to add Nodes ePharmacy and it's child nodes
            mychildnode = new myTreeNode("e Pharmacy", 4);
            mychildnode.ImageIndex = 6;
            mychildnode.SelectedImageIndex = 6;
            mynode.Nodes.Add(mychildnode);

            myRequestNode = new myTreeNode("New Rx", 40);
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);

            myRequestNode = new myTreeNode("New Rx & Refill Request", 41);
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);

            myRequestNode = new myTreeNode("Other Service Level", 42);
            //myRequestNode.Text = "Other";
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);
            //end code by Mayuri:20090929

            mychildnode = new myTreeNode("Others", 5);
            mychildnode.ImageIndex = 9;
            mychildnode.SelectedImageIndex = 9;
            mynode.Nodes.Add(mychildnode);

            mychildnode = new myTreeNode("Patient Contacts", 6);
            mychildnode.ImageIndex = 8;
            mychildnode.SelectedImageIndex = 8;
            mynode.Nodes.Add(mychildnode);
            trvContactType.SelectedNode = mychildnode;

            myRequestNode = new myTreeNode("Referrals", 43);
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);

            myRequestNode = new myTreeNode("Primary Care Physician", 44);
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);

            myRequestNode = new myTreeNode("Other Care Team", 45);
            myRequestNode.ImageIndex = 7;
            myRequestNode.SelectedImageIndex = 7;
            mychildnode.Nodes.Add(myRequestNode);
         }
     //end Code Added by Mayuri:20090914
        public void FillCovertLetter()
        {
            try
            {
                DataTable _dt = null;
                eDocManager.eDocGetList oList  = new gloEDocumentV3.eDocManager.eDocGetList();
                _dt = oList.GetFaxConvertPages();
                oList.Dispose();
                oList = null;
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        cmbTemplate.DataSource = _dt;
                        cmbTemplate.DisplayMember = _dt.Columns[1].ColumnName;
                        cmbTemplate.ValueMember = _dt.Columns[0].ColumnName;
                        cmbTemplate_SelectionChangeCommitted(null, null);
                        cmbTemplate.SelectedIndex = 0;
                        //This code commented by Mayuri:20091130
                        //We have used cmbTemplate instead of cmbCoverPage
                        //cmbCoverPage.DataSource = _dt;
                        //cmbCoverPage.DisplayMember = _dt.Columns[1].ColumnName;
                        //cmbCoverPage.ValueMember = _dt.Columns[0].ColumnName;
                        //cmbCoverPage_SelectionChangeCommitted(null, null);
                        //cmbCoverPage.SelectedIndex = 0;
                        //End code commenetd by Mayuri:20091130
                    }
                }
            }
            catch (Exception ex)
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("FillConverLetter: " + ex.ToString());
            }
        }

     
        #region "FlexGrid Events"
        private void c1FaxList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (c1FaxList.RowSel > 0)
                    {
                        c1FaxList.ContextMenu = cmnuAddFaxTo;
                    }
                    else
                    {
                        c1FaxList.ContextMenu = null;
                    }
                }
                else
                {
                    c1FaxList.ContextMenu = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }

        }

        private void c1FaxList_DoubleClick(object sender, EventArgs e)
        {

            try
            {


                C1.Win.C1FlexGrid.HitTestInfo hitThat = c1FaxList.HitTest();
                if (hitThat.Type != C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader) //added for bugid Bug #73911 
                {

                    if (mskFaxNo.IsValidated == true)
                    {
                        if (c1FaxList.RowSel > 0)//Start/Pending FaxGLO2011-0011669
                        {
                            if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)//condition added by Sandip Darade 20100125  BUG ID 5885
                            {
                                mskFaxNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFAX)).ToString().Trim();
                            }
                            else
                            {
                                mskFaxNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FAX)).ToString().Trim();
                            }
                        }

                    }
                    else
                    {
                        return;
                    }
                    //COmmentged by Sanjog on 2011 june 1
                    ////Added By Shweta 20091024
                    ////Variable added for get coverpage fileInfo
                    //FileInfo oFiledata = default(System.IO.FileInfo);
                    ////Variable added to provide path for saving the coverpage for selected patient
                    //string newSelectedPatient = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\temp~" + c1FaxList.GetData(c1FaxList.Row, 0) + ".docx";
                    //COmment End by Sanjog on 2011 june 1

                    mnuFaxTo_Click(sender, e);

                    //COmmented by Sanjog on 2011 june 1
                    ////Added By Shweta 20091024
                    //if (gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage == true)
                    //{
                    //    //tlb_Refresh_Click(sender, e);
                    //    RefreshData();
                    //    //To save the coverpage for selected patient
                    //    //Save the current document from dsoFAXPreview at given path 
                    //    dsoFAXPreview.Save(newSelectedPatient, true, null, null);
                    //    oCurDoc.Saved = true;
                    //    //Read FileInfo
                    //    oFiledata = new System.IO.FileInfo(newSelectedPatient);
                    //    pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                    //    object missing = System.Reflection.Missing.Value;
                    //    //save the current document at pdfFilepath
                    //    oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                    //}
                    ////End code adding 20091024
                    //COmmnet End 

                }
                }//try
            //Code added by Shweta 20091028          
            //Give message if word updates are not installed or Addin for saving the word document as pdf not installed 
            catch (COMException ex)
            {
                int nErrorCode = ex.ErrorCode;
                if (nErrorCode == -2147467259)
                {
                    MessageBox.Show("Unable to send fax with cover page as word updates are not installed properly,Please install Addins for the MS-Word", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            //End 20091028
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void c1FaxList_EnterCell(object sender, EventArgs e)
        {
            lblSearchOn.Text = "Search";
            //lblFaxDetails.Text = "";10769 //UC 6020.100// FAx >> Application is not displaying send to name if we send fax by entering no in fax field

            //Shweta 20091222 
            //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
           // txtFAXNo.Text = "";
           // mskFaxNo.ResetText();
            //End code by Shweta 
   
            //Added By Shweta 20091020
            //Variable added for get coverpage fileInfo
            //    FileInfo oFiledata = default(System.IO.FileInfo);
            //Variable added to provide path for saving the coverpage for selected patient
            string newSelectedPatient = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\tempFaxTo" + c1FaxList.Row + ".docx";

            try
            {

                if (c1FaxList.Rows.Count <= 1)
                {
                    return;
                }//if
                
                

                if (c1FaxList.Rows.Count > 0)
                {
                    if (c1FaxList.ColSel == 2)
                    {
                       lblSearchOn.Text = "Search";
                       //lblSearchOn.Text = (c1FaxList.GetData(0, COL_LASTNAME)).ToString().Trim() + " :";
                    }
                    else if (c1FaxList.ColSel != -1)
                    {
                        lblSearchOn.Text = "Search";
                        //lblSearchOn.Text = (c1FaxList.GetData(0, c1FaxList.ColSel)).ToString().Trim() + " :";
                    }
                }

                if (c1FaxList.RowSel > 0)
                {
                    if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                    {
                       //lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_MIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_LASTNAME)).ToString().Trim();
                       //txtFAXNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FAX)).ToString().Trim();
                        //COMMENTED BY SHUBHANGI 
                       //lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYMIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYLASTNAME)).ToString().Trim();
                        //ADDED BY SHUBHANGI TO CHECK WHETHER PREFIX & SUFFIX ARE PRESENT
                        if (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX).ToString() != "" && c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX).ToString() != "")
                        {
                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYMIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYLASTNAME)).ToString().Trim() + " "+ (c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX)).ToString().Trim();
                        }
                        else if (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX).ToString() != "" && c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX).ToString() == "")
                        {
                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYMIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYLASTNAME)).ToString().Trim();
                        }
                        else if (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX).ToString() == "" && c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX).ToString() != "")
                        {
                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYMIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYLASTNAME)).ToString().Trim();
                        }
                        else
                        {
                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYMIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYLASTNAME)).ToString().Trim()+ " "+ (c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX)).ToString().Trim();
                        }
                       //Shweta 20091222
                       //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                       //txtFAXNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFAX)).ToString().Trim();
                      
                       mskFaxNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_PHYFAX)).ToString().Trim();
                       //End code by Shweta 
                    }
                    else
                    {
                       //lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FIRSTNAME)).ToString().Trim();
                       lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_NAME )).ToString().Trim();

                       //Shweta 20091222
                       //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                        //txtFAXNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FAX)).ToString().Trim(); 
                      mskFaxNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FAX)).ToString().Trim();
                      //End code by Shweta 
                      
                    }

                    
                    
                }//if 

                //Added By Shweta 20091020               
                //Check whether Include cover page checked or not
                if (gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage == true)
                {
                    //tlb_Refresh_Click(sender, e);
                    RefreshData();
                   // To save the coverpage for selected patient
                   // Save the current document from dsoFAXPreview at given path 
                   // dsoFAXPreview.Save(newSelectedPatient, true, null, null);
                   // oCurDoc.Saved = true;
                   //Read FileInfo
                   // oFiledata = new System.IO.FileInfo(newSelectedPatient);
                   // pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                   // object missing = System.Reflection.Missing.Value;
                   // save the current document at pdfFilepath
                   // oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                 }
                //End Code Added by Shweta:20091020
            }//try
                 //Code added by Shweta 20091028          
            //Give message if word updates are not installed or Addin for saving the word document as pdf not installed 
            catch (COMException ex)
            {
                int nErrorCode = ex.ErrorCode;
                if (nErrorCode == -2147467259)
                {
                    MessageBox.Show("Unable to send Fax with cover page as word updates are not installed properly,Please install Addins for the MS-Word", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            //End 20091028
           
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }//catch

  }

        private void SearchFlex()
        {
            string sSearchDoc = txtSearchDocument.Text.ToUpper() + "*";
            int i;
            //c1FaxList.Rows = -1;

            //On Error Resume Next

            if (sSearchDoc != "")
            {
                for (i = 0; i <= c1FaxList.Rows.Count - 1; i++)
                {
                    string Selcol = c1FaxList.GetData(i, c1FaxList.ColSel).ToString();
                    if (Selcol.ToUpper().Contains(sSearchDoc) == true)
                    {
                        c1FaxList.Row = i;
                        c1FaxList.Focus();
                        return;
                    }
                }
            }

            sSearchDoc = null;

        }


        #endregion
                          
        #region "Methods related to  fax Contacts"

        private void trvFaxTo_MouseDown(object sender, MouseEventArgs e)
        {
            //Code Added on 20091026
            trvFaxTo.ContextMenu = null;
            //End code Added on 20091026

            //Code Commented by Mayuri:20091026
            //Added this code on NodeMouseclick
            //try
            //{
            //    trvFaxTo.SelectedNode = e.Node;
            //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //    {
            //        //Code Added by Mayuri:20091014
            //        //To check whether root node selected if so contextmenu shouldn't display
            //        if (trvFaxTo.Nodes[0].Nodes.Count > 0)
            //        {
            //            if ((!object.ReferenceEquals(trvFaxTo.SelectedNode, trvFaxTo.Nodes[0])))
            //            {
            //                trvFaxTo.ContextMenu = cmnuDeleteFaxTo;

            //            }
            //            else
            //            {
            //                trvFaxTo.ContextMenu = null;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        trvFaxTo.ContextMenu = null;
            //    }
            //}

            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString, "DMS Fax", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //End Code Commented by Mayuri:20091026

        }

        private void mnuFaxTo_Click(object sender, EventArgs e)
        {
            Int64 Id = 0;
            myTreeNode myNode = null;
            try
            {

                //Start/This Change occured when Fax Issues is resolved
                if (mskFaxNo.IsValidated == false)
                {
                    return;
                }
                //End/This Change occured when Fax Issues is resolved


                if (c1FaxList.RowSel > 0)//Pending FaxGLO2011-0011669
                {
                    Id = Convert.ToInt64(c1FaxList[c1FaxList.RowSel, COL_ID].ToString());

                    // lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_FIRSTNAME ].ToString();
                    //if category is physician then First & Last name should get added to selected contacts
                    if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                    {
                        //COMMENTED BY SHUBHANGI
                        //lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_PHYFIRSTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYMIDDLECONTACT ].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYLASTNAME].ToString();               // lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_LASTNAME ].ToString();
                        //ADDED BY SHUBHANGI TO CHECK WHETHER PREFIX & SUFFIX ARE PRESENT
                        if (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX).ToString() != "" && c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX).ToString() != "")
                        {
                            lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_PREFIX].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYFIRSTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYMIDDLECONTACT].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYLASTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_SUFFIX].ToString();
                        }
                        else if (c1FaxList[c1FaxList.RowSel, COL_PREFIX].ToString() != "" && c1FaxList[c1FaxList.RowSel, COL_SUFFIX].ToString() == "")
                        {
                            lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_PREFIX].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYFIRSTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYMIDDLECONTACT].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYLASTNAME].ToString();
                        }
                        else if (c1FaxList.GetData(c1FaxList.RowSel, COL_PREFIX).ToString() == "" && c1FaxList.GetData(c1FaxList.RowSel, COL_SUFFIX).ToString() != "")
                        {
                            lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_PHYFIRSTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYMIDDLECONTACT].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYLASTNAME].ToString();
                        }
                        else
                        {
                            lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_PHYFIRSTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYMIDDLECONTACT].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_PHYLASTNAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_SUFFIX].ToString();
                        }
                    }
                    //if categories other than physician then only Name gets added into selected contacts
                    else
                    {
                        lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_NAME].ToString() + " " + c1FaxList[c1FaxList.RowSel, COL_CONTACT].ToString();               // lblFaxDetails.Text = c1FaxList[c1FaxList.RowSel, COL_LASTNAME ].ToString();
                    }

                  //  int cnt = 0;
                    //Shweta 20011222
                    //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                    //if (txtFAXNo.Text == "")
                    if (mskFaxNo.Text == "")
                    //End code by Shweta 
                    {
                        MessageBox.Show("Please enter the fax number for this contact.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFaxNo.Focus();
                        return; // TODO: might not be correct. Was : Exit Sub 
                    }

                    //myTreeNode myNode;

                    for (int i = 0; i <= trvFaxTo.Nodes[0].Nodes.Count - 1; i++)
                    {
                       // myNode = new myTreeNode();
                        myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[i];
                        if (myNode.Key == Id)
                        {
                            return;
                        }
                    }
                    //Changed By Shweta 20091130
                    //myNode = new myTreeNode(lblFaxDetails.Text, Id, txtFAXNo.Text);
                    //Saved the template ID in Node to get teh selected template for respective receiver

                    //Shweta 20091222
                    //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                    //myNode = new myTreeNode(lblFaxDetails.Text, Id, txtFAXNo.Text, Convert.ToInt64(cmbTemplate.SelectedValue));
                    myNode = new myTreeNode(lblFaxDetails.Text, Id, mskFaxNo.Text);
                    //End Shweta 20091130
                    if (trvContactType.SelectedNode.Text == "Physician")
                    {
                        myNode.ImageIndex = 5;
                        myNode.SelectedImageIndex = 5;
                    }
                    else if (trvContactType.SelectedNode.Text == "Others")
                    {
                        myNode.ImageIndex = 9;
                        myNode.SelectedImageIndex = 9;
                    }
                    else if (trvContactType.SelectedNode.Text == "Pharmacy")
                    {
                        myNode.ImageIndex = 3;
                        myNode.SelectedImageIndex = 3;
                    }
                    else if (trvContactType.SelectedNode.Text == "Insurance")
                    {
                        myNode.ImageIndex = 2;
                        myNode.SelectedImageIndex = 2;
                    }
                    else if (trvContactType.SelectedNode.Text == "Hospital")
                    {
                        myNode.ImageIndex = 1;
                        myNode.SelectedImageIndex = 1;
                    }
                    else if (trvContactType.SelectedNode.Text == "Patient Contacts")
                    {
                        myNode.ImageIndex = 8;
                        myNode.SelectedImageIndex = 8;
                    }
                    else if (trvContactType.SelectedNode.Text == "Referrals")
                    {
                        myNode.ImageIndex = 8;
                        myNode.SelectedImageIndex = 8;
                    }
                    else if (trvContactType.SelectedNode.Text == "Primary Care Physician")
                    {
                        myNode.ImageIndex = 8;
                        myNode.SelectedImageIndex = 8;
                    }
                    else if (trvContactType.SelectedNode.Text == "Other Care Team")
                    {
                        myNode.ImageIndex = 8;
                        myNode.SelectedImageIndex = 8;
                    }
                }
                //Added By Sanjog 2011 June 1
                //Variable added for get coverpage fileInfo
             
                //Variable added to provide path for saving the coverpage for selected patient
                //string newSelectedPatient = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\temp~" + c1FaxList.GetData(c1FaxList.Row, 0) + ".docx";//
                string newSelectedPatient = "";
                //Start/Pending FaxGLO2011-0011669
                if (Id == 0)
                {
                    newSelectedPatient = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\temp~" + 0 + ".docx";

                }
                else
                {
                    newSelectedPatient = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\temp~" + c1FaxList.GetData(c1FaxList.Row, 0) + ".docx";

                }
                //End/Pending FaxGLO2011-0011669
                if (gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage == true)
                {
                    RefreshData();

                    //Fax Module change 
                   // dsoFAXPreview.Save();

                    gloWord.LoadAndCloseWord.SaveDSO(ref dsoFAXPreview, ref oCurDoc, ref oWordApp);
                    //Fax Module Change

                    //To save the coverpage for selected patient
                    //Save the current document from dsoFAXPreview at given path 

                    dsoFAXPreview.Save(newSelectedPatient, true, null, null);
                    oCurDoc.Saved = true;
                    //Read FileInfo
                    FileInfo oFiledata  = new System.IO.FileInfo(newSelectedPatient);
                    pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                    object missing = System.Reflection.Missing.Value;
                    //save the current document at pdfFilepath
                    oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                    oFiledata = null;
                }
                //End code - Sanjog on 2011 June 1

                trvFaxTo.Nodes[0].Nodes.Add(myNode);
                myNode = null;
                trvFaxTo.ExpandAll();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                //MessageBox.Show(ex.ToString(),"DMS Fax",MessageBoxButtons.OK,MessageBoxIcon.Error)
                //    (ex.ToString(), "DMS Fax", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //  If dgContacts.CurrentRowIndex < 0 Then
            //    MessageBox.Show("Please select the Contact", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //    Exit Sub
            //End If
            //If Trim(txtFAXNo.Text) = "" Then
            //    MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //    txtFAXNo.Focus()
            //    Exit Sub
            //End If

            
            if (c1FaxList.Row < 1)
            {
                MessageBox.Show("Select the Contact", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }

            //if (cmbCategory.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please select the Contact", "DMS Fax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return; // TODO: might not be correct. Was : Exit Sub 
            //}
            
            //Shweta 20091222
            //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
            //if (txtFAXNo.Text.Trim() == "")
            if (mskFaxNo.IsValidated == false)
            //End code by Shweta 
            {
                //MessageBox.Show("Please enter the FAX No", "DMS Fax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               //Shweta 20091222
                //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
               //txtFAXNo.Focus();
              //  mskFaxNo.Focus();
                //End code by Shweta 
                return; // TODO: might not be correct. Was : Exit Sub 
            }
            // ClsContactDBLayer ContactDBLayer = new ClsContactDBLayer(true);

            //Shweta 20091222
            //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
            //if (UpdateContactFAXNo(Convert.ToInt64(c1FaxList.GetData(c1FaxList.RowSel, COL_ID).ToString()), txtFAXNo.Text) == false)
            if (UpdateContactFAXNo(Convert.ToInt64(c1FaxList.GetData(c1FaxList.RowSel, COL_ID).ToString()), mskFaxNo.Text) == false)
            //End code by Shweta 
            {
                MessageBox.Show("Unable to update the Contact's Fax no.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                {
                    int nRow = c1FaxList.RowSel;
                    if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                    {
                        //Shweta 20091222
                        //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                        //c1FaxList.SetData(nRow, COL_PHYFAX, txtFAXNo.Text);
                        c1FaxList.SetData(nRow, COL_PHYFAX , mskFaxNo.Text);
                        //End code by Shweta 
                    }
                    //Categories other than physician
                    else
                    {
                        //Shweta 20091222
                        //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                        //c1FaxList.SetData(nRow, COL_FAX, txtFAXNo.Text);
                        c1FaxList.SetData(nRow, COL_FAX, mskFaxNo.Text);
                        //End code by Shweta 
                    }
                    //Fill_FaxList() 
                    c1FaxList.Row = nRow;
                    //Added by Mayuri:20091015
                    //To clear text after updating faxno.
                    //Commented by Shweta 20091222
                    //As after updating we need to keep same fax no. for the contact in fax maskTextbox
                    //txtFAXNo.Text = "";                   
                    //End Commenting by Shweta 
                    //End code Added by Mayuri:20091015
                }
            }



            //sarika 28th nov 07 
            Int64 Id;
            Id = Convert.ToInt64(c1FaxList[c1FaxList.RowSel, COL_ID]);


            myTreeNode myNode;

            for (int i = 0; i <= trvFaxTo.Nodes[0].Nodes.Count - 1; i++)
            {
               // myNode = new myTreeNode();
                myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[i];
                if (myNode.Key == Id)
                {
                    //Shweta 20091222
                    //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                    //myNode.Tag = txtFAXNo.Text;
                    myNode.Tag = mskFaxNo.Text;
                    //End code by Shweta 
                    return; // TODO: might not be correct. Was : Exit Sub 


                }
            }

        }
        
        public bool UpdateContactFAXNo(Int64 nContactID, string strFAXNo)
        {

            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            Database .DBParameter oParam = new Database .DBParameter();
            Database.DBParameters oParams = new Database.DBParameters();



            try
            {
                oDB.Connect(false);
                oParam = new Database.DBParameter();
                oParams.Add("@ContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParams.Add("@FAXNo", strFAXNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_UpdateContactFAXNo", oParams);
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                if (oParams != null)
                {
                    oParams.Dispose();
                    oParams = null;
                }
            }

        }

        private void GetFaxList()
        {
            try
            {

                c1FaxList.AllowEditing = false;

                // GLO2011-0014859 : 6041 Update - Contact Info Not Fully Visible in Fax Window
                // Allow column resizing and disable sorting
                c1FaxList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                c1FaxList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                
                Getcontacts();



                if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                {
                    c1FaxList.Cols[COL_ID].Visible = false;
                    c1FaxList.Cols[COL_PREFIX].Visible = true;
                    c1FaxList.Cols[COL_PHYFIRSTNAME].Visible = true;
                    c1FaxList.Cols[COL_PHYMIDDLECONTACT].Visible = true;
                    c1FaxList.Cols[COL_PHYLASTNAME].Visible = true;
                    c1FaxList.Cols[COL_SUFFIX].Visible = true;
                    c1FaxList.Cols[COL_PHYPHONE].Visible = true;
                    c1FaxList.Cols[COL_PHYMOBILE].Visible = false;
                    c1FaxList.Cols[COL_PHYEMAIL].Visible = false;
                    c1FaxList.Cols[COL_PHYURL].Visible = false;
                    c1FaxList.Cols[COL_PHYFAX].Visible = true;
                    c1FaxList.Cols[COL_PHYDEGREE].Visible = false;
                    c1FaxList.Cols[COL_PREFIX].Caption = "Prefix";
                    c1FaxList.Cols[COL_PHYFIRSTNAME].Caption = "First Name";
                    c1FaxList.Cols[COL_PHYMIDDLECONTACT].Caption = "Middle Name";
                    c1FaxList.Cols[COL_PHYLASTNAME].Caption = "Last Name";
                    c1FaxList.Cols[COL_SUFFIX].Caption = "Suffix";
                }
                else
                {
                    c1FaxList.Cols[COL_ID].Visible = false;
                    c1FaxList.Cols[COL_NAME].Visible = true;
                    c1FaxList.Cols[COL_CONTACT].Visible = true;
                    c1FaxList.Cols[COL_PHONE].Visible = true;
                    c1FaxList.Cols[COL_MOBILE].Visible = false;
                    c1FaxList.Cols[COL_EMAIL].Visible = false;
                    c1FaxList.Cols[COL_URL].Visible = false;
                    c1FaxList.Cols[COL_FAX].Visible = true;
                }
                if ((trvContactType.SelectedNode.Text == Cat_Physician) || (trvContactType.SelectedNode.Text == Cat_Hospital) || (trvContactType.SelectedNode.Text == Cat_Insurance) || (trvContactType.SelectedNode.Text == Cat_Others) || (trvContactType.SelectedNode.Text == Cat_Pharmacy) || (trvContactType.SelectedNode.Text == Cat_PatientContacts) || (trvContactType.SelectedNode.Text == Cat_Referrals) || (trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician) || (trvContactType.SelectedNode.Text == Cat_OtherCareTeam))
                {
                    tlb_Update.Visible = true;

                    mskFaxNo.ReadOnly = false;

                }


                else if ((trvContactType.SelectedNode.Text == Cat_ePharmacy) || (trvContactType.SelectedNode.Text == Cat_NewRx) || (trvContactType.SelectedNode.Text == Cat_NewRxRefillRqst) || (trvContactType.SelectedNode.Text == Cat_Other))
                {
                    tlb_Update.Visible = false;
                    mskFaxNo.ReadOnly = true;

                }
                //if (txtSearchDocument.Text == "")
                //{
                if (trvContactType.SelectedNode.Text != "Contacts")    //added for bugid Bug #73910
                {
                    c1FaxList.Select(0, 0, false);
                }
                mskFaxNo.Text = "";
                lblFaxDetails.Text = "";
                //}

               
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }


                #endregion " Make Log Entry "

            }
            finally
            {
                //   oDB.Disconnect();
            }

        }

        //Sandip Darade 20100422
        //initially only 1000 records will be pulled from database 
        //records will be now pulled as per user search query
        private void Getcontacts()
        {

            try
            {


                string strsearch = txtSearchDocument .Text.Trim();

                string[] strSearchArray = null;
                string sFilter = "";

                strsearch = strsearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (strsearch.StartsWith("*") == true)
                {
                    strsearch = strsearch.Replace("*", "%");
                }

                strsearch = strsearch.Replace("*", "[*]");

                if (strsearch.Length > 1)
                {
                    string str = strsearch.Substring(1);
                    strsearch = strsearch.Substring(0, 1) + str;
                }
               
                if (strsearch .Trim() != "")
                {
                    strSearchArray = strsearch.Split(',');
                }

                if ((!string.IsNullOrEmpty(strsearch)))
                {

                    if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                    {
                        if (strSearchArray.Length == 1)
                        {
                            //sFilter = "AND ( sFirstName like ('" + strsearch + "%')OR sMiddleName like ('" + strsearch + "%') OR sLastName like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') )";
                            //ADDED BY SHUBHANGI TO APPLY SEARCH ON PREFIX & SUFFIX COLUMN
                            sFilter = "AND   ( Contacts_Physician_DTL.sPrefix like ('" + strsearch + "%')OR Contacts_Mst.sFirstName like ('" + strsearch + "%')OR Contacts_Mst.sMiddleName like ('" + strsearch + "%') OR Contacts_Mst.sLastName like ('" + strsearch + "%')OR Contacts_Physician_DTL.sDegree like ('" + strsearch + "%') OR Contacts_Mst.sPhone like ('" + strsearch + "%') OR Contacts_Mst.sFax like ('" + strsearch + "%') )";
                        }
                        else
                        {
                            //For Comma separated value search
                            for (int i = 0; i <= strSearchArray.Length - 1; i++)
                            {
                                strsearch = strSearchArray[i].Trim();

                                if (i == 0)
                                {
                                    sFilter = sFilter + " AND ";
                                    //ADDED BY SHUBHANGI TO APPLY SEARCH ON PREFIX & SUFFIX COLUMN
                                    //sFilter = sFilter + " ( " + " sFirstName like ('" + strsearch + "%')OR sMiddleName like ('" + strsearch + "%') OR sLastName like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') ";
                                    sFilter = sFilter + " ( " + " Contacts_Physician_DTL.sPrefix like ('" + strsearch + "%')OR Contacts_Mst.sFirstName like ('" + strsearch + "%')OR Contacts_Mst.sMiddleName like ('" + strsearch + "%') OR Contacts_Mst.sLastName like ('" + strsearch + "%')OR Contacts_Physician_DTL.sDegree like ('" + strsearch + "%') OR Contacts_Mst.sPhone like ('" + strsearch + "%') OR Contacts_Mst.sFax like ('" + strsearch + "%') ";
                                    sFilter = sFilter + " ) ";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(sFilter))
                                    {

                                        sFilter = sFilter + " AND ";

                                        //sFilter = sFilter + " (" + " sFirstName like ('" + strsearch + "%')OR sMiddleName like ('" + strsearch + "%')OR sLastName like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') ";
                                        //ADDED BY SHUBHANGI TO APPLY SEARCH ON PREFIX & SUFFIX COLUMN
                                        sFilter = sFilter + " (" + " Contacts_Physician_DTL.sPrefix like ('" + strsearch + "%')OR Contacts_Mst.sFirstName like ('" + strsearch + "%')OR Contacts_Mst.sMiddleName like ('" + strsearch + "%') OR Contacts_Mst.sLastName like ('" + strsearch + "%')OR Contacts_Physician_DTL.sDegree like ('" + strsearch + "%') OR Contacts_Mst.sPhone like ('" + strsearch + "%') OR Contacts_Mst.sFax like ('" + strsearch + "%')";

                                        sFilter = sFilter + " ) ";
                                    }

                                }

                            }

                        }
                    }
                    else
                    {

                        if (strSearchArray.Length == 1)
                        {
                            sFilter = "AND ( sName like ('" + strsearch + "%')OR sContact like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') )";
                        }
                        else
                        {
                            //For Comma separated value search
                            for (int i = 0; i <= strSearchArray.Length - 1; i++)
                            {
                                strsearch = strSearchArray[i].Trim();

                                if (i == 0)
                                {
                                    sFilter = sFilter + " AND ";
                                    sFilter = sFilter + " ( " + " sName like ('" + strsearch + "%')OR sContact like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') ";
                                    sFilter = sFilter + " ) ";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(sFilter))
                                    {

                                        sFilter = sFilter + " AND ";

                                        sFilter = sFilter + " (" + " sName like ('" + strsearch + "%')OR sContact like ('" + strsearch + "%') OR sPhone like ('" + strsearch + "%') OR sFax like ('" + strsearch + "%') ";
                                        sFilter = sFilter + " ) ";

                                    }

                                }

                            }
                        }



                    }
                }



                string Category = trvContactType.SelectedNode.Text;
                // string Category = cmbCategory.SelectedItem.ToString();
                gloEDocumentV3.Database.DBLayer oDB = new DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                Database.DBParameters oDBParameters = new DBParameters();
                oDBParameters.Add("@Type", Category, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Filter", sFilter, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                DataTable oDT = null;
                oDB.Retrive("gsp_ViewContacts_Mst_Temp_Filtered", oDBParameters, out oDT);

                c1FaxList.DataSource = oDT;
                //DataView dv = oDT.DefaultView;
                if (Category == "Contacts")   //added for bugid Bug #73910
                {
                    c1FaxList.Rows.Count = 1;
                }
             
                // GLO2011-0014859 : 6041 Update - Contact Info Not Fully Visible in Fax Window
                int nWidth = pnlC1FaxList.Width - 10;
                if (Category.ToString().Trim() != "Physician" && Category.ToString().Trim() != "Patient Contacts" && Category.ToString().Trim() != "Referrals" && Category.ToString().Trim() != "Primary Care Physician" && Category.ToString().Trim() != "Other Care Team")
                {
                    c1FaxList.Cols[COL_NAME].Width = Convert.ToInt32(nWidth / 2.5);
                    c1FaxList.Cols[COL_CONTACT].Width = nWidth / 4;
                    c1FaxList.Cols[COL_PHONE].Width = nWidth / 6;
                    c1FaxList.Cols[COL_MOBILE].Width = nWidth / 6;
                    c1FaxList.Cols[COL_EMAIL].Width = nWidth / 9;
                    c1FaxList.Cols[COL_URL].Width = nWidth / 6;
                    c1FaxList.Cols[COL_FAX].Width = nWidth / 4;
                }
                else
                {
                    c1FaxList.Cols[COL_PREFIX].Width = nWidth / 11;
                    c1FaxList.Cols[COL_PHYFIRSTNAME].Width = nWidth / 6;
                    c1FaxList.Cols[COL_PHYMIDDLECONTACT].Width = nWidth / 6;
                    c1FaxList.Cols[COL_PHYLASTNAME].Width = nWidth / 6;
                    c1FaxList.Cols[COL_SUFFIX].Width = nWidth / 11;
                    c1FaxList.Cols[COL_PHYPHONE].Width = nWidth / 6;
                    c1FaxList.Cols[COL_PHYFAX].Width = nWidth / 5;
                }

                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {



            }
        }       
        //Commented By Mayuri:20090914
        //This Function used previously,but as we implemented Generalized search we replaced this function by using datatable instead of datareader
        //          private void GetFaxList()
        //{
                //    try
        //    {
        //        txtSearchDocument.Text = "";

        //        c1FaxList.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
        //        c1FaxList.Rows.Count = 1;
        //        c1FaxList.Rows.Fixed = 1;
        //        c1FaxList.Cols.Count = COL_COUNT;
        //        c1FaxList.AllowEditing = false;
        //        c1FaxList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
               
                ////temp
        //        if (cmbCategory.SelectedItem != null && cmbCategory.SelectedItem.ToString() != "")
        //        {
        //            if (cmbCategory.SelectedItem.ToString() == Cat_Physician)
        //            {
        //                c1FaxList.Cols[COL_FIRSTNAME].Width = 125;
        //                c1FaxList.Cols[COL_MIDDLECONTACT].Width = 121;
        //                c1FaxList.Cols[COL_LASTNAME].Width = 125;
        //                c1FaxList.Cols[COL_PHONE].Width = 120;
        //                c1FaxList.Cols[COL_PHONE].DataType = System.Type.GetType("System.String");

        //                c1FaxList.Cols[COL_FAX].Width = 120;
        //                c1FaxList.Cols[COL_FAX].DataType = System.Type.GetType("System.String");

        //                c1FaxList.SetData(0, COL_FIRSTNAME, "First Name");
        //                c1FaxList.SetData(0, COL_MIDDLECONTACT, "Middle Name");
        //                c1FaxList.SetData(0, COL_LASTNAME, "Last Name");
        //                c1FaxList.SetData(0, COL_PHONE, "Phone");
        //                c1FaxList.SetData(0, COL_FAX, "Fax");
        //            }
        //            else
        //            {
        //                c1FaxList.Cols[COL_FIRSTNAME].Width = 185;
        //                c1FaxList.Cols[COL_MIDDLECONTACT].Width = 181;
        //                c1FaxList.Cols[COL_LASTNAME].Width = 0;
        //                c1FaxList.Cols[COL_PHONE].Width = 120;
        //                c1FaxList.Cols[COL_FAX].Width = 125;

        //                c1FaxList.SetData(0, COL_FIRSTNAME, "Name");
        //                c1FaxList.SetData(0, COL_MIDDLECONTACT, "Contact");
        //                c1FaxList.SetData(0, COL_LASTNAME, "");
        //                c1FaxList.SetData(0, COL_PHONE, "Phone");
        //                c1FaxList.SetData(0, COL_FAX, "Fax");
        //            }

        //            c1FaxList.Cols[COL_ID].Visible = false;
        //            c1FaxList.Rows[0].Height = 22;
        //            System.Data.SqlClient.SqlDataReader oDataReader;
                   
        //            if (cmbCategory.SelectedIndex >= 0)
        //            {
        //                string Category = cmbCategory.SelectedItem.ToString();
        //                gloEDocumentV3.Database.DBLayer oDB = new DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
        //                Database.DBParameters oDBParameters = new  DBParameters();
        //               oDBParameters.Add("@Type", Category, ParameterDirection.Input, SqlDbType.VarChar);
        //                oDB.Connect(false);
        //                oDB.Retrive("gsp_ViewContacts_Mst", oDBParameters, out oDataReader);
        //                c1FaxList.BeginInit();
        //                if (oDataReader.HasRows == true)
        //                {
        //                    if (cmbCategory.SelectedItem.ToString() == Cat_Physician)
        //                    {
        //                        while (oDataReader.Read())
        //                        {
        //                            c1FaxList.Rows.Add();
        //                            c1FaxList.Rows[c1FaxList.Rows.Count - 1].Height = 22;
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_FIRSTNAME, oDataReader[1]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_MIDDLECONTACT, oDataReader[2]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_LASTNAME, oDataReader[3]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_PHONE, Convert.ToString(oDataReader[4]));
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_FAX, oDataReader[8]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_ID, oDataReader[0]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        while (oDataReader.Read())
        //                        {
        //                            c1FaxList.Rows.Add();
        //                            c1FaxList.Rows[c1FaxList.Rows.Count - 1].Height = 22;
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_FIRSTNAME, oDataReader[1]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_MIDDLECONTACT, oDataReader[2]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_LASTNAME, "");
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_PHONE, Convert.ToString(oDataReader[3]));
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_FAX, oDataReader[7]);
        //                            c1FaxList.SetData(c1FaxList.Rows.Count - 1, COL_ID, oDataReader[0]);
        //                        }
        //                    }


        //                    c1FaxList.Cols[COL_PHONE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //                    c1FaxList.Cols[COL_FAX].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;



        //                    if (cmbCategory.SelectedItem.ToString() == Cat_Physician)
        //                    {
        //                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FIRSTNAME)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_MIDDLECONTACT)).ToString().Trim() + " " + (c1FaxList.GetData(c1FaxList.RowSel, COL_LASTNAME)).ToString().Trim();
        //                    }
        //                    else
        //                    {
        //                        lblFaxDetails.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FIRSTNAME)).ToString().Trim();
        //                    }
        //                    txtFAXNo.Text = (c1FaxList.GetData(c1FaxList.RowSel, COL_FAX)).ToString().Trim();
                //                } // End DataReade Has Row
        //                oDB.Disconnect();
        //                c1FaxList.EndInit();
        //                if (c1FaxList.GetData(0, COL_LASTNAME).ToString() != "")
        //                {
        //                    c1FaxList.Col = 2;
        //                }
        //                else
        //                {
        //                    c1FaxList.Col = 0;
        //                }
        //            }//if
        //        }//if(cmbCategory.SelectedItem)
        //    }//try
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "               
        //        //throw;
        //    }
        //    finally
        //    {
        //     //   oDB.Disconnect();
        //    }

        //}
        //end Code commented by Mayuri:20090914

        //delete contacts

        //Code Added by Mayuri:20090916
        //To hide columns of grid before searching text.
        //private void HideColumn()
        //{
        //    if (trvContactType.SelectedNode.Text == Cat_Physician)
        //    {
        //        c1FaxList.Cols[COL_ID].Visible = false;
        //        c1FaxList.Cols[COL_PHYMOBILE].Visible = false;
        //        c1FaxList.Cols[COL_PHYEMAIL].Visible = false;
        //        c1FaxList.Cols[COL_PHYURL].Visible = false;
        //        c1FaxList.Cols[COL_PHYDEGREE].Visible = false;
        //    }
        //    else
        //    {
        //        c1FaxList.Cols[COL_ID].Visible = false;
        //        c1FaxList.Cols[COL_MOBILE].Visible = false;
        //        c1FaxList.Cols[COL_EMAIL].Visible = false;
        //        c1FaxList.Cols[COL_URL].Visible = false;
        //    }
        //}
        private void HideColumn()
        {
            if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
            {
                c1FaxList.Cols[COL_ID].Visible = false;
                c1FaxList.Cols[COL_PHYFIRSTNAME].Visible = true;
                c1FaxList.Cols[COL_PHYMIDDLECONTACT].Visible = true;
                c1FaxList.Cols[COL_PHYLASTNAME].Visible = true;
                c1FaxList.Cols[COL_PHYPHONE].Visible = true;
                c1FaxList.Cols[COL_PHYMOBILE].Visible = false;
                c1FaxList.Cols[COL_PHYEMAIL].Visible = false;
                c1FaxList.Cols[COL_PHYURL].Visible = false;
                c1FaxList.Cols[COL_PHYFAX].Visible = true;
                c1FaxList.Cols[COL_PHYDEGREE].Visible = false;
                c1FaxList.Cols[COL_PHYFIRSTNAME].Caption = "First Name";
                c1FaxList.Cols[COL_PHYMIDDLECONTACT].Caption = "Middle Name";
                c1FaxList.Cols[COL_PHYLASTNAME].Caption = "Last Name";


            }
            else
            {
                c1FaxList.Cols[COL_ID].Visible = false;
                c1FaxList.Cols[COL_NAME].Visible = true;
                c1FaxList.Cols[COL_CONTACT].Visible = true;
                c1FaxList.Cols[COL_PHONE].Visible = true;
                c1FaxList.Cols[COL_MOBILE].Visible = false;
                c1FaxList.Cols[COL_EMAIL].Visible = false;
                c1FaxList.Cols[COL_URL].Visible = false;
                c1FaxList.Cols[COL_FAX].Visible = true;
            }
        
        }

        //end code added by Mayuri:20090916
        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //sarika 19th dec 07 
                myTreeNode myNode = (myTreeNode)trvFaxTo.Nodes[0];

                if ((!object.ReferenceEquals(trvFaxTo.SelectedNode, myNode)))
                {
                    //----- 
                    trvFaxTo.SelectedNode.Remove();
                    trvFaxTo.Refresh();
                    trvFaxTo.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                //  MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
       
        //comented by Mayuri:20090915
        //#region "cmbCategory Events"
        //private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //GetFaxList2();
        //    if (cmbCategory.SelectedItem != null && cmbCategory.SelectedItem.ToString() != "")
        //    {
        //        if (cmbCategory.SelectedItem.ToString() == "Physician")
        //        {
        //            lblSearchOn.Text = "Search";
        //            // lblSearchOn.Text = "Last Name :";
        //        }
        //        else
        //        {
        //            // lblSearchOn.Text = "Name :";
        //        }
        //    }
        //    else
        //    {
        //        lblSearchOn.Text = "Search";
        //        //  lblSearchOn.Text = "Last Name :";
        //    }

        //}//eFax()

        //private void GetFaxCategory()
        //{
        //    cmbCategory.Items.Clear();
        //    cmbCategory.Items.Add(Cat_Physician);
        //    cmbCategory.Items.Add(Cat_Hospital);
        //    cmbCategory.Items.Add(Cat_Insurance);
        //    cmbCategory.Items.Add(Cat_Pharmacy);
        //    cmbCategory.Items.Add(Cat_Other);

        //}

        //#endregion
                
        #region "Commented Code"
           //ClsContactDBLayer ContactDBLayer = new ClsContactDBLayer(true);
        //if (UpdateContactFAXNo(c1FaxList.GetData(c1FaxList.RowSel, COL_ID), txtFAXNo.Text) == false)
        //{
        //    MessageBox.Show("Unable to update the Contact's FAX No.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //}
        //else
        //{
        //    {
        //        int nRow = c1FaxList.RowSel;
        //        c1FaxList.SetData(nRow, COL_FAX, txtFAXNo.Text);
        //        c1FaxList.Row = nRow;
        //    }
        //}


        //long Id;
        //Id = (long)(c1FaxList.Item(c1FaxList.RowSel, COL_ID));


        //TreeNode myNode;

        //for (int i = 0; i <= trvFaxTo.Nodes(0).Nodes.Count - 1; i++)
        //{
        //    myNode = new myTreeNode();
        //    myNode = trvFaxTo.Nodes(0).Nodes(i);
        //    if (myNode.Key == Id)
        //    {
        //        myNode.Tag = txtFAXNo.Text;
        //        return; 
        //    }
        //}
        // }
        //---------------------------------------- 
        //catch (Exception oError)
        //{
        //    MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //} 

        // }

        //public bool UpdateContactFAXNo(long nContactID,string strFAXNo)
        //{
        //    try 
        //    {	        
        //                    // Cmd = New System.Data.SqlClient.SqlCommand("gsp_UpdateContactFAXNo", Conn)
        //        //        Cmd.CommandType = CommandType.StoredProcedure
        //        //        Dim objParam As SqlParameter

        //        //        objParam = Cmd.Parameters.Add("@ContactID", SqlDbType.BigInt)
        //        //        objParam.Direction = ParameterDirection.Input
        //        //        objParam.Value = nContactID

        //        //        objParam = Cmd.Parameters.Add("@FAXNo", SqlDbType.VarChar)
        //        //        objParam.Direction = ParameterDirection.Input
        //        //        objParam.Value = strFAXNo

        //        //        Conn.Open()
        //        //        Cmd.ExecuteNonQuery()
        //        //        Conn.Close()
        //        //        Return True
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    } 
        //}
#endregion

        #region "txtSearchDocument Events"
        private void txtSearchDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 )
                {
                    {
                        if (c1FaxList.RowSel >= 0)
                        {
                            c1FaxList.Select();
                            //CurrentRowIndex = 0 
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtSearchDocument_TextChanged(object sender, EventArgs e)
       {
           // c1FaxList.DataSource = dv;
           // try
           // {

           //     string strSearch;
           //     {
           //         if (txtSearchDocument.Text.Trim() != "")
           //         {
           //             strSearch =   txtSearchDocument.Text.Replace("'", "''");
           //         }
           //         else
           //         {
           //             strSearch = "";
           //         }
           //     }
           //     //Code Added by Mayuri:20090915
           //     //To Made search Generalize means search in all columns without selecting particular cell & then search 
           //     if (lblSearchOn.Text == "Search")
           //     {
           //         HideColumn();
           //         if( (trvContactType.SelectedNode.Text != null) && (trvContactType.SelectedNode.Text != "" ))
           //         {
           //             //If Category is Physician
           //             if (trvContactType.SelectedNode.Text== Cat_Physician)
           //             {
           //                                 //c1FaxList.Cols[COL_ID].Visible = false;
           //                                 //c1FaxList.Cols[COL_PHYMOBILE].Visible = false;
           //                                 //c1FaxList.Cols[COL_PHYEMAIL].Visible = false;
           //                                 //c1FaxList.Cols[COL_PHYURL].Visible = false;
           //                                 //c1FaxList.Cols[COL_PHYDEGREE].Visible = false;
           //                      dv.RowFilter = dv.Table.Columns[COL_PHYFIRSTNAME].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                     dv.Table.Columns[COL_PHYMIDDLECONTACT].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                     dv.Table.Columns[COL_PHYLASTNAME].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                     dv.Table.Columns[COL_PHYPHONE].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                     dv.Table.Columns[COL_PHYFAX].ColumnName + " Like '%" + strSearch + "%'";
           //             }
           //             //If Category other than Physician
           //             else 
           //             {
           //                                 //c1FaxList.Cols[COL_ID].Visible = false;
           //                                 //c1FaxList.Cols[COL_MOBILE ].Visible = false;
           //                                 //c1FaxList.Cols[COL_EMAIL ].Visible = false;
           //                                 //c1FaxList.Cols[COL_URL ].Visible = false;
           //                     dv.RowFilter = dv.Table.Columns[COL_NAME].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                 dv.Table.Columns[COL_CONTACT].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                 dv.Table.Columns[COL_PHONE].ColumnName + " Like '%" + strSearch + "%' OR " +
           //                                 dv.Table.Columns[COL_FAX].ColumnName + " Like '%" + strSearch + "%'  ";
                                                
           //                     }
                                   
           //         }

                    
           //     }
           //     //end code Added by Mayuri
                 
           //         //c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_LASTNAME, false, false, true);
           //         //c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_MIDDLECONTACT, false, false, true);
           //         //c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_LASTNAME, false, false, true);
                   

           //     //}
           //     //
           //    // {
           //     //    if (lblSearchOn.Text == "First Name :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_FIRSTNAME, false, false, true);
           //     //      // c1FaxList.TopRow = c1FaxList.FindRow(strSearch, 1, COL_FIRSTNAME, false, false, true);
           //     //        c1FaxList.TopRow = c1FaxList.Row;
           //     //    }
           //     //    else if (lblSearchOn.Text == "Middle Name :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_MIDDLECONTACT, false, false, true);
           //     //    }
           //         //else if (lblSearchOn.Text == "Last Name :")
           //         //{
           //         //   c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_LASTNAME, false, false, true);
                      
           //         //}
           //     //    else if (lblSearchOn.Text == "Phone :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_PHONE, false, false, true);
           //     //    }
           //     //    else if (lblSearchOn.Text == "Fax :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_FAX, false, false, true);
           //     //    }
           //     //    else if (lblSearchOn.Text == "Name :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_FIRSTNAME, false, false, true);
           //     //    }
           //     //    else if (lblSearchOn.Text == "Contact :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_MIDDLECONTACT, false, false, true);
           //     //    }
           //     //    else if (lblSearchOn.Text == "Phone :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_PHONE, false, false, true);
           //     //    }
           //     //    else if (lblSearchOn.Text == "Fax :")
           //     //    {
           //     //        c1FaxList.Row = c1FaxList.FindRow(strSearch, 1, COL_FAX, false, false, true);
           //     //    }
           //    // }           


           //}

           // catch (Exception ex)
           // {

           //     //MessageBox.Show(ex.ToString, "Fax", MessageBoxButtons.OK, MessageBoxIcon.Error);
           // }
           //Sandip Darade 20100422
           //initially only 1000 records will be pulled from database 
           //records will be now pulled as per user search query
           GetFaxList();

        }


        private void txtSearchDocument_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SearchFlex();
            }

        }


     
        #endregion


       private void tls_Fax_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
           // oDocManager.DocumentProgressEvent += new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);

            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                    FaxSaveNClose(ref oDocManager);
                    if (oDocManager != null)
                    {
                        oDocManager.Dispose();
                        oDocManager = null;
                    }
                    oDialogResultIsOK = true;
                    break;
                case "Cancel":
                    this.Close();
                    break;
                case "Refresh":
                    //20090914
                    _IsRefresh = true;
                     txtSearchDocument.Clear();
                     RefreshData();
                     //if (txtSearchDocument.Text == "")
                     //{
                     //    c1FaxList.Select(0, 0, false);
                     //}
                    lblFaxDetails.Text = "";
                     //Shweta 20091222
                    //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                     //txtFAXNo.Clear();
                    mskFaxNo.Clear();
                    //End code by Shweta 
                    //Commenetd to fix issue:#4986-Can't send one-time fax
                    //After refresh shouldn't select physician
                    //if (trvContactType.Nodes[0].Nodes.Count > 0)
                    //{
                    //    trvContactType.SelectedNode = trvContactType.Nodes[0].Nodes[0];
                    //}
                    //GetFaxList();
                    //Code Added by Mayuri:20091012
                    //After selecting template it should get load
                    if (cmbTemplate.Items.Count > 0)
                    {
                        cmbTemplate.SelectedIndex = 0;

                    }
                    //Code Commented by Mayuri:20091130
                    //We have used cmbTemplate instead of cmbCoverPage
                    //if (cmbCoverPage.Items.Count > 0)
                    //{
                    //    cmbCoverPage.SelectedIndex = 0;
                        
                    //}
                    //End code commented by Mayuri:20091130
                    break;
            }
        }

        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            Application.DoEvents();
                if (Percentage <= pbDocument.Maximum) { pbDocument.Value = Percentage; }
        }

        //void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        //{
        //    Application.DoEvents();
        //    if (Percentage <= pbDocument.Maximum) { pbDocument.Value = Percentage; }
        //}

        #region "Fax Methods"
        private bool FaxDMSDocument()
        {
            Boolean _blnInternetFax = false;
            _blnInternetFax = gloEDocV3Admin.blnIsInternetFaxEnabled;
            SDKInteraction.eDocV3SDKInteraction oPDFToTif = new gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction();
            string _OutFileName = "";
            string _InFileName = "";
          //  int _FaxColNo = 0;
            try
            {

                if (trvContactType.SelectedNode.Text == "Physician")
                {
                    //_FaxColNo = COL_PHYFAX;
                }
                else
                {
                    //_FaxColNo = COL_FAX;
                }
                if ((c1FaxList.RowSel > 0))
                {
                    if (Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX"))) == mskFaxNo.Text)
                    {
                        if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                        {
                            gstrFAXContactPersonFAXNo = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX")));
                            gstrFAXContactPerson = c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FirstName")) + " " + c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("LastName"));

                            //}
                        }
                        else
                        {
                            gstrFAXContactPersonFAXNo = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX")));
                            gstrFAXContactPerson = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, COL_NAME));

                        }

                    }
                    else
                    {


                        gstrFAXContactPersonFAXNo = mskFaxNo.Text;
                        gstrFAXContactPerson = "";
                        lblFaxDetails.Text = "";
                    }
                }
                else
                {


                    gstrFAXContactPersonFAXNo = mskFaxNo.Text;
                    gstrFAXContactPerson = "";
                    lblFaxDetails.Text = "";
                }


                oDocManager_DocumentProgressEvent(0, "");
                ArrayList oPages = new ArrayList();
                byte[] FaxFileStream = null;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                pdftron.PDF.PDFDoc finalPDF = new pdftron.PDF.PDFDoc();


                if (_FAXDocumentType == enmFAXDocType.DMSDoc)
                {
                    for (int _DocCounter = 0; _DocCounter <= oSelectedDocuments.Count - 1; _DocCounter++)
                    {
                        for (int _ContCounter = 0; _ContCounter <= oSelectedDocuments[_DocCounter].Containers.Count - 1; _ContCounter++)
                        {
                            _InFileName = RetrieveFAXDocumentName(".pdf", gloEDocV3Admin.gTemporaryProcessPath);
                            _OutFileName = _InFileName.Replace(".pdf", "_Out.pdf");
                            if (_blnInternetFax == true)
                            {
                                FaxFileStream = null;
                                FaxFileStream = (byte[])oList.GetContainerStream(oSelectedDocuments[_DocCounter].DocumentID, oSelectedDocuments[_DocCounter].Containers[_ContCounter].ContainerID, oSelectedDocuments[_DocCounter].ClinicID, _OpenExternalSource);
                            }
                            oList.GetContainerStream(oSelectedDocuments[_DocCounter].DocumentID, oSelectedDocuments[_DocCounter].Containers[_ContCounter].ContainerID, oSelectedDocuments[_DocCounter].ClinicID, ref _InFileName, _OpenExternalSource);
                            oPages.Clear();
                            for (int i = 0; i <= oSelectedDocuments[_DocCounter].Containers[_ContCounter].Pages.Count - 1; i++)
                            {
                                oPages.Add(oSelectedDocuments[_DocCounter].Containers[_ContCounter].Pages[i].ContainerPageNumber);
                            }
                            if (oPages != null && oPages.Count > 0)
                            {
                                finalPDF = oPDFToTif.GenerateOneFile(oPages, _InFileName, finalPDF);//, _OutFileName);                             
                                if (File.Exists(_InFileName) == true)
                                {
                                    File.Delete(_InFileName);
                                }
                            }
                        }
                    }

                    finalPDF.Save(_OutFileName, 0);
                    finalPDF.Close();
                   
                }
                else if (_FAXDocumentType == enmFAXDocType.OtherPDFDoc)
                {
                    _OutFileName = _PDFDocPath;

                }

                oDocManager_DocumentProgressEvent(30, "");
              if(  SendToDB(_OutFileName, ref finalPDF, ref _blnInternetFax, ref oPDFToTif)==false )
                {
                    if (oList != null)
                    {
                        oList.Dispose();
                        oList = null;
                    }
                    if (finalPDF != null)
                    {
                        finalPDF.Dispose();
                        finalPDF = null;
                    }
              
                    return false;
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                if (finalPDF != null)
                {
                    finalPDF.Dispose();
                    finalPDF = null;
                }
              
                oDocManager_DocumentProgressEvent(20, "");
                oDocManager_DocumentProgressEvent(100, "");


                _bIsClose = true;
            }
            catch (Exception ex)
            {
                _bIsClose = true;
                throw ex;
            }
            finally
            {
                if (File.Exists(_OutFileName) == true)
                {
                    File.Delete(_OutFileName);
                }

                if (File.Exists(_InFileName) == true)
                {
                    File.Delete(_InFileName);
                }
                if (oPDFToTif != null)
                {
                    oPDFToTif.Dispose();
                    oPDFToTif = null;
                }
            }
            return true;
        }

        //added function to implement fax functionality from cda
        private bool FaxCDADocument()
        {
            Boolean _blnInternetFax = false;
            _blnInternetFax = gloEDocV3Admin.blnIsInternetFaxEnabled;
            SDKInteraction.eDocV3SDKInteraction oPDFToTif = new gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction();
            string _OutFileName = "";
            string _InFileName = "";
            //  int _FaxColNo = 0;
            try
            {

                if (trvContactType.SelectedNode.Text == "Physician")
                {
                    //_FaxColNo = COL_PHYFAX;
                }
                else
                {
                    //_FaxColNo = COL_FAX;
                }
                if ((c1FaxList.RowSel > 0))
                {
                    if (Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX"))) == mskFaxNo.Text)
                    {
                        if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                        {
                            gstrFAXContactPersonFAXNo = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX")));
                            gstrFAXContactPerson = c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FirstName")) + " " + c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("LastName"));

                            //}
                        }
                        else
                        {
                            gstrFAXContactPersonFAXNo = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, c1FaxList.Cols.IndexOf("FAX")));
                            gstrFAXContactPerson = Convert.ToString(c1FaxList.GetData(c1FaxList.RowSel, COL_NAME));

                        }

                    }
                    else
                    {


                        gstrFAXContactPersonFAXNo = mskFaxNo.Text;
                        gstrFAXContactPerson = "";
                        lblFaxDetails.Text = "";
                    }
                }
                else
                {


                    gstrFAXContactPersonFAXNo = mskFaxNo.Text;
                    gstrFAXContactPerson = "";
                    lblFaxDetails.Text = "";
                }


                oDocManager_DocumentProgressEvent(0, "");
                ArrayList oPages = new ArrayList();
              //  byte[] FaxFileStream = null;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                pdftron.PDF.PDFDoc finalPDF = new pdftron.PDF.PDFDoc();


               // if (_FAXDocumentType == enmFAXDocType.DMSDoc)
                //{
                    //for (int _DocCounter = 0; _DocCounter <= oSelectedDocuments.Count - 1; _DocCounter++)
                    //{
                       // for (int _ContCounter = 0; _ContCounter <= oSelectedDocuments[_DocCounter].Containers.Count - 1; _ContCounter++)
                       // {
                         //   _InFileName = RetrieveFAXDocumentName(".pdf", gloEDocV3Admin.gTemporaryProcessPath);
                          //  _OutFileName = _InFileName.Replace(".pdf", "_Out.pdf");
                            //if (_blnInternetFax == true)
                            //{
                            //    FaxFileStream = null;
                            //    FaxFileStream = (byte[])oList.GetContainerStream(oSelectedDocuments[_DocCounter].DocumentID, oSelectedDocuments[_DocCounter].Containers[_ContCounter].ContainerID, oSelectedDocuments[_DocCounter].ClinicID);
                            //}
                         //   oList.GetContainerStream(oSelectedDocuments[_DocCounter].DocumentID, oSelectedDocuments[_DocCounter].Containers[_ContCounter].ContainerID, oSelectedDocuments[_DocCounter].ClinicID, _InFileName);
                            //oPages.Clear();
                            //for (int i = 0; i <= oSelectedDocuments[_DocCounter].Containers[_ContCounter].Pages.Count - 1; i++)
                            //{
                            //    oPages.Add(oSelectedDocuments[_DocCounter].Containers[_ContCounter].Pages[i].ContainerPageNumber);
                            //}
                            //if (oPages != null && oPages.Count > 0)
                            //{
                                //finalPDF = oPDFToTif.GenerateOneFile(oPages, _InFileName, finalPDF);//, _OutFileName);                             
                                //if (File.Exists(_InFileName) == true)
                                //{
                                //    File.Delete(_InFileName);
                                //}
                           // }
                      //  }
                   // }

                //    finalPDF.Save(_OutFileName, 0);
                //    finalPDF.Close();

                //}
                //else if (_FAXDocumentType == enmFAXDocType.OtherPDFDoc)
                //{
                    _OutFileName = pdfFileName ;

               // }

                oDocManager_DocumentProgressEvent(30, "");
                if (SendToDB(_OutFileName, ref finalPDF, ref _blnInternetFax, ref oPDFToTif) == false)
                {
                    if (oList != null)
                    {
                        oList.Dispose();
                        oList = null;
                    }
                    if (finalPDF != null)
                    {
                        finalPDF.Dispose();
                        finalPDF = null;
                    }
              
                    return false;
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                if (finalPDF != null)
                {
                    finalPDF.Dispose();
                    finalPDF = null;
                }

                oDocManager_DocumentProgressEvent(20, "");
                oDocManager_DocumentProgressEvent(100, "");


                _bIsClose = true;
            }
            catch (Exception ex)
            {
                _bIsClose = true;
                throw ex;
            }
            finally
            {
                if (File.Exists(_OutFileName) == true)
                {
                    File.Delete(_OutFileName);
                }

                if (File.Exists(_InFileName) == true)
                {
                    File.Delete(_InFileName);
                }
                if (oPDFToTif != null)
                {
                    oPDFToTif.Dispose();
                    oPDFToTif = null;
                }
            }
            return true;
        }

        public void AddCoverPage(string FaxDocumentFilepath,Int64 nContactID)
        {
            try
            {
                int FaxDocumentPageCount = 0;
                ArrayList oPages = null;
 

                String[] fileName = Directory.GetFiles(gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath);
                //Added by Shweta 20091024
                //To search related pdfPath as per contactID and assign that for mergeing the document  
                for (int i = 0; i <= fileName.Length - 1; i++)
                {   
                    //Start/Pending FaxGLO2011-0011669
                    FileInfo info = new FileInfo(fileName[i]);
                      if (info != null)
                      {
                          bool _bResult = info.Name.Contains(nContactID.ToString() + ".pdf");
                          //bool _bResult = fileName[i].Contains(nContactID.ToString() + ".pdf");
                          //IF the result is true only then assign pdf path 
                          if (_bResult == true)
                          {
                              pdfFilepath = fileName[i].ToString();
                              break;
                          }
                          //if (nContactID == 0)
                          //{
                          //    pdfFilepath = fileName[1].ToString();
                          //    break;
                          //}
                          info = null;
                      }
                    //End/Pending FaxGLO2011-0011669
                }
                //End code addding

                if (File.Exists(pdfFilepath) == true)
                {
                    oPages = new ArrayList();
                    pdftron.PDF.PDFDoc oFaxFileDoc = new pdftron.PDF.PDFDoc(FaxDocumentFilepath);
                    FaxDocumentPageCount = oFaxFileDoc.GetPageCount();
                    if (oFaxFileDoc != null)
                    { oFaxFileDoc.Close(); oFaxFileDoc.Dispose(); oFaxFileDoc = null; }
                    for (int i = 1; i <= FaxDocumentPageCount; i++)
                    {
                        //Commented by Mayuri:20091014
                        //To Increment count by 1 only
                       // i = i+1;
                        //End Code commented by Mayuri:20091014
                        oPages.Add(i);
                    }
                     
                    //Changed by Shweta 20091024
                    //Merge the document for sending fax with the coverpage
                    gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction oSDKInteraction = new gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction();

                    oSDKInteraction.MergePagesinExistingDocument(oPages, FaxDocumentFilepath, pdfFilepath,true);
                    oSDKInteraction.Dispose();
                    oSDKInteraction = null;
                    //End code 20091024
                }
            }
            catch (Exception ex)
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Add CoverPage :" + ex.ToString());
            }
        }
        //private void FaxUsingBlackIce(eDocSDKInteraction oPDFToTif ,string  _pdfFileName  )
        //{
        //    string _TifFileName = eDocManager.eDocValidator.GetNewDocumentName(gloEDocumentAdmin.gTemporaryProcessPath, "tif");

        //    try
        //    {
        //        //copy the file 
        //        //add in faxpending_mst

        //        String sFileName = "";
        //        oPDFToTif.PDFToTIFF(_pdfFileName, _TifFileName);
        //        String _tsfilename = "";


        //        if (multipleRecipients == false)
        //        {

        //            //single contact
        //            //copy the tiff file to this path 
        //            sFileName = RetrieveFAXDocumentName(".tif");
        //            _tsfilename = Path.GetFileNameWithoutExtension(sFileName);
        //            File.Copy(_TifFileName, sFileName);
        //            AddPendingFAX(oActionDocument.PatientID , gstrFAXContactPerson, "DMS", gstrFAXContactPersonFAXNo, "", _tsfilename, System.DateTime.Now, "", ".tif", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "true");
        //               _tsfilename = "";
        //        }
        //        else
        //        {
        //            myTreeNode myNode;
        //            //multiple contacts
        //            for (int c = 0; c <= trvFaxTo.Nodes[0].Nodes.Count - 1; c++)
        //            {
        //                sFileName = RetrieveFAXDocumentName(".tif");
        //                myNode = new myTreeNode();
        //                _tsfilename = Path.GetFileNameWithoutExtension(sFileName);
        //                File.Copy(_TifFileName, sFileName);

        //                myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[c];

        //                AddPendingFAX(oActionDocument.PatientID, myNode.Text, "DMS", myNode.Tag.ToString(), "", _tsfilename, System.DateTime.Now, "", ".tif", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "true");
        //                _tsfilename = "";

        //                myNode = null;
        //            }//for

        //        }//else


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
               
        //    }//finally
        //}//FaxUsingBlackIce

        //private void FaxUsingBlackIce(eDocSDKInteraction oPDFToTif, string _pdfFileName)
        //{
        //    string _TifFileName = eDocManager.eDocValidator.GetNewDocumentName(gloEDocumentAdmin.gTemporaryProcessPath, "tif");

        //    try
        //    {
        //        //copy the file 
        //        //add in faxpending_mst

        //        String sFileName = "";
        //        //   oPDFToTif.PDFToTIFF(_pdfFileName, _TifFileName);
        //        String _tsfilename = "";

        //        //create tiff using Black Ice printer drivers

        //        //Check FAX Printer settings are set or not
        //        //If isPrinterSettingsSet(True) = False Then
        //        //                Exit Sub
        //        //End If


        //        try
        //        {
        //            gloEDocumentV3.frmEDocument.SetFAXPrinterDefaultSettings1();
        //        }
        //        catch (Exception fax_ex)
        //        {
        //            MessageBox.Show(fax_ex.ToString(), "gloDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //        //                If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function


        //        pdftron.PDF.PDFDoc opdfdoc = new pdftron.PDF.PDFDoc(_pdfFileName);

        //        pdftron.PDFNet opdfnet = new pdftron.PDFNet();

        //        oprintdoc = new System.Drawing.Printing.PrintDocument();
        //        System.Drawing.Printing.StandardPrintController printControl = new System.Drawing.Printing.StandardPrintController();

        //        oprintdoc.PrintController = printControl;
        //        oprintdoc.PrinterSettings.PrinterName = "Black Ice Tiff";
        //        sFileName = RetrieveFAXDocumentName(".tif");
        //        _tsfilename = Path.GetFileNameWithoutExtension(sFileName);

        //        if (gloEDocumentV3.frmEDocument.SetFAXPrinterDocumentSettings1(_tsfilename) == false)
        //        {
        //            return;
        //        }//if
        //        //  MainMenu.SetFAXPrinterDocumentSettings1(Path.GetFileNameWithoutExtension(sFileName))
        //        gloEDocumentV3.frmEDocument.SetFAXPrinterOutputDirectorySettings1(gstrFAXOutputDirectory);

        //        oprintdoc.DocumentName = _pdfFileName;



        //        if (multipleRecipients == false)
        //        {

        //            //single contact
        //            //copy the tiff file to this path 

        //            //sFileName = RetrieveFAXDocumentName(".tif");
        //            //_tsfilename = Path.GetFileNameWithoutExtension(sFileName);

        //            //if (gloEDocumentV3.frmEDocument.SetFAXPrinterDocumentSettings1(_tsfilename) == false)
        //            //{
        //            //    return;
        //            //}//if

        //            //      File.Copy(_TifFileName, sFileName);
        //            AddPendingFAX(oActionDocument.PatientID, gstrFAXContactPerson, "DMS", gstrFAXContactPersonFAXNo, "", _tsfilename, System.DateTime.Now, "", ".tif", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "true");
        //            _tsfilename = "";
        //        }
        //        else
        //        {
        //            myTreeNode myNode;
        //            //multiple contacts
        //            for (int c = 0; c <= trvFaxTo.Nodes[0].Nodes.Count - 1; c++)
        //            {
        //                sFileName = RetrieveFAXDocumentName(".tif");
        //                myNode = new myTreeNode();
        //                _tsfilename = Path.GetFileNameWithoutExtension(sFileName);
        //                File.Copy(_TifFileName, sFileName);

        //                myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[c];

        //                AddPendingFAX(oActionDocument.PatientID, myNode.Text, "DMS", myNode.Tag.ToString(), "", _tsfilename, System.DateTime.Now, "", ".tif", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "true");
        //                _tsfilename = "";

        //                myNode = null;
        //            }//for

        //        }//else


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {

        //    }//finally
        //}//FaxUsingBlackIce

        public Int64  FaxUsingBlackIce(gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction oPDFToTif, string _pdfFileName)
        {
            string _TifFileName = RetrieveFAXDocumentName(".tif", gloEDocV3Admin.gstrFaxOutputDirectory);//eDocManager.eDocValidator.GetNewDocumentName(gloEDocV3Admin.gTemporaryProcessPath, "tif");
            Int64 _nClinicalFaxID = 0;
            string myTiffFileName = "";
            try
            {
                if (_FaxType == "Clinical Charts")
                {
                    myTiffFileName = "";
                    _PatientID = 0;
                }
                else
                {
                    oPDFToTif.PDFToTIFF(_pdfFileName, _TifFileName);
                    myTiffFileName = Path.GetFileNameWithoutExtension(_TifFileName);
                }
                if (OpenFromCDA == true) //added to implement fax functionality from cda
                {
                    _FaxType = "CDA";
                }
             _nClinicalFaxID=   AddPendingFAX(_PatientID, gstrFAXContactPerson, _FaxType, gstrFAXContactPersonFAXNo, gloEDocV3Admin.gUserName, myTiffFileName, System.DateTime.Now, "", ".tif", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "true");

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();

                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }


                #endregion " Make Log Entry "

            }
            finally
            {

            }//finally
            return _nClinicalFaxID;
        }//FaxUsingBlackIce


        //private String getFaxData(string _pdfFileName)
        //{
        //    String sFileName = "";
        //    String strFileData = "";
        //    try
        //    {
        //        ////get the pdf document
        //        sFileName = RetrieveFAXDocumentName(".pdf", gloEDocV3Admin.gTemporaryProcessPath);
        //        ////use the pdf doc instead of tiff
        //        File.Copy(_pdfFileName, sFileName);
        //        Byte[] oByte = null;
        //        oByte = GenerateBinaryStream(sFileName);                
        //        strFileData = Convert.ToBase64String(oByte);                
        //    }
        //    catch (Exception ex)
        //    {
               
        //    }
        //    finally
        //    {

        //    }
        //    return strFileData;
        //}

       //Start' Huge Pending Fax from DMS

     //public  void eFax(string _pdfFileName)
     //{
     //        String sFileName = "";

     //       try
     //       {

     //           ////get the pdf document
     //           sFileName = RetrieveFAXDocumentName(".pdf", gloEDocV3Admin.gTemporaryProcessPath);

     //           ////use the pdf doc instead of tiff
     //           File.Copy(_pdfFileName, sFileName);

     //           Byte[] oByte = null;

     //           oByte = GenerateBinaryStream(sFileName);

     //           String strFileData = "";

     //           strFileData = Convert.ToBase64String(oByte);

     //           if (multipleRecipients == false)
     //           {

     //               //single contact
     //               //copy the tiff file to this path 

     //               AddPendingFAX(_PatientID, gstrFAXContactPerson, _FaxType, gstrFAXContactPersonFAXNo, gloEDocV3Admin.gUserName, "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");

     //               //// Call Fax function

     //           }
     //           else
     //           {
     //               myTreeNode myNode;
     //               //multiple contacts
     //               for (int c = 0; c <= trvFaxTo.Nodes[0].Nodes.Count - 1; c++)
     //               {
     //                   myNode = new myTreeNode();

     //                   myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[c];
                        

     //                   //If the fax cover page setting is true then check the related pdfFile for selected contact from the treeview 
     //                   if (gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage == true)
     //                   {
     //                       string ID = myNode.Key.ToString();

     //                       if (_pdfFileName.Contains(ID) == true)
     //                       {
     //                           AddPendingFAX(_PatientID, myNode.Text, _FaxType, myNode.Tag.ToString(), "", "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");
     //                       }
     //                   }
     //                   else
     //                   {

     //                       AddPendingFAX(_PatientID, myNode.Text, _FaxType, myNode.Tag.ToString(), "", "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");
     //                   }

     //                  // AddPendingFAX(_PatientID, myNode.Text, "DMS", myNode.Tag.ToString(), gloEDocV3Admin.gUserName, "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");
     //                   myNode = null;
     //               }//for

     //           }//else
     //       }
     //       catch (Exception ex)
     //       {
     //           #region " Make Log Entry "

     //           _ErrorMessage = ex.ToString();
     //           //Code added on 7rd October 2008 By - Sagar Ghodke
     //           //Make Log entry in DMSExceptionLog file for any exceptions found
     //           if (_ErrorMessage.Trim() != "")
     //           {
     //               string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
     //               gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
     //               _MessageString = "";
     //           }

     //           //End Code add
     //           #endregion " Make Log Entry "


     //           MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
     //           //throw ex;
     //       }
     //       finally
     //       {

     //       }
     //   }

        public Int64 eFax(string _pdfFileName)
        {
            Int64 _nClinicalFaxID = 0;
            try
            {
                if (OpenFromCDA == true)//added to implement fax functionality from cda
                {
                    _FaxType = "CDA";

                }
                if (_FaxType == "Clinical Charts")
                {
                    _PatientID = 0;
                }

                _nClinicalFaxID = AddPendingFAX(_PatientID, gstrFAXContactPerson, _FaxType, gstrFAXContactPersonFAXNo, gloEDocV3Admin.gUserName, "", System.DateTime.Now, _pdfFileName, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "


                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
            return _nClinicalFaxID;
        }

        //End/Start' Huge Pending Fax from DMS


        //private void eFax(Byte[] FaxFileBinaryData)
        //{
        //   // String sFileName = "";

        //    try
        //    {

        //        ////get the pdf document


        //        //sFileName = RetrieveFAXDocumentName(".pdf", gloEDocV3Admin.gTemporaryProcessPath);

        //        ////use the pdf doc instead of tiff
        //        //File.Copy(_pdfFileName, sFileName);

        //        //Byte[] oByte = null;

        //        //oByte = GenerateBinaryStream(sFileName);

        //        String strFileData = "";

        //        strFileData = Convert.ToBase64String(FaxFileBinaryData);

        //        if (multipleRecipients == false)
        //        {

        //            //single contact
        //            //copy the tiff file to this path 

        //            AddPendingFAX(_PatientID, gstrFAXContactPerson, "DMS", gstrFAXContactPersonFAXNo, "", "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");

        //            //// Call Fax function

        //        }
        //        else
        //        {
        //            myTreeNode myNode;
        //            //multiple contacts
        //            for (int c = 0; c <= trvFaxTo.Nodes[0].Nodes.Count - 1; c++)
        //            {
        //                myNode = new myTreeNode();


        //                myNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[c];

        //                AddPendingFAX(_PatientID, myNode.Text, "DMS", myNode.Tag.ToString(), "", "", System.DateTime.Now, strFileData, ".pdf", 0, ".docx", 0, "Pending", "", "", "", null, "high", "base64", "", "", "false");
        //                myNode = null;
        //            }//for

        //        }//else
        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "


        //        MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //throw ex;
        //    }
        //    finally
        //    {
              
        //    }
        //}

        private String RetrieveFAXDocumentName(String _Extension,String  _Path) 
        {
            try
            {
                ////String _Path = gloEDocV3Admin.gstrFaxOutputDirectory;
                //String _NewDocumentName = "";
                  


                //DateTime _dtCurrentDateTime = System.DateTime.Now;
                //int i=0;

                //_NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + _Extension;
                //while ((File.Exists(_Path + "\\" + _NewDocumentName) == true) && (i < int.MaxValue))
                //{
                //    i = i + 1;
                //    _NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt")+ "-" + i.ToString() + _Extension;
                //}//while

                //return _Path + "\\" + _NewDocumentName;
                return gloGlobal.clsFileExtensions.NewDocumentName(_Path, _Extension, "MMddyyyyHHmmssffff");
            }
            catch (Exception ex)
            {
                //#region " Make Log Entry "

                //_ErrorMessage = ex.ToString();
                ////Code added on 7rd October 2008 By - Sagar Ghodke
                ////Make Log entry in DMSExceptionLog file for any exceptions found
                //if (_ErrorMessage.Trim() != "")
                //{
                //    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                //    _MessageString = "";
                //}

                ////End Code add
                //#endregion " Make Log Entry "
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                throw;
            }
        }//RetrieveFAXDocumentName1


        //Old Code
        //public Int64 AddPendingFAX(long nPatientID, string sFAXTo, string sFAXTYpe, string sFAXNo, string sLoginUser, string sFileName, DateTime dtFAXDate, string BinaryFile, string EFax_DocumentExtension, enmFAXPriority CurrentFAXPriority, String EFax_CoverPageDocumentExtension, Int32 nNoOfAttempts, String sCurrentStatus, String TransactionID, String Status, String TransResultCode, Byte[] FaxCoverPageBinaryData, String EFax_Resolution, String EFax_DocumentEncodingType, String EFax_DocumentContentType, String EFax_BillingCode, String EFax_Tiff_image_flag)
        //{

        //    SqlConnection objCon = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
        //    SqlCommand objCmd = new SqlCommand();
        //    Int64 nFaxID = 0;              
                                  

        //    try
        //    {
        //        objCmd.CommandType = CommandType.StoredProcedure;
        //        objCmd.CommandText = "Fax_InUpPendingEFAX";

        //        SqlParameter objParaPatientID = new SqlParameter();
        //        {
        //            objParaPatientID.ParameterName = "@PatientID";
        //            objParaPatientID.Value = nPatientID;
        //            objParaPatientID.Direction = ParameterDirection.Input;
        //        }
        //        objCmd.Parameters.Add(objParaPatientID);

        //        SqlParameter objParaFAXTo = new SqlParameter();
        //        {
        //            objParaFAXTo.ParameterName = "@FAXTo";
        //            objParaFAXTo.Value = sFAXTo;
        //            objParaFAXTo.Direction = ParameterDirection.Input;
        //            objParaFAXTo.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaFAXTo);

        //        SqlParameter objParaFAXTYpe = new SqlParameter();
        //        {
        //            objParaFAXTYpe.ParameterName = "@FAXType";
        //            objParaFAXTYpe.Value = sFAXTYpe;
        //            objParaFAXTYpe.Direction = ParameterDirection.Input;
        //            objParaFAXTYpe.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaFAXTYpe);

        //        SqlParameter objParaFAXNo = new SqlParameter();
        //        {
        //            objParaFAXNo.ParameterName = "@FAXNo";
        //            objParaFAXNo.Value = sFAXNo;
        //            objParaFAXNo.Direction = ParameterDirection.Input;
        //            objParaFAXNo.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaFAXNo);

        //        SqlParameter objParaLoginUser = new SqlParameter();
        //        {
        //            objParaLoginUser.ParameterName = "@LoginUser";
        //            objParaLoginUser.Value = sLoginUser;
        //            objParaLoginUser.Direction = ParameterDirection.Input;
        //            objParaLoginUser.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaLoginUser);

        //        SqlParameter objParaFileName = new SqlParameter();
        //        {
        //            objParaFileName.ParameterName = "@FileName";
        //            objParaFileName.Value = sFileName;
        //            objParaFileName.Direction = ParameterDirection.Input;
        //            objParaFileName.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaFileName);

        //        SqlParameter objParaFaxDate = new SqlParameter();
        //        {
        //            objParaFaxDate.ParameterName = "@FAXDate";
        //            objParaFaxDate.Value = dtFAXDate;
        //            objParaFaxDate.Direction = ParameterDirection.Input;
        //            objParaFaxDate.SqlDbType = SqlDbType.DateTime;
        //        }
        //        objCmd.Parameters.Add(objParaFaxDate);

        //        SqlParameter objParaFaxFileBinaryData = new SqlParameter();
        //        {
        //            objParaFaxFileBinaryData.ParameterName = "@FaxFileBinaryData";
        //            objParaFaxFileBinaryData.Value = BinaryFile;
        //            objParaFaxFileBinaryData.Direction = ParameterDirection.Input;
        //            objParaFaxFileBinaryData.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaFaxFileBinaryData);

        //        SqlParameter objParaEFax_DocumentExtension = new SqlParameter();
        //        {
        //            objParaEFax_DocumentExtension.ParameterName = "@EFax_DocumentExtension";
        //            objParaEFax_DocumentExtension.Value = EFax_DocumentExtension;
        //            objParaEFax_DocumentExtension.Direction = ParameterDirection.Input;
        //            objParaEFax_DocumentExtension.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_DocumentExtension);

        //        SqlParameter objParaEFax_CoverPageDocumentExtension = new SqlParameter();
        //        {
        //            objParaEFax_CoverPageDocumentExtension.ParameterName = "@EFax_CoverPageDocumentExtension";
        //            objParaEFax_CoverPageDocumentExtension.Value = EFax_CoverPageDocumentExtension;
        //            objParaEFax_CoverPageDocumentExtension.Direction = ParameterDirection.Input;
        //            objParaEFax_CoverPageDocumentExtension.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_CoverPageDocumentExtension);

        //        SqlParameter objParaFaxPriority = new SqlParameter();
        //        {
        //            objParaFaxPriority.ParameterName = "@FAXPriority";
        //            if (CurrentSendingFAXPriority == enmFAXPriority.NormalPriority)
        //            {
        //                objParaFaxPriority.Value = 0;
        //            }
        //            else if (CurrentSendingFAXPriority == enmFAXPriority.SendImmediately)
        //            {
        //                objParaFaxPriority.Value = 1;
        //            }
        //            objParaFaxPriority.Direction = ParameterDirection.Input;
        //            objParaFaxPriority.SqlDbType = SqlDbType.Bit;
        //        }
        //        objCmd.Parameters.Add(objParaFaxPriority);

        //        SqlParameter objParaNoOfAttempts = new SqlParameter();
        //        {
        //            objParaNoOfAttempts.ParameterName = "@nNoOfAttempts";
        //            objParaNoOfAttempts.Value = nNoOfAttempts;
        //            objParaNoOfAttempts.Direction = ParameterDirection.Input;
        //            objParaNoOfAttempts.SqlDbType = SqlDbType.Int;
        //        }
        //        objCmd.Parameters.Add(objParaNoOfAttempts);

        //        SqlParameter objParaCurrentStatus = new SqlParameter();
        //        {
        //            objParaCurrentStatus.ParameterName = "@sCurrentStatus";
        //            objParaCurrentStatus.Value = sCurrentStatus;
        //            objParaCurrentStatus.Direction = ParameterDirection.Input;
        //            objParaCurrentStatus.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaCurrentStatus);

        //        SqlParameter objParaTransactionID = new SqlParameter();
        //        {
        //            objParaTransactionID.ParameterName = "@TransactionID";
        //            objParaTransactionID.Value = TransactionID;
        //            objParaTransactionID.Direction = ParameterDirection.Input;
        //            objParaTransactionID.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaTransactionID);

        //        SqlParameter objParaStatus = new SqlParameter();
        //        {
        //            objParaStatus.ParameterName = "@Status";
        //            objParaStatus.Value = Status;
        //            objParaStatus.Direction = ParameterDirection.Input;
        //            objParaStatus.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaStatus);

        //        SqlParameter objParaTransResultCode = new SqlParameter();
        //        {
        //            objParaTransResultCode.ParameterName = "@TransResultCode";
        //            objParaTransResultCode.Value = TransResultCode;
        //            objParaTransResultCode.Direction = ParameterDirection.Input;
        //            objParaTransResultCode.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaTransResultCode);

        //        SqlParameter objParaFaxCoverPageBinaryData = new SqlParameter();
        //        {
        //            objParaFaxCoverPageBinaryData.ParameterName = "@FaxCoverPageBinaryData";
        //            objParaFaxCoverPageBinaryData.Value = FaxCoverPageBinaryData;
        //            objParaFaxCoverPageBinaryData.Direction = ParameterDirection.Input;
        //            objParaFaxCoverPageBinaryData.SqlDbType = SqlDbType.Image;
        //        }

        //        objCmd.Parameters.Add(objParaFaxCoverPageBinaryData);

        //        SqlParameter objParaEFax_Resolution = new SqlParameter();
        //        {
        //            objParaEFax_Resolution.ParameterName = "@EFax_Resolution";
        //            objParaEFax_Resolution.Value = EFax_Resolution;
        //            objParaEFax_Resolution.Direction = ParameterDirection.Input;
        //            objParaEFax_Resolution.SqlDbType = SqlDbType.VarChar;
        //        }

        //        objCmd.Parameters.Add(objParaEFax_Resolution);

        //        SqlParameter objParaEFax_DocumentEncodingType = new SqlParameter();
        //        {
        //            objParaEFax_DocumentEncodingType.ParameterName = "@EFax_DocumentEncodingType";
        //            objParaEFax_DocumentEncodingType.Value = EFax_DocumentEncodingType;
        //            objParaEFax_DocumentEncodingType.Direction = ParameterDirection.Input;
        //            objParaEFax_DocumentEncodingType.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_DocumentEncodingType);

        //        SqlParameter objParaEFax_DocumentContentType = new SqlParameter();
        //        {
        //            objParaEFax_DocumentContentType.ParameterName = "@EFax_DocumentContentType";
        //            objParaEFax_DocumentContentType.Value = EFax_DocumentContentType;
        //            objParaEFax_DocumentContentType.Direction = ParameterDirection.Input;
        //            objParaEFax_DocumentContentType.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_DocumentContentType);

        //        SqlParameter objParaEFax_BillingCode = new SqlParameter();
        //        {
        //            objParaEFax_BillingCode.ParameterName = "@EFax_BillingCode";
        //            objParaEFax_BillingCode.Value = EFax_BillingCode;
        //            objParaEFax_BillingCode.Direction = ParameterDirection.Input;
        //            objParaEFax_BillingCode.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_BillingCode);

        //        SqlParameter objParaEFax_Tiff_image_flag = new SqlParameter();
        //        {
        //            objParaEFax_Tiff_image_flag.ParameterName = "@EFax_Tiff_image_flag";
        //            objParaEFax_Tiff_image_flag.Value = EFax_Tiff_image_flag;
        //            objParaEFax_Tiff_image_flag.Direction = ParameterDirection.Input;
        //            objParaEFax_Tiff_image_flag.SqlDbType = SqlDbType.VarChar;
        //        }
        //        objCmd.Parameters.Add(objParaEFax_Tiff_image_flag);

        //        SqlParameter objParaFaxID = new SqlParameter();
        //        {
        //            objParaFaxID.ParameterName = "@FAXID";
        //            objParaFaxID.Value = 0;
        //            objParaFaxID.Direction = ParameterDirection.InputOutput;
        //            objParaFaxID.SqlDbType = SqlDbType.BigInt;
        //        }
        //        objCmd.Parameters.Add(objParaFaxID);
                               
        //        objCmd.Connection = objCon;
        //        objCon.Open();
        //        objCmd.ExecuteNonQuery();

        //        nFaxID = Convert.ToInt64(objParaFaxID.Value);

        //        objCon.Close();
        //        objCmd = null;
        //        objCon = null;

               
        //    }
        //    catch (SqlException ex)
        //    {
        //        #region " Make Log Entry "

        //        _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "


        //        MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
        //        //UpdateLog("clsFAX -- AddPendingFAX -- " + ex.ToString); 
        //    }//catch 
        //    catch (Exception ex1)
        //    {
        //        #region " Make Log Entry "

        //        _ErrorMessage = ex1.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "


        //        //  UpdateLog("clsFAX -- AddPendingFAX -- " + ex.ToString); 
        //       //  MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        //        MessageBox.Show(ex1.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    } //catch

        //    return nFaxID;

        //} //func
        //Start/New Code
        public Int64 AddPendingFAX(long nPatientID, string sFAXTo, string sFAXTYpe, string sFAXNo, string sLoginUser, string sFileName, DateTime dtFAXDate, string BinaryFile, string EFax_DocumentExtension, enmFAXPriority CurrentFAXPriority, String EFax_CoverPageDocumentExtension, Int32 nNoOfAttempts, String sCurrentStatus, String TransactionID, String Status, String TransResultCode, String FaxCoverPageBinaryData, String EFax_Resolution, String EFax_DocumentEncodingType, String EFax_DocumentContentType, String EFax_BillingCode, String EFax_Tiff_image_flag)
        {
            string MyallLength = "";
            //string MyallLength = "sFAXTo " + sFAXTo.Length.ToString() + "sFAXTYpe " + sFAXTYpe.Length.ToString() + "sFAXNo " + sFAXNo.Length.ToString() + "sLoginUser " + sLoginUser.Length.ToString() + "sFileName " + sFileName.Length.ToString() + "BinaryFile " + BinaryFile.Length.ToString() + "EFax_DocumentExtension " + EFax_DocumentExtension.Length.ToString() + "EFax_CoverPageDocumentExtension " + EFax_CoverPageDocumentExtension.Length.ToString() + "nNoOfAttempts " + nNoOfAttempts.ToString().Length.ToString() + "sCurrentStatus " + sCurrentStatus.Length.ToString() + "Status " + Status.Length.ToString() + "TransResultCode " + TransResultCode.Length.ToString() + "FaxCoverPageBinaryData " + FaxCoverPageBinaryData.Length.ToString() + "EFax_Resolution " + EFax_Resolution.Length.ToString() + "EFax_DocumentEncodingType " + EFax_DocumentEncodingType.Length.ToString() + "EFax_DocumentContentType  " + EFax_DocumentContentType.Length.ToString() + "EFax_BillingCode " + EFax_BillingCode.Length.ToString() + "EFax_Tiff_image_flag " + EFax_Tiff_image_flag.Length.ToString();
            SqlConnection objCon = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
            SqlCommand objCmd = new SqlCommand();
            Int64 nFaxID = 0;
            SqlTransaction _transaction = null;
            //SqlParameter _sqlParameter;

            FileStream oFile = null;
            int myMaxLength = (((int)(gloEDocV3Admin.GetMemoryBuferSetting() / 7) * 3)/10);// (10485760/7)*3;
            BinaryReader oReader = null;
            long myIndex = 0;
            String myOutputString = "";
            byte[] bytesRead = null;
            int myBytesRead = 0;
            myMaxLength = ((int)(myMaxLength / 12) * 12);  //For the Multiples of 3
            // SqlParameter _sqlParameterFaxId = null;
            bool isRollback = false;
            bool _isFileExceed = false;

            try
            {
                if (objCon.State == ConnectionState.Closed)
                {
                    objCon.Open();
                }
                _transaction = objCon.BeginTransaction();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "Fax_InUpPendingEFAX";
                objCmd.Transaction = _transaction;
                objCmd.CommandTimeout = 0;
                objCmd.Connection = objCon;


                SqlParameter objParaFAXTo = new SqlParameter();
                {
                    objParaFAXTo.ParameterName = "@FAXTo";
                    objParaFAXTo.Value = sFAXTo;
                    objParaFAXTo.Direction = ParameterDirection.Input;
                    objParaFAXTo.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaFAXTo);

                SqlParameter objParaFAXTYpe = new SqlParameter();
                {
                    objParaFAXTYpe.ParameterName = "@FAXType";
                    objParaFAXTYpe.Value = sFAXTYpe;
                    objParaFAXTYpe.Direction = ParameterDirection.Input;
                    objParaFAXTYpe.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaFAXTYpe);

                SqlParameter objParaFAXNo = new SqlParameter();
                {
                    objParaFAXNo.ParameterName = "@FAXNo";
                    objParaFAXNo.Value = sFAXNo;
                    objParaFAXNo.Direction = ParameterDirection.Input;
                    objParaFAXNo.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaFAXNo);

                SqlParameter objParaLoginUser = new SqlParameter();
                {
                    objParaLoginUser.ParameterName = "@LoginUser";
                    objParaLoginUser.Value = sLoginUser;
                    objParaLoginUser.Direction = ParameterDirection.Input;
                    objParaLoginUser.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaLoginUser);

                SqlParameter objParaFileName = new SqlParameter();
                {
                    objParaFileName.ParameterName = "@FileName";
                    objParaFileName.Value = sFileName;
                    objParaFileName.Direction = ParameterDirection.Input;
                    objParaFileName.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaFileName);

                SqlParameter objParaFaxDate = new SqlParameter();
                {
                    objParaFaxDate.ParameterName = "@FAXDate";
                    objParaFaxDate.Value = dtFAXDate;
                    objParaFaxDate.Direction = ParameterDirection.Input;
                    objParaFaxDate.SqlDbType = SqlDbType.DateTime;
                }
                objCmd.Parameters.Add(objParaFaxDate);


                SqlParameter objParaEFax_DocumentExtension = new SqlParameter();
                {
                    objParaEFax_DocumentExtension.ParameterName = "@EFax_DocumentExtension";
                    objParaEFax_DocumentExtension.Value = EFax_DocumentExtension;
                    objParaEFax_DocumentExtension.Direction = ParameterDirection.Input;
                    objParaEFax_DocumentExtension.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_DocumentExtension);

                SqlParameter objParaEFax_CoverPageDocumentExtension = new SqlParameter();
                {
                    objParaEFax_CoverPageDocumentExtension.ParameterName = "@EFax_CoverPageDocumentExtension";
                    objParaEFax_CoverPageDocumentExtension.Value = EFax_CoverPageDocumentExtension;
                    objParaEFax_CoverPageDocumentExtension.Direction = ParameterDirection.Input;
                    objParaEFax_CoverPageDocumentExtension.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_CoverPageDocumentExtension);

                SqlParameter objParaFaxPriority = new SqlParameter();
                {
                    objParaFaxPriority.ParameterName = "@FAXPriority";
                    if (CurrentSendingFAXPriority == enmFAXPriority.NormalPriority)
                    {
                        objParaFaxPriority.Value = 0;
                    }
                    else if (CurrentSendingFAXPriority == enmFAXPriority.SendImmediately)
                    {
                        objParaFaxPriority.Value = 1;
                    }
                    objParaFaxPriority.Direction = ParameterDirection.Input;
                    objParaFaxPriority.SqlDbType = SqlDbType.Bit;
                }
                objCmd.Parameters.Add(objParaFaxPriority);

                SqlParameter objParaNoOfAttempts = new SqlParameter();
                {
                    objParaNoOfAttempts.ParameterName = "@nNoOfAttempts";
                    objParaNoOfAttempts.Value = nNoOfAttempts;
                    objParaNoOfAttempts.Direction = ParameterDirection.Input;
                    objParaNoOfAttempts.SqlDbType = SqlDbType.Int;
                }
                objCmd.Parameters.Add(objParaNoOfAttempts);

                SqlParameter objParaCurrentStatus = new SqlParameter();
                {
                    objParaCurrentStatus.ParameterName = "@sCurrentStatus";
                    objParaCurrentStatus.Value = sCurrentStatus;
                    objParaCurrentStatus.Direction = ParameterDirection.Input;
                    objParaCurrentStatus.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaCurrentStatus);

                SqlParameter objParaTransactionID = new SqlParameter();
                {
                    objParaTransactionID.ParameterName = "@TransactionID";
                    objParaTransactionID.Value = TransactionID;
                    objParaTransactionID.Direction = ParameterDirection.Input;
                    objParaTransactionID.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaTransactionID);

                SqlParameter objParaStatus = new SqlParameter();
                {
                    objParaStatus.ParameterName = "@Status";
                    objParaStatus.Value = Status;
                    objParaStatus.Direction = ParameterDirection.Input;
                    objParaStatus.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaStatus);

                SqlParameter objParaTransResultCode = new SqlParameter();
                {
                    objParaTransResultCode.ParameterName = "@TransResultCode";
                    objParaTransResultCode.Value = TransResultCode;
                    objParaTransResultCode.Direction = ParameterDirection.Input;
                    objParaTransResultCode.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaTransResultCode);

                SqlParameter objParaFaxCoverPageBinaryData = new SqlParameter();
                {
                    objParaFaxCoverPageBinaryData.ParameterName = "@FaxCoverPageBinaryData";
                    objParaFaxCoverPageBinaryData.Value = FaxCoverPageBinaryData;
                    objParaFaxCoverPageBinaryData.Direction = ParameterDirection.Input;
                    objParaFaxCoverPageBinaryData.SqlDbType = SqlDbType.Image;
                }

                objCmd.Parameters.Add(objParaFaxCoverPageBinaryData);

                SqlParameter objParaEFax_Resolution = new SqlParameter();
                {
                    objParaEFax_Resolution.ParameterName = "@EFax_Resolution";
                    objParaEFax_Resolution.Value = EFax_Resolution;
                    objParaEFax_Resolution.Direction = ParameterDirection.Input;
                    objParaEFax_Resolution.SqlDbType = SqlDbType.VarChar;
                }

                objCmd.Parameters.Add(objParaEFax_Resolution);

                SqlParameter objParaEFax_DocumentEncodingType = new SqlParameter();
                {
                    objParaEFax_DocumentEncodingType.ParameterName = "@EFax_DocumentEncodingType";
                    objParaEFax_DocumentEncodingType.Value = EFax_DocumentEncodingType;
                    objParaEFax_DocumentEncodingType.Direction = ParameterDirection.Input;
                    objParaEFax_DocumentEncodingType.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_DocumentEncodingType);

                SqlParameter objParaEFax_DocumentContentType = new SqlParameter();
                {
                    objParaEFax_DocumentContentType.ParameterName = "@EFax_DocumentContentType";
                    objParaEFax_DocumentContentType.Value = EFax_DocumentContentType;
                    objParaEFax_DocumentContentType.Direction = ParameterDirection.Input;
                    objParaEFax_DocumentContentType.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_DocumentContentType);

                SqlParameter objParaEFax_BillingCode = new SqlParameter();
                {
                    objParaEFax_BillingCode.ParameterName = "@EFax_BillingCode";
                    objParaEFax_BillingCode.Value = EFax_BillingCode;
                    objParaEFax_BillingCode.Direction = ParameterDirection.Input;
                    objParaEFax_BillingCode.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_BillingCode);

                SqlParameter objParaEFax_Tiff_image_flag = new SqlParameter();
                {
                    objParaEFax_Tiff_image_flag.ParameterName = "@EFax_Tiff_image_flag";
                    objParaEFax_Tiff_image_flag.Value = EFax_Tiff_image_flag;
                    objParaEFax_Tiff_image_flag.Direction = ParameterDirection.Input;
                    objParaEFax_Tiff_image_flag.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objParaEFax_Tiff_image_flag);

                ////New Code
                try
                {
                    if (File.Exists(BinaryFile))
                    {
                        oFile = new FileStream(BinaryFile, FileMode.Open, FileAccess.Read);
                        if (oFile.Length > 2147483645)
                        {
                            oFile.Close();
                            oFile.Dispose();
                            oFile = null;
                            MessageBox.Show("File Cannot exceed more than 2 GB", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isFileExceed = true;
                        }
                        if (_isFileExceed == false)
                        {
                            oReader = new BinaryReader(oFile);
                            bytesRead = new byte[myMaxLength];
                            myBytesRead = oReader.Read(bytesRead, (int)myIndex, myMaxLength);
                            myOutputString = Convert.ToBase64String(bytesRead, 0, myBytesRead);
                        }

                    }
                    else
                    {
                        oFile = null;
                        oReader = null;
                        myBytesRead = -1;
                        myOutputString = "";
                    }


                    if (objParaFAXTo != null)
                    {
                        objParaFAXTo = null;
                    }

                    if (objParaFAXTYpe != null)
                    {
                        objParaFAXTYpe = null;
                    }

                    if (objParaFAXNo != null)
                    {
                        objParaFAXNo = null;
                    }


                    if (objParaLoginUser != null)
                    {
                        objParaLoginUser = null;
                    }

                    if (objParaNoOfAttempts != null)
                    {
                        objParaNoOfAttempts = null;
                    }
                    if (objParaFileName != null)
                    {
                        objParaFileName = null;
                    }

                    if (objParaFaxDate != null)
                    {
                        objParaFaxDate = null;
                    }

                    if (objParaEFax_DocumentExtension != null)
                    {
                        objParaEFax_DocumentExtension = null;
                    }

                    if (objParaEFax_CoverPageDocumentExtension != null)
                    {
                        objParaEFax_CoverPageDocumentExtension = null;
                    }

                    if (objParaFaxPriority != null)
                    {
                        objParaFaxPriority = null;
                    }

                    if (objParaCurrentStatus != null)
                    {
                        objParaCurrentStatus = null;
                    }

                    if (objParaTransactionID != null)
                    {
                        objParaTransactionID = null;
                    }

                    if (objParaStatus != null)
                    {
                        objParaStatus = null;
                    }

                    if (objParaTransResultCode != null)
                    {
                        objParaTransResultCode = null;
                    }

                    if (objParaFaxCoverPageBinaryData != null)
                    {
                        objParaFaxCoverPageBinaryData = null;
                    }

                    if (objParaEFax_Resolution != null)
                    {
                        objParaEFax_Resolution = null;
                    }

                    if (objParaEFax_DocumentEncodingType != null)
                    {
                        objParaEFax_DocumentEncodingType = null;
                    }

                    if (objParaEFax_DocumentContentType != null)
                    {
                        objParaEFax_DocumentContentType = null;
                    }

                    if (objParaEFax_BillingCode != null)
                    {
                        objParaEFax_BillingCode = null;
                    }

                    if (objParaEFax_Tiff_image_flag != null)
                    {
                        objParaEFax_Tiff_image_flag = null;
                    }

                }
                catch (Exception ex)
                {
                    #region " Make Log Entry "

                    _ErrorMessage = ex.ToString();
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    #endregion " Make Log Entry "
                }
                if (_isFileExceed == false)
                {
                    //nPatient ID
                    SqlParameter objParaPatientID = new SqlParameter();
                    {
                        objParaPatientID.ParameterName = "@PatientID";
                        objParaPatientID.Value = nPatientID;
                        objParaPatientID.Direction = ParameterDirection.Input;
                    }
                    objCmd.Parameters.Add(objParaPatientID);

                    //BinaryData
                    SqlParameter objParaFaxFileBinaryData = new SqlParameter();
                    {
                        objParaFaxFileBinaryData.ParameterName = "@FaxFileBinaryData";
                        objParaFaxFileBinaryData.Value = myOutputString;
                        objParaFaxFileBinaryData.Direction = ParameterDirection.Input;
                        objParaFaxFileBinaryData.SqlDbType = SqlDbType.VarChar;
                    }
                    objCmd.Parameters.Add(objParaFaxFileBinaryData);

                    //FaxId
                    SqlParameter objParaFaxID = new SqlParameter();
                    {
                        objParaFaxID.ParameterName = "@FAXID";
                        objParaFaxID.Value = 0;
                        objParaFaxID.Direction = ParameterDirection.InputOutput;
                        objParaFaxID.SqlDbType = SqlDbType.BigInt;
                    }
                    objCmd.Parameters.Add(objParaFaxID);


                    objCmd.ExecuteNonQuery();     //iF THE VALUE IS LESS THEN 

                    if (objParaPatientID != null)
                    {
                        objParaPatientID = null;
                    }

                    if (objParaFaxFileBinaryData != null)
                    {
                        objParaFaxFileBinaryData = null;
                    }

                  

                    nFaxID = Convert.ToInt64(objParaFaxID.Value);
                    if (objParaFaxID != null)
                    {
                        objParaFaxID = null;
                    }
                    SqlCommand _mysqlcommand = null;
                    try
                    {

                        //iT IS USED TO UPADATE THE DATA
                        _mysqlcommand = new SqlCommand("Fax_UpPendingEFAX", objCon);
                        _mysqlcommand.CommandType = CommandType.StoredProcedure;
                        _mysqlcommand.CommandTimeout = 0;
                        _mysqlcommand.Transaction = _transaction;
                        SqlParameter _mySqlParameter = new SqlParameter();



                        //iF MBYTES IS LESS THEN 
                        while (myBytesRead == myMaxLength && oReader != null)
                        {
                            myIndex += (long)myOutputString.Length;

                            _mysqlcommand.Parameters.Clear();

                            myBytesRead = oReader.Read(bytesRead, 0, myMaxLength);

                            //myOutputString = string.Empty;

                            myOutputString = Convert.ToBase64String(bytesRead, 0, myBytesRead);

                            //nPatient ID
                            _mySqlParameter = new SqlParameter();
                            _mySqlParameter.ParameterName = "@PatientID";
                            _mySqlParameter.Value = nPatientID;
                            _mySqlParameter.Direction = ParameterDirection.Input;
                            _mysqlcommand.Parameters.Add(_mySqlParameter);

                            //BinaryData
                            _mySqlParameter = new SqlParameter();
                            _mySqlParameter.ParameterName = "@FaxFileBinaryData";
                            _mySqlParameter.Value = myOutputString;
                            _mySqlParameter.Direction = ParameterDirection.Input;
                            _mySqlParameter.SqlDbType = SqlDbType.VarChar;
                            _mysqlcommand.Parameters.Add(_mySqlParameter);

                            //FaxId
                            _mySqlParameter = new SqlParameter();
                            _mySqlParameter.ParameterName = "@FAXID";
                            _mySqlParameter.Value = nFaxID;
                            _mySqlParameter.Direction = ParameterDirection.InputOutput;
                            _mySqlParameter.SqlDbType = SqlDbType.BigInt;
                            _mysqlcommand.Parameters.Add(_mySqlParameter);

                            //oFFsET
                            _mySqlParameter = new SqlParameter();
                            _mySqlParameter.ParameterName = "@offset";
                            _mySqlParameter.Value = myIndex;
                            _mySqlParameter.Direction = ParameterDirection.Input;
                            _mySqlParameter.SqlDbType = SqlDbType.BigInt;
                            _mysqlcommand.Parameters.Add(_mySqlParameter);


                            //lENGTH
                            _mySqlParameter = new SqlParameter();
                            _mySqlParameter.ParameterName = "@length";
                            _mySqlParameter.Value = myOutputString.Length;
                            _mySqlParameter.Direction = ParameterDirection.Input;
                            _mySqlParameter.SqlDbType = SqlDbType.BigInt;
                            _mysqlcommand.Parameters.Add(_mySqlParameter);


                            _mysqlcommand.Connection = objCon;
                            if (objCon.State == ConnectionState.Closed)
                            {
                                objCon.Open();
                            }

                            _mysqlcommand.ExecuteNonQuery();

                            if (_mySqlParameter != null)
                            {
                                _mySqlParameter = null;
                            }
                        }

                    }

                    catch (SqlException ex)
                    {
                        isRollback = true;
                        MessageBox.Show(myIndex.ToString());
                        _transaction.Rollback();
                        _ErrorMessage = ex.Message;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                    }


                    catch (Exception ex)
                    {
                        isRollback = true;
                        MessageBox.Show(myIndex.ToString());
                        _transaction.Rollback();
                        _ErrorMessage = ex.Message;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                    }
                    finally
                    {
                        if (_mysqlcommand != null)
                        {
                            _mysqlcommand.Parameters.Clear();
                            _mysqlcommand.Dispose();
                            _mysqlcommand = null;
                        }
                    }
                    //////////





                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(MyallLength);
                    //objCmd.Connection = objCon;
                    //if (objCon.State == ConnectionState.Closed)
                    //{
                    //    objCon.Open();
                    //}

                }//file size exceeds

                try
                {
                    if (isRollback == false)
                    {
                        _transaction.Commit();
                    }
                    else
                    {
                        isRollback = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                bytesRead = null;
                if (oReader != null)
                {
                    oReader.Close();
                    oReader.Dispose();
                    oReader = null;
                }
                if (oFile != null)
                {
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                }


                objCon.Close();
                objCon.Dispose();
                //objCmd = null;
                objCon = null;


            }
            catch (SqlException ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //UpdateLog("clsFAX -- AddPendingFAX -- " + ex.ToString); 
            }//catch 
            catch (Exception ex1)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex1.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                //  UpdateLog("clsFAX -- AddPendingFAX -- " + ex.ToString); 
                //  MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                MessageBox.Show(ex1.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            } //catch
            finally
            {
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();  
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }

            return nFaxID;

        } //func
        //End/New Code
        private byte[] GenerateBinaryStream(string strfilename)
        {
            FileStream oFile;
            BinaryReader oReader;
            byte[] bytesRead=null;

            try
            {
                // Dim strfilename As String = Application.StartupPath & "\SampleFax.docx" 
                //Const CHUNK_SIZE As Integer = 1024 * 8 
                //8K write buffer. 
                if (File.Exists(strfilename))
                {

                    //'Please uncomment the following line of code to read the file, even the file is in use by same or another process 
                    // oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous); 

                    //'To read the file only when it is not in use by any process 
                    oFile = new FileStream(strfilename, FileMode.Open, FileAccess.Read);

                    oReader = new BinaryReader(oFile);

                    bytesRead = new byte[oReader.BaseStream.Length];
                    oReader.Read(bytesRead, 0, bytesRead.Length);
                    oFile.Close();
                    oReader.Close();
                    oFile.Dispose();
                    oFile = null;
                    oReader.Dispose();
                    oReader = null;
                    return bytesRead;
                }

                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                throw ex;
            }
        }

        private void cmbCoverPage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Code Commneted by Mayuri:20091130
            //We have used cmbTemplate instead of cmbCoverPage
            //try
            //{
            //    if (Convert.ToInt64(cmbCoverPage.SelectedValue) > 0)
            //    {
            //        LoadFaxDocument();
            //    }
            //    else
            //    {
            //        //wdFaxCoverpage.Close();
            //        dsoFAXPreview.Close();
            //    }
               

            //}
            //catch (Exception ex)
            //{

            //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Error While loading Fax Coverpage" + ex.ToString());
            //}
            //End code Commneted by Mayuri:20091130
        }

        //Changes By Shweta 20091130
        //Added Parameter for templateID to load the selected template for respective receiver
        //public void LoadFaxDocument()
        public void LoadFaxDocument(Int64 nTemplateID)
        {
        //    FileStream oFileStream = null;
          //  FileInfo oFiledata = default(System.IO.FileInfo);
            try
            {
                pdfFilepath = "";
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                ConverPageTemplateName = gloEDocumentV3.gloEDocV3Admin.CoverLetterDocumentName;
                
                //Commented By Shweta 20091130
                // oList.GetContainerStream(Convert.ToInt64(cmbCoverPage.SelectedValue), ConverPageTemplateName);
                //oList.GetContainerStream(Convert.ToInt64(cmbTemplate.SelectedValue), ConverPageTemplateName);
                oList.GetContainerStream(nTemplateID, ConverPageTemplateName);//Pass th templateID
                oList.Dispose();
                oList = null;
                if (oCurDoc != null) { oCurDoc = null; }
                //if ((oCurDoc != null))
                //{
                   
                //    Marshal.ReleaseComObject(oCurDoc);
                //    oCurDoc = null;
                //}
            //    if (oWordApp != null) { oWordApp = null; }
                //if ((oWordApp != null))
                //{

                //    Marshal.ReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
                //wdFaxCoverpage.Close();
                dsoFAXPreview.Close();
                if (File.Exists(ConverPageTemplateName) == true)
                {
                  //  dsoFAXPreview.Open(ConverPageTemplateName);
                    object thisObject = (object)ConverPageTemplateName;
                  
                    gloWord.LoadAndCloseWord.OpenDSO(ref dsoFAXPreview, ref thisObject, ref oCurDoc, ref oWordApp);
                    ConverPageTemplateName = (string)thisObject;

                    //wdFaxCoverpage.Open(ConverPageTemplateName);
                    //oCurDoc = (Wd.Document)wdFaxCoverpage.ActiveDocument;
                    oCurDoc = (Wd.Document)dsoFAXPreview.ActiveDocument;
                 //   oWordApp = oCurDoc.Application;
                    oCurDoc.ActiveWindow.SetFocus();
                    gloOffice.Supporting.WdApplication = oCurDoc.Application;
                    gloOffice.Supporting.CurrentDocument = oCurDoc;
                    gloOffice.Supporting.PatientID = PatientID;
                    gloOffice.Supporting.ClinicID = gloEDocumentV3.gloEDocV3Admin.gClinicID;
                    gloOffice.Supporting.DataBaseConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;
                    gloOffice.Supporting.GetFormFieldData(null);
                    oCurDoc.Save();
                  //  dsoFAXPreview.Save();
                    gloWord.LoadAndCloseWord.SaveDSO(ref dsoFAXPreview, ref oCurDoc, ref oWordApp);
                    //Commented by Shweta 20091020
                    //This code has shifted in c1FaxList_EnterCell() for saving the document against the selected patient to send fax 
                    //oFiledata = new System.IO.FileInfo(ConverPageTemplateName);
                    //pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                    //object missing = System.Reflection.Missing.Value;
                    //oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForPrint, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                    //end commenting by Shweta 20091020
                }
            }
            //Code Added by Mayuri:20091015
            //Give message if word updates are not installed
            catch (COMException ex)
            {
                int nErrorCode = ex.ErrorCode;
                if (nErrorCode == -2147467259)
                {
                    MessageBox.Show("Unable to send fax with cover page as word updates are not installed properly,Please install Addins for the MS-Word", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            //End Code Added by Mayuri:20091015
            catch (Exception ex)
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Load Fax Document :" + ex.ToString());
            }
            
            finally
            {
            //    if (oFileStream != null) { oFileStream.Close(); oFileStream.Dispose();}
            }
        }

        private void trvContactType_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            trvContactType.SelectedNode = e.Node;
            txtSearchDocument.Text = "";
            GetFaxList();
            //Sandip Darade 201010204
            lblFaxDetails.Text = "";
            mskFaxNo.Text = "";
            
            if (trvContactType.SelectedNode.Text != "Contacts") //added for bugid Bug #73910 
            {
                c1FaxList.Row = 0;
            }
             
        }

        private void tlb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                _IsRefresh = true;
                RefreshData();
            }
            catch (Exception ex)
            {
             // gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                MessageBox.Show(ex.Message);
            }

            //strFAXType = "DMS";
            //try
            //{ 
            //        int i = 0;
            //        string strText = null;
            //        if ((oCurDoc == null) == false)
            //        {
            //            foreach (Wd.FormField oField in oCurDoc.FormFields)
            //            {
            //                strText = oField.StatusText;
            //                switch (strText.ToString().Trim())
            //                {
            //                    case "FAX.FAXTo":
                                  
            //                        oField.Result = lblFaxDetails.Text;
            //                        break;
            //                    case "FAX.FAXNo":
            //                        //Shwtea 20091222
            //                        //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
            //                        //oField.Result = txtFAXNo.Text;
            //                        oField.Result = mskFaxNo.Text;
            //                        //End code by Shweta 
            //                        break;
            //                    case "FAX.FAXType":
            //                        oField.Result = strFAXType;
            //                        break;
            //                }
            //            }
            //        }
            
            //}
            //catch (Exception ex)
            //{ }
            //try
            //{
            ////lblSearchOn.Text = "";
            //txtSearchDocument.Clear();
            //lblFaxDetails.Text = "";
            
            //txtFAXNo.Clear();
            //if (trvContactType.Nodes[0].Nodes.Count > 0)
            //{
            //    //myNode = trvContactType.SelectedNode .Nodes[0].Nodes[0];
            //    //trvContactType.SelectedNode = myNode;
            //    trvContactType.SelectedNode = trvContactType.Nodes[0].Nodes[0];
            //}
            //if (cmbCoverPage.Items.Count > 0)
            //{
            //    //cmbCoverPage .SelectedItem
            //    cmbCoverPage.SelectedIndex = 0;
            //}
           // try
           // {
           //      int i;
           //String strText;
           // if (oCurDoc!=null)
           // {
           //     for (i = 1 ;i< oCurDoc.FormFields.Count;i++)
           //         //strText =oCurDoc .FormFields
           //         strText = oCurDoc.FormFields[i].StatusText;
           //    switch  (strText)
           //    {
                          
           //        case FAX.FAXTo:
                
           //                 oCurDoc.FormFields.Item(i).Result = lblContactPerson.Text;
                
           //        case FAX.FAXNo:
                
           //                 oCurDoc.FormFields.Item(i).Result = txtFAXNo.Text;
                
           //        case FAX.FAXType:
                
           //                 oCurDoc.FormFields.Item(i).Result = strFAXType;
                
           // }
                
           // }

        //   }
        //   catch (Exception ex)
        //   {
        //      // gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

        //    MessageBox.Show(ex.Message);
        //   }
            
        //}
        
        //private bool GetFaxDirSettings()
        //{
        //    RegistryKey regKey;

        //    if ((Registry.LocalMachine.OpenSubKey("Software\\gloEMR") == null) == true)
        //    {
        //        return false;
        //    }
        //    regKey = Registry.LocalMachine.OpenSubKey("Software\\gloEMR", true);
          
          
        //    if ((regKey.GetValue("FAXOutputDirectory") == null) == false)
        //    {
        //        gstrFAXOutputDirectory = (regKey.GetValue("FAXOutputDirectory")).ToString();
        //    }      

        //  regKey.Close();

        //    return true;
        } 

        #endregion
        //Added by Mayuri"20100223-issue#4986-Can't send one-time fax
        private void RefreshData()
        {
            //strFAXType = "DMS";
            try
            {
               // int i = 0;
                string strText = null;
                if (_IsRefresh == true)
                {
                    
                    txtSearchDocument.Clear();
                    if (txtSearchDocument.Text == "")
                    {

                        c1FaxList.Select(0, 0, false);
                    }
                }
                if ((oCurDoc == null) == false)
                {
                    foreach (Wd.FormField oField in oCurDoc.FormFields)
                    {
                        strText = oField.StatusText;
                        switch (strText.ToString().Trim())
                        {
                            case "FAX.FAXTo":

                                oField.Result = lblFaxDetails.Text;
                                break;
                            case "FAX.FAXNo":
                                //Shwtea 20091222
                                //Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                                //oField.Result = txtFAXNo.Text;
                                oField.Result = mskFaxNo.Text;
                                //End code by Shweta 
                                break;
                            case "FAX.FAXType":
                                if (OpenFromCDA == true)  //added for Bug #74538:
                                {
                                    oField.Result = "CDA";
                                }
                                else
                                {
                                oField.Result = _FaxType;
                                }
                                    break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                _IsRefresh = false;
            }

        }
        //Code Added by Mayuri:20091012
        //after selecting template it should get load
        private void cmbTemplate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(cmbTemplate.SelectedValue) > 0)
                {
                    //Changed By Shweta 20091130
                    //Load the selected template in the preview panel 
                    //LoadFaxDocument();
                    LoadFaxDocument(Convert.ToInt64(cmbTemplate.SelectedValue));//pass Template ID
                   
                }
                else
                {
                    //wdFaxCoverpage.Close();
                    dsoFAXPreview.Close();
                }


            }
            catch (Exception ex)
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Error While loading Fax Coverpage" + ex.ToString());
            }

        }


        private void optNormalPriority_CheckedChanged(object sender, EventArgs e)
        {
           
             if (optNormalPriority.Checked == true)
                {
                    optNormalPriority.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                {
                    optNormalPriority.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                }
        }

          
        private void optHighPriority_CheckedChanged(object sender, EventArgs e)
        {

            if (optHighPriority.Checked == true)
            {
                optHighPriority.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                optHighPriority.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void btnUp1_Click_1(object sender, EventArgs e)
        {
            pnlc1FaxListHeader.Visible = false;
            btnDown1.Visible = true;
            btnDown1.BackgroundImage = gloEDocumentV3.Properties.Resources.Down;
            btnDown1.BackgroundImageLayout = ImageLayout.Center;
            btnUp1.Visible = false;
        }

        private void btnDown1_Click_1(object sender, EventArgs e)
        {
             pnlc1FaxListHeader.Visible = true;
             btnDown1.Visible = false;
             btnUp1.Visible = true;
             btnUp1.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
             btnUp1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            pnldsoFAXPreview.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
            pnlc1FaxListHeader.Dock = DockStyle.Top;
            panel5.Dock = DockStyle.Top;
            panel5.BringToFront();
            pnldsoFAXPreview.BringToFront();
            //btnUp.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
            //btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            pnldsoFAXPreview.Visible = false;
            btnDown.Visible = true;
            //btnDown.BackgroundImage = gloEDocumentV3.Properties.Resources.Down;
            //btnDown.BackgroundImageLayout = ImageLayout.Center;
            btnUp.Visible = false;
           
            pnlc1FaxListHeader.Dock = DockStyle.Fill;
            panel5.Dock = DockStyle.Bottom;
            if (pnldsoFAXPreview.Visible== false)
            {
                pnlc1FaxListHeader.BringToFront();
            }
            btnDown.BackgroundImage = gloEDocumentV3.Properties.Resources.Down ;
            btnDown.BackgroundImageLayout = ImageLayout.Center;


        }

        private void btnUp1_MouseHover(object sender, EventArgs e)
        {
            btnUp1.BackgroundImage = gloEDocumentV3.Properties.Resources.UPHover;
            btnUp1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp1_MouseLeave(object sender, EventArgs e)
        {
            btnUp1.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
            btnUp1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown1_MouseHover(object sender, EventArgs e)
        {
            btnDown1.BackgroundImage = gloEDocumentV3.Properties.Resources.DownHover;
            btnDown1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown1_MouseLeave(object sender, EventArgs e)
        {
            btnDown1.BackgroundImage = gloEDocumentV3.Properties.Resources.Down;
            btnDown1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = gloEDocumentV3.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = gloEDocumentV3.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }
        private void btnUp_MouseHover(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = gloEDocumentV3.Properties.Resources.UPHover;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = gloEDocumentV3.Properties.Resources.UP;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void trvFaxTo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Code Added by Mayuri:20091026
            try
            {
                trvFaxTo.SelectedNode = e.Node;
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //Code Added by Mayuri:20091014
                    //To check whether root node selected if so contextmenu shouldn't display
                    if (trvFaxTo.Nodes[0].Nodes.Count > 0)
                    {
                        if ((!object.ReferenceEquals(trvFaxTo.SelectedNode, trvFaxTo.Nodes[0])))
                        {
                            trvFaxTo.ContextMenu = cmnuDeleteFaxTo;


                        }
                        else
                        {
                            trvFaxTo.ContextMenu = null;
                        }
                    }
                    else
                    {
                        trvFaxTo.ContextMenu = null;
                    }
                   
                }
                else
                {
                    trvFaxTo.ContextMenu = null;
                }

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                //MessageBox.Show(ex.ToString, "DMS Fax", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //End Code Added by Mayuri:20091026

        }

        private void txtFAXNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code added by dipak to validate txtFAXNo fo numeric with max lenght 10 digit
            if (e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
                return;
            }
            if (txtFAXNo.Text.Length <10)
            {
                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }  
            else
            {
                e.Handled = true;
            }
        }

        private void FaxSaveNClose(ref eDocManager.eDocManager oDocManager)
        {
            try
            {
                if (trvFaxTo.Nodes[0].Nodes.Count <= 0)
                {
                    if (mskFaxNo.Text == "")
                    {
                        MessageBox.Show("Please enter the FAX No", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        if (_IsInternetFax == true)
                        {
                            if (mskFaxNo.IsValidated == false)
                            {
                                return;
                            }
                        }
                    }
                }
                Application.DoEvents();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Fax  - End" + " " + DateTime.Now.TimeOfDay);
                pbDocument.Minimum = 0;
                pbDocument.Maximum = 100;
                pbDocument.Value = 0;
                pnlFaxProgressBar.Visible = true;
                if (optNormalPriority.Checked)
                {
                    CurrentSendingFAXPriority = enmFAXPriority.NormalPriority;

                }
                else
                {
                    CurrentSendingFAXPriority = enmFAXPriority.SendImmediately;
                }
                Application.DoEvents();
                _bIsClose = true;
                if (OpenFromCDA == false) //added if condition to implement fax functionality from cda
                {
                    if (FaxDMSDocument() == false)
                    {
                        return;
                    }
                    else
                    { this.DocumentFaxed = true; }
                }
                else
                {
                    if (FaxCDADocument()==false) ///added function to implement fax functionality from cda
                    {
                        return;
                    }
                    else
                    { this.DocumentFaxed = true; }
                }
                Application.DoEvents();

                if (_bIsClose == true)
                {
                    this.Close();
                }
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Fax  - End" + " " + DateTime.Now.TimeOfDay);

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, "Fax Send", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
                #endregion " Make Log Entry "
            }
            finally
            {
                oDocManager.Dispose();
            }
        }
        
        public bool SendToDB(string _OutFileName, ref pdftron.PDF.PDFDoc finalPDF, ref bool _blnInternetFax, ref SDKInteraction.eDocV3SDKInteraction oPDFToTif)
        {
            string myActualFile = String.Empty;
            string _Result = string.Empty;
            bool _bMessageshown = false;
            Int64 _nClinicalFaxID = 0;
            try
            {
                if (trvFaxTo.Nodes[0].Nodes.Count > 0)
                {
                    multipleRecipients = true;
                }
                myArrLst = CreateCoverPage();
                if (myArrLst != null)
                {
                    if (multipleRecipients == true)
                    {
                        for (int i = 0; i <= trvFaxTo.Nodes[0].Nodes.Count - 1; i++)
                        {
                            if (myArrLst.Count > i)
                            {
                                pdfFilepath = myArrLst[i].ToString();
                            }
                            String sCoverPageBinary = "";
                            myTreeNode newtreeNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[i];
                            gstrFAXContactPerson = newtreeNode.Text.ToString();
                            gstrFAXContactPersonFAXNo = trvFaxTo.Nodes[0].Nodes[i].Tag.ToString();
                            //Fixed issue#75511-gloEMR: Fax- Application sends fax without fax number
                            if (gstrFAXContactPersonFAXNo == "")
                            {
                                MessageBox.Show("Please enter the FAX No", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                int rowIndex = 0;

                                rowIndex = c1FaxList.FindRow(Convert.ToString(newtreeNode.Key), 0, 0, false, true, false);
                                c1FaxList.Select(rowIndex, 0, true);
                                mskFaxNo.Focus();
                                return false;
                            }
                            if (_FaxType == "Clinical Charts")
                            {

                                sCoverPageBinary = ConvertCoverPagetoBinary(pdfFilepath);

                                // _dtFaxDetails.Rows.Add(new object[] { _nClinicalFaxID, sCoverPageBinary });

                            }
                            else
                            {

                                AddCoverPage(_OutFileName, pdfFilepath, ref myActualFile);
                                if (_bMessageshown != true)
                                {
                                    if (ValidatePagelimit(ref myActualFile, ref _Result) == false)
                                    {
                                        finalPDF.Dispose();
                                        finalPDF = null;
                                        return true;
                                    }
                                    else
                                    {
                                        _bMessageshown = true;
                                        finalPDF.Dispose();
                                        finalPDF = null;
                                    }
                                }
                                else
                                {
                                    if (finalPDF != null)
                                    {
                                        finalPDF.Dispose();
                                        finalPDF = null;
                                    }
                                }
                            }
                            //ValidFilePDFDocumentAfterMerging(myActualFile, ref _Result);
                            //string[] _sMessage =_Result.Trim().Split('_');
                            //if (_sMessage!=null)
                            //{
                            //    if (_sMessage[1].ToString() == "warning")
                            //    {
                            //        if (MessageBox.Show(this, "The fax you are sending is " + _sMessage[0].ToString() +" pages long.  Are you sure you want to send a fax this long?", "gloDMS", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                            //        {
                            //            return;
                            //        }
                            //    }
                            //    else if (_sMessage[1].ToString() == "error")
                            //    {
                            //        MessageBox.Show(this, "The fax you are sending is " + _sMessage[2].ToString() + " long.  This exceeds the maximum page limit for faxes of  " + _sMessage[0].ToString() + " pages, which is set in EMR Admin.  The fax will not be sent.", "gloDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);                              
                            //       return;                                
                            //    }
                            //    else
                            //    {
                            //       // continue; 
                            //    }
                            //}
                            _nClinicalFaxID = SaveToAppropriateFormat(ref _blnInternetFax, ref  oPDFToTif, ref  myActualFile);
                            if (_FaxType == "Clinical Charts")
                            {
                                //_dtFaxDetails.Rows.Add(new object[] { _nClinicalFaxID, sCoverPageBinary });

                                _dtFaxDetails.Rows.Add(_dtFaxDetails.NewRow());

                                _dtFaxDetails.Rows[_dtFaxDetails.Rows.Count - 1]["nFaxID"] = _nClinicalFaxID;
                                _dtFaxDetails.Rows[_dtFaxDetails.Rows.Count - 1]["FaxCoverPageBinary"] = sCoverPageBinary;

                            }
                            //if (_blnInternetFax == false)
                            //{
                            //    FaxUsingBlackIce(oPDFToTif, myActualFile);

                            //}
                            //else
                            //{
                            //    eFax(myActualFile);
                            //}
                        }

                    }
                    else
                    {
                         string sCoverPageBinary = "";
                         if (_FaxType == "Clinical Charts")
                         {

                             sCoverPageBinary = ConvertCoverPagetoBinary(pdfFilepath);
                         }
                         else
                         {
                             AddCoverPage(_OutFileName, pdfFilepath, ref myActualFile);

                             if (ValidatePagelimit(ref myActualFile, ref _Result) == false)
                             {
                                 return true;
                             }
                         }
                         _nClinicalFaxID = SaveToAppropriateFormat(ref _blnInternetFax, ref  oPDFToTif, ref  myActualFile);

                         if (_FaxType == "Clinical Charts")
                         {
                             //_dtFaxDetails.Rows.Add(new object[] { _nClinicalFaxID, sCoverPageBinary });

                             _dtFaxDetails.Rows.Add(_dtFaxDetails.NewRow());

                             _dtFaxDetails.Rows[_dtFaxDetails.Rows.Count - 1]["nFaxID"] = _nClinicalFaxID;
                             _dtFaxDetails.Rows[_dtFaxDetails.Rows.Count - 1]["FaxCoverPageBinary"] = sCoverPageBinary;

                         }


                        //if (_blnInternetFax == false)
                        //{
                        //    FaxUsingBlackIce(oPDFToTif, myActualFile);

                        //}
                        //else
                        //{
                        //    eFax(myActualFile);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                myArrLst.Clear();
                if (pdfFilepath != null && pdfFilepath != "")
                {
                    if (File.Exists(pdfFilepath))
                    {
                        try
                        {
                            File.Delete(pdfFilepath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Cannot able to Deleted." + ex.ToString(), "gloDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            return true;
        }

        private String ConvertCoverPagetoBinary(string BinaryFile)
        {

            FileStream oFile = null;
            int myMaxLength = (((int)(gloEDocV3Admin.GetMemoryBuferSetting() / 7) * 3) / 10);
            BinaryReader oReader = null;
            long myIndex = 0;
            String myOutputString = "";
            byte[] bytesRead = null;
            int myBytesRead = 0;
            myMaxLength = ((int)(myMaxLength / 12) * 12);
            try
            {
                if (File.Exists(BinaryFile))
                {
                    oFile = new FileStream(BinaryFile, FileMode.Open, FileAccess.Read);

                    oReader = new BinaryReader(oFile);
                    bytesRead = new byte[myMaxLength];
                    myBytesRead = oReader.Read(bytesRead, (int)myIndex, myMaxLength);
                    myOutputString = Convert.ToBase64String(bytesRead, 0, myBytesRead);


                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

            }
            finally
            {
                if (oFile != null)
                {
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                }
                bytesRead = null;
                if (oReader != null)
                {
                    oReader.Close();
                    oReader.Dispose();
                    oReader = null;
                }

            }
            return myOutputString;
        }

        private bool  ValidatePagelimit(ref string myActualFile,ref string _Result)
        {
            ValidFilePDFDocumentAfterMerging(myActualFile, ref _Result);
          
            string[] _sMessage = _Result.Trim().Split('-');
            if (_sMessage != null && _sMessage[0] != "")
            {
                if (_sMessage[1].ToString() == "warning")
                {
                    if (MessageBox.Show(this, "The fax you are sending is " + _sMessage[0].ToString() + " pages long." + Environment.NewLine + "Are you sure you want to send a fax this long?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
                else if (_sMessage[1].ToString() == "error")
                {
                    MessageBox.Show(this, "You are attempting to send a fax that will be  " + _sMessage[2].ToString() + " pages long." + Environment.NewLine + "This exceeds the maximum page limit for faxes," + Environment.NewLine + "which is set to " + _sMessage[0].ToString() + " pages in gloEMR Admin." + Environment.NewLine + "BECAUSE THE PAGE LIMIT IS EXCEEDED, THIS FAX WILL NOT BE SENT.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    return true;
                    // continue; 
                }
            }
            
              return true;
           
        }
        public Int64 SaveToAppropriateFormat(ref bool _blnInternetFax, ref SDKInteraction.eDocV3SDKInteraction oPDFToTif, ref string myActualFile)
        {
            Int64 _nClinicalFaxID = 0;
            if (_blnInternetFax == false)
            {
                _nClinicalFaxID = FaxUsingBlackIce(oPDFToTif, myActualFile);

            }
            else
            {
                _nClinicalFaxID = eFax(myActualFile);
            }
            return _nClinicalFaxID;
        }
        public string getFaxPagesSetting(string sSettingName)
        {
            string result = string.Empty;
            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            DataTable dt = null;
            try
            {
                oDB.Connect(false);
                string _strSQL = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings where sSettingsName='" + sSettingName + "'";

                oDB.Retrive_Query(_strSQL, out dt);
               
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["sSettingsValue"].ToString();
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
                if (dt != null)
                {                   
                    dt.Dispose();
                    dt = null;
                }
            }

            return result;

        }

        public void ValidFilePDFDocumentAfterMerging(string PDFFilePath,ref string _Result)
        {
             try
            {
                string _FaxExceeds = getFaxPagesSetting("FAXEXCEEDSPAGES");
                string _Allowfaxes = getFaxPagesSetting("ALLOWFAXESPAGES");
                _Result = string.Empty;

                using (pdftron.PDF.PDFDoc myPDFDoc = new pdftron.PDF.PDFDoc(PDFFilePath))
                {
                    Int32 cnt = myPDFDoc.GetPageCount();
                    if (_FaxExceeds != "")
                    {
                        if (cnt > Convert.ToInt32(_FaxExceeds))
                        {
                            _Result = _FaxExceeds + "-" + "warning";
                        }
                        //else
                        //{
                        //    _Result = string.Empty;
                        //}
                    }
                    else
                    {
                        _Result = string.Empty;
                    }

                    if (_Allowfaxes != "")
                    {
                        if (cnt > Convert.ToInt32(_Allowfaxes))
                        {
                            _Result = _Allowfaxes + "-" + "error" + "-" + cnt.ToString();
                        }
                        //else
                        //{
                        //    _Result = string.Empty;
                        //}
                    }
                    //else
                    //{
                    //    _Result = string.Empty;
                    //}
                   
                }
                
            }
             catch (Exception ex)
             {

                 gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Add CoverPage :" + ex.ToString());
             }

        }
        public void AddCoverPage(string FaxDocumentFilepath, string pdfFilepath, ref string myPDFFilePath)
        {
            try
            {
                int FaxDocumentPageCount = 0;
                ArrayList oPages = null;
   

             //  String[] fileName = Directory.GetFiles(gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath);
                if (File.Exists(pdfFilepath) == true)
                {
                    oPages = new ArrayList();
                    pdftron.PDF.PDFDoc oFaxFileDoc = new pdftron.PDF.PDFDoc(FaxDocumentFilepath);
                    FaxDocumentPageCount = oFaxFileDoc.GetPageCount();
                    if (oFaxFileDoc != null)
                    { oFaxFileDoc.Close(); oFaxFileDoc.Dispose(); oFaxFileDoc = null; }
                    for (int i = 1; i <= FaxDocumentPageCount; i++)
                    {

                        oPages.Add(i);
                    }
                    gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction oSDKInteraction = new gloEDocumentV3.SDKInteraction.eDocV3SDKInteraction();
                    oSDKInteraction.MergePagesinExistingDocument(oPages, FaxDocumentFilepath, pdfFilepath, true);
                    oSDKInteraction.Dispose();
                    oSDKInteraction = null;
                    myPDFFilePath = pdfFilepath;
                }
                else
                {

                    myPDFFilePath = FaxDocumentFilepath;
                }
               

            }
            catch (Exception ex)
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Add CoverPage :" + ex.ToString());
            }
        }

        public ArrayList CreateCoverPage()
        {
            myArrLst = new ArrayList();
            if (oCurDoc != null)
            {
                if (dsoFAXPreview.DocumentName != "" && dsoFAXPreview.DocumentName != null)
                {
                    oCurDoc = (Wd.Document)dsoFAXPreview.ActiveDocument;
                }


                if (multipleRecipients == true)
                {
                    for (int i = 0; i <= trvFaxTo.Nodes[0].Nodes.Count - 1; i++)
                    {
                        myTreeNode newtreeNode = (myTreeNode)trvFaxTo.Nodes[0].Nodes[i];
                        string mystringText = newtreeNode.Text.ToString();
                        string newTextNumber = trvFaxTo.Nodes[0].Nodes[i].Tag.ToString();
                        try
                        {


                            if (trvFaxTo.Nodes[0].Nodes.Count > 0)
                            {
                                lblFaxDetails.Text = mystringText;
                                mskFaxNo.Text = newTextNumber;
                            }
                            else
                            {

                                lblFaxDetails.Text = mystringText;
                                mskFaxNo.Text = newTextNumber;

                            }


                        }
                        catch (COMException ex)
                        {
                            int nErrorCode = ex.ErrorCode;
                            if (nErrorCode == -2147467259)
                            {
                                MessageBox.Show("Unable to send fax with cover page as word updates are not installed properly,Please install Addins for the MS-Word", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }

                        gloOffice.Supporting.WdApplication = oCurDoc.Application;
                        gloOffice.Supporting.CurrentDocument = oCurDoc;
                        gloOffice.Supporting.PatientID = PatientID;
                        gloOffice.Supporting.ClinicID = gloEDocumentV3.gloEDocV3Admin.gClinicID;
                        gloOffice.Supporting.DataBaseConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;

                        RefreshData();

                        String newSelectedPatient = gloOffice.Supporting.NewDocumentName();
                        object oFileName = (object)newSelectedPatient;
                        object missing = System.Reflection.Missing.Value;
                        try
                        {
                            oCurDoc.SaveAs(ref oFileName, ref missing, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problem in saving the file." + ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        oCurDoc.Saved = true;
                        System.IO.FileInfo oFiledata = new System.IO.FileInfo(Convert.ToString(oFileName));
                        pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                        oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                        myArrLst.Add(pdfFilepath);
                        oFiledata = null;
                    }
                }
                else
                {
                    try
                    {

                        if (mskFaxNo.IsValidated == true)
                        {
                            if (c1FaxList.RowSel > 0)
                            {
                                if (trvContactType.SelectedNode.Text == Cat_Physician || trvContactType.SelectedNode.Text == Cat_PatientContacts || trvContactType.SelectedNode.Text == Cat_Referrals || trvContactType.SelectedNode.Text == Cat_PrimaryCarePhysician || trvContactType.SelectedNode.Text == Cat_OtherCareTeam)
                                {
                                    gstrFAXContactPerson = lblFaxDetails.Text;
                                    gstrFAXContactPersonFAXNo = mskFaxNo.Text;

                                }

                            }
                        }
                        else
                        {
                            return null;
                        }

                    }
                    catch (COMException ex)
                    {
                        int nErrorCode = ex.ErrorCode;
                        if (nErrorCode == -2147467259)
                        {
                            MessageBox.Show("Unable to send fax with cover page as word updates are not installed properly,Please install Addins for the MS-Word", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    gloOffice.Supporting.WdApplication = oCurDoc.Application;
                    gloOffice.Supporting.CurrentDocument = oCurDoc;
                    gloOffice.Supporting.PatientID = PatientID;
                    gloOffice.Supporting.ClinicID = gloEDocumentV3.gloEDocV3Admin.gClinicID;
                    gloOffice.Supporting.DataBaseConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;

                    RefreshData();

                    String newSelectedPatient = gloOffice.Supporting.NewDocumentName();
                    object oFileName = (object)newSelectedPatient;
                    object missing = System.Reflection.Missing.Value;
                    try
                    {
                        oCurDoc.SaveAs(ref oFileName, ref missing, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem in saving the file." + ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    oCurDoc.Saved = true;
                    System.IO.FileInfo oFiledata = new System.IO.FileInfo(Convert.ToString(oFileName));
                    pdfFilepath = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oFiledata.Name.Replace(oFiledata.Extension, ".pdf");
                    oCurDoc.ExportAsFixedFormat(pdfFilepath, Wd.WdExportFormat.wdExportFormatPDF, false, Wd.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Wd.WdExportRange.wdExportAllDocument, 1, 1, Wd.WdExportItem.wdExportDocumentContent, false, true, Wd.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, ref missing);
                    myArrLst.Add(pdfFilepath);
                    oFiledata = null;
                }


            }
            return myArrLst;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
          txtSearchDocument.ResetText();
          txtSearchDocument.Focus();
        }

    }//class
}//namespace
