using System.Threading.Tasks;

namespace Memoriser.App.Query
{
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<Task<TResult>>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
