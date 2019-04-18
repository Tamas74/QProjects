using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.DocumentContextMenu;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_AddUserTag : Form
    {
        public frmEDocEvent_AddUserTag()
        {
            _SelectedDocuments = new DocumentContextMenu.eContextDocuments();
            InitializeComponent();
        }

        #region "Page Manuipulation Variables"
        
        public bool oDialogResultIsOK = false;
        public Int64 oClinicID = 0;
        public string _ErrorMessage = "";
        private DocumentContextMenu.eContextDocuments _SelectedDocuments = null;
        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;

        #endregion 

        #region " Property Procedures "

        public DocumentContextMenu.eContextDocuments oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        #endregion

        private void frmEDocEvent_AddUserTag_Load(object sender, EventArgs e)
        {
            lvwUserTags.Items.Clear();
            lvwUserTags.Columns.Clear();

            lvwUserTags.Columns.Add("ContainerID");
            lvwUserTags.Columns.Add("DocumentID");
            lvwUserTags.Columns.Add("DocPageNumber");
            lvwUserTags.Columns.Add("ConPageNumber");
            lvwUserTags.Columns.Add("UserTagID");
            lvwUserTags.Columns.Add("Date/Time");
            lvwUserTags.Columns.Add("User Tag");

            lvwUserTags.Columns[0].Width = 0;
            lvwUserTags.Columns[1].Width = 0;
            lvwUserTags.Columns[2].Width = 0;
            lvwUserTags.Columns[3].Width = 0;
            lvwUserTags.Columns[4].Width = 0;
            lvwUserTags.Columns[5].Width = 200;
            lvwUserTags.Columns[6].Width = 350;

            lvwUserTags.Visible = false;
            txtUserTags.Visible = true;
            tlb_Delete.Visible = false;
            tlb_UserTag.Visible = false;
            //tlb_History.Visible = false;
            
            txtUserTags.Text = "";
            
            pbDocument.Minimum = 0;
            pbDocument.Maximum = 100;
            pbDocument.Value = 0;
            //this.Select();
            this.BringToFront();
            txtUserTags.Select();
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

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResultIsOK = false;
            this.Close();
        }

        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            lvwUserTags.Visible = false;
            txtUserTags.Visible = true;
            tlb_Delete.Visible = false;
            tlb_UserTag.Visible = false;
            tlb_History.Visible = true;

            tlb_Ok.Enabled = true;
        }

        private void tlb_History_Click(object sender, EventArgs e)
        {
            lvwUserTags.Visible = true;
            lvwUserTags.BringToFront();
            txtUserTags.Visible = false;
            tlb_Delete.Visible = true;
            tlb_UserTag.Visible = true;
            tlb_History.Visible = false;
            tlb_Ok.Enabled = false;

            FillUserTags();
        }

        private void tlb_Delete_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            ListView.SelectedListViewItemCollection oSelectedItems = new ListView.SelectedListViewItemCollection(lvwUserTags);
            ListViewItem oListItem = null;
            bool _result = false;
         //   Int64 _PatientId = 0;

            try
            {
                oSelectedItems = lvwUserTags.SelectedItems;

                if (oSelectedItems != null)
                {
                    if (oSelectedItems.Count > 0)
                    {
                        for (int i = 0; i < oSelectedItems.Count; i++)
                        {
                            oListItem = new ListViewItem();
                            if (oListItem != null)
                            {
                                oListItem = oSelectedItems[i];
                                _result = oDocManager.DeleteUserTag(Convert.ToInt64(oListItem.Text), Convert.ToInt64(oListItem.SubItems[4].Text), gloEDocV3Admin.gClinicID, _OpenExternalSource);
                                if (_result == false)
                                {
                                    MessageBox.Show("Problem while deleting user tags", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                if (oListItem != null)
                                {
                                    oListItem = null;
                                }
                            }

                        }//end - for (int i = 0; i < oSelectedItems.Count; i++)

                    }//end - if (oSelectedItems.Count > 0)

                }//end - if (oSelectedItems != null)

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

                if (oSelectedDocuments != null)
                {
                    oSelectedDocuments.Dispose();
                    oSelectedDocuments = null;
                }

            }
            FillUserTags();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            int _PMaxValue = 0;

            try
            {
                if (oDocManager != null)
                {
                    if (txtUserTags.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter user tags", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (oSelectedDocuments.Count > 0)
                    {
                        tlb_Ok.Enabled = false;
                        tlb_Cancel.Enabled = false;
                        tlb_UserTag.Enabled = false;
                        tlb_History.Enabled = false;
                        tlb_Delete.Enabled = false;

                        Application.DoEvents();

                        int _TotPageCount = 0;
                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
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

                        _PMaxValue = _TotPageCount ;
                        pbDocument.Minimum = 0;
                        pbDocument.Maximum = _PMaxValue;
                        pbDocument.Value = 0;

                        //
                        Int64 _userID = gloEDocV3Admin.gUserID;
                        string _userName = "";
                        _userName = eDocManager.eDocValidator.GetUserName(_userID, gloEDocV3Admin.gClinicID);
                        //

                        Application.DoEvents();

                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                        {
                            for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                            {
                                for (int k = 0; k <= oSelectedDocuments[i].Containers[j].Pages.Count - 1; k++)
                                {
                                    oDialogResultIsOK = oDocManager.AddUserTag(oSelectedDocuments[i].PatientID, oSelectedDocuments[i].DocumentID, oSelectedDocuments[i].Containers[j].ContainerID, oSelectedDocuments[i].Containers[j].Pages[k].ContainerPageNumber, oSelectedDocuments[i].Containers[j].Pages[k].DocumentPageNumber, _userID, _userName, DateTime.Now, txtUserTags.Text, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                                    pbDocument.Value = pbDocument.Value + 1;
                                    pbDocument.Refresh();
                                    Application.DoEvents();
                                }

                            }
                        }
                        Application.DoEvents();

                        if (oDialogResultIsOK == true)
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("There is some problem while adding notes.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                tlb_UserTag.Enabled = true;
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

        private void FillUserTags()
        {
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            gloEDocumentV3.Common.NTAOs oNotes = null;

            try
            {
                if (lvwUserTags.Items.Count > 0) { lvwUserTags.Items.Clear(); }

                if (oSelectedDocuments != null && oSelectedDocuments.Count > 0)
                {
                    for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                    {
                        for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                        {
                            for (int k = 0; k <= oSelectedDocuments[i].Containers[j].Pages.Count - 1; k++)
                            {
                                int _DocPageNo = oSelectedDocuments[i].Containers[j].Pages[k].DocumentPageNumber;
                                int _ConPageNo = oSelectedDocuments[i].Containers[j].Pages[k].ContainerPageNumber;
                                oNotes = oList.GetUserTags(oSelectedDocuments[i].DocumentID, oSelectedDocuments[i].Containers[j].ContainerID, _DocPageNo, _ConPageNo, gloEDocV3Admin.gClinicID, _OpenExternalSource);

                                if (oNotes != null)
                                {
                                    if (oNotes.Count > 0)
                                    {
                                        for (int n = 0; n < oNotes.Count; n++)
                                        {
                                            ListViewItem oItem = new ListViewItem();
                                            if (oItem != null)
                                            {
                                                oItem.Text = oNotes[n].DocumentID.ToString();
                                                oItem.SubItems.Add(oNotes[n].ContainerID.ToString());
                                                oItem.SubItems.Add(oNotes[n].DocumentPageNumber.ToString());
                                                oItem.SubItems.Add(oNotes[n].ContainerPageNumber.ToString());
                                                oItem.SubItems.Add(oNotes[n].NTAOID.ToString());
                                                oItem.SubItems.Add(oNotes[n].NTAODateTime.ToShortDateString());
                                                oItem.SubItems.Add(oNotes[n].NTAODescription);
                                                lvwUserTags.Items.Add(oItem);
                                                if (oItem != null)
                                                {
                                                    oItem = null;
                                                }
                                            }

                                        } //end - for (int k = 0; k < oNotes.Count; k++)

                                    } //end - if (oNotes.Count > 0)

                                } //end - if (oNotes != null)
                                if (oNotes != null)
                                {
                                    oNotes.Dispose();
                                    oNotes = null;
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
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oList != null) 
                {
                    oList.Dispose();
                    oList = null;
                }
                if (oNotes != null)
                {
                    oNotes.Dispose();
                    oNotes = null;
                }
            }
        }

    }
}
