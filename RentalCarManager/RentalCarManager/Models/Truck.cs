using RentalCarManager.CustomDataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace RentalCarManager.Models
{
    internal class Truck : Vehicle
    {
        [Required(ErrorMessage = "Cargo Capacity is required."), Display(Name = "Cargo capacity (Litres)"), FilterName("Cargo capacity (Litres)")]
        public double CargoCapacity { get; set; }
    }
}
