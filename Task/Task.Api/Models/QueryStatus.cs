using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public enum QueryStatus : byte
    {
        Pending = 1,
        Activ,
        End,
        Cance
    }
}