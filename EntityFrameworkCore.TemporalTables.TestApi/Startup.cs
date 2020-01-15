using EntityFrameworkCore.TemporalTables.Extensions;
using EntityFrameworkCore.TemporalTables.TestApi.Configurations;
using EntityFrameworkCore.TemporalTables.TestApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCore.TemporalTables.TestApi
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
            services.AddControllers();

            // Beginning of added lines for Temporal tables.
            services.AddDbContextPool<Context>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseInternalServiceProvider(provider);
            });
            services.AddEntityFrameworkSqlServer();
            services.RegisterTemporalTablesForDatabase<Context>();
            // Done adding lines for Temporal tables.
            
            services.AddSwagger(); // Added for easier debugging.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwashbuckleSwagger();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Context>();
                context.Database.Migrate();
            }
        }
    }
}
