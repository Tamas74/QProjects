using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatient;

using pdftron;
using pdftron.PDF;
using pdftron.SDF;
using pdftron.Filters;
using pdftron.Common;
using System.IO;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Data.SqlClient;
using gloStripControl;
using gloBilling.WC_Forms;
using System.Text.RegularExpressions;
using pdftron.PDF.Annots;

namespace gloBilling
{
    public partial class frmWorkerCompFormViewer_C4Auth : Form
    {
        private PDFViewCtrl _pdfview;
        private PDFDoc _pdfdoc;
        public long PatientId;
        public long TransactionID;
        public long TransactionMasterID;
        public long ClinicId;
        public long VisitId;
        
        public bool flgReadOnly = false;

        public C4Authorization objC4Auth;
        clsWorkerCompData ObjWorkerComp=null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        string sDBConnectionString = null;
        SqlConnection objConn = null;
        SqlCommand _sqlcommandMST = new SqlCommand();
        SqlCommand _sqlcommandDTL = new SqlCommand();

        SqlParameter oParam = null;

        SqlDataAdapter daWorkersCompFormsMST = null;
        SqlDataAdapter daWorkersCompFormsDTL = null;

        DataSet dsWorkersCompForms = null;
        Int64 iFormID = 0;
        DataTable dtProperties = null;

        private string _messageBoxCaption = "";
        private Int64 _UserID = 0;

        object objUserName = null;
        object objMachineName = null;
        string sUserName = null;
       // gloPatientStripControl.gloClaimSearchControl oPatientStripControl = null;

        public string ClaimNo { get; set; }

        public frmWorkerCompFormViewer_C4Auth(Int64 patientid, Int64 transactionID, Int64 transactionmasterID, string _databaseconnectionstring, Int64 formID)
        {
            InitializeComponent();
            Boolean bIsNewForm = true;
            DataSet dsInfo = null;

            try
            {
                sDBConnectionString = _databaseconnectionstring;
                objC4Auth = new C4Authorization();


                #region " Retrieve ClinicID from AppSettings "

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { ClinicId = System.Convert.ToInt64(appSettings["ClinicID"]); }
                    else { ClinicId = 1; }
                }
                else
                { ClinicId = 1; }

                #endregion

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption =System.Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "";
                    }
                }
                else
                { _messageBoxCaption = ""; }

                #endregion

                #region " Retrieve UserID from appSettings "

                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    {
                        _UserID =System.Convert.ToInt64(appSettings["UserID"]);
                    }
                }
                else
                {
                    _UserID = 0;
                }

                #endregion
                if (ObjWorkerComp != null)
                {
                    ObjWorkerComp.Dispose();
                }

                ObjWorkerComp = new clsWorkerCompData(_databaseconnectionstring);

                sUserName = appSettings["UserName"];
                iFormID = formID;

                if (iFormID > 0)
                {
                    ObjWorkerComp.CheckLockStatusForWorkerCompForm(iFormID, true, sUserName, Environment.MachineName, ref objUserName, ref objMachineName);

                    if (!string.IsNullOrEmpty(System.Convert.ToString(objUserName)) || !string.IsNullOrEmpty(System.Convert.ToString(objMachineName)))
                    {
                        if (string.Compare(System.Convert.ToString(objMachineName), Environment.MachineName, true) == 0 && string.Compare(System.Convert.ToString(objUserName), sUserName, true) == 0)
                        {
                            MessageBox.Show("This Worker's Comp Form is opened for modify from other application by you on this machine." + Environment.NewLine + " You cannot modify it right now from this application.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("This Worker's Comp Form is being modified by '" + System.Convert.ToString(objUserName) + "' on '" + System.Convert.ToString(objMachineName) + "'." + Environment.NewLine + " You cannot modify it now.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        flgReadOnly = true;
                    }
                }
                
                
                PatientId = patientid;
                TransactionID = transactionID;
                TransactionMasterID = transactionmasterID;

                //dtProperties = new DataTable();
                if (dtProperties != null)
                {
                    dtProperties.Dispose();
                }
                dtProperties = GetAllPropertiesInfo(objC4Auth);
                if (objConn != null)
                {
                    if (objConn.State != ConnectionState.Closed)
                    {
                        objConn.Close();
                    }

                    objConn.Dispose();
                }
                objConn = new SqlConnection(_databaseconnectionstring);
                objConn.Open();

                dsWorkersCompForms = new DataSet("WorkersCompForms");

                _sqlcommandMST.CommandType = CommandType.StoredProcedure;
                _sqlcommandMST.CommandText = "gsp_GetDataForWorkersCompFormMST";
                //_sqlcommandMST.CommandText = "SELECT TOP 1 FormId, PatientId, ExamId, ClaimTransactionMasterID, ClaimTransactionID, FormType, LastModifiedBy, LastModified, CreatedBy, CreatedOn FROM WorkersCompForms_MST WHERE PatientId = " + PatientId + " AND ClaimTransactionMasterID = " + TransactionMasterID + " AND ClaimTransactionID = " + TransactionID + " AND FormId = " + formID + " AND FormType = " + (int)WorkersCompFormTypes.MG2 + " ORDER BY LastModified DESC";
                _sqlcommandMST.Connection = objConn;
                _sqlcommandMST.CommandTimeout = 0;

                oParam = new SqlParameter();
                oParam.ParameterName = "@PatientId";
                oParam.Value = PatientId;
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;

                oParam = new SqlParameter();
                oParam.ParameterName = "@ClaimTransactionMasterID";
                oParam.Value = TransactionMasterID;
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;

                oParam = new SqlParameter();
                oParam.ParameterName = "@ClaimTransactionID";
                oParam.Value = TransactionID;
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;

                oParam = new SqlParameter();
                oParam.ParameterName = "@FormID";
                oParam.Value = formID;
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;

                oParam = new SqlParameter();
                oParam.ParameterName = "@FormType";
                oParam.Value = (int)WorkersCompFormTypes.C4Auth;
                oParam.DbType = DbType.Int32;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;

                daWorkersCompFormsMST = new SqlDataAdapter(_sqlcommandMST);

                daWorkersCompFormsMST.FillSchema(dsWorkersCompForms, SchemaType.Source, "WorkersCompForms_MST");
                daWorkersCompFormsMST.Fill(dsWorkersCompForms, "WorkersCompForms_MST");

                if (dsWorkersCompForms != null && dsWorkersCompForms.Tables.Count > 0)
                {
                    if (dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Count > 0)
                    {
                        iFormID = System.Convert.ToInt64(dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["FormId"]);
                    }
                }

                _sqlcommandDTL.CommandType = CommandType.StoredProcedure;
                _sqlcommandDTL.CommandText = "gsp_GetDataForWorkersCompFormDTL";
                //_sqlcommandDTL.CommandText = "SELECT Id, FormId, FieldName, FieldValue FROM WorkersCompForms_DTL WHERE FormId = " + iFormID;
                _sqlcommandDTL.Connection = objConn;
                _sqlcommandDTL.CommandTimeout = 0;

                oParam = new SqlParameter();
                oParam.ParameterName = "@FormID";
                oParam.Value = iFormID;
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandDTL.Parameters.Add(oParam);
                oParam = null;
                if (daWorkersCompFormsDTL != null)
                {
                    daWorkersCompFormsDTL.Dispose();
                }
                daWorkersCompFormsDTL = new SqlDataAdapter(_sqlcommandDTL);

                daWorkersCompFormsDTL.FillSchema(dsWorkersCompForms, SchemaType.Source, "WorkersCompForms_DTL");
                daWorkersCompFormsDTL.Fill(dsWorkersCompForms, "WorkersCompForms_DTL");

                objConn.Close();

                if (dsWorkersCompForms != null && dsWorkersCompForms.Tables.Count > 0)
                {
                    if (dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Count > 0 && dsWorkersCompForms.Tables["WorkersCompForms_DTL"].Rows.Count > 0)
                    {
                         SetDataToC4AuthObject(dsWorkersCompForms.Tables["WorkersCompForms_DTL"], ref objC4Auth);
                        bIsNewForm = false;
                    }
                }

                if (bIsNewForm)
                {
                    

                    dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                      ObjWorkerComp.SetC4AuthorizationPrdefinedInfo(dsInfo, ref objC4Auth, true, true);
                }


                _pdfview = new PDFViewCtrl();
                _pdfview.Location = new System.Drawing.Point(0, 0);
                _pdfview.Dock = System.Windows.Forms.DockStyle.Fill;
                ViewerPanel.Controls.Add(_pdfview);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dsInfo != null)
                {
                    dsInfo.Dispose();
                    dsInfo = null;
                }
            }
       
        }
         
        public void LoadForWorkerCompViewer()
        {
            try
            {
                lblClaimNo.Text = ClaimNo.ToString();

                //_pdfdoc = new PDFDoc(@"D:\SprintWork\8060\NYWC\NYWCFiles\c4AUTH_tab_ImageSign.pdf");
                _pdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign, global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign.Length);

                try
                {
                    _pdfdoc.InitSecurityHandler();
                }
                catch
                {
                }

                if (objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage != null && objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage.Length > 0)
                {
                    LoadPDFSign(ref _pdfdoc);
                }

                SetFormFieldsData(ref _pdfdoc);
                if (flgReadOnly)
                {
                    SetFieldsReadOnly(ref _pdfdoc);
                    tsb_SaveData.Enabled = false;
                    pnl_txtSearch.Enabled = false;
                    btnSearchPatientClaim.Enabled = false;
                }

                _pdfview.SetPageViewMode(PDFViewCtrl.PageViewMode.e_fit_page);
                _pdfview.BringToFront();
                _pdfview.SetCaching(true);
                _pdfview.SetDoc(_pdfdoc);
                _pdfview.EnableInteractiveForms(true);
                _pdfview.SetFocus();
                _pdfview.SetPagePresentationMode(PDFViewCtrl.PagePresentationMode.e_single_continuous);

                _pdfview.SetHighlightFields(false);

                _pdfview.TabStop = false;
                while (true)
                {
                    if (_pdfview.IsFinishedRendering())
                    {
                        // Console.WriteLine("Viewer says its finished load document");
                        break;
                    }

                    //  Console.WriteLine("Viewer says Wait I am not finished load document attempt # - " + loadcounter++);
                }
                // TODO : Enable this and write code for the data setting for C4.
                tsb_GetData.Visible = false;
                tsb_GetData.Enabled = false;
                //LoadPatientStripControl();
                lblPatientName.Text = objC4Auth.PatientName;

                
            }
            catch (PDFNetException ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmWorkerCompFormViewer_C4Auth_Load(object sender, EventArgs e)
        {
            ObjWorkerComp.ConnectToPDFTron();
            LoadForWorkerCompViewer();
            txtClaimNoSearch.Focus();
            txtClaimNoSearch.TabStop = false;
            btnSearchPatientClaim.TabStop = false;
            txtClearSearch.TabStop = false;
            this.Text = "Worker Comp Form Viewer : C-4 AUTH";

            if (iFormID <= 0)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Add, "Attempted to create New Form of type C4 AUTH.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            }
            else
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.View, "C4 AUTH Form opened for view.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            }
        }

        private void SetFormFieldsDataClaimOnly(ref PDFDoc FormPdfDoc)
        {
            //PropertyInfo[] propinfo = null;

            try
            {

                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("InsuranceCarrierName"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("InsuranceCarrierFullAddress"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("WCBCaseNo"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("CarrierCaseNo"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("DateOfInjury"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("AttendingDoctorsName"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("AttendingDoctorsFullAddress"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("ProviderAuthorizationNo"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("ProviderPhoneNo"), objC4Auth);
                SetData(FormPdfDoc, objC4Auth.GetType().GetProperty("ProviderFaxNo"), objC4Auth);

                SetData(FormPdfDoc, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.GetType().GetProperty("ProviderSignImage"), objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity);
                SetData(FormPdfDoc, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.GetType().GetProperty("ProviderSignatureDate"), objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity);
                
                // Regenerate field appearances.
                try
                { FormPdfDoc.RefreshFieldAppearances(); }
                catch (Exception) { }

                _pdfview.Update();
                _pdfview.SetHighlightFields(false);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //propinfo = null;
            }


        }

        private void SetFormFieldsData(ref PDFDoc FormPdfDoc)
        {
            PropertyInfo[] propinfo = null;

            try
            {
                //while (true)
                //{
                //    if (_pdfview.IsFinishedRendering())
                //    {
                //        break;
                //    }
                //}

                //_pdfdoc = _pdfview.GetDoc();

                //_pdfdoc.InitSecurityHandler();


                propinfo = objC4Auth.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest);
                    }
                }

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_Footer.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(FormPdfDoc, prop, objC4Auth.C4_Auth_Footer);
                    }
                }

                // Regenerate field appearances.
                try
                {
                    //MessageBox.Show(_pdfview.Font.Name);   

                    FormPdfDoc.RefreshFieldAppearances();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                _pdfview.Update();
                _pdfview.SetHighlightFields(false);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                propinfo = null;
            }


        }

        private bool SetData(PDFDoc oDoc, PropertyInfo prop, object objClass)
        {
            string sField = null;
            object oValue = null;
            Field fld = null;

            try
            {
                if (oDoc == null || prop == null || objClass == null) { return false; }

                sField = objC4Auth.GetAttributeDisplayName(prop);
                oValue = prop.GetValue(objClass, null);

                if (string.IsNullOrEmpty(sField))
                { return false; }

                try
                {
                    fld = oDoc.GetField(sField);
                }
                catch (Exception)
                {
                    return false;
                }
                
                if (fld != null)
                {
                    if (string.Compare(prop.PropertyType.FullName, "System.Boolean", true) == 0)
                    {
                        fld.SetValue(System.Convert.ToBoolean(oValue == null ? false : oValue));
                    }
                    else
                    {
                        fld.SetValue(System.Convert.ToString(oValue == null ? "" : oValue));
                    }

                    try
                    {
                        //fld.RefreshAppearance();
                    }
                    catch (Exception)
                    {
                       
                    }
                    
                    
                    return true;
                }
                else
                {
                    //Console.Write("Filed not found : " + sField);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                sField = null;
                oValue = null;
                fld = null;
            }
        }

        private void tsp_Close_Click(object sender, EventArgs e)
        {
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Close, "C4 AUTH Form Closed.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            this.Close();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (iFormID <= 0)
                {
                    MessageBox.Show("C-4 AUTH Form you are trying to print is not yet saved." + System.Environment.NewLine + "Form will be saved automatically before printing.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tsb_SaveData_Click((object)(tsb_SaveData), e: e);
                }

                tsb_Print.Enabled = false;

                Print();

                //if (iFormID <= 0)
                //{
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Print, "C4 AUTH Form printed without saving in system.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                //}
                //else
                //{
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Print, "C4 AUTH Form printed from system.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                //}

                //_pdfview.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tsb_Print.Enabled = true;
            }
        }
      //  pdftron.PDF.PDFDraw pdfdraw = null;
        //PDFDoc doc = null;
 
        //private void Print()
        //{
        //    try
        //    {
        //        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
        //        {
        //            oDialog.ConnectionString = sDBConnectionString;
        //            oDialog.TopMost = true;
        //            oDialog.AllowSomePages = true;
        //            oDialog.ModuleName = "PrintWorkersCompForms";

        //            oDialog.RegistryModuleName = "NYWCForms";


        //            //System.Drawing.Printing.StandardPrintController oPrintController = new System.Drawing.Printing.StandardPrintController();
        //            //printDocument1.PrintController = oPrintController;

        //            ////if (gloEDocV3Admin.blnUseDefaultPrinterDialog == false)
        //            ////{

        //            //PrintDialog1 = new PrintDialog();
        //            if (oDialog != null)
        //            {
        //                //PrintDialog1.AllowCurrentPage = true;
        //                //PrintDialog1.AllowSelection = true;
        //                doc = _pdfview.GetDoc();
        //                doc.Lock();
        //                int maxPage = doc.GetPageCount();


        //                oDialog.PrinterSettings = printDocument1.PrinterSettings;
        //                oDialog.PrinterSettings.Copies = 1;
        //                oDialog.ShowPrinterProfileDialog = true;

        //                oDialog.AllowSomePages = true;
        //                oDialog.PrinterSettings.ToPage = maxPage;
        //                oDialog.PrinterSettings.FromPage = 1;
        //                oDialog.PrinterSettings.MaximumPage = maxPage;
        //                oDialog.PrinterSettings.MinimumPage = 1;

        //                PrintDialog1.AllowSomePages = true;
        //                PrintDialog1.PrinterSettings.ToPage = maxPage;
        //                PrintDialog1.PrinterSettings.FromPage = 1;
        //                PrintDialog1.PrinterSettings.MaximumPage = maxPage;
        //                PrintDialog1.PrinterSettings.MinimumPage = 1;

        //                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    //if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        //                    //{
        //                    //  myPrinterSetting = PrintDialog1.PrinterSettings;
        //                    ////try
        //                    ////{
        //                    printDocument1.PrinterSettings = oDialog.PrinterSettings;
        //                    ////}
        //                    ////catch
        //                    ////{
        //                    ////    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
        //                    ////}

        //                    //Lines Added By Dipak 20090909 
        //                    //oDialogResultIsOK = true;
        //                    //PopulatePrinterPageArray(maxPage, printDocument1.PrinterSettings);
        //                    //pdfdraw = new pdftron.PDF.PDFDraw();
        //                    //if (rect != null)
        //                    //{
        //                    //    rect.Dispose();
        //                    //    rect = null;
        //                    //}
        //                    ////printDocument1.DocumentName = strFileName;
        //                    //printDocument1.Print();
        //                    //if (pdfdraw != null)
        //                    //{
        //                    //    pdfdraw.Dispose();
        //                    //    pdfdraw = null;
        //                    //}
        //                    //if (rect != null)
        //                    //{
        //                    //    rect.Dispose();
        //                    //    rect = null;
        //                    //}

        //                    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController
        //                        (_pdfview.GetDoc(), _pdfview.GetDoc().GetFileName(), oDialog.PrinterSettings,
        //                        oDialog.CustomPrinterExtendedSettings);
        //                    //ogloPrintProgressController.AutoLandscape = false;
        //                    ogloPrintProgressController.ShowProgress(this);
        //                    //if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
        //                    //{
        //                    //    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
        //                    //    {
        //                    //        ogloPrintProgressController.Show();
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        ogloPrintProgressController.Show();
        //                    //    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    ogloPrintProgressController.TopMost = true;
        //                    //    ogloPrintProgressController.ShowInTaskbar = false;

        //                    //    ogloPrintProgressController.ShowDialog(this);
        //                    //    if (ogloPrintProgressController != null)
        //                    //    {
        //                    //        ogloPrintProgressController.Dispose();
        //                    //    }
        //                    //    ogloPrintProgressController = null;
        //                    //}


        //                }//if
        //                doc.Unlock();
        //                //   PrintDialog1.Dispose();
        //                //   PrintDialog1 = null;
        //            }
        //            else
        //            {
        //                string _ErrorMessage = "Error in Showing Print Dialog";

        //                if (_ErrorMessage.Trim() != "")
        //                {
        //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //                    _MessageString = "";
        //                }


        //                MessageBox.Show(_ErrorMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            ////}
        //            ////else
        //            ////{
        //            ////    // printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
        //            ////    if (myPrinterSetting == null)
        //            ////    {
        //            ////        myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
        //            ////    }

        //            ////    if (myPrinterSetting != null)
        //            ////    {
        //            ////        try
        //            ////        {
        //            ////            printDocument1.PrinterSettings = myPrinterSetting;
        //            ////        }
        //            ////        catch
        //            ////        {
        //            ////            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
        //            ////        }
        //            ////    }


        //            ////oDialogResultIsOK = true;
        //        }
        //      //  pdfdraw = new pdftron.PDF.PDFDraw();
        //        //if (rect != null)
        //        //{
        //        //    rect.Dispose();
        //        //    rect = null;
        //        //}
        //        ////  printDocument1.DocumentName = strFileName;
        //        //printDocument1.Print();
        //        //   myPrinterSetting = null;
        //        //if (pdfdraw != null)
        //        //{
        //        //    pdfdraw.Dispose();
        //        //    pdfdraw = null;
        //        //}
        //        //if (rect != null)
        //        //{
        //        //    rect.Dispose();
        //        //    rect = null;
        //        //}


        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;
        //    }
        //    finally
        //    {
        //        //if (pdfdraw != null)
        //        //{
        //        //    pdfdraw.Dispose();
        //        //    pdfdraw = null;
        //        //}
        //        //if (rect != null)
        //        //{
        //        //    rect.Dispose();
        //        //    rect = null;
        //        //}
        //        //if (PrintDialog1 != null)
        //        //{
        //        //   PrintDialog1.Dispose();
        //        //   PrintDialog1 = null;
        //        //}
        //        PageArray.Clear();
        //    }

        //}
        //List<int> PageArray = new List<int> { };
        //private void PopulatePrinterPageArray(int maxPage, System.Drawing.Printing.PrinterSettings printerSettings)
        //{

        //    PageArray.Clear();
        //    bool collate = printerSettings.Collate;
        //    int copies = printerSettings.Copies;
        //    int from = printerSettings.FromPage;
        //    int to = printerSettings.ToPage;
        //    if (from < 1) from = 1;
        //    if (to > maxPage) to = maxPage;
        //    if (collate)
        //    {
        //        for (int iPage = from; iPage <= to; iPage++)
        //        {
        //            for (int icopies = 0; icopies < copies; icopies++)
        //            {
        //                PageArray.Add(iPage);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int icopies = 0; icopies < copies; icopies++)
        //        {
        //            for (int iPage = from; iPage <= to; iPage++)
        //            {
        //                PageArray.Add(iPage);
        //            }
        //        }

        //    }
        //}
        private void Print()
        {
            clsWorkerCompData.Print(_pdfdoc, sDBConnectionString, this);
            //PDFDoc doc = null;
            //try
            //{
            //    using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
            //    {
            //        oDialog.ConnectionString = sDBConnectionString;
            //        oDialog.TopMost = true;
            //        oDialog.AllowSomePages = true;
            //        oDialog.ModuleName = "PrintWorkersCompForms";

            //        oDialog.RegistryModuleName = "NYWCForms";


            //        //System.Drawing.Printing.StandardPrintController oPrintController = new System.Drawing.Printing.StandardPrintController();
            //        //printDocument1.PrintController = oPrintController;

            //        ////if (gloEDocV3Admin.blnUseDefaultPrinterDialog == false)
            //        ////{

            //        //PrintDialog1 = new PrintDialog();
            //        if (oDialog != null)
            //        {
            //            //PrintDialog1.AllowCurrentPage = true;
            //            //PrintDialog1.AllowSelection = true;
            //            doc = _pdfdoc; //_pdfview.GetDoc();
            //            doc.Lock();
            //            int maxPage = doc.GetPageCount();

            //            if (!gloGlobal.gloTSPrint.isCopyPrint)
            //            {
            //                oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
            //                oDialog.PrinterSettings.Copies = 1;
            //                oDialog.ShowPrinterProfileDialog = true;

            //                oDialog.AllowSomePages = true;
            //                oDialog.PrinterSettings.ToPage = maxPage;
            //                oDialog.PrinterSettings.FromPage = 1;
            //                oDialog.PrinterSettings.MaximumPage = maxPage;
            //                oDialog.PrinterSettings.MinimumPage = 1;
            //            }
            //            //PrintDialog1.AllowSomePages = true;
            //            //PrintDialog1.PrinterSettings.ToPage = maxPage;
            //            //PrintDialog1.PrinterSettings.FromPage = 1;
            //            //PrintDialog1.PrinterSettings.MaximumPage = maxPage;
            //            //PrintDialog1.PrinterSettings.MinimumPage = 1;

            //            if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //            {
            //                //if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //                //{
            //                //  myPrinterSetting = PrintDialog1.PrinterSettings;
            //                ////try
            //                ////{
            //                //printDocument1.PrinterSettings = oDialog.PrinterSettings;
            //                if (!gloGlobal.gloTSPrint.isCopyPrint)
            //                {
            //                    myPrinterSetting = oDialog.PrinterSettings;
            //                }
            //                ////}
            //                ////catch
            //                ////{
            //                ////    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
            //                ////}

            //                //Lines Added By Dipak 20090909 
            //                //oDialogResultIsOK = true;
            //                //PopulatePrinterPageArray(maxPage, printDocument1.PrinterSettings);
            //                //pdfdraw = new pdftron.PDF.PDFDraw();
            //                //if (rect != null)
            //                //{
            //                //    rect.Dispose();
            //                //    rect = null;
            //                //}
            //                ////printDocument1.DocumentName = strFileName;
            //                //printDocument1.Print();
            //                //if (pdfdraw != null)
            //                //{
            //                //    pdfdraw.Dispose();
            //                //    pdfdraw = null;
            //                //}
            //                //if (rect != null)
            //                //{
            //                //    rect.Dispose();
            //                //    rect = null;
            //                //}

            //                gloPrintDialog.gloPrintProgressController ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController
            //                    (doc, doc.GetFileName(), oDialog.PrinterSettings,
            //                    oDialog.CustomPrinterExtendedSettings);
            //                //ogloPrintProgressController.AutoLandscape = false;
            //                ogloPrintProgressController.ShowProgress(this);
            //                //if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
            //                //{
            //                //    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
            //                //    {
            //                //        ogloPrintProgressController.Show();
            //                //    }
            //                //    else
            //                //    {
            //                //        ogloPrintProgressController.Show();
            //                //    }
            //                //}
            //                //else
            //                //{
            //                //    ogloPrintProgressController.TopMost = true;
            //                //    ogloPrintProgressController.ShowInTaskbar = false;

            //                //    ogloPrintProgressController.ShowDialog(this);
            //                //    if (ogloPrintProgressController != null)
            //                //    {
            //                //        ogloPrintProgressController.Dispose();
            //                //    }
            //                //    ogloPrintProgressController = null;
            //                //}


            //            }//if
            //            doc.Unlock();
            //            //   PrintDialog1.Dispose();
            //            //   PrintDialog1 = null;
            //        }
            //        else
            //        {
            //            string _ErrorMessage = "Error in Showing Print Dialog";

            //            if (_ErrorMessage.Trim() != "")
            //            {
            //                string _MessageString = "Date Time : " + DateTime.Now.Ticks.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                _MessageString = "";
            //            }


            //            MessageBox.Show(_ErrorMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        ////}
            //        ////else
            //        ////{
            //        ////    // printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
            //        ////    if (myPrinterSetting == null)
            //        ////    {
            //        ////        myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
            //        ////    }

            //        ////    if (myPrinterSetting != null)
            //        ////    {
            //        ////        try
            //        ////        {
            //        ////            printDocument1.PrinterSettings = myPrinterSetting;
            //        ////        }
            //        ////        catch
            //        ////        {
            //        ////            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
            //        ////        }
            //        ////    }


            //        ////oDialogResultIsOK = true;
            //    }
            //    //   pdfdraw = new pdftron.PDF.PDFDraw();
            //    //if (rect != null)
            //    //{
            //    //    rect.Dispose();
            //    //    rect = null;
            //    //}
            //    ////  printDocument1.DocumentName = strFileName;
            //    //printDocument1.Print();
            //    //   myPrinterSetting = null;
            //    //if (pdfdraw != null)
            //    //{
            //    //    pdfdraw.Dispose();
            //    //    pdfdraw = null;
            //    //}
            //    //if (rect != null)
            //    //{
            //    //    rect.Dispose();
            //    //    rect = null;
            //    //}


            //}
            //catch (Exception ex)
            //{
            //    #region " Make Log Entry "

            //    string _ErrorMessage = ex.ToString();
            //    //Code added on 7rd October 2008 By - Sagar Ghodke
            //    //Make Log entry in DMSExceptionLog file for any exceptions found
            //    if (_ErrorMessage.Trim() != "")
            //    {
            //        string _MessageString = "Date Time : " + DateTime.Now.Ticks.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //        _MessageString = "";
            //    }

            //    //End Code add
            //    #endregion " Make Log Entry "

            //    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ex = null;
            //}
            //finally
            //{
            //    //if (pdfdraw != null)
            //    //{
            //    //    pdfdraw.Dispose();
            //    //    pdfdraw = null;
            //    //}
            //    //if (rect != null)
            //    //{
            //    //    rect.Dispose();
            //    //    rect = null;
            //    //}
            //    //if (PrintDialog1 != null)
            //    //{
            //    //   PrintDialog1.Dispose();
            //    //   PrintDialog1 = null;
            //    //}
            //    //PageArray.Clear();
            //}

        }
        //private static double MarginX = 0, MarginY = 0;
        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    //Graphics gr = e.Graphics;
        //    e.Graphics.PageUnit = GraphicsUnit.Inch;
        //    //Rectangle rectPage = e.PageBounds;
        //    // print without margins 
        //    // Rectangle rectPage = ev.MarginBounds ' print using margins 

        //    if (rect == null)
        //    {
        //        //// if (rbCustomDPI.Checked)
        //        //// {
        //        ////     float dpi = Math.Max(e.Graphics.DpiX, e.Graphics.DpiY);

        //        ////     if (dpi > (float)numCustomDPIValue.Value)
        //        ////     {
        //        ////         dpi = (float)numCustomDPIValue.Value;
        //        ////         //   gloAuditTrail.gloAuditTrail.PrintLog("Started Setting DPI "+dpi.ToString());
        //        ////         pdfdraw.SetDPI(dpi);
        //        ////         //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting DPI " + dpi.ToString());
        //        ////     }
        //        //// }
        //        float dpi = 200;
        //        pdfdraw.SetDPI(dpi);
        //        //double left;
        //        //double right;
        //        //double top;
        //        //double bottom;

        //        //left = rectPage.Left / 100;
        //        //right = rectPage.Right / 100;
        //        //top = rectPage.Top / 100;
        //        //bottom = rectPage.Bottom / 100;

        //        RectangleF myRectangle = e.PageSettings.PrintableArea;

        //        rect = new pdftron.PDF.Rect(((-myRectangle.Top) / 100) * 72, ((myRectangle.Bottom) / 100) * 72, ((myRectangle.Right) / 100) * 72, ((-myRectangle.Left) / 100) * 72);

        //        //double x1, x2, y1, y2;
        //        //thisRect.Get(ref x1, ref y1, ref x2, ref y2);
        //    }

        //    //PDFDoc doc = _pdfview.GetDoc();
        //    //doc.Lock();
        //    try
        //    {

        //        ////Int16 val = System.Convert.ToInt16(Math.Ceiling(100.00 / _nPagesCnt));
        //        ////int _cnt = val * (_printPageIndex + 2);
        //        ////if (_cnt > 100)
        //        ////{
        //        ////    pbDocument.Value = 100;
        //        ////}
        //        ////else
        //        ////{
        //        ////    pbDocument.Value = _cnt;
        //        ////}
        //        //   gloAuditTrail.gloAuditTrail.PrintLog("Started Drawing PDF" +_printPageIndex.ToString());


        //        //Rect thisRect = thisPage.GetBox(Page.Box.e_trim);
        //        //double x1, x2, y1, y2;
        //        //thisRect.Get(ref x1, ref y1, ref x2, ref y2);


        //        //Rect pageRect = thisPage.GetBox(Page.Box.e_media);
        //        pdfdraw.DrawInRect(doc.GetPage(PageArray[_printPageIndex]), e.Graphics, rect);
        //        //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Drawing PDF" + _printPageIndex.ToString());

        //        ////txtPrintStatus.AppendText("Printing Page " + oSourceDocSelectedPages[_printPageIndex].ToString());
        //        ////txtPrintStatus.AppendText(Environment.NewLine);

        //        ////pbDocument.Refresh();
        //        Application.DoEvents();

        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    //        doc.Unlock();

        //    if (_printPageIndex < PageArray.Count - 1)
        //    {
        //        e.HasMorePages = true;
        //        _printPageIndex = _printPageIndex + 1;
        //    }
        //    else
        //    {
        //        e.HasMorePages = false;
        //        _printPageIndex = 0;


        //    }

        //}
       // private int _printPageIndex = 0;
 //       pdftron.PDF.Rect rect = null;
        //private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //PDFDoc doc = oPDFDoc; //GetPDFDoc();

        //    ////if (oPDFDoc == null)
        //    ////{
        //    ////    MessageBox.Show("Error: Print document is not selected.");
        //    ////    return;
        //    ////}
        //    //     print_page_itr = oPDFDoc.GetPageIterator();// doc.GetPage(1);// PageBegin;
        //    if (rect != null)
        //    {
        //        rect.Dispose();
        //        rect = null;
        //    }

        //    pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //    ////if (rbCustomDPI.Checked)
        //    ////{

        //    ////    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting Builtin Rasterizer");
        //    ////    pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //    ////    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting Builtin Rasterizer");

        //    ////}
        //    ////else
        //    ////{
        //    ////    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting GDI Rasterizer");
        //    ////    pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
        //    ////    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting GDI Rasterizer");
        //    ////}

        //}
        private void SetDataToC4AuthObject(DataTable dtData, ref C4Authorization objC4Auth)
        {
            PropertyInfo prop = null;
            object oValue = null;

            try
            {
                foreach (DataRow drRow in dtData.Rows)
                {
                    if (string.IsNullOrEmpty(System.Convert.ToString(drRow["FieldName"])))
                    {
                        continue;
                    }

                    oValue = System.Convert.ToString(drRow["FieldValue"]);

                    prop = objC4Auth.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (prop != null && prop.PropertyType == System.Type.GetType("System.Byte[]"))
                        {
                            if (!string.IsNullOrEmpty(System.Convert.ToString(drRow["FieldValueImage"])))
                            {
                                oValue = (byte[])(drRow["FieldValueImage"]);
                            }
                        }

                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest, oValue, null);
                        }

                        continue;
                    }

                    prop = objC4Auth.C4_Auth_Footer.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC4Auth.C4_Auth_Footer, oValue, null);
                        }

                        continue;
                    }

                    prop = null;
                    oValue = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                prop = null;
                oValue = null;
            }

            return ;
        }

        private Boolean SetDataToDBAdapter()
        {
            DataRow drRow = null;
            Boolean bIsSuccess = false;
            Boolean bIsNewForm = false;
            DataTable dtUniqueIds = null;
            DataRow[] drRowDTL = null;

            try
            {

                if (dsWorkersCompForms.Tables["WorkersCompForms_MST"] == null || dsWorkersCompForms.Tables["WorkersCompForms_DTL"] == null) { return bIsSuccess; }

                if (dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Count <= 0)
                {
                    drRow = dsWorkersCompForms.Tables["WorkersCompForms_MST"].NewRow();

                    iFormID =ObjWorkerComp.GetUniqueID(sDBConnectionString,_messageBoxCaption);

                    drRow["FormId"] = iFormID;
                    drRow["PatientId"] = PatientId;
                    drRow["ExamId"] = 0;
                    drRow["ClaimTransactionMasterID"] = TransactionMasterID;
                    drRow["ClaimTransactionID"] = TransactionID;
                    drRow["FormType"] =(int)WorkersCompFormTypes.C4Auth;
                    drRow["LastModifiedBy"] = _UserID;
                    drRow["LastModified"] = DateTime.Now;
                    drRow["CreatedBy"] = _UserID;
                    drRow["CreatedOn"] = DateTime.Now;
                    drRow["sUserName"] = sUserName;
                    drRow["sMachineName"] = Environment.MachineName;

                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Add(drRow);
                    drRow = null;

                    bIsNewForm = true;
                }
                else
                {
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["ClaimTransactionMasterID"] = TransactionMasterID;
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["ClaimTransactionID"] = TransactionID;
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["LastModifiedBy"] = _UserID;
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["LastModified"] = DateTime.Now;
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["sUserName"] = sUserName;
                    dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows[0]["sMachineName"] = Environment.MachineName;
                }

                if (dsWorkersCompForms.Tables["WorkersCompForms_DTL"] != null)
                {
                    dtUniqueIds = null;

                    if (bIsNewForm)
                    {
                      //  dtUniqueIds = new DataTable();

                        dtUniqueIds = ObjWorkerComp.GetUniqueIDs(dtProperties.Rows.Count, sDBConnectionString, _messageBoxCaption);
                    }

                    drRowDTL = null;

                    for (Int32 i = 0; i < dtProperties.Rows.Count; i++)
                    {
                        drRowDTL = null;
                        drRowDTL = dsWorkersCompForms.Tables["WorkersCompForms_DTL"].Select("FieldName='" + dtProperties.Rows[i]["PropertyName"] + "'");

                        if (drRowDTL != null && drRowDTL.Length > 0)
                        {
                            drRowDTL[0]["FieldValue"] = dtProperties.Rows[i]["FormFieldValue"];
                            drRowDTL[0]["FieldValueImage"] = dtProperties.Rows[i]["FormFieldValueImage"];
                        }
                        else
                        {
                            drRow = null;
                            drRow = dsWorkersCompForms.Tables["WorkersCompForms_DTL"].NewRow();

                            Int64 iFieldId = 0;

                            if (bIsNewForm && dtUniqueIds.Rows.Count > 0)
                            {
                                iFieldId =System.Convert.ToInt64(dtUniqueIds.Rows[i][0]);
                            }
                            
                            if (iFieldId == 0 )
                            {
                                iFieldId = ObjWorkerComp.GetUniqueID(sDBConnectionString, _messageBoxCaption);
                            }

                            drRow["Id"] = iFieldId;
                            drRow["FormId"] = iFormID;
                            drRow["FieldName"] = dtProperties.Rows[i]["PropertyName"];
                            drRow["FieldValue"] = dtProperties.Rows[i]["FormFieldValue"];
                            drRow["FieldValueImage"] = dtProperties.Rows[i]["FormFieldValueImage"];

                            dsWorkersCompForms.Tables["WorkersCompForms_DTL"].Rows.Add(drRow);
                        }

                        drRowDTL = null;
                    }

                }
                bIsSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                drRow = null;
                drRowDTL = null;

                if (dtUniqueIds != null)
                {
                    dtUniqueIds.Dispose();
                    dtUniqueIds = null;
                }
            }

            return bIsSuccess;
        }

        private Boolean UpdateDataAdaptersToDatabase()
        {
            Boolean bIsSuccess = false;
            SqlCommandBuilder objCommandBuilder = null;

            try
            {
                objCommandBuilder = new SqlCommandBuilder(daWorkersCompFormsMST);
                daWorkersCompFormsMST.Update(dsWorkersCompForms, "WorkersCompForms_MST");
                objCommandBuilder.Dispose();
                objCommandBuilder = null;

                objCommandBuilder = new SqlCommandBuilder(daWorkersCompFormsDTL);
                daWorkersCompFormsDTL.Update(dsWorkersCompForms, "WorkersCompForms_DTL");
                objCommandBuilder.Dispose();
                objCommandBuilder = null;

                bIsSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (objCommandBuilder != null)
                {
                    objCommandBuilder.Dispose();
                    objCommandBuilder = null;
                }
            }
            return bIsSuccess;
        }

        private Boolean GetFormData(ref PDFDoc FormPdfDoc)
        {
            DataRow[] drRow = null;
            Boolean bIsSuccess = false;
            FieldIterator itr = null;
            Field field = null;

            try
            {
                //_pdfview.Focus();

                //_pdfdoc = _pdfview.GetDoc();

                //_pdfdoc.InitSecurityHandler();

                FormPdfDoc.Lock();

                for (itr = FormPdfDoc.GetFieldIterator(); itr.HasNext(); itr.Next())
                    {
                        field = itr.Current();
                        Field.Type type = field.GetType();

                        drRow = null;
                        drRow = dtProperties.Select("FormFieldName='" + field.GetName() + "'");

                        switch (type)
                        {
                            case Field.Type.e_button:
                                if (drRow != null && drRow.Length > 0)
                                {
                                    drRow[0]["FormFieldValueImage"] =ObjWorkerComp.GetProviderSignImage(field,ref FormPdfDoc,2,_messageBoxCaption);
                                }
                                //Console.WriteLine("Button" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());
                                break;
                            case Field.Type.e_radio:
                                //Console.WriteLine("Radio button" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString() + "  :  " + field.GetValueAsBool());
                                break;
                            case Field.Type.e_check:
                                //Console.WriteLine("Check box" + "  :  " + field.GetName());
                                //Console.WriteLine("Check box" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());

                                if (drRow != null && drRow.Length > 0)
                                {
                                    drRow[0]["FormFieldValue"] = field.GetValueAsBool();
                                }

                                break;
                            case Field.Type.e_text:
                                {
                                    //Console.WriteLine("Text" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());

                                    if (drRow != null && drRow.Length > 0)
                                    {
                                        drRow[0]["FormFieldValue"] = field.GetValueAsString();
                                    }
                                }
                                break;
                            case Field.Type.e_choice:
                                //Console.WriteLine("Choice" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());
                                break;
                            case Field.Type.e_signature:
                                //Console.WriteLine("Signature" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());
                                break;
                        }
                        field = null;
                    }

                    bIsSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                drRow = null;
                field = null;

                if (itr != null)
                {
                    itr.Dispose();
                    itr = null;
                }
                FormPdfDoc.Unlock();
            }
            return bIsSuccess;
        }
        private Boolean SetFormData(ref PDFDoc FreshPdfDoc)
        {
            DataRow[] drRow = null;
            Boolean bIsSuccess = false;
            FieldIterator itr = null;
            Field field = null;

            try
            {


                // FreshPdfDoc.InitSecurityHandler();

                for (itr = FreshPdfDoc.GetFieldIterator(); itr.HasNext(); itr.Next())
                {
                    field = itr.Current();
                    Field.Type type = field.GetType();

                    drRow = null;
                    drRow = dtProperties.Select("FormFieldName='" + field.GetName() + "'");

                    switch (type)
                    {
                        case Field.Type.e_button:
                            if (drRow != null && drRow.Length > 0)
                            {
                                //try
                                //{
                                //    objC4.C4DoctorInformation.ProviderID =  System.Convert.ToInt64(drRow[0]["FormFieldValue"]) ;
                                //}
                                //catch
                                //{
                                //}

                            }
                            //Console.WriteLine("Button" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());
                            break;
                        case Field.Type.e_radio:
                            //Console.WriteLine("Radio button" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString() + "  :  " + field.GetValueAsBool());
                            break;
                        case Field.Type.e_check:
                            //Console.WriteLine("Check box" + "  :  " + field.GetName());
                            //Console.WriteLine("Check box" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());

                            if (drRow != null && drRow.Length > 0)
                            {
                                try
                                {
                                    field.SetValue(System.Convert.ToBoolean(drRow[0]["FormFieldValue"]));
                                }
                                catch
                                {
                                }
                            }

                            break;
                        case Field.Type.e_text:
                            {
                                //Console.WriteLine("Text" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());


                                if (drRow != null && drRow.Length > 0)
                                {
                                    try
                                    {
                                        field.SetValue(System.Convert.ToString(drRow[0]["FormFieldValue"]));
                                    }
                                    catch
                                    {
                                    }

                                }
                            }
                            break;
                        case Field.Type.e_choice:
                            //Console.WriteLine("Choice" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());
                            break;
                        case Field.Type.e_signature:
                            //Console.WriteLine("Signature" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());


                            break;
                    }

                    field = null;
                }

                bIsSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                drRow = null;
                field = null;

                if (itr != null)
                {
                    itr.Dispose();
                    itr = null;
                }
            }

            return bIsSuccess;
        }
        private DataTable GetAllPropertiesInfo(C4Authorization objC4Auth)
        {
            DataTable dtProp = null;
            PropertyInfo[] propinfo = null;
            
            try
            {
                dtProp = new DataTable();

                dtProp.Columns.Add("PropertyName", System.Type.GetType("System.String"));
                dtProp.Columns.Add("FormFieldName", System.Type.GetType("System.String"));
                dtProp.Columns.Add("FormFieldValue", System.Type.GetType("System.String"));
                dtProp.Columns.Add("FormFieldValueImage", System.Type.GetType("System.Byte[]"));

                propinfo = objC4Auth.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_DiagnosticTests);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Therapy);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Surgery);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_AuthorizationRequested_Treatment);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_MedicalTreatmentGuidelines);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_CarrierResponseToAuthorizationRequest);

                propinfo = null;
                propinfo = objC4Auth.C4_Auth_Footer.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC4Auth.C4_Auth_Footer);

                return dtProp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dtProp;
            }
            finally
            {
                if (dtProp != null)
                {
                    dtProp.Dispose();
                    dtProp = null;
                }
            }
            
        }

        private void PopulatePropertiesData(ref DataTable dtProp, PropertyInfo[] propinfo, object obj)
        {
            DataRow drRow = null;

            try
            {
                if (propinfo != null)
                { 

                    foreach (PropertyInfo prop in propinfo)
                    {
                        string sDisplayName = objC4Auth.GetAttributeDisplayName(prop);

                        if (!string.IsNullOrEmpty(sDisplayName))
                        {
                            drRow = dtProp.NewRow();

                            drRow["PropertyName"] = prop.Name;
                            drRow["FormFieldName"] = sDisplayName;
                            drRow["FormFieldValue"] = prop.GetValue(obj, null);

                            if (prop.PropertyType == System.Type.GetType("System.Byte[]"))
                            {
                                drRow["FormFieldValue"] = null;
                                drRow["FormFieldValueImage"] = prop.GetValue(obj, null);
                            }
                            else
                            {
                                drRow["FormFieldValue"] = prop.GetValue(obj, null);
                                drRow["FormFieldValueImage"] = null;
                            }

                            dtProp.Rows.Add(drRow);

                            drRow = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsb_SaveData_Click(object sender, EventArgs e)
        {
            Boolean bIsSuccess = false;

            try
            {
                _pdfview.Focus();
                this.Cursor = Cursors.WaitCursor;
                tsb_SaveData.Enabled = false;

                bIsSuccess = GetFormData(ref _pdfdoc);
                bIsSuccess = SetDataToDBAdapter();
                bIsSuccess = UpdateDataAdaptersToDatabase();

                if (bIsSuccess)
                {
                    MessageBox.Show("Form data saved successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Save, "C4 AUTH Form saved with claim '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms,
                                                gloAuditTrail.ActivityType.Save,
                                                (lblClaimNo.Text.Trim().Length > 0) ? "C4 AUTH Form saved with claim '" + lblClaimNo.Text + "'." : "C4 AUTH Form saved without claim association.",
                                                PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));

                }
                else
                {
                    MessageBox.Show("Unable to save form data", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tsb_SaveData.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        //private byte[] GetProviderSignImage(Field field)
        //{
        //    System.Drawing.Bitmap ProviderSignatureBmp = null;
        //    try
        //    {

        //        if (field.IsAnnot())
        //        {
        //            Annot ButtonAnnot = new Annot(field.GetSDFObj());
        //            Obj app_stm = ButtonAnnot.GetAppearance();
        //            if (app_stm != null)
        //            {
        //                ElementReader reader = new ElementReader();
        //                Element element;
        //                reader.Begin(app_stm);
        //                while ((element = reader.Next()) != null)  // Read page contents
        //                {
        //                    if (element.GetType() == Element.Type.e_image)
        //                    {
        //                        ProviderSignatureBmp = element.GetBitmap();
        //                        break;
        //                    }
        //                }
        //                reader.End();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    if (ProviderSignatureBmp == null) return null;
        //    else
        //    {
        //        ImageConverter imgByte = new ImageConverter();
        //        try
        //        {
        //            byte[] ImgByteArr = (byte[])imgByte.ConvertTo(ProviderSignatureBmp, System.Type.GetType("System.Byte[]"));
        //            return ImgByteArr;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //        imgByte = null;
        //        return null;
        //    }
        //}

        private bool IsValidClaimNumber()
        {
            string[] _claim = null;

            try
            {
                if (!String.IsNullOrEmpty(txtClaimNoSearch.Text))
                {
                    _claim = txtClaimNoSearch.Text.Split('-');

                    if (_claim.Length.Equals(2))
                    {
                        if (System.Convert.ToString(_claim[1]).Trim() == "")
                        {
                            MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClaimNoSearch.Focus();
                        
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _claim = null;
            }
            return true;
        }
        
        private void btnSearchPatientClaim_Click(object sender, EventArgs e)
        {
            frmWCPatientClaims ofrmWCPatientClaims = null;
            DataSet dsInfo = null;

            try
            {
                if (IsValidClaimNumber())
                {
                   ofrmWCPatientClaims = new frmWCPatientClaims(PatientId);

                       ofrmWCPatientClaims.ShowDialog(this);
                        txtClaimNoSearch.Focus();

                        if (ofrmWCPatientClaims.flgOk == false)
                        { return; }

                        double dblVScrollPosHere = _pdfview.GetVScrollPos();
                        double dblHScrollPosHere = _pdfview.GetHScrollPos();

                        bool bIsSuccess = GetFormData(ref _pdfdoc);

                        TransactionID= ofrmWCPatientClaims.TransactionID;
                        TransactionMasterID=ofrmWCPatientClaims.TransactionMasterID;

                        ClaimNo = ofrmWCPatientClaims.ClaimNo;
                        if (ObjWorkerComp != null)
                        {
                            ObjWorkerComp.Dispose();
                        }

                        ObjWorkerComp = new clsWorkerCompData(sDBConnectionString);

                        dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                        ObjWorkerComp.SetC4AuthorizationPrdefinedInfo(dsInfo, ref objC4Auth,true,false);

                        //PDFDoc FreshPdfdoc = new PDFDoc(@"D:\SprintWork\8060\NYWC\NYWCFiles\c4AUTH_tab_ImageSign.pdf");
                        PDFDoc FreshPdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign, global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign.Length);

                        try
                        {
                            FreshPdfdoc.InitSecurityHandler();
                        }
                        catch
                        {
                        }

                        if (objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage != null && objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage.Length > 0)
                        {
                            LoadPDFSign(ref FreshPdfdoc);
                        }

                        SetFormData(ref FreshPdfdoc);

                        SetFormFieldsDataClaimOnly(ref FreshPdfdoc);

                        _pdfview.SetDoc(FreshPdfdoc);

                        try
                        {
                            if (_pdfdoc != null)
                            {
                                _pdfdoc.Close();
                                _pdfdoc.Dispose();
                            }
                        }
                        catch
                        {
                        }
                        _pdfdoc = FreshPdfdoc;

                        _pdfview.SetHScrollPos(dblHScrollPosHere);
                        _pdfview.SetVScrollPos(dblVScrollPosHere);

                        lblClaimNo.Text = ClaimNo;

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Modify, "Claim modified on C4 AUTH Form with new value '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ofrmWCPatientClaims != null)
                {
                    ofrmWCPatientClaims.Dispose();
                    ofrmWCPatientClaims = null;
                }

                if (dsInfo != null)
                {
                    dsInfo.Dispose();
                    dsInfo = null;
                }
            }
        }

        private void LoadPDFSign(ref PDFDoc SignPdfdoc)
        {
            try
            {
                string fname = ObjWorkerComp.SignPDFImage(ref SignPdfdoc, objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage, "topmostSubform[0].Page2[0].ProviderSignature[0]", 2);

                if (!string.IsNullOrEmpty(fname))
                {
                    try
                    {
                        PDFDoc FreshPdfDoc = new PDFDoc(fname);
                        try
                        {
                            FreshPdfDoc.InitSecurityHandler();
                        }
                        catch
                        {
                        }
                        //       _pdfview.SetDoc(FreshPdfDoc);
                        try
                        {
                            if (SignPdfdoc != null)
                            {
                                SignPdfdoc.Close();
                                SignPdfdoc.Dispose();
                            }
                        }
                        catch
                        {
                        }
                        SignPdfdoc = FreshPdfDoc;

                        //   _pdfview.SetVScrollPos(dblVScrollPos);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void txtClaimNoSearch_KeyDown(object sender, KeyEventArgs e)
        {
            Regex CheckPattern = null;
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable _dt = null;
            // string _sqlQuery = null;
            DataSet dsInfo = null;

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CheckPattern = new Regex(@"^[0-9-]+$");

                    if (!string.IsNullOrEmpty(txtClaimNoSearch.Text.Trim()))
                    {
                        if (CheckPattern.IsMatch(txtClaimNoSearch.Text.Trim()))
                        {
                            //oDB = new gloDatabaseLayer.DBLayer(sDBConnectionString);

                            //_sqlQuery = "SELECT  ISNULL(nTransactionMasterID, 0) AS nTransactionMasterID,ISNULL(nTransactionID, 0) AS nTransactionID " +
                            //" FROM    BL_Transaction_Claim_MST WITH ( NOLOCK ) " +
                            //" WHERE   dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo, " +
                            //" BL_Transaction_Claim_MST.nSubClaimNo, " +
                            //" BL_Transaction_Claim_MST.sMainClaimNo, 5) = '" + txtClaimNoSearch.Text.Trim().Replace("'", "''") + "' " +
                            //" AND BL_Transaction_Claim_MST.nPatientID = " + PatientId;

                            //oDB.Connect(false);
                            //oDB.Retrive_Query(_sqlQuery, out _dt);
                            //oDB.Disconnect();

                            _dt = ObjWorkerComp.GetClaimsForPatient(PatientId, sDBConnectionString);

                            if (_dt != null && _dt.Rows.Count > 0)
                            {
                                DataRow[] drRows = _dt.Select("Claim = '" + txtClaimNoSearch.Text.Trim().Replace("'", "''") + "'");

                                if (drRows != null && drRows.Length > 0)
                                {

                                    TransactionMasterID = System.Convert.ToInt64(drRows[0]["TransactionMasterID"]);
                                    TransactionID = System.Convert.ToInt64(drRows[0]["TransactionID"]);
                                    
                                    if (TransactionMasterID > 0 && TransactionID > 0)
                                    {
                                        double dblVScrollPosHere = _pdfview.GetVScrollPos();
                                        double dblHScrollPosHere = _pdfview.GetHScrollPos();

                                        bool bIsSuccess = GetFormData(ref _pdfdoc);

                                        if (ObjWorkerComp != null)
                                        {
                                            ObjWorkerComp.Dispose();
                                        }


                                        ObjWorkerComp = new clsWorkerCompData(sDBConnectionString);

                                        dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                                        ObjWorkerComp.SetC4AuthorizationPrdefinedInfo(dsInfo, ref objC4Auth, true, false);

                                        //PDFDoc FreshPdfdoc = new PDFDoc(@"D:\SprintWork\8060\NYWC\NYWCFiles\c4AUTH_tab_ImageSign.pdf");
                                        PDFDoc FreshPdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign, global::gloBilling.Properties.Resources.c4AUTH_tab_ImageSign.Length);

                                        try
                                        {
                                            FreshPdfdoc.InitSecurityHandler();
                                        }
                                        catch
                                        {
                                        }

                                        if (objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage != null && objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage.Length > 0)
                                        {
                                            LoadPDFSign(ref FreshPdfdoc);
                                        }

                                        SetFormData(ref FreshPdfdoc);
                                        SetFormFieldsDataClaimOnly(ref FreshPdfdoc);
                                        _pdfview.SetDoc(FreshPdfdoc);

                                        try
                                        {
                                            if (_pdfdoc != null)
                                            {
                                                _pdfdoc.Close();
                                                _pdfdoc.Dispose();
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        _pdfdoc = FreshPdfdoc;

                                        _pdfview.SetHScrollPos(dblHScrollPosHere);
                                        _pdfview.SetVScrollPos(dblVScrollPosHere);

                                        lblClaimNo.Text = txtClaimNoSearch.Text.Trim();

                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Modify, "Claim modified on C4 AUTH Form with new value '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtClaimNoSearch.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtClaimNoSearch.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtClaimNoSearch.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Only numeric values allowed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClaimNoSearch.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CheckPattern = null;
                // _sqlQuery = null;

                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }

                if (dsInfo != null)
                {
                    dsInfo.Dispose();
                    dsInfo = null;
                }

            }
        }

        private void txtClearSearch_Click(object sender, EventArgs e)
        {
            txtClaimNoSearch.Clear();
            txtClaimNoSearch.Focus();
       }

        private void SetFieldsReadOnly(ref PDFDoc FormPdfDoc)
        {
            FieldIterator itr = null;
            Field field = null;

            try
            {
                //_pdfview.Focus();

                //_pdfdoc = _pdfview.GetDoc();

                //_pdfdoc.InitSecurityHandler();

                for (itr = FormPdfDoc.GetFieldIterator(); itr.HasNext(); itr.Next())
                {
                    field = itr.Current();

                    field.SetFlag(Field.Flag.e_read_only, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                field = null;

                if (itr != null)
                {
                    itr.Dispose();
                    itr = null;
                }

            }

            
        }

        private void frmWorkerCompFormViewer_C4Auth_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flgReadOnly == false)
            {
                ObjWorkerComp.CheckLockStatusForWorkerCompForm(iFormID, false, sUserName, Environment.MachineName, ref objUserName, ref objMachineName);
            }

            ObjWorkerComp.DisconnectToPDFTron();
        }

        private void frmWorkerCompFormViewer_C4Auth_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (_pdfview != null)
                {
                    _pdfview.Dispose();
                    _pdfview = null;
                }

                if (_pdfdoc != null)
                {
                    _pdfdoc.Dispose();
                    _pdfdoc = null;
                }

                appSettings = null;

                if (_sqlcommandMST != null) { _sqlcommandMST.Dispose(); }
                _sqlcommandMST = null;

                if (_sqlcommandDTL != null) { _sqlcommandDTL.Dispose(); }
                _sqlcommandDTL = null;


                if (objConn != null)
                {
                    if (objConn.State != ConnectionState.Closed)
                    {
                        objConn.Close();
                    }

                    objConn.Dispose();
                    objConn = null;
                }

                if (daWorkersCompFormsMST != null)
                {
                    daWorkersCompFormsMST.Dispose();
                    daWorkersCompFormsMST = null;
                }

                if (daWorkersCompFormsDTL != null)
                {
                    daWorkersCompFormsDTL.Dispose();
                    daWorkersCompFormsDTL = null;
                }

                if (dsWorkersCompForms != null)
                {
                    dsWorkersCompForms.Dispose();
                    dsWorkersCompForms = null;
                }

                if (dtProperties != null)
                {
                    dtProperties.Dispose();
                    dtProperties = null;
                }
                //Clean up the Forms Saved physically on Temp Directory.
                if (System.IO.Directory.Exists(ObjWorkerComp._AppTempFolderPathNYWC) == true)
                {
                    try
                    { System.IO.Directory.Delete(ObjWorkerComp._AppTempFolderPathNYWC, true); }
                    catch (IOException)
                    { //gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(), false); 
                    }
                    //     dirChecked = false;
                }
                //**Clean up the Forms Saved physically on Temp Directory.  

                objC4Auth = null;
                if (ObjWorkerComp != null)
                {
                    ObjWorkerComp.Dispose();
                    ObjWorkerComp = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

    }
}
