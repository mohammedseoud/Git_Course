using ElBayt.Common.Infra.Logging;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Logging;
using ElBayt.Common.Mapping;
using ElBayt.Common.Security;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Infra.Context;
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
            services.AddControllers();


            services.AddDbContext<ElBaytContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("ElBaytConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ElBaytContext>();

            services.AddAutoMapper();

            services.AddScoped<IUserIdentity>();
            services.AddScoped<ITypeMapper, TypeMapper>();
            services.AddSingleton<ILogger, Logger>();
            services.AddScoped<IELBaytUnitOfWork, ELBaytUnitOfWork>();
            services.AddScoped<IElBaytServices, ElBaytServices>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
