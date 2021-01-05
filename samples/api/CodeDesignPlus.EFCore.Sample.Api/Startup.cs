using AutoMapper;
using CodeDesignPlus.EfCore.Sample.Api.SqlServer;
using CodeDesignPlus.EFCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace CodeDesignPlus.EFCore.Sample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            this.AddDbContext(services);

            this.AddSwagger(services);

            services.AddEFCore(this.Configuration)
                    .AddIdentityService<string>()
                    .AddRepositories<long, string, SqlServerContext>();


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeDesignPlus.EFCore.Sample.Api");
            });

            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityService<string>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDbContext(IServiceCollection services)
        {
            var migration = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<SqlServerContext>(options =>
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlServer(this.Configuration.GetConnectionString("DefeaultConnection"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migration);

                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                    });
                }
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var version = "v1";

                options.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"CodeDesignPlus.EFCore.Sample.Api {version}",
                    Description = "Api de ejemplo con el patron de repositorio",
                    Version = version,
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "CodeDesignPlus",
                        Email = "codedesignplus@gmail.com"
                    }
                });
            });
        }
    }
}
