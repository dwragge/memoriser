using Memoriser.App.Commands;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Commands.Handlers;
using Memoriser.App.Query;
using Memoriser.App.Query.Handlers;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Memoriser.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Memoriser.App
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
            services.AddDbContext<LearningItemContext>(options =>
                options.UseSqlServer(Configuration["DefaultConnection"]));

            services.AddTransient<LearningItemContext>();
            services.AddTransient<IAsyncQueryHandler<GetRequiredLearningItemsQuery, LearningItem[]>, GetRequiredLearningItemsQueryHandler>();
            services.AddTransient<IAsyncCommandHandler<AddWordCommand>, AddWordCommandHandler>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
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

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
