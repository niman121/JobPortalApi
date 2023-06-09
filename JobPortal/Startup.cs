using JobPortal.Data;
using JobPortal.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using JobPortal.Service.Services;
using JobPortal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JobPortal.Data.Repositories.Interfaces;
using JobPortal.Data.Repositories;
using JobPortal.ApiHelper;
using JobPortal.Service.AppSettings;

namespace JobPortal
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
            services.AddDbContext<JobDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("jobPortalDbConnection")));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Job Portal Api", Version = "v1" });
            });

            //adding dependencies and repository in DI Container
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IRecruiterService, RecruiterService>();
            services.AddScoped<IJobPortalUserRepository, JobPortalUserRepository>();
            services.AddScoped<IJobPortalCandidateRepository, JobPortalCandidateRepository>();
            services.AddScoped<IJobPortalAdminRepository, JobPortalAdminRepository>();
            services.AddScoped<IJobPortalJobRepository, JobPortalJobRepository>();
            services.AddScoped<IJobPortalRecruiterRepository, JobPortalRecruiterRepository>();
            services.AddScoped<IJobPortalOtpRepository, JobPortalOtpRepository>();
            services.AddScoped<IOtpService, OptService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJobPortalRoleRepository, JobPortalRolesRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings")).Configure<JwtSettings>(Configuration.GetSection("Jwt"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "JobPortal.API v1"));
                
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
