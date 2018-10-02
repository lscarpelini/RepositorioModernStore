using ModernStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore.Infra.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        //Arquivo serve para modelar o banco gerado pelo entity
        public CustomerMap()
        {
            ToTable("Customer");//Para Renomear a Tabela
            HasKey(x => x.Id);  //Define chave primaria
            Property(x => x.BirthDate);
            Property(x => x.Document.Number).IsRequired().HasMaxLength(11).IsFixedLength();
            Property(x => x.Email.Address).IsRequired().HasMaxLength(160).HasColumnName("Email");
            Property(x => x.Name.FirstName).IsRequired().HasMaxLength(60);
            Property(x => x.Name.LastName).IsRequired().HasMaxLength(60);
            HasRequired(x => x.User);
        }
    }
}
