using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;
using gloBilling.Common;
namespace gloBilling
{
    public partial class frmClaimNotes : Form
    {

        #region "Variable Declarations"

        private string _databaseConnection = "";
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";

        private Int64 _MasterTransactionID = 0;
        private Int64 _TransactionID = 0;
        private Int64 _TransactionDetailID = 0;
        private Int64 _TransactionLineNo = 0;
        private Int64 _nClaimNo = 0;
        private bool _IsFormLoad = false;
        private DateTime _ModifyDate = DateTime.Now;

        public Common.GeneralNote oNote = new global::gloBilling.Common.GeneralNote();
        public Common.GeneralNotes oNotes = null;

        private string _messageBoxCaption = "";
        public bool oDialogResult = false;
        private bool chkSave_Flag = false;
        private bool chkDelete_flag = false;
        public bool _IsVoidShowNote = false;
        
        //C1 values
        private Int64 _C1TranID = 0;
        private Int64 _C1NoteID = 0;
        private string _C1Notes = "";
        public Int32 _C1SelectedRow = 0;
        public bool _bIsAddNotesFlag = false;
        public Int32 _C1NoteRowID = 0; //23
        public DateTime _dtNoteCreatedDatetime;
        
        #endregion

        #region Properties
        
        public bool IsVoidShowNote
        {
            get { return _IsVoidShowNote; }
            set
            {
                _IsVoidShowNote = value;
            }
        }

        #endregion

        #region " Column Constants "

        const int COL_NOTESSTYPE = 0;
        const int COL_DATE = 1;
        const int COL_NOTES = 2;
        const int COL_USER = 3;
        const int COL_STATEMENTNOTES = 4;
        const int COL_CLOSEDATE = 5;
        const int COL_CLAIMNO = 6;
        const int COL_TRANSACTIONID = 7;
        const int COL_CLINICID = 8;
        const int COL_ID = 9;
        const int COL_USERNAME = 10;
        const int COL_NOTEROWNO = 11; //23
        const int COL_DTCREATEDDATETIME = 12; //23

        const int COL_COUNT = 13;

        #endregion " Column Constants "

        #region "Constructor"

        public frmClaimNotes(Int64 TransactionID)
        {
            InitializeComponent();
            _TransactionID = TransactionID;
            #region " Retrieve Global Settings "

            _UserID = gloPMGlobal.UserID;
            _UserName = gloPMGlobal.UserName;
            _messageBoxCaption = gloPMGlobal.MessageBoxCaption;
            _databaseConnection = gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloPMGlobal.ClinicID;
 
            #endregion
          

        }

        #endregion

        #region "Form Load "

        private void frmClaimNotes_Load(object sender, EventArgs e)
        {
            _IsFormLoad = true;
            gloC1FlexStyle.Style(C1NotesGrid, false);
            gloCharges.GetMasterTransactionID(_TransactionID, out _MasterTransactionID, out _nClaimNo, out _ModifyDate);
            FillNotes();
        }      

        #endregion

        #region " C1 Grid Design Method "

        public void DesignGrid()
        {
           
            try
            {
                C1NotesGrid.Redraw = true;
                C1NotesGrid.ScrollBars = ScrollBars.Both;
                C1NotesGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

                C1NotesGrid.Clear();
                C1NotesGrid.Cols.Count = COL_COUNT;
                C1NotesGrid.Rows.Count = 1;
                C1NotesGrid.Rows.Fixed = 1;
                C1NotesGrid.Cols.Fixed = 0;
                C1NotesGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Set Headers "
            
                C1NotesGrid.SetData(0, COL_DATE, "Date");
                C1NotesGrid.SetData(0, COL_NOTES, "Note");
                C1NotesGrid.SetData(0, COL_STATEMENTNOTES, "StatementNotes");
                C1NotesGrid.SetData(0, COL_CLOSEDATE, "CloseDate");
                C1NotesGrid.SetData(0, COL_NOTEROWNO, "RowID");
                C1NotesGrid.SetData(0, COL_CLAIMNO, "ClaimNo");
                C1NotesGrid.SetData(0, COL_USER, "UserID");
                C1NotesGrid.SetData(0, COL_USERNAME, "User");
                C1NotesGrid.SetData(0, COL_TRANSACTIONID, "TransactionID");
                C1NotesGrid.SetData(0, COL_ID, "NoteID");
                C1NotesGrid.SetData(0, COL_CLINICID, "ClinicID");
                C1NotesGrid.SetData(0, COL_NOTESSTYPE, "NotesType");
                C1NotesGrid.SetData(0, COL_DTCREATEDDATETIME, "NotesType");

                #endregion " Set Headers "

                #region " Show/Hide "

                C1NotesGrid.Cols[COL_DATE].Visible = true;
                C1NotesGrid.Cols[COL_NOTES].Visible = true;
                C1NotesGrid.Cols[COL_USERNAME].Visible = true;
                C1NotesGrid.Cols[COL_STATEMENTNOTES].Visible = false;
                C1NotesGrid.Cols[COL_CLOSEDATE].Visible = false;
                C1NotesGrid.Cols[COL_NOTEROWNO].Visible = false; //23
                C1NotesGrid.Cols[COL_CLAIMNO].Visible = false;
                C1NotesGrid.Cols[COL_USER].Visible = false;
                C1NotesGrid.Cols[COL_TRANSACTIONID].Visible= false;
                C1NotesGrid.Cols[COL_ID].Visible = false;
                C1NotesGrid.Cols[COL_CLINICID].Visible = false;
                C1NotesGrid.Cols[COL_NOTESSTYPE].Visible = false;
                C1NotesGrid.Cols[COL_DTCREATEDDATETIME].Visible = false;

                #endregion " Show/Hide "

                #region " Width "

                C1NotesGrid.Cols[COL_NOTESSTYPE].Width = 90;
                C1NotesGrid.Cols[COL_DATE].Width = 85;
                C1NotesGrid.Cols[COL_NOTES].Width = 560;
                C1NotesGrid.Cols[COL_USER].Width = 75;
                C1NotesGrid.Cols[COL_STATEMENTNOTES].Width = 0;
                C1NotesGrid.Cols[COL_CLOSEDATE].Width = 0;
                C1NotesGrid.Cols[COL_NOTEROWNO].Width = 0; //23
                C1NotesGrid.Cols[COL_CLAIMNO].Width = 0;
                C1NotesGrid.Cols[COL_USERNAME].Width = 75;
                C1NotesGrid.Cols[COL_DTCREATEDDATETIME].Width = 0;
                #endregion " Width "

                #region " DataType "

                C1NotesGrid.Cols[COL_NOTESSTYPE].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_DATE].DataType = typeof(System.DateTime);
                C1NotesGrid.Cols[COL_DATE].Format = "MM/dd/yyyy";

                C1NotesGrid.Cols[COL_NOTES].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_USER].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_STATEMENTNOTES].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_CLOSEDATE].DataType = typeof(System.DateTime);
                C1NotesGrid.Cols[COL_NOTEROWNO].DataType = typeof(System.Int32); //23
                C1NotesGrid.Cols[COL_CLAIMNO].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_USERNAME].DataType = typeof(System.String);
                #endregion " DataType "

                #region " Styles "

                C1NotesGrid.ShowCellLabels = false;

                C1NotesGrid.Cols[COL_NOTESSTYPE].AllowEditing = false;
                C1NotesGrid.Cols[COL_DATE].AllowEditing = false;
                C1NotesGrid.Cols[COL_NOTES].AllowEditing = false;
                C1NotesGrid.Cols[COL_USERNAME].AllowEditing = false;

                C1NotesGrid.Cols[COL_DATE].AllowSorting = true;
                C1NotesGrid.Cols[COL_NOTES].AllowSorting = false;
                C1NotesGrid.Cols[COL_USERNAME].AllowSorting = false;


                #endregion " Styles "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                C1NotesGrid.Redraw = true;

            }
        }

        #endregion " C1 Grid Design Method "

        #region C1 Grid events"

        private void C1NotesGrid_RowColChange(object sender, EventArgs e)
        {
            string _C1Ntype = "";
            try
            {
                if (panel_NoteDtl.Visible == false)
                {
                    if (IsVoidShowNote)
                    {
                        gloC1FlexStyle.Style(C1NotesGrid, false);

                        tlb_Notes.Enabled = false;
                        tlb_EditNotes.Enabled = false;
                        tlb_Delete.Enabled = false;
                        tlb_Ok.Enabled = false;
                        tlb_Save.Enabled = false;

                    }
                    else
                    {
                        if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                        {
                            _C1SelectedRow = C1NotesGrid.RowSel;

                            if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONID).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE).ToString().Trim().Length > 0)
                            {

                                _C1TranID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_TRANSACTIONID).ToString());
                                _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_ID).ToString());
                                _C1Notes = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTES).ToString());

                                _C1NoteRowID = Convert.ToInt32(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTEROWNO).ToString());//23


                                if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                                {
                                    _C1Ntype = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTESSTYPE).ToString());
                                  
                                }

                                //set notes textbox & notestype 
                                txtNotes.Text = _C1Notes;

                                
                                txtNotes.ReadOnly = true;
                                tlb_EditNotes.Enabled = true;
                                tlb_Delete.Enabled = true;
                            }
                        }
                    }
                }
                else
                {
                    if ((_IsFormLoad == true) || (_C1SelectedRow == 0))
                    {
                        _IsFormLoad = false;
                        return;
                    }
                    C1NotesGrid.Select(_C1SelectedRow, 0, true);
                    return;
                }
            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void C1NotesGrid_MouseMove(object sender, MouseEventArgs e)
        {

            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void C1NotesGrid_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltip1.Hide(); 
        }
     
        private void C1NotesGrid_Click(object sender, EventArgs e)
        {
           
        }


        #endregion

        #region "Fill Methods"

        private void FillNotes()
        {
            try
            {
                DataTable dt;
                DesignGrid();

                tlb_Close.Visible = true;
                tlb_Cancel.Visible = false;

                dt = gloCharges.GetClaimNotes(_MasterTransactionID);
                if (dt != null)
                {
                    //Int64 rowIndex = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            #region "Set Grid Data"

                            C1NotesGrid.Rows.Add();
                            int rowIndex = C1NotesGrid.Rows.Count - 1;
                            C1NotesGrid.SetData(rowIndex, COL_TRANSACTIONID, Convert.ToInt64(dt.Rows[i]["nTransactionID"]));
                            C1NotesGrid.SetData(rowIndex, COL_CLINICID, Convert.ToInt64(dt.Rows[i]["nClinicID"]));
                            C1NotesGrid.SetData(rowIndex, COL_ID, Convert.ToInt64(dt.Rows[i]["nNoteId"]));

                            string _tempdate = "";
                            if (dt.Rows[i]["nNoteDateTime"] != null && Convert.ToString(dt.Rows[i]["nNoteDateTime"]) != "")
                            {
                                _tempdate = Convert.ToDateTime(dt.Rows[i]["nNoteDateTime"].ToString()).ToShortDateString();
                            }
                            C1NotesGrid.SetData(rowIndex, COL_DATE, _tempdate);
                            C1NotesGrid.SetData(rowIndex, COL_USER, Convert.ToString(dt.Rows[i]["nUserID"]));
                            C1NotesGrid.SetData(rowIndex, COL_USERNAME, Convert.ToString(dt.Rows[i]["sUserName"]));
                            C1NotesGrid.SetData(rowIndex, COL_NOTES, Convert.ToString(dt.Rows[i]["sNoteDescription"]));

                            string _CreatedDateTime = "";
                            if (dt.Rows[i]["dtCreatedDateTime"] != null && Convert.ToString(dt.Rows[i]["dtCreatedDateTime"]) != "")
                            {
                                _CreatedDateTime = Convert.ToString(dt.Rows[i]["dtCreatedDateTime"]).ToString();
                            }

                            C1NotesGrid.SetData(rowIndex, COL_DTCREATEDDATETIME, _CreatedDateTime);
                            C1NotesGrid.SetData(rowIndex, COL_NOTEROWNO, i);

                            #endregion
                        }
                    }
                }

                if (C1NotesGrid.Rows.Count == 1)
                {
                    tlb_EditNotes.Visible = false;
                    tlb_Delete.Visible = false;
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;
                    tlb_Notes.Visible = true;
                }
                else
                {
                    if (IsVoidShowNote)
                    {
                        tlb_EditNotes.Visible = false;
                        tlb_Delete.Visible = false;
                        tlb_Ok.Visible = false;
                        tlb_Save.Visible = false;
                        tlb_Notes.Visible = false;
                    }
                    else
                    { 
                    
                        tlb_EditNotes.Visible = true;
                        tlb_Delete.Visible = true;
                        tlb_Ok.Visible = true;
                        tlb_Save.Visible = true;
                        tlb_Notes.Visible = true;
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private bool saveNotes()
        {

            bool result = true;

            if (panel_NoteDtl.Visible == true)
            {
                //validation for text box
                if (txtNotes.Text != null && Convert.ToString(txtNotes.Text).Trim() != "")
                {
                    try
                    {
                        oNote = new global::gloBilling.Common.GeneralNote();
                        oNote.TransactionID = _MasterTransactionID;
                        oNote.TransactionLineId = _TransactionLineNo;
                        oNote.TransactionDetailID = _TransactionDetailID;
                        oNote.NoteType = NoteType.Claim_Note;
                        oNote.NoteID = _C1NoteID;
                        oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text);
                        oNote.UserID = _UserID;
                        oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                        oNote.NoteDescription = txtNotes.Text.Trim();
                        oNote.ClinicID = _ClinicID;
                        if (_C1NoteID != 0)
                        {
                            oNote.dtCreatedDatetime = _dtNoteCreatedDatetime;
                        }
                        oNotes = new global::gloBilling.Common.GeneralNotes();
                        oNotes.Add(oNote);
                        
                        if (gloCharges.SaveClaimNotes(oNotes))
                        {
                            if (_C1NoteID == 0)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Claim Note Added for claim # " + Convert.ToString(_nClaimNo), 0, _MasterTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            else
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Claim Note Modified for claim # " + Convert.ToString(_nClaimNo), 0, _MasterTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                        }
                        chkSave_Flag = true;
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        ex = null;
                        chkSave_Flag = false;
                    }
                    finally
                    {
                        oNotes.Clear();
                        oNotes.Dispose();
                    }
                  
                    result = true;
                }
                else
                {
                    MessageBox.Show("Please enter the note.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Focus();
                    txtNotes.Select();
                    result = false;
                }
            }
            return result;

        }

        #endregion

        #region " Tool Strip Events "

        private void tlb_Delete_Click(object sender, EventArgs e)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strSQL = "";
            try
            {
                if (C1NotesGrid != null && C1NotesGrid.Rows.Count > 1)
                {

                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_TRANSACTIONID].Index) != null
                                          && Convert.ToString(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_TRANSACTIONID].Index)) != "")
                    {
                        DialogResult res = MessageBox.Show("Do you want to delete selected note? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            _C1SelectedRow = 0;
                            Int64 HistoryNoteID = gloCharges.InsertNoteBeforeDelete(Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID)), "Claim_Notes", "BL_Transaction_Lines_Notes");
                            strSQL = "DELETE FROM BL_Transaction_Lines_Notes WHERE nNoteID = " + C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString();
                            oDB.Connect(false);
                            oDB.Execute_Query(strSQL);
                            oDB.Disconnect();
                            oDB.Dispose();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "Claim Note Deleted for Claim # "+Convert.ToString(_nClaimNo), 0, HistoryNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            tlb_Notes.Visible = true;
                            tlb_EditNotes.Visible = true;
                            tlb_Delete.Visible = true;
                            tlb_Close.Visible = true;
                            tlb_Save.Visible = false;
                            tlb_Ok.Visible = false;
                            tlb_Cancel.Visible = false;
                            panel_NoteDtl.Visible = false;
                            chkDelete_flag = true;
                            //break;
                        }
                        else if (res == DialogResult.No)
                        { //SLR : Please revisit why return is before assignment on 4/21/2014
                            chkDelete_flag = false;
                            return;
                        }
                        txtNotes.Tag = 0;
                        txtNotes.Text = "";
                        panel_NoteDtl.Visible = false;
                    }//
                }


                FillNotes();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void tlb_Close_Click(object sender, EventArgs e)
        {

            this.Close();
            if (chkSave_Flag == true || chkDelete_flag == true)
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            return;

        }


        //Save&Close Notes
        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (saveNotes())
            {
                oDialogResult = true;
                this.Close();

                if (chkSave_Flag == true || chkDelete_flag == true)
                    DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        //Save notes
        private void tlb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveNotes())
                {
                    FillNotes();
                    txtNotes.Text = "";
                    panel_NoteDtl.Visible = false;
                   
                }
                oDialogResult = true;
               
            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }


        }

        //Add notes 
        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            tlb_EditNotes.Visible = false; //added on 9
            tlb_Delete.Visible = false;
            tlb_Notes.Visible = false;
            tlb_Ok.Visible = true;
            tlb_Save.Visible = true;
            tlb_Close.Visible = false;
            tlb_Cancel.Visible = true;
            _C1NoteID = 0;
            _bIsAddNotesFlag = true;
                        
            panel_NoteDtl.Visible = true;
            panel_NoteDtl.Visible = true;
            txtNotes.Tag = 0;//added on 25
            txtNotes.Text = "";
            txtNotes.ReadOnly = false;
            txtNotes.Focus();
            txtNotes.Select();
        }

        //Modify Notes
        private void tlb_EditNotes_Click(object sender, EventArgs e)
        {

            try
            {
                tlb_EditNotes.Visible = false; //added on 9
                tlb_Delete.Visible = false;
                tlb_Notes.Visible = false;
                tlb_Ok.Visible = true;
                tlb_Save.Visible = true;
                tlb_Close.Visible = false;
                tlb_Cancel.Visible = true;
                
                _bIsAddNotesFlag = false;  // it is for flag false => Edit notes ... true => add notes

                if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                {

                    _C1SelectedRow = C1NotesGrid.RowSel;
                    panel_NoteDtl.Visible = true;

                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString().Trim().Length > 0 &&
                         C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONID).ToString().Trim().Length > 0 &&
                         C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString().Trim().Length > 0 
                       )
                    {
                        txtNotes.Text = C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString();
                       _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString());
                       _dtNoteCreatedDatetime = Convert.ToDateTime(Convert.ToDateTime(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DTCREATEDDATETIME)).ToString("MM/dd/yyyy hh:mm:ss tt"));
                    }

                    dtpNoteDate.Text = "";
                    dtpNoteDate.Text = (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE)).ToString();
                    dtpNoteDate.Value = Convert.ToDateTime(Convert.ToDateTime(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE)).ToString("MM/dd/yyyy"));
                    dtpNoteDate.Update();
                    txtNotes.ReadOnly = false;
                    txtNotes.Focus();
                    txtNotes.Select();
                  
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }


        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                
                txtNotes.Text = "";
                txtNotes.Tag = 0;
                panel_NoteDtl.Visible = false;
                tlb_Cancel.Visible = false;
                tlb_Close.Visible = true;
                
                if (C1NotesGrid.Rows.Count == 1)
                {
                    tlb_EditNotes.Visible = false; //added on 9
                    tlb_Delete.Visible = false;
                    tlb_Notes.Visible = false;
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;

                    tlb_Notes.Visible = true;
                }
                else
                {
                   
                    tlb_EditNotes.Visible = true; //added on 9
                    tlb_Delete.Visible = true;
                    tlb_Notes.Visible = true;
                    tlb_Ok.Visible = true;

                    tlb_Save.Visible = true;
                    tlb_Notes.Visible = true;
                }

            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        #endregion

        #region "Browse Quick Notes"
        private void btnBrowseNotes_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {
                ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.ClaimInternal.GetHashCode());
                ofrmQuickNotes.ShowDialog(this);
                if (txtNotes.Text != "")
                    txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                else
                    txtNotes.Text = ofrmQuickNotes.Note;

                const int MaxChars = 255;
                if (txtNotes.Text.Length > MaxChars)
                    txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);

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
    }
}