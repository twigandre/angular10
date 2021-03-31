using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEPractices4ML.Dao;
using SEPractices4ML.Dao.Repository;
using SEPractices4ML.Dao.Entityes;
using SEPractices4ML.BusinessLogic.User;
using SEPractices4ML.BusinessLogic.Member;
using SEPractices4ML.BusinessLogic.Notificacao;
using SEPractices4ML.BusinessLogic.Practices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

namespace SEPractices4ML
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
              
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc();

            #region Autentication Configs

            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            #region Cors - Only Angular Localhost
            services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(Environment.GetEnvironmentVariable("URL_FRONT")).AllowAnyHeader().AllowAnyMethod();
                    });
                });
            #endregion

            #region Data Base Configuration
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<Context>(options => options.UseNpgsql("Host=" + Environment.GetEnvironmentVariable("HOST_DATABASE") +";" +
            "                                                    Port=" + Environment.GetEnvironmentVariable("DATABASE_PORT") +";" +
            "                                                    Database=" + Environment.GetEnvironmentVariable("DATABASE_NAME") + ";" +
            "                                                    Username=" + Environment.GetEnvironmentVariable("DATABASE_USER") + ";" +
            "                                                    Password=" + Environment.GetEnvironmentVariable("DATABSE_PSW") + ";" +
            "                                                    timeout=300"));
            #endregion

            #region Dependency Injection - Entity Framework
            services.AddScoped<IRepository<UserEntity>, EntityRepository<UserEntity>>();
            services.AddScoped<IRepository<MembersEntity>, EntityRepository<MembersEntity>>();
            services.AddScoped<IRepository<NotificationEntity>, EntityRepository<NotificationEntity>>();
            services.AddScoped<IRepository<PracticesEntity>, EntityRepository<PracticesEntity>>();
            services.AddScoped<IRepository<PracticesAuthorsEntity>, EntityRepository<PracticesAuthorsEntity>>();
            services.AddScoped<IRepository<PracticesAnexoEntity>, EntityRepository<PracticesAnexoEntity>>();
            #endregion

            #region Dependency Injection - BusinessLogic
            services.AddScoped<IUserBll, UserBll>();
            services.AddScoped<IMemberBll, MemberBll>();
            services.AddScoped<INotificationBll, NotificationBll>();
            services.AddScoped<IPracticesBll, PracticesBll>();
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

            // diz ao .NET que de fato estamos utilizando autenticação e autorização nesta API
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
