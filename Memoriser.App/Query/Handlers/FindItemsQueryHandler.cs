using System.Linq;
using Memoriser.App.Query.Queries;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Memoriser.ApplicationCore.LearningItems;

namespace Memoriser.App.Query.Handlers
{
    public class FindItemsQueryHandler : IAsyncQueryHandler<FindItemsQuery, LearningItem[]>
    {
        private readonly LearningItemContext _context;
        public FindItemsQueryHandler(LearningItemContext context)
        {
            _context = context;
        }

        public async Task<LearningItem[]> QueryAsync(FindItemsQuery query)
        {
            return await _context.LearningItems
                .Where(query.Predicate)
                .Include(x => x.Interval)
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
