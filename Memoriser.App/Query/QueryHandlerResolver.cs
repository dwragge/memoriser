using Autofac;

namespace Memoriser.App.Query
{
    public class QueryHandlerResolver : IQueryHandlerResolver
    {
        private readonly IContainer _container;
        public QueryHandlerResolver(IContainer container)
        {
            _container = container;
        }

        public IAsyncQueryHandler<IQuery<TResult>, TResult> Resolve<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _container.Resolve(handlerType);
            return handler;
        }
    }
}
