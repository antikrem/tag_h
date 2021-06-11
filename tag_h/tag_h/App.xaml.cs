using System.Windows;
using System.Linq;

using Autofac;

using tag_h.Injection;
using System;

namespace tag_h
{
    public partial class App : Application
    {
        private IContainer GenerateContainer()
        {
            var container = new ContainerBuilder();

            foreach ((Type entry, Type service) in InjectionModule.GetInjectionDefinitions())
                container.RegisterType(service).As(entry).SingleInstance();

            return container.Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = GenerateContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var application = scope.Resolve<ITagHApplication>();
                application.Show();
            }

            container.ComponentRegistry.Registrations
                .Where(r => typeof(IStopOnDejection).IsAssignableFrom(r.Activator.LimitType))
                .Select(r => r.Services.First())
                .Select(s => container.ResolveService(s) as IStopOnDejection)
                .Distinct()
                .ToList()
                .ForEach(x => x.Stop());

        }
    }
}
