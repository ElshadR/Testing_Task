using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public class Login
    {
        public string NickName { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}