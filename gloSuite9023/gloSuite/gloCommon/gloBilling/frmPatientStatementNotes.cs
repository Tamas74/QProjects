using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmPatientStatementNotes : Form
    {
        
        #region " Declarations "
        
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
      //  private bool _CheckFormLoad = false; 
        private Int64 _nStatementNoteID = 0;
     //   Int64 _nTempStatementNoteID = 0;
        //Int64 _nPAccountID = 0;
        //Int64 _nAccountPatientID = 0;
      //  gloPatientStripControl.gloPatientStripControl oPatientControl = null;

        #endregion " Declarations "

        #region  " Property Procedures "

        //public Int64 PAccountID
        //{
        //    get { return _nPAccountID; }
        //    set { _nPAccountID = value; }
        //}

        //public Int64 AccountPatientID
        //{
        //    get { return _nAccountPatientID; }
        //    set { _nAccountPatientID = value; }
        //}
        public Int64 SelectedStmtNotesID { get; set; }
        #endregion  " Property Procedures "
        
        #region " Constructor "
        
        public frmPatientStatementNotes(string databaseconnectionstring,Int64 PatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = PatientID;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion
        }
        
        #endregion " Constructor "

        #region " Form Load "

        private void frmPatientStatementNotes_Load(object sender, EventArgs e)                   
        {
            //_CheckFormLoad = true; 
            gloC1FlexStyle.Style(C1AllNotes, false);

           // LoadPatientStrip(_PatientID, 0, true);
            //txtStatementNote.Text = GetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
            GetLastNotes(_PatientID);
            panel1.SendToBack();

            if (SelectedStmtNotesID != 0)
            {
                int iRow = C1AllNotes.FindRow(Convert.ToString(SelectedStmtNotesID), 1, 0, true);
                if (iRow > 0)
                {
                    C1AllNotes.Select(iRow, 0);
                }
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            if (txtStatementNote.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter the patient statement note.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementNote.Select();
                                break;
                            }
                            else
                            {
                                Int64 _Result = SetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value, txtStatementNote.Text);
                                if (_Result > 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Patinet Statement Note", _PatientID, _PatientID, 0, ActivityOutCome.Success);
                                    GetLastNotes(_PatientID);
                                    pnlDetail.Visible = false;       
                                }
                                else if (_Result == 0 || _Result != -1)
                                {
                                    MessageBox.Show("Patinet Statement note not added, Try again.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtStatementNote.Select();
                                    break;
                                }
                                else if (_Result == -1)
                                { break; }
                               
                            }        
                         
                        }
                        break;
                    case "SaveClose":
                        {

                            if (txtStatementNote.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter the patient statement note.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementNote.Select();
                                break;
                            }
                            else
                            {
                                Int64 _Result = SetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value, txtStatementNote.Text);
                                if (_Result > 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Patinet Statement Note", _PatientID, _PatientID, 0, ActivityOutCome.Success);
                                    this.Close();
                                }
                                else  if (_Result == 0  || _Result != -1  )
                                {                                
                                    MessageBox.Show("Patinet Statement note not added, Try again.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                                                 
                                    txtStatementNote.Select();
                                    break;
                                }
                                else if (_Result == -1)
                                { break; }
                              
                            }     
                                               
                        }
                        break;
                    case "NewNote":
                        pnlDetail.Visible = true; 
                        dtFromDate.Value = DateTime.Now;
                        dtToDate.Value = DateTime.Now.AddMonths(1);
                        //_CheckFormLoad = true; 
                        txtStatementNote.Text = "";                         
                        _nStatementNoteID = 0;
                        tlb_EditNotes.Enabled = false;
                        tlb_Delete.Enabled = false;
                        tlb_Notes.Enabled = false;
                        tls_btnOK.Enabled = true;
                        tls_btnSaveClose.Enabled = true;
                        tlb_Cancel.Visible = true;
                        tls_btnCancel.Visible = false; 
                        break;
                    case "Edit":                       
                        pnlDetail.Visible = true;
                        SetValues();
                        tls_btnOK.Enabled = true;
                        tls_btnSaveClose.Enabled = true;
                        tlb_Cancel.Visible = true;
                        tls_btnCancel.Visible = false;
                        tlb_Delete.Enabled = false;
                        tlb_Notes.Enabled = false;
                        tlb_EditNotes.Enabled = false;
                        //_CheckFormLoad = true;  
                        break; 
                    case "Delete":
                        if (MessageBox.Show("Are you sure you want to delete selected note?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            SetValues(); 
                            if (DeleteStatementNotes())
                            {
                                GetLastNotes(_PatientID);
                                Int64 HistoryNoteID = gloCharges.InsertNoteBeforeDelete(_nStatementNoteID, "Patient Statement", "Patient_Statement_Notes");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Delete, "Deleted Patinet Statement Note", _PatientID, HistoryNoteID, 0, ActivityOutCome.Success);

                                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Delete, "Deleted Patinet Statement Note", 0, _PatientID, 0, ActivityOutCome.Success);
                            }
                        }
                        break;
                    case "Cancel":
                      
                            txtStatementNote.Text = "";                            
                            pnlDetail.Visible = false;
                            tlb_Cancel.Visible = false;
                            tls_btnCancel.Visible = true;
                        
                            if (C1AllNotes.Rows.Count == 1)
                            {
                                tlb_EditNotes.Enabled = false;
                                tlb_Delete.Enabled = false;
                                tls_btnOK.Enabled = false;
                                tls_btnSaveClose.Enabled = false;
                                tlb_Notes.Enabled = true;
                            }
                            else
                            {
                                tlb_EditNotes.Enabled = true;
                                tlb_Delete.Enabled = true;
                                tls_btnOK.Enabled = false;
                                tls_btnSaveClose.Enabled = false;
                                tlb_Notes.Enabled = true;
                                _nStatementNoteID = 0;
                            }                     

                        break;
                    case "Close":
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
      
        #endregion " Tool Strip Event "

        //#region " Patient Strip Control Events "

        //void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (oPatientControl.PatientID > 0)
        //    {
        //        _PatientID = oPatientControl.PatientID;
        //        oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 1, false);
        //        GetLastNotes(_PatientID);
        //        _CheckFormLoad = true; 
        //        //txtStatementNote.Text = GetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
        //    }
        //}

        //void oPatientControl_ControlSize_Changed(object sender, EventArgs e)
        //{

        //}

        //void oPatientControl_PatientModified(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (oPatientControl.PatientID > 0)
        //        {
        //            _PatientID = oPatientControl.PatientID;
        //            GetLastNotes(_PatientID);
        //            _CheckFormLoad = true; 
        //            //txtStatementNote.Text = GetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}

        //private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        //{
        //    try
        //    {

        //        if (oPatientControl != null)
        //        {
        //            for (int i = 0; i < this.Controls.Count; i++)
        //            {
        //                if (oPatientControl.Name == this.Controls[i].Name)
        //                {
        //                    this.Controls.RemoveAt(i);
        //                    break;
        //                }
        //            }
        //        }
        //        oPatientControl = new gloPatientStripControl.gloPatientStripControl(_databaseconnectionstring, SearchEnable);
        //        oPatientControl.ControlSize_Changed += new gloPatientStripControl.gloPatientStripControl.ControlSizeChanged(oPatientControl_ControlSize_Changed);
        //        oPatientControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
        //        oPatientControl.PatientModified += new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);
        //        oPatientControl.btnDownEnable = true;
        //        oPatientControl.btnUpEnable = true;
        //        oPatientControl.DTP.Visible = false;
        //        oPatientControl.FillDetails(PatientId, gloPatientStripControl.FormName.None, PatientProviderId, false);                
        //        this.Controls.Add(oPatientControl);                
        //        oPatientControl.Dock = DockStyle.Top;
        //        oPatientControl.SendToBack();
        //        oPatientControl.Padding = new Padding(3, 0, 3, 0);
        //        panel1.SendToBack();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //}

        //#endregion " Patient Strip Control Events "

        #region " Form Control Events"

        private void dtFromDate_ValueChanged(object sender, EventArgs e)
        {
           // txtStatementNote.Text = GetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
        }

        private void dtToDate_ValueChanged(object sender, EventArgs e)
        {
           // txtStatementNote.Text = GetStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
        }

        #endregion

        #region "Fill Methods"
        #endregion

        #region "Private Methods"

        private String GetStatementNotes(Int64 PatientID,DateTime FromDate,DateTime ToDate)
        {
            String _result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                String _SQLString = "select nStatementNoteID,isnull(nFromDate,0) as nFromDate,isnull(nTodate,0) as nTodate,isnull(sStatementNote,'') as sStatementNote from Patient_Statement_Notes WITH (NOLOCK) "
                              + " where (" + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " >= nFromDate and " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " <= nTodate and nPatientID=" + PatientID + " and nClinicID=" + _ClinicID + ") or "
                              + " (" + gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()) + " >= nFromDate and " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()) + " <= nTodate and nPatientID=" + PatientID + " and nClinicID=" + _ClinicID + ")";
                DataTable _sresult;
                oDB.Retrive_Query(_SQLString,out _sresult);
                if (_sresult != null && _sresult.Rows.Count > 0)
                {
                    //dgAllNotes.DataSource = _sresult;

                    DesignGrid(_sresult);
                    //dgAllNotes.Rows[0].Selected = true; 
                    _result = Convert.ToString(_sresult.Rows[0][1]);
                    _nStatementNoteID = Convert.ToInt64(_sresult.Rows[0][0]);

                }
                else
                {
                    _result = "";
                    _nStatementNoteID = 0;

                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private void DesignGrid(DataTable _sresult)
        {
            C1AllNotes.Redraw = true;
            C1AllNotes.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;


            C1AllNotes.Clear();
            C1AllNotes.Cols.Count = 4;
            C1AllNotes.Rows.Count = 1;
            C1AllNotes.Rows.Fixed = 1;
            C1AllNotes.Cols.Fixed = 0;
            C1AllNotes.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;


            //dgAllNotes.Cols["nStatementNoteID"].Visible = false;
            C1AllNotes.Cols[0].Caption = "nStatementNoteID";
            C1AllNotes.Cols[1].Caption = "From Date";
            C1AllNotes.Cols[2].Caption = "To Date";
            C1AllNotes.Cols[3].Caption = "Statement Note";



            C1AllNotes.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1AllNotes.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1AllNotes.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1AllNotes.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;



            //int _nWidth = 0;
            //_nWidth = 820;//Convert.ToInt32( c1QueuedClaims.Width);
            //C1AllNotes.Cols[0].Width = Convert.ToInt32(_nWidth * 0.10);
            //C1AllNotes.Cols[1].Width = Convert.ToInt32(_nWidth * 0.10);
            //C1AllNotes.Cols[2].Width = Convert.ToInt32(_nWidth * 0.10);
            C1AllNotes.Cols[3].Width = 522;

            C1AllNotes.Cols[0].Visible = false;


            Int32 rowIndex = 1;
            for (int i = 0; i < _sresult.Rows.Count; i++)
            {
               
                if (i >= 0)
                {
                    C1AllNotes.Rows.Add();
                    rowIndex = C1AllNotes.Rows.Count - 1;

                    C1AllNotes.SetData(rowIndex, 0, _sresult.Rows[i]["nStatementNoteID"].ToString());
                
                    //dgAllNotes.SetData(rowIndex, 2, _sresult.Rows[i]["nTransactionID"].ToString());
                    
                   
                    string _tempdate = "";
                    if (_sresult.Rows[i]["nFromDate"] != null && Convert.ToString(_sresult.Rows[i]["nFromDate"]) != "" && Convert.ToString(_sresult.Rows[i]["nFromDate"]) != "0")
                    {
                        _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_sresult.Rows[i]["nFromDate"]))).ToShortDateString();                       
                    }
                    C1AllNotes.SetData(rowIndex, 1, _tempdate);


                   _tempdate = "";
                    if (_sresult.Rows[i]["nTodate"] != null && Convert.ToString(_sresult.Rows[i]["nTodate"]) != "" && Convert.ToString(_sresult.Rows[i]["nTodate"]) != "0")
                    {
                        _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_sresult.Rows[i]["nTodate"]))).ToShortDateString();
                    }
                    C1AllNotes.SetData(rowIndex, 2, _tempdate);

                    C1AllNotes.SetData(rowIndex, 3, _sresult.Rows[i]["sStatementNote"].ToString());

                }
            }          
         
        }

        /// <summary>
        /// returns null on exceptions
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        private Int64 GetCountForStatementNotes(Int64 PatientID, DateTime FromDate, DateTime ToDate)
        {
            Int64 _result =0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //String _SQLString = "select nStatementNoteID from Patient_Statement_Notes "
                //              + " where (" + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " >= nFromDate and " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " <= nTodate and nPatientID=" + PatientID + " and nClinicID=" + _ClinicID + ") or "
                //              + " (" + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " >= nFromDate and " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()) + " <= nTodate and nPatientID=" + PatientID + " and nClinicID=" + _ClinicID + ")";
                string _SQLString = "SELECT nStatementNoteID FROM Patient_Statement_Notes WITH (NOLOCK) "
                                  + " WHERE ( (nFromDate  BETWEEN " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " AND " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()) + " ) " 
                                  + "or ( nToDate BETWEEN " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()) + " AND " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()) + " )) AND nPatientID=" + PatientID + " AND nClinicID=" + _ClinicID + " ";

                DataTable Dt = null;
                // _res =
                oDB.Retrive_Query(_SQLString, out Dt);

                if (Dt != null && Dt.Rows.Count == 1)
                {
                    return Convert.ToInt64(Dt.Rows[0][0].ToString());
                }
                else if (Dt != null && Dt.Rows.Count > 1)
                    return -1;
                else
                    return 0;
               
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {                
               DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }


        private bool DeleteStatementNotes()
        {
            bool _result = false; 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                
                    if (_nStatementNoteID != 0)
                    {
                        
                        string _SQLDeleteString = "Delete from Patient_Statement_Notes where nStatementNoteID = " + _nStatementNoteID.ToString();
                        oDB.Execute_Query(_SQLDeleteString);
                      
                        _nStatementNoteID = 0;
                        _result= true;
                    }                   

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                _result= false ;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 SetStatementNotes(Int64 PatientID, DateTime FromDate, DateTime ToDate, String StatementNotes)
        {
            Int64 _result = 0;
            if (Convert.ToDateTime(dtFromDate.Value.ToShortDateString()) <= Convert.ToDateTime(dtToDate.Value.ToShortDateString()))
            {

                Int64 strNote = GetCountForStatementNotes(_PatientID, dtFromDate.Value, dtToDate.Value);
                if (strNote == 0 || _nStatementNoteID == strNote)
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    try
                    {
                        object _intresult = 0;
                        oDBParameters.Add("@nStatementNoteID", _nStatementNoteID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@nFromDate", gloDateMaster.gloDate.DateAsNumber(FromDate.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nToDate", gloDateMaster.gloDate.DateAsNumber(ToDate.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@sStatementNote", StatementNotes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@nClinicID", this._ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nAccountPatientID", AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        _result = oDB.Execute("gsp_INUP_Patient_Statement_Notes", oDBParameters, out _intresult);

                        if (_intresult != null)
                        {
                            if (_intresult.ToString().Trim() != "")
                            {
                                if (Convert.ToInt64(_intresult) > 0)
                                {
                                    _result = Convert.ToInt64(_intresult.ToString());
                                    _nStatementNoteID = _result;
                                }
                            }
                        }

                    }
                    catch (gloDatabaseLayer.DBException DBErr)
                    {
                        DBErr.ERROR_Log(DBErr.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Only one note allowed at a time.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1; 
                    //if (strNote != null)
                    //    return Convert.ToInt64(strNote);
                    //else
                    //    return 0;
                }
            }
            else
            {
                MessageBox.Show("From date can not be greater than to date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1; 
            }

            return _result;
        }

        private void GetLastNotes(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                String _SQLString = "select nStatementNoteID,isnull(nFromDate,0) as nFromDate,isnull(nTodate,0) as nTodate,isnull(sStatementNote,'') as sStatementNote from Patient_Statement_Notes WITH (NOLOCK) "
                              + " where nPatientID=" + PatientID + " and nClinicID=" + _ClinicID + " order by nFromDate desc";
                DataTable _sresult;
                oDB.Retrive_Query(_SQLString, out _sresult);
                //dgAllNotes.DataSource = _sresult;
                DesignGrid(_sresult);
                if (_sresult != null && _sresult.Rows.Count > 0)
                {
                    
                    //C1AllNotes.Rows[0].Selected = true;
                    //if (Convert.ToInt64(_sresult.Rows[0][1]) != 0)
                    //{
                    //    dtFromDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_sresult.Rows[0][1]));
                    //}
                    //if (Convert.ToInt64(_sresult.Rows[0][2]) != 0)
                    //{
                    //    dtToDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_sresult.Rows[0][2]));
                    //}
                    //txtStatementNote.Text = Convert.ToString(_sresult.Rows[0][3]);
                    _nStatementNoteID = Convert.ToInt64(_sresult.Rows[0][0]);
                    tlb_Delete.Enabled = true;                    
                    tlb_Notes.Enabled = true;
                    tls_btnOK.Enabled = false;
                    tls_btnSaveClose.Enabled = false;
                    tlb_Cancel.Visible = false;
                    tls_btnCancel.Visible = true;
                    tlb_EditNotes.Enabled = true; 
                }
                else
                {
                    tlb_Delete.Enabled = false;  
                    _nStatementNoteID = 0;
                    dtFromDate.Value = DateTime.Now;
                    dtToDate.Value = DateTime.Now.AddMonths(1);
                    txtStatementNote.Text = "";
                    tlb_Delete.Enabled = false;
                    tlb_Notes.Enabled = true;
                    tls_btnOK.Enabled = false;
                    tls_btnSaveClose.Enabled = false;
                    tlb_Cancel.Visible = false;
                    tlb_EditNotes.Enabled = false; 
                    tls_btnCancel.Visible = true;
                    
                    //txtStatementNote.Text = GetStatementNotes(PatientID, dtFromDate.Value, dtToDate.Value);
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }            
        }

        #endregion

        private void SetDefault()
        {
            _nStatementNoteID = 0;
            txtStatementNote.Text = "";
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now.AddMonths(1);
        }       

        private void SetValues()
        {

            //_CheckFormLoad = true;
            //if (dgAllNotes.CursorCell. != null)
            //{
            int _index = C1AllNotes.RowSel;
            if (_index > 0)
            {
                if (C1AllNotes.GetData(_index, 1) != null)
                {
                    //_CheckFormLoad = true;
                    //if (Convert.ToInt64(dgAllNotes.GetData(_index, 1).ToString()) != 0)
                    //{
                    dtFromDate.Value = Convert.ToDateTime(C1AllNotes.GetData(_index, 1).ToString());
                    // dtFromDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dgAllNotes.GetData(_index, 1).ToString()));
                    //}
                    //if (Convert.ToInt64(dgAllNotes.GetData(_index, 2).ToString()) != 0)
                    //{
                    dtToDate.Value = Convert.ToDateTime(C1AllNotes.GetData(_index, 2).ToString());
                    // dtToDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dgAllNotes.GetData(_index, 2).ToString()));
                    //}
                    txtStatementNote.Text = Convert.ToString(C1AllNotes.GetData(_index, 3).ToString());

                    _nStatementNoteID = Convert.ToInt64(C1AllNotes.GetData(_index, 0).ToString());

                }
            }
            //}


              
        }
               
        private void C1AllNotes_MouseMove(object sender, MouseEventArgs e)
        {
            //gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
        
        private void txtStatementNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (_CheckFormLoad != true && txtStatementNote.Text.Length >= 200)
            //{
            //    MessageBox.Show("Only 200 characters are allowed.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //_CheckFormLoad = false;
          
        }

        private void txtStatementNote_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtStatementNote_TextChanged(object sender, EventArgs e)
        {
            //if (_CheckFormLoad != true && txtStatementNote.Text.Length >= 200)
            //{
            //    MessageBox.Show("Only 200 characters are allowed.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                
            //}
            //_CheckFormLoad = false;
        }

        private void txtStatementNote_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void C1AllNotes_RowColChange(object sender, EventArgs e)
        {

            // Commeneted By Shweta In 6052 (12172011) against TFS Bug Id : 17109 
            // 1.The issue was related to update note while adding 'New note' if we have click the statement note grid then the statementnoteId 
            //was assigned and due to which the selected notes gets modified with new note.
            // 2.We are not selecting the current selected row statement note information while modifying note. 
            // therefore this code is not needed here 

            //int _index = C1AllNotes.RowSel;
            //if (_index > 0)
            //{
            //    if (C1AllNotes.GetData(_index, 1) != null)
            //    {
            //      _nStatementNoteID = Convert.ToInt64(C1AllNotes.GetData(_index, 0).ToString());
            //    }
            //}
        }

        #region "Browse Quick Notes"
        private void btnBrowseNotes_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {
                ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.StatementPatient.GetHashCode());

                ofrmQuickNotes.ShowDialog(this);
                if (txtStatementNote.Text != "")
                    txtStatementNote.Text = txtStatementNote.Text + " " + ofrmQuickNotes.Note;
                else
                    txtStatementNote.Text = ofrmQuickNotes.Note;

                const int MaxChars = 200;
                if (txtStatementNote.Text.Length > MaxChars)
                    txtStatementNote.Text = txtStatementNote.Text.Substring(0, MaxChars);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.Dispose(); ofrmQuickNotes = null;
                }
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
                return;
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        //private void dgAllNotes_SelectionChanged(object sender, EventArgs e)
        //{
            //if (dgAllNotes.c != null && dgAllNotes.CurrentRow.Index != null)
            //{
            //    if (Convert.ToInt64(dgAllNotes.CurrentRow.Cells[1].Value.ToString()) != 0)
            //    {
            //        dtFromDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dgAllNotes.CurrentRow.Cells[1].Value.ToString()));
            //    }
            //    if (Convert.ToInt64(dgAllNotes.CurrentRow.Cells[2].Value.ToString()) != 0)
            //    {
            //        dtToDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dgAllNotes.CurrentRow.Cells[2].Value.ToString()));
            //    }
            //    txtStatementNote.Text = Convert.ToString(dgAllNotes.CurrentRow.Cells[3].Value.ToString());
            //    _nStatementNoteID = Convert.ToInt64(dgAllNotes.CurrentRow.Cells[0].Value.ToString());
            //}
        //}

    }
}