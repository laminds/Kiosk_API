using Kiosk.Business.Helpers;
using Kiosk.Business.ViewModels.Account;
using Kiosk.Domain;
using Kiosk.Interfaces.Background;
using Kiosk.Interfaces.Repositories;
using Kiosk.Interfaces.Services;
using Kiosk.Mail;
using Kiosk.Repositories;
using Kiosk.Services;
using Kiosk.UoW;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Kiosk.Domain.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Kiosk.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Kiosk.Services.HubSpot;
using Kiosk.API.Controllers;
using Microsoft.Extensions.FileProviders;
using System.IO;
using MailKit;

namespace Kiosk.API
{
    public partial class Startup
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
            services.AddDbContext<KioskContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("KioskConnection")));
            // services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DgrContext>().AddDefaultTokenProviders();
            services.AddDistributedMemoryCache();
            services.AddAutoMapper(c => c.AddProfile<MapperConfiguration>(), typeof(Startup));

            RegisterRequestLocalizationOptions(services);
            RegisterNewtonsoftJson(services);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SessionWebApI>();

            RegisterADFS(services, GetConfiguration());
            RegisterDI(services);
            // RegisterHangfire(services);
            RegisterSwagger(services);
            RegisterCors(services);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = "microsoft:identityserver:758ac6c5-d1d8-4f12-ae82-463f5c3ffad8";
                options.Authority = "https://identity.youfit.com/adfs";
                options.MetadataAddress = "https://identity.youfit.com/adfs/.well-known/openid-configuration";

                // Ignore HTTPS in development only!
                options.RequireHttpsMetadata = false;

                // Use the public key of ADFS to validate the JSON Web Token.
                //var publicCert = new X509Certificate2("adfs_pkey.cer");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "http://identity.youfit.com/adfs/services/trust",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    RequireExpirationTime = false,
                    RequireSignedTokens = true,
                };

                options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
            });

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint(url: "v1/swagger.json", name: "API V1");
                    });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseStaticFiles();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            //{
            //    AppPath = null,
            //    DashboardTitle = "Hangfire Dashboard"
            //});

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"AgreementContract")),
                RequestPath = new PathString("/AgreementContract")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHangfireDashboard();
            });

            AppSettings.Initialize(Configuration);
            MailSettings.Initialize(Configuration);
            HubSpotConfig.Initialize(Configuration);
            FreshEmail.Initialize(Configuration);
            ABC.Initialize(Configuration);
            AmazonSettings.Initialize(Configuration);
            MemberPlanList.Initialize(Configuration);
            ASH.Initialize(Configuration);
            MemberCheckOutSettings.Initialize(Configuration);
            PlaidSettings.Initialize(Configuration);
            ADFSSettings.Initialize(Configuration);
            //  Jwt.Initialize(Configuration);
        }

        private static void RegisterDI(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            RegisterServices(services);
            RegisterRepositories(services);
            BackgroundServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // services.AddScoped<IPersonService, PersonService>();
            //services.AddScoped<INewMemberService, NewMemberService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IHubSpotService, HubSpotService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IABCService, ABCService>();
            services.AddScoped<IFreshEmailService, FreshEmailService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IAmenitiesService, AmenitiesService>();
            services.AddScoped<ISilverSneakerService, SilverSneakerService>();
            services.AddScoped<IAmazonS3Service, AmazonS3Service>();
            services.AddScoped<IPlaidService, PlaidService>();
            services.AddScoped<IManageMembershipService, ManageMembershipService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ISaveWorkFlowService, SaveWorkFlowService>();
            services.AddScoped<IJiraTicketService, JiraTicketService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            // services.AddScoped<IPersonRepository, PersonRepository>();
            //services.AddScoped<INewMemberRepository, NewMemberRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IAmenitiesRepository, AmenitiesRepository>();
            services.AddScoped<ISilverSneakersRespository, SilverSneakersRepository>();
            services.AddScoped<IPlaidRepository, PlaidRepository>();
            services.AddScoped<IManageMembershipRepository, ManageMembershipRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IJiraTicketRepository, JiraTicketRepository>();
            services.AddScoped<ISaveWorkFlowRepository, SaveWorkFlowRepository>();
        }

        private static void BackgroundServices(IServiceCollection services)
        {
            services.AddScoped<IBackgroundService, Services.BackgroundService>();
            services.AddScoped<IBackgroundMailerJobs, BackgroundMailerJobs>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //            c.OAuth2("oauth2")
                //.Description("OAuth2 Implicit Grant")
                //.Flow("implicit")
                //.AuthorizationUrl("https://my-adfs/adfs/oauth2/authorize")
                //.TokenUrl("https://my-adfs/adfs/oauth2/token")
                //.Scopes(scopes =>
                //{
                //    scopes.Add("email", "Email details");
                //});

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "V1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
            });
        }

        private IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        private static void RegisterADFS(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
            })
            .AddWsFederation(options =>
            {
                options.Wtrealm = configuration.GetSection("ADFSSettings").GetSection("Wtrealm").Value;
                options.MetadataAddress = configuration.GetSection("ADFSSettings").GetSection("MetadataAddress").Value;
            })
            .AddCookie();

        }

        private void RegisterHangfire(IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("KioskConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }).WithJobExpirationTimeout(TimeSpan.FromDays(7)));
            services.AddHangfireServer();
        }

        private void RegisterCors(IServiceCollection services)
        {
            var webUrl = Configuration.GetSection("Urls:FrontEnd").Value;
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder
                    .WithOrigins(webUrl)
                    //.SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    );
            });
        }

        private static void RegisterNewtonsoftJson(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //  options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            });
        }

        private static void RegisterRequestLocalizationOptions(IServiceCollection services)
        {
            services.AddLocalization(opt => { opt.ResourcesPath = "Resource"; });
            services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(
                            opt =>
                            {
                                var supportedCulters = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                            };
                                opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                                opt.SupportedCultures = supportedCulters;
                                opt.SupportedUICultures = supportedCulters;
                            });
        }
    }
}