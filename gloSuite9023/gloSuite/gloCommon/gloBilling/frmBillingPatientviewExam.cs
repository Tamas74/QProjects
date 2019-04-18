using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmBillingPatientviewExam : Form
    {

        #region " Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        Int64 _UserID = 0;
        string _UserName = "";
        string _messageBoxCaption = "";
        string _databaseconnectionstring = "";
        Int64 _nPatientID = 0;
        string _minDOS = "";
        string _SelectedProvider = "";
        #endregion

        #region "Constructor"

        public frmBillingPatientviewExam(string DatabaseConnectionString, Int64 nPatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _nPatientID = nPatientID;
            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        public frmBillingPatientviewExam(string DatabaseConnectionString, Int64 nPatientID, string minDOS,string SelectedProvider)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _nPatientID = nPatientID;
            _minDOS = minDOS;
            _SelectedProvider = SelectedProvider;
            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        #endregion

        #region "Form Load"

        private void frmBillingPatientviewExam_Load(object sender, EventArgs e)
        {
            try
            {
                Fill_PastExams();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion

   
        #region "Public & Private Methods"

        private void Fill_PastExams()
        {
           
            DataTable dtPatientExam = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);
            DataRow[] _dr = null;

            try
            {
                dtPatientExam = ogloBilling.Fill_PastExams(_nPatientID);



                c1PateintExams.BeginUpdate();
                c1PateintExams.DataSource = dtPatientExam;//.DefaultView;
                c1PateintExams.EndUpdate();

                c1PateintExams.Cols[0].Visible = false;
                c1PateintExams.Cols["nExamID"].Visible = false;
                c1PateintExams.Cols["nVisitID"].Visible = false;
                c1PateintExams.Cols["Specialty"].Visible = false;



                c1PateintExams.Cols["nExamID"].Width = 0;
                c1PateintExams.Cols["nVisitID"].Width = 0;
                c1PateintExams.Cols["DOS"].Width = 75;
                c1PateintExams.Cols["Exam Name"].Width = 250;
                c1PateintExams.Cols["Template Name"].Width = 200;
                c1PateintExams.Cols["ReviewedBy"].Width = 0;
                c1PateintExams.Cols["ProviderName"].Width = 100;
                c1PateintExams.Cols["Finished"].Width = 75;
                c1PateintExams.Cols["ReviewedBy"].Visible = false;

                // _width = 0;

                c1PateintExams.AllowEditing = true;
                c1PateintExams.Cols["nExamID"].AllowEditing = false;
                c1PateintExams.Cols["nVisitID"].AllowEditing = false;
                c1PateintExams.Cols["DOS"].AllowEditing = false;
                c1PateintExams.Cols["Exam Name"].AllowEditing = false;
                c1PateintExams.Cols["Template Name"].AllowEditing = false;
                c1PateintExams.Cols["ReviewedBy"].AllowEditing = false;
                c1PateintExams.Cols["ProviderName"].AllowEditing = false;
                c1PateintExams.Cols["Finished"].AllowEditing = false;
                c1PateintExams.Cols["Specialty"].AllowEditing = false;
                c1PateintExams.Cols["ReviewedBy"].Caption = "Reviewed By";
                c1PateintExams.Cols["ProviderName"].Caption = "Provider Name";
                c1PateintExams.ExtendLastCol = true;

              

                if (c1PateintExams.Rows != null && c1PateintExams.Rows.Count > 1 && c1PateintExams.Cols["DOS"] != null && _minDOS != "")
                {
                    DateTime _Dt ;
                  
                    _Dt = gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(_minDOS));
                    _dr = dtPatientExam.Select("DOS='" + _Dt + "'AND ProviderName='"+_SelectedProvider.Trim()+"'");

                    if (_dr != null)
                    {
                        if (_dr.Length > 0)
                        {
                            for (int _row = 1; _row < c1PateintExams.Rows.Count; _row++)
                            {
                                if (Convert.ToDateTime(c1PateintExams.Rows[_row]["DOS"]) == (gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(_minDOS))) && Convert.ToString(c1PateintExams.Rows[_row]["ProviderName"]) == _SelectedProvider.Trim())
                                {
                                    c1PateintExams.Row = _row;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            c1PateintExams.Row = c1PateintExams.FindRow(gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(_minDOS)), 0, c1PateintExams.Cols["DOS"].Index, true);
                        }

                    }
                    else
                    {
                        c1PateintExams.Row = c1PateintExams.FindRow(gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(_minDOS)), 0, c1PateintExams.Cols["DOS"].Index, true);
                    }


                    
                }

                if (c1PateintExams.Row > 0)
                {
                    c1PateintExams.RowSel = c1PateintExams.Row;
                }
                else
                {
                   
                    if (c1PateintExams.Rows.Count > 1)
                    {
                        c1PateintExams.Row = 1;
                        c1PateintExams.RowSel = c1PateintExams.Row;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;

                }
                if (_dr != null)
                {
                    _dr = null;
                }
            }
            
        }

        #endregion

      

        #region " Form Button Click Events "
        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void c1PateintExams_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Int64 _nExamID = 0;
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1PateintExams.HitTest(e.X, e.Y);
                    if (hitInfo.Row > 0)
                    {
                        _nExamID = Convert.ToInt64(c1PateintExams.GetData(c1PateintExams.RowSel, c1PateintExams.Cols["nExamID"].Index));

                        if (_nExamID != 0)
                        {
                            gloOffice.frmWd_PatientExam oPatientExam = new gloOffice.frmWd_PatientExam(_databaseconnectionstring, _nExamID);
                            oPatientExam.StartPosition = FormStartPosition.CenterParent;
                            oPatientExam.WindowState = FormWindowState.Maximized;
                            oPatientExam.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


        }

        private void tls_Load_Click(object sender, EventArgs e)
        {
            Int64 _nExamID = 0;
            try
            {

                //C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1PateintExams.HitTest(e.X, e.Y);
                //if (hitInfo.Row > 0)
                //{
                if (c1PateintExams.RowSel != -1)
                {
                    _nExamID = Convert.ToInt64(c1PateintExams.GetData(c1PateintExams.RowSel, c1PateintExams.Cols["nExamID"].Index));
                }

                if (_nExamID != 0)
                {
                    gloOffice.frmWd_PatientExam oPatientExam = new gloOffice.frmWd_PatientExam(_databaseconnectionstring, _nExamID);
                    oPatientExam.StartPosition = FormStartPosition.CenterParent;
                    oPatientExam.WindowState = FormWindowState.Maximized;
                    oPatientExam.Show();
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

    }
}