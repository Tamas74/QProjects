using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAccountsV2;

namespace gloBilling
{
    public partial class frmCleargagePaymentVoid : Form
    {

        #region "Varaible Declaration"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = String.Empty;
        private Int64 ngCleargageFileID = 0;

        DataTable dtPAccountID = null;
        int CleargageVoidlistRowCount = 0;
        bool OnAutoDistrubute = false;
        
        #endregion

        #region "C1CleargageVoid Constants"

        private const int Col_sAccountNo = 0;
        private const int Col_PatientName = 1;
        private const int Col_sPaymentMethod = 2;
        private const int Col_Action = 3;
        private const int Col_CreditEventType = 4;
        private const int Col_RefernceNo = 5;
        private const int Col_EncounterID = 6;
        private const int Col_CheckAmount = 7;
        private const int Col_AvailableAmount = 8;
        private const int Col_OriginalPayment = 9;
        private const int Col_Allocation = 10;
        private const int Col_PrevPaid = 11;
        private const int Col_Refund = 12;
        private const int Col_TotalRefund = 13;

        private const int Col_nPAccountID = 14;
        private const int Col_nGuarantorID = 15;
        private const int Col_nAccountPatientID = 16;
        private const int Col_nCleargageFileID = 17;
        private const int Col_nPatientID = 18;
        private const int Col_nCloseDate = 19;
        private const int Col_sCGTransactionID = 20;
        private const int Col_sCGOriginalTransactionID = 21;
        private const int Col_dtTimeStamp = 22;
        private const int Col_trntype = 23;
        private const int Col_sAccountType = 24;
        private const int Col_sAccountNumber = 25;
        private const int Col_sPatientCode = 26;
        private const int Col_nEOBPaymentID = 27;
        private const int Col_dtCreatedDateTime = 28;
        private const int Col_nCredits_RefID = 29;
        private const int Col_nReserveID = 30;
        private const int Col_nHEncounterID = 31;
        private const int Col_DistinctAccountNo = 32;
        private const int Col_HRefernceNo = 33;
        private const int Col_HsAccountNo = 34;
        private const int Col_HsPaymentMethod = 35;
        private const int Col_HAction = 36;
        private const int Col_HPlanID = 37;
        private const int Col_HCreditEventType = 38;
        private const int Col_HPatientName = 39;
        private const int Col_CGCreditID = 40;
        
        private const int Col_Count = 41;

        #endregion

        #region "Constructors"

        public frmCleargagePaymentVoid()
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
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmCleargagePaymentVoid(Int64 nCleargageFileID)
        {
            ngCleargageFileID = nCleargageFileID;
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
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                C1CleargageVoid.Select();
                this.Close();
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void frmCleargagePaymentVoid_Load(object sender, EventArgs e)
        {            
            try
            {
                LoadCleargagePaymentVoidList();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void rb_ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_ShowAll.Checked == true)
                {
                    #region "Save Button Text/Style Change"

                    tsb_Next.Visible = false;
                    tsb_Save.Text = "&Save";
                    tsb_Save.ToolTipText = "Save";
                    rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Bold);
                    LoadCleargagePaymentVoidList();

                    #endregion
                }
                else
                {
                    #region "Save Button Text/Style Change"

                    rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Regular);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void rb_ShowOneByOne_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_ShowOneByOne.Checked == true)
                {
                    #region "Save Button Text/Style Change"

                    CleargageVoidlistRowCount = 0;
                    tsb_Next.Visible = true;
                    tsb_Save.Text = "&Save && Next";
                    tsb_Save.ToolTipText = "Save & Next";
                    rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Bold);
                    LoadCleargagePaymentVoidOneByOne();

                    #endregion
                }
                else
                {
                    #region "Save Button Text/Style Change"

                    rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Regular);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void frmCleargagePaymentVoid_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAccountsUsingAmount();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Next_Click(object sender, EventArgs e)
        {
            try
            {
                CleargageVoidlistRowCount++;
                LoadCleargagePaymentVoidOneByOne();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (rb_ShowAll.Checked == true)
                {
                    LoadCleargagePaymentVoidList();
                }
                else 
                {
                    LoadCleargagePaymentVoidOneByOne();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }            
        }
        
        private void DesignCleargagePaymentVoid()
        {
            try
            {
                gloC1FlexStyle.Style(C1CleargageVoid, true);
                C1CleargageVoid.AllowSorting = AllowSortingEnum.None;
                C1CleargageVoid.SelectionMode = SelectionModeEnum.Cell;

                C1CleargageVoid.Rows.Count = 1;
                C1CleargageVoid.Cols.Count = Col_Count;

                #region "Headers"
                                
                C1CleargageVoid.SetData(0, Col_sAccountNo, "Acc. #");
                C1CleargageVoid.SetData(0, Col_PatientName, "Patient");
                C1CleargageVoid.SetData(0, Col_sPaymentMethod, "Pay. Method");
                C1CleargageVoid.SetData(0, Col_Action, "Action");
                C1CleargageVoid.SetData(0, Col_CreditEventType, "Event Type");
                C1CleargageVoid.SetData(0, Col_RefernceNo, "Reference #");
                C1CleargageVoid.SetData(0, Col_EncounterID, "EncounterID");
                C1CleargageVoid.SetData(0, Col_CheckAmount, "Amount");
                C1CleargageVoid.SetData(0, Col_AvailableAmount, "Available");
                C1CleargageVoid.SetData(0, Col_OriginalPayment, "Original Payment");
                C1CleargageVoid.SetData(0, Col_Allocation, "Allocation");
                C1CleargageVoid.SetData(0, Col_PrevPaid, "Prev Paid");
                C1CleargageVoid.SetData(0, Col_Refund, "Refund");
                C1CleargageVoid.SetData(0, Col_TotalRefund, "Total Refund");
                

                C1CleargageVoid.SetData(0, Col_nPAccountID, "nPAccountID");
                C1CleargageVoid.SetData(0, Col_nGuarantorID, "nGuarantorID");
                C1CleargageVoid.SetData(0, Col_nAccountPatientID, "nAccountPatientID");
                C1CleargageVoid.SetData(0, Col_nCleargageFileID, "nCleargageFileID");
                C1CleargageVoid.SetData(0, Col_nPatientID, "nPatientID");
                C1CleargageVoid.SetData(0, Col_nCloseDate, "nCloseDate");
                C1CleargageVoid.SetData(0, Col_sCGTransactionID, "sCGTransactionID");
                C1CleargageVoid.SetData(0, Col_sCGOriginalTransactionID, "sCGOriginalTransactionID");
                C1CleargageVoid.SetData(0, Col_dtTimeStamp, "dtTimeStamp");
                C1CleargageVoid.SetData(0, Col_trntype, "trntype");
                C1CleargageVoid.SetData(0, Col_sAccountType, "sAccountType");
                C1CleargageVoid.SetData(0, Col_sAccountNumber, "sAccountNumber");
                C1CleargageVoid.SetData(0, Col_sPatientCode, "sPatientCode");
                C1CleargageVoid.SetData(0, Col_nEOBPaymentID, "nEOBPaymentID");
                C1CleargageVoid.SetData(0, Col_dtCreatedDateTime, "dtCreatedDateTime");
                C1CleargageVoid.SetData(0, Col_nCredits_RefID, "nCredits_RefID");
                C1CleargageVoid.SetData(0, Col_nReserveID, "nReserveID");
                C1CleargageVoid.SetData(0, Col_nHEncounterID, "nHEncounterID");
                C1CleargageVoid.SetData(0, Col_DistinctAccountNo, "DistinctAccountNo");
                C1CleargageVoid.SetData(0, Col_HsAccountNo, "HsAccountNo");
                C1CleargageVoid.SetData(0, Col_HRefernceNo, "HRefernceNo");
                C1CleargageVoid.SetData(0, Col_HsPaymentMethod, "HsPaymentMethod");
                C1CleargageVoid.SetData(0, Col_HAction, "HAction");
                C1CleargageVoid.SetData(0, Col_HPlanID, "HPlanID");
                C1CleargageVoid.SetData(0, Col_HCreditEventType, "HCreditEventType");
                C1CleargageVoid.SetData(0, Col_HPatientName, "HPatientName");
                C1CleargageVoid.SetData(0, Col_CGCreditID, "CGCreditID");

                #endregion

                #region "Visiblility"

                C1CleargageVoid.Cols[Col_sAccountNo].Visible = true;
                C1CleargageVoid.Cols[Col_PatientName].Visible = true;
                C1CleargageVoid.Cols[Col_sPaymentMethod].Visible = true;
                C1CleargageVoid.Cols[Col_Action].Visible = true;
                C1CleargageVoid.Cols[Col_CreditEventType].Visible = true;
                C1CleargageVoid.Cols[Col_RefernceNo].Visible = true;
                C1CleargageVoid.Cols[Col_EncounterID].Visible = true;                
                C1CleargageVoid.Cols[Col_CheckAmount].Visible = true;
                C1CleargageVoid.Cols[Col_AvailableAmount].Visible = false;
                C1CleargageVoid.Cols[Col_OriginalPayment].Visible = true;
                C1CleargageVoid.Cols[Col_Allocation].Visible = true;
                C1CleargageVoid.Cols[Col_PrevPaid].Visible = true;
                C1CleargageVoid.Cols[Col_Refund].Visible = true;
                C1CleargageVoid.Cols[Col_TotalRefund].Visible = false;

                C1CleargageVoid.Cols[Col_nPAccountID].Visible = false;
                C1CleargageVoid.Cols[Col_nGuarantorID].Visible = false;
                C1CleargageVoid.Cols[Col_nAccountPatientID].Visible = false;
                C1CleargageVoid.Cols[Col_nCleargageFileID].Visible = false;
                C1CleargageVoid.Cols[Col_nPatientID].Visible = false;
                C1CleargageVoid.Cols[Col_nCloseDate].Visible = false;
                C1CleargageVoid.Cols[Col_sCGTransactionID].Visible = false;
                C1CleargageVoid.Cols[Col_sCGOriginalTransactionID].Visible = false;
                C1CleargageVoid.Cols[Col_dtTimeStamp].Visible = false;
                C1CleargageVoid.Cols[Col_trntype].Visible = false;
                C1CleargageVoid.Cols[Col_sAccountType].Visible = false;
                C1CleargageVoid.Cols[Col_sAccountNumber].Visible = false;
                C1CleargageVoid.Cols[Col_sPatientCode].Visible = false;
                C1CleargageVoid.Cols[Col_nEOBPaymentID].Visible = false;                
                C1CleargageVoid.Cols[Col_dtCreatedDateTime].Visible = false;
                C1CleargageVoid.Cols[Col_nCredits_RefID].Visible = false;
                C1CleargageVoid.Cols[Col_nReserveID].Visible = false;
                C1CleargageVoid.Cols[Col_nHEncounterID].Visible = false;
                C1CleargageVoid.Cols[Col_DistinctAccountNo].Visible = false;
                C1CleargageVoid.Cols[Col_HsAccountNo].Visible = false;
                C1CleargageVoid.Cols[Col_HRefernceNo].Visible = false;
                C1CleargageVoid.Cols[Col_HsPaymentMethod].Visible = false;
                C1CleargageVoid.Cols[Col_HAction].Visible = false;
                C1CleargageVoid.Cols[Col_HPlanID].Visible = false;
                C1CleargageVoid.Cols[Col_HCreditEventType].Visible = false;
                C1CleargageVoid.Cols[Col_HPatientName].Visible = false;
                C1CleargageVoid.Cols[Col_CGCreditID].Visible = false;
                

                #endregion

                #region "Width"

                C1CleargageVoid.Cols[Col_sAccountNo].Width = 65;
                C1CleargageVoid.Cols[Col_PatientName].Width = 130;
                C1CleargageVoid.Cols[Col_sPaymentMethod].Width = 80;
                C1CleargageVoid.Cols[Col_Action].Width = 60;
                C1CleargageVoid.Cols[Col_CreditEventType].Width = 75;
                C1CleargageVoid.Cols[Col_RefernceNo].Width = 80;
                C1CleargageVoid.Cols[Col_EncounterID].Width = 150;
                C1CleargageVoid.Cols[Col_CheckAmount].Width = 75;
                C1CleargageVoid.Cols[Col_AvailableAmount].Width = 0;
                C1CleargageVoid.Cols[Col_OriginalPayment].Width = 285;
                C1CleargageVoid.Cols[Col_Allocation].Width = 105;
                C1CleargageVoid.Cols[Col_PrevPaid].Width = 75;
                C1CleargageVoid.Cols[Col_Refund].Width = 75;
                C1CleargageVoid.Cols[Col_TotalRefund].Width = 0;

                C1CleargageVoid.Cols[Col_nPAccountID].Width = 0;
                C1CleargageVoid.Cols[Col_nGuarantorID].Width = 0;
                C1CleargageVoid.Cols[Col_nAccountPatientID].Width = 0;
                C1CleargageVoid.Cols[Col_nCleargageFileID].Width = 0;
                C1CleargageVoid.Cols[Col_nPatientID].Width = 0;
                C1CleargageVoid.Cols[Col_nCloseDate].Width = 0;
                C1CleargageVoid.Cols[Col_sCGTransactionID].Width = 0;
                C1CleargageVoid.Cols[Col_sCGOriginalTransactionID].Width = 0;
                C1CleargageVoid.Cols[Col_dtTimeStamp].Width = 0;
                C1CleargageVoid.Cols[Col_trntype].Width = 0;
                C1CleargageVoid.Cols[Col_sAccountType].Width = 0;
                C1CleargageVoid.Cols[Col_sAccountNumber].Width = 0;
                C1CleargageVoid.Cols[Col_sPatientCode].Width = 0;
                C1CleargageVoid.Cols[Col_nEOBPaymentID].Width = 0;
                C1CleargageVoid.Cols[Col_dtCreatedDateTime].Width = 0;
                C1CleargageVoid.Cols[Col_nCredits_RefID].Width = 0;
                C1CleargageVoid.Cols[Col_nReserveID].Width = 0;
                C1CleargageVoid.Cols[Col_nHEncounterID].Width = 0;
                C1CleargageVoid.Cols[Col_DistinctAccountNo].Width = 0;
                C1CleargageVoid.Cols[Col_HsAccountNo].Width = 0;
                C1CleargageVoid.Cols[Col_HRefernceNo].Width = 0;
                C1CleargageVoid.Cols[Col_HsPaymentMethod].Width = 0;
                C1CleargageVoid.Cols[Col_HAction].Width = 0;
                C1CleargageVoid.Cols[Col_HPlanID].Width = 0;
                C1CleargageVoid.Cols[Col_HCreditEventType].Width = 0;
                C1CleargageVoid.Cols[Col_HPatientName].Width = 0;
                C1CleargageVoid.Cols[Col_CGCreditID].Width = 0;

                #endregion

                #region "Text Alignment"

                C1CleargageVoid.Cols[Col_sAccountNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_PatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sPaymentMethod].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_Action].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_CreditEventType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_RefernceNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_EncounterID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_CheckAmount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1CleargageVoid.Cols[Col_AvailableAmount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1CleargageVoid.Cols[Col_OriginalPayment].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_Allocation].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_PrevPaid].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1CleargageVoid.Cols[Col_Refund].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1CleargageVoid.Cols[Col_TotalRefund].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                C1CleargageVoid.Cols[Col_nPAccountID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nGuarantorID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nAccountPatientID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nCleargageFileID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nPatientID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nCloseDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sCGTransactionID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sCGOriginalTransactionID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_dtTimeStamp].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_trntype].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sAccountType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sAccountNumber].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_sPatientCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nEOBPaymentID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_dtCreatedDateTime].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nCredits_RefID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nReserveID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_nHEncounterID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_DistinctAccountNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HsAccountNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HRefernceNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HsPaymentMethod].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HAction].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HPlanID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HCreditEventType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_HPatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CleargageVoid.Cols[Col_CGCreditID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region "Style"

                Font Font_CellStyle = gloGlobal.clsgloFont.gFont_SMALL_BOLD;

                #region "Currency Style"

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;
                try
                {
                    if (C1CleargageVoid.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1CleargageVoid.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1CleargageVoid.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = Font_CellStyle;
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }
                }
                catch
                {
                    csCurrencyStyle = C1CleargageVoid.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = Font_CellStyle;
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }

                #region "Set Style"

                C1CleargageVoid.Cols[Col_CheckAmount].Style = csCurrencyStyle;
                C1CleargageVoid.Cols[Col_AvailableAmount].Style = csCurrencyStyle;
                C1CleargageVoid.Cols[Col_PrevPaid].Style = csCurrencyStyle;

                #endregion

                #endregion

                #region "Editable Currency Style"

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;
                try
                {
                    if (C1CleargageVoid.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = C1CleargageVoid.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = C1CleargageVoid.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }
                }
                catch
                {
                    csEditableCurrencyStyle = C1CleargageVoid.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;
                }

                #endregion

                #region "Claim Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;
                try
                {
                    if (C1CleargageVoid.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = C1CleargageVoid.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = C1CleargageVoid.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);

                        csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }
                }
                catch
                {
                    csClaimRowStyle = C1CleargageVoid.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }

                #endregion

                #region "Claim Service Line Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimServiceLineRowStyle;
                try
                {
                    if (C1CleargageVoid.Styles.Contains("cs_ClaimServiceRowStyle"))
                    {
                        csClaimServiceLineRowStyle = C1CleargageVoid.Styles["cs_ClaimServiceRowStyle"];
                    }
                    else
                    {
                        csClaimServiceLineRowStyle = C1CleargageVoid.Styles.Add("cs_ClaimServiceRowStyle");
                        csClaimServiceLineRowStyle.DataType = typeof(System.String);

                        csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont;
                        csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                    }
                }
                catch
                {
                    csClaimServiceLineRowStyle = C1CleargageVoid.Styles.Add("cs_ClaimServiceRowStyle");
                    csClaimServiceLineRowStyle.DataType = typeof(System.String);
                    csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont;
                    csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                }

                #endregion

                #region "Void / Reject"

                C1.Win.C1FlexGrid.CellStyle csRowStyleForVoid;
                try
                {
                    if (C1CleargageVoid.Styles.Contains("cs_RowStyleForVoid"))
                    {
                        csRowStyleForVoid = C1CleargageVoid.Styles["cs_RowStyleForVoid"];
                    }
                    else
                    {
                        csRowStyleForVoid = C1CleargageVoid.Styles.Add("cs_RowStyleForVoid");
                        csRowStyleForVoid.DataType = typeof(System.String);

                        csRowStyleForVoid.Font = gloGlobal.clsgloFont.gFont;
                        csRowStyleForVoid.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csRowStyleForVoid = C1CleargageVoid.Styles.Add("cs_RowStyleForVoid");
                    csRowStyleForVoid.DataType = typeof(System.String);
                    csRowStyleForVoid.Font = gloGlobal.clsgloFont.gFont;
                    csRowStyleForVoid.BackColor = Color.FromArgb(229, 224, 236);
                }

                #endregion

                #endregion

                #region "Allow Editing"

                C1CleargageVoid.Cols[Col_sAccountNo].AllowEditing = false;
                C1CleargageVoid.Cols[Col_PatientName].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sPaymentMethod].AllowEditing = false;
                C1CleargageVoid.Cols[Col_Action].AllowEditing = false;
                C1CleargageVoid.Cols[Col_CreditEventType].AllowEditing = false;
                C1CleargageVoid.Cols[Col_RefernceNo].AllowEditing = false;
                C1CleargageVoid.Cols[Col_EncounterID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_CheckAmount].AllowEditing = false;
                C1CleargageVoid.Cols[Col_AvailableAmount].AllowEditing = false;
                C1CleargageVoid.Cols[Col_OriginalPayment].AllowEditing = false;
                C1CleargageVoid.Cols[Col_Allocation].AllowEditing = false;
                C1CleargageVoid.Cols[Col_PrevPaid].AllowEditing = false;
                C1CleargageVoid.Cols[Col_Refund].AllowEditing = true;
                C1CleargageVoid.Cols[Col_TotalRefund].AllowEditing = true;

                C1CleargageVoid.Cols[Col_nPAccountID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nGuarantorID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nAccountPatientID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nCleargageFileID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nPatientID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nCloseDate].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sCGTransactionID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sCGOriginalTransactionID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_dtTimeStamp].AllowEditing = false;
                C1CleargageVoid.Cols[Col_trntype].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sAccountType].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sAccountNumber].AllowEditing = false;
                C1CleargageVoid.Cols[Col_sPatientCode].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nEOBPaymentID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_dtCreatedDateTime].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nCredits_RefID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nReserveID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_nHEncounterID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_DistinctAccountNo].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HsAccountNo].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HRefernceNo].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HsPaymentMethod].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HAction].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HPlanID].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HCreditEventType].AllowEditing = false;
                C1CleargageVoid.Cols[Col_HPatientName].AllowEditing = false;
                C1CleargageVoid.Cols[Col_CGCreditID].AllowEditing = false;

                #endregion

                C1CleargageVoid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1CleargageVoid.ExtendLastCol = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void LoadCleargagePaymentVoidList()
        {
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;
            DataTable dtCleargagePaymentVoidList = null;
            try
            {                
                OnAutoDistrubute = true;
                oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                dtCleargagePaymentVoidList = oClsCleargagePaymentPosting.GetCleargagePaymentList_Correction(ngCleargageFileID, 0, "");
                DesignCleargagePaymentVoid();                    

                #region "Get AccountID from List

                dtPAccountID = new DataTable();
                dtPAccountID.Columns.Add("nPAccountID");
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dtPAccountID.Columns["nPAccountID"];
                dtPAccountID.PrimaryKey = keyColumns;
                for (int i = 0; i < dtCleargagePaymentVoidList.Rows.Count; i++)
                {
                    if (dtPAccountID.Rows.Count == 0)
                    {
                        dtPAccountID.Rows.Add(Convert.ToInt64(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                    }
                    else
                    {
                        if (!dtPAccountID.Rows.Contains(Convert.ToInt64(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"])))
                        {
                            dtPAccountID.Rows.Add(Convert.ToInt64(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                        }
                    }
                }

                #endregion

                if (dtCleargagePaymentVoidList != null && dtCleargagePaymentVoidList.Rows.Count > 0)
                {
                    C1CleargageVoid.DataSource = null;
                    //DesignCleargagePaymentVoid();                    

                    #region "Bind Data"

                    var Accounts = new List<KeyValuePair<string, string>>();

                    for (int i = 0; i < dtCleargagePaymentVoidList.Rows.Count; i++)
                    {
                        if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]), Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]))))
                        {
                            #region "Master Data"

                            C1CleargageVoid.Rows.Add();
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_PatientName, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["PatientName"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_Action, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CreditEventType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_RefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_EncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CheckAmount, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_AvailableAmount, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]));
                            
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPAccountID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nGuarantorID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nGuarantorID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nAccountPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nAccountPatientID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCleargageFileID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCleargageFileID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPatientID"]));                            
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCloseDate, Convert.ToDateTime(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]).ToString("MM/dd/yyyy"));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGTransactionID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGOriginalTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGOriginalTransactionID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtTimeStamp, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtTimeStamp"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_trntype, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["trntype"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountType"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNumber, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNumber"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPatientCode, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPatientCode"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nEOBPaymentID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nEOBPaymentID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtCreatedDateTime, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtCreatedDateTime"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCredits_RefID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCredits_RefID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nReserveID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nReserveID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nHEncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_DistinctAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HAction, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HRefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CGCreditID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCGCreditID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPlanID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPlanID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));

                            Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]), Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"])));

                            #endregion

                            #region "Style For Master Data"

                            C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].Style = C1CleargageVoid.Styles["cs_ClaimRowStyle"];                            
                            C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].AllowEditing = false;

                            #endregion
                        }

                        #region "Dont add sub Line for Event Type Void / Reject"

                        if (Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]).ToUpper() == Convert.ToString(CreditEventType.VOID))
                        {
                            continue;
                        }

                        #endregion

                        #region "Sub Data"

                        C1CleargageVoid.Rows.Add();
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_OriginalPayment, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Original Payment"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_Allocation, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Allocation"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_PrevPaid, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Amount"]));
                        
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPAccountID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nGuarantorID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nGuarantorID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nAccountPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nAccountPatientID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCleargageFileID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCleargageFileID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPatientID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCloseDate, Convert.ToDateTime(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]).ToString("MM/dd/yyyy"));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGTransactionID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGOriginalTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGOriginalTransactionID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtTimeStamp, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtTimeStamp"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_trntype, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["trntype"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountType"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNumber, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNumber"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPatientCode, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPatientCode"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nEOBPaymentID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nEOBPaymentID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtCreatedDateTime, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtCreatedDateTime"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCredits_RefID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCredits_RefID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nReserveID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nReserveID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nHEncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_DistinctAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));                        
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HRefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HAction, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPlanID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPlanID"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HCreditEventType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPatientName, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["PatientName"]));
                        C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CGCreditID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCGCreditID"]));

                        #endregion

                        #region "Style For Sub Data"

                        C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].Style = C1CleargageVoid.Styles["cs_ClaimServiceRowStyle"];
                        C1CleargageVoid.SetCellStyle(C1CleargageVoid.Rows.Count - 1, Col_Refund, "cs_EditableCurrencyStyle");

                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally 
            {
                OnAutoDistrubute = false;
                if (dtCleargagePaymentVoidList != null) 
                {
                    dtCleargagePaymentVoidList.Dispose();
                    dtCleargagePaymentVoidList = null;
                }

                if (oClsCleargagePaymentPosting != null)
                {
                    oClsCleargagePaymentPosting.Dispose();
                    oClsCleargagePaymentPosting = null;
                }                
            }
        }

        private void LoadCleargagePaymentVoidOneByOne()
        {
            Int64 nPAccountID = 0;
            DataTable dtCleargagePaymentVoidList = null;
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;
            try
            {
                C1CleargageVoid.Select();
                OnAutoDistrubute = true;
                oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                                
                if (rb_ShowAll.Checked == false)
                {
                    if (dtPAccountID != null && dtPAccountID.Rows.Count > 0)
                    {
                        if (dtPAccountID.Rows.Count > 0 && dtPAccountID.Rows.Count > CleargageVoidlistRowCount)
                        {
                            nPAccountID = Convert.ToInt64(dtPAccountID.Rows[CleargageVoidlistRowCount]["nPAccountID"]);
                        }
                        else
                        {
                            tsb_Next.Enabled = false;
                            if (dtPAccountID.Rows.Count > 0 && tsb_Next.Enabled == false)
                            {
                                MessageBox.Show("You have gone through all the accounts. List will be refreshed for all the accounts.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCleargagePaymentVoidList();
                                rb_ShowAll.Checked = true;
                                rb_ShowOneByOne.Checked = false;
                            }
                            return;
                        }
                    }

                    dtCleargagePaymentVoidList = oClsCleargagePaymentPosting.GetCleargagePaymentList_Correction(ngCleargageFileID, nPAccountID, "");
                    DesignCleargagePaymentVoid();                    

                    #region "Bind Data"

                    var Accounts = new List<KeyValuePair<string, string>>();

                    for (int i = 0; i < dtCleargagePaymentVoidList.Rows.Count; i++)
                    {
                        if (nPAccountID == Convert.ToInt64(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]))
                        {
                            if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]), Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]))))
                            {
                                #region "Master Data"

                                C1CleargageVoid.Rows.Add();
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_PatientName, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["PatientName"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_Action, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CreditEventType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_RefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_EncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CheckAmount, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_AvailableAmount, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]));

                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPAccountID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nGuarantorID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nGuarantorID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nAccountPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nAccountPatientID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCleargageFileID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCleargageFileID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPatientID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCloseDate, Convert.ToDateTime(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]).ToString("MM/dd/yyyy"));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGTransactionID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGOriginalTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGOriginalTransactionID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtTimeStamp, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtTimeStamp"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_trntype, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["trntype"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountType"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNumber, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNumber"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPatientCode, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPatientCode"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nEOBPaymentID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nEOBPaymentID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtCreatedDateTime, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtCreatedDateTime"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCredits_RefID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCredits_RefID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nReserveID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nReserveID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nHEncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_DistinctAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HAction, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HRefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CGCreditID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCGCreditID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPlanID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPlanID"]));
                                C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));

                                Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]), Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"])));

                                #endregion

                                #region "Style For Master Data"

                                C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].Style = C1CleargageVoid.Styles["cs_ClaimRowStyle"];
                                //C1CleargageVoid.SetCellStyle(C1CleargageVoid.Rows.Count - 1, Col_TotalRefund, "cs_MasterEditableCurrencyStyle");
                                C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].AllowEditing = false;
                                                                
                                #endregion
                            }

                            #region "Dont add sub Line for Event Type Void / Reject"

                            if (Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]).ToUpper() == Convert.ToString(CreditEventType.VOID))
                            {
                                continue;
                            }

                            #endregion

                            #region "Sub Data"

                            C1CleargageVoid.Rows.Add();
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_OriginalPayment, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Original Payment"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_Allocation, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Allocation"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_PrevPaid, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Amount"]));

                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPAccountID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPAccountID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nGuarantorID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nGuarantorID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nAccountPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nAccountPatientID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCleargageFileID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCleargageFileID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nPatientID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nPatientID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCloseDate, Convert.ToDateTime(dtCleargagePaymentVoidList.Rows[i]["nCloseDate"]).ToString("MM/dd/yyyy"));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGTransactionID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sCGOriginalTransactionID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCGOriginalTransactionID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtTimeStamp, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtTimeStamp"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_trntype, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["trntype"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountType"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sAccountNumber, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNumber"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_sPatientCode, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPatientCode"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nEOBPaymentID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nEOBPaymentID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_dtCreatedDateTime, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["dtCreatedDateTime"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nCredits_RefID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCredits_RefID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nReserveID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nReserveID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_nHEncounterID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_DistinctAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["CheckAmount"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["EncounterID"]) + "_" + Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));                            
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsAccountNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sAccountNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HRefernceNo, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["RefernceNo"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HsPaymentMethod, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPaymentMethod"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HAction, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["Action"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPlanID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sPlanID"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HCreditEventType, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["sCreditEventType"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_HPatientName, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["PatientName"]));
                            C1CleargageVoid.SetData(C1CleargageVoid.Rows.Count - 1, Col_CGCreditID, Convert.ToString(dtCleargagePaymentVoidList.Rows[i]["nCGCreditID"]));

                            #endregion

                            #region "Style For Sub Data"

                            C1CleargageVoid.Rows[C1CleargageVoid.Rows.Count - 1].Style = C1CleargageVoid.Styles["cs_ClaimServiceRowStyle"];
                            C1CleargageVoid.SetCellStyle(C1CleargageVoid.Rows.Count - 1, Col_Refund, "cs_EditableCurrencyStyle");

                            #endregion
                        }
                    }

                    #endregion
                }

                if (dtPAccountID != null)
                {
                    if (dtPAccountID.Rows.Count == CleargageVoidlistRowCount + 1)
                    {
                        tsb_Next.Enabled = false;
                        tsb_Save.Text = "&Save";
                        tsb_Save.ToolTipText = "Save";
                    }
                    else
                    {
                        tsb_Next.Enabled = true;
                        tsb_Save.Text = "&Save && Next";
                        tsb_Save.ToolTipText = "Save & Next";
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                OnAutoDistrubute = false;
                if (dtCleargagePaymentVoidList != null)
                {
                    dtCleargagePaymentVoidList.Dispose();
                    dtCleargagePaymentVoidList = null;
                }

                if (oClsCleargagePaymentPosting != null)
                {
                    oClsCleargagePaymentPosting.Dispose();
                    oClsCleargagePaymentPosting = null;
                }  
            }

        }

        private void tsb_AutoDistributeCleargageVoidPayment_Click(object sender, EventArgs e)
        {
            try
            {
                C1CleargageVoid.Select();                
                if (C1CleargageVoid.Rows.Count > 0)
                {                    
                    for (int i = 1; i < C1CleargageVoid.Rows.Count; i++)
                    {
                        Int64 _PAccountID = 0;
                        decimal dAvailablePayment = 0;
                        decimal dLineamt = 0;
                        string dtCloseDate = string.Empty;
                        string sDistinctAccountNo = string.Empty;
                        OnAutoDistrubute = true;

                        #region "Auto Distributte the Check Amount"

                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_CheckAmount)) != null && Convert.ToString(C1CleargageVoid.GetData(i, Col_AvailableAmount)) != "" && Convert.ToString(C1CleargageVoid.GetData(i, Col_AvailableAmount)) != "") 
                        {
                            _PAccountID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPAccountID));
                            dAvailablePayment = Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_AvailableAmount));
                            sDistinctAccountNo = Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo));

                            if (Convert.ToString(C1CleargageVoid.GetData(i, Col_nCloseDate)) != "")
                            {
                                dtCloseDate = Convert.ToDateTime(C1CleargageVoid.GetData(i, Col_nCloseDate)).ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                dtCloseDate = "";
                            }

                            for (int j = 1; j < C1CleargageVoid.Rows.Count; j++) 
                            {
                                if (_PAccountID == Convert.ToInt64(C1CleargageVoid.GetData(j, Col_nPAccountID)) && dtCloseDate == Convert.ToString(C1CleargageVoid.GetData(j, Col_nCloseDate)) && Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_Refund)) == 0 && Convert.ToString(C1CleargageVoid.GetData(j, Col_PrevPaid)) != "" && sDistinctAccountNo == Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo)))
                                {
                                    #region "Distribute Amount"

                                    if (dAvailablePayment == 0)
                                    {
                                        C1CleargageVoid.SetData(j, Col_Refund, dAvailablePayment);
                                    }
                                    if (dAvailablePayment > 0)
                                    {
                                        if (Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_PrevPaid)) > 0)
                                        {
                                            dLineamt = Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_PrevPaid));
                                        }
                                        else
                                        {
                                            dLineamt = 0;
                                        }

                                        if (dAvailablePayment > dLineamt)
                                        {
                                            dAvailablePayment = dAvailablePayment - dLineamt;
                                            C1CleargageVoid.SetData(j, Col_Refund, dLineamt);
                                        }
                                        else
                                        {
                                            C1CleargageVoid.SetData(j, Col_Refund, dAvailablePayment);
                                            dAvailablePayment = 0;
                                        }

                                    }

                                    #endregion
                                }
                            }

                            #region "Set Available Payment after Distribution"

                            if (_PAccountID == Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPAccountID)))
                            {
                                C1CleargageVoid.SetData(i, Col_AvailableAmount, dAvailablePayment);
                            }

                            #endregion
                        }

                        #endregion
                    }                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                OnAutoDistrubute = false;
            }
        }

        private void C1CleargageVoid_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (OnAutoDistrubute == false)
                {
                    for (int i = 1; i < C1CleargageVoid.Rows.Count; i++)
                    {
                        if (e.Col == Col_Refund && Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPAccountID)) == Convert.ToInt64(C1CleargageVoid.GetData(e.Row, Col_nPAccountID)) && Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)) == Convert.ToString(C1CleargageVoid.GetData(e.Row, Col_DistinctAccountNo)))
                        {
                            if (Convert.ToString(C1CleargageVoid.GetData(i, Col_nCloseDate)) == Convert.ToString(C1CleargageVoid.GetData(e.Row, Col_nCloseDate)))
                            {
                                decimal dDistributedTotalAmount = 0; 
                                decimal dLineamt = 0;
                                decimal dDistributeAmt = Convert.ToDecimal(C1CleargageVoid.GetData(e.Row, Col_Refund));                                
                                
                                if (Convert.ToDecimal(C1CleargageVoid.GetData(e.Row, Col_PrevPaid)) > 0)
                                {
                                    dLineamt = Convert.ToDecimal(C1CleargageVoid.GetData(e.Row, Col_PrevPaid));
                                }                                                              

                                #region "Validating Manually Distributed  Amount"

                                if (dDistributeAmt < 0)
                                {
                                    MessageBox.Show("Distributed refund amount cannot be negative,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    C1CleargageVoid.SetData(e.Row, Col_Refund, Convert.ToDecimal(0));

                                    #region "Calculate Available Amount"

                                    dDistributedTotalAmount = CalculateAvailableAmount(sender, e, i);
                                    if (dDistributedTotalAmount > 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) - dDistributedTotalAmount);
                                    }
                                    else if (dDistributedTotalAmount == 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)));
                                    }

                                    #endregion

                                    return;
                                }
                                if (dDistributeAmt > dLineamt)
                                {
                                    MessageBox.Show("Distributed refund amount exceeds previous paid.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    C1CleargageVoid.SetData(e.Row, Col_Refund, Convert.ToDecimal(0));

                                    #region "Calculate Available Amount"

                                    dDistributedTotalAmount = CalculateAvailableAmount(sender, e, i);
                                    if (dDistributedTotalAmount > 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) - dDistributedTotalAmount);
                                    }
                                    else if (dDistributedTotalAmount == 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)));
                                    }

                                    #endregion

                                    return;
                                }
                                if (dDistributeAmt == 0)
                                {
                                    #region "Calculate Available Amount"

                                    dDistributedTotalAmount = CalculateAvailableAmount(sender, e, i);
                                    if (dDistributedTotalAmount > 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) - dDistributedTotalAmount);
                                    }
                                    else if (dDistributedTotalAmount == 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)));
                                    }

                                    #endregion

                                    return;
                                }
                                else
                                {
                                    dDistributedTotalAmount = CalculateAvailableAmount(sender, e, i);
                                    if (dDistributedTotalAmount > Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)))
                                    {
                                        MessageBox.Show("Distributed refund amount exceeds available amount,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1CleargageVoid.SetData(e.Row, Col_Refund, Convert.ToDecimal(0));

                                        #region "Calculate Available Amount"

                                        dDistributedTotalAmount = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedTotalAmount > 0)
                                        {
                                            C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) - dDistributedTotalAmount);
                                        }
                                        else if (dDistributedTotalAmount == 0)
                                        {
                                            C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)));
                                        }

                                        #endregion

                                        return;
                                    }
                                }

                                #endregion

                                if (Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_AvailableAmount)) != Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)))
                                {
                                    #region "Calculate Available Amount"

                                    dDistributeAmt = CalculateAvailableAmount(sender, e, i);

                                    if (dDistributeAmt == 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)));
                                        return;
                                    }

                                    #endregion
                                    
                                    if (dDistributeAmt <= Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) && dDistributeAmt > 0)
                                    {
                                        C1CleargageVoid.SetData(i, Col_AvailableAmount, Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) - dDistributeAmt);
                                        return;
                                    }
                                }
                                else
                                {
                                    dDistributeAmt = Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_AvailableAmount));
                                    dDistributeAmt = dDistributeAmt - Convert.ToDecimal(C1CleargageVoid.GetData(e.Row, Col_Refund));
                                    C1CleargageVoid.SetData(i, Col_AvailableAmount, dDistributeAmt);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private decimal CalculateAvailableAmount(object sender, RowColEventArgs e, int i)
        {
            decimal dDistributedAmt = 0;
            try
            {
                for (int j = i; j < C1CleargageVoid.Rows.Count; j++)
                {
                    if (Convert.ToInt64(C1CleargageVoid.GetData(j, Col_nPAccountID)) == Convert.ToInt64(C1CleargageVoid.GetData(e.Row, Col_nPAccountID)) && Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)) == Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo)))
                    {
                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_nCloseDate)) == Convert.ToString(C1CleargageVoid.GetData(j, Col_nCloseDate)))
                        {
                            dDistributedAmt = dDistributedAmt + Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_Refund));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return dDistributedAmt;
        }

        private void C1CleargageVoid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (C1CleargageVoid.ColSel == Col_Refund)
                    {
                        if (C1CleargageVoid.GetData(C1CleargageVoid.RowSel, C1CleargageVoid.ColSel) != null)
                        {
                            C1CleargageVoid.SetData(C1CleargageVoid.RowSel, C1CleargageVoid.ColSel, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void C1CleargageVoid_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }                     
        }

        private void C1CleargageVoid_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == Col_Refund && C1CleargageVoid.GetData(e.Row, Col_AvailableAmount) != null)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void SaveCleargagePayment(List<string> Acc, String sAccountID, List<string> Accounts_AvailableNotMatched)
        {
            Int64 SelectedPaymentTrayID = 0;
            frmPaymentTransferInfo ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            //string sAccountID = string.Empty;
            string sSelectedPaymentTray = string.Empty;
            //List<string> Acc = new List<string>();
            Int64 nCleargageFileID = 0;
            String sCGTransactionID = String.Empty;
            String sCGOriginalTransactionID = String.Empty;
            String sEncounterID = String.Empty;
            String sAction = String.Empty;
            String sAccountIDNotMatched = String.Empty;

            #region "Payment Variables"

            Int64 nPatientID = 0;
            Int64 nPAccountID = 0;
            Int64 nGuarantorID = 0;
            Int64 nAccountPatientID = 0;
            Int64 nCreditRefID = 0;
            Int64 nExistingReserveCreditID = 0;
            Int64 nExistingReserveID = 0;
            Decimal dReserveRefundAmount = 0;
            Decimal dCorrectionAmount = 0;
            DateTime dtCloseDate = DateTime.MinValue;
            Int64 nPaymentTrayID = 0;
            String sPaymentTrayDesc = String.Empty;
            DateTime dtReceiptDate = DateTime.MinValue;
            String sAccountNo = String.Empty;
            Int32 nPaymentMode = 0;
            String sCreditCardType = String.Empty;
            String sCGReferenceNo = String.Empty;
            String sOutMessage = String.Empty;
            String sPatientName = String.Empty;
            String sPlanID = String.Empty;
            String sPaymentMethod = String.Empty;
            String sPatientCode = String.Empty;

            string sCGAccountNumber = string.Empty;
            Int64 nCGCreditID = 0;
            gloPatientPaymentV2 ogloPaymentPatient = new gloPatientPaymentV2();

            #endregion

            try
            {
                C1CleargageVoid.Select();

                #region Check Amount is Distributed or Not

                //if (C1CleargageVoid.Rows.Count > 1)
                //{
                //    bool IsAmountDistribute = false;
                //    for (int i = 1; i < C1CleargageVoid.Rows.Count; i++)
                //    {
                //        if (Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_Refund)) != 0)
                //        {
                //            IsAmountDistribute = true;
                //            break;
                //        }
                //        else
                //        {
                //            IsAmountDistribute = false;
                //        }
                //    }
                //    if (IsAmountDistribute == false)
                //    {
                //        MessageBox.Show("No payment has been made to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                #endregion

                #region "Accounts"

                //for (int j = 1; j < C1CleargageVoid.Rows.Count; j++)
                //{
                //    if (Convert.ToInt64(C1CleargageVoid.GetData(j, Col_nPAccountID)) != 0 && Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_AvailableAmount)) == 0 && (Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_Refund)) != 0) )
                //    {

                //        if (sAccountID != string.Empty && !Acc.Contains(Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo))))
                //        {
                //            sAccountID = sAccountID + "," + Convert.ToString(C1CleargageVoid.GetData(j, Col_nPAccountID));
                //            Acc.Add(Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo)));
                //        }
                //        else if (sAccountID == string.Empty)
                //        {
                //            sAccountID = Convert.ToString(C1CleargageVoid.GetData(j, Col_nPAccountID));
                //            Acc.Add(Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo)));
                //        }
                //    }
                //}



                #endregion
                if (Acc.Count <= 0)
                {
                    if (Accounts_AvailableNotMatched != null && Accounts_AvailableNotMatched.Count > 0)
                    {
                        foreach (object objAcc in Accounts_AvailableNotMatched)
                        {
                            sAccountID += "," + Convert.ToString(objAcc).Split('-').Last();
                        }
                        sAccountID = sAccountID.Substring(1);
                    }
                }

                #region "Payment Tray"

                if (SelectedPaymentTrayID == 0)
                {
                    ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                    ofrmPaymentTransferInfo.CleargagePosting = "CleargageCorrection";
                    ofrmPaymentTransferInfo.AccountIDs = sAccountID;
                    ofrmPaymentTransferInfo.ShowDialog();
                    if (ofrmPaymentTransferInfo.PaymentTrayID > 0)
                    {
                        sSelectedPaymentTray = ofrmPaymentTransferInfo.PaymentTrayName;
                        SelectedPaymentTrayID = ofrmPaymentTransferInfo.PaymentTrayID;
                    }
                    else
                    {
                        sSelectedPaymentTray = string.Empty;
                        SelectedPaymentTrayID = 0;
                    }
                }
                if (ofrmPaymentTransferInfo.PaymentTransferCloseDate == string.Empty || SelectedPaymentTrayID == 0)
                {
                    return;
                }

                #endregion

                foreach (object objAcc in Acc)
                {
                    gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
                    string sDistinctPAccountID = Convert.ToString(objAcc);
                    decimal dCheckAmout = 0;
                    decimal dTotalRefund = 0;

                    for (int i = 1; i < C1CleargageVoid.Rows.Count; i++)
                    {
                        nCleargageFileID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nCleargageFileID));
                        sEncounterID = Convert.ToString(C1CleargageVoid.GetData(i, Col_nHEncounterID));
                        sCGTransactionID = Convert.ToString(C1CleargageVoid.GetData(i, Col_sCGTransactionID));
                        sCGOriginalTransactionID = Convert.ToString(C1CleargageVoid.GetData(i, Col_sCGOriginalTransactionID));
                        sAction = Convert.ToString(C1CleargageVoid.GetData(i, Col_HAction));
                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)) == sDistinctPAccountID && Convert.ToString(C1CleargageVoid.GetData(i, Col_PrevPaid)) == "")
                        {
                            dCheckAmout = Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount));
                        }

                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)) == sDistinctPAccountID && (Convert.ToString(C1CleargageVoid.GetData(i, Col_PrevPaid)) != "" || (Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID))))
                        {
                            dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);
                            if (Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_Refund)) != 0 || (Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID)))
                            {
                                sPatientCode = Convert.ToString(C1CleargageVoid.GetData(i, Col_sPatientCode));
                                nPatientID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPatientID));
                                nPAccountID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPAccountID));
                                nGuarantorID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nGuarantorID));
                                nAccountPatientID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nAccountPatientID));
                                nCreditRefID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nCredits_RefID));
                                nExistingReserveCreditID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nEOBPaymentID));
                                nExistingReserveID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nReserveID));
                                nPaymentTrayID = SelectedPaymentTrayID;
                                sPaymentTrayDesc = sSelectedPaymentTray;
                                dtReceiptDate = Convert.ToDateTime(C1CleargageVoid.GetData(i, Col_dtTimeStamp));
                                sPatientName = Convert.ToString(C1CleargageVoid.GetData(i, Col_HPatientName));
                                sAccountNo = Convert.ToString(C1CleargageVoid.GetData(i, Col_HsAccountNo));
                                sCGReferenceNo = Convert.ToString(C1CleargageVoid.GetData(i, Col_HRefernceNo));
                                sPlanID=Convert.ToString(C1CleargageVoid.GetData(i, Col_HPlanID));
                                sPaymentMethod=Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod));
                                sCGAccountNumber = Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountNumber));
                                nCGCreditID = Convert.ToInt64(C1CleargageVoid.GetData(i, Col_CGCreditID));
                                

                                #region "Reserve / Correction Amount"

                                if (Convert.ToString(C1CleargageVoid.GetData(i, Col_Allocation)) != null && Convert.ToString(C1CleargageVoid.GetData(i, Col_Allocation)) != "")
                                {
                                    if (Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nReserveID)) != 0)
                                    {
                                        dReserveRefundAmount = Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_Refund));
                                        dCorrectionAmount = 0;
                                    }
                                    else if (Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nReserveID)) == 0)
                                    {
                                        dCorrectionAmount = Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_Refund));
                                        dReserveRefundAmount = 0;
                                    }
                                    else
                                    {
                                        dReserveRefundAmount = 0; dCorrectionAmount = 0;
                                    }
                                }

                                #endregion

                                #region "Payment Mode"

                                if (Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod)) != null && Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod)).Trim() != "")
                                {
                                    if (Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod)).ToUpper() == Convert.ToString(PaymentMethod.CREDIT))
                                    {
                                        nPaymentMode = gloAccountsV2.PaymentModeV2.CreditCard.GetHashCode();
                                    }
                                    else if (Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod)).ToUpper() == Convert.ToString(PaymentMethod.CASH))
                                    {
                                        nPaymentMode = gloAccountsV2.PaymentModeV2.Cash.GetHashCode();
                                    }
                                    else if (Convert.ToString(C1CleargageVoid.GetData(i, Col_HsPaymentMethod)).ToUpper() == Convert.ToString(PaymentMethod.ACH))
                                    {
                                        nPaymentMode = gloAccountsV2.PaymentModeV2.Check.GetHashCode();
                                    }
                                }

                                #endregion

                                #region "Credit Card Type"

                                if (nPaymentMode == gloAccountsV2.PaymentModeV2.CreditCard.GetHashCode())
                                {
                                    if (Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountType)) != null && Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountType)).Trim() != "")
                                    {
                                        switch (Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountType)).ToUpper())
                                        {
                                            case "VI":
                                                sCreditCardType = "Visa";
                                                break;
                                            case "MC":
                                                sCreditCardType = "Master Card";
                                                break;
                                            case "DI":
                                                sCreditCardType = "DISCOVER";
                                                break;
                                            case "AX":
                                                sCreditCardType = "American Express";
                                                break;
                                        }
                                    }
                                }

                                #endregion

                                #region "Save Payment"

                                if (nPatientID != 0 && nPAccountID != 0 && nGuarantorID != 0 && nAccountPatientID != 0 && nCreditRefID != 0 && dtCloseDate != DateTime.MinValue && nPaymentTrayID != 0 && dtReceiptDate != DateTime.MinValue && sAccountNo != "" && sCGReferenceNo != "")
                                {
                                    decimal dAmount = 0;
                                    bool bIsVoided = false;
                                    Int64 nRefundID = 0;
                                   
                                    if (Convert.ToString(C1CleargageVoid.GetData(i, Col_HCreditEventType)).ToUpper() == Convert.ToString(CreditEventType.CREDIT))
                                    {
                                        Int64 nCreditRefundID = 0;
                                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_Allocation)).ToUpper() == "REFUND TO :")
                                        {
                                            bool bIsRefunded = false;
                                            bIsRefunded = ogloPaymentPatient.IsRefunded(nCreditRefID);
                                            if (bIsRefunded)
                                            {
                                                dTotalRefund += dCorrectionAmount;
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Add, "Encounter ID : " + sEncounterID + " had already refund in system skipped from posting", nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success);
                                                //break;
                                            }

                                        }
                                        else
                                        {
                                            sOutMessage = oclsCleargagePaymentPosting.SaveCleargagePaymentList_Correction(nPatientID, nPAccountID, nGuarantorID, nAccountPatientID, nCreditRefID, nExistingReserveCreditID, nExistingReserveID, dReserveRefundAmount, dCorrectionAmount, dtCloseDate, nPaymentTrayID, sPaymentTrayDesc, dtReceiptDate, sCGAccountNumber, nPaymentMode, sCreditCardType, sCGReferenceNo, sCGOriginalTransactionID, out nCreditRefundID, out nRefundID);
                                            if (sOutMessage == "Success")
                                            {
                                                string sMessage = "Cleargage Payment for encounter IDs : " + sEncounterID;
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Add, sMessage, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success);

                                                if (nExistingReserveID != 0)
                                                {
                                                    dAmount = dReserveRefundAmount;
                                                }
                                                else
                                                {
                                                    dAmount = dCorrectionAmount;
                                                }

                                                oclsCleargagePaymentPosting.SavePaymentPostingDetails(0, sPatientCode, sPatientName, sPlanID, sCGTransactionID, sCGOriginalTransactionID, dAmount, sPaymentMethod.ToUpper(), sAction, dtReceiptDate, "", "", Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountType)).ToUpper(), sCGAccountNumber, sCGReferenceNo, nCreditRefundID, sEncounterID, nCleargageFileID, nCreditRefID, nRefundID);
                                                dTotalRefund += dAmount;

                                            }
                                            else
                                            {
                                                string sMsg = sOutMessage + " for account#: " + sAccountNo + ".";
                                                MessageBox.Show(sMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                        }

                                       
                                    }
                                    else
                                    {
                                        if (Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID))
                                        {
                                            bool bIsRefunded = false;
                                            bIsRefunded = ogloPaymentPatient.IsRefunded(nCreditRefID);
                                            if (bIsRefunded)
                                            {
                                                MessageBox.Show("Cannot void payment for account#: " + sAccountNo + " with reference #: " + sCGReferenceNo + Environment.NewLine + "Payment you are trying to void has refunds on it, void the refunds first and try again. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Save, "Cannot void payment for account#: " + sAccountNo + " with reference #: " + sCGReferenceNo, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            }
                                            if (MessageBox.Show("Payment will be voided for account #:" + sAccountNo + " with reference #:" + sCGReferenceNo + ".\nDo you want to void the payment ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {
                                                if (oclsCleargagePaymentPosting.UpdateCleargageTransaction(nCGCreditID, nCreditRefID, sCGTransactionID, sCGOriginalTransactionID, Convert.ToString(dtReceiptDate), sCGReferenceNo, true) == 0)
                                                {
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.Generate, "Update Cleargege record is fail", Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPatientID)), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                }
                                                else
                                                {
                                                    bIsVoided = VoidFeeCredit(nCGCreditID, nCreditRefID, nCleargageFileID, sEncounterID, nPatientID, sPatientName, sPlanID, sCGTransactionID, sCGOriginalTransactionID, dAmount, sPaymentMethod, sAction, Convert.ToDateTime(dtReceiptDate), Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountType)).ToUpper(), sCGAccountNumber, sCGReferenceNo, nPAccountID, nAccountPatientID);
                                                }
                                            }
                                            else 
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Save, "NO is selected for \"Putting remaining amount into other reserve \" for account# : " + sAccountNo, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                            }
                                        }
                                    }
                                    if (dTotalRefund == dCheckAmout || bIsVoided == true)
                                    {
                                        #region "Update Line Status"

                                        oclsCleargagePaymentPosting.UpdateStatusAsPosted(sEncounterID, nCleargageFileID, sCGTransactionID, sCGOriginalTransactionID, sCGReferenceNo, sAction);
                                        string _Statusmessage = "Auto Cleargage Refund/Void file status updated for encounter ID : " + sEncounterID + " and System credit ID : " + nRefundID;
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Modify, _Statusmessage, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        #endregion
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                }

                #region "Update File Status"

                oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(nCleargageFileID);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Modify, "Auto Cleargage Void/Refund file status updated for CleargageFileID : " + nCleargageFileID, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                #endregion

                #region "Partially Refund do allowed"
                if (Accounts_AvailableNotMatched != null && Accounts_AvailableNotMatched.Count > 0)
                {
                    foreach (object objAcc in Accounts_AvailableNotMatched)
                    {
                        if (sAccountIDNotMatched == "" || sAccountIDNotMatched == string.Empty)
                        {
                           // sAccountIDNotMatched = Convert.ToString(objAcc);
                            sAccountIDNotMatched += Convert.ToString(objAcc).Split('-').First();
                        }
                        else
                        {
                            //sAccountIDNotMatched = sAccountIDNotMatched + "," + Convert.ToString(objAcc);
                            sAccountIDNotMatched += "," + Convert.ToString(objAcc).Split('-').First();
                        }
                    }
                    MessageBox.Show("Correction refund amount not match with requested refund amount for AccountNo : " + sAccountIDNotMatched, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion
                #region "Load"

                if (rb_ShowAll.Checked == true)
                {
                    LoadCleargagePaymentVoidList();
                }
                else
                {
                    CleargageVoidlistRowCount++;
                    LoadCleargagePaymentVoidOneByOne();
                }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Add, "Cleargage Payment for encounter IDs : " + sEncounterID, nPatientID, nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (ofrmPaymentTransferInfo != null)
                { ofrmPaymentTransferInfo.Dispose(); ofrmPaymentTransferInfo = null; }
                if (oclsCleargagePaymentPosting != null)
                { oclsCleargagePaymentPosting.Dispose(); oclsCleargagePaymentPosting = null; }
            }
        }

        private void ValidateAccountsUsingAmount()
        {
            List<string> Acc = new List<string>();
            List<string> Accounts_AvailableNotMatched = new List<string>();
            String sAccountIDNotMatched = String.Empty;
            String sAccountID = String.Empty;
            Decimal dTotalAmount = 0;
            gloPatientPaymentV2 ogloPaymentPatient = new gloPatientPaymentV2();

            try
            {
                C1CleargageVoid.Select();
                for (int i = 1; i < C1CleargageVoid.Rows.Count; i++)
                {
                    if (Convert.ToString(C1CleargageVoid.GetData(i, Col_PrevPaid)) == "")
                    {
                        dTotalAmount = 0;
                        for (int j = i; j < C1CleargageVoid.Rows.Count; j++)
                        {
                            if (Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)) == Convert.ToString(C1CleargageVoid.GetData(j, Col_DistinctAccountNo)))
                            {
                                dTotalAmount = dTotalAmount + Convert.ToDecimal(C1CleargageVoid.GetData(j, Col_Refund));
                            }
                        }
                        if (Convert.ToDecimal(C1CleargageVoid.GetData(i, Col_CheckAmount)) == dTotalAmount)
                        {
                            if (sAccountID != String.Empty && !Acc.Contains(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo))))
                            {
                                sAccountID = sAccountID + "," + Convert.ToString(C1CleargageVoid.GetData(i, Col_nPAccountID));
                                Acc.Add(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)));
                            }
                            else if (sAccountID == String.Empty)
                            {
                                sAccountID = Convert.ToString(C1CleargageVoid.GetData(i, Col_nPAccountID));
                                Acc.Add(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)));
                            }
                        }
                        else
                        {
                            if (dTotalAmount == 0 && (Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID)))
                            {
                                if (sAccountID != String.Empty && !Acc.Contains(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo))))
                                {
                                    sAccountID = sAccountID + "," + Convert.ToString(C1CleargageVoid.GetData(i, Col_nPAccountID));
                                    Acc.Add(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)));
                                }
                                else if (sAccountID == String.Empty)
                                {
                                    sAccountID = Convert.ToString(C1CleargageVoid.GetData(i, Col_nPAccountID));
                                    Acc.Add(Convert.ToString(C1CleargageVoid.GetData(i, Col_DistinctAccountNo)));
                                }
                            }
                            if (dTotalAmount != 0)
                            {
                                if (!Accounts_AvailableNotMatched.Contains(Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountNo))) && !(Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(C1CleargageVoid.GetData(i, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID)))
                                {
                                    Accounts_AvailableNotMatched.Add(Convert.ToString(C1CleargageVoid.GetData(i, Col_sAccountNo))+"-"+Convert.ToInt64(C1CleargageVoid.GetData(i, Col_nPAccountID)));
                                }
                            }
                        }
                    }
                }
                
                if (Acc != null && Acc.Count > 0 || (Accounts_AvailableNotMatched != null && Accounts_AvailableNotMatched.Count > 0))
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Save, "Cleargage Payment Refund / Void Save Start ", 0, ngCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    SaveCleargagePayment(Acc, sAccountID, Accounts_AvailableNotMatched);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageVoidRefund, gloAuditTrail.ActivityType.Save, "Cleargage Payment Refund / Void Start Save End ", 0, ngCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private bool VoidFeeCredit(Int64 nCGCreditID, Int64 nCreditRefID, Int64 nCleargageFileID, string sEncounterID, Int64 nPatientID, string sPatientName, string sPlanID, string sCGTransactionID, string sCGOriginalTransactionID, Decimal Refund, string sPaymentMethod, string Action, DateTime dtTimeStamp, string sAccountType, string sAccountNumber, string sRefernceNo, Int64 nPAccountID, Int64 nAccountPatientID)
        {
            DateTime dtVoidCloseDate = DateTime.Now;
            String sVoidTrayName = String.Empty;
            String sVoidTrayCode = String.Empty;
            String sVoidNotes = String.Empty;
            Int64 nVoidTrayId = 0;
            Int64 nRetVal = 0;
            gloPatientPaymentV2 ogloPaymentPatient = new gloPatientPaymentV2();
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            DataTable _dtPAccountIds = null;
            bool IsVoidedNow = false;
            try
            {
                gloAccountsV2.frmVoidPaymentV2 ofrmVoid = new gloAccountsV2.frmVoidPaymentV2(nCreditRefID, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.OneTimePaymentBegin, "Recent Transaction start", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                #region "Void Fee"

                dtVoidCloseDate = ofrmVoid.VoidCloseDate;
                sVoidTrayName = ofrmVoid.VoidTrayName;
                nVoidTrayId = ofrmVoid.VoidTrayID;
                sVoidTrayCode = ofrmVoid.VoidTrayCode;
                sVoidNotes = "Cleargage payment voided ref# : " + sRefernceNo + " date : " + Convert.ToDateTime(dtTimeStamp).ToString("MM/dd/yyyy") + ".";
                nRetVal = ogloPaymentPatient.VoidPatientPayment(nCreditRefID, nPAccountID, nAccountPatientID, sVoidNotes, dtVoidCloseDate, nVoidTrayId, sVoidTrayName);
                oclsCleargagePaymentPosting.SaveCleargageFEECREDITTransaction(nCGCreditID, nCreditRefID, nCleargageFileID, sEncounterID, nPatientID, sPatientName, sPlanID, sCGTransactionID, sCGOriginalTransactionID, Refund, sPaymentMethod, Action, dtTimeStamp, sAccountType, sAccountNumber, sRefernceNo, Convert.ToInt64(ofrmVoid.VoidTrayID), false);

                #region "Follow Up"

                Cls_GlobalSettings.IsPaymentVoided = true;
                _dtPAccountIds = gloInsurancePaymentV2.GetPatientAccountsForPatPmtVoid(nCreditRefID);
                if (_dtPAccountIds != null && _dtPAccountIds.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtPAccountIds.Rows.Count - 1; i++)
                    {
                        Collections.CL_FollowUpCode.SetAutoAccountFollowUp(Convert.ToInt64(_dtPAccountIds.Rows[i]["nPAccountID"].ToString()), Convert.ToInt64(_dtPAccountIds.Rows[i]["nPatientID"].ToString()), dtVoidCloseDate);
                    }
                }

                IsVoidedNow = true;

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                IsVoidedNow = false;
            }
            return IsVoidedNow;
        }

        private static void SetToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                if (Convert.ToString(oGrid.GetData(nRow, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.REJECT) || Convert.ToString(oGrid.GetData(nRow, Col_CreditEventType)).ToUpper() == Convert.ToString(CreditEventType.VOID))
                                {
                                    string dText = Convert.ToString("Payment will be voided");
                                    //Added Format for Tooltip in 6031
                                    sText = dText.ToString();
                                }
                                else
                                {
                                    sText = oGrid.GetData(nRow, nCol).ToString();
                                }

                                //sText = oGrid.GetData(nRow, nCol).ToString();
                            }

                            colsize = oGrid.Cols[nCol].WidthDisplay;
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        oC1ToolTip.SetToolTip(oGrid, sText);

                        if (sText.Contains("\r\n"))
                        {
                            sText = sText.ToString().Replace("\r\n", " ");
                            oC1ToolTip.SetToolTip(oGrid, sText);

                        }
                        else if (stringsize.Width > colsize)
                        {

                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void C1CleargageVoid_MouseMove(object sender, MouseEventArgs e)
        {
            SetToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

    }
}
