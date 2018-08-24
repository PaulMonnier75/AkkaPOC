using System;
using System.ComponentModel;
using Akka.Actor;
using Akka.Util.Internal;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatController.Adapters.LeftSide;
using Core;
using Core.IAdapters.LeftSide;
using Core.IAdapters.RightSide;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plugins.Repositories;
using IContainer = Autofac.IContainer;

namespace ChatController
{
    public class Startup
    {
        private IContainer ApplicationContainer { get; set; }
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ApplicationContainer = CreateAndRegisterDependencies(services);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        private IContainer CreateAndRegisterDependencies(IServiceCollection services)
        {
            var builder = new ContainerBuilder();            
            
            builder.Populate(services);
            builder.RegisterType<ChatRepository>().As<IChatRepositoryAdapter>();
            builder.RegisterInstance(GetLuisSecrets());
            
            Core.Core.ConfigureIoc(builder);

            var actorSystem = ActorSystem.Create("ActorSystem");

            builder.Register((c, p) => new Core.Core(actorSystem)).As<ICore>().SingleInstance();
            builder.Register((c, p) => new ChatAdapter(c.Resolve<ICore>())).As<IChatAdapter>();

            var container = builder.Build();
            
            Core.Core.Resolver(container, actorSystem);

            return container;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }

        private LuisSecrets GetLuisSecrets()
        {
            var luisConfig = new LuisSecrets();
            
            Configuration.GetSection("Luis").Bind(luisConfig);

            return luisConfig;
        }      
    }
}