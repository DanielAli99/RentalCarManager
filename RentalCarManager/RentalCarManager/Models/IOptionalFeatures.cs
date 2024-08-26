using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarManager.Models
{
    internal interface IOptionalFeatures
    {
        [NotMapped]
        string GetDescription { get; }
    }
}
