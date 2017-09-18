using System.Threading.Tasks;

namespace Memoriser.App.Query
{
    public interface IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> QueryAsync(TQuery query);
    }
}
