using System;

namespace gloGlobal.EPA
{    
    public enum RoleType
    {
        None = 0,
        PAReviewer = 1,
        PAPreparer = 2,
        PASubmitter = 3
    }

    public enum URLOption
    {
        GetAuthToken,
        SubmitPARequest,
        GetPAStatus,
        GetRolePerProviderAuthToken
    }

    public enum ServiceType
    {
        Worklist,
        WorkProcess,
        Process
    }
}
