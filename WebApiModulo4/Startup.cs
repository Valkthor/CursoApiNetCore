using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiModulo4.Contexts;
using WebApiModulo4.Controllers;
using WebApiModulo4.Helpers;
using WebApiModulo4.Services;

namespace WebApiModulo4
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
            //se agrega la configracion paa los filtros de acciones creados en helpers
            services.AddScoped<MiFiltroDeAccion>();

            // se habilita un conjunto de servicios para la funcionalidad de guardar informacion en cache
            services.AddResponseCaching();

            //aca se indica que clase se debe usar si la interfaz es solicitada.
            services.AddTransient<IClaseB, ClaseB2>();

            services.AddTransient<ClaseB>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddControllers(options =>
            {
                
                options.Filters.Add(new MiFiltroDeExcepcion());
                // Si hubiese Inyección de dependencias en el filtro
                //options.Filters.Add(typeof(MiFiltroDeExcepcion)); 
            });

            //se agrega autenticacion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
app.UseRouting();
            // se configura un middleware de cache
            app.UseResponseCaching(); 

            //agregar autenticacion
            app.UseAuthentication();

            app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
        }
    }
}
