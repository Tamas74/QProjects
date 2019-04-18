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
    public partial class frmSetupReasonCodes : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ReasonID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64   _ClinicID = 0;
        private const int  COL_SELECT =1;
        private const int  COL_CODE = 2;
        private const int  COL_DESCRIPTION = 3;
        private string _sGroupCode = "";
        private string _ReasonCode = string.Empty;
        private string _ReasonGroupCode = string.Empty;
        private bool _IsReasonCodeSave = false;
        #endregion " Declarations "

        #region " Property Procedures "
        public string GroupCode
        {
            set { _sGroupCode = value; }
        }
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
        
        public string ReasonCode { get { return _ReasonCode; } set { _ReasonCode = value; } }
        public string ReasonGroupCode { get { return _ReasonGroupCode; } set { _ReasonGroupCode = value; } }
        public bool IsReasonCodeSave { get { return _IsReasonCodeSave; } set { _IsReasonCodeSave = value; } }
        #endregion " Property Procedures "

        public frmSetupReasonCodes(string databaseConnectionString, Int64 ReasonID)
        {
            _databaseconnectionstring = databaseConnectionString;
            _ReasonID = ReasonID;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
        }
        public frmSetupReasonCodes(string databaseConnectionString, string ReasonCode, string ReasonGroupCode)
        {
            _databaseconnectionstring = databaseConnectionString;
            _ReasonCode = ReasonCode;
            _ReasonGroupCode = ReasonGroupCode;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
        }

        private void frmSetupReasonCodes_Load(object sender, EventArgs e)
        {
            FillResonCode();
            DrsignC1GroupCode();
            FillC1GroupCode();

            gloC1FlexStyle.Style(C1GroupCode, false);
            if (ReasonCode!="" && ReasonGroupCode!="")
            {
                txtCode.Text = ReasonCode;
                for (int i = 0; i < C1GroupCode.Rows.Count; i++)
                {
                    if (Convert.ToString(C1GroupCode.GetData(i,COL_CODE)).ToUpper()==ReasonGroupCode.ToUpper())
                    {
                        C1GroupCode.SetCellCheck(i, COL_SELECT, CheckEnum.Checked);
                        txtDescription.Focus();
                        break;
                    }
                }
                C1GroupCode.Enabled = false;
            }
        }

    

        private bool Validate()
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    // Problem# 412 : Adding validation if the reason code description is blank.
                    MessageBox.Show("Please enter Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return false;
                }
               //code added by dipak 20091210 to check is recored is already exist  
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                bool isSelected = false;
                for (int i = 0; i < C1GroupCode.Rows.Count; i++)
                {
                    if (C1GroupCode.GetCellCheck(i, COL_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        string sGroupCode = Convert.ToString(C1GroupCode.GetData(i, COL_CODE));
                        string sGroupCodeDesc = Convert.ToString(C1GroupCode.GetData (i, COL_DESCRIPTION));
                        isSelected = true; 
                        if (ObjReasonCodes.IsExists(_ReasonID, txtCode.Text, txtDescription.Text, sGroupCode))
                        {
                            MessageBox.Show("Group Code : " + sGroupCode + " is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isSelected = false; 
                            return false ;
                           
                        }
                    }
                }
               //if no group code selected
                if (isSelected == false )
                {
                    MessageBox.Show("Select group code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false ;
                }
                //if (txtDescription.Text == "")
                //{
                //    MessageBox.Show("Please enter Description for Place Of Service", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtName.Focus();
                //    return false;
                //}
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

        private void FillResonCode()
        {
            //    bool _result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";

                try
                {
                    oDB.Connect(false);

                    //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                    //
                    //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";
                    strQuery = " select nReasonID, sCode, sDescription,isnull(bIsSystem,0) as bIsSystem, isnull(bIsBlock,0) as bIsBlock,  isnull(nActionID,0) as nActionID, nClinicID from BL_ReasonCodes_MST where nReasonID='" + _ReasonID + "' AND nClinicID=" + _ClinicID + " ";
                    //

                    DataTable dt = new DataTable();
                    oDB.Retrive_Query(strQuery, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtCode.Text = Convert.ToString(dt.Rows[0]["sCode"]);
                        txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                        if(Convert.ToBoolean(dt.Rows[0]["bIsSystem"])==true)
                        {
                            chkIsSystem.Checked = true;
                        }
                        else
                        {
                            chkIsSystem.Checked = false;
                        }
                         if(Convert.ToBoolean(dt.Rows[0]["bIsBlock"])==true)
                        {
                            chkIsBlock.Checked=true;
                        }
                        else
                        {
                             chkIsBlock.Checked=false;
                        }
                    }
                    

                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                }
                finally
                {

                }

                //return dt;
           
        }


        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                //code commented by dipak as multiple choice for group added
                //if (ObjReasonCodes.IsExists(_ReasonID, txtCode.Text, txtDescription.Text))
                //{
                //    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //else
                //{
                    //Modify
               
                //for loop for add multiple records.
                for (int i = 0; i < C1GroupCode.Rows.Count; i++)
                    {
                        if (C1GroupCode.GetCellCheck(i, COL_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            string sGroupCode =Convert.ToString(C1GroupCode.GetData(i, COL_CODE));
                            string sGroupCodeDesc =Convert.ToString (C1GroupCode.GetData(i, COL_DESCRIPTION));

                            Int64 _tempResult = ObjReasonCodes.AddModify(_ReasonID, txtCode.Text.Trim(), txtDescription.Text.Trim(), sGroupCode, sGroupCodeDesc, chkIsBlock.Checked, chkIsSystem.Checked);

                            // Int64 _tempResult = ObjReasonCodes.AddModify(_ReasonID, txtCode.Text.Trim(), txtDescription.Text.Trim(), chkIsBlock.Checked, chkIsSystem.Checked);
                            if (_tempResult > 0)
                            {
                                IsReasonCodeSave = true;
                                if (_ReasonID != 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Modify, "Modify Reason code", 0, _tempResult, 0, ActivityOutCome.Success);
                                }
                                else {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Add, "Add Reason code", 0, _tempResult, 0, ActivityOutCome.Success);
                                }

                                //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Add, "Add Reason code", 0, _tempResult, 0, ActivityOutCome.Failure);
                            }

                        }
                    }
                //}



                this.Close();

            }
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {

            if (Validate())
            {
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                //code commented by dipak as multiple choice for group added
                //if (ObjReasonCodes.IsExists(_ReasonID, txtCode.Text, txtDescription.Text))
                //{
                //    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //else
                //{
                    //Modify
                //end comment
                //for loop for add multiple records.
                for (int i = 0; i < C1GroupCode.Rows.Count-1; i++)
                    {
                        if (C1GroupCode.GetCellCheck(i, COL_SELECT)==C1.Win .C1FlexGrid .CheckEnum.Checked)
                        {
                            string sGroupCode =Convert.ToString(C1GroupCode.GetData(i, COL_CODE));
                            string sGroupCodeDesc = Convert.ToString((C1GroupCode.GetData(i, COL_DESCRIPTION )));

                            Int64 _tempResult = ObjReasonCodes.AddModify(_ReasonID, txtCode.Text.Trim(), txtDescription.Text.Trim(), sGroupCode,sGroupCodeDesc, chkIsBlock.Checked, chkIsSystem.Checked);
                            if (_tempResult > 0)
                            {
                                IsReasonCodeSave = true;
                                if (_ReasonID != 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Modify, "Modify Reason code", 0, _tempResult, 0, ActivityOutCome.Success);
                                }
                                else
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Add, "Add Reason code", 0, _tempResult, 0, ActivityOutCome.Success);
                                }
                                //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ReasonCodeSetup, ActivityType.Add, "Add Reason code", 0, _tempResult, 0, ActivityOutCome.Failure);
                            }
                           
                        }
                    }
                    txtCode.Text = "";
                    txtDescription.Text = "";
                    chkIsSystem.Checked = false;
                    chkIsBlock.Checked = false;
                    _ReasonID = 0;

               //} 
            }
        }

        //public bool IsExists(Int64 nID, string Code, string Name)
        //{
        //    bool _result = false;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";

        //    try
        //    {
        //        oDB.Connect(false);
                
        //            //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
        //            //
        //            //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";
        //            strQuery = " select count(nID) from BL_ReasonCodes_MST where sCode='" + Code.Replace("'", "''") + "'AND nClinicID=" + _ClinicID + " ";
        //            //
              
        //        object _intResult = null;
        //        //_intResult = oDB.Execute_Query(strQuery);
        //        _intResult = oDB.ExecuteScalar_Query(strQuery);

        //        if (_intResult != null)
        //        {
        //            if (_intResult.ToString().Trim() != "")
        //            {
        //                if (Convert.ToInt64(_intResult) > 0)
        //                {
        //                    _result = true;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //    finally
        //    {

        //    }

        //    return _result;
        //}

        //public Int64 AddModify(Int64 nID, string sCode,string sDesc,bool IsBlocked,bool IsSystem)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    Object _oResult = new object();
        //    try
        //    {
        //        //Pass 0 to Add
        //        oDB.Connect(false);

        //        oParameters.Add("@nReasonID", _ReasonID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@sDescription", txtDescription.Text, ParameterDirection.Input, SqlDbType.VarChar, 100);
        //        oParameters.Add("@bIsSystem", chkIsSystem.Checked, ParameterDirection.Input, SqlDbType.Bit, 100);
        //        oParameters.Add("@bIsBlock", chkIsBlock.Checked, ParameterDirection.Input, SqlDbType.Bit, 100);
        //        oParameters.Add("@nActionID", null, ParameterDirection.Input, SqlDbType.BigInt);

        //        oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

        //        oDB.Execute("BL_INUP_ReasonCodesMST", oParameters, out _oResult);

        //        return Convert.ToInt64(_oResult);

        //    }
        //    catch (gloDatabaseLayer.DBException dbex)
        //    {
        //        dbex.ERROR_Log(dbex.ToString());
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return 0;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        oParameters.Dispose();
        //        _oResult = null;
        //    }
        //}

        private void DrsignC1GroupCode()
        {
            C1GroupCode.Clear();
            C1GroupCode.Cols.Count = 4;
            C1GroupCode.Rows.Count = 1;
            
            C1GroupCode.SetData(0, 1, "Select");
            C1GroupCode.SetData(0, 2, "Code");
            C1GroupCode.SetData(0, 3, "Description");
            //C1GroupCode.  
            int nWidth = C1GroupCode.Width;
            C1GroupCode.Cols[0].Width = 0;
            C1GroupCode.Cols[1].Width = (int)(0.05 * (nWidth));
            C1GroupCode.Cols[2].Width = (int)(0.08 * (nWidth));
            C1GroupCode.Cols[3].Width = (int)(0.89 * (nWidth));

            C1GroupCode.Cols[1].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;
            

        }
     public  void FillC1GroupCode()
    {
        DataTable dtGroupCode = new DataTable();

        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);        
        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);


         string _sqlQuery = "";

        oDB.Connect(false);
        _sqlQuery = " SELECT sCode,sDescription FROM BL_Reason_GroupCode_MST";
        oDB.Retrive_Query(_sqlQuery, out dtGroupCode);
        
         for (int i = 0; i < dtGroupCode.Rows.Count; i++)
        {
            C1GroupCode .Rows.Add();
            int rowIndex = C1GroupCode.Rows.Count - 1;
            //C1GroupCode.SetCellCheck(rowIndex, 1, 0);
            C1GroupCode.SetCellCheck(rowIndex, 1,C1.Win.C1FlexGrid.CheckEnum.Unchecked);             
            //C1GroupCode.SetData(rowIndex, COL_SELECT,false);//Select-CheckBox
            //Select-CheckBox
            if ((Convert.ToString(dtGroupCode.Rows[i]["sCode"]) == _sGroupCode)&&(_ReasonID!=0)) 
            {
                C1GroupCode.SetCellCheck(rowIndex, 1, C1.Win.C1FlexGrid.CheckEnum.Checked );
                //C1GroupCode.SetData(rowIndex, COL_SELECT , true ); 
            }
            C1GroupCode.SetData(rowIndex, COL_CODE, Convert.ToString(dtGroupCode.Rows[i]["sCode"])); //
            C1GroupCode.SetData(rowIndex, COL_DESCRIPTION, Convert.ToString(dtGroupCode.Rows[i]["sDescription"])); //
                            
        }
        
       // C1GroupCode.DataSource = dtGroupCode;  
         //dtGroupCode=
        oDB.Disconnect();
        oDB.Dispose();
    }

        private void C1GroupCode_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1,(C1FlexGrid)sender, e.Location);
        }
       
    }
}