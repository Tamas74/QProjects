using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;
using System.Xml;
using System.IO;
using gloCommunity.Classes;

namespace gloCommunity.Forms
{
    public partial class frmBizTalkIncomingMsg : Form
    {
        public frmBizTalkIncomingMsg()
        {
            InitializeComponent();
        }
        gloC1FlexStyle ogloC1FlexStyle = new gloC1FlexStyle();
        DataTable dt = new DataTable();
        int COL_FirstName = 0;
        int COL_LastName = 1;
        int COL_dtDocTimeStamp = 2;
        int COL_FileName = 3;
        //Code Start-Added by kanchan on 20101020
        int COL_CCDId = 4;
        int COL_DocType = 5;
        int COL_PatientID = 6;
        Int16 COLUMN_COUNT = 7;



        private void frmBizTalkIncomingMsg_Load(System.Object sender, System.EventArgs e)
        {
            ogloC1FlexStyle.Style(cfgCCD);

            try
            {
                dt = PopulateFiles();

                SetGridStyle(dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

        }

        private DataTable PopulateFiles()
        {

            //Declaration of variables for making connection
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter sqladpt = null;
            string strquery = null;

            SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

            strquery = "Select sFirstName, sLastName, dtDOB, sFileName,nDocID,sFileType sStatus, nPatientID FROM BizTalk_IncomingMessage";

            cmd = new SqlCommand(strquery, conn);
            sqladpt = new SqlDataAdapter(cmd);

            sqladpt.Fill(dt);

            return dt;
        }

        private void SetGridStyle(DataTable dt)
        {
            var _with1 = cfgCCD;
            _with1.DataSource = dt;

            _with1.Dock = DockStyle.Fill;
            float _TotalWidth = 0;
            //Code Added by kanchan on 20101020 to display CCD
            //_TotalWidth = (.Width - 20) / 4
            _TotalWidth = (_with1.Width - 20) / 9;
            //Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            _with1.Cols.Count = COLUMN_COUNT;
            // .Rows.Count = 1
            _with1.AllowEditing = false;
            _with1.AllowAddNew = false;

            _with1.Styles.ClearUnused();

            //.Cols(COL_PATIENTID).Width = .Width * 0
            //.Cols(COL_PATIENTID).AllowEditing = False
            //.SetData(0, COL_PATIENTID, "PatientID")
            //.Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.CenterCenter

            _with1.Cols[COL_FirstName].Width = (int)((double)_width * 1);
            _with1.Cols[COL_FirstName].AllowEditing = false;
            _with1.SetData(0, COL_FirstName, "FirstName");
            _with1.Cols[COL_FirstName].TextAlignFixed = TextAlignEnum.CenterCenter;

            _with1.Cols[COL_LastName].Width = (int)((double)_width * 1);
            _with1.Cols[COL_LastName].AllowEditing = false;
            _with1.SetData(0, COL_LastName, "LastName");
            _with1.Cols[COL_LastName].TextAlignFixed = TextAlignEnum.CenterCenter;

            _with1.Cols[COL_dtDocTimeStamp].Width = (int)((double)_width * 1);
            _with1.Cols[COL_dtDocTimeStamp].AllowEditing = false;
            cfgCCD.Cols[COL_dtDocTimeStamp].DataType = typeof(System.String);
            _with1.SetData(0, COL_dtDocTimeStamp, "Date Time Stamp");
            _with1.Cols[COL_dtDocTimeStamp].TextAlignFixed = TextAlignEnum.CenterCenter;

            _with1.Cols[COL_FileName].Width = (int)((double)_width * 1);
            _with1.SetData(0, COL_FileName, "Document Name");
            _with1.Cols[COL_FileName].DataType = typeof(System.DateTime);
            _with1.Cols[COL_FileName].AllowEditing = true;

            //Code Start-Added by kanchan on 20101020 
            _with1.Cols[COL_CCDId].Width = (int)((double)_width * 0);
            _with1.SetData(0, COL_CCDId, "CCD ID");
            _with1.Cols[COL_CCDId].DataType = typeof(Int64);
            _with1.Cols[COL_CCDId].AllowEditing = true;
            _with1.Cols[COL_CCDId].TextAlignFixed = TextAlignEnum.CenterCenter;

            //Code Start-Added by kanchan on 20101020 
            _with1.Cols[COL_DocType].Width = (int)((double)_width * 1);
            _with1.SetData(0, COL_DocType, "Document Type");
            _with1.Cols[COL_DocType].AllowEditing = true;
            _with1.Cols[COL_DocType].TextAlignFixed = TextAlignEnum.CenterCenter;

           //Code Start-Added by kanchan on 20101020 
            _with1.Cols[COL_PatientID].Width = (int)((double)_width * 1);
            _with1.SetData(0, COL_PatientID, "PatientID");
            _with1.Cols[COL_PatientID].AllowEditing = true;
            _with1.Cols[COL_PatientID].Visible = false;
            _with1.Cols[COL_PatientID].TextAlignFixed = TextAlignEnum.CenterCenter;




        }

        private void cfgCCD_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if ((cfgCCD.Row > 0))
                {

                    if ((cfgCCD.GetData(cfgCCD.Row, COL_FileName) == null) == false)
                    {
                        //Code Start-Added by kanchan on 20101020 for CCD browser display

                        //Dim filename As String = cfgCCD.GetData(cfgCCD.Row, COL_FileName)
                        //Dim firstName As String = cfgCCD.GetData(cfgCCD.Row, COL_FirstName)
                        //Dim LastName As String = cfgCCD.GetData(cfgCCD.Row, COL_LastName)
                        //Dim frm As New frmCCD_ViewDetails(System.Windows.Forms.Application.StartupPath & "\Temp\" & filename & ".doc")
                        //Dim frm As New frmCCD_ViewDetails(filename)


                        Int64 _CCDID = cfgCCD.GetData(cfgCCD.Row, COL_CCDId);
                        string _DocType = cfgCCD.GetData(cfgCCD.Row, COL_DocType);
                        //Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
                        string FilePath = null;
                        string _extension = null;
                        if (_DocType == "CCD" | _DocType == "CCR")
                        {
                            _extension = ".xml";

                            FilePath = RetrieveDocumentFile(_CCDID, _extension);

                            //Dim frm As New frmPatientClinicalInformation(filename & ".rtf")

                            frmPatientClinicalInformation frm = new frmPatientClinicalInformation();
                            frm.CCDXMLFilePath = FilePath;

                            // Added transformation
                            System.Xml.Xsl.XslTransform myXslTransform = null;
                            string _strfileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".html";
                            //Dim ogloCCDInterface As gloCCDInterface
                            //ogloCCDInterface = New gloCCDInterface
                            string sFileType = string.Empty;
                            sFileType = GetClinicalFileType(FilePath);

                            if (sFileType == "CCR")
                            {
                                myXslTransform = new System.Xml.Xsl.XslTransform();
                                myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccrCss.xsl");
                                myXslTransform.Transform(FilePath, System.IO.Path.Combine(Application.StartupPath, _strfileName));
                                frm.WebBrowser1.Navigate(System.IO.Path.Combine(Application.StartupPath, _strfileName));
                            }
                            else if (sFileType == "CCD")
                            {
                                myXslTransform = new System.Xml.Xsl.XslTransform();
                                myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccdCss.xsl");
                                myXslTransform.Transform(FilePath, System.IO.Path.Combine(Application.StartupPath, _strfileName));
                                frm.WebBrowser1.Navigate(System.IO.Path.Combine(Application.StartupPath, _strfileName));
                            }
                            else
                            {
                                frm.WebBrowser1.Navigate(FilePath);
                            }

                            frm.tlsDisclosureSet.Items(0).Visible = false;

                            //frm.MdiParent = Me.MdiParent
                            frm.WindowState = FormWindowState.Maximized;
                            frm.ShowDialog();
                            if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, _strfileName)))
                            {
                                File.Delete(System.IO.Path.Combine(Application.StartupPath, _strfileName));
                            }
                        }
                        else if (_DocType == "DOC")
                        {
                            _extension = ".docx";
                            FilePath = RetrieveDocumentFile(_CCDID, _extension);
                            frmbizshowWord frm = new frmbizshowWord(FilePath);
                            frm.WindowState = FormWindowState.Maximized;
                            frm.ShowDialog();
                        }
                        else if (_DocType == "PDF")
                        {
                            _extension = ".pdf";
                            FilePath = RetrieveDocumentFile(_CCDID, _extension);
                            frmCCDForm objfrm = new frmCCDForm();
                            FileInfo ofile1 = new FileInfo(FilePath);
                            objfrm.WebBrowser1.Navigate(ofile1.FullName);
                            this.Focus();
                            objfrm.ShowInTaskbar = false;
                            objfrm.ShowDialog(this);

                        }

                        //After showing the file in browser delete temporary file .

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string RetrieveDocumentFile(Int64 nCCDId, string _extension)
        {
            object oResult = new object();
            string strFileName = "";
            SqlParameter sqlParam = null;
            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(GetConnectionString);

            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "BizTalk_RetrieveFile";

                //cmd = New SqlCommand("CCD_RetrieveCCDFile", conn)
                cmd.CommandType = CommandType.StoredProcedure;

                sqlParam = cmd.Parameters.Add("@nDOCID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = nCCDId;
                conn.Open();
                oResult = cmd.ExecuteScalar();

                if (oResult == null)
                {
                    return "";
                }


                if (Information.IsDBNull(oResult) == false)
                {
                    strFileName = ExamNewFaxFileName(gstrgloEMRStartupPath + gstrgloTempFolder, _extension);
                    //' generate Physical file
                    strFileName = GenerateFile(oResult, strFileName);
                    return strFileName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        //Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
        public string GetClinicalFileType(string strFilePath)
        {
            XmlReader xreader = null;
            string strTypre = "";
            try
            {
                //Dim oXMLSettings As New Xml.XmlReaderSettings()

                xreader = XmlReader.Create(strFilePath);
                while (xreader.Read())
                {
                    if (xreader.NodeType == XmlNodeType.Element)
                    {
                        switch (xreader.Name)
                        {
                            case "ContinuityOfCareRecord":
                                //gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                                strTypre = "CCR";
                                break;
                            case "ClinicalDocument":
                                //gloLibCCDGeneral.ClinicalDocFileType = "CCD"
                                strTypre = "CCD";
                                break;
                            case "ccr:ContinuityOfCareRecord":
                                //gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                                strTypre = "CCR";
                                break;
                        }
                    }
                }

                return strTypre;


            }
            catch (Exception ex)
            {
            }
        }
        //Code Start-Added by kanchan on 20101020 for CCD browser display
        public string GenerateFile(object cntFromDB, string strFileName)
        {
            try
            {
                if ((cntFromDB != null))
                {
                    byte[] content = Convert.ToByte(cntFromDB);
                    MemoryStream stream = new MemoryStream(content);
                    System.IO.FileStream oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.Create);
                    stream.WriteTo(oFile);
                    oFile.Close();
                    return strFileName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }

        }

        private void tlbbtn_Close_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void tlbbtn_Refresh_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dt = PopulateFiles();

            SetGridStyle(dt);
        }

    }
}




