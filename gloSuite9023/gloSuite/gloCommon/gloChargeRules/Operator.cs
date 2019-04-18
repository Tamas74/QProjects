using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargeRules
{
    public enum Operator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        FoundIn,
        NotFoundIn,
        Exists,
        NotExists,
        //Like,
        NotDefined
    }
}
