using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using C1.Win.C1Editor;
using System.Text.RegularExpressions;
using System.IO;

namespace gloPatientPortal
{
    public partial class frmPortalMessages : Form
    {

        #region Variable declaration
        string _strConnectionString = string.Empty;
        string _strDMSconnectionstring = string.Empty;
        long _nLoginID;

        public enum MessageType
        {
            Messages = 1,
            EmailTemplates = 2,
            ReplySecureMessage = 3
        }

        public enum SubjectPlaceHolder
        {
            Patient = 1,
            Clinic = 2
        }
        int nMessageType = 0;
        #endregion

        #region Constructor
        public frmPortalMessages()
        {
            InitializeComponent();
        }

        public frmPortalMessages(string strConnectionString, long nLoginID, string strDMSconnectionstring)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
            _strDMSconnectionstring = strDMSconnectionstring;
        }
        #endregion

        private void frmPortalMessages_Load(object sender, EventArgs e)
        {
            c1EditorToolStripObjects1.Items[0].Visible = false;
            c1EditorToolStripObjects1.Items[1].Visible = false;
            c1EditorToolStripObjects1.Items[3].Visible = false;
            c1EditorToolStripObjects1.Items[4].Visible = false;
            bIsFromOnLoad = true;
            rbMessages.Checked = true;

        }
        string sEmailFullContent = string.Empty;
        string sNewEmailTemplate = string.Empty;
        int sLastSelectedRadio = 0;
        bool bIsFromOnLoad = false;


        private void FillMessageName()
        {
            DataTable dt = null;
            clsPortalEmailMessages _clsPortalEmailMessages = null;
            try
            {
                if (rbMessages.Checked)
                {
                    _clsPortalEmailMessages = new clsPortalEmailMessages();
                    dt = _clsPortalEmailMessages.FillControls(_strConnectionString, nMessageType);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbEmailMessage.SelectedIndexChanged -= new EventHandler(cmbEmailMessage_SelectedIndexChanged);
                        cmbEmailMessage.DataSource = dt;
                        cmbEmailMessage.DisplayMember = "sMessageName";
                        cmbEmailMessage.ValueMember = "nPortalMessageId";
                        cmbEmailMessage.SelectedIndex = -1;
                        cmbEmailMessage.SelectedIndexChanged += new EventHandler(cmbEmailMessage_SelectedIndexChanged);

                    }
                    else
                    {
                        cmbEmailMessage.DataSource = dt;
                    }

                }
                else if (rbEmailTemplate.Checked)
                {
                    _clsPortalEmailMessages = new clsPortalEmailMessages();
                    dt = _clsPortalEmailMessages.FillControls(_strConnectionString, nMessageType);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbEmailMessage.SelectedIndexChanged -= new EventHandler(cmbEmailMessage_SelectedIndexChanged);
                        cmbEmailMessage.DataSource = dt;
                        cmbEmailMessage.DisplayMember = "sDisplayTemplateName";
                        cmbEmailMessage.ValueMember = "nEmailTempId";
                        cmbEmailMessage.SelectedIndex = -1;
                        cmbEmailMessage.SelectedIndexChanged += new EventHandler(cmbEmailMessage_SelectedIndexChanged);

                    }

                }
                else if (rdReplySecureMessage.Checked)
                {
                    _clsPortalEmailMessages = new clsPortalEmailMessages();
                    dt = _clsPortalEmailMessages.FillControls(_strConnectionString, nMessageType);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbEmailMessage.SelectedIndexChanged -= new EventHandler(cmbEmailMessage_SelectedIndexChanged);
                        cmbEmailMessage.DataSource = dt;
                        cmbEmailMessage.DisplayMember = "sMessageName";
                        cmbEmailMessage.ValueMember = "nPortalMessageId";
                        cmbEmailMessage.SelectedIndex = -1;
                        cmbEmailMessage.SelectedIndexChanged += new EventHandler(cmbEmailMessage_SelectedIndexChanged);

                    }
                    else
                    {
                        cmbEmailMessage.DataSource = dt;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (_clsPortalEmailMessages != null)
                {
                    _clsPortalEmailMessages = null;
                }
            }


        }

        private void rbMessages_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMessages.Checked)
            {

                if (bIsFromOnLoad == false)
                {
                    if (MessageBox.Show("Do you want to discard the existing changes?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        if (sLastSelectedRadio == 2)
                        {

                            rdReplySecureMessage.CheckedChanged -= new EventHandler(rdReplySecureMessage_CheckedChanged);
                            rdReplySecureMessage.Checked = true;
                            rdReplySecureMessage.Font = new Font("Tahoma", 9, FontStyle.Bold);
                            rdReplySecureMessage.CheckedChanged += new EventHandler(rdReplySecureMessage_CheckedChanged);
                            //SecureMessageRadioSelect();
                        }
                        else if (sLastSelectedRadio == 1)
                        {

                            rbEmailTemplate.CheckedChanged -= new EventHandler(rbEmailTemplate_CheckedChanged);
                            rbEmailTemplate.Checked = true;
                            rbEmailTemplate.Font = new Font("Tahoma", 9, FontStyle.Bold);
                            rbEmailTemplate.CheckedChanged += new EventHandler(rbEmailTemplate_CheckedChanged);
                            //TemplateRadioSelect();
                        }

                        return;
                    }
                }
                bIsFromOnLoad = false;
                sLastSelectedRadio = 0;
                DisclaimerRadioChanged();
            }
            else
            {
                rbMessages.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void DisclaimerRadioChanged()
        {
            c1EditorEmailMessageBody.Xml = "";
            txtEmailSubject.Text = "";
            nMessageType = (int)MessageType.Messages;
            FillMessageName();
            if (cmbEmailMessage.DataSource != null)
            {
                bEmailMessageValidation = false;
                cmbEmailMessage.SelectedIndex = 0;
                bEmailMessageValidation = true;
            }
            pnlEmailSubject.Visible = false;
            pnlPlaceDisplay.Visible = false;
            pnlToolStrip.Visible = true;
            pnlMainBody.Visible = true;
            pnlTxtMsgBody.Visible = false;
            tsc_MessageController.Visible = true;
            rbMessages.Font = new Font("Tahoma", 9, FontStyle.Bold);
            ShowHideToolstripButtons();
        }

        private void ShowHideToolstripButtons()
        {
            if (nMessageType == 1)
            {
                ts_Preview.Visible = false;
                ts_cancel.Visible = false;
                ts_Save.Visible = true;
                ts_Publish.Visible = false;
                ts_Back.Visible = false;
            }
            else if (nMessageType == 2)
            {
                ts_Preview.Visible = true;
                ts_cancel.Visible = false;
                ts_Save.Visible = true;
                ts_Publish.Visible = false;
                ts_Back.Visible = false;
            }
            else if (nMessageType == 3)
            {
                ts_Preview.Visible = false;
                ts_cancel.Visible = false;
                ts_Save.Visible = true;
                ts_Publish.Visible = false;
                ts_Back.Visible = false;
            }
        }

        private void rbEmailTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEmailTemplate.Checked)
            {

                if (MessageBox.Show("Do you want to discard the existing changes?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    if (sLastSelectedRadio == 0)
                    {

                        rbMessages.CheckedChanged -= new EventHandler(rbMessages_CheckedChanged);
                        rbMessages.Checked = true;
                        rbMessages.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        rbMessages.CheckedChanged += new EventHandler(rbMessages_CheckedChanged);
                        //DisclaimerRadioChanged();
                    }
                    else if (sLastSelectedRadio == 2)
                    {

                        rdReplySecureMessage.CheckedChanged -= new EventHandler(rdReplySecureMessage_CheckedChanged);
                        rdReplySecureMessage.Checked = true;
                        rdReplySecureMessage.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        rdReplySecureMessage.CheckedChanged += new EventHandler(rdReplySecureMessage_CheckedChanged);
                        //SecureMessageRadioSelect();
                    }
                    return;
                }
                sLastSelectedRadio = 1;
                TemplateRadioSelect();
            }
            else
            {
                rbEmailTemplate.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void TemplateRadioSelect()
        {
            c1EditorEmailMessageBody.Xml = "";
            txtEmailSubject.Text = "";
            nMessageType = (int)MessageType.EmailTemplates;
            FillMessageName();
            if (cmbEmailMessage.DataSource != null)
            {
                bEmailMessageValidation = false;
                cmbEmailMessage.SelectedIndex = 0;
                bEmailMessageValidation = true;
            }
            FillPlaceholder();
            pnlEmailSubject.Visible = true;
            pnlPlaceDisplay.Visible = true;
            pnlToolStrip.Visible = true;
            pnlMainBody.Visible = true;
            pnlTxtMsgBody.Visible = false;
            tsc_MessageController.Visible = true;
            rbEmailTemplate.Font = new Font("Tahoma", 9, FontStyle.Bold);
            ShowHideToolstripButtons();
        }

        private void rdReplySecureMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdReplySecureMessage.Checked)
            {
                if (MessageBox.Show("Do you want to discard the existing changes?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    if (sLastSelectedRadio == 0)
                    {

                        rbMessages.CheckedChanged -= new EventHandler(rbMessages_CheckedChanged);
                        rbMessages.Checked = true;
                        rbMessages.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        rbMessages.CheckedChanged += new EventHandler(rbMessages_CheckedChanged);
                        //DisclaimerRadioChanged();
                    }
                    else if (sLastSelectedRadio == 1)
                    {

                        rbEmailTemplate.CheckedChanged -= new EventHandler(rbEmailTemplate_CheckedChanged);
                        rbEmailTemplate.Checked = true;
                        rbEmailTemplate.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        rbEmailTemplate.CheckedChanged += new EventHandler(rbEmailTemplate_CheckedChanged);
                        //TemplateRadioSelect();
                    }
                    return;
                }

                sLastSelectedRadio = 2;
                SecureMessageRadioSelect();
            }
            else
            {
                rdReplySecureMessage.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void SecureMessageRadioSelect()
        {
            c1EditorEmailMessageBody.Xml = "";
            txtEmailSubject.Text = "";
            nMessageType = (int)MessageType.ReplySecureMessage;
            FillMessageName();
            if (cmbEmailMessage.DataSource != null)
            {
                bEmailMessageValidation = false;
                cmbEmailMessage.SelectedIndex = 0;
                bEmailMessageValidation = true;
            }
            pnlEmailSubject.Visible = false;
            pnlPlaceDisplay.Visible = false;
            pnlToolStrip.Visible = false;
            pnlMainBody.Visible = false;
            pnlTxtMsgBody.Visible = true;
            tsc_MessageController.Visible = false;
            rdReplySecureMessage.Font = new Font("Tahoma", 9, FontStyle.Bold);
            ShowHideToolstripButtons();
        }

        private void FillPlaceholder()
        {
            DataTable dtPlaceHolder = new DataTable();
            try
            {
                dtPlaceHolder.Columns.Add("PlaceholderName");
                dtPlaceHolder.Columns.Add("PlaceholderValue");

                DataRow dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Select";
                dr["PlaceholderValue"] = "";
                dtPlaceHolder.Rows.Add(dr);

                dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Patient Name";
                dr["PlaceholderValue"] = "{#PatientName#}";
                dtPlaceHolder.Rows.Add(dr);

                dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Clinic Name";
                dr["PlaceholderValue"] = "{#ClinicName#}";
                dtPlaceHolder.Rows.Add(dr);

                dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Patient Portal Activation Link";
                //dr["PlaceholderValue"] = "https://{#Link#}";
                dr["PlaceholderValue"] = "https://www.PatientPortalActivationLink.com/index.html".ToLower();
                dtPlaceHolder.Rows.Add(dr);

                dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Patient Portal Link";
                dr["PlaceholderValue"] = "https://www.PatientPortalLoginLink.com/index.html".ToLower();
                dtPlaceHolder.Rows.Add(dr);

                dr = dtPlaceHolder.NewRow();
                dr["PlaceholderName"] = "Phone Number";
                dr["PlaceholderValue"] = "{#PhoneNo#}";
                dtPlaceHolder.Rows.Add(dr);

                SetPlaceholderCombo(dtPlaceHolder);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetPlaceholderCombo(DataTable dt)
        {
            try
            {
                cmbPlaceHolder.SelectedIndexChanged -= new EventHandler(cmbPlaceHolder_SelectedIndexChanged);
                cmbPlaceHolder.DataSource = dt;
                cmbPlaceHolder.DisplayMember = "PlaceholderName";
                cmbPlaceHolder.ValueMember = "PlaceholderValue";
                cmbPlaceHolder.SelectedIndexChanged += new EventHandler(cmbPlaceHolder_SelectedIndexChanged);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        bool bEmailMessageValidation = false;
        int nEmailMessageLastIndex = 0;
        bool bIsActivationEmailTemplate = false;
        DataTable dtMessageTemplate = null;
        private void cmbEmailMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

            clsPortalEmailMessages _clsPortalEmailMessages = null;
            //DataTable dt = null;
            try
            {
                if (Convert.ToInt64(cmbEmailMessage.SelectedValue) < 0)
                {
                    ResetControl();
                    return;
                }
                if (bEmailMessageValidation)
                {
                    if (MessageBox.Show("Do you want to discard the existing changes?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        cmbEmailMessage.SelectedIndexChanged -= new EventHandler(cmbEmailMessage_SelectedIndexChanged);
                        cmbEmailMessage.SelectedIndex = nEmailMessageLastIndex;
                        cmbEmailMessage.SelectedIndexChanged += new EventHandler(cmbEmailMessage_SelectedIndexChanged);
                        return;
                    }
                }
                nEmailMessageLastIndex = cmbEmailMessage.SelectedIndex;

                if (nMessageType == 2)
                {
                    if (cmbPlaceHolder.SelectedIndex > 0)
                    {
                        cmbPlaceHolder.SelectedIndex = 0;
                    }
                    txtLinkDisplayName.Text = "";
                }


                _clsPortalEmailMessages = new clsPortalEmailMessages();
                dtMessageTemplate = _clsPortalEmailMessages.GetEmailMessageDetails(_strConnectionString, nMessageType, Convert.ToInt64(cmbEmailMessage.SelectedValue));
                if (dtMessageTemplate != null && dtMessageTemplate.Rows.Count > 0)
                {
                    if (nMessageType == 2)
                    {
                        c1EditorEmailMessageBody.Mode = C1.Win.C1Editor.EditorMode.Design;

                        txtEmailSubject.Text = Convert.ToString(dtMessageTemplate.Rows[0]["sSubject"]);
                        string sEmailText = Convert.ToString(dtMessageTemplate.Rows[0]["sMsgBody"]);
                        sEmailFullContent = sEmailText;
                        string sEmailContent = sEmailText.Substring(sEmailText.IndexOf("<!--Start Email Template-->"), ((sEmailText.IndexOf("<!--End Email Template-->") + "<!--End Email Template-->".Length) - sEmailText.IndexOf("<!--Start Email Template-->")));
                        sEmailContent = sEmailContent.Replace("\"", "'");
                        sEmailContent = sEmailContent.Replace("href='{#Link#}'", "href='https://www.PatientPortalActivationLink.com/index.html'".ToLower());
                        sEmailContent = sEmailContent.Replace("href='{#Link_Login#}'", "href='https://www.PatientPortalLoginLink.com/index.html'".ToLower());
                        sEmailContent = sEmailContent.Replace("<span>", "<div>");
                        sEmailContent = sEmailContent.Replace("</span>", "</div>");
                        //sEmailContent = sEmailContent.Insert(sEmailContent.Length, "</div>");
                        c1EditorEmailMessageBody.LoadXml(sEmailContent, null);

                        if (Convert.ToBoolean(dtMessageTemplate.Rows[0]["bIsPR"]) == false)
                        {
                            btnPLCHPatientName.Visible = false;
                            AddRemovePRPlaceHolder(false);
                        }
                        else
                        {
                            btnPLCHPatientName.Visible = true;
                            AddRemovePRPlaceHolder(true);
                        }
                        AddForgotPasswordPlaceHolder(Convert.ToString(dtMessageTemplate.Rows[0]["sTemplateCode"]));
                        if (Convert.ToString(dtMessageTemplate.Rows[0]["sTemplateCode"]) == "PATIENTINVITATION" || Convert.ToString(dtMessageTemplate.Rows[0]["sTemplateCode"]) == "PATIENTINVITATION - Resent")
                            bIsActivationEmailTemplate = true;
                        else
                            bIsActivationEmailTemplate = false;
                    }
                    else if (nMessageType == 1)
                    {
                        //c1EditorEmailMessageBody.Text = Convert.ToString(dt.Rows[0]["sMessageBody"]);
                        c1EditorEmailMessageBody.LoadXml(Convert.ToString(dtMessageTemplate.Rows[0]["sMessageBody"]), null);
                        bIsActivationEmailTemplate = false;
                    }
                    else if (nMessageType == 3)
                    {
                        //c1EditorEmailMessageBody.Text = Convert.ToString(dtMessageTemplate.Rows[0]["sMessageBody"]);
                        txtMessageBody.Text = Convert.ToString(dtMessageTemplate.Rows[0]["sMessageBody"]);
                        bIsActivationEmailTemplate = false;
                    }
                }
                c1EditorEmailMessageBody.Focus();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_clsPortalEmailMessages != null)
                {
                    _clsPortalEmailMessages = null;
                }
            }

        }

        private void AddForgotPasswordPlaceHolder(string TemplateCode)
        {
            DataTable dt = (DataTable)cmbPlaceHolder.DataSource;
            try
            {
                if (dt != null)
                {
                    if (TemplateCode == "FORGOT PASSWORD")
                    {
                        DataRow dr = null;
                        DataRow[] drUserName = dt.Select("PlaceholderName='User Name'");
                        if (drUserName != null && drUserName.Length == 0)
                        {
                            dr = dt.NewRow();
                            dr["PlaceholderName"] = "User Name";
                            dr["PlaceholderValue"] = "{#UserName#}";
                            dt.Rows.Add(dr);
                        }
                        DataRow[] drTemporaryPassword = dt.Select("PlaceholderName='Temporary Password'");
                        if (drTemporaryPassword != null && drTemporaryPassword.Length == 0)
                        {
                            dr = dt.NewRow();
                            dr["PlaceholderName"] = "Temporary Password";
                            dr["PlaceholderValue"] = "{#TempPassword#}";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow[] drUserName = dt.Select("PlaceholderName='User Name'");
                        if (drUserName != null && drUserName.Length > 0)
                        {
                            dt.Rows.Remove(drUserName[0]);
                        }
                        DataRow[] drTemporaryPassword = dt.Select("PlaceholderName='Temporary Password'");
                        if (drTemporaryPassword != null && drTemporaryPassword.Length > 0)
                        {
                            dt.Rows.Remove(drTemporaryPassword[0]);
                        }
                    }
                    SetPlaceholderCombo(dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddRemovePRPlaceHolder(bool bIsPR)
        {
            DataTable dt = (DataTable)cmbPlaceHolder.DataSource;
            try
            {
                if (dt != null)
                {
                    if (bIsPR)
                    {
                        DataRow[] drSelect = dt.Select("PlaceholderName='Patient Repesentative Name'");
                        if (drSelect != null && drSelect.Length == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["PlaceholderName"] = "Patient Repesentative Name";
                            dr["PlaceholderValue"] = "{#PatientRepresentativeName#}";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow[] dr = dt.Select("PlaceholderName='Patient Repesentative Name'");
                        if (dr != null && dr.Length > 0)
                        {
                            dt.Rows.Remove(dr[0]);
                        }
                    }
                    SetPlaceholderCombo(dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ResetControl()
        {
            if (nMessageType == 1)
            {
                cmbEmailMessage.SelectedIndex = 0;
                c1EditorEmailMessageBody.Text = "";
                c1EditorEmailMessageBody.Xml = "";
            }
            else if (nMessageType == 2)
            {
                cmbEmailMessage.SelectedIndex = 0;
                c1EditorEmailMessageBody.Text = "";
                c1EditorEmailMessageBody.Xml = "";
                txtEmailSubject.Text = "";
                cmbPlaceHolder.SelectedIndex = 0;
                txtLinkDisplayName.Text = "";
            }
            else if (nMessageType == 3)
            {
                cmbEmailMessage.SelectedIndex = 0;
                c1EditorEmailMessageBody.Text = "";
                c1EditorEmailMessageBody.Xml = "";
                txtMessageBody.Text = "";
            }
        }

        private void btnAddPlaceHolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (nMessageType == 2)
                {
                    if (bIsActivationEmailTemplate == false && cmbPlaceHolder.Text.ToString() == "Patient Portal Activation Link")
                    {
                        MessageBox.Show("Activation link can not added. Activation link is not supported in template other than \"Patient Activation\".", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPlaceHolder.SelectedIndex = 0;
                        return;
                    }
                }
                if (cmbPlaceHolder.SelectedValue.ToString().Trim() != "")
                {
                    string sDisplayName = string.Empty;
                    string sLink = string.Empty;
                    string sPlaceHolderValue = string.Empty;

                    if (cmbPlaceHolder.Text.ToString() == "Patient Portal Activation Link")
                    {
                        if (txtLinkDisplayName.Text.Trim() == "")
                        {
                            //MessageBox.Show("Enter display name for link.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                            sDisplayName = "{#Link#}";
                            sLink = string.Format("<a href='{0}'>{1}</a>", cmbPlaceHolder.SelectedValue.ToString(), sDisplayName);
                        }
                        else
                        {
                            sDisplayName = txtLinkDisplayName.Text.Trim();
                            sLink = string.Format("<a href='{0}'>{1}</a>", cmbPlaceHolder.SelectedValue.ToString(), sDisplayName);
                        }
                    }
                    else if (cmbPlaceHolder.Text.ToString() == "Patient Portal Link")
                    {
                        if (txtLinkDisplayName.Text.Trim() == "")
                        {
                            //MessageBox.Show("Enter display name for link.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                            sDisplayName = "{#Link_Login#}";
                            sLink = string.Format("<a href='{0}'>{1}</a>", cmbPlaceHolder.SelectedValue.ToString(), sDisplayName);
                        }
                        else
                        {
                            sDisplayName = txtLinkDisplayName.Text.Trim();
                            sLink = string.Format("<a href='{0}'>{1}</a>", cmbPlaceHolder.SelectedValue.ToString(), sDisplayName);
                        }
                    }
                    else
                    {
                        sLink = string.Format("{0}", cmbPlaceHolder.SelectedValue.ToString());
                    }

                    sPlaceHolderValue = sLink;

                    C1TextRange range = c1EditorEmailMessageBody.Selection;
                    range.XmlText = sPlaceHolderValue;

                    cmbPlaceHolder.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Please select placeholder.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private string GetEmailContent(string sEmailContent)
        {

            string endHeader = "<!--Start Email Template-->";
            string sEmailHeaderContent = string.Empty;
            string sEmailFooterContent = string.Empty;
            string startfooter = "<!--End Email Template-->";
            string sFullemail = string.Empty;

            if (nMessageType == 1)
            {
                sEmailContent = GetTemplateXml(sEmailContent);
                sFullemail = sEmailContent;
            }
            else if (nMessageType == 2)
            {
                sEmailContent = GetTemplateXml(sEmailContent);
                sEmailContent = sEmailContent.Insert(0, endHeader);
                if (!sEmailContent.Contains(startfooter))
                {
                    sEmailContent = sEmailContent.Insert(sEmailContent.Length, startfooter);
                }
                sEmailContent = FindAllHref(sEmailContent);

                sEmailHeaderContent = sEmailFullContent.Substring(0, sEmailFullContent.IndexOf(endHeader));
                sEmailFooterContent = sEmailFullContent.Substring(sEmailFullContent.IndexOf(startfooter) + startfooter.Length, ((sEmailFullContent.Length - startfooter.Length) - sEmailFullContent.IndexOf(startfooter)));
                sFullemail = sEmailHeaderContent + sEmailContent + sEmailFooterContent;

                sFullemail = Regex.Replace(sFullemail, @"\r\n+", "");
            }
            else if (nMessageType == 3)
            {
                //sEmailContent = c1EditorEmailMessageBody.Text;
                sEmailContent = txtMessageBody.Text;
                sFullemail = sEmailContent;
            }
            //MessageBox.Show(sFullemail.Trim());
            return sFullemail.Trim();
        }

        private string GetTemplateXml(string sEmailContent)
        {
            string startbody = "<body>";
            string endbody = "</body>";
            sEmailContent = sEmailContent.Substring(sEmailContent.IndexOf(startbody) + startbody.Length, ((sEmailContent.IndexOf(endbody) - endbody.Length) - sEmailContent.IndexOf(startbody)));
            return sEmailContent;
        }

        private string FindAllHref(string sInputEmailContent)
        {
            List<string> lstHrefs = new List<string>();

            string pattern = "<(a).*?href=(\"|')(.+?)(\"|').*?>";
            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(sInputEmailContent);

            foreach (var item in match)
            {
                if (item.ToString().Contains("https://www.PatientPortalActivationLink.com/index.html".ToLower()))
                {
                    sInputEmailContent = sInputEmailContent.Replace("https://www.PatientPortalActivationLink.com/index.html".ToLower(), "{#Link#}");
                }
                if (item.ToString().Contains("https://www.PatientPortalLoginLink.com/index.html".ToLower()))
                {
                    sInputEmailContent = sInputEmailContent.Replace("https://www.PatientPortalLoginLink.com/index.html".ToLower(), "{#Link_Login#}");
                }
            }

            return sInputEmailContent;
        }

        private void cmbPlaceHolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlaceHolder.Text.ToString() == "Patient Portal Activation Link")
            {
                pnlDisplayName.Visible = true;
                txtLinkDisplayName.Text = "";
            }
            else if (cmbPlaceHolder.Text.ToString() == "Patient Portal Link")
            {
                pnlDisplayName.Visible = true;
                txtLinkDisplayName.Text = "";
            }
            else
            {
                pnlDisplayName.Visible = false;
                txtLinkDisplayName.Text = "";
            }
        }

        private void btnPLCHClinicName_Click(object sender, EventArgs e)
        {
            AddPlaceHolderToSubject((int)SubjectPlaceHolder.Clinic);
        }

        private void btnPLCHPatientName_Click(object sender, EventArgs e)
        {
            AddPlaceHolderToSubject((int)SubjectPlaceHolder.Patient);
        }

        private void AddPlaceHolderToSubject(int nPlaceHolderType)
        {
            if (nPlaceHolderType == Convert.ToInt16(SubjectPlaceHolder.Clinic))
            {

                int position = txtEmailSubject.SelectionStart; ;
                txtEmailSubject.Text = txtEmailSubject.Text.Insert(position, "{#ClinicName#}");
                txtEmailSubject.SelectionStart = position;
                txtEmailSubject.SelectionLength = "{#ClinicName#}".Length;

            }
            else if (nPlaceHolderType == Convert.ToInt16(SubjectPlaceHolder.Patient))
            {

                int position = txtEmailSubject.SelectionStart; ;
                txtEmailSubject.Text = txtEmailSubject.Text.Insert(position, "{#PatientName#}");
                txtEmailSubject.SelectionStart = position;
                txtEmailSubject.SelectionLength = "{#PatientName#}".Length;
            }
        }

        private void c1EditorEmailMessageBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("+{ENTER}");
            }
            if (e.Modifiers == Keys.Shift && e.KeyValue == 13)
            {
                e.SuppressKeyPress = false;
            }
        }

        private void LoadEmailControl()
        {
            if (sNewEmailTemplate != "")
            {
                string path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.ToString().LastIndexOf("\\")) + @"\HealthFormPreview\EmailTemplatePreview.htm";
                string sHTMLContent = File.ReadAllText(path);

                string start = "<!--Start Email Template-->";
                string end = "<!--End Email Template-->";


                sHTMLContent = sHTMLContent.Substring(sHTMLContent.IndexOf(start) + start.Length, ((sHTMLContent.IndexOf(end) - end.Length) - sHTMLContent.IndexOf(start)));
                sHTMLContent = sNewEmailTemplate;
                sHTMLContent.Insert(0, start);
                sHTMLContent.Insert(sHTMLContent.Length, end);
                File.WriteAllText(path, sHTMLContent);

                webBrowser1.Url = new Uri(path);

            }
        }

        private void ts_Save_Click(object sender, EventArgs e)
        {
            string sEmailContent = string.Empty;
            string sEmailSubject = string.Empty;

            if (nMessageType == 2)
            {
                DialogResult _diaResult = validatePlaceHolder();

                if (_diaResult == DialogResult.No || _diaResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (nMessageType == 1)
            {
                sEmailContent = GetEmailContent(c1EditorEmailMessageBody.SaveXml());
                if (sEmailContent == "<p>&nbsp;</p>")
                {
                    sEmailContent = "";
                }
                sEmailSubject = string.Empty;
            }
            else if (nMessageType == 2)
            {
                if (c1EditorEmailMessageBody.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter message body.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtEmailSubject.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter subject", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (((System.Data.DataRowView)(cmbEmailMessage.SelectedItem)).Row.ItemArray[1].ToString().Contains("Patient Activation"))
                {
                    if (c1EditorEmailMessageBody.Xml.Contains("https://www.PatientPortalActivationLink.com/index.html".ToLower()) == false)
                    {
                        MessageBox.Show("Activation template must have activation link", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                sEmailContent = GetEmailContent(c1EditorEmailMessageBody.SaveXml());
                sEmailSubject = txtEmailSubject.Text.Trim();
            }
            if (nMessageType == 3)
            {
                //sEmailContent = c1EditorEmailMessageBody.Text;
                sEmailContent = txtMessageBody.Text; 
                sEmailSubject = string.Empty;
            }
            //MessageBox.Show(sEmailContent);

            if (MessageBox.Show("Do you want to save changes?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            clsPortalEmailMessages _clsPortalEmailMessages = null;
            Int64 nEmailMessageID = 0;
            try
            {

                _clsPortalEmailMessages = new clsPortalEmailMessages();
                nEmailMessageID = _clsPortalEmailMessages.UpdateEmailMessageDetails(_strConnectionString, nMessageType, Convert.ToInt64(cmbEmailMessage.SelectedValue), sEmailSubject, sEmailContent, _nLoginID);

                if (nEmailMessageID > 0)
                {
                    string sMessageName = "Template";
                    if (nMessageType == 1)
                    {
                        sMessageName = "Portal Disclaimer";
                    }
                    else if (nMessageType == 2)
                    {
                        sMessageName = "Email template";
                    }
                    else if (nMessageType == 3)
                    {
                        sMessageName = "Reply to Secure message";
                    }
                    MessageBox.Show(sMessageName + " updated successfully.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                if (_clsPortalEmailMessages != null)
                {
                    _clsPortalEmailMessages = null;
                }
            }
        }

        private DialogResult validatePlaceHolder()
        {
            bool _result = false;
            bool _resultSubject = false;
            DialogResult _dialogResult = DialogResult.Yes;
            string sMessage = string.Empty;

            List<string> lstplaceholder = findAllPlaceHolder(c1EditorEmailMessageBody.SaveXml());
            List<string> lstSubjectplaceholder = findAllPlaceHolder(txtEmailSubject.Text.ToString());
            string sUnmatchPlaceHolder = string.Empty;
            foreach (var item in lstplaceholder)
            {
                if (item == "{#PatientName#}")
                {

                }
                else if (item == "{#PatientRepresentativeName#}")
                {

                }
                else if (item == "{#UserName#}")
                {

                }
                else if (item == "{#TempPassword#}")
                {

                }
                else if (item == "{#ClinicName#}")
                {

                }
                else if (item == "{#Link#}")
                {

                }
                else if (item == "{#Link_Login#}")
                {

                }
                else if (item == "{#PhoneNo#}")
                {

                }
                else
                {
                    sUnmatchPlaceHolder = sUnmatchPlaceHolder + "," + item;
                    _result = true;
                }
            }

            foreach (var item in lstSubjectplaceholder)
            {
                if (item == "{#PatientName#}")
                {

                }
                else if (item == "{#ClinicName#}")
                {

                }
                else
                {
                    sUnmatchPlaceHolder = sUnmatchPlaceHolder + "," + item;
                    _resultSubject = true;
                }
            }

            if (_result && _resultSubject)
            {
                sMessage = string.Format("The template and subject both has incomplete place holders, leads to show incomplete content. Do you still want to save and continue? \n\r\n\rPlace holder: Starts with {0} and end with {1}. \n\rExample {2}", "{#", "#}", "{#PATINET NAME#}");
                _dialogResult = MessageBox.Show(sMessage, "gloEMRAdmin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            }
            else if (_result)
            {
                sMessage = string.Format("The template has incomplete place holders, leads to show incomplete content. Do you still want to save and continue? \n\r\n\rPlace holder: Starts with {0} and end with {1}. \n\rExample {2}", "{#", "#}", "{#PATINET NAME#}");
                _dialogResult = MessageBox.Show(sMessage, "gloEMRAdmin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            }
            else if (_resultSubject)
            {
                sMessage = string.Format("The subject has incomplete place holders, leads to show incomplete content. Do you still want to save and continue? \n\r\n\rPlace holder: Starts with {0} and end with {1}. \n\rExample {2}", "{#", "#}", "{#PATINET NAME#}");
                _dialogResult = MessageBox.Show(sMessage, "gloEMRAdmin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            }
            return _dialogResult;
        }

        private List<string> findAllPlaceHolder(string sEmailTest)
        {
            List<string> lstHrefs = new List<string>();

            string pattern = "({#[a-zA-Z0-9_]*#})|(({#[a-zA-Z0-9]*)|([a-zA-Z0-9]*#}))";
            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(sEmailTest);
            foreach (var item in match)
            {
                lstHrefs.Add(item.ToString());
            }

            return lstHrefs;
        }

        private void ts_Preview_Click(object sender, EventArgs e)
        {
            if (c1EditorEmailMessageBody.Text.Trim() == "")
            {
                MessageBox.Show("Please enter message body.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pnlWebBrowser.Visible = true;
            pnlWebBrowser.Focus();
            pnlWebBrowser.BringToFront();

            ts_Back.Visible = true;
            ts_Save.Visible = true;
            ts_Preview.Visible = false;
            ts_cancel.Visible = false;
            ts_ClearMessage.Visible = false;
            ts_RestoreDefault.Visible = false;

            sNewEmailTemplate = GetEmailContent(c1EditorEmailMessageBody.SaveXml());
            pnlMainContent.Hide();
            LoadEmailControl();
        }

        private void ts_Publish_Click(object sender, EventArgs e)
        {

        }

        private void ts_cancel_Click(object sender, EventArgs e)
        {
            cmbEmailMessage.SelectedIndex = -1;
            txtEmailSubject.Text = "";
            txtLinkDisplayName.Text = "";
            cmbPlaceHolder.SelectedIndex = -1;
            c1EditorEmailMessageBody.Xml = "";
        }

        private void ts_Back_Click(object sender, EventArgs e)
        {
            pnlWebBrowser.Visible = false;
            pnlWebBrowser.SendToBack();
            ts_ClearMessage.Visible = true;
            ts_RestoreDefault.Visible = true;
            pnlMainContent.Show();
            ShowHideToolstripButtons();
        }

        private void ts_ClearMessage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the message?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            c1EditorEmailMessageBody.Xml = "";
            if (nMessageType==3)
            {
                txtMessageBody.Text = "";
            }
        }

        private void ts_RestoreDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to restore the default message?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            //DataTable dt = null;
            clsPortalEmailMessages _clsPortalEmailMessages = null;
            try
            {
                _clsPortalEmailMessages = new clsPortalEmailMessages();
                //dt = _clsPortalEmailMessages.GetEmailMessageDetails(_strConnectionString, nMessageType, Convert.ToInt64(cmbEmailMessage.SelectedValue));
                if (dtMessageTemplate != null && dtMessageTemplate.Rows.Count > 0)
                {
                    if (nMessageType == 2)
                    {
                        c1EditorEmailMessageBody.Mode = C1.Win.C1Editor.EditorMode.Design;

                        string sEmailText = Convert.ToString(dtMessageTemplate.Rows[0]["sDefaultMsgBody"]);
                        sEmailFullContent = sEmailText;
                        string sEmailContent = sEmailText.Substring(sEmailText.IndexOf("<!--Start Email Template-->"), ((sEmailText.IndexOf("<!--End Email Template-->") + "<!--End Email Template-->".Length) - sEmailText.IndexOf("<!--Start Email Template-->")));
                        sEmailContent = sEmailContent.Replace("\"", "'");
                        sEmailContent = sEmailContent.Replace("href='{#Link#}'", "href='https://www.PatientPortalActivationLink.com/index.html'".ToLower());
                        sEmailContent = sEmailContent.Replace("href='{#Link_Login#}'", "href='https://www.PatientPortalLoginLink.com/index.html'".ToLower());
                        sEmailContent = sEmailContent.Replace("<span>", "<div>");
                        sEmailContent = sEmailContent.Replace("</span>", "</div>");
                        //sEmailContent = sEmailContent.Insert(sEmailContent.Length, "</div>");
                        c1EditorEmailMessageBody.LoadXml(sEmailContent, null);

                        if (Convert.ToBoolean(dtMessageTemplate.Rows[0]["bIsPR"]) == false)
                        {
                            btnPLCHPatientName.Visible = false;
                        }
                        else
                        {
                            btnPLCHPatientName.Visible = true;
                        }
                        if (Convert.ToString(dtMessageTemplate.Rows[0]["sTemplateCode"]) == "PATIENTINVITATION" || Convert.ToString(dtMessageTemplate.Rows[0]["sTemplateCode"]) == "PATIENTINVITATION - Resent")
                            bIsActivationEmailTemplate = true;
                        else
                            bIsActivationEmailTemplate = false;
                    }
                    else if (nMessageType == 1)
                    {
                        //c1EditorEmailMessageBody.Text = Convert.ToString(dt.Rows[0]["sMessageBody"]);
                        c1EditorEmailMessageBody.LoadXml(Convert.ToString(dtMessageTemplate.Rows[0]["sDefaultMsgBody"]), null);
                        bIsActivationEmailTemplate = false;
                    }
                    else if (nMessageType == 3)
                    {
                        //c1EditorEmailMessageBody.Text = Convert.ToString(dtMessageTemplate.Rows[0]["sDefaultMsgBody"]);
                        txtMessageBody.Text = Convert.ToString(dtMessageTemplate.Rows[0]["sDefaultMsgBody"]); 
                        bIsActivationEmailTemplate = false;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (_clsPortalEmailMessages != null)
                {
                    _clsPortalEmailMessages = null;
                }
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatientPortal.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatientPortal.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

    }
}
