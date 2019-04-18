using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloUIControlLibrary.Classes.ClinicalChartQueue
{
    public enum enumBackgroundProcessType
    {
        Queue = 1,
        Print = 2,
        FAX = 3,
        Export = 4,
        TsPrint = 5
    }
    
    public enum enumDocType
    {
        PatientInformation = 1,
        CurrentMedication = 2,
        OrdersTestResult = 3,
        Orders = 4,
        PatientExam = 5,
        OrderTemplates = 6,
        RefferalDocument = 7,
        Messages = 8,
        NursesNotes = 9,
        Triage = 10,
        PTProtocol = 11,
        PatientConsent = 12,
        DisclosureManagement = 13,
        PatientLetters = 14,
        FormGallery = 15,
        ClaimDetails = 16,
        PaymentDetails = 17,
        ScannedDocument = 18,        
        PatientForms = 19,
        ClaimHistory = 20,
        Patient_Education = 21
    }

    public enum enumPrintStatus
    { 
        None = 0,
        Queued = 1,
        Processed = 2,
        Cancel =3,
        ReQueue=4,
        ErrorProcessed = 5,
        InProgress =6
    }

    public enum enumNoteStatus
    { 
        Queue = 1,
        Print = 2,
        Status = 3
    }
}
