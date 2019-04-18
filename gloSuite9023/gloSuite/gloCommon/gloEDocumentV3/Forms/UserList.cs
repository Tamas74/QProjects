using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace gloEDocumentV3.Forms
{
    public partial class UserList : UserControl
    {
        #region " Constructor "

        public UserList()
        {
            InitializeComponent();
        } 

        #endregion

        #region " Variable Declarations "

        public int colIndex = 0; 

        #endregion

        #region " Enumerations "

        public enum EnmControl
        {
            ADD = 0,
            Close = 1,
            Search = 2,
            Ok = 3,
            C1Task = 4
        } 

        #endregion

        #region " Delegates Declarations "

        public delegate void TextKeyPress(object sender, EventArgs e);
        public event TextKeyPress txtKeyPress;

        public delegate void TextChanged(object sender, EventArgs e);
        public event TextChanged txtChanged;

        public delegate void OKClick(object sender, EventArgs e);
        public event OKClick btnOkClick;

        public delegate void CancelClick(object sender, EventArgs e);
        public event CancelClick btnCancelClick;

        public delegate void AddClick(object sender, EventArgs e);
        public event AddClick btnAddClick;
        
        #endregion

        #region " Property Procedures "

        public string SearchText
        {
            get { return txtSearch.Text; }
            set { value = txtSearch.Text; }
        }

        public object CurrentID
        {
            get
            {
                return C1Task[C1Task.Row, 0];
            }
        }

        public int GridWidth
        {
            get
            {
                return C1Task.Width;
            }
        }

        public bool this[int rowIndex]
        {
            get
            {
                if (C1Task.Row == rowIndex)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public object this[int rowIndex, int colIndex]
        {
            get
            {
                return (Object)C1Task.GetData(rowIndex, colIndex);
            }
            set
            {
                value = C1Task.GetData(rowIndex, colIndex);
            }
        }

        public int GetCurrentRowIndex
        {
            get
            {
                return C1Task.RowSel;
            }
        }

        public int GetSelect
        {
            get
            {
                return C1Task.Row;
            }
        }

        public bool setVisible
        {
            set
            {
                btnAdd.Visible = value;
            }
        }

        #endregion

        #region " Events Region "

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(sender, e);
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            txtChanged(sender, e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOkClick(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnCancelClick(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAddClick(sender, e);
        }

        private void C1Task_Click(object sender, EventArgs e)
        {

            int i;
            i = C1Task.Col;

            int temp;
            C1.Win.C1FlexGrid.HitTestInfo hitInfo = C1Task.HitTest(MousePosition.X, MousePosition.Y);
            temp = hitInfo.Row;
            temp = C1Task.RowSel;

            if (temp > 0)
            {
                if (i > 0)
                {
                    if (C1Task.GetData(0, i).ToString() != "Select")
                    {
                        //Label1.Text = "";
                        //Label1.Text = "Search On " + Convert.ToString(C1Task.GetData(0, i));

                    }
                    else
                    {
                        int SelectedItem = Convert.ToInt32(C1Task.GetData(0, i));
                        colIndex = i;
                    }
                }
                else
                {
                    return;
                }
            }
            
        }

        #endregion

        #region " Public  & Private Methods "

        public void DataSource(DataView dv)
        {
            C1Task.DataSource = dv;
        }

        public void Selectsearch(UserList.EnmControl enm)
        {
            switch (enm)
            {
                case UserList.EnmControl.ADD:
                    btnAdd_Click(null, null);
                    break;
                case UserList.EnmControl.Close:
                    btnClose_Click(null, null);
                    break;
                case UserList.EnmControl.Search:
                    txtSearch.Focus();
                    break;
                case UserList.EnmControl.Ok:
                    btnOK_Click(null, null);
                    break;
                case UserList.EnmControl.C1Task:
                    C1Task.Select();
                    break;
            }
        } 



        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

    }
}
