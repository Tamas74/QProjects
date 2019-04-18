using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using C1.Win.C1FlexGrid;

namespace gloTaskMail
{
    public partial class frmViewUserMail : Form
    {
        #region Column Constants for C1FlexGrid
       

        private const int COL_MAIL_ITEMID = 0;
        private const int COL_MAIL_ITEMCHANGEKEY = 1;        
        private const int COL_MAIL_SELECT = 2;
        private const int COL_MAIL_RECDATETIME = 3;
        private const int COL_MAIL_FROM = 4;
        private const int COL_MAIL_SUBJECT = 5;
        private const int COL_MAIL_BODY = 6;       
        private const int COL_MAIL_PMSID = 7;
        private const int COL_MAIL_PATIENTID = 8;
        private const int COL_MAIL_PATIENTNAME = 9;
        private const int COL_MAIL_COUNT = 10;

        private const int COL_PATIENT_SELECT = 0;
        private const int COL_PATIENT_PATIENTID = 1;
        private const int COL_PATIENT_PATIENTNAME = 2;
        private const int COL_PATIENT_ITEMID = 3;
        private const int COL_PATIENT_ITEMCHANGEKEY = 4;
        private const int COL_PATIENT_RECDATE = 5;
        private const int COL_PATIENT_FROM = 6;
        private const int COL_PATIENT_SUBJECT = 7;
        private const int COL_PATIENT_BODY = 8;        
        private const int COL_PATIENT_COUNT = 9;

        private const int ROW_HEIGHT = 23; 
        #endregion

        private Int64 _userID = 0;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";
        private string _exchangeItemID = "";
        private string _exchangeItemChangeKey = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        LinkLabel lnkLblRemove = new LinkLabel();
        LinkLabel lnkLblDownload = new LinkLabel();
        Label lblAttachment = new Label();

        //gloListControl.gloListControl oListControl;
        gloPatient.gloSmartPatientControl oSmartPatient=null;

        //gloEditor.gloEditorControl EditorControl;
        // PmsExchangeMails oMails = new PmsExchangeMails();

        public string ExchangeItemID
        {
            get { return _exchangeItemID; }
            set { _exchangeItemID = value; }
        }

        public string ExchangeItemChangeKey
        {
            get { return _exchangeItemChangeKey; }
            set { _exchangeItemChangeKey = value; }
        }

        #region Contructor
        
        public frmViewUserMail()
        {
            InitializeComponent();
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();            

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userID = Convert.ToInt64(appSettings["UserID"].ToString()); }
                else { _userID = 0; }
            }
            else
            { _userID = 0; }


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }



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

        private static frmViewUserMail _frm = null;

        public static frmViewUserMail GetInstance()
        {

            if (_frm != null)
            {
                return _frm;
            }
            else
            {
                _frm = new frmViewUserMail();
                return _frm;
            }

        }

        bool blnDisposed;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (blnDisposed == false)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    components.Dispose();
                    try
                    {
                        if (saveFileDialog1 != null)
                        {

                            saveFileDialog1.Dispose();
                            saveFileDialog1 = null;
                        }
                    }
                    catch
                    {
                    }
                }
                base.Dispose(disposing);
            }
            _frm = null;
            blnDisposed = true;
        }

        private void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }


        #endregion

        #region Fill Data

        #region "gloSuite - while integrating this function is comment, if required then we have to add gloExchange project, other wise try to maintain it in gloExchange service"

        private void FillUserMails()
        {
            //gloExchange.Common.Mail.PmsExchangeMail ogloMail = new gloExchange.Common.Mail.PmsExchangeMail(_databaseconnectionstring);
            //DataTable dtMails = new DataTable();
            //try
            //{
            //    c1Mails.Rows.Count = 1;

            //    dtMails = ogloMail.GetUserMails(_userID);
            //    for (int i = 0; i < dtMails.Rows.Count; i++)
            //    {
            //        c1Mails.Rows.Add();
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_SUBJECT, Convert.ToString(dtMails.Rows[i]["sSubject"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_BODY, Convert.ToString(dtMails.Rows[i]["sBody"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_FROM, Convert.ToString(dtMails.Rows[i]["sSenderName"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_RECDATETIME, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtMails.Rows[i]["dtReceivedDate"])));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_ITEMID, Convert.ToString(dtMails.Rows[i]["sExchangeItemID"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_ITEMCHANGEKEY, Convert.ToString(dtMails.Rows[i]["sExchangeItemChangeKey"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_PATIENTNAME, Convert.ToString(dtMails.Rows[i]["PatientName"]));
            //        c1Mails.SetData(c1Mails.Rows.Count - 1, COL_MAIL_PATIENTID, Convert.ToString(dtMails.Rows[i]["nPatientID"]));

            //        if (_exchangeItemID.Trim() == Convert.ToString(dtMails.Rows[i]["sExchangeItemID"]).Trim() && _exchangeItemChangeKey.Trim() == Convert.ToString(dtMails.Rows[i]["sExchangeItemChangeKey"]).Trim())
            //            c1Mails.Row = c1Mails.Rows.Count - 1;
            //        else
            //            c1Mails.Rows[c1Mails.Rows.Count - 1].Selected = false;                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    dtMails.Dispose();
            //    ogloMail.Dispose();
            //}
        }

        private void FillPatientMails()
        {
            //gloExchange.Common.Mail.PmsExchangeMail ogloMail = new gloExchange.Common.Mail.PmsExchangeMail(_databaseconnectionstring);
            //DataTable dtPatients = new DataTable();
            //DataTable dtMails = new DataTable();
            //try
            //{
            //    C1.Win.C1FlexGrid.CellRange oRange; 
  
            //    //Get Patient List
            //    c1PatientMails.Rows.Count = 1;
            //    dtPatients = ogloMail.GetAsoociatedPatientName(_userID);
            //    for (int i = 0; i < dtPatients.Rows.Count; i++)
            //    {

            //        //Add Parent Node to Patient mail grid
            //        c1PatientMails.Rows.Add();
            //        Int32 RowCount = c1PatientMails.Rows.Count - 1;
            //        c1PatientMails.SetData(RowCount, COL_PATIENT_PATIENTID, Convert.ToString(dtPatients.Rows[i]["nPatientID"]));
            //        c1PatientMails.SetData(RowCount, COL_PATIENT_PATIENTNAME, Convert.ToString(dtPatients.Rows[i]["PatientName"]));

            //        Node oParent;
            //        c1PatientMails.Rows[RowCount].IsNode = true;
            //        c1PatientMails.Rows[RowCount].Node.Key = Convert.ToString(dtPatients.Rows[i]["nPatientID"]);
            //        oParent = c1PatientMails.Rows[RowCount].Node;

            //        oRange = c1PatientMails.GetCellRange(RowCount, 0, RowCount, c1PatientMails.Cols.Count - 1);
            //        oRange.Style = c1PatientMails.Styles["cs_Parent"];
     

            //        //Get Patinet Mails                    
            //        dtMails = ogloMail.GetPatientMails(_userID, Convert.ToInt64(dtPatients.Rows[i]["nPatientID"]));
            //        for (int k = 0; k < dtMails.Rows.Count; k++)
            //        {
            //            Node oChild;
            //            oChild = oParent.AddNode(NodeTypeEnum.LastChild, "", oParent.Key, null);
            //            Int32 ChildIndex = oChild.Row.Index;

            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_ITEMID, Convert.ToString(dtMails.Rows[k]["sExchangeItemID"]));
            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_ITEMCHANGEKEY, Convert.ToString(dtMails.Rows[k]["sExchangeItemChangeKey"]));
            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_SUBJECT, Convert.ToString(dtMails.Rows[k]["sSubject"]));
            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_BODY, Convert.ToString(dtMails.Rows[k]["sBody"]));
            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_RECDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtMails.Rows[k]["dtReceivedDate"])).ToShortDateString());
            //            c1PatientMails.SetData(ChildIndex, COL_PATIENT_FROM, Convert.ToString(dtMails.Rows[k]["sSenderName"]));

            //            oRange = c1PatientMails.GetCellRange(ChildIndex, 0, ChildIndex, c1PatientMails.Cols.Count - 1);
            //            oRange.Style = c1PatientMails.Styles["cs_Child"];
            //        }
            //        c1PatientMails.Refresh();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    ogloMail.Dispose();
            //    dtPatients.Dispose();
            //    dtMails.Dispose();
            //}
        }

        private void ReadMailsFromExchange()
        {
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;

            //    gloExchange.gloExchange ogloExchange = new gloExchange.gloExchange(_databaseconnectionstring);
            //    ogloExchange.GetExchangeSettings();
            //    PmsExchangeMails oMails = ogloExchange.FindMails(_userID);
            //    FillUserMails();

            //    Cursor.Current = Cursors.Default;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void c1Mails_Click(object sender, EventArgs e)
        {
            //string _FileName = "";
            //string _FilePath = "";

            //try
            //{

            //    if (c1Mails.Row > 0)
            //    {
            //        pnlflow_Attachment.Controls.Clear();
            //        lblSubject.Text = "";
            //        lblDate.Text = "";
            //        lblFrom.Text = "";
            //        webBrowser1.Url = null;
            //        webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //        pnlMailDetails.Visible = true;


            //        if (Directory.Exists(Application.StartupPath + "\\Temp\\") == false)
            //        {
            //            Directory.CreateDirectory(Application.StartupPath + "\\Temp");
            //        }

            //        _FileName = DateTime.Now.ToString("yyyyMMddhhmmsstt");
            //        _FilePath = Application.StartupPath + "\\Temp\\" + _FileName + ".html";

            //        if (File.Exists(_FilePath) == true)
            //        {
            //            File.Delete(_FilePath); 
            //        }

            //        FileStream fs = new FileStream(_FilePath, FileMode.Create);
            //        StreamWriter sw = new StreamWriter(fs);
            //        sw.Write(c1Mails.GetData(c1Mails.Row,COL_MAIL_BODY).ToString());
            //        sw.Close();

            //        if (File.Exists(_FilePath) == true)
            //        {                       
            //            System.Uri TempURL = new Uri(_FilePath);
            //            webBrowser1.Url = TempURL;
            //            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            //        }

            //        lblSubject.Text = Convert.ToString(c1Mails.GetData(c1Mails.Row, COL_MAIL_SUBJECT));
            //        lblDate.Text = Convert.ToDateTime(c1Mails.GetData(c1Mails.Row, COL_MAIL_RECDATETIME)).ToShortDateString();
            //        lblFrom.Text = Convert.ToString(c1Mails.GetData(c1Mails.Row, COL_MAIL_FROM));

            //        DataTable dtAttachments = new DataTable();
            //        string ExchangeItemID = Convert.ToString(c1Mails.GetData(c1Mails.Row,COL_MAIL_ITEMID));
            //        string ExchangeItemChangeKey = Convert.ToString(c1Mails.GetData(c1Mails.Row, COL_MAIL_ITEMCHANGEKEY));
            //        gloExchange.Common.Mail.PmsExchangeMail oMail = new PmsExchangeMail(_databaseconnectionstring);

            //        dtAttachments = oMail.GetAttachments(ExchangeItemID, ExchangeItemChangeKey);

            //        for (int i = 0; i < dtAttachments.Rows.Count; i++)
            //        {
            //            SetAttachments(Convert.ToString(dtAttachments.Rows[i]["sAttachmentName"]), (Object)dtAttachments.Rows[i]["iAttachmentContents"]);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void c1PatientMails_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (c1PatientMails.Row < 1)
            //        return;

            //    string _FileName = "";
            //    string _FilePath = "";

            //    if (c1PatientMails.Rows[c1PatientMails.Row].Node.Children == 0)
            //    {
            //        pnlflow_Attachment.Controls.Clear();
            //        webBrowser1.Url = null;
            //        webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //        lblSubject.Text = "";
            //        lblDate.Text = "";
            //        lblFrom.Text = "";
            //        pnlMailDetails.Visible = true;

            //        if (Directory.Exists(Application.StartupPath + "\\Temp\\") == false)
            //        {
            //            Directory.CreateDirectory(Application.StartupPath + "\\Temp");
            //        }
            //        _FileName = DateTime.Now.ToString("yyyyMMddhhmmsstt");
            //        _FilePath = Application.StartupPath + "\\Temp\\" + _FileName + ".html";

            //        if (File.Exists(_FilePath) == true)
            //        {
            //            File.Delete(_FilePath);
            //        }

            //        FileStream fs = new FileStream(_FilePath, FileMode.Create);
            //        StreamWriter sw = new StreamWriter(fs);
            //        sw.Write(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_BODY).ToString());
            //        sw.Close();

            //        if (File.Exists(_FilePath) == true)
            //        {
            //            System.Uri TempURL = new Uri(_FilePath);
            //            webBrowser1.Url = TempURL;
            //            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            //        }

            //        lblSubject.Text = Convert.ToString(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_SUBJECT));
            //        lblDate.Text = Convert.ToDateTime(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_RECDATE)).ToShortDateString();
            //        lblFrom.Text = Convert.ToString(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_FROM));

            //        DataTable dtAttachments = new DataTable();
            //        string ExchangeItemID = Convert.ToString(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_ITEMID));
            //        string ExchangeItemChangeKey = Convert.ToString(c1PatientMails.GetData(c1PatientMails.Row, COL_PATIENT_ITEMCHANGEKEY));
            //        gloExchange.Common.Mail.PmsExchangeMail oMail = new PmsExchangeMail(_databaseconnectionstring);

            //        dtAttachments = oMail.GetAttachments(ExchangeItemID, ExchangeItemChangeKey);

            //        for (int i = 0; i < dtAttachments.Rows.Count; i++)
            //        {
            //            SetAttachments(Convert.ToString(dtAttachments.Rows[i]["sAttachmentName"]), (Object)dtAttachments.Rows[i]["iAttachmentContents"]);
            //        }
            //    }
            //    else
            //    {
            //        webBrowser1.Url = null;
            //        webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //        pnlflow_Attachment.Controls.Clear();
            //        pnlMailDetails.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void oSmartPatient_ItemSelectedClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (oSmartPatient.PatientID > 0)
            //    {

            //        gloMail ogloMail = new gloMail(_databaseconnectionstring);
            //        gloExchange.Common.Mail.PmsExchangeMail oExchangeMail = new PmsExchangeMail(_databaseconnectionstring);

            //        for (int i = 1; i < c1Mails.Rows.Count; i++)
            //        {
            //            if (Convert.ToBoolean(c1Mails.GetData(i, COL_MAIL_SELECT)) == true)
            //            {
            //                c1Mails.SetData(i, COL_MAIL_PATIENTID, oSmartPatient.PatientID.ToString());
            //                c1Mails.SetData(i, COL_MAIL_PATIENTNAME, oSmartPatient.PatientFirstName);

            //                //Int32 Index = Convert.ToInt32(c1Mails.GetData(i, COL_MAIL_ID));
            //                //ogloMail.AssociatePatient(PatientID,oMails[Index].ExchangeItemID,oMails[Index].ExchangeItemChangeKey);

            //                oExchangeMail.ExchangeItemID = c1Mails.GetData(i, COL_MAIL_ITEMID).ToString();
            //                oExchangeMail.ExchangeItemChangeKey = c1Mails.GetData(i, COL_MAIL_ITEMCHANGEKEY).ToString();
            //                oExchangeMail.Subject = c1Mails.GetData(i, COL_MAIL_SUBJECT).ToString();
            //                oExchangeMail.Body = (object)c1Mails.GetData(i, COL_MAIL_BODY).ToString();
            //                oExchangeMail.MailDate = Convert.ToDateTime(c1Mails.GetData(i, COL_MAIL_RECDATETIME).ToString());
            //                oExchangeMail.FromName = c1Mails.GetData(i, COL_MAIL_FROM).ToString();

            //                oExchangeMail.AssociatePatient(oSmartPatient.PatientID, _userID, oExchangeMail);

            //            }
            //        }
            //        FillPatientMails();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //pnlViewMail.Visible = true;
        }

        #endregion "gloSuite"


        #endregion

        

        private void SendMailsToExchange()
        {
        }

        private void frmViewUserMail_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1PatientMails, false); 

            DesignMailsGrid();
            DesignPatientMailGrid();            
            c1Mails.BringToFront();            
            tsb_UserMails.Enabled = false;
            FillUserMails();
            FillPatientMails();
            c1Mails_Click(null, null);
        }
       
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "AssociatePatient":
                        {
                            //AssociatePatient();
                            SmartAssociation();
                        }
                        break;
                    case "UserMails":
                        {
                            ShowUserMails();
                        }
                        break;
                    case "PatientMails":
                        {
                            ShowPatientMails();
                        }
                        break;
                    case "Refresh":
                        {
                            FillUserMails();
                            FillPatientMails();
                        }
                        break;
                  

                    case "Receive":
                        {
                            ReadMailsFromExchange();
                        }
                        break;

                    case "Close":
                        {
                            this.Close();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        
        #region View Mail

        //View selected User mail
        

        //View selected Patient Mail
        

        //Show user mails
        private void ShowUserMails()
        {
            try
            {
                c1Mails.BringToFront();
                c1Mails.Refresh();
                c1Mails.Row = -1;

                for (int i = 1; i < c1Mails.Rows.Count; i++)
                {
                    c1Mails.SetData(i, COL_MAIL_SELECT, false);
                }

                tsb_UserMails.Enabled = false;
                tsb_PatientMails.Enabled = true;
                tsb_AssociatePatient.Enabled = true;

                webBrowser1.Url = null;
                webBrowser1.IsWebBrowserContextMenuEnabled = false;
                pnlflow_Attachment.Controls.Clear();

                lblDate.Text = "";
                lblSubject.Text = "";
                lblFrom.Text = "";
                lblMailsHeader.Text = "       User Mail";

                pnlMailDetails.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Show Patients and Associated mails 
        private void ShowPatientMails()
        {
            try
            {

                c1PatientMails.BringToFront();
                c1PatientMails.Refresh();
                c1PatientMails.Row = -1;
                tsb_PatientMails.Enabled = false;
                tsb_UserMails.Enabled = true;
                tsb_AssociatePatient.Enabled = false;

                webBrowser1.Url = null;
                webBrowser1.IsWebBrowserContextMenuEnabled = false;
                pnlflow_Attachment.Controls.Clear();
                lblDate.Text = "";
                lblSubject.Text = "";
                lblFrom.Text = "";
                lblMailsHeader.Text = "       Patient Mail";

                pnlMailDetails.Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
#endregion
        
        #region Patient Association

        #region Commented Code 20080618
        //private void AssociatePatient()
        //{
        //    try
        //    {
        //        oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, pnlViewMail.Width - 5);
        //        oListControl.ControlHeader = "Select Patient";
        //        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PatientSelectedClick);
        //        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
        //        this.pnlAssociatePatient.Controls.Add(oListControl);


        //        // oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurence.SelectedValue), Convert.ToString(cmbInsurence.SelectedText));

        //        pnlAssociatePatient.BringToFront();
        //        oListControl.OpenControl();
        //        oListControl.Dock = DockStyle.Fill;
        //        oListControl.BringToFront();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void oListControl_PatientSelectedClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Int64 PatientID = 0;
        //        string PatientName = "";

        //        if (oListControl.SelectedItems.Count > 0)
        //        {
        //            PatientName = oListControl.SelectedItems[0].Description;
        //            PatientID = oListControl.SelectedItems[0].ID;
        //            gloMail ogloMail = new gloMail(_databaseconnectionstring);
        //            gloExchange.Common.Mail.PmsExchangeMail oExchangeMail = new PmsExchangeMail(_databaseconnectionstring);

        //            for (int i = 1; i < c1Mails.Rows.Count; i++)
        //            {
        //                if (Convert.ToBoolean(c1Mails.GetData(i, COL_MAIL_SELECT)) == true)
        //                {
        //                    c1Mails.SetData(i, COL_MAIL_PATIENTID, PatientID.ToString());
        //                    c1Mails.SetData(i, COL_MAIL_PATIENTNAME, PatientName);

        //                    //Int32 Index = Convert.ToInt32(c1Mails.GetData(i, COL_MAIL_ID));
        //                    //ogloMail.AssociatePatient(PatientID,oMails[Index].ExchangeItemID,oMails[Index].ExchangeItemChangeKey);

        //                    oExchangeMail.ExchangeItemID = c1Mails.GetData(i, COL_MAIL_ITEMID).ToString();
        //                    oExchangeMail.ExchangeItemChangeKey = c1Mails.GetData(i, COL_MAIL_ITEMCHANGEKEY).ToString();
        //                    oExchangeMail.Subject = c1Mails.GetData(i, COL_MAIL_SUBJECT).ToString();
        //                    oExchangeMail.Body = (object)c1Mails.GetData(i, COL_MAIL_BODY).ToString();
        //                    oExchangeMail.MailDate = Convert.ToDateTime(c1Mails.GetData(i, COL_MAIL_RECDATETIME).ToString());
        //                    oExchangeMail.FromName = c1Mails.GetData(i, COL_MAIL_FROM).ToString();

        //                    oExchangeMail.AssociatePatient(PatientID, _userID, oExchangeMail);

        //                }
        //            }
        //            FillPatientMails();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    pnlAssociatePatient.SendToBack();
        //    pnlMailDetails.BringToFront();

        //}

        //private void oListControl_ItemClosedClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (oListControl != null)
        //        {
        //            for (int i = this.pnlAssociatePatient.Controls.Count - 1; i >= 0; i--)
        //            {
        //                if (this.pnlAssociatePatient.Controls[i].Name == oListControl.Name)
        //                {
        //                    this.pnlAssociatePatient.Controls.Remove(this.pnlAssociatePatient.Controls[i]);
        //                    break;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    pnlAssociatePatient.SendToBack();
        //    pnlMailDetails.BringToFront();
        //}

        #endregion

        private void SmartAssociation()
        {
            try
            {
                if (oSmartPatient != null)
                {
                    if (this.pnlAssociatePatient.Controls.Contains(oSmartPatient))
                    {
                        this.pnlAssociatePatient.Controls.Remove(oSmartPatient);
                    }
                    try
                    {
                        oSmartPatient.ItemSelectedClick -= new gloPatient.gloSmartPatientControl.ItemSelected(oSmartPatient_ItemSelectedClick);
                        oSmartPatient.ItemClosedClick -= new gloPatient.gloSmartPatientControl.ItemClosed(oSmartPatient_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oSmartPatient.Dispose();
                    oSmartPatient = null;
                }
                oSmartPatient = new gloPatient.gloSmartPatientControl(pnlAssociatePatient.Width, _databaseconnectionstring);
                oSmartPatient.ClinicID = _ClinicID;                
                oSmartPatient.ItemSelectedClick += new gloPatient.gloSmartPatientControl.ItemSelected(oSmartPatient_ItemSelectedClick);
                oSmartPatient.ItemClosedClick += new gloPatient.gloSmartPatientControl.ItemClosed(oSmartPatient_ItemClosedClick);
                this.pnlAssociatePatient.Controls.Add(oSmartPatient);
                oSmartPatient.LoadPatients();

                oSmartPatient.BringToFront();
                oSmartPatient.Dock = DockStyle.Fill;
                oSmartPatient.Select();
                pnlViewMail.Visible = false;
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void c1Mails_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                {
                    bool IsMailSelected = false; 
                    for (int i = 1; i < c1Mails.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(c1Mails.GetData(i, COL_MAIL_SELECT)) == true)
                        {
                            IsMailSelected = true; 
                        }
                    }

                    if (IsMailSelected == true)
                    {
                        SmartAssociation();
                        if (oSmartPatient != null)
                        {
                            oSmartPatient.txtSearch.Text = e.KeyChar.ToString();
                            oSmartPatient.txtSearch.SelectionStart = 1;
                            oSmartPatient.txtSearch.Select();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void oSmartPatient_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                if (oSmartPatient != null)
                {
                    pnlAssociatePatient.Controls.Remove(oSmartPatient);
                    try
                    {
                        oSmartPatient.ItemSelectedClick -= new gloPatient.gloSmartPatientControl.ItemSelected(oSmartPatient_ItemSelectedClick);
                        oSmartPatient.ItemClosedClick -= new gloPatient.gloSmartPatientControl.ItemClosed(oSmartPatient_ItemClosedClick);

                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            pnlViewMail.Visible = true;
        }

#endregion

        #region Grid Design
        
        private void DesignMailsGrid()
        {
            try
            {
                c1Mails.Rows.Count = 1;
                c1Mails.Rows.Fixed = 1;
                c1Mails.Cols.Count = COL_MAIL_COUNT;
                c1Mails.Cols.Fixed = 0;

                c1Mails.SetData(0, COL_MAIL_ITEMID, "Exchange ItemID");
                c1Mails.SetData(0, COL_MAIL_ITEMCHANGEKEY, "Exchange Item Change Key");
                c1Mails.SetData(0, COL_MAIL_SELECT, " ");
                c1Mails.SetData(0, COL_MAIL_FROM, "From");
                c1Mails.SetData(0, COL_MAIL_SUBJECT, "Subject");
                c1Mails.SetData(0, COL_MAIL_BODY, "Body");
                c1Mails.SetData(0, COL_MAIL_RECDATETIME, "Received");
                c1Mails.SetData(0, COL_MAIL_PMSID, "PMSID");
                c1Mails.SetData(0, COL_MAIL_PATIENTID, "Patient ID");
                c1Mails.SetData(0, COL_MAIL_PATIENTNAME, "Patient Name");

                c1Mails.Cols[COL_MAIL_SELECT].DataType = typeof(System.Boolean);
                c1Mails.Cols[COL_MAIL_RECDATETIME].DataType = typeof(System.DateTime);

                c1Mails.Cols[COL_MAIL_ITEMID].Visible = false;
                c1Mails.Cols[COL_MAIL_ITEMCHANGEKEY].Visible = false;
                c1Mails.Cols[COL_MAIL_SELECT].Visible = true;
                c1Mails.Cols[COL_MAIL_FROM].Visible = true;
                c1Mails.Cols[COL_MAIL_SUBJECT].Visible = true;
                c1Mails.Cols[COL_MAIL_BODY].Visible = false;
                c1Mails.Cols[COL_MAIL_RECDATETIME].Visible = true;
                c1Mails.Cols[COL_MAIL_PMSID].Visible = false;
                c1Mails.Cols[COL_MAIL_PATIENTID].Visible = false;
                c1Mails.Cols[COL_MAIL_PATIENTNAME].Visible = true;


                int _width = (pnlMailList.Width - 2);                
               
                c1Mails.Cols[COL_MAIL_SELECT].Width = Convert.ToInt32(_width * 0.08);
                c1Mails.Cols[COL_MAIL_FROM].Width = Convert.ToInt32(_width * 0.22); ;
                c1Mails.Cols[COL_MAIL_SUBJECT].Width = Convert.ToInt32(_width * 0.33);
                c1Mails.Cols[COL_MAIL_RECDATETIME].Width = Convert.ToInt32(_width * 0.15);
                c1Mails.Cols[COL_MAIL_PMSID].Width = 0;
                c1Mails.Cols[COL_MAIL_PATIENTNAME].Width = Convert.ToInt32(_width * 0.22);

                c1Mails.Cols[COL_MAIL_SELECT].AllowEditing = true;
                c1Mails.Cols[COL_MAIL_ITEMID].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_ITEMCHANGEKEY].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_FROM].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_SUBJECT].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_BODY].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_RECDATETIME].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_PMSID].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_PATIENTID].AllowEditing = false;
                c1Mails.Cols[COL_MAIL_PATIENTNAME].AllowEditing = false;

                c1Mails.Rows[0].Height = ROW_HEIGHT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DesignPatientMailGrid()
        {
            try
            {
                c1PatientMails.Rows.Count = 1;
                c1PatientMails.Rows.Fixed = 1;
                c1PatientMails.Cols.Count = COL_PATIENT_COUNT;
                c1PatientMails.Cols.Fixed = 0;

                c1PatientMails.SetData(0, COL_PATIENT_SELECT, " ");
                c1PatientMails.SetData(0, COL_PATIENT_PATIENTID, "Patient ID");
                c1PatientMails.SetData(0, COL_PATIENT_PATIENTNAME, "Patient Name");
                c1PatientMails.SetData(0, COL_PATIENT_ITEMID, "Exchange Item ID");
                c1PatientMails.SetData(0, COL_PATIENT_ITEMCHANGEKEY, "Exchange Change Key");
                c1PatientMails.SetData(0, COL_PATIENT_FROM, "From");
                c1PatientMails.SetData(0, COL_PATIENT_SUBJECT, "Subject");
                c1PatientMails.SetData(0, COL_PATIENT_BODY, "Body");
                c1PatientMails.SetData(0, COL_PATIENT_RECDATE, "Date");

                c1PatientMails.Cols[COL_PATIENT_RECDATE].DataType = typeof(System.DateTime);

                int Width = pnlMailList.Width-2;
                c1PatientMails.Cols[COL_PATIENT_SELECT].Width = Convert.ToInt32(Width * 0.05);
                c1PatientMails.Cols[COL_PATIENT_PATIENTID].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_PATIENTNAME].Width = Convert.ToInt32(Width * 0.2);
                c1PatientMails.Cols[COL_PATIENT_ITEMID].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_ITEMCHANGEKEY].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_FROM].Width = Convert.ToInt32(Width * 0.25);
                c1PatientMails.Cols[COL_PATIENT_SUBJECT].Width = Convert.ToInt32(Width * 0.35);
                c1PatientMails.Cols[COL_PATIENT_BODY].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_RECDATE].Width = Convert.ToInt32(Width * 0.15);

                c1PatientMails.Tree.Column = COL_PATIENT_SELECT;            
                c1PatientMails.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                c1PatientMails.AllowMerging = AllowMergingEnum.Nodes;


                 //Define Cell Style For Parent Node
                 C1.Win.C1FlexGrid.CellStyle  csParent;//= c1PatientMails.Styles.Add("cs_Parent");
                 try
                 {
                     if (c1PatientMails.Styles.Contains("cs_Parent"))
                     {
                         csParent = c1PatientMails.Styles["cs_Parent"];
                     }
                     else
                     {
                         csParent = c1PatientMails.Styles.Add("cs_Parent");
                         csParent.Font = gloGlobal.clsgloFont.gFont_SMALL;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                         csParent.BackColor = Color.FromArgb(222, 231, 250);
                         csParent.ForeColor = Color.DarkBlue;
                     }

                 }
                 catch
                 {
                     csParent = c1PatientMails.Styles.Add("cs_Parent");
                     csParent.Font = gloGlobal.clsgloFont.gFont_SMALL;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                     csParent.BackColor = Color.FromArgb(222, 231, 250);
                     csParent.ForeColor = Color.DarkBlue;


                 }
     
              
                //Cell Style For Child Node
                C1.Win.C1FlexGrid.CellStyle csProviderName ;//= c1PatientMails.Styles.Add("cs_Child");
                try
                {
                    if (c1PatientMails.Styles.Contains("cs_Child"))
                    {
                        csProviderName = c1PatientMails.Styles["cs_Child"];
                    }
                    else
                    {
                        csProviderName = c1PatientMails.Styles.Add("cs_Child");
                        csProviderName.Font = gloGlobal.clsgloFont.gFont_SMALL;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                        csProviderName.BackColor = Color.FromArgb(240, 247, 255);
                        csProviderName.ForeColor = Color.DarkBlue; 
                    }

                }
                catch
                {
                    csProviderName = c1PatientMails.Styles.Add("cs_Child");
                    csProviderName.Font = gloGlobal.clsgloFont.gFont_SMALL;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    csProviderName.BackColor = Color.FromArgb(240, 247, 255);
                    csProviderName.ForeColor = Color.DarkBlue; 


                }
              
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Attachments

        private void SetAttachments(string AttachmentName, Object Attachment)
        {
            try
            {

                pnlflow_Attachment.AutoScroll = true;
                pnlflow_Attachment.FlowDirection = FlowDirection.LeftToRight;

                lblAttachment = new Label();
                lblAttachment.AutoSize = true;

                lblAttachment.Text = "     " + AttachmentName;
                //lblAttachment.Text = AttachmentName;
                //
                lblAttachment.Tag = AttachmentName;
                lblAttachment.Image = imgMail.Images[0];// global::gloTaskMail.Properties.Resources.Attachment_01;
                lblAttachment.ImageAlign = ContentAlignment.MiddleLeft ;
                lblAttachment.TextAlign = ContentAlignment.MiddleLeft;
                //lblAttachment
                //lnkLblRemove = new LinkLabel();
                //lnkLblRemove.Click += new EventHandler(lnkLblRemove_Click);
                //lnkLblRemove.Text = "Remove";
                //lnkLblRemove.Tag = lblAttachment;


                lnkLblDownload = new LinkLabel();
                lnkLblDownload.AutoSize = true;
                lnkLblDownload.TextAlign = ContentAlignment.TopLeft;
                lnkLblDownload.Click += new EventHandler(lnkLblDownload_Click);
                lnkLblDownload.Text = "Download";
                lnkLblDownload.Tag = lblAttachment;


                lnkLblDownload.ImageKey = DateTime.Now.ToLongTimeString() + DateTime.Now.Millisecond.ToString();
                //lnkLblRemove.ImageKey = lnkLblDownload.ImageKey;


                //Convert the attachment and add it to the attachment label tag
                Byte[]  oArrAttachFile = (Byte[])Attachment;
                lblAttachment.Tag = oArrAttachFile;

                pnlflow_Attachment.Controls.Add(lblAttachment);
                //pnlflow_Attachment.Controls.Add(lnkLblRemove);
                //pnlflow_Attachment.SetFlowBreak(lnkLblRemove, true);

                pnlflow_Attachment.Controls.Add(lnkLblDownload);
                pnlflow_Attachment.SetFlowBreak(lnkLblDownload, true);
                webBrowser1.BringToFront();


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void lnkLblDownload_Click(object sender, EventArgs e)
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
                string Extention = lblAttachment.Text.Substring(lblAttachment.Text.LastIndexOf('.'), lblAttachment.Text.Length - lblAttachment.Text.LastIndexOf('.'));

                saveFileDialog1.DefaultExt = Extention;

                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    FileInfo oFileInfo = new FileInfo(saveFileDialog1.FileName);

                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        MessageBox.Show("File already exists");
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
                saveFileDialog1.DefaultExt = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

       private void lnkLblRemove_Click(object sender, EventArgs e)
        {
            //Label lblTempAttachment = new Label();
            //try
            //{

            //    //get the link label clicked,that link label tag has 
            //    //the attachment object related to it,retrive it from
            //    //the linklabel.
            //    lblTempAttachment = (Label)((LinkLabel)sender).Tag;

            //    //remove the attachment label from flow panel
            //    for (int i = 0; i < pnlflow_Attachment.Controls.Count; i++)
            //    {
            //        string strkey1 = ((Label)(pnlflow_Attachment.Controls[i])).ImageKey.ToString();
            //        string strkey2 = ((LinkLabel)sender).ImageKey.ToString();
            //        if (strkey1 == strkey2)
            //        {
            //            pnlflow_Attachment.Controls.RemoveAt(i + 1);
            //            break;
            //        }

            //    }

            //    pnlflow_Attachment.Controls.Remove(lblTempAttachment);
            //    pnlflow_Attachment.Controls.Remove(((LinkLabel)sender));
            //    pnlflow_Attachment.Refresh();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    lblTempAttachment.Dispose();
            //}

        }

        #endregion
      
        #region MouseHover And Leave

        private void btn_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloTaskMail.Properties.Resources.Img_ButtonHover;
            }
            catch
            {

            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            }
            catch
            {

            }
        } 
        
#endregion

        //Delete Temp files generated for displaying Emails
        private void frmViewUserMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                webBrowser1.Url = null;

                //if (Directory.Exists(Application.StartupPath + "\\Temp\\") == true)
                //{
                //    Directory.Delete(Application.StartupPath + "\\Temp\\", true);
                //}                
                if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\Temp\\") == true)
                {
                    Directory.Delete(appSettings["StartupPath"].ToString() + "\\Temp\\", true);
                }

            }
            catch
            {

            }
        }

        //Resize Mail Grids
        private void pnlMailList_Resize(object sender, EventArgs e)
        {
            try
            {
                int _width = pnlMailList.Width - 2;

                c1PatientMails.Cols[COL_PATIENT_SELECT].Width = Convert.ToInt32(_width * 0.05);
                c1PatientMails.Cols[COL_PATIENT_PATIENTID].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_PATIENTNAME].Width = Convert.ToInt32(_width * 0.2);
                c1PatientMails.Cols[COL_PATIENT_ITEMID].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_ITEMCHANGEKEY].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_FROM].Width = Convert.ToInt32(_width * 0.25);
                c1PatientMails.Cols[COL_PATIENT_SUBJECT].Width = Convert.ToInt32(_width * 0.35);
                c1PatientMails.Cols[COL_PATIENT_BODY].Width = 0;
                c1PatientMails.Cols[COL_PATIENT_RECDATE].Width = Convert.ToInt32(_width * 0.15);

                c1Mails.Cols[COL_MAIL_SELECT].Width = Convert.ToInt32(_width * 0.08);
                c1Mails.Cols[COL_MAIL_FROM].Width = Convert.ToInt32(_width * 0.22); ;
                c1Mails.Cols[COL_MAIL_SUBJECT].Width = Convert.ToInt32(_width * 0.33);
                c1Mails.Cols[COL_MAIL_RECDATETIME].Width = Convert.ToInt32(_width * 0.15);
                c1Mails.Cols[COL_MAIL_PMSID].Width = 0;
                c1Mails.Cols[COL_MAIL_PATIENTNAME].Width = Convert.ToInt32(_width * 0.22);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1PatientMails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
    
    }
}