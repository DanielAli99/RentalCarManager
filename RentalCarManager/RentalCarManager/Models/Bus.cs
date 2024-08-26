using RentalCarManager.CustomDataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace RentalCarManager.Models
{
    internal class Bus : Vehicle
    {
       [Required, FilterName("Extra doors"), Display(Name = "Extra doors")]
        public bool ExtraDoors { get; set; } 
    }
}
