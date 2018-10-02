using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;
using System;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterCustomerCommad : ICommand
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string BirthDate { get; private set; }

        public string Email { get; private set; }

        public string Document { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        //Campo somente usado no comand, Pois não iremos salvar 2x a mesma informação no banco
        public string ConfirmPassword { get; private set; }
    }
}
