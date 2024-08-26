using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarManager.Models
{
    internal class Radio : IOptionalFeatures
    {
        [Required(ErrorMessage = "Minimum Frequency is required.")]
        public double MinFrequency { get; set; }
        [Required(ErrorMessage = "Maximum Frequency is required.")]
        public double MaxFrequency { get; set; }
        [NotMapped]
        public string GetDescription
        {
            get { return $"{MinFrequency}Hz - {MaxFrequency}Hz"; }
            
        }
    }
}
