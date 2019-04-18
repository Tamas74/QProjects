using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace gloBilling
{
    public class TrnCtrlColValChangeEventArg : EventArgs
    {
        public Int64 id = 0;
        public string code = "";
        public string description = "";
        public bool isdeleted = false;
        public TransactionLineColumnType oType = TransactionLineColumnType.None;
    }

    #region "New Payment Section"
    public enum EOBPaymentType
    {
        None = 0, InsuraceReserverd = 1, PatientReserved = 2, EOB = 3, InsuracePayment = 4, InsuraceRefund = 5, PatientPayment = 6,
        PatientRefund = 7, InsuranceCorrection = 8, PatientCorrection = 9
        //In 5020 PatientCorrection enum is not in use will see to it near future if ...
    }



    public enum EOBPaymentTypeAccountNo
    {
        None = 0, InsuraceReserverd = -1, PatientCopayReserved = -2, PatientAdvanceReserved = -3, PatientOtherReserved = -4,
        PatientReserved = -5, EOB = -6, InsuraceAdjuestment = -7, PatientAdjuestment = -8, InsuranceVoidPayerID = -9

    }

    public enum EOBPaymentSubType
    {
        None = 0, Insurace = 1, Copay = 2, Advance = 3, Coinsurace = 4, Dedcutiable = 5, WriteOff = 6, WithHold = 7, Patient = 8, Reserved = 9, Other = 10, TakeBack = 11, Adjuestment = 12, Correction = 13, Refund = 14, StatementNote = 15, InternalNote = 16, Charges_BillingNote = 17, Charges_StatementNote = 18, Charges_InternalNote = 19, Claim_Box19Note = 20,Claim_Box10dNote = 21
    }

    public enum EOBPaymentSign
    {
        None = 0, Payment_Credit = 1, Receipt_Debit = 2
    }

    public enum EOBPaymentAccountType
    {
        None = 0, Patient = 1, PatientInsurace = 2, ContactInsurance = 3, Reserved = 4, InsuranceCompany = 5
    }

    public enum EOBPaymentMode
    {
        None = 0,
        Cash = 1,
        Check = 2,
        MoneyOrder = 3,
        CreditCard = 4,
        EFT = 5, //Electronic Funds Transfer
        PaymentVoidReserved = 6
    }

    public enum EOBPaymentCreditLineType
    {
        MainCreditLine = 0,
        ReserveAsCreditLine = -1,
        CorrectionAsCreditLine = -2
    }

    public enum EOBCommentType
    {
        None = 0,
        Reason = 1,
        Adjustment = 2,
        SystemReasonCode = 3
    }

    #endregion


    public enum FeeScheduleType
    {
        None = 0,
        UserSelected = 1,
        Provider = 2,
        Insurance = 3,
        Default = 4,
        CPT = 5
    }

    public enum BatchType
    {
        None = 0,
        Queue = 1,
        Batch = 2
    }

    public enum FacilityType
    {
        None = 0,
        Facility = 1,
        NonFacility = 3
    }
    //Used to show the dialog while saving the Insurance payment 
    public enum PaymentDialogType
    {
        Payment = 1,
        Correction = 2,
        Reserve = 3
    }

    //Used to set the dialog result while Insurance payment 
    public enum PaymentDialogResult
    {
       PendingInsurancePayment = 1,
       OrininalPayment = 2,
       NewPayment = 3
    }


    //public enum ClaimSendType
    //{ 
    //    None = 0,
    //    Paper = 1,
    //    Electronic = 2
    //}
    // We are using same enum from patient with the name TypeOfBilling

    public enum TransactionStatus
    {
        //**Note 
        // Any changes made to this enum also do 
        // simultaneous change to same enum in gloPMClaimManager

        None = 0,
        Transacted = 1,
        Queue = 2,
        Batch = 3,
        Send = 4,
        Rejected = 5,
        Accepted = 6,
        ReQueue = 7,
        ReBatch = 8,
        ReSend = 9,
        FullyPaid = 10,
        PartialPaid = 11,
        Hold = 12,
        Challenge = 13,
        Alert = 14,
        Pending = 15,
        SendToClaimManager = 16,
        SendToClearingHouse = 17,
        Deleted = 18,
        Self = 19, //MaheshB 0n 20091113
        InsurancePaid = 20,
        Rebilled = 21,
        Resent = 22 //20100422 gloPM5040.
    }

    public enum ClaimStatus
    {
        None = 0,
        Open = 1,
        Close = 2
    }

    public enum TransactionType
    {
        None = 0,
        Payment = 1,
        Billed = 2,
        Adjustment = 3,
        Copay = 4,
        Deductible = 5,
        Receipt = 6,
        Writeoff = 7,
        Coverage = 8,
        InsuracePayment = 9,
        PatientPayment = 10,
        Coinsurance = 11,
        WithHold = 12, //Unassigned5 = 12,
        Refund = 13,
        Reassign = 14
    }

    public enum PaymentOtherType
    {
        None = 0,
        Copay = 1,
        PatientCoverage = 2,
        InsuranceCoverage = 3,
        AdvancePayment = 4
    }

    public enum PaymentMode
    {
        //**..Note - If you add any enumeration type to this enum
        //**..Please add the same to the GetPaymentModeValue() method on frmBillingPayment
        //**..switch case

        None = 0,
        Cash = 1,
        Check = 2,
        MoneyOrder = 3,
        CreditCard = 4,
        EFT = 5, //Electronic Funds Transfer
        Voucher=6
    }

    public enum PayerMode
    {
        None = 0,
        Self = 1,
        Insurance = 2,
        BadDebt=3
    }

    public enum InsuranceTypeFlag
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3
    }

    public enum TaxType
    {
        None = 1, Discount = 2
    }

    public enum VoidType
    {
        None = 0, ClaimVoid = 1, PatientPaymentVoid = 2, InsurancePaymentVoid = 3, PatientPaymentVoidEntry = 4, InsurancePaymentVoidEntry = 5, PatientPaymentRefundVoid = 6, PatientPaymentRefundVoidEntry = 7, InsurancePaymentRefundVoid = 8,InsurancePaymentRefundVoidEntry = 9
    }

    public enum NoteType
    {
        GeneralNote = 0, CashPayment = 1, Payment_Insurance = 2, Payment_Patient = 3, Payment_Copay = 4, Payment_Deductable = 5, Payment_Adjustment = 6, Payment_Coinsurance = 7, Payment_Refund = 8, Payment_WithHold = 9, Void_Note = 10, Box19_Note = 11, Claim_Note = 12, Claim_Box10dNote = 13
    }

    public enum BillingNoteType
    {
        None = 0,
        ChargesBillingNote = 1,
        ChargesStatementNote = 2,
        ChargesInternalNote = 3,
        PaymentNote = 4,
        PaymentStatementNote = 5,
        PaymentInternalNote = 6

    }

    public enum ClaimValidationService
    {
        YOST = 1,
        Alpha2 = 2,
        None = 0
    }

    public enum ClearinghouseFolderTypes
    {
        None = 1,
        Inbox = 2,
        Outbox = 3,
        General = 4
    }

    public enum TransactionLineColumnType
    {
        None = 0,
        Insurance = 1,
        CPT = 2,
        Diagnosis = 3,
        Modifier = 4,
        ReasonCode=5,
        RemarkCode =6  // enum added for Remark code association - 8060
    }

    public enum ClaimReSubmissionCode
    {
        Replacement = 7, Void_Cancel = 8

    }

    public enum ApplyPaymentOption
    {
        None = 0,
        Select_N_Apply = 1,
        Collect_N_Apply = 2,
        AddToPending = 3
    }

    public enum CliamLineUserStatus
    {
        None = 0,
        Rebill = 1,
        Rejected = 2,
        Followup = 3
    }

    public enum PaymentCloseTrayStatus
    {
        None = 0,
        Closed = 1
    }

    public enum RemitClaimStatus
    {
        Processed_As_Primary = 1,
        Processed_As_Secondary = 2,
        Processed_As_Tertiary = 3,
        Denied = 4,
        Pended = 5,
        Received_But_Not_InProcess = 10,
        Suspended = 13,
        Suspended_Investigation_With_Field = 15,
        Suspended_Return_With_Material = 16,
        Suspended_Review_Pending = 17,
        Processed_As_Primary_Forwarded_To_Additional_Payer = 19,
        Processed_As_Secondary_Forwarded_To_Additional_Payer = 20,
        Processed_As_Tertiary_Forwarded_To_Additional_Payer = 21,
        Reversal_of_Previous_Payment = 22,
        Not_Our_Claim_Forwarded_To_Additional_Payer = 23,
        Predetermination_Pricing_Only_No_Payment = 25,
        Reviewed = 27
    }

    public enum RemitClaimProcessStatus
    {
        None = 0,
        SucessfullyPosted = 1,
        ErrorPosting = 2
    }
    public enum HoldType
    {
        BillingHold = 0,
        PlanHold = 1
    }
    public enum PaymentAddressType
    {
        PateintProviderAddress = 0,
        RemitAddress = 1,
        OtherAddress = 2
    }
    public enum RemitAddressType
    {
        PateintProviderAddress = 0,       
        OtherAddress = 1
    }
    public enum BillingType
    {
        Professional = 1,
        Institutional = 2,
    }
    public enum CloseDayType
    {
        Charge = 0,
        Payment = 1
    }

    public enum ClaimActionType
    {
        NoPost = 0,
        Paid = 1
    }

    public enum ReplacementClaimCreationType
    {
        None = 0,
        Copy = 1,
        Replacement = 2
      
    }

    public enum QuickNoteType
    {
        None = 0,
        [Description("Claim Internal")]
        ClaimInternal = 1,
        [Description("Account Internal")]
        AccountInternal = 2,
        [Description("Statement Patient")]
        StatementPatient = 3,
        [Description("Statement Charge")]
        StatementCharge = 4,
        [Description("Statement Remark Code")]
        StatementRemarkCode = 5,
        [Description("Statement Reason Code")]
        StatementReasonCode = 6
    }

    public enum enumColType
    {
        Category = 0,
        Month = 1,
        Document = 2
    }

     public enum enumMonth
     {           
        None = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
        All = 13
     }

     public enum ERAPostedDuration
     {
         [Description("Last 1 Week")]
         LastWeek = 0,
         [Description("Last 1 Month")]
         LastMonth = 1,
         [Description("Last 1 Year")]
         LastYear = 2,
         [Description("All")]
         All = 3
         
     }
}

public static class Enumerations
{
    public static string GetEnumDescription(Enum value)
    {
        try
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);
            fi = null;
            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        catch (Exception)
        {
            return "";
        }
        
    }
}
