using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using ModernStore.Domain.Commands.Inputs;

namespace ModernStore.ConsoleForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var commad = new RegisterOrderCommad
            {
                Customer = Guid.NewGuid(),
                DeliveryFee = 10,
                Discount = 2,
                Items = new List<RegisterOrderItemCommad>
                {
                    new RegisterOrderItemCommad
                    {
                        Product = Guid.NewGuid(),
                        Quantity = 2
                    }
                }
            };

            GenerateOrder(
                new FakeCustomerRepository(),
                new FakeProductRepository(),
                new FakeOrderRepository(),
                commad);
        }

        public static void GenerateOrder(
                                ICustomerRepository customerRepository,
                                IProductRepository productRepository,
                                IOrderRepository orderRepository,
                                RegisterOrderCommad command)
        {

            var handler = new OrderCommandHandler(customerRepository, productRepository, orderRepository);
            //Tudo que o handler precisa já vem com a classe somente os dados te tela vem do command
            handler.Handle(command);

            if(handler.IsValid())
                Console.WriteLine("Padido Cadastrado com successo");


            Console.ReadKey();
        }
    }

    class FakeProductRepository : IProductRepository
    {
        public Product Get(Guid id)
        {
            return new Product("Mouse", 10, 50, "Image"); ;
        }
    }

    class FakeCustomerRepository : ICustomerRepository
    {
        public bool DocumentExists(string document)
        {
            throw new NotImplementedException();
        }

        public Customer Get(Guid id)
        {
            return null;
        }

        public Customer GetByUserId(Guid id)
        {
            return new Customer(
                    new Name("Lucas", "Scarpelini"),
                    new Document("22538388787"),
                    new Email("llaksjd@asd.com"),
                    new User("lscarpelini", "pass","pass")
                );
        }

        public void Save(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

    class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            
        }
    }
}
