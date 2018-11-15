//using ModernStore.Domain.Entities;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Mapping;
using ModernStore.Shared;
using System.Data.Entity;

namespace ModernStore.Infra.Contexts
{
    //*********************** INSTRUÇÕES ENTITY FRAMEWORK *******************
    // - Criar a classe DataContext (Ex: ModernStoreDataContext)
    // - Instalar o Entity Framework no projeto > Install-Package Entityframework 
    // - Criar um construtor com a connString 
    // - Adicionar Referencias do Domain e Domain.Shared
    // - Instalar o FluentValidator (Para comunicação com o domínio)
    // - Criar os DbSet para o mapeamento das tabelas
    // - Criar uma pasta Mapping
    // - Dentro da pasta Mapping Criar as classes de papeamento das Entidades com o Bando de Dados



    public class ModernStoreDataContext : DbContext
    {


        //public ModernStoreDataContext() : base(@"data source=(localdb)\MSSQLLocalDB;initial catalog=ModernStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        public ModernStoreDataContext() : base(RuntimeSettings.ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false; //Leitura Rapida
            Configuration.ProxyCreationEnabled = false; //Propriedades dos campos das entidades (USO COM API)
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

       public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

    }
}
