using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloUserControlLibrary;
using System.IO;
using Microsoft.Win32;
using gloEmdeonInterface.Classes;

using System.Runtime.InteropServices;
using System.Diagnostics;
namespace gloEmdeonInterface.Forms
{
    public partial class frmViewSpirometryTests : Form
    {
        #region "Declaration"
        #region "Grid Colum"
        private const Int16 COL_Select = 0;
        private const Int16 COL_OrederPrefix = 1;
        private const Int16 COL_TestType = 2;
        private const Int16 COL_DocumentID = 3;
        private const Int16 COL_DucumentCreatedDate = 4;
        private const Int16 COL_OrderByID = 5;
        private const Int16 COL_OrderByName = 6;
        private const Int16 COL_ReviewdID = 7;
        private const Int16 COL_ReviewdName = 8;
        private const Int16 COL_ModifiedDate = 9;
        private const Int16 COL_Status = 10;
        private const Int16 COL_FileName = 11;
        private const Int16 COL_Intepretation = 12;
        private const Int16 COL_OrederType = 13;
        private const Int16 COL_TestComment = 14;
        private const Int16 COL_TestTakenBy = 15;
        private const Int16 COL_COUNT = 16;
       

        #endregion "Grid Colum"
     
        public delegate void ViewVitalForm(Int64 nVitalID);
        public event ViewVitalForm EvntVitalFormHandler;
        private long _nPatinet_ID = 0;
        private long _ClinicID = 0;
        private long _loginUserId = 0;
        private string _loginUserName = string.Empty; 
        private string gloConnectionString= null;
        private string SpiroConnectionString = string.Empty;
        clsSpiroReports objSpiroReport = null;
        private gloUC_PatientStrip gloUC_PatientStrip1;
        private AxSPIROACTIVEXLib.AxSpiroActiveX axSpiroActiveX1;
        private AxERCACTIVEXLib.AxERCActiveX axERCActiveX1;
        private AxSPIROTRENDXLib.AxSpiroTrendX axSpiroTrendX1;
        private string _gstrMessageBoxCaption = string.Empty;
        private bool IsReportSaved = false;
        private bool IsPostTest = false;
        private long nDocumentID = 0;
        private DataView dvLoadTest = null;
        String[] SelectRec = null;
        Int32 CurrentRecIndex = 0;
        Int32 TotalRecCount = 0;
        public delegate void Vitalseventhandler(object sender, EventArgs e,long nVitalsId);
        public event Vitalseventhandler Vitalsevent;
        private bool IsFormLoded = true; 
        #endregion

        #region "Constructor & destructor"

        public frmViewSpirometryTests(long Pationt_ID, string DeviceConnectionString,string gloEMRConnectionString)
        {
            // check for patient is selected
            if (Pationt_ID != 0 && DeviceConnectionString.Length > 0 && gloEMRConnectionString.Length > 0  )
            {
                
                 InitializeComponent();

                //get patint ID
                _nPatinet_ID = Pationt_ID;
                // get device connection string
                SpiroConnectionString = DeviceConnectionString;
                gloConnectionString = gloEMRConnectionString;

                // commented code to solve 64 machine SDK problem by manoj jadhav on 07142011
                //// check if SDK is Installed or Not
                //if (ISSdkInstalled())
                //{
                //   // make all spiro button accesable on toolstrib
                //    tlbbtnNew.Enabled = true;
                //    tlbbtnPost.Enabled = true;
                //    tlbbtnReview.Enabled = true;
                //    tlbbtnView.Enabled = true;
                //    tlbbtnStatus.Enabled = true;
                //    tlbbtnPrint.Enabled = true;
                //    tlbbtnCalibrate.Enabled = true;
                //    tlbbtnCompare.Enabled = true;
                //    tlbbtnConfigureRace.Enabled = true;
                //    //call spiro activex initilise method
                //    InitializeSpiroActivexComponent();
                //    InitializeERCActiveXComponent();
                //    InitializeSpiroTrendexComponent();

                //}
                //else
                //{
                //    // make all spiro button unaccesable on toolstrib
                //    tlbbtnNew.Enabled = false;
                //    tlbbtnPost.Enabled = false;
                //    tlbbtnReview.Enabled = false;
                //    tlbbtnView.Enabled = false;
                //    tlbbtnStatus.Enabled = false;
                //    tlbbtnPrint.Enabled = false;
                //    tlbbtnCalibrate.Enabled = false;
                //    tlbbtnCompare.Enabled = false;
                //    tlbbtnConfigureRace.Enabled = false;
                //    tlbbtnSettings.Enabled = false;
                    
                //}

                // added code to solve 64 machine SDK problem by manoj jadhav on 07142011
                if (InitializeSpiroActivexComponent() && InitializeERCActiveXComponent() && InitializeSpiroTrendexComponent())
                {
                    //  make all spiro button accesable on toolstrib
                    tlbbtnNew.Enabled = true;
                    tlbbtnPost.Enabled = true;
                    tlbbtnReview.Enabled = true;
                    tlbbtnView.Enabled = true;
                    tlbbtnStatus.Enabled = true;
                    tlbbtnPrint.Enabled = true;
                    tlbbtnCalibrate.Enabled = true;
                    tlbbtnCompare.Enabled = true;
                    tlbbtnConfigureRace.Enabled = true;





                }
                else
                {
                    // make all spiro button unaccesable on toolstrib
                    tlbbtnNew.Enabled = false;
                    tlbbtnPost.Enabled = false;
                    tlbbtnReview.Enabled = false;
                    tlbbtnView.Enabled = false;
                    tlbbtnStatus.Enabled = false;
                    tlbbtnPrint.Enabled = false;
                    tlbbtnCalibrate.Enabled = false;
                    tlbbtnCompare.Enabled = false;
                    tlbbtnConfigureRace.Enabled = false;
                    tlbbtnSettings.Enabled = false;
                    this.ResumeLayout(true);
                }

                             
                SetPationtData();

                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"].Length > 0)
                    {
                        _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { _gstrMessageBoxCaption = "gloEMR"; }
                #endregion

                // get current login user id an user name
                long.TryParse(Convert.ToString(appSettings["UserID"]), out _loginUserId);
                _loginUserName = Convert.ToString(appSettings["UserName"]);
                
                
            }
            else
            {
                MessageBox.Show("Patient is not selected on dashboard", _gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information  );
                this.Close();  
            }
         
         
        }

        #endregion

        #region "Events"
       
        // form load evnt
        private void frmSpiroTest_Load(object sender, EventArgs e)
        {
            IsFormLoded = false; 

            //// get glo ERM Connection string
            //if (System.Configuration.ConfigurationManager.AppSettings["DataBaseConnectionString"].ToString() != string.Empty)
            //    gloConnectionString = System.Configuration.ConfigurationManager.AppSettings["DataBaseConnectionString"].ToString();
         
              
            // get clinic ID
            if (System.Configuration.ConfigurationManager.AppSettings["ClinicID"].ToString() != string.Empty)
                          long.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ClinicID"]), out _ClinicID);

            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _nPatinet_ID, gloConnectionString, _gstrMessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            // get spirometry connection string
            //if (ISDeviceConnectionString())
            //{
               cmbTestType.SelectedIndex = 0;
                // SET current dat as to date for filter
               dtpTodate.Value = DateTime.Now;
                // set 30 day before date as from date
               dtpFromDate.Value =dtpTodate.Value.AddDays(-30) ;
                // set page size
               AddPagingSize();
               // get test data
               //CurrentRecIndex = TotalRecCount - 1; 
               AddSelectRange();
               FillTestView();
            //}
            //else
            //   this.Close(); 
          
            // change interpretation background to white
            txtInterpreatation.BackColor = Color.White;
            IsFormLoded = true; 
         }
     
        // menustrip clicked event
        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "NewTest":
                    {
                       
                        // uncheck all grid colum
                        //for (int row = 1; row < c1Spiro.Rows.Count; row++)
                        //{
                        //    c1Spiro.SetData(row, COL_Select, "False");
                        //}
                        nDocumentID = 0;
                        StartNewTest(); 
                        break;
                    }
                case "Post":
                    {
                       //Int32 nColumChecked = NoOfColumChecked();
                       //if (nColumChecked == 1)
                       //{
                       //    if (long.TryParse( Convert.ToString(c1Spiro.GetData(GetSectedRowIndex(), COL_DocumentID)), out nDocumentID)== true)
                       //    {
                       //        PostTest(nDocumentID, false);
                       //    }
                       // }
                       //else if (nColumChecked == 0)
                       //    MessageBox.Show("You must select one at least record to conduct post test.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                       //else if (nColumChecked > 1)
                       //    MessageBox.Show("You must select one record to conduct post test.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (c1Spiro.RowSel > 0)
                        {
                            nDocumentID = 0;
                            long.TryParse(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_DocumentID)), out nDocumentID);
                            if (nDocumentID != 0)
                            {
                                PostTest(nDocumentID, false);

                            } 

                        } 
                        else
                            MessageBox.Show("No test selected to conduct post test.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                       break;
                    }
                case "Review":
                    {
                        //Int32 nColumChecked = NoOfColumChecked();
                        //if (nColumChecked == 1)
                        //{
                        //    if (long.TryParse(Convert.ToString(c1Spiro.GetData(GetSectedRowIndex(), COL_DocumentID)), out nDocumentID) == true)
                        //    {
                        //        ShowReport(nDocumentID,true);
                        //    }
                        //}
                        //else if (nColumChecked == 0)
                        //    MessageBox.Show("You must select at least one record to review report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //else if (nColumChecked > 1)
                        //    MessageBox.Show("You must select one record to review report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (c1Spiro.RowSel > 0)
                        {
                            nDocumentID = 0;
                            long.TryParse(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_DocumentID)), out nDocumentID);
                            if (nDocumentID != 0)
                            {
                                ShowReport(nDocumentID, true);
                            }

                        } 
                        else
                            MessageBox.Show("No test selected to review test.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
                case "View":
                    {
                        //Int32 nColumChecked = NoOfColumChecked();
                        //if (nColumChecked == 1)
                        //{
                        //    if (long.TryParse(Convert.ToString(c1Spiro.GetData(GetSectedRowIndex(), COL_DocumentID)), out nDocumentID) == true)
                        //    {
                        //        ShowReport(nDocumentID, false);
                        //    }
                        //}
                        //else if (nColumChecked == 0)
                        //    MessageBox.Show("You must select at least one record to view report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //else if (nColumChecked > 1)
                        //    MessageBox.Show("You must select one record to view report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (c1Spiro.RowSel > 0)
                        {
                            nDocumentID = 0;
                            long.TryParse(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_DocumentID)), out nDocumentID);
                            if (nDocumentID != 0)
                            {
                                ShowReport(nDocumentID, false);
                            } 

                        } 
                        else
                            MessageBox.Show("No test selected to view test.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                        break;
                    }
                case "Compare":
                    {
                        Int32 nColumChecked = NoOfColumChecked();
                        if (nColumChecked  > 1)
                            CompaireReport();
                        else
                            MessageBox.Show("You must select minimum two records to perform comparison report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        break;
                    }
                case "Status":
                    {
                        //Int32 nColumChecked = NoOfColumChecked();
                        //if (nColumChecked == 1)
                        //{
                        //    if (long.TryParse(Convert.ToString(c1Spiro.GetData(GetSectedRowIndex(), COL_DocumentID)), out nDocumentID) == true)
                        //    {
                        //          Status(nDocumentID);
                        //   }
                        //}
                        //else if (nColumChecked == 0)
                        //    MessageBox.Show("You must select at least one record to view/modify report status.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //else if (nColumChecked > 1)
                        //    MessageBox.Show("You must select one record to view/modify report status. ", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (c1Spiro.RowSel > 0)
                        {
                            nDocumentID = 0;
                            long.TryParse(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_DocumentID)), out nDocumentID);
                            if (nDocumentID != 0)
                            {
                                Status(nDocumentID);
                            } 

                        } 
                        else
                            MessageBox.Show("No test selected to view/modify test status.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                          
                        break;
                    }
                case "Calibrate Device":
                    {
                        axSpiroActiveX1.StartCalibration();
                        break;
                    }
                case "Print":
                    {
                        //Int32 nColumChecked = NoOfColumChecked();
                        //if (nColumChecked == 1)
                        //{
                        //    if (long.TryParse(Convert.ToString(c1Spiro.GetData(GetSectedRowIndex(), COL_DocumentID)), out nDocumentID) == true)
                        //    {
                        //        PrintReport(nDocumentID);
                        //    }
                        //}
                        //else if (nColumChecked == 0)
                        //    MessageBox.Show("You must select at least one record to print report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //else if (nColumChecked > 1)
                        //    MessageBox.Show("You must select one record to print report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (c1Spiro.RowSel > 0)
                        {
                            nDocumentID = 0;
                            long.TryParse(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_DocumentID)), out nDocumentID);
                            if (nDocumentID != 0)
                            {
                                PrintReport(nDocumentID);
                            }

                        } 
                        else
                            MessageBox.Show("No test selected to print test report.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        
                        break;

                    }
                case "Settings":
                    {

                       axSpiroActiveX1.DoConfiguration();
                       break;
                    }
                case "Configure Race":
                    {
                        frmSpirometryRaceMappingsConfiguration frmobj = new frmSpirometryRaceMappingsConfiguration(SpiroConnectionString, gloConnectionString); 
                        frmobj.ShowDialog(this);
                        frmobj.Dispose();
                        frmobj = null;
                        break;
                    }
                case "Close":
                    {
                        this.Close();
                        break;
                    }
               
            }

        }

        //axSpiroActiveX ready event
        private void axSpiroActiveX1_SpiroReportReady(object sender, EventArgs e)
        {

            if (IsPostTest)
            {
                nDocumentID = UpdateReport(nDocumentID, true);
                IsPostTest = false; 
            }
            else 
            {
                nDocumentID = SaveTestReport();

            }

            if (nDocumentID != 0)
            {
                IsReportSaved = true;
                // refresh select range combo
                AddSelectRange();
                FillTestView();
                //SelectColum(nDocumentID);
            }
            
       }

       //axSpiroActiveX ending event
        private void axSpiroActiveX1_SpiroEnding(object sender, EventArgs e)
        {
           if (IsReportSaved == true)
            {
                IsReportSaved = false; 
                axERCActiveX1.SetPermissions(true, true, true, true);
                axERCActiveX1.SetReviewedBy(_loginUserName);
                axERCActiveX1.SetSpiroPermissions(0, 1, 1, 1, 1, 0);
                axERCActiveX1.StartReview();
            }
           if (objSpiroReport != null)
           {
                    objSpiroReport.Dispose();
                    objSpiroReport = null;
           }
            //SelectColum(nDocumentID);
            IsPostTest = false; 
            Microsoft.VisualBasic.Interaction.AppActivate(Application.ProductName);
            this.Focus();
        }

        //axERCActiveX  report datachanged event
        private void axERCActiveX1_ReportDataChanged(object sender, EventArgs e)
        {
            
            if (nDocumentID > 0)
            {
                nDocumentID = UpdateReport(nDocumentID, false);
                AddSelectRange();
                FillTestView();
                SelectColum(nDocumentID);
               
            }
         }

        // axERCActiveX Dospiropost event
        private void axERCActiveX1_DoSpiroPostClicked(object sender, EventArgs e)
        {
            clsSpiroReportmanager ClsRptManager = null;
            try
            {
                ClsRptManager = new clsSpiroReportmanager(SpiroConnectionString);
                if (ClsRptManager.getSpiroObject(nDocumentID).Interpretation != axERCActiveX1.GetSpirometryInterpretation().Trim())
                {
                    nDocumentID = UpdateReport(nDocumentID, false);
                    AddSelectRange();
                    FillTestView();
                    SelectColum(nDocumentID);

                }

                axERCActiveX1.EndReview();
                PostTest(nDocumentID,false);

            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
              gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.axERCActiveX1_DoSpiroPostClicked() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (ClsRptManager != null)
                {
                    ClsRptManager.Dispose();
                    ClsRptManager = null;
                }
            }
        
        }

        // axERCActiveX return to spirotest event
        private void axERCActiveX1_ReturnToSpiroTest(object sender, EventArgs e)
        {

          clsSpiroReportmanager ClsRptManager = null;
            try
            {
                ClsRptManager = new clsSpiroReportmanager(SpiroConnectionString);
                if (ClsRptManager.getSpiroObject(nDocumentID).Interpretation != axERCActiveX1.GetSpirometryInterpretation().Trim())
                {
                    nDocumentID = UpdateReport(nDocumentID, false);
                    AddSelectRange();
                    FillTestView();
                    SelectColum(nDocumentID);

                }
                axERCActiveX1.EndReview();
                PostTest(nDocumentID,true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.axERCActiveX1_ReturnToSpiroTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (ClsRptManager != null)
                {
                    ClsRptManager.Dispose();
                    ClsRptManager = null;
                }
            }

        }

        // commented code to get focus back to application problem by manoj jadhav on 07142011
        //// code for setting focus back to application
        //[DllImport( "user32.dll" )]
        //public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow); 
        //public const int SW_RESTORE = 9;
        //public const int SW_MAXIMIZE = 3;

        //public void SwitchToCurrent() 
        //{  
        //     IntPtr hWnd = IntPtr.Zero;
        //     Process process = Process.GetCurrentProcess() ;
        //     Process[] processes = Process.GetProcessesByName( process.ProcessName );  

        //     foreach ( Process _process in processes ) 
        //     {    
        //         if (_process.Id == process.Id &&  _process.MainModule.FileName == process.MainModule.FileName   &&  _process.MainWindowHandle != IntPtr.Zero ) 
        //         {     
        //             hWnd = _process.MainWindowHandle;    
        //             HandleRef o = new HandleRef(null, hWnd);
        //             ShowWindowAsync(o, SW_RESTORE);
        //             ShowWindowAsync(o, SW_MAXIMIZE);
        //             break;  
        //         }  //if
        //      } // for
        //}

        ////end code for setting focus back to application 


        // axERCActiveX end review event
        private void axERCActiveX1_EndReview(object sender, EventArgs e)
        {
            this.Focus();
            SelectColum(nDocumentID);
           // SwitchToCurrent();
            Microsoft.VisualBasic.Interaction.AppActivate(Application.ProductName);
          
        }

        // gridview selected row change event
        private void c1Spiro_SelChange(object sender, EventArgs e)
        {
            SetInterpratation();
        }

        // datetime from date value changed
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (IsFormLoded)
            {
                AddSelectRange();
                FillTestView(); 
            }
               
        }
        
        //datetime todate value changed
        private void dtpTodate_ValueChanged(object sender, EventArgs e)
        {
            if (IsFormLoded)
            {
                AddSelectRange();
                FillTestView(); 
            }
        }
      
        // combobox value changed
        private void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFormLoded)
            {
                AddSelectRange();
                FillTestView();
            }
                
           
        }

        private void ChkUseDateRange_CheckStateChanged(object sender, EventArgs e)
        {
            if (IsFormLoded)
            {
                dtpFromDate.Enabled = ChkUseDateRange.Checked;
                dtpTodate.Enabled = ChkUseDateRange.Checked;
                AddSelectRange();
                FillTestView();
            }
        }

        public void CallVitalsForm(long VitalsId)
        {
            Vitalsevent(this, EventArgs.Empty, VitalsId);
        }
      
        #endregion
        
               
        #region "Function and Methods"

        /// <summary>
        /// method to make Check Selected Row
        /// </summary>
        /// <param name="DocumentID"></param>
        private void SelectColum(long DocumentID)
        {
            long TempDocID = 0;
            for (int irow = 1; irow < c1Spiro.Rows.Count; irow++)
            {
                if (long.TryParse(Convert.ToString(c1Spiro.GetData(irow, COL_DocumentID)), out TempDocID))
                {
                    if (TempDocID == DocumentID)
                    {
                       // c1Spiro.SetData(irow, COL_Select, "true");
                       // c1Spiro.RowSel = irow;
                        c1Spiro.Select(irow, COL_DocumentID);
                        c1Spiro.Refresh();
                        break;
                    }
                }// end it try parse
            }// end of for

        }


        /// <summary>
        ///// function to check SDK is Installed On System
        ///// </summary>
        //private bool ISSdkInstalled()
        //{
        //    bool _ISSdkInstalled = false;
        //    String SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products";
        //    RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey);
        //    foreach (string SoftwareName in rk.GetSubKeyNames())
        //    {
        //        var name = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(SoftwareName).OpenSubKey("InstallProperties").GetValue("DisplayName");
        //        //if (name.ToString() == "Midmark IQ Devices SDK 8.4.1")
        //        if (Convert.ToString(name).Contains("Midmark"))
        //        {
        //            _ISSdkInstalled = true;
        //        }
               
        //    }

        //    return _ISSdkInstalled;
        //}
        
        /// <summary>
        /// method to intilize spiro activex control
        /// </summary>

        // Change Method Type to solve 64 machine SDK problem by manoj jadhav on 07142011
        private bool InitializeSpiroActivexComponent()
        {
            bool _InitializeSpiroActivexComponent = false;
            System.ComponentModel.ComponentResourceManager resources = null;
            try
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSpirometryTests));
                this.axSpiroActiveX1 = new AxSPIROACTIVEXLib.AxSpiroActiveX();
                ((System.ComponentModel.ISupportInitialize)(this.axSpiroActiveX1)).BeginInit();
                this.SuspendLayout();
                this.axSpiroActiveX1.Enabled = true;
                this.axSpiroActiveX1.Visible = false;
                this.axSpiroActiveX1.Location = new System.Drawing.Point(308, 69);
                this.axSpiroActiveX1.Name = "axSpiroActiveX1";
                this.axSpiroActiveX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpiroActiveX1.OcxState")));
                this.axSpiroActiveX1.Size = new System.Drawing.Size(100, 123);
                this.axSpiroActiveX1.TabIndex = 0;
                this.axSpiroActiveX1.SpiroReportReady += new System.EventHandler(this.axSpiroActiveX1_SpiroReportReady);
                this.axSpiroActiveX1.SpiroEnding += new System.EventHandler(this.axSpiroActiveX1_SpiroEnding);
                this.Controls.Add(this.axSpiroActiveX1);
                ((System.ComponentModel.ISupportInitialize)(this.axSpiroActiveX1)).EndInit();
                this.ResumeLayout(false);
                _InitializeSpiroActivexComponent = true;
            }
            catch //(Exception ex)
            {
                this.Controls.Remove(this.axSpiroActiveX1);
                axSpiroActiveX1.Dispose();
                _InitializeSpiroActivexComponent = false;
            }
            finally
            {
                if (resources != null)
                {
                    resources = null;
                }

            }

            return _InitializeSpiroActivexComponent;

        }

        /// <summary>
        /// method to initilise ERXactivex control
        /// </summary>
        
        // Change Method Type to solve 64 machine SDK problem by manoj jadhav on 07142011
        private bool InitializeERCActiveXComponent()
        {
            bool _InitializeERCActiveXComponent = false;
            System.ComponentModel.ComponentResourceManager resources = null;
            try
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSpirometryTests));
                this.axERCActiveX1 = new AxERCACTIVEXLib.AxERCActiveX();
                ((System.ComponentModel.ISupportInitialize)(this.axERCActiveX1)).BeginInit();
                this.SuspendLayout();
                this.axERCActiveX1.Enabled = true;
                this.axERCActiveX1.Visible = false;
                this.axERCActiveX1.Location = new System.Drawing.Point(900, 13);
                this.axERCActiveX1.Name = "axERCActiveX1";
                this.axERCActiveX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axERCActiveX1.OcxState")));
                this.axERCActiveX1.Size = new System.Drawing.Size(53, 26);
                this.axERCActiveX1.TabIndex = 40;
                this.axERCActiveX1.ReportDataChanged += new System.EventHandler(this.axERCActiveX1_ReportDataChanged);
                //Post-Bd
                this.axERCActiveX1.DoSpiroPost += new System.EventHandler(this.axERCActiveX1_DoSpiroPostClicked);
                //Pre-Bd
                this.axERCActiveX1.ReviewEnding += new System.EventHandler(this.axERCActiveX1_EndReview);  
                 
                this.axERCActiveX1.ReturnToSpiroTest += new System.EventHandler(this.axERCActiveX1_ReturnToSpiroTest);

                this.Controls.Add(this.axERCActiveX1);
                ((System.ComponentModel.ISupportInitialize)(this.axERCActiveX1)).EndInit();
                this.ResumeLayout(false);
                _InitializeERCActiveXComponent = true;

            }
            catch //(Exception ex)
            {
                this.Controls.Remove(this.axERCActiveX1);
                axERCActiveX1.Dispose();
                _InitializeERCActiveXComponent = false;

            }
            finally
            {
                if (resources != null)
                {
                    resources = null;
                }

            }
            return _InitializeERCActiveXComponent;
        }

        /// <summary>
        /// method to intilieze trendex spiro control
        /// </summary>
        // Change Method Type to solve 64 machine SDK problem by manoj jadhav on 07142011
        private bool InitializeSpiroTrendexComponent()
        {
            bool _InitializeSpiroTrendexComponent = false;
            System.ComponentModel.ComponentResourceManager resources = null;
            try
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSpirometryTests));
                this.axSpiroTrendX1 = new AxSPIROTRENDXLib.AxSpiroTrendX();
                ((System.ComponentModel.ISupportInitialize)(this.axSpiroTrendX1)).BeginInit();
                this.SuspendLayout();
                this.axSpiroTrendX1.Enabled = true;
                this.axSpiroTrendX1.Visible = false;
                this.axSpiroTrendX1.Location = new System.Drawing.Point(1011, 4);
                this.axSpiroTrendX1.Name = "axSpiroTrendX1";
                this.axSpiroTrendX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpiroTrendX1.OcxState")));
                this.axSpiroTrendX1.Size = new System.Drawing.Size(37, 35);
                this.axSpiroTrendX1.TabIndex = 42;
                this.Controls.Add(this.axSpiroTrendX1);
                ((System.ComponentModel.ISupportInitialize)(this.axSpiroTrendX1)).EndInit();
                this.ResumeLayout(false);
                _InitializeSpiroTrendexComponent = true;
            }
            catch //(Exception ex)
            {
                this.Controls.Remove(this.axSpiroTrendX1);
                axSpiroTrendX1.Dispose();
                _InitializeSpiroTrendexComponent = false;
            }
            finally
            {
                if (resources != null)
                {
                    resources = null;
                }

            }
            return _InitializeSpiroTrendexComponent;
        }

        /// <summary>
        /// method to set data to patiient control
        /// </summary>
        private void SetPationtData()
        {

            gloUC_PatientStrip1 = new gloUC_PatientStrip();
            pnlMain.Controls.Add(gloUC_PatientStrip1);
            gloUC_PatientStrip1.Padding = new Padding(3, 0, 3, 0);
            gloUC_PatientStrip1.Dock = DockStyle.Top;
            gloUC_PatientStrip1.ShowDetail(_nPatinet_ID, gloUC_PatientStrip.enumFormName.PatientSummary, 0, 0, 0, false, false, false, string.Empty, false);
         
       
        }
      


        /// <summary>
        /// method to design grid
        /// </summary>
       private void DesignGrid()
        {

            try
            {
               // c1Spiro.Clear();
                c1Spiro.DataSource = null;
                c1Spiro.Clear();
                c1Spiro.Cols.Count = COL_COUNT;
                c1Spiro.Rows.Count = 1;
                c1Spiro.Rows.Fixed = 1;
                 
              
                c1Spiro.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
                // setfont
                c1Spiro.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1Spiro.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1Spiro.BackColor = Color.White;
                c1Spiro.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                c1Spiro.ScrollBars = ScrollBars.Both;
                SetGridStyle(c1Spiro); 
                 


                // set visibility of column
                c1Spiro.Cols[COL_Select].Visible = true;
                c1Spiro.Cols[COL_DocumentID].Visible = false;
                c1Spiro.Cols[COL_DucumentCreatedDate].Visible = true;
                c1Spiro.Cols[COL_OrderByID].Visible = false;
                c1Spiro.Cols[COL_OrderByName].Visible = true;
                c1Spiro.Cols[COL_ReviewdID].Visible = false;
                c1Spiro.Cols[COL_ReviewdName].Visible = true;
                c1Spiro.Cols[COL_ModifiedDate].Visible = true;
                c1Spiro.Cols[COL_Status].Visible = true;
                c1Spiro.Cols[COL_FileName].Visible = false;
                // set column type
                c1Spiro.Cols[COL_Select].DataType = typeof(bool);
                c1Spiro.AllowEditing = true;
                c1Spiro.Cols[COL_Intepretation].Visible = false;
                c1Spiro.Cols[COL_OrederType].Visible = false;
                c1Spiro.Cols[COL_OrederPrefix].Visible = true;
                c1Spiro.Cols[COL_TestType].Visible = true;
                c1Spiro.Cols[COL_TestComment].Visible = true;
                c1Spiro.Cols[COL_TestTakenBy].Visible = true; 

                // set column editing

                c1Spiro.Cols[COL_Select].AllowEditing = true;
                c1Spiro.Cols[COL_DocumentID].AllowEditing = false;
                c1Spiro.Cols[COL_DucumentCreatedDate].AllowEditing = false;
                c1Spiro.Cols[COL_OrderByID].AllowEditing = false;
                c1Spiro.Cols[COL_OrderByName].AllowEditing = false;
                c1Spiro.Cols[COL_ReviewdID].AllowEditing = false;
                c1Spiro.Cols[COL_ReviewdName].AllowEditing = false;
                c1Spiro.Cols[COL_ModifiedDate].AllowEditing = false;
                c1Spiro.Cols[COL_Status].AllowEditing = false;
                c1Spiro.Cols[COL_FileName].AllowEditing = false;
                c1Spiro.Cols[COL_Intepretation].AllowEditing = false;
                c1Spiro.Cols[COL_OrederType].AllowEditing = false;
                c1Spiro.Cols[COL_OrederPrefix].AllowEditing = false;
                c1Spiro.Cols[COL_TestType].AllowEditing = false;
                c1Spiro.Cols[COL_TestComment].AllowEditing = false;
                c1Spiro.Cols[COL_TestTakenBy].AllowEditing = false;


                //set Heading
                c1Spiro.SetData(0, COL_Select, "Compare");
                c1Spiro.SetData(0, COL_DocumentID, "Document ID");
                c1Spiro.SetData(0, COL_DucumentCreatedDate, "Test Date");
                c1Spiro.SetData(0, COL_OrderByID, "Ordered ID");
                c1Spiro.SetData(0, COL_OrderByName, "Ordered By");
                c1Spiro.SetData(0, COL_ReviewdID, "Reviewed ID");
                c1Spiro.SetData(0, COL_ReviewdName, "Reviewed By");
                c1Spiro.SetData(0, COL_ModifiedDate, "Modified Date");
                c1Spiro.SetData(0, COL_Status, "Status");
                c1Spiro.SetData(0, COL_FileName, "File Name");
                c1Spiro.SetData(0, COL_Intepretation, "Interpretation");
                c1Spiro.SetData(0, COL_OrederType, "Ordered Type");
                c1Spiro.SetData(0, COL_OrederPrefix, "Test ID");
                c1Spiro.SetData(0, COL_TestType, "Test Type");
                c1Spiro.SetData(0, COL_TestComment, "Test Detail");
                c1Spiro.SetData(0, COL_TestTakenBy, "Test Taken By");

                
                // set width
                c1Spiro.Cols[COL_Select].Width = 60;
                c1Spiro.Cols[COL_DocumentID].Width = 50;
                c1Spiro.Cols[COL_DucumentCreatedDate].Width = 150;
                c1Spiro.Cols[COL_OrderByID].Width = 10;
                c1Spiro.Cols[COL_OrderByName].Width = 180;
                c1Spiro.Cols[COL_ReviewdID].Width = 10;
                c1Spiro.Cols[COL_ReviewdName].Width = 150;
                c1Spiro.Cols[COL_ModifiedDate].Width = 150;
                c1Spiro.Cols[COL_Status].Width = 120;
                c1Spiro.Cols[COL_FileName].Width = 10;
                c1Spiro.Cols[COL_Intepretation].Width = 10;
                c1Spiro.Cols[COL_OrederType].Width = 10;
                c1Spiro.Cols[COL_OrederPrefix].Width = 80;
                c1Spiro.Cols[COL_TestType].Width = 80;
                c1Spiro.Cols[COL_TestComment].Width = 150;
                c1Spiro.Cols[COL_TestTakenBy].Width = 100;

               // c1Spiro.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;  
                c1Spiro.ExtendLastCol = true;

               
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Designing Grid " + e.ToString());
                //obj.Dispose();
                //obj = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.DesignGrid() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
           
        }

       /// <summary>
       ///Set to load data into grid
       /// </summary>
       private void LoadData()
       {
           DesignGrid(); 
           DataTable _dtSpiroTest = null;
           try
           {
               if (cmbTestType.SelectedIndex == 0)
               {
                   _dtSpiroTest = dvLoadTest.Table;
               }
               else
               {
                   dvLoadTest.RowFilter = "sTestType= '" + cmbTestType.Text + "'";
                   _dtSpiroTest = dvLoadTest.ToTable();
               }
               for (int iRow = 0; iRow <= _dtSpiroTest.Rows.Count-1  ; iRow++)
               {
                   c1Spiro.Rows.Add();
                   Int32 _Row = c1Spiro.Rows.Count - 1;
                   c1Spiro.SetData(_Row, COL_Select, 0);
                   c1Spiro.SetData(_Row, COL_DocumentID, _dtSpiroTest.Rows[iRow]["eDocumentId"].ToString());
                   c1Spiro.SetData(_Row, COL_DucumentCreatedDate, _dtSpiroTest.Rows[iRow]["dtDocumentCreated"].ToString());
                   c1Spiro.SetData(_Row, COL_OrderByID, _dtSpiroTest.Rows[iRow]["nOrderById"].ToString());
                   c1Spiro.SetData(_Row, COL_OrderByName, _dtSpiroTest.Rows[iRow]["sOrderByName"].ToString());
                   c1Spiro.SetData(_Row, COL_OrederType, _dtSpiroTest.Rows[iRow]["sOrderByType"].ToString());
                   c1Spiro.SetData(_Row, COL_ReviewdID, _dtSpiroTest.Rows[iRow]["nReviewedById"].ToString());
                   c1Spiro.SetData(_Row, COL_ReviewdName, _dtSpiroTest.Rows[iRow]["sReviewer"].ToString());
                   c1Spiro.SetData(_Row, COL_ModifiedDate, _dtSpiroTest.Rows[iRow]["dtDocumentModified"].ToString());
                   c1Spiro.SetData(_Row, COL_Status, _dtSpiroTest.Rows[iRow]["sStatus"].ToString());
                   c1Spiro.SetData(_Row, COL_FileName, _dtSpiroTest.Rows[iRow]["sDocumentName"].ToString());
                   c1Spiro.SetData(_Row, COL_Intepretation, _dtSpiroTest.Rows[iRow]["sInterpretation"].ToString());
                   c1Spiro.SetData(_Row, COL_OrederPrefix, _dtSpiroTest.Rows[iRow]["OrderPrefix"].ToString());
                   c1Spiro.SetData(_Row, COL_TestType, _dtSpiroTest.Rows[iRow]["sTestType"].ToString());
                   c1Spiro.SetData(_Row, COL_TestComment, _dtSpiroTest.Rows[iRow]["sTestDetails"].ToString());
                   c1Spiro.SetData(_Row, COL_TestTakenBy, _dtSpiroTest.Rows[iRow]["sUserName"].ToString());

               }
             //  c1Spiro.AutoSizeCols();     

               c1Spiro.AutoSizeCol(COL_OrderByName);
               c1Spiro.AutoSizeCol(COL_ReviewdName);
               c1Spiro.AutoSizeCol(COL_TestComment);
               c1Spiro.AutoSizeCol(COL_TestTakenBy); 

                       
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
               //obj.UpdateLog("Error In Binding Grid " + ex.ToString());
               //obj.Dispose();
               //obj = null;  
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.LoadData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

           }
           finally
           {
               if (_dtSpiroTest != null)
               {
                   _dtSpiroTest.Dispose();
                   _dtSpiroTest = null;
               }
           }
           SetInterpratation(); 
       }


    

       private void AddPagingSize()
       {
           cmbPageSize.Items.Clear();   
           string PagingSize = RetrivePagingSize();
           // if setting found
           if (PagingSize != string.Empty)
           {
               long CurrentSize=0;
               string[] pagingArray = PagingSize.Split(',');
               for (int index = 0; index <= pagingArray.Length-1; index++)
               {
                  if (long.TryParse(pagingArray[index], out CurrentSize))  
                  {
                    cmbPageSize.Items.Add(CurrentSize); 
                  }

               }
               cmbPageSize.SelectedIndex = 0; 
           }
           // if setting not found
           else
           {
               cmbPageSize.Items.Add("50");
               cmbPageSize.Items.Add("100");
               cmbPageSize.Items.Add("250");
               cmbPageSize.SelectedIndex = 0; 
           }

       }


       private void AddSelectRange()
       {
           long PagingSize = 0;
           clsSpiroTestMst objcls = null;
           long TotalNoOfRec = 0;
           try
           {
               if (long.TryParse(cmbPageSize.Text, out PagingSize))
               {
                  
                   TotalRecCount = 0;
                   objcls = new clsSpiroTestMst();
                   objcls.ConnectionString = SpiroConnectionString;
                   TotalNoOfRec = objcls.RetriveTestDataCount(_nPatinet_ID, ChkUseDateRange.Checked, dtpFromDate.Value, dtpTodate.Value, cmbTestType.Text);
                   if ((TotalNoOfRec / PagingSize) > 0)
                       SelectRec = new string[Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalNoOfRec) / Convert.ToDecimal(PagingSize)))];
                   else
                       SelectRec = new string[1];

                   if (TotalNoOfRec != 0)
                   {
                       long i = 0;
                       TotalRecCount = 0;
                       for (int cnt = 0; i < TotalNoOfRec; cnt++)
                       {

                           if (TotalNoOfRec - i >= PagingSize)
                               SelectRec[cnt] = Convert.ToString((i + 1) + " To " + (i + PagingSize));
                           else
                               SelectRec[cnt] = Convert.ToString((i + 1) + " To " + TotalNoOfRec); ;

                           TotalRecCount++;
                           i = i + PagingSize;


                       }

                   }// end total count
                   else
                   {
                       SelectRec[0] = "0 To 0";
                   }


                   if (CurrentRecIndex < 0)
                       CurrentRecIndex = 0;
                   if (CurrentRecIndex > SelectRec.Length - 1)
                       CurrentRecIndex = SelectRec.Length - 1;


               } // end page size

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.AddSelectRange() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

           }
           finally
           {
               if (objcls != null)
               {
                   objcls.Dispose();
                   objcls = null;
               }
           }

       }


       /// <summary>
       ///Method to Fill dataview
       /// </summary>
       private void FillTestView()
       {
           clsSpiroTestMst objcls = null;
           DataTable _dtSpiroTest = null;
           long OrderbyID = 0;
           try
           {
               long RecCountFrom = 0;
               long RecCountTo = 0;
               if (SelectRec.Length > CurrentRecIndex)
               {
                   long.TryParse(Convert.ToString(SelectRec[CurrentRecIndex].Split('T')[0]).Trim(), out RecCountFrom);
                   long.TryParse(Convert.ToString(SelectRec[CurrentRecIndex].Split('o')[1]).Trim(), out RecCountTo);
                   //lblSelected.Text =Convert.ToString(SelectRec[CurrentRecIndex]);
                   if (TotalRecCount == 0)
                       lblSelected.Text = "Page 1 of 1";
                   else
                   lblSelected.Text = "Page " + (CurrentRecIndex +1) + " of " + TotalRecCount;
                  
               }
               objcls = new clsSpiroTestMst();
               objcls.ConnectionString = SpiroConnectionString;
               _dtSpiroTest = objcls.RetriveTestData(_nPatinet_ID, ChkUseDateRange.Checked, dtpFromDate.Value, dtpTodate.Value,cmbTestType.Text, RecCountFrom, RecCountTo);
               objcls.ConnectionString = gloConnectionString;
               foreach (DataRow  dr in _dtSpiroTest.Rows )
               {
                   OrderbyID = 0;
                   long.TryParse(Convert.ToString(dr["nOrderById"]), out OrderbyID);
                   dr["sOrderByName"] = objcls.GetOrdredBy(OrderbyID, Convert.ToString(dr["sOrderByType"]));
               }
               if (dvLoadTest != null)
               {
                   dvLoadTest.Dispose();
                   dvLoadTest = null;
               }
               dvLoadTest = new DataView(_dtSpiroTest);
             
               // call load data method
               LoadData();

               
           }
           catch (Exception ex)
           {
               //MessageBox.Show(Ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
               //obj.UpdateLog("Error In Retrive Data." + ex.ToString());
               //obj.Dispose();
               //obj = null; 
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.FillTestView() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

           }
           finally
           {
               if (_dtSpiroTest != null)
               {
                   _dtSpiroTest.Dispose();
                   _dtSpiroTest = null;
               }
               if (objcls != null)
               {
                   objcls.Dispose();
                   objcls = null;
               }

           }


       }

       /// <summary>
       /// method to start new test
       /// </summary>
        private void StartNewTest()
        {
            // set defulat value to control 
            axSpiroActiveX1.SetPatientData(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0);
            axSpiroActiveX1.SetPatSmokingHistory(0, 0, 0, 0, 0);
            clsSpiroTestMst clsobj = null;
            DataTable dtGetPatientData = null;
            try
            {
                clsobj = new clsSpiroTestMst();
                clsobj.ConnectionString = gloConnectionString;
                dtGetPatientData = clsobj.GetPationtData(_nPatinet_ID);
                // if patient Data is Present
                if (dtGetPatientData.Rows.Count > 0)
                {
                    Int32 age = 0;
                    if (gloUC_PatientStrip1!=null) Int32.TryParse(Convert.ToString(gloUC_PatientStrip1.PatientAge.Years), out age);
                    frmSpirometryTestsNew obj = new frmSpirometryTestsNew(_nPatinet_ID,age, Convert.ToInt64(dtGetPatientData.Rows[0]["gloEMRRaceID"].ToString()), dtGetPatientData.Rows[0]["gloEMRRace"].ToString(), gloConnectionString, SpiroConnectionString);
                    obj.EvntViewVitalFormHandler += new frmSpirometryTestsNew.ViewVitalForm(obj_EvntViewVitalFormHandler);
                    obj.ShowDialog(this);
                      // if new screen lunch button is clicked
                    if (obj.LunchTest == true)
                    {
                        
                        // create object of new clspsirorecport class
                        objSpiroReport = new clsSpiroReports();

                        // create prefix text
                        objSpiroReport.TestOrderprefix = clsSpiroGeneralModule.readSpiroDeviceprefix(gloConnectionString);
                        
                        // create prefixorderby id
                        objSpiroReport.TestOrderId = clsSpiroGeneralModule.GetNewTestOrderID(_nPatinet_ID, SpiroConnectionString);
                         
                        // assing patient genral information and smoking histroy to clspiroclass object
                        objSpiroReport.IsSmoker = obj.Smoker == 1 ? true : false;
                        objSpiroReport.NoOfCigarsperDay = obj.CigsDay;
                        objSpiroReport.Noofsmokingyears = obj.SmokerForYears;
                        objSpiroReport.Isquitsmoking = obj.QuitSmoking == 1 ? true : false;
                        objSpiroReport.Noofquitsmokingyears = obj.QuitYearsAgo;
                        objSpiroReport.Hieghtincms = obj.PatientHeight;
                        objSpiroReport.Weightinkg = obj.PatientWeight;
                        objSpiroReport.ClinicId = _ClinicID;
                        objSpiroReport.PatientId = _nPatinet_ID;
                        objSpiroReport.RaceCode = obj.MappedSpiroRaceCode;
                        objSpiroReport.VisitId = obj.VisitID;
                        objSpiroReport.VisitId_Dtl = obj.VisitID;
                        objSpiroReport.ReviewerId = _loginUserId;
                        objSpiroReport.Reviewer = _loginUserName;
                        objSpiroReport.OrderedById = obj.OrderByID;
                        objSpiroReport.OrderByType = obj.OrderedByType;
                        objSpiroReport.UserId = _loginUserId;
                        objSpiroReport.UserName = _loginUserName;
                        objSpiroReport.RefernceId = 0;
                        objSpiroReport.MachineName = Environment.MachineName;
                        objSpiroReport.DocumentId = 0;
                        objSpiroReport.Status = "Active";
                        objSpiroReport.DocumentExtension = "CAR";
                        objSpiroReport.TestSource = "Midmark_Spirometry";

                        // set patient data to activex control
                        axSpiroActiveX1.SetPatientData(dtGetPatientData.Rows[0]["sLastName"].ToString().Trim(), dtGetPatientData.Rows[0]["sFirstName"].ToString().Trim(), dtGetPatientData.Rows[0]["sMiddleName"].ToString().Trim(), dtGetPatientData.Rows[0]["sPatientCode"].ToString().Trim(), dtGetPatientData.Rows[0]["dtDOB"].ToString().Trim(), Convert.ToInt32(dtGetPatientData.Rows[0]["Gender"].ToString()), obj.PatientWeight, obj.PatientHeight, obj.MappedSpiroRaceCode, 0);
                        // set aptient smoking histroy
                        if (obj.Smoker == 1)
                        {
                            axSpiroActiveX1.SetPatSmokingHistory(obj.Smoker, obj.CigsDay, obj.SmokerForYears, obj.QuitSmoking, obj.QuitYearsAgo);
                        }
                        // start new test screen

                        axSpiroActiveX1.SetReqPhysician(obj.OrderByName);
                  
                        axSpiroActiveX1.SetTechnicianName(objSpiroReport.UserName);

                        axSpiroActiveX1.StartSpiro();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, "Started new spirometry test", _nPatinet_ID, objSpiroReport.TestOrderId, objSpiroReport.OrderedById, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    // end of lunch button is clicked
                    obj.EvntViewVitalFormHandler -= new frmSpirometryTestsNew.ViewVitalForm(obj_EvntViewVitalFormHandler);
                    
                    obj.Dispose();
                    obj = null;
                }// end of if patient data present

            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Start New Test." + ex.ToString());
                //obj.Dispose();
                //obj = null; 
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.StartNewTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (clsobj != null)
                {
                    clsobj.Dispose();
                    clsobj = null;
                }
                if (dtGetPatientData != null)
                {
                    dtGetPatientData.Dispose();
                    dtGetPatientData = null;
                }
            }
         
         }

        void obj_EvntViewVitalFormHandler(long nVitalID)
        {
            EvntVitalFormHandler(nVitalID);
        }

        

        /// <summary>
        /// funaction to save reportort
        /// </summary>
       private long SaveTestReport()
        {
            long _SaveTestReport = 0;
            clsSpiroReportmanager objSpiroReortManger = null;
            try
            {
                // get Temp Folder path
                string FilePath = string.Empty;                
                // create filename filename  
                string FileName = _nPatinet_ID + "_" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + "." + objSpiroReport.DocumentExtension.ToString();
                // get file path
                FilePath = gloSettings.FolderSettings.AppTempFolderPath + FileName;
                // check file exists with same name
                if (File.Exists(FilePath) == true)
                {
                    File.Delete(FilePath);
                }
                // genrate file 
               // axSpiroActiveX1.GetSpiroReportToFile(FilePath);

                // get report to file
                axSpiroActiveX1.GetSpiroReportToFile(FilePath);
                // set report to axERCActiveX1 control for getting interpretation and test type
                if (File.Exists(FilePath))
                    axERCActiveX1.SetReportDataFromFile(FilePath);
                //delete report genrated by axSpiroActiveX1 control
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
                axERCActiveX1.GetReportDataToFile(FilePath);
                // set interpretation 
                objSpiroReport.Interpretation = axERCActiveX1.GetSpirometryInterpretation();
                objSpiroReport.TestType = GetTestType(axERCActiveX1.GetComments());
                objSpiroReport.TestDetails = axERCActiveX1.GetComments();

                // check i f file genrated sucessfully
                if (File.Exists(FilePath) == true)
                {
                    objSpiroReport.TestSource = "Midmark_Spirometry";
                    // asing doucument property to spiroreport object
                    objSpiroReport.Documentname = FileName;
                    // set document date as todate as date
                    objSpiroReport.dtDocumentCreated = DateTime.Now;
                    // set modified date as todate date
                    objSpiroReport.dtDocumentModified = DateTime.Now;
                    
                    // read byte array from file and asign to spiroreport object
                    objSpiroReport.DocStream = File.ReadAllBytes(FilePath);
                    // create spiromanager class object for saving report in file stream 
                    objSpiroReortManger = new clsSpiroReportmanager(SpiroConnectionString);
                    //call method save report in file stream
                    objSpiroReortManger.saveSpiroReport(objSpiroReport);
                    // delete file from temp folder
                    File.Delete(FilePath);
                    _SaveTestReport = objSpiroReport.DocumentId;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, "New spirometry test saved", _nPatinet_ID, objSpiroReport.TestOrderId, objSpiroReport.OrderedById, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                   
                }
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Start New Test." + ex.ToString());
                //obj.Dispose();
                //obj = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.SaveTestReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               _SaveTestReport = 0;
            }
            finally
            {
                if (objSpiroReortManger != null)
                {
                    objSpiroReortManger.Dispose();
                    objSpiroReortManger = null;
                }
                if (objSpiroReport != null)
                {
                    objSpiroReport.Dispose();
                    objSpiroReport = null;
                }
            }
            //}
           return _SaveTestReport;
        }
      
        /// <summary>
       /// funcation to Update test
       /// <param name="DocumentID"/>
       /// <param name="axspiroevent"/>
       /// </summary>
       private long UpdateReport(long _DocumentID,bool ISAxspiroEvent)
       {
           long _UpdateReport = 0;
           clsSpiroReportmanager objSpiroManager = null;
           clsSpiroReports objSpiroReportup = null;
           try
           {
                   objSpiroManager = new clsSpiroReportmanager(SpiroConnectionString);
                   // get report object
                   objSpiroReportup = objSpiroManager.getSpiroObject(_DocumentID);
                   // get selected File Name
                   string FileName = objSpiroReportup.Documentname;
                   //  if present
                   if (_DocumentID != 0 && FileName != string.Empty)
                   {
                       // tempary location for storing file
                       string FilePath = string.Empty;
                       
                       FilePath = gloSettings.FolderSettings.AppTempFolderPath + "" + FileName;
                       // check file exists with same name
                       if (File.Exists(FilePath) == true)
                       {
                           File.Delete(FilePath);
                       }
                       //if Event call from Post then genrate file from Axspiro control 
                       if (ISAxspiroEvent)
                       {
                           axSpiroActiveX1.GetSpiroReportToFile(FilePath);
                           // set report to axERCActiveX1 control for getting interpretation and test type
                           if (File.Exists(FilePath))
                               axERCActiveX1.SetReportDataFromFile(FilePath);
                           //delete file
                           if (File.Exists(FilePath))
                               File.Delete(FilePath);
                           // genrate visit whe post data is saved
                           objSpiroReportup.VisitId_Dtl = clsSpiroGeneralModule.getVisitID(DateTime.Now, _nPatinet_ID, gloConnectionString);
                           objSpiroReportup.UserId = _loginUserId;
                           objSpiroReportup.UserName = _loginUserName;
                       }
                       IsReportSaved = false;
                       axERCActiveX1.GetReportDataToFile(FilePath);
                       objSpiroReportup.Interpretation = axERCActiveX1.GetSpirometryInterpretation();
                       objSpiroReportup.TestType = GetTestType(axERCActiveX1.GetComments());
                       objSpiroReportup.TestDetails = axERCActiveX1.GetComments();
                       // if file genrated succsfully
                       if (File.Exists(FilePath) == true)
                       {
                           objSpiroReportup.TestSource = "Midmark_Spirometry";
                           // set order by type
                           //objSpiroReportup.OrderByType = Convert.ToString(c1Spiro.GetData(selectedrwindex, COL_OrederType));
                           // set now date as  modified date
                           objSpiroReportup.dtDocumentModified = DateTime.Now;
                           // add comments
                           objSpiroReportup.DocStream = File.ReadAllBytes(FilePath);
                           // add reviewed by 
                           objSpiroReportup.ReviewerId = _loginUserId;
                           objSpiroReportup.Reviewer = _loginUserName;
                           //call method save report in file stream
                           objSpiroManager.saveSpiroReport(objSpiroReportup);
                           //return update report id
                           _UpdateReport = objSpiroReportup.DocumentId;
                           // delete file from temp folder
                           File.Delete(FilePath);
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, "Spirometry report updated.", _nPatinet_ID, objSpiroReportup.TestOrderId, objSpiroReportup.OrderedById, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                       }// end of file genrated succsfully

                   }// end of if present

               } // end of no of colum checked
               //cmbTestType.SelectedIndex = 0;
          
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.UpdateReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

           }
           finally
           {
               if (objSpiroReportup != null)
               {
                   objSpiroReportup.Dispose();
                   objSpiroReportup = null;
               }
               if (objSpiroManager != null)
               {
                   objSpiroManager.Dispose();
                   objSpiroManager = null;
               }

           }
           return _UpdateReport;
       }

         /// <summary>
        /// funcation to return test type funcation
        /// <param name="sComments"/>
        /// </summary>
        private string GetTestType(string sComments)
        {
            string _GetTestType = string.Empty;
            // if test type contains Pre
            if (sComments.Contains("Pre:"))
            {   
                if (sComments.Contains("Post:"))
                     _GetTestType="Pre/Post";
                else
                     _GetTestType = "Pre";

            }
            else if (sComments.Contains("Post:"))
            {
                _GetTestType = "Post";
            }
           return _GetTestType; 
        }

        /// <summary>
        /// function to retrive no of colum checked in grid
        /// </summary>
        private Int32 NoOfColumChecked()
        {
            Int32 _cnt = 0;
            for (int rowcnt = 1; rowcnt < c1Spiro.Rows.Count; rowcnt++) 
            { 
                if (c1Spiro.GetData(rowcnt, COL_Select).ToString() == "True")
                { 
                    _cnt++;
                }
            }
            return _cnt;
        }

        /// <summary>
        /// function to retrive selected row index in grid
        /// </summary>
        private Int32 GetSectedRowIndex()
        {
            Int32 _GetSectedRowIndex = 0;
            for (int rowcnt = 1; rowcnt < c1Spiro.Rows.Count; rowcnt++)
            {
                if (Convert.ToString(c1Spiro.GetData(rowcnt, COL_Select)) == "True")
                {
                    _GetSectedRowIndex = rowcnt;
                    break; 
                }
            }
            return _GetSectedRowIndex;

        }
       
        /// <summary>
        /// method to View/Review Report
        /// </summary>
        /// <param name="ReView"></param>
        private void ShowReport(long DocumentId, bool ReView)
        {
           axERCActiveX1.SetReportDataDirect(null); 
           clsSpiroReportmanager objSpiroManager = null;
           clsSpiroReports objSpiroRpt = null;
           try
            {
                    objSpiroManager = new clsSpiroReportmanager(SpiroConnectionString);
                    objSpiroRpt = new clsSpiroReports();
                    objSpiroRpt = objSpiroManager.getSpiroObject(DocumentId);
                    axERCActiveX1.SetReportDataDirect(objSpiroManager.GetContainerStream(DocumentId) );
                    
                    if (ReView)
                    {
                        axERCActiveX1.SetPermissions(true, true, true, true);    
                        axERCActiveX1.SetReviewedBy(_loginUserName);
                        axERCActiveX1.SetSpiroPermissions(0, 1, 1, 1, 1, 0);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, "Spirometry test REVIEW started", _nPatinet_ID, objSpiroRpt.TestOrderId, objSpiroRpt.OrderedById, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);


                    }
                    else
                    {
                        axERCActiveX1.SetPermissions(false, false, false, false);
                        axERCActiveX1.SetSpiroPermissions(0, 0, 0, 0, 0, 0);
                        axERCActiveX1.SetReviewedBy(objSpiroRpt.Reviewer); 
                    }
                 
                    axERCActiveX1.StartReview();
                            
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), _gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.ShowReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {
                if (objSpiroManager != null)
                {
                    objSpiroManager.Dispose();
                    objSpiroManager = null;
                }
                if (objSpiroRpt != null)
                {
                    objSpiroRpt.Dispose();
                    objSpiroRpt = null;
                }

            }
        }
      
        
        /// <summary>
        /// method to post test 
        /// </summary>
        /// <param name="Preflag"></param>
        private void PostTest(long DocumentID,bool Preflag)
        {
            axSpiroActiveX1.SetPatientData(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0);
            axSpiroActiveX1.SetPatSmokingHistory(0, 0, 0, 0, 0);
            clsSpiroReportmanager objSpiroManager = null;
            clsSpiroReports objpostrpt = null;
            clsSpiroTestMst clsobj = null;
           try
            {
                    // get Patient Data
                    clsobj = new clsSpiroTestMst();
                    clsobj.ConnectionString = gloConnectionString;
                    DataTable dtGetPatientData = clsobj.GetPationtData(_nPatinet_ID);
                    // if patient Data is Present and documentid found
                    if (dtGetPatientData.Rows.Count > 0 && DocumentID != 0)
                    {
                        objSpiroManager = new clsSpiroReportmanager(SpiroConnectionString);
                        objpostrpt = new clsSpiroReports();
                        objpostrpt = objSpiroManager.getSpiroObject(DocumentID);
                        // set Report data to axspiroactivex control
                        axSpiroActiveX1.SetSpiroReportDirect(objSpiroManager.GetContainerStream(DocumentID));
                        //   set patient data to activex control
                        axSpiroActiveX1.SetPatientData(dtGetPatientData.Rows[0]["sLastName"].ToString().Trim(), dtGetPatientData.Rows[0]["sFirstName"].ToString().Trim(), dtGetPatientData.Rows[0]["sMiddleName"].ToString().Trim(), dtGetPatientData.Rows[0]["sPatientCode"].ToString().Trim(), dtGetPatientData.Rows[0]["dtDOB"].ToString().Trim(), Convert.ToInt32(dtGetPatientData.Rows[0]["Gender"].ToString()), objpostrpt.Weightinkg, objpostrpt.Hieghtincms, Convert.ToInt32(objpostrpt.RaceCode), 0);
                        //set aptient smoking histroy
                        axSpiroActiveX1.SetPatSmokingHistory(objpostrpt.IsSmoker == true ? 1 : 0, objpostrpt.NoOfCigarsperDay, objpostrpt.Noofsmokingyears, objpostrpt.Isquitsmoking == true ? 1 : 0, objpostrpt.Noofquitsmokingyears);
                        // if it is pretest form

                      //  axSpiroActiveX1.SetTechnicianName(_loginUserName); 

                        if (Preflag)
                        {
                            axSpiroActiveX1.StartInPreBd();
                        }
                        //start spiro report
                        axSpiroActiveX1.StartSpiro();
                        IsPostTest = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, "Spirometry report POST started.", _nPatinet_ID, objpostrpt.TestOrderId, objpostrpt.OrderedById, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    }
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.PostTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {

                if (objpostrpt != null)
                {
                    objpostrpt.Dispose();
                    objpostrpt = null;
                }

                if (objSpiroManager != null)
                {
                    objSpiroManager.Dispose();
                    objSpiroManager = null;
                }
                if (clsobj != null)
                {
                    clsobj.Dispose();
                    clsobj = null;
                }


            }

        }

        /// <summary>
        /// method to print report
        /// </summary>
        private void PrintReport(long DocumentID)
        {
            clsSpiroReportmanager objSpiroManager = null;
            axERCActiveX1.SetReportDataDirect(null);
            try
            {
                    objSpiroManager = new clsSpiroReportmanager(SpiroConnectionString);
                    axERCActiveX1.SetReportDataDirect(objSpiroManager.GetContainerStream(DocumentID));
                    axERCActiveX1.SetPermissions(true, true, true, true);     
                    axERCActiveX1.PrintReport(); 
  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.PrintReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (objSpiroManager != null)
                {
                    objSpiroManager.Dispose();
                    objSpiroManager = null;
               }
            }
        }

       /// <summary>
        /// method to compaire report
       /// </summary>
        private void CompaireReport()
        {
            // Declare object of clspiromanager class
            clsSpiroReportmanager objSpiroManager = null;
            axSpiroTrendX1.ClearReports();
            try
            {
                    // create new instance 
                    objSpiroManager = new clsSpiroReportmanager(SpiroConnectionString);  
                    for (int rowcnt = 1; rowcnt < c1Spiro.Rows.Count; rowcnt++)
                    {
                     // add selected report to activex control
                        if (Convert.ToString(c1Spiro.GetData(rowcnt, COL_Select)) == "True")
                        {
                            axSpiroTrendX1.AddSpiroReportFromMemory(objSpiroManager.GetContainerStream(Convert.ToInt64(c1Spiro.GetData(rowcnt, COL_DocumentID))));
                        }
                    }
                    // display comaprison screen
                    axSpiroTrendX1.DisplayTrendReport(1);
               
            }// try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), _gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.CompaireReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (objSpiroManager != null)
                {
                    objSpiroManager.Dispose();
                    objSpiroManager = null;
                }

            }// finally
       }

        /// <summary>
        ///  method to change status
        /// </summary>
        private void Status(long DocumentID)
        {
            frmSpirometryChangeStatus frmobj=null;
            clsSpiroReportmanager objspirorepmanager = null;
            try
            {
                      // Int32 selectedRowIndex = GetSectedRowIndex();
                       // call form to show status

                if (c1Spiro.RowSel >= 0)
                {
                    string s = Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_Status));
                    frmobj = new frmSpirometryChangeStatus(Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_Status)));
                    frmobj.ShowDialog(this);
                    // check if Sattus is changed
                    if (Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_Status)).Trim() != frmobj.Status.Trim())
                    {
                        objspirorepmanager = new clsSpiroReportmanager(SpiroConnectionString);
                        objspirorepmanager.updateStatus(DocumentID, frmobj.Status.Trim());
                        // update data view
                        foreach (DataRow dr in dvLoadTest.Table.Rows)
                        {
                            if (Convert.ToInt64(Convert.ToString(dr["eDocumentId"])) == DocumentID)
                            {
                                dr["sStatus"] = frmobj.Status.Trim();
                                c1Spiro.SetData(c1Spiro.RowSel, COL_Status, frmobj.Status.Trim());
                                c1Spiro.Select(c1Spiro.RowSel, COL_DocumentID);
                                //c1Spiro.AutoSizeCols();
                                break;
                            }//if

                        } //for


                    }//if if status not mached
                }
                else { MessageBox.Show(this, "Select Test to set the status.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                  
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.Status() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (frmobj != null)
                {
                    frmobj.Dispose();
                    frmobj = null;
                }

                if (objspirorepmanager != null)
                {
                    objspirorepmanager.Dispose();
                    objspirorepmanager = null;
                }

            }

        }

        
        ///// <summary>
        ///// method to set spiro connectionstring 
        ///// </summary>
        //private bool ISDeviceConnectionString()
        //{
        //    System.Data.SqlClient.SqlConnection sqlcon = null;
        //    bool _ISDeviceConnectionString = false ;  
        //    SpiroConnectionString = gloEmdeonInterface.Classes.clsSpiroGeneralModule.RetriveDeviceCon(_ClinicID, gloConnectionString);
        //    if (SpiroConnectionString.Length > 0)
        //    {
        //        try
        //        {
        //            sqlcon = new System.Data.SqlClient.SqlConnection(SpiroConnectionString);
        //            sqlcon.Open();
        //            sqlcon.Close();
        //            _ISDeviceConnectionString = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Unable to connect device database, Invalid server credentials.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.ISDeviceConnectionString() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
        //            _ISDeviceConnectionString = false;
        //        }
        //        finally
        //        {
        //            if (sqlcon != null)
        //            {
        //                sqlcon.Dispose();
        //                sqlcon = null;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        _ISDeviceConnectionString = false;
        //        MessageBox.Show("Unable to detect device database configuration, Configure device database in admin", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
           
        //    return _ISDeviceConnectionString; 
        //}

        /// <summary>
        ///  method to apply style to grid
        /// </summary>
        private void SetGridStyle(C1.Win.C1FlexGrid.C1FlexGrid oFlex)
        {
            c1Spiro.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;

            //oFlex.Cols.Count = 0;
            //oFlex.Cols.Fixed = 0;
            oFlex.Rows.Count = 1;
            oFlex.Rows.Fixed = 1;

            oFlex.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            oFlex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            oFlex.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);


            oFlex.Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211);
            oFlex.Styles.Fixed.ForeColor = Color.White;
            oFlex.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);

            oFlex.Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250);  // Color.LightBlue;
            oFlex.Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125);
            oFlex.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);

            oFlex.Styles.Normal.BackColor = Color.FromArgb(240, 247, 255);
            oFlex.Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125);
            oFlex.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);

            oFlex.Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102);
            oFlex.Styles.Highlight.ForeColor = Color.Black;

            oFlex.Styles.Focus.BackColor = Color.FromArgb(254, 207, 102);
            oFlex.Styles.Focus.ForeColor = Color.Black;


            C1.Win.C1FlexGrid.CellStyle csHeader;// = oFlex.Styles.Add("CS_Header");
            try
            {
                if (oFlex.Styles.Contains("CS_Header"))
                {
                    csHeader = oFlex.Styles["CS_Header"];
                }
                else
                {
                    csHeader = oFlex.Styles.Add("CS_Header");
                    csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold);
                    csHeader.ForeColor = Color.Black;
                    csHeader.BackColor = Color.FromArgb(192, 203, 233);
                    //csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csHeader.DataType = Type.GetType("System.String");
                }

            }
            catch
            {
                csHeader = oFlex.Styles.Add("CS_Header");
                csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold);
                csHeader.ForeColor = Color.Black;
                csHeader.BackColor = Color.FromArgb(192, 203, 233);
                //csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csHeader.DataType = Type.GetType("System.String");
            }
           


            C1.Win.C1FlexGrid.CellStyle csRecord;// = oFlex.Styles.Add("CS_Record");
            try
            {
                if (oFlex.Styles.Contains("CS_Record"))
                {
                    csRecord = oFlex.Styles["CS_Record"];
                }
                else
                {
                    csRecord = oFlex.Styles.Add("CS_Record");
                    csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                    csRecord.ForeColor = Color.Black;
                    csRecord.BackColor = Color.GhostWhite;
                    //csRecord.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csRecord.DataType = Type.GetType("System.String");
                }

            }
            catch
            {
                csRecord = oFlex.Styles.Add("CS_Record");
                csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                csRecord.ForeColor = Color.Black;
                csRecord.BackColor = Color.GhostWhite;
                //csRecord.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csRecord.DataType = Type.GetType("System.String");
            }
    


            C1.Win.C1FlexGrid.CellStyle csComboList;// = oFlex.Styles.Add("CS_ComboList");
            try
            {
                if (oFlex.Styles.Contains("CS_ComboList"))
                {
                    csComboList = oFlex.Styles["CS_ComboList"];
                }
                else
                {
                    csComboList = oFlex.Styles.Add("CS_ComboList");
                     csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
            csComboList.ForeColor = Color.Black;
            csComboList.BackColor = Color.GhostWhite;
            //csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            csComboList.DataType = Type.GetType("System.String");
                }

            }
            catch
            {
                csComboList = oFlex.Styles.Add("CS_ComboList");
                csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
            csComboList.ForeColor = Color.Black;
            csComboList.BackColor = Color.GhostWhite;
            //csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            csComboList.DataType = Type.GetType("System.String");
            }
          
            csComboList.ComboList = "...";


            C1.Win.C1FlexGrid.CellStyle csCheckBox;// = oFlex.Styles.Add("CS_CheckBox");
            try
            {
                if (oFlex.Styles.Contains("CS_CheckBox"))
                {
                    csCheckBox = oFlex.Styles["CS_CheckBox"];
                }
                else
                {
                    csCheckBox = oFlex.Styles.Add("CS_CheckBox");
                    csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                    csCheckBox.ForeColor = Color.Black;
                    csCheckBox.BackColor = Color.GhostWhite;
                    //csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csCheckBox.DataType = Type.GetType("System.Boolean");
                }

            }
            catch
            {
                csCheckBox = oFlex.Styles.Add("CS_CheckBox");
                csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                csCheckBox.ForeColor = Color.Black;
                csCheckBox.BackColor = Color.GhostWhite;
                //csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csCheckBox.DataType = Type.GetType("System.Boolean");
            }
            


            C1.Win.C1FlexGrid.CellStyle csNotNormal ;//= oFlex.Styles.Add("CS_NotNormal");
            try
            {
                if (oFlex.Styles.Contains("CS_NotNormal"))
                {
                    csNotNormal = oFlex.Styles["CS_NotNormal"];
                }
                else
                {
                    csNotNormal = oFlex.Styles.Add("CS_NotNormal");
                    csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                    csNotNormal.ForeColor = Color.Red;
                    csNotNormal.BackColor = Color.GhostWhite;
                }

            }
            catch
            {
                csNotNormal = oFlex.Styles.Add("CS_NotNormal");
                csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular);
                csNotNormal.ForeColor = Color.Red;
                csNotNormal.BackColor = Color.GhostWhite;
            }
            


        }


        /// <summary>
        /// method to set interpreatation to text box 
        /// </summary>
        private void SetInterpratation()
        {
            if (c1Spiro.RowSel > 0 && c1Spiro.Rows.Count > 0)
            {
                if (Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_Intepretation)).Trim().Length > 0)
                {
                    txtInterpreatation.Text = Convert.ToString(c1Spiro.GetData(c1Spiro.RowSel, COL_Intepretation)).Trim();
                    PnlInterpretation.Visible = true;
                }
                else
                {
                    txtInterpreatation.Text = string.Empty;
                    PnlInterpretation.Visible = false;
                }

            }
            else
            {
                txtInterpreatation.Text = string.Empty;
                PnlInterpretation.Visible = false;
            }
        }


        private string RetrivePagingSize()
        {
            string _GetPagingValue = string.Empty;
            clsSpiroTestMst objcls = null;
            try
            {
                objcls = new clsSpiroTestMst();
                objcls.ConnectionString = gloConnectionString;
                _GetPagingValue = objcls.RetrivePagingSize(_ClinicID);
             }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In To Retrive Spirometry Paging Setting Size." + ex.ToString());
                //obj.Dispose();
                //obj = null; 
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.RetrivePagingSize() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               _GetPagingValue = string.Empty;  
            }
            finally
            {
                if (objcls != null)
                {
                    objcls.Dispose();
                    objcls = null;
                }
            }

            return _GetPagingValue;
        }

       #endregion

       private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFormLoded)
            {
                AddSelectRange();
                FillTestView(); 
            }
        }

       private void BtnFirst_Click(object sender, EventArgs e)
       {
           if (CurrentRecIndex != 0)
           {
               CurrentRecIndex = 0;
               FillTestView();
           }

       }

       private void BtnNext_Click(object sender, EventArgs e)
       {

           if (CurrentRecIndex + 1 < TotalRecCount)
           {
               CurrentRecIndex++; 
               FillTestView(); 
           }
       }

       private void BtnPrev_Click(object sender, EventArgs e)
       {
           if (CurrentRecIndex - 1 >= 0)
           {
               CurrentRecIndex--;
               FillTestView(); 
           }
          
       }

       private void BtnLast_Click(object sender, EventArgs e)
       {
           if (CurrentRecIndex != TotalRecCount-1)
           {
               CurrentRecIndex = TotalRecCount-1;
               FillTestView(); 

           }



          
       }

       
    }
}
