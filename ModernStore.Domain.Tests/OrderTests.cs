using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        [TestCategory("Order - Novo pedido")]
        public void Produto_Fora_de_Estoque()
        {

            //var customer = new Customer(new Name("Lucas","Scarpelini"), new DateTime(1982, 7, 22), new Document("2253838187"), new Email("lscarpelini@hotmail.com"), new User("Lucas", "123"));

            //var mouse = new Product("Mouse Gamer", 100, 0, "Mouse.jpg");

            //var order = new Order(customer, 8, 10);
            //order.AddItem(new OrderItem(mouse, 1));

            ////Assert.AreEqual(1, order.Notifications.Count);
            //Assert.IsFalse(order.IsValid());

        }
    }
}
