using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsRxHubDBLayerException : ApplicationException   
    {
        public ClsRxHubDBLayerException(string sErrorMessage)
                : base(sErrorMessage)
            {

            }
    }
}
