using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.DocumentContextMenu;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_Acknowledge : Form
    {
        public frmEDocEvent_Acknowledge()
        {
            _SelectedDocuments = new eContextDocuments();
            InitializeComponent();
        }

        #region "Page Manuipulation Variables"

        public bool oDialogResultIsOK = false;
        public string _ErrorMessage = "";
        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;
        public bool _IsAcknowledge = false;
        private DocumentContextMenu.eContextDocuments _SelectedDocuments = null;

        #endregion

        #region " Property Procedures "

        public DocumentContextMenu.eContextDocuments oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        
        #endregion

        private void frmEDocEvent_Acknowledge_Load(object sender, EventArgs e)
        {
            lvwAcknowledge.Items.Clear();
            lvwAcknowledge.Columns.Clear();

            lvwAcknowledge.Columns.Add("DocumentID");
            lvwAcknowledge.Columns.Add("ContainerID");
            lvwAcknowledge.Columns.Add("UserID");
            lvwAcknowledge.Columns.Add("User Name");
            lvwAcknowledge.Columns.Add("AcknowledgementID");
            lvwAcknowledge.Columns.Add("Date/Time");
            lvwAcknowledge.Columns.Add("AcknowledgeDescription");

            lvwAcknowledge.Columns[0].Width = 0;
            lvwAcknowledge.Columns[1].Width = 0;
            lvwAcknowledge.Columns[2].Width = 0;
            lvwAcknowledge.Columns[3].Width = 0;
            lvwAcknowledge.Columns[4].Width = 0;
            lvwAcknowledge.Columns[5].Width = 200;
            lvwAcknowledge.Columns[6].Width = 350;

            lvwAcknowledge.Visible = false;

            lblUser.Visible = true;
            cmbUser.Visible = true;
            lblComment.Visible = true;
            txtComment.Visible = true;
            tlb_Delete.Visible = false;
            tlb_Review.Visible = false;

            if (oSelectedDocuments.Count > 1)
            {
                tlb_AssignTask.Enabled = false;
            }
            else
            {
                tlb_AssignTask.Enabled = true;
            }


            //tlb_History.Visible = false;

            txtComment.Text = "";
            cmbUser.SelectedIndex = -1;

            pbDocument.Minimum = 0;
            pbDocument.Maximum = 100;
            pbDocument.Value = 0;

            cmbUser.Enabled = false;

            FillUserCombo();


            string _UsrName = eDocManager.eDocValidator.GetUserName(gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID);
            for (int i = 0; i <= cmbUser.Items.Count - 1; i++)
            {
                if (cmbUser.Items[i].ToString().ToUpper() == _UsrName.ToUpper())
                {
                    cmbUser.SelectedIndex = i;
                    break;
                }
            }

            this.Select();
            this.BringToFront();

            txtComment.Select();

            if (oSelectedDocuments.Count > 1)
            {
                this.Text = "Acknowledge\\ Review";
            }
            else
            {
                if (eDocManager.eDocValidator.IsAcknowledged(_SelectedDocuments[0].DocumentID, gloEDocV3Admin.gClinicID, _OpenExternalSource) == false)
                { this.Text = "Acknowledge"; }
                else
                {
                    if (_IsAcknowledge == false )
                    {
                        this.Text = "Acknowledge";
                    }
                    else
                    { this.Text = "Review"; }
                    
                }
            }

            if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
            {
                tlb_AssignTask.Enabled = false;
                tlb_AssignTask.Visible = false;

                lblUser.Visible = false;
                cmbUser.Visible = false;
            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResultIsOK = false;
            this.Close();
        }

        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            lvwAcknowledge.Visible = false;
            lblUser.Visible = true;
            cmbUser.Visible = true;
            lblComment.Visible = true;
            txtComment.Visible = true;
            tlb_Delete.Visible = false;
            tlb_Review.Visible = false;
            tlb_History.Visible = true;

            tlb_Ok.Enabled = true;
        }

        private void tlb_History_Click(object sender, EventArgs e)
        {
            lvwAcknowledge.Visible = true;
            lvwAcknowledge.BringToFront();
            lblUser.Visible = false;
            cmbUser.Visible = false;
            lblComment.Visible = false;
            txtComment.Visible = false;
            tlb_Delete.Visible = true;
            tlb_Review.Visible = true;
            tlb_History.Visible = false;

            tlb_Ok.Enabled = false;
            FillAcknowledgements();

        }

        private void ErrorMessagees(string _ErrorMessage)
        {
            #region " Make Log Entry "
            try
            {
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
            }
            catch (Exception ex)
            {
                string _ErrorHere = ex.ToString();
            }

            #endregion " Make Log Entry "

        }

        private void tlb_Delete_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            ListView.SelectedListViewItemCollection oSelectedItems = new ListView.SelectedListViewItemCollection(lvwAcknowledge);
            bool _result = false;
            ListViewItem oListItem = null;

            try
            {
                if (oDocManager != null)
                {
                    oSelectedItems = lvwAcknowledge.SelectedItems;
                    if (oSelectedItems != null && oSelectedItems.Count > 0)
                    {
                        for (int i = 0; i < oSelectedItems.Count; i++)
                        {
                            oListItem = new ListViewItem();
                            if (oListItem != null)
                            {
                                oListItem = oSelectedItems[i];
                                _result = oDocManager.DeleteAcknowledge(Convert.ToInt64(oListItem.Text), Convert.ToInt64(oListItem.SubItems[4].Text), gloEDocV3Admin.gClinicID, _OpenExternalSource);
                                if (_result == false)
                                {
                                    MessageBox.Show("Cannot delete Acknowledge", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                if (oListItem != null)
                                {

                                    oListItem = null;
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDocManager != null)
                {
                    oDocManager.Dispose();
                    oDocManager = null;
                }
                if (oSelectedDocuments != null)
                {
                    oSelectedDocuments.Dispose();
                    oSelectedDocuments = null;
                }
            }
            FillAcknowledgements();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            int _PMaxValue = 0;

            try
            {
                if (oDocManager != null)
                {
                    if (txtComment.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + this.Text.ToLower() + " comment.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (oSelectedDocuments.Count > 0)
                    {
                        tlb_Ok.Enabled = false;
                        tlb_Cancel.Enabled = false;
                        tlb_Review.Enabled = false;
                        tlb_History.Enabled = false;
                        tlb_Delete.Enabled = false;

                        Int64 _AckUserID = 0;
                        string _AckUserName = "";


                        Application.DoEvents();

                        int _TotPageCount = 0;
                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                        {

                            if (oSelectedDocuments[i].Containers.Count > 0)
                            {
                                if (oSelectedDocuments[i].Containers[0].Pages.Count <= 0)
                                {
                                    _TotPageCount = _TotPageCount + 1;
                                }
                                else
                                {
                                    _TotPageCount = _TotPageCount + oSelectedDocuments[i].Containers[0].Pages.Count;
                                }
                            }
                        }

                        _PMaxValue = _TotPageCount;
                        pbDocument.Minimum = 0;
                        pbDocument.Maximum = _PMaxValue;
                        pbDocument.Value = 0;

                        Application.DoEvents();


                        _AckUserName = cmbUser.Text.ToString().Trim(); //read from combobox
                        _AckUserID = gloEDocumentV3.eDocManager.eDocValidator.GetUserID(_AckUserName, gloEDocV3Admin.gClinicID); ; //read from combobox

                        Application.DoEvents();

                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                        {
                            for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                            {
                                oDialogResultIsOK = oDocManager.AddAcknowledge(oSelectedDocuments[i].PatientID, oSelectedDocuments[i].DocumentID, oSelectedDocuments[i].Containers[j].ContainerID, _AckUserID, _AckUserName, DateTime.Now, txtComment.Text.Trim(), gloEDocV3Admin.gClinicID, _OpenExternalSource);
                                pbDocument.Value = pbDocument.Value + 1;
                                pbDocument.Refresh();
                                Application.DoEvents();
                            }
                        }

                        Application.DoEvents();

                        if (oDialogResultIsOK == true)
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("There is some problem while adding " + this.Text.ToLower() + ".", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDocManager != null)
                {
                    oDocManager.Dispose();
                    oDocManager = null;
                }
                tlb_Ok.Enabled = true;
                tlb_Cancel.Enabled = true;
                tlb_Review.Enabled = true;
                tlb_History.Enabled = true;
                tlb_Delete.Enabled = true;
            }
        }

        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            Application.DoEvents();
            int _PVal = 0;
            _PVal = pbDocument.Value + Percentage;
            if (_PVal <= pbDocument.Maximum) { pbDocument.Value = _PVal; }
        }

        private void FillUserCombo()
        {
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            ArrayList _UserList = new ArrayList();
            try
            {
                if (oList != null)
                {
                    _UserList = oList.GetUsers(gloEDocV3Admin.gClinicID);
                    if (_UserList != null && _UserList.Count > 0)
                    {
                        for (int i = 0; i < _UserList.Count; i++)
                        {
                            cmbUser.Items.Add(_UserList[i].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (_UserList != null)
                {
                    _UserList = null;
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
            }
        }

        private void FillAcknowledgements()
        {
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            gloEDocumentV3.Common.NTAOs oAcknowledgements = null;
            try
            {
                if (oList != null)
                {
                    if (lvwAcknowledge.Items.Count > 0) { lvwAcknowledge.Items.Clear(); }

                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0)
                    {
                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                        {
                            for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                            {
                                oAcknowledgements = new gloEDocumentV3.Common.NTAOs();
                                oAcknowledgements = oList.GetAcknowledges(oSelectedDocuments[i].Containers[j].ContainerID, oSelectedDocuments[i].DocumentID, gloEDocV3Admin.gClinicID, _OpenExternalSource);

                                if (oAcknowledgements != null && oAcknowledgements.Count > 0)
                                {
                                    for (int k = 0; k < oAcknowledgements.Count; k++)
                                    {

                                        ListViewItem oItem = new ListViewItem();

                                        oItem.Text = oAcknowledgements[k].DocumentID.ToString();
                                        oItem.SubItems.Add(oAcknowledgements[k].ContainerID.ToString());
                                        oItem.SubItems.Add(oAcknowledgements[k].UserID.ToString());
                                        oItem.SubItems.Add(oAcknowledgements[k].UserName.ToString());
                                        oItem.SubItems.Add(oAcknowledgements[k].NTAOID.ToString());
                                        oItem.SubItems.Add(oAcknowledgements[k].NTAODateTime.ToShortDateString());
                                        oItem.SubItems.Add(oAcknowledgements[k].NTAODescription);
                                        lvwAcknowledge.Items.Add(oItem);

                                        if (oItem != null) { oItem = null; }

                                    } //end - for (int k = 0; k < oAcknowledgements.Count; k++)
                                }
                                if (oAcknowledgements != null) { oAcknowledgements.Dispose(); }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
            }
        }

        private void ts_AssignTask_Click(object sender, EventArgs e)
        {
            Int64 _DocumentIDs = 0;
            try
            {
                if (oSelectedDocuments != null)
                {
                    if (oSelectedDocuments.Count <= 0)
                    {
                        MessageBox.Show("Please select a document to create the task.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (oSelectedDocuments.Count == 1)
                    {
                        _DocumentIDs = oSelectedDocuments[0].DocumentID; 

                        Int64 nProviderID = 0;
                        nProviderID = Int64.Parse(gloEDocV3Admin.GetProviderDetails());

                        Int64 DMSUser = 0;
                        if (nProviderID > 0)
                        {
                            DMSUser = GetLoginProvider_DMSUser(nProviderID);
                        }
                        String DMSDescription = "Task for the DMS Document: " + oSelectedDocuments[0].Category.ToString() + " - " + oSelectedDocuments[0].DocumentName.ToString();

                        //00000926 : Creating task for Assigned DMS Document task
                        using (gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString, 0, 0, _DocumentIDs, gloTaskMail.TaskType.AssignedDMS))
                        {
                            ofrmTask.IsEMREnable = true;
                            ofrmTask.PatientID = oSelectedDocuments[0].PatientID;
                            ofrmTask.ProviderID = nProviderID;
                            ofrmTask.DMSUser = DMSUser;
                            if (txtComment.Text == "")
                            {
                                ofrmTask.DMSDescription = DMSDescription;
                            }
                            else
                            {
                                ofrmTask.DMSDescription = txtComment.Text +"\n"+ DMSDescription;
                            }
                            ofrmTask.ShowDialog(ofrmTask.Parent == null ? this : ofrmTask.Parent);
                        }
                    }
                    else
                    {
                        MessageBox.Show("DMS Task can be sent for only one document at a time. Please select a single document", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Int64 GetLoginProvider_DMSUser(Int64 nProviderID)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                string strQuery = strQuery = "SELECT ISNULL(nOthersID,0) as  nOthersID FROM ProviderSettings Where UPPER(sSettingsType)='DMSUSER' AND nProviderID=" + nProviderID;

                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand(strQuery, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                object objResult = cmd.ExecuteScalar();
                con.Close();
                return System.Convert.ToInt64(objResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Dispose(); con = null;
                }

            }
        }
    }
}
