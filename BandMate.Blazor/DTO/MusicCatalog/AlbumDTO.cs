namespace BandMate.Blazor.DTO.MusicCatalog
{
    public class AlbumDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AlbumImgCoverUrl { get; set; }
        public string Release_Date { get; set; }
        public IEnumerable<TrackDTO> Tracks { get; set; }
    }
}
