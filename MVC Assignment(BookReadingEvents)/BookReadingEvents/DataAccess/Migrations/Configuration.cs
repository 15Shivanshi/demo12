namespace DataAccess.Migrations
{
    using DataAccess.DatabaseModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.EventDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.EventDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<UserEntity>
            {
                new UserEntity
                {
                    EmailID ="myadmin@bookevents.com",
                    FullName="Administrator",
                    Password="pass"
                },
                new UserEntity
                {
                    EmailID ="a@g.com",
                    FullName="a g",
                    Password="pass"
                },
                new UserEntity
                {
                    EmailID ="b@g.com",
                    FullName="b g",
                    Password="pass"
                },
                new UserEntity
                {
                    EmailID ="c@g.com",
                    FullName="c g",
                    Password="pass"
                }
            };

            users.ForEach(u => context.Users.AddOrUpdate(u));
            context.SaveChanges();

            //var events = new List<EventEntity>
            //{
            //new EventEntity
            //{
            //    HostEmailID ="a@g.com",
            //    Title ="event 1",
            //    Date =Convert.ToDateTime("1212-12-12"),
            //    Location ="loc",
            //    StartTime =Convert.ToDateTime("1212-12-12"),
            //    Type =EventType.Private,
            //    Duration =2,
            //    Description ="desc",
            //    OtherDetails="other details",
            //    InvitedUsers ="a@a.com, b@b.com, c@c.com"
            //},

            //new EventEntity
            //{
            //    HostEmailID = "b@g.com",
            //    Title = "event 2",
            //    Date = Convert.ToDateTime("1212-12-12"),
            //    Location = "loc",
            //    StartTime = Convert.ToDateTime("1212-12-12"),
            //    Type = EventType.Private,
            //    Duration = 2,
            //    Description = "desc",
            //    OtherDetails = "other details",
            //    InvitedUsers = "a@a.com, b@b.com, c@c.com"
            //},
            //new EventEntity
            //{
            //    HostEmailID = "c@g.com",
            //    Title = "event 3",
            //    Date = Convert.ToDateTime("1212-12-12"),
            //    Location = "loc",
            //    StartTime = Convert.ToDateTime("1212-12-12"),
            //    Type = EventType.Private,
            //    Duration = 3,
            //    Description = "desc",
            //    OtherDetails = "other details",
            //    InvitedUsers = "a@a.com, b@b.com, c@c.com"
            //}
            //};
        }
    }
}
