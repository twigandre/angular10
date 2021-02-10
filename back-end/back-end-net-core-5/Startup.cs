using System;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using back_end_net_core_5.Dao;
using back_end_net_core_5.Dao.Repository;
using back_end_net_core_5.Dao.Entityes;
using back_end_net_core_5.BusinessLogic;

namespace back_end_net_core_5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
                        
            #region Cors - Only Angular Localhost
                services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
                });
            #endregion

            #region Data Base Configuration
            //add name="DatabaseContext" connectionString="Server=192.168.0.251;Database=dbh_10306_cadsuf;User ID=dev;pwd=dev;MultipleActiveResultSets=true" providerName="System.Data.SqlClient"
            var ConnectionString = @"server=localhost;database=aplication;user=root;password=123456";
                services.AddDbContext<Context>(options => options.UseSqlServer(ConnectionString));
                services.AddScoped<Context>();

            #endregion
                        
            #region Repository - Entity Framework
                // New entities need to be mapped here.
                services.AddScoped<IRepository<LoginEntity>, EntityRepository<LoginEntity>>();
            #endregion

            #region Dependency Injection - BusinessLogic
                //Blls and your Intefaces, need to be mapped here
                services.AddScoped<ICriptografiaBll, CriptografiaBll>();
                services.AddScoped<IControleUsuarioBll, ControleUsuarioBll>();
                services.AddScoped<IUploadBucketBll, UploadBucketBll>();
                services.AddScoped<ITransferUtility, TransferUtility>();
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
