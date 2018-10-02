using FluentValidator;


namespace ModernStore.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }

        public Email(string address)
        {
            Address = address;
            new ValidationContract<Email>(this)
                    .IsEmail(x => x.Address, "Email Inválido");
        }

        public string Address { get; private set; }
    }
}
