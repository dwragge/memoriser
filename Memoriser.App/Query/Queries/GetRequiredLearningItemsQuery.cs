using System.Threading.Tasks;
using Memoriser.ApplicationCore.Models;

namespace Memoriser.App.Query.Queries
{
    public class GetRequiredLearningItemsQuery : IQuery<Task<LearningItem[]>>
    {
    }
}
