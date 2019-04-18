using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloContacts
{
    public partial class frmPlanHold : Form
    {

        #region " Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public bool oDialogResult = false;
        private string _databaseConnection = "";
        private Int64 _nContactID = 0;
        private Int64 _nInsCompanyID = 0;
        private string _sPlanName = "";
        private Int64 _UserID = 0;
        private string _UserName = "";
        public gloPMContacts.PlanHold _oPlanHold = null;
        private string _messageBoxCaption = ""; 
        private decimal _nAmount=0;
        private Int16 _nClaimCount = 0;
        private Boolean bReasonModified = false;

        #endregion

        #region "Constructor"

        public frmPlanHold(string sDatabaseConnectionString, string sPlanName, Int64 nContactID, Int64 nInsCompanyID, gloPMContacts.PlanHold oPlanHold)
        {
            InitializeComponent();
          
            _databaseConnection = sDatabaseConnectionString;
            _sPlanName = sPlanName;
            _nContactID = nContactID;
            _nInsCompanyID = nInsCompanyID;
            _oPlanHold = new gloPMContacts.PlanHold();
            _oPlanHold = oPlanHold;
        
            if (sPlanName != "" && nContactID != 0)
            {
                //Function for assigning _nAmount & _nClaimCount which is displayed on form load
                GetClaimDetails(nContactID, ref _nAmount, ref _nClaimCount);
            }
            
            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
            #endregion
            
            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        } 

        #endregion

        #region "Form Load"
     
        private void frmPlanHold_Load(object sender, EventArgs e)
        {
            try
            {
                txtHoldNote.MaxLength = 255;
                txtHoldNoteMod.MaxLength = 255;

                //Reason For Condition****************************************
                //if already that plan is on Hold
                //*************************************************************
                if (_oPlanHold.IsHold == true)
                {
                    pnlBillingHold.Visible = false;
                    pnlOnBillingHold.Visible = true;

                    lbModlNoOfClaim.Text = Convert.ToString(_nClaimCount);
                    lblModTotalAmt.Text = "$" + Convert.ToString(_nAmount);

                    lblHoldDate.Text = _oPlanHold.HoldDateTime.ToString("MM/dd/yyyy"); //Convert.ToString(_oPlanHold.HoldDateTime.Date);
                    lblHoldTime.Text = _oPlanHold.HoldDateTime.ToString("hh:mm tt");
                    lblHoldUser.Text = GetUserName(Convert.ToInt64(_oPlanHold.HoldUserID));

                    lblHoldModDate.Text = _oPlanHold.HoldModDateTime.ToString("MM/dd/yyyy");
                    lblHoldModTime.Text = _oPlanHold.HoldModDateTime.ToString("hh:mm tt");
                    lblHoldModUser.Text = GetUserName(Convert.ToInt64(_oPlanHold.UnHoldUserID));

                   // this.Icon = Properties.Resources.Insurance_Plan_HOld;
                    
                    txtHoldNoteMod.Text = _oPlanHold.HoldReason;
                    txtHoldNoteMod.Select();

                
                        

                }
                else
                {
                    txtHoldNote.Select();
                    lblClaimCount.Text = Convert.ToString(_nClaimCount);
                    lblTotBalance.Text = "$" + Convert.ToString(_nAmount);
                    pnlBillingHold.Visible = true;
                    pnlOnBillingHold.Visible = false;
                   // this.Icon = Properties.Resources.Release_Insurance_Plan_HOld;
                    
                }

                #region "Assigning Hold Message on Form"

                //Reason For Condition****************************************
                //before assigning the name of plan if user click on hold plan 
                //then plan name will appear on Hold Plan Form
                //Else by default "Insurance Plan" will appear
                //************************************************************
                if (_sPlanName != "")
                {
                    lblMsgHold.Text = _sPlanName + " will be put on Billing Hold. Insurance Billing will not proceed until the hold is released.";
                }
                else
                {
                    lblMsgHold.Text = "Insurance Plan will be put on Billing Hold. Insurance Billing will not proceed until the hold is released.";
                } 

                #endregion



                bReasonModified = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        } 

        #endregion

        #region " Tool strip Events " 
        
        #region "Event fire while first time user is saving plan On Hold"
        private void tls_SaveandCls_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtHoldNote.Text.Trim() == null || txtHoldNote.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Hold Reason.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoldNote.Text = "";
                    txtHoldNote.Focus();

                }
                else
                {
                    if (MessageBox.Show("Continue with Plan Hold?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        AssignDataToObject(txtHoldNote.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 
        #endregion

        #region "Event fire after clicking close button while putting Hold Information first time"

        private void tls_Close_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (bReasonModified)
            {
                result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (txtHoldNote.Text.Trim() == null || txtHoldNote.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Hold Reason", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHoldNote.Text = "";
                        txtHoldNote.Focus();

                    }
                    else
                    {
                        AssignDataToObject(txtHoldNote.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    oDialogResult = false;
                    this.Close();
                }
            }
            else
            {
                oDialogResult = false;
                this.Close();
            }
        } 

        #endregion

        #region "Event fire while Release button is clicked "

        private void tls_Releas_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("The Plan's claims will be back to normal and the hold information will be deleted. " + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    _oPlanHold.IsHold = false;
                    _oPlanHold.HoldModified = true;
                    oDialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        } 

        #endregion

        #region "Event fire after clicking close button while modifying Hold Information "

        private void tls_ModClose_Click(object sender, EventArgs e)
        {
            
            DialogResult result;
            if (bReasonModified)
            {
                result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (txtHoldNoteMod.Text.Trim() == null || txtHoldNoteMod.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Hold Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHoldNoteMod.Text = "";
                        txtHoldNoteMod.Focus();

                    }
                    else
                    {
                        AssignDataToObject(txtHoldNoteMod.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    oDialogResult = false;
                    this.Close();
                }
            }
            else
            {
                oDialogResult = false;
                this.Close();
            }
        } 

        #endregion

        #region "Event fire while user is updating plan On Hold"

        private void tls_ModSavenClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoldNoteMod.Text.Trim() == null || txtHoldNoteMod.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Hold Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoldNoteMod.Text = "";
                    txtHoldNoteMod.Focus();

                }
                else
                {
                    //if (MessageBox.Show("Continue with Plan Hold?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                        AssignDataToObject(txtHoldNoteMod.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 
        #endregion
           
        #endregion

        #region " TextBox Events"

        private void txtHoldNote_TextChanged(object sender, EventArgs e)
        {
            bReasonModified = true;
        }

        private void txtHoldNoteMod_TextChanged(object sender, EventArgs e)
        {
            bReasonModified = true;
        } 

        #endregion

        #region "Public & Private Methods"
        
        private string GetUserName(Int64 nUID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            String _UserName = "";
            string strQuery = "";
            try
            {

                oDB.Connect(false);
                strQuery = " select ISNULL(sLoginName,'') As sUserName from User_MST where nUserID= " + nUID + "";


                object _Result = oDB.ExecuteScalar_Query(strQuery);

                if (_Result != null && _Result.ToString() != "")
                {
                    _UserName = _Result.ToString();
                }
                return _UserName;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }
        }

        private void GetClaimDetails(Int64 nContactID,ref decimal nAmount,ref Int16 claimCount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPlanHoldDetails = null;
           
            try
            {

                #region " Commented Code "
                //// oDB.Connect(false);
                //// string strQuery = "";
                //// _dtPlanHoldDetails = new DataTable();
                //// strQuery = "select count(distinct nClaimNo)as ClaimCount,sum(dNextActionAmount) as Balance"
                ////             + " from BL_EOB_NEXTACTION where nNextActionContactID= " + nContactID + " and nNextActionContactID not in(0)";

                ////oDB.Retrive_Query(strQuery, out _dtPlanHoldDetails);

                //// if (_dtPlanHoldDetails != null && _dtPlanHoldDetails.Rows.Count > 0)
                //// {
                ////     claimCount = Convert.ToInt16(_dtPlanHoldDetails.Rows[0]["ClaimCount"]);
                ////     nAmount = Convert.ToDecimal(_dtPlanHoldDetails.Rows[0]["Balance"]);
                //// } 
                #endregion

                oDBParameters.Add("@Flag", "IndividualPlanHoldDetails", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Hold_Claims", oDBParameters, out _dtPlanHoldDetails);

                if (_dtPlanHoldDetails.Rows.Count > 0 && _dtPlanHoldDetails != null)
                {
                    nAmount  = Convert.ToDecimal(_dtPlanHoldDetails.Rows[0]["nBalance"]);
                    claimCount = Convert.ToInt16(_dtPlanHoldDetails.Rows[0]["nClaimCount"]);
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
                _dtPlanHoldDetails.Dispose();
            }
        }

        #region "Assign/Update Data To Object"

        //**************************************************
        //Method used to assign/Update the _oPlanHold object
        //***************************************************
        private void AssignDataToObject(string sReason)
        {
            try
            {
                if (_oPlanHold.IsHold == false)
                {
                    _oPlanHold = new gloPMContacts.PlanHold();
                    _oPlanHold.HoldDateTime = DateTime.Now;
                    _oPlanHold.HoldUserID = _UserID;
                }

                _oPlanHold.ContactID = _nContactID;
                _oPlanHold.HoldModDateTime = DateTime.Now;
                _oPlanHold.HoldReason = sReason;//txtHoldNoteMod.Text.Trim();
                _oPlanHold.UnHoldUserID = _UserID;
                _oPlanHold.InsCompanyID = _nInsCompanyID;
                _oPlanHold.UnHoldDateTime = DateTime.Now;
                _oPlanHold.IsHold = true;
                _oPlanHold.HoldModified = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        //************************* Ends Here ***************** 
        #endregion

       
       
        #endregion

        
       

        

                     
    }
}