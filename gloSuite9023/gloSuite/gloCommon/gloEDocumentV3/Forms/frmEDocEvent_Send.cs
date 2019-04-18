using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using gloEDocumentV3.DocumentContextMenu;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_Send : Form
    {
        public frmEDocEvent_Send()
        {
            InitializeComponent();
            _SelectedDocuments = new DocumentContextMenu.eContextDocuments();
            _EventParameter = new DocumentContextMenu.eContextEventParameter();
        }

        #region "Page Manuipulation Variables"
        public bool oDialogResultIsOK = false;
        public Int64 oClinicID = 0;
        public Int64 oDialogDocumentID = 0;
        public Int64 oDialogContainerID = 0;
        public Int64 ContaineraID = 0;
        public string _ErrorMessage = "";
        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;

        private DocumentContextMenu.eContextDocuments _SelectedDocuments = null;

        private DocumentContextMenu.eContextEventParameter _EventParameter = null;

        public DocumentContextMenu.eContextEventParameter oEventParameter
        {
            get { return _EventParameter; }
            set { _EventParameter = value; }
        }

        public DocumentContextMenu.eContextDocuments oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }
               
        #endregion

        private void frmEDocEvent_Send_Load(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            lvwDocuments.Items.Clear();
            txtDocumentName.Text = "";
            gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            try
            {

  

                string _ExceptDocumentIDs = "(";
                for (int i = 0; i <= _SelectedDocuments.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        _ExceptDocumentIDs = _ExceptDocumentIDs + _SelectedDocuments[i].DocumentID.ToString();
                    }
                    else
                    {
                        _ExceptDocumentIDs = _ExceptDocumentIDs + "," + _SelectedDocuments[i].DocumentID.ToString();
                    }
                }
                if (_ExceptDocumentIDs == "(")
                {
                    _ExceptDocumentIDs = _ExceptDocumentIDs + "0)";
                }
                else
                {
                    _ExceptDocumentIDs = _ExceptDocumentIDs + ")";
                }

                oBaseDocuments = oList.GetBaseDocumentsExcepts(_EventParameter.PatientID, _EventParameter.Category, _EventParameter.Year, _ExceptDocumentIDs, gloEDocV3Admin.gClinicID,_OpenExternalSource);

                #region "Destination Document"
                    lvwDocuments.View = View.Details;
                    if (oBaseDocuments != null)
                    {
                        lvwDocuments.Columns.Add("Document");
                        lvwDocuments.Columns.Add("Container ID");
                        lvwDocuments.Columns.Add("Document ID");
                        lvwDocuments.Columns.Add("Category");
                        lvwDocuments.Columns.Add("Year");
                        lvwDocuments.Columns.Add("Month");

                     
                        for (int i = 0; i <= oBaseDocuments.Count - 1; i++)
                        {
                            for (int j = 0; j <= oBaseDocuments[i].EContainers.Count - 1; j++)
                            {
                                ListViewItem oItem = new ListViewItem();
                                oItem.Text = oBaseDocuments[i].DocumentName;
                                oItem.SubItems.Add(oBaseDocuments[i].EContainers[j].EContainerID.ToString());
                                oItem.SubItems.Add(oBaseDocuments[i].EContainers[j].EDocumentID.ToString()); // oSelectedDocuments[i]. Containers Containers[j]. oSelectedDocuments[i].DocumentID.ToString());
                                oItem.SubItems.Add(oBaseDocuments[i].Category);//  oSelectedDocuments[i].Category);
                                oItem.SubItems.Add(oBaseDocuments[i].Year);
                                oItem.SubItems.Add(oBaseDocuments[i].Month);
                                lvwDocuments.Items.Add(oItem);
                                oItem = null;
                            }
                        }

                        lvwDocuments.Columns[0].Width = lvwDocuments.Width - 30;
                        lvwDocuments.Columns[1].Width = 0;
                        lvwDocuments.Columns[2].Width = 0;
                        lvwDocuments.Columns[3].Width = 0;
                        lvwDocuments.Columns[4].Width = 0;
                        lvwDocuments.Columns[5].Width = 0;
                    }
                #endregion  

                

                    lvwDocuments.Enabled = true;
                    txtSearch.Enabled = true;
                    txtDocumentName.Enabled = false;

                    if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument)
                    {
                        txtSearch.Enabled = false;
                        lvwDocuments.Enabled = false;
                        txtDocumentName.Enabled = true;
                                               
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {
                                txtDocumentName.Text = oSelectedDocuments[0].DocumentName.Trim().ToString();
                            }
                            else
                            {
                                txtDocumentName.Text = eDocManager.eDocValidator.GetNewDocumentName(oSelectedDocuments[0].PatientID, _EventParameter.Category.ToString(), _EventParameter.ClinicID, _OpenExternalSource);
                            }
                        }
                        else
                        {
                            txtDocumentName.Text = eDocManager.eDocValidator.GetNewDocumentName(oSelectedDocuments[0].PatientID, _EventParameter.Category.ToString(), _EventParameter.ClinicID, _OpenExternalSource);
                        }
                    }
                    
                              
                    if (oEventParameter.EventType ==   gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument || oEventParameter.EventType ==  gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName)
                    {
                        if (_EventParameter.DocumentName.Trim() != "")
                        {
                            for (int i = 0; i <= lvwDocuments.Items.Count - 1; i++)
                            {
                                if (lvwDocuments.Items[i].Text == _EventParameter.DocumentName)
                                {
                                    lvwDocuments.Items[i].Selected = true;
                                    lvwDocuments.EnsureVisible(i);
                                    break;
                                }
                            }
                                                       
                            if (oSelectedDocuments != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    txtDocumentName.Text = _SelectedDocuments[0].DocumentName;
                                }
                                else
                                {
                                    txtDocumentName.Text = _EventParameter.DocumentName;
                                }
                            }
                            else
                            {
                                txtDocumentName.Text = _EventParameter.DocumentName;
                            }
                            
                        }
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
                MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            finally
            {
                if (oBaseDocuments != null)
                {
                    oBaseDocuments.Dispose();
                    oBaseDocuments = null;
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
                try
                {

                    lvwDocuments.SelectedItems.Clear();
                    if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument || oEventParameter.EventType ==  gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName)
                    {
                        string _SearchText = txtSearch.Text + e.KeyChar.ToString();

                        if (_SearchText.Trim() != "")
                        {
                            ListViewItem oFoundItem = lvwDocuments.FindItemWithText(_SearchText, false, 0, true);
                            if (oFoundItem != null)
                            {
                                oFoundItem.Selected = true;
                                lvwDocuments.TopItem = oFoundItem;
                                txtDocumentName.Text = oFoundItem.Text;
                                //txtSearch.Text = oFoundItem.Text;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
          }

         
        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResultIsOK = false;
            this.Close();
        }

        private void txtDocumentName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string sFileName = txtDocumentName.Text.Trim();
                string sValidFileName = "";
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");

                if (sFileName != sValidFileName)
                {
                    txtDocumentName.Text = sValidFileName;
                    txtDocumentName.Select(txtDocumentName.Text.Length, 1);
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


                string _ex = ex.Message;
            }
        }

        private void lvwDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDocuments.Items.Count > 0)
            {
                if (lvwDocuments.SelectedItems.Count > 0)
                {
                    // txtSearch.Text = lvwDocuments.SelectedItems[0].Text;
                    txtDocumentName.Text = lvwDocuments.SelectedItems[0].Text;

                    ListViewItem oItem = lvwDocuments.SelectedItems[0];
                    _EventParameter.DocumentName = oItem.Text;
                    _EventParameter.ContainerID= Convert.ToInt64(oItem.SubItems[1].Text.ToString());
                    _EventParameter.DocumentID= Convert.ToInt64(oItem.SubItems[2].Text.ToString());
                    _EventParameter.Category = oItem.SubItems[3].Text.ToString();
                    _EventParameter.Year = oItem.SubItems[4].Text.ToString();
                    _EventParameter.Month = oItem.SubItems[5].Text.ToString();
                    oItem = null;                        
                }
            }
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            try
            {

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Send To Category - START" + " " + DateTime.Now.TimeOfDay);

                if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument || oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName)
                {
                    for (int i =0; i <= oSelectedDocuments.Count - 1; i++)
                    {
                        for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                        {
                            //ContaineraID = 0;
                            //ContaineraID = oManager.GetContainerID(oEventParameter.DocumentID,oSelectedDocuments[i].Containers[j].DocumentPageFrom, oSelectedDocuments[i].Containers[j].DocumentPageTo, oSelectedDocuments[i].ClinicID);
                            if(_EventParameter.ContainerID <=0 && _EventParameter.DocumentID <= 0) //if (ContaineraID <= 0 || _EventParameter.DocumentID <= 0)
                            {
                                MessageBox.Show("Select document to Merge.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    
                    
                }
                if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument)
                {

                    if (txtDocumentName.Text.Trim() == "")
                    {
                        System.Windows.Forms.MessageBox.Show("Enter document name.", gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        txtDocumentName.Focus();
                        return;
                    }
                    if (_EventParameter.IsPageMenu == false)
                    {
                        if (eDocManager.eDocValidator.IsDocumentNameExists(txtDocumentName.Text.Trim(), _EventParameter.PatientID, _EventParameter.Category,_EventParameter.SubCategory, _EventParameter.ClinicID, _OpenExternalSource) == true)
                        {
                            MessageBox.Show("Document with same name already exist", gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            txtDocumentName.Focus();
                            return;
                        }
                        if (oManager.SendtoNewDocument(_SelectedDocuments, _EventParameter, txtDocumentName.Text.Trim(), out  oDialogDocumentID, out oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    else if (_EventParameter.IsPageMenu == true)
                    {
                        if (eDocManager.eDocValidator.IsDocumentNameExists(txtDocumentName.Text.Trim(), _EventParameter.PatientID, _EventParameter.Category,_EventParameter.SubCategory, _EventParameter.ClinicID, _OpenExternalSource) == true)
                        {
                            System.Windows.Forms.MessageBox.Show("Document with same name already exist", gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            txtDocumentName.Focus();
                            return;
                        }
                        if (oManager.SendtoNewDocument(_SelectedDocuments, _EventParameter, txtDocumentName.Text.Trim(), out oDialogDocumentID, out  oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending page(s) to new document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument)
                {
                    if (_EventParameter.IsPageMenu == false)
                    {
                        if (oManager.SendtoExistingDocument(_SelectedDocuments, _EventParameter, out oDialogDocumentID, out oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else if (_EventParameter.IsPageMenu == true)
                    {
                        if (oManager.SendtoExistingDocument(_SelectedDocuments, _EventParameter, out oDialogDocumentID, out oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending page(s) to existing document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName)
                {
                    if (_EventParameter.IsPageMenu == false)
                    {
                        if (oManager.SendtoExistingDocument(_SelectedDocuments, _EventParameter, out oDialogDocumentID, out oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else if (_EventParameter.IsPageMenu == true)
                    {
                        if (oManager.SendtoExistingDocument(_SelectedDocuments, _EventParameter, out oDialogDocumentID, out oDialogContainerID, _OpenExternalSource) == false)
                        {
                            MessageBox.Show("Error while sending page(s) to existing document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                }
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Send To Category - END" + " " + DateTime.Now.TimeOfDay);
                oDialogResultIsOK = true;
                this.Close();

            }
            catch (Exception ex)
            {
                #region " make log entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _messagestring = "date time : " + DateTime.Now.ToString() + Environment.NewLine + "error : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_messagestring);
                    _messagestring = "";
                }

                #endregion " make log entry "

            }
        }

        private void txtDocumentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFileName = txtDocumentName.Text.Trim();
                string sValidFileName = "";
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");

                if (sFileName != sValidFileName)
                {
                    txtDocumentName.Text = sValidFileName;
                    txtDocumentName.Select(txtDocumentName.Text.Length, 1);
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


                string _ex = ex.Message;
            }
        }


        
        

      

    }
}
