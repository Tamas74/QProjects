using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPatient
{
    public partial class frmPatientChangeHistory : Form
    {
        private Int64  _patientID = 0;
        //private string _messageboxcaption = "gloPM";
        private string _messageboxcaption = String.Empty;
       
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        public frmPatientChangeHistory(Int64 PatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            _patientID = PatientID;


            //Added By Pramod Nair For Messagebox Caption 
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

            #endregion          

        }

        private void frmPatientChangeHistory_Load(object sender, EventArgs e)
        {
            if (_patientID > 0)
            {
                FillPatientHistory();
                //Try
                try
                {
                    String gstrMessageBoxCaption ="";
                    if (appSettings["MessageBOXCaption"] != null)
                    {
                        if (appSettings["MessageBOXCaption"] != "")
                        {
                            gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                        }
                        else
                        {
                            gstrMessageBoxCaption = "gloPM";
                        }
                    }
                    else
                    { gstrMessageBoxCaption = "gloPM"; }

                    gloPatient.GetWindowTitle(this, _patientID, _databaseconnectionstring, gstrMessageBoxCaption);
                }
                catch (Exception ex)
                {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),false );
                }
            //Catch ex As Exception
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            //End Try
            }
        }

        private void FillPatientHistory()
        {
            gloPatient oPatientTrans = new gloPatient(_databaseconnectionstring);
            Patient oPatient;
            DataTable dtHistory;
            try
            {
                
                dtHistory = oPatientTrans.GetPatientChangeHistory(_patientID);
                oPatient = oPatientTrans.GetPatientDemo(_patientID);   
                if (dtHistory != null)
                {
                    lblPatientName.Text = oPatient.DemographicsDetail.PatientFirstName + " " + 
                                            oPatient.DemographicsDetail.PatientMiddleName + " " + 
                                            oPatient.DemographicsDetail.PatientLastName;
                    lblPatientCode.Text = oPatient.DemographicsDetail.PatientCode;
   
                    dgPatientHistory.DataSource = dtHistory.DefaultView;
                    //nPatientID, sFirstName, sMiddleName, sLastName, dtDOB, sGender,    
                    //sAddressLine1, sAddressLine2, sCity, sState, sZIP, sCounty, sPhone, dtChangeDateTime

                    dgPatientHistory.Columns["nPatientID"].Visible = false;

                    dgPatientHistory.Columns["sFirstName"].HeaderText = "First Name";
                    dgPatientHistory.Columns["sMiddleName"].HeaderText = "Middle Name";
                    dgPatientHistory.Columns["sLastName"].HeaderText = "Last Name";
                    dgPatientHistory.Columns["dtDOB"].HeaderText = "Date of Birth";
                    dgPatientHistory.Columns["sGender"].HeaderText = "Gender";
                    dgPatientHistory.Columns["sAddressLine1"].HeaderText = "Address 1";
                    dgPatientHistory.Columns["sAddressLine2"].HeaderText = "Address 2";
                    dgPatientHistory.Columns["sCity"].HeaderText = "City";
                    dgPatientHistory.Columns["sState"].HeaderText = "State";
                    dgPatientHistory.Columns["sZIP"].HeaderText = "Zip";
                    dgPatientHistory.Columns["sCounty"].HeaderText = "County";
                    dgPatientHistory.Columns["sPhone"].HeaderText = "Phone";
                    dgPatientHistory.Columns["dtChangeDateTime"].HeaderText = "Date";
                    dgPatientHistory.Columns["sLoginName"].HeaderText = "User"; //added on 22/04/2010
                    dgPatientHistory.Columns["sReferrals"].HeaderText = "Referrals"; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sGuarantors"].HeaderText = "Account Guarantor"; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["OtherGuarantors"].HeaderText = "Other Guarantors"; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sCareTeam"].HeaderText = "Care Team Member"; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPharmacy"].HeaderText = "Pharmacy"; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPhysicians"].HeaderText = "Physicians"; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sInsuranceName"].HeaderText = "Insurance Name"; // added on 29/07/2016 by juily
                    
                    dgPatientHistory.Columns["dtChangeDateTime"].DisplayIndex = 0;
                    dgPatientHistory.Columns["sFirstName"].DisplayIndex  = 1;
                    dgPatientHistory.Columns["sMiddleName"].DisplayIndex = 2;
                    dgPatientHistory.Columns["sLastName"].DisplayIndex = 3;
                    dgPatientHistory.Columns["dtDOB"].DisplayIndex = 4;
                    dgPatientHistory.Columns["sGender"].DisplayIndex = 5;
                    dgPatientHistory.Columns["sAddressLine1"].DisplayIndex = 6;
                    dgPatientHistory.Columns["sAddressLine2"].DisplayIndex = 7;
                    dgPatientHistory.Columns["sCity"].DisplayIndex = 8;
                    dgPatientHistory.Columns["sState"].DisplayIndex = 9;
                    dgPatientHistory.Columns["sZIP"].DisplayIndex = 10;
                    dgPatientHistory.Columns["sCounty"].DisplayIndex = 11;
                    dgPatientHistory.Columns["sPhone"].DisplayIndex = 12;
                    dgPatientHistory.Columns["sLoginName"].DisplayIndex = 13;  //added on 22/04/2010
                    dgPatientHistory.Columns["sReferrals"].DisplayIndex = 14; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sGuarantors"].DisplayIndex = 15; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["OtherGuarantors"].DisplayIndex = 16; // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sCareTeam"].DisplayIndex = 17; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPharmacy"].DisplayIndex = 18; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPhysicians"].DisplayIndex  = 19; // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sInsuranceName"].DisplayIndex  = 20; // added on 29/07/2016 by juily

                    int _width = pnlChangeHistory.Width;
                    dgPatientHistory.Columns["dtChangeDateTime"].Width = Convert.ToInt32(_width * 0.2);
                    dgPatientHistory.Columns["sFirstName"].Width = Convert.ToInt32(_width * 0.12);
                    dgPatientHistory.Columns["sMiddleName"].Width = Convert.ToInt32(_width * 0.12);
                    dgPatientHistory.Columns["sLastName"].Width = Convert.ToInt32(_width * 0.12);
                    dgPatientHistory.Columns["dtDOB"].Width = Convert.ToInt32(_width * 0.12);
                    dgPatientHistory.Columns["sGender"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sAddressLine1"].Width = Convert.ToInt32(_width * 0.15);
                    dgPatientHistory.Columns["sAddressLine2"].Width = Convert.ToInt32(_width * 0.15);
                    dgPatientHistory.Columns["sCity"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sState"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sZIP"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sCounty"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sPhone"].Width = Convert.ToInt32(_width * 0.1);
                    dgPatientHistory.Columns["sLoginName"].Width = Convert.ToInt32(_width * 0.12);  //added on 22/04/2010
                    dgPatientHistory.Columns["sReferrals"].Width = Convert.ToInt32(_width * 0.15); // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sGuarantors"].Width = Convert.ToInt32(_width * 0.15); // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["OtherGuarantors"].Width = Convert.ToInt32(_width * 0.15); // added on 28/07/2016 by juily
                    dgPatientHistory.Columns["sCareTeam"].Width = Convert.ToInt32(_width * 0.15); // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPharmacy"].Width = Convert.ToInt32(_width * 0.15); // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sPhysicians"].Width  = Convert.ToInt32(_width * 0.15); // added on 29/07/2016 by juily
                    dgPatientHistory.Columns["sInsuranceName"].Width = Convert.ToInt32(_width * 0.15); // added on 29/07/2016 by juily

                    //Merged from 5076.
                    dgPatientHistory.Columns["dtChangeDateTime"].DefaultCellStyle.Format = "MM/dd/yyyy";
                    dgPatientHistory.Columns["dtDOB"].DefaultCellStyle.Format = "MM/dd/yyyy";
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Close":
                        this.Close(); 
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       
        
    }
}