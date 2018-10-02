using FluentValidator;
using System;

namespace ModernStore.Shared.Entities
{
    //Classe de validação
    //Abstract para não ser instanciada
    public abstract class Entity : Notifiable
    {

        protected Entity()
        {
            Id = Guid.NewGuid();

        }
        public Guid Id { get; private set; }


    }
}
