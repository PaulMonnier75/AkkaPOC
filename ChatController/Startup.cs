using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatController.Adapters.LeftSide;
using Core;
using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatController
{
    public class Startup
    {
        private IContainer ApplicationContainer { get; set; }
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

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
            
            builder.RegisterType<ChatAdapter>().As<IChatAdapter>();
            builder.RegisterType<Core.Core>().As<ICore>().SingleInstance();

            return builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseMvc();
        }
    }
}