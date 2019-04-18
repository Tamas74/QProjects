using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class frmWorkerCompFormViewer_C42 : Form
    {
        #region "Member Declarations"
     
        //private Int64 _C42RenderAttempts =0;
     //   private string _AppTempFolderPathNYWC = gloSettings.FolderSettings.AppTempFolderPath + "NYWC\\";
        private PDFViewCtrl _pdfview;
        private PDFDoc _pdfdoc;
        public long PatientId;
        public long TransactionID;
        public long TransactionMasterID;
        public long ClinicId;
        public long VisitId;

        public bool flgReadOnly = false;

        public C4_2ProgressReport objC42;
        clsWorkerCompData ObjWorkerComp=null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "";
        private Int64 _UserID = 0;

        string sDBConnectionString = null;
        SqlConnection objConn = null;


        SqlParameter oParam = null;
        SqlCommand _sqlcommandDTL = new SqlCommand();
        SqlCommand _sqlcommandMST = new SqlCommand();

        SqlDataAdapter daWorkersCompFormsMST = null;
        SqlDataAdapter daWorkersCompFormsDTL = null;

        DataSet dsWorkersCompForms = null;
        Int64 iFormID = 0;
        DataTable dtProperties = null;



        object objUserName = null;
        object objMachineName = null;
        string sUserName = null;
        // gloPatientStripControl.gloClaimSearchControl oPatientStripControl = null; 

        #endregion // "Member Declarations"

        #region "Property Procedures"
        public string ClaimNo { get; set; }
        #endregion // "Property Procedures"

        public frmWorkerCompFormViewer_C42(Int64 patientid, Int64 transactionID, Int64 transactionmasterID, string _databaseconnectionstring, Int64 formID)
        {
            InitializeComponent();
            Boolean bIsNewForm = true;
            DataSet dsInfo = null;

            try
            {


                sDBConnectionString = _databaseconnectionstring;
                objC42 = new C4_2ProgressReport();

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { ClinicId = System.Convert.ToInt64(appSettings["ClinicID"]); }
                    else { ClinicId = 1; }
                }
                else
                { ClinicId = 1; }


                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = System.Convert.ToString(appSettings["MessageBOXCaption"]);
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
                        _UserID = System.Convert.ToInt64(appSettings["UserID"]);
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

                // dtProperties = new DataTable();
                if (dtProperties != null)
                {
                    dtProperties.Dispose();
                    dtProperties = null;
                }
                dtProperties = GetAllPropertiesInfo(objC42);
                if (objConn != null)
                {
                    if (objConn.State != ConnectionState.Closed)
                    {
                        objConn.Close();
                    }

                    objConn.Dispose();
                    objConn = null;
                }
                objConn = new SqlConnection(_databaseconnectionstring);
                objConn.Open();
                if (dsWorkersCompForms != null)
                {
                    dsWorkersCompForms.Dispose();
                    dsWorkersCompForms = null;
                }
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
                oParam.Value = (int)WorkersCompFormTypes.C42;
                oParam.DbType = DbType.Int32;
                oParam.Direction = ParameterDirection.Input;
                _sqlcommandMST.Parameters.Add(oParam);
                oParam = null;
                if (daWorkersCompFormsMST != null)
                {
                    daWorkersCompFormsMST.Dispose();
                    daWorkersCompFormsMST = null;
                }



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
                    daWorkersCompFormsDTL = null;
                }
                daWorkersCompFormsDTL = new SqlDataAdapter(_sqlcommandDTL);

                daWorkersCompFormsDTL.FillSchema(dsWorkersCompForms, SchemaType.Source, "WorkersCompForms_DTL");
                daWorkersCompFormsDTL.Fill(dsWorkersCompForms, "WorkersCompForms_DTL");

                objConn.Close();

                if (dsWorkersCompForms != null && dsWorkersCompForms.Tables.Count > 0)
                {
                    if (dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Count > 0 && dsWorkersCompForms.Tables["WorkersCompForms_DTL"].Rows.Count > 0)
                    {
                        SetDataToC42Object(dsWorkersCompForms.Tables["WorkersCompForms_DTL"], ref objC42);
                        bIsNewForm = false;
                    }
                }

                if (bIsNewForm)
                {


                    dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                    ObjWorkerComp.SetC42PrdefinedInfo(dsInfo, ref objC42, true, true);
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

                _pdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.C4_2_ImageSign, global::gloBilling.Properties.Resources.C4_2_ImageSign.Length);

                try
                {
                    _pdfdoc.InitSecurityHandler();
                }
                catch
                {
                }

                setMyFontForDocumentFields(ref _pdfdoc);

                if (objC42.C4_2_DoctorsInfo.ProviderSignImage != null && objC42.C4_2_DoctorsInfo.ProviderSignImage.Length > 0)
                {
                    LoadPDFSign(ref _pdfdoc);
                }

                
                // TODO : Enable this and write code for the data setting for C4.
                tsb_GetData.Visible = false;
                tsb_GetData.Enabled = false;
                //LoadPatientStripControl();
                SetFormFieldsData(ref _pdfdoc);
                lblPatientName.Text = objC42.C4_2_PatientInfo.PatientFullName;

                if (flgReadOnly)
                {
                    SetFieldsReadOnly(ref _pdfdoc);
                    tsb_SaveData.Enabled = false;
                    pnl_txtSearch.Enabled = false;
                    btnSearchPatientClaim.Enabled = false;
                }
                _pdfview.SetPageViewMode(PDFViewCtrl.PageViewMode.e_fit_page);
      //          _pdfview.SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                _pdfview.BringToFront();
                _pdfview.SetCaching(true);
                if (_pdfview.GetDoc() == null)
                {
                    _pdfview.SetDoc(_pdfdoc);
                }
                _pdfview.EnableInteractiveForms(true);
                _pdfview.SetFocus();
                _pdfview.SetPagePresentationMode(PDFViewCtrl.PagePresentationMode.e_single_continuous);
                _pdfview.SetHighlightFields(false);
                _pdfview.TabStop = false;
                //long  loadcounter=0;
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

                lblPatientName.Text = objC42.C4_2_PatientInfo.PatientFullName;

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

        private void setMyFontForDocumentFields(ref PDFDoc _pdfdoc)
        {

            //-----------Un used do not consider: Rnd To swap unknown fonts using User defined fonts through Page iterators.
            //               for (PageIterator Pi = _pdfdoc.GetPageIterator(); Pi.HasNext(); Pi.Next())
            //               {
            //                   Page pg = Pi.Current();
            //                   Obj res = pg.GetResourceDict();
            //                   if (res == null) { continue; }
            //                   Obj fonts = res.FindObj("Font");
            //                   if (fonts == null) { continue; }

            //                   for (DictIterator i = fonts.GetDictIterator(); i.HasNext(); i.Next())
            //                   {
            //                       pdftron.PDF.Font font = new pdftron.PDF.Font(i.Value());
            //                       if (font.GetType() != pdftron.PDF.Font.Type.e_TrueType) { continue; }
            //                       string fname = font.GetName();
            //                       if (fname != "MyFont") { continue; }
            //                       pdftron.PDF.Font new_font = pdftron.PDF.Font.Create(_pdfdoc, pdftron.PDF.Font.StandardType1Font.e_helvetica);
            //                       _pdfdoc.GetSDFDoc().Swap(new_font.GetSDFObj().GetObjNum(), font.GetSDFObj().GetObjNum());
            //                   }
            //                 }
            //-----------Un used do not consider: Rnd To swap unknown fonts using User defined fonts through Page iterators.

            pdftron.PDF.Font fnt = null;
            Obj formObj = null;
            Obj dr = null;
            Obj font_dict = null;
            Obj myfont = null;
            Obj myfont_dict = null;
            try
            {
                formObj = _pdfdoc.GetAcroForm();
                dr = formObj.FindObj("DR");
                if (dr == null) dr = formObj.PutDict("DR");
                font_dict = dr.FindObj("Font");
                if (font_dict == null) font_dict = dr.PutDict("Font");
                myfont = font_dict.FindObj("MyFont");
                if (myfont == null)
                {
                    fnt = pdftron.PDF.Font.Create(_pdfdoc, pdftron.PDF.Font.StandardType1Font.e_times_roman, true);
                    myfont = fnt.GetSDFObj();
                    myfont_dict = font_dict.Put("MyFont", myfont);
                    Obj o = null;
                    Obj m = null;
                    for (FieldIterator fi = _pdfdoc.GetFieldIterator(); fi.HasNext(); fi.Next())
                    {
                        Field currentField = fi.Current();
                        if (currentField.GetType() == Field.Type.e_text)
                        {
                            o = currentField.GetSDFObj();
                            if (o != null)
                            {
                                m = o.PutString("DA", "/MyFont 10 Tf 0 0 0 rg ");
                            }
                            currentField.EraseAppearance();
                            try
                            {
                                currentField.RefreshAppearance();
                            }
                            catch (Exception ex1)
                            {
                                MessageBox.Show(ex1.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ex1 = null;
                            }
                        }
                    }
                    o = null;
                    m = null;
                }

                _pdfdoc.Save(ObjWorkerComp._AppTempFolderPathNYWC + "Startup" + Guid.NewGuid().ToString() + ".pdf", SDFDoc.SaveOptions.e_compatibility);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                if (fnt != null) { fnt.Dispose(); fnt = null; }
                if (formObj != null) { formObj.Dispose(); formObj = null; }
                if (dr != null) { dr.Dispose(); dr = null; }
                if (font_dict != null) { font_dict.Dispose(); font_dict = null; }
                if (myfont != null) { myfont.Dispose(); myfont = null; }
                if (myfont_dict != null) { myfont_dict.Dispose(); myfont_dict = null; }
            }
        }

        private void frmWorkerCompFormViewer_C42_Load(object sender, EventArgs e)
        {
            ObjWorkerComp.ConnectToPDFTron();
            LoadForWorkerCompViewer();
            txtClaimNoSearch.Focus();
            txtClaimNoSearch.TabStop = false;
            btnSearchPatientClaim.TabStop = false;
            txtClearSearch.TabStop = false;
            this.Text = "Worker Comp Form Viewer : C-4.2";

            if (iFormID > 0)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Modify, "C-4.2 Form opened for view.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            }
            else
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Add, "Attempted to create New Form of type C-4.2.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            }
        }

        private void SetFormFieldsDataClaimOnly(ref PDFDoc FormPdfDoc)
        {
            PropertyInfo[] propinfo = null;

            try
            {
                //_pdfdoc = _pdfview.GetDoc();

                // FormPdfDoc.InitSecurityHandler();
                propinfo = null;
                propinfo = objC42.C4_2_DoctorsInfo.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_DoctorsInfo);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_BillingInfo.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_BillingInfo);
                    }
                }

                propinfo = null;

                SetData(ref FormPdfDoc, objC42.C4_2_Footer.GetType().GetProperty("AuthorizedProviderName"), objC42.C4_2_Footer);
                SetData(ref FormPdfDoc, objC42.C4_2_Footer.GetType().GetProperty("AuthorizedProviderSpeciality"), objC42.C4_2_Footer);

                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("DateOfExam"), objC42.C4_2_PatientInfo);
                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("CarrierCaseNumber"), objC42.C4_2_PatientInfo);
                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("WCBCaseNumber"), objC42.C4_2_PatientInfo);
                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("DateOfInjuryMM"), objC42.C4_2_PatientInfo);
                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("DateOfInjuryDD"), objC42.C4_2_PatientInfo);
                SetData(ref FormPdfDoc, objC42.C4_2_PatientInfo.GetType().GetProperty("DateOfInjuryYY"), objC42.C4_2_PatientInfo);
                  
                SetData(ref FormPdfDoc, objC42.C4_2_Header.GetType().GetProperty("DateOfInjuryHeaderDD"), objC42.C4_2_Header);
                SetData(ref FormPdfDoc, objC42.C4_2_Header.GetType().GetProperty("DateOfInjuryHeaderMM"), objC42.C4_2_Header);
                SetData(ref FormPdfDoc, objC42.C4_2_Header.GetType().GetProperty("DateOfInjuryHeaderYYYY"), objC42.C4_2_Header);

                

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
                propinfo = null;
            }


        }


        private void SetFormFieldsData(ref PDFDoc FormPdfDoc)
        {
            //_C42RenderAttempts ++;
            PropertyInfo[] propinfo = null;

            try
            {

                propinfo = objC42.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_Header.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_Header);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_PatientInfo.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_PatientInfo);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_DoctorsInfo.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_DoctorsInfo);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_BillingInfo.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_BillingInfo);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ExamAndTreatment);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_DoctorsOpinion.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_DoctorsOpinion);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ReturnToWork);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply);
                    }
                }
                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2WorkLimitations.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ReturnToWork.C4_2WorkLimitations);
                    }
                }
                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply);
                    }
                }

                propinfo = null;
                propinfo = objC42.C4_2_Footer.GetType().GetProperties();

                if (propinfo != null && propinfo.Length > 0)
                {
                    foreach (PropertyInfo prop in propinfo)
                    {
                        SetData(ref FormPdfDoc, prop, objC42.C4_2_Footer);
                    }
                }




                // Regenerate field appearances.

                // try
                //{   FormPdfDoc.RefreshFieldAppearances(); }
                //catch (Exception) { }

                //_pdfview.Update();
                //_pdfview.SetHighlightFields(false);

                try
                {

                    string fname = ObjWorkerComp._AppTempFolderPathNYWC + "BeforeRefresh" + Guid.NewGuid().ToString() + ".pdf";
                    FormPdfDoc.Save(fname, SDFDoc.SaveOptions.e_compatibility);


                    try
                    {
                        if (_pdfview != null && _pdfview.GetDoc() != null)
                        {
                            _pdfview.CloseDoc();
                            FormPdfDoc = new PDFDoc(fname);
                            _pdfview.SetDoc(FormPdfDoc);
                        }

                    }
                    catch (Exception)
                    { }


                    //MessageBox.Show(_pdfview.Font.Name);   

                    //string fname = Guid.NewGuid().ToString();
                    //_pdfdoc.Save(_AppTempFolderPathNYWC + "BeforeRefresh" + fname + ".pdf", SDFDoc.SaveOptions.e_compatibility);
                    //try
                    //{
                    //    _pdfdoc.RefreshFieldAppearances();
                    //    _pdfdoc.Save(_AppTempFolderPathNYWC + "AfterRefresh" + fname + ".pdf", SDFDoc.SaveOptions.e_compatibility);
                    //}
                    //catch (Exception)
                    //{


                    //}




                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.InnerException + " : ex.ToString : " + ex.ToString(),
                    //        _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //    ex = null;

                    //    //while (_C42RenderAttempts < 10)
                    //    //{
                    //    //    //SetFormFieldsData();
                    //    //    LoadForWorkerCompViewer();
                    //    //    Console.WriteLine("Attempt after Crash with # :" + _C42RenderAttempts);
                    //    //    break;
                    //    //}
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool SetData(ref PDFDoc oDoc, PropertyInfo prop, object objClass)
        {
            string sField = null;
            object oValue = null;
            Field fld = null;

            try
            {
                if (oDoc == null || prop == null || objClass == null) { return false; }

                sField = objC42.GetAttributeDisplayName(prop);
                oValue = prop.GetValue(objClass, null);

                if (string.IsNullOrEmpty(sField))
                { return false; }
                //if (oValue == null)
                //{ return false; }
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
                    //try
                    //{
                    //    //Obj myObj = fld.GetSDFObj();
                    //    ////pdftron.PDF.Font myFont = pdftron.PDF.Font.Create(oDoc, pdftron.PDF.Font.StandardType1Font.e_helvetica);

                    //    //if (myObj != null)
                    //    //{
                    //    //    myObj.PutString("DA", "/Helvetica 10 Tf 0 0 0 rg "); 
                    //    //}
                    //    fld.EraseAppearance();

                    //}
                    //catch
                    //{

                    //}




                    Field.Type itype = fld.GetType();
                    switch (itype)
                    {
                        case Field.Type.e_check:
                            //fld.SetValue(true);
                            fld.SetValue(System.Convert.ToBoolean(oValue));
                            /*//MessageBox.Show("Check box"); */
                            break;
                        case Field.Type.e_radio:
                            {
                                int maxlen = fld.GetMaxLen();
                                String thisString = System.Convert.ToString(oValue);
                                if (thisString.Length < maxlen || maxlen < 0)
                                {
                                    maxlen = thisString.Length;
                                }
                                if (maxlen == 0)
                                {
                                    fld.SetValue("");
                                }
                                else
                                {
                                    fld.SetValue(thisString.Substring(0, maxlen));
                                }

                                break;
                            }
                        case Field.Type.e_text:
                            {
                                try
                                {
                                    //Obj myObj = fld.GetSDFObj();
                                    ////pdftron.PDF.Font myFont = pdftron.PDF.Font.Create(oDoc, pdftron.PDF.Font.StandardType1Font.e_helvetica);

                                    //if (myObj != null)
                                    //{
                                    //    myObj.PutString("DA", "/Helvetica 10 Tf 0 0 0 rg "); 
                                    //}
                                    fld.EraseAppearance();

                                }
                                catch
                                {

                                }
                                //*****SAMPLE Code from PDF net SDK : Change the value for the Field
                                //MessageBox.Show("Text");
                                //// Edit all variable text in the document
                                //String old_value;
                                //if (fld.getValue()!=null)
                                //{
                                //    old_value=current.getValueAsString();
                                //    current.setValue("This is a new value. The old one was: " + old_value);
                                //}
                                //*****SAMPLE Code from PDF net SDK : Change the value for the Field


                                int maxlen = fld.GetMaxLen();
                                String thisString = System.Convert.ToString(oValue);
                                if (thisString.Length < maxlen || maxlen < 0)
                                {
                                    maxlen = thisString.Length;
                                }
                                if (maxlen == 0)
                                {
                                    fld.SetValue("");
                                }
                                else
                                {
                                    fld.SetValue(thisString.Substring(0, maxlen));
                                }

                                try
                                {
                                    fld.RefreshAppearance();
                                }
                                catch
                                {
                                    string fname = Guid.NewGuid().ToString();
                                    oDoc.Save(ObjWorkerComp._AppTempFolderPathNYWC + "CatchBegin" + fname + ".pdf", SDFDoc.SaveOptions.e_compatibility);

                                    Obj myObj = fld.GetSDFObj();
                                    if (myObj != null)
                                    {
                                        Obj thisObj = myObj.PutString("DA", "/Times-Roman 10 Tf 1 0 0 rg ");
                                        //One of Listed standard 14 font to be used in above function, below are some Listed fonts supported.
                                        //Courier
                                        //Courier-Bold
                                        //Helvetica
                                        //Times-Roman
                                        thisObj = null;
                                        myObj = null;

                                    }
                                    //myObj.FindObj("Font");

                                    fld.EraseAppearance();

                                    if (fld.GetType() == Field.Type.e_text || fld.GetType() == Field.Type.e_radio)
                                    {
                                        //*****SAMPLE Code from PDF net SDK : Change the value for the Field
                                        //MessageBox.Show("Text");
                                        //// Edit all variable text in the document
                                        //String old_value;
                                        //if (fld.getValue()!=null)
                                        //{
                                        //    old_value=current.getValueAsString();
                                        //    current.setValue("This is a new value. The old one was: " + old_value);
                                        //}
                                        //*****SAMPLE Code from PDF net SDK : Change the value for the Field

                                        maxlen = fld.GetMaxLen();
                                        thisString = System.Convert.ToString(oValue);
                                        if (thisString.Length < maxlen || maxlen < 0) // 0 checked since it returns maxlength (-1) if not set for control.
                                        {
                                            maxlen = thisString.Length;
                                        }
                                        if (maxlen == 0)
                                        {
                                            fld.SetValue("");
                                        }
                                        else
                                        {
                                            fld.SetValue(thisString.Substring(0, maxlen));
                                        }
                                        //fld.SetValue(thisString.Substring(0, maxlen));
                                    }
                                    try
                                    {

                                        fld.RefreshAppearance();

                                        oDoc.Save(ObjWorkerComp._AppTempFolderPathNYWC + "CatchEnd" + fname + ".pdf", SDFDoc.SaveOptions.e_compatibility);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error refreshing field appearances: field name : " + fld + " : field value : " + fld.GetValueAsString() + " : with Inner Exception : " + ex.InnerException + " : Error String : " + ex.ToString(),
                                            _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        ex = null;
                                    }

                                }
                            }
                            break;
                        case Field.Type.e_choice:/*MessageBox.Show("Choice");*/ break;
                        case Field.Type.e_signature:/*MessageBox.Show("Signature"); */break;
                        case Field.Type.e_button:/*//MessageBox.Show("Button");*/break;
                        default: /*//MessageBox.Show("Unknown Feild Type");*/break;
                    }




                    //--------------------------------------------------------

                    //if (string.Compare(prop.PropertyType.FullName, "System.Boolean", true) == 0)
                    //{

                    //    if (fld.GetType() == pdftron.PDF.Field.Type.e_check)
                    //    {

                    //        fld.SetValue(System.Convert.ToBoolean(oValue));
                    //    }
                    //    else
                    //    {
                    //        int maxlen = fld.GetMaxLen();
                    //        String thisString = System.Convert.ToString(oValue);
                    //        if( thisString.Length < maxlen || maxlen<0) 
                    //        {
                    //            maxlen = thisString.Length ;
                    //        }
                    //        fld.SetValue(thisString.Substring(0,maxlen));

                    //    }

                    //}
                    //else
                    //{
                    //    int maxlen = fld.GetMaxLen();
                    //    String thisString = System.Convert.ToString(oValue);
                    //    if (thisString.Length < maxlen || maxlen < 0)
                    //    {
                    //        maxlen = thisString.Length;
                    //    }
                    //    fld.SetValue(thisString.Substring(0, maxlen));
                    //}



                    //--------------------------------------------------------


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

        //private bool SetData(PDFDoc oDoc, string sField, string sValue, Field.TextJustification Alignment = Field.TextJustification.e_left_justified)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(sField) || string.IsNullOrEmpty(sValue))
        //        { return false; }

        //        Field fld = oDoc.GetField(sField);

        //        if (fld != null)
        //        {
        //            fld.SetValue(sValue);
        //            fld.SetJustification(Alignment);
        //            //fld.SetFlag(Field.Flag.e_read_only, true);
        //            fld.RefreshAppearance();
        //            //oDoc.RefreshFieldAppearances();
        //            return true;
        //        }
        //        else
        //        {
        //            Console.Write("Filed not found : " + sField);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //}

        //private bool SetData(PDFDoc oDoc, string sField, Boolean bValue)
        //{
        //    try
        //    {
        //        Field fld = oDoc.GetField(sField);

        //        if (fld != null)
        //        {
        //            fld.SetValue(bValue);
        //            //fld.SetFlag(Field.Flag.e_read_only, true);
        //            return true;
        //        }
        //        else
        //        {
        //            Console.Write("Filed not found : " + sField);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //}

        private void tsp_Close_Click(object sender, EventArgs e)
        {
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Close, "C-4.2 Form Closed.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
            this.Close();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (iFormID <= 0)
                {
                    MessageBox.Show("C-4.2 Form you are trying to print is not yet saved." + System.Environment.NewLine + "Form will be saved automatically before printing.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tsb_SaveData_Click((object)(tsb_SaveData), e: e);
                }

                tsb_Print.Enabled = false;

                Print();

                //if (iFormID <= 0)
                //{
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Print, "C-4.2 Form printed without Saving in system.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                //}
                //else
                //{
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Print, "C-4.2 Form printed from system.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
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

       // pdftron.PDF.PDFDraw pdfdraw = null;

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
        //     //   pdfdraw = new pdftron.PDF.PDFDraw();
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
        //pdftron.PDF.Rect rect = null;
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

        private void SetDataToC42Object(DataTable dtData, ref C4_2ProgressReport objC42)
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

                    prop = objC42.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_Header.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_Header, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_PatientInfo.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_PatientInfo, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_DoctorsInfo.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

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
                            prop.SetValue(objC42.C4_2_DoctorsInfo, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_BillingInfo.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_BillingInfo, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ExamAndTreatment.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ExamAndTreatment, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_DoctorsOpinion.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_DoctorsOpinion, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ReturnToWork.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ReturnToWork, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ReturnToWork.C4_2WorkLimitations.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ReturnToWork.C4_2WorkLimitations, oValue, null);
                        }

                        continue;
                    }

                    prop = objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply, oValue, null);
                        }

                        continue;
                    }


                    prop = objC42.C4_2_Footer.GetType().GetProperty(System.Convert.ToString(drRow["FieldName"]));

                    if (prop != null)
                    {
                        if (!string.IsNullOrEmpty(System.Convert.ToString(oValue)))
                        {
                            oValue = System.Convert.ChangeType(oValue, prop.PropertyType);
                            prop.SetValue(objC42.C4_2_Footer, oValue, null);
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
                if (dsWorkersCompForms == null) { return bIsSuccess; }
                if (dsWorkersCompForms.Tables["WorkersCompForms_MST"] == null || dsWorkersCompForms.Tables["WorkersCompForms_DTL"] == null) { return bIsSuccess; }

                if (dsWorkersCompForms.Tables["WorkersCompForms_MST"].Rows.Count <= 0)
                {
                    drRow = dsWorkersCompForms.Tables["WorkersCompForms_MST"].NewRow();

                    iFormID = ObjWorkerComp.GetUniqueID(sDBConnectionString, _messageBoxCaption);

                    drRow["FormId"] = iFormID;
                    drRow["PatientId"] = PatientId;
                    drRow["ExamId"] = 0;
                    drRow["ClaimTransactionMasterID"] = TransactionMasterID;
                    drRow["ClaimTransactionID"] = TransactionID;
                    drRow["FormType"] = (int)WorkersCompFormTypes.C42;
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
                        //                        dtUniqueIds = new DataTable();

                        dtUniqueIds = ObjWorkerComp.GetUniqueIDs(dtProperties.Rows.Count, sDBConnectionString, _messageBoxCaption);
                    }

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
                                iFieldId = System.Convert.ToInt64(dtUniqueIds.Rows[i][0]);
                            }

                            if (iFieldId == 0)
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
                        case Field.Type.e_check:
                            //Console.WriteLine("Check box" + "  :  " + field.GetName());
                            //Console.WriteLine("Check box" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString());

                            if (drRow != null && drRow.Length > 0)
                            {
                                drRow[0]["FormFieldValue"] = field.GetValueAsBool();
                            }

                            break;
                        case Field.Type.e_radio:
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
                        case Field.Type.e_radio:
                            //Console.WriteLine("Radio button" + "  :  " + field.GetName() + "  :  " + field.GetValueAsString() + "  :  " + field.GetValueAsBool());
                            //break;
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

        private DataTable GetAllPropertiesInfo(C4_2ProgressReport objC42)
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

                propinfo = objC42.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42);

                propinfo = null;
                propinfo = objC42.C4_2_Header.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_Header);

                propinfo = null;
                propinfo = objC42.C4_2_PatientInfo.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_PatientInfo);

                propinfo = null;
                propinfo = objC42.C4_2_DoctorsInfo.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_DoctorsInfo);

                propinfo = null;
                propinfo = objC42.C4_2_BillingInfo.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_BillingInfo);

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ExamAndTreatment);

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ExamAndTreatment.C4_2Exam_Tests);

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ExamAndTreatment.C4_2Exam_Referrals);

                propinfo = null;
                propinfo = objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ExamAndTreatment.C4_2FollowUpVisit);

                propinfo = null;
                propinfo = objC42.C4_2_DoctorsOpinion.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_DoctorsOpinion);

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ReturnToWork);

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ReturnToWork.C4_2WorkRestrictionsApply);

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2WorkLimitations.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ReturnToWork.C4_2WorkLimitations);

                propinfo = null;
                propinfo = objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_ReturnToWork.C4_2_HowLongWorkLimitationsApply);

                propinfo = null;
                propinfo = objC42.C4_2_Footer.GetType().GetProperties();
                PopulatePropertiesData(ref dtProp, propinfo, objC42.C4_2_Footer);

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
                        string sDisplayName = objC42.GetAttributeDisplayName(prop);

                        if (!string.IsNullOrEmpty(sDisplayName))
                        {
                            drRow = dtProp.NewRow();

                            drRow["PropertyName"] = prop.Name;
                            drRow["FormFieldName"] = sDisplayName;

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
                   // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Save, "C-4.2 Form saved with Claim '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms,
                             gloAuditTrail.ActivityType.Save,
                             (lblClaimNo.Text.Trim().Length > 0) ? "C-4.2 Form saved with claim '" + lblClaimNo.Text + "'." : "C-4.2 Form saved without claim association.",
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
                //_C42RenderAttempts = 0;
                if (IsValidClaimNumber())
                {
                    ofrmWCPatientClaims = new frmWCPatientClaims(PatientId);


                    ofrmWCPatientClaims.ShowDialog(this);
                    txtClaimNoSearch.Focus();

                    if (ofrmWCPatientClaims.flgOk == false)
                    { return; }

                    double dblVScrollPosHere = _pdfview.GetVScrollPos();
                    double dblHScrollPosHere = _pdfview.GetHScrollPos();
                    //_pdfview.Focus();
                    //_pdfdoc = _pdfview.GetDoc();
                    bool bIsSuccess = GetFormData(ref _pdfdoc);
                    //GetFormData();

                    TransactionID = ofrmWCPatientClaims.TransactionID;
                    TransactionMasterID = ofrmWCPatientClaims.TransactionMasterID;

                    ClaimNo = ofrmWCPatientClaims.ClaimNo;
                    if (ObjWorkerComp != null)
                    {
                        ObjWorkerComp.Dispose();
                        ObjWorkerComp = null;
                    }
                    ObjWorkerComp = new clsWorkerCompData(sDBConnectionString);

                    dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                    ObjWorkerComp.SetC42PrdefinedInfo(dsInfo, ref objC42, true, false);

                    PDFDoc FreshPdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.C4_2_ImageSign, global::gloBilling.Properties.Resources.C4_2_ImageSign.Length);

                    try
                    {
                        FreshPdfdoc.InitSecurityHandler();
                    }
                    catch
                    {
                    }

                    if (objC42.C4_2_DoctorsInfo.ProviderSignImage != null && objC42.C4_2_DoctorsInfo.ProviderSignImage.Length > 0)
                    {
                        LoadPDFSign(ref FreshPdfdoc);
                    }
                    //SetFormFieldsData();
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

                    if (ObjWorkerComp.nServiceLinesCnt > 6)
                    {
                        pnlClaimWarning.Visible = true;
                    }
                    else
                    {
                        pnlClaimWarning.Visible = false;
                    }

                    lblClaimNo.Text = ClaimNo;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Modify, "Claim modified on C-4.2 Form with new value '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SLR: Finaly dispose ofrmPriorAuthorization
                if (ofrmWCPatientClaims != null)
                {
                    ofrmWCPatientClaims.Dispose();
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
                string fname = ObjWorkerComp.SignPDFImage(ref SignPdfdoc, objC42.C4_2_DoctorsInfo.ProviderSignImage, "topmostSubform[0].Page2[0].ProviderSignature[0]", 2);

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
                //_C42RenderAttempts=0;
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
                                            ObjWorkerComp = null;
                                        }

                                        ObjWorkerComp = new clsWorkerCompData(sDBConnectionString);

                                        dsInfo = ObjWorkerComp.GetWorkersCompPredefinedInfo(PatientId, TransactionID, TransactionMasterID, VisitId, ClinicId);

                                        ObjWorkerComp.SetC42PrdefinedInfo(dsInfo, ref objC42, true, false);

                                        PDFDoc FreshPdfdoc = new PDFDoc(global::gloBilling.Properties.Resources.C4_2_ImageSign, global::gloBilling.Properties.Resources.C4_2_ImageSign.Length);

                                        try
                                        {
                                            FreshPdfdoc.InitSecurityHandler();
                                        }
                                        catch
                                        {
                                        }
                                        if (objC42.C4_2_DoctorsInfo.ProviderSignImage != null && objC42.C4_2_DoctorsInfo.ProviderSignImage.Length > 0)
                                        {
                                            LoadPDFSign(ref FreshPdfdoc);
                                        }
                                        //SetFormFieldsData();
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

                                        if (ObjWorkerComp.nServiceLinesCnt > 6)
                                        {
                                            pnlClaimWarning.Visible = true;
                                        }
                                        else
                                        {
                                            pnlClaimWarning.Visible = false;
                                        }

                                        lblClaimNo.Text = txtClaimNoSearch.Text.Trim();
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.NYWCForms, gloAuditTrail.ActivityType.Modify, "Claim modified on C-4.2 Form with new value '" + lblClaimNo.Text + "'.", PatientId, iFormID, 0, gloAuditTrail.ActivityOutCome.Success, (_messageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
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

        //private void SetFieldsReadOnly()
        //{
        //    FieldIterator itr = null;
        //    Field field = null;

        //    try
        //    {
        //        _pdfview.Focus();

        //        _pdfdoc = _pdfview.GetDoc();

        //        _pdfdoc.InitSecurityHandler();

        //        for (itr = _pdfdoc.GetFieldIterator(); itr.HasNext(); itr.Next())
        //        {
        //            field = itr.Current();

        //            field.SetFlag(Field.Flag.e_read_only, true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        field = null;

        //        if (itr != null)
        //        {
        //            itr.Dispose();
        //            itr = null;
        //        }

        //    }

        //}
        private void SetFieldsReadOnly(ref PDFDoc _pdfdoc)
        {
            FieldIterator itr = null;
            Field field = null;

            try
            {


                for (itr = _pdfdoc.GetFieldIterator(); itr.HasNext(); itr.Next())
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
        private void frmWorkerCompFormViewer_C42_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flgReadOnly == false)
            {
                ObjWorkerComp.CheckLockStatusForWorkerCompForm(iFormID, false, sUserName, Environment.MachineName, ref objUserName, ref objMachineName);
            }

            ObjWorkerComp.DisconnectToPDFTron();
        }

        private void frmWorkerCompFormViewer_C42_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                if (_pdfview != null)
                { _pdfview.Dispose(); _pdfview = null; }

                if (_pdfdoc != null)
                { _pdfdoc.Dispose(); _pdfdoc = null; }

                //if (doc != null)
                //{ doc.Dispose(); doc = null; }

               
                
                appSettings = null;



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
                if (_sqlcommandMST != null) { _sqlcommandMST.Parameters.Clear(); _sqlcommandMST.Dispose(); _sqlcommandMST = null; }


                if (_sqlcommandDTL != null) { _sqlcommandDTL.Parameters.Clear(); _sqlcommandDTL.Dispose(); _sqlcommandDTL = null; }
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
                    catch (IOException ioEX)
                    { gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(), false); }
                }
                //**Clean up the Forms Saved physically on Temp Directory.  
                objC42 = null;
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
