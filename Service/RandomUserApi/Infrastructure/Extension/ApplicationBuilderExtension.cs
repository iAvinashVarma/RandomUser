using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Entity;
using RandomUser.Business.Model;
using RandomUserApi.Infrastructure.Options;
using System;
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

        public static void UseCustomMvc(this IApplicationBuilder app)
        {
            app.UseMvc(r =>
            {
                r.Select().Filter();
                r.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var oDataBuilder = new ODataConventionModelBuilder();
            oDataBuilder.EntitySet<User>("Users");
            return oDataBuilder.GetEdmModel();
        }
    }
}
