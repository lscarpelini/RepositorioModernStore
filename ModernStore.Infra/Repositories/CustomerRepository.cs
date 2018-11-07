using ModernStore.Domain.Repositories;
using System;
using System.Data.Entity;//Usada para fazer o Include
using System.Linq;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Contexts;
using ModernStore.Domain.Commands.Results.Querys;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Customer Get(Guid id)
        {
            return _context.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public GetCustomerCommandResult Get(string username)
        {
            ////Metodo para carregar o DTO via Entity Framework
            //return _context
            //    .Customers
            //    .Include(x => x.User)
            //    .AsNoTracking()
            //    .Select(x => new GetCustomerCommandResult
            //    {
            //        Name = x.Name.ToString(),
            //        Document = x.Document.Number,
            //        Active = x.User.Active,
            //        Email = x.Email.Address,
            //        Password = x.User.Password,
            //        Username = x.User.Username
            //    })
            //    .FirstOrDefault(x => x.Username == username);

            ////Metodo para carregar o DTO via Dapper
            using (var conn = new SqlConnection(@"data source=(localdb)\MSSQLLocalDB;initial catalog=ModernStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                string query = "SELECT* FROM[GetCustomerInfoView] WHERE [Active] = 1 AND [Username]=@username";
                conn.Open();
                return conn
                    .Query<GetCustomerCommandResult>(query,
                    new { username = username }) //Similar ao Include
                    .FirstOrDefault();
            }
        }

        public Customer GetByUsername(string username)
        {
            return _context.Customers.Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.User.Username == username);
        }
    }
}
