using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupAntiScrubber : Form
    {

        #region " Constructors "

        public frmSetupAntiScrubber(string DatabaseConnectionString)
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

        public frmSetupAntiScrubber(string DatabaseConnectionString,Int64 ScrubberID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _ScrubberID = ScrubberID;
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

        #endregion " Constructors "

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ScrubberID = 0;
        private string _Code = "";
        private string _Description = "";
        private string _POSCode = "";
        private string _TOSCode = "";
        private Int64 _ClinicID = 0;
        private string _MessageBoxCaption = "";

        gloGridListControl ogloGridListControl = null;

        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_DESC = 2;
        const int COL_COUNT = 3;
        #endregion " Private Variables "

        #region " Property Procedures " 

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 ScrubberID
        {
            get { return _ScrubberID; }
            set { _ScrubberID = value; }
        }
        public string CPTCode
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string CPTDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public string POSCode
        {
            get { return _POSCode; }
            set { _POSCode = value; }
        }
        public string TOSCode
        {
            get { return _TOSCode; }
            set { _TOSCode = value; }
        }

        #endregion " Property Procedures "

        #region " Form Load Event "

        private void frmSetupAntiScrubber_Load(object sender, EventArgs e)
        {
           
            gloC1FlexStyle.Style(c1CPT,false);

            try
            {
                cmbTOS.Select();
                FillPOS();
                FillTOS();
                DesignGrid();
                FillControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion " Form Load Event "

        #region " Fill Data Methods "

        private void FillTOS()
        {
            CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            DataTable dtTOS = new DataTable();
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
            DataTable dtPOS = new DataTable();
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

        private void FillControls()
        {
            gloAntiScrubber ogloAntiScrubber = new gloAntiScrubber(_databaseconnectionstring);
            try
            {
                AntiScrubber oAntiScrubber = new AntiScrubber();
                oAntiScrubber = ogloAntiScrubber.GetAntiScrubber(_ScrubberID);

                if (oAntiScrubber != null)
                {
                    if (oAntiScrubber.CPTs != null)
                    {
                        for (int i = 0; i < oAntiScrubber.CPTs.Count; i++)
                        {
                            C1.Win.C1FlexGrid.Row oNewRow = c1CPT.Rows.Add();
                            c1CPT.SetData(oNewRow.Index, COL_ID, oAntiScrubber.CPTs[i].ID);
                            c1CPT.SetData(oNewRow.Index, COL_CODE, oAntiScrubber.CPTs[i].Code);
                            c1CPT.SetData(oNewRow.Index, COL_DESC, oAntiScrubber.CPTs[i].Description);
                        }
                        if (oAntiScrubber.POSCode != null && Convert.ToString(oAntiScrubber.POSCode).Trim()!="")
                        {
                            cmbPOS.SelectedValue = (object)oAntiScrubber.POSCode;
                        }
                        if (oAntiScrubber.TOSCode != null && Convert.ToString(oAntiScrubber.TOSCode).Trim()!="")
                        {
                            cmbTOS.SelectedValue = (object)oAntiScrubber.TOSCode;
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
                if (ogloAntiScrubber != null)
                {
                    ogloAntiScrubber.Dispose();
                }
            }
        }

        #endregion

        #region " Private Methods "

        private void DesignGrid()
        {
            try
            {
                int _width = 0;
                #region " CPT Grid "

                c1CPT.Rows.Count = 1;
                c1CPT.Rows.Fixed = 1;
                c1CPT.Cols.Count = COL_COUNT;
                c1CPT.Cols.Fixed = 0;

                c1CPT.SetData(0, COL_ID, "ID");
                c1CPT.SetData(0, COL_CODE, "Code");
                c1CPT.SetData(0, COL_DESC, "Description");

                c1CPT.Cols[COL_ID].Visible = false;
                c1CPT.Cols[COL_CODE].Visible = true;
                c1CPT.Cols[COL_DESC].Visible = true;

                c1CPT.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1CPT.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                _width = pnlCPTGrid.Width - 2;

                c1CPT.Cols[COL_ID].Width = 0;
                c1CPT.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.4);
                c1CPT.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.60);



                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

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


                if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                    pnlInternalControl.SetBounds(pnlCPTGrid.Location.X, pnlCPTGrid.Location.Y, 0, 0, BoundsSpecified.Location);


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

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
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

                        switch (ogloGridListControl.ControlType)
                        {

                            case gloGridListControlType.CPT:
                                {
                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isExists = false;
                                    for (int j = 0; j <= c1CPT.Rows.Count-1; j++)
                                    {
                                        if (c1CPT.GetData(j, 1).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isExists = true;
                                        }

                                    }
                                    //End 

                                    if(!isExists)
                                    {
                                        C1.Win.C1FlexGrid.Row oNewRow = c1CPT.Rows.Add();
                                        c1CPT.SetData(oNewRow.Index, COL_ID, ogloGridListControl.SelectedItems[0].ID);
                                        c1CPT.SetData(oNewRow.Index, COL_CODE, ogloGridListControl.SelectedItems[0].Code);
                                        c1CPT.SetData(oNewRow.Index, COL_DESC, ogloGridListControl.SelectedItems[0].Description);
                                    }
                                }
                                break;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                c1CPTSearchGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                CloseInternalControl();
            }
        }

        private void SaveScrubber()
        {
            gloAntiScrubber ogloAntiScrubber = new gloAntiScrubber(_databaseconnectionstring);
            try
            {

                AntiScrubber oAntiScrubber = new AntiScrubber();
                oAntiScrubber.ScrubberID = _ScrubberID;
                oAntiScrubber.ClinicID = this.ClinicID;
                oAntiScrubber.POSCode = Convert.ToString(cmbPOS.SelectedValue);
                oAntiScrubber.TOSCode = Convert.ToString(cmbTOS.SelectedValue);

                for (int k = 1; k < c1CPT.Rows.Count; k++)
                {
                    oAntiScrubber.CPTs.Add(Convert.ToInt64(c1CPT.GetData(k, COL_ID)), Convert.ToString(c1CPT.GetData(k, COL_CODE)), Convert.ToString(c1CPT.GetData(k, COL_DESC)));
                }

                ogloAntiScrubber.Add(oAntiScrubber);
                //this.Close();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloAntiScrubber.Dispose();
            }
        }

        #endregion

        #region " C1 and Other Events "

        private void c1CPTSearchGrid_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1CPTSearchGrid.Editor.Text;

                    if (ogloGridListControl.ControlHeader == "ICD9")
                    {
                        if (_strSearchString.Length == 3)
                        {
                            _strSearchString = _strSearchString + ".";
                        }
                        else if (_strSearchString.Length > 3)
                        {
                            if (_strSearchString.Substring(3, 1).ToString() != ".")
                            {
                                string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                                _strSearchString = _PeriodSearch;
                            }
                        }
                    }
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1CPTSearchGrid_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1CPTSearchGrid_KeyUp(object sender, KeyEventArgs e)
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnSelectCPT_Click(object sender, EventArgs e)
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
                    OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnDeleteCPT_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    c1CPT.Rows.Remove(c1CPT.Row);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

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

        #endregion " C1 and Other Events "

        #region " Toolstrip Button Events"

        private void tsb_OK_Click(object sender, EventArgs e)
        {

            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    if (Convert.ToString(cmbPOS.SelectedValue).Trim() != "" || Convert.ToString(cmbTOS.SelectedValue).Trim() != "")
                    {
                        SaveScrubber();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Select atleast one POS or TOS.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select atleast one CPT.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    if (Convert.ToString(cmbPOS.SelectedValue).Trim() != "" || Convert.ToString(cmbTOS.SelectedValue).Trim() != "")
                    {
                        SaveScrubber();
                         _Code = "";
                         _Description = "";
                         _POSCode = "";
                         _TOSCode = "";
                         txtCode.Text = "";
                         txtDescription.Text = "";
                         cmbPOS.SelectedIndex = 0;
                         cmbTOS.SelectedIndex = 0;
                         _ScrubberID = 0;
                         //if (c1CPT.DataSource != null)
                         //{
                         //    c1CPT.DataSource = null;
                         //}
                         //c1CPT.Refresh();
                         int j = c1CPT.Rows.Count;
                         
                             c1CPT.Rows.RemoveRange(1,j-1);
                         
                         //c1CPT.Rows.c
                         //c1CPT.Rows[1].Clear(C1.Win.C1FlexGrid.ClearFlags.UserData);

                    }
                    else
                    {
                        MessageBox.Show("Select atleast one POS or TOS.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select atleast one CPT.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void c1CPT_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

      

    

    }
}