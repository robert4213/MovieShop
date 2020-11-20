using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext:DbContext //represent database 
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<UserRole>().HasKey(ur => new {ur.UserId, ur.RoleId});
            modelBuilder.Entity<MovieCast>().HasKey(mc => new {mc.MovieId,mc.CastId,mc.Character});
            modelBuilder.Entity<MovieCrew>().HasKey(mc => new {mc.CrewId, mc.MovieId, mc.Department, mc.Job});
            modelBuilder.Entity<Purchase>().Property(purchase => purchase.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Review>(ConfigureReview);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2048);
            builder.Property(t => t.Name).HasMaxLength(2048);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // Configure
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.HasIndex(m => m.ImdbUrl).IsUnique();
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Property(m => m.Budget).HasPrecision(18, 2);
            builder.Property(m => m.Revenue).HasPrecision(18, 2);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(m => new {m.Genreid, m.Movieid});
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => new {r.MovieId,r.UserId});
            builder.Property(r => r.Rating).IsRequired().HasPrecision(3, 2);
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }
        
        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<Crew> Crews { get; set; }
        
        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<MovieCrew> MovieCrews { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Favorite> Favorites { get; set; }
    }
}