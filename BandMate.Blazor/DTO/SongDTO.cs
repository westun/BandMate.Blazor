namespace BandMate.Blazor.DTO
{
    public class SongDTO
    {
        public int SongID { get; set; }
        public int? SongListTypeID { get; set; }
        public string SongListType { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int? Bpm { get; set; }
        public string? KeySignature { get; set; }
        public string? Mp3FileName { get; set; }
        public string? YoutubeURL { get; set; }
        public string? TabFileName { get; set; }
        public string? LyricFileName { get; set; }
        public string? SheetMusicFileName { get; set; }
        public string? SheetMusicURL { get; set; }
        public string? LyricURL { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string MyNotes { get; set; }
        public string CreatedByAccountID { get; set; }
    }
}
