using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.Enumeration;
using gloTaskMail;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_SendRecFax : Form
    {
        #region " Enumerated DataType "

        public enum TaskType
        { Task = 1, OrderRadiology = 2, FAX = 3, LabOrder = 4, DMS = 5, Exam = 6, RCMDocs = 31 }

        #endregion

        #region " C1 Columns Constants Declarations "


        private int Col_UserID = 0;
        private int Col_LoginName = 1;
        private int Col_Column1 = 2;
        private int Col_Column2 = 3;
        private int Col_ProviderID = 4;
        private int Col_Check = 5;
        //private int Col_Count_User = 6;

        #endregion

        #region " Variable Declarations "

        private ArrayList _SelectedDocuments = null;
        public bool _SendResult = false;
        //ArrayList Users = null;
        private gloEDocumentV3.Forms.UserList dgcustomGrid = null;
        Int64 _PatientId = 0;
        public string _ErrorMessage = "";

        public bool oDialogResultIsOK = false;
        public Int64 oDialogDocumentID = 0;
        public Int64 oDialogContainerID = 0;
        public Int64 oClinicID = 0;
        public Int64 oImportInCategoryID = 0;
        public string oImportInCategory = "";
        public string oImportInSubCategory = "";
        public string oImportInYear = DateTime.Now.Year.ToString();
        public string oImportInMonth = "";
        public string _messageBoxCaption = "DMS";
        private bool _blnReceivedFaxes = false;

        public enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;

        #endregion

        #region " Property Procedures "
        string _strNonXML = "";
        public Int64 PatientID
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }

        public ArrayList oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }
        public string strNonXML
        {
            get { return _strNonXML; }
            set { _strNonXML = value; }
        }

        public bool SendResult
        {
            get { return _SendResult; }
            set { _SendResult = value; }
        }

        public DocumentContextMenu.eContextDocuments oSelDocuments
        {
            get;
            set;
        }

        #endregion

        #region " Structure Defination "

        public struct User
        {
            public Int64 UserID;
            public string UserName;
            //  public Int64 ProviderID;

        }

        #endregion

        #region " Constructor "

        public frmEDocEvent_SendRecFax()
        {
            _SelectedDocuments = new ArrayList();
            InitializeComponent();

            this.Text = "Send Received Faxes";
        }

        public frmEDocEvent_SendRecFax(DocumentContextMenu.eContextDocuments oSelDoc)
        {
            oSelDocuments = oSelDoc;
            InitializeComponent();
            this.Text = "Send DMS Task";
            this.panel2.Visible = false;

        } //frmEDocEvent_SendRecFax

        #endregion

        private void frmEDocEvent_SendRecFax_Load(object sender, EventArgs e)
        {
            //Added by Shweta 20100105 against bug id:5378 
            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            // string sRecieveFaxUserName = "";
            User ouser = new User();
            //End 20100105
            try
            {
                #region " Designer Code "

                lblUser.Visible = true;
                cmbUser.Visible = true;
                lblTask.Visible = true;
                txtTask.Visible = true;
                tlb_Delete.Visible = false;
                tlb_Review.Visible = false;

                #endregion


                txtTask.Text = "";
                //Commented by Shweta 20100105 against bug id:5378 
                //cmbUser.SelectedIndex = -1;
                //end commenting
                //Added by Shweta 20100105 against bug id:5378 
                pbDocument.Minimum = 0;
                pbDocument.Maximum = 100;
                pbDocument.Value = 0;
                if (_OpenExternalSource != enum_OpenExternalSource.DirectMessage)
                {
                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && (PatientID > 0 || (_OpenExternalSource == enum_OpenExternalSource.RCM && PatientID == -1)))
                    {
                        //03-Dec-14 Aniket: Assign To user should not be mandatory for Received faxes screen
                        _blnReceivedFaxes = true;
                        ouser.UserID = oManager.GetRecieveFaxUserID();
                        ouser.UserName = oManager.GetRecieveFaxUserName(ouser.UserID);
                        // Bug #39873: 00000317 : Tasks 
                        // following condition added to resolve Bug.
                        if (ouser.UserID > 0)
                        {
                            cmbUser.Items.Add(ouser.UserName);
                            cmbUser.SelectedIndex = 0;
                        }
                        //End 20100105

                    }
                }
                else
                {
                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && PatientID > 0)
                    {
                        
                        
                        ouser.UserID = oManager.GetDirectMsgAttachmentUserID();
                        ouser.UserName = oManager.GetDirectMsgAttachUserName(ouser.UserID);
                        // Bug #39873: 00000317 : Tasks 
                        // following condition added to resolve Bug.
                        if (ouser.UserID > 0)
                        {
                            cmbUser.Items.Add(ouser.UserName);
                            cmbUser.SelectedIndex = 0;
                        }
                        //End 20100105

                    }
                }
               

                FillCategories();


                if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && (PatientID > 0 || (_OpenExternalSource == enum_OpenExternalSource.RCM && PatientID == -1)))
                {
                    _PatientId = PatientID;

                    txtDocumentName.Text = gloEDocumentV3.eDocManager.eDocValidator.GetNewDocumentName(_PatientId, "", gloEDocV3Admin.gClinicID, _OpenExternalSource);
                    txtDocumentName.Select();
                }
                else if (oSelDocuments != null && oSelDocuments.Count > 0 && PatientID > 0)
                {
                    txtDocumentName.Text = oSelDocuments[0].DocumentName;
                    txtDocumentName.Select();

                    cmbCategory.SelectedValue = oSelDocuments[0].CategoryID;
                    cmbCategory.Enabled = false;
                    txtDocumentName.ReadOnly = true;
                }

                if (cmbPriority.Items.Count >= 0)
                { cmbPriority.SelectedIndex = 0; }


                pnlControl.Visible = false;
                if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                {
                    this.Text = "Send to DMS";
                    chkSendTask.Enabled = true;
                    

                }
               
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (oManager != null)
                {
                    oManager.Dispose();
                    oManager = null;
                }
            }
        }

        #region " Form Button Click Event "

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResultIsOK = false;
            SendResult = false;
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
            tlb_Ok.Enabled = false;
            tlb_Cancel.Enabled = false;
            string oImportDocumentName = "";

            try
            {
                #region " Validation "

                if (txtDocumentName.Text == "")
                {
                    MessageBox.Show("Please enter the Document Name.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tlb_Ok.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    return;
                }
                if (cmbCategory.Text == "")
                {
                    MessageBox.Show("Please select the Category.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tlb_Ok.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    return;
                }
                if ((_OpenExternalSource == enum_OpenExternalSource.DirectMessage || _OpenExternalSource == enum_OpenExternalSource.RCM) && (chkSendTask.Checked == false))
                {

                }
                else
                {
                    if (cmbUser.Items.Count > 0)
                    {


                        if (cmbPriority.Text == "")
                        {
                            MessageBox.Show("Please select the Priority for the Task", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbPriority.Focus();
                            tlb_Ok.Enabled = true;
                            tlb_Cancel.Enabled = true;
                            return;
                        }
                        if (dtpDueDate.Value == DateTime.MinValue)
                        {
                            MessageBox.Show("Please select valid Due Date", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlb_Ok.Enabled = true;
                            tlb_Cancel.Enabled = true;
                            return;
                        }
                        DateTime dtTemp = new DateTime();
                        dtTemp = dtpDueDate.Value;
                        DateTime dtCurrent = new DateTime();
                        dtCurrent = DateTime.Now;
                        if (dtTemp.Date < dtCurrent.Date)
                        {
                            //21-Nov-14 Aniket: Resolving Bug #76279: gloEMR: Task - Due date message, space required between two sentences
                            MessageBox.Show("Task due date is elapsed. Please select valid due date.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDueDate.Focus();
                            tlb_Ok.Enabled = true;
                            tlb_Cancel.Enabled = true;
                            return;
                        }
                        // solving sales force case-GLO2010-0006201  added cmbUser.Text.Trim() != ""

                        if (cmbUser.Text.Trim() != "" && txtTask.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter Task Description.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlb_Ok.Enabled = true;
                            tlb_Cancel.Enabled = true;
                            return;
                        }

                    }
                    else
                    {
                        //03-Dec-14 Aniket: Assign To user should not be mandatory for Received faxes screen
                        if (_blnReceivedFaxes == false)
                        {
                            MessageBox.Show("Please select 'Assigned To'.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbUser.Focus();
                            tlb_Ok.Enabled = true;
                            tlb_Cancel.Enabled = true;
                            return;
                        }
                    }
                }

                #endregion
                this.Cursor = Cursors.WaitCursor;
                pbDocument.Minimum = 0;
                pbDocument.Maximum = 100;

                if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && (PatientID > 0 || (_OpenExternalSource == enum_OpenExternalSource.RCM && PatientID == -1)))
                {
                    
                    oImportInMonth = gloEDocumentV3.eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                    oImportDocumentName = txtDocumentName.Text.Trim();
                    oImportInCategoryID = System.Convert.ToInt64(cmbCategory.SelectedValue);
                    oImportInCategory = cmbCategory.Text;

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM && string.IsNullOrEmpty(oImportInSubCategory))
                    { oImportInSubCategory = DateTime.Now.ToString("MM dd yyyy"); }

                    oImportInYear = DateTime.Now.Year.ToString();
                    if (_strNonXML != "" && _OpenExternalSource ==enum_OpenExternalSource.DirectMessage )
                    {
                       
                        ArrayList SourceDocment = new ArrayList();
                        SourceDocment.Add(_strNonXML);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START" + " " + DateTime.Now.TimeOfDay);
                        oDialogResultIsOK = oManager.ImportSplit(PatientID, SourceDocment, oImportDocumentName, oImportInCategoryID,oImportInCategory,"", oImportInYear, oImportInMonth, gloEDocV3Admin.gClinicID, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, _OpenExternalSource);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED" + " " + DateTime.Now.TimeOfDay);
                       
                    }
                    else
                    {
                        oManager.ImportImages(PatientID, oSelectedDocuments, oImportDocumentName, oImportInCategoryID, oImportInCategory, oImportInSubCategory, oImportInYear, oImportInMonth, gloEDocV3Admin.gClinicID, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, _OpenExternalSource,true);
                    }

                }
                else if (oSelDocuments != null && oSelDocuments.Count > 0 && PatientID > 0)
                {
                    oImportInMonth = gloEDocumentV3.eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                    oImportDocumentName = txtDocumentName.Text.Trim();
                    oImportInCategoryID = System.Convert.ToInt64(cmbCategory.SelectedValue);
                    oImportInCategory = cmbCategory.Text;
                    oImportInYear = DateTime.Now.Year.ToString();
                    oDialogDocumentID = oSelDocuments[0].DocumentID;
                }
                if (oDialogDocumentID > 0)
                {
                    oDialogResultIsOK = true;
                    this.SendResult = oDialogResultIsOK;
                }

                if (oDialogResultIsOK == true && cmbUser.Items.Count > 0)
                {
                    if ((_OpenExternalSource ==enum_OpenExternalSource.DirectMessage || _OpenExternalSource == enum_OpenExternalSource.RCM) && (chkSendTask.Checked ==false ))
                    {

                    }
                    else
                    {
                    ArrayList TaskDetailArrList = new ArrayList();
                    Int64[] arrUser = new Int64[cmbUser.Items.Count];
                    // Add Task
                    TaskDetailArrList.Add(0);
                    //From ID
                    TaskDetailArrList.Add(System.Convert.ToInt64(gloEDocumentV3.eDocManager.eDocValidator.GetUserID(cmbUser.Text, gloEDocV3Admin.gClinicID)));
                    // 2=TaskDate
                    TaskDetailArrList.Add(DateTime.Now);
                    //3=Subject
                    string strSubject = null;
                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && PatientID > 0 && _OpenExternalSource != enum_OpenExternalSource.DirectMessage)
                    {
                        strSubject= "DMS Fax Received";
                    }
                    else if (oSelDocuments != null && oSelDocuments.Count > 0 && PatientID > 0)
                    {
                        strSubject = "DMS Doc Received";
                    }
                    else if (_strNonXML != null && _OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                    {
                        strSubject = "Direct Message DMS Doc Received";
                    }
                    else if (oSelectedDocuments != null && oSelectedDocuments.Count > 0 && _OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        strSubject = "RCM Fax Received";
                    }
                    TaskDetailArrList.Add(strSubject);
                    //4= DueDate
                    TaskDetailArrList.Add(dtpDueDate.Value);
                    //5= Priority
                    TaskDetailArrList.Add(cmbPriority.Text);
                    //6=Status
                    TaskDetailArrList.Add("Not Started");
                    //7= Notes
                    TaskDetailArrList.Add(txtTask.Text);
                    //8= PatientID
                    Int64 _PatientId = System.Convert.ToInt64(PatientID);
                    TaskDetailArrList.Add(PatientID);

                    //9. FAXTIFFFIleName
                    string DocumentInfo = oImportInYear.ToString() + "," + oDialogDocumentID.ToString();
                    TaskDetailArrList.Add(DocumentInfo);

                    for (int i = 0; i <= cmbUser.Items.Count - 1; i++)
                    {
                        arrUser[i] = System.Convert.ToInt64(gloEDocumentV3.eDocManager.eDocValidator.GetUserID(cmbUser.Items[i].ToString(), gloEDocV3Admin.gClinicID));
                    }
                    //     bool _return = AddTask(TaskDetailArrList, arrUser, TaskType.DMS);
                    //'sarika 20090206 New Task Implementation as in gloPM
                    bool _return;

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        _return = AddTasks(TaskDetailArrList, arrUser, TaskType.RCMDocs, oDialogContainerID, oDialogDocumentID);
                    }
                    else
                    {
                        _return = AddTasks(TaskDetailArrList, arrUser, TaskType.DMS, oDialogContainerID, oDialogDocumentID);
                    }
                    
                    //----
                    this.SendResult = _return;

                    if (_return)
                    {
                        //Remove page/pages code goes here
                    }
                    }
                    this.Close();
                }
                else
                {
                    this.Close();
                }
                //pbDocument.Value = 100;
                pbDocument.Value = pbDocument.Maximum;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.ToString());
                tlb_Ok.Enabled = true;
                tlb_Cancel.Enabled = true;

            }
            finally
            {
                if (oManager != null)
                {
                    oManager.Dispose();
                    oManager = null;

                }

            }
        }

        #endregion " Form Button Click Event "

        #region " Public & Private Methods "

        public bool AddTask(ArrayList TaskDetailArrList, Array UserArr, TaskType Type)
        {
            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            Database.DBParameters oParameters = null;

            try
            {
                oDB.Connect(false);
                oParameters = new Database.DBParameters();
                //oParameters.Add("@nFromID", Convert.ToInt64(TaskDetailArrList[1]), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFromID", System.Convert.ToInt64(gloEDocV3Admin.gUserID), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtTaskDate", System.Convert.ToDateTime(TaskDetailArrList[2]), ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sSubject", TaskDetailArrList[3].ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtDueDate", System.Convert.ToDateTime(TaskDetailArrList[4]), ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sPriority", TaskDetailArrList[5].ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sStatus", TaskDetailArrList[6].ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sNotes", TaskDetailArrList[7].ToString(), ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@nPatientId", System.Convert.ToInt64(TaskDetailArrList[8]), ParameterDirection.Input, SqlDbType.BigInt);
                if (Type == TaskType.DMS)
                {
                    oParameters.Add("@FAXTIFFFileName", TaskDetailArrList[9].ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                }
                oParameters.Add("@TaskType", Type, ParameterDirection.Input, SqlDbType.Int);
                //oParameters.Add("@MachineID", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                Int64 MachineId = gloEDocumentV3.eDocManager.eDocValidator.GetPrefixTransactionID(System.Convert.ToInt64(TaskDetailArrList[8]));
                oParameters.Add("@MachineID", MachineId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTaskID", System.Convert.ToInt64(TaskDetailArrList[0]), ParameterDirection.InputOutput, SqlDbType.BigInt);

                Object Result = new object();
                oDB.Execute("gsp_InUpTasks_Mst", oParameters, out Result);
                oParameters.Dispose();
                if (Result != null)
                {
                    Int64 TaskID = System.Convert.ToInt64(Result);
                    oParameters = new Database.DBParameters();
                    oParameters.Add("@nTaskID", TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                    int res = oDB.Execute("gsp_DeleteTasks_DTL", oParameters);
                    if (res > 0)
                    {
                        //on success

                    }
                    oParameters.Dispose();
                    oParameters = null;

                    for (int i = 0; i < UserArr.Length; i++)
                    {
                        oParameters = new Database.DBParameters();
                        oParameters.Add("@nTaskID", TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nToID", System.Convert.ToInt64(UserArr.GetValue(i)), ParameterDirection.Input, SqlDbType.BigInt);

                        int result = oDB.Execute("gsp_InsertTasks_DTL", oParameters);
                        if (result > 0)
                        {
                            //MessageBox.Show("Task added sucessfully", gloEDocumentAdmin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        //else
                        //{
                        //    MessageBox.Show("Problem adding Task.", gloEDocumentAdmin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                if (oDB.Connect(false))
                    oDB.Disconnect();

                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
        }

        private void FillUserCombo()
        {
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            ArrayList _UserList = new ArrayList();
            try
            {
                _UserList = oList.GetUsers(gloEDocV3Admin.gClinicID);
                if (_UserList != null)
                {
                    if (_UserList.Count > 0)
                    {
                        for (int i = 0; i < _UserList.Count; i++)
                        {
                            cmbUser.Items.Add(_UserList[i].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (_UserList != null) { _UserList = null; }
                if (oList != null) { oList.Dispose(); oList = null; }
            }
        }

        private void FillCategories()
        {
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            DataTable dtCategories = new DataTable();
            try
            {
                oList.GetCategories(gloEDocV3Admin.gClinicID, out dtCategories, _OpenExternalSource);
                if (dtCategories != null)
                {
                    if (dtCategories != null && dtCategories.Rows.Count > 0)
                    {
                        //CategoryId,CategoryName
                        cmbCategory.DataSource = dtCategories.Copy();
                        cmbCategory.DisplayMember = dtCategories.Columns["CategoryName"].ToString();
                        cmbCategory.ValueMember = dtCategories.Columns["CategoryId"].ToString();
                    }
                }

                if (cmbCategory.Items.Count > 0)
                {
                    if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                    {
                       
                        
                        string _DefaultCategory = "";
                        int _index = 0;

                        _DefaultCategory = oList.GetDirectMsgAttachmentCategory();
                        _index = cmbCategory.FindStringExact(_DefaultCategory);

                        if (_index != -1) { cmbCategory.SelectedIndex = _index; }
                        else { cmbCategory.SelectedIndex = 0; }
                    }
                    else
                    {
                        //***
                        //Code added on 20090102 - By Sagar Ghodke
                        //Code added to set the Default Fax Category from Settings 
                        string _DefaultCategory = "";
                        int _index = 0;

                        _DefaultCategory = oList.GetReceivedFaxCategory();
                        _index = cmbCategory.FindStringExact(_DefaultCategory);

                        if (_index != -1) { cmbCategory.SelectedIndex = _index; }
                        else { cmbCategory.SelectedIndex = 0; }

                        //End Code add 20090102 
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "
                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (oList != null) { oList.Dispose(); oList = null; }
            }

        }

        private void RemoveControl()
        {
            try
            {
                if (dgcustomGrid != null)
                {
                    //pnlTask.Controls.Remove(dgcustomGrid);
                    pnlGrid.Controls.Remove(dgcustomGrid);
                    dgcustomGrid.Visible = false;
                    dgcustomGrid.Dispose();
                    dgcustomGrid = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {

            }
        }

        private void LoadGrid()
        {
            try
            {
                AddControl();
                if (dgcustomGrid != null)
                {
                    dgcustomGrid.Top = pnlTask.Top + 100;
                    dgcustomGrid.Left = pnlTask.Left;
                    dgcustomGrid.Height = pnlTask.Height - 20;
                    dgcustomGrid.Visible = true;
                    dgcustomGrid.Width = pnlTask.Width;
                    dgcustomGrid.BringToFront();
                    BindGrid();
                    dgcustomGrid.Selectsearch(gloEDocumentV3.Forms.UserList.EnmControl.Search);
                    //dgcustomGrid.Label1.Visible = true;
                    //dgcustomGrid.txtsearch.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddControl()
        {
            dgcustomGrid = new gloEDocumentV3.Forms.UserList();
            pnlGrid.Controls.Add(dgcustomGrid);
            dgcustomGrid.btnOkClick += new UserList.OKClick(dgcustomGrid_btnOkClick);
            dgcustomGrid.btnCancelClick += new UserList.CancelClick(dgcustomGrid_btnCancelClick);
            dgcustomGrid.btnAddClick += new UserList.AddClick(dgcustomGrid_btnAddClick);
            dgcustomGrid.txtKeyPress+=new UserList.TextKeyPress(dgcustomGrid_txtKeyPress);
            dgcustomGrid.txtChanged  +=new UserList.TextChanged(dgcustomGrid_txtChanged);
            dgcustomGrid.Dock = DockStyle.Fill;
            dgcustomGrid.BringToFront();
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.BringToFront();
            dgcustomGrid.btnAdd.Visible = false;
            dgcustomGrid.btnOK.Visible = false;
            dgcustomGrid.btnClose.Visible = false;
            dgcustomGrid.Visible = false;
            dgcustomGrid.C1Task.AllowEditing = true;
            gloUserControlLibrary.gloC1FlexStyle.Style(dgcustomGrid.C1Task, false);
        }

        private void BindGrid()
        {
            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            Database.DBParameters oParameters = new Database.DBParameters();
            try
            {
                DataTable dt = new DataTable();
                DataColumn col = new DataColumn();

                oDB.Connect(false);
                oParameters.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("gsp_FillUsers", oParameters, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        col.ColumnName = "Select";
                        col.DataType = System.Type.GetType("System.Boolean");
                        col.DefaultValue = false;
                        dt.Columns.Add(col);

                        //' For DataBinding Users 
                        DataView dv = new DataView();
                        dv = dt.DefaultView;
                        dgcustomGrid.DataSource(dv);
                    }
                    dt.Dispose();
                    dt = null;
                }
                //Referralcount = dt.Rows.Count 
                HideColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
        }

        private void HideColumns()
        {
            try
            {


                double _TotalWidth = dgcustomGrid.C1Task.Width - 5;
                // '' Show User Info 
                //.Cols.Count = Col_Count_User 
                dgcustomGrid.C1Task.Cols.Fixed = 0;
                dgcustomGrid.C1Task.Rows.Fixed = 1;
                dgcustomGrid.C1Task.Cols.Count = 6;
                dgcustomGrid.C1Task.AllowEditing = true;

                dgcustomGrid.C1Task.SetData(0, Col_Check, "Select");
                //.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter 
                dgcustomGrid.C1Task.Cols[Col_Check].Width = System.Convert.ToInt32(_TotalWidth * 0.1);
                dgcustomGrid.C1Task.Cols[Col_Check].AllowEditing = true;
                dgcustomGrid.C1Task.Cols[Col_Check].DataType = typeof(System.Boolean);


                dgcustomGrid.C1Task.SetData(0, Col_UserID, "UserID");
                //.Cols(Col_UserID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter 
                dgcustomGrid.C1Task.Cols[Col_UserID].Width = System.Convert.ToInt32(_TotalWidth * 0);
                dgcustomGrid.C1Task.Cols[Col_UserID].AllowEditing = false;

                dgcustomGrid.C1Task.SetData(0, Col_LoginName, "Login Name");
                //.Cols(Col_LoginName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter 
                dgcustomGrid.C1Task.Cols[Col_LoginName].Width = System.Convert.ToInt32(_TotalWidth * 0.6);
                dgcustomGrid.C1Task.Cols[Col_LoginName].AllowEditing = false;

                dgcustomGrid.C1Task.SetData(0, Col_Column1, "Name");
                //.Cols(Col_Column1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter 
                dgcustomGrid.C1Task.Cols[Col_Column1].Width = System.Convert.ToInt32(_TotalWidth * 0.7);
                dgcustomGrid.C1Task.Cols[Col_Column1].AllowEditing = false;

                dgcustomGrid.C1Task.Cols[Col_ProviderID].Width = 0;
                dgcustomGrid.C1Task.Cols[Col_Column2].Width = 0;

                //Move the last column select to first column 
                dgcustomGrid.C1Task.Cols.Move(dgcustomGrid.C1Task.Cols.Count - 1, 0);
                dgcustomGrid.C1Task.Cols[0].AllowEditing = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SetGridValues()
        {
            try
            {
                for (int i = 1; i <= dgcustomGrid.C1Task.Rows.Count - 1; i++)
                {

                    if (dgcustomGrid.C1Task.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        if (FindDuplicateTo(System.Convert.ToInt64(dgcustomGrid.C1Task.GetData(i, 1).ToString())))
                        {
                            User ouser = new User();
                            ouser.UserID = System.Convert.ToInt64(dgcustomGrid.C1Task.GetData(i, 1));
                            ouser.UserName = dgcustomGrid.C1Task.GetData(i, 2).ToString();
                            cmbUser.Items.Add(ouser.UserName);
                            Object tempObj = new object();
                            tempObj = dgcustomGrid[i, 2];
                            if (tempObj != null)
                            {
                                string str = System.Convert.ToString(tempObj.ToString());
                                //cmbUser.Text = str;

                            }
                            tempObj = null;
                        }
                    }
                }

                if (cmbUser.Items.Count > 0)
                {
                    cmbUser.SelectedIndex = 0;
                }

                RemoveControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private bool FindDuplicateTo(Int64 Id)
        {
            try
            {

                for (int i = 0; i <= cmbUser.Items.Count - 1; i++)
                {
                    Int64 _UserId = gloEDocumentV3.eDocManager.eDocValidator.GetUserID(cmbUser.Items[i].ToString(), gloEDocV3Admin.gClinicID);
                    if (Id == _UserId)
                    {
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                return false;

            }

        }

        #endregion " Public & Private Methods "

        #region " Other Events "

        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            Application.DoEvents();
            int _PVal = 0;
            _PVal = pbDocument.Value + Percentage;
            if (_PVal <= pbDocument.Maximum) { pbDocument.Value = _PVal; }
        }

        private void pnlTask_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            try
            {
                tlb_Ok.Enabled = false;
                tlb_Cancel.Enabled = false;
                pnlTask.Visible = false;
                pnlControl.Visible = true;
                RemoveControl();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        void dgcustomGrid_txtKeyPress(object sender, EventArgs e)
        {
        }
        void dgcustomGrid_txtChanged(object sender, EventArgs e)
        {
            try
            {
                string value = ((System.Windows.Forms.TextBox)sender).Text.Trim();
                DataView dv = (DataView)dgcustomGrid.C1Task.DataSource;
                dv.RowFilter = "sLoginName like '%" + value.Replace("'","''")  + "%'";
            }
            catch(Exception ) //ex)
            {
                //ex = null;
            }
         }
        void dgcustomGrid_btnAddClick(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void dgcustomGrid_btnCancelClick(object sender, EventArgs e)
        {
            try
            {
                RemoveControl();
                tlb_Ok.Enabled = true;
                tlb_Cancel.Enabled = true;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        void dgcustomGrid_btnOkClick(object sender, EventArgs e)
        {
            SetGridValues();

            tlb_Ok.Enabled = true;
            tlb_Cancel.Enabled = true;
        }

        private void btnRemove_user_Click(object sender, EventArgs e)
        {
            cmbUser.Items.Remove(cmbUser.Text);
            if (cmbUser.Items.Count > 0)
            {
                cmbUser.SelectedIndex = 0;
            }
        }

        private void tlb_CntrOK_Click(object sender, EventArgs e)
        {
            try
            {
                dgcustomGrid_btnOkClick(null, null);
                pnlTask.Visible = true;
                pnlControl.Visible = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {

            }
        }

        private void tlb_CntrCancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnlControl.SendToBack();
                dgcustomGrid_btnCancelClick(null, null);
                tls_MaintainDoc.Visible = true;
                pnlTask.Visible = true;
                pnlControl.Visible = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {

            }
        }

        private void txtDocumentName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string sFileName = txtDocumentName.Text.Trim();
                string sValidFileName = "";
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");

                if (sFileName != sValidFileName)
                {
                    txtDocumentName.Text = sValidFileName;
                    txtDocumentName.Select(txtDocumentName.Text.Length, 1);
                }
            }
            catch (Exception ex)
            {
                string _ex = ex.Message;
            }
        }

        #endregion

        #region "Assign Tasks"
        //'sarika 20090206 New Task Implementation as in gloPM
        //  private void AddTasks(gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetails Users, System.DateTime TaskDate, long PatientID, long TaskId, string TaskDesc, System.DateTime TaskDueDate)
        private bool AddTasks(ArrayList TaskDetailArrList, Array UserArr, TaskType Type, Int64 oContainerID, Int64 oDocumentID)
        {
            bool _result = false;

            Int64[] ArrTasks = new Int64[UserArr.Length];
            //Dim mlist As myList 
            int i = 0;
            gloTaskMail.gloTask ogloTask = new gloTaskMail.gloTask(gloEDocV3Admin.gDatabaseConnectionString);
            try
            {

                for (i = 0; i <= UserArr.Length - 1; i++)
                {
                    ArrTasks.SetValue(System.Convert.ToInt64(UserArr.GetValue(i)), i);// Users.Item(i).ID;
                }

                
                Task oTask = new Task();
                gloTaskMail.TaskProgress oTaskProgress = new gloTaskMail.TaskProgress();

                for (i = 0; i <= UserArr.Length - 1; i++)
                {

                    TaskAssign oTaskAssign = new TaskAssign();

                    oTaskAssign.AssignFromID = System.Convert.ToInt64(gloEDocV3Admin.gUserID);// gnLoginID;
                    oTaskAssign.AssignFromName = "";// Convert.ToInt64(gloEDocV3Admin.gUserID);// gstrLoginName;
                    oTaskAssign.AssignToID = System.Convert.ToInt64(ArrTasks.GetValue(i));
                    //Sandip Darade 20091027 
                    oTaskAssign.ClinicID = gloEDocV3Admin.gClinicID;
                    if (oTaskAssign.AssignFromID == oTaskAssign.AssignToID)
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    }
                    else
                    {
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                    }



                    oTaskAssign.AssignToName = gloEDocV3Admin.gUserName;// Users.Item(i).Description;


                    //----
                    oTask.Assignment.Add(oTaskAssign);
                    oTaskAssign.Dispose();
                    oTaskAssign = null;
                }


                oTaskProgress.ClinicID = System.Convert.ToInt64(gloEDocV3Admin.gClinicID);// gnClinicID;
                oTaskProgress.Complete = 0;
                oTaskProgress.DateTime = System.Convert.ToDateTime(TaskDetailArrList[2]);
                oTaskProgress.Description = System.Convert.ToString(TaskDetailArrList[7]);
                oTaskProgress.StatusID = 1;
                //' Not Started 
                oTaskProgress.TaskID = 0;// TaskId;

                //' 
                oTask.UserID = System.Convert.ToInt64(gloEDocV3Admin.gUserID);
                oTask.TaskType = (gloTaskMail.TaskType) Type;
                oTask.PatientID = System.Convert.ToInt64(TaskDetailArrList[8]);  //PatientID
                //oTask.Subject = "DMS Received Fax";
                oTask.Subject = TaskDetailArrList[3].ToString();
                oTask.ClinicID = 1;// gnClinicID;
                oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(System.Convert.ToDateTime(TaskDetailArrList[2]).ToShortDateString()); //taskdate
                oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(System.Convert.ToDateTime(TaskDetailArrList[2]).ToShortDateString()); //taskdate
                oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(System.Convert.ToDateTime(TaskDetailArrList[4]).ToShortDateString());//taskduedate
                //oTask.FaxTiffFileName = FaxTiffFileName 
                oTask.IsPrivate = false;
                oTask.MachineName = System.Windows.Forms.SystemInformation.ComputerName;// gstrClientMachineName;
                oTask.Progress = oTaskProgress;
                oTask.ReferenceID1 = oContainerID;// _OrderParamter.OrderID;//ContainerID
                oTask.ReferenceID2 = oDocumentID; //DocumentID
                if (UserArr.Length > 1)  //added for bugid  #100203: 
                {
                    oTask.TaskGroupID = ogloTask.GetUniqueueId();
                }
                else
                {
                  oTask.TaskGroupID  =0;
                }
                  //'LabOrder ID for referance 
                //Pradeep Godse 20100908
                oTask.OwnerID = System.Convert.ToInt64(gloEDocV3Admin.gUserID);
                //end pradeep
                if (System.Convert.ToString(TaskDetailArrList[5]).ToUpper() == "HIGH")
                {
                    oTask.PriorityID = 3;
                }
                else if (System.Convert.ToString(TaskDetailArrList[5]).ToUpper() == "LOW")
                {
                    oTask.PriorityID = 1;
                }
                else
                {
                    oTask.PriorityID = 2;
                }

                ogloTask.Add(oTask);
                oTaskProgress.Dispose();
                oTask.Dispose();
                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
                throw ex;
            }
            finally
            {
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
            }


            return _result;
        }

        private void chkSendTask_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSendTask.Checked)
            {
                pnlTasksPanel.Enabled = true;
            }
            else
            {
                pnlTasksPanel.Enabled = false;

            }
        }

        //--
        #endregion
    }
}
