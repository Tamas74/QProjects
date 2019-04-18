using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriArqEDIRealTimeClaimStatus
{
    public class clsQAuditEnums
    {
    }

    public enum ActivityModule
    {
        None = 0,
        ClaimModel = 1,
        EDI276Model = 2,
        EDI277Model = 3,
        EDIQueue=4,
        EDI276Operation = 5,
        EDI277Operation = 6,
        EDIUpload = 7,
        EDiDownLoad = 8,
        EDIGeneral=9,
        EDIActivityLog=10
    }

    public enum ActivityOutCome
    {
        None=0,
        Started =1,
        Stopped=2,
        Success = 3,
        Failure = 4,
        InProcess = 5,
        Complete = 6,
        InComplete = 7
    }


    public enum ActivityReference
    {
        None = 0,
        EDI276File = 1,
        EDi277File = 2,
        ClaimNumber = 3,
        Practice = 4,
        FTPServer = 5
    }


    public enum ActivityType
    {
        None = 0,
        Add = 1,
        Modify = 2,
        Delete = 3,
        View = 4,
        RequestProcessing = 5,
        QueueCreation = 6,
        QueueStatusUpdate = 7,
        FetchDatabase = 8,
        GenerateEDI = 9,
        ParseEDI = 10,
        FillEDIModel = 11,
        GenerateSegmentIDs = 12,
        SaveToDatabase = 13,
        EDIUploading = 14,
        EDIDownloading = 15,
        ResponseProcessing = 16,
        FillClaimModel = 17,
        GeneralOperation = 17,
        SaveToFile = 18,
        InitializeEDi=19
    }
}
