using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmSelectCollectionAgency : Form
    {
        public long ContactId_Collection { get; set; }
        private string strConString="";
       
        private const int COL_CollectionAgency_Select = 0;
        private const int COL_CollectionAgency_ContactID = 1;
        private const int COL_CollectionAgency_Name = 2;
        private const int COL_CollectionAgency_FeeType = 3;
        private const int COL_CollectionAgency_Fee= 4;
        public DataTable dtCollectionAgency = null;
        gloContacts.clsCollectionAgency oCollection = null;
        private string _Messageboxcaption = "gloPM";

        public frmSelectCollectionAgency()
        {
            InitializeComponent();
        }

        public frmSelectCollectionAgency(string databaseConnectionstring)
        {
            InitializeComponent();
            strConString = databaseConnectionstring;
            oCollection = new gloContacts.clsCollectionAgency(strConString);
            dtCollectionAgency =  oCollection.GetCollectionAgency(0, false );
            _Messageboxcaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }
        

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {

           int nCollectionAgnecy = c1CollectionAgency.FindRow(true, 0, 0, true);
           if (nCollectionAgnecy > 0)
           {
               for (int row = c1CollectionAgency.Rows.Fixed; row < c1CollectionAgency.Rows.Count; row++)
               {
                   C1.Win.C1FlexGrid.CheckEnum state = c1CollectionAgency.GetCellCheck(row, COL_CollectionAgency_Select);
                   if (state == C1.Win.C1FlexGrid.CheckEnum.Checked)
                   {
                       ContactId_Collection = Convert.ToInt64(c1CollectionAgency.GetData(row, COL_CollectionAgency_ContactID));
                       this.DialogResult = DialogResult.OK;
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.CollectionAgency, ActivityType.Select, "Collection agency selected to Transfer Claim Balance.", 0, ContactId_Collection, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                   }
                  
               }

               this.Close();
           }
           else
           {
               MessageBox.Show("select collection agency.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        }

        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmSelectCollectionAgency_Load(object sender, EventArgs e)
        {
         
           
            c1CollectionAgency.SetData(0, COL_CollectionAgency_ContactID, "Contact ID");
            c1CollectionAgency.SetData(0, COL_CollectionAgency_Select, "Select");
            c1CollectionAgency.SetData(0, COL_CollectionAgency_Name, "Collection Agency");
            c1CollectionAgency.SetData(0, COL_CollectionAgency_FeeType, "Fee Type");
            c1CollectionAgency.SetData(0, COL_CollectionAgency_Fee, "Fee");

            c1CollectionAgency.Cols[COL_CollectionAgency_ContactID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1CollectionAgency.Cols[COL_CollectionAgency_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            c1CollectionAgency.Cols[COL_CollectionAgency_Select].AllowEditing =true;
            c1CollectionAgency.Cols[COL_CollectionAgency_Name].AllowEditing = false;
            c1CollectionAgency.Cols[COL_CollectionAgency_ContactID].AllowEditing = false;
            c1CollectionAgency.Cols[COL_CollectionAgency_FeeType].AllowEditing = false;
            c1CollectionAgency.Cols[COL_CollectionAgency_Fee].AllowEditing = false;

            c1CollectionAgency.Cols[COL_CollectionAgency_ContactID].Width = 0;
            c1CollectionAgency.Cols[COL_CollectionAgency_Select].Width = Width /8;
            c1CollectionAgency.Cols[COL_CollectionAgency_Name].Width = (Width /2)+93;
            c1CollectionAgency.Cols[COL_CollectionAgency_FeeType].Width = 0;
            c1CollectionAgency.Cols[COL_CollectionAgency_Fee].Width = Width /7;

            c1CollectionAgency.Cols[COL_CollectionAgency_Select].DataType = typeof(Boolean);

           // c1CollectionAgency.Clear();
            c1CollectionAgency.DataSource = null;
            //c1CollectionAgency.Clear();
            //c1CollectionAgency.DataSource = dtCollectionAgency.DefaultView;
            c1CollectionAgency.Rows.Fixed = 1;
            if (dtCollectionAgency != null && dtCollectionAgency.Rows.Count > 0)
            {

                for (int i = 1; i <= dtCollectionAgency.Rows.Count; i++)
                {
                    c1CollectionAgency.Rows.Add();
                    c1CollectionAgency.SetData(i, COL_CollectionAgency_ContactID, Convert.ToString(dtCollectionAgency.Rows[i - 1]["nContactId"]));
                    c1CollectionAgency.SetData(i, COL_CollectionAgency_Name, Convert.ToString(dtCollectionAgency.Rows[i - 1]["sName"]));
                    if (Convert.ToInt16(dtCollectionAgency.Rows[i - 1]["nBadDebtFeeType"]) == 1)
                    {
                        c1CollectionAgency.SetData(i, COL_CollectionAgency_FeeType, "% of self pay balance");
                        c1CollectionAgency.SetData(i, COL_CollectionAgency_Fee, Convert.ToString(dtCollectionAgency.Rows[i - 1]["nPercentofSelfPayBalance"]) + " %");
                    }
                    else if (Convert.ToInt16(dtCollectionAgency.Rows[i - 1]["nBadDebtFeeType"]) == 2)
                    {
                        c1CollectionAgency.SetData(i, COL_CollectionAgency_FeeType, "Flat Fee");
                        c1CollectionAgency.SetData(i, COL_CollectionAgency_Fee, "$ " + Convert.ToString(dtCollectionAgency.Rows[i - 1]["nFlatfee"]));
                    }
                }
               
               
            }
             
            
        }

        private void c1CollectionAgency_CellChecked(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_CollectionAgency_Select)
            {
                for (int row = c1CollectionAgency.Rows.Fixed; row < c1CollectionAgency.Rows.Count; row++)
                {
                    c1CollectionAgency.SetCellCheck(row, COL_CollectionAgency_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                }
                c1CollectionAgency.SetCellCheck(e.Row, COL_CollectionAgency_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
            }
        }

       

       
    }
}
