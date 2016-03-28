namespace BootcampTrack.Data.Migrations
{
    using Core.Constants;
    using Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BootcampTrack.Data.Infrastructure.BootcampTrackDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BootcampTrack.Data.Infrastructure.BootcampTrackDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Roles.AddOrUpdate(
              p => p.Name,
              new Role { Id = Guid.NewGuid().ToString(), Name = RoleConstants.SchoolAdministrator },
              new Role { Id = Guid.NewGuid().ToString(), Name = RoleConstants.Instructor },
              new Role { Id = Guid.NewGuid().ToString(), Name = RoleConstants.Student }
            );
        }
    }
}