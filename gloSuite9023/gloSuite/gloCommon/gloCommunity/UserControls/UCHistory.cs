using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Net;
using System.IO;
using System.Xml;
using System.Configuration;


namespace gloCommunity.UserControls
{
    public partial class UCHistory : UserControl
    {
        Int64 key = -1;
        string strAction = "";
        gloLists.Lists gloList;
        public clsHistory.Histories oHistories;
        private bool _blnParentChecked = false;
        // clsHistory oHistoriesCollection;

        public UCHistory(string _strAction)
        {
            strAction = _strAction;
            InitializeComponent();
        }

        private void UCHistory_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //clsGeneral.UpdateLog("History loaded successfully");
            oHistories = new clsHistory.Histories();
            try
            {
                if (strAction == "Upload")
                {
                    //clsGeneral.UpdateLog("creating Tree view on load for  upload action");
                    FillTreeView();
                    //clsGeneral.UpdateLog("created Tree view successfully for upload action");
                    trvCategory.ExpandAll();
                    myTreeNode mynode = default(myTreeNode);
                    if (trvCategory.Nodes[0].GetNodeCount(false) > 0)
                    {
                        mynode = (myTreeNode)trvCategory.Nodes[0].Nodes[0];
                        key = mynode.Key;
                        trvCategory.SelectedNode = mynode;
                        //BindGrid();
                    }
                }
                else
                {
                    tlbClinicRepository_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog(ex.Message.ToString () + "in history load event" );
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in load of History " + strAction +"\n" + ex.Message .ToString (), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }

        private void FillTreeView()
        {
            //clsGeneral.UpdateLog("in  FillTreeView");
            DataTable dc = null;
            clsTemplate objclsTemplate = new clsTemplate();
            try
            {
                dc = objclsTemplate.GetAllCategory("History");
                myTreeNode mynode = default(myTreeNode);
                mynode = new myTreeNode("History", -1);
                mynode.ImageIndex = 0;
                mynode.SelectedImageIndex = 0;
                trvCategory.Nodes.Add(mynode);

                myTreeNode mychildnode = default(myTreeNode);
                int i = 0;
                Int64 key = default(Int64);
                string strname = null;
                for (i = 0; i <= dc.Rows.Count - 1; i++)
                {
                    key = Convert.ToInt64(dc.Rows[i][0]);
                    strname = dc.Rows[i][1].ToString();
                    mychildnode = new myTreeNode(strname, key);
                    mychildnode.ImageIndex = 1;
                    mychildnode.SelectedImageIndex = 1;
                    mynode.Nodes.Add(mychildnode);
                }
            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in FillTreeView of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("glocomm Error While Fill Treeview   in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                dc = null;
            }
            finally
            {
                objclsTemplate = null;
                if (dc != null)
                {
                    dc.Dispose();
                    dc = null;
                }
            }
        }

        private void BindGrid(string CategoryName, TreeNode myNode)
        {
            //clsGeneral.UpdateLog("in  BindGrid");
            dgHistory.Visible = true;
            clsHistory objclsHistory = new clsHistory();
            DataTable dtHistory = null;
            DataColumn ColSelect = null;
            try
            {
                if (strAction == "Download")
                {
                    //clsGeneral.UpdateLog("Binding Grid for Download");
                    string _TableName = "Category";
                    clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
                    string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml";
                    //clsGeneral.UpdateLog("In Binding Grid getting xml data by passing xml path as   " + ServerXmlPath);
                    dtHistory = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);

                    //get unique Category & History Item 
                    //string[] arrColumn = { "CategoryName", "Type", "Description", "Comments" };
                    DataView UniqueView = new DataView();
                    UniqueView.Table = dtHistory;
                    UniqueView.Sort = "Description";
                    if (myNode.Parent != null)
                    {
                        if (myNode.Parent.Parent == null)
                            UniqueView.RowFilter = "CategoryName = '" + CategoryName + "'";//for Clinic Repository
                        else
                            UniqueView.RowFilter = "CategoryName = '" + CategoryName + "' AND Specialty = '" + myNode.Parent.Parent.Text + "'";//for Global Repository
                    }
                    else
                    {
                        dgHistory.DataSource = null;
                        return;
                    }
                    dtHistory = UniqueView.ToTable();//(true, arrColumn);
                    //end

                    //Add Select column for selecting History Item.
                    ColSelect = new DataColumn("Select", typeof(System.Boolean));
                    dtHistory.Columns.Add(ColSelect);
                    //End

                    dtHistory.AcceptChanges();
                    dgHistory.DataSource = dtHistory;

                    dgHistory.Columns[0].Visible = false;
                    dgHistory.Columns[1].Visible = false;

                    dgHistory.Columns[2].HeaderText = "Description";
                    dgHistory.Columns[2].ReadOnly = true;

                    dgHistory.Columns[3].HeaderText = "Comments";
                    dgHistory.Columns[3].ReadOnly = true;

                    dgHistory.Columns[4].Visible = true;
                    dgHistory.Columns[5].Visible = false;
                    dgHistory.Columns[6].Visible = false;
                    dgHistory.Columns[7].Visible = true;
                    dgHistory.Columns[8].Visible = false;
                    dgHistory.Columns[9].Visible = false;
                    dgHistory.Columns[10].Visible = false;
                    dgHistory.Columns[11].Visible = false;
                    dgHistory.Columns[12].Visible = false;
                    dgHistory.Columns[13].Visible = true;
                    dgHistory.Columns["ClinicName"].Visible = false;
                    dgHistory.Columns["Specialty"].Visible = false;
                }
                else
                {
                    //clsGeneral.UpdateLog("Binding Grid for upload");
                    if (key != -1)
                    {
                        dtHistory = objclsHistory.FetchData(key);
                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            //Add Select column for selecting History Item.
                            ColSelect = new DataColumn("Select", typeof(System.Boolean));
                            dtHistory.Columns.Add(ColSelect);
                            //End

                            dtHistory.AcceptChanges();
                            dgHistory.DataSource = dtHistory;

                            dgHistory.Columns[0].Visible = false;
                            dgHistory.Columns[4].Visible = false;
                            dgHistory.Columns[7].Visible = false;
                            dgHistory.Columns[8].Visible = false;
                            dgHistory.Columns[9].Visible = false;

                            dgHistory.Columns[1].HeaderText = "Description";
                            dgHistory.Columns[1].ReadOnly = true;

                            dgHistory.Columns[2].HeaderText = "Comments";
                            dgHistory.Columns[2].ReadOnly = true;
                            dgHistory.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            dgHistory.Columns[3].Visible = true;
                            dgHistory.Columns[5].Visible = false;
                            dgHistory.Columns[6].Visible = true;
                            dgHistory.Columns[10].Visible = false;
                            dgHistory.Columns[11].Visible = false;
                            dgHistory.Columns[12].Visible = false;
                            dgHistory.Columns[13].Visible = false;
                        }
                        else
                            dgHistory.Visible = false;
                    }

                }
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    dgHistory.Columns[dgHistory.Columns.Count - 1].DisplayIndex = 0;//First Displaying Select column.
                    dgHistory.Columns[dgHistory.Columns.Count - 1].Width = 50;
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Binding Grid  " + ex.Message.ToString ());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in bindgrid of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //clsGeneral.UpdateLog("In side trvCategory_AfterSelect  ");
                myTreeNode mynode = null;
                string _CategoryName = "";
                if ((e.Node != null))
                {
                    if ((!object.ReferenceEquals(mynode, trvCategory.Nodes[0])))
                    {
                        mynode = (myTreeNode)e.Node;
                        _CategoryName = mynode.Text;
                        if (strAction == "Download")
                        {
                            BindGrid(_CategoryName, e.Node);
                            //clsGeneral.UpdateLog("BindGrid for download for category  " + _CategoryName);
                        }
                        else
                        {
                            key = mynode.Key;
                            _CategoryName = mynode.Text;
                            if (key != -1)
                            {
                                //clsGeneral.UpdateLog("BindGrid for "+ strAction   +"for category  " + _CategoryName);
                                BindGrid(_CategoryName, e.Node);
                            }
                            else
                            {
                                dgHistory.DataSource = null;
                            }
                        }
                        ShowCheckUncheckHistoryItems(mynode);
                    }
                    else
                    {
                        dgHistory.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in trvCategory_AfterSelect of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("glocomm Error While Treeview Category  in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void dgHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int colChk = e.ColumnIndex;
            if (colChk == dgHistory.Columns.Count - 1)//Checkbox column.
            {
                DataGridViewRow rowToSelect = this.dgHistory.CurrentRow;
                string strval = rowToSelect.Cells["Select"].Value.ToString();
                if (strval.Length == 0)
                {
                    rowToSelect.Cells["Select"].Value = "True";
                    UpdateHistoriesCollection(rowToSelect, true);//Update the Collection according to User action CheckUncheck of Checkboxes.
                }
                else
                {
                    if (strval == "True")
                    {
                        rowToSelect.Cells["Select"].Value = "False";
                        UpdateHistoriesCollection(rowToSelect, false);//Update the Collection according to User action CheckUncheck of Checkboxes.
                    }
                    else
                    {
                        rowToSelect.Cells["Select"].Value = "True";
                        UpdateHistoriesCollection(rowToSelect, true);//Update the Collection according to User action CheckUncheck of Checkboxes.
                    }
                }

                rowToSelect.Cells["Description"].Selected = true;

                this.dgHistory.CurrentCell = rowToSelect.Cells["Description"];
            }
        }

        string _myCategoryname = "";
        myTreeNode _mynode = null;

        private void trvCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                myTreeNode mynode = null;
                string _Categoryname = "";
                if ((e.Node != null))
                {

                    if (e.Node.Text.Trim() == "History")
                    {
                        //if (e.Node.Checked == false)
                        oHistories.Clear();
                        if (trvCategory.Nodes[0].Nodes[0] != null)
                        {
                            this.trvCategory.AfterSelect -= new TreeViewEventHandler(this.trvCategory_AfterSelect);
                            trvCategory.SelectedNode = trvCategory.Nodes[0].Nodes[0];
                            this.trvCategory.AfterSelect += new TreeViewEventHandler(this.trvCategory_AfterSelect);
                        }

                        if (strAction == "Upload")
                            ChkUnchkChilds(trvCategory.Nodes[0]);
                        else
                        {
                            if (e.Node.Parent == null)
                                ChkUnchkChilds(trvCategory.Nodes[0]);//Clinic Repository
                            else
                                ChkUnchkChilds(e.Node.Parent.Nodes[e.Node.Index]);//Global Repository
                        }

                        //BindGrid(e.Node.Nodes[0].Text, e.Node);
                        //CheckUncheckHistoryItems((myTreeNode)e.Node.Nodes[0], "True");
                        return;
                    }

                    if ((!object.ReferenceEquals(mynode, trvCategory.Nodes[0])))
                    {
                        mynode = (myTreeNode)e.Node;
                        _mynode = mynode;

                        if (strAction == "Download")
                        {
                            _Categoryname = mynode.Text;
                            _myCategoryname = _Categoryname;
                            BindGrid(_Categoryname, e.Node);
                        }
                        else
                        {
                            _Categoryname = mynode.Text;
                            key = mynode.Key;
                            if (key != -1)
                            {
                                BindGrid(_Categoryname, e.Node);
                            }
                            else
                            {
                                dgHistory.DataSource = null;
                            }
                        }

                        CheckUncheckHistoryItems(mynode, "True");
                    }
                    else
                    {
                        dgHistory.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in trvCategory_AfterCheck of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("glocomm Error While Tereeview Category After Check in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void CheckUncheckHistoryItems(myTreeNode HistoryCatNode, string CheckUncheck)
        {
            clsHistory.History oHistory = null;
            bool _IsItemfound = false;
            try
            {
                if (dgHistory.DataSource != null && dgHistory.Rows.Count > 0)
                {
                    for (int i = 0; i < dgHistory.Rows.Count; i++)
                    {
                        if (HistoryCatNode.Checked == true)
                        {
                            for (int HistoriesCnt = 0; HistoriesCnt < oHistories.Count; HistoriesCnt++)
                            {
                                if (strAction == "Upload")
                                {
                                    if (oHistories[HistoriesCnt].Description == dgHistory.Rows[i].Cells["Description"].Value.ToString() && oHistories[HistoriesCnt].CategoryName == dgHistory.Rows[i].Cells["CategoryName"].Value.ToString())
                                    {
                                        dgHistory.Rows[i].Cells["Select"].Value = "True";
                                        oHistories[HistoriesCnt].Select = true;
                                        _IsItemfound = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (oHistories[HistoriesCnt].Description == dgHistory.Rows[i].Cells["Description"].Value.ToString() && oHistories[HistoriesCnt].CategoryName == dgHistory.Rows[i].Cells["CategoryName"].Value.ToString() && oHistories[HistoriesCnt].Specialty == dgHistory.Rows[i].Cells["Specialty"].Value.ToString())
                                    {
                                        dgHistory.Rows[i].Cells["Select"].Value = "True";
                                        oHistories[HistoriesCnt].Select = true;
                                        _IsItemfound = true;
                                        break;
                                    }
                                }
                            }

                            if (_IsItemfound == false)
                            {
                                dgHistory.Rows[i].Cells["Select"].Value = "True";
                                //Add values to Collection
                                oHistory = new clsHistory.History();

                                oHistory.Select = true;
                                oHistory.CategoryName = HistoryCatNode.Text;
                                oHistory.Type = "History";
                                oHistory.Description = dgHistory.Rows[i].Cells["Description"].Value.ToString();
                                oHistory.Comments = dgHistory.Rows[i].Cells["Comments"].Value.ToString();
                                oHistory.ConceptID = dgHistory.Rows[i].Cells["ConceptID"].Value.ToString();
                                oHistory.DescriptionID = dgHistory.Rows[i].Cells["DescriptionID"].Value.ToString();
                                oHistory.SnoMedID = dgHistory.Rows[i].Cells["SnoMedID"].Value.ToString();
                                oHistory.SnomedDescription = dgHistory.Rows[i].Cells["SnomedDescription"].Value.ToString();
                                oHistory.TranID1 = dgHistory.Rows[i].Cells["TranID1"].Value.ToString();
                                oHistory.TranID2 = dgHistory.Rows[i].Cells["TranID2"].Value.ToString();
                                oHistory.TranID3 = dgHistory.Rows[i].Cells["TranID3"].Value.ToString();
                                oHistory.ICD9 = dgHistory.Rows[i].Cells["ICD9"].Value.ToString();
                                oHistory.SnomedDefination = dgHistory.Rows[i].Cells["SnomedDefination"].Value.ToString();
                                //Added new cloumn in History master on 20121003
                                oHistory.CPTCode = Convert.ToString(dgHistory.Rows[i].Cells["CPTCode"].Value);
                                oHistory.HistoryType = Convert.ToString(dgHistory.Rows[i].Cells["HistoryType"].Value);
                                //End //Added new cloumn in History master.
                                if (strAction == "Download")
                                    oHistory.Specialty = dgHistory.Rows[i].Cells["Specialty"].Value.ToString();
                                oHistories.Add(oHistory);
                                //
                            }
                        }
                        else
                        {
                            dgHistory.Rows[i].Cells["Select"].Value = "False";
                            for (int HistoriesCnt = 0; HistoriesCnt < oHistories.Count; HistoriesCnt++)
                            {
                                if (oHistories[HistoriesCnt].CategoryName == dgHistory.Rows[i].Cells["CategoryName"].Value.ToString() && oHistories[HistoriesCnt].Description == dgHistory.Rows[i].Cells["Description"].Value.ToString())
                                {
                                    oHistories.RemoveAt(HistoriesCnt);
                                    _IsItemfound = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While checking unchecking history items in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in CheckUncheckHistoryItems of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (oHistory != null)
                {
                    oHistory.Dispose();
                    oHistory = null;
                }
            }
        }

        private void ShowCheckUncheckHistoryItems(myTreeNode HistoryCatNode)
        {
            bool _blnFound = false;
            try
            {
                //clsGeneral.UpdateLog("In side ShowCheckUncheckHistoryItems  ");
                if (oHistories.Count > 0)
                {
                    for (int i = 0; i < dgHistory.Rows.Count; i++)
                    {
                        for (int HistoriesCnt = 0; HistoriesCnt < oHistories.Count; HistoriesCnt++)
                        {
                            if (strAction == "Download")
                            {
                                //clsGeneral.UpdateLog("In side ShowCheckUncheckHistoryItems  " + strAction);
                                if (oHistories[HistoriesCnt].CategoryName == HistoryCatNode.Text && oHistories[HistoriesCnt].Description == dgHistory.Rows[i].Cells["Description"].Value.ToString() && oHistories[HistoriesCnt].Specialty == dgHistory.Rows[i].Cells["Specialty"].Value.ToString())
                                {
                                    _blnFound = true;//history item found in Histories collection
                                    dgHistory.Rows[i].Cells["Select"].Value = oHistories[HistoriesCnt].Select.ToString();
                                    continue;
                                }
                            }
                            else
                            {
                                //clsGeneral.UpdateLog("In side ShowCheckUncheckHistoryItems  " + strAction);
                                if (oHistories[HistoriesCnt].CategoryName == HistoryCatNode.Text && oHistories[HistoriesCnt].Description == dgHistory.Rows[i].Cells["Description"].Value.ToString())
                                {
                                    _blnFound = true;//history item found in Histories collection
                                    dgHistory.Rows[i].Cells["Select"].Value = oHistories[HistoriesCnt].Select.ToString();
                                    continue;
                                }
                            }
                        }
                    }
                }

                if (_blnFound == false && _blnParentChecked == true && HistoryCatNode.Checked == true)//if history not found add into histories collection
                    CheckUncheckHistoryItems(HistoryCatNode, "True");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        //private void ShowCheckUncheckHistoryItems(myTreeNode HistoryCatNode)
        //{
        //    bool _blnFound = false;

        //    try
        //    {
        //        clsGeneral.UpdateLog("In side ShowCheckUncheckHistoryItems  ");
        //        if (oHistories.Count > 0)
        //        {
        //            int mycnt = oHistories.Count;
        //            if (mycnt > 0)
        //            {
        //                int myi = 0;
        //                int mymax = mycnt;
        //                while (myi <= mymax - 1)
        //                {
        //                    //if (HistoryCatNode.Checked == true)
        //                    //{
        //                    //    dgHistory.Rows[myi].Cells["Select"].Value = true;

        //                    //}
        //                    //else
        //                    //{
        //                    int myicnt = 0;
        //                    int mymaxcnt = dgHistory.Rows.Count - 1;
        //                    while (myicnt <= mymaxcnt)
        //                    {
        //                        if (oHistories[myi].CategoryName == HistoryCatNode.Text && oHistories[myi].Description == dgHistory.Rows[myicnt].Cells["Description"].Value.ToString())
        //                        {
        //                            _blnFound = true;
        //                            dgHistory.Rows[myicnt].Cells["Select"].Value = oHistories[myi].Select.ToString();
        //                        }
        //                        myicnt = myicnt + 1;
        //                    }
        //                    //}
        //                }
        //                myi = myi + 1;
        //            }
        //        }

        //        if (_blnFound == false && _blnParentChecked == true)
        //            CheckUncheckHistoryItems(HistoryCatNode, "True");
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
        //    }
        //}

        private void UpdateHistoriesCollection(DataGridViewRow rowToSelect, bool CheckUncheck)
        {
            bool _IsFoundHistoryItem = false;
            clsHistory.History oHistory = null;

            try
            {
                //clsGeneral.UpdateLog("In side UpdateHistoriesCollection");
                for (int HistoriesCnt = 0; HistoriesCnt < oHistories.Count; HistoriesCnt++)
                {
                    if (oHistories[HistoriesCnt].CategoryName == rowToSelect.Cells["CategoryName"].Value.ToString() && oHistories[HistoriesCnt].Description == rowToSelect.Cells["Description"].Value.ToString())
                    {
                        _IsFoundHistoryItem = true;
                        oHistories.RemoveAt(HistoriesCnt);
                        break;
                    }
                }

                if (_IsFoundHistoryItem == false)
                {
                    //Add values to Collection
                    oHistory = new clsHistory.History();

                    oHistory.Select = true;
                    oHistory.CategoryName = rowToSelect.Cells["CategoryName"].Value.ToString();
                    oHistory.Type = "History";
                    oHistory.Description = rowToSelect.Cells["Description"].Value.ToString();
                    oHistory.Comments = rowToSelect.Cells["Comments"].Value.ToString();
                    oHistory.ConceptID = rowToSelect.Cells["ConceptID"].Value.ToString();
                    oHistory.DescriptionID = rowToSelect.Cells["DescriptionID"].Value.ToString();
                    oHistory.SnoMedID = rowToSelect.Cells["SnoMedID"].Value.ToString();
                    oHistory.SnomedDescription = rowToSelect.Cells["SnomedDescription"].Value.ToString();
                    oHistory.TranID1 = rowToSelect.Cells["TranID1"].Value.ToString();
                    oHistory.TranID2 = rowToSelect.Cells["TranID2"].Value.ToString();
                    oHistory.TranID3 = rowToSelect.Cells["TranID3"].Value.ToString();
                    oHistory.ICD9 = rowToSelect.Cells["ICD9"].Value.ToString();
                    oHistory.SnomedDefination = rowToSelect.Cells["SnomedDefination"].Value.ToString();
                    //Added new cloumn in History master on 20121003
                    oHistory.CPTCode = Convert.ToString(rowToSelect.Cells["CPTCode"].Value);
                    oHistory.HistoryType = Convert.ToString(rowToSelect.Cells["HistoryType"].Value);
                    //End //Added new cloumn in History master.
                    if (strAction == "Download")
                        oHistory.Specialty = rowToSelect.Cells["Specialty"].Value.ToString();
                    oHistories.Add(oHistory);
                    //
                }
            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in UpdateHistoriesCollection of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Update History Collection in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oHistory != null)
                {
                    oHistory.Dispose();
                    oHistory = null;
                }
            }
        }

        //private void ChkUnchkChilds(TreeNode trvCategory)
        //{
        //    for (int i = 0; i <= trvCategory.Nodes.Count - 1; i++)
        //    {
        //        if (trvCategory.Checked == true)
        //            trvCategory.Nodes[i].Checked = true;
        //        else
        //            trvCategory.Nodes[i].Checked = false;
        //    }
        //}

        private void ChkUnchkChilds(TreeNode trvCategory)
        {
            _blnParentChecked = trvCategory.Checked;
            this.trvCategory.AfterCheck -= new TreeViewEventHandler(this.trvCategory_AfterCheck);
            for (int i = 0; i <= trvCategory.Nodes.Count - 1; i++)
            {
                if (i == 0)
                {
                    this.trvCategory.AfterCheck += new TreeViewEventHandler(this.trvCategory_AfterCheck);
                    trvCategory.Nodes[i].Checked = _blnParentChecked;
                    this.trvCategory.AfterCheck -= new TreeViewEventHandler(this.trvCategory_AfterCheck);
                }
                else
                    trvCategory.Nodes[i].Checked = _blnParentChecked;
            }
            this.trvCategory.AfterCheck += new TreeViewEventHandler(this.trvCategory_AfterCheck);
        }

        #region "Download"
        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            //clsGeneral.UpdateLog("In side tlbClinicRepository_Click for download");
            this.Cursor = Cursors.WaitCursor;
            trvCategory.Nodes.Clear();
            dgHistory.DataSource = null;
            oHistories.Clear();

            try
            {
                string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrHistoryflnm + "/" + clsGeneral.gstrHistoryflnm + ".xml";
                //clsGeneral.UpdateLog("Downloading history xml from " + DownloadPath);
                DownloadHistoryXml(DownloadPath, "User", clsGeneral.gstrClinicName);
                //clsGeneral.UpdateLog("Downloaded history xml from " + DownloadPath);

            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in tlbClinicRepository_Click of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                this.Cursor = Cursors.Default;
                //clsGeneral.UpdateLog("glocomm Error While Clicking on Clinic Repository in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            trvCategory.Nodes.Clear();
            dgHistory.DataSource = null;
            oHistories.Clear();
            //clsGeneral.UpdateLog("inside tlbGlobalRepository_Click");
            string strSitepath = "";
            string strSitefolder = "";
            string strFrom = "";
            clsgloCommunity ObjclsgloCommunity = new clsgloCommunity();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            try
            {
                strSitepath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                strSitefolder = clsGeneral.WebGlobalXmlFolder;
                strFrom = "Global";
                //clsGeneral.UpdateLog("inside tlbGlobalRepository_Click : getting the gloLists ");
                gloList = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    gloList.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        gloList.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        gloList.CookieContainer = clsGeneral.oCookie;
                    }
                }

                gloList.Url = strSitepath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                gloList.Timeout = int.MaxValue;
                System.Xml.XmlNode node = gloList.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {
                        if (xmlnode.Attributes["Title"].Value.ToString() == strSitefolder)
                        {
                            DataTable dtGlobalRepository = new DataTable();
                            dtGlobalRepository = ObjclsgloCommunity.GetList(xmlnode.Attributes["Title"].Value.ToString(), strSitepath);
                            if (dtGlobalRepository.Rows.Count > 0 && dtGlobalRepository != null)
                            {
                                foreach (DataRow dr in dtGlobalRepository.Rows)
                                {
                                    if (dr["ContentType"].ToString().Trim() == "Folder")
                                    {
                                        string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + dr["Title"].ToString().Trim() + "/" + clsGeneral.gstrHistoryflnm + "/" + clsGeneral.gstrHistoryflnm + ".xml";
                                        DownloadHistoryXml(DownloadPath, strFrom, dr["Title"].ToString().Trim());
                                    }
                                }
                            }
                        }
                    }
                }

                if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml") == true)
                {
                    //if HistoryGlobal.xml is exist then 1st check History.xml file is exist then Delete the History.xml file
                    //& rename the HistoryGlobal.xml(its contain all global repository data) file to History.xml.
                    if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml"))
                    {
                        File.Delete(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml");
                        File.Move(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml", gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //clsGeneral.UpdateLog("glocomm Error While Clicking on Global Repository in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in tlbGlobalRepository_Click of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                gloList = null;
                ObjclsgloCommunity = null;
                objDlLiquidData = null;
                this.Cursor = Cursors.Default;
            }
        }

        private bool DownloadHistoryXml(string DownloadPath, string IsFrom, string Title)
        {
            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml";
            bool IsDownloadXml = false;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            //
            //clsGeneral.UpdateLog("inside DownloadHistoryXml");
            try
            {
                if (IsFrom == "Global")
                {
                    if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml") == true)
                    {
                        //clsGeneral.UpdateLog("inside DownloadHistoryXml : checking if the global xml file exists then downloading xml");
                        IsDownloadXml = DownloadXML(DownloadPath, clsGeneral.gstrHistoryflnm + ".xml");

                        if (IsDownloadXml == true)
                        {

                            //clsGeneral.UpdateLog("inside DownloadHistoryXml :  if downloading xml is successful ");
                            //show Specialty & Categories
                            string _TableName = "Category";
                            //clsGeneral.UpdateLog("inside DownloadHistoryXml :  getting xmldata " + ServerXmlPath);
                            DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                            if (dt.Rows.Count > 0 && dt != null)
                            {
                                //clsGeneral.UpdateLog("inside DownloadHistoryXml : filling glocommunity history tree view ");
                                FillgloCommunityHistoryTreeView(dt, IsFrom, Title);
                                //clsGeneral.UpdateLog("inside DownloadHistoryXml : filled glocommunity history tree view ");
                            }
                            //end
                            //clsGeneral.UpdateLog("inside DownloadHistoryXml : loading historyglobal.xml ");
                            //Select all History.xml content(Categories) into HistoryGlobal.xml
                            XmlDocument oDocFirst = new XmlDocument();
                            oDocFirst.Load(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml");

                            //clsGeneral.UpdateLog("inside DownloadHistoryXml : loading history.xml ");
                            XmlDocument oDocSecond = new XmlDocument();
                            oDocSecond.Load(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + ".xml");

                            XmlNode oNodeWhereInsert = oDocFirst.SelectSingleNode("/History");
                            foreach (XmlNode oNode in oDocSecond.SelectNodes("/History/Category"))
                            {
                                oNodeWhereInsert.AppendChild(oDocFirst.ImportNode(oNode, true));
                            }
                            oDocFirst.Save(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml");
                            //end
                        }
                    }
                    else
                    {
                        //clsGeneral.UpdateLog("inside DownloadHistoryXml : if the HistoryGlobal.xml does not exist then download xml  ");
                        //execute only ones when HistoryGlobal.xml not exist
                        IsDownloadXml = DownloadXML(DownloadPath, clsGeneral.gstrHistoryflnm + "Global.xml");
                        if (IsDownloadXml == true)
                        {
                            string _TableName = "Category";
                            //clsGeneral.UpdateLog("inside DownloadHistoryXml : loading the data from xml to datatable  ");
                            DataTable dt = objDlLiquidData.GetXmlData(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrHistoryflnm + "Global.xml", _TableName);
                            if (dt.Rows.Count > 0 && dt != null)
                            {

                                FillgloCommunityHistoryTreeView(dt, IsFrom, Title);
                            }
                        }
                        //end
                    }
                }
                else
                {
                    IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);
                    if (IsDownloadXml == true)
                    {
                        string _TableName = "Category";
                        DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            FillgloCommunityHistoryTreeView(dt, IsFrom, Title);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in DownloadHistoryXml of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                IsDownloadXml = false;
                //clsGeneral.UpdateLog("glocomm Error While Downloading XML in  Usercontrol History: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objgloCommunity = null;
                objDlLiquidData = null;
            }
            return IsDownloadXml;
        }

        public bool DownloadXML(string temppath, string filename)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            string strDestinationPath = gloSettings.FolderSettings.AppTempFolderPath;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(temppath);
                //clsGeneral.UpdateLog("inside DownloadXML : creating webrequest for  " +temppath );
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    request.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        ((System.Net.HttpWebRequest)(request)).CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            ((System.Net.HttpWebRequest)(request)).CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            ((System.Net.HttpWebRequest)(request)).CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        ((System.Net.HttpWebRequest)(request)).CookieContainer = clsGeneral.oCookie;
                    }
                }
                request.Timeout = int.MaxValue;
                request.AllowWriteStreamBuffering = false;
                response = (HttpWebResponse)request.GetResponse();
                //clsGeneral.UpdateLog("inside DownloadXML : getting the response  ");
                Stream s = response.GetResponseStream();

                //Write to disk
                FileStream fs = new FileStream(strDestinationPath + "\\" + filename, FileMode.Create);
                //clsGeneral.UpdateLog("inside DownloadXML : creating  a filestream object to read from the file byte ways  ");
                byte[] read = new byte[256];
                int count = s.Read(read, 0, read.Length);

                while (count > 0)
                {
                    fs.Write(read, 0, count);
                    count = s.Read(read, 0, read.Length);
                }
                //Close everything
                fs.Close();
                s.Close();
                response.Close();
                return true;
            }
            catch (System.Net.WebException ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in DownloadXML of History " + strAction , 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                if (response != null)
                    response.Close();
                //clsGeneral.UpdateLog("glocomm Error While DownloadXML in History  Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
        }

        private void FillgloCommunityHistoryTreeView(DataTable dt, string IsFrom, string Title)
        {
            //clsGeneral.UpdateLog("inside FillgloCommunityHistoryTreeView :   ");
            DataTable dc = dt;
            try
            {
                myTreeNode Parentnode = default(myTreeNode);//Clinic node
                myTreeNode mynode = default(myTreeNode);
                if (IsFrom == "User")
                {
                    mynode = new myTreeNode("History", -1);
                    mynode.ImageIndex = 0;
                    mynode.SelectedImageIndex = 0;
                    trvCategory.Nodes.Add(mynode);
                }
                else
                {
                    //Add Clinic name node as parent
                    Parentnode = new myTreeNode(Title, -1);
                    Parentnode.ImageIndex = 2;
                    Parentnode.SelectedImageIndex = 2;
                    trvCategory.Nodes.Add(Parentnode);
                    //end

                    mynode = new myTreeNode("History", -1);
                    mynode.ImageIndex = 0;
                    mynode.SelectedImageIndex = 0;
                    Parentnode.Nodes.Add(mynode);
                }

                myTreeNode mychildnode = default(myTreeNode);

                //get unique Category name 
                DataView UniqueView = new DataView();
                UniqueView.Table = dc;
                dc = UniqueView.ToTable(true, "CategoryName");

                string strname = null;
                for (int i = 0; i <= dc.Rows.Count - 1; i++)
                {
                    //key = Convert.ToInt64(dc.Rows[i][0]);
                    strname = dc.Rows[i]["CategoryName"].ToString();
                    mychildnode = new myTreeNode(strname, -1);
                    mychildnode.ImageIndex = 1;
                    mychildnode.SelectedImageIndex = 1;
                    mynode.Nodes.Add(mychildnode);
                    if (i == 0)
                        trvCategory.SelectedNode = mychildnode;
                }
                trvCategory.ExpandAll();
            }

            catch //(Exception ex)
            {
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in FillgloCommunityHistoryTreeView of History " + strAction + "\n" + ex.Message.ToString (), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dc = null;
            }
            finally
            {
                if (dc != null)
                {
                    dc.Dispose();
                    dc = null;
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        private bool IsFileExist(string Path)
        {
            bool _IsExist = false;
            try
            {
                //clsGeneral.UpdateLog("inside IsFileExist : ");
                WebRequest request = WebRequest.Create(new Uri(Path));
                request.Method = "HEAD";
                //clsGeneral.UpdateLog("inside IsFileExist : getting a web request ");
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    request.UseDefaultCredentials = true;
                else
                {//Client side environment
                    // if (clsGeneral.oCookie == null)
                    // {
                    //     Authentication oAuth = new gloCommunity.Classes.Authentication();
                    //     oAuth.GetSucurityToken();
                    //     clsGeneral.UpdateLog("inside IsFileExist : if envt is production then get the security token ");
                    //}
                    clsGeneral.CheckAuthenticatedCookie();
                    ((System.Net.HttpWebRequest)(request)).CookieContainer = clsGeneral.oCookie;
                }
                //clsGeneral.UpdateLog("inside IsFileExist : getting the response ");
                WebResponse response = request.GetResponse();
                _IsExist = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in IsFileExist of History " + strAction + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                _IsExist = false;
            }
            return _IsExist;
        }

        #endregion
    }
}
