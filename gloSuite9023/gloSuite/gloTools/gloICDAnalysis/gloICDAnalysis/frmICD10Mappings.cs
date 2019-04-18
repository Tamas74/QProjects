using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Collections;
using C1.Win.C1FlexGrid;

namespace gloICDAnalysis
{
    public partial class frmICD10Mappings : Form
    {
        private const Int16 COL = 0;
        private const Int16 COL_ICD9Code = 1;
        private const Int16 COL_ICD10Structure = 2;
        private const Int16 COL_ICD10ID = 3;
        private const Int16 COL_MappingType = 4;
        private const Int16 COL_Text = 5;
        
        public enum SuggestionType
        {
            ICD10,
            ICD9,
            Description
        }

        public string DBConnectionString;


        DataTable _CodeList = new DataTable();

         public frmICD10Mappings(string dbConnectionString, DataTable CodeList)
        {
            DBConnectionString = dbConnectionString;
            _CodeList = CodeList;
            InitializeComponent();
        }

        public SuggestionType CurrentSelection
        {
            get
            {
                if (optICD10.Checked)
                {
                    return SuggestionType.ICD10;
                }
                else if (optDescription.Checked)
                {
                    return SuggestionType.Description;
                }

                return SuggestionType.ICD10;
            }
        }

        public string CurrentMapping
        {
            get
            {
                string map = "F";
                if (rbBackword.Checked)
                {
                    map = "B";
                }
                else if (rbForward.Checked)
                {
                    map =  "F";
                }
                return map;
            }
        }

        private void frmICD10Search_Load(object sender, EventArgs e)
        {
            Style(c1FlexGrid1, true);
            LoadSuggesion(SuggestionType.ICD10);
            panel2.Hide();
            //DesignGrid();
            LoadData();
        }

        private void tlbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchOption_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                LoadSuggesion(CurrentSelection);
                ResetForm();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void MappingOption_CheckedChanged(object sender, EventArgs e)
        {
            try
            {              
                LoadData();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                LoadData();                
            }
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void c1FlexGrid1_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (c1FlexGrid1.RowSel >= 1)
            {
                string code = Convert.ToString(c1FlexGrid1.GetData(c1FlexGrid1.RowSel, COL_ICD10Structure));
                if (!string.IsNullOrEmpty(code))
                {
                    //LoadNotes(code);
                }
                else
                {
                    panel5.Visible = false;
                }
            }
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string[] sTextData = txtSearch.Text.Split(':');
                string sCode = string.Empty;

                LoadMappingsRevised(_CodeList);

               // LoadMappings_New(_CodeList); 
                
                //if (CurrentSelection == SuggestionType.ICD10)
                //{
                //    if (sTextData.Length > 0)
                //    {
                //        sCode = sTextData[0];
                //    }
                //}
                //else if (CurrentSelection == SuggestionType.Description)
                //{
                //    if (sTextData.Length > 1)
                //    {
                //        sCode = sTextData[1];
                //    }
                //}
                // sCode = string.Join(",", _CodeList.ToArray());
                // sCode = "'" + sCode + "'";
                //if (!string.IsNullOrEmpty(sCode))
                    //{
                      //  sCode = sCode.Replace(".", "").Trim();

                        //c1FlexGrid1.AfterSelChange -= new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid1_AfterSelChange);
                        //LoadMappingsRevised(sCode.Replace(".", ""), 0, _CodeList.to);
                        //c1FlexGrid1.Tree.Show(3);
                        //c1FlexGrid1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid1_AfterSelChange);
                    //}
                    //else
                    //{
                    //    ResetForm();
                    //}
               
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while loading the mappings");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DesignGrid()
        {
            c1FlexGrid1.Clear();

            c1FlexGrid1.Cols.Fixed = 0;
            c1FlexGrid1.Rows.Fixed = 1;
            c1FlexGrid1.Cols.Count = 4;
            c1FlexGrid1.Rows.Count = 1;
            c1FlexGrid1.ExtendLastCol = true;
            c1FlexGrid1.Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1FlexGrid1.Styles.Normal.Border.Style = BorderStyleEnum.None;

            c1FlexGrid1.DrawMode = DrawModeEnum.OwnerDraw;

            c1FlexGrid1.Tree.Column = 0;
            c1FlexGrid1.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Symbols;
            c1FlexGrid1.Tree.Indent = 20;

            c1FlexGrid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes;

          
            c1FlexGrid1.Cols[COL].Visible = true;
            c1FlexGrid1.Cols[COL_ICD10Structure].Visible = true;
            c1FlexGrid1.Cols[COL_ICD10ID].Visible = true;
            c1FlexGrid1.Cols[COL_MappingType].Visible = false;
            c1FlexGrid1.Cols[COL].Width = 100;
            c1FlexGrid1.Cols[COL_ICD10Structure].Width = 180;

            c1FlexGrid1.SetData(0, COL, "ICD9 Code");
            c1FlexGrid1.SetData(0, COL_ICD10Structure, "ICD10 Code");
            c1FlexGrid1.SetData(0, COL_ICD10ID, "Description");
            c1FlexGrid1.SetData(0, COL_MappingType, "Mapping Type");
        }

        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;
            FlexGrid.Styles.Fixed.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;


            // Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Focus.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            FlexGrid.ShowCellLabels = blnShowCellLabels;
        }

        private void ResetForm()
        {
            try
            {
                treeView1.Nodes.Clear();

                DesignGrid();

                //txtSearch.Text = string.Empty;
                lblLiner.Text = string.Empty;
                lblSelectedICD.Text = string.Empty;

                panel2.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while reset");
            }
        }

        private DataTable GetMappings(DataTable tvpSelectedCodes)
        {           
            using (SqlConnection cn = new SqlConnection(DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("gsp_GetICD10Mapping", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TVP_ICDCodes", tvpSelectedCodes));
                  
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private DataTable GetCodeInfo(string p)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DBConnectionString))
                {
                    string sql = "select sICD9Code,sDescription,nCodeType from icd9 where nICDRevision=9 and replace(sICD9Code,'.','')  ='" + p + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);

                                if (dt != null)
                                { return dt; }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while fetching code information");
            }
            return null;
        }

        private void LoadSuggesion(SuggestionType type)
        {
            AutoCompleteStringCollection s = new AutoCompleteStringCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(DBConnectionString))
                {
                    string sql = "select sICD9Code,sDescription from ICD9 where nICDRevision = 9";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);

                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (type == SuggestionType.ICD10)
                                    {
                                        s.Add(dr["sICD9Code"].ToString() + " : " + dr["sDescription"].ToString());
                                    }
                                    else if (type == SuggestionType.Description)
                                    {
                                        s.Add(dr["sDescription"].ToString() + " : " + dr["sICD9Code"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                txtSearch.AutoCompleteCustomSource = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while loading suggestions");
            }
            finally
            {
                if (s != null) { s = null; }
            }

        }

        private void LoadMappingsRevised(DataTable tvpSelectedCodes)
        {
            C1.Win.C1FlexGrid.CellStyle style = c1FlexGrid1.Styles.Add("text");
            style.BackColor = System.Drawing.Color.FromArgb(204, 224, 248);
            style.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);

            DataTable dtMain = null;
            DataView dvChoice = null;
            DataTable dtScenarios = null;
            DataTable dtChoice = null;
            
            try
            {
                DesignGrid();

                dtMain = GetMappings(tvpSelectedCodes);

                if (dtMain == null || dtMain.Rows.Count<1)
                {
                    ResetForm();
                    return;
                }

                CreateTree(dtMain);

                //C1.Win.C1FlexGrid.Row r;
                //foreach (DataRow row in dtMain.Rows)
                //{
                //    c1FlexGrid1.Rows.Add();
                //    r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                //    c1FlexGrid1.SetData(r.Index, COL, row["sICD9Code"]);
                //    c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, row["sICD10Code"]);
                //    c1FlexGrid1.SetData(r.Index, COL_ICD10ID, row["sDescription"]);
                //}

                //using (DataTable dtMapping = dtMain.DefaultView.ToTable(true, new string[] { "flag_1", "flag_2", "flag_3" }))
                //{
                //    foreach (DataRow dr in dtMapping.Rows)
                //    {
                //        var approx = dr["flag_1"];
                //        var nomap = dr["flag_2"];
                //        var combination = dr["flag_3"];

                //        SetMappingType(approx, nomap, combination);
                //    }
                //}

                if (dtMain != null && dtMain.Rows.Count >= 1)
                {
                    //dtScenarios = dtMain.DefaultView.ToTable(true, new string[] { "scenario" });
                    //dvChoice = dtMain.DefaultView;

                    //DataView dvCodes = dtMain.DefaultView;

                    //C1.Win.C1FlexGrid.Row r;

                    //foreach (DataRow row in dtScenarios.Rows)
                    //{
                    //    #region " Scenarios Section "

                    //    dvChoice.RowFilter = "scenario=" + Convert.ToString(row["scenario"]);
                    //    dtChoice = dvChoice.ToTable(true, new string[] { "choice" });

                    //    if (dtChoice.Rows.Count > 1)
                    //    {
                    //        c1FlexGrid1.Rows.Add();

                    //        r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                    //        r.IsNode = true;
                    //        r.Node.Level = 0;
                    //        r.Style = style;

                    //        c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "Scenario - " + row["scenario"].ToString());
                    //    }

                    //    #endregion

                    //    //foreach (DataRow rView in dtChoice.Rows)
                    //    //{
                    //    //    #region " Choice header section "

                    //    //    string sSuffix = string.Empty;

                    //    //    c1FlexGrid1.Rows.Add();
                    //    //    r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                    //    //    r.IsNode = true;

                    //    //    dvCodes.RowFilter = "scenario=" + Convert.ToString(row["scenario"] + " and choice =" + Convert.ToString(rView["choice"]));

                    //    //    if (dvCodes.Count > 1)
                    //    //    { sSuffix = "(Choose 1 of " + dvCodes.Count + ")"; }

                    //    //    if (Convert.ToInt16(rView["choice"]) <= 1)
                    //    //    { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "To " + sSuffix); }
                    //    //    else
                    //    //    { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "With " + sSuffix); }

                    //    //    #endregion

                    //    //    //foreach (DataRowView rowView in dvCodes)
                    //    //    //{
                    //    //    //    #region " Choice detail section "

                    //    //    //    c1FlexGrid1.Rows.Add();
                    //    //    //    r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                    //    //    //    r.IsNode = true;
                    //    //    //    r.Node.Level = 1;

                    //    //    //    c1FlexGrid1.SetData(r.Index, COL, rowView["icd_9"].ToString().Trim());
                    //    //    //    c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, rowView["icd_10"].ToString().Trim());
                    //    //    //    c1FlexGrid1.SetCellImage(r.Index, COL_ICD10Structure, imgList.Images[3]);

                    //    //    //    c1FlexGrid1.SetData(r.Index, COL_ICD10ID, rowView["sDescription"].ToString());
                    //    //    //    c1FlexGrid1.SetData(r.Index, COL_MappingType, rowView["sMappingType"].ToString());

                    //    //    //    //if (rowView["sMappingType"].ToString() == "F")
                    //    //    //    //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[5]); }
                    //    //    //    //else
                    //    //    //    //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[6]); }

                    //    //    //    #endregion
                    //    //    //}
                    //    //}
                    //}

                    //LoadOneLiner(text, dtScenarios.Rows.Count);
                }
                else
                {
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (dtMain != null) { dtMain.Dispose(); dtMain = null; }
                if (dvChoice != null) { dvChoice.Dispose(); dvChoice = null; }
                if (dtScenarios != null) { dtScenarios.Dispose(); dtScenarios = null; }
                if (dtChoice != null) { dtChoice.Dispose(); dtChoice = null; }
                if (style != null) { style = null; }
            }
        }

        private void CreateTree(DataTable dtMain)
        {
            DataView dvICDCode = null;
            DataTable dtICDCode = null;
            DataView dvChoice = null;
            DataTable dtScenarios = null;
            DataTable dtChoice = null;
            C1.Win.C1FlexGrid.CellStyle style = c1FlexGrid1.Styles.Add("text");
            style.BackColor = System.Drawing.Color.FromArgb(204, 224, 248);
            style.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            try
            {
                C1.Win.C1FlexGrid.Row r;

                //code to set mapping type
                //using (DataTable dtMapping = dtMain.DefaultView.ToTable(true, new string[] { "nApproximateFlag", "nNoMapFlag", "nCombinationFlag" }))
                //{
                //    foreach (DataRow dr in dtMapping.Rows)
                //    {
                //        var approx = dr["nApproximateFlag"];
                //        var nomap = dr["nNoMapFlag"];
                //        var combination = dr["nCombinationFlag"];

                //        SetMappingType(approx, nomap, combination);
                //    }
                //}
                string scode="";
                foreach (DataRow dr in dtMain.Rows)
                {
                    if (c1FlexGrid1.GetData(c1FlexGrid1.Rows.Count-1 , COL).ToString() != dr["sICD9Code"].ToString())
                    {

                        c1FlexGrid1.Rows.Add();
                        r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                        r.IsNode = true;
                        r.Node.Level = 0;
                        c1FlexGrid1.SetData(r.Index, COL,dr["sICD9Code"].ToString());
                            
                        dtScenarios = dtMain.DefaultView.ToTable(true, new string[] { "nscenario" });
                        dvChoice = dtMain.DefaultView;

                        DataView dvCodes = dtMain.DefaultView;



                        foreach (DataRow row in dtScenarios.Rows)
                        {
                            #region " Scenarios Section "

                            dvChoice.RowFilter = "nscenario=" + Convert.ToString(row["nscenario"]);
                            dtChoice = dvChoice.ToTable(true, new string[] { "nchoiceList" });

                            if (dtChoice.Rows.Count > 1)
                            {
                                c1FlexGrid1.Rows.Add();

                                r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                                r.IsNode = false;
                                r.Node.Level = 1;
                                r.Style = style;

                                c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "Scenario - " + row["nscenario"].ToString());
                            }

                            #endregion

                            foreach (DataRow rView in dtChoice.Rows)
                            {
                                #region " Choice header section "

                                string sSuffix = string.Empty;

                                c1FlexGrid1.Rows.Add();
                                r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                                r.IsNode = true;
                                r.Node.Level = 2;
                                
                                dvCodes.RowFilter = "nscenario=" + Convert.ToString(row["nscenario"] + " and nchoicelist =" + Convert.ToString(rView["nchoicelist"]));

                                if (dvCodes.Count > 1)
                                { sSuffix = "(Choose 1 of " + dvCodes.Count + ")"; }

                                if (Convert.ToInt16(rView["nchoicelist"]) <= 1)
                                { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "To " + sSuffix); }
                                else
                                { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "With " + sSuffix); }

                                #endregion

                                foreach (DataRowView rowView in dvCodes)
                                {
                                    #region " Choice detail section "

                                    c1FlexGrid1.Rows.Add();
                                    r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                                    r.IsNode = true;
                                    r.Node.Level = 3;

                                    c1FlexGrid1.SetData(r.Index, COL, rowView["sICD9Code"].ToString().Trim());
                                    c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, rowView["sICD10Code"].ToString().Trim());
                                    c1FlexGrid1.SetCellImage(r.Index, COL_ICD10Structure, imgList.Images[3]);

                                    c1FlexGrid1.SetData(r.Index, COL_ICD10ID, rowView["sDescription"].ToString());
                                    //c1FlexGrid1.SetData(r.Index, COL_MappingType, rowView["sMappingType"].ToString());

                                    //if (rowView["sMappingType"].ToString() == "F")
                                    //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[5]); }
                                    //else
                                    //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[6]); }

                                    #endregion
                                }
                            }
                        }
                                



                    }
                    
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadOneLiner(string text, int noOfScenarios)
        {
            using (DataTable dt = GetCodeInfo(text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (noOfScenarios > 1)
                        {
                            lblLiner.Text = "Choose one of the following scenarios to represent ICD9 Code " + dr["sICD9Code"].ToString() + " (" + dr["sDescription"].ToString() + ")";
                        }
                        else
                        {
                            lblLiner.Text = "ICD9 Code " + dr["sICD9Code"].ToString() + " (" + dr["sDescription"].ToString() + ") can be converted as follows";
                        }
                        panel2.Show();
                        break;
                    }
                }
            }
        }

        private void SetMappingType(object approx, object nomap, object combination)
        {
            try
            {
                panel2.Visible = true;
                if (approx.ToString() == "True" || approx.ToString() == "1")
                {
                    if (nomap.ToString() =="True" || nomap.ToString() == "1")
                    {
                        lbl_MappingType.Text = "Mapping : NO DX";
                        lblLiner.Text = "No mapping available to represent ICD-9 code " + txtSearch.Text;
                    }
                    else
                    {
                        if (combination.ToString() == "True" || combination.ToString()=="1")
                        {
                            lbl_MappingType.Text = "Mapping : COMBINATION";
                            lblLiner.Text = "Choose one of the following scenario(s) to represent " + txtSearch.Text;
                        }
                        else
                        {
                            lbl_MappingType.Text = "Mapping : APPROXIMATE";
                            lblLiner.Text = "ICD-9 code " + txtSearch.Text + " approximately matches with the following ";
                        }
                    }
                }
                else
                {
                    lbl_MappingType.Text = "Mapping: DIRECT";
                    lblLiner.Text = "ICD-9 code " + txtSearch.Text + " directly converts to following ";
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        private void LoadNotes(string code)
        {
           
            //string desc = Convert.ToString(c1FlexGrid1.GetData(c1FlexGrid1.RowSel, COL_ICD10Structure));
            //lblSelectedICD.Text = desc + " " + code;
           

            this.Cursor = Cursors.WaitCursor;

            List<ICDNote> notes = GetICDNotes(code); ;
            if (notes.Count > 0)
            {
                panel5.Visible = true;
                FillNotesTreeView(notes);
            }
            else
            {
                treeView1.Nodes.Clear();

                //TreeNode node = new TreeNode();
                //node.NodeFont = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);//new Font(treeView1.Font, FontStyle.Bold);
                //node.ForeColor = System.Drawing.Color.Black;
                //node.Text = "No Notes Found.";
                //treeView1.Nodes.Add(node);
                lblSelectedICD.Text = string.Empty;
                panel5.Visible = false;
            }

            lblSelectedICD.Text = Convert.ToString(c1FlexGrid1.GetData(c1FlexGrid1.RowSel, COL_ICD10Structure)) + " " + Convert.ToString(c1FlexGrid1.GetData(c1FlexGrid1.RowSel, COL_ICD10ID));

            this.Cursor = Cursors.Default;


        }

        private void FillNotesTreeView(List<ICDNote> notes)
        {
            treeView1.Nodes.Clear();

            foreach (ICDNote note in notes)
            {
                TreeNode node = new TreeNode();
                node.NodeFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);//new Font(treeView1.Font, FontStyle.Bold);
                node.ForeColor = System.Drawing.Color.DarkBlue;

                node.Text = note.TypeCaption; //note.Type.ToString();
                node.ImageIndex = 4;
                foreach (string n in note.Notes)
                {
                    TreeNode Td = new TreeNode();
                    Td.Text = n;
                    Td.ImageIndex = 1;
                    node.Nodes.Add(Td);

                }
                treeView1.Nodes.Add(node);
            }
            treeView1.ExpandAll();
        }

        #region "Notes functions"

        private class ICDNote
        {
            public enum NoteType
            {
                Other,
                Custom,
                Includes,
                Excludes1,
                Excludes2,
                InclusionTerm,
                CodeAlso,
                CodeFirst,
                UseAdditionalCode,
                SevenCharNote,
                Note
            }

            public ICDNote()
            {
                //AdditionalNotes = new List<string>();
            }

            public ICDNote(NoteType type, List<string> notes, string typeCaption = "")
            {
                Type = type;
                Notes = notes;
                TypeCaption = typeCaption;
                //AdditionalNotes = new List<string>();
            }

            public NoteType Type { get; set; }
            public List<string> Notes { get; set; }
            public string TypeCaption { get; set; }
            //public List<string> AdditionalNotes { get; set; }
        }

        private XDocument LoadICDNotesXML()
        {
            XDocument doc = null;
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = DBConnectionString;
                using (SqlCommand command = new SqlCommand("Select XMLData from ICDNotes Where ID=1", conn))
                {
                    conn.Open();
                    XmlReader reader = command.ExecuteXmlReader();
                    if (reader.Read())
                    {
                        doc = System.Xml.Linq.XDocument.Load(reader);

                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
                return null;
            }
            return doc;
        }
        private List<ICDNote> GetICDNotes(string code)
        {
            List<ICDNote> notes = new List<ICDNote>();
            try
            {
                var doc = LoadICDNotesXML();
                foreach (XElement element in doc.Descendants("diag"))
                {
                    string nodeValue = element.Element("name").Value;
                    if (nodeValue.Replace(".", "") == code)
                    {
                        notes = ExtractNotes(element);
                        break;
                    }
                }
                return notes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
                return null;
            }
        }

        private List<ICDNote> ExtractNotes(XElement element)
        {
            List<ICDNote> notes = new List<ICDNote>();
            foreach (XNode child in element.Nodes())
            {
                XElement childElement = (XElement)child;

                var temp = from record in childElement.Elements("note")
                           select record.Value;

                if (temp.FirstOrDefault() != null)
                {
                    string sEnumType = string.Empty;
                    ICDNote.NoteType type = ICDNote.NoteType.Other;

                    if (childElement.Name.ToString() == "includes")
                    {
                        type = ICDNote.NoteType.Includes;
                        sEnumType = "Includes";
                    }
                    else if (childElement.Name.ToString() == "excludes1")
                    {
                        type = ICDNote.NoteType.Excludes1;
                        sEnumType = "Excludes 1";
                    }
                    else if (childElement.Name.ToString() == "excludes2")
                    {
                        type = ICDNote.NoteType.Excludes2;
                        sEnumType = "Excludes 2";
                    }
                    else if (childElement.Name.ToString() == "inclusionTerm")
                    {
                        type = ICDNote.NoteType.InclusionTerm;
                        sEnumType = "Inclusion Terms";
                    }
                    else if (childElement.Name.ToString() == "codeAlso")
                    {
                        type = ICDNote.NoteType.CodeAlso;
                        sEnumType = "Code Also";
                    }
                    else if (childElement.Name.ToString() == "codeFirst")
                    {
                        type = ICDNote.NoteType.CodeFirst;
                        sEnumType = "Code First";
                    }
                    else if (childElement.Name.ToString() == "useAdditionalCode")
                    {
                        type = ICDNote.NoteType.UseAdditionalCode;
                        sEnumType = "Use Additional Code";
                    }
                    else if (childElement.Name.ToString() == "notes")
                    {
                        type = ICDNote.NoteType.Note;
                        sEnumType = "Notes";
                    }

                    if (childElement.Name.ToString() == "sevenChrNote")
                    {
                        XElement tempElement = (XElement)childElement.NextNode;
                        var temp7char = from record in tempElement.Elements("extension")
                                        select record;

                        type = ICDNote.NoteType.SevenCharNote;

                        //Adding a Seven Char Note
                        ICDNote note = new ICDNote(type, temp.ToList<string>(),"7th Characters");

                        //Adding a Seven Char ExtensionNotes
                        foreach (XElement n in temp7char.ToList())
                        {
                            string s = n.Attribute("char").Value + " - " + n.Value;
                            note.Notes.Add(s);
                        }
                        note.TypeCaption = "Notes for 7th character";
                        notes.Add(note);
                    }
                    else
                    {
                        notes.Add(new ICDNote(type, temp.ToList<string>(), sEnumType));
                    }
                }
            }
            return notes;
        }


        #endregion
                

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ResetForm();
        }

        private void tlstrpBtnExporttoExcel_Click(object sender, EventArgs e)
        {
            c1FlexGrid1.SaveExcel(@"C:\TestICD.xls");
        }


        private void LoadMappings_New(DataTable tvpSelectedCodes)     //(string text, int level)
        { 
            C1.Win.C1FlexGrid.CellStyle style = c1FlexGrid1.Styles.Add("text");
            style.BackColor = System.Drawing.Color.FromArgb(204, 224, 248);
            style.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);

            DataTable dtMain = null;
            //DataView dvChoice = null;
            //DataTable dtScenarios = null;
            //DataTable dtChoice = null;

            try
            {
                DesignGrid_New();

                dtMain = GetMappings(tvpSelectedCodes);

                if (dtMain == null || dtMain.Rows.Count < 1)
                {
                    ResetForm();
                    return;
                }
                DataView dvICD9 = null;
                dvICD9 = dtMain.DefaultView;
                foreach (DataRow dr in tvpSelectedCodes.Rows)
                {
                    C1.Win.C1FlexGrid.Row r;
                    dvICD9.RowFilter = "sICD9Code=" + Convert.ToString(dr["sICDCode"]);
                    c1FlexGrid1.Rows.Add();

                    r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                    r.IsNode = true;
                    //r.Node.Level = 0;
                    r.Style = style;

                    c1FlexGrid1.SetData(r.Index, COL_ICD9Code, Convert.ToString(dr["sICDCode"]));
                    CreateMappingTree(style, dvICD9);
                }
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (dtMain != null) { dtMain.Dispose(); dtMain = null; }
                //if (dvChoice != null) { dvChoice.Dispose(); dvChoice = null; }
                //if (dtScenarios != null) { dtScenarios.Dispose(); dtScenarios = null; }
                //if (dtChoice != null) { dtChoice.Dispose(); dtChoice = null; }
                if (style != null) { style = null; }
            }
        }

        private void CreateMappingTree(C1.Win.C1FlexGrid.CellStyle style, DataView dvICD)
        {
            DataTable dtMain = dvICD.ToTable();  
            DataView dvChoice = null;
            DataTable dtScenarios = null;
            DataTable dtChoice = null;
            try
            {
                using (DataTable dtMapping = dtMain.DefaultView.ToTable(true, new string[] { "nApproximateFlag", "nNoMapFLAG", "nCombinationFLAG" }))
                {
                    foreach (DataRow dr in dtMapping.Rows)
                    {
                        var approx = dr["nApproximateFlag"];
                        var nomap = dr["nNoMapFLAG"];
                        var combination = dr["nCombinationFLAG"];

                        SetMappingType(approx, nomap, combination);
                    }
                }

                if (dtMain != null && dtMain.Rows.Count >= 1)
                {
                    dtScenarios = dtMain.DefaultView.ToTable(true, new string[] { "nScenario" });
                    dvChoice = dtMain.DefaultView;

                    DataView dvCodes = dtMain.DefaultView;

                    C1.Win.C1FlexGrid.Row r;

                    foreach (DataRow row in dtScenarios.Rows)
                    {
                        #region " Scenarios Section "

                        dvChoice.RowFilter = "nScenario=" + Convert.ToString(row["nScenario"]);
                        dtChoice = dvChoice.ToTable(true, new string[] { "nChoiceList" });

                        if (dtChoice.Rows.Count > 1)
                        {
                            c1FlexGrid1.Rows.Add();

                            r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                            r.IsNode = true;
                            r.Node.Level = 0;
                            r.Style = style;

                            c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "Scenario - " + row["nScenario"].ToString());
                        }

                        #endregion

                        foreach (DataRow rView in dtChoice.Rows)
                        {
                            #region " nChoiceList header section "

                            string sSuffix = string.Empty;

                            c1FlexGrid1.Rows.Add();
                            r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                            r.IsNode = true;

                            dvCodes.RowFilter = "nScenario=" + Convert.ToString(row["nScenario"] + " and nChoiceList =" + Convert.ToString(rView["nChoiceList"]));

                            if (dvCodes.Count > 1)
                            { sSuffix = "(Choose 1 of " + dvCodes.Count + ")"; }

                            if (Convert.ToInt16(rView["nChoiceList"]) <= 1)
                            { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "To " + sSuffix); }
                            else
                            { c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, "With " + sSuffix); }

                            #endregion

                            foreach (DataRowView rowView in dvCodes)
                            {
                                #region " Choice detail section "

                                c1FlexGrid1.Rows.Add();
                                r = c1FlexGrid1.Rows[c1FlexGrid1.Rows.Count - 1];
                                r.IsNode = true;
                                r.Node.Level = 1;

                                c1FlexGrid1.SetData(r.Index, COL_ICD10Structure, rowView["sICD10Code"].ToString().Trim());
                                c1FlexGrid1.SetCellImage(r.Index, COL_ICD10Structure, imgList.Images[3]);

                                c1FlexGrid1.SetData(r.Index, COL_ICD10ID, rowView["sDescription"].ToString());
                                //c1FlexGrid1.SetData(r.Index, COL_MappingType, rowView["sMappingType"].ToString());

                                //if (rowView["sMappingType"].ToString() == "F")
                                //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[5]); }
                                //else
                                //{ c1FlexGrid1.SetCellImage(r.Index, COL, imgList.Images[6]); }

                                #endregion
                            }
                        }
                    }

                    //LoadOneLiner(text, dtScenarios.Rows.Count);
                }
                else
                {
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (dtMain != null) { dtMain.Dispose(); dtMain = null; }
                if (dvChoice != null) { dvChoice.Dispose(); dvChoice = null; }
                if (dtScenarios != null) { dtScenarios.Dispose(); dtScenarios = null; }
                if (dtChoice != null) { dtChoice.Dispose(); dtChoice = null; }
                if (style != null) { style = null; }
            }
        }

        private void DesignGrid_New()
        {
            c1FlexGrid1.Clear();

            c1FlexGrid1.Cols.Fixed = 1;
            c1FlexGrid1.Rows.Fixed = 1;
            c1FlexGrid1.Cols.Count = 5;
            c1FlexGrid1.Rows.Count = 1;
            c1FlexGrid1.ExtendLastCol = true;

            c1FlexGrid1.Tree.Column = 1;
            c1FlexGrid1.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Leaf;
            c1FlexGrid1.Tree.Indent = 20;

            c1FlexGrid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes;

            c1FlexGrid1.Cols[COL_ICD9Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter; 

            c1FlexGrid1.Cols[COL].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[COL_ICD10Structure].Visible = true;
            c1FlexGrid1.Cols[COL_ICD10ID].Visible = true;
            c1FlexGrid1.Cols[COL_MappingType].Visible = false;

            c1FlexGrid1.Cols[COL_ICD10Structure].Width = 180;

            c1FlexGrid1.SetData(0, COL_ICD9Code, "ICD9 Code");
            c1FlexGrid1.SetData(0, COL_ICD10Structure, "ICD10 Code");
            c1FlexGrid1.SetData(0, COL_ICD10ID, "Description");
            c1FlexGrid1.SetData(0, COL_MappingType, "Mapping Type");
        }
       
    }
}
