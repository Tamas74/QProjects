using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmCGNotes : Form
    {

        #region "Variable Declarations"

        private string _databaseConnection = "";
        private Int64 _nFileID = 0;
        private Int64 _nTransactionID = 0;                
        private Int64 _UserID = 0;
        private string _UserName = "";
        private string _sTransactionID = "";
        private string _sOriginalTransactionID = "";
        private bool _IsFormLoad = false;       
        DataTable _dtCGNotes = null;       
        private bool _AddNote = false;
        Int64 _TempNoteID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "";
        public bool oDialogResult = false;      
        public bool _IsUpdated = false;
        private bool chkClose_Flag = false;
        private bool chkSave_Flag = false;
        private string _formName = "";
        public Int64 nReturnNoteID = 0;
        public bool IsCallFromMarkPosted = false;
        private ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;

        #endregion

        #region " Column Constants "


        const int COL_NOTEID = 0;
        const int COL_DATE = 1;
        const int COL_NOTES = 2;
        const int COL_PMT = 3;
        const int COL_USER = 4;
        const int COL_RecordDate = 5;
        const int COL_COUNT = 6;

        #endregion " Column Constants "

        #region "Constructor"


        public frmCGNotes(Int64 nTransactionID,bool bAddNote)
        {
            InitializeComponent();


            this._nTransactionID = nTransactionID;
            #region " Retrieve Global Settings "
            _UserID = gloGlobal.gloPMGlobal.UserID;
            _UserName = gloGlobal.gloPMGlobal.UserName;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            _databaseConnection = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _AddNote = bAddNote;
            #endregion
          

        }
      
        #endregion

        #region "Form Load "

        private void frmCGNotes_Load(object sender, EventArgs e)
        {
           
            _IsFormLoad = true;
            
           
                gloC1FlexStyle.Style(C1NotesGrid, false);
                if (!IsCallFromMarkPosted)
                {
                    #region " CG Notes "
                    this.Text = "Payment Transaction Notes";
                    FillCGNotes();
                    tlb_Notes.Visible = true;
                    if (C1NotesGrid.Rows.Count == 1)
                    {
                        tlb_EditNotes.Visible = false;
                        tlb_Delete.Visible = false;
                        // tlb_Notes_Click(null, null);
                    }
                    else
                    {
                        tlb_EditNotes.Visible = true;
                        tlb_Delete.Visible = true;
                    }
                    tlb_Close.Visible = true;
                    tlb_Save.Visible = false;
                    tlb_Ok.Visible = false;
                    if (_AddNote)
                        tlb_Notes_Click(null, null);
                    #endregion
                }
                else
                {
                    #region " CG Notes "
                    this.Text = "Payment Transaction Notes";
                    FillCGNotes();
                    tlb_Save.Visible = false;
                    tlb_Cancel.Visible = false;
                        tlb_Notes_Click(null, null);
                    #endregion
                }
               
           
        }

        #endregion

        #region C1 Grid events"

      
        private void C1NotesGrid_MouseMove(object sender, MouseEventArgs e)
        {

            //gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void C1NotesGrid_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltip1.Hide(); 
        }
        
        

        #endregion

        #region "Fill Methods"

        private bool SaveCGNotes()
        {
            if (txtNotes.Text.Trim() == "")
            {
                MessageBox.Show("Enter note.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            {
               nReturnNoteID=oClsCleargagePaymentPosting.SaveCGNote(_TempNoteID,_nTransactionID, txtNotes.Text.ToString().Trim(), dtpNoteDate.Value);
                _TempNoteID = 0;
            }
            return true;
        }

        private void FillCGNotes()
        {
            oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            {
                _dtCGNotes = oClsCleargagePaymentPosting.GetCGNotes(_nTransactionID);
                if (_dtCGNotes != null)
                {
                    C1NotesGrid.DataSource = _dtCGNotes;

                    C1NotesGrid.Cols.Fixed = 0;
                    // HEADER //
                    C1NotesGrid.Cols[COL_DATE].Caption = "Date";
                    C1NotesGrid.Cols[COL_NOTES].Caption = "Note";
                    C1NotesGrid.Cols[COL_USER].Caption = "User";
                    C1NotesGrid.Cols[COL_RecordDate].Caption = "Record Date Time";

                    // VISIBILITY //
                    C1NotesGrid.Cols[COL_NOTEID].Visible = false;
                    C1NotesGrid.Cols[COL_DATE].Visible = true;
                    C1NotesGrid.Cols[COL_NOTES].Visible = true;
                    C1NotesGrid.Cols[COL_PMT].Visible = false;
                    C1NotesGrid.Cols[COL_USER].Visible = true;
                    C1NotesGrid.Cols[COL_RecordDate].Visible = true;

                    // COLUMN WIDTH //
                    C1NotesGrid.Cols[COL_DATE].Width = 100;
                    C1NotesGrid.Cols[COL_NOTES].Width = 420;
                    C1NotesGrid.Cols[COL_USER].Width = 100;
                    C1NotesGrid.Cols[COL_DATE].DataType = typeof(System.DateTime);
                    C1NotesGrid.Cols[COL_DATE].Format = "MM/dd/yyyy";
                    C1NotesGrid.Cols[COL_RecordDate].Width = 150;
                    C1NotesGrid.Cols[COL_RecordDate].DataType = typeof(System.DateTime);
                    C1NotesGrid.Cols[COL_RecordDate].Format = "MM/dd/yyyy hh:mm:ss tt";

                    C1NotesGrid.AllowEditing = false;
                    C1NotesGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                }
            }
            if (C1NotesGrid.Rows.Count == 1)
            {
                tlb_EditNotes.Visible = false;
                tlb_Delete.Visible = false;
              //  tlb_Notes_Click(null, null);
            }
            else
            {
                tlb_EditNotes.Visible = true;
                tlb_Delete.Visible = true;
            }
        }

        #endregion

        

        #region " Tool Strip Events "

        private void tlb_Delete_Click(object sender, EventArgs e)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strSQL = "";
            string sNote = "";
            try
            {

                if (_nTransactionID != 0)
                {
                    if (C1NotesGrid != null && C1NotesGrid.Rows.Count > 1)
                    {
                        if (MessageBox.Show("Are you sure you want to delete selected note?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            sNote = C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString();
                            strSQL = "DELETE FROM CG_Notes WHERE nNoteID = " + C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEID).ToString();
                            oDB.Connect(false);
                            oDB.Execute_Query(strSQL);
                            oDB.Disconnect();
                            oDB.Dispose();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "Payment Transaction note ("  + sNote +  ") deleted", 0, _nTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            FillCGNotes();

                            tlb_Notes.Visible = true;
                            if (C1NotesGrid.Rows.Count == 1)
                            {
                                tlb_EditNotes.Visible = false;
                                tlb_Delete.Visible = false;
                              //  tlb_Notes_Click(null, null);
                            }
                            else
                            {
                                tlb_EditNotes.Visible = true;
                                tlb_Delete.Visible = true;
                            }
                            tlb_Close.Visible = true;
                            tlb_Save.Visible = false;
                            tlb_Ok.Visible = false;                          
                            panel_NoteDtl.Visible = false;                           
                        }
                    }
                    return;
                }

                FillCGNotes();

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
        }

        //Save&Close Notes
        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmCGNotes_FormClosing);           
            #region " ERA Notes "
            if (_nTransactionID != 0 && sender != null)
            {
                if (SaveCGNotes())
                    this.Close();
                return;
            }
            #endregion
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCGNotes_FormClosing);

        }

        //Save notes
        private void tlb_Save_Click(object sender, EventArgs e)
        {
          
            try
            {

                #region " ERA Notes Save "
                if (_nTransactionID != 0)
                {
                    if (SaveCGNotes())
                    {
                        panel_NoteDtl.Visible = false;
                        FillCGNotes();
                        tlb_Notes.Visible = true;
                        tlb_Delete.Visible = true;                    
                        tlb_Close.Visible = true;
                        tlb_Ok.Visible = false;
                        tlb_Save.Visible = false;
                        tlb_EditNotes.Visible = true;  
                        txtNotes.Text = "";
                        label_BillingAleartMSG.Visible = false;
                        tlb_Cancel.Visible = false;
                    }
                   
                }
                #endregion

               
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
            
            tlb_EditNotes.Visible = false; 
            tlb_Delete.Visible = false;
            tlb_Notes.Visible = false;
            if (!IsCallFromMarkPosted)
            {
                tlb_Ok.Visible = true;
                tlb_Save.Visible = true;
                tlb_Cancel.Visible = true;
            }

            tlb_Close.Visible = false;           
            panel_NoteDtl.Visible = true;
            label_BillingAleartMSG.Visible = false;

            panel_NoteDtl.Visible = true;
            
            txtNotes.Tag = 0;
            txtNotes.Text = "";
            txtNotes.ReadOnly = false;
            
            dtpNoteDate.Value = DateTime.Now;          
          
            txtNotes.Focus();
            txtNotes.Select();

            

        }

        //Modify Notes
        private void tlb_EditNotes_Click(object sender, EventArgs e)
        {

            try
            {
                if (_nTransactionID != 0)
                {
                    Int32 _RowSel = C1NotesGrid.RowSel;
                    if (_RowSel >= 0)
                    {
                        tlb_EditNotes.Visible = false; //added on 9
                        tlb_Delete.Visible = false;
                        tlb_Notes.Visible = false;
                        tlb_Ok.Visible = true;
                        tlb_Save.Visible = true;
                        tlb_Close.Visible = false;                                            
                        panel_NoteDtl.Visible = true;
                        _TempNoteID = Convert.ToInt64(C1NotesGrid.GetData(_RowSel, COL_NOTEID).ToString());
                        txtNotes.Text = C1NotesGrid.GetData(_RowSel, COL_NOTES).ToString();
                        dtpNoteDate.Value = Convert.ToDateTime(C1NotesGrid.GetData(_RowSel, COL_DATE).ToString());
                    }
                   
                }
                tlb_Cancel.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }
        #endregion

        private void frmCGNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmCGNotes_FormClosing);
            if (_nTransactionID != 0)
            {
                this.Close();
                return;
            }

            if (chkSave_Flag == false)
                chkClose_Flag = true;
            else
                chkClose_Flag = false;           
            tlb_Ok_Click(null, null);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCGNotes_FormClosing);
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_nTransactionID != 0)
                {
                    tlb_Notes.Visible = true;
                    if (C1NotesGrid.Rows.Count == 1)
                    {
                        tlb_EditNotes.Visible = false;
                        tlb_Delete.Visible = false;
                    }
                    else
                    {
                        tlb_EditNotes.Visible = true;
                        tlb_Delete.Visible = true;
                    }
                    tlb_Close.Visible = true;
                    tlb_Save.Visible = false;
                    tlb_Ok.Visible = false;
                    tlb_Cancel.Visible = false;
                    panel_NoteDtl.Visible = false;                  
                    _TempNoteID = 0;
                    return;
                }
                

            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
    }
}
