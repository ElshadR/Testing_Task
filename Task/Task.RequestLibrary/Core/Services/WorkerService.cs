using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.RequestLibrary.Core.Interfaces;
using Task.RequestLibrary.Models;

namespace Task.RequestLibrary.Core.Services
{
    public class WorkerService:IWorkerService
    {
        private readonly RequestSender _requestSender = null;
        public WorkerService(RequestSender requestSender)
        {
            _requestSender = requestSender;

        }

        public async Task<RequestResult<WorkerModel>> Get()
        {
            return await _requestSender.GetResponse<WorkerModel>("api/worker/all", (x) =>
            {
                x.AddParameter("Id", "2",RestSharp.ParameterType.UrlSegment);
            });
        }

        public async Task<RequestResult<List<WorkerModel>>> GetActiveWorker()
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult<List<WorkerModel>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
