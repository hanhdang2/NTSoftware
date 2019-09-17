
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Helper;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Repository.Repository;
using NTSoftware.Service;
using NTSoftware.Service.AutoMapper;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace NTSoftware
{
    public class Startup
    {
        private IHostingEnvironment _env { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromHours(15));
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("NTSoftware")));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();

            #region REPOSITOTRY


            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient(typeof(IUnitOfWork), typeof(NTUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(NTRepository<,>));
            services.AddTransient<IContractCompanyRepository, ContractCompanyRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IEmployeeContractRepository, EmployeeContractRepository>();
            services.AddTransient<IEmployeeProjectRepository, EmployeeProjectRepository>();
            services.AddTransient<IDetailUserRepository, DetailUserRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRuleRepository, RuleRepository>();

            #endregion REPOSITOTRY

            #region SERVICE


            services.AddTransient<ISmsSenderService, AuthMessageSenderService>();
            services.AddTransient<IEmailSender, AuthMessageSenderService>();
            services.AddTransient<IDetailUserService, DetailUserService>();
            services.AddTransient<IRuleService, RuleService>();
            services.AddTransient<IContractCompanyService, ContractCompanyService>();
            services.AddTransient<ICompanyDetailService, CompanyDetailService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IEmployeeContractService, EmployeeContractService>();
            services.AddTransient<IEmployeeProjectService, EmployeeProjectService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IFillForm, FillFrom>();

            #endregion SERVICE
            services.Configure<EmailSettingViewModel>(Configuration.GetSection("EmailSettings"));

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "";

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
            });
            var applicationUrl = Configuration["ApplicationUrl"].TrimEnd('/');
            //config authen
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });

            // In production, the Angular files will be served from this directory
            // The port to use for https redirection in production
            if (!_env.IsDevelopment() && !string.IsNullOrWhiteSpace(Configuration["HttpsRedirectionPort"]))
            {
                services.AddHttpsRedirection(options =>
                {
                    options.HttpsPort = int.Parse(Configuration["HttpsRedirectionPort"]);
                });
            }
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddAutoMapper();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "NTSoftware App",
                    Description = "NTSoftware API Swagger surface",
                    Contact = new Contact { Name = "NgocLH", Email = "lhngoc@gmail.com", Url = "" }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Utilities.ConfigureLogger(loggerFactory);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //app.UseIdentityServer();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Swagger UI - QuickApp";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $" V1");
            });


            //Configure Cors
            //app.UseCors("allowSpecificOrigins");
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
