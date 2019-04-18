using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloPatient
{
    public partial class frmMUReferals : Form
    {
        public frmMUReferals()
        {
            InitializeComponent();
        }

      private DataTable _cmbRef;
      public DataTable cmbRef
        {
            get { return _cmbRef; }
            set { _cmbRef = value; }
        }


      //private int Changed=0;
      //public DataTable cmbRef
      //{
      //    get { return _cmbRef; }
      //    set { _cmbRef = value; }
      //}

      DataTable dtRef = null;
      DataSet dsRef = new DataSet();
     // DataSet ds = null;
      private const Int32 COL_ID = 1;
      private const Int32 COL_Name = 2;
      private const Int32 COL_Date = 3;
      private const Int32 COL_Checkbox = 4;  
      private const Int32 COL_COUNT = 4;

     // Int32 RowIndex = 0;

      private void frmMUReferals_Load(object sender, EventArgs e)
      {
        
          
          FillMuReferals();          
        
      }

      private void FillMuReferals()
      {
        
          gloAddress.gloC1FlexStyle.Style(C1MuReferals,false);
          dtRef = cmbRef.Copy () ;

          //ds = new DataSet();
          //ds.Tables.Add(dtRef);
          C1MuReferals.Visible = true;
     

          try
          {
              if (dtRef!=null)
              {
                  if (dtRef.Rows.Count > 0)
                  {
                      DesignGrid();
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

      private void DesignGrid()
      {
          try
          {

              C1MuReferals.Visible=true;
              //C1MuReferals.Rows.Fixed = 1;
              //C1MuReferals.AllowDragging=C1.Win.C1FlexGrid.AllowDraggingEnum.None;
              //C1MuReferals.Cols.Count = 4;
              //C1MuReferals.Rows.Count = 1;

              dtRef.TableName = "PatientReferrals";
              dsRef.Tables.Add (dtRef);
              
              //C1MuReferals.DataSource=dtRef;

              C1MuReferals.SetDataBinding(dsRef, "PatientReferrals", true);

            


              //C1MuReferals.Cols[COL_ID].AllowEditing = false;
             
              //C1MuReferals.Cols[COL_ID].Visible = false;
            

              //C1MuReferals.Cols[COL_Checkbox].AllowEditing = true;
              //C1MuReferals.Cols[COL_Checkbox].Caption = "Mu Referral";
              //C1MuReferals.Cols[COL_Checkbox].DataType = typeof(bool);
             

              //C1MuReferals.Cols[COL_Name].AllowEditing = false;
              //C1MuReferals.Cols[COL_Name].Caption = "Name";
              //C1MuReferals.Cols[COL_Name].DataType = typeof(string);
             


              //C1MuReferals.Cols[COL_Date].AllowEditing = true;
              //C1MuReferals.Cols[COL_Date].Caption = "Date";
              //C1MuReferals.Cols[COL_Date].DataType = typeof(DateTime);
              //C1MuReferals.Cols[COL_Date].DataType = typeof(DateTime);
            

              //C1MuReferals.Select(1, COL_Date);
              
          }
          catch (Exception ex)
          {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
          }
          finally
          {
              //if ((ogloSettings != null))
              //{
              //    ogloSettings.Dispose();
              //    ogloSettings = null;
              //}
              //C1ConsolidatedList.Redraw = true;
          }
      }

      private void tls_btnOK_Click(object sender, EventArgs e)
      {

          cmbRef.Dispose();
            cmbRef=null;


          cmbRef = new DataTable();

          this.Focus(); 

          this.BindingContext[dsRef, "PatientReferrals"].EndCurrentEdit();
          C1MuReferals.EndUpdate();
          //this.BindingContext(dsRef, "PatientReferrals").EndCurrentEdit();

          

          dsRef.AcceptChanges();
          cmbRef = dsRef.Tables["PatientReferrals"].Copy ()  ; //(DataTable)(C1MuReferals.DataSource);
          this.Close();
          
 
      }

      private void tls_btnCancel_Click(object sender, EventArgs e)
      {
          this.Close();

      }

      private void frmMUReferals_FormClosed(object sender, FormClosedEventArgs e)
      {
          dsRef.Tables.Remove(dtRef);
          dsRef.Dispose();
          dsRef = null;
      }

    }

   
    

}
