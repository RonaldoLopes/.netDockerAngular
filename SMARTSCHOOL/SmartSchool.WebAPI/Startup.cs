using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.WebAPI.Data;
using System;
using System.IO;
using System.Reflection;

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
                context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"))
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
            //necessario para o automapper procurar quem herda de profile
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //versionamento API
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
             .AddApiVersioning(options =>
             {
                 options.DefaultApiVersion = new ApiVersion(1, 0);
                 options.AssumeDefaultVersionWhenUnspecified = true;
                 options.ReportApiVersions = true;
             });

            services.AddControllers().AddNewtonsoftJson(
                                opt => opt.SerializerSettings.ReferenceLoopHandling =
                                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            //swagger documentação API
            services.AddSwaggerGen(options =>
            {

                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                        options.SwaggerDoc(
                            description.GroupName,
                            new Microsoft.OpenApi.Models.OpenApiInfo()
                            {
                                Title = "SmartSchool API",
                                Version = description.ApiVersion.ToString(),
                                TermsOfService = new Uri("http://SeusTermosDeUso.com"),
                                Description = "A descrição dessa API",
                                License = new Microsoft.OpenApi.Models.OpenApiLicense
                                {
                                    Name = "API License",
                                    Url = new Uri("http://mit.com")
                                },
                                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                                {
                                    Name = "Ronaldo Cassiano Lopes Alves",
                                    Email = "ronaldo@risc.ind.br",
                                    Url = new Uri("http://risc.ind.br")
                                }
                            }
                    );
                }



                //Necessário para incluir o XML na documentação
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //usar swagger
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    options.RoutePrefix = "";
                });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
