using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloGlobal.SBPQuestionnaire
{
    public partial class frmSBPAuditHxData : Form
    {
        public frmSBPAuditHxData()
        {
            InitializeComponent();
        }



        //long _PatientID;



        int Col_COUNT = 8;

        int Col_QuestionLoincCode = 0;
        int Col_DomainName = 1;
        int Col_sQuestion = 2;
        int Col_OldDisplayText = 3;//before recorded answer
        int Col_OldRecordedAnsCode = 4;//before recorded answer code
        int Col_InputType = 5;
        int Col_DisplayText = 6;//After recorded answer
        int Col_RecordAnsCode = 7;//after recorded answer code
       

        DataTable dtPatSBPhxdata = null;

        private void frmSBPAuditHxData_Load(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(SBPAuditHxData.AppConnectionString);
                oDB.Connect(false);

                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nPatientId", SBPAuditHxData.PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtTransactionDate", SBPAuditHxData.TransactionDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Retrive("SBP_PatientData", oDBParameters, out dtPatSBPhxdata);
                oDBParameters.Dispose();
                
                oDB.Disconnect();
                SetFlexgridColumns();
                Binddata(dtPatSBPhxdata);
            }
            catch (Exception ex)
            {
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (dtPatSBPhxdata != null)
                {
                    dtPatSBPhxdata.Dispose();
                    dtPatSBPhxdata = null;
                }
            }
            finally
            {
             
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.ViewedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Open, "Viewed Social Psychological Behavioral observations form", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
        }


        private void SetFlexgridColumns()
        {
            //.AllowDrop = True
            c1SBPAuditHxData.AllowDrop = true;
            // .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            c1SBPAuditHxData.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

            // .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            //c1SBPAuditHxData.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

           
            c1SBPAuditHxData.ExtendLastCol = true;
            c1SBPAuditHxData.Rows.Count = 1;
            c1SBPAuditHxData.Rows.Fixed = 1;
            c1SBPAuditHxData.Cols.Count = Col_COUNT;
            c1SBPAuditHxData.Cols.Fixed = 0;

            //.Rows(.Rows.Count - 1).Height = 21
            c1SBPAuditHxData.Rows[c1SBPAuditHxData.Rows.Count - 1].Height = 21;

            int _width = c1SBPAuditHxData.Width - 2;

            c1SBPAuditHxData.Cols[Col_QuestionLoincCode].Width = 0;
            c1SBPAuditHxData.Cols[Col_DomainName].Width = Convert.ToInt32(_width * 0.20);
            c1SBPAuditHxData.Cols[Col_sQuestion].Width = Convert.ToInt32(_width * 0.35);
            c1SBPAuditHxData.Cols[Col_OldDisplayText].Width = Convert.ToInt32(_width * 0.15);//before recorded answer
            c1SBPAuditHxData.Cols[Col_OldRecordedAnsCode].Width = 0;//before recorded answer code
            c1SBPAuditHxData.Cols[Col_InputType].Width = 0;
            c1SBPAuditHxData.Cols[Col_DisplayText].Width = Convert.ToInt32(_width * 0.10);//After recorded answer
            c1SBPAuditHxData.Cols[Col_RecordAnsCode].Width = 0;//after recorded answer code

       
         
          


            //set column header
            c1SBPAuditHxData.SetData(0, Col_QuestionLoincCode, "Que. Loinc Code");
            c1SBPAuditHxData.SetData(0, Col_DomainName, "Domain Name");
            c1SBPAuditHxData.SetData(0, Col_sQuestion, "Question");
            c1SBPAuditHxData.SetData(0, Col_OldDisplayText, "Before Rec. Ans");//before recorded answer
            c1SBPAuditHxData.SetData(0, Col_OldRecordedAnsCode, "Before Ans. Code");//before recorded answer code
            c1SBPAuditHxData.SetData(0, Col_InputType, "InputType");
            c1SBPAuditHxData.SetData(0, Col_DisplayText, "After Rec. Ans.");//After recorded answer
            c1SBPAuditHxData.SetData(0, Col_RecordAnsCode, "After Ans. Code");//after recorded answer code
       

            


            //set visiblity for column 
            c1SBPAuditHxData.Cols[Col_QuestionLoincCode].Visible = true;
            c1SBPAuditHxData.Cols[Col_sQuestion].Visible = true;
            c1SBPAuditHxData.Cols[Col_DisplayText].Visible = true;
            c1SBPAuditHxData.Cols[Col_RecordAnsCode].Visible = true;
            c1SBPAuditHxData.Cols[Col_InputType].Visible = true;
            c1SBPAuditHxData.Cols[Col_DomainName].Visible = true;
            c1SBPAuditHxData.Cols[Col_OldDisplayText].Visible = true;
            c1SBPAuditHxData.Cols[Col_OldRecordedAnsCode].Visible = true;

            //set column editing properties.
            c1SBPAuditHxData.Cols[Col_QuestionLoincCode].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_sQuestion].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_DisplayText].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_RecordAnsCode].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_InputType].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_DomainName].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_OldDisplayText].AllowEditing = false;
            c1SBPAuditHxData.Cols[Col_OldRecordedAnsCode].AllowEditing = false;


            c1SBPAuditHxData.ForeColor = Color.Black;
        }

        private void Binddata(DataTable PatientSBPAuditHxdata)
        {
            try
            {
                if (!(PatientSBPAuditHxdata == null))
                {
                    if ((PatientSBPAuditHxdata.Rows.Count > 0))
                    {
                        c1SBPAuditHxData.Rows.Count = 1;
                        for (int i = 0; i <= PatientSBPAuditHxdata.Rows.Count - 1; i++)
                        {
                            c1SBPAuditHxData.Rows.Add();
                            // add new row
                            c1SBPAuditHxData.SetData((i + 1), Col_QuestionLoincCode, PatientSBPAuditHxdata.Rows[i]["sQuestionLoincCode"]);
                            c1SBPAuditHxData.SetData((i + 1), Col_DomainName, PatientSBPAuditHxdata.Rows[i]["sDomainName"]);
                            //c1SBPAuditHxData.Cols[Col_sQuestion].Style.WordWrap = true;
                            //c1SBPAuditHxData.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows;  
                            c1SBPAuditHxData.SetData((i + 1), Col_sQuestion, PatientSBPAuditHxdata.Rows[i]["sQuestion"]);
                            c1SBPAuditHxData.SetData((i + 1), Col_DisplayText, PatientSBPAuditHxdata.Rows[i]["DisplayText"]);//After recorded answer
                            c1SBPAuditHxData.SetData((i + 1), Col_RecordAnsCode, PatientSBPAuditHxdata.Rows[i]["RecordAns"]);//after recorded answer code
                            c1SBPAuditHxData.SetData((i + 1), Col_InputType, PatientSBPAuditHxdata.Rows[i]["sInputType"]);
                            c1SBPAuditHxData.SetData((i + 1), Col_OldDisplayText, PatientSBPAuditHxdata.Rows[i]["OldDisplayText"]);//before recorded answer
                            c1SBPAuditHxData.SetData((i + 1), Col_OldRecordedAnsCode, PatientSBPAuditHxdata.Rows[i]["OldRecordedAns"]);//before recorded answer code
                            
                        }

                    }
                    else
                    {
                        c1SBPAuditHxData.Rows.Count = 1;
                    }

                }
                else
                {
                    c1SBPAuditHxData.Rows.Count = 1;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                if (dtPatSBPhxdata != null)
                {
                    dtPatSBPhxdata.Dispose();
                    dtPatSBPhxdata = null;
                }
            }
            finally
            {
                dtPatSBPhxdata = null;
            }
        }
        private void btn_tls_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void c1SBPAuditHxData_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTip(C1SuperTooltip1, c1SBPAuditHxData, e.Location);
        }

        public  void ShowToolTip( C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                Font myFont = oGrid.Font;
                int nRow = oGrid.MouseRow;
                int nCol = oGrid.MouseCol;
                if (((nCol > -1)
                            && (nRow > 0)))
                {
                    if (((nRow < oGrid.Rows.Count)
                                && (nCol < oGrid.Cols.Count)))
                    {
                        // And nCol > 0 Then
                        //  If Not oGrid.GetData(nRow, nCol) Is Nothing Then
                        // sText = oGrid.GetData(nRow, nCol).ToString()
                        // TO RESOLVED 8821
                        int colsize = 0;
                        string sText = "";
                        sText = c1SBPAuditHxData.GetData(nRow, nCol).ToString();
                        // End If
                        //  If (nCol < oGrid.Cols.Count) Then
                        colsize = c1SBPAuditHxData.Cols[nCol].WidthDisplay;
                        // End If
                        if ((string.IsNullOrEmpty(sText) == false))
                        {
                            Graphics oGrp = oGrid.CreateGraphics();
                            int chars;
                            int lines;
                            SizeF stringsize = oGrp.MeasureString(sText.ToString(), myFont, SizeF.Empty, System.Drawing.StringFormat.GenericDefault, out chars, out lines);
                            // 'Code Review Changes: Dispose Graphics object
                            oGrp.Dispose();
                            // ' oGrid.GetCellRect(nRow, nCol).Height
                            // If stringsize.Width > colsize Or lines > 1 And oGrid.GetCellRect(nRow, nCol).Height < (19 * lines) Then
                            if (((stringsize.Width > colsize) || (lines > 1)))
                            {
                                // oC1ToolTip.SetToolTip(oGrid, sText.ToString())
                                // 'TO RESOLVED 8821
                                oC1ToolTip.Font = myFont;
                                oC1ToolTip.MaximumWidth = 400;
                                oC1ToolTip.SetToolTip(oGrid, sText);
                            }
                            else
                            {
                                oC1ToolTip.SetToolTip(oGrid, "");
                            }

                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }

                    }
                    else
                    {
                        oC1ToolTip.SetToolTip(oGrid, "");
                    }

                }
                else
                {
                    oC1ToolTip.SetToolTip(oGrid, "");
                }

            }
            catch (Exception)
            {
            }

        }
        
    }



    public static class SBPAuditHxData
    {
        public static string AppConnectionString { get; set; }
        public static long PatientId { get; set; }
        public static DateTime TransactionDate { get; set; }

    }
}
