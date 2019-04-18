using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;
using gloPatientStripControl;
using gloDateMaster;
using gloBilling.EOBPayment.Common;
using System.Data.SqlClient;

namespace gloBilling.Payment
{
    public partial class frmInsurancePayment : Form
    {

        #region "Public Methods "

        private void CalculateActualAmountforMain(int iRow, int iCol, CorrTBActionPerformed action)
        {
            double dAmount = 0;
            try
            {
                if (action == CorrTBActionPerformed.Edit)
                {
                    object objCurrentValue = c1SinglePaymentCorrTB.GetData(iRow, iCol);
                    RowColEventArgs oArg = null;

                    switch (iCol)
                    {
                        case COL_ALLOWED:

                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_ALLOWED));

                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_StartEdit(null, oArg);

                            c1SinglePayment.SetData(iRow, iCol, dAmount);
                            c1SinglePayment.Select(iRow, iCol);

                            c1SinglePayment_AfterEdit(null, oArg);

                            if (_IsValidEntered)
                            {
                                //Assigning Writeoff Delta Value in Delta Grid.
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, dAmount);
                                //**                   
                            }


                            break;
                        case COL_PAYMENT:

                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_PAYMENT));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            //if (dAmount == 0 && c1SinglePayment.GetData(iRow, iCol)==null)
                            //{
                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                            //}

                            if (_IsValidEntered)
                            {
                                //Assigning Writeoff Delta Value in Delta Grid.
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                if (dAmount == 0 || c1SinglePayment.GetData(iRow, iCol) == null)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, null);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, dAmount);
                                }

                                //**                   
                            }

                            c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, Convert.ToString(c1SinglePayment.GetData(iRow, COL_NEXT)));
                            c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, Convert.ToString(c1SinglePayment.GetData(iRow, COL_PARTY)));
                            break;

                        case COL_WRITEOFF:
                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_WRITEOFF));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            break;

                        case COL_COPAY:
                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_COPAY));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            break;

                        case COL_DEDUCTIBLE:
                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_DEDUCTIBLE));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            break;

                        case COL_COINSURANCE:
                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_COINSURANCE));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            break;

                        case COL_WITHHOLD:
                            dAmount = Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, iCol)) + Convert.ToDouble(c1SinglePaymentCorrTB.GetData(iRow, COL_LAST_WITHHOLD));
                            c1SinglePayment.SetData(iRow, iCol, dAmount);

                            c1SinglePayment.Select(iRow, iCol);
                            oArg = new RowColEventArgs(iRow, iCol);
                            c1SinglePayment_AfterEdit(null, oArg);

                            break;
                    }

                    if (c1SinglePaymentCorrTB.GetData(iRow, iCol) == null && _IsValidEntered)
                    {
                        c1SinglePaymentCorrTB.SetData(iRow, iCol, objCurrentValue);
                    }
                   
                    if (!_IsValidEntered)
                    {
                        c1SinglePaymentCorrTB.Focus(); c1SinglePaymentCorrTB.Select(iRow, iCol);

                    }
                    c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));
                }
                else if (action == CorrTBActionPerformed.Delete)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(c1SinglePaymentCorrTB.GetData(iRow, iCol))))
                    {
                        KeyEventArgs oKeyArg = null;
                        switch (iCol)
                        {
                            case COL_WRITEOFF:
                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                c1SinglePayment.SetData(iRow, COL_WRITEOFF, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;
                            case COL_COPAY:
                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COPAY));
                                c1SinglePayment.SetData(iRow, COL_COPAY, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;
                            case COL_DEDUCTIBLE:
                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_DEDUCTIBLE));
                                c1SinglePayment.SetData(iRow, COL_DEDUCTIBLE, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;
                            case COL_COINSURANCE:
                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COINSURANCE));
                                c1SinglePayment.SetData(iRow, COL_COINSURANCE, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;
                            case COL_WITHHOLD:

                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WITHHOLD));
                                c1SinglePayment.SetData(iRow, COL_WITHHOLD, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;

                            case COL_ALLOWED:
                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_ALLOWED));
                                c1SinglePayment.SetData(iRow, COL_ALLOWED, dAmount);
                                c1SinglePayment.SetData(iRow, COL_WRITEOFF, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_TOTALCHARGE)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_ALLOWED)));
                                c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF)));
                                
                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));
                                break;

                            case COL_PAYMENT:

                                c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                //c1SinglePayment.SetData(iRow, iCol, null);
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_PAYMENT));
                                c1SinglePayment.SetData(iRow, COL_PAYMENT, dAmount);

                                //c1SinglePayment.Select(iRow, iCol);
                                //oKeyArg = new KeyEventArgs(Keys.Delete);
                                //c1SinglePayment_KeyUp(null, oKeyArg);

                                OnRemainingCalculationChanged();
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                break;
                            case COL_NEXT:
                            case COL_PARTY:

                                string _actionSelected = Convert.ToString(c1SinglePaymentCorrTB.GetData(c1SinglePaymentCorrTB.RowSel, COL_NEXT));

                                oKeyArg = new KeyEventArgs(Keys.Delete);
                                c1SinglePayment_KeyUp(null, oKeyArg);

                                if (_actionSelected.StartsWith("P"))
                                {
                                    for (int iRowCount = 1; iRowCount <= c1SinglePaymentCorrTB.Rows.Count - 1; iRowCount++)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRowCount, COL_NEXT, c1SinglePayment.GetData(iRowCount, COL_NEXT));
                                        c1SinglePaymentCorrTB.SetData(iRowCount, COL_PARTY, c1SinglePayment.GetData(iRowCount, COL_PARTY));
                                    }
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, c1SinglePayment.GetData(iRow, COL_NEXT));
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, c1SinglePayment.GetData(iRow, COL_PARTY));
                                }

                                break;
                        }

                        //Filling Delta Value in Correction/ Take back Grid
                        CalculateDeltaAmountforCorrTB(c1SinglePayment.RowSel, c1SinglePayment.ColSel, CorrTBActionPerformed.Delete);
                        //***
                    }                  

                }
                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void CalculateDeltaAmountforCorrTB(int iRow, int iCol, CorrTBActionPerformed action)
        {
            double dAmount = 0;
            try
            {
                if (action == CorrTBActionPerformed.Edit)
                {
                  //  RowColEventArgs oArg = null;

                    switch (iCol)
                    {
                        case COL_ALLOWED:

                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_ALLOWED));

                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_ALLOWED, dAmount);

                                    //Assigning Write off Delta Value in Delta Grid.
                                    dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, dAmount);
                                    //** 
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_ALLOWED, null);

                                    //Assigning Write off Delta Value in Delta Grid.
                                    dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                    if (dAmount != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, dAmount);
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, null);
                                    }
                                    //** 
                                }
                                                 
                            }

                           
                            break;
                        case COL_PAYMENT:
                            if (_IsValidEntered)
                            {
                                if (c1SinglePayment.GetData(iRow, iCol) != null)
                                {
                                    dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_PAYMENT));
                                    if (dAmount != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                    }

                                    //Allowed Calculation Added in 6022, and was missing in 6020
                                    //Assigning Write off Delta Value in Delta Grid.
                                    dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                    if (dAmount != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, dAmount);
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iRow, COL_WRITEOFF, null);
                                    }
                                    //**
                                    //**Upto this.

                                    #region Set editable true style

                                    if (c1SinglePaymentCorrTB.ColSel == COL_PAYMENT)
                                    {
                                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_WRITEOFF, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_COPAY, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_DEDUCTIBLE, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_COINSURANCE, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_WITHHOLD, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                                    }

                                    #endregion

                                    c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, Convert.ToString(c1SinglePayment.GetData(iRow, COL_NEXT)));
                                    c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, Convert.ToString(c1SinglePayment.GetData(iRow, COL_PARTY)));
                                }
                            }

                            
                            break;

                        case COL_WRITEOFF:

                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }
                            break;

                        case COL_COPAY:
                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COPAY));
                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }
                            break;

                        case COL_DEDUCTIBLE:
                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_DEDUCTIBLE));
                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }

                            break;

                        case COL_COINSURANCE:
                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COINSURANCE));
                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }

                            break;

                        case COL_WITHHOLD:
                            if (_IsValidEntered)
                            {
                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WITHHOLD));
                                if (dAmount != 0)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                }
                                else
                                {
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }
                            break;
                        case COL_NEXT:
                        case COL_PARTY:

                            for (int iRowCount = 1; iRowCount <= c1SinglePaymentCorrTB.Rows.Count - 1; iRowCount++)
                            {
                                c1SinglePaymentCorrTB.SetData(iRowCount, COL_NEXT, c1SinglePayment.GetData(iRowCount, COL_NEXT));
                                c1SinglePaymentCorrTB.SetData(iRowCount, COL_PARTY, c1SinglePayment.GetData(iRowCount, COL_PARTY));
                            }
                            break;
                    }
                    if (c1SinglePaymentCorrTB.Rows.Count >= iRow && c1SinglePayment.Rows.Count >= iRow)
                    {
                        c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));
                    }
                }
                else if (action == CorrTBActionPerformed.Delete)
                {
                   // KeyEventArgs oKeyArg = null;
                    switch (iCol)
                    {
                        case COL_PAYMENT:
                        case COL_WRITEOFF:
                        case COL_COPAY:
                        case COL_DEDUCTIBLE:
                        case COL_COINSURANCE:
                        case COL_WITHHOLD:

                            if (c1SinglePaymentCorrTB.Rows.Count - 1 >= iRow && c1SinglePaymentCorrTB.Cols.Count - 1 >= iCol)
                            {
                                switch (iCol)
                                {
                                    case COL_PAYMENT:

                                        if (_IsValidEntered)
                                        {
                                            
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_PAYMENT));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, Convert.ToString(c1SinglePayment.GetData(iRow, COL_NEXT)));
                                            c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, Convert.ToString(c1SinglePayment.GetData(iRow, COL_PARTY)));
                                            
                                        }
                                        break;
                                    case COL_WRITEOFF:

                                        if (_IsValidEntered)
                                        {
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WRITEOFF));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                        }
                                        break;
                                    case COL_COPAY:

                                        if (_IsValidEntered)
                                        {
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COPAY));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                        }
                                        break;
                                    case COL_DEDUCTIBLE:

                                        if (_IsValidEntered)
                                        {
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_DEDUCTIBLE));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                        }
                                        break;
                                    case COL_COINSURANCE:

                                        if (_IsValidEntered)
                                        {
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_COINSURANCE));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                        }
                                        break;
                                    case COL_WITHHOLD:

                                        if (_IsValidEntered)
                                        {
                                            //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_WITHHOLD));
                                            //if (dAmount != 0)
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                            //}
                                            //else
                                            //{
                                            //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                            //}
                                            c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                        }
                                        break;
                                }


                                //c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                c1SinglePaymentCorrTB.SetData(iRow, COL_BALANCE, Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_BALANCE)));

                                for (int iRowCount = 1; iRowCount <= c1SinglePaymentCorrTB.Rows.Count - 1; iRowCount++)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRowCount, COL_NEXT, c1SinglePayment.GetData(iRowCount, COL_NEXT));
                                    c1SinglePaymentCorrTB.SetData(iRowCount, COL_PARTY, c1SinglePayment.GetData(iRowCount, COL_PARTY));
                                }
                            }

                            break;

                        case COL_ALLOWED:

                            if (c1SinglePaymentCorrTB.Rows.Count - 1 >= iRow && c1SinglePaymentCorrTB.Cols.Count - 1 >= iCol)
                            {
                                if (_IsValidEntered)
                                {
                                    //dAmount = Convert.ToDouble(c1SinglePayment.GetData(iRow, iCol)) - Convert.ToDouble(c1SinglePayment.GetData(iRow, COL_LAST_ALLOWED));
                                    //if (dAmount != 0)
                                    //{
                                    //    c1SinglePaymentCorrTB.SetData(iRow, iCol, dAmount);
                                    //}
                                    //else
                                    //{
                                    //    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                    //}
                                    c1SinglePaymentCorrTB.SetData(iRow, iCol, null);
                                }
                            }

                            break;

                        case COL_NEXT:
                        case COL_PARTY:

                            for (int iRowCount = 1; iRowCount <= c1SinglePayment.Rows.Count - 1; iRowCount++)
                            {
                                if (c1SinglePaymentCorrTB.Rows.Count - 1 >= iRowCount)
                                {
                                    c1SinglePaymentCorrTB.SetData(iRowCount, COL_NEXT, c1SinglePayment.GetData(iRowCount, COL_NEXT));
                                    c1SinglePaymentCorrTB.SetData(iRowCount, COL_PARTY, c1SinglePayment.GetData(iRowCount, COL_PARTY));
                                }
                            }
                            
                            break;
                    }

                    OnRemainingCalculationChanged();

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
               

        private void FillDataInCorrectionTBWindow()
        {
            c1SinglePaymentCorrTB.ScrollBars = ScrollBars.Both;
            try
            {
                for (int iMainRCount = 1; iMainRCount <= c1SinglePayment.Rows.Count - 1; iMainRCount++)
                {
                    c1SinglePaymentCorrTB.Rows.Add();

                    for (int iMainCCount = 1; iMainCCount <= c1SinglePayment.Cols.Count - 1; iMainCCount++)
                    {
                        c1SinglePaymentCorrTB.SetCellStyle(iMainRCount, iMainCCount, c1SinglePayment.GetCellStyle(iMainRCount, iMainCCount));

                        if (iMainCCount != COL_ALLOWED)
                        {
                            c1SinglePaymentCorrTB.SetData(iMainRCount, iMainCCount, c1SinglePayment.GetData(iMainRCount, iMainCCount));
                        }
                        else
                        {
                            c1SinglePaymentCorrTB.SetData(iMainRCount, iMainCCount, "");
                        }

                        switch (iMainCCount)
                        {
                            case COL_ALLOWED:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_ALLOWED)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_ALLOWED)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_ALLOWED)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_ALLOWED, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_ALLOWED)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_ALLOWED)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_ALLOWED, null);
                                    }
                                }
                                break;
                            case COL_PAYMENT:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_PAYMENT)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_PAYMENT)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_PAYMENT)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_PAYMENT, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_PAYMENT)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_PAYMENT)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_PAYMENT, null);
                                    }
                                }
                                break;
                            case COL_WRITEOFF:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_WRITEOFF)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_WRITEOFF)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_WRITEOFF, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_WRITEOFF)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_WRITEOFF, null);
                                    }
                                }
                                break;
                            case COL_COPAY:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_COPAY)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_COPAY)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_COPAY)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_COPAY, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_COPAY)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_COPAY)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_COPAY, null);
                                    }
                                }
                                break;
                            case COL_DEDUCTIBLE:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_DEDUCTIBLE)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_DEDUCTIBLE)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_DEDUCTIBLE)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_DEDUCTIBLE, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_DEDUCTIBLE)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_DEDUCTIBLE)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_DEDUCTIBLE, null);
                                    }
                                }
                                break;
                            case COL_COINSURANCE:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_COINSURANCE)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_COINSURANCE)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_COINSURANCE)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_COINSURANCE, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_COINSURANCE)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_COINSURANCE)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_COINSURANCE, null);
                                    }
                                }
                                break;
                            case COL_WITHHOLD:
                                if (Convert.ToString(c1SinglePayment.GetData(iMainRCount, COL_WITHHOLD)) != string.Empty)
                                {
                                    if (Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_WITHHOLD)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_WITHHOLD)) != 0)
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_WITHHOLD, Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_WITHHOLD)) - Convert.ToDouble(c1SinglePayment.GetData(iMainRCount, COL_LAST_WITHHOLD)));
                                    }
                                    else
                                    {
                                        c1SinglePaymentCorrTB.SetData(iMainRCount, COL_WITHHOLD, null);
                                    }
                                }
                                break;
                        }
                    }

                    //Visible Rows True or False based on Main Payment Grid
                    c1SinglePaymentCorrTB.Rows[iMainRCount].Visible = c1SinglePayment.Rows[iMainRCount].Visible;

                    //**

                    #region " Set Allowed & WriteOff for Secondary "

                    if (c1SinglePaymentCorrTB.GetData(iMainRCount, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePaymentCorrTB.GetData(iMainRCount, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (PatientControl.SelectedInsuranceParty > 1)
                        {
                            c1SinglePaymentCorrTB.SetCellStyle(iMainRCount, COL_ALLOWED, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                            c1SinglePaymentCorrTB.Cols[COL_ALLOWED].AllowEditing = false;
                            c1SinglePaymentCorrTB.SetData(iMainRCount, COL_WRITEOFF, null);

                        }
                        else
                        {
                            c1SinglePaymentCorrTB.SetCellStyle(iMainRCount, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                            c1SinglePaymentCorrTB.Cols[COL_ALLOWED].AllowEditing = true;

                        }
                    }

                    #endregion
                }

                if (c1SinglePaymentCorrTB.Rows.Count>=8 && pnlSinglePaymentCorrTB.Visible)//.ScrollBarsVisible == ScrollBars.Vertical)
                {
                    ResizeGridColumn(COL_PARTY, c1SinglePaymentCorrTB, 80);
                    RowColEventArgs oArg = new RowColEventArgs(c1SinglePaymentCorrTB.RowSel, COL_PARTY);
                    c1SinglePaymentCorrTB_AfterResizeColumn(null, oArg);
                }
                else
                {
                    ResizeGridColumn(COL_PARTY, c1SinglePaymentCorrTB, 98);
                    RowColEventArgs oArg = new RowColEventArgs(c1SinglePaymentCorrTB.RowSel, COL_PARTY);
                    c1SinglePaymentCorrTB_AfterResizeColumn(null, oArg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeGridColumn(Int16 iColumnIndex,C1FlexGrid c1Grid,Int16 iValue)
        {
            try
            {
                c1Grid.Cols[iColumnIndex].Width = iValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateWriteOffCorrTB(RowColEventArgs e)
        {
            decimal _chargePerUnit = 0;
            decimal _charges = 0;
            decimal _allowed = 0;
            decimal _writeOff = 0;

            if (PatientControl.SelectedInsuranceParty == 1 && (e.Col == COL_ALLOWED || e.Col == COL_PAYMENT))
            {
                _chargePerUnit = Convert.ToDecimal(Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_CHARGE)));
                _charges = Convert.ToDecimal(Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_TOTALCHARGE)));

                bool _isAllowedNull = false;

                if (c1SinglePayment.GetData(e.Row, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(e.Row, COL_ALLOWED)).Trim() != "")
                { _allowed = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(e.Row, COL_ALLOWED))); }
                else
                { _isAllowedNull = true; }

                _writeOff = _charges - _allowed;

                if (_allowed <= _charges)
                {
                    if (_isAllowedNull == false)
                    {
                        c1SinglePaymentCorrTB.SetData(e.Row, COL_WRITEOFF, _writeOff);
                    }
                }
            }
        }

        private void CalculateRunningBalanceCorrTB(int rowIndex)
        {
            bool _IsPaymentCorrectionMode = false;
            decimal _actualBalance = 0;
            decimal _newBalance = 0;

            decimal _currPayment = 0;
            decimal _currWriteOff = 0;
            decimal _currWithHold = 0;

            decimal _lastPaidAmt = 0;
            decimal _lastWOAmt = 0;
            decimal _lastWHAmt = 0;

            //bool _isNullCurrPayment = false;
            //bool _isNullCurrWriteOff = false;
            //bool _isNullCurrWithHold = false;
            //bool _isNullLastPaidAmt = false;
            //bool _isNullLastWOAmt = false;
            //bool _isNullLastWHAmt = false;

            try
            {
                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_ISCORRECTION) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_ISCORRECTION)).Trim() != "")
                { _IsPaymentCorrectionMode = Convert.ToBoolean(c1SinglePaymentCorrTB.GetData(rowIndex, COL_ISCORRECTION)); }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_PAYMENT)).Trim() != "")
                { _currPayment = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_PAYMENT)); }
                else { ;} //_isNullCurrPayment = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_PAYMENT)).Trim() != "")
                { _lastPaidAmt = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_PAYMENT)); }
                else { ;}  //_isNullLastPaidAmt = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_WRITEOFF)).Trim() != "")
                { _currWriteOff = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_WRITEOFF)); }
                else { ;} //_isNullCurrWriteOff = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_WITHHOLD)).Trim() != "")
                { _currWithHold = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_WITHHOLD)); }
                else { ;} // _isNullCurrWithHold = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WRITEOFF) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WRITEOFF)).Trim() != "")
                { _lastWOAmt = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WRITEOFF)); }
                else { ;} //_isNullCurrWriteOff = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WITHHOLD) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WITHHOLD)).Trim() != "")
                { _lastWHAmt = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LAST_WITHHOLD)); }
                else { ;}  //_isNullLastWHAmt = true; }

                if (c1SinglePaymentCorrTB.GetData(rowIndex, COL_LINE_DB_BALANCE) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LINE_DB_BALANCE)).Trim() != "")
                { _actualBalance = Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rowIndex, COL_LINE_DB_BALANCE)); }

                if (_IsPaymentCorrectionMode)
                {
                    _newBalance = (_actualBalance + _lastPaidAmt + _lastWOAmt + _lastWHAmt) - (_currPayment + _currWithHold + _currWriteOff);
                }
                else
                {
                    _newBalance = (_actualBalance) - (_currPayment + _currWithHold + _currWriteOff);
                }

                //if (_IsPaymentCorrectionMode)
                //{
                //    if (_currPayment != 0)
                //    {
                //        _newBalance = (_actualBalance + _lastPaidAmt + _lastWOAmt + _lastWHAmt) - (_currPayment + _currWithHold + _currWriteOff);
                //    }
                //    else
                //    {
                //        _newBalance = _actualBalance;
                //    }
                //}
                //else
                //{
                //    _newBalance = (_actualBalance) - (_currPayment + _currWithHold + _currWriteOff);
                //}

                c1SinglePaymentCorrTB.SetData(rowIndex, COL_BALANCE, _newBalance);
                //CalculateTotals();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { }
        } 

        #endregion
              
        #region " C1 Grid Events "

        private void c1SinglePaymentCorrTB_AfterEdit(object sender, RowColEventArgs e)
        {
            RowColEventArgs oArg = null;
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            bool _isChargeLineSplitted = Convert.ToBoolean(c1SinglePaymentCorrTB.GetData(e.Row, COL_ISSPLITTED));

            switch (e.Col)
            {
                case COL_NEXT:

                    //string _selection = Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, e.Col));

                    c1SinglePayment.Select(e.Row, e.Col);
                    c1SinglePayment.SetData(e.Row, COL_NEXT, Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_NEXT)));
                    oArg = new RowColEventArgs(e.Row, e.Col);
                    c1SinglePayment_AfterEdit(null, oArg);

                    //if (_selection.StartsWith("P"))
                    //{
                        for (int iRow = 1; iRow <= c1SinglePaymentCorrTB.Rows.Count - 1; iRow++)
                        {
                            c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, c1SinglePayment.GetData(iRow, COL_NEXT));
                            c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, c1SinglePayment.GetData(iRow, COL_PARTY));
                        }
                    //}
                    //else
                    //{
                    //    c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PARTY)));
                    //}
                    break;

                case COL_PARTY:

                    string _NextSelection = Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_NEXT));

                    c1SinglePayment.Select(e.Row, e.Col);
                    c1SinglePayment.SetData(e.Row, COL_PARTY, Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_PARTY)));
                    oArg = new RowColEventArgs(e.Row, e.Col);
                    c1SinglePayment_AfterEdit(null, oArg);

                    //if (_NextSelection.StartsWith("P"))
                    //{
                        for (int iRow = 1; iRow <= c1SinglePaymentCorrTB.Rows.Count - 1; iRow++)
                        {
                            //c1SinglePaymentCorrTB.SetData(iRow, COL_NEXT, c1SinglePayment.GetData(iRow, COL_NEXT));
                            c1SinglePaymentCorrTB.SetData(iRow, COL_PARTY, c1SinglePayment.GetData(iRow, COL_PARTY));
                        }
                    //}

                    break;

                case COL_ALLOWED:
                case COL_PAYMENT:
                case COL_WRITEOFF:
                case COL_COPAY:
                case COL_DEDUCTIBLE:
                case COL_COINSURANCE:
                case COL_WITHHOLD:

                    #region  " Payment Changed - Allowed, Payment, WriteOff, Copay, Deductible, CoInsurance, WithHold"

                    #region Set editable true style

                    if (c1SinglePaymentCorrTB.ColSel == COL_PAYMENT)
                    {
                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_WRITEOFF, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_COPAY, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_DEDUCTIBLE, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_COINSURANCE, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePaymentCorrTB.SetCellStyle(c1SinglePaymentCorrTB.RowSel, COL_WITHHOLD, c1SinglePaymentCorrTB.Styles["cs_EditableCurrencyStyle"]);
                    }

                    #endregion

                    CalculateActualAmountforMain(e.Row, e.Col, CorrTBActionPerformed.Edit);

                    #endregion

                    break;
            }

        }

        private void c1SinglePaymentCorrTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                CalculateActualAmountforMain(c1SinglePaymentCorrTB.RowSel, c1SinglePaymentCorrTB.ColSel, CorrTBActionPerformed.Delete);
                  
            }
        }

        private void c1SinglePaymentCorrTB_EnterCell(object sender, EventArgs e)
        {
            if (c1SinglePayment.Rows.Count - 1 >= c1SinglePaymentCorrTB.RowSel && c1SinglePayment.Cols.Count - 1 >= c1SinglePaymentCorrTB.ColSel)
            {
                c1SinglePayment.Select(c1SinglePaymentCorrTB.RowSel, c1SinglePaymentCorrTB.ColSel);
            }
        }

        private void c1SinglePaymentCorrTB_AfterScroll(object sender, RangeEventArgs e)
        {
            int x = c1SinglePaymentCorrTB.ScrollPosition.X;
            int y = c1SinglePaymentCorrTB.ScrollPosition.Y;
            c1SinglePayment.ScrollPosition = new Point(x, y);
        }
        
        private void c1SinglePaymentCorrTB_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1SinglePaymentTotal.Cols[e.Col].Width = c1SinglePaymentCorrTB.Cols[e.Col].Width;
                c1MultiplePayment.Cols[e.Col].Width = c1SinglePaymentCorrTB.Cols[e.Col].Width;
                c1MultiplePaymentTotal.Cols[e.Col].Width = c1SinglePaymentCorrTB.Cols[e.Col].Width;
                c1SinglePayment.Cols[e.Col].Width = c1SinglePaymentCorrTB.Cols[e.Col].Width;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1SinglePaymentCorrTB_CellButtonClick(object sender, RowColEventArgs e)
        {
            c1SinglePayment.Select(e.Row, e.Col);
            SelectReasonCodes();
        }

        private void c1SinglePaymentCorrTB_StartEdit(object sender, RowColEventArgs e)
        {

        }

        private void c1SinglePayment_EnterCell(object sender, EventArgs e)
        {
            if (c1SinglePaymentCorrTB.Rows.Count - 1 >= c1SinglePayment.RowSel && c1SinglePaymentCorrTB.Cols.Count - 1 >= c1SinglePayment.ColSel)
            {
                c1SinglePaymentCorrTB.Select(c1SinglePayment.RowSel, c1SinglePayment.ColSel);
            }
        }

        private void c1SinglePayment_AfterScroll(object sender, RangeEventArgs e)
        {
            int x = c1SinglePayment.ScrollPosition.X;
            int y = c1SinglePayment.ScrollPosition.Y;
            c1SinglePaymentCorrTB.ScrollPosition = new Point(x, y);
        }

        private void c1SinglePayment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.c1SinglePayment.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);

                if (c1SinglePayment.ColSel == COL_ALLOWED || c1SinglePayment.ColSel == COL_PAYMENT || c1SinglePayment.ColSel == COL_WRITEOFF || c1SinglePayment.ColSel == COL_COPAY || c1SinglePayment.ColSel == COL_DEDUCTIBLE || c1SinglePayment.ColSel == COL_COINSURANCE || c1SinglePayment.ColSel == COL_WITHHOLD || c1SinglePayment.ColSel == COL_NEXT || c1SinglePayment.ColSel == COL_PARTY)
                {
                    if (c1SinglePayment.ColSel == COL_PAYMENT)
                    {
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_PAYMENT, null);

                        // Not to clear W/O if payment amount is cleared
                        // Commented on 29/07/2010
                        //c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_WRITEOFF, null);

                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_COPAY, null);
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_DEDUCTIBLE, null);
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_COINSURANCE, null);
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_WITHHOLD, null);

                        // Clear the Next action & Party if payment is cleared
                        // Added on 29/07/2010
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_NEXT, null);
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_PARTY, null);

                        //string _selectedAction = Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_NEXT));
                        //decimal _PaymentDone = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAYMENT));

                        //// If Payment is done & action is not selected then set Next & party to default
                        //if (_selectedAction.Equals(string.Empty) && (_PaymentDone != 0))
                        //{
                        //    c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_NEXT, null);
                        //    c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_PARTY, null);
                        //}
                    }
                    else if ((c1SinglePayment.ColSel == COL_NEXT) || (c1SinglePayment.ColSel == COL_PARTY))
                    {
                        #region " If deleted action is Pending, then clear the selected party for all the service lines "

                        string _actionSelected = Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_NEXT));
                        string _partySelected = Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PARTY));

                        if (_actionSelected.StartsWith("P"))
                        {
                            this.c1SinglePayment.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);
                            for (int rInd = 1; rInd < c1SinglePayment.Rows.Count; rInd++)
                            {
                                if (c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) != null && Convert.ToInt32(c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE)) == ColServiceLineType.ServiceLine.GetHashCode())
                                {
                                    if (c1SinglePayment.ColSel == COL_NEXT)
                                    {
                                        c1SinglePayment.SetData(rInd, COL_NEXT, null);
                                        c1SinglePayment.SetData(rInd, COL_PARTY, null);
                                    }
                                    else if (c1SinglePayment.ColSel == COL_PARTY)
                                    {
                                        c1SinglePayment.SetData(rInd, COL_PARTY, null);
                                    }
                                }
                            }
                            this.c1SinglePayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);
                        }
                        else
                        {
                            c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_NEXT, null);
                            c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_PARTY, null);
                        }

                        #endregion
                    }
                    else if (c1SinglePayment.ColSel == COL_ALLOWED && c1SinglePayment.Cols[COL_ALLOWED].AllowEditing == false)
                    {

                    }
                    else
                    {
                        c1SinglePayment.SetData(c1SinglePayment.RowSel, c1SinglePayment.ColSel, null);
                    }

                    //RowColEventArgs _args = new RowColEventArgs(c1SinglePayment.RowSel, c1SinglePayment.ColSel);
                    //CalculateRunningBalance(_args);

                    OnRemainingCalculationChanged();
                }
                this.c1SinglePayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);

                //Filling Delta Value in Correction/ Take back Grid
                CalculateDeltaAmountforCorrTB(c1SinglePayment.RowSel, c1SinglePayment.ColSel, CorrTBActionPerformed.Delete);
                //***
            }
        }

        private void c1SinglePayment_AfterEdit(object sender, RowColEventArgs e)
        {
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            bool _isChargeLineSplitted = Convert.ToBoolean(c1SinglePayment.GetData(e.Row, COL_ISSPLITTED));

            #region  " Payment Changed - Allowed, Payment, WriteOff, Copay, Deductible, CoInsurance, WithHold"

            if (e.Col == COL_ALLOWED || e.Col == COL_PAYMENT || e.Col == COL_WRITEOFF || e.Col == COL_COPAY || e.Col == COL_DEDUCTIBLE || e.Col == COL_COINSURANCE || e.Col == COL_WITHHOLD)
            {
                #region Set editable true style

                if (c1SinglePayment.ColSel == COL_PAYMENT)
                {
                    c1SinglePayment.SetCellStyle(c1SinglePayment.RowSel, COL_WRITEOFF, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                    c1SinglePayment.SetCellStyle(c1SinglePayment.RowSel, COL_COPAY, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                    c1SinglePayment.SetCellStyle(c1SinglePayment.RowSel, COL_DEDUCTIBLE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                    c1SinglePayment.SetCellStyle(c1SinglePayment.RowSel, COL_COINSURANCE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                    c1SinglePayment.SetCellStyle(c1SinglePayment.RowSel, COL_WITHHOLD, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                }

                #endregion

                if (!IsInsurancePlanSelected())
                {
                    c1SinglePayment.SetData(e.Row, e.Col, null);
                    return;
                }

                if (!IsValidAmount(e))
                { return; }

                if ((e.Col == COL_ALLOWED) || (e.Col == COL_PAYMENT))
                {
                    CalculateWriteOff(e);
                }
                if (e.Col == COL_PAYMENT)
                {
                    if (c1SinglePayment.GetData(e.Row, e.Col) != null && Convert.ToDecimal(c1SinglePayment.GetData(e.Row, e.Col)) >= 0)
                    {
                        // Warning Check : For the different Insurance Company
                        CheckInsuranceCompany();
                    }

                    if (!_isChargeLineSplitted)
                    {
                        string _selectedAction = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_NEXT));
                        string _strPayment = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PAYMENT));
                        decimal _payment = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_PAYMENT));

                        bool _isBilled = IsPartyBilled(PatientControl.GetNextParty(), _selectedAction);

                        // If Payment is done (greater than or equal to zero) & action is not selected then set Next & party to default
                        if ((_selectedAction.Equals(string.Empty)) && (_strPayment != ""))
                        {
                            #region " Set default action to bill & also set resp default party "

                            c1SinglePayment.SetData(e.Row, COL_NEXT, ogloEOBPaymentInsurance.GetPaymentActionStatus("B"));

                            if (_isBilled)
                            { c1SinglePayment.SetData(e.Row, COL_PARTY, null); }
                            else
                            { c1SinglePayment.SetData(e.Row, COL_PARTY, PatientControl.GetNextParty()); }

                            #endregion
                        }
                    }
                }
                //CalculateRunningBalance(e);
                OnRemainingCalculationChanged();
            }
            #endregion

            #region " Next Action Changed - Next , Party "

            if (e.Col.Equals(COL_NEXT) || e.Col.Equals(COL_PARTY))
            {
                int _selfPartyNo = PatientControl.GetSelfPartyNo();
                string _selection = Convert.ToString(c1SinglePayment.GetData(e.Row, e.Col));

                if (_selection.Equals(string.Empty))
                {
                    // If either Next or Party is blank, both will be reset to blank.
                    //c1SinglePayment.SetData(e.Row, COL_NEXT, null);
                    //c1SinglePayment.SetData(e.Row, COL_PARTY, null);
                }
                else
                {
                    if (!IsNextOrPartyAllowed(e))
                    { return; }

                    if (e.Col == COL_NEXT)
                    {
                        if (!IsValidCode(e, COL_NEXT))
                        { return; }

                        #region " Set default party based on the next action selected for each service line "

                        this.c1SinglePayment.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);
                        for (int rInd = 1; rInd < c1SinglePayment.Rows.Count; rInd++)
                        {
                            if (c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) != null && Convert.ToInt32(c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE)) == ColServiceLineType.ServiceLine.GetHashCode())
                            {
                                string _nextAction = Convert.ToString(c1SinglePayment.GetData(rInd, COL_NEXT));
                                if (_selection.StartsWith("P"))
                                {
                                    if (!_isChargeLineSplitted)
                                    {
                                        c1SinglePayment.SetData(rInd, COL_NEXT, ogloEOBPaymentInsurance.GetPaymentActionStatus("P"));

                                        string _nextParty = PatientControl.GetNextParty();

                                        // if next party is self, set to null as self is not allowed for Pending Action
                                        if (_nextParty.StartsWith(_selfPartyNo.ToString()))
                                        { c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                                        else
                                        { c1SinglePayment.SetData(rInd, COL_PARTY, _nextParty); }
                                    }
                                }
                                else
                                {
                                    if (rInd.Equals(e.Row))
                                    {
                                        bool _isBilled = false;
                                        if (_nextAction.StartsWith("B")) // B = Bill
                                        {
                                            _isBilled = IsPartyBilled(PatientControl.GetNextParty(), _nextAction);
                                            c1SinglePayment.SetData(rInd, COL_NEXT, ogloEOBPaymentInsurance.GetPaymentActionStatus("B"));

                                            if (_isBilled)
                                            { c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                                            else
                                            { c1SinglePayment.SetData(rInd, COL_PARTY, PatientControl.GetNextParty()); }
                                        }
                                        else if (_nextAction.StartsWith("R") || _nextAction.StartsWith("N")) // R = Rebill & N = None
                                        {
                                            _isBilled = IsPartyBilled(PatientControl.GetCurrentParty(), _nextAction);
                                            string initChar = (_nextAction.StartsWith("R") == true ? "R" : "N");
                                            c1SinglePayment.SetData(rInd, COL_NEXT, ogloEOBPaymentInsurance.GetPaymentActionStatus(initChar));

                                            if (!_isBilled)
                                            { c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                                            else
                                            { c1SinglePayment.SetData(rInd, COL_PARTY, PatientControl.GetCurrentParty()); }
                                        }
                                        //else if (_nextAction.StartsWith("N")) // N = None
                                        //{ c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                                    }
                                    else
                                    {
                                        if (_nextAction.StartsWith("P"))
                                        {
                                            c1SinglePayment.SetData(rInd, COL_NEXT, null);
                                            c1SinglePayment.SetData(rInd, COL_PARTY, null);
                                        }
                                    }
                                }
                            }
                        }
                        this.c1SinglePayment.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);

                        #endregion
                    }
                    else if (e.Col == COL_PARTY)
                    {
                        if (!IsValidCode(e, COL_PARTY))
                        { return; }

                        if (!IsValidParty(e))
                        { return; }

                        #region " If Pending action selected, then set the selected party to all the service lines "

                        string _actionSelected = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_NEXT));
                        string _partySelected = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PARTY));

                        this.c1SinglePayment.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);
                        for (int rInd = 1; rInd < c1SinglePayment.Rows.Count; rInd++)
                        {
                            if (c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) != null && Convert.ToInt32(c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE)) == ColServiceLineType.ServiceLine.GetHashCode())
                            {
                                if (_actionSelected.StartsWith("P"))
                                {
                                    if (!_isChargeLineSplitted)
                                    {
                                        // if next party is self, set to null as self is not allowed for Pending Action
                                        if (_partySelected.StartsWith(_selfPartyNo.ToString()))
                                        { c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                                        else
                                        { c1SinglePayment.SetData(rInd, COL_PARTY, _partySelected); }
                                    }
                                }
                            }
                        }
                        this.c1SinglePayment.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);

                        #endregion
                    }
                }
            }
            #endregion

            //Filling Delta Value in Correction/ Take back Grid
            CalculateDeltaAmountforCorrTB(e.Row, e.Col, CorrTBActionPerformed.Edit);
            //*****
        }

        private void c1SinglePayment_StartEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == COL_ALLOWED)
            {
                _allowedAmountBeforeEdit = 0;

                if (c1SinglePayment.GetData(e.Row, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(e.Row, COL_ALLOWED)).Trim() != "")
                { _allowedAmountBeforeEdit = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_ALLOWED)); }
            }
        }

        private void c1SinglePayment_CellButtonClick(object sender, RowColEventArgs e)
        {
            SelectReasonCodes();
        }

        private void c1SinglePayment_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1SinglePaymentTotal.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
                c1MultiplePayment.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
                c1MultiplePaymentTotal.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
                c1SinglePaymentCorrTB.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1MultiplePayment_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1MultiplePaymentTotal.Cols[e.Col].Width = c1MultiplePayment.Cols[e.Col].Width;
                //c1SinglePayment.Cols[e.Col].Width = c1MultiplePayment.Cols[e.Col].Width;
                //c1SinglePaymentTotal.Cols[e.Col].Width = c1MultiplePayment.Cols[e.Col].Width;
                //c1SinglePaymentCorrTB.Cols[e.Col].Width = c1MultiplePayment.Cols[e.Col].Width;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Calculation Methods "

        private void CalculateWriteOff(RowColEventArgs e)
        {
            decimal _chargePerUnit = 0;
            decimal _charges = 0;
            decimal _allowed = 0;
            decimal _writeOff = 0;

            if (PatientControl.SelectedInsuranceParty == 1 && (e.Col == COL_ALLOWED || e.Col == COL_PAYMENT))
            {
                _chargePerUnit = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(e.Row, COL_CHARGE)));
                _charges = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(e.Row, COL_TOTALCHARGE)));

                bool _isAllowedNull = false;

                if (c1SinglePayment.GetData(e.Row, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(e.Row, COL_ALLOWED)).Trim() != "")
                { _allowed = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(e.Row, COL_ALLOWED))); }
                else
                { _isAllowedNull = true; }

                _writeOff = _charges - _allowed;

                if (_allowed <= _charges)
                {
                    if (_isAllowedNull == false)
                    {
                        c1SinglePayment.SetData(e.Row, COL_WRITEOFF, _writeOff);
                    }
                }
            }
        }



        private void CalculateRunningBalance(int rowIndex)
        {
            bool _IsPaymentCorrectionMode = false;
            decimal _actualBalance = 0;
            decimal _newBalance = 0;

            decimal _currPayment = 0;
            decimal _currWriteOff = 0;
            decimal _currWithHold = 0;

            decimal _lastPaidAmt = 0;
            decimal _lastWOAmt = 0;
            decimal _lastWHAmt = 0;

            //bool _isNullCurrPayment = false;
            //bool _isNullCurrWriteOff = false;
            //bool _isNullCurrWithHold = false;
            //bool _isNullLastPaidAmt = false;
            //bool _isNullLastWOAmt = false;
            //bool _isNullLastWHAmt = false;

            try
            {
                if (c1SinglePayment.GetData(rowIndex, COL_ISCORRECTION) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_ISCORRECTION)).Trim() != "")
                { _IsPaymentCorrectionMode = Convert.ToBoolean(c1SinglePayment.GetData(rowIndex, COL_ISCORRECTION)); }

                if (c1SinglePayment.GetData(rowIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_PAYMENT)).Trim() != "")
                { _currPayment = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_PAYMENT)); }
                else { ;} //_isNullCurrPayment = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_LAST_PAYMENT)).Trim() != "")
                { _lastPaidAmt = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_LAST_PAYMENT)); }
                else { ;} //_isNullLastPaidAmt = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_WRITEOFF)).Trim() != "")
                { _currWriteOff = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_WRITEOFF)); }
                else { ;} //_isNullCurrWriteOff = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_WITHHOLD)).Trim() != "")
                { _currWithHold = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_WITHHOLD)); }
                else { ;}// _isNullCurrWithHold = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_LAST_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_LAST_WRITEOFF)).Trim() != "")
                { _lastWOAmt = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_LAST_WRITEOFF)); }
                else { ;}//_isNullCurrWriteOff = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_LAST_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_LAST_WITHHOLD)).Trim() != "")
                { _lastWHAmt = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_LAST_WITHHOLD)); }
                else { ;} //_isNullLastWHAmt = true; }

                if (c1SinglePayment.GetData(rowIndex, COL_LINE_DB_BALANCE) != null && Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_LINE_DB_BALANCE)).Trim() != "")
                { _actualBalance = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_LINE_DB_BALANCE)); }

                if (_IsPaymentCorrectionMode)
                {
                    _newBalance = (_actualBalance + _lastPaidAmt + _lastWOAmt + _lastWHAmt) - (_currPayment + _currWithHold + _currWriteOff);
                }
                else
                {
                    _newBalance = (_actualBalance) - (_currPayment + _currWithHold + _currWriteOff);
                }

                //if (_IsPaymentCorrectionMode)
                //{
                //    if (_currPayment != 0)
                //    {
                //        _newBalance = (_actualBalance + _lastPaidAmt + _lastWOAmt + _lastWHAmt) - (_currPayment + _currWithHold + _currWriteOff);
                //    }
                //    else
                //    {
                //        _newBalance = _actualBalance;
                //    }
                //}
                //else
                //{
                //    _newBalance = (_actualBalance) - (_currPayment + _currWithHold + _currWriteOff);
                //}

                c1SinglePayment.SetData(rowIndex, COL_BALANCE, _newBalance);
                CalculateTotals();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { }
        }



        private void CalculateTotals()
        {
            decimal _totalAllowed = 0;
            decimal _totalPayment = 0;
            decimal _totalWriteOff = 0;
            decimal _totalCopay = 0;
            decimal _totalDeductible = 0;
            decimal _totalCoInsurance = 0;
            decimal _totalWithHold = 0;
            decimal _totalBalance = 0;

            try
            {
                if (c1SinglePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(i, COL_ALLOWED) != null && c1SinglePayment.GetData(i, COL_ALLOWED).ToString() != null && c1SinglePayment.GetData(i, COL_ALLOWED).ToString().Trim().Length > 0)
                            {
                                _totalAllowed = _totalAllowed + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_ALLOWED)));
                            }
                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_PAYMENT).ToString() != null && c1SinglePayment.GetData(i, COL_PAYMENT).ToString().Trim().Length > 0)
                            {
                                _totalPayment = _totalPayment + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)));
                            }
                            if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString() != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim().Length > 0)
                            {
                                _totalWriteOff = _totalWriteOff + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                            }
                            if (c1SinglePayment.GetData(i, COL_COPAY) != null && c1SinglePayment.GetData(i, COL_COPAY).ToString() != null && c1SinglePayment.GetData(i, COL_COPAY).ToString().Trim().Length > 0)
                            {
                                _totalCopay = _totalCopay + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_COPAY)));
                            }
                            if (c1SinglePayment.GetData(i, COL_DEDUCTIBLE) != null && c1SinglePayment.GetData(i, COL_DEDUCTIBLE).ToString() != null && c1SinglePayment.GetData(i, COL_DEDUCTIBLE).ToString().Trim().Length > 0)
                            {
                                _totalDeductible = _totalDeductible + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)));
                            }
                            if (c1SinglePayment.GetData(i, COL_COINSURANCE) != null && c1SinglePayment.GetData(i, COL_COINSURANCE).ToString() != null && c1SinglePayment.GetData(i, COL_COINSURANCE).ToString().Trim().Length > 0)
                            {
                                _totalCoInsurance = _totalCoInsurance + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_COINSURANCE)));
                            }
                            if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString() != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim().Length > 0)
                            {
                                _totalWithHold = _totalWithHold + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                            }
                            if (c1SinglePayment.GetData(i, COL_BALANCE) != null && c1SinglePayment.GetData(i, COL_BALANCE).ToString() != null && c1SinglePayment.GetData(i, COL_BALANCE).ToString().Trim().Length > 0)
                            {
                                _totalBalance = _totalBalance + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_BALANCE)));
                            }
                        }
                    }
                }
                c1SinglePaymentTotal.SetData(0, COL_ALLOWED, _totalAllowed);
                c1SinglePaymentTotal.SetData(0, COL_PAYMENT, _totalPayment);
                c1SinglePaymentTotal.SetData(0, COL_WRITEOFF, _totalWriteOff);
                c1SinglePaymentTotal.SetData(0, COL_COPAY, _totalCopay);
                c1SinglePaymentTotal.SetData(0, COL_DEDUCTIBLE, _totalDeductible);
                c1SinglePaymentTotal.SetData(0, COL_COINSURANCE, _totalCoInsurance);
                c1SinglePaymentTotal.SetData(0, COL_WITHHOLD, _totalWithHold);
                c1SinglePaymentTotal.SetData(0, COL_BALANCE, _totalBalance);
            }
            catch
            {
            }
        }

        private decimal CalculateSinglePaymentTotal(int ColNumber)
        {
            decimal _result = 0;
            try
            {
                if (c1SinglePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(i, ColNumber) != null && c1SinglePayment.GetData(i, ColNumber).ToString() != null && c1SinglePayment.GetData(i, ColNumber).ToString().Trim().Length > 0)
                            {
                                _result = _result + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, ColNumber)));
                            }
                        }
                    }

                }
            }
            catch
            {
            }
            finally
            {
            }
            return _result;

        }

        private decimal CalculateMultiplePaymentTotal(int ColNumber)
        {
            decimal _result = 0;
            try
            {
                if (c1MultiplePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1MultiplePayment.Rows.Count - 1; i++)
                    {
                        if (c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1MultiplePayment.GetData(i, ColNumber) != null && c1MultiplePayment.GetData(i, ColNumber).ToString() != null && c1MultiplePayment.GetData(i, ColNumber).ToString().Trim().Length > 0)
                            {
                                _result = _result + Convert.ToDecimal(Convert.ToString(c1MultiplePayment.GetData(i, ColNumber)));
                            }
                        }
                    }

                }
            }
            catch
            {
            }
            finally
            {
            }
            return _result;
        }

        #endregion
    }
}
