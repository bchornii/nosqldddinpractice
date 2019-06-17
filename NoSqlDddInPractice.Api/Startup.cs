using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NoSqlDddInPractice.Api.Infrastructure.AutofacModules;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Data;
using NoSqlDddInPractice.Data.Configurations;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace NoSqlDddInPractice.Api
{
    public class Startup
    {
        public string AppName => "SnackApp";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddCustomDbContext(Configuration)
                .AddCustomSwagger(appName: AppName);

            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly,
                typeof(InitializeSnakMachineCommand).GetTypeInfo().Assembly);

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new MediatorModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var basePath = Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH") ?? "/";
                if (!basePath.EndsWith("/"))
                {
                    basePath += "/";
                }

                c.SwaggerEndpoint($"{basePath}swagger/{AppName}/swagger.json", $"{AppName}");
            });
        }
    }

    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddControllersAsServices();    //Injecting Controllers themselves thru DI
                                                    //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            EntityTypeConfiguration.Configure();
            EnumerationTypeConfiguration.Configure();
            SnackTypeTypeConfiguration.Configure();
            SnackMachineTypeConfiguration.Configure();

            services.AddScoped(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new AppDbContext(connectionString, serviceProvider.GetService<IMediator>());
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string appName)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(appName, new Info
                {
                    Title = $"{appName} HTTP API",
                    Version = appName
                });
            });

            return services;
        }
    }
}
