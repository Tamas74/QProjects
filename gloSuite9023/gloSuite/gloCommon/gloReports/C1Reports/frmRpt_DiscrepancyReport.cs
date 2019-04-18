using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling;

namespace gloReports.C1Reports
{
    public partial class frmRpt_DiscrepancyReport : Form
    {

        #region " Variable Declarations"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _ClinicID = 0;
        private ReportType _SelectedReportType=ReportType.None;

        private static frmRpt_DiscrepancyReport frm;
        private bool blnDisposed;

        enum ReportType
        {
            None = 0,
            Discrepancy = 1,
            ClaimStatus = 2,
            ErrorRemittance = 3,
            ProcessRemittance = 4
        }

        #endregion

        #region " Grid Constants "
        // C1 Grid Column Constants
        const int COL_DIS_DATE = 0;
        const int COL_DIS_CLAIM_NO = 1;
        const int COL_DIS_CPT = 2;
        const int COL_DIS_DX1 = 3;
        const int COL_DIS_DX2 = 4;
        const int COL_DIS_DX3 = 5;
        const int COL_DIS_DX4 = 6;
        const int COL_DIS_M1 = 7;
        const int COL_DIS_M2 = 8;
        const int COL_DIS_CHARGES = 9;
        const int COL_DIS_ALLOWED = 10;
        const int COL_DIS_REM_ALLOWED = 11;
        const int COL_DIS_DIFFRANCE = 12;
        const int COL_DIS_INSURANCE = 13;
        
        const int COL_COLCOUNT = 14;

        #region "Constants for C1 Remittance Claims "

        const int COL_CLAIM_SELECT = 0;
        const int COL_CLAIM_REMITID = 1;
        const int COL_CLAIM_CLAIMNO = 2;
        const int COL_CLAIM_CLAIMSTATUS = 3;
        const int COL_CLAIM_TOTALCLAIMAMOUNT = 4;
        const int COL_CLAIM_CLAIMPAYMENTAMOUNT = 5;
        const int COL_CLAIM_PAYERCONTROLNUMBER = 6;

        const int COL_CLAIM_CHECKNO = 7;
        const int COL_CLAIM_CHECKDATE = 8;

        const int COL_CLAIM_CONTRACTUALOBLIGATION = 9;
        const int COL_CLAIM_CORRECTIONREVERSALS = 10;
        const int COL_CLAIM_OTHERADJUSTMENTS = 11;
        const int COL_CLAIM_PATIENTRESPOSIBILITY = 12;
        const int COL_CLAIM_SUBSCRIBERID = 13;
        const int COL_CLAIM_CLAIMSTARTDATE = 14;
        const int COL_CLAIM_CLAIMENDDATE = 15;
        const int COL_CLAIM_COVERAGEAMOUNT = 16;
        const int COL_CLAIM_DISCOUNTAMOUNT = 17;
        const int COL_CLAIM_PATIENTPAIDAMOUNT = 18;
        const int COL_CLAIM_INTERESTAMOUNT = 19;
        const int COL_CLAIM_TAXAMOUNT = 20;
        const int COL_CLAIM_OTHERCLAIMID = 21;
        const int COL_CLAIM_RENDERINGPROVIDERID = 22;
        const int COL_CLAIM_PROVIDERID = 23;
        const int COL_CLAIM_FISCALDATE = 24;
        const int COL_CLAIM_PROVIDERADJUSTMENT = 25;

        const int COL_CLAIM_TOTALCOINSURANCEAMOUNT = 26;
        const int COL_CLAIM_TOTALDEDUCTIBLEAMOUNT = 27;

        const int COL_CLAIM_COUNT = 28;

        #endregion "Constants for C1 Remittance Claims "



        #endregion " Grid Constants "

        #region " Constructors"


        public frmRpt_DiscrepancyReport(String DataBaseConnectionString)
        {
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
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion

            _databaseconnectionstring = DataBaseConnectionString;
            InitializeComponent();
        }


        public static frmRpt_DiscrepancyReport GetInstance(Int64 PatientID, String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmRpt_DiscrepancyReport(DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #endregion

        #region " Form Events"

        protected override void Dispose(bool disposing)
        {

            if (!(this.blnDisposed))
            {
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }

                }

            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmRpt_DiscrepancyReport()
        {
            Dispose(false);
        }

        private void frmRpt_DiscrepancyReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmRpt_DiscrepancyReport_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1Discrepancy, false);

            try
            {                

                FillTree();

                if (trv_viewRemittance.Nodes.Count > 0) 
                { 
                    trv_viewRemittance.SelectedNode = trv_viewRemittance.Nodes[0];                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        #region " Fill Methods"

        private void FillTree()
        {
            TreeNode oNode;
            try
            {
                trv_viewRemittance.Nodes.Clear();

                oNode = new TreeNode();
                oNode.Text = "Discrepancy Report";
                oNode.Tag = "Discrepancy Report";
                oNode.ImageIndex = 1;
                oNode.SelectedImageIndex = 1;                
                trv_viewRemittance.Nodes.Add(oNode);                
                FillRemittance(oNode);

                oNode = new TreeNode();
                oNode.Text = "Claim Status";
                oNode.Tag = "Claim Status";
                oNode.ImageIndex = 2;
                oNode.SelectedImageIndex = 2;
                trv_viewRemittance.Nodes.Add(oNode);                
                FillClaimStatus(oNode);

                oNode = new TreeNode();
                oNode.Text = "Error Remits";
                oNode.Tag = "Error Remits";
                oNode.ImageIndex = 1;
                oNode.SelectedImageIndex = 1;
                trv_viewRemittance.Nodes.Add(oNode);
                //FillClaimStatus(oNode);

                oNode = new TreeNode();
                oNode.Text = "Payment Processed Remits";
                oNode.Tag = "Payment Processed Remits";
                oNode.ImageIndex = 1;
                oNode.SelectedImageIndex = 1;
                trv_viewRemittance.Nodes.Add(oNode);
                //FillClaimStatus(oNode);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
            }
        }


        //Fill Tree from Remittance Master
        //Table : BL_Transaction_Remittance_MST
        private void FillRemittance(TreeNode oDiscrepancyNode)
        {

            try
            {                
                this.trv_viewRemittance.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trv_viewRemittance_AfterSelect);
                
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                TreeNode oNode = new TreeNode();
                DataTable dtRemittance = new DataTable();
                oDB.Connect(false);
                string strSQL = "";
                strSQL = "select distinct nRemitID,sPayerName from BL_Transaction_Remittance_MST";
                oDB.Retrive_Query(strSQL, out dtRemittance);
                oDB.Disconnect();
                oDB.Dispose();


               // bool isnodePresent = false;

                if (dtRemittance != null)
                {
                    //Dispay Payer Name in Tree Node
                    for (int i = 0; i < dtRemittance.Rows.Count; i++)
                    {
                        oNode = new TreeNode();
                        oNode.Text = Convert.ToString(dtRemittance.Rows[i]["sPayerName"]);
                        oNode.Tag = dtRemittance.Rows[i]["nRemitID"];
                        oNode.ImageIndex = 0;
                        oNode.SelectedImageIndex = 0;

                        oDiscrepancyNode.Nodes.Add(oNode);                        
                    }

                    trv_viewRemittance.ExpandAll();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                this.trv_viewRemittance.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_viewRemittance_AfterSelect);
            }


        }

        //Fill Tree from Claim Status
        private void FillClaimStatus(TreeNode dtClaimstatus)
        {
            TreeNode oNode;
            try
            {
                this.trv_viewRemittance.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trv_viewRemittance_AfterSelect);                
                oNode = new TreeNode();
                oNode.Text = "Rebill";
                oNode.Tag = "1";
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 0;
                dtClaimstatus.Nodes.Add(oNode);
                

                oNode = new TreeNode();
                oNode.Text = "Rejected";
                oNode.Tag = "2";
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 0;
                dtClaimstatus.Nodes.Add(oNode);

                oNode = new TreeNode();
                oNode.Text = "Followup";
                oNode.Tag = "3";
                oNode.ImageIndex = 0;
                oNode.SelectedImageIndex = 0;
                dtClaimstatus.Nodes.Add(oNode);

                dtClaimstatus.Collapse();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                this.trv_viewRemittance.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_viewRemittance_AfterSelect);
            }
        }

        
        //Fill C1 Grid with Remittance Details
        //Table : BL_Transaction_Remittance_ClaimLines, BL_Transaction_Lines
        private void FillDiscrepancyGrid(Int64 _remitID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt;
            try
            {
                oDB.Connect(false);

                //Get Remittance Details from BL_Transaction_Remittance_ClaimLines, BL_Transaction_Lines Tables
                String SqlQuery = "SELECT BL_Transaction_Remittance_ClaimLines.sServiceDate, BL_Transaction_Remittance_ClaimLines.sClaimNumber, " +
                                 " BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx2Code,  " +
                                 " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sMod1Code,  " +
                                 " BL_Transaction_Lines.sMod2Code, BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dAllowed,  " +
                                 " CONVERT(NUMERIC(18,2), CASE WHEN RTRIM(LTRIM(isnull(BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount,'0'))) = '' THEN '0' ELSE BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount END ) as Sanction,  " +
                                 " (CONVERT(NUMERIC(18,2), isnull(BL_Transaction_Lines.dAllowed,0)) - CONVERT(NUMERIC(18,2), CASE WHEN RTRIM(LTRIM(isnull(BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount,'0'))) = '' THEN '0' ELSE BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount END )) AS Diffrance  " +
                                 " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_Remittance_ClaimLines ON  " +
                                 " BL_Transaction_Lines.sCPTCode = BL_Transaction_Remittance_ClaimLines.sServiceProcedureCode AND  " +
                                 " BL_Transaction_Lines.sMod1Code = BL_Transaction_Remittance_ClaimLines.sModifier1 AND  " +
                                 " BL_Transaction_Lines.sMod2Code = BL_Transaction_Remittance_ClaimLines.sModifier2 AND  " +
                                 " BL_Transaction_Lines.nClaimNumber = CONVERT(NUMERIC,BL_Transaction_Remittance_ClaimLines.sClaimNumber) " +
                                 " WHERE CONVERT(NUMERIC(18,2), isnull(BL_Transaction_Lines.dAllowed,0)) <> CONVERT(NUMERIC(18,2), CASE WHEN RTRIM(LTRIM(isnull(BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount,'0'))) = '' THEN '0' ELSE BL_Transaction_Remittance_ClaimLines.sLineAllowedAmount END) " +
                                 " and BL_Transaction_Remittance_ClaimLines.nRemitID = " + _remitID + "";

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    //Add Rows in C1 Grid
                    foreach (DataRow dr in dt.Rows)
                    {
                        c1Discrepancy.Rows.Add();
                        int rowIndex = c1Discrepancy.Rows.Count - 1;
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DATE, Convert.ToDateTime(dr["sServiceDate"]));//Select-CheckBox
                        c1Discrepancy.SetData(rowIndex, COL_DIS_CLAIM_NO, Convert.ToString(dr["sClaimNumber"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_CPT, Convert.ToString(dr["sCPTCode"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DX1, Convert.ToString(dr["sDx1Code"]));
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DX2, Convert.ToString(dr["sDx2Code"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DX3, Convert.ToString(dr["sDx3Code"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DX4, Convert.ToString(dr["sDx4Code"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_M1, Convert.ToString(dr["sMod1Code"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_M2, Convert.ToString(dr["sMod2Code"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_CHARGES, Convert.ToString(dr["dCharges"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_ALLOWED, Convert.ToString(dr["dAllowed"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_REM_ALLOWED, Convert.ToString(dr["Sanction"])); //
                        c1Discrepancy.SetData(rowIndex, COL_DIS_DIFFRANCE, Convert.ToString(dr["Diffrance"])); //                        
                    }
                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        #endregion

        #region " ToolStrip Button Click Event "

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion " ToolStrip Button Click Event "

        #region " C1 Flex Grid"

        //Design C1 Grid To Display Remittance Details 
        private void DesignGrid(ReportType _reporttype,Int64 _value)
        {
            c1Discrepancy.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
           // c1Discrepancy.Clear();
            c1Discrepancy.DataSource = null;

            C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Discrepancy.Styles.Add("cs_CurrencyStyle");
            try
            {
                if (c1Discrepancy.Styles.Contains("cs_CurrencyStyle"))
                {
                    csCurrencyStyle = c1Discrepancy.Styles["cs_CurrencyStyle"];
                }
                else
                {
                    csCurrencyStyle = c1Discrepancy.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }

            }
            catch
            {
                csCurrencyStyle = c1Discrepancy.Styles.Add("cs_CurrencyStyle");
                csCurrencyStyle.DataType = typeof(System.Decimal);
                csCurrencyStyle.Format = "c";
                csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

            }
         


            switch (_reporttype)
            {
                case ReportType.Discrepancy :
                    #region " Discrepancy "
                    //C1 Grid Column Header
                    c1Discrepancy.Cols.Count = COL_COLCOUNT;
                    c1Discrepancy.Rows.Count = 1;
                    c1Discrepancy.Rows.Fixed = 1;

                    c1Discrepancy.SetData(0, COL_DIS_DATE, "Service Date");
                    c1Discrepancy.SetData(0, COL_DIS_CLAIM_NO, "Claim");
                    c1Discrepancy.SetData(0, COL_DIS_CPT, "CPT");
                    c1Discrepancy.SetData(0, COL_DIS_DX1, "DX1");
                    c1Discrepancy.SetData(0, COL_DIS_DX2, "DX2");
                    c1Discrepancy.SetData(0, COL_DIS_DX3, "DX3");
                    c1Discrepancy.SetData(0, COL_DIS_DX4, "DX4");
                    c1Discrepancy.SetData(0, COL_DIS_M1, "M1");
                    c1Discrepancy.SetData(0, COL_DIS_M2, "M2");
                    c1Discrepancy.SetData(0, COL_DIS_CHARGES, "Charges");
                    c1Discrepancy.SetData(0, COL_DIS_ALLOWED, "Allowed");
                    c1Discrepancy.SetData(0, COL_DIS_REM_ALLOWED, "Rmt Allowed");
                    c1Discrepancy.SetData(0, COL_DIS_DIFFRANCE, "Discrepancy");
                    c1Discrepancy.SetData(0, COL_DIS_INSURANCE, "Insurance");

                    c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                    //Set C1 Grid Column Width
                    int width = pnl_View.Width - 1;
                    if (width < 1051)
                        width = 1051;
                    c1Discrepancy.Cols[COL_DIS_DATE].Width = (int)(width * 0.09);
                    c1Discrepancy.Cols[COL_DIS_CLAIM_NO].Width = (int)(width * 0.08);
                    c1Discrepancy.Cols[COL_DIS_CPT].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_DX1].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_DX2].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_DX3].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_DX4].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_M1].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_M2].Width = (int)(width * 0.05);
                    c1Discrepancy.Cols[COL_DIS_CHARGES].Width = (int)(width * 0.1);
                    c1Discrepancy.Cols[COL_DIS_ALLOWED].Width = (int)(width * 0.1);
                    c1Discrepancy.Cols[COL_DIS_REM_ALLOWED].Width = (int)(width * 0.1);
                    c1Discrepancy.Cols[COL_DIS_DIFFRANCE].Width = (int)(width * 0.1);
                    c1Discrepancy.Cols[COL_DIS_INSURANCE].Width = (int)(width * 0.1);

                    c1Discrepancy.Cols[COL_DIS_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_M1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_M2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1Discrepancy.Cols[COL_DIS_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    c1Discrepancy.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;                    
                    
                    c1Discrepancy.Cols[COL_DIS_CHARGES].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_DIS_ALLOWED].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_DIS_REM_ALLOWED].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_DIS_DIFFRANCE].Style = csCurrencyStyle;

                    FillDiscrepancyGrid(_value);

                    #endregion
                    break;

                case ReportType.ClaimStatus:
                    #region " Cliam Status"

                    ShowClaimStatus(_value);
                    
                    #endregion
                    break;
                case ReportType.ErrorRemittance:
                    #region " ErrorRemittance "
                    c1Discrepancy.Cols.Count = COL_CLAIM_COUNT;
                    c1Discrepancy.Rows.Count = 1;
                    c1Discrepancy.SetData(0, COL_CLAIM_SELECT, "  ");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMNO, "Claim No");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMSTATUS, "Claim Status");
                    c1Discrepancy.SetData(0, COL_CLAIM_TOTALCLAIMAMOUNT, "Total Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMPAYMENTAMOUNT, "Claim Payment");
                    c1Discrepancy.SetData(0, COL_CLAIM_PAYERCONTROLNUMBER, "Payer Control No");
                    c1Discrepancy.SetData(0, COL_CLAIM_CONTRACTUALOBLIGATION, "Contractual Obligation");
                    c1Discrepancy.SetData(0, COL_CLAIM_CORRECTIONREVERSALS, "Correction Reversal");
                    c1Discrepancy.SetData(0, COL_CLAIM_OTHERADJUSTMENTS, "Other Adjustments");
                    c1Discrepancy.SetData(0, COL_CLAIM_PATIENTRESPOSIBILITY, "Patient Responsibility");
                    c1Discrepancy.SetData(0, COL_CLAIM_SUBSCRIBERID, "Insurance ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMSTARTDATE, "Claim Start Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMENDDATE, "Claim End Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_COVERAGEAMOUNT, "Coverage Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_DISCOUNTAMOUNT, "Discount Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_PATIENTPAIDAMOUNT, "Patient Paid Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_INTERESTAMOUNT, "Interest Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_TAXAMOUNT, "Tax Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_OTHERCLAIMID, "Other Claim ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_RENDERINGPROVIDERID, "Rendering Provider ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_PROVIDERID, "Provider ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_FISCALDATE, "Fiscal Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_PROVIDERADJUSTMENT, "Provider Adjustments");

                    c1Discrepancy.Cols[COL_CLAIM_SELECT].DataType = System.Type.GetType("System.Boolean");
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].AllowEditing = true;
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Width = 0; //Convert.ToInt32(nWidth * 0.08);
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Visible = false;

                    c1Discrepancy.Cols[COL_CLAIM_REMITID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].Width = 90;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTATUS].Width = 350;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_PAYERCONTROLNUMBER].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Width = 145;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Width = 130;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Width = 125;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Width = 140;
                    c1Discrepancy.Cols[COL_CLAIM_SUBSCRIBERID].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTARTDATE].Width = 106;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMENDDATE].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Width = 115;
                    c1Discrepancy.Cols[COL_CLAIM_DISCOUNTAMOUNT].Width = 115;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Width = 135;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Width = 110;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERCLAIMID].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_RENDERINGPROVIDERID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_FISCALDATE].Width = 80;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Width = 140;


                    //--------End

                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_REMITID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTATUS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PAYERCONTROLNUMBER].Visible = true;

                    c1Discrepancy.Cols[COL_CLAIM_CHECKNO].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_CHECKDATE].Visible = false;
                    
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_SUBSCRIBERID].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTARTDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMENDDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_DISCOUNTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERCLAIMID].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_RENDERINGPROVIDERID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_FISCALDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Visible = false;



                    c1Discrepancy.Cols[COL_CLAIM_TOTALCOINSURANCEAMOUNT].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALDEDUCTIBLEAMOUNT].Visible = false;
                    

                    c1Discrepancy.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    //c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;

                    
                    //Added By Pramod Nair 20090828
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].TextAlign =C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    //End

                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Style = csCurrencyStyle;

                    FillClaimsGrid();
                    #endregion
                    break;
                case ReportType.ProcessRemittance:
                    #region " Process Remittance "
                    c1Discrepancy.Cols.Count = COL_CLAIM_COUNT;
                    c1Discrepancy.Rows.Count = 1;
                    c1Discrepancy.SetData(0, COL_CLAIM_SELECT, "  ");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMNO, "Claim No");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMSTATUS, "Claim Status");
                    c1Discrepancy.SetData(0, COL_CLAIM_TOTALCLAIMAMOUNT, "Total Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMPAYMENTAMOUNT, "Claim Payment");
                    c1Discrepancy.SetData(0, COL_CLAIM_PAYERCONTROLNUMBER, "Payer Control No");

                    c1Discrepancy.Cols[COL_CLAIM_CHECKNO].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_CHECKDATE].Visible = false;

                    c1Discrepancy.SetData(0, COL_CLAIM_CONTRACTUALOBLIGATION, "Contractual Obligation");
                    c1Discrepancy.SetData(0, COL_CLAIM_CORRECTIONREVERSALS, "Correction Reversal");
                    c1Discrepancy.SetData(0, COL_CLAIM_OTHERADJUSTMENTS, "Other Adjustments");
                    c1Discrepancy.SetData(0, COL_CLAIM_PATIENTRESPOSIBILITY, "Patient Responsibility");
                    c1Discrepancy.SetData(0, COL_CLAIM_SUBSCRIBERID, "Insurance ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMSTARTDATE, "Claim Start Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_CLAIMENDDATE, "Claim End Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_COVERAGEAMOUNT, "Coverage Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_DISCOUNTAMOUNT, "Discount Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_PATIENTPAIDAMOUNT, "Patient Paid Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_INTERESTAMOUNT, "Interest Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_TAXAMOUNT, "Tax Amount");
                    c1Discrepancy.SetData(0, COL_CLAIM_OTHERCLAIMID, "Other Claim ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_RENDERINGPROVIDERID, "Rendering Provider ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_PROVIDERID, "Provider ID");
                    c1Discrepancy.SetData(0, COL_CLAIM_FISCALDATE, "Fiscal Date");
                    c1Discrepancy.SetData(0, COL_CLAIM_PROVIDERADJUSTMENT, "Provider Adjustments");

                    c1Discrepancy.Cols[COL_CLAIM_SELECT].DataType = System.Type.GetType("System.Boolean");
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].AllowEditing = true;
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Width = 0; //Convert.ToInt32(nWidth * 0.08);
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Visible = false;

                    c1Discrepancy.Cols[COL_CLAIM_REMITID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].Width = 90;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTATUS].Width = 350;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_PAYERCONTROLNUMBER].Width = 120;
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Width = 145;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Width = 130;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Width = 125;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Width = 140;
                    c1Discrepancy.Cols[COL_CLAIM_SUBSCRIBERID].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTARTDATE].Width = 106;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMENDDATE].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Width = 115;
                    c1Discrepancy.Cols[COL_CLAIM_DISCOUNTAMOUNT].Width = 115;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Width = 135;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Width = 110;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERCLAIMID].Width = 100;
                    c1Discrepancy.Cols[COL_CLAIM_RENDERINGPROVIDERID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERID].Width = 0;
                    c1Discrepancy.Cols[COL_CLAIM_FISCALDATE].Width = 80;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Width = 140;


                    //--------End

                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_REMITID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTATUS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PAYERCONTROLNUMBER].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_SUBSCRIBERID].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMSTARTDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMENDDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_DISCOUNTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERCLAIMID].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_RENDERINGPROVIDERID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERID].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_FISCALDATE].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Visible = true;
                    c1Discrepancy.Cols[COL_CLAIM_SELECT].Visible = false;

                    c1Discrepancy.Cols[COL_CLAIM_TOTALCOINSURANCEAMOUNT].Visible = false;
                    c1Discrepancy.Cols[COL_CLAIM_TOTALDEDUCTIBLEAMOUNT].Visible = false;

                    c1Discrepancy.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    //c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;


                    //Added By Pramod Nair 20090828
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMPAYMENTAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    //End

                    c1Discrepancy.Cols[COL_CLAIM_TOTALCLAIMAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CONTRACTUALOBLIGATION].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_CORRECTIONREVERSALS].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_OTHERADJUSTMENTS].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTRESPOSIBILITY].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_COVERAGEAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PATIENTPAIDAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_INTERESTAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_TAXAMOUNT].Style = csCurrencyStyle;
                    c1Discrepancy.Cols[COL_CLAIM_PROVIDERADJUSTMENT].Style = csCurrencyStyle;

                    FillClaimsGrid();
                    #endregion
                    break;

            }

          


            c1Discrepancy.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

            
        }       
        
        #endregion

        #region "Tree Control Events"
        
        private void trv_viewRemittance_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {                
                if (trv_viewRemittance.SelectedNode != null && trv_viewRemittance.SelectedNode.Tag.ToString().Trim() != "")
                {
                    //Fill C1 grid with Selected Remittance Details
                    if (trv_viewRemittance.SelectedNode.Parent != null)
                    {                        
                        switch (trv_viewRemittance.SelectedNode.Parent.Tag.ToString().Trim())
                        {
                            case "Discrepancy Report":
                                _SelectedReportType = ReportType.Discrepancy;
                                DesignGrid(ReportType.Discrepancy, Convert.ToInt64(trv_viewRemittance.SelectedNode.Tag));
                                break;
                            case "Claim Status":
                                _SelectedReportType = ReportType.ClaimStatus;
                                DesignGrid(ReportType.ClaimStatus, Convert.ToInt64(trv_viewRemittance.SelectedNode.Tag));
                                break;
                        }
                    }
                    else
                    {
                        _SelectedReportType = ReportType.None;
                        c1Discrepancy.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                        c1Discrepancy.Clear();
                        c1Discrepancy.DataSource = null;
                        c1Discrepancy.Rows.Count = 1;
                        c1Discrepancy.Cols.Count = 1;
                        c1Discrepancy.Cols[0].Visible = false;
                        foreach (TreeNode oNode in trv_viewRemittance.Nodes)
                        {
                            if (oNode.Level == 0)
                            {
                                if (oNode.Tag == trv_viewRemittance.SelectedNode.Tag)
                                { oNode.Expand(); }
                                else
                                { oNode.Collapse();  }
                            }
                        }
                        switch (trv_viewRemittance.SelectedNode.Tag.ToString().Trim())
                        {
                            case "Discrepancy Report":
                                _SelectedReportType = ReportType.Discrepancy;
                                DesignGrid(ReportType.Discrepancy, 0);
                                break;
                            case "Claim Status":
                                _SelectedReportType = ReportType.ClaimStatus;
                                DesignGrid(ReportType.ClaimStatus, -1);
                                break;
                            case "Error Remits":
                                _SelectedReportType = ReportType.ErrorRemittance;
                                DesignGrid(ReportType.ErrorRemittance,0);
                                break;
                            case "Payment Processed Remits":
                                _SelectedReportType = ReportType.ProcessRemittance;
                                DesignGrid(ReportType.ProcessRemittance, 0);
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        
        #endregion

        #region " Claim Status "
        
        private DataTable GetClaimStatus(Int64 ClaimStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {

                //if (ClaimStatus < 0)
                //{
                //    return null;
                //}

                oDB.Connect(false);
                strSQL = "SELECT  DISTINCT  "
                + " case len(BL_Transaction_MST.nClaimNo)   "
                + " when 5 then convert(varchar, BL_Transaction_MST.nClaimNo)     "
                + " when 4 then '0' + convert(varchar,BL_Transaction_MST.nClaimNo)  "
                + " when 3 then '00' + convert(varchar,BL_Transaction_MST.nClaimNo)   "
                + " when 2 then '000' + convert(varchar,BL_Transaction_MST.nClaimNo)    "
                + " when 1 then convert(varchar, '0000' + convert(varchar,BL_Transaction_MST.nClaimNo))     "
                + " end AS Claim ,    "
                + " BL_Transaction_MST.nClaimNo AS ClaimNo ,    "
                + " BL_Transaction_MST.nTransactionID, "
                + " BL_Transaction_MST.nTransactionDate, "
                + " CONVERT(VARCHAR,CONVERT(DateTime,CONVERT(VARCHAR,BL_Transaction_MST.nTransactionDate),101),101) AS Date,     "
                + " BL_Transaction_MST.nPatientID,Patient.sPatientCode as Code,     "
                + " ISNULL(Patient.sFirstName,'')+SPACE(1)+ISNULL(Patient.sMiddleName,'')+SPACE(1)+ISNULL(Patient.sLastName,'') AS PatientName,     "
                + " ISNULL(Patient.sFirstName,'') AS FirstName,     "
                + " ISNULL(Patient.sMiddleName,'') AS MI, "
                + " ISNULL(Patient.sLastName,'') AS LastName,     "
                + " ISNULL(Patient.nSSN,'') AS nPatientSSN,     "
                + " BL_Transaction_MST.nTransactionProviderID,    "
                + " ISNULL(Provider_MST.sFirstName,'')+SPACE(1)+ISNULL(Provider_MST.sMiddleName,'')+SPACE(1)+ISNULL(Provider_MST.sLastName,'') AS ProviderName,     "
                + " ISNULL(Provider_MST.sFirstName,'') AS ProviderFName,     "
                + " ISNULL(Provider_MST.sMiddleName,'') AS ProviderMName, "
                + " ISNULL(Provider_MST.sLastName,'') AS ProviderLName,     "
                + " ISNULL(BL_Transaction_MST.sFacilityCode,'') AS sFacilityCode,     "
                + " ISNULL(BL_Transaction_MST.sFacilityDescription,'') AS Facility,     "
                + " ISNULL(BL_Transaction_MST.nSendCounter,0) AS nSendCounter,     "
                + " ISNULL(BL_Transaction_MST.nSendToRejection,0) AS nSendToRejection,     "
                + " ISNULL(BL_Transaction_MST.nLastStatusId,0) AS nLastStatusId,    0 AS nSendToInsuranceID,    0 AS nClaimSendType,     "
                + " 0 AS nBatchID,  "
                + " (    	 "
                + " 	SELECT  DISTINCT TOP 1 ISNULL(PatientInsurance_DTL.sSubscriberID,'') AS InsuranceID FROM BL_Transaction_MST_Ins INNER JOIN PatientInsurance_DTL ON BL_Transaction_MST_Ins.nInsuranceID = PatientInsurance_DTL.nInsuranceID     	WHERE (BL_Transaction_MST_Ins.nTransactionID = BL_Transaction_MST.nTransactionID)  AND (PatientInsurance_DTL.nPatientID = BL_Transaction_MST.nPatientID) AND (BL_Transaction_MST_Ins.nClinicID =  1 )     		 "
                + " ) AS InsuranceID,     "
                + " (   SELECT  DISTINCT TOP 1 ISNULL(PatientInsurance_DTL.nInsuranceID, 0) AS PatientInsuranceID FROM BL_Transaction_MST_Ins INNER JOIN PatientInsurance_DTL ON BL_Transaction_MST_Ins.nInsuranceID = PatientInsurance_DTL.nInsuranceID    WHERE (BL_Transaction_MST_Ins.nTransactionID = BL_Transaction_MST.nTransactionID)  AND (PatientInsurance_DTL.nPatientID = BL_Transaction_MST.nPatientID) AND (BL_Transaction_MST_Ins.nClinicID =  1 )     	)  "
                + " AS PatientInsuranceID,    	 "
                + " (    	SELECT  DISTINCT TOP 1 ISNULL(PatientInsurance_DTL.sInsuranceName,'') AS LineInsuranceName FROM BL_Transaction_MST_Ins INNER JOIN PatientInsurance_DTL ON BL_Transaction_MST_Ins.nInsuranceID = PatientInsurance_DTL.nInsuranceID     	WHERE (BL_Transaction_MST_Ins.nTransactionID = BL_Transaction_MST.nTransactionID)  AND (PatientInsurance_DTL.nPatientID = BL_Transaction_MST.nPatientID) AND (BL_Transaction_MST_Ins.nClinicID =  1 )     	) "
                + " AS InsuranceName,    	 "
                + " (    	SELECT  DISTINCT TOP 1 ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) AS LineInsuranceFlag FROM BL_Transaction_MST_Ins INNER JOIN PatientInsurance_DTL ON BL_Transaction_MST_Ins.nInsuranceID = PatientInsurance_DTL.nInsuranceID     	WHERE (BL_Transaction_MST_Ins.nTransactionID = BL_Transaction_MST.nTransactionID)  AND (PatientInsurance_DTL.nPatientID = BL_Transaction_MST.nPatientID) AND (BL_Transaction_MST_Ins.nClinicID =  1 )     	)  "
                + " AS InsuranceFlag,     "
                + " ISNULL(BL_Transaction_MST.nTransactionStatusID,0) AS TransactionStatusId,    	  "
                + " case ISNULL(BL_Transaction_MST.nTransactionStatusID,0)    	 when 0 then 'None' 	 when 1 then 'Transacted' 	 when 2 then 'Queue' 	 when 3 then 'Batch' 	 when 4 then 'Send' 	 when 5 then 'Rejected' 	 when 6 then 'Accepted' 	 when 7 then 'ReQueue' 	 when 8 then 'ReBatch' 	 when 9 then 'ReSend' 	 when 10 then 'FullyPaid' 	 when 11 then 'PartialPaid' 	 when 12 then 'Hold ' 	 when 13 then 'Challenge' 	 when 14 then 'Alert' 	 when 15 then 'Pending' 	 when 16 then 'SendToClaimManager' 	 when 17 then 'SendToClearingHouse' 	 "
                + " end AS Status,  	  "
                + " ISNULL(BL_Transaction_Lines.nClaimLineStatusID,0) AS nClaimLineStatusID"
                + " FROM BL_Transaction_MST INNER JOIN "
                + " Provider_MST ON BL_Transaction_MST.nTransactionProviderID = Provider_MST.nProviderID INNER JOIN "
                + " BL_Transaction_Lines ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID LEFT OUTER JOIN "
                + " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID "
                + " WHERE     (BL_Transaction_MST.nClinicID = 1) AND (BL_Transaction_Lines.nClaimLineStatusID = " + ClaimStatus + ") "
                + " ORDER BY nTransactionDate DESC ";

                oDB.Retrive_Query(strSQL, out dt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dt;
        }

        private void ShowClaimStatus(Int64 ClaimStatus)
        {
            try
            {
                DataTable dtClaimStatus = new DataTable();
                dtClaimStatus = GetClaimStatus(ClaimStatus);
               // c1Discrepancy.Clear();
                c1Discrepancy.DataSource = null;
                if (dtClaimStatus != null)
                {
                    c1Discrepancy.DataSource = dtClaimStatus;

                    //Claim
                    //ClaimNo
                    //nTransactionID
                    //nTransactionDate
                    //Date
                    //nPatientID
                    //Code
                    //PatientName
                    //FirstName
                    //MI
                    //LastName
                    //nPatientSSN
                    //nTransactionProviderID
                    //ProviderName
                    //ProviderFName
                    //ProviderMName
                    //ProviderLName
                    //sFacilityCode
                    //Facility
                    //nSendCounter
                    //nSendToRejection
                    //nLastStatusId
                    //nSendToInsuranceID
                    //nClaimSendType
                    //nBatchID
                    //InsuranceID
                    //PatientInsuranceID
                    //InsuranceName
                    //InsuranceFlag
                    //TransactionStatusId
                    //Status

                    c1Discrepancy.Cols["Claim"].Visible = true;
                    c1Discrepancy.Cols["ClaimNo"].Visible = false;
                    c1Discrepancy.Cols["nTransactionID"].Visible = false;
                    c1Discrepancy.Cols["nTransactionDate"].Visible = false;
                    c1Discrepancy.Cols["Date"].Visible = true;
                    c1Discrepancy.Cols["nPatientID"].Visible = false;
                    c1Discrepancy.Cols["Code"].Visible = true;
                    c1Discrepancy.Cols["PatientName"].Visible = false;
                    c1Discrepancy.Cols["FirstName"].Visible = true;
                    c1Discrepancy.Cols["MI"].Visible = true;
                    c1Discrepancy.Cols["LastName"].Visible = true;
                    c1Discrepancy.Cols["nPatientSSN"].Visible = false;
                    c1Discrepancy.Cols["nTransactionProviderID"].Visible = false;
                    c1Discrepancy.Cols["ProviderName"].Visible = false;
                    c1Discrepancy.Cols["ProviderFName"].Visible = true;
                    c1Discrepancy.Cols["ProviderMName"].Visible = true;
                    c1Discrepancy.Cols["ProviderLName"].Visible = true;
                    c1Discrepancy.Cols["sFacilityCode"].Visible = false;
                    c1Discrepancy.Cols["Facility"].Visible = true;
                    c1Discrepancy.Cols["nSendCounter"].Visible = false;
                    c1Discrepancy.Cols["nSendToRejection"].Visible = false;
                    c1Discrepancy.Cols["nLastStatusId"].Visible = false;
                    c1Discrepancy.Cols["nSendToInsuranceID"].Visible = false;
                    c1Discrepancy.Cols["nClaimSendType"].Visible = false;
                    c1Discrepancy.Cols["nBatchID"].Visible = false;
                    c1Discrepancy.Cols["InsuranceID"].Visible = true;
                    c1Discrepancy.Cols["PatientInsuranceID"].Visible = false;
                    c1Discrepancy.Cols["InsuranceName"].Visible = true;
                    c1Discrepancy.Cols["InsuranceFlag"].Visible = false;
                    c1Discrepancy.Cols["TransactionStatusId"].Visible = false;
                    c1Discrepancy.Cols["Status"].Visible = false;
                    c1Discrepancy.Cols["nClaimLineStatusID"].Visible = false;

                    c1Discrepancy.Cols["Claim"].Caption = "Claim";
                    c1Discrepancy.Cols["Date"].Caption = "Date";
                    c1Discrepancy.Cols["Code"].Caption = "Code";
                    c1Discrepancy.Cols["FirstName"].Caption = "First";
                    c1Discrepancy.Cols["MI"].Caption = "MI";
                    c1Discrepancy.Cols["LastName"].Caption = "Last";
                    c1Discrepancy.Cols["ProviderFName"].Caption = "Provider First";
                    c1Discrepancy.Cols["ProviderMName"].Caption = "MI";
                    c1Discrepancy.Cols["ProviderLName"].Caption = "Last";
                    c1Discrepancy.Cols["Facility"].Caption = "Facility";
                    c1Discrepancy.Cols["InsuranceID"].Caption = "Insurance ID";
                    c1Discrepancy.Cols["InsuranceName"].Caption = "Insurance Name";


                    c1Discrepancy.Cols["Claim"].Width = 60;
                    c1Discrepancy.Cols["Date"].Width = 80;
                    c1Discrepancy.Cols["Code"].Width = 80;
                    c1Discrepancy.Cols["FirstName"].Width = 110;
                    c1Discrepancy.Cols["MI"].Width = 30;
                    c1Discrepancy.Cols["LastName"].Width = 110;
                    c1Discrepancy.Cols["ProviderFName"].Width = 110;
                    c1Discrepancy.Cols["ProviderMName"].Width = 30;
                    c1Discrepancy.Cols["ProviderLName"].Width = 110;
                    c1Discrepancy.Cols["Facility"].Width = 150;
                    c1Discrepancy.Cols["InsuranceID"].Width = 120;
                    c1Discrepancy.Cols["InsuranceName"].Width = 250;
                    //c1Discrepancy.Cols["Status"].Width = 150;
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

        private void c1Discrepancy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_SelectedReportType == ReportType.ClaimStatus)
            {
                try
                {
                    if (c1Discrepancy != null && c1Discrepancy.Rows.Count > 1)
                    {
                        if (c1Discrepancy.GetData(c1Discrepancy.RowSel, c1Discrepancy.Cols["nClaimLineStatusID"].Index) != null
                            && Convert.ToString(c1Discrepancy.GetData(c1Discrepancy.RowSel, c1Discrepancy.Cols["nClaimLineStatusID"].Index)) != ""
                            && Convert.ToInt32(c1Discrepancy.GetData(c1Discrepancy.RowSel, c1Discrepancy.Cols["nClaimLineStatusID"].Index)) != CliamLineUserStatus.None.GetHashCode()
                            )
                        {
                            Int64 _transactionId = 0;
                            Int64 _patientId = 0;
                            bool _isTransactionOpen = false;
                            string _recordMachineId = "";
                            Int64 _recordUserId = 0;


                            _transactionId = Convert.ToInt64(c1Discrepancy.GetData(c1Discrepancy.RowSel, c1Discrepancy.Cols["nTransactionID"].Index));
                            _patientId = Convert.ToInt64(c1Discrepancy.GetData(c1Discrepancy.RowSel, c1Discrepancy.Cols["nPatientID"].Index));

                            if (_transactionId > 0 && _patientId > 0)
                            {
                                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
                                _isTransactionOpen = ogloBilling.IsRecordOpen(_transactionId, out _recordMachineId, out _recordUserId);
                                ogloBilling.Dispose();

                                if (_isTransactionOpen == false)
                                {
                                    //////frmBillingTransaction ofrmBillingTransaction = new frmBillingTransaction(_patientId, _transactionId, false, _databaseconnectionstring, "");
                                    //frmBillingTransaction ofrmBillingTransaction = frmBillingTransaction.GetInstance(_patientId, _transactionId, false, _databaseconnectionstring, "");
                                    //ofrmBillingTransaction.WindowState = FormWindowState.Maximized;
                                    //ofrmBillingTransaction.IsBatchModify = false;
                                    //ofrmBillingTransaction.IsSaveToHistoryForModify = true;
                                    //ofrmBillingTransaction.ShowDialog(this);
                                    //ofrmBillingTransaction.Dispose();
                                }
                                else
                                {
                                    DialogResult _dlgRst = DialogResult.None;
                                    _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + " \n Would you like to open this in View mode.", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (_dlgRst == DialogResult.OK)
                                    {
                                        //frmBillingTransaction ofrmBillingTransaction = frmBillingTransaction.GetInstance(_patientId, _transactionId, false, _databaseconnectionstring, "");
                                        //ofrmBillingTransaction.WindowState = FormWindowState.Maximized;
                                        //ofrmBillingTransaction.OpenViewMode = true;
                                        //ofrmBillingTransaction.IsBatchModify = false;
                                        //ofrmBillingTransaction.IsSaveToHistoryForModify = true;
                                        //ofrmBillingTransaction.ShowDialog(this);
                                        //ofrmBillingTransaction.Dispose();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cannot modify claim.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }

        #endregion

        #region "Error Claims"

        public DataTable GetClaims()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);            
            DataTable dtClaims = new DataTable();
            string _strSQL = "";
            try
            {
                oDB.Connect(false);

                _strSQL = " SELECT nRemitID, sClaimNumber, sClaimStatus, sTotalClaimAmount, sClaimPaymentAmount, sPayerControlNumber, sContractualObligation,  " +
                      " sCorrectionReversals, sOtherAdjustments, sPatientResponsibility, sInsuranceID, sClaimEndDate, sClaimStartDate, sCoverageAmount,  " +
                      " sDiscountAmount, sPatientPaidAmount, sInterestAmount, sTaxAmount, sOtherClaimID, sRenderingProviderID, sProviderID, sFiscalDate, " +
                      " sProviderAdjustment, nClinicID " +
                      " FROM  BL_Transaction_Remittance_Claims ";

                if (_SelectedReportType == ReportType.ErrorRemittance)
                {
                    _strSQL +=   " WHERE isnull(bIsProcessedForPayment,0)= 0 AND ISNULL(nRemitClaimStatus,0) = " + RemitClaimProcessStatus.ErrorPosting.GetHashCode() + "";
                }
                else if (_SelectedReportType == ReportType.ProcessRemittance)
                {
                    _strSQL +=   " WHERE isnull(bIsProcessedForPayment,0)= 1";
                }

                oDB.Retrive_Query(_strSQL, out dtClaims);                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return dtClaims;
        }

        private void FillClaimsGrid()
        {
            try
            {
                #region "Fill Remittance Claims"

                DataTable dtClaims = GetClaims();

                if (dtClaims != null)
                {
                    foreach (DataRow dr in dtClaims.Rows)
                    {
                        c1Discrepancy.Rows.Add();
                        int rowIndex = c1Discrepancy.Rows.Count - 1;

                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_SELECT, false);//Select-CheckBox
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMNO, Convert.ToString(dr["sClaimNumber"]).ToString()); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMENDDATE, Convert.ToString(dr["sClaimEndDate"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMPAYMENTAMOUNT, Convert.ToString(dr["sPatientPaidAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMSTARTDATE, Convert.ToString(dr["sClaimStartDate"])); //
                        //c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMSTATUS, Convert.ToString(dtClaims[_Index].ClaimStatus)); //
                        #region " Retrive Remit Status Code and Description "

                        string _strClaimStatus = "";

                        if (Convert.ToString(dr["sClaimStatus"]).Trim() != "")
                        {
                            int _claimStatusid = -1;
                            _claimStatusid = Convert.ToInt32(Convert.ToString(dr["sClaimStatus"]).Trim());
                            _strClaimStatus = _claimStatusid.ToString() + "-";
                            switch (_claimStatusid)
                            {
                                case 1:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Primary.ToString().Replace("_", " ");
                                    break;
                                case 2:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Secondary.ToString().Replace("_", " ");
                                    break;
                                case 3:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Tertiary.ToString().Replace("_", " ");
                                    break;
                                case 4:
                                    _strClaimStatus += RemitClaimStatus.Denied.ToString().Replace("_", " ");
                                    break;
                                case 5:
                                    _strClaimStatus += RemitClaimStatus.Pended.ToString().Replace("_", " ");
                                    break;
                                case 10:
                                    _strClaimStatus += RemitClaimStatus.Received_But_Not_InProcess.ToString().Replace("_", " ");
                                    break;
                                case 13:
                                    _strClaimStatus += RemitClaimStatus.Suspended.ToString().Replace("_", " ");
                                    break;
                                case 15:
                                    _strClaimStatus += RemitClaimStatus.Suspended_Investigation_With_Field.ToString().Replace("_", " ");
                                    break;
                                case 16:
                                    _strClaimStatus += RemitClaimStatus.Suspended_Return_With_Material.ToString().Replace("_", " ");
                                    break;
                                case 17:
                                    _strClaimStatus += RemitClaimStatus.Suspended_Review_Pending.ToString().Replace("_", " ");
                                    break;
                                case 19:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Primary_Forwarded_To_Additional_Payer.ToString().Replace("_", " ");
                                    break;
                                case 20:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Secondary_Forwarded_To_Additional_Payer.ToString().Replace("_", " ");
                                    break;
                                case 21:
                                    _strClaimStatus += RemitClaimStatus.Processed_As_Tertiary_Forwarded_To_Additional_Payer.ToString().Replace("_", " ");
                                    break;
                                case 22:
                                    _strClaimStatus += RemitClaimStatus.Reversal_of_Previous_Payment.ToString().Replace("_", " ");
                                    break;
                                case 23:
                                    _strClaimStatus += RemitClaimStatus.Not_Our_Claim_Forwarded_To_Additional_Payer.ToString().Replace("_", " ");
                                    break;
                                case 25:
                                    _strClaimStatus += RemitClaimStatus.Predetermination_Pricing_Only_No_Payment.ToString().Replace("_", " ");
                                    break;
                                case 27:
                                    _strClaimStatus += RemitClaimStatus.Reviewed.ToString().Replace("_", " ");
                                    break;
                                default:
                                    break;
                            }
                        }
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CLAIMSTATUS, _strClaimStatus.TrimEnd('-')); //

                        #endregion " Retrive Remit Status Code and Description "
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CONTRACTUALOBLIGATION, Convert.ToString(dr["sContractualObligation"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_CORRECTIONREVERSALS, Convert.ToString(dr["sCorrectionReversals"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_COVERAGEAMOUNT, Convert.ToString(dr["sCoverageAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_DISCOUNTAMOUNT, Convert.ToString(dr["sDiscountAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_FISCALDATE, Convert.ToString(dr["sFiscalDate"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_INTERESTAMOUNT, Convert.ToString(dr["sInterestAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_OTHERADJUSTMENTS, Convert.ToString(dr["sOtherAdjustments"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_OTHERCLAIMID, Convert.ToString(dr["sOtherClaimID"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_PATIENTPAIDAMOUNT, Convert.ToString(dr["sPatientPaidAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_PATIENTRESPOSIBILITY, Convert.ToString(dr["sPatientResponsibility"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_PAYERCONTROLNUMBER, Convert.ToString(dr["sPayerControlNumber"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_PROVIDERADJUSTMENT, Convert.ToString(dr["sProviderAdjustment"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_PROVIDERID, Convert.ToString(dr["sProviderID"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_REMITID, Convert.ToString(dr["nRemitID"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_RENDERINGPROVIDERID, Convert.ToString(dr["sRenderingProviderID"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_SUBSCRIBERID, Convert.ToString(dr["sInsuranceID"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_TAXAMOUNT, Convert.ToString(dr["sTaxAmount"])); //
                        c1Discrepancy.SetData(rowIndex, COL_CLAIM_TOTALCLAIMAMOUNT, Convert.ToString(dr["sTotalClaimAmount"])); //
                    }
                }
                #endregion

                //#region "Fill Remittance Claims Details"
                //if (dtClaimserviceLines != null && dtClaimserviceLines.Count > 0)
                //{
                //    for (int _Index = 0; _Index < dtClaimserviceLines.Count; _Index++)
                //    {
                //        c1RemittanceClaimsDetails.Rows.Add();
                //        int rowIndex = c1RemittanceClaimsDetails.Rows.Count - 1;

                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SELECT, false);//Select-CheckBox
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_LINEALLOWEDAMOUNT, Convert.ToString(dtClaimserviceLines[_Index].LineAllowedAmount)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_LINEITEMAMOUNT, Convert.ToString(dtClaimserviceLines[_Index].LineItemAmount)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_LINEPROVIDERPAYMENTAMOUNT, Convert.ToString(dtClaimserviceLines[_Index].LineProviderPaymentAmount)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICEDATE, Convert.ToString(dtClaimserviceLines[_Index].ServiceDate)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELINECONTRACTUALOBLIGATION, Convert.ToString(dtClaimserviceLines[_Index].ServiceLineContractualObligation)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELINECORRECTIONREVERSAL, Convert.ToString(dtClaimserviceLines[_Index].ServiceLineCorrectionReversal)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELINEOTHERADJUSTMENTS, Convert.ToString(dtClaimserviceLines[_Index].ServiceLineOtherAdjustments)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELINEPATIENTRESPONSIBILITY, Convert.ToString(dtClaimserviceLines[_Index].ServiceLinePatientResponsibility)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELINEPAYERINITIATEDREDUCTION, Convert.ToString(dtClaimserviceLines[_Index].ServiceLinePayerInitiatedReduction)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICELOCATION, Convert.ToString(dtClaimserviceLines[_Index].ServiceLocationOrPOS)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICEMODIFIER1, Convert.ToString(dtClaimserviceLines[_Index].ServiceModifier1)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICEMODIFIER2, Convert.ToString(dtClaimserviceLines[_Index].ServiceModifier2)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICEPROCEDURECODE, Convert.ToString(dtClaimserviceLines[_Index].ServiceProcedureCode)); //
                //        c1RemittanceClaimsDetails.SetData(rowIndex, COL_CLAIM_LINE_SERVICEPROVIDERCONTROLNO, Convert.ToString(dtClaimserviceLines[_Index].ServiceProviderControlNo)); //

                //    }
                //}
                //#endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion


    }
}