using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterOrderCommad : ICommand
    {
        public Guid Customer { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal Discount { get; set; }

        public IEnumerable<RegisterOrderItemCommad> Items { get; set; }

    }
}
