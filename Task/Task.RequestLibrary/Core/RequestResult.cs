using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Core
{
    public class RequestResult<T>
    {
        public bool IsSucced { get; set; }

        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
