using TodoApi.DTOs;

namespace TodoApi.Services {
    public interface ITodoService {
        Task<IEnumerable<TodoReadDto>> GetAllAsync(
            bool? isCompleted,
            string? search,
            int page,
            int pageSize);

        Task<TodoReadDto?> GetByIdAsync(int id);

        Task<TodoReadDto> CreateAsync(TodoCreateDto dto);

        Task<bool> UpdateAsync(int id, TodoUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> CompleteAsync(int id);
    }
}
