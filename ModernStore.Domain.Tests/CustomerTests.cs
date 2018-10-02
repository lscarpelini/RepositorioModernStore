using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {
        //[TestMethod]
        //[TestCategory("Customer - Novo Cliente")]
        //public void Dado_Nome_Invalido_Retornar_Um_Erro()
        //{
        //    var user = new User("Lucas", "Lucas");
        //    var customer = new Customer(new Name("Lucas", "Scarpelini"), DateTime.Now, new Document("22538381878"), new Email("LSCARPELINI@HOTMAIL.COM"), user);
        //    Assert.AreEqual(1, customer.Notifications);
        //}

        [TestMethod]
        [TestCategory("Customer - Novo Cliente")]
        public void Dado_SobreNome_Invalido_Retornar_Um_Erro()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Customer - Novo Cliente")]
        public void Dado_Email_Invalido_Retornar_Um_Erro()
        {
            Assert.Fail();
        }
    }
}
