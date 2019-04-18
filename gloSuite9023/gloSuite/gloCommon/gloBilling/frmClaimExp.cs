using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloBilling.Common;



namespace gloBilling
{
    public partial class frmClaimExp : Form
    {
        #region "Constructor"

            public frmClaimExp(Transaction  Transaction,Int64  TransactionID,string Databasestring)
        {
            InitializeComponent();

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

            _TransactionID = TransactionID;
            _DatabaseConnectionString = Databasestring;
          //  Transaction = Transaction;
        }

        #endregion

        #region "Variable Declaration"
        private string  _DatabaseConnectionString = "";
        long _TransactionID;
        //Transaction Transaction=null;
        public ClaimStructure _ClaimStructure;
        public bool _isModified = false;
        int Split_Col = 10;
        string oriClaimNo = "";
        string CurrentServiceRow = "";  
        string _messageBoxCaption="";
        bool _IsEnterBlankClaim=false;
        bool nonNumberEntered = false;
        private Label label;

        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; }
        }

        public bool Autosplit { get; set; }
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion      

        #region "Form Load"

        private void frmClaimExp_Load(object sender, EventArgs e)
        {
           
            DesignExpGrid("AllGrid");           
            FillClaim();
            FillOtherData();
            btnSplit.Visible = false;
            btnUnSplit.Visible = false;

            if (C1MainClaim.Rows.Count <= 7)
            {
                tsb_AutoSplit.Enabled = false;                               
            }
            else if (Autosplit)
            {
                AutoSplit();
            }
          

            if (getWidthofListItems(Convert.ToString(lbldx.Text), lbldx) >= lbldx.Width - 30)
            {
                lbldx.Text = lbldx.Text.Substring(0, 80)+"...";
            }

            C1MainClaim.Focus();
            C1MainClaim.Select(1, Split_Col, 1, Split_Col);
            C1MainClaim.Focus();
        }

        #endregion

        #region "Public Private Method"

            private void DesignExpGrid(string GridName)
        {
            try
            {
                double width = 0;

                #region "Currency Style"

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = C1MainClaim.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (C1MainClaim.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1MainClaim.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1MainClaim.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = C1MainClaim.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
  

                C1.Win.C1FlexGrid.CellStyle csDateStyle;// = C1MainClaim.Styles.Add("cs_DateStyle");

                try
                {
                    if (C1MainClaim.Styles.Contains("cs_DateStyle"))
                    {
                        csDateStyle = C1MainClaim.Styles["cs_DateStyle"];
                    }
                    else
                    {
                        csDateStyle = C1MainClaim.Styles.Add("cs_DateStyle");

                    }

                }
                catch
                {
                    csDateStyle = C1MainClaim.Styles.Add("cs_DateStyle");

                }
                csDateStyle.DataType =typeof(System.DateTime );
                csDateStyle.Format = "MM/dd/yyyy";

                #endregion

                if (GridName == "MainClaim" || GridName == "AllGrid")
                {
                    width = 600;
                    C1MainClaim.ExtendLastCol = true;

                    #region "Caption\Header"
                    C1MainClaim.Cols[0].Caption = "#";
                    C1MainClaim.Cols[1].Caption = "DOS";
                    C1MainClaim.Cols[2].Caption = "CPT";
                    C1MainClaim.Cols[3].Caption = "Modifiers";
                    C1MainClaim.Cols[4].Caption = "Dx1";
                    C1MainClaim.Cols[5].Caption = "Dx2";
                    C1MainClaim.Cols[6].Caption = "Dx3";
                    C1MainClaim.Cols[7].Caption = "Dx4";
                    C1MainClaim.Cols[8].Caption = "Units";
                    C1MainClaim.Cols[9].Caption = "Total";
                    C1MainClaim.Cols[Split_Col].Caption = "Split #";
                    C1MainClaim.Cols[11].Visible = false;
                    C1MainClaim.Cols[12].Visible = false;
                    C1MainClaim.Cols[13].Visible = false;
                    #endregion

                    #region "Read/Write"

                    C1MainClaim.Cols[0].AllowEditing = false;
                    C1MainClaim.Cols[1].AllowEditing = false;
                    C1MainClaim.Cols[2].AllowEditing = false;
                    C1MainClaim.Cols[3].AllowEditing = false;
                    C1MainClaim.Cols[4].AllowEditing = false;
                    C1MainClaim.Cols[5].AllowEditing = false;
                    C1MainClaim.Cols[6].AllowEditing = false;
                    C1MainClaim.Cols[7].AllowEditing = false;
                    C1MainClaim.Cols[8].AllowEditing = false;
                    C1MainClaim.Cols[9].AllowEditing = false;
                    C1MainClaim.Cols[Split_Col].AllowEditing = true;
                    C1MainClaim.Cols[Split_Col].AllowSorting  = false;

                    #endregion

                    #region "Width"

                    C1MainClaim.Cols[0].Width = Convert.ToInt16(width * 0.05);//
                    C1MainClaim.Cols[1].Width = Convert.ToInt16( width * 0.14);
                    C1MainClaim.Cols[2].Width = Convert.ToInt16( width * 0.08);
                    C1MainClaim.Cols[3].Width =  Convert.ToInt16(width * 0.1);
                    C1MainClaim.Cols[4].Width =  Convert.ToInt16(width * 0.1);
                    C1MainClaim.Cols[5].Width =  Convert.ToInt16(width * 0.1);
                    C1MainClaim.Cols[6].Width =  Convert.ToInt16(width * 0.1);
                    C1MainClaim.Cols[7].Width =  Convert.ToInt16(width * 0.1);
                    C1MainClaim.Cols[8].Width =  Convert.ToInt16(width * 0.06);
                    C1MainClaim.Cols[9].Width =  Convert.ToInt16(width * 0.15);
                    C1MainClaim.Cols[Split_Col].Width = Convert.ToInt16(width * 0.08);
                   

                    #endregion

                    #region "Style"


                  //  C1MainClaim.Cols[Split_Col].DataType = typeof(System.Int16);
                    //Changed Datatype & Added Format for Column in 6031
                    C1MainClaim.Cols[8].DataType = typeof(System.Decimal);
                    C1MainClaim.Cols[8].Format = "#############0.####";
                    C1MainClaim.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    C1MainClaim.Cols[9].Style = csCurrencyStyle;
                   // C1MainClaim.Cols[Split_Col].Format = "0";


                    C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = C1MainClaim.Styles.Add("cs_EditableCurrencyStyle");
                    try
                    {
                        if (C1MainClaim.Styles.Contains("cs_EditableCurrencyStyle"))
                        {
                            csEditableCurrencyStyle = C1MainClaim.Styles["cs_EditableCurrencyStyle"];
                        }
                        else
                        {
                            csEditableCurrencyStyle = C1MainClaim.Styles.Add("cs_EditableCurrencyStyle");
            
                        }

                    }
                    catch
                    {
                        csEditableCurrencyStyle = C1MainClaim.Styles.Add("cs_EditableCurrencyStyle");
  
                    }
                    csEditableCurrencyStyle.DataType = typeof(System.Int16);

                    csEditableCurrencyStyle.Format = "0";
                    csEditableCurrencyStyle.BackColor = Color.White;
                    C1MainClaim.Cols[Split_Col].Style = csEditableCurrencyStyle;

                    C1MainClaim.Cols[1].Style = csDateStyle;
                    

                    #endregion

                }

                if (GridName == "NewClaims" || GridName == "AllGrid")
                {
                    width = 600;
                    C1NewClaim.ExtendLastCol = true;

                    #region "Caption\Header"
                    C1NewClaim.Cols[0].Caption = "Split #";
                    C1NewClaim.Cols[1].Caption = "DOS";
                    C1NewClaim.Cols[2].Caption = "# Charges";
                    C1NewClaim.Cols[3].Caption = "Total Charges";
                    C1NewClaim.Cols[4].Caption = "# Diagnoses";
                    C1NewClaim.Cols[5].Caption = "Diagnoses";


                    #endregion

                    #region "Width"

                    C1NewClaim.Cols[0].Width = Convert.ToInt16(width * 0.08);//
                    C1NewClaim.Cols[1].Width = Convert.ToInt16(width * 0.14);
                    C1NewClaim.Cols[2].Width = Convert.ToInt16(width * 0.11);
                    C1NewClaim.Cols[3].Width = Convert.ToInt16(width * 0.15);
                    C1NewClaim.Cols[4].Width = Convert.ToInt16(width * 0.13);
                    C1NewClaim.Cols[5].Width = Convert.ToInt16(width * 0.36);                 
                  
                    #endregion

                    #region "Style"
                    
                    C1NewClaim.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    C1NewClaim.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                    C1NewClaim.Cols[3].Style = csCurrencyStyle;

                    C1NewClaim.Cols[1].Style = csDateStyle;

                    #endregion

                }

                if (GridName == "SplitClaim" || GridName == "AllGrid")
                {
                    width = 600;
                    C1SplitClaim.ExtendLastCol = true;

                    #region "Caption\Header"
                    C1SplitClaim.Cols[0].Caption = "#";
                    C1SplitClaim.Cols[1].Caption = "DOS";
                    C1SplitClaim.Cols[2].Caption = "CPT";
                    C1SplitClaim.Cols[3].Caption = "Modifiers";
                    C1SplitClaim.Cols[4].Caption = "Dx1";
                    C1SplitClaim.Cols[5].Caption = "Dx2";
                    C1SplitClaim.Cols[6].Caption = "Dx3";
                    C1SplitClaim.Cols[7].Caption = "Dx4";
                    C1SplitClaim.Cols[8].Caption = "Units";
                    C1SplitClaim.Cols[9].Caption = "Total";
                    C1SplitClaim.Cols[10].Visible = false;
                    C1SplitClaim.Cols[11].Visible = false;
                    //Changed Datatype & Added Format for Column in 6031
                    C1SplitClaim.Cols[8].DataType = typeof(System.Decimal);
                    C1SplitClaim.Cols[8].Format = "#############0.####";

                    #endregion

                    #region "Width"

                    C1SplitClaim.Cols[0].Width = Convert.ToInt16(width * 0.05);//
                    C1SplitClaim.Cols[1].Width = Convert.ToInt16(width * 0.12);
                    C1SplitClaim.Cols[2].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[3].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[4].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[5].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[6].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[7].Width = Convert.ToInt16(width * 0.1);
                    C1SplitClaim.Cols[8].Width = Convert.ToInt16(width * 0.06);
                    C1SplitClaim.Cols[9].Width = Convert.ToInt16(width * 0.12);
                    C1SplitClaim.Cols[10].Width = Convert.ToInt16(width * 0.08);

                    #endregion

                    #region "Style"

                    C1SplitClaim.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter ;
                    C1SplitClaim.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    C1SplitClaim.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    C1SplitClaim.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    C1SplitClaim.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    C1SplitClaim.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    C1SplitClaim.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    C1SplitClaim.Cols[9].Style = csCurrencyStyle;

                    C1SplitClaim.Cols[1].Style = csDateStyle;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

            private void FillOtherData()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters(); ;
                object Dx = null;
                DataTable dtExpClaim=new DataTable();
                try
                {
                    oDB.Connect(false);
                    oDBParameters.Add("@nTransactionid", _TransactionID , ParameterDirection.Input, SqlDbType.VarChar);               
                    Dx=oDB.ExecuteScalar ("BL_Select_Claim_DX", oDBParameters);
                    string sqlstring="SELECT dbo.GET_NAME(p.sFirstName,p.sMiddleName,p.sLastName) as Patient,dbo.getSubClaimNumber(TRNCMST.nClaimNo,TRNCMST.nSubClaimNo, TRNCMST.sMainClaimNo,5)"+
                           " as 'ClaimNo',p.sPatientCode as PatientCode FROM  BL_TRANSACTION_CLAIM_MST TRNCMST WITH(NOLOCK) left join patient p WITH(NOLOCK) on p.nPatientID=TRNCMST.nPatientID " +
                           " WHERE TRNCMST.nTransactionID ="+_TransactionID;
                    oDB.Retrive_Query(sqlstring,out dtExpClaim);
                    if (dtExpClaim != null)
                    {
                        lblpatient.Text = dtExpClaim.Rows[0][2].ToString() +" "+ dtExpClaim.Rows[0][0].ToString();
                        lblclaim.Text = dtExpClaim.Rows[0][1].ToString() + "  " + Convert.ToString(C1MainClaim.Rows.Count - 1) + " Charges" + "  " + Convert.ToString(Dx).Split(',').GetLength(0).ToString() + " Diagnoses";
                        //lblCharges.Text = C1MainClaim.Rows.Count - 1 + " Charges" ;                     
                        //lblDiagnosis.Text =Convert.ToString(Dx).Split(',').GetLength(0).ToString()+ " Diags";
                        oriClaimNo = dtExpClaim.Rows[0][1].ToString();
                    }
                    oDB.Disconnect();
                    lbldx.Text = Convert.ToString(Dx);
                    lbldx.Tag = Convert.ToString(Dx);          
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    dtExpClaim.Dispose();
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
                    
            }

            private void FillClaim()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);           
            DataTable dtClaimDetails = new DataTable();
            try
            {
                string strSQL = "SELECT ROW_NUMBER() OVER(ORDER BY nTransactionLineNo) AS '#',convert(datetime,dbo.CONVERT_TO_DATE(TraLine.nFromDate)) as DOS,TraLine.sCPTCode ,CASE WHEN  LEN(TraLine.sMod4Code ) > 0 THEN TraLine.sMod1Code +','+TraLine.sMod2Code+','+TraLine.sMod3Code+','+TraLine.sMod4Code WHEN "+
                                " LEN(TraLine.sMod3Code ) > 0 THEN TraLine.sMod1Code +','+TraLine.sMod2Code+','+TraLine.sMod3Code WHEN LEN(TraLine.sMod2Code ) > 0 THEN "+
                                " TraLine.sMod1Code +','+TraLine.sMod2Code ELSE TraLine.sMod1Code END AS Modifier,TraLine.sDx1Code," +
                                "TraLine.sDx2Code ,TraLine.sDx3Code,TraLine.sDx4Code,TraLine.dUnit,dTotal,'' AS Split#,TraLine.nTransactionDetailID ,TraLine.nTransactionMasterDetailID,TraLine.nTransactionMasterID   FROM BL_Transaction_Claim_Lines AS  TraLine WITH(NOLOCK)  WHERE nTransactionID =" + _TransactionID;
                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dtClaimDetails);
                oDB.Disconnect();
              
                C1MainClaim.DataSource = dtClaimDetails.DefaultView;
                DesignExpGrid("MainClaim");
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                dtClaimDetails.Dispose();
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

            private void FillSplitClaim(string SplitClaimNo)
            {
                try
                {
                    int ChargeLine = 0;
                    C1SplitClaim.Clear();
                    C1SplitClaim.Rows.Count = 1;
                    //
                    if (SplitClaimNo == "0")
                    {
                        lblChargeSplit.Text = "   Charges for Split ";
                    }
                    else
                    {
                        lblChargeSplit.Text = "   Charges for Split " + SplitClaimNo;
                    }
                    //

                    for (int i = 1; C1MainClaim.Rows.Count > i; i++)
                    {
                        if (C1MainClaim.GetData(i, "Split#").ToString() == SplitClaimNo)
                        {
                            C1SplitClaim.Rows.Add();
                            ChargeLine = ChargeLine + 1;
                            C1SplitClaim.SetData(ChargeLine, 0, C1MainClaim.Rows[i][0]);
                            C1SplitClaim.SetData(ChargeLine, 1, C1MainClaim.Rows[i]["DOS"]);
                            C1SplitClaim.SetData(ChargeLine, 2, C1MainClaim.Rows[i]["sCPTCode"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 3, C1MainClaim.Rows[i]["Modifier"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 4, C1MainClaim.Rows[i]["sDx1Code"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 5, C1MainClaim.Rows[i]["sDx2Code"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 6, C1MainClaim.Rows[i]["sDx3Code"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 7, C1MainClaim.Rows[i]["sDx4Code"].ToString());
                            C1SplitClaim.SetData(ChargeLine, 8, C1MainClaim.Rows[i]["dUnit"]);
                            C1SplitClaim.SetData(ChargeLine, 9, C1MainClaim.Rows[i]["dTotal"]);
                            C1SplitClaim.SetData(ChargeLine, 10, C1MainClaim.Rows[i]["nTransactionDetailID"]);
                            C1SplitClaim.SetData(ChargeLine, 11, C1MainClaim.Rows[i]["nTransactionMasterDetailID"]);

                        }
                    }
                    DesignExpGrid("SplitClaim");
                    //C1SplitClaim.RowSel =C1SplitClaim.FindRow(CurrentServiceRow, 0,0,false );
                    C1SplitClaim.Row = C1SplitClaim.FindRow(CurrentServiceRow, 1,0,false,false,true);

                    if (C1SplitClaim.Row == -1)
                    {
                        if (C1SplitClaim.Rows.Count > 1)
                        {
                            C1SplitClaim.Select(1, 0);
                            if (C1SplitClaim.Rows.Count > 1)
                            {
                                C1SplitClaim.Select(1, 0);
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private int getWidthofListItems(string _text, Label label)
            {
                //Code Review Changes: Dispose Graphics object
                int width = 0;
                Graphics g = this.CreateGraphics();
                if (g!=null)
                {
                    SizeF s = g.MeasureString(_text, label.Font);
                    width = Convert.ToInt32(s.Width);
                    //Dispose graphics object
                    g.Dispose();
                }
                
                return width;
            }

        #endregion

        #region "Form Control Event"

            private void btnSplit_Click(object sender, EventArgs e)
        {

            try
            {
                this.C1NewClaim.RowColChange -= new System.EventHandler(this.C1NewClaim_RowColChange);
                C1NewClaim.Clear();
                C1NewClaim.Rows.Count = 1;
                int ClaimNo = 0;
                int i = 0;
                ArrayList SplitClaimno = new ArrayList();
              //  bool Seq = false;
                for (i = 1; C1MainClaim.Rows.Count > i; i++)
                {
                    if (C1MainClaim.GetData(i, "Split#").ToString().Trim() != "" && C1MainClaim.GetData(i, "Split#") != null)
                    {
                        if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) > ClaimNo)
                        {
                            ClaimNo = Convert.ToInt16(C1MainClaim.GetData(i, "Split#"));
                        }
                        SplitClaimno.Add(Convert.ToInt16(C1MainClaim.GetData(i, "Split#")));
                    }
                    //else
                    //{
                    //    C1MainClaim.SetData(i, Split_Col, "");
                    //    DesignExpGrid("NewClaims");
                    //    MessageBox.Show("Split claim not enter.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}                   
                }
                //for (i = 1; ClaimNo > i; i++)
                //{
                //    for (int z = 0; SplitClaimno.Count  > z; z++)
                //    {
                //        if (i == Convert.ToInt16(SplitClaimno[z]))
                //        {
                //            Seq = true;                            
                //        }
                //    }
                //    if (Seq == false)
                //    {
                //        DesignExpGrid("NewClaims");
                //        MessageBox.Show("Split claim not in sequence.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                //    Seq = false;
                //}

                int Charges = 0;
              //  string Diagnosis = "";
                double TotalCharge = 0;
                ArrayList DxCollection = new ArrayList();
                string DxList = "";
                DateTime DOS = Convert.ToDateTime("01/01/0001");

                for (int y = 1; ClaimNo >= y; y++)
                {
                    Charges = 0;
                    TotalCharge = 0;
                    DOS = Convert.ToDateTime("01/01/0001");
                    DxCollection.Clear();
                    DxList = "";
                    for (i = 1; C1MainClaim.Rows.Count > i; i++)
                    {
                        if (Convert.ToString(C1MainClaim.GetData(i, "Split#") ) != "" && C1MainClaim.GetData(i, "Split#") != null)
                        {
                            if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) == y)
                            {
                                Charges = Charges + 1;
                                TotalCharge = TotalCharge + Convert.ToDouble(C1MainClaim.GetData(i, "dTotal"));
                                if (Convert.ToDateTime(C1MainClaim.GetData(i, "DOS")) > DOS)
                                {
                                    DOS = Convert.ToDateTime(C1MainClaim.GetData(i, "DOS"));
                                }
                                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")) != "")
                                {
                                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")));
                                }
                                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")) != "")
                                {
                                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")));
                                }
                                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")) != "")
                                {
                                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")));
                                }
                                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")) != "")
                                {
                                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")));
                                }
                            }
                        }
                    }
                    for (int z = 0; DxCollection.Count > z; z++)
                    {
                        if (z == 0)
                        {
                            DxList = DxCollection[z].ToString();
                        }
                        else
                        {
                            if (DxCollection[z].ToString().Trim() != "")
                            {
                                DxList = DxList + "," + DxCollection[z].ToString();
                            }
                        }
                    }
                    C1NewClaim.Rows.Add();
                    C1NewClaim.SetData(y, 0, y);
                    C1NewClaim.SetData(y, 1, DOS.ToString("mm/dd/yyyy"));
                    C1NewClaim.SetData(y, 2, Charges);
                    C1NewClaim.SetData(y, 3, TotalCharge);
                    C1NewClaim.SetData(y, 4, DxCollection.Count);
                    C1NewClaim.SetData(y, 5, DxList);

                }
                this.C1NewClaim.RowColChange += new System.EventHandler(this.C1NewClaim_RowColChange);
                if (C1NewClaim.Rows.Count > 1)
                {
                    FillSplitClaim("1");
                }
                DesignExpGrid("NewClaims");
               // C1MainClaim.Cols[Split_Col].AllowEditing = false;
                btnSplit.Visible = false;
                btnUnSplit.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

            public void FillNewClaim(string CurrentSplitNo)
            {
                try
                {
                   
                    C1NewClaim.Clear();
                    C1NewClaim.Rows.Count = 1;
                    int ClaimNo = 0;
                    int i = 0;
                    ArrayList SplitClaimno = new ArrayList();
                   // bool Seq = false;
                    for (i = 1; C1MainClaim.Rows.Count > i; i++)
                    {
                        if (C1MainClaim.GetData(i, "Split#").ToString().Trim() != "" && C1MainClaim.GetData(i, "Split#") != null)
                        {
                            if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) > ClaimNo)
                            {
                                ClaimNo = Convert.ToInt16(C1MainClaim.GetData(i, "Split#"));
                            }
                            SplitClaimno.Add(Convert.ToInt16(C1MainClaim.GetData(i, "Split#")));
                        }
                        //else
                        //{
                        //    C1MainClaim.SetData(i, Split_Col, "");
                        //    DesignExpGrid("NewClaims");
                        //    MessageBox.Show("Split claim not enter.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}                   
                    }
                    //for (i = 1; ClaimNo > i; i++)
                    //{
                    //    for (int z = 0; SplitClaimno.Count  > z; z++)
                    //    {
                    //        if (i == Convert.ToInt16(SplitClaimno[z]))
                    //        {
                    //            Seq = true;                            
                    //        }
                    //    }
                    //    if (Seq == false)
                    //    {
                    //        DesignExpGrid("NewClaims");
                    //        MessageBox.Show("Split claim not in sequence.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //    Seq = false;
                    //}

                    int Charges = 0;
                    //string Diagnosis = "";
                    double TotalCharge = 0;
                    ArrayList DxCollection = new ArrayList();
                    string DxList = "";
                    DateTime DOS = Convert.ToDateTime("01/01/0001");
                    int NewClaimNo = 0;
                    for (int y = 1; ClaimNo >= y; y++)
                    {
                        Charges = 0;
                        TotalCharge = 0;
                        DOS = Convert.ToDateTime("01/01/0001");
                        DxCollection.Clear();
                        DxList = "";
                        for (i = 1; C1MainClaim.Rows.Count > i; i++)
                        {
                            if (Convert.ToString(C1MainClaim.GetData(i, "Split#")) != "" && C1MainClaim.GetData(i, "Split#") != null)
                            {
                                if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) == y)
                                {
                                    Charges = Charges + 1;
                                    TotalCharge = TotalCharge + Convert.ToDouble(C1MainClaim.GetData(i, "dTotal"));
                                    if (Convert.ToDateTime(C1MainClaim.GetData(i, "DOS")) > DOS)
                                    {
                                        DOS = Convert.ToDateTime(C1MainClaim.GetData(i, "DOS"));
                                    }
                                    if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")) != "")
                                    {
                                        DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")));
                                    }
                                    if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")) != "")
                                    {
                                        DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")));
                                    }
                                    if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")) != "")
                                    {
                                        DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")));
                                    }
                                    if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")) != "")
                                    {
                                        DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")));
                                    }
                                }
                            }
                        }
                        for (int z = 0; DxCollection.Count > z; z++)
                        {
                            if (z == 0)
                            {
                                DxList = DxCollection[z].ToString().Trim();
                            }
                            else
                            {
                                if (DxCollection[z].ToString().Trim() != "")
                                {
                                    DxList = DxList + "," + DxCollection[z].ToString().Trim();
                                }
                            }
                        }
                        if (_IsEnterBlankClaim == false)
                        {
                            if (DOS != Convert.ToDateTime("01/01/0001"))
                            {
                                NewClaimNo = NewClaimNo + 1;
                                C1NewClaim.Rows.Add();
                                C1NewClaim.SetData(NewClaimNo, 0, y);
                                C1NewClaim.SetData(NewClaimNo, 1, DOS.ToString("MM/dd/yyyy"));
                                C1NewClaim.SetData(NewClaimNo, 2, Charges);
                                C1NewClaim.SetData(NewClaimNo, 3, TotalCharge);
                                C1NewClaim.SetData(NewClaimNo, 4, DxCollection.Count);
                                C1NewClaim.SetData(NewClaimNo, 5, DxList);
                            }
                        }
                        else
                        {
                            C1NewClaim.Rows.Add();
                            C1NewClaim.SetData(y, 0, y);
                            C1NewClaim.SetData(y, 1, DOS.ToString("MM/dd/yyyy"));
                            C1NewClaim.SetData(y, 2, Charges);
                            C1NewClaim.SetData(y, 3, TotalCharge);
                            C1NewClaim.SetData(y, 4, DxCollection.Count);
                            C1NewClaim.SetData(y, 5, DxList);
                        }


                    }

                    if (C1NewClaim.Rows.Count > 1)
                    {
                        if (CurrentSplitNo == "")
                        {
                            CurrentSplitNo = Convert.ToString(C1NewClaim.GetData(1, 0));
                        }
                        FillSplitClaim(CurrentSplitNo);
                    }
                    else
                    {
                        FillSplitClaim("0");
                    }
                    DesignExpGrid("NewClaims");
                   
                    C1NewClaim.Row = C1NewClaim.FindRow(CurrentSplitNo, 1, 0, false, false, true);
                 
                 //   C1NewClaim.Select(Convert.ToInt16 (CurrentSplitNo),1,true );
                    // C1MainClaim.Cols[Split_Col].AllowEditing = false;
                    btnSplit.Visible = false;
                    btnUnSplit.Visible = true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            public bool Validation()
            {
                try
                {
                    this.C1NewClaim.RowColChange -= new System.EventHandler(this.C1NewClaim_RowColChange);
                  //  C1NewClaim.Clear();
                  //  C1NewClaim.Rows.Count = 1;
                    int ClaimNo = 0;
                    int i = 0;
                    ArrayList SplitClaimno = new ArrayList();
                    bool Seq = false;
                    for (i = 1; C1MainClaim.Rows.Count > i; i++)
                    {
                        if (C1MainClaim.GetData(i, "Split#").ToString().Trim() != "" && C1MainClaim.GetData(i, "Split#") != null)
                        {
                            if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) > ClaimNo)
                            {
                                ClaimNo = Convert.ToInt16(C1MainClaim.GetData(i, "Split#"));
                            }
                            SplitClaimno.Add(Convert.ToInt16(C1MainClaim.GetData(i, "Split#")));
                        }
                        else
                        {
                            C1MainClaim.SetData(i, Split_Col, "");
                            DesignExpGrid("NewClaims");
                            //MessageBox.Show("Split number not enter.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("All charges from the original claim must have a split #. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            
                            //  FillNewClaim(C1MainClaim.GetData(i, "Split#").ToString());
                            return false;
                        }
                    }
                    for (i = 1; ClaimNo > i; i++)
                    {
                        for (int z = 0; SplitClaimno.Count > z; z++)
                        {
                            if (i == Convert.ToInt16(SplitClaimno[z]))
                            {
                                Seq = true;
                            }
                        }
                        if (Seq == false)
                        {
                            DesignExpGrid("NewClaims");
                            //MessageBox.Show("Split number not in sequence.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("You cannot skip any Split #s.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                           // FillNewClaim(C1MainClaim.GetData(i, "Split#").ToString());
                            return false;
                        }
                        Seq = false;
                    }

                    return true;
                    //int Charges = 0, DiagnosisNo = 0;
                    //string Diagnosis = "";
                    //double TotalCharge = 0;
                    //ArrayList DxCollection = new ArrayList();
                    //string DxList = "";
                    //DateTime DOS = Convert.ToDateTime("01/01/0001");

                    //for (int y = 1; ClaimNo >= y; y++)
                    //{
                    //    Charges = 0;
                    //    TotalCharge = 0;
                    //    DOS = Convert.ToDateTime("01/01/0001");
                    //    DxCollection.Clear();
                    //    DxList = "";
                    //    for (i = 1; C1MainClaim.Rows.Count > i; i++)
                    //    {
                    //        if (C1MainClaim.GetData(i, "Split#") != "" && C1MainClaim.GetData(i, "Split#") != null)
                    //        {
                    //            if (Convert.ToInt16(C1MainClaim.GetData(i, "Split#")) == y)
                    //            {
                    //                Charges = Charges + 1;
                    //                TotalCharge = TotalCharge + Convert.ToDouble(C1MainClaim.GetData(i, "dTotal"));
                    //                if (Convert.ToDateTime(C1MainClaim.GetData(i, "DOS")) > DOS)
                    //                {
                    //                    DOS = Convert.ToDateTime(C1MainClaim.GetData(i, "DOS"));
                    //                }
                    //                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")) != "")
                    //                {
                    //                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx1Code")));
                    //                }
                    //                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")) != "")
                    //                {
                    //                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx2Code")));
                    //                }
                    //                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")) != "")
                    //                {
                    //                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx3Code")));
                    //                }
                    //                if (!DxCollection.Contains(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code"))) && Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")) != "")
                    //                {
                    //                    DxCollection.Add(Convert.ToString(C1MainClaim.GetData(i, "sDx4Code")));
                    //                }
                    //            }
                    //        }
                    //    }
                    //    for (int z = 0; DxCollection.Count > z; z++)
                    //    {
                    //        if (z == 0)
                    //        {
                    //            DxList = DxCollection[z].ToString();
                    //        }
                    //        else
                    //        {
                    //            if (DxCollection[z].ToString().Trim() != "")
                    //            {
                    //                DxList = DxList + "," + DxCollection[z].ToString();
                    //            }
                    //        }
                    //    }
                    //    C1NewClaim.Rows.Add();
                    //    C1NewClaim.SetData(y, 0, y);
                    //    C1NewClaim.SetData(y, 1, DOS);
                    //    C1NewClaim.SetData(y, 2, Charges);
                    //    C1NewClaim.SetData(y, 3, TotalCharge);
                    //    C1NewClaim.SetData(y, 4, DxCollection.Count);
                    //    C1NewClaim.SetData(y, 5, DxList);

                    //}
                    //this.C1NewClaim.RowColChange += new System.EventHandler(this.C1NewClaim_RowColChange);
                    //if (C1NewClaim.Rows.Count > 1)
                    //{
                    //    FillSplitClaim("1");
                    //}
                    //DesignExpGrid("NewClaims");
                    //C1MainClaim.Cols[Split_Col].AllowEditing = false;
                    //btnSplit.Visible = false;
                    //btnUnSplit.Visible = true;
                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return false;
                }
                finally
                {
                    this.C1NewClaim.RowColChange += new System.EventHandler(this.C1NewClaim_RowColChange);
                }
            }

            private void tsb_Cancel_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void tsb_AutoSplit_Click(object sender, EventArgs e)
            {
                try
                {
                    AutoSplit();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private void btnUnSplit_Click(object sender, EventArgs e)
            {
                C1MainClaim.Cols[Split_Col].AllowEditing = true;                   
                C1SplitClaim.Clear();
                C1SplitClaim.Rows.Count = 1;
                C1NewClaim.Clear();
                C1NewClaim.Rows.Count = 1;
                DesignExpGrid("NewClaims");
                DesignExpGrid("SplitClaim");
                btnSplit.Visible = true;
                btnUnSplit.Visible = false;
            }

            private void tsb_OK_Click(object sender, EventArgs e)
            {
              //  frmBillingModifyCharges ofrmBillingModifyCharges = null;
                try
                {
                    if (Validation())
                    {
                        DialogResult _dlgInsurance = DialogResult.None;
                        _dlgInsurance = MessageBox.Show("Splitting claims is not reversible. Claim " + oriClaimNo + " will be split into " + Convert.ToString(C1NewClaim.Rows.Count - 1) + " new claims. Continue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlgInsurance == DialogResult.Yes)
                        {


                            _ClaimStructure = new ClaimStructure(_DatabaseConnectionString);
                            for (int z = 1; C1NewClaim.Rows.Count > z; z++)
                            {
                                CSClaim _CSClaim = new CSClaim();
                                _CSClaim.TransactionID = _TransactionID;


                                for (int i = 1; C1MainClaim.Rows.Count > i; i++)
                                {
                                    if (C1MainClaim.GetData(i, "Split#").ToString().Trim() == z.ToString().Trim())
                                    {
                                        CSClaimLine _ClaimLine = new CSClaimLine();
                                        _ClaimLine.CPT = C1MainClaim.GetData(i, "sCPTCode").ToString();
                                        _ClaimLine.TransactionMasterDetailID = Convert.ToInt64(C1MainClaim.GetData(i, "nTransactionMasterDetailID"));
                                        _ClaimLine.TransactionDetailID = Convert.ToInt64(C1MainClaim.GetData(i, "nTransactionDetailID"));

                                        _CSClaim.CSClaimLines.Add(_ClaimLine);
                                        if (_CSClaim.CSClaimLines.Count == 1)
                                        {
                                            _CSClaim.TransactionMasterID = Convert.ToInt64(C1MainClaim.GetData(i, "nTransactionDetailID"));
                                        }
                                    }
                                }
                                _ClaimStructure.CSClaims.Add(_CSClaim);
                                IsModified = true;
                            }
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private void lbldx_MouseEnter(object sender, EventArgs e)
            {
                try
                {

                    label = (Label)sender;

                    if (lbldx.Tag != null && Convert.ToString(lbldx.Tag) != "")
                    {
                        string[] ostr = Convert.ToString(lbldx.Tag).Split(new Char[] { ' ' });
                        string str = string.Empty;

                        for (int iCount = 0; iCount < ostr.Length; iCount++)
                        {
                            if (iCount % 20 == 0 && iCount !=0)
                            {
                                str += Environment.NewLine + " " + ostr[iCount]+" ";
                            }
                            else
                            {
                                str += ostr[iCount] + " ";
                            }
                        }

                        if (getWidthofListItems(Convert.ToString(lbldx.Tag), lbldx) >= lbldx.Width - 30)
                        {
                            tooltip_Dx.SetToolTip(lbldx, str);
                        }
                        else
                        {
                            this.tooltip_Dx.Hide(lbldx);
                        }
                    }
                    else
                    {
                        this.tooltip_Dx.Hide(lbldx);
                    }
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
                }
            }

            private void lbldx_MouseLeave(object sender, EventArgs e)
            {
                this.tooltip_Dx.Hide(lbldx);
            }

       #endregion

        #region "C1 Grid Event"

            private void AutoSplit()
            {
                int cnt = 1, track = 1;
                for (int i = 1; C1MainClaim.Rows.Count > i; i++)
                {
                    C1MainClaim.SetData(i, Split_Col, cnt);
                    track += 1;
                    if (track > 6)
                    {
                        track = 1;
                        cnt += 1;
                    }
                }
                C1NewClaim.Select(1, 1);
                C1MainClaim.Select(1, Split_Col, 1, Split_Col);
            }

            private void C1NewClaim_RowColChange(object sender, EventArgs e)
        {
            try
            {
                if (C1NewClaim.RowSel > -1)
                {
                    if (C1NewClaim.Rows[C1NewClaim.RowSel][0] != null)
                    {
                        string SplitClaimNo = C1NewClaim.Rows[C1NewClaim.RowSel][0].ToString();
                        
                        FillSplitClaim(SplitClaimNo);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

            private void C1MainClaim_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
            {

                try
                {
                  if (e.Row > 0)
                    {
                        if (C1MainClaim.GetData(e.Row, Split_Col) != null)
                        {
                            if (C1MainClaim.GetData(e.Row, Split_Col).ToString().Contains("."))
                            {
                                C1MainClaim.SetData(e.Row, Split_Col, "");
                                // C1MainClaim.Focus();
                                C1MainClaim.Select(e.Row, Split_Col);
                                return;
                            }

                            if (C1MainClaim.GetData(e.Row, Split_Col) != null && C1MainClaim.GetData(e.Row, Split_Col).ToString() != "")
                            {

                                try
                                {
                                    if (Convert.ToInt16(C1MainClaim.GetData(e.Row, Split_Col)) == 0)
                                    {
                                        C1MainClaim.SetData(e.Row, Split_Col, "");
                                        //C1MainClaim.Focus();
                                        //C1MainClaim.Select(e.Row, Split_Col);
                                    }

                                    if (Convert.ToInt16(C1MainClaim.GetData(e.Row, Split_Col)) > C1MainClaim.Rows.Count - 1)
                                    {
                                        C1MainClaim.SetData(e.Row, Split_Col, "");
                                        // C1MainClaim.Focus();
                                        //C1MainClaim.Select(e.Row, Split_Col);
                                    }
                                }
                                catch //(Exception ex)
                                {
                                    if (e.Row > 0)
                                    {
                                        C1MainClaim.SetData(e.Row, Split_Col, "");
                                    }
                                }

                            }
                            C1MainClaim.Focus();
                            CurrentServiceRow = Convert.ToString(C1MainClaim.GetData(e.Row, 0));
                            C1MainClaim.Select(e.Row, Split_Col);
                            FillNewClaim(C1MainClaim.GetData(e.Row, Split_Col).ToString());
                        }
                    }

                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private void C1MainClaim_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
            {
                if (nonNumberEntered == true)
                {
                    e.Handled = true;
                }
            }

            private void C1MainClaim_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (nonNumberEntered == true)
                {
                    e.Handled = true;
                }

            }

            private void C1MainClaim_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                }
                try
                {

                    if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col) != null)
                    {
                        if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString().Trim() != "")
                        {
                            if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString().Contains("."))
                            {
                                C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                // C1MainClaim.Focus();
                                C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                                return;
                            }
                            if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col) != null && C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString() != "")
                            {

                                try
                                {
                                    if (Convert.ToInt16(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)) == 0)
                                    {
                                        C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                        //C1MainClaim.Focus();
                                        //C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                                    }

                                    if (Convert.ToInt16(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)) > C1MainClaim.Rows.Count - 1)
                                    {
                                        C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                        // C1MainClaim.Focus();
                                        //C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                                    }
                                }
                                catch //(Exception ex)
                                {
                                    if (C1MainClaim.RowSel > 0)
                                    {
                                        C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                    }

                                }

                            }
                            C1MainClaim.Focus();
                            CurrentServiceRow = Convert.ToString(C1MainClaim.GetData(C1MainClaim.RowSel, 0));
                            C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                            if (e.KeyCode == Keys.Enter)
                            {
                              
                                if (C1MainClaim.Rows.Count > 1)
                                {
                                    if (C1MainClaim.RowSel != C1MainClaim.Rows.Count - 1)
                                    {
                                        C1MainClaim.Select(C1MainClaim.RowSel + 1, Split_Col);
                                    }
                                }
                            }
                            if (Convert.ToString(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)).Trim() != "")
                            {
                                FillNewClaim(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private void C1MainClaim_Click(object sender, EventArgs e)
            {
                try
                {

                    if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col) != null)
                    {
                        if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString().Contains("."))
                        {
                            C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                            // C1MainClaim.Focus();
                            C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                            return;
                        }
                        if (C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col) != null && C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString() != "")
                        {

                            try
                            {
                                if (Convert.ToInt16(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)) == 0)
                                {
                                    C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                    //C1MainClaim.Focus();
                                    //C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                                }

                                if (Convert.ToInt16(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)) > C1MainClaim.Rows.Count - 1)
                                {
                                    C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                    // C1MainClaim.Focus();
                                    //C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                                }
                            }
                            catch //(Exception ex)
                            {
                                if (C1MainClaim.RowSel > 0)
                                {
                                    C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, "");
                                }
                            }

                        }
                        C1MainClaim.Focus();
                        CurrentServiceRow = Convert.ToString(C1MainClaim.GetData(C1MainClaim.RowSel, 0));
                        C1MainClaim.Select(C1MainClaim.RowSel, Split_Col);
                        if (Convert.ToString(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col)).Trim() != "")
                        {
                            FillNewClaim(C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString());
                        }
                    }

                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }

            private void C1NewClaim_MouseMove(object sender, MouseEventArgs e)
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }

            private void C1NewClaim_MouseLeave(object sender, EventArgs e)
            {
                C1SuperTooltip1.Hide();
            }

            private void C1MainClaim_KeyDownEdit(object sender, C1.Win.C1FlexGrid.KeyEditEventArgs e)
            {
                nonNumberEntered = false;


                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {

                    if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                    {

                        if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                        {
                            nonNumberEntered = true;
                        }
                    }
                }

                if (Control.ModifierKeys == Keys.Shift)
                {
                    nonNumberEntered = true;
                }

            }

            private void C1MainClaim_KeyDown(object sender, KeyEventArgs e)
            {

                nonNumberEntered = false;


                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {

                    if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                    {

                        if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                        {
                            nonNumberEntered = true;
                        }
                    }
                }

                if (Control.ModifierKeys == Keys.Shift)
                {
                    nonNumberEntered = true;
                }

            }
                                                                                                                                                                                                                                       
        #endregion

            private void C1MainClaim_KeyUpEdit(object sender, C1.Win.C1FlexGrid.KeyEditEventArgs e)
            {                
                //C1MainClaim.FinishEditing();
                //string test = C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString();
                //C1MainClaim.SetData(C1MainClaim.RowSel, Split_Col, test);
                //C1MainClaim.StartEditing();
                //C1MainClaim.Refresh();
               

                //string test = C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString();
            }

            

            //private void C1MainClaim_TextChanged(object sender, EventArgs e)
            //{
            //    string test =C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString();
            //}

            //private void C1MainClaim_ChangeEdit(object sender, EventArgs e)
            //{
            //    string test = C1MainClaim.GetData(C1MainClaim.RowSel, Split_Col).ToString();
            //}
            //TextBox t1 = new TextBox();
            //private void C1MainClaim_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
            //{
            //    C1MainClaim.Editor = t1;
            //}
                        
    }
}
