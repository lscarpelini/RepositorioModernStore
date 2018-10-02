using ModernStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore.Infra.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("Order");
            HasKey(x => x.Id);
            Property(x => x.CreateDate);
            Property(x => x.DeliveryFee).HasColumnType("money");
            Property(x => x.Discount).HasColumnType("money");
            Property(x => x.Number).HasMaxLength(8).IsFixedLength();
            Property(x => x.Status);
            //Para mepeamento com outra tabela
            HasMany(x => x.Items);
            //Para um mapeamento de tabela obrigatória
            HasRequired(x => x.Customer);
;
        }
    }
}
