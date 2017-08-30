using Autofac;
using System.Reflection;

namespace Memoriser.App.Query
{
    public class QueryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncQueryHandler<,>));

            builder.RegisterType<QueryProcessor>().As<IQueryProcessor>();
        }
    }
}
