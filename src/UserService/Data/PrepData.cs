using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Data
{
    public static class PrepData
    {
        public static void Prepare(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<UserServiceContext>();
                Console.WriteLine("Applying Migration");
                db.Database.Migrate();
            }
        }
    }
}
