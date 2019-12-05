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

        public async Task<RequestResult<AnswerModel>> AnswerQuery(AnswerModel answer)
        {
            return await _requestSender.PutResponse<AnswerModel>("api/process/answerquery", request =>
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(answer);
            });
        }

        public async Task<RequestResult<List<WorkerModel>>> BusyWorkers()
        {
            return await _requestSender.GetResponse<List<WorkerModel>>("api/process/busyworkers");
        }

        public async Task<RequestResult<QueryModel>> CancelQuery(int id)
        {
            return await _requestSender.PutResponse<QueryModel>("api/process/CancelQuery", request =>
            {
                request.AddQueryParameter("id", id.ToString());
            });
        }

        public async Task<RequestResult<List<QueryModel>>> ComplitedQueries()
        {
            return await _requestSender.GetResponse<List<QueryModel>>("api/process/complitedqueries");
        }

        public async Task<RequestResult<QueryModel>> GetQuery(int workerId)
        {
            return await _requestSender.GetResponse<QueryModel>("api/process/getquery",request=>
            {
                request.AddParameter("workerId", workerId);
            });
        }

        public async Task<RequestResult<Login>> Login(Login login)
        {
            return await _requestSender.PostResponse<Login>("api/process/login",request=>
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(login);
            });
        }

        public async Task<RequestResult<List<QueryModel>>> PendingQuery()
        {
            return await _requestSender.GetResponse<List<QueryModel>>("api/process/pendingquery");
        }

        public async Task<RequestResult<QueryModel>> StatusQuery(int id)
        {
            return await _requestSender.GetResponse<QueryModel>("api/process/statusquery",request=>
            {
                request.AddQueryParameter("id", id.ToString());
            });
        }
        public async Task<RequestResult<int>> CountPendingByQuery(int id)
        {
            return await _requestSender.GetResponse<int>("api/process/CountPendingByQuery", request=>
            {
                request.AddQueryParameter("id", id.ToString());
            });
        }

        public async Task<RequestResult<WorkerModel>> IsBusyWorker(int workerId)
        {
            return await _requestSender.GetResponse<WorkerModel>("api/process/IsBusyWorker", request =>
            {
                request.AddQueryParameter("workerId", workerId.ToString());
            });
        }

        public async Task<RequestResult<List<QueryModel>>> PendingQueryByUser(int userId)
        {
            return await _requestSender.GetResponse<List<QueryModel>>("api/process/PendingQueryByUser", request =>
            {
                request.AddQueryParameter("userId", userId.ToString());
            });
        }

        public async Task<RequestResult<SettingModel>> Settings(SettingModel setting)
        {
            return await _requestSender.PutResponse<SettingModel>("api/process/Settings", request =>
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(setting);
            });
        }
    }
}
