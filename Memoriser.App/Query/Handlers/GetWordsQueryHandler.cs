using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Memoriser.App.Query.Handlers
{
    public class GetWordsQueryHandler : IAsyncQueryHandler<GetWordsQuery, LearningItem[]>
    {
        private readonly LearningItemContext _context;
        public GetWordsQueryHandler(LearningItemContext context)
        {
            _context = context;
        }

        public async Task<LearningItem[]> HandleAsync(GetWordsQuery query)
        {
            return await _context.LearningItems
                .Include(x => x.Interval)
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
