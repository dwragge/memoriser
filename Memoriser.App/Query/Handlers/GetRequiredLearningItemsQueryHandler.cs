using System.Linq;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Memoriser.App.Query.Handlers
{
    public class GetRequiredLearningItemsQueryHandler : IAsyncQueryHandler<GetRequiredLearningItemsQuery, LearningItem[]>
    {
        private readonly LearningItemContext _context;
        public GetRequiredLearningItemsQueryHandler(LearningItemContext context)
        {
            _context = context;
        }

        public async Task<LearningItem[]> HandleAsync(GetRequiredLearningItemsQuery query)
        {
            return await _context.LearningItems
                .Include(x => x.Interval)
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
