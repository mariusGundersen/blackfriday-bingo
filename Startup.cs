using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blackfriday_bingo.Pingdom;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlackFridayBingo
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
            services
                .UseNamespaceViewLocation()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                
            PingdomService.Run(new List<string>
            {                            {
                "https://dev.komplett.no/",
                "https://dev.komplett.no/department/10000/datautstyr",
                "https://dev.komplett.no/kampanje/8631/datautstyr/periferiutstyr/skjermer/skjermer/skjermguiden?tag=tnpanel"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
