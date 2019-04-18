using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gloPatient.Classes;


namespace gloPatient
{   
    public partial class frmvwRecentPatient : Form
    {
        //enum GetWindow_Cmd : uint
        //{
        //    GW_HWNDFIRST = 0,
        //    GW_HWNDLAST = 1,
        //    GW_HWNDNEXT = 2,
        //    GW_HWNDPREV = 3,
        //    GW_OWNER = 4,
        //    GW_CHILD = 5,
        //    GW_ENABLEDPOPUP = 6
        //}
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        //[DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //static extern long GetWindowText(IntPtr hwnd, StringBuilder lpString, long cch);
        //[DllImport("user32.dll", EntryPoint = "GetWindowText",
        // CharSet = CharSet.Auto)]
        //static extern IntPtr GetWindowCaption(IntPtr hwnd,
        //  StringBuilder lpString, int maxCount);
        private string _dbconnstring = "";
        private Int64 _UserID = 0;
        private DataTable dtrecentpat = null;
        public delegate void onRecpatientClick(Int64 PatientID);
        public event onRecpatientClick onRecpatientEventclick; //(object sender, RoutedEventArgs e);
     
       
         public bool timeset = false;
        
        public frmvwRecentPatient()
        {
            InitializeComponent();
        }

        public frmvwRecentPatient(string DbConnecstring, Int64 UserID)
        {
            
            
           
            InitializeComponent();
            this.Text = "Recent Patients";
            _dbconnstring = DbConnecstring;
            _UserID = UserID;
        }

        
        private void frmvwRecentPatient_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.4; 
            gloPatient objglopat = new gloPatient(_dbconnstring);
            C1viewPat.AllowEditing = false; 
           
            dtrecentpat = objglopat.GetRecentPatient(_UserID);
                C1viewPat.DataSource = dtrecentpat;
                C1viewPat.Cols[1].Visible = false;
                objglopat.Dispose();
                objglopat = null; 

        }

        

        private void C1viewPat_DoubleClick(object sender, EventArgs e)
        {
            if (C1viewPat.RowSel >= 0)
            {
               
                    Int64 PatientID = Convert.ToInt64(C1viewPat.Rows[C1viewPat.RowSel][1]);
             

                    //IntPtr handl = GetWindow(this.Handle, (uint)GetWindow_Cmd.GW_OWNER);

                    //string str = GetWindowCaption(handl);
                    //if(str.Trim()=="QEMR") 
                    onRecpatientEventclick(PatientID);
                
                    this.Close();
            }
        }

        //static string GetWindowCaption(IntPtr hwnd)
        //{
        //    StringBuilder sb = new StringBuilder(256);
        //    GetWindowCaption(hwnd, sb, 256);
        //    return sb.ToString();
        //}
        private void frmvwRecentPatient_Shown(object sender, EventArgs e)
        {
            this.Opacity = 1;
            
        }

      

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void C1viewPat_MouseMove(object sender, MouseEventArgs e)
        {
      
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }
    }
}
