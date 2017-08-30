using Memoriser.ApplicationCore.Models;

namespace Memoriser.App.Query.Queries
{
    public class GetWordByNameQuery : IQuery<LearningItem>
    {
        public string ToFind { get; set; }

        public GetWordByNameQuery(string toFind)
        {
            ToFind = toFind;
        }
    }
}
