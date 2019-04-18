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
    public partial class frmSBPHistory : Form
    {
        public frmSBPHistory()
        {
            InitializeComponent();
        }



        //long _PatientID;



        int Col_COUNT = 5;

        int Col_Patid = 0;
        int Col_TransactionDate = 1;
        int Col_AuditId = 2;
        int Col_Description = 3;
        int Col_Loginname = 4;

        //string AppConnectionString = "";

        DataTable dtPatSBPhx = null;




        private void frmSBPHistory_Load(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(frmSBPHxData.AppConnectionString);
                oDB.Connect(false);

                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nPatientId", frmSBPHxData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("SBP_PatientTransactionHistory", oDBParameters, out dtPatSBPhx);


                oDB.Disconnect();
                SetFlexgridColumns();
                Binddata(dtPatSBPhx);
            }
            catch (Exception ex)
            {
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (dtPatSBPhx != null)
                {
                    dtPatSBPhx.Dispose();
                    dtPatSBPhx = null;
                }
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.ViewedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Open, "Viewed Social Psychological Behavioral observations form", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }
        }

        private void Binddata(DataTable PatientSBPAuditHx)
        {
            try
            {
                if (!(PatientSBPAuditHx == null))
                {
                    if ((PatientSBPAuditHx.Rows.Count > 0))
                    {
                        c1SBPHistory.Rows.Count = 1;
                        for (int i = 0; (i <= (PatientSBPAuditHx.Rows.Count - 1)); i++)
                        {
                            c1SBPHistory.Rows.Add();
                            // add new row
                            c1SBPHistory.SetData((i + 1), Col_Patid, PatientSBPAuditHx.Rows[i]["nPatientId"]);
                            c1SBPHistory.SetData((i + 1), Col_TransactionDate, PatientSBPAuditHx.Rows[i]["dtTransactionDate"]);
                            c1SBPHistory.SetData((i + 1), Col_AuditId, PatientSBPAuditHx.Rows[i]["nAuditId"]);
                            c1SBPHistory.SetData((i + 1), Col_Description, PatientSBPAuditHx.Rows[i]["sDescription"]);
                            c1SBPHistory.SetData((i + 1), Col_Loginname, PatientSBPAuditHx.Rows[i]["sLoginName"]);


                        }

                    }
                    else
                    {
                        c1SBPHistory.Rows.Count = 1;
                    }

                }
                else
                {
                    c1SBPHistory.Rows.Count = 1;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                dtPatSBPhx = null;
            }
            finally
            {
                dtPatSBPhx = null;
            }
        }


        private void SetFlexgridColumns()
        {
           //.AllowDrop = True
            c1SBPHistory.AllowDrop = true;
            // .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            c1SBPHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

           // .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
           // c1SBPHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

      

           c1SBPHistory.ExtendLastCol = true;
           c1SBPHistory.Rows.Count = 1;
           c1SBPHistory.Rows.Fixed = 1;
           c1SBPHistory.Cols.Count = Col_COUNT;
           c1SBPHistory.Cols.Fixed = 0;

           //.Rows(.Rows.Count - 1).Height = 21
           c1SBPHistory.Rows[c1SBPHistory.Rows.Count - 1].Height = 21;

            //int _Width = c1SBPHistory.Width / 10;
            int _width = c1SBPHistory.Width - 2;
          
           c1SBPHistory.Cols[Col_Patid].Width = 0;
           c1SBPHistory.Cols[Col_TransactionDate].Width = Convert.ToInt32(_width * 0.16); 
           c1SBPHistory.Cols[Col_AuditId].Width = 0;
           c1SBPHistory.Cols[Col_Description].Width = Convert.ToInt32(_width * 0.5);
           c1SBPHistory.Cols[Col_Loginname].Width = Convert.ToInt32(_width * 0.2);
     
           
            //set column header
            c1SBPHistory.SetData(0, Col_Patid, "Patient ID");
            c1SBPHistory.SetData(0, Col_TransactionDate, "Transaction Date");
            c1SBPHistory.SetData(0, Col_AuditId, "AuditId");
            c1SBPHistory.SetData(0, Col_Description, "Description");
            c1SBPHistory.SetData(0, Col_Loginname, "Login User");
         
          
            //set visiblity for column 
            c1SBPHistory.Cols[Col_Patid].Visible = true;
            c1SBPHistory.Cols[Col_TransactionDate].Visible = true;
            c1SBPHistory.Cols[Col_AuditId].Visible = true;
            c1SBPHistory.Cols[Col_Description].Visible = true;
            c1SBPHistory.Cols[Col_Loginname].Visible = true;

          
           
           //set column editing properties.
            c1SBPHistory.Cols[Col_Patid].AllowEditing = false;
            c1SBPHistory.Cols[Col_TransactionDate].AllowEditing = false;
            c1SBPHistory.Cols[Col_AuditId].AllowEditing = false;
            c1SBPHistory.Cols[Col_Description].AllowEditing = false;
            c1SBPHistory.Cols[Col_Loginname].AllowEditing = false;

            

            c1SBPHistory.ForeColor = Color.Black;

        }

        private void btn_tls_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void c1SBPHistory_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (c1SBPHistory.Rows.Count > 1)
                {
                    DateTime dtTransactiondate ;
                    long nPatientId = 0;
                    int nSelectedRow = c1SBPHistory.RowSel;

                    dtTransactiondate = Convert.ToDateTime(c1SBPHistory.GetData(nSelectedRow, Col_TransactionDate));
                    nPatientId = Convert.ToInt64(c1SBPHistory.GetData(nSelectedRow, Col_Patid));

                    frmSBPAuditHxData ofrmAuditHxData = new frmSBPAuditHxData();
                    SBPAuditHxData.AppConnectionString = frmSBPHxData.AppConnectionString;
                    SBPAuditHxData.PatientId = nPatientId;
                    SBPAuditHxData.TransactionDate = dtTransactiondate;
                    ofrmAuditHxData.ShowDialog();
                    ofrmAuditHxData.Dispose();
                    ofrmAuditHxData = null;
                    //for (int i = 0; i <= c1SBPHistory.Rows.Count - 1; i++)
                    //{
                    //    dtTransactiondate = Convert.ToDateTime( c1SBPHistory.GetData(i, Col_TransactionDate));
                    //    nAuditId = Convert.ToInt64(c1SBPHistory.GetData(i, Col_AuditId));
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               
            }
            finally
            {

            }
        }

        private void c1SBPHistory_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTip(C1SuperTooltip1, c1SBPHistory, e.Location);
        }

        public void ShowToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
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
                        sText = c1SBPHistory.GetData(nRow, nCol).ToString();
                        // End If
                        //  If (nCol < oGrid.Cols.Count) Then
                        colsize = c1SBPHistory.Cols[nCol].WidthDisplay;
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

    public static class frmSBPHxData
    {
        public static string AppConnectionString { get; set; }
        public static long nPatientId { get; set; }
        
    }

}
