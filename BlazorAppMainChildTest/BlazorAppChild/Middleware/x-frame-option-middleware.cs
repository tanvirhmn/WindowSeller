using System.Globalization;

namespace BlazorAppChild.Middleware
{
    public class x_frame_option_middleware
    {
        private readonly RequestDelegate _next;

        public x_frame_option_middleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
          
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");


            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }



    }

    public static class XFrameOptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseXFrameOptions(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<x_frame_option_middleware>();
        }
    }
}
