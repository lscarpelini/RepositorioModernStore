using ModernStore.Domain.Commands.Results.Querys;
using ModernStore.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ModernStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);
        IEnumerable<GetProductListCommandResult> Get();
    }
}
