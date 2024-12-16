using GudumholmIdærtAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<ActiveMember> ActiveMembers { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<ActiveMemberSport> ActiveMemberSports { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=sql.itcn.dk;Database=magn82442.SKOLE;User ID=magn8244.SKOLE;Password=72Xh87JnCn;;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Member
        modelBuilder.Entity<Member>()
         .HasDiscriminator<string>("MemberType")
         .HasValue<ActiveMember>("Active")
         .HasValue<PassiveMember>("Passive")
         .HasValue<BestyrelseMember>("Bestyrelse")
         .HasValue<ParentMember>("Parant");

        modelBuilder.Entity<Member>()
           .HasOne(m => m.House)
           .WithMany(h => h.Members)
           .HasForeignKey(m => m.HouseId);

        modelBuilder.Entity<Member>().UseTphMappingStrategy();
        modelBuilder.Entity<Member>().ToTable("Members");

        modelBuilder.Entity<Member>()
            .Property<string>("MemberType")
            .IsRequired();

        //house
        modelBuilder.Entity<House>()
            .HasKey(h=>h.HouseId);


        //activemember
        modelBuilder.Entity<ActiveMemberSport>()
             .HasKey(am => new { am.ActiveMemberId, am.SportId });

        modelBuilder.Entity<ActiveMemberSport>()
            .HasOne(am => am.ActiveMember)
            .WithMany(am => am.ActiveMemberSports)
            .HasForeignKey(am => am.ActiveMemberId);

        modelBuilder.Entity<ActiveMemberSport>()
            .HasOne(am => am.Sport)
            .WithMany(s => s.ActiveMemberSports)
            .HasForeignKey(am => am.SportId);

        //bestyrlseMember
        modelBuilder.Entity<BestyrelseMember>()
        .HasOne(b => b.Sport)
        .WithMany() 
        .HasForeignKey(b => b.SportId)
        .OnDelete(DeleteBehavior.Restrict);

        //parant
        modelBuilder.Entity<ParentMember>()
        .HasMany(pm => pm.Children)
        .WithOne()
        .OnDelete(DeleteBehavior.Restrict);

    }
}
