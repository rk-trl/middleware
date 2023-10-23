using Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSingleton<IPrint, Print>();

var app = builder.Build();



//app.Use(async (context, next) =>
//{
//    app.Logger.LogInformation($" First middleware start time {DateTime.UtcNow}");
//    //next.Invoke(context);
//    await next();
//    app.Logger.LogInformation($" First middleware end time {DateTime.UtcNow}");
//});

//app.UseWhen(cxt => cxt.Request.Query.ContainsKey("role"), appbuilder => appbuilder.Run(async c =>
//{

//    Console.WriteLine($"Role is {c.Request.Query["role"]}");
//    app.Logger.LogInformation($"Role is {c.Request.Query["role"]}");
//    await c.Response.WriteAsync($"Role is {c.Request.Query["role"]}");
//})
//);


////app.Use((HttpContext cxt, Func<Task> next) => {
////    return Task.CompletedTask;
////});

//app.Map("/map", a =>
//{
//    a.Map("/branch", x => x.Run(async context => 
//        await context.Response.WriteAsync("child branch inside map")));

//    a.Run(async context =>
//    {
//        await context.Response.WriteAsync("New brranch map");
//    });
//});

//app.MapWhen(cxt => cxt.Request.Query.ContainsKey("count"), appbuilder => appbuilder.Run(async c =>
//{

//    Console.WriteLine($"count is {c.Request.Query["count"]}");
//    app.Logger.LogInformation($"count is {c.Request.Query["count"]}");
//    await c.Response.WriteAsync($"count is {c.Request.Query["count"]}");
//})
//);

//app.UseRouting();
//// Map routes to different controllers and action methods

//app.Map("/WeatherForecast", adminApp =>
//{
//    adminApp.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllerRoute(
//            name: "WeatherForecast",
//            pattern: "{controller=WeatherForecast}/{action=Get}");
//    });
//});



app.UseExtension();

app.UseRouting();
// Map routes to different controllers and action methods

app.Map("/WeatherForecast", adminApp =>
{
    adminApp.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "WeatherForecast",
            pattern: "{controller=WeatherForecast}/{action=Get}");
    });
});

//app.Map("/WeatherForecast_Auto", custFunc);
//void custFunc(IApplicationBuilder builder)
//{
//    throw new NotImplementedException();
//}

app.UseMiddleware<CustomMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
//app.Run(async ctx => await ctx.Response.WriteAsync("Hello"));
