using Inyector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetWebAppSample
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
            services.AddMvc();

            //// use injector
            //services.UseInjector(config =>
            //{
            //    config.Rules.Add(new NamingConventionRule(typeof(ICarRepository).Assembly, InyectorType.Singleton,
            //        "Repository"));
            //});

            InyectorStartup.Init(c =>
            {
                c.AddAssemblyRule(typeof(Startup).Assembly,
                    (t1, t2) =>
                    {
                        return $"I{t1.Name}" == t2.Name;
                    },
                    (t1, t2) => services.AddSingleton(t2, t1));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}