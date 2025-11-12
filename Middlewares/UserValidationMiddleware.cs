using LionStrategiesTest.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LionStrategiesTest.Middlewares
{
    public class UserValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {

            if ((context.Request.Path.StartsWithSegments("/api/Users") && context.Request.Method == "POST") 
                || context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("email", out var email))
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Email header is missing.");
                return;
            }

            var user = await userRepository.GetByEmailAsync(email.ToString());

            if (user == null)
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("User not found.");
                return;
            }

            context.Items["UserRole"] = user.Role;

            await _next(context);
        }
    }
}