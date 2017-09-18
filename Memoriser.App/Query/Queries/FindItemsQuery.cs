using System;
using System.Linq.Expressions;
using Memoriser.ApplicationCore.Models;

namespace Memoriser.App.Query.Queries
{
    public class FindItemsQuery : IQuery<LearningItem[]>
    {
        public Expression<Func<LearningItem, bool>> Predicate { get; }

        private FindItemsQuery(Expression<Func<LearningItem, bool>> predicate)
        {
            Predicate = predicate;
        }

        public static FindItemsQuery All => new FindItemsQuery(x => true);
        public static FindItemsQuery ByWord(string word) => new FindItemsQuery(x => x.ToBeGuessed == word);
    }
}
