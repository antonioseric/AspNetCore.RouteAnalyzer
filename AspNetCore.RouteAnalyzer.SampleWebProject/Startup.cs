using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace AspNetCore.RouteAnalyzer.SampleWebProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IRouteAnalyzer m_routeAnalyzer;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouteAnalyzer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime,
            IRouteAnalyzer routeAnalyzer
        )
        {
            m_routeAnalyzer = routeAnalyzer;

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseEndpoints(config =>
            {
                config.MapControllers();
                config.MapRouteAnalyzer();
            });

            // Lifetime events
            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopping.Register(OnStopping);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        void OnStarted()
        {
            var infos = m_routeAnalyzer.GetAllRouteInformations();
            Debug.WriteLine("======== ALL ROUTE INFORMATION ========");
            foreach (var info in infos)
            {
                Debug.WriteLine(info.ToString());
            }
            Debug.WriteLine("");
            Debug.WriteLine("");
        }

        void OnStopping()
        {
        }

        void OnStopped()
        {
        }
    }
}
