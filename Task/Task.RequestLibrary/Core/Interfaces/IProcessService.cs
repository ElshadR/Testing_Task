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
        Task<RequestResult<Login>> Login(Login login);
        Task<RequestResult<List<QueryModel>>> ComplitedQueries();
        Task<RequestResult<List<QueryModel>>> PendingQuery();
        Task<RequestResult<List<QueryModel>>> PendingQueryByUser(int userId);
        Task<RequestResult<QueryModel>> AddQuery(QueryModel query);
        Task<RequestResult<QueryModel>> GetQuery(int workerId);
        Task<RequestResult<SettingModel>> Settings(SettingModel setting);
        Task<RequestResult<QueryModel>> StatusQuery(int id);
        Task<RequestResult<QueryModel>> CancelQuery(int id);
        Task<RequestResult<List<WorkerModel>>> BusyWorkers();
        Task<RequestResult<AnswerModel>> AnswerQuery(AnswerModel answer);
        Task<RequestResult<int>> CountPendingByQuery(int id);
        Task<RequestResult<WorkerModel>> IsBusyWorker(int workerId);
    }
}
