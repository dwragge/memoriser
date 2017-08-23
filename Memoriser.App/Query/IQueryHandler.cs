using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memoriser.App.Query
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
