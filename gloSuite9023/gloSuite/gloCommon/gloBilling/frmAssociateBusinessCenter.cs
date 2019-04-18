using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmAssociateBusinessCenter : Form
    {
        const int COL_ID = 0;
        const int COL_PRIORITY = 1;
        const int COL_PROVIDERID = 2;
        const int COL_PROVIDERNAME = 3;
        const int COL_FACILITYID = 4;
        const int COL_FACILITYNAME = 5;
        const int COL_CPTFROM = 6;
        const int COL_CPTFROMDESC = 7;
        const int COL_CPTTO = 8;
        const int COL_CPTTODESC = 9;
        const int COL_BUSINESSCENTERID = 10;
        const int COL_BUSINESSCENTERCODE = 11;
        //const int COL_BUSINESSCENTERNAME = 12;

        const int COL_COUNT = 12;

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _BusinessCenterID = 0;
        private Int64 _ClinicID = 0;
        private bool isClosed = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        public gloGridListControl ogloGridListControl = null;

        public Int64 BusinessCenterID
        {
            set {
                _BusinessCenterID = value;
            }

               get {
                   return _BusinessCenterID;
            }
        }
        public frmAssociateBusinessCenter()
        {
            InitializeComponent();
        }
        public frmAssociateBusinessCenter(Int64 ID, String databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _BusinessCenterID = ID;
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
        private void frmAssociateBusinessCenter_Load(object sender, EventArgs e)
        {
            DesignBillingTaxonomyGrid();
            if (_BusinessCenterID != 0)
            {
                tsb_Save.Visible = false;
                FillValueCode();
            }
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
        }
        private void FillValueCode()
        {
           // bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nValueID,sValueCode,sDescription,IsActive FROM UB_ValueCodes where nValueID= " + _BusinessCenterID + "";
                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //txtCode.Text = Convert.ToString(dt.Rows[0]["sValueCode"]);
                    //txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    //if (Convert.ToBoolean(dt.Rows[0]["IsActive"]) == true)
                    //{
                    //    rbActive.Checked = true;
                    //    rbInactive.Checked = false;
                    //}
                    //else
                    //{
                    //    rbActive.Checked = false;
                    //    rbInactive.Checked = true;
                    //}
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
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            string sMessage = "";
            if (Validate(ref sMessage))
            {
                //ValueCodes ObjValueCodes = new ValueCodes(_databaseconnectionstring);
                //if (ObjValueCodes.IsExistsValueCode(_ValueCodeID, txtCode.Text.Trim()))
                //{
                //    MessageBox.Show("Code is already in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtCode.Focus();
                //    return;
                //}
                //Int64 _tempResult = ObjValueCodes.AddValueCode(_ValueCodeID, txtCode.Text.Trim(), txtDescription.Text.Trim(), (rbActive.Checked ? true : false));
                //if (_tempResult > 0)
                //{
                //    _ValueCodeID = 0;// _tempResult;
                //}
                //else
                //{           
                //    txtCode.Text = "";
                //    txtDescription.Text = "";
                //    _ValueCodeID = 0;
                //}

                //txtCode.Text = "";
                //txtDescription.Text = "";

                if (isClosed == true)
                {
                    this.Close();
                }
            }
           
            
        }

        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            isClosed = true; 
            tsb_Save_Click(null, null);
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool  Validate(ref string sMessage)
        {
            Boolean _bresult = true;
            if (!cbx_BusCenByDoctor.Checked && !cbx_BusCenByFacility.Checked && !cbx_BusCenByCPT.Checked)
            {
                sMessage = "Select any 1 option from the top.";
                return false;
            }
            if (c1BusinessCenter.Rows.Count > 1)
            {
                //if (cbx_BusCenByDoctor.Checked)
                //{
                //    if (c1BusinessCenter.FindRow(null, 0, COL_PROVIDERNAME, true) > 0)
                //    {
                //        sMessage = "Provider field cannot be left blank.";
                //        return false;
                //    }
                //    if (c1BusinessCenter.FindRow("", 0, COL_PROVIDERNAME, true) > 0)
                //    {
                //        sMessage = "Provider field cannot be left blank.";
                //        return false;
                //    }
                //}
                //if (cbx_BusCenByFacility.Checked)
                //{
                //    if (c1BusinessCenter.FindRow(null, 0, COL_FACILITYNAME, true) > 0)
                //    {
                //        sMessage = "Facility field cannot be left blank.";
                //        return false;
                //    }
                //    if (c1BusinessCenter.FindRow("", 0, COL_FACILITYNAME, true) > 0)
                //    {
                //        sMessage = "Facility field cannot be left blank.";
                //        return false;
                //    }
                //}
                //if (cbx_BusCenByCPT.Checked)
                //{
                //    if (c1BusinessCenter.FindRow(null, 0, COL_CPTFROM, true) > 0)
                //    {
                //        sMessage = "Cpt From field cannot be left blank.";
                //        return false;
                //    }
                //    if (c1BusinessCenter.FindRow("", 0, COL_CPTFROM, true) > 0)
                //    {
                //        sMessage = "Cpt From field cannot be left blank.";
                //        return false;
                //    }
                //    if (c1BusinessCenter.FindRow(null, 0, COL_CPTTO, true) > 0)
                //    {
                //        sMessage = "Cpt To field cannot be left blank.";
                //        return false;
                //    }
                //    if (c1BusinessCenter.FindRow("", 0, COL_CPTTO, true) > 0)
                //    {
                //        sMessage = "Cpt To field cannot be left blank.";
                //        return false;
                //    }
                //}             



                for (int i = 1; i <= c1BusinessCenter.Rows.Count - 1; i++)
                {
                    Int16 _Party = 0;
                    if (c1BusinessCenter.GetData(i, COL_PRIORITY) != null)
                    {
                        if (Int16.TryParse(c1BusinessCenter.GetData(i, COL_PRIORITY).ToString(), out _Party) == true)
                        {
                            if (_Party > 0)
                            {

                                for (int j = i + 1; j <= c1BusinessCenter.Rows.Count - 1; j++)
                                {
                                    Int16 _NextParty = 0;
                                    if (Int16.TryParse(c1BusinessCenter.GetData(j, COL_PRIORITY).ToString(), out _NextParty) == true)
                                    {
                                        if (_Party == _NextParty)
                                        {
                                            MessageBox.Show("Same priority is found for multiple rules. Please assign unique priority.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1BusinessCenter.Select(i, COL_PRIORITY);
                                            c1BusinessCenter.Focus();
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                for (int i = 1; i <= c1BusinessCenter.Rows.Count - 1; i++)
                {
                    Int16 _Party = 0;
                    if (c1BusinessCenter.GetData(i, COL_PRIORITY) != null && c1BusinessCenter.GetData(i, COL_PRIORITY).ToString().Length > 0)
                    {
                        if (Int16.TryParse(c1BusinessCenter.GetData(i, COL_PRIORITY).ToString(), out _Party) == true)
                        {
                            if (_Party != i)
                            {
                                MessageBox.Show("Priority is out of order. Please specify priority in sequence. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1BusinessCenter.Select(i, COL_PRIORITY); 
                                c1BusinessCenter.Focus();                               
                                return false;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }


                if (mskStartDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the start date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskEnddate.Focus();
                    mskEnddate.Select();
                }
                else if (mskEnddate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the end date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskEnddate.Focus();
                    mskEnddate.Select();
                }
                else if (mskStartDate.Text.Trim() != "/  /" && IsValidDate(mskStartDate.Text.Trim(), "mskStartDate") == false)
                {
                    MessageBox.Show("Please enter valid start date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskStartDate.Focus();
                    mskStartDate.Select();
                }
                else if (mskEnddate.Text.Trim() != "/  /" && IsValidDate(mskEnddate.Text.Trim(), "mskEndtDate") == false)
                {
                    MessageBox.Show("Please enter valid end date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskEnddate.Focus();
                    mskEnddate.Select();
                }  
                else if (IsValidDate(mskEnddate.Text.Trim(), "mskEndDate") == true)
                {
                    if (mskStartDate.Text.Trim() != "/  /" && Convert.ToDateTime(mskStartDate.Text.Trim()) > Convert.ToDateTime(mskEnddate.Text.Trim()))
                        MessageBox.Show("Start date must be less than end date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                sMessage = "Grid cannot be left blank.";
                return false;
            }
            return _bresult;
        }


        private bool IsValidDate(object strDate, string name)
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
                        if (name == "mskStartDate")
                            MessageBox.Show("Please enter valid start date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (name == "mskEndDate")
                            MessageBox.Show("Please enter valid end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (FormatException e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null;
                Success = false; // If this line is reached, an exception was thrown
            }
            return Success;
        }




        private void DesignBillingTaxonomyGrid()
        {

            try
            {
                c1BusinessCenter.Cols.Count = COL_COUNT;
                c1BusinessCenter.Rows.Count = 1;
                c1BusinessCenter.Rows.Fixed = 1;

                c1BusinessCenter.Rows.Add();
                c1BusinessCenter.SetData(0, COL_ID, "ID");
                c1BusinessCenter.SetData(0, COL_PRIORITY, "Priority");
                c1BusinessCenter.SetData(0, COL_PROVIDERID, "Provider ID");
                c1BusinessCenter.SetData(0, COL_PROVIDERNAME, "Provider Name");
                c1BusinessCenter.SetData(0, COL_FACILITYID, "Facility ID");
                c1BusinessCenter.SetData(0, COL_FACILITYNAME, "Facility Name");
                c1BusinessCenter.SetData(0, COL_CPTFROM, "From CPT Code");
                c1BusinessCenter.SetData(0, COL_CPTFROMDESC, "From CPT Code Desc");
                c1BusinessCenter.SetData(0, COL_CPTTO, "To CPT Code");
                c1BusinessCenter.SetData(0, COL_CPTTODESC, "To CPT Code Desc");
                c1BusinessCenter.SetData(0, COL_BUSINESSCENTERID, "Business Center ID");
                c1BusinessCenter.SetData(0, COL_BUSINESSCENTERCODE, "Business Center Code");
               // c1BusinessCenter.SetData(0, COL_BUSINESSCENTERNAME, "Business Center Name");


                c1BusinessCenter.Cols[COL_ID].AllowEditing = false;
                c1BusinessCenter.Cols[COL_PRIORITY].AllowEditing = true;
                c1BusinessCenter.Cols[COL_PROVIDERID].AllowEditing = false;
                c1BusinessCenter.Cols[COL_PROVIDERNAME].AllowEditing = true;
                c1BusinessCenter.Cols[COL_FACILITYID].AllowEditing = false;
                c1BusinessCenter.Cols[COL_FACILITYNAME].AllowEditing = true;
                c1BusinessCenter.Cols[COL_CPTFROM].AllowEditing = true;
                c1BusinessCenter.Cols[COL_CPTFROMDESC].AllowEditing = false;
                c1BusinessCenter.Cols[COL_CPTTO].AllowEditing = true;
                c1BusinessCenter.Cols[COL_CPTTODESC].AllowEditing = false;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERID].AllowEditing = false;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERCODE].AllowEditing = true;
               // c1BusinessCenter.Cols[COL_BUSINESSCENTERNAME].AllowEditing = false;


                c1BusinessCenter.Cols[COL_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_PRIORITY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_PROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_FACILITYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_FACILITYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_CPTFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_CPTFROMDESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_CPTTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_CPTTODESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
               //c1BusinessCenter.Cols[COL_BUSINESSCENTERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1BusinessCenter.Cols[COL_ID].Width = 0;
                c1BusinessCenter.Cols[COL_PRIORITY].Width = 60;
                c1BusinessCenter.Cols[COL_PROVIDERID].Width = 0;
                c1BusinessCenter.Cols[COL_PROVIDERNAME].Width = 200;
                c1BusinessCenter.Cols[COL_FACILITYID].Width = 0;
                c1BusinessCenter.Cols[COL_FACILITYNAME].Width = 200;
                c1BusinessCenter.Cols[COL_CPTFROM].Width = 102;
                c1BusinessCenter.Cols[COL_CPTFROMDESC].Width = 0;
                c1BusinessCenter.Cols[COL_CPTTO].Width = 95;
                c1BusinessCenter.Cols[COL_CPTTODESC].Width = 0;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERID].Width = 0;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERCODE].Width = 150;
                //c1BusinessCenter.Cols[COL_BUSINESSCENTERNAME].Width = 0;

                c1BusinessCenter.Cols[COL_PRIORITY].DataType=Type.GetType("System.Int64");

                c1BusinessCenter.Cols[COL_ID].Visible = true;
                c1BusinessCenter.Cols[COL_PROVIDERID].Visible = true;
                c1BusinessCenter.Cols[COL_FACILITYID].Visible = true;
                c1BusinessCenter.Cols[COL_CPTFROMDESC].Visible = true;
                c1BusinessCenter.Cols[COL_CPTTODESC].Visible = true;
                c1BusinessCenter.Cols[COL_BUSINESSCENTERCODE].Visible = true;
                //c1BusinessCenter.Cols[COL_BUSINESSCENTERNAME].Visible = true;

                c1BusinessCenter.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;                
                //c1BusinessCenter.ExtendLastCol = true;
                c1BusinessCenter.Font = new Font("Tahoma", 9, FontStyle.Regular);
                c1BusinessCenter.Styles.Fixed.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void Reorder()
        {
            c1BusinessCenter.Sort(SortFlags.Ascending, COL_PRIORITY);           
        }

        private void c1BillingTaxonomy_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (e.Row > 0)
                {
                    switch (e.Col)
                    {
                        case COL_PROVIDERNAME:
                            {
                                //e.Cancel = true;
                                OpenInternalControl(gloGridListControlType.Providers, "Provider", false, e.Row, e.Col, "");
                                string _SearchText = "";
                                if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 0)
                                {
                                    _SearchText = Convert.ToString(c1BusinessCenter.GetData(e.Row, e.Col));
                                    if (_SearchText != "" && ogloGridListControl != null)
                                    {
                                        ogloGridListControl.AdvanceSearch(_SearchText);
                                    }
                                }
                            }
                            break;
                        case COL_CPTFROM:
                            {
                                //e.Cancel = true;
                                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, e.Row, e.Col, "");
                                string _SearchText = "";
                                if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 0)
                                {
                                    _SearchText = Convert.ToString(c1BusinessCenter.GetData(e.Row, e.Col));
                                    if (_SearchText != "" && ogloGridListControl != null)
                                    {
                                        ogloGridListControl.AdvanceSearch(_SearchText);
                                    }
                                }
                            }
                            break;
                        case COL_BUSINESSCENTERCODE:
                            {
                                //e.Cancel = true;
                                OpenInternalControl(gloGridListControlType.BusinessCenter, "BusinessCenter", false, e.Row, e.Col, "");
                                string _SearchText = "";
                                if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 0)
                                {
                                    _SearchText = Convert.ToString(c1BusinessCenter.GetData(e.Row, e.Col));
                                    if (_SearchText != "" && ogloGridListControl != null)
                                    {
                                        ogloGridListControl.AdvanceSearch(_SearchText);
                                    }
                                }
                            }
                            break;
                        case COL_CPTTO:
                            {
                                //e.Cancel = true;
                                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, e.Row, e.Col, "");
                                string _SearchText = "";
                                if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 0)
                                {
                                    _SearchText = Convert.ToString(c1BusinessCenter.GetData(e.Row, e.Col));
                                    if (_SearchText != "" && ogloGridListControl != null)
                                    {
                                        ogloGridListControl.AdvanceSearch(_SearchText);
                                    }
                                }
                            }
                            break;
                        case COL_FACILITYNAME:
                            {
                                //e.Cancel = true;
                                OpenInternalControl(gloGridListControlType.Facility, "Facility", false, e.Row, e.Col, "");
                                string _SearchText = "";
                                if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 0)
                                {
                                    _SearchText = Convert.ToString(c1BusinessCenter.GetData(e.Row, e.Col));
                                    if (_SearchText != "" && ogloGridListControl != null)
                                    {
                                        ogloGridListControl.AdvanceSearch(_SearchText);
                                    }
                                }
                            }
                            break;
                      

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl.SelectedItems.Count > 0)
                {
                    if (c1BusinessCenter.ColSel == COL_BUSINESSCENTERCODE)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel - 1, Convert.ToString(ogloGridListControl.SelectedItems[0].ID));
                    }
                    else if (c1BusinessCenter.ColSel == COL_FACILITYNAME || c1BusinessCenter.ColSel == COL_PROVIDERNAME)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel, Convert.ToString(ogloGridListControl.SelectedItems[0].Description));
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel -1, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                    }
                    else
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel + 1, Convert.ToString(ogloGridListControl.SelectedItems[0].Description));
                    }
                    c1BusinessCenter.Focus();
                    c1BusinessCenter.Select(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel);
                    if (ogloGridListControl != null)
                    {
                        CloseInternalControl();
                    }
                }
                else
                {
                    if (c1BusinessCenter.ColSel == COL_FACILITYNAME || c1BusinessCenter.ColSel == COL_PROVIDERNAME || c1BusinessCenter.ColSel == COL_BUSINESSCENTERCODE)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel - 1, "");
                    }
                    else
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel,"");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel + 1, "");
                    }

                    CloseInternalControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                c1BusinessCenter.Focus();
                c1BusinessCenter.Select(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel);
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

                int _x = c1BusinessCenter.Cols[ColIndex].Left;

                int _y = c1BusinessCenter.Rows[RowIndex].Bottom;

                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                if (ControlType == gloGridListControlType.CPT)
                {
                    pnlInternalControl.Width = 250;
                }
                else
                {
                    pnlInternalControl.Width = c1BusinessCenter.Cols[ColIndex].Width;
                }
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                pnlInternalControl.Focus();
                ogloGridListControl.Select();
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
                RePositionInternalControl();
            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {

                for (int i = 0; i < pnlInternalControl.Controls.Count; i++)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null) { ogloGridListControl.Dispose(); ogloGridListControl = null; }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }

            return _result;
        }

        private void RePositionInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                {
                    if (c1BusinessCenter.Rows[c1BusinessCenter.RowSel].Bottom + c1BusinessCenter.ScrollPosition.Y > 220)
                    {
                        pnlInternalControl.SetBounds((c1BusinessCenter.Cols[c1BusinessCenter.ColSel].Left + c1BusinessCenter.ScrollPosition.X), (c1BusinessCenter.Rows[c1BusinessCenter.RowSel].Bottom + c1BusinessCenter.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    }
                    else
                    {
                        pnlInternalControl.SetBounds((c1BusinessCenter.Cols[c1BusinessCenter.ColSel].Left + c1BusinessCenter.ScrollPosition.X), (c1BusinessCenter.Rows[c1BusinessCenter.RowSel].Bottom + c1BusinessCenter.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_LeaveEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col != COL_PRIORITY)
                {
                    c1BusinessCenter.ChangeEdit -= new System.EventHandler(this.c1BusinessCenter_ChangeEdit);
                    c1BusinessCenter.Editor.Text = "";
                    c1BusinessCenter.ChangeEdit += new System.EventHandler(this.c1BusinessCenter_ChangeEdit);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_ChangeEdit(object sender, EventArgs e)
       {
            string _strSearchString = "";
            try
            {  
                _strSearchString = c1BusinessCenter.Editor.Text;
                if (ogloGridListControl.ControlType == gloGridListControlType.Providers || ogloGridListControl.ControlType == gloGridListControlType.Facility)
                    ogloGridListControl.AdvanceSearch(_strSearchString);
                else
                    ogloGridListControl.FillControl(_strSearchString);
                
                //ogloGridListControl.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_Click(object sender, EventArgs e)
        {
            CloseInternalControl();
        }

        private void c1BusinessCenter_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                RePositionInternalControl();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible == true && e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                    ogloGridListControl.Select();
                    ogloGridListControl.Focus();
                }
                #region "Numeric Validation"
                if (c1BusinessCenter.ColSel == COL_PRIORITY)
                {
                    if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                    {
                        e.Handled = true;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                e.SuppressKeyPress = true;

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;


                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {

                                //If Item is Selected Move to nextcell
                                //MoveNext();

                                //********* Code Commented shifted to ogloGridListControl_ItemSelected
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    CloseInternalControl();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (c1BusinessCenter.ColSel == COL_PROVIDERNAME)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PROVIDERNAME, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PROVIDERID, 0);
                    }
                    else if (c1BusinessCenter.ColSel == COL_FACILITYNAME)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_FACILITYNAME, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_FACILITYID, 0);
                    }
                    else if (c1BusinessCenter.ColSel == COL_CPTFROM)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTFROM, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTFROMDESC, "");
                    }
                    else if (c1BusinessCenter.ColSel == COL_CPTTO)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTTO, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTTODESC, "");
                    }
                    else if (c1BusinessCenter.ColSel == COL_BUSINESSCENTERCODE)
                    {
                        //c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERNAME, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERCODE, "");
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERID, 0);
                    }
                    else if (c1BusinessCenter.ColSel == COL_PRIORITY)
                    {
                        c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PRIORITY, null);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    CloseInternalControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BusinessCenter_AfterEdit(object sender, RowColEventArgs e)
        {
            c1BusinessCenter.FinishEditing();
            try
            {
                if (c1BusinessCenter.ColSel == COL_PROVIDERNAME)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_PROVIDERNAME) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_PROVIDERNAME)) == "")
                        {
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PROVIDERNAME, "");
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PROVIDERID, 0);
                        }
                    }
                }
                else if (c1BusinessCenter.ColSel == COL_FACILITYNAME)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_FACILITYNAME) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_FACILITYNAME)) == "")
                        {
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_FACILITYNAME, "");
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_FACILITYID, 0);
                        }
                    }
                }
                else if (c1BusinessCenter.ColSel == COL_CPTFROM)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_CPTFROM) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_CPTFROM)) == "")
                        {
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTFROM, "");
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTFROMDESC, "");
                        }
                    }
                }
                else if (c1BusinessCenter.ColSel == COL_CPTTO)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_CPTTO) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_CPTTO)) == "")
                        {
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTTO, "");
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_CPTTODESC, "");
                        }
                    }
                }
                else if (c1BusinessCenter.ColSel == COL_BUSINESSCENTERCODE)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERCODE) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERCODE)) == "")
                        {
                            //c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERNAME, "");
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERID, 0);
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_BUSINESSCENTERCODE, "");
                        }
                    }
                }
                else if (e.Col == COL_PRIORITY)
                {
                    if (c1BusinessCenter.GetData(c1BusinessCenter.RowSel, e.Col) != null)
                    {
                        if (Convert.ToString(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, e.Col)) != "")
                        {
                            c1BusinessCenter.SetData(c1BusinessCenter.RowSel, e.Col, Convert.ToInt64(c1BusinessCenter.GetData(c1BusinessCenter.RowSel, e.Col)));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ts_btnAddLine_Click(object sender, EventArgs e)
        {
            c1BusinessCenter.FinishEditing();
            if (c1BusinessCenter.Rows.Count > 0)
            {
                this.c1BusinessCenter.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1BusinessCenter_AfterRowColChange);
                this.c1BusinessCenter.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BusinessCenter_AfterEdit);
                c1BusinessCenter.Select(c1BusinessCenter.RowSel,COL_PROVIDERNAME);
                this.c1BusinessCenter.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BusinessCenter_AfterEdit);
                this.c1BusinessCenter.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1BusinessCenter_AfterRowColChange);
            }

            //if (ValidateForCPT(c1BusinessCenter.RowSel, c1BusinessCenter.ColSel) == true)
            //{
                AddLine();
            //}
        }

        private void tsb_btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (c1BusinessCenter != null && c1BusinessCenter.Rows.Count > 1)
            {
                int rowIndex = c1BusinessCenter.RowSel;
                c1BusinessCenter.Rows.Remove(rowIndex);
                CloseInternalControl();
            }
        }

        private void AddLine()
        {
            try
            {
                if (c1BusinessCenter != null)
                {
                    c1BusinessCenter.Rows.Add();
                    int rowIndex = c1BusinessCenter.Rows.Count - 1;
                    c1BusinessCenter.Select(rowIndex, COL_PROVIDERNAME, true);
                    c1BusinessCenter.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1BusinessCenter_AfterRowColChange(object sender, RangeEventArgs e)
        {
            try
            {

                if ((e.OldRange.c1 == COL_PROVIDERNAME) && (e.NewRange.c1 != COL_PROVIDERNAME))
                    { CloseInternalControl(); }
                if ((e.OldRange.c1 == COL_FACILITYNAME) && (e.NewRange.c1 != COL_FACILITYNAME))
                    { CloseInternalControl(); }
                if ((e.OldRange.c1 == COL_CPTFROM) && (e.NewRange.c1 != COL_CPTFROM))
                    { CloseInternalControl(); }
                if ((e.OldRange.c1 == COL_CPTTO) && (e.NewRange.c1 != COL_CPTTO))
                    { CloseInternalControl(); }
                if ((e.OldRange.c1 == COL_BUSINESSCENTERCODE) && (e.NewRange.c1 != COL_BUSINESSCENTERCODE))
                { CloseInternalControl(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void cbx_BusCenByDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_BusCenByDoctor.Checked)
            {
                c1BusinessCenter.Cols[COL_PROVIDERNAME].Visible = true;
            }
            else
            {
                c1BusinessCenter.Cols[COL_PROVIDERNAME].Visible = false;
            }
        }

        private void cbx_BusCenByFacility_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_BusCenByFacility.Checked)
            {
                c1BusinessCenter.Cols[COL_FACILITYNAME].Visible = true;
            }
            else
            {
                c1BusinessCenter.Cols[COL_FACILITYNAME].Visible = false;
            }
        }

        private void cbx_BusCenByCPT_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_BusCenByCPT.Checked)
            {
                c1BusinessCenter.Cols[COL_CPTFROM].Visible = true;
                c1BusinessCenter.Cols[COL_CPTTO].Visible = true;
            }
            else
            {
                c1BusinessCenter.Cols[COL_CPTFROM].Visible = false;
                c1BusinessCenter.Cols[COL_CPTTO].Visible = false;
            }
        }

        #region " Menu events for shortcut keys"

        private void mnuFeeSchedule_AddLine_Click(object sender, EventArgs e)
        {
            ts_btnAddLine_Click(null, null);

        }

        private void mnuFeeSchedule_RemoveLine_Click(object sender, EventArgs e)
        {
            tsb_btnRemoveLine_Click(null, null);

        }

        private void mnuFeeSchedule_Save_Click(object sender, EventArgs e)
        {
            tsb_Saveclose_Click(null, null);

        }

        private void mnuFeeSchedule_Close_Click(object sender, EventArgs e)
        {
            tsb_close_Click(null, null);

        }
        #endregion

        private void c1BusinessCenter_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1BusinessCenter.ColSel == COL_PRIORITY)
            {
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }

            }
            #endregion
        }

        private void pnlDetails_Leave(object sender, EventArgs e)
        {
            CloseInternalControl();
            cbx_BusCenByDoctor.Focus();
        }

      

        private void c1BusinessCenter_CellChanged(object sender, RowColEventArgs e)
        {
            Reorder();
        }

        private void dtpStartDate_CloseUp(object sender, EventArgs e)
        {
            mskStartDate.Text = dtpStartDate.Value.ToString("MMddyyyy");   
        }

        private void dtpEndDate_CloseUp(object sender, EventArgs e)
        {
            mskEnddate.Text = dtpEndDate.Value.ToString("MMddyyyy");  
        }

    }
}