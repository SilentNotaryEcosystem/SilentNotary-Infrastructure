using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using In.Domain;

namespace In.Entity
{
    public abstract class EntityBase<TId> : IHasKey<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TId Id { get; set; }

        public object GetId()
        {
            return Id;
        }
    }
}
