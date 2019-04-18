using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEMRGeneralLibrary.gloEMRActors;
using gloEMRGeneralLibrary.gloEMRLab;
using gloEmdeonInterface.Classes;
using System.Collections;
using System.IO;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Wd = Microsoft.Office.Interop.Word;
using gloEmdeonCommon;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using gloWord;
using gloGlobal;




namespace gloEmdeonInterface.Forms
{
    public partial class frmViewNormalLab : Form,IHotKey 
    {

        //private enum enumTestType
        //{
        //    Lab = 0,
        //    Radiology = 1,
        //    Referrals = 2,
        //    Other = 3
        //}

        public bool _IsFinish = false;
        public delegate void SaveLabHandler();
        public event SaveLabHandler saveLabOrder;
       
        //Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
        public delegate void TaskEduHandler(Int64 nTemplateID, Int64 nPatientID, string sTemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource oType, string OpenFor, string sResourceType);
        public event TaskEduHandler EducationOrder;

        //Incident #55971: 00017037:Patient Context issue
        //Change patient provider from Lab not refresh provider combo box if open Dashboard as File>>Dashboard without closing lab main form.
        //Added delegate call when change provider is done.
        public delegate void ProviderChanged(Int64 NewProviderID);
        public event ProviderChanged OnProviderChanged;
        
      
        
        Boolean blnpriningdone = false; 


        #region  variables & properties declaration

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloUserControlLibrary.gloUC_PatientStrip gloUC_PatientStrip1 = new gloUserControlLibrary.gloUC_PatientStrip();
        private string _dataBaseConnectionString = "";
        private Int64 _LoginProviderId = 0;
        private Int64 _LoginUserID = 0;
        private long _ClinicID = 1;
        private Int64 _PatientProviderID = 0;
        private Int64 _LabProviderID = 0;
        private long _patientID = 0;

        private bool _blnClosed = false;
        private Boolean blnSaved = true;
        private bool IsClosed = false;
        private String gstrMessageBoxCaption = string.Empty;
        public ArrayList ArrLabs = new ArrayList();
        private long CurTestID = 0;
        //private Boolean blnIsAddendum = false;
        
       
        
       

        //Stores all lab test details -20130528
        private ArrayList ArrlabLst = new ArrayList();
        private WordToolStrip.gloWordToolStrip tlsOrders;
        
     //   private long TemplateId;
     //   private long TestId;
        private Hashtable TemplateHash = new Hashtable();
        public Wd.Document oCurDoc;
   
        Wd.Document oTempDoc;
        Wd.Application oWordApp;
      //  internal AxDSOFramer.AxFramerControl wdTemp;
        bool blnSignClick = false;
        private Int64 initialOrderStatus = 0;
        private int ReadyForReview = 1004; // Orderstatus number for Ready for Result review. 
        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order1 = new gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder();
        gloEDocumentV3.gloEDocV3Management oViewDocument = new gloEDocumentV3.gloEDocV3Management();


        gloEMRGeneralLibrary.clsInfobutton clsinfobutton_Orders = new gloEMRGeneralLibrary.clsInfobutton();
        private bool _IsPreferredLabCleared = false;
        
        private string _DMSCategory_Labs = "";

        private LabRequestOrderParameter _OrderParamter = new LabRequestOrderParameter();
        public LabRequestOrderParameter LabOrderParameter
        {
            get
            {
                return _OrderParamter;
            }
            set
            {
                _OrderParamter = value;
            }
        }
        public bool IsPreferredLabCleared
        {
            get { return _IsPreferredLabCleared; }
            set { _IsPreferredLabCleared = value; }
        }


        



        public object objPatientExam { get; set; }
        public object objPatientLetters { get; set; }
        public object objPatientMessages { get; set; }
        public object objNurseNotes { get; set; }
        public object objHistory { get; set; }
        public object objLabs { get; set; }
        public object objDMS { get; set; }
        public object objRxmed { get; set; }
        public object objOrders { get; set; }
        public object objProblemList { get; set; }
        public object objCriteria { get; set; }
        public object objWord { get; set; }
        public string DirectAddress { get; set; }
        public bool SecureMsgEnable { get; set; }
        public bool SecureMsgUserright { get; set; }
        //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
        public dynamic dMdi { get; set; }
        public string getOrderLabTest { get; set; }
        DataTable dtTemplateSpecility = null;
        public delegate void OpenLabEducation(Int64 nTemplateID,Int64 nPatientID, string sTemplateName,gloEMRGeneralLibrary.clsInfobutton.enumSource oType,string OpenFor,string sResourceType);
        public OpenLabEducation eventLabEducation ;

        Boolean bnlIsFaxOpened;
        public Form _MdiParent { get; set; }

        Int64 nProviderAssociationID = 0;
        string sProviderTaxID = "";
        #endregion variables & properties declaration


        #region constructor & destructor

        public frmViewNormalLab(long patientID)
        {
            InitializeComponent();
            //Code Start-Added by kanchan on 20101227 for Performance
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //Code End-Added by kanchan on 20101227 for Performance

            _patientID = patientID;

            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                _LoginUserID = Convert.ToInt64(appSettings["UserID"]);
            }

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }
            //Code Start-Added by kanchan on 20101227 for Performance
            Set_PatientDetailStrip();
            //Code End-Added by kanchan on 20101227 for Performance
        }

        #endregion constructor & desstructor

        private void frmViewNormalLab_Load(object sender, EventArgs e)
        {
            try
            {
          
                
                //Code commented & Added by kanchan on 20101227 for Performance
                // Setting Patient Strip                          
                //Set_PatientDetailStrip();
                cmbProvider.SelectedIndexChanged -= new EventHandler(cmbProvider_SelectedIndexChanged);
              
                this.gloUCLab_Transaction.Visible = false;  //not used 
                Fill_OrderStatus();
                
                //03-Jun-2013 Aniket: Do not show the New button.
                tlbbtnNew.Visible = false;
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

                //FillTests_NEW();
                FillTestsByType(gloEMRLabTest.OrderTestType.LabTests);

                //Clear All Tests 
                //*//  gloUCLab_Transaction.ClearTest();
                gloLabUC_Transaction1.IsCQMConceptDisplay = true; 

                gloLabUC_Transaction1.ClearTest();
                gloLabUC_Transaction1.ParentControl = "Record Result";
                
                //Show First Test Detail 
                gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "");

                // Get login provider id                            
                _LoginProviderId = GetProviderIDForUser(_LoginUserID);

                //Get Patient Provider ID                 
                if (_LoginProviderId == 0)
                {
                _OrderParamter.ProviderID = gloUC_PatientStrip1.ProviderID;
                }
                else
                {
                    _OrderParamter.ProviderID = _LoginProviderId;
                }
              

                // get default patient provider 
                _PatientProviderID = _OrderParamter.ProviderID;
                
                //03-Jun-2013 Aniket: Do not open the Last Order. Open the Order screen in new mode
                //code for getting last order ID of an patient , by Abhijeet on 20100624               
                //if (_OrderParamter.OrderID == 0)
                //{
                //    long lngLastOrderID = GetLastOrderId(_patientID);
                //    if (lngLastOrderID > 0)
                //        _OrderParamter.OrderID = lngLastOrderID;
                //}
                // end of code for placing smart order

                if (_OrderParamter.OrderID == 0) // placing new order
                {
                    cmbOrderStatus.SelectedValue = 1001;

                    if (GetIsCPOEOrder() == 0)
                    {
                        chkOrderNotCPOE.Checked = true;
                    }
                    else
                    {
                        chkOrderNotCPOE.Checked = false;
                    }

                    gloUCLab_OrderDetail.SetNewOrderNumber("ORD");

                    if (_LoginProviderId > 0)
                    {
                        _LabProviderID = _LoginProviderId;
                    }
                    else
                    {
                        _LabProviderID = _PatientProviderID;
                    }

                    
                    //by Abhijeet on 20100619                
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Load, "Lab request order load for placing new order", _patientID, 0, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                else  // loading for existing order
                {
                    _OrderParamter.IsEditMode = true;
                    LoadOrder();
                                        
                    //by Abhijeet on 20100619                
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.View, "Lab request order viewed for modification", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders viewed", gloAuditTrail.ActivityOutCome.Success);
                }

                //commented as per provider logic implimentation by madan.
                //if (_LoginProviderId > 0)
                //{
                //    gloUC_PatientStrip1.SetProviderName(GetProviderName(_LoginProviderId), _LoginProviderId);
                //}

                //*// gloUCLab_Transaction.TransactionType = (gloUserControlLibrary.gloUC_Transaction.enumTransactionType) _OrderParamter.TransactionType.GetHashCode();
                gloLabUC_Transaction1.TransactionType = (gloUserControlLibrary.gloLabUC_Transaction.enumTransactionType)_OrderParamter.TransactionType.GetHashCode();

               
                //*// gloUCLab_Transaction.PatientID = _patientID;
                gloLabUC_Transaction1.PatientID = _patientID;
                //Added by Mayuri:20140320-To pass Order ProviderID to fill task users against order provider and not the patient provider.
                gloLabUC_Transaction1.ProviderID = _OrderParamter.ProviderID;
                FillProviders();
                pnlLeft.Visible = true;
                //Bug #81526: 00000893 :Unable to select from dx drop down unless select the grey box first 
                if (_OrderParamter.OrderID == 0)
                {
                    gloLabUC_Transaction1.Fill_Diagnosis_CPT();
                }

              
                GetAdminSettings();

                // code for filling Smart order test in test control, by ABhijeet on 20100624
                if (ArrLabs != null)
                {
                    if (ArrLabs.Count > 0)
                    {
                        for (int i = 0; i < ArrLabs.Count; i++)
                        {
                            myList objmylst =  (myList)(ArrLabs[i]);

                            //*// gloUCLab_Transaction.AddTest(0, objmylst.ID, objmylst.Value, 1, "");
                            gloLabUC_Transaction1.AddTest(0, objmylst.ID, objmylst.Value, 1, "");
                        }
                    }
                }
                // End for filling samrt order test in test control ,by Abhijeet on 20100624
                try
                {
                    gloPatient.gloPatient.GetWindowTitle(this, _patientID, _dataBaseConnectionString, gstrMessageBoxCaption);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

                InitialiseTemplateTreeControl();
                calltoAddRefreshButtonControl();
                
                

                //Settings Value
                gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR, true);

                if (gloSettings.gloRegistrySetting.GetRegistryValue("HighLightWord") != null)
                    gloEmdeonCommon.mdlGeneral.gblnWordColorHighlight = Convert.ToBoolean(gloSettings.gloRegistrySetting.GetRegistryValue("HighLightWord"));


                if ((gloSettings.gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") != null))
                {
                    string aVal = Convert.ToString(gloSettings.gloRegistrySetting.GetRegistryValue("UseDefaultPrinter"));
                    if (aVal == "1")
                        gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = true;
                    else
                        gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = false;
                }
                else
                    gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = true;

                gloSettings.gloRegistrySetting.CloseRegistryKey();
                initialOrderStatus = Convert.ToInt64(this.cmbOrderStatus.SelectedValue);
                cmbProvider.SelectedIndexChanged += new EventHandler(cmbProvider_SelectedIndexChanged);
                pnlPlannedOrder.Visible = true;
            }
           
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading lab orders request form :" + ex.ToString(), false);
            }
 
        }

        private int GetIsCPOEOrder()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParam = null;
            DataTable dt = null;
            int IsCPOEOrder = 0;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@UserID", _LoginUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_IsCPOEOrder", oParam, out dt);
                oDB.Disconnect();

                if (dt.Rows.Count > 0)
                {
                    IsCPOEOrder = (int) dt.Rows[0][0];
                }
 
                return IsCPOEOrder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if ((oParam != null)) { oParam.Dispose();  oParam = null;}
                if ((oDB != null)) { oDB.Dispose(); oDB = null;}
                if ((dt != null)) { dt.Dispose(); dt = null; }
            }
        }

        private void FillProviders()
        {
            clsGeneral _objclsgeneral = new clsGeneral();
            DataSet dsProvider;
            DataTable dtProvider = null;
            //dtprovider = _objclsgeneral.GetProviders();
            dsProvider = GetProviders();
            dtProvider = dsProvider.Tables[0];
            cmbProvider.DataSource = dtProvider.DefaultView;
            cmbProvider.ValueMember = dtProvider.Columns["nProviderID"].ColumnName;
            cmbProvider.DisplayMember = dtProvider.Columns["ProviderName"].ColumnName;
            if (dsProvider.Tables.Count > 1 && _OrderParamter.OrderID == 0)
            {
                if (dsProvider.Tables[1].Rows.Count > 0)
                {
                    if (dsProvider.Tables[1].Rows[0][0] != null && Convert.ToString(dsProvider.Tables[1].Rows[0][0]).Trim() != "")
                    {
                        cmbProvider.SelectedValue = dsProvider.Tables[1].Rows[0][0];
                    }
                    else
                    {
                        cmbProvider.SelectedValue = _OrderParamter.ProviderID;
                    }
                }
                else
                {
                    cmbProvider.SelectedValue = _OrderParamter.ProviderID;
                }

            }
            else
            {
                cmbProvider.SelectedValue = _OrderParamter.ProviderID;
            }

            if (_objclsgeneral != null)
            {
                _objclsgeneral.Dispose();
                _objclsgeneral = null;
            }
        }
       
        private DataSet GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParam = null;
            DataSet ds = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                oDB.Connect(false);

                oParam = new gloDatabaseLayer.DBParameters();
                if (_OrderParamter.OrderID == 0)
                {
                    oParam.Add("@nProviderID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                }
                else
                {
                    oParam.Add("@nProviderID", _OrderParamter.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                }
                oParam.Add("@nPatientID", _patientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetOrderingProvider", oParam, out ds);

                oDB.Disconnect();
                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if ((oParam != null))
                {
                    oParam.Dispose();
                    oParam = null;
                }
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                //if ((dt != null))
                //{
                //    dt.Dispose();
                //    dt = null;
                //}
            }
        }
       
        private void Fill_OrderStatus()
        {
            dtTemplateSpecility = GetAllOrderStatus();
            cmbOrderStatus.DataSource = dtTemplateSpecility.DefaultView;
            cmbOrderStatus.ValueMember = dtTemplateSpecility.Columns[1].ColumnName;
            cmbOrderStatus.DisplayMember = dtTemplateSpecility.Columns[0].ColumnName;
            cmbOrderStatus.SelectedIndex = -1;
        }

        public DataTable GetAllOrderStatus()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            DataTable oResultTable = new DataTable();

            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_GetOrderStatus", out oResultTable);
                oDB.Disconnect();

                return oResultTable;

            }
            catch //(System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oResultTable != null)
                {
                    oResultTable = null;
                }
            }
        }


        private long GetLastOrderId(long patientID)
        {
            // by Abhijeet on 20100624 
            // It is function which give last order ID of an patient with which form load
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            try
            {
                string strQry = "select labom_OrderID from lab_order_mst where labom_dgloLabOrderID is null and datediff(dd,dbo.gloGetDate(),labom_OrderDate)=0 and labom_patientid=" + _patientID.ToString() + " order by labom_orderDate Desc";
                oDB.Connect(false);
                object objOrdId = oDB.ExecuteScalar_Query(strQry);
                long lngOrderID = 0;
                if (objOrdId != null && objOrdId.ToString() != "")
                    lngOrderID = Convert.ToInt64(objOrdId);
                return lngOrderID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading lab orders request form :" + ex.ToString(), false);
                return 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        public string GetProviderName(long providerID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string providerName = "";
            try
            {
                oDB.Connect(false);
                providerName = Convert.ToString(oDB.ExecuteScalar_Query("SELECT rtrim(sFirstName)+rtrim(sLastName) from provider_mst where nproviderID = " + providerID.ToString()));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                providerName = "";
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
            return providerName;
        }

        private void Set_PatientDetailStrip()
        {
            try
            {
                
                this.Controls.Add(gloUC_PatientStrip1);
                gloUC_PatientStrip1.Padding = new Padding(3, 0, 3, 0);
                gloUC_PatientStrip1.DTPEnabled = false;
                pnlToolStrip.SendToBack();
                {
                    gloUC_PatientStrip1.Dock = DockStyle.Top;
                    // Pass Paarameters Type of Form   LabOrderParameter

                    gloUC_PatientStrip1.DTPValue = LabOrderParameter.TransactionDate;
                    gloUC_PatientStrip1.ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, LabOrderParameter.VisitID, LabOrderParameter.ProviderID, false, false, false, "", false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //28-Apr-14 8020 Orders PRD: Show tests by Order Type
        //public void FillRadiologyImagingTests(Int64 PreferredID = 0)
        //{
        //    try
        //    {
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null;
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                
        //        DataTable dt = new DataTable();

        //        //DataColumn Col2 = new DataColumn("TestID");
        //        //Col2.DataType = System.Type.GetType("System.Decimal");
        //        //dt.Columns.Add(Col2);
        //        //DataColumn Col3 = new DataColumn("TestName");
        //        //Col3.DataType = System.Type.GetType("System.String");
        //        //dt.Columns.Add(Col3);

        //        dt = GetTestStructure();

        //        //When New Order Just Load all Data as preferredID =0 and When the preferred lab is cleared then Load all the Data
        //        if (oLabTests == null)
        //        {
        //            if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.RadiologyImaging);
        //            }

        //        }


        //        if (oLabTests == null)
        //        {
        //            if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
        //            {

        //                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.RadiologyImaging, oLabActor_Order.PreferredLabID);

        //            }

        //            //When the PreferredID is not 0
        //            if (PreferredID != 0)
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.RadiologyImaging, PreferredID);
        //            }

        //        }


        //        if (oLabTests.Count > 0)
        //        {
        //            DataRow row = null;
        //            //'Add data from the object to a datatable 
        //            for (int i = 0; i <= oLabTests.Count - 1; i++)
        //            {
                       
        //                gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest  = oLabTests.get_Item(i);
        //                row = dt.NewRow();

        //                row["TestName"] = oLabTest.Name;
        //                row["TestID"] = oLabTest.TestID;
        //                row["TestCodeTestName"] = oLabTest.Code + " - " + oLabTest.Name;

        //                dt.Rows.Add(row);
        //            }
        //        }
        //        GloUC_trvTest.ParentMember = null;
        //        if ((dt != null))
        //        {
        //            GloUC_trvTest.ImageIndex = 0;
        //            GloUC_trvTest.SelectedImageIndex = 0;
        //            GloUC_trvTest.DataSource = dt;
        //            GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;

        //            GloUC_trvTest.CodeMember = "TestName";
        //            GloUC_trvTest.ValueMember = "TestID";

        //            if (chkIncludeTestCode.Checked == false)
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestName";
        //            }
        //            else
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestCodeTestName";
        //            }
                    
        //            GloUC_trvTest.FillTreeView();
        //        }
        //        if (gloUCLab_OrderDetail.OrderSelected == true)
        //        {
        //            if (PreferredID != 0)
        //            {
        //                CheckAssoPreferredLab(PreferredID);
        //            }
        //        }
        //        oLabTests.Dispose();
        //        oLabTests = null;
        //        oTest.Dispose();
        //        oTest = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //28-Apr-14 8020 Orders PRD: Show tests by OthersType
        //public void FillOthers(Int64 PreferredID=0)
        //{
        //    try
        //    {
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null;
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                
        //        DataTable dt = new DataTable();

        //        //DataColumn Col2 = new DataColumn("TestID");
        //        //Col2.DataType = System.Type.GetType("System.Decimal");
        //        //dt.Columns.Add(Col2);
        //        //DataColumn Col3 = new DataColumn("TestName");
        //        //Col3.DataType = System.Type.GetType("System.String");
        //        //dt.Columns.Add(Col3);

        //        dt = GetTestStructure();

        //        //28-Apr-14 8020 Orders PRD: Show tests by OthersType

        //       if (oLabTests == null)
        //        {
        //            if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.Other);
        //            }

        //        }


        //        if (oLabTests == null)
        //        {
        //            if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
        //            {

        //                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.Other, oLabActor_Order.PreferredLabID);

        //            }

        //            //When the PreferredID is not 0
        //            if (PreferredID != 0)
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.Other, PreferredID);
        //            }

        //        }

                
        //        if (oLabTests.Count > 0)
        //        {
        //            DataRow row = null;
        //            //'Add data from the object to a datatable 
        //            for (int i = 0; i <= oLabTests.Count - 1; i++)
        //            {
                        
        //                gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest  = oLabTests.get_Item(i);
        //                row = dt.NewRow();

        //                row["TestName"] = oLabTest.Name;
        //                row["TestID"] = oLabTest.TestID;
        //                row["TestCodeTestName"] = oLabTest.Code + " - " + oLabTest.Name;

        //                dt.Rows.Add(row);
        //            }
        //        }
        //        GloUC_trvTest.ParentMember = null;
        //        if ((dt != null))
        //        {
        //            GloUC_trvTest.ImageIndex = 0;
        //            GloUC_trvTest.SelectedImageIndex = 0;
        //            GloUC_trvTest.DataSource = dt;
        //            GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;

        //            GloUC_trvTest.CodeMember = "TestName";
        //            GloUC_trvTest.ValueMember = "TestID";

        //            if (chkIncludeTestCode.Checked == false)
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestName";
        //            }
        //            else
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestCodeTestName";
        //            }

        //            GloUC_trvTest.FillTreeView();
        //        }
        //        if (gloUCLab_OrderDetail.OrderSelected == true)
        //        {
        //            if (PreferredID != 0)
        //            {
        //                CheckAssoPreferredLab(PreferredID);
        //            }
        //        }
        //        oLabTests.Dispose();
        //        oLabTests = null;
        //        oTest.Dispose();
        //        oTest = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private DataTable GetTestStructure()
        {
                DataTable dtTestStructure = new DataTable();
                DataColumn Col2 = new DataColumn("TestID");
                Col2.DataType = System.Type.GetType("System.Decimal");
                dtTestStructure.Columns.Add(Col2);

                DataColumn Col3 = new DataColumn("TestName");
                Col3.DataType = System.Type.GetType("System.String");
                dtTestStructure.Columns.Add(Col3);

                DataColumn Col4 = new DataColumn("TestCodeTestName");
                Col4.DataType = System.Type.GetType("System.String");
                dtTestStructure.Columns.Add(Col4);

                return dtTestStructure;
        }


        public void FillTestsByType(gloEMRLabTest.OrderTestType TestType, Int64 PreferredID = 0)
        {
            try
            {
                gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null;
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                DataTable dtTests = new DataTable();

                dtTests = GetTestStructure();

                
                if (oLabTests == null)
                {
                    if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
                    {
                        oLabTests = oTest.GetTestsByType(TestType);
                    }

                }


                if (oLabTests == null)
                {
                    if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
                    {
                        oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
                        oLabTests = oTest.GetTestsByType(TestType, oLabActor_Order.PreferredLabID);
                    }

                    if (PreferredID != 0)
                    {
                        oLabTests = oTest.GetTestsByType(TestType, PreferredID);
                    }

                }
                
                if (oLabTests != null)
                {
                    if (oLabTests.Count > 0)
                    {
                        DataRow row = null;
                        
                        for (int i = 0; i <= oLabTests.Count - 1; i++)
                        {
                            gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest = oLabTests.get_Item(i);
                            if (oLabTest != null)
                            {
                                row = dtTests.NewRow();

                                row["TestName"] = oLabTest.Name;
                                row["TestID"] = oLabTest.TestID;
                                row["TestCodeTestName"] = oLabTest.Code + " - " + oLabTest.Name;

                                dtTests.Rows.Add(row);
                            }
                        }
                    }
                }

                GloUC_trvTest.ParentMember = null;
                if ((dtTests != null))
                {

                    GloUC_trvTest.ImageIndex = 0;
                    GloUC_trvTest.SelectedImageIndex = 0;
                    GloUC_trvTest.DataSource = dtTests;
                    GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
                    GloUC_trvTest.CodeMember = "TestName";
                    GloUC_trvTest.ValueMember = "TestID";

                    if (chkIncludeTestCode.Checked == false)
                    {
                        GloUC_trvTest.DescriptionMember = "TestName";
                    }
                    else
                    {
                        GloUC_trvTest.DescriptionMember = "TestCodeTestName";
                    }

                    GloUC_trvTest.FillTreeView();
                }
                if (gloUCLab_OrderDetail.OrderSelected == true)
                {
                    if (PreferredID != 0)
                    {
                        CheckAssoPreferredLab(PreferredID);
                    }
                }

                if (oLabTests != null)
                {
                    oLabTests.Dispose();
                    oLabTests = null;
                }
                oTest.Dispose();
                oTest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //public void FillTests_NEW(Int64 PreferredID = 0)
        //{
        //    try
        //    {
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null;
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;

        //        DataTable dt = new DataTable();
        //        //DataColumn Col2 = new DataColumn("TestID");
        //        //Col2.DataType = System.Type.GetType("System.Decimal");
        //        //dt.Columns.Add(Col2);

        //        //DataColumn Col3 = new DataColumn("TestName");
        //        //Col3.DataType = System.Type.GetType("System.String");
        //        //dt.Columns.Add(Col3);

        //        //DataColumn Col4 = new DataColumn("TestCodeTestName");
        //        //Col4.DataType = System.Type.GetType("System.String");
        //        //dt.Columns.Add(Col4);

        //        dt = GetTestStructure();

        //        //When New Order Just Load all Data as preferredID =0 and When the preferred lab is cleared then Load all the Data
        //        if (oLabTests == null)
        //        {
        //            if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.LabTests);
        //            }

        //        }


        //        if (oLabTests == null)
        //        {
        //            if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
        //            {
        //                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.LabTests, oLabActor_Order.PreferredLabID);
        //            }

        //            //When the PreferredID is not 0
        //            if (PreferredID != 0)
        //            {
        //                oLabTests = oTest.GetTestsByType(gloEMRLabTest.OrderTestType.LabTests, PreferredID);
        //            }

        //        }
        //        //When Modify _OrderParamter.IsEditMode == true, PreferredID == 0 So we Take the PreferredID from oLabTests

        //        if (oLabTests != null)
        //        {
        //            if (oLabTests.Count > 0)
        //            {
        //                DataRow row = null;
        //                //'Add data from the object to a datatable 
        //                for (int i = 0; i <= oLabTests.Count - 1; i++)
        //                {
        //                    // TreeNode trvnode = new TreeNode();
        //                    gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest = oLabTests.get_Item(i);
        //                    if (oLabTest != null)
        //                    {
        //                        row = dt.NewRow();

        //                        row["TestName"] = oLabTest.Name;
        //                        row["TestID"] = oLabTest.TestID;
        //                        row["TestCodeTestName"] = oLabTest.Code + " - " + oLabTest.Name;

        //                        dt.Rows.Add(row);
        //                    }
        //                }
        //            }
        //        }

        //        GloUC_trvTest.ParentMember = null;
        //        if ((dt != null))
        //        {

        //            GloUC_trvTest.ImageIndex = 0;
        //            GloUC_trvTest.SelectedImageIndex = 0;
        //            GloUC_trvTest.DataSource = dt;
        //            GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
        //            GloUC_trvTest.CodeMember = "TestName";
        //            GloUC_trvTest.ValueMember = "TestID";

        //            if (chkIncludeTestCode.Checked == false)
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestName";
        //            }
        //            else
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestCodeTestName";
        //            }

        //            GloUC_trvTest.FillTreeView();
        //        }
        //        if (gloUCLab_OrderDetail.OrderSelected == true)
        //        {
        //            if (PreferredID != 0)
        //            {
        //                CheckAssoPreferredLab(PreferredID);
        //            }
        //        }

        //        if (oLabTests != null)
        //        {
        //            oLabTests.Dispose();
        //            oLabTests = null;
        //        }
        //        oTest.Dispose();
        //        oTest = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public Int64 GetProviderIDForUser(Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Int64 ProID = 0;
            try
            {
                oDB.Connect(false);
                ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " + UserID + ""));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ProID = 0;

            }
            finally
            {
                oDB.Dispose();
            }
            return ProID;
        }


        public void LoadOrder()
        {
            try
            {
                //#---LOAD DETAILS---# 

                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                
                //Assign Actor to Object 
                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);

                if ((oLabActor_Order != null))
                {
                    //Clear All Tests 
                    gloLabUC_Transaction1.ClearTest();
                  
                    cmbOrderStatus.SelectedValue = oLabActor_Order.OrderStatusNumber;
                    chkOrderNotCPOE.Checked = oLabActor_Order.blnOrderNotCPOE;
                    chkOutboundTransition.Checked = oLabActor_Order.bOutboundTransistion;

                    if (oLabActor_Order.FastingStatus == "FASTING")
                    {
                        chkFasting.Checked = true;
                    }
                    else 
                    {
                        chkFasting.Checked = false;
                    }

                    //Show Order Detail 

                    //Developer:Sanjog Dhamke
                    //Date: 12/07/2011
                    //Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
                    //Reason: OrderNoID is converted to int16 format but actual OrderNoID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.

                    gloUCLab_OrderDetail.SetData(oLabActor_Order.OrderNoPrefix, Convert.ToInt64(oLabActor_Order.OrderNoID), oLabActor_Order.PreferredLab, oLabActor_Order.ReferredBy, oLabActor_Order.SampledBy, null, oLabActor_Order.PreferredLabID, oLabActor_Order.ReferredByID, oLabActor_Order.SampledByID, "",
                    oLabActor_Order.TaskDueDate, oLabActor_Order.ReferredToID, oLabActor_Order.ReferredTo, oLabActor_Order.SendTo);

                    //Show First Test Detail 
                    ////--Remark--// 
                    //----------------------------------------------------------- 
                    //Assign Values to Order Object 

                    {
                    

                        //Code commented by Abhijeet because for modify order current date time was not assigning to patient strip
                        //End of changes by Abhijeet on 20110418 for assinging current date & time to patient strip control,

                        this.gloUC_PatientStrip1.Provider = oLabActor_Order.Provider;
                        
                        _OrderParamter.ProviderID = oLabActor_Order.ProviderID;
                        _LabProviderID = oLabActor_Order.ProviderID;

                        
                        string _Provider = null;
                        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo oLabActorContact = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo();

                        DataTable oDataTable =   oLabActorContact.GetProviderName(_OrderParamter.ProviderID);
                        oLabActorContact.Dispose();
                        oLabActorContact = null;
                        _Provider = "";
                        if ((oDataTable != null))
                        {
                            if (oDataTable.Rows.Count > 0)
                            {
                                if (!(oDataTable.Rows[0]["sFirstName"] == System.DBNull.Value))
                                {
                                    _Provider = oDataTable.Rows[0]["sFirstName"] + "";
                                }
                                if (!(oDataTable.Rows[0]["sMiddleName"] == System.DBNull.Value))
                                {
                                    _Provider = _Provider + " " + oDataTable.Rows[0]["sMiddleName"] + "";
                                }
                                if (!(oDataTable.Rows[0]["sLastName"] == System.DBNull.Value))
                                {
                                    _Provider = _Provider + " " + oDataTable.Rows[0]["sLastName"] + "";
                                }
                            }
                        }
                        gloUC_PatientStrip1.SetProviderName(_Provider, _OrderParamter.ProviderID);

                        
                        gloLabUC_Transaction1.SetData(oLabActor_Order, "emr");
                    }
                }
                if (oLabActor_Order != null)
                {
                    oLabActor_Order.Dispose();
                    oLabActor_Order = null;
                }
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name == this.tlbbtnNew.Name)
                {

                    
                    if (gloLabUC_Transaction1.LabModified)
                    {
                    
                        DialogResult drResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (drResult == DialogResult.Yes)
                        {
                            _blnClosed = true;
                            IsClosed = true;
                            SaveOrder(0);
                        }
                        else if (drResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    MenuEvent_New();
                }
                else if (e.ClickedItem.Name == this.tlbbtnSave.Name)
                {
                    if (!getProviderTaxID(_LabProviderID))
                    {
                        return;
                    } 
                    _blnClosed = true;
                    
                    MenuEvent_Save(0);
                    gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(_LabProviderID);
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _OrderParamter.OrderID, sProviderTaxID, _LabProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.OrderAndResult);
                    oclsselectProviderTaxID = null;
                    if (blnSaved == true)
                    {
                        this.Close();
                    }

                    
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is successfully saved", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    bool IsUpdated = AutoOrderStatusUpdateforReview(_OrderParamter.OrderID, Convert.ToInt64(this.cmbOrderStatus.SelectedValue));
                }
                else if (e.ClickedItem.Name == this.tlbbtnClose.Name)
                {
                    _blnClosed = true;
                    //*// if (gloUCLab_Transaction.LabModified)
                    if (gloLabUC_Transaction1.LabModified)
                    {
                        DialogResult oResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (oResult == DialogResult.Yes)
                        {
                            if (!getProviderTaxID(_LabProviderID))
                            {
                                return;
                            } 
                            IsClosed = true;
                            SaveOrder(0);
                            gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(_LabProviderID);
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _OrderParamter.OrderID, sProviderTaxID, _LabProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.OrderAndResult);
                            oclsselectProviderTaxID = null;
                            //15-Oct-15 Aniket: Resolving Bug #90339: Order Entry :- Application not allow user to remove test from lab order
                            if (blnSaved == true)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is successfully saved & closed", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                               this.Close();
                            }
                        }
                        else if (oResult == DialogResult.No)
                        {
                            //by Abhijeet on 20100619  
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request orders closed without saving", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            this.Close();
                        }
                        else if (oResult == DialogResult.Cancel)
                        {
                            _blnClosed = false;
                        }
                    }
                    else
                    {
                        //by Abhijeet on 20100619               
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request orders closed without saving", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab orders closed", _patientID, 0, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Modified lab orders not saved because of no result available ", oLabActor_Order1.PatientID, oLabActor_Order1.OrderID, oLabActor_Order1.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        public bool getProviderTaxID(Int64 nProviderID = 0)
        {
            sProviderTaxID = "";
            nProviderAssociationID = 0;
            try
            {
                DialogResult oResult = System.Windows.Forms.DialogResult.OK;
                gloGlobal.frmSelectProviderTaxID oForm = new gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID));
                if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count > 1)
                {
                    oForm.ShowDialog(this);
                    oResult = oForm.DialogResult;
                    nProviderAssociationID = oForm.nAssociationID;
                    sProviderTaxID = oForm.sProviderTaxID;

                    oForm = null;
                }
                else if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count == 1)
                {
                    //'oResult = oForm.DialogResult
                    nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows[0]["nAssociationID"]);
                    sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows[0]["sTIN"]);
                    oForm = null;
                }
                else
                {
                    nProviderAssociationID = 0;
                    sProviderTaxID = "";
                }

                if (oResult == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;

            }
            finally
            {
            }
        }
        private void MenuEvent_New()
        {
            try
            {
                _OrderParamter.IsEditMode = false;
                _OrderParamter.OrderID = 0;
                _OrderParamter.OrderNumberID = 0;
                _OrderParamter.OrderNumberPrefix = "ORD";
                _OrderParamter.PatientID = _patientID;
                _OrderParamter.VisitID = GenerateVisitID(DateTime.Now);
                _OrderParamter.TransactionDate = DateTime.Now;
                _OrderParamter.CloseAfterSave = true;

                //Show Patient Detail 
                //gloUC_PatientStrip1.ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, LabOrderParameter.VisitID, LabOrderParameter.ProviderID, false, false, false, "", false);

                //' Set Tranasaction Date 
                this.gloUC_PatientStrip1.DTPValue = DateTime.Now;
                //' 'Show Order Detail 
                gloUCLab_OrderDetail.SetData("", 0, "", "", "", null, 0, 0, 0, "", DateTime.Now.Date,0,"",1);
                //Show Order Detail
                gloUCLab_OrderDetail.SetNewOrderNumber("ORD");
                //Show Test Detail
                gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "");

                //Clear All Tests
                //*//gloUCLab_Transaction.ClearTest();
                gloLabUC_Transaction1.ClearTest();

                //by Abhijeet on 20100619                
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Lab Request Orders viewed for placing new order", _patientID, 0, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void MenuEvent_Save(Int16 IsFinished)
        {

            string _sLoginProviderName = string.Empty;
            string _sPatientProvider = string.Empty;
            try
            {

                _sPatientProvider = GetProviderName(_PatientProviderID);
                _sLoginProviderName = GetProviderName(_LoginProviderId);

                //'check Provider of patient and order is same 
                Int16 _IsFinished = IsFinished;
                if (_LoginProviderId == 0)
                {
                    
                        SaveOrder(_IsFinished, _PatientProviderID);
                    

                }
                    
                else if (_LoginProviderId == _PatientProviderID)
                {
                    SaveOrder(_IsFinished, _LoginProviderId);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public bool changePatientProvider(Int64 nPatientProviderID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string strQry = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                if (nPatientProviderID != 0)
                {
                    oDB.Connect(false);

                    strQry = "update Patient set nProviderID='" + nPatientProviderID + "' where nPatientID='" + nPatientID + "' ";
                    _Result = oDB.Execute_Query(strQry);
                    if (_Result < 0)
                        _boolResult = false;
                    else if (_Result >= 0)
                        _boolResult = true;
                    else
                        _boolResult = false;
                    oDB.Disconnect();
                }
                //Incident #55971: 00017037:Patient Context issue
                //Change patient provider from Lab not refresh provider combo box if open Dashboard as File>>Dashboard without closing lab main form.
                if (OnProviderChanged != null)
                {
                    if (nPatientProviderID != _PatientProviderID)
                    {
                        //this event pass changed patient provider (nPatientProviderID) to frmViewgloLab.
                        OnProviderChanged(nPatientProviderID);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            return _boolResult;
        }




        private void SaveOrder(Int16 IsFinished)
        {
            gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();
            try
            {
                //Assign Actor to Object

                gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTests item = oLabOrderRequest.LabOrder.OrderTests;
                foreach (gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest item1 in item)
                {
                    item1.Is_Finished = gloLabUC_Transaction1.GetTemplateIsFinished(CurTestID);
                    if (item1.Is_Finished == true)
                    {

                        tlbbtn_Finish.Enabled = false;
                    }
                }


                oLabOrderRequest.LabOrder = oLabActor_Order1;

                _OrderParamter.CloseAfterSave = true;


                if (_OrderParamter.IsEditMode == false)
                {
                    oLabActor_Order1.OrderID = 0;
                }
                else
                {
                    oLabActor_Order1.OrderID = _OrderParamter.OrderID;
                }

                gloUCLab_OrderDetail.ReadData();
                oLabActor_Order1.OrderNoPrefix = gloUCLab_OrderDetail.OrderNumberPrefix;
                oLabActor_Order1.OrderNoID = gloUCLab_OrderDetail.OrderNumberID;
                oLabActor_Order1.PreferredLab = gloUCLab_OrderDetail.PreferredLab;
                oLabActor_Order1.PreferredLabID = gloUCLab_OrderDetail.PreferredLabID;
                oLabActor_Order1.SampledBy = gloUCLab_OrderDetail.SampledBy;
                oLabActor_Order1.SampledByID = gloUCLab_OrderDetail.SampledByID;
                oLabActor_Order1.ReferredBy = gloUCLab_OrderDetail.ReferredBy;
                oLabActor_Order1.ReferredByID = gloUCLab_OrderDetail.ReferredByID;
                oLabActor_Order1.Users = gloUCLab_OrderDetail.Users;

                oLabActor_Order1.TaskDescription = gloUCLab_OrderDetail.TaskDescription;
                oLabActor_Order1.TaskDueDate = gloUCLab_OrderDetail.TaskDueDate;

                oLabActor_Order1.ReferredByFName = gloUCLab_OrderDetail.ReferredByFName;
                oLabActor_Order1.ReferredByMName = gloUCLab_OrderDetail.ReferredByMName;
                oLabActor_Order1.ReferredByLName = gloUCLab_OrderDetail.ReferredByLName;

                oLabActor_Order1.TransactionDate = gloUC_PatientStrip1.TransactionDate;
                oLabActor_Order1.PatientID = _patientID;
                try
                {
                    oLabActor_Order1.VisitID = GenerateVisitID(oLabActor_Order1.TransactionDate);
                }
                catch
                {
                }
                
                oLabActor_Order1.PatientAge.Years = gloUC_PatientStrip1.PatientAge.Years;
                oLabActor_Order1.PatientAge.Months = gloUC_PatientStrip1.PatientAge.Months;
                if ((gloUC_PatientStrip1.PatientAge.Years == 0 && gloUC_PatientStrip1.PatientAge.Months == 0
                    && gloUC_PatientStrip1.PatientAge.Days == 0) && (gloUC_PatientStrip1.PatientAge.Hours != 0))
                {
                    oLabActor_Order1.PatientAge.Days = Convert.ToInt16(gloUC_PatientStrip1.PatientAge.Hours / 24);
                }
                else
                {
                    oLabActor_Order1.PatientAge.Days = gloUC_PatientStrip1.PatientAge.Days;
                }
                //oLabActor_Order1.PatientAge.Days = gloUC_PatientStrip1.PatientAge.Days;
                oLabActor_Order1.PatientAge.Age = gloUC_PatientStrip1.PatientAge.Age;
                oLabActor_Order1.Provider = cmbProvider.Text;
                oLabActor_Order1.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);

                //29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
                oLabActor_Order1.OrderCreatorUser = appSettings["UserName"];  
                

                if (this.cmbOrderStatus.SelectedValue != null)
                {
                    oLabActor_Order1.OrderStatusNumber = (int)this.cmbOrderStatus.SelectedValue;
                }
                oLabActor_Order1.blnOrderNotCPOE = this.chkOrderNotCPOE.Checked;
                oLabActor_Order1.bOutboundTransistion = this.chkOutboundTransition.Checked;


                if (chkFasting.Checked == true)
                {
                    oLabActor_Order1.FastingStatus = "FASTING";
                }
                else
                {
                    oLabActor_Order1.FastingStatus = "";
                }

                oLabActor_Order1.DMSID = 0;

                //Lab Order Master - Test Details

                //*//oLabActor_Order1.OrderTests = gloUCLab_Transaction.GetData();
                oLabActor_Order1.OrderTests = gloLabUC_Transaction1.GetData();

                //oLabActor_Order1.ArrTestName = gloUCLab_Transaction.arrTestNames;
                //*//oLabActor_Order1.ArrTestName = gloUCLab_Transaction.arrTestNames;
                oLabActor_Order1.ArrTestName = gloLabUC_Transaction1.arrTestNames;


                if (oLabActor_Order1.ArrTestName != null)
                {
                    myList olist = null;
                   // int DMTest = -1;
                    for (int i = 0; i <= oLabActor_Order1.ArrTestName.Count - 1; i++)
                    {
                        if (ArrLabs != null)
                        {
                            for (int j = 0; j <= ArrLabs.Count - 1; j++)
                            {
                                olist = (myList) ArrLabs[j];
                                if (olist.DMTemplateName == oLabActor_Order1.ArrTestName[i].ToString())
                                {
                                    olist.IsFinished = true;
                                    olist = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                //if (IsPrint == false) //removed by madan on 20100422
                //{
                if ((oLabActor_Order1.OrderTests == null) == true)
                {
                    if (IsClosed == false)
                    {
                        MessageBox.Show("Please select at least one test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _OrderParamter.CloseAfterSave = false;
                        blnSaved = false;
                        return;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Modified lab request orders not saved because of no result available ", oLabActor_Order1.PatientID, oLabActor_Order1.OrderID, oLabActor_Order1.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        MessageBox.Show("Order cannot be saved. At least one test should be associated to the order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        blnSaved = false;
                        return;
                    }
                }

                if (_OrderParamter.IsEditMode == false)
                {
                    _OrderParamter.OrderID = oLabOrderRequest.Add(IsFinished);
                    // Updating Order date 
                    UpdateOrderDate(_OrderParamter.OrderID);

                    //by Abhijeet on 20100619                
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab request orders Added successfully", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Request Orders added.  ", gloAuditTrail.ActivityOutCome.Success);
                }
                else
                {
                    oLabOrderRequest.Modify(_OrderParamter.OrderID, IsFinished);
                    //by Abhijeet on 20100619                
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Modify, "Lab request  orders modified successfully", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab orders modified successfully", _patientID, _OrderParamter.OrderID, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }

                long TaskID = 0;
              //  TaskID = GetTaskID_OfLab(_OrderParamter.OrderID);
                RemoveExistingTaskUsers();

                if (oLabActor_Order1.Users.Count > 0)
                {
                    AddTasks(oLabActor_Order1.Users, oLabActor_Order1.TransactionDate, oLabActor_Order1.PatientID, TaskID, oLabActor_Order1.TaskDescription, oLabActor_Order1.TaskDueDate,oLabActor_Order1 );
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Added task for Lab request orders modified", oLabActor_Order1.PatientID, TaskID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                //code added by pradeep 20101026
                if (saveLabOrder != null)
                    saveLabOrder();
                
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;
                blnSaved = true;

                //Developer:Sanjog Dhamke
                //Date: 9 Feb 2012
                //Bug ID: 18826 To update the result flag after save
                //Reason: To call function for update the result flag
                UpdateResultFlag();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                blnSaved = false;
            }
        }


        public void SaveLabOrder_Handler(object sender, EventArgs e)
        {
        }
        public void SaveOrder(Int16 IsFinished, Int64 NewProviderID)
        {
            gloEMRLabOrder oLabOrderRequest = new gloEMRLabOrder();

            try
            {
                //Assign Actor to Object
                oLabOrderRequest.LabOrder = oLabActor_Order1;
                gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTests item = oLabOrderRequest.LabOrder.OrderTests;
                foreach (gloEMRGeneralLibrary.gloEMRActors.LabActor.OrderTest item1 in item)
                {
                    item1.Is_Finished = gloLabUC_Transaction1.GetTemplateIsFinished(item1.TestID);

                    if (item1.Is_Finished == true)
                    {
                        tlbbtn_Finish.Enabled = false;
                    }


                }

                _OrderParamter.CloseAfterSave = true;

                //Assign Values to Order Object

                if (_OrderParamter.IsEditMode == false)
                {
                    oLabActor_Order1.OrderID = 0;
                }
                else
                {
                    oLabActor_Order1.OrderID = _OrderParamter.OrderID;
                }

                //Data From Order Detail User Control
                gloUCLab_OrderDetail.ReadData();
                oLabActor_Order1.OrderNoPrefix = gloUCLab_OrderDetail.OrderNumberPrefix;
                oLabActor_Order1.OrderNoID = gloUCLab_OrderDetail.OrderNumberID;
                oLabActor_Order1.PreferredLab = gloUCLab_OrderDetail.PreferredLab;
                oLabActor_Order1.PreferredLabID = gloUCLab_OrderDetail.PreferredLabID;
                oLabActor_Order1.SampledBy = gloUCLab_OrderDetail.SampledBy;
                oLabActor_Order1.SampledByID = gloUCLab_OrderDetail.SampledByID;
                oLabActor_Order1.ReferredBy = gloUCLab_OrderDetail.ReferredBy;
                oLabActor_Order1.ReferredByID = gloUCLab_OrderDetail.ReferredByID;
                oLabActor_Order1.ReferredToID = gloUCLab_OrderDetail.ReferredToID;
                oLabActor_Order1.SendTo = gloUCLab_OrderDetail.SendTo;
                oLabActor_Order1.Users = gloUCLab_OrderDetail.Users;

                oLabActor_Order1.TaskDescription = gloUCLab_OrderDetail.TaskDescription;
                oLabActor_Order1.TaskDueDate = gloUCLab_OrderDetail.TaskDueDate;

                oLabActor_Order1.ReferredByFName = gloUCLab_OrderDetail.ReferredByFName;
                oLabActor_Order1.ReferredByMName = gloUCLab_OrderDetail.ReferredByMName;
                oLabActor_Order1.ReferredByLName = gloUCLab_OrderDetail.ReferredByLName;

                oLabActor_Order1.TransactionDate = this.gloUC_PatientStrip1.TransactionDate;
                oLabActor_Order1.PatientID = _patientID;
                try
                {
                    oLabActor_Order1.VisitID = GenerateVisitID(oLabActor_Order1.TransactionDate);
                }
                catch
                {
                }
                oLabActor_Order1.PatientAge.Years = this.gloUC_PatientStrip1.PatientAge.Years;
                oLabActor_Order1.PatientAge.Months = this.gloUC_PatientStrip1.PatientAge.Months;
                if ((this.gloUC_PatientStrip1.PatientAge.Years == 0 && this.gloUC_PatientStrip1.PatientAge.Months == 0
                    && this.gloUC_PatientStrip1.PatientAge.Days == 0) && (this.gloUC_PatientStrip1.PatientAge.Hours != 0))
                {
                    oLabActor_Order1.PatientAge.Days = Convert.ToInt16(this.gloUC_PatientStrip1.PatientAge.Hours / 24);
                }
                else
                {
                    oLabActor_Order1.PatientAge.Days = this.gloUC_PatientStrip1.PatientAge.Days;
                }
                oLabActor_Order1.PatientAge.Age = this.gloUC_PatientStrip1.PatientAge.Age;

                //29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
                oLabActor_Order1.OrderCreatorUser = appSettings["UserName"];


                 oLabActor_Order1.Provider = this.cmbProvider.Text ;
                if (this.cmbOrderStatus.SelectedValue != null)
                {
                    oLabActor_Order1.OrderStatusNumber = (int)this.cmbOrderStatus.SelectedValue;
                }
                oLabActor_Order1.blnOrderNotCPOE = this.chkOrderNotCPOE.Checked;
                
                oLabActor_Order1.bOutboundTransistion = this.chkOutboundTransition.Checked;

                if (chkFasting.Checked == true)
                {
                    oLabActor_Order1.FastingStatus = "FASTING";
                }
                else 
                {
                    oLabActor_Order1.FastingStatus = "";
                }

                //commeneted by Mayuri:20140507-no need to set order to patient provider as we are giving ordering provider filter
                //if (NewProviderID == 0)
                //{
                //    oLabActor_Order1.ProviderID = (long)this.cmbProvider.SelectedValue;
                //}
                //else
                //{
                //    oLabActor_Order1.ProviderID = NewProviderID;
                //}


                oLabActor_Order1.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
                oLabActor_Order1.DMSID = 0;

                oLabActor_Order1.OrderTests = gloLabUC_Transaction1.GetData();
                oLabActor_Order1.ArrTestName = gloLabUC_Transaction1.arrTestNames;
               

                if (oLabActor_Order1.ArrTestName != null)
                {
                    myList olist = null;
                 //   int DMTest = -1;
                    for (int i = 0; i <= oLabActor_Order1.ArrTestName.Count - 1; i++)
                    {
                       
                        if (ArrLabs != null)
                        {
                            for (int j = 0; j <= ArrLabs.Count - 1; j++)
                            {
                                olist = (myList)ArrLabs[j];
                                if (olist.DMTemplateName == oLabActor_Order1.ArrTestName[i].ToString())
                                {
                                    olist.IsFinished = true;
                                    olist = null;
                                    break;
                                }
                            }
                        }
                    }
                }


                if ((oLabActor_Order1.OrderTests == null) == true)
                {
                    if (IsClosed == false)
                    {
                        MessageBox.Show("Please select at least one test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _OrderParamter.CloseAfterSave = false;
                        blnSaved = false;
                        return;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Modified lab request orders not saved because of no result available ", oLabActor_Order1.PatientID, oLabActor_Order1.OrderID, oLabActor_Order1.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                        MessageBox.Show("Order cannot be saved, At least one test should be associated to the order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }


                if (_OrderParamter.IsEditMode == false)
                {
                    _OrderParamter.OrderID = oLabOrderRequest.Add(IsFinished);
                    // Updating Order date 
                    UpdateOrderDate(_OrderParamter.OrderID);
                    
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab request orders Added successfully", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    
                }
                else
                {
                    oLabOrderRequest.Modify(_OrderParamter.OrderID, IsFinished);
                    
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Modify, "Lab request orders modified successfully", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    
                }

                long TaskID = 0;
               // TaskID = GetTaskID_OfLab(_OrderParamter.OrderID);
                //Added by Mayuri:20140324-dont send task to user in modify mode if already sent,except it is completed.
                RemoveExistingTaskUsers();
               
                if (oLabActor_Order1.Users.Count > 0)
                {
                    AddTasks(oLabActor_Order1.Users, oLabActor_Order1.TransactionDate, oLabActor_Order1.PatientID, TaskID, oLabActor_Order1.TaskDescription, oLabActor_Order1.TaskDueDate, oLabActor_Order1);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Added task for Lab orders modified", oLabActor_Order1.PatientID, TaskID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                //code added by pradeep 20101026
                if (saveLabOrder != null)
                    saveLabOrder();
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;
                blnSaved = true;

                //Developer:Sanjog Dhamke
                //Date: 9 Feb 2012
                //Bug ID: 18826 To update the result flag after save
                //Reason: To call Function for update the result flag
                UpdateResultFlag();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                blnSaved = false;
            }
        }


        private void UpdateOrderDate(long orderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            try
            {
                string strQry = "update lab_order_mst set labom_orderDate= labom_transactionDate where labom_orderid=" + orderID.ToString();
                oDB.Connect(false);
                oDB.Execute_Query(strQry);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
        }

        private void RemoveExistingTaskUsers()
        {
            DataTable dttaskdetail;
            DataView dv = null;
            int k;
            clsGeneral _objgeneral = new clsGeneral();
            dttaskdetail = _objgeneral.GetTaskDetail_OfLab(_OrderParamter.OrderID);

            if (oLabActor_Order1.Users.Count > 0)
            {
                for (k = oLabActor_Order1.Users.Count - 1; k >= 0; k--)
                {
                    if (dttaskdetail != null)
                    {
                        if (dttaskdetail.Rows.Count > 0)
                        {
                            dv = dttaskdetail.DefaultView;
                            dv.RowFilter = "nAssigntoID=" + oLabActor_Order1.Users.get_Item(k).ID;
                            if (dv.Count > 0)
                            {
                                oLabActor_Order1.Users.RemoveAt(k);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
            }
            if (dttaskdetail != null)
            {
                dttaskdetail.Dispose();
                dttaskdetail = null;
            }
            if (dv != null)
            {
                dv.Dispose();
                dv = null;
            }
            if (_objgeneral != null)
            {
                _objgeneral.Dispose();
                _objgeneral = null;
            }

        }
        private long GenerateVisitID(DateTime DtTransDate)
        {    // function used to generate the Visit ID for order.

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDBParameters.Add("@nPatientID", _patientID, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@dtVisitdate", DtTransDate, ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                long lngAppointID = 0;
                oDBParameters.Add("@AppointmentID", lngAppointID, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                long lngTransId = 0;
                lngTransId = GetPrefixTransactionID(_patientID);
                oDBParameters.Add("@MachineID", lngTransId, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@VisitID", 0, ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt, 18);

                oDBParameters.Add("@flag", 0, ParameterDirection.Output, System.Data.SqlDbType.Int);

                oDB.Connect(false);
                long lngVisitId = 0;
                object tmpVisitId;
                object tmpFlag;
                oDB.Execute("gsp_InsertVisits", oDBParameters, out tmpVisitId, out tmpFlag);
                oDB.Disconnect();

                if (tmpVisitId.ToString() == "" || tmpVisitId == null)
                    lngVisitId = 0;
                else
                    lngVisitId = Convert.ToInt64(tmpVisitId);

                return lngVisitId;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL19 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL20 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);

            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";

                    oDB.Connect(false);
                    _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                    oDB.Disconnect();

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }

                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

                return _Result;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
                    
            }
        }


        public Int64 GetTaskID_OfLab(Int64 LabOrderID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Int64 LabID = 0;
            string strQry = string.Empty;
            object _objResult = new object();
            try
            {
                oDB.Connect(false);
                strQry = "SELECT nTaskID FROM TM_TaskMST WHERE nReferenceID1 = " + LabOrderID;
                _objResult = oDB.ExecuteScalar_Query(strQry);
                if (_objResult != null && !_objResult.Equals(""))
                {
                    LabID = Convert.ToInt64(_objResult);
                }
                else
                {
                    LabID = 0;
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                LabID = 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQry = string.Empty;
                _objResult = null;
            }
            return LabID;
        }
  


     
        private void AddTasks(gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetails Users, DateTime TaskDate, long PatientID, long TaskId, string TaskDesc, DateTime TaskDueDate,gloEMRGeneralLibrary .gloEMRActors .LabActor.LabOrder _LabOrder)
        {
            gloTaskMail.gloTask ogloTask = new gloTaskMail.gloTask(_dataBaseConnectionString);
            gloTaskMail.Task oTask = new gloTaskMail.Task();
            gloTaskMail.TaskProgress oTaskProgress = new gloTaskMail.TaskProgress();

            try
            {
                Boolean _isNormal = true;  //Variables Added by Mayuri:20140320- Flag use to check normal and abnormal results
                Boolean _isOrderwithTests = true;//Variables Added by Mayuri:20140320- Flag use to check Order without results

                Int64[] ArrTasks = new Int64[Users.Count];
                int i = 0;




                for (i = 0; i <= Users.Count - 1; i++)
                {

                    gloTaskMail.TaskAssign oTaskAssign = new gloTaskMail.TaskAssign();

                    oTaskAssign.AssignFromID = _LoginUserID;
                    oTaskAssign.AssignFromName = GetLoginUserName(_LoginUserID);
                    oTaskAssign.AssignToID = Users.get_Item(i).ID;
                    if (oTaskAssign.AssignFromID == oTaskAssign.AssignToID)
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    }
                    else
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                    }
                    oTaskAssign.AssignToName = Users.get_Item(i).Description;
                    oTaskAssign.ClinicID = _ClinicID;

                    oTask.Assignment.Add(oTaskAssign);
                }

                oTaskProgress.ClinicID = _ClinicID;
                oTaskProgress.Complete = 0;
                oTaskProgress.DateTime = TaskDate;
                oTaskProgress.Description = TaskDesc;
                oTaskProgress.StatusID = 1;

                oTaskProgress.TaskID = TaskId;

                oTask.TaskID = TaskId;
                oTask.UserID = _LoginUserID;
                oTask.TaskType = gloTaskMail.TaskType.LabOrder;
                oTask.PatientID = PatientID;
                //' oTask.Subject = TaskDesc'' Task Subject is same as Task Description in LabOrders 


                //Subject changes Added by Mayuri:20140320-To check whther order contains abnormal result
                //if order contains abnormal result then send task subject as abnormal with priority high else priority low.

                if (oLabActor_Order1.ArrTestName != null)
                {
                    //for (int j = 0; j <= oLabActor_Order1.ArrTestName.Count - 1; j++)
                    //{
                    if (oLabActor_Order1.OrderTests.Count > 0 && oLabActor_Order1.OrderTests.get_Item(0).OrderTestResults.Count > 0)
                    {
                        _isOrderwithTests = false;
                        if (Users.Count > 0)
                        {
                            for (i = 0; i <= Users.Count - 1; i++)
                            {

                                if (Users.get_Item(i).Code != "Normal")
                                {
                                    _isNormal = false;
                                    break;
                                }
                            }
                        }
                        //    break;
                    }
                    //else
                    //{
                    //    _isOrderwithTests = true;

                    //    break;
                    //}

                    //}
                }
                else
                {
                    _isOrderwithTests = false;
                }
                //SLR :Check Logic 4/24/2014

                if (_isNormal == false)
                {
                    oTask.PriorityID = 3;
                    oTask.Subject = "Lab Results (Abnormal) Available for Order " + "ORD" + "-" + _LabOrder.OrderNoID;
                }
                else if (_isOrderwithTests == true)
                {
                    oTask.PriorityID = 1;
                    oTask.Subject = "Lab order assigned ";
                }
                else
                {
                    oTask.PriorityID = 1;
                    oTask.Subject = "Lab Results Available for Order " + "ORD" + "-" + _LabOrder.OrderNoID;
                }


                oTask.ClinicID = _ClinicID;
                oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDate));
                oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDate));
                oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(TaskDueDate));
                //oTask.FaxTiffFileName = FaxTiffFileName 
                oTask.IsPrivate = false;
                oTask.MachineName = Environment.MachineName;
                oTask.Progress = oTaskProgress;
                oTask.ReferenceID1 = _OrderParamter.OrderID;
                //'LabOrder ID for referance 
                oTask.ProviderID = _PatientProviderID;
                if (Users.Count > 1)  //added if condition for bugid 98692
                {
                    oTask.TaskGroupID = ogloTask.GetUniqueueId();
                }
                else
                {
                    oTask.TaskGroupID = 0;
                }
                oTask.OwnerID = _LoginUserID;
                if (TaskId == 0)
                {
                    ogloTask.Add(oTask);
                }
                else
                {
                    ogloTask.Modify(oTask);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                ogloTask.Dispose();
                ogloTask = null;
                oTask.Dispose();
                oTask = null;
                oTaskProgress.Dispose();
                oTaskProgress = null;
            }
        }

        public string GetLoginUserName(Int64 UserID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _strLoginUserName = string.Empty;
            try
            {
                oDB.Connect(false);
                //ProID = Trim(oDB.ExecuteScaler)
                _strLoginUserName = Convert.ToString(oDB.ExecuteScalar_Query("Select sLoginName from dbo.User_MST where nUserID =" + UserID + ""));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strLoginUserName = "";

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _strLoginUserName;
        }

        private void GloUC_trvTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                AddTestToTransactionGrid((gloUserControlLibrary.myTreeNode)GloUC_trvTest.SelectedNode);

                //gloUserControlLibrary.myTreeNode mynode = (gloUserControlLibrary.myTreeNode)GloUC_trvTest.SelectedNode;
                //if ((mynode != null))
                //{
                //    bool _Isgroup = false;
                //    if ((mynode.Parent == null))
                //    {
                //        _Isgroup = true;
                //    }

                //    if (pnl_btnTests.Dock == DockStyle.Top)
                //    {
                //        // '' Add Test TO The Orders 
                //        gloLabUC_Transaction1.AddTest(0,  mynode.ID , mynode.Text, 1, "");
                //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    }
                //    else if (pnl_btnGroups.Dock == DockStyle.Top)
                //    {
                //        // '' Add Test from Groups TO The Orders 
                //        if (_Isgroup)
                //        {
                //            //' Selected Node is Group 
                //            //'Add all tests from that group 
                //            foreach (gloUserControlLibrary.myTreeNode oTestNode in GloUC_trvTest.SelectedNode.Nodes)
                //            {
                //                gloLabUC_Transaction1.AddTest(0, oTestNode.ID, oTestNode.Text, 1, "");
                //            }

                //            GloUC_trvTest.SelectedNode.ExpandAll();
                //        }
                //        else
                //        {
                //            //' Selected Node is Test Under Some Group 
                //            // '' Add Test TO The Orders 
                //            gloLabUC_Transaction1.AddTest(0,  mynode.ID , mynode.Text, 1, "");

                //        }
                //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void AddTestToTransactionGrid(gloUserControlLibrary.myTreeNode mynode)
        { 

            try

            {
                
                //gloUserControlLibrary.myTreeNode mynode = (gloUserControlLibrary.myTreeNode)e.Node;

                if ((mynode != null))
                {
                    
                    bool _Isgroup = false;
                    
                    if ((mynode.Parent == null))
                    {
                        _Isgroup = true;
                    }

                   
                    if (pnl_btnGroups.Dock == DockStyle.Top)
                    {
                        
                        if (_Isgroup)
                        {
                        
                            foreach (gloUserControlLibrary.myTreeNode oTestNode in GloUC_trvTest.SelectedNode.Nodes)
                            {
                                gloLabUC_Transaction1.AddTest(0, oTestNode.ID, oTestNode.Code, 1, "");
                            }
                            GloUC_trvTest.SelectedNode.ExpandAll();
                        }
                        else
                        {
                            gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Code, 1, "");
                        }

                        GloUC_trvTest.txtsearch.ResetText();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                    }

                    else
                    {
                        
                        gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Code, 1, "");
                        GloUC_trvTest.txtsearch.ResetText();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);

                    }

                    GetOutboundTransistion( mynode.ID );
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

       

        private void GloUC_trvTest_MouseUp(object sender, MouseEventArgs e)
        {

            if (GloUC_trvTest.SelectedNode == null)
            {
                GloUC_trvTest.ContextMenuStrip = null;
            }
        }

        private void GloUC_trvTest_MouseDown(object sender, MouseEventArgs e)
        {

            if (gloUCLab_OrderDetail.OrderLabType != "Planned Order")
            {

                ContextMenuStrip oContxMenu = null;

                if (GloUC_trvTest.SelectedNode != null)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        try
                        {
                            if (((oContxMenu == null) == false))
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(oContxMenu);
                                if (((oContxMenu.Items == null) == false))
                                {
                                    oContxMenu.Items.Clear();
                                }
                                oContxMenu.Dispose();
                                oContxMenu = null;
                            }

                            oContxMenu = new ContextMenuStrip();
                            ToolStripMenuItem oMnuItem = default(ToolStripMenuItem);
                            oMnuItem = new ToolStripMenuItem();
                            oMnuItem.Text = "Add to Planned Orders";
                            //oMnuItem.ShortcutKeys = Shortcut.CtrlShiftR;
                            oMnuItem.ShowShortcutKeys = false;
                            //oMnuItem.Image = ImgContextMenu.Images("Add Result.ico");

                            oMnuItem.Click += new EventHandler(callAddFormHandler);
                            oContxMenu.Items.Add(oMnuItem);
                            oMnuItem = null;
                            GloUC_trvTest.ContextMenuStrip = oContxMenu;

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }
                    }
                }
                else
                {
                    if (oContxMenu != null)
                    {
                        oContxMenu.Items.Clear();
                    }
                    GloUC_trvTest.ContextMenuStrip = null;

                }
            }
            else
            {
                GloUC_trvTest.ContextMenuStrip = null;
            }
        }

        public delegate void OpenPlanOfTreatment(Int64 PatientID, string CallingForm, TreeNode oNode = null, string TestType = "", string SearchText = "");
        public event OpenPlanOfTreatment EvntOpenPlanOfTreatment;

        private void callAddFormHandler(object sender, EventArgs e)
        {
            string TestType = "";

            if (pnl_btnTests.Dock == DockStyle.Top)
            {
                TestType = "Lab Tests";
            }
            else if (pnl_btnRefTest.Dock == DockStyle.Top)
            {
                TestType = "Referrals";
            }
            else if (pnl_btnRadiologyImaging.Dock == DockStyle.Top)
            {
                TestType ="Radiology/Imaging";
            }
            else if (pnl_btnOthers.Dock == DockStyle.Top)
            {
               TestType = "Other";
            }
            else if (pnl_btnGroups.Dock == DockStyle.Top)
            {
                TestType = "Groups";
            }
            EvntOpenPlanOfTreatment(_patientID, "Lab", GloUC_trvTest.SelectedNode, TestType, GloUC_trvTest.txtsearch.Text);
        }

        private void GloUC_trvTest_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try

            {

                AddTestToTransactionGrid((gloUserControlLibrary.myTreeNode)e.Node);

                //gloUserControlLibrary.myTreeNode mynode = (gloUserControlLibrary.myTreeNode)e.Node;

                //if ((mynode != null))
                //{
                    
                //    bool _Isgroup = false;
                    
                //    if ((mynode.Parent == null))
                //    {
                //        _Isgroup = true;
                //    }

                //    //if (pnl_btnTests.Dock == DockStyle.Top)
                //    //{
                //    //    // '' Add Test TO The Orders 

                //    //    if (chkIncludeTestCode.Checked == false)
                //    //    {
                //    //        gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Text, 1, "");
                //    //    }
                //    //    else
                //    //    {
                //    //        gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Name.Replace(mynode.Code + " - ", ""), 1, "");
                //    //    }

                      
                //    //    GloUC_trvTest.txtsearch.ResetText();
                //    //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);

                //    //} 
                //    //else if (pnl_btnRefTest.Dock == DockStyle.Top)
                //    //{
                //    //    // '' Add Test TO The Orders 
                //    //    gloLabUC_Transaction1.AddTest(0,  mynode.ID , mynode.Text, 1, "");
                //    //    GloUC_trvTest.txtsearch.ResetText();
                //    //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    //}

                //    ////28-Apr-14 8020 Orders PRD: Show tests by Order Type
                //    //else if (pnl_btnRadiologyImaging.Dock == DockStyle.Top)
                //    //{
                //    //    gloLabUC_Transaction1.AddTest(0,  mynode.ID , mynode.Text, 1, "");
                //    //    GloUC_trvTest.txtsearch.ResetText();
                //    //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    //}


                //    ////28-Apr-14 8020 Orders PRD: Show tests by Other Type
                //    //else if (pnl_btnOthers.Dock == DockStyle.Top)
                //    //{
                //    //    gloLabUC_Transaction1.AddTest(0,  mynode.ID , mynode.Text, 1, "");
                //    //    GloUC_trvTest.txtsearch.ResetText();
                //    //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    //}

                //    //else 
                //    if (pnl_btnGroups.Dock == DockStyle.Top)
                //    {
                //        // '' Add Test from Groups TO The Orders 
                //        if (_Isgroup)
                //        {
                //            //' Selected Node is Group 
                //            //'Add all tests from that group 
                //            foreach (gloUserControlLibrary.myTreeNode oTestNode in GloUC_trvTest.SelectedNode.Nodes)
                //            {
                //                gloLabUC_Transaction1.AddTest(0, oTestNode.ID, oTestNode.Text, 1, "");
                //            }
                //            GloUC_trvTest.SelectedNode.ExpandAll();
                //        }
                //        else
                //        {
                //            // '' Add Test TO The Orders 
                //            gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Text, 1, "");
                //        }
                //        GloUC_trvTest.txtsearch.ResetText();
                //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);
                //    }

                //    else
                //    {
                //        //if (chkIncludeTestCode.Checked == false)
                //        //{
                //        //    gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Text, 1, "");
                //        //}
                //        //else
                //        //{
                //            gloLabUC_Transaction1.AddTest(0, mynode.ID, mynode.Code, 1, "");
                //        //}

                //        GloUC_trvTest.txtsearch.ResetText();
                //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success);

                //    }

                //    GetOutboundTransistion( mynode.ID );
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void GetOutboundTransistion(Int64 labtm_ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            Object objTempalte = false;
            try
            {
                string strQry = "select isnull(bOutboundTransistion, 0) from Lab_Test_Mst where labtm_ID =" + labtm_ID;
                oDB.Connect(false);
                objTempalte = oDB.ExecuteScalar_Query(strQry);
                if (chkOutboundTransition.Checked == false)
                {
                    chkOutboundTransition.Checked = (bool)objTempalte;
                }
            }
            catch //(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading lab test word Template", false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }


        //private void gloUCLab_Transaction_gUC_ButtonDiagnCPTClicked()
        //{
        //    //Int64 _VisitID = 0;
        //    // try
        //    // {
        //    //     _VisitID =  GetVisitID(DateTime.Now, _patientID);
        //    //     {
        //    //         if ( Classes.clsEmdeonGeneral.gblnICD9Driven)
        //    //         {
        //    //             frm_Diagnosis frm = new frm_Diagnosis(_VisitID, 0, false);
        //    //             frm.StartPosition = FormStartPosition.CenterScreen;
        //    //             frm.ShowInTaskbar = false;
        //    //             frm.ShowDialog(this);
        //    //         }
        //    //         else
        //    //         {
        //    //             frm_Treatment oTreatment = new frm_Treatment(0, _VisitID, DateTime.Now, "", false, _patientID);
        //    //             oTreatment.ShowDialog(this);
        //    //             oTreatment.Dispose();
        //    //         }
        //    //     }
        //    // }
        //    // catch (Exception ex)
        //    // {
        //    //     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    //     //throw;
        //    // }           
        //}

        //Added by Mayuri:20130313-To fill lab task users list.
        private void gloLabUC_Transaction1_gUC_OkButtonClicked(Int16 SendTaskType, Int16 ResultType)
        {
            DataTable dtUsers = GetLabTaskUserByProvider(_OrderParamter.ProviderID , SendTaskType, ResultType);
            gloUCLab_OrderDetail.FillLabTaskUsers(dtUsers);
          
            if (dtUsers != null)
            {
                dtUsers.Dispose();
                dtUsers = null;
            }
        }

       
        public long GetVisitID(System.DateTime VisitDate, long patientID)
        {
            long _retvisitID = 0;
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBparams = new gloDatabaseLayer.DBParameters();
            object objRet = null;
            try
            {
                oDBLayer.Connect(false);
                oDBparams.Clear();

                oDBparams.Add("@visitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBparams.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt, 18);

                objRet = oDBLayer.ExecuteScalar("gsp_GetVisitID", oDBparams);
                if (objRet != null)
                {
                    _retvisitID = Convert.ToInt64(objRet);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                oDBparams.Dispose();
                oDBparams = null;
            }
            return _retvisitID;
        }

        //private void gloUCLab_Transaction_gUC_ScanDocument(long TestID)
        //{

        //    //Int64 _ScanContainerID = 0;
        //    //Int64 _ScanDocumentID = 0;
        //    //bool _ScanDocFlag = true;
        //    //bool _result = false;
        //    //try
        //    //{
        //    //    if (gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, _DMSCategory_Labs, _ClinicID) == false)
        //    //    {
        //    //        MessageBox.Show("DMS Category for lab order has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //        _ScanDocFlag = false;
        //    //    }

        //    //    if (_ScanDocFlag == true)
        //    //    {
        //    //        _result = Set_ScanDocumentEvent(_OrderParamter.PatientID, _DMSCategory_Labs, ref _ScanContainerID, ref _ScanDocumentID);
        //    //    }

        //    //    if (_result == true)
        //    //    {                  
        //    //      gloUCLab_Transaction.AddScanDocument(TestID, _ScanDocumentID);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    //}       
        //}
        
        private void GetAdminSettings()
        {
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString.ToString().Trim());
            //DataTable dtSettings = new DataTable();
            string strQry = "select sSettingsValue from settings where sSettingsName='LAB CATEGORY'";
            try
            {
                objDbLayer.Connect(false);
                _DMSCategory_Labs = Convert.ToString(objDbLayer.ExecuteScalar_Query(strQry));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
        }

        private bool Set_ScanDocumentEvent(Int64 PatientID, ref ArrayList _iLabDMSIDs)
        {
            gloEDocumentV3.gloEDocV3Management oScanDocument = new gloEDocumentV3.gloEDocV3Management();
            bool _result = false;
            try
            {
                //_result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocument.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
               // _result = oScanDocument.ShowEScanner(_patientID, LabCategory, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), _ClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, out ScanContainerID, out ScanDocumentID);
                //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                oScanDocument.dMdi = dMdi;
                _result = oScanDocument.ShowEDocument_LabOrder(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, null, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, 0, out _iLabDMSIDs);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oScanDocument.Dispose();
            }
            return _result;
        }

        private void gloUCLab_Transaction_gUC_TestSelected(long TestID, string Specimen, string CollectionContainer, string StorageTemperature, string LOINCCode, string Instructionas, string Precuation, string Comments)
        {
            //gloUCLab_TestDetail.SetData(TestID, Specimen, CollectionContainer, StorageTemperature, LOINCCode, Instructionas, Precuation, Comments);
        }
      

        private void gloUCLab_Transaction_gUC_ViewDocument(long TestID, long DocumentID)
        {
            //try
            //{
            //    if (DocumentID > 0)
            //    {
            //        if (oViewDocument == null)
            //        {
            //            oViewDocument = new gloEDocumentV3.gloEDocV3Management();
            //        }
            //        oViewDocument.ShowEDocument(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, this.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID);
            //        oViewDocument.EvnRefreshDocuments += new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
            //        oViewDocument.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //}
        }

        void oViewDocument_EvnRefreshDocuments()
        {
        }

        //Added by madan on 20100618-- for unlock records.

        private void frmViewNormalLab_FormClosed(object sender, FormClosedEventArgs e)
        {
            // if the Locked by by the Current User & on Current Machine only
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                if (clsEmdeonGeneral.gloLab_IsOrderLocked && LabOrderParameter.OrderID > 0)
                {
                    objClsGeneral.UnLockRecords(clsGeneral.TrnType.Labs, LabOrderParameter.OrderID, 0, DateTime.Now);
                    clsEmdeonGeneral.gloLab_IsOrderLocked = false;
                }
                if (gloUCLab_OrderDetail != null)
                {
                    gloUCLab_OrderDetail.Dispose();
                    gloUCLab_OrderDetail = null;
                }
                if (GloUC_TemplateTreeControl_Orders!= null)
                {
                        GloUC_TemplateTreeControl_Orders.FinalizeControlParameter("");
                }
            
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                }
                try
                {
                    dynamic mdi = this._MdiParent;

                    if (mdi != null)
                    {
                       mdi.ActiveDSO = null;
                    }
                }
                catch
                {
                }
            }
        }
        private void frmViewNormalLab_Activated(object sender, EventArgs e)
        {
            dynamic mdi = this._MdiParent;

            if (mdi != null)
            {
                mdi.RegisterMyHotKey();
                mdi._isShowDialogueForm = true;
                mdi.ActiveDSO = wdOrders;
            }
          

        }
        private void frmViewNormalLab_FormClosing(object sender, FormClosingEventArgs e)
        { // Code for this evenet written by Abhijeet on 20100623

            if (_blnClosed == false)
            {
                _blnClosed = true;
                dynamic mdi = this._MdiParent;

                if (mdi != null)
                {
                    mdi._isShowDialogueForm = false;
                }
                //*//if (gloUCLab_Transaction.LabModified)
                if (gloLabUC_Transaction1.LabModified)
                {
                    ////// added by Ujwala for SmartDx/SmartCPT/SmartOrder functionality - as on 11252010
                    if (ArrLabs != null)
                    {
                        IsClosed = true;
                        SaveOrder(0);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is successfully saved & closed", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    else
                    {
                        DialogResult oResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (oResult == DialogResult.Yes)
                        {
                            IsClosed = true;
                            SaveOrder(0);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is successfully saved & closed", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        }
                        else if (oResult == DialogResult.No)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is closed without saving", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        }
                        else if (oResult == DialogResult.Cancel)
                        {
                            _blnClosed = false;
                            e.Cancel = true;
                        }
                    }
                    ////// added by Ujwala for SmartDx/SmartCPT/SmartOrder functionality - as on 11252010
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab request order is closed without saving", _patientID, _OrderParamter.OrderID, _LabProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
            }
        }

        private void gloLabUC_Transaction1_gUC_ButtonDiagnCPTClicked()
        {
            Int64 _VisitID = 0;
            try
            {
                _VisitID = GetVisitID(DateTime.Now, _patientID);
                {
                    if (Classes.clsEmdeonGeneral.gblnICD9Driven)
                    {
                        ////Added by madan on 20100803-- to solve the icd display issue
                        //gloEmdeonCommon.mdlGeneral.gnPatientID = _patientID;
                        ////end.. madan... changes

                        frm_Diagnosis frm = new frm_Diagnosis(_VisitID, 0, false, _patientID);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowInTaskbar = false;
                        frm.ShowDialog(this);
                        frm.Dispose();
                        frm = null;
                    }
                    else
                    {
                        frm_Treatment oTreatment = new frm_Treatment(0, _VisitID, DateTime.Now, "", false, _patientID);
                        oTreatment.ShowDialog(this);
                        oTreatment.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //throw;
            }
        }


        private void gloLabUC_Transaction1_gUC_ScanDocument(long TestID)
        {

            ArrayList _iLabDMSIDs = null;

            bool _result = false;
            try
            {
                _result = Set_ScanDocumentEvent(_OrderParamter.PatientID, ref _iLabDMSIDs);


                if (_result == true)
                {
                    //*// gloUCLab_Transaction.AddScanDocument(TestID, _ScanDocumentID);
                    gloLabUC_Transaction1.AddScanDocument(TestID, _iLabDMSIDs);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void gloLabUC_Transaction1_gUC_TestSelected(long TestID, string Specimen, string CollectionContainer, string StorageTemperature, string LOINCCode, string Instructionas, string Precuation, string Comments)
        {
            gloUCLab_TestDetail.SetData(TestID, Specimen, CollectionContainer, StorageTemperature, LOINCCode, Instructionas, Precuation, Comments);
            CurTestID = TestID;
        }
     
        private void gloLabUC_Transaction1_gUC_GetURLDocument(long TestID, string Specimen, string CollectionContainer, string StorageTemperature, string LOINCCode, string Instructionas, string Precuation, string Comments)
        {
            gloUCLab_TestDetail.SetData(TestID, Specimen, CollectionContainer, StorageTemperature, LOINCCode, Instructionas, Precuation, Comments);
        }

        private void gloLabUC_Transaction1_gUC_ViewDocument(long TestID, long DocumentID)
        {
            try
            {
                if (DocumentID > 0)
                {
                    if (oViewDocument == null)
                    {
                        oViewDocument = new gloEDocumentV3.gloEDocV3Management();
                    }
                    oViewDocument.oPatientExam = objPatientExam;

                    oViewDocument.oPatientMessages = objPatientMessages;
                    oViewDocument.oPatientLetters = objPatientLetters;
                    oViewDocument.oNurseNotes = objNurseNotes;
                    oViewDocument.oHistory = objHistory;
                    oViewDocument.oLabs = objLabs;
                    oViewDocument.oDMS = objDMS;
                    oViewDocument.oRxmed = objRxmed;
                    oViewDocument.oOrders = objOrders;
                    oViewDocument.oProblemList = objProblemList;

                    oViewDocument.oCriteria = objCriteria;
                    oViewDocument.oWord = objWord;
                    //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                    oViewDocument.dMdi = this.MdiParent;

                    oViewDocument.ShowEDocument(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, this.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID);
                    oViewDocument.EvnRefreshDocuments += new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
                    oViewDocument.EvnRefreshDocuments -= new gloEDocumentV3.gloEDocV3Management.RefreshDocuments(oViewDocument_EvnRefreshDocuments);
                    oViewDocument.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #region "Open DICOM Document"
        //Developer:Mitesh Patel
        //Date: 28 Jun 2012
        //Bug ID: 28624
        private void gloLabUC_Transaction1_gUC_ViewDicomDocument(long nPatientId, long DocumentID)
        {
            string sTestDicomPath = string.Empty;
            string sDicomPath = string.Empty;

            try
            {
                if (nPatientId <= 0 && DocumentID <= 0)
                {
                    return;
                }
                sDicomPath = GetEMRDciomPath();

                if (ConfirmNull(sDicomPath))
                {

                    if (System.IO.Directory.Exists(sDicomPath) == false)
                    {
                        MessageBox.Show("Please configure valid DICOM path to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    char ch1 = sDicomPath[sDicomPath.Length - 1];

                    if (ch1 != '\\')
                    {
                        sDicomPath = sDicomPath + "\\";
                    }

                }
                else
                {
                    MessageBox.Show("Please configure the DICOM path from Tool->Settings->Server Path, to save the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                clsGeneral objcls = new clsGeneral();
                Int64 nDBId = 0;
                nDBId = objcls.GetDataBaseConnectionIdFromHL7DB();
                objcls.Dispose();
                if (nDBId == 0)
                {
                    MessageBox.Show("gloEMR database connection ID not found in HL7 database.Please add gloEMR database in HL7 Database setting. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sTestDicomPath = string.Empty;
                }
                else
                {
                    sTestDicomPath = GetTestDicomPath();
                }


                if (ConfirmNull(sTestDicomPath))
                {
                    sTestDicomPath = sTestDicomPath + "\\" + nPatientId;

                    char ch = sTestDicomPath[sTestDicomPath.Length - 1];

                    if (ch != '\\')
                    {
                        sTestDicomPath = sTestDicomPath + "\\";
                    }

                    if (System.IO.Directory.Exists(sTestDicomPath) == false)
                    {
                        MessageBox.Show("Please configure valid dicom path in gloHL7 service to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    gloEmdeonCommon.frmgloDICOM objFrmDicom = new frmgloDICOM(nPatientId, DocumentID, sTestDicomPath, sDicomPath, _LoginUserID);
                    objFrmDicom.ShowInTaskbar = false;
                    objFrmDicom.BringToFront();
                    objFrmDicom.ShowDialog(this);

                    if (objFrmDicom != null)
                    {
                        objFrmDicom.Dispose();
                        objFrmDicom = null;
                    }

                    //Commented Refresh grids.
                    #region "Refresh grids"

                    //if (c1TestLibrary.Rows.Count > 0 && c1TestLibrary.RowSel > 0)
                    //{
                    //    long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    //    gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                    //    if (tabControl1.SelectedIndex == 1)
                    //    {
                    //        gloUCLab_OrderDetail.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        gloUCLab_OrderDetail.Visible = true;
                    //    }
                    //    ShowOrders(curOrderID);
                    //    string _strOrderType = GetOrderType(curOrderID);
                    //    if (_strOrderType == "Emr")
                    //    {
                    //        tlbbtn_Finish.Enabled = true;
                    //        tlbbtn_Requisition.Enabled = false;
                    //        gloUCLab_OrderDetail.PreferredLabActivity(false);
                    //    }
                    //    else
                    //    {
                    //        gloUCLab_OrderDetail.PreferredLabActivity(true);
                    //        tlbbtn_Finish.Enabled = false;
                    //        tlbbtn_Requisition.Enabled = true;
                    //    }


                    //    tlbbtn_ViewHistory.Visible = true;
                    //}

                    #endregion
                }
                else
                {
                    MessageBox.Show("Please configure dicom path in gloHL7 service to view the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                sTestDicomPath = string.Empty;
                sDicomPath = string.Empty;
            }

        }

        private string GetTestDicomPath()
        {
            string sDicomPath = string.Empty;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetHL7ConnectionString());

            string strQuery = string.Empty;
            object objResult = null;

            try
            {
                clsGeneral objclsgeneral = new clsGeneral();
                Int64 intDBId = 0;
                intDBId = objclsgeneral.GetDataBaseConnectionIdFromHL7DB();
                objclsgeneral.Dispose();
                strQuery = "Select ISNULL(sSettingsValue,'')as sValue from HL7_Settings where sSettingsName='Dicom Path' and nDBConnectionId=" + intDBId.ToString();

                objDbLayer.Connect(false);

                objResult = objDbLayer.ExecuteScalar_Query(strQuery);

                if (objResult != null && objResult.ToString() != "")
                {
                    sDicomPath = Convert.ToString(objResult);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                sDicomPath = string.Empty;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                strQuery = string.Empty;
                objResult = null;
            }
            return sDicomPath;
        }

        private string GetEMRDciomPath()
        {
            string sDicomPath = string.Empty;

            try
            {
                if (gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR) == false)
                {
                    return "";
                }

                gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR, true);

                if ((gloSettings.gloRegistrySetting.GetRegistryValue("DICOMPATH") == null) == true)
                {
                    sDicomPath = "";
                }
                else
                {
                    sDicomPath = Convert.ToString(gloSettings.gloRegistrySetting.GetRegistryValue("DICOMPATH").ToString());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                sDicomPath = string.Empty;
            }
            finally
            {
                gloSettings.gloRegistrySetting.CloseRegistryKey();

            }
            return sDicomPath;
        }

        protected bool ConfirmNull(string strValue)
        {
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnCheck;
        }
        #endregion ""



        private void btnGroups_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_btnGroups.Dock = DockStyle.Top;
                btnTests.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;
                
                pnl_btnTests.Dock = DockStyle.Bottom;
                btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnRefTest.Dock = DockStyle.Bottom;
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Order Type
                pnl_btnRadiologyImaging.Dock = DockStyle.Bottom;
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Others Type
                pnl_btnOthers.Dock = DockStyle.Bottom;
                btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

                
                pnlPlannedOrder.Dock = DockStyle.Bottom;
                btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongButton;
                btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;



                gloUCLab_OrderDetail.OrderLabType = "Groups";
                gloUCLab_OrderDetail.ReadData();
                gloUCLab_OrderDetail.OrderSelected = false;
                FillGroups_NEW(gloUCLab_OrderDetail.PreferredLabID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnRefTest_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_btnRefTest.Dock = DockStyle.Top;
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnGroups.Dock = DockStyle.Bottom;
                btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnTests.Dock = DockStyle.Bottom;
                btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Order Type
                pnl_btnRadiologyImaging.Dock = DockStyle.Bottom;
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Others Type
                pnl_btnOthers.Dock = DockStyle.Bottom;
                btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

                pnlPlannedOrder.Dock = DockStyle.Bottom;
                btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongButton;
                btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;

                gloUCLab_OrderDetail.OrderLabType = "Referrals";
                gloUCLab_OrderDetail.ReadData();
                gloUCLab_OrderDetail.OrderSelected = false;
                //FillRefTests(gloUCLab_OrderDetail.PreferredLabID);
                FillTestsByType(gloEMRLabTest.OrderTestType.Referrals, gloUCLab_OrderDetail.PreferredLabID); 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //public void FillRefTests(Int64 PreferredID = 0)
        //{
        //    try
        //    {
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null;

        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();
        //        gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
        //        gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                
        //        DataTable dt = new DataTable();

        //        //DataColumn Col2 = new DataColumn("TestID");
        //        //Col2.DataType = System.Type.GetType("System.Decimal");
        //        //dt.Columns.Add(Col2);
        //        //DataColumn Col3 = new DataColumn("TestName");
        //        //Col3.DataType = System.Type.GetType("System.String");
        //        //dt.Columns.Add(Col3);

        //        dt = GetTestStructure();

        //        if (oLabTests == null)
        //        {
        //            if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
        //            {
        //                oLabTests = oTest.GetRefferalTestsOrder(false); 
        //            }

        //        }


        //        if (oLabTests == null)
        //        {
        //            if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
        //            {

        //                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
        //                oLabTests = oTest.GetRefferalTestsOrder(false, oLabActor_Order.PreferredLabID);

        //            }

        //            //When the PreferredID is not 0
        //            if (PreferredID != 0)
        //            {
        //                oLabTests = oTest.GetRefferalTestsOrder(false, PreferredID);
        //            }

        //        }


        //        if (oLabTests.Count > 0)
        //        {
        //            DataRow row = null;
        //            //'Add data from the object to a datatable 
        //            for (int i = 0; i <= oLabTests.Count - 1; i++)
        //            {
                      
        //                gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest = oLabTests.get_Item(i);
        //                row = dt.NewRow();

        //                row["TestName"] = oLabTest.Name;
        //                row["TestID"] = oLabTest.TestID;
        //                row["TestCodeTestName"] = oLabTest.Code + " - " + oLabTest.Name;

        //                dt.Rows.Add(row);
        //            }
        //        }
        //        GloUC_trvTest.ParentMember = null;
        //        if ((dt != null))
        //        {
        //            GloUC_trvTest.ImageIndex = 0;
        //            GloUC_trvTest.SelectedImageIndex = 0;
        //            GloUC_trvTest.DataSource = dt;
        //            GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;

        //            GloUC_trvTest.CodeMember = "TestName";
        //            GloUC_trvTest.ValueMember = "TestID";

        //            if (chkIncludeTestCode.Checked == false)
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestName";
        //            }
        //            else
        //            {
        //                GloUC_trvTest.DescriptionMember = "TestCodeTestName";
        //            }

        //            GloUC_trvTest.FillTreeView();
        //        }
        //        if (gloUCLab_OrderDetail.OrderSelected == true)
        //        {
        //            if (PreferredID != 0)
        //            {
        //                CheckAssoPreferredLab(PreferredID);
        //            }
        //        }
        //        oTest.Dispose();
        //        oTest = null;
        //        oLabTests.Dispose();
        //        oLabTests = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable FillAllPreferredLabs(Int64 PreferredID = 0)  
        {
            DataTable dt = new DataTable();
            string strSql = "SELECT labtm_ID as TEST_ID FROM Lab_Test_Mst_PreferredLab WHERE dbo.Lab_Test_Mst_PreferredLab.labci_Id =" + PreferredID;
            SqlConnection con = new SqlConnection(_dataBaseConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSql, con);
            da.Fill(dt);
            da.Dispose();
            da = null;
            con.Close();
            con.Dispose();
            con = null;
            return dt;
        }

        //'Using tree view user control
        public void FillGroups_NEW(Int64 PreferredID = 0)
        {
           

           
            DataTable dt = new DataTable();
            DataColumn Col0 = new DataColumn("GroupID");
            Col0.DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add(Col0);
            DataColumn Col1 = new DataColumn("GroupName");
            Col1.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(Col1);
            DataColumn Col2 = new DataColumn("TestID");
            Col2.DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add(Col2);
            DataColumn Col3 = new DataColumn("TestName");
            Col3.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(Col3);
            DataColumn Col4 = new DataColumn("TestCodeTestName");
            Col4.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(Col4);



            try
            {
                gloEMRLabGroup oGroup = new gloEMRLabGroup();
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabGroups oLabGroups = new gloEMRGeneralLibrary.gloEMRActors.LabActor.LabGroups();
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
                
                if ((_OrderParamter.IsEditMode == false && PreferredID == 0) || (IsPreferredLabCleared == true && PreferredID == 0))
                {
                        oLabGroups = oGroup.GetGroups();
                }

                if (_OrderParamter.IsEditMode == true && _OrderParamter.OrderID > 0 && PreferredID == 0 && IsPreferredLabCleared == false)
                {

                oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
                oLabGroups = oGroup.GetGroups(oLabActor_Order.PreferredLabID);

                }

                //When the PreferredID is not 0
                if (PreferredID != 0)
                {
                oLabGroups = oGroup.GetGroups(PreferredID);
                }

                
                
                DataRow row = null;
                if (oLabGroups.Count > 0)
                {
                    for (int i = 0; i <= oLabGroups.Count - 1; i++)
                    {
                        string _groupName = null;
                        Int64 _groupID = default(Int64);

                        _groupName = oLabGroups.get_Item(i).LabGroupName;

                        _groupID = oLabGroups.get_Item(i).LabGroupID;


                        for (int j = 0; j <= oLabGroups.get_Item(i).Tests.Count - 1; j++)
                        {
                            row = dt.NewRow();
                            row["GroupName"] = _groupName;
                            row["GroupID"] = _groupID;
                            row["TestName"] = oLabGroups.get_Item(i).Tests.get_Item(j).Name;
                            row["TestID"] = oLabGroups.get_Item(i).Tests.get_Item(j).TestID;
                            row["TestCodeTestName"] = oLabGroups.get_Item(i).Tests.get_Item(j).Code + " - " + oLabGroups.get_Item(i).Tests.get_Item(j).Name;

                            dt.Rows.Add(row);
                        }
                    }
                }
                if ((dt != null))
                {
                    GloUC_trvTest.ImageIndex = 0;
                    GloUC_trvTest.SelectedImageIndex = 0;
                    GloUC_trvTest.ParentImageIndex = 1;
                    GloUC_trvTest.SelectedParentImageIndex = 1;
                    GloUC_trvTest.DataSource = dt;
                    GloUC_trvTest.ParentMember = "GroupName";
                    GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;

                    GloUC_trvTest.CodeMember = "TestName";
                    GloUC_trvTest.ValueMember = "TestID";

                    if (chkIncludeTestCode.Checked == false)
                    {
                        GloUC_trvTest.DescriptionMember = "TestName";
                    }
                    else
                    {
                        GloUC_trvTest.DescriptionMember = "TestCodeTestName";
                    }
                    
                    GloUC_trvTest.FillTreeView();
                }
                if (gloUCLab_OrderDetail.OrderSelected == true)
                {
                    if (PreferredID != 0)
                    {
                        CheckAssoPreferredLab(PreferredID);
                    }
                }
               
                oLabGroups.Dispose();
                oLabGroups = null;
                oGroup.Dispose();
                oGroup = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
         
        }

        //28-Apr-14 8020 Orders PRD: Show tests by Order Type
        private void btnRadiologyImaging_Click(object sender, EventArgs e)
        {
            try
            {

                pnl_btnRadiologyImaging.Dock = DockStyle.Top;
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnGroups.Dock = DockStyle.Bottom;
                btnGroups.BackgroundImage = Properties.Resources.Img_LongButton;
                btnGroups.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnTests.Dock = DockStyle.Bottom;
                btnTests.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnRefTest.Dock = DockStyle.Bottom;
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Others Type
                pnl_btnOthers.Dock = DockStyle.Bottom;
                btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

                pnlPlannedOrder.Dock = DockStyle.Bottom;
                btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongButton;
                btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;

                gloUCLab_OrderDetail.OrderLabType = "RadiologyImaging";
                gloUCLab_OrderDetail.ReadData();
                gloUCLab_OrderDetail.OrderSelected = false;
                //FillRadiologyImagingTests(gloUCLab_OrderDetail.PreferredLabID);
                FillTestsByType(gloEMRLabTest.OrderTestType.RadiologyImaging, gloUCLab_OrderDetail.PreferredLabID);
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnTests_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_btnGroups.Dock = DockStyle.Bottom;
                btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;


                pnl_btnTests.Dock = DockStyle.Top;
                btnTests.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;

                pnl_btnRefTest.Dock = DockStyle.Bottom;
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

                //28-Apr-14 8020 Orders PRD: Show tests by Order Type
                pnl_btnRadiologyImaging.Dock = DockStyle.Bottom;
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;


                //28-Apr-14 8020 Orders PRD: Show tests by Others Type
                pnl_btnOthers.Dock = DockStyle.Bottom;
                btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

                pnlPlannedOrder.Dock = DockStyle.Bottom;
                btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongButton;
                btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;

                gloUCLab_OrderDetail.OrderLabType = "LabTests";
                gloUCLab_OrderDetail.ReadData();
                gloUCLab_OrderDetail.OrderSelected = false;
                //FillTests_NEW(gloUCLab_OrderDetail.PreferredLabID);
                FillTestsByType(gloEMRLabTest.OrderTestType.LabTests, gloUCLab_OrderDetail.PreferredLabID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnGroups_MouseEnter(object sender, EventArgs e)
        {
            btnGroups.BackgroundImage = Properties.Resources.Img_LongYellow;
            btnGroups.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void btnRefTest_MouseEnter(object sender, EventArgs e)
        {
            btnRefTest.BackgroundImage = Properties.Resources.Img_LongYellow;
            btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnTests_MouseEnter(object sender, EventArgs e)
        {
            btnTests.BackgroundImage = Properties.Resources.Img_LongYellow;
            btnTests.BackgroundImageLayout = ImageLayout.Stretch;
        }

        //28-Apr-14 8020 Orders PRD: Show tests by Order Type
        private void btnRadiologyImaging_MouseEnter(object sender, EventArgs e)
        {
            btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongYellow;
            btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private void btnGroups_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnGroups.Dock == DockStyle.Top)
            {
                btnGroups.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnGroups.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnGroups.BackgroundImage = Properties.Resources.Img_LongButton;
                btnGroups.BackgroundImageLayout = ImageLayout.Stretch;
            }

        }

        private void btnRefTest_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnRefTest.Dock == DockStyle.Top)
            {
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;
            }

        }

        private void btnTests_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnTests.Dock == DockStyle.Top)
            {
                btnTests.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
                btnTests.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        //28-Apr-14 8020 Orders PRD: Show tests by Order Type
        private void btnRadiologyImaging_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnRadiologyImaging.Dock == DockStyle.Top)
            {
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongButton;
                btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        //Developer:Sanjog Dhamke
        //Date: 9 Feb 2012
        //Bug ID: 18826 To update the result flag after save
        //Reason: To call SP for update the result flag
        private void UpdateResultFlag()
        {
            gloDatabaseLayer.DBLayer oDBObj = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParas = new gloDatabaseLayer.DBParameters();
            try
            {
                if (gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSpecificResultRange.Trim() == "1")
                {
                    oDBObj.Connect(false);
                    oDBParas.Add(new gloDatabaseLayer.DBParameter("@nPatientID", _patientID, ParameterDirection.Input, SqlDbType.BigInt));
                    oDBObj.ExecuteScalar("Lab_UpdateResultFlag", oDBParas);
                    oDBObj.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error While result flag update with Patient specific result range :" + ex.ToString(), false);

            }
            finally
            {
                if (oDBObj != null)
                {
                    oDBObj.Disconnect();
                    oDBObj.Dispose();
                }
                if (oDBParas != null)
                {                    
                    oDBParas.Dispose();
                }

            }
        }


        public string ExamNewDocumentName
        {
            get
            {
                return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff");
            }
        }

        private void InitialiseTemplateTreeControl()
        {
            gloEmdeonCommon.gloEMRWord.clsWordDocument clsWord_TemplateTree = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
            gloEmdeonCommon.gloEMRWord.DocCriteria oCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
            GloUC_TemplateTreeControl_Orders.InitiliseControlParameter(_dataBaseConnectionString);
            GloUC_TemplateTreeControl_Orders.DocCriteria = oCriteria;
            GloUC_TemplateTreeControl_Orders.ObjClsWord = clsWord_TemplateTree;
            GloUC_TemplateTreeControl_Orders.ProviderId = _PatientProviderID;
            GloUC_TemplateTreeControl_Orders.Fill_ExamTemplates(0);
        }

        public void calltoAddRefreshButtonControl()
        {
            DateTimePicker dtP = new DateTimePicker();
            try
            {
                gloEmdeonCommon.gloEMRWord.clsWordDocument ObjWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                gloEmdeonCommon.gloEMRWord.DocCriteria objCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();

                GloUC_AddRefreshDic1.CONNECTIONSTRINGs = _dataBaseConnectionString;
                
                GloUC_AddRefreshDic1.OBJWORDs = ObjWord;
                GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria;
                GloUC_AddRefreshDic1.M_PATIENTIDs = _patientID;
                //Added on 20150929-To show ordering provider in ordertemplates for provider signature
                GloUC_AddRefreshDic1.M_ProviderIDs = Convert.ToInt64(cmbProvider.SelectedValue);
                GloUC_AddRefreshDic1.ObjFrom = this;

                dtP.Value = System.DateTime.Now.Date;

                try
                {
                    if (GloUC_AddRefreshDic1.DTLETTERDATEs != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs);
                        }
                        catch
                        {
                        }
                        GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose();
                        GloUC_AddRefreshDic1.DTLETTERDATEs = null;
                    }
                }
                catch
                {
                }


                GloUC_AddRefreshDic1.DTLETTERDATEs = dtP;
                GloUC_AddRefreshDic1.OWORDAPPs = oWordApp;
                GloUC_AddRefreshDic1.wdPatientWordDocs = wdOrders;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                //dtP.Dispose();
                //dtP = null;
            }
        }

        private object GetTemplate(long TempId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            object objTempalte = null;
            //byte[] img = null;
            //img = Encoding.ASCII.GetBytes(dt.Rows[0]["sDescription"].ToString());
            try
            {
                string strQry = "select sDescription from TemplateGallery_MST where nTemplateID =" + TempId;
                oDB.Connect(false);
                objTempalte = oDB.ExecuteScalar_Query(strQry);
                return objTempalte;
            }
            catch //(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in loading lab test word Template", false);
                return objTempalte;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                objTempalte = null;
            }
        }

        public void LoadWordToolStrip(int _isfinished = 0)
        {
            if (tlsOrders != null)
            {
                this.pnlToolStrip.Controls.Remove(tlsOrders);
                tlsOrders.ToolStripClick -= new WordToolStrip.gloWordToolStrip.MyToolStripClickEventHandler(tlsOrdestlsOrders_ToolStripClick);
                tlsOrders.ToolStripButtonClick -= new WordToolStrip.gloWordToolStrip.MyToolStripButtonClickEventHandler(tlsOrders_ToolStripButtonClick1);
 
                tlsOrders.Dispose();
                tlsOrders = null;
            }
           tlsOrders = new WordToolStrip.gloWordToolStrip();
            tlsOrders.ToolStripClick += new WordToolStrip.gloWordToolStrip.MyToolStripClickEventHandler(tlsOrdestlsOrders_ToolStripClick);
            tlsOrders.ToolStripButtonClick += new WordToolStrip.gloWordToolStrip.MyToolStripButtonClickEventHandler(tlsOrders_ToolStripButtonClick1);
            tlsOrders.Dock = DockStyle.Top;
            tlsOrders.ButtonsToHide.Add("Capture Sign");

            if (_isfinished == 1)
            {
                tlsOrders._isFinished_Lab = false;
                tlsOrders._isFinishedLabButton = true;
                tlsOrders.ButtonsToHide.Add("Finish");
                tlsOrders.ButtonsToHide.Add("Insert File");
                tlsOrders.ButtonsToHide.Add("Scan Documents");
                tlsOrders.ButtonsToHide.Add("Undo");
                tlsOrders.ButtonsToHide.Add("Redo");
                tlsOrders.ButtonsToHide.Add("Insert Associated Provider Signature");
                tlsOrders.ButtonsToHide.Add("Insert Sign");
                tlsOrders.ButtonsToHide.Add("Save");
                tlsOrders.ButtonsToHide.Add("Add Addendum");
                pnlGloUC_TemplateTreeControl.Visible = false;
                GloUC_AddRefreshDic1.Visible = false;
                //tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
                tlsOrders.FormType = WordToolStrip.enumControlType.OrdersAddendum;

            }
            else
            {
                

                tlsOrders._isFinished_Lab = true;
                tlsOrders._isFinishedLabButton = false;
                pnlGloUC_TemplateTreeControl.Visible = true;
                tlsOrders.ButtonsToHide.Remove("Finish");
                tlsOrders.ButtonsToHide.Remove("Save");
              //  tlsOrders.ButtonsToHide.Remove("Add Addendum");
                tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
            }

            tlsOrders.dtInput = AddChildMenu();
            clsProvider oclsProvider = new clsProvider();
            tlsOrders.ptProvider = oclsProvider.GetPatientProviderName(_patientID);
            tlsOrders.ptProviderId = oclsProvider.GetPatientProvider(_patientID);
            //Memory Leak
            if ((oclsProvider != null))
            {
                oclsProvider.Dispose();
                oclsProvider = null;
            }

            if (SecureMsgEnable == false || SecureMsgUserright == false)
            {
                tlsOrders.ButtonsToHide.Add("SecureMsg");
            }
            GetCoSign(GetLoginUserName(_LoginUserID));
            tlsOrders.IsCoSignEnabled = gblnCoSignFlag;
            //tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
          
            tlsOrders.Visible = true;
            //tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
            tlsOrders.ConnectionString = _dataBaseConnectionString;
            tlsOrders.UserID = _LoginUserID;
            tlsOrders.ptProviderId = _PatientProviderID;
            this.pnlToolStrip.Controls.Add(tlsOrders);
            if (SecureMsgEnable == false || SecureMsgUserright == false)
            {
                tlsOrders.ButtonsToHide.Add("SecureMsg");
            }
    }


        private void gloLabUC_transaction1_gUC_InfoButtonDocumentClicked(string rCode,string openFor,string TemplateName,string _sResourceType)
        {
            //gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders 
            // TemplateID,PatientID,TemplateName,SoureType,openfor

            //Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document


            if (eventLabEducation != null)
            {
                eventLabEducation(Convert.ToInt64(rCode), _patientID, TemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders, openFor, _sResourceType);
            }
            else
            {
                EducationOrder(Convert.ToInt64(rCode), _patientID, TemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders, openFor, _sResourceType);
            }                  
            //frmInfoDocument oDocument = new frmInfoDocument(Convert.ToInt64(rCode), _patientID, openFor);
            //oDocument.Show();
        }

        private void gloLabUC_transaction1_gUC_InfobuttonClicked(string lCode)
        {
            string sAgeUnit = "";
            string sAgeValue = "";
            gloUserControlLibrary.AgeDetail ageDetail = gloUC_PatientStrip1.PatientAge;

            string strSql = "select sLang as Lang from Patient where nPatientID  =" + _patientID;
            DataTable dtPatinfo = new DataTable();
            SqlConnection con = new SqlConnection(_dataBaseConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSql, con);
            da.Fill(dtPatinfo);
            string pLang = "";
            da.Dispose();
            da = null;
            con.Close();
            con.Dispose();
            con = null;
            if (dtPatinfo != null)
                pLang = Convert.ToString(dtPatinfo.Rows[0]["Lang"]);
            
            if (ageDetail.Years != 0)
            {
                sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }
            else if (ageDetail.Months != 0)
            {
            sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }
            else if (ageDetail.Days != 0)
            {
                sAgeUnit = "a";
                sAgeValue = Convert.ToString(ageDetail.Years);
            }

           // clsinfobutton_Orders.Openinfosource(lCode, "2.16.840.1.113883.6.1", pLang, _patientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders.GetHashCode(), GetVisitID(System.DateTime.Now, _patientID), sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, _LoginProviderId);
            clsinfobutton_Orders.GetEducationMaterial_OpenInfobutton(false, gloUC_PatientStrip1.PatientGender, false, sAgeUnit, sAgeValue, pLang, lCode, "2.16.840.1.113883.6.1", "", "Provider", _LoginProviderId, _patientID, GetVisitID(System.DateTime.Now, _patientID), this);
        }

        private void gloLabUC_Transaction1_gUC_ButtonTemplatesClicked(long testId, object sTemplate, bool refreshtemplate = false, int isfinished = 0)
        {
            if (sTemplate != null)
            {
                if (oCurDoc != null)
                {
                    oCurDoc = null;
                    wdOrders.Close();
                }
                //Problem #939 Added Code to Populate particaular Dx and CPT against each test
                if (_OrderParamter.IsEditMode)
                {
                    SaveOrder(0);
                }
                else
                {
                    MenuEvent_Save(0);
                    _OrderParamter.IsEditMode = true;
                }

                pnlOrder.Visible = false;
                pnlWordTemplate.Visible = true;
                ts_LabMain.Visible = false;
                LoadWordToolStrip(isfinished);
                gloEmdeonCommon.gloEMRWord.clsWordDocument clsLabWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                gloEmdeonCommon.gloEMRWord.DocCriteria oCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
                string strFileName = "";
                strFileName = ExamNewDocumentName;
                try
                {
                    strFileName = clsLabWord.GenerateFile(sTemplate, strFileName);

                    //wdOrders.Open(strFileName);
                    object thisObject = (object)strFileName;
                    //  Wd.Application oWordApp = null;
                   
                    gloWord.LoadAndCloseWord.OpenDSO(ref wdOrders, ref thisObject, ref oCurDoc, ref oWordApp);

                    strFileName = (string)thisObject;

                    clsLabWord.CurDocument = oCurDoc;

                    oCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Orders;
                    oCriteria.PatientID = _patientID;
                    oCriteria.PrimaryID = 0;
                    //Added on 20150929-To show ordering provider in ordertemplates for provider signature
                    oCriteria.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
                    oCriteria.VisitID = GetVisitID(System.DateTime.Now, _patientID);
                    clsLabWord.DocumentCriteria = oCriteria;
                    // refreshtemplate added to load liquidlink data for firsttime ,not while modifying
                    if (refreshtemplate == true)
                    {
                        clsLabWord.GetFormFieldData(gloEmdeonCommon.gloEMRWord.enumDocType.None);
                    }
                    oCurDoc = clsLabWord.CurDocument;

                    if ((oCurDoc.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments))
                    {
                        _IsFinish = true;

                        //tlbbtn_Finish.Enabled = true;
                        if (tmrDocProtect != null)
                        {
                            tmrDocProtect.Enabled = true;
                        }
                    }
                    else
                    {
                        tmrDocProtect.Enabled = false;
                    }

                }
                catch //(Exception ex)
                {
                    MessageBox.Show("No template is associated with this Test. Templates can be associated from Edit->Orders & Results Setup.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    oCriteria = null;
                    clsLabWord = null;
                }
            }
            else
            {
                //12-Jun-13 Aniket: Message change
                MessageBox.Show("No template is associated with this Test. Templates can be associated from Edit->Orders & Results Setup.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsOrdestlsOrders_ToolStripClick(Object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "Close":
                  
                    wdOrders.Close();
                    oCurDoc = null;
                    oWordApp = null;
                    pnlOrder.Visible = true;
                    pnlWordTemplate.Visible = false;
                    tlsOrders.Visible = false;
                    ts_LabMain.Visible = true;
                    ts_LabMain.BringToFront();
                    break;
                case "Save":
                    SaveTestTemplate(false);
                    break;
                case "Finish":

                    SetWordObjectEntry(true);
                    
                    if (_IsFinish == true)
                    {

                        SaveTestTemplate(true);
                       
                    }
                    else
                    {
                        SaveTestTemplate(false );
                    }
                     //ts_LabMain.Visible = true;
                    //ts_LabMain.BringToFront();
                    break;

                //case "Addendum":
                //    InsertAddendum();
                //    break;
                case "Save & Finish":
                     SaveTestTemplate(true);
                     pnlOrder.Visible = true;
                     pnlWordTemplate.Visible = false;
                     tlsOrders.Visible = false;
                     ts_LabMain.Visible = true;
                     ts_LabMain.BringToFront();
                    break;
                case "Save & Close":
                    SaveTestTemplate(true);
                    
                    pnlOrder.Visible = true;
                    pnlWordTemplate.Visible = false;
                    tlsOrders.Visible = false;
                    ts_LabMain.Visible = true;
                    ts_LabMain.BringToFront();
                    break;
                case "Print":
                    try
                    {
                     
                    
                        PrintOrder();
                        
                    }
                    finally
                    {
                    
                      
                    }
                        break;
                case "FAX":
                    bnlIsFaxOpened = true;
                    FaxOrder();
                    bnlIsFaxOpened = false;
                    break;
                case "Undo":
                    Undo();
                    break;
                case "Redo":
                    Redo();
                    break;
                case "tblbtn_StrikeThrough":
                    InsertStrike();
                    break;
                case "Scan Documents":
                    ImportDocument(2);
                    break;
                case "Insert File":
                    ImportDocument(1);
                    break;
                case "Insert Sign":
                    if ((oCurDoc == null) == false)
                    {
                        blnSignClick = true;
                        if (_LoginProviderId > 0)
                            InsertProviderSignature(_LoginProviderId);
                        else
                            InsertUserSignature();
                        blnSignClick = false;
                    }
                    break;
                case "Insert Associated Provider Signature":
                    if (oCurDoc != null)
                        //Added on 20150929-To show ordering provider in ordertemplates for provider signature
                        InsertProviderSignature(Convert.ToInt64(cmbProvider.SelectedValue));
                    break;
                case "Insert CoSign":
                    InsertCoSignature();
                    break;
                case "SecureMsg":
                    //string strProviderDirectAddress = "";
                    if ((DirectAddress != "" && DirectAddress != null) || gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation != null)
                    {
                        string sError = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_patientID);
                        if (sError != "")
                        {
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageOrder();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Direct address not set to the login provider. Please set Direct address from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void tlsOrders_ToolStripButtonClick1(object sender, System.EventArgs e, string _Tag)
        {
            try
            {
                if ((oCurDoc == null) == false)
                    InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)); //gloGlobal.clsMISC.IsNumeric(_Tag) ? Convert.ToInt64(_Tag) : 0);
            }
            catch //(Exception ex)
            {
            }

          

            GetCoSign(GetLoginUserName(_LoginUserID));
                tlsOrders.IsCoSignEnabled = gblnCoSignFlag;
                tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
               
     
   
               

            tlsOrders.Visible = true;
            tlsOrders.FormType = WordToolStrip.enumControlType.LabOrder;
            tlsOrders.ConnectionString = _dataBaseConnectionString;
            tlsOrders.UserID = _LoginUserID;
            tlsOrders.ptProviderId = _PatientProviderID;
            this.pnlToolStrip.Controls.Add(tlsOrders);
        }

        
        private bool gblnCoSignFlag = false;

        private void GetCoSign(string username)
        {
            SqlConnection conn = new SqlConnection(_dataBaseConnectionString);
            SqlCommand cmd = default(SqlCommand);
            string _strSQL = string.Empty;

            try
            {
                conn.Open();
                _strSQL = "select bCoSign from User_MST where sLoginName ='" + username.Replace("'", "''") + "'";
                cmd = new SqlCommand(_strSQL, conn);
                bool rslt = Convert.ToBoolean(cmd.ExecuteScalar());
                if (!Information.IsDBNull(rslt))
                {
                    gblnCoSignFlag = rslt;
                }
                else
                {
                    gblnCoSignFlag = false;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            finally
            {
                //code opti
                gblnCoSignFlag = false;
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if ((conn != null))
                {
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        private DataTable AddChildMenu()
        {
            clsProvider oProvider = new clsProvider();


            try
            {
                bool rslt = false;
                rslt = oProvider.CheckSignDelegateStatus();
                if (rslt)
                {
                    //Memory Leak
                    DataTable dt = null;
                    dt = oProvider.GetAllAssignProviders(_LoginUserID );
                    //Memory Leak
                    oProvider.Dispose();
                    oProvider = null;
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if ((oProvider != null))
                {
                    oProvider.Dispose();
                    oProvider = null;
                }
            }
        }

       public void InsertCoSignature()
        {
            try
            {
                if (oCurDoc == null)
                    return;
                gloEmdeonCommon.gloEMRWord.clsWordDocument objWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                gloEmdeonCommon.gloEMRWord.DocCriteria objCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
                objCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Others;
                objCriteria.PatientID = _patientID;
                objCriteria.VisitID = 0;
                objCriteria.PrimaryID = _LoginUserID ;
                //' For inserting coSignature
                objWord.DocumentCriteria = objCriteria;
              
                                string ImagePath = "";
                ImagePath =objWord.getImagePath("User_MST.imgSignature", "Co-Signature");
                objCriteria = null;
                objWord = null;
                ImagePath = Strings.Mid(ImagePath, 1, Strings.Len(ImagePath) - 2);

                if (System.IO.File.Exists(ImagePath))
                {
                    oCurDoc.ActiveWindow.SetFocus();
                    gloEmdeonCommon.gloEMRWord.clsWordDocument oWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                    oWord.CurDocument = oCurDoc;
                    Wd.WdViewType myType = default(Wd.WdViewType);
                    Boolean myLayout = gloWord.LoadAndCloseWord.ChangeToEditView(ref oCurDoc, ref myType);
                    oWord.InsertImage(ImagePath);
                 
                    oWord = null;
                    oCurDoc.Application.Selection.TypeParagraph();
                    oCurDoc.Application.Selection.TypeText(Text: Strings.Format(DateAndTime.Now, "MM/dd/yyyy") + " " + Strings.Format(DateAndTime.Now, "Medium Time"));
                    gloWord.LoadAndCloseWord.RestoreFromEditView(ref oCurDoc, ref myType, myLayout);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature inserted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
            }
            catch (Exception objErr)
            {
                MessageBox.Show(objErr.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertUserSignature()
        {
            try
            {
                if (oCurDoc == null)
                    return;
                gloEmdeonCommon.gloEMRWord.clsWordDocument objWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                gloEmdeonCommon.gloEMRWord.DocCriteria objCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
                objCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Exam;
                objCriteria.PatientID = _patientID;
                objCriteria.VisitID = 0;
                objCriteria.PrimaryID = _LoginUserID;
                objWord.DocumentCriteria = objCriteria;
                String ImagePath;
               // DataTable dtTable = new DataTable();
                ImagePath = objWord.getImagePath("User_MST.imgSignature", "Provider Signature");
                objCriteria = null;
                objWord = null;
                ImagePath = Strings.Mid(ImagePath, 1, Strings.Len(ImagePath) - 2);

                if (File.Exists(ImagePath))
                {
                    oCurDoc.ActiveWindow.SetFocus();
                    gloEmdeonCommon.gloEMRWord.clsWordDocument oWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                    oWord.CurDocument = oCurDoc;
                    Wd.WdViewType myType = default(Wd.WdViewType);
                    Boolean myLayout = gloWord.LoadAndCloseWord.ChangeToEditView(ref oCurDoc, ref myType);
                    oWord.InsertImage(ImagePath);
                    oWord = null;
                    oCurDoc.Application.Selection.TypeParagraph();
                    oCurDoc.Application.Selection.TypeText(Text: "Signed by User :" + " '" + GetLoginUserName(_LoginUserID) + "'. " + Strings.Format(DateAndTime.Now, "MM/dd/yyyy") + " " + Strings.Format(DateAndTime.Now, "Medium Time"));
                    gloWord.LoadAndCloseWord.RestoreFromEditView(ref oCurDoc, ref myType, myLayout);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Order Entry", _patientID , 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(objErr.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertProviderSignature(Int64 ProviderID = 0)
        {
            try
            {
                if (oCurDoc == null)
                    return;
                gloEmdeonCommon.gloEMRWord.clsWordDocument objWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                gloEmdeonCommon.gloEMRWord.DocCriteria objCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
                objCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Exam;
                objCriteria.PatientID = _patientID;
                objCriteria.VisitID = 0;
                objCriteria.PrimaryID = _LoginUserID;
                objCriteria.ProviderID = _LoginProviderId;
                GetLoginUserName(_LoginUserID);
                objWord.DocumentCriteria = objCriteria;
                string[] pSign = objWord.GetProviderSignature(ProviderID, _patientID, GetVisitID(System.DateTime.Now, _patientID), blnSignClick);
                objCriteria = null;
                objWord = null;
                if (pSign[2] == "1")
                {
                    if (File.Exists(pSign[0]))
                    {
                        oCurDoc.ActiveWindow.SetFocus();
                        gloEmdeonCommon.gloEMRWord.clsWordDocument oWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                        oWord.CurDocument = oCurDoc;
                        Wd.WdViewType myType = default(Wd.WdViewType);
                        Boolean myLayout = gloWord.LoadAndCloseWord.ChangeToEditView(ref oCurDoc, ref myType);
                        oWord.InsertImage(pSign[0]);
                        oWord = null;
                        Wd.Range wdRng = oCurDoc.Application.Selection.Range;
                        if (wdRng.Tables.Count > 0)
                        {
                            //oCurDoc.Application.Selection.Move(1)
                            oCurDoc.Application.Selection.EndKey();
                        }
                        oCurDoc.Application.Selection.TypeParagraph();
                        oCurDoc.Application.Selection.TypeText(Text: pSign[1]);
                        gloWord.LoadAndCloseWord.RestoreFromEditView(ref oCurDoc, ref myType, myLayout);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs , gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                }
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            }
        }

        private void SetWordObjectEntry(bool IsFinished, bool isloaded = false)
        {


            if (oCurDoc == null)
            {
                return;
            }
            oCurDoc.ActiveWindow.SetFocus();
            if (IsFinished == true )
            {
               
                
               if (tlsOrders._isFinished_Lab ==false)
                {
                    if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)
                    {
                        gloWord.LoadAndCloseWord.CleanupDoc(ref oCurDoc);
                        gloWord.LoadAndCloseWord.LockFields(ref oCurDoc);
                        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments);
                       // _IsFinish = IsFinished;
                        gloLabUC_Transaction1.SetTemplateIsFinished(Convert.ToBoolean(IsFinished), CurTestID);
                        pnlOrder.Visible = false ;
                        pnlWordTemplate.Visible = true ;
                        tlsOrders.Visible = true;
                        //ts_LabMain.Visible = true;
                        //ts_LabMain.BringToFront();

                    }

                }
            

               else if (MessageBox.Show(" Do you want to Finish ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                   
                    if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)
                    {
                        gloWord.LoadAndCloseWord.CleanupDoc(ref oCurDoc);
                        gloWord.LoadAndCloseWord.LockFields(ref oCurDoc);
                        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments);
                        _IsFinish = IsFinished;
                        gloLabUC_Transaction1.SetTemplateIsFinished(Convert.ToBoolean(IsFinished), CurTestID);
                        pnlOrder.Visible = true ;
                        pnlWordTemplate.Visible = false  ;
                        tlsOrders.Visible = false ;
                        ts_LabMain.Visible = true;
                        ts_LabMain.BringToFront();
                      
                    }

                 
                }
                else
                {

                    _IsFinish = false;
                    pnlOrder.Visible = false ;
                    pnlWordTemplate.Visible = true ;
                    tlsOrders.Visible = true ;
                }
            }
        }
        
        

        private void SaveTestTemplate(bool saveAndClose)
        {
            //bool isExceptionWhileCopingFile = false;
            wdOrders.Focus();
            
            object biFile = null; // clsLabWord.ConvertFiletoBinary(strFileName);
           
                byte[] myBytes = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(ref wdOrders, ref oCurDoc, ref oWordApp, gloSettings.FolderSettings.AppTempFolderPath, false, saveAndClose);
                if (myBytes != null)
                {
                    biFile = myBytes;
                }
           
            gloLabUC_Transaction1.SetWordTemplate(biFile);

            
        }


      

     
        
        private void Undo()
        {
            try
            {
                if ((oCurDoc == null) == false)
                    oCurDoc.Undo();
            }
            catch //(Exception ex)
            {
            }
        }

        private void Redo()
        {
            try
            {
                if ((oCurDoc == null) == false)
                    oCurDoc.Redo();
            }
            catch //(Exception ex)
            {
            }
        }

        private void InsertStrike()
        {
            try
            {
                string strThrough = null;
                if ((oCurDoc != null))
                {
                    if ((oCurDoc.Application.Selection != null))
                    {
                        if (oCurDoc.Application.Selection.Characters.Count - 1 > 0)
                        {
                            strThrough = "Strikethrough by " + GetLoginUserName(_LoginUserID) + " on " + Strings.Format(DateAndTime.Now, "MM/dd/yyyy") + " " + Strings.Format(DateAndTime.Now, "Medium Time");
                            tmrDocProtect.Enabled = false;
                          

                                if (oCurDoc.Application.ActiveDocument.ProtectionType == Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments)
                                oCurDoc.Application.ActiveDocument.Unprotect();
                                oCurDoc.Application.Selection.Range.Font.DoubleStrikeThrough = 1;
                                oCurDoc.Application.Selection.Move(1);
                                oCurDoc.Application.Selection.TypeParagraph();
                                oCurDoc.Application.Selection.Font.DoubleStrikeThrough = 0;
                                oCurDoc.Application.Selection.TypeText(Text: strThrough);
                                oCurDoc.Application.Selection.Move(1);
                                oCurDoc.Application.Selection.TypeParagraph();
                                if (_IsFinish == true)
                                {
                                    if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)
                                    {
                                        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments);

                                    }
                                }
                            }
                        }
                    }
                
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (_IsFinish == true)
                {
                       tmrDocProtect.Enabled = true;
                }
            }
        }

        private void PrintOrder()
        {
            if ((oCurDoc != null))
            {
                GeneratePrintFaxDocument(true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient order printed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
        }

        private void FaxOrder()
        {
            if ((oCurDoc != null))
            {
                GeneratePrintFaxDocument(false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient order Faxed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
        }

        private void CheckAssoPreferredLab(Int64 PreferredID =0)
        {
            if (gloUCLab_OrderDetail.OrderSelected == true)
            {
                DataTable dtfill = new DataTable();
                dtfill = FillAllPreferredLabs(PreferredID);

                if ((dtfill != null))
                {
                    if (dtfill.Rows.Count > 0)
                    {
                        oLabActor_Order1.OrderTests = gloLabUC_Transaction1.GetData();
                        Boolean _isPreferredLabTest = false;
                        if (oLabActor_Order1.OrderTests != null)
                        {
                            DataTable dt1 = new DataTable();
                            DataColumn Column1 = new DataColumn("TestID");
                            Column1.DataType = System.Type.GetType("System.Decimal");
                            dt1.Columns.Add(Column1);

                            DataColumn Column3 = new DataColumn("TestName");
                            Column3.DataType = System.Type.GetType("System.String");
                            dt1.Columns.Add(Column3);

                            for (int j = 0; j <= oLabActor_Order1.OrderTests.Count - 1; j++)
                            {
                                DataRow row = null;
                                row = dt1.NewRow();
                                row["TestName"] = oLabActor_Order1.OrderTests.get_Item(j).TestName;
                                row["TestID"] = oLabActor_Order1.OrderTests.get_Item(j).TestID;
                                if (oLabActor_Order1.OrderTests.get_Item(j).OrderTestResults.Count == 0)
                                {
                                    dt1.Rows.Add(row);
                                }
                            }

                            var list = new List<string>();
                            var listID = new List<string>();
                            foreach (DataRow rowsingrid in dt1.Rows)
                            {
                                string PrvAddedTestID = Convert.ToString(rowsingrid["TestID"]);
                                string PrvAddedTestName = rowsingrid["TestName"].ToString();
                                foreach (DataRow rowsinView in dtfill.Rows)
                                {
                                    if (rowsinView["TEST_ID"].ToString().Equals(rowsingrid["TestID"].ToString()))
                                    {
                                        _isPreferredLabTest = true;
                                        break;
                                    }
                                    else
                                    {
                                        _isPreferredLabTest = false;

                                    }
                                }
                                if (_isPreferredLabTest == false)
                                {
                                    list.Add(PrvAddedTestName);
                                    listID.Add(PrvAddedTestID);
                                }
                            }
                            string ConPrvAddedTestNames = String.Join("',' ", list.ToArray());

                            if (list.Count > 0)
                            {
                                DialogResult drResult = MessageBox.Show("'" + ConPrvAddedTestNames + "' are not associated with selected Preferred Lab." + "\n" + " Do you want to remove the test(s)?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (drResult == DialogResult.Yes)
                                {

                                    for (int i = 0; i <= listID.Count - 1; i++)
                                    {
                                        string TestIDToDelete = listID[i];
                                        gloLabUC_Transaction1.Set_All_DeleteTest(TestIDToDelete);
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private void GeneratePrintFaxDocument(bool IsPrintFlag = true)
        {
            try
            {

                //Boolean _SaveFlag = false;

                if (oCurDoc == null)
                {
                    return;
                }

                //if (oCurDoc.Saved)
                //{
                //    _SaveFlag = true;
                //}

                if (((wdOrders == null) == false && (oWordApp == null) == false))
                {
                    try
                    {
                        gloWord.LoadAndCloseWord.SaveDSO(ref wdOrders, ref oCurDoc, ref oWordApp);

                    }
                    catch (Exception)
                    {
                    }
                }


                //gloEmdeonCommon.gloEMRWord.clsWordDocument clsLabWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                //if (clsLabWord.CheckWordForException() == false)
                //{
                //    clsLabWord = null;
                //    return;
                //}

                //string sFileName = ExamNewDocumentName;
                //oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, false, "", false);
                //wdOrders.Close();
                //AxDSOFramer.AxFramerControl wdTemp = new AxDSOFramer.AxFramerControl();
                //this.Controls.Add(wdTemp);
                //try
                //{
                //    wdTemp.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive;
                //    wdTemp.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen;
                //}
                //catch
                //{
                //}


                //object thisObject = (object)sFileName;

                //gloWord.LoadAndCloseWord.OpenDSO(ref wdTemp, ref thisObject, ref oCurDoc, ref oWordApp);

                //sFileName = (string)thisObject;


                oTempDoc = (Wd.Document)wdOrders.ActiveDocument;


                if (IsPrintFlag)
                {
                    //if (oTempDoc.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments)
                    //    oTempDoc.Unprotect();

                    //clsPrintFAX oPrint = new clsPrintFAX();

                    //Object dresult;
                    //dresult = oPrint.PrintDoc(oTempDoc, _patientID);

                    DocumentPrintOut(ref oCurDoc);

                    //Order Status Set To "Sent"

                    if (blnpriningdone)
                    {
                        if (cmbOrderStatus.SelectedValue.ToString() == "1001")
                        {
                            if (MessageBox.Show("You printed the Template. Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cmbOrderStatus.SelectedValue = 1005;

                                if (_OrderParamter.OrderID > 0)
                                {
                                    UpdateManualOrderStatus("1005", _OrderParamter.OrderID.ToString());
                                }
                            }
                        }
                    }

                    //oPrint = null;
                }
                else
                {
                    FaxOrder(oTempDoc);
                }
                //wdTemp.Close();
                ////Memory Leak
                //this.Controls.Remove(wdTemp);
                //wdTemp.Dispose();

                //LoadWordUserControl(sFileName, false);
                //'Set Cursor at start Postion of Documents
                //if(oTempDoc!=null)
                //    oTempDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory);
                //else
                if (oCurDoc != null) //added for bugid 105024
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory);
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                //if (oTempDoc != null)
                //    oTempDoc.ActiveWindow.View.ShowFieldCodes = false;
                //else
                if (oCurDoc != null)  //added for bugid 105024
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
                if (_IsFinish == true)
                {


                    if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)
                    {
                        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments);
                        
                    }

                }

            }
        }


        //private void GeneratePrintFaxDocument(bool IsPrintFlag = true)
        //{
        //    try
        //    {
        //        gloEmdeonCommon.gloEMRWord.clsWordDocument clsLabWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
        //        if (clsLabWord.CheckWordForException() == false)
        //        {
        //            clsLabWord = null;
        //            return;
        //        }

        //        string sFileName = ExamNewDocumentName;
        //        oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, false, "", false);
        //        wdOrders.Close();
        //       AxDSOFramer.AxFramerControl wdTemp  = new AxDSOFramer.AxFramerControl();
        //        this.Controls.Add(wdTemp);
        //        try
        //        {
        //            wdTemp.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive;
        //            wdTemp.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen;
        //        }
        //        catch 
        //        { 
        //        }
                

        //        object thisObject = (object)sFileName;

        //        gloWord.LoadAndCloseWord.OpenDSO(ref wdTemp, ref thisObject, ref oCurDoc, ref oWordApp);

        //        sFileName = (string)thisObject;

        //        oTempDoc = (Wd.Document)wdTemp.ActiveDocument;
        //        if (IsPrintFlag)
        //        {
        //            if (oTempDoc.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments)
        //                oTempDoc.Unprotect();

        //            //clsPrintFAX oPrint = new clsPrintFAX();

        //            //Object dresult;
        //            //dresult = oPrint.PrintDoc(oTempDoc, _patientID);

        //            DocumentPrintOut(ref oCurDoc);

        //            //Order Status Set To "Sent"

        //            if (blnpriningdone)
        //            {
        //                if (cmbOrderStatus.SelectedValue.ToString() == "1001")
        //                {
        //                    if (MessageBox.Show("You printed the Template. Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                    {
        //                        cmbOrderStatus.SelectedValue = 1005;

        //                        if (_OrderParamter.OrderID > 0)
        //                        {
        //                            UpdateManualOrderStatus("1005", _OrderParamter.OrderID.ToString());
        //                        }
        //                    }
        //                }
        //            }
                           
        //            //oPrint = null;
        //        }
        //        else
        //        {
        //            FaxOrder(oTempDoc);
        //        }
        //        wdTemp.Close();
        //        //Memory Leak
        //        this.Controls.Remove(wdTemp);
        //        wdTemp.Dispose();

        //        LoadWordUserControl(sFileName, false);
        //        //'Set Cursor at start Postion of Documents
        //        oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory);
        //    }
        //    catch //(Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
        //    }
        //}


        private void DocumentPrintOut(ref Wd.Document CurrentDocument)
        {
            //Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            Microsoft.Office.Interop.Word.Document aDoc = null;
            //Bug ID: 00000295 (Printing - EMR)
            //Reason: To resolve reported exception.
            //Description: Set background =false referring link http://www.xtremevbtalk.com/showthread.php?t=55010
            //Changed to resolve the office template issue : #63993: Coding and Unit Testing - 00000112
            gloWord.LoadAndCloseWord myLoadWord = null;
            object Background = false;
            object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
            object Copies = 1;
            object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
            object PrintToFile = false;
            object Collate = false;
            object ActivePrinterMacGX = Type.Missing;
            object ManualDuplexPrint = false;
            object PrintZoomColumn = 1;
            object PrintZoomRow = 1;
            object missing = Type.Missing;

            object oFileFormat = default(object);
            object oFileName = default(object);
            //wordApplication = new Microsoft.Office.Interop.Word.Application();
            
            string sFileName = ExamNewDocumentName;

            int retValue = 0;
            blnpriningdone = false;

            //Bug #63771: 00000624: Patient Statement Print
            //When you click the 'Print' button, nothing happens at all.


            //commented temp
            //if (_isFromPatientAccount)
            //{
            //    if (_gloTemplate.TemplateName.Contains("PatientStatement"))
            //    { sFileName = sFileName.Replace(".docx", ".doc"); }
            //}


            try
            {
                try
                {
                    System.IO.File.Copy(CurrentDocument.FullName, sFileName);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while copy file in DocumentPrintOut. " + ex.Message, false);
                    throw;
                }
                //oFileName = (object)sFileName;

                //oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;

                //aDoc = wordApplication.Documents.Add(oFileName);
                //aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);

                //Bug #63771: 00000624: Patient Statement Print
                //When you click the 'Print' button, nothing happens at all.

                gloWord.LoadAndCloseWord thisLoadWord = GetMyLoadWordApplication();

                try
                {
                    aDoc = thisLoadWord.LoadWordApplication(sFileName);
                    oFileName = (object)sFileName;
                    oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                    aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);

                    //if (!_isFromPatientAccount)
                    //{
                        // gloWord.gloWord.CurrentDoc = aDoc;
                        // gloWord.gloWord.CleanupDocument();
                        gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
                    //}
                    //aDoc.Application.Options.PrintBackground = true;
                    //aDoc.PrintOut(ref Background, ref missing, ref missing, ref missing,
                    //    ref missing, ref missing, ref missing, ref Copies,
                    //    ref missing, ref missing, ref PrintToFile, ref Collate,
                    //    ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                    //    ref PrintZoomRow, ref missing, ref missing);


                    bool DefaultPrinter = gloGlobal.gloTSPrint.IsDefaultPrinterOn();
                    if (DefaultPrinter)
                    {
                        //aDoc.Application.Options.PrintBackground = true;
                        retValue = gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                         ref missing, ref missing, ref missing, ref Copies,
                         ref missing, ref missing, ref PrintToFile, ref Collate,
                         ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                         ref PrintZoomRow, ref missing, ref missing);

                        if (retValue == -1)
                        {
                            blnpriningdone = true;
                        }


                    }
                    else
                    {

                        myLoadWord = GetMyLoadWordApplication();
                        PrintWordDocument(ref myLoadWord, aDoc.FullName.ToString(), true, _patientID, 0, ref aDoc, false, "");
                    }
                    thisLoadWord.CloseWordOnly(ref aDoc);
                }
                catch (Exception ex1)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
                }
                thisLoadWord.CloseApplicationOnly();
                thisLoadWord = null;

                //aDoc.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {

                if (Background != null) { Background = null; }
                if (Range != null) { Range = null; }
                //if (wordApplication != null) { wordApplication.Application.Quit(ref missing, ref missing, ref missing); }
                if (aDoc != null) { aDoc = null; }

                if (oFileName != null) { oFileName = null; }
                if (missing != null) { missing = null; }
                if (oFileFormat != null) { oFileFormat = null; }
                //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                //Delete file created for temporary purpose. 
                if (System.IO.File.Exists(sFileName)) { File.Delete(sFileName); }
                if (!string.IsNullOrEmpty(sFileName)) { sFileName = string.Empty; }
                if (PageType != null) { PageType = null; }
                if (myLoadWord != null)
                {
                    myLoadWord.CloseApplicationOnly();
                    myLoadWord = null;
                }
                CloseMyLoadWordApplication();
            }
        }
        private gloWord.LoadAndCloseWord pLoadWordApplication = null;
        private gloWord.LoadAndCloseWord GetMyLoadWordApplication()

        {
            if (((pLoadWordApplication == null)))
            {
                pLoadWordApplication = new gloWord.LoadAndCloseWord();
                pLoadWordApplication.LoadApplicationOnly();
            }
            else
            {
                if ((pLoadWordApplication.CheckWordApplicationLocked()))
                {
                    pLoadWordApplication = new gloWord.LoadAndCloseWord();
                    pLoadWordApplication.LoadApplicationOnly();
                }
            }
            return pLoadWordApplication;
        }

        private void CloseMyLoadWordApplication()
        {
            if (((pLoadWordApplication == null) == false))
            {
                pLoadWordApplication.CloseApplicationOnly();
                pLoadWordApplication = null;
            }
        }
        
        
        gloPrintDialog.gloPrintDialog oDialogAll = null;
        string strAllOldPrinterName = "";
        System.Drawing.Printing.PrintDocument DefaultPrintDocument = new System.Drawing.Printing.PrintDocument();

        public void PrintWordDocument(ref gloWord.LoadAndCloseWord myLoadWord, string sFileName, bool bIsPrintFlag, long m_patientID, int totalPages,
            ref Wd.Document wdDoc, bool blnPrintCancel, string _PreviousUsedPrinter, int PageNo = 0, bool UseDirectFaxName = false, IWin32Window iOwner = null, int PrintDocno = 0, bool blnShowPrinterDialog = true)
        {
            string oDialogAllPrinterName = "";

            Wd.Document oTempDoc = null;

            string sFileNameForPrintOrFax = "";
            object Missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.WdStatistic PageCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;

            //if ((UseDirectFaxName))
            //{
            //    sFileNameForPrintOrFax = sFileName;
            //    if (!File.Exists(sFileNameForPrintOrFax))
            //    {
            //        MessageBox.Show("Error while printing or faxing. Please try again.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //}
            // else
            // {

            sFileNameForPrintOrFax = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff");

            try
            {
                File.Copy(sFileName, sFileNameForPrintOrFax);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Copy file before printing. Source path:= '" + sFileName + "' ; Destination Path :='" + sFileNameForPrintOrFax + "'" + " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            if (!File.Exists(sFileNameForPrintOrFax))
            {
                MessageBox.Show("Error while printing . Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            if ((totalPages == 0))
            {
                try
                {
                    oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax);
                    oTempDoc.Application.Visible = false;
                    totalPages = oTempDoc.ComputeStatistics(PageCountStat, Missing);
                    //'added for bugid 96435

                    myLoadWord.CloseWordOnly(ref oTempDoc);
                }
                catch (Exception)
                {
                }

            }
            // }


            if (bIsPrintFlag)
            {
                string strpatname = "";

                //if (gblnPageNo == true)
                //{
                //    strpatname = GetPatientDetails(m_patientID);

                //}
                gloWord.clsPrintWordQueue obj = new gloWord.clsPrintWordQueue();
                obj.FilePath = sFileNameForPrintOrFax;


                try
                {

                    //'  Using oDialog As New gloPrintDialog.gloPrintDialog(True)
                    if (oDialogAll == null)
                    {
                        oDialogAll = new gloPrintDialog.gloPrintDialog(true);
                    }
                    // Dim strOldPrinterName As String = String.Empty
                    oDialogAll.ConnectionString = _dataBaseConnectionString;

                    oDialogAll.TopMost = true;
                    oDialogAll.ShowPrinterProfileDialog = true;
                    oDialogAll.ModuleName = "WordPrinting";

                    oDialogAll.RegistryModuleName = "WordPrinting";

                    if (oDialogAll != null)
                    {
                        //Dim printDocument1 As New System.Drawing.Printing.PrintDocument()

                        if ((!gloGlobal.gloTSPrint.isCopyPrint))
                        {

                            oDialogAll.AllowSomePages = true;
                            if ((PrintDocno == 0))
                            {
                                oDialogAll.PrinterSettings = DefaultPrintDocument.PrinterSettings;
                            }

                            oDialogAll.PrinterSettings.ToPage = oDialogAll.PrinterSettings.MaximumPage;

                            oDialogAll.PrinterSettings.FromPage = 1;
                            oDialogAll.PrinterSettings.MaximumPage = oDialogAll.PrinterSettings.MaximumPage;

                            oDialogAll.PrinterSettings.MinimumPage = 1;
                        }
                        oDialogAll.bEnableLocalPrinter = gloGlobal.gloTSPrint.isCopyPrint;



                        if ((PrintDocno == 0))
                        {

                            if ((!gloGlobal.gloTSPrint.isCopyPrint))
                            {


                                try
                                {
                                    strAllOldPrinterName = oDialogAll.PrinterSettings.PrinterName;

                                }
                                catch (Exception)
                                {
                                }
                            }

                            if (oDialogAll.ShowDialog(iOwner) == System.Windows.Forms.DialogResult.OK)
                            {

                                blnpriningdone = true; 
                                if ((oDialogAll.bUseDefaultPrinter == true))
                                {
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = true;
                                }
                                if (gloGlobal.gloTSPrint.isCopyPrint && (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false) == false))
                                {
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = false;
                                }
                                if ((!gloGlobal.gloTSPrint.isCopyPrint))
                                {
                                    oDialogAllPrinterName = oDialogAll.PrinterSettings.PrinterName;
                                    if (oDialogAllPrinterName != strAllOldPrinterName)
                                    {
                                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oDialogAllPrinterName);
                                        Application.DoEvents();
                                    }
                                }
                                gloWord.frmgloPrintQueueController objogloPrintProgressController = new gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings);
                                objogloPrintProgressController.gblnPageNo = true;
                                objogloPrintProgressController.strpatname = strpatname;
                                objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName;
                                objogloPrintProgressController.lstgloTemplate.Add(obj);
                                objogloPrintProgressController.lnPatientId = m_patientID.ToString();
                                
                                Form myCtrl = null;
                                try
                                {
                                    IntPtr handle = this.Handle;// GetActiveWindow();
                                    myCtrl = (Form)Control.FromHandle(handle);
                                }
                                catch (Exception)
                                {
                                   
                                }


                                objogloPrintProgressController.TopMost = true;
                                objogloPrintProgressController.ShowInTaskbar = false;

                                if ((myCtrl != null))
                                {
                                    objogloPrintProgressController.ShowDialog(myCtrl);
                                }
                                else
                                {
                                    objogloPrintProgressController.ShowDialog();
                                }
                                if (objogloPrintProgressController != null)
                                {
                                    objogloPrintProgressController.Dispose();
                                }
                                objogloPrintProgressController = null;

                            }
                            else
                            {
                                blnpriningdone = false; 
                            }
                        }
                        else
                        {
                            if ((oDialogAll.bUseDefaultPrinter == true))
                            {
                                oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = true;
                            }
                            if (gloGlobal.gloTSPrint.isCopyPrint && (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false) == false))
                            {
                                oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = false;
                            }
                            if ((!gloGlobal.gloTSPrint.isCopyPrint))
                            {
                                if ((oDialogAllPrinterName != string.Empty))
                                {
                                    oDialogAll.PrinterSettings.PrinterName = oDialogAllPrinterName;
                                }
                            }
                            gloWord.frmgloPrintQueueController objogloPrintProgressController = new gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings);
                            objogloPrintProgressController.gblnPageNo = true;
                            objogloPrintProgressController.strpatname = strpatname;
                            objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName;
                            objogloPrintProgressController.lstgloTemplate.Add(obj);
                            objogloPrintProgressController.lnPatientId = m_patientID.ToString();

                            if (oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint)
                            {
                                if (oDialogAll.CustomPrinterExtendedSettings.IsShowProgress)
                                {
                                    objogloPrintProgressController.Show();
                                }
                                else
                                {
                                    objogloPrintProgressController.Show();
                                }
                            }
                            else
                            {
                                Form myCtrl = null;
                                try
                                {
                                    IntPtr handle = this.Handle;
                                    myCtrl = (Form)Control.FromHandle(handle);
                                }
                                catch (Exception)
                                {
                                    
                                }


                                objogloPrintProgressController.TopMost = true;
                                objogloPrintProgressController.ShowInTaskbar = false;

                                if ((myCtrl != null))
                                {
                                    objogloPrintProgressController.ShowDialog(myCtrl);
                                }
                                else
                                {
                                    objogloPrintProgressController.ShowDialog();
                                }
                                if (objogloPrintProgressController != null)
                                {
                                    objogloPrintProgressController.Dispose();
                                }
                                objogloPrintProgressController = null;
                            }
                        }
                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (!string.IsNullOrEmpty(_ErrorMessage.Trim()))
                        {
                            string _MessageString = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") + _ErrorMessage;
                            //      gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }


                        MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();

                    if (!string.IsNullOrEmpty(_ErrorMessage.Trim()))
                    {
                        string _MessageString = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") + _ErrorMessage;
                        //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;



                }

                finally
                {
                    if (oDialogAllPrinterName != strAllOldPrinterName)
                    {
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(strAllOldPrinterName);
                        Application.DoEvents();
                    }
                }

            }

        }


        private void FaxOrder(Wd.Document oTempDoc)
        {
            if (mdlFAX.RetrieveFAXDetails(mdlFAX.enmFAXType.PatientOrders, _patientID, "", "", "Test Order", 0, GetVisitID(System.DateTime.Now, _patientID), 0) == false)
                return;
            mdlFAX.CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority;
            //Unprotoct the document
            
            
            if (oTempDoc.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments)
                oTempDoc.Unprotect();
            //Send the document for Printing i.e. to generate the TIFF File
            clsPrintFAX objPrintFAX = new clsPrintFAX(); //strFAXPrinterName
            if (objPrintFAX.FAXDocument(oTempDoc, _patientID, mdlFAX.gstrFAXContactPerson, mdlFAX.gstrFAXContactPersonFAXNo, GetLoginUserName(_LoginUserID), System.DateTime.Now, "Test Order", clsPrintFAX.enmFAXType.PatientOrders,true,true,true,this ) == false)
            {
                if (!string.IsNullOrEmpty(Strings.Trim(objPrintFAX.ErrorMessage)))
                    MessageBox.Show("Unable to send the FAX due to " + objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (cmbOrderStatus.SelectedValue.ToString() == "1001")
                {
                    if (MessageBox.Show("You Faxed the Template. Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmbOrderStatus.SelectedValue = 1005;

                        if (_OrderParamter.OrderID > 0)
                        {
                            UpdateManualOrderStatus("1005", _OrderParamter.OrderID.ToString());
                        }

                    }
                }
            }

            oTempDoc = null;
            if ((objPrintFAX != null))
            {
                //objPrintFAX.Dispose();
                objPrintFAX = null;
            }
        //}
        }

        private void MessageOrder()
        {
            if (oCurDoc != null)
            {
                GenerateSecureMsgDocument();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Messages using Secure Message", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
        }

        private void GenerateSecureMsgDocument()
        {
            try
            {
                gloEmdeonCommon.gloEMRWord.clsWordDocument clsLabWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                if (clsLabWord.CheckWordForException() == false)
                    return;
            //    bool _SaveFlag = false;
                //if (oCurDoc.Saved) ;
                    //_SaveFlag = true;

                string sFileName = ExamNewDocumentName;
                oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, false, "", false);
                wdOrders.Close();
                AxDSOFramer.AxFramerControl wdTemp = new AxDSOFramer.AxFramerControl();
                this.Controls.Add(wdTemp);
                try
                {
                    wdTemp.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive;
                    wdTemp.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen;
                }
                catch
                {
                }
                //wdTemp.Open(sFileName);


                object thisObject = (object)sFileName;
              //  Wd.Application oWordApp = null;
                gloWord.LoadAndCloseWord.OpenDSO(ref wdTemp, ref thisObject, ref oCurDoc, ref oWordApp);

                sFileName = (string)thisObject;

                oTempDoc = (Wd.Document)wdTemp.ActiveDocument;
                if (oTempDoc.ProtectionType == Wd.WdProtectionType.wdAllowOnlyComments)
                {
                    oTempDoc.Unprotect();
                }
                clsPrintFAX oSendDoc = new clsPrintFAX();
                string osenddox = string.Empty;

                osenddox = oSendDoc.SendDoc(oTempDoc, _patientID);
                oSendDoc = null;

                wdTemp.Close();
                //Memory Leak
                this.Controls.Remove(wdTemp);
                wdTemp.Dispose();
                wdTemp = null;

                if (File.Exists(osenddox))
                {
                    string sError = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_patientID);
                    if (sError != "")
                    {
                        MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        InBox.NewMail ofrmSendNewMail = new InBox.NewMail(_patientID, osenddox,0);
                        
                        if (gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation != null)
                        {
                            ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation;
                        }

                        ofrmSendNewMail.EvntGenerateCDA += Raise_EvntGenerateCDA_NormalLab;

                        ofrmSendNewMail.ShowDialog();

                        ofrmSendNewMail.EvntGenerateCDA -= Raise_EvntGenerateCDA_NormalLab;
                        ofrmSendNewMail.Close();
                        ofrmSendNewMail = null;
                    }
                }
                else
                {
                    MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadWordUserControl(sFileName, false);
                //'Set Cursor at start Postion of Documents
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory);
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
            }
        }

     
        private void LoadWordUserControl(string strFileName, bool blnGetData = false)
        {
            gloEmdeonCommon.gloEMRWord.clsWordDocument ObjWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
            gloEmdeonCommon.gloEMRWord.DocCriteria objCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
            //wdOrders.Open(strFileName);

            object thisObject = (object)strFileName;
          //  Wd.Application oWordApp = null;
            gloWord.LoadAndCloseWord.OpenDSO(ref wdOrders, ref thisObject, ref oCurDoc, ref oWordApp);

            strFileName = (string)thisObject;

            if (blnGetData)
            {
                objCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Orders;
                objCriteria.PatientID = _patientID;
                objCriteria.VisitID = GetVisitID(System.DateTime.Now, _patientID);
                objCriteria.PrimaryID = 0;
                ObjWord.DocumentCriteria = objCriteria;
                ObjWord.CurDocument = oCurDoc;
                ObjWord.GetFormFieldData(gloEmdeonCommon.gloEMRWord.enumDocType.None);
                oCurDoc = ObjWord.CurDocument;
                oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
                objCriteria = null;
            }
            else
            {
                ObjWord.CurDocument = oCurDoc;
                ObjWord.HighlightColor();
                oCurDoc = ObjWord.CurDocument;
                oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
                ObjWord = null;
            }
            if (tlsOrders._isFinished_Lab != true )
            {
                _IsFinish = true;
                SetWordObjectEntry(_IsFinish, true);
            }
            else
            {
                _IsFinish = false ;
            }
            //SetWordObject(False) '' COMMENT BY SUDHIR 20090529 '' IT WAS ALLOWING TO EDIT EVEN IF FINISHED ''
            // SetWordObject(blnIsFinish);
        }


        public string gDMSV2TempPath = gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp";

        public void ImportDocument(Int16 nInsertScan)
        {
            //'Insert File - 1
            //'Scan Images - 2
            //'Set focus to Wd object
            if (oCurDoc == null)
                return;
            oCurDoc.ActiveWindow.SetFocus();
            try
            {
                if (nInsertScan == 1)
                {
                    System.Windows.Forms.OpenFileDialog oFileDialogWindow = new System.Windows.Forms.OpenFileDialog();
                    oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf";
                    oFileDialogWindow.FilterIndex = 3;
                    oFileDialogWindow.Title = "Insert External Documents";
                    oFileDialogWindow.Multiselect = false;
                    if (oFileDialogWindow.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        FileInfo oFile = new FileInfo(oFileDialogWindow.FileName);
                        if (oFile.Extension.ToUpper() == (".Doc").ToUpper() ||
                            oFile.Extension.ToUpper() == (".Docx").ToUpper() ||
                            oFile.Extension.ToUpper() == (".txt").ToUpper() ||
                            oFile.Extension.ToUpper() == (".rtf").ToUpper())
                            oCurDoc.Application.Selection.InsertFile(oFile.FullName);
                        oFile = null;
                    }
                    //Memory Leak
                    oFileDialogWindow.Dispose();
                    oFileDialogWindow = null;
                }
                else if (nInsertScan == 2)
                {
                    System.Collections.ArrayList oFiles = new System.Collections.ArrayList();
                    gloEDocumentV3.gloEDocV3Management oEDocument = new gloEDocumentV3.gloEDocV3Management();
                    gloEDocumentV3.gloEDocV3Admin.Connect(_dataBaseConnectionString, GetDMSConnectionString(), gDMSV2TempPath, _LoginUserID, _ClinicID, Application.StartupPath);
                    oEDocument.ShowEScannerForImages(_patientID, out oFiles);
                    oEDocument.Dispose();

                    bool firstFlag = true;
                    int i = 0;
                    Wd.WdViewType myType = default(Wd.WdViewType);
                    Boolean myLayout = gloWord.LoadAndCloseWord.ChangeToEditView(ref oCurDoc, ref myType);
                    for (i = 0; i <= oFiles.Count - 1; i++)
                    {
                        if (File.Exists(oFiles[i].ToString()))
                        {
                            //' SUDHIR 20090619 '' 
                            gloEmdeonCommon.gloEMRWord.clsWordDocument oWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
                            oWord.GetandSetMyFirstFlag(true, firstFlag);
                            oWord.CurDocument = oCurDoc;
                            oWord.InsertImage(oFiles[i].ToString());
                            firstFlag = oWord.GetandSetMyFirstFlag(false, false);
                            oWord = null;
                            oCurDoc.Application.Selection.EndKey();
                            oCurDoc.Application.Selection.InsertBreak();
                        }
                    }
                    gloWord.LoadAndCloseWord.RestoreFromEditView(ref oCurDoc, ref myType, myLayout);
                    for (i = oFiles.Count - 1; i >= 0; i--)
                    {
                        if (File.Exists(oFiles[i].ToString()))
                        {
                            try
                            {
                                FileSystem.Kill(oFiles[i].ToString());
                            }
                            catch
                            {
                            }
                        }
                    }
                    //Memory Leak
                    if ((oFiles != null))
                    {
                        oFiles.Clear();
                        oFiles = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        public string GetDMSConnectionString(string strSQLServerName, string strDatabase, bool isSQLAuthentication, string sUserName, string sPassword)
        {
            string strConnectionString = null;
            if (isSQLAuthentication == false)
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";Integrated Security=SSPI";

            else
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";User ID=" + sUserName + ";Password=" + sPassword + "";

            return strConnectionString;
        }

        public string GetDMSConnectionString()
        {
            return GetDMSConnectionString(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsIsSqlAuthentication, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsUserId, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword);
        }



        private void wdOrders_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Wd.Document)wdOrders.ActiveDocument;
            oWordApp = oCurDoc.Application;
            try
            {
                try
                {
                    oWordApp.WindowSelectionChange -= DDLCBEvent;
                }
                catch //(Exception ex)
                {
                }

                oWordApp.WindowSelectionChange += DDLCBEvent;
            }
            catch //(Exception ex)
            {
            }
            oCurDoc.ActiveWindow.SetFocus();
            if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)  
            oCurDoc.FormFields.Shaded = false;
        }

        private void wdOrders_OnDocumentClosed(object sender, EventArgs e)
        {
          //  oCurDoc = null;
          //  oWordApp = null;
            try
            {
                if ((oCurDoc != null))
                {
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
               
            }
            catch //(Exception ex)
            {
                //UpdateVoiceLog(ex.ToString)
            }
        }

        private void wdOrders_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            try
            {
                if ((oWordApp != null))
                {
                    try
                    {
                        oWordApp.WindowSelectionChange -= DDLCBEvent;
                    }
                    catch //(Exception ex)
                    {
                    }

                    foreach (Wd.RecentFile oFile in oWordApp.RecentFiles)
                    {
                        if (oFile != null)
                        {
                            try
                            {
                                if (oFile.Path == gloSettings.FolderSettings.AppTempFolderPath)
                                {
                                    try
                                    {
                                        oFile.Delete();
                                    }
                                    catch //(Exception ex)
                                    {
                                        // gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                                        // ex = null;
                                    }
                                }
                            }
                            catch //(Exception ex)
                            {
                                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                                //ex = null;
                            }
                        }
                    }

                }
            }
            catch //(Exception ex)
            {
                //UpdateVoiceLog(ex.ToString)
            }
        }

        private void DDLCBEvent(Wd.Selection Sel)
        {
            try
            {
                if (Sel == null)
                {
                    return;
                }
                if (Sel.Start == Sel.End)
                {
                    Wd.Range r = null;
                    try
                    {
                        r = Sel.Range;

                    }
                    catch (Exception )
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }
                    try
                    {
                        r.SetRange(Sel.Start, Sel.End + 1);

                    }
                    catch (Exception )
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }

                    //r.SetRange(Sel.Start, Sel.End + 1);

                    if (r.FormFields != null && r.FormFields.Count >= 1)
                    {
                       // object om = System.Reflection.Missing.Value;
                        Wd.FormField f = null;
                        try
                        {
                            object o = 1;
                            f = r.FormFields.get_Item(ref o);
                            o = null;
                        }
                        catch
                        {

                        }
                        if (f != null)
                        {
                            if (f.Type == Wd.WdFieldType.wdFieldFormCheckBox)
                            {
                                f.CheckBox.Value = !f.CheckBox.Value;
                                object oUnit = Wd.WdUnits.wdCharacter;
                                object oCnt = 1;
                                object oMove = Wd.WdMovementType.wdMove;
                                Sel.MoveRight(oUnit, oCnt, oMove);
                                //Memory Leak
                                oUnit = null;
                                oCnt = null;
                                oMove = null;
                            }
                        }
                     //   om = null;
                        f = null;
                        //o = null;
                    }
                }
            }
            catch //(Exception excp)
            {
            }
        }

        private void GloUC_TemplateTreeControl_Orders_Treeview_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e, string sFilename)
        {
            Int64 nProviderID = 0;

            oCurDoc = (Wd.Document)wdOrders.ActiveDocument;
        //    oCurDoc.Application.ScreenUpdating = false;
            oCurDoc.ActiveWindow.SetFocus();

            //if (gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) == true)
            //{
                oCurDoc.Application.Selection.InsertFile(sFilename, "", false, false, false);  
            //}
            //oCurDoc.Application.Selection.InsertFile(sFilename, "", false, false, false);
            wdOrders.Select();
           // oCurDoc.Application.ScreenUpdating = true;
            gloEmdeonCommon.gloEMRWord.clsWordDocument clsLabWord = new gloEmdeonCommon.gloEMRWord.clsWordDocument();
            gloEmdeonCommon.gloEMRWord.DocCriteria oCriteria = new gloEmdeonCommon.gloEMRWord.DocCriteria();
            clsLabWord.CurDocument = oCurDoc;
            oCriteria.DocCategory = gloEmdeonCommon.gloEMRWord.enumDocCategory.Orders;
            oCriteria.PatientID = _patientID;
            oCriteria.PrimaryID = 0;
            oCriteria.VisitID = GetVisitID(System.DateTime.Now, _patientID);

            if (Int64.TryParse(Convert.ToString(cmbProvider.SelectedValue), out nProviderID))
            { oCriteria.ProviderID = nProviderID; }
            
            clsLabWord.DocumentCriteria = oCriteria;
            clsLabWord.GetFormFieldData(gloEmdeonCommon.gloEMRWord.enumDocType.None);
            oCurDoc = clsLabWord.CurDocument;
        }

        #region "Auto OrderStatus to Ready4Review"

        private bool AutoOrderStatusUpdateforReview(long lOrderID, Int64 intCurrentOrderStatus)
        {
            bool IsSucceed = false;
            try
            {
                // Auto Order status Update to Ready for Review, if Status is New (1001) Or Sent (1005)

                if ((intCurrentOrderStatus == 1001 || intCurrentOrderStatus == 1005) && (initialOrderStatus != 0 && intCurrentOrderStatus != 0))
                {
                    if (initialOrderStatus != intCurrentOrderStatus || gloLabUC_Transaction1.LabModified == true)
                    {
                        // MessageBox.Show("Condition Met");
                        if (lOrderID > 0)
                        {
                            bool Isready = GetOrderStatusReadyforReview(lOrderID);
                            if (Isready == true)
                            {
                                //MessageBox.Show("Ready For update & Condition Met");
                                UpdateManualOrderStatus(Convert.ToString(ReadyForReview), Convert.ToString(lOrderID));
                                IsSucceed = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error during Auto OrderStatusUpdate to Ready For review :" + ex.ToString(), false);
                IsSucceed = false;
            }
            return IsSucceed;
        }

        private bool GetOrderStatusReadyforReview(long lOrderID)
        {
            gloDatabaseLayer.DBLayer oDBObj = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParas = new gloDatabaseLayer.DBParameters();
            bool IsReady = false;
            try
            {
                oDBObj.Connect(false);
                oDBParas.Add(new gloDatabaseLayer.DBParameter("@OrderID", lOrderID, ParameterDirection.Input, SqlDbType.BigInt));
                IsReady = Convert.ToBoolean(oDBObj.ExecuteScalar("GetOrderStatusReadyforReview", oDBParas));
                oDBObj.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error While Getting Order status ready for review :" + ex.ToString(), false);
                IsReady = false;
            }
            finally
            {
                if (oDBObj != null)
                {
                    oDBObj.Disconnect();
                    oDBObj.Dispose();
                }
                if (oDBParas != null)
                {
                    
                    oDBParas.Dispose();
                }
            }
            return IsReady;
        }

        private void UpdateManualOrderStatus(String intOrderStatus, String intOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBparams = new gloDatabaseLayer.DBParameters();

            string strQuery = string.Empty;
            DataTable _dtResult = new DataTable();

            try
            {
                strQuery = "gsp_UpdateOrderStatus";

                oDB.Connect(false);
                oDBparams.Clear();

                oDBparams.Add("@intOrderStatus", intOrderStatus, ParameterDirection.Input, SqlDbType.Int);
                oDBparams.Add("@intOrderID", intOrderID, ParameterDirection.Input, SqlDbType.BigInt, 18);

                oDB.Execute(strQuery, oDBparams);

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                if (oDBparams != null)
                {

                    oDBparams.Dispose();
                }

            }
        }

        #endregion


        public DataTable GetLabTaskUserByProvider(long ProviderId,Int16 SendTaskType,Int16 ResultType)
        {
            SqlConnection Conn = new SqlConnection(_dataBaseConnectionString);
            SqlCommand Cmd;
            SqlDataAdapter adpt = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlParameter objParam = default(SqlParameter);

            try
            {
                Conn.Open();
                Cmd = new SqlCommand("gsp_GetLabTaskUsersForProvider", Conn);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                

                objParam = Cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ProviderId;


                objParam = Cmd.Parameters.Add("@nSendTaskType", SqlDbType.SmallInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = SendTaskType;

                objParam = Cmd.Parameters.Add("@nResultType", SqlDbType.SmallInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ResultType;

                adpt.Fill(dt);
                Conn.Close();
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                return dt;

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((adpt != null))
                {
                    adpt.Dispose();
                    adpt = null;
                }

                if (objParam != null)
                {
                    objParam = null;
                }

                if ((dt != null))
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {

            pnl_btnGroups.Dock = DockStyle.Bottom;
            btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
            btnTests.BackgroundImageLayout = ImageLayout.Stretch;


            pnl_btnTests.Dock = DockStyle.Bottom;
            btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
            btnTests.BackgroundImageLayout = ImageLayout.Stretch;

            pnl_btnRefTest.Dock = DockStyle.Bottom;
            btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
            btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

         
            pnl_btnRadiologyImaging.Dock = DockStyle.Bottom;
            btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongButton;
            btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;

            //28-Apr-14 8020 Orders PRD: Show tests by Others Type
            pnl_btnOthers.Dock = DockStyle.Top;
            btnOthers.BackgroundImage = Properties.Resources.Img_LongOrange;
            btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

            pnlPlannedOrder.Dock = DockStyle.Bottom;
            btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongButton;
            btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;

            gloUCLab_OrderDetail.OrderLabType = "Other";
            gloUCLab_OrderDetail.ReadData();
            gloUCLab_OrderDetail.OrderSelected = false;

            //FillOthers(gloUCLab_OrderDetail.PreferredLabID); 
            FillTestsByType(gloEMRLabTest.OrderTestType.Other, gloUCLab_OrderDetail.PreferredLabID);  
        
        }

        private void gloLabUC_Transaction1_Load(object sender, EventArgs e)
        {

        }

        private void btnOthers_MouseEnter(object sender, EventArgs e)
        {
            btnOthers.BackgroundImage = Properties.Resources.Img_LongYellow;
            btnOthers.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnOthers_MouseLeave(object sender, EventArgs e)
        {
            if (pnl_btnOthers.Dock == DockStyle.Top)
            {
                btnOthers.BackgroundImage = Properties.Resources.Img_LongOrange;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
                btnOthers.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }


        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvider.SelectedValue != null)
            {
                _OrderParamter.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
                gloLabUC_Transaction1.ProviderID=Convert.ToInt64(cmbProvider.SelectedValue);
                gloUCLab_OrderDetail.ResetUsersOnProviderchange();
                
            }
        }

        #region "Call Generate CCDA from Dashboard"        
        public event EventCDA_NormalLab Event_CallCDA;
        public delegate void EventCDA_NormalLab(Int64 PatientID);


        protected virtual void Raise_EvntGenerateCDA_NormalLab(Int64 PatientID)
        {
            if (Event_CallCDA != null)
            {
                Event_CallCDA(PatientID);
            }

        }
        #endregion
        
        //

        public void Navigate(string strstring)
        {

            if (bnlIsFaxOpened == false)
            {
                try
                {
                    if ((oCurDoc != null))
                    {
                        oCurDoc.ActiveWindow.SetFocus();
                        gloEmdeonCommon.gloEMRWord.clsWordDocument.FindAndReplace(MyApp: oCurDoc.Application, FindText: strstring, ReplaceWith: " ", Forward: true, Wrap: 1, Replace: 0, MatchWildCards: false, MatchWholeWord: false);
                       
                    }
                }
                catch (Exception ex2)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
            }
            else
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "frmSelectContactFAXWithFAXCoverPage")
                    {
                        if ((((frmSelectContactFAXWithFAXCoverPage)frm).oCurDoc != null))
                        {
                            try
                            {
                                ((frmSelectContactFAXWithFAXCoverPage)frm).oCurDoc.ActiveWindow.SetFocus();
                                gloEmdeonCommon.gloEMRWord.clsWordDocument.FindAndReplace(MyApp: ((frmSelectContactFAXWithFAXCoverPage)frm).oCurDoc.Application, FindText: strstring, ReplaceWith: " ", Forward: true, Wrap: 1, Replace: 0, MatchWildCards: false, MatchWholeWord: false);
                              
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            }
                        }
                    }
                }
            }
        }

        private void cmbProvider_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Added on 20150929-To show ordering provider in ordertemplates for provider signature
            calltoAddRefreshButtonControl();
        }

        private void chkIncludeTestCode_CheckedChanged(object sender, EventArgs e)
        {
            //28-Jul-16 Aniket: Resolving Bug #98952: gloEMR:Lab Order:Application display Unnecessary Message .
            gloUCLab_OrderDetail.OrderSelected = false;

            if (pnl_btnTests.Dock == DockStyle.Top)
            {
                FillTestsByType(gloEMRLabTest.OrderTestType.LabTests, gloUCLab_OrderDetail.PreferredLabID);
            }

            else if (pnl_btnRefTest.Dock == DockStyle.Top)
            { 
                FillTestsByType(gloEMRLabTest.OrderTestType.Referrals, gloUCLab_OrderDetail.PreferredLabID);
            }

            else if (pnl_btnRadiologyImaging.Dock == DockStyle.Top)
            { 
                FillTestsByType(gloEMRLabTest.OrderTestType.RadiologyImaging, gloUCLab_OrderDetail.PreferredLabID);
            }

            else if (pnl_btnOthers.Dock == DockStyle.Top)
            {
                FillTestsByType(gloEMRLabTest.OrderTestType.Other, gloUCLab_OrderDetail.PreferredLabID);
            }

            else if (pnl_btnGroups.Dock == DockStyle.Top)
            {
                FillGroups_NEW(gloUCLab_OrderDetail.PreferredLabID);
            }

        }

        private void gloLabUC_Transaction1_gUC_AddFormHandlerClick()
        {
            gloEMRGeneralLibrary.frmLab_ContactInformation oContactInformation = new gloEMRGeneralLibrary.frmLab_ContactInformation();
            oContactInformation.nEditID = 0;
            oContactInformation.sEditContactName = "";
            oContactInformation.sEditFirstName = "";
            oContactInformation.sEditMiddleName = "";
            oContactInformation.sEditLastName = "";
            oContactInformation.blnIsModify = true;
            oContactInformation.blnContactType = gloEMRGeneralLibrary.gloEMRActors.LabActor.enumContactType.PreferredLab;
            oContactInformation.ShowDialog(((oContactInformation.Parent == null) ? this : oContactInformation.Parent));
            oContactInformation.Dispose();
            oContactInformation = null;
        }

        private void btnPlannedOrder_Click(object sender, EventArgs e)
        {


            pnlPlannedOrder.Dock = DockStyle.Top;
            btnPlannedOrder.BackgroundImage = Properties.Resources.Img_LongOrange;
            btnPlannedOrder.BackgroundImageLayout = ImageLayout.Stretch;


            pnl_btnRefTest.Dock = DockStyle.Top;
            btnRefTest.BackgroundImage = Properties.Resources.Img_LongOrange;
            btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;

            pnl_btnGroups.Dock = DockStyle.Bottom;
            btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
            btnTests.BackgroundImageLayout = ImageLayout.Stretch;

            pnl_btnTests.Dock = DockStyle.Bottom;
            btnTests.BackgroundImage = Properties.Resources.Img_LongButton;
            btnTests.BackgroundImageLayout = ImageLayout.Stretch;

            //28-Apr-14 8020 Orders PRD: Show tests by Order Type
            pnl_btnRadiologyImaging.Dock = DockStyle.Bottom;
            btnRadiologyImaging.BackgroundImage = Properties.Resources.Img_LongButton;
            btnRadiologyImaging.BackgroundImageLayout = ImageLayout.Stretch;

            //28-Apr-14 8020 Orders PRD: Show tests by Others Type
            pnl_btnOthers.Dock = DockStyle.Bottom;
            btnOthers.BackgroundImage = Properties.Resources.Img_LongButton;
            btnOthers.BackgroundImageLayout = ImageLayout.Stretch;

            pnl_btnRefTest.Dock = DockStyle.Bottom;
            btnRefTest.BackgroundImage = Properties.Resources.Img_LongButton;
            btnRefTest.BackgroundImageLayout = ImageLayout.Stretch;


            gloUCLab_OrderDetail.OrderLabType = "Planned Order";
            gloUCLab_OrderDetail.ReadData();
            gloUCLab_OrderDetail.OrderSelected = false;
            FillTestsByType(gloEMRLabTest.OrderTestType.PlannedOrder,_patientID);


        }

        private void tmrDocProtect_Tick(object sender, EventArgs e)
        {
             try
            {

                if (_IsFinish == true)
                {
                    if (oCurDoc != null)
                    {
                        Wd.TaskPane protectPane = oCurDoc.ActiveWindow.Application.TaskPanes[Wd.WdTaskPanes.wdTaskPaneDocumentProtection];
                        if (protectPane != null)
                        {
                            protectPane.Visible = false;
                            try
                            {
                                Marshal.ReleaseComObject(protectPane);
                                protectPane = null;
                            }
                            catch
                            {
                            }
                        }
                    }
                    //oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory);
                    oCurDoc.Saved = true;
                }

            }
            catch
            { }
            finally
            {
                //  tmrDocProtect.Enabled = true;
            }

        }
        
        
       
       

    }

   
}
