using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.WebAPI.Data;
using AutoMapper;
using System;

namespace SmartSchool.WebAPI
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
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            /*
            *instâncias do controller(objetos)
            *services.AddSingleton<IRepository, Repository>();
            *Quando padrão singletown, ele cria a requisição na primeira vez que  é instanciado
            *e toda vez que é necessário, ele requisita esta instância
            *AddSingleton: -> Cria uma única instância do serviço quando é solicitado pela primeira vez
            *                 e reutiliza essa mesma instância em todos os locais em que esse serviço é necessário.
            */
            /*
            *services.AddTransient<IRepository, Repository>();
            *AddTransient: -> Sempre gerará uma nova instância para cada item encontrado que possua
            *                 tal dependência, ou seja, se houver 5 dependências serão 5 instâncias diferentes
            */

            /*
            *services.AddScoped<IRepository, Repository>();
            *AddScoped: -> Essa é diferente da Transient que garante que em uma requisição seja criada
            *              uma instância de uma classe onde se houver outras dependências, seja utilizada
            *              essa única instância pra todas, renovando somente nas requisições subsequentes,
            *              mas, mantendo essa obrigatoriedade
            */
            services.AddScoped<IRepository, Repository>();
            
            services.AddControllers().AddNewtonsoftJson(
                                opt => opt.SerializerSettings.ReferenceLoopHandling  = 
                                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            //necessario para o automapper procurar quem herda de profile
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
