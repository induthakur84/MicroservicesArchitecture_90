using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MigrationUpdate
{

    // static class because we don't need to create an object of migrationManger

    // this class acts as a helper class for database migration.
    public static class MigrationManager
    {
        // this is the generic method that can work with any dbcontext type
        public static void UpdateDatabase<TContext>(IApplicationBuilder app) 
            where TContext:  DbContext
        {


            // this method runs the migration process in the backend task.
            // it prevent blocking the applcation in startup process



            Task.Run(() =>
            {

                //create a new service scope
                // it required because dbcontext is usually register as Scoped

                using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope();


                // get the dbcontext instance from dependency inejction container

                using var context = serviceScope.ServiceProvider
                .GetRequiredService<TContext>();


                // set the maximum time(in second ) allowed to database commands
                // it will be useful when the migraiton take longer that default timepuut


                context.Database.SetCommandTimeout(1000);


                // applies all the pending migration to the data
                //if that not added inside the database // ef core check it///

                // then update the database schemes

                context.Database.Migrate();

            });
                
        }

    }
}
