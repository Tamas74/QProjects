using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gloCommunity.Classes
{
    
   public class gloC1FlexStyle
    {
        public void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels = false)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(240)), Convert.ToInt32(Convert.ToByte(247)), Convert.ToInt32(Convert.ToByte(255)));
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            //' Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(240)), Convert.ToInt32(Convert.ToByte(247)), Convert.ToInt32(Convert.ToByte(255)));
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Normal.Font = gloGlobal.clsgloFont.gFont;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(31)), Convert.ToInt32(Convert.ToByte(73)), Convert.ToInt32(Convert.ToByte(125)));

            //' Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(222)), Convert.ToInt32(Convert.ToByte(231)), Convert.ToInt32(Convert.ToByte(250)));
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Fixed.Font =gloGlobal.clsgloFont.gFont;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(31)), Convert.ToInt32(Convert.ToByte(73)), Convert.ToInt32(Convert.ToByte(125)));
            //shubhangi 20100407 TO SET ALLIGNMENT OF HEADER TO LEFT
            if (FlexGrid.Rows.Count > 0)
            {
                FlexGrid.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            }



            //' Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(86)), Convert.ToInt32(Convert.ToByte(126)), Convert.ToInt32(Convert.ToByte(211)));
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Fixed.ForeColor = Color.White;

            //' Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(197)), Convert.ToInt32(Convert.ToByte(108)));
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Highlight.Font = gloGlobal.clsgloFont.gFont;
            //'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Highlight.ForeColor = Color.Black;

            //' Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(224)), Convert.ToInt32(Convert.ToByte(160)));
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Focus.Font = gloGlobal.clsgloFont.gFont_BOLD;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Focus.ForeColor = Color.Black;

            //' EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(31)), Convert.ToInt32(Convert.ToByte(73)), Convert.ToInt32(Convert.ToByte(125)));
            FlexGrid.Styles.Editor.Font = gloGlobal.clsgloFont.gFont;
            //'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Editor.ForeColor = Color.Black;

            //' Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(197)), Convert.ToInt32(Convert.ToByte(108)));
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Search.Font = gloGlobal.clsgloFont.gFont;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Search.ForeColor = Color.White;

            //' Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(224)), Convert.ToInt32(Convert.ToByte(160)));
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.Frozen.Font = gloGlobal.clsgloFont.gFont;
            //'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            FlexGrid.Styles.Frozen.ForeColor = Color.Black;

            //' New Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(240)), Convert.ToInt32(Convert.ToByte(247)), Convert.ToInt32(Convert.ToByte(255)));
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(31)), Convert.ToInt32(Convert.ToByte(73)), Convert.ToInt32(Convert.ToByte(125)));
            FlexGrid.Styles.NewRow.Font = gloGlobal.clsgloFont.gFont;
            //' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            //' Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = Color.White;
            //'System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(159)), Convert.ToInt32(Convert.ToByte(181)), Convert.ToInt32(Convert.ToByte(221)));
            //'System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(31)), Convert.ToInt32(Convert.ToByte(73)), Convert.ToInt32(Convert.ToByte(125)));




            FlexGrid.ShowCellLabels = blnShowCellLabels;

        }

        //public void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        //{
        //    try
        //    {
        //        Font myFont = oGrid.Font;
        //        SizeF stringsize = default(SizeF);
        //        int colsize = 0;
        //        string sText = "";
        //        int nRow = 0;
        //        int nCol = 0;

        //        if (oGrid.MouseCol > -1 & oGrid.MouseRow > -1)
        //        {
        //            oC1ToolTip.Font = myFont;
        //            oC1ToolTip.MaximumWidth = 400;

        //            nRow = oGrid.MouseRow;
        //            nCol = oGrid.MouseCol;

        //            //And nCol > 0 Then
        //            if (nRow > 0)
        //            {
        //                if ((oGrid.GetData(nRow, nCol) != null))
        //                {
        //                    //sText = oGrid.GetData(nRow, nCol).ToString()
        //                    //TO RESOLVED 8821
        //                    sText = oGrid.GetData(nRow, nCol).ToString();//[nRow, nCol];
        //                }
        //                colsize = oGrid.Cols[nCol].WidthDisplay;

        //            }
        //            Graphics oGrp = oGrid.CreateGraphics();
        //            int chars = 0;
        //            int lines = 0;
        //            stringsize = oGrp.MeasureString(sText.ToString(), myFont, SizeF.Empty, StringFormat.GenericDefault(), chars, lines);

        //            //' oGrid.GetCellRect(nRow, nCol).Height
        //            //If stringsize.Width > colsize Or lines > 1 And oGrid.GetCellRect(nRow, nCol).Height < (19 * lines) Then
        //            if (stringsize.Width > colsize | lines > 1)
        //            {
        //                //oC1ToolTip.SetToolTip(oGrid, sText.ToString())
        //                //'TO RESOLVED 8821
        //                oC1ToolTip.SetToolTip(oGrid, sText);
        //            }
        //            else
        //            {
        //                oC1ToolTip.SetToolTip(oGrid, "");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
