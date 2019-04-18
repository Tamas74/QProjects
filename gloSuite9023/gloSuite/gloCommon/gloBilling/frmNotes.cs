using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmNotes : Form
    {

        #region "Variable Declarations"

        private string _databaseConnection = "";
        private Int64 _TransactionID = 0;
        private Int64 _TransactionDetailID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _TransactionLineNo = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private bool _IsFormLoad = false;

        DataTable _dtERANotes = null;
        private gloERA.enumNoteType _ERANoteType = global::gloBilling.gloERA.enumNoteType.None;
        private Int64 _ERAReferenceID = 0;
        private Int64 _TempNoteID = 0;
        private bool _AddNote = false;
        private gloERA.gloERA oERA = null;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Common.GeneralNote oNote = new global::gloBilling.Common.GeneralNote();
        public Common.GeneralNotes oNotes = null;

        private string _messageBoxCaption = "";
        public bool oDialogResult = false;
        public bool _IsVoidNote = false;
        public bool _IsUpdated = false;
        private bool chkClose_Flag = false;
        private bool chkSave_Flag = false;
        private string _formName = "";
        public bool _IsVoidShowNote = false;

        public Int64 _nChildTransactionID = 0;
        public Int64 _nTransactionMasterDetailID = 0;

        //C1 values
        private Int64 _C1TranID = 0;
        private Int64 _C1TranDTLID = 0;
        private Int64 _C1NoteID = 0;
        private string _C1Notes = "";
        public Int32 _C1SelectedRow = 0;
        public bool _bIsAddNotesFlag = false;
        public string _C1NoteType = "";
        public Int32 _C1NoteRowID = 0; //23
        public EOBPaymentSubType _NoteTypeEnum = EOBPaymentSubType.Charges_BillingNote;
        public bool _IsChargeTypeNote = false;
        public bool _IsPaymentTypeNotes = false;

        private DataTable _dtUsers = null;
        //Payment Notes Object
        public Common.GeneralNote oPaymentNote = new global::gloBilling.Common.GeneralNote();
        public Common.GeneralNotes oPaymentNotes = null;


        #endregion

        #region Properties

        public bool IsVoidNote
        {
            get { return _IsVoidNote; }
            set
            {
                _IsVoidNote = value;
            }
        }

        public bool IsUpdated
        {
            get { return _IsUpdated; }
            set
            {
                _IsUpdated = value;
            }
        }
        public string FormName
        {
            get { return _formName; }
            set
            {
                _formName = value;
            }
        }


        public bool IsVoidShowNote
        {
            get { return _IsVoidShowNote; }
            set
            {
                _IsVoidShowNote = value;
            }
        }

        public Int64 nChildTransactionID // MasterTransactionId 
        {
            get { return _nChildTransactionID; }
            set
            {
                _nChildTransactionID = value;
            }
        }


        public Int64 nTransactionMasterDetailID // MasterTransactionDetailID 
        {
            get { return _nTransactionMasterDetailID; }
            set
            {
                _nTransactionMasterDetailID = value;
            }
        }


        //public Int64 VoidTrayID
        //{
        //    get { return _nVoidedTrayID; }
        //    set
        //    {
        //        _nVoidedTrayID = value;
        //    }
        //}

        //public Int64 VoidCloseDate
        //{
        //    get { return _nVoidCloseDate; }
        //    set
        //    {
        //        _nVoidCloseDate = value;
        //    }
        //}

        //public string VoidTrayName
        //{
        //    get { return _sVoidedTrayName; }
        //    set
        //    {
        //        _sVoidedTrayName = value;
        //    }
        //}

        //public string VoidTrayCode
        //{
        //    get { return _sVoidTrayCode; }
        //    set
        //    {
        //        _sVoidTrayCode = value;
        //    }
        //}
        public string DeletedNoteHistoryID { get; set; }
        #endregion

        #region " Column Constants "


        const int COL_NOTESSTYPE = 0;
        const int COL_DATE = 1;
        const int COL_NOTES = 2;
        const int COL_PMT = 3;
        const int COL_USER = 4;
        const int COL_ID = 5;
        const int COL_BILLINGTRANSACTIONDETAILID = 6;
        const int COL_REASONCODE = 7;
        const int COL_REASONDESCRIPTION = 8;
        const int COL_REASONAMOUNT = 9;
        const int COL_CLINICID = 10;
        const int COL_STATEMENTNOTES = 11;
        const int COL_ISSTATEMENTNOTESONPRINT = 12;
        const int COL_INTERNALNOTES = 13;
        const int COL_ISINTERNALNOTESONPRINT = 14;
        const int COL_CLOSEDATE = 15;
        const int COL_BILLINGNOTETYPE = 16;
        const int COL_TRANSACTIONLineNo = 17;
        const int COL_CLAIMNO = 18;
        const int COL_EOBPAYMENTID = 19;
        const int COL_EOBID = 20;
        const int COL_EOBPAYMENTDETAILID = 21;
        const int COL_BILLINGTRANSACTIONID = 22;
        const int COL_USERNAME = 23;
        const int COL_NOTEROWNO = 24; //23

        const int COL_COUNT = 25;

        #endregion " Column Constants "

        #region "Constructor"


        public frmNotes(string DatabaseConnectionString, Int64 ClinicID, Int64 TransactionID, Int64 TransactionDetailID, Int64 TransactionLineNo, Common.GeneralNotes Notes)
        {
            InitializeComponent();
           

            _TransactionID = TransactionID;
            _ClinicID = ClinicID;
            _TransactionLineNo = TransactionLineNo;
            _TransactionDetailID = TransactionDetailID;
            oNotes = Notes;
            _IsChargeTypeNote = true;
            //_NoteTypeEnum=?????????

            #region " Retrieve Global Settings "

            _UserID = gloGlobal.gloPMGlobal.UserID;
            _UserName = gloGlobal.gloPMGlobal.UserName;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            _databaseConnection = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
 
            #endregion
          

        }

        public frmNotes(string DatabaseConnectionString, Int64 ClinicID)
        {
            InitializeComponent();

            _ClinicID = ClinicID;

            //_NoteTypeEnum=?????????

             #region " Retrieve Global Settings "

            _UserID = gloGlobal.gloPMGlobal.UserID;
            _UserName = gloGlobal.gloPMGlobal.UserName;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            _databaseConnection = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;

            #endregion

          
        }

        public frmNotes(string DatabaseConnectionString, Int64 ClinicID, gloERA.enumNoteType enumERANoteType, Int64 nERAReferenceID, bool bAddNote)
        {
            InitializeComponent();
            _ERANoteType = enumERANoteType;
            _ERAReferenceID = nERAReferenceID;
            _AddNote = bAddNote;

            #region " Retrieve Global Settings "

            _UserID = gloGlobal.gloPMGlobal.UserID;
            _UserName = gloGlobal.gloPMGlobal.UserName;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            _databaseConnection = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;

            #endregion
        }

        #endregion

        #region "Form Load "

        private void frmNotes_Load(object sender, EventArgs e)
        {
            _dtUsers = gloCharges.GetUsers();
            _IsFormLoad = true;
            if (IsVoidShowNote)
            {
                gloC1FlexStyle.Style(C1NotesGrid, false);

                tlb_Notes.Visible = false;
                tlb_EditNotes.Visible = false;
                tlb_Delete.Visible = false;
                tlb_Ok.Visible = false;
                tlb_Save.Visible = false;
                if (_formName == "PatientFinancialView")
                    FillNotes_PatFinView();
                else
                    FillVoidNotes();
            }
            else
            {
                gloC1FlexStyle.Style(C1NotesGrid, false);
                if (_ERAReferenceID != 0)
                {
                    #region " ERA Notes "
                    this.Text = "Check Notes";
                    FillERANotes();
                    gbCharges.Visible = false;
                    label_NoteType.Visible = false;
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
                    rbCBillingNotes.Visible = false;
                    rbCInternalNotes.Visible = false;
                    rbCStatementNotes.Visible = false;
                    rbPInternalNotes.Visible = false;
                    rbPPaymentNotes.Visible = false;
                    rbPStatementNotes.Visible = false;
                    if (_AddNote)
                        tlb_Notes_Click(null, null);
                    #endregion
                }
                else
                {
                    FillNotes();
                    rbCInternalNotes.TabStop = true;
                }
            }

        }

        #endregion

        #region " C1 Grid Design Method "

        public void DesignGrid()
        {
            // gloC1FlexStyle.Style(C1NotesGrid, false);
            try
            {
                C1NotesGrid.Redraw = true;
                C1NotesGrid.ScrollBars = ScrollBars.Both;
                C1NotesGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                C1NotesGrid.Clear();
                C1NotesGrid.Cols.Count = COL_COUNT;
                C1NotesGrid.Rows.Count = 1;
                C1NotesGrid.Rows.Fixed = 1;
                C1NotesGrid.Cols.Fixed = 0;
                C1NotesGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Set Headers "

                C1NotesGrid.SetData(0, COL_NOTESSTYPE, "Type");
                C1NotesGrid.SetData(0, COL_DATE, "Date");
                C1NotesGrid.SetData(0, COL_NOTES, "Note");
                C1NotesGrid.SetData(0, COL_PMT, "PMT");
                C1NotesGrid.SetData(0, COL_USER, "User");
                //-----charges
                C1NotesGrid.SetData(0, COL_ID, "Id");
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Name = "BLTransactionDtlID";
                C1NotesGrid.Cols[COL_REASONCODE].Name = "Code";
                C1NotesGrid.Cols[COL_REASONDESCRIPTION].Name = "Description";
                C1NotesGrid.Cols[COL_REASONAMOUNT].Name = "Amount";
                C1NotesGrid.Cols[COL_CLINICID].Name = "ClinicId";
                C1NotesGrid.Cols[COL_STATEMENTNOTES].Name = "StatementNotes";
                C1NotesGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Name = "IsStatmentNoteOnPrint";
                C1NotesGrid.Cols[COL_INTERNALNOTES].Name = "InternalNotes";
                C1NotesGrid.Cols[COL_ISINTERNALNOTESONPRINT].Name = "IsInternalNoteOnPrint";
                C1NotesGrid.Cols[COL_CLOSEDATE].Name = "CloseDate";
                C1NotesGrid.Cols[COL_BILLINGNOTETYPE].Name = "BLNoteType";
                C1NotesGrid.Cols[COL_TRANSACTIONLineNo].Name = "LineNo";
                C1NotesGrid.Cols[COL_NOTEROWNO].Name = "RowID"; //23
                //----Payment
                C1NotesGrid.Cols[COL_CLAIMNO].Name = "ClaimNo";
                C1NotesGrid.Cols[COL_EOBPAYMENTID].Name = "EOBPaymentID";
                C1NotesGrid.Cols[COL_EOBID].Name = "EOBID";
                C1NotesGrid.Cols[COL_EOBPAYMENTDETAILID].Name = "EOBPayDtlID";
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Name = "BLTransactionID";
                C1NotesGrid.Cols[COL_USERNAME].Name = "UserName";


                #endregion " Set Headers "

                #region " Show/Hide "


                C1NotesGrid.Cols[COL_NOTESSTYPE].Visible = true;
                C1NotesGrid.Cols[COL_DATE].Visible = true;
                C1NotesGrid.Cols[COL_NOTES].Visible = true;
                C1NotesGrid.Cols[COL_PMT].Visible = false;
                C1NotesGrid.Cols[COL_USER].Visible = true;
                //-----charges
                C1NotesGrid.Cols[COL_ID].Visible = false;
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Visible = false;
                C1NotesGrid.Cols[COL_REASONCODE].Visible = false;
                C1NotesGrid.Cols[COL_REASONDESCRIPTION].Visible = false;
                C1NotesGrid.Cols[COL_REASONAMOUNT].Visible = false;
                C1NotesGrid.Cols[COL_CLINICID].Visible = false;
                C1NotesGrid.Cols[COL_STATEMENTNOTES].Visible = false;
                C1NotesGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Visible = false;
                C1NotesGrid.Cols[COL_INTERNALNOTES].Visible = false;
                C1NotesGrid.Cols[COL_ISINTERNALNOTESONPRINT].Visible = false;
                C1NotesGrid.Cols[COL_CLOSEDATE].Visible = false;
                C1NotesGrid.Cols[COL_BILLINGNOTETYPE].Visible = false;
                C1NotesGrid.Cols[COL_TRANSACTIONLineNo].Visible = false;
                C1NotesGrid.Cols[COL_NOTEROWNO].Visible = false; //23
                //----Payment
                C1NotesGrid.Cols[COL_CLAIMNO].Visible = false;
                C1NotesGrid.Cols[COL_EOBPAYMENTID].Visible = false;
                C1NotesGrid.Cols[COL_EOBID].Visible = false;
                C1NotesGrid.Cols[COL_EOBPAYMENTDETAILID].Visible = false;
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Visible = false;
                C1NotesGrid.Cols[COL_USERNAME].Visible = false;
                #endregion " Show/Hide "

                #region " Width "

                C1NotesGrid.Cols[COL_NOTESSTYPE].Width = 90;
                C1NotesGrid.Cols[COL_DATE].Width = 85;
                C1NotesGrid.Cols[COL_NOTES].Width = 560;
                C1NotesGrid.Cols[COL_PMT].Width = 0;
                C1NotesGrid.Cols[COL_USER].Width = 75;

                //-----charges
                C1NotesGrid.Cols[COL_ID].Width = 0;
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Width = 0;
                C1NotesGrid.Cols[COL_REASONCODE].Width = 0;
                C1NotesGrid.Cols[COL_REASONDESCRIPTION].Width = 0;
                C1NotesGrid.Cols[COL_REASONAMOUNT].Width = 0;
                C1NotesGrid.Cols[COL_CLINICID].Width = 0;
                C1NotesGrid.Cols[COL_STATEMENTNOTES].Width = 0;
                C1NotesGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Width = 0;
                C1NotesGrid.Cols[COL_INTERNALNOTES].Width = 0;
                C1NotesGrid.Cols[COL_ISINTERNALNOTESONPRINT].Width = 0;
                C1NotesGrid.Cols[COL_CLOSEDATE].Width = 0;
                C1NotesGrid.Cols[COL_BILLINGNOTETYPE].Width = 0;
                C1NotesGrid.Cols[COL_TRANSACTIONLineNo].Width = 0;
                C1NotesGrid.Cols[COL_NOTEROWNO].Width = 0; //23
                //----Payment
                C1NotesGrid.Cols[COL_CLAIMNO].Width = 0;
                C1NotesGrid.Cols[COL_EOBPAYMENTID].Width = 0;
                C1NotesGrid.Cols[COL_EOBID].Width = 0;
                C1NotesGrid.Cols[COL_EOBPAYMENTDETAILID].Width = 0;
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Width = 0;
                C1NotesGrid.Cols[COL_USERNAME].Width = 0;
                #endregion " Width "

                #region " DataType "

                C1NotesGrid.Cols[COL_NOTESSTYPE].DataType = typeof(System.String);
                //C1NotesGrid.Cols[COL_DATE].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_DATE].DataType = typeof(System.DateTime);
                C1NotesGrid.Cols[COL_DATE].Format = "MM/dd/yyyy";

                C1NotesGrid.Cols[COL_NOTES].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_PMT].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_USER].DataType = typeof(System.String);

                //-----charges
                C1NotesGrid.Cols[COL_ID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_REASONCODE].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_REASONDESCRIPTION].DataType = typeof(System.String);
                C1NotesGrid.Cols[COL_REASONAMOUNT].DataType = typeof(System.Decimal);
                C1NotesGrid.Cols[COL_CLINICID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_STATEMENTNOTES].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_INTERNALNOTES].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_ISINTERNALNOTESONPRINT].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_CLOSEDATE].DataType = typeof(System.DateTime);
                C1NotesGrid.Cols[COL_BILLINGNOTETYPE].DataType = typeof(System.Int16);
                C1NotesGrid.Cols[COL_TRANSACTIONLineNo].DataType = typeof(System.Int32);
                C1NotesGrid.Cols[COL_NOTEROWNO].DataType = typeof(System.Int32); //23
                //----Payment
                C1NotesGrid.Cols[COL_CLAIMNO].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_EOBID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_EOBPAYMENTDETAILID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].DataType = typeof(System.Int64);
                C1NotesGrid.Cols[COL_USERNAME].DataType = typeof(System.String);
                #endregion " DataType "

                #region " Styles "

                C1NotesGrid.ShowCellLabels = false;
                //C1NotesGrid.Styles.Normal.WordWrap = true ;
                //C1.Win.C1FlexGrid.CellStyle csEditableActionStatus = C1NotesGrid.Styles.Add("cs_ReasonCodes");
                //csEditableActionStatus.DataType = typeof(System.String);
                //csEditableActionStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csEditableActionStatus.BackColor = Color.White;


                C1NotesGrid.Cols[COL_NOTESSTYPE].AllowEditing = false;
                C1NotesGrid.Cols[COL_DATE].AllowEditing = false;
                C1NotesGrid.Cols[COL_NOTES].AllowEditing = false;
                C1NotesGrid.Cols[COL_PMT].AllowEditing = false;
                C1NotesGrid.Cols[COL_USER].AllowEditing = false;


                #endregion " Styles "

                //C1NotesGrid.ScrollBars = ScrollBars.Vertical;

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
                            //C1.Win.C1FlexGrid.HitTestInfo hitInfo = C1NotesGrid.HitTest(e.X, e.Y);
                            _C1SelectedRow = C1NotesGrid.RowSel;

                            if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO).ToString().Trim().Length > 0 &&
                                    C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE).ToString().Trim().Length > 0)
                            {

                                //if (hitInfo.Row > 0)
                                //if (_C1SelectedRow > 0)
                                //{
                                _C1TranID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_BILLINGTRANSACTIONID).ToString());
                                _C1TranDTLID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_BILLINGTRANSACTIONDETAILID).ToString());
                                _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_ID).ToString());
                                _C1Notes = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTES).ToString());

                                _C1NoteRowID = Convert.ToInt32(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTEROWNO).ToString());//23


                                if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                                {
                                    _C1Ntype = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTESSTYPE).ToString());
                                    selectNoteType(_C1Ntype);
                                }
                                //set notes textbox & notestype 
                                txtNotes.Text = _C1Notes;

                                //txtNotes.Enabled = false;
                                txtNotes.ReadOnly = true;
                                tlb_EditNotes.Enabled = true;
                                tlb_Delete.Enabled = true;
                                //}
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

            //gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void C1NotesGrid_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltip1.Hide(); 
        }
        
        

        private void C1NotesGrid_Click(object sender, EventArgs e)
        {
            string _C1Ntype = "";
            try
            {
                if (panel_NoteDtl.Visible == false)
                {

                    if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                    {
                        gbCharges.Enabled = false;
                        gbPayment.Enabled = false;
                    }


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

                            if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID).ToString().Trim().Length > 0 &&
                                C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID).ToString().Trim().Length > 0 &&
                                C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString().Trim().Length > 0 &&
                                C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString().Trim().Length > 0 &&
                                C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO).ToString().Trim().Length > 0 &&
                                C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE).ToString().Trim().Length > 0)
                            {
                                //if (hitInfo.Row > 0)
                                //if (_C1SelectedRow > 0)
                                //{
                                _C1TranID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_BILLINGTRANSACTIONID).ToString());
                                _C1TranDTLID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_BILLINGTRANSACTIONDETAILID).ToString());
                                _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(_C1SelectedRow, COL_ID).ToString());
                                _C1Notes = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTES).ToString());

                                _C1NoteRowID = Convert.ToInt32(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTEROWNO).ToString()); //

                                if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                                {
                                    _C1Ntype = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTESSTYPE).ToString());
                                    selectNoteType(_C1Ntype);
                                }
                                //set notes textbox & notestype 
                                txtNotes.Text = _C1Notes;

                                //txtNotes.Enabled = false;
                                txtNotes.ReadOnly = true;
                                tlb_EditNotes.Enabled = true;
                                tlb_Delete.Enabled = true;

                                //}
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
        }


        #endregion

        #region "Fill Methods"

        private void FillNotes()
        {
            DataTable dt;
            DesignGrid();
            int rowIndex = 0;
            string _sBillingCHPType = "";

            StringBuilder sbTransMstDtlID = null;
            try
            {

                #region " Fetch the Charge Notes form the Object and Bind it to the Grid "

                tlb_Close.Visible = true;
                tlb_Cancel.Visible = false;

                if (oNotes != null && oNotes.Count > 0)
                {

                    for (int i = oNotes.Count - 1; i >= 0; i--)
                    {
                        if (oNotes[i].BillingNoteType == EOBPaymentSubType.InternalNote || oNotes[i].BillingNoteType == EOBPaymentSubType.StatementNote || oNotes[i].BillingNoteType == EOBPaymentSubType.Other)
                        {
                            oNote = new global::gloBilling.Common.GeneralNote();
                            oNote.TransactionID = oNotes[i].TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                            oNote.TransactionLineId = oNotes[i].TransactionLineId;// Convert.ToInt64(dt.Rows[i]["nLineNo"]);
                            // txtNotes.Tag = _TransactionLineNo;
                            oNote.TransactionDetailID = oNotes[i].TransactionDetailID; //Convert.ToInt64(dt.Rows[i]["nTransactionDetailID"]);
                            oNote.NoteType = oNotes[i].NoteType; //NoteType.GeneralNote;
                            oNote.NoteID = oNotes[i].NoteID; //Convert.ToInt64(dt.Rows[i]["nNoteId"]);
                            //oNote.NoteDate = oNotes[i].NoteDate; //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
                            oNote.UserID = oNotes[i].UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
                            oNote.NoteDate = oNotes[i].StatementNoteDate;
                            //dtpNoteDate.Text = gloDateMaster.gloDate.DateAsDateString(oNotes[i].StatementNoteDate);

                            //Problem # - 140 : while creating new note it pull last date                           
                            //--------------------------------------------------------------------
                           // dtpNoteDate.Value = gloDateMaster.gloDate.DateAsDate(oNotes[i].StatementNoteDate);
                            //--------------------------------------------------------------------

                            //oNote.NoteRowID = oNotes[i].NoteRowID; //23
                            //oNote.StatementNoteDate = oNotes[i].StatementNoteDate;
                            oNote.NoteDescription = oNotes[i].NoteDescription; //Convert.ToString(dt.Rows[i]["sNoteDescription"]);
                            oNote.StatementNoteDate = oNotes[i].StatementNoteDate;
                            oNote.ClinicID = _ClinicID;
                            oNote.dtCreatedDatetime = oNotes[i].dtCreatedDatetime;

                            oNote.BillingNoteType = oNotes[i].BillingNoteType;

                            if (oPaymentNotes == null)
                            { oPaymentNotes = new global::gloBilling.Common.GeneralNotes(); }

                            oPaymentNotes.Add(oNote);

                            oNotes.RemoveAt(i);

                        }
                    }



                 
                    for (int i = oNotes.Count - 1; i >= 0; i--)
                    {
                        if (i >= 0)
                        {
                            C1NotesGrid.Rows.Add();
                            rowIndex = C1NotesGrid.Rows.Count - 1;
                            oNotes[i].NoteRowID = i;
                            switch (oNotes[i].BillingNoteType)
                            {

                                case EOBPaymentSubType.StatementNote:
                                    _sBillingCHPType = "Show on Statement";// "Statement";
                                    break;
                                case EOBPaymentSubType.InternalNote:
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.Charges_BillingNote:
                                    _sBillingCHPType = "Show on Claim";// "Billing";
                                    break;
                                case EOBPaymentSubType.Charges_StatementNote:
                                    _sBillingCHPType = "Show on Statement";// "Statement";
                                    break;
                                case EOBPaymentSubType.Charges_InternalNote:
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.Other:
                                    _sBillingCHPType = "Show on Claim";// "Payment";
                                    break;
                                case EOBPaymentSubType.None:
                                case EOBPaymentSubType.Insurace:
                                case EOBPaymentSubType.Copay:
                                case EOBPaymentSubType.Advance:
                                case EOBPaymentSubType.Coinsurace:
                                case EOBPaymentSubType.Dedcutiable:
                                case EOBPaymentSubType.WriteOff:
                                case EOBPaymentSubType.WithHold:
                                case EOBPaymentSubType.Patient:
                                case EOBPaymentSubType.Reserved:
                                case EOBPaymentSubType.TakeBack:
                                case EOBPaymentSubType.Adjuestment:
                                case EOBPaymentSubType.Correction:
                                case EOBPaymentSubType.Refund:
                                    break;
                                default:
                                    break;
                            }



                            C1NotesGrid.SetData(rowIndex, COL_EOBPAYMENTID, 0);
                            C1NotesGrid.SetData(rowIndex, COL_NOTESSTYPE, _sBillingCHPType);
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONID, oNotes[i].TransactionID.ToString());
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONDETAILID, oNotes[i].TransactionDetailID.ToString());
                            C1NotesGrid.SetData(rowIndex, COL_TRANSACTIONLineNo, oNotes[i].TransactionLineId.ToString());
                            C1NotesGrid.SetData(rowIndex, COL_CLINICID, oNotes[i].ClinicID.ToString());
                            C1NotesGrid.SetData(rowIndex, COL_ID, oNotes[i].NoteID.ToString());
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGNOTETYPE, oNotes[i].BillingNoteType.GetHashCode());
                            string _tempdate = "";
                            if ( Convert.ToString(oNotes[i].NoteDate) != "")
                            {
                                _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oNotes[i].NoteDate))).ToShortDateString();
                            }
                            if (Convert.ToString(oNotes[i].StatementNoteDate) != "")
                            {
                                _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oNotes[i].StatementNoteDate))).ToShortDateString();
                            }
                          
                            C1NotesGrid.SetData(rowIndex, COL_DATE, _tempdate);
                            if (_dtUsers != null && _dtUsers.Rows.Count > 0)
                            {
                                //_UserName = GetUserName(oNotes[i].UserID);
                                _UserName = Convert.ToString(_dtUsers.Select("nUserID =" + oNotes[i].UserID + "")[0]["sUserName"]);
                            }
                            C1NotesGrid.SetData(rowIndex, COL_USER, _UserName); ///here swap value id->username & name into username
                            C1NotesGrid.SetData(rowIndex, COL_USERNAME, oNotes[i].UserID);
                            C1NotesGrid.SetData(rowIndex, COL_NOTES, oNotes[i].NoteDescription.ToString());

                            C1NotesGrid.SetData(rowIndex, COL_NOTEROWNO, oNotes[i].NoteRowID);  //23

                            if (_IsChargeTypeNote == true)
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Charges");
                            }
                            else
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Payment");
                            }

                        }
                    }

                }
                #endregion

                #region "Fetch the Payment notes From the Dbase and Bind it to the Payment Object "

                // Payment Notes
                if (oPaymentNotes == null)
                {
                    dt = new DataTable();

                    sbTransMstDtlID = new StringBuilder();
                    sbTransMstDtlID.Append(nTransactionMasterDetailID);

                    dt = gloCharges.GetPaymentNotes(_nChildTransactionID, sbTransMstDtlID);

                    if (oPaymentNotes == null)
                    { oPaymentNotes = new global::gloBilling.Common.GeneralNotes(); }

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                oPaymentNote = new global::gloBilling.Common.GeneralNote();
                                oPaymentNote.TransactionID = Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                                oPaymentNote.TransactionLineId = Convert.ToInt64(dt.Rows[i]["nLineNo"]);
                                // txtNotes.Tag = _TransactionLineNo;
                                oPaymentNote.TransactionDetailID = Convert.ToInt64(dt.Rows[i]["nTransactionDetailID"]);
                                oPaymentNote.NoteType = NoteType.GeneralNote;
                                oPaymentNote.NoteID = Convert.ToInt64(dt.Rows[i]["nNoteId"]);
                                oPaymentNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dt.Rows[i]["nNoteDateTime"]).ToString());
                                oPaymentNote.UserID = Convert.ToInt64(dt.Rows[i]["nUserID"]);
                                //oPaymentNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(dt.Rows[i]["nDateTime"].ToString());
                                oPaymentNote.NoteDescription = Convert.ToString(dt.Rows[i]["sNoteDescription"]);
                                oPaymentNote.ClinicID = _ClinicID;

                                oPaymentNote.BillingNoteType = (EOBPaymentSubType)(dt.Rows[i]["nNoteType"]);

                                oPaymentNote.NoteRowID = i;

                                if (oPaymentNote != null)
                                {
                                    oPaymentNotes.Add(oPaymentNote);
                                }
                            }
                        }
                    }
                }

                #endregion

                #region "Binds the Payment Notes From the Payment Object to Grid "

                if (oPaymentNotes != null)
                {
                  
                    for (int i = oPaymentNotes.Count - 1; i >= 0; i--)
                    {

                        C1NotesGrid.Rows.Add();
                        rowIndex = C1NotesGrid.Rows.Count - 1;
                        oPaymentNotes[i].NoteRowID = i;
                        _sBillingCHPType = "";

                        switch (oPaymentNotes[i].BillingNoteType)     // ((EOBPaymentSubType)Convert.ToInt32(dt.Rows[i]["nNoteType"]))
                        {
                            case EOBPaymentSubType.Charges_BillingNote:  //17
                                _sBillingCHPType = "Show on Claim";  // "Billing";
                                break;
                            case EOBPaymentSubType.Charges_StatementNote:   //18
                                _sBillingCHPType = "Show on Statement";  //"Statement";
                                break;
                            case EOBPaymentSubType.Charges_InternalNote:  // 19
                                _sBillingCHPType = "Internal";
                                break;
                            case EOBPaymentSubType.Other:                 //10
                                _sBillingCHPType = "Show on Claim"; //"Payment";
                                break;
                            case EOBPaymentSubType.InternalNote:   //16
                                _sBillingCHPType = "Internal";
                                break;
                            case EOBPaymentSubType.StatementNote:  //15
                                _sBillingCHPType = "Show on Statement";  //"Statement";
                                break;

                        }




                        C1NotesGrid.SetData(rowIndex, COL_EOBPAYMENTID, 0);
                        C1NotesGrid.SetData(rowIndex, COL_NOTESSTYPE, _sBillingCHPType);
                        C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONID, oPaymentNotes[i].TransactionID.ToString());
                        C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONDETAILID, oPaymentNotes[i].TransactionDetailID.ToString());
                        C1NotesGrid.SetData(rowIndex, COL_TRANSACTIONLineNo, oPaymentNotes[i].TransactionLineId.ToString());
                        C1NotesGrid.SetData(rowIndex, COL_CLINICID, oPaymentNotes[i].ClinicID.ToString());
                        C1NotesGrid.SetData(rowIndex, COL_ID, oPaymentNotes[i].NoteID.ToString());
                        C1NotesGrid.SetData(rowIndex, COL_BILLINGNOTETYPE, oPaymentNotes[i].BillingNoteType.GetHashCode());
                        string _tempdate = "";
                     
                        if ( Convert.ToString(oPaymentNotes[i].NoteDate) != "")
                        {
                            _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oPaymentNotes[i].NoteDate))).ToString("MM/dd/yyyy");
                        }
                        if ( Convert.ToString(oPaymentNotes[i].StatementNoteDate) != "")
                        {
                            _tempdate = (gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oPaymentNotes[i].StatementNoteDate))).ToString("MM/dd/yyyy");
                        }
                        C1NotesGrid.SetData(rowIndex, COL_DATE, _tempdate);
                        if (_dtUsers != null && _dtUsers.Rows.Count > 0)
                        {
                            _UserName = Convert.ToString(_dtUsers.Select("nUserID =" + oPaymentNotes[i].UserID + "")[0]["sUserName"]);
                        }
                        C1NotesGrid.SetData(rowIndex, COL_USER, _UserName); ///here swap value id->username & name into username
                        C1NotesGrid.SetData(rowIndex, COL_USERNAME, oPaymentNotes[i].UserID);
                        C1NotesGrid.SetData(rowIndex, COL_NOTES, oPaymentNotes[i].NoteDescription.ToString());

                        C1NotesGrid.SetData(rowIndex, COL_NOTEROWNO, oPaymentNotes[i].NoteRowID);  //------------rowno 23

                        if (_IsChargeTypeNote == true)
                        {
                            C1NotesGrid.SetData(rowIndex, COL_PMT, "Charges");
                        }
                        else
                        {
                            C1NotesGrid.SetData(rowIndex, COL_PMT, "Payment");
                        }


                    }
                }
                #endregion

                //To Sort the Grid by Date
                C1NotesGrid.Sort(C1.Win.C1FlexGrid.SortFlags.Descending, COL_DATE);
                C1NotesGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
                C1NotesGrid.Cols[COL_DATE].AllowSorting = true;
                C1NotesGrid.Cols[COL_NOTESSTYPE].AllowSorting = false;
                C1NotesGrid.Cols[COL_NOTES].AllowSorting = false;
                C1NotesGrid.Cols[COL_USER].AllowSorting = false;


                //if (C1NotesGrid.Rows.Count == 1)
                //{
                //    tlb_EditNotes.Visible = false;
                //    tlb_Delete.Visible = false;
                //    tlb_Ok.Visible = false;
                //    tlb_Save.Visible = false;

                //    tlb_Notes.Visible = true;

                //}
                //else
                //{
                //    tlb_EditNotes.Visible = true;
                //    tlb_Delete.Visible = true;
                //    tlb_Ok.Visible = true;
                //    tlb_Save.Visible = true;
                //    tlb_Notes.Visible = true;
                //}

                if (panel_NoteDtl.Visible == false)
                {
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
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;

                    tlb_Notes.Visible = true;
                }
                else
                {
                    tlb_EditNotes.Visible = true;
                    tlb_Delete.Visible = true;
                    tlb_Ok.Visible = true;
                    tlb_Save.Visible = true;
                    tlb_Notes.Visible = true;
                }

                rbCInternalNotes.TabStop = true;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (sbTransMstDtlID != null)
                {
                    sbTransMstDtlID.Clear();
                }
            }
        }

        private void FillVoidNotes()
        {
            try
            {
                string _sBillingCHPType = "";
                DataTable dt;
                DesignGrid();

                dt = gloCharges.GetVoidNotes(_nChildTransactionID, _nTransactionMasterDetailID);
               
                if (dt != null)
                {
                   
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            _sBillingCHPType = "";

                            switch ((EOBPaymentSubType)Convert.ToInt32(dt.Rows[i]["nNoteType"]))
                            {
                                case EOBPaymentSubType.Charges_BillingNote:  //17
                                    _sBillingCHPType = "Show on Claim"; //"Billing";
                                    break;
                                case EOBPaymentSubType.Charges_StatementNote:   //18
                                    _sBillingCHPType = "Show on Statement";  //"Statement";
                                    break;
                                case EOBPaymentSubType.Charges_InternalNote:  // 19
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.Other:                 //10
                                    _sBillingCHPType = "Show on Claim";  //"Payment";
                                    break;
                                case EOBPaymentSubType.InternalNote:   //16
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.StatementNote:  //15
                                    _sBillingCHPType = "Show on Statement";  //"Statement";
                                    break;

                            }
                            if (_sBillingCHPType == "")
                            {
                                _sBillingCHPType = "Void";
                            }
                            C1NotesGrid.Rows.Add();
                            int rowIndex = C1NotesGrid.Rows.Count - 1;


                            C1NotesGrid.SetData(rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(dt.Rows[i]["nEOBPaymentID"]));
                            C1NotesGrid.SetData(rowIndex, COL_NOTESSTYPE, _sBillingCHPType);
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONID, Convert.ToInt64(dt.Rows[i]["nTransactionID"]));
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONDETAILID, Convert.ToInt64(dt.Rows[i]["nTransactionDetailID"]));
                            C1NotesGrid.SetData(rowIndex, COL_TRANSACTIONLineNo, Convert.ToInt64(dt.Rows[i]["nLineNo"]));
                            C1NotesGrid.SetData(rowIndex, COL_CLINICID, Convert.ToInt64(dt.Rows[i]["nClinicID"]));
                            C1NotesGrid.SetData(rowIndex, COL_ID, Convert.ToInt64(dt.Rows[i]["nNoteId"]));
                            EOBPaymentSubType _EPS = (EOBPaymentSubType)Convert.ToInt32(dt.Rows[i]["nNoteType"]);
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGNOTETYPE, _EPS.GetHashCode());
                            string _tempdate = "";
                            if (dt.Rows[i]["nNoteType"].ToString() == "0")
                            {
                                if (dt.Rows[i]["nNoteDateTime"] != null && Convert.ToString(dt.Rows[i]["nNoteDateTime"]) != "")
                                {
                                    _tempdate = Convert.ToDateTime(dt.Rows[i]["nNoteDateTime"]).ToString("MM/dd/yyyy");
                                }
                            }
                            else
                            {

                                if (dt.Rows[i]["nStatementNoteDate"] != null && Convert.ToString(dt.Rows[i]["nStatementNoteDate"]) != "")
                                {
                                    _tempdate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(dt.Rows[i]["nStatementNoteDate"]));
                                }
                            }
                            C1NotesGrid.SetData(rowIndex, COL_DATE, _tempdate);
                            C1NotesGrid.SetData(rowIndex, COL_USER, Convert.ToString(dt.Rows[i]["sUserName"]));
                            C1NotesGrid.SetData(rowIndex, COL_NOTES, Convert.ToString(dt.Rows[i]["sNoteDescription"]));
                            if (_IsChargeTypeNote == true)
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Charges");
                            }
                            else
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Payment");
                            }

                            _sBillingCHPType = "";
                        }
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
   
        private void FillNotes_PatFinView()
        {
            try
            {
                string _sBillingCHPType = "";
                DataTable dt;
                DesignGrid();

                dt = gloCharges.GetFiniancialViewNotes(_nChildTransactionID, _nTransactionMasterDetailID);
                if (dt != null)
                {
                    //Int64 rowIndex = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            _sBillingCHPType = "";

                            switch ((EOBPaymentSubType)Convert.ToInt32(dt.Rows[i]["nNoteType"]))
                            {
                                case EOBPaymentSubType.Charges_BillingNote:  //17
                                    _sBillingCHPType = "Show on Claim"; //"Billing";
                                    break;
                                case EOBPaymentSubType.Charges_StatementNote:   //18
                                    _sBillingCHPType = "Show on Statement";  //"Statement";
                                    break;
                                case EOBPaymentSubType.Charges_InternalNote:  // 19
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.Other:                 //10
                                    _sBillingCHPType = "Show on Claim";  //"Payment";
                                    break;
                                case EOBPaymentSubType.InternalNote:   //16
                                    _sBillingCHPType = "Internal";
                                    break;
                                case EOBPaymentSubType.StatementNote:  //15
                                    _sBillingCHPType = "Show on Statement";  //"Statement";
                                    break;

                            }
                            if (_sBillingCHPType == "")
                            {
                                _sBillingCHPType = "Void";
                            }
                            C1NotesGrid.Rows.Add();
                            int rowIndex = C1NotesGrid.Rows.Count - 1;


                            C1NotesGrid.SetData(rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(dt.Rows[i]["nEOBPaymentID"]));
                            C1NotesGrid.SetData(rowIndex, COL_NOTESSTYPE, _sBillingCHPType);
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONID, Convert.ToInt64(dt.Rows[i]["nTransactionID"]));
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGTRANSACTIONDETAILID, Convert.ToInt64(dt.Rows[i]["nTransactionDetailID"]));
                            C1NotesGrid.SetData(rowIndex, COL_TRANSACTIONLineNo, Convert.ToInt64(dt.Rows[i]["nLineNo"]));
                            C1NotesGrid.SetData(rowIndex, COL_CLINICID, Convert.ToInt64(dt.Rows[i]["nClinicID"]));
                            C1NotesGrid.SetData(rowIndex, COL_ID, Convert.ToInt64(dt.Rows[i]["nNoteId"]));
                            EOBPaymentSubType _EPS = (EOBPaymentSubType)Convert.ToInt32(dt.Rows[i]["nNoteType"]);
                            C1NotesGrid.SetData(rowIndex, COL_BILLINGNOTETYPE, _EPS.GetHashCode());
                            string _tempdate = "";
                            if (dt.Rows[i]["nNoteDateTime"] != null && Convert.ToString(dt.Rows[i]["nNoteDateTime"]) != "")
                            {
                                _tempdate = Convert.ToDateTime(dt.Rows[i]["nNoteDateTime"].ToString()).ToShortDateString();
                            }
                            C1NotesGrid.SetData(rowIndex, COL_DATE, _tempdate);
                            C1NotesGrid.SetData(rowIndex, COL_USER, Convert.ToString(dt.Rows[i]["sUserName"]));
                            C1NotesGrid.SetData(rowIndex, COL_NOTES, Convert.ToString(dt.Rows[i]["sNoteDescription"]));
                            if (_IsChargeTypeNote == true)
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Charges");
                            }
                            else
                            {
                                C1NotesGrid.SetData(rowIndex, COL_PMT, "Payment");
                            }

                            _sBillingCHPType = "";
                        }
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
                //validation for notetype selection
                if (_IsChargeTypeNote)
                {
                    if ((rbCBillingNotes.Checked == false) && (rbCStatementNotes.Checked == false) && (rbCInternalNotes.Checked == false))
                    {
                        MessageBox.Show("Please select note type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = false;
                        return result;
                    }
                }
                else
                {
                    if ((rbPPaymentNotes.Checked == false) && (rbPStatementNotes.Checked == false) && (rbPInternalNotes.Checked == false))
                    {
                        MessageBox.Show("Please select note type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = false;
                        return result;
                    }
                }

                //validation for text box
                if (txtNotes.Text != null && Convert.ToString(txtNotes.Text).Trim() != "")
                {
                    if (txtNotes.Tag != null && Convert.ToInt64(txtNotes.Tag) > 0)
                    {
                        if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                        {
                            if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE).ToString().Length > 0)
                            {
                                int _NT = 0;
                                if (int.TryParse(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE).ToString(), out _NT) == true)
                                {
                                    EOBPaymentSubType _NoteType = (EOBPaymentSubType)_NT;
                                    if (_NoteType == EOBPaymentSubType.Other || _NoteType == EOBPaymentSubType.InternalNote || _NoteType == EOBPaymentSubType.StatementNote)
                                    {
                                        if (oPaymentNotes != null && oPaymentNotes.Count > 0)
                                        {

                                            //for (int j = 0; j < oPaymentNotes.Count; j++)
                                            //{
                                            for (int j = oPaymentNotes.Count - 1; j >= 0; j--)
                                            {
                                                string _CurrentNoteDesc = oPaymentNotes[j].NoteDescription.ToString();
                                                DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oPaymentNotes[j].NoteDate);
                                                Int64 _CurrentNoteTranLineNo = oPaymentNotes[j].TransactionLineId;
                                                Int64 _CurrentNoteUserId = oPaymentNotes[j].UserID;
                                                Int64 _CurrentNoteTranDetailId = oPaymentNotes[j].TransactionDetailID;

                                                Int32 _rowId = oPaymentNotes[j].NoteRowID;

                                                if ((_CurrentNoteDesc == _C1Notes) && (oPaymentNotes[j].NoteDate == gloDateMaster.gloDate.DateAsNumber(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE).ToString()))
                                                    && (_CurrentNoteTranLineNo == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONLineNo))) && (_CurrentNoteTranDetailId == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID)))
                                                    && (_rowId == Convert.ToInt32(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO))))
                                                {
                                                    oPaymentNotes[j].NoteDescription = txtNotes.Text.ToString().Trim();
                                                    oPaymentNotes[j].UserID = _UserID;//Current login user 
                                                    oPaymentNotes[j].StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                                                    break;
                                                }
                                                //txtNotes.Tag = 0;
                                            }
                                            txtNotes.Tag = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (oNotes != null && oNotes.Count > 0)
                                        {

                                            for (int j = 0; j < oNotes.Count; j++)
                                            {
                                                string _CurrentNoteDesc = oNotes[j].NoteDescription.ToString();
                                                DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oNotes[j].NoteDate);
                                                Int64 _CurrentNoteTranLineNo = oNotes[j].TransactionLineId;
                                                Int64 _CurrentNoteUserId = oNotes[j].UserID;
                                                Int64 _CurrentNoteTranDetailId = oNotes[j].TransactionDetailID;
                                                Int32 _rowId = oNotes[j].NoteRowID;
                                                if ((_CurrentNoteDesc == _C1Notes) && (oNotes[j].StatementNoteDate == gloDateMaster.gloDate.DateAsNumber(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE).ToString()))
                                                    && (_CurrentNoteTranLineNo == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONLineNo))) && (_CurrentNoteTranDetailId == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID)))
                                                    && (_rowId == Convert.ToInt32(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO))))
                                                {
                                                    oNotes[j].NoteDescription = txtNotes.Text.ToString().Trim();
                                                    oNotes[j].UserID = _UserID; //Current login user 
                                                    oNotes[j].StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                                                    break;
                                                }
                                                //txtNotes.Tag = 0;
                                            }
                                            txtNotes.Tag = 0;
                                        }
                                    }
                                }

                                txtNotes.Text = "";
                                panel_NoteDtl.Visible = false;

                            }
                        }
                    }


                    else
                    {


                        oNote = new global::gloBilling.Common.GeneralNote();
                        oNote.TransactionID = _TransactionID;
                        oNote.TransactionLineId = _TransactionLineNo;

                        //txtNotes.Tag = _TransactionLineNo;

                        oNote.TransactionDetailID = _TransactionDetailID;
                        oNote.NoteType = NoteType.GeneralNote;
                        oNote.NoteID = 0;
                        oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                        oNote.UserID = _UserID;
                        oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                        oNote.dtCreatedDatetime = DateTime.Now;
                        

                        oNote.NoteDescription = txtNotes.Text.Trim();
                        oNote.ClinicID = _ClinicID;

                        if (rbCBillingNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.Charges_BillingNote;
                        }
                        else if (rbCStatementNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.Charges_StatementNote;//BillingNoteType.ChargesStatementNote;
                        }
                        else if (rbCInternalNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.Charges_InternalNote;//BillingNoteType.ChargesInternalNote;
                        }
                        else if (rbPPaymentNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.Other;// BillingNoteType.PaymentNote;
                        }
                        else if (rbPInternalNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.InternalNote;// BillingNoteType.PaymentInternalNote;
                        }
                        else if (rbPStatementNotes.Checked)
                        {
                            oNote.BillingNoteType = EOBPaymentSubType.StatementNote;// BillingNoteType.PaymentStatementNote;
                        }

                        if (oNotes != null)
                        {
                            oNotes.Add(oNote);
                        }
                        else
                        {
                            oNotes = new global::gloBilling.Common.GeneralNotes();
                            oNotes.Add(oNote);
                        }
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

        private bool SaveERANotes()
        {
            if (txtNotes.Text.Trim() == "")
            {
                MessageBox.Show("Enter check note.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            using (oERA = new global::gloBilling.gloERA.gloERA())
            {
                oERA.SaveERANote(_TempNoteID, global::gloBilling.gloERA.enumNoteType.Check, _ERAReferenceID, txtNotes.Text.ToString().Trim(), dtpNoteDate.Value);
                _TempNoteID = 0;
            }
            return true;
        }

        private void FillERANotes()
        {
            using (oERA = new global::gloBilling.gloERA.gloERA())
            {
                _dtERANotes = oERA.GetERANotes(global::gloBilling.gloERA.enumNoteType.Check, _ERAReferenceID);
                if (_dtERANotes != null)
                {
                    C1NotesGrid.DataSource = _dtERANotes;

                    C1NotesGrid.Cols.Fixed = 0;
                    // HEADER //
                    C1NotesGrid.Cols[1].Caption = "Date";
                    C1NotesGrid.Cols[2].Caption = "Note";
                    C1NotesGrid.Cols[4].Caption = "User";

                    // VISIBILITY //
                    C1NotesGrid.Cols[0].Visible = false;
                    C1NotesGrid.Cols[1].Visible = true;
                    C1NotesGrid.Cols[2].Visible = true;
                    C1NotesGrid.Cols[3].Visible = false;
                    C1NotesGrid.Cols[4].Visible = true;

                    // COLUMN WIDTH //
                    C1NotesGrid.Cols[1].Width = 100;
                    C1NotesGrid.Cols[2].Width = C1NotesGrid.Width - 200;
                    C1NotesGrid.Cols[4].Width = 100;
                    C1NotesGrid.Cols[1].DataType = typeof(System.DateTime);
                    C1NotesGrid.Cols[1].Format = "MM/dd/yyyy";

                    C1NotesGrid.AllowEditing = false;
                    C1NotesGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                }
            }
        }

        #endregion

        #region "Private Methods"

        private void selectNoteType(string sNoteType)
        {
            if (sNoteType == "Show on Claim")//"Billing")
            {
                if (_IsChargeTypeNote)
                {
                    rbCBillingNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.Charges_BillingNote;
                }
                
            }
            else if (sNoteType == "Show on Statement")//"Statement")
            {
                if (_IsChargeTypeNote)
                {
                    rbCStatementNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.Charges_StatementNote;
                }
               
            }
            else if (sNoteType == "Internal")
            {
                if (_IsChargeTypeNote)
                {
                    rbCInternalNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.Charges_InternalNote;
                }
               
            }
            else if (sNoteType == "Show on Claim")//"Payment")
            {
                if (_IsChargeTypeNote == false)
                {
                    rbPPaymentNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.Other;
                }
               
            }
            else if (sNoteType == "Internal")
            {
                if (_IsChargeTypeNote == false)
                {
                    rbPInternalNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.InternalNote;
                }
               
            }
            else if (sNoteType == "Show on Statement")//"Statement")
            {
                if (_IsChargeTypeNote == false)
                {
                    rbPStatementNotes.Checked = true;
                    _NoteTypeEnum = EOBPaymentSubType.StatementNote;
                }
               
            }
        }


        #endregion

        #region " Tool Strip Events "

        private void tlb_Delete_Click(object sender, EventArgs e)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strSQL = "";

            try
            {

                if (_ERAReferenceID != 0)
                {
                    if (C1NotesGrid != null && C1NotesGrid.Rows.Count > 1)
                    {
                        if (MessageBox.Show("Are you sure you want to delete selected note?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Int64 HistoryNoteID = gloCharges.InsertNoteBeforeDelete(Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, 0)), "ERA Notes", "ERA_NOTES");
                            strSQL = "DELETE FROM ERA_NOTES WHERE nNoteID = " + C1NotesGrid.GetData(C1NotesGrid.RowSel, 0).ToString();
                            oDB.Connect(false);
                            oDB.Execute_Query(strSQL);
                            oDB.Disconnect();
                            oDB.Dispose();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "ERA note deleted", 0, HistoryNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            FillERANotes();

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
                            rbCBillingNotes.Visible = false;
                            rbCInternalNotes.Visible = false;
                            rbCStatementNotes.Visible = false;
                            rbPInternalNotes.Visible = false;
                            rbPPaymentNotes.Visible = false;
                            rbPStatementNotes.Visible = false;
                        }
                    }
                    return;
                }

                if (C1NotesGrid != null && C1NotesGrid.Rows.Count > 1)
                {

                    if (_ERAReferenceID != 0)
                    {
                        Int64 HistoryNoteID = gloCharges.InsertNoteBeforeDelete(Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, 0)), "ERA Notes", "ERA_NOTES");
                        strSQL = "DELETE FROM ERA_NOTES WHERE nNoteID = " + C1NotesGrid.GetData(C1NotesGrid.RowSel, 0).ToString();
                        oDB.Connect(false);
                        oDB.Execute_Query(strSQL);
                        oDB.Disconnect();
                        oDB.Dispose();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "ERA note deleted", 0, HistoryNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        FillERANotes();

                        tlb_Notes.Visible = true;
                        tlb_EditNotes.Visible = true;
                        tlb_Delete.Visible = true;
                        tlb_Close.Visible = true;
                        tlb_Save.Visible = false;
                        tlb_Ok.Visible = false;
                        tlb_Cancel.Visible = false;
                        panel_NoteDtl.Visible = false;
                        rbCBillingNotes.Visible = false;
                        rbCInternalNotes.Visible = false;
                        rbCStatementNotes.Visible = false;
                        rbPInternalNotes.Visible = false;
                        rbPPaymentNotes.Visible = false;
                        rbPStatementNotes.Visible = false;  
                        return;
                    }

                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE).ToString().Length > 0)
                    {
                        int _NT = 0;
                        if (int.TryParse(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGNOTETYPE).ToString(), out _NT) == true)
                        {
                            EOBPaymentSubType _NoteType = (EOBPaymentSubType)_NT;
                            if (_NoteType == EOBPaymentSubType.Other || _NoteType == EOBPaymentSubType.InternalNote || _NoteType == EOBPaymentSubType.StatementNote)
                            {
                                MessageBox.Show("Cannot delete the  payment notes ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }//if
                            else
                            {

                                if (C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Index) != null
                                                      && Convert.ToString(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Index)) != "")
                                {

                                    Int64 nTransactionID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_BILLINGTRANSACTIONID].Index));
                                    Int64 nTransactionLineNo = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_TRANSACTIONLineNo].Index));
                                    Int64 nNoteID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_ID].Index));
                                    //bool IsSavedNote = Convert.ToBoolean(lvwNotes.SelectedItems[i].SubItems[6].Text);
                                  //  bool IsSavedNote = false;
                                    string sNoteDescription = Convert.ToString(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_NOTES].Index));
                                    DateTime dtNoteDate = Convert.ToDateTime(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_DATE].Index));
                                    Int64 nTransactionDetailID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Index));
                                    Int64 nUserId = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_USERNAME].Index));

                                    Int32 nNoteRowID = Convert.ToInt32(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_NOTEROWNO].Index)); //23


                                    //strSQL = "DELETE FROM BL_Transaction_Lines_Notes WHERE nTransactionID=" + nTransactionID + " AND nLineNo=" + nTransactionLineNo + " AND nTransactionDetailID = " + nTransactionDetailID + " AND nNoteId=" + nNoteID + " AND nClinicID=" + _ClinicID + "";
                                    //oDB.Connect(false);
                                    //oDB.Execute_Query(strSQL);
                                    //oDB.Disconnect();

                                    if (oNotes != null && oNotes.Count > 0)
                                    {
                                        for (int j = 0; j < oNotes.Count; j++)
                                        {
                                            string _CurrentNoteDesc = oNotes[j].NoteDescription.ToString();
                                            DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oNotes[j].StatementNoteDate);
                                            Int64 _CurrentNoteTranLineNo = oNotes[j].TransactionLineId;
                                            Int64 _CurrentNoteUserId = oNotes[j].UserID;
                                            Int64 _CurrentNoteTranDetailId = oNotes[j].TransactionDetailID;

                                            Int32 _rowId = oNotes[j].NoteRowID;

                                            if ((_CurrentNoteDesc.ToUpper() == sNoteDescription.ToUpper()) && (_CurrentNoteDate == dtNoteDate)
                                                && (_CurrentNoteTranLineNo == nTransactionLineNo) && (_CurrentNoteTranDetailId == nTransactionDetailID)
                                                && (_rowId == Convert.ToInt32(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTEROWNO))))
                                            {

                                                DialogResult res = MessageBox.Show("Do you want to delete selected note? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                                if (res == DialogResult.Yes)
                                                {
                                                    Int64 HistoryNoteID = gloCharges.InsertNoteBeforeDelete(Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, C1NotesGrid.Cols[COL_ID].Index)), "Charge Note", "BL_Transaction_Lines_Notes");
                                                    oNotes.RemoveAt(j); //remove record
                                                    _C1SelectedRow = 0;
                                                    if (HistoryNoteID!=0)
                                                    {
                                                         DeletedNoteHistoryID += "," + Convert.ToString(HistoryNoteID);
                                                    }
                                                    break;
                                                }
                                                else if (res == DialogResult.No)
                                                {
                                                    return;
                                                }
                                                //oNotes.RemoveAt(j);
                                            }
                                        }
                                    }
                                    txtNotes.Tag = 0;
                                    txtNotes.Text = "";
                                    panel_NoteDtl.Visible = false;
                                }//
                            }
                        }
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
        }

        //Save&Close Notes
        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmNotes_FormClosing);
            C1NotesGrid.RowColChange-=new EventHandler(C1NotesGrid_RowColChange);
            #region " ERA Notes "
            if (_ERAReferenceID != 0 && sender != null)
            {
                if (SaveERANotes())
                    this.Close();
                return;
            }
            #endregion

            if (saveNotes())
            {
                if (oPaymentNotes != null)
                {
                    //for (int i = 0; i < oPaymentNotes.Count; i++)
                    //{
                    for (int i = oPaymentNotes.Count - 1; i >= 0; i--)
                    {

                        oNote = new global::gloBilling.Common.GeneralNote();
                        oNote.TransactionID = oPaymentNotes[i].TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                        oNote.TransactionLineId = oPaymentNotes[i].TransactionLineId;// Convert.ToInt64(dt.Rows[i]["nLineNo"]);
                        // txtNotes.Tag = _TransactionLineNo;
                        oNote.TransactionDetailID = oPaymentNotes[i].TransactionDetailID; //Convert.ToInt64(dt.Rows[i]["nTransactionDetailID"]);
                        oNote.NoteType = oPaymentNotes[i].NoteType; //NoteType.GeneralNote;
                        oNote.NoteID = oPaymentNotes[i].NoteID; //Convert.ToInt64(dt.Rows[i]["nNoteId"]);
                        oNote.NoteDate = oPaymentNotes[i].NoteDate; //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
                        oNote.UserID = oPaymentNotes[i].UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
                        oNote.NoteDate = oPaymentNotes[i].StatementNoteDate;
                        oNote.StatementNoteDate = oPaymentNotes[i].StatementNoteDate;
                        oNote.NoteDescription = oPaymentNotes[i].NoteDescription; //Convert.ToString(dt.Rows[i]["sNoteDescription"]);
                        oNote.ClinicID = _ClinicID;
                        oNote.dtCreatedDatetime = DateTime.Now;

                        oNote.BillingNoteType = oPaymentNotes[i].BillingNoteType; // (EOBPaymentSubType)(dt.Rows[i]["nNoteType"]);


                        if (oNotes != null)
                        {
                            oNotes.Add(oNote);
                        }
                        else
                        {
                            oNotes = new global::gloBilling.Common.GeneralNotes();
                            oNotes.Add(oNote);
                        }
                    }
                    //for (int j = 0; j < oNotes.Count; j++)
                    //{
                    //    string _CurrentNoteDesc = oNotes[j].NoteDescription.ToString();
                    //    DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oNotes[j].NoteDate);
                    //    Int64 _CurrentNoteTranLineNo = oNotes[j].TransactionLineId;
                    //    Int64 _CurrentNoteUserId = oNotes[j].UserID;
                    //    Int64 _CurrentNoteTranDetailId = oNotes[j].TransactionDetailID;

                    //    if ((oNotes[j].BillingNoteType == EOBPaymentSubType.Other || oNotes[j].BillingNoteType == EOBPaymentSubType.InternalNote || oNotes[j].BillingNoteType == EOBPaymentSubType.StatementNote || (_CurrentNoteDesc != _C1Notes))
                    //        && (_CurrentNoteTranLineNo == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_TRANSACTIONLineNo))) && (_CurrentNoteTranDetailId == Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID))))
                    //    {
                    //        if (txtNotes.Text.ToString() != "")
                    //        {

                    //            oNotes[j].NoteDescription = txtNotes.Text.ToString();
                    //        }
                    //        break;
                    //    }
                    //}
                }
                rbCInternalNotes.TabStop = true;
                //rbCStatementNotes.TabStop = true;
                //rbCBillingNotes.TabStop = true;

                oDialogResult = true;

                //C1NotesGrid.Enabled = true;
                if (chkClose_Flag == false)
                    _IsUpdated = true;
                this.Close();

            }
            C1NotesGrid.RowColChange += new EventHandler(C1NotesGrid_RowColChange);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNotes_FormClosing);

        }

        //Save notes
        private void tlb_Save_Click(object sender, EventArgs e)
        {
            C1NotesGrid.RowColChange -= new EventHandler(C1NotesGrid_RowColChange);
            try
            {

                #region " ERA Notes Save "
                if (_ERAReferenceID != 0)
                {
                    if (SaveERANotes())
                    {
                        panel_NoteDtl.Visible = false;
                        FillERANotes();
                        tlb_Notes.Visible = true;
                        tlb_Delete.Visible = true;
                        tlb_Cancel.Visible = false;
                        tlb_Close.Visible = true;
                        tlb_Ok.Visible = false;
                        tlb_Save.Visible = false;
                        tlb_EditNotes.Visible = true;
                        rbCBillingNotes.Visible = false;
                        rbCInternalNotes.Visible = false;
                        rbCStatementNotes.Visible = false;
                        rbPInternalNotes.Visible = false;
                        rbPPaymentNotes.Visible = false;
                        rbPStatementNotes.Visible = false;

                        txtNotes.Text = "";
                        
                        label_BillingAleartMSG.Visible = false;
                    }
                    return;
                }
                #endregion

                if (saveNotes())
                {
                    panel_NoteDtl.Visible = false;
                    FillNotes();
                    txtNotes.Text = "";
                    
                    label_BillingAleartMSG.Visible = false;
                    // C1NotesGrid.Enabled = true;

                }
                rbCInternalNotes.TabStop = true;
                //rbCStatementNotes.TabStop = true;
                //rbCBillingNotes.TabStop = true;

                oDialogResult = true;
                _IsUpdated = true;
                chkSave_Flag = true;
            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            C1NotesGrid.RowColChange += new EventHandler(C1NotesGrid_RowColChange);

        }

        //Add notes 
        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            //tlb_EditNotes.Enabled = false;
            //tlb_Delete.Enabled = false;
            //tlb_Notes.Enabled = false;

            //tlb_Save.Enabled = true;
            //tlb_Ok.Enabled = true;

            //C1NotesGrid.Enabled = false;

            tlb_EditNotes.Visible = false; //added on 9
            tlb_Delete.Visible = false;
            tlb_Notes.Visible = false;
            tlb_Ok.Visible = true;
            tlb_Save.Visible = true;


            tlb_Close.Visible = false;
            tlb_Cancel.Visible = true;

            gbCharges.Enabled = true;
            gbPayment.Enabled = true;


            //Add new notes
            _bIsAddNotesFlag = true;

            //SetRedioBtnNoteType(_BillingNoteTypeEnum);
            panel_NoteDtl.Visible = true;
            label_BillingAleartMSG.Visible = false;

            // _NoteTypeEnum = EOBPaymentSubType.Charges_BillingNote; //23 while adding notes always billing notes check
            _NoteTypeEnum = EOBPaymentSubType.None;
            // label_BillingAleartMSG.Visible = false;
            if (_IsChargeTypeNote)
            {
                gbCharges.BringToFront();
            }
            else
            {
                gbPayment.BringToFront();
            }



            switch (_NoteTypeEnum)
            {
                case 0:
                    if (_IsChargeTypeNote)
                    {
                        gbCharges.BringToFront();
                        rbCBillingNotes.Checked = false;  //don't select any default note type while adding new note
                        rbCStatementNotes.Checked = false;
                        rbCInternalNotes.Checked = false;
                    }
                    else
                    {
                        gbPayment.BringToFront();
                        rbPPaymentNotes.Checked = false;  //don't select any default note type while adding new note
                        rbPStatementNotes.Checked = false;
                        rbPInternalNotes.Checked = false;
                    }
                    break;
                case EOBPaymentSubType.Charges_BillingNote:
                    rbCBillingNotes.Checked = true;
                    label_BillingAleartMSG.Visible = true;
                    txtNotes.MaxLength = 75;
                    break;
                case EOBPaymentSubType.Charges_StatementNote:
                    rbCStatementNotes.Checked = true;
                    break;
                case EOBPaymentSubType.Charges_InternalNote:
                    rbCInternalNotes.Checked = true;
                    break;
                case EOBPaymentSubType.Other:
                    rbPPaymentNotes.Checked = true;
                    break;
                case EOBPaymentSubType.StatementNote:
                    rbPStatementNotes.Checked = true;
                    break;
                case EOBPaymentSubType.InternalNote:
                    rbPInternalNotes.Checked = true;
                    break;
            }

            panel_NoteDtl.Visible = true;
            //txtNotes.Enabled = true;
            txtNotes.Tag = 0;//added on 25
            txtNotes.Text = "";
            txtNotes.ReadOnly = false;

            //Problem # - 140 : while creating new note it pull last date 
            // so set datetime picker to current date while creating new note.
            //----------------------------
            dtpNoteDate.Value = DateTime.Now;
            //----------------------------

            rbCInternalNotes.TabStop = true;
            //rbCStatementNotes.TabStop = true;
            //rbCBillingNotes.TabStop = true;


            txtNotes.Focus();
            txtNotes.Select();

            if (_ERAReferenceID != 0)
            {
                rbCBillingNotes.Visible = false;
                rbCInternalNotes.Visible = false;
                rbCStatementNotes.Visible = false;
                rbPInternalNotes.Visible = false;
                rbPPaymentNotes.Visible = false;
                rbPStatementNotes.Visible = false;  
            }

        }

        //Modify Notes
        private void tlb_EditNotes_Click(object sender, EventArgs e)
        {

            try
            {
                if (_ERAReferenceID != 0)
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
                        tlb_Cancel.Visible = true;
                        rbCBillingNotes.Visible = false;
                        rbCInternalNotes.Visible = false;
                        rbCStatementNotes.Visible = false;
                        rbPInternalNotes.Visible = false;
                        rbPPaymentNotes.Visible = false;
                        rbPStatementNotes.Visible = false;  

                        gbCharges.Enabled = false;
                        gbPayment.Enabled = false;
                        panel_NoteDtl.Visible = true;
                        _TempNoteID = Convert.ToInt64(C1NotesGrid.GetData(_RowSel, 0).ToString());
                        txtNotes.Text = C1NotesGrid.GetData(_RowSel, 2).ToString();
                        dtpNoteDate.Value = Convert.ToDateTime(C1NotesGrid.GetData(_RowSel, 1).ToString());
                    }
                    return;
                }
                //tlb_EditNotes.Enabled = false;
                //tlb_Notes.Enabled = false;
                //tlb_Delete.Enabled = false;

                //tlb_Save.Enabled = true;
                //tlb_Ok.Enabled = true;

                tlb_EditNotes.Visible = false; //added on 9
                tlb_Delete.Visible = false;
                tlb_Notes.Visible = false;
                tlb_Ok.Visible = true;
                tlb_Save.Visible = true;

                tlb_Close.Visible = false;
                tlb_Cancel.Visible = true;

                gbCharges.Enabled = false;
                gbPayment.Enabled = false;

                _bIsAddNotesFlag = false;  // it is for flag false => Edit notes ... true => add notes



                if ((C1NotesGrid.Rows.Count > 0) && (C1NotesGrid.RowSel > 0))
                {

                    _C1SelectedRow = C1NotesGrid.RowSel;

                    if (_IsChargeTypeNote)
                    {
                        gbCharges.BringToFront();

                    }
                    else
                    {
                        gbPayment.BringToFront();
                    }
                    switch (_NoteTypeEnum)
                    {

                        //case EOBPaymentSubType .Charges_BillingNote :
                        //    if (_IsChargeTypeNote)
                        //    {
                        //        gbCharges.BringToFront();
                        //        rbCBillingNotes.Checked = true;  //according to charges or payment form set ->rbPPaymentNotes.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        gbPayment.BringToFront();
                        //        rbPPaymentNotes.Checked = true;  //according to charges or payment form set ->rbPPaymentNotes.Checked = true;
                        //    }
                        //    break;
                        case EOBPaymentSubType.Charges_BillingNote:
                            rbCBillingNotes.Checked = true;
                            label_BillingAleartMSG.Visible = true;
                            txtNotes.MaxLength = 75;
                            break;
                        case EOBPaymentSubType.Charges_StatementNote:
                            rbCStatementNotes.Checked = true;
                            label_BillingAleartMSG.Visible = false;
                            txtNotes.MaxLength = 255;
                            break;
                        case EOBPaymentSubType.Charges_InternalNote:
                            rbCInternalNotes.Checked = true;
                            label_BillingAleartMSG.Visible = false;
                            txtNotes.MaxLength = 255;
                            break;
                        case EOBPaymentSubType.Other:
                            rbPPaymentNotes.Checked = true;
                            label_BillingAleartMSG.Visible = false;
                            txtNotes.MaxLength = 255;
                            break;
                        case EOBPaymentSubType.StatementNote:
                            rbPStatementNotes.Checked = true;
                            label_BillingAleartMSG.Visible = false;
                            txtNotes.MaxLength = 255;
                            break;
                        case EOBPaymentSubType.InternalNote:
                            rbPInternalNotes.Checked = true;
                            label_BillingAleartMSG.Visible = false;
                            txtNotes.MaxLength = 255;
                            break;
                    }

                    panel_NoteDtl.Visible = true;
                    string _C1Ntype = "";

                    if (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString().Trim().Length > 0 &&
                         C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID).ToString().Trim().Length > 0 &&
                         C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID).ToString().Trim().Length > 0 &&
                         C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString().Trim().Length > 0 &&
                        C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE) != null && C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTESSTYPE).ToString().Trim().Length > 0)
                    {
                        txtNotes.Text = C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString();
                        _C1Notes = C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_NOTES).ToString();

                        _C1TranID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONID).ToString());
                        _C1TranDTLID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_BILLINGTRANSACTIONDETAILID).ToString());
                        _C1NoteID = Convert.ToInt64(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_ID).ToString());
                        _C1Ntype = Convert.ToString(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTESSTYPE).ToString());

                        _C1NoteRowID = Convert.ToInt32(C1NotesGrid.GetData(_C1SelectedRow, COL_NOTEROWNO).ToString()); //23

                        selectNoteType(_C1Ntype);
                    }

                    //txtNotes.Text = _C1Notes;
                    //txtNotes.Enabled = true;
                    txtNotes.Tag = _TransactionLineNo;

                    dtpNoteDate.Text = "";
                    dtpNoteDate.Text = (C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE)).ToString();
                    dtpNoteDate.Value = Convert.ToDateTime(Convert.ToDateTime(C1NotesGrid.GetData(C1NotesGrid.RowSel, COL_DATE)).ToString("MM/dd/yyyy"));
                    dtpNoteDate.Update();
                    txtNotes.ReadOnly = false;
                    txtNotes.Focus();
                    txtNotes.Select();
                    //if (nTransactionID > 0 && nTransactionLineNo > 0)
                    //{
                    //    txtNotes.Text = _C1Notes;
                    //    txtNotes.Tag = _TransactionLineNo;
                    //    txtNotes.SelectAll();
                    //    tlb_Notes_Click(null, null);
                    //}


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
                if (_ERAReferenceID != 0)
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
                    rbCBillingNotes.Visible = false;
                    rbCInternalNotes.Visible = false;
                    rbCStatementNotes.Visible = false;
                    rbPInternalNotes.Visible = false;
                    rbPPaymentNotes.Visible = false;
                    rbPStatementNotes.Visible = false;  
                    _TempNoteID = 0;
                    return;
                }
                // C1NotesGrid.Enabled = true;

                txtNotes.Text = "";
                txtNotes.Tag = 0;
                label_BillingAleartMSG.Visible = false;
                panel_NoteDtl.Visible = false;
                tlb_Cancel.Visible = false;
                tlb_Close.Visible = true;

                //tlb_Notes.Enabled = true;
                //tlb_EditNotes.Enabled = true;
                //tlb_Delete.Enabled = true;

                //if (C1NotesGrid.Rows.Count == 1)
                //{
                //    //tlb_EditNotes.Enabled = false;
                //    //tlb_Delete.Enabled = false;
                //    //tlb_Ok.Enabled = false;
                //    //tlb_Save.Enabled = false;

                //    tlb_EditNotes.Visible = false; //added on 9
                //    tlb_Delete.Visible = false;
                //    tlb_Notes.Visible = false;
                //    tlb_Ok.Visible = false;
                //    tlb_Save.Visible = false;

                //    tlb_Notes.Visible = true;
                //    //tlb_Notes.Enabled = true;
                //}
                //else
                //{
                //    //tlb_EditNotes.Enabled = true;
                //    //tlb_Delete.Enabled = true;
                //    //tlb_Ok.Enabled = true;
                //    //tlb_Save.Enabled = true;

                //    tlb_EditNotes.Visible = true; //added on 9
                //    tlb_Delete.Visible = true;
                //    tlb_Notes.Visible = true;
                //    tlb_Ok.Visible = true;

                //    tlb_Save.Visible = true;
                //    tlb_Notes.Visible = true;
                //    //tlb_Notes.Enabled = true;
                //}

                if (panel_NoteDtl.Visible == false)
                {
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
                    tlb_Ok.Visible = false;
                    tlb_Save.Visible = false;

                    tlb_Notes.Visible = true;
                }
                else
                {
                    tlb_EditNotes.Visible = true;
                    tlb_Delete.Visible = true;
                    tlb_Ok.Visible = true;
                    tlb_Save.Visible = true;
                    tlb_Notes.Visible = true;
                }

                rbCInternalNotes.TabStop = true;
                //rbCStatementNotes.TabStop = true;
                //rbCBillingNotes.TabStop = true;

            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        #endregion

        #region "Radio Button Events "


        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtNotes.Text.ToString().Length > 75)
                //{
                //    MessageBox.Show("Billing notes can be only 75 character", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtNotes.Focus();
                //    txtNotes.Select();
                //}

            }
            catch (System.Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void rbCBillingNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCBillingNotes.Checked == true)
            {
                rbCBillingNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = true;
                txtNotes.MaxLength = 75;
                if (txtNotes.Text.Length > 75)
                    txtNotes.Text = txtNotes.Text.Substring(0, 75);
                btnBrowseNotes.Visible = false;
            }
            else
            {
                btnBrowseNotes.Visible = true;
                rbCBillingNotes.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbCStatementNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCStatementNotes.Checked == true)
            {
                rbCStatementNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = false;
                txtNotes.MaxLength = 255;
                btnBrowseNotes.Visible = true;
            }
            else
            {
                btnBrowseNotes.Visible = false;
                rbCStatementNotes.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbCInternalNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCInternalNotes.Checked == true)
            {
                rbCInternalNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = false;
                txtNotes.MaxLength = 255;
                btnBrowseNotes.Visible = true;
            }
            else
            {
                btnBrowseNotes.Visible = false;
                rbCInternalNotes.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbPPaymentNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPPaymentNotes.Checked == true)
            {
                rbPPaymentNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = false;
                txtNotes.MaxLength = 255;
            }
            else
            {
                rbPPaymentNotes.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbPStatementNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPStatementNotes.Checked == true)
            {
                rbPStatementNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = false;
                txtNotes.MaxLength = 255;
            }
            else
            {
                rbPStatementNotes.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbPInternalNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPInternalNotes.Checked == true)
            {
                rbPInternalNotes.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                label_BillingAleartMSG.Visible = false;
                txtNotes.MaxLength = 255;
            }
            else
            {
                rbPInternalNotes.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
        }


        #endregion

        #region "Browse Quick Notes"
        private void btnBrowseNotes_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {

                if (rbCStatementNotes.Checked == true)
                    ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.StatementCharge.GetHashCode());
                if (rbCInternalNotes.Checked == true)
                    ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.ClaimInternal.GetHashCode());

                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.ShowDialog(this);
                    if (txtNotes.Text != "")
                        txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                    else
                        txtNotes.Text = ofrmQuickNotes.Note;

                    const int MaxChars = 255;
                    if (txtNotes.Text.Length > MaxChars)
                        txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);
                }

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

        private void frmNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmNotes_FormClosing);
            if (_ERAReferenceID != 0)
            {
                this.Close();
                return;
            }

            if (chkSave_Flag == false)
                chkClose_Flag = true;
            else
                chkClose_Flag = false;
            //txtNotes.Text = "";
            //panel_NoteDtl.Visible = false;
            //oDialogResult = true;
            //this.Close();
            tlb_Ok_Click(null, null);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNotes_FormClosing);
        }
    }
}