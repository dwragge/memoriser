using System;
using System.Threading.Tasks;
using Memoriser.App.Query;
using Moq;

namespace Memoriser.UnitTests.API
{
    public class MockQueryHandler<TQuery, TOut> where TQuery : IQuery<TOut>
    {
        private readonly Mock<IAsyncQueryHandler<TQuery, TOut>> _mock;
        private bool _setupCalled;
        public MockQueryHandler<TQuery, TOut> ReturnsForAll(TOut returnValue)
        {
            _mock.Setup(x => x.QueryAsync(It.IsAny<TQuery>())).Returns(Task.FromResult(returnValue));
            _setupCalled = true;
            return this;
        }

        public IAsyncQueryHandler<TQuery, TOut> Handler
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

        public MockQueryHandler()
        {
            _mock = new Mock<IAsyncQueryHandler<TQuery, TOut>>();
        }
    }
}
