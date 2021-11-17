using CreateApiEmty.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace CreateApiEmty
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
          Configuration =  configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;MultipleActiveResultSets=true";
            string conn = "Server=(localdb)\\mssqllocaldb;Database=Persons;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<UsersContext>(options => options.UseSqlServer(con));
            services.AddDbContext<PersonContext>(options => options.UseSqlServer(conn));
            services.AddMvc()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(opts =>
                {
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("xml", new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/xml"));
                });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

         
        }
    }
}
