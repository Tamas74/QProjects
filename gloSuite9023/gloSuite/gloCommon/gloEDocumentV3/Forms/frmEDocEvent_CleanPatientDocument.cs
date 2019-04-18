using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Data;
using System.Data.SqlClient;

namespace gloEDocumentV3.Forms
{
    public partial class frmEDocEvent_CleanPatientDocument : Form
    {
        public frmEDocEvent_CleanPatientDocument()
        {
            InitializeComponent();
        }
        public  Int64 oPatientID = 0;
        public  bool oDialogResultIsOK = false;
       
        
        
        private void frmEDocEvent_CleanPatientDocument_Load(object sender, EventArgs e)
        {
            FillPatientInfo();
            chkYear2007.Visible = false;
            if (gloEDocumentV3.eDocManager.eDocValidator.IsUserAdministrator(gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID) == true)
            {
            }
        }

        public void FillPatientInfo()
        {
            gloEDocumentV3.Database.DBLayer oDB = null; 
            DataTable dt;
            DataTable dtYears;
            try
            {
                oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                string _strQry = "SELECT sPatientCode, isnull(sFirstName,' ') +  ' ' + isnull(sMiddleName,' ') + ' ' + isnull(sLastName,' ') as PatientName, nSSN, dtDOB, sGender, sMaritalStatus, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sCounty, sPhone, sMobile, sEmail, sFAX, sOccupation, sEmploymentStatus, sPlaceofEmployment, sWorkAddressLine1, sWorkAddressLine2, sWorkCity, sWorkState, sWorkZIP, sWorkPhone, sWorkFAX, sChiefComplaints, nProviderID, nPCPId, sGuarantor, nPharmacyID, sSpouseName, sSpousePhone, dbo.fn_GetRaceEthnicity(" + oPatientID + ",'race','|') as sRace, sPatientStatus, PatientPhoto.iPhoto, dtRegistrationDate, dtInjuryDate, dtSurgeryDate, sHandDominance, sLocation, sMother_fName, sMother_mName, sMother_lName, sMother_Address1, sMother_Address2, sMother_City, sMother_State, sMother_ZIP, sMother_County, sMother_Phone, sMother_Mobile, sMother_FAX, sMother_Email, sFather_fName, sFather_mName, sFather_lName, sFather_Address1, sFather_Address2, sFather_City, sFather_State, sFather_ZIP, sFather_County, sFather_Phone, sFather_Mobile, sFather_FAX, sFather_Email, sGuardian_fName, sGuardian_mName, sGuardian_lName, sGuardian_Address1, sGuardian_Address2, sGuardian_City, sGuardian_State, sGuardian_ZIP, sGuardian_County, sGuardian_Phone, sGuardian_Mobile, sGuardian_FAX, sGuardian_Email, nPatientDirective, nExemptFromReport, sExternalCode, sUserName, sMachineName, sInsuranceNotes, bIsWorkersComp, sWorkersCompClaimNo, bIsAuto, sAutoClaimNo FROM Patient LEFT OUTER JOIN Patient_Photo PatientPhoto  WITH (NOLOCK) on Patient.nPatientID=PatientPhoto.nPatientID WHERE  Patient.nPatientID = " + oPatientID + "";
                oDB.Connect(false);
                dt = new DataTable();
                oDB.Retrive_Query(_strQry, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblPatientCode.Text = dt.Rows[0]["sPatientCode"].ToString().Trim();
                        lblPatientName.Text = dt.Rows[0]["PatientName"].ToString().Trim();
                    }
                    dt.Dispose();
                    dt = null;
                }

                //Sudhir 20090102
                #region " Fill Years "
                dtYears = new DataTable();

                _strQry = "";
                _strQry = "select distinct Year from dbo.eDocument_Details_V3 where PatientID = " + oPatientID + " ";
                oDB.Retrive_Query(_strQry, out dtYears);
                if (dtYears != null)
                {
                    if (dtYears.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtYears.Rows.Count - 1; i++)
                        {
                            chkListYears.Items.Add(dtYears.Rows[i]["Year"].ToString());
                        }
                    }
                    dtYears.Dispose();
                    dtYears = null;
                }

                #endregion

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool DeleteDocuments()
        {
            string _strQRY = "";
            bool _Result = true;
            gloEDocumentV3.Database.DBLayer oDB = null; 
            DataTable dt_Documents;
            try
            {
                //Changed by Rahul Patel on 27-10-2010
                //For Hybrid Database changes i.e changing the Connection string for DMS Database 
                //oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                oDB = new Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                dt_Documents = new DataTable();
                oDB.Connect(true);
                oDB.ErrorMessage = "";
                _strQRY = "SELECT eDocumentID, Year from eDocument_Details_V3 WHERE PatientID = " + oPatientID + " order by Year asc ";
                oDB.Retrive_Query(_strQRY, out dt_Documents);
                string Year = "";
                Int64 _DocumentID = 0;
                if (oDB.ErrorMessage != "")
                {
                    return _Result;
                }

                //sudhir 20090201
                if (dt_Documents != null)
                {
                    if (dt_Documents.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt_Documents.Rows.Count - 1; i++)
                        {
                            Year = dt_Documents.Rows[i]["Year"].ToString();
                            _DocumentID = Convert.ToInt64(dt_Documents.Rows[i]["eDocumentID"]);

                            for (int j = 0; j <= chkListYears.CheckedItems.Count - 1; j++)
                            {
                                if (chkListYears.CheckedItems[j].ToString() == Year)
                                {
                                    if (DeleteDocument(Year, _DocumentID) == false)
                                    {
                                        _Result = false;
                                    }
                                    break;
                                }
                            }                            
                        }

                    }
                    dt_Documents.Dispose();
                    dt_Documents = null;
                }

                oDB.Disconnect();

                return _Result;

            }
            catch (Exception ex)
            {
                _Result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                throw;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            //return _Result;
        }
        private bool DeleteDocument(string Year, Int64 oDocumentID)
        {
            bool Result = false;
            string _strQRY = "";
            _strQRY = "DELETE FROM eDocument_NTAO_V3 WHERE eDocumentID = " + oDocumentID + "";
            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                _strQRY = "";
                _strQRY = "DELETE  FROM eDocument_Container_V3 WHERE eDocumentID =  " + oDocumentID + "";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                _strQRY = "";
                _strQRY = "DELETE  FROM eDocument_Details_V3 WHERE eDocumentID =  " + oDocumentID + "";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                _strQRY = "";
                _strQRY = "DELETE  FROM eDocument_Pages_V3 WHERE eDocumentID =  " + oDocumentID + "";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                _strQRY = "";
                if (Year == "2007")
                {
                    _strQRY = "UPDATE DMS_MST SET IsMigrated = 'false' WHERE PatientID = " + oPatientID + " AND (Year = '2006' OR Year = '2007')";
                    oDB.Execute_Query(_strQRY);
                    if (oDB.ErrorMessage != "")
                    {
                        return Result;
                    }

                }
                else
                {
                    _strQRY = "UPDATE DMS_MST SET IsMigrated = 'false' WHERE PatientID = " + oPatientID + " AND Year = '" + Year + "'";
                    oDB.Execute_Query(_strQRY);
                    if (oDB.ErrorMessage != "")
                    {
                        return Result;
                    }

                }

                // Sudhir 20090110 -- To Update Task Entries from Task Master //
                _strQRY = "";
                _strQRY = "UPDATE Tasks_Mst SET sFAXTIFFFileName = NULL WHERE sFAXTIFFFileName = '" + Year + "," + oDocumentID + "' AND nPatientID = " + oPatientID + "";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                // Sudhir 20090110 -- To Update Lab Order Test Details //
                _strQRY = "";
                _strQRY = "UPDATE Lab_Order_TestDtl SET labotd_DMSID = 0 WHERE labotd_OrderID IN (SELECT labom_OrderID FROM Lab_Order_MST WHERE labom_PatientID = " + oPatientID + ") AND labotd_DMSID=" + oDocumentID + "";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }

                // Sudhir 20090110 -- To Update LM_Order Details //
                _strQRY = "";
                _strQRY = "UPDATE LM_Orders SET lm_DMSID = 0 WHERE lm_Order_ID IN (SELECT lm_Order_ID FROM LM_Orders WHERE lm_Patient_ID = " + oPatientID + ") AND lm_DMSID = '" + oDocumentID + "'";
                oDB.Execute_Query(_strQRY);
                if (oDB.ErrorMessage != "")
                {
                    return Result;
                }
            }
            catch
            {
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            Result = true;
            return Result;
        }
        private void tlb_Ok_Click(object sender, EventArgs e)
        {

            if (chkListYears.CheckedItems.Count > 0)
            {
                MessageBox.Show("This will clear all the document for selected patient.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBox.Show("Are you sure you want to clear all document for selected patients?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DeleteDocuments() == true)
                    {
                        oDialogResultIsOK = true;
                        this.Close();
                    }
                    else
                    {
                        oDialogResultIsOK = false;
                        MessageBox.Show("Error While Deleting Document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select year.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}