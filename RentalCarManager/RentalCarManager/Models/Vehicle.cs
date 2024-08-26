using RentalCarManager.CustomDataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarManager.Models
{
    internal abstract class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [Required(ErrorMessage = "Make is required."), FilterName("Vehicle Make")]
        public required string Make { get; set; }
        [Required(ErrorMessage = "Model is required."), FilterName("Vehicle Model")]
        public required string Model { get; set; }
        [Required(ErrorMessage = "License plate is required."), Display(Name = "License Plate")]
        public required string LicensePlate { get; set; }
        [Required(ErrorMessage = "Color of vehicle is required."), FilterName("Vehicle Color")]
        public required string Color { get; set; }
        [Required(ErrorMessage = "Year of registration is required."), DynamicDateRange, FilterName("Year of registration")]
        public required int Year { get; set; }
        [Required(ErrorMessage = "Acceleration is required."), FilterName("Vehicle Acceleration (0-100km/h)")]
        public required double Acceleration { get; set; }
        [Required, Range(0.01, 10000.00, ErrorMessage = "Price must be between €0.01 and €10,000.00"), DisplayFormat(DataFormatString = "{0:C}"), Display(Name = "Daily Rental Rate (€)"), FilterName("Daily rental rate (€)")]
        public required double DailyRentalRate { get; set; }
        [FilterName("Air Conditioning System")]
        public AC? AC { get; set; }
        [FilterName("Radio System")]
        public Radio? Radio { get; set; }
        [SystemManaged]
        public CustomerInfo? Customer { get; set; }
        [NotMapped]
        public bool Rented { get { return Customer != null; } }

        //public virtual bool PreFlightCheck()
        //{
        // This method is unnecessary because the user is presented with a filter page that only shows available vehicles based on their criteria.
        // The FilterVehiclesByProperties method in VehicleService ensures that only vehicles without an associated customer (i.e., those with a null CustomerInfo property) are selectable,
        // indicating that the vehicle is not currently rented, by checking the Rented propery.
        //
        // However if I was to use this method, users would need to input their own values for any filterable properties.
        // Given the extensive range of possible values, it might take a while for them to find a vehicle that matches their exact specifications.
        // If they manage to find a match, I would include an override in the specific vehicle classes (e.g., Car, Truck, Bus)
        // and ensure that any additional property requirements are met.
        //}
    }
}
