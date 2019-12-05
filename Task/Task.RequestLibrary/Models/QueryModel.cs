using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Models
{
   public class QueryModel
    {
        public QueryModel()
        {
            CreatedAt = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            QueryStatus = QueryStatusType.Pending;
        }
        public int Id { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }

        public int UserId { get; set; }
        public int? WorkerId { get; set; }
        public WorkerModel Worker { get; set; }

        public string CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public QueryStatusType QueryStatus { get; set; }
    }
}
