using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEDocumentV3.Enumeration;

namespace gloEDocumentV3.Forms
{
    public partial class frmEDocEvent__ArchiveDocuments : Form
    {
        private Int64 _PatientID;
        eDocManager.eDocGetList oList;
        DataTable dt = null;
        string _ErrorMessage = "";
        private Int16 COL_eDocumentID = 0;
        private Int16 COL_Select = 1;
        private Int16 COL_DocumentName = 2;
        private Int16 COL_CategoryID = 3;
        private Int16 COL_Category = 4;
        private Int16 COL_PatientID = 5;
        private Int16 COL_Year = 6;
        private Int16 COL_Month = 7;
        private Int16 COL_PageCounts = 8;
        private Int16 COL_CreatedDateTime = 9;
        private Int16 COL_ModifiedDateTime = 10;
        private Int16 COL_IsAcknowledge = 11;
        private Int16 COL_HasNote = 12;
        private Int16 COL_ExternalID = 13;
        private Int16 COL_ExternalCode = 14;
        private Int16 COL_ExternalDescription = 15;
        private Int16 COL_UsedStatus = 16;
        private Int16 COL_UsedMachine = 17;
        private Int16 COL_ArchiveID = 18;
        private Int16 COL_ArchiveStatus = 19;
        private Int16 COL_ArchiveDescription = 20;
        private Int16 COL_DocumentDetails = 21;
        private Int16 COL_Count = 22;
        int NewRow;

        public enum_OpenExternalSource OpenExternalSource { get; set; }

        public frmEDocEvent__ArchiveDocuments(Int64 PatientId, enum_OpenExternalSource openExternalSource)
        {
            _PatientID = PatientId;
            InitializeComponent();

            this.OpenExternalSource = openExternalSource;
        }


        private void frmEDocEvent__ArchiveDocuments_Load(object sender, EventArgs e)
        {
            if (oList != null)
            {
                oList.Dispose();
                oList = null;
            }
            oList = new gloEDocumentV3.eDocManager.eDocGetList();
            dt = new DataTable();


            try
            {
                if (oList != null)
                {
                    dt = oList.GetArchiveDocument(_PatientID, this.OpenExternalSource);
                    //C1.Win.C1FlexGrid.Node oNode;

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DesignGrid();
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                FillData(i);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage =ex.ToString();
                MessageBox.Show("ERROR : " + _ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorMessagees(_ErrorMessage);
            }
            finally
            {
                
                if (dt != null)
                {
                    dt.Clear();
                    dt.Dispose();
                    dt = null;
                }
            }
        }
        private void ErrorMessagees(string _ErrorMessage)
        {
            #region " Make Log Entry "
            try
            {
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
            }
            catch (Exception ex)
            {
                string _ErrorHere = ex.ToString();
            }
           
            #endregion " Make Log Entry "
        }

        public void FillData(int i)
        {

            Int32 _ParentIndex = 0;
            if (i == 0 || dt.Rows[i - 1]["Category"].ToString() != dt.Rows[i]["Category"].ToString())
            {
                C1ArchiveList.Rows.Add();
                NewRow = C1ArchiveList.Rows.Count - 1;
                C1ArchiveList.Rows[NewRow].IsNode = true;
                C1ArchiveList.Rows[NewRow].Node.Level = 0;
                C1ArchiveList.Rows[NewRow].Node.Data = dt.Rows[i]["Category"].ToString();
                C1ArchiveList.SetData(NewRow, COL_DocumentName, dt.Rows[i]["Category"].ToString());
                C1ArchiveList.SetCellCheck(NewRow, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                _ParentIndex = NewRow;
            }
            else
            {
                NewRow = C1ArchiveList.Rows.Count - 1;
                _ParentIndex = C1ArchiveList.Rows[NewRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index;
            }
           
            
            C1.Win.C1FlexGrid.Node oChild = C1ArchiveList.Rows[_ParentIndex].Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dt.Rows[i]["DocumentName"].ToString());

            Int32 _ChildIndex = oChild.Row.Index;

            C1ArchiveList.SetData(_ChildIndex, COL_Select, false);
            C1ArchiveList.SetData(_ChildIndex, COL_eDocumentID, dt.Rows[i]["eDocumentID"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_DocumentName, dt.Rows[i]["DocumentName"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_CategoryID, dt.Rows[i]["CategoryID"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_Category, dt.Rows[i]["Category"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_PatientID, dt.Rows[i]["PatientID"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_Year, dt.Rows[i]["Year"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_Month, dt.Rows[i]["Month"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_PageCounts, dt.Rows[i]["PageCounts"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_CreatedDateTime, dt.Rows[i]["CreatedDateTime"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ModifiedDateTime, dt.Rows[i]["ModifiedDateTime"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_IsAcknowledge, dt.Rows[i]["IsAcknowledge"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_HasNote, dt.Rows[i]["HasNote"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ExternalID, dt.Rows[i]["ExternalID"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ExternalCode, dt.Rows[i]["ExternalCode"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ExternalDescription, dt.Rows[i]["ExternalDescription"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_UsedStatus, dt.Rows[i]["UsedStatus"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_UsedMachine, dt.Rows[i]["UsedMachine"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ArchiveID, dt.Rows[i]["ArchiveID"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ArchiveStatus, dt.Rows[i]["ArchiveStatus"].ToString());
            C1ArchiveList.SetData(_ChildIndex, COL_ArchiveDescription, dt.Rows[i]["ArchiveDescription"].ToString());
        }


        private void DesignGrid()
        {
            try
            {


                C1ArchiveList.Rows.Count = 1;
                C1ArchiveList.Rows.Fixed = 1;
                C1ArchiveList.Cols.Count = COL_Count;
                C1ArchiveList.ExtendLastCol = true;
                C1ArchiveList.Tree.Column = 1;

                C1ArchiveList.Cols[COL_eDocumentID].Width = 0;
                C1ArchiveList.Cols[COL_eDocumentID].Visible = false;
                C1ArchiveList.Cols[COL_eDocumentID].AllowEditing = false;
                C1ArchiveList.Cols[COL_eDocumentID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_eDocumentID, "Document ID");

                C1ArchiveList.Cols[COL_Select].Width = 50;
                C1ArchiveList.Cols[COL_Select].AllowEditing = true;
                C1ArchiveList.Cols[COL_Select].Visible = true;
                C1ArchiveList.Cols[COL_Select].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.Cols[COL_Select].DataType = typeof(System.Boolean);
                C1ArchiveList.SetData(0, COL_Select, "Select");


                C1ArchiveList.Cols[COL_DocumentName].Width = 175;
                C1ArchiveList.Cols[COL_DocumentName].AllowEditing = false;
                C1ArchiveList.Cols[COL_DocumentName].Visible = true;
                C1ArchiveList.Cols[COL_DocumentName].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_DocumentName, "Document Name");

                C1ArchiveList.Cols[COL_CategoryID].Width = 0;
                C1ArchiveList.Cols[COL_CategoryID].AllowEditing = false;
                C1ArchiveList.Cols[COL_CategoryID].Visible = false;
                C1ArchiveList.Cols[COL_CategoryID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_CategoryID, "Category ID");

                C1ArchiveList.Cols[COL_Category].Width = 120;
                C1ArchiveList.Cols[COL_Category].AllowEditing = false;
                C1ArchiveList.Cols[COL_Category].Visible = false;
                C1ArchiveList.Cols[COL_Category].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_Category, "Category Name ");

                C1ArchiveList.Cols[COL_PatientID].Width = 0;
                C1ArchiveList.Cols[COL_PatientID].AllowEditing = false;
                C1ArchiveList.Cols[COL_PatientID].Visible = false;
                C1ArchiveList.Cols[COL_PatientID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_PatientID, "Patient ID");

                C1ArchiveList.Cols[COL_Year].Width = 50;
                C1ArchiveList.Cols[COL_Year].AllowEditing = false;
                C1ArchiveList.Cols[COL_Year].Visible = true;
                C1ArchiveList.Cols[COL_Year].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_Year, "Year");

                C1ArchiveList.Cols[COL_Month].Width = 50;
                C1ArchiveList.Cols[COL_Month].AllowEditing = false;
                C1ArchiveList.Cols[COL_Month].Visible = false;
                C1ArchiveList.Cols[COL_Month].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_Month, "Month");

                C1ArchiveList.Cols[COL_PageCounts].Width = 120;
                C1ArchiveList.Cols[COL_PageCounts].AllowEditing = false;
                C1ArchiveList.Cols[COL_PageCounts].Visible = false;
                C1ArchiveList.Cols[COL_PageCounts].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_PageCounts, "Page Count");

                C1ArchiveList.Cols[COL_CreatedDateTime].Width = 120;
                C1ArchiveList.Cols[COL_CreatedDateTime].AllowEditing = false;
                C1ArchiveList.Cols[COL_CreatedDateTime].Visible = true;
                C1ArchiveList.Cols[COL_CreatedDateTime].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_CreatedDateTime, "Created DateTime");

                C1ArchiveList.Cols[COL_ModifiedDateTime].Width = 120;
                C1ArchiveList.Cols[COL_ModifiedDateTime].AllowEditing = false;
                C1ArchiveList.Cols[COL_ModifiedDateTime].Visible = false;
                C1ArchiveList.Cols[COL_ModifiedDateTime].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ModifiedDateTime, "Modified Date Time");

                C1ArchiveList.Cols[COL_IsAcknowledge].Width = 120;
                C1ArchiveList.Cols[COL_IsAcknowledge].AllowEditing = false;
                C1ArchiveList.Cols[COL_IsAcknowledge].Visible = false;
                C1ArchiveList.Cols[COL_IsAcknowledge].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_IsAcknowledge, "Is Acknowledge");

                C1ArchiveList.Cols[COL_HasNote].Width = 120;
                C1ArchiveList.Cols[COL_HasNote].AllowEditing = false;
                C1ArchiveList.Cols[COL_HasNote].Visible= false;
                C1ArchiveList.Cols[COL_HasNote].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_HasNote, "Has Note");

                C1ArchiveList.Cols[COL_ExternalID].Width = 0;
                C1ArchiveList.Cols[COL_ExternalID].AllowEditing = false;
                C1ArchiveList.Cols[COL_ExternalID].Visible = false;
                C1ArchiveList.Cols[COL_ExternalID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ExternalID, "External ID");

                C1ArchiveList.Cols[COL_ExternalCode].Width = 0;
                C1ArchiveList.Cols[COL_ExternalCode].AllowEditing = false;
                C1ArchiveList.Cols[COL_ExternalCode].Visible = false;
                C1ArchiveList.Cols[COL_ExternalCode].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ExternalCode, "External Code");

                C1ArchiveList.Cols[COL_ExternalDescription].Width = 0;
                C1ArchiveList.Cols[COL_ExternalDescription].AllowEditing = false;
                C1ArchiveList.Cols[COL_ExternalDescription].Visible = false;
                C1ArchiveList.Cols[COL_ExternalDescription].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ExternalDescription, "External Description");

                C1ArchiveList.Cols[COL_UsedStatus].Width = 0;
                C1ArchiveList.Cols[COL_UsedStatus].AllowEditing = false;
                C1ArchiveList.Cols[COL_UsedStatus].Visible = false;
                C1ArchiveList.Cols[COL_UsedStatus].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_UsedStatus, "Used Status");

                C1ArchiveList.Cols[COL_UsedMachine].Width = 0;
                C1ArchiveList.Cols[COL_UsedMachine].AllowEditing = false;
                C1ArchiveList.Cols[COL_UsedMachine].Visible = false;
                C1ArchiveList.Cols[COL_UsedMachine].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_UsedMachine, "Used Machine");

                C1ArchiveList.Cols[COL_ArchiveID].Width = 0;
                C1ArchiveList.Cols[COL_ArchiveID].AllowEditing = false;
                C1ArchiveList.Cols[COL_ArchiveID].Visible = false;
                C1ArchiveList.Cols[COL_ArchiveID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ArchiveID, "Archive ID");

                C1ArchiveList.Cols[COL_ArchiveStatus].Width = 120;
                C1ArchiveList.Cols[COL_ArchiveStatus].AllowEditing = false;
                C1ArchiveList.Cols[COL_ArchiveStatus].Visible = false;
                C1ArchiveList.Cols[COL_ArchiveStatus].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ArchiveStatus, "Archive Status");

                C1ArchiveList.Cols[COL_ArchiveDescription].Width = 250;
                C1ArchiveList.Cols[COL_ArchiveDescription].AllowEditing = false;
                C1ArchiveList.Cols[COL_ArchiveDescription].Visible = false;
                C1ArchiveList.Cols[COL_ArchiveDescription].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_ArchiveDescription, "Archive Description");

                C1ArchiveList.Cols[COL_DocumentDetails].Width = 0;
                C1ArchiveList.Cols[COL_DocumentDetails].AllowEditing = false;
                C1ArchiveList.Cols[COL_DocumentDetails].Visible = false;
                C1ArchiveList.Cols[COL_DocumentDetails].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                C1ArchiveList.SetData(0, COL_DocumentDetails, "Document Details");

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
                throw;
            }
        }

        private void tsb_Retrive_Click(object sender, EventArgs e)
        {
            bool _result = true;
            string strMessage = string.Empty;
            if (!oList.ValidateArchiveDBExists())
            {
                MessageBox.Show(this, "Error in connecting archive database ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                

                if (oList != null)
                {
                    //For all rows in Grid.
                    for (int _index = 1; _index <= C1ArchiveList.Rows.Count - 1; _index++)
                    {

                        if (C1ArchiveList.Rows[_index].Node.Level == 1)
                        {
                            //Check if selected current Row in grid.
                            if (C1ArchiveList.GetCellCheck(_index, COL_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                Int64 _patientID = Convert.ToInt64(C1ArchiveList.GetData(_index, COL_PatientID));
                                Int64 _documentID = Convert.ToInt64(C1ArchiveList.GetData(_index, COL_eDocumentID));
                                bool bReturned = false;

                                //Retrive documents if selected to retrive.

                                if (OpenExternalSource == enum_OpenExternalSource.RCM)
                                { bReturned = oList.RetrieveRCMDocuments(_documentID); }
                                else
                                { bReturned = oList.RetrieveDocumentOfPatient(_patientID, _documentID); }

                                if (bReturned == false)
                                {
                                    string Message = "Problem while restoring document name :" + C1ArchiveList.GetData(_index, COL_DocumentName);
                                    MessageBox.Show(this, Message + ".", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _ErrorMessage = Message;
                                    ErrorMessagees(_ErrorMessage);
                                    _result = false;
                                }
                                _documentID = 0;
                                _patientID = 0;
                            }
                        }
                    }
                    // code to Check at least a single Document or Category
                    // Developer : Roopali Babriya
                    bool _atLeastOneSelected = false;

                    for (int _ind = 1; _ind <= C1ArchiveList.Rows.Count - 1; _ind++)//For all rows in Grid.
                    {

                        if (C1ArchiveList.Rows[_ind].Node.Level == 1)
                        {
                            //Check if selected current Row in grid.
                            if (C1ArchiveList.GetCellCheck(_ind, COL_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                _atLeastOneSelected = true;
                                break;
                            }
                        }
                    }
                    // Developer : Roopali Babriya
                    //** code to Check at least a single Document or Category

                    if (_result && _atLeastOneSelected)
                    {
                        MessageBox.Show(this, "All documents restored successfully.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!_atLeastOneSelected)
                    {
                        MessageBox.Show(this, "Please select at least one document or a category to retrieve.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //rebind

                    //clear grid before rebind
                   // C1ArchiveList.Clear();
                    C1ArchiveList.DataSource = null;
                    C1ArchiveList.Rows.Count = 0;

                    //oList = new gloEDocumentV3.eDocManager.eDocGetList();
                   // dt = new DataTable();
                    dt = oList.GetArchiveDocument(_PatientID, this.OpenExternalSource);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DesignGrid();
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                FillData(i);                             
                            }
                        }
                    }
                    //***rebind
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage =ex.ToString();
                ErrorMessagees(_ErrorMessage);
                MessageBox.Show("ERROR : " + _ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

       

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void C1ArchiveList_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            
            
       }

        private void C1ArchiveList_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            Int32 _SelRow = e.Row;
            if (_SelRow > 0)
            {
                if (C1ArchiveList.Rows[_SelRow].Node.Level == 0)
                {
                    if (e.Col == COL_Select)
                    {
                        C1.Win.C1FlexGrid.CheckEnum _CellCheck = C1ArchiveList.GetCellCheck(_SelRow, COL_Select);

                        for (int iRow = _SelRow; iRow <= C1ArchiveList.Rows.Count - 1; iRow++)
                        {
                            if (C1ArchiveList.Rows[iRow].Node.Level == 1)
                            {
                                if (C1ArchiveList.Rows[iRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index == _SelRow)
                                {
                                    if (_CellCheck == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    {
                                        C1ArchiveList.SetCellCheck(iRow, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        C1ArchiveList.SetCellCheck(iRow, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tsb_SelectAllDocuments_Click(object sender, EventArgs e)
        {

            if (C1ArchiveList.Rows.Count > 0)
            {
                for (int _rowIndex = 0; _rowIndex <= C1ArchiveList.Rows.Count-1; _rowIndex++)
                {
                    if (C1ArchiveList.Rows[_rowIndex].IsNode)
                    {
                        //if (C1ArchiveList.Rows[_rowIndex].Node.Level == 0)
                        {
                            if (tsb_SelectAllDocuments.Tag.ToString().ToUpper() == "SELECTALL")
                            {
                                C1ArchiveList.SetCellCheck(_rowIndex, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            else
                            {
                                C1ArchiveList.SetCellCheck(_rowIndex, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                        }
                    }
                }
            }


            // Set Text and Tags.
            if (tsb_SelectAllDocuments.Tag.ToString().ToUpper() == "SELECTALL")
            {
                tsb_SelectAllDocuments.Tag = "ClearAll";
                tsb_SelectAllDocuments.Text = "Clear &All";
            }
            else
            {
                tsb_SelectAllDocuments.Tag = "SelectAll";
                tsb_SelectAllDocuments.Text = "Select &All";
            }

        }

    }
}