using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmModifyAuthorization : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;      
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "
        
        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
     
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmModifyAuthorization(string databaseConnectionString)
        {
            _databaseconnectionstring = databaseConnectionString;                       
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
             InitializeComponent();

             #region " Retrieve MessageBoxCaption from AppSettings "

             if (appSettings["MessageBOXCaption"] != null)
             {
                 if (appSettings["MessageBOXCaption"] != "")
                 {
                     _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                 }
                 else
                 {
                     _messageBoxCaption = "gloPM";
                 }
             }
             else
             { _messageBoxCaption = "gloPM"; }

             #endregion
         }

        #endregion " Constructor "

        #region " Form Load "

          private void frmSetupAuthorization_Load(object sender, EventArgs e)
        {
            try
            {
                SetData();                              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

         #endregion " Form Load "

        #region " Tool Strip Event "

      

        #endregion " Tool Strip Event "

        #region " Private Methods "

          private void DesignGrid()
          {
              try
              {

                  c1ProirAuthorization.Cols["nPriorAuthorizationID"].Visible = false;
                  c1ProirAuthorization.Cols["sPriorAuthorizationNo"].Visible = true;
                  c1ProirAuthorization.Cols["nPatientID"].Visible = false;
                  c1ProirAuthorization.Cols["nReferralID"].Visible = false;
                  c1ProirAuthorization.Cols["sReferralName"].Visible = true;
                  c1ProirAuthorization.Cols["sInusrance"].Visible = true;
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Visible = true;
                  c1ProirAuthorization.Cols["nStartDate"].Visible = true;
                  c1ProirAuthorization.Cols["nExpDate"].Visible  = true;
                  c1ProirAuthorization.Cols["nVisitsAllowed"].Visible = true;
                 
                  #region " Set Header "
                  
                  c1ProirAuthorization.Cols["nPriorAuthorizationID"].Caption = "";
                  c1ProirAuthorization.Cols["sPriorAuthorizationNo"].Caption = "Auth#";
                  c1ProirAuthorization.Cols["nPatientID"].Caption = "";
                  c1ProirAuthorization.Cols["nReferralID"].Caption  = "";
                  c1ProirAuthorization.Cols["sReferralName"].Caption  = "Referring";                
                  c1ProirAuthorization.Cols["Insurance"].Caption = "Insurance";
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Caption = "Note";
                  c1ProirAuthorization.Cols["nStartDate"].Caption = "Start";
                  c1ProirAuthorization.Cols["nExpDate"].Caption = "End";
                  c1ProirAuthorization.Cols["nVisitsAllowed"].Caption = "Visits Remain";

                  #endregion

                  int _nWidth = 0;
                  _nWidth = 826;//Convert.ToInt32( c1QueuedClaims.Width);
                  c1ProirAuthorization.Cols["nPriorAuthorizationID"].Width = 0;
                  c1ProirAuthorization.Cols["sPriorAuthorizationNo"].Width = Convert.ToInt32(_nWidth * 0.10);

                  c1ProirAuthorization.Cols["sReferralName"].Width = Convert.ToInt32(_nWidth * 0.10);

                  c1ProirAuthorization.Cols["Insurance"].Width = Convert.ToInt32(_nWidth * 0.10);

                  c1ProirAuthorization.Cols["PatientName"].Width = Convert.ToInt32(_nWidth * 0.15);
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Width = Convert.ToInt32(_nWidth * 0.25);

                  c1ProirAuthorization.Cols["nStartDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                  c1ProirAuthorization.Cols["nExpDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                  c1ProirAuthorization.Cols["nVisitsAllowed"].Width = Convert.ToInt32(_nWidth * 0.10);
              
              }
              catch (Exception ex)
              {

              }
          }

        private void GetPriorAuthorizations(Int64 AuthorizationID)
        {

            try
            {
             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
                    
        }

        private void SetData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPrior = new DataTable();
            try
            {
          
               // gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();                
                oDB.Retrive("GET_PriorAuthorization", out dtPrior);
                c1ProirAuthorization.DataSource = dtPrior.DefaultView;
                DesignGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
            }
        }

        private bool Validate()
        {
            try
            {
                return true;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;                
            }
            finally
            { 
                
            }
        }

        #endregion " Private Methods "

      

        private void frmModifyAuthorization_Load(object sender, EventArgs e)
        {

        }

        private void dtAuthorizationThroughDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

    

    }
}