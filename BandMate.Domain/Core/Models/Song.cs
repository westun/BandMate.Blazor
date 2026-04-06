using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class Song
    {
        public int SongID { get; set; }

        [Required]
        public int BandID { get; set; }

        public int? CreatedByAccountID { get; set; }

        public int? SongListTypeID { get; set; }

        public string Artist { get; set; }

        [Required]
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

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset CreatedDate { get; set; }

        public string? ExternalAlbumID { get; set; }

        public virtual Band Band { get; set; }
        public virtual SongListType SongListType { get; set; }
        public virtual Account CreatedByAccount { get; set; }
        public virtual ICollection<ElectionSong> ElectionSongs { get; set; }
        public virtual ICollection<ElectionVote> ElectionVotes { get; set; }
        public virtual ICollection<SongAccount> SongAccounts { get; set; }
        public virtual ICollection<SetListItem> SetListItems { get; set; }
    }
}