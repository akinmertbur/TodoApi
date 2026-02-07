using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
    public static class DbInitializer {
        public static async Task SeedAsync(TodoDbContext context) {
            // Ensure DB exists + migrations applied (safe for dev)
            await context.Database.MigrateAsync();

            // Seed only if empty
            if (await context.TodoItems.AnyAsync())
                return;

            var now = DateTime.UtcNow;

            var todos = new List<TodoItem>
            {
                new TodoItem
                {
                    Title = "Finish CRUD endpoints",
                    Description = "Implement GET/POST/PUT/DELETE for todos",
                    Priority = 1,
                    IsCompleted = false,
                    DueDate = now.AddDays(3),
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new TodoItem
                {
                    Title = "Add PATCH /complete endpoint",
                    Description = "Set IsCompleted=true and update UpdatedAt",
                    Priority = 2,
                    IsCompleted = false,
                    DueDate = now.AddDays(2),
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new TodoItem
                {
                    Title = "Write a clean README",
                    Description = "Include setup steps, migrations, endpoints",
                    Priority = 3,
                    IsCompleted = false,
                    DueDate = now.AddDays(5),
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new TodoItem
                {
                    Title = "Refactor LINQ queries",
                    Description = "Keep filtering + pagination readable",
                    Priority = 2,
                    IsCompleted = true,
                    DueDate = null,
                    CreatedAt = now.AddDays(-2),
                    UpdatedAt = now.AddDays(-1)
                },
                new TodoItem
                {
                    Title = "Polish Swagger docs",
                    Description = "Add summaries and response codes",
                    Priority = 1,
                    IsCompleted = false,
                    DueDate = now.AddDays(1),
                    CreatedAt = now,
                    UpdatedAt = now
                }
            };

            context.TodoItems.AddRange(todos);
            await context.SaveChangesAsync();
        }
    }
}
