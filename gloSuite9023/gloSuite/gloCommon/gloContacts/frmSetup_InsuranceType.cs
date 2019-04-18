using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloContacts
{
    public partial class frmSetup_InsuranceType : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _InsuranceTypeID = 0;
        private string _InsuranceTypeDescription = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 1;
        #endregion " Declarations "

        #region " Constructor "

        public frmSetup_InsuranceType(string DatabaseConnectionString, Int64 TypeID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _InsuranceTypeID = TypeID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        public frmSetup_InsuranceType(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        #endregion " Constructor "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 InsurancetypeID
        {
            get { return _InsuranceTypeID; }
            set { _InsuranceTypeID = value; }
        }
        public String InsuranceTypeDescription
        {
            get { return _InsuranceTypeDescription; }
            set { _InsuranceTypeDescription = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion

        #region " Methods "

        private bool Validate()
        {
            try
            {
                if (txtTypeCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter plan type code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeCode.Focus();
                    return false;
                }

                if (txtTypeDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Enter plan type.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    txtTypeDescription.Focus();
                    return false;
                }

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

        public void DeleteInsuranceType(Int64 InsuranceTypeID)
        {
            InsuranceType oInsuranceType = new InsuranceType(_databaseconnectionstring);
            try
            {
                oInsuranceType.Delete(InsuranceTypeID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oInsuranceType != null) { oInsuranceType.Dispose(); oInsuranceType = null; }
            }

        }

        private void FillInsuranceType(Int64 InsuranceTypeID)
        {
            InsuranceType oInsuranceType = new InsuranceType(_databaseconnectionstring);
            DataTable dttype = null;

            try
            {
                if (InsuranceTypeID > 0)
                {
                    dttype = oInsuranceType.GetInsuranceType(InsuranceTypeID);
                    if (dttype != null && dttype.Rows.Count > 0)
                    {
                        txtTypeCode.Text = dttype.Rows[0]["sTypeCode"].ToString();
                        txtTypeDescription.Text = dttype.Rows[0]["sTypeDesc"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oInsuranceType != null) { oInsuranceType.Dispose(); oInsuranceType = null; }
                if (dttype != null) { dttype.Dispose(); dttype = null; }
            }
        }

        #endregion " Methods "

        private void frmSetup_InsuranceType_Load(object sender, EventArgs e)
        {
            try
            {
                FillInsuranceType(InsurancetypeID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }


        private void tls_InsuranceType_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempResult = 0;
            InsuranceType oInsurancetype = null;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "SaveClose":
                        {

                            if (Validate())
                            {
                                oInsurancetype = new InsuranceType(_databaseconnectionstring);
                                oInsurancetype.ClinicID = _ClinicID;
                                oInsurancetype.InduranceTypeDesc = txtTypeDescription.Text.Trim();
                                oInsurancetype.InsuranceTypeCode = txtTypeCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancetype.CheckDuplicate(_InsuranceTypeID, txtTypeCode.Text.Trim(), txtTypeDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancetypeID > 0)
                                {
                                    _tempResult = oInsurancetype.Modify(InsurancetypeID);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Plan Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancetype.Add();
                                    _InsuranceTypeID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Plan Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                _InsuranceTypeDescription = txtTypeDescription.Text;
                                this.DialogResult = DialogResult.Yes;

                                this.Close();
                            }
                        }    //Case "OK"

                        break;

                    case "Save":
                        {

                            if (Validate())
                            {
                                oInsurancetype = new InsuranceType(_databaseconnectionstring);
                                oInsurancetype.ClinicID = _ClinicID;
                                oInsurancetype.InduranceTypeDesc = txtTypeDescription.Text.Trim();
                                oInsurancetype.InsuranceTypeCode = txtTypeCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancetype.CheckDuplicate(_InsuranceTypeID, txtTypeCode.Text.Trim(), txtTypeDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancetypeID > 0)
                                {
                                    _tempResult = oInsurancetype.Modify(InsurancetypeID);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Plan Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancetype.Add();
                                    _InsuranceTypeID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Plan Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }

                                txtTypeCode.Text = "";
                                txtTypeDescription.Text = "";
                                _InsuranceTypeID = 0;
                                txtTypeCode.Focus();
                            }
                        }
                        break;


                    case "Cancel":
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Plan Type", 0, _tempResult, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);


            }
            finally
            {
                if (oInsurancetype != null) { oInsurancetype.Dispose(); oInsurancetype = null; }
            }

        }
    }


    #region "Insurance Type"

    public class InsuranceType : IDisposable
    {
        #region " Private Variables "

        private Int64 _nInsuranceTypeID = 0;
        private string _sTypeCode = "";
        private string _sTypeDecription = "";
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Private Variables "
        //

        #region Properties

        public Int64 InsuranceTypeID
        {
            get { return _nInsuranceTypeID; }
            set { _nInsuranceTypeID = value; }
        }

        public string InsuranceTypeCode
        {
            get { return _sTypeCode; }
            set { _sTypeCode = value; }
        }

        public string InduranceTypeDesc
        {
            get { return _sTypeDecription; }
            set { _sTypeDecription = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public InsuranceType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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


        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~InsuranceType()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// To Modify InsurancePlan to database   
        /// </summary>
        /// <returns></returns>
        public Int64 Modify(Int64 InsuranceTypeID)
        {
            Int64 _result = 0;
            String strSQL = "";
            _nInsuranceTypeID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem
                strSQL = "UPDATE InsuranceType_MST SET "
                + " sInsuranceTypeCode = '" + _sTypeCode.Replace("'", "''").Trim() + "', sInsuranceTypeDesc = '" + _sTypeDecription.Replace("'", "''").Trim() + "', "
                + " nClinicID = " + _ClinicID + " , bIsSystem = 0"
                + " WHERE nInsuranceTypeID = " + InsuranceTypeID;

                _result = oDB.Execute_Query(strSQL);

                if (_result > 0)
                    return InsuranceTypeID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSQL = null;
            }
        }

        /// <summary>
        /// To Insert new InsurancePlan to database   
        /// </summary>
        /// <returns> New InsurancePlanID </returns>
        public Int64 Add()
        {
            Int64 _result = 0;
            String strSql = "";
            _nInsuranceTypeID = 0;
            Int64 _replicationId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem

                //..**Code changes made on 20090520 By - Sagar Ghodke
                //..**Code changes made to implement replication id
                //..Below commented code is previous code

                //strSql = "Select ISNULL(MAX(nInsuranceTypeID),0) +1 FROM InsuranceType_MST ";
                //_nInsuranceTypeID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                _replicationId = oDB.GetPrefixTransactionID(0);

                strSql = " IF EXISTS(SELECT nInsuranceTypeID FROM InsuranceType_MST WHERE convert(varchar(18),nInsuranceTypeID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
               " SELECT  isnull(max(nInsuranceTypeID),0)+1 FROM InsuranceType_MST where convert(varchar(18),nInsuranceTypeID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
               " ELSE " +
               " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                _nInsuranceTypeID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                //..**End code changes made on 20090520,Sagar Ghodke

                strSql = "INSERT INTO InsuranceType_MST (nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem) " +
                       " VALUES (" + _nInsuranceTypeID + ",'" + _sTypeDecription.Replace("'", "''") + "','" + _sTypeCode.Replace("'", "''") + "'," + _ClinicID + ",0)";

                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nInsuranceTypeID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSql = null;
            }
           // return _result;
        }


        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from InsuranceType_MST where nInsuranceTypeID =" + ID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }
        }



        /// <summary>
        /// this method checks whether given InsurancePlan code is already present in database
        /// returns true if alredy exists
        /// </summary>
        /// <param name="InsurancePlanId"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal bool CheckDuplicate(Int64 _InsuranceTypeId, string _InsuranceTypeCode, string _InsuranceTypeDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            try
            {

                oDB.Connect(false);
                if (_InsuranceTypeId == 0)
                {//nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem

                    strQuery = " select count(nInsuranceTypeID) FROM InsuranceType_MST where sInsuranceTypeCode = '" + _InsuranceTypeCode.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    strQuery = " select count(nInsuranceTypeID) FROM InsuranceType_MST where sInsuranceTypeCode = '" + _InsuranceTypeCode.Replace("'", "''") + "' AND nInsuranceTypeID <> " + _InsuranceTypeId + "  AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }
            return _result;
        }

        /// <summary>
        /// Get All the Details of Selected InsurancePlan
        /// </summary>
        /// <param name="_InsurancePlanID"></param>
        /// <returns></returns>
        internal DataTable GetInsuranceType(long _InsuranceTypeID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String strQuery = null;
            try
            {
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(sInsuranceTypeCode,'') AS sTypeCode, ISNULL(sInsuranceTypeDesc,'') AS sTypeDesc, ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsSystem,'false') AS bIsSystem "
                                + " FROM InsuranceType_MST WHERE nInsuranceTypeID = " + _InsuranceTypeID + "  AND nClinicID = " + ClinicID;
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }
        }

        /// <summary>
        ///  Get All unblocked the Insurance Plans of the Clinic
        /// </summary>
        /// <returns></returns>
        internal DataTable GetInsuranceTypes()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String strQuery = null;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nInsuranceTypeID,ISNULL(sInsuranceTypeCode,'') AS sTypeCode, ISNULL(sInsuranceTypeDesc,'') AS sTypeDesc FROM InsuranceType_MST where nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
            }
        }


    }

    #endregion
}