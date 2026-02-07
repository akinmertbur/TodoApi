using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Services {
    public class TodoService : ITodoService {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<TodoReadDto>> GetAllAsync(
            bool? isCompleted,
            string? search,
            int page,
            int pageSize) {
            // Safety defaults
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;
            pageSize = pageSize > 100 ? 100 : pageSize;

            IQueryable<TodoItem> query = _context.TodoItems.AsNoTracking();

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (!string.IsNullOrWhiteSpace(search)) {
                string term = search.Trim().ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(term));
            }

            query = query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var items = await query.ToListAsync();

            return items.Select(MapToReadDto);
        }

        public async Task<TodoReadDto?> GetByIdAsync(int id) {
            var item = await _context.TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return item is null ? null : MapToReadDto(item);
        }

        public async Task<TodoReadDto> CreateAsync(TodoCreateDto dto) {
            var now = DateTime.UtcNow;

            var entity = new TodoItem {
                Title = dto.Title.Trim(),
                Description = dto.Description?.Trim(),
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                IsCompleted = false,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();

            return MapToReadDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, TodoUpdateDto dto) {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            entity.Title = dto.Title.Trim();
            entity.Description = dto.Description?.Trim();
            entity.DueDate = dto.DueDate;
            entity.Priority = dto.Priority;
            entity.IsCompleted = dto.IsCompleted;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id) {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteAsync(int id) {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            if (!entity.IsCompleted) {
                entity.IsCompleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        private static TodoReadDto MapToReadDto(TodoItem item) {
            return new TodoReadDto {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                DueDate = item.DueDate,
                Priority = item.Priority,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }
    }
}
