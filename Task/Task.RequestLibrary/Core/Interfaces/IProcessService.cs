using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.RequestLibrary.Models;

namespace Task.RequestLibrary.Core.Interfaces
{
    public interface IProcessService
    {
        Task<RequestResult<List<QueryModel>>> ComplitedQueries();
        Task<RequestResult<List<QueryModel>>> PendingQuery();
        Task<RequestResult<QueryModel>> AddQuery(QueryModel query);
        Task<RequestResult<QueryModel>> GetQuery();
        Task<RequestResult<QueryModel>> StatusQuery(int id);
        Task<RequestResult<QueryModel>> CancelQuery(int id);
        Task<RequestResult<List<WorkerModel>>> BusyWorkers();
    }
}
