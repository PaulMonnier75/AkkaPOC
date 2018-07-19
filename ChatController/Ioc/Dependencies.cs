using System;
using Autofac;

namespace ChatController.Ioc
{
    public class Dependencies
    {
        private static readonly Dependencies Instance = new Dependencies();
        private static ContainerBuilder _builder;
        private static IContainer _container;

        public static Dependencies Configure()
        {
            _builder = new ContainerBuilder();

            // Right Side Adapters

            // Core Hexagone

            // Left Side Adapters

            return Instance;
        }

        public Dependencies Register<T1, T2>() where T2 : T1
        {
            _builder.RegisterType<T2>().As<T1>().SingleInstance();
            return this;
        }

        public Dependencies RegisterFactory<T1>(Func<IComponentContext, object> factory)
        {
            _builder.Register(factory).As<T1>();
            return this;
        }

        public static void BuildContainer()
        {
            _container = _builder.Build();
        }

        public static T Resolve<T>()
        {
            if (_builder == null)
                throw new InvalidOperationException("Builder not initialized");
            if (_container == null)
                throw new InvalidOperationException("Container not initialized");
            return _container.Resolve<T>();
        }
    }
}