namespace BandMate.Blazor.DTO.MusicCatalog
{
    public class ArtistDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<AlbumDTO> Albums { get; set; }
    }
}
