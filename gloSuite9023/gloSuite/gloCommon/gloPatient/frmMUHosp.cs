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
    public partial class frmMUHosp : Form
    {
        private string _dbconnstring = "";
        private Int64 _PatientID = 0;
        public frmMUHosp(string DbConnecstring,Int64 PatientID)
        {
          
            InitializeComponent();
            _dbconnstring = DbConnecstring;
            _PatientID = PatientID;
        }

      private DataTable _cmbRef;
      public DataTable cmbRef
        {
            get { return _cmbRef; }
            set { _cmbRef = value; }
        }
      public bool _SaveData = false;
     public DataTable dtpathosp = null;
      //private int Changed=0;
      //public DataTable cmbRef
      //{
      //    get { return _cmbRef; }
      //    set { _cmbRef = value; }
      //}

  public     DataTable dtHosp = null;
      DataSet dsHosp = new DataSet();
     // DataSet ds = null;
      private const Int32 COL_TableID = 4;
     private const Int32 COL_ID = 3;
      private const Int32 COL_Name = 1;
      private const Int32 COL_Date = 2;
      private const Int32 COL_Checkbox =0;  
      private const Int32 COL_COUNT =5;

     // Int32 RowIndex = 0;

      private void frmMUReferals_Load(object sender, EventArgs e)
      {

          FillMuReferals();

          C1muHosp.Cols[2].AllowEditing = true;
        C1muHosp.Cols[0].DataType = typeof(bool); 
        C1muHosp.Cols[0].AllowEditing  = true;
       // C1muHosp.Cols["MuCheckBox"].c
        
      }

      private void FillMuReferals()
      {
        
         gloAddress.gloC1FlexStyle.Style(C1muHosp,false);
       //   dtRef = cmbRef.Copy () ;
          gloPatient objglopat = new gloPatient(_dbconnstring );
     if(dtHosp==null) 
          dtHosp=   objglopat.GetMUPatHosp(_PatientID);  
        //  gloPatient. 
          //ds = new DataSet();
   //    dsHosp.Tables.Add(dtHosp);
        //  C1muHosp.Visible = true;
     

          try
          {
              if (dtHosp != null)
              {
                  if (dtHosp.Rows.Count > 0)
                  {
                      DesignGrid();
                      foreach (DataRow dr in dtHosp.Rows)
                      {
                          C1muHosp.Rows.Add();
                          int row = C1muHosp.Rows.Count - 1;
                    
                          C1muHosp.SetData(row, COL_Checkbox, dr["chkStatus"]);
                          C1muHosp.SetData(row, COL_Name, dr["Description"]);
                          //C1muHosp.SetData(row, COL_Date,Convert.ToDateTime(dr["MuDate"]).ToString("yyyy/MM/dd HH:mm:ss"));
                          C1muHosp.SetData(row, COL_Date, dr["TransitionDate"]);
                          C1muHosp.SetData(row, COL_ID , dr["ContactID"]);
                          C1muHosp.SetData(row, COL_TableID, dr["ID"]);
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

      private void DesignGrid()
      {
          try
          {

              C1muHosp.Visible=true;
              C1muHosp.Rows.Fixed = 1;
              C1muHosp.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
              C1muHosp.Cols.Count = COL_COUNT;
              C1muHosp.Rows.Count = 1;

           //   dtHosp.TableName = "PatientHospital";
          //    dsHosp.Tables.Add(dtHosp);
              
              //C1MuReferals.DataSource=dtRef;

          //    C1muHosp.SetDataBinding(dsHosp, "PatientHospital", true);




              //C1muHosp.Cols[COL_ID].AllowEditing = false;
         
              //C1muHosp.Cols[COL_ID].Visible = false;
           
              C1muHosp.Cols[COL_Checkbox].AllowEditing = true;
              C1muHosp.Cols[COL_Checkbox].Caption = "Select";
              C1muHosp.Cols[COL_Checkbox].DataType = typeof(bool);
              C1muHosp.Cols[COL_Checkbox].Width = 60; 
              C1muHosp.Cols[COL_Name].AllowEditing = false;
              C1muHosp.Cols[COL_Name].Caption = "Name";
              C1muHosp.Cols[COL_Name].DataType = typeof(string);
              C1muHosp.Cols[COL_Name].Width = 300; 
              C1muHosp.Cols[COL_Date].AllowEditing = true;
              C1muHosp.Cols[COL_Date].Caption = "Date";
              C1muHosp.Cols[COL_Date].DataType = typeof(DateTime);
              C1muHosp.Cols[COL_Date].DataType = typeof(DateTime);
              C1muHosp.Cols[COL_Date].Format = "MM/dd/yyyy hh:mm tt";
              C1muHosp.Cols[COL_Date].Width = 150;
              C1muHosp.Cols[COL_ID].DataType = typeof(Int64);
              C1muHosp.Cols[COL_ID].DataType = typeof(Int64);
              C1muHosp.Cols[COL_ID].Width = 0;
              C1muHosp.Cols[COL_ID].Visible = false;


              C1muHosp.Cols[COL_TableID].DataType = typeof(Int64);
              C1muHosp.Cols[COL_TableID].DataType = typeof(Int64);
              C1muHosp.Cols[COL_TableID].Width = 0;
              C1muHosp.Cols[COL_TableID].Visible = false; 
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


          try
          {
              this.Focus();
              //if (dtpathosp != null)
              //{
              //    dtpathosp.Dispose();
              //    dtpathosp = null; 
              //}
           if(dtpathosp==null)
           {
              dtpathosp = new DataTable();
              dtpathosp.Columns.Clear();
              dtpathosp.Columns.Add("ContactID", typeof(long));
              dtpathosp.Columns.Add("chkStatus", typeof(bool));
              dtpathosp.Columns.Add("TransitionDate", typeof(DateTime));
              dtpathosp.Columns.Add("Description", typeof(string));
              dtpathosp.Columns.Add("ID", typeof(Int64));
           }
               C1muHosp.EndUpdate() ;
              C1muHosp.EndUpdate();
              C1muHosp.Refresh();
              dtpathosp.Rows.Clear();  
              for (int rowcnt = 1; rowcnt < C1muHosp.Rows.Count; rowcnt++)
              {
                  DataRow drnew = dtpathosp.NewRow();
                  
                  C1muHosp.Select(0, COL_Date );
                 
                  drnew["chkStatus"] = Convert.ToBoolean(C1muHosp.GetData(rowcnt, COL_Checkbox));
                  drnew["ContactID"] = Convert.ToInt64(C1muHosp.GetData(rowcnt, COL_ID));
                  drnew["TransitionDate"] = Convert.ToDateTime(C1muHosp.GetData(rowcnt, COL_Date));
                  drnew["Description"] = Convert.ToString(C1muHosp.GetData(rowcnt, COL_Name));
                  drnew["ID"] = Convert.ToString(C1muHosp.GetData(rowcnt, COL_TableID ));
                  dtpathosp.Rows.Add(drnew);
                 
              }
              dtpathosp.AcceptChanges();  
              _SaveData = true; 
              //gloPatient objglopat = new gloPatient(_dbconnstring);
              //objglopat.SaveMUPatHosp(_PatientID, dtpathosp);
              //dtpathosp.Dispose();
              this.Close();
          }
          catch
          {
          }
 
      }

      private void tls_btnCancel_Click(object sender, EventArgs e)
      {
          this.Close();

      }

      private void frmMUReferals_FormClosed(object sender, FormClosedEventArgs e)
      {
          
      }

    }

   
    

}
