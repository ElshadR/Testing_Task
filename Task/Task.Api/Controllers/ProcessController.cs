using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Task.Api.Models;

namespace Task.Api.Controllers
{
    [RoutePrefix("api/process")]
    [Route("{action}/{id?}", Name = "ProcessRoute")]
    public class ProcessController : ApiController
    {
        [HttpGet]
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
                        HttpContext.Current.Session["Login"] = login;
                        return Ok(login);
                    }
                    else
                    {
                        var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == login.NickName && u.Password == login.Password);
                        if (user != null)
                        {
                            login.Role = Role.User;
                            HttpContext.Current.Session["Login"] = login;
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
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> PendingQuery()
        {
            throw new Exception();
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
        [HttpPost]
        public async Task<IHttpActionResult> AddQuery([FromBody] Query query)
        {
            if (query == null)
                return StatusCode(HttpStatusCode.NoContent);

            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    db.Queries.Add(query);
                    await db.SaveChangesAsync();
                }
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetQuery()
        {
            //Burada biz her bir isci ucun bos sorgulari veririk hansiki bir alqoritmnen isliyir,
            //her bir sorgu verildikde burdan hemin anda status deyisdirilir activ olur;
            //eger query pending den elave bir statusdadirsa onda o verile bilmir ancaq statusu pending olsa.

            throw new Exception();
        }
        [HttpGet]
        public async Task<IHttpActionResult> StatusQuery(int id)
        {
            //buradan biz her bir querinin statusunu oyrene bilirik
            throw new Exception();
        }
        [HttpPut]
        public async Task<IHttpActionResult> CancelQuery(int id)
        {
            //burada ise sorgu kime ayiddirse ancaq o hemin sorgunu sile geri qytara bilir.
            throw new Exception();
        }
        
    }
}
