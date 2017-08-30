using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Memoriser.App.Query;
using Xunit;

namespace Memoriser.UnitTests.API.Queries
{
    public class QueryProcessorTests
    {
        [Fact]
        public async Task Can_ProcessQuery()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestQueryHandler>().As<IAsyncQueryHandler<TestQuery, string>>();
            var container = builder.Build();
            var processor = new QueryProcessor(container);

            var query = new TestQuery("Dylan Wragge");
            var result = await processor.ProcessAsync(query);

            result.Should().Be("Hello Dylan Wragge.");
        }

        public class TestQuery : IQuery<string>
        {
            public string Name { get; set; }
            public TestQuery(string name)
            {
                Name = name;
            }
        }

        public class TestQueryHandler : IAsyncQueryHandler<TestQuery, string>
        {
            public Task<string> HandleAsync(TestQuery query)
            {
                return Task.FromResult($"Hello {query.Name}.");
            }
        }
    }
}
