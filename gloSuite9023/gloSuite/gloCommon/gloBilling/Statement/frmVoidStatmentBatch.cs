using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.Statement;
   

namespace gloBilling
{
    public partial class frmVoidStatmentBatch : Form
    {

        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;       
        
        private string _messageBoxcaption = "";
        private Int64 _MasterId = 0;
        private Int64 _DetailId = 0;
        private Boolean _IsBatchVoid;
        private Int64 _nPAccountID;



        #region "Property Procedure"
        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }
          
        #endregion 
        public frmVoidStatmentBatch()
        {
            InitializeComponent();
        }
        public frmVoidStatmentBatch(Int64 MasterID,Int64 DetailID,Boolean IsBatchVoid)
        {
            InitializeComponent();
            _MasterId = MasterID;
            _DetailId = DetailID;
            _IsBatchVoid = IsBatchVoid;
         
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxcaption = "gloPM";
                }
            }
            else
            { _messageBoxcaption = "gloPM"; }

            #endregion
             
        }

        private void frmVoidStatmentBatch_Load(object sender, EventArgs e)
        {
            txtNotes.Text = ""; 
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (txtNotes.Text.Trim() != "")
            {
                gloStatment objClsgloStatment = new gloStatment();
                DataTable dtPAccountID = new DataTable();
                if (_IsBatchVoid)
                {
                    objClsgloStatment.VoidBatch(_MasterId,txtNotes.Text.ToString());
                    dtPAccountID =  objClsgloStatment.GetBatchAccountsAndPatients(_MasterId);

                    if (dtPAccountID != null && dtPAccountID.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPAccountID.Rows.Count; i++)
                        {
                            objClsgloStatment.ResetStatementCount(Convert.ToInt64(dtPAccountID.Rows[i]["nPAccountID"]), DateTime.Now, false);
                            //Bug #68473: CR00000352 : RCM Queue issue
                            if (objClsgloStatment.GetStatementCount(Convert.ToInt64(dtPAccountID.Rows[i]["nPAccountID"])) == 0 && !(objClsgloStatment.GetIsPaymentPlan(Convert.ToInt64(dtPAccountID.Rows[i]["nPAccountID"]))))
                            {
                                Collections.CL_FollowUpCode.DeleteAccountFollowUp(Convert.ToInt64(dtPAccountID.Rows[i]["nPAccountID"]));
                            }

                        }
                    
                    }
                }
                else
                {
                    objClsgloStatment.VoidSingleStatment(_MasterId, _DetailId, txtNotes.Text.ToString());
                    objClsgloStatment.ResetStatementCount(PAccountID,DateTime.Now, false);
                    //Bug #68473: CR00000352 : RCM Queue issue
                    if (objClsgloStatment.GetStatementCount(PAccountID) == 0 && !(objClsgloStatment.GetIsPaymentPlan(PAccountID)))
                    {
                        Collections.CL_FollowUpCode.DeleteAccountFollowUp(PAccountID);
                    }

                }
                this.Close();  
            }
            else
            {
                MessageBox.Show("Enter valid notes.",_messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNotes.Focus();  
            }          

        }     
       

       
        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void frmVoidStatmentBatch_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Dispose(true);  
        }






    }
}
