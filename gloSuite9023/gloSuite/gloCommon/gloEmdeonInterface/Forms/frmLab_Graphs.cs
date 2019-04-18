using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace gloEmdeonInterface.Forms
{
    /// <summary>
    /// Form is not in use right now, this is expected to be convert 
    /// migration functionality of graphs on controls for Test(Results).
    /// </summary>
	public partial class frmLab_Graphs
	{
		
		private SqlConnection Conn;
		//private DataView Dv;--- commented by madan to remove warnings... on 20100520
		
		bool _flg;

        //object _bFlg;--- commented by madan to remove warnings... on 20100520
		object _fromDt;
		object _ToDt;
		object _strTest;
		object _strResults;
        String gstrMessageBoxCaption = "";
        Int64  gnPatientID=0;

        // added by Abhijeet on date 20100417  
        //Below code is commentd by madan on 20100520
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
		
		public frmLab_Graphs(bool bFlg, DateTime fromDt, DateTime ToDt, string strTest, string strResults)
		{
			
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			
			// Add any initialization after the InitializeComponent() call.
			
			string sqlconn;
            sqlconn = "";// GetConnectionString;
			Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
			//Type = ContactType
			_flg = bFlg;
			_fromDt = fromDt;
			_ToDt = ToDt;
			_strTest = strTest;
			_strResults = strResults;

            // Code by : Abhijeet Farkande on date : 20100417, 20100514
            // changes : accessing the message box caption from appSetting               
            if (appSettings != null)
            {                               
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { gstrMessageBoxCaption = "gloEMR"; }
            }
            else
            {               
                gstrMessageBoxCaption = "gloEMR";
            }
            // End of code for accessing the user Name                
			
		}
		
		public DataTable FillControlsTest()
		{
		    System.Data.SqlClient.SqlCommand Cmd=null;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter();
                DataTable dt = new DataTable();

                Cmd = new SqlCommand("gsp_Lab_Tests", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objParamPatientId;
                objParamPatientId = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt);
                objParamPatientId.Direction = ParameterDirection.Input;
                objParamPatientId.Value = gnPatientID;

                adpt.SelectCommand = Cmd;
                adpt.Fill(dt);
                adpt.Dispose();
                adpt = null;
                return dt;
            }
            catch (Exception ex)
            {
                Conn.Close();
                //     gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                //    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
                throw (ex);
                //return null;-- commeted this for fixing warnings... by madan 20100520
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
            }
		}
		
		public DataTable FillControlsResult()
		{
            SqlCommand Cmd = null;
            SqlDataAdapter adpt = new SqlDataAdapter();
			DataTable dt = new DataTable();

            if (Convert.ToInt64(cmbTests.SelectedValue) > 0)
			{
				Cmd = new SqlCommand("gsp_Lab_Results", Conn);
				Cmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter objParamPatientId;
				objParamPatientId = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt);
				objParamPatientId.Direction = ParameterDirection.Input;
				objParamPatientId.Value = gnPatientID;
				
				objParamPatientId = Cmd.Parameters.Add("@nTest", SqlDbType.BigInt);
				objParamPatientId.Direction = ParameterDirection.Input;
				objParamPatientId.Value = cmbTests.SelectedValue ; //.ValueMember
				
				adpt.SelectCommand = Cmd;
				adpt.Fill(dt);
                adpt.Dispose();
                adpt = null;
                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
			}
            return dt;
		}
		
		public void cmbTests_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{


                if (Convert.ToInt64(cmbTests.SelectedValue) > 0)
				{
					
					DataTable dt;
                    //int i = 0;-- commented this for fixing the warnings.. by madan on 20100520
					dt = FillControlsResult();
					
					if ((dt == null) == false)
					{
						cmbResults.DataSource = dt;
						cmbResults.DisplayMember = dt.Columns[0].ColumnName;
						cmbResults.ValueMember = dt.Columns[1].ColumnName;
					}
				}
			}
			catch (Exception ex)
			{
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
				//gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.gloAuditTrail.ExceptionLog, gloAuditTrail.gloAuditTrail.ActivityCategory , gloAuditTrail.gloAuditTrail.ExceptionLog, ex.ToString(), gloAuditTrail.gloAuditTrail.ExceptionLog);
				
			}
		}
		
		public void btnShowGraphs_Click(System.Object sender, System.EventArgs e)
		{
            DataTable dt_OnlyMinMax = null;
            try
            {




                if (IsDate(dtFrom.Text))
                {
                    if (IsDate(dtTo.Text))
                    {

                        if (DateTime.Compare(dtFrom.Value, dtTo.Value) > 0)
                        {
                            MessageBox.Show("From-date should be less than To-Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtFrom.Focus();
                            return;
                        }

                        if (cmbTests.SelectedValue != null)
                        {
                            if (cmbResults.SelectedValue != null)
                            {

                                DateTime dtFromdt;
                                DateTime dtTodt;

                                dtFromdt = System.Convert.ToDateTime(dtFrom.Text + " 12:01:00.00 AM");
                                dtTodt = System.Convert.ToDateTime(dtTo.Text + " 12:01:00.00 AM");

                                DataTable dt_AllPlotValues = fillData(dtFromdt, dtTodt, false, 0, 0);

                                //DataTable dt_MINMAX = null;


                                //' new data table req. for the data fill for the ranges
                                dt_OnlyMinMax = new DataTable();

                                DataColumn clmnMin = new DataColumn();
                                clmnMin.ColumnName = "Min";
                                //.DataType = System.Type.GetType("System.integer")
                                dt_OnlyMinMax.Columns.Add(clmnMin);

                                DataColumn clmnMax = new DataColumn();
                                clmnMax.ColumnName = "Max";
                                //.DataType = System.Type.GetType("System.Integer")
                                //.DefaultValue = strVal(1)
                                dt_OnlyMinMax.Columns.Add(clmnMax);
                                ///'''''
                                string[] strVal;
                                int j = 0;
                                //int nActualValue = 0; -- commented to remove warnings... on 20100520 by madan 

                                for (j = 0; j <= dt_AllPlotValues.Rows.Count - 1; j++)
                                {
                                    //dt_MINMAX = getMinMAxRanges(dt.Rows(j)(0))  'Split(dt.Rows(j)("labotrd_ResultRange"), "-")
                                    //nActualValue = getMinMAxRanges()

                                    if ((dt_AllPlotValues.Rows[j][0]) != null)
                                    {
                                        strVal = System.Convert.ToString(dt_AllPlotValues.Rows[j][0]).Split('-');


                                        if (strVal.Length <= 1)
                                        {
                                            MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        //}
                                        DataRow r;
                                        r = dt_OnlyMinMax.NewRow();
                                        if ((strVal == null) == false)
                                        {
                                            if (strVal[0] == "")
                                            {
                                                strVal[1] = "-" + strVal[1];
                                                r[0] = System.Convert.ToInt32(strVal[1]);
                                                r[1] = System.Convert.ToInt32(strVal[2]);
                                            }
                                            else
                                            {
                                                r[0] = System.Convert.ToInt32(strVal[0]);
                                                r[1] = System.Convert.ToInt32(strVal[1]);
                                            }
                                            //r.Item(0) = CType(strVal(0), Integer)
                                            //r.Item(1) = CType(strVal(1), Integer)
                                            dt_OnlyMinMax.Rows.Add(r);
                                        }
                                    }
                                }
                                if (dt_AllPlotValues != null)
                                {
                                    dt_AllPlotValues.Dispose();
                                    dt_AllPlotValues = null;
                                }

                                //dt_MINMAX = dt_OnlyMinMax;

                                if (dt_OnlyMinMax.Rows.Count <= 0)
                                {
                                    MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbTests.Focus();
                                    return;
                                }
                                else
                                {
                                    //object  oGraphResult = new frmLab_GraphsResult(dtFrom.Text, dtTo.Text, cmbTests.SelectedValue, cmbResults.SelectedValue, gnPatientID, cmbTests.Text, cmbResults.Text, dt_AllPlotValues, dt_OnlyMinMax);
                                    //oGraphResult.MdiParent = this.Owner;
                                    //oGraphResult.WindowState = FormWindowState.Maximized;
                                    //oGraphResult.ShowInTaskbar = false;
                                    //oGraphResult.BringToFront();
                                    //oGraphResult.Show();
                                    //this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Select Results ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmbResults.Focus();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Select Tests ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbTests.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Valid To Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtTo.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Valid From Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtFrom.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {

                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.gloAuditTrail.ExceptionLog, gloAuditTrail.gloAuditTrail.ExceptionLog, gloAuditTrail.gloAuditTrail.ExceptionLog, ex.ToString(), gloAuditTrail.gloAuditTrail.ExceptionLog);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (dt_OnlyMinMax != null)
                {
                    dt_OnlyMinMax.Dispose();
                    dt_OnlyMinMax = null;
                }
            }


		}

        private bool IsDate(string testDateTime)
        {
            bool _result = false;

            try
            {
                if (testDateTime.Length > 0)
                {
                    DateTime dt = new DateTime();
                    DateTime.TryParse(dtFrom.Text.Trim(), out dt);
                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }
            return _result;
        }
		
		private void frmLab_Graphs_Load(object sender, System.EventArgs e)
		{
			try
			{
				DataTable dt;
				//int i = 0; -- commeted this variable to remove warnings by madan on 20100520
				
				dt = FillControlsTest();
				
				dtFrom.Value = System.DateTime.Now;
				dtTo.Value = System.DateTime.Now;
				
				if (dt != null)
				{
					cmbTests.DataSource = dt;
					cmbTests.DisplayMember = dt.Columns["TestName"].ColumnName;
					cmbTests.ValueMember = dt.Columns["TestID"].ColumnName;
					if (dt.Rows.Count > 0)
					{
						cmbTests.SelectedIndex = 0;
					}
				}
				
				if (_flg == true)
				{
					if (_fromDt != null)
					{
						dtFrom.Value =  (System.DateTime) (_fromDt);
					}
					
					if (_ToDt != null)
					{
						dtTo.Value =  (System.DateTime) (_ToDt);
					}
					
					if (_strTest != null)
					{
						//Dim testTemp As Integer = 0
						
						//For testTemp = 0 To cmbTests.Items.Count - 1
						//    If cmbTests.Items.Item(testTemp) Then
						cmbTests.Text = Convert.ToString(_strTest);
						//Next
						
					}
					
					if (_strResults != null)
					{
						cmbResults.Text =  Convert.ToString( _strResults);
					}
				}
				
			}
			catch (Exception ex)
			{
				//gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.gloAuditTrail.ExceptionLog, gloAuditTrail.gloAuditTrail.ExceptionLog,, ex.ToString(), gloAuditTrail.gloAuditTrail.ExceptionLog);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
				MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		public void cmbResults_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			// MessageBox.Show("1." & cmbResults.SelectedValue & "-" & cmbResults.Text & " 2. " & cmbTests.SelectedValue & "-" & cmbTests.Text)
		}
		
		// bLab_Flag = false - means call from the same form
		// bLab_Flag = true -  means call from Lab Order Form
		
		public DataTable fillData(DateTime FrmDate, DateTime Todate, bool bLab_Flag, long LabTestId, long LabResultID)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();
				
				SqlDataAdapter oadpt = new SqlDataAdapter();
				DataTable dt = new DataTable();
				
				cmd = new SqlCommand("gsp_Lab_Graphs", Conn);
				cmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter objParamPatientId;
				objParamPatientId = cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt);
				objParamPatientId.Direction = ParameterDirection.Input;
				objParamPatientId.Value = gnPatientID; //gnPatientID '391234534627492001
				
				SqlParameter objParamTestId;
				objParamTestId = cmd.Parameters.Add("@nTestId", SqlDbType.BigInt);
				objParamTestId.Direction = ParameterDirection.Input;
				if (bLab_Flag == false)
				{
					objParamTestId.Value = cmbTests.SelectedValue; //39221650183922105    '39221650133922105 '39221650133922103
				}
				else
				{
					objParamTestId.Value = LabTestId;
				}
				
				SqlParameter objParamnResultId;
				objParamnResultId = cmd.Parameters.Add("@nResultId", SqlDbType.BigInt);
				objParamnResultId.Direction = ParameterDirection.Input;
				if (bLab_Flag == false)
				{
					objParamnResultId.Value = cmbResults.SelectedValue; //1
				}
				else
				{
					objParamnResultId.Value = LabResultID; //1
				}
				
				// false = call from same form
				// true = call from Lab Order form
				
				SqlParameter objParamFromDate;
				objParamFromDate = cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime);
				objParamFromDate.Direction = ParameterDirection.Input;
				if (bLab_Flag == false)
				{
					objParamFromDate.Value = System.Convert.ToDateTime(dtFrom.Text + " 12:01:00.00 AM"); //"05/21/2007"
				}
				else
				{
					objParamFromDate.Value = System.DateTime.Today + " 12:01:00.00 AM"; //"05/21/2007"
				}
				
				
				SqlParameter objParamToDate;
				objParamToDate = cmd.Parameters.Add("@dtTodate", SqlDbType.DateTime);
				objParamToDate.Direction = ParameterDirection.Input;
				if (bLab_Flag == false)
				{
					objParamToDate.Value = System.Convert.ToDateTime(dtTo.Text + " 23:59:00.00 PM"); //"07/29/2007"
				}
				else
				{
					Todate = DateTime.Parse(Todate.ToString("MM/dd/yyyy"));
					objParamToDate.Value = Todate + " 23:59:00.00 PM"; //CType(System.DateTime.Today & " 23:59:00.00 PM", DateTime) '"07/29/2007"
				}
				
				
				SqlParameter objLabFlag;
				objLabFlag = cmd.Parameters.Add("@bLabFlg", SqlDbType.Bit);
				objLabFlag.Direction = ParameterDirection.Input;
				objLabFlag.Value = bLab_Flag;
				
				oadpt.SelectCommand = cmd;
				oadpt.Fill(dt);
                oadpt.Dispose();
                oadpt = null;
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
				return dt;
			}
			catch (Exception ex)
			{
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
				//MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                return null;
                //throw (ex);
			
			}
		}
		
		public void cmbResults_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			
		}

        private void frmLab_Graphs_Load_1(object sender, EventArgs e)
        {

        }
	}
}
