using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloDatabaseLayer;
using gloAuditTrail;
using gloBilling.EOBPayment;
using C1.Win.C1FlexGrid;


namespace gloBilling
{
    public partial class frmViewPatientPayment : Form
    {

        #region "Variable declaration"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _sqlDatabaseConnectionString = "";
        private Int64 _nPatientID = 0;
        private Int64 _nClinicID = 1;
        private Int64 _neobPaymentID = 0;
        string _strMessageBoxCaption = string.Empty;
        private string _strCloseDate = "";
        private Boolean _bPaymentIsVoid = false;

        #endregion

        #region Constructor

        public frmViewPatientPayment(string databaseconnectionstring, Int64 patientid, Int64 clinicid, Int64 neobPaymentID)
        {
            InitializeComponent();
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _strMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _strMessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _strMessageBoxCaption = "gloPM"; ; }

            #endregion
            _sqlDatabaseConnectionString = databaseconnectionstring;
            _nPatientID = patientid;
            _nClinicID = clinicid;
            _neobPaymentID = neobPaymentID;
        }

        #endregion

        private void frmViewPatientPayment_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1PaymentDetail, false);
            FillPrintReceipt(tls_btnReceipt);
            fillPatientPaymentHeader();
            c1PaymentDetail.Focus();
            
        }

        public void fillPatientPaymentHeader()
        {
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsPatFinView = new DataSet();
            try
            {
                //oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@nClinicID", this._nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentID", this._neobPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Patient_Financial_View_PatPmnt_Header", oParameters, out dsPatFinView);
                if (dsPatFinView.Tables[0].Rows.Count > 0)
                {
                    lblCloseDate.Text = (dsPatFinView.Tables[0].Rows[0]["nCloseDate"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["nCloseDate"].ToString());
                    lblPaymentTray.Text = (dsPatFinView.Tables[0].Rows[0]["sPaymentTrayDescription"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["sPaymentTrayDescription"].ToString());
                    lblPatient.Text = (dsPatFinView.Tables[0].Rows[0]["PatientName"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["PatientName"].ToString());
                    if (dsPatFinView.Tables[0].Rows[0]["PatientName"].ToString().Trim() != "" && dsPatFinView.Tables[0].Rows[0]["PatientName"].ToString().Length > 25)
                        C1SuperTooltip1.SetToolTip(lblPatient, dsPatFinView.Tables[0].Rows[0]["PatientName"].ToString());
                    else
                        C1SuperTooltip1.SetToolTip(lblPatient, "");//if (dsPatFinView.Tables[0].Rows[0]["nPayMode"] != null)
                    
                    //{
                    //int _mode = Convert.ToInt16(dsPatFinView.Tables[0].Rows[0]["nPayMode"]);
                    //if (_mode.Equals(PaymentMode.Check.GetHashCode()))
                    //{
                    //    lblCheckNo.Text = "Check";
                    //}
                    //else if (_mode.Equals(PaymentMode.MoneyOrder.GetHashCode()))
                    //{
                    //    lblCheckNo.Text = "Money Order";
                    //}
                    //else if (_mode.Equals(PaymentMode.EFT.GetHashCode()))
                    //{
                    //    lblCheckNo.Text = "EFT";
                    //}

                    if (dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString().ToUpper() == "Cash".ToUpper())
                    {
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckDate.Text="Date :";
                    }
                    else if (dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString().ToUpper() == "Check".ToUpper())
                    {
                        lblCheckNo.Text = "Check# :";
                        lblCheckDate.Text = "Check Date :";
                    }
                    else if (dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString().ToUpper() == "MoneyOrder".ToUpper())
                    {
                        lblCheckNo.Text = "MO# :";
                        lblCheckDate.Text = "MO Date :";
                    }
                    else if (dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString().ToUpper() == "CreditCard".ToUpper())
                    {
                        lblCheckNo.Text = "Card# :";
                        lblCheckDate.Text = "Date :";
                    }
                    else if (dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString().ToUpper() == "EFT".ToUpper())
                    {
                        lblCheckNo.Text = "EFT# :";
                        lblCheckDate.Text = "EFT Date :";
                    }
                    lblPmntType.Text = (dsPatFinView.Tables[0].Rows[0]["nPayModeType"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["nPayModeType"].ToString());
                    lblCheckNum.Text = (dsPatFinView.Tables[0].Rows[0]["sCheckNumber"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["sCheckNumber"].ToString().Replace("&", "&&"));

                    if (dsPatFinView.Tables[0].Rows[0]["sCheckNumber"].ToString().Trim() != "" && dsPatFinView.Tables[0].Rows[0]["sCheckNumber"].ToString().Length > 25)
                        C1SuperTooltip1.SetToolTip(lblCheckNum, dsPatFinView.Tables[0].Rows[0]["sCheckNumber"].ToString());
                    else
                        C1SuperTooltip1.SetToolTip(lblCheckNum, "");//if (dsPatFinView.Tables[0].Rows[0]["nPayMode"] != null)
                    
                    lblShowCheckDate.Text = (dsPatFinView.Tables[0].Rows[0]["nCheckDate"] == DBNull.Value ? "" :(dsPatFinView.Tables[0].Rows[0]["nCheckDate"].ToString() == "0" ? "": dsPatFinView.Tables[0].Rows[0]["nCheckDate"].ToString()));
                    lblAmount.Text = "$ " + (dsPatFinView.Tables[0].Rows[0]["nCheckAmount"] == DBNull.Value ? "0.00" : dsPatFinView.Tables[0].Rows[0]["nCheckAmount"].ToString());
                    lblAvailableReserve.Text = "$ " + (dsPatFinView.Tables[0].Rows[0]["AvailableReserve"] == DBNull.Value ? "0.00" : dsPatFinView.Tables[0].Rows[0]["AvailableReserve"].ToString());
                    lblRemaining.Text = "$ " + (dsPatFinView.Tables[0].Rows[0]["paymentRemaining"] == DBNull.Value ? "0.00" : dsPatFinView.Tables[0].Rows[0]["paymentRemaining"].ToString());
                   
                    txtPayMstNotes.Text = (dsPatFinView.Tables[0].Rows[0]["sNoteDescription"] == DBNull.Value ? "" : dsPatFinView.Tables[0].Rows[0]["sNoteDescription"].ToString());
                    txtPayMstNotes.ReadOnly = true;
                    txtPayMstNotes.BackColor = Color.White;
                    if (dsPatFinView.Tables[0].Rows[0]["sPaymentTrayDescription"].ToString().Trim() != "" && dsPatFinView.Tables[0].Rows[0]["sPaymentTrayDescription"].ToString().Length > 40)
                        C1SuperTooltip1.SetToolTip(lblPaymentTray, dsPatFinView.Tables[0].Rows[0]["sPaymentTrayDescription"].ToString());
                    else
                        C1SuperTooltip1.SetToolTip(lblPaymentTray, "");

                    #region " Alert message for voided check "

                    if (Convert.ToBoolean(dsPatFinView.Tables[0].Rows[0]["bIsPaymentVoid"]) == true)
                    {
                        lblAlertMessage.Visible = true;
                        lblAlertMessage.Text = "Voided [" + Convert.ToString(dsPatFinView.Tables[0].Rows[0]["sUserName"].ToString()) + "] on " + Convert.ToString(dsPatFinView.Tables[0].Rows[0]["PaymentVoidCloseDate"].ToString());
                        if (lblAlertMessage.Text.Length>38)
                            C1SuperTooltip1.SetToolTip(lblAlertMessage, "Voided [" + Convert.ToString(dsPatFinView.Tables[0].Rows[0]["sUserName"].ToString()) + "] on " + Convert.ToString(dsPatFinView.Tables[0].Rows[0]["PaymentVoidCloseDate"].ToString()));
                        else
                            C1SuperTooltip1.SetToolTip(lblAlertMessage, "");
                        //ts_VoidPayment.Enabled = false;
                        _bPaymentIsVoid = true;
                    }
                    else
                    {
                        lblAlertMessage.Visible = false;
                        lblAlertMessage.Text = "";
                        _bPaymentIsVoid = false;
                        //ts_VoidPayment.Enabled = true;
                    }
                    #endregion
                }
                fillPatientPaymentDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private void fillPatientPaymentDetails()
        {
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsPatFinView = new DataSet();
            try
            {
                oParameters.Add("@nEOBPaymentID", _neobPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Patient_Financial_View_PatPmt_History", oParameters, out dsPatFinView);
                if (dsPatFinView.Tables[0].Rows.Count > 0)
                {
                    dsPatFinView.Tables[0].TableName = "dtPatPmtHistory";
                    c1PaymentDetail.DataMember = "dtPatPmtHistory";
                    c1PaymentDetail.DataSource = dsPatFinView;
                }
                else
                {
                    dsPatFinView.Tables[0].TableName = "dtPatPmtHistory";
                    c1PaymentDetail.DataMember = "dtPatPmtHistory";
                    c1PaymentDetail.DataSource = dsPatFinView;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnDefaultReceipt_Click(object sender, EventArgs e)
        {

                    if (_neobPaymentID > 0)
                    {
                        if (_bPaymentIsVoid == false)
                        {
                            if (tls_btnDefaultReceipt.Tag != null)
                            {
                                if (tls_btnDefaultReceipt.Tag.ToString().Trim().Length > 0)
                                {
                                  //  ToolStripMenuItem oTemplateItem = null;
                                    //oTemplateItem = (ToolStripMenuItem)sender;
                                    PrintReceipt(_neobPaymentID, Convert.ToInt64(Convert.ToString(tls_btnDefaultReceipt.Tag)));
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Payment is already voided. Receipt cannot be generated. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }
         }
       

    private void PrintReceipt(Int64 PaymentId, Int64 TemplateID)
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_sqlDatabaseConnectionString);
            //DataTable dtAssociation = null;
            Int64 _TemplateID = 0;
            Int64 _PayId = 0;
            try
            {
                #region " Print Receipt using Payment ID "

                _PayId = PaymentId;

                if (_PayId > 0)
                {
                    if (TemplateID > 0)
                    {
                        _TemplateID = TemplateID;// Convert.ToInt64(cmbPayReceipt.SelectedValue);

                        ogloTemplate.TemplateID = _TemplateID;
                        ogloTemplate.PrimeryID = _PayId;
                        //ogloTemplate.TemplateName = "";
                        ogloTemplate.PatientID = _nPatientID;
                        ogloTemplate.ClinicID = _nClinicID;
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_sqlDatabaseConnectionString, ogloTemplate);
                        frm.Show();
                        frm.WindowState = FormWindowState.Maximized;
                  
                    }
                }

                #endregion " Print Receipt using Payment ID "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { }
        }

    private void FillPrintReceipt(ToolStripDropDownButton tsbCats)
    {
        gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_sqlDatabaseConnectionString);
        DataTable dtCategories = new DataTable();
        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        DataTable dtTemplates = new DataTable();
      //  String CategoryName = "";

       // tls_btnDefaultReceipt.DropDownItems.Clear();

        try
        {
            oDB.Connect(false);
            tls_btnDefaultReceipt.Visible = false;
            tls_btnReceipt.Visible = false;
            tls_btnDefaultReceipt.Tag = null;

            dtTemplates = ogloTemplate.GetAssociation(gloOffice.AssociationCategories.PatientReceipt);

            if (dtTemplates != null && dtTemplates.Rows.Count > 0)
            {
                for (int j = 0; j < dtTemplates.Rows.Count; j++)
                {
                    ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                    oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                    oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                    oTemplateItem.ForeColor = Color.FromArgb(31, 73, 125);
                    oTemplateItem.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    oTemplateItem.Image = global::gloBilling.Properties.Resources.Bullet06;
                    oTemplateItem.ImageAlign = ContentAlignment.MiddleLeft;
                    oTemplateItem.ImageScaling = ToolStripItemImageScaling.None;
                    tsbCats.DropDownItems.Add(oTemplateItem);
                    oTemplateItem.Click += new EventHandler(oTemplateItem_Click);

                    if (dtTemplates.Rows[j]["bIsDefault"] != null && Convert.ToBoolean(dtTemplates.Rows[j]["bIsDefault"]) == true)
                    {
                        tls_btnDefaultReceipt.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                        tls_btnDefaultReceipt.Visible = true;
                    }
                }
            }

            if (dtTemplates != null) { dtTemplates.Dispose(); }
            if (tsbCats.DropDownItems.Count > 0) { tsbCats.Visible = true; }
        }
        catch //(Exception ex)
        {
            //MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        finally
        {
            if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
        }
    }

    void oTemplateItem_Click(object sender, EventArgs e)
    {

        if (_bPaymentIsVoid == false)
        {
            ToolStripMenuItem oTemplateItem = null;
            oTemplateItem = (ToolStripMenuItem)sender;
            if (oTemplateItem != null && oTemplateItem.Tag != null && oTemplateItem.Tag.ToString().Trim().Length > 0)
            {
                PrintReceipt(_neobPaymentID, Convert.ToInt64(Convert.ToString(oTemplateItem.Tag)));
            }
            oTemplateItem = null;
        }
        else
        {
            MessageBox.Show("Payment is already voided. Receipt cannot be generated. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
          
        }
    }

    private void ts_VoidPayment_Click(object sender, EventArgs e)
    {
        DialogResult dlgRst = DialogResult.None;
        gloEOBPaymentPatient ogloPaymentPatient = new gloEOBPaymentPatient(_sqlDatabaseConnectionString);
        int iVoidCloseDate = 0;
        string strVoidTrayName = "";
        Int64 nVoidTrayId = 0;
        string strVoidTrayCode = "";
        Int64 nRetVal = 0;
        string strVoidNotes = "";
        //bool blnIsVoid = false;
        bool bIsRefunded = false;
        try
        {
            if (_neobPaymentID != 0)
            {
                //_nEOBPaymentID = Convert.ToInt64(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index).ToString());
                // ********Check for voided claim************
                bIsRefunded = ogloPaymentPatient.IsRefunded(_neobPaymentID);
                if (bIsRefunded)
                {
                    MessageBox.Show("Payment has been refunded so it may not be voided. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (lblAlertMessage.Visible == true)
                {
                    MessageBox.Show("Payment is already voided. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //blnIsVoid = Convert.ToBoolean(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["bISVoid"].Index));
                //if (blnIsVoid)
                //{
                //    MessageBox.Show("Payment is already voided. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //*****************End************************
                //_strCloseDate = c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nCloseDate"].Index).ToString();
                _strCloseDate = lblCloseDate.Text;
                dlgRst = MessageBox.Show("Do you want to void the payment? ", _strMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                try
                {
                    if (dlgRst == DialogResult.Yes)
                    {
                        frmVoidPayment ofrmVoid = new frmVoidPayment(_neobPaymentID);
                        ofrmVoid.ShowDialog(this);
                        if (ofrmVoid.oDialogResult)
                        {
                            iVoidCloseDate = ofrmVoid.VoidCloseDate;
                            strVoidTrayName = ofrmVoid.VoidTrayName;
                            nVoidTrayId = ofrmVoid.VoidTrayID;
                            strVoidTrayCode = ofrmVoid.VoidTrayCode;
                            strVoidNotes = ofrmVoid.VoidNotes;

                            nRetVal = ogloPaymentPatient.VoidPatientPayment(_neobPaymentID, _nPatientID, "", _strCloseDate, strVoidNotes, iVoidCloseDate, nVoidTrayId, strVoidTrayCode, strVoidTrayName);
                           
                            //By Debasish Das BUG ID 3360.
                            Cls_GlobalSettings.IsPaymentVoided = true;
                            //**
                        }
                        ofrmVoid.Dispose();
                    }
                    fillPatientPaymentHeader();
                    //oPatientControl.FillDetails(_nPatientID, gloPatientStripControl.FormName.Billing, 0, false);
                    //tbPatientFinancial_SelectedIndexChanged(sender, e);
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                }
            }
            else
            {
                MessageBox.Show("No existing payment found for void. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception EX)
        {
            gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
        }
    }

    private void c1PaymentDetail_MouseMove(object sender, MouseEventArgs e)
    {
        HitTestInfo hitInfo = this.c1PaymentDetail.HitTest(e.X, e.Y);
        if (hitInfo.Row > 0)
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        else
            C1SuperTooltip1.SetToolTip(c1PaymentDetail, "");
    }
   

    }
        
    }

