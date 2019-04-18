using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloTaskMail
{
    public partial class frmViewMail : Form
    {

        #region "Declarations"

            private string _databaseconnectionstring = "";
            //private string _messageBoxCaption = "gloPM";
            //Added By Pramod For Message Box
            private string _messageBoxCaption = String.Empty;


            public Int64 SelectedView=0;
            private Int64 _Userid=0;
            private Int64 _Mailid = 0;
            private Int64 _Clinicid = 0;
            private string _UserName = "";

        #endregion "Declarations"

        #region  " c1ViewMials Columns "

            const int COL_SELECT = 0;
            const int COL_READUNREAD = 1;
            const int COL_ATTACHMENT = 2;
            const int COL_MAILID = 3;
            const int COL_MAILLINKID = 4;
            const int COL_FROM = 5;
            const int COL_SUBJECT = 6;
            const int COL_DATETIME = 7;

            const int COL_COUNT = 8;


        #endregion  " c1ViewMials Columns "

        #region " Properties "

            public string DataBaseConnectionString
            {
                get { return _databaseconnectionstring; }
                set { _databaseconnectionstring = value; }
            }

            public Int64 UserID
            {
                get { return _Userid; }
                set { _Userid = value; }
            }

            public Int64 MailID
            {
                get { return _Mailid; }
                set { _Mailid = value; }
            }
        
            public Int64 ClinicID
            {
                get { return _Clinicid; }
                set { _Clinicid = value; }
            }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        #endregion " Properties "

        #region " Constructor "

        //public frmViewMail()
        //{
        //    InitializeComponent();
        //}

        public frmViewMail(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _Userid = Convert.ToInt64(appSettings["UserID"]);
            gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
            _UserName = oTask.GetUserName(UserID);

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

            InitializeComponent();
        }

        #endregion " Constructor "

        #region  " Form Events "

        private void frmViewMail_Load(object sender, EventArgs e)
        {
            
            lblDateTime.Text = DateTime.Now.ToLongDateString(); 

            c1ViewMails.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
            c1ViewMails.Rows.Count = 1;

            fillMailFolders();


            //designc1ViewMails();
            ////fillc1ViewMails();
            //fillInboxMails();

        }

        private void frmViewMail_Shown(object sender, EventArgs e)
        {
            designc1ViewMails();

        }

        #endregion  " Form Events "

        #region " C1 Mail Folders "

        private void fillMailFolders()
        {
            try
            {
               // c1MailFolders.Clear();
                c1MailFolders.DataSource = null;
                c1MailFolders.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                c1MailFolders.Cols.Count = 1;
                c1MailFolders.Rows.Count = 1;

                c1MailFolders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;

                //Header Row
                c1MailFolders.SetData(0, 0, " Mail ");
                c1MailFolders.Rows[0].AllowEditing = false;
                C1.Win.C1FlexGrid.CellStyle csTop;// = c1MailFolders.Styles.Add("cs_Top");
                try
                {
                    if (c1MailFolders.Styles.Contains("cs_Top"))
                    {
                        csTop = c1MailFolders.Styles["cs_Top"];
                    }
                    else
                    {
                        csTop = c1MailFolders.Styles.Add("cs_Top");
                        csTop.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csTop = c1MailFolders.Styles.Add("cs_Top");
                    csTop.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }
 
                c1MailFolders.Rows[0].Style = csTop;


                int nWidth = c1MailFolders.Width;
                c1MailFolders.Cols[0].Width = nWidth;

                CellStyle cs = c1MailFolders.Styles.Normal;
                cs.Border.Direction = BorderDirEnum.Both;
                cs.Border.Width = 0;

                cs.WordWrap = false;
              //  cs = c1MailFolders.Styles.Add("Parent");
                try
                {
                    if (c1MailFolders.Styles.Contains("Parent"))
                    {
                        cs = c1MailFolders.Styles["Parent"];
                    }
                    else
                    {
                        cs = c1MailFolders.Styles.Add("Parent");
                        cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(c1MailFolders.Font, FontStyle.Bold);
                        //cs.BackColor = Color.Lavender;
                        cs.BackColor = Color.Lavender;
                        cs.TextEffect = TextEffectEnum.Raised;
                    }

                }
                catch
                {
                    cs = c1MailFolders.Styles.Add("Parent");
                    cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(c1MailFolders.Font, FontStyle.Bold);
                    //cs.BackColor = Color.Lavender;
                    cs.BackColor = Color.Lavender;
                    cs.TextEffect = TextEffectEnum.Raised;

                }
               


              //  cs = c1MailFolders.Styles.Add("Child");

                try
                {
                    if (c1MailFolders.Styles.Contains("Child"))
                    {
                        cs = c1MailFolders.Styles["Child"];
                    }
                    else
                    {
                        cs = c1MailFolders.Styles.Add("Child");
                        cs.BackColor = Color.White;

                       
                    }

                }
                catch
                {
                    cs = c1MailFolders.Styles.Add("Child");
                    cs.BackColor = Color.White;

                   

                }
                //cs.BackColor = Color.AliceBlue;

                c1MailFolders.Tree.Column = 0;
                c1MailFolders.Tree.Style = TreeStyleFlags.Symbols; // Simple;
                c1MailFolders.AllowMerging = AllowMergingEnum.None; //Nodes;

                c1MailFolders.Rows.Add();
                int rowIndex = c1MailFolders.Rows.Count - 1;

                Node oParent;
                c1MailFolders.Rows[rowIndex].IsNode = true;
                c1MailFolders.Rows[rowIndex].Node.Data = " MailBox - " + UserName;
                c1MailFolders.Rows[rowIndex].Node.Key = 0;
                c1MailFolders.Rows[rowIndex].Node.Image = pic_UpcomingAppt_Provider.Image;
                c1MailFolders.Rows[rowIndex].Height = 21;

                oParent = c1MailFolders.Rows[rowIndex].Node;


                Node oFolderNode;
                int childRowIndex = 0;
                //Inbox
                //oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 1, pic_UpcomingAppt_Description.Image);
                oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 1, imageList1.Images["Inbox.ico"]);

                childRowIndex = oFolderNode.Row.Index;
                gloTaskMail.gloMail oMail = new gloMail(_databaseconnectionstring);
                c1MailFolders.SetData(childRowIndex,0, "Inbox - (" + oMail.getInboxCount(UserID) + ")");
                c1MailFolders.Rows[childRowIndex].Height = 21;
                oFolderNode = null;

                //Outbox
                //oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 2, pic_UpcomingAppt_Description.Image);
                oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 2, imageList1.Images["Sent.ico"]);
                childRowIndex = oFolderNode.Row.Index;
                c1MailFolders.SetData(childRowIndex, 0, "Outbox");
                c1MailFolders.Rows[childRowIndex].Height = 21;
                oFolderNode = null;

                //Drafts
                //oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 3, pic_UpcomingAppt_Description.Image);
                oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 3, imageList1.Images["Drafts.ico"]);
                childRowIndex = oFolderNode.Row.Index;
                c1MailFolders.SetData(childRowIndex, 0, "Drafts - (" +oMail.getDraftCount(UserID)+ ")");
                c1MailFolders.Rows[childRowIndex].Height = 21;
                oFolderNode = null;


                //Delete 
                //oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 4, pic_UpcomingAppt_Description.Image);
                oFolderNode = oParent.AddNode(NodeTypeEnum.LastChild, "", 4, imageList1.Images["Delete.ico"]);
                childRowIndex = oFolderNode.Row.Index;
                c1MailFolders.SetData(childRowIndex, 0, "Deleted");
                c1MailFolders.Rows[childRowIndex].Height = 21;
                oFolderNode = null;


                c1MailFolders.RowSel = 2;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            { 
            
            }


            
        }

        private void c1MailFolders_SelChange(object sender, EventArgs e)
        {
            Int64 key = 0;
            try
            {
                //key = Convert.ToInt64(c1MailFolders.GetData(c1MailFolders.RowSel, 0));
                if (c1MailFolders.RowSel > 0)
                {
                    key = Convert.ToInt64(c1MailFolders.Rows[c1MailFolders.RowSel].Node.Key);
                    SelectedView = key;

                    switch (key)
                    {
                        case 1: //Inbox
                            designc1ViewMails();
                            fillInboxMails();
                            break;
                        case 2: // Outbox
                            designc1SentMails();
                            fillSentMails();
                            break;
                        case 3: //Drafts
                            designc1DraftMails();
                            fillGetDrafts();
                            break;
                        case 4: //Deleted
                            break;

                        default:
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            { 
            
            }
        }

        #endregion " C1 Mail Folders "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _Mailid = 0;
            try
            {
                frmSetupMail ofrmSetupMail;

                switch (SelectedView)
                {
                    case 1: //Inbox,Outbox
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "New":
                                    {
                                        ofrmSetupMail = new frmSetupMail(_databaseconnectionstring);
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();
                                    }
                                    break;
                                case "Reply":
                                    {
                                        //Int64 _Mailid = 0;
                                        _Mailid = Convert.ToInt64(c1ViewMails.GetData(c1ViewMails.RowSel, COL_MAILID));
                                        ofrmSetupMail = new frmSetupMail(_databaseconnectionstring, _Mailid, gloTasksMails.Common.SendingType.Reply);
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();
                                    }
                                    break;
                                case "ReplyAll":
                                    {
                                        //Int64 _Mailid = 0;
                                        _Mailid = Convert.ToInt64(c1ViewMails.GetData(c1ViewMails.RowSel, COL_MAILID));
                                        ofrmSetupMail = new frmSetupMail(_databaseconnectionstring, _Mailid, gloTasksMails.Common.SendingType.ReplyAll);
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();
                                    }
                                    break;
                                case "Forward":
                                    {
                                        //Int64 _Mailid = 0;
                                        _Mailid = Convert.ToInt64(c1ViewMails.GetData(c1ViewMails.RowSel, COL_MAILID));
                                        ofrmSetupMail = new frmSetupMail(_databaseconnectionstring, _Mailid, gloTasksMails.Common.SendingType.Forward);
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();
                                    }
                                    break;
                            }
                        }
                        break;

                    case 2: // Sent
                        { 

                        }
                        break;

                    case 3: // Drafts
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                
                                case "New" :
                                    {
                                        ofrmSetupMail = new frmSetupMail(_databaseconnectionstring);
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();
                                    }
                                    break;
                                                               
                                case "Forward":
                                    {
                                        _Mailid = Convert.ToInt64(c1ViewMails.GetData(c1ViewMails.RowSel, COL_MAILID));
                                        ofrmSetupMail = new frmSetupMail(_Mailid);
                                        ofrmSetupMail.DataBaseConnectionString = _databaseconnectionstring;
                                        ofrmSetupMail.ShowDialog(this);
                                        ofrmSetupMail.Dispose();

                                    }
                                    break;

                                case "Delete":
                                    {
                                        _Mailid = Convert.ToInt64(c1ViewMails.GetData(c1ViewMails.RowSel, COL_MAILID));
                                        if(MessageBox.Show("Delete Draft,Are you Sure ?",_messageBoxCaption,MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                                        {
                                            gloMail ogloMail = new gloMail(_databaseconnectionstring);
                                            if(ogloMail.DeleteDraftMail(_Mailid))
                                            {
                                                MessageBox.Show("Draft Deleted",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    break;

                            }//switch (e.ClickedItem.Tag.ToString())
                        }
                        break;
                }//end -  switch (SelectedView)

            } //end - try
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion " Tool Strip Event "

        #region  " C1 Methods for Inbox Mails "

        private void designc1ViewMails()
        {
            try
            {
                //c1ViewMails.Clear(ClearFlags.All);

                c1ViewMails.Cols.Count = COL_COUNT;

                c1ViewMails.Dock = DockStyle.Fill;

                c1ViewMails.SetData(0, COL_SELECT, "");
                c1ViewMails.SetData(0, COL_READUNREAD, "");
                c1ViewMails.SetData(0, COL_ATTACHMENT, "");
                c1ViewMails.SetData(0, COL_MAILID, "MAILID");
                c1ViewMails.SetData(0, COL_MAILLINKID, "MAILLINKID");
                c1ViewMails.SetData(0, COL_FROM, "FROM");
                c1ViewMails.SetData(0, COL_SUBJECT, "Subject ");
                c1ViewMails.SetData(0, COL_DATETIME, "DateTime");


                int nWidth = pnlMyMail.Width;

                c1ViewMails.Cols[COL_SELECT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                c1ViewMails.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");
                c1ViewMails.Cols[COL_SELECT].AllowEditing = true;

                c1ViewMails.Cols[COL_READUNREAD].Width = 30; // Convert.ToInt32(0.05 * nWidth);

                c1ViewMails.Cols[COL_ATTACHMENT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                //c1ViewMails.Cols[COL_ATTACHMENT].DataType = System.Type.GetType("Image");

                c1ViewMails.Cols[COL_MAILID].Width = 0;
                c1ViewMails.Cols[COL_MAILLINKID].Width = 0;
                c1ViewMails.Cols[COL_FROM].Width = Convert.ToInt32(0.20 * nWidth);
                c1ViewMails.Cols[COL_SUBJECT].Width = Convert.ToInt32(0.50 * nWidth);
                c1ViewMails.Cols[COL_DATETIME].Width = Convert.ToInt32(0.13 * nWidth);

                c1ViewMails.DrawMode = DrawModeEnum.Normal;
                c1ViewMails.FocusRect = FocusRectEnum.None;
                c1ViewMails.SelectionMode = SelectionModeEnum.Row;

                c1ViewMails.AutoResize = false;
                c1ViewMails.AllowEditing = false;
                c1ViewMails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
                c1ViewMails.AllowResizing = AllowResizingEnum.Columns;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }

        private void fillInboxMails()
        {
            gloTaskMail.gloMail ogloMail = new gloMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();
            
            try
            {
                oMails = ogloMail.getInboxMails(UserID);
                fillc1ViewMails(oMails);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloMail.Dispose();
            }
        }

        private void fillc1ViewMails(Mails oMails)
        {
            

            try
            {
                //c1ViewMails.Clear(ClearFlags.All);
                c1ViewMails.Clear(ClearFlags.UserData);
                c1ViewMails.Rows.Count = 1;

                if (oMails != null && oMails.Count > 0)
                {


                    for (int i = 0; i < oMails.Count; i++)
                    {

                        if (c1ViewMails.Rows.Count - 1 <= i)
                        {
                            c1ViewMails.Rows.Add();
                        }
                        c1ViewMails.Rows[i + 1].Height = 21;
                        c1ViewMails.SetData(i + 1, COL_MAILID, oMails[i].MailID);
                        c1ViewMails.SetData(i + 1, COL_MAILLINKID, oMails[i].MailLinkID);
                        c1ViewMails.SetData(i + 1, COL_FROM, oMails[i].FromName);
                        c1ViewMails.SetData(i + 1, COL_SUBJECT, oMails[i].Subject);
                        c1ViewMails.SetData(i + 1, COL_DATETIME, DateTime.Now);

                        if (oMails[i].IsRead == false)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["UnreadMeassage_03.ico"]);
                        }
                        if (oMails[i].IsRead == true)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["ReadMeassage_03.ico"]);    
                        }

                        if (oMails[i].AttachmentName1 != "")
                        {
                            //c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, global::gloTaskMail.Properties.Resources.Attachment); 
                            c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, imageList1.Images["Attachment_01.ico"]);
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }

        #endregion  " C1 Methods for Inbox Mails "

        #region " C1 Methods for Sent Mails "

        private void designc1SentMails()
        {
            try
            {
                c1ViewMails.Clear(ClearFlags.All);
                c1ViewMails.Cols.Count = COL_COUNT;

                c1ViewMails.Dock = DockStyle.Fill;

                c1ViewMails.SetData(0, COL_SELECT, "");
                c1ViewMails.SetData(0, COL_READUNREAD, "");
                c1ViewMails.SetData(0, COL_ATTACHMENT, "");
                c1ViewMails.SetData(0, COL_MAILID, "MAILID");
                c1ViewMails.SetData(0, COL_MAILLINKID, "MAILLINKID");
                c1ViewMails.SetData(0, COL_FROM, "TO");
                c1ViewMails.SetData(0, COL_SUBJECT, "Subject ");
                c1ViewMails.SetData(0, COL_DATETIME, "DateTime");


                int nWidth = pnlMyMail.Width;

                c1ViewMails.Cols[COL_SELECT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                c1ViewMails.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");
                c1ViewMails.Cols[COL_SELECT].AllowEditing = true;

                c1ViewMails.Cols[COL_READUNREAD].Width = 30; // Convert.ToInt32(0.05 * nWidth);

                c1ViewMails.Cols[COL_ATTACHMENT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                //c1ViewMails.Cols[COL_ATTACHMENT].DataType = System.Type.GetType("Image");

                c1ViewMails.Cols[COL_MAILID].Width = 0;
                c1ViewMails.Cols[COL_MAILLINKID].Width = 0;
                c1ViewMails.Cols[COL_FROM].Width = Convert.ToInt32(0.20 * nWidth);
                c1ViewMails.Cols[COL_SUBJECT].Width = Convert.ToInt32(0.50 * nWidth);
                c1ViewMails.Cols[COL_DATETIME].Width = Convert.ToInt32(0.13 * nWidth);

                c1ViewMails.DrawMode = DrawModeEnum.Normal;
                c1ViewMails.FocusRect = FocusRectEnum.None;
                c1ViewMails.SelectionMode = SelectionModeEnum.Row;

                c1ViewMails.AutoResize = false;
                c1ViewMails.AllowEditing = false;
                c1ViewMails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
                c1ViewMails.AllowResizing = AllowResizingEnum.Columns;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }

        private void fillSentMails()
        {
            gloTaskMail.gloMail ogloMail = new gloMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();

            try
            {
                oMails = ogloMail.getSentMails(UserID);
                fillc1ViewSentMails(oMails);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloMail.Dispose();

            }
        }

        private void fillc1ViewSentMails(Mails oMails)
        {


            try
            {
                //c1ViewMails.Clear(ClearFlags.All);
                c1ViewMails.Clear(ClearFlags.UserData);
                c1ViewMails.Rows.Count = 1;

                if (oMails != null && oMails.Count > 0)
                {


                    for (int i = 0; i < oMails.Count; i++)
                    {

                        if (c1ViewMails.Rows.Count - 1 <= i)
                        {
                            c1ViewMails.Rows.Add();
                        }
                        c1ViewMails.Rows[i + 1].Height = 21;
                        c1ViewMails.SetData(i + 1, COL_MAILID, oMails[i].MailID);
                        c1ViewMails.SetData(i + 1, COL_MAILLINKID, oMails[i].MailLinkID);
                        c1ViewMails.SetData(i + 1, COL_FROM, oMails[i].To[0].Code);
                        c1ViewMails.SetData(i + 1, COL_SUBJECT, oMails[i].Subject);
                        c1ViewMails.SetData(i + 1, COL_DATETIME, DateTime.Now);

                        if (oMails[i].IsRead == false)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["UnreadMeassage_03.ico"]);
                        }
                        if (oMails[i].IsRead == true)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["ReadMeassage_03.ico"]);
                        }

                        if (oMails[i].AttachmentName1 != "")
                        {
                            //c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, global::gloTaskMail.Properties.Resources.Attachment); 
                            c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, imageList1.Images["Attachment_01.ico"]);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }

        #endregion " C1 Methods for Sent Mails "

        #region " C1 Methods for Draft Mails "

        private void designc1DraftMails()
        {
            try
            {

                c1ViewMails.Cols.Count = COL_COUNT;

                c1ViewMails.Dock = DockStyle.Fill;

                c1ViewMails.SetData(0, COL_SELECT, "");
                c1ViewMails.SetData(0, COL_READUNREAD, "");
                c1ViewMails.SetData(0, COL_ATTACHMENT, "");
                c1ViewMails.SetData(0, COL_MAILID, "MAILID");
                c1ViewMails.SetData(0, COL_MAILLINKID, "MAILLINKID");
                c1ViewMails.SetData(0, COL_FROM, "TO");
                c1ViewMails.SetData(0, COL_SUBJECT, "Subject ");
                c1ViewMails.SetData(0, COL_DATETIME, "DateTime");


                int nWidth = pnlMyMail.Width;

                c1ViewMails.Cols[COL_SELECT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                c1ViewMails.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");
                c1ViewMails.Cols[COL_SELECT].AllowEditing = true;

                c1ViewMails.Cols[COL_READUNREAD].Width = 30; // Convert.ToInt32(0.05 * nWidth);

                c1ViewMails.Cols[COL_ATTACHMENT].Width = 30; // Convert.ToInt32(0.05 * nWidth);
                //c1ViewMails.Cols[COL_ATTACHMENT].DataType = System.Type.GetType("Image");

                c1ViewMails.Cols[COL_MAILID].Width = 0;
                c1ViewMails.Cols[COL_MAILLINKID].Width = 0;
                c1ViewMails.Cols[COL_FROM].Width = Convert.ToInt32(0.20 * nWidth);
                c1ViewMails.Cols[COL_SUBJECT].Width = Convert.ToInt32(0.50 * nWidth);
                c1ViewMails.Cols[COL_DATETIME].Width = Convert.ToInt32(0.13 * nWidth);

                c1ViewMails.DrawMode = DrawModeEnum.Normal;
                c1ViewMails.FocusRect = FocusRectEnum.None;
                c1ViewMails.SelectionMode = SelectionModeEnum.Row;

                c1ViewMails.AutoResize = false;
                c1ViewMails.AllowEditing = false;
                c1ViewMails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
                c1ViewMails.AllowResizing = AllowResizingEnum.Columns;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }

        private void fillGetDrafts()
        {
            gloTaskMail.gloMail ogloMail = new gloMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();

            try
            {
                oMails = ogloMail.getDrafts(UserID);
                fillC1Drafts(oMails);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloMail.Dispose();
            }
        }

        private void fillC1Drafts(Mails oMails)
        {
            try
            {
                //c1ViewMails.Clear(ClearFlags.All);
                c1ViewMails.Clear(ClearFlags.UserData);
                c1ViewMails.Rows.Count = 1;
                
                if (oMails != null && oMails.Count > 0)
                {
                    
                    for (int i = 0; i < oMails.Count; i++)
                    {

                        if (c1ViewMails.Rows.Count - 1 <= i)
                        {
                            c1ViewMails.Rows.Add();
                        }
                        c1ViewMails.Rows[i + 1].Height = 21;
                        c1ViewMails.SetData(i + 1, COL_MAILID, oMails[i].MailID);
                        c1ViewMails.SetData(i + 1, COL_MAILLINKID, oMails[i].MailLinkID);
                        c1ViewMails.SetData(i + 1, COL_FROM, oMails[i].To[0].Code);
                        c1ViewMails.SetData(i + 1, COL_SUBJECT, oMails[i].Subject);
                        c1ViewMails.SetData(i + 1, COL_DATETIME, DateTime.Now);

                        if (oMails[i].IsRead == false)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["UnreadMeassage_03.ico"]);
                        }
                        if (oMails[i].IsRead == true)
                        {
                            c1ViewMails.SetCellImage(i + 1, COL_READUNREAD, imageList1.Images["ReadMeassage_03.ico"]);
                        }

                        if (oMails[i].AttachmentName1 != "")
                        {
                            //c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, global::gloTaskMail.Properties.Resources.Attachment); 
                            c1ViewMails.SetCellImage(i + 1, COL_ATTACHMENT, imageList1.Images["Attachment_01.ico"]);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {

            }
        }
        

        #endregion " C1 Methods for Draft Mails "
    }
}