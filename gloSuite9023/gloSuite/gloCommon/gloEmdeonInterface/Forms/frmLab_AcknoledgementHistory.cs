using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloTaskMail;


/// <summary>
/// Added by madan on 20100507
/// this form used for viewing acknowledgement history for selected order....
/// </summary>

namespace gloEmdeonInterface.Forms
{
    public partial class frmLab_AcknoledgementHistory : Form
    {
        #region " Variables "

        #region " c1History Constants "

        private const int COL_SRNO = 0;
        private const int COL_USERID = 1;
        private const int COL_USERNAME = 2;
        private const int COL_REVIEWDATETIME = 3;
        private const int COL_COMMENTS = 4;
        //Developer:Sanjog Dhamke
        //Date: 22 Dec 2011 (6060)
        //PRD Name: Lab Usability - To show Patient Note also in Grid
        //Reason: We add another column for Patient Note.
        private const int COL_PATIENTNOTE = 5;
        private const int COL_AKWSRNO = 6;
        private const int COL_COUNT = 7;

        #endregion " c1History Constants "

        private Int64 OrderId = 0;
        private string ConnectionString = string.Empty;
        public bool IsReOpened = false;

        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: We are open acknw. window from History window.
         
        public Int64 _PatientProviderID;
        public Int64 _LoginProviderId;
        public Int64 _patientID;
        public Boolean _CloseWindow = false;

        #endregion " Variables "

        #region " Constructor "

        public frmLab_AcknoledgementHistory(Int64 _OrderId,string _ConnectionString)
        {
            // Set varibles passed in to form.
            OrderId = _OrderId;           
            ConnectionString = _ConnectionString;

            InitializeComponent();
        }

        #endregion " Constructor "



        #region " Form Load "

        private void frm_LabAckHistory_Load(object sender, EventArgs e)
        {
           
            gloC1FlexStyle.Style(c1History, false);

            try
            {
                
                if (OrderId > 0)
                {
                    LoadHistory(OrderId);
                }
                //Developer:Sanjog Dhamke
                //Date: 24 Jan 2012
                //PRD Name: lab Usability
                //Reason:Add this code to show direct add mode screen if no ackw is present against order
                if (c1History.Rows.Count == 1)
                {
                    _CloseWindow = true;
                    tlb_Add_Click(null, null);
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }


        #endregion

        #region " Button Click Events "

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tlb_OpenOrder_Click(object sender, EventArgs e)
        {
            if (OrderId > 0)
            {
                if (ConvertToOpenOrder(OrderId))
                {
                    IsReOpened = true;
                    this.Dispose();
                }              
            }
        }
        #endregion" Button Click Events "

        #region " Private & Public Methods "

        private DataTable getAcknoledgement(Int64 OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            string strQuery = string.Empty;
            DataTable dtResult = new DataTable();

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nUserID,ReviewDatetime,Comments,nAcknowledgeSrNo,ISNULL(PatientNote,'') AS PatientNote FROM Lab_Acknowledgment Where nOrderId =" + OrderId + "  Order By ReviewDatetime DESC";

                oDB.Retrive_Query(strQuery, out dtResult);

                if (dtResult != null)
                {
                    return dtResult;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQuery = string.Empty;
            }

        }
        public void LoadHistory(Int64 OrderId)
        {

            DataTable dtAcknowledge = new DataTable();
            try
            {
                if (OrderId > 0)
                {
                    dtAcknowledge = getAcknoledgement(OrderId);
                }
                if (dtAcknowledge != null)
                {
                    DesignGrid();

                    int rowIndex = 0;
                    Int64 _UserID = 0;
                    string _UserName = string.Empty;

                    for (int i = 0; i <= dtAcknowledge.Rows.Count - 1; i++)
                    {
                        rowIndex = c1History.Rows.Count;
                        c1History.Rows.Add();
                        c1History.SetData(rowIndex, COL_SRNO, rowIndex.ToString());
                        c1History.SetData(rowIndex, COL_AKWSRNO, dtAcknowledge.Rows[i]["nAcknowledgeSrNo"]);
                        //Developer:Sanjog Dhamke
                        //Date: 22 Dec 2011 (6060)
                        //PRD Name: Lab Usability - To show Patient Note also in Grid
                        //Reason: We add another column for Patient Note.

                        #region DesignStyles for Fixed Bug Id 64778 on 20140227
                        ////Set style for C1grid For wraping text in the row.
                        C1.Win.C1FlexGrid.CellStyle c1StylePatnotes;
                       // c1StylePatnotes = c1History.Styles.Add("c1StylePatnotes");
                        try
                        {
                            if (c1History.Styles.Contains("c1StylePatnotes"))
                            {
                                c1StylePatnotes = c1History.Styles["c1StylePatnotes"];
                            }
                            else
                            {
                                c1StylePatnotes = c1History.Styles.Add("c1StylePatnotes");

                            }

                        }
                        catch
                        {
                            c1StylePatnotes = c1History.Styles.Add("c1StylePatnotes");

                        }
                        c1StylePatnotes.WordWrap = true;
                        #endregion

                        c1History.SetCellStyle(rowIndex, COL_PATIENTNOTE, c1StylePatnotes);
                        c1History.SetData(rowIndex, COL_PATIENTNOTE, dtAcknowledge.Rows[i]["PatientNote"].ToString().Replace("\n", "").ToString());
                        _UserID = Convert.ToInt64(dtAcknowledge.Rows[i]["nUserID"]);
                        if (_UserID > 0)
                        {
                            _UserName = GetUserName(_UserID);
                            c1History.SetData(rowIndex, COL_USERID, _UserID);
                            c1History.SetData(rowIndex, COL_USERNAME, _UserName);
                        }
                        c1History.SetData(rowIndex, COL_REVIEWDATETIME, dtAcknowledge.Rows[i]["ReviewDatetime"].ToString());

                        #region Wraping text for Fixed Bug Id 64778 on 20140227
                        using (Graphics g = c1History.CreateGraphics())
                        {

                            // measure text height
                            //StringFormat sf = new StringFormat();
                            int wid = c1History.Cols[COL_PATIENTNOTE].WidthDisplay - 2;
                            string text = Convert.ToString(c1History.GetData(rowIndex, COL_PATIENTNOTE));
                            SizeF sz = g.MeasureString(text, c1History.Font, wid, StringFormat.GenericDefault);

                            // adjust row height if necessary
                            C1.Win.C1FlexGrid.Row row = c1History.Rows[rowIndex];
                            if (sz.Height + 4 > row.HeightDisplay)
                            { row.HeightDisplay = (int)sz.Height + 4; }
                        }
                        #endregion

                        #region DesignStyles.
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
                        #endregion

                        c1History.SetCellStyle(rowIndex, COL_COMMENTS, c1Style);
                        c1History.SetData(rowIndex, COL_COMMENTS, dtAcknowledge.Rows[i]["Comments"].ToString().Replace("\n", "").ToString());

                        using (Graphics g = c1History.CreateGraphics())
                        {

                            // measure text height
                            //StringFormat sf = new StringFormat();
                            int wid = c1History.Cols[COL_COMMENTS].WidthDisplay - 2;
                            string text = Convert.ToString(c1History.GetData(rowIndex, COL_COMMENTS));
                            SizeF sz = g.MeasureString(text, c1History.Font, wid, StringFormat.GenericDefault);

                            // adjust row height if necessary
                            C1.Win.C1FlexGrid.Row row = c1History.Rows[rowIndex];
                            if (sz.Height + 4 > row.HeightDisplay)
                            { row.HeightDisplay = (int)sz.Height + 4; }
                        }



                    }
                    c1History.Refresh();
                    
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        public void DesignGrid()
        {
            try
            {
                c1History.Rows.Count = 1;
                c1History.Rows.Fixed = 1;
                c1History.Cols.Count = COL_COUNT;
                c1History.Cols.Fixed = 0;

                int _Width = (c1History.Width) / 10;
                c1History.Cols[COL_SRNO].Width = _Width;
                c1History.Cols[COL_USERID].Width = 0;
                c1History.Cols[COL_USERNAME].Width = _Width * 2;
                c1History.Cols[COL_REVIEWDATETIME].Width = _Width * 2;
                c1History.Cols[COL_COMMENTS].Width = _Width * 3;
                c1History.Cols[COL_AKWSRNO].Width = 0;
                //Developer:Sanjog Dhamke
                //Date: 22 Dec 2011 (6060)
                //PRD Name: Lab Usability - To show Patient Note also in Grid
                //Reason: We add another column for Patient Note.
                c1History.Cols[COL_PATIENTNOTE].Width = _Width * 2 ;

                c1History.Cols[COL_SRNO].Visible = true;
                c1History.Cols[COL_USERID].Visible = false;
                c1History.Cols[COL_USERNAME].Visible = true;
                c1History.Cols[COL_REVIEWDATETIME].Visible = true;
                c1History.Cols[COL_COMMENTS].Visible = true;
                c1History.Cols[COL_AKWSRNO].Visible = true;
                c1History.Cols[COL_PATIENTNOTE].Visible = true;

                c1History.SetData(0, COL_SRNO, "No");
                c1History.SetData(0, COL_USERID, "Userid");
                c1History.SetData(0, COL_USERNAME, "User Name");
                c1History.SetData(0, COL_REVIEWDATETIME, "Review Date");
                c1History.SetData(0, COL_COMMENTS, "Comments");
                c1History.SetData(0, COL_PATIENTNOTE, "Patient Notes");
                c1History.SetData(0, COL_AKWSRNO, "AkwSrNo");

                c1History.AutoSizeRows();
                for (int i = 0; i <= c1History.Cols.Count - 1; i++)
                {
                    c1History.Cols[i].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        //Method added for retrieving User Name according to UserID
        private string GetUserName(Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            object _intresult = new object();
            string _result = "";
            string _strSQL = "";
            try
            {
                _result = "";
                oDB.Connect(false);

               // _strSQL = "SELECT (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') +  space(1) + ISNULL(sLastName,'')) AS UserName " +
                //" FROM User_MST WHERE nUserID =" + UserID;

                _strSQL = "Select sLoginName FROM User_MST WHERE nUserID =" + UserID;

                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = _intresult.ToString();
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        private bool ConvertToOpenOrder(Int64 _OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            string strQuery = string.Empty;
            int _Result = 0;
            Boolean _boolResult = false;
            try
            {
                oDB.Connect(false);

                //09-Jul-14 Aniket: Set the OrderStatus to 'Ready for Results Review' for unacknowledged Orders
                strQuery = "Update dbo.Lab_Order_MST set bIsClosed='False', OrderStatusNumber=1004 where labom_OrderID=" + _OrderID;
                _Result = oDB.Execute_Query(strQuery);
                if (_Result < 0)
                    _boolResult = false;
                else if (_Result >= 0)
                    _boolResult = true;
                else
                    _boolResult = false;
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _boolResult = false;
            }
            finally
            {
                if (oDB!=null)
                {
                    oDB.Dispose();
                }
                strQuery = string.Empty;
                _Result = 0;
            }
            return _boolResult;
        }


        #endregion" Private & Public Methods "

        //Developer:Sanjog Dhamke
        //Date: 20 Dec 2011 (6060)
        //Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        //Reason: We are open acknw. window from History window.
         
        private void tlb_Add_Click(object sender, EventArgs e)
        {
            try
            {
                frmLab_Acknoledgement ofrm = new frmLab_Acknoledgement(OrderId, true, ConnectionString,0);
                ofrm._LoginProviderId = _LoginProviderId;
                //Developer:Sanjog Dhamke
                //Date:21 Dec 2011
                //PRD Name: To Open task window in lab Ackw.
                //Reason: To pass the patient Id to task window
                ofrm.PatientID = _patientID;
                ofrm._PatientProviderID = _PatientProviderID;
                ofrm.StartPosition = FormStartPosition.CenterScreen;
                ofrm.ShowDialog(this);
                ofrm.Dispose();
                ofrm = null;
                //Developer:Sanjog Dhamke
                //Date:2 March 2012
                //PRD Name: MWC 7.0 Feedback on Labs
                //Reason: In Add Mode don't need to show the Ack. History form after saving the acknowledgement, So we close this form.
                if (_CloseWindow == true)
                    this.Close();
                else
                    LoadHistory(OrderId);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void tlb_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1History.RowSel > 0)
                {
                    frmLab_Acknoledgement ofrm = new frmLab_Acknoledgement(OrderId, true, ConnectionString,Convert.ToInt64( c1History.GetData(c1History.RowSel,COL_AKWSRNO)));
                    ofrm._LoginProviderId = _LoginProviderId;
                    //Developer:Sanjog Dhamke
                    //Date:21 Dec 2011
                    //PRD Name: To Open task window in lab Ackw.
                    //Reason: To pass the patient Id to task window
                    ofrm.PatientID = _patientID;
                    ofrm._PatientProviderID = _PatientProviderID;
                    ofrm.StartPosition = FormStartPosition.CenterScreen;
                    ofrm.ShowDialog(this);
                    ofrm.Dispose();
                    ofrm = null;
                    LoadHistory(OrderId);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void tlb_Delete_Click(object sender, EventArgs e)
        {

            try
            {
                if (c1History.RowSel > 0)
                {
                    gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oReviewed = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                    oReviewed.Delete_Acknowledgement(Convert.ToInt64(c1History.GetData(c1History.RowSel, COL_AKWSRNO)),OrderId);
                    oReviewed.Dispose();
                    oReviewed = null;
                    LoadHistory(OrderId);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void c1History_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1History.Rows.Count > 0)
                {
                    tlb_Modify_Click(null, null);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void frmLab_AcknoledgementHistory_Shown(object sender, EventArgs e)
        {
            if (OrderId > 0)
            {
                if ((c1History.Rows.Count > 1) && (GetOrderStatusISAcknowledged(OrderId) == false))
                {
                    //_CloseWindow = true; // will close both forms
                    tlb_Add_Click(null, null);
                }
            }
        }

        public bool GetOrderStatusISAcknowledged(Int64 nOrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            string strQry = string.Empty;
            bool IsAck = false;

            try
            {
                if (nOrderID != 0)
                {
                    oDB.Connect(false);
                    strQry = "SELECT ISNULL(bIsClosed,0) FROM Lab_Order_MST where labom_OrderID='" + nOrderID + "' ";
                    IsAck = Convert.ToBoolean(oDB.ExecuteScalar_Query(strQry));
                    oDB.Disconnect();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                IsAck = false;
            }
            finally
            {
                oDB.Dispose();
            }
            return IsAck;
        }
    }
}
