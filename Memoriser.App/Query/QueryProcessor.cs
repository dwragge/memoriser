using System.Threading.Tasks;
using Autofac;

namespace Memoriser.App.Query
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly ILifetimeScope _container;
        public QueryProcessor(ILifetimeScope container)
        {
            _container = container;
        }

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _container.Resolve(handlerType);
            return handler.HandleAsync((dynamic)query);
        }
    }
}
