using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloPatient;
using C1.Win.C1FlexGrid;
using gloGlobal; 

namespace gloPM
{
    public partial class frmAddReferrals : Form 
    {
        #region "Variable Declarations"
        private Int64 _PatientId = 0;
        private Int64 _contactid = 0;
        private gloListControl.gloListControl oListControl = null;
        
        #endregion

        #region "Constructors"

        public C1FlexGrid ReferalGrid { get; set; }

        #endregion

        #region "Constructors"

        public frmAddReferrals()
        {
            InitializeComponent();
        }

        public frmAddReferrals(string Databaseconnectionstring, Int64 PatirntID, C1FlexGrid grdReferrals)
        {
            ReferalGrid = grdReferrals;
            InitializeComponent();
            _PatientId = PatirntID;
        } 
        #endregion

        #region "Form Load"
        private void frmAddReferrals_Load(object sender, EventArgs e)
        {
            Get_Referrals();    
        } 
        #endregion 

        #region "Get Referrals && Get ReferralDetails"

        private void Get_Referrals()
        {
            try
            {
                removOListControl();
                oListControl = new gloListControl.gloListControl(gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Referrals, true, this.Width);
                oListControl.ClinicID = gloPMGlobal.ClinicID;
                oListControl.ControlHeader = "Referrals";

              

                //_CurrentControlType = gloListControl.gloListControlType.Referrals;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl .ModifyFormHandlerClick +=new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                this.Controls.Add(oListControl);

                //Added By Debasish Das on 29th Jun 2010
                if (ReferalGrid.Rows.Count > 1)
                {
                    for (int iGrdCount = 1; iGrdCount <= ReferalGrid.Rows.Count-1; iGrdCount++)
                    {
                        oListControl.SelectedItems.Add(Convert.ToInt64(ReferalGrid.GetData(iGrdCount, 9)), Convert.ToString(ReferalGrid.GetData(iGrdCount, 0)));
                    }
                }
                //**
                
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void removOListControl(bool bClose=true)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                }
                catch { }
                if (bClose)
                {
                    oListControl.Dispose();
                    oListControl = null;
                }
            }
        }

        void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == "Referrals")
            {

                gloContacts.frmSetupPhysician ofrmAddContact = new gloContacts.frmSetupPhysician(gloPMGlobal.DatabaseConnectionString);
                ofrmAddContact.ShowDialog(this);
                ofrmAddContact.Dispose();
                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);
                }

            }
        }
        void oListControl_ModifyFormHandlerClick(object sender, EventArgs e)
        {
           
            if (oListControl.dgListView.CurrentRow!= null)
                {
                _contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
              }
            if (oListControl.ControlHeader == "Referrals")
            {
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    gloContacts.frmSetupPhysician ofrmModifyContact = new gloContacts.frmSetupPhysician(_contactid, gloPMGlobal.DatabaseConnectionString);
                    ofrmModifyContact.ShowDialog(this);
                    ofrmModifyContact.Dispose();
                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);
                    }
                }

            }
        }
        private DataTable GetDetails(Int64 contactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dt = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT Contacts_MST.sName,ISNULL(Contacts_MST.sContact,'') AS sContact,  ISNULL(Contacts_MST.sAddressLine1,'') AS sAddressLine1, ISNULL(Contacts_MST.sAddressLine2,'') AS sAddressLine2, "
                + " ISNULL(Contacts_MST.sCity,'') AS sCity, ISNULL(Contacts_MST.sState,'') AS sState, ISNULL(Contacts_MST.sZIP,'') AS sZIP, ISNULL(Contacts_MST.sPhone,'') AS sPhone, "
                + " ISNULL(Contacts_MST.sFax,'') AS sFax, ISNULL(Contacts_MST.sEmail,'') AS sEmail, ISNULL(Contacts_MST.sURL, '') AS sURL, ISNULL(Contacts_MST.sMobile,'') AS sMobile, ISNULL(Contacts_MST.sPager,'') AS sPager, "
                + " ISNULL(Contacts_MST.sNotes,'') AS sNotes, ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName, ISNULL(Contacts_MST.sLastName,'') AS sLastName, "
                + " ISNULL(Contacts_MST.sGender,'') AS sGender, ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy, ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, "
                + " ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID, ISNULL(Contacts_Physician_DTL.sUPIN,'') AS sUPIN, ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI, "
                + " ISNULL(Contacts_Physician_DTL.sHospitalAffiliation,'') AS sHospitalAffiliation, ISNULL(Contacts_Physician_DTL.sExternalCode,'') AS sExternalCode, ISNULL(Contacts_Physician_DTL.sDegree,'') AS sDegree"
                + " FROM Contacts_MST LEFT OUTER JOIN Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID "
                + " WHERE (Contacts_MST.nContactID = " + contactID + ") ";


                SqlQuery += " AND (Contacts_MST.sContactType = 'Physician')";




                oDB.Retrive_Query(SqlQuery, out  dt);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dt;
        } 

        #endregion

        #region "List Control Events"

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString );
            try
            {
                oDB.Connect(false);
                if (oListControl.SelectedItems.Count > 0)
                {
                    gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);

                    gloPatient.PatientDetails PatientReferrals = new gloPatient.PatientDetails();
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataTable dt = GetDetails(Convert.ToInt64(oListControl.SelectedItems[i].ID));
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            gloPatient.PatientDetail _Referral = new gloPatient.PatientDetail();

                            _Referral.PatientID = _PatientId;
                            _Referral.ContactId = Convert.ToInt64(oListControl.SelectedItems[i].ID);
                            _Referral.Name = Convert.ToString(dt.Rows[0]["sName"]);
                            _Referral.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                            _Referral.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                            _Referral.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                            _Referral.City = Convert.ToString(dt.Rows[0]["sCity"]);
                            _Referral.State = Convert.ToString(dt.Rows[0]["sState"]);
                            _Referral.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                            _Referral.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                            _Referral.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                            _Referral.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                            _Referral.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                            _Referral.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                            _Referral.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                            _Referral.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                            _Referral.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                            _Referral.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                            _Referral.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                            _Referral.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                            _Referral.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                            _Referral.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                            _Referral.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                            _Referral.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                            _Referral.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                            _Referral.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                            _Referral.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                            _Referral.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);
                            _Referral.ContactFlag = PatientContactType.Referral;
                            _Referral.ClinicID = gloPMGlobal.ClinicID;

                            PatientReferrals.Add(_Referral);
                        }
                    }

                    //Added By Debasish Das on 29th Jun 2010
                    oDB.Execute_Query("Delete from Patient_DTL where nPatientID = '" + _PatientId + "' AND nClinicID = " + gloPMGlobal.ClinicID + " AND nContactFlag = " + (int)PatientContactType.Referral);
                   //**
                    ogloPatient.Add_ReferraltoPatient(PatientReferrals);


                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            this.Close();
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            //if (oListControl != null)   
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removOListControl(false);
            this.Close();
        } 

        #endregion



    }
}