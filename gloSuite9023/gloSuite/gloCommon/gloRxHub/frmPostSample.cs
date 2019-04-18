using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloRxHub
{
    public partial class frmSamplePost : Form
    {
        public frmSamplePost()
        {
            InitializeComponent();
        }

        public string strFileName ;
        public int MethodType = 1;
        public string strOutputFile;

        //sarika Automate rx Eligibility request
        public bool  blnIsAutomated = false;


        //Post EDI
        public void Button1_Click(object sender, EventArgs e)
        {
            ClsRxHubInterface  objRxHubInterface =   new ClsRxHubInterface();
          
            try
            {
                objRxHubInterface.PostEDIFile(strFileName, 1, 0);
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

        }


        public void Button2_Click(object sender, EventArgs e)
        {
            ClsRxHubInterface objRxHubInterface = new ClsRxHubInterface();

            try
            {
                for (int i = 0; i <= 4; )
                {
                    strOutputFile = objRxHubInterface.PostRxHistoryRequest(strFileName);
                    if (strOutputFile != "")
                    {
                        this.Close();
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                               
        
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }


        public void Button3_Click(object sender, EventArgs e)
        {
            ClsRxHubInterface objRxHubInterface = new ClsRxHubInterface();

            try
            {
               Int16 rbType = 0;
                if (rbType1.Checked == true )
                {
                    rbType = 1;
                }
                else if(rbType2.Checked == true )
                {
                    rbType = 2;
                }
                else if(rbType3.Checked == true )
                {
                    rbType = 3;
                }
                else if(rbType4.Checked == true )
                {
                    rbType = 4;
                }

            
                if (rbType4.Checked == true )
                {
                    for (int i = 0; i <= 4; )
                    {
                        strOutputFile = objRxHubInterface.PostEDIFile_New(strFileName, 0, rbType);
                        if (strOutputFile != "")
                        {
                            this.Close();
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }

                  
                }
                else
                {
                    strOutputFile = objRxHubInterface.PostEDIFile(strFileName, 2,rbType);
                }

                //sarika automate rx eligibilty request 20090710

                // MessageBox.Show("Eligibility response has been received successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (blnIsAutomated == false)

                {
                    //MessageBox.Show("Eligibility response has been received successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //updatelog
                    //clsRxGeneral.UpdateLogForRx("Eligibility response has been received successfully.");
                }
                //---

                this.Close();
                //objRxHubInterface.ExtractText(
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        public void rbType1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbType1.Checked == true )
            {
                rbType1.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbType1.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
             
        }
    }
}