using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<MovieProducer> MovieProducers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieProducer>()
            .HasKey(mp => new { mp.MovieId, mp.ProducerId });

        modelBuilder.Entity<MovieProducer>()
            .HasOne(mp => mp.Movie)
            .WithMany(m => m.MovieProducers)
            .HasForeignKey(mp => mp.MovieId);

        modelBuilder.Entity<MovieProducer>()
            .HasOne(mp => mp.Producer)
            .WithMany(p => p.MovieProducers)
            .HasForeignKey(mp => mp.ProducerId);
    }
}
