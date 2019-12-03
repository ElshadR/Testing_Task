using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.RequestLibrary.Models;

namespace Task.RequestLibrary.Core.Interfaces
{
    public interface IWorkerService
    {
        Task<RequestResult<WorkerModel>> Get();

        Task<RequestResult<List<WorkerModel>>> GetAll();

        Task<RequestResult<List<WorkerModel>>> GetActiveWorker();
    }
}
