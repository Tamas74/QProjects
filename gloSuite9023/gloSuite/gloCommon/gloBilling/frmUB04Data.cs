using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;
using gloBilling;

namespace gloBilling
{
    public partial class frmUB04Data : Form
    {

        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";

        private string sMinDos = String.Empty;


      //  private Int64 _CPTMappingId = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 ID = 16;
    //    private string _CPTMappingName = "";
        System.Collections.ArrayList _DetailCPTID = new System.Collections.ArrayList();


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public gloGridListControl ogloGridListControl = null;
        private dsChargesTVP oUb04Data_TVP = new dsChargesTVP();
        public Common.UB04Data _oUB = new global::gloBilling.Common.UB04Data();
        public Common.UB04Datas _oUBs = new Common.UB04Datas();
        private Int64 _TransactionID = 0;
        string c1name = "";
        Panel pnlContainedGloListcontrol = new Panel();
        private bool _IsValidDate = true;
        C1FlexGrid _c1flexGridforcmuDelete = new C1FlexGrid();
     //   string c1nameforcmuDelete = "";
        public Boolean _bIsVoid = false;       

        #endregion "Private Variables"

        #region "Properties"


        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        //public Common.UB04Data Ub04DataTemp
        //{
        //    get { return _oUB; }
        //    set { _oUB = value; }
        //}
        public dsChargesTVP Ub04Data_TVP
        {
            get { return oUb04Data_TVP; }
            set { oUb04Data_TVP = value; }
        }
        #endregion

        #region "Constructor and Form Load"

        public frmUB04Data(string databaseConnectionString, Int64 TransactionID, Common.UB04Data oUB, string MinDate)
        {

                InitializeComponent();
                sMinDos = MinDate;
                this.ShowInTaskbar = false;
                _databaseconnectionstring = databaseConnectionString;
                _TransactionID = TransactionID;
                _oUB = oUB;
                _oUBs.Add(oUB);
                if (_oUB != null)
                {
                    if (_oUB.TypeofbillDeleted == false)
                    {
                        txttypeofbilling.Text = _oUB.sTypeofbill;                      
                       
                    }
                    if (_oUB.AdmitTypeDeleted == false)
                    {
                        txtAdmissionType.Text = _oUB.sAdmissionType;
                    }
                    if (_oUB.DischargeStatusDeleted == false)
                    {
                        txtDischargeStatus.Text = _oUB.sDischargeStatus;
                    }
                    txtAdmtHour.Text = _oUB.sAdmitHour;
                    if (_oUB.sAdmitDate != "")
                    {
                        mskAdmitDate.Text = Convert.ToDateTime(_oUB.sAdmitDate).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskAdmitDate.Text = "";
                    }
                    txtdischargeHour.Text = _oUB.sDischargeHour;
                }
                else
                {
                    //_oUB = new Common.UB04Data();
                }
                FillCondition(ID);
                FillOccurenceCode(ID);
                FillOccurenceSpanCode(ID);
                FillValueCodesandAmounts(ID);

                c1OccrenceCode.Cols[3].DataType = typeof(System.DateTime);
                c1OccrenceCode.Cols[3].DataType = typeof(System.DateTime);
                c1OccrenceCode.Cols[3].Format = "MM/dd/yyyy";

                c1OccurenceSpanCodeRange.Cols[3].DataType = typeof(System.DateTime);
                c1OccurenceSpanCodeRange.Cols[3].DataType = typeof(System.DateTime);
                c1OccurenceSpanCodeRange.Cols[3].Format = "MM/dd/yyyy";


                c1OccurenceSpanCodeRange.Cols[4].DataType = typeof(System.DateTime);
                c1OccurenceSpanCodeRange.Cols[4].DataType = typeof(System.DateTime);
                c1OccurenceSpanCodeRange.Cols[4].Format = "MM/dd/yyyy";
        }

        private void frmUB04Data_Load(object sender, EventArgs e)
        {

            // InitializeComponent();


            if (_bIsVoid == true)
            { ts_btnSaveClose.Enabled = false; }

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }

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

            //Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            //Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            //tom.SetTabOrder(scheme);
        }

        #endregion

        #region "Private Function"

        private void FillCondition(Int64 ID)
        {
            DataTable dt = new DataTable();
            //_CPTMappingId = 0;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;

                _sqlRetrieveQuery = "select sConditionCode ,sDescription  from UB_ConditionCodes ";

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }

                //_sqlRetrieveQuery = "select Isnull(sSettingsValue,'') as sSettingsValue from Settings where sSettingsName = 'UB04_TypeOfBill'";

                //object typeofbill = null;
                //typeofbill = oDB.ExecuteScalar_Query(_sqlRetrieveQuery);

                //if (_oUB !=null && _oUB.IsModify == false)
                //{
                //    if (typeofbill != null)
                //    {
                //        txttypeofbilling.Text = Convert.ToString(typeofbill);
                //    }
                //}

                int row = 0;
                int a = _oUBs.Count;
                int nConditioncodeNo = 18;
                if (dt != null)
                {
                    for (int i = 5; i < 16; i++)
                    {
                        c1ConditionCode.Rows.Add();
                        row = row + 1;
                        c1ConditionCode.SetData(row, 0, nConditioncodeNo);
                        if (row == 1)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode01);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode01 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode01 + "'")[0][1].ToString());
                            }

                        }
                        if (row == 2)
                        {

                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode02);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode02 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode02 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 3)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode03);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode03 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode03 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 4)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode04);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode04 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode04 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 5)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode05);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode05 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode05 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 6)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode06);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode06 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode06 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 7)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode07);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode07 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode07 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 8)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode08);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode08 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode08 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 9)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode09);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode09 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode09 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 10)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode10);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode10 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode10 + "'")[0][1].ToString());
                            }
                        }
                        if (row == 11)
                        {
                            c1ConditionCode.SetData(row, 1, _oUB.sConditionCode11);
                            if (dt.Select("sConditionCode='" + _oUB.sConditionCode11 + "'").GetLength(0) > 0)
                            {
                                c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode11 + "'")[0][1].ToString());
                            }
                        }
                        //if (row == 12)
                        //{
                        //    c1ConditionCode.SetData(row, 1, _oUB.sConditionCode12);
                        //    if (dt.Select("sConditionCode='" + _oUB.sConditionCode12 + "'").GetLength(0) > 0)
                        //    {
                        //        c1ConditionCode.SetData(row, 2, dt.Select("sConditionCode='" + _oUB.sConditionCode12 + "'")[0][1].ToString());
                        //    }
                        //}

                        nConditioncodeNo = nConditioncodeNo + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void FillOccurenceCode(Int64 ID)
        {
            DataTable dt = new DataTable();
            //_CPTMappingId = 0;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;

                _sqlRetrieveQuery = "select sOccurrenceCode  ,sDescription  from UB_OccurrenceCodes  ";

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }

                int row = 1;
                for (int i = 4; i < 20; i++)
                {
                    c1OccrenceCode.Rows.Add();

                    if (row == 1)
                    {
                        c1OccrenceCode.SetData(row, 0, "31a");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode01);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode01 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode01 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate01 != null && _oUB.sOccurrenceDate01 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate01);
                        }
                        else
                        {
                            if (_oUB.IsModify == false)
                            {
                                if (sMinDos !=null && sMinDos.Trim() != "")
                                c1OccrenceCode.SetData(row, 3, sMinDos);
                            }
                        }
                    }
                    else if (row == 5)
                    {
                        c1OccrenceCode.SetData(row, 0, "31b");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode05);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode05 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode05 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate05 != null && _oUB.sOccurrenceDate05 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate05);
                        }

                    }
                    else if (row == 2)
                    {
                        c1OccrenceCode.SetData(row, 0, "32a");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode02);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode02 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode02 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate02 != null && _oUB.sOccurrenceDate02 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate02);
                        }

                    }
                    else if (row == 6)
                    {
                        c1OccrenceCode.SetData(row, 0, "32b");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode06);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode06 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode06 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate06 != null && _oUB.sOccurrenceDate06 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate06);
                        }

                    }
                    else if (row == 3)
                    {
                        c1OccrenceCode.SetData(row, 0, "33a");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode03);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode03 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode03 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate03 != null && _oUB.sOccurrenceDate03 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate03);
                        }

                    }
                    else if (row == 7)
                    {
                        c1OccrenceCode.SetData(row, 0, "33b");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode07);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode07 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode07 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate07 != null && _oUB.sOccurrenceDate07 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate07);
                        }

                    }
                    else if (row == 4)
                    {
                        c1OccrenceCode.SetData(row, 0, "34a");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode04);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode04 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode04 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate04 != null && _oUB.sOccurrenceDate04 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate04);
                        }

                    }
                    else if (row == 8)
                    {
                        c1OccrenceCode.SetData(row, 0, "34b");
                        c1OccrenceCode.SetData(row, 1, _oUB.sOccurrenceCode08);
                        if (dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode08 + "'").GetLength(0) > 0)
                        {
                            c1OccrenceCode.SetData(row, 2, dt.Select("sOccurrenceCode='" + _oUB.sOccurrenceCode08 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceDate08 != null && _oUB.sOccurrenceDate08 != "")
                        {
                            c1OccrenceCode.SetData(row, 3, _oUB.sOccurrenceDate08);
                        }

                    }

                    ++i;
                    row = row + 1;

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void FillOccurenceSpanCode(Int64 ID)
        {
            DataTable dt = new DataTable();
            //_CPTMappingId = 0;
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;

                _sqlRetrieveQuery = "select sOccurrenceSpanCode,sDescription  from UB_OccurrenceSpanCodes ";

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }

                int row = 1;
                for (int i = 4; i < 16; i++)
                {
                    c1OccurenceSpanCodeRange.Rows.Add();

                    int a = c1OccurenceSpanCodeRange.Rows.Count;
                    if (row == 1)
                    {
                        c1OccurenceSpanCodeRange.SetData(row, 0, "35a");
                        c1OccurenceSpanCodeRange.SetData(row, 1, _oUB.sOccurrenceSpanCode01);
                        if (dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode01 + "'").GetLength(0) > 0)
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 2, dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode01 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceFromSpanDate01 != null && _oUB.sOccurrenceFromSpanDate01 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 3, _oUB.sOccurrenceFromSpanDate01);
                        }
                        if (_oUB.sOccurrenceTOSpanDate01 != null && _oUB.sOccurrenceTOSpanDate01 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 4, _oUB.sOccurrenceTOSpanDate01);
                        }
                    }
                    else if (row == 3)
                    {
                        c1OccurenceSpanCodeRange.SetData(row, 0, "35b");
                        c1OccurenceSpanCodeRange.SetData(row, 1, _oUB.sOccurrenceSpanCode03);
                        if (dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode03 + "'").GetLength(0) > 0)
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 2, dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode03 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceFromSpanDate03 != null && _oUB.sOccurrenceFromSpanDate03 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 3, _oUB.sOccurrenceFromSpanDate03);
                        }
                        if (_oUB.sOccurrenceToSpanDate03 != null && _oUB.sOccurrenceToSpanDate03 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 4, _oUB.sOccurrenceToSpanDate03);
                        }
                    }
                    else if (row == 2)
                    {
                        c1OccurenceSpanCodeRange.SetData(row, 0, "36a");
                        c1OccurenceSpanCodeRange.SetData(row, 1, _oUB.sOccurrenceSpanCode02);
                        if (dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode02 + "'").GetLength(0) > 0)
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 2, dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode02 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceFromSpanDate02 != null && _oUB.sOccurrenceFromSpanDate02 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 3, _oUB.sOccurrenceFromSpanDate02);
                        }
                        if (_oUB.sOccurrenceToSpanDate02 != null && _oUB.sOccurrenceToSpanDate02 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 4, _oUB.sOccurrenceToSpanDate02);
                        }
                    }
                    else if (row == 4)
                    {
                        c1OccurenceSpanCodeRange.SetData(row, 0, "36b");
                        c1OccurenceSpanCodeRange.SetData(row, 1, _oUB.sOccurrenceSpanCode04);
                        if (dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode04 + "'").GetLength(0) > 0)
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 2, dt.Select("sOccurrenceSpanCode='" + _oUB.sOccurrenceSpanCode04 + "'")[0][1].ToString());
                        }
                        if (_oUB.sOccurrenceFromSpanDate04 != null && _oUB.sOccurrenceFromSpanDate04 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 3, _oUB.sOccurrenceFromSpanDate04);
                        }
                        if (_oUB.sOccurrenceToSpanDate04 != null && _oUB.sOccurrenceToSpanDate04 != "")
                        {
                            c1OccurenceSpanCodeRange.SetData(row, 4, _oUB.sOccurrenceToSpanDate04);
                        }
                    }

                    i = i + 2;
                    row = row + 1;
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void FillValueCodesandAmounts(Int64 ID)
        {
            DataTable dt = new DataTable();
            //_CPTMappingId = 0;
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;
                _sqlRetrieveQuery = "select sValueCode,sDescription  from  UB_ValueCodes ";

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }

                int row = 1;
                for (int i = 4; i < 28; i++)
                {
                    c1ValueCode.Rows.Add();

                    if (row == 1)
                    {
                        c1ValueCode.SetData(row, 0, "39a");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode01);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode01 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode01 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount01);
                    }
                    else if (row == 4)
                    {
                        c1ValueCode.SetData(row, 0, "39b");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode04);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode04 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode04 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount04);
                    }
                    else if (row == 7)
                    {
                        c1ValueCode.SetData(row, 0, "39c");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode07);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode07 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode07 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount07);
                    }
                    else if (row == 10)
                    {
                        c1ValueCode.SetData(row, 0, "39d");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode10);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode10 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode10 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount10);
                    }
                    else if (row == 2)
                    {
                        c1ValueCode.SetData(row, 0, "40a");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode02);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode02 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode02 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount02);
                    }
                    else if (row == 5)
                    {
                        c1ValueCode.SetData(row, 0, "40b");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode05);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode05 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode05 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount05);
                    }
                    else if (row == 8)
                    {
                        c1ValueCode.SetData(row, 0, "40c");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode08);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode08 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode08 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount08);
                    }
                    else if (row == 11)
                    {
                        c1ValueCode.SetData(row, 0, "40d");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode11);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode11 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode11 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount11);
                    }
                    else if (row == 3)
                    {
                        c1ValueCode.SetData(row, 0, "41a");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode03);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode03 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode03 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount03);
                    }
                    else if (row == 6)
                    {
                        c1ValueCode.SetData(row, 0, "41b");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode06);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode06 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode06 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount06);
                    }
                    else if (row == 9)
                    {
                        c1ValueCode.SetData(row, 0, "41c");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode09);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode09 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode09 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount09);
                    }
                    else if (row == 12)
                    {
                        c1ValueCode.SetData(row, 0, "41d");
                        c1ValueCode.SetData(row, 1, _oUB.sValueCode12);
                        if (dt.Select("sValueCode='" + _oUB.sValueCode12 + "'").GetLength(0) > 0)
                        {
                            c1ValueCode.SetData(row, 2, dt.Select("sValueCode='" + _oUB.sValueCode12 + "'")[0][1].ToString());
                        }
                        c1ValueCode.SetData(row, 3, _oUB.nValueAmount12);
                    }
                    i++;
                    row = row + 1;
                }

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Grid List Control"

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

                C1FlexGrid _c1flexGrid = new C1FlexGrid();
                string currentFlexcontrol = ControlType.ToString();
                if (currentFlexcontrol == "CondtionCodes")
                {
                    _c1flexGrid = c1ConditionCode;
                    pnlContainedGloListcontrol = pnlInternalControl;

                }
                else if (currentFlexcontrol == "OccurrenceCode")
                {
                    _c1flexGrid = c1OccrenceCode;
                    pnlContainedGloListcontrol = pnlInternalOccurrenceCode;

                }
                else if (currentFlexcontrol == "OccurrenceSpanCode")
                {
                    _c1flexGrid = c1OccurenceSpanCodeRange;
                    pnlContainedGloListcontrol = pnlInternalSpanCodes;
                    pnlContainedGloListcontrol.Height = pnlInternalOccurrenceCode.Height;
                }
                else if (currentFlexcontrol == "ValueCodes")
                {
                    _c1flexGrid = c1ValueCode;
                    pnlContainedGloListcontrol = pnlInternalValueCode;
                }

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlContainedGloListcontrol.Width + 500, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlContainedGloListcontrol.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();


                int _x = _c1flexGrid.Cols[ColIndex].Left;
                int _y = _c1flexGrid.Rows[RowIndex].Bottom + 30; //+(RowIndex*10);
                int _width = pnlContainedGloListcontrol.Width;
                int _height = pnlContainedGloListcontrol.Height;



                int _parentleft = pnlContainedGloListcontrol.Parent.Bounds.Left;
                int _parentwidth = pnlContainedGloListcontrol.Parent.Bounds.Width;
                int _parentsReamainingHieght = pnlContainedGloListcontrol.Parent.Height - _y;
                int _parentRemainingWidth = pnlContainedGloListcontrol.Parent.Width - _x;


                if (_height - 10 > _parentsReamainingHieght)
                {
                    _y = _y - _height - 20;
                    pnlContainedGloListcontrol.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlContainedGloListcontrol.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlContainedGloListcontrol.SetBounds(_c1flexGrid.Cols[ColIndex].Left, _y + _c1flexGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                if (_width >= _parentRemainingWidth)
                {
                    pnlContainedGloListcontrol.Width = _parentRemainingWidth;
                    ogloGridListControl.Width = _parentRemainingWidth;
                }
                else
                {
                    pnlContainedGloListcontrol.Width = 400;
                    ogloGridListControl.Width = 400;
                }

                pnlContainedGloListcontrol.Visible = true;
                pnlContainedGloListcontrol.BringToFront();
                ogloGridListControl.Visible = true;
                ogloGridListControl.BringToFront();
                ogloGridListControl.Focus();

                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {

            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 4/2/2014

                for (int i = pnlContainedGloListcontrol.Controls.Count-1; i >= 0;  i--)
                {
                    pnlContainedGloListcontrol.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlContainedGloListcontrol.Visible = false;
                pnlInternalControl.Visible = false;
                pnlInternalOccurrenceCode.Visible = false;
                pnlInternalSpanCodes.Visible = false;
                pnlInternalValueCode.Visible = false;
                pnlContainedGloListcontrol.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            string strmsg = "";
            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            if (c1name == "c1ConditionCode")
            {
                _c1flexGrid = c1ConditionCode;
                strmsg = "Duplicate Condition Codes exist.\nContinue? ";
            }
            else if (c1name == "c1OccrenceCode")
            {
                _c1flexGrid = c1OccrenceCode;
                strmsg = "Duplicate Occurrence Codes exist.\nContinue? ";
            }
            else if (c1name == "c1OccurenceSpanCodeRange")
            {
                _c1flexGrid = c1OccurenceSpanCodeRange;
                strmsg = "Duplicate Occurrence Span Codes exist.\nContinue? ";
            }
            else if (c1name == "c1ValueCode")
            {
                _c1flexGrid = c1ValueCode;
                strmsg = "Duplicate Value Codes exist.\nContinue? ";
            }
            #endregion
            int COL_CODE = 1;
            int COL_Description = 2;
            try
            {

                int _rowIndex = 0;
                switch (_c1flexGrid.ColSel)
                {
                    case 1:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            //...Check if code exists
                            bool _isExistsCode = false;
                            if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 1)
                            {
                                for (int rIndex = 1; rIndex < _c1flexGrid.Rows.Count; rIndex++)
                                {
                                    if (rIndex != ogloGridListControl.ParentRowIndex)
                                    {
                                        if (_c1flexGrid.GetData(rIndex, COL_CODE) != null && Convert.ToString(_c1flexGrid.GetData(rIndex, COL_CODE)).Trim() != ""
                                            && Convert.ToString(_c1flexGrid.GetData(rIndex, COL_CODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                        {
                                            _isExistsCode = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (_isExistsCode == false)
                            {
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                _c1flexGrid.SetData(_rowIndex, COL_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                _c1flexGrid.SetData(_rowIndex, COL_Description, ogloGridListControl.SelectedItems[0].Description.Trim());
                                _c1flexGrid.Focus();


                            }
                            else
                            {
                                if (MessageBox.Show(strmsg, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    _c1flexGrid.SetData(_rowIndex, COL_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                    _c1flexGrid.SetData(_rowIndex, COL_Description, ogloGridListControl.SelectedItems[0].Description.Trim());
                                    _c1flexGrid.Focus();
                                }
                                else
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    _c1flexGrid.SetData(_rowIndex, COL_CODE, null);
                                    _c1flexGrid.SetData(_rowIndex, COL_Description, null);
                                    _c1flexGrid.Focus();
                                }
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            _c1flexGrid.Focus();
                            _c1flexGrid.Select(_rowIndex, COL_CODE, true);

                        }
                        break;
                    case 2:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            //...Check if code exists
                            bool _isExistsCode = false;
                            if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 1)
                            {
                                for (int rIndex = 1; rIndex < _c1flexGrid.Rows.Count; rIndex++)
                                {
                                    if (rIndex != ogloGridListControl.ParentRowIndex)
                                    {
                                        if (_c1flexGrid.GetData(rIndex, COL_CODE) != null && Convert.ToString(_c1flexGrid.GetData(rIndex, COL_CODE)).Trim() != ""
                                            && Convert.ToString(_c1flexGrid.GetData(rIndex, COL_CODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                        {
                                            _isExistsCode = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (_isExistsCode == false)
                            {
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                _c1flexGrid.SetData(_rowIndex, COL_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                _c1flexGrid.SetData(_rowIndex, COL_Description, ogloGridListControl.SelectedItems[0].Description.Trim());
                                _c1flexGrid.Focus();


                            }
                            else
                            {
                                if (MessageBox.Show(strmsg, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    _c1flexGrid.SetData(_rowIndex, COL_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                    _c1flexGrid.SetData(_rowIndex, COL_Description, ogloGridListControl.SelectedItems[0].Description.Trim());
                                    _c1flexGrid.Focus();
                                }
                                else
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    _c1flexGrid.SetData(_rowIndex, COL_CODE, null);
                                    _c1flexGrid.SetData(_rowIndex, COL_Description, null);
                                    _c1flexGrid.Focus();
                                }
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            _c1flexGrid.Focus();
                            _c1flexGrid.Select(_rowIndex, COL_Description, true);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                CloseInternalControl();
            }
        }

        #endregion

        #region " C1 Grid Events "

        private void ts_btnClose_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            this.Close();
        }

        private void _c1flexGrid_StartEdit(object sender, RowColEventArgs e)
        {

            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid GridName = (C1FlexGrid)sender;

            try
            {


                c1name = GridName.Name;
                if (c1name == "c1ConditionCode")
                    _c1flexGrid = c1ConditionCode;
                else if (c1name == "c1OccrenceCode")
                    _c1flexGrid = c1OccrenceCode;
                else if (c1name == "c1OccurenceSpanCodeRange")
                    _c1flexGrid = c1OccurenceSpanCodeRange;
                else if (c1name == "c1ValueCode")
                {

                    _c1flexGrid = c1ValueCode;
                    if (Convert.ToString(_c1flexGrid.GetData(e.Row, 3)) == "-")
                    {

                    }
                }
                //if (e.Row > 0)
                //{
                //    CellNote _cellNote = null;
                //    CellRange _cellRange = _c1flexGrid.GetCellRange(e.Row, e.Col);
                //    _cellRange.UserData = _cellNote;
                //}

                switch (e.Col)
                {

                    case 1:
                        {
                            //   ogloGridListControl = null;
                            if (c1name == "c1ConditionCode")
                                OpenInternalControl(gloGridListControlType.CondtionCodes, "Condition Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1OccrenceCode")
                                OpenInternalControl(gloGridListControlType.OccurrenceCode, "Occurrence Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1OccurenceSpanCodeRange")
                                OpenInternalControl(gloGridListControlType.OccurrenceSpanCode, "Occurrence Span Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1ValueCode")
                                OpenInternalControl(gloGridListControlType.ValueCodes, "Value Codes", false, e.Row, e.Col, "");

                            string _SearchText = "";
                            if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(_c1flexGrid.GetData(e.Row, 1));
                                //ogloGridListControl.FillControl(_SearchText);
                                ogloGridListControl.AdvanceSearch(_SearchText);

                            }

                        }
                        break;
                    case 2:
                        {
                            //   ogloGridListControl = null;
                            if (c1name == "c1ConditionCode")
                                OpenInternalControl(gloGridListControlType.CondtionCodes, "Condition Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1OccrenceCode")
                                OpenInternalControl(gloGridListControlType.OccurrenceCode, "Occurrence Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1OccurenceSpanCodeRange")
                                OpenInternalControl(gloGridListControlType.OccurrenceSpanCode, "Occurrence Span Codes", false, e.Row, e.Col, "");
                            else if (c1name == "c1ValueCode")
                                OpenInternalControl(gloGridListControlType.ValueCodes, "Value Codes", false, e.Row, e.Col, "");

                            string _SearchText = "";
                            if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(_c1flexGrid.GetData(e.Row, 1));
                                //ogloGridListControl.FillControl(_SearchText);
                                ogloGridListControl.AdvanceSearch(_SearchText);

                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void ts_btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                txttypeofbilling.Select();
                _oUB.IsModify = false;
                string strDate = "";
                if (mskAdmitDate.Text.Length > 0)
                {
                    mskAdmitDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    strDate = mskAdmitDate.Text;
                    mskAdmitDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                }
                if (txtAdmtHour.Text.Trim() != "" || strDate.Trim() != "" || txtdischargeHour.Text.Trim() != "")
                {
                    _oUB.HasOtherData = true;
                }
                else
                {
                    _oUB.HasOtherData = false;
                }
                c1ConditionCode.FinishEditing();
                c1OccrenceCode.FinishEditing();
                c1OccurenceSpanCodeRange.FinishEditing();
                c1ValueCode.FinishEditing();
                if (txttypeofbilling.Text != "")
                {
                    if (txttypeofbilling.Text.Length == 4)
                    {
                        if (txttypeofbilling.Text.Substring(0, 1) != "0")
                        {
                            MessageBox.Show("Invalid Type of Bill.\nLength must be 3 or 4 digits.\nType of Bill must begin with zero if 4 digits are entered.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txttypeofbilling.Focus();
                            return;
                        }
                    }
                    else if (txttypeofbilling.Text.Length < 3)
                    {
                        MessageBox.Show("Invalid Type of Bill Length must be 3 or 4 digits. Type of Bill must begin with zero if 4 digits are entered.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txttypeofbilling.Focus();
                        return;
                    }
                }

                for (int i = 1; c1OccrenceCode.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1OccrenceCode.Cols[1][i]) != "")
                    {
                        if (Convert.ToString(c1OccrenceCode.Cols[3][i]) == "")
                        {

                            if (MessageBox.Show("Occurrence Date is blank.\nContinue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return;
                            else
                                break;

                        }
                    }
                }

                for (int i = 1; c1OccurenceSpanCodeRange.Rows.Count > i; i++)
                {
                    
                    if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][i]) != "")
                    {
                        if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][i]) == "")
                        {
                            if (MessageBox.Show("Occurrence Span From Date is blank.\nContinue?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return;
                            else
                                break;
                        }
                    }
                    if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][i]) != "")
                    {
                        if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][i]) == "")
                        {
                            if (MessageBox.Show("Occurrence Span Through Date is blank.\nContinue?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return;
                            else
                                break;
                        }
                    }
                }

                for (int i = 1; c1OccurenceSpanCodeRange.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][i]) != "")
                    {
                        if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][i]) != "")
                        {
                            if (Convert.ToDateTime(c1OccurenceSpanCodeRange.Cols[3][i]) > Convert.ToDateTime(c1OccurenceSpanCodeRange.Cols[4][i]))
                            {
                                MessageBox.Show("From Date cannot be greater than To Date for Occurrence Span Code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
                //if (_oUB.IsModify == false)
                //{
                for (int i = 1; c1ConditionCode.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1ConditionCode.Cols[1][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                }
                //}

                //if (_oUB.IsModify == false)
                //{
                for (int i = 1; c1OccrenceCode.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1OccrenceCode.Cols[1][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;

                        break;
                    }
                    else if (Convert.ToString(c1OccrenceCode.Cols[3][i]) != "")
                    {
                        _oUB.IsModify = true;
                        if (i != 1)
                        {
                            _oUB.HasOtherData = true;
                            //break;
                        }
                    }
                }
                //}

                //if (_oUB.IsModify == false)
                //{
                for (int i = 1; c1OccurenceSpanCodeRange.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                    else if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                    else if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                }
                //}

                //if (_oUB.IsModify == false)
                //{
                for (int i = 1; c1ValueCode.Rows.Count > i; i++)
                {
                    if (Convert.ToString(c1ValueCode.Cols[1][i]) != "")
                    {
                        if (Convert.ToString(c1ValueCode.Cols[3][i]) == "")
                        {
                            if (MessageBox.Show("Value Amount is blank.\nContinue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                  
                }


                for (int i = 1; c1ValueCode.Rows.Count > i; i++)
                { 
                    if (Convert.ToString(c1ValueCode.Cols[1][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                    else if (Convert.ToString(c1ValueCode.Cols[3][i]) != "")
                    {
                        _oUB.IsModify = true;
                        _oUB.HasOtherData = true;
                        break;
                    }
                }
                //}



                _oUB.nTransactionMasterID = 0;
                _oUB.nTransactionID = 0;
                _oUB.nClaimNo = 0;
                _oUB.sTypeofbill = txttypeofbilling.Text;
               
                _oUB.sAdmissionType = txtAdmissionType.Text;
                _oUB.sDischargeStatus = txtDischargeStatus.Text;
                if (IsValidDate(mskAdmitDate.Text))
                    _oUB.sAdmitDate = mskAdmitDate.Text;
                else

                    _oUB.sAdmitDate = ""; 
              
               
                _oUB.sAdmitHour = txtAdmtHour.Text;
                _oUB.sDischargeHour = txtdischargeHour.Text;
                if (txttypeofbilling.Text.Trim() != "")
                {
                    _oUB.IsModify = true;
                }

                _oUB.sConditionCode01 = Convert.ToString(c1ConditionCode.Cols[1][1]);
                _oUB.sConditionCode02 = Convert.ToString(c1ConditionCode.Cols[1][2]);
                _oUB.sConditionCode03 = Convert.ToString(c1ConditionCode.Cols[1][3]);
                _oUB.sConditionCode04 = Convert.ToString(c1ConditionCode.Cols[1][4]);
                _oUB.sConditionCode05 = Convert.ToString(c1ConditionCode.Cols[1][5]);
                _oUB.sConditionCode06 = Convert.ToString(c1ConditionCode.Cols[1][6]);
                _oUB.sConditionCode07 = Convert.ToString(c1ConditionCode.Cols[1][7]);
                _oUB.sConditionCode08 = Convert.ToString(c1ConditionCode.Cols[1][8]);
                _oUB.sConditionCode09 = Convert.ToString(c1ConditionCode.Cols[1][9]);
                _oUB.sConditionCode10 = Convert.ToString(c1ConditionCode.Cols[1][10]);
                _oUB.sConditionCode11 = Convert.ToString(c1ConditionCode.Cols[1][11]);
                //_oUB.sConditionCode12 = c1ConditionCode.Cols[1][12].ToString();

                _oUB.sOccurrenceCode01 = Convert.ToString(c1OccrenceCode.Cols[1][1]);
                _oUB.sOccurrenceDate01 = Convert.ToString(c1OccrenceCode.Cols[3][1]);

                _oUB.sOccurrenceCode02 = Convert.ToString(c1OccrenceCode.Cols[1][2]);
                _oUB.sOccurrenceDate02 = Convert.ToString(c1OccrenceCode.Cols[3][2]);

                _oUB.sOccurrenceCode03 = Convert.ToString(c1OccrenceCode.Cols[1][3]);
                _oUB.sOccurrenceDate03 = Convert.ToString(c1OccrenceCode.Cols[3][3]);

                _oUB.sOccurrenceCode04 = Convert.ToString(c1OccrenceCode.Cols[1][4]);
                _oUB.sOccurrenceDate04 = Convert.ToString(c1OccrenceCode.Cols[3][4]);

                _oUB.sOccurrenceCode05 = Convert.ToString(c1OccrenceCode.Cols[1][5]);
                _oUB.sOccurrenceDate05 = Convert.ToString(c1OccrenceCode.Cols[3][5]);

                _oUB.sOccurrenceCode06 = Convert.ToString(c1OccrenceCode.Cols[1][6]);
                _oUB.sOccurrenceDate06 = Convert.ToString(c1OccrenceCode.Cols[3][6]);

                _oUB.sOccurrenceCode07 = Convert.ToString(c1OccrenceCode.Cols[1][7]);
                _oUB.sOccurrenceDate07 = Convert.ToString(c1OccrenceCode.Cols[3][7]);

                _oUB.sOccurrenceCode08 = Convert.ToString(c1OccrenceCode.Cols[1][8]);
                _oUB.sOccurrenceDate08 = Convert.ToString(c1OccrenceCode.Cols[3][8]);


                _oUB.sOccurrenceSpanCode01 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][1]);
                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][1]) != "")
                _oUB.sOccurrenceFromSpanDate01 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][1]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][1]) != "")
                _oUB.sOccurrenceTOSpanDate01 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][1]);

                _oUB.sOccurrenceSpanCode02 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][2]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][2]) != "")
                _oUB.sOccurrenceFromSpanDate02 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][2]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][2]) != "")
                _oUB.sOccurrenceToSpanDate02 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][2]);

                _oUB.sOccurrenceSpanCode03 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][3]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][3]) != "")
                _oUB.sOccurrenceFromSpanDate03 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][3]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][3]) != "")
                _oUB.sOccurrenceToSpanDate03 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][3]);

                _oUB.sOccurrenceSpanCode04 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][4]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][4]) != "")
                _oUB.sOccurrenceFromSpanDate04 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][4]);

                //if (Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][4]) != "")
                _oUB.sOccurrenceToSpanDate04 = Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][4]);


                _oUB.sValueCode01 = Convert.ToString(c1ValueCode.Cols[1][1]);
                if (c1ValueCode.Cols[3][1] != null && Convert.ToString(c1ValueCode.Cols[3][1]) != "")
                {
                    _oUB.nValueAmount01 = Convert.ToDecimal(c1ValueCode.Cols[3][1]);
                }
                else
                {
                    _oUB.nValueAmount01 = null;
                }
                _oUB.sValueCode02 = Convert.ToString(c1ValueCode.Cols[1][2]);
                if (c1ValueCode.Cols[3][2] != null && Convert.ToString(c1ValueCode.Cols[3][2]) != "")
                {
                    _oUB.nValueAmount02 = Convert.ToDecimal(c1ValueCode.Cols[3][2]);
                }
                else
                {
                    _oUB.nValueAmount02 = null;
                }
                _oUB.sValueCode03 = Convert.ToString(c1ValueCode.Cols[1][3]);
                if (c1ValueCode.Cols[3][3] != null && Convert.ToString(c1ValueCode.Cols[3][3]) != "")
                {
                    _oUB.nValueAmount03 = Convert.ToDecimal(c1ValueCode.Cols[3][3]);
                }
                else
                {
                    _oUB.nValueAmount03 = null;
                }
                _oUB.sValueCode04 = Convert.ToString(c1ValueCode.Cols[1][4]);
                if (c1ValueCode.Cols[3][4] != null && Convert.ToString(c1ValueCode.Cols[3][4]) != "")
                {
                    _oUB.nValueAmount04 = Convert.ToDecimal(c1ValueCode.Cols[3][4]);
                }
                else
                {
                    _oUB.nValueAmount04 = null;
                }
                _oUB.sValueCode05 = Convert.ToString(c1ValueCode.Cols[1][5]);
                if (c1ValueCode.Cols[3][5] != null && Convert.ToString(c1ValueCode.Cols[3][5]) != "")
                {
                    _oUB.nValueAmount05 = Convert.ToDecimal(c1ValueCode.Cols[3][5]);
                }
                else
                {
                    _oUB.nValueAmount05 = null;
                }
                _oUB.sValueCode06 = Convert.ToString(c1ValueCode.Cols[1][6]);
                if (c1ValueCode.Cols[3][6] != null && Convert.ToString(c1ValueCode.Cols[3][6]) != "")
                {
                    _oUB.nValueAmount06 = Convert.ToDecimal(c1ValueCode.Cols[3][6]);
                }
                else
                {
                    _oUB.nValueAmount06 = null;
                }
                _oUB.sValueCode07 = Convert.ToString(c1ValueCode.Cols[1][7]);
                if (c1ValueCode.Cols[3][7] != null && Convert.ToString(c1ValueCode.Cols[3][7]) != "")
                {
                    _oUB.nValueAmount07 = Convert.ToDecimal(c1ValueCode.Cols[3][7]);
                }
                else
                {
                    _oUB.nValueAmount07 = null;
                }
                _oUB.sValueCode08 = Convert.ToString(c1ValueCode.Cols[1][8]);
                if (c1ValueCode.Cols[3][8] != null && Convert.ToString(c1ValueCode.Cols[3][8]) != "")
                {
                    _oUB.nValueAmount08 = Convert.ToDecimal(c1ValueCode.Cols[3][8]);
                }
                else
                {
                    _oUB.nValueAmount08 = null;
                }

                _oUB.sValueCode09 = Convert.ToString(c1ValueCode.Cols[1][9]);
                if (c1ValueCode.Cols[3][9] != null && Convert.ToString(c1ValueCode.Cols[3][9]) != "")
                {
                    _oUB.nValueAmount09 = Convert.ToDecimal(c1ValueCode.Cols[3][9]);
                }
                else
                {
                    _oUB.nValueAmount09 = null;
                }

                _oUB.sValueCode10 = Convert.ToString(c1ValueCode.Cols[1][10]);
                if (c1ValueCode.Cols[3][10] != null && Convert.ToString(c1ValueCode.Cols[3][10]) != "")
                {
                    _oUB.nValueAmount10 = Convert.ToDecimal(c1ValueCode.Cols[3][10]);
                }
                else
                {
                    _oUB.nValueAmount10 = null;
                }

                _oUB.sValueCode11 = Convert.ToString(c1ValueCode.Cols[1][11]);
                if (c1ValueCode.Cols[3][11] != null && Convert.ToString(c1ValueCode.Cols[3][11]) != "")
                {
                    _oUB.nValueAmount11 = Convert.ToDecimal(c1ValueCode.Cols[3][11]);
                }
                else
                {
                    _oUB.nValueAmount11 = null;
                }

                _oUB.sValueCode12 = Convert.ToString(c1ValueCode.Cols[1][12]);
                if (c1ValueCode.Cols[3][12] != null && Convert.ToString(c1ValueCode.Cols[3][12]) != "")
                {
                    _oUB.nValueAmount12 = Convert.ToDecimal(c1ValueCode.Cols[3][12]);
                }
                else
                {
                    _oUB.nValueAmount12 = null;
                }
                //_oUB.IsModify = true;
                if (_IsValidDate)
                  this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void _c1flexGrid_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                C1FlexGrid _c1flexGrid = new C1FlexGrid();
                C1FlexGrid aaa = (C1FlexGrid)sender;
                string c1name = aaa.Name;
                if (c1name == "c1ConditionCode")
                    _c1flexGrid = c1ConditionCode;
                else if (c1name == "c1OccrenceCode")
                    _c1flexGrid = c1OccrenceCode;
                else if (c1name == "c1OccurenceSpanCodeRange")
                    _c1flexGrid = c1OccurenceSpanCodeRange;
                else if (c1name == "c1ValueCode")
                    _c1flexGrid = c1ValueCode;
                if (e.KeyCode == Keys.Tab)
                {

                        if (_c1flexGrid == c1ConditionCode)
                    {
                        if (_c1flexGrid.RowSel > 0 && Convert.ToString(_c1flexGrid.Cols[_c1flexGrid.ColSel][_c1flexGrid.RowSel]) == "" && _c1flexGrid.ColSel == 2)
                        {
                            int i = 0;
                            for (i = _c1flexGrid.RowSel; _c1flexGrid.Rows.Count > i; i++)
                            {
                                if (Convert.ToString(_c1flexGrid.Cols[1][i]) == "" && Convert.ToString(_c1flexGrid.Cols[2][i]) == "")
                                {
                                    e.SuppressKeyPress = true;
                                }
                                else
                                {
                                    e.SuppressKeyPress = false;
                                    break;
                                }
                            }
                            if (i == _c1flexGrid.Rows.Count || _c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count)
                            {
                                e.SuppressKeyPress = false;
                                c1ConditionCode.Select(- 1, 1);
                                 c1ValueCode.Focus();
                                 c1ValueCode.Select(1, 1);
                             
                            }
                        }
                        else if (_c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count && _c1flexGrid.ColSel == 2)
                        {
                            e.SuppressKeyPress = false;
                            c1ConditionCode.Select(-1, 1);
                            c1ValueCode.Focus();
                            c1ValueCode.Select(1, 1);

                        }
                                    

                     }
                    else if (_c1flexGrid == c1OccrenceCode)
                    {
                         if (_c1flexGrid.RowSel > 0&&Convert.ToString(_c1flexGrid.Cols[_c1flexGrid.ColSel][_c1flexGrid.RowSel]) == "" && _c1flexGrid.ColSel == 3)
                        {
                            int i = 0;
                            for (i = _c1flexGrid.RowSel; _c1flexGrid.Rows.Count > i; i++)
                            {
                                if (Convert.ToString(_c1flexGrid.Cols[1][i]) == "" && Convert.ToString(_c1flexGrid.Cols[3][i]) == "")
                                {
                                    e.SuppressKeyPress = true;
                                }
                                else
                                {
                                    e.SuppressKeyPress = false;
                                    break;
                                }
                            }
                            if (i == _c1flexGrid.Rows.Count || _c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count)
                            {
                                e.SuppressKeyPress = false;
                                c1OccrenceCode.Select(- 1, 1);
                                c1OccurenceSpanCodeRange.Focus();
                                c1OccurenceSpanCodeRange.Select(1, 1);
                               
                            }
                        }
                         else if (_c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count && _c1flexGrid.ColSel == 3)
                         {
                             e.SuppressKeyPress = false;
                             c1OccrenceCode.Select(-1, 1);
                             c1OccurenceSpanCodeRange.Focus();
                             c1OccurenceSpanCodeRange.Select(1, 1);

                         }
                    }
                    else    if (_c1flexGrid == c1OccurenceSpanCodeRange)
                    {
                         if (_c1flexGrid.RowSel > 0&&Convert.ToString(_c1flexGrid.Cols[_c1flexGrid.ColSel][_c1flexGrid.RowSel]) == "" && _c1flexGrid.ColSel == 4)
                        {
                            int i = 0;
                            for (i = _c1flexGrid.RowSel; _c1flexGrid.Rows.Count > i; i++)
                            {
                                if (Convert.ToString(_c1flexGrid.Cols[1][i]) == "" && Convert.ToString(_c1flexGrid.Cols[4][i]) == "")
                                {
                                    e.SuppressKeyPress = true;
                                }
                                else
                                {
                                    e.SuppressKeyPress = false;
                                    break;
                                }
                            }
                            if (i == _c1flexGrid.Rows.Count || _c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count)
                            {
                                e.SuppressKeyPress = false;
                                c1OccurenceSpanCodeRange.Select(- 1, 1);
                                c1ConditionCode.Focus();
                                c1ConditionCode.Select(1, 1);
                               
                            }
                        }
                         else if (_c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count && _c1flexGrid.ColSel == 4)
                         {
                             e.SuppressKeyPress = false;
                             c1OccurenceSpanCodeRange.Select(-1, 1);
                             c1ConditionCode.Focus();
                             c1ConditionCode.Select(1, 1);

                         }
                    }
                    else if (_c1flexGrid == c1ValueCode)
                    {
                        if (_c1flexGrid.RowSel > 0&&Convert.ToString(_c1flexGrid.Cols[_c1flexGrid.ColSel][_c1flexGrid.RowSel]) == "" && _c1flexGrid.ColSel == 3)
                        {
                            int i = 0;
                            for (i = _c1flexGrid.RowSel; _c1flexGrid.Rows.Count > i; i++)
                            {
                                if (Convert.ToString(_c1flexGrid.Cols[1][i]) == "" && Convert.ToString(_c1flexGrid.Cols[3][i]) == "")
                                {
                                    e.SuppressKeyPress = true;
                                }
                                else
                                {
                                    e.SuppressKeyPress = false;
                                    break;
                                }
                            }
                            if (i == _c1flexGrid.Rows.Count || _c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count)
                            {
                                e.SuppressKeyPress = false;
                                c1ValueCode.Select(- 1, 1);
                                c1OccrenceCode.Focus();
                                c1OccrenceCode.Select(1, 1);

                            }
                        }
                        else if (_c1flexGrid.RowSel + 1 == _c1flexGrid.Rows.Count && _c1flexGrid.ColSel == 3)
                        {
                            e.SuppressKeyPress = false;
                            c1ValueCode.Select(-1, 1);
                            c1OccrenceCode.Focus();
                            c1OccrenceCode.Select(1, 1);

                        }
                    }
                   // string aa = Convert.ToString(_c1flexGrid.Cols[_c1flexGrid.ColSel][_c1flexGrid.RowSel]);
                    
                    
                    //if (Convert.ToString(_c1flexGrid.Cols[1][_c1flexGrid.RowSel]) == "")
                    //{
                    //    int i = 0;
                    //    e.SuppressKeyPress = true;
                    //    for ( i = _c1flexGrid.RowSel; _c1flexGrid.Rows.Count > i; i++)
                    //    {
                    //        if (Convert.ToString(_c1flexGrid.Cols[1][i]) != "")
                    //          {
                    //            e.SuppressKeyPress = false;
                    //            _c1flexGrid.Focus();
                    //            _c1flexGrid.Select(i, 0);
                    //            break;
                    //          }
                    //    }   
                    //    if(i==_c1flexGrid.Rows.Count)
                    //    {
                    //        e.SuppressKeyPress = false;
                    //        if (_c1flexGrid == c1ConditionCode)
                    //        {
                    //            c1OccrenceCode.Focus();
                    //            c1OccrenceCode.Select(1, 1);
                    //        }
                    //        if (_c1flexGrid == c1OccrenceCode)
                    //        {
                    //            c1OccurenceSpanCodeRange.Focus();
                    //            c1OccurenceSpanCodeRange.Select(1, 1);
                    //        }
                    //        if (_c1flexGrid == c1OccurenceSpanCodeRange)
                    //        {
                    //            c1ValueCode.Focus();
                    //            c1ValueCode.Select(1, 1);
                    //        }
                    //        if (_c1flexGrid == c1ValueCode)
                    //        {
                    //            c1ConditionCode.Focus();
                    //            c1ConditionCode.Select(1, 1);
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    e.SuppressKeyPress = false;
                    //}
                    if (_c1flexGrid.Rows.Count == _c1flexGrid.RowSel + 1 && _c1flexGrid.ColSel == 5)
                    {
                        //e.SuppressKeyPress = true;
                        //TopToolStrip.Focus();
                        //  ts_btnAddLine.Select();
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel, null);
                    if (c1name == "c1OccrenceCode")
                    {
                        if (_c1flexGrid.RowSel > 0 && _c1flexGrid.ColSel == 3)
                        {
                            if (c1OccrenceCode.Row == 1)
                            {
                                if (Convert.ToString(_c1flexGrid.GetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel)) == "")
                                {
                                    _oUB.MinDOSDeleted = true;
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void _c1flexGrid_KeyUp_1(object sender, KeyEventArgs e)
        {

            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid aaa = (C1FlexGrid)sender;
            string c1name = aaa.Name;

            try
            {
                if (c1name == "c1ConditionCode")
                    _c1flexGrid = c1ConditionCode;
                else if (c1name == "c1OccrenceCode")
                    _c1flexGrid = c1OccrenceCode;
                else if (c1name == "c1OccurenceSpanCodeRange")
                    _c1flexGrid = c1OccurenceSpanCodeRange;
                else if (c1name == "c1ValueCode")
                    _c1flexGrid = c1ValueCode;
                string _code = "";
                string _description = "";
                bool _isdeleted = true;
                int COL_CODE = 1;
                int COL_Description = 2;
           //     int COL_FromDate = 3;


                TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
                RowColEventArgs e1 = null;



                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlContainedGloListcontrol.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {
                            }
                        }
                    }


                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlContainedGloListcontrol.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab)
                {
                    e.SuppressKeyPress = true;
                    #region "Escape Key"
                    if (pnlContainedGloListcontrol.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (_c1flexGrid.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;

                    if (_c1flexGrid.GetData(_c1flexGrid.RowSel, COL_CODE) != null)
                    {
                        _code = _c1flexGrid.GetData(_c1flexGrid.RowSel, COL_CODE).ToString();
                    }
                    if (_c1flexGrid.GetData(_c1flexGrid.RowSel, COL_Description) != null)
                    {
                        _description = _c1flexGrid.GetData(_c1flexGrid.RowSel, COL_Description).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;
                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (_c1flexGrid.ColSel)
                    {

                        case 1:
                            {

                                _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel, "");
                                _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel + 1, "");

                                //CellRange rg = _c1flexGrid.GetCellRange(_c1flexGrid.RowSel, _c1flexGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;
                        case 2:
                            {

                                //_c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel, "");
                                //_c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel + 1, "");

                                _c1flexGrid.SetData(_c1flexGrid.RowSel, COL_CODE, "");
                                _c1flexGrid.SetData(_c1flexGrid.RowSel, COL_Description, "");

                                //CellRange rg = _c1flexGrid.GetCellRange(_c1flexGrid.RowSel, _c1flexGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;

                        case 3:
                            {

                                _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel, null);
                                // _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel + 1, "");

                                //CellRange rg = _c1flexGrid.GetCellRange(_c1flexGrid.RowSel, _c1flexGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;
                        case 4:
                            {

                                _c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel, null);
                                //_c1flexGrid.SetData(_c1flexGrid.RowSel, _c1flexGrid.ColSel + 1, "");

                                //CellRange rg = _c1flexGrid.GetCellRange(_c1flexGrid.RowSel, _c1flexGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;

                    }
                    _code = "";
                    e1 = new RowColEventArgs(_c1flexGrid.RowSel, _c1flexGrid.ColSel);
                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = true;


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;


                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }

        }

        private void _c1flexGrid_ChangeEdit(object sender, EventArgs e)
        {

            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid aaa = (C1FlexGrid)sender;
            string c1name = aaa.Name;
            if (c1name == "c1ConditionCode")
                _c1flexGrid = c1ConditionCode;
            else if (c1name == "c1OccrenceCode")
                _c1flexGrid = c1OccrenceCode;
            else if (c1name == "c1OccurenceSpanCodeRange")
                _c1flexGrid = c1OccurenceSpanCodeRange;
            else if (c1name == "c1ValueCode")
                _c1flexGrid = c1ValueCode;

            string _strSearchString = "";
            int COL_CODE = 1;
            int COL_Description = 2;

            try
            {
                _strSearchString = _c1flexGrid.Editor.Text;

                if (ogloGridListControl != null)
                {

                    string _COL_CODE = "";
                    string _COL_DESC = "";

                    if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 0)
                    {
                        _COL_CODE = Convert.ToString(_c1flexGrid.GetData(_c1flexGrid.Row, COL_CODE));
                        _COL_DESC = Convert.ToString(_c1flexGrid.GetData(_c1flexGrid.Row, COL_Description));

                    }


                    //ogloGridListControl.FillControl(_strSearchString);
                    //if (_strSearchString != "" && ogloGridListControl != null)
                    //{
                    ogloGridListControl.AdvanceSearch(_strSearchString);
                    //}
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex.ToString();
            }
            finally
            {
            }
        }

        private void _c1flexGrid_AfterRowColChange(object sender, RangeEventArgs e)
        {
            try
            {
                {
                    //CloseInternalControl(); 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {

            }
        }


        private void _c1flexGrid_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
                    {
                        //e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void ts_btnClose_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }


        //private void _c1flexGrid_AfterEdit(object sender, RowColEventArgs e)
        //{
        //    //try
        //    //{
        //    //    C1FlexGrid _c1flexGrid = new C1FlexGrid();
        //    //    C1FlexGrid aaa = (C1FlexGrid)sender;
        //    //    string c1name = aaa.Name;
        //    //    if (c1name == "c1ConditionCode")
        //    //        _c1flexGrid = c1ConditionCode;
        //    //    else if (c1name == "c1OccrenceCode")
        //    //        _c1flexGrid = c1OccrenceCode;
        //    //    else if (c1name == "c1OccurenceSpanCodeRange")
        //    //        _c1flexGrid = c1OccurenceSpanCodeRange;
        //    //    else if (c1name == "c1ValueCode")
        //    //        _c1flexGrid = c1ValueCode;
        //    //    if (e.Col == 1)  //Check for CPT CODE if blank then change CPT DEsc to blank
        //    //    {
        //    //        if (_c1flexGrid.GetData(_c1flexGrid.RowSel, 1) != null)
        //    //        {
        //    //            if (Convert.ToString(_c1flexGrid.GetData(_c1flexGrid.RowSel, 2)) == "")
        //    //            {
        //    //                //  _c1flexGrid.SetData(_c1flexGrid.RowSel, 3, "");
        //    //            }
        //    //        }
        //    //    }
        //    //    else if (e.Col == 2)  //Check for CPT CODE if blank then change CPT DEsc to blank
        //    //    {
        //    //        //if (_c1flexGrid.GetData(_c1flexGrid.RowSel, 2) != null)
        //    //        //{
        //    //        //    if (Convert.ToString(_c1flexGrid.GetData(_c1flexGrid.RowSel, 4)) == "")
        //    //        //    {
        //    //        //        _c1flexGrid.SetData(_c1flexGrid.RowSel, 5, "");
        //    //        //    }
        //    //        //}
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    //    ex = null;
        //    //}
        //}

        private void _c1flexGrid_LeaveEdit(object sender, RowColEventArgs e)
        {
            //C1FlexGrid _c1flexGrid = new C1FlexGrid();
            //C1FlexGrid aaa = (C1FlexGrid)sender;
            //string c1name = aaa.Name;
            //if (c1name == "c1ConditionCode")
            //    _c1flexGrid = c1ConditionCode;
            //else if (c1name == "c1OccrenceCode")
            //    _c1flexGrid = c1OccrenceCode;
            //else if (c1name == "c1OccurenceSpanCodeRange")
            //    _c1flexGrid = c1OccurenceSpanCodeRange;
            //else if (c1name == "c1ValueCode")
            //    _c1flexGrid = c1ValueCode;
            //try
            //{
            //    if (_c1flexGrid != null)
            //    {
            //        _c1flexGrid.ChangeEdit -= new System.EventHandler(this._c1flexGrid_ChangeEdit);
            //        _c1flexGrid.Editor.Text = "";
            //        _c1flexGrid.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        private void c1ValueCode_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if (c1ValueCode.ColSel == 3 && c1ValueCode.RowSel != 5 && c1ValueCode.RowSel != 6)
            {
                decimal _result = Convert.ToDecimal(c1ValueCode.GetData(c1ValueCode.RowSel, c1ValueCode.ColSel));
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }
            }
        }

        private void _c1flexGrid_MouseDown(object sender, MouseEventArgs e)
        {
            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid aaa = (C1FlexGrid)sender;
            string c1name = aaa.Name;
            Int32 tempRow = 0;
            
            c1ConditionCode.ContextMenuStrip = null;
            c1OccrenceCode.ContextMenuStrip = null;
            c1ValueCode.ContextMenuStrip = null;
            c1OccurenceSpanCodeRange.ContextMenuStrip = null;
            
            if (c1name == "c1ConditionCode")
            {
                _c1flexGrid = c1ConditionCode;
                _c1flexGridforcmuDelete = c1ConditionCode;
                //HitTestInfo hitInfo = _c1flexGrid.HitTest(e.X, e.Y);
                
                //c1ConditionCode.RowSel = hitInfo.Row;

                //if (hitInfo.Row > 0)
                //{

                //c1ConditionCode.Focus();
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                }
                if (pnlInternalOccurrenceCode.Visible == true)
                {
                    pnlInternalOccurrenceCode.Visible = false;
                    pnlInternalOccurrenceCode.SendToBack();
                }
                if (pnlInternalSpanCodes.Visible == true)
                {
                    pnlInternalSpanCodes.Visible = false;
                    pnlInternalSpanCodes.SendToBack();
                }
                if (pnlInternalValueCode.Visible == true)
                {
                    pnlInternalValueCode.Visible = false;
                    pnlInternalValueCode.SendToBack();
                }
                //c1ConditionCode.FinishEditing();
                //c1ConditionCode.Refresh();
                //}
                tempRow = _c1flexGrid.HitTest(e.X, e.Y).Row;
                if (tempRow>0&&(Convert.ToString(c1ConditionCode.Cols[1][tempRow]) != "" || Convert.ToString(c1ConditionCode.Cols[2][tempRow]) != ""))
                {
                    c1ConditionCode.ContextMenuStrip = cmnu_DeleteUB04Data;
                    _c1flexGrid.Select(tempRow, 1);
                }
               //
            }
            else if (c1name == "c1OccrenceCode")
            {
                _c1flexGrid = c1OccrenceCode;
                _c1flexGridforcmuDelete = c1OccrenceCode;
                //HitTestInfo hitInfo = _c1flexGrid.HitTest(e.X, e.Y);
                //_c1flexGrid.RowSel = hitInfo.Row;

                //if (hitInfo.Row > 0)
                //{
                //_c1flexGrid.Focus();
                if (pnlInternalOccurrenceCode.Visible == true)
                {
                    pnlInternalOccurrenceCode.Visible = false;
                    pnlInternalOccurrenceCode.SendToBack();
                }
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                }
                if (pnlInternalSpanCodes.Visible == true)
                {
                    pnlInternalSpanCodes.Visible = false;
                    pnlInternalSpanCodes.SendToBack();
                }
                if (pnlInternalValueCode.Visible == true)
                {
                    pnlInternalValueCode.Visible = false;
                    pnlInternalValueCode.SendToBack();
                }
                //}
                tempRow = _c1flexGrid.HitTest(e.X, e.Y).Row;
                int colHit = _c1flexGrid.HitTest(e.X, e.Y).Column;

                if (tempRow > 0 && (Convert.ToString(c1OccrenceCode.Cols[1][tempRow]) != "" || Convert.ToString(c1OccrenceCode.Cols[2][tempRow]) != "" || Convert.ToString(c1OccrenceCode.Cols[3][tempRow]) != ""))
                {
                   
                    if (colHit != 3)
                    {
                        c1OccrenceCode.ContextMenuStrip = cmnu_DeleteUB04Data;
                        _c1flexGrid.Select(tempRow, 1);
                    }
                }
              // 
               
            }
            else if (c1name == "c1OccurenceSpanCodeRange")
            {
                _c1flexGrid = c1OccurenceSpanCodeRange;
                _c1flexGridforcmuDelete = c1OccurenceSpanCodeRange;
                //HitTestInfo hitInfo = _c1flexGrid.HitTest(e.X, e.Y);
                //_c1flexGrid.RowSel = hitInfo.Row;

                // if (hitInfo.Row > 0)
                //{
                //_c1flexGrid.Focus();
                if (pnlInternalSpanCodes.Visible == true)
                {
                    pnlInternalSpanCodes.Visible = false;
                    pnlInternalSpanCodes.SendToBack();
                }
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                }
                if (pnlInternalOccurrenceCode.Visible == true)
                {
                    pnlInternalOccurrenceCode.Visible = false;
                    pnlInternalOccurrenceCode.SendToBack();
                }
                if (pnlInternalValueCode.Visible == true)
                {
                    pnlInternalValueCode.Visible = false;
                    pnlInternalValueCode.SendToBack();
                }
                //}
                tempRow = _c1flexGrid.HitTest(e.X, e.Y).Row;
                int colHit = _c1flexGrid.HitTest(e.X, e.Y).Column;
                if (tempRow > 0 && (Convert.ToString(c1OccurenceSpanCodeRange.Cols[1][tempRow]) != "" || Convert.ToString(c1OccurenceSpanCodeRange.Cols[2][tempRow]) != "" || Convert.ToString(c1OccurenceSpanCodeRange.Cols[3][tempRow]) != "" || Convert.ToString(c1OccurenceSpanCodeRange.Cols[4][tempRow]) != ""))
                {

                    if (colHit != 3 && colHit != 4)
                    {
                        c1OccurenceSpanCodeRange.ContextMenuStrip = cmnu_DeleteUB04Data;
                        _c1flexGrid.Select(tempRow, 1);
                    }
                }
               // 
                
            }
            else if (c1name == "c1ValueCode")
            {
                _c1flexGrid = c1ValueCode;
                _c1flexGridforcmuDelete = c1ValueCode;


                //HitTestInfo hitInfo = _c1flexGrid.HitTest(e.X, e.Y);
                //_c1flexGrid.RowSel = hitInfo.Row;

                // if (hitInfo.Row > 0)
                //{
                //_c1flexGrid.Focus();
                if (pnlInternalValueCode.Visible == true)
                {
                    pnlInternalValueCode.Visible = false;
                    pnlInternalValueCode.SendToBack();
                }
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                }
                if (pnlInternalOccurrenceCode.Visible == true)
                {
                    pnlInternalOccurrenceCode.Visible = false;
                    pnlInternalOccurrenceCode.SendToBack();
                }
                if (pnlInternalSpanCodes.Visible == true)
                {
                    pnlInternalSpanCodes.Visible = false;
                    pnlInternalSpanCodes.SendToBack();
                }
                tempRow = _c1flexGrid.HitTest(e.X, e.Y).Row;
                if (tempRow > 0 && (Convert.ToString(c1ValueCode.Cols[1][tempRow]) != "" || Convert.ToString(c1ValueCode.Cols[2][tempRow]) != "" || Convert.ToString(c1ValueCode.Cols[3][tempRow]) != ""))
                {
                    c1ValueCode.ContextMenuStrip = cmnu_DeleteUB04Data;
                    _c1flexGrid.Select(tempRow, 1);
                }
                //
              
            }
        }

        private void txttypeofbilling_TextChanged(object sender, EventArgs e)
        {
           if (txttypeofbilling.Text.Trim() == "")
            {
                _oUB.TypeofbillDeleted = true;
            }
            else
            {
                _oUB.TypeofbillDeleted = false;
            }
        }

        private void c1OccrenceCode_AfterSelChange(object sender, RangeEventArgs e)
        {

        }

        private void c1OccrenceCode_CellChanged(object sender, RowColEventArgs e)
        {
            if (c1OccrenceCode.RowSel > 0 && c1OccrenceCode.ColSel == 3)
            {
                if (c1OccrenceCode.Row == 1)
                {
                    if (Convert.ToString(c1OccrenceCode.GetData(c1OccrenceCode.RowSel, c1OccrenceCode.ColSel)) != "")
                    {
                        _oUB.MinDOSDeleted = false;
                    }
                }

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

          //  int COL_CODE = 1;
            int COL_Description = 2;
            int COL_FromDate = 3;
            int COL_TODATE = 4;

            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 1, "");
            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            if (_c1flexGridforcmuDelete == c1ConditionCode)
            {
                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            }
            if (_c1flexGridforcmuDelete == c1OccrenceCode)
            {
                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            }
            if (_c1flexGridforcmuDelete == c1OccurenceSpanCodeRange)
            {
                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_TODATE, null);
            }
            if (_c1flexGridforcmuDelete == c1ValueCode)
            {
                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 3, null);
            }
            #region "Commented Code"
            //switch (_c1flexGridforcmuDelete.ColSel)
            //{

            //    case 1:
            //        {
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 1, "");
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            if (_c1flexGridforcmuDelete == c1ConditionCode)
            //             {
            //                 _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //             }
            //            if (_c1flexGridforcmuDelete == c1OccrenceCode)
            //             {
            //                 _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //             }
            //            if (_c1flexGridforcmuDelete == c1OccurenceSpanCodeRange)
            //             {
            //                 _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //                 _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_TODATE, null);
            //             }
            //            if (_c1flexGridforcmuDelete == c1ValueCode)
            //             {
            //                 _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 3, null);
            //             }

            //        }
            //        break;
            //    case 2:
            //        {
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_CODE, "");
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            if (_c1flexGridforcmuDelete == c1ConditionCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccrenceCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccurenceSpanCodeRange)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_TODATE, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1ValueCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 3, "");
            //            }

            //        }
            //        break;

            //    case 3:
            //        {

            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_CODE, "");
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            if (_c1flexGridforcmuDelete == c1ConditionCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccrenceCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccurenceSpanCodeRange)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_TODATE, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1ValueCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 3, "");
            //            }


            //        }
            //        break;
            //    case 4:
            //        {

            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_CODE, "");
            //            _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            if (_c1flexGridforcmuDelete == c1ConditionCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_Description, "");
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccrenceCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1OccurenceSpanCodeRange)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_FromDate, null);
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, COL_TODATE, null);
            //            }
            //            if (_c1flexGridforcmuDelete == c1ValueCode)
            //            {
            //                _c1flexGridforcmuDelete.SetData(_c1flexGridforcmuDelete.RowSel, 3, "");
            //            }
                        
            //            //if (_c1flexGridforcmuDelete.Editor != null)
            //            //{
            //            //    _c1flexGridforcmuDelete.ChangeEdit -= new System.EventHandler(this._c1flexGrid_ChangeEdit);
            //            //    _c1flexGridforcmuDelete.Editor.Text = "";
            //            //    _c1flexGridforcmuDelete.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            //            //}
            //            break;
            //        }


            //}
            #endregion

        }
        #endregion " C1 Grid Events "

        private void txtAdmissionType_TextChanged(object sender, EventArgs e)
        {
            if (txtAdmissionType.Text.Trim() == "")
            {
                _oUB.AdmitTypeDeleted = true;
            }
            else
            {
                _oUB.AdmitTypeDeleted = false;
            }
        }

        private void txtDischargeStatus_TextChanged(object sender, EventArgs e)
        {
            if (txtDischargeStatus.Text.Trim() == "")
            {
                _oUB.DischargeStatusDeleted = true;
            }
            else
            {
                _oUB.DischargeStatusDeleted = false;
            }
        }

        private void mskAdmitDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }
        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }
        private void mskAdmitDate_Validating(object sender, CancelEventArgs e)
        {
              MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsValidDate = true;
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid
                            _IsValidDate = false;
                            e.Cancel = true;
                        }
                    }
                }
        }

        private void txtAdmtHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) ))
            {
                e.Handled = true;
            }
            
        }

        private void txtdischargeHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }
    }
}

