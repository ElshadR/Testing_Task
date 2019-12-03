namespace Task.Api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Task.Api.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Task.Api.Models.TaskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task.Api.Models.TaskDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Workers.Any())
            {
                var director = new Worker()
                {
                    FullName = "Elshad Rustemov",
                    NickName = "elshad1997",
                    Position = Position.Director,
                    Password = "elshad@123",
                };
                var operator1 = new Worker()
                {
                    FullName = "Melek Ibrahimova",
                    NickName = "melek1996",
                    Position = Position.Operator,
                    Password = "melek@1234",
                };
                var operator2 = new Worker()
                {
                    FullName = "Mikayil Sadiqzade",
                    NickName = "mikayil1998",
                    Position = Position.Operator,
                    Password = "mikayil@12345",
                };
                var operator3 = new Worker()
                {
                    FullName = "Senan Huseyinzade",
                    NickName = "senan1995",
                    Position = Position.Operator,
                    Password = "senan@123456",
                };
                var menecer1 = new Worker()
                {
                    FullName = "Ilkin Xaniyev",
                    NickName = "ilkin1989",
                    Position = Position.Menecer,
                    Password = "ilkin@1234567",
                };
                var menecer2 = new Worker()
                {
                    FullName = "Penah Haciyev",
                    NickName = "penah1997",
                    Position = Position.Menecer,
                    Password = "penah@12345678",
                };
                
                context.Workers.Add(director);
                context.Workers.Add(operator1);
                context.Workers.Add(operator2);
                context.Workers.Add(operator3);
                context.Workers.Add(menecer1);
                context.Workers.Add(menecer2);
                context.SaveChangesAsync().GetAwaiter().GetResult();

            }
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    UserName = "cavid1993",
                    Password = "cavid@1234567",
                };
                var user2 = new User()
                {
                    UserName = "mushfiq1990",
                    Password = "mushfiq@12345678",
                };
                context.Users.Add(user);
                context.Users.Add(user2);
                context.SaveChangesAsync().GetAwaiter().GetResult();

            }
        }
    }
}
