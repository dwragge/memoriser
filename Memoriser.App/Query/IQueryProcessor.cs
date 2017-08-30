using System.Threading.Tasks;

namespace Memoriser.App.Query
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}
