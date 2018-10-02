using ModernStore.Shared.Commands;
using System;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterOrderItemCommad : ICommand
    {
        public Guid Product { get; set; }

        public int Quantity { get; set; }
    }
}
