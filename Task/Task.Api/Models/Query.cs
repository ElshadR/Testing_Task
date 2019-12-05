using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public class Query
    {
        public Query()
        {
            QueryStatus = QueryStatus.Pending;
        }
        public int Id { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }

        public int? WorkerId { get; set; }

        [JsonIgnore]
        public Worker Worker { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public QueryStatus QueryStatus { get; set; }
    }
}