using System.Threading.Tasks;

namespace Memoriser.App.Query
{
    public interface IQueryHandlerResolver
    {
        IAsyncQueryHandler<IQuery<TResult>, TResult> Resolve<TResult>(IQuery<TResult> query);
    }
}
