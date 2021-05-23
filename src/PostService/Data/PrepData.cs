using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PostService.Data
{
    public static class PrepData
    {
        public static void Prepare(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<PostServiceContext>();
                Console.WriteLine("Applying Migration");
                db.Database.Migrate();
            }
        }
    }
}
