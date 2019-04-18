using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
namespace gloBilling
{
    public partial class frmShowCases : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;              
        private Int64 _ClinicID = 0;        
        private Int64 _PatientID;
        private Int64 _CurrentnCaseId=0;
        private bool _bisSave = false;
        private String _CurrentnCaseName = String.Empty;       
        private Int64 _CurrentReferralProviderID = 0;
        private DataTable _CaseData = new DataTable();
        private DataTable _Diagnosis = new DataTable();
        private DataTable _Insurences = new DataTable();
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

        public bool IsSave
        {
            get { return _bisSave; }
            set { _bisSave = value; }
        }

        public Int64 CurrentCase
        {
            get { return _CurrentnCaseId; }
            set { _CurrentnCaseId = value; }
        }
        public String CurrentnCaseName
        {
            get { return _CurrentnCaseName; }
            set { _CurrentnCaseName = value; }
        }
        public Int64 CurrentReferralProviderID
        {
            get { return _CurrentReferralProviderID; }
            set { _CurrentReferralProviderID = value; }
        }
        public DataTable CurrentCaseData
        {
            get { return _CaseData; }
            set { _CaseData = value; }
        }
        public DataTable CurrentDiagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; }
        }
        public DataTable CurrentCaseInsurences
        {
            get { return _Insurences; }
            set { _Insurences = value; }
        }
        #endregion " Property Procedures "

        #region " Constructor "

        public frmShowCases(Int64 PatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
             _PatientID=PatientID;
         }

        #endregion " Constructor "

        #region "Destructor"
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (CurrentCaseData != null)
            {
                CurrentCaseData.Dispose();
                CurrentCaseData = null;
            }
            if (CurrentDiagnosis != null)
            {
                CurrentDiagnosis.Dispose();
                CurrentDiagnosis = null;
            }
            if (CurrentCaseInsurences != null)
            {
                CurrentCaseInsurences.Dispose();
                CurrentCaseInsurences = null;
            }

            if (_CaseData != null)
            {
                _CaseData.Dispose();
                _CaseData = null;
            }
            if (_Diagnosis != null)
            {
                _Diagnosis.Dispose();
                _Diagnosis = null;
            }
            if (_Insurences != null)
            {
                _Insurences.Dispose();
                _Insurences = null;
            }

            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion "Destructor

        #region " Form Load "

        private void frmShowCases_Load(object sender, EventArgs e)
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

                  c1Cases.SetData(0, 0, "nid");
                  c1Cases.SetData(0, 1, "Case Name");
                  c1Cases.SetData(0, 2, "Accident Type");
                  c1Cases.SetData(0, 3, "Claim #");
                  c1Cases.SetData(0, 4, "Start Date");
                  c1Cases.SetData(0, 5, "End Date");
                  c1Cases.SetData(0, 6, "Diag");
                  c1Cases.SetData(0, 7, "Facility");
                  c1Cases.SetData(0, 8, "Auth #");
                  c1Cases.SetData(0, 9, "Referring Provider");
                  c1Cases.SetData(0, 10, "Rpt Category ");
                  //c1PatientDetails.SetData(0, 9, "AccidentType");
                  c1Cases.SetData(0, 11, "Note");


                  c1Cases.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[8].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[10].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                  c1Cases.Cols[11].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                  c1Cases.Cols[0].AllowEditing = false;
                  c1Cases.Cols[1].AllowEditing = false;
                  c1Cases.Cols[2].AllowEditing = false;
                  c1Cases.Cols[3].AllowEditing = false;
                  c1Cases.Cols[4].AllowEditing = false;
                  c1Cases.Cols[5].AllowEditing = false;
                  c1Cases.Cols[6].AllowEditing = false;
                  c1Cases.Cols[7].AllowEditing = false;
                  c1Cases.Cols[8].AllowEditing = false;
                  c1Cases.Cols[9].AllowEditing = false;
                  c1Cases.Cols[10].AllowEditing = false;
                  c1Cases.Cols[11].AllowEditing = false;


                  
                  c1Cases.Cols[12].Visible = false;
                  c1Cases.Cols[13].Visible = false;


                  c1Cases.Cols[0].Visible = false;


                  //c1Cases.Cols[1].Width = 100;
                  //c1Cases.Cols[2].Width = 75;
                  //c1Cases.Cols[3].Width = 75;
                  //c1Cases.Cols[4].Width = 150;
                  //c1Cases.Cols[5].Width = 140;
                  //c1Cases.Cols[6].Width = 75;
                  //c1Cases.Cols[7].Width = 140;
                  //c1Cases.Cols[8].Width = 50;
                  //c1Cases.Cols[9].Width = 75;
                  //c1Cases.Cols[10].Width = 50;

                  c1Cases.Cols[1].Width = 175;
                  c1Cases.Cols[2].Width = 100;
                  c1Cases.Cols[3].Width = 75;
                  c1Cases.Cols[4].Width = 75;
                  c1Cases.Cols[5].Width = 75;
                  c1Cases.Cols[6].Width = 175;
                  c1Cases.Cols[7].Width = 140;
                  c1Cases.Cols[8].Width = 75;
                  c1Cases.Cols[9].Width = 140;
                  c1Cases.Cols[10].Width = 100;
                  c1Cases.Cols[11].Width = 100;
                  c1Cases.Cols[4].DataType = typeof(System.DateTime);
                  c1Cases.Cols[5].DataType = typeof(System.DateTime);
                  c1Cases.Cols[4].Format = "MM/dd/yyyy";
                  c1Cases.Cols[5].Format = "MM/dd/yyyy";
              
              }
              catch (Exception ex)
              {
                  gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        //Hardcoded nPatientID
        private void SetData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCase = new DataTable();
            try
            {


                String _strQuery = "SELECT Patient_Cases_MST.nCaseID, sCaseName,"+
                                    " Case when Patient_Cases_MST.nAccidentType =1 then 'Work' " +
                                    " when Patient_Cases_MST.nAccidentType =2 then 'Auto' " +
                                    " when Patient_Cases_MST.nAccidentType =3 then 'Other' " +
                                    " else 'None'  end  AccidentType ," +
                                     "sWCNumber,dtStartdate,dtEnddate," +
                                    " dbo.GetCaesDiagnoses(Patient_Cases_MST.nCaseID) as Diag,dbo.GetCasesFacility(Patient_Cases_MST.nCaseId) as sFacilityDescription,(Select sPriorAuthorizationNo FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS sAuthorizationNumber, " +
                                    " (SELECT " +
                                        "ISNULL(Contacts_MST.sFirstName,'')  +space(1)+" +
                                        "CASE ISNULL(Contacts_MST.sMiddleName,'') WHEN '' THEN '' WHEN Contacts_MST.sMiddleName THEN Contacts_MST.sMiddleName +SPACE(1) END" +
                                        "+ISNULL(Contacts_MST.sLastName,'') AS sReferralName " +
                                        "FROM Contacts_MST WHERE nContactID=Patient_Cases_MST.nReferralID) as Referring, " +
                                    " Cases_ReportingCategory.sCode, " +                                    
                                    " sNote, "+
                                    "  Patient.nPatientID,   Patient.sPatientCode + '-'+Patient.sFirstName+' ' +Patient.sMiddleName+' '+ Patient.sLastName as sPatientName    " +
                                    " FROM Patient_Cases_MST LEFT OUTER JOIN Cases_ReportingCategory " +
                                    " on Patient_Cases_MST.nReportCategoryId =Cases_ReportingCategory.nID " +
                                    " INNER JOIN Patient on  Patient_Cases_MST.nPatientID=Patient.nPatientID "+
                                   "  where  Patient_Cases_MST.nPatientId= " + _PatientID;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtCase);
                c1Cases.DataSource = dtCase.Copy();//.DefaultView
                DesignGrid();

               
                for (int i = 1; i < c1Cases.Cols.Count; i++)
                {
                    c1Cases.Cols[i].AllowEditing = false;
                   
                }
                
                for (int i = 1; i < c1Cases.Rows.Count; i++)
                {
                    if (CurrentCase == Convert.ToInt64(c1Cases.GetData(i, 0)))
                    {
                        c1Cases.RowSel = i;
                        c1Cases.Select(i,1,true);
                        break;
                    }
                }
                c1Cases.Refresh();


                    #region "SET OTHER DATA"

                if (dtCase != null && dtCase.Rows.Count > 0)
                {
                    lblPatientName1.Text = Convert.ToString(dtCase.Rows[0]["sPatientName"]).Trim();
                }
               
                #endregion
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
                if (dtCase != null)
                {
                    dtCase.Dispose();
                    dtCase = null;
                }
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

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            Int64 _nCaseID = 0;
            if (c1Cases.RowSel > 0)
            {
                _nCaseID = Convert.ToInt64(c1Cases.GetData(c1Cases.RowSel, 0));

                frmSetupCases ofrmSetupCases = new frmSetupCases(_PatientID, _nCaseID);
                ofrmSetupCases.ShowDialog(this);
                ofrmSetupCases.BringToFront();
                ofrmSetupCases.Dispose();
                SetData();
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
                CurrentCase = 0;
              _CurrentReferralProviderID = 0;
          
            this.Close();
        }

        private void frmShowCases_FormClosing(object sender, FormClosingEventArgs e)
        {
           
           
        }

        private void tsb_ADD_Click(object sender, EventArgs e)
        { 
            frmSetupCases ofrmSetupCases = new frmSetupCases(_PatientID);
            ofrmSetupCases.ShowDialog(this);
            ofrmSetupCases.Dispose();
            SetData();
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            
            if ( Convert.ToInt64(c1Cases.RowSel) > 0)
            {  
                _CurrentnCaseId = Convert.ToInt64(c1Cases.GetData(c1Cases.RowSel, 0));
                _CurrentnCaseName = Convert.ToString(c1Cases.GetData(c1Cases.RowSel, 1));
               // _CurrentReferralProviderID = Convert.ToInt64(c1Cases.GetData(c1Cases.RowSel, 5));
                getCaseData(_CurrentnCaseId);
                _bisSave = true;
            }
            this.Close();
        }

        private void c1ProirAuthorization_DoubleClick(object sender, EventArgs e)
        {
            
            if ( Convert.ToInt64(c1Cases.RowSel) > 0)
            {

                _CurrentnCaseId = Convert.ToInt64(c1Cases.GetData(c1Cases.RowSel, 0));
                _CurrentnCaseName = Convert.ToString(c1Cases.GetData(c1Cases.RowSel, 1));
            //    _CurrentReferralProviderID = Convert.ToInt64(c1Cases.GetData(c1Cases.RowSel, 5));
                getCaseData(_CurrentnCaseId);
                _bisSave = true;
            }
            this.Close();
        }

        public void getCaseData(Int64 CaseID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            DataTable dtCases = new DataTable();
            DataTable dtCasesDiag = new DataTable();
            DataTable dtCasesIns = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT nCaseID, nPatientId, sCaseName, bUpdateDiagnose," +
                " dtHopitalizationDateTo, dtHopitalizationDateFrom, nInsuranceid,"+
                " (Select nPriorAuthorizationID FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS nAuthorizationID, " +
                " (Select sPriorAuthorizationNo FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS sAuthorizationNumber, dtStartdate, dtEnddate, nFacilityID, nReferralID, nAccidenttype, " +
                " dtInjuryDate, sWCnumber, dtunbaleWorkfrom, dtunbaleWorkto,sState, nReportCategoryID," +
                " sNote,dtCreatedDateTime, dtModifiedDateTime, nCreatedUserID, nModifyUserID,ISNULL(nICDRevision,9 ) AS nICDRevision , sClaimDateQualifier,dtOtherClaimDate,sOtherClaimDateQualifier FROM  Patient_Cases_MST" +
               "  WHERE nCaseID= " + CaseID;
                oDB.Retrive_Query(strQuery, out dtCases);
                if (dtCases != null)
                    CurrentCaseData = dtCases;

                strQuery = " SELECT sdxCode,sDxDescription FROM Patient_Cases_Diag where nCaseID= " + CaseID + " Order By nIndex";
                oDB.Retrive_Query(strQuery, out dtCasesDiag);
                if (dtCasesDiag != null)
                    CurrentDiagnosis = dtCasesDiag;
                strQuery = "select nID, nCaseID, nInsuranceId ,sInsuranceName , nResponsibilityNumber from Patient_Cases_InsPlan where nCaseID= " + CaseID+"ORDER BY nResponsibilityNumber";
                oDB.Retrive_Query(strQuery, out dtCasesIns);
                if (dtCasesDiag != null)
                    CurrentCaseInsurences= dtCasesIns;
               
               
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtCases != null)
                {
                    dtCases.Dispose();
                    dtCases = null;
                }
                if (dtCasesDiag != null)
                {
                    dtCasesDiag.Dispose();
                    dtCasesDiag = null;
                }
                if (dtCasesIns != null)
                {
                    dtCasesIns.Dispose();
                    dtCasesIns = null;
                }

            }

        }

        private void c1Cases_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
        }
    }
}
