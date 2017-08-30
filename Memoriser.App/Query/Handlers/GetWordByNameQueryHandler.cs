using System.Linq;
using System.Threading.Tasks;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Memoriser.App.Query.Handlers
{
    public class GetWordByNameQueryHandler : IAsyncQueryHandler<GetWordByNameQuery, LearningItem>
    {
        private readonly LearningItemContext _context;

        public GetWordByNameQueryHandler(LearningItemContext context)
        {
            _context = context;
        }

        public async Task<LearningItem> HandleAsync(GetWordByNameQuery query)
        {
            return await _context.LearningItems
                .Where(item => item.ToBeGuessed.Equals(query.ToFind))
                .Include(x => x.Interval)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
