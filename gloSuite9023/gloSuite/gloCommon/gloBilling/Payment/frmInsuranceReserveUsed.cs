using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

using gloBilling.Payment;
using gloSettings;
using gloDateMaster;


namespace gloBilling.Payment
{
    public partial class frmInsuranceReserveUsed : Form
    {
        private DataTable _dtEOBPayments;

        public DataTable EOBUsedReserves
        {
            get { return _dtEOBPayments; }
            set { _dtEOBPayments = value; }
        }

        public frmInsuranceReserveUsed(Int64 EOBPaymentID)
        {
            InitializeComponent();
            try
            {
                this.EOBPaymentID = EOBPaymentID;
                EOBUsedReserves = InsurancePayment.GetInsuranceReservesUsed(EOBPaymentID);
            }
            catch 
            {
                EOBUsedReserves = null;
            }
        }

        #region  " Grid Constants "

        public static class ReservesAvailableFields
        {
            public const int Company = 0;
            public const int CheckNo = 1;
            public const int UsedReserve = 2;
            public const int Status = 3;
            public const int Count = 4;
        }

        #endregion

        public Int64 _EOBPaymentID = 0;
        public Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        private void frmInsuranceReserveUsed_Load(object sender, EventArgs e)
        {
            LoadReservesDetails();
        }

        private void LoadReservesDetails()
        {
            try
            {
                if (EOBUsedReserves != null)
                {
                    DesignReserveGrid();
                    foreach (DataRow row in EOBUsedReserves.Rows)
                    {
                        c1ReservesUsed.Rows.Add();
                        int i = c1ReservesUsed.Rows.Count - 1;
                        c1ReservesUsed.SetData(i, ReservesAvailableFields.Company, row["InsuranceCompany"]);
                        c1ReservesUsed.SetData(i, ReservesAvailableFields.CheckNo, row["CheckNumber"]);
                        c1ReservesUsed.SetData(i, ReservesAvailableFields.UsedReserve, row["UsedAmount"]);
                        c1ReservesUsed.SetData(i, ReservesAvailableFields.Status, row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void DesignReserveGrid()
        {
            try
            {
                #region " Grid Settings "

                c1ReservesUsed.Redraw = false;
                c1ReservesUsed.Clear();

                c1ReservesUsed.Cols.Count = ReservesAvailableFields.Count;
                c1ReservesUsed.Rows.Count = 1;
                c1ReservesUsed.Rows.Fixed = 1;
                c1ReservesUsed.Cols.Fixed = 0;

                c1ReservesUsed.AllowEditing = false;
                c1ReservesUsed.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ReservesUsed.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1ReservesUsed.SetData(0, ReservesAvailableFields.Company, "Insurance Company");
                c1ReservesUsed.SetData(0, ReservesAvailableFields.CheckNo, "Check# / Ref.#");
                c1ReservesUsed.SetData(0, ReservesAvailableFields.UsedReserve, "Used Reserves");
                c1ReservesUsed.SetData(0, ReservesAvailableFields.Status, "Status");

                #endregion

                #region " Show/Hide "

                c1ReservesUsed.Cols[ReservesAvailableFields.Company].Visible = true;
                c1ReservesUsed.Cols[ReservesAvailableFields.CheckNo].Visible = true;
                c1ReservesUsed.Cols[ReservesAvailableFields.UsedReserve].Visible = true;

                #endregion

                #region " Width "

                c1ReservesUsed.Cols[ReservesAvailableFields.Company].Width = 200;
                c1ReservesUsed.Cols[ReservesAvailableFields.CheckNo].Width = 100;
                c1ReservesUsed.Cols[ReservesAvailableFields.UsedReserve].Width = 100;
                c1ReservesUsed.Cols[ReservesAvailableFields.Status].Width = 70;

                #endregion

                #region " Data Type "

                c1ReservesUsed.Cols[ReservesAvailableFields.Company].DataType = typeof(System.String);
                c1ReservesUsed.Cols[ReservesAvailableFields.CheckNo].DataType = typeof(System.String);
                c1ReservesUsed.Cols[ReservesAvailableFields.UsedReserve].DataType = typeof(System.Decimal);

                #endregion

                #region " Alignment "

                c1ReservesUsed.Cols[ReservesAvailableFields.Company].TextAlign = TextAlignEnum.LeftCenter;
                c1ReservesUsed.Cols[ReservesAvailableFields.CheckNo].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1ReservesUsed.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1ReservesUsed.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1ReservesUsed.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1ReservesUsed.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1ReservesUsed.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
         

                c1ReservesUsed.Cols[ReservesAvailableFields.UsedReserve].Style = csCurrencyStyle;

                c1ReservesUsed.KeyActionEnter = KeyActionEnum.MoveAcross;
                c1ReservesUsed.VisualStyle = VisualStyle.Office2007Blue;
                c1ReservesUsed.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1ReservesUsed.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1ReservesUsed.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1ReservesUsed.Redraw = true; }
        }
    }
}
