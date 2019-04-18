using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloCMSEDI
{
    public partial class frmPrintCMS1500 : Form
    {
        #region "Private Variable"
        string _databaseconnectionstring="";
        string _messageboxcaption="";
        Int64 _ClinicID=0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private bool _IsAllowPrinting = true;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 
        public bool IsAllowPrinting
        {
            get { return _IsAllowPrinting; }
            set { _IsAllowPrinting = value; }
        }
        #endregion
       
        #region" Constructor "

          public frmPrintCMS1500(string DatabaseConnection)
        {
             InitializeComponent();
             lblMessage.Text = "In a recent system upgrade the layout for the CMS1500 was modified to provide a clearer printing" + Environment.NewLine + "resolution and slightly larger font size.   Due to this, you must review the quality of the print out and" + Environment.NewLine + "confirm that the new prints outs look correct with each data element appearing properly its box." + Environment.NewLine + Environment.NewLine + "Have you confirmed this yet?";
            _databaseconnectionstring = DatabaseConnection;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }
        }

        #endregion 

        #region " Form Load "

      private void DeletePrintFromUpdateMessage()
      {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                object  _value = 0;
                String Query="";
                Query="Delete From settings where sSettingsName ='PrintFromUpdateMessage'";
                oDB.Connect(false);
                oDB.Execute_Query(Query);
  
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            
      }


        #endregion " Form Load "

      private void btnContinue_Click(object sender, EventArgs e)
      {
          if (rbFormPrintProper.Checked)
          {
              DeletePrintFromUpdateMessage();
              IsAllowPrinting = true;
          }
          else if (rbYetToCheck.Checked)
          {
              IsAllowPrinting = true;
          }
          else if (rbProblemforPrint.Checked)
          {
              MessageBox.Show("The batch will not print.  Print Cancelled. " + Environment.NewLine + "Contact your practice administrator and/or your gloStream vendor. " + Environment.NewLine + "There is a method for aligning the data to fit inside the form boxes. " + Environment.NewLine + "Please make the needed corrections and then retry the print. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
              IsAllowPrinting = false;


          }
          else
          {
              MessageBox.Show("Please Select One Option. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
          }
          this.Close();  
      }
      public  bool GetDefaultCoordinateUpdateSetting()
      {
      
          gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
          bool _Result = false;
          try
          {

              object _value = 0;
              String Query = "";
              Query = "Select Count(sSettingsValue) as SettingsValue from settings  WITH(NOLOCK) where sSettingsName='PrintFromUpdateMessage'";
              oDB.Connect(false);

              _value = oDB.ExecuteScalar_Query(Query);
              if (Convert.ToInt64(_value) > 0)
                  _Result = true;
              else
                  _Result = false;
          }
          catch (Exception ex)
          {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
          }
          finally
          {
              if (oDB != null)
              {
                  oDB.Disconnect();
                  oDB.Dispose();
              }
          }
          return _Result;
      }

      private void rbFormPrintProper_CheckedChanged(object sender, EventArgs e)
      {
          if (rbFormPrintProper.Checked)         
              rbFormPrintProper.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold); 
          else
              rbFormPrintProper.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular); 
      }

      private void rbYetToCheck_CheckedChanged(object sender, EventArgs e)
      {
          if (rbYetToCheck.Checked)
              rbYetToCheck.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
          else
              rbYetToCheck.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular); 

      }

      private void rbProblemforPrint_CheckedChanged(object sender, EventArgs e)
      {
          if (rbProblemforPrint.Checked)
              rbProblemforPrint.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
          else
              rbProblemforPrint.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular); 
      }
     
    }
}