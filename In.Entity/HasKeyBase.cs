using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace In.Entity
{
    public abstract class HasKeyBase<TId> : IHasKey<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TId Id { get; set; }
    }
}
