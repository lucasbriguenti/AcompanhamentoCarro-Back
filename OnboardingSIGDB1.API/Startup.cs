using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.OpenApi.Models;
using OnboardingSIGDB1.API.Filter;

namespace OnboardingSIGDB1.API
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
            services.AddMvc(opt => opt.Filters.Add<NotificationFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //MapeamentoAutoMapper(services);

            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("OnboardingSIGDB1.API"));
                //options.UseLazyLoadingProxies();
            });

            IoC.Startup.MapeamentoGenerico(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Registrador de Kilometragem", Version = "v1" });
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Use(async (context, next) =>
            //{
            //    await next.Invoke();
            //    string method = context.Request.Method;

            //    var allowedMethodsToCommit = new string[] {​​​​ "POST", "PUT", "DELETE" }​​​​;

            //    if (allowedMethodsToCommit.Contains(method))
            //    {​​​​
            //        var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
            //        var contextoDeNotificacaoDeDominio = context.RequestServices.GetService(typeof(IDomainNotificationHandlerAsync<DomainNotification>));
            //        var notificacaoDeDominio = (IDomainNotificationHandlerAsync<DomainNotification>)contextoDeNotificacaoDeDominio;

            //        if (!notificacaoDeDominio.HasNotifications())
            //            unitOfWork.Commit();

            //    }​​​​
            //});
            


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
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Registrador de Kilometragem V1");
            });

        }
        //private void MapeamentoAutoMapper(IServiceCollection services)
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<EmpresaDto, Empresa>()
        //        .ForMember(x => x.DataFundacao, opt => opt.MapFrom(x => x.DataFundacao ?? null));

        //        cfg.CreateMap<FuncionarioDto, Funcionario>()
        //        .ForMember(x => x.DataContratacao, opt => opt.MapFrom(x => x.DataContratacao ?? null));

        //        cfg.CreateMap<CargoDto, Cargo>();

        //        cfg.CreateMap<VincularFuncionarioCargoDto, FuncionarioCargo>();
        //    });

        //    var mapper = config.CreateMapper();
        //    services.AddSingleton(mapper);
        //}
    }
}
