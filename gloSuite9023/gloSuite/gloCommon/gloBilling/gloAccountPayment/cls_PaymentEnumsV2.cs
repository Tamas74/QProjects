using System;
using System.Collections.Generic;
using System.Text;

namespace gloAccountsV2
{
    public enum PaymentModeV2
    {
        None = 0,
        Cash = 1,
        Check = 2,
        MoneyOrder = 3,
        CreditCard = 4,
        EFT = 5,
        Voucher=6
    }
    public enum PayerTypeV2
    {
        None = 0,
        Self = 1,
        Insurance = 2
    }
    public enum VoidTypeV2
    {
        None = 0, 
        ClaimVoid = 1, 
        PatientPaymentVoid = 2, 
        InsurancePaymentVoid = 3, 
        PatientPaymentRefundVoid = 4, 
        InsurancePaymentRefundVoid = 5 
        
    }
    public enum PaymentEntryTypeV2
    {
        None = 0, 
        InsuraceReserved = 1, 
        PatientReserved = 2, 
        EOB = 3, 
        InsuracePayment = 4, 
        InsuraceRefund = 5, 
        PatientPayment = 6,
        PatientRefund = 7, 
        InsuranceCorrection = 8, 
        PatientCorrection = 9,
        UseReserve = 10,
        TakeBack = 11,
        InsurancetOffset = 12
    }
    public enum ReserveEntryTypeV2
    {
        None = 0,
        InsuraceReserve = 1,
        PatientReserve = 2,
        InsuranceRefund = 3,
        PatientRefund = 4,
        ClaimVoidReserved = 5
    }

    public enum InsuranceTypeFlagV2
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3
    }

    public enum PaymentTypeV2
    {
        None = 0, InsuraceReserverd = 1, PatientReserved = 2, EOB = 3, InsuracePayment = 4, InsuraceRefund = 5, PatientPayment = 6,
        PatientRefund = 7, InsuranceCorrection = 8, PatientCorrection = 9
    }

    public enum NoteTypeV2
    {
        GeneralNote = 0, Payment_Insurance = 1, Payment_Patient = 2, Payment_InsuranceReserved = 3, Payment_PatientReserved = 4
    }

    public enum NoteSubTypeV2
    {
        None = 0, MasterPayment = 1, Copay = 2, Advance = 3, Other = 4, StatementNote = 5, InternalNote = 6
    }

    public enum BillingNoteTypeV2
    {
        None = 0,
        ChargesBillingNote = 1,
        ChargesStatementNote = 2,
        ChargesInternalNote = 3,
        PaymentNote = 4,
        PaymentStatementNote = 5,
        PaymentInternalNote = 6,
        ICD9 = 7,
        CPT = 8
    }

    public enum EOBCommentTypeV2
    {
        None = 0,
        Reason = 1,
        Adjustment = 2,
        SystemReasonCode = 3
    }
}
