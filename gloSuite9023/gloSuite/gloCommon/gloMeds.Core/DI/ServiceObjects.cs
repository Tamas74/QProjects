using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloMeds.Core.DI
{    
    [DataContract()]
    public class PIResp : IDisposable
    {

        [DataMember()]
        public int pid { get; set; }

        [DataMember()]
        public List<int> iid { get; set; }

        public PIResp()
        {
            iid = new List<int>();
        }

        public void Dispose()
        {
        }
    }
}
