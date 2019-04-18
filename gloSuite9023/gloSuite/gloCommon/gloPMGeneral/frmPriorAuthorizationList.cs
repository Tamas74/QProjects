using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace gloPMGeneral
{
    public partial class frmPriorAuthorizationList : Form
    {

        #region Declarations
        //Declarations
        private String _connectionString = "";
        private String _messageBoxCaption = "gloPM";


//nAuthorizationID, nPatientID, nInsuranceID, sInsuranceName, dtAuthorization, dtAuthorizationThrough,
//sAuthorizationNumber, nTotalVisits, nVisitsMade, nAppointmentType, sAuthorizationStatus, dtAuthorizationStatus


        private const int  COL_nAuthorizationID = 0 ;
        private const int COL_nInsuranceID = 1;
        private const int COL_sInsuranceName = 2;
        private const int COL_dtAuthorization = 3;
        private const int COL_dtAuthorizationThrough = 4;
        private const int COL_sAuthorizationNumber = 5;
        private const int COL_nTotalVisits = 6;
        private const int COL_nVisitsMade = 7;
        private const int COL_nAppointmentType = 8;
        private const int COL_sAuthorizationStatus = 9;
        private const int COL_dtAuthorizationStatus = 10;



        #endregion Declarations

        #region "Property Procedures"
        //Property Procedures
            private Int64 _PatientID = 0;
            private Int64 _InsuranceId = 0;
            private Int64 _nAuthorizationID = 0;
            private string _sAuthorizationNumber = "";
            private bool _DialogResult = false;
            private int _AuthorizationCount = 0;

            public Int64 PatientID
            {
                set { _PatientID = value; }
                get { return _PatientID; }
            }

            public Int64 InsuranceID
            {
                set { _InsuranceId = value; }
                get { return _InsuranceId; }
            }

            public Int64 AuthorizationID
            {
                set { _nAuthorizationID = value; }
                get { return _nAuthorizationID; }
            }

            public string AuthorizationNumber
            {
                set { _sAuthorizationNumber = value; }
                get { return _sAuthorizationNumber; }
            }

            public bool AuthorizationDialogResult
            {
                set { _DialogResult = value; }
                get { return _DialogResult; }
            }

            public int AuthorizationCount
            {
                set { _AuthorizationCount = value; }
                get { return _AuthorizationCount; }
            }

        #endregion "Property Procedures"

        #region "Constructor"
           
         public frmPriorAuthorizationList()
        {
            InitializeComponent();
        }

         public frmPriorAuthorizationList(Int64 PatientID, Int64 InsuranceId, String ConnectionString)
        {
            InitializeComponent();

            _PatientID = PatientID;
            _InsuranceId = InsuranceId;
            _connectionString = ConnectionString;

            try
            {
                DesignGrid();
                Show_PriorAuthorizations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ////dtTemp.Dispose();
                //oPriorAutorization.Dispose();
            }
            if (c1PriorAuthorization.Rows.Count > 1)
            {
                _AuthorizationCount = c1PriorAuthorization.Rows.Count - 1;
            }
        }

        #endregion "Constructor"


        private void frmPriorAuthorizationList_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1PriorAuthorization, false); 
            
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            PriorAutorization oPriorAuthorization = new PriorAutorization(_connectionString);
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        if (c1PriorAuthorization.Rows.Count > 1)
                        {
                            if (c1PriorAuthorization.Row > 0)
                            {
                                if (c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID) != null && Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID)) != "")
                                {
                                    if (c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber) != null && Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber)) != "")
                                    {
                                        _DialogResult = true;
                                        _nAuthorizationID = Convert.ToInt64(Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID)));
                                        _sAuthorizationNumber = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber));
                                        this.Close();
                                    }
                                }
                            }
                        }
                        
                        break;
                    case "Close":
                        _DialogResult = false;
                        this.Close();
                        break;
                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Show_PriorAuthorizations()
        {
            PriorAutorization oPriorAutorization = new PriorAutorization(_connectionString);
            DataTable dtTemp = new DataTable();
            dtTemp = oPriorAutorization.ViewPriorAuthorization(_PatientID);
            //dtTemp = ViewPriorAuthorization(_PatientID);
            c1PriorAuthorization.Rows.Count = 1;
            try
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {

                    c1PriorAuthorization.Rows.Add();
                    Int32 Index = c1PriorAuthorization.Rows.Count - 1;
                    c1PriorAuthorization.SetData(Index, COL_nVisitsMade, Convert.ToString(dtTemp.Rows[i]["nVisitsMade"]));
                    if (dtTemp.Rows[i]["dtAuthorization"] != null && dtTemp.Rows[i]["dtAuthorization"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorization, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorization"]));
                    }
                    if (dtTemp.Rows[i]["dtAuthorizationThrough"] != null && dtTemp.Rows[i]["dtAuthorizationThrough"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorizationThrough, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorizationThrough"]));
                    }
                    if (dtTemp.Rows[i]["dtAuthorizationStatus"] != null && dtTemp.Rows[i]["dtAuthorizationStatus"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorizationStatus, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorizationStatus"]));
                    }
                    c1PriorAuthorization.SetData(Index, COL_sAuthorizationStatus, Convert.ToString(dtTemp.Rows[i]["sAuthorizationStatus"]));
                    c1PriorAuthorization.SetData(Index, COL_nTotalVisits, Convert.ToString(dtTemp.Rows[i]["nTotalVisits"]));
                    c1PriorAuthorization.SetData(Index, COL_sAuthorizationNumber, Convert.ToString(dtTemp.Rows[i]["sAuthorizationNumber"]));

                    c1PriorAuthorization.SetData(Index, COL_nAppointmentType, Convert.ToString(dtTemp.Rows[i]["nAppointmentType"]));
                    c1PriorAuthorization.SetData(Index, COL_nAuthorizationID, Convert.ToString(dtTemp.Rows[i]["nAuthorizationID"]));
                    c1PriorAuthorization.SetData(Index, COL_sInsuranceName, Convert.ToString(dtTemp.Rows[i]["sInsuranceName"]));
                    c1PriorAuthorization.SetData(Index, COL_nInsuranceID, Convert.ToString(dtTemp.Rows[i]["nInsuranceID"]));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DesignGrid()
        {
            try
            {
                c1PriorAuthorization.Cols.Count = 11;
                c1PriorAuthorization.Rows.Count = 1;
                //nAuthorizationID,  nInsuranceID, sInsuranceName, dtAuthorization, dtAuthorizationThrough,
                //sAuthorizationNumber, nTotalVisits, nVisitsMade, nAppointmentType, sAuthorizationStatus, dtAuthorizationStatus

                c1PriorAuthorization.SetData(0, COL_nAuthorizationID, "nAuthorizationID ");
                c1PriorAuthorization.SetData(0, COL_nInsuranceID, "nInsuranceID");
                c1PriorAuthorization.SetData(0, COL_sInsuranceName, "InsuranceName");
                c1PriorAuthorization.SetData(0, COL_dtAuthorization, "Authorization Date");
                c1PriorAuthorization.SetData(0, COL_dtAuthorizationStatus, "Authorization Status Date");
                c1PriorAuthorization.SetData(0, COL_dtAuthorizationThrough, "Valid Till");
                c1PriorAuthorization.SetData(0, COL_sAuthorizationNumber, "Auth Number");
                c1PriorAuthorization.SetData(0, COL_nTotalVisits, "Total Visits");
                c1PriorAuthorization.SetData(0, COL_nVisitsMade, "Visits Made");
                c1PriorAuthorization.SetData(0, COL_nAppointmentType, " Appointment Type");
                c1PriorAuthorization.SetData(0, COL_sAuthorizationStatus, " Auth Status");


                c1PriorAuthorization.Cols[COL_nAuthorizationID].Visible = false;
                c1PriorAuthorization.Cols[COL_nInsuranceID].Visible = false;
                c1PriorAuthorization.Cols[COL_sInsuranceName].Visible = true;
                c1PriorAuthorization.Cols[COL_dtAuthorization].Visible = false;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].Visible = false;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].Visible = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].Visible = true;
                c1PriorAuthorization.Cols[COL_nTotalVisits].Visible = true;
                c1PriorAuthorization.Cols[COL_nVisitsMade].Visible = true;
                c1PriorAuthorization.Cols[COL_nAppointmentType].Visible = false;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].Visible = true;


                int nWidth = pnl_PriorAuthorizations.Width;
                c1PriorAuthorization.Cols[COL_nAuthorizationID].Width = 0;
                c1PriorAuthorization.Cols[COL_nInsuranceID].Width = 0;
                c1PriorAuthorization.Cols[COL_sInsuranceName].Width = (int)(0.25 * (nWidth));
                c1PriorAuthorization.Cols[COL_dtAuthorization].Width = 0;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].Width = 0;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nTotalVisits].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nVisitsMade].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nAppointmentType].Width = 0;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].Width = (int)(0.15 * (nWidth));

                c1PriorAuthorization.Cols[COL_dtAuthorization].AllowEditing = true; ;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_nTotalVisits].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_nVisitsMade].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].AllowEditing = true;

                c1PriorAuthorization.AllowEditing = false;
                c1PriorAuthorization.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void c1PriorAuthorization_DoubleClick(object sender, EventArgs e)
        {
            if (c1PriorAuthorization.Rows.Count > 1)
            {
                if (c1PriorAuthorization.Row > 1)
                {
                    if (c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID) != null && Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID)) != "")
                    {
                        if (c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber) != null && Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber)) != "")
                        {
                            _DialogResult = true;
                            _nAuthorizationID = Convert.ToInt64(Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_nAuthorizationID)));
                            _sAuthorizationNumber = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.Row, COL_sAuthorizationNumber));
                            this.Close();
                        }
                    }
                }
            }
        }

        private void c1PriorAuthorization_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

    }
}