using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
    public class TodoDbContext : DbContext {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {
        }
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Optional: extra constraints (most are already via DataAnnotations)
            modelBuilder.Entity<TodoItem>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<TodoItem>()
                .Property(x => x.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<TodoItem>()
                .Property(x => x.IsCompleted)
                .HasDefaultValue(false);
        }
    }
}
