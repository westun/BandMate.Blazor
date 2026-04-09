using System.ComponentModel.DataAnnotations;

namespace BandMate.Shared.ViewModels
{
    public class CreateBandModel
    {
        [Required(ErrorMessage = "Band name is required.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; } = string.Empty;
    }
}
