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
    public partial class frmReservesAvailable : Form
    {
        #region  " Grid Constants "

        public static class ReservesAvailableFields
        {
            public const int Company = 0;
            public const int CheckNo = 1;
            public const int ToReserves = 2;
            public const int Count = 3;
        }

        #endregion

        public Int64 _EOBPaymentID = 0;
        public Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        public frmReservesAvailable(Int64 EOBPaymentID)
        {
            InitializeComponent();
            this.EOBPaymentID = EOBPaymentID;
        }

        private void DesignReserveGrid()
        {
            try
            {
                #region " Grid Settings "

                c1ReservesAvailable.Redraw = false;
                c1ReservesAvailable.Clear();

                c1ReservesAvailable.Cols.Count = ReservesAvailableFields.Count;
                c1ReservesAvailable.Rows.Count = 1;
                c1ReservesAvailable.Rows.Fixed = 1;
                c1ReservesAvailable.Cols.Fixed = 0;

                c1ReservesAvailable.AllowEditing = false;
                c1ReservesAvailable.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ReservesAvailable.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1ReservesAvailable.SetData(0, ReservesAvailableFields.Company, "Insurance Company");
                c1ReservesAvailable.SetData(0, ReservesAvailableFields.CheckNo, "Check# / Ref.#");
                c1ReservesAvailable.SetData(0, ReservesAvailableFields.ToReserves, "To Reserves");

                #endregion

                #region " Show/Hide "


                #endregion

                #region " Width "

                c1ReservesAvailable.Cols[ReservesAvailableFields.Company].Width = 200;
                c1ReservesAvailable.Cols[ReservesAvailableFields.CheckNo].Width = 100;
                c1ReservesAvailable.Cols[ReservesAvailableFields.ToReserves].Width = 100;

                #endregion

                #region " Data Type "

                c1ReservesAvailable.Cols[ReservesAvailableFields.Company].DataType = typeof(System.String);
                c1ReservesAvailable.Cols[ReservesAvailableFields.CheckNo].DataType = typeof(System.String);

                c1ReservesAvailable.Cols[ReservesAvailableFields.ToReserves].DataType = typeof(System.Decimal);
                #endregion

                #region " Alignment "

                c1ReservesAvailable.Cols[ReservesAvailableFields.Company].TextAlign = TextAlignEnum.LeftCenter;
                c1ReservesAvailable.Cols[ReservesAvailableFields.CheckNo].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1ReservesAvailable.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1ReservesAvailable.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1ReservesAvailable.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1ReservesAvailable.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1ReservesAvailable.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
         
            

                c1ReservesAvailable.Cols[ReservesAvailableFields.ToReserves].Style = csCurrencyStyle;

                c1ReservesAvailable.KeyActionEnter = KeyActionEnum.MoveAcross;
                c1ReservesAvailable.VisualStyle = VisualStyle.Office2007Blue;
                c1ReservesAvailable.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1ReservesAvailable.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1ReservesAvailable.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1ReservesAvailable.Redraw = true; }
        }

        private void frmReservesAvailable_Load(object sender, EventArgs e)
        {
            LoadReservesDetails();
        }

        private void LoadReservesDetails()
        {
            try
            {
                DataTable _dtEOBPayments = new DataTable();
                ///if (ValidateParameters())
                {
                    DesignReserveGrid();
                    _dtEOBPayments = InsurancePayment.GetInsuranceReservesAvailable(EOBPaymentID);
                    foreach (DataRow row in _dtEOBPayments.Rows)
                    {
                        c1ReservesAvailable.Rows.Add();
                        int i = c1ReservesAvailable.Rows.Count - 1;
                        c1ReservesAvailable.SetData(i, ReservesAvailableFields.Company, row["sPayerName"]);
                        c1ReservesAvailable.SetData(i, ReservesAvailableFields.CheckNo, row["sCheckNumber"]);
                        c1ReservesAvailable.SetData(i, ReservesAvailableFields.ToReserves, row["PrevPaid"]);
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
    }
}
