using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shop.DataLayer.Models;
using Shop.DataLayer.Repositories;
using Shop.DataLayer.Repositories.Abstracts;
using Shop.WebApi.Mapping;

namespace Shop.WebApi
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "PickPoint Shop WebApi", Version = "v1"}); });
            
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton<IOrderRepository>(OrderListRepository.Instance);
            services.AddSingleton<IPostamatRepository>(prov =>
                {
                    var postamats = new List<Postamat>();
                    postamats.Add(new Postamat() {Id = "mhkpostomat1", Address = "Дагестан, Махачкала, Коркмасова ул., д. 14", IsActive = true});
                    postamats.Add(new Postamat() {Id = "mhkpostomat2", Address = "Дагестан, Махачкала, Умаханова ул., д. 25А", IsActive = true});
                    postamats.Add(new Postamat() {Id = "mhkpostomat3", Address = "Дагестан, Махачкала, Степана Разина ул., д. 13", IsActive = true});
                    postamats.Add(new Postamat() {Id = "mhkpostomat4", Address = "Дагестан, Махачкала, Ленина ул., д. 1"});

                    return new PostamatListRepository(postamats);
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.WebApi v1"));
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}