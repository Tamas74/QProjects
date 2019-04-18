using System;
using System.Collections.Generic;
using System.Text;

namespace gloCardScanning
{
    public static class gloC1FlexStyle
    {
        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,(byte) 0);
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73,125);

            // Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Alternate.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            // Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Focus.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255,197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            FlexGrid.ShowCellLabels = blnShowCellLabels;
        }

        public static void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            System.Drawing.Font myfont = oGrid.Font;
            try
            {
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                sText = oGrid.GetData(nRow, nCol).ToString();
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
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            { if (myfont != null) { myfont = null; } }
        }

        public static void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation,bool getnextcolumn)
        {
            System.Drawing.Font myfont = oGrid.Font;
            try
            {
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    nRow = oGrid.MouseRow;
                    if (getnextcolumn == true)
                    {
                        nCol = oGrid.MouseCol + 1;
                    }
                    else
                    {
                        nCol = oGrid.MouseCol;
                    }

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
                sText = null;
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            { if (myfont != null) {myfont = null; } }
        }

    }
}
