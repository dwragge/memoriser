using System;
using System.Threading.Tasks;
using Memoriser.App.Commands;
using Moq;

namespace Memoriser.UnitTests.API
{
    public class MockCommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly Mock<IAsyncCommandHandler<TCommand>> _mock;
        private bool _setupCalled;

        public MockCommandHandler<TCommand> ReturnsForAll()
        {
            _mock.Setup(x => x.HandleAsync(It.IsAny<TCommand>())).Returns(Task.FromResult(0));
            _setupCalled = true;
            return this;
        }
        public IAsyncCommandHandler<TCommand> Handler
        {
            get
            {
                if (!_setupCalled)
                {
                    throw new InvalidOperationException("Tried to get handler before being setup.");
                }
                return _mock.Object;
            }
        }

        public MockCommandHandler()
        {
            _mock = new Mock<IAsyncCommandHandler<TCommand>>();
        }
    }
}
