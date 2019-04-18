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
    partial class frmEDocEvent_Rename : Form
    {
        public frmEDocEvent_Rename()
        {
            InitializeComponent();
            _SelectedDocuments = new DocumentContextMenu.eContextDocuments();
            _EventParameter = new DocumentContextMenu.eContextEventParameter();

        }

        #region "Page Manuipulation Variables"

        private string invalidCharString = ":" + "*" + "?" + "<" + ">" + "\\" + "//";

        public bool oDialogResultIsOK = false;
        public string oDialogDocumentName = "";
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

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            try
            {


                if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument)
                {
                    if (txtSourceDocument.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the document name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }
                    if (txtSourceDocument.Text.ToUpper() != _SelectedDocuments[0].DocumentName.ToUpper())
                    {
                        if (eDocManager.eDocValidator.IsDocumentNameExists(txtSourceDocument.Text, _SelectedDocuments[0].PatientID, _SelectedDocuments[0].Category.ToString(),"", gloEDocV3Admin.gClinicID, _OpenExternalSource) == true)
                        {
                            MessageBox.Show("Document with the same name already exist. Enter another name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSourceDocument.Select(0, txtSourceDocument.Text.Length);
                            return;
                        }
                    }


                    char[] chars = invalidCharString.ToCharArray();
                    for (int i = 0; i < chars.Length - 1; i++)
                    {
                        string strCharacter = chars[i].ToString();

                        bool _result = txtSourceDocument.Text.Contains(strCharacter);
                        if (_result == true)
                        {
                            MessageBox.Show("Document name is not in valid format.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }


                }
                else if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages)
                {
                    if (txtSourceDocument.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the page name. Enter another name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }
                    if (eDocManager.eDocValidator.IsPageNameExists(txtSourceDocument.Text, _SelectedDocuments[0].DocumentID, gloEDocV3Admin.gClinicID, Convert.ToInt32(_SelectedDocuments[0].Containers[0].Pages[0].DocumentPageNumber.ToString()), _OpenExternalSource) == true)
                    {
                        MessageBox.Show("Page with the same name already exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSourceDocument.Select(0, txtSourceDocument.Text.Length);
                        return;
                    }
                }
               
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                int _RecAff = 0;
                string _sqlQuery = "";
                if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument)
                {
                    if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                    {
                        _sqlQuery = "UPDATE eDocument_Details_V3_RCM SET DocumentName  = '" + txtSourceDocument.Text + "' WHERE PatientID = " + _SelectedDocuments[0].PatientID + " AND eDocumentID = " + _SelectedDocuments[0].DocumentID + " AND ClinicID = " + gloEDocV3Admin.gClinicID + " ";
                    }
                    else
                    {
                        _sqlQuery = "UPDATE eDocument_Details_V3 SET DocumentName  = '" + txtSourceDocument.Text + "' WHERE PatientID = " + _SelectedDocuments[0].PatientID + " AND eDocumentID = " + _SelectedDocuments[0].DocumentID + " AND ClinicID = " + gloEDocV3Admin.gClinicID + " ";
                    }
                    
                }
                else if (_EventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages)
                {
                    if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                    {
                        _sqlQuery = "UPDATE eDocument_Pages_V3_RCM SET PageName  = '" + txtSourceDocument.Text + "' WHERE eContainerID = " + _SelectedDocuments[0].Containers[0].ContainerID + " AND eDocumentID = " + _SelectedDocuments[0].DocumentID + " AND DocumentPageNumber = " + Convert.ToInt32(_SelectedDocuments[0].Containers[0].Pages[0].DocumentPageNumber.ToString()) + " AND ClinicID = " + gloEDocV3Admin.gClinicID + " ";
                    }
                    else
                    {
                        _sqlQuery = "UPDATE eDocument_Pages_V3 SET PageName  = '" + txtSourceDocument.Text + "' WHERE eContainerID = " + _SelectedDocuments[0].Containers[0].ContainerID + " AND eDocumentID = " + _SelectedDocuments[0].DocumentID + " AND DocumentPageNumber = " + Convert.ToInt32(_SelectedDocuments[0].Containers[0].Pages[0].DocumentPageNumber.ToString()) + " AND ClinicID = " + gloEDocV3Admin.gClinicID + " ";
                    }
                }
                oDB.Connect(false);
                _RecAff = oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();
                oDB.Dispose();

                if (_RecAff > 0)
                {
                    oDialogResultIsOK = true;
                    oDialogDocumentName = txtSourceDocument.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Document name not changed.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSourceDocument_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string sFileName = txtSourceDocument.Text.Trim();
                string sValidFileName = "";
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");

                if (sFileName != sValidFileName)
                {
                    txtSourceDocument.Text = sValidFileName;
                    txtSourceDocument.Select(txtSourceDocument.Text.Length, 1);
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
              

        private void txtSourceDocument_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFileName = txtSourceDocument.Text.Trim();
                string sValidFileName = "";
                
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                if (sFileName != sValidFileName)
                {
                    txtSourceDocument.Text = sValidFileName;
                    txtSourceDocument.Select(txtSourceDocument.Text.Length, 1);
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
