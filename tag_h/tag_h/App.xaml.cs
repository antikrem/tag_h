using System.Windows;
using System.Linq;

using Autofac;

using tag_h.Helper.Injection;
using tag_h.Persistence;
using tag_h.Tasks;
using tag_h.Views;

namespace tag_h
{
    public partial class App : Application
    {
        //private IContainer GenerateContainer()
        //{
        //    var container = new ContainerBuilder();

        //    var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        //    var temp =
        //        container.RegisterAssemblyTypes(assembly)
        //        .Where(x => x.IsDefined(typeof(Injectable), false));

        //    var injectables =
        //        container.RegisterAssemblyTypes(assembly)
        //        .Where(x => x.IsDefined(typeof(Injectable), false))
        //        .AsImplementedInterfaces()
        //        .InstancePerRequest();

        //    return container.Build();
        //}

        private IContainer GenerateContainer()
        {
            var container = new ContainerBuilder();

            container.RegisterType<DatabaseConnection>().As<IDatabaseConnection>().SingleInstance();
            container.RegisterType<Database>().As<IDatabase>().SingleInstance();
            container.RegisterType<TaskRunner>().As<ITaskRunner>().SingleInstance();
            container.RegisterType<TagHApplication>().As<ITagHApplication>().SingleInstance();
            container.RegisterType<HImageRepository>().As<IHImageRepository>().SingleInstance();
            container.RegisterType<TagRespository>().As<ITagRepository>().SingleInstance();
            container.RegisterType<MainWindow>().As<IMainWindow>().SingleInstance();


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
