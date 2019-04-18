using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Payment
{
    public partial class frmSplitClaimHoldSelection : Form
    {
        #region " Variable Declaration "

        const int COL_INSVIEW_COUNT = 8;
        const int COL_SELECT = 0;
        const int COL_CLAIM_NO = 1;
        const int COL_NEXT = 2;
        const int COL_PARTY = 3;
        const int COL_INSURANCENAME = 4;

        const int COL_INSURANCEID = 5;
        const int COL_INSSELFMODE = 6;
        const int COL_CONTACTID = 7;
 
        #endregion

        #region " Property Procedures "

        string _mainClaimNumber = string.Empty;
        DataTable _SplittedClaims = null;

        public string MainClaimNumber
        {
            get { return _mainClaimNumber; }
        }

        public DataTable SplittedClaims
        {
            get { return _SplittedClaims; }
        }

        DataRow _ParentClaimHoldNote = null;
        public DataRow ParentClaimHoldNote
        {
            get { return _ParentClaimHoldNote; }
        }

        #endregion

        #region " Constructor "

        public frmSplitClaimHoldSelection()
        {
            InitializeComponent();
        }

        public frmSplitClaimHoldSelection(DataTable dtSplittedClaims, DataRow _drParentClaimHoldNote, Int64 ClaimNumber, string SubClaimNumber)
        {
            string _claim = InsurancePayment.GetFormattedClaimPaymentNumber(Convert.ToString(ClaimNumber));

            if (!String.IsNullOrEmpty(SubClaimNumber))
            { _claim = string.Concat(_claim, "-", SubClaimNumber); }

            _mainClaimNumber = _claim;
            _SplittedClaims = dtSplittedClaims;
            _ParentClaimHoldNote = _drParentClaimHoldNote;

            InitializeComponent();
        } 

        #endregion

        #region " Form Events & Methods "

        private void frmSplitClaimHoldSelection_Load(object sender, EventArgs e)
        {
            if (_ParentClaimHoldNote != null)
            {
                DateTime _date = Convert.ToDateTime(_ParentClaimHoldNote["dtHoldDate"]);
                lblNote.Text = Convert.ToString(_ParentClaimHoldNote["sHoldReason"]);

                if (lblNote.Text.Length != 0)
                { lblNote.Text = lblNote.Text.Replace("\r", "").Replace("\n", " "); }
                
                if (lblNote.Text.Length > 110)
                { lblNote.Text = lblNote.Text.Substring(0, 110) + " .."; }
                
                lblUser.Text = Convert.ToString(_ParentClaimHoldNote["UserName"]);
                lblUser.Text = lblUser.Text + " " + _date.ToString("MM/dd/yyyy hh:mm tt");
            }
            //lblNote.Text = "Note : Claim Number " + MainClaimNumber + " will be no longer on hold.";

            FillSplittedClaimsList();
        }

        private void FillSplittedClaimsList()
        {
            DesignSplittedClaimsGrid();

            try
            {
                if (SplittedClaims != null)
                {
                    foreach (DataRow row in SplittedClaims.Rows)
                    {
                        c1SplitClaims.Rows.Add();
                        int _rowIndex = c1SplitClaims.Rows.Count - 1;

                        c1SplitClaims.SetData(_rowIndex, COL_SELECT, Convert.ToBoolean(row["IsHold"]));
                        c1SplitClaims.SetData(_rowIndex, COL_CLAIM_NO, Convert.ToString(row["SubClaimNo"]));

                        string _next = Convert.ToString(row["NextAction"]);
                        string _nextDisplay = "";

                        if (_next.Equals("R"))
                        { _nextDisplay = "Rebilling"; }
                        else if (_next.Equals("B"))
                        { _nextDisplay = "Billing"; }
                        else if (_next.Equals("P"))
                        { _nextDisplay = "Pending"; }
                        else if (_next.Equals("N"))
                        { _nextDisplay = "None"; }

                        c1SplitClaims.SetData(_rowIndex, COL_NEXT, _nextDisplay);

                        c1SplitClaims.SetData(_rowIndex, COL_PARTY, Convert.ToString(row["PartyNo"]));
                        c1SplitClaims.SetData(_rowIndex, COL_INSURANCENAME, Convert.ToString(row["Party"]));
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " ToolStrip Button Click Events "

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnSaveClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        #endregion

        #region " C1 Grid Events & Methods "

        private void c1SplitClaims_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            bool _isSelected = Convert.ToBoolean(c1SplitClaims.GetData(e.Row, COL_SELECT));
            SplittedClaims.Rows[e.Row - 1]["IsHold"] = _isSelected;
        }

        private void DesignSplittedClaimsGrid()
        {
            c1SplitClaims.Cols.Count = COL_INSVIEW_COUNT;
            c1SplitClaims.Rows.Count = 1;
            c1SplitClaims.SetData(0, COL_SELECT, "Select");
            c1SplitClaims.SetData(0, COL_CLAIM_NO, "Claim #");
            c1SplitClaims.SetData(0, COL_NEXT, "Action");
            c1SplitClaims.SetData(0, COL_PARTY, "Party");
            c1SplitClaims.SetData(0, COL_INSURANCENAME, "Insurance");

            c1SplitClaims.SetData(0, COL_INSURANCEID, "InsuranceID");
            c1SplitClaims.SetData(0, COL_INSSELFMODE, "Mode");
            c1SplitClaims.SetData(0, COL_CONTACTID, "Contact ID");

            c1SplitClaims.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column

            c1SplitClaims.Cols[COL_SELECT].AllowEditing = true;
            c1SplitClaims.Cols[COL_CLAIM_NO].AllowEditing = false;
            c1SplitClaims.Cols[COL_NEXT].AllowEditing = false;
            c1SplitClaims.Cols[COL_PARTY].AllowEditing = false;
            c1SplitClaims.Cols[COL_INSURANCENAME].AllowEditing = false;

            c1SplitClaims.Cols[COL_INSURANCEID].Visible = false;
            c1SplitClaims.Cols[COL_INSSELFMODE].Visible = false;
            c1SplitClaims.Cols[COL_CONTACTID].Visible = false;

            c1SplitClaims.Cols[COL_SELECT].Width = 45;
            c1SplitClaims.Cols[COL_NEXT].Width = 60;
            c1SplitClaims.Cols[COL_PARTY].Width = 50;

            int nWidth;
            nWidth = pnlInsurace.Width;

            c1SplitClaims.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

            c1SplitClaims.Cols[COL_INSURANCENAME].Width = Convert.ToInt32(nWidth - 265);
            c1SplitClaims.Cols[COL_INSURANCEID].Width = 0;
            c1SplitClaims.Cols[COL_INSSELFMODE].Width = 0;

            gloC1FlexStyle.Style(c1SplitClaims, false);
        }

        #endregion
    }
}