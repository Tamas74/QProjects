using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloGlobal.Common;

namespace gloGlobal.EPA
{
    public class gloEPAHelper : IDisposable
    {
        public string ServiceURL { get; set; }

        public gloEPAHelper(string _ServiceURL)
        {
            this.ServiceURL = _ServiceURL;
        }

        private AuthResponse GetAuthenticationToken(AuthRequest request)
        {
            BaseServiceHelper<AuthResponse, EPA.URLOption> helper = new BaseServiceHelper<AuthResponse, EPA.URLOption>(ServiceURL);
            return helper.GetResponse(request, URLOption.GetAuthToken);
        }

        public AuthResponse GetAuthenticationToken(Int64 UserID, Int64 SPI, EPA.RoleType Role)
        {
            provider providerRole = new provider();
            AuthRequest authRequest = new AuthRequest();
                        
            providerRole.id = Convert.ToString(SPI);

            authRequest.roles.providers.Add(providerRole);
            authRequest.roles.commonRoles.Add(Role.ToString().ToLower());

            authRequest.endUserId = Convert.ToString(UserID);

            return this.GetAuthenticationToken(authRequest);
        }
        public PAStatusResponse GetPAStatus(string paRefID)
        {
            PAStatusRequest statusRequest = new PAStatusRequest(paRefID);
            BaseServiceHelper<PAStatusResponse, EPA.URLOption> helper = new BaseServiceHelper<PAStatusResponse, EPA.URLOption>(ServiceURL);
            return helper.GetResponse(statusRequest, URLOption.GetPAStatus);
        }

        public List<PAInitResponse> SubmitPARequest(PAInitRequest request)
        {
            BaseServiceHelper<List<PAInitResponse>, EPA.URLOption> helper = new BaseServiceHelper<List<PAInitResponse>, EPA.URLOption>(ServiceURL);
            return helper.GetResponse(request, URLOption.SubmitPARequest);
        }

        public void Dispose()
        {

        }
    }
}
