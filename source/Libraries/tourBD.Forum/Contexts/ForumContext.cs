using Microsoft.EntityFrameworkCore;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Contexts
{
    public class ForumContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyname;

        public ForumContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyname = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    _connectionString,
                    b => b.MigrationsAssembly(_migrationAssemblyname)
                );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post);

            modelBuilder.Entity<Like>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Replay>()
                .HasOne(r => r.Comment)
                .WithMany(c => c.Replays)
                .HasForeignKey(r => r.CommentId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Replay> Replays { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
