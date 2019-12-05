using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Task.Api.Models;

namespace Task.Api.Controllers
{
    [RoutePrefix("api/process")]
    [Route("{action}/{id?}", Name = "ProcessRoute")]
    public class ProcessController : ApiController
    {
        static object locker = new object();
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] Login login)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    
                    var worker = await db.Workers.FirstOrDefaultAsync(w=>w.NickName == login.NickName && w.Password == login.Password);
                    if (worker != null)
                    {
                        login.Role = Role.Worker;
                        login.Id = worker.Id;
                        worker.IsLogin = true;
                        await db.SaveChangesAsync();
                        return Ok(login);
                    }
                    else
                    {
                        var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == login.NickName && u.Password == login.Password);
                        if (user != null)
                        {
                            login.Role = Role.User;
                            login.Id = user.Id;
                            return Ok(login);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> ComplitedQueries()
        {
            try
            {
                using(TaskDbContext db=new TaskDbContext())
                {
                    var cQueries = await db.Queries.Where(q => q.QueryStatus == QueryStatus.Pending).ToListAsync();
                    
                    return Ok(cQueries);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> PendingQuery()
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var queries = await db.Queries.Where(q => q.QueryStatus==QueryStatus.Pending).ToListAsync();

                    if (queries.Count != 0)
                        return Ok(queries);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IHttpActionResult> PendingQueryByUser(int userId)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var queries = await db.Queries.Where(q => userId == q.UserId).ToListAsync();

                    if (queries.Count != 0)
                        return Ok(queries);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IHttpActionResult> BusyWorkers()
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var workers = await db.Workers.Where(q => q.IsBusy).ToListAsync();

                    if (workers.Count != 0)
                        return Ok(workers);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();

        }
        [HttpGet]
        public async Task<IHttpActionResult> IsBusyWorker(int workerId)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var worker = await db.Workers.FirstOrDefaultAsync(q =>  q.Id == workerId);

                    if (worker != null)
                        return Ok(worker);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IHttpActionResult> AddQuery([FromBody] Query query)
        {
            lock (locker)
            {
                if (query == null)
                    return StatusCode(HttpStatusCode.NoContent);

                try
                {
                    using (TaskDbContext db = new TaskDbContext())
                    {

                        //var login = JsonConvert.DeserializeObject<Login>(HttpContext.Current.Session["login"].ToString());
                        db.Queries.Add(query);

                        db.SaveChanges();
                    }
                    return Ok(query);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQuery(int workerId)
        {
            try
            {
                lock (locker)
                {
                    using (TaskDbContext db = new TaskDbContext())
                    {

                        var worker =  db.Workers.FirstOrDefault(x => x.Id == workerId && !x.IsBusy && x.IsLogin);
                        if (worker != null)
                        {
                            var query =  db.Queries.OrderBy(q => q.CreatedAt).FirstOrDefault(q => q.QueryStatus == QueryStatus.Pending);

                            if (query == null)
                                return NotFound();
                            var path = HostingEnvironment.MapPath("~/Models/settings.json");
                            string json = File.ReadAllText(path);
                            dynamic jsonObj = JsonConvert.DeserializeObject(json);
                            int tm = jsonObj["Tm"];
                            int td = jsonObj["Td"];

                            switch (worker.Position)
                            {
                                case Position.Operator:
                                    query.StartedAt = DateTime.Now;
                                    query.QueryStatus = QueryStatus.Activ;
                                    query.WorkerId = worker.Id;
                                    worker.IsBusy = true;
                                    db.SaveChanges();
                                    return Ok(query);
                                case Position.Menecer:
                                    // Tm vaxtdan sonra
                                    if (DateTime.Now.Subtract(query.CreatedAt) > TimeSpan.FromMinutes(tm))
                                    {
                                        query.StartedAt = DateTime.Now;
                                        query.QueryStatus = QueryStatus.Activ;
                                        query.WorkerId = worker.Id;
                                        worker.IsBusy = true;
                                        db.SaveChanges();
                                        return Ok(query);
                                    }
                                    break;
                                case Position.Director:
                                    if (DateTime.Now.Subtract(query.CreatedAt) > TimeSpan.FromMinutes(td))
                                    {
                                        query.StartedAt = DateTime.Now;
                                        query.QueryStatus = QueryStatus.Activ;
                                        query.WorkerId = worker.Id;
                                        worker.IsBusy = true;
                                        db.SaveChanges();
                                        return Ok(query);
                                    }
                                    break;
                            }
                        }
                        return NotFound();
                    }
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> StatusQuery(int id)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var query = await db.Queries.FirstOrDefaultAsync(q => q.Id == id);

                    if (query == null)
                        return NotFound();

                    return Ok(query.QueryStatus);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> AnswerQuery([FromBody] AnswerModel answer)
        {
            if (answer == null)
                return StatusCode(HttpStatusCode.NoContent);

            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var query = await db.Queries.FirstOrDefaultAsync(q => q.Id == answer.Id);

                    if (query == null)
                        return NotFound();

                    query.Answer = answer.Text;

                    query.QueryStatus = QueryStatus.End;
                    query.EndedAt = DateTime.Now;
                    var worker = await db.Workers.FirstOrDefaultAsync(w => w.Id == query.WorkerId);
                    worker.IsBusy = false;
                    await db.SaveChangesAsync();

                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> CancelQuery(int id)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var query = await db.Queries.FirstOrDefaultAsync(q => q.Id == id && (q.QueryStatus==QueryStatus.Pending));

                    if (query == null)
                        return NotFound();
                    query.QueryStatus = QueryStatus.Cance;
                    await db.SaveChangesAsync();

                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> Settings([FromBody] SettingModel setting)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~/Models/settings.json");
                string json = File.ReadAllText(path);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                int tm = jsonObj["Tm"];
                int td = jsonObj["Td"];
                if (tm!=setting.Tm)
                    jsonObj["Tm"] = setting.Tm;
                if(td!=setting.Td)
                    jsonObj["Td"] = setting.Td;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(path, output);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> CountPendingByQuery(int id)
        {
            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    var query = await db.Queries.FirstOrDefaultAsync(q => q.Id == id && q.QueryStatus == QueryStatus.Pending);
                    
                    if (query == null)
                        return NotFound();
                    var count = await db.Queries.Where(q=>q.QueryStatus == QueryStatus.Pending).CountAsync(q => q.CreatedAt < query.CreatedAt);

                    return Ok(count);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
