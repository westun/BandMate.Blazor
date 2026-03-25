using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class Election
    {
        public int ElectionID { get; set; }
        
        [ForeignKey("Band")]
        public int BandID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public bool Voted { get; set; }

        [Display(Name = "Start Date")]
        public DateTimeOffset? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTimeOffset? EndDate { get; set; }

        public virtual Band Band { get; set; }
        public virtual ICollection<ElectionSong> ElectionSongs { get; set; }
        public virtual ICollection<ElectionVote> ElectionVotes { get; set; }

    }
}