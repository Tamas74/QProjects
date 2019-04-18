using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;  
using System.IO;
using C1.Win.C1FlexGrid;
using System.Collections;


namespace gloBilling
{
    
    
    public partial class frmRpt_MissingCharges : Form
    {

       
        #region " Private Variables "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
       
        const int COL_PATIENTID = 0;
        //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
        const int COL_SELECT = 1;
        const int COL_PATIENTCODE = 2;
        const int COL_PATIENTNAME = 3;
        const int COL_PHONENO = 4;
        const int COL_DOB = 5;
        const int COL_DATEOFSERVICE = 6;
        const int COL_PROVIDER = 7;
        const int COL_APPOINTMENTTYPE = 8;
        const int COL_DTLAPPOINTMENTID = 9;
        const int COL_LOCATION = 10;
        const int COL_LOCATIONID = 11;
        const int COL_MissingChargesDetails = 12;
        const int COL_COUNT = 15;
        const int COL_PROVIDERID = 13;
        const int COL_APPOINTMENTTYPEID = 14;


        private bool bIsremovechecked=false;
        private Int32 iselected = 1;

        enum ReportBy
        {
            DOS=0,
            DOS_Provider=1
        }
        #endregion

        #region "Contructor"

        public frmRpt_MissingCharges(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            InitializeComponent();

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

        #endregion
        
        #region "Form Control Events"

        private void frmRpt_MissingCharges_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1MissingCharges);
            
            if (c1MissingCharges.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
                tls_RemoveAppt.Enabled = false;
                tls_Select.Enabled = false; 
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
                tls_RemoveAppt.Enabled = true;
                tls_Select.Enabled = true; 
            }
            Fill_FilterDatesCombo();
            FillProviderData();
            FillAppoinmentType();
            FillLocationName();
            cmb_datefilter.Focus();
            cmb_datefilter.Select();

            if (trvProvider.Nodes.Count > 0)
            {
                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    trvProvider.Nodes[i].Checked = true;
                }
            }
             btnDeSelectProvider.Visible = true;
             btnSelectProvider.Visible = false;


            if (trvLocation.Nodes.Count > 0)
            {
                for (int i = 0; i < trvLocation.Nodes.Count; i++)
                {
                    trvLocation.Nodes[i].Checked = true;
                }
            }
            btnDeSelectLocation.Visible = true;
            btnSelectLocation.Visible = false;

            if (trvApptType.Nodes.Count > 0)
            {
                for (int i = 0; i < trvApptType.Nodes.Count; i++)
                {
                    trvApptType.Nodes[i].Checked = true;
                }
            }
            btnDeSelectApptType.Visible = true;
            btnSelectApptType.Visible = false;
            rdReportByDOS.Checked = true;
        }
        
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        
        private void c1MissingCharges_DoubleClick(object sender, EventArgs e)
        {
            try
            {
             
              Cursor.Current = Cursors.WaitCursor;

              if (c1MissingCharges.Row > 0)
              {

                  iselected = c1MissingCharges.RowSel;
                  Int64 _nPatientID = Convert.ToInt64(c1MissingCharges.GetData(c1MissingCharges.Row, COL_PATIENTID));
                  string sProviderFromMissingChrgAppt = Convert.ToString(c1MissingCharges.GetData(c1MissingCharges.Row, COL_PROVIDER));
                  Int64 _nLocationIDFromMissingChrgAppt = Convert.ToInt64(c1MissingCharges.GetData(c1MissingCharges.Row, COL_LOCATIONID));
                  Int64 _nProviderIDFromMissingChrgAppt = Convert.ToInt64(c1MissingCharges.GetData(c1MissingCharges.Row, COL_PROVIDERID));
                  Int64 _nAppointmemtTypeID = Convert.ToInt64(c1MissingCharges.GetData(c1MissingCharges.Row, COL_APPOINTMENTTYPEID));
                  DateTime _DOS = Convert.ToDateTime(c1MissingCharges.GetData(c1MissingCharges.Row, COL_DATEOFSERVICE));
                  Int64 _nAppointmentID=Convert.ToInt64(c1MissingCharges.GetData(c1MissingCharges.Row,COL_DTLAPPOINTMENTID));

                  Int64 nFacilityIDFromMissingChrgAppt = Convert.ToInt64(GetFacilityID(_nLocationIDFromMissingChrgAppt));

                  frmBillingTransaction ofrmBillingTransactiion = frmBillingTransaction.GetInstance(_nPatientID);
                  //to avoid show dialog and mdiparent issue , need to check is charges already open, if yes then ask for save changes,
                  //if not then directlly open missing charges - new charges window
                  ofrmBillingTransactiion.IsMissingChargeLoad = true;
                  ofrmBillingTransactiion.nSelectedAppointmentFacilityID = nFacilityIDFromMissingChrgAppt;
                  ofrmBillingTransactiion.sSelectedAppointmentProvider = sProviderFromMissingChrgAppt;
                  ofrmBillingTransactiion.sSelectedAppointmentProviderID = _nProviderIDFromMissingChrgAppt;
                  ofrmBillingTransactiion.nSelectedAppointmentTypeID = _nAppointmemtTypeID;
                  ofrmBillingTransactiion.sSelectedAppointmentDOS =Convert.ToString(_DOS);
                  ofrmBillingTransactiion.nSelectedAppointmentID=_nAppointmentID;


                  if (ofrmBillingTransactiion.MdiParent == null)
                  {
                      ofrmBillingTransactiion.OpenChargesForExternal = true;
                      ofrmBillingTransactiion.OpenChargesExternalDateTime = Convert.ToDateTime(c1MissingCharges.GetData(c1MissingCharges.Row, COL_DATEOFSERVICE));
                      Cursor.Current = Cursors.Default;
                      gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.MissingCharges, gloAuditTrail.ActivityType.Modify, "Open charges for modify from Missing charges form", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                      ofrmBillingTransactiion.ShowDialog(this);
                      ShowReport();
                      ofrmBillingTransactiion.Dispose();
                  }
                  else
                  {
                      MessageBox.Show("Charges entry screen is already open, save or discard the existing charges and retry.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
              }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private Int64 GetFacilityID(Int64 nLocationID)
        {
            Int64 nFacilityID = 0;
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nLocationID", nLocationID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("Get_Facility", oDBParameters, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    nFacilityID = Convert.ToInt64(dt.Rows[0]["nFacilityID"]);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                dt.Dispose();
            }
            return nFacilityID;
        }


        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;

            _filterby = cmb_datefilter.SelectedIndex;
            switch (_filterby)
            {
                case 0://Date Range
                    FilterBy_DateRange();
                    break;

                case 1://Today
                    FilterBy_Today();
                    break;

                case 2://Tomorrow
                    FilterBy_Tomorrow();
                    break;

                case 3://Yesterday
                    FilterBy_Yesterday();
                    break;

                case 4://This week
                    FilterBy_Thisweek();
                    break;

                case 5://Last Week
                    FilterBy_lastweek();
                    break;

                case 6://Current Month
                    FilterBy_currentmonth();
                    break;

                case 7://Last Month
                    FilterBy_lastmonth();
                    break;

                case 8://Current Year
                    FilterBy_currenYear();
                    break;

                case 9://Last 30 days
                    FilterBy_last30days();
                    break;

                case 10://Last 60 days
                    FilterBy_last60days();
                    break;

                case 11://Last 90 days
                    FilterBy_last90days();
                    break;

                case 12://Last 120 days
                    FilterBy_last120days();
                    break;
            }

        }
        
        private void chkShowRemovedAppt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkShowRemovedAppt.Checked)
                {
                    if (bIsremovechecked)
                    tls_RemoveAppt.Enabled = false;

                }
                else
                {
                    if (c1MissingCharges.Rows.Count > 1)
                    {
                        if (!bIsremovechecked)
                      tls_RemoveAppt.Enabled = true;
                    }
                    else
                    {
                        if (bIsremovechecked)
                        tls_RemoveAppt.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
         
            }
                
        }
        
        private void btnSelectProvider_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = true;
                    }
                }
                btnDeSelectProvider.Visible = true;
                btnSelectProvider.Visible = false;
                this.Cursor = Cursors.Default;
                trvProvider.Select();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnDeSelectProvider_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = false;
                    }
                }
                btnDeSelectProvider.Visible = false;
                btnSelectProvider.Visible = true;
                this.Cursor = Cursors.Default;
                trvProvider.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnSelectApptType_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (trvApptType.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvApptType.Nodes.Count; i++)
                    {
                        trvApptType.Nodes[i].Checked = true;
                    }
                }
                btnDeSelectApptType.Visible = true;
                btnSelectApptType.Visible = false;
                this.Cursor = Cursors.Default;
                trvApptType.Select();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDeSelectApptType_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (trvApptType.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvApptType.Nodes.Count; i++)
                    {
                        trvApptType.Nodes[i].Checked = false;
                    }
                }
                btnDeSelectApptType.Visible = false;
                btnSelectApptType.Visible = true;
                this.Cursor = Cursors.Default;
                trvApptType.Select();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnSelectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (trvLocation.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvLocation.Nodes.Count; i++)
                    {
                        trvLocation.Nodes[i].Checked = true;
                    }
                }
                btnDeSelectLocation.Visible = true;
                btnSelectLocation.Visible = false;
                this.Cursor = Cursors.Default;
                trvLocation.Select();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDeSelectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (trvLocation.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvLocation.Nodes.Count; i++)
                    {
                        trvLocation.Nodes[i].Checked = false;
                    }
                }
                btnDeSelectLocation.Visible = false;
                btnSelectLocation.Visible = true;
                this.Cursor = Cursors.Default;
                trvLocation.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "Tool Strip Button Events"

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (rdReportByDOS.Checked)
                {
                    ShowReport(ReportBy.DOS);
                }
                else if (rdReportByDOS_Provider.Checked)
                {
                    ShowReport(ReportBy.DOS_Provider);
                }
                
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (c1MissingCharges != null && c1MissingCharges.Rows.Count > 1)
            {
                ExportReportToExcel(false);
            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            if (c1MissingCharges != null && c1MissingCharges.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }

        private void tls_Select_Click(object sender, EventArgs e)
        {
            MissingChargesSelectionDeselection();
        }

        private void tls_RemoveAppt_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder strBuilderApointmentDetIDs = new StringBuilder();
                Int64 _nCount = 0;

                if (c1MissingCharges.Rows.Count > 0)
                {
                    
                    for (int i = 1; i < c1MissingCharges.Rows.Count; i++)
                    {
                        if (c1MissingCharges.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                        {
                            _nCount++;
                            if (_nCount == 1)
                            {
                                strBuilderApointmentDetIDs.Append("'" + c1MissingCharges.Rows[i][COL_DTLAPPOINTMENTID].ToString() + "'");
                            }
                            else
                            {
                                strBuilderApointmentDetIDs.Append(",'" + c1MissingCharges.Rows[i][COL_DTLAPPOINTMENTID].ToString() + "'");
                            }
                        }
                    }

                    if (_nCount > 0)
                    {
                        if (MessageBox.Show("Are you sure that you want to remove the selected appointments from the report ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            string sApointmentDetailId = "";
                            sApointmentDetailId = strBuilderApointmentDetIDs.ToString();
                            if (sApointmentDetailId != "")
                            {
                                RemoveFromReport(sApointmentDetailId);
                                ShowReport();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select an appointment to remove.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1MissingCharges.Focus();
                        c1MissingCharges.Select();
                    
                    }


                }
            }
            catch (Exception exRemove)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exRemove.ToString(), false);
            }


        }

        #endregion
        
        #region "Private and Public Methods"

        private void DesignGrid(Boolean bShowUnlinkAppt=false)
        {
            c1MissingCharges.Rows.Count = 1;
            c1MissingCharges.Cols.Count = COL_COUNT;
            c1MissingCharges.SetData(0, COL_SELECT, "Select");
            c1MissingCharges.SetData(0, COL_DATEOFSERVICE, "Date Of Service");
            c1MissingCharges.SetData(0, COL_PATIENTID, "Patient ID");
            c1MissingCharges.SetData(0, COL_PATIENTNAME, "Patient Name");
            c1MissingCharges.SetData(0, COL_PROVIDER, "Provider");
            c1MissingCharges.SetData(0, COL_APPOINTMENTTYPE, "Appointment Type");
            c1MissingCharges.SetData(0, COL_DTLAPPOINTMENTID, "Appointment Detail Id");

            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1MissingCharges.SetData(0, COL_PATIENTCODE, "Patient Code");
            c1MissingCharges.SetData(0, COL_PHONENO, "Phone No");
            c1MissingCharges.SetData(0, COL_DOB, "Date Of Birth");
            c1MissingCharges.SetData(0, COL_LOCATION, "Location");
            c1MissingCharges.SetData(0, COL_LOCATIONID, "Location ID");
            c1MissingCharges.SetData(0, COL_MissingChargesDetails, "Missing Charge Details");
            c1MissingCharges.SetData(0, COL_PROVIDERID, "nProviderID");
            c1MissingCharges.SetData(0, COL_APPOINTMENTTYPEID, "AppointmentTypeID");
           


            
            if (bShowUnlinkAppt)
            {
                c1MissingCharges.Cols[COL_SELECT].Width = 50;
                c1MissingCharges.Cols[COL_DATEOFSERVICE].Width = 150;
                c1MissingCharges.Cols[COL_PATIENTID].Width = 0;
                c1MissingCharges.Cols[COL_PATIENTNAME].Width = 200;
                c1MissingCharges.Cols[COL_PROVIDER].Width = 200;
                c1MissingCharges.Cols[COL_APPOINTMENTTYPE].Width = 140;
                c1MissingCharges.Cols[COL_DTLAPPOINTMENTID].Width = 0;

                //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
                c1MissingCharges.Cols[COL_PATIENTCODE].Width = 90;
                c1MissingCharges.Cols[COL_PHONENO].Width = 90;
                c1MissingCharges.Cols[COL_DOB].Width = 130;
                c1MissingCharges.Cols[COL_LOCATION].Width = 200;
                c1MissingCharges.Cols[COL_LOCATIONID].Width = 50;
                c1MissingCharges.Cols[COL_MissingChargesDetails].Width = 0;
                c1MissingCharges.Cols[COL_MissingChargesDetails].Visible = false;
            }
            else
            {
                c1MissingCharges.Cols[COL_SELECT].Width = 50;
                c1MissingCharges.Cols[COL_DATEOFSERVICE].Width = 110;
                c1MissingCharges.Cols[COL_PATIENTID].Width = 0;
                c1MissingCharges.Cols[COL_PATIENTNAME].Width = 125;
                c1MissingCharges.Cols[COL_PROVIDER].Width = 125;
                c1MissingCharges.Cols[COL_APPOINTMENTTYPE].Width = 140;
                c1MissingCharges.Cols[COL_DTLAPPOINTMENTID].Width = 0;

                //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
                c1MissingCharges.Cols[COL_PATIENTCODE].Width = 90;
                c1MissingCharges.Cols[COL_PHONENO].Width = 90;
                c1MissingCharges.Cols[COL_DOB].Width = 100;
                c1MissingCharges.Cols[COL_LOCATION].Width = 125;
                c1MissingCharges.Cols[COL_LOCATIONID].Width = 50;
                c1MissingCharges.Cols[COL_MissingChargesDetails].Width = 300;
                c1MissingCharges.Cols[COL_MissingChargesDetails].Visible = true;
            }

            c1MissingCharges.Cols[COL_DATEOFSERVICE].Visible = true;
            c1MissingCharges.Cols[COL_PATIENTID].Visible = false;
            c1MissingCharges.Cols[COL_PATIENTNAME].Visible = true;
            c1MissingCharges.Cols[COL_APPOINTMENTTYPE].Visible = true;


            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1MissingCharges.Cols[COL_PATIENTCODE].Visible = true;
            c1MissingCharges.Cols[COL_PHONENO].Visible = true;
            c1MissingCharges.Cols[COL_DOB].Visible = true;
            c1MissingCharges.Cols[COL_DTLAPPOINTMENTID].Visible = false;

            c1MissingCharges.Cols[COL_LOCATION].Visible = true;
            c1MissingCharges.Cols[COL_LOCATIONID].Visible = false;
            c1MissingCharges.Cols[COL_PROVIDERID].Visible = false;
            c1MissingCharges.Cols[COL_APPOINTMENTTYPEID].Visible = false;

            //c1MissingCharges.Cols[COL_SELECT].AllowEditing = true;


            c1MissingCharges.Cols[COL_SELECT].DataType = typeof(System.Boolean);
            c1MissingCharges.Cols[COL_DATEOFSERVICE].DataType = typeof(System.DateTime);

            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1MissingCharges.Cols[COL_DOB].DataType = typeof(System.DateTime);

            c1MissingCharges.Cols[COL_DATEOFSERVICE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1MissingCharges.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1MissingCharges.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1MissingCharges.Cols[COL_APPOINTMENTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
           
            c1MissingCharges.Cols[COL_LOCATION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1MissingCharges.Cols[COL_PATIENTCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1MissingCharges.Cols[COL_PHONENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1MissingCharges.Cols[COL_DOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            c1MissingCharges.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

            c1MissingCharges.AllowEditing = true;
            c1MissingCharges.Cols[COL_SELECT].AllowEditing = true;
            c1MissingCharges.Cols[COL_DOB].AllowEditing = false;
            c1MissingCharges.Cols[COL_DATEOFSERVICE].AllowEditing = false;
            c1MissingCharges.Cols[COL_PATIENTCODE].AllowEditing = false;
            c1MissingCharges.Cols[COL_PATIENTNAME].AllowEditing = false;
            c1MissingCharges.Cols[COL_APPOINTMENTTYPE].AllowEditing = false;
            c1MissingCharges.Cols[COL_PROVIDER].AllowEditing = false;
            c1MissingCharges.Cols[COL_PHONENO].AllowEditing = false;
            c1MissingCharges.Cols[COL_DTLAPPOINTMENTID].AllowEditing = false;
            c1MissingCharges.Cols[COL_LOCATION].AllowEditing = false;
            c1MissingCharges.Cols[COL_MissingChargesDetails].AllowEditing = false;

            c1MissingCharges.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

        }

        private void ShowReport(ReportBy showReportUsing=ReportBy.DOS, Boolean bShowUnlinkAppt=false)
        {
            try
            {
                DesignGrid(bShowUnlinkAppt);
                DataTable dtPatients = null;
                dtPatients = GetMissingCharges(showReportUsing, bShowUnlinkAppt);
                bIsremovechecked = chkShowRemovedAppt.Checked;
                if (dtPatients != null)
                {
                    for (int i = 0; i < dtPatients.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1MissingCharges.Rows.Add();
                        c1MissingCharges.SetData(NewRow.Index, COL_PATIENTID, Convert.ToString(dtPatients.Rows[i]["nPatientID"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_PATIENTNAME, Convert.ToString(dtPatients.Rows[i]["sPatientName"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_DATEOFSERVICE, Convert.ToString(dtPatients.Rows[i]["dtDateOfService"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_APPOINTMENTTYPE, Convert.ToString(dtPatients.Rows[i]["sAppointmentType"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_PROVIDER, Convert.ToString(dtPatients.Rows[i]["sProvidername"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_PATIENTCODE, Convert.ToString(dtPatients.Rows[i]["sPatientCode"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_PHONENO, Convert.ToString(dtPatients.Rows[i]["sPhone"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_DOB, Convert.ToString(dtPatients.Rows[i]["dtDOB"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_DTLAPPOINTMENTID, Convert.ToString(dtPatients.Rows[i]["nDTLAppointmentID"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_LOCATION, Convert.ToString(dtPatients.Rows[i]["sLocationName"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_MissingChargesDetails, Convert.ToString(dtPatients.Rows[i]["MissingChargeDetails"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_LOCATIONID, Convert.ToString(dtPatients.Rows[i]["nLocationId"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_PROVIDERID, Convert.ToString(dtPatients.Rows[i]["nProviderID"]));
                        c1MissingCharges.SetData(NewRow.Index, COL_APPOINTMENTTYPEID, Convert.ToString(dtPatients.Rows[i]["nAppointmentTypeID"]));
                    }
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.MissingCharges, gloAuditTrail.ActivityType.View, "View Missing Charges", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                c1MissingCharges.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_DATEOFSERVICE);

                if (c1MissingCharges.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                    tls_RemoveAppt.Enabled = false;
                    tls_Select.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                    if (chkShowRemovedAppt.Checked)
                    {
                        tls_RemoveAppt.Enabled = false;
                    }
                    else
                    {
                        tls_RemoveAppt.Enabled = true;
                    }
                    tls_Select.Enabled = true;
                  

                    if (iselected < c1MissingCharges.Rows.Count)
                    {
                        c1MissingCharges.Select(iselected, c1MissingCharges.Cols[0].Index);
                    }
                    else
                    {
                        c1MissingCharges.Select(1, c1MissingCharges.Cols[0].Index);
                    }
                    iselected = 1;
                }
                tls_Select.Text = "Select All";
                tls_Select.Tag = "Select";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private DataTable GetMissingCharges(ReportBy ReportUsing, Boolean bShowUnlinkAppt = false)
        {
            DataTable _dt = null;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
                     
            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                if (bShowUnlinkAppt)
                {
                    _sqlcommand.CommandText = "rpt_Missing_Charges_LinkedAppt";
                }
                else
                {
                    _sqlcommand.CommandText = "rpt_Missing_Charges";
                }
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                Int64 stDate = 0;
                Int64 endDate = 0;
                stDate = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());


                _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nStartDate"].Value = stDate;

                _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nEndDate"].Value = endDate;

                _sqlcommand.Parameters.Add("@nClinicId", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nClinicId"].Value = _clinicId;

                StringBuilder sbSelectedProviderID = new StringBuilder();
                Int64 _nProviderCount = 0;
                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        _nProviderCount++;

                        if (_nProviderCount == 1)
                        {
                            sbSelectedProviderID.Append(Convert.ToString(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1)) );
                        }
                        else
                        {
                            sbSelectedProviderID.Append("," + Convert.ToString(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1)));
                        }
                    }
                }
                if (sbSelectedProviderID.Length >0)
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = sbSelectedProviderID.ToString();
                }


               
                StringBuilder sbSelectedApptTypeID = new StringBuilder();
                Int64 _nApptTypeCount = 0;
                for (int i = 0; i < trvApptType.Nodes.Count; i++)
                {
                    if (trvApptType.Nodes[i].Checked == true)
                    {
                        _nApptTypeCount++;
                        if (_nApptTypeCount == 1)
                        {
                            sbSelectedApptTypeID.Append(Convert.ToString(GetTagElement(trvApptType.Nodes[i].Tag.ToString(), '~', 1)));
                        }
                        else
                        {
                            sbSelectedApptTypeID.Append("," + Convert.ToString(GetTagElement(trvApptType.Nodes[i].Tag.ToString(), '~', 1)));
                        }
                    }
                }
                if (sbSelectedApptTypeID.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@nAppointmentTypeID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nAppointmentTypeID"].Value = sbSelectedApptTypeID.ToString();  
                }


                if (chkShowRemovedAppt.Checked)
                {
                     _sqlcommand.Parameters.Add("@bShowDeleltedAppt", System.Data.SqlDbType.Bit);
                     _sqlcommand.Parameters["@bShowDeleltedAppt"].Value = chkShowRemovedAppt.Checked;            
                }

                //if (cmbLocationName.SelectedValue != null)
                //{
                //    Int64 _LocationId = Convert.ToInt64(cmbLocationName.SelectedValue);
                //    if (_LocationId != 0)
                //    {
                //        _sqlcommand.Parameters.Add("@nLocationID", System.Data.SqlDbType.BigInt);
                //        _sqlcommand.Parameters["@nLocationID"].Value = _LocationId;
                //    }
                //}



                StringBuilder sbSelectedALocation = new StringBuilder();
                Int64 _nLocationCount = 0;
                for (int i = 0; i < trvLocation.Nodes.Count; i++)
                {
                    if (trvLocation.Nodes[i].Checked == true)
                    {
                        _nLocationCount++;
                        if (_nLocationCount == 1)
                        {
                            sbSelectedALocation.Append(Convert.ToString(GetTagElement(trvLocation.Nodes[i].Tag.ToString(), '~', 1)));
                        }
                        else
                        {
                            sbSelectedALocation.Append("," + Convert.ToString(GetTagElement(trvLocation.Nodes[i].Tag.ToString(), '~', 1)));
                        }
                    }
                }
                if (sbSelectedALocation.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@nLocationID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nLocationID"].Value = sbSelectedALocation.ToString() ;
                }

                _sqlcommand.Parameters.Add("@bExcludeNoCharge", System.Data.SqlDbType.Int);
                if (chkExcludeNoCharge.Checked)
                {
                    _sqlcommand.Parameters["@bExcludeNoCharge"].Value = 1;
                }
                else
                {
                    _sqlcommand.Parameters["@bExcludeNoCharge"].Value = 0;
                }

                if (!bShowUnlinkAppt)
                {
                    _sqlcommand.Parameters.Add("@nShowReportBy", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nShowReportBy"].Value = ReportUsing.GetHashCode();
                }


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                _dt = new DataTable();
                da.Fill(_dt);
                da.Dispose();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oConnection != null)
                {
                    oConnection.Close();
                    oConnection.Dispose(); 
                }
                _sqlcommand.Parameters.Clear();  
                _sqlcommand.Dispose();
            }
            return _dt;
        }

        private void ExportReportToExcel(bool OpenReport)
        {
            //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    _Checked = false;
                }
                _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                oSettings.Dispose();

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

                if (_DefaultLocationPath != "" && _Checked == true)
                {
                    if (_DefaultLocationPath.EndsWith("\\"))
                    {
                        char[] trimChars = { '\\' };
                        _DefaultLocationPath = _DefaultLocationPath.TrimEnd(trimChars);
                    }
                    // If not exist create directory
                    if (Directory.Exists(_DefaultLocationPath) == false)
                    {
                        Directory.CreateDirectory(_DefaultLocationPath);
                    }

                    saveFileDialog.InitialDirectory = _DefaultLocationPath;
                    //_FilePath = _DefaultLocationPath + "\\Missing Charges";
                    //_FilePath += Convert.ToString(DateTime.Now).Replace(":", "");
                    //_FilePath = _FilePath.Replace("/", "") + ".xls";
                }
                
                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
                c1MissingCharges.Cols[1].Visible = false;
                c1MissingCharges.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                c1MissingCharges.Cols[1].Visible = true; 
                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    { System.Diagnostics.Process.Start(_FilePath); }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(IOException)// ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ioEx.ToString();
                //ioEx = null;
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              
            }
        }

        #region "Sort By Criteria "

        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmb_datefilter.Items.Clear();
                cmb_datefilter.Items.Add("Custom");
                cmb_datefilter.Items.Add("Today");
                cmb_datefilter.Items.Add("Tomorrow");
                cmb_datefilter.Items.Add("Yesterday");
                cmb_datefilter.Items.Add("This Week");
                cmb_datefilter.Items.Add("Last Week");
                cmb_datefilter.Items.Add("Current Month");
                cmb_datefilter.Items.Add("Last Month");
                cmb_datefilter.Items.Add("Current Year");
                cmb_datefilter.Items.Add("Last 30 Days");
                cmb_datefilter.Items.Add("Last 60 Days");
                cmb_datefilter.Items.Add("Last 90 Days");
                cmb_datefilter.Items.Add("Last 120 Days");
                cmb_datefilter.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillProviderData()
        {
            try
            {
                DataTable dtProviders = null;
                dtProviders = gloCharges.GetCachedProviders();

                DataRow dr = dtProviders.NewRow();
                dr["nProviderID"] = 0;
                dr["sProviderName"] = "<None>";
                dtProviders.Rows.InsertAt(dr, 0);

                if (dtProviders != null)
                {
                    if (dtProviders.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtProviders.Rows.Count; i++)
                        {
                            TreeNode oNode = new TreeNode();
                            oNode.Text = dtProviders.Rows[i]["sProviderName"].ToString();
                            oNode.Tag = dtProviders.Rows[i]["nProviderID"] + "~" + dtProviders.Rows[i]["sProviderName"];
                            trvProvider.Nodes.Add(oNode);
                            oNode = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillAppoinmentType()
        {
            try
            {
                DataTable dtAppointType = null;
                dtAppointType = GetAppointmentTypesList();

                DataRow dr = dtAppointType.NewRow();
                dr["nAppointmentTypeID"] = 0;
                dr["sAppointmentType"] = "<None>";
                dtAppointType.Rows.InsertAt(dr, 0);

                if (dtAppointType != null)
                {
                    if (dtAppointType.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAppointType.Rows.Count; i++)
                        {
                            TreeNode oNodeApptType = new TreeNode();
                            oNodeApptType.Text = dtAppointType.Rows[i]["sAppointmentType"].ToString();
                            oNodeApptType.Tag = dtAppointType.Rows[i]["nAppointmentTypeID"] + "~" + dtAppointType.Rows[i]["sAppointmentType"]; 
                            trvApptType.Nodes.Add(oNodeApptType);
                            oNodeApptType = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillLocationName()
        {
           
            DataTable dtLocation = null;
            try
            {
                dtLocation = GetLocationNames();

                if (dtLocation != null)
                {
                    if (dtLocation.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLocation.Rows.Count; i++)
                        {
                            TreeNode oNodeLocation = new TreeNode();
                            oNodeLocation.Text = dtLocation.Rows[i]["sLocation"].ToString();
                            oNodeLocation.Tag = dtLocation.Rows[i]["nLocationID"] + "~" + dtLocation.Rows[i]["sLocation"]; ;
                            trvLocation.Nodes.Add(oNodeLocation);
                            oNodeLocation = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        
        public DataTable GetAppointmentTypesList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAppointmentType = new DataTable();
            string _strSQL = "";

            try
            {
                oDB.Connect(false);
                _strSQL = "select nAppointmentTypeID, sAppointmentType from AB_AppointmentType where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType ";
                oDB.Retrive_Query(_strSQL, out dtAppointmentType);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dtAppointmentType;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtAppointmentType.Dispose();
                dtAppointmentType = null;
            }
        }
        
        public DataTable GetLocationNames()
        {
            DataTable dtLocationName = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                string _strSQL = "";
                oDB.Connect(false);
                _strSQL = "SELECT  nLocationID,sLocation FROM AB_Location ORDER BY nLocationID";
                oDB.Retrive_Query(_strSQL, out dtLocationName);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dtLocationName;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtLocationName.Dispose();
                dtLocationName = null;
            }
        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            string[] temp;
            try
            {
                temp = TagContent.Split(Delimeter);
                if (Position - 1 < temp.Length)
                {
                    return temp[Position - 1];
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return (object)"";
        }

        #region " Methods "
        private void FilterBy_Today()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_Tomorrow()
        {
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Yesterday()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }
                
        private void FilterBy_Thisweek()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastweek()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currentmonth()
        {
            DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);

            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;


        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }
        
        #endregion

       #endregion "Sort By Criteria "

        private void MissingChargesSelectionDeselection()
        {
            C1.Win.C1FlexGrid.CheckEnum oStatus = C1.Win.C1FlexGrid.CheckEnum.None;
           try
            {

                if (tls_Select.Tag.ToString() == "Select")
                {
                    tls_Select.Text = "DeSelect All";
                    tls_Select.Tag = "Deselect";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Checked;
                }
                else if (tls_Select.Tag.ToString() == "Deselect")
                {
                    tls_Select.Text = "Select All";
                    tls_Select.Tag = "Select";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                }
                               
                if (c1MissingCharges != null && c1MissingCharges.Rows.Count > 0)
                {
                    for (int i = 1; i < c1MissingCharges.Rows.Count; i++)
                    {
                        c1MissingCharges.SetCellCheck(i, 1, oStatus);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            
        }

        private void RemoveFromReport(String sAppointDetailsId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = " ";
            try
            {
                oDB.Connect(false);
                strSQL = "UPDATE AS_Appointment_DTL SET bRemoveFromReport=1 WHERE nDTLAppointmentID IN (" + sAppointDetailsId + ")";
                int result = oDB.Execute_Query(strSQL);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

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

        #endregion

        private void tlsUnlinkAppt_Click(object sender, EventArgs e)
        {
            try
            {
                ShowReport(ReportBy.DOS,true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1MissingCharges_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
        }

        private void rdReportByDOS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdReportByDOS.Checked)
            {
                rdReportByDOS.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rdReportByDOS_Provider.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdReportByDOS_Provider_CheckedChanged(object sender, EventArgs e)
        {
            if (rdReportByDOS_Provider.Checked)
            {
                rdReportByDOS_Provider.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rdReportByDOS.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

    }
}
