namespace WebAppTechnology.Middleware;

public class RequestLimitingMiddleware
{
    private static int _currentRequestCount = 0;
    private readonly RequestDelegate _next;
    private readonly int _parallelLimit;

    public RequestLimitingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<RequestLimitingMiddleware> logger)
    {
        _next = next;
        _parallelLimit = configuration.GetValue<int>("Settings:ParallelLimit");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (Interlocked.Increment(ref _currentRequestCount) > _parallelLimit)
        {
            Interlocked.Decrement(ref _currentRequestCount);
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("503 Service Unavailable");
            return;
        }

        try
        {
            await _next(context);
        }
        finally
        {
            Interlocked.Decrement(ref _currentRequestCount);
        }
    }
}