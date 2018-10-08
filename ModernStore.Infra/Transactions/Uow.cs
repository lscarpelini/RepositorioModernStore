using ModernStore.Infra.Contexts;
using System;

namespace ModernStore.Infra.Transactions
{
    public class Uow : IUow
    {

        private readonly ModernStoreDataContext _context;

        public Uow(ModernStoreDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //Rollback automatico do EF :)
        }
    }
}
