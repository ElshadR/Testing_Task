using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public class Worker
    {
        public Worker()
        {
            Queries = new HashSet<Query>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
        public string NickName { get; set; }

        public bool IsBusy { get; set; }

        public Position Position { get; set; }

        public ICollection<Query> Queries { get; set; }
    }
}