using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Memoriser.App.Commands;
using Xunit;

namespace Memoriser.UnitTests.API.Commands
{
    public class CommandDispatcherTests
    {
        public static int Counter = 1;
        [Fact]
        public async Task Can_DispatchCommand()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestCommandHandler>().As<IAsyncCommandHandler<TestCommand>>();
            var container = builder.Build();
            var dispatcher = new CommandDispatcher(container);

            var command = new TestCommand(10);
            await dispatcher.DispatchAsync(command);

            Counter.Should().Be(10);
        }

        public class TestCommand : ICommand
        {
            public int Num { get; set; }
            public TestCommand(int num)
            {
                Num = num;
            }
        }

        public class TestCommandHandler : IAsyncCommandHandler<TestCommand>
        {
            public Task HandleAsync(TestCommand command)
            {
                Counter = command.Num;
                return Task.FromResult(0);
            }
        }
    }
}
