using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.C1GridFilter
{
    public partial class AccountLogTypeFilterEditor : UserControl,
        C1.Win.C1FlexGrid.IC1ColumnFilterEditor
    {

        #region "Class Variables"

        private bool NoteChecked = false;
        AccountLogTypeFilter _filter;
        private enum GridColumnAccountLog
        {
            NoteId,
            EOBID,
            DebitId,
            CreditId,
            MstTransactionID,
            TransactionDetailID,
            TransactionID,
            RefundID,
            CloseDate,
            PatientID,
            Patient,
            Type,
            LogDesc,
            Delta,
            Balance,
            PatDue,
            User,
            Datetime
        }

        List<String> listNoteType = new List<String>()
        {
            "Chrg Note",
            "Claim Note",
            "Acct Note",
            "Stmt Note",
            "Patient Alert"
        }; 

        #endregion

        #region "Constructor"

        public AccountLogTypeFilterEditor()
        {
            InitializeComponent();
            chklistTypes.CheckOnClick = true;
        } 

        #endregion

        #region "IC1ColumnFilterEditor Implementations"

        void C1.Win.C1FlexGrid.IC1ColumnFilterEditor.ApplyChanges()
        {
            // reset filter
            _filter.Filters.Clear();

            // add selected ranges
            for (int iCount = 0; iCount <= chklistTypes.CheckedItems.Count - 1; iCount++)
            {
                _filter.Filters.Add(chklistTypes.CheckedItems[iCount].ToString());
            }

            if (chkNotes.Checked)
            {
                _filter.Notes = true;
            }
            else
            {
                _filter.Notes = false;
            }
        }

        void C1.Win.C1FlexGrid.IC1ColumnFilterEditor.Initialize(C1.Win.C1FlexGrid.C1FlexGridBase grid, int columnIndex, C1.Win.C1FlexGrid.IC1ColumnFilter filter)
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            int iNoteTypeCount = 0;
            for (int iRowCount = 1; iRowCount <= grid.Rows.Count - 1; iRowCount++)
            {
                if (!chklistTypes.Items.Contains(grid.GetData(iRowCount, (int)GridColumnAccountLog.Type)))
                {
                    chklistTypes.Items.Add(grid.GetData(iRowCount, (int)GridColumnAccountLog.Type), CheckState.Unchecked);
                }

                if (listNoteType.Contains(Convert.ToString(grid.GetData(iRowCount, (int)GridColumnAccountLog.Type)))) { iNoteTypeCount++; }
            }

            if (iNoteTypeCount > 0)
            {
                chkNotes.Visible = true;
            }
            else
            {
                chkNotes.Visible = false;
            }

            _filter = (AccountLogTypeFilter)filter;

            if (_filter.Filters.Count > 0)
            {
                foreach (var pt in _filter.Filters)
                {
                    int index = chklistTypes.Items.IndexOf(pt);
                    chklistTypes.SetItemChecked(index, true);
                }
            }
            else
            {
                for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
                {
                    chklistTypes.SetItemCheckState(ItemCount, CheckState.Checked);
                }
            }


            if (chklistTypes.CheckedItems.Count == chklistTypes.Items.Count)
            {
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                chkSelectAll.Checked = true;
               
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            }
          
            this.chkNotes.CheckedChanged -= new System.EventHandler(this.chkNotes_CheckedChanged);
            if (_filter.Notes)
            {
                chkNotes.Checked = true;
            }
            else
            {
                chkNotes.Checked = false;
            }
            this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
        }

        bool C1.Win.C1FlexGrid.IC1ColumnFilterEditor.KeepFormOpen
        {
            get { return false; }
        } 

        #endregion

        #region "Controls Event"

        private void chkNotes_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkNotes.Checked)
            {
                NoteChecked = true;
                chkSelectAll.Checked = false;

                for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
                {
                    if (listNoteType.Contains(Convert.ToString(chklistTypes.Items[ItemCount]))) 
                    { chklistTypes.SetItemCheckState(ItemCount, CheckState.Checked); }
                    else
                    { chklistTypes.SetItemCheckState(ItemCount, CheckState.Unchecked); }
                }
            }
            else
            {
                NoteChecked = false;               
                
                for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
                {
                    if (listNoteType.Contains(Convert.ToString(chklistTypes.Items[ItemCount]))) { chklistTypes.SetItemCheckState(ItemCount, CheckState.Unchecked); }
                }                

                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                if (chklistTypes.CheckedItems.Count == chklistTypes.Items.Count)
                {
                    chkSelectAll.Checked = true;
                }
                else
                {
                    chkSelectAll.Checked = false;
                }
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            }         
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
                {
                    chklistTypes.SetItemCheckState(ItemCount, CheckState.Checked);
                }
                this.chkNotes.CheckedChanged -= new System.EventHandler(this.chkNotes_CheckedChanged);
                chkNotes.Checked = false;
                this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
            }
            else
            {
                for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
                {
                    chklistTypes.SetItemCheckState(ItemCount, CheckState.Unchecked);
                }

                if (!NoteChecked)
                {
                    chkNotes.Checked = false;
                }
                NoteChecked = false;
                                               
            }
        
        }

        #region " chklistTypes Events "              

        private void chklistTypes_MouseUp(object sender, MouseEventArgs e)
        {
            this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);          
            if (chklistTypes.CheckedItems.Count == chklistTypes.Items.Count)
            {
                this.chkNotes.CheckedChanged -= new System.EventHandler(this.chkNotes_CheckedChanged);
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);

                chkSelectAll.Checked = true;
                chkNotes.Checked = false;

                this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            }
            else
            {
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                chkSelectAll.Checked = false;
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);

                CheckforAllNoteTypeChecked();
            }

        }

        private void chklistTypes_KeyUp(object sender, KeyEventArgs e)
        {           
            this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);         
            if (chklistTypes.CheckedItems.Count == chklistTypes.Items.Count)
            {
                this.chkNotes.CheckedChanged -= new System.EventHandler(this.chkNotes_CheckedChanged);
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);

                chkSelectAll.Checked = true;
                chkNotes.Checked = false;

                this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            }
            else
            {
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                chkSelectAll.Checked = false;
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);

                CheckforAllNoteTypeChecked();
            }            
        }

        #endregion 

        #endregion

        #region "Public and Private Methods"

        private void CheckforAllNoteTypeChecked()
        {
            List<String> NoteTypes = new List<string>();

            for (int ItemCount = 0; ItemCount <= chklistTypes.Items.Count - 1; ItemCount++)
            {
                if (listNoteType.Contains(Convert.ToString(chklistTypes.Items[ItemCount]))) { NoteTypes.Add(chklistTypes.Items[ItemCount].ToString()); }
            }

            bool IsAllNoteTypeChecked = true;

            foreach (string notetype in NoteTypes)
            {
                int index = chklistTypes.CheckedItems.IndexOf(notetype);
                if (index <= -1)
                {
                    IsAllNoteTypeChecked = false;
                    break;
                }
            }

            this.chkNotes.CheckedChanged -= new System.EventHandler(this.chkNotes_CheckedChanged);
            if (!IsAllNoteTypeChecked)            
            {
                chkNotes.Checked = false;
            }

            this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
        } 

        #endregion        

    }

    class ColorCodedCheckedListBox : CheckedListBox
    {
        public Color UncheckedColor { get; set; }
        public Color CheckedColor { get; set; }
        public Color IndeterminateColor { get; set; }

        public ColorCodedCheckedListBox()
        {
            UncheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            CheckedColor = Color.Green; 
            IndeterminateColor = Color.Orange;
        }

        public ColorCodedCheckedListBox(Color uncheckedColor, Color checkedColor, Color indeterminateColor)
        {
            UncheckedColor = uncheckedColor;
            CheckedColor = checkedColor;
            IndeterminateColor = indeterminateColor;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                if (this.DesignMode)
                {
                    base.OnDrawItem(e);
                }
                else
                {
                    Color textColor = this.GetItemCheckState(e.Index) == CheckState.Unchecked ? UncheckedColor : (this.GetItemCheckState(e.Index) == CheckState.Checked ? CheckedColor : IndeterminateColor);

                    DrawItemEventArgs e2 = new DrawItemEventArgs
                       (e.Graphics,
                        e.Font,
                        new Rectangle(e.Bounds.Location, e.Bounds.Size),
                        e.Index,
                        (e.State & DrawItemState.Focus) == DrawItemState.Focus ? DrawItemState.Focus : DrawItemState.None,
                        textColor,
                        this.BackColor);

                    base.OnDrawItem(e2);
                }
            }
        }
    }

}
