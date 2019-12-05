using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Web.Models
{
    public enum QueryStatusType : byte
    {
        Pending = 1,
        Activ,
        End,
        Cance
    }
}