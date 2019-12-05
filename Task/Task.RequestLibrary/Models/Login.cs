using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
