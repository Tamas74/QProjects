using System;
using System.Collections.Generic;
using System.Text;

namespace gloCMSEDI
{
    // Some Changed
    class ClsgloBillingEnums
    {
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

    public enum ClaimStatus
    {
        None = 0,
        Open = 1,
        Close = 2
    }
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
        InsurancePaid=20,
        Rebilled=21,
        Resent=22 //20100422 gloPM5040.
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
        EFT = 5 //Electronic Funds Transfer
    }

    public enum PayerMode
    {
        None = 0,
        Self = 1,
        Insurance = 2
    }

    public enum InsuranceTypeFlag
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3
    }

    public enum EOBPaymentSubType
    {
        None = 0, Insurace = 1, Copay = 2, Advance = 3, Coinsurace = 4, Dedcutiable = 5, WriteOff = 6, WithHold = 7, Patient = 8, Reserved = 9, Other = 10, TakeBack = 11, Adjuestment = 12, Correction = 13, Refund = 14, StatementNote = 15, InternalNote = 16, Charges_BillingNote = 17, Charges_StatementNote = 18, Charges_InternalNote = 19, Claim_Box19Note = 20
    }

    public enum TaxType
    {
        None = 1, Discount = 2
    }

    public enum NoteType
    {
        GeneralNote = 0, CashPayment = 1, Payment_Insurance = 2, Payment_Patient = 3, Payment_Copay = 4, Payment_Deductable = 5, Payment_Adjustment = 6, Payment_Coinsurance = 7, Payment_Refund = 8, Payment_WithHold = 9
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
        Modifier = 4
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

    public enum FeeScheduleType
    {
        None = 0,
        UserSelected = 1,
        Provider = 2,
        Insurance = 3,
        Default = 4,
        CPT = 5
    }

    public enum RemitClaimProcessStatus
    {
        None = 0,
        SucessfullyPosted = 1,
        ErrorPosting = 2
    }
}
