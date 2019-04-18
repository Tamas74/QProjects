using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloPMGeneral
{
    public partial class frmShowPriorAuthorization : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;      
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _AsOfDate;
        private Int64 _CurrentPriorAuthorization;
        private Int64 _PatientID;
        private Int64 _CurrentReferralProviderID;
        private string _CurrentPriorAuthorizationNo = "";
        bool _isSaved = false;
       // bool _isAdded = false;

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

        public Int64 CurrentPriorAuthorization
        {
            get { return _CurrentPriorAuthorization; }
            set { _CurrentPriorAuthorization = value; }
        }

        public string CurrentPriorAuthorizationNo
        {
            get { return _CurrentPriorAuthorizationNo; }
            set { _CurrentPriorAuthorizationNo = value; }
        }

        public Int64 CurrentReferralProviderID
        {
            get { return _CurrentReferralProviderID; }
            set { _CurrentReferralProviderID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmShowPriorAuthorization(string databaseConnectionString, Int64 AsOfDate, Int64 CurrentPriorAuthorization, Int64 PatientID)
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


             _AsOfDate = AsOfDate;
             _CurrentPriorAuthorization = CurrentPriorAuthorization;
             _PatientID=PatientID;
         }

        #endregion " Constructor "

        #region " Form Load "

          private void frmShowPriorAuthorization_Load(object sender, EventArgs e)
        {
            try
            {
                //Set by default this date As todays Date From calling Module if not present.
            
                //_CurrentPriorAuthorization=
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
                  //c1ProirAuthorization.Cols["nPatientID"].Visible = false;
                  c1ProirAuthorization.Cols["nReferralID"].Visible = false;
                  c1ProirAuthorization.Cols["sReferralName"].Visible = true;
                  c1ProirAuthorization.Cols["sInsuranceName"].Visible = true;
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Visible = true;
                  c1ProirAuthorization.Cols["nStartDate"].Visible = true;
                  c1ProirAuthorization.Cols["nExpDate"].Visible  = true;
                  c1ProirAuthorization.Cols["nVisitsAllowed"].Visible = false;
                  c1ProirAuthorization.Cols["bIsTrackAuthLimit"].Visible = false;
                  c1ProirAuthorization.Cols["bIsInActive"].Visible = false;
                  c1ProirAuthorization.Cols["PatientName"].Visible = false;
                 
                  #region " Set Header "
                  
                  c1ProirAuthorization.Cols["nPriorAuthorizationID"].Caption = "";
                  c1ProirAuthorization.Cols["sPriorAuthorizationNo"].Caption = "Auth #";
                  //c1ProirAuthorization.Cols["nPatientID"].Caption = "";
                  c1ProirAuthorization.Cols["nReferralID"].Caption  = "";
                  c1ProirAuthorization.Cols["sReferralName"].Caption = "Referring";
                  c1ProirAuthorization.Cols["sInsuranceName"].Caption = "Insurance";
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Caption = "Note";
                  c1ProirAuthorization.Cols["nStartDate"].Caption = "Start";
                  c1ProirAuthorization.Cols["nExpDate"].Caption = "Expiration";
                  c1ProirAuthorization.Cols["VisitsRemained"].Caption = "Visits Remain.";

                  #endregion

                  int _nWidth = 0;
                  _nWidth = 826;//Convert.ToInt32( c1QueuedClaims.Width);
                  c1ProirAuthorization.Cols["nPriorAuthorizationID"].Width = 0;
                  c1ProirAuthorization.Cols["sPriorAuthorizationNo"].Width = Convert.ToInt32(_nWidth * 0.10);

                  c1ProirAuthorization.Cols["sReferralName"].Width = Convert.ToInt32(_nWidth * 0.14);

                  c1ProirAuthorization.Cols["sInsuranceName"].Width = Convert.ToInt32(_nWidth * 0.14);

                  c1ProirAuthorization.Cols["PatientName"].Width = Convert.ToInt32(_nWidth * 0.15);
                  c1ProirAuthorization.Cols["sAuthorizationNote"].Width = Convert.ToInt32(_nWidth * 0.235);

                  c1ProirAuthorization.Cols["nStartDate"].Width = Convert.ToInt32(_nWidth * 0.085);
                  c1ProirAuthorization.Cols["nExpDate"].Width = Convert.ToInt32(_nWidth * 0.085);
                  //c1ProirAuthorization.Cols["nVisitsAllowed"].Width = Convert.ToInt32(_nWidth * 0.10);
                  //c1ProirAuthorization.Cols["VisitsRemained"].Caption = "Visits Remain";

                  //Merging 5076 Mahesh nawal
                  c1ProirAuthorization.Cols["nStartDate"].DataType = typeof(System.DateTime);
                  c1ProirAuthorization.Cols["nStartDate"].Format = "MM/dd/yyyy";
                  c1ProirAuthorization.Cols["nExpDate"].DataType = typeof(System.DateTime); ;
                  c1ProirAuthorization.Cols["nExpDate"].Format = "MM/dd/yyyy";

              
              }
              catch //(Exception ex)
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

        //Hardcoded nPatientID
        private void SetData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPrior = new DataTable();
            try
            {
          
               
                oDBParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", gloSettings.AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@AsOfDate",_AsOfDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                //oDB.Retrive("GET_PriorAuthorization",oDBParameters, out dtPrior);
                oDB.Retrive("GET_PriorAuthorization_Revised", oDBParameters, out dtPrior);
                c1ProirAuthorization.DataSource = dtPrior;//.DefaultView
                DesignGrid();

               
                for (int i = 1; i < c1ProirAuthorization.Cols.Count; i++)
                {
                    c1ProirAuthorization.Cols[i].AllowEditing = false;
                   
                }
                //for (int _Count = 1; _Count < c1ProirAuthorization.Rows.Count; _Count++)
                //{
                //    string _strReferral = String.Empty;
                //    _strReferral = Convert.ToString(c1ProirAuthorization.GetData(_Count, "sReferralName")).Trim();
                //    if (_strReferral != "")
                //    {
                //        if (_strReferral.StartsWith(",") || _strReferral.EndsWith(","))
                //        {
                //            _strReferral=_strReferral.Replace(",", ""); 
                //        c1ProirAuthorization.SetData(_Count, "sReferralName", _strReferral.Trim());}
                //    }
                //}
                for (int i = 1; i < c1ProirAuthorization.Rows.Count; i++)
                {
                    if (_CurrentPriorAuthorization == Convert.ToInt64(c1ProirAuthorization.GetData(i, 0)))
                    {
                        c1ProirAuthorization.RowSel = i;
                        c1ProirAuthorization.Select(i,1,true);
                        break;
                    }
                }
                c1ProirAuthorization.Refresh();


                    #region "SET OTHER DATA"

                    if (dtPrior != null && dtPrior.Rows.Count > 0)
                    {
                        lblPatientName1.Text = Convert.ToString(dtPrior.Rows[0]["PatientName"]).Trim();
                    }
                if (_AsOfDate > 0)
                {
                    //lblInPutDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDate(_AsOfDate).ToString("MM/dd/yyyy"));
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
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
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
            Int64 _SelectedAuthID = 0;
            if (c1ProirAuthorization.RowSel > 0)
            {
                _SelectedAuthID = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 0));

                frmModifyAuthorization OBJPrior = new frmModifyAuthorization(_databaseconnectionstring, _SelectedAuthID);
                OBJPrior.ShowDialog(this);
                OBJPrior.BringToFront();
                SetData();
                OBJPrior.Dispose();
                OBJPrior = null;
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //if (!_isSaved)
            //{
                _CurrentPriorAuthorization = 0;
                _CurrentPriorAuthorizationNo = "";
                _CurrentReferralProviderID = 0;
           // }
            //else
            //{
            //    if (c1ProirAuthorization.RowSel != null && Convert.ToInt64(c1ProirAuthorization.RowSel) > 0)
            //    {
            //        if (c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained") != null && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) != "" && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) == "Inactive")
            //        {
            //            MessageBox.Show("Selected prior authorization is inactive. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //        _CurrentPriorAuthorization = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 0));
            //        _CurrentPriorAuthorizationNo = Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 1));
            //        _CurrentReferralProviderID = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 5));
            //    }
            //}
            this.Close();
        }

        private void frmShowPriorAuthorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSaved)
            {
                _CurrentPriorAuthorization = 0;
                _CurrentPriorAuthorizationNo = "";
                _CurrentReferralProviderID = 0;
            }
            //else
            //{
            //    if (c1ProirAuthorization.RowSel != null && Convert.ToInt64(c1ProirAuthorization.RowSel) > 0)
            //    {
            //        if (c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained") != null && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) != "" && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) == "Inactive")
            //        {
            //            MessageBox.Show("Selected prior authorization is inactive. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //        if (!_isAdded)
            //        {
            //            _CurrentPriorAuthorization = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 0));
            //            _CurrentPriorAuthorizationNo = Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 1));
            //            _CurrentReferralProviderID = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 5));
            //        }
            //    }
            //}
        }

        private void tsb_ADD_Click(object sender, EventArgs e)
        {
            frmSetupAuthorization objSetupAuth = new frmSetupAuthorization(_PatientID);
            objSetupAuth.ShowDialog(this);
            objSetupAuth.BringToFront();
            SetData();

            if (objSetupAuth.IsAdded)
            {
                //_isAdded = true;
                _isSaved = true;
                _CurrentPriorAuthorization = objSetupAuth._PriorAuthorizationID;
                _CurrentPriorAuthorizationNo = objSetupAuth._PriorAuthorizationNo;
                _CurrentReferralProviderID = objSetupAuth._ReferralID;
                objSetupAuth.Dispose();
                objSetupAuth = null;
                this.Close();
            }
            if (objSetupAuth != null)
            {
                objSetupAuth.Dispose();
                objSetupAuth = null;
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            _isSaved = true;
            if (Convert.ToInt64(c1ProirAuthorization.RowSel) > 0)
            {
                if (c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained") != null && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) != "" && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) == "Inactive")
                {
                    MessageBox.Show("Selected prior authorization is inactive. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return ;   
                }
                _CurrentPriorAuthorization = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 0));
                _CurrentPriorAuthorizationNo = Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 1));
                _CurrentReferralProviderID = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 5));
            }
            this.Close();
        }

        private void c1ProirAuthorization_DoubleClick(object sender, EventArgs e)
        {
            _isSaved = true;
            if ( Convert.ToInt64(c1ProirAuthorization.RowSel) > 0)
            {
                if (c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained") != null && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) != "" && Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, "VisitsRemained")) == "Inactive")
                {
                    MessageBox.Show("Selected prior authorization is inactive. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isSaved = false;
                    return;
                }
                _CurrentPriorAuthorization = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 0));
                _CurrentPriorAuthorizationNo = Convert.ToString(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 1));
                _CurrentReferralProviderID = Convert.ToInt64(c1ProirAuthorization.GetData(c1ProirAuthorization.RowSel, 5));
            }
            this.Close();
        }
    }
}