using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;

namespace Memoriser.App.Query.Handlers
{
    public class GetRequiredLearningItemsQueryHandler : IQueryHandler<GetRequiredLearningItemsQuery, LearningItem[]>
    {
        public GetRequiredLearningItemsQueryHandler()
        {

        }

        public LearningItem[] Handle(GetRequiredLearningItemsQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
