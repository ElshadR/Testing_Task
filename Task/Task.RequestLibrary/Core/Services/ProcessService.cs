using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.RequestLibrary.Core.Interfaces;
using Task.RequestLibrary.Models;

namespace Task.RequestLibrary.Core.Services
{
    public class ProcessService : IProcessService
    {
        private readonly RequestSender _requestSender = null;
        public ProcessService(RequestSender requestSender)
        {
            _requestSender = requestSender;

        }
        public async Task<RequestResult<QueryModel>> AddQuery(QueryModel query)
        {
            return await _requestSender.PostResponse<QueryModel>("api/process/addquery",request=>
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(query);
            });
        }

        public async Task<RequestResult<List<WorkerModel>>> BusyWorkers()
        {
            return await _requestSender.GetResponse<List<WorkerModel>>("api/process/busyworkers");
        }

        public Task<RequestResult<QueryModel>> CancelQuery(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<QueryModel>>> ComplitedQueries()
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<QueryModel>> GetQuery()
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<QueryModel>>> PendingQuery()
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<QueryModel>> StatusQuery(int id)
        {
            throw new NotImplementedException();
        }
    }
}
