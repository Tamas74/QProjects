using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupPaymentNotes : Form
    {
        private string _databaseConnection = "";
        private Int64 _BillingTransactionID = 0;
        private Int64 _BillingTransactionDetailID = 0;
        private Int64 _UserID = 0;
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private NoteType _NoteType = NoteType.GeneralNote; 

        public Common.PaymentNote oNote = new global::gloBilling.Common.PaymentNote();
        public bool oDialogResult = false;
        public NoteType SetNoteType
        {
            get { return _NoteType; }
            set { _NoteType = value; }
        }

        public frmSetupPaymentNotes(string DatabaseConnectionString, Int64 ClinicID, Int64 BillingTransactionID, Int64 BillingTransactionDetailIDs)
        {
            InitializeComponent();
            _databaseConnection = DatabaseConnectionString;
            _BillingTransactionID = BillingTransactionID;
            _ClinicID = ClinicID;
            _BillingTransactionDetailID = BillingTransactionDetailIDs;

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
        }


        public frmSetupPaymentNotes(string DatabaseConnectionString, Int64 ClinicID)
        {
            InitializeComponent();
            _databaseConnection = DatabaseConnectionString;
            _ClinicID = ClinicID;
         
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
        }

        private void frmSetupPaymentNotes_Load(object sender, EventArgs e)
        {
            lvwNotes.Items.Clear();
            lvwNotes.Columns.Clear();

            lvwNotes.Columns.Add("PaymentTransactionID");
            lvwNotes.Columns.Add("PaymentTransactionDetailID");
            lvwNotes.Columns.Add("NoteType");
            lvwNotes.Columns.Add("NoteID");
            lvwNotes.Columns.Add("Date/Time");
            lvwNotes.Columns.Add("Notes");

            lvwNotes.Columns[0].Width = 0;
            lvwNotes.Columns[1].Width = 0;
            lvwNotes.Columns[2].Width = 0;
            lvwNotes.Columns[3].Width = 0;
            lvwNotes.Columns[4].Width = 100;
            lvwNotes.Columns[5].Width = 350;


            lvwNotes.Visible = false;
            txtNotes.Visible = true;
            tlb_Delete.Visible = false;
            tlb_Notes.Visible = false;
            tlb_History.Visible = true;

            txtNotes.Text = "";
            txtNotes.Select();

            //Fill Current Notes if any
            if (oNote != null)
            {
                if (oNote.PaymentID == 0 && oNote.TransactionPaymentDetailId == 0 && oNote.NoteDescription.Length > 0)
                {
                    txtNotes.Text = oNote.NoteDescription;
                }
            }
            if (txtNotes.Text.Length > 0) { txtNotes.SelectionStart = txtNotes.Text.Length; }
        }

        private void GetPaymentNotes()
        {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
           gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            
           DataTable dtNotes;
           string strSQL = "";
       //    Int64 _PaymentTransactionID = 0;
       //    Int64 _PaymentTransactionDetailID = 0;

            try
            {

                dtNotes = new DataTable();

                oDB.Connect(false);
                strSQL = "select nPaymentTransactionID, nPaymentTransactionDetailID, nPaymentNoteId, nNoteType, nNoteDateTime, nUserID, sNoteDescription, nClinicID from BL_Transaction_Payment_Notes where " +
                " nPaymentTransactionID IN (select nPaymentTransactionID from BL_Transaction_Payment_DTL where nBillingTransactionID = " + _BillingTransactionID + " and nBillingTransactionDetailID = " + _BillingTransactionDetailID + ") " +
                " and " +
                " nPaymentTransactionDetailID IN (select nPaymentTransactionDetailID from BL_Transaction_Payment_DTL where nBillingTransactionID = " + _BillingTransactionID + " and nBillingTransactionDetailID = " + _BillingTransactionDetailID + ")" +
                " AND nClinicID = " + _ClinicID + "";

                oDB.Retrive_Query(strSQL, out dtNotes);
                oDB.Disconnect();

                //First Clear list data if any
                if (lvwNotes.Items.Count > 0)
                {
                    lvwNotes.Items.Clear();
                }
                
                //Fill Current Notes if any
                if (oNote != null)
                {
                    if (oNote.PaymentID > 0 && oNote.TransactionPaymentDetailId > 0 && oNote.NoteDescription.Length > 0)
                    {
                    ListViewItem oItem = new ListViewItem();
                    oItem.Text = oNote.PaymentID.ToString();
                    oItem.SubItems.Add(oNote.TransactionPaymentDetailId.ToString());
                    oItem.SubItems.Add(oNote.NoteType.GetHashCode().ToString());
                    oItem.SubItems.Add(oNote.PaymentNoteID.ToString());
                    oItem.SubItems.Add(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oNote.NoteDate)).ToString("MM/dd/yyyy"));
                    oItem.SubItems.Add(oNote.NoteDescription);
                    lvwNotes.Items.Add(oItem);
                    oItem = null;
                    }
                }

                //Fill History of Notes
                if (dtNotes != null)
                {
                    if (dtNotes.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtNotes.Rows.Count; i++)
                        {
                            ListViewItem oItem = new ListViewItem();
                            oItem.Text = dtNotes.Rows[i]["nPaymentTransactionID"].ToString();
                            oItem.SubItems.Add(dtNotes.Rows[i]["nPaymentTransactionDetailID"].ToString());
                            oItem.SubItems.Add(dtNotes.Rows[i]["nNoteType"].ToString());
                            oItem.SubItems.Add(dtNotes.Rows[i]["nPaymentNoteId"].ToString());
                            oItem.SubItems.Add(Convert.ToString(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtNotes.Rows[i]["nNoteDateTime"])).ToShortDateString()));
                            oItem.SubItems.Add(dtNotes.Rows[i]["sNoteDescription"].ToString());
                            lvwNotes.Items.Add(oItem);
                            oItem = null;
                        }
                    }
                }
                if (dtNotes != null) { dtNotes.Dispose(); }

                //Fill Current Notes if any
                if (oNote != null)
                {
                    if (oNote.PaymentID == 0 && oNote.TransactionPaymentDetailId == 0 && oNote.NoteDescription.Length > 0)
                    {
                        txtNotes.Text = oNote.NoteDescription;
                    }
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
            }
        }

        private void tlb_History_Click(object sender, EventArgs e)
        {
            try
            {
                lvwNotes.Visible = true;
                lvwNotes.BringToFront();
                txtNotes.Visible = false;
                tlb_Delete.Visible = true;
                tlb_Notes.Visible = true;
                tlb_History.Visible = false;
                tlb_Ok.Enabled = false;

                GetPaymentNotes();
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
            tlb_Delete.Visible = false;
            tlb_Notes.Visible = false;
            tlb_History.Visible = true;

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
                oNote.PaymentID = 0;
                oNote.TransactionPaymentDetailId = 0;
                //oNote.NoteType = NoteType.GeneralNote;
                oNote.NoteType = SetNoteType;
                oNote.PaymentNoteID = 0;
                oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                oNote.UserID = _UserID;
                oNote.NoteDescription = txtNotes.Text;
                oNote.ClinicID = _ClinicID;
                oDialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

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
                        Int64 nPaymentID = Convert.ToInt64(lvwNotes.SelectedItems[i].Text);
                        Int64 nTransactionPaymentDetailID = Convert.ToInt64(lvwNotes.SelectedItems[i].SubItems[1].Text);
                        Int64 nNoteID = Convert.ToInt64(lvwNotes.SelectedItems[i].SubItems[3].Text);

                        strSQL = " DELETE FROM BL_Transaction_Payment_Notes WHERE (nTransactionPaymentID = "+nPaymentID +") AND (nTransactionPaymentDetailID = "+nTransactionPaymentDetailID +") AND (nPaymentNoteId = "+ nNoteID+") AND (nClinicID = "+_ClinicID+")";
                        oDB.Connect(false);
                        oDB.Execute_Query(strSQL);
                        oDB.Disconnect();
                    }

                    GetPaymentNotes();
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
            }
        }



    }
}