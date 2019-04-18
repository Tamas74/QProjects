using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gloGallery
{
    public class clsCPT
    {

        #region Variables Declaration 

        private Int64 _CPTiD = 0;
        private Int64 _nSpecialtyID;
        private Int64 _nCategoryID = 0;
        private Int64 _clinicID;


        private Decimal _Units = 0;
        private decimal _Rate = 0;
        private decimal _Charges = 0;
        private decimal _Allowed = 0;
        private decimal _ClinicFee = 0;

        private string _CPTCode = "";
        private string _Description = "";
        private string _SpecialityCode = "";
        private string _CategoryType = "";
        private string _Categorydesc = "";
        private string _CodeTypeCode = "";
        private string _CodeTypeDesc = "";
        private string _Modifier1Code = "";
        private string _Modifier1Desc = "";
        private string _Modifier2Code = "";
        private string _Modifier2Desc = "";
        private string _Modifier3Code = "";
        private string _Modifier3Desc = "";
        private string _Modifier4Code = "";
        private string _Modifier4Desc = "";
        private string _NDCCode = "";
        private string _messageBoxCaption = "";
        private string _sStatementDesc = String.Empty;
        private string _sRevenueCode = String.Empty;
        private string _databaseConnectionString = String.Empty;
        public String gstrSQLError = "Error while establishing connection with the server";
        
        private bool _IsCPTDrug = false;
        private bool _IsTaxable = false;
        private bool _IsUseFromFeeSchedule = false;
        private bool _Inactive = false;

        private DataView dv;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion Variables Declaration

        # region Propeties

        public Int64 CPTID
        {
            get { return _CPTiD; }
            set { _CPTiD = value; }
        }
        public string CPTCode
        {
            get { return _CPTCode; }
            set { _CPTCode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Int64 nSpecialtyID
        {
            get { return _nSpecialtyID; }
            set { _nSpecialtyID = value; }
        }
        public Int64 nCategoryID
        {
            get { return _nCategoryID; }
            set { _nCategoryID = value; }
        }

        public string SpecialityCode
        {
            get { return _SpecialityCode; }
            set { _SpecialityCode = value; }
        }
        public string CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
        }
        public string Categorydesc
        {
            get { return _Categorydesc; }
            set { _Categorydesc = value; }
        }
        public string CodeTypeCode
        {
            get { return _CodeTypeCode; }
            set { _CodeTypeCode = value; }
        }
        public string CodeTypeDesc
        {
            get { return _CodeTypeDesc; }
            set { _CodeTypeDesc = value; }
        }
        public string Modifier1Code
        {
            get { return _Modifier1Code; }
            set { _Modifier1Code = value; }
        }
        public string Modifier1Desc
        {
            get { return _Modifier1Desc; }
            set { _Modifier1Desc = value; }
        }
        public string Modifier2Code
        {
            get { return _Modifier2Code; }
            set { _Modifier2Code = value; }
        }
        public string Modifier2Desc
        {
            get { return _Modifier2Desc; }
            set { _Modifier2Desc = value; }
        }
        public string Modifier3Code
        {
            get { return _Modifier3Code; }
            set { _Modifier3Code = value; }
        }
        public string Modifier3Desc
        {
            get { return _Modifier3Desc; }
            set { _Modifier3Desc = value; }
        }
        public string Modifier4Code
        {
            get { return _Modifier4Code; }
            set { _Modifier4Code = value; }
        }
        public string Modifier4Desc
        {
            get { return _Modifier4Desc; }
            set { _Modifier4Desc = value; }
        }
        public Decimal Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public bool IsCPTDrug
        {
            get { return _IsCPTDrug; }
            set { _IsCPTDrug = value; }
        }
        public string NDCCode
        {
            get { return _NDCCode; }
            set { _NDCCode = value; }
        }
        public bool IsTaxable
        {
            get { return _IsTaxable; }
            set { _IsTaxable = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = (decimal)value; }
        }
        public decimal Charges
        {
            get { return _Charges; }
            set { _Charges = value; }
        }
        public decimal Allowed
        {
            get { return _Allowed; }
            set { _Allowed = value; }
        }
        public bool IsUseFromFeeSchedule
        {
            get { return _IsUseFromFeeSchedule; }
            set { _IsUseFromFeeSchedule = value; }
        }
        public decimal ClinicFee
        {
            get { return _ClinicFee; }
            set { _ClinicFee = value; }
        }
        public bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        public string StatementDesc
        {
            get { return _sStatementDesc; }
            set { _sStatementDesc = value; }
        }

        public string RevenueCode
        {
            //Added for Bug Id 5386.
            get
            {
                return _sRevenueCode;
            }
            set
            {
                _sRevenueCode = value;
            }
            //End
        }
        public bool CPTTriggrs { get; set; }
        public Int32 PeriodDays { get; set; }
        public string BillingReminder { get; set; }

        #region CPT Activation And Deactivation        
        public DateTime ActivationDate { get; set; }        
        public DateTime DeactivationDate { get; set; } 
        #endregion

        # endregion Propeties

        # region Constructors

        public clsCPT()
        {
        }

        public clsCPT(string sConnectionString)
        {
            _databaseConnectionString=sConnectionString;

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

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

            #endregion
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

        ~clsCPT()
        {
            Dispose(false);
        }

        # endregion Constructors

        # region Methods

        public DataView Search(DataView dv, int colIndex, string txtSearch)
        {
            if (dv != null)
            {
                dv.RowFilter = "" + dv.Table.Columns[colIndex].ColumnName + " Like '" + txtSearch + "%'";
            }
            return dv;
        }

        public DataView GetAllCPT(long ID)
        {
            //Public Function GetAllCPT(ByVal ID As Long)
            //Dim strText As String
            //Dim objBusLayer As New clsBuslayer
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_ViewCPT_MST", oParameters, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Load, "clsCPT -- GetAllCPT -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            DataView dv = new DataView(dt);
            return dv;
        }

        public Boolean IsCPTCodeInUse(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            Boolean _isCPTInUse = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPTCode", sCPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("Check_For_IsCPTCodeInUse", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["CPTCount"]) > 0)
                    {
                        _isCPTInUse = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _isCPTInUse;
        }

        public DataTable GetAllCPT(string Speciality = "", string Category = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@Speciality", Speciality, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@Category", Category, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetAllCPT", oParameters, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dt;
        }

        public bool CheckDuplicate(string CPTCode, string description)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            object oResult = null;
            try
            {
                string strQuery = "SELECT COUNT(*) FROM CPT_MST WHERE sCPTCode = '" + CPTCode + "' AND sDescription = '" + description + "'";
                oDB.Connect(false);
                oResult=oDB.ExecuteScalar_Query(strQuery);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            if (Convert.ToInt32(oResult) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDuplicate(long ID, string CPTCode, Int64 CategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 rowAffected = 0;
            try
            {
                oParameters.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CPTCode", CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@CategoryID", CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                rowAffected=Convert.ToInt64(oDB.ExecuteScalar("gsp_CheckCPT_MST", oParameters));
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- CheckDuplicate -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SelectCPT(long ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@CPTID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_ScanCPT_MST", oParameters,out dt);
                oDB.Disconnect();
                dv = dt.DefaultView;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- SelectCPT -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SelectCategory(long ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@CategoryID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("gsp_ScanCategory_MST", oParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public DataTable GetAllSpeciality()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_FillSpecialty_MST", out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dt;
        }

        public DataTable GetAllCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@CategoryType", "CPT", ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_FillCategory_MST", oParameters, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dt;
        }

        public void AddNewCPT(long ID, string CPTCode, string Description, long SpecialtyID, long CategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@CPTID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CPTCode", CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@SpecialtyID", SpecialtyID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CategoryID", CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("gsp_InUpCPT_MST", oParameters);
                oDB.Disconnect();
                if (ID != 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Modify, "CPT Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "CPT Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "clsCPT -- AddNewCPT -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void DeleteCPT(long ID, string CPTCode)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@id", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("gsp_DeleteCPT_MST", oParameters);
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "CPT Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "clsCPT -- DeleteCPT -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

        }

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intresult = 0;
                oParameters.Add("@CPTID", CPTID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sCPTCode", CPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sDescription", Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sSpecialityCode", SpecialityCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCategoryType", CategoryType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCategoryDesc", Categorydesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCodeTypeCode", CodeTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCodeTypeDesc", CodeTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier1Code", Modifier1Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier1Desc", Modifier1Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier2Code", Modifier2Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier2Desc", Modifier2Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier3Code", Modifier3Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier3Desc", Modifier3Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier4Code", Modifier4Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier4Desc", Modifier4Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nUnits", Units, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bIsCPTDrug", IsCPTDrug, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@sNDCCode", NDCCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@bIsTaxable", IsTaxable, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nRate", Rate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@nCharges", Charges, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@nAllowed", Allowed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bIsUseFromFeeSchedule", IsUseFromFeeSchedule, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nClinicFee", ClinicFee, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bInactive", Inactive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nClinicID", _clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sStatementDesc", StatementDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sRevenueCode", RevenueCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bCPTTriggrs", CPTTriggrs, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nCategoryID", nCategoryID, ParameterDirection.Input, SqlDbType.BigInt);

                if (this.ActivationDate != DateTime.MinValue)
                { oParameters.Add("@dtActivationDate", this.ActivationDate, ParameterDirection.Input, SqlDbType.Date); }
                else
                { oParameters.Add("@dtActivationDate", DBNull.Value, ParameterDirection.Input, SqlDbType.Date); }

                if (this.DeactivationDate != DateTime.MinValue)
                { oParameters.Add("@dtInactivationDate", this.DeactivationDate, ParameterDirection.Input, SqlDbType.Date); }
                else
                { oParameters.Add("@dtInactivationDate", DBNull.Value, ParameterDirection.Input, SqlDbType.Date); }
                                
                if (PeriodDays == -1)
                {
                    oParameters.Add("@nPeriodDays", null, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                else
                {
                    oParameters.Add("@nPeriodDays", PeriodDays, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                oParameters.Add("@sBillingReminder", BillingReminder, ParameterDirection.Input, SqlDbType.VarChar);

                _result = oDB.Execute("gsp_InUpCPT_MST", oParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
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
                oParameters.Dispose();
                oDB.Dispose();

            }
            return _result;
        }

        public bool IsExistCPT(string CPTCode)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("sCPTCode", CPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDB.Connect(false);

                var res = oDB.ExecuteScalar("gsp_IsExistsCPT", oParameters);

                if (res != null && (Convert.ToInt32(res) > 0))
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public void SortDataview(string strsort, string strSortOrder = "")
        {
            //DCatview.Sort = strsort
            dv.Sort = "[" + strsort + "]" + strSortOrder;
        }
        public void SetRowFilter(string txtSearch)
        {
            string strexpr = null;
            string str = null;
            str = dv.Sort;
            str = Splittext(str);
            str = str.Substring(2);
            str = str.Substring(1, str.Length - 1);
            strexpr = "" + dv.Table.Columns[str].ColumnName + "  Like '" + txtSearch + "%'";
            dv.RowFilter = strexpr;
        }
        private string Splittext(string strsplittext)
        {
            string[] arrstring = null;
            try
            {

                if (!string.IsNullOrEmpty(strsplittext.Trim()))
                {
                    char splitchar = ' ';
                    arrstring = strsplittext.Split(splitchar);
                    return arrstring[0];
                }
                else
                {
                    return strsplittext;
                }
            }
            catch //(Exception ex)
            {
                return strsplittext;
            }
        }

        # endregion Methods
    }
}
