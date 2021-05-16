using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Models;

namespace Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>

    {
    public OrderItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
    }
}
