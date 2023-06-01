using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Stripe;
using ZwajApp.API.Data;
using ZwajApp.API.Helpers;
using Microsoft.OpenApi.Models;
using ZwajApp.API.Data.DAL;
using ZwajApp.API.Data.DAL.Models;

namespace ZwajApp.API
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
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //IdentityBuilder builder = services.AddIdentityCore<User>(opt=>{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequiredLength = 4;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireUppercase = false;
            //});

            services.AddDbContext<DataContext>(x => x.
            UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            .ConfigureWarnings(warnings=>warnings.Ignore())
            );
            IdentityBuilder builder = services.AddIdentityCore<User>(opt=>{
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });
            builder = new IdentityBuilder(builder.UserType,typeof(Role),builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            services.AddAuthorization(
                options=>{
                    options.AddPolicy("RequireAdminRole",policy=>policy.RequireRole("Admin"));
                    options.AddPolicy("ModeratePhotoRole",policy=>policy.RequireRole("Admin","Moderator"));
                    options.AddPolicy("VipOnly",policy=>policy.RequireRole("VIP"));
                }
            );

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers().AddNewtonsoftJson(
                option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            }
            );

            services.AddSingleton(typeof(IConverter),new SynchronizedConverter(new PdfTools()));
            services.AddCors();
            services.AddSignalR();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddAutoMapper(typeof(ZwajRepository).Assembly);
            services.AddTransient<TrialData>();
            
            services.AddScoped<IZwajRepository, ZwajRepository>();
            services.AddScoped<LogUserActivity>();
            //Authentication MiddleWare

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ZwajApp Swagger UI",
                    Description = "ZwajApp Swagger UI",
                });
            });

        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //IdentityBuilder builder = services.AddIdentityCore<User>(opt=>{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequiredLength = 4;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireUppercase = false;
            //});
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            IdentityBuilder builder = services.AddIdentityCore<User>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType,typeof(Role),builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            services.AddAuthorization(
                options=>{
                    options.AddPolicy("RequireAdminRole",policy=>policy.RequireRole("Admin"));
                    options.AddPolicy("ModeratePhotoRole",policy=>policy.RequireRole("Admin","Moderator"));
                    options.AddPolicy("VipOnly",policy=>policy.RequireRole("VIP"));
                }
            );

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(
                option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            }
            );

            services.AddSingleton(typeof(IConverter),new SynchronizedConverter(new PdfTools()));
            services.AddCors();
            services.AddSignalR();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddAutoMapper(typeof(ZwajRepository).Assembly);
            services.AddTransient<TrialData>();
            
            services.AddScoped<IZwajRepository, ZwajRepository>();
            services.AddScoped<LogUserActivity>();
            //Authentication MiddleWare

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test Swagger UI",
                    Description = "Test Swagger UI",
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TrialData trialData)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe:SecretKey").Value);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(BuilderExtensions =>
                {
                    BuilderExtensions.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            trialData.TrialUsers();

            app.UseRouting();

            app.UseCors(x => x.SetIsOriginAllowed(Options=> _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
           
            app.UseEndpoints(routes => {
                routes.MapHub<ChatHub>("/chat");
            });
           
            app.UseAuthentication();
           
            app.UseDefaultFiles();

            app.UseEndpoints(end =>
            {
                end.MapControllers();
            });
            app.UseStaticFiles();

            app.Use(async(context,next) =>{
                await next();
                //if(context.Response.StatusCode== 404){
                //    context.Request.Path="/index.html";
                //    await next();
                //}
                
            });

          
            //app.UseMvc();
        }
    }
}
