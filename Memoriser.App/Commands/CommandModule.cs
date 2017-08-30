using Autofac;
using System.Reflection;

namespace Memoriser.App.Commands
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncCommandHandler<>));
        }
    }
}
