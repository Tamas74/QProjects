using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEMRGeneralLibrary.gloEMRLab;
using gloEmdeonCommon.gloEMRWord;
using gloEmdeonInterface.Classes;
using gloPatientPortalCommon;


namespace gloEmdeonInterface.Forms
{
    public partial class frmLab_Acknoledgement : Form
    {
        private long OrderId;
        private string OrderNumberPrefix;
        private long OrderNumberID;
        public static bool ISsavedAckw = false;
        public string _ViewedDocumentPath = "";
        public string _ViewedDocumentDispName = "";
        private string _connString = string.Empty;
        public bool IsClosed = false;
        public bool blnCommentsPlaced = false;
        // added by Abhijeet on date 20100417
      //  private string _UserName = "";
        //Commeted the below code to remove warnings... on 20100520
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        // end of changes by Abhijeet on date 20100417
        //Added by madan on 20100424
        private Int64 _LoginUserId = 0;
        //End Madan.

        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: We are open acknw. window from History window.

        public Int64 _PatientProviderID;
        public Int64 _LoginProviderId;
        public string OrderStatus = "Normal";

        private Int64 _nAkwSrNo = 0;

        //by Abhijeet on 20100626 ,create & declare Patient ID property to assign/access from outside  
        long _patientID = 0;
        public long PatientID
        {
            get { return _patientID;}
            set { _patientID = value;}
        }
        // End of changes by Abhijeet on 20100626 for an Patient ID property


        //public frmLab_Acknoledgement(long _OrderID , string _OrderNumberPrefix ,long _OrderNumberID )
        //{
        //    OrderId = _OrderID;
        //    OrderNumberPrefix = _OrderNumberPrefix;
        //    OrderNumberID = _OrderNumberID;
        //    //This call is required by the Windows Form Designer.
        //    //Add any initialization after the InitializeComponent() call
        //    InitializeComponent();
        //}

        string gstrMessageBoxCaption = string.Empty; // by Abhijeet on 20100514

        //Added for Orders & Resultset on 20140219
        private gloListControl.gloListControl oListControl;
        DataTable dtMasterData = new DataTable();
        private string _strIsFrom = "";
        //End Orders & Resultset

        public frmLab_Acknoledgement(long _OrderID, string _OrderNumberPrefix, long _OrderNumberID, String ConnectionString)
        {
            OrderId = _OrderID;
            OrderNumberPrefix = _OrderNumberPrefix;
            OrderNumberID = _OrderNumberID;
            _connString = ConnectionString;

            // Code by : Abhijeet Farkande on date : 20100417, 20100514
            // changes : accessing the user Name                
            if (appSettings != null)
            {
                _LoginUserId = Convert.ToInt64(appSettings["UserID"]);
               // _UserName = Convert.ToString(appSettings["UserName"]);                
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {
                //_UserName = "Admin";
                gstrMessageBoxCaption = "gloEMR";
            }
            // End of code for accessing the user Name                

            //This call is required by the Windows Form Designer.
            //Add any initialization after the InitializeComponent() call
            InitializeComponent();
        }
        public frmLab_Acknoledgement(long _OrderID, string _OrderNumberPrefix, long _OrderNumberID,Boolean _IsClosed, String ConnectionString)
        {
            OrderId = _OrderID;
            OrderNumberPrefix = _OrderNumberPrefix;
            OrderNumberID = _OrderNumberID;
            _connString = ConnectionString;
            IsClosed = _IsClosed;

            // Code by : Abhijeet Farkande on date : 20100417, 20100514
            // changes : accessing the user Name                
            if (appSettings != null)
            {
                _LoginUserId = Convert.ToInt64(appSettings["UserID"]);
                // _UserName = Convert.ToString(appSettings["UserName"]);   
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {
                //_UserName = "Admin";
                gstrMessageBoxCaption = "gloEMR";
            }
            // End of code for accessing the user Name                

            //This call is required by the Windows Form Designer.
            //Add any initialization after the InitializeComponent() call
            InitializeComponent();
        }
        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: New Constructor to get value of AckwSrno
        public frmLab_Acknoledgement(long _OrderID, Boolean _IsClosed, String ConnectionString, Int64 AckSrNo)
        {
            OrderId = _OrderID;
            _connString = ConnectionString;
            IsClosed = _IsClosed;
            _nAkwSrNo = AckSrNo;
            if (appSettings != null)
            {
                _LoginUserId = Convert.ToInt64(appSettings["UserID"]);
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {
                //_UserName = "Admin";
                gstrMessageBoxCaption = "gloEMR";
            }

            InitializeComponent();

        }

        private void frmLab_Acknoledgement_Load(object sender, EventArgs e)
        {

            //Madan added on 20100508
            if (!IsClosed)
            {
                this.Text = "Review";
                panel1.Visible = false;
                this.Height = this.Height - panel1.Height;
                ts_AssignTask.Visible = false;
            }
            //End madan.

            //Developer:Sanjog Dhamke
            //Date: 20 Dec 2011 (6060)
            //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
            //Reason: To fill ackw. details for selected ackw.

            //This flag is true means we are opening this form for Acknowledgement, so we have to check whether acknowledgement is present or not and if present then open it for modify.

            //if (_nAkwSrNo == 0)
            //{
            //    txtOrderName.Text = OrderNumberPrefix + "-" + OrderNumberID;
            //    txtComments.Text = "";
            //    dtpReviwed.Value = Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")); // Convert.ToString(System.DateTime.Now, "MM/dd/yyyy hh:mm:ss tt");
            //    cmbUsers.Visible = false;

            //    try
            //    {

            //        //Added by madan to get login username on 20100424
            //        if (_LoginUserId != 0)
            //        {
            //            string _tempUserName = string.Empty;
            //            _tempUserName = GetLoginUserName(_LoginUserId);
            //            if (_tempUserName != "")
            //            {
            //                _UserName = _tempUserName;
            //            }
            //        }
            //        //End Madan

            //        txtUser.Text = _UserName; // by Abhijeet on  20100417 

            //        txtUser.ReadOnly = true;
            //        txtUser.BackColor = Color.White;
            //        txtOrderName.BackColor = Color.White;
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            //    }
            //}
            //else
            //{
            FillAcknowledgement(OrderId, _nAkwSrNo, _LoginUserId);
            //}

            DataColumn dcId = new DataColumn("ID");
            DataColumn dcNotes = new DataColumn("Notes");
            dtMasterData.Columns.Add(dcId);
            dtMasterData.Columns.Add(dcNotes);
            dtMasterData.PrimaryKey = new DataColumn[] { dtMasterData.Columns["ID"] };
        }

        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: To fill ackw. details for selected ackw.

        private bool FillAcknowledgement(Int64 OrdrID,Int64 nTaskSrNo,Int64 LoginID)
        {
            if (IsClosed == true)
            {
                DataTable dtAkw = new DataTable();
                try
                {
                    gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oReviewed = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                    dtAkw = oReviewed.Get_Acknowledgment(OrdrID,_nAkwSrNo,_LoginUserId);
                    oReviewed = null;

                    if (dtAkw.Rows.Count > 0)
                    {
                        txtComments.Text = Convert.ToString(dtAkw.Rows[0]["Comments"]);
                        txt_PatientNotes.Text = Convert.ToString(dtAkw.Rows[0]["PatientNote"]);
                        txtOrderName.Text = Convert.ToString(dtAkw.Rows[0]["nOrderNumberPrefix"]) + "-" + Convert.ToString(dtAkw.Rows[0]["nOrderNumberID"]);
                        //dtpReviwed.Value = Convert.ToDateTime(dtAkw.Rows[0]["ReviewDatetime"]);
                        dtpReviwed.Value = Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                        txtUser.Text = Convert.ToString(dtAkw.Rows[0]["UserName"]);
                        _nAkwSrNo = Convert.ToInt64(dtAkw.Rows[0]["nAcknowledgeSrNo"]);

                        OrderNumberPrefix = Convert.ToString(dtAkw.Rows[0]["nOrderNumberPrefix"]);
                        OrderNumberID = Convert.ToInt64(dtAkw.Rows[0]["nOrderNumberID"]);
                        return true;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }
        private void SaveAcknoledgement()
        {
            blnCommentsPlaced = false;
            
            long _ViwedUserID = 0;

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Enter user name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                blnCommentsPlaced = false;
                return;
            }

            //Developer:Sanjog Dhamke
            //Date: 20 Dec 2011 (6060)
            //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
            //Reason: Remove the mandatory field of Note & Comment

            //if (string.IsNullOrEmpty(txtComments.Text.Trim()))
            //{
            //    if (!IsClosed)
            //    {
            //        MessageBox.Show("Enter Review comment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        blnCommentsPlaced = false;
            //        return;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Acknowledge comment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        blnCommentsPlaced = false;
            //        return;
            //    }
            //}

            //Sanjog - Remove the mandatory field of Note & Comment
            //if (string.IsNullOrEmpty(txt_PatientNotes.Text.Trim()))
            //{
            //    if (IsClosed)
            //    {
            //        MessageBox.Show("Enter Patient notes", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        blnCommentsPlaced = false;
            //        return;
            //    }
            //}

            try
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //DialogResult.Cancel
                // ProgressBar1.Enabled = True
                ts_btnSave.Enabled = false;
                ts_btnClose.Enabled = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
                oDB.Connect(false);
                _ViwedUserID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nUserID FROM User_MST WHERE UPPER(sLoginName) = '" + (txtUser.Text).ToUpper() + "' AND sLoginName IS NOT NULL"));
                oDB.Disconnect();

                //Dim oReviwed As New gloStream.gloDMS.Document.Document
                //If oReviwed.UpdateReviwed(_ViewedDocumentPath, _ViwedUserID, dtpReviwed.Value, txtComments.Text.Trim) = True Then
                //    Me.DialogResult = Windows.Forms.DialogResult.OK 'DialogResult.OK
                //End If
                //oReviwed = Nothing


                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oReviewed = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();                 
    
                oReviewed.Add_Acknowledgment(OrderId, OrderNumberPrefix, OrderNumberID, _ViwedUserID, dtpReviwed.Value, txtComments.Text.Trim(),txt_PatientNotes.Text.Trim(),_nAkwSrNo);

                oReviewed = null;

                //frmLab_RequestOrder.tlbbtn_Acknowledgment.Visible = False
                //frmLab_RequestOrder.tlbbtn_VWAcknowledgment.Visible = True
                ISsavedAckw = true;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                //gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Lab Order Acknowledgment Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                //by Abhijeet on 20100430                
                if (!IsClosed)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab order reviewed.", PatientID, OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab order acknowledged.", PatientID, OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Lab Order Acknowledgment Added", gloAuditTrail.ActivityOutCome.Success);
                blnCommentsPlaced = true;
            }
            catch (Exception oError)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(oError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK);
                blnCommentsPlaced = false;
                return;
            }
            finally
            {
                //ProgressBar1.Enabled = False
                ts_btnSave.Enabled = true;
                ts_btnClose.Enabled = true;
            }

        }


        private void tlsp_LabAcknoledgment_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
                try
                {
                    switch (e.ClickedItem.Tag.ToString().ToLower())
                    {
                        case "save":
                            SaveAcknoledgement();
                             if (blnCommentsPlaced)
                            {
                                if (IsClosed) //Added by madan as per gloLab Requirement on 20100508
                                {
                                    UpdateOrder(OrderId);
                                    // 1006 OrderStatus number for Acknowledge.
                                    UpdateManualOrderStatus("1006",Convert.ToString(OrderId));
                                    SendPortalEmail();
                                }
                                this.Close();
                            }
                            break;
                        case "close":
                            this.Close();
                            //by Abhijeet on 20100430                           
                            if (!IsClosed)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Lab order not reviewed.", PatientID, OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.CancelOperation, "Lab order not acknowledged.", PatientID, OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            }

                            break;
                       
                        //'Developer:Sanjog Dhamke
                        //'Date: 21 Dec 2011
                        //'PRD Name: Lab Usability (6060)
                        //'Reason: To Open the Patient Letter & Referral Letter
                       
                        case "assign task":
                            //SaveAcknoledgement();
                            //if (blnCommentsPlaced)
                            //{
                                AssignTask();
                            //}
                            break;
                        case "assign normal":
                            GetNormalAckNotes();
                            break;
                        default :
                            break;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK);
                }
            
        }
        //Added by madan-- on 20100424
        public string GetLoginUserName(Int64 UserID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
            string _strLoginUserName = string.Empty;
            try
            {
                oDB.Connect(false);
                //ProID = Trim(oDB.ExecuteScaler)
                _strLoginUserName = Convert.ToString(oDB.ExecuteScalar_Query("Select sLoginName from dbo.User_MST where nUserID =" + UserID + ""));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strLoginUserName = "";

            }
            finally
            {
                oDB.Dispose();
            }
            return _strLoginUserName;
        }
        public bool UpdateOrder(Int64 nOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
            string strQry = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                if (nOrderID != 0)
                {
                    oDB.Connect(false);

                    strQry = "update Lab_Order_MST set bIsClosed=1 where labom_OrderID='" + nOrderID + "' ";
                    _Result = oDB.Execute_Query(strQry);
                    if (_Result < 0)
                    {
                        _boolResult = false;
                    }
                    else if (_Result >= 0)
                    {
                        _boolResult = true;
                    }
                    else
                    {
                        _boolResult = false;
                    }
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            return _boolResult;
        }

        private void UpdateManualOrderStatus(String intOrderStatus, String intOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
            gloDatabaseLayer.DBParameters oDBparams = new gloDatabaseLayer.DBParameters();

            string strQuery = string.Empty;
            DataTable _dtResult = new DataTable();

            try
            {
                strQuery = "gsp_UpdateOrderStatus";

                oDB.Connect(false);
                oDBparams.Clear();

                oDBparams.Add("@intOrderStatus", intOrderStatus, ParameterDirection.Input, SqlDbType.Int);
                oDBparams.Add("@intOrderID", intOrderID, ParameterDirection.Input, SqlDbType.BigInt, 18);

                oDB.Execute(strQuery, oDBparams);

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
        }

        private void AssignTask()
        {
            long _TaskId = 0;
            clsGeneral objClsGeneral = new clsGeneral();
            string strOrdDesc = GetOrderDetailsForTask(OrderId, _patientID);
            string strSubject = "";
            string strPriority = "";
            if (OrderStatus == "Normal")
            {
                strPriority = "Normal";
            }
            else
            {
                strPriority = "High";
            }
            if (OrderStatus == "Normal")
            {
                strSubject = "Re: Lab Results for Order " + txtOrderName.Text;
            }
            else
            {
                strSubject = "Re: Lab Results (" + OrderStatus + ") for Order " + txtOrderName.Text;
            }
            if (txtComments.Text.Trim() != "")
            {
                strOrdDesc = txtComments.Text + "\n" + strOrdDesc;
            }
            objClsGeneral.TestList = strOrdDesc;
            
            //Developer:Sanjog Dhamke
            //Date:21 Dec 2011
            //PRD Name: To Open task window in lab Ackw.
            //Reason: To pass the patient Id to task window

            //00000926 : Creating task for Assigned Lab order task
            _TaskId = objClsGeneral.AssignTaskToUser(_patientID, _PatientProviderID, _LoginProviderId, OrderId, gloTaskMail.TaskType.AssignedLabOrder, strSubject, strPriority);
            if (_TaskId > 0)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", _patientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
            objClsGeneral.Dispose();
        }

        private void GetNormalAckNotes()
        {
            //if (IsDefaultNormalNoteExists())
            //{
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
                string strInternalNote = string.Empty;
                string strPatientNote = string.Empty;
                try
                {
                    oDB.Connect(false);
                    //ProID = Trim(oDB.ExecuteScaler)
                    strInternalNote = Convert.ToString(oDB.ExecuteScalar_Query("SELECT ISNULL(labAckNotes,'') AS labAckNotes FROM Lab_AckNotes_MST  WHERE labAckNotes_ID IN (SELECT TOP 1 labAckInternalNotes_ID FROM Lab_AckNormalNotes)"));
                    strPatientNote = Convert.ToString(oDB.ExecuteScalar_Query("SELECT ISNULL(labAckNotes,'') AS labAckNotes FROM Lab_AckNotes_MST  WHERE labAckNotes_ID IN (SELECT TOP 1 labAckPatientNotes_ID FROM Lab_AckNormalNotes)"));
                    oDB.Disconnect();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    strInternalNote = "";
                    strPatientNote = "";
                }
                finally
                {
                    oDB.Dispose();
                    txtComments.Text = strInternalNote;
                    txt_PatientNotes.Text = strPatientNote;
                }
            //}
            //else
            //{
            //    MessageBox.Show("Please Set Normal Notes from Edit -> Orders & Results Setup.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
        }

        public bool IsDefaultNormalNoteExists()
        {
            bool isValid = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connString);
            string _IsValidNormalNotesPresent = string.Empty;
            try
            {
                oDB.Connect(false);
                //ProID = Trim(oDB.ExecuteScaler)
                _IsValidNormalNotesPresent = Convert.ToString(oDB.ExecuteScalar_Query("IF EXISTS ( SELECT  * FROM  Lab_AckNormalNotes ) BEGIN IF EXISTS ( SELECT  * FROM Lab_AckNormalNotes WHERE labAckInternalNotes_ID = 0 OR labAckPatientNotes_ID = 0 ) BEGIN SELECT  'FALSE' END ELSE BEGIN SELECT  'TRUE' END END ELSE SELECT  'FALSE'"));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsValidNormalNotesPresent = "";

            }
            finally
            {
                oDB.Dispose();
            }
            if (Convert.ToString(_IsValidNormalNotesPresent) != "" && Convert.ToString(_IsValidNormalNotesPresent).ToLower() == "true")
            {
                isValid = true;
            }
            return isValid;
        }

        private string GetOrderDetailsForTask(Int64 OrderID, Int64 PatID)
        {

            DataSet ds = new DataSet();
            string strTaskDesc = "";
            gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer odb = new gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer();

            try
            {
                var _with1 = odb;
                gloEMRGeneralLibrary.gloEMRDatabase.DBParameter oPara = default(gloEMRGeneralLibrary.gloEMRDatabase.DBParameter);
                _with1.DBParametersCol.Clear();

                oPara = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oPara.DataType = SqlDbType.BigInt;
                oPara.Direction = ParameterDirection.Input;
                oPara.Value = PatID;
                oPara.Name = "@PatientID";
                _with1.DBParametersCol.Add(oPara);
                oPara = null;

                oPara = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oPara.DataType = SqlDbType.BigInt;
                oPara.Direction = ParameterDirection.Input;
                oPara.Value = OrderID;
                oPara.Name = "@OrderID";
                _with1.DBParametersCol.Add(oPara);
                oPara = null;


                ds = _with1.GetDataSet("LabUC_TestDetails");
                DataView oDv;
                Int64 IntTestID = 0;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oDv = ds.Tables[0].DefaultView;
                        if (IntTestID != Convert.ToInt64(ds.Tables[0].Rows[i]["TestID"]))
                        {
                            IntTestID = Convert.ToInt64(ds.Tables[0].Rows[i]["TestID"]);
                            oDv.RowFilter = "TestID = " + Convert.ToString(ds.Tables[0].Rows[i]["TestID"]);
                            strTaskDesc += "\n" + Convert.ToString(ds.Tables[0].Rows[i]["TestName"]) + "\n" ;
                            for (int j = 0; j < oDv.Count; j++)
                            {
                                strTaskDesc +="\t\t" + Convert.ToString(oDv[j]["ResultName"]) + "\t\t" + Convert.ToString(oDv[j]["ResultValue"]) + "\n";
                                if (Convert.ToString(oDv[j]["Flag"]) != "N" && Convert.ToString(oDv[j]["Flag"]) != "")
                                {
                                    OrderStatus = "Abnormal";
                                }
                            }
                        }
                        
                    }
                }

                ds = null;
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), true);
                return null;
            }
            finally
            {
                if ((odb != null))
                {
                    odb.Dispose();
                    odb = null;
                }
                if ((ds != null))
                {
                    ds.Dispose();
                    ds = null;

                }
            }
            return strTaskDesc;
        }

        #region "Orders & Resultset"
        private void btnInternalBr_Click(object sender, EventArgs e)
        {
            _strIsFrom = "InternalNotes";
            BrowseQuickNotes();
        }

        private void btnPatientBr_Click(object sender, EventArgs e)
        {
            _strIsFrom = "PatientComments";
            BrowseQuickNotes();
        }

        private void BrowseQuickNotes()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i += -1)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= oListControl_SelectedClick;
                        oListControl.ItemClosedClick -= oListControl_ItemClosedClick;

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }


                oListControl = new gloListControl.gloListControl(_connString, gloListControl.gloListControlType.OrderQuickNotes, true, this.Width, _strIsFrom);
                oListControl.ControlHeader = "Quick Text";
                oListControl.ItemSelectedClick += oListControl_SelectedClick;
                oListControl.ItemClosedClick += oListControl_ItemClosedClick;
                if (dtMasterData != null && dtMasterData.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtMasterData.Rows.Count - 1; i++)
                    {
                        oListControl.SelectedItems.Add(Convert.ToInt64(dtMasterData.Rows[i]["ID"]), Convert.ToString(dtMasterData.Rows[i]["Notes"]), "");
                    }
                }

                this.Controls.Add(oListControl);
                oListControl.OpenControl();
                pnlMain.Visible = false;
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i += -1)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= oListControl_SelectedClick;
                        oListControl.ItemClosedClick -= oListControl_ItemClosedClick;

                    }
                    catch { }
                }
                pnlMain.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void oListControl_SelectedClick(object sender, EventArgs e)
        {

            try
            {
                DataTable dtAddData = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcNotes = new DataColumn("Notes");
               
                dtAddData.Columns.Add(dcId);
                dtAddData.Columns.Add(dcNotes);


                if (oListControl.SelectedItems.Count > 0)
                {

                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        object key = oListControl.SelectedItems[i].ID;

                        if (dtMasterData.Rows.Find(key) == null)
                        {
                            DataRow drTemp = dtMasterData.NewRow();
                            drTemp["ID"] = oListControl.SelectedItems[i].ID;
                            drTemp["Notes"] = oListControl.SelectedItems[i].Code;
                            
                            dtMasterData.Rows.Add(drTemp);


                            DataRow drNew = dtAddData.NewRow();
                            drNew["ID"] = oListControl.SelectedItems[i].ID;
                            drNew["Notes"] = oListControl.SelectedItems[i].Code;
                            
                            dtAddData.Rows.Add(drNew);
                        }

                    }
                }
                int k = 0;
                if (dtAddData != null && dtAddData.Rows.Count > 0)
                {
                    for (k = 0; k <= dtAddData.Rows.Count - 1; k++)
                    {
                        if (_strIsFrom == "InternalNotes")
                        {
                            if (!string.IsNullOrEmpty(txtComments.Text.Trim()))
                            {
                                txtComments.Text = txtComments.Text + Environment.NewLine + dtAddData.Rows[k]["Notes"].ToString();
                            }
                            else
                            {
                                txtComments.Text = dtAddData.Rows[k]["Notes"].ToString();
                            }
                        }
                        else if (_strIsFrom == "PatientComments")
                        {
                            if (!string.IsNullOrEmpty(txt_PatientNotes.Text.Trim()))
                            {
                                txt_PatientNotes.Text = txt_PatientNotes.Text + Environment.NewLine + dtAddData.Rows[k]["Notes"].ToString();
                            }
                            else
                            {
                                txt_PatientNotes.Text = dtAddData.Rows[k]["Notes"].ToString();
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pnlMain.Visible = true;
            }
        }
        #endregion


        #region "Patient Portal"
        /// <summary>
        /// Sent Email to Patient & All patient Representative on Order Acknowledge. 
        /// </summary>
        private void SendPortalEmail()
        {
            try
            {
                clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_connString);
                if (oclsgloPatientPortalEmail.IsPatientPortalEnabled() == true)
                {
                    if (oclsgloPatientPortalEmail.IsNotifyLabAcknowledgement() == true)
                    {
                        DataTable dtValidPortalUser = null;
                        dtValidPortalUser = oclsgloPatientPortalEmail.ToCheckPatientRegisterOrNotOnPortal(PatientID);
                        oclsgloPatientPortalEmail.getPatientPortalSettings();
                        if (dtValidPortalUser != null && dtValidPortalUser.Rows.Count > 0)
                        {

                            string strFilepath = "";
                            clsGeneral objClsGeneral = new clsGeneral();
                            strFilepath = objClsGeneral.GenerateCDA(_patientID, _LoginUserId);
                            oclsgloPatientPortalEmail.SendPortalEmail(PatientID, oclsgloPatientPortalEmail.strPatientPortalEmailService, oclsgloPatientPortalEmail.strPatientPortalSiteNm, oclsgloPatientPortalEmail._ClinicID, "LAB RESULTS");
                            if (objClsGeneral != null) //added for memory management
                            {
                                objClsGeneral.Dispose();
                                objClsGeneral = null;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        #endregion
    }
}