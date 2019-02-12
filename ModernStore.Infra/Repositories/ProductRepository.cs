using System;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Contexts;
using System.Linq;
using ModernStore.Domain.Commands.Results.Querys;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;

        public ProductRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            ////Metodo para carregar o DTO via Dapper
            using (var conn = new SqlConnection(@"data source=(localdb)\MSSQLLocalDB;initial catalog=ModernStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                string query = "SELECT Id, Title, Price, Image FROM Product";
                conn.Open();
                return conn.Query<GetProductListCommandResult>(query);
            }
        }

        public Product Get(Guid id)
        {
            //AsNoTracking Não mapeia o produto
            //Portanto em uma operação de insert ele tenta inserir o produto novamente
            //Isso gera ERRO
            return _context
                .Products
                //.AsNoTracking() // Para não fazer o tracking do registro e melhorar a performance
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
