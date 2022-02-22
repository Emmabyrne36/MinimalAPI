using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;
using MinimalAPI.Services;

namespace MinimalAPI.APIs
{
    public class LoginApi
    {
        public void Configure(WebApplication? app, WebApplicationBuilder? builder)
        {
            if (app == null) return;
            if (builder == null) return;

            app.MapPost("/login", [AllowAnonymous] async (
                [FromBodyAttribute] UserModel userModel,
                TokenService tokenService,
                IUserRepositoryService userRepositoryService,
                HttpResponse response) =>
            {
                var userDto = userRepositoryService.GetUser(userModel);

                if (userDto == null)
                {
                    response.StatusCode = 401;
                    return;
                }

                var token = tokenService.BuildToken(
                    builder.Configuration["Jwt:Key"], 
                    builder.Configuration["Jwt:Issuer"],
                    builder.Configuration["Jwt:Audience"], 
                    userDto);

                await response.WriteAsJsonAsync(new { token = token });

                return;
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Login")
                .WithTags("Accounts");

            app.MapGet("/authorized-resource", [Authorize] () => "Action Succeeded")
                .Produces(StatusCodes.Status200OK)
                .WithName("Authorized")
                .WithTags("Accounts")
                .RequireAuthorization();
        }
    }
}
