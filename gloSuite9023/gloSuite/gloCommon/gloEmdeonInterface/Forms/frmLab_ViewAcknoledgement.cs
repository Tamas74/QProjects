using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEMRGeneralLibrary.gloEMRLab;

namespace gloEmdeonInterface.Forms
{
    public partial class frmLab_ViewAcknoledgement : Form
    {

        private long OrderId;
        private string OrderNumberPrefix;
        private long OrderNumberID;
        public string _ViewedDocumentPath = "";
        public string _ViewedDocumentDispName = "";
        private string _connString = string.Empty;

        // by Abhijeet on 20100514 declared variable
        string gstrMessageBoxCaption = string.Empty;
        //commented by madan on 20100520
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //end of changes  by Abhijeet on 20100514

        //by Abhijeet on 20100626 ,create & declare Patient ID property to assign/access from outside  
        long _patientID = 0;
        public long PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }
        // End of changes by Abhijeet on 20100626 for an Patient ID property

        public frmLab_ViewAcknoledgement(long _OrderID, String _OrderNumberPrefix, long _OrderNumberID, String ConnectionString)
        {
            OrderId = _OrderID;
            OrderNumberPrefix = _OrderNumberPrefix;
            OrderNumberID = _OrderNumberID;
            _connString = ConnectionString;

            // Code by : Abhijeet Farkande on date 20100514
            // changes : Message box caption is accessing
            if (appSettings != null)
            {
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {
                gstrMessageBoxCaption = "gloEMR";
            }
            // End of code for accessing messagebox caption               

            InitializeComponent();
        }

        private void frmLab_ViewAcknoledgement_Load(object sender, EventArgs e)
        {

            //Dim _DocumentPath As String
            
                try
                {
                    //by Abhijeet on 20100430                   
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "view Acknowledgement", PatientID, OrderId,0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    txtFileName.Text = OrderNumberPrefix + "-" + OrderNumberID;
                    //Panel2.Text = ""
                    //dtpReviwed.Value = Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt")

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);

                    DataTable dtAcw = new DataTable();
                   oDB.Connect(false);

                 oDB.Retrive_Query("SELECT nUserID,ReviewDatetime,Comments FROM Lab_Acknowledgment Where nOrderId = " + OrderId + " and  nOrderNumberPrefix = '" + OrderNumberPrefix + "' and nOrderNumberID = " + OrderNumberID + " ",out dtAcw);
                    oDB.Disconnect();
                    oDB = null;


                    //cmbUsers.Items.Clear()


                    oDB = new gloDatabaseLayer.DBLayer(_connString);
                    string strLoginName = null;
                    oDB.Connect(false);
                    strLoginName = oDB.ExecuteScalar_Query( "SELECT sLoginName FROM User_MST WHERE  nUserID = " + dtAcw.Rows[0]["nUserID"].ToString() + "").ToString();

                    oDB.Disconnect();

                    //cmbUsers.Items.Add(strLoginName)
                    //cmbUsers.SelectedIndex = 0
                    txtuser.Text = strLoginName;
                    //dtpReviwed.Value = Format(dtAcw.Rows(0)("ReviewDatetime"), "MM/dd/yyyy hh:mm:ss tt")
                    //txtReviewed.Text = Strings.Format(dtAcw.Rows[0]["ReviewDatetime"], "MM/dd/yyyy hh:mm:ss tt");
                    txtReviewed.Text = dtAcw.Rows[0]["ReviewDatetime"].ToString(); 

                    txtComments.Text = dtAcw.Rows[0]["Comments"].ToString();
                }
                catch (Exception ex)
                {


                    //4. Progress Bar 
                    //ProgressBar1.Minimum = 0
                    //ProgressBar1.Maximum = 100
                    //ProgressBar1.Value = 0
                    //ProgressBar1.Enabled = False

                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
            


        }

        private void tlsp_Lab_ViewAcknoledgement_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            
                try
                {
                    switch (e.ClickedItem.Tag.ToString())
                    {
                        case "Ok":
                            this.Close();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK);
                }
            

        }
    }
}