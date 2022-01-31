using SDIKit.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDIKit.Common.Entity
{
    public interface IEntity
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}