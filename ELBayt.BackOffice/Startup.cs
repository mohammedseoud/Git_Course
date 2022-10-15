using ElBayt.Common.Infra.Logging;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Security;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Infra.Context;
using Microsoft.IdentityModel.Tokens;
using ElBayt.Infra.UnitOfWork;
using ElBayt.Services.ElBaytServices;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Infra.Security;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ElBayt.Common.Common;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using ElBayt.Infra.Mapping;
using ElBayt.Core.Mapping;
using Microsoft.OpenApi.Models;

namespace ELBayt.WebAPI
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
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers().AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null); ;

            services.AddSwaggerGen();

            services.AddDbContext<ElBaytContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("ElBaytConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ElBaytContext>();

            JWTConfigure(services);

            

            services.AddAutoMapper(typeof(TypeMapper));

          

            services.AddScoped<IUserIdentity>();
            services.AddScoped<ITypeMapper, TypeMapper>();
            services.AddScoped<IShopMapper, ShopMapper>();
            services.AddScoped<IFileMapper, FileMapper>();

            services.AddSingleton<ILogger, Logger>();
            services.AddScoped<IELBaytUnitOfWork, ELBaytUnitOfWork>();
            services.AddScoped<IElBaytServices, ElBaytServices>();
            services.AddScoped<IDepartmentsServices, DepartmentsServices>();
            services.AddCors(opt=> {
                opt.AddPolicy(CorsOrigin.LOCAL_ORIGIN , builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
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
            app.UseCors(CorsOrigin.LOCAL_ORIGIN);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Configuration["FilesInfo:Path"]),
                RequestPath = new PathString(Configuration["FilesInfo:Folder"])
            });

            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ELBayt.BackOffice");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void JWTConfigure(IServiceCollection services)
        {
            services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddAuthentication(cgf =>
            {
                cgf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cgf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        }
    }
}