using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public class User
    {
        public User()
        {
            Queries = new HashSet<Query>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<Query> Queries { get; set; }
    }
}