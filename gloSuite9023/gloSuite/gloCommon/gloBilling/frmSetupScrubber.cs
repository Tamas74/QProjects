using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace gloBilling
{


    public partial class frmSetupScrubber : Form
    {
        #region "Declaration"

        private string _databaseconnectionstring;
        public string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _sCPTCode = "";
        gloGridListControl ogloGridListControl = null;
        DataTable dtPOS = new DataTable();
        DataTable dtTOS = new DataTable();
        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_DESC = 2;
        const int COL_COUNT = 3;

        #endregion "Declaration"

        #region "Contructor"

        public frmSetupScrubber(String databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = databaseconnectionstring;

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
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmSetupScrubber(String CPTCode, String databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = databaseconnectionstring;
            _sCPTCode = CPTCode;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
        }

        #endregion

        private void frmSetupScrubber_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1ModifiersSelected, false);
            gloC1FlexStyle.Style(c1DiagnosisSelected, false);

            //gloC1FlexStyle.Style(c1Diagnosis, false);
            //gloC1FlexStyle.Style(c1Modifier, false);


            DesignGrid();
            FillTOS();
            FillPOS();

            if (_sCPTCode.Trim() != "")
            {
                Fill_Scrubber();
            }
        }

        #region "Fill Data Methods"

        private void Fill_Scrubber()
        {
            gloScrubber ogloScrubber = new gloScrubber(_databaseconnectionstring);
            try
            {
                Scrubber oScrubber = new Scrubber();
                oScrubber = ogloScrubber.GetScrubber(_sCPTCode);

                if (oScrubber != null)
                {
                    txtCPTCodeFrom.Text = oScrubber.CPTCode;
                    txtCPTCodeTo.Text = oScrubber.CPTCode;

                    txtCPTCodeFrom.ReadOnly = true;
                    txtCPTCodeTo.ReadOnly = true;

                    cmbPOS.SelectedValue = oScrubber.POSCode;
                    cmbTOS.SelectedValue = oScrubber.TOSCode;

                    if (oScrubber.Diagnosis != null)
                    {
                        for (int i = 0; i < oScrubber.Diagnosis.Count; i++)
                        {
                            C1.Win.C1FlexGrid.Row oNewRow = c1DiagnosisSelected.Rows.Add();
                            c1DiagnosisSelected.SetData(oNewRow.Index, COL_ID, oScrubber.Diagnosis[i].ID);
                            c1DiagnosisSelected.SetData(oNewRow.Index, COL_CODE, oScrubber.Diagnosis[i].Code);
                            c1DiagnosisSelected.SetData(oNewRow.Index, COL_DESC, oScrubber.Diagnosis[i].Description);
                        }
                    }

                    if (oScrubber.Modifiers != null)
                    {
                        for (int i = 0; i < oScrubber.Modifiers.Count; i++)
                        {
                            C1.Win.C1FlexGrid.Row oNewRow = c1ModifiersSelected.Rows.Add();
                            c1ModifiersSelected.SetData(oNewRow.Index, COL_ID, oScrubber.Modifiers[i].ID);
                            c1ModifiersSelected.SetData(oNewRow.Index, COL_CODE, oScrubber.Modifiers[i].Code);
                            c1ModifiersSelected.SetData(oNewRow.Index, COL_DESC, oScrubber.Modifiers[i].Description);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloScrubber != null)
                {
                    ogloScrubber.Dispose();
                }
            }
        }

        private void FillTOS()
        {
            CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
           
            try
            {
                dtTOS = oTOSPOS.GetTOS(0);

                if (dtTOS != null && dtTOS.Rows.Count > 0)
                {
                    DataTable dtBindTable = new DataTable();
                    dtBindTable.Columns.Add("Code");
                    dtBindTable.Columns.Add("Description");

                    for (int i = 0; i < dtTOS.Rows.Count; i++)
                    {
                        DataRow dr = dtBindTable.NewRow();
                        dr["Code"] = Convert.ToString(dtTOS.Rows[i]["sTOSCode"]);
                        dr["Description"] = Convert.ToString(dtTOS.Rows[i]["sTOSCode"]) + "-" + Convert.ToString(dtTOS.Rows[i]["sDescription"]);
                        dtBindTable.Rows.Add(dr);

                    }
                    dtBindTable.Rows.InsertAt(dtBindTable.NewRow(), 0);
                    dtBindTable.AcceptChanges();

                    cmbTOS.DataSource = dtBindTable;
                    cmbTOS.ValueMember = "Code";
                    cmbTOS.DisplayMember = "Description";
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillPOS()
        {
            CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            
            try
            {
                dtPOS = oTOSPOS.GetPOS(0);

                if (dtPOS != null && dtPOS.Rows.Count > 0)
                {
                    DataTable dtBindTable = new DataTable();
                    dtBindTable.Columns.Add("Code");
                    dtBindTable.Columns.Add("Description");

                    for (int i = 0; i < dtPOS.Rows.Count; i++)
                    {
                        DataRow dr = dtBindTable.NewRow();
                        dr["Code"] = Convert.ToString(dtPOS.Rows[i]["sPOSCode"]);
                        dr["Description"] = Convert.ToString(dtPOS.Rows[i]["sPOSCode"]) + "-" + Convert.ToString(dtPOS.Rows[i]["sPOSName"]);
                        dtBindTable.Rows.Add(dr);
                    }

                    dtBindTable.Rows.InsertAt(dtBindTable.NewRow(), 0);
                    dtBindTable.AcceptChanges();

                    cmbPOS.DataSource = dtBindTable;
                    cmbPOS.ValueMember = "Code";
                    cmbPOS.DisplayMember = "Description";
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Tool Strip Button clicks"

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            gloScrubber ogloScrubber = new gloScrubber(_databaseconnectionstring);
            try
            {
                if (ValidateData())
                {
                    Int64 nIndex=-1;
                    string strCPT = txtCPTCodeFrom.Text.Trim();
                    Int64 nCPTCodeFrom = Convert.ToInt64(txtCPTCodeFrom.Text.Trim());
                    Int64 nCPTCodeTo = Convert.ToInt64(txtCPTCodeTo.Text.Trim());
                    if (strCPT.Length != nCPTCodeFrom.ToString().Length)
                    {
                        nIndex = strCPT.IndexOfAny("123456789".ToCharArray());
                    }
                    for (Int64 i = nCPTCodeFrom; i <= nCPTCodeTo; i++)
                    {
                        Scrubber oScrubber = new Scrubber();

                        if (nIndex == -1)
                        {
                            oScrubber.ScrubberID = 0;
                            oScrubber.CPTCode = i.ToString();
                            oScrubber.CPTDescription = "";
                        }
                        else
                        {

                            oScrubber.ScrubberID = 0;
                            if (i != nCPTCodeTo)
                            {
                                if (nIndex == 0)
                                {
                                    oScrubber.CPTCode = i.ToString();
                                }
                                else if (nIndex == 1)
                                {
                                    oScrubber.CPTCode = (i.ToString().Length == 5 ? "" + i.ToString() : "0" + i.ToString());
                                }
                                else if (nIndex == 2)
                                {
                                    oScrubber.CPTCode = (i.ToString().Length == 5 ? "" + i.ToString() : (i.ToString().Length == 4 ? "0" + i.ToString() : "00" + i.ToString()));
                                }
                                else if (nIndex == 3)
                                {
                                    oScrubber.CPTCode = (i.ToString().Length == 5 ? "" + i.ToString() : (i.ToString().Length == 4 ? "0" + i.ToString() : (i.ToString().Length == 3 ? "00" + i.ToString() :  "000" + i.ToString())));
                                }
                                else if (nIndex == 4)
                                {
                                    oScrubber.CPTCode = (i.ToString().Length == 5 ? "" + i.ToString() : (i.ToString().Length == 4 ? "0" + i.ToString() : (i.ToString().Length == 3 ? "00" + i.ToString() : (i.ToString().Length == 2?"000" + i.ToString():"0000" + i.ToString()))));
                                }
                            }
                            else
                            {
                                oScrubber.CPTCode = txtCPTCodeTo.Text.Trim();
                            }
                            oScrubber.CPTDescription = "";
                        }

                        if (cmbTOS.SelectedIndex != -1)
                        {
                            oScrubber.TOSCode = cmbTOS.SelectedValue.ToString();
                            EnumerableRowCollection result = from TOS in dtTOS.AsEnumerable()
                                                             where TOS.Field<string>("sTOSCode").Equals(oScrubber.TOSCode)
                                                             select TOS;
                            foreach (DataRow dr in result)
                            {
                                oScrubber.TOSDesc = dr.Field<string>("sDescription").ToUpper();
                            }     
                            
                        }
                        else
                        {
                            oScrubber.TOSCode = "";
                            oScrubber.TOSDesc = "";
                        }
                       

                        if (cmbPOS.SelectedIndex != -1)
                        {
                            oScrubber.POSCode = cmbPOS.SelectedValue.ToString();
                            EnumerableRowCollection result = from POS in dtPOS.AsEnumerable()
                                                             where POS.Field<string>("sPOSCode").Equals(oScrubber.POSCode)
                                                             select POS;
                            foreach (DataRow dr in result)
                            {
                                oScrubber.POSDesc = dr.Field<string>("sPOSName").ToUpper();
                            }                           
                        }
                        else
                        {
                            oScrubber.POSCode = "";
                            oScrubber.POSDesc = "";
                        }
                       


                        for (int k = 1; k < c1ModifiersSelected.Rows.Count; k++)
                        {
                            oScrubber.Modifiers.Add(0, Convert.ToString(c1ModifiersSelected.GetData(k, COL_CODE)), Convert.ToString(c1ModifiersSelected.GetData(k, COL_DESC)));
                        }
                        for (int k = 1; k < c1DiagnosisSelected.Rows.Count; k++)
                        {
                            oScrubber.Diagnosis.Add(0, Convert.ToString(c1DiagnosisSelected.GetData(k, COL_CODE)), Convert.ToString(c1DiagnosisSelected.GetData(k, COL_DESC)));
                        }

                        ogloScrubber.Add(oScrubber);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloScrubber.Dispose();
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {

            gloScrubber ogloScrubber = new gloScrubber(_databaseconnectionstring);
            try
            {
                if (ValidateData())
                {
                    Int64 nCPTCodeFrom = Convert.ToInt64(txtCPTCodeFrom.Text.Trim());
                    Int64 nCPTCodeTo = Convert.ToInt64(txtCPTCodeTo.Text.Trim());

                    for (Int64 i = nCPTCodeFrom; i <= nCPTCodeTo; i++)
                    {
                        Scrubber oScrubber = new Scrubber();

                        oScrubber.ScrubberID = 0;
                        oScrubber.CPTCode = i.ToString();
                        oScrubber.CPTDescription = "";
                        if (cmbTOS.SelectedIndex != -1)
                        {
                            oScrubber.TOSCode = cmbTOS.SelectedValue.ToString();
                        }
                        else
                        {
                            oScrubber.TOSCode = "";
                        }
                        oScrubber.TOSDesc = "";

                        if (cmbPOS.SelectedIndex != -1)
                        {
                            oScrubber.POSCode = cmbPOS.SelectedValue.ToString();
                        }
                        else
                        {
                            oScrubber.POSCode = "";
                        }
                        oScrubber.POSDesc = "";


                        for (int k = 1; k < c1ModifiersSelected.Rows.Count; k++)
                        {
                            oScrubber.Modifiers.Add(0, Convert.ToString(c1ModifiersSelected.GetData(k, COL_CODE)), Convert.ToString(c1ModifiersSelected.GetData(k, COL_DESC)));
                        }
                        for (int k = 1; k < c1DiagnosisSelected.Rows.Count; k++)
                        {
                            oScrubber.Diagnosis.Add(0, Convert.ToString(c1DiagnosisSelected.GetData(k, COL_CODE)), Convert.ToString(c1DiagnosisSelected.GetData(k, COL_DESC)));
                        }

                        ogloScrubber.Add(oScrubber);
                        _sCPTCode = "";
                        txtCPTCodeFrom.Text = "";
                        txtCPTCodeTo.Text = "";
                        cmbTOS.SelectedIndex = -1;
                        cmbPOS.SelectedIndex = -1;
                        int m = c1DiagnosisSelected.Rows.Count;
                        int j = c1ModifiersSelected.Rows.Count;
                        if (m > 1)
                        {
                            c1DiagnosisSelected.Rows.RemoveRange(1, m - 1);
                        }
                        if (j > 1)
                        {
                            c1ModifiersSelected.Rows.RemoveRange(1, j - 1);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloScrubber.Dispose();
            }
        }

        #endregion

        private bool ValidateData()
        {
            try
            {
                if (txtCPTCodeFrom.Text.Trim() == "")
                {
                    MessageBox.Show("Enter CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCPTCodeFrom.Focus();
                    return false;
                }

                if (txtCPTCodeTo.Text.Trim() == "")
                {
                    txtCPTCodeTo.Text = txtCPTCodeFrom.Text;
                }

                if (Convert.ToInt64(txtCPTCodeFrom.Text.Trim()) > Convert.ToInt64(txtCPTCodeTo.Text.Trim()))
                {
                    MessageBox.Show("Starting CPT code should be greater than end CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCPTCodeFrom.Focus();
                    return false;
                }
            }
            catch (Exception )//ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
            return true;
        }

        private void DesignGrid()
        {
            try
            {
                #region "Modifier Grid"
                c1ModifiersSelected.Rows.Count = 1;
                c1ModifiersSelected.Rows.Fixed = 1;
                c1ModifiersSelected.Cols.Count = COL_COUNT;
                c1ModifiersSelected.Cols.Fixed = 0;

                c1ModifiersSelected.SetData(0, COL_ID, "ID");
                c1ModifiersSelected.SetData(0, COL_CODE, "Code");
                c1ModifiersSelected.SetData(0, COL_DESC, "Description");

                c1ModifiersSelected.Cols[COL_ID].Visible = false;
                c1ModifiersSelected.Cols[COL_CODE].Visible = true;
                c1ModifiersSelected.Cols[COL_DESC].Visible = true;

                c1ModifiersSelected.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ModifiersSelected.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                int _width = pnlModifiers.Width - 2;

                c1ModifiersSelected.Cols[COL_ID].Width = 0;
                c1ModifiersSelected.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.4);
                c1ModifiersSelected.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.60);


                c1ModifiersSelected.AllowEditing = false;
                #endregion

                #region "Diagnosis Grid"
                c1DiagnosisSelected.Rows.Count = 1;
                c1DiagnosisSelected.Rows.Fixed = 1;
                c1DiagnosisSelected.Cols.Count = COL_COUNT;
                c1DiagnosisSelected.Cols.Fixed = 0;

                c1DiagnosisSelected.SetData(0, COL_ID, "ID");
                c1DiagnosisSelected.SetData(0, COL_CODE, "Code");
                c1DiagnosisSelected.SetData(0, COL_DESC, "Description");

                c1DiagnosisSelected.Cols[COL_ID].Visible = false;
                c1DiagnosisSelected.Cols[COL_CODE].Visible = true;
                c1DiagnosisSelected.Cols[COL_DESC].Visible = true;

                c1DiagnosisSelected.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1DiagnosisSelected.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                _width = pnlDiagnosis.Width - 2;

                c1DiagnosisSelected.Cols[COL_ID].Width = 0;
                c1DiagnosisSelected.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.4);
                c1DiagnosisSelected.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.60);


                c1ModifiersSelected.AllowEditing = false;
                #endregion
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #region "Modifier Selection Events"

        private void c1Modifier_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, 0, 0, "");
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
        }

        private void c1Modifier_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1Modifier.Editor.Text;
                    //ogloGridListControl.AdvanceSearch(_strSearchString);
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void c1Modifier_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {

                                    bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                                    if (_IsItemSelected)
                                    {
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    CloseInternalControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnSelectMod_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible)
                {
                    if (ogloGridListControl != null)
                    {

                        bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                        if (_IsItemSelected)
                        {
                        }
                    }
                }
                else
                {
                    OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, 0, 0, "");
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
        }

        private void btnDeleteMod_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1ModifiersSelected.Rows.Count > 1)
                {
                    c1ModifiersSelected.Rows.Remove(c1ModifiersSelected.Row);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion

        #region "Diagnosis Selection"

        private void c1Diagnosis_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.ICD9, "Diagnosis", false, 0, 0, "");
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
        }

        private void c1Diagnosis_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1Diagnosis.Editor.Text;

                    //******************************************************
                    //Commented By Debasish as Business Logic shifted to SP
                    //******************************************************
                    ////if (_strSearchString.Length == 3)
                    ////{
                    ////    _strSearchString = _strSearchString + ".";
                    ////}
                    ////else if (_strSearchString.Length > 3)
                    ////{
                    ////    if (_strSearchString.Substring(3, 1).ToString() != ".")
                    ////    {
                    ////        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                    ////        _strSearchString = _PeriodSearch;
                    ////    }

                    ////}
                    //******************************************************

                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void c1Diagnosis_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {

                                    bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                                    if (_IsItemSelected)
                                    {
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    CloseInternalControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnDeleteDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1DiagnosisSelected.Rows.Count > 1)
                {
                    c1DiagnosisSelected.Rows.Remove(c1DiagnosisSelected.Row);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnSelectDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible)
                {
                    if (ogloGridListControl != null)
                    {

                        bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                        if (_IsItemSelected)
                        {
                        }
                    }
                }
                else
                {
                    OpenInternalControl(gloGridListControlType.ICD9, "Diagnosis", false, 0, 0, "");
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
        }

        #endregion

        #region "Grid Control Methods & events"

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {
                //_dxCodeForDistinct = "";
                //_dxDescriptionForDistinct = "";

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                //int _x = c1Modifier.Cols[ColIndex].Left;
                //int _y = c1Modifier.Rows[RowIndex].Bottom;
                //int _width = pnlInternalControl.Width;
                //int _height = pnlInternalControl.Height;
                //int _parentleft = this.Parent.Bounds.Left;
                //int _parentwidth = this.Parent.Bounds.Width;
                //int _diffFactor = _parentwidth - _x;

                //if (_diffFactor < _width)
                //{
                //    _x = this.Parent.Bounds.Width + (_diffFactor);
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}
                //else
                //{
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}

                if (ogloGridListControl.ControlType == gloGridListControlType.Modifier)
                    pnlInternalControl.SetBounds(pnlModifiers.Location.X, pnlModifiers.Location.Y, 0, 0, BoundsSpecified.Location);
                else if (ogloGridListControl.ControlType == gloGridListControlType.ICD9)
                    pnlInternalControl.SetBounds(pnlDiagnosis.Location.X, pnlDiagnosis.Location.Y, 0, 0, BoundsSpecified.Location);


                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
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
                //RePositionInternalControl();
            }
            return _result;
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl.SelectedItems != null)
                {
                    if (ogloGridListControl.SelectedItems.Count > 0)
                    {
                        //switch (ogloGridListControl.ControlType)
                        //{
                        //}

                        switch (ogloGridListControl.ControlType)
                        {

                            case gloGridListControlType.ICD9:
                                {

                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isCPTExists = false;
                                    for (int j = 0; j <= c1DiagnosisSelected.Rows.Count - 1; j++)
                                    {
                                        if (c1DiagnosisSelected.GetData(j, 1).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isCPTExists = true;
                                            break;
                                        }

                                    }
                                    //End 

                                    if (!isCPTExists)
                                    {
                                        C1.Win.C1FlexGrid.Row oNewRow = c1DiagnosisSelected.Rows.Add();
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_ID, ogloGridListControl.SelectedItems[0].ID);
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_CODE, ogloGridListControl.SelectedItems[0].Code);
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_DESC, ogloGridListControl.SelectedItems[0].Description);
                                    }

                                }
                                break;
                            case gloGridListControlType.Modifier:
                                {
                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isModExists = false;
                                    for (int j = 0; j <= c1ModifiersSelected.Rows.Count - 1; j++)
                                    {
                                        if (c1ModifiersSelected.GetData(j, 1).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isModExists = true;
                                            break;
                                        }

                                    }
                                    //End 

                                    if (!isModExists)
                                    {
                                        C1.Win.C1FlexGrid.Row oNewRow = c1ModifiersSelected.Rows.Add();
                                        c1ModifiersSelected.SetData(oNewRow.Index, COL_ID, ogloGridListControl.SelectedItems[0].ID);
                                        c1ModifiersSelected.SetData(oNewRow.Index, COL_CODE, ogloGridListControl.SelectedItems[0].Code);
                                        c1ModifiersSelected.SetData(oNewRow.Index, COL_DESC, ogloGridListControl.SelectedItems[0].Description);
                                    }

                                }
                                break;
                        }


                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                c1Modifier.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                c1Diagnosis.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                CloseInternalControl();
            }
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
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
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
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

        #endregion

        #region "Designer Events"

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void c1ModifiersSelected_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void c1DiagnosisSelected_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }



        private void txtCPTCodeFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtCPTCodeTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }


    }
}