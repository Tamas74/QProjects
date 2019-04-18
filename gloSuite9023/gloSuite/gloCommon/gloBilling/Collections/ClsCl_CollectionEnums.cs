using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloBilling.Collections
{
    public class CollectionEnums
    {
        public enum FollowUpType
        {
            Claim=1,
            PatientAccount=2,
            BadDebt=3
        }

        public enum ScheduleType
        {
            System,
            Manual
        }
    }
}
