using System;
using System.Collections.Generic;
using System.Text;
using gloEDocumentV3.Enumeration;
using gloEDocumentV3.DocumentContextMenu;
using System.Collections;

namespace gloEDocumentV3
{
    public static partial class eDocV3Manipulation
    {

        #region "Constant Variables"
        internal const int COL_DOCUMENTID = 0;
        internal const int COL_CONATINERS = 1;
        internal const int COL_NODENAME = 2;
        internal const int COL_DOCUMENTNAME = 3;
        internal const int COL_CATEGORY = 4;
        internal const int COL_PATIENTID = 5;
        internal const int COL_YEAR = 6;
        internal const int COL_MONTH = 7;
        internal const int COL_PAGENUMBERS = 8;
        internal const int COL_CREATEDDATETIME = 9;
        internal const int COL_ISACKNOWLEDGE = 10;
        internal const int COL_HASNOTES = 11;
        internal const int COL_CLINCID = 12;
        internal const int COL_COLTYPE = 13;
        internal const int COL_FLAG_DATE = 14;
        internal const int COL_FLAG_ACKNOWLEDGE = 15;
        internal const int COL_FLAG_NOTES = 16;
        internal const int COL_FLAG_OTHER = 17;
        internal const int COL_TOTAL = 18;
        #endregion

        //static string ErrorMessage = "";
       // static bool HasError = false;

        internal static System.Windows.Forms.ImageList oPageImageList = new System.Windows.Forms.ImageList();

        #region "Fill Categories"

        internal static void FillCategories(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, bool ShowCreatedDate)
        {
            //ErrorMessage = "";
            //HasError = false;

            //if (oFillGrid != null)
            //{
            //    #region "Design Grid"
            //    oFillGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
            //    oFillGrid.Rows.Count = 0;
            //    oFillGrid.Rows.Fixed = 0;
            //    oFillGrid.Cols.Count = COL_TOTAL;
            //    oFillGrid.Cols.Fixed = 0;

            //    oFillGrid.Cols[COL_CONATINERID].Width = 0;
            //    oFillGrid.Cols[COL_DOCUMENTID].Width = 0;
            //    if (ShowCreatedDate == true)
            //    {
            //        oFillGrid.Cols[COL_NODENAME].Width = oFillGrid.Width - 120;
            //    }
            //    else
            //    {
            //        oFillGrid.Cols[COL_NODENAME].Width = oFillGrid.Width - 60;
            //    }
            //    oFillGrid.Cols[COL_DOCUMENTNAME].Width = 0;
            //    oFillGrid.Cols[COL_CATEGORY].Width = 0;
            //    oFillGrid.Cols[COL_PATIENTID].Width = 0;
            //    oFillGrid.Cols[COL_YEAR].Width = 0;
            //    oFillGrid.Cols[COL_MONTH].Width = 0;
            //    oFillGrid.Cols[COL_PAGENUMBERS].Width = 0;
            //    oFillGrid.Cols[COL_CREATEDDATETIME].Width = 0;
            //    oFillGrid.Cols[COL_ISACKNOWLEDGE].Width = 0;
            //    oFillGrid.Cols[COL_HASNOTES].Width = 0;
            //    oFillGrid.Cols[COL_CLINCID].Width = 0;
            //    oFillGrid.Cols[COL_COLTYPE].Width = 0;
            //    oFillGrid.Cols[COL_FLAG_ACKNOWLEDGE].Width = 20;
            //    oFillGrid.Cols[COL_FLAG_NOTES].Width = 20;
            //    if (ShowCreatedDate == true)
            //    {
            //        oFillGrid.Cols[COL_FLAG_DATE].Width = 60;
            //    }
            //    else
            //    {
            //        oFillGrid.Cols[COL_FLAG_DATE].Width = 0;
            //    }
            //    oFillGrid.Cols[COL_FLAG_OTHER].Width = 0;


            //    oFillGrid.Cols[COL_CONATINERID].Visible = false;
            //    oFillGrid.Cols[COL_DOCUMENTID].Visible = false;
            //    oFillGrid.Cols[COL_NODENAME].Visible = true;
            //    oFillGrid.Cols[COL_DOCUMENTNAME].Visible = false;
            //    oFillGrid.Cols[COL_CATEGORY].Visible = false;
            //    oFillGrid.Cols[COL_PATIENTID].Visible = false;
            //    oFillGrid.Cols[COL_YEAR].Visible = false;
            //    oFillGrid.Cols[COL_MONTH].Visible = false;
            //    oFillGrid.Cols[COL_PAGENUMBERS].Visible = false;
            //    oFillGrid.Cols[COL_CREATEDDATETIME].Visible = false;
            //    oFillGrid.Cols[COL_ISACKNOWLEDGE].Visible = false;
            //    oFillGrid.Cols[COL_HASNOTES].Visible = false;
            //    oFillGrid.Cols[COL_CLINCID].Visible = false;
            //    oFillGrid.Cols[COL_COLTYPE].Visible = false;
            //    oFillGrid.Cols[COL_FLAG_ACKNOWLEDGE].Visible = true;
            //    oFillGrid.Cols[COL_FLAG_NOTES].Visible = true;
            //    oFillGrid.Cols[COL_FLAG_DATE].Visible = ShowCreatedDate;
            //    oFillGrid.Cols[COL_FLAG_OTHER].Visible = false;

            //    oFillGrid.Cols[COL_NODENAME].DataType = typeof(System.String);

            //    for (int i = 0; i <= oFillGrid.Cols.Count - 1; i++)
            //    {
            //        oFillGrid.Cols[i].AllowEditing = false;
            //    }

            //    oFillGrid.Cols[COL_NODENAME].AllowEditing = true;

            //    C1.Win.C1FlexGrid.CellStyle ostyle_Category = oFillGrid.Styles.Add("style_Category");
            //    ostyle_Category.Font = new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocumentAdmin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    ostyle_Category.ForeColor = Color.DarkBlue;
            //    ostyle_Category.BackColor = Color.FromArgb(232, 237, 243);
            //    //ostyle_Category.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
            //    //ostyle_Category.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
            //    ostyle_Category.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;


            //    C1.Win.C1FlexGrid.CellStyle ostyle_Month = oFillGrid.Styles.Add("style_Month");
            //    ostyle_Month.Font = new System.Drawing.Font(gloEDocumentAdmin.gFontName, gloEDocumentAdmin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    ostyle_Month.ForeColor = Color.DarkSlateBlue;
            //    ostyle_Month.BackColor = Color.FromArgb(250, 250, 250);
            //    //ostyle_Month.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
            //    //ostyle_Month.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
            //    ostyle_Month.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;

            //    C1.Win.C1FlexGrid.CellStyle ostyle_Document_NotAcknowledge = oFillGrid.Styles.Add("style_Document_NotAcknowledge");
            //    ostyle_Document_NotAcknowledge.Font = new System.Drawing.Font(gloEDocumentAdmin.gFontName, gloEDocumentAdmin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    ostyle_Document_NotAcknowledge.ForeColor = Color.Black;
            //    ostyle_Document_NotAcknowledge.BackColor = Color.FromArgb(255, 255, 255);
            //    //ostyle_Document_NotAcknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
            //    //ostyle_Document_NotAcknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
            //    ostyle_Document_NotAcknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;

            //    C1.Win.C1FlexGrid.CellStyle ostyle_Document_Acknowledge = oFillGrid.Styles.Add("style_Document_Acknowledge");
            //    ostyle_Document_Acknowledge.Font = new System.Drawing.Font(gloEDocumentAdmin.gFontName, gloEDocumentAdmin.gFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    ostyle_Document_Acknowledge.ForeColor = Color.Black;
            //    ostyle_Document_Acknowledge.BackColor = Color.FromArgb(255, 255, 255);
            //    //ostyle_Document_Acknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
            //    //ostyle_Document_Acknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
            //    ostyle_Document_Acknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;

            //    #endregion

            //    gloEDocumentV3.Common.Categories oCategories = new gloEDocumentV3.Common.Categories();
            //    gloEDocument.eDocManager.eDocGetList oList = new gloEDocument.eDocManager.eDocGetList();
            //    oCategories = oList.GetCategories(gloEDocumentAdmin.gClinicID);
            //    try
            //    {
            //        if (oCategories != null)
            //        {
            //            if (oCategories.Count > 0)
            //            {
            //                oFillGrid.Tree.Column = COL_NODENAME;
            //                oFillGrid.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
            //                oFillGrid.Tree.Indent = 15;
            //                oFillGrid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes;

            //                for (int i = 0; i <= oCategories.Count - 1; i++)
            //                {
            //                    oFillGrid.Rows.Add();
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].ImageAndText = true;
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].Height = 23;
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].IsNode = true;
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].Style = oFillGrid.Styles["style_Category"];
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].Node.Level = 0;
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].Node.Image = global::gloEDocument.Properties.Resources.Category;
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].Node.Data = oCategories[i].ToString();
            //                    //Sagar Ghodke - 20080714 - Changes for Bug Fixes Category(Document Name) made uneditable
            //                    oFillGrid.Rows[oFillGrid.Rows.Count - 1].AllowEditing = false;
            //                    //
            //                    oFillGrid.SetData(oFillGrid.Rows.Count - 1, COL_COLTYPE, gloEDocument.enum_DocumentColumnType.Category);
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ErrorMessage = ex.Message;
            //        HasError = true;
            //    }
            //    finally
            //    {
            //        oCategories.Dispose();
            //        oList.Dispose();
            //    }
            //}
        }

        #endregion
    }
}