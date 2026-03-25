using System;

namespace BandMate.Domain.Core.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }

        public int? SongID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? BpmStart { get; set; }
        public int? BpmEnd { get; set; }

        public int? Repetitions { get; set; }

        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public bool IsCompleted => this.CompletedDate.HasValue;

        public virtual Song Song { get; set; }
    }
}
