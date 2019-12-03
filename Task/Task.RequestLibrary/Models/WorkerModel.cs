using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Models
{
    public class WorkerModel
    {
        public WorkerModel()
        {
            Queries = new List<QueryModel>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
        public string NickName { get; set; }

        public bool IsBusy { get; set; }

        public PositionType Position { get; set; }

        public List<QueryModel> Queries { get; set; }
    }
}
