using BandMate.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BandMate.Domain.Persistence
{
    public class BandMateContext : DbContext
    {
        public BandMateContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<Election> Elections { get; set; }
        public DbSet<ElectionVote> ElectionVotes { get; set; }
        public DbSet<SongListType> SongListTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongAccount> SongAccounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SetList> SetLists { get; set; }
        public DbSet<SetListItem> SetListItems { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<BandName> BandNames { get; set; }
        public DbSet<BandAccount> BandAccounts { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectionVote>(eb =>
            {
                eb.ToTable("ElectionVote");

                eb.HasOne(ev => ev.Election)
                    .WithMany(ev => ev.ElectionVotes)
                    .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(ev => ev.Account)
                    .WithMany(ev => ev.ElectionVotes)
                    .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(ev => ev.Song)
                    .WithMany(ev => ev.ElectionVotes)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Song>(eb =>
            {
                eb.ToTable("Song");

                eb.Property(s => s.CreatedDate)
                    .HasDefaultValueSql("getutcdate()");

                eb.HasMany(s => s.ElectionSongs)
                .WithOne(es => es.Song)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Election>()
                .ToTable("Election")
                .HasMany(e => e.ElectionSongs)
                .WithOne(e => e.Election)
                .OnDelete(DeleteBehavior.NoAction);

            //new PasswordResetRequestConfiguration()
            //    .Configure(modelBuilder.Entity<PasswordResetRequest>());

            modelBuilder.Entity<BandAccount>()
                .ToTable("BandAccount")
                .HasKey(ba => new { ba.BandID, ba.AccountID });

            modelBuilder.Entity<ElectionSong>()
                .ToTable("ElectionSong")
                .HasKey(es => new { es.ElectionID, es.SongID });

            modelBuilder.Entity<ElectionVote>()
                .ToTable("ElectionVote")
                .HasKey(ev => new { ev.ElectionID, ev.SongID, ev.AccountID });

            modelBuilder.Entity<SongAccount>()
                .ToTable("SongAccount")
                .HasKey(sa => new { sa.SongID, sa.AccountID });

            modelBuilder.Entity<SetListItem>()
                .ToTable("SetListItem")
                .HasOne(sli => sli.Song)
                .WithMany(s => s.SetListItems)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .ToTable("Account");

            modelBuilder.Entity<Address>()
                .ToTable("Address");

            modelBuilder.Entity<BandName>()
                .ToTable("BandName");

            modelBuilder.Entity<Band>()
                .ToTable("Band");

            modelBuilder.Entity<Gig>()
                .ToTable("Gig");

            modelBuilder.Entity<Rating>()
                .ToTable("Rating");

            modelBuilder.Entity<SetList>(eb =>
            {
                eb.ToTable("SetList");

                eb.Property(sl => sl.Created)
                    .HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<SongListType>()
                .ToTable("SongListType");

            modelBuilder.Entity<Venue>()
                .ToTable("Venue");
        }
    }
}
