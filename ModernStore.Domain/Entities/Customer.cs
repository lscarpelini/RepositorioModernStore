using FluentValidator;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Entities;
using System;

namespace ModernStore.Domain.Entities
{
    public class Customer : Entity
    {
        protected Customer() {}

        public Customer(Name name,  Document document, Email email, User user)
        {

            Name = name;
            Email = email;
            User = user;
            Document = document;

            //Retorna validações dos Value Objecs
            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(document.Notifications);

        }

        public Name Name { get; private  set; }

        public DateTime? BirthDate { get; private set; }

        public Email Email { get; private set; }

        public User User { get; private set; }

        public Document Document { get; private set; }



    }
}
