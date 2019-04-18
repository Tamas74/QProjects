using System;
using System.Collections.Generic;
using System.Text;

namespace gloBilling
{
    public static class gloC1FlexStyle
    {
        static System.Drawing.Font regularFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
        static System.Drawing.Font boldFont =gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = regularFont;
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73,125);

            // Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Alternate.Font = regularFont;
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = boldFont;
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            // Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = regularFont;
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Focus.Font = boldFont;
            FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = regularFont;
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255,197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = regularFont;
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = regularFont;
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = regularFont;


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            FlexGrid.ShowCellLabels = blnShowCellLabels;
        }


        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = regularFont;
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = regularFont;
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = boldFont;
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            // Highlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = regularFont;
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = regularFont;
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = regularFont;
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = regularFont;
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = regularFont;


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

           
        }

        public static void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try 
	        {	
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;
                
                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0; 

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol; 
                    
                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if ((oGrid.Cols.Count > nCol) && (oGrid.Rows.Count > nRow))
                    {
                        if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                        {
                            if (nRow > 0)
                            {
                                if (oGrid.GetData(nRow, nCol) != null)
                                {
                                    if (oGrid.Cols[nCol].DataType != null && oGrid.Cols[nCol].DataType.Name == "DateTime")
                                    {
                                        if (oGrid.GetData(nRow, nCol) != null && oGrid.GetData(nRow, nCol) != DBNull.Value && Convert.ToString(oGrid.GetData(nRow, nCol)) != "")
                                        {
                                            DateTime dtText = Convert.ToDateTime(oGrid.GetData(nRow, nCol));
                                            sText = String.Format("{0:MM/dd/yyyy}", dtText);
                                        }
                                    }
                                    else if (oGrid.Cols[nCol].Caption.ToLower().Trim() == "unit")
                                    {
                                        if (oGrid.GetData(nRow, nCol) != null && oGrid.GetData(nRow, nCol) != DBNull.Value && Convert.ToString(oGrid.GetData(nRow, nCol)) != "")
                                        {
                                            decimal dText = Convert.ToDecimal(oGrid.GetData(nRow, nCol));
                                            //Added Format for Tooltip in 6031
                                            sText = dText.ToString("###0.####");
                                        }
                                    }
                                    else
                                    {
                                        sText = oGrid.GetData(nRow, nCol).ToString();
                                    }
                                }
                                colsize = oGrid.Cols[nCol].WidthDisplay;
                            }
                            System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                            stringsize = oGrp.MeasureString(sText, myfont);
                            //Code Review Changes: Dispose Graphics object
                            oGrp.Dispose();
                            if (stringsize.Width > colsize)
                            {
                                oC1ToolTip.SetToolTip(oGrid, sText);
                            }
                            else
                            {
                                oC1ToolTip.SetToolTip(oGrid, "");
                            }
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }
                    }
                    else
                    {
                        oC1ToolTip.SetToolTip(oGrid, "");
                    }
                }
	        }
	        catch (Exception ex)
	        {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
		        
	        }
        }

        public static void ShowToolTipForBillingServiceLine(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                if (oGrid.Cols[nCol].DataType == typeof(System.DateTime))
                                {
                                    DateTime dtText = Convert.ToDateTime(oGrid.GetData(nRow, nCol));
                                    sText = String.Format("{0:MM/dd/yyyy}", dtText);
                                }
                                else
                                {
                                    sText = oGrid.GetData(nRow, nCol).ToString();
                                } 
                            }
                            colsize = oGrid.Cols[nCol].WidthDisplay;
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        oC1ToolTip.SetToolTip(oGrid, sText);

                        //if (stringsize.Width > colsize)
                        //{
                        //    oC1ToolTip.SetToolTip(oGrid, sText);
                        //}
                        //else
                        //{
                        //    oC1ToolTip.SetToolTip(oGrid, "");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        public static void ShowToolTipForLineBreak(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                //on grid Tooltip show only date not time  02/05/2011 Mahesh nawal
                                if (oGrid.Cols[nCol].DataType == System.Type.GetType("System.DateTime"))
                                {
                                    DateTime dtText = Convert.ToDateTime(oGrid.GetData(nRow, nCol));
                                    sText = String.Format("{0:MM/dd/yyyy}", dtText);
                                }
                                else if (oGrid.Cols[nCol].Caption.ToLower().Trim() == "units")
                                {
                                    decimal dText = Convert.ToDecimal(oGrid.GetData(nRow, nCol));
                                    //Added Format for Tooltip in 6031
                                    sText = dText.ToString("###0.####");
                                }
                                else
                                {
                                    sText = oGrid.GetData(nRow, nCol).ToString();
                                }

                                //sText = oGrid.GetData(nRow, nCol).ToString();
                            }
                           
                            colsize = oGrid.Cols[nCol].WidthDisplay;
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        oC1ToolTip.SetToolTip(oGrid, sText);

                        if (sText.Contains("\r\n"))
                        {
                            sText = sText.ToString().Replace("\r\n", " ");
                            oC1ToolTip.SetToolTip(oGrid, sText);
 
                        }
                        else if (stringsize.Width > colsize)
                        {
                           
                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 

            }
        }

        public static void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation,bool getnextcolumn)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    if (getnextcolumn == true)
                    {
                        nCol = oGrid.MouseCol + 1;
                    }
                    else
                    {
                        nCol = oGrid.MouseCol;
                    }

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {

                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                if (oGrid.Cols[nCol].DataType == typeof(System.DateTime))
                                {
                                    DateTime dtText = Convert.ToDateTime(oGrid.GetData(nRow, nCol));
                                    sText = String.Format("{0:MM/dd/yyyy}", dtText);
                                    //sText = Convert.ToDateTime(oGrid.GetData(nRow, nCol).ToString()).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    sText = oGrid.GetData(nRow, nCol).ToString();
                                }
                            }
                            if (getnextcolumn == true)
                            {
                                colsize = oGrid.Cols[nCol - 1].WidthDisplay;
                            }
                            else
                            {
                                colsize = oGrid.Cols[nCol].WidthDisplay;
                            }
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        if (stringsize.Width > colsize)
                        {
                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 

            }
        }

        public static void ShowToolTipForBillingServiceLine(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation, bool getnextcolumn)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    if (getnextcolumn == true)
                    {
                        nCol = oGrid.MouseCol + 1;
                    }
                    else
                    {
                        nCol = oGrid.MouseCol;
                    }

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {

                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                sText = oGrid.GetData(nRow, nCol).ToString();
                            }
                            if (getnextcolumn == true)
                            {
                                colsize = oGrid.Cols[nCol - 1].WidthDisplay;
                            }
                            else
                            {
                                colsize = oGrid.Cols[nCol].WidthDisplay;
                            }
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        oC1ToolTip.SetToolTip(oGrid, sText);

                        //if (stringsize.Width > colsize)
                        //{
                        //    oC1ToolTip.SetToolTip(oGrid, sText);
                        //}
                        //else
                        //{
                        //    oC1ToolTip.SetToolTip(oGrid, "");
                        //}
                    }
                    else
                    {
                        oC1ToolTip.SetToolTip(oGrid, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

    }
}