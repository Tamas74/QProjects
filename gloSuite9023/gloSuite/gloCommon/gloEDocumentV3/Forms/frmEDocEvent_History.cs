using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.DocumentContextMenu;


namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_History : Form
    {
        #region " Constructor "

        public frmEDocEvent_History()
        {
            _SelectedDocuments = new eContextDocuments();
            InitializeComponent();
        } 

        #endregion

        #region "Page Manuipulation Variables"
        
        public Int64 oClinicID = 0;
        public bool oDialogResultIsOK = false;
        private Int64 _PatientID = 0;
       
        public string _ErrorMessage = "";
        private eContextDocuments _SelectedDocuments = null;

        public Enumeration.enum_OpenExternalSource _OpenExternalSource = Enumeration.enum_OpenExternalSource.None;

        #endregion

        #region " Property Procedures "

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        
        public eContextDocuments oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        #endregion

        #region " Column Contants "

        private const int COL_DOCUMENTID =0;
        private const int COL_DOCUMENTNAME = 1; 
        private const int COL_CONTAINERID = 2;
        private const int COL_CONTAINERPAGENUMBER = 3;
        private const int COL_DOCUMENTPAGENUMBER = 4;
        private const int COL_NTAOID = 5;
        private const int COL_USERID = 6;
        private const int COL_USERNAME = 7;
        private const int COL_PAGENAME = 8;
        private const int COL_NTAODATETIME = 9;
        private const int COL_NTAODESCRIPTION = 10;
        private const int COL_ISPAGE = 11;
        private const int COL_NTAOTYPE = 12;
        private const int COL_CLINICID = 13;

        private const int COL_COUNT = 14; 
        
        #endregion

        #region " Form Load "

        private void frmEDocEvent_History_Load(object sender, EventArgs e)
       
              
        {
            LoadHistory(gloEDocumentV3.Enumeration.enum_NTAOType.Acknowledge);
            lblDescription.Text = "Acknowledges Description";
            this.Select();
            this.BringToFront();
        } 

        #endregion

        #region " Button Click Events "

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Notes_Click(object sender, EventArgs e)
        {
            LoadHistory(gloEDocumentV3.Enumeration.enum_NTAOType.Notes);
            lblDescription.Text = "Notes Description";
        }

        private void tlb_Acknowledge_Click(object sender, EventArgs e)
        {
            LoadHistory(gloEDocumentV3.Enumeration.enum_NTAOType.Acknowledge);
            lblDescription.Text = "Acknowledges Description";
        }

        private void tlb_UserTags_Click(object sender, EventArgs e)
        {
            LoadHistory(gloEDocumentV3.Enumeration.enum_NTAOType.Tag);
            lblDescription.Text = "Tags Description";
        }

        #endregion " Button Click Events "

        #region " Private & Public Methods "

        private void LoadHistory(gloEDocumentV3.Enumeration.enum_NTAOType HistoryType)
        {

            Common.NTAOs oNTAOs = new gloEDocumentV3.Common.NTAOs();
            eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

            try
            {
                if (HistoryType != gloEDocumentV3.Enumeration.enum_NTAOType.None && HistoryType != gloEDocumentV3.Enumeration.enum_NTAOType.Other)
                {
                    //c1History.ShowCellLabels = true;
                    c1History.Rows.Count = 1;
                    c1History.Rows.Fixed = 1;
                    c1History.Cols.Count = COL_COUNT;
                    c1History.Cols.Fixed = 0;
                    
                    int _Width = (c1History.Width - 20) / 10;
                    c1History.Cols[COL_DOCUMENTID].Width = 0;
                    c1History.Cols[COL_DOCUMENTNAME].Width = _Width * 3; 
                    c1History.Cols[COL_CONTAINERID].Width = 0;
                    c1History.Cols[COL_CONTAINERPAGENUMBER].Width = 0;
                    c1History.Cols[COL_DOCUMENTPAGENUMBER].Width = 0;
                    c1History.Cols[COL_NTAOID].Width = 0;
                    c1History.Cols[COL_USERID].Width = 0;
                    c1History.Cols[COL_USERNAME].Width = _Width * 3;
                    c1History.Cols[COL_NTAODATETIME].Width = Convert.ToInt32(_Width * 2.5);
                    c1History.Cols[COL_NTAODESCRIPTION].Width = _Width * 5;
                    c1History.Cols[COL_ISPAGE].Width = 0;
                    c1History.Cols[COL_NTAOTYPE].Width = 0;
                    c1History.Cols[COL_CLINICID].Width = 0;
                    c1History.Cols[COL_PAGENAME].Width = _Width * 2;


                    c1History.Cols[COL_DOCUMENTID].Visible = false;
                    c1History.Cols[COL_DOCUMENTNAME].Visible = true;
                    c1History.Cols[COL_CONTAINERID].Visible = false;
                    c1History.Cols[COL_CONTAINERPAGENUMBER].Visible = false;
                    c1History.Cols[COL_DOCUMENTPAGENUMBER].Visible = false;
                    c1History.Cols[COL_NTAOID].Visible = false;
                    c1History.Cols[COL_USERID].Visible = false;
                    c1History.Cols[COL_USERNAME].Visible = true;
                    c1History.Cols[COL_NTAODATETIME].Visible = true;
                    c1History.Cols[COL_NTAODESCRIPTION].Visible = true;
                    c1History.Cols[COL_ISPAGE].Visible = false;
                    c1History.Cols[COL_NTAOTYPE].Visible = false;
                    c1History.Cols[COL_CLINICID].Visible = false;
                    c1History.Cols[COL_PAGENAME].Visible = false; 



                    c1History.SetData(0, COL_DOCUMENTID, "DocumentID");
                    c1History.SetData(0, COL_DOCUMENTNAME, "Document Name");
                    c1History.SetData(0, COL_CONTAINERID, "ContainerID");
                    c1History.SetData(0, COL_CONTAINERPAGENUMBER, "ContainerPageNo");
                    c1History.SetData(0, COL_DOCUMENTPAGENUMBER, "DocumentPageNo");
                    c1History.SetData(0, COL_NTAOID, "NTAOID");
                    c1History.SetData(0, COL_USERID, "UserID");
                    c1History.SetData(0, COL_USERNAME, "User Name");
                    c1History.SetData(0, COL_NTAODATETIME, "Date/Time");
                    c1History.SetData(0, COL_NTAODESCRIPTION, HistoryType.ToString() + "  " + "Description");
                    c1History.SetData(0, COL_ISPAGE, "Is Page");
                    c1History.SetData(0, COL_NTAOTYPE, "Type");
                    c1History.SetData(0, COL_CLINICID, "ClinicID");
                    c1History.SetData(0, COL_PAGENAME, "Page Name");

                    //Set style for C1grid For wraping text in the row.
                    C1.Win.C1FlexGrid.CellStyle c1Style;
                    // c1Style = c1History.Styles.Add("c1Style");
                    try
                    {
                        if (c1History.Styles.Contains("c1Style"))
                        {
                            c1Style = c1History.Styles["c1Style"];
                        }
                        else
                        {
                            c1Style = c1History.Styles.Add("c1Style");
                        }

                    }
                    catch
                    {
                        c1Style = c1History.Styles.Add("c1Style");
                    }

                    c1Style.WordWrap = true;

                    c1History.AutoSizeRows();

                    for (int i = 0; i <= c1History.Cols.Count - 1; i++)
                    {
                        c1History.Cols[i].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (HistoryType == gloEDocumentV3.Enumeration.enum_NTAOType.Acknowledge)
                    {
                        oNTAOs = oList.GetAcknowledges(oSelectedDocuments, oClinicID, _OpenExternalSource);
                        c1History.Cols[COL_PAGENAME].Visible = false; 
                    }
                    else if (HistoryType == gloEDocumentV3.Enumeration.enum_NTAOType.Notes)
                    {
                        oNTAOs = oList.GetNotes(oSelectedDocuments,oClinicID, _OpenExternalSource);
                        c1History.Cols[COL_PAGENAME].Visible = true; 
                    }
                    else if (HistoryType == gloEDocumentV3.Enumeration.enum_NTAOType.Tag)
                    {
                        oNTAOs = oList.GetUserTags(oSelectedDocuments, oClinicID, _OpenExternalSource);
                        c1History.Cols[COL_PAGENAME].Visible = true; 
                    }

                    if (oNTAOs != null)
                    {
                        int rowIndex = 0;

                        for (int i = 0; i <= oNTAOs.Count - 1; i++)
                        {
                            rowIndex = c1History.Rows.Count;
                            c1History.Rows.Add();

                            

                            c1History.SetData(rowIndex, COL_DOCUMENTID, oNTAOs[i].DocumentID);
                            c1History.SetData(rowIndex, COL_DOCUMENTNAME, oNTAOs[i].DocumentName);
                            c1History.SetData(rowIndex, COL_CONTAINERID, oNTAOs[i].ContainerID);
                            c1History.SetData(rowIndex, COL_CONTAINERPAGENUMBER, oNTAOs[i].ContainerPageNumber);
                            c1History.SetData(rowIndex, COL_DOCUMENTPAGENUMBER, oNTAOs[i].DocumentPageNumber);
                            c1History.SetData(rowIndex, COL_NTAOID, oNTAOs[i].NTAOID);
                            c1History.SetData(rowIndex, COL_USERID, oNTAOs[i].UserID);
                            c1History.SetData(rowIndex, COL_USERNAME, oNTAOs[i].UserName);
                            c1History.SetData(rowIndex, COL_NTAODATETIME, oNTAOs[i].NTAODateTime);

                            c1History.SetCellStyle(rowIndex, COL_NTAODESCRIPTION, c1Style);
                            c1History.SetData(rowIndex, COL_NTAODESCRIPTION, oNTAOs[i].NTAODescription.Replace("\n", "").ToString());
                            
                            using (Graphics g = c1History.CreateGraphics())
                            {
                                
                                // measure text height
                               // StringFormat sf = new StringFormat();
                                int wid = c1History.Cols[COL_NTAODESCRIPTION].WidthDisplay - 2;
                                string text = Convert.ToString(c1History.GetData(rowIndex, COL_NTAODESCRIPTION));
                                SizeF sz = g.MeasureString(text, c1History.Font, wid, StringFormat.GenericDefault);

                                // adjust row height if necessary
                                C1.Win.C1FlexGrid.Row row = c1History.Rows[rowIndex];
                                if (sz.Height + 4 > row.HeightDisplay)
                                { row.HeightDisplay = (int)sz.Height + 4;  }
                            }
                            
                            c1History.SetData(rowIndex, COL_ISPAGE, oNTAOs[i].IsPage);
                            c1History.SetData(rowIndex, COL_NTAOTYPE, oNTAOs[i].NTAOType);
                            c1History.SetData(rowIndex, COL_CLINICID, oNTAOs[i].ClinicID);
                            c1History.SetData(rowIndex, COL_PAGENAME, oNTAOs[i].PageName);

                            
                        }
                        c1History.Refresh();
                        //c1History.AutoSizeRows();
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oNTAOs != null) { oNTAOs.Dispose(); }
                if (oList != null) { oList.Dispose(); }
            }
        }

        #endregion " Private & Public Methods "

        

    }
}
