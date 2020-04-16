using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Entity;
using RandomUserApi.Infrastructure.Options;
using System.IO;

namespace RandomUserApi.Infrastructure.Extension
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSwaggerForDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(o =>
            {
                o.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }

        public static void UseTestData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<RepositoryContext>();
            AddTestData(context);
        }

        private static void AddTestData(RepositoryContext context)
        {
            var usersDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Data\Users.json");
            var users = UserDataMock.GetUsersWithAutoIncrement(usersDataPath);
            foreach (var user in users)
            {
                context.Users.AddRange(user);
                context.SaveChanges();
            }
        }
    }
}
