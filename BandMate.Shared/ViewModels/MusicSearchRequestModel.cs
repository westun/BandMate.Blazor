using System.ComponentModel.DataAnnotations;

namespace BandMate.Shared.ViewModels
{
    public class MusicSearchRequestModel
    {
        [Required(ErrorMessage = "Search Criteria is required")]
        public string Criteria { get; set; } = string.Empty;
    }
}
