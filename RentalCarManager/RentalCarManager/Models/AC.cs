using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarManager.Models
{
    class AC : IOptionalFeatures
    {
        [Required(ErrorMessage = "Minimum Temperature is required.")]
        public double MinTemperature { get; set; }
        [Required(ErrorMessage = "Maximum Temperature is required.")]
        public double MaxTemperature { get; set; }
        [NotMapped]
        public string GetDescription
        {
            get { return $"{MinTemperature}\u00B0C - {MaxTemperature}\u00B0C"; }
        }
    }
}
