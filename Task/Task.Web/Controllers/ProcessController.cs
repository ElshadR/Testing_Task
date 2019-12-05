using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task.RequestLibrary.Core;
using Task.RequestLibrary.Core.Interfaces;
using Task.RequestLibrary.Core.Services;
using Task.Web.Models;

namespace Task.Web.Controllers
{
    public class ProcessController : Controller
    {
        private IWorkerService WorkerService;
        private IProcessService ProcessService;
        public ProcessController()
        {
            var request = new RequestSender();
            WorkerService = new WorkerService(request);
            ProcessService = new ProcessService(request);
        }
        // GET: Process
        public async Task<ActionResult> GetQ(int workerId)
        {
            var query = await ProcessService.GetQuery(workerId);
            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                data = query
            }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("POST", "PUT")]
        public async Task<ActionResult> QueryAnswer(AnswerModel answer)
        {
            var query = await ProcessService.AnswerQuery(new RequestLibrary.Models.AnswerModel
            {
                 Id=answer.Id,
                 Text=answer.Text
            });
            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                    data = query
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddQuery(QueryModel queryModel)
        {
            var query = await ProcessService.AddQuery(new RequestLibrary.Models.QueryModel
            {
                  Question=queryModel.Question,
                  UserId=queryModel.UserId
            });
            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                data = query
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> IsBusyWorker(int workerId)
        {
            var query = await ProcessService.IsBusyWorker(workerId);

            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                    data = query
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> PendingQueryByUser(int userId)
        {
            var query = await ProcessService.PendingQueryByUser(userId);

            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                    data = query
            }, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs("POST", "PUT")]
        public async Task<ActionResult> CancelQuery(int queryId)
        {
            var query = await ProcessService.CancelQuery(queryId);
            if (query.IsSucced)
            {
                return Json(new
                {
                    msg = 200,
                    data = query
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                msg = 500,
                    data = query
            }, JsonRequestBehavior.AllowGet);
        }
    }
}