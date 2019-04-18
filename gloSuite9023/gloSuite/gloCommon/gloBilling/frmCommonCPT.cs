using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmCommonCPT : Form
    {
        public frmCommonCPT()
        {
            InitializeComponent();
        }
        Int16   CPTType = 1;
        DataView dv = null;
        DataTable dt = null;
        ClsCommonCPT objClsCommonCPT = null;
        public string DatabaseConnectionString
        {
            get;
            set;
        }
        public frmCommonCPT(Int16 _CPTType)
        {
            InitializeComponent();
            CPTType = _CPTType;
        }



        private void frmCommonCPT_Load(object sender, EventArgs e)
        {
            txtSearch.Select();  
            txtSearch.Focus();
            objClsCommonCPT = new ClsCommonCPT(DatabaseConnectionString, CPTType);
           FillGrid(CPTType);
            
            }
        private void FillGrid(int  CommonType)
        {
            switch (CommonType)
            {
                case 1:


        
          
            try
            {
                if (objClsCommonCPT == null)
                {
                    objClsCommonCPT = new ClsCommonCPT(DatabaseConnectionString, CPTType);
                }
                dt = objClsCommonCPT.getAllCommonCPT();

                if (dt != null)
                    dv = dt.DefaultView;
                else
                    return;

              
               // DataGridViewColumn dgColForSort = new DataGridViewColumn();
               // ListSortDirection lstdirection = new ListSortDirection();
              
                c1FlexCommon.DataSource = dv;
                c1FlexCommon.AllowEditing = true; 
                for (int col = 0; col <=dv.Table.Columns.Count; col++)
                {
                    c1FlexCommon.Cols[col].Visible = false;
                    c1FlexCommon.Cols[col].AllowEditing  = false;   
                }
               
                c1FlexCommon.Cols["sCPTCode"].Visible = true;
                c1FlexCommon.Cols["sDescription"].Visible = true;
                c1FlexCommon.Cols["Selected"].Visible = true;
                c1FlexCommon.Cols["Selected"].Caption = "Select";
                c1FlexCommon.Cols["Selected"].DataType =typeof(bool);
  
                c1FlexCommon.Cols["sCPTCode"].Caption  ="CPT Code";
                c1FlexCommon.Cols["sDescription"].Caption = "Description";
                c1FlexCommon.Cols["Selected"].Width = 100;
                c1FlexCommon.Cols["sCPTCode"].Width = 150;
                c1FlexCommon.Cols["sDescription"].Width  = 550;

                c1FlexCommon.Cols["Selected"].AllowEditing = true;
                c1FlexCommon.Cols["Selected"].AllowSorting = false; 
            }
            catch
            {
            }
                    break;


            }

        }

        private void tls_CPT_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string str = Convert.ToString(e.ClickedItem.Tag);
            switch (str)
            {
                case "OK":
                    dt = ((DataView)c1FlexCommon.DataSource).Table;
                    dt.AcceptChanges(); 
                    DataRow[] drr = dt.Select("Selected=1");
                    DataTable dtCommonType = new DataTable();
                    dtCommonType.Columns.Add("nCPTID",typeof(Int64));
                    dtCommonType.Columns.Add("bIsSelected", typeof(bool));  

                    foreach (DataRow dr in drr)
                    {
                     DataRow drcommon=   dtCommonType.NewRow();
                     drcommon["nCPTID"] = dr["nCPTID"];
                     drcommon["bIsSelected"] = true;
                     dtCommonType.Rows.Add(drcommon);   
                    }
                //    ClsCommonCPT objclscommon = new ClsCommonCPT(DatabaseConnectionString ,1);
                    if (objClsCommonCPT == null)
                    {
                        objClsCommonCPT = new ClsCommonCPT(DatabaseConnectionString, CPTType);
                    }
                    objClsCommonCPT.InsertCommonType(dtCommonType, CPTType);
                    dtCommonType.Dispose();
                    dtCommonType = null; 
                    this.Close();
                    break;
                case "Cancel":
                    this.Close();
                    break;
            }
        }

        private void frmCommonCPT_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dv != null)
            {
                dv.Dispose();
                dv = null;

            }
            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
            if (objClsCommonCPT != null)
            {
                objClsCommonCPT = null;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                FilterData(txtSearch.Text.Trim());
            }
            else
            {
                dv.RowFilter = ""; 
            }
            }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";  
        }

        private void FilterData(string Search)
        {
          string[] strSearchArray=null;
          string sFilter = "";
            Search = Search.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");

            if (Search.Trim() != "")
            {
                strSearchArray = Search.Split(',');
            }
            if (dv != null)
            {

                for (int i = 0; i < strSearchArray.Length; i++)
                {
                    Search = strSearchArray[i].Trim();
                    if (Search.Length > 1)
                    {
                        string str = Search.Substring(1).Replace("%", "");
                        Search = Search.Substring(0, 1) + str;
                    }
                    if (Search.Trim() != "")
                    {
                        if (sFilter == "")//(i == 0)
                        {
                            sFilter = " (sCPTCode Like '" + Search + "%' OR sDescription like '%" + Search + "%')";
                        }
                        else
                        {
                            sFilter = sFilter + " AND (sCPTCode Like '" + Search + "%' OR sDescription like '%" + Search + "%')";
                        }
                    }
                }

            dv.RowFilter = sFilter; 
            c1FlexCommon.DataSource = dv;    
 
            }
        }
        }
   
}


