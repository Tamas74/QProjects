using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Net;
using System.IO;   
using gloEMRGeneralLibrary.gloEMRDatabase;
using gloOffice;
using Microsoft.VisualBasic;
using C1.Win.C1FlexGrid;
using System.Xml;
using System.Configuration;

namespace gloCommunity.UserControls
{
    public partial class UC_CVSetup : UserControl
    {
        ClsCVSetup objclsCV = null;
      //  DataTable Resultdata = null;
     //   DataTable tempdata = null;
        DataTable dt = new DataTable();
        DataTable dtdownloadedsum = new DataTable();
        string strAction = "";
        const int Col_MsgID = 1;
        const int Col_CriteriaName = 2;
        const int Col_Displaymsg = 3;

        public UC_CVSetup(string stract)
        {
            InitializeComponent();
            strAction = stract;
        }

        private void UC_CVSetup_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (strAction == "Download")
                {
                    pnltls.Visible = true;
                    tlbClinicRepository_Click(null, null);
                }
                else
                {
                    clsGeneral.isCVDownload = false;
                    objclsCV = new ClsCVSetup();
                    FillCVCriteria();
                    trvCVSetup.Visible = false;
                    pnltls.Visible = false;
                    pnlRepository.Visible = false;
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void fillDownloadedData(DataTable showdata)
        {
            DataTable dtCVSetup = null;
            int bl = 0;
            try
            {

                dtCVSetup = showdata.Clone();
                //DataRow[] drf = showdata.Select("Category='FollowUp'");


                for (int len = 0; len < showdata.Rows.Count; len++)
                {


                    dtCVSetup.ImportRow(showdata.Rows[len]);
                    dtCVSetup.Rows[len]["Select"] = bl;
                }



                flxCVSetup.DataSource = dtCVSetup.Copy();
                dtdownloadedsum = null;
                dtdownloadedsum = dtCVSetup.Copy();


                if (flxCVSetup.DataSource != null)
                {
                    flxCVSetup.Cols[0].DataType = typeof(bool);
                    flxCVSetup.Cols[1].Visible = false;
                    flxCVSetup.Cols[4].Visible = false;
                    flxCVSetup.Cols[5].Visible = false;
                    flxCVSetup.Cols[6].Visible = false;
                    flxCVSetup.Cols[7].Visible = false;
                    flxCVSetup.Cols[8].Visible = false;
                    flxCVSetup.Cols[9].Visible = false;
                }

            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                dtCVSetup.Dispose();



            }
        }

        public void FillCVCriteria()
        {
            try
            {
                dt = objclsCV.GetCV_Criteria();
                if (dt != null)
                {
                    flxCVSetup.Visible = true;
                 //   flxCVSetup.Clear();
                    flxCVSetup.DataSource = null;
                    flxCVSetup.DataSource = dt;
                }
                Designgrid();
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Designgrid()
        {
            try
            {   
                if (dt != null)
                {   
                    flxCVSetup.Cols[0].DataType = typeof(bool);
                    flxCVSetup.Cols[0].AllowEditing = true;
                    flxCVSetup.Cols["Select"].Move(0);
                    flxCVSetup.Cols["Select"].Width = 65;

                    flxCVSetup.Rows.Fixed = 1;
                    flxCVSetup.Cols[1].Caption = "ID";
                    flxCVSetup.Cols[2].Caption = "Criteria Name";
                    flxCVSetup.Cols[3].Caption = "Message";

                    flxCVSetup.Cols[0].Visible = true;
                    flxCVSetup.Cols[1].Visible = false;
                    flxCVSetup.Cols[2].Visible = true;
                    flxCVSetup.Cols[3].Visible = false;

                    flxCVSetup.Cols[4].Visible = false;
                    flxCVSetup.Cols[5].Visible = false;
                    flxCVSetup.Cols[6].Visible = false;
                    flxCVSetup.Cols[7].Visible = false;
                    flxCVSetup.Cols[8].Visible = false;
                    flxCVSetup.Cols[9].Visible = false;

                    flxCVSetup.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    flxCVSetup.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    flxCVSetup.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    flxCVSetup.Cols[1].AllowEditing = false;
                    flxCVSetup.Cols[2].AllowEditing = false;
                    flxCVSetup.Cols[3].AllowEditing = false;

                    flxCVSetup.ShowCellLabels = true;

                    for (int i = 0; i < flxCVSetup.Rows.Count; i++)
                    {
                        flxCVSetup.SetCellCheck(i, flxCVSetup.Cols["Select"].Index, CheckEnum.Unchecked);
                    }
                }

            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void flxCVSetup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Int64 _nCriteriaId = 0;
            string _sCriterianame = string.Empty;
            int _RowIndex = flxCVSetup.HitTest(e.X, e.Y).Row;
            try
            {
                if (flxCVSetup.Rows.Count > 1)
                {
                    if (_RowIndex != 0)
                    {
                        if (strAction == "Upload")
                        {
                            _nCriteriaId = Convert.ToInt64(flxCVSetup.GetData(flxCVSetup.RowSel, Col_MsgID));
                            Criteria oCriteria = null;
                            ClsCVSetup oDM = null;
                            //oCriteria = oDM.GetCriteria(_nCriteriaId);
                            //if (oCriteria != null)
                            //{
                                FillSummery(_nCriteriaId, oCriteria, oDM);
                            //}
                        }//end Upload

                        else//Download
                        {
                            string _EditName = flxCVSetup.GetData(Convert.ToInt32(flxCVSetup.Row), "CVCriteriaName").ToString();
                            bool _IsAssigned = AssignValToCriteria(_EditName, 0);

                        }//end Download
                    }//_RowIndex
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool FillSummery(long CriteriaId, Criteria oCriteria, ClsCVSetup oDM)
        {
          //  string strDemographic = "";
            string strVitals = "";
            bool _IsFillSummery = false;

            txt_summary.Text = "";
            //OtherDetails oOtherDetails;

            try
            {
                if (strAction == "Upload")
                {
                    oCriteria = new Criteria();
                    oDM = new ClsCVSetup();
                    oCriteria = oDM.GetCriteria(CriteriaId);
                }

                if (oCriteria != null)
                {
                    #region "Demograpich Information"
                    if ((oCriteria.AgeMinimum.ToString() != null && oCriteria.AgeMinimum.ToString() != "") && (oCriteria.AgeMaximum.ToString() != null && oCriteria.AgeMaximum.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "Age = " + oCriteria.AgeMinimum + "  To  " + oCriteria.AgeMaximum + Constants.vbNewLine + Constants.vbTab;
                    }
                    
                    if ((oCriteria.Gender.ToString() != null && oCriteria.Gender.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "Gender = " + oCriteria.Gender + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (!string.IsNullOrEmpty(txt_summary.Text.ToString()))
                    {
                        txt_summary.Text = "Demographics:  " + Constants.vbNewLine + Constants.vbTab + txt_summary.Text;
                    }
                    #endregion

                    #region Vitals Information
                    string _Height = "";
                    if (oCriteria.HeightMinimum.ToString() != "")
                    {
                        string[] arrHeight;
                        arrHeight = GetFtInch(oCriteria.HeightMinimum);
                        if (arrHeight.Length > 0)
                            _Height = Conversion.Val(arrHeight[0]) + "' " + Conversion.Val(arrHeight[1]) + "''";
                    }
                    if (!_Height.Contains("0' 0''"))//(!string.IsNullOrEmpty(_Height))
                    {
                        strVitals = strVitals + "Minimum Height = " + _Height + Constants.vbNewLine + Constants.vbTab;
                    }

                    string _HeightMax = "";
                    if (oCriteria.HeightMinimum.ToString() != "")
                    {
                        string[] arrHeight;
                        arrHeight = GetFtInch(oCriteria.HeightMaximum);
                        if (arrHeight.Length > 0)
                            _HeightMax = Conversion.Val(arrHeight[0]) + "' " + Conversion.Val(arrHeight[1]) + "''";
                        //_HeightMax = Conversion.Val(txtHeightMax.Text) + "' " + Conversion.Val(txtHeightMaxInch.Text) + "''";
                    }
                    if (!_HeightMax.Contains("0' 0''"))//(!string.IsNullOrEmpty(_HeightMax))
                    {
                        strVitals = strVitals + "Maximum Height = " + _HeightMax + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BPSittingMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum BP Sitting = " + Convert.ToDouble(Conversion.Val(oCriteria.BPSittingMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BPSittingMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum BP Sitting = " + Convert.ToDouble(Conversion.Val(oCriteria.BPSittingMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.WeightMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Weight Minimum = " + Convert.ToDouble(Conversion.Val(oCriteria.WeightMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.WeightMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Weight Maximum = " + Convert.ToDouble(Conversion.Val(oCriteria.WeightMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BPStandingMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum BP Standing = " + Convert.ToDouble(Conversion.Val(oCriteria.BPStandingMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BPStandingMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum BP Standing = " + Convert.ToDouble(Conversion.Val(oCriteria.BPStandingMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.TempratureMinumum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum Temperature = " + Convert.ToDouble(Conversion.Val(oCriteria.TempratureMinumum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.TempratureMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum Temperature = " + Convert.ToDouble(Conversion.Val(oCriteria.TempratureMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.PulseMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum Pulse = " + Convert.ToDouble(Conversion.Val(oCriteria.PulseMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.PulseMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum Pulse = " + Convert.ToDouble(Conversion.Val(oCriteria.PulseMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BMIMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum BMI = " + Convert.ToDouble(Conversion.Val(oCriteria.BMIMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.BMIMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum BMI = " + Convert.ToDouble(Conversion.Val(oCriteria.BMIMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.PulseOXMinimum) != 0.0)
                    {
                        strVitals = strVitals + "Minimum PulseOX = " + Convert.ToDouble(Conversion.Val(oCriteria.PulseOXMinimum.ToString().Trim())) + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (Convert.ToDouble(oCriteria.PulseOXMaximum) != 0.0)
                    {
                        strVitals = strVitals + "Maximum PulseOX = " + Convert.ToDouble(Conversion.Val(oCriteria.PulseOXMaximum.ToString().Trim())) + Constants.vbNewLine + Constants.vbNewLine;
                    }
                    if (!string.IsNullOrEmpty(strVitals))
                    {
                        txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine + "Vitals:  " + Constants.vbNewLine + Constants.vbTab + strVitals;
                    }
                    #endregion

                    #region "Show Other Details."
                    string strhistory = "";
                    string strDrugs = "";
                    string strICD9 = "";
                    string strCPT = "";
                    string strRadiology = "";
                    string strLab = "";
                    string strProb = "";

                    if (oCriteria.OtherDetails != null)
                    {
                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "History")
                            {
                                if (strhistory.Contains("History:"))
                                {
                                    if (strhistory.Contains(oCriteria.OtherDetails[i].CategoryName))
                                    {
                                        strhistory = strhistory + "," + oCriteria.OtherDetails[i].ItemName;
                                    }
                                    else
                                    {
                                        strhistory = strhistory + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].ItemName + Constants.vbNewLine + Constants.vbTab + Constants.vbTab + oCriteria.OtherDetails[i].ItemName;
                                    }
                                }
                                else
                                {
                                    strhistory = "History:  " + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].CategoryName + Constants.vbNewLine + Constants.vbTab + Constants.vbTab + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + Constants.vbNewLine + strhistory;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "Medication")
                            {
                                if (strDrugs.Contains("Drugs:"))
                                {
                                    strDrugs = strDrugs + "," + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strDrugs = "Drugs:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + strDrugs;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "ICD9")
                            {
                                if (strICD9.Contains("ICD9:"))
                                {
                                    strICD9 = strICD9 + "," + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strICD9 = "ICD9:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + Constants.vbNewLine + strICD9;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "CPT")
                            {
                                if (strCPT.Contains("CPT:"))
                                {
                                    strCPT = strCPT + "," + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strCPT = "CPT:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].CategoryName + " - " + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + Constants.vbNewLine + strCPT;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "Lab")
                            {
                                if (strLab.Contains("Labs:"))
                                {
                                    strLab = strLab + "," + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strLab = "Labs:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + strLab;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "Order")
                            {
                                if (strRadiology.Contains("Orders:"))
                                {
                                    strRadiology = strRadiology + "," + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strRadiology = "Orders:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + strRadiology;

                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            if (oCriteria.OtherDetails[i].DetailType.ToString().Trim() == "Problemlist")
                            {
                                if (strProb.Contains("Problem List:"))
                                {
                                    strProb = strProb + "," + oCriteria.OtherDetails[i].ItemName;
                                }
                                else
                                {
                                    txt_summary.Text = txt_summary.Text + Constants.vbNewLine + Constants.vbNewLine + Constants.vbNewLine;
                                    strProb = "Problem List:" + Constants.vbNewLine + Constants.vbTab + oCriteria.OtherDetails[i].ItemName;
                                }
                            }
                        }
                        txt_summary.Text = txt_summary.Text + Constants.vbNewLine + strProb;
                    }//oCriteria.OtherDetails != null
                    #endregion


                    _IsFillSummery = true;
                }//oCriteria !=null
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (oCriteria != null)
                    oCriteria = null;
                if (oDM != null)
                    oDM = null;
            }
            return _IsFillSummery;
        }

        public string[] GetFtInch(string strHeight)
        {
            //Dim arrHeight() As String
            strHeight = Strings.Mid(strHeight, 1, Strings.Len(strHeight) - 1);
            //arrHeight = 
            return Strings.Split(strHeight, "'");

            //Return arrHeight
        }

        private void tlbClinicRepository_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlRepository.Visible = false;
            trvCVSetup.Nodes.Clear();
            txt_summary.Text = "";
            cleargriddata();

            try
            {
                string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrCVSetupflnm + "/" + clsGeneral.gstrCVSetupflnm + ".xml";
                DownloadPatCriteriaFrmgloCommunity(DownloadPath, "User", clsGeneral.gstrClinicName);
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                pnlRepository.Visible = true;
                trvCVSetup.Nodes.Clear();
                txt_summary.Text = "";
                cleargriddata();
                GetGlobalCVSetupList();
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void cleargriddata()
        {
            try
            {
                //flxCVSetup.Clear();
                flxCVSetup.DataSource = null;

                if (flxCVSetup.Rows.Count > 1)
                {
                    flxCVSetup.Rows.RemoveRange(1, flxCVSetup.Rows.Count - 1);
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private bool DownloadPatCriteriaFrmgloCommunity(string DownloadPath, string IsFrom, string Title)
        {
            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrCVSetupflnm + ".xml";
            bool IsDownloadXml = false;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = null;
            //
            DataColumn ColSelect = null;

            try
            {
                objgloCommunity = new clsgloCommunity();
                IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                if (IsDownloadXml == true)
                {
                    DataSet dsCVSetup = ReadDmSetupXml(ServerXmlPath);
                    if (dsCVSetup != null && dsCVSetup.Tables.Count > 0)
                    {
                        //Fill Patient Criteria from gloCommunity

                        dt = dsCVSetup.Tables["CriteriaName"];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //Add Select column for selecting Patient Criteria.
                            ColSelect = new DataColumn("Select", typeof(System.Boolean));
                            dt.Columns.Add(ColSelect);
                            //End
                            flxCVSetup.DataSource = dt.DefaultView;
                            Designgrid();
                        }
                        //end
                    }
                }
            }
            catch //(Exception ex)
            {
                IsDownloadXml = false;
            }
            finally
            {
                objgloCommunity = null;
            }
            return IsDownloadXml;
        }

        private DataSet ReadDmSetupXml(string ServerXmlPath)
        {
            DataSet dsCVSetup = null;
            try
            {
                dsCVSetup = new DataSet();
                dsCVSetup.ReadXml(ServerXmlPath);
                return dsCVSetup;
            }
            catch //(Exception ex)
            {
                dsCVSetup = null;
                //MessageBox.Show(ex.Message);
            }

            finally
            {
                if (dsCVSetup != null)
                {
                    dsCVSetup.Dispose();
                    dsCVSetup = null;
                }
            }
            return dsCVSetup;
        }

        private void GetGlobalCVSetupList()
        {
            clsgloCommunity objclsgloCommunity = new clsgloCommunity();
            gloLists.Lists myservice = new gloLists.Lists();
            try
            {
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    myservice.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        myservice.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            myservice.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            myservice.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        myservice.CookieContainer = clsGeneral.oCookie;
                    }
                    //End
                }
                
                myservice.Url = clsGeneral.Webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                System.Xml.XmlNode node = myservice.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {
                        if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.WebGlobalXmlFolder)
                        {
                            DataTable dt = new DataTable();
                            dt = objclsgloCommunity.GetList(xmlnode.Attributes["Title"].Value.ToString(), clsGeneral.Webpath + "/");

                            for (int lenitem = 0; lenitem <= dt.Rows.Count - 1; lenitem++)
                            {
                                if (dt.Rows[lenitem]["ContentType"].ToString().Trim() == "Folder")
                                {
                                    gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                                    string StrName = dt.Rows[lenitem]["title"].ToString();
                                    tr.Text = StrName;
                                    string fileUrl = clsGeneral.Webpath + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrCVSetupflnm + "/" + clsGeneral.gstrCVSetupflnm + ".xml";
                                    tr.Tag = fileUrl;
                                    tr.ImageIndex = 13;
                                    tr.SelectedImageIndex = 13;
                                    trvCVSetup.Nodes.Add(tr);
                                }
                            }
                        }//End xmlnode.Attributes["Title"]
                    }//End xmlnode.Attributes["BaseType"]
                }//End foreach
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
     
            }
            finally
            {
                objclsgloCommunity = null;
                myservice = null;
            }
        }

        private void flxCVSetup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool _CellCheckResult = false;
                var _with1 = flxCVSetup;
                int RowIndex = _with1.HitTest(e.X, e.Y).Row;

                if (RowIndex == 0)
                {
                    CheckEnum _CheckUncheck = new CheckEnum();

                    _CheckUncheck = _with1.GetCellCheck(0, 0);

                    if (_CheckUncheck == CheckEnum.Checked)
                        _CellCheckResult = true;
                    else
                        _CellCheckResult = false;

                    for (int i = 1; i < _with1.Rows.Count; i++)
                    {
                        _with1.SetData(i, "Select", _CellCheckResult);
                    }
                }
            }
        }

        private void trvCVSetup_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        public bool AssignValToCriteria(string EditName, long CriteriaId, string Action = "Display")
        {
            bool IsAssigned = false;
            DataSet dsCVSetup = null;

            ClsCVSetup oDM = null;
            Criteria oCriteria = null;
            OtherDetail oOtherDetail = null;
            OtherDetails oOtherDetails = null;

            //pnlSummaryOthers.Visible = true;
            //pnlSummaryOthers.BringToFront();

            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrCVSetupflnm + ".xml";

            try
            {
                oCriteria = new Criteria();
                oDM = new ClsCVSetup();
                oOtherDetails = new OtherDetails();

                //Load DmSetup xml in xmlDocument
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ServerXmlPath);
                //
                //Get Patient Criteria according to Patient Criteria Name
                XmlNode node = null;
                string _DmSetup = "CVSetup"/*RootNode*/, _CriteriaName = "CriteriaName"/*ChildNode*/, _PatientCriteriaName = "CVCriteriaName";
                node = xmlDocument.SelectSingleNode("/" + _DmSetup + "/" + _CriteriaName + "[@" + _PatientCriteriaName + "='" + EditName + "']");
                //
                //Add into dataset
                dsCVSetup = new DataSet();
                XmlTextReader xtr = new XmlTextReader(node.OuterXml, XmlNodeType.Element, null);
                dsCVSetup.ReadXml(xtr);
                //

                //Assign Criteria to oCriteria & oDM object
                if (dsCVSetup != null && dsCVSetup.Tables.Count > 0)
                {
                    oCriteria.Name = EditName;
                    #region "Fill Demographics"
                    if (dsCVSetup.Tables["Demographics"] != null && dsCVSetup.Tables["Demographics"].Rows.Count > 0)
                    {
                        oCriteria.AgeMinimum = Convert.ToDouble(dsCVSetup.Tables["Demographics"].Rows[0]["AgeMin"]);
                        oCriteria.AgeMaximum = Convert.ToDouble(dsCVSetup.Tables["Demographics"].Rows[0]["AgeMax"]);
                        oCriteria.Gender = dsCVSetup.Tables["Demographics"].Rows[0]["Gender"].ToString();
                    }
                    #endregion

                    #region "Fill Vitals"
                    if (dsCVSetup.Tables["Vitals"] != null && dsCVSetup.Tables["Demographics"].Rows.Count > 0)
                    {
                        oCriteria.HeightMinimum = dsCVSetup.Tables["Vitals"].Rows[0]["HeightMin"].ToString();
                        oCriteria.HeightMaximum = dsCVSetup.Tables["Vitals"].Rows[0]["HeightMax"].ToString();
                        oCriteria.WeightMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["WeightMin"]);
                        oCriteria.WeightMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["WeightMax"]);
                        oCriteria.BMIMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BMIMin"]);
                        oCriteria.BMIMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BMIMax"]);
                        oCriteria.TempratureMinumum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["TemperatureMin"]);
                        oCriteria.TempratureMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["TemperatureMax"]);
                        oCriteria.PulseMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["PulseMin"]);
                        oCriteria.PulseMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["PulseMax"]);
                        oCriteria.PulseOXMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["PulseOxMin"]);
                        oCriteria.PulseOXMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["PulseOxMax"]);
                        oCriteria.BPSittingMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BPSittingMin"]);
                        oCriteria.BPSittingMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BPSittingMax"]);
                        oCriteria.BPStandingMinimum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BPStandingMin"]);
                        oCriteria.BPStandingMaximum = Convert.ToDouble(dsCVSetup.Tables["Vitals"].Rows[0]["BPStandingMax"]);
                        oCriteria.DisplayMessage = dsCVSetup.Tables["Vitals"].Rows[0]["DisplayMessage"].ToString();
                    }
                    #endregion

                    #region "Fill History"
                    if (dsCVSetup.Tables["Historyc"] != null && dsCVSetup.Tables["Historyc"].Rows.Count > 0)
                    {
                        for (int cntHistory = 0; cntHistory < dsCVSetup.Tables["Historyc"].Rows.Count; cntHistory++)
                        {
                            oOtherDetail = new OtherDetail();
                            oOtherDetail.ItemName = dsCVSetup.Tables["Historyc"].Rows[cntHistory]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsCVSetup.Tables["Historyc"].Rows[cntHistory]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsCVSetup.Tables["Historyc"].Rows[cntHistory]["Operator"].ToString();
                            oOtherDetail.DetailType = (enumDetailType)Convert.ToInt32(dsCVSetup.Tables["Historyc"].Rows[cntHistory]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Drugs"
                    if (dsCVSetup.Tables["Drugsc"] != null && dsCVSetup.Tables["Drugsc"].Rows.Count > 0)
                    {
                        for (int cntDrugs = 0; cntDrugs < dsCVSetup.Tables["Drugsc"].Rows.Count; cntDrugs++)
                        {
                            oOtherDetail = new OtherDetail();
                            oOtherDetail.ItemName = dsCVSetup.Tables["Drugsc"].Rows[cntDrugs]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsCVSetup.Tables["Drugsc"].Rows[cntDrugs]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsCVSetup.Tables["Drugsc"].Rows[cntDrugs]["Operator"].ToString();
                            oOtherDetail.DetailType = (enumDetailType)Convert.ToInt32(dsCVSetup.Tables["Drugsc"].Rows[cntDrugs]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Lab"
                    if (dsCVSetup.Tables["Labc"] != null && dsCVSetup.Tables["Labc"].Rows.Count > 0)
                    {
                        for (int cntLab = 0; cntLab < dsCVSetup.Tables["Labc"].Rows.Count; cntLab++)
                        {
                            oOtherDetail = new OtherDetail();
                            oOtherDetail.ItemName = dsCVSetup.Tables["Labc"].Rows[cntLab]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsCVSetup.Tables["Labc"].Rows[cntLab]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsCVSetup.Tables["Labc"].Rows[cntLab]["Operator"].ToString();
                            oOtherDetail.DetailType = (enumDetailType)Convert.ToInt32(dsCVSetup.Tables["Labc"].Rows[cntLab]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Order"
                    if (dsCVSetup.Tables["Orderc"] != null && dsCVSetup.Tables["Orderc"].Rows.Count > 0)
                    {
                        for (int cntOrder = 0; cntOrder < dsCVSetup.Tables["Orderc"].Rows.Count; cntOrder++)
                        {
                            oOtherDetail = new OtherDetail();
                            oOtherDetail.ItemName = dsCVSetup.Tables["Orderc"].Rows[cntOrder]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsCVSetup.Tables["Orderc"].Rows[cntOrder]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsCVSetup.Tables["Orderc"].Rows[cntOrder]["Operator"].ToString();
                            oOtherDetail.DetailType = (enumDetailType)Convert.ToInt32(dsCVSetup.Tables["Orderc"].Rows[cntOrder]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    //BIND OTHER DETAILS TO CRITERIA
                    if (oOtherDetails.Count > 0)
                        oCriteria.OtherDetails = oOtherDetails;
                }
                //
                if (Action == "Display")
                    IsAssigned = FillSummery(0, oCriteria, oDM);
                else if (Action == "Download")
                {
                    //Save Criteria
                    long _CriteriaId = oCriteria.AddCVCriteria(oCriteria);
                    if (_CriteriaId > 0)
                        IsAssigned = true;
                }
                else
                {
                    //Modify Existing Criteria according to gloCommunity Criteria
                    long _CriteriaId = oCriteria.ModifyCVCriteria(CriteriaId, oCriteria);
                    if (_CriteriaId > 0)
                        IsAssigned = true;
                }
            }
            catch (Exception ex)
            {
                dsCVSetup = null;
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error Assigning Criteria for CV Setup  in  Usercontrol CV Setup : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (dsCVSetup != null)
                {
                    dsCVSetup.Dispose();
                    dsCVSetup = null;
                }
                if (oCriteria != null)
                    oCriteria = null;
                if (oDM != null)
                    oDM = null;
                if (oOtherDetail != null)
                    oOtherDetail = null;
                if (oOtherDetails != null)
                    oOtherDetails = null;
            }
            return IsAssigned;
        }

        private void trvCVSetup_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                txt_summary.Text = "";
                cleargriddata();
                DownloadPatCriteriaFrmgloCommunity(e.Node.Tag.ToString(), "Global", clsGeneral.gstrClinicName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error  for Node Double Click  in  Usercontrol CV Setup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}

