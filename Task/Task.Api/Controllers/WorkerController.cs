using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Task.Api.Models;

namespace Task.Api.Controllers
{
    [RoutePrefix("api/worker")]
    [Route("{action}/{id?}", Name = "WorkerRoute")]
    public class WorkerController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> all()
        {
            using(TaskDbContext db=new TaskDbContext())
            {
                var worker = await db.Workers.ToListAsync();
                if (worker != null)
                    return Ok(worker);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IHttpActionResult> worker(int id)
        {
            using(TaskDbContext db=new TaskDbContext())
            {
                var worker = await db.Workers.FirstOrDefaultAsync(x=>x.Id==id);
                if (worker != null)
                    return Ok(worker);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IHttpActionResult> Worker([FromBody] Worker worker)
        {
            if (worker == null)
                return StatusCode(HttpStatusCode.NoContent);

            try
            {
                using (TaskDbContext db = new TaskDbContext())
                {
                    db.Workers.Add(worker);
                    await db.SaveChangesAsync();
                }
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
