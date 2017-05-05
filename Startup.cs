using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using eCommerceReloaded.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace eCommerceReloaded
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            StripeConfiguration.SetApiKey(Configuration["DBInfo:ApiKey"]);
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StripeConfiguration.SetApiKey(Configuration["DBInfo:ApiKey"]);
            // Add framework services.
            services.AddDbContext<eCommerceReloadedContext>(options => options.UseNpgsql(Configuration["DBInfo:ConnectionString"]));
            services.Configure<MSApiKeyOption>(Configuration);
            services.AddMvc();
            services.AddSession();
        }
    }
    
}
