using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using Microsoft.VisualBasic;
using C1.Win.C1FlexGrid;
using System.Xml;
using System.Configuration;
using System.Net;
namespace gloCommunity.UserControls
{
    public partial class UCDmSetup : UserControl
    {
        string strAction = "";
    //    private int COL_ID = 0;
     //   private int COL_NAME = 1;
     //   private int COL_SELECT = 2;
        private int COL_COUNT = 3;
        bool blnloadIm = false;
        public UCDmSetup(string _strAction)
        {
            strAction = _strAction;
            InitializeComponent();
        }

        private void UCDmSetup_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (strAction == "Upload")
                {
                    trvDmSetup.Visible = false;
                    pnltls.Visible = false;
                    pnlRepository.Visible = false;

                    FillCriteria();
                }
                else
                {
                    tlbClinicRepository_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While loading  form  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        #region "Upload"
        private bool FillCriteria()
        {
            gloC1FlexStyle objgloC1FlexStyle = null;
            bool IsFillCriteria = false;
            DataTable dtCriteria = null;
            gloStream.DiseaseManagement.DiseaseManagement oclsDmSetup = null;
            DataColumn ColSelect = null;
            try
            {
                objgloC1FlexStyle = new gloC1FlexStyle();
                objgloC1FlexStyle.Style(c1PatientCriteria);

                oclsDmSetup = new gloStream.DiseaseManagement.DiseaseManagement();
                dtCriteria = oclsDmSetup.GetCriteraNames();
                if (dtCriteria != null && dtCriteria.Rows.Count > 0)
                {
                    //Add Select column for selecting Patient Criteria.
                    ColSelect = new DataColumn("Select", typeof(System.Boolean));
                    dtCriteria.Columns.Add(ColSelect);
                    //End
                    c1PatientCriteria.DataSource = dtCriteria.DefaultView;
                    DesignGrid(c1PatientCriteria);
                }
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Fill Criteria in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (objgloC1FlexStyle != null)
                    objgloC1FlexStyle = null;

                if (oclsDmSetup != null)
                    oclsDmSetup = null;

                if (dtCriteria != null)
                {
                    dtCriteria.Dispose();
                    dtCriteria = null;
                }
            }
            return IsFillCriteria;
        }

        private void AddAssociates(myTreeNode mynode, string strType, bool addTemplate = false)
        {
            foreach (myTreeNode myRootNode in trOrderInfo.Nodes[0].Nodes)
            {
                if (myRootNode.Text == strType)
                {
                    //'Loop for all field nodes in each Root node
                    foreach (myTreeNode myTargetNode in myRootNode.Nodes)
                    {
                        //'Check whether the node already exists
                        if (myTargetNode.Text == mynode.Text)
                        {
                            //'If present do nothing
                            return;
                        }
                        if (myRootNode.Text == "IM")
                        {
                            if (myTargetNode.Key == mynode.Key & blnloadIm == false)
                            {
                                return;
                            }
                        }
                    }
                    //'Map all the node values to the associated node
                    myTreeNode Associatenode ;
                    if (myRootNode.Text != "IM")
                    {
                        Associatenode = (myTreeNode)mynode.Clone();
                        Associatenode.Key = mynode.Key;

                        Associatenode.Text = mynode.Text;
                        Associatenode.Tag = mynode.Tag;
                    }
                    else
                    {
                        Associatenode = new myTreeNode();

                    }
                    if (myRootNode.Text == "Guidelines")
                    {
                        Associatenode.DMTemplate = mynode.TemplateResult;
                        Associatenode.DMTemplateName = mynode.DrugName;
                    }
                    if (myRootNode.Text == "Rx")
                    {
                        Associatenode.DMTemplate = mynode.DrugName;
                        Associatenode.DrugName = mynode.DrugName;
                        Associatenode.Dosage = mynode.Dosage;
                        Associatenode.Tag = mynode.Key;
                        Associatenode.DrugForm = mynode.DrugForm;
                        Associatenode.Duration = mynode.Duration;
                        Associatenode.Frequency = mynode.Frequency;
                        Associatenode.DDID = mynode.DDID;
                        Associatenode.NDCCode = mynode.NDCCode;
                        Associatenode.Route = mynode.Route;
                        Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier;
                        Associatenode.IsNarcotics = mynode.IsNarcotics;
                    }
                    if (myRootNode.Text == "IM")
                    {
                        ///'''''''''''''''''''
                        myTreeNode IMnode ;
                        if (string.IsNullOrEmpty(mynode.DrugQtyQualifier.ToString()))
                        {
                            if (mynode.Key == 0 & Convert.ToInt64(mynode.Tag) != 0)
                            {
                                mynode.Key = Convert.ToInt64(mynode.Tag);
                            }

                            IMnode = (myTreeNode)mynode.Clone();
                            IMnode.Key = mynode.Key;

                            IMnode.Text = mynode.Text;
                            IMnode.Tag = mynode.Tag;
                            IMnode.Route = mynode.Route;
                            // Orginal Name of Vaccine
                            IMnode.DrugForm = mynode.DrugForm;
                            //ConceptID
                            IMnode.Duration = mynode.Duration;
                            //DescriptionID
                            IMnode.Frequency = mynode.Frequency;
                            //SnoMedID                    
                            IMnode.DMTemplateName = mynode.DrugName;
                            IMnode.ImageIndex = 0;
                            IMnode.SelectedImageIndex = 0;
                            myRootNode.Nodes.Add(IMnode);
                        }
                        
                        else
                        {
                            for (int Cnt = 1; Cnt <= Convert.ToInt32(mynode.DrugQtyQualifier); Cnt++)
                            {
                                ///'''''''
                                if (mynode.Key != 0 & Convert.ToInt64(mynode.Tag) == 0)
                                {
                                    mynode.Tag = mynode.Key;
                                }
                                ///'''''''

                                IMnode = (myTreeNode)mynode.Clone();
                                IMnode.Key = mynode.Key;
                                IMnode.Route = mynode.Text;
                                //Orginal Name of Vaccine
                                IMnode.Text = mynode.Text + "" + Cnt;
                                IMnode.Tag = mynode.Tag;
                                IMnode.DMTemplateName = mynode.DrugName;
                                IMnode.DrugForm = mynode.DrugForm;
                                //ConceptID
                                IMnode.Duration = mynode.Duration;
                                //DescriptionID
                                IMnode.Frequency = mynode.Frequency;
                                //SnoMedID                    

                                IMnode.ImageIndex = 0;
                                IMnode.SelectedImageIndex = 0;
                                myRootNode.Nodes.Add(IMnode);
                            }
                        }

                    }

                    if (myRootNode.Text != "IM")
                    {

                        Associatenode.ImageIndex = 0;
                        Associatenode.SelectedImageIndex = 0;
                        myRootNode.Nodes.Add(Associatenode);
                    }

                }
            }
            trOrderInfo.ExpandAll();
            blnloadIm = false;
        }

        public bool FillSummery(long CriteriaId, gloStream.DiseaseManagement.Supporting.Criteria oCriteria, gloStream.DiseaseManagement.DiseaseManagement oDM)
        {
            string strVitals = "";
            txt_summary.Text = "";

            bool _IsFillSummery = false;
            //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = null;
            //gloStream.DiseaseManagement.DiseaseManagement oDM = null;
            pnlSummaryOthers.Visible = true;
            pnlSummaryOthers.BringToFront();

            try
            {
                if (strAction == "Upload")
                {
                    oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                    oDM = new gloStream.DiseaseManagement.DiseaseManagement();
                    oCriteria = oDM.GetCriteria(CriteriaId, 0);
                }
                
                if (oCriteria != null)
                {
                    #region "Demograpich Information"
                    if ((oCriteria.AgeMinimum.ToString() != null && oCriteria.AgeMinimum.ToString() != "") && (oCriteria.AgeMaximum.ToString() != null && oCriteria.AgeMaximum.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "Age = " + oCriteria.AgeMinimum + "  To  " + oCriteria.AgeMaximum + Constants.vbNewLine + Constants.vbTab;
                    }
                    if (!string.IsNullOrEmpty(oCriteria.City))
                    {
                        txt_summary.Text = txt_summary.Text + "City = " + oCriteria.City + Constants.vbNewLine + Constants.vbTab;
                    }

                    if ((oCriteria.Gender.ToString() != null && oCriteria.Gender.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "Gender = " + oCriteria.Gender + Constants.vbNewLine + Constants.vbTab;
                    }
                    if ((oCriteria.State.ToString() != null && oCriteria.State.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "State = " + oCriteria.State + Constants.vbNewLine + Constants.vbTab;
                    }
                    if ((oCriteria.Race.ToString() != null && oCriteria.Race.ToString() != ""))
                    {
                        txt_summary.Text = txt_summary.Text + "Race = " + oCriteria.Race + Constants.vbNewLine + Constants.vbTab;
                    }
                    if (!string.IsNullOrEmpty(oCriteria.Zip.ToString()))
                    {
                        txt_summary.Text = txt_summary.Text + "Zip Code = " + oCriteria.Zip + Constants.vbNewLine + Constants.vbTab;
                    }

                    if (!string.IsNullOrEmpty(oCriteria.MaritalStatus.ToString()))
                    {
                        txt_summary.Text = txt_summary.Text + "Marital Status = " + oCriteria.MaritalStatus + Constants.vbNewLine + Constants.vbTab;
                    }
                    if (!string.IsNullOrEmpty(oCriteria.EmployeeStatus.ToString()))
                    {
                        txt_summary.Text = txt_summary.Text + "Employee Status = " + oCriteria.EmployeeStatus + Constants.vbNewLine + Constants.vbTab;
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
                        arrHeight = oDM.GetFtInch(oCriteria.HeightMinimum);
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
                        arrHeight = oDM.GetFtInch(oCriteria.HeightMaximum);
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

                    #region"Add Orders to be given Treeview"
                    //Add Labs details
                    if (oCriteria.LabOrders != null)
                    {
                        for (int i = 1; i <= oCriteria.LabOrders.Count; i++)
                        {
                            try
                            {
                                //objList = new myList();
                                //objList = (myList).LabOrders.Item(i);

                                myTreeNode mynode = new myTreeNode(((myList)(oCriteria.LabOrders[i])).DMTemplateName, ((myList)(oCriteria.LabOrders[i])).ID);

                                //objList = null;
                                //check if selected node is rootnode
                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "Labs");
                                    mynode = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //clsGeneral.UpdateLog("glocomm Error While Fill Summary  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
      
                            }
                        }
                    }
                    //end Labs

                    //Add RadiologyOrders details
                    if (oCriteria.RadiologyOrders != null)
                    {
                        for (int i = 1; i <= oCriteria.RadiologyOrders.Count; i++)
                        {
                            try
                            {
                                //objList = new myList();
                                //objList = (myList).LabOrders.Item(i);

                                myTreeNode mynode = new myTreeNode(((myList)(oCriteria.RadiologyOrders[i])).DMTemplateName, ((myList)(oCriteria.RadiologyOrders[i])).ID);

                                //objList = null;
                                //check if selected node is rootnode
                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "Orders");
                                    mynode = null;
                                }
                            }
                            catch //(Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //end RadiologyOrders

                    //Add Referrals details
                    if (oCriteria.Referrals != null)
                    {
                        for (int i = 1; i <= oCriteria.Referrals.Count; i++)
                        {
                            try
                            {
                                myTreeNode mynode = new myTreeNode(((myList)(oCriteria.Referrals[i])).DMTemplateName, ((myList)(oCriteria.Referrals[i])).ID);
                                //check if selected node is rootnode
                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "Referrals");
                                    mynode = null;
                                }
                            }
                            catch //(Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //end Referrals

                    //Add RxDrugs details
                    if (oCriteria.RxDrugs != null)
                    {
                        for (int i = 1; i <= oCriteria.RxDrugs.Count; i++)
                        {
                            try
                            {
                                //objList = new myList();
                                //objList = (myList).LabOrders.Item(i);

                                myTreeNode mynode = new myTreeNode(((myList)(oCriteria.RxDrugs[i])).DMTemplateName, ((myList)(oCriteria.RxDrugs[i])).ID, ((myList)(oCriteria.RxDrugs[i])).DrugName, ((myList)(oCriteria.RxDrugs[i])).Dosage, ((myList)(oCriteria.RxDrugs[i])).DrugForm, ((myList)(oCriteria.RxDrugs[i])).Route, ((myList)(oCriteria.RxDrugs[i])).Frequency, ((myList)(oCriteria.RxDrugs[i])).NDCCode, ((myList)(oCriteria.RxDrugs[i])).IsNarcotic, ((myList)(oCriteria.RxDrugs[i])).Duration, ((myList)(oCriteria.RxDrugs[i])).DDid, ((myList)(oCriteria.RxDrugs[i])).DrugQtyQualifier);
                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "Rx");
                                    mynode = null;
                                }
                            }
                            catch //(Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //end RxDrugs

                    //Add Guidelines details
                    if (oCriteria.Guidelines != null)
                    {
                        for (int i = 1; i <= oCriteria.Guidelines.Count; i++)
                        {
                            try
                            {
                                myTreeNode mynode = new myTreeNode(((myList)(oCriteria.Guidelines[i])).DMTemplateName, ((myList)(oCriteria.Guidelines[i])).ID);
                                if (((myList)(oCriteria.Guidelines[i])).TemplateResult != null)
                                    mynode.TemplateResult = ((myList)(oCriteria.Guidelines[i])).TemplateResult;
                                else
                                    mynode.TemplateResult = null;
                                //check if selected node is rootnode
                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "Guidelines");
                                    mynode = null;
                                }
                            }
                            catch //(Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //end Guidelines

                    //Add IM details
                    if (oCriteria.IMlst != null)
                    {
                        for (int i = 1; i <= oCriteria.IMlst.Count; i++)
                        {
                            try
                            {
                                //objList = new myList();
                                //objList = (myList).LabOrders.Item(i);
                                blnloadIm = true;
                                myTreeNode mynode = new myTreeNode();
                                mynode.Text = ((myList)(oCriteria.IMlst[i])).DMTemplateName;
                                mynode.Tag = ((myList)(oCriteria.IMlst[i])).ID;               //IM ID
                                mynode.Key = ((myList)(oCriteria.IMlst[i])).ID;              //IM ID
                                mynode.DrugForm = ((myList)(oCriteria.IMlst[i])).DrugForm;       //ConceptID
                                mynode.Duration = ((myList)(oCriteria.IMlst[i])).Duration;       //DescriptionID
                                mynode.Frequency = ((myList)(oCriteria.IMlst[i])).Frequency;     //SnoMedID   
                                mynode.DrugQtyQualifier = ((myList)(oCriteria.IMlst[i])).DrugQtyQualifier;
                                mynode.TemplateResult = null;

                                if ((mynode != null))
                                {
                                    AddAssociates(mynode, "IM");
                                    mynode = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //clsGeneral.UpdateLog("glocomm Error While Fill Summary  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            }
                        }
                    }
                    //end IM
                    #endregion

                    _IsFillSummery = true;
                }//oCriteria !=null
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Fill Summary  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
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

        private void AddDefaultNodesToTreeview()
        {
            myTreeNode associatenode = new myTreeNode("Orders", -1);

            trOrderInfo.Nodes.Add(associatenode);

            myTreeNode MyChild = new myTreeNode();
            MyChild.Text = "Labs";
            MyChild.Key = -1;
            MyChild.ImageIndex = 6;
            MyChild.SelectedImageIndex = 6;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Orders";
            MyChild.Key = -1;
            MyChild.ImageIndex = 7;
            MyChild.SelectedImageIndex = 7;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Guidelines";
            MyChild.Key = -1;
            MyChild.ImageIndex = 8;
            MyChild.SelectedImageIndex = 8;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Rx";
            MyChild.Key = -1;
            MyChild.ImageIndex = 9;
            MyChild.SelectedImageIndex = 9;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Referrals";
            MyChild.Key = -1;
            MyChild.ImageIndex = 10;
            MyChild.SelectedImageIndex = 10;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "IM";
            MyChild.Key = -1;
            MyChild.ImageIndex = 12;
            MyChild.SelectedImageIndex = 12;
            associatenode.Nodes.Add(MyChild);

            trOrderInfo.ExpandAll();
        }
        #endregion

        private void DesignGrid(C1.Win.C1FlexGrid.C1FlexGrid GridControl)
        {
            var _with1 = c1PatientCriteria;

            if (strAction == "Upload")
            {
                _with1.SetData(0, "dm_mst_ID", "ID");
                _with1.SetData(0, "Select", "Select");
                _with1.SetData(0, "dm_mst_CriteriaName", "Name");

                _with1.Cols["dm_mst_ID"].Visible = false;
                _with1.Cols["dm_mst_ID"].AllowEditing = false;

                _with1.Cols["Select"].Visible = true;
                _with1.Cols["Select"].AllowEditing = true;
                _with1.Cols["Select"].DataType = typeof(bool);
                //_with1.Cols["Select"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                _with1.Cols["Select"].Move(0);

                _with1.Cols["dm_mst_CriteriaName"].Visible = true;
                _with1.Cols["dm_mst_CriteriaName"].AllowEditing = false;

                if (c1PatientCriteria.Cols.Count == COL_COUNT)
                {
                    c1PatientCriteria.Cols["Select"].Width = 65;
                    c1PatientCriteria.Cols["dm_mst_CriteriaName"].Width = c1PatientCriteria.Width - 20;
                }
            }//end Upload

            else//Download
            {
                _with1.SetData(0, "Select", "Select");
                _with1.SetData(0, "PatientCriteriaName", "Name");

                _with1.Cols["Select"].Visible = true;
                _with1.Cols["Select"].AllowEditing = true;
                _with1.Cols["Select"].DataType = typeof(bool);
                _with1.Cols["Select"].Move(0);

                _with1.Cols["PatientCriteriaName"].Visible = true;
                _with1.Cols["PatientCriteriaName"].AllowEditing = false;

                _with1.Cols["CriteriaName_Id"].Visible = false;
                _with1.Cols["DmSetup_Id"].Visible = false;

                if (c1PatientCriteria.Cols.Count == COL_COUNT)
                {
                    c1PatientCriteria.Cols["Select"].Width = 65;
                    c1PatientCriteria.Cols["PatientCriteriaName"].Width = c1PatientCriteria.Width - 20;
                }
            }//end Download


            for (int i = 0; i < c1PatientCriteria.Rows.Count; i++)
            {
                _with1.SetCellCheck(i, _with1.Cols["Select"].Index, CheckEnum.Unchecked);
            }

            _with1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
        }

        #region "Download"
        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlRepository.Visible = false;
            trvDmSetup.Nodes.Clear();
            trOrderInfo.Nodes.Clear();
            txt_summary.Text = "";
            ClearGridData();
            try
            {
                string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrDmSetupflnm + "/" + clsGeneral.gstrDmSetupflnm + ".xml";
                DownloadPatCriteriaFrmgloCommunity(DownloadPath, "User", clsGeneral.gstrClinicName);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Clinical Repository Click  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlRepository.Visible = true;
            try
            {
                trvDmSetup.Nodes.Clear();
                trOrderInfo.Nodes.Clear();
                txt_summary.Text = "";
                ClearGridData();
                GetGlobalDmSetupList();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Global Repository Click  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private bool DownloadPatCriteriaFrmgloCommunity(string DownloadPath, string IsFrom, string Title)
        {
            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrDmSetupflnm + ".xml";
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
                    DataSet dsDmSetup = ReadDmSetupXml(ServerXmlPath);
                    if (dsDmSetup != null && dsDmSetup.Tables.Count > 0)
                    {
                        //Fill Patient Criteria from gloCommunity

                        DataTable dtPatientCriteriaNm = dsDmSetup.Tables["CriteriaName"];

                        if (dtPatientCriteriaNm != null && dtPatientCriteriaNm.Rows.Count > 0)
                        {
                            //Add Select column for selecting Patient Criteria.
                            ColSelect = new DataColumn("Select", typeof(System.Boolean));
                            dtPatientCriteriaNm.Columns.Add(ColSelect);
                            //End
                            c1PatientCriteria.DataSource = dtPatientCriteriaNm.DefaultView;
                            DesignGrid(c1PatientCriteria);
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

        public bool AssignValToCriteria(string EditName,long CriteriaId,string Action = "Display")
        {
            bool IsAssigned = false;
            DataSet dsDmSetup = null;

            gloStream.DiseaseManagement.Supporting.Criteria oCriteria = null;
            gloStream.DiseaseManagement.DiseaseManagement oDM = null;
            gloStream.DiseaseManagement.Supporting.OtherDetail oOtherDetail = null;
            gloStream.DiseaseManagement.Supporting.OtherDetails oOtherDetails = null;

            pnlSummaryOthers.Visible = true;
            pnlSummaryOthers.BringToFront();

            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrDmSetupflnm + ".xml";

            try
            {
                oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                oDM = new gloStream.DiseaseManagement.DiseaseManagement();
                oOtherDetails = new gloStream.DiseaseManagement.Supporting.OtherDetails();

                //Load DmSetup xml in xmlDocument
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ServerXmlPath);
                //
                //Get Patient Criteria according to Patient Criteria Name
                XmlNode node = null;
                string _DmSetup = "DmSetup"/*RootNode*/, _CriteriaName = "CriteriaName"/*ChildNode*/, _PatientCriteriaName = "PatientCriteriaName";
                node = xmlDocument.SelectSingleNode("/" + _DmSetup + "/" + _CriteriaName + "[@" + _PatientCriteriaName + "='" + EditName + "']");
                //
                //Add into dataset
                dsDmSetup = new DataSet();
                XmlTextReader xtr = new XmlTextReader(node.OuterXml, XmlNodeType.Element, null);
                dsDmSetup.ReadXml(xtr);
                //

                //Assign Criteria to oCriteria & oDM object
                if (dsDmSetup != null && dsDmSetup.Tables.Count > 0)
                {
                    oCriteria.Name = EditName;
                    #region "Fill Demographics"
                    if (dsDmSetup.Tables["Demographics"] != null && dsDmSetup.Tables["Demographics"].Rows.Count > 0)
                    {
                        oCriteria.AgeMinimum = Convert.ToDouble(dsDmSetup.Tables["Demographics"].Rows[0]["AgeMin"]);
                        oCriteria.AgeMaximum = Convert.ToDouble(dsDmSetup.Tables["Demographics"].Rows[0]["AgeMax"]);
                        oCriteria.Gender = dsDmSetup.Tables["Demographics"].Rows[0]["Gender"].ToString();
                        oCriteria.Race = dsDmSetup.Tables["Demographics"].Rows[0]["Race"].ToString();
                        oCriteria.MaritalStatus = dsDmSetup.Tables["Demographics"].Rows[0]["MaritalStatus"].ToString();
                        oCriteria.City = dsDmSetup.Tables["Demographics"].Rows[0]["City"].ToString();
                        oCriteria.State = dsDmSetup.Tables["Demographics"].Rows[0]["State"].ToString();
                        oCriteria.Zip = dsDmSetup.Tables["Demographics"].Rows[0]["Zip"].ToString();
                        oCriteria.EmployeeStatus = dsDmSetup.Tables["Demographics"].Rows[0]["EmplyementStatus"].ToString();
                    }
                    #endregion

                    #region "Fill Vitals"
                    if (dsDmSetup.Tables["Vitals"] != null && dsDmSetup.Tables["Vitals"].Rows.Count > 0)
                    {
                        oCriteria.HeightMinimum = dsDmSetup.Tables["Vitals"].Rows[0]["HeightMin"].ToString();
                        oCriteria.HeightMaximum = dsDmSetup.Tables["Vitals"].Rows[0]["HeightMax"].ToString();
                        oCriteria.WeightMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["WeightMin"]);
                        oCriteria.WeightMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["WeightMax"]);
                        oCriteria.BMIMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BMIMin"]);
                        oCriteria.BMIMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BMIMax"]);
                        oCriteria.TempratureMinumum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["TemperatureMin"]);
                        oCriteria.TempratureMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["TemperatureMax"]);
                        oCriteria.PulseMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["PulseMin"]);
                        oCriteria.PulseMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["PulseMax"]);
                        oCriteria.PulseOXMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["PulseOxMin"]);
                        oCriteria.PulseOXMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["PulseOxMax"]);
                        oCriteria.BPSittingMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BPSittingMin"]);
                        oCriteria.BPSittingMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BPSittingMax"]);
                        oCriteria.BPStandingMinimum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BPStandingMin"]);
                        oCriteria.BPStandingMaximum = Convert.ToDouble(dsDmSetup.Tables["Vitals"].Rows[0]["BPStandingMax"]);
                        oCriteria.DisplayMessage = dsDmSetup.Tables["Vitals"].Rows[0]["DisplayMessage"].ToString();
                    }
                    #endregion

                    #region "Fill History"
                    if (dsDmSetup.Tables["Historyc"] != null && dsDmSetup.Tables["Historyc"].Rows.Count > 0)
                    {
                        for (int cntHistory = 0; cntHistory < dsDmSetup.Tables["Historyc"].Rows.Count; cntHistory++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["Historyc"].Rows[cntHistory]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["Historyc"].Rows[cntHistory]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["Historyc"].Rows[cntHistory]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["Historyc"].Rows[cntHistory]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Drugs"
                    if (dsDmSetup.Tables["Drugsc"] != null && dsDmSetup.Tables["Drugsc"].Rows.Count > 0)
                    {
                        for (int cntDrugs = 0; cntDrugs < dsDmSetup.Tables["Drugsc"].Rows.Count; cntDrugs++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["Drugsc"].Rows[cntDrugs]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["Drugsc"].Rows[cntDrugs]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["Drugsc"].Rows[cntDrugs]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["Drugsc"].Rows[cntDrugs]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill ICD9"
                    if (dsDmSetup.Tables["ICD9c"] != null && dsDmSetup.Tables["ICD9c"].Rows.Count > 0)
                    {
                        for (int cntICD9 = 0; cntICD9 < dsDmSetup.Tables["ICD9c"].Rows.Count; cntICD9++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["ICD9c"].Rows[cntICD9]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["ICD9c"].Rows[cntICD9]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["ICD9c"].Rows[cntICD9]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["ICD9c"].Rows[cntICD9]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill CPT"
                    if (dsDmSetup.Tables["CPTc"] != null && dsDmSetup.Tables["CPTc"].Rows.Count > 0)
                    {
                        for (int cntCPT = 0; cntCPT < dsDmSetup.Tables["CPTc"].Rows.Count; cntCPT++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["CPTc"].Rows[cntCPT]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["CPTc"].Rows[cntCPT]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["CPTc"].Rows[cntCPT]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["CPTc"].Rows[cntCPT]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Lab"
                    if (dsDmSetup.Tables["Labc"] != null && dsDmSetup.Tables["Labc"].Rows.Count > 0)
                    {
                        for (int cntLab = 0; cntLab < dsDmSetup.Tables["Labc"].Rows.Count; cntLab++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["Labc"].Rows[cntLab]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["Labc"].Rows[cntLab]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["Labc"].Rows[cntLab]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["Labc"].Rows[cntLab]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Order"
                    if (dsDmSetup.Tables["Orderc"] != null && dsDmSetup.Tables["Orderc"].Rows.Count > 0)
                    {
                        for (int cntOrder = 0; cntOrder < dsDmSetup.Tables["Orderc"].Rows.Count; cntOrder++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["Orderc"].Rows[cntOrder]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["Orderc"].Rows[cntOrder]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["Orderc"].Rows[cntOrder]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["Orderc"].Rows[cntOrder]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    #region "Fill Problemlist"
                    if (dsDmSetup.Tables["Problemlistc"] != null && dsDmSetup.Tables["Problemlistc"].Rows.Count > 0)
                    {
                        for (int cntProblemlist = 0; cntProblemlist < dsDmSetup.Tables["Problemlistc"].Rows.Count; cntProblemlist++)
                        {
                            oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                            oOtherDetail.ItemName = dsDmSetup.Tables["Problemlistc"].Rows[cntProblemlist]["ItemName"].ToString();
                            oOtherDetail.CategoryName = dsDmSetup.Tables["Problemlistc"].Rows[cntProblemlist]["CategoryName"].ToString();
                            oOtherDetail.OperatorName = dsDmSetup.Tables["Problemlistc"].Rows[cntProblemlist]["Operator"].ToString();
                            oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)Convert.ToInt32(dsDmSetup.Tables["Problemlistc"].Rows[cntProblemlist]["DetailType"]);
                            oOtherDetails.Add(oOtherDetail);
                            oOtherDetail = null;
                        }
                    }
                    #endregion

                    //BIND OTHER DETAILS TO CRITERIA
                    if (oOtherDetails.Count > 0)
                        oCriteria.OtherDetails = oOtherDetails;

                    #region "Orders to be Given"
                    myList objList = default(myList);
                    #region "Fill Orders to be Given - Labs"
                    if (dsDmSetup.Tables["Labsc"] != null && dsDmSetup.Tables["Labsc"].Rows.Count > 0)
                    {
                        for (int cntLabs = 0; cntLabs < dsDmSetup.Tables["Labsc"].Rows.Count; cntLabs++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["Labsc"].Rows[cntLabs]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["Labsc"].Rows[cntLabs]["DMTemplateName"].ToString();
                            oCriteria.LabOrders.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #region "Fill Orders to be Given - Orders"
                    if (dsDmSetup.Tables["Ordersc"] != null && dsDmSetup.Tables["Ordersc"].Rows.Count > 0)
                    {
                        for (int cntOrdersc = 0; cntOrdersc < dsDmSetup.Tables["Ordersc"].Rows.Count; cntOrdersc++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["Ordersc"].Rows[cntOrdersc]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["Ordersc"].Rows[cntOrdersc]["DMTemplateName"].ToString();
                            oCriteria.RadiologyOrders.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #region "Fill Orders to be Given - Referrals"
                    if (dsDmSetup.Tables["Referralsc"] != null && dsDmSetup.Tables["Referralsc"].Rows.Count > 0)
                    {
                        for (int cntReferralsc = 0; cntReferralsc < dsDmSetup.Tables["Referralsc"].Rows.Count; cntReferralsc++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["Referralsc"].Rows[cntReferralsc]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["Referralsc"].Rows[cntReferralsc]["DMTemplateName"].ToString();
                            oCriteria.Referrals.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #region "Fill Orders to be Given - Guidelines"
                    if (dsDmSetup.Tables["Guidelinesc"] != null && dsDmSetup.Tables["Guidelinesc"].Rows.Count > 0)
                    {
                        for (int cntGuidelines = 0; cntGuidelines < dsDmSetup.Tables["Guidelinesc"].Rows.Count; cntGuidelines++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["Guidelinesc"].Rows[cntGuidelines]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["Guidelinesc"].Rows[cntGuidelines]["DMTemplateName"].ToString();
                            oCriteria.Guidelines.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #region "Fill Orders to be Given - Rx"
                    if (dsDmSetup.Tables["Rxc"] != null && dsDmSetup.Tables["Rxc"].Rows.Count > 0)
                    {
                        for (int cntRxc = 0; cntRxc < dsDmSetup.Tables["Rxc"].Rows.Count; cntRxc++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DMTemplateName"].ToString();
                            objList.DrugName = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DrugName"].ToString();
                            objList.Dosage = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["Dosage"].ToString();
                            objList.DrugForm = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DrugForm"].ToString();
                            objList.Route = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["Route"].ToString();
                            objList.Duration = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["Duration"].ToString();
                            objList.Frequency = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["Frequency"].ToString();
                            objList.IsNarcotic = Convert.ToInt16(dsDmSetup.Tables["Rxc"].Rows[cntRxc]["IsNarcotic"]);
                            objList.NDCCode = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["NDCCode"].ToString();
                            objList.DDid = Convert.ToInt64(dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DDid"]);
                            objList.DrugQtyQualifier = dsDmSetup.Tables["Rxc"].Rows[cntRxc]["DrugQtyQualifier"].ToString();
                            oCriteria.RxDrugs.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #region "Fill Orders to be Given - IM"
                    if (dsDmSetup.Tables["IMc"] != null && dsDmSetup.Tables["IMc"].Rows.Count > 0)
                    {
                        for (int cntIMc = 0; cntIMc < dsDmSetup.Tables["IMc"].Rows.Count; cntIMc++)
                        {
                            objList = new myList();
                            objList.ID = -1;
                            objList.Index = -1;
                            objList.Value = dsDmSetup.Tables["IMc"].Rows[cntIMc]["DMTemplateName"].ToString();
                            objList.DMTemplateName = dsDmSetup.Tables["IMc"].Rows[cntIMc]["DMTemplateName"].ToString();
                            objList.DrugName = dsDmSetup.Tables["IMc"].Rows[cntIMc]["DrugName"].ToString();
                            objList.Dosage = dsDmSetup.Tables["IMc"].Rows[cntIMc]["Dosage"].ToString();
                            objList.DrugForm = dsDmSetup.Tables["IMc"].Rows[cntIMc]["DrugForm"].ToString();
                            objList.Route = dsDmSetup.Tables["IMc"].Rows[cntIMc]["Route"].ToString();
                            objList.Duration = dsDmSetup.Tables["IMc"].Rows[cntIMc]["Duration"].ToString();
                            objList.Frequency = dsDmSetup.Tables["IMc"].Rows[cntIMc]["Frequency"].ToString();
                            objList.IsNarcotic = Convert.ToInt16(dsDmSetup.Tables["IMc"].Rows[cntIMc]["IsNarcotic"]);
                            objList.NDCCode = dsDmSetup.Tables["IMc"].Rows[cntIMc]["NDCCode"].ToString();
                            objList.DDid = Convert.ToInt64(dsDmSetup.Tables["IMc"].Rows[cntIMc]["DDid"]);
                            objList.DrugQtyQualifier = dsDmSetup.Tables["IMc"].Rows[cntIMc]["DrugQtyQualifier"].ToString();
                            oCriteria.IMlst.Add(objList);

                            objList = null;
                        }
                    }
                    #endregion

                    #endregion
                }
                //
                if (Action == "Display")
                    IsAssigned = FillSummery(0, oCriteria, oDM);
                else if (Action == "Download")
                {
                    //Save Criteria
                    long _CriteriaId = oDM.SaveCriteria(0, oCriteria);
                    if (_CriteriaId > 0)
                        IsAssigned = true;
                }
                else
                {
                    //Modify Existing Criteria according to gloCommunity Criteria
                    long _CriteriaId = oDM.SaveCriteria(CriteriaId, oCriteria);
                    if (_CriteriaId > 0)
                        IsAssigned = true;
                }
            }
            catch (Exception ex)
            {
                dsDmSetup = null;
                //clsGeneral.UpdateLog("glocomm Error While Assign Val function  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dsDmSetup != null)
                {
                    dsDmSetup.Dispose();
                    dsDmSetup = null;
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

        private DataSet ReadDmSetupXml(string ServerXmlPath)
        {
            DataSet dsDmSetup = null;
            try
            {
                dsDmSetup = new DataSet();
                dsDmSetup.ReadXml(ServerXmlPath);
                return dsDmSetup;
            }
            catch (Exception ex)
            {
                dsDmSetup = null;
                //clsGeneral.UpdateLog("glocomm Error While Read DMSetup XML  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }

            finally
            {
                if (dsDmSetup != null)
                {
                    dsDmSetup.Dispose();
                    dsDmSetup = null;
                }
            }
            return dsDmSetup;
        }

        private void GetGlobalDmSetupList()
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
                                    string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrDmSetupflnm + "/" + clsGeneral.gstrDmSetupflnm + ".xml";
                                    tr.Tag = fileUrl;
                                    tr.ImageIndex = 13;
                                    tr.SelectedImageIndex = 13;
                                    trvDmSetup.Nodes.Add(tr);
                                }
                            }
                        }//End xmlnode.Attributes["Title"]
                    }//End xmlnode.Attributes["BaseType"]
                }//End foreach
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Get Global DMSetup list  in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objclsgloCommunity = null;
                myservice = null;
            }
        }

        private void ClearGridData()
        {
           // c1PatientCriteria.Clear();
            c1PatientCriteria.DataSource = null;

            if (c1PatientCriteria.Rows.Count >= 2)
            {
                c1PatientCriteria.Rows.RemoveRange(1, c1PatientCriteria.Rows.Count - 1);
            }
        }
        #endregion

        private void trvDmSetup_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                txt_summary.Text = "";
                trOrderInfo.Nodes.Clear();
                ClearGridData();
                DownloadPatCriteriaFrmgloCommunity(e.Node.Tag.ToString(), "Global", clsGeneral.gstrClinicName);
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Double Click on Treeview   in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void c1PatientCriteria_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool _CellCheckResult = false;
                var _with1 = c1PatientCriteria;
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

        private void c1PatientCriteria_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string _EditName = null;
            try
            {
                var _with1 = c1PatientCriteria;
                int _RowIndex = _with1.HitTest(e.X, e.Y).Row;
                if (_with1.Rows.Count > 1)
                {
                    if (_RowIndex != 0)
                    {
                        if (strAction == "Upload")
                        {
                            long _ID = 0;
                            _ID = Convert.ToInt64(_with1.GetData(Convert.ToInt32(_with1.Row), "dm_mst_ID"));
                            _EditName = _with1.GetData(Convert.ToInt32(_with1.Row), "dm_mst_CriteriaName").ToString();

                            if (_ID > 0 & !string.IsNullOrEmpty(_EditName.Trim()))
                            {
                                trOrderInfo.Nodes.Clear();
                                AddDefaultNodesToTreeview();
                                gloStream.DiseaseManagement.Supporting.Criteria oCriteria = null;
                                gloStream.DiseaseManagement.DiseaseManagement oDM = null;
                                FillSummery(_ID, oCriteria, oDM);
                            }
                            else
                            {
                                MessageBox.Show("No Record Exist.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }//end Upload

                        else//Download
                        {
                            trOrderInfo.Nodes.Clear();
                            AddDefaultNodesToTreeview();
                            _EditName = _with1.GetData(Convert.ToInt32(_with1.Row), "PatientCriteriaName").ToString();
                            bool _IsAssigned = AssignValToCriteria(_EditName, 0);

                        }//end Download
                    }//_RowIndex
                }
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Double Click on Flexgrid   in  Usercontrol DMSetUP: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void trvDmSetup_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                txt_summary.Text = "";
                trOrderInfo.Nodes.Clear();
                ClearGridData();
                DownloadPatCriteriaFrmgloCommunity(e.Node.Tag.ToString(), "Global", clsGeneral.gstrClinicName);
            }
            catch (Exception ex)
            {
                //commented by kanchan on 20120104
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("glocomm Error While Double Click on Treeview   in  Usercontrol DMSetUP: " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
    }
}
