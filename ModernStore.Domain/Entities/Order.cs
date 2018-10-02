using FluentValidator;
using ModernStore.Domain.Enums;
using ModernStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernStore.Domain.Entities
{
    public class Order : Entity
    {
        public List<OrderItem> _Items;

        protected Order() { }

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0,8).ToUpper();
            Status = (int)EOrderStatus.Created;
            DeliveryFee = deliveryFee;
            Discount = discount;

            _Items = new List<OrderItem>();

            new ValidationContract<Order>(this)
                    .IsGreaterThan(x => x.DeliveryFee, -1);//Taxa de entrega de 0 até qualquer numero
        }

        public Customer Customer { get; private set; }

        public DateTime CreateDate { get; private set; }

        public string Number { get; private set; }

        public int Status { get; private set; }

        //Coleção somente de leitura
        public ICollection<OrderItem> Items => _Items.ToArray();

        public decimal DeliveryFee { get; private set; }

        public decimal Discount { get; private set; }

        public decimal Subtotal() => Items.Sum(x => x.Total());

        public decimal Total() => Subtotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            //Adiciona notificações do item do pedido no proprio pedido.
            AddNotifications(item.Notifications);
            if(item.IsValid())
                _Items.Add(item);
        }

        //Registra pedido
        public void Place()
        {

        }
    }
}
