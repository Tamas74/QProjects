using System;

namespace gloTaskMail
{
    public static class gloC1FlexStyle
    {
        //to avoid new every time we added following two lines.
        //this changes suggested by Laxman Sir.
        static System.Drawing.Font NormalFont = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,(byte) 0);
        static System.Drawing.Font BoldFont = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);        
        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = NormalFont;
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73,125);

            // Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Alternate.Font = NormalFont;
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = BoldFont;
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            // Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = NormalFont;
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Focus.Font = BoldFont;
            FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = NormalFont;
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255,197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = NormalFont;
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = NormalFont;
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = NormalFont;


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            FlexGrid.ShowCellLabels = blnShowCellLabels;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
		        
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }
        //Added on 20100701
        public static void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation,string TaskPriority)
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
                                if (sText == "1")
                                {
                                    sText = "High Priority";
                                }
                                else if (sText == "2")
                                {
                                    sText = "Normal Priority";
                                }
                                else if (sText == "3")
                                {
                                    sText = "Low Priority";
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
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }
        //End

    }
}
