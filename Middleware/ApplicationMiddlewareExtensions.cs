
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware
{
    public static class ApplicationMiddlewareExtensions
    {
        public static void UseExtension(this IApplicationBuilder app)
        {


            app.Use(async (context, next) =>
           {
               //await context.Response.WriteAsync($"First call {DateTime.UtcNow}");
               Console.WriteLine($"First call {DateTime.UtcNow}");
               await next();
               Console.WriteLine($"End of First call {DateTime.UtcNow}");
               //await context.Response.WriteAsync($"First call {DateTime.UtcNow}");
           });

            app.UseWhen(cxt => cxt.Request.Query.ContainsKey("role"), appbuilder => appbuilder.Run(async c =>
            {

                Console.WriteLine($"Role is {c.Request.Query["role"]}");
                await c.Response.WriteAsync($"Role is {c.Request.Query["role"]}");
            })
            );


            //app.Use((HttpContext cxt, Func<Task> next) => {
            //    return Task.CompletedTask;
            //});

            app.Map("/map", a =>
            {
                a.Map("/branch", x => x.Run(async context =>
                    await context.Response.WriteAsync("child branch inside map")));

                a.Run(async context =>
                {
                    await context.Response.WriteAsync("New brranch map");
                });
            });

            app.MapWhen(cxt => cxt.Request.Query.ContainsKey("count"), appbuilder => appbuilder.Run(async c =>
            {

                Console.WriteLine($"count is {c.Request.Query["count"]}");
                await c.Response.WriteAsync($"count is {c.Request.Query["count"]}");
                
            })
            );

          

        }

    }
}
