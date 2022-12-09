using ElBayt.Common.Common;
using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Infra.Logging;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Infra.Security;
using ElBayt.Common.Security;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Infra.Context;
using ElBayt.Infra.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using ElBayt.Services.ElBaytServices;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using ElBayt.Core.Mapping;
using ElBayt.Infra.Mapping;

namespace ElBayt_ECommerce.WebAPI
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
            services.AddControllers() .AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null); ;


            services.AddDbContext<ElBaytContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("ElBaytConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ElBaytContext>();


            services.AddAutoMapper(typeof(TypeMapper));
            services.AddSwaggerGen();


            services.AddScoped<IUserIdentity>();       
            services.AddScoped<IShopMapper, ShopMapper>();
            services.AddScoped<ITypeMapper, TypeMapper>();
            services.AddScoped<IFileMapper, FileMapper>();

            services.AddSingleton<ILogger, Logger>();
            services.AddScoped<IELBaytUnitOfWork, ELBaytUnitOfWork>();
            services.AddScoped<IElBaytServices, ElBaytServices>();
            services.AddScoped<IDepartmentsServices, DepartmentsServices>();
            services.AddCors(opt => {
                opt.AddPolicy(CorsOrigin.LOCAL_ORIGIN, builder =>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ELBayt.BackOffice");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void JWTConfigure(IServiceCollection services)
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
