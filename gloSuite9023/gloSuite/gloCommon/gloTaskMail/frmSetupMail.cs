using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloTaskMail
{
    public partial class frmSetupMail : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private Int64 _nUserId = 0;
        private Int64 _nClinicId = 0;
        private Int64 _nMailId = 0;
        private Int64 _DraftMailId = 0;
        //private string _messageBoxCaption = "gloPM";

        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        gloEditor.gloEditorControl EditorControl;
        gloListControl.gloListControl oListUsers;

        gloGeneralItem.gloItems ToList;
        gloGeneralItem.gloItems CcList;
        gloGeneralItem.gloItems BCcList;

        LinkLabel lnkLblRemove= new LinkLabel();
        LinkLabel lnkLblDownload = new LinkLabel();
        Label lblAttachment = new Label();

        private Byte[] oArrAttachFile;

        private gloTasksMails.Common.SendingType _sendingType = 0;

        private gloTasksMails.Common.ItemType _itemType = 0;

        private int _flowBreak = 0;

        #endregion " Declarations "

        #region " Property Procedures "

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 UserID
        {
            get { return _nUserId; }
            set { _nUserId = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicId; }
            set { _nClinicId = value; }
        }

        public Int64 MailID
        {
            get { return _nMailId; }
            set { _nMailId = value; }
        }

        public Int64 DraftMailId
        {
            get { return _DraftMailId; }
            set { _DraftMailId = value; }
        }

        public gloTasksMails.Common.SendingType SendingType
        {
            get { return _sendingType; }
            set { _sendingType = value; }
        }

        public gloTasksMails.Common.ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor & Destructor "

        public frmSetupMail(string DatabaseConnectionString)
        {

            _databaseconnectionstring = DatabaseConnectionString;
             System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _nUserId = Convert.ToInt64(appSettings["UserID"]);
            _nClinicId = Convert.ToInt64(appSettings["ClinicID"]);
             InitializeComponent();

             //Added By Pramod Nair For Messagebox Caption 
             #region " Retrieve MessageBoxCaption from AppSettings "

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

        public frmSetupMail(string DatabaseConnectionString,Int64 Mailid)
        {

            _databaseconnectionstring = DatabaseConnectionString;
            _nMailId = Mailid;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _nUserId = Convert.ToInt64(appSettings["UserID"]);
            _nClinicId = Convert.ToInt64(appSettings["ClinicID"]);
            InitializeComponent();
        }

        public frmSetupMail(string DatabaseConnectionString, Int64 MailId,gloTasksMails.Common.SendingType SendingType)
        {

            _databaseconnectionstring = DatabaseConnectionString;
            _nMailId = MailId;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _nUserId = Convert.ToInt64(appSettings["UserID"]);
            _nClinicId = Convert.ToInt64(appSettings["ClinicID"]);

            _sendingType = SendingType;

            InitializeComponent();
        }

        public frmSetupMail(Int64 DraftMailId)
        {
            _DraftMailId = DraftMailId;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _nUserId = Convert.ToInt64(appSettings["UserID"]);
            _nClinicId = Convert.ToInt64(appSettings["ClinicID"]);
            InitializeComponent();
        }
        
        #endregion " Constructor & Destructor "

        #region " Enumeration "

        public enum MailPriority
        { 
            High =1,
            Medium=2,
            Low=3
            
        }

        #endregion " Enumeration "

        #region " Form Load "

        private void frmSetupMail_Load(object sender, EventArgs e)
        {
            //Create new Instance of Control.
            EditorControl = new gloEditor.gloEditorControl();
            this.pnlBody.Controls.Add(EditorControl);
            EditorControl.Dock = DockStyle.Fill;
            EditorControl.BringToFront();

            if (MailID > 0)
            {
                SetMail(MailID, SendingType);
            }

            if (DraftMailId > 0)
            {
                SetDraftMail(DraftMailId);
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Controls Click Event "

        private void ts_Mail_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "Send" :
                    if (Validations())
                    {
                        SendMail();
                    }
                    break;
                case "Save" :
                    if (Validations())
                    {
                        Int64 _returnDraftId = 0;
                        _returnDraftId = Convert.ToInt64(txtTo.Tag);
                        _returnDraftId =  SaveDraft(_returnDraftId);
                        //Adding the Saved Draft ID to tag property of the To TextBox for further modifications.
                        txtTo.Tag = _returnDraftId;
                    }
                    break;
                case "Attachment" :
                    GetAttachment();
                    break;
                case "HighPriority" :
                    ts_btnLowPriority.CheckState = CheckState.Unchecked;
                    break;
                case "LowPriority" :
                    ts_btnHighPriority.CheckState = CheckState.Unchecked;
                    break;
                case "Signature" :
                    InsertSignature();
                    break;
                                
                default:
                    break;
            }
        }

        #endregion " Tool Strip Controls Click Event "
        
        #region " List Control Events "
        //Users List
        private void oListUsers_ItemClosedClick(object sender, EventArgs e)
        {


        }

        private void oListToUsers_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtTo.Text = "";
                ToList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem ToItem;
                
                if (oListUsers.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {

                        ToItem = new gloGeneralItem.gloItem();

                        ToItem.ID = oListUsers.SelectedItems[i].ID;
                        ToItem.Code = oListUsers.SelectedItems[i].Code;
                        ToItem.Description = oListUsers.SelectedItems[i].Description;

                        ToList.Add(ToItem);
                        ToItem.Dispose();
                        ToItem = null;

                        txtTo.Text += "<" + oListUsers.SelectedItems[i].Code + ">;";
                    }
                }
                txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oListUsers.Dispose();
            }

        }

        private void oListCcUsers_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtCc.Text = "";
                CcList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem CcItem;
                
                if (oListUsers.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {
                        CcItem = new gloGeneralItem.gloItem();

                        CcItem.ID = oListUsers.SelectedItems[i].ID;
                        CcItem.Code = oListUsers.SelectedItems[i].Code;
                        CcItem.Description = oListUsers.SelectedItems[i].Description;

                        CcList.Add(CcItem);
                        CcItem.Dispose();
                        CcItem = null;

                        txtCc.Text += "<" + oListUsers.SelectedItems[i].Code + ">;";
                    }
                }
                txtCc.Text = txtCc.Text.Substring(0, txtCc.Text.Length - 1);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                
            }
            finally
            {
                oListUsers.Dispose();
            }

        }

        private void oListBCcUsers_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtBCc.Text = "";
                BCcList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem BCcItem;
                
                if (oListUsers.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {
                        BCcItem = new gloGeneralItem.gloItem();

                        BCcItem.ID = oListUsers.SelectedItems[i].ID;
                        BCcItem.Code = oListUsers.SelectedItems[i].Code;
                        BCcItem.Description = oListUsers.SelectedItems[i].Description;

                        BCcList.Add(BCcItem);
                        BCcItem.Dispose();
                        BCcItem = null;

                        txtBCc.Text += "<" + oListUsers.SelectedItems[i].Code + ">;";
                    }
                }
                txtBCc.Text = txtBCc.Text.Substring(0, txtBCc.Text.Length - 1);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oListUsers.Dispose();
            }

        }

        #endregion " List Control Events "

        #region " Button Click Events "
        private void removeOListUsers(bool bDispose = true)
        {
            if (oListUsers != null)
            {
                if (this.Controls.Contains(oListUsers))
                {
                    this.Controls.Remove(oListUsers);
                }
                try
                {
                    oListUsers.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListToUsers_ItemSelectedClick);
                    oListUsers.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                }
                catch
                {
                }
                if (bDispose)
                {
                    oListUsers.Dispose();
                    oListUsers = null;
                }
            }
        }
        private void btnTo_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListUsers != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListUsers.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListUsers();
                oListUsers = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Contacts";

                oListUsers.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListToUsers_ItemSelectedClick);
                oListUsers.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                oListUsers.Dock = DockStyle.Fill;

                if (ToList != null)
                {
                    for (int i = 0; i < ToList.Count; i++)
                    {
                        oListUsers.SelectedItems.Add(ToList[i]);
                    }
                }
                    this.Controls.Add(oListUsers);

                //

                //
                oListUsers.OpenControl();
                oListUsers.Dock = DockStyle.Fill;
                oListUsers.BringToFront();


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCc_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListUsers != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListUsers.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListUsers();
                oListUsers = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Contacts";

                oListUsers.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListCcUsers_ItemSelectedClick);
                oListUsers.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                oListUsers.Dock = DockStyle.Fill;

                if (CcList != null)
                {
                    for (int i = 0; i < CcList.Count; i++)
                    {
                        oListUsers.SelectedItems.Add(CcList[i]);
                    }
                }
                this.Controls.Add(oListUsers);

                //

                //
                oListUsers.OpenControl();
                oListUsers.Dock = DockStyle.Fill;
                oListUsers.BringToFront();


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnBCc_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListUsers != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListUsers.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListUsers();
                oListUsers = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Contacts";

                oListUsers.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListBCcUsers_ItemSelectedClick);
                oListUsers.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                oListUsers.Dock = DockStyle.Fill;

                if (BCcList != null)
                {
                    for (int i = 0; i < BCcList.Count; i++)
                    {
                        oListUsers.SelectedItems.Add(BCcList[i]);
                    }
                }


                this.Controls.Add(oListUsers);

                //

                //
                oListUsers.OpenControl();
                oListUsers.Dock = DockStyle.Fill;
                oListUsers.BringToFront();


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion " Button Click Events "

        #region " Private Methods "

        private void SendMail()
        {
            gloTaskMail.Mail oMail = new Mail();
            gloTaskMail.gloMail ogloMail = new gloMail(_databaseconnectionstring);
            
            try
            {
                oMail.MailID = 0;
                oMail.FromID = UserID;
                oMail.To = ToList;
                oMail.Cc = CcList;
                oMail.BCc = BCcList;
                oMail.Subject = txtSubject.Text.Trim();

                if (ts_btnHighPriority.CheckState == CheckState.Checked)
                {
                    oMail.PriorityID =Convert.ToInt64(MailPriority.High);
                }
                else if (ts_btnLowPriority.CheckState == CheckState.Checked)
                {
                    oMail.PriorityID = Convert.ToInt64(MailPriority.Low);
                }
                else
                {
                    oMail.PriorityID = Convert.ToInt64(MailPriority.Medium);
                }


                oMail.OwnerID = UserID;
                
                oMail.Body =(Object)EditorControl.getData();

                oMail.IsRead = false;
                oMail.ClinicID = this.ClinicID;


                //Get the flow break of the pnlflow_Attachment if it is equal to 2 then this mail is 
                //reply,reply all or forward ,  if it is 1 then its a new mail
                //
                for (int i = 0; i < pnlflow_Attachment.Controls.Count; i++)
                {
                    if (pnlflow_Attachment.GetFlowBreak(pnlflow_Attachment.Controls[i]))
                    {
                        //MessageBox.Show("FlowBreak - "+i);
                        _flowBreak = i;
                        break;
                    }
                }

                if (_flowBreak == 1)
                {
                    //jump 2 step to avoid the "Remove" label...and get only the Attachment Label
                    for (int i = 0; i < pnlflow_Attachment.Controls.Count; i = i + 2)
                    {
                        switch (i)
                        {
                            case 0:
                                oMail.AttachmentName1 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment1 = pnlflow_Attachment.Controls[i].Tag;

                                break;
                            case 2:
                                oMail.AttachmentName2 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment2 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 4:
                                oMail.AttachmentName3 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment3 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 6:
                                oMail.AttachmentName4 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment4 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 8:
                                oMail.AttachmentName5 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment5 = pnlflow_Attachment.Controls[i].Tag;
                                break;


                        }//switch (i)

                    }//for (int i = 0; i < pnlflow_Attachment.Controls.Count; i=i+2)
                }
                else if (_flowBreak == 2)
                {
                    //if it is reply,reply all or forward mail it contains added link label Download 
                    //so jump 3 steps to avoid "Remove" & "DownLoad" labels ...and get the attachment label

                    for (int i = 0; i < pnlflow_Attachment.Controls.Count; i = i + 3)
                    {
                        switch (i)
                        {
                            case 0:
                                oMail.AttachmentName1 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment1 = pnlflow_Attachment.Controls[i].Tag;

                                break;
                            case 3:
                                oMail.AttachmentName2 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment2 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 6:
                                oMail.AttachmentName3 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment3 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 9:
                                oMail.AttachmentName4 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment4 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 12:
                                oMail.AttachmentName5 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment5 = pnlflow_Attachment.Controls[i].Tag;
                                break;


                        }//switch (i)

                    }//for (int i = 0; i < pnlflow_Attachment.Controls.Count; i=i+2)
                
                }
                _flowBreak = 0;
                ogloMail.AddMail(oMail);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            { 
            
            }
        }

        private void InsertSignature()
        {
            gloTaskMail.clsSignature oSignature = new clsSignature(_databaseconnectionstring);
            DataTable dtDefaultSign = new DataTable();
            try
            {
                dtDefaultSign = oSignature.getDefaultSignature(UserID);
                if (dtDefaultSign != null && dtDefaultSign.Rows.Count > 0)
                {
                    EditorControl.setData((Byte[])dtDefaultSign.Rows[0]["iSignature"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            finally
            {
                oSignature.Dispose();
                dtDefaultSign.Dispose();
            }

        }
        
        private void GetAttachment()
        {
            
            try
            {
                //Allow only 5 Attachments
                if (pnlflow_Attachment.Controls.Count >= 10)
                {
                    return;
                }
                
                fileDialog.Filter = "(All Files)|*.*";


                if (fileDialog.ShowDialog(this) == DialogResult.Cancel)
                    return;

                if (fileDialog.FileName == "")
                {

                    return;
                }
                else
                {
                    pnlflow_Attachment.AutoScroll = true;
                    pnlflow_Attachment.FlowDirection = FlowDirection.LeftToRight;

                    lblAttachment = new Label();
                    lblAttachment.AutoSize = true;

                    FileInfo oFileInfo = new FileInfo(fileDialog.FileName);
                    //lblAttachment.Name = oFileInfo.Name;
                    lblAttachment.Text = "    " + oFileInfo.Name;
                    lblAttachment.Tag = fileDialog.FileName;
                    lblAttachment.Image =global::gloTaskMail.Properties.Resources.Attachment_01;
                    lblAttachment.ImageAlign = ContentAlignment.TopLeft;
                    lblAttachment.TextAlign = ContentAlignment.MiddleCenter;


                    //LinkLabel lnkLblRemove = new LinkLabel();
                    lnkLblRemove = new LinkLabel();
                    lnkLblRemove.Click += new EventHandler(lnkLblRemove_Click);
                    lnkLblRemove.Text = "Remove";
                    lnkLblRemove.Tag = lblAttachment;


                    //Convert the attachment and add it to the attachment label tag
                    FileStream oFileStream = new FileStream(fileDialog.FileName, FileMode.Open);
                    oArrAttachFile= new byte[Convert.ToInt32(oFileStream.Length)];
                    oFileStream.Read(oArrAttachFile, 0, Convert.ToInt32(oFileStream.Length));
                    lblAttachment.Tag = oArrAttachFile;

                    
                    pnlflow_Attachment.Controls.Add(lblAttachment);
                    pnlflow_Attachment.Controls.Add(lnkLblRemove);
                    pnlflow_Attachment.SetFlowBreak(lnkLblRemove, true);

                    oFileStream.Dispose();

                    

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {
                
                //oArrAttachFile.SetValue(null, 0);
            }

        }

        private void SetMail(Int64 MailId, gloTasksMails.Common.SendingType SendingType)
        {
            gloMail ogloMail = new gloMail(_databaseconnectionstring);
            Mail oMail;
            try
            {
                switch (ItemType)
                {
                    case gloTasksMails.Common.ItemType.Inbox:
                        {
                            switch (SendingType)
                            {
                                case gloTasksMails.Common.SendingType.Reply:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getInboxMail(MailId);
                                        SetReply(oMail);
                                    }
                                    break;
                                case gloTasksMails.Common.SendingType.ReplyAll:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getInboxMail(MailId);
                                        SetReplyAll(oMail);
                                    }
                                    break;
                                case gloTasksMails.Common.SendingType.Forward:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getInboxMail(MailId);
                                        SetForward(oMail);
                                    }
                                    break;
                            }
                        }
                        break;
                    case gloTasksMails.Common.ItemType.Outbox:
                        {
                            switch (SendingType)
                            {
                                case gloTasksMails.Common.SendingType.Reply:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getSentMail(MailId);
                                        SetReply(oMail);
                                    }
                                    break;
                                case gloTasksMails.Common.SendingType.ReplyAll:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getSentMail(MailId);
                                        SetReplyAll(oMail);
                                    }
                                    break;
                                case gloTasksMails.Common.SendingType.Forward:
                                    {
                                        oMail = new Mail();
                                        oMail = ogloMail.getSentMail(MailId);
                                        SetForward(oMail);
                                    }
                                    break;
                            }
                        }
                        break;

                }
                
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetReply(Mail oMail)
        {
            try
            {
                if (oMail != null)
                {
                    this.ToList = null;
                    this.CcList = null;
                    this.BCcList = null;
                    this.ToList = oMail.To;
                    for (int i = 0; i < oMail.To.Count; i++)
                    {
                        txtTo.Text += "<" + oMail.To[i].Code + ">;";
                        
                    }
                    txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1);

                    txtSubject.Text = "RE: "+oMail.Subject;

                    if (oMail.Body != null)
                    {
                        EditorControl.setData((Byte[])oMail.Body);
                    }

                    if (oMail.Attachment1 != null)
                    {
                        SetAttachments(oMail.AttachmentName1, oMail.Attachment1);
                    }
                    if (oMail.Attachment2 != null)
                    {
                        SetAttachments(oMail.AttachmentName2, oMail.Attachment2);
                    }
                    if (oMail.Attachment3 != null)
                    {
                        SetAttachments(oMail.AttachmentName3, oMail.Attachment3);
                    }
                    if (oMail.Attachment4 != null)
                    {
                        SetAttachments(oMail.AttachmentName4, oMail.Attachment4);
                    }
                    if (oMail.Attachment5 != null)
                    {
                        SetAttachments(oMail.AttachmentName5, oMail.Attachment5);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

        }

        private void SetReplyAll(Mail oMail)
        {
            try
            {
                if (oMail != null)
                {
                    this.ToList = null;
                    this.ToList = oMail.To;

                    //Set The To List for this Mail
                    for (int i = 0; i < oMail.To.Count; i++)
                    {
                        txtTo.Text += "<" + oMail.To[i].Code + ">;";

                    }
                    txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1);

                    //Set the Cc List if Exist for this Mail
                    if (oMail.Cc != null)
                    {
                        this.CcList = null;
                        this.CcList = oMail.Cc;
                        for (int i = 0; i < oMail.Cc.Count; i++)
                        {
                            txtCc.Text += "<" + oMail.Cc[i].Code + ">;";

                        }
                        txtCc.Text = txtCc.Text.Substring(0, txtCc.Text.Length - 1);
                    }

                    //Set the BCc list if Exist for this Mail
                    if (oMail.BCc != null)
                    {
                        this.BCcList = null;
                        this.BCcList = oMail.BCc;
                        for (int i = 0; i < oMail.BCc.Count; i++)
                        {
                            txtBCc.Text += "<" + oMail.BCc[i].Code + ">;";

                        }
                        txtBCc.Text = txtBCc.Text.Substring(0, txtBCc.Text.Length - 1);
                    }

                    txtSubject.Text = "RE: " + oMail.Subject;

                    if (oMail.Body != null)
                    {
                        EditorControl.setData((Byte[])oMail.Body);
                    }

                    //Set All Attachments for the Mail
                    if (oMail.Attachment1 != null)
                    {
                        SetAttachments(oMail.AttachmentName1, oMail.Attachment1);
                    }
                    if (oMail.Attachment2 != null)
                    {
                        SetAttachments(oMail.AttachmentName2, oMail.Attachment2);
                    }
                    if (oMail.Attachment3 != null)
                    {
                        SetAttachments(oMail.AttachmentName3, oMail.Attachment3);
                    }
                    if (oMail.Attachment4 != null)
                    {
                        SetAttachments(oMail.AttachmentName4, oMail.Attachment4);
                    }
                    if (oMail.Attachment5 != null)
                    {
                        SetAttachments(oMail.AttachmentName5, oMail.Attachment5);
                    }


                }//if (oMail != null)
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetForward(Mail oMail)
        {
            try
            {
                if (oMail != null)
                {
                    this.ToList = null;
                    this.CcList = null;
                    this.BCcList = null;

                    txtSubject.Text = "Fwd: " + oMail.Subject;
                    if (oMail.Body != null)
                    {
                        EditorControl.setData((Byte[])oMail.Body);
                    }
                    //Set All Attachments for the Mail
                    if (oMail.Attachment1 != null)
                    {
                        SetAttachments(oMail.AttachmentName1, oMail.Attachment1);
                    }
                    if (oMail.Attachment2 != null)
                    {
                        SetAttachments(oMail.AttachmentName2, oMail.Attachment2);
                    }
                    if (oMail.Attachment3 != null)
                    {
                        SetAttachments(oMail.AttachmentName3, oMail.Attachment3);
                    }
                    if (oMail.Attachment4 != null)
                    {
                        SetAttachments(oMail.AttachmentName4, oMail.Attachment4);
                    }
                    if (oMail.Attachment5 != null)
                    {
                        SetAttachments(oMail.AttachmentName5, oMail.Attachment5);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void SetAttachments(string AttachmentName,Object Attachments)
        {
            try
            {
                
                pnlflow_Attachment.AutoScroll = true;
                pnlflow_Attachment.FlowDirection = FlowDirection.LeftToRight;

                lblAttachment = new Label();
                lblAttachment.AutoSize = true;

                //lblAttachment.Text = "    " + AttachmentName;
                lblAttachment.Text = AttachmentName;
                //
                lblAttachment.Tag = AttachmentName;
                lblAttachment.Image = global::gloTaskMail.Properties.Resources.Attachment_01;
                lblAttachment.ImageAlign = ContentAlignment.TopLeft;
                lblAttachment.TextAlign = ContentAlignment.MiddleCenter;

                lnkLblRemove = new LinkLabel();
                lnkLblRemove.Click += new EventHandler(lnkLblRemove_Click);
                lnkLblRemove.Text = "Remove";
                lnkLblRemove.Tag = lblAttachment;

                
                lnkLblDownload = new LinkLabel();
                lnkLblDownload.AutoSize = true;
                lnkLblDownload.TextAlign = ContentAlignment.TopLeft; 
                lnkLblDownload.Click += new EventHandler(lnkLblDownload_Click);
                lnkLblDownload.Text = "Download Attachment";
                lnkLblDownload.Tag = lblAttachment;


                lnkLblDownload.ImageKey = DateTime.Now.ToLongTimeString() + DateTime.Now.Millisecond.ToString(); 
                lnkLblRemove.ImageKey = lnkLblDownload.ImageKey;

                
                //Convert the attachment and add it to the attachment label tag
                oArrAttachFile = (Byte[])Attachments;
                lblAttachment.Tag = oArrAttachFile;

                pnlflow_Attachment.Controls.Add(lblAttachment);
                pnlflow_Attachment.Controls.Add(lnkLblRemove);
                //pnlflow_Attachment.SetFlowBreak(lnkLblRemove, true);

                pnlflow_Attachment.Controls.Add(lnkLblDownload);
                pnlflow_Attachment.SetFlowBreak(lnkLblDownload, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private Int64 SaveDraft(Int64 DraftId)
        {
            gloMail ogloMail = new gloMail(_databaseconnectionstring);
            Mail oMail = new Mail();

            try
            {
               // oMail.MailID = 0;
                oMail.MailID = DraftId;
                oMail.FromID = UserID;
                oMail.To = ToList;
                oMail.Cc = CcList;
                oMail.BCc = BCcList;
                oMail.Subject = txtSubject.Text.Trim();

                if (ts_btnHighPriority.CheckState == CheckState.Checked)
                {
                    oMail.PriorityID = Convert.ToInt64(MailPriority.High);
                }
                else if (ts_btnLowPriority.CheckState == CheckState.Checked)
                {
                    oMail.PriorityID = Convert.ToInt64(MailPriority.Low);
                }
                else
                {
                    oMail.PriorityID = Convert.ToInt64(MailPriority.Medium);
                }


                oMail.OwnerID = UserID;

                oMail.Body = (Object)EditorControl.getData();

                oMail.IsRead = false;
                oMail.ClinicID = this.ClinicID;


                //Get the flow break of the pnlflow_Attachment if it is equal to 2 then this mail is 
                //reply,reply all or forward ,  if it is 1 then its a new mail
                //
                for (int i = 0; i < pnlflow_Attachment.Controls.Count; i++)
                {
                    if (pnlflow_Attachment.GetFlowBreak(pnlflow_Attachment.Controls[i]))
                    {
                        //MessageBox.Show("FlowBreak - "+i);
                        _flowBreak = i;
                        break;
                    }
                }

                if (_flowBreak == 1)
                {
                    //jump 2 step to avoid the "Remove" label...and get only the Attachment Label
                    for (int i = 0; i < pnlflow_Attachment.Controls.Count; i = i + 2)
                    {
                        switch (i)
                        {
                            case 0:
                                oMail.AttachmentName1 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment1 = pnlflow_Attachment.Controls[i].Tag;

                                break;
                            case 2:
                                oMail.AttachmentName2 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment2 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 4:
                                oMail.AttachmentName3 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment3 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 6:
                                oMail.AttachmentName4 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment4 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 8:
                                oMail.AttachmentName5 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment5 = pnlflow_Attachment.Controls[i].Tag;
                                break;


                        }//switch (i)

                    }//for (int i = 0; i < pnlflow_Attachment.Controls.Count; i=i+2)
                }
                else if (_flowBreak == 2)
                {
                    //if it is reply,reply all or forward mail it contains added link label Download 
                    //so jump 3 steps to avoid "Remove" & "DownLoad" labels ...and get the attachment label

                    for (int i = 0; i < pnlflow_Attachment.Controls.Count; i = i + 3)
                    {
                        switch (i)
                        {
                            case 0:
                                oMail.AttachmentName1 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment1 = pnlflow_Attachment.Controls[i].Tag;

                                break;
                            case 3:
                                oMail.AttachmentName2 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment2 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 6:
                                oMail.AttachmentName3 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment3 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 9:
                                oMail.AttachmentName4 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment4 = pnlflow_Attachment.Controls[i].Tag;
                                break;
                            case 12:
                                oMail.AttachmentName5 = pnlflow_Attachment.Controls[i].Text;
                                oMail.Attachment5 = pnlflow_Attachment.Controls[i].Tag;
                                break;


                        }//switch (i)

                    }//for (int i = 0; i < pnlflow_Attachment.Controls.Count; i=i+2)

                }
                _flowBreak = 0;

                return  ogloMail.SaveDraftMail(oMail);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                ogloMail.Dispose();
                oMail.Dispose();
            }
        }

        private bool Validations()
        {
            try
            {
                if (txtTo.Text == "")
                {
                    MessageBox.Show("There should be atleast one recipient for this mail.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnTo.Focus();
                    return false;

                }
                if (txtSubject.Text == "")
                {
                    if (MessageBox.Show("No subject added, continue sending.  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        return true;

                    }
                    else
                    {
                        txtSubject.Focus();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            { 
            
            }
        }

        private void SetDraftMail(Int64 DraftId)
        {
            gloMail ogloMail = new gloMail(_databaseconnectionstring);
            Mail oMail;
            try
            {
                oMail = new Mail();
                oMail = ogloMail.getDraft(DraftId);

                txtTo.Tag = oMail.MailID;

                if (oMail != null)
                {
                    if (oMail.To != null)
                    {
                        this.ToList = null;
                        this.ToList = oMail.To;

                        //Set The To List for this Mail
                        for (int i = 0; i < oMail.To.Count; i++)
                        {
                            txtTo.Text += "<" + oMail.To[i].Code + ">;";

                        }
                        txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1);
                    }

                    //Set the Cc List if Exist for this Mail
                    if (oMail.Cc != null)
                    {
                        this.CcList = null;
                        this.CcList = oMail.Cc;
                        for (int i = 0; i < oMail.Cc.Count; i++)
                        {
                            txtCc.Text += "<" + oMail.Cc[i].Code + ">;";

                        }
                        txtCc.Text = txtCc.Text.Substring(0, txtCc.Text.Length - 1);
                    }

                    //Set the BCc list if Exist for this Mail
                    if (oMail.BCc != null)
                    {
                        this.BCcList = null;
                        this.BCcList = oMail.BCc;
                        for (int i = 0; i < oMail.BCc.Count; i++)
                        {
                            txtBCc.Text += "<" + oMail.BCc[i].Code + ">;";

                        }
                        txtBCc.Text = txtBCc.Text.Substring(0, txtBCc.Text.Length - 1);
                    }

                    txtSubject.Text = oMail.Subject;

                    if (oMail.Body != DBNull.Value)
                    {
                        EditorControl.setData((Byte[])oMail.Body);
                    }

                    //Set All Attachments for the Mail
                    if (oMail.Attachment1 != null)
                    {
                        SetAttachments(oMail.AttachmentName1, oMail.Attachment1);
                    }
                    if (oMail.Attachment2 != null)
                    {
                        SetAttachments(oMail.AttachmentName2, oMail.Attachment2);
                    }
                    if (oMail.Attachment3 != null)
                    {
                        SetAttachments(oMail.AttachmentName3, oMail.Attachment3);
                    }
                    if (oMail.Attachment4 != null)
                    {
                        SetAttachments(oMail.AttachmentName4, oMail.Attachment4);
                    }
                    if (oMail.Attachment5 != null)
                    {
                        SetAttachments(oMail.AttachmentName5, oMail.Attachment5);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
            
            }
        }
               
        #endregion " Private Methods "

        #region " Remove & Download LinkLabel Events "

        void lnkLblDownload_Click(object sender, EventArgs e)
        {
            Label lblTempAttachment = new Label();
            try
            {
                //get the link label clicked,that link label tag has 
                //the attachment object related to it,retrive it from
                //the linklabel.

               lblTempAttachment = ((Label)((LinkLabel)sender).Tag);
               
                //Open the Save File Dialog Control to get the location of file to save.
                saveFileDialog1.FileName = lblTempAttachment.Text;
                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    FileInfo oFileInfo = new FileInfo(fileDialog.FileName);

                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        MessageBox.Show("File already exists.  ");
                        return;
                    }
                    else
                    {
                       // MemoryStream ms = new MemoryStream((Byte[])lblTempAttachment.Tag);
                        Byte[] msBytes = (Byte[])lblTempAttachment.Tag;
                        if (msBytes != null)
                        {
                            FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                            fs.Write(msBytes, 0, msBytes.Length);
                            fs.Close();
                            fs.Dispose();
                            MessageBox.Show("File downloaded successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                         //   ms.Dispose();
                        
                      //  saveFileDialog1.Dispose();

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        void lnkLblRemove_Click(object sender, EventArgs e)
        {
            Label lblTempAttachment = null;
            try
            {

                //get the link label clicked,that link label tag has 
                //the attachment object related to it,retrive it from
                //the linklabel.
                lblTempAttachment = (Label)((LinkLabel)sender).Tag;

                //remove the attachment label from flow panel
                for (int i = 0; i < pnlflow_Attachment.Controls.Count; i++)
                {
                    string strkey1 = ((Label)(pnlflow_Attachment.Controls[i])).ImageKey.ToString();
                    string strkey2 = ((LinkLabel)sender).ImageKey.ToString();
                    if (strkey1 == strkey2)
                    {
                        pnlflow_Attachment.Controls.RemoveAt(i + 1);
                        break;
                    }

                }

                pnlflow_Attachment.Controls.Remove(lblTempAttachment);
                pnlflow_Attachment.Controls.Remove(((LinkLabel)sender));
                pnlflow_Attachment.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (lblTempAttachment != null)
                {
                    lblTempAttachment.Dispose();

                }
            }

        }

        #endregion " Remove & Download LinkLabel Events "

        #region " Text Box Text Change Events "

        //Check if the Text from the text box is deleted then also 
        //clear the respective list.

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            if (txtTo.Text == "")
            {
                this.ToList = null;
            }
        }

        private void btnCc_TextChanged(object sender, EventArgs e)
        {
            if (txtCc.Text == "")
            {
                this.BCcList = null;
            }
        }

        private void btnBCc_TextChanged(object sender, EventArgs e)
        {
            if (txtBCc.Text == "")
            {
                this.BCcList = null;
            }
        }

        #endregion " Text Box Text Change Events "

        private void frmSetupMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        


    }
}