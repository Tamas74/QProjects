using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Collections;


namespace gloOffice
{
    public partial class frmWd_PatientStatement : Form
    {
        #region " Private Variables "
        private Wd.Document oCurDoc;
        private Wd.Application oWordApp;

        private DataTable dtTransaction;
        private DataTable dtLinePayment;
        private DataTable dtLinePaymentDetails;
        private DataTable dtStatement;

        String _DataBaseConnectionString = "";

        ArrayList arrFormFields;

        Double TotalLinePaid = 0;
        Double TotalChanges = 0;

        private String _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion 

        #region " Property Procedures "
        private Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        private Int64 _ClinicID = 1;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        private Int64 _TemplateID = 0;
        public Int64 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        private String _FileStatement = "";
        public String FileStatement
        {
            get { return _FileStatement; }
            set { _FileStatement = value; }
        }        
        
        #endregion 

        Object objMissing = System.Reflection.Missing.Value;

        #region " Constructor "
        public frmWd_PatientStatement(String DataBaseConnectionString, DataTable Transaction, DataTable LinePayment, DataTable LinePaymentDetails, Int64 TemplateID)
        {
            InitializeComponent();

            _DataBaseConnectionString = DataBaseConnectionString;
            dtTransaction = Transaction;
            dtLinePayment = LinePayment;
            dtLinePaymentDetails = LinePaymentDetails;
            _TemplateID = TemplateID;

            FillPatientStatement();
            //oCurDoc.Close(ref objMissing, ref objMissing, ref objMissing);

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }
        #endregion

        private void FillPatientStatement()
        {
            if (_TemplateID > 0)
            {
                DataTable dtTemp;
                gloOffice.gloPatientStatement ogloPatientStatement = new gloPatientStatement(_DataBaseConnectionString);
                gloOffice.gloTemplate ogloTemplate = new gloTemplate(_DataBaseConnectionString);
                gloOffice.Supporting.DataBaseConnectionString = _DataBaseConnectionString;
                object objTemplateDocument;
                try
                {
                    dtTemp = ogloPatientStatement.GetSingleTemplate(_TemplateID);

                    _FileStatement = gloOffice.Supporting.GenerateDocumentFile(_TemplateID);

                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        if (dtTemp.Rows[0]["sDescription"] != null)
                        {
                            objTemplateDocument = dtTemp.Rows[0]["sDescription"];
                            ogloTemplate.ConvertBinaryToFile(objTemplateDocument, _FileStatement);
                          //  wdStatement.Open(_FileStatement);
                            object thisObject = (object)_FileStatement;
                           // Wd.Application oWordApp = null;
                            gloWord.LoadAndCloseWord.OpenDSO(ref wdStatement, ref thisObject, ref oCurDoc, ref oWordApp );
                            _FileStatement = (string)thisObject;
                            ScanFormFields();

                            GenerateStatementTable();

                            PopulateInWord();

                            String strNewFileName = gloOffice.Supporting.NewDocumentName();
                            object oFileName = (object)strNewFileName;
                            object missing = System.Reflection.Missing.Value;
                            object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                            oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                            //wdStatement.Close();
                            _FileStatement = strNewFileName;
                        }
                    }
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
            }
        }

        private void ScanFormFields()
        {
            arrFormFields = new ArrayList();

            foreach (Wd.Table aTable in oCurDoc.Tables)
            {

                for (int i = 1; i < aTable.Rows.Count; i++)
                {
                    for (int j = 1; j <= aTable.Columns.Count; j++)
                    {
                        Object obj = 1;
                        if (aTable.Cell(i, j).Range.FormFields.Count > 0)
                        {
                            //Inserting Collection of FormFields in ArrayList
                            arrFormFields.Add(aTable.Cell(i, j).Range.FormFields.get_Item(ref obj).Result);
                        }
                    }

                }
               break;
            }
        }

        private void GenerateStatementTable()
        {
            dtStatement = new DataTable();
            Int64 _TransactionId = 0;
            Int64 _LineNo = 0;
            Int64 _LineDetailId = 0;
            DataRow _dtLineRow = null;
            try
            {



                for (int iCol = 0; iCol < arrFormFields.Count; iCol++)
                {
                    dtStatement.Columns.Add(arrFormFields[iCol].ToString());
                }


                for (int i = 0; i < dtTransaction.Rows.Count; i++)
                {
                    _TransactionId = Convert.ToInt64(dtTransaction.Rows[i]["nTransactionID"]);

                    for (int j = 0; j < dtLinePayment.Rows.Count; j++)
                    {
                        if (Convert.ToInt64(dtLinePayment.Rows[j]["nTransactionID"]) == _TransactionId)
                        {
                            _dtLineRow = dtStatement.NewRow();
                            //** Date Of Service (nFromDate)
                            if (dtStatement.Columns.Contains("Date"))
                            {
                                _dtLineRow["Date"] = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLinePayment.Rows[j]["nFromDate"])).ToShortDateString();
                            }
                            //** CPT Code
                            if (dtStatement.Columns.Contains("CPT"))
                            {
                                _dtLineRow["CPT"] = Convert.ToString(dtLinePayment.Rows[j]["sCPTCode"]);
                            }
                            //** CPT Description
                            if (dtStatement.Columns.Contains("Description"))
                            {
                                _dtLineRow["Description"] = Convert.ToString(dtLinePayment.Rows[j]["sCPTDescription"]);
                            }
                            //** Dx1 Code
                            if (dtStatement.Columns.Contains("Dx"))
                            {
                                _dtLineRow["Dx"] = Convert.ToString(dtLinePayment.Rows[j]["sDx1Code"]);
                            }
                            ////** Dx1 Description
                            //_dtLineRow[4] = Convert.ToString(dtLinePayment.Rows[j]["sDx1Description"]);
                            //** Charges 
                            if (dtStatement.Columns.Contains("Charges"))
                            {
                                _dtLineRow["Charges"] = Convert.ToString(dtLinePayment.Rows[j]["dCharges"]);
                                TotalChanges = TotalChanges + Convert.ToDouble(_dtLineRow["Charges"]);
                            }
                            //** Charges 
                            //if (dtStatement.Columns.Contains("Paid"))
                            //{
                            //    _dtLineRow["Paid"] = Convert.ToString(dtLinePayment.Rows[j]["LinePaid"]);
                            //}
                            //** Balance
                            if (dtStatement.Columns.Contains("Balance"))
                            {
                                _dtLineRow["Balance"] = Convert.ToString(dtLinePayment.Rows[j]["dTotal"]);
                            }

                            dtStatement.Rows.Add(_dtLineRow);
                            dtStatement.AcceptChanges();
                            _dtLineRow = null;

                            _LineNo = Convert.ToInt64(dtLinePayment.Rows[j]["nTransactionLineNo"]);
                            _LineDetailId = Convert.ToInt64(dtLinePayment.Rows[j]["nTransactionDetailID"]);

                            for (int detailIndex = 0; detailIndex < dtLinePaymentDetails.Rows.Count; detailIndex++)
                            {
                                if (Convert.ToInt64(dtLinePaymentDetails.Rows[j]["nBillingTransactionDetailID"]) == _LineDetailId)
                                {
                                    _dtLineRow = dtStatement.NewRow();
                                    //** Date Of Service (nFromDate)
                                    if (dtStatement.Columns.Contains("Date"))
                                    {
                                        _dtLineRow["Date"] = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLinePaymentDetails.Rows[detailIndex]["nPaymentDate"])).ToShortDateString();
                                    }
                                    ////** CPT Code

                                    if (dtStatement.Columns.Contains("CPT"))
                                    {
                                        _dtLineRow["CPT"] = Convert.ToString(dtLinePaymentDetails.Rows[j]["sCPTCode"]);
                                    }

                                    //_dtLineRow[1] = Convert.ToString(dtLinePaymentDetails.Rows[j]["sCPTCode"]);
                                    ////** CPT Description
                                    if (dtStatement.Columns.Contains("Description"))
                                    {
                                        _dtLineRow["Description"] = Convert.ToString(dtLinePaymentDetails.Rows[j]["sCPTDescription"]);
                                    }
                                    //_dtLineRow[2] = Convert.ToString(dtLinePaymentDetails.Rows[j]["sCPTDescription"]);
                                    ////** Dx1 Code
                                    //_dtLineRow[3] = Convert.ToString(dtLinePaymentDetails.Rows[j]["sDx1Code"]);
                                    //////** Dx1 Description
                                    //_dtLineRow[4] = Convert.ToString(dtLinePayment.Rows[j]["sDx1Description"]);
                                    //** Charges 
                                    //_dtLineRow[4] = Convert.ToString(dtLinePaymentDetails.Rows[j]["dCharges"]);
                                    ////** Charges 
                                    if (dtStatement.Columns.Contains("Paid"))
                                    {
                                        _dtLineRow["Paid"] = Convert.ToString(dtLinePaymentDetails.Rows[detailIndex]["dCurrentPaymentAmt"]);
                                        TotalLinePaid = TotalLinePaid + Convert.ToDouble(_dtLineRow["Paid"]);
                                    }
                                    ////** Balance 
                                    //_dtLineRow[6] = Convert.ToString(dtLinePaymentDetails.Rows[j]["dTotal"]);

                                    dtStatement.Rows.Add(_dtLineRow);
                                    dtStatement.AcceptChanges();
                                    _dtLineRow = null;

                                }
                            }

                        }
                    }
                    _TransactionId = 0;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void PopulateInWord()
        {

            Object objValue = 1;
            Object objRowUnit = Wd.WdUnits.wdRow;
            Object objCell = Wd.WdUnits.wdCell;

            try
            {
                foreach (Wd.Table aTable in oCurDoc.Tables)
                {
                    #region " Deleting UnWanted Rows "
                    int _RowCnt = aTable.Rows.Count;
                    for (int i = 1; i <= _RowCnt; i++)
                    {
                        if (i != 1)
                        {
                            aTable.Rows[aTable.Rows.Count].Delete();
                        }
                    }
                    #endregion

                    #region " Fill Table "
                    oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable);
                    oCurDoc.Application.Selection.Move(ref objRowUnit, ref objValue);

                    for (int iRow = 0; iRow < dtStatement.Rows.Count; iRow++)
                    {
                        aTable.Rows.Add(ref objMissing);
                        //oCurDoc.Application.Selection.Move(ref objRowUnit, ref objValue);
                        oCurDoc.Application.Selection.Next(ref objCell, ref objValue);
                        oCurDoc.Application.Selection.Select();
                        int minColCount = Math.Min(dtStatement.Columns.Count,aTable.Columns.Count);
                        for (int iCol = 0; iCol < minColCount; iCol++)
                        {
                            oCurDoc.Application.Selection.MoveRight(ref objCell, ref objValue, ref objMissing);
                            oCurDoc.Application.Selection.SelectCell();
                            if ((iRow + 2) <= aTable.Rows.Count)
                            {
                                aTable.Cell(iRow + 2, iCol + 1).Range.Select();
                                aTable.Cell(iRow + 2, iCol + 1).Range.Text = dtStatement.Rows[iRow][iCol].ToString();
                            }
                        }
                        oCurDoc.Application.Selection.Select();
                    } 
                    #endregion

                    #region " Inserting Amount Due "

                    aTable.Rows.Add(ref objMissing);
                    oCurDoc.Application.Selection.Next(ref objCell, ref objValue);
                    oCurDoc.Application.Selection.Select();

                    oCurDoc.Application.Selection.MoveRight(ref objCell, ref objValue, ref objMissing);
                    oCurDoc.Application.Selection.SelectCell();
                    if (aTable.Columns.Count >= 1)
                    {
                        aTable.Cell(aTable.Rows.Count, aTable.Columns.Count - 1).Range.Select();
                        aTable.Cell(aTable.Rows.Count, aTable.Columns.Count - 1).Range.Text = "Amount Due : ";
                    }
                    oCurDoc.Application.Selection.Select();

                    oCurDoc.Application.Selection.Next(ref objCell, ref objValue);
                    oCurDoc.Application.Selection.Select();

                    oCurDoc.Application.Selection.MoveRight(ref objCell, ref objValue, ref objMissing);
                    oCurDoc.Application.Selection.SelectCell();
                    aTable.Cell(aTable.Rows.Count, aTable.Columns.Count).Range.Select();
                    aTable.Cell(aTable.Rows.Count, aTable.Columns.Count).Range.Text = Convert.ToString(TotalChanges - TotalLinePaid);

                    oCurDoc.Application.Selection.Select();

                    #endregion

                    //float _TableWidht = oCurDoc.Application.ActiveDocument.PageSetup.PageWidth - (oCurDoc.Application.ActiveDocument.PageSetup.LeftMargin + oCurDoc.Application.ActiveDocument.PageSetup.RightMargin);
                    //for (int iCol = 1; iCol <= aTable.Columns.Count; iCol++)
                    //{
                    //    aTable.Columns[iCol].SetWidth(_TableWidht / aTable.Columns.Count, Wd.WdRulerStyle.wdAdjustProportional);
                    //}

                    //aTable.Columns.DistributeWidth();
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region " DSO Events "
        private void wdStatement_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdStatement.ActiveDocument;
            oWordApp = oCurDoc.Application;
        }
        private void wdStatement_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                if ((oCurDoc != null))
                {
                    //' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit 
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
                
                //if ((oWordApp != null))
                //{
                //    //RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent 
                //    //RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked 
                //    //UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick for oWordApp") 
                //    //Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        #endregion 

    }
}