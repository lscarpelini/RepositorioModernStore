using ModernStore.Domain.Commands.Results.Querys;
using ModernStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernStore.Domain.Repositories
{
    //Repository Patern
    //Uma unidade de acesso a dados para cada entidade
    //Somente Implementação no Dominio da Aplicação   
    public interface ICustomerRepository
    {
        Customer Get(Guid id);

        GetCustomerCommandResult Get(string username);

        bool DocumentExists(string document);

        void Save(Customer customer);

        void Update(Customer customer);
    }
}
