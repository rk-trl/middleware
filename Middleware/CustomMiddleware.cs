namespace Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPrint _print;

        public CustomMiddleware(RequestDelegate next, IPrint print)
        {
            _next = next;
            _print = print;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Custom Middleware");
            _print.Print();
            await _next.Invoke(httpContext);
        }

    }
}
