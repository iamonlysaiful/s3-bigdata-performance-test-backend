using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using S3ITEST.API.Queries;
using S3ITEST.API.Schema;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.DB.EntityModels;
using S3ITEST.TYPES;

namespace S3ITEST.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //Cors Handler
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddTransient<IBuildingRepositories, BuildingRepositories>();
            services.AddTransient<IObjectRepositories, ObjectRepositories>();
            services.AddTransient <IDataFieldRepositories, DataFieldRepositories>();
            services.AddTransient <IReadingRepositories, ReadingRepositories>();

            var connection = new DapperDBContext(Configuration.GetConnectionString("DatabaseITEST"));
            services.AddSingleton(connection);

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<RootQuery>();
            services.AddSingleton<BuildingQuery>();
            services.AddSingleton<DataFieldQuery>();
            services.AddSingleton<ObjectQuery>();
            services.AddSingleton<ReadingQuery>();

            services.AddSingleton<ObjectType>();
            services.AddSingleton<DataFieldType>();
            services.AddSingleton<BuildingType>();
            services.AddSingleton<ReadingResponseType>();

            services.AddSingleton<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddSingleton<ISchema, S3ITESTSchema>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseGraphiQl("/graphiql");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
