using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace gloMeds.Core.MedHx
{
    public static class SerializationHelper
    {
        public static MedHxRequests Deserialize(string request)
        {
            return JsonConvert.DeserializeObject<MedHxRequests>(request);
        }

        public static List<MedHxItem> DeserializeMedHxItem(string request)
        {
            return JsonConvert.DeserializeObject<List<MedHxItem>>(request);
        }
        public static List<MedHxItemNew> DeserializeMedHxItemNew(string request)
        {
            return JsonConvert.DeserializeObject<List<MedHxItemNew>>(request);
        }
        public static string Serialize(MedHxRequests request)
        {
            return JsonConvert.SerializeObject(request);
        }
    }
}
