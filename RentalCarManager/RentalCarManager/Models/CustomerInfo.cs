using System.ComponentModel.DataAnnotations;

namespace RentalCarManager.Models
{
    internal class CustomerInfo
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Name of customer is required.")]
        public required string Name {  get; set; }
        [Required]
        public required DateTime DateRented { get; set; }
        [Required]
        public required Vehicle Vehicle { get; set; }
    }
}
