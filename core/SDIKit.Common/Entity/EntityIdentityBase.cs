using SDIKit.Common.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDIKit.Common.Entity
{
    public abstract class EntityIdentityBase : IEntityIdentity<long>
    {
        protected EntityIdentityBase()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}