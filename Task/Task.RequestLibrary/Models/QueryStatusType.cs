using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Models
{
    public enum QueryStatusType : byte
    {
        Pending = 1,
        Activ,
        End,
        Cance
    }
}
