using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloGlobal
{
    public partial class frmSelectProviderTaxID : Form
    {
        public long nAssociationID { get; set; }
        public string sProviderTaxID { get; set; }
        //private string strConString="";
       
        private const int COL_Select = 0;
        private const int COL_MasterID = 1;
        private const int COL_AssociationID= 2;
        private const int COL_ProviderID= 3;
        private const int COL_TIN= 4;
        private const int COL_Tital = 5;
        public DataTable dtProviderTaxIDs = null;       
        private string _Messageboxcaption = "gloEMR";
        //private Int64 nProviderID = 0;
        //private bool IsCloseandSaveClicked = false;
        public frmSelectProviderTaxID()
        {
            InitializeComponent();
        }

        public frmSelectProviderTaxID(Int64 nProviderID)
        {
            InitializeComponent();
            nAssociationID = 0;
            sProviderTaxID = "";
            dtProviderTaxIDs = getMultipleProviderTaxID(nProviderID);
            _Messageboxcaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }
        

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {
           //IsCloseandSaveClicked = false;
           int nCollectionAgnecy = c1SelectProviderTaxIDs.FindRow(true, 0, 0, true);
           if (nCollectionAgnecy > 0)
           {
               for (int row = c1SelectProviderTaxIDs.Rows.Fixed; row < c1SelectProviderTaxIDs.Rows.Count; row++)
               {
                   C1.Win.C1FlexGrid.CheckEnum state = c1SelectProviderTaxIDs.GetCellCheck(row, COL_Select);
                   if (state == C1.Win.C1FlexGrid.CheckEnum.Checked)
                   {
                       nAssociationID = Convert.ToInt64(c1SelectProviderTaxIDs.GetData(row, COL_AssociationID));
                       sProviderTaxID = Convert.ToString (c1SelectProviderTaxIDs.GetData(row, COL_TIN));
                       this.DialogResult = DialogResult.OK;
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ProviderMultipleTaxID, ActivityCategory.ProviderMultipleTaxIDView, ActivityType.Select, "Provider TaxID =" + sProviderTaxID + "", 0, nAssociationID, 0, ActivityOutCome.Success, SoftwareComponent.gloEMR , true);
                   }
                  
               }
               //IsCloseandSaveClicked = true;
               this.Close();
           }
           else
           {
               //if (MessageBox.Show("do you want to continue transaction without provider Tax ID ?", _Messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
               //{
               //    nAssociationID = 0;
               //    sProviderTaxID = "";                   
               //    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ProviderMultipleTaxID, ActivityCategory.ProviderMultipleTaxIDView, ActivityType.Yes, "User continue transaction without selection TaxID", 0, nAssociationID, 0, ActivityOutCome.Success, SoftwareComponent.gloEMR, true);
               //}
               MessageBox.Show("Select Provider TaxID.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information );
                
              
           }
           //IsCloseandSaveClicked = true;

        }

        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
          
            this.DialogResult=MessageBox.Show("Do you want to continue transaction without provider Tax ID ?", _Messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );
            if (this.DialogResult == DialogResult.Yes)
            {
                nAssociationID = 0;
                sProviderTaxID = "";
                this.DialogResult = DialogResult.OK  ;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ProviderMultipleTaxID, ActivityCategory.ProviderMultipleTaxIDView, ActivityType.Close, "User continue transaction without selection of TaxID", 0, nAssociationID, 0, ActivityOutCome.Success, SoftwareComponent.gloEMR, true);
                this.Close();
            }
            else if (this.DialogResult == DialogResult.No)
            {
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel ;
            }
           

        }

        private void frmSelectProviderTaxID_Load(object sender, EventArgs e)
        {


            c1SelectProviderTaxIDs.SetData(0, COL_MasterID, "nTaxMasterID");
            c1SelectProviderTaxIDs.SetData(0, COL_Select, "Select");
            c1SelectProviderTaxIDs.SetData(0, COL_AssociationID, "nAssociationID");
            c1SelectProviderTaxIDs.SetData(0, COL_ProviderID, "nProviderID");
            c1SelectProviderTaxIDs.SetData(0, COL_TIN, "TaxID");
            c1SelectProviderTaxIDs.SetData(0, COL_Tital, "Title");

            c1SelectProviderTaxIDs.Cols[COL_MasterID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1SelectProviderTaxIDs.Cols[COL_TIN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            c1SelectProviderTaxIDs.Cols[COL_Select].AllowEditing = true;
            c1SelectProviderTaxIDs.Cols[COL_MasterID].AllowEditing = false;
            c1SelectProviderTaxIDs.Cols[COL_AssociationID].AllowEditing = false;
            c1SelectProviderTaxIDs.Cols[COL_ProviderID].AllowEditing = false;
            c1SelectProviderTaxIDs.Cols[COL_TIN].AllowEditing = false;
            c1SelectProviderTaxIDs.Cols[COL_Tital].AllowEditing = false;

            c1SelectProviderTaxIDs.Cols[COL_MasterID].Width = 0;
            c1SelectProviderTaxIDs.Cols[COL_Select].Width = Width / 8;
            c1SelectProviderTaxIDs.Cols[COL_Tital].Width = (Width / 2) + 93;
            c1SelectProviderTaxIDs.Cols[COL_TIN].Width = Width / 4;
            c1SelectProviderTaxIDs.Cols[COL_ProviderID].Width = 0;
            c1SelectProviderTaxIDs.Cols[COL_AssociationID].Width = 0;

            c1SelectProviderTaxIDs.Cols[COL_Select].DataType = typeof(Boolean);

           // c1CollectionAgency.Clear();
            c1SelectProviderTaxIDs.DataSource = null;
            //c1CollectionAgency.Clear();
            //c1CollectionAgency.DataSource = dtProviderTaxIDs.DefaultView;
            c1SelectProviderTaxIDs.Rows.Fixed = 1;
            if (dtProviderTaxIDs != null && dtProviderTaxIDs.Rows.Count > 0)
            {

                for (int i = 1; i <= dtProviderTaxIDs.Rows.Count; i++)
                {
                    c1SelectProviderTaxIDs.Rows.Add();
                    c1SelectProviderTaxIDs.SetData(i, COL_MasterID, Convert.ToString(dtProviderTaxIDs.Rows[i - 1]["nTINMasterID"]));
                    c1SelectProviderTaxIDs.SetData(i, COL_AssociationID, Convert.ToString(dtProviderTaxIDs.Rows[i - 1]["nAssociationID"]));
                    c1SelectProviderTaxIDs.SetData(i, COL_ProviderID, Convert.ToString(dtProviderTaxIDs.Rows[i - 1]["nProviderID"]));
                    c1SelectProviderTaxIDs.SetData(i, COL_TIN, Convert.ToString(dtProviderTaxIDs.Rows[i - 1]["sTIN"]));
                    c1SelectProviderTaxIDs.SetData(i, COL_Tital, Convert.ToString(dtProviderTaxIDs.Rows[i - 1]["sTINTital"]));
                  
                }
                c1SelectProviderTaxIDs.Cols[6].Width = 0;
               
            }
             
            
        }

        private void c1SelectProviderTaxIDs_CellChecked(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_Select)
            {
                for (int row = c1SelectProviderTaxIDs.Rows.Fixed; row < c1SelectProviderTaxIDs.Rows.Count; row++)
                {
                    c1SelectProviderTaxIDs.SetCellCheck(row, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                }
                c1SelectProviderTaxIDs.SetCellCheck(e.Row, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
            }
        }

        public DataTable getMultipleProviderTaxID(Int64 nProviderId)
        {
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getProviderMultipleTaxIDs", oDBParameters, out dt);
                return dt.Copy() ;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB.Dispose();
                oDB = null;

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        private void frmSelectProviderTaxID_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.DialogResult ==DialogResult.Cancel  )
            {
                    e.Cancel = true;
               
            }
        }

       
    }
}
