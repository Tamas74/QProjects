using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class FrmMatchConfirmation : Form
    {
        // This form is created by Abhijeet
        // Purpose : for taking confrimation from user to match patient.
        // It display the demographic information of Unmatch patient & patient which is selected to match .
        // for patient selected to match it shows the information in red color if it not match with ifnormation of unmatch patient 

     //   private long _lngUnmatchPatientID = 0;-- Commeted by madan on 20100520-- To remove the warnings.
        private long _lngSelectedPatientID = 0;
        private string _strDBConnectionString = "";
        private long _lngTaskID = 0;
        private bool blnIgnoreCase = true;
        // property which keep the track of user accept matching or not 
        private Boolean _boolMatchPatient = false;
        public Boolean MatchPatient
        {
            get { return _boolMatchPatient; }
            set { _boolMatchPatient = value; }
        }


        public FrmMatchConfirmation(long TaskID ,long SelectedPatientID,string DBConnectionString)
        {
           // _lngUnmatchPatientID = UnmatchPatientID;
            _lngTaskID = TaskID;
            _lngSelectedPatientID = SelectedPatientID;
            _strDBConnectionString = DBConnectionString;
            InitializeComponent();            
        }

        private void DisplayPatientDetails(long UnmatchPatientID, long SelectedPatientID)
        {   // function used to diaplay the patients information

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_strDBConnectionString);
            System.Data.SqlClient.SqlDataReader dr;
            try
            {
             //commented this below code to fix the warnings.   
               // string strConfirmMessage = "";
                oDB.Connect(false);               
                // oDB.Retrive_Query("Select * from patient where npatientid=" + SelectedPatientID.ToString(), out dr);   //Remove select *
                oDB.Retrive_Query("Select  nPatientID, sFirstName, sMiddleName, sLastName, nSSN, sGender, dtDOB from patient where npatientid=" + SelectedPatientID.ToString(), out dr); 
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        
                        if (SelectedPatientID == Convert.ToInt64(dr["nPatientID"]))
                        {
                            lblSelectedPatientFNameValue.Text = Convert.ToString(dr["sFirstName"]).Trim();
                            lblSelectedPatientMNameValue.Text = Convert.ToString(dr["sMiddleName"]).Trim();
                            lblSelectedPatientLNameValue.Text = Convert.ToString(dr["sLastName"]).Trim();
                            lblSelectedPatientDOBValue.Text = Convert.ToString(dr["dtDOB"]).Trim();
                            lblSelectedPatientGenderValue.Text = Convert.ToString(dr["sGender"]).Trim();
                            lblSelectedPatientSSNValue.Text = Convert.ToString(dr["nSSN"]).Trim();
                        }                       
                     }
                }
                oDB.Disconnect();

                System.Data.SqlClient.SqlDataReader drTask;
                oDB.Connect(false);
                //oDB.Retrive_Query("Select * from PatientsUnmatchedInLab where nTaskid=" + _lngTaskID.ToString(), out drTask); //Remove select *
                oDB.Retrive_Query("Select  nTaskId, sFirstName, sMiddleName, sLastName, dtDOB, nSSN, sGender  from PatientsUnmatchedInLab where nTaskid=" + _lngTaskID.ToString(), out drTask);
                if (drTask.HasRows)
                {
                    while (drTask.Read())
                    {
                        if (_lngTaskID == Convert.ToInt64(drTask["nTaskID"]))
                        {
                            lblUnmatchPatientFNameValue.Text = Convert.ToString(drTask["sFirstName"]).Trim();
                            lblUnmatchPatientMNameValue.Text = Convert.ToString(drTask["sMiddleName"]).Trim();
                            lblUnmatchPatientLNameValue.Text = Convert.ToString(drTask["sLastName"]).Trim();
                            lblUnmatchPatientDOBValue.Text = Convert.ToString(drTask["dtDOB"]).Trim();
                            lblUnmatchPatientGenderValue.Text = Convert.ToString(drTask["sGender"]).Trim();
                            lblUnmatchPatientSSNValue.Text = Convert.ToString(drTask["nSSN"]).Trim();                                                       
                        }                                               
                    }
                }
                oDB.Disconnect();

                CheckLabels();

                //if (lblUnmatchPatientFNameValue.Text != lblSelectedPatientFNameValue.Text)
                //    lblSelectedPatientFNameValue.ForeColor = Color.Red;
                //if (lblUnmatchPatientMNameValue.Text != lblSelectedPatientMNameValue.Text)
                //    lblSelectedPatientMNameValue.ForeColor = Color.Red;
                //if (lblUnmatchPatientLNameValue.Text != lblSelectedPatientLNameValue.Text)
                //    lblSelectedPatientLNameValue.ForeColor = Color.Red;
                //if (lblUnmatchPatientDOBValue.Text != lblSelectedPatientDOBValue.Text)
                //    lblSelectedPatientDOBValue.ForeColor = Color.Red;
                //if (lblUnmatchPatientGenderValue.Text != lblSelectedPatientGenderValue.Text)
                //    lblSelectedPatientGenderValue.ForeColor = Color.Red;
                //if (lblUnmatchPatientSSNValue.Text != lblSelectedPatientSSNValue.Text)
                //    lblSelectedPatientSSNValue.ForeColor = Color.Red;

                //by Abhijeet on 20100430
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Display un match patient and selected patient to be match details", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
            catch(Exception ex)
            {
                // clsGeneral objclsGen = new clsGeneral();
                // objclsGen.UpdateLog(" Error in displaying patient Details for confirmation : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(" Error in displaying patient Details for confirmation : " + ex.ToString(), false); 
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

        private void CheckLabels()
        {
            SetLabelColor(lblUnmatchPatientFNameValue, lblSelectedPatientFNameValue);
            SetLabelColor(lblUnmatchPatientMNameValue, lblSelectedPatientMNameValue);
            SetLabelColor(lblUnmatchPatientLNameValue, lblSelectedPatientLNameValue);
            SetLabelColor(lblUnmatchPatientDOBValue, lblSelectedPatientDOBValue);
            SetLabelColor(lblUnmatchPatientGenderValue, lblSelectedPatientGenderValue);
            SetLabelColor(lblUnmatchPatientSSNValue, lblSelectedPatientSSNValue);
        }

        private void SetLabelColor(Label lbl1, Label lbl2)
        {
            if (string.Compare(lbl1.Text, lbl2.Text, blnIgnoreCase) != 0)
            { lbl2.ForeColor = Color.Red; }
            else
            { lbl2.ForeColor = Color.Green; }
        }

        private void FrmMatchConfirmation_Load(object sender, EventArgs e)
        {
           // Making all label value blank
            lblUnmatchPatientFNameValue.Text = "";
            lblUnmatchPatientMNameValue.Text = "";
            lblUnmatchPatientLNameValue.Text = "";
            lblUnmatchPatientDOBValue.Text = "";
            lblUnmatchPatientGenderValue.Text = "";
            lblUnmatchPatientSSNValue.Text="";
            lblSelectedPatientFNameValue.Text = "";
            lblSelectedPatientMNameValue.Text = "";
            lblSelectedPatientLNameValue.Text = "";
            lblSelectedPatientDOBValue.Text = "";
            lblSelectedPatientGenderValue.Text = "";
            lblSelectedPatientSSNValue.Text = ""; 

            // Displaying patient details
            //DisplayPatientDetails(_lngUnmatchPatientID, _lngSelectedPatientID);
            DisplayPatientDetails(_lngTaskID, _lngSelectedPatientID);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          //  this.Close();
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
                        
            if (e.ClickedItem.Name == tlbbtn_save.Name)
            {                
                    MatchPatient = true;
                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "confirm operation to map un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                    this.Close();
            }
            else 
            {                          
                    MatchPatient = false;
                    //by Abhijeet on 20100430
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Did not confirm operation to map un match patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    this.Close();
            }

        }

        private void chkIgnoreCase_CheckedChanged(object sender, EventArgs e)
        {
            blnIgnoreCase = chkIgnoreCase.Checked;
            CheckLabels();
        }

    }
}