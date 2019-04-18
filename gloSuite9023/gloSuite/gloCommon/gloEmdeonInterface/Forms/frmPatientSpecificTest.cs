using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloDatabaseLayer;

namespace gloEmdeonInterface.Forms
{
    public partial class frmPatientSpecificTest : Form
    {
        #region "Grid Constatnts"

        private const int C1RESULTRANGE_COL_TESTID = 0;
        private const int C1RESULTRANGE_COL_TESNAME = 1;
        private const int C1RESULTRANGE_COL_RESULTID = 2;
        private const int C1RESULTRANGE_COL_RESULTDT = 3;
        private const int C1RESULTRANGE_COL_RESULTV1 = 4;
        private const int C1RESULTRANGE_COL_RESULTV2 = 5;
        private const int C1RESULTRANGE_COL_ISRESULT = 6;
        private const int C1RESULTRANGE_COL_DATE = 7;
        private const int C1RESULTRANGE_COL_RANGE = 8;
        private const int C1RESULTRANGE_COL_LOINC = 9;

        private const int C1RESULTRANGE_COL_COUNT = 10;

        #endregion "Grid Constatnts"

        private string _sResultRange = "LesserThanEqualTo|LesserThan|Equals|GreaterThan|GreaterThanEqualTo|Between";
        private string _sConnectionString = string.Empty;
        private Int64 _nPatientId = 0;
        private bool IsHasTest = false;

        public frmPatientSpecificTest(string sConnectionString, Int64 nPatientId)
        {
            _sConnectionString = sConnectionString;
            _nPatientId = nPatientId;
            InitializeComponent();
        }

        private void FillTests_NEW()
        {
            try
            {
                gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests oLabTests = null; // new gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests();
                gloEMRGeneralLibrary.gloEMRActors.LabActor.Test oLabTest = null;// new gloEMRGeneralLibrary.gloEMRActors.LabActor.Test();
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest oTest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest();

                DataTable dt = new DataTable();
                DataColumn Col2 = new DataColumn("TestID");
                Col2.DataType = System.Type.GetType("System.Decimal");
                dt.Columns.Add(Col2);
                DataColumn Col3 = new DataColumn("TestName");
                Col3.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(Col3);

                oLabTests = oTest.GetTests(false);

                if (oLabTests != null)
                {
                    if (oLabTests.Count > 0)
                    {
                        DataRow row = null;
                        //'Add data from the object to a datatable 
                        for (int i = 0; i <= oLabTests.Count - 1; i++)
                        {
                            //TreeNode trvnode = new TreeNode();
                            oLabTest = oLabTests.get_Item(i);
                            if (oLabTest != null)
                            {
                                row = dt.NewRow();

                                row["TestName"] = oLabTest.Name;
                                row["TestID"] = oLabTest.TestID;
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }
                GloUC_trvTest.ParentMember = null;
                if ((dt != null))
                {
                    GloUC_trvTest.ImageIndex = 0;
                    GloUC_trvTest.SelectedImageIndex = 0;
                    GloUC_trvTest.DataSource = dt;
                    GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
                    GloUC_trvTest.CodeMember = Convert.ToString(dt.Columns["TestName"].ColumnName);
                    GloUC_trvTest.ValueMember = Convert.ToString(dt.Columns["TestID"].ColumnName);
                    GloUC_trvTest.DescriptionMember = Convert.ToString(dt.Columns["TestName"].ColumnName);
                    GloUC_trvTest.FillTreeView();
                }
                if (oLabTests != null)
                {
                    oLabTests.Dispose();
                    oLabTests = null;
                }
                oTest.Dispose();
                oTest = null;
            }
            catch //(Exception ex)
            {

            }
        }

        private void frmPatientSpecificTest_Load(object sender, EventArgs e)
        {
            gloUserControlLibrary.gloC1FlexStyle.Style(c1PatientResultRange, false);

            //gloUserControlLibrary.gloC1FlexStyle.Style(c1PatientResultRange);
            DesignC1TestRangeGrid();
            FillTests_NEW();


            LoadPatientData();
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _nPatientId, _sConnectionString, "gloEMR");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void DesignC1TestRangeGrid()
        {
            //c1PatientResultRange.Clear();
            c1PatientResultRange.DataSource = null;
            c1PatientResultRange.Clear();

            c1PatientResultRange.Rows.Count = 1;
            c1PatientResultRange.Cols.Fixed = 1;
            c1PatientResultRange.Cols.Count = C1RESULTRANGE_COL_COUNT;

            //Set columns
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_TESTID, "Test Id");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_TESNAME, "Test-Result");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_RESULTID, "Result Id");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_RESULTV1, "Result Range1");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_RESULTV2, "Result Range2");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_RESULTDT, "Operator");
            c1PatientResultRange.SetData(0, C1RESULTRANGE_COL_LOINC, "LOINC Code");

            //Set Coloumn Visibliltiy
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_TESTID].Visible = false;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTID].Visible = false;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_TESNAME].Visible = true;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV1].Visible = true;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV2].Visible = true;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].Visible = true;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_ISRESULT].Visible = false;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RANGE].Visible = false;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_DATE].Visible = false;

            c1PatientResultRange.Cols[C1RESULTRANGE_COL_LOINC].Visible = true;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].DataType = typeof(string);

            c1PatientResultRange.Cols[C1RESULTRANGE_COL_LOINC].AllowEditing = false;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_TESNAME].AllowEditing = false;

            //Set Allign
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_TESNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_LOINC].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_LOINC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

            //Set Width
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_TESNAME].Width = 200;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].Width = 100;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV2].Width = 100;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV1].Width = 100;
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_LOINC].Width = 100;

            ///'Set the property for treeview column
            c1PatientResultRange.Tree.Column = C1RESULTRANGE_COL_TESNAME;
            c1PatientResultRange.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
            c1PatientResultRange.Tree.Indent = 15;

            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV1].DataType = typeof(System.Int64);
            c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTV2].DataType = typeof(System.Int64);


        }

        private void AddTest(Int64 nTestId)
        {
            DataTable dtLabTest = new DataTable();

            try
            {
                dtLabTest = GetSpecificTest(nTestId);

                if (dtLabTest == null || dtLabTest.Rows.Count <= 0)
                {
                    MessageBox.Show("No result is associated with this test.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string sResultName = string.Empty;

                sResultName = Convert.ToString(dtLabTest.Rows[0]["ResultName"].ToString());
                
                if (sResultName.Length<=0 )
                {
                    return;
                }

                string sTestName = string.Empty;
                Int64 nnTestId = 0;

                sTestName = Convert.ToString(dtLabTest.Rows[0]["TestName"].ToString());
                nnTestId = Convert.ToInt64(dtLabTest.Rows[0]["TestId"].ToString());

                if (dtLabTest.Rows.Count > 1)
                {
                    if (sTestName.Length > 0 && nnTestId > 0)
                    {
                        c1PatientResultRange.Rows.Add();

                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESTID, nnTestId.ToString());
                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESNAME, sTestName);
                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_ISRESULT, "false");
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].AllowEditing =
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].IsNode = true;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].ImageAndText = true;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Node.Level = 0;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Height = 22;

                        for (int i = 0; i < dtLabTest.Rows.Count; i++)
                        {
                            c1PatientResultRange.Rows.Add();
                            c1PatientResultRange.Rows[i].AllowEditing = true;
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESTID, Convert.ToString(dtLabTest.Rows[i]["TestId"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTID, Convert.ToString(dtLabTest.Rows[i]["ResultId"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESNAME, Convert.ToString(dtLabTest.Rows[i]["ResultName"].ToString()));
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].IsNode = true;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].ImageAndText = true;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Node.Level = 1;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Height = 22;
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_ISRESULT, "true");
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RANGE, Convert.ToString(dtLabTest.Rows[i]["Range"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_DATE, DateTime.Now.ToString());
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_LOINC, dtLabTest.Rows[i]["LOINCID"].ToString());
                        }
                    }

                }
                else
                {
                    if (sTestName.Length > 0 && nnTestId > 0)
                    {
                        c1PatientResultRange.Rows.Add();
                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count-1, C1RESULTRANGE_COL_TESTID, nnTestId.ToString());
                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count-1, C1RESULTRANGE_COL_TESNAME, sTestName);
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count-1].IsNode = true;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count-1].ImageAndText = true;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count-1].Node.Level = 0;
                        c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count-1].Height = 22;
                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count-1, C1RESULTRANGE_COL_ISRESULT, "false");
                        
                        for (int i = 0; i < dtLabTest.Rows.Count; i++)
                        {
                            c1PatientResultRange.Rows.Add();
                            c1PatientResultRange.Rows[i].AllowEditing = true;
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESTID, Convert.ToString(dtLabTest.Rows[i]["TestId"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTID, Convert.ToString(dtLabTest.Rows[i]["ResultId"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESNAME, Convert.ToString(dtLabTest.Rows[i]["ResultName"].ToString()));
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].IsNode = true;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].ImageAndText = true;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Node.Level = 1;
                            c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Height = 22;
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_ISRESULT, "true");
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RANGE, Convert.ToString(dtLabTest.Rows[i]["Range"].ToString()));
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_DATE, DateTime.Now.ToString());
                            c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_LOINC, dtLabTest.Rows[i]["LOINCID"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtLabTest != null)
                {
                    dtLabTest.Dispose();
                }
            }

        }

        private DataTable GetSpecificTest(Int64 nTestId)
        {
            DataTable dtResult = new DataTable();
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;

            try
            {
                sQuery = @"SELECT    ISNULL(Lab_Test_Mst.labtm_Name,'') as TestName,ISNULL(Lab_Test_ResultDtl.labtrd_TestID,0) as TestId,ISNULL(Lab_Test_ResultDtl.labtrd_ResultID,0) as ResultId,ISNULL(Lab_Test_ResultDtl.labtrd_ResultName,'') as ResultName,ISNULL(Lab_Test_ResultDtl.labtrd_RefRange,'')as Range,ISNULL(Lab_Test_ResultDtl.labtrd_LOINCId,'') AS LOINCID
                           FROM  dbo.Lab_Test_Mst INNER JOIN  dbo.Lab_Test_ResultDtl ON dbo.Lab_Test_Mst.labtm_ID = dbo.Lab_Test_ResultDtl.labtrd_TestID where labtrd_TestID=" + nTestId;

                objDbLayer.Connect(false);
                objDbLayer.Retrive_Query(sQuery, out dtResult);
                objDbLayer.Disconnect();
            }
            catch //(Exception ex)
            {
                dtResult = null;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return dtResult;
        }

        private void GloUC_trvTest_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                gloUserControlLibrary.myTreeNode mynode = (gloUserControlLibrary.myTreeNode)e.Node;
                if ((mynode != null))
                {
                    //TreeNode oNode = new TreeNode();
                    //oNode.Tag = mynode.ID;
                    //oNode.Text = mynode.Text;

                    if (IsAllreadyExits( mynode.ID ) )
                    {
                        return;
                    }

                    AddTest( mynode.ID );



                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1PatientResultRange_EnterCell(object sender, EventArgs e)
        {
            if (c1PatientResultRange.ColSel == C1RESULTRANGE_COL_RESULTDT)
            {
                if (Convert.ToString(c1PatientResultRange.GetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_ISRESULT)).ToLower() == "true")
                {
                    c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].AllowEditing = true;
                    c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].ComboList = _sResultRange;
                }
                else
                {
                    c1PatientResultRange.Cols[C1RESULTRANGE_COL_RESULTDT].AllowEditing = false;
                }
            }
        }

        private void LoadPatientData()
        {
            DataTable dtPatientData = new DataTable();
            List<Int64> lsTempIds = new List<long>();
            try
            {
                dtPatientData = GetPatientSpecificData();

                if (dtPatientData == null || dtPatientData.Rows.Count <= 0)
                {
                    return;
                }

                for (int k = 0; k < dtPatientData.Rows.Count; k++)
                {
                    IsHasTest = true;
                    
                    Int64 nTempID = 0;
                    bool blnIsLoaded = false;

                    nTempID = Convert.ToInt64(dtPatientData.Rows[k]["TestId"]);

                    //Avoid multiple etries in grid.
                    if (lsTempIds.Count > 0)
                    {
                        foreach (Int64 ids in lsTempIds)
                        {
                            if (ids == nTempID)
                            {
                                blnIsLoaded = true;
                                break;
                            }
                        }
                    }

                    if (blnIsLoaded)
                    {
                        continue;
                    }

                    c1PatientResultRange.Rows.Add();

                    c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESTID, Convert.ToString(dtPatientData.Rows[k]["TestId"].ToString()));
                    c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESNAME, Convert.ToString(dtPatientData.Rows[k]["TestName"].ToString()));
                    c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_ISRESULT, "false");

                    c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].IsNode = true;
                    c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].ImageAndText = true;
                    c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Node.Level = 0;
                    c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Height = 22;
                    c1PatientResultRange.Rows[k].AllowEditing = false;

                    if (Convert.ToInt64(dtPatientData.Rows[k]["TestId"]) == nTempID)
                    {
                        for (int i = 0; i < dtPatientData.Rows.Count; i++)
                        {
                            if (dtPatientData.Rows[i]["TestId"] == null)
                            {
                                continue;
                            }
                            if (Convert.ToInt64(dtPatientData.Rows[i]["TestId"]) == nTempID)
                            {
                                c1PatientResultRange.Rows.Add();
                                c1PatientResultRange.Rows[i].AllowEditing = true;
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESTID, dtPatientData.Rows[i]["TestId"]);
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTID, dtPatientData.Rows[i]["ResultId"]);
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_TESNAME, dtPatientData.Rows[i]["ResultName"]);
                                c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].IsNode = true;
                                c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].ImageAndText = true;
                                c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Node.Level = 1;
                                c1PatientResultRange.Rows[c1PatientResultRange.Rows.Count - 1].Height = 22;
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_ISRESULT, "true");
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RANGE, dtPatientData.Rows[i]["Range"].ToString());
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_DATE, dtPatientData.Rows[i]["dtDate"].ToString().ToString());
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTV1, dtPatientData.Rows[i]["range1"].ToString().ToString());
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTV2, dtPatientData.Rows[i]["range2"].ToString().ToString());
                                c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_LOINC, dtPatientData.Rows[i]["LOINCID"].ToString().ToString());

                                //"LesserThan|GreaterThan|Equals|Between";
                                switch (dtPatientData.Rows[i]["operator"].ToString().ToString())
                                {
                                    case "-":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "Between");
                                        break;
                                    case "<":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "LesserThan");
                                        break;
                                    case ">":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "GreaterThan");
                                        break;
                                    case "=":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "Equals");
                                        break;
                                    case ">=":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "GreaterThanEqualTo");
                                        break;
                                    case "<=":
                                        c1PatientResultRange.SetData(c1PatientResultRange.Rows.Count - 1, C1RESULTRANGE_COL_RESULTDT, "LessThanEqualTo");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    lsTempIds.Add(nTempID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private DataTable GetPatientSpecificData()
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            DataTable dtResult = new DataTable();
            try
            {
                sQuery = @"SELECT    ISNULL(dbo.Lab_Test_Mst.labtm_Name,'') AS TestName, ISNULL(dbo.Lab_Test_ResultDtl_Patient.labtrp_TestID,0) AS TestId, 
                        ISNULL(dbo.Lab_Test_ResultDtl_Patient.labtrp_ResultID,0) AS ResultId,ISNULL(dbo.Lab_Test_ResultDtl_Patient.labtrp_ResultName,'') AS ResultName, 
                        dbo.Lab_Test_ResultDtl_Patient.labtrp_DateTime as dtDate, ISNULL(dbo.Lab_Test_ResultDtl_Patient.labtrp_RefRange,'') AS Range, 
                        ISNULL( dbo.Lab_Test_ResultDtl_Patient.labtrp_RefRangeValue1,'') as range1, isnull(dbo.Lab_Test_ResultDtl_Patient.labtrp_RefRangeValue2,'') as range2, 
                        ISNULL(dbo.Lab_Test_ResultDtl_Patient.labtrp_RefRangeOperator,'') as operator,ISNULL(Lab_Test_ResultDtl.labtrd_LOINCId,'') AS LOINCID
                        FROM         dbo.Lab_Test_Mst INNER JOIN
                        dbo.Lab_Test_ResultDtl ON dbo.Lab_Test_Mst.labtm_ID = dbo.Lab_Test_ResultDtl.labtrd_TestID INNER JOIN
                        dbo.Lab_Test_ResultDtl_Patient ON dbo.Lab_Test_ResultDtl.labtrd_ResultID = dbo.Lab_Test_ResultDtl_Patient.labtrp_ResultID AND 
                        dbo.Lab_Test_ResultDtl.labtrd_TestID = dbo.Lab_Test_ResultDtl_Patient.labtrp_TestID Where  dbo.Lab_Test_ResultDtl_Patient.labtrp_PatientId=" + _nPatientId;

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtResult);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                dtResult = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }

            return dtResult;
        }

        private bool SavePatientSpecificData()
        {
            bool blnResult = false;
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            List<clsLabFields> lsFields = new List<clsLabFields>();
            try
            {

                if (validateC1Grid())
                {
                    return false;
                }
                lsFields = GetDataFromGrid();

                if (lsFields != null && lsFields.Count > 0)
                {
                    objDbLayer.Connect(false);

                    sQuery = "Delete from Lab_Test_ResultDtl_Patient where labtrp_PatientId=" + _nPatientId;

                    objDbLayer.Execute_Query(sQuery);

                    foreach (clsLabFields oclsLabDataInsertUpdate in lsFields)
                    {
                        sQuery = string.Empty;
                        sQuery = @"Insert Into Lab_Test_ResultDtl_Patient(labtrp_PatientId,labtrp_TestID,labtrp_ResultID,labtrp_ResultName,labtrp_DateTime, labtrp_RefRange,labtrp_RefRangeValue1,labtrp_RefRangeValue2,labtrp_RefRangeOperator,labtrp_LoincId) 
                                Values(" + oclsLabDataInsertUpdate.Labtrp_PatientId.ToString() + "," + oclsLabDataInsertUpdate.Labtrp_TestID.ToString() + "," + oclsLabDataInsertUpdate.Labtrp_ResultID + ",'" + oclsLabDataInsertUpdate.Labtrp_ResultName.Replace("'", "''") + "','" + oclsLabDataInsertUpdate.Labtrp_DateTime.ToShortDateString() + "','" + oclsLabDataInsertUpdate.Labtrp_RefRange.Replace("'", "''") + "','" + oclsLabDataInsertUpdate.Labtrp_RefRangeValue1.Replace("'", "''") + "','" + oclsLabDataInsertUpdate.Labtrp_RefRangeValue2.Replace("'", "''") + "','" + oclsLabDataInsertUpdate.Labtrp_RefRangeOperator.Replace("'", "''") + "','"+oclsLabDataInsertUpdate.Labtrp_LoincId.Replace("'","''")+"')";
                        objDbLayer.Execute_Query(sQuery);
                    }

                    objDbLayer.Disconnect();
                }
                blnResult = true;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return blnResult;
        }

        private List<clsLabFields> GetDataFromGrid()
        {
            List<clsLabFields> lsLabFields = new List<clsLabFields>();
            clsLabFields objFields;
            try
            {
                if (c1PatientResultRange.Rows.Count > 1)
                {


                    for (int i = 1; i < c1PatientResultRange.Rows.Count; i++)
                    {
                        if (c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_ISRESULT).ToString() == "true")
                        {
                            objFields = new clsLabFields();
                            objFields.Labtrp_TestID = Convert.ToInt64(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_TESTID));
                            objFields.Labtrp_ResultName = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_TESNAME));
                            objFields.Labtrp_ResultID = Convert.ToInt64(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTID));
                            objFields.Labtrp_DateTime = Convert.ToDateTime(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_DATE));

                            objFields.Labtrp_RefRangeValue1 = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTV1));
                            objFields.Labtrp_RefRangeValue2 = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTV2));

                            objFields.Labtrp_LoincId = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_LOINC));
                            objFields.Labtrp_PatientId = _nPatientId;

                            //"LesserThan|GreaterThan|Equals|Between";
                            switch (Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTDT)).ToLower())
                            {
                                case "between":
                                    objFields.Labtrp_RefRangeOperator = "-";
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeValue1 + objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue2;
                                    break;

                                case "lesserthan":
                                    objFields.Labtrp_RefRangeOperator = "<";
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue1;
                                    break;

                                case "greaterthan":
                                    objFields.Labtrp_RefRangeOperator = ">";
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue1;
                                    break;

                                case "lesserthanequalto":
                                    objFields.Labtrp_RefRangeOperator = "<=";
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue1;
                                    break;

                                case "greaterthanequalto":
                                    objFields.Labtrp_RefRangeOperator = ">=";
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue1;
                                    break;

                                case "equals":
                                    objFields.Labtrp_RefRangeOperator = "=";                                    
                                    objFields.Labtrp_RefRange = objFields.Labtrp_RefRangeOperator + objFields.Labtrp_RefRangeValue1;
                                    break;

                                default:
                                    break;
                            }

                            

                            lsLabFields.Add(objFields);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lsLabFields = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            return lsLabFields;

        }

        private void tlbbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                c1PatientResultRange.FinishEditing();
                if (c1PatientResultRange.Rows.Count > 1)
                {
                    if (SavePatientSpecificData())
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (IsHasTest)
                    {
                        if (MessageBox.Show("Are you sure?\r\nDo you want to delete all ranges?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            DelteAllPatientSpecificData();
                            this.Close();
                        }
                        else
                        {
                            LoadPatientData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one test", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {MessageBox.Show(this,ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validateC1Grid()
        {
            bool blnResult = false;
            try
            {
                c1PatientResultRange.FinishEditing();

                if (c1PatientResultRange.Rows.Count > 1)
                {
                    for (int i = 1; i < c1PatientResultRange.Rows.Count - 1; i++)
                    {
                        string sOperator = string.Empty;
                        string sValue1 = string.Empty;
                        string sValue2 = string.Empty;

                        sOperator = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTDT));

                        if (sOperator.Length > 0)
                        {
                            sValue1 = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTV1));
                            sValue2 = Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_RESULTV2));

                            switch (sOperator.ToLower())
                            {
                                case "between":
                                    if (sValue1.Length <= 0 || sValue2.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'Between' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;
                                case "lesserthan":
                                    if (sValue2.Length <= 0 && sValue1.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'LesserThan' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;
                                case "greaterthan":
                                    if (sValue2.Length <= 0 && sValue1.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'GreaterThan' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;
                                case "lesserthanequalto":
                                    if (sValue2.Length <= 0 && sValue1.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'LesserThan' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;
                                case "greaterthanequalto":
                                    if (sValue2.Length <= 0 && sValue1.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'GreaterThan' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;
                                case "equals":
                                    if (sValue2.Length <= 0 && sValue1.Length <= 0)
                                    {
                                        MessageBox.Show("Reference ranges should not be empty, when 'equal' operator is selected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return blnResult = true;
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnResult;
        }

        private bool IsAllreadyExits(Int64 nTestID)
        {
            bool _blnResult = false;

            try
            {
                for (int i = 0; i < c1PatientResultRange.Rows.Count - 1; i++)
                {
                    if (Convert.ToString(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_ISRESULT)).ToLower() == "false")
                    {
                        if (Convert.ToInt64(c1PatientResultRange.GetData(i, C1RESULTRANGE_COL_TESTID)) == nTestID)
                        {
                            _blnResult = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                _blnResult = true;
               
            }
            return _blnResult;
        }



        private void tlbbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void oMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(c1PatientResultRange.GetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_ISRESULT)).ToLower() == "false")
            {
                if (MessageBox.Show("Are you sure you want to remove this test?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    C1.Win.C1FlexGrid.CellRange oRange = c1PatientResultRange.Rows[c1PatientResultRange.RowSel].Node.GetCellRange();
                    int nTestStart = oRange.TopRow;
                    int nTestEnd = oRange.BottomRow;
                    c1PatientResultRange.Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1);
                }
            }
            else
            {
                c1PatientResultRange.RemoveItem(c1PatientResultRange.RowSel);
            }
        }

        private void c1PatientResultRange_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //"LesserThan|GreaterThan|Equals|Between";

            if (e.Row > 0)
            {
                string sValue = string.Empty;

                if (c1PatientResultRange.ColSel == C1RESULTRANGE_COL_RESULTDT)
                {
                    sValue = Convert.ToString(c1PatientResultRange.GetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_RESULTDT));

                    //switch (sValue.ToLower())
                    //{
                    //    //case "lesserthan":
                    //    //    c1PatientResultRange.SetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_RESULTV1, "0");
                    //    //    break;
                    //    //case "greaterthan":
                    //    //    c1PatientResultRange.SetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_RESULTV1, "0");
                    //    //    break;
                    //    //case "lesserthanequalto":
                    //    //    c1PatientResultRange.SetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_RESULTV1, "0");
                    //    //    break;
                    //    //case "greaterthanequalto":
                    //    //    c1PatientResultRange.SetData(c1PatientResultRange.RowSel, C1RESULTRANGE_COL_RESULTV1, "0");
                    //    //    break;
                    //    //default:
                    //    //    break;
                    //}
                }
            }

        }

        /// <summary>
        /// Method to delete all  existing data.
        /// </summary>
        private void DelteAllPatientSpecificData()
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            object ObjResult;
            int ncount = 0;
            try
            {
                objDbLayer.Connect(false);

                sQuery = "Select Count(*) from  Lab_Test_ResultDtl_Patient where labtrp_PatientId=" + _nPatientId;

                ObjResult = objDbLayer.ExecuteScalar_Query(sQuery);

                if (ObjResult != null && ObjResult.ToString() != "")
                {
                    ncount = Convert.ToInt16(ObjResult);
                }

                if (ncount > 0)
                {
                    sQuery = "Delete from Lab_Test_ResultDtl_Patient where labtrp_PatientId=" + _nPatientId;
                    objDbLayer.Execute_Query(sQuery);
                }               
                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                ObjResult = null;
            }
        }

        private void c1PatientResultRange_MouseDown(object sender, MouseEventArgs e)
        {
            c1PatientResultRange.ContextMenu = null;
          
            MenuItem oMenuItem = null;
            Point p = new Point();
            try
            {


                int _tmpDataRow = c1PatientResultRange.HitTest(e.X, e.Y).Row;

                if (_tmpDataRow <= 0)
                {
                    return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    c1PatientResultRange.Focus();
                    c1PatientResultRange.Select(c1PatientResultRange.MouseRow, c1PatientResultRange.MouseCol, true);
                    ContextMenu OContextMenu = new ContextMenu();
                    OContextMenu.MenuItems.Clear();

                    oMenuItem = new MenuItem();
                    oMenuItem.Text = "Delete";
                    oMenuItem.Enabled = true;
                    OContextMenu.MenuItems.Add(oMenuItem);
                    oMenuItem.Click += new EventHandler(oMenuItem_Click);

                    p.X = e.X;
                    p.Y = e.Y;
                    try
                    {
                        if (c1PatientResultRange.ContextMenu != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(c1PatientResultRange.ContextMenu);
                            if (c1PatientResultRange.ContextMenu.MenuItems != null)
                            {
                                c1PatientResultRange.ContextMenu.MenuItems.Clear();
                            }
                            c1PatientResultRange.ContextMenu.Dispose();
                            c1PatientResultRange.ContextMenu = null;
                        }
                    }
                    catch
                    {
                    }
                    OContextMenu.Show(c1PatientResultRange, p);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmPatientSpecificTest_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }



    class clsLabFields
    {
        #region "Variables"

      //  Int64 _nPatientId = 0;
        //labtrp_PatientId		numeric(18, 0)	Unchecked
        long labtrp_PatientId = 0;
        //labtrp_TestID			numeric(18, 0)	Unchecked
        long labtrp_TestID = 0;
        //labtrp_ResultID			int	Unchecked
        long labtrp_ResultID = 0;
        //labtrp_DateTime			datetime	Checked
        DateTime labtrp_DateTime;
        //labtrp_ResultName		varchar(255)	Unchecked
        string labtrp_ResultName = "";
        //labtrp_RefRange			varchar(255)	Checked
        string labtrp_RefRange = "";
        //labtrp_RefRangeValue1	varchar(255)	Checked
        string labtrp_RefRangeValue1 = "";
        //labtrp_RefRangeValue2	varchar(255)	Checked
        string labtrp_RefRangeValue2 = "";
        //labtrp_RefRangeOperator	varchar(255)	Checked
        string labtrp_RefRangeOperator = "";

        string labtrp_LoincId = "";

        #endregion "Variables"

        #region "Properties"

        public long Labtrp_PatientId
        {
            get { return labtrp_PatientId; }
            set { labtrp_PatientId = value; }
        }
        public long Labtrp_TestID
        {
            get { return labtrp_TestID; }
            set { labtrp_TestID = value; }
        }
        public long Labtrp_ResultID
        {
            get { return labtrp_ResultID; }
            set { labtrp_ResultID = value; }
        }
        public DateTime Labtrp_DateTime
        {
            get { return labtrp_DateTime; }
            set { labtrp_DateTime = value; }
        }
        public string Labtrp_ResultName
        {
            get { return labtrp_ResultName; }
            set { labtrp_ResultName = value; }
        }

        public string Labtrp_RefRange
        {
            get { return labtrp_RefRange; }
            set { labtrp_RefRange = value; }
        }
        public string Labtrp_RefRangeValue1
        {
            get { return labtrp_RefRangeValue1; }
            set { labtrp_RefRangeValue1 = value; }
        }
        public string Labtrp_RefRangeValue2
        {
            get { return labtrp_RefRangeValue2; }
            set { labtrp_RefRangeValue2 = value; }
        }
        public string Labtrp_RefRangeOperator
        {
            get { return labtrp_RefRangeOperator; }
            set { labtrp_RefRangeOperator = value; }
        }
        public string Labtrp_LoincId
        {
            get { return labtrp_LoincId; }
            set { labtrp_LoincId = value; }
        }

        #endregion "Properties"

    }
}
