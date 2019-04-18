using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;
using gloCommon;
using gloPatient.Classes;

namespace gloPatient
{
    public partial class frmAccountNotes : Form
    {

        #region "Variable Declarations"

        private string _databaseConnectionString = "";
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private bool _IsFormLoad = false;

      
        private string _messageBoxCaption = "";
        public bool oDialogResult = false;
        private bool chkSave_Flag = false;
        private bool chkDelete_flag = false;
        public bool _IsVoidShowNote = false;
        
        //C1 values
        private Int64 _C1NoteID = 0;
        private string _C1Notes = "";
        public Int32 _C1SelectedRow = 0;
        public bool _bIsAddNotesFlag = false;
        public Int32 _C1NoteRowID = 0;

//        private Boolean IsPatientAccountFeature = false;

        #endregion

        #region Properties
        
        public Int64 nPatientID{get;set;}
        
        public Int64 nPatientAccountID { get; set; }

        public Int64 nPAccountID { get; set; }

        public Int64 nHighlightNoteID { get; set; }

        #endregion
       
        #region "Constructor"

        public frmAccountNotes()
        {
            InitializeComponent();
            

            #region " Retrieve Global Settings "

            _UserID = gloPMGlobal.UserID;
            _UserName = gloPMGlobal.UserName;
            _messageBoxCaption = gloPMGlobal.MessageBoxCaption;
            _databaseConnectionString = gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloPMGlobal.ClinicID;
 
            #endregion
          
        }

        #endregion

        #region "Form Load "

        private void frmClaimNotes_Load(object sender, EventArgs e)
        {
            _IsFormLoad = true;
            gloC1FlexStyle.Style(C1NotesGrid, false);
            FillPatientAccountDetails();
            FillNotes();
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            txtNotes.Focus();
        }

        private void FillPatientAccountDetails()
        {
            try
            {

                DataTable dtAccDetails = gloAccount.GetAccountDetails(nPAccountID, nPatientID);
                lblAccountNo.Text = dtAccDetails.Rows[0]["sAccount"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccount"].ToString();
                lblAccountDesc.Text = dtAccDetails.Rows[0]["sAccountDesc"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccountDesc"].ToString();
                lblAccGuarantor.Text = dtAccDetails.Rows[0]["sGuarantorName"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sGuarantorName"].ToString();
                this.toolTip1.SetToolTip(lblAccountNo, dtAccDetails.Rows[0]["sAccount"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccount"].ToString());
                this.toolTip1.SetToolTip(lblAccountDesc, dtAccDetails.Rows[0]["sAccountDesc"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccountDesc"].ToString());
                this.toolTip1.SetToolTip(lblAccGuarantor, dtAccDetails.Rows[0]["sGuarantorName"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sGuarantorName"].ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }      

        #endregion

        #region " C1 Grid Design Method "

        public void DesignGrid()
        {
           
            try
            {

                C1NotesGrid.Styles.Normal.WordWrap = true;
                C1NotesGrid.Redraw = true;
                C1NotesGrid.ScrollBars = ScrollBars.Both;
                C1NotesGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                C1NotesGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Set Headers "

                C1NotesGrid.Cols[0].Caption = "";
                C1NotesGrid.Cols[1].Caption = "NoteID";
                C1NotesGrid.Cols[2].Caption = "NotesType";
                C1NotesGrid.Cols[3].Caption = "Date";
                C1NotesGrid.Cols[4].Caption = "Note";
                C1NotesGrid.Cols[5].Caption = "CreatedDateTime";
                C1NotesGrid.Cols[6].Caption = "AccountPatientID";
                C1NotesGrid.Cols[7].Caption = "PatientAccountID";
                C1NotesGrid.Cols[8].Caption = "PatientID";
                C1NotesGrid.Cols[9].Caption = "UserID";
                C1NotesGrid.Cols[10].Caption = "User";
                C1NotesGrid.Cols[11].Caption = "ClinicID";


                #endregion " Set Headers "

                #region " Show/Hide "

                C1NotesGrid.Cols[0].Visible = false;
                C1NotesGrid.Cols[1].Visible = false;
                C1NotesGrid.Cols[2].Visible = false;
                C1NotesGrid.Cols[3].Visible = true;
                C1NotesGrid.Cols[4].Visible = true; 
                C1NotesGrid.Cols[5].Visible = false;
                C1NotesGrid.Cols[6].Visible = false;
                C1NotesGrid.Cols[7].Visible = false;
                C1NotesGrid.Cols[8].Visible = false;
                C1NotesGrid.Cols[9].Visible = false;
                C1NotesGrid.Cols[10].Visible = true;
                C1NotesGrid.Cols[11].Visible = false;

                #endregion " Show/Hide "

                #region " Width "

                C1NotesGrid.Cols[3].Width = 80;
                C1NotesGrid.Cols[4].Width = 520;
                C1NotesGrid.Cols[10].Width = 100;

                #endregion " Width "

                #region " DataType "

                C1NotesGrid.Cols[3].DataType = typeof(System.DateTime);
                C1NotesGrid.Cols[3].Format = "MM/dd/yyyy";
               
                #endregion " DataType "

                #region " Styles "

                C1NotesGrid.ShowCellLabels = false;

                C1NotesGrid.Cols[0].AllowEditing = false;
                C1NotesGrid.Cols[1].AllowEditing = false;
                C1NotesGrid.Cols[2].AllowEditing = false;
                C1NotesGrid.Cols[3].AllowEditing = false;
                C1NotesGrid.Cols[4].AllowEditing = false;
                C1NotesGrid.Cols[5].AllowEditing = false;
                C1NotesGrid.Cols[6].AllowEditing = false;
                C1NotesGrid.Cols[7].AllowEditing = false;
                C1NotesGrid.Cols[8].AllowEditing = false;
                C1NotesGrid.Cols[9].AllowEditing = false;
                C1NotesGrid.Cols[10].AllowEditing = false;
                C1NotesGrid.Cols[11].AllowEditing = false;

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
            try
            {
                if (panel_NoteDtl.Visible == false)
                {
                        if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                        {
                            _C1SelectedRow = C1NotesGrid.RowSel;

                            if (C1NotesGrid.GetData(C1NotesGrid.RowSel, 4) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, 4).ToString().Trim().Length > 0)
                            {

                                _C1Notes = Convert.ToString(C1NotesGrid.GetData(C1NotesGrid.RowSel, 4));
                                txtNotes.Text = _C1Notes;
                                txtNotes.ReadOnly = true;
                                tlb_EditNotes.Enabled = true;
                                tlb_Delete.Enabled = true;
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
                tlb_Close.Visible = true;
                tlb_Cancel.Visible = false;
                dt = gloAccount.GetPatAccountNotes(nPatientID, nPatientAccountID, nPAccountID);
                this.C1NotesGrid.RowColChange -= new System.EventHandler(this.C1NotesGrid_RowColChange);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        C1NotesGrid.DataSource = dt.DefaultView;
                    }
                    else
                    {
                        C1NotesGrid.DataSource = dt;
                    }
                }
               

                DesignGrid();

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
                    tlb_EditNotes.Visible = true;
                    tlb_Delete.Visible = true;
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;
                    tlb_Notes.Visible = true;
                }

                if (nHighlightNoteID != 0)
                {
                    int i = C1NotesGrid.FindRow(Convert.ToString(nHighlightNoteID), 0, 1, true,true,false);
                    if (i > 0)
                    {
                        C1NotesGrid.Select(i, 1, true);
                    }
                }

                C1NotesGrid.AutoSizeRows();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
               
                this.C1NotesGrid.RowColChange += new System.EventHandler(this.C1NotesGrid_RowColChange);
            }
        }

        private bool saveNotes()
        {
            bool result = true;
            try
            {
                if (panel_NoteDtl.Visible == true)
                {
                    //validation for text box
                    if (txtNotes.Text != null && Convert.ToString(txtNotes.Text).Trim() != "")
                    {
                        gloAccount.SavePatAccNotes(_C1NoteID, gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text.Trim()), txtNotes.Text.Trim(), nPatientID, nPatientAccountID, nPAccountID);
                        chkSave_Flag = true;
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
                
            }
            catch (Exception)
            {

            }
            finally
            {
               
            }
            return result;

        }

        #endregion

        #region " Tool Strip Events "

        private void tlb_Delete_Click(object sender, EventArgs e)
        {

           
            string strSQL = "";
            try
            {
                if (C1NotesGrid != null && C1NotesGrid.Rows.Count > 1)
                {

                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, 1) != null
                                          && Convert.ToString(C1NotesGrid.GetData(C1NotesGrid.RowSel, 1)) != "")
                    {
                        DialogResult res = MessageBox.Show("Do you want to delete selected note? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            _C1SelectedRow = 0;
                            Int64 HistoryNoteID = InsertNoteBeforeDelete(Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, 1)), "Account Note", "PA_Accounts_Notes");
                            strSQL = "DELETE FROM PA_Accounts_Notes WHERE nID = " + C1NotesGrid.GetData(C1NotesGrid.RowSel, 1);
                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                            oDB.Connect(false);
                            oDB.Execute_Query(strSQL);
                            oDB.Disconnect();
                            oDB.Dispose();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "Patient account note deleted", 0, HistoryNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                            tlb_Notes.Visible = true;
                            tlb_EditNotes.Visible = true;
                            tlb_Delete.Visible = true;
                            tlb_Close.Visible = true;
                            tlb_Save.Visible = false;
                            tlb_Ok.Visible = false;
                            tlb_Cancel.Visible = false;
                            panel_NoteDtl.Visible = false;
                            chkDelete_flag = true;
                           
                        }
                        else if (res == DialogResult.No)
                        {
                            chkDelete_flag = false;
                            return;
                        }
                        txtNotes.Tag = 0;
                        txtNotes.Text = "";
                        panel_NoteDtl.Visible = false;
                    }
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
            tlb_EditNotes.Visible = false; 
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
            txtNotes.Tag = 0;
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
                tlb_EditNotes.Visible = false; 
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
                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, 1) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, 1).ToString().Trim().Length > 0 )
                    {
                        txtNotes.Text = C1NotesGrid.GetData(C1NotesGrid.RowSel, 4).ToString();
                        _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, 1).ToString());
                    }

                    dtpNoteDate.Text = "";
                    dtpNoteDate.Text = (C1NotesGrid.GetData(C1NotesGrid.RowSel, 3)).ToString();
                    dtpNoteDate.Value = Convert.ToDateTime(Convert.ToDateTime(C1NotesGrid.GetData(C1NotesGrid.RowSel, 3)).ToString("MM/dd/yyyy"));
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
                    tlb_EditNotes.Visible = false; 
                    tlb_Delete.Visible = false;
                    tlb_Notes.Visible = false;
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;

                    tlb_Notes.Visible = true;
                }
                else
                {
                   
                    tlb_EditNotes.Visible = true; 
                    tlb_Delete.Visible = true;
                    tlb_Notes.Visible = true;
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;
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
            frmQuickNotes ofrmQuickNotes=null;
            try
            {
                // as per enum QuickNoteType declared in gloBilling AccountInternal = 2
                ofrmQuickNotes = new frmQuickNotes(2);
                ofrmQuickNotes.ShowDialog(this);
                if (txtNotes.Text != "")
                    txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                else
                    txtNotes.Text = ofrmQuickNotes.Note;

                const int MaxChars = 255;
                if(txtNotes.Text.Length>MaxChars)
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
                    ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
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
                    ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion
        #region "Commented code - To check Acct. Feature Settings ON/OFF"

        //gloAccount objAccount = new gloAccount(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
        //objAccount.Dispose();

        //if (IsPatientAccountFeature)
        //{
        //FillPatientAccountDetails();
        //pnlPatDetails.Visible = true; ;
        //}
        //else
        //{
        //pnlPatDetails.Visible = false;
        //}

        #endregion

        public Int64 InsertNoteBeforeDelete(Int64 NoteID, string NoteType, string sourceTable)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object obj = new object();
            Int64 NoteHistoryID = 0;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nNoteHistoryID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nNoteID", NoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sNoteType", NoteType, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sNoteSourceTable", sourceTable, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_InsertNoteHistory", oDBParameters, out obj);

                if (obj != null)
                {
                    NoteHistoryID = Convert.ToInt64(obj);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return NoteHistoryID;

        }
    }
}