using FluentValidator;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class OrderCommandHandler : Notifiable, ICommandHandler<RegisterOrderCommad>
    {
        //Dependencias da classe
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(ICustomerRepository CustomerRepository,
                                   IProductRepository ProductRepository,
                                   IOrderRepository OrderRepository)
        {
            _customerRepository = CustomerRepository;
            _productRepository = ProductRepository;
            _orderRepository = OrderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommad command)
        {
            //Instancia um novo cliente vindo do repositório
            var customer = _customerRepository.Get(command.Customer);
            
            //Gera pedido a partir do repositório
            var order = new Order(customer, command.DeliveryFee, command.Discount);

            //Adiciona os itens do pedido
            foreach (var item in command.Items)
            {
                var product = _productRepository.Get(item.Product);     //Chave do Produto
                order.AddItem(new OrderItem(product, item.Quantity));  //Quantidade do Produto
            }
            
            //Se o pedido tiver notificações
            AddNotifications(order.Notifications);

            //Persiste no banco
            if (IsValid())
                _orderRepository.Save(order);

            //Retorna Algo para tela
            return new RegisterOrderCommandResult(order.Number.ToString());

        }
    }
}
