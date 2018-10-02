using FluentValidator;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Resources;
using ModernStore.Domain.Services;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerCommadHandler : Notifiable,
                    ICommandHandler<RegisterCustomerCommad>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommadHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(RegisterCustomerCommad Command)
        {
            //Passo 1 : Verificar de CPF de Cliente já existe
            if( _customerRepository.DocumentExists(Command.Document))
            {
                AddNotification("Document", "Este CPF já esta em uso");
                return null;
            }

            //Passo 2 : Gerar novo cliente
            var name = new Name(Command.FirstName, Command.LastName);
            var document = new Document(Command.Document);
            var email = new Email(Command.Email);
            var user = new User(Command.Username, Command.Password, Command.ConfirmPassword);
            var customer = new Customer(name, document, email, user);

            //Passo 3 : Adiciona notificações caso venha algo do domínio
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(user.Notifications);
            AddNotifications(customer.Notifications);

            //Passo 4 : Inserir no Banco
            if (IsValid())
                _customerRepository.Save(customer);


            //Passo 5 : Enviar E-mail de boas vindas
            _emailService.Send(customer.Name.ToString(),
                               customer.Email.ToString(),
                               string.Format(EmailTemplates.WelcomeEmailTitle, customer.Name),
                               string.Format(EmailTemplates.WelcomeEmailBody, customer.Name));

            //Passo 6 : Retornar Algo para tela
            return new RegisterCustomerCommandResult(customer.Id, customer.Name.ToString());

        }
    }
}
