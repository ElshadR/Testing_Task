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
    public class LoginController : Controller
    {
        // GET: Login
        private IWorkerService WorkerService;
        private IProcessService ProcessService;
        public LoginController()
        {
            var request = new RequestSender();
            WorkerService = new WorkerService(request);
            ProcessService = new ProcessService(request);
        }
        public async Task<ActionResult> Login()
        {


            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Login login)
        {
            var l = await ProcessService.Login(new RequestLibrary.Models.Login
            {
                 NickName=login.NickName,
                  Password=login.Password
            });

            if(l.IsSucced)
            {
                login.Id = l.Data.Id;
                login.Role = GetRole(l.Data.Role);
                return RedirectToAction("index","home",login);
            }
            return View();

        }
        private Role GetRole(RequestLibrary.Models.Role role)
        {
            switch (role)
            {
                case RequestLibrary.Models.Role.Worker:
                    return Role.Worker;
                case RequestLibrary.Models.Role.User:
                    return Role.User;
            }
            return  0;
        }
    }
}