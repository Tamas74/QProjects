using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmViewAvailableReserveV2 : Form
    {

        #region "Variable declaration"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _sqlDatabaseConnectionString = "";
        private Int64 _nPatientID = 0;
        private Int64 _nPAccountId = 0;        
        private Int64 _nClinicID = 1;
        string _strMessageBoxCaption = string.Empty;
        
        private int iReserveSelRow = 1;
        #endregion

        #region Constructor

        public frmViewAvailableReserveV2(string databaseconnectionstring, Int64 patientid, Int64 clinicid, Int64 nPAccountId)
        {
            InitializeComponent();
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _strMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _strMessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _strMessageBoxCaption = "gloPM"; ; }

            #endregion
            _sqlDatabaseConnectionString = databaseconnectionstring;
            _nPatientID = patientid;
            _nClinicID = clinicid;
            _nPAccountId = nPAccountId;
        }

        #endregion

        private void frmViewAvailableReserveV2_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1FlexGridAvailResrv, false);
            FillPatientReserves();
            c1FlexGridAvailResrv.Focus();
        }

        
        private void FillPatientReserves()
        {

            gloPatientFinancialViewV2 objPatFinacialView = null;
            
            objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            
            DataSet dsReserves = new DataSet();

            try
            {
                #region "Reserves"
                objPatFinacialView.GetPatientReserve(out dsReserves);

                c1FlexGridAvailResrv.DataMember = "Reserves";
                c1FlexGridAvailResrv.DataSource = dsReserves;
                c1FlexGridAvailResrv.ShowCellLabels = false;
                setGridStyle(c1FlexGridAvailResrv, 0, 0, 0);

                if (dsReserves.Tables["Reserves"].Rows.Count > 0)
                {                    
                    c1FlexGridTotalAvailResrv.Visible = true;
                    c1FlexGridTotalAvailResrv.Rows[0].Visible = false;

                    c1FlexGridTotalAvailResrv.DataMember = "TotalReserves";
                    c1FlexGridTotalAvailResrv.DataSource = dsReserves;
                    setGridStyle(c1FlexGridTotalAvailResrv, c1FlexGridTotalAvailResrv.Rows.Count - 1, 11, 25);
                    if (dsReserves.Tables["Reserves"].Rows.Count >= iReserveSelRow)
                    {
                        c1FlexGridAvailResrv.Row = iReserveSelRow;
                    }
                }
                else
                {
                    c1FlexGridTotalAvailResrv.DataMember = "Reserves";
                    c1FlexGridTotalAvailResrv.DataSource = dsReserves;
                    c1FlexGridTotalAvailResrv.Visible = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);                
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }
        
        private void setGridStyle(C1FlexGrid C1Flex, Int32 iRowNumber, Int32 iColNumber, Int32 iColCount)
        {
            int _width = (C1Flex.Width - 15) / 10;
            
            if (C1Flex.Name == "c1FlexGridAvailResrv")
            {
                C1Flex.Cols[3].Width = (int)(_width * (1.3));
                C1Flex.Cols[4].Width = (int)(_width * (0.9));
                C1Flex.Cols[5].Width = (int)(_width * (2.8));
                C1Flex.Cols[6].Width = (int)(_width * (0.9));
                C1Flex.Cols[7].Width = (int)(_width * (0.7));
                C1Flex.Cols[8].Width = (int)(_width * (2.6));
                C1Flex.Cols[9].Width = (int)(_width * (1));
                return;
            }
            C1Flex.Cols[11].Width = (int)(_width * (1.3));
            C1Flex.Cols[12].Width = (int)(_width * (0.9));
            C1Flex.Cols[13].Width = (int)(_width * (2.8));
            C1Flex.Cols[14].Width = (int)(_width * (0.9));
            C1Flex.Cols[15].Width = (int)(_width * (0.7));
            C1Flex.Cols[16].Width = (int)(_width * (2.6));
            C1Flex.Cols[17].Width = (int)(_width * (1));

            CellStyle csSubTotalRow;
            CellStyle csSubCol;
            
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.Blue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }

            try
            {
                if (C1Flex.Styles.Contains("SubCol"))
                {
                    csSubCol = C1Flex.Styles["SubCol"];
                }
                else
                {
                    csSubCol = C1Flex.Styles.Add("SubCol");
                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol.TextEffect = TextEffectEnum.Flat;
                    csSubCol.ForeColor = Color.Maroon;
                }

            }
            catch
            {
                csSubCol = C1Flex.Styles.Add("SubCol");
                csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol.TextEffect = TextEffectEnum.Flat;
                csSubCol.ForeColor = Color.Maroon;
            }
            CellRange subTotalRange;
            subTotalRange = C1Flex.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
            subTotalRange.Style = csSubTotalRow;
            CellRange subCol;
            subCol = C1Flex.GetCellRange(iRowNumber, iColNumber, iRowNumber, iColNumber);
            subCol.Style = csSubCol;
        }
        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void c1FlexGridAvailResrv_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestInfo hitInfo = this.c1FlexGridAvailResrv.HitTest(e.X, e.Y);
            if (hitInfo.Row > 0)
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            else
                C1SuperTooltip1.SetToolTip(c1FlexGridAvailResrv, "");
        }

    }

}

