using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;
using System;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterCustomerCommad : ICommand
    {
        public string FirstName { get;  set; }

        public string LastName { get;  set; }

        public string Email { get;  set; }

        public string BirthDate { get;  set; }

        public string Document { get;  set; }

        public string Username { get;  set; }

        public string Password { get;  set; }

        //Campo somente usado no comand, Pois não iremos salvar 2x a mesma informação no banco
        public string ConfirmPassword { get;  set; }
    }
}
