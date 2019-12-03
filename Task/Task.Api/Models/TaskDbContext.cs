using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Task.Api.Models
{
    public class TaskDbContext:DbContext
    {
        public TaskDbContext():base("taskDbConnections")
        {

        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<User> Users { get; set; }

    }
}