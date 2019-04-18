using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.Enumeration;
using System.IO;


namespace gloEDocumentV3.Forms
{
    public partial class frmEDocEvent_ExportToPDF : Form
    {

        //setting of the object of the collection class which is saved in the glodocv3..
        //where we have saved the public property for the variable...
        gloEDocumentV3.Document.Documents _oDocument = new gloEDocumentV3.Document.Documents();

        //Setting of the Column name with the integervalue... 
        private const int COL_DOC_SELECT = 0;
        private const int COL_DOC_NAME = 1;
        private const int COL_DOC_PATH = 2;
        private const int COL_DOC_TYPE = 3;
        private const int COL_DOC_HiddenPath = 4;
        private const int COL_DOC_HiddenPageNo = 5;
        private const int COL_DOC_DocumentName = 6;

        private const int COL_DOC_COUNT = 7;

        //
        private DocumentContextMenu.eContextEventParameter _EventParameter = null;


        //Globally setting of the File's folder path when exporting the Document..
        public string FileSaveFolderPath = "";


        public DocumentContextMenu.eContextEventParameter oEventParameter
        {
            get { return _EventParameter; }
            set { _EventParameter = value; }
        }

        //constructor with no argument..
        public frmEDocEvent_ExportToPDF()
        {
            InitializeComponent();
        }

        //Constructor with single argument passed(Argument passed from the frmEDocEvent_Recievedfax under Export button
        //and the assigned the value to object for the internal purpuse...
        public frmEDocEvent_ExportToPDF(gloEDocumentV3.Document.Documents oDocument)
        {
            InitializeComponent();
            _oDocument = oDocument;
        }


        //setting of the selectedDocument and getting the value from the Recieved fax..
        System.Collections.ArrayList _SelectedDocuments = null;
        public ArrayList oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        /// <summary>
        /// It is used to Close the frmEDocEvent_ExportToPDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void frmEDocEvent_ExportToPDF_Load(object sender, EventArgs e)
        {



            //Design to load the form..
            designC1Documents();

            //used for the Duplicate records 
            bool IsRecordPresent = false;

            for (int i = 0; i <= _oDocument.Count - 1; i++)
            {

                IsRecordPresent = false;
                for (int j = 0; j <= c1Documents.Rows.Count - 1; j++)
                {
                    //checking for the duplicate record form the c1flexgrid
                    //c1Flexgrid                              //object of the Collection class
                    if (Convert.ToString(c1Documents.GetData(j, COL_DOC_DocumentName)) == _oDocument[i].Documentname)
                    {
                        IsRecordPresent = true;
                        break;
                    }
                }

                //Checked if present the insert only once into the grid
                //retriving only the folder under and showing into the grid 
                if (IsRecordPresent == false)
                {
                    c1Documents.Rows.Add();
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_SELECT, _oDocument[i].DocumentSelect);
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_DocumentName, _oDocument[i].Documentname);
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_PATH, _oDocument[i].DocumentPath);
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPath, _oDocument[i].DocumentHiddenPath);
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPageNo, _oDocument[i].DocumentHiddenPageNo);
                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_NAME, _oDocument[i].PageName);
                }

            }
            pbDocument.Minimum = 0;
            pbDocument.Maximum = 100;
            pbDocument.Value = 0;

        }
        private void designC1Documents()
        {
            try
            {


                c1Documents.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
                c1Documents.Rows.Count = 1;
                c1Documents.Rows.Fixed = 1;
                c1Documents.Cols.Count = COL_DOC_COUNT;
                c1Documents.Cols.Fixed = 0;


                //at the row 0 storing the Column heading
                //c1Documents.SetData(0, 0, "Select");
                c1Documents.SetData(0, 6, "Document Name");
                c1Documents.Cols[COL_DOC_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Documents.Cols[COL_DOC_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Documents.Cols[COL_DOC_DocumentName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                //TO Set the Editing to False
                c1Documents.Cols[COL_DOC_NAME].AllowEditing = true;
                c1Documents.Cols[COL_DOC_SELECT].AllowEditing = true;
                c1Documents.Cols[COL_DOC_PATH].AllowEditing = false;
                c1Documents.Cols[COL_DOC_TYPE].AllowEditing = false;
                c1Documents.Cols[COL_DOC_HiddenPath].AllowEditing = false;
                c1Documents.Cols[COL_DOC_HiddenPageNo].AllowEditing = false;
                c1Documents.Cols[COL_DOC_DocumentName].AllowEditing = false;

                c1Documents.Cols[COL_DOC_SELECT].DataType = typeof(System.Boolean);

                //Here we have setted the path
                c1Documents.Cols[COL_DOC_SELECT].Width = 0;
                c1Documents.Cols[COL_DOC_NAME].Width = 0;
                c1Documents.Cols[COL_DOC_PATH].Width = 0;
                c1Documents.Cols[COL_DOC_TYPE].Width = 0;
                c1Documents.Cols[COL_DOC_HiddenPath].Width = 0;
                c1Documents.Cols[COL_DOC_HiddenPageNo].Width = 0;
                c1Documents.Cols[COL_DOC_DocumentName].Width = 200;


                //Here Which column to be visible
                c1Documents.Cols[COL_DOC_NAME].Visible = false;
                c1Documents.Cols[COL_DOC_SELECT].Visible = false;
                c1Documents.Cols[COL_DOC_PATH].Visible = false;
                c1Documents.Cols[COL_DOC_TYPE].Visible = false;
                c1Documents.Cols[COL_DOC_HiddenPath].Visible = false;
                c1Documents.Cols[COL_DOC_HiddenPageNo].Visible = false;
                c1Documents.Cols[COL_DOC_DocumentName].Visible = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {

            }
        }



        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtSourceDocument.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the Document Name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                
                //Here Checked for the No's of rows Present in the c1flexgrid..
                if (c1Documents.Rows.Count > 0)
                {
                    for (int k = 0; k <= c1Documents.Rows.Count - 1; k++)
                    {
                        //When same name is Present then Show the MessageBox in the FlexGrid..
                        if (txtSourceDocument.Text == c1Documents.Rows[k][COL_DOC_DocumentName].ToString())
                        {

                            MessageBox.Show("Document with the same name already exists. Please enter another Name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSourceDocument.Select(0, txtSourceDocument.Text.Length);
                            return;

                        }
                        //Here checked for the Current selected row If Present then Rename It's Value..



                    }
                    //If there is row selected then retive the document name..
                    if (c1Documents.Selection.ContainsRow(c1Documents.RowSel))
                    {
                        //checking for the rows in the flexgrid
                        for (int l = 0; l <= _oDocument.Count - 1; l++)
                        {
                            //Checking for the Documentname changed then again store it into the collection class
                            if (_oDocument[l].Documentname.Trim() == c1Documents.GetData(c1Documents.RowSel, COL_DOC_DocumentName).ToString().Trim())
                            {
                                _oDocument[l].Documentname = txtSourceDocument.Text.Trim();
                            }
                        }
                        c1Documents.SetData(c1Documents.RowSel, COL_DOC_DocumentName, txtSourceDocument.Text.Trim());
                        pnl_Raname.Visible = false;
                        panel1.Visible = true;
                        txtSourceDocument.Text = "";
                        return;

                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtSourceDocument_TextChanged(object sender, EventArgs e)
        {

            //validatio done for the Rename textbox (to not ,to take the wildcard character..)
            string sFileName = txtSourceDocument.Text.Trim();
            string sValidFileName = "";
            sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
            if (sFileName != sValidFileName)
            {
                txtSourceDocument.Text = sValidFileName;
                txtSourceDocument.Select(txtSourceDocument.Text.Length, 1);
            }

        }

        private void tls_MaintainDoc_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tlb_CancelMain_Click(object sender, EventArgs e)
        {
            this.Close();
            
           

        }

        private void tlb_RenameMain_Click(object sender, EventArgs e)
        {
            //It will show the selected file into the Label
            if (c1Documents.Rows.Count > 0)
            {

                if (c1Documents.Selection.ContainsRow(c1Documents.RowSel))
                {
                    txtSourceDocument.Text = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_DOC_DocumentName));
                }
            }

            pnl_Raname.Visible = true;
            panel1.Visible = false;

        }

        private void tsb_CancelRename_Click(object sender, EventArgs e)
        {
            pnl_Raname.Visible = false;
            panel1.Visible = true;
            txtSourceDocument.Text = "";
        }
        
        private void c1Documents_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //taking the selected doument and the axis of the column hitted..
                c1Documents.Select(c1Documents.HitTest(e.X, e.Y).Row, COL_DOC_DocumentName);
                //checking for the mouse button clicked Right..
                if (e.Button == MouseButtons.Right)
                {
                    #region " Mouse Right Click functionality "

                    int _rowIndex = c1Documents.RowSel;//c1Documents.HitTest(e.X, e.Y).Row;

                    if (_rowIndex >= 0)
                    {
                        c1Documents.Select(_rowIndex, 1);

                        if (c1Documents.GetData(_rowIndex, COL_DOC_DocumentName) != null)
                        {
                            if (c1Documents.Selection.ContainsRow(c1Documents.RowSel))
                            {
                                //it will fire the Contextmanu..
                                c1Documents.ContextMenu = cmnuToRename;
                               if(c1Documents.Rows.Count == 1  || c1Documents.RowSel == 0 )
                               {
                                   c1Documents.ContextMenu = null;
                               }
                           }

                        }
                        else
                        {
                            c1Documents.ContextMenu = null;
                        }
                    }
                }
                else
                {
                    c1Documents.ContextMenu = null;
                }

                    #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }

        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            tlb_RenameMain_Click(sender, e);
        }
        
        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            Application.DoEvents();
            if (Percentage <= pbDocument.Maximum) { pbDocument.Value = Percentage; }
        }

        private void tlb_ExportMain_Click(object sender, EventArgs e)
        {
            //Variable Declaration
            Application.DoEvents();
            eDocManager.eDocManager oDocumentManager = new gloEDocumentV3.eDocManager.eDocManager();
            DMSImport.DMSImportReport oFileName = new DMSImport.DMSImportReport();
            bool _oDialogResultIsOk = false;
            ArrayList oDocumentPath = new ArrayList();
            string sDocumentName = "";
            DirectoryInfo oinfo;
            FileInfo[] oFileinfo;
            bool _isRecordPresented = false;
            sDocumentName = "";
            bool _isRecordUnsuccessfull = false;


            pbDocument.Minimum = 0;
            pbDocument.Maximum = 100;
            pbDocument.Value = 0;

            try
            {
                //taken the dialogbox of the folder path
                if (c1Documents.Rows.Count > 1)
                {

                    if (dlgFolderPath.ShowDialog(this) == DialogResult.OK)
                    {
                        //saving the selected folder path
                        FileSaveFolderPath = dlgFolderPath.SelectedPath;
                        //directory info oject, used to store the information of the directory..
                        oinfo = new DirectoryInfo(FileSaveFolderPath);


                        oDocManager_DocumentProgressEvent(10, "");
                        //when new document name Enter into the list clear it 
                        for (int i = 0; i <= _oDocument.Count - 1; i++)
                        {
                            Application.DoEvents();
                            //It will call the Progress Event 
                           // oDocumentManager.DocumentProgressEvent += new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);

                            if (oDocumentPath != null)
                            {
                                oDocumentPath = null;
                                oDocumentPath = new ArrayList();
                            }

                            //It is used for checking of the Dupication Generation of the csv file (data)
                            if (sDocumentName == _oDocument[i].Documentname)
                            {
                                _isRecordPresented = false;
                                sDocumentName = _oDocument[i].Documentname;

                            }
                            else
                            {
                                sDocumentName = _oDocument[i].Documentname;
                                _isRecordPresented = true;

                            }

                            oDocManager_DocumentProgressEvent(25, "");

                            oFileinfo = oinfo.GetFiles(sDocumentName.ToString() + @".pdf");

                            //check the file length if present or not 
                            if (oFileinfo.Length <= 0)
                            {
                                Application.DoEvents();
                                for (int j = i; j <= _oDocument.Count - 1; j++)
                                {

                                    if (sDocumentName == _oDocument[j].Documentname)
                                    {
                                        oDocumentPath.Add(_oDocument[j].DocumentHiddenPath);
                                    }
                                    else
                                    {
                                        i = j - 1;
                                        break;
                                    }

                                    if (_isRecordPresented == true)
                                    {
                                        eDocManager.eDocManager.GenerateImportReport(sDocumentName, _oDocument[i].DocumentHiddenPath, "Sucessfull", "File Successfully Created");
                                        _isRecordPresented = false;

                                    }

                                }
                                // It Will Generate the Report
                                _oDialogResultIsOk = oDocumentManager.ImportImages(oDocumentPath, FileSaveFolderPath, sDocumentName);
                                oDocManager_DocumentProgressEvent(50, "");

                            }
                            else
                            {
                                if (_isRecordPresented == true)
                                {
                                    eDocManager.eDocManager.GenerateImportReport(sDocumentName, _oDocument[i].DocumentHiddenPath, "Unsucessfull", "File with same name already exist");
                                    _isRecordUnsuccessfull = true;
                                }

                                //_isUnsuccessfullyCreated = true;



                            }
                        }
                        oDocManager_DocumentProgressEvent(75, "");
                        oDocManager_DocumentProgressEvent(100, "");
                        if (_isRecordUnsuccessfull == true)
                        {
                            MessageBox.Show("Document not exported,file name already present", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Document exported successfully", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("No Document Available To Export", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        

        }

        private void tlb_ImportReport_Click(object sender, EventArgs e)
        {
            DMSImport.DMSImportReport frmImportReport = new DMSImport.DMSImportReport();
            try
            {

                frmImportReport.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                frmImportReport.Dispose();
            }



        }

        private void tlbRemove_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.Document.ExportDocument oExportDocument = new gloEDocumentV3.Document.ExportDocument();
            if (c1Documents.Selection.ContainsRow(c1Documents.RowSel))
            {
                //checking for the rows in the flexgrid
                for (int l = _oDocument.Count - 1; l> 0; l--)
                {
                    //Checking for the Documentname changed then again store it into the collection class
                    if (_oDocument[l].Documentname.Trim() == c1Documents.GetData(c1Documents.RowSel, COL_DOC_DocumentName).ToString().Trim())
                    {
                        oExportDocument.Documentname = _oDocument[l].Documentname;
                       _oDocument.Remove(oExportDocument);                       
                     }
                }
                c1Documents.RemoveItem(c1Documents.RowSel);
            }
    
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            tlbRemove_Click(sender, e);
        }


    }






}

