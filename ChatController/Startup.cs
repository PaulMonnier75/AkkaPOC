using System;
using Akka.Actor;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatController.Adapters.LeftSide;
using Core;
using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            
            Core.Core.ConfigureIoc(builder);

            var actorSystem = ActorSystem.Create("ActorSystem");

            builder.Register((c, p) => new Core.Core(actorSystem)).As<ICore>().SingleInstance();
            builder.Register((c, p) => new MediaAdapter(c.Resolve<ICore>())).As<IMediaAdapter>();
            builder.Register((c, p) => new HomeAutomationAdapterAdapter(c.Resolve<ICore>())).As<IHomeAutomationAdapter>();

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
    }
}