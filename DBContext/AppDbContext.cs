using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=LAPTOP-COT3JNJ0;Database=GudumholmIdærtAPI;User ID=Username2;Password=Password2;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>()
            .HasDiscriminator<string>("MemberType") // Column used for the discriminator
            .HasValue<ActiveMember>("Active")      // Discriminator value for ActiveMember
            .HasValue<PassiveMember>("Passive");   // Discriminator value for PassiveMember
    }
}
