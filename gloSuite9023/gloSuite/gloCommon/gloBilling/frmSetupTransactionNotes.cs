using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupTransactionNotes : Form
    {
        
        #region "Variable Declarations"

        private string _databaseConnection = "";
        private Int64 _TransactionID = 0;
        private Int64 _TransactionDetailID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _TransactionLineNo = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ncloseDate = 0;
        private bool IsPaymentVoid = false;  
 
        public Common.GeneralNote oNote = new global::gloBilling.Common.GeneralNote();
        public Common.GeneralNotes oNotes = null;
        public Common.Transaction oTransaction = null;

        private string _messageBoxCaption = "";
        public bool oDialogResult = false;
        public bool _IsVoidNote = false;

        public bool _IsVoidShowNote = false;

        private bool _ShowSelectTray = false;

        public Int64 _nChildTransactionID = 0;

        private Int64 _nVoidedTrayID = 0;
        private Int64 _nVoidCloseDate = 0;
        private Int64 _nPriorAuthID = 0;

        private string _sPriorAuthNo = "";
        private string _sVoidedTrayName = "";
        private string _sVoidTrayCode = "";
        private ToolTip _tlTip = new ToolTip();
        Label label;

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

        public bool IsVoidShowNote
        {
            get { return _IsVoidShowNote; }
            set
            {
                _IsVoidShowNote = value;
            }
        }

        public Int64 nChildTransactionID
        {
            get { return _nChildTransactionID; }
            set
            {
                _nChildTransactionID = value;
            }
        }

        public Int64 VoidTrayID
        {
            get { return _nVoidedTrayID; }
            set
            {
                _nVoidedTrayID = value;
            }
        }

        public Int64 VoidCloseDate
        {
            get { return _nVoidCloseDate; }
            set
            {
                _nVoidCloseDate = value;
            }
        }

        public string VoidTrayName
        {
            get { return _sVoidedTrayName; }
            set
            {
                _sVoidedTrayName = value;
            }
        }

        public string VoidTrayCode
        {
            get { return _sVoidTrayCode; }
            set
            {
                _sVoidTrayCode = value;
            }
        }

        public Boolean bIsReplacementClaimRequired;
       

        #endregion

        #region "Constructor"

        public frmSetupTransactionNotes(string DatabaseConnectionString, Int64 ClinicID, Int64 TransactionID, Int64 TransactionDetailID, Int64 TransactionLineNo, Common.GeneralNotes Notes, Common.Transaction Transaction, Int64 nPriorAuthID, string sPriorAuthNo)
        {
            InitializeComponent();
            _databaseConnection = DatabaseConnectionString;
            _TransactionID = TransactionID;
            _ClinicID = ClinicID;
            _TransactionLineNo = TransactionLineNo;
            _TransactionDetailID = TransactionDetailID;
            oNotes = Notes;
            oTransaction = Transaction; 
            _nPriorAuthID = nPriorAuthID;
            _sPriorAuthNo = sPriorAuthNo;

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

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
          
        }

        public frmSetupTransactionNotes(string DatabaseConnectionString, Int64 ClinicID, Int64 TransactionID, Int64 TransactionDetailID, Int64 TransactionLineNo, Common.GeneralNotes Notes)
        {
            InitializeComponent();
            _databaseConnection = DatabaseConnectionString;
            _TransactionID = TransactionID;
            _ClinicID = ClinicID;
            _TransactionLineNo = TransactionLineNo;
            _TransactionDetailID = TransactionDetailID;
            oNotes = Notes;
 
            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion


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

        }
        
        #endregion

        #region "Form Load "
        
        private void frmSetupTransactionNotes_Load(object sender, EventArgs e)
        {

            txtNotes.Text = "";
            txtNotes.Select();

            if (_IsVoidNote) // _IsVoidNote -- Specifies that the Notes is opened For Void Charges Not For Transaction Notes
            {
                this.Text = "Void Claim";
                tlb_Notes.Visible = false;
                tlb_EditNotes.Visible = false;
                tlb_History.Visible = false;
                tlb_Delete.Visible = false;
                tlb_Save.Visible = false;

                lblVoidNotes.Visible = true;
                lblVoidAllCharges.Visible = true;
                
                tlb_Cancel.Visible = true;
                lvwNotes.Visible = false;
                txtNotes.Visible = true;
                pnlVoidsCriteria.Visible = true;

                FillCloseDayTray();

                #region "Get Close Date"

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
                DataTable _dt=null;
               // DataTable _dtSplit;
                string strQuery="";
                oDB.Connect(false);

                strQuery = "SELECT  dbo.CONVERT_DateAsNumber(Credits.dtCloseDate) AS CloseDate , " +
                           "         ISNULL(Credits.bIsPaymentVoid, 0) AS IsPaymentVoid "+
                           " FROM    Credits "+
                           "         LEFT OUTER JOIN Debits ON Credits.nCreditID = Debits.nCreditID "+
                           " WHERE   Debits.nBillingTransactionID = " + _TransactionID  +
                           "         AND ISNULL(Credits.bIsPaymentVoid, 0) = 0 ";

                oDB.Retrive_Query(strQuery, out _dt);

                if (_dt == null || _dt.Rows.Count == 0)
                {
                    strQuery = "select distinct nTransactionDate as CloseDate,0 AS IsPaymentVoid from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID= " + _TransactionID + "";
                    oDB.Retrive_Query(strQuery, out _dt);
                }

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    //mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Rows[0]["CloseDate"])).ToString("MM/dd/yyyy");
                    mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Compute("MAX(CloseDate)", string.Empty))).ToString("MM/dd/yyyy");
                    //_ncloseDate = Convert.ToInt64(_dt.Rows[0]["CloseDate"]);
                    _ncloseDate = Convert.ToInt64(_dt.Compute("MAX(CloseDate)", string.Empty));
                    IsPaymentVoid = Convert.ToBoolean(_dt.Rows[0]["IsPaymentVoid"]);
                }

                if (IsPaymentVoid)
                {
                    strQuery = "select distinct nTransactionDate as CloseDate from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID= " + _TransactionID + "";
                    oDB.Retrive_Query(strQuery, out _dt);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Rows[0]["CloseDate"])).ToString("MM/dd/yyyy");
                        _ncloseDate = Convert.ToInt64(_dt.Rows[0]["CloseDate"]);
                    }
                }

                oDB.Disconnect();

                #region " Commented Code "

                //strQuery = " select nStatus from BL_Transaction_Claim_MST where nTransactionMasterID =" + _TransactionID + "  and nTransactionID= " + _nChildTransactionID + "";
                //oDB.Retrive_Query(strQuery, out _dtSplit);
                //if (_dtSplit != null || _dtSplit.Rows.Count > 0)
                //{
                //    if (Convert.ToInt64(_dtSplit.Rows[0]["nStatus"]) == 2)
                //    {
                //        strQuery = "select distinct nTransactionDate as CloseDate from BL_Transaction_Claim_MST where nTransactionMasterID =" + _TransactionID + "  and nTransactionID= " + _nChildTransactionID + "";
                //        oDB.Retrive_Query(strQuery, out _dt);
                //    }
                //    else
                //    {

                //}
                //}

                #endregion
                oDB.Dispose();
                oDB = null;
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                #endregion

            }
            else if (IsVoidShowNote) // To Load the Void Notes 
            {
                this.Text = "Void Notes";
                lvwNotes.Items.Clear();
                lvwNotes.Columns.Clear();

                lvwNotes.Columns.Add("TransactionID");
                lvwNotes.Columns.Add("TransactionLineID");
                lvwNotes.Columns.Add("NoteType");
                lvwNotes.Columns.Add("NoteID");
                lvwNotes.Columns.Add("Date");
                lvwNotes.Columns.Add("Notes");
                lvwNotes.Columns.Add("Saved");
                lvwNotes.Columns.Add("TransactionDetailID");

                lvwNotes.Columns[0].Width = 0;
                lvwNotes.Columns[1].Width = 0;
                lvwNotes.Columns[2].Width = 0;
                lvwNotes.Columns[3].Width = 0;
                lvwNotes.Columns[4].Width = 100;
                lvwNotes.Columns[5].Width = 350;
                lvwNotes.Columns[6].Width = 0;
                lvwNotes.Columns[7].Width = 0;

                lvwNotes.Visible = false;
                txtNotes.Visible = true;

                tlb_Notes.Visible = false;
                tlb_History.Visible = false;
                tlb_Delete.Visible = false;
                tlb_Save.Visible = false;
                tlb_Ok.Visible = false;
                txtNotes.Enabled = false;

                tlb_History_Click(null, null);
 
            }
            else
            {
                this.Text = "Charge Notes";
                lvwNotes.Items.Clear();
                lvwNotes.Columns.Clear();

                lvwNotes.Columns.Add("TransactionID");
                lvwNotes.Columns.Add("TransactionLineID");
                lvwNotes.Columns.Add("NoteType");
                lvwNotes.Columns.Add("NoteID");
                lvwNotes.Columns.Add("Date");
                lvwNotes.Columns.Add("Notes");
                lvwNotes.Columns.Add("Saved");
                lvwNotes.Columns.Add("TransactionDetailID");

                lvwNotes.Columns[0].Width = 0;
                lvwNotes.Columns[1].Width = 0;
                lvwNotes.Columns[2].Width = 0;
                lvwNotes.Columns[3].Width = 0;
                lvwNotes.Columns[4].Width = 100;
                lvwNotes.Columns[5].Width = 350;
                lvwNotes.Columns[6].Width = 0;
                lvwNotes.Columns[7].Width = 0;

                lvwNotes.Visible = false;
                txtNotes.Visible = true;


                tlb_History_Click(null, null);

                //tlb_Delete.Visible = false;
                //tlb_Notes.Visible = false;
                //tlb_History.Visible = true;

            }
        }

        private void frmSetupTransactionNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtNotes.Text.Length == 0 && txtNotes.Text.Trim()== "")
            {
                if (_IsVoidNote)
                {
                    //MessageBox.Show("Please enter the notes", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //e.Cancel = true;
                }
            }

        }

        #endregion

        #region "Fill Methods"

        private void GetNotes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt=null;
            try
            {
                dt = new DataTable();
                oDB.Connect(false);
                if (IsVoidShowNote)
                {
                    string _strQuery= " SELECT ISNULL(nNoteType,0) AS  nNoteType, ISNULL(nTransactionID,0) AS  nTransactionID,  "+
                                       " ISNULL(nTransactionDetailID,0) AS nTransactionDetailID,ISNULL(nLineNo,0) AS  nLineNo,  "+
                                       " ISNULL(nClinicID,0) AS  nClinicID, ISNULL(nNoteId,0) AS  nNoteId,  "+
                                       " ISNULL(nNoteDateTime,0) AS  nNoteDateTime, ISNULL(nUserID,0) AS  nUserID,  "+
                                       " ISNULL(sNoteDescription,'') AS  sNoteDescription  FROM BL_Transaction_Lines_Notes WITH(NOLOCK)  " +
                                       " WHERE   nTransactionID = " + nChildTransactionID + " AND nClinicID = " + _ClinicID + "  AND nNoteType =10 ";

                    oDB.Retrive_Query(_strQuery, out dt);
                }
                else
                {

                    oDBParameters.Clear();
                    oDBParameters.Add("@nTransactionID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nTransactionDetailID", _TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nLineNo", _TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dt);
                    oDB.Disconnect();
                }
                //First Clear list data if any
                if (lvwNotes.Items.Count > 0)
                {
                    lvwNotes.Items.Clear();
                }

                if (oNotes != null && oNotes.Count > 0)
                {
                    if (IsVoidShowNote)
                    {
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ListViewItem oItem = new ListViewItem();
                                    oItem.Text = dt.Rows[i]["nTransactionID"].ToString();
                                    oItem.SubItems.Add(dt.Rows[i]["nLineNo"].ToString());
                                    oItem.SubItems.Add(dt.Rows[i]["nNoteType"].ToString());
                                    oItem.SubItems.Add(dt.Rows[i]["nNoteId"].ToString());
                                    oItem.SubItems.Add(Convert.ToString(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nNoteDateTime"])).ToShortDateString()));
                                    oItem.SubItems.Add(dt.Rows[i]["sNoteDescription"].ToString());
                                    oItem.SubItems.Add(true.ToString()); //to identify existing saved notes
                                    oItem.SubItems.Add(Convert.ToString(dt.Rows[i]["nTransactionDetailID"]));
                                    lvwNotes.Items.Add(oItem);
                                    oItem = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < oNotes.Count; i++)
                        {
                            ListViewItem oItem = new ListViewItem();
                            oItem.Text = oNotes[i].TransactionID.ToString();
                            oItem.SubItems.Add(oNotes[i].TransactionLineId.ToString());
                            oItem.SubItems.Add(oNotes[i].NoteType.ToString());
                            oItem.SubItems.Add(oNotes[i].NoteID.ToString());
                            DateTime _dtNotes = DateTime.Now;
                            if (Convert.ToString(oNotes[i].NoteDate) != "")
                            {
                                _dtNotes = gloDateMaster.gloDate.DateAsDate(oNotes[i].NoteDate);
                            }
                            string _strDate = _dtNotes.ToShortDateString();
                            oItem.SubItems.Add(Convert.ToString(_strDate));
                            oItem.SubItems.Add(oNotes[i].NoteDescription.ToString());
                            oItem.SubItems.Add(false.ToString());
                            oItem.SubItems.Add(oNotes[i].TransactionDetailID.ToString());

                            lvwNotes.Items.Add(oItem);
                            oItem = null;
                        }
                    }

                }
                else if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ListViewItem oItem = new ListViewItem();
                            oItem.Text = dt.Rows[i]["nTransactionID"].ToString();
                            oItem.SubItems.Add(dt.Rows[i]["nLineNo"].ToString());
                            oItem.SubItems.Add(dt.Rows[i]["nNoteType"].ToString());
                            oItem.SubItems.Add(dt.Rows[i]["nNoteId"].ToString());
                            oItem.SubItems.Add(Convert.ToString(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["nNoteDateTime"])).ToShortDateString()));
                            oItem.SubItems.Add(dt.Rows[i]["sNoteDescription"].ToString());
                            oItem.SubItems.Add(true.ToString()); //to identify existing saved notes
                            oItem.SubItems.Add(Convert.ToString(dt.Rows[i]["nTransactionDetailID"]));
                            lvwNotes.Items.Add(oItem);
                            oItem = null;
                        }
                    }
                }


                if (lvwNotes.Columns.Count > 6)
                {
                    lvwNotes.Columns[0].Width = 0;
                    lvwNotes.Columns[1].Width = 0;
                    lvwNotes.Columns[2].Width = 0;
                    lvwNotes.Columns[3].Width = 0;
                    lvwNotes.Columns[4].Width = 100;
                    lvwNotes.Columns[5].Width = 350;
                    lvwNotes.Columns[6].Width = 0;
                    lvwNotes.Columns[7].Width = 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Clear();
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }

        #endregion

        #region " Tool Strip Events "

        private void tlb_History_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsVoidShowNote)
                {
                    lvwNotes.Visible = true;
                    lvwNotes.BringToFront();
                }
                else
                {
                    lvwNotes.Visible = true;
                    lvwNotes.BringToFront();
                    txtNotes.Visible = false;
                    tlb_Delete.Visible = true;
                    tlb_Notes.Visible = true;
                    tlb_History.Visible = false;
                    //tlb_EditNotes.Visible = true;
                    //tlb_Ok.Enabled = false;

                    txtNotes.Text = "";
                }
                GetNotes();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            lvwNotes.Visible = false;
            txtNotes.Visible = true;
            if (_IsVoidNote)
            {
                tlb_EditNotes.Visible = false;
                tlb_History.Visible = false;
                tlb_Delete.Visible = false;
                tlb_Save.Visible = false;
                tlb_Cancel.Visible = false;
            }
            else
            {
                tlb_Delete.Visible = false;
                tlb_Notes.Visible = false;
                tlb_History.Visible = true;
                //tlb_EditNotes.Visible = false;
            }
            tlb_Ok.Enabled = true;
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = true;

                if (_IsVoidNote)
                {
                    //This Function is used to Validate the Void close date,Void Tray and Notes
                    isValid = ValidateVoidClaim(); //Returns true When no Pblms found
                }

                if (txtNotes.Text.Trim() != "")
                {

                    if (txtNotes.Tag != null && Convert.ToInt64(txtNotes.Tag) > 0)
                    {
                        if (oNotes != null && oNotes.Count > 0)
                        {
                            for (int noteIndex = 0; noteIndex < oNotes.Count; noteIndex++)
                            {
                                if (oNotes[noteIndex].TransactionLineId == Convert.ToInt64(txtNotes.Tag))
                                {
                                    oNotes[noteIndex].NoteDescription = txtNotes.Text.ToString();
                                    break;
                                }
                            }
                        }
                        txtNotes.Tag = 0;
                    }
                    else
                    {
                        if (_IsVoidNote)
                        {
                            if (isValid)
                            {
                               

                                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
                                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                                oDB.Connect(false);
                                oDBParameters.Clear();

                                oDBParameters.Add("@nTransactionID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLineNo", _TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTransactionDetailID", _TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nNoteType", NoteType.Void_Note.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@nNoteDateTime", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNoteDescription", txtNotes.Text, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nStatementNoteDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtCreatedDateTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                                oDB.Execute("BL_INUP_Transaction_Lines_Notes", oDBParameters);

                                oNote = new global::gloBilling.Common.GeneralNote();
                                oNote.NoteDescription = txtNotes.Text.Trim();

                                #region " Update Transaction Tables And Payment Tables With Voided Date and Tray "

                                string _sqlQuery = "";
                                Int64 _nVoidcloseDt = 0;
                                Int64 _nVoidTrayID = 0;
                               // DataTable _dtPayDtl = null;

                                _nVoidcloseDt = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString());
                                _nVoidTrayID = Convert.ToInt64(cmbCloseDayTray.SelectedValue.ToString());

                                VoidTrayID = _nVoidTrayID;
                                VoidTrayCode = "";
                                VoidTrayName = cmbCloseDayTray.Text.Trim();
                                VoidCloseDate = _nVoidcloseDt;

                                _sqlQuery = " UPDATE BL_Transaction_MST WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + " , nVoidUserID= " + _UserID + ",sVoidUserName= '" + _UserName + "' WHERE nTransactionID = " + _TransactionID + " ";
                                oDB.Execute_Query(_sqlQuery);

                                _sqlQuery = "";
                                _sqlQuery = " UPDATE BL_Transaction_Lines WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + "  WHERE nTransactionID = " + _TransactionID + " ";
                                oDB.Execute_Query(_sqlQuery);

                                _sqlQuery = "";
                                _sqlQuery = " UPDATE BL_Transaction_Claim_MST WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + ",nVoidUserID= " + _UserID + ",sVoidUserName= '" + _UserName + "' WHERE nTransactionMasterID = " + _TransactionID + " ";
                                oDB.Execute_Query(_sqlQuery);

                                _sqlQuery = "";
                                _sqlQuery = " UPDATE BL_Transaction_Claim_Lines WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + " WHERE nTransactionMasterID = " + _TransactionID + " ";
                                oDB.Execute_Query(_sqlQuery);



                                //_sqlQuery = "select distinct nEObPaymentId from BL_EOBPayment_DTL WITH (NOLOCK) where nbillingTransactionID= " + _TransactionID + " ";
                                //oDB.Retrive_Query(_sqlQuery, out _dtPayDtl);

                                //if (_dtPayDtl != null && _dtPayDtl.Rows.Count > 0)
                                //{
                                //    for (int i = 0; i < _dtPayDtl.Rows.Count; i++)
                                //    {
                                //        if (Convert.ToInt64(_dtPayDtl.Rows[i]["nEObPaymentId"]) > 0)
                                //        {

                                //            _sqlQuery = " UPDATE BL_EOBPayment_MST WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + " WHERE nEObPaymentId = " + Convert.ToInt64(_dtPayDtl.Rows[i]["nEObPaymentId"]) + " ";
                                //            oDB.Execute_Query(_sqlQuery);

                                //            _sqlQuery = " UPDATE BL_EOBPayment_DTL WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + " WHERE nbillingTransactionID = " + _TransactionID + " ";
                                //            oDB.Execute_Query(_sqlQuery);

                                //            _sqlQuery = " UPDATE BL_EOBPayment_EOB WITH(READPAST) SET nVoidCloseDate = " + _nVoidcloseDt + ",nVoidTrayID= " + _nVoidTrayID + " WHERE nbillingTransactionID = " + _TransactionID + " ";
                                //            oDB.Execute_Query(_sqlQuery);

                                //            _sqlQuery = " UPDATE BL_EOB_NextAction WITH(READPAST) SET bIsVoid = 1  WHERE nbillingTransactionID = " + _TransactionID + " ";
                                //            oDB.Execute_Query(_sqlQuery);

                                //        }
                                //    }
                                //}

                                #endregion

                                oDB.Disconnect();
                                oDB.Dispose();
                                if (oDBParameters != null)
                                {
                                    oDBParameters.Clear();
                                    oDBParameters.Dispose();
                                    oDBParameters = null;
                                }           
                            }
                        }
                        else
                        {
                            oNote = new global::gloBilling.Common.GeneralNote();
                            oNote.TransactionID = _TransactionID;
                            oNote.TransactionLineId = _TransactionLineNo;
                            oNote.TransactionDetailID = _TransactionDetailID;
                            oNote.NoteType = NoteType.GeneralNote;
                            oNote.NoteID = 0;
                            oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                            oNote.UserID = _UserID;
                            oNote.NoteDescription = txtNotes.Text;
                            oNote.ClinicID = _ClinicID;

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
                    }
                    if (isValid)
                    {
                        oDialogResult = true;
                        if (chkNewClaim.Checked)
                            bIsReplacementClaimRequired = true;
                        else
                            bIsReplacementClaimRequired = false;
                        this.Close();
                    }
                    
                }
                else
                {
                    if (_IsVoidNote)
                    {
                        //MessageBox.Show("Please enter the notes", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oDialogResult = false;
                    }
                    else
                    {
                        oDialogResult = true;
                        if (chkNewClaim.Checked)
                            bIsReplacementClaimRequired = true;
                        else
                            bIsReplacementClaimRequired = false;
                        this.Close();
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oNote != null)
                { oNote.Dispose(); }
            }

        }

        private void tlb_Delete_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strSQL = "";

            try
            {
                if (lvwNotes.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lvwNotes.SelectedItems.Count; i++)
                    {
                        Int64 nTransactionID = Convert.ToInt64(lvwNotes.SelectedItems[i].Text);
                        Int64 nTransactionLineNo = Convert.ToInt64(lvwNotes.SelectedItems[i].SubItems[1].Text);
                        Int64 nNoteID = Convert.ToInt64(lvwNotes.SelectedItems[i].SubItems[3].Text);
                        bool IsSavedNote = Convert.ToBoolean(lvwNotes.SelectedItems[i].SubItems[6].Text);
                        string sNoteDescription = Convert.ToString(lvwNotes.SelectedItems[i].SubItems[5].Text);
                        DateTime dtNoteDate = Convert.ToDateTime(lvwNotes.SelectedItems[i].SubItems[4].Text);
                        Int64 nTransactionDetailID = Convert.ToInt64(lvwNotes.SelectedItems[i].SubItems[7].Text);
                        Int64 nUserId = Convert.ToInt64(lvwNotes.SelectedItems[i].Text);

                        strSQL = "DELETE FROM BL_Transaction_Lines_Notes WHERE nTransactionID=" + nTransactionID + " AND nLineNo=" + nTransactionLineNo + " AND nTransactionDetailID = " + nTransactionDetailID + " AND nNoteId=" + nNoteID + " AND nClinicID=" + _ClinicID + "";
                        oDB.Connect(false);
                        oDB.Execute_Query(strSQL);
                        oDB.Disconnect();

                        if (oNotes != null && oNotes.Count > 0)
                        { //SLR: Changed on 4/2/2014
                            for (int j = oNotes.Count - 1; j >= 0; j--)
                            {
                                string _CurrentNoteDesc = oNotes[j].NoteDescription.ToString();
                                DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oNotes[j].NoteDate);
                                Int64 _CurrentNoteTranLineNo = oNotes[j].TransactionLineId;
                                Int64 _CurrentNoteUserId = oNotes[j].UserID;
                                Int64 _CurrentNoteTranDetailId = oNotes[j].TransactionDetailID;

                                if ((_CurrentNoteDesc.ToUpper() == sNoteDescription.ToUpper()) && (_CurrentNoteDate == dtNoteDate)
                                    && (_CurrentNoteTranLineNo == nTransactionLineNo) && (_CurrentNoteTranDetailId == nTransactionDetailID))
                                {
                                    oNotes.RemoveAt(j);
                                }
                            }
                        }

                        //if (IsSavedNote == true)
                        //{
                        //    //If the selected note is saved then delete the entry form database

                        //    strSQL = "DELETE FROM BL_Transaction_Lines_Notes WHERE nTransactionID=" + nTransactionID + " AND nLineNo=" + nTransactionLineNo + " AND nTransactionDetailID = "+ nTransactionDetailID +" AND nNoteId=" + nNoteID + " AND nClinicID=" + _ClinicID + "";
                        //    oDB.Connect(false);
                        //    oDB.Execute_Query(strSQL);
                        //    oDB.Disconnect();
                        //}
                        //else
                        //{ 
                        //    //If the selected note is in current Transaction Line remove that note object from 
                        //    //Transaction Line Notes collection

                        //    if (oNotes != null && oNotes.Count > 0) 
                        //    {
                        //        for (int j = 0; j < oNotes.Count; j++)
                        //        {
                        //            string _CurrentNoteDesc = oNotes[j].NoteDescription.ToString();
                        //            DateTime _CurrentNoteDate = gloDateMaster.gloDate.DateAsDate(oNotes[j].NoteDate);
                        //            Int64 _CurrentNoteTranLineNo = oNotes[j].TransactionLineId;
                        //            Int64 _CurrentNoteUserId = oNotes[j].UserID;
                        //            Int64 _CurrentNoteTranDetailId = oNotes[j].TransactionDetailID;

                        //            if ((_CurrentNoteDesc.ToUpper() == sNoteDescription.ToUpper()) && (_CurrentNoteDate == dtNoteDate)
                        //                && (_CurrentNoteTranLineNo == nTransactionLineNo) && (_CurrentNoteTranDetailId == nTransactionDetailID))
                        //            {
                        //                oNotes.RemoveAt(j);
                        //            }
                        //        } 
                        //    }
                        //}
                    }

                    GetNotes();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void tlb_EditNotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwNotes.SelectedItems != null && lvwNotes.SelectedItems.Count > 0)
                {
                    Int64 nTransactionID = Convert.ToInt64(lvwNotes.SelectedItems[0].Text);
                    Int64 nTransactionLineNo = Convert.ToInt64(lvwNotes.SelectedItems[0].SubItems[1].Text);
                    Int64 nNoteID = Convert.ToInt64(lvwNotes.SelectedItems[0].SubItems[3].Text);
                    bool IsSavedNote = Convert.ToBoolean(lvwNotes.SelectedItems[0].SubItems[6].Text);
                    string sNoteDescription = Convert.ToString(lvwNotes.SelectedItems[0].SubItems[5].Text);
                    DateTime dtNoteDate = Convert.ToDateTime(lvwNotes.SelectedItems[0].SubItems[4].Text);
                    Int64 nTransactionDetailID = Convert.ToInt64(lvwNotes.SelectedItems[0].SubItems[7].Text);
                    Int64 nUserId = Convert.ToInt64(lvwNotes.SelectedItems[0].Text);

                    if (nTransactionID > 0 && nTransactionLineNo > 0)
                    {
                        txtNotes.Text = sNoteDescription;
                        txtNotes.Tag = nTransactionLineNo;
                        txtNotes.SelectAll();
                        tlb_Notes_Click(null, null);
                    }

                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tlb_Save_Click(object sender, EventArgs e)
        {
            if (txtNotes.Text.Trim() != "")
            {

                if (txtNotes.Tag != null && Convert.ToInt64(txtNotes.Tag) > 0)
                {
                    if (oNotes != null && oNotes.Count > 0)
                    {
                        for (int noteIndex = 0; noteIndex < oNotes.Count; noteIndex++)
                        {
                            if (oNotes[noteIndex].TransactionLineId == Convert.ToInt64(txtNotes.Tag))
                            {
                                oNotes[noteIndex].NoteDescription = txtNotes.Text.ToString();
                                break;
                            }
                        }
                    }
                    txtNotes.Tag = 0;
                }
                else
                {
                    oNote = new global::gloBilling.Common.GeneralNote();
                    oNote.TransactionID = _TransactionID;
                    oNote.TransactionLineId = _TransactionLineNo;
                    oNote.TransactionDetailID = _TransactionDetailID;
                    oNote.NoteType = NoteType.GeneralNote;
                    oNote.NoteID = 0;
                    oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    oNote.UserID = _UserID;
                    oNote.NoteDescription = txtNotes.Text;
                    oNote.ClinicID = _ClinicID;

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
            }
            oDialogResult = true;
            tlb_History_Click(null, null);
        } 

        #endregion

        #region " Close Date Validation "
        
        private void mskCloseDate_Validating(object sender, CancelEventArgs e)
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");

            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                int _addDays = 0;
                _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                        else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                        {
                            MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Select();
                            mskCloseDate.Focus();
                            e.Cancel = true;
                        }
                        else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                        {
                            MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            e.Cancel = true;
                        }
                        else if (IsPaymentVoid)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Void Close Date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Void Close Date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Please enter a close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Please enter a valid close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }


        }

        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException e)
            {
                Success = false; // If this line is reached, an exception was thrown
                e.ToString();
                e = null;
            }
            return Success;
        }

        private bool ValidateVoidClaim()
        {
            mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskCloseDate.Text;
            mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            bool Success = false; ;
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
                if (strDate.Length > 0)
                {
                    if (IsValidDate(mskCloseDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Please enter a valid close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        Success = false;
                    }
                    else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                    {
                        MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select(); 
                        mskCloseDate.Focus();
                        Success = false;
                    }
                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                    {
                        MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        Success = false;
                    }
                    else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate && !IsPaymentVoid)
                    {
                        MessageBox.Show("Void Close Date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        Success = false;
                    }
                    else if (cmbCloseDayTray.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Tray.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCloseDayTray.Focus();
                        Success = false;
                    }
                    else if (txtNotes.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the notes", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNotes.Focus();
                        txtNotes.Select();
                        Success = false;
                    }
                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            Success = false;
                        }
                        else
                        {
                            Success = true;
                        }
                    }
                    else if (!SavePAValidation())
                    {
                        Success = false;
                    }
                    else
                    {
                        Success = true;
                    }
                }
                else if (strDate.Length == 0)
                {
                    MessageBox.Show("Please enter a close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                }
                else
                {
                    Success = true;
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            return Success;
            
        }
        
        #endregion

        #region " Void Tray "

        private void FillCloseDayTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseConnection);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {

                if (IsAdmin(_UserID) == true)
                {
                    //_sqlQuery = "SELECT nChargeTrayID,sCode, " +
                    //    " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                    //    " FROM BL_ChargesTray WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                    //    "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0  AND nClinicID = " + _ClinicID + "";

                    _sqlQuery = "SELECT nChargeTrayID,sCode, " +
                       " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                       " FROM BL_ChargesTray WITH (NOLOCK) WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                       "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 and ISNULL(bIsActive,0)=1 AND nClinicID = " + _ClinicID + "";
                }
                else
                {
                    //_sqlQuery = "SELECT nChargeTrayID,sCode, " +
                    //" sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                    //" FROM BL_ChargesTray WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                    //"AND sDescription <> '' AND nUserID = " + _UserID + " AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _ClinicID + "";

                    _sqlQuery = "SELECT nChargeTrayID,sCode, " +
                   " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                   " FROM BL_ChargesTray WITH (NOLOCK) WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                   "AND sDescription <> '' AND nUserID = " + _UserID + " AND ISNULL(bIsClosed,0) = 0 and ISNULL(bIsActive,0)=1 AND nClinicID = " + _ClinicID + "";
                }

                DataTable dtCloseDayTray = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
                oDB.Disconnect();

                cmbCloseDayTray.SelectedIndexChanged -= cmbCloseDayTray_SelectedIndexChanged;

                cmbCloseDayTray.DataSource = dtCloseDayTray;
                cmbCloseDayTray.ValueMember = "nChargeTrayID";
                cmbCloseDayTray.DisplayMember = "sDescription";

                cmbCloseDayTray.SelectedIndexChanged += cmbCloseDayTray_SelectedIndexChanged;
                cmbCloseDayTray_SelectedIndexChanged(null, null);

                if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                {

                     oDB.Connect(false);

                    //To Fetch the Tray From the Charges Tables 
                     _sqlQuery = " select nChargesDayTrayID,sChargesTrayDescription from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID= " + _TransactionID + "";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        DataRow[] filteredRows = dtCloseDayTray.Select("nChargeTrayID = " + Convert.ToInt64(_retVal) +"");
                        if (filteredRows.Length > 0)
                        {
                            DataRow dr;
                            dr = filteredRows[0];
                            _defaultTrayId = Convert.ToInt64(dr[0]);
                        }
                        else
                        {
                            _sqlQuery = " SELECT ISNULL(nChargeTrayID,0) As nChargeTrayID FROM BL_ChargesTray WITH (NOLOCK) " +
                                         " WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                                        " AND sDescription <> '' AND bIsDefault = 'true' AND ISNULL(bIsClosed,0) = 0 AND nUserID = " + _UserID + " AND nClinicID = " + _ClinicID + "";
                            _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                            if (_retVal != null && Convert.ToString(_retVal) != "")
                            {
                                _defaultTrayId = Convert.ToInt64(_retVal);
                            }

                        }
                    }
                     
                    oDB.Disconnect();
                    if (Convert.ToString(_defaultTrayId).Trim() != "" && Convert.ToInt64(_defaultTrayId) > 0)
                    {
                        //_defaultTrayId = Convert.ToInt64(_retVal);
                        cmbCloseDayTray.SelectedValue = _defaultTrayId;
                    }
                    else
                    { cmbCloseDayTray.SelectedIndex = 0; }

                   
                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                        {
                            _defaultTrayId = Convert.ToInt64(_retVal);

                            #region " Set last selected tray "

                            //...Check if the last selected tray is same as the default tray if yes set the 
                            //...last selected tray or else show pop to select the tray

                            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnection);
                            Object _retSettingValue = null;
                            oSettings.GetSetting("CHARGES_LASTCLOSETRAYID", _UserID, _ClinicID, out _retSettingValue);
                            oSettings.Dispose();

                            if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                            {
                                if (Convert.ToString(_retSettingValue).Trim() == _defaultTrayId.ToString())
                                { cmbCloseDayTray.SelectedValue = _defaultTrayId; }
                                else
                                {
                                    //...Show pop-up to select the Tray
                                   // _ShowSelectTray = true;
                                }
                            }
                            else
                            { cmbCloseDayTray.SelectedValue = _defaultTrayId; }

                            #endregion " Set last selected close date "
                        }
                        else
                        {
                            //...Is default is not present then select the last selected tray

                            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnection);
                            Object _retSettingValue = null;
                            oSettings.GetSetting("CHARGES_LASTCLOSETRAYID", _UserID, _ClinicID, out _retSettingValue);
                            oSettings.Dispose();

                            if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                            { cmbCloseDayTray.SelectedValue = Convert.ToInt64(_retSettingValue); }
                            else
                            { cmbCloseDayTray.SelectedValue = 0; }
                        }
                    
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
            }
        }

        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            DataTable oDataTable = new DataTable();
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST WITH(NOLOCK) where nUserID='" + UserId + "' and nAdministrator = 1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;


        }

        private void cmbCloseDayTray_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbCloseDayTray_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmbCloseDayTray.ContextMenu = null;// new ContextMenu();
                cmbCloseDayTray.ContextMenuStrip = null;// new ContextMenuStrip();
            }
        }

        private void cmbCloseDayTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCloseDayTray.SelectedIndex < 0)
                { lblCloseDayTray.Text = ""; }
                else
                { lblCloseDayTray.Text = cmbCloseDayTray.Text; }
            }
            catch (Exception)// ex)
            {
                lblCloseDayTray.Text = "";
                //ex.ToString();
                //ex = null;
            }
        }

        private void frmSetupTransactionNotes_Shown(object sender, EventArgs e)
        {
            if (_ShowSelectTray == true)
            {
                frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_databaseConnection);
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = true;
                ofrmBillingTraySelection.ShowDialog(this);
                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    { cmbCloseDayTray.SelectedValue = ofrmBillingTraySelection.SelectedTrayID; }
                    else
                    { cmbCloseDayTray.SelectedValue = -1; }
                }
                ofrmBillingTraySelection.Dispose();
            }
        }

        private void btnSelectChargeTry_Click(object sender, EventArgs e)
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_databaseConnection);
            ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
            ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
            ofrmBillingTraySelection.IsChargeTray = true;
            ofrmBillingTraySelection.ShowDialog(this);
            if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
            {
                if (ofrmBillingTraySelection.SelectedTrayID > 0)
                {
                    //20100217 Mahesh Nawal Fill the Combo box
                    FillCloseDayTray();
                    cmbCloseDayTray.SelectedValue = ofrmBillingTraySelection.SelectedTrayID; }
                else
                { cmbCloseDayTray.SelectedValue = -1; }
            }
            ofrmBillingTraySelection.Dispose();
        }

        private void btnSetupJournal_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetupChargesTray ofrmSetupChargesTray = new frmSetupChargesTray(0, _databaseConnection);
                ofrmSetupChargesTray.StartPosition = FormStartPosition.CenterScreen;
                ofrmSetupChargesTray.ShowDialog(this);
                ofrmSetupChargesTray.Dispose();
                FillCloseDayTray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnModifyJournal_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _TrayID = 0;
                if (cmbCloseDayTray != null && cmbCloseDayTray.DataSource != null && cmbCloseDayTray.Items.Count > 0)
                {
                    if (cmbCloseDayTray.SelectedValue != null && cmbCloseDayTray.SelectedValue.ToString() != "")
                    {
                        _TrayID = Convert.ToInt64(cmbCloseDayTray.SelectedValue.ToString());

                        frmSetupChargesTray ofrmSetupChargesTray = new frmSetupChargesTray(_TrayID, _databaseConnection);
                        ofrmSetupChargesTray.StartPosition = FormStartPosition.CenterScreen;
                        ofrmSetupChargesTray.ShowDialog(this);
                        ofrmSetupChargesTray.Dispose();
                        FillCloseDayTray();
                        cmbCloseDayTray.SelectedValue = _TrayID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        #endregion

        private bool SavePAValidation()
        {
            DataTable _dtDates = new DataTable();
            bool _trackLimit = false;
            try
            {
                DataRow _drPAInfo = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetPriorAuthorizationInfo(oTransaction.PriorAuthorizationID);
                    if (_drPAInfo != null)
                    {
                        DataTable _dtLines = new DataTable();
                        _dtLines.Columns.Add("DateUsed");
                        if (Convert.ToString(_drPAInfo["nAuthorizationType"]).Trim() != "2")
                        {
                            _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);
                            if (_trackLimit)
                            {
                                DataTable _dtUniqueDates = new DataTable();
                                _dtUniqueDates = null;
                                if (oTransaction.Lines != null)
                                {
                                    
                                    _dtUniqueDates = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetUniqueDatesVoidClaim(oTransaction.PriorAuthorizationID, (gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date))), oTransaction.TransactionMasterID);
                                    
                                //    for (int _GridRow = 0; _GridRow < oTransaction.Lines.Count; _GridRow++)
                                //    {
                                //        _dtLines.DefaultView.RowFilter = "DateUsed=" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[_GridRow].DateServiceFrom.ToShortDateString());
                                //        if (_dtLines.DefaultView.Count.Equals(0))
                                //        {
                                //            DataRow rw = _dtLines.NewRow();
                                //            if (Convert.ToString(oTransaction.Lines[_GridRow].DateServiceFrom).Trim() != "")
                                //            {
                                //                Boolean _bIsExist = false;
                                //                if (_dtUniqueDates.Rows.Count > 0)
                                //                {
                                //                    for (int i = 0; i <= _dtUniqueDates.Rows.Count - 1; i++)
                                //                    {
                                //                        if (gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[_GridRow].DateServiceFrom.ToShortDateString()) == Convert.ToInt64(_dtUniqueDates.Rows[i]["DateUsed"]))
                                //                        {
                                //                            _bIsExist = true;
                                //                        }
                                                            
                                //                    }
                                //                }
                                //                if (!_bIsExist)
                                //                {
                                //                    rw["DateUsed"] = gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[_GridRow].DateServiceFrom.ToShortDateString());
                                //                    _dtLines.Rows.Add(rw);
                                //                    _dtLines.AcceptChanges();
                                //                }
                                                
                                //            }
                                //            rw = null;
                                //        }
                                //        _dtLines.DefaultView.RowFilter = "";

                                //    }

                                }
                                if (_dtUniqueDates != null && _dtUniqueDates.Rows.Count > 0)
                                { //Entry in Database.
                                    if (MessageBox.Show("Prior authorization " + _sPriorAuthNo + " will use -" + _dtUniqueDates.Rows.Count + " visits.\nContinue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    { return false; }
                                }
                            }
                        }
                    }
                }
            catch
            { }
            return true;
        }

        private void lblCloseDayTray_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                label = (Label)sender;
                _tlTip.RemoveAll(); 
                if (lblCloseDayTray.Text != null && lblCloseDayTray.Text != "")
                {
                   
                    if (lblCloseDayTray.Text.Length >= 17)
                    {
                        
                        _tlTip.SetToolTip(lblCloseDayTray, lblCloseDayTray.Text);
                    }
                    else
                    {
                        this._tlTip.Hide(lblCloseDayTray);
                    }
                }
                else
                {
                    this._tlTip.Hide(lblCloseDayTray);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblCloseDayTray_MouseLeave(object sender, EventArgs e)
        {
            this._tlTip.Hide(lblCloseDayTray);
        }

        private void mskCloseDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
    }
}