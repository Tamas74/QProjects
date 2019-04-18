using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEDocumentV3.Enumeration;
using System.Collections;
namespace gloEDocumentV3.Forms
{
    public partial class frmEDocumentViewer
    {
        string ErrorMessage = "";
       // bool HasError = false;

        private int _PatientStripMAXHeight;

        public int PatientStripMAXHeight
        {
            get { return _PatientStripMAXHeight; }
            set { _PatientStripMAXHeight = value; }
        }

        private int _PatientStripMINHeight;

        public int PatientStripMINHeight
        {
            get { return _PatientStripMINHeight; }
            set { _PatientStripMINHeight = value; }
        }
	
        
        #region "Constant Variables"
        internal const int COL_DOCUMENTID = 0;
        internal const int COL_CONATINERS = 1;
        internal const int COL_NODENAME = 2;
        internal const int COL_DOCUMENTNAME = 3;
        internal const int COL_CATEGORY = 4;
        internal const int COL_CATEGORYID = 5;
        internal const int COL_CATEGORYGROUPID = 6;
        internal const int COL_CATEGORYLEVELID = 7;
        internal const int COL_PATIENTID = 8;
        internal const int COL_YEAR = 9;
        internal const int COL_MONTH = 10;
        internal const int COL_PAGENUMBERS = 11;
        internal const int COL_CREATEDDATETIME = 12;
        internal const int COL_ISACKNOWLEDGE = 13;
        internal const int COL_HASNOTES = 14;
        internal const int COL_CLINCID = 15;
        internal const int COL_COLTYPE = 16;
        internal const int COL_FLAG_DATE = 17;
        internal const int COL_FLAG_ACKNOWLEDGE = 18;
        internal const int COL_FLAG_NOTES = 19;
        internal const int COL_FLAG_OTHER = 20;
        internal const int COL_SUBCATEGORY = 21;
        internal const int COL_TOTAL = 22;
        #endregion
        
        
        #region "Fill Categories"


        #region "Dhruv 20100626-> Fill Categories"

   
        private void FillCategories(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, bool ShowCreatedDate, Int64 ExternalDocumentID, Enumeration.enum_OpenExternalSource ExternalSource)
        {
            ErrorMessage = "";
          //  HasError = false;

            if (oFillGrid != null)
            {
                #region "Design Grid"
                oFillGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                oFillGrid.Rows.Count = 0;
                oFillGrid.Rows.Fixed = 0;
                oFillGrid.Cols.Count = COL_TOTAL;
                oFillGrid.Cols.Fixed = 0;

                oFillGrid.Cols[COL_CONATINERS].Width = 0;
                oFillGrid.Cols[COL_DOCUMENTID].Width = 0;
                if (ShowCreatedDate == true)
                {
                    oFillGrid.Cols[COL_NODENAME].Width = oFillGrid.Width - 120;
                }
                else
                {
                    oFillGrid.Cols[COL_NODENAME].Width = oFillGrid.Width - 60;
                }
                oFillGrid.Cols[COL_DOCUMENTNAME].Width = 0;
                oFillGrid.Cols[COL_CATEGORY].Width = 0;
                oFillGrid.Cols[COL_CATEGORYID].Width = 0;
                oFillGrid.Cols[COL_CATEGORYGROUPID].Width = 0;
                oFillGrid.Cols[COL_CATEGORYLEVELID].Width = 0;
                oFillGrid.Cols[COL_PATIENTID].Width = 0;
                oFillGrid.Cols[COL_YEAR].Width = 0;
                oFillGrid.Cols[COL_MONTH].Width = 0;
                oFillGrid.Cols[COL_PAGENUMBERS].Width = 0;
                oFillGrid.Cols[COL_CREATEDDATETIME].Width = 0;
                oFillGrid.Cols[COL_ISACKNOWLEDGE].Width = 0;
                oFillGrid.Cols[COL_HASNOTES].Width = 0;
                oFillGrid.Cols[COL_CLINCID].Width = 0;
                oFillGrid.Cols[COL_COLTYPE].Width = 0;
                oFillGrid.Cols[COL_FLAG_ACKNOWLEDGE].Width = 20;
                oFillGrid.Cols[COL_FLAG_NOTES].Width = 20;
                oFillGrid.Cols[COL_SUBCATEGORY].Width = 0;
                if (ShowCreatedDate == true)
                {
                    oFillGrid.Cols[COL_FLAG_DATE].Width = 60;
                }
                else
                {
                    oFillGrid.Cols[COL_FLAG_DATE].Width = 0;
                }
                oFillGrid.Cols[COL_FLAG_OTHER].Width = 0;


                oFillGrid.Cols[COL_CONATINERS].Visible = false;
                oFillGrid.Cols[COL_DOCUMENTID].Visible = false;
                oFillGrid.Cols[COL_NODENAME].Visible = true;
                oFillGrid.Cols[COL_DOCUMENTNAME].Visible = false;
                oFillGrid.Cols[COL_CATEGORY].Visible = false;
                oFillGrid.Cols[COL_PATIENTID].Visible = false;
                oFillGrid.Cols[COL_YEAR].Visible = false;
                oFillGrid.Cols[COL_MONTH].Visible = false;
                oFillGrid.Cols[COL_PAGENUMBERS].Visible = false;
                oFillGrid.Cols[COL_CREATEDDATETIME].Visible = false;
                oFillGrid.Cols[COL_ISACKNOWLEDGE].Visible = false;
                oFillGrid.Cols[COL_HASNOTES].Visible = false;
                oFillGrid.Cols[COL_CLINCID].Visible = false;
                oFillGrid.Cols[COL_COLTYPE].Visible = false;
                oFillGrid.Cols[COL_FLAG_ACKNOWLEDGE].Visible = true;
                oFillGrid.Cols[COL_FLAG_NOTES].Visible = true;
                oFillGrid.Cols[COL_FLAG_DATE].Visible = ShowCreatedDate;
                oFillGrid.Cols[COL_FLAG_OTHER].Visible = false;
                oFillGrid.Cols[COL_SUBCATEGORY].Visible = false;

                oFillGrid.Cols[COL_NODENAME].DataType = typeof(System.String);
                oFillGrid.Cols[COL_CONATINERS].DataType = typeof(gloEDocumentV3.Document.eBaseContainers);

                oFillGrid.Cols[COL_DOCUMENTID].DataType = typeof(System.Int64);
                oFillGrid.Cols[COL_CATEGORYID].DataType = typeof(System.Int32);
                oFillGrid.Cols[COL_CATEGORYGROUPID].DataType = typeof(System.Int32);
                oFillGrid.Cols[COL_CATEGORYLEVELID].DataType = typeof(System.Int32);
                oFillGrid.Cols[COL_PATIENTID].DataType = typeof(System.Int64);


                for (int i = 0; i <= oFillGrid.Cols.Count - 1; i++)
                {
                    oFillGrid.Cols[i].AllowEditing = false;
                }

                oFillGrid.Cols[COL_NODENAME].AllowEditing = true;

                C1.Win.C1FlexGrid.CellStyle ostyle_Category; //= oFillGrid.Styles.Add("style_Category");
                try
                {
                    if (oFillGrid.Styles.Contains("style_Category"))
                    {
                        ostyle_Category = oFillGrid.Styles["style_Category"];
                    }
                    else
                    {
                        ostyle_Category = oFillGrid.Styles.Add("style_Category");
                        ostyle_Category.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        {
                            ostyle_Category.ForeColor = Color.White; //Color.DarkBlue;
                            ostyle_Category.BackColor = Color.FromArgb(51, 97, 175);
                        }
                        else
                        {
                            ostyle_Category.ForeColor = Color.DarkBlue;
                            ostyle_Category.BackColor = Color.FromArgb(232, 237, 243);
                        }
                       
                        //ostyle_Category.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                        //ostyle_Category.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                        ostyle_Category.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                    }

                }
                catch
                {
                    ostyle_Category = oFillGrid.Styles.Add("style_Category");
                    ostyle_Category.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        ostyle_Category.ForeColor = Color.White; //Color.DarkBlue;
                        ostyle_Category.BackColor = Color.FromArgb(51, 97, 175);
                    }
                    else
                    {
                        ostyle_Category.ForeColor = Color.DarkBlue;
                        ostyle_Category.BackColor = Color.FromArgb(232, 237, 243);
                    }
                    //ostyle_Category.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                    //ostyle_Category.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                    ostyle_Category.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                }



                C1.Win.C1FlexGrid.CellStyle ostyle_Month;//= oFillGrid.Styles.Add("style_Month");
                try
                {
                    if (oFillGrid.Styles.Contains("style_Month"))
                    {
                        ostyle_Month = oFillGrid.Styles["style_Month"];
                    }
                    else
                    {
                        ostyle_Month = oFillGrid.Styles.Add("style_Month");
                        ostyle_Month.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        ostyle_Month.ForeColor = Color.DarkSlateBlue;
                        ostyle_Month.BackColor = Color.FromArgb(250, 250, 250);
                        //ostyle_Month.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                        //ostyle_Month.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                        ostyle_Month.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                    }

                }
                catch
                {
                    ostyle_Month = oFillGrid.Styles.Add("style_Month");
                    ostyle_Month.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ostyle_Month.ForeColor = Color.DarkSlateBlue;
                    ostyle_Month.BackColor = Color.FromArgb(250, 250, 250);
                    //ostyle_Month.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                    //ostyle_Month.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                    ostyle_Month.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                }
              
               

                C1.Win.C1FlexGrid.CellStyle ostyle_Document_NotAcknowledge;// = oFillGrid.Styles.Add("style_Document_NotAcknowledge");
                try
                {
                    if (oFillGrid.Styles.Contains("style_Document_NotAcknowledge"))
                    {
                        ostyle_Document_NotAcknowledge = oFillGrid.Styles["style_Document_NotAcknowledge"];
                    }
                    else
                    {
                        ostyle_Document_NotAcknowledge = oFillGrid.Styles.Add("style_Document_NotAcknowledge");
                        ostyle_Document_NotAcknowledge.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        ostyle_Document_NotAcknowledge.ForeColor = Color.Black;
                        ostyle_Document_NotAcknowledge.BackColor = Color.FromArgb(255, 255, 255);
                        //ostyle_Document_NotAcknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                        //ostyle_Document_NotAcknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                        ostyle_Document_NotAcknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                    }

                }
                catch
                {
                    ostyle_Document_NotAcknowledge = oFillGrid.Styles.Add("style_Document_NotAcknowledge");
                    ostyle_Document_NotAcknowledge.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ostyle_Document_NotAcknowledge.ForeColor = Color.Black;
                    ostyle_Document_NotAcknowledge.BackColor = Color.FromArgb(255, 255, 255);
                    //ostyle_Document_NotAcknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                    //ostyle_Document_NotAcknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                    ostyle_Document_NotAcknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                }
              

                C1.Win.C1FlexGrid.CellStyle ostyle_Document_Acknowledge;// = oFillGrid.Styles.Add("style_Document_Acknowledge");
                try
                {
                    if (oFillGrid.Styles.Contains("style_Document_Acknowledge"))
                    {
                        ostyle_Document_Acknowledge = oFillGrid.Styles["style_Document_Acknowledge"];
                    }
                    else
                    {
                        ostyle_Document_Acknowledge = oFillGrid.Styles.Add("style_Document_Acknowledge");
                        ostyle_Document_Acknowledge.Font = gloGlobal.clsgloFont.gFont_SMALL;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        ostyle_Document_Acknowledge.ForeColor = Color.Black;
                        ostyle_Document_Acknowledge.BackColor = Color.FromArgb(255, 255, 255);
                        //ostyle_Document_Acknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                        //ostyle_Document_Acknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                        ostyle_Document_Acknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                    }

                }
                catch
                {
                    ostyle_Document_Acknowledge = oFillGrid.Styles.Add("style_Document_Acknowledge");
                    ostyle_Document_Acknowledge.Font = gloGlobal.clsgloFont.gFont_SMALL;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ostyle_Document_Acknowledge.ForeColor = Color.Black;
                    ostyle_Document_Acknowledge.BackColor = Color.FromArgb(255, 255, 255);
                    //ostyle_Document_Acknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                    //ostyle_Document_Acknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                    ostyle_Document_Acknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                }
            

                #endregion

                #region "Fill Categories"
                //Commented BY Rahul Patel on 27-10-2010
                //for changing the connection string for DMS database
                //gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _strSQL = "";
                DataTable oDataTable = null;
                try
                {

                    if (oDB != null)
                    {
                        oDB.Connect(false);

                        if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.None || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.RxMeds || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.IntuitMessage || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.SecureMessage || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Amedments || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder)
                        {
                            _strSQL = "SELECT CategoryId,CategoryName,isnull(GroupId,0) as GroupId,isnull(LevelId,0) as LevelId  FROM eDocument_Category_V3 WITH(NOLOCK) WHERE ClinicID = " + gloEDocV3Admin.gClinicID + " " +
                                " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY LevelId,GroupId,CategoryName";
                        }
                        else if(ExternalSource == enum_OpenExternalSource.RCM)
                        {
                            _strSQL = "SELECT CategoryId,CategoryName,isnull(GroupId,0) as GroupId,isnull(LevelId,0) as LevelId  FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + gloEDocV3Admin.gClinicID + " " +
                                " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY LevelId,GroupId,CategoryName";
                        }
                        else
                        {
                            if(ExternalSource ==gloEDocumentV3 .Enumeration .enum_OpenExternalSource .Immunization )
                            {
                                 string _ImmunizationCategory = "";
                                using (eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList())
                                {
                                    _ImmunizationCategory = oList.GetImmunizationCateory();
                                }
                                //oList.Dispose();

                                _strSQL = "SELECT DISTINCT CategoryId,CategoryName,isnull(GroupId,0) as GroupId,isnull(LevelId,0) as LevelId FROM eDocument_Category_V3 WITH(NOLOCK) WHERE ClinicID = " + gloEDocV3Admin.gClinicID + " " +
                                " AND CategoryName = '" + _ImmunizationCategory + "' AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY LevelId,GroupId,CategoryName";

                            }
                      
                        else
                        {
                            
                            if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.AdvanceDirective)
                            {
                                string _AdvanceDirectiveCategory = "";
                                using (eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList())
                                {
                                    _AdvanceDirectiveCategory = oList.GetAdvanceDirectiveCateory();
                                }
                                //oList.Dispose();

                                _strSQL = "SELECT DISTINCT CategoryId,CategoryName,isnull(GroupId,0) as GroupId,isnull(LevelId,0) as LevelId FROM eDocument_Category_V3 WITH(NOLOCK) WHERE ClinicID = " + gloEDocV3Admin.gClinicID + " " +
                                " AND CategoryName = '" + _AdvanceDirectiveCategory + "' AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY LevelId,GroupId,CategoryName";
                            }
                            else
                            {
                                if (ExternalDocumentID > 0)
                                {
                                    if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard
                                        || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder
                                        || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewPatientSummary
                                        || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewTask
                                       )
                                    {
                                        _strSQL = "SELECT   distinct  eDocument_Category_V3.CategoryId, eDocument_Category_V3.CategoryName, isnull(eDocument_Category_V3.GroupId,0) as GroupId , isnull(eDocument_Category_V3.LevelId,0) as LevelId  " +
                                        " FROM eDocument_Category_V3 WITH(NOLOCK) INNER JOIN eDocument_Details_V3 ON eDocument_Category_V3.ClinicID = eDocument_Details_V3.ClinicID AND eDocument_Category_V3.CategoryName = eDocument_Details_V3.Category " +
                                        " WHERE (eDocument_Category_V3.ClinicID = " + gloEDocV3Admin.gClinicID + ") AND (eDocument_Category_V3.CategoryId IS NOT NULL) AND " +
                                        " (eDocument_Category_V3.CategoryName IS NOT NULL) AND (eDocument_Details_V3.eDocumentID = " + ExternalDocumentID + ") " +
                                        "  ORDER BY LevelId, GroupId, eDocument_Category_V3.CategoryName ";
                                       // " ORDER BY eDocument_Category_V3.LevelId, eDocument_Category_V3.GroupId, eDocument_Category_V3.CategoryName ";
                                    }
                                }
                            }
                        }
                        }
                        

                        oDB.Retrive_Query(_strSQL, out oDataTable);
                        if (oDataTable != null)
                        {
                            if (oDataTable.Rows.Count > 0)
                            {
                                oFillGrid.Tree.Column = COL_NODENAME;
                                oFillGrid.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                                oFillGrid.Tree.Indent = 15;
                                oFillGrid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes;

                                int _catid = 0;
                                int _catgroupid = 0;
                                int _catlevelid = 0;
                                int _fillrowindex = 0;

                                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                {
                                    _catid = 0;
                                    _catgroupid = 0;
                                    _catlevelid = 0;
                                    _fillrowindex = 0;

                                    _catid = Convert.ToInt32(oDataTable.Rows[i]["CategoryId"].ToString());
                                    _catgroupid = Convert.ToInt32(oDataTable.Rows[i]["GroupId"].ToString());
                                    _catlevelid = Convert.ToInt32(oDataTable.Rows[i]["LevelId"].ToString());

                                    if (_catlevelid == 0)
                                    {
                                        if (oFillGrid == null)
                                        {
                                            return;
                                        }

                                        oFillGrid.Rows.Add();
                                        _fillrowindex = oFillGrid.Rows.Count - 1;



                                        oFillGrid.Rows[_fillrowindex].ImageAndText = true;
                                        oFillGrid.Rows[_fillrowindex].Height = 23;
                                        oFillGrid.Rows[_fillrowindex].IsNode = true;
                                        oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Category"];
                                        oFillGrid.Rows[_fillrowindex].Node.Level = 0;
                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            //oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.RCMCategory;
                                            oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.DMSCategory.ToBitmap();
                                        }
                                        else
                                        {
                                            oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.Category;
                                        }
                                        oFillGrid.Rows[_fillrowindex].Node.Data = oDataTable.Rows[i]["CategoryName"].ToString();

                                        oFillGrid.Rows[_fillrowindex].AllowEditing = false;

                                        oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Category);
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDataTable.Rows[i]["CategoryName"].ToString());
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, _catid);
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORYGROUPID, _catgroupid);
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORYLEVELID, _catlevelid);
                                    }
                                    else
                                    {
                                        _fillrowindex = oFillGrid.FindRow(_catgroupid.ToString(), 0, COL_CATEGORYID, false, true, false);

                                        if (_fillrowindex >= 0)
                                        {
                                            C1.Win.C1FlexGrid.Node oGroupNode = null;
                                            oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                            oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDataTable.Rows[i]["CategoryName"].ToString());
                                            _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;

                                            oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Category"];
                                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                            {
                                                //oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.RCMCategory;
                                                oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.DMSCategory.ToBitmap();
                                            }
                                            else
                                            {
                                                oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.Category;
                                            }
                                            oFillGrid.Rows[_fillrowindex].AllowEditing = false;

                                            oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Category);
                                            oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDataTable.Rows[i]["CategoryName"].ToString());
                                            oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, _catid);
                                            oFillGrid.SetData(_fillrowindex, COL_CATEGORYGROUPID, _catgroupid);
                                            oFillGrid.SetData(_fillrowindex, COL_CATEGORYLEVELID, _catlevelid);
                                        }
                                    }

                                }
                            }
                        }
                        oDB.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                       oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                #endregion
            }
        }
        #endregion "Dhruv 20100626-> Fill Categories"

        #endregion

        #region "Fill Documents"

        #region "dhruv -> 20100626 Fill Documents"

 
        private void FillDocuments(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, string Year, Int64 PatientID, Int64 ExternalDocumentID, Enumeration.enum_OpenExternalSource ExternalSource)
        {
            gloEDocumentV3.Document.BaseDocuments oDocuments = null; //new gloEDocumentV3.Document.BaseDocuments();
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            try
            {
                if (oFillGrid != null)
                {
                    if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.None || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.RxMeds || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.IntuitMessage || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.SecureMessage || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Amedments || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder)
                    {
                        if (oList != null)
                        {
                            //oDocuments = oList.GetBaseDocuments(PatientID, Convert.ToInt32(Year), gloEDocV3Admin.gClinicID, _OpenExternalSource);
                            oDocuments = oList.GetBaseDocuments_Optimized(PatientID, Convert.ToInt32(Year), gloEDocV3Admin.gClinicID, _OpenExternalSource);
                        }
                    }
                    else if (ExternalSource == enum_OpenExternalSource.RCM)
                    {
                        if (oList != null)
                        {
                            if (ExternalDocumentID > 0)
                            {
                                flgShowAckDocs = true;
                                oDocuments = oList.GetBaseDocuments(PatientID, ExternalDocumentID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                            }
                            else
                            {
                                // oDocuments = oList.GetBaseDocuments_RCM(Convert.ToInt32(Year), gloEDocV3Admin.gClinicID);
                                oDocuments = oList.GetBaseDocuments_RCM_Optimized(Convert.ToInt32(Year), gloEDocV3Admin.gClinicID, !flgShowAckDocs);
                            }
                        }
                    }
                    else
                    {
                        if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                        {
                            string _ImmunizationCategory = "";
                            using (eDocManager.eDocGetList oListCat = new gloEDocumentV3.eDocManager.eDocGetList())
                            {
                                _ImmunizationCategory = oListCat.GetImmunizationCateory();
                            }
                            //oListCat.Dispose();

                            if (oList != null)
                            {
                                oDocuments = oList.GetBaseDocuments_Immunization(PatientID, _ImmunizationCategory, gloEDocV3Admin.gClinicID,ExternalDocumentID );
                               
                            }
                        }

                        if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.AdvanceDirective)
                        {
                            string _AdvanceDirectiveCategory = "";
                            using (eDocManager.eDocGetList oListCat = new gloEDocumentV3.eDocManager.eDocGetList())
                            {
                                _AdvanceDirectiveCategory = oListCat.GetAdvanceDirectiveCateory();
                            }
                            //oListCat.Dispose();

                            if (oList != null)
                            {
                                oDocuments = oList.GetBaseDocuments(PatientID, _AdvanceDirectiveCategory, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                            }
                        }
                        else
                        {
                            if (ExternalDocumentID > 0)
                            {
                                if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard
                                || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder
                                || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewPatientSummary
                                || ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewTask)
                                {
                                    if (oList != null)
                                    {
                                        oDocuments = oList.GetBaseDocuments(PatientID, ExternalDocumentID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                                    }
                                }
                            }
                        }
                    }


                    if (oDocuments != null)
                    {
                        for (int i = 0; i <= oDocuments.Count - 1; i++)
                        {
                            int _fillrowindex = 0;

                            _fillrowindex = oFillGrid.FindRow(oDocuments[i].CategoryID.ToString(), 0, COL_CATEGORYID, false, true, false);

                            if (!string.IsNullOrEmpty(oDocuments[i].SubCategory))
                            {
                                if (_fillrowindex >= 0)
                                {
                                    int _fillrowindex_SubCategory = 0;
                                    Int32 iSubCatCnt = oFillGrid.Rows[_fillrowindex].Node.Children;

                                    if (iSubCatCnt > 0)
                                    {
                                        for (Int32 iSubCnt = _fillrowindex; iSubCnt < oFillGrid.Rows.Count; iSubCnt++)
                                        {
                                            if (oDocuments[i].CategoryID == Convert.ToInt64(oFillGrid.GetData(iSubCnt, COL_CATEGORYID)))
                                            {
                                                if (string.Compare(oDocuments[i].SubCategory, Convert.ToString(oFillGrid.GetData(iSubCnt, COL_SUBCATEGORY)), true) == 0)
                                                {
                                                    _fillrowindex_SubCategory = iSubCnt;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (_fillrowindex_SubCategory <= 0)
                                    {
                                        _fillrowindex_SubCategory = 0;

                                        C1.Win.C1FlexGrid.Node oGroupNode = null;
                                        oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                        oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].SubCategory);
                                        _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;

                                        C1.Win.C1FlexGrid.CellStyle ostyle_Category; 
                                        try
                                        {
                                            if (oFillGrid.Styles.Contains("style_SubCategory"))
                                            {
                                                ostyle_Category = oFillGrid.Styles["style_SubCategory"];
                                            }
                                            else
                                            {
                                                ostyle_Category = oFillGrid.Styles.Add("style_SubCategory");
                                                ostyle_Category.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                                ostyle_Category.ForeColor = Color.Black; //Color.DarkBlue;
                                                ostyle_Category.BackColor = Color.FromArgb(198, 228, 255);
                                                ostyle_Category.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                                            }

                                        }
                                        catch
                                        {
                                            ostyle_Category = oFillGrid.Styles.Add("style_SubCategory");
                                            ostyle_Category.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                            ostyle_Category.ForeColor = Color.Black; //Color.DarkBlue;
                                            ostyle_Category.BackColor = Color.FromArgb(198, 228, 255);                                            //ostyle_Category.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
                                            ostyle_Category.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;
                                        }

                                        oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_SubCategory"];
                                        oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.RCMSubCategory;

                                        oFillGrid.Rows[_fillrowindex].AllowEditing = false;

                                        oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.SubCategory);
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID.ToString());
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category.ToString());
                                        oFillGrid.SetData(_fillrowindex, COL_SUBCATEGORY, oDocuments[i].SubCategory);
                                    }
                                    else
                                    {
                                        _fillrowindex = _fillrowindex_SubCategory;
                                    }
                                }
                            }

                            if (_fillrowindex >= 0)
                            {
                                C1.Win.C1FlexGrid.Node oGroupNode = null;
                                oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                #region "Add Node and Set Style"
                                if (oDocuments[i].IsAcknowledge == false)
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentNew);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_NotAcknowledge"];
                                }
                                else
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentAcknowledge);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_Acknowledge"];
                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        oFillGrid.Rows[_fillrowindex].Visible = flgShowAckDocs;
                                    }
                                }
                                oFillGrid.Rows[_fillrowindex].Height = 23;
                                #endregion

                                #region "Set Flag"
                                if (oDocuments[i].IsAcknowledge == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagAcknowledge); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagNone); }

                                if (oDocuments[i].HasNote == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNote); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNone); }

                                oFillGrid.SetCellCheck(_fillrowindex, COL_NODENAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                #endregion

                                #region "Fill Document Information"
                                oFillGrid.SetData(_fillrowindex, COL_CONATINERS, oDocuments[i].EContainers);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTID, oDocuments[i].EDocumentID);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTNAME, oDocuments[i].DocumentName);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category);
                                oFillGrid.SetData(_fillrowindex, COL_SUBCATEGORY, oDocuments[i].SubCategory);
                                oFillGrid.SetData(_fillrowindex, COL_PATIENTID, oDocuments[i].PatientID);
                                oFillGrid.SetData(_fillrowindex, COL_YEAR, oDocuments[i].Year);
                                oFillGrid.SetData(_fillrowindex, COL_MONTH, oDocuments[i].Month);
                                oFillGrid.SetData(_fillrowindex, COL_PAGENUMBERS, oDocuments[i].PageCounts);
                                oFillGrid.SetData(_fillrowindex, COL_CREATEDDATETIME, oDocuments[i].CreatedDateTime);
                                oFillGrid.SetData(_fillrowindex, COL_ISACKNOWLEDGE, oDocuments[i].IsAcknowledge);
                                oFillGrid.SetData(_fillrowindex, COL_HASNOTES, oDocuments[i].HasNote);
                                oFillGrid.SetData(_fillrowindex, COL_CLINCID, oDocuments[i].ClinicID);
                                oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document);
                                oFillGrid.SetData(_fillrowindex, COL_FLAG_DATE, oDocuments[i].CreatedDateTime.ToString("MM/dd/yy"));
                                #endregion

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, "DMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null; 
                }
                if (oDocuments != null)
                {
                    oDocuments.Dispose();
                    oDocuments = null;
                }
              }
        }

        private void FillDocuments_Immnunization(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, string Year, Int64 PatientID, Int64 ExternalDocumentID, Enumeration.enum_OpenExternalSource ExternalSource)
        {
            gloEDocumentV3.Document.BaseDocuments oDocuments = null; //new gloEDocumentV3.Document.BaseDocuments();
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            try
            {
                if (oFillGrid != null)
                {
                    
                        if (ExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                        {
                            string _ImmunizationCategory = "";
                            using (eDocManager.eDocGetList oListCat = new gloEDocumentV3.eDocManager.eDocGetList())
                            {
                                _ImmunizationCategory = oListCat.GetImmunizationCateory();
                            }
                            //oListCat.Dispose();

                            if (oList != null)
                            {
                                oDocuments = oList.GetBaseDocuments(PatientID, _ImmunizationCategory, gloEDocV3Admin.gClinicID, _OpenExternalSource);

                            }
                        }

                        oFillGrid.Rows.Count = 1;
                   


                    if (oDocuments != null)
                    {
                        for (int i = 0; i <= oDocuments.Count - 1; i++)
                        {
                            int _fillrowindex = 0;

                            _fillrowindex = oFillGrid.FindRow(oDocuments[i].CategoryID.ToString(), 0, COL_CATEGORYID, false, true, false);

                            if (_fillrowindex >= 0)
                            {
                                C1.Win.C1FlexGrid.Node oGroupNode = null;
                                oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                #region "Add Node and Set Style"
                                if (oDocuments[i].IsAcknowledge == false)
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentNew);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_NotAcknowledge"];
                                }
                                else
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentAcknowledge);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_Acknowledge"];
                                }
                                oFillGrid.Rows[_fillrowindex].Height = 23;
                                #endregion

                                #region "Set Flag"
                                if (oDocuments[i].IsAcknowledge == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagAcknowledge); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagNone); }

                                if (oDocuments[i].HasNote == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNote); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNone); }

                                oFillGrid.SetCellCheck(_fillrowindex, COL_NODENAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                #endregion

                                #region "Fill Document Information"
                                oFillGrid.SetData(_fillrowindex, COL_CONATINERS, oDocuments[i].EContainers);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTID, oDocuments[i].EDocumentID);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTNAME, oDocuments[i].DocumentName);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category);
                                oFillGrid.SetData(_fillrowindex, COL_PATIENTID, oDocuments[i].PatientID);
                                oFillGrid.SetData(_fillrowindex, COL_YEAR, oDocuments[i].Year);
                                oFillGrid.SetData(_fillrowindex, COL_MONTH, oDocuments[i].Month);
                                oFillGrid.SetData(_fillrowindex, COL_PAGENUMBERS, oDocuments[i].PageCounts);
                                oFillGrid.SetData(_fillrowindex, COL_CREATEDDATETIME, oDocuments[i].CreatedDateTime);
                                oFillGrid.SetData(_fillrowindex, COL_ISACKNOWLEDGE, oDocuments[i].IsAcknowledge);
                                oFillGrid.SetData(_fillrowindex, COL_HASNOTES, oDocuments[i].HasNote);
                                oFillGrid.SetData(_fillrowindex, COL_CLINCID, oDocuments[i].ClinicID);
                                oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document);
                                oFillGrid.SetData(_fillrowindex, COL_FLAG_DATE, oDocuments[i].CreatedDateTime.ToString("MM/dd/yy"));
                                #endregion

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, "DMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                if (oDocuments != null)
                {
                    oDocuments.Dispose();
                    oDocuments = null;
                }
            }
        }
        #endregion "dhruv -> 20100626 Fill Documents"


        #region "Dhruv 20100626 -> FillFilteredDocuments"
    
        private void FillFilteredDocuments_RCM(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, string Year, Int64 PatientID, string WhereUserTagIs, string WhereNotesIs, string WhereAcknowledgeIs,string WhereDocumentNameIs,string WhichYearIs)
        {
            gloEDocumentV3.Document.BaseDocuments oDocuments = null;//new gloEDocumentV3.Document.BaseDocuments();
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            try
            {
                if (oFillGrid != null && oFillGrid.Rows.Count > 0)
                {
                    if (oList != null)
                    {
                        oDocuments = oList.GetFilteredBaseDocuments(PatientID, Convert.ToInt32(Year), gloEDocV3Admin.gClinicID, WhereUserTagIs, WhereNotesIs, WhereAcknowledgeIs, WhereDocumentNameIs, WhichYearIs, _OpenExternalSource);
                    }

                    if (oDocuments != null)
                    {
                        for (int i = 0; i <= oDocuments.Count - 1; i++)
                        {
                            int _fillrowindex = 0;

                            _fillrowindex = oFillGrid.FindRow(oDocuments[i].CategoryID.ToString(), 0, COL_CATEGORYID, false, true, false);

                            if (!string.IsNullOrEmpty(oDocuments[i].SubCategory))
                            {
                                if (_fillrowindex >= 0)
                                {
                                    int _fillrowindex_SubCategory = 0;
                                    Int32 iSubCatCnt = oFillGrid.Rows[_fillrowindex].Node.Children;

                                    if (iSubCatCnt > 0)
                                    {
                                        for (Int32 iSubCnt = _fillrowindex; iSubCnt < oFillGrid.Rows.Count; iSubCnt++)
                                        {
                                            if (oDocuments[i].CategoryID == Convert.ToInt64(oFillGrid.GetData(iSubCnt, COL_CATEGORYID)))
                                            {
                                                if (string.Compare(oDocuments[i].SubCategory, Convert.ToString(oFillGrid.GetData(iSubCnt, COL_SUBCATEGORY)), true) == 0)
                                                {
                                                    _fillrowindex_SubCategory = iSubCnt;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (_fillrowindex_SubCategory <= 0)
                                    {
                                        _fillrowindex_SubCategory = 0;

                                        C1.Win.C1FlexGrid.Node oGroupNode = null;
                                        oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                        oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].SubCategory);
                                        _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;

                                        oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Category"];
                                        //oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.RCMCategory;
                                        oFillGrid.Rows[_fillrowindex].Node.Image = global::gloEDocumentV3.Properties.Resources.DMSCategory.ToBitmap();

                                        oFillGrid.Rows[_fillrowindex].AllowEditing = false;

                                        oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.SubCategory);
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID.ToString());
                                        oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category.ToString());
                                        //oFillGrid.SetData(_fillrowindex, COL_CATEGORYGROUPID, _catgroupid);
                                        //oFillGrid.SetData(_fillrowindex, COL_CATEGORYLEVELID, _catlevelid);
                                        oFillGrid.SetData(_fillrowindex, COL_SUBCATEGORY, oDocuments[i].SubCategory);
                                    }
                                    else
                                    {
                                        _fillrowindex = _fillrowindex_SubCategory;
                                    }
                                }
                            }

                            if (_fillrowindex >= 0)
                            {
                                C1.Win.C1FlexGrid.Node oGroupNode = null;
                                oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                #region "Add Node and Set Style"
                                if (oDocuments[i].IsAcknowledge == false)
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentNew);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_NotAcknowledge"];
                                }
                                else
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentAcknowledge);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_Acknowledge"];
                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        oFillGrid.Rows[_fillrowindex].Visible = flgShowAckDocs;
                                    }
                                }
                                oFillGrid.Rows[_fillrowindex].Height = 23;
                                #endregion

                                #region "Set Flag"
                                if (oDocuments[i].IsAcknowledge == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagAcknowledge); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagNone); }

                                if (oDocuments[i].HasNote == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNote); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNone); }

                                oFillGrid.SetCellCheck(_fillrowindex, COL_NODENAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                #endregion

                                #region "Fill Document Information"
                                oFillGrid.SetData(_fillrowindex, COL_CONATINERS, oDocuments[i].EContainers);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTID, oDocuments[i].EDocumentID);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTNAME, oDocuments[i].DocumentName);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category);
                                oFillGrid.SetData(_fillrowindex, COL_SUBCATEGORY, oDocuments[i].SubCategory);
                                oFillGrid.SetData(_fillrowindex, COL_PATIENTID, oDocuments[i].PatientID);
                                oFillGrid.SetData(_fillrowindex, COL_YEAR, oDocuments[i].Year);
                                oFillGrid.SetData(_fillrowindex, COL_MONTH, oDocuments[i].Month);
                                oFillGrid.SetData(_fillrowindex, COL_PAGENUMBERS, oDocuments[i].PageCounts);
                                oFillGrid.SetData(_fillrowindex, COL_CREATEDDATETIME, oDocuments[i].CreatedDateTime);
                                oFillGrid.SetData(_fillrowindex, COL_ISACKNOWLEDGE, oDocuments[i].IsAcknowledge);
                                oFillGrid.SetData(_fillrowindex, COL_HASNOTES, oDocuments[i].HasNote);
                                oFillGrid.SetData(_fillrowindex, COL_CLINCID, oDocuments[i].ClinicID);
                                oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document);
                                oFillGrid.SetData(_fillrowindex, COL_FLAG_DATE, oDocuments[i].CreatedDateTime.ToString("MM/dd/yy"));
                                #endregion

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, "RCM DMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                if (oDocuments != null)
                {
                    oDocuments.Dispose();
                    oDocuments = null;
                }
            }
        }

        private void FillFilteredDocuments(C1.Win.C1FlexGrid.C1FlexGrid oFillGrid, string Year, Int64 PatientID, string WhereUserTagIs, string WhereNotesIs, string WhereAcknowledgeIs, string WhereDocumentNameIs, string WhichYearIs)
        {
            gloEDocumentV3.Document.BaseDocuments oDocuments = null;//new gloEDocumentV3.Document.BaseDocuments();
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            try
            {
                if (oFillGrid != null && oFillGrid.Rows.Count > 0)
                {
                    if (oList != null)
                    {
                        oDocuments = oList.GetFilteredBaseDocuments(PatientID, Convert.ToInt32(Year), gloEDocV3Admin.gClinicID, WhereUserTagIs, WhereNotesIs, WhereAcknowledgeIs, WhereDocumentNameIs, WhichYearIs, _OpenExternalSource);
                    }


                    if (oDocuments != null)
                    {
                        for (int i = 0; i <= oDocuments.Count - 1; i++)
                        {
                            int _fillrowindex = 0;

                            _fillrowindex = oFillGrid.FindRow(oDocuments[i].CategoryID.ToString(), 0, COL_CATEGORYID, false, true, false);

                            if (_fillrowindex >= 0)
                            {
                                C1.Win.C1FlexGrid.Node oGroupNode = null;
                                oGroupNode = oFillGrid.Rows[_fillrowindex].Node;

                                #region "Add Node and Set Style"
                                if (oDocuments[i].IsAcknowledge == false)
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentNew);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_NotAcknowledge"];
                                }
                                else
                                {
                                    oGroupNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDocuments[i].DocumentName, oDocuments[i].EDocumentID, global::gloEDocumentV3.Properties.Resources.DocumentAcknowledge);
                                    _fillrowindex = oGroupNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                                    oFillGrid.Rows[_fillrowindex].Style = oFillGrid.Styles["style_Document_Acknowledge"];
                                }
                                oFillGrid.Rows[_fillrowindex].Height = 23;
                                #endregion

                                #region "Set Flag"
                                if (oDocuments[i].IsAcknowledge == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagAcknowledge); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagNone); }

                                if (oDocuments[i].HasNote == true)
                                { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNote); }
                                else { oFillGrid.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNone); }

                                oFillGrid.SetCellCheck(_fillrowindex, COL_NODENAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                #endregion

                                #region "Fill Document Information"
                                oFillGrid.SetData(_fillrowindex, COL_CONATINERS, oDocuments[i].EContainers);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTID, oDocuments[i].EDocumentID);
                                oFillGrid.SetData(_fillrowindex, COL_DOCUMENTNAME, oDocuments[i].DocumentName);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORYID, oDocuments[i].CategoryID);
                                oFillGrid.SetData(_fillrowindex, COL_CATEGORY, oDocuments[i].Category);
                                oFillGrid.SetData(_fillrowindex, COL_PATIENTID, oDocuments[i].PatientID);
                                oFillGrid.SetData(_fillrowindex, COL_YEAR, oDocuments[i].Year);
                                oFillGrid.SetData(_fillrowindex, COL_MONTH, oDocuments[i].Month);
                                oFillGrid.SetData(_fillrowindex, COL_PAGENUMBERS, oDocuments[i].PageCounts);
                                oFillGrid.SetData(_fillrowindex, COL_CREATEDDATETIME, oDocuments[i].CreatedDateTime);
                                oFillGrid.SetData(_fillrowindex, COL_ISACKNOWLEDGE, oDocuments[i].IsAcknowledge);
                                oFillGrid.SetData(_fillrowindex, COL_HASNOTES, oDocuments[i].HasNote);
                                oFillGrid.SetData(_fillrowindex, COL_CLINCID, oDocuments[i].ClinicID);
                                oFillGrid.SetData(_fillrowindex, COL_COLTYPE, gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document);
                                oFillGrid.SetData(_fillrowindex, COL_FLAG_DATE, oDocuments[i].CreatedDateTime.ToString("MM/dd/yy"));
                                #endregion

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, "DMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                if (oDocuments != null)
                {
                    oDocuments.Dispose();
                    oDocuments = null;
                }
            }
        }
        #endregion "Dhruv 20100626 -> FillFilteredDocuments"
        #endregion

        #region "Load Documents"
     
        #region "Dhruv 20100622-> LoadDocument "

        private void LoadDocument(Int64 DocumentID, Int64 ContainerID, enum_OpenExternalSource _OpenExternalSource)
        {
            //_IsDocumentsLoading = true;
            //c1Documents.Enabled = false;
            //// Commented by Pramod for DMS Hang Application Issue Start
            #region "Unload Viewer"
            //if (oPDFView != null)
            //{
            //    if (oPDFView.GetDoc() != null)
            //    {
            //        oPDFView.Close();
            //        oPDFDoc.Close();
            //        oPDFDoc.Dispose();

            //        try
            //        {
            //            if (oPDFView.Container != null)
            //            { oPDFView.Container.Dispose(); }
            //            if (oPDFView != null)
            //            { oPDFView.Dispose(); }
            //        }
            //        catch (Exception ex)
            //        {

            //            #region " Make Log Entry "
            //            _ErrorMessage = ex.ToString();
            //            if (_ErrorMessage.Trim() != "")
            //            {
            //                string _MessageString = "Load Docuemnt: Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                _MessageString = "";
            //            }
            //            #endregion " Make Log Entry "
            //        }
            //        oPDFView = null;
            //    }
            //    else
            //    {
            //        oPDFView.Close();
            //        try
            //        {
            //            if (oPDFView.Container != null)
            //            { oPDFView.Container.Dispose(); }
            //            if (oPDFView != null) { oPDFView.Dispose(); }
            //        }
            //        catch (Exception ex)
            //        {

            //            #region " Make Log Entry "
            //            _ErrorMessage = ex.ToString();
            //            if (_ErrorMessage.Trim() != "")
            //            {
            //                string _MessageString = "LoadDocument: Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                _MessageString = "";
            //            }
            //            #endregion " Make Log Entry "
            //        }
            //        oPDFView = null;
            //    }
            //}

            //if (oPDFDoc != null) { oPDFDoc.Dispose(); oPDFDoc = null; }
            //// Commented by Pramod for DMS Hang Application Issue End
            if (oProcessLabel != null)
            {
                if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                oProcessLabel.Dispose(); oProcessLabel = null;
            }
            #endregion
                        
            if (DocumentID > 0 && ContainerID > 0)
            {
                _IsDocumentsLoading = true;
                c1Documents.Enabled = false;
                Application.DoEvents();

                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewerCode.cs Load Document() at Line 1624: Wait Process Start");
                #region "Wait Process"
                if (oProcessLabel != null)
                {
                    if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                    oProcessLabel.Dispose(); oProcessLabel = null;
                }
                oProcessLabel = new Label();
                pnlPreview.Controls.Add(oProcessLabel);
                oProcessLabel.Dock = DockStyle.Fill;
                //oProcessLabel.Image = Properties.Resources.Wait;
                //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                oProcessLabel.Location = new Point(0, 0);
                oProcessLabel.ForeColor = Color.Blue;
                //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                oProcessLabel.Text = "Please wait !!!";
                oProcessLabel.Name = "lblProcess";
                oProcessLabel.Visible = true;
                oProcessLabel.BringToFront();
                #endregion
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewerCode.cs Load Document() at Line 1648: Wait Process End");
                Application.DoEvents();

                string _FilePath = "";
                string _FolderPath = "";
                object ContainerStream = null;
                bool _DocumentLoadedFromDatabase = false;
                Byte[] byteRead = null;

                #region "Decide Whether Load as file or stream"
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                if (_DocumentLoadAsFile == true)
                {

                    _FolderPath = gloEDocV3Admin.gDocumentOpenTemporaryProcessPath;

                    if (System.IO.Directory.Exists(_FolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(_FolderPath);
                    }

                    _FilePath = _FolderPath + "\\" + DocumentID.ToString() + "~" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";

                    try
                    {
                        if (System.IO.File.Exists(_FilePath) == true)
                        {
                            System.IO.File.Delete(_FilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                    }

                    //tls_MaintainDoc.Enabled = false;
                    
                    //ArrayList arr = new ArrayList();
                    //arr.Add(DocumentID);
                    //arr.Add(ContainerID);
                    //arr.Add(_FilePath);

                    //Commented & Added By Debasish Das on 1st Nov 2010 (Background Work)
                    //while (bwLoadDocument.CancellationPending)
                    //{

                    //    bwLoadDocument.CancelAsync();
                        
                    //}

                    //bwLoadDocument.RunWorkerAsync(arr);
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewerCode.cs Load Document() at Line 1701: Before GetContainerStream()");
                    oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID,ref _FilePath, _OpenExternalSource);
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewerCode.cs Load Document() at Line 1703: After GetContainerStream()");
                    _DocumentLoadedFromDatabase = System.IO.File.Exists(_FilePath);

                    //***
                }
                else
                {
                    ContainerStream = oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                    if (ContainerStream != null) { byteRead = (byte[])ContainerStream; _DocumentLoadedFromDatabase = true; }
                }
                oList.Dispose();
                oList = null;
                #endregion

                #region Commented Code (Background Work)
                if (_DocumentLoadedFromDatabase == true)
                {
                    try
                    {
                        //oPDFView = new pdftron.PDF.PDFView();

                        //if (pnlPreview.Controls.Contains(oPDFView) == true) { pnlPreview.Controls.Remove(oPDFView); }
                        //if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }

                        #region "Open Document & Load Pages"

                        if (_DocumentLoadAsFile == true)
                        {
                           
                            if (oPDFView == null)
                            {
                                //oPDFView = new pdftron.PDF.PDFViewCtrl();
                                oPDFView = new MyPDFView(this, 0);
                            }
                          pdftron.PDF.PDFDoc oldDoc = oPDFView.GetDoc();
                            //if (oPDFDoc != null)
                            //{
                            //    oPDFDoc.Close();
                            //    oPDFDoc.Dispose();
                            //    oPDFDoc = null; 
                            //}
                            oPDFDoc = new pdftron.PDF.PDFDoc(_FilePath);
                            if (oPDFDoc != null)
                            {
                                if (oPDFDoc.IsModified() == true)
                                {
                                    oPDFDoc.Save(_FilePath, 0);
                                }
                            }
                            if (oPDFView == null)
                            {
                                //oPDFView = new pdftron.PDF.PDFViewCtrl();


                                oPDFView = new MyPDFView(this, 0);
                            }
                            if (oPDFDoc != null)
                            {
                                if (oPDFView != null)
                                {
                                    oPDFView.Show();
                                    oPDFView.SetDoc(oPDFDoc);
                                    //if (oPDFDoc != null)
                                    //{
                                    //    if (oPDFDoc.IsModified() == true)
                                    //    {
                                    //        oPDFDoc.Save(_FilePath, 0);
                                    //    }
                                    //}
                                    //////Developer: Mitesh Patel
                                    //////Date:04-Oct-2012'
                                    //////Bug No: 38226
                                    //if (oPDFDoc != null)
                                    //{
                                    //    if (oPDFDoc.IsModified() == true)
                                    //    {
                                    //       IsSaved = true;
                                    //    }
                                    //}
                                }
                            }


                            if (oldDoc != null)
                            {
                                oldDoc.Dispose();
                                oldDoc = null;
                            }
                        }
                        else
                        {
                            if (byteRead != null)
                            {
                                if (oPDFDoc != null)
                                {
                                    oPDFDoc.Close();
                                    oPDFDoc.Dispose();
                                    oPDFDoc = null;
                                }
                                oPDFDoc = new pdftron.PDF.PDFDoc(byteRead, byteRead.Length);
                                if (oPDFDoc != null)
                                {
                                    if (oPDFView != null)
                                    {
                                        oPDFView.SetDoc(oPDFDoc);
                                    }
                                }
                            }
                        }
                        if (oPDFView != null)
                        {
                            oPDFView.MouseWheel += new MouseEventHandler(oPDFView_MouseWheel);
                            oPDFView.MouseDown += new MouseEventHandler(oPDFView_MouseDown);
                            oPDFView.MouseUp += new MouseEventHandler(oPDFView_MouseUp);
                        }
                        //LoadPages(DocumentID, gloEDocV3Admin.gClinicID);
                        #endregion

                        pnlPreview.Controls.Add(oPDFView);
                        oPDFView.Location = new Point(0, 0);
                        oPDFView.Dock = DockStyle.Fill;
                        oPDFView.BringToFront();
                        oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page);
                        //oPDFView.CancelRendering();
                        oPDFView.SetCaching(true);
                        oPDFView.SetProgressiveRendering(true);
                        oPDFView.Visible = true;
                        oPDFView.Refresh();
                        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page);
                        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width);
                        ////oPDFView.OnScroll(0, 1);                       
                        LoadPages(DocumentID, gloEDocV3Admin.gClinicID);
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                    }
                    finally
                    {
                        if (byteRead != null)
                        {
                            Array.Clear(byteRead, 0, byteRead.Length);
                        }
                    }
                } 
                #endregion

            }

            #region Commented Code (Background Work)
            Cursor.Current = Cursors.Default;

            Application.DoEvents();
            if (oProcessLabel != null)
            {
                if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                oProcessLabel.Dispose(); oProcessLabel = null;
            }
            //Added To show Notes if Attached to Document
            if (c1Documents.Rows.Count > 0 && c1Documents.RowSel >-1)
            {
                if (c1Documents.GetData(c1Documents.Row, COL_HASNOTES) != null)
                {
                    if (c1Documents.GetData(c1Documents.Row, COL_HASNOTES).ToString() == "True")
                    {
                        if (btnNote_Up.Visible)
                        {
                            btnNote_Up_Click(null, null);
                        }
                    }
                    else
                    {
                        if (btnNote_Down.Visible)
                        {
                            btnNote_Down_Click(null, null);
                        }
                    }

                }
                else
                { btnNote_Down_Click(null, null); }
            }
            _IsDocumentsLoading = false;
            c1Documents.Enabled = true; 
            #endregion

        }
  
        #endregion "Dhruv 20100622-> LoadDocument "
        
        private void LoadDocumentForSample(Int64 DocumentID, Int64 ContainerID)
        {
            _IsDocumentsLoading = true;
            c1Documents.Enabled = false;
            //// Commented by Pramod for DMS Hang Application Issue Start
            #region "Unload Viewer"
       
            if (oProcessLabel != null)
            {
                if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                oProcessLabel.Dispose(); oProcessLabel = null;
            }
            #endregion


            if (DocumentID > 0 && ContainerID > 0)
            {
                Application.DoEvents();

                #region "Wait Process"
                if (oProcessLabel != null)
                {
                    if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                    oProcessLabel.Dispose(); oProcessLabel = null;
                }
                oProcessLabel = new Label();
                pnlPreview.Controls.Add(oProcessLabel);
                oProcessLabel.Dock = DockStyle.Fill;
                //oProcessLabel.Image = Properties.Resources.Wait;
                //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                oProcessLabel.Location = new Point(0, 0);
                oProcessLabel.ForeColor = Color.Blue;
                //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                oProcessLabel.Text = "Please wait !!!";
                oProcessLabel.Name = "lblProcess";
                oProcessLabel.Visible = true;
                oProcessLabel.BringToFront();
                #endregion

                Application.DoEvents();

                string _FilePath = "";
                string _FolderPath = "";
                object ContainerStream = null;
                bool _DocumentLoadedFromDatabase = false;
                Byte[] byteRead = null;

                #region "Decide Whether Load as file or stream"
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                if (_DocumentLoadAsFile == true)
                {

                    _FolderPath = gloEDocV3Admin.gDocumentOpenTemporaryProcessPath;

                    if (System.IO.Directory.Exists(_FolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(_FolderPath);
                    }

                    _FilePath = _FolderPath + "\\" + DocumentID.ToString() + "~" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";

                    try
                    {
                        if (System.IO.File.Exists(_FilePath) == true)
                        {
                            System.IO.File.Delete(_FilePath);
                        }
                    }
                    catch (Exception ex)
                    {

                        #region " Make Log Entry "
                        _ErrorMessage = ex.ToString();
                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = " Load Document : Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }
                        #endregion " Make Log Entry "

                       

                    }
                    oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, ref _FilePath, _OpenExternalSource);

                    _DocumentLoadedFromDatabase = System.IO.File.Exists(_FilePath);
                }
                else
                {
                    ContainerStream = oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                    if (ContainerStream != null) 
                    { 
                        byteRead = (byte[])ContainerStream;
                        _DocumentLoadedFromDatabase = true; 
                    }
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }

                #endregion

                if (_DocumentLoadedFromDatabase == true)
                {
                    try
                    {
                        //oPDFView = new pdftron.PDF.PDFView();

                        //if (pnlPreview.Controls.Contains(oPDFView) == true) { pnlPreview.Controls.Remove(oPDFView); }
                        //if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }

                        #region "Open Document & Load Pages"

                        if (_DocumentLoadAsFile == true)
                        {
                            if (oPDFView == null)
                            {
                                //oPDFView = new pdftron.PDF.PDFViewCtrl();
                                oPDFView = new MyPDFView(this, 0);
                            }
                            pdftron.PDF.PDFDoc oldDoc = oPDFView.GetDoc();
                            if (oPDFDoc != null)
                            {
                                oPDFDoc.Close();
                                oPDFDoc.Dispose();
                                oPDFDoc = null;
                            }
                            oPDFDoc = new pdftron.PDF.PDFDoc(_FilePath);
                            if (oPDFDoc != null)
                            {
                                if (oPDFDoc.IsModified() == true)
                                {
                                    oPDFDoc.Save(_FilePath, 0);
                                }
                            }
                            if (oPDFView == null)
                            {
                                //oPDFView = new pdftron.PDF.PDFViewCtrl();
                                oPDFView = new MyPDFView(this, 0);
                            }
                            if (oPDFDoc != null)
                            {
                                if (oPDFView != null)
                                {
                                    oPDFView.SetDoc(oPDFDoc);
                                    oPDFView.Show();
                                }
                            }
                        
                            if (oldDoc != null)
                            {
                                oldDoc.Dispose();
                                oldDoc = null;
                            }
                        }
                        else
                        {
                            if (byteRead != null)
                            {
                                if (oPDFDoc != null)
                                {
                                    oPDFDoc.Close();
                                    oPDFDoc.Dispose();
                                    oPDFDoc = null;
                                }
                                oPDFDoc = new pdftron.PDF.PDFDoc(byteRead, byteRead.Length);
                                if (oPDFDoc != null)
                                {
                                    if (oPDFView != null)
                                    {
                                        oPDFView.SetDoc(oPDFDoc);
                                    }
                                }
                            }
                        }
                        if (oPDFView != null)
                        {
                            oPDFView.MouseWheel += new MouseEventHandler(oPDFView_MouseWheel);
                            oPDFView.MouseDown += new MouseEventHandler(oPDFView_MouseDown);
                            oPDFView.MouseUp += new MouseEventHandler(oPDFView_MouseUp);
                        }
                        //LoadPages(DocumentID, gloEDocV3Admin.gClinicID);
                        #endregion

                        pnlPreview.Controls.Add(oPDFView);
                        oPDFView.Location = new Point(0, 0);
                        oPDFView.Dock = DockStyle.Fill;
                        oPDFView.BringToFront();
                        oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page);
                        //oPDFView.CancelRendering();
                        oPDFView.SetCaching(true);
                        oPDFView.SetProgressiveRendering(true);
                        oPDFView.Visible = true;
                        oPDFView.Refresh();
                        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page);
                        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width);
                        //oPDFView.OnScroll(0, 1);
                        LoadPages(DocumentID, gloEDocV3Admin.gClinicID);
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                    }
                    finally
                    {
                        if (byteRead != null) 
                        { 
                            Array.Clear(byteRead, 0, byteRead.Length);
                        }
                    }
                }
            }
            Cursor.Current = Cursors.Default;

            Application.DoEvents();
            if (oProcessLabel != null)
            {
                if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                oProcessLabel.Dispose();
                oProcessLabel = null;
            }

            _IsDocumentsLoading = false;
            c1Documents.Enabled = true;
        }


        #region "Dhruv 20100622 -> UnloadDocument replaced by UnloadDocView"
        public void UnloadDocuments()
        {
            lbl_DocDateTime.Text = string.Empty;  
            txtNotes.Text = "";
            lvwPages.Items.Clear();
            lblPagesHeader.Text = "Pages";
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (oPDFView != null)
                {
                    //SG: Memory Leaks, removing control before dispose
                    if (pnlPreview.Controls.Contains(oPDFView) == true) { pnlPreview.Controls.Remove(oPDFView); }
                    //

                    if (oPDFView.GetDoc() != null)
                    {
                        oPDFView.CloseDoc();
                        oPDFDoc.Close();
                        oPDFDoc.Dispose();
                        oPDFDoc=null;

                        try
                        {
                            if (oPDFView.Container != null)
                            {
                                oPDFView.Container.Dispose(); 
                            }
                            
                            //SG: Memory Leaks, removing events before disposing oPDFView
                            try
                            {
                                oPDFView.MouseWheel -= new MouseEventHandler(oPDFView_MouseWheel);
                                oPDFView.MouseDown -= new MouseEventHandler(oPDFView_MouseDown);
                                oPDFView.MouseUp -= new MouseEventHandler(oPDFView_MouseUp);
                            }
                            catch
                            {
                            }
                            //

                            oPDFView.Dispose();
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }
                        oPDFView = null;
                    }
                    else
                    {
                        oPDFView.CloseDoc();
                        try
                        {
                            if (oPDFView.Container != null)
                            {
                                oPDFView.Container.Dispose();
                            }

                            //SG: Memory Leaks, removing events before disposing oPDFView
                            try
                            {
                                oPDFView.MouseWheel -= new MouseEventHandler(oPDFView_MouseWheel);
                                oPDFView.MouseDown -= new MouseEventHandler(oPDFView_MouseDown);
                                oPDFView.MouseUp -= new MouseEventHandler(oPDFView_MouseUp);
                            }
                            catch
                            {
                            }
                            //

                            oPDFView.Dispose();
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage =ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }
                        oPDFView = null;
                    }
                }

                if (oPDFDoc != null) { oPDFDoc.Dispose(); oPDFDoc = null; }


            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion "Dhruv 20100622 -> UnloadDocument replaced by UnloadDocView"
        private void SelectDocumentInGrid(Int64 DocumentID, Int64 ContainerID)
        {
            try
            {
                _IsDocumentsLoading = true;
                for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.GetData(i, COL_COLTYPE) != null && c1Documents.Rows[i].Visible == true)
                    {
                        if ((gloEDocumentV3.Enumeration.enum_DocumentColumnType)c1Documents.GetData(i, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                        {
                            if (c1Documents.GetData(i, COL_CONATINERS) != null && ((Document.eBaseContainers)c1Documents.GetData(i, COL_CONATINERS))[0] != null)
                            {
                                if (Convert.ToInt64(c1Documents.GetData(i, COL_DOCUMENTID)) == DocumentID && ((Document.eBaseContainers)c1Documents.GetData(i, COL_CONATINERS))[0].EContainerID == ContainerID)
                                {
                                    c1Documents.RowSel = i;
                                    c1Documents.Select(i, COL_NODENAME);
                                    break;
                                }   
                            }
                        }
                    }
                }
                _IsDocumentsLoading = false;
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
           
        }

        //code added by dipak on 20090821 to get document id and container id from c1documents flexgid
        //while solving bug #2665
        private void GetSelectedDocumentInGrid(out Int64 DocumentID, out Int64 ContainerID)
        {
            _IsDocumentsLoading = true;
            Int64 _retDocumentID = 0;
            Int64 _retContainerID = 0;

            try
            {
                
                    if (c1Documents.GetData(c1Documents.Row  , COL_COLTYPE) != null)
                    {
                        if ((gloEDocumentV3.Enumeration.enum_DocumentColumnType)c1Documents.GetData(c1Documents.Row  , COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                        {
                            //c1Documents.RowSel = i;
                            c1Documents.Select(c1Documents.Row , COL_NODENAME);
                            _retDocumentID = Convert.ToInt64(c1Documents.GetData(c1Documents.Row, COL_DOCUMENTID));
                            _retContainerID = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.Row, COL_CONATINERS))[0].EContainerID;
                                                      
                        }
                    }
               

                
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            

            DocumentID = _retDocumentID;
            ContainerID = _retContainerID;
        }
        
        private void GetFirstDocumentInGrid(out Int64 DocumentID, out Int64 ContainerID)
        {
            _IsDocumentsLoading = true;
            Int64 _retDocumentID = 0;
            Int64 _retContainerID = 0;

            try
            {
                //this.c1Documents.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);

                for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.GetData(i, COL_COLTYPE) != null && c1Documents.Rows[i].Visible)
                    {
                        if ((gloEDocumentV3.Enumeration.enum_DocumentColumnType)c1Documents.GetData(i, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                        {
                            c1Documents.RowSel = i;
                            c1Documents.Select(i, COL_NODENAME);
                            _retDocumentID = Convert.ToInt64(c1Documents.GetData(i, COL_DOCUMENTID));
                            _retContainerID = ((Document.eBaseContainers)c1Documents.GetData(i, COL_CONATINERS))[0].EContainerID;

                            #region " Set Acknowledge/Review Button "

                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;

                            if (c1Documents != null && c1Documents.Rows.Count > 0)
                            {
                                if (c1Documents.RowSel >= 0)
                                {
                                    int _Index = c1Documents.RowSel;
                                    if ((enum_DocumentColumnType)c1Documents.GetData(_Index, COL_COLTYPE) == enum_DocumentColumnType.Document)
                                    {
                                        if (Convert.ToBoolean(c1Documents.GetData(_Index, COL_ISACKNOWLEDGE)) == true)
                                        {
                                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                        }
                                        else
                                        {
                                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                        }
                                    }
                                }
                            }

                            #endregion " Set Acknowledge/Review Button "

                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
               // this.c1Documents.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
                _IsDocumentsLoading = false;
            }

            DocumentID = _retDocumentID;
            ContainerID = _retContainerID;
        }

        #endregion

        
        #region " Events for Change Cursor "
        void oPDFView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try { oPDFView.Cursor = oPDFHandFreeCursor; }
                catch { oPDFView.Cursor = Cursors.Default; }
            }
        }

        void oPDFView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try { oPDFView.Cursor = oPDFHandHoldCursor; }
                catch { oPDFView.Cursor = Cursors.Default; }
            }
        } 
        #endregion

        #region "Load Pages"

        #region "Dhruv 20100626 ->LoadPages"
      
        private void LoadPages(Int64 lDocumentID, Int64 lClinicID)
        {
            txtNotes.Text = "";

            lbl_DocDateTime.Text = string.Empty; 

            if (lvwPages != null)
            {
                if (chkInSearchMode.Checked == false)
                {
                    FillPages(lvwPages, lDocumentID, lClinicID);
                }
                else
                {
                    string _WhereUserTagIs = "";
                    string _WhereNotesIs = "";
                    string _WhereAcknowledgeIs = "";
                    string _WhereDocumentNameIs = "";
                    string _WhichYearIs = "";

                    if (chkSearch_UserTag.Checked == true) { _WhereUserTagIs = txtSearch_UserTag.Text.Trim(); }
                    if (chkSearch_Notes.Checked == true) { _WhereNotesIs = txtSearch_Notes.Text.Trim(); }
                    if (chkSearch_Acknowledge.Checked == true) { _WhereAcknowledgeIs = txtSearch_Acknowledge.Text.Trim(); }
                    if (chkSearch_DocumentName.Checked == true) { _WhereDocumentNameIs = txtSearch_DocumentName.Text.Trim(); }

                    // DropDown Value
                    if (cmbSearchYear.Items.Count > 0)
                    {
                        if (cmbSearchYear.SelectedValue.ToString() != "All")
                        {
                            _WhichYearIs = cmbSearchYear.SelectedValue.ToString();
                        }
                    }

                    FillFilteredPages(lvwPages, lDocumentID, lClinicID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs);
                }


                if (lvwPages.Items.Count > 0)
                {
                   
                    lvwPages.Items[0].Selected = true;
                    lvwPages_Click(null, null);
                    if (c1Documents != null)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            if (c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTNAME) != null &&
                                Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTNAME)) != "")
                            {
                                lblPagesHeader.Text = c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTNAME).ToString() + " Pages";
                                lbl_DocDateTime.Text = "Created :" + c1Documents.GetData(c1Documents.RowSel, COL_CREATEDDATETIME).ToString() ;
                            }
                            else 
                            { 
                                lblPagesHeader.Text = " Pages";
                                
                            }
                        }
                    }
                    else 
                    { 
                        lblPagesHeader.Text = " Pages";
                        
                    }
                }
                else
                {
                    lblPagesHeader.Text = " Pages";
                    
                }
            }
        }
        #endregion "Dhruv 20100626 ->LoadPages"


        #region "Dhruv20100626 ->FillPages"
    
        private void FillPages(System.Windows.Forms.ListView oFillListView, Int64 DocumentID, Int64 ClinicID)
        {
            if (oFillListView != null)
            {
                oFillListView.Items.Clear();
                oFillListView.View = System.Windows.Forms.View.SmallIcon;

                gloEDocumentV3.Document.BasePages oPages = null;//new gloEDocumentV3.Document.BasePages();
                gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                System.Windows.Forms.ListViewItem oItem = null;
                try
                {
                    if (oList != null)
                    {
                        oPages = oList.GetBasePages(DocumentID, ClinicID, _OpenExternalSource);
                    }
                    if (oPages != null)
                    {
                        for (int i = 0; i <= oPages.Count - 1; i++)
                        {
                            oItem = new System.Windows.Forms.ListViewItem();

                            //Fill Page Info - Start
                            oItem.Text = oPages[i].PageName;
                            oItem.SubItems.Add(oPages[i].BookMarkTag);
                            oItem.SubItems.Add(oPages[i].ContainerPageNumber.ToString());
                            oItem.SubItems.Add(oPages[i].DocumentPageNumber.ToString());
                            oItem.SubItems.Add(oPages[i].ContainerID.ToString());
                            oItem.SubItems.Add(oPages[i].DocumentID.ToString());
                            oItem.SubItems.Add(oPages[i].HasNotes.ToString());
                            oItem.ImageIndex = 0;
                            oItem.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            if (oPages[i].HasNotes == true) { oItem.ImageIndex = 1; }
                            oFillListView.Items.Add(oItem);
                            if (oItem != null)
                            {
                                oItem = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);

                    oFillListView.Items.Clear();
                    //HasError = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (oItem != null)
                    {
                        oItem = null;
                    }
                    if (oPages != null)
                    {
                        oPages.Dispose();
                        oPages = null;
                    }
                    if (oList != null)
                    {
                        oList.Dispose();
                        oList = null;
                    }
                }
            }
        }
        #endregion "Dhruv20100626 ->FillPages"


        #region "Dhruv 20100626 -> FillFilteredPages"
     
        private void FillFilteredPages(System.Windows.Forms.ListView oFillListView, Int64 DocumentID, Int64 ClinicID, string WhereUserTagIs, string WhereNotesIs, string WhereAcknowledgeIs)
        {
            if (oFillListView != null)
            {
                oFillListView.Items.Clear();
                oFillListView.View = System.Windows.Forms.View.SmallIcon;

                gloEDocumentV3.Document.BasePages oPages = null;//new gloEDocumentV3.Document.BasePages();
                gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                System.Windows.Forms.ListViewItem oItem = null;
                try
                {
                    if (oList != null)
                    {
                        oPages = oList.GetFilteredBasePages(DocumentID, ClinicID, WhereUserTagIs, WhereNotesIs, WhereAcknowledgeIs, _OpenExternalSource);
                    }

                    if (oPages != null)
                    {
                        for (int i = 0; i <= oPages.Count - 1; i++)
                        {
                            oItem = new System.Windows.Forms.ListViewItem();

                            //Fill Page Info - Start
                            oItem.Text = oPages[i].PageName;
                            oItem.SubItems.Add(oPages[i].BookMarkTag);
                            oItem.SubItems.Add(oPages[i].ContainerPageNumber.ToString());
                            oItem.SubItems.Add(oPages[i].DocumentPageNumber.ToString());
                            oItem.SubItems.Add(oPages[i].ContainerID.ToString());
                            oItem.SubItems.Add(oPages[i].DocumentID.ToString());
                            oItem.SubItems.Add(oPages[i].HasNotes.ToString());
                            oItem.ImageIndex = 0;
                            oItem.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            if (oPages[i].HasNotes == true) { oItem.ImageIndex = 1; }
                            oFillListView.Items.Add(oItem);
                            if (oItem != null)
                            {
                                oItem = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);

                    oFillListView.Items.Clear();
                    //HasError = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (oItem != null)
                    {
                        oItem = null;
                    }

                    if (oPages != null)
                    {
                        oPages.Dispose();
                        oPages = null;
                    }
                    if (oList != null)
                    {
                        oList.Dispose();
                        oList = null;
                    }
                }
            }
        }
        #endregion "Dhruv 20100626 -> FillFilteredPages"

        #endregion

        #region "Supporting Procedures"
        private void ShowHideControl(ShowHideFlag flag, bool show)
        {
            switch (flag)
            {
                case ShowHideFlag.Document:
                    {
                        pnlDocumentsLegends.Visible = show;
                    }
                    break;
                case ShowHideFlag.Legend:
                    {
                        pnlLegends.Visible = show;
                    }
                    break;
                case ShowHideFlag.Patient:
                    {
                        pnlPatients.Visible = show;
                    }
                    break;
                case ShowHideFlag.Pages:
                    {
                        pnlPages.Visible = show;
                    }
                    break;
                case ShowHideFlag.Preview:
                    {
                        pnlPreview.Visible = show;
                    }
                    break;
                case ShowHideFlag.Notes:
                    {
                        pnlNotes.Visible = show;
                    }
                    break;
                case ShowHideFlag.Tags:
                    {
                        pnlTags.Visible = show;
                    }
                    break;
                case ShowHideFlag.Search:
                    {
                        pnlSearch.Visible = show;
                    }
                    break;
            }
        }

        private void ChangeYear_Click(string CurYear)
        {
            Int32 _DialogYear = Convert.ToInt32(CurYear);

            if (_DialogYear > 0)
            {
                if (_DialogYear != Convert.ToInt32(_SelectedYear))
                {
                    _IsDocumentsLoading = true;
                    _SelectedYear = _DialogYear.ToString();
                    lblDocumentsHeader.Text = _SelectedYear.ToString() + " Documents";
                    slblDocumentHeader = lblDocumentsHeader.Text;

                    Application.DoEvents();
                    #region "Wait Process"

                    if (oProcessLabel != null)
                    {
                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                        oProcessLabel.Dispose(); oProcessLabel = null;
                    }
                    oProcessLabel = new Label();
                    pnlPreview.Controls.Add(oProcessLabel);
                    oProcessLabel.Dock = DockStyle.Fill;
                    //oProcessLabel.Image = Properties.Resources.Wait;
                    //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                    oProcessLabel.Location = new Point(0, 0);
                    oProcessLabel.ForeColor = Color.Blue;
                    //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                    oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                    //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                    oProcessLabel.Text = "Please wait !!!";
                    oProcessLabel.Name = "lblProcess";
                    oProcessLabel.Visible = true;
                    oProcessLabel.BringToFront();
                    #endregion
                    Application.DoEvents();

                    if (chkInSearchMode.Checked == false)
                    {

                        #region "Fill Documents"
                        lvwPages.BeginUpdate();
                        lvwPages.Items.Clear();
                        lvwPages.EndUpdate();
                        FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                        FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
                        #endregion

                    }
                    else
                    {
                        #region "Fill Documents"
                        //Searching Text
                        string _WhereUserTagIs = "";
                        string _WhereNotesIs = "";
                        string _WhereAcknowledgeIs = "";
                        string _WhereDocumentNameIs = "";
                        string _WhichYearIs = "";

                        if (chkSearch_UserTag.Checked == true) { _WhereUserTagIs = txtSearch_UserTag.Text.Trim(); }
                        if (chkSearch_Notes.Checked == true) { _WhereNotesIs = txtSearch_Notes.Text.Trim(); }
                        if (chkSearch_Acknowledge.Checked == true) { _WhereAcknowledgeIs = txtSearch_Acknowledge.Text.Trim(); }
                        if (chkSearch_DocumentName.Checked == true) { _WhereDocumentNameIs = txtSearch_DocumentName.Text.Trim(); }

                         // DropDown Value
                        if (cmbSearchYear.Items.Count > 0)
                        {
                            if (cmbSearchYear.SelectedValue.ToString() != "All")
                            {
                                _WhichYearIs = cmbSearchYear.SelectedValue.ToString();
                            }
                        }

                        FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);

                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        {
                            FillFilteredDocuments_RCM(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs,_WhereDocumentNameIs,_WhichYearIs);
                        }
                        else
                        {
                            FillFilteredDocuments(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                        }
                        
                        #endregion
                    }

                    Application.DoEvents();
                    //UnloadDocuments(); //Dhruv 20100622 
                    UnloadDocView();


                    #region "Load First Document"
                    Int64 _docid = 0;
                    Int64 _contid = 0;
                    GetFirstDocumentInGrid(out _docid, out _contid);


                    //##########

                    if (oProcessLabel != null)
                    {
                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                        oProcessLabel.Dispose(); oProcessLabel = null;
                    }

                    Cursor.Current = Cursors.Default;

                    _IsDocumentsLoading = false;
                    c1Documents.Enabled = true;

                    //##########

                    //LoadDocument(_docid, _contid, _OpenExternalSource);
                    #endregion

                    #region " Set Acknowledge/Review Button "

                    tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                    tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;

                    if (c1Documents != null && c1Documents.Rows.Count > 0)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            int _Index = c1Documents.RowSel;
                            if ((enum_DocumentColumnType)c1Documents.GetData(_Index, COL_COLTYPE) == enum_DocumentColumnType.Document)
                            {
                                if (Convert.ToBoolean(c1Documents.GetData(_Index, COL_ISACKNOWLEDGE)) == true)
                                {
                                    tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                    tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                }
                                else
                                {
                                    tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                    tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                }
                            }
                        }
                    }

                    #endregion " Set Acknowledge/Review Button "

                    //if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                    //_IsDocumentsLoading = false;
                }
            }
        }

        #endregion

        #region "Patient Strip"
        
        gloUserControlLibrary.gloUC_PatientStrip oPatientStrip = null;
        private void LoadPatientStrip()
        {
            //for (int i = panel19.Controls.Count - 1; i >= 0; i--)
            //{
            //    if (panel19.Controls[i].GetType() == typeof(gloUserControlLibrary.gloUC_PatientStrip))
            //    {
            //        panel19.Controls.RemoveAt(i);
            //    }
            //}
            if (oPatientStrip != null)
            {
                if (panel19.Controls.Contains(oPatientStrip))
                {
                    panel19.Controls.Remove(oPatientStrip);
                }
                try
                {
                    oPatientStrip.ControlSizeChanged -= new gloUserControlLibrary.gloUC_PatientStrip.ControlSizeChangedEventHandler(oPatientStrip_ControlSizeChanged);
                }
                catch
                {
                }
                oPatientStrip.Dispose();
                oPatientStrip = null;
            }
            oPatientStrip = new gloUserControlLibrary.gloUC_PatientStrip();
            
            oPatientStrip.Visible = true;
            //property changed by Mayuri:20000919
            //oPatientStrip.Dock = DockStyle.Fill;
            oPatientStrip.Dock = DockStyle.Top;
            oPatientStrip.Padding = new Padding(0);
            //oPatientStrip.BorderStyle = BorderStyle.None;
            //Code Added by Mayuri:20090918
            //In order to display Patient details on Scan Documents Form
            //so that changes made from Tools->Patient control customization->SacnDocuments will get reflected on ScanDocuments form
            //oPatientStrip.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None, 0, 0, 0, false, false, false, "", false);
            oPatientStrip.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ScanDocuments, 0, 0, 0, false, false, false, "", false);
            panel19.Controls.Add(oPatientStrip);
            //end code by Mayuri:20090918
            lblPatients.Text = oPatientStrip.PatientCode + " - " + oPatientStrip.PatientName + " - " + oPatientStrip.PatientDateOfBirth.ToString("MM/dd/yyyy");
             _PatientStripMAXHeight = oPatientStrip.Height + 28;
            _PatientStripMINHeight = 28;
            //oPatientStrip.BringToFront();
            //20090919
            oPatientStrip.ControlSizeChanged += new gloUserControlLibrary.gloUC_PatientStrip.ControlSizeChangedEventHandler(oPatientStrip_ControlSizeChanged);
            panel19.BringToFront();
            
        }

        void oPatientStrip_ControlSizeChanged()
        {
           pnlPatients.Height = oPatientStrip.Height;
        }
        #endregion

        #region "Selected Documents"

        #region "Dhruv 20100626 ->Selected Documents"
 
        private DocumentContextMenu.eContextDocuments GetSelectedDocuments(out string DocumentIDsForQuery)
        {
            DocumentContextMenu.eContextDocuments oSelectedDocuments = new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            DocumentContextMenu.Common.eContextContainer oDocumentContainer = null;
            DocumentContextMenu.eContextDocument oDocument = null;
            string _DocumentIDsForQuery = "(";
            bool _ispagesource = false;
            System.Collections.ArrayList _selecteddocumentindexs = GetSelectedDocumentsIndex(out _ispagesource);


            if (_ispagesource == true)
            {
                if (_selecteddocumentindexs != null)
                {
                    for (int i = 0; i <= _selecteddocumentindexs.Count - 1; i++)
                    {
                        int _rowindex = Convert.ToInt32(_selecteddocumentindexs[i].ToString());
                        oDocument = new gloEDocumentV3.DocumentContextMenu.eContextDocument();

                        #region "Document Information"
                        if (oDocument != null)
                        {
                            oDocument.DocumentID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            oDocument.DocumentName = Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTNAME));
                            oDocument.PatientID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_PATIENTID));
                            oDocument.CategoryID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CATEGORYID));
                            oDocument.Category = Convert.ToString(c1Documents.GetData(_rowindex, COL_CATEGORY));
                            oDocument.Year = Convert.ToString(c1Documents.GetData(_rowindex, COL_YEAR));
                            oDocument.Month = Convert.ToString(c1Documents.GetData(_rowindex, COL_MONTH));
                            oDocument.PageCount = Convert.ToInt32(c1Documents.GetData(_rowindex, COL_PAGENUMBERS));
                            oDocument.ClinicID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CLINCID));
                            oDocument.IsAcknowledge = Convert.ToBoolean(c1Documents.GetData(_rowindex, COL_ISACKNOWLEDGE));
                            if (i == 0)
                            {
                                _DocumentIDsForQuery = _DocumentIDsForQuery + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            }
                            else
                            {
                                _DocumentIDsForQuery = _DocumentIDsForQuery + "," + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            }


                        #endregion

                            #region "Container Information"
                            if (lvwPages != null)
                            {
                                for (int j = 0; j <= lvwPages.SelectedItems.Count - 1; j++)
                                {
                                    string _PageName = Convert.ToString(lvwPages.SelectedItems[j].Text);
                                    Int64 _ContNumber = Convert.ToInt64(lvwPages.SelectedItems[j].SubItems[4].Text);
                                    Int64 _DoctNumber = Convert.ToInt64(lvwPages.SelectedItems[j].SubItems[5].Text);
                                    int _ContPageNumber = Convert.ToInt32(lvwPages.SelectedItems[j].SubItems[2].Text);
                                    int _DoctPageNumber = Convert.ToInt32(lvwPages.SelectedItems[j].SubItems[3].Text);
                                    bool _PageHasNotes = Convert.ToBoolean(lvwPages.SelectedItems[j].SubItems[6].Text);

                                    if (oDocumentContainer != null)
                                    {
                                   //     oDocumentContainer.Dispose();
                                        oDocumentContainer = null;
                                    }
                                    for (int k = 0; k <= oDocument.Containers.Count - 1; k++)
                                    {
                                        if (oDocument.Containers[k].ContainerID == _ContNumber)
                                        {
                                            oDocumentContainer = oDocument.Containers[k];
                                            break;
                                        }
                                    }
                                    if (oDocumentContainer == null)
                                    {
                                        oDocumentContainer = new gloEDocumentV3.DocumentContextMenu.Common.eContextContainer();
                                        oDocumentContainer.ContainerID = _ContNumber;
                                        Document.eBaseContainers _tempcontainers = null;
                                        _tempcontainers = (Document.eBaseContainers)c1Documents.GetData(Convert.ToInt32(_selecteddocumentindexs[i].ToString()), COL_CONATINERS);

                                        for (int l = 0; l <= _tempcontainers.Count - 1; l++)
                                        {
                                            if (_tempcontainers[l].EContainerID == _ContNumber)
                                            {
                                                oDocumentContainer.DocumentPageFrom = _tempcontainers[l].PageFrom;
                                                oDocumentContainer.DocumentPageTo = _tempcontainers[l].PageTo;
                                                break;
                                            }
                                        }
                                        oDocumentContainer.Pages.Add(_ContPageNumber, _DoctPageNumber, _PageName, _PageHasNotes);
                                        oDocument.Containers.Add(oDocumentContainer);
                                    }
                                    else
                                    {
                                        oDocumentContainer.Pages.Add(_ContPageNumber, _DoctPageNumber, _PageName, _PageHasNotes);
                                    }
                                }
                            }
                            #endregion
                            
                            oSelectedDocuments.Add(oDocument);
                            if (oDocument != null)
                            {
                               // oDocument.Dispose();
                                oDocument = null;
                            }
                        }
                    }
                }
            }
       
            else
            {
                for (int i = 0; i <= _selecteddocumentindexs.Count - 1; i++)
                {
                    int _rowindex = Convert.ToInt32(_selecteddocumentindexs[i].ToString());
                    oDocument = new gloEDocumentV3.DocumentContextMenu.eContextDocument();

                    #region "Document Information"
                    if (oDocument != null)
                    {
                        oDocument.DocumentID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        oDocument.DocumentName = Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTNAME));
                        oDocument.PatientID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_PATIENTID));
                        oDocument.CategoryID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CATEGORYID));
                        oDocument.Category = Convert.ToString(c1Documents.GetData(_rowindex, COL_CATEGORY));
                        oDocument.Year = Convert.ToString(c1Documents.GetData(_rowindex, COL_YEAR));
                        oDocument.Month = Convert.ToString(c1Documents.GetData(_rowindex, COL_MONTH));
                        oDocument.PageCount = Convert.ToInt32(c1Documents.GetData(_rowindex, COL_PAGENUMBERS));
                        oDocument.ClinicID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CLINCID));
                        oDocument.IsAcknowledge = Convert.ToBoolean(c1Documents.GetData(_rowindex, COL_ISACKNOWLEDGE));

                        if (i == 0)
                        {
                            _DocumentIDsForQuery = _DocumentIDsForQuery + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        }
                        else
                        {
                            _DocumentIDsForQuery = _DocumentIDsForQuery + "," + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        }
                    }
                    #endregion

                    #region "Container Information"
                    Document.eBaseContainers _tempcontainers = null;
                    _tempcontainers = (Document.eBaseContainers)c1Documents.GetData(Convert.ToInt32(_selecteddocumentindexs[i].ToString()), COL_CONATINERS);
                    if (_tempcontainers != null)
                    {
                        for (int j = 0; j <= _tempcontainers.Count - 1; j++)
                        {
                            oDocumentContainer = new gloEDocumentV3.DocumentContextMenu.Common.eContextContainer();
                            if (oDocumentContainer != null)
                            {
                                oDocumentContainer.ContainerID = _tempcontainers[j].EContainerID;
                                oDocumentContainer.DocumentPageFrom = _tempcontainers[j].PageFrom;
                                oDocumentContainer.DocumentPageTo = _tempcontainers[j].PageTo;
                                #region "Document Container Pages"
                                gloEDocumentV3.Document.BasePages oPages = new gloEDocumentV3.Document.BasePages();
                                gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                                oPages = oList.GetBasePages(_tempcontainers[j].EDocumentID, _tempcontainers[j].EContainerID, _tempcontainers[j].ClinicID, _OpenExternalSource);
                                if (oPages != null)
                                {
                                    for (int k = 0; k <= oPages.Count - 1; k++)
                                    {
                                        oDocumentContainer.Pages.Add(oPages[k].ContainerPageNumber, oPages[k].DocumentPageNumber, oPages[k].PageName, oPages[k].HasNotes);
                                    }
                                }
                                if (oList != null)
                                {
                                    oList.Dispose();
                                    oList = null;
                                }
                                if (oPages != null)
                                {
                                    oPages.Dispose();
                                    oPages = null;
                                }
                                #endregion
                                oDocument.Containers.Add(oDocumentContainer);
                                if (oDocumentContainer != null)
                                {
                               //     oDocumentContainer.Dispose();
                                    oDocumentContainer = null;
                                }
                            }
                        }
                        _tempcontainers.Dispose();
                        _tempcontainers = null;
                    }
                    #endregion

                    oSelectedDocuments.Add(oDocument);
                    if (oDocument != null)
                    {
                    //    oDocument.Dispose();
                        oDocument = null;
                    }
                }
            }

            if (_DocumentIDsForQuery == "(")
            {
                _DocumentIDsForQuery = _DocumentIDsForQuery + "0)";
            }
            else
            {
                _DocumentIDsForQuery = _DocumentIDsForQuery + ")";
            }
            DocumentIDsForQuery = _DocumentIDsForQuery;
            return oSelectedDocuments;
        }
        #endregion "Dhruv 20100626 ->Selected Documents"




        #region "Dhruv 20100626 -> GetSelectedDocumentsIndex"
        
        private System.Collections.ArrayList GetSelectedDocumentsIndex(out bool IsPageSource)
        {
            int _CurrentDocumentIndex = 0;
            _CurrentDocumentIndex = c1Documents.RowSel;
            System.Collections.ArrayList _result = new System.Collections.ArrayList();
            bool _IsPageSource = false;
            if (c1Documents != null)
            {
                for (int d = 0; d <= c1Documents.Rows.Count - 1; d++)
                {
                    if (d >= 0)
                    {
                        if (c1Documents.GetData(d, COL_COLTYPE) != null)
                        {
                            if ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(d, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                            {
                                if (c1Documents.GetCellCheck(d, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    _result.Add(d);
                                    _IsPageSource = false;
                                }
                            }
                        }
                    }
                }

                if (_result.Count <= 0)
                {
                    //Code Changes For Bug no.42887 :: scan docs >> provider signature >> Application is showing an exception
                    if (_CurrentDocumentIndex >= 0)
                    {
                        if ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(_CurrentDocumentIndex, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                        {
                            _result.Add(_CurrentDocumentIndex);
                            _IsPageSource = true;
                        }
                    }
                }
            }
            IsPageSource = _IsPageSource;
            return _result;
        }
        #endregion "Dhruv 20100626 -> GetSelectedDocumentsIndex"

        #region "Intuit Functions"
        private System.Collections.ArrayList GetIntuitCheckedDocumentsIndex(out bool IsPageSource)
        {
            int _CurrentDocumentIndex = 0;
            _CurrentDocumentIndex = c1Documents.RowSel;
            System.Collections.ArrayList _result = new System.Collections.ArrayList();
            bool _IsPageSource = false;
            if (c1Documents != null)
            {
                for (int d = 0; d <= c1Documents.Rows.Count - 1; d++)
                {
                    if (d >= 0)
                    {
                        if (c1Documents.GetData(d, COL_COLTYPE) != null)
                        {
                            if ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(d, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                            {
                                if (c1Documents.GetCellCheck(d, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    _result.Add(d);
                                    _IsPageSource = false;
                                }
                            }
                        }
                    }
                }

            }
            IsPageSource = _IsPageSource;
            return _result;
        }

        private DocumentContextMenu.eContextDocuments GetIntuitSelectedDocuments(out string DocumentIDsForQuery)
        {
            DocumentContextMenu.eContextDocuments oSelectedDocuments = new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            DocumentContextMenu.Common.eContextContainer oDocumentContainer = null;
            DocumentContextMenu.eContextDocument oDocument = null;
            string _DocumentIDsForQuery = "(";
            bool _ispagesource = false;
            System.Collections.ArrayList _selecteddocumentindexs = new System.Collections.ArrayList();

            _selecteddocumentindexs = GetIntuitCheckedDocumentsIndex(out _ispagesource);


            if (_ispagesource == true)
            {
                if (_selecteddocumentindexs != null)
                {
                    for (int i = 0; i <= _selecteddocumentindexs.Count - 1; i++)
                    {
                        int _rowindex = Convert.ToInt32(_selecteddocumentindexs[i].ToString());
                        oDocument = new gloEDocumentV3.DocumentContextMenu.eContextDocument();

                        #region "Document Information"
                        if (oDocument != null)
                        {
                            oDocument.DocumentID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            oDocument.DocumentName = Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTNAME));
                            oDocument.PatientID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_PATIENTID));
                            oDocument.CategoryID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CATEGORYID));
                            oDocument.Category = Convert.ToString(c1Documents.GetData(_rowindex, COL_CATEGORY));
                            oDocument.Year = Convert.ToString(c1Documents.GetData(_rowindex, COL_YEAR));
                            oDocument.Month = Convert.ToString(c1Documents.GetData(_rowindex, COL_MONTH));
                            oDocument.PageCount = Convert.ToInt32(c1Documents.GetData(_rowindex, COL_PAGENUMBERS));
                            oDocument.ClinicID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CLINCID));
                            oDocument.IsAcknowledge = Convert.ToBoolean(c1Documents.GetData(_rowindex, COL_ISACKNOWLEDGE));
                            if (i == 0)
                            {
                                _DocumentIDsForQuery = _DocumentIDsForQuery + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            }
                            else
                            {
                                _DocumentIDsForQuery = _DocumentIDsForQuery + "," + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                            }


                        #endregion

                            #region "Container Information"
                            if (lvwPages != null)
                            {
                                for (int j = 0; j <= lvwPages.SelectedItems.Count - 1; j++)
                                {
                                    string _PageName = Convert.ToString(lvwPages.SelectedItems[j].Text);
                                    Int64 _ContNumber = Convert.ToInt64(lvwPages.SelectedItems[j].SubItems[4].Text);
                                    Int64 _DoctNumber = Convert.ToInt64(lvwPages.SelectedItems[j].SubItems[5].Text);
                                    int _ContPageNumber = Convert.ToInt32(lvwPages.SelectedItems[j].SubItems[2].Text);
                                    int _DoctPageNumber = Convert.ToInt32(lvwPages.SelectedItems[j].SubItems[3].Text);
                                    bool _PageHasNotes = Convert.ToBoolean(lvwPages.SelectedItems[j].SubItems[6].Text);

                                    if (oDocumentContainer != null)
                                    {
                                  //      oDocumentContainer.Dispose();
                                        oDocumentContainer = null;
                                    }
                                    for (int k = 0; k <= oDocument.Containers.Count - 1; k++)
                                    {
                                        if (oDocument.Containers[k].ContainerID == _ContNumber)
                                        {
                                            oDocumentContainer = oDocument.Containers[k];
                                            break;
                                        }
                                    }
                                    if (oDocumentContainer == null)
                                    {
                                        oDocumentContainer = new gloEDocumentV3.DocumentContextMenu.Common.eContextContainer();
                                        oDocumentContainer.ContainerID = _ContNumber;
                                        Document.eBaseContainers _tempcontainers = null;
                                        _tempcontainers = (Document.eBaseContainers)c1Documents.GetData(Convert.ToInt32(_selecteddocumentindexs[i].ToString()), COL_CONATINERS);
                                        if (_tempcontainers != null)
                                        {
                                            for (int l = 0; l <= _tempcontainers.Count - 1; l++)
                                            {
                                                if (_tempcontainers[l].EContainerID == _ContNumber)
                                                {
                                                    oDocumentContainer.DocumentPageFrom = _tempcontainers[l].PageFrom;
                                                    oDocumentContainer.DocumentPageTo = _tempcontainers[l].PageTo;
                                                    break;
                                                }
                                            }
                                          
                                            _tempcontainers.Dispose();
                                            _tempcontainers = null;
                                        }
                                        oDocumentContainer.Pages.Add(_ContPageNumber, _DoctPageNumber, _PageName, _PageHasNotes);
                                        oDocument.Containers.Add(oDocumentContainer);
                                    }
                                    else
                                    {
                                        oDocumentContainer.Pages.Add(_ContPageNumber, _DoctPageNumber, _PageName, _PageHasNotes);
                                    }
                                }
                            }
                            #endregion

                            oSelectedDocuments.Add(oDocument);
                            if (oDocument != null)
                            {
                                //oDocument.Dispose();
                                oDocument = null;
                            }
                        }
                    }
                }
            }

            else
            {
                for (int i = 0; i <= _selecteddocumentindexs.Count - 1; i++)
                {
                    int _rowindex = Convert.ToInt32(_selecteddocumentindexs[i].ToString());
                    oDocument = new gloEDocumentV3.DocumentContextMenu.eContextDocument();

                    #region "Document Information"
                    if (oDocument != null)
                    {
                        oDocument.DocumentID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        oDocument.DocumentName = Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTNAME));
                        oDocument.PatientID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_PATIENTID));
                        oDocument.CategoryID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CATEGORYID));
                        oDocument.Category = Convert.ToString(c1Documents.GetData(_rowindex, COL_CATEGORY));
                        oDocument.Year = Convert.ToString(c1Documents.GetData(_rowindex, COL_YEAR));
                        oDocument.Month = Convert.ToString(c1Documents.GetData(_rowindex, COL_MONTH));
                        oDocument.PageCount = Convert.ToInt32(c1Documents.GetData(_rowindex, COL_PAGENUMBERS));
                        oDocument.ClinicID = Convert.ToInt64(c1Documents.GetData(_rowindex, COL_CLINCID));
                        oDocument.IsAcknowledge = Convert.ToBoolean(c1Documents.GetData(_rowindex, COL_ISACKNOWLEDGE));

                        if (i == 0)
                        {
                            _DocumentIDsForQuery = _DocumentIDsForQuery + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        }
                        else
                        {
                            _DocumentIDsForQuery = _DocumentIDsForQuery + "," + Convert.ToString(c1Documents.GetData(_rowindex, COL_DOCUMENTID));
                        }
                    }
                    #endregion

                    #region "Container Information"
                    Document.eBaseContainers _tempcontainers = null;
                    _tempcontainers = (Document.eBaseContainers)c1Documents.GetData(Convert.ToInt32(_selecteddocumentindexs[i].ToString()), COL_CONATINERS);
                    if (_tempcontainers != null)
                    {
                        for (int j = 0; j <= _tempcontainers.Count - 1; j++)
                        {
                            oDocumentContainer = new gloEDocumentV3.DocumentContextMenu.Common.eContextContainer();
                            if (oDocumentContainer != null)
                            {
                                oDocumentContainer.ContainerID = _tempcontainers[j].EContainerID;
                                oDocumentContainer.DocumentPageFrom = _tempcontainers[j].PageFrom;
                                oDocumentContainer.DocumentPageTo = _tempcontainers[j].PageTo;
                                #region "Document Container Pages"
                                gloEDocumentV3.Document.BasePages oPages = new gloEDocumentV3.Document.BasePages();
                                gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                                oPages = oList.GetBasePages(_tempcontainers[j].EDocumentID, _tempcontainers[j].EContainerID, _tempcontainers[j].ClinicID, _OpenExternalSource);
                                if (oPages != null)
                                {
                                    for (int k = 0; k <= oPages.Count - 1; k++)
                                    {
                                        oDocumentContainer.Pages.Add(oPages[k].ContainerPageNumber, oPages[k].DocumentPageNumber, oPages[k].PageName, oPages[k].HasNotes);
                                    }
                                }
                                if (oList != null)
                                {
                                    oList.Dispose();
                                    oList = null;
                                }
                                if (oPages != null)
                                {
                                    oPages.Dispose();
                                    oPages = null;
                                }
                                #endregion
                                oDocument.Containers.Add(oDocumentContainer);
                                if (oDocumentContainer != null)
                                {
                               //     oDocumentContainer.Dispose();
                                    oDocumentContainer = null;
                                }
                            }
                        }
                        _tempcontainers.Dispose();
                        _tempcontainers = null;
                    }
                    #endregion

                    oSelectedDocuments.Add(oDocument);
                    if (oDocument != null)
                    {
                        //oDocument.Dispose();
                        oDocument = null;
                    }
                }
            }

            if (_DocumentIDsForQuery == "(")
            {
                _DocumentIDsForQuery = _DocumentIDsForQuery + "0)";
            }
            else
            {
                _DocumentIDsForQuery = _DocumentIDsForQuery + ")";
            }
            DocumentIDsForQuery = _DocumentIDsForQuery;
            return oSelectedDocuments;
        }

        

        #endregion ""

        private System.Collections.ArrayList GetlaborderCheckedDocumentsIndex()
        {
            int _CurrentDocumentIndex = 0;
            _CurrentDocumentIndex = c1Documents.RowSel;
            System.Collections.ArrayList _result = new System.Collections.ArrayList();
            //bool _IsPageSource = false;
            if (c1Documents != null)
            {
                for (int d = 0; d <= c1Documents.Rows.Count - 1; d++)
                {
                    if (d >= 0)
                    {
                        if (c1Documents.GetData(d, COL_COLTYPE) != null)
                        {
                            if ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(d, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                            {
                                if (c1Documents.GetCellCheck(d, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    _result.Add(d);
                                   // _IsPageSource = false;
                                }
                            }
                        }
                    }
                }

            }
          //  IsPageSource = _IsPageSource;
            return _result;
        }
        #endregion

        #region "Fill Context Menu"

        #region "Dhruv 20100626 -> Fill Context Menu"
        
        private void FillContextMenu(Int64 patientid, Int64 clinicid, string selectedyear, bool ispagemenu)
        {
            DocumentContextMenu.eDocV3ContextMenuStrip oConextMenuStrip = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuStrip();
            DocumentContextMenu.eDocV3ContextMenuItem oContextMenuCategory = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
            DocumentContextMenu.eDocV3ContextMenuItem oContextMenuDocument = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();

            Common.Categories oCategories = new gloEDocumentV3.Common.Categories();
            Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
            eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            string _ExceptDocumentIDs = "";
            int _CatCntr = 0;
            int _DocCntr = 0;

            _IsDocumentsLoading = true;

            try
            {
                oSelectedDocuments = GetSelectedDocuments(out _ExceptDocumentIDs);
                oConextMenuStrip = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuStrip();
                oConextMenuStrip.ESelectedDocuments = oSelectedDocuments;

                #region "View Pages"
                if (ispagemenu == true)
                {
                    #region "View Main Menu"
                    oContextMenuCategory = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    oContextMenuCategory.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_View;
                    oContextMenuCategory.Image = Properties.Resources.DocumentView;
                    #region "Event Parameters"
                    oContextMenuCategory.EContextEventParameter.PatientID = patientid;
                    oContextMenuCategory.EContextEventParameter.ContainerID = 0;
                    oContextMenuCategory.EContextEventParameter.DocumentID = 0;
                    oContextMenuCategory.EContextEventParameter.CategoryID = 0;
                    oContextMenuCategory.EContextEventParameter.Category = "";
                    oContextMenuCategory.EContextEventParameter.Year = "";
                    oContextMenuCategory.EContextEventParameter.Month = "";
                    oContextMenuCategory.EContextEventParameter.DocumentName = "";
                    oContextMenuCategory.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.None;
                    oContextMenuCategory.EContextEventParameter.ClinicID = clinicid;
                    oContextMenuCategory.EContextEventParameter.IsPageMenu = ispagemenu;
                    #endregion
                    #endregion

                    #region "View Large Icon"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_LargeIcon;
                        if (lvwPages.View == View.LargeIcon)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewLargeIcon;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View Small Icon"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_SmallIcon;
                        if (lvwPages.View == View.SmallIcon)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewSmallIcon;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View List"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_List;
                        if (lvwPages.View == View.List)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewList;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View Tile"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_Tiles;
                        if (lvwPages.View == View.Tile)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewTile;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                        oContextMenuDocument = null;
                    #endregion

                        oConextMenuStrip.Items.Add(oContextMenuCategory);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuCategory.Dispose();
                    //    oContextMenuCategory = null;
                    //}
                }
                #endregion

                #region "Print"
                oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                if (oContextMenuDocument != null)
                {
                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Print;
                    oContextMenuDocument.Image = Properties.Resources.Print_All;
                    #region "Event Parameters"
                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                    oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                    oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                    oContextMenuDocument.EContextEventParameter.Category = "";
                    oContextMenuDocument.EContextEventParameter.Year = "";
                    oContextMenuDocument.EContextEventParameter.Month = "";
                    oContextMenuDocument.EContextEventParameter.DocumentName = "";
                    if (ispagemenu == true)
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintPages;
                    }
                    else
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintDocument;
                    }
                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                    #endregion
                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                }
                //if (oContextMenuDocument != null)
                //{
                //    oContextMenuDocument.Dispose();
                //    oContextMenuDocument = null;
                //}
                #endregion

                #region "Fax"
                oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                if (oContextMenuDocument != null)
                {
                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Fax;
                    oContextMenuDocument.Image = Properties.Resources.Fax_All;
                    #region "Event Parameters"
                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                    oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                    oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                    oContextMenuDocument.EContextEventParameter.Category = "";
                    oContextMenuDocument.EContextEventParameter.Year = "";
                    oContextMenuDocument.EContextEventParameter.Month = "";
                    oContextMenuDocument.EContextEventParameter.DocumentName = "";
                    if (ispagemenu == true)
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.FaxPages;
                    }
                    else
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.FaxDocument;
                    }
                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                    #endregion
                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                }
                //if (oContextMenuDocument != null)
                //{
                //    oContextMenuDocument.Dispose();
                //    oContextMenuDocument = null;
                //}
                #endregion

                #region "Rename"
                if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    if (oSelectedDocuments.Count == 1)
                    {
                        bool _fillmenu = false;
                        if (ispagemenu == true)
                        { if (oSelectedDocuments[0].Containers[0].Pages.Count == 1) { _fillmenu = true; } }
                        else { _fillmenu = true; }

                        if (_fillmenu == true)
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Rename;
                                oContextMenuDocument.Image = Properties.Resources.Rename;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                if (ispagemenu == true)
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument;
                                }
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                }
                else
                {
                    if (_OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument)
                    {
                        if (oSelectedDocuments.Count == 1)
                        {
                            bool _fillmenu = false;
                            if (ispagemenu == true)
                            { if (oSelectedDocuments[0].Containers[0].Pages.Count == 1) { _fillmenu = true; } }
                            else { _fillmenu = true; }

                            if (_fillmenu == true)
                            {
                                oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                if (oContextMenuDocument != null)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Rename;
                                    oContextMenuDocument.Image = Properties.Resources.Rename;
                                    #region "Event Parameters"
                                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                    oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                    oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                    oContextMenuDocument.EContextEventParameter.Category = "";
                                    oContextMenuDocument.EContextEventParameter.Year = "";
                                    oContextMenuDocument.EContextEventParameter.Month = "";
                                    oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                    if (ispagemenu == true)
                                    {
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages;
                                    }
                                    else
                                    {
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument;
                                    }
                                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                    #endregion
                                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                }
                                //if (oContextMenuDocument != null)
                                //{
                                //    oContextMenuDocument.Dispose();
                                //    oContextMenuDocument = null;
                                //}
                            }
                        }
                    }
                }
                #endregion
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {

                    #region "UnAcknowledge"
                    if (oSelectedDocuments.Count == 1)
                    {
                        if (ispagemenu == false)
                        {

                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_UnAcknowledge;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentReviwed;


                                    #region "Event Parameters"
                                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                    //code added by dipak 20090820 for solv problem of index outof range after right clicking on large document 
                                    //problem occure when solve Bug #2680
                                    if (oSelectedDocuments[0].Containers.Count > 0)
                                    {
                                        oContextMenuDocument.EContextEventParameter.ContainerID = oSelectedDocuments[0].Containers[0].ContainerID;
                                    }
                                    else
                                    {
                                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                    }
                                    oContextMenuDocument.EContextEventParameter.DocumentID = oSelectedDocuments[0].DocumentID;
                                    oContextMenuDocument.EContextEventParameter.CategoryID = oSelectedDocuments[0].CategoryID;
                                    oContextMenuDocument.EContextEventParameter.Category = oSelectedDocuments[0].Category;
                                    oContextMenuDocument.EContextEventParameter.Year = oSelectedDocuments[0].Year;
                                    oContextMenuDocument.EContextEventParameter.Month = oSelectedDocuments[0].Month;
                                    oContextMenuDocument.EContextEventParameter.DocumentName = oSelectedDocuments[0].DocumentName;

                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.UnAcknowledgeDocument;

                                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                    #endregion
                                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                }
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Acknowledge"
                    if (oSelectedDocuments.Count >= 1)
                    {
                        if (ispagemenu == false)
                        {

                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentReviwed;
                                }
                                else
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentAcknowledge;
                                }

                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                //code added by dipak 20090820 for solv problem of index outof range after right clicking on large document 
                                //problem occure when solve Bug #2680
                                if (oSelectedDocuments[0].Containers.Count > 0)
                                {
                                    oContextMenuDocument.EContextEventParameter.ContainerID = oSelectedDocuments[0].Containers[0].ContainerID;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                }
                                oContextMenuDocument.EContextEventParameter.DocumentID = oSelectedDocuments[0].DocumentID;
                                oContextMenuDocument.EContextEventParameter.CategoryID = oSelectedDocuments[0].CategoryID;
                                oContextMenuDocument.EContextEventParameter.Category = oSelectedDocuments[0].Category;
                                oContextMenuDocument.EContextEventParameter.Year = oSelectedDocuments[0].Year;
                                oContextMenuDocument.EContextEventParameter.Month = oSelectedDocuments[0].Month;
                                oContextMenuDocument.EContextEventParameter.DocumentName = oSelectedDocuments[0].DocumentName;
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgeDocument;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgeDocument;
                                }
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Select All & Unselect All"
                    if (ispagemenu == true)
                    {
                        if (lvwPages.Items.Count == lvwPages.SelectedItems.Count)
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_UnselectAll;
                                oContextMenuDocument.Image = Properties.Resources.DocumentUnselectAll;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.UnselectAll;
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                        else
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_SelectAll;
                                oContextMenuDocument.Image = Properties.Resources.DocumentSelectAll;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SelectAll;
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Categorization"
                    if (_OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument)
                    {
                        if (oSelectedDocuments.Count <= 1)
                        {
                            //oCategories = new gloEDocumentV3.Common.Categories();
                            if (oList != null)
                            {
                                oCategories = oList.GetCategories(gloEDocV3Admin.gClinicID, _OpenExternalSource);
                            }

                            for (_CatCntr = 0; _CatCntr <= oCategories.Count - 1; _CatCntr++)
                            {
                                //oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                                if (oList != null)
                                {
                                    oBaseDocuments = oList.GetBaseDocumentsExcepts(patientid, oCategories[_CatCntr].CategoryName, selectedyear, _ExceptDocumentIDs, clinicid, _OpenExternalSource);
                                }
                                #region "Category Menu"
                                oContextMenuCategory = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                if (oContextMenuCategory != null)
                                {

                                    oContextMenuCategory.Text = oCategories[_CatCntr].CategoryName.Replace("&", "&&");
                                    oContextMenuCategory.Image = Properties.Resources.Category;
                                  
                                    #region "Event Parameters"
                                    if (oBaseDocuments.Count <= 0)
                                    {
                                        oContextMenuCategory.EContextEventParameter.PatientID = patientid;
                                        oContextMenuCategory.EContextEventParameter.ContainerID = 0;
                                        oContextMenuCategory.EContextEventParameter.DocumentID = 0;
                                        oContextMenuCategory.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                        oContextMenuCategory.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                        oContextMenuCategory.EContextEventParameter.Year = selectedyear;
                                        oContextMenuCategory.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                        oContextMenuCategory.EContextEventParameter.DocumentName = "";
                                        oContextMenuCategory.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument;
                                        oContextMenuCategory.EContextEventParameter.ClinicID = clinicid;
                                        oContextMenuCategory.EContextEventParameter.IsPageMenu = ispagemenu;
                                        oContextMenuCategory.Click += new EventHandler(oContextMenuDocument_Click);
                                    }
                                }

                                    #endregion
                                #endregion

                                #region "Documents"
                                if (oBaseDocuments != null && oBaseDocuments.Count > 0)
                                {
                                    #region "Send to New Document"
                                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                    if (oContextMenuDocument != null)
                                    {
                                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_SendToNewFile;
                                        oContextMenuDocument.Image = Properties.Resources.DocumentSendToNew;
                                        #region "Event Parameters"
                                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                        oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                        oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                        oContextMenuDocument.EContextEventParameter.Year = selectedyear;
                                        oContextMenuDocument.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument;
                                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                        #endregion

                                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                    }
                                    //if (oContextMenuDocument != null)
                                    //{
                                    //    oContextMenuDocument.Dispose();
                                    //    oContextMenuDocument = null;
                                    //}
                                    #endregion

                                    oContextMenuCategory.DropDownItems.Add("-");

                                    #region "Send to Existing Document"
                                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                    if (oContextMenuDocument != null)
                                    {
                                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_MergeInExisting;
                                        oContextMenuDocument.Image = Properties.Resources.DocumentMergeInExisting;
                                        #region "Event Parameters"
                                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                        oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                        oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                        oContextMenuDocument.EContextEventParameter.Year = selectedyear;
                                        oContextMenuDocument.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument;
                                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                        #endregion

                                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                    }
                                    //if (oContextMenuDocument != null)
                                    //{
                                    //    oContextMenuDocument.Dispose();
                                    //    oContextMenuDocument = null;
                                    //}
                                    #endregion

                                    oContextMenuCategory.DropDownItems.Add("-");

                                    #region "Send To Existing Documents with Names"
                                    for (_DocCntr = 0; _DocCntr <= oBaseDocuments.Count - 1; _DocCntr++)
                                    {
                                        oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                        if (oContextMenuDocument != null)
                                        {
                                            oContextMenuDocument.Text = oBaseDocuments[_DocCntr].DocumentName;
                                            oContextMenuDocument.Image = Properties.Resources.DocumentMergeInExisting;
                                            #region "Event Parameters"
                                            oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                            //Start:/Error while there is no data in the Document[object reference is null] while right clicking 
                                            if (oBaseDocuments[_DocCntr] != null)
                                            {
                                                if (oBaseDocuments[_DocCntr].EContainers.Count > 0)
                                                {
                                                    oContextMenuDocument.EContextEventParameter.ContainerID = oBaseDocuments[_DocCntr].EContainers[0].EContainerID;

                                                }
                                                else
                                                { continue; }
                                            }
                                            else
                                            { continue; }
                                            //End:/Error while there is no data in the Document.
                                            oContextMenuDocument.EContextEventParameter.DocumentID = oBaseDocuments[_DocCntr].EDocumentID;
                                            oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                            oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                            oContextMenuDocument.EContextEventParameter.Year = oBaseDocuments[_DocCntr].Year;
                                            oContextMenuDocument.EContextEventParameter.Month = oBaseDocuments[_DocCntr].Month;
                                            oContextMenuDocument.EContextEventParameter.DocumentName = oBaseDocuments[_DocCntr].DocumentName;
                                            oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName;
                                            oContextMenuDocument.EContextEventParameter.ClinicID = oBaseDocuments[_DocCntr].ClinicID;
                                            oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                            #endregion

                                            oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                            oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                        }
                                        //if (oContextMenuDocument != null)
                                        //{
                                        //    oContextMenuDocument.Dispose();
                                        //    oContextMenuDocument = null;
                                        //}
                                    }
                                    #endregion
                                }
                                #endregion

                                oConextMenuStrip.Items.Add(oContextMenuCategory);

                                //if (oContextMenuCategory != null)
                                //{
                                //    oContextMenuCategory.Dispose();
                                //    oContextMenuCategory = null;
                                //}
                                if (oBaseDocuments != null)
                                {
                                    oBaseDocuments.Dispose();
                                    oBaseDocuments = null;
                                }
                            }
                            if (oCategories != null)
                            {
                                oCategories.Dispose();
                                oCategories = null;
                            }
                        }
                    }
                    #endregion


                }
                #region "Assign Context Menu Strip"
                if (ispagemenu == true)
                {
                    if (lvwPages.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(lvwPages.ContextMenuStrip); lvwPages.ContextMenuStrip.Dispose(); }

                    lvwPages.ContextMenuStrip = oConextMenuStrip;
                }
                else
                {
                    if (c1Documents.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(c1Documents.ContextMenuStrip); c1Documents.ContextMenuStrip.Dispose(); }

                    c1Documents.ContextMenuStrip = oConextMenuStrip;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oBaseDocuments != null)
                {
                    oBaseDocuments.Dispose();
                    oBaseDocuments = null;
                }
                if (oCategories != null)
                {
                    oCategories.Dispose();
                    oCategories = null;
                }
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                _IsDocumentsLoading = false;
            }
        }
        
        private void FillContextMenu_RCM(Int64 patientid, Int64 clinicid, string selectedyear, bool ispagemenu)
        {
            DocumentContextMenu.eDocV3ContextMenuStrip oConextMenuStrip = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuStrip();
            DocumentContextMenu.eDocV3ContextMenuItem oContextMenuCategory = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
            DocumentContextMenu.eDocV3ContextMenuItem oContextMenuDocument = null;//new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            //DocumentContextMenu.eDocV3ContextMenuItem oContextMenuSubCategory = null;

            Common.Categories oCategories = new gloEDocumentV3.Common.Categories();
            Common.SubCategories oSubCategories = new gloEDocumentV3.Common.SubCategories();
            Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
            eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            string _ExceptDocumentIDs = "";
            int _CatCntr = 0;
            int _DocCntr = 0;

            _IsDocumentsLoading = true;

            try
            {
                oSelectedDocuments = GetSelectedDocuments(out _ExceptDocumentIDs);
                oConextMenuStrip = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuStrip();
                oConextMenuStrip.ESelectedDocuments = oSelectedDocuments;

                #region "View Pages"
                if (ispagemenu == true)
                {
                    #region "View Main Menu"
                    oContextMenuCategory = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    oContextMenuCategory.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_View;
                    oContextMenuCategory.Image = Properties.Resources.DocumentView;
                    #region "Event Parameters"
                    oContextMenuCategory.EContextEventParameter.PatientID = patientid;
                    oContextMenuCategory.EContextEventParameter.ContainerID = 0;
                    oContextMenuCategory.EContextEventParameter.DocumentID = 0;
                    oContextMenuCategory.EContextEventParameter.CategoryID = 0;
                    oContextMenuCategory.EContextEventParameter.Category = "";
                    oContextMenuCategory.EContextEventParameter.Year = "";
                    oContextMenuCategory.EContextEventParameter.Month = "";
                    oContextMenuCategory.EContextEventParameter.DocumentName = "";
                    oContextMenuCategory.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.None;
                    oContextMenuCategory.EContextEventParameter.ClinicID = clinicid;
                    oContextMenuCategory.EContextEventParameter.IsPageMenu = ispagemenu;
                    #endregion
                    #endregion

                    #region "View Large Icon"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_LargeIcon;
                        if (lvwPages.View == View.LargeIcon)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewLargeIcon;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View Small Icon"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_SmallIcon;
                        if (lvwPages.View == View.SmallIcon)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewSmallIcon;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View List"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_List;
                        if (lvwPages.View == View.List)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewList;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuDocument.Dispose();
                    //    oContextMenuDocument = null;
                    //}
                    #endregion

                    #region "View Tile"
                    oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                    if (oContextMenuDocument != null)
                    {
                        oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_View_Tiles;
                        if (lvwPages.View == View.Tile)
                        { oContextMenuDocument.Image = Properties.Resources.ViewSelectedItem; }
                        else { oContextMenuDocument.Image = Properties.Resources.ViewNotSelectedItem; }
                        #region "Event Parameters"
                        oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                        oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                        oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                        oContextMenuDocument.EContextEventParameter.Category = "";
                        oContextMenuDocument.EContextEventParameter.Year = "";
                        oContextMenuDocument.EContextEventParameter.Month = "";
                        oContextMenuDocument.EContextEventParameter.DocumentName = "";
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.ViewTile;
                        oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                        oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                        #endregion
                        oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                        oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                        oContextMenuDocument = null;
                    #endregion

                        oConextMenuStrip.Items.Add(oContextMenuCategory);
                    }
                    //if (oContextMenuDocument != null)
                    //{
                    //    oContextMenuCategory.Dispose();
                    //    oContextMenuCategory = null;
                    //}
                }
                #endregion

                #region "Print"
                oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                if (oContextMenuDocument != null)
                {
                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Print;
                    oContextMenuDocument.Image = Properties.Resources.Print_All;
                    #region "Event Parameters"
                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                    oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                    oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                    oContextMenuDocument.EContextEventParameter.Category = "";
                    oContextMenuDocument.EContextEventParameter.Year = "";
                    oContextMenuDocument.EContextEventParameter.Month = "";
                    oContextMenuDocument.EContextEventParameter.DocumentName = "";
                    if (ispagemenu == true)
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintPages;
                    }
                    else
                    {
                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintDocument;
                    }
                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                    #endregion
                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                }
                //if (oContextMenuDocument != null)
                //{
                //    oContextMenuDocument.Dispose();
                //    oContextMenuDocument = null;
                //}
                #endregion

                #region "Rename"
                if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    if (oSelectedDocuments.Count == 1)
                    {
                        bool _fillmenu = false;
                        if (ispagemenu == true)
                        { if (oSelectedDocuments[0].Containers[0].Pages.Count == 1) { _fillmenu = true; } }
                        else { _fillmenu = true; }

                        if (_fillmenu == true)
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Rename;
                                oContextMenuDocument.Image = Properties.Resources.Rename;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                if (ispagemenu == true)
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument;
                                }
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                }
                else
                {
                    if (_OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument)
                    {
                        if (oSelectedDocuments.Count == 1)
                        {
                            bool _fillmenu = false;
                            if (ispagemenu == true)
                            { if (oSelectedDocuments[0].Containers[0].Pages.Count == 1) { _fillmenu = true; } }
                            else { _fillmenu = true; }

                            if (_fillmenu == true)
                            {
                                oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                if (oContextMenuDocument != null)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Rename;
                                    oContextMenuDocument.Image = Properties.Resources.Rename;
                                    #region "Event Parameters"
                                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                    oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                    oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                    oContextMenuDocument.EContextEventParameter.Category = "";
                                    oContextMenuDocument.EContextEventParameter.Year = "";
                                    oContextMenuDocument.EContextEventParameter.Month = "";
                                    oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                    if (ispagemenu == true)
                                    {
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages;
                                    }
                                    else
                                    {
                                        oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument;
                                    }
                                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                    #endregion
                                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                }
                                //if (oContextMenuDocument != null)
                                //{
                                //    oContextMenuDocument.Dispose();
                                //    oContextMenuDocument = null;
                                //}
                            }
                        }
                    }
                }
                #endregion
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {

                    #region "UnAcknowledge"
                    if (oSelectedDocuments.Count == 1)
                    {
                        if (ispagemenu == false)
                        {

                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_UnAcknowledge;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentReviwed;


                                    #region "Event Parameters"
                                    oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                    //code added by dipak 20090820 for solv problem of index outof range after right clicking on large document 
                                    //problem occure when solve Bug #2680
                                    if (oSelectedDocuments[0].Containers.Count > 0)
                                    {
                                        oContextMenuDocument.EContextEventParameter.ContainerID = oSelectedDocuments[0].Containers[0].ContainerID;
                                    }
                                    else
                                    {
                                        oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                    }
                                    oContextMenuDocument.EContextEventParameter.DocumentID = oSelectedDocuments[0].DocumentID;
                                    oContextMenuDocument.EContextEventParameter.CategoryID = oSelectedDocuments[0].CategoryID;
                                    oContextMenuDocument.EContextEventParameter.Category = oSelectedDocuments[0].Category;
                                    oContextMenuDocument.EContextEventParameter.Year = oSelectedDocuments[0].Year;
                                    oContextMenuDocument.EContextEventParameter.Month = oSelectedDocuments[0].Month;
                                    oContextMenuDocument.EContextEventParameter.DocumentName = oSelectedDocuments[0].DocumentName;
                                    
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.UnAcknowledgeDocument;

                                    oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                    oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                    #endregion
                                    oConextMenuStrip.Items.Add(oContextMenuDocument);
                                    oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                }
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Acknowledge"
                    if (oSelectedDocuments.Count >= 1)
                    {
                        if (ispagemenu == false)
                        {

                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentReviwed;
                                }
                                else
                                {
                                    oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                    oContextMenuDocument.Image = Properties.Resources.DocumentAcknowledge;
                                }

                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                //code added by dipak 20090820 for solv problem of index outof range after right clicking on large document 
                                //problem occure when solve Bug #2680
                                if (oSelectedDocuments[0].Containers.Count > 0)
                                {
                                    oContextMenuDocument.EContextEventParameter.ContainerID = oSelectedDocuments[0].Containers[0].ContainerID;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                }
                                oContextMenuDocument.EContextEventParameter.DocumentID = oSelectedDocuments[0].DocumentID;
                                oContextMenuDocument.EContextEventParameter.CategoryID = oSelectedDocuments[0].CategoryID;
                                oContextMenuDocument.EContextEventParameter.Category = oSelectedDocuments[0].Category;
                                oContextMenuDocument.EContextEventParameter.Year = oSelectedDocuments[0].Year;
                                oContextMenuDocument.EContextEventParameter.Month = oSelectedDocuments[0].Month;
                                oContextMenuDocument.EContextEventParameter.DocumentName = oSelectedDocuments[0].DocumentName;
                                if (oSelectedDocuments[0].IsAcknowledge == true)
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgeDocument;
                                }
                                else
                                {
                                    oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgeDocument;
                                }
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Select All & Unselect All"
                    if (ispagemenu == true)
                    {
                        if (lvwPages.Items.Count == lvwPages.SelectedItems.Count)
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_UnselectAll;
                                oContextMenuDocument.Image = Properties.Resources.DocumentUnselectAll;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.UnselectAll;
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                        else
                        {
                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                            if (oContextMenuDocument != null)
                            {
                                oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_SelectAll;
                                oContextMenuDocument.Image = Properties.Resources.DocumentSelectAll;
                                #region "Event Parameters"
                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                                oContextMenuDocument.EContextEventParameter.Category = "";
                                oContextMenuDocument.EContextEventParameter.Year = "";
                                oContextMenuDocument.EContextEventParameter.Month = "";
                                oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SelectAll;
                                oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                #endregion
                                oConextMenuStrip.Items.Add(oContextMenuDocument);
                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                            }
                            //if (oContextMenuDocument != null)
                            //{
                            //    oContextMenuDocument.Dispose();
                            //    oContextMenuDocument = null;
                            //}
                        }
                    }
                    #endregion

                    #region "Send To DMS"

                    if (oSelectedDocuments.Count <= 1)
                    {
                        oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                        if (oContextMenuDocument != null)
                        {
                            oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_SendToDMS;
                            oContextMenuDocument.Image = Properties.Resources.SendtoDMSFiles;
                            #region "Event Parameters"
                            oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                            oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                            oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                            oContextMenuDocument.EContextEventParameter.CategoryID = 0;
                            oContextMenuDocument.EContextEventParameter.Category = "";
                            oContextMenuDocument.EContextEventParameter.Year = "";
                            oContextMenuDocument.EContextEventParameter.Month = "";
                            oContextMenuDocument.EContextEventParameter.DocumentName = "";
                            oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToDMS;
                            oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                            oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                            #endregion
                            oConextMenuStrip.Items.Add(oContextMenuDocument);
                            oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                        }
                    }

                    #endregion

                    #region "Categorization"
                    if (_OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument)
                    {
                        if (oSelectedDocuments.Count <= 1)
                        {
                            //oCategories = new gloEDocumentV3.Common.Categories();
                            if (oList != null)
                            {
                                oCategories = oList.GetCategories(gloEDocV3Admin.gClinicID, _OpenExternalSource);
                            }

                            for (_CatCntr = 0; _CatCntr <= oCategories.Count - 1; _CatCntr++)
                            {
                                //oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                                if (oList != null)
                                {
                                    oBaseDocuments = oList.GetBaseDocumentsExcepts(patientid, oCategories[_CatCntr].CategoryName, selectedyear, _ExceptDocumentIDs, clinicid, _OpenExternalSource);
                                }
                                #region "Category Menu"
                                oContextMenuCategory = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                if (oContextMenuCategory != null)
                                {
                                    
                                        oContextMenuCategory.Text = oCategories[_CatCntr].CategoryName.Replace("&","&&");
                                    oContextMenuCategory.Image = Properties.Resources.Category;
                                    #region "Event Parameters"
                                    if (oBaseDocuments.Count <= 0)
                                    {
                                        oContextMenuCategory.EContextEventParameter.PatientID = patientid;
                                        oContextMenuCategory.EContextEventParameter.ContainerID = 0;
                                        oContextMenuCategory.EContextEventParameter.DocumentID = 0;
                                        oContextMenuCategory.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                        oContextMenuCategory.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                        oContextMenuCategory.EContextEventParameter.Year = selectedyear;
                                        oContextMenuCategory.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                        oContextMenuCategory.EContextEventParameter.DocumentName = "";
                                        oContextMenuCategory.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument;
                                        oContextMenuCategory.EContextEventParameter.ClinicID = clinicid;
                                        oContextMenuCategory.EContextEventParameter.IsPageMenu = ispagemenu;
                                        oContextMenuCategory.Click += new EventHandler(oContextMenuDocument_Click);
                                    }
                                }


                                    #endregion
                                #endregion




                                #region "SubCategory Menu"

                                oSubCategories = oList.GetSubCategories(gloEDocV3Admin.gClinicID, oCategories[_CatCntr].CategoryID, selectedyear, flgShowAckDocs);

                                for (int _SubCatCntr = 0; _SubCatCntr <= oSubCategories.Count - 1; _SubCatCntr++)
                                {
                                    DocumentContextMenu.eDocV3ContextMenuItem oContextMenuSubCategory = null;

                                    oBaseDocuments = oList.GetSubCategoryBaseDocumentsExcepts(patientid, oCategories[_CatCntr].CategoryName, oSubCategories[_SubCatCntr].SubCategoryName, selectedyear, _ExceptDocumentIDs, clinicid, _OpenExternalSource, !flgShowAckDocs);

                                    if (!string.IsNullOrEmpty(oSubCategories[_SubCatCntr].SubCategoryName))
                                    {
                                        oContextMenuSubCategory = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();

                                        if (oContextMenuSubCategory != null)
                                        {
                                            oContextMenuSubCategory.Text = oSubCategories[_SubCatCntr].SubCategoryName;
                                            oContextMenuSubCategory.Image = Properties.Resources.RCMSubCategory;

                                            oContextMenuCategory.DropDownItems.Add(oContextMenuSubCategory);
                                            oContextMenuSubCategory.Click += new EventHandler(oContextMenuDocument_Click);

                                            #region "Event Parameters"
                                            if (oBaseDocuments.Count <= 0)
                                            {
                                                oContextMenuSubCategory.EContextEventParameter.PatientID = patientid;
                                                oContextMenuSubCategory.EContextEventParameter.ContainerID = 0;
                                                oContextMenuSubCategory.EContextEventParameter.DocumentID = 0;
                                                oContextMenuSubCategory.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                                oContextMenuSubCategory.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                                oContextMenuSubCategory.EContextEventParameter.SubCategory = oSubCategories[_SubCatCntr].SubCategoryName;
                                                oContextMenuSubCategory.EContextEventParameter.Year = selectedyear;
                                                oContextMenuSubCategory.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                                oContextMenuSubCategory.EContextEventParameter.DocumentName = "";
                                                oContextMenuSubCategory.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument;
                                                oContextMenuSubCategory.EContextEventParameter.ClinicID = clinicid;
                                                oContextMenuSubCategory.EContextEventParameter.IsPageMenu = ispagemenu;
                                                oContextMenuSubCategory.Click += new EventHandler(oContextMenuDocument_Click);
                                            }
                                        }
                                    }

                                    
                                    //Boolean bIsSubCategory = false;
                                    //ArrayList iSubCatList = new ArrayList();

                                    #region "Documents"
                                    if (oBaseDocuments != null && oBaseDocuments.Count > 0)
                                    {

                                        #region "Send to New Document"
                                        oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                        if (oContextMenuDocument != null)
                                        {
                                            oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_SendToNewFile;
                                            oContextMenuDocument.Image = Properties.Resources.DocumentSendToNew;
                                            #region "Event Parameters"
                                            oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                            oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                            oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                            oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                            oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                            oContextMenuDocument.EContextEventParameter.SubCategory = oSubCategories[_SubCatCntr].SubCategoryName;
                                            oContextMenuDocument.EContextEventParameter.Year = selectedyear;
                                            oContextMenuDocument.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                            oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                            oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument;
                                            oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                            oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                            #endregion

                                            if (oContextMenuSubCategory != null)
                                            {
                                                oContextMenuSubCategory.DropDownItems.Add(oContextMenuDocument);
                                            }
                                            else
                                            {
                                                oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                            }

                                            oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                        }
                                        //if (oContextMenuDocument != null)
                                        //{
                                        //    oContextMenuDocument.Dispose();
                                        //    oContextMenuDocument = null;
                                        //}
                                        #endregion

                                        if (oContextMenuSubCategory != null)
                                        {
                                            oContextMenuSubCategory.DropDownItems.Add("-");
                                        }
                                        else
                                        {
                                            oContextMenuCategory.DropDownItems.Add("-");
                                        }


                                        #region "Send to Existing Document"
                                        oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                        if (oContextMenuDocument != null)
                                        {
                                            oContextMenuDocument.Text = gloEDocumentV3.DocumentContextMenu.ContextMenuNames.gDocMenu_MergeInExisting;
                                            oContextMenuDocument.Image = Properties.Resources.DocumentMergeInExisting;
                                            #region "Event Parameters"
                                            oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                            oContextMenuDocument.EContextEventParameter.ContainerID = 0;
                                            oContextMenuDocument.EContextEventParameter.DocumentID = 0;
                                            oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                            oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                            oContextMenuDocument.EContextEventParameter.SubCategory = oSubCategories[_SubCatCntr].SubCategoryName;
                                            oContextMenuDocument.EContextEventParameter.Year = selectedyear;
                                            oContextMenuDocument.EContextEventParameter.Month = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                                            oContextMenuDocument.EContextEventParameter.DocumentName = "";
                                            oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument;
                                            oContextMenuDocument.EContextEventParameter.ClinicID = clinicid;
                                            oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                            #endregion

                                            if (oContextMenuSubCategory != null)
                                            {
                                                oContextMenuSubCategory.DropDownItems.Add(oContextMenuDocument);
                                            }
                                            else
                                            {
                                                oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                            }

                                            oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                        }
                                        //if (oContextMenuDocument != null)
                                        //{
                                        //    oContextMenuDocument.Dispose();
                                        //    oContextMenuDocument = null;
                                        //}
                                        #endregion

                                        if (oContextMenuSubCategory != null)
                                        {
                                            oContextMenuSubCategory.DropDownItems.Add("-");
                                        }
                                        else
                                        {
                                            oContextMenuCategory.DropDownItems.Add("-");
                                        }


                                        #region "Send To Existing Documents with Names"
                                        for (_DocCntr = 0; _DocCntr <= oBaseDocuments.Count - 1; _DocCntr++)
                                        {
                                            oContextMenuDocument = new gloEDocumentV3.DocumentContextMenu.eDocV3ContextMenuItem();
                                            if (oContextMenuDocument != null)
                                            {
                                                oContextMenuDocument.Text = oBaseDocuments[_DocCntr].DocumentName;
                                                oContextMenuDocument.Image = Properties.Resources.DocumentMergeInExisting;
                                                #region "Event Parameters"
                                                oContextMenuDocument.EContextEventParameter.PatientID = patientid;
                                                //Start:/Error while there is no data in the Document[object reference is null] while right clicking 
                                                if (oBaseDocuments[_DocCntr] != null)
                                                {
                                                    if (oBaseDocuments[_DocCntr].EContainers.Count > 0)
                                                    {
                                                        oContextMenuDocument.EContextEventParameter.ContainerID = oBaseDocuments[_DocCntr].EContainers[0].EContainerID;

                                                    }
                                                    else
                                                    { continue; }
                                                }
                                                else
                                                { continue; }
                                                //End:/Error while there is no data in the Document.
                                                oContextMenuDocument.EContextEventParameter.DocumentID = oBaseDocuments[_DocCntr].EDocumentID;
                                                oContextMenuDocument.EContextEventParameter.CategoryID = oCategories[_CatCntr].CategoryID;
                                                oContextMenuDocument.EContextEventParameter.Category = oCategories[_CatCntr].CategoryName;
                                                oContextMenuDocument.EContextEventParameter.SubCategory = oSubCategories[_SubCatCntr].SubCategoryName;
                                                oContextMenuDocument.EContextEventParameter.Year = oBaseDocuments[_DocCntr].Year;
                                                oContextMenuDocument.EContextEventParameter.Month = oBaseDocuments[_DocCntr].Month;
                                                oContextMenuDocument.EContextEventParameter.DocumentName = oBaseDocuments[_DocCntr].DocumentName;
                                                oContextMenuDocument.EContextEventParameter.EventType = gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName;
                                                oContextMenuDocument.EContextEventParameter.ClinicID = oBaseDocuments[_DocCntr].ClinicID;
                                                oContextMenuDocument.EContextEventParameter.IsPageMenu = ispagemenu;
                                                #endregion

                                                if (oContextMenuSubCategory != null)
                                                {
                                                    oContextMenuSubCategory.DropDownItems.Add(oContextMenuDocument);
                                                }
                                                else
                                                {
                                                    oContextMenuCategory.DropDownItems.Add(oContextMenuDocument);
                                                }

                                                oContextMenuDocument.Click += new EventHandler(oContextMenuDocument_Click);
                                            }
                                            //if (oContextMenuDocument != null)
                                            //{
                                            //    oContextMenuDocument.Dispose();
                                            //    oContextMenuDocument = null;
                                            //}
                                        }
                                        #endregion
                                    }
                                    #endregion

                                    //if (bIsSubCategory)
                                    //{
                                    //    oConextMenuStrip.Items.Add(oContextMenuSubCategory);
                                    //}
                                    //else
                                    //{

                                    if (oSubCategories.Count > 0)
                                    { 
                                        oConextMenuStrip.Items.Add(oContextMenuCategory);
                                    }
                                    //}

                                    //if (oContextMenuSubCategory != null)
                                    //{
                                    //    oContextMenuSubCategory.Dispose();
                                    //    oContextMenuSubCategory = null;
                                    //}

                                    //if (oContextMenuCategory != null)
                                    //{
                                    //    oContextMenuCategory.Dispose();
                                    //    oContextMenuCategory = null;
                                    //}

                                    if (oBaseDocuments != null)
                                    {
                                        oBaseDocuments.Dispose();
                                        oBaseDocuments = null;
                                    }


                                }

                                if (oSubCategories.Count <= 0)
                                {
                                    oConextMenuStrip.Items.Add(oContextMenuCategory);
                                }

                               

                                        #endregion
                                #endregion



                                
                            }
                            if (oCategories != null)
                            {
                                oCategories.Dispose();
                                oCategories = null;
                            }
                        }
                    }
                    #endregion

                    
                }
                #region "Assign Context Menu Strip"
                if (ispagemenu == true)
                {
                    if (lvwPages.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(lvwPages.ContextMenuStrip); lvwPages.ContextMenuStrip.Dispose(); }
                    lvwPages.ContextMenuStrip = oConextMenuStrip;
                }
                else
                {
                    if (c1Documents.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(c1Documents.ContextMenuStrip); c1Documents.ContextMenuStrip.Dispose(); }

                    c1Documents.ContextMenuStrip = oConextMenuStrip;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oBaseDocuments != null)
                {
                    oBaseDocuments.Dispose();
                    oBaseDocuments = null;
                }
                if (oCategories != null)
                {
                    oCategories.Dispose();
                    oCategories = null;
                }
                if (oList  != null)
                {
                    oList.Dispose();
                    oList = null;
                }
                _IsDocumentsLoading = false;
            }
        }
        #endregion "Dhruv 20100626 -> Fill Context Menu"





        #region "Dhruv 20100626 -> oContextMenuDocument_Click"
        
        void oContextMenuDocument_Click(object sender, EventArgs e)
        {
            DocumentContextMenu.eDocV3ContextMenuStrip oConextMenuStrip = null;
            DocumentContextMenu.eDocV3ContextMenuItem oContextMenuItem = null;
            
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;
            DocumentContextMenu.eContextEventParameter oEventParameter = null;
            string _ExceptDocumentIDs = "";

            #region "Find Context Menu Item"
            try
            {
                oContextMenuItem = (DocumentContextMenu.eDocV3ContextMenuItem)sender;
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            if (oContextMenuItem != null)
            {
                #region "Find Context Menu Strip"
                try
                {
                    if (oContextMenuItem.OwnerItem != null)
                    {
                        if (oContextMenuItem.OwnerItem.OwnerItem != null)
                        {
                            if (oContextMenuItem.OwnerItem.OwnerItem.OwnerItem != null)
                            {
                                oConextMenuStrip = (DocumentContextMenu.eDocV3ContextMenuStrip)oContextMenuItem.OwnerItem.OwnerItem.OwnerItem.Owner;
                            }
                            else
                            {
                                oConextMenuStrip = (DocumentContextMenu.eDocV3ContextMenuStrip)oContextMenuItem.OwnerItem.OwnerItem.Owner;
                            }
                        }
                        else
                        {
                            oConextMenuStrip = (DocumentContextMenu.eDocV3ContextMenuStrip)oContextMenuItem.OwnerItem.Owner;
                        }
                    }
                    else
                    {
                        oConextMenuStrip = (DocumentContextMenu.eDocV3ContextMenuStrip)oContextMenuItem.Owner;
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion

                if (oConextMenuStrip != null)
                {
                     //Developer: Mitesh Patel
                      //Date: 5-sep-2012'
                       //Bug ID: 35758
                    oSelectedDocuments = GetSelectedDocuments(out _ExceptDocumentIDs); 
                    oConextMenuStrip.ESelectedDocuments = oSelectedDocuments;
                   // oSelectedDocuments = oConextMenuStrip.ESelectedDocuments;
                    oEventParameter = oContextMenuItem.EContextEventParameter;
                    Int64 DocumentID = 0;
                    Int64 ContainerID = 0;
                    if (oSelectedDocuments != null && oEventParameter != null)
                    {
                        if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToNewDocument)
                        {
                            #region " Send To New Document "

                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    bool _Result = false;

                                    using (frmEDocEvent_Send oDocEvents = new frmEDocEvent_Send())
                                    {
                                        
                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.oEventParameter = oEventParameter;
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                          if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
            {
            }
                                          else
                                          {
                                              if (clsSplit != null)
                                              {
                                                  clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                                              }
                                        }
                                        DocumentID =oDocEvents.oDialogDocumentID;
                                        ContainerID = oDocEvents.oDialogContainerID;
                                        _Result = oDocEvents.oDialogResultIsOK;
                                    }
                                    //oDocEvents.Dispose();

                                    if (_Result == true && DocumentID > 0 && ContainerID > 0)
                                    {
                                        tsb_Refresh_Click(null, null);                                      
                                        SelectDocumentInGrid(DocumentID, ContainerID);
                                        LoadDocument(DocumentID, ContainerID, _OpenExternalSource);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Send, "RCM document content(s) moved.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Send, "Scan document content(s) moved.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }

                                    }

                                }
                            }
                            if (oSelectedDocuments != null) { oSelectedDocuments.Dispose(); }
                            if (oEventParameter != null) { oEventParameter.Dispose(); }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingDcument)
                        {
                            #region " Send To Existing Document "

                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    bool _Result = false;

                                    using (frmEDocEvent_Send oDocEvents = new frmEDocEvent_Send())
                                    {
                                       
                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.oEventParameter = oEventParameter;
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                        DocumentID = oDocEvents.oDialogDocumentID;
                                        ContainerID = oDocEvents.oDialogContainerID;
                                        _Result = oDocEvents.oDialogResultIsOK;
                                    }
                                    //oDocEvents.Dispose();

                                    if (_Result == true && DocumentID > 0 && ContainerID > 0)
                                    {
                                        tsb_Refresh_Click(null, null);
                                        SelectDocumentInGrid(DocumentID, ContainerID);
                                        LoadDocument(DocumentID, ContainerID, _OpenExternalSource);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.merge, "RCM document merged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.merge, "Scan document merged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                    }

                                }
                            }
                            if (oSelectedDocuments != null) 
                            { 
                                oSelectedDocuments.Dispose();
                                oSelectedDocuments = null;
                                
                            }
                            if (oEventParameter != null)
                            {
                                oEventParameter.Dispose();
                                oEventParameter = null;
                            }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SendToExistingWithDcumentName)
                        {
                            #region " Send To Existing With Document Name "

                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    bool _Result = false;
                                    using (frmEDocEvent_Send oDocEvents = new frmEDocEvent_Send())
                                    {
                                       
                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.oEventParameter = oEventParameter;
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                        DocumentID = oDocEvents.oDialogDocumentID;
                                        ContainerID = oDocEvents.oDialogContainerID;
                                        _Result = oDocEvents.oDialogResultIsOK;
                                    }
                                    //oDocEvents.Dispose();

                                    if (_Result == true && DocumentID > 0 && ContainerID > 0)
                                    {
                                        tsb_Refresh_Click(null, null);
                                        SelectDocumentInGrid(DocumentID, ContainerID);
                                        LoadDocument(DocumentID, ContainerID, _OpenExternalSource);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.merge, "RCM document content(s) merged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.merge, "Scan document content(s) merged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                    }

                                }
                            }
                            if (oSelectedDocuments != null)
                            {
                                oSelectedDocuments.Dispose();
                                oSelectedDocuments = null;
                            }
                            if (oEventParameter != null) 
                            {
                                oEventParameter.Dispose();
                                oEventParameter = null;
                            }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenameDocument)
                        {
                            #region " Rename Document "

                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    using (frmEDocEvent_Rename oDocEvents = new frmEDocEvent_Rename())
                                    {
                                        bool _Result = false;

                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.oEventParameter = oEventParameter;
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                        if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                                        {
                                        }
                                        else
                                        {
                                            if (clsSplit != null)
                                            {
                                                clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                                            }
                                        }
                                        _Result = oDocEvents.oDialogResultIsOK;

                                        if (_Result == true)
                                        {
                                            if (c1Documents != null)
                                            {
                                                if (GetCheckedCount() == 1)
                                                {

                                                    for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                                                    {
                                                        if (c1Documents.GetData(i, COL_COLTYPE) != null && ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(i, COL_COLTYPE)) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                                                        {
                                                            if (c1Documents.GetCellCheck(i, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                                            {
                                                                c1Documents.SetData(i, COL_DOCUMENTNAME, oDocEvents.oDialogDocumentName);
                                                                c1Documents.Rows[i].Node.Data = oDocEvents.oDialogDocumentName;
                                                                break;
                                                            }
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    c1Documents.SetData(c1Documents.RowSel, COL_DOCUMENTNAME, oDocEvents.oDialogDocumentName);
                                                    c1Documents.Rows[c1Documents.RowSel].Node.Data = oDocEvents.oDialogDocumentName;
                                                }
                                            }

                                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Modify, "RCM document renamed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                            else
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Modify, "Scan document renamed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                        }
                                    }
                                    //oDocEvents.Dispose();

                                }
                            }


                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.RenamePages)
                        {
                            #region " Rename Pages "

                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    bool _Result = false;
                                    using(frmEDocEvent_Rename oDocEvents = new frmEDocEvent_Rename())
                                    {
                                   
                                    oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                    oDocEvents.oEventParameter = oEventParameter;
                                    oDocEvents._OpenExternalSource = _OpenExternalSource;
                                    oDocEvents.ShowDialog(this);
                                    _Result = oDocEvents.oDialogResultIsOK;
                                    if (_Result == true)
                                    {
                                        lvwPages.SelectedItems[0].Text = oDocEvents.oDialogDocumentName;
                                    }
                                    }
                                   // oDocEvents.Dispose();
                                }
                            }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.SelectAll)
                        {
                            #region " Select All "

                            if (lvwPages != null && lvwPages.Items.Count > 0)
                            {
                                for (int i = 0; i < lvwPages.Items.Count; i++)
                                {
                                    lvwPages.Items[i].Selected = true;
                                }
                            }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.UnselectAll)
                        {
                            #region " Unselect All "

                            if (lvwPages != null && lvwPages.Items.Count > 0)
                            {
                                for (int i = 0; i < lvwPages.Items.Count; i++)
                                {
                                    lvwPages.Items[i].Selected = false;
                                }
                                //** Keep the first Page selected 
                                lvwPages.Items[0].Selected = true;
                            }

                            #endregion
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintPages ||
                           oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.PrintDocument)
                        {
                            tsb_Print_Click(null, null);
                        }
                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgeDocument ||
                            oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.AcknowledgePages)
                        {
                            tsb_Acknowledge_Click(null, null);
                        }

                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.UnAcknowledgeDocument ||
                            oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.UnAcknowledgePages)
                        {
                            tsb_UnAcknowledge_Click(null, null);
                        }


                        else if (oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.FaxPages ||
                            oEventParameter.EventType == gloEDocumentV3.Enumeration.enum_DocumentEventType.FaxDocument)
                        {
                            tsb_Fax_Click(null, null);
                        }
                        else if (oEventParameter.EventType == enum_DocumentEventType.ViewLargeIcon || oEventParameter.EventType == enum_DocumentEventType.ViewSmallIcon || oEventParameter.EventType == enum_DocumentEventType.ViewList || oEventParameter.EventType == enum_DocumentEventType.ViewTile)
                        {
                            if (lvwPages != null)
                            {
                                if (oEventParameter.EventType == enum_DocumentEventType.ViewLargeIcon)
                                {
                                    lvwPages.View = System.Windows.Forms.View.LargeIcon;
                                }
                                else if (oEventParameter.EventType == enum_DocumentEventType.ViewSmallIcon)
                                {
                                    lvwPages.View = System.Windows.Forms.View.SmallIcon;
                                }
                                else if (oEventParameter.EventType == enum_DocumentEventType.ViewList)
                                {
                                    lvwPages.View = System.Windows.Forms.View.List;
                                }
                                else if (oEventParameter.EventType == enum_DocumentEventType.ViewTile)
                                {
                                    lvwPages.View = System.Windows.Forms.View.Tile;
                                }
                            }
                        }
                        else if (oEventParameter.EventType == enum_DocumentEventType.SendToDMS)
                        {
                            if (oSelectedDocuments != null && oEventParameter != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    
                                    using (frmEDocEvent_SendToDMS oDocEvents = new frmEDocEvent_SendToDMS())
                                    {

                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.oEventParameter = oEventParameter;
                                        oDocEvents.chkSendTask.Enabled = true;
                                        oDocEvents.chkSendTask.Checked = false;
                                        oDocEvents.ShowDialog(this);
                                        
                                        
                                    }
                                                                       
                                }
                            }
                        }

                    }
                }
            }
        }
        #endregion "Dhruv 20100626 -> oContextMenuDocument_Click"

        #endregion
    }
}
